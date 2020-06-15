using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000091 RID: 145
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8f537d09-f85e-4414-b23b-502e54c79927")]
	[ComVisible(true)]
	[ComImport]
	public interface IVMRWindowlessControl9
	{
		// Token: 0x06000316 RID: 790
		int GetNativeVideoSize(out int lpWidth, out int lpHeight, out int lpARWidth, out int lpARHeight);

		// Token: 0x06000317 RID: 791
		int GetMinIdealVideoSize(out int lpHeight);

		// Token: 0x06000318 RID: 792
		int GetMaxIdealVideoSize(out int lpWidth, out int lpHeight);

		// Token: 0x06000319 RID: 793
		int SetVideoPosition([MarshalAs(UnmanagedType.LPStruct)] [In] RECT lpSRCRect, [MarshalAs(UnmanagedType.LPStruct)] [In] RECT lpDSTRect);

		// Token: 0x0600031A RID: 794
		int GetVideoPosition([MarshalAs(UnmanagedType.LPStruct)] out RECT lpSRCRect, [MarshalAs(UnmanagedType.LPStruct)] out RECT lpDSTRect);

		// Token: 0x0600031B RID: 795
		int GetAspectRatioMode(out VMR9AspectRatioMode lpAspectRatioMode);

		// Token: 0x0600031C RID: 796
		int SetAspectRatioMode([In] VMR9AspectRatioMode AspectRatioMode);

		// Token: 0x0600031D RID: 797
		int SetVideoClippingWindow([In] IntPtr hwnd);

		// Token: 0x0600031E RID: 798
		int RepaintVideo([In] IntPtr hwnd, [In] IntPtr hdc);

		// Token: 0x0600031F RID: 799
		int DisplayModeChanged();

		// Token: 0x06000320 RID: 800
		int GetCurrentImage(out IntPtr lpDib);

		// Token: 0x06000321 RID: 801
		int SetBorderColor([In] uint Clr);

		// Token: 0x06000322 RID: 802
		int GetBorderColor(out uint lpClr);
	}
}
