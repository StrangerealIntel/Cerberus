using System;

namespace ChromV1
{
	// Token: 0x02000054 RID: 84
	public class AeadParameters : ICipherParameters
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x0000D164 File Offset: 0x0000B364
		public AeadParameters(KeyParameter key, int macSize, byte[] nonce, byte[] associatedText)
		{
			this.Key = key;
			this.nonce = nonce;
			this.MacSize = macSize;
			this.associatedText = associatedText;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000D18C File Offset: 0x0000B38C
		public virtual KeyParameter Key { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000D194 File Offset: 0x0000B394
		public virtual int MacSize { get; }

		// Token: 0x060001C8 RID: 456 RVA: 0x0000D19C File Offset: 0x0000B39C
		public virtual byte[] GetAssociatedText()
		{
			return this.associatedText;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
		public virtual byte[] GetNonce()
		{
			return this.nonce;
		}

		// Token: 0x040000B1 RID: 177
		private readonly byte[] associatedText;

		// Token: 0x040000B2 RID: 178
		private readonly byte[] nonce;
	}
}
