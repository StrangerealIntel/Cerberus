using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001FC RID: 508
	[NullableContext(1)]
	[Nullable(0)]
	public class DataTableConverter : JsonConverter
	{
		// Token: 0x06001496 RID: 5270 RVA: 0x0006D0C0 File Offset: 0x0006B2C0
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			DataTable dataTable = (DataTable)value;
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			writer.WriteStartArray();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				writer.WriteStartObject();
				foreach (object obj2 in dataRow.Table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj2;
					object obj3 = dataRow[dataColumn];
					if (serializer.NullValueHandling != NullValueHandling.Ignore || (obj3 != null && obj3 != DBNull.Value))
					{
						writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(dataColumn.ColumnName) : dataColumn.ColumnName);
						serializer.Serialize(writer, obj3);
					}
				}
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0006D204 File Offset: 0x0006B404
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			DataTable dataTable = existingValue as DataTable;
			if (dataTable == null)
			{
				dataTable = ((objectType == typeof(DataTable)) ? new DataTable() : ((DataTable)Activator.CreateInstance(objectType)));
			}
			if (reader.TokenType == JsonToken.PropertyName)
			{
				dataTable.TableName = (string)reader.Value;
				reader.ReadAndAssert();
				if (reader.TokenType == JsonToken.Null)
				{
					return dataTable;
				}
			}
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonSerializationException.Create(reader, "Unexpected JSON token when reading DataTable. Expected StartArray, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			reader.ReadAndAssert();
			while (reader.TokenType != JsonToken.EndArray)
			{
				DataTableConverter.CreateRow(reader, dataTable, serializer);
				reader.ReadAndAssert();
			}
			return dataTable;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0006D2E0 File Offset: 0x0006B4E0
		private static void CreateRow(JsonReader reader, DataTable dt, JsonSerializer serializer)
		{
			DataRow dataRow = dt.NewRow();
			reader.ReadAndAssert();
			while (reader.TokenType == JsonToken.PropertyName)
			{
				string text = (string)reader.Value;
				reader.ReadAndAssert();
				DataColumn dataColumn = dt.Columns[text];
				if (dataColumn == null)
				{
					Type columnDataType = DataTableConverter.GetColumnDataType(reader);
					dataColumn = new DataColumn(text, columnDataType);
					dt.Columns.Add(dataColumn);
				}
				if (dataColumn.DataType == typeof(DataTable))
				{
					if (reader.TokenType == JsonToken.StartArray)
					{
						reader.ReadAndAssert();
					}
					DataTable dataTable = new DataTable();
					while (reader.TokenType != JsonToken.EndArray)
					{
						DataTableConverter.CreateRow(reader, dataTable, serializer);
						reader.ReadAndAssert();
					}
					dataRow[text] = dataTable;
				}
				else if (dataColumn.DataType.IsArray && dataColumn.DataType != typeof(byte[]))
				{
					if (reader.TokenType == JsonToken.StartArray)
					{
						reader.ReadAndAssert();
					}
					List<object> list = new List<object>();
					while (reader.TokenType != JsonToken.EndArray)
					{
						list.Add(reader.Value);
						reader.ReadAndAssert();
					}
					Array array = Array.CreateInstance(dataColumn.DataType.GetElementType(), list.Count);
					((ICollection)list).CopyTo(array, 0);
					dataRow[text] = array;
				}
				else
				{
					object value = (reader.Value != null) ? (serializer.Deserialize(reader, dataColumn.DataType) ?? DBNull.Value) : DBNull.Value;
					dataRow[text] = value;
				}
				reader.ReadAndAssert();
			}
			dataRow.EndEdit();
			dt.Rows.Add(dataRow);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0006D498 File Offset: 0x0006B698
		private static Type GetColumnDataType(JsonReader reader)
		{
			JsonToken tokenType = reader.TokenType;
			switch (tokenType)
			{
			case JsonToken.StartArray:
				reader.ReadAndAssert();
				if (reader.TokenType == JsonToken.StartObject)
				{
					return typeof(DataTable);
				}
				return DataTableConverter.GetColumnDataType(reader).MakeArrayType();
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Date:
			case JsonToken.Bytes:
				return reader.ValueType;
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.EndArray:
				return typeof(string);
			}
			throw JsonSerializationException.Create(reader, "Unexpected JSON token when reading DataTable: {0}".FormatWith(CultureInfo.InvariantCulture, tokenType));
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0006D554 File Offset: 0x0006B754
		public override bool CanConvert(Type valueType)
		{
			return typeof(DataTable).IsAssignableFrom(valueType);
		}
	}
}
