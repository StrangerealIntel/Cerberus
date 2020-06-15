using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000056 RID: 86
	[Guid("0000010c-0000-0000-C000-000000000046")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IPersist
	{
		// Token: 0x0600023A RID: 570
		[PreserveSig]
		int GetClassID(out Guid pClassID);
	}
}
