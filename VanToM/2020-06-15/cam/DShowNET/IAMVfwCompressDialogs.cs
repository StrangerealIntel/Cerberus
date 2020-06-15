using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200007B RID: 123
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("D8D715A3-6E5E-11D0-B3F0-00AA003761C5")]
	[ComVisible(true)]
	[ComImport]
	public interface IAMVfwCompressDialogs
	{
		// Token: 0x060002EB RID: 747
		[PreserveSig]
		int ShowDialog([In] VfwCompressDialogs iDialog, [In] IntPtr hwnd);

		// Token: 0x060002EC RID: 748
		int GetState([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] pState, ref int pcbState);

		// Token: 0x060002ED RID: 749
		int SetState([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] byte[] pState, [In] int cbState);

		// Token: 0x060002EE RID: 750
		int SendDriverMessage(int uMsg, long dw1, long dw2);
	}
}
