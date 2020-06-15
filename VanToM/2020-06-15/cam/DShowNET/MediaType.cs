using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000089 RID: 137
	[ComVisible(false)]
	public class MediaType
	{
		// Token: 0x04000261 RID: 609
		public static readonly Guid Video = new Guid(1935960438, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

		// Token: 0x04000262 RID: 610
		public static readonly Guid Interleaved = new Guid(1937138025, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

		// Token: 0x04000263 RID: 611
		public static readonly Guid Audio = new Guid(1935963489, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

		// Token: 0x04000264 RID: 612
		public static readonly Guid Text = new Guid(1937012852, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

		// Token: 0x04000265 RID: 613
		public static readonly Guid Stream = new Guid(3828804483u, 21071, 4558, 159, 83, 0, 32, 175, 11, 167, 112);
	}
}
