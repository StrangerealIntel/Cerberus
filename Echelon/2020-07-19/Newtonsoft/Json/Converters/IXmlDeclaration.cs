using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200020E RID: 526
	[NullableContext(1)]
	internal interface IXmlDeclaration : IXmlNode
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600151F RID: 5407
		string Version { get; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001520 RID: 5408
		// (set) Token: 0x06001521 RID: 5409
		string Encoding { get; set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001522 RID: 5410
		// (set) Token: 0x06001523 RID: 5411
		string Standalone { get; set; }
	}
}
