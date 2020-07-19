using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001F0 RID: 496
	internal abstract class QueryExpression
	{
		// Token: 0x06001469 RID: 5225 RVA: 0x0006C470 File Offset: 0x0006A670
		public QueryExpression(QueryOperator @operator)
		{
			this.Operator = @operator;
		}

		// Token: 0x0600146A RID: 5226
		[NullableContext(1)]
		public abstract bool IsMatch(JToken root, JToken t);

		// Token: 0x04000938 RID: 2360
		internal QueryOperator Operator;
	}
}
