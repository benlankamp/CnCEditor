using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CnCEditor.FileFormats
{
    public static class BinaryExtensions
    {
		/*
		 * Copyright 2007-2020 The OpenRA Developers (see AUTHORS)
		 * This file is part of OpenRA, which is free software. It is made
		 * available to you under the terms of the GNU General Public License
		 * as published by the Free Software Foundation, either version 3 of
		 * the License, or (at your option) any later version. For more
		 * information, see COPYING.
		 */

		public static T ByteToType<T>(this Stream stream)
        {
            int strucSize = Marshal.SizeOf(typeof(T));

            byte[] bytes = new byte[strucSize];
            stream.Read( bytes, 0, strucSize);

            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T theStructure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return theStructure;
        }

		public static int Peek(this Stream s)
		{
			var b = s.ReadByte();
			if (b == -1)
				return -1;
			s.Seek(-1, SeekOrigin.Current);
			return (byte)b;
		}

		public static byte ReadUInt8(this Stream s)
		{
			var b = s.ReadByte();
			if (b == -1)
				throw new EndOfStreamException();
			return (byte)b;
		}

		public static byte[] ReadBytes(this Stream s, int count)
		{
			if (count < 0)
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			var buffer = new byte[count];
			s.ReadBytes(buffer, 0, count);
			return buffer;
		}

		public static void ReadBytes(this Stream s, byte[] buffer, int offset, int count)
		{
			if (count < 0)
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			while (count > 0)
			{
				int bytesRead;
				if ((bytesRead = s.Read(buffer, offset, count)) == 0)
					throw new EndOfStreamException();
				offset += bytesRead;
				count -= bytesRead;
			}
		}

		public static ushort ReadUInt16(this Stream s)
		{
			return BitConverter.ToUInt16(s.ReadBytes(2), 0);
		}

		public static short ReadInt16(this Stream s)
		{
			return BitConverter.ToInt16(s.ReadBytes(2), 0);
		}

		public static uint ReadUInt32(this Stream s)
		{
			return BitConverter.ToUInt32(s.ReadBytes(4), 0);
		}

		public static int ReadInt32(this Stream s)
		{
			return BitConverter.ToInt32(s.ReadBytes(4), 0);
		}
	}

    public class SubStream : Stream
    {
        private Stream baseStream;
        private readonly long length;
        private long position;

        private uint originOffset;

        public SubStream(Stream baseStream, uint offset, uint length)
        {
            if (baseStream == null) throw new ArgumentNullException("Base stream is null");
            if (!baseStream.CanRead) throw new ArgumentException("Cannot read from base stream");
            if (offset < 0) throw new ArgumentOutOfRangeException("Offset is invalid");

            this.baseStream = baseStream;

            this.length = length;
            this.originOffset = offset;
            this.position = 0;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            CheckDisposed();

            long remaining = length - position;
            if (remaining <= 0) return 0;
            if (remaining < count) count = (int)remaining;

            // seek offset
            baseStream.Seek( this.originOffset + this.position, SeekOrigin.Begin );

            int read = baseStream.Read(buffer, 0, count);
            position += read;

            return read;
        }

        private void CheckDisposed()
        {
            if (baseStream == null) throw new ObjectDisposedException(GetType().Name);
        }

        public override long Length
        {
            get { CheckDisposed(); return length; }
        }

        public override bool CanRead
        {
            get { CheckDisposed(); return true; }
        }

        public override bool CanWrite
        {
            get { CheckDisposed(); return false; }
        }

        public override bool CanSeek
        {
            get { CheckDisposed(); return false; }
        }

        public override long Position
        {
            get
            {
                CheckDisposed();
                return position;
            }
            set { throw new NotSupportedException(); }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    this.position = offset;
                    return this.baseStream.Seek(this.originOffset + offset, SeekOrigin.Begin);
                case SeekOrigin.Current:
                    this.position += offset;
                    return this.baseStream.Seek(offset, SeekOrigin.Current);
                default:
                    throw new NotSupportedException();
            }
        }
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
        public override void Flush()
        {
            CheckDisposed(); baseStream.Flush();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (baseStream != null)
                {
                    try { baseStream.Dispose(); }
                    catch { }
                    baseStream = null;
                }
            }
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
