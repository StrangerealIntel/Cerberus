using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x020000A9 RID: 169
	[ComVisible(true)]
	public enum ZipEntrySource
	{
		// Token: 0x04000229 RID: 553
		None,
		// Token: 0x0400022A RID: 554
		FileSystem,
		// Token: 0x0400022B RID: 555
		Stream,
		// Token: 0x0400022C RID: 556
		ZipFile,
		// Token: 0x0400022D RID: 557
		WriteDelegate,
		// Token: 0x0400022E RID: 558
		JitStream,
		// Token: 0x0400022F RID: 559
		ZipOutputStream
	}
}
