using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000117 RID: 279
	internal class ShareDataConverter : JsonConverter
	{
		// Token: 0x06000983 RID: 2435 RVA: 0x0003D4E4 File Offset: 0x0003B6E4
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			ShareData shareData = value as ShareData;
			if (shareData == null)
			{
				throw new ArgumentException("invalid data to serialize");
			}
			writer.WriteStartArray();
			writer.WriteStartArray();
			writer.WriteValue(shareData.NodeId);
			writer.WriteEndArray();
			writer.WriteStartArray();
			foreach (ShareData.ShareDataItem shareDataItem in shareData.Items)
			{
				writer.WriteValue(shareDataItem.NodeId);
			}
			writer.WriteEndArray();
			writer.WriteStartArray();
			int num = 0;
			foreach (ShareData.ShareDataItem shareDataItem2 in shareData.Items)
			{
				writer.WriteValue(0);
				writer.WriteValue(num++);
				writer.WriteValue(Crypto.EncryptKey(shareDataItem2.Data, shareDataItem2.Key).ToBase64());
			}
			writer.WriteEndArray();
			writer.WriteEndArray();
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0003D604 File Offset: 0x0003B804
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0003D60C File Offset: 0x0003B80C
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(ShareData);
		}
	}
}
