using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200006C RID: 108
	[Guid("670d1d20-a068-11d0-b3f0-00aa003761c5")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IAMCopyCaptureFileProgress
	{
		// Token: 0x060002A4 RID: 676
		[PreserveSig]
		int Progress(int iProgress);
	}
}
