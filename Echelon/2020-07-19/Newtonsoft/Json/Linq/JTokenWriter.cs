using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001E3 RID: 483
	[NullableContext(2)]
	[Nullable(0)]
	public class JTokenWriter : JsonWriter
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0006962C File Offset: 0x0006782C
		public JToken CurrentToken
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00069634 File Offset: 0x00067834
		public JToken Token
		{
			get
			{
				if (this._token != null)
				{
					return this._token;
				}
				return this._value;
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00069650 File Offset: 0x00067850
		[NullableContext(1)]
		public JTokenWriter(JContainer container)
		{
			ValidationUtils.ArgumentNotNull(container, "container");
			this._token = container;
			this._parent = container;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00069674 File Offset: 0x00067874
		public JTokenWriter()
		{
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0006967C File Offset: 0x0006787C
		public override void Flush()
		{
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00069680 File Offset: 0x00067880
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00069688 File Offset: 0x00067888
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new JObject());
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0006969C File Offset: 0x0006789C
		[NullableContext(1)]
		private void AddParent(JContainer container)
		{
			if (this._parent == null)
			{
				this._token = container;
			}
			else
			{
				this._parent.AddAndSkipParentCheck(container);
			}
			this._parent = container;
			this._current = container;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x000696D0 File Offset: 0x000678D0
		private void RemoveParent()
		{
			this._current = this._parent;
			this._parent = this._parent.Parent;
			if (this._parent != null && this._parent.Type == JTokenType.Property)
			{
				this._parent = this._parent.Parent;
			}
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0006972C File Offset: 0x0006792C
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new JArray());
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00069740 File Offset: 0x00067940
		[NullableContext(1)]
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this.AddParent(new JConstructor(name));
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00069758 File Offset: 0x00067958
		protected override void WriteEnd(JsonToken token)
		{
			this.RemoveParent();
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00069760 File Offset: 0x00067960
		[NullableContext(1)]
		public override void WritePropertyName(string name)
		{
			JObject jobject = this._parent as JObject;
			if (jobject != null)
			{
				jobject.Remove(name);
			}
			this.AddParent(new JProperty(name));
			base.WritePropertyName(name);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00069794 File Offset: 0x00067994
		private void AddValue(object value, JsonToken token)
		{
			this.AddValue(new JValue(value), token);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x000697A4 File Offset: 0x000679A4
		internal void AddValue(JValue value, JsonToken token)
		{
			if (this._parent != null)
			{
				this._parent.Add(value);
				this._current = this._parent.Last;
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					return;
				}
			}
			else
			{
				this._value = (value ?? JValue.CreateNull());
				this._current = this._value;
			}
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00069820 File Offset: 0x00067A20
		public override void WriteValue(object value)
		{
			if (value is System.Numerics.BigInteger)
			{
				base.InternalWriteValue(JsonToken.Integer);
				this.AddValue(value, JsonToken.Integer);
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00069844 File Offset: 0x00067A44
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddValue(null, JsonToken.Null);
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00069858 File Offset: 0x00067A58
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddValue(null, JsonToken.Undefined);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0006986C File Offset: 0x00067A6C
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this.AddValue(new JRaw(json), JsonToken.Raw);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00069884 File Offset: 0x00067A84
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this.AddValue(JValue.CreateComment(text), JsonToken.Comment);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0006989C File Offset: 0x00067A9C
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x000698B0 File Offset: 0x00067AB0
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x000698C8 File Offset: 0x00067AC8
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x000698E0 File Offset: 0x00067AE0
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x000698F8 File Offset: 0x00067AF8
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00069910 File Offset: 0x00067B10
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00069928 File Offset: 0x00067B28
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00069940 File Offset: 0x00067B40
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Boolean);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00069958 File Offset: 0x00067B58
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00069970 File Offset: 0x00067B70
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00069988 File Offset: 0x00067B88
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string value2 = value.ToString(CultureInfo.InvariantCulture);
			this.AddValue(value2, JsonToken.String);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x000699B8 File Offset: 0x00067BB8
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x000699D0 File Offset: 0x00067BD0
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x000699E8 File Offset: 0x00067BE8
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00069A00 File Offset: 0x00067C00
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00069A28 File Offset: 0x00067C28
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00069A40 File Offset: 0x00067C40
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Bytes);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00069A54 File Offset: 0x00067C54
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00069A6C File Offset: 0x00067C6C
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00069A84 File Offset: 0x00067C84
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00069A98 File Offset: 0x00067C98
		[NullableContext(1)]
		internal override void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			JTokenReader jtokenReader = reader as JTokenReader;
			if (jtokenReader == null || !writeChildren || !writeDateConstructorAsDate || !writeComments)
			{
				base.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return;
			}
			if (jtokenReader.TokenType == JsonToken.None && !jtokenReader.Read())
			{
				return;
			}
			JToken jtoken = jtokenReader.CurrentToken.CloneToken();
			if (this._parent != null)
			{
				this._parent.Add(jtoken);
				this._current = this._parent.Last;
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					base.InternalWriteValue(JsonToken.Null);
				}
			}
			else
			{
				this._current = jtoken;
				if (this._token == null && this._value == null)
				{
					this._token = (jtoken as JContainer);
					this._value = (jtoken as JValue);
				}
			}
			jtokenReader.Skip();
		}

		// Token: 0x0400090E RID: 2318
		private JContainer _token;

		// Token: 0x0400090F RID: 2319
		private JContainer _parent;

		// Token: 0x04000910 RID: 2320
		private JValue _value;

		// Token: 0x04000911 RID: 2321
		private JToken _current;
	}
}
