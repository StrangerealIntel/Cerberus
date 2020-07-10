using System;
using System.Security.Cryptography;

namespace MufMaOSvGyvz
{
	// Token: 0x02000009 RID: 9
	public class Random
	{
		// Token: 0x06000026 RID: 38
		public static string RandomString(int int_0)
		{
			string result;
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				int num = int_0 * 6;
				int num2 = (num + 7) / 8;
				byte[] array = new byte[num2];
				rngcryptoServiceProvider.GetBytes(array);
				result = Convert.ToBase64String(array);
			}
			return result;
		}
	}
}
