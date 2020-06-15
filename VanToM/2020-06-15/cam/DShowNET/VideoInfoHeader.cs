using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200009D RID: 157
	[ComVisible(false)]
	[StructLayout(LayoutKind.Sequential)]
	public class VideoInfoHeader
	{
		// Token: 0x04000294 RID: 660
		public DsRECT SrcRect;

		// Token: 0x04000295 RID: 661
		public DsRECT TargetRect;

		// Token: 0x04000296 RID: 662
		public int BitRate;

		// Token: 0x04000297 RID: 663
		public int BitErrorRate;

		// Token: 0x04000298 RID: 664
		public long AvgTimePerFrame;

		// Token: 0x04000299 RID: 665
		public BitmapInfoHeader BmiHeader;
	}
}
