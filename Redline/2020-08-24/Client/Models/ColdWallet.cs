using System;
using System.Runtime.Serialization;

namespace RedLine.Client.Models
{
	// Token: 0x02000039 RID: 57
	[DataContract(Name = "ColdWallet", Namespace = "v1/Models")]
	public struct ColdWallet
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000495A File Offset: 0x00002B5A
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00004962 File Offset: 0x00002B62
		[DataMember(Name = "FileName")]
		public string FileName { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000496B File Offset: 0x00002B6B
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00004973 File Offset: 0x00002B73
		[DataMember(Name = "DataArray")]
		public byte[] DataArray { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000497C File Offset: 0x00002B7C
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00004984 File Offset: 0x00002B84
		[DataMember(Name = "WalletName")]
		public string WalletName { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000498D File Offset: 0x00002B8D
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00004995 File Offset: 0x00002B95
		[DataMember(Name = "WalletDir")]
		public string WalletDir { get; set; }
	}
}
