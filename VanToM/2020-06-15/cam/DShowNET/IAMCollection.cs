using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200004F RID: 79
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("56a868b9-0ad4-11ce-b03a-0020af0ba770")]
	[ComVisible(true)]
	[ComImport]
	public interface IAMCollection
	{
		// Token: 0x0600021E RID: 542
		[PreserveSig]
		int get_Count(ref int plCount);

		// Token: 0x0600021F RID: 543
		[PreserveSig]
		int Item(int lItem, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);

		// Token: 0x06000220 RID: 544
		[PreserveSig]
		int get_NewEnum([MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);
	}
}
