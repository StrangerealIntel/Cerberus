using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000210 RID: 528
	[NullableContext(1)]
	internal interface IXmlElement : IXmlNode
	{
		// Token: 0x06001528 RID: 5416
		void SetAttributeNode(IXmlNode attribute);

		// Token: 0x06001529 RID: 5417
		string GetPrefixOfNamespace(string namespaceUri);

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600152A RID: 5418
		bool IsEmpty { get; }
	}
}
