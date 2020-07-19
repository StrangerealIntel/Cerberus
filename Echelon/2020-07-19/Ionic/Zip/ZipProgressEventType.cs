using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000083 RID: 131
	[ComVisible(true)]
	public enum ZipProgressEventType
	{
		// Token: 0x04000144 RID: 324
		Adding_Started,
		// Token: 0x04000145 RID: 325
		Adding_AfterAddEntry,
		// Token: 0x04000146 RID: 326
		Adding_Completed,
		// Token: 0x04000147 RID: 327
		Reading_Started,
		// Token: 0x04000148 RID: 328
		Reading_BeforeReadEntry,
		// Token: 0x04000149 RID: 329
		Reading_AfterReadEntry,
		// Token: 0x0400014A RID: 330
		Reading_Completed,
		// Token: 0x0400014B RID: 331
		Reading_ArchiveBytesRead,
		// Token: 0x0400014C RID: 332
		Saving_Started,
		// Token: 0x0400014D RID: 333
		Saving_BeforeWriteEntry,
		// Token: 0x0400014E RID: 334
		Saving_AfterWriteEntry,
		// Token: 0x0400014F RID: 335
		Saving_Completed,
		// Token: 0x04000150 RID: 336
		Saving_AfterSaveTempArchive,
		// Token: 0x04000151 RID: 337
		Saving_BeforeRenameTempArchive,
		// Token: 0x04000152 RID: 338
		Saving_AfterRenameTempArchive,
		// Token: 0x04000153 RID: 339
		Saving_AfterCompileSelfExtractor,
		// Token: 0x04000154 RID: 340
		Saving_EntryBytesRead,
		// Token: 0x04000155 RID: 341
		Extracting_BeforeExtractEntry,
		// Token: 0x04000156 RID: 342
		Extracting_AfterExtractEntry,
		// Token: 0x04000157 RID: 343
		Extracting_ExtractEntryWouldOverwrite,
		// Token: 0x04000158 RID: 344
		Extracting_EntryBytesWritten,
		// Token: 0x04000159 RID: 345
		Extracting_BeforeExtractAll,
		// Token: 0x0400015A RID: 346
		Extracting_AfterExtractAll,
		// Token: 0x0400015B RID: 347
		Error_Saving
	}
}
