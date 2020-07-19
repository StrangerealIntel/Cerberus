using System;
using System.Diagnostics;
using Echelon.Global.Other;
using Echelon.Global.Other.WinStructs;

namespace Echelon.Global.Protect
{
	// Token: 0x0200004B RID: 75
	internal class DebugProtect3
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x0000D058 File Offset: 0x0000B258
		public static void HideOSThreads()
		{
			foreach (object obj in Process.GetCurrentProcess().Threads)
			{
				ProcessThread processThread = (ProcessThread)obj;
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("[Получение потоков]: thread.Id {0:X}", processThread.Id);
				IntPtr intPtr = NativeMethods.OpenThread(ThreadAccess.SET_INFORMATION, false, (uint)processThread.Id);
				if (intPtr == IntPtr.Zero)
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine("[Получение потоков]: порпуск thread.Id {0:X}", processThread.Id);
				}
				else
				{
					if (DebugProtect3.HideFromDebugger(intPtr))
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("[Получение потоков]: thread.Id {0:X} спрятан от дебаггера.", processThread.Id);
					}
					NativeMethods.CloseHandle(intPtr);
				}
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000D144 File Offset: 0x0000B344
		public static bool HideFromDebugger(IntPtr Handle)
		{
			return NativeMethods.NtSetInformationThread(Handle, ThreadInformationClass.ThreadHideFromDebugger, IntPtr.Zero, 0) == NtStatus.Success;
		}
	}
}
