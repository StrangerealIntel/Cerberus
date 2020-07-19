using System;
using System.Runtime.InteropServices;
using SmartAssembly.StringsEncoding;

namespace ChromV2
{
	// Token: 0x02000072 RID: 114
	public static class BCrypt
	{
		// Token: 0x06000264 RID: 612
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptOpenAlgorithmProvider(out IntPtr phAlgorithm, [MarshalAs(UnmanagedType.LPWStr)] string pszAlgId, [MarshalAs(UnmanagedType.LPWStr)] string pszImplementation, uint dwFlags);

		// Token: 0x06000265 RID: 613
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptCloseAlgorithmProvider(IntPtr hAlgorithm, uint flags);

		// Token: 0x06000266 RID: 614
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptGetProperty(IntPtr hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, byte[] pbOutput, int cbOutput, ref int pcbResult, uint flags);

		// Token: 0x06000267 RID: 615
		[DllImport("bcrypt.dll", EntryPoint = "BCryptSetProperty")]
		internal static extern uint BCryptSetAlgorithmProperty(IntPtr hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, byte[] pbInput, int cbInput, int dwFlags);

		// Token: 0x06000268 RID: 616
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptImportKey(IntPtr hAlgorithm, IntPtr hImportKey, [MarshalAs(UnmanagedType.LPWStr)] string pszBlobType, out IntPtr phKey, IntPtr pbKeyObject, int cbKeyObject, byte[] pbInput, int cbInput, uint dwFlags);

		// Token: 0x06000269 RID: 617
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptDestroyKey(IntPtr hKey);

		// Token: 0x0600026A RID: 618
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptEncrypt(IntPtr hKey, byte[] pbInput, int cbInput, ref BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO pPaddingInfo, byte[] pbIV, int cbIV, byte[] pbOutput, int cbOutput, ref int pcbResult, uint dwFlags);

		// Token: 0x0600026B RID: 619
		[DllImport("bcrypt.dll")]
		internal static extern uint BCryptDecrypt(IntPtr, byte[], int, ref BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO, byte[], int, byte[], int, ref int, int);

		// Token: 0x04000111 RID: 273
		public const uint ERROR_SUCCESS = 0u;

		// Token: 0x04000112 RID: 274
		public const uint BCRYPT_PAD_PSS = 8u;

		// Token: 0x04000113 RID: 275
		public const uint BCRYPT_PAD_OAEP = 4u;

		// Token: 0x04000114 RID: 276
		public static readonly byte[] BCRYPT_KEY_DATA_BLOB_MAGIC = BitConverter.GetBytes(1296188491);

		// Token: 0x04000115 RID: 277
		public static readonly string BCRYPT_OBJECT_LENGTH = ChromV265450.Strings.Get(107396440);

		// Token: 0x04000116 RID: 278
		public static readonly string BCRYPT_CHAIN_MODE_GCM = ChromV265450.Strings.Get(107396391);

		// Token: 0x04000117 RID: 279
		public static readonly string BCRYPT_AUTH_TAG_LENGTH = ChromV265450.Strings.Get(107396402);

		// Token: 0x04000118 RID: 280
		public static readonly string BCRYPT_CHAINING_MODE = ChromV265450.Strings.Get(107396381);

		// Token: 0x04000119 RID: 281
		public static readonly string BCRYPT_KEY_DATA_BLOB = ChromV265450.Strings.Get(107395820);

		// Token: 0x0400011A RID: 282
		public static readonly string BCRYPT_AES_ALGORITHM = ChromV265450.Strings.Get(107395835);

		// Token: 0x0400011B RID: 283
		public static readonly string MS_PRIMITIVE_PROVIDER = ChromV265450.Strings.Get(107395830);

		// Token: 0x0400011C RID: 284
		public static readonly int BCRYPT_AUTH_MODE_CHAIN_CALLS_FLAG = 1;

		// Token: 0x0400011D RID: 285
		public static readonly int BCRYPT_INIT_AUTH_MODE_INFO_VERSION = 1;

		// Token: 0x0400011E RID: 286
		public static readonly uint STATUS_AUTH_TAG_MISMATCH = 3221266434u;

		// Token: 0x02000259 RID: 601
		public struct BCRYPT_PSS_PADDING_INFO
		{
			// Token: 0x060016C5 RID: 5829 RVA: 0x00075AA0 File Offset: 0x00073CA0
			public BCRYPT_PSS_PADDING_INFO(string pszAlgId, int cbSalt)
			{
				this.pszAlgId = pszAlgId;
				this.cbSalt = cbSalt;
			}

			// Token: 0x04000A73 RID: 2675
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszAlgId;

			// Token: 0x04000A74 RID: 2676
			public int cbSalt;
		}

		// Token: 0x0200025A RID: 602
		public struct BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO : IDisposable
		{
			// Token: 0x060016C6 RID: 5830 RVA: 0x00075AB0 File Offset: 0x00073CB0
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

			// Token: 0x060016C7 RID: 5831 RVA: 0x00075B9C File Offset: 0x00073D9C
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

			// Token: 0x04000A75 RID: 2677
			public int cbSize;

			// Token: 0x04000A76 RID: 2678
			public int dwInfoVersion;

			// Token: 0x04000A77 RID: 2679
			public IntPtr pbNonce;

			// Token: 0x04000A78 RID: 2680
			public int cbNonce;

			// Token: 0x04000A79 RID: 2681
			public IntPtr pbAuthData;

			// Token: 0x04000A7A RID: 2682
			public int cbAuthData;

			// Token: 0x04000A7B RID: 2683
			public IntPtr pbTag;

			// Token: 0x04000A7C RID: 2684
			public int cbTag;

			// Token: 0x04000A7D RID: 2685
			public IntPtr pbMacContext;

			// Token: 0x04000A7E RID: 2686
			public int cbMacContext;

			// Token: 0x04000A7F RID: 2687
			public int cbAAD;

			// Token: 0x04000A80 RID: 2688
			public long cbData;

			// Token: 0x04000A81 RID: 2689
			public int dwFlags;
		}

		// Token: 0x0200025B RID: 603
		public struct BCRYPT_KEY_LENGTHS_STRUCT
		{
			// Token: 0x04000A82 RID: 2690
			public int dwMinLength;

			// Token: 0x04000A83 RID: 2691
			public int dwMaxLength;

			// Token: 0x04000A84 RID: 2692
			public int dwIncrement;
		}

		// Token: 0x0200025C RID: 604
		public struct BCRYPT_OAEP_PADDING_INFO
		{
			// Token: 0x060016C8 RID: 5832 RVA: 0x00075C30 File Offset: 0x00073E30
			public BCRYPT_OAEP_PADDING_INFO(string alg)
			{
				this.pszAlgId = alg;
				this.pbLabel = IntPtr.Zero;
				this.cbLabel = 0;
			}

			// Token: 0x04000A85 RID: 2693
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszAlgId;

			// Token: 0x04000A86 RID: 2694
			public IntPtr pbLabel;

			// Token: 0x04000A87 RID: 2695
			public int cbLabel;
		}
	}
}
