using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x0200010F RID: 271
	internal class NodeConverter : JsonConverter
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x0003D2A0 File Offset: 0x0003B4A0
		public NodeConverter(byte[] masterKey, ref List<SharedKey> sharedKeys)
		{
			this.masterKey = masterKey;
			this.sharedKeys = sharedKeys;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0003D2B8 File Offset: 0x0003B4B8
		public override bool CanConvert(Type objectType)
		{
			return typeof(Node) == objectType;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0003D2CC File Offset: 0x0003B4CC
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			JToken jtoken = JObject.Load(reader);
			Node node = new Node(this.masterKey, ref this.sharedKeys);
			JsonReader jsonReader = jtoken.CreateReader();
			jsonReader.Culture = reader.Culture;
			jsonReader.DateFormatString = reader.DateFormatString;
			jsonReader.DateParseHandling = reader.DateParseHandling;
			jsonReader.DateTimeZoneHandling = reader.DateTimeZoneHandling;
			jsonReader.FloatParseHandling = reader.FloatParseHandling;
			jsonReader.MaxDepth = reader.MaxDepth;
			jsonReader.SupportMultipleContent = reader.SupportMultipleContent;
			serializer.Populate(jsonReader, node);
			return node;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0003D368 File Offset: 0x0003B568
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000560 RID: 1376
		private readonly byte[] masterKey;

		// Token: 0x04000561 RID: 1377
		private List<SharedKey> sharedKeys;
	}
}
