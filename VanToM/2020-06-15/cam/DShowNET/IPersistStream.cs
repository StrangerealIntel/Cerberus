using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000057 RID: 87
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0000010c-0000-0000-C000-000000000046")]
	[ComVisible(true)]
	[ComImport]
	public interface IPersistStream
	{
		// Token: 0x0600023B RID: 571
		[PreserveSig]
		int GetClassID(out Guid pClassID);
	}
}
