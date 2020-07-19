using System;
using System.Text;
using SmartAssembly.StringsEncoding;

namespace ChromV1
{
	// Token: 0x02000058 RID: 88
	public static class AesGcm256
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000E230 File Offset: 0x0000C430
		public static string Decrypt(byte[] encryptedBytes, byte[] key, byte[] iv)
		{
			string text = string.Empty;
			string result;
			try
			{
				GcmBlockCipher gcmBlockCipher = new GcmBlockCipher(new AesFastEngine());
				AeadParameters parameters = new AeadParameters(new KeyParameter(key), 128, iv, null);
				gcmBlockCipher.Init(false, parameters);
				byte[] array = new byte[gcmBlockCipher.GetOutputSize(encryptedBytes.Length)];
				int outOff = gcmBlockCipher.ProcessBytes(encryptedBytes, 0, encryptedBytes.Length, array, 0);
				gcmBlockCipher.DoFinal(array, outOff);
				text = Encoding.UTF8.GetString(array).TrimEnd(Strings.Get(107396945).ToCharArray());
				result = text;
			}
			catch
			{
				result = text;
			}
			return result;
		}
	}
}
