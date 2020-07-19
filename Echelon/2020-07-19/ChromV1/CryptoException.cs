using System;

namespace ChromV1
{
	// Token: 0x0200005B RID: 91
	public class CryptoException : Exception
	{
		// Token: 0x060001FC RID: 508 RVA: 0x00010CB0 File Offset: 0x0000EEB0
		public CryptoException()
		{
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00010CB8 File Offset: 0x0000EEB8
		public CryptoException(string message) : base(message)
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00010CC4 File Offset: 0x0000EEC4
		public CryptoException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
