using System;
using System.Diagnostics;
using System.Threading;

namespace MufMaOSvGyvz
{
	// Token: 0x02000013 RID: 19
	public static class Mutex
	{
		// Token: 0x06000056 RID: 86
		public static void GenerateMutex(string string_0)
		{
			using (Mutex mutex = new Mutex(false, "Global\\" + string_0))
			{
				if (!mutex.WaitOne(0, false))
				{
					Process.GetCurrentProcess().Kill();
				}
			}
		}
	}
}
