using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000087 RID: 135
	[ComVisible(false)]
	public class FilterCategory
	{
		// Token: 0x04000250 RID: 592
		public static readonly Guid AudioInputDevice = new Guid(869902178u, 37064, 4560, 189, 67, 0, 160, 201, 17, 206, 134);

		// Token: 0x04000251 RID: 593
		public static readonly Guid VideoInputDevice = new Guid(2248913680u, 23809, 4560, 189, 59, 0, 160, 201, 17, 206, 134);

		// Token: 0x04000252 RID: 594
		public static readonly Guid VideoCompressorCategory = new Guid(869902176u, 37064, 4560, 189, 67, 0, 160, 201, 17, 206, 134);

		// Token: 0x04000253 RID: 595
		public static readonly Guid AudioCompressorCategory = new Guid(869902177u, 37064, 4560, 189, 67, 0, 160, 201, 17, 206, 134);

		// Token: 0x04000254 RID: 596
		public static readonly Guid LegacyAmFilterCategory = new Guid(137913329, 28894, 4560, 189, 64, 0, 160, 201, 17, 206, 134);
	}
}
