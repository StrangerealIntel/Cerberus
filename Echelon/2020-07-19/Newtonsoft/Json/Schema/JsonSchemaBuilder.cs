using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020001C2 RID: 450
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaBuilder
	{
		// Token: 0x06001165 RID: 4453 RVA: 0x00060204 File Offset: 0x0005E404
		public JsonSchemaBuilder(JsonSchemaResolver resolver)
		{
			this._stack = new List<JsonSchema>();
			this._documentSchemas = new Dictionary<string, JsonSchema>();
			this._resolver = resolver;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0006022C File Offset: 0x0005E42C
		private void Push(JsonSchema value)
		{
			this._currentSchema = value;
			this._stack.Add(value);
			this._resolver.LoadedSchemas.Add(value);
			this._documentSchemas.Add(value.Location, value);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00060274 File Offset: 0x0005E474
		private JsonSchema Pop()
		{
			JsonSchema currentSchema = this._currentSchema;
			this._stack.RemoveAt(this._stack.Count - 1);
			this._currentSchema = this._stack.LastOrDefault<JsonSchema>();
			return currentSchema;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x000602A8 File Offset: 0x0005E4A8
		private JsonSchema CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x000602B0 File Offset: 0x0005E4B0
		internal JsonSchema Read(JsonReader reader)
		{
			JToken jtoken = JToken.ReadFrom(reader);
			this._rootSchema = (jtoken as JObject);
			JsonSchema jsonSchema = this.BuildSchema(jtoken);
			this.ResolveReferences(jsonSchema);
			return jsonSchema;
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x000602E8 File Offset: 0x0005E4E8
		private string UnescapeReference(string reference)
		{
			return Uri.UnescapeDataString(reference).Replace("~1", "/").Replace("~0", "~");
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00060310 File Offset: 0x0005E510
		private JsonSchema ResolveReferences(JsonSchema schema)
		{
			if (schema.DeferredReference != null)
			{
				string text = schema.DeferredReference;
				bool flag = text.StartsWith("#", StringComparison.Ordinal);
				if (flag)
				{
					text = this.UnescapeReference(text);
				}
				JsonSchema jsonSchema = this._resolver.GetSchema(text);
				if (jsonSchema == null)
				{
					if (flag)
					{
						string[] array = schema.DeferredReference.TrimStart(new char[]
						{
							'#'
						}).Split(new char[]
						{
							'/'
						}, StringSplitOptions.RemoveEmptyEntries);
						JToken jtoken = this._rootSchema;
						foreach (string reference in array)
						{
							string text2 = this.UnescapeReference(reference);
							if (jtoken.Type == JTokenType.Object)
							{
								jtoken = jtoken[text2];
							}
							else if (jtoken.Type == JTokenType.Array || jtoken.Type == JTokenType.Constructor)
							{
								int num;
								if (int.TryParse(text2, out num) && num >= 0 && num < jtoken.Count<JToken>())
								{
									jtoken = jtoken[num];
								}
								else
								{
									jtoken = null;
								}
							}
							if (jtoken == null)
							{
								break;
							}
						}
						if (jtoken != null)
						{
							jsonSchema = this.BuildSchema(jtoken);
						}
					}
					if (jsonSchema == null)
					{
						throw new JsonException("Could not resolve schema reference '{0}'.".FormatWith(CultureInfo.InvariantCulture, schema.DeferredReference));
					}
				}
				schema = jsonSchema;
			}
			if (schema.ReferencesResolved)
			{
				return schema;
			}
			schema.ReferencesResolved = true;
			if (schema.Extends != null)
			{
				for (int j = 0; j < schema.Extends.Count; j++)
				{
					schema.Extends[j] = this.ResolveReferences(schema.Extends[j]);
				}
			}
			if (schema.Items != null)
			{
				for (int k = 0; k < schema.Items.Count; k++)
				{
					schema.Items[k] = this.ResolveReferences(schema.Items[k]);
				}
			}
			if (schema.AdditionalItems != null)
			{
				schema.AdditionalItems = this.ResolveReferences(schema.AdditionalItems);
			}
			if (schema.PatternProperties != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair in schema.PatternProperties.ToList<KeyValuePair<string, JsonSchema>>())
				{
					schema.PatternProperties[keyValuePair.Key] = this.ResolveReferences(keyValuePair.Value);
				}
			}
			if (schema.Properties != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair2 in schema.Properties.ToList<KeyValuePair<string, JsonSchema>>())
				{
					schema.Properties[keyValuePair2.Key] = this.ResolveReferences(keyValuePair2.Value);
				}
			}
			if (schema.AdditionalProperties != null)
			{
				schema.AdditionalProperties = this.ResolveReferences(schema.AdditionalProperties);
			}
			return schema;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0006062C File Offset: 0x0005E82C
		private JsonSchema BuildSchema(JToken token)
		{
			JObject jobject = token as JObject;
			if (jobject == null)
			{
				throw JsonException.Create(token, token.Path, "Expected object while parsing schema object, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			JToken value;
			if (jobject.TryGetValue("$ref", out value))
			{
				return new JsonSchema
				{
					DeferredReference = (string)value
				};
			}
			string text = token.Path.Replace(".", "/").Replace("[", "/").Replace("]", string.Empty);
			if (!StringUtils.IsNullOrEmpty(text))
			{
				text = "/" + text;
			}
			text = "#" + text;
			JsonSchema result;
			if (this._documentSchemas.TryGetValue(text, out result))
			{
				return result;
			}
			this.Push(new JsonSchema
			{
				Location = text
			});
			this.ProcessSchemaProperties(jobject);
			return this.Pop();
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00060720 File Offset: 0x0005E920
		private void ProcessSchemaProperties(JObject schemaObject)
		{
			foreach (KeyValuePair<string, JToken> keyValuePair in schemaObject)
			{
				string key = keyValuePair.Key;
				if (key != null)
				{
					uint num = Newtonsoft.Json.<PrivateImplementationDetails>.ComputeStringHash(key);
					if (num <= 2223801888u)
					{
						if (num <= 981021583u)
						{
							if (num <= 353304077u)
							{
								if (num != 299789532u)
								{
									if (num != 334560121u)
									{
										if (num == 353304077u)
										{
											if (key == "divisibleBy")
											{
												this.CurrentSchema.DivisibleBy = new double?((double)keyValuePair.Value);
											}
										}
									}
									else if (key == "minItems")
									{
										this.CurrentSchema.MinimumItems = new int?((int)keyValuePair.Value);
									}
								}
								else if (key == "properties")
								{
									this.CurrentSchema.Properties = this.ProcessProperties(keyValuePair.Value);
								}
							}
							else if (num <= 879704937u)
							{
								if (num != 479998177u)
								{
									if (num == 879704937u)
									{
										if (key == "description")
										{
											this.CurrentSchema.Description = (string)keyValuePair.Value;
										}
									}
								}
								else if (key == "additionalProperties")
								{
									this.ProcessAdditionalProperties(keyValuePair.Value);
								}
							}
							else if (num != 926444256u)
							{
								if (num == 981021583u)
								{
									if (key == "items")
									{
										this.ProcessItems(keyValuePair.Value);
									}
								}
							}
							else if (key == "id")
							{
								this.CurrentSchema.Id = (string)keyValuePair.Value;
							}
						}
						else if (num <= 1693958795u)
						{
							if (num != 1361572173u)
							{
								if (num != 1542649473u)
								{
									if (num == 1693958795u)
									{
										if (key == "exclusiveMaximum")
										{
											this.CurrentSchema.ExclusiveMaximum = new bool?((bool)keyValuePair.Value);
										}
									}
								}
								else if (key == "maximum")
								{
									this.CurrentSchema.Maximum = new double?((double)keyValuePair.Value);
								}
							}
							else if (key == "type")
							{
								this.CurrentSchema.Type = this.ProcessType(keyValuePair.Value);
							}
						}
						else if (num <= 2051482624u)
						{
							if (num != 1913005517u)
							{
								if (num == 2051482624u)
								{
									if (key == "additionalItems")
									{
										this.ProcessAdditionalItems(keyValuePair.Value);
									}
								}
							}
							else if (key == "exclusiveMinimum")
							{
								this.CurrentSchema.ExclusiveMinimum = new bool?((bool)keyValuePair.Value);
							}
						}
						else if (num != 2171383808u)
						{
							if (num == 2223801888u)
							{
								if (key == "required")
								{
									this.CurrentSchema.Required = new bool?((bool)keyValuePair.Value);
								}
							}
						}
						else if (key == "enum")
						{
							this.ProcessEnum(keyValuePair.Value);
						}
					}
					else if (num <= 2692244416u)
					{
						if (num <= 2474713847u)
						{
							if (num != 2268922153u)
							{
								if (num != 2470140894u)
								{
									if (num == 2474713847u)
									{
										if (key == "minimum")
										{
											this.CurrentSchema.Minimum = new double?((double)keyValuePair.Value);
										}
									}
								}
								else if (key == "default")
								{
									this.CurrentSchema.Default = keyValuePair.Value.DeepClone();
								}
							}
							else if (key == "pattern")
							{
								this.CurrentSchema.Pattern = (string)keyValuePair.Value;
							}
						}
						else if (num <= 2609687125u)
						{
							if (num != 2556802313u)
							{
								if (num == 2609687125u)
								{
									if (key == "requires")
									{
										this.CurrentSchema.Requires = (string)keyValuePair.Value;
									}
								}
							}
							else if (key == "title")
							{
								this.CurrentSchema.Title = (string)keyValuePair.Value;
							}
						}
						else if (num != 2642794062u)
						{
							if (num == 2692244416u)
							{
								if (key == "disallow")
								{
									this.CurrentSchema.Disallow = this.ProcessType(keyValuePair.Value);
								}
							}
						}
						else if (key == "extends")
						{
							this.ProcessExtends(keyValuePair.Value);
						}
					}
					else if (num <= 3522602594u)
					{
						if (num <= 3114108242u)
						{
							if (num != 2957261815u)
							{
								if (num == 3114108242u)
								{
									if (key == "format")
									{
										this.CurrentSchema.Format = (string)keyValuePair.Value;
									}
								}
							}
							else if (key == "minLength")
							{
								this.CurrentSchema.MinimumLength = new int?((int)keyValuePair.Value);
							}
						}
						else if (num != 3456888823u)
						{
							if (num == 3522602594u)
							{
								if (key == "uniqueItems")
								{
									this.CurrentSchema.UniqueItems = (bool)keyValuePair.Value;
								}
							}
						}
						else if (key == "readonly")
						{
							this.CurrentSchema.ReadOnly = new bool?((bool)keyValuePair.Value);
						}
					}
					else if (num <= 3947606640u)
					{
						if (num != 3526559937u)
						{
							if (num == 3947606640u)
							{
								if (key == "patternProperties")
								{
									this.CurrentSchema.PatternProperties = this.ProcessProperties(keyValuePair.Value);
								}
							}
						}
						else if (key == "maxLength")
						{
							this.CurrentSchema.MaximumLength = new int?((int)keyValuePair.Value);
						}
					}
					else if (num != 4128829753u)
					{
						if (num == 4244322099u)
						{
							if (key == "maxItems")
							{
								this.CurrentSchema.MaximumItems = new int?((int)keyValuePair.Value);
							}
						}
					}
					else if (key == "hidden")
					{
						this.CurrentSchema.Hidden = new bool?((bool)keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00060F2C File Offset: 0x0005F12C
		private void ProcessExtends(JToken token)
		{
			IList<JsonSchema> list = new List<JsonSchema>();
			if (token.Type == JTokenType.Array)
			{
				using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)token).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JToken token2 = enumerator.Current;
						list.Add(this.BuildSchema(token2));
					}
					goto IL_61;
				}
			}
			JsonSchema jsonSchema = this.BuildSchema(token);
			if (jsonSchema != null)
			{
				list.Add(jsonSchema);
			}
			IL_61:
			if (list.Count > 0)
			{
				this.CurrentSchema.Extends = list;
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00060FC4 File Offset: 0x0005F1C4
		private void ProcessEnum(JToken token)
		{
			if (token.Type != JTokenType.Array)
			{
				throw JsonException.Create(token, token.Path, "Expected Array token while parsing enum values, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			this.CurrentSchema.Enum = new List<JToken>();
			foreach (JToken jtoken in ((IEnumerable<JToken>)token))
			{
				this.CurrentSchema.Enum.Add(jtoken.DeepClone());
			}
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00061068 File Offset: 0x0005F268
		private void ProcessAdditionalProperties(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalProperties = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalProperties = this.BuildSchema(token);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x000610AC File Offset: 0x0005F2AC
		private void ProcessAdditionalItems(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalItems = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalItems = this.BuildSchema(token);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000610F0 File Offset: 0x0005F2F0
		private IDictionary<string, JsonSchema> ProcessProperties(JToken token)
		{
			IDictionary<string, JsonSchema> dictionary = new Dictionary<string, JsonSchema>();
			if (token.Type != JTokenType.Object)
			{
				throw JsonException.Create(token, token.Path, "Expected Object token while parsing schema properties, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			foreach (JToken jtoken in ((IEnumerable<JToken>)token))
			{
				JProperty jproperty = (JProperty)jtoken;
				if (dictionary.ContainsKey(jproperty.Name))
				{
					throw new JsonException("Property {0} has already been defined in schema.".FormatWith(CultureInfo.InvariantCulture, jproperty.Name));
				}
				dictionary.Add(jproperty.Name, this.BuildSchema(jproperty.Value));
			}
			return dictionary;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000611C0 File Offset: 0x0005F3C0
		private void ProcessItems(JToken token)
		{
			this.CurrentSchema.Items = new List<JsonSchema>();
			JTokenType type = token.Type;
			if (type != JTokenType.Object)
			{
				if (type == JTokenType.Array)
				{
					this.CurrentSchema.PositionalItemsValidation = true;
					using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)token).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							JToken token2 = enumerator.Current;
							this.CurrentSchema.Items.Add(this.BuildSchema(token2));
						}
						return;
					}
				}
				throw JsonException.Create(token, token.Path, "Expected array or JSON schema object, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			this.CurrentSchema.Items.Add(this.BuildSchema(token));
			this.CurrentSchema.PositionalItemsValidation = false;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x000612A4 File Offset: 0x0005F4A4
		private JsonSchemaType? ProcessType(JToken token)
		{
			JTokenType type = token.Type;
			if (type == JTokenType.Array)
			{
				JsonSchemaType? jsonSchemaType = new JsonSchemaType?(JsonSchemaType.None);
				foreach (JToken jtoken in ((IEnumerable<JToken>)token))
				{
					if (jtoken.Type != JTokenType.String)
					{
						throw JsonException.Create(jtoken, jtoken.Path, "Expected JSON schema type string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
					}
					jsonSchemaType |= JsonSchemaBuilder.MapType((string)jtoken);
				}
				return jsonSchemaType;
			}
			if (type != JTokenType.String)
			{
				throw JsonException.Create(token, token.Path, "Expected array or JSON schema type string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			return new JsonSchemaType?(JsonSchemaBuilder.MapType((string)token));
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000613B8 File Offset: 0x0005F5B8
		internal static JsonSchemaType MapType(string type)
		{
			JsonSchemaType result;
			if (!JsonSchemaConstants.JsonSchemaTypeMapping.TryGetValue(type, out result))
			{
				throw new JsonException("Invalid JSON schema type: {0}".FormatWith(CultureInfo.InvariantCulture, type));
			}
			return result;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000613F4 File Offset: 0x0005F5F4
		internal static string MapType(JsonSchemaType type)
		{
			return JsonSchemaConstants.JsonSchemaTypeMapping.Single((KeyValuePair<string, JsonSchemaType> kv) => kv.Value == type).Key;
		}

		// Token: 0x04000869 RID: 2153
		private readonly IList<JsonSchema> _stack;

		// Token: 0x0400086A RID: 2154
		private readonly JsonSchemaResolver _resolver;

		// Token: 0x0400086B RID: 2155
		private readonly IDictionary<string, JsonSchema> _documentSchemas;

		// Token: 0x0400086C RID: 2156
		private JsonSchema _currentSchema;

		// Token: 0x0400086D RID: 2157
		private JObject _rootSchema;
	}
}
