using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200020F RID: 527
	[NullableContext(1)]
	internal interface IXmlDocumentType : IXmlNode
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001524 RID: 5412
		string Name { get; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001525 RID: 5413
		string System { get; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001526 RID: 5414
		string Public { get; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001527 RID: 5415
		string InternalSubset { get; }
	}
}
