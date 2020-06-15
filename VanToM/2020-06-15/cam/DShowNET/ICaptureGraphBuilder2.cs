using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000068 RID: 104
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	[Guid("93E5A4E0-2D50-11d2-ABFA-00A0C9C6E38D")]
	[ComImport]
	public interface ICaptureGraphBuilder2
	{
		// Token: 0x06000286 RID: 646
		[PreserveSig]
		int SetFiltergraph([In] IGraphBuilder pfg);

		// Token: 0x06000287 RID: 647
		[PreserveSig]
		int GetFiltergraph(out IGraphBuilder ppfg);

		// Token: 0x06000288 RID: 648
		[PreserveSig]
		int SetOutputFileName([In] ref Guid pType, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpstrFile, out IBaseFilter ppbf, out IFileSinkFilter ppSink);

		// Token: 0x06000289 RID: 649
		[PreserveSig]
		int FindInterface([In] ref Guid pCategory, [In] ref Guid pType, [In] IBaseFilter pbf, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppint);

		// Token: 0x0600028A RID: 650
		[PreserveSig]
		int RenderStream([In] ref Guid pCategory, [In] ref Guid pType, [MarshalAs(UnmanagedType.IUnknown)] [In] object pSource, [In] IBaseFilter pfCompressor, [In] IBaseFilter pfRenderer);

		// Token: 0x0600028B RID: 651
		[PreserveSig]
		int ControlStream([In] ref Guid pCategory, [In] ref Guid pType, [In] IBaseFilter pFilter, [In] long pstart, [In] long pstop, [In] short wStartCookie, [In] short wStopCookie);

		// Token: 0x0600028C RID: 652
		[PreserveSig]
		int AllocCapFile([MarshalAs(UnmanagedType.LPWStr)] [In] string lpstrFile, [In] long dwlSize);

		// Token: 0x0600028D RID: 653
		[PreserveSig]
		int CopyCaptureFile([MarshalAs(UnmanagedType.LPWStr)] [In] string lpwstrOld, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwstrNew, [In] int fAllowEscAbort, [In] IAMCopyCaptureFileProgress pFilter);

		// Token: 0x0600028E RID: 654
		[PreserveSig]
		int FindPin([In] object pSource, [In] int pindir, [In] ref Guid pCategory, [In] ref Guid pType, [MarshalAs(UnmanagedType.Bool)] [In] bool fUnconnected, [In] int num, out IPin ppPin);
	}
}
