using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200009C RID: 156
	[Guid("0579154A-2B53-4994-B0D0-E773148EFF85")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	[ComImport]
	public interface ISampleGrabberCB
	{
		// Token: 0x06000358 RID: 856
		[PreserveSig]
		int SampleCB(double SampleTime, IMediaSample pSample);

		// Token: 0x06000359 RID: 857
		[PreserveSig]
		int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen);
	}
}
