using System;

namespace ChromV1
{
	// Token: 0x02000062 RID: 98
	public class InvalidCipherTextException : CryptoException
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00011860 File Offset: 0x0000FA60
		public InvalidCipherTextException()
		{
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00011868 File Offset: 0x0000FA68
		public InvalidCipherTextException(string message) : base(message)
		{
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00011874 File Offset: 0x0000FA74
		public InvalidCipherTextException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
