using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ionic.Crc
{
	// Token: 0x020000DA RID: 218
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000C")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class CRC32
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00034E74 File Offset: 0x00033074
		public long TotalBytesRead
		{
			get
			{
				return this._TotalBytesRead;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00034E7C File Offset: 0x0003307C
		public int Crc32Result
		{
			get
			{
				return (int)(~(int)this._register);
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00034E88 File Offset: 0x00033088
		public int GetCrc32(Stream input)
		{
			return this.GetCrc32AndCopy(input, null);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00034E94 File Offset: 0x00033094
		public int GetCrc32AndCopy(Stream input, Stream output)
		{
			if (input == null)
			{
				throw new Exception("The input stream must not be null.");
			}
			byte[] array = new byte[8192];
			int count = 8192;
			this._TotalBytesRead = 0L;
			int i = input.Read(array, 0, count);
			if (output != null)
			{
				output.Write(array, 0, i);
			}
			this._TotalBytesRead += (long)i;
			while (i > 0)
			{
				this.SlurpBlock(array, 0, i);
				i = input.Read(array, 0, count);
				if (output != null)
				{
					output.Write(array, 0, i);
				}
				this._TotalBytesRead += (long)i;
			}
			return (int)(~(int)this._register);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00034F38 File Offset: 0x00033138
		public int ComputeCrc32(int W, byte B)
		{
			return this._InternalComputeCrc32((uint)W, B);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00034F44 File Offset: 0x00033144
		internal int _InternalComputeCrc32(uint W, byte B)
		{
			return (int)(this.crc32Table[(int)((UIntPtr)((W ^ (uint)B) & 255u))] ^ W >> 8);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00034F5C File Offset: 0x0003315C
		public void SlurpBlock(byte[] block, int offset, int count)
		{
			if (block == null)
			{
				throw new Exception("The data buffer must not be null.");
			}
			for (int i = 0; i < count; i++)
			{
				int num = offset + i;
				byte b = block[num];
				if (this.reverseBits)
				{
					uint num2 = this._register >> 24 ^ (uint)b;
					this._register = (this._register << 8 ^ this.crc32Table[(int)((UIntPtr)num2)]);
				}
				else
				{
					uint num3 = (this._register & 255u) ^ (uint)b;
					this._register = (this._register >> 8 ^ this.crc32Table[(int)((UIntPtr)num3)]);
				}
			}
			this._TotalBytesRead += (long)count;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00035004 File Offset: 0x00033204
		public void UpdateCRC(byte b)
		{
			if (this.reverseBits)
			{
				uint num = this._register >> 24 ^ (uint)b;
				this._register = (this._register << 8 ^ this.crc32Table[(int)((UIntPtr)num)]);
				return;
			}
			uint num2 = (this._register & 255u) ^ (uint)b;
			this._register = (this._register >> 8 ^ this.crc32Table[(int)((UIntPtr)num2)]);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0003506C File Offset: 0x0003326C
		public void UpdateCRC(byte b, int n)
		{
			while (n-- > 0)
			{
				if (this.reverseBits)
				{
					uint num = this._register >> 24 ^ (uint)b;
					this._register = (this._register << 8 ^ this.crc32Table[(int)((UIntPtr)((num >= 0u) ? num : (num + 256u)))]);
				}
				else
				{
					uint num2 = (this._register & 255u) ^ (uint)b;
					this._register = (this._register >> 8 ^ this.crc32Table[(int)((UIntPtr)((num2 >= 0u) ? num2 : (num2 + 256u)))]);
				}
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00035110 File Offset: 0x00033310
		private static uint ReverseBits(uint data)
		{
			uint num = (data & 1431655765u) << 1 | (data >> 1 & 1431655765u);
			num = ((num & 858993459u) << 2 | (num >> 2 & 858993459u));
			num = ((num & 252645135u) << 4 | (num >> 4 & 252645135u));
			return num << 24 | (num & 65280u) << 8 | (num >> 8 & 65280u) | num >> 24;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00035180 File Offset: 0x00033380
		private static byte ReverseBits(byte data)
		{
			uint num = (uint)data * 131586u;
			uint num2 = 17055760u;
			uint num3 = num & num2;
			uint num4 = num << 2 & num2 << 1;
			return (byte)(16781313u * (num3 + num4) >> 24);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000351B8 File Offset: 0x000333B8
		private void GenerateLookupTable()
		{
			this.crc32Table = new uint[256];
			byte b = 0;
			do
			{
				uint num = (uint)b;
				for (byte b2 = 8; b2 > 0; b2 -= 1)
				{
					if ((num & 1u) == 1u)
					{
						num = (num >> 1 ^ this.dwPolynomial);
					}
					else
					{
						num >>= 1;
					}
				}
				if (this.reverseBits)
				{
					this.crc32Table[(int)CRC32.ReverseBits(b)] = CRC32.ReverseBits(num);
				}
				else
				{
					this.crc32Table[(int)b] = num;
				}
				b += 1;
			}
			while (b != 0);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00035240 File Offset: 0x00033440
		private uint gf2_matrix_times(uint[] matrix, uint vec)
		{
			uint num = 0u;
			int num2 = 0;
			while (vec != 0u)
			{
				if ((vec & 1u) == 1u)
				{
					num ^= matrix[num2];
				}
				vec >>= 1;
				num2++;
			}
			return num;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00035278 File Offset: 0x00033478
		private void gf2_matrix_square(uint[] square, uint[] mat)
		{
			for (int i = 0; i < 32; i++)
			{
				square[i] = this.gf2_matrix_times(mat, mat[i]);
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000352A8 File Offset: 0x000334A8
		public void Combine(int crc, int length)
		{
			uint[] array = new uint[32];
			uint[] array2 = new uint[32];
			if (length == 0)
			{
				return;
			}
			uint num = ~this._register;
			array2[0] = this.dwPolynomial;
			uint num2 = 1u;
			for (int i = 1; i < 32; i++)
			{
				array2[i] = num2;
				num2 <<= 1;
			}
			this.gf2_matrix_square(array, array2);
			this.gf2_matrix_square(array2, array);
			uint num3 = (uint)length;
			do
			{
				this.gf2_matrix_square(array, array2);
				if ((num3 & 1u) == 1u)
				{
					num = this.gf2_matrix_times(array, num);
				}
				num3 >>= 1;
				if (num3 == 0u)
				{
					break;
				}
				this.gf2_matrix_square(array2, array);
				if ((num3 & 1u) == 1u)
				{
					num = this.gf2_matrix_times(array2, num);
				}
				num3 >>= 1;
			}
			while (num3 != 0u);
			num ^= (uint)crc;
			this._register = ~num;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00035374 File Offset: 0x00033574
		public CRC32() : this(false)
		{
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00035380 File Offset: 0x00033580
		public CRC32(bool reverseBits) : this(-306674912, reverseBits)
		{
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00035390 File Offset: 0x00033590
		public CRC32(int polynomial, bool reverseBits)
		{
			this.reverseBits = reverseBits;
			this.dwPolynomial = (uint)polynomial;
			this.GenerateLookupTable();
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000353B4 File Offset: 0x000335B4
		public void Reset()
		{
			this._register = uint.MaxValue;
		}

		// Token: 0x04000487 RID: 1159
		private const int BUFFER_SIZE = 8192;

		// Token: 0x04000488 RID: 1160
		private uint dwPolynomial;

		// Token: 0x04000489 RID: 1161
		private long _TotalBytesRead;

		// Token: 0x0400048A RID: 1162
		private bool reverseBits;

		// Token: 0x0400048B RID: 1163
		private uint[] crc32Table;

		// Token: 0x0400048C RID: 1164
		private uint _register = uint.MaxValue;
	}
}
