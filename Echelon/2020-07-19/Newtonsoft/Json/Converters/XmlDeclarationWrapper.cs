using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200020A RID: 522
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlDeclarationWrapper : XmlNodeWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x060014F8 RID: 5368 RVA: 0x0006F288 File Offset: 0x0006D488
		public XmlDeclarationWrapper(XmlDeclaration declaration) : base(declaration)
		{
			this._declaration = declaration;
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x0006F298 File Offset: 0x0006D498
		public string Version
		{
			get
			{
				return this._declaration.Version;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0006F2A8 File Offset: 0x0006D4A8
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x0006F2B8 File Offset: 0x0006D4B8
		public string Encoding
		{
			get
			{
				return this._declaration.Encoding;
			}
			set
			{
				this._declaration.Encoding = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0006F2C8 File Offset: 0x0006D4C8
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x0006F2D8 File Offset: 0x0006D4D8
		public string Standalone
		{
			get
			{
				return this._declaration.Standalone;
			}
			set
			{
				this._declaration.Standalone = value;
			}
		}

		// Token: 0x0400095B RID: 2395
		private readonly XmlDeclaration _declaration;
	}
}
