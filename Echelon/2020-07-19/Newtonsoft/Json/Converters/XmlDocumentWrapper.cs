using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000208 RID: 520
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlDocumentWrapper : XmlNodeWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x060014E6 RID: 5350 RVA: 0x0006F0EC File Offset: 0x0006D2EC
		public XmlDocumentWrapper(XmlDocument document) : base(document)
		{
			this._document = document;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0006F0FC File Offset: 0x0006D2FC
		public IXmlNode CreateComment([Nullable(2)] string data)
		{
			return new XmlNodeWrapper(this._document.CreateComment(data));
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0006F110 File Offset: 0x0006D310
		public IXmlNode CreateTextNode([Nullable(2)] string text)
		{
			return new XmlNodeWrapper(this._document.CreateTextNode(text));
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0006F124 File Offset: 0x0006D324
		public IXmlNode CreateCDataSection([Nullable(2)] string data)
		{
			return new XmlNodeWrapper(this._document.CreateCDataSection(data));
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0006F138 File Offset: 0x0006D338
		public IXmlNode CreateWhitespace([Nullable(2)] string text)
		{
			return new XmlNodeWrapper(this._document.CreateWhitespace(text));
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0006F14C File Offset: 0x0006D34C
		public IXmlNode CreateSignificantWhitespace([Nullable(2)] string text)
		{
			return new XmlNodeWrapper(this._document.CreateSignificantWhitespace(text));
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0006F160 File Offset: 0x0006D360
		[NullableContext(2)]
		[return: Nullable(1)]
		public IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XmlDeclarationWrapper(this._document.CreateXmlDeclaration(version, encoding, standalone));
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0006F178 File Offset: 0x0006D378
		[NullableContext(2)]
		[return: Nullable(1)]
		public IXmlNode CreateXmlDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			return new XmlDocumentTypeWrapper(this._document.CreateDocumentType(name, publicId, systemId, null));
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0006F190 File Offset: 0x0006D390
		public IXmlNode CreateProcessingInstruction(string target, [Nullable(2)] string data)
		{
			return new XmlNodeWrapper(this._document.CreateProcessingInstruction(target, data));
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0006F1A4 File Offset: 0x0006D3A4
		public IXmlElement CreateElement(string elementName)
		{
			return new XmlElementWrapper(this._document.CreateElement(elementName));
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0006F1B8 File Offset: 0x0006D3B8
		public IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			return new XmlElementWrapper(this._document.CreateElement(qualifiedName, namespaceUri));
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0006F1CC File Offset: 0x0006D3CC
		public IXmlNode CreateAttribute(string name, [Nullable(2)] string value)
		{
			return new XmlNodeWrapper(this._document.CreateAttribute(name))
			{
				Value = value
			};
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0006F1E8 File Offset: 0x0006D3E8
		public IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, [Nullable(2)] string value)
		{
			return new XmlNodeWrapper(this._document.CreateAttribute(qualifiedName, namespaceUri))
			{
				Value = value
			};
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x0006F204 File Offset: 0x0006D404
		[Nullable(2)]
		public IXmlElement DocumentElement
		{
			[NullableContext(2)]
			get
			{
				if (this._document.DocumentElement == null)
				{
					return null;
				}
				return new XmlElementWrapper(this._document.DocumentElement);
			}
		}

		// Token: 0x04000959 RID: 2393
		private readonly XmlDocument _document;
	}
}
