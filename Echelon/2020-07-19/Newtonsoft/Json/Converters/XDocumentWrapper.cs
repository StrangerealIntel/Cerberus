using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000214 RID: 532
	[NullableContext(1)]
	[Nullable(0)]
	internal class XDocumentWrapper : XContainerWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x0006F720 File Offset: 0x0006D920
		private XDocument Document
		{
			get
			{
				return (XDocument)base.WrappedNode;
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0006F730 File Offset: 0x0006D930
		public XDocumentWrapper(XDocument document) : base(document)
		{
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x0006F73C File Offset: 0x0006D93C
		public override List<IXmlNode> ChildNodes
		{
			get
			{
				List<IXmlNode> childNodes = base.ChildNodes;
				if (this.Document.Declaration != null && (childNodes.Count == 0 || childNodes[0].NodeType != XmlNodeType.XmlDeclaration))
				{
					childNodes.Insert(0, new XDeclarationWrapper(this.Document.Declaration));
				}
				return childNodes;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x0006F79C File Offset: 0x0006D99C
		protected override bool HasChildNodes
		{
			get
			{
				return base.HasChildNodes || this.Document.Declaration != null;
			}
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0006F7BC File Offset: 0x0006D9BC
		public IXmlNode CreateComment([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XComment(text));
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0006F7CC File Offset: 0x0006D9CC
		public IXmlNode CreateTextNode([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0006F7DC File Offset: 0x0006D9DC
		public IXmlNode CreateCDataSection([Nullable(2)] string data)
		{
			return new XObjectWrapper(new XCData(data));
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0006F7EC File Offset: 0x0006D9EC
		public IXmlNode CreateWhitespace([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0006F7FC File Offset: 0x0006D9FC
		public IXmlNode CreateSignificantWhitespace([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0006F80C File Offset: 0x0006DA0C
		[NullableContext(2)]
		[return: Nullable(1)]
		public IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XDeclarationWrapper(new XDeclaration(version, encoding, standalone));
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0006F81C File Offset: 0x0006DA1C
		[NullableContext(2)]
		[return: Nullable(1)]
		public IXmlNode CreateXmlDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			return new XDocumentTypeWrapper(new XDocumentType(name, publicId, systemId, internalSubset));
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0006F830 File Offset: 0x0006DA30
		public IXmlNode CreateProcessingInstruction(string target, [Nullable(2)] string data)
		{
			return new XProcessingInstructionWrapper(new XProcessingInstruction(target, data));
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0006F840 File Offset: 0x0006DA40
		public IXmlElement CreateElement(string elementName)
		{
			return new XElementWrapper(new XElement(elementName));
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0006F854 File Offset: 0x0006DA54
		public IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			return new XElementWrapper(new XElement(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri)));
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0006F86C File Offset: 0x0006DA6C
		public IXmlNode CreateAttribute(string name, [Nullable(2)] string value)
		{
			return new XAttributeWrapper(new XAttribute(name, value));
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0006F880 File Offset: 0x0006DA80
		public IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, [Nullable(2)] string value)
		{
			return new XAttributeWrapper(new XAttribute(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri), value));
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0006F89C File Offset: 0x0006DA9C
		[Nullable(2)]
		public IXmlElement DocumentElement
		{
			[NullableContext(2)]
			get
			{
				if (this.Document.Root == null)
				{
					return null;
				}
				return new XElementWrapper(this.Document.Root);
			}
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0006F8C0 File Offset: 0x0006DAC0
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			XDeclarationWrapper xdeclarationWrapper = newChild as XDeclarationWrapper;
			if (xdeclarationWrapper != null)
			{
				this.Document.Declaration = xdeclarationWrapper.Declaration;
				return xdeclarationWrapper;
			}
			return base.AppendChild(newChild);
		}
	}
}
