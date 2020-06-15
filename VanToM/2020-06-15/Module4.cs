using System;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.CompilerServices;

namespace Stub
{
	// Token: 0x0200003B RID: 59
	[StandardModule]
	internal sealed class Module4
	{
		// Token: 0x06000154 RID: 340
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr OpenThread(Module4.ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

		// Token: 0x06000155 RID: 341
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern uint SuspendThread(IntPtr hThread);

		// Token: 0x06000156 RID: 342
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern uint ResumeThread(IntPtr hThread);

		// Token: 0x06000157 RID: 343
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern bool CloseHandle(IntPtr hHandle);

		// Token: 0x0200003C RID: 60
		public enum ThreadAccess
		{
			// Token: 0x04000132 RID: 306
			TERMINATE = 1,
			// Token: 0x04000133 RID: 307
			SUSPEND_RESUME,
			// Token: 0x04000134 RID: 308
			GET_CONTEXT = 8,
			// Token: 0x04000135 RID: 309
			SET_CONTEXT = 16,
			// Token: 0x04000136 RID: 310
			SET_INFORMATION = 32,
			// Token: 0x04000137 RID: 311
			QUERY_INFORMATION = 64,
			// Token: 0x04000138 RID: 312
			SET_THREAD_TOKEN = 128,
			// Token: 0x04000139 RID: 313
			IMPERSONATE = 256,
			// Token: 0x0400013A RID: 314
			DIRECT_IMPERSONATION = 512
		}
	}
}
