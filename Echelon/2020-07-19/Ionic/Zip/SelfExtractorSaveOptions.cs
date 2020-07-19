using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x020000B2 RID: 178
	[ComVisible(true)]
	public class SelfExtractorSaveOptions
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00025698 File Offset: 0x00023898
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x000256A0 File Offset: 0x000238A0
		public SelfExtractorFlavor Flavor { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x000256AC File Offset: 0x000238AC
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x000256B4 File Offset: 0x000238B4
		public string PostExtractCommandLine { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x000256C0 File Offset: 0x000238C0
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x000256C8 File Offset: 0x000238C8
		public string DefaultExtractDirectory { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x000256D4 File Offset: 0x000238D4
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x000256DC File Offset: 0x000238DC
		public string IconFile { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x000256E8 File Offset: 0x000238E8
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x000256F0 File Offset: 0x000238F0
		public bool Quiet { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x000256FC File Offset: 0x000238FC
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x00025704 File Offset: 0x00023904
		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00025710 File Offset: 0x00023910
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x00025718 File Offset: 0x00023918
		public bool RemoveUnpackedFilesAfterExecute { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00025724 File Offset: 0x00023924
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x0002572C File Offset: 0x0002392C
		public Version FileVersion { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00025738 File Offset: 0x00023938
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x00025740 File Offset: 0x00023940
		public string ProductVersion { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0002574C File Offset: 0x0002394C
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x00025754 File Offset: 0x00023954
		public string Copyright { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00025760 File Offset: 0x00023960
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x00025768 File Offset: 0x00023968
		public string Description { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00025774 File Offset: 0x00023974
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0002577C File Offset: 0x0002397C
		public string ProductName { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00025788 File Offset: 0x00023988
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x00025790 File Offset: 0x00023990
		public string SfxExeWindowTitle { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0002579C File Offset: 0x0002399C
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x000257A4 File Offset: 0x000239A4
		public string AdditionalCompilerSwitches { get; set; }
	}
}
