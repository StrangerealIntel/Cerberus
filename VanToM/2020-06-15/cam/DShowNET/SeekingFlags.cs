using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200005D RID: 93
	[ComVisible(false)]
	[Flags]
	public enum SeekingFlags
	{
		// Token: 0x040001D7 RID: 471
		NoPositioning = 0,
		// Token: 0x040001D8 RID: 472
		AbsolutePositioning = 1,
		// Token: 0x040001D9 RID: 473
		RelativePositioning = 2,
		// Token: 0x040001DA RID: 474
		IncrementalPositioning = 3,
		// Token: 0x040001DB RID: 475
		PositioningBitsMask = 3,
		// Token: 0x040001DC RID: 476
		SeekToKeyFrame = 4,
		// Token: 0x040001DD RID: 477
		ReturnTime = 8,
		// Token: 0x040001DE RID: 478
		Segment = 16,
		// Token: 0x040001DF RID: 479
		NoFlush = 32
	}
}
