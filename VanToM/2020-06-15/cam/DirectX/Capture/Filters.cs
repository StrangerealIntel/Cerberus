using System;
using cam.DShowNET;

namespace cam.DirectX.Capture
{
	// Token: 0x02000098 RID: 152
	public class Filters
	{
		// Token: 0x06000343 RID: 835 RVA: 0x00013B14 File Offset: 0x00011D14
		public Filters()
		{
			this.VideoInputDevices = new FilterCollection(FilterCategory.VideoInputDevice);
			this.AudioInputDevices = new FilterCollection(FilterCategory.AudioInputDevice);
			this.VideoCompressors = new FilterCollection(FilterCategory.VideoCompressorCategory);
			this.AudioCompressors = new FilterCollection(FilterCategory.AudioCompressorCategory);
		}

		// Token: 0x0400028E RID: 654
		public FilterCollection VideoInputDevices;

		// Token: 0x0400028F RID: 655
		public FilterCollection AudioInputDevices;

		// Token: 0x04000290 RID: 656
		public FilterCollection VideoCompressors;

		// Token: 0x04000291 RID: 657
		public FilterCollection AudioCompressors;
	}
}
