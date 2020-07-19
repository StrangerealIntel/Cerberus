using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001F1 RID: 497
	[NullableContext(1)]
	[Nullable(0)]
	internal class CompositeExpression : QueryExpression
	{
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0006C480 File Offset: 0x0006A680
		// (set) Token: 0x0600146C RID: 5228 RVA: 0x0006C488 File Offset: 0x0006A688
		public List<QueryExpression> Expressions { get; set; }

		// Token: 0x0600146D RID: 5229 RVA: 0x0006C494 File Offset: 0x0006A694
		public CompositeExpression(QueryOperator @operator) : base(@operator)
		{
			this.Expressions = new List<QueryExpression>();
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0006C4A8 File Offset: 0x0006A6A8
		public override bool IsMatch(JToken root, JToken t)
		{
			QueryOperator @operator = this.Operator;
			if (@operator == QueryOperator.And)
			{
				using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsMatch(root, t))
						{
							return false;
						}
					}
				}
				return true;
			}
			if (@operator != QueryOperator.Or)
			{
				throw new ArgumentOutOfRangeException();
			}
			using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsMatch(root, t))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
