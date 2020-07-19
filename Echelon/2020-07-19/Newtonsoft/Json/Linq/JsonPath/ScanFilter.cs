using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001F6 RID: 502
	[NullableContext(2)]
	[Nullable(0)]
	internal class ScanFilter : PathFilter
	{
		// Token: 0x0600147D RID: 5245 RVA: 0x0006CAC0 File Offset: 0x0006ACC0
		public ScanFilter(string name)
		{
			this.Name = name;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0006CAD0 File Offset: 0x0006ACD0
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken c in current)
			{
				if (this.Name == null)
				{
					yield return c;
				}
				JToken value = c;
				for (;;)
				{
					JContainer container = value as JContainer;
					value = PathFilter.GetNextScanValue(c, container, value);
					if (value == null)
					{
						break;
					}
					JProperty jproperty = value as JProperty;
					if (jproperty != null)
					{
						if (jproperty.Name == this.Name)
						{
							yield return jproperty.Value;
						}
					}
					else if (this.Name == null)
					{
						yield return value;
					}
				}
				value = null;
				c = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0400093F RID: 2367
		internal string Name;
	}
}
