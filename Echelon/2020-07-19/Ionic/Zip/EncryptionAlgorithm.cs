using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x0200007E RID: 126
	[ComVisible(true)]
	public enum EncryptionAlgorithm
	{
		// Token: 0x0400013E RID: 318
		None,
		// Token: 0x0400013F RID: 319
		PkzipWeak,
		// Token: 0x04000140 RID: 320
		WinZipAes128,
		// Token: 0x04000141 RID: 321
		WinZipAes256,
		// Token: 0x04000142 RID: 322
		Unsupported
	}
}
