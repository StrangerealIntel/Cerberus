using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200006D RID: 109
	[Guid("e46a9787-2b71-444d-a4b5-1fab7b708d6a")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IVideoFrameStep
	{
		// Token: 0x060002A5 RID: 677
		[PreserveSig]
		int Step(int dwFrames, [MarshalAs(UnmanagedType.IUnknown)] [In] object pStepObject);

		// Token: 0x060002A6 RID: 678
		[PreserveSig]
		int CanStep(int bMultiple, [MarshalAs(UnmanagedType.IUnknown)] [In] object pStepObject);

		// Token: 0x060002A7 RID: 679
		[PreserveSig]
		int CancelStep();
	}
}
