using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001E0 RID: 480
	[NullableContext(1)]
	[Nullable(0)]
	public class JTokenEqualityComparer : IEqualityComparer<JToken>
	{
		// Token: 0x060013C3 RID: 5059 RVA: 0x0006907C File Offset: 0x0006727C
		public bool Equals(JToken x, JToken y)
		{
			return JToken.DeepEquals(x, y);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00069088 File Offset: 0x00067288
		public int GetHashCode(JToken obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetDeepHashCode();
		}
	}
}
