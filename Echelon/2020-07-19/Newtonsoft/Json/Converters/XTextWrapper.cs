using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000215 RID: 533
	[NullableContext(2)]
	[Nullable(0)]
	internal class XTextWrapper : XObjectWrapper
	{
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x0006F8F8 File Offset: 0x0006DAF8
		[Nullable(1)]
		private XText Text
		{
			[NullableContext(1)]
			get
			{
				return (XText)base.WrappedNode;
			}
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0006F908 File Offset: 0x0006DB08
		[NullableContext(1)]
		public XTextWrapper(XText text) : base(text)
		{
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0006F914 File Offset: 0x0006DB14
		// (set) Token: 0x06001558 RID: 5464 RVA: 0x0006F924 File Offset: 0x0006DB24
		public override string Value
		{
			get
			{
				return this.Text.Value;
			}
			set
			{
				this.Text.Value = value;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0006F934 File Offset: 0x0006DB34
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Text.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Text.Parent);
			}
		}
	}
}
