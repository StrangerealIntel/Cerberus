using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200004A RID: 74
	[Guid("56a868c0-0ad4-11ce-b03a-0020af0ba770")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[ComVisible(true)]
	[ComImport]
	public interface IMediaEventEx
	{
		// Token: 0x060001BE RID: 446
		[PreserveSig]
		int GetEventHandle(ref IntPtr hEvent);

		// Token: 0x060001BF RID: 447
		[PreserveSig]
		int GetEvent(ref DsEvCode lEventCode, ref int lParam1, ref int lParam2, int msTimeout);

		// Token: 0x060001C0 RID: 448
		[PreserveSig]
		int WaitForCompletion(int msTimeout, out int pEvCode);

		// Token: 0x060001C1 RID: 449
		[PreserveSig]
		int CancelDefaultHandling(int lEvCode);

		// Token: 0x060001C2 RID: 450
		[PreserveSig]
		int RestoreDefaultHandling(int lEvCode);

		// Token: 0x060001C3 RID: 451
		[PreserveSig]
		int FreeEventParams(DsEvCode lEvCode, int lParam1, int lParam2);

		// Token: 0x060001C4 RID: 452
		[PreserveSig]
		int SetNotifyWindow(IntPtr hwnd, int lMsg, IntPtr lInstanceData);

		// Token: 0x060001C5 RID: 453
		[PreserveSig]
		int SetNotifyFlags(int lNoNotifyFlags);

		// Token: 0x060001C6 RID: 454
		[PreserveSig]
		int GetNotifyFlags(ref int lplNoNotifyFlags);
	}
}
