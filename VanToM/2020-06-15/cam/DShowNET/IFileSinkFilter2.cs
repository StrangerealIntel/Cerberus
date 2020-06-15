using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200006B RID: 107
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00855B90-CE1B-11d0-BD4F-00A0C911CE86")]
	[ComVisible(true)]
	[ComImport]
	public interface IFileSinkFilter2
	{
		// Token: 0x060002A0 RID: 672
		[PreserveSig]
		int SetFileName([MarshalAs(UnmanagedType.LPWStr)] [In] string pszFileName, [MarshalAs(UnmanagedType.LPStruct)] [In] AMMediaType pmt);

		// Token: 0x060002A1 RID: 673
		[PreserveSig]
		int GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string pszFileName, [MarshalAs(UnmanagedType.LPStruct)] [Out] AMMediaType pmt);

		// Token: 0x060002A2 RID: 674
		[PreserveSig]
		int SetMode([In] int dwFlags);

		// Token: 0x060002A3 RID: 675
		[PreserveSig]
		int GetMode(out int dwFlags);
	}
}
