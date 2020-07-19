using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000115 RID: 277
	[JsonConverter(typeof(SessionHistoryResponse.SessionHistoryConverter))]
	internal class SessionHistoryResponse : Collection<ISession>
	{
		// Token: 0x020002A5 RID: 677
		internal class SessionHistoryConverter : JsonConverter
		{
			// Token: 0x0600175D RID: 5981 RVA: 0x00077F40 File Offset: 0x00076140
			public override bool CanConvert(Type objectType)
			{
				return typeof(SessionHistoryResponse.SessionHistoryConverter.Session) == objectType;
			}

			// Token: 0x0600175E RID: 5982 RVA: 0x00077F54 File Offset: 0x00076154
			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				if (reader.TokenType == JsonToken.Null)
				{
					return null;
				}
				SessionHistoryResponse sessionHistoryResponse = new SessionHistoryResponse();
				foreach (JArray jArray in JArray.Load(reader).OfType<JArray>())
				{
					sessionHistoryResponse.Add(new SessionHistoryResponse.SessionHistoryConverter.Session(jArray));
				}
				return sessionHistoryResponse;
			}

			// Token: 0x0600175F RID: 5983 RVA: 0x00077FCC File Offset: 0x000761CC
			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0200032C RID: 812
			private class Session : ISession
			{
				// Token: 0x06001936 RID: 6454 RVA: 0x0007C928 File Offset: 0x0007AB28
				public Session(JArray jArray)
				{
					try
					{
						this.LoginTime = jArray.Value<long>(0).ToDateTime();
						this.LastSeenTime = jArray.Value<long>(1).ToDateTime();
						this.Client = jArray.Value<string>(2);
						this.IpAddress = IPAddress.Parse(jArray.Value<string>(3));
						this.Country = jArray.Value<string>(4);
						this.SessionId = jArray.Value<string>(6);
						jArray.Value<long>(7);
						if (jArray.Value<long>(5) == 1L)
						{
							this.Status |= SessionStatus.Current;
						}
						if (jArray.Value<long>(7) == 1L)
						{
							this.Status |= SessionStatus.Active;
						}
						if (this.Status == SessionStatus.Undefined)
						{
							this.Status = SessionStatus.Expired;
						}
					}
					catch (Exception ex)
					{
						this.Client = "Deserialization error: " + ex.Message;
					}
				}

				// Token: 0x17000504 RID: 1284
				// (get) Token: 0x06001937 RID: 6455 RVA: 0x0007CA48 File Offset: 0x0007AC48
				// (set) Token: 0x06001938 RID: 6456 RVA: 0x0007CA50 File Offset: 0x0007AC50
				public string Client { get; private set; }

				// Token: 0x17000505 RID: 1285
				// (get) Token: 0x06001939 RID: 6457 RVA: 0x0007CA5C File Offset: 0x0007AC5C
				// (set) Token: 0x0600193A RID: 6458 RVA: 0x0007CA64 File Offset: 0x0007AC64
				public IPAddress IpAddress { get; private set; }

				// Token: 0x17000506 RID: 1286
				// (get) Token: 0x0600193B RID: 6459 RVA: 0x0007CA70 File Offset: 0x0007AC70
				// (set) Token: 0x0600193C RID: 6460 RVA: 0x0007CA78 File Offset: 0x0007AC78
				public string Country { get; private set; }

				// Token: 0x17000507 RID: 1287
				// (get) Token: 0x0600193D RID: 6461 RVA: 0x0007CA84 File Offset: 0x0007AC84
				// (set) Token: 0x0600193E RID: 6462 RVA: 0x0007CA8C File Offset: 0x0007AC8C
				public DateTime LoginTime { get; private set; }

				// Token: 0x17000508 RID: 1288
				// (get) Token: 0x0600193F RID: 6463 RVA: 0x0007CA98 File Offset: 0x0007AC98
				// (set) Token: 0x06001940 RID: 6464 RVA: 0x0007CAA0 File Offset: 0x0007ACA0
				public DateTime LastSeenTime { get; private set; }

				// Token: 0x17000509 RID: 1289
				// (get) Token: 0x06001941 RID: 6465 RVA: 0x0007CAAC File Offset: 0x0007ACAC
				// (set) Token: 0x06001942 RID: 6466 RVA: 0x0007CAB4 File Offset: 0x0007ACB4
				public SessionStatus Status { get; private set; }

				// Token: 0x1700050A RID: 1290
				// (get) Token: 0x06001943 RID: 6467 RVA: 0x0007CAC0 File Offset: 0x0007ACC0
				// (set) Token: 0x06001944 RID: 6468 RVA: 0x0007CAC8 File Offset: 0x0007ACC8
				public string SessionId { get; private set; }
			}
		}
	}
}
