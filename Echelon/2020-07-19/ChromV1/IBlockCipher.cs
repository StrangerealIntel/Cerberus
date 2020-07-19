using System;

namespace ChromV1
{
	// Token: 0x02000057 RID: 87
	public interface IBlockCipher
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001DB RID: 475
		string AlgorithmName { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001DC RID: 476
		bool IsPartialBlockOkay { get; }

		// Token: 0x060001DD RID: 477
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x060001DE RID: 478
		int GetBlockSize();

		// Token: 0x060001DF RID: 479
		int ProcessBlock(byte[] inBuf, int inOff, byte[] outBuf, int outOff);

		// Token: 0x060001E0 RID: 480
		void Reset();
	}
}
