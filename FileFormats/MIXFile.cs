using CnCEditor.Encryption;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace CnCEditor.FileFormats
{
    public partial class MIXFile
    {
        public bool IsValid = false;

        public string FileName;

        public struct SubFile
        {
            public string Name;
            public UInt32 Offset;
            public UInt32 Size;
        }

        public List<SubFile> Files = new List<SubFile>();

        public bool IsDisposed { get; private set; }

        /* Private */
        private const string PubKey = "AihRvNoIbTn85FZRYNZRcT+i6KpU+maCsEqr3Q5q+LDB5tH7Tz2qQ38V";

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct AlternateHeader {
            public Int16 First;
            public Int16 Second;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct FileHeader
        {
            public Int16 Count;     // file count
            public Int32 Size;       // total size of all embedded files
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct FileIndex
        {
            public UInt32 CRC;
            public UInt32 Offset;
            public UInt32 Size;
        };

        private byte[] rawFile;
        private Stream rawStream;

        private FileHeader fileHeader;

        private long dataStart = 0;

        // read from disk
        public MIXFile(string fileName)
        {
            if (!File.Exists(fileName)) return;

            FileName = new FileInfo(fileName).Name;

            this.rawFile = File.ReadAllBytes(fileName);
            this.rawStream = new MemoryStream(this.rawFile);

            this.ParseFile();
        }

        // get from raw bytes
        public MIXFile(byte[] rawFile, string fileName)
        {
            FileName = fileName;

            this.rawFile = rawFile;
            this.rawStream = new MemoryStream(this.rawFile);

            this.ParseFile();
        }

        // read from substream
        public MIXFile(SubStream rawStream, string fileName)
        {
            FileName = fileName;
            this.rawStream = rawStream;

            this.ParseFile();
        }

        public byte[] GetFile(SubFile file)
        {
            rawStream.Seek(dataStart + file.Offset, SeekOrigin.Begin);
            return rawStream.ReadBytes((int)file.Size);
        }

        public byte[] GetFile(string fileName)
        {
            if ( Files.Count( x => x.Name == fileName ) == 0 ) return null;

            SubFile file = Files.Single(x => x.Name == fileName);
            return GetFile(file);
        }

        public SubStream GetFileAsStream(SubFile file)
        {
            rawStream.Seek(dataStart + file.Offset, SeekOrigin.Begin);
            return new SubStream(this.rawStream, (uint)rawStream.Position, file.Size);
        }

        private void ParseFile()
        { 
            AlternateHeader alternate;
            alternate = rawStream.ByteToType<AlternateHeader>();

            if (alternate.First == 0)
            {
                bool IsDigest = (alternate.Second & 0x01) != 0;
                bool IsEncrypted = (alternate.Second & 0x02) != 0;

                MemoryStream fileIndexStream;

                if(IsEncrypted)
                {
                    fileIndexStream = DecryptHeader(4, out dataStart);
                } else
                {
                    fileIndexStream = new MemoryStream(rawStream.ReadBytes(fileHeader.Size * Marshal.SizeOf(typeof(FileIndex))));
                }

                fileHeader = fileIndexStream.ByteToType<FileHeader>();

                if (rawStream.Length >= dataStart + fileHeader.Size)
                {
                    this.IsValid = true;

                    // parse the index
                    FileIndex fileIndex;

                    for (int f = 0; f < fileHeader.Count; f++)
                    {
                        // read index
                        fileIndex = fileIndexStream.ByteToType<FileIndex>();

                        // find file in known names
                        if (MIXDatabase.ValidFileNames.ContainsKey((uint)fileIndex.CRC))
                        {
                            SubFile newSubFile = new SubFile();
                            newSubFile.Name = MIXDatabase.ValidFileNames[(uint)fileIndex.CRC];
                            newSubFile.Offset = fileIndex.Offset;
                            newSubFile.Size = fileIndex.Size;
                            Files.Add(newSubFile);
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    if (rawStream != null)
                    {
                        rawStream.Close();
                        rawStream.Dispose();
                    }

                    this.rawFile = null;
                }

                IsDisposed = true;
            }
        }

        private MemoryStream DecryptHeader( long offset, out long headerEnd )
        {
            rawStream.Seek(offset, SeekOrigin.Begin);

            // Decrypt blowfish key
            var keyblock = rawStream.ReadBytes(80);
            var blowfishKey = new BlowfishKeyProvider().DecryptKey(keyblock);
            var fish = new Blowfish(blowfishKey);

            // Decrypt first block to work out the header length
            var ms = new BinaryReader( Decrypt( ReadBlocks(offset + 80, 1), fish ) );
            var numFiles = ms.ReadUInt16();

            // Decrypt the full header - round bytes up to a full block
            var blockCount = ( 13 + numFiles * 12 ) / 8;
            headerEnd = offset + 80 + blockCount * 8;

            return Decrypt( ReadBlocks( offset + 80, blockCount), fish );
        }

        private MemoryStream Decrypt(uint[] h, Blowfish fish)
        {
            var decrypted = fish.Decrypt(h);

            var ms = new MemoryStream(decrypted.Length * 4);
            var writer = new BinaryWriter(ms);
            foreach (var t in decrypted)
                writer.Write(t);
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        private uint[] ReadBlocks(long offset, int count)
        {
            rawStream.Seek(offset, SeekOrigin.Begin);

            var ret = new uint[2 * count];
            for (var i = 0; i < ret.Length; i++)
                ret[i] = rawStream.ReadUInt32();

            return ret;
        }
    }

    public static class MIXDatabase
    {
        public static Dictionary<uint,string> ValidFileNames = new Dictionary<uint,string>();

        public static void Init()
        {
            const string mixDbFilePath = @"Data\mixdatabase.dat";

            if (File.Exists(mixDbFilePath))
            {
                using (var s = new FileStream(mixDbFilePath, FileMode.Open))
                {
                    while (s.Peek() > -1)
                    {
                        var count = s.ReadUInt32();
                        var chars = new List<char>();

                        for (var i = 0; i < count; i++)
                        {
                            byte c;

                            // Read filename
                            while ((c = s.ReadUInt8()) != 0)
                            {
                                chars.Add((char)c);
                            }

                            string fileName = new string(chars.ToArray());

                            uint CRC_classic = MIXFile.HashFilename(fileName, HashType.Classic);
                            uint CRC_crc32 = MIXFile.HashFilename(fileName, HashType.CRC32);

                            if (!ValidFileNames.ContainsKey(CRC_classic)) ValidFileNames.Add(CRC_classic, fileName);
                            if (!ValidFileNames.ContainsKey(CRC_crc32)) ValidFileNames.Add(CRC_crc32, fileName);

                            chars.Clear();

                            // Skip comment
                            while ((c = s.ReadUInt8()) != 0) { }
                        }
                    }
                }
            }
        }
    }
}
