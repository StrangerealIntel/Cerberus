using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000055 RID: 85
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("56a8689f-0ad4-11ce-b03a-0020af0ba770")]
	[ComVisible(true)]
	[ComImport]
	public interface IFilterGraph
	{
		// Token: 0x06000232 RID: 562
		[PreserveSig]
		int AddFilter([In] IBaseFilter pFilter, [MarshalAs(UnmanagedType.LPWStr)] [In] string pName);

		// Token: 0x06000233 RID: 563
		[PreserveSig]
		int RemoveFilter([In] IBaseFilter pFilter);

		// Token: 0x06000234 RID: 564
		[PreserveSig]
		int EnumFilters(out IEnumFilters ppEnum);

		// Token: 0x06000235 RID: 565
		[PreserveSig]
		int FindFilterByName([MarshalAs(UnmanagedType.LPWStr)] [In] string pName, out IBaseFilter ppFilter);

		// Token: 0x06000236 RID: 566
		[PreserveSig]
		int ConnectDirect([In] IPin ppinOut, [In] IPin ppinIn, [MarshalAs(UnmanagedType.LPStruct)] [In] AMMediaType pmt);

		// Token: 0x06000237 RID: 567
		[PreserveSig]
		int Reconnect([In] IPin ppin);

		// Token: 0x06000238 RID: 568
		[PreserveSig]
		int Disconnect([In] IPin ppin);

		// Token: 0x06000239 RID: 569
		[PreserveSig]
		int SetDefaultSyncSource();
	}
}
