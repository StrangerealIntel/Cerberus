using System;

namespace ChromV1
{
	// Token: 0x0200005F RID: 95
	public interface IAeadBlockCipher
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000217 RID: 535
		string AlgorithmName { get; }

		// Token: 0x06000218 RID: 536
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x06000219 RID: 537
		int GetBlockSize();

		// Token: 0x0600021A RID: 538
		int ProcessByte(byte input, byte[] outBytes, int outOff);

		// Token: 0x0600021B RID: 539
		int ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff);

		// Token: 0x0600021C RID: 540
		int DoFinal(byte[] outBytes, int outOff);

		// Token: 0x0600021D RID: 541
		byte[] GetMac();

		// Token: 0x0600021E RID: 542
		int GetUpdateOutputSize(int len);

		// Token: 0x0600021F RID: 543
		int GetOutputSize(int len);

		// Token: 0x06000220 RID: 544
		void Reset();
	}
}
