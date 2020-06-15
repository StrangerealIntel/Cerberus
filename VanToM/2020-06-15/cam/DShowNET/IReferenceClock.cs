using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200005E RID: 94
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("56a86897-0ad4-11ce-b03a-0020af0ba770")]
	[ComVisible(true)]
	[ComImport]
	public interface IReferenceClock
	{
		// Token: 0x06000261 RID: 609
		[PreserveSig]
		int GetTime(ref long pTime);

		// Token: 0x06000262 RID: 610
		[PreserveSig]
		int AdviseTime(long baseTime, long streamTime, IntPtr hEvent, ref int pdwAdviseCookie);

		// Token: 0x06000263 RID: 611
		[PreserveSig]
		int AdvisePeriodic(long startTime, long periodTime, IntPtr hSemaphore, ref int pdwAdviseCookie);

		// Token: 0x06000264 RID: 612
		[PreserveSig]
		int Unadvise(int dwAdviseCookie);
	}
}
