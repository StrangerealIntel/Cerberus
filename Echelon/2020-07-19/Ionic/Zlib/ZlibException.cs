using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x020000D0 RID: 208
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000E")]
	[ComVisible(true)]
	public class ZlibException : Exception
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x000333E0 File Offset: 0x000315E0
		public ZlibException()
		{
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000333E8 File Offset: 0x000315E8
		public ZlibException(string s) : base(s)
		{
		}
	}
}
