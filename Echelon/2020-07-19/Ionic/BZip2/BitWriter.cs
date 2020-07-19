using System;
using System.IO;

namespace Ionic.BZip2
{
	// Token: 0x020000B7 RID: 183
	internal class BitWriter
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x00026E98 File Offset: 0x00025098
		public BitWriter(Stream s)
		{
			this.output = s;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00026EA8 File Offset: 0x000250A8
		public byte RemainingBits
		{
			get
			{
				return (byte)(this.accumulator >> 32 - this.nAccumulatedBits & 255u);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00026EC4 File Offset: 0x000250C4
		public int NumRemainingBits
		{
			get
			{
				return this.nAccumulatedBits;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00026ECC File Offset: 0x000250CC
		public int TotalBytesWrittenOut
		{
			get
			{
				return this.totalBytesWrittenOut;
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00026ED4 File Offset: 0x000250D4
		public void Reset()
		{
			this.accumulator = 0u;
			this.nAccumulatedBits = 0;
			this.totalBytesWrittenOut = 0;
			this.output.Seek(0L, SeekOrigin.Begin);
			this.output.SetLength(0L);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00026F08 File Offset: 0x00025108
		public void WriteBits(int nbits, uint value)
		{
			int i = this.nAccumulatedBits;
			uint num = this.accumulator;
			while (i >= 8)
			{
				this.output.WriteByte((byte)(num >> 24 & 255u));
				this.totalBytesWrittenOut++;
				num <<= 8;
				i -= 8;
			}
			this.accumulator = (num | value << 32 - i - nbits);
			this.nAccumulatedBits = i + nbits;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00026F78 File Offset: 0x00025178
		public void WriteByte(byte b)
		{
			this.WriteBits(8, (uint)b);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00026F84 File Offset: 0x00025184
		public void WriteInt(uint u)
		{
			this.WriteBits(8, u >> 24 & 255u);
			this.WriteBits(8, u >> 16 & 255u);
			this.WriteBits(8, u >> 8 & 255u);
			this.WriteBits(8, u & 255u);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00026FD8 File Offset: 0x000251D8
		public void Flush()
		{
			this.WriteBits(0, 0u);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00026FE4 File Offset: 0x000251E4
		public void FinishAndPad()
		{
			this.Flush();
			if (this.NumRemainingBits > 0)
			{
				byte value = (byte)(this.accumulator >> 24 & 255u);
				this.output.WriteByte(value);
				this.totalBytesWrittenOut++;
			}
		}

		// Token: 0x040002D1 RID: 721
		private uint accumulator;

		// Token: 0x040002D2 RID: 722
		private int nAccumulatedBits;

		// Token: 0x040002D3 RID: 723
		private Stream output;

		// Token: 0x040002D4 RID: 724
		private int totalBytesWrittenOut;
	}
}
