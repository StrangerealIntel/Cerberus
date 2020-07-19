using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200020D RID: 525
	[NullableContext(1)]
	internal interface IXmlDocument : IXmlNode
	{
		// Token: 0x06001512 RID: 5394
		IXmlNode CreateComment([Nullable(2)] string text);

		// Token: 0x06001513 RID: 5395
		IXmlNode CreateTextNode([Nullable(2)] string text);

		// Token: 0x06001514 RID: 5396
		IXmlNode CreateCDataSection([Nullable(2)] string data);

		// Token: 0x06001515 RID: 5397
		IXmlNode CreateWhitespace([Nullable(2)] string text);

		// Token: 0x06001516 RID: 5398
		IXmlNode CreateSignificantWhitespace([Nullable(2)] string text);

		// Token: 0x06001517 RID: 5399
		[NullableContext(2)]
		[return: Nullable(1)]
		IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone);

		// Token: 0x06001518 RID: 5400
		[NullableContext(2)]
		[return: Nullable(1)]
		IXmlNode CreateXmlDocumentType(string name, string publicId, string systemId, string internalSubset);

		// Token: 0x06001519 RID: 5401
		IXmlNode CreateProcessingInstruction(string target, [Nullable(2)] string data);

		// Token: 0x0600151A RID: 5402
		IXmlElement CreateElement(string elementName);

		// Token: 0x0600151B RID: 5403
		IXmlElement CreateElement(string qualifiedName, string namespaceUri);

		// Token: 0x0600151C RID: 5404
		IXmlNode CreateAttribute(string name, [Nullable(2)] string value);

		// Token: 0x0600151D RID: 5405
		IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, [Nullable(2)] string value);

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600151E RID: 5406
		[Nullable(2)]
		IXmlElement DocumentElement { [NullableContext(2)] get; }
	}
}
