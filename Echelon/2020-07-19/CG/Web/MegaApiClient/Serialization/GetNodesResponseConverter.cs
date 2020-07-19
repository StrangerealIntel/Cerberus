using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x0200010A RID: 266
	internal class GetNodesResponseConverter : JsonConverter
	{
		// Token: 0x0600094B RID: 2379 RVA: 0x0003D08C File Offset: 0x0003B28C
		public GetNodesResponseConverter(byte[] masterKey)
		{
			this.masterKey = masterKey;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0003D09C File Offset: 0x0003B29C
		public override bool CanConvert(Type objectType)
		{
			return typeof(GetNodesResponse) == objectType;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0003D0B0 File Offset: 0x0003B2B0
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			JToken jtoken = JObject.Load(reader);
			GetNodesResponse getNodesResponse = new GetNodesResponse(this.masterKey);
			JsonReader jsonReader = jtoken.CreateReader();
			jsonReader.Culture = reader.Culture;
			jsonReader.DateFormatString = reader.DateFormatString;
			jsonReader.DateParseHandling = reader.DateParseHandling;
			jsonReader.DateTimeZoneHandling = reader.DateTimeZoneHandling;
			jsonReader.FloatParseHandling = reader.FloatParseHandling;
			jsonReader.MaxDepth = reader.MaxDepth;
			jsonReader.SupportMultipleContent = reader.SupportMultipleContent;
			serializer.Populate(jsonReader, getNodesResponse);
			return getNodesResponse;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0003D148 File Offset: 0x0003B348
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000556 RID: 1366
		private readonly byte[] masterKey;
	}
}
