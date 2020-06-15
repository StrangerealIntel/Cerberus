using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace rec.My.Resources
{
	// Token: 0x020000AB RID: 171
	[StandardModule]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[CompilerGenerated]
	[HideModuleName]
	internal sealed class Resources
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00014A48 File Offset: 0x00012C48
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = object.ReferenceEquals(Resources.resourceMan, null);
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("rec.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00014A98 File Offset: 0x00012C98
		// (set) Token: 0x0600038D RID: 909 RVA: 0x00014AB4 File Offset: 0x00012CB4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x040002BF RID: 703
		private static ResourceManager resourceMan;

		// Token: 0x040002C0 RID: 704
		private static CultureInfo resourceCulture;
	}
}
