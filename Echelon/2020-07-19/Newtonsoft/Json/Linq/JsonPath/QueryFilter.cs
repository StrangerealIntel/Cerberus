using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001F3 RID: 499
	[NullableContext(1)]
	[Nullable(0)]
	internal class QueryFilter : PathFilter
	{
		// Token: 0x06001476 RID: 5238 RVA: 0x0006CA40 File Offset: 0x0006AC40
		public QueryFilter(QueryExpression expression)
		{
			this.Expression = expression;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0006CA50 File Offset: 0x0006AC50
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken jtoken in current)
			{
				foreach (JToken jtoken2 in ((IEnumerable<JToken>)jtoken))
				{
					if (this.Expression.IsMatch(root, jtoken2))
					{
						yield return jtoken2;
					}
				}
				IEnumerator<JToken> enumerator2 = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0400093C RID: 2364
		internal QueryExpression Expression;
	}
}
