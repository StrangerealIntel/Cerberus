using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000092 RID: 146
	[ComVisible(false)]
	public enum VMRMode : uint
	{
		// Token: 0x04000285 RID: 645
		Windowed = 1u,
		// Token: 0x04000286 RID: 646
		Windowless,
		// Token: 0x04000287 RID: 647
		Renderless = 4u
	}
}
