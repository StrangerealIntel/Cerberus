using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000049 RID: 73
	[Guid("56a868b6-0ad4-11ce-b03a-0020af0ba770")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[ComImport]
	public interface IMediaEvent
	{
		// Token: 0x060001B8 RID: 440
		[PreserveSig]
		int GetEventHandle(ref IntPtr hEvent);

		// Token: 0x060001B9 RID: 441
		[PreserveSig]
		int GetEvent(ref DsEvCode lEventCode, ref int lParam1, ref int lParam2, int msTimeout);

		// Token: 0x060001BA RID: 442
		[PreserveSig]
		int WaitForCompletion(int msTimeout, ref int pEvCode);

		// Token: 0x060001BB RID: 443
		[PreserveSig]
		int CancelDefaultHandling(int lEvCode);

		// Token: 0x060001BC RID: 444
		[PreserveSig]
		int RestoreDefaultHandling(int lEvCode);

		// Token: 0x060001BD RID: 445
		[PreserveSig]
		int FreeEventParams(DsEvCode lEvCode, int lParam1, int lParam2);
	}
}
