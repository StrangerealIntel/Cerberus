using System;
using System.Runtime.Serialization;

namespace RedLine.Models.Browsers
{
	// Token: 0x02000030 RID: 48
	[DataContract(Name = "Autofill", Namespace = "v1/Models")]
	public class Autofill
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000043D0 File Offset: 0x000025D0
		// (set) Token: 0x06000115 RID: 277 RVA: 0x000043D8 File Offset: 0x000025D8
		[DataMember(Name = "Name")]
		public string Name { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000043E1 File Offset: 0x000025E1
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000043E9 File Offset: 0x000025E9
		[DataMember(Name = "Value")]
		public string Value { get; set; }
	}
}
