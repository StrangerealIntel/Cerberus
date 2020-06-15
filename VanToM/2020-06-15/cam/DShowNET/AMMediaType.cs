using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000061 RID: 97
	[ComVisible(false)]
	[StructLayout(LayoutKind.Sequential)]
	public class AMMediaType
	{
		// Token: 0x040001E0 RID: 480
		public Guid majorType;

		// Token: 0x040001E1 RID: 481
		public Guid subType;

		// Token: 0x040001E2 RID: 482
		[MarshalAs(UnmanagedType.Bool)]
		public bool fixedSizeSamples;

		// Token: 0x040001E3 RID: 483
		[MarshalAs(UnmanagedType.Bool)]
		public bool temporalCompression;

		// Token: 0x040001E4 RID: 484
		public int sampleSize;

		// Token: 0x040001E5 RID: 485
		public Guid formatType;

		// Token: 0x040001E6 RID: 486
		public IntPtr unkPtr;

		// Token: 0x040001E7 RID: 487
		public int formatSize;

		// Token: 0x040001E8 RID: 488
		public IntPtr formatPtr;
	}
}
