using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000060 RID: 96
	[Guid("56a86892-0ad4-11ce-b03a-0020af0ba770")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumPins
	{
		// Token: 0x06000269 RID: 617
		[PreserveSig]
		int Next([In] int cPins, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] IPin[] ppPins, out int pcFetched);

		// Token: 0x0600026A RID: 618
		[PreserveSig]
		int Skip([In] int cPins);

		// Token: 0x0600026B RID: 619
		void Reset();

		// Token: 0x0600026C RID: 620
		void Clone(out IEnumPins ppEnum);
	}
}
