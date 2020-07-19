using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x020000A5 RID: 165
	internal class ZipCipherStream : Stream
	{
		// Token: 0x0600038B RID: 907 RVA: 0x00019944 File Offset: 0x00017B44
		public ZipCipherStream(Stream s, ZipCrypto cipher, CryptoMode mode)
		{
			this._cipher = cipher;
			this._s = s;
			this._mode = mode;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00019964 File Offset: 0x00017B64
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._mode == CryptoMode.Encrypt)
			{
				throw new NotSupportedException("This stream does not encrypt via Read()");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			byte[] array = new byte[count];
			int num = this._s.Read(array, 0, count);
			byte[] array2 = this._cipher.DecryptMessage(array, num);
			for (int i = 0; i < num; i++)
			{
				buffer[offset + i] = array2[i];
			}
			return num;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000199D8 File Offset: 0x00017BD8
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._mode == CryptoMode.Decrypt)
			{
				throw new NotSupportedException("This stream does not Decrypt via Write()");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count == 0)
			{
				return;
			}
			byte[] array;
			if (offset != 0)
			{
				array = new byte[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = buffer[offset + i];
				}
			}
			else
			{
				array = buffer;
			}
			byte[] array2 = this._cipher.EncryptMessage(array, count);
			this._s.Write(array2, 0, array2.Length);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00019A64 File Offset: 0x00017C64
		public override bool CanRead
		{
			get
			{
				return this._mode == CryptoMode.Decrypt;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00019A70 File Offset: 0x00017C70
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00019A74 File Offset: 0x00017C74
		public override bool CanWrite
		{
			get
			{
				return this._mode == CryptoMode.Encrypt;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00019A80 File Offset: 0x00017C80
		public override void Flush()
		{
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00019A84 File Offset: 0x00017C84
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00019A8C File Offset: 0x00017C8C
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00019A94 File Offset: 0x00017C94
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00019A9C File Offset: 0x00017C9C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00019AA4 File Offset: 0x00017CA4
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040001C8 RID: 456
		private ZipCrypto _cipher;

		// Token: 0x040001C9 RID: 457
		private Stream _s;

		// Token: 0x040001CA RID: 458
		private CryptoMode _mode;
	}
}
