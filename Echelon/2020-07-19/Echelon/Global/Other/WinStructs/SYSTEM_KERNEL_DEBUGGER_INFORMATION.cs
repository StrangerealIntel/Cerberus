using System;
using System.Runtime.InteropServices;

namespace Echelon.Global.Other.WinStructs
{
	// Token: 0x02000052 RID: 82
	public struct SYSTEM_KERNEL_DEBUGGER_INFORMATION
	{
		// Token: 0x040000AD RID: 173
		[MarshalAs(UnmanagedType.U1)]
		public bool KernelDebuggerEnabled;

		// Token: 0x040000AE RID: 174
		[MarshalAs(UnmanagedType.U1)]
		public bool KernelDebuggerNotPresent;
	}
}
