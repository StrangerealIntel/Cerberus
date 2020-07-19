using System;

namespace Ionic.Zlib
{
	// Token: 0x020000C9 RID: 201
	internal class WorkItem
	{
		// Token: 0x060006CD RID: 1741 RVA: 0x00032258 File Offset: 0x00030458
		public WorkItem(int size, CompressionLevel compressLevel, CompressionStrategy strategy, int ix)
		{
			this.buffer = new byte[size];
			int num = size + (size / 32768 + 1) * 5 * 2;
			this.compressed = new byte[num];
			this.compressor = new ZlibCodec();
			this.compressor.InitializeDeflate(compressLevel, false);
			this.compressor.OutputBuffer = this.compressed;
			this.compressor.InputBuffer = this.buffer;
			this.index = ix;
		}

		// Token: 0x040003F3 RID: 1011
		public byte[] buffer;

		// Token: 0x040003F4 RID: 1012
		public byte[] compressed;

		// Token: 0x040003F5 RID: 1013
		public int crc;

		// Token: 0x040003F6 RID: 1014
		public int index;

		// Token: 0x040003F7 RID: 1015
		public int ordinal;

		// Token: 0x040003F8 RID: 1016
		public int inputBytesAvailable;

		// Token: 0x040003F9 RID: 1017
		public int compressedBytesAvailable;

		// Token: 0x040003FA RID: 1018
		public ZlibCodec compressor;
	}
}
