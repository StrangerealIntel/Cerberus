using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001F7 RID: 503
	[NullableContext(1)]
	[Nullable(0)]
	internal class ScanMultipleFilter : PathFilter
	{
		// Token: 0x0600147F RID: 5247 RVA: 0x0006CAE8 File Offset: 0x0006ACE8
		public ScanMultipleFilter(List<string> names)
		{
			this._names = names;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0006CAF8 File Offset: 0x0006ACF8
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken c in current)
			{
				JToken value = c;
				for (;;)
				{
					JContainer container = value as JContainer;
					value = PathFilter.GetNextScanValue(c, container, value);
					if (value == null)
					{
						break;
					}
					JProperty property = value as JProperty;
					if (property != null)
					{
						foreach (string b in this._names)
						{
							if (property.Name == b)
							{
								yield return property.Value;
							}
						}
						List<string>.Enumerator enumerator2 = default(List<string>.Enumerator);
					}
					property = null;
				}
				value = null;
				c = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000940 RID: 2368
		private List<string> _names;
	}
}
