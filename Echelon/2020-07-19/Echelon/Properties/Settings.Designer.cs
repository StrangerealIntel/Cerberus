using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Echelon.Properties
{
	// Token: 0x02000040 RID: 64
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.5.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000087 RID: 135
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
