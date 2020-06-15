using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200005B RID: 91
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("36b73880-c2c8-11cf-8b46-00805f6cef60")]
	[ComVisible(true)]
	[ComImport]
	public interface IMediaSeeking
	{
		// Token: 0x06000250 RID: 592
		[PreserveSig]
		int GetCapabilities(ref SeekingCapabilities pCapabilities);

		// Token: 0x06000251 RID: 593
		[PreserveSig]
		int CheckCapabilities([In] [Out] ref SeekingCapabilities pCapabilities);

		// Token: 0x06000252 RID: 594
		[PreserveSig]
		int IsFormatSupported([In] ref Guid pFormat);

		// Token: 0x06000253 RID: 595
		[PreserveSig]
		int QueryPreferredFormat(out Guid pFormat);

		// Token: 0x06000254 RID: 596
		[PreserveSig]
		int GetTimeFormat(out Guid pFormat);

		// Token: 0x06000255 RID: 597
		[PreserveSig]
		int IsUsingTimeFormat([In] ref Guid pFormat);

		// Token: 0x06000256 RID: 598
		[PreserveSig]
		int SetTimeFormat([In] ref Guid pFormat);

		// Token: 0x06000257 RID: 599
		[PreserveSig]
		int GetDuration(ref long pDuration);

		// Token: 0x06000258 RID: 600
		[PreserveSig]
		int GetStopPosition(ref long pStop);

		// Token: 0x06000259 RID: 601
		[PreserveSig]
		int GetCurrentPosition(ref long pCurrent);

		// Token: 0x0600025A RID: 602
		[PreserveSig]
		int ConvertTimeFormat(ref long pTarget, [In] ref Guid pTargetFormat, long Source, [In] ref Guid pSourceFormat);

		// Token: 0x0600025B RID: 603
		[PreserveSig]
		int SetPositions([MarshalAs(UnmanagedType.LPStruct)] [In] [Out] DsOptInt64 pCurrent, SeekingFlags dwCurrentFlags, [MarshalAs(UnmanagedType.LPStruct)] [In] [Out] DsOptInt64 pStop, SeekingFlags dwStopFlags);

		// Token: 0x0600025C RID: 604
		[PreserveSig]
		int GetPositions(ref long pCurrent, ref long pStop);

		// Token: 0x0600025D RID: 605
		[PreserveSig]
		int GetAvailable(ref long pEarliest, ref long pLatest);

		// Token: 0x0600025E RID: 606
		[PreserveSig]
		int SetRate(double dRate);

		// Token: 0x0600025F RID: 607
		[PreserveSig]
		int GetRate(ref double pdRate);

		// Token: 0x06000260 RID: 608
		[PreserveSig]
		int GetPreroll(ref long pllPreroll);
	}
}
