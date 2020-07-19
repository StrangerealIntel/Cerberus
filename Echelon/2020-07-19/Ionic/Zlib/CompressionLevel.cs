using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x020000CD RID: 205
	[ComVisible(true)]
	public enum CompressionLevel
	{
		// Token: 0x0400042B RID: 1067
		None,
		// Token: 0x0400042C RID: 1068
		Level0 = 0,
		// Token: 0x0400042D RID: 1069
		BestSpeed,
		// Token: 0x0400042E RID: 1070
		Level1 = 1,
		// Token: 0x0400042F RID: 1071
		Level2,
		// Token: 0x04000430 RID: 1072
		Level3,
		// Token: 0x04000431 RID: 1073
		Level4,
		// Token: 0x04000432 RID: 1074
		Level5,
		// Token: 0x04000433 RID: 1075
		Default,
		// Token: 0x04000434 RID: 1076
		Level6 = 6,
		// Token: 0x04000435 RID: 1077
		Level7,
		// Token: 0x04000436 RID: 1078
		Level8,
		// Token: 0x04000437 RID: 1079
		BestCompression,
		// Token: 0x04000438 RID: 1080
		Level9 = 9
	}
}
