using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Echelon.Properties
{
	// Token: 0x0200003F RID: 63
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600017D RID: 381 RVA: 0x0000BE50 File Offset: 0x0000A050
		internal Resources()
		{
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000BE58 File Offset: 0x0000A058
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Echelon.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000BE88 File Offset: 0x0000A088
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000BE90 File Offset: 0x0000A090
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000BE98 File Offset: 0x0000A098
		internal static string Domains
		{
			get
			{
				return Resources.ResourceManager.GetString("Domains", Resources.resourceCulture);
			}
		}

		// Token: 0x04000085 RID: 133
		private static ResourceManager resourceMan;

		// Token: 0x04000086 RID: 134
		private static CultureInfo resourceCulture;
	}
}
