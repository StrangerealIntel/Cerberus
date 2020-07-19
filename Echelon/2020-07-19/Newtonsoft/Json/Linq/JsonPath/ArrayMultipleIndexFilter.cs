using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001E9 RID: 489
	[NullableContext(1)]
	[Nullable(0)]
	internal class ArrayMultipleIndexFilter : PathFilter
	{
		// Token: 0x0600143F RID: 5183 RVA: 0x0006AE80 File Offset: 0x00069080
		public ArrayMultipleIndexFilter(List<int> indexes)
		{
			this.Indexes = indexes;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0006AE90 File Offset: 0x00069090
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken t in current)
			{
				foreach (int index in this.Indexes)
				{
					JToken tokenIndex = PathFilter.GetTokenIndex(t, errorWhenNoMatch, index);
					if (tokenIndex != null)
					{
						yield return tokenIndex;
					}
				}
				List<int>.Enumerator enumerator2 = default(List<int>.Enumerator);
				t = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000920 RID: 2336
		internal List<int> Indexes;
	}
}
