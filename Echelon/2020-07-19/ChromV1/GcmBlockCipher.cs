using System;
using SmartAssembly.StringsEncoding;

namespace ChromV1
{
	// Token: 0x0200005E RID: 94
	public class GcmBlockCipher : IAeadBlockCipher
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00010ECC File Offset: 0x0000F0CC
		public GcmBlockCipher(IBlockCipher c) : this(c, null)
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		public GcmBlockCipher(IBlockCipher c, IGcmMultiplier m)
		{
			if (c.GetBlockSize() != 16)
			{
				throw new ArgumentException(Strings.Get(107395085) + 16.ToString() + Strings.Get(107395576));
			}
			if (m == null)
			{
				m = new Tables8kGcmMultiplier();
			}
			this.cipher = c;
			this.multiplier = m;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00010F40 File Offset: 0x0000F140
		public virtual string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + Strings.Get(107395571);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00010F5C File Offset: 0x0000F15C
		public virtual int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00010F60 File Offset: 0x0000F160
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			this.macBlock = null;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				this.nonce = aeadParameters.GetNonce();
				this.A = aeadParameters.GetAssociatedText();
				int num = aeadParameters.MacSize;
				if (num < 96 || num > 128 || num % 8 != 0)
				{
					throw new ArgumentException(Strings.Get(107395530) + num.ToString());
				}
				this.macSize = num / 8;
				this.keyParam = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException(Strings.Get(107395489));
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				this.nonce = parametersWithIV.GetIV();
				this.A = null;
				this.macSize = 16;
				this.keyParam = (KeyParameter)parametersWithIV.Parameters;
			}
			int num2 = forEncryption ? 16 : (16 + this.macSize);
			this.bufBlock = new byte[num2];
			if (this.nonce == null || this.nonce.Length < 1)
			{
				throw new ArgumentException(Strings.Get(107395476));
			}
			if (this.A == null)
			{
				this.A = new byte[0];
			}
			this.cipher.Init(true, this.keyParam);
			this.H = new byte[16];
			this.cipher.ProcessBlock(this.H, 0, this.H, 0);
			this.multiplier.Init(this.H);
			this.initS = this.gHASH(this.A);
			if (this.nonce.Length == 12)
			{
				this.J0 = new byte[16];
				Array.Copy(this.nonce, 0, this.J0, 0, this.nonce.Length);
				this.J0[15] = 1;
			}
			else
			{
				this.J0 = this.gHASH(this.nonce);
				byte[] array = new byte[16];
				GcmBlockCipher.packLength((ulong)((long)this.nonce.Length * 8L), array, 8);
				GcmUtilities.Xor(this.J0, array);
				this.multiplier.MultiplyH(this.J0);
			}
			this.S = Arrays.Clone(this.initS);
			this.counter = Arrays.Clone(this.J0);
			this.bufOff = 0;
			this.totalLength = 0UL;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000111D4 File Offset: 0x0000F3D4
		public virtual byte[] GetMac()
		{
			return Arrays.Clone(this.macBlock);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000111E4 File Offset: 0x0000F3E4
		public virtual int GetOutputSize(int len)
		{
			if (this.forEncryption)
			{
				return len + this.bufOff + this.macSize;
			}
			return len + this.bufOff - this.macSize;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00011210 File Offset: 0x0000F410
		public virtual int GetUpdateOutputSize(int len)
		{
			return (len + this.bufOff) / 16 * 16;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00011220 File Offset: 0x0000F420
		public virtual int ProcessByte(byte input, byte[] output, int outOff)
		{
			return this.Process(input, output, outOff);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0001122C File Offset: 0x0000F42C
		public virtual int ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			int num = 0;
			for (int num2 = 0; num2 != len; num2++)
			{
				byte[] array = this.bufBlock;
				int num3 = this.bufOff;
				this.bufOff = num3 + 1;
				array[num3] = input[inOff + num2];
				if (this.bufOff == this.bufBlock.Length)
				{
					this.gCTRBlock(this.bufBlock, 16, output, outOff + num);
					if (!this.forEncryption)
					{
						Array.Copy(this.bufBlock, 16, this.bufBlock, 0, this.macSize);
					}
					this.bufOff = this.bufBlock.Length - 16;
					num += 16;
				}
			}
			return num;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000112D4 File Offset: 0x0000F4D4
		public int DoFinal(byte[] output, int outOff)
		{
			int num = this.bufOff;
			if (!this.forEncryption)
			{
				if (num < this.macSize)
				{
					throw new InvalidCipherTextException(Strings.Get(107395407));
				}
				num -= this.macSize;
			}
			if (num > 0)
			{
				byte[] array = new byte[16];
				Array.Copy(this.bufBlock, 0, array, 0, num);
				this.gCTRBlock(array, num, output, outOff);
			}
			byte[] array2 = new byte[16];
			GcmBlockCipher.packLength((ulong)((long)this.A.Length * 8L), array2, 0);
			GcmBlockCipher.packLength(this.totalLength * 8UL, array2, 8);
			GcmUtilities.Xor(this.S, array2);
			this.multiplier.MultiplyH(this.S);
			byte[] array3 = new byte[16];
			this.cipher.ProcessBlock(this.J0, 0, array3, 0);
			GcmUtilities.Xor(array3, this.S);
			int num2 = num;
			this.macBlock = new byte[this.macSize];
			Array.Copy(array3, 0, this.macBlock, 0, this.macSize);
			if (this.forEncryption)
			{
				Array.Copy(this.macBlock, 0, output, outOff + this.bufOff, this.macSize);
				num2 += this.macSize;
			}
			else
			{
				byte[] array4 = new byte[this.macSize];
				Array.Copy(this.bufBlock, num, array4, 0, this.macSize);
				if (!Arrays.ConstantTimeAreEqual(this.macBlock, array4))
				{
					throw new InvalidCipherTextException(Strings.Get(107395418));
				}
			}
			this.Reset(false);
			return num2;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00011460 File Offset: 0x0000F660
		public virtual void Reset()
		{
			this.Reset(true);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0001146C File Offset: 0x0000F66C
		private int Process(byte A_1, byte[] A_2, int A_3)
		{
			byte[] array = this.bufBlock;
			int num = this.bufOff;
			this.bufOff = num + 1;
			array[num] = A_1;
			if (this.bufOff == this.bufBlock.Length)
			{
				this.gCTRBlock(this.bufBlock, 16, A_2, A_3);
				if (!this.forEncryption)
				{
					Array.Copy(this.bufBlock, 16, this.bufBlock, 0, this.macSize);
				}
				this.bufOff = this.bufBlock.Length - 16;
				return 16;
			}
			return 0;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000114F4 File Offset: 0x0000F6F4
		private void Reset(bool A_1)
		{
			this.S = Arrays.Clone(this.initS);
			this.counter = Arrays.Clone(this.J0);
			this.bufOff = 0;
			this.totalLength = 0UL;
			if (this.bufBlock != null)
			{
				Array.Clear(this.bufBlock, 0, this.bufBlock.Length);
			}
			if (A_1)
			{
				this.macBlock = null;
			}
			this.cipher.Reset();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00011570 File Offset: 0x0000F770
		private void gCTRBlock(byte[] A_1, int A_2, byte[] A_3, int A_4)
		{
			for (int i = 15; i >= 12; i--)
			{
				byte[] array = this.counter;
				int num = i;
				byte b = array[num] + 1;
				array[num] = b;
				if (b != 0)
				{
					break;
				}
			}
			byte[] array2 = new byte[16];
			this.cipher.ProcessBlock(this.counter, 0, array2, 0);
			byte[] array3;
			if (this.forEncryption)
			{
				Array.Copy(GcmBlockCipher.Zeroes, A_2, array2, A_2, 16 - A_2);
				array3 = array2;
			}
			else
			{
				array3 = A_1;
			}
			for (int j = A_2 - 1; j >= 0; j--)
			{
				byte[] array4 = array2;
				int num2 = j;
				array4[num2] ^= A_1[j];
				A_3[A_4 + j] = array2[j];
			}
			GcmUtilities.Xor(this.S, array3);
			this.multiplier.MultiplyH(this.S);
			this.totalLength += (ulong)((long)A_2);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0001164C File Offset: 0x0000F84C
		private byte[] gHASH(byte[] A_1)
		{
			byte[] array = new byte[16];
			for (int i = 0; i < A_1.Length; i += 16)
			{
				byte[] array2 = new byte[16];
				int length = Math.Min(A_1.Length - i, 16);
				Array.Copy(A_1, i, array2, 0, length);
				GcmUtilities.Xor(array, array2);
				this.multiplier.MultiplyH(array);
			}
			return array;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000116AC File Offset: 0x0000F8AC
		private static void packLength(ulong A_0, byte[] A_1, int A_2)
		{
			Pack.UInt32_To_BE((uint)(A_0 >> 32), A_1, A_2);
			Pack.UInt32_To_BE((uint)A_0, A_1, A_2 + 4);
		}

		// Token: 0x040000D2 RID: 210
		private static readonly byte[] Zeroes = new byte[16];

		// Token: 0x040000D3 RID: 211
		private readonly IBlockCipher cipher;

		// Token: 0x040000D4 RID: 212
		private readonly IGcmMultiplier multiplier;

		// Token: 0x040000D5 RID: 213
		private byte[] A;

		// Token: 0x040000D6 RID: 214
		private byte[] bufBlock;

		// Token: 0x040000D7 RID: 215
		private int bufOff;

		// Token: 0x040000D8 RID: 216
		private byte[] counter;

		// Token: 0x040000D9 RID: 217
		private bool forEncryption;

		// Token: 0x040000DA RID: 218
		private byte[] H;

		// Token: 0x040000DB RID: 219
		private byte[] initS;

		// Token: 0x040000DC RID: 220
		private byte[] J0;

		// Token: 0x040000DD RID: 221
		private KeyParameter keyParam;

		// Token: 0x040000DE RID: 222
		private byte[] macBlock;

		// Token: 0x040000DF RID: 223
		private int macSize;

		// Token: 0x040000E0 RID: 224
		private byte[] nonce;

		// Token: 0x040000E1 RID: 225
		private byte[] S;

		// Token: 0x040000E2 RID: 226
		private ulong totalLength;
	}
}
