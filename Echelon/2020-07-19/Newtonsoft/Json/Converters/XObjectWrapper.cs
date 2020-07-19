using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000219 RID: 537
	[NullableContext(2)]
	[Nullable(0)]
	internal class XObjectWrapper : IXmlNode
	{
		// Token: 0x0600156B RID: 5483 RVA: 0x0006FBD0 File Offset: 0x0006DDD0
		public XObjectWrapper(XObject xmlObject)
		{
			this._xmlObject = xmlObject;
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x0006FBE0 File Offset: 0x0006DDE0
		public object WrappedNode
		{
			get
			{
				return this._xmlObject;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x0006FBE8 File Offset: 0x0006DDE8
		public virtual XmlNodeType NodeType
		{
			get
			{
				XObject xmlObject = this._xmlObject;
				if (xmlObject == null)
				{
					return XmlNodeType.None;
				}
				return xmlObject.NodeType;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x0006FC00 File Offset: 0x0006DE00
		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x0006FC04 File Offset: 0x0006DE04
		[Nullable(1)]
		public virtual List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x0006FC0C File Offset: 0x0006DE0C
		[Nullable(1)]
		public virtual List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0006FC14 File Offset: 0x0006DE14
		public virtual IXmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0006FC18 File Offset: 0x0006DE18
		// (set) Token: 0x06001573 RID: 5491 RVA: 0x0006FC1C File Offset: 0x0006DE1C
		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0006FC24 File Offset: 0x0006DE24
		[NullableContext(1)]
		public virtual IXmlNode AppendChild(IXmlNode newChild)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0006FC2C File Offset: 0x0006DE2C
		public virtual string NamespaceUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000963 RID: 2403
		private readonly XObject _xmlObject;
	}
}
