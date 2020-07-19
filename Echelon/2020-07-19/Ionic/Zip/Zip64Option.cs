using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x020000AC RID: 172
	[ComVisible(true)]
	public enum Zip64Option
	{
		// Token: 0x04000275 RID: 629
		Default,
		// Token: 0x04000276 RID: 630
		Never = 0,
		// Token: 0x04000277 RID: 631
		AsNecessary,
		// Token: 0x04000278 RID: 632
		Always
	}
}
