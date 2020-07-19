using System;

namespace ChromV1
{
	// Token: 0x02000068 RID: 104
	public class Tables8kGcmMultiplier : IGcmMultiplier
	{
		// Token: 0x06000245 RID: 581 RVA: 0x000128C0 File Offset: 0x00010AC0
		public void Init(byte[] H)
		{
			this.M[0] = new uint[16][];
			this.M[1] = new uint[16][];
			this.M[0][0] = new uint[4];
			this.M[1][0] = new uint[4];
			this.M[1][8] = GcmUtilities.AsUints(H);
			for (int i = 4; i >= 1; i >>= 1)
			{
				uint[] array = (uint[])this.M[1][i + i].Clone();
				GcmUtilities.MultiplyP(array);
				this.M[1][i] = array;
			}
			uint[] array2 = (uint[])this.M[1][1].Clone();
			GcmUtilities.MultiplyP(array2);
			this.M[0][8] = array2;
			for (int j = 4; j >= 1; j >>= 1)
			{
				uint[] array3 = (uint[])this.M[0][j + j].Clone();
				GcmUtilities.MultiplyP(array3);
				this.M[0][j] = array3;
			}
			int num = 0;
			for (;;)
			{
				for (int k = 2; k < 16; k += k)
				{
					for (int l = 1; l < k; l++)
					{
						uint[] array4 = (uint[])this.M[num][k].Clone();
						GcmUtilities.Xor(array4, this.M[num][l]);
						this.M[num][k + l] = array4;
					}
				}
				if (++num == 32)
				{
					break;
				}
				if (num > 1)
				{
					this.M[num] = new uint[16][];
					this.M[num][0] = new uint[4];
					for (int m = 8; m > 0; m >>= 1)
					{
						uint[] array5 = (uint[])this.M[num - 2][m].Clone();
						GcmUtilities.MultiplyP8(array5);
						this.M[num][m] = array5;
					}
				}
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00012B20 File Offset: 0x00010D20
		public void MultiplyH(byte[] x)
		{
			uint[] array = new uint[4];
			for (int i = 15; i >= 0; i--)
			{
				uint[] array2 = this.M[i + i][(int)(x[i] & 15)];
				array[0] ^= array2[0];
				array[1] ^= array2[1];
				array[2] ^= array2[2];
				array[3] ^= array2[3];
				array2 = this.M[i + i + 1][(x[i] & 240) >> 4];
				array[0] ^= array2[0];
				array[1] ^= array2[1];
				array[2] ^= array2[2];
				array[3] ^= array2[3];
			}
			Pack.UInt32_To_BE(array[0], x, 0);
			Pack.UInt32_To_BE(array[1], x, 4);
			Pack.UInt32_To_BE(array[2], x, 8);
			Pack.UInt32_To_BE(array[3], x, 12);
		}

		// Token: 0x040000ED RID: 237
		private readonly uint[][][] M = new uint[32][][];
	}
}
