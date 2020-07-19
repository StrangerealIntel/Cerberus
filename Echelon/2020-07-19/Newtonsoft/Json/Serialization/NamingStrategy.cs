using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001B7 RID: 439
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class NamingStrategy
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0005EC4C File Offset: 0x0005CE4C
		// (set) Token: 0x060010A2 RID: 4258 RVA: 0x0005EC54 File Offset: 0x0005CE54
		public bool ProcessDictionaryKeys { get; set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0005EC60 File Offset: 0x0005CE60
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x0005EC68 File Offset: 0x0005CE68
		public bool ProcessExtensionDataNames { get; set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0005EC74 File Offset: 0x0005CE74
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x0005EC7C File Offset: 0x0005CE7C
		public bool OverrideSpecifiedNames { get; set; }

		// Token: 0x060010A7 RID: 4263 RVA: 0x0005EC88 File Offset: 0x0005CE88
		public virtual string GetPropertyName(string name, bool hasSpecifiedName)
		{
			if (hasSpecifiedName && !this.OverrideSpecifiedNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0005ECA4 File Offset: 0x0005CEA4
		public virtual string GetExtensionDataName(string name)
		{
			if (!this.ProcessExtensionDataNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0005ECBC File Offset: 0x0005CEBC
		public virtual string GetDictionaryKey(string key)
		{
			if (!this.ProcessDictionaryKeys)
			{
				return key;
			}
			return this.ResolvePropertyName(key);
		}

		// Token: 0x060010AA RID: 4266
		protected abstract string ResolvePropertyName(string name);

		// Token: 0x060010AB RID: 4267 RVA: 0x0005ECD4 File Offset: 0x0005CED4
		public override int GetHashCode()
		{
			return ((base.GetType().GetHashCode() * 397 ^ this.ProcessDictionaryKeys.GetHashCode()) * 397 ^ this.ProcessExtensionDataNames.GetHashCode()) * 397 ^ this.OverrideSpecifiedNames.GetHashCode();
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0005ED30 File Offset: 0x0005CF30
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NamingStrategy);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0005ED40 File Offset: 0x0005CF40
		[NullableContext(2)]
		protected bool Equals(NamingStrategy other)
		{
			return other != null && (base.GetType() == other.GetType() && this.ProcessDictionaryKeys == other.ProcessDictionaryKeys && this.ProcessExtensionDataNames == other.ProcessExtensionDataNames) && this.OverrideSpecifiedNames == other.OverrideSpecifiedNames;
		}
	}
}
