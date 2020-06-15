using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200005A RID: 90
	[ComVisible(false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public class FilterInfo
	{
		// Token: 0x040001CA RID: 458
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string achName;

		// Token: 0x040001CB RID: 459
		[MarshalAs(UnmanagedType.IUnknown)]
		public object pUnk;
	}
}
