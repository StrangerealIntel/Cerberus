using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200014E RID: 334
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonWriter : IDisposable
	{
		// Token: 0x06000C35 RID: 3125 RVA: 0x00047CA4 File Offset: 0x00045EA4
		internal static JsonWriter.State[][] BuildStateArray()
		{
			List<JsonWriter.State[]> list = JsonWriter.StateArrayTempate.ToList<JsonWriter.State[]>();
			JsonWriter.State[] item = JsonWriter.StateArrayTempate[0];
			JsonWriter.State[] item2 = JsonWriter.StateArrayTempate[7];
			foreach (ulong num in EnumUtils.GetEnumValuesAndNames(typeof(JsonToken)).Values)
			{
				if (list.Count <= (int)num)
				{
					JsonToken jsonToken = (JsonToken)num;
					if (jsonToken - JsonToken.Integer <= 5 || jsonToken - JsonToken.Date <= 1)
					{
						list.Add(item2);
					}
					else
					{
						list.Add(item);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00047D4C File Offset: 0x00045F4C
		static JsonWriter()
		{
			JsonWriter.StateArray = JsonWriter.BuildStateArray();
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00047F24 File Offset: 0x00046124
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x00047F2C File Offset: 0x0004612C
		public bool CloseOutput { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00047F38 File Offset: 0x00046138
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x00047F40 File Offset: 0x00046140
		public bool AutoCompleteOnClose { get; set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00047F4C File Offset: 0x0004614C
		protected internal int Top
		{
			get
			{
				List<JsonPosition> stack = this._stack;
				int num = (stack != null) ? stack.Count : 0;
				if (this.Peek() != JsonContainerType.None)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x00047F88 File Offset: 0x00046188
		public WriteState WriteState
		{
			get
			{
				switch (this._currentState)
				{
				case JsonWriter.State.Start:
					return WriteState.Start;
				case JsonWriter.State.Property:
					return WriteState.Property;
				case JsonWriter.State.ObjectStart:
				case JsonWriter.State.Object:
					return WriteState.Object;
				case JsonWriter.State.ArrayStart:
				case JsonWriter.State.Array:
					return WriteState.Array;
				case JsonWriter.State.ConstructorStart:
				case JsonWriter.State.Constructor:
					return WriteState.Constructor;
				case JsonWriter.State.Closed:
					return WriteState.Closed;
				case JsonWriter.State.Error:
					return WriteState.Error;
				default:
					throw JsonWriterException.Create(this, "Invalid state: " + this._currentState.ToString(), null);
				}
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x00048004 File Offset: 0x00046204
		internal string ContainerPath
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None || this._stack == null)
				{
					return string.Empty;
				}
				return JsonPosition.BuildPath(this._stack, null);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0004804C File Offset: 0x0004624C
		public string Path
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				JsonPosition? currentPosition = (this._currentState != JsonWriter.State.ArrayStart && this._currentState != JsonWriter.State.ConstructorStart && this._currentState != JsonWriter.State.ObjectStart) ? new JsonPosition?(this._currentPosition) : null;
				return JsonPosition.BuildPath(this._stack, currentPosition);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x000480C8 File Offset: 0x000462C8
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x000480D0 File Offset: 0x000462D0
		public Formatting Formatting
		{
			get
			{
				return this._formatting;
			}
			set
			{
				if (value < Formatting.None || value > Formatting.Indented)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._formatting = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x000480F4 File Offset: 0x000462F4
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x000480FC File Offset: 0x000462FC
		public DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._dateFormatHandling;
			}
			set
			{
				if (value < DateFormatHandling.IsoDateFormat || value > DateFormatHandling.MicrosoftDateFormat)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateFormatHandling = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00048120 File Offset: 0x00046320
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x00048128 File Offset: 0x00046328
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._dateTimeZoneHandling;
			}
			set
			{
				if (value < DateTimeZoneHandling.Local || value > DateTimeZoneHandling.RoundtripKind)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateTimeZoneHandling = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0004814C File Offset: 0x0004634C
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x00048154 File Offset: 0x00046354
		public StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._stringEscapeHandling;
			}
			set
			{
				if (value < StringEscapeHandling.Default || value > StringEscapeHandling.EscapeHtml)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._stringEscapeHandling = value;
				this.OnStringEscapeHandlingChanged();
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0004817C File Offset: 0x0004637C
		internal virtual void OnStringEscapeHandlingChanged()
		{
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00048180 File Offset: 0x00046380
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x00048188 File Offset: 0x00046388
		public FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._floatFormatHandling;
			}
			set
			{
				if (value < FloatFormatHandling.String || value > FloatFormatHandling.DefaultValue)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._floatFormatHandling = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x000481AC File Offset: 0x000463AC
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x000481B4 File Offset: 0x000463B4
		[Nullable(2)]
		public string DateFormatString
		{
			[NullableContext(2)]
			get
			{
				return this._dateFormatString;
			}
			[NullableContext(2)]
			set
			{
				this._dateFormatString = value;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x000481C0 File Offset: 0x000463C0
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x000481D4 File Offset: 0x000463D4
		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? CultureInfo.InvariantCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x000481E0 File Offset: 0x000463E0
		protected JsonWriter()
		{
			this._currentState = JsonWriter.State.Start;
			this._formatting = Formatting.None;
			this._dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			this.CloseOutput = true;
			this.AutoCompleteOnClose = true;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0004820C File Offset: 0x0004640C
		internal void UpdateScopeWithFinishedValue()
		{
			if (this._currentPosition.HasIndex)
			{
				this._currentPosition.Position = this._currentPosition.Position + 1;
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00048230 File Offset: 0x00046430
		private void Push(JsonContainerType value)
		{
			if (this._currentPosition.Type != JsonContainerType.None)
			{
				if (this._stack == null)
				{
					this._stack = new List<JsonPosition>();
				}
				this._stack.Add(this._currentPosition);
			}
			this._currentPosition = new JsonPosition(value);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00048284 File Offset: 0x00046484
		private JsonContainerType Pop()
		{
			ref JsonPosition currentPosition = this._currentPosition;
			if (this._stack != null && this._stack.Count > 0)
			{
				this._currentPosition = this._stack[this._stack.Count - 1];
				this._stack.RemoveAt(this._stack.Count - 1);
			}
			else
			{
				this._currentPosition = default(JsonPosition);
			}
			return currentPosition.Type;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00048304 File Offset: 0x00046504
		private JsonContainerType Peek()
		{
			return this._currentPosition.Type;
		}

		// Token: 0x06000C53 RID: 3155
		public abstract void Flush();

		// Token: 0x06000C54 RID: 3156 RVA: 0x00048314 File Offset: 0x00046514
		public virtual void Close()
		{
			if (this.AutoCompleteOnClose)
			{
				this.AutoCompleteAll();
			}
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00048328 File Offset: 0x00046528
		public virtual void WriteStartObject()
		{
			this.InternalWriteStart(JsonToken.StartObject, JsonContainerType.Object);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00048334 File Offset: 0x00046534
		public virtual void WriteEndObject()
		{
			this.InternalWriteEnd(JsonContainerType.Object);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00048340 File Offset: 0x00046540
		public virtual void WriteStartArray()
		{
			this.InternalWriteStart(JsonToken.StartArray, JsonContainerType.Array);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0004834C File Offset: 0x0004654C
		public virtual void WriteEndArray()
		{
			this.InternalWriteEnd(JsonContainerType.Array);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00048358 File Offset: 0x00046558
		public virtual void WriteStartConstructor(string name)
		{
			this.InternalWriteStart(JsonToken.StartConstructor, JsonContainerType.Constructor);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00048364 File Offset: 0x00046564
		public virtual void WriteEndConstructor()
		{
			this.InternalWriteEnd(JsonContainerType.Constructor);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00048370 File Offset: 0x00046570
		public virtual void WritePropertyName(string name)
		{
			this.InternalWritePropertyName(name);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0004837C File Offset: 0x0004657C
		public virtual void WritePropertyName(string name, bool escape)
		{
			this.WritePropertyName(name);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00048388 File Offset: 0x00046588
		public virtual void WriteEnd()
		{
			this.WriteEnd(this.Peek());
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00048398 File Offset: 0x00046598
		public void WriteToken(JsonReader reader)
		{
			this.WriteToken(reader, true);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x000483A4 File Offset: 0x000465A4
		public void WriteToken(JsonReader reader, bool writeChildren)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this.WriteToken(reader, writeChildren, true, true);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x000483BC File Offset: 0x000465BC
		[NullableContext(2)]
		public void WriteToken(JsonToken token, object value)
		{
			switch (token)
			{
			case JsonToken.None:
				return;
			case JsonToken.StartObject:
				this.WriteStartObject();
				return;
			case JsonToken.StartArray:
				this.WriteStartArray();
				return;
			case JsonToken.StartConstructor:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WriteStartConstructor(value.ToString());
				return;
			case JsonToken.PropertyName:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WritePropertyName(value.ToString());
				return;
			case JsonToken.Comment:
				this.WriteComment((value != null) ? value.ToString() : null);
				return;
			case JsonToken.Raw:
				this.WriteRawValue((value != null) ? value.ToString() : null);
				return;
			case JsonToken.Integer:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is System.Numerics.BigInteger)
				{
					System.Numerics.BigInteger bigInteger = (System.Numerics.BigInteger)value;
					this.WriteValue(bigInteger);
					return;
				}
				this.WriteValue(Convert.ToInt64(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Float:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is decimal)
				{
					decimal value2 = (decimal)value;
					this.WriteValue(value2);
					return;
				}
				if (value is double)
				{
					double value3 = (double)value;
					this.WriteValue(value3);
					return;
				}
				if (value is float)
				{
					float value4 = (float)value;
					this.WriteValue(value4);
					return;
				}
				this.WriteValue(Convert.ToDouble(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.String:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WriteValue(value.ToString());
				return;
			case JsonToken.Boolean:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WriteValue(Convert.ToBoolean(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Null:
				this.WriteNull();
				return;
			case JsonToken.Undefined:
				this.WriteUndefined();
				return;
			case JsonToken.EndObject:
				this.WriteEndObject();
				return;
			case JsonToken.EndArray:
				this.WriteEndArray();
				return;
			case JsonToken.EndConstructor:
				this.WriteEndConstructor();
				return;
			case JsonToken.Date:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is DateTimeOffset)
				{
					DateTimeOffset value5 = (DateTimeOffset)value;
					this.WriteValue(value5);
					return;
				}
				this.WriteValue(Convert.ToDateTime(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Bytes:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is Guid)
				{
					Guid value6 = (Guid)value;
					this.WriteValue(value6);
					return;
				}
				this.WriteValue((byte[])value);
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("token", token, "Unexpected token type.");
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00048614 File Offset: 0x00046814
		public void WriteToken(JsonToken token)
		{
			this.WriteToken(token, null);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00048620 File Offset: 0x00046820
		internal virtual void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			int num = this.CalculateWriteTokenInitialDepth(reader);
			for (;;)
			{
				if (!writeDateConstructorAsDate || reader.TokenType != JsonToken.StartConstructor)
				{
					goto IL_4E;
				}
				object value = reader.Value;
				if (!string.Equals((value != null) ? value.ToString() : null, "Date", StringComparison.Ordinal))
				{
					goto IL_4E;
				}
				this.WriteConstructorDate(reader);
				IL_73:
				if (num - 1 >= reader.Depth - (JsonTokenUtils.IsEndToken(reader.TokenType) ? 1 : 0) || !writeChildren || !reader.Read())
				{
					break;
				}
				continue;
				IL_4E:
				if (writeComments || reader.TokenType != JsonToken.Comment)
				{
					this.WriteToken(reader.TokenType, reader.Value);
					goto IL_73;
				}
				goto IL_73;
			}
			if (this.IsWriteTokenIncomplete(reader, writeChildren, num))
			{
				throw JsonWriterException.Create(this, "Unexpected end when reading token.", null);
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000486F4 File Offset: 0x000468F4
		private bool IsWriteTokenIncomplete(JsonReader reader, bool writeChildren, int initialDepth)
		{
			int num = this.CalculateWriteTokenFinalDepth(reader);
			return initialDepth < num || (writeChildren && initialDepth == num && JsonTokenUtils.IsStartToken(reader.TokenType));
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00048730 File Offset: 0x00046930
		private int CalculateWriteTokenInitialDepth(JsonReader reader)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.None)
			{
				return -1;
			}
			if (!JsonTokenUtils.IsStartToken(tokenType))
			{
				return reader.Depth + 1;
			}
			return reader.Depth;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0004876C File Offset: 0x0004696C
		private int CalculateWriteTokenFinalDepth(JsonReader reader)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.None)
			{
				return -1;
			}
			if (!JsonTokenUtils.IsEndToken(tokenType))
			{
				return reader.Depth;
			}
			return reader.Depth - 1;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x000487A8 File Offset: 0x000469A8
		private void WriteConstructorDate(JsonReader reader)
		{
			DateTime value;
			string message;
			if (!JavaScriptUtils.TryGetDateFromConstructorJson(reader, out value, out message))
			{
				throw JsonWriterException.Create(this, message, null);
			}
			this.WriteValue(value);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x000487D8 File Offset: 0x000469D8
		private void WriteEnd(JsonContainerType type)
		{
			switch (type)
			{
			case JsonContainerType.Object:
				this.WriteEndObject();
				return;
			case JsonContainerType.Array:
				this.WriteEndArray();
				return;
			case JsonContainerType.Constructor:
				this.WriteEndConstructor();
				return;
			default:
				throw JsonWriterException.Create(this, "Unexpected type when writing end: " + type.ToString(), null);
			}
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00048838 File Offset: 0x00046A38
		private void AutoCompleteAll()
		{
			while (this.Top > 0)
			{
				this.WriteEnd();
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00048850 File Offset: 0x00046A50
		private JsonToken GetCloseTokenForType(JsonContainerType type)
		{
			switch (type)
			{
			case JsonContainerType.Object:
				return JsonToken.EndObject;
			case JsonContainerType.Array:
				return JsonToken.EndArray;
			case JsonContainerType.Constructor:
				return JsonToken.EndConstructor;
			default:
				throw JsonWriterException.Create(this, "No close token for type: " + type.ToString(), null);
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000488A4 File Offset: 0x00046AA4
		private void AutoCompleteClose(JsonContainerType type)
		{
			int num = this.CalculateLevelsToComplete(type);
			for (int i = 0; i < num; i++)
			{
				JsonToken closeTokenForType = this.GetCloseTokenForType(this.Pop());
				if (this._currentState == JsonWriter.State.Property)
				{
					this.WriteNull();
				}
				if (this._formatting == Formatting.Indented && this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
				{
					this.WriteIndent();
				}
				this.WriteEnd(closeTokenForType);
				this.UpdateCurrentState();
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00048924 File Offset: 0x00046B24
		private int CalculateLevelsToComplete(JsonContainerType type)
		{
			int num = 0;
			if (this._currentPosition.Type == type)
			{
				num = 1;
			}
			else
			{
				int num2 = this.Top - 2;
				for (int i = num2; i >= 0; i--)
				{
					int index = num2 - i;
					if (this._stack[index].Type == type)
					{
						num = i + 2;
						break;
					}
				}
			}
			if (num == 0)
			{
				throw JsonWriterException.Create(this, "No token to close.", null);
			}
			return num;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000489A0 File Offset: 0x00046BA0
		private void UpdateCurrentState()
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.None:
				this._currentState = JsonWriter.State.Start;
				return;
			case JsonContainerType.Object:
				this._currentState = JsonWriter.State.Object;
				return;
			case JsonContainerType.Array:
				this._currentState = JsonWriter.State.Array;
				return;
			case JsonContainerType.Constructor:
				this._currentState = JsonWriter.State.Array;
				return;
			default:
				throw JsonWriterException.Create(this, "Unknown JsonType: " + jsonContainerType.ToString(), null);
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00048A14 File Offset: 0x00046C14
		protected virtual void WriteEnd(JsonToken token)
		{
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00048A18 File Offset: 0x00046C18
		protected virtual void WriteIndent()
		{
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00048A1C File Offset: 0x00046C1C
		protected virtual void WriteValueDelimiter()
		{
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00048A20 File Offset: 0x00046C20
		protected virtual void WriteIndentSpace()
		{
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00048A24 File Offset: 0x00046C24
		internal void AutoComplete(JsonToken tokenBeingWritten)
		{
			JsonWriter.State state = JsonWriter.StateArray[(int)tokenBeingWritten][(int)this._currentState];
			if (state == JsonWriter.State.Error)
			{
				throw JsonWriterException.Create(this, "Token {0} in state {1} would result in an invalid JSON object.".FormatWith(CultureInfo.InvariantCulture, tokenBeingWritten.ToString(), this._currentState.ToString()), null);
			}
			if ((this._currentState == JsonWriter.State.Object || this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.Constructor) && tokenBeingWritten != JsonToken.Comment)
			{
				this.WriteValueDelimiter();
			}
			if (this._formatting == Formatting.Indented)
			{
				if (this._currentState == JsonWriter.State.Property)
				{
					this.WriteIndentSpace();
				}
				if (this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.ArrayStart || this._currentState == JsonWriter.State.Constructor || this._currentState == JsonWriter.State.ConstructorStart || (tokenBeingWritten == JsonToken.PropertyName && this._currentState != JsonWriter.State.Start))
				{
					this.WriteIndent();
				}
			}
			this._currentState = state;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00048B24 File Offset: 0x00046D24
		public virtual void WriteNull()
		{
			this.InternalWriteValue(JsonToken.Null);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00048B30 File Offset: 0x00046D30
		public virtual void WriteUndefined()
		{
			this.InternalWriteValue(JsonToken.Undefined);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00048B3C File Offset: 0x00046D3C
		[NullableContext(2)]
		public virtual void WriteRaw(string json)
		{
			this.InternalWriteRaw();
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00048B44 File Offset: 0x00046D44
		[NullableContext(2)]
		public virtual void WriteRawValue(string json)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(JsonToken.Undefined);
			this.WriteRaw(json);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00048B5C File Offset: 0x00046D5C
		[NullableContext(2)]
		public virtual void WriteValue(string value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00048B68 File Offset: 0x00046D68
		public virtual void WriteValue(int value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00048B74 File Offset: 0x00046D74
		[CLSCompliant(false)]
		public virtual void WriteValue(uint value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00048B80 File Offset: 0x00046D80
		public virtual void WriteValue(long value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00048B8C File Offset: 0x00046D8C
		[CLSCompliant(false)]
		public virtual void WriteValue(ulong value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00048B98 File Offset: 0x00046D98
		public virtual void WriteValue(float value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00048BA4 File Offset: 0x00046DA4
		public virtual void WriteValue(double value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00048BB0 File Offset: 0x00046DB0
		public virtual void WriteValue(bool value)
		{
			this.InternalWriteValue(JsonToken.Boolean);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00048BBC File Offset: 0x00046DBC
		public virtual void WriteValue(short value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00048BC8 File Offset: 0x00046DC8
		[CLSCompliant(false)]
		public virtual void WriteValue(ushort value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00048BD4 File Offset: 0x00046DD4
		public virtual void WriteValue(char value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00048BE0 File Offset: 0x00046DE0
		public virtual void WriteValue(byte value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00048BEC File Offset: 0x00046DEC
		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00048BF8 File Offset: 0x00046DF8
		public virtual void WriteValue(decimal value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00048C04 File Offset: 0x00046E04
		public virtual void WriteValue(DateTime value)
		{
			this.InternalWriteValue(JsonToken.Date);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00048C10 File Offset: 0x00046E10
		public virtual void WriteValue(DateTimeOffset value)
		{
			this.InternalWriteValue(JsonToken.Date);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00048C1C File Offset: 0x00046E1C
		public virtual void WriteValue(Guid value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00048C28 File Offset: 0x00046E28
		public virtual void WriteValue(TimeSpan value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00048C34 File Offset: 0x00046E34
		public virtual void WriteValue(int? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00048C58 File Offset: 0x00046E58
		[CLSCompliant(false)]
		public virtual void WriteValue(uint? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00048C7C File Offset: 0x00046E7C
		public virtual void WriteValue(long? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00048CA0 File Offset: 0x00046EA0
		[CLSCompliant(false)]
		public virtual void WriteValue(ulong? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00048CC4 File Offset: 0x00046EC4
		public virtual void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00048CE8 File Offset: 0x00046EE8
		public virtual void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00048D0C File Offset: 0x00046F0C
		public virtual void WriteValue(bool? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00048D30 File Offset: 0x00046F30
		public virtual void WriteValue(short? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00048D54 File Offset: 0x00046F54
		[CLSCompliant(false)]
		public virtual void WriteValue(ushort? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00048D78 File Offset: 0x00046F78
		public virtual void WriteValue(char? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00048D9C File Offset: 0x00046F9C
		public virtual void WriteValue(byte? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00048DC0 File Offset: 0x00046FC0
		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00048DE4 File Offset: 0x00046FE4
		public virtual void WriteValue(decimal? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00048E08 File Offset: 0x00047008
		public virtual void WriteValue(DateTime? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00048E2C File Offset: 0x0004702C
		public virtual void WriteValue(DateTimeOffset? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00048E50 File Offset: 0x00047050
		public virtual void WriteValue(Guid? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00048E74 File Offset: 0x00047074
		public virtual void WriteValue(TimeSpan? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00048E98 File Offset: 0x00047098
		[NullableContext(2)]
		public virtual void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.InternalWriteValue(JsonToken.Bytes);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00048EB0 File Offset: 0x000470B0
		[NullableContext(2)]
		public virtual void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00048ED0 File Offset: 0x000470D0
		[NullableContext(2)]
		public virtual void WriteValue(object value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			if (value is System.Numerics.BigInteger)
			{
				throw JsonWriter.CreateUnsupportedTypeException(this, value);
			}
			JsonWriter.WriteValue(this, ConvertUtils.GetTypeCode(value.GetType()), value);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00048F04 File Offset: 0x00047104
		[NullableContext(2)]
		public virtual void WriteComment(string text)
		{
			this.InternalWriteComment();
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00048F0C File Offset: 0x0004710C
		public virtual void WriteWhitespace(string ws)
		{
			this.InternalWriteWhitespace(ws);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00048F18 File Offset: 0x00047118
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00048F28 File Offset: 0x00047128
		protected virtual void Dispose(bool disposing)
		{
			if (this._currentState != JsonWriter.State.Closed && disposing)
			{
				this.Close();
			}
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00048F44 File Offset: 0x00047144
		internal static void WriteValue(JsonWriter writer, PrimitiveTypeCode typeCode, object value)
		{
			for (;;)
			{
				switch (typeCode)
				{
				case PrimitiveTypeCode.Char:
					goto IL_AD;
				case PrimitiveTypeCode.CharNullable:
					goto IL_BA;
				case PrimitiveTypeCode.Boolean:
					goto IL_E0;
				case PrimitiveTypeCode.BooleanNullable:
					goto IL_ED;
				case PrimitiveTypeCode.SByte:
					goto IL_113;
				case PrimitiveTypeCode.SByteNullable:
					goto IL_120;
				case PrimitiveTypeCode.Int16:
					goto IL_146;
				case PrimitiveTypeCode.Int16Nullable:
					goto IL_153;
				case PrimitiveTypeCode.UInt16:
					goto IL_179;
				case PrimitiveTypeCode.UInt16Nullable:
					goto IL_186;
				case PrimitiveTypeCode.Int32:
					goto IL_1AD;
				case PrimitiveTypeCode.Int32Nullable:
					goto IL_1BA;
				case PrimitiveTypeCode.Byte:
					goto IL_1E1;
				case PrimitiveTypeCode.ByteNullable:
					goto IL_1EE;
				case PrimitiveTypeCode.UInt32:
					goto IL_215;
				case PrimitiveTypeCode.UInt32Nullable:
					goto IL_222;
				case PrimitiveTypeCode.Int64:
					goto IL_249;
				case PrimitiveTypeCode.Int64Nullable:
					goto IL_256;
				case PrimitiveTypeCode.UInt64:
					goto IL_27D;
				case PrimitiveTypeCode.UInt64Nullable:
					goto IL_28A;
				case PrimitiveTypeCode.Single:
					goto IL_2B1;
				case PrimitiveTypeCode.SingleNullable:
					goto IL_2BE;
				case PrimitiveTypeCode.Double:
					goto IL_2E5;
				case PrimitiveTypeCode.DoubleNullable:
					goto IL_2F2;
				case PrimitiveTypeCode.DateTime:
					goto IL_319;
				case PrimitiveTypeCode.DateTimeNullable:
					goto IL_326;
				case PrimitiveTypeCode.DateTimeOffset:
					goto IL_34D;
				case PrimitiveTypeCode.DateTimeOffsetNullable:
					goto IL_35A;
				case PrimitiveTypeCode.Decimal:
					goto IL_381;
				case PrimitiveTypeCode.DecimalNullable:
					goto IL_38E;
				case PrimitiveTypeCode.Guid:
					goto IL_3B5;
				case PrimitiveTypeCode.GuidNullable:
					goto IL_3C2;
				case PrimitiveTypeCode.TimeSpan:
					goto IL_3E9;
				case PrimitiveTypeCode.TimeSpanNullable:
					goto IL_3F6;
				case PrimitiveTypeCode.BigInteger:
					goto IL_41D;
				case PrimitiveTypeCode.BigIntegerNullable:
					goto IL_42F;
				case PrimitiveTypeCode.Uri:
					goto IL_45B;
				case PrimitiveTypeCode.String:
					goto IL_468;
				case PrimitiveTypeCode.Bytes:
					goto IL_475;
				case PrimitiveTypeCode.DBNull:
					goto IL_482;
				default:
				{
					IConvertible convertible = value as IConvertible;
					if (convertible == null)
					{
						goto IL_4A8;
					}
					JsonWriter.ResolveConvertibleValue(convertible, out typeCode, out value);
					break;
				}
				}
			}
			IL_AD:
			writer.WriteValue((char)value);
			return;
			IL_BA:
			writer.WriteValue((value == null) ? null : new char?((char)value));
			return;
			IL_E0:
			writer.WriteValue((bool)value);
			return;
			IL_ED:
			writer.WriteValue((value == null) ? null : new bool?((bool)value));
			return;
			IL_113:
			writer.WriteValue((sbyte)value);
			return;
			IL_120:
			writer.WriteValue((value == null) ? null : new sbyte?((sbyte)value));
			return;
			IL_146:
			writer.WriteValue((short)value);
			return;
			IL_153:
			writer.WriteValue((value == null) ? null : new short?((short)value));
			return;
			IL_179:
			writer.WriteValue((ushort)value);
			return;
			IL_186:
			writer.WriteValue((value == null) ? null : new ushort?((ushort)value));
			return;
			IL_1AD:
			writer.WriteValue((int)value);
			return;
			IL_1BA:
			writer.WriteValue((value == null) ? null : new int?((int)value));
			return;
			IL_1E1:
			writer.WriteValue((byte)value);
			return;
			IL_1EE:
			writer.WriteValue((value == null) ? null : new byte?((byte)value));
			return;
			IL_215:
			writer.WriteValue((uint)value);
			return;
			IL_222:
			writer.WriteValue((value == null) ? null : new uint?((uint)value));
			return;
			IL_249:
			writer.WriteValue((long)value);
			return;
			IL_256:
			writer.WriteValue((value == null) ? null : new long?((long)value));
			return;
			IL_27D:
			writer.WriteValue((ulong)value);
			return;
			IL_28A:
			writer.WriteValue((value == null) ? null : new ulong?((ulong)value));
			return;
			IL_2B1:
			writer.WriteValue((float)value);
			return;
			IL_2BE:
			writer.WriteValue((value == null) ? null : new float?((float)value));
			return;
			IL_2E5:
			writer.WriteValue((double)value);
			return;
			IL_2F2:
			writer.WriteValue((value == null) ? null : new double?((double)value));
			return;
			IL_319:
			writer.WriteValue((DateTime)value);
			return;
			IL_326:
			writer.WriteValue((value == null) ? null : new DateTime?((DateTime)value));
			return;
			IL_34D:
			writer.WriteValue((DateTimeOffset)value);
			return;
			IL_35A:
			writer.WriteValue((value == null) ? null : new DateTimeOffset?((DateTimeOffset)value));
			return;
			IL_381:
			writer.WriteValue((decimal)value);
			return;
			IL_38E:
			writer.WriteValue((value == null) ? null : new decimal?((decimal)value));
			return;
			IL_3B5:
			writer.WriteValue((Guid)value);
			return;
			IL_3C2:
			writer.WriteValue((value == null) ? null : new Guid?((Guid)value));
			return;
			IL_3E9:
			writer.WriteValue((TimeSpan)value);
			return;
			IL_3F6:
			writer.WriteValue((value == null) ? null : new TimeSpan?((TimeSpan)value));
			return;
			IL_41D:
			writer.WriteValue((System.Numerics.BigInteger)value);
			return;
			IL_42F:
			writer.WriteValue((value == null) ? null : new System.Numerics.BigInteger?((System.Numerics.BigInteger)value));
			return;
			IL_45B:
			writer.WriteValue((Uri)value);
			return;
			IL_468:
			writer.WriteValue((string)value);
			return;
			IL_475:
			writer.WriteValue((byte[])value);
			return;
			IL_482:
			writer.WriteNull();
			return;
			IL_4A8:
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			throw JsonWriter.CreateUnsupportedTypeException(writer, value);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00049414 File Offset: 0x00047614
		private static void ResolveConvertibleValue(IConvertible convertible, out PrimitiveTypeCode typeCode, out object value)
		{
			TypeInformation typeInformation = ConvertUtils.GetTypeInformation(convertible);
			typeCode = ((typeInformation.TypeCode == PrimitiveTypeCode.Object) ? PrimitiveTypeCode.String : typeInformation.TypeCode);
			Type conversionType = (typeInformation.TypeCode == PrimitiveTypeCode.Object) ? typeof(string) : typeInformation.Type;
			value = convertible.ToType(conversionType, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00049478 File Offset: 0x00047678
		private static JsonWriterException CreateUnsupportedTypeException(JsonWriter writer, object value)
		{
			return JsonWriterException.Create(writer, "Unsupported type: {0}. Use the JsonSerializer class to get the object's JSON representation.".FormatWith(CultureInfo.InvariantCulture, value.GetType()), null);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00049498 File Offset: 0x00047698
		protected void SetWriteState(JsonToken token, object value)
		{
			switch (token)
			{
			case JsonToken.StartObject:
				this.InternalWriteStart(token, JsonContainerType.Object);
				return;
			case JsonToken.StartArray:
				this.InternalWriteStart(token, JsonContainerType.Array);
				return;
			case JsonToken.StartConstructor:
				this.InternalWriteStart(token, JsonContainerType.Constructor);
				return;
			case JsonToken.PropertyName:
			{
				string text = value as string;
				if (text == null)
				{
					throw new ArgumentException("A name is required when setting property name state.", "value");
				}
				this.InternalWritePropertyName(text);
				return;
			}
			case JsonToken.Comment:
				this.InternalWriteComment();
				return;
			case JsonToken.Raw:
				this.InternalWriteRaw();
				return;
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.InternalWriteValue(token);
				return;
			case JsonToken.EndObject:
				this.InternalWriteEnd(JsonContainerType.Object);
				return;
			case JsonToken.EndArray:
				this.InternalWriteEnd(JsonContainerType.Array);
				return;
			case JsonToken.EndConstructor:
				this.InternalWriteEnd(JsonContainerType.Constructor);
				return;
			default:
				throw new ArgumentOutOfRangeException("token");
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00049574 File Offset: 0x00047774
		internal void InternalWriteEnd(JsonContainerType container)
		{
			this.AutoCompleteClose(container);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00049580 File Offset: 0x00047780
		internal void InternalWritePropertyName(string name)
		{
			this._currentPosition.PropertyName = name;
			this.AutoComplete(JsonToken.PropertyName);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00049598 File Offset: 0x00047798
		internal void InternalWriteRaw()
		{
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0004959C File Offset: 0x0004779C
		internal void InternalWriteStart(JsonToken token, JsonContainerType container)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(token);
			this.Push(container);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x000495B4 File Offset: 0x000477B4
		internal void InternalWriteValue(JsonToken token)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(token);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x000495C4 File Offset: 0x000477C4
		internal void InternalWriteWhitespace(string ws)
		{
			if (ws != null && !StringUtils.IsWhiteSpace(ws))
			{
				throw JsonWriterException.Create(this, "Only white space characters should be used.", null);
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000495E4 File Offset: 0x000477E4
		internal void InternalWriteComment()
		{
			this.AutoComplete(JsonToken.Comment);
		}

		// Token: 0x0400067E RID: 1662
		private static readonly JsonWriter.State[][] StateArray;

		// Token: 0x0400067F RID: 1663
		internal static readonly JsonWriter.State[][] StateArrayTempate = new JsonWriter.State[][]
		{
			new JsonWriter.State[]
			{
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Property,
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Object,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Array,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			}
		};

		// Token: 0x04000680 RID: 1664
		[Nullable(2)]
		private List<JsonPosition> _stack;

		// Token: 0x04000681 RID: 1665
		private JsonPosition _currentPosition;

		// Token: 0x04000682 RID: 1666
		private JsonWriter.State _currentState;

		// Token: 0x04000683 RID: 1667
		private Formatting _formatting;

		// Token: 0x04000686 RID: 1670
		private DateFormatHandling _dateFormatHandling;

		// Token: 0x04000687 RID: 1671
		private DateTimeZoneHandling _dateTimeZoneHandling;

		// Token: 0x04000688 RID: 1672
		private StringEscapeHandling _stringEscapeHandling;

		// Token: 0x04000689 RID: 1673
		private FloatFormatHandling _floatFormatHandling;

		// Token: 0x0400068A RID: 1674
		[Nullable(2)]
		private string _dateFormatString;

		// Token: 0x0400068B RID: 1675
		[Nullable(2)]
		private CultureInfo _culture;

		// Token: 0x020002B5 RID: 693
		[NullableContext(0)]
		internal enum State
		{
			// Token: 0x04000BC7 RID: 3015
			Start,
			// Token: 0x04000BC8 RID: 3016
			Property,
			// Token: 0x04000BC9 RID: 3017
			ObjectStart,
			// Token: 0x04000BCA RID: 3018
			Object,
			// Token: 0x04000BCB RID: 3019
			ArrayStart,
			// Token: 0x04000BCC RID: 3020
			Array,
			// Token: 0x04000BCD RID: 3021
			ConstructorStart,
			// Token: 0x04000BCE RID: 3022
			Constructor,
			// Token: 0x04000BCF RID: 3023
			Closed,
			// Token: 0x04000BD0 RID: 3024
			Error
		}
	}
}
