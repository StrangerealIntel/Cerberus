using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000FC RID: 252
	internal abstract class MegaAesCtrStream : Stream
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x0003C180 File Offset: 0x0003A380
		protected MegaAesCtrStream(Stream stream, long streamLength, MegaAesCtrStream.Mode mode, byte[] fileKey, byte[] iv)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (fileKey == null || fileKey.Length != 16)
			{
				throw new ArgumentException("Invalid fileKey");
			}
			if (iv == null || iv.Length != 8)
			{
				throw new ArgumentException("Invalid Iv");
			}
			this.stream = stream;
			this.streamLength = streamLength;
			this.mode = mode;
			this.fileKey = fileKey;
			this.iv = iv;
			this.ChunksPositions = this.GetChunksPositions(this.streamLength).ToArray<long>();
			this.chunksPositionsCache = new HashSet<long>(this.ChunksPositions);
			this.encryptor = Crypto.CreateAesEncryptor(this.fileKey);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0003C274 File Offset: 0x0003A474
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.encryptor.Dispose();
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0003C288 File Offset: 0x0003A488
		public long[] ChunksPositions { get; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0003C290 File Offset: 0x0003A490
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0003C294 File Offset: 0x0003A494
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0003C298 File Offset: 0x0003A498
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x0003C29C File Offset: 0x0003A49C
		public override long Length
		{
			get
			{
				return this.streamLength;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0003C2A4 File Offset: 0x0003A4A4
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0003C2AC File Offset: 0x0003A4AC
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				if (this.position != value)
				{
					throw new NotSupportedException("Seek is not supported");
				}
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0003C2C8 File Offset: 0x0003A4C8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.position == this.streamLength)
			{
				return 0;
			}
			for (long num = this.position; num < Math.Min(this.position + (long)count, this.streamLength); num += 16L)
			{
				if (this.chunksPositionsCache.Contains(num))
				{
					if (num != 0L)
					{
						this.ComputeChunk(this.encryptor);
					}
					for (int i = 0; i < 8; i++)
					{
						this.currentChunkMac[i] = this.iv[i];
						this.currentChunkMac[i + 8] = this.iv[i];
					}
				}
				this.IncrementCounter();
				byte[] array = new byte[16];
				byte[] array2 = new byte[array.Length];
				int num2 = this.stream.Read(array, 0, array.Length);
				if (num2 != array.Length)
				{
					num2 += this.stream.Read(array, num2, array.Length - num2);
				}
				byte[] array3 = new byte[16];
				Array.Copy(this.iv, array3, 8);
				Array.Copy(this.counter, 0, array3, 8, 8);
				byte[] array4 = Crypto.EncryptAes(array3, this.encryptor);
				for (int j = 0; j < num2; j++)
				{
					array2[j] = (array4[j] ^ array[j]);
					byte[] array5 = this.currentChunkMac;
					int num3 = j;
					array5[num3] ^= ((this.mode == MegaAesCtrStream.Mode.Crypt) ? array[j] : array2[j]);
				}
				Array.Copy(array2, 0, buffer, (int)((long)offset + num - this.position), (int)Math.Min((long)array2.Length, this.streamLength - num));
				this.currentChunkMac = Crypto.EncryptAes(this.currentChunkMac, this.encryptor);
			}
			long num4 = Math.Min((long)count, this.streamLength - this.position);
			this.position += num4;
			if (this.position == this.streamLength)
			{
				this.ComputeChunk(this.encryptor);
				for (int k = 0; k < 4; k++)
				{
					this.metaMac[k] = (this.fileMac[k] ^ this.fileMac[k + 4]);
					this.metaMac[k + 4] = (this.fileMac[k + 8] ^ this.fileMac[k + 12]);
				}
				this.OnStreamRead();
			}
			return (int)num4;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0003C51C File Offset: 0x0003A71C
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0003C524 File Offset: 0x0003A724
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0003C52C File Offset: 0x0003A72C
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0003C534 File Offset: 0x0003A734
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0003C53C File Offset: 0x0003A73C
		protected virtual void OnStreamRead()
		{
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0003C540 File Offset: 0x0003A740
		private void IncrementCounter()
		{
			if ((this.currentCounter & 255L) != 255L && (this.currentCounter & 255L) != 0L)
			{
				byte[] array = this.counter;
				int num = 7;
				array[num] += 1;
			}
			else
			{
				byte[] bytes = BitConverter.GetBytes(this.currentCounter);
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(bytes);
				}
				Array.Copy(bytes, this.counter, 8);
			}
			this.currentCounter += 1L;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0003C5CC File Offset: 0x0003A7CC
		private void ComputeChunk(ICryptoTransform encryptor)
		{
			for (int i = 0; i < 16; i++)
			{
				byte[] array = this.fileMac;
				int num = i;
				array[num] ^= this.currentChunkMac[i];
			}
			this.fileMac = Crypto.EncryptAes(this.fileMac, encryptor);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0003C618 File Offset: 0x0003A818
		private IEnumerable<long> GetChunksPositions(long size)
		{
			yield return 0L;
			long chunkStartPosition = 0L;
			int num;
			for (int idx = 1; idx <= 8; idx = num + 1)
			{
				if (chunkStartPosition >= size - (long)(idx * 131072))
				{
					break;
				}
				chunkStartPosition += (long)(idx * 131072);
				yield return chunkStartPosition;
				num = idx;
			}
			while (chunkStartPosition + 1048576L < size)
			{
				chunkStartPosition += 1048576L;
				yield return chunkStartPosition;
			}
			yield break;
		}

		// Token: 0x04000525 RID: 1317
		protected readonly byte[] fileKey;

		// Token: 0x04000526 RID: 1318
		protected readonly byte[] iv;

		// Token: 0x04000527 RID: 1319
		protected readonly long streamLength;

		// Token: 0x04000528 RID: 1320
		protected long position;

		// Token: 0x04000529 RID: 1321
		protected byte[] metaMac = new byte[8];

		// Token: 0x0400052A RID: 1322
		private readonly Stream stream;

		// Token: 0x0400052B RID: 1323
		private readonly MegaAesCtrStream.Mode mode;

		// Token: 0x0400052C RID: 1324
		private readonly HashSet<long> chunksPositionsCache;

		// Token: 0x0400052D RID: 1325
		private readonly byte[] counter = new byte[8];

		// Token: 0x0400052E RID: 1326
		private readonly ICryptoTransform encryptor;

		// Token: 0x0400052F RID: 1327
		private long currentCounter;

		// Token: 0x04000530 RID: 1328
		private byte[] currentChunkMac = new byte[16];

		// Token: 0x04000531 RID: 1329
		private byte[] fileMac = new byte[16];

		// Token: 0x0200029F RID: 671
		protected enum Mode
		{
			// Token: 0x04000B74 RID: 2932
			Crypt,
			// Token: 0x04000B75 RID: 2933
			Decrypt
		}
	}
}
