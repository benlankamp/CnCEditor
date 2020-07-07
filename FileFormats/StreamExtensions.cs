using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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

		public static T ByteToType<T>(this MemoryStream stream)
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
}
