using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200004C RID: 76
	[ComVisible(true)]
	[Guid("56a868b4-0ad4-11ce-b03a-0020af0ba770")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[ComImport]
	public interface IVideoWindow
	{
		// Token: 0x060001E8 RID: 488
		[PreserveSig]
		int put_Caption(string caption);

		// Token: 0x060001E9 RID: 489
		[PreserveSig]
		int get_Caption(out string caption);

		// Token: 0x060001EA RID: 490
		[PreserveSig]
		int put_WindowStyle(int windowStyle);

		// Token: 0x060001EB RID: 491
		[PreserveSig]
		int get_WindowStyle(ref int windowStyle);

		// Token: 0x060001EC RID: 492
		[PreserveSig]
		int put_WindowStyleEx(int windowStyleEx);

		// Token: 0x060001ED RID: 493
		[PreserveSig]
		int get_WindowStyleEx(ref int windowStyleEx);

		// Token: 0x060001EE RID: 494
		[PreserveSig]
		int put_AutoShow(int autoShow);

		// Token: 0x060001EF RID: 495
		[PreserveSig]
		int get_AutoShow(ref int autoShow);

		// Token: 0x060001F0 RID: 496
		[PreserveSig]
		int put_WindowState(int windowState);

		// Token: 0x060001F1 RID: 497
		[PreserveSig]
		int get_WindowState(ref int windowState);

		// Token: 0x060001F2 RID: 498
		[PreserveSig]
		int put_BackgroundPalette(int backgroundPalette);

		// Token: 0x060001F3 RID: 499
		[PreserveSig]
		int get_BackgroundPalette(ref int backgroundPalette);

		// Token: 0x060001F4 RID: 500
		[PreserveSig]
		int put_Visible(int visible);

		// Token: 0x060001F5 RID: 501
		[PreserveSig]
		int get_Visible(ref int visible);

		// Token: 0x060001F6 RID: 502
		[PreserveSig]
		int put_Left(int left);

		// Token: 0x060001F7 RID: 503
		[PreserveSig]
		int get_Left(ref int left);

		// Token: 0x060001F8 RID: 504
		[PreserveSig]
		int put_Width(int width);

		// Token: 0x060001F9 RID: 505
		[PreserveSig]
		int get_Width(ref int width);

		// Token: 0x060001FA RID: 506
		[PreserveSig]
		int put_Top(int top);

		// Token: 0x060001FB RID: 507
		[PreserveSig]
		int get_Top(ref int top);

		// Token: 0x060001FC RID: 508
		[PreserveSig]
		int put_Height(int height);

		// Token: 0x060001FD RID: 509
		[PreserveSig]
		int get_Height(ref int height);

		// Token: 0x060001FE RID: 510
		[PreserveSig]
		int put_Owner(IntPtr owner);

		// Token: 0x060001FF RID: 511
		[PreserveSig]
		int get_Owner(ref IntPtr owner);

		// Token: 0x06000200 RID: 512
		[PreserveSig]
		int put_MessageDrain(IntPtr drain);

		// Token: 0x06000201 RID: 513
		[PreserveSig]
		int get_MessageDrain(ref IntPtr drain);

		// Token: 0x06000202 RID: 514
		[PreserveSig]
		int get_BorderColor(ref int color);

		// Token: 0x06000203 RID: 515
		[PreserveSig]
		int put_BorderColor(int color);

		// Token: 0x06000204 RID: 516
		[PreserveSig]
		int get_FullScreenMode(ref int fullScreenMode);

		// Token: 0x06000205 RID: 517
		[PreserveSig]
		int put_FullScreenMode(int fullScreenMode);

		// Token: 0x06000206 RID: 518
		[PreserveSig]
		int SetWindowForeground(int focus);

		// Token: 0x06000207 RID: 519
		[PreserveSig]
		int NotifyOwnerMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000208 RID: 520
		[PreserveSig]
		int SetWindowPosition(int left, int top, int width, int height);

		// Token: 0x06000209 RID: 521
		[PreserveSig]
		int GetWindowPosition(ref int left, ref int top, ref int width, ref int height);

		// Token: 0x0600020A RID: 522
		[PreserveSig]
		int GetMinIdealImageSize(ref int width, ref int height);

		// Token: 0x0600020B RID: 523
		[PreserveSig]
		int GetMaxIdealImageSize(ref int width, ref int height);

		// Token: 0x0600020C RID: 524
		[PreserveSig]
		int GetRestorePosition(ref int left, ref int top, ref int width, ref int height);

		// Token: 0x0600020D RID: 525
		[PreserveSig]
		int HideCursor(int hideCursor__1);

		// Token: 0x0600020E RID: 526
		[PreserveSig]
		int IsCursorHidden(ref int hideCursor);
	}
}
