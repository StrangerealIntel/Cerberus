using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001BF RID: 447
	[NullableContext(1)]
	[Nullable(0)]
	internal class TraceJsonWriter : JsonWriter
	{
		// Token: 0x060010D8 RID: 4312 RVA: 0x0005F1A8 File Offset: 0x0005D3A8
		public TraceJsonWriter(JsonWriter innerWriter)
		{
			this._innerWriter = innerWriter;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._sw.Write("Serialized JSON: " + Environment.NewLine);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
			this._textWriter.Culture = innerWriter.Culture;
			this._textWriter.DateFormatHandling = innerWriter.DateFormatHandling;
			this._textWriter.DateFormatString = innerWriter.DateFormatString;
			this._textWriter.DateTimeZoneHandling = innerWriter.DateTimeZoneHandling;
			this._textWriter.FloatFormatHandling = innerWriter.FloatFormatHandling;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0005F264 File Offset: 0x0005D464
		public string GetSerializedJsonMessage()
		{
			return this._sw.ToString();
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0005F274 File Offset: 0x0005D474
		public override void WriteValue(decimal value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0005F298 File Offset: 0x0005D498
		public override void WriteValue(decimal? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0005F2E4 File Offset: 0x0005D4E4
		public override void WriteValue(bool value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0005F308 File Offset: 0x0005D508
		public override void WriteValue(bool? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0005F354 File Offset: 0x0005D554
		public override void WriteValue(byte value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0005F378 File Offset: 0x0005D578
		public override void WriteValue(byte? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0005F3C4 File Offset: 0x0005D5C4
		public override void WriteValue(char value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0005F3E8 File Offset: 0x0005D5E8
		public override void WriteValue(char? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0005F434 File Offset: 0x0005D634
		[NullableContext(2)]
		public override void WriteValue(byte[] value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value == null)
			{
				base.WriteUndefined();
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0005F464 File Offset: 0x0005D664
		public override void WriteValue(DateTime value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0005F488 File Offset: 0x0005D688
		public override void WriteValue(DateTime? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0005F4D4 File Offset: 0x0005D6D4
		public override void WriteValue(DateTimeOffset value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0005F4F8 File Offset: 0x0005D6F8
		public override void WriteValue(DateTimeOffset? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0005F544 File Offset: 0x0005D744
		public override void WriteValue(double value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0005F568 File Offset: 0x0005D768
		public override void WriteValue(double? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0005F5B4 File Offset: 0x0005D7B4
		public override void WriteUndefined()
		{
			this._textWriter.WriteUndefined();
			this._innerWriter.WriteUndefined();
			base.WriteUndefined();
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0005F5D4 File Offset: 0x0005D7D4
		public override void WriteNull()
		{
			this._textWriter.WriteNull();
			this._innerWriter.WriteNull();
			base.WriteUndefined();
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0005F5F4 File Offset: 0x0005D7F4
		public override void WriteValue(float value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0005F618 File Offset: 0x0005D818
		public override void WriteValue(float? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0005F664 File Offset: 0x0005D864
		public override void WriteValue(Guid value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0005F688 File Offset: 0x0005D888
		public override void WriteValue(Guid? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0005F6D4 File Offset: 0x0005D8D4
		public override void WriteValue(int value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0005F6F8 File Offset: 0x0005D8F8
		public override void WriteValue(int? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0005F744 File Offset: 0x0005D944
		public override void WriteValue(long value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0005F768 File Offset: 0x0005D968
		public override void WriteValue(long? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0005F7B4 File Offset: 0x0005D9B4
		[NullableContext(2)]
		public override void WriteValue(object value)
		{
			if (value is System.Numerics.BigInteger)
			{
				this._textWriter.WriteValue(value);
				this._innerWriter.WriteValue(value);
				base.InternalWriteValue(JsonToken.Integer);
				return;
			}
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value == null)
			{
				base.WriteUndefined();
				return;
			}
			base.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0005F820 File Offset: 0x0005DA20
		public override void WriteValue(sbyte value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0005F844 File Offset: 0x0005DA44
		public override void WriteValue(sbyte? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0005F890 File Offset: 0x0005DA90
		public override void WriteValue(short value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0005F8B4 File Offset: 0x0005DAB4
		public override void WriteValue(short? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0005F900 File Offset: 0x0005DB00
		[NullableContext(2)]
		public override void WriteValue(string value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0005F924 File Offset: 0x0005DB24
		public override void WriteValue(TimeSpan value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0005F948 File Offset: 0x0005DB48
		public override void WriteValue(TimeSpan? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0005F994 File Offset: 0x0005DB94
		public override void WriteValue(uint value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0005F9B8 File Offset: 0x0005DBB8
		public override void WriteValue(uint? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0005FA04 File Offset: 0x0005DC04
		public override void WriteValue(ulong value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0005FA28 File Offset: 0x0005DC28
		public override void WriteValue(ulong? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0005FA74 File Offset: 0x0005DC74
		[NullableContext(2)]
		public override void WriteValue(Uri value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value == null)
			{
				base.WriteUndefined();
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0005FAA8 File Offset: 0x0005DCA8
		public override void WriteValue(ushort value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0005FACC File Offset: 0x0005DCCC
		public override void WriteValue(ushort? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0005FB18 File Offset: 0x0005DD18
		public override void WriteWhitespace(string ws)
		{
			this._textWriter.WriteWhitespace(ws);
			this._innerWriter.WriteWhitespace(ws);
			base.WriteWhitespace(ws);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0005FB3C File Offset: 0x0005DD3C
		[NullableContext(2)]
		public override void WriteComment(string text)
		{
			this._textWriter.WriteComment(text);
			this._innerWriter.WriteComment(text);
			base.WriteComment(text);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0005FB60 File Offset: 0x0005DD60
		public override void WriteStartArray()
		{
			this._textWriter.WriteStartArray();
			this._innerWriter.WriteStartArray();
			base.WriteStartArray();
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0005FB80 File Offset: 0x0005DD80
		public override void WriteEndArray()
		{
			this._textWriter.WriteEndArray();
			this._innerWriter.WriteEndArray();
			base.WriteEndArray();
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0005FBA0 File Offset: 0x0005DDA0
		public override void WriteStartConstructor(string name)
		{
			this._textWriter.WriteStartConstructor(name);
			this._innerWriter.WriteStartConstructor(name);
			base.WriteStartConstructor(name);
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0005FBC4 File Offset: 0x0005DDC4
		public override void WriteEndConstructor()
		{
			this._textWriter.WriteEndConstructor();
			this._innerWriter.WriteEndConstructor();
			base.WriteEndConstructor();
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0005FBE4 File Offset: 0x0005DDE4
		public override void WritePropertyName(string name)
		{
			this._textWriter.WritePropertyName(name);
			this._innerWriter.WritePropertyName(name);
			base.WritePropertyName(name);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0005FC08 File Offset: 0x0005DE08
		public override void WritePropertyName(string name, bool escape)
		{
			this._textWriter.WritePropertyName(name, escape);
			this._innerWriter.WritePropertyName(name, escape);
			base.WritePropertyName(name);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0005FC2C File Offset: 0x0005DE2C
		public override void WriteStartObject()
		{
			this._textWriter.WriteStartObject();
			this._innerWriter.WriteStartObject();
			base.WriteStartObject();
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0005FC4C File Offset: 0x0005DE4C
		public override void WriteEndObject()
		{
			this._textWriter.WriteEndObject();
			this._innerWriter.WriteEndObject();
			base.WriteEndObject();
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0005FC6C File Offset: 0x0005DE6C
		[NullableContext(2)]
		public override void WriteRawValue(string json)
		{
			this._textWriter.WriteRawValue(json);
			this._innerWriter.WriteRawValue(json);
			base.InternalWriteValue(JsonToken.Undefined);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0005FC90 File Offset: 0x0005DE90
		[NullableContext(2)]
		public override void WriteRaw(string json)
		{
			this._textWriter.WriteRaw(json);
			this._innerWriter.WriteRaw(json);
			base.WriteRaw(json);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0005FCB4 File Offset: 0x0005DEB4
		public override void Close()
		{
			this._textWriter.Close();
			this._innerWriter.Close();
			base.Close();
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0005FCD4 File Offset: 0x0005DED4
		public override void Flush()
		{
			this._textWriter.Flush();
			this._innerWriter.Flush();
		}

		// Token: 0x04000841 RID: 2113
		private readonly JsonWriter _innerWriter;

		// Token: 0x04000842 RID: 2114
		private readonly JsonTextWriter _textWriter;

		// Token: 0x04000843 RID: 2115
		private readonly StringWriter _sw;
	}
}
