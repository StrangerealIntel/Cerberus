using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ProstoLoader.Properties
{
	// Token: 0x0200002B RID: 43
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x00003EF1 File Offset: 0x000020F1
		static object smethod_0(SettingsBase settingsBase_0, string string_0)
		{
			return settingsBase_0[string_0];
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00003EFA File Offset: 0x000020FA
		static void smethod_1(SettingsBase settingsBase_0, string string_0, object object_0)
		{
			settingsBase_0[string_0] = object_0;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00003F04 File Offset: 0x00002104
		static SettingsBase smethod_2(SettingsBase settingsBase_0)
		{
			return SettingsBase.Synchronized(settingsBase_0);
		}
	}
}
