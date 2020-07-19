using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020001C1 RID: 449
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchema
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0005FDE8 File Offset: 0x0005DFE8
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x0005FDF0 File Offset: 0x0005DFF0
		public string Id { get; set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0005FDFC File Offset: 0x0005DFFC
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x0005FE04 File Offset: 0x0005E004
		public string Title { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x0005FE10 File Offset: 0x0005E010
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x0005FE18 File Offset: 0x0005E018
		public bool? Required { get; set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x0005FE24 File Offset: 0x0005E024
		// (set) Token: 0x0600111B RID: 4379 RVA: 0x0005FE2C File Offset: 0x0005E02C
		public bool? ReadOnly { get; set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x0005FE38 File Offset: 0x0005E038
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x0005FE40 File Offset: 0x0005E040
		public bool? Hidden { get; set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x0005FE4C File Offset: 0x0005E04C
		// (set) Token: 0x0600111F RID: 4383 RVA: 0x0005FE54 File Offset: 0x0005E054
		public bool? Transient { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x0005FE60 File Offset: 0x0005E060
		// (set) Token: 0x06001121 RID: 4385 RVA: 0x0005FE68 File Offset: 0x0005E068
		public string Description { get; set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0005FE74 File Offset: 0x0005E074
		// (set) Token: 0x06001123 RID: 4387 RVA: 0x0005FE7C File Offset: 0x0005E07C
		public JsonSchemaType? Type { get; set; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x0005FE88 File Offset: 0x0005E088
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x0005FE90 File Offset: 0x0005E090
		public string Pattern { get; set; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0005FE9C File Offset: 0x0005E09C
		// (set) Token: 0x06001127 RID: 4391 RVA: 0x0005FEA4 File Offset: 0x0005E0A4
		public int? MinimumLength { get; set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0005FEB0 File Offset: 0x0005E0B0
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x0005FEB8 File Offset: 0x0005E0B8
		public int? MaximumLength { get; set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0005FEC4 File Offset: 0x0005E0C4
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x0005FECC File Offset: 0x0005E0CC
		public double? DivisibleBy { get; set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x0005FED8 File Offset: 0x0005E0D8
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x0005FEE0 File Offset: 0x0005E0E0
		public double? Minimum { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0005FEEC File Offset: 0x0005E0EC
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x0005FEF4 File Offset: 0x0005E0F4
		public double? Maximum { get; set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0005FF00 File Offset: 0x0005E100
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x0005FF08 File Offset: 0x0005E108
		public bool? ExclusiveMinimum { get; set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0005FF14 File Offset: 0x0005E114
		// (set) Token: 0x06001133 RID: 4403 RVA: 0x0005FF1C File Offset: 0x0005E11C
		public bool? ExclusiveMaximum { get; set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x0005FF28 File Offset: 0x0005E128
		// (set) Token: 0x06001135 RID: 4405 RVA: 0x0005FF30 File Offset: 0x0005E130
		public int? MinimumItems { get; set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0005FF3C File Offset: 0x0005E13C
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x0005FF44 File Offset: 0x0005E144
		public int? MaximumItems { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0005FF50 File Offset: 0x0005E150
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x0005FF58 File Offset: 0x0005E158
		public IList<JsonSchema> Items { get; set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0005FF64 File Offset: 0x0005E164
		// (set) Token: 0x0600113B RID: 4411 RVA: 0x0005FF6C File Offset: 0x0005E16C
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0005FF78 File Offset: 0x0005E178
		// (set) Token: 0x0600113D RID: 4413 RVA: 0x0005FF80 File Offset: 0x0005E180
		public JsonSchema AdditionalItems { get; set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x0005FF8C File Offset: 0x0005E18C
		// (set) Token: 0x0600113F RID: 4415 RVA: 0x0005FF94 File Offset: 0x0005E194
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0005FFA0 File Offset: 0x0005E1A0
		// (set) Token: 0x06001141 RID: 4417 RVA: 0x0005FFA8 File Offset: 0x0005E1A8
		public bool UniqueItems { get; set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0005FFB4 File Offset: 0x0005E1B4
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x0005FFBC File Offset: 0x0005E1BC
		public IDictionary<string, JsonSchema> Properties { get; set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0005FFC8 File Offset: 0x0005E1C8
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x0005FFD0 File Offset: 0x0005E1D0
		public JsonSchema AdditionalProperties { get; set; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0005FFDC File Offset: 0x0005E1DC
		// (set) Token: 0x06001147 RID: 4423 RVA: 0x0005FFE4 File Offset: 0x0005E1E4
		public IDictionary<string, JsonSchema> PatternProperties { get; set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0005FFF0 File Offset: 0x0005E1F0
		// (set) Token: 0x06001149 RID: 4425 RVA: 0x0005FFF8 File Offset: 0x0005E1F8
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00060004 File Offset: 0x0005E204
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x0006000C File Offset: 0x0005E20C
		public string Requires { get; set; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x00060018 File Offset: 0x0005E218
		// (set) Token: 0x0600114D RID: 4429 RVA: 0x00060020 File Offset: 0x0005E220
		public IList<JToken> Enum { get; set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x0006002C File Offset: 0x0005E22C
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x00060034 File Offset: 0x0005E234
		public JsonSchemaType? Disallow { get; set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x00060040 File Offset: 0x0005E240
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x00060048 File Offset: 0x0005E248
		public JToken Default { get; set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x00060054 File Offset: 0x0005E254
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x0006005C File Offset: 0x0005E25C
		public IList<JsonSchema> Extends { get; set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x00060068 File Offset: 0x0005E268
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x00060070 File Offset: 0x0005E270
		public string Format { get; set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x0006007C File Offset: 0x0005E27C
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x00060084 File Offset: 0x0005E284
		internal string Location { get; set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x00060090 File Offset: 0x0005E290
		internal string InternalId
		{
			get
			{
				return this._internalId;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00060098 File Offset: 0x0005E298
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x000600A0 File Offset: 0x0005E2A0
		internal string DeferredReference { get; set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x000600AC File Offset: 0x0005E2AC
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x000600B4 File Offset: 0x0005E2B4
		internal bool ReferencesResolved { get; set; }

		// Token: 0x0600115D RID: 4445 RVA: 0x000600C0 File Offset: 0x0005E2C0
		public JsonSchema()
		{
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00060100 File Offset: 0x0005E300
		public static JsonSchema Read(JsonReader reader)
		{
			return JsonSchema.Read(reader, new JsonSchemaResolver());
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00060110 File Offset: 0x0005E310
		public static JsonSchema Read(JsonReader reader, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			return new JsonSchemaBuilder(resolver).Read(reader);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00060134 File Offset: 0x0005E334
		public static JsonSchema Parse(string json)
		{
			return JsonSchema.Parse(json, new JsonSchemaResolver());
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00060144 File Offset: 0x0005E344
		public static JsonSchema Parse(string json, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(json, "json");
			JsonSchema result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				result = JsonSchema.Read(jsonReader, resolver);
			}
			return result;
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00060194 File Offset: 0x0005E394
		public void WriteTo(JsonWriter writer)
		{
			this.WriteTo(writer, new JsonSchemaResolver());
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000601A4 File Offset: 0x0005E3A4
		public void WriteTo(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			new JsonSchemaWriter(writer, resolver).WriteSchema(this);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x000601CC File Offset: 0x0005E3CC
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteTo(new JsonTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			return stringWriter.ToString();
		}

		// Token: 0x04000866 RID: 2150
		private readonly string _internalId = Guid.NewGuid().ToString("N");
	}
}
