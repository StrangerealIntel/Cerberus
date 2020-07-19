using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000171 RID: 369
	[NullableContext(1)]
	[Nullable(0)]
	internal class EnumInfo
	{
		// Token: 0x06000D91 RID: 3473 RVA: 0x0004EC50 File Offset: 0x0004CE50
		public EnumInfo(bool isFlags, ulong[] values, string[] names, string[] resolvedNames)
		{
			this.IsFlags = isFlags;
			this.Values = values;
			this.Names = names;
			this.ResolvedNames = resolvedNames;
		}

		// Token: 0x04000735 RID: 1845
		public readonly bool IsFlags;

		// Token: 0x04000736 RID: 1846
		public readonly ulong[] Values;

		// Token: 0x04000737 RID: 1847
		public readonly string[] Names;

		// Token: 0x04000738 RID: 1848
		public readonly string[] ResolvedNames;
	}
}
