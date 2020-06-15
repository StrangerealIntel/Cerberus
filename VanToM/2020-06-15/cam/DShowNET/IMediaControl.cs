using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000048 RID: 72
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("56a868b1-0ad4-11ce-b03a-0020af0ba770")]
	[ComVisible(true)]
	[ComImport]
	public interface IMediaControl
	{
		// Token: 0x060001AF RID: 431
		[PreserveSig]
		int Run();

		// Token: 0x060001B0 RID: 432
		[PreserveSig]
		int Pause();

		// Token: 0x060001B1 RID: 433
		[PreserveSig]
		int Stop();

		// Token: 0x060001B2 RID: 434
		[PreserveSig]
		int GetState(int msTimeout, ref int pfs);

		// Token: 0x060001B3 RID: 435
		[PreserveSig]
		int RenderFile(string strFilename);

		// Token: 0x060001B4 RID: 436
		[PreserveSig]
		int AddSourceFilter([In] string strFilename, [MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);

		// Token: 0x060001B5 RID: 437
		[PreserveSig]
		int get_FilterCollection([MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);

		// Token: 0x060001B6 RID: 438
		[PreserveSig]
		int get_RegFilterCollection([MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);

		// Token: 0x060001B7 RID: 439
		[PreserveSig]
		int StopWhenReady();
	}
}
