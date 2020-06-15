using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000085 RID: 133
	[ComVisible(false)]
	[StructLayout(LayoutKind.Sequential)]
	public class DsOptInt64
	{
		// Token: 0x060002FF RID: 767 RVA: 0x00012EB8 File Offset: 0x000110B8
		public DsOptInt64(long Value)
		{
			this.Value = Value;
		}

		// Token: 0x0400024E RID: 590
		public long Value;
	}
}
