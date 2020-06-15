using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200009F RID: 159
	[ComVisible(false)]
	[StructLayout(LayoutKind.Sequential)]
	public class WaveFormatEx
	{
		// Token: 0x040002A6 RID: 678
		public short wFormatTag;

		// Token: 0x040002A7 RID: 679
		public short nChannels;

		// Token: 0x040002A8 RID: 680
		public int nSamplesPerSec;

		// Token: 0x040002A9 RID: 681
		public int nAvgBytesPerSec;

		// Token: 0x040002AA RID: 682
		public short nBlockAlign;

		// Token: 0x040002AB RID: 683
		public short wBitsPerSample;

		// Token: 0x040002AC RID: 684
		public short cbSize;
	}
}
