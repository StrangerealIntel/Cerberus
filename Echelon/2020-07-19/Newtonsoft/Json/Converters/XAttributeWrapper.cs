using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200021A RID: 538
	[NullableContext(2)]
	[Nullable(0)]
	internal class XAttributeWrapper : XObjectWrapper
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x0006FC30 File Offset: 0x0006DE30
		[Nullable(1)]
		private XAttribute Attribute
		{
			[NullableContext(1)]
			get
			{
				return (XAttribute)base.WrappedNode;
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0006FC40 File Offset: 0x0006DE40
		[NullableContext(1)]
		public XAttributeWrapper(XAttribute attribute) : base(attribute)
		{
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x0006FC4C File Offset: 0x0006DE4C
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x0006FC5C File Offset: 0x0006DE5C
		public override string Value
		{
			get
			{
				return this.Attribute.Value;
			}
			set
			{
				this.Attribute.Value = value;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0006FC6C File Offset: 0x0006DE6C
		public override string LocalName
		{
			get
			{
				return this.Attribute.Name.LocalName;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x0006FC80 File Offset: 0x0006DE80
		public override string NamespaceUri
		{
			get
			{
				return this.Attribute.Name.NamespaceName;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x0006FC94 File Offset: 0x0006DE94
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Attribute.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Attribute.Parent);
			}
		}
	}
}
