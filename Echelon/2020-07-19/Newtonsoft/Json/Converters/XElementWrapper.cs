using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200021B RID: 539
	[NullableContext(1)]
	[Nullable(0)]
	internal class XElementWrapper : XContainerWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x0006FCB8 File Offset: 0x0006DEB8
		private XElement Element
		{
			get
			{
				return (XElement)base.WrappedNode;
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0006FCC8 File Offset: 0x0006DEC8
		public XElementWrapper(XElement element) : base(element)
		{
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0006FCD4 File Offset: 0x0006DED4
		public void SetAttributeNode(IXmlNode attribute)
		{
			XObjectWrapper xobjectWrapper = (XObjectWrapper)attribute;
			this.Element.Add(xobjectWrapper.WrappedNode);
			this._attributes = null;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x0006FD04 File Offset: 0x0006DF04
		public override List<IXmlNode> Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					if (!this.Element.HasAttributes && !this.HasImplicitNamespaceAttribute(this.NamespaceUri))
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>();
						foreach (XAttribute attribute in this.Element.Attributes())
						{
							this._attributes.Add(new XAttributeWrapper(attribute));
						}
						string namespaceUri = this.NamespaceUri;
						if (this.HasImplicitNamespaceAttribute(namespaceUri))
						{
							this._attributes.Insert(0, new XAttributeWrapper(new XAttribute("xmlns", namespaceUri)));
						}
					}
				}
				return this._attributes;
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0006FDEC File Offset: 0x0006DFEC
		private bool HasImplicitNamespaceAttribute(string namespaceUri)
		{
			if (!StringUtils.IsNullOrEmpty(namespaceUri))
			{
				IXmlNode parentNode = this.ParentNode;
				if (namespaceUri != ((parentNode != null) ? parentNode.NamespaceUri : null) && StringUtils.IsNullOrEmpty(this.GetPrefixOfNamespace(namespaceUri)))
				{
					bool flag = false;
					if (this.Element.HasAttributes)
					{
						foreach (XAttribute xattribute in this.Element.Attributes())
						{
							if (xattribute.Name.LocalName == "xmlns" && StringUtils.IsNullOrEmpty(xattribute.Name.NamespaceName) && xattribute.Value == namespaceUri)
							{
								flag = true;
							}
						}
					}
					if (!flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0006FEDC File Offset: 0x0006E0DC
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			IXmlNode result = base.AppendChild(newChild);
			this._attributes = null;
			return result;
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x0006FEEC File Offset: 0x0006E0EC
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x0006FEFC File Offset: 0x0006E0FC
		[Nullable(2)]
		public override string Value
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Value;
			}
			[NullableContext(2)]
			set
			{
				this.Element.Value = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x0006FF0C File Offset: 0x0006E10C
		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Name.LocalName;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x0006FF20 File Offset: 0x0006E120
		[Nullable(2)]
		public override string NamespaceUri
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Name.NamespaceName;
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0006FF34 File Offset: 0x0006E134
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this.Element.GetPrefixOfNamespace(namespaceUri);
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0006FF48 File Offset: 0x0006E148
		public bool IsEmpty
		{
			get
			{
				return this.Element.IsEmpty;
			}
		}

		// Token: 0x04000964 RID: 2404
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<IXmlNode> _attributes;
	}
}
