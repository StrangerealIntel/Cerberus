using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000081 RID: 129
	[ComVisible(false)]
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public struct BitmapInfoHeader
	{
		// Token: 0x04000240 RID: 576
		public int Size;

		// Token: 0x04000241 RID: 577
		public int Width;

		// Token: 0x04000242 RID: 578
		public int Height;

		// Token: 0x04000243 RID: 579
		public short Planes;

		// Token: 0x04000244 RID: 580
		public short BitCount;

		// Token: 0x04000245 RID: 581
		public int Compression;

		// Token: 0x04000246 RID: 582
		public int ImageSize;

		// Token: 0x04000247 RID: 583
		public int XPelsPerMeter;

		// Token: 0x04000248 RID: 584
		public int YPelsPerMeter;

		// Token: 0x04000249 RID: 585
		public int ClrUsed;

		// Token: 0x0400024A RID: 586
		public int ClrImportant;
	}
}
