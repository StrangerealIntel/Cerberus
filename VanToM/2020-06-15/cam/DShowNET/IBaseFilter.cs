using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000059 RID: 89
	[Guid("56a86895-0ad4-11ce-b03a-0020af0ba770")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	[ComImport]
	public interface IBaseFilter
	{
		// Token: 0x06000243 RID: 579
		[PreserveSig]
		int GetClassID(out Guid pClassID);

		// Token: 0x06000244 RID: 580
		[PreserveSig]
		int Stop();

		// Token: 0x06000245 RID: 581
		[PreserveSig]
		int Pause();

		// Token: 0x06000246 RID: 582
		[PreserveSig]
		int Run(long tStart);

		// Token: 0x06000247 RID: 583
		[PreserveSig]
		int GetState(int dwMilliSecsTimeout, out int filtState);

		// Token: 0x06000248 RID: 584
		[PreserveSig]
		int SetSyncSource([In] IReferenceClock pClock);

		// Token: 0x06000249 RID: 585
		[PreserveSig]
		int GetSyncSource(out IReferenceClock pClock);

		// Token: 0x0600024A RID: 586
		[PreserveSig]
		int EnumPins(out IEnumPins ppEnum);

		// Token: 0x0600024B RID: 587
		[PreserveSig]
		int FindPin([MarshalAs(UnmanagedType.LPWStr)] [In] string Id, out IPin ppPin);

		// Token: 0x0600024C RID: 588
		[PreserveSig]
		int QueryFilterInfo([Out] FilterInfo pInfo);

		// Token: 0x0600024D RID: 589
		[PreserveSig]
		int JoinFilterGraph([In] IFilterGraph pGraph, [MarshalAs(UnmanagedType.LPWStr)] [In] string pName);

		// Token: 0x0600024E RID: 590
		[PreserveSig]
		int QueryVendorInfo([MarshalAs(UnmanagedType.LPWStr)] out string pVendorInfo);
	}
}
