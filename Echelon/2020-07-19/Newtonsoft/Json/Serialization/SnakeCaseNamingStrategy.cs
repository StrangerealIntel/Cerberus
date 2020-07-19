using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001BD RID: 445
	public class SnakeCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x060010BD RID: 4285 RVA: 0x0005EF24 File Offset: 0x0005D124
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0005EF3C File Offset: 0x0005D13C
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames) : this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0005EF50 File Offset: 0x0005D150
		public SnakeCaseNamingStrategy()
		{
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0005EF58 File Offset: 0x0005D158
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToSnakeCase(name);
		}
	}
}
