using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200020C RID: 524
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlNodeWrapper : IXmlNode
	{
		// Token: 0x06001504 RID: 5380 RVA: 0x0006F340 File Offset: 0x0006D540
		[NullableContext(1)]
		public XmlNodeWrapper(XmlNode node)
		{
			this._node = node;
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x0006F350 File Offset: 0x0006D550
		public object WrappedNode
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0006F358 File Offset: 0x0006D558
		public XmlNodeType NodeType
		{
			get
			{
				return this._node.NodeType;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0006F368 File Offset: 0x0006D568
		public virtual string LocalName
		{
			get
			{
				return this._node.LocalName;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x0006F378 File Offset: 0x0006D578
		[Nullable(1)]
		public List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				if (this._childNodes == null)
				{
					if (!this._node.HasChildNodes)
					{
						this._childNodes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._childNodes = new List<IXmlNode>(this._node.ChildNodes.Count);
						foreach (object obj in this._node.ChildNodes)
						{
							XmlNode node = (XmlNode)obj;
							this._childNodes.Add(XmlNodeWrapper.WrapNode(node));
						}
					}
				}
				return this._childNodes;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0006F438 File Offset: 0x0006D638
		protected virtual bool HasChildNodes
		{
			get
			{
				return this._node.HasChildNodes;
			}
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0006F448 File Offset: 0x0006D648
		[NullableContext(1)]
		internal static IXmlNode WrapNode(XmlNode node)
		{
			XmlNodeType nodeType = node.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				return new XmlElementWrapper((XmlElement)node);
			}
			if (nodeType == XmlNodeType.DocumentType)
			{
				return new XmlDocumentTypeWrapper((XmlDocumentType)node);
			}
			if (nodeType != XmlNodeType.XmlDeclaration)
			{
				return new XmlNodeWrapper(node);
			}
			return new XmlDeclarationWrapper((XmlDeclaration)node);
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0006F4A8 File Offset: 0x0006D6A8
		[Nullable(1)]
		public List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				if (this._attributes == null)
				{
					if (!this.HasAttributes)
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>(this._node.Attributes.Count);
						foreach (object obj in this._node.Attributes)
						{
							XmlAttribute node = (XmlAttribute)obj;
							this._attributes.Add(XmlNodeWrapper.WrapNode(node));
						}
					}
				}
				return this._attributes;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0006F560 File Offset: 0x0006D760
		private bool HasAttributes
		{
			get
			{
				XmlElement xmlElement = this._node as XmlElement;
				if (xmlElement != null)
				{
					return xmlElement.HasAttributes;
				}
				XmlAttributeCollection attributes = this._node.Attributes;
				return attributes != null && attributes.Count > 0;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x0006F5A8 File Offset: 0x0006D7A8
		public IXmlNode ParentNode
		{
			get
			{
				XmlAttribute xmlAttribute = this._node as XmlAttribute;
				XmlNode xmlNode = (xmlAttribute != null) ? xmlAttribute.OwnerElement : this._node.ParentNode;
				if (xmlNode == null)
				{
					return null;
				}
				return XmlNodeWrapper.WrapNode(xmlNode);
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x0006F5F0 File Offset: 0x0006D7F0
		// (set) Token: 0x0600150F RID: 5391 RVA: 0x0006F600 File Offset: 0x0006D800
		public string Value
		{
			get
			{
				return this._node.Value;
			}
			set
			{
				this._node.Value = value;
			}
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0006F610 File Offset: 0x0006D810
		[NullableContext(1)]
		public IXmlNode AppendChild(IXmlNode newChild)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)newChild;
			this._node.AppendChild(xmlNodeWrapper._node);
			this._childNodes = null;
			this._attributes = null;
			return newChild;
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x0006F64C File Offset: 0x0006D84C
		public string NamespaceUri
		{
			get
			{
				return this._node.NamespaceURI;
			}
		}

		// Token: 0x0400095D RID: 2397
		[Nullable(1)]
		private readonly XmlNode _node;

		// Token: 0x0400095E RID: 2398
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<IXmlNode> _childNodes;

		// Token: 0x0400095F RID: 2399
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<IXmlNode> _attributes;
	}
}
