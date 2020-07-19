using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000217 RID: 535
	[NullableContext(2)]
	[Nullable(0)]
	internal class XProcessingInstructionWrapper : XObjectWrapper
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x0006F9B8 File Offset: 0x0006DBB8
		[Nullable(1)]
		private XProcessingInstruction ProcessingInstruction
		{
			[NullableContext(1)]
			get
			{
				return (XProcessingInstruction)base.WrappedNode;
			}
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0006F9C8 File Offset: 0x0006DBC8
		[NullableContext(1)]
		public XProcessingInstructionWrapper(XProcessingInstruction processingInstruction) : base(processingInstruction)
		{
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0006F9D4 File Offset: 0x0006DBD4
		public override string LocalName
		{
			get
			{
				return this.ProcessingInstruction.Target;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0006F9E4 File Offset: 0x0006DBE4
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x0006F9F4 File Offset: 0x0006DBF4
		public override string Value
		{
			get
			{
				return this.ProcessingInstruction.Data;
			}
			set
			{
				this.ProcessingInstruction.Data = value;
			}
		}
	}
}
