using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000095 RID: 149
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9e5530c5-7034-48b4-bb46-0b8a6efc8e36")]
	[ComImport]
	public interface IVMRFilterConfig
	{
		// Token: 0x06000332 RID: 818
		[PreserveSig]
		int SetImageCompositor([In] IntPtr lpVMRImgCompositor);

		// Token: 0x06000333 RID: 819
		[PreserveSig]
		int SetNumberOfStreams([In] uint dwMaxStreams);

		// Token: 0x06000334 RID: 820
		[PreserveSig]
		int GetNumberOfStreams(out uint pdwMaxStreams);

		// Token: 0x06000335 RID: 821
		[PreserveSig]
		int SetRenderingPrefs([In] uint dwRenderFlags);

		// Token: 0x06000336 RID: 822
		[PreserveSig]
		int GetRenderingPrefs(out uint pdwRenderFlags);

		// Token: 0x06000337 RID: 823
		[PreserveSig]
		int SetRenderingMode([In] uint Mode);

		// Token: 0x06000338 RID: 824
		[PreserveSig]
		int GetRenderingMode(out VMRMode Mode);
	}
}
