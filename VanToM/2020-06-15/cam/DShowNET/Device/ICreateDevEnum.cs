using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET.Device
{
	// Token: 0x02000066 RID: 102
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("29840822-5B84-11D0-BD3B-00A0C911CE86")]
	[ComImport]
	public interface ICreateDevEnum
	{
		// Token: 0x06000283 RID: 643
		[PreserveSig]
		int CreateClassEnumerator([In] ref Guid pType, out UCOMIEnumMoniker ppEnumMoniker, [In] int dwFlags);
	}
}
