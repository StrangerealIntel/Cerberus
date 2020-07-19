using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001B0 RID: 432
	[NullableContext(1)]
	[Nullable(0)]
	internal class JsonSerializerInternalReader : JsonSerializerInternalBase
	{
		// Token: 0x06000FE6 RID: 4070 RVA: 0x00058090 File Offset: 0x00056290
		public JsonSerializerInternalReader(JsonSerializer serializer) : base(serializer)
		{
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0005809C File Offset: 0x0005629C
		public void Populate(JsonReader reader, object target)
		{
			ValidationUtils.ArgumentNotNull(target, "target");
			Type type = target.GetType();
			JsonContract jsonContract = this.Serializer._contractResolver.ResolveContract(type);
			if (!reader.MoveToContent())
			{
				throw JsonSerializationException.Create(reader, "No JSON content found.");
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				if (jsonContract.ContractType == JsonContractType.Array)
				{
					JsonArrayContract jsonArrayContract = (JsonArrayContract)jsonContract;
					IList list;
					if (!jsonArrayContract.ShouldCreateWrapper)
					{
						list = (IList)target;
					}
					else
					{
						IList list2 = jsonArrayContract.CreateWrapper(target);
						list = list2;
					}
					this.PopulateList(list, reader, jsonArrayContract, null, null);
					return;
				}
				throw JsonSerializationException.Create(reader, "Cannot populate JSON array onto type '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
			}
			else
			{
				if (reader.TokenType != JsonToken.StartObject)
				{
					throw JsonSerializationException.Create(reader, "Unexpected initial token '{0}' when populating object. Expected JSON object or array.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
				}
				reader.ReadAndAssert();
				string id = null;
				if (this.Serializer.MetadataPropertyHandling != MetadataPropertyHandling.Ignore && reader.TokenType == JsonToken.PropertyName && string.Equals(reader.Value.ToString(), "$id", StringComparison.Ordinal))
				{
					reader.ReadAndAssert();
					object value = reader.Value;
					id = ((value != null) ? value.ToString() : null);
					reader.ReadAndAssert();
				}
				if (jsonContract.ContractType == JsonContractType.Dictionary)
				{
					JsonDictionaryContract jsonDictionaryContract = (JsonDictionaryContract)jsonContract;
					IDictionary dictionary;
					if (!jsonDictionaryContract.ShouldCreateWrapper)
					{
						dictionary = (IDictionary)target;
					}
					else
					{
						IDictionary dictionary2 = jsonDictionaryContract.CreateWrapper(target);
						dictionary = dictionary2;
					}
					this.PopulateDictionary(dictionary, reader, jsonDictionaryContract, null, id);
					return;
				}
				if (jsonContract.ContractType == JsonContractType.Object)
				{
					this.PopulateObject(target, reader, (JsonObjectContract)jsonContract, null, id);
					return;
				}
				throw JsonSerializationException.Create(reader, "Cannot populate JSON object onto type '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
			}
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00058258 File Offset: 0x00056458
		[NullableContext(2)]
		private JsonContract GetContractSafe(Type type)
		{
			if (type == null)
			{
				return null;
			}
			return this.GetContract(type);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00058270 File Offset: 0x00056470
		private JsonContract GetContract(Type type)
		{
			return this.Serializer._contractResolver.ResolveContract(type);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00058284 File Offset: 0x00056484
		[NullableContext(2)]
		public object Deserialize([Nullable(1)] JsonReader reader, Type objectType, bool checkAdditionalContent)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			JsonContract contractSafe = this.GetContractSafe(objectType);
			object result;
			try
			{
				JsonConverter converter = this.GetConverter(contractSafe, null, null, null);
				if (reader.TokenType == JsonToken.None && !reader.ReadForType(contractSafe, converter != null))
				{
					if (contractSafe != null && !contractSafe.IsNullable)
					{
						throw JsonSerializationException.Create(reader, "No JSON content found and type '{0}' is not nullable.".FormatWith(CultureInfo.InvariantCulture, contractSafe.UnderlyingType));
					}
					result = null;
				}
				else
				{
					object obj;
					if (converter != null && converter.CanRead)
					{
						obj = this.DeserializeConvertable(converter, reader, objectType, null);
					}
					else
					{
						obj = this.CreateValueInternal(reader, objectType, contractSafe, null, null, null, null);
					}
					if (checkAdditionalContent)
					{
						while (reader.Read())
						{
							if (reader.TokenType != JsonToken.Comment)
							{
								throw JsonSerializationException.Create(reader, "Additional text found in JSON string after finishing deserializing object.");
							}
						}
					}
					result = obj;
				}
			}
			catch (Exception ex)
			{
				if (!base.IsErrorHandled(null, contractSafe, null, reader as IJsonLineInfo, reader.Path, ex))
				{
					base.ClearErrorContext();
					throw;
				}
				this.HandleError(reader, false, 0);
				result = null;
			}
			return result;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x000583AC File Offset: 0x000565AC
		private JsonSerializerProxy GetInternalSerializer()
		{
			if (this.InternalSerializer == null)
			{
				this.InternalSerializer = new JsonSerializerProxy(this);
			}
			return this.InternalSerializer;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x000583CC File Offset: 0x000565CC
		[NullableContext(2)]
		private JToken CreateJToken([Nullable(1)] JsonReader reader, JsonContract contract)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (contract != null)
			{
				if (contract.UnderlyingType == typeof(JRaw))
				{
					return JRaw.Create(reader);
				}
				if (reader.TokenType == JsonToken.Null && !(contract.UnderlyingType == typeof(JValue)) && !(contract.UnderlyingType == typeof(JToken)))
				{
					return null;
				}
			}
			JToken token;
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jtokenWriter.WriteToken(reader);
				token = jtokenWriter.Token;
			}
			return token;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00058488 File Offset: 0x00056688
		private JToken CreateJObject(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jtokenWriter.WriteStartObject();
				for (;;)
				{
					if (reader.TokenType == JsonToken.PropertyName)
					{
						string text = (string)reader.Value;
						if (!reader.ReadAndMoveToContent())
						{
							goto IL_83;
						}
						if (!this.CheckPropertyName(reader, text))
						{
							jtokenWriter.WritePropertyName(text);
							jtokenWriter.WriteToken(reader, true, true, false);
						}
					}
					else if (reader.TokenType != JsonToken.Comment)
					{
						break;
					}
					if (!reader.Read())
					{
						goto IL_83;
					}
				}
				jtokenWriter.WriteEndObject();
				return jtokenWriter.Token;
				IL_83:
				throw JsonSerializationException.Create(reader, "Unexpected end when deserializing object.");
			}
			JToken result;
			return result;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00058544 File Offset: 0x00056744
		[NullableContext(2)]
		private object CreateValueInternal([Nullable(1)] JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, object existingValue)
		{
			if (contract != null && contract.ContractType == JsonContractType.Linq)
			{
				return this.CreateJToken(reader, contract);
			}
			for (;;)
			{
				switch (reader.TokenType)
				{
				case JsonToken.StartObject:
					goto IL_73;
				case JsonToken.StartArray:
					goto IL_85;
				case JsonToken.StartConstructor:
					goto IL_F0;
				case JsonToken.Comment:
					if (!reader.Read())
					{
						goto Block_7;
					}
					continue;
				case JsonToken.Raw:
					goto IL_13C;
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
					goto IL_94;
				case JsonToken.String:
					goto IL_A9;
				case JsonToken.Null:
				case JsonToken.Undefined:
					goto IL_10C;
				}
				break;
			}
			goto IL_14D;
			IL_73:
			return this.CreateObject(reader, objectType, contract, member, containerContract, containerMember, existingValue);
			IL_85:
			return this.CreateList(reader, objectType, contract, member, existingValue, null);
			IL_94:
			return this.EnsureType(reader, reader.Value, CultureInfo.InvariantCulture, contract, objectType);
			IL_A9:
			string text = (string)reader.Value;
			if (objectType == typeof(byte[]))
			{
				return Convert.FromBase64String(text);
			}
			if (JsonSerializerInternalReader.CoerceEmptyStringToNull(objectType, contract, text))
			{
				return null;
			}
			return this.EnsureType(reader, text, CultureInfo.InvariantCulture, contract, objectType);
			IL_F0:
			string value = reader.Value.ToString();
			return this.EnsureType(reader, value, CultureInfo.InvariantCulture, contract, objectType);
			IL_10C:
			if (objectType == typeof(DBNull))
			{
				return DBNull.Value;
			}
			return this.EnsureType(reader, reader.Value, CultureInfo.InvariantCulture, contract, objectType);
			IL_13C:
			return new JRaw((string)reader.Value);
			IL_14D:
			throw JsonSerializationException.Create(reader, "Unexpected token while deserializing object: " + reader.TokenType.ToString());
			Block_7:
			throw JsonSerializationException.Create(reader, "Unexpected end when deserializing object.");
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x000586E0 File Offset: 0x000568E0
		[NullableContext(2)]
		private static bool CoerceEmptyStringToNull(Type objectType, JsonContract contract, [Nullable(1)] string s)
		{
			return StringUtils.IsNullOrEmpty(s) && objectType != null && objectType != typeof(string) && objectType != typeof(object) && contract != null && contract.IsNullable;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00058740 File Offset: 0x00056940
		internal string GetExpectedDescription(JsonContract contract)
		{
			switch (contract.ContractType)
			{
			case JsonContractType.Object:
			case JsonContractType.Dictionary:
			case JsonContractType.Dynamic:
			case JsonContractType.Serializable:
				return "JSON object (e.g. {\"name\":\"value\"})";
			case JsonContractType.Array:
				return "JSON array (e.g. [1,2,3])";
			case JsonContractType.Primitive:
				return "JSON primitive value (e.g. string, number, boolean, null)";
			case JsonContractType.String:
				return "JSON string value";
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x000587A0 File Offset: 0x000569A0
		[NullableContext(2)]
		private JsonConverter GetConverter(JsonContract contract, JsonConverter memberConverter, JsonContainerContract containerContract, JsonProperty containerProperty)
		{
			JsonConverter result = null;
			if (memberConverter != null)
			{
				result = memberConverter;
			}
			else if (((containerProperty != null) ? containerProperty.ItemConverter : null) != null)
			{
				result = containerProperty.ItemConverter;
			}
			else if (((containerContract != null) ? containerContract.ItemConverter : null) != null)
			{
				result = containerContract.ItemConverter;
			}
			else if (contract != null)
			{
				if (contract.Converter != null)
				{
					result = contract.Converter;
				}
				else
				{
					JsonConverter matchingConverter = this.Serializer.GetMatchingConverter(contract.UnderlyingType);
					if (matchingConverter != null)
					{
						result = matchingConverter;
					}
					else if (contract.InternalConverter != null)
					{
						result = contract.InternalConverter;
					}
				}
			}
			return result;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00058858 File Offset: 0x00056A58
		[NullableContext(2)]
		private object CreateObject([Nullable(1)] JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, object existingValue)
		{
			Type type = objectType;
			string text;
			if (this.Serializer.MetadataPropertyHandling == MetadataPropertyHandling.Ignore)
			{
				reader.ReadAndAssert();
				text = null;
			}
			else if (this.Serializer.MetadataPropertyHandling == MetadataPropertyHandling.ReadAhead)
			{
				JTokenReader jtokenReader = reader as JTokenReader;
				if (jtokenReader == null)
				{
					jtokenReader = (JTokenReader)JToken.ReadFrom(reader).CreateReader();
					jtokenReader.Culture = reader.Culture;
					jtokenReader.DateFormatString = reader.DateFormatString;
					jtokenReader.DateParseHandling = reader.DateParseHandling;
					jtokenReader.DateTimeZoneHandling = reader.DateTimeZoneHandling;
					jtokenReader.FloatParseHandling = reader.FloatParseHandling;
					jtokenReader.SupportMultipleContent = reader.SupportMultipleContent;
					jtokenReader.ReadAndAssert();
					reader = jtokenReader;
				}
				object result;
				if (this.ReadMetadataPropertiesToken(jtokenReader, ref type, ref contract, member, containerContract, containerMember, existingValue, out result, out text))
				{
					return result;
				}
			}
			else
			{
				reader.ReadAndAssert();
				object result2;
				if (this.ReadMetadataProperties(reader, ref type, ref contract, member, containerContract, containerMember, existingValue, out result2, out text))
				{
					return result2;
				}
			}
			if (this.HasNoDefinedType(contract))
			{
				return this.CreateJObject(reader);
			}
			switch (contract.ContractType)
			{
			case JsonContractType.Object:
			{
				bool flag = false;
				JsonObjectContract jsonObjectContract = (JsonObjectContract)contract;
				object obj;
				if (existingValue != null && (type == objectType || type.IsAssignableFrom(existingValue.GetType())))
				{
					obj = existingValue;
				}
				else
				{
					obj = this.CreateNewObject(reader, jsonObjectContract, member, containerMember, text, out flag);
				}
				if (flag)
				{
					return obj;
				}
				return this.PopulateObject(obj, reader, jsonObjectContract, member, text);
			}
			case JsonContractType.Primitive:
			{
				JsonPrimitiveContract contract2 = (JsonPrimitiveContract)contract;
				if (this.Serializer.MetadataPropertyHandling != MetadataPropertyHandling.Ignore && reader.TokenType == JsonToken.PropertyName && string.Equals(reader.Value.ToString(), "$value", StringComparison.Ordinal))
				{
					reader.ReadAndAssert();
					if (reader.TokenType == JsonToken.StartObject)
					{
						throw JsonSerializationException.Create(reader, "Unexpected token when deserializing primitive value: " + reader.TokenType.ToString());
					}
					object result3 = this.CreateValueInternal(reader, type, contract2, member, null, null, existingValue);
					reader.ReadAndAssert();
					return result3;
				}
				break;
			}
			case JsonContractType.Dictionary:
			{
				JsonDictionaryContract jsonDictionaryContract = (JsonDictionaryContract)contract;
				object result4;
				if (existingValue == null)
				{
					bool flag2;
					IDictionary dictionary = this.CreateNewDictionary(reader, jsonDictionaryContract, out flag2);
					if (flag2)
					{
						if (text != null)
						{
							throw JsonSerializationException.Create(reader, "Cannot preserve reference to readonly dictionary, or dictionary created from a non-default constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
						}
						if (contract.OnSerializingCallbacks.Count > 0)
						{
							throw JsonSerializationException.Create(reader, "Cannot call OnSerializing on readonly dictionary, or dictionary created from a non-default constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
						}
						if (contract.OnErrorCallbacks.Count > 0)
						{
							throw JsonSerializationException.Create(reader, "Cannot call OnError on readonly list, or dictionary created from a non-default constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
						}
						if (!jsonDictionaryContract.HasParameterizedCreatorInternal)
						{
							throw JsonSerializationException.Create(reader, "Cannot deserialize readonly or fixed size dictionary: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
						}
					}
					this.PopulateDictionary(dictionary, reader, jsonDictionaryContract, member, text);
					if (flag2)
					{
						return (jsonDictionaryContract.OverrideCreator ?? jsonDictionaryContract.ParameterizedCreator)(new object[]
						{
							dictionary
						});
					}
					IWrappedDictionary wrappedDictionary = dictionary as IWrappedDictionary;
					if (wrappedDictionary != null)
					{
						return wrappedDictionary.UnderlyingDictionary;
					}
					result4 = dictionary;
				}
				else
				{
					IDictionary dictionary2;
					if (!jsonDictionaryContract.ShouldCreateWrapper && existingValue is IDictionary)
					{
						dictionary2 = (IDictionary)existingValue;
					}
					else
					{
						IDictionary dictionary3 = jsonDictionaryContract.CreateWrapper(existingValue);
						dictionary2 = dictionary3;
					}
					result4 = this.PopulateDictionary(dictionary2, reader, jsonDictionaryContract, member, text);
				}
				return result4;
			}
			case JsonContractType.Dynamic:
			{
				JsonDynamicContract contract3 = (JsonDynamicContract)contract;
				return this.CreateDynamic(reader, contract3, member, text);
			}
			case JsonContractType.Serializable:
			{
				JsonISerializableContract contract4 = (JsonISerializableContract)contract;
				return this.CreateISerializable(reader, contract4, member, text);
			}
			}
			string text2 = "Cannot deserialize the current JSON object (e.g. {{\"name\":\"value\"}}) into type '{0}' because the type requires a {1} to deserialize correctly." + Environment.NewLine + "To fix this error either change the JSON to a {1} or change the deserialized type so that it is a normal .NET type (e.g. not a primitive type like integer, not a collection type like an array or List<T>) that can be deserialized from a JSON object. JsonObjectAttribute can also be added to the type to force it to deserialize from a JSON object." + Environment.NewLine;
			text2 = text2.FormatWith(CultureInfo.InvariantCulture, type, this.GetExpectedDescription(contract));
			throw JsonSerializationException.Create(reader, text2);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00058C50 File Offset: 0x00056E50
		[NullableContext(2)]
		private bool ReadMetadataPropertiesToken([Nullable(1)] JTokenReader reader, ref Type objectType, ref JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, object existingValue, [NotNullWhen(true)] out object newValue, out string id)
		{
			id = null;
			newValue = null;
			if (reader.TokenType == JsonToken.StartObject)
			{
				JObject jobject = (JObject)reader.CurrentToken;
				JProperty jproperty = jobject.Property("$ref", StringComparison.Ordinal);
				if (jproperty != null)
				{
					JToken value = jproperty.Value;
					if (value.Type != JTokenType.String && value.Type != JTokenType.Null)
					{
						throw JsonSerializationException.Create(value, value.Path, "JSON reference {0} property must have a string or null value.".FormatWith(CultureInfo.InvariantCulture, "$ref"), null);
					}
					string text = (string)jproperty;
					if (text != null)
					{
						JToken jtoken = jproperty.Next ?? jproperty.Previous;
						if (jtoken != null)
						{
							throw JsonSerializationException.Create(jtoken, jtoken.Path, "Additional content found in JSON reference object. A JSON reference object should only have a {0} property.".FormatWith(CultureInfo.InvariantCulture, "$ref"), null);
						}
						newValue = this.Serializer.GetReferenceResolver().ResolveReference(this, text);
						if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
						{
							this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader, reader.Path, "Resolved object reference '{0}' to {1}.".FormatWith(CultureInfo.InvariantCulture, text, newValue.GetType())), null);
						}
						reader.Skip();
						return true;
					}
				}
				JToken jtoken2 = jobject["$type"];
				if (jtoken2 != null)
				{
					string qualifiedTypeName = (string)jtoken2;
					JsonReader jsonReader = jtoken2.CreateReader();
					jsonReader.ReadAndAssert();
					this.ResolveTypeName(jsonReader, ref objectType, ref contract, member, containerContract, containerMember, qualifiedTypeName);
					if (jobject["$value"] != null)
					{
						for (;;)
						{
							reader.ReadAndAssert();
							if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "$value")
							{
								break;
							}
							reader.ReadAndAssert();
							reader.Skip();
						}
						return false;
					}
				}
				JToken jtoken3 = jobject["$id"];
				if (jtoken3 != null)
				{
					id = (string)jtoken3;
				}
				JToken jtoken4 = jobject["$values"];
				if (jtoken4 != null)
				{
					JsonReader jsonReader2 = jtoken4.CreateReader();
					jsonReader2.ReadAndAssert();
					newValue = this.CreateList(jsonReader2, objectType, contract, member, existingValue, id);
					reader.Skip();
					return true;
				}
			}
			reader.ReadAndAssert();
			return false;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00058E80 File Offset: 0x00057080
		[NullableContext(2)]
		private bool ReadMetadataProperties([Nullable(1)] JsonReader reader, ref Type objectType, ref JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, object existingValue, out object newValue, out string id)
		{
			id = null;
			newValue = null;
			if (reader.TokenType == JsonToken.PropertyName)
			{
				string text = reader.Value.ToString();
				if (text.Length > 0 && text[0] == '$')
				{
					string text2;
					for (;;)
					{
						text = reader.Value.ToString();
						bool flag;
						if (string.Equals(text, "$ref", StringComparison.Ordinal))
						{
							reader.ReadAndAssert();
							if (reader.TokenType != JsonToken.String && reader.TokenType != JsonToken.Null)
							{
								break;
							}
							object value = reader.Value;
							text2 = ((value != null) ? value.ToString() : null);
							reader.ReadAndAssert();
							if (text2 != null)
							{
								goto Block_7;
							}
							flag = true;
						}
						else if (string.Equals(text, "$type", StringComparison.Ordinal))
						{
							reader.ReadAndAssert();
							string qualifiedTypeName = reader.Value.ToString();
							this.ResolveTypeName(reader, ref objectType, ref contract, member, containerContract, containerMember, qualifiedTypeName);
							reader.ReadAndAssert();
							flag = true;
						}
						else if (string.Equals(text, "$id", StringComparison.Ordinal))
						{
							reader.ReadAndAssert();
							object value2 = reader.Value;
							id = ((value2 != null) ? value2.ToString() : null);
							reader.ReadAndAssert();
							flag = true;
						}
						else
						{
							if (string.Equals(text, "$values", StringComparison.Ordinal))
							{
								goto Block_14;
							}
							flag = false;
						}
						if (!flag || reader.TokenType != JsonToken.PropertyName)
						{
							return false;
						}
					}
					throw JsonSerializationException.Create(reader, "JSON reference {0} property must have a string or null value.".FormatWith(CultureInfo.InvariantCulture, "$ref"));
					Block_7:
					if (reader.TokenType == JsonToken.PropertyName)
					{
						throw JsonSerializationException.Create(reader, "Additional content found in JSON reference object. A JSON reference object should only have a {0} property.".FormatWith(CultureInfo.InvariantCulture, "$ref"));
					}
					newValue = this.Serializer.GetReferenceResolver().ResolveReference(this, text2);
					if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
					{
						this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Resolved object reference '{0}' to {1}.".FormatWith(CultureInfo.InvariantCulture, text2, newValue.GetType())), null);
					}
					return true;
					Block_14:
					reader.ReadAndAssert();
					object obj = this.CreateList(reader, objectType, contract, member, existingValue, id);
					reader.ReadAndAssert();
					newValue = obj;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000590AC File Offset: 0x000572AC
		[NullableContext(2)]
		private void ResolveTypeName([Nullable(1)] JsonReader reader, ref Type objectType, ref JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, [Nullable(1)] string qualifiedTypeName)
		{
			if ((((member != null) ? member.TypeNameHandling : null) ?? (((containerContract != null) ? containerContract.ItemTypeNameHandling : null) ?? (((containerMember != null) ? containerMember.ItemTypeNameHandling : null) ?? this.Serializer._typeNameHandling))) != TypeNameHandling.None)
			{
				StructMultiKey<string, string> structMultiKey = ReflectionUtils.SplitFullyQualifiedTypeName(qualifiedTypeName);
				Type type;
				try
				{
					type = this.Serializer._serializationBinder.BindToType(structMultiKey.Value1, structMultiKey.Value2);
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, "Error resolving type specified in JSON '{0}'.".FormatWith(CultureInfo.InvariantCulture, qualifiedTypeName), ex);
				}
				if (type == null)
				{
					throw JsonSerializationException.Create(reader, "Type specified in JSON '{0}' was not resolved.".FormatWith(CultureInfo.InvariantCulture, qualifiedTypeName));
				}
				if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
				{
					this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Resolved type '{0}' to {1}.".FormatWith(CultureInfo.InvariantCulture, qualifiedTypeName, type)), null);
				}
				if (objectType != null && objectType != typeof(IDynamicMetaObjectProvider) && !objectType.IsAssignableFrom(type))
				{
					throw JsonSerializationException.Create(reader, "Type specified in JSON '{0}' is not compatible with '{1}'.".FormatWith(CultureInfo.InvariantCulture, type.AssemblyQualifiedName, objectType.AssemblyQualifiedName));
				}
				objectType = type;
				contract = this.GetContract(type);
			}
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00059298 File Offset: 0x00057498
		private JsonArrayContract EnsureArrayContract(JsonReader reader, Type objectType, JsonContract contract)
		{
			if (contract == null)
			{
				throw JsonSerializationException.Create(reader, "Could not resolve type '{0}' to a JsonContract.".FormatWith(CultureInfo.InvariantCulture, objectType));
			}
			JsonArrayContract jsonArrayContract = contract as JsonArrayContract;
			if (jsonArrayContract == null)
			{
				string text = "Cannot deserialize the current JSON array (e.g. [1,2,3]) into type '{0}' because the type requires a {1} to deserialize correctly." + Environment.NewLine + "To fix this error either change the JSON to a {1} or change the deserialized type to an array or a type that implements a collection interface (e.g. ICollection, IList) like List<T> that can be deserialized from a JSON array. JsonArrayAttribute can also be added to the type to force it to deserialize from a JSON array." + Environment.NewLine;
				text = text.FormatWith(CultureInfo.InvariantCulture, objectType, this.GetExpectedDescription(contract));
				throw JsonSerializationException.Create(reader, text);
			}
			return jsonArrayContract;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00059308 File Offset: 0x00057508
		[NullableContext(2)]
		private object CreateList([Nullable(1)] JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, object existingValue, string id)
		{
			if (this.HasNoDefinedType(contract))
			{
				return this.CreateJToken(reader, contract);
			}
			JsonArrayContract jsonArrayContract = this.EnsureArrayContract(reader, objectType, contract);
			object result;
			if (existingValue == null)
			{
				bool flag;
				IList list = this.CreateNewList(reader, jsonArrayContract, out flag);
				if (flag)
				{
					if (id != null)
					{
						throw JsonSerializationException.Create(reader, "Cannot preserve reference to array or readonly list, or list created from a non-default constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
					if (contract.OnSerializingCallbacks.Count > 0)
					{
						throw JsonSerializationException.Create(reader, "Cannot call OnSerializing on an array or readonly list, or list created from a non-default constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
					if (contract.OnErrorCallbacks.Count > 0)
					{
						throw JsonSerializationException.Create(reader, "Cannot call OnError on an array or readonly list, or list created from a non-default constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
					if (!jsonArrayContract.HasParameterizedCreatorInternal && !jsonArrayContract.IsArray)
					{
						throw JsonSerializationException.Create(reader, "Cannot deserialize readonly or fixed size list: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
				}
				if (!jsonArrayContract.IsMultidimensionalArray)
				{
					this.PopulateList(list, reader, jsonArrayContract, member, id);
				}
				else
				{
					this.PopulateMultidimensionalArray(list, reader, jsonArrayContract, member, id);
				}
				if (flag)
				{
					if (jsonArrayContract.IsMultidimensionalArray)
					{
						list = CollectionUtils.ToMultidimensionalArray(list, jsonArrayContract.CollectionItemType, contract.CreatedType.GetArrayRank());
					}
					else
					{
						if (!jsonArrayContract.IsArray)
						{
							return (jsonArrayContract.OverrideCreator ?? jsonArrayContract.ParameterizedCreator)(new object[]
							{
								list
							});
						}
						Array array = Array.CreateInstance(jsonArrayContract.CollectionItemType, list.Count);
						list.CopyTo(array, 0);
						list = array;
					}
				}
				else
				{
					IWrappedCollection wrappedCollection = list as IWrappedCollection;
					if (wrappedCollection != null)
					{
						return wrappedCollection.UnderlyingCollection;
					}
				}
				result = list;
			}
			else
			{
				if (!jsonArrayContract.CanDeserialize)
				{
					throw JsonSerializationException.Create(reader, "Cannot populate list type {0}.".FormatWith(CultureInfo.InvariantCulture, contract.CreatedType));
				}
				IList list3;
				if (!jsonArrayContract.ShouldCreateWrapper)
				{
					IList list2 = existingValue as IList;
					if (list2 != null)
					{
						list3 = list2;
						goto IL_204;
					}
				}
				IList list4 = jsonArrayContract.CreateWrapper(existingValue);
				list3 = list4;
				IL_204:
				result = this.PopulateList(list3, reader, jsonArrayContract, member, id);
			}
			return result;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0005952C File Offset: 0x0005772C
		[NullableContext(2)]
		private bool HasNoDefinedType(JsonContract contract)
		{
			return contract == null || contract.UnderlyingType == typeof(object) || contract.ContractType == JsonContractType.Linq || contract.UnderlyingType == typeof(IDynamicMetaObjectProvider);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00059580 File Offset: 0x00057780
		[NullableContext(2)]
		private object EnsureType([Nullable(1)] JsonReader reader, object value, [Nullable(1)] CultureInfo culture, JsonContract contract, Type targetType)
		{
			if (targetType == null)
			{
				return value;
			}
			if (ReflectionUtils.GetObjectType(value) != targetType)
			{
				if (value == null && contract.IsNullable)
				{
					return null;
				}
				try
				{
					if (!contract.IsConvertable)
					{
						return ConvertUtils.ConvertOrCast(value, culture, contract.NonNullableUnderlyingType);
					}
					JsonPrimitiveContract jsonPrimitiveContract = (JsonPrimitiveContract)contract;
					if (contract.IsEnum)
					{
						string text = value as string;
						if (text != null)
						{
							return EnumUtils.ParseEnum(contract.NonNullableUnderlyingType, null, text, false);
						}
						if (ConvertUtils.IsInteger(jsonPrimitiveContract.TypeCode))
						{
							return Enum.ToObject(contract.NonNullableUnderlyingType, value);
						}
					}
					else if (contract.NonNullableUnderlyingType == typeof(DateTime))
					{
						string text2 = value as string;
						DateTime value2;
						if (text2 != null && DateTimeUtils.TryParseDateTime(text2, reader.DateTimeZoneHandling, reader.DateFormatString, reader.Culture, out value2))
						{
							return DateTimeUtils.EnsureDateTime(value2, reader.DateTimeZoneHandling);
						}
					}
					if (value is System.Numerics.BigInteger)
					{
						System.Numerics.BigInteger i = (System.Numerics.BigInteger)value;
						return ConvertUtils.FromBigInteger(i, contract.NonNullableUnderlyingType);
					}
					return Convert.ChangeType(value, contract.NonNullableUnderlyingType, culture);
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, "Error converting value {0} to type '{1}'.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(value), targetType), ex);
				}
				return value;
			}
			return value;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0005971C File Offset: 0x0005791C
		private bool SetPropertyValue(JsonProperty property, [Nullable(2)] JsonConverter propertyConverter, [Nullable(2)] JsonContainerContract containerContract, [Nullable(2)] JsonProperty containerProperty, JsonReader reader, object target)
		{
			bool flag;
			object value;
			JsonContract contract;
			bool flag2;
			bool result;
			if (this.CalculatePropertyDetails(property, ref propertyConverter, containerContract, containerProperty, reader, target, out flag, out value, out contract, out flag2, out result))
			{
				return result;
			}
			object obj;
			if (propertyConverter != null && propertyConverter.CanRead)
			{
				if (!flag2 && property.Readable)
				{
					value = property.ValueProvider.GetValue(target);
				}
				obj = this.DeserializeConvertable(propertyConverter, reader, property.PropertyType, value);
			}
			else
			{
				obj = this.CreateValueInternal(reader, property.PropertyType, contract, property, containerContract, containerProperty, flag ? value : null);
			}
			if ((!flag || obj != value) && this.ShouldSetPropertyValue(property, containerContract as JsonObjectContract, obj))
			{
				property.ValueProvider.SetValue(target, obj);
				if (property.SetIsSpecified != null)
				{
					if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
					{
						this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "IsSpecified for property '{0}' on {1} set to true.".FormatWith(CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType)), null);
					}
					property.SetIsSpecified(target, true);
				}
				return true;
			}
			return flag;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0005986C File Offset: 0x00057A6C
		[NullableContext(2)]
		private bool CalculatePropertyDetails([Nullable(1)] JsonProperty property, ref JsonConverter propertyConverter, JsonContainerContract containerContract, JsonProperty containerProperty, [Nullable(1)] JsonReader reader, [Nullable(1)] object target, out bool useExistingValue, out object currentValue, out JsonContract propertyContract, out bool gottenCurrentValue, out bool ignoredValue)
		{
			currentValue = null;
			useExistingValue = false;
			propertyContract = null;
			gottenCurrentValue = false;
			ignoredValue = false;
			if (property.Ignored)
			{
				return true;
			}
			JsonToken tokenType = reader.TokenType;
			if (property.PropertyContract == null)
			{
				property.PropertyContract = this.GetContractSafe(property.PropertyType);
			}
			if (property.ObjectCreationHandling.GetValueOrDefault(this.Serializer._objectCreationHandling) != ObjectCreationHandling.Replace && (tokenType == JsonToken.StartArray || tokenType == JsonToken.StartObject || propertyConverter != null) && property.Readable)
			{
				currentValue = property.ValueProvider.GetValue(target);
				gottenCurrentValue = true;
				if (currentValue != null)
				{
					propertyContract = this.GetContract(currentValue.GetType());
					useExistingValue = (!propertyContract.IsReadOnlyOrFixedSize && !propertyContract.UnderlyingType.IsValueType());
				}
			}
			if (!property.Writable && !useExistingValue)
			{
				if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
				{
					this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Unable to deserialize value to non-writable property '{0}' on {1}.".FormatWith(CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType)), null);
				}
				return true;
			}
			if (tokenType == JsonToken.Null && base.ResolvedNullValueHandling(containerContract as JsonObjectContract, property) == NullValueHandling.Ignore)
			{
				ignoredValue = true;
				return true;
			}
			if (this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Ignore) && !this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Populate) && JsonTokenUtils.IsPrimitiveToken(tokenType) && MiscellaneousUtils.ValueEquals(reader.Value, property.GetResolvedDefaultValue()))
			{
				ignoredValue = true;
				return true;
			}
			if (currentValue == null)
			{
				propertyContract = property.PropertyContract;
			}
			else
			{
				propertyContract = this.GetContract(currentValue.GetType());
				if (propertyContract != property.PropertyContract)
				{
					propertyConverter = this.GetConverter(propertyContract, property.Converter, containerContract, containerProperty);
				}
			}
			return false;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00059AA0 File Offset: 0x00057CA0
		private void AddReference(JsonReader reader, string id, object value)
		{
			try
			{
				if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
				{
					this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Read object reference Id '{0}' for {1}.".FormatWith(CultureInfo.InvariantCulture, id, value.GetType())), null);
				}
				this.Serializer.GetReferenceResolver().AddReference(this, id, value);
			}
			catch (Exception ex)
			{
				throw JsonSerializationException.Create(reader, "Error reading object reference '{0}'.".FormatWith(CultureInfo.InvariantCulture, id), ex);
			}
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00059B40 File Offset: 0x00057D40
		private bool HasFlag(DefaultValueHandling value, DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00059B48 File Offset: 0x00057D48
		[NullableContext(2)]
		private bool ShouldSetPropertyValue([Nullable(1)] JsonProperty property, JsonObjectContract contract, object value)
		{
			return (value != null || base.ResolvedNullValueHandling(contract, property) != NullValueHandling.Ignore) && (!this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Ignore) || this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Populate) || !MiscellaneousUtils.ValueEquals(value, property.GetResolvedDefaultValue())) && property.Writable;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00059BDC File Offset: 0x00057DDC
		private IList CreateNewList(JsonReader reader, JsonArrayContract contract, out bool createdFromNonDefaultCreator)
		{
			if (!contract.CanDeserialize)
			{
				throw JsonSerializationException.Create(reader, "Cannot create and populate list type {0}.".FormatWith(CultureInfo.InvariantCulture, contract.CreatedType));
			}
			if (contract.OverrideCreator != null)
			{
				if (contract.HasParameterizedCreator)
				{
					createdFromNonDefaultCreator = true;
					return contract.CreateTemporaryCollection();
				}
				object obj = contract.OverrideCreator(new object[0]);
				if (contract.ShouldCreateWrapper)
				{
					obj = contract.CreateWrapper(obj);
				}
				createdFromNonDefaultCreator = false;
				return (IList)obj;
			}
			else
			{
				if (contract.IsReadOnlyOrFixedSize)
				{
					createdFromNonDefaultCreator = true;
					IList list = contract.CreateTemporaryCollection();
					if (contract.ShouldCreateWrapper)
					{
						list = contract.CreateWrapper(list);
					}
					return list;
				}
				if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || this.Serializer._constructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor))
				{
					object obj2 = contract.DefaultCreator();
					if (contract.ShouldCreateWrapper)
					{
						obj2 = contract.CreateWrapper(obj2);
					}
					createdFromNonDefaultCreator = false;
					return (IList)obj2;
				}
				if (contract.HasParameterizedCreatorInternal)
				{
					createdFromNonDefaultCreator = true;
					return contract.CreateTemporaryCollection();
				}
				if (!contract.IsInstantiable)
				{
					throw JsonSerializationException.Create(reader, "Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantiated.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
				}
				throw JsonSerializationException.Create(reader, "Unable to find a constructor to use for type {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00059D34 File Offset: 0x00057F34
		private IDictionary CreateNewDictionary(JsonReader reader, JsonDictionaryContract contract, out bool createdFromNonDefaultCreator)
		{
			if (contract.OverrideCreator != null)
			{
				if (contract.HasParameterizedCreator)
				{
					createdFromNonDefaultCreator = true;
					return contract.CreateTemporaryDictionary();
				}
				createdFromNonDefaultCreator = false;
				return (IDictionary)contract.OverrideCreator(new object[0]);
			}
			else
			{
				if (contract.IsReadOnlyOrFixedSize)
				{
					createdFromNonDefaultCreator = true;
					return contract.CreateTemporaryDictionary();
				}
				if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || this.Serializer._constructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor))
				{
					object obj = contract.DefaultCreator();
					if (contract.ShouldCreateWrapper)
					{
						obj = contract.CreateWrapper(obj);
					}
					createdFromNonDefaultCreator = false;
					return (IDictionary)obj;
				}
				if (contract.HasParameterizedCreatorInternal)
				{
					createdFromNonDefaultCreator = true;
					return contract.CreateTemporaryDictionary();
				}
				if (!contract.IsInstantiable)
				{
					throw JsonSerializationException.Create(reader, "Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantiated.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
				}
				throw JsonSerializationException.Create(reader, "Unable to find a default constructor to use for type {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00059E3C File Offset: 0x0005803C
		private void OnDeserializing(JsonReader reader, JsonContract contract, object value)
		{
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Started deserializing {0}".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnDeserializing(value, this.Serializer._context);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00059EB0 File Offset: 0x000580B0
		private void OnDeserialized(JsonReader reader, JsonContract contract, object value)
		{
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Finished deserializing {0}".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnDeserialized(value, this.Serializer._context);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00059F24 File Offset: 0x00058124
		private object PopulateDictionary(IDictionary dictionary, JsonReader reader, JsonDictionaryContract contract, [Nullable(2)] JsonProperty containerProperty, [Nullable(2)] string id)
		{
			IWrappedDictionary wrappedDictionary = dictionary as IWrappedDictionary;
			object obj = (wrappedDictionary != null) ? wrappedDictionary.UnderlyingDictionary : dictionary;
			if (id != null)
			{
				this.AddReference(reader, id, obj);
			}
			this.OnDeserializing(reader, contract, obj);
			int depth = reader.Depth;
			if (contract.KeyContract == null)
			{
				contract.KeyContract = this.GetContractSafe(contract.DictionaryKeyType);
			}
			if (contract.ItemContract == null)
			{
				contract.ItemContract = this.GetContractSafe(contract.DictionaryValueType);
			}
			JsonConverter jsonConverter = contract.ItemConverter ?? this.GetConverter(contract.ItemContract, null, contract, containerProperty);
			JsonPrimitiveContract jsonPrimitiveContract = contract.KeyContract as JsonPrimitiveContract;
			PrimitiveTypeCode primitiveTypeCode = (jsonPrimitiveContract != null) ? jsonPrimitiveContract.TypeCode : PrimitiveTypeCode.Empty;
			bool flag = false;
			for (;;)
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType != JsonToken.EndObject)
						{
							break;
						}
						goto IL_2A0;
					}
				}
				else
				{
					object obj2 = reader.Value;
					if (!this.CheckPropertyName(reader, obj2.ToString()))
					{
						try
						{
							try
							{
								if (primitiveTypeCode - PrimitiveTypeCode.DateTime > 1)
								{
									if (primitiveTypeCode - PrimitiveTypeCode.DateTimeOffset > 1)
									{
										obj2 = this.EnsureType(reader, obj2, CultureInfo.InvariantCulture, contract.KeyContract, contract.DictionaryKeyType);
									}
									else
									{
										DateTimeOffset dateTimeOffset;
										obj2 = (DateTimeUtils.TryParseDateTimeOffset(obj2.ToString(), reader.DateFormatString, reader.Culture, out dateTimeOffset) ? dateTimeOffset : this.EnsureType(reader, obj2, CultureInfo.InvariantCulture, contract.KeyContract, contract.DictionaryKeyType));
									}
								}
								else
								{
									DateTime dateTime;
									obj2 = (DateTimeUtils.TryParseDateTime(obj2.ToString(), reader.DateTimeZoneHandling, reader.DateFormatString, reader.Culture, out dateTime) ? dateTime : this.EnsureType(reader, obj2, CultureInfo.InvariantCulture, contract.KeyContract, contract.DictionaryKeyType));
								}
							}
							catch (Exception ex)
							{
								throw JsonSerializationException.Create(reader, "Could not convert string '{0}' to dictionary key type '{1}'. Create a TypeConverter to convert from the string to the key type object.".FormatWith(CultureInfo.InvariantCulture, reader.Value, contract.DictionaryKeyType), ex);
							}
							if (!reader.ReadForType(contract.ItemContract, jsonConverter != null))
							{
								throw JsonSerializationException.Create(reader, "Unexpected end when deserializing object.");
							}
							object value;
							if (jsonConverter != null && jsonConverter.CanRead)
							{
								value = this.DeserializeConvertable(jsonConverter, reader, contract.DictionaryValueType, null);
							}
							else
							{
								value = this.CreateValueInternal(reader, contract.DictionaryValueType, contract.ItemContract, null, contract, containerProperty, null);
							}
							dictionary[obj2] = value;
							goto IL_2CE;
						}
						catch (Exception ex2)
						{
							if (base.IsErrorHandled(obj, contract, obj2, reader as IJsonLineInfo, reader.Path, ex2))
							{
								this.HandleError(reader, true, depth);
								goto IL_2CE;
							}
							throw;
						}
						goto IL_2A0;
					}
				}
				IL_2CE:
				if (flag || !reader.Read())
				{
					goto IL_2E0;
				}
				continue;
				IL_2A0:
				flag = true;
				goto IL_2CE;
			}
			throw JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType.ToString());
			IL_2E0:
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, obj, "Unexpected end when deserializing object.");
			}
			this.OnDeserialized(reader, contract, obj);
			return obj;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0005A264 File Offset: 0x00058464
		private object PopulateMultidimensionalArray(IList list, JsonReader reader, JsonArrayContract contract, [Nullable(2)] JsonProperty containerProperty, [Nullable(2)] string id)
		{
			int arrayRank = contract.UnderlyingType.GetArrayRank();
			if (id != null)
			{
				this.AddReference(reader, id, list);
			}
			this.OnDeserializing(reader, contract, list);
			JsonContract contractSafe = this.GetContractSafe(contract.CollectionItemType);
			JsonConverter converter = this.GetConverter(contractSafe, null, contract, containerProperty);
			int? num = null;
			Stack<IList> stack = new Stack<IList>();
			stack.Push(list);
			IList list2 = list;
			bool flag = false;
			for (;;)
			{
				int depth = reader.Depth;
				JsonToken tokenType;
				if (stack.Count == arrayRank)
				{
					try
					{
						if (reader.ReadForType(contractSafe, converter != null))
						{
							tokenType = reader.TokenType;
							if (tokenType != JsonToken.Comment)
							{
								if (tokenType == JsonToken.EndArray)
								{
									stack.Pop();
									list2 = stack.Peek();
									num = null;
								}
								else
								{
									object value;
									if (converter != null && converter.CanRead)
									{
										value = this.DeserializeConvertable(converter, reader, contract.CollectionItemType, null);
									}
									else
									{
										value = this.CreateValueInternal(reader, contract.CollectionItemType, contractSafe, null, contract, containerProperty, null);
									}
									list2.Add(value);
								}
							}
							goto IL_249;
						}
						goto IL_250;
					}
					catch (Exception ex)
					{
						JsonPosition position = reader.GetPosition(depth);
						if (base.IsErrorHandled(list, contract, position.Position, reader as IJsonLineInfo, reader.Path, ex))
						{
							this.HandleError(reader, true, depth + 1);
							if (num != null)
							{
								int? num2 = num;
								int position2 = position.Position;
								if (num2.GetValueOrDefault() == position2 & num2 != null)
								{
									throw JsonSerializationException.Create(reader, "Infinite loop detected from error handling.", ex);
								}
							}
							num = new int?(position.Position);
							goto IL_249;
						}
						throw;
					}
					goto IL_1A4;
				}
				goto IL_1A4;
				IL_249:
				if (flag)
				{
					goto IL_250;
				}
				continue;
				IL_1A4:
				if (!reader.Read())
				{
					goto IL_250;
				}
				tokenType = reader.TokenType;
				if (tokenType == JsonToken.StartArray)
				{
					IList list3 = new List<object>();
					list2.Add(list3);
					stack.Push(list3);
					list2 = list3;
					goto IL_249;
				}
				if (tokenType == JsonToken.Comment)
				{
					goto IL_249;
				}
				if (tokenType != JsonToken.EndArray)
				{
					break;
				}
				stack.Pop();
				if (stack.Count > 0)
				{
					list2 = stack.Peek();
					goto IL_249;
				}
				flag = true;
				goto IL_249;
			}
			throw JsonSerializationException.Create(reader, "Unexpected token when deserializing multidimensional array: " + reader.TokenType.ToString());
			IL_250:
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, list, "Unexpected end when deserializing array.");
			}
			this.OnDeserialized(reader, contract, list);
			return list;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0005A4F0 File Offset: 0x000586F0
		private void ThrowUnexpectedEndException(JsonReader reader, JsonContract contract, [Nullable(2)] object currentObject, string message)
		{
			try
			{
				throw JsonSerializationException.Create(reader, message);
			}
			catch (Exception ex)
			{
				if (!base.IsErrorHandled(currentObject, contract, null, reader as IJsonLineInfo, reader.Path, ex))
				{
					throw;
				}
				this.HandleError(reader, false, 0);
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0005A548 File Offset: 0x00058748
		private object PopulateList(IList list, JsonReader reader, JsonArrayContract contract, [Nullable(2)] JsonProperty containerProperty, [Nullable(2)] string id)
		{
			IWrappedCollection wrappedCollection = list as IWrappedCollection;
			object obj = (wrappedCollection != null) ? wrappedCollection.UnderlyingCollection : list;
			if (id != null)
			{
				this.AddReference(reader, id, obj);
			}
			if (list.IsFixedSize)
			{
				reader.Skip();
				return obj;
			}
			this.OnDeserializing(reader, contract, obj);
			int depth = reader.Depth;
			if (contract.ItemContract == null)
			{
				contract.ItemContract = this.GetContractSafe(contract.CollectionItemType);
			}
			JsonConverter converter = this.GetConverter(contract.ItemContract, null, contract, containerProperty);
			int? num = null;
			bool flag = false;
			do
			{
				try
				{
					if (!reader.ReadForType(contract.ItemContract, converter != null))
					{
						break;
					}
					JsonToken tokenType = reader.TokenType;
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType == JsonToken.EndArray)
						{
							flag = true;
						}
						else
						{
							object value;
							if (converter != null && converter.CanRead)
							{
								value = this.DeserializeConvertable(converter, reader, contract.CollectionItemType, null);
							}
							else
							{
								value = this.CreateValueInternal(reader, contract.CollectionItemType, contract.ItemContract, null, contract, containerProperty, null);
							}
							list.Add(value);
						}
					}
				}
				catch (Exception ex)
				{
					JsonPosition position = reader.GetPosition(depth);
					if (!base.IsErrorHandled(obj, contract, position.Position, reader as IJsonLineInfo, reader.Path, ex))
					{
						throw;
					}
					this.HandleError(reader, true, depth + 1);
					if (num != null)
					{
						int? num2 = num;
						int position2 = position.Position;
						if (num2.GetValueOrDefault() == position2 & num2 != null)
						{
							throw JsonSerializationException.Create(reader, "Infinite loop detected from error handling.", ex);
						}
					}
					num = new int?(position.Position);
				}
			}
			while (!flag);
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, obj, "Unexpected end when deserializing array.");
			}
			this.OnDeserialized(reader, contract, obj);
			return obj;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0005A738 File Offset: 0x00058938
		private object CreateISerializable(JsonReader reader, JsonISerializableContract contract, [Nullable(2)] JsonProperty member, [Nullable(2)] string id)
		{
			Type underlyingType = contract.UnderlyingType;
			if (!JsonTypeReflector.FullyTrusted)
			{
				string text = "Type '{0}' implements ISerializable but cannot be deserialized using the ISerializable interface because the current application is not fully trusted and ISerializable can expose secure data." + Environment.NewLine + "To fix this error either change the environment to be fully trusted, change the application to not deserialize the type, add JsonObjectAttribute to the type or change the JsonSerializer setting ContractResolver to use a new DefaultContractResolver with IgnoreSerializableInterface set to true." + Environment.NewLine;
				text = text.FormatWith(CultureInfo.InvariantCulture, underlyingType);
				throw JsonSerializationException.Create(reader, text);
			}
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Deserializing {0} using ISerializable constructor.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			SerializationInfo serializationInfo = new SerializationInfo(contract.UnderlyingType, new JsonFormatterConverter(this, contract, member));
			bool flag = false;
			string text2;
			for (;;)
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType != JsonToken.EndObject)
						{
							break;
						}
						flag = true;
					}
				}
				else
				{
					text2 = reader.Value.ToString();
					if (!reader.Read())
					{
						goto Block_7;
					}
					serializationInfo.AddValue(text2, JToken.ReadFrom(reader));
				}
				if (flag || !reader.Read())
				{
					goto IL_14C;
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType.ToString());
			Block_7:
			throw JsonSerializationException.Create(reader, "Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, text2));
			IL_14C:
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, serializationInfo, "Unexpected end when deserializing object.");
			}
			if (!contract.IsInstantiable)
			{
				throw JsonSerializationException.Create(reader, "Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantiated.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
			if (contract.ISerializableCreator == null)
			{
				throw JsonSerializationException.Create(reader, "ISerializable type '{0}' does not have a valid constructor. To correctly implement ISerializable a constructor that takes SerializationInfo and StreamingContext parameters should be present.".FormatWith(CultureInfo.InvariantCulture, underlyingType));
			}
			object obj = contract.ISerializableCreator(new object[]
			{
				serializationInfo,
				this.Serializer._context
			});
			if (id != null)
			{
				this.AddReference(reader, id, obj);
			}
			this.OnDeserializing(reader, contract, obj);
			this.OnDeserialized(reader, contract, obj);
			return obj;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0005A944 File Offset: 0x00058B44
		[return: Nullable(2)]
		internal object CreateISerializableItem(JToken token, Type type, JsonISerializableContract contract, [Nullable(2)] JsonProperty member)
		{
			JsonContract contractSafe = this.GetContractSafe(type);
			JsonConverter converter = this.GetConverter(contractSafe, null, contract, member);
			JsonReader jsonReader = token.CreateReader();
			jsonReader.ReadAndAssert();
			object result;
			if (converter != null && converter.CanRead)
			{
				result = this.DeserializeConvertable(converter, jsonReader, type, null);
			}
			else
			{
				result = this.CreateValueInternal(jsonReader, type, contractSafe, null, contract, member, null);
			}
			return result;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0005A9A8 File Offset: 0x00058BA8
		private object CreateDynamic(JsonReader reader, JsonDynamicContract contract, [Nullable(2)] JsonProperty member, [Nullable(2)] string id)
		{
			if (!contract.IsInstantiable)
			{
				throw JsonSerializationException.Create(reader, "Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantiated.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
			if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || this.Serializer._constructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor))
			{
				IDynamicMetaObjectProvider dynamicMetaObjectProvider = (IDynamicMetaObjectProvider)contract.DefaultCreator();
				if (id != null)
				{
					this.AddReference(reader, id, dynamicMetaObjectProvider);
				}
				this.OnDeserializing(reader, contract, dynamicMetaObjectProvider);
				int depth = reader.Depth;
				bool flag = false;
				for (;;)
				{
					JsonToken tokenType = reader.TokenType;
					if (tokenType == JsonToken.PropertyName)
					{
						string text = reader.Value.ToString();
						try
						{
							if (!reader.Read())
							{
								throw JsonSerializationException.Create(reader, "Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, text));
							}
							JsonProperty closestMatchProperty = contract.Properties.GetClosestMatchProperty(text);
							if (closestMatchProperty != null && closestMatchProperty.Writable && !closestMatchProperty.Ignored)
							{
								if (closestMatchProperty.PropertyContract == null)
								{
									closestMatchProperty.PropertyContract = this.GetContractSafe(closestMatchProperty.PropertyType);
								}
								JsonConverter converter = this.GetConverter(closestMatchProperty.PropertyContract, closestMatchProperty.Converter, null, null);
								if (!this.SetPropertyValue(closestMatchProperty, converter, null, member, reader, dynamicMetaObjectProvider))
								{
									reader.Skip();
								}
							}
							else
							{
								Type type = JsonTokenUtils.IsPrimitiveToken(reader.TokenType) ? reader.ValueType : typeof(IDynamicMetaObjectProvider);
								JsonContract contractSafe = this.GetContractSafe(type);
								JsonConverter converter2 = this.GetConverter(contractSafe, null, null, member);
								object value;
								if (converter2 != null && converter2.CanRead)
								{
									value = this.DeserializeConvertable(converter2, reader, type, null);
								}
								else
								{
									value = this.CreateValueInternal(reader, type, contractSafe, null, null, member, null);
								}
								contract.TrySetMember(dynamicMetaObjectProvider, text, value);
							}
							goto IL_259;
						}
						catch (Exception ex)
						{
							if (base.IsErrorHandled(dynamicMetaObjectProvider, contract, text, reader as IJsonLineInfo, reader.Path, ex))
							{
								this.HandleError(reader, true, depth);
								goto IL_259;
							}
							throw;
						}
						goto IL_22C;
					}
					if (tokenType != JsonToken.EndObject)
					{
						break;
					}
					goto IL_22C;
					IL_259:
					if (flag || !reader.Read())
					{
						goto IL_26A;
					}
					continue;
					IL_22C:
					flag = true;
					goto IL_259;
				}
				throw JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType.ToString());
				IL_26A:
				if (!flag)
				{
					this.ThrowUnexpectedEndException(reader, contract, dynamicMetaObjectProvider, "Unexpected end when deserializing object.");
				}
				this.OnDeserialized(reader, contract, dynamicMetaObjectProvider);
				return dynamicMetaObjectProvider;
			}
			throw JsonSerializationException.Create(reader, "Unable to find a default constructor to use for type {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0005AC5C File Offset: 0x00058E5C
		private object CreateObjectUsingCreatorWithParameters(JsonReader reader, JsonObjectContract contract, [Nullable(2)] JsonProperty containerProperty, ObjectConstructor<object> creator, [Nullable(2)] string id)
		{
			ValidationUtils.ArgumentNotNull(creator, "creator");
			bool flag = contract.HasRequiredOrDefaultValueProperties || this.HasFlag(this.Serializer._defaultValueHandling, DefaultValueHandling.Populate);
			Type underlyingType = contract.UnderlyingType;
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				string arg = string.Join(", ", from p in contract.CreatorParameters
				select p.PropertyName);
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Deserializing {0} using creator with parameters: {1}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType, arg)), null);
			}
			List<JsonSerializerInternalReader.CreatorPropertyContext> list = this.ResolvePropertyAndCreatorValues(contract, containerProperty, reader, underlyingType);
			if (flag)
			{
				using (IEnumerator<JsonProperty> enumerator = contract.Properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonProperty property = enumerator.Current;
						if (!property.Ignored && list.All((JsonSerializerInternalReader.CreatorPropertyContext p) => p.Property != property))
						{
							list.Add(new JsonSerializerInternalReader.CreatorPropertyContext(property.PropertyName)
							{
								Property = property,
								Presence = new JsonSerializerInternalReader.PropertyPresence?(JsonSerializerInternalReader.PropertyPresence.None)
							});
						}
					}
				}
			}
			object[] array = new object[contract.CreatorParameters.Count];
			foreach (JsonSerializerInternalReader.CreatorPropertyContext creatorPropertyContext in list)
			{
				if (flag && creatorPropertyContext.Property != null && creatorPropertyContext.Presence == null)
				{
					object value = creatorPropertyContext.Value;
					JsonSerializerInternalReader.PropertyPresence value2;
					if (value == null)
					{
						value2 = JsonSerializerInternalReader.PropertyPresence.Null;
					}
					else
					{
						string text = value as string;
						if (text != null)
						{
							value2 = (JsonSerializerInternalReader.CoerceEmptyStringToNull(creatorPropertyContext.Property.PropertyType, creatorPropertyContext.Property.PropertyContract, text) ? JsonSerializerInternalReader.PropertyPresence.Null : JsonSerializerInternalReader.PropertyPresence.Value);
						}
						else
						{
							value2 = JsonSerializerInternalReader.PropertyPresence.Value;
						}
					}
					creatorPropertyContext.Presence = new JsonSerializerInternalReader.PropertyPresence?(value2);
				}
				JsonProperty jsonProperty = creatorPropertyContext.ConstructorProperty;
				if (jsonProperty == null && creatorPropertyContext.Property != null)
				{
					jsonProperty = contract.CreatorParameters.ForgivingCaseSensitiveFind((JsonProperty p) => p.PropertyName, creatorPropertyContext.Property.UnderlyingName);
				}
				if (jsonProperty != null && !jsonProperty.Ignored)
				{
					if (flag)
					{
						JsonSerializerInternalReader.PropertyPresence? presence = creatorPropertyContext.Presence;
						JsonSerializerInternalReader.PropertyPresence propertyPresence = JsonSerializerInternalReader.PropertyPresence.None;
						if (!(presence.GetValueOrDefault() == propertyPresence & presence != null))
						{
							presence = creatorPropertyContext.Presence;
							propertyPresence = JsonSerializerInternalReader.PropertyPresence.Null;
							if (!(presence.GetValueOrDefault() == propertyPresence & presence != null))
							{
								goto IL_34A;
							}
						}
						if (jsonProperty.PropertyContract == null)
						{
							jsonProperty.PropertyContract = this.GetContractSafe(jsonProperty.PropertyType);
						}
						if (this.HasFlag(jsonProperty.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Populate))
						{
							creatorPropertyContext.Value = this.EnsureType(reader, jsonProperty.GetResolvedDefaultValue(), CultureInfo.InvariantCulture, jsonProperty.PropertyContract, jsonProperty.PropertyType);
						}
					}
					IL_34A:
					int num = contract.CreatorParameters.IndexOf(jsonProperty);
					array[num] = creatorPropertyContext.Value;
					creatorPropertyContext.Used = true;
				}
			}
			object obj = creator(array);
			if (id != null)
			{
				this.AddReference(reader, id, obj);
			}
			this.OnDeserializing(reader, contract, obj);
			foreach (JsonSerializerInternalReader.CreatorPropertyContext creatorPropertyContext2 in list)
			{
				if (!creatorPropertyContext2.Used && creatorPropertyContext2.Property != null && !creatorPropertyContext2.Property.Ignored)
				{
					JsonSerializerInternalReader.PropertyPresence? presence = creatorPropertyContext2.Presence;
					JsonSerializerInternalReader.PropertyPresence propertyPresence = JsonSerializerInternalReader.PropertyPresence.None;
					if (!(presence.GetValueOrDefault() == propertyPresence & presence != null))
					{
						JsonProperty property2 = creatorPropertyContext2.Property;
						object value3 = creatorPropertyContext2.Value;
						if (this.ShouldSetPropertyValue(property2, contract, value3))
						{
							property2.ValueProvider.SetValue(obj, value3);
							creatorPropertyContext2.Used = true;
						}
						else if (!property2.Writable && value3 != null)
						{
							JsonContract jsonContract = this.Serializer._contractResolver.ResolveContract(property2.PropertyType);
							if (jsonContract.ContractType != JsonContractType.Array)
							{
								goto IL_577;
							}
							JsonArrayContract jsonArrayContract = (JsonArrayContract)jsonContract;
							if (jsonArrayContract.CanDeserialize && !jsonArrayContract.IsReadOnlyOrFixedSize)
							{
								object value4 = property2.ValueProvider.GetValue(obj);
								if (value4 != null)
								{
									jsonArrayContract = (JsonArrayContract)this.GetContract(value4.GetType());
									IList list2;
									if (!jsonArrayContract.ShouldCreateWrapper)
									{
										list2 = (IList)value4;
									}
									else
									{
										IList list3 = jsonArrayContract.CreateWrapper(value4);
										list2 = list3;
									}
									IList list4 = list2;
									if (!list4.IsFixedSize)
									{
										IEnumerable enumerable;
										if (!jsonArrayContract.ShouldCreateWrapper)
										{
											enumerable = (IList)value3;
										}
										else
										{
											IList list3 = jsonArrayContract.CreateWrapper(value3);
											enumerable = list3;
										}
										using (IEnumerator enumerator3 = enumerable.GetEnumerator())
										{
											while (enumerator3.MoveNext())
											{
												object value5 = enumerator3.Current;
												list4.Add(value5);
											}
											goto IL_64D;
										}
										goto IL_577;
									}
								}
							}
							IL_64D:
							creatorPropertyContext2.Used = true;
							continue;
							IL_577:
							if (jsonContract.ContractType != JsonContractType.Dictionary)
							{
								goto IL_64D;
							}
							JsonDictionaryContract jsonDictionaryContract = (JsonDictionaryContract)jsonContract;
							if (jsonDictionaryContract.IsReadOnlyOrFixedSize)
							{
								goto IL_64D;
							}
							object value6 = property2.ValueProvider.GetValue(obj);
							if (value6 != null)
							{
								IDictionary dictionary;
								if (!jsonDictionaryContract.ShouldCreateWrapper)
								{
									dictionary = (IDictionary)value6;
								}
								else
								{
									IDictionary dictionary2 = jsonDictionaryContract.CreateWrapper(value6);
									dictionary = dictionary2;
								}
								IDictionary dictionary3 = dictionary;
								IDictionary dictionary4;
								if (!jsonDictionaryContract.ShouldCreateWrapper)
								{
									dictionary4 = (IDictionary)value3;
								}
								else
								{
									IDictionary dictionary2 = jsonDictionaryContract.CreateWrapper(value3);
									dictionary4 = dictionary2;
								}
								using (IDictionaryEnumerator enumerator4 = dictionary4.GetEnumerator())
								{
									while (enumerator4.MoveNext())
									{
										DictionaryEntry entry = enumerator4.Entry;
										dictionary3[entry.Key] = entry.Value;
									}
								}
								goto IL_64D;
							}
							goto IL_64D;
						}
					}
				}
			}
			if (contract.ExtensionDataSetter != null)
			{
				foreach (JsonSerializerInternalReader.CreatorPropertyContext creatorPropertyContext3 in list)
				{
					if (!creatorPropertyContext3.Used)
					{
						JsonSerializerInternalReader.PropertyPresence? presence = creatorPropertyContext3.Presence;
						JsonSerializerInternalReader.PropertyPresence propertyPresence = JsonSerializerInternalReader.PropertyPresence.None;
						if (!(presence.GetValueOrDefault() == propertyPresence & presence != null))
						{
							contract.ExtensionDataSetter(obj, creatorPropertyContext3.Name, creatorPropertyContext3.Value);
						}
					}
				}
			}
			if (flag)
			{
				foreach (JsonSerializerInternalReader.CreatorPropertyContext creatorPropertyContext4 in list)
				{
					if (creatorPropertyContext4.Property != null)
					{
						this.EndProcessProperty(obj, reader, contract, reader.Depth, creatorPropertyContext4.Property, creatorPropertyContext4.Presence.GetValueOrDefault(), !creatorPropertyContext4.Used);
					}
				}
			}
			this.OnDeserialized(reader, contract, obj);
			return obj;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0005B490 File Offset: 0x00059690
		[return: Nullable(2)]
		private object DeserializeConvertable(JsonConverter converter, JsonReader reader, Type objectType, [Nullable(2)] object existingValue)
		{
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Started deserializing {0} with converter {1}.".FormatWith(CultureInfo.InvariantCulture, objectType, converter.GetType())), null);
			}
			object result = converter.ReadJson(reader, objectType, existingValue, this.GetInternalSerializer());
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Finished deserializing {0} with converter {1}.".FormatWith(CultureInfo.InvariantCulture, objectType, converter.GetType())), null);
			}
			return result;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0005B554 File Offset: 0x00059754
		private List<JsonSerializerInternalReader.CreatorPropertyContext> ResolvePropertyAndCreatorValues(JsonObjectContract contract, [Nullable(2)] JsonProperty containerProperty, JsonReader reader, Type objectType)
		{
			List<JsonSerializerInternalReader.CreatorPropertyContext> list = new List<JsonSerializerInternalReader.CreatorPropertyContext>();
			bool flag = false;
			string text;
			for (;;)
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType != JsonToken.EndObject)
						{
							break;
						}
						flag = true;
					}
				}
				else
				{
					text = reader.Value.ToString();
					JsonSerializerInternalReader.CreatorPropertyContext creatorPropertyContext = new JsonSerializerInternalReader.CreatorPropertyContext(text)
					{
						ConstructorProperty = contract.CreatorParameters.GetClosestMatchProperty(text),
						Property = contract.Properties.GetClosestMatchProperty(text)
					};
					list.Add(creatorPropertyContext);
					JsonProperty jsonProperty = creatorPropertyContext.ConstructorProperty ?? creatorPropertyContext.Property;
					if (jsonProperty != null && !jsonProperty.Ignored)
					{
						if (jsonProperty.PropertyContract == null)
						{
							jsonProperty.PropertyContract = this.GetContractSafe(jsonProperty.PropertyType);
						}
						JsonConverter converter = this.GetConverter(jsonProperty.PropertyContract, jsonProperty.Converter, contract, containerProperty);
						if (!reader.ReadForType(jsonProperty.PropertyContract, converter != null))
						{
							goto Block_8;
						}
						if (converter != null && converter.CanRead)
						{
							creatorPropertyContext.Value = this.DeserializeConvertable(converter, reader, jsonProperty.PropertyType, null);
						}
						else
						{
							creatorPropertyContext.Value = this.CreateValueInternal(reader, jsonProperty.PropertyType, jsonProperty.PropertyContract, jsonProperty, contract, containerProperty, null);
						}
					}
					else
					{
						if (!reader.Read())
						{
							goto Block_11;
						}
						if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
						{
							this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Could not find member '{0}' on {1}.".FormatWith(CultureInfo.InvariantCulture, text, contract.UnderlyingType)), null);
						}
						if ((contract.MissingMemberHandling ?? this.Serializer._missingMemberHandling) == MissingMemberHandling.Error)
						{
							goto Block_15;
						}
						if (contract.ExtensionDataSetter != null)
						{
							creatorPropertyContext.Value = this.ReadExtensionDataValue(contract, containerProperty, reader);
						}
						else
						{
							reader.Skip();
						}
					}
				}
				if (flag || !reader.Read())
				{
					goto IL_27A;
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType.ToString());
			Block_8:
			throw JsonSerializationException.Create(reader, "Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, text));
			Block_11:
			throw JsonSerializationException.Create(reader, "Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, text));
			Block_15:
			throw JsonSerializationException.Create(reader, "Could not find member '{0}' on object of type '{1}'".FormatWith(CultureInfo.InvariantCulture, text, objectType.Name));
			IL_27A:
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, null, "Unexpected end when deserializing object.");
			}
			return list;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0005B7F4 File Offset: 0x000599F4
		public object CreateNewObject(JsonReader reader, JsonObjectContract objectContract, [Nullable(2)] JsonProperty containerMember, [Nullable(2)] JsonProperty containerProperty, [Nullable(2)] string id, out bool createdFromNonDefaultCreator)
		{
			object obj = null;
			if (objectContract.OverrideCreator != null)
			{
				if (objectContract.CreatorParameters.Count > 0)
				{
					createdFromNonDefaultCreator = true;
					return this.CreateObjectUsingCreatorWithParameters(reader, objectContract, containerMember, objectContract.OverrideCreator, id);
				}
				obj = objectContract.OverrideCreator(CollectionUtils.ArrayEmpty<object>());
			}
			else if (objectContract.DefaultCreator != null && (!objectContract.DefaultCreatorNonPublic || this.Serializer._constructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor || objectContract.ParameterizedCreator == null))
			{
				obj = objectContract.DefaultCreator();
			}
			else if (objectContract.ParameterizedCreator != null)
			{
				createdFromNonDefaultCreator = true;
				return this.CreateObjectUsingCreatorWithParameters(reader, objectContract, containerMember, objectContract.ParameterizedCreator, id);
			}
			if (obj != null)
			{
				createdFromNonDefaultCreator = false;
				return obj;
			}
			if (!objectContract.IsInstantiable)
			{
				throw JsonSerializationException.Create(reader, "Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantiated.".FormatWith(CultureInfo.InvariantCulture, objectContract.UnderlyingType));
			}
			throw JsonSerializationException.Create(reader, "Unable to find a constructor to use for type {0}. A class should either have a default constructor, one constructor with arguments or a constructor marked with the JsonConstructor attribute.".FormatWith(CultureInfo.InvariantCulture, objectContract.UnderlyingType));
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0005B904 File Offset: 0x00059B04
		private object PopulateObject(object newObject, JsonReader reader, JsonObjectContract contract, [Nullable(2)] JsonProperty member, [Nullable(2)] string id)
		{
			this.OnDeserializing(reader, contract, newObject);
			Dictionary<JsonProperty, JsonSerializerInternalReader.PropertyPresence> dictionary;
			if (!contract.HasRequiredOrDefaultValueProperties && !this.HasFlag(this.Serializer._defaultValueHandling, DefaultValueHandling.Populate))
			{
				dictionary = null;
			}
			else
			{
				dictionary = contract.Properties.ToDictionary((JsonProperty m) => m, (JsonProperty m) => JsonSerializerInternalReader.PropertyPresence.None);
			}
			Dictionary<JsonProperty, JsonSerializerInternalReader.PropertyPresence> dictionary2 = dictionary;
			if (id != null)
			{
				this.AddReference(reader, id, newObject);
			}
			int depth = reader.Depth;
			bool flag = false;
			for (;;)
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType != JsonToken.EndObject)
						{
							break;
						}
						goto IL_2C9;
					}
				}
				else
				{
					string text = reader.Value.ToString();
					if (!this.CheckPropertyName(reader, text))
					{
						try
						{
							JsonProperty closestMatchProperty = contract.Properties.GetClosestMatchProperty(text);
							if (closestMatchProperty != null)
							{
								if (closestMatchProperty.Ignored || !this.ShouldDeserialize(reader, closestMatchProperty, newObject))
								{
									if (reader.Read())
									{
										this.SetPropertyPresence(reader, closestMatchProperty, dictionary2);
										this.SetExtensionData(contract, member, reader, text, newObject);
									}
								}
								else
								{
									if (closestMatchProperty.PropertyContract == null)
									{
										closestMatchProperty.PropertyContract = this.GetContractSafe(closestMatchProperty.PropertyType);
									}
									JsonConverter converter = this.GetConverter(closestMatchProperty.PropertyContract, closestMatchProperty.Converter, contract, member);
									if (!reader.ReadForType(closestMatchProperty.PropertyContract, converter != null))
									{
										throw JsonSerializationException.Create(reader, "Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, text));
									}
									this.SetPropertyPresence(reader, closestMatchProperty, dictionary2);
									if (!this.SetPropertyValue(closestMatchProperty, converter, contract, member, reader, newObject))
									{
										this.SetExtensionData(contract, member, reader, text, newObject);
									}
								}
								goto IL_2F6;
							}
							if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
							{
								this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Could not find member '{0}' on {1}".FormatWith(CultureInfo.InvariantCulture, text, contract.UnderlyingType)), null);
							}
							if ((contract.MissingMemberHandling ?? this.Serializer._missingMemberHandling) == MissingMemberHandling.Error)
							{
								throw JsonSerializationException.Create(reader, "Could not find member '{0}' on object of type '{1}'".FormatWith(CultureInfo.InvariantCulture, text, contract.UnderlyingType.Name));
							}
							if (!reader.Read())
							{
								goto IL_2F6;
							}
							this.SetExtensionData(contract, member, reader, text, newObject);
							goto IL_2F6;
						}
						catch (Exception ex)
						{
							if (base.IsErrorHandled(newObject, contract, text, reader as IJsonLineInfo, reader.Path, ex))
							{
								this.HandleError(reader, true, depth);
								goto IL_2F6;
							}
							throw;
						}
						goto IL_2C9;
					}
				}
				IL_2F6:
				if (flag || !reader.Read())
				{
					goto IL_307;
				}
				continue;
				IL_2C9:
				flag = true;
				goto IL_2F6;
			}
			throw JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType.ToString());
			IL_307:
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, newObject, "Unexpected end when deserializing object.");
			}
			if (dictionary2 != null)
			{
				foreach (KeyValuePair<JsonProperty, JsonSerializerInternalReader.PropertyPresence> keyValuePair in dictionary2)
				{
					JsonProperty key = keyValuePair.Key;
					JsonSerializerInternalReader.PropertyPresence value = keyValuePair.Value;
					this.EndProcessProperty(newObject, reader, contract, depth, key, value, true);
				}
			}
			this.OnDeserialized(reader, contract, newObject);
			return newObject;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0005BCC4 File Offset: 0x00059EC4
		private bool ShouldDeserialize(JsonReader reader, JsonProperty property, object target)
		{
			if (property.ShouldDeserialize == null)
			{
				return true;
			}
			bool flag = property.ShouldDeserialize(target);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, reader.Path, "ShouldDeserialize result for property '{0}' on {1}: {2}".FormatWith(CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType, flag)), null);
			}
			return flag;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0005BD48 File Offset: 0x00059F48
		private bool CheckPropertyName(JsonReader reader, string memberName)
		{
			if (this.Serializer.MetadataPropertyHandling == MetadataPropertyHandling.ReadAhead && memberName != null && (memberName == "$id" || memberName == "$ref" || memberName == "$type" || memberName == "$values"))
			{
				reader.Skip();
				return true;
			}
			return false;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0005BDBC File Offset: 0x00059FBC
		private void SetExtensionData(JsonObjectContract contract, [Nullable(2)] JsonProperty member, JsonReader reader, string memberName, object o)
		{
			if (contract.ExtensionDataSetter != null)
			{
				try
				{
					object value = this.ReadExtensionDataValue(contract, member, reader);
					contract.ExtensionDataSetter(o, memberName, value);
					return;
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, "Error setting value in extension data for type '{0}'.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType), ex);
				}
			}
			reader.Skip();
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0005BE28 File Offset: 0x0005A028
		[return: Nullable(2)]
		private object ReadExtensionDataValue(JsonObjectContract contract, [Nullable(2)] JsonProperty member, JsonReader reader)
		{
			object result;
			if (contract.ExtensionDataIsJToken)
			{
				result = JToken.ReadFrom(reader);
			}
			else
			{
				result = this.CreateValueInternal(reader, null, null, null, contract, member, null);
			}
			return result;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0005BE60 File Offset: 0x0005A060
		private void EndProcessProperty(object newObject, JsonReader reader, JsonObjectContract contract, int initialDepth, JsonProperty property, JsonSerializerInternalReader.PropertyPresence presence, bool setDefaultValue)
		{
			if (presence == JsonSerializerInternalReader.PropertyPresence.None || presence == JsonSerializerInternalReader.PropertyPresence.Null)
			{
				try
				{
					Required required = property.Ignored ? Required.Default : (property._required ?? contract.ItemRequired.GetValueOrDefault());
					if (presence != JsonSerializerInternalReader.PropertyPresence.None)
					{
						if (presence == JsonSerializerInternalReader.PropertyPresence.Null)
						{
							if (required == Required.Always)
							{
								throw JsonSerializationException.Create(reader, "Required property '{0}' expects a value but got null.".FormatWith(CultureInfo.InvariantCulture, property.PropertyName));
							}
							if (required == Required.DisallowNull)
							{
								throw JsonSerializationException.Create(reader, "Required property '{0}' expects a non-null value.".FormatWith(CultureInfo.InvariantCulture, property.PropertyName));
							}
						}
					}
					else
					{
						if (required == Required.AllowNull || required == Required.Always)
						{
							throw JsonSerializationException.Create(reader, "Required property '{0}' not found in JSON.".FormatWith(CultureInfo.InvariantCulture, property.PropertyName));
						}
						if (setDefaultValue && !property.Ignored)
						{
							if (property.PropertyContract == null)
							{
								property.PropertyContract = this.GetContractSafe(property.PropertyType);
							}
							if (this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Populate) && property.Writable)
							{
								property.ValueProvider.SetValue(newObject, this.EnsureType(reader, property.GetResolvedDefaultValue(), CultureInfo.InvariantCulture, property.PropertyContract, property.PropertyType));
							}
						}
					}
				}
				catch (Exception ex)
				{
					if (!base.IsErrorHandled(newObject, contract, property.PropertyName, reader as IJsonLineInfo, reader.Path, ex))
					{
						throw;
					}
					this.HandleError(reader, true, initialDepth);
				}
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0005C038 File Offset: 0x0005A238
		private void SetPropertyPresence(JsonReader reader, JsonProperty property, [Nullable(new byte[]
		{
			2,
			1
		})] Dictionary<JsonProperty, JsonSerializerInternalReader.PropertyPresence> requiredProperties)
		{
			if (property != null && requiredProperties != null)
			{
				JsonToken tokenType = reader.TokenType;
				JsonSerializerInternalReader.PropertyPresence value;
				if (tokenType != JsonToken.String)
				{
					if (tokenType - JsonToken.Null > 1)
					{
						value = JsonSerializerInternalReader.PropertyPresence.Value;
					}
					else
					{
						value = JsonSerializerInternalReader.PropertyPresence.Null;
					}
				}
				else
				{
					value = (JsonSerializerInternalReader.CoerceEmptyStringToNull(property.PropertyType, property.PropertyContract, (string)reader.Value) ? JsonSerializerInternalReader.PropertyPresence.Null : JsonSerializerInternalReader.PropertyPresence.Value);
				}
				requiredProperties[property] = value;
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0005C0B4 File Offset: 0x0005A2B4
		private void HandleError(JsonReader reader, bool readPastError, int initialDepth)
		{
			base.ClearErrorContext();
			if (readPastError)
			{
				reader.Skip();
				while (reader.Depth > initialDepth && reader.Read())
				{
				}
			}
		}

		// Token: 0x020002F5 RID: 757
		[NullableContext(0)]
		internal enum PropertyPresence
		{
			// Token: 0x04000C55 RID: 3157
			None,
			// Token: 0x04000C56 RID: 3158
			Null,
			// Token: 0x04000C57 RID: 3159
			Value
		}

		// Token: 0x020002F6 RID: 758
		[NullableContext(2)]
		[Nullable(0)]
		internal class CreatorPropertyContext
		{
			// Token: 0x06001838 RID: 6200 RVA: 0x000794A0 File Offset: 0x000776A0
			[NullableContext(1)]
			public CreatorPropertyContext(string name)
			{
				this.Name = name;
			}

			// Token: 0x04000C58 RID: 3160
			[Nullable(1)]
			public readonly string Name;

			// Token: 0x04000C59 RID: 3161
			public JsonProperty Property;

			// Token: 0x04000C5A RID: 3162
			public JsonProperty ConstructorProperty;

			// Token: 0x04000C5B RID: 3163
			public JsonSerializerInternalReader.PropertyPresence? Presence;

			// Token: 0x04000C5C RID: 3164
			public object Value;

			// Token: 0x04000C5D RID: 3165
			public bool Used;
		}
	}
}
