using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001DE RID: 478
	public class JsonMergeSettings
	{
		// Token: 0x06001325 RID: 4901 RVA: 0x0006661C File Offset: 0x0006481C
		public JsonMergeSettings()
		{
			this._propertyNameComparison = StringComparison.Ordinal;
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0006662C File Offset: 0x0006482C
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x00066634 File Offset: 0x00064834
		public MergeArrayHandling MergeArrayHandling
		{
			get
			{
				return this._mergeArrayHandling;
			}
			set
			{
				if (value < MergeArrayHandling.Concat || value > MergeArrayHandling.Merge)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._mergeArrayHandling = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x00066658 File Offset: 0x00064858
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x00066660 File Offset: 0x00064860
		public MergeNullValueHandling MergeNullValueHandling
		{
			get
			{
				return this._mergeNullValueHandling;
			}
			set
			{
				if (value < MergeNullValueHandling.Ignore || value > MergeNullValueHandling.Merge)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._mergeNullValueHandling = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x00066684 File Offset: 0x00064884
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x0006668C File Offset: 0x0006488C
		public StringComparison PropertyNameComparison
		{
			get
			{
				return this._propertyNameComparison;
			}
			set
			{
				if (value < StringComparison.CurrentCulture || value > StringComparison.OrdinalIgnoreCase)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._propertyNameComparison = value;
			}
		}

		// Token: 0x040008E5 RID: 2277
		private MergeArrayHandling _mergeArrayHandling;

		// Token: 0x040008E6 RID: 2278
		private MergeNullValueHandling _mergeNullValueHandling;

		// Token: 0x040008E7 RID: 2279
		private StringComparison _propertyNameComparison;
	}
}
