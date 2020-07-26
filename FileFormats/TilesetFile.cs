using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace CnCEditor.FileFormats
{
    public class TilesetFile
    {
        public Bitmap tileImage;

        private List<Color> Palette = new List<Color>(255);
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct TilesetHeader_Generic
        {
            public Int16 width; // always 24 (ICON_WIDTH)
            public Int16 height; // always 24 (ICON_HEIGHT)
            public Int16 count; // count of cells in set, not same as images
            public Int16 allocated; // is treated like a bool, always 0 in the file?
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct TilesetHeader_RA
        {
            public Int16 width;         // always 24 (ICON_WIDTH)
            public Int16 height;        // always 24 (ICON_HEIGHT)
            public Int16 count;         // count of cells in set, not same as images

            public Int16 allocated;     // is treated like a bool, always 0 in the file?

            public Int16 map_width;     // tile width in cells
            public Int16 map_height;    // tile height in cells

            public Int32 size;          // file size
            public Int32 imgoffset;     // offset of images
            public Int32 palettes;      // seems to always be 0x00000000
            public Int32 remaps;        // unknown, bitfield?
            public Int32 index2;        // array of images length, unknown
            public Int32 color_map;     // terrain type index, ra only
            public Int32 index1;        // image index for each cell
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct TilesetHeader_TD
        {
            public Int16 width; // always 24 (ICON_WIDTH)
            public Int16 height; // always 24 (ICON_HEIGHT)
            public Int16 count; // count of cells in set, not same as images
            public Int16 allocated; // is treated like a bool, always 0 in the file?

            public Int32 size; // filesize
            public Int32 icons; // always 0x00000020
            public Int32 palettes; // seems to always be 0x00000000
            public Int32 remaps; // unknown, bitfield?
            public Int32 trans_flag; // array of images length, unknown
            public Int32 map; // image index for each cell
        }

        public TilesetFile(byte[] rawData)
        {
            using ( var rawDataStream = new MemoryStream(rawData) )
            {
                // check if tileset is TD or RA
                rawDataStream.Seek(Marshal.SizeOf(typeof(TilesetHeader_Generic)) + sizeof(Int32), SeekOrigin.Begin);

                Int32 flag = rawDataStream.ReadInt32();

                rawDataStream.Seek(0, SeekOrigin.Begin);

                if (flag == 0x20)
                {
                    // TD
                    var tileSetHeader = rawDataStream.ByteToType<TilesetHeader_TD>();
                }
                else
                {
                    int headerSize = Marshal.SizeOf(typeof(TilesetHeader_RA));

                    // read palette
                    using (var paletteStream = File.Open(@"C:\Games\Red Alert\temperat.pal", FileMode.Open))
                    {
                        for(int i = 0 ; i <= 255; i++)
                        {
                            byte[] paletteEntry = new byte[3];
                            paletteStream.ReadBytes(paletteEntry,0,3);
                            Palette.Add(Color.FromArgb(255, (paletteEntry[0] & 63) * 255 / 63, (paletteEntry[1] & 63) * 255 / 63, (paletteEntry[2] & 63) * 255 / 63));
                        }
                    }

                    // RA
                    var tileSetHeader = rawDataStream.ByteToType<TilesetHeader_RA>();

                    if (tileSetHeader.size != rawData.Length ||
                        tileSetHeader.width != 24 ||
                        tileSetHeader.height != 24 ||
                        tileSetHeader.count > 64 ||
                        tileSetHeader.imgoffset > rawData.Length)
                    {
                        // invalid file
                        return;
                    }

                       // size of one tile in pixels
                    int tileSize = tileSetHeader.width * tileSetHeader.height;

                    // size of the entire patch
                    int tilePatchWidth = tileSetHeader.width * tileSetHeader.map_width;
                    int tilePatchHeight = tileSetHeader.height * ( tileSetHeader.map_width > 1 ? tileSetHeader.map_height : tileSetHeader.count );

                    if (tilePatchWidth == 0 || tilePatchHeight == 0) return;

                    // create bitmap for the patch
                    this.tileImage = new Bitmap(tilePatchWidth, tilePatchHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    BitmapData tileImageData = this.tileImage.LockBits(new Rectangle(0, 0, this.tileImage.Width, this.tileImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                    int stride = tileImageData.Stride;

                    // go through all tiles
                    int tileX = 0, tileY = 0;
                    int xPos, yPos;

                    byte[] rawImage = new byte[tileSize];

                    for (int tileNr = 0; tileNr < tileSetHeader.count; tileNr++)
                    {
                        rawDataStream.Seek(tileSetHeader.index1 + tileNr, SeekOrigin.Begin);
                        byte tileSetIndex = (byte)rawDataStream.ReadByte();

                        // non-transparent
                        if (tileSetIndex != 0xff)
                        {
                            // seek to tile
                            rawDataStream.Seek( tileSetHeader.imgoffset + tileSetIndex * tileSize, SeekOrigin.Begin);
                            if (rawDataStream.Position + tileSize > rawDataStream.Length) break;

                            // read raw image
                            rawDataStream.ReadBytes(rawImage, 0, tileSize);

                            // translate to RGB
                            int idx = 0;

                            for (int row = 0; row < tileSetHeader.height; row++) 
                            {
                                for (int col = 0; col < tileSetHeader.width; col++)
                                {
                                    xPos = tileX + col;
                                    yPos = tileY + row;

                                    unsafe
                                    {
                                        byte* ptr = (byte*)tileImageData.Scan0;

                                        ptr[(xPos * 4) + yPos * stride] = Palette[rawImage[idx]].B;
                                        ptr[(xPos * 4) + yPos * stride + 1] = Palette[rawImage[idx]].G;
                                        ptr[(xPos * 4) + yPos * stride + 2] = Palette[rawImage[idx]].R;
                                        ptr[(xPos * 4) + yPos * stride + 3] = Palette[rawImage[idx]].A;
                                    }

                                    // translate index to RGB
                                    //tileImage.SetPixel(tileX + col, tileY + row, );

                                    idx++;
                                }
                            }
                        }
                        
                        tileX += tileSetHeader.width;
                        if (tileX >= tilePatchWidth)
                        {
                            tileX = 0;
                            tileY += tileSetHeader.height;
                        }
                    }

                    this.tileImage.UnlockBits(tileImageData);
                }
            }
        }
    }
}
