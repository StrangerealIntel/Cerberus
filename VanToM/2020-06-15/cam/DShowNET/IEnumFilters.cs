using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200005F RID: 95
	[ComVisible(true)]
	[Guid("56a86893-0ad4-11ce-b03a-0020af0ba770")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumFilters
	{
		// Token: 0x06000265 RID: 613
		[PreserveSig]
		int Next([In] uint cFilters, ref IBaseFilter x, out uint pcFetched);

		// Token: 0x06000266 RID: 614
		[PreserveSig]
		int Skip([In] int cFilters);

		// Token: 0x06000267 RID: 615
		void Reset();

		// Token: 0x06000268 RID: 616
		void Clone(out IEnumFilters ppEnum);
	}
}
