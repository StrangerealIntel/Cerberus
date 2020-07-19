using System;
using System.IO;
using System.Security.Cryptography;

namespace Ionic.Zip
{
	// Token: 0x020000A0 RID: 160
	internal class WinZipAesCrypto
	{
		// Token: 0x06000360 RID: 864 RVA: 0x00018B14 File Offset: 0x00016D14
		private WinZipAesCrypto(string password, int KeyStrengthInBits)
		{
			this._Password = password;
			this._KeyStrengthInBits = KeyStrengthInBits;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00018B38 File Offset: 0x00016D38
		public static WinZipAesCrypto Generate(string password, int KeyStrengthInBits)
		{
			WinZipAesCrypto winZipAesCrypto = new WinZipAesCrypto(password, KeyStrengthInBits);
			int num = winZipAesCrypto._KeyStrengthInBytes / 2;
			winZipAesCrypto._Salt = new byte[num];
			Random random = new Random();
			random.NextBytes(winZipAesCrypto._Salt);
			return winZipAesCrypto;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00018B7C File Offset: 0x00016D7C
		public static WinZipAesCrypto ReadFromStream(string password, int KeyStrengthInBits, Stream s)
		{
			WinZipAesCrypto winZipAesCrypto = new WinZipAesCrypto(password, KeyStrengthInBits);
			int num = winZipAesCrypto._KeyStrengthInBytes / 2;
			winZipAesCrypto._Salt = new byte[num];
			winZipAesCrypto._providedPv = new byte[2];
			s.Read(winZipAesCrypto._Salt, 0, winZipAesCrypto._Salt.Length);
			s.Read(winZipAesCrypto._providedPv, 0, winZipAesCrypto._providedPv.Length);
			winZipAesCrypto.PasswordVerificationStored = (short)((int)winZipAesCrypto._providedPv[0] + (int)winZipAesCrypto._providedPv[1] * 256);
			if (password != null)
			{
				winZipAesCrypto.PasswordVerificationGenerated = (short)((int)winZipAesCrypto.GeneratedPV[0] + (int)winZipAesCrypto.GeneratedPV[1] * 256);
				if (winZipAesCrypto.PasswordVerificationGenerated != winZipAesCrypto.PasswordVerificationStored)
				{
					throw new BadPasswordException("bad password");
				}
			}
			return winZipAesCrypto;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00018C44 File Offset: 0x00016E44
		public byte[] GeneratedPV
		{
			get
			{
				if (!this._cryptoGenerated)
				{
					this._GenerateCryptoBytes();
				}
				return this._generatedPv;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00018C60 File Offset: 0x00016E60
		public byte[] Salt
		{
			get
			{
				return this._Salt;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00018C68 File Offset: 0x00016E68
		private int _KeyStrengthInBytes
		{
			get
			{
				return this._KeyStrengthInBits / 8;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00018C74 File Offset: 0x00016E74
		public int SizeOfEncryptionMetadata
		{
			get
			{
				return this._KeyStrengthInBytes / 2 + 10 + 2;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00018CDC File Offset: 0x00016EDC
		// (set) Token: 0x06000367 RID: 871 RVA: 0x00018C84 File Offset: 0x00016E84
		public string Password
		{
			private get
			{
				return this._Password;
			}
			set
			{
				this._Password = value;
				if (this._Password != null)
				{
					this.PasswordVerificationGenerated = (short)((int)this.GeneratedPV[0] + (int)this.GeneratedPV[1] * 256);
					if (this.PasswordVerificationGenerated != this.PasswordVerificationStored)
					{
						throw new BadPasswordException();
					}
				}
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00018CE4 File Offset: 0x00016EE4
		private void _GenerateCryptoBytes()
		{
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(this._Password, this.Salt, this.Rfc2898KeygenIterations);
			this._keyBytes = rfc2898DeriveBytes.GetBytes(this._KeyStrengthInBytes);
			this._MacInitializationVector = rfc2898DeriveBytes.GetBytes(this._KeyStrengthInBytes);
			this._generatedPv = rfc2898DeriveBytes.GetBytes(2);
			this._cryptoGenerated = true;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00018D48 File Offset: 0x00016F48
		public byte[] KeyBytes
		{
			get
			{
				if (!this._cryptoGenerated)
				{
					this._GenerateCryptoBytes();
				}
				return this._keyBytes;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00018D64 File Offset: 0x00016F64
		public byte[] MacIv
		{
			get
			{
				if (!this._cryptoGenerated)
				{
					this._GenerateCryptoBytes();
				}
				return this._MacInitializationVector;
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00018D80 File Offset: 0x00016F80
		public void ReadAndVerifyMac(Stream s)
		{
			bool flag = false;
			this._StoredMac = new byte[10];
			s.Read(this._StoredMac, 0, this._StoredMac.Length);
			if (this._StoredMac.Length != this.CalculatedMac.Length)
			{
				flag = true;
			}
			if (!flag)
			{
				for (int i = 0; i < this._StoredMac.Length; i++)
				{
					if (this._StoredMac[i] != this.CalculatedMac[i])
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				throw new BadStateException("The MAC does not match.");
			}
		}

		// Token: 0x04000198 RID: 408
		internal byte[] _Salt;

		// Token: 0x04000199 RID: 409
		internal byte[] _providedPv;

		// Token: 0x0400019A RID: 410
		internal byte[] _generatedPv;

		// Token: 0x0400019B RID: 411
		internal int _KeyStrengthInBits;

		// Token: 0x0400019C RID: 412
		private byte[] _MacInitializationVector;

		// Token: 0x0400019D RID: 413
		private byte[] _StoredMac;

		// Token: 0x0400019E RID: 414
		private byte[] _keyBytes;

		// Token: 0x0400019F RID: 415
		private short PasswordVerificationStored;

		// Token: 0x040001A0 RID: 416
		private short PasswordVerificationGenerated;

		// Token: 0x040001A1 RID: 417
		private int Rfc2898KeygenIterations = 1000;

		// Token: 0x040001A2 RID: 418
		private string _Password;

		// Token: 0x040001A3 RID: 419
		private bool _cryptoGenerated;

		// Token: 0x040001A4 RID: 420
		public byte[] CalculatedMac;
	}
}
