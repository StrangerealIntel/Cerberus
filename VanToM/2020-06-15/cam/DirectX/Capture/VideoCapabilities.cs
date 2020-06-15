using System;
using System.Drawing;
using System.Runtime.InteropServices;
using cam.DShowNET;

namespace cam.DirectX.Capture
{
	// Token: 0x020000A5 RID: 165
	public class VideoCapabilities
	{
		// Token: 0x0600037A RID: 890 RVA: 0x00014720 File Offset: 0x00012920
		internal VideoCapabilities(IAMStreamConfig videoStreamConfig)
		{
			AMMediaType ammediaType = null;
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				int num2;
				int num3;
				int num = videoStreamConfig.GetNumberOfCapabilities(ref num2, ref num3);
				intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(VideoStreamConfigCaps)));
				IntPtr ptr;
				num = videoStreamConfig.GetStreamCaps(0, out ptr, intPtr);
				ammediaType = (AMMediaType)Marshal.PtrToStructure(ptr, typeof(AMMediaType));
				VideoStreamConfigCaps videoStreamConfigCaps = (VideoStreamConfigCaps)Marshal.PtrToStructure(intPtr, typeof(VideoStreamConfigCaps));
				this.InputSize = videoStreamConfigCaps.InputSize;
				this.MinFrameSize = videoStreamConfigCaps.MinOutputSize;
				this.MaxFrameSize = videoStreamConfigCaps.MaxOutputSize;
				this.FrameSizeGranularityX = videoStreamConfigCaps.OutputGranularityX;
				this.FrameSizeGranularityY = videoStreamConfigCaps.OutputGranularityY;
				this.MinFrameRate = 10000000.0 / (double)videoStreamConfigCaps.MaxFrameInterval;
				this.MaxFrameRate = 10000000.0 / (double)videoStreamConfigCaps.MinFrameInterval;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
				intPtr = IntPtr.Zero;
				if (ammediaType != null)
				{
					DsUtils.FreeAMMediaType(ammediaType);
				}
				ammediaType = null;
			}
		}

		// Token: 0x040002B3 RID: 691
		public Size InputSize;

		// Token: 0x040002B4 RID: 692
		public Size MinFrameSize;

		// Token: 0x040002B5 RID: 693
		public Size MaxFrameSize;

		// Token: 0x040002B6 RID: 694
		public int FrameSizeGranularityX;

		// Token: 0x040002B7 RID: 695
		public int FrameSizeGranularityY;

		// Token: 0x040002B8 RID: 696
		public double MinFrameRate;

		// Token: 0x040002B9 RID: 697
		public double MaxFrameRate;
	}
}
