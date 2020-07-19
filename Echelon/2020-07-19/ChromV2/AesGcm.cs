using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using SmartAssembly.StringsEncoding;

namespace ChromV2
{
	// Token: 0x02000071 RID: 113
	internal sealed class AesGcm
	{
		// Token: 0x0600025D RID: 605 RVA: 0x000132A4 File Offset: 0x000114A4
		public byte[] Decrypt(byte[] A_1, byte[] A_2, byte[] A_3, byte[] A_4, byte[] A_5)
		{
			IntPtr intPtr = this.OpenAlgorithmProvider(BCrypt.BCRYPT_AES_ALGORITHM, BCrypt.MS_PRIMITIVE_PROVIDER, BCrypt.BCRYPT_CHAIN_MODE_GCM);
			IntPtr intPtr2;
			IntPtr hglobal = this.ImportKey(intPtr, A_1, out intPtr2);
			BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO bcrypt_AUTHENTICATED_CIPHER_MODE_INFO = new BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO(A_2, A_3, A_5);
			byte[] array2;
			using (bcrypt_AUTHENTICATED_CIPHER_MODE_INFO)
			{
				byte[] array = new byte[this.MaxAuthTagSize(intPtr)];
				int num = 0;
				uint num2 = BCrypt.BCryptDecrypt(intPtr2, A_4, A_4.Length, ref bcrypt_AUTHENTICATED_CIPHER_MODE_INFO, array, array.Length, null, 0, ref num, 0);
				if (num2 != 0u)
				{
					throw new CryptographicException(string.Format(ChromV265450.Strings.Get(107396637), num2));
				}
				array2 = new byte[num];
				num2 = BCrypt.BCryptDecrypt(intPtr2, A_4, A_4.Length, ref bcrypt_AUTHENTICATED_CIPHER_MODE_INFO, array, array.Length, array2, array2.Length, ref num, 0);
				if (num2 == BCrypt.STATUS_AUTH_TAG_MISMATCH)
				{
					throw new CryptographicException(ChromV265450.Strings.Get(107397032));
				}
				if (num2 != 0u)
				{
					throw new CryptographicException(string.Format(ChromV265450.Strings.Get(107396963), num2));
				}
			}
			BCrypt.BCryptDestroyKey(intPtr2);
			Marshal.FreeHGlobal(hglobal);
			BCrypt.BCryptCloseAlgorithmProvider(intPtr, 0u);
			return array2;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000133D0 File Offset: 0x000115D0
		private int MaxAuthTagSize(IntPtr A_1)
		{
			byte[] property = this.GetProperty(A_1, BCrypt.BCRYPT_AUTH_TAG_LENGTH);
			return BitConverter.ToInt32(new byte[]
			{
				property[4],
				property[5],
				property[6],
				property[7]
			}, 0);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00013414 File Offset: 0x00011614
		private IntPtr OpenAlgorithmProvider(string A_1, string A_2, string A_3)
		{
			IntPtr zero = IntPtr.Zero;
			uint num = BCrypt.BCryptOpenAlgorithmProvider(out zero, A_1, A_2, 0u);
			if (num != 0u)
			{
				throw new CryptographicException(string.Format(ChromV265450.Strings.Get(107396926), num));
			}
			byte[] bytes = Encoding.Unicode.GetBytes(A_3);
			num = BCrypt.BCryptSetAlgorithmProperty(zero, BCrypt.BCRYPT_CHAINING_MODE, bytes, bytes.Length, 0);
			if (num != 0u)
			{
				throw new CryptographicException(string.Format(ChromV265450.Strings.Get(107396293), num));
			}
			return zero;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00013498 File Offset: 0x00011698
		private IntPtr ImportKey(IntPtr hAlg, byte[] key, out IntPtr hKey)
		{
			int num = BitConverter.ToInt32(this.GetProperty(hAlg, BCrypt.BCRYPT_OBJECT_LENGTH), 0);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			byte[] array = this.Concat(new byte[][]
			{
				BCrypt.BCRYPT_KEY_DATA_BLOB_MAGIC,
				BitConverter.GetBytes(1),
				BitConverter.GetBytes(key.Length),
				key
			});
			uint num2 = BCrypt.BCryptImportKey(hAlg, IntPtr.Zero, BCrypt.BCRYPT_KEY_DATA_BLOB, out hKey, intPtr, num, array, array.Length, 0u);
			if (num2 != 0u)
			{
				throw new CryptographicException(string.Format(ChromV265450.Strings.Get(107396131), num2));
			}
			return intPtr;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0001352C File Offset: 0x0001172C
		private byte[] GetProperty(IntPtr A_1, string A_2)
		{
			int num = 0;
			uint num2 = BCrypt.BCryptGetProperty(A_1, A_2, null, 0, ref num, 0u);
			if (num2 != 0u)
			{
				throw new CryptographicException(string.Format(ChromV265450.Strings.Get(107396602), num2));
			}
			byte[] array = new byte[num];
			num2 = BCrypt.BCryptGetProperty(A_1, A_2, array, array.Length, ref num, 0u);
			if (num2 != 0u)
			{
				throw new CryptographicException(string.Format(ChromV265450.Strings.Get(107396481), num2));
			}
			return array;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000135A8 File Offset: 0x000117A8
		public byte[] Concat(byte[][] A_1)
		{
			int num = 0;
			foreach (byte array in A_1)
			{
				if (array != null)
				{
					num += array.Length;
				}
			}
			byte[] array2 = new byte[num - 1 + 1];
			int num2 = 0;
			foreach (byte array3 in A_1)
			{
				if (array3 != null)
				{
					Buffer.BlockCopy(array3, 0, array2, num2, array3.Length);
					num2 += array3.Length;
				}
			}
			return array2;
		}
	}
}
