using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001DA RID: 474
	[NullableContext(1)]
	[Nullable(0)]
	public class JPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060012FB RID: 4859 RVA: 0x0006603C File Offset: 0x0006423C
		public JPropertyDescriptor(string name) : base(name, null)
		{
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00066048 File Offset: 0x00064248
		private static JObject CastInstance(object instance)
		{
			return (JObject)instance;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00066050 File Offset: 0x00064250
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00066054 File Offset: 0x00064254
		[return: Nullable(2)]
		public override object GetValue(object component)
		{
			JObject jobject = component as JObject;
			if (jobject == null)
			{
				return null;
			}
			return jobject[this.Name];
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00066070 File Offset: 0x00064270
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00066074 File Offset: 0x00064274
		public override void SetValue(object component, object value)
		{
			JObject jobject = component as JObject;
			if (jobject != null)
			{
				JToken value2 = (value as JToken) ?? new JValue(value);
				jobject[this.Name] = value2;
			}
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x000660B4 File Offset: 0x000642B4
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x000660B8 File Offset: 0x000642B8
		public override Type ComponentType
		{
			get
			{
				return typeof(JObject);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x000660C4 File Offset: 0x000642C4
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x000660C8 File Offset: 0x000642C8
		public override Type PropertyType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x000660D4 File Offset: 0x000642D4
		protected override int NameHashCode
		{
			get
			{
				return base.NameHashCode;
			}
		}
	}
}
