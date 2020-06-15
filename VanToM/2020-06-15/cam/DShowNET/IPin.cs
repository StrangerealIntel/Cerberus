using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000054 RID: 84
	[ComVisible(true)]
	[Guid("56a86891-0ad4-11ce-b03a-0020af0ba770")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IPin
	{
		// Token: 0x06000223 RID: 547
		[PreserveSig]
		int Connect([In] IPin pReceivePin, [MarshalAs(UnmanagedType.LPStruct)] [In] AMMediaType pmt);

		// Token: 0x06000224 RID: 548
		[PreserveSig]
		int ReceiveConnection([In] IPin pReceivePin, [MarshalAs(UnmanagedType.LPStruct)] [In] AMMediaType pmt);

		// Token: 0x06000225 RID: 549
		[PreserveSig]
		int Disconnect();

		// Token: 0x06000226 RID: 550
		[PreserveSig]
		int ConnectedTo(out IPin ppPin);

		// Token: 0x06000227 RID: 551
		[PreserveSig]
		int ConnectionMediaType([MarshalAs(UnmanagedType.LPStruct)] [Out] AMMediaType pmt);

		// Token: 0x06000228 RID: 552
		[PreserveSig]
		int QueryPinInfo(out PinInfo pInfo);

		// Token: 0x06000229 RID: 553
		[PreserveSig]
		int QueryDirection(ref PinDirection pPinDir);

		// Token: 0x0600022A RID: 554
		[PreserveSig]
		int QueryId([MarshalAs(UnmanagedType.LPWStr)] out string Id);

		// Token: 0x0600022B RID: 555
		[PreserveSig]
		int QueryAccept([MarshalAs(UnmanagedType.LPStruct)] [In] AMMediaType pmt);

		// Token: 0x0600022C RID: 556
		[PreserveSig]
		int EnumMediaTypes(IntPtr ppEnum);

		// Token: 0x0600022D RID: 557
		[PreserveSig]
		int QueryInternalConnections(IntPtr apPin, [In] [Out] ref int nPin);

		// Token: 0x0600022E RID: 558
		[PreserveSig]
		int EndOfStream();

		// Token: 0x0600022F RID: 559
		[PreserveSig]
		int BeginFlush();

		// Token: 0x06000230 RID: 560
		[PreserveSig]
		int EndFlush();

		// Token: 0x06000231 RID: 561
		[PreserveSig]
		int NewSegment(long tStart, long tStop, double dRate);
	}
}
