using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200006A RID: 106
	[Guid("a2104830-7c70-11cf-8bce-00aa00a3f1a6")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IFileSinkFilter
	{
		// Token: 0x0600029E RID: 670
		[PreserveSig]
		int SetFileName([MarshalAs(UnmanagedType.LPWStr)] [In] string pszFileName, [MarshalAs(UnmanagedType.LPStruct)] [In] AMMediaType pmt);

		// Token: 0x0600029F RID: 671
		[PreserveSig]
		int GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string pszFileName, [MarshalAs(UnmanagedType.LPStruct)] [Out] AMMediaType pmt);
	}
}
