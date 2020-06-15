using System;
using System.Runtime.InteropServices;
using System.Text;

namespace cam.DShowNET
{
	// Token: 0x02000053 RID: 83
	[ComVisible(false)]
	public class DsHlp
	{
		// Token: 0x06000222 RID: 546
		[DllImport("quartz.dll", CharSet = CharSet.Auto)]
		public static extern int AMGetErrorText(int hr, StringBuilder buf, int max);

		// Token: 0x040001C8 RID: 456
		public const int OATRUE = -1;

		// Token: 0x040001C9 RID: 457
		public const int OAFALSE = 0;
	}
}
