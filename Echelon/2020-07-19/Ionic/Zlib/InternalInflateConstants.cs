using System;

namespace Ionic.Zlib
{
	// Token: 0x020000C5 RID: 197
	internal static class InternalInflateConstants
	{
		// Token: 0x040003B5 RID: 949
		internal static readonly int[] InflateMask = new int[]
		{
			0,
			1,
			3,
			7,
			15,
			31,
			63,
			127,
			255,
			511,
			1023,
			2047,
			4095,
			8191,
			16383,
			32767,
			65535
		};
	}
}
