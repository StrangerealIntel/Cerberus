using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200007C RID: 124
	[ComVisible(false)]
	[StructLayout(LayoutKind.Sequential)]
	public class VideoStreamConfigCaps
	{
		// Token: 0x0400021B RID: 539
		public Guid Guid;

		// Token: 0x0400021C RID: 540
		public AnalogVideoStandard VideoStandard;

		// Token: 0x0400021D RID: 541
		public Size InputSize;

		// Token: 0x0400021E RID: 542
		public Size MinCroppingSize;

		// Token: 0x0400021F RID: 543
		public Size MaxCroppingSize;

		// Token: 0x04000220 RID: 544
		public int CropGranularityX;

		// Token: 0x04000221 RID: 545
		public int CropGranularityY;

		// Token: 0x04000222 RID: 546
		public int CropAlignX;

		// Token: 0x04000223 RID: 547
		public int CropAlignY;

		// Token: 0x04000224 RID: 548
		public Size MinOutputSize;

		// Token: 0x04000225 RID: 549
		public Size MaxOutputSize;

		// Token: 0x04000226 RID: 550
		public int OutputGranularityX;

		// Token: 0x04000227 RID: 551
		public int OutputGranularityY;

		// Token: 0x04000228 RID: 552
		public int StretchTapsX;

		// Token: 0x04000229 RID: 553
		public int StretchTapsY;

		// Token: 0x0400022A RID: 554
		public int ShrinkTapsX;

		// Token: 0x0400022B RID: 555
		public int ShrinkTapsY;

		// Token: 0x0400022C RID: 556
		public long MinFrameInterval;

		// Token: 0x0400022D RID: 557
		public long MaxFrameInterval;

		// Token: 0x0400022E RID: 558
		public int MinBitsPerSecond;

		// Token: 0x0400022F RID: 559
		public int MaxBitsPerSecond;
	}
}
