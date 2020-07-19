using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x020000A7 RID: 167
	[Flags]
	[ComVisible(true)]
	public enum ZipEntryTimestamp
	{
		// Token: 0x0400021F RID: 543
		None = 0,
		// Token: 0x04000220 RID: 544
		DOS = 1,
		// Token: 0x04000221 RID: 545
		Windows = 2,
		// Token: 0x04000222 RID: 546
		Unix = 4,
		// Token: 0x04000223 RID: 547
		InfoZip1 = 8
	}
}
