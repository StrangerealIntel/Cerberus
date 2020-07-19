using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200014D RID: 333
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonValidatingReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000C01 RID: 3073 RVA: 0x000463B8 File Offset: 0x000445B8
		// (remove) Token: 0x06000C02 RID: 3074 RVA: 0x000463F4 File Offset: 0x000445F4
		public event ValidationEventHandler ValidationEventHandler;

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00046430 File Offset: 0x00044630
		public override object Value
		{
			get
			{
				return this._reader.Value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00046440 File Offset: 0x00044640
		public override int Depth
		{
			get
			{
				return this._reader.Depth;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00046450 File Offset: 0x00044650
		public override string Path
		{
			get
			{
				return this._reader.Path;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00046460 File Offset: 0x00044660
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x00046470 File Offset: 0x00044670
		public override char QuoteChar
		{
			get
			{
				return this._reader.QuoteChar;
			}
			protected internal set
			{
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00046474 File Offset: 0x00044674
		public override JsonToken TokenType
		{
			get
			{
				return this._reader.TokenType;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00046484 File Offset: 0x00044684
		public override Type ValueType
		{
			get
			{
				return this._reader.ValueType;
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00046494 File Offset: 0x00044694
		private void Push(JsonValidatingReader.SchemaScope scope)
		{
			this._stack.Push(scope);
			this._currentScope = scope;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x000464AC File Offset: 0x000446AC
		private JsonValidatingReader.SchemaScope Pop()
		{
			JsonValidatingReader.SchemaScope result = this._stack.Pop();
			this._currentScope = ((this._stack.Count != 0) ? this._stack.Peek() : null);
			return result;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x000464F0 File Offset: 0x000446F0
		private IList<JsonSchemaModel> CurrentSchemas
		{
			get
			{
				return this._currentScope.Schemas;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00046500 File Offset: 0x00044700
		private IList<JsonSchemaModel> CurrentMemberSchemas
		{
			get
			{
				if (this._currentScope == null)
				{
					return new List<JsonSchemaModel>(new JsonSchemaModel[]
					{
						this._model
					});
				}
				if (this._currentScope.Schemas == null || this._currentScope.Schemas.Count == 0)
				{
					return JsonValidatingReader.EmptySchemaList;
				}
				switch (this._currentScope.TokenType)
				{
				case JTokenType.None:
					return this._currentScope.Schemas;
				case JTokenType.Object:
				{
					if (this._currentScope.CurrentPropertyName == null)
					{
						throw new JsonReaderException("CurrentPropertyName has not been set on scope.");
					}
					IList<JsonSchemaModel> list = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
					{
						JsonSchemaModel item;
						if (jsonSchemaModel.Properties != null && jsonSchemaModel.Properties.TryGetValue(this._currentScope.CurrentPropertyName, out item))
						{
							list.Add(item);
						}
						if (jsonSchemaModel.PatternProperties != null)
						{
							foreach (KeyValuePair<string, JsonSchemaModel> keyValuePair in jsonSchemaModel.PatternProperties)
							{
								if (Regex.IsMatch(this._currentScope.CurrentPropertyName, keyValuePair.Key))
								{
									list.Add(keyValuePair.Value);
								}
							}
						}
						if (list.Count == 0 && jsonSchemaModel.AllowAdditionalProperties && jsonSchemaModel.AdditionalProperties != null)
						{
							list.Add(jsonSchemaModel.AdditionalProperties);
						}
					}
					return list;
				}
				case JTokenType.Array:
				{
					IList<JsonSchemaModel> list2 = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel2 in this.CurrentSchemas)
					{
						if (!jsonSchemaModel2.PositionalItemsValidation)
						{
							if (jsonSchemaModel2.Items != null && jsonSchemaModel2.Items.Count > 0)
							{
								list2.Add(jsonSchemaModel2.Items[0]);
							}
						}
						else
						{
							if (jsonSchemaModel2.Items != null && jsonSchemaModel2.Items.Count > 0 && jsonSchemaModel2.Items.Count > this._currentScope.ArrayItemCount - 1)
							{
								list2.Add(jsonSchemaModel2.Items[this._currentScope.ArrayItemCount - 1]);
							}
							if (jsonSchemaModel2.AllowAdditionalItems && jsonSchemaModel2.AdditionalItems != null)
							{
								list2.Add(jsonSchemaModel2.AdditionalItems);
							}
						}
					}
					return list2;
				}
				case JTokenType.Constructor:
					return JsonValidatingReader.EmptySchemaList;
				default:
					throw new ArgumentOutOfRangeException("TokenType", "Unexpected token type: {0}".FormatWith(CultureInfo.InvariantCulture, this._currentScope.TokenType));
				}
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00046808 File Offset: 0x00044A08
		private void RaiseError(string message, JsonSchemaModel schema)
		{
			string message2 = ((IJsonLineInfo)this).HasLineInfo() ? (message + " Line {0}, position {1}.".FormatWith(CultureInfo.InvariantCulture, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition)) : message;
			this.OnValidationEvent(new JsonSchemaException(message2, null, this.Path, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition));
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00046878 File Offset: 0x00044A78
		private void OnValidationEvent(JsonSchemaException exception)
		{
			ValidationEventHandler validationEventHandler = this.ValidationEventHandler;
			if (validationEventHandler != null)
			{
				validationEventHandler(this, new ValidationEventArgs(exception));
				return;
			}
			throw exception;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x000468A8 File Offset: 0x00044AA8
		public JsonValidatingReader(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this._reader = reader;
			this._stack = new Stack<JsonValidatingReader.SchemaScope>();
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x000468D0 File Offset: 0x00044AD0
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x000468D8 File Offset: 0x00044AD8
		public JsonSchema Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				if (this.TokenType != JsonToken.None)
				{
					throw new InvalidOperationException("Cannot change schema while validating JSON.");
				}
				this._schema = value;
				this._model = null;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00046900 File Offset: 0x00044B00
		public JsonReader Reader
		{
			get
			{
				return this._reader;
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00046908 File Offset: 0x00044B08
		public override void Close()
		{
			base.Close();
			if (base.CloseInput)
			{
				JsonReader reader = this._reader;
				if (reader == null)
				{
					return;
				}
				reader.Close();
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00046930 File Offset: 0x00044B30
		private void ValidateNotDisallowed(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			JsonSchemaType? currentNodeSchemaType = this.GetCurrentNodeSchemaType();
			if (currentNodeSchemaType != null && JsonSchemaGenerator.HasFlag(new JsonSchemaType?(schema.Disallow), currentNodeSchemaType.GetValueOrDefault()))
			{
				this.RaiseError("Type {0} is disallowed.".FormatWith(CultureInfo.InvariantCulture, currentNodeSchemaType), schema);
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00046994 File Offset: 0x00044B94
		private JsonSchemaType? GetCurrentNodeSchemaType()
		{
			switch (this._reader.TokenType)
			{
			case JsonToken.StartObject:
				return new JsonSchemaType?(JsonSchemaType.Object);
			case JsonToken.StartArray:
				return new JsonSchemaType?(JsonSchemaType.Array);
			case JsonToken.Integer:
				return new JsonSchemaType?(JsonSchemaType.Integer);
			case JsonToken.Float:
				return new JsonSchemaType?(JsonSchemaType.Float);
			case JsonToken.String:
				return new JsonSchemaType?(JsonSchemaType.String);
			case JsonToken.Boolean:
				return new JsonSchemaType?(JsonSchemaType.Boolean);
			case JsonToken.Null:
				return new JsonSchemaType?(JsonSchemaType.Null);
			}
			return null;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00046A28 File Offset: 0x00044C28
		public override int? ReadAsInt32()
		{
			int? result = this._reader.ReadAsInt32();
			this.ValidateCurrentToken();
			return result;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00046A3C File Offset: 0x00044C3C
		public override byte[] ReadAsBytes()
		{
			byte[] result = this._reader.ReadAsBytes();
			this.ValidateCurrentToken();
			return result;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00046A50 File Offset: 0x00044C50
		public override decimal? ReadAsDecimal()
		{
			decimal? result = this._reader.ReadAsDecimal();
			this.ValidateCurrentToken();
			return result;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00046A64 File Offset: 0x00044C64
		public override double? ReadAsDouble()
		{
			double? result = this._reader.ReadAsDouble();
			this.ValidateCurrentToken();
			return result;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00046A78 File Offset: 0x00044C78
		public override bool? ReadAsBoolean()
		{
			bool? result = this._reader.ReadAsBoolean();
			this.ValidateCurrentToken();
			return result;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00046A8C File Offset: 0x00044C8C
		public override string ReadAsString()
		{
			string result = this._reader.ReadAsString();
			this.ValidateCurrentToken();
			return result;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00046AA0 File Offset: 0x00044CA0
		public override DateTime? ReadAsDateTime()
		{
			DateTime? result = this._reader.ReadAsDateTime();
			this.ValidateCurrentToken();
			return result;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00046AB4 File Offset: 0x00044CB4
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? result = this._reader.ReadAsDateTimeOffset();
			this.ValidateCurrentToken();
			return result;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00046AC8 File Offset: 0x00044CC8
		public override bool Read()
		{
			if (!this._reader.Read())
			{
				return false;
			}
			if (this._reader.TokenType == JsonToken.Comment)
			{
				return true;
			}
			this.ValidateCurrentToken();
			return true;
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00046AF8 File Offset: 0x00044CF8
		private void ValidateCurrentToken()
		{
			if (this._model == null)
			{
				JsonSchemaModelBuilder jsonSchemaModelBuilder = new JsonSchemaModelBuilder();
				this._model = jsonSchemaModelBuilder.Build(this._schema);
				if (!JsonTokenUtils.IsStartToken(this._reader.TokenType))
				{
					this.Push(new JsonValidatingReader.SchemaScope(JTokenType.None, this.CurrentMemberSchemas));
				}
			}
			switch (this._reader.TokenType)
			{
			case JsonToken.None:
				return;
			case JsonToken.StartObject:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> schemas = this.CurrentMemberSchemas.Where(new Func<JsonSchemaModel, bool>(this.ValidateObject)).ToList<JsonSchemaModel>();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Object, schemas));
				this.WriteToken(this.CurrentSchemas);
				return;
			}
			case JsonToken.StartArray:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> schemas2 = this.CurrentMemberSchemas.Where(new Func<JsonSchemaModel, bool>(this.ValidateArray)).ToList<JsonSchemaModel>();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Array, schemas2));
				this.WriteToken(this.CurrentSchemas);
				return;
			}
			case JsonToken.StartConstructor:
				this.ProcessValue();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Constructor, null));
				this.WriteToken(this.CurrentSchemas);
				return;
			case JsonToken.PropertyName:
				this.WriteToken(this.CurrentSchemas);
				using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentSchemas.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonSchemaModel schema = enumerator.Current;
						this.ValidatePropertyName(schema);
					}
					return;
				}
				break;
			case JsonToken.Comment:
				goto IL_3F9;
			case JsonToken.Raw:
				break;
			case JsonToken.Integer:
				this.ProcessValue();
				this.WriteToken(this.CurrentMemberSchemas);
				using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonSchemaModel schema2 = enumerator.Current;
						this.ValidateInteger(schema2);
					}
					return;
				}
				goto IL_1E8;
			case JsonToken.Float:
				goto IL_1E8;
			case JsonToken.String:
				goto IL_23A;
			case JsonToken.Boolean:
				goto IL_28C;
			case JsonToken.Null:
				goto IL_2DE;
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.WriteToken(this.CurrentMemberSchemas);
				return;
			case JsonToken.EndObject:
				goto IL_330;
			case JsonToken.EndArray:
				this.WriteToken(this.CurrentSchemas);
				foreach (JsonSchemaModel schema3 in this.CurrentSchemas)
				{
					this.ValidateEndArray(schema3);
				}
				this.Pop();
				return;
			case JsonToken.EndConstructor:
				this.WriteToken(this.CurrentSchemas);
				this.Pop();
				return;
			default:
				goto IL_3F9;
			}
			this.ProcessValue();
			return;
			IL_1E8:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel schema4 = enumerator.Current;
					this.ValidateFloat(schema4);
				}
				return;
			}
			IL_23A:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel schema5 = enumerator.Current;
					this.ValidateString(schema5);
				}
				return;
			}
			IL_28C:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel schema6 = enumerator.Current;
					this.ValidateBoolean(schema6);
				}
				return;
			}
			IL_2DE:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel schema7 = enumerator.Current;
					this.ValidateNull(schema7);
				}
				return;
			}
			IL_330:
			this.WriteToken(this.CurrentSchemas);
			foreach (JsonSchemaModel schema8 in this.CurrentSchemas)
			{
				this.ValidateEndObject(schema8);
			}
			this.Pop();
			return;
			IL_3F9:
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00046F68 File Offset: 0x00045168
		private void WriteToken(IList<JsonSchemaModel> schemas)
		{
			foreach (JsonValidatingReader.SchemaScope schemaScope in this._stack)
			{
				bool flag = schemaScope.TokenType == JTokenType.Array && schemaScope.IsUniqueArray && schemaScope.ArrayItemCount > 0;
				if (!flag)
				{
					if (!schemas.Any((JsonSchemaModel s) => s.Enum != null))
					{
						continue;
					}
				}
				if (schemaScope.CurrentItemWriter == null)
				{
					if (JsonTokenUtils.IsEndToken(this._reader.TokenType))
					{
						continue;
					}
					schemaScope.CurrentItemWriter = new JTokenWriter();
				}
				schemaScope.CurrentItemWriter.WriteToken(this._reader, false);
				if (schemaScope.CurrentItemWriter.Top == 0 && this._reader.TokenType != JsonToken.PropertyName)
				{
					JToken token = schemaScope.CurrentItemWriter.Token;
					schemaScope.CurrentItemWriter = null;
					if (flag)
					{
						if (schemaScope.UniqueArrayItems.Contains(token, JToken.EqualityComparer))
						{
							this.RaiseError("Non-unique array item at index {0}.".FormatWith(CultureInfo.InvariantCulture, schemaScope.ArrayItemCount - 1), schemaScope.Schemas.First((JsonSchemaModel s) => s.UniqueItems));
						}
						schemaScope.UniqueArrayItems.Add(token);
					}
					else if (schemas.Any((JsonSchemaModel s) => s.Enum != null))
					{
						foreach (JsonSchemaModel jsonSchemaModel in schemas)
						{
							if (jsonSchemaModel.Enum != null && !jsonSchemaModel.Enum.ContainsValue(token, JToken.EqualityComparer))
							{
								StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
								token.WriteTo(new JsonTextWriter(stringWriter), new JsonConverter[0]);
								this.RaiseError("Value {0} is not defined in enum.".FormatWith(CultureInfo.InvariantCulture, stringWriter.ToString()), jsonSchemaModel);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000471E8 File Offset: 0x000453E8
		private void ValidateEndObject(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			Dictionary<string, bool> requiredProperties = this._currentScope.RequiredProperties;
			if (requiredProperties != null)
			{
				if (requiredProperties.Values.Any((bool v) => !v))
				{
					IEnumerable<string> values = from kv in requiredProperties
					where !kv.Value
					select kv.Key;
					this.RaiseError("Required properties are missing from object: {0}.".FormatWith(CultureInfo.InvariantCulture, string.Join(", ", values)), schema);
				}
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x000472B8 File Offset: 0x000454B8
		private void ValidateEndArray(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			int arrayItemCount = this._currentScope.ArrayItemCount;
			if (schema.MaximumItems != null)
			{
				int num = arrayItemCount;
				int? num2 = schema.MaximumItems;
				if (num > num2.GetValueOrDefault() & num2 != null)
				{
					this.RaiseError("Array item count {0} exceeds maximum count of {1}.".FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MaximumItems), schema);
				}
			}
			if (schema.MinimumItems != null)
			{
				int num3 = arrayItemCount;
				int? num2 = schema.MinimumItems;
				if (num3 < num2.GetValueOrDefault() & num2 != null)
				{
					this.RaiseError("Array item count {0} is less than minimum count of {1}.".FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MinimumItems), schema);
				}
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0004738C File Offset: 0x0004558C
		private void ValidateNull(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Null))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x000473AC File Offset: 0x000455AC
		private void ValidateBoolean(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Boolean))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000473CC File Offset: 0x000455CC
		private void ValidateString(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.String))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			string text = this._reader.Value.ToString();
			if (schema.MaximumLength != null)
			{
				int length = text.Length;
				int? num = schema.MaximumLength;
				if (length > num.GetValueOrDefault() & num != null)
				{
					this.RaiseError("String '{0}' exceeds maximum length of {1}.".FormatWith(CultureInfo.InvariantCulture, text, schema.MaximumLength), schema);
				}
			}
			if (schema.MinimumLength != null)
			{
				int length2 = text.Length;
				int? num = schema.MinimumLength;
				if (length2 < num.GetValueOrDefault() & num != null)
				{
					this.RaiseError("String '{0}' is less than minimum length of {1}.".FormatWith(CultureInfo.InvariantCulture, text, schema.MinimumLength), schema);
				}
			}
			if (schema.Patterns != null)
			{
				foreach (string text2 in schema.Patterns)
				{
					if (!Regex.IsMatch(text, text2))
					{
						this.RaiseError("String '{0}' does not match regex pattern '{1}'.".FormatWith(CultureInfo.InvariantCulture, text, text2), schema);
					}
				}
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00047528 File Offset: 0x00045728
		private void ValidateInteger(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Integer))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			object value = this._reader.Value;
			if (schema.Maximum != null)
			{
				if (JValue.Compare(JTokenType.Integer, value, schema.Maximum) > 0)
				{
					this.RaiseError("Integer {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum && JValue.Compare(JTokenType.Integer, value, schema.Maximum) == 0)
				{
					this.RaiseError("Integer {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
			}
			if (schema.Minimum != null)
			{
				if (JValue.Compare(JTokenType.Integer, value, schema.Minimum) < 0)
				{
					this.RaiseError("Integer {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum && JValue.Compare(JTokenType.Integer, value, schema.Minimum) == 0)
				{
					this.RaiseError("Integer {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
			}
			if (schema.DivisibleBy != null)
			{
				bool flag;
				if (value is System.Numerics.BigInteger)
				{
					System.Numerics.BigInteger bigInteger = (System.Numerics.BigInteger)value;
					if (!Math.Abs(schema.DivisibleBy.Value - Math.Truncate(schema.DivisibleBy.Value)).Equals(0.0))
					{
						flag = (bigInteger != 0L);
					}
					else
					{
						flag = (bigInteger % new System.Numerics.BigInteger(schema.DivisibleBy.Value) != 0L);
					}
				}
				else
				{
					flag = !JsonValidatingReader.IsZero((double)Convert.ToInt64(value, CultureInfo.InvariantCulture) % schema.DivisibleBy.GetValueOrDefault());
				}
				if (flag)
				{
					this.RaiseError("Integer {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(value), schema.DivisibleBy), schema);
				}
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00047774 File Offset: 0x00045974
		private void ProcessValue()
		{
			if (this._currentScope != null && this._currentScope.TokenType == JTokenType.Array)
			{
				JsonValidatingReader.SchemaScope currentScope = this._currentScope;
				int arrayItemCount = currentScope.ArrayItemCount;
				currentScope.ArrayItemCount = arrayItemCount + 1;
				foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
				{
					if (jsonSchemaModel != null && jsonSchemaModel.PositionalItemsValidation && !jsonSchemaModel.AllowAdditionalItems && (jsonSchemaModel.Items == null || this._currentScope.ArrayItemCount - 1 >= jsonSchemaModel.Items.Count))
					{
						this.RaiseError("Index {0} has not been defined and the schema does not allow additional items.".FormatWith(CultureInfo.InvariantCulture, this._currentScope.ArrayItemCount), jsonSchemaModel);
					}
				}
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00047860 File Offset: 0x00045A60
		private void ValidateFloat(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Float))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			double num = Convert.ToDouble(this._reader.Value, CultureInfo.InvariantCulture);
			if (schema.Maximum != null)
			{
				double num2 = num;
				double? num3 = schema.Maximum;
				if (num2 > num3.GetValueOrDefault() & num3 != null)
				{
					this.RaiseError("Float {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum)
				{
					double num4 = num;
					num3 = schema.Maximum;
					if (num4 == num3.GetValueOrDefault() & num3 != null)
					{
						this.RaiseError("Float {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Maximum), schema);
					}
				}
			}
			if (schema.Minimum != null)
			{
				double num5 = num;
				double? num3 = schema.Minimum;
				if (num5 < num3.GetValueOrDefault() & num3 != null)
				{
					this.RaiseError("Float {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum)
				{
					double num6 = num;
					num3 = schema.Minimum;
					if (num6 == num3.GetValueOrDefault() & num3 != null)
					{
						this.RaiseError("Float {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Minimum), schema);
					}
				}
			}
			if (schema.DivisibleBy != null && !JsonValidatingReader.IsZero(JsonValidatingReader.FloatingPointRemainder(num, schema.DivisibleBy.GetValueOrDefault())))
			{
				this.RaiseError("Float {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.DivisibleBy), schema);
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00047A4C File Offset: 0x00045C4C
		private static double FloatingPointRemainder(double dividend, double divisor)
		{
			return dividend - Math.Floor(dividend / divisor) * divisor;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00047A5C File Offset: 0x00045C5C
		private static bool IsZero(double value)
		{
			return Math.Abs(value) < 4.4408920985006262E-15;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00047A70 File Offset: 0x00045C70
		private void ValidatePropertyName(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			string text = Convert.ToString(this._reader.Value, CultureInfo.InvariantCulture);
			if (this._currentScope.RequiredProperties.ContainsKey(text))
			{
				this._currentScope.RequiredProperties[text] = true;
			}
			if (!schema.AllowAdditionalProperties && !this.IsPropertyDefinied(schema, text))
			{
				this.RaiseError("Property '{0}' has not been defined and the schema does not allow additional properties.".FormatWith(CultureInfo.InvariantCulture, text), schema);
			}
			this._currentScope.CurrentPropertyName = text;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00047B04 File Offset: 0x00045D04
		private bool IsPropertyDefinied(JsonSchemaModel schema, string propertyName)
		{
			if (schema.Properties != null && schema.Properties.ContainsKey(propertyName))
			{
				return true;
			}
			if (schema.PatternProperties != null)
			{
				foreach (string pattern in schema.PatternProperties.Keys)
				{
					if (Regex.IsMatch(propertyName, pattern))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00047B98 File Offset: 0x00045D98
		private bool ValidateArray(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Array);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00047BAC File Offset: 0x00045DAC
		private bool ValidateObject(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Object);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00047BC0 File Offset: 0x00045DC0
		private bool TestType(JsonSchemaModel currentSchema, JsonSchemaType currentType)
		{
			if (!JsonSchemaGenerator.HasFlag(new JsonSchemaType?(currentSchema.Type), currentType))
			{
				this.RaiseError("Invalid type. Expected {0} but got {1}.".FormatWith(CultureInfo.InvariantCulture, currentSchema.Type, currentType), currentSchema);
				return false;
			}
			return true;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00047C14 File Offset: 0x00045E14
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00047C40 File Offset: 0x00045E40
		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x00047C6C File Offset: 0x00045E6C
		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Token: 0x04000677 RID: 1655
		private readonly JsonReader _reader;

		// Token: 0x04000678 RID: 1656
		private readonly Stack<JsonValidatingReader.SchemaScope> _stack;

		// Token: 0x04000679 RID: 1657
		private JsonSchema _schema;

		// Token: 0x0400067A RID: 1658
		private JsonSchemaModel _model;

		// Token: 0x0400067B RID: 1659
		private JsonValidatingReader.SchemaScope _currentScope;

		// Token: 0x0400067D RID: 1661
		private static readonly IList<JsonSchemaModel> EmptySchemaList = new List<JsonSchemaModel>();

		// Token: 0x020002B3 RID: 691
		private class SchemaScope
		{
			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x06001783 RID: 6019 RVA: 0x00078378 File Offset: 0x00076578
			// (set) Token: 0x06001784 RID: 6020 RVA: 0x00078380 File Offset: 0x00076580
			public string CurrentPropertyName { get; set; }

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x06001785 RID: 6021 RVA: 0x0007838C File Offset: 0x0007658C
			// (set) Token: 0x06001786 RID: 6022 RVA: 0x00078394 File Offset: 0x00076594
			public int ArrayItemCount { get; set; }

			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x06001787 RID: 6023 RVA: 0x000783A0 File Offset: 0x000765A0
			public bool IsUniqueArray { get; }

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x06001788 RID: 6024 RVA: 0x000783A8 File Offset: 0x000765A8
			public IList<JToken> UniqueArrayItems { get; }

			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x06001789 RID: 6025 RVA: 0x000783B0 File Offset: 0x000765B0
			// (set) Token: 0x0600178A RID: 6026 RVA: 0x000783B8 File Offset: 0x000765B8
			public JTokenWriter CurrentItemWriter { get; set; }

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x0600178B RID: 6027 RVA: 0x000783C4 File Offset: 0x000765C4
			public IList<JsonSchemaModel> Schemas
			{
				get
				{
					return this._schemas;
				}
			}

			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x0600178C RID: 6028 RVA: 0x000783CC File Offset: 0x000765CC
			public Dictionary<string, bool> RequiredProperties
			{
				get
				{
					return this._requiredProperties;
				}
			}

			// Token: 0x170004D3 RID: 1235
			// (get) Token: 0x0600178D RID: 6029 RVA: 0x000783D4 File Offset: 0x000765D4
			public JTokenType TokenType
			{
				get
				{
					return this._tokenType;
				}
			}

			// Token: 0x0600178E RID: 6030 RVA: 0x000783DC File Offset: 0x000765DC
			public SchemaScope(JTokenType tokenType, IList<JsonSchemaModel> schemas)
			{
				this._tokenType = tokenType;
				this._schemas = schemas;
				this._requiredProperties = schemas.SelectMany(new Func<JsonSchemaModel, IEnumerable<string>>(this.GetRequiredProperties)).Distinct<string>().ToDictionary((string p) => p, (string p) => false);
				if (tokenType == JTokenType.Array)
				{
					if (schemas.Any((JsonSchemaModel s) => s.UniqueItems))
					{
						this.IsUniqueArray = 1;
						this.UniqueArrayItems = new List<JToken>();
					}
				}
			}

			// Token: 0x0600178F RID: 6031 RVA: 0x000784B0 File Offset: 0x000766B0
			private IEnumerable<string> GetRequiredProperties(JsonSchemaModel schema)
			{
				if (((schema != null) ? schema.Properties : null) == null)
				{
					return Enumerable.Empty<string>();
				}
				return from p in schema.Properties
				where p.Value.Required
				select p.Key;
			}

			// Token: 0x04000BB7 RID: 2999
			private readonly JTokenType _tokenType;

			// Token: 0x04000BB8 RID: 3000
			private readonly IList<JsonSchemaModel> _schemas;

			// Token: 0x04000BB9 RID: 3001
			private readonly Dictionary<string, bool> _requiredProperties;
		}
	}
}
