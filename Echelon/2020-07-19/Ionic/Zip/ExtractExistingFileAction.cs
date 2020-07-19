using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000090 RID: 144
	[ComVisible(true)]
	public enum ExtractExistingFileAction
	{
		// Token: 0x04000168 RID: 360
		Throw,
		// Token: 0x04000169 RID: 361
		OverwriteSilently,
		// Token: 0x0400016A RID: 362
		DoNotOverwrite,
		// Token: 0x0400016B RID: 363
		InvokeExtractProgressEvent
	}
}
