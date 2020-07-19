using System;
using System.Security.Cryptography;
using CG.Web.MegaApiClient.Cryptography;
using CG.Web.MegaApiClient.Serialization;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000E2 RID: 226
	internal class Crypto
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x00039650 File Offset: 0x00037850
		static Crypto()
		{
			Crypto.AesCbc = new AesManaged();
			Crypto.IsKnownReusable = true;
			Crypto.AesCbc.Padding = PaddingMode.None;
			Crypto.AesCbc.Mode = CipherMode.CBC;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00039684 File Offset: 0x00037884
		public static byte[] DecryptKey(byte[] data, byte[] key)
		{
			byte[] array = new byte[data.Length];
			for (int i = 0; i < data.Length; i += 16)
			{
				Array.Copy(Crypto.DecryptAes(data.CopySubArray(16, i), key), 0, array, i, 16);
			}
			return array;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000396CC File Offset: 0x000378CC
		public static byte[] EncryptKey(byte[] data, byte[] key)
		{
			byte[] array = new byte[data.Length];
			using (ICryptoTransform cryptoTransform = Crypto.CreateAesEncryptor(key))
			{
				for (int i = 0; i < data.Length; i += 16)
				{
					Array.Copy(Crypto.EncryptAes(data.CopySubArray(16, i), cryptoTransform), 0, array, i, 16);
				}
			}
			return array;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00039738 File Offset: 0x00037938
		public static void GetPartsFromDecryptedKey(byte[] decryptedKey, out byte[] iv, out byte[] metaMac, out byte[] fileKey)
		{
			iv = new byte[8];
			metaMac = new byte[8];
			Array.Copy(decryptedKey, 16, iv, 0, 8);
			Array.Copy(decryptedKey, 24, metaMac, 0, 8);
			fileKey = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				fileKey[i] = (decryptedKey[i] ^ decryptedKey[i + 16]);
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0003979C File Offset: 0x0003799C
		public static byte[] DecryptAes(byte[] data, byte[] key)
		{
			byte[] result;
			using (ICryptoTransform cryptoTransform = Crypto.AesCbc.CreateDecryptor(key, Crypto.DefaultIv))
			{
				result = cryptoTransform.TransformFinalBlock(data, 0, data.Length);
			}
			return result;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000397EC File Offset: 0x000379EC
		public static ICryptoTransform CreateAesEncryptor(byte[] key)
		{
			return new CachedCryptoTransform(() => Crypto.AesCbc.CreateEncryptor(key, Crypto.DefaultIv), Crypto.IsKnownReusable);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00039810 File Offset: 0x00037A10
		public static byte[] EncryptAes(byte[] data, ICryptoTransform encryptor)
		{
			return encryptor.TransformFinalBlock(data, 0, data.Length);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00039820 File Offset: 0x00037A20
		public static byte[] EncryptAes(byte[] data, byte[] key)
		{
			byte[] result;
			using (ICryptoTransform cryptoTransform = Crypto.CreateAesEncryptor(key))
			{
				result = cryptoTransform.TransformFinalBlock(data, 0, data.Length);
			}
			return result;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00039864 File Offset: 0x00037A64
		public static byte[] CreateAesKey()
		{
			byte[] key;
			using (Aes aes = Aes.Create())
			{
				aes.Mode = CipherMode.CBC;
				aes.KeySize = 128;
				aes.Padding = PaddingMode.None;
				aes.GenerateKey();
				key = aes.Key;
			}
			return key;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000398C0 File Offset: 0x00037AC0
		public static byte[] EncryptAttributes(Attributes attributes, byte[] nodeKey)
		{
			byte[] array = ("MEGA" + JsonConvert.SerializeObject(attributes, Formatting.None)).ToBytes();
			array = array.CopySubArray(array.Length + 16 - array.Length % 16, 0);
			return Crypto.EncryptAes(array, nodeKey);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00039904 File Offset: 0x00037B04
		public static Attributes DecryptAttributes(byte[] attributes, byte[] nodeKey)
		{
			byte[] data = Crypto.DecryptAes(attributes, nodeKey);
			Attributes result;
			try
			{
				string text = data.ToUTF8String().Substring(4);
				int num = text.IndexOf('\0');
				if (num != -1)
				{
					text = text.Substring(0, num);
				}
				result = JsonConvert.DeserializeObject<Attributes>(text);
			}
			catch (Exception ex)
			{
				result = new Attributes(string.Format("Attribute deserialization failed: {0}", ex.Message));
			}
			return result;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0003997C File Offset: 0x00037B7C
		public static BigInteger[] GetRsaPrivateKeyComponents(byte[] encodedRsaPrivateKey, byte[] masterKey)
		{
			encodedRsaPrivateKey = encodedRsaPrivateKey.CopySubArray(encodedRsaPrivateKey.Length + (16 - encodedRsaPrivateKey.Length % 16), 0);
			byte[] array = Crypto.DecryptKey(encodedRsaPrivateKey, masterKey);
			BigInteger[] array2 = new BigInteger[4];
			for (int i = 0; i < 4; i++)
			{
				array2[i] = array.FromMPINumber();
				int num = ((int)array[0] * 256 + (int)array[1] + 7) / 8;
				array = array.CopySubArray(array.Length - num - 2, num + 2);
			}
			return array2;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000399F4 File Offset: 0x00037BF4
		public static byte[] RsaDecrypt(BigInteger data, BigInteger p, BigInteger q, BigInteger d)
		{
			return data.modPow(d, p * q).getBytes();
		}

		// Token: 0x040004C7 RID: 1223
		private static readonly Aes AesCbc;

		// Token: 0x040004C8 RID: 1224
		private static readonly bool IsKnownReusable;

		// Token: 0x040004C9 RID: 1225
		private static readonly byte[] DefaultIv = new byte[16];
	}
}
