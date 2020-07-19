using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000211 RID: 529
	[NullableContext(2)]
	internal interface IXmlNode
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600152B RID: 5419
		XmlNodeType NodeType { get; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600152C RID: 5420
		string LocalName { get; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600152D RID: 5421
		[Nullable(1)]
		List<IXmlNode> ChildNodes { [NullableContext(1)] get; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600152E RID: 5422
		[Nullable(1)]
		List<IXmlNode> Attributes { [NullableContext(1)] get; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600152F RID: 5423
		IXmlNode ParentNode { get; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001530 RID: 5424
		// (set) Token: 0x06001531 RID: 5425
		string Value { get; set; }

		// Token: 0x06001532 RID: 5426
		[NullableContext(1)]
		IXmlNode AppendChild(IXmlNode newChild);

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001533 RID: 5427
		string NamespaceUri { get; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001534 RID: 5428
		object WrappedNode { get; }
	}
}
