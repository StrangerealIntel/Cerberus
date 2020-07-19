using System;

namespace Ionic.Zlib
{
	// Token: 0x020000D2 RID: 210
	internal static class InternalConstants
	{
		// Token: 0x04000440 RID: 1088
		internal static readonly int MAX_BITS = 15;

		// Token: 0x04000441 RID: 1089
		internal static readonly int BL_CODES = 19;

		// Token: 0x04000442 RID: 1090
		internal static readonly int D_CODES = 30;

		// Token: 0x04000443 RID: 1091
		internal static readonly int LITERALS = 256;

		// Token: 0x04000444 RID: 1092
		internal static readonly int LENGTH_CODES = 29;

		// Token: 0x04000445 RID: 1093
		internal static readonly int L_CODES = InternalConstants.LITERALS + 1 + InternalConstants.LENGTH_CODES;

		// Token: 0x04000446 RID: 1094
		internal static readonly int MAX_BL_BITS = 7;

		// Token: 0x04000447 RID: 1095
		internal static readonly int REP_3_6 = 16;

		// Token: 0x04000448 RID: 1096
		internal static readonly int REPZ_3_10 = 17;

		// Token: 0x04000449 RID: 1097
		internal static readonly int REPZ_11_138 = 18;
	}
}
