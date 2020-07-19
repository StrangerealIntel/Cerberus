using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001B5 RID: 437
	public class KebabCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06001097 RID: 4247 RVA: 0x0005EA4C File Offset: 0x0005CC4C
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0005EA64 File Offset: 0x0005CC64
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames) : this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0005EA78 File Offset: 0x0005CC78
		public KebabCaseNamingStrategy()
		{
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0005EA80 File Offset: 0x0005CC80
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToKebabCase(name);
		}
	}
}
