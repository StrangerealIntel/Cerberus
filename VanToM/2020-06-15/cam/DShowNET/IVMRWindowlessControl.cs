using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000094 RID: 148
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0eb1088c-4dcd-46f0-878f-39dae86a51b7")]
	[ComVisible(true)]
	[ComImport]
	public interface IVMRWindowlessControl
	{
		// Token: 0x06000323 RID: 803
		int GetNativeVideoSize(out int lpWidth, out int lpHeight, out int lpARWidth, out int lpARHeight);

		// Token: 0x06000324 RID: 804
		int GetMinIdealVideoSize(out int lpHeight);

		// Token: 0x06000325 RID: 805
		int GetMaxIdealVideoSize(out int lpWidth, out int lpHeight);

		// Token: 0x06000326 RID: 806
		int SetVideoPosition([MarshalAs(UnmanagedType.LPStruct)] [In] RECT lpSRCRect, [MarshalAs(UnmanagedType.LPStruct)] [In] RECT lpDSTRect);

		// Token: 0x06000327 RID: 807
		int GetVideoPosition([MarshalAs(UnmanagedType.LPStruct)] out RECT lpSRCRect, [MarshalAs(UnmanagedType.LPStruct)] out RECT lpDSTRect);

		// Token: 0x06000328 RID: 808
		int GetAspectRatioMode(out uint lpAspectRatioMode);

		// Token: 0x06000329 RID: 809
		int SetAspectRatioMode([In] uint AspectRatioMode);

		// Token: 0x0600032A RID: 810
		int SetVideoClippingWindow([In] IntPtr hwnd);

		// Token: 0x0600032B RID: 811
		int RepaintVideo([In] IntPtr hwnd, [In] IntPtr hdc);

		// Token: 0x0600032C RID: 812
		int DisplayModeChanged();

		// Token: 0x0600032D RID: 813
		int GetCurrentImage(out IntPtr lpDib);

		// Token: 0x0600032E RID: 814
		int SetBorderColor([In] uint Clr);

		// Token: 0x0600032F RID: 815
		int GetBorderColor(out uint lpClr);

		// Token: 0x06000330 RID: 816
		int SetColorKey([In] uint Clr);

		// Token: 0x06000331 RID: 817
		int GetColorKey(out uint lpClr);
	}
}
