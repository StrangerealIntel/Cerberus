using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001DC RID: 476
	[NullableContext(1)]
	[Nullable(0)]
	public class JRaw : JValue
	{
		// Token: 0x0600131A RID: 4890 RVA: 0x000664E8 File Offset: 0x000646E8
		public JRaw(JRaw other) : base(other)
		{
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000664F4 File Offset: 0x000646F4
		[NullableContext(2)]
		public JRaw(object rawJson) : base(rawJson, JTokenType.Raw)
		{
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00066500 File Offset: 0x00064700
		public static JRaw Create(JsonReader reader)
		{
			JRaw result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.WriteToken(reader);
					result = new JRaw(stringWriter.ToString());
				}
			}
			return result;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00066570 File Offset: 0x00064770
		internal override JToken CloneToken()
		{
			return new JRaw(this);
		}
	}
}
