using System;
using System.Security.Cryptography;
using System.Text;

// Token: 0x0200000A RID: 10
internal class Encryption
{
	// Token: 0x06000038 RID: 56 RVA: 0x00002DC0 File Offset: 0x00001DC0
	public static string Release(string input, string key)
	{
		byte[] bytes = Encoding.Unicode.GetBytes(input);
		byte[] bytes2 = Encoding.Unicode.GetBytes(key);
		byte[] bytes3 = Encryption.RSMDecrypt(bytes, bytes2);
		return Encoding.Unicode.GetString(bytes3);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002E04 File Offset: 0x00001E04
	public static byte[] RSMDecrypt(byte[] ƈƖƻƨÔ, byte[] ƄƏƵÉ)
	{
		Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(ƄƏƵÉ, new byte[8], 1);
		byte[] array = new RijndaelManaged
		{
			Key = rfc2898DeriveBytes.GetBytes(16),
			IV = rfc2898DeriveBytes.GetBytes(16)
		}.CreateDecryptor().TransformFinalBlock(ƈƖƻƨÔ, 0, ƈƖƻƨÔ.Length);
		checked
		{
			byte[] array2 = new byte[array.Length - 17 + 1];
			Buffer.BlockCopy(array, 16, array2, 0, array.Length - 16);
			return array2;
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002E7C File Offset: 0x00001E7C
	public static string DecryptText(string input, string key)
	{
		char[] array = input.ToCharArray();
		char[] array2 = key.ToCharArray();
		checked
		{
			char[] array3 = new char[input.Length - 2 + 1];
			int num = (int)array[input.Length - 1];
			array[input.Length - 1] = '\0';
			int num2 = 0;
			int num3 = 0;
			int num4 = input.Length - 1;
			for (int i = num3; i <= num4; i++)
			{
				if (i < input.Length - 1)
				{
					if (num2 >= array2.Length)
					{
						num2 = 0;
					}
					int num5 = (int)array[i];
					int num6 = (int)array2[num2];
					int value = num5 - num - num6;
					array3[i] = Convert.ToChar(value);
					num2++;
				}
			}
			return new string(array3);
		}
	}
}
