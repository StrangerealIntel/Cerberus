using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001E8 RID: 488
	internal class ArrayIndexFilter : PathFilter
	{
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0006AE44 File Offset: 0x00069044
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x0006AE4C File Offset: 0x0006904C
		public int? Index { get; set; }

		// Token: 0x0600143D RID: 5181 RVA: 0x0006AE58 File Offset: 0x00069058
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken jtoken in current)
			{
				if (this.Index != null)
				{
					JToken tokenIndex = PathFilter.GetTokenIndex(jtoken, errorWhenNoMatch, this.Index.GetValueOrDefault());
					if (tokenIndex != null)
					{
						yield return tokenIndex;
					}
				}
				else if (jtoken is JArray || jtoken is JConstructor)
				{
					foreach (JToken jtoken2 in ((IEnumerable<JToken>)jtoken))
					{
						yield return jtoken2;
					}
					IEnumerator<JToken> enumerator2 = null;
				}
				else if (errorWhenNoMatch)
				{
					throw new JsonException("Index * not valid on {0}.".FormatWith(CultureInfo.InvariantCulture, jtoken.GetType().Name));
				}
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}
	}
}
