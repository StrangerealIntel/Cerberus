using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000204 RID: 516
	[NullableContext(1)]
	[Nullable(0)]
	public class RegexConverter : JsonConverter
	{
		// Token: 0x060014C5 RID: 5317 RVA: 0x0006E760 File Offset: 0x0006C960
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			Regex regex = (Regex)value;
			BsonWriter bsonWriter = writer as BsonWriter;
			if (bsonWriter != null)
			{
				this.WriteBson(bsonWriter, regex);
				return;
			}
			this.WriteJson(writer, regex, serializer);
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0006E7A4 File Offset: 0x0006C9A4
		private bool HasFlag(RegexOptions options, RegexOptions flag)
		{
			return (options & flag) == flag;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0006E7AC File Offset: 0x0006C9AC
		private void WriteBson(BsonWriter writer, Regex regex)
		{
			string text = null;
			if (this.HasFlag(regex.Options, RegexOptions.IgnoreCase))
			{
				text += "i";
			}
			if (this.HasFlag(regex.Options, RegexOptions.Multiline))
			{
				text += "m";
			}
			if (this.HasFlag(regex.Options, RegexOptions.Singleline))
			{
				text += "s";
			}
			text += "u";
			if (this.HasFlag(regex.Options, RegexOptions.ExplicitCapture))
			{
				text += "x";
			}
			writer.WriteRegex(regex.ToString(), text);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0006E854 File Offset: 0x0006CA54
		private void WriteJson(JsonWriter writer, Regex regex, JsonSerializer serializer)
		{
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Pattern") : "Pattern");
			writer.WriteValue(regex.ToString());
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Options") : "Options");
			serializer.Serialize(writer, regex.Options);
			writer.WriteEndObject();
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0006E8E0 File Offset: 0x0006CAE0
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.StartObject)
			{
				return this.ReadRegexObject(reader, serializer);
			}
			if (tokenType == JsonToken.String)
			{
				return this.ReadRegexString(reader);
			}
			if (tokenType != JsonToken.Null)
			{
				throw JsonSerializationException.Create(reader, "Unexpected token when reading Regex.");
			}
			return null;
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0006E934 File Offset: 0x0006CB34
		private object ReadRegexString(JsonReader reader)
		{
			string text = (string)reader.Value;
			if (text.Length > 0 && text[0] == '/')
			{
				int num = text.LastIndexOf('/');
				if (num > 0)
				{
					string pattern = text.Substring(1, num - 1);
					RegexOptions regexOptions = MiscellaneousUtils.GetRegexOptions(text.Substring(num + 1));
					return new Regex(pattern, regexOptions);
				}
			}
			throw JsonSerializationException.Create(reader, "Regex pattern must be enclosed by slashes.");
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0006E9A8 File Offset: 0x0006CBA8
		private Regex ReadRegexObject(JsonReader reader, JsonSerializer serializer)
		{
			string text = null;
			RegexOptions? regexOptions = null;
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType == JsonToken.EndObject)
						{
							if (text == null)
							{
								throw JsonSerializationException.Create(reader, "Error deserializing Regex. No pattern found.");
							}
							return new Regex(text, regexOptions.GetValueOrDefault());
						}
					}
				}
				else
				{
					string a = reader.Value.ToString();
					if (!reader.Read())
					{
						throw JsonSerializationException.Create(reader, "Unexpected end when reading Regex.");
					}
					if (string.Equals(a, "Pattern", StringComparison.OrdinalIgnoreCase))
					{
						text = (string)reader.Value;
					}
					else if (string.Equals(a, "Options", StringComparison.OrdinalIgnoreCase))
					{
						regexOptions = new RegexOptions?(serializer.Deserialize<RegexOptions>(reader));
					}
					else
					{
						reader.Skip();
					}
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected end when reading Regex.");
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0006EA94 File Offset: 0x0006CC94
		public override bool CanConvert(Type objectType)
		{
			return objectType.Name == "Regex" && this.IsRegex(objectType);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0006EAB4 File Offset: 0x0006CCB4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool IsRegex(Type objectType)
		{
			return objectType == typeof(Regex);
		}

		// Token: 0x04000954 RID: 2388
		private const string PatternName = "Pattern";

		// Token: 0x04000955 RID: 2389
		private const string OptionsName = "Options";
	}
}
