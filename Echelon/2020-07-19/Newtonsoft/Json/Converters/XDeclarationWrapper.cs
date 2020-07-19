using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000212 RID: 530
	[NullableContext(1)]
	[Nullable(0)]
	internal class XDeclarationWrapper : XObjectWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0006F65C File Offset: 0x0006D85C
		internal XDeclaration Declaration { get; }

		// Token: 0x06001536 RID: 5430 RVA: 0x0006F664 File Offset: 0x0006D864
		public XDeclarationWrapper(XDeclaration declaration) : base(null)
		{
			this.Declaration = declaration;
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0006F674 File Offset: 0x0006D874
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0006F678 File Offset: 0x0006D878
		public string Version
		{
			get
			{
				return this.Declaration.Version;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0006F688 File Offset: 0x0006D888
		// (set) Token: 0x0600153A RID: 5434 RVA: 0x0006F698 File Offset: 0x0006D898
		public string Encoding
		{
			get
			{
				return this.Declaration.Encoding;
			}
			set
			{
				this.Declaration.Encoding = value;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x0006F6A8 File Offset: 0x0006D8A8
		// (set) Token: 0x0600153C RID: 5436 RVA: 0x0006F6B8 File Offset: 0x0006D8B8
		public string Standalone
		{
			get
			{
				return this.Declaration.Standalone;
			}
			set
			{
				this.Declaration.Standalone = value;
			}
		}
	}
}
