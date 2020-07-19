using System;

namespace Ionic.Zip
{
	// Token: 0x020000A2 RID: 162
	internal static class ZipConstants
	{
		// Token: 0x040001B6 RID: 438
		public const uint PackedToRemovableMedia = 808471376u;

		// Token: 0x040001B7 RID: 439
		public const uint Zip64EndOfCentralDirectoryRecordSignature = 101075792u;

		// Token: 0x040001B8 RID: 440
		public const uint Zip64EndOfCentralDirectoryLocatorSignature = 117853008u;

		// Token: 0x040001B9 RID: 441
		public const uint EndOfCentralDirectorySignature = 101010256u;

		// Token: 0x040001BA RID: 442
		public const int ZipEntrySignature = 67324752;

		// Token: 0x040001BB RID: 443
		public const int ZipEntryDataDescriptorSignature = 134695760;

		// Token: 0x040001BC RID: 444
		public const int SplitArchiveSignature = 134695760;

		// Token: 0x040001BD RID: 445
		public const int ZipDirEntrySignature = 33639248;

		// Token: 0x040001BE RID: 446
		public const int AesKeySize = 192;

		// Token: 0x040001BF RID: 447
		public const int AesBlockSize = 128;

		// Token: 0x040001C0 RID: 448
		public const ushort AesAlgId128 = 26126;

		// Token: 0x040001C1 RID: 449
		public const ushort AesAlgId192 = 26127;

		// Token: 0x040001C2 RID: 450
		public const ushort AesAlgId256 = 26128;
	}
}
