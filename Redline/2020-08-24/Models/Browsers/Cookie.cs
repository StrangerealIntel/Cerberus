using System;
using System.Runtime.Serialization;

namespace RedLine.Models.Browsers
{
	// Token: 0x02000032 RID: 50
	[DataContract(Name = "Cookie", Namespace = "v1/Models")]
	public class Cookie
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000044CC File Offset: 0x000026CC
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000044D4 File Offset: 0x000026D4
		[DataMember(Name = "Host")]
		public string Host { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000044DD File Offset: 0x000026DD
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000044E5 File Offset: 0x000026E5
		[DataMember(Name = "Http")]
		public bool Http { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000044EE File Offset: 0x000026EE
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000044F6 File Offset: 0x000026F6
		[DataMember(Name = "Path")]
		public string Path { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000044FF File Offset: 0x000026FF
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00004507 File Offset: 0x00002707
		[DataMember(Name = "Secure")]
		public bool Secure { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00004510 File Offset: 0x00002710
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00004518 File Offset: 0x00002718
		[DataMember(Name = "Expires")]
		public long Expires { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004521 File Offset: 0x00002721
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00004529 File Offset: 0x00002729
		[DataMember(Name = "Name")]
		public string Name { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004532 File Offset: 0x00002732
		// (set) Token: 0x06000134 RID: 308 RVA: 0x0000453A File Offset: 0x0000273A
		[DataMember(Name = "Value")]
		public string Value { get; set; }
	}
}
