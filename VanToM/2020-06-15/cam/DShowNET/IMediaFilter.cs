using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000058 RID: 88
	[Guid("56a86899-0ad4-11ce-b03a-0020af0ba770")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IMediaFilter
	{
		// Token: 0x0600023C RID: 572
		[PreserveSig]
		int GetClassID(out Guid pClassID);

		// Token: 0x0600023D RID: 573
		[PreserveSig]
		int Stop();

		// Token: 0x0600023E RID: 574
		[PreserveSig]
		int Pause();

		// Token: 0x0600023F RID: 575
		[PreserveSig]
		int Run(long tStart);

		// Token: 0x06000240 RID: 576
		[PreserveSig]
		int GetState(int dwMilliSecsTimeout, ref int filtState);

		// Token: 0x06000241 RID: 577
		[PreserveSig]
		int SetSyncSource([In] IReferenceClock pClock);

		// Token: 0x06000242 RID: 578
		[PreserveSig]
		int GetSyncSource(out IReferenceClock pClock);
	}
}
