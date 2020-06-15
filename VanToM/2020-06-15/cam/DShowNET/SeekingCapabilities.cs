using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200005C RID: 92
	[ComVisible(false)]
	[Flags]
	public enum SeekingCapabilities
	{
		// Token: 0x040001CD RID: 461
		CanSeekAbsolute = 1,
		// Token: 0x040001CE RID: 462
		CanSeekForwards = 2,
		// Token: 0x040001CF RID: 463
		CanSeekBackwards = 4,
		// Token: 0x040001D0 RID: 464
		CanGetCurrentPos = 8,
		// Token: 0x040001D1 RID: 465
		CanGetStopPos = 16,
		// Token: 0x040001D2 RID: 466
		CanGetDuration = 32,
		// Token: 0x040001D3 RID: 467
		CanPlayBackwards = 64,
		// Token: 0x040001D4 RID: 468
		CanDoSegments = 128,
		// Token: 0x040001D5 RID: 469
		Source = 256
	}
}
