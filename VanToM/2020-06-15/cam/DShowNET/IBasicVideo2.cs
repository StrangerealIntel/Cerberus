using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200004B RID: 75
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[ComVisible(true)]
	[Guid("329bb360-f6ea-11d1-9038-00a0c9697298")]
	[ComImport]
	public interface IBasicVideo2
	{
		// Token: 0x060001C7 RID: 455
		[PreserveSig]
		int AvgTimePerFrame(ref double pAvgTimePerFrame);

		// Token: 0x060001C8 RID: 456
		[PreserveSig]
		int BitRate(ref int pBitRate);

		// Token: 0x060001C9 RID: 457
		[PreserveSig]
		int BitErrorRate(ref int pBitRate);

		// Token: 0x060001CA RID: 458
		[PreserveSig]
		int VideoWidth(ref int pVideoWidth);

		// Token: 0x060001CB RID: 459
		[PreserveSig]
		int VideoHeight(ref int pVideoHeight);

		// Token: 0x060001CC RID: 460
		[PreserveSig]
		int put_SourceLeft(int SourceLeft);

		// Token: 0x060001CD RID: 461
		[PreserveSig]
		int get_SourceLeft(ref int pSourceLeft);

		// Token: 0x060001CE RID: 462
		[PreserveSig]
		int put_SourceWidth(int SourceWidth);

		// Token: 0x060001CF RID: 463
		[PreserveSig]
		int get_SourceWidth(ref int pSourceWidth);

		// Token: 0x060001D0 RID: 464
		[PreserveSig]
		int put_SourceTop(int SourceTop);

		// Token: 0x060001D1 RID: 465
		[PreserveSig]
		int get_SourceTop(ref int pSourceTop);

		// Token: 0x060001D2 RID: 466
		[PreserveSig]
		int put_SourceHeight(int SourceHeight);

		// Token: 0x060001D3 RID: 467
		[PreserveSig]
		int get_SourceHeight(ref int pSourceHeight);

		// Token: 0x060001D4 RID: 468
		[PreserveSig]
		int put_DestinationLeft(int DestinationLeft);

		// Token: 0x060001D5 RID: 469
		[PreserveSig]
		int get_DestinationLeft(ref int pDestinationLeft);

		// Token: 0x060001D6 RID: 470
		[PreserveSig]
		int put_DestinationWidth(int DestinationWidth);

		// Token: 0x060001D7 RID: 471
		[PreserveSig]
		int get_DestinationWidth(ref int pDestinationWidth);

		// Token: 0x060001D8 RID: 472
		[PreserveSig]
		int put_DestinationTop(int DestinationTop);

		// Token: 0x060001D9 RID: 473
		[PreserveSig]
		int get_DestinationTop(ref int pDestinationTop);

		// Token: 0x060001DA RID: 474
		[PreserveSig]
		int put_DestinationHeight(int DestinationHeight);

		// Token: 0x060001DB RID: 475
		[PreserveSig]
		int get_DestinationHeight(ref int pDestinationHeight);

		// Token: 0x060001DC RID: 476
		[PreserveSig]
		int SetSourcePosition(int left, int top, int width, int height);

		// Token: 0x060001DD RID: 477
		[PreserveSig]
		int GetSourcePosition(ref int left, ref int top, ref int width, ref int height);

		// Token: 0x060001DE RID: 478
		[PreserveSig]
		int SetDefaultSourcePosition();

		// Token: 0x060001DF RID: 479
		[PreserveSig]
		int SetDestinationPosition(int left, int top, int width, int height);

		// Token: 0x060001E0 RID: 480
		[PreserveSig]
		int GetDestinationPosition(ref int left, ref int top, ref int width, ref int height);

		// Token: 0x060001E1 RID: 481
		[PreserveSig]
		int SetDefaultDestinationPosition();

		// Token: 0x060001E2 RID: 482
		[PreserveSig]
		int GetVideoSize(ref int pWidth, ref int pHeight);

		// Token: 0x060001E3 RID: 483
		[PreserveSig]
		int GetVideoPaletteEntries(int StartIndex, int Entries, ref int pRetrieved, IntPtr pPalette);

		// Token: 0x060001E4 RID: 484
		[PreserveSig]
		int GetCurrentImage(ref int pBufferSize, IntPtr pDIBImage);

		// Token: 0x060001E5 RID: 485
		[PreserveSig]
		int IsUsingDefaultSource();

		// Token: 0x060001E6 RID: 486
		[PreserveSig]
		int IsUsingDefaultDestination();

		// Token: 0x060001E7 RID: 487
		[PreserveSig]
		int GetPreferredAspectRatio(ref int plAspectX, ref int plAspectY);
	}
}
