using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001EA RID: 490
	internal class ArraySliceFilter : PathFilter
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0006AEB0 File Offset: 0x000690B0
		// (set) Token: 0x06001442 RID: 5186 RVA: 0x0006AEB8 File Offset: 0x000690B8
		public int? Start { get; set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0006AEC4 File Offset: 0x000690C4
		// (set) Token: 0x06001444 RID: 5188 RVA: 0x0006AECC File Offset: 0x000690CC
		public int? End { get; set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0006AED8 File Offset: 0x000690D8
		// (set) Token: 0x06001446 RID: 5190 RVA: 0x0006AEE0 File Offset: 0x000690E0
		public int? Step { get; set; }

		// Token: 0x06001447 RID: 5191 RVA: 0x0006AEEC File Offset: 0x000690EC
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			int? num = this.Step;
			int num2 = 0;
			if (num.GetValueOrDefault() == num2 & num != null)
			{
				throw new JsonException("Step cannot be zero.");
			}
			foreach (JToken jtoken in current)
			{
				JArray a = jtoken as JArray;
				if (a != null)
				{
					int stepCount = this.Step ?? 1;
					int num3 = this.Start ?? ((stepCount > 0) ? 0 : (a.Count - 1));
					int stopIndex = this.End ?? ((stepCount > 0) ? a.Count : -1);
					num = this.Start;
					num2 = 0;
					if (num.GetValueOrDefault() < num2 & num != null)
					{
						num3 = a.Count + num3;
					}
					num = this.End;
					num2 = 0;
					if (num.GetValueOrDefault() < num2 & num != null)
					{
						stopIndex = a.Count + stopIndex;
					}
					num3 = Math.Max(num3, (stepCount > 0) ? 0 : int.MinValue);
					num3 = Math.Min(num3, (stepCount > 0) ? a.Count : (a.Count - 1));
					stopIndex = Math.Max(stopIndex, -1);
					stopIndex = Math.Min(stopIndex, a.Count);
					bool positiveStep = stepCount > 0;
					if (this.IsValid(num3, stopIndex, positiveStep))
					{
						int i = num3;
						while (this.IsValid(i, stopIndex, positiveStep))
						{
							yield return a[i];
							i += stepCount;
						}
					}
					else if (errorWhenNoMatch)
					{
						throw new JsonException("Array slice of {0} to {1} returned no results.".FormatWith(CultureInfo.InvariantCulture, (this.Start != null) ? this.Start.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*", (this.End != null) ? this.End.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*"));
					}
				}
				else if (errorWhenNoMatch)
				{
					throw new JsonException("Array slice is not valid on {0}.".FormatWith(CultureInfo.InvariantCulture, jtoken.GetType().Name));
				}
				a = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0006AF0C File Offset: 0x0006910C
		private bool IsValid(int index, int stopIndex, bool positiveStep)
		{
			if (positiveStep)
			{
				return index < stopIndex;
			}
			return index > stopIndex;
		}
	}
}
