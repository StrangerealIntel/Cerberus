using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000083 RID: 131
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	[Guid("B196B28B-BAB4-101A-B69C-00AA00341D07")]
	[ComImport]
	public interface ISpecifyPropertyPages
	{
		// Token: 0x060002FE RID: 766
		[PreserveSig]
		int GetPages(ref DsCAUUID pPages);
	}
}
