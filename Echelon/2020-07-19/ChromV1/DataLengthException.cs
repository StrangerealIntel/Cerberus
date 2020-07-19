using System;

namespace ChromV1
{
	// Token: 0x0200005C RID: 92
	public class DataLengthException : CryptoException
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00010CD0 File Offset: 0x0000EED0
		public DataLengthException()
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00010CD8 File Offset: 0x0000EED8
		public DataLengthException(string message) : base(message)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00010CE4 File Offset: 0x0000EEE4
		public DataLengthException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
