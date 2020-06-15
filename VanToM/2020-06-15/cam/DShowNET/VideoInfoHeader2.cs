using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200009E RID: 158
	[ComVisible(false)]
	[StructLayout(LayoutKind.Sequential)]
	public class VideoInfoHeader2
	{
		// Token: 0x0400029A RID: 666
		public DsRECT SrcRect;

		// Token: 0x0400029B RID: 667
		public DsRECT TargetRect;

		// Token: 0x0400029C RID: 668
		public int BitRate;

		// Token: 0x0400029D RID: 669
		public int BitErrorRate;

		// Token: 0x0400029E RID: 670
		public long AvgTimePerFrame;

		// Token: 0x0400029F RID: 671
		public int InterlaceFlags;

		// Token: 0x040002A0 RID: 672
		public int CopyProtectFlags;

		// Token: 0x040002A1 RID: 673
		public int PictAspectRatioX;

		// Token: 0x040002A2 RID: 674
		public int PictAspectRatioY;

		// Token: 0x040002A3 RID: 675
		public int ControlFlags;

		// Token: 0x040002A4 RID: 676
		public int Reserved2;

		// Token: 0x040002A5 RID: 677
		public BitmapInfoHeader BmiHeader;
	}
}
