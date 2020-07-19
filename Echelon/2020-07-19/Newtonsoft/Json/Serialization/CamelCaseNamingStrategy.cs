using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200018D RID: 397
	public class CamelCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000E8B RID: 3723 RVA: 0x00053608 File Offset: 0x00051808
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00053620 File Offset: 0x00051820
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames) : this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00053634 File Offset: 0x00051834
		public CamelCaseNamingStrategy()
		{
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0005363C File Offset: 0x0005183C
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToCamelCase(name);
		}
	}
}
