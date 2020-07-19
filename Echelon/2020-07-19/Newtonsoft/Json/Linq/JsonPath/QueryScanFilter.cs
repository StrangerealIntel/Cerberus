using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001F4 RID: 500
	[NullableContext(1)]
	[Nullable(0)]
	internal class QueryScanFilter : PathFilter
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x0006CA70 File Offset: 0x0006AC70
		public QueryScanFilter(QueryExpression expression)
		{
			this.Expression = expression;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0006CA80 File Offset: 0x0006AC80
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken jtoken in current)
			{
				JContainer jcontainer = jtoken as JContainer;
				if (jcontainer != null)
				{
					foreach (JToken jtoken2 in jcontainer.DescendantsAndSelf())
					{
						if (this.Expression.IsMatch(root, jtoken2))
						{
							yield return jtoken2;
						}
					}
					IEnumerator<JToken> enumerator2 = null;
				}
				else if (this.Expression.IsMatch(root, jtoken))
				{
					yield return jtoken;
				}
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0400093D RID: 2365
		internal QueryExpression Expression;
	}
}
