using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000063 RID: 99
	[Guid("56a8689a-0ad4-11ce-b03a-0020af0ba770")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	[ComImport]
	public interface IMediaSample
	{
		// Token: 0x0600026E RID: 622
		[PreserveSig]
		int GetPointer(ref IntPtr ppBuffer);

		// Token: 0x0600026F RID: 623
		[PreserveSig]
		int GetSize();

		// Token: 0x06000270 RID: 624
		[PreserveSig]
		int GetTime(ref long pTimeStart, ref long pTimeEnd);

		// Token: 0x06000271 RID: 625
		[PreserveSig]
		int SetTime([MarshalAs(UnmanagedType.LPStruct)] [In] DsOptInt64 pTimeStart, [MarshalAs(UnmanagedType.LPStruct)] [In] DsOptInt64 pTimeEnd);

		// Token: 0x06000272 RID: 626
		[PreserveSig]
		int IsSyncPoint();

		// Token: 0x06000273 RID: 627
		[PreserveSig]
		int SetSyncPoint([MarshalAs(UnmanagedType.Bool)] [In] bool bIsSyncPoint);

		// Token: 0x06000274 RID: 628
		[PreserveSig]
		int IsPreroll();

		// Token: 0x06000275 RID: 629
		[PreserveSig]
		int SetPreroll([MarshalAs(UnmanagedType.Bool)] [In] bool bIsPreroll);

		// Token: 0x06000276 RID: 630
		[PreserveSig]
		int GetActualDataLength();

		// Token: 0x06000277 RID: 631
		[PreserveSig]
		int SetActualDataLength(int len);

		// Token: 0x06000278 RID: 632
		[PreserveSig]
		int GetMediaType([MarshalAs(UnmanagedType.LPStruct)] out AMMediaType ppMediaType);

		// Token: 0x06000279 RID: 633
		[PreserveSig]
		int SetMediaType([MarshalAs(UnmanagedType.LPStruct)] [In] AMMediaType pMediaType);

		// Token: 0x0600027A RID: 634
		[PreserveSig]
		int IsDiscontinuity();

		// Token: 0x0600027B RID: 635
		[PreserveSig]
		int SetDiscontinuity([MarshalAs(UnmanagedType.Bool)] [In] bool bDiscontinuity);

		// Token: 0x0600027C RID: 636
		[PreserveSig]
		int GetMediaTime(ref long pTimeStart, ref long pTimeEnd);

		// Token: 0x0600027D RID: 637
		[PreserveSig]
		int SetMediaTime([MarshalAs(UnmanagedType.LPStruct)] [In] DsOptInt64 pTimeStart, [MarshalAs(UnmanagedType.LPStruct)] [In] DsOptInt64 pTimeEnd);
	}
}
