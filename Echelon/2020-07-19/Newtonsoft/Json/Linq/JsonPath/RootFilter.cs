using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001F5 RID: 501
	[NullableContext(1)]
	[Nullable(0)]
	internal class RootFilter : PathFilter
	{
		// Token: 0x0600147A RID: 5242 RVA: 0x0006CAA0 File Offset: 0x0006ACA0
		private RootFilter()
		{
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0006CAA8 File Offset: 0x0006ACA8
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			return new JToken[]
			{
				root
			};
		}

		// Token: 0x0400093E RID: 2366
		public static readonly RootFilter Instance = new RootFilter();
	}
}
