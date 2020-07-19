using System;
using System.Security.Cryptography;
using System.Text;

namespace Medo.Security.Cryptography
{
	// Token: 0x020000DF RID: 223
	public class Pbkdf2
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x0003907C File Offset: 0x0003727C
		public Pbkdf2(HMAC algorithm, byte[] password, byte[] salt, int iterations)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm", "Algorithm cannot be null.");
			}
			if (salt == null)
			{
				throw new ArgumentNullException("salt", "Salt cannot be null.");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password", "Password cannot be null.");
			}
			this.Algorithm = algorithm;
			this.Algorithm.Key = password;
			this.Salt = salt;
			this.IterationCount = iterations;
			this.BlockSize = this.Algorithm.HashSize / 8;
			this.BufferBytes = new byte[this.BlockSize];
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00039124 File Offset: 0x00037324
		public Pbkdf2(HMAC algorithm, byte[] password, byte[] salt) : this(algorithm, password, salt, 1000)
		{
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00039134 File Offset: 0x00037334
		public Pbkdf2(HMAC algorithm, string password, string salt, int iterations) : this(algorithm, Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt), iterations)
		{
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00039164 File Offset: 0x00037364
		public Pbkdf2(HMAC algorithm, string password, string salt) : this(algorithm, password, salt, 1000)
		{
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00039174 File Offset: 0x00037374
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x0003917C File Offset: 0x0003737C
		public HMAC Algorithm { get; private set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00039188 File Offset: 0x00037388
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x00039190 File Offset: 0x00037390
		public byte[] Salt { get; private set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x0003919C File Offset: 0x0003739C
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x000391A4 File Offset: 0x000373A4
		public int IterationCount { get; private set; }

		// Token: 0x060007CF RID: 1999 RVA: 0x000391B0 File Offset: 0x000373B0
		public byte[] GetBytes(int count)
		{
			byte[] array = new byte[count];
			int i = 0;
			int num = this.BufferEndIndex - this.BufferStartIndex;
			if (num > 0)
			{
				if (count < num)
				{
					Buffer.BlockCopy(this.BufferBytes, this.BufferStartIndex, array, 0, count);
					this.BufferStartIndex += count;
					return array;
				}
				Buffer.BlockCopy(this.BufferBytes, this.BufferStartIndex, array, 0, num);
				this.BufferStartIndex = (this.BufferEndIndex = 0);
				i += num;
			}
			while (i < count)
			{
				int num2 = count - i;
				this.BufferBytes = this.Func();
				if (num2 <= this.BlockSize)
				{
					Buffer.BlockCopy(this.BufferBytes, 0, array, i, num2);
					this.BufferStartIndex = num2;
					this.BufferEndIndex = this.BlockSize;
					return array;
				}
				Buffer.BlockCopy(this.BufferBytes, 0, array, i, this.BlockSize);
				i += this.BlockSize;
			}
			return array;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000392A4 File Offset: 0x000374A4
		private byte[] Func()
		{
			byte[] array = new byte[this.Salt.Length + 4];
			Buffer.BlockCopy(this.Salt, 0, array, 0, this.Salt.Length);
			Buffer.BlockCopy(Pbkdf2.GetBytesFromInt(this.BlockIndex), 0, array, this.Salt.Length, 4);
			byte[] array2 = this.Algorithm.ComputeHash(array);
			byte[] array3 = array2;
			for (int i = 2; i <= this.IterationCount; i++)
			{
				array2 = this.Algorithm.ComputeHash(array2, 0, array2.Length);
				for (int j = 0; j < this.BlockSize; j++)
				{
					array3[j] ^= array2[j];
				}
			}
			if (this.BlockIndex == 4294967295u)
			{
				throw new InvalidOperationException("Derived key too long.");
			}
			this.BlockIndex += 1u;
			return array3;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00039378 File Offset: 0x00037578
		private static byte[] GetBytesFromInt(uint i)
		{
			byte[] bytes = BitConverter.GetBytes(i);
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					bytes[3],
					bytes[2],
					bytes[1],
					bytes[0]
				};
			}
			return bytes;
		}

		// Token: 0x040004B0 RID: 1200
		private readonly int BlockSize;

		// Token: 0x040004B1 RID: 1201
		private uint BlockIndex = 1u;

		// Token: 0x040004B2 RID: 1202
		private byte[] BufferBytes;

		// Token: 0x040004B3 RID: 1203
		private int BufferStartIndex;

		// Token: 0x040004B4 RID: 1204
		private int BufferEndIndex;
	}
}
