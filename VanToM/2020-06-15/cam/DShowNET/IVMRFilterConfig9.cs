using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000090 RID: 144
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	[Guid("5a804648-4f66-4867-9c43-4f5c822cf1b8")]
	[ComImport]
	public interface IVMRFilterConfig9
	{
		// Token: 0x0600030F RID: 783
		[PreserveSig]
		int SetImageCompositor([In] IntPtr lpVMRImgCompositor);

		// Token: 0x06000310 RID: 784
		[PreserveSig]
		int SetNumberOfStreams([In] uint dwMaxStreams);

		// Token: 0x06000311 RID: 785
		[PreserveSig]
		int GetNumberOfStreams(out uint pdwMaxStreams);

		// Token: 0x06000312 RID: 786
		[PreserveSig]
		int SetRenderingPrefs([In] uint dwRenderFlags);

		// Token: 0x06000313 RID: 787
		[PreserveSig]
		int GetRenderingPrefs(out uint pdwRenderFlags);

		// Token: 0x06000314 RID: 788
		[PreserveSig]
		int SetRenderingMode([In] VMRMode9 Mode);

		// Token: 0x06000315 RID: 789
		[PreserveSig]
		int GetRenderingMode(out VMRMode9 Mode);
	}
}
