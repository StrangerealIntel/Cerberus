using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020001C6 RID: 454
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaModel
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00061FB8 File Offset: 0x000601B8
		// (set) Token: 0x06001197 RID: 4503 RVA: 0x00061FC0 File Offset: 0x000601C0
		public bool Required { get; set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00061FCC File Offset: 0x000601CC
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x00061FD4 File Offset: 0x000601D4
		public JsonSchemaType Type { get; set; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00061FE0 File Offset: 0x000601E0
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x00061FE8 File Offset: 0x000601E8
		public int? MinimumLength { get; set; }

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x00061FF4 File Offset: 0x000601F4
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x00061FFC File Offset: 0x000601FC
		public int? MaximumLength { get; set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x00062008 File Offset: 0x00060208
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x00062010 File Offset: 0x00060210
		public double? DivisibleBy { get; set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0006201C File Offset: 0x0006021C
		// (set) Token: 0x060011A1 RID: 4513 RVA: 0x00062024 File Offset: 0x00060224
		public double? Minimum { get; set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00062030 File Offset: 0x00060230
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x00062038 File Offset: 0x00060238
		public double? Maximum { get; set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x00062044 File Offset: 0x00060244
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x0006204C File Offset: 0x0006024C
		public bool ExclusiveMinimum { get; set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00062058 File Offset: 0x00060258
		// (set) Token: 0x060011A7 RID: 4519 RVA: 0x00062060 File Offset: 0x00060260
		public bool ExclusiveMaximum { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0006206C File Offset: 0x0006026C
		// (set) Token: 0x060011A9 RID: 4521 RVA: 0x00062074 File Offset: 0x00060274
		public int? MinimumItems { get; set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x00062080 File Offset: 0x00060280
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x00062088 File Offset: 0x00060288
		public int? MaximumItems { get; set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x00062094 File Offset: 0x00060294
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x0006209C File Offset: 0x0006029C
		public IList<string> Patterns { get; set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x000620A8 File Offset: 0x000602A8
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x000620B0 File Offset: 0x000602B0
		public IList<JsonSchemaModel> Items { get; set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x000620BC File Offset: 0x000602BC
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x000620C4 File Offset: 0x000602C4
		public IDictionary<string, JsonSchemaModel> Properties { get; set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x000620D0 File Offset: 0x000602D0
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x000620D8 File Offset: 0x000602D8
		public IDictionary<string, JsonSchemaModel> PatternProperties { get; set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x000620E4 File Offset: 0x000602E4
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x000620EC File Offset: 0x000602EC
		public JsonSchemaModel AdditionalProperties { get; set; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x000620F8 File Offset: 0x000602F8
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x00062100 File Offset: 0x00060300
		public JsonSchemaModel AdditionalItems { get; set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0006210C File Offset: 0x0006030C
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x00062114 File Offset: 0x00060314
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x00062120 File Offset: 0x00060320
		// (set) Token: 0x060011BB RID: 4539 RVA: 0x00062128 File Offset: 0x00060328
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x00062134 File Offset: 0x00060334
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x0006213C File Offset: 0x0006033C
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x00062148 File Offset: 0x00060348
		// (set) Token: 0x060011BF RID: 4543 RVA: 0x00062150 File Offset: 0x00060350
		public bool UniqueItems { get; set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0006215C File Offset: 0x0006035C
		// (set) Token: 0x060011C1 RID: 4545 RVA: 0x00062164 File Offset: 0x00060364
		public IList<JToken> Enum { get; set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00062170 File Offset: 0x00060370
		// (set) Token: 0x060011C3 RID: 4547 RVA: 0x00062178 File Offset: 0x00060378
		public JsonSchemaType Disallow { get; set; }

		// Token: 0x060011C4 RID: 4548 RVA: 0x00062184 File Offset: 0x00060384
		public JsonSchemaModel()
		{
			this.Type = JsonSchemaType.Any;
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
			this.Required = false;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x000621B8 File Offset: 0x000603B8
		public static JsonSchemaModel Create(IList<JsonSchema> schemata)
		{
			JsonSchemaModel jsonSchemaModel = new JsonSchemaModel();
			foreach (JsonSchema schema in schemata)
			{
				JsonSchemaModel.Combine(jsonSchemaModel, schema);
			}
			return jsonSchemaModel;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00062210 File Offset: 0x00060410
		private static void Combine(JsonSchemaModel model, JsonSchema schema)
		{
			model.Required = (model.Required || schema.Required.GetValueOrDefault());
			model.Type &= (schema.Type ?? JsonSchemaType.Any);
			model.MinimumLength = MathUtils.Max(model.MinimumLength, schema.MinimumLength);
			model.MaximumLength = MathUtils.Min(model.MaximumLength, schema.MaximumLength);
			model.DivisibleBy = MathUtils.Max(model.DivisibleBy, schema.DivisibleBy);
			model.Minimum = MathUtils.Max(model.Minimum, schema.Minimum);
			model.Maximum = MathUtils.Max(model.Maximum, schema.Maximum);
			model.ExclusiveMinimum = (model.ExclusiveMinimum || schema.ExclusiveMinimum.GetValueOrDefault());
			model.ExclusiveMaximum = (model.ExclusiveMaximum || schema.ExclusiveMaximum.GetValueOrDefault());
			model.MinimumItems = MathUtils.Max(model.MinimumItems, schema.MinimumItems);
			model.MaximumItems = MathUtils.Min(model.MaximumItems, schema.MaximumItems);
			model.PositionalItemsValidation = (model.PositionalItemsValidation || schema.PositionalItemsValidation);
			model.AllowAdditionalProperties = (model.AllowAdditionalProperties && schema.AllowAdditionalProperties);
			model.AllowAdditionalItems = (model.AllowAdditionalItems && schema.AllowAdditionalItems);
			model.UniqueItems = (model.UniqueItems || schema.UniqueItems);
			if (schema.Enum != null)
			{
				if (model.Enum == null)
				{
					model.Enum = new List<JToken>();
				}
				model.Enum.AddRangeDistinct(schema.Enum, JToken.EqualityComparer);
			}
			model.Disallow |= schema.Disallow.GetValueOrDefault();
			if (schema.Pattern != null)
			{
				if (model.Patterns == null)
				{
					model.Patterns = new List<string>();
				}
				model.Patterns.AddDistinct(schema.Pattern);
			}
		}
	}
}
