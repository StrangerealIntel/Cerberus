using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000071 RID: 113
	[ComVisible(false)]
	[Flags]
	public enum AMTunerModeType
	{
		// Token: 0x040001F6 RID: 502
		Default = 0,
		// Token: 0x040001F7 RID: 503
		TV = 1,
		// Token: 0x040001F8 RID: 504
		FMRadio = 2,
		// Token: 0x040001F9 RID: 505
		AMRadio = 4,
		// Token: 0x040001FA RID: 506
		Dss = 8
	}
}
