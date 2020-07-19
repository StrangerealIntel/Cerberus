using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000218 RID: 536
	[NullableContext(1)]
	[Nullable(0)]
	internal class XContainerWrapper : XObjectWrapper
	{
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0006FA04 File Offset: 0x0006DC04
		private XContainer Container
		{
			get
			{
				return (XContainer)base.WrappedNode;
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0006FA14 File Offset: 0x0006DC14
		public XContainerWrapper(XContainer container) : base(container)
		{
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x0006FA20 File Offset: 0x0006DC20
		public override List<IXmlNode> ChildNodes
		{
			get
			{
				if (this._childNodes == null)
				{
					if (!this.HasChildNodes)
					{
						this._childNodes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._childNodes = new List<IXmlNode>();
						foreach (XNode node in this.Container.Nodes())
						{
							this._childNodes.Add(XContainerWrapper.WrapNode(node));
						}
					}
				}
				return this._childNodes;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x0006FABC File Offset: 0x0006DCBC
		protected virtual bool HasChildNodes
		{
			get
			{
				return this.Container.LastNode != null;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0006FACC File Offset: 0x0006DCCC
		[Nullable(2)]
		public override IXmlNode ParentNode
		{
			[NullableContext(2)]
			get
			{
				if (this.Container.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Container.Parent);
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0006FAF0 File Offset: 0x0006DCF0
		internal static IXmlNode WrapNode(XObject node)
		{
			XDocument xdocument = node as XDocument;
			if (xdocument != null)
			{
				return new XDocumentWrapper(xdocument);
			}
			XElement xelement = node as XElement;
			if (xelement != null)
			{
				return new XElementWrapper(xelement);
			}
			XContainer xcontainer = node as XContainer;
			if (xcontainer != null)
			{
				return new XContainerWrapper(xcontainer);
			}
			XProcessingInstruction xprocessingInstruction = node as XProcessingInstruction;
			if (xprocessingInstruction != null)
			{
				return new XProcessingInstructionWrapper(xprocessingInstruction);
			}
			XText xtext = node as XText;
			if (xtext != null)
			{
				return new XTextWrapper(xtext);
			}
			XComment xcomment = node as XComment;
			if (xcomment != null)
			{
				return new XCommentWrapper(xcomment);
			}
			XAttribute xattribute = node as XAttribute;
			if (xattribute != null)
			{
				return new XAttributeWrapper(xattribute);
			}
			XDocumentType xdocumentType = node as XDocumentType;
			if (xdocumentType != null)
			{
				return new XDocumentTypeWrapper(xdocumentType);
			}
			return new XObjectWrapper(node);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0006FBB4 File Offset: 0x0006DDB4
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			this.Container.Add(newChild.WrappedNode);
			this._childNodes = null;
			return newChild;
		}

		// Token: 0x04000962 RID: 2402
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<IXmlNode> _childNodes;
	}
}
