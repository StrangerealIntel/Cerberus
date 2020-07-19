using System;

namespace Newtonsoft.Json
{
	// Token: 0x0200013C RID: 316
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class JsonExtensionDataAttribute : Attribute
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0003EBDC File Offset: 0x0003CDDC
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x0003EBE4 File Offset: 0x0003CDE4
		public bool WriteData { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0003EBF0 File Offset: 0x0003CDF0
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x0003EBF8 File Offset: 0x0003CDF8
		public bool ReadData { get; set; }

		// Token: 0x06000A43 RID: 2627 RVA: 0x0003EC04 File Offset: 0x0003CE04
		public JsonExtensionDataAttribute()
		{
			this.WriteData = true;
			this.ReadData = true;
		}
	}
}
