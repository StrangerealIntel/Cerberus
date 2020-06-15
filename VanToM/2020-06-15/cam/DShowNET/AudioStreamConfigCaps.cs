using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200007D RID: 125
	[ComVisible(false)]
	[StructLayout(LayoutKind.Sequential)]
	public class AudioStreamConfigCaps
	{
		// Token: 0x04000230 RID: 560
		public Guid Guid;

		// Token: 0x04000231 RID: 561
		public int MinimumChannels;

		// Token: 0x04000232 RID: 562
		public int MaximumChannels;

		// Token: 0x04000233 RID: 563
		public int ChannelsGranularity;

		// Token: 0x04000234 RID: 564
		public int MinimumBitsPerSample;

		// Token: 0x04000235 RID: 565
		public int MaximumBitsPerSample;

		// Token: 0x04000236 RID: 566
		public int BitsPerSampleGranularity;

		// Token: 0x04000237 RID: 567
		public int MinimumSampleFrequency;

		// Token: 0x04000238 RID: 568
		public int MaximumSampleFrequency;

		// Token: 0x04000239 RID: 569
		public int SampleFrequencyGranularity;
	}
}
