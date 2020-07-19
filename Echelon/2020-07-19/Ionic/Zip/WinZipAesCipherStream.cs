using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace Ionic.Zip
{
	// Token: 0x020000A1 RID: 161
	internal class WinZipAesCipherStream : Stream
	{
		// Token: 0x0600036D RID: 877 RVA: 0x00018E14 File Offset: 0x00017014
		internal WinZipAesCipherStream(Stream s, WinZipAesCrypto cryptoParams, long length, CryptoMode mode) : this(s, cryptoParams, mode)
		{
			this._length = length;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00018E28 File Offset: 0x00017028
		internal WinZipAesCipherStream(Stream s, WinZipAesCrypto cryptoParams, CryptoMode mode)
		{
			this._params = cryptoParams;
			this._s = s;
			this._mode = mode;
			this._nonce = 1;
			if (this._params == null)
			{
				throw new BadPasswordException("Supply a password to use AES encryption.");
			}
			int num = this._params.KeyBytes.Length * 8;
			if (num != 256 && num != 128 && num != 192)
			{
				throw new ArgumentOutOfRangeException("keysize", "size of key must be 128, 192, or 256");
			}
			this._mac = new HMACSHA1(this._params.MacIv);
			this._aesCipher = new RijndaelManaged();
			this._aesCipher.BlockSize = 128;
			this._aesCipher.KeySize = num;
			this._aesCipher.Mode = CipherMode.ECB;
			this._aesCipher.Padding = PaddingMode.None;
			byte[] rgbIV = new byte[16];
			this._xform = this._aesCipher.CreateEncryptor(this._params.KeyBytes, rgbIV);
			if (this._mode == CryptoMode.Encrypt)
			{
				this._iobuf = new byte[2048];
				this._PendingWriteBlock = new byte[16];
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00018F7C File Offset: 0x0001717C
		private void XorInPlace(byte[] buffer, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				buffer[offset + i] = (this.counterOut[i] ^ buffer[offset + i]);
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00018FB0 File Offset: 0x000171B0
		private void WriteTransformOneBlock(byte[] buffer, int offset)
		{
			Array.Copy(BitConverter.GetBytes(this._nonce++), 0, this.counter, 0, 4);
			this._xform.TransformBlock(this.counter, 0, 16, this.counterOut, 0);
			this.XorInPlace(buffer, offset, 16);
			this._mac.TransformBlock(buffer, offset, 16, null, 0);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00019020 File Offset: 0x00017220
		private void WriteTransformBlocks(byte[] buffer, int offset, int count)
		{
			int num = offset;
			int num2 = count + offset;
			while (num < buffer.Length && num < num2)
			{
				this.WriteTransformOneBlock(buffer, num);
				num += 16;
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00019058 File Offset: 0x00017258
		private void WriteTransformFinalBlock()
		{
			if (this._pendingCount == 0)
			{
				throw new InvalidOperationException("No bytes available.");
			}
			if (this._finalBlock)
			{
				throw new InvalidOperationException("The final block has already been transformed.");
			}
			Array.Copy(BitConverter.GetBytes(this._nonce++), 0, this.counter, 0, 4);
			this.counterOut = this._xform.TransformFinalBlock(this.counter, 0, 16);
			this.XorInPlace(this._PendingWriteBlock, 0, this._pendingCount);
			this._mac.TransformFinalBlock(this._PendingWriteBlock, 0, this._pendingCount);
			this._finalBlock = true;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00019108 File Offset: 0x00017308
		private int ReadTransformOneBlock(byte[] buffer, int offset, int last)
		{
			if (this._finalBlock)
			{
				throw new NotSupportedException();
			}
			int num = last - offset;
			int num2 = (num > 16) ? 16 : num;
			Array.Copy(BitConverter.GetBytes(this._nonce++), 0, this.counter, 0, 4);
			if (num2 == num && this._length > 0L && this._totalBytesXferred + (long)last == this._length)
			{
				this._mac.TransformFinalBlock(buffer, offset, num2);
				this.counterOut = this._xform.TransformFinalBlock(this.counter, 0, 16);
				this._finalBlock = true;
			}
			else
			{
				this._mac.TransformBlock(buffer, offset, num2, null, 0);
				this._xform.TransformBlock(this.counter, 0, 16, this.counterOut, 0);
			}
			this.XorInPlace(buffer, offset, num2);
			return num2;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000191F8 File Offset: 0x000173F8
		private void ReadTransformBlocks(byte[] buffer, int offset, int count)
		{
			int num = offset;
			int num2 = count + offset;
			while (num < buffer.Length && num < num2)
			{
				int num3 = this.ReadTransformOneBlock(buffer, num, num2);
				num += num3;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00019230 File Offset: 0x00017430
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._mode == CryptoMode.Encrypt)
			{
				throw new NotSupportedException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must not be less than zero.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must not be less than zero.");
			}
			if (buffer.Length < offset + count)
			{
				throw new ArgumentException("The buffer is too small");
			}
			int count2 = count;
			if (this._totalBytesXferred >= this._length)
			{
				return 0;
			}
			long num = this._length - this._totalBytesXferred;
			if (num < (long)count)
			{
				count2 = (int)num;
			}
			int num2 = this._s.Read(buffer, offset, count2);
			this.ReadTransformBlocks(buffer, offset, count2);
			this._totalBytesXferred += (long)num2;
			return num2;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00019300 File Offset: 0x00017500
		public byte[] FinalAuthentication
		{
			get
			{
				if (!this._finalBlock)
				{
					if (this._totalBytesXferred != 0L)
					{
						throw new BadStateException("The final hash has not been computed.");
					}
					byte[] buffer = new byte[0];
					this._mac.ComputeHash(buffer);
				}
				byte[] array = new byte[10];
				Array.Copy(this._mac.Hash, 0, array, 0, 10);
				return array;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00019368 File Offset: 0x00017568
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._finalBlock)
			{
				throw new InvalidOperationException("The final block has already been transformed.");
			}
			if (this._mode == CryptoMode.Decrypt)
			{
				throw new NotSupportedException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must not be less than zero.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must not be less than zero.");
			}
			if (buffer.Length < offset + count)
			{
				throw new ArgumentException("The offset and count are too large");
			}
			if (count == 0)
			{
				return;
			}
			if (count + this._pendingCount <= 16)
			{
				Buffer.BlockCopy(buffer, offset, this._PendingWriteBlock, this._pendingCount, count);
				this._pendingCount += count;
				return;
			}
			int num = count;
			int num2 = offset;
			if (this._pendingCount != 0)
			{
				int num3 = 16 - this._pendingCount;
				if (num3 > 0)
				{
					Buffer.BlockCopy(buffer, offset, this._PendingWriteBlock, this._pendingCount, num3);
					num -= num3;
					num2 += num3;
				}
				this.WriteTransformOneBlock(this._PendingWriteBlock, 0);
				this._s.Write(this._PendingWriteBlock, 0, 16);
				this._totalBytesXferred += 16L;
				this._pendingCount = 0;
			}
			int num4 = (num - 1) / 16;
			this._pendingCount = num - num4 * 16;
			Buffer.BlockCopy(buffer, num2 + num - this._pendingCount, this._PendingWriteBlock, 0, this._pendingCount);
			num -= this._pendingCount;
			this._totalBytesXferred += (long)num;
			if (num4 > 0)
			{
				do
				{
					int num5 = this._iobuf.Length;
					if (num5 > num)
					{
						num5 = num;
					}
					Buffer.BlockCopy(buffer, num2, this._iobuf, 0, num5);
					this.WriteTransformBlocks(this._iobuf, 0, num5);
					this._s.Write(this._iobuf, 0, num5);
					num -= num5;
					num2 += num5;
				}
				while (num > 0);
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00019548 File Offset: 0x00017748
		public override void Close()
		{
			if (this._pendingCount > 0)
			{
				this.WriteTransformFinalBlock();
				this._s.Write(this._PendingWriteBlock, 0, this._pendingCount);
				this._totalBytesXferred += (long)this._pendingCount;
				this._pendingCount = 0;
			}
			this._s.Close();
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000195AC File Offset: 0x000177AC
		public override bool CanRead
		{
			get
			{
				return this._mode == CryptoMode.Decrypt;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600037A RID: 890 RVA: 0x000195C0 File Offset: 0x000177C0
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600037B RID: 891 RVA: 0x000195C4 File Offset: 0x000177C4
		public override bool CanWrite
		{
			get
			{
				return this._mode == CryptoMode.Encrypt;
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000195D0 File Offset: 0x000177D0
		public override void Flush()
		{
			this._s.Flush();
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600037D RID: 893 RVA: 0x000195E0 File Offset: 0x000177E0
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600037E RID: 894 RVA: 0x000195E8 File Offset: 0x000177E8
		// (set) Token: 0x0600037F RID: 895 RVA: 0x000195F0 File Offset: 0x000177F0
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000195F8 File Offset: 0x000177F8
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00019600 File Offset: 0x00017800
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00019608 File Offset: 0x00017808
		[Conditional("Trace")]
		private void TraceOutput(string format, params object[] varParams)
		{
			lock (this._outputLock)
			{
				int hashCode = Thread.CurrentThread.GetHashCode();
				Console.ForegroundColor = hashCode % 8 + ConsoleColor.DarkGray;
				Console.Write("{0:000} WZACS ", hashCode);
				Console.WriteLine(format, varParams);
				Console.ResetColor();
			}
		}

		// Token: 0x040001A5 RID: 421
		private const int BLOCK_SIZE_IN_BYTES = 16;

		// Token: 0x040001A6 RID: 422
		private WinZipAesCrypto _params;

		// Token: 0x040001A7 RID: 423
		private Stream _s;

		// Token: 0x040001A8 RID: 424
		private CryptoMode _mode;

		// Token: 0x040001A9 RID: 425
		private int _nonce;

		// Token: 0x040001AA RID: 426
		private bool _finalBlock;

		// Token: 0x040001AB RID: 427
		internal HMACSHA1 _mac;

		// Token: 0x040001AC RID: 428
		internal RijndaelManaged _aesCipher;

		// Token: 0x040001AD RID: 429
		internal ICryptoTransform _xform;

		// Token: 0x040001AE RID: 430
		private byte[] counter = new byte[16];

		// Token: 0x040001AF RID: 431
		private byte[] counterOut = new byte[16];

		// Token: 0x040001B0 RID: 432
		private long _length;

		// Token: 0x040001B1 RID: 433
		private long _totalBytesXferred;

		// Token: 0x040001B2 RID: 434
		private byte[] _PendingWriteBlock;

		// Token: 0x040001B3 RID: 435
		private int _pendingCount;

		// Token: 0x040001B4 RID: 436
		private byte[] _iobuf;

		// Token: 0x040001B5 RID: 437
		private object _outputLock = new object();
	}
}
