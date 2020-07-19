using System;
using System.Security.Principal;

namespace Echelon.Stealer.SystemsData
{
	// Token: 0x02000022 RID: 34
	public static class RunChecker
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000052C8 File Offset: 0x000034C8
		public static bool IsAdmin
		{
			get
			{
				return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000052E0 File Offset: 0x000034E0
		public static bool IsWin64
		{
			get
			{
				return Environment.Is64BitOperatingSystem;
			}
		}
	}
}
