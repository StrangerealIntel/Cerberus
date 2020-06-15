using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000069 RID: 105
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	[Guid("56a868a9-0ad4-11ce-b03a-0020af0ba770")]
	[ComImport]
	public interface IGraphBuilder
	{
		// Token: 0x0600028F RID: 655
		[PreserveSig]
		int AddFilter([In] IBaseFilter pFilter, [MarshalAs(UnmanagedType.LPWStr)] [In] string pName);

		// Token: 0x06000290 RID: 656
		[PreserveSig]
		int RemoveFilter([In] IBaseFilter pFilter);

		// Token: 0x06000291 RID: 657
		[PreserveSig]
		int EnumFilters(out IEnumFilters ppEnum);

		// Token: 0x06000292 RID: 658
		[PreserveSig]
		int FindFilterByName([MarshalAs(UnmanagedType.LPWStr)] [In] string pName, out IBaseFilter ppFilter);

		// Token: 0x06000293 RID: 659
		[PreserveSig]
		int ConnectDirect([In] IPin ppinOut, [In] IPin ppinIn, [MarshalAs(UnmanagedType.LPStruct)] [In] AMMediaType pmt);

		// Token: 0x06000294 RID: 660
		[PreserveSig]
		int Reconnect([In] IPin ppin);

		// Token: 0x06000295 RID: 661
		[PreserveSig]
		int Disconnect([In] IPin ppin);

		// Token: 0x06000296 RID: 662
		[PreserveSig]
		int SetDefaultSyncSource();

		// Token: 0x06000297 RID: 663
		[PreserveSig]
		int Connect([In] IPin ppinOut, [In] IPin ppinIn);

		// Token: 0x06000298 RID: 664
		[PreserveSig]
		int Render([In] IPin ppinOut);

		// Token: 0x06000299 RID: 665
		[PreserveSig]
		int RenderFile([MarshalAs(UnmanagedType.LPWStr)] [In] string lpcwstrFile, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpcwstrPlayList);

		// Token: 0x0600029A RID: 666
		[PreserveSig]
		int AddSourceFilter([MarshalAs(UnmanagedType.LPWStr)] [In] string lpcwstrFileName, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpcwstrFilterName, out IBaseFilter ppFilter);

		// Token: 0x0600029B RID: 667
		[PreserveSig]
		int SetLogFile(IntPtr hFile);

		// Token: 0x0600029C RID: 668
		[PreserveSig]
		int Abort();

		// Token: 0x0600029D RID: 669
		[PreserveSig]
		int ShouldOperationContinue();
	}
}
