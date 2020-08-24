using System;
using System.Runtime.Serialization;

namespace RedLine.Models
{
	// Token: 0x02000014 RID: 20
	[DataContract(Name = "LoginPair", Namespace = "v1/Models")]
	public class LoginPair
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000034FD File Offset: 0x000016FD
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003505 File Offset: 0x00001705
		[DataMember(Name = "Host")]
		public string Host { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000350E File Offset: 0x0000170E
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003516 File Offset: 0x00001716
		[DataMember(Name = "Login")]
		public string Login { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000351F File Offset: 0x0000171F
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003527 File Offset: 0x00001727
		[DataMember(Name = "Password")]
		public string Password { get; set; }
	}
}
