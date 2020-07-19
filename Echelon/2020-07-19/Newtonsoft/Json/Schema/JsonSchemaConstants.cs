using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020001C3 RID: 451
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal static class JsonSchemaConstants
	{
		// Token: 0x0400086E RID: 2158
		public const string TypePropertyName = "type";

		// Token: 0x0400086F RID: 2159
		public const string PropertiesPropertyName = "properties";

		// Token: 0x04000870 RID: 2160
		public const string ItemsPropertyName = "items";

		// Token: 0x04000871 RID: 2161
		public const string AdditionalItemsPropertyName = "additionalItems";

		// Token: 0x04000872 RID: 2162
		public const string RequiredPropertyName = "required";

		// Token: 0x04000873 RID: 2163
		public const string PatternPropertiesPropertyName = "patternProperties";

		// Token: 0x04000874 RID: 2164
		public const string AdditionalPropertiesPropertyName = "additionalProperties";

		// Token: 0x04000875 RID: 2165
		public const string RequiresPropertyName = "requires";

		// Token: 0x04000876 RID: 2166
		public const string MinimumPropertyName = "minimum";

		// Token: 0x04000877 RID: 2167
		public const string MaximumPropertyName = "maximum";

		// Token: 0x04000878 RID: 2168
		public const string ExclusiveMinimumPropertyName = "exclusiveMinimum";

		// Token: 0x04000879 RID: 2169
		public const string ExclusiveMaximumPropertyName = "exclusiveMaximum";

		// Token: 0x0400087A RID: 2170
		public const string MinimumItemsPropertyName = "minItems";

		// Token: 0x0400087B RID: 2171
		public const string MaximumItemsPropertyName = "maxItems";

		// Token: 0x0400087C RID: 2172
		public const string PatternPropertyName = "pattern";

		// Token: 0x0400087D RID: 2173
		public const string MaximumLengthPropertyName = "maxLength";

		// Token: 0x0400087E RID: 2174
		public const string MinimumLengthPropertyName = "minLength";

		// Token: 0x0400087F RID: 2175
		public const string EnumPropertyName = "enum";

		// Token: 0x04000880 RID: 2176
		public const string ReadOnlyPropertyName = "readonly";

		// Token: 0x04000881 RID: 2177
		public const string TitlePropertyName = "title";

		// Token: 0x04000882 RID: 2178
		public const string DescriptionPropertyName = "description";

		// Token: 0x04000883 RID: 2179
		public const string FormatPropertyName = "format";

		// Token: 0x04000884 RID: 2180
		public const string DefaultPropertyName = "default";

		// Token: 0x04000885 RID: 2181
		public const string TransientPropertyName = "transient";

		// Token: 0x04000886 RID: 2182
		public const string DivisibleByPropertyName = "divisibleBy";

		// Token: 0x04000887 RID: 2183
		public const string HiddenPropertyName = "hidden";

		// Token: 0x04000888 RID: 2184
		public const string DisallowPropertyName = "disallow";

		// Token: 0x04000889 RID: 2185
		public const string ExtendsPropertyName = "extends";

		// Token: 0x0400088A RID: 2186
		public const string IdPropertyName = "id";

		// Token: 0x0400088B RID: 2187
		public const string UniqueItemsPropertyName = "uniqueItems";

		// Token: 0x0400088C RID: 2188
		public const string OptionValuePropertyName = "value";

		// Token: 0x0400088D RID: 2189
		public const string OptionLabelPropertyName = "label";

		// Token: 0x0400088E RID: 2190
		public static readonly IDictionary<string, JsonSchemaType> JsonSchemaTypeMapping = new Dictionary<string, JsonSchemaType>
		{
			{
				"string",
				JsonSchemaType.String
			},
			{
				"object",
				JsonSchemaType.Object
			},
			{
				"integer",
				JsonSchemaType.Integer
			},
			{
				"number",
				JsonSchemaType.Float
			},
			{
				"null",
				JsonSchemaType.Null
			},
			{
				"boolean",
				JsonSchemaType.Boolean
			},
			{
				"array",
				JsonSchemaType.Array
			},
			{
				"any",
				JsonSchemaType.Any
			}
		};
	}
}
