using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000062 RID: 98
	[ComVisible(false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
	public struct PinInfo
	{
		// Token: 0x040001E9 RID: 489
		public IBaseFilter filter;

		// Token: 0x040001EA RID: 490
		public PinDirection dir;

		// Token: 0x040001EB RID: 491
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string name;
	}
}
