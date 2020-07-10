using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.Devices;

namespace KillClass
{
	// Token: 0x0200000C RID: 12
	internal class Kill
	{
		// Token: 0x06000031 RID: 49
		public static void KillSwitch()
		{
			if (Kill.AntiVM() || Kill.CheckDebugger() || Kill.CheckSandboxieDLL() || Kill.CheckSize() || Kill.CheckXP())
			{
				Process.GetCurrentProcess().Kill();
			}
			Environment.FailFast(null);
		}

		// Token: 0x06000032 RID: 50
		private static bool CheckSize()
		{
			try
			{
				if (new DriveInfo(Path.GetPathRoot(Environment.SystemDirectory)).TotalSize <= 61000000000L)
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000033 RID: 51
		private static bool CheckXP()
		{
			try
			{
				if (new ComputerInfo().OSFullName.ToLower().Contains("xp"))
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000034 RID: 52
		private static bool AntiVM()
		{
			try
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
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000035 RID: 53
		private static bool CheckDebugger()
		{
			bool flag = false;
			bool result;
			try
			{
				Kill.CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref flag);
				result = flag;
			}
			catch
			{
				result = flag;
			}
			return result;
		}

		// Token: 0x06000036 RID: 54
		private static bool CheckSandboxieDLL()
		{
			bool result;
			try
			{
				if (Kill.GetModuleHandle("SbieDll.dll").ToInt32() != 0)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000037 RID: 55
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetModuleHandle(string string_0);

		// Token: 0x06000038 RID: 56
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern bool CheckRemoteDebuggerPresent(IntPtr intptr_0, ref bool bool_0);
	}
}
