using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200006E RID: 110
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	[Guid("C6E13340-30AC-11d0-A18C-00A0C9118956")]
	[ComImport]
	public interface IAMStreamConfig
	{
		// Token: 0x060002A8 RID: 680
		[PreserveSig]
		int SetFormat([MarshalAs(UnmanagedType.LPStruct)] [In] AMMediaType pmt);

		// Token: 0x060002A9 RID: 681
		[PreserveSig]
		int GetFormat(out IntPtr pmt);

		// Token: 0x060002AA RID: 682
		[PreserveSig]
		int GetNumberOfCapabilities(ref int piCount, ref int piSize);

		// Token: 0x060002AB RID: 683
		[PreserveSig]
		int GetStreamCaps(int iIndex, out IntPtr pmt, [In] IntPtr pSCC);
	}
}
