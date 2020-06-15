using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET.Device
{
	// Token: 0x02000067 RID: 103
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	[Guid("55272A00-42CB-11CE-8135-00AA004BB851")]
	[ComImport]
	public interface IPropertyBag
	{
		// Token: 0x06000284 RID: 644
		[PreserveSig]
		int Read([MarshalAs(UnmanagedType.LPWStr)] [In] string pszPropName, [MarshalAs(UnmanagedType.Struct)] [In] [Out] ref object pVar, IntPtr pErrorLog);

		// Token: 0x06000285 RID: 645
		[PreserveSig]
		int Write([MarshalAs(UnmanagedType.LPWStr)] [In] string pszPropName, [MarshalAs(UnmanagedType.Struct)] [In] ref object pVar);
	}
}
