using CnCEditor.Encryption;
using System;
using System.IO;
using System.Text;

namespace CnCEditor.FileFormats
{
	public enum HashType { Classic, CRC32 }

	public partial class MIXFile
	{
		/*
		 * Copyright 2007-2020 The OpenRA Developers (see AUTHORS)
		 * This file is part of OpenRA, which is free software. It is made
		 * available to you under the terms of the GNU General Public License
		 * as published by the Free Software Foundation, either version 3 of
		 * the License, or (at your option) any later version. For more
		 * information, see COPYING.
		 */
		public static uint HashFilename(string name, HashType type)
		{
			switch (type)
			{
				case HashType.Classic:
					{
						name = name.ToUpperInvariant();
						if (name.Length % 4 != 0)
							name = name.PadRight(name.Length + (4 - name.Length % 4), '\0');

						using (var ms = new MemoryStream(Encoding.ASCII.GetBytes(name)))
						{
							var len = name.Length >> 2;
							uint result = 0;

							while (len-- != 0)
								result = ((result << 1) | (result >> 31)) + ms.ReadUInt32();

							return result;
						}
					}

				case HashType.CRC32:
					{
						name = name.ToUpperInvariant();
						var l = name.Length;
						var a = l >> 2;
						if ((l & 3) != 0)
						{
							name += (char)(l - (a << 2));
							var i = 3 - (l & 3);
							while (i-- != 0)
								name += name[a << 2];
						}

						return CRC32.Calculate(Encoding.ASCII.GetBytes(name));
					}

				default: throw new NotImplementedException("Unknown hash type `{type}`");
			}
		}
	}
}
