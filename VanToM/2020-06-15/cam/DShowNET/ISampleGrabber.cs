using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200009B RID: 155
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("6B652FFF-11FE-4fce-92AD-0266B5D7C78F")]
	[ComVisible(true)]
	[ComImport]
	public interface ISampleGrabber
	{
		// Token: 0x06000351 RID: 849
		[PreserveSig]
		int SetOneShot([MarshalAs(UnmanagedType.Bool)] [In] bool OneShot);

		// Token: 0x06000352 RID: 850
		[PreserveSig]
		int SetMediaType([MarshalAs(UnmanagedType.LPStruct)] [In] AMMediaType pmt);

		// Token: 0x06000353 RID: 851
		[PreserveSig]
		int GetConnectedMediaType([MarshalAs(UnmanagedType.LPStruct)] [Out] AMMediaType pmt);

		// Token: 0x06000354 RID: 852
		[PreserveSig]
		int SetBufferSamples([MarshalAs(UnmanagedType.Bool)] [In] bool BufferThem);

		// Token: 0x06000355 RID: 853
		[PreserveSig]
		int GetCurrentBuffer(ref int pBufferSize, IntPtr pBuffer);

		// Token: 0x06000356 RID: 854
		[PreserveSig]
		int GetCurrentSample(IntPtr ppSample);

		// Token: 0x06000357 RID: 855
		[PreserveSig]
		int SetCallback(ISampleGrabberCB pCallback, int WhichMethodToCallback);
	}
}
