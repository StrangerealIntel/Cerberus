using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000216 RID: 534
	[NullableContext(2)]
	[Nullable(0)]
	internal class XCommentWrapper : XObjectWrapper
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0006F958 File Offset: 0x0006DB58
		[Nullable(1)]
		private XComment Text
		{
			[NullableContext(1)]
			get
			{
				return (XComment)base.WrappedNode;
			}
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0006F968 File Offset: 0x0006DB68
		[NullableContext(1)]
		public XCommentWrapper(XComment text) : base(text)
		{
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0006F974 File Offset: 0x0006DB74
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x0006F984 File Offset: 0x0006DB84
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

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0006F994 File Offset: 0x0006DB94
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
