using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000074 RID: 116
	[Guid("211A8760-03AC-11d1-8D13-00AA00BD8339")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IAMTunerNotification
	{
		// Token: 0x060002BB RID: 699
		[PreserveSig]
		int OnEvent(AMTunerEventType Event);
	}
}
