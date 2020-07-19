using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x020000AA RID: 170
	[ComVisible(true)]
	public enum ZipErrorAction
	{
		// Token: 0x04000231 RID: 561
		Throw,
		// Token: 0x04000232 RID: 562
		Skip,
		// Token: 0x04000233 RID: 563
		Retry,
		// Token: 0x04000234 RID: 564
		InvokeErrorEvent
	}
}
