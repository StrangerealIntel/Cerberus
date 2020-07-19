using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Ionic.BZip2;
using Ionic.Crc;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x020000A6 RID: 166
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00004")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class ZipEntry
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00019AAC File Offset: 0x00017CAC
		internal bool AttributesIndicateDirectory
		{
			get
			{
				return this._InternalFileAttrs == 0 && (this._ExternalFileAttrs & 16) == 16;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00019AC8 File Offset: 0x00017CC8
		internal void ResetDirEntry()
		{
			this.__FileDataPosition = -1L;
			this._LengthOfHeader = 0;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00019ADC File Offset: 0x00017CDC
		public string Info
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(string.Format("          ZipEntry: {0}\n", this.FileName)).Append(string.Format("   Version Made By: {0}\n", this._VersionMadeBy)).Append(string.Format(" Needed to extract: {0}\n", this.VersionNeeded));
				if (this._IsDirectory)
				{
					stringBuilder.Append("        Entry type: directory\n");
				}
				else
				{
					stringBuilder.Append(string.Format("         File type: {0}\n", this._IsText ? "text" : "binary")).Append(string.Format("       Compression: {0}\n", this.CompressionMethod)).Append(string.Format("        Compressed: 0x{0:X}\n", this.CompressedSize)).Append(string.Format("      Uncompressed: 0x{0:X}\n", this.UncompressedSize)).Append(string.Format("             CRC32: 0x{0:X8}\n", this._Crc32));
				}
				stringBuilder.Append(string.Format("       Disk Number: {0}\n", this._diskNumber));
				if (this._RelativeOffsetOfLocalHeader > (long)((ulong)-1))
				{
					stringBuilder.Append(string.Format("   Relative Offset: 0x{0:X16}\n", this._RelativeOffsetOfLocalHeader));
				}
				else
				{
					stringBuilder.Append(string.Format("   Relative Offset: 0x{0:X8}\n", this._RelativeOffsetOfLocalHeader));
				}
				stringBuilder.Append(string.Format("         Bit Field: 0x{0:X4}\n", this._BitField)).Append(string.Format("        Encrypted?: {0}\n", this._sourceIsEncrypted)).Append(string.Format("          Timeblob: 0x{0:X8}\n", this._TimeBlob)).Append(string.Format("              Time: {0}\n", SharedUtilities.PackedToDateTime(this._TimeBlob)));
				stringBuilder.Append(string.Format("         Is Zip64?: {0}\n", this._InputUsesZip64));
				if (!string.IsNullOrEmpty(this._Comment))
				{
					stringBuilder.Append(string.Format("           Comment: {0}\n", this._Comment));
				}
				stringBuilder.Append("\n");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00019D18 File Offset: 0x00017F18
		internal static ZipEntry ReadDirEntry(ZipFile zf, Dictionary<string, object> previouslySeen)
		{
			Stream readStream = zf.ReadStream;
			Encoding encoding = (zf.AlternateEncodingUsage == ZipOption.Always) ? zf.AlternateEncoding : ZipFile.DefaultEncoding;
			int num = SharedUtilities.ReadSignature(readStream);
			if (ZipEntry.IsNotValidZipDirEntrySig(num))
			{
				readStream.Seek(-4L, SeekOrigin.Current);
				if ((long)num != 101010256L && (long)num != 101075792L && num != 67324752)
				{
					throw new BadReadException(string.Format("  Bad signature (0x{0:X8}) at position 0x{1:X8}", num, readStream.Position));
				}
				return null;
			}
			else
			{
				int num2 = 46;
				byte[] array = new byte[42];
				int num3 = readStream.Read(array, 0, array.Length);
				if (num3 != array.Length)
				{
					return null;
				}
				int num4 = 0;
				ZipEntry zipEntry = new ZipEntry();
				zipEntry.AlternateEncoding = encoding;
				zipEntry._Source = ZipEntrySource.ZipFile;
				zipEntry._container = new ZipContainer(zf);
				zipEntry._VersionMadeBy = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._VersionNeeded = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._BitField = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._CompressionMethod = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._TimeBlob = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				zipEntry._LastModified = SharedUtilities.PackedToDateTime(zipEntry._TimeBlob);
				zipEntry._timestamp |= ZipEntryTimestamp.DOS;
				zipEntry._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				zipEntry._CompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				zipEntry._UncompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				zipEntry._CompressionMethod_FromZipFile = zipEntry._CompressionMethod;
				zipEntry._filenameLength = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._extraFieldLength = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._commentLength = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._diskNumber = (uint)array[num4++] + (uint)array[num4++] * 256u;
				zipEntry._InternalFileAttrs = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._ExternalFileAttrs = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				zipEntry._RelativeOffsetOfLocalHeader = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				zipEntry.IsText = ((zipEntry._InternalFileAttrs & 1) == 1);
				array = new byte[(int)zipEntry._filenameLength];
				num3 = readStream.Read(array, 0, array.Length);
				num2 += num3;
				if ((zipEntry._BitField & 2048) == 2048)
				{
					zipEntry._FileNameInArchive = SharedUtilities.Utf8StringFromBuffer(array);
				}
				else
				{
					zipEntry._FileNameInArchive = SharedUtilities.StringFromBuffer(array, encoding);
				}
				while (previouslySeen.ContainsKey(zipEntry._FileNameInArchive))
				{
					zipEntry._FileNameInArchive = ZipEntry.CopyHelper.AppendCopyToFileName(zipEntry._FileNameInArchive);
					zipEntry._metadataChanged = true;
				}
				if (zipEntry.AttributesIndicateDirectory)
				{
					zipEntry.MarkAsDirectory();
				}
				else if (zipEntry._FileNameInArchive.EndsWith("/"))
				{
					zipEntry.MarkAsDirectory();
				}
				zipEntry._CompressedFileDataSize = zipEntry._CompressedSize;
				if ((zipEntry._BitField & 1) == 1)
				{
					zipEntry._Encryption_FromZipFile = (zipEntry._Encryption = EncryptionAlgorithm.PkzipWeak);
					zipEntry._sourceIsEncrypted = true;
				}
				if (zipEntry._extraFieldLength > 0)
				{
					zipEntry._InputUsesZip64 = (zipEntry._CompressedSize == (long)((ulong)-1) || zipEntry._UncompressedSize == (long)((ulong)-1) || zipEntry._RelativeOffsetOfLocalHeader == (long)((ulong)-1));
					num2 += zipEntry.ProcessExtraField(readStream, zipEntry._extraFieldLength);
					zipEntry._CompressedFileDataSize = zipEntry._CompressedSize;
				}
				if (zipEntry._Encryption == EncryptionAlgorithm.PkzipWeak)
				{
					zipEntry._CompressedFileDataSize -= 12L;
				}
				else if (zipEntry.Encryption == EncryptionAlgorithm.WinZipAes128 || zipEntry.Encryption == EncryptionAlgorithm.WinZipAes256)
				{
					zipEntry._CompressedFileDataSize = zipEntry.CompressedSize - (long)(ZipEntry.GetLengthOfCryptoHeaderBytes(zipEntry.Encryption) + 10);
					zipEntry._LengthOfTrailer = 10;
				}
				if ((zipEntry._BitField & 8) == 8)
				{
					if (zipEntry._InputUsesZip64)
					{
						zipEntry._LengthOfTrailer += 24;
					}
					else
					{
						zipEntry._LengthOfTrailer += 16;
					}
				}
				zipEntry.AlternateEncoding = (((zipEntry._BitField & 2048) == 2048) ? Encoding.UTF8 : encoding);
				zipEntry.AlternateEncodingUsage = ZipOption.Always;
				if (zipEntry._commentLength > 0)
				{
					array = new byte[(int)zipEntry._commentLength];
					num3 = readStream.Read(array, 0, array.Length);
					num2 += num3;
					if ((zipEntry._BitField & 2048) == 2048)
					{
						zipEntry._Comment = SharedUtilities.Utf8StringFromBuffer(array);
					}
					else
					{
						zipEntry._Comment = SharedUtilities.StringFromBuffer(array, encoding);
					}
				}
				return zipEntry;
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001A420 File Offset: 0x00018620
		internal static bool IsNotValidZipDirEntrySig(int signature)
		{
			return signature != 33639248;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001A430 File Offset: 0x00018630
		public ZipEntry()
		{
			this._CompressionMethod = 8;
			this._CompressionLevel = CompressionLevel.Default;
			this._Encryption = EncryptionAlgorithm.None;
			this._Source = ZipEntrySource.None;
			this.AlternateEncoding = Encoding.GetEncoding("IBM437");
			this.AlternateEncodingUsage = ZipOption.Default;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0001A49C File Offset: 0x0001869C
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0001A4AC File Offset: 0x000186AC
		public DateTime LastModified
		{
			get
			{
				return this._LastModified.ToLocalTime();
			}
			set
			{
				this._LastModified = ((value.Kind == DateTimeKind.Unspecified) ? DateTime.SpecifyKind(value, DateTimeKind.Local) : value.ToLocalTime());
				this._Mtime = SharedUtilities.AdjustTime_Reverse(this._LastModified).ToUniversalTime();
				this._metadataChanged = true;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0001A504 File Offset: 0x00018704
		private int BufferSize
		{
			get
			{
				return this._container.BufferSize;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0001A514 File Offset: 0x00018714
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0001A51C File Offset: 0x0001871C
		public DateTime ModifiedTime
		{
			get
			{
				return this._Mtime;
			}
			set
			{
				this.SetEntryTimes(this._Ctime, this._Atime, value);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0001A534 File Offset: 0x00018734
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0001A53C File Offset: 0x0001873C
		public DateTime AccessedTime
		{
			get
			{
				return this._Atime;
			}
			set
			{
				this.SetEntryTimes(this._Ctime, value, this._Mtime);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0001A554 File Offset: 0x00018754
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0001A55C File Offset: 0x0001875C
		public DateTime CreationTime
		{
			get
			{
				return this._Ctime;
			}
			set
			{
				this.SetEntryTimes(value, this._Atime, this._Mtime);
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001A574 File Offset: 0x00018774
		public void SetEntryTimes(DateTime created, DateTime accessed, DateTime modified)
		{
			this._ntfsTimesAreSet = true;
			if (created == ZipEntry._zeroHour && created.Kind == ZipEntry._zeroHour.Kind)
			{
				created = ZipEntry._win32Epoch;
			}
			if (accessed == ZipEntry._zeroHour && accessed.Kind == ZipEntry._zeroHour.Kind)
			{
				accessed = ZipEntry._win32Epoch;
			}
			if (modified == ZipEntry._zeroHour && modified.Kind == ZipEntry._zeroHour.Kind)
			{
				modified = ZipEntry._win32Epoch;
			}
			this._Ctime = created.ToUniversalTime();
			this._Atime = accessed.ToUniversalTime();
			this._Mtime = modified.ToUniversalTime();
			this._LastModified = this._Mtime;
			if (!this._emitUnixTimes && !this._emitNtfsTimes)
			{
				this._emitNtfsTimes = true;
			}
			this._metadataChanged = true;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0001A66C File Offset: 0x0001886C
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0001A674 File Offset: 0x00018874
		public bool EmitTimesInWindowsFormatWhenSaving
		{
			get
			{
				return this._emitNtfsTimes;
			}
			set
			{
				this._emitNtfsTimes = value;
				this._metadataChanged = true;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0001A684 File Offset: 0x00018884
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0001A68C File Offset: 0x0001888C
		public bool EmitTimesInUnixFormatWhenSaving
		{
			get
			{
				return this._emitUnixTimes;
			}
			set
			{
				this._emitUnixTimes = value;
				this._metadataChanged = true;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0001A69C File Offset: 0x0001889C
		public ZipEntryTimestamp Timestamp
		{
			get
			{
				return this._timestamp;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0001A6A4 File Offset: 0x000188A4
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0001A6AC File Offset: 0x000188AC
		public FileAttributes Attributes
		{
			get
			{
				return (FileAttributes)this._ExternalFileAttrs;
			}
			set
			{
				this._ExternalFileAttrs = (int)value;
				this._VersionMadeBy = 45;
				this._metadataChanged = true;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0001A6C4 File Offset: 0x000188C4
		internal string LocalFileName
		{
			get
			{
				return this._LocalFileName;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0001A6CC File Offset: 0x000188CC
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0001A6D4 File Offset: 0x000188D4
		public string FileName
		{
			get
			{
				return this._FileNameInArchive;
			}
			set
			{
				if (this._container.ZipFile == null)
				{
					throw new ZipException("Cannot rename; this is not supported in ZipOutputStream/ZipInputStream.");
				}
				if (string.IsNullOrEmpty(value))
				{
					throw new ZipException("The FileName must be non empty and non-null.");
				}
				string text = ZipEntry.NameInArchive(value, null);
				if (this._FileNameInArchive == text)
				{
					return;
				}
				this._container.ZipFile.RemoveEntry(this);
				this._container.ZipFile.InternalAddEntry(text, this);
				this._FileNameInArchive = text;
				this._container.ZipFile.NotifyEntryChanged();
				this._metadataChanged = true;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0001A774 File Offset: 0x00018974
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0001A77C File Offset: 0x0001897C
		public Stream InputStream
		{
			get
			{
				return this._sourceStream;
			}
			set
			{
				if (this._Source != ZipEntrySource.Stream)
				{
					throw new ZipException("You must not set the input stream for this entry.");
				}
				this._sourceWasJitProvided = true;
				this._sourceStream = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0001A7A4 File Offset: 0x000189A4
		public bool InputStreamWasJitProvided
		{
			get
			{
				return this._sourceWasJitProvided;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0001A7AC File Offset: 0x000189AC
		public ZipEntrySource Source
		{
			get
			{
				return this._Source;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0001A7B4 File Offset: 0x000189B4
		public short VersionNeeded
		{
			get
			{
				return this._VersionNeeded;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0001A7BC File Offset: 0x000189BC
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0001A7C4 File Offset: 0x000189C4
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				this._Comment = value;
				this._metadataChanged = true;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0001A7D4 File Offset: 0x000189D4
		public bool? RequiresZip64
		{
			get
			{
				return this._entryRequiresZip64;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0001A7DC File Offset: 0x000189DC
		public bool? OutputUsedZip64
		{
			get
			{
				return this._OutputUsesZip64;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0001A7E4 File Offset: 0x000189E4
		public short BitField
		{
			get
			{
				return this._BitField;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0001A7EC File Offset: 0x000189EC
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0001A7F4 File Offset: 0x000189F4
		public CompressionMethod CompressionMethod
		{
			get
			{
				return (CompressionMethod)this._CompressionMethod;
			}
			set
			{
				if (value == (CompressionMethod)this._CompressionMethod)
				{
					return;
				}
				if (value != CompressionMethod.None && value != CompressionMethod.Deflate && value != CompressionMethod.BZip2)
				{
					throw new InvalidOperationException("Unsupported compression method.");
				}
				this._CompressionMethod = (short)value;
				if (this._CompressionMethod == 0)
				{
					this._CompressionLevel = CompressionLevel.None;
				}
				else if (this.CompressionLevel == CompressionLevel.None)
				{
					this._CompressionLevel = CompressionLevel.Default;
				}
				if (this._container.ZipFile != null)
				{
					this._container.ZipFile.NotifyEntryChanged();
				}
				this._restreamRequiredOnSave = true;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0001A88C File Offset: 0x00018A8C
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0001A894 File Offset: 0x00018A94
		public CompressionLevel CompressionLevel
		{
			get
			{
				return this._CompressionLevel;
			}
			set
			{
				if (this._CompressionMethod != 8 && this._CompressionMethod != 0)
				{
					return;
				}
				if (value == CompressionLevel.Default && this._CompressionMethod == 8)
				{
					return;
				}
				this._CompressionLevel = value;
				if (value == CompressionLevel.None && this._CompressionMethod == 0)
				{
					return;
				}
				if (this._CompressionLevel == CompressionLevel.None)
				{
					this._CompressionMethod = 0;
				}
				else
				{
					this._CompressionMethod = 8;
				}
				if (this._container.ZipFile != null)
				{
					this._container.ZipFile.NotifyEntryChanged();
				}
				this._restreamRequiredOnSave = true;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0001A930 File Offset: 0x00018B30
		public long CompressedSize
		{
			get
			{
				return this._CompressedSize;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001A938 File Offset: 0x00018B38
		public long UncompressedSize
		{
			get
			{
				return this._UncompressedSize;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0001A940 File Offset: 0x00018B40
		public double CompressionRatio
		{
			get
			{
				if (this.UncompressedSize == 0L)
				{
					return 0.0;
				}
				return 100.0 * (1.0 - 1.0 * (double)this.CompressedSize / (1.0 * (double)this.UncompressedSize));
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001A9A0 File Offset: 0x00018BA0
		public int Crc
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0001A9A8 File Offset: 0x00018BA8
		public bool IsDirectory
		{
			get
			{
				return this._IsDirectory;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0001A9B0 File Offset: 0x00018BB0
		public bool UsesEncryption
		{
			get
			{
				return this._Encryption_FromZipFile != EncryptionAlgorithm.None;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0001A9C0 File Offset: 0x00018BC0
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0001A9C8 File Offset: 0x00018BC8
		public EncryptionAlgorithm Encryption
		{
			get
			{
				return this._Encryption;
			}
			set
			{
				if (value == this._Encryption)
				{
					return;
				}
				if (value == EncryptionAlgorithm.Unsupported)
				{
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				this._Encryption = value;
				this._restreamRequiredOnSave = true;
				if (this._container.ZipFile != null)
				{
					this._container.ZipFile.NotifyEntryChanged();
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001AA84 File Offset: 0x00018C84
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0001AA28 File Offset: 0x00018C28
		public string Password
		{
			private get
			{
				return this._Password;
			}
			set
			{
				this._Password = value;
				if (this._Password == null)
				{
					this._Encryption = EncryptionAlgorithm.None;
					return;
				}
				if (this._Source == ZipEntrySource.ZipFile && !this._sourceIsEncrypted)
				{
					this._restreamRequiredOnSave = true;
				}
				if (this.Encryption == EncryptionAlgorithm.None)
				{
					this._Encryption = EncryptionAlgorithm.PkzipWeak;
				}
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0001AA8C File Offset: 0x00018C8C
		internal bool IsChanged
		{
			get
			{
				return this._restreamRequiredOnSave | this._metadataChanged;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001AA9C File Offset: 0x00018C9C
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0001AAA4 File Offset: 0x00018CA4
		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0001AAB0 File Offset: 0x00018CB0
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0001AAB8 File Offset: 0x00018CB8
		public ZipErrorAction ZipErrorAction { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0001AAC4 File Offset: 0x00018CC4
		public bool IncludedInMostRecentSave
		{
			get
			{
				return !this._skippedDuringSave;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0001AAD0 File Offset: 0x00018CD0
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0001AAD8 File Offset: 0x00018CD8
		public SetCompressionCallback SetCompression { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0001AAE4 File Offset: 0x00018CE4
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0001AB08 File Offset: 0x00018D08
		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete.  It will be removed in a future version of the library. Your applications should  use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				return this.AlternateEncoding == Encoding.GetEncoding("UTF-8") && this.AlternateEncodingUsage == ZipOption.AsNecessary;
			}
			set
			{
				if (value)
				{
					this.AlternateEncoding = Encoding.GetEncoding("UTF-8");
					this.AlternateEncodingUsage = ZipOption.AsNecessary;
					return;
				}
				this.AlternateEncoding = ZipFile.DefaultEncoding;
				this.AlternateEncodingUsage = ZipOption.Default;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0001AB3C File Offset: 0x00018D3C
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0001AB44 File Offset: 0x00018D44
		[Obsolete("This property is obsolete since v1.9.1.6. Use AlternateEncoding and AlternateEncodingUsage instead.", true)]
		public Encoding ProvisionalAlternateEncoding { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0001AB50 File Offset: 0x00018D50
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0001AB58 File Offset: 0x00018D58
		public Encoding AlternateEncoding { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0001AB64 File Offset: 0x00018D64
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0001AB6C File Offset: 0x00018D6C
		public ZipOption AlternateEncodingUsage { get; set; }

		// Token: 0x060003D9 RID: 985 RVA: 0x0001AB78 File Offset: 0x00018D78
		internal static string NameInArchive(string filename, string directoryPathInArchive)
		{
			string pathName;
			if (directoryPathInArchive == null)
			{
				pathName = filename;
			}
			else if (string.IsNullOrEmpty(directoryPathInArchive))
			{
				pathName = Path.GetFileName(filename);
			}
			else
			{
				pathName = Path.Combine(directoryPathInArchive, Path.GetFileName(filename));
			}
			return SharedUtilities.NormalizePathForUseInZipFile(pathName);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001ABC4 File Offset: 0x00018DC4
		internal static ZipEntry CreateFromNothing(string nameInArchive)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.None, null, null);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001ABD0 File Offset: 0x00018DD0
		internal static ZipEntry CreateFromFile(string filename, string nameInArchive)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.FileSystem, filename, null);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001ABDC File Offset: 0x00018DDC
		internal static ZipEntry CreateForStream(string entryName, Stream s)
		{
			return ZipEntry.Create(entryName, ZipEntrySource.Stream, s, null);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001ABE8 File Offset: 0x00018DE8
		internal static ZipEntry CreateForWriter(string entryName, WriteDelegate d)
		{
			return ZipEntry.Create(entryName, ZipEntrySource.WriteDelegate, d, null);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001ABF4 File Offset: 0x00018DF4
		internal static ZipEntry CreateForJitStreamProvider(string nameInArchive, OpenDelegate opener, CloseDelegate closer)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.JitStream, opener, closer);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001AC00 File Offset: 0x00018E00
		internal static ZipEntry CreateForZipOutputStream(string nameInArchive)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.ZipOutputStream, null, null);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001AC0C File Offset: 0x00018E0C
		private static ZipEntry Create(string nameInArchive, ZipEntrySource source, object arg1, object arg2)
		{
			if (string.IsNullOrEmpty(nameInArchive))
			{
				throw new ZipException("The entry name must be non-null and non-empty.");
			}
			ZipEntry zipEntry = new ZipEntry();
			zipEntry._VersionMadeBy = 45;
			zipEntry._Source = source;
			zipEntry._Mtime = (zipEntry._Atime = (zipEntry._Ctime = DateTime.UtcNow));
			if (source == ZipEntrySource.Stream)
			{
				zipEntry._sourceStream = (arg1 as Stream);
			}
			else if (source == ZipEntrySource.WriteDelegate)
			{
				zipEntry._WriteDelegate = (arg1 as WriteDelegate);
			}
			else if (source == ZipEntrySource.JitStream)
			{
				zipEntry._OpenDelegate = (arg1 as OpenDelegate);
				zipEntry._CloseDelegate = (arg2 as CloseDelegate);
			}
			else if (source != ZipEntrySource.ZipOutputStream)
			{
				if (source == ZipEntrySource.None)
				{
					zipEntry._Source = ZipEntrySource.FileSystem;
				}
				else
				{
					string text = arg1 as string;
					if (string.IsNullOrEmpty(text))
					{
						throw new ZipException("The filename must be non-null and non-empty.");
					}
					try
					{
						zipEntry._Mtime = File.GetLastWriteTime(text).ToUniversalTime();
						zipEntry._Ctime = File.GetCreationTime(text).ToUniversalTime();
						zipEntry._Atime = File.GetLastAccessTime(text).ToUniversalTime();
						if (File.Exists(text) || Directory.Exists(text))
						{
							zipEntry._ExternalFileAttrs = (int)File.GetAttributes(text);
						}
						zipEntry._ntfsTimesAreSet = true;
						zipEntry._LocalFileName = Path.GetFullPath(text);
					}
					catch (PathTooLongException innerException)
					{
						string message = string.Format("The path is too long, filename={0}", text);
						throw new ZipException(message, innerException);
					}
				}
			}
			zipEntry._LastModified = zipEntry._Mtime;
			zipEntry._FileNameInArchive = SharedUtilities.NormalizePathForUseInZipFile(nameInArchive);
			return zipEntry;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001ADA8 File Offset: 0x00018FA8
		internal void MarkAsDirectory()
		{
			this._IsDirectory = true;
			if (!this._FileNameInArchive.EndsWith("/"))
			{
				this._FileNameInArchive += "/";
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0001ADDC File Offset: 0x00018FDC
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0001ADE4 File Offset: 0x00018FE4
		public bool IsText
		{
			get
			{
				return this._IsText;
			}
			set
			{
				this._IsText = value;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001ADF0 File Offset: 0x00018FF0
		public override string ToString()
		{
			return string.Format("ZipEntry::{0}", this.FileName);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0001AE04 File Offset: 0x00019004
		internal Stream ArchiveStream
		{
			get
			{
				if (this._archiveStream == null)
				{
					if (this._container.ZipFile != null)
					{
						ZipFile zipFile = this._container.ZipFile;
						zipFile.Reset(false);
						this._archiveStream = zipFile.StreamForDiskNumber(this._diskNumber);
					}
					else
					{
						this._archiveStream = this._container.ZipOutputStream.OutputStream;
					}
				}
				return this._archiveStream;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001AE78 File Offset: 0x00019078
		private void SetFdpLoh()
		{
			long position = this.ArchiveStream.Position;
			try
			{
				this.ArchiveStream.Seek(this._RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
			}
			catch (IOException innerException)
			{
				string message = string.Format("Exception seeking  entry({0}) offset(0x{1:X8}) len(0x{2:X8})", this.FileName, this._RelativeOffsetOfLocalHeader, this.ArchiveStream.Length);
				throw new BadStateException(message, innerException);
			}
			byte[] array = new byte[30];
			this.ArchiveStream.Read(array, 0, array.Length);
			short num = (short)((int)array[26] + (int)array[27] * 256);
			short num2 = (short)((int)array[28] + (int)array[29] * 256);
			this.ArchiveStream.Seek((long)(num + num2), SeekOrigin.Current);
			this._LengthOfHeader = (int)(30 + num2 + num) + ZipEntry.GetLengthOfCryptoHeaderBytes(this._Encryption_FromZipFile);
			this.__FileDataPosition = this._RelativeOffsetOfLocalHeader + (long)this._LengthOfHeader;
			this.ArchiveStream.Seek(position, SeekOrigin.Begin);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001AF7C File Offset: 0x0001917C
		private static int GetKeyStrengthInBits(EncryptionAlgorithm a)
		{
			if (a == EncryptionAlgorithm.WinZipAes256)
			{
				return 256;
			}
			if (a == EncryptionAlgorithm.WinZipAes128)
			{
				return 128;
			}
			return -1;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001AF9C File Offset: 0x0001919C
		internal static int GetLengthOfCryptoHeaderBytes(EncryptionAlgorithm a)
		{
			if (a == EncryptionAlgorithm.None)
			{
				return 0;
			}
			if (a == EncryptionAlgorithm.WinZipAes128 || a == EncryptionAlgorithm.WinZipAes256)
			{
				int keyStrengthInBits = ZipEntry.GetKeyStrengthInBits(a);
				return keyStrengthInBits / 8 / 2 + 2;
			}
			if (a == EncryptionAlgorithm.PkzipWeak)
			{
				return 12;
			}
			throw new ZipException("internal error");
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001AFE8 File Offset: 0x000191E8
		internal long FileDataPosition
		{
			get
			{
				if (this.__FileDataPosition == -1L)
				{
					this.SetFdpLoh();
				}
				return this.__FileDataPosition;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0001B004 File Offset: 0x00019204
		private int LengthOfHeader
		{
			get
			{
				if (this._LengthOfHeader == 0)
				{
					this.SetFdpLoh();
				}
				return this._LengthOfHeader;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001B020 File Offset: 0x00019220
		public void Extract()
		{
			this.InternalExtract(".", null, null);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001B030 File Offset: 0x00019230
		public void Extract(ExtractExistingFileAction extractExistingFile)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(".", null, null);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001B048 File Offset: 0x00019248
		public void Extract(Stream stream)
		{
			this.InternalExtract(null, stream, null);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001B054 File Offset: 0x00019254
		public void Extract(string baseDirectory)
		{
			this.InternalExtract(baseDirectory, null, null);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001B060 File Offset: 0x00019260
		public void Extract(string baseDirectory, ExtractExistingFileAction extractExistingFile)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(baseDirectory, null, null);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001B074 File Offset: 0x00019274
		public void ExtractWithPassword(string password)
		{
			this.InternalExtract(".", null, password);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001B084 File Offset: 0x00019284
		public void ExtractWithPassword(string baseDirectory, string password)
		{
			this.InternalExtract(baseDirectory, null, password);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001B090 File Offset: 0x00019290
		public void ExtractWithPassword(ExtractExistingFileAction extractExistingFile, string password)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(".", null, password);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001B0A8 File Offset: 0x000192A8
		public void ExtractWithPassword(string baseDirectory, ExtractExistingFileAction extractExistingFile, string password)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(baseDirectory, null, password);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001B0BC File Offset: 0x000192BC
		public void ExtractWithPassword(Stream stream, string password)
		{
			this.InternalExtract(null, stream, password);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001B0C8 File Offset: 0x000192C8
		public CrcCalculatorStream OpenReader()
		{
			if (this._container.ZipFile == null)
			{
				throw new InvalidOperationException("Use OpenReader() only with ZipFile.");
			}
			return this.InternalOpenReader(this._Password ?? this._container.Password);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001B104 File Offset: 0x00019304
		public CrcCalculatorStream OpenReader(string password)
		{
			if (this._container.ZipFile == null)
			{
				throw new InvalidOperationException("Use OpenReader() only with ZipFile.");
			}
			return this.InternalOpenReader(password);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001B128 File Offset: 0x00019328
		internal CrcCalculatorStream InternalOpenReader(string password)
		{
			this.ValidateCompression();
			this.ValidateEncryption();
			this.SetupCryptoForExtract(password);
			if (this._Source != ZipEntrySource.ZipFile)
			{
				throw new BadStateException("You must call ZipFile.Save before calling OpenReader");
			}
			long length = (this._CompressionMethod_FromZipFile == 0) ? this._CompressedFileDataSize : this.UncompressedSize;
			Stream archiveStream = this.ArchiveStream;
			this.ArchiveStream.Seek(this.FileDataPosition, SeekOrigin.Begin);
			this._inputDecryptorStream = this.GetExtractDecryptor(archiveStream);
			Stream extractDecompressor = this.GetExtractDecompressor(this._inputDecryptorStream);
			return new CrcCalculatorStream(extractDecompressor, length);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001B1BC File Offset: 0x000193BC
		private void OnExtractProgress(long bytesWritten, long totalBytesToWrite)
		{
			if (this._container.ZipFile != null)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnExtractBlock(this, bytesWritten, totalBytesToWrite);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001B1F8 File Offset: 0x000193F8
		private void OnBeforeExtract(string path)
		{
			if (this._container.ZipFile != null && !this._container.ZipFile._inExtractAll)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnSingleEntryExtract(this, path, true);
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001B248 File Offset: 0x00019448
		private void OnAfterExtract(string path)
		{
			if (this._container.ZipFile != null && !this._container.ZipFile._inExtractAll)
			{
				this._container.ZipFile.OnSingleEntryExtract(this, path, false);
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001B294 File Offset: 0x00019494
		private void OnExtractExisting(string path)
		{
			if (this._container.ZipFile != null)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnExtractExisting(this, path);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001B2C0 File Offset: 0x000194C0
		private static void ReallyDelete(string fileName)
		{
			if ((File.GetAttributes(fileName) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
			{
				File.SetAttributes(fileName, FileAttributes.Normal);
			}
			File.Delete(fileName);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001B2E4 File Offset: 0x000194E4
		private void WriteStatus(string format, params object[] args)
		{
			if (this._container.ZipFile != null && this._container.ZipFile.Verbose)
			{
				this._container.ZipFile.StatusMessageTextWriter.WriteLine(format, args);
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001B334 File Offset: 0x00019534
		private void InternalExtract(string baseDir, Stream outstream, string password)
		{
			if (this._container == null)
			{
				throw new BadStateException("This entry is an orphan");
			}
			if (this._container.ZipFile == null)
			{
				throw new InvalidOperationException("Use Extract() only with ZipFile.");
			}
			this._container.ZipFile.Reset(false);
			if (this._Source != ZipEntrySource.ZipFile)
			{
				throw new BadStateException("You must call ZipFile.Save before calling any Extract method");
			}
			this.OnBeforeExtract(baseDir);
			this._ioOperationCanceled = false;
			string text = null;
			Stream stream = null;
			bool flag = false;
			bool flag2 = false;
			try
			{
				this.ValidateCompression();
				this.ValidateEncryption();
				if (this.ValidateOutput(baseDir, outstream, out text))
				{
					this.WriteStatus("extract dir {0}...", new object[]
					{
						text
					});
					this.OnAfterExtract(baseDir);
				}
				else
				{
					if (text != null && File.Exists(text))
					{
						flag = true;
						int num = this.CheckExtractExistingFile(baseDir, text);
						if (num == 2)
						{
							goto IL_2D6;
						}
						if (num == 1)
						{
							return;
						}
					}
					string text2 = password ?? (this._Password ?? this._container.Password);
					if (this._Encryption_FromZipFile != EncryptionAlgorithm.None)
					{
						if (text2 == null)
						{
							throw new BadPasswordException();
						}
						this.SetupCryptoForExtract(text2);
					}
					if (text != null)
					{
						this.WriteStatus("extract file {0}...", new object[]
						{
							text
						});
						text += ".tmp";
						string directoryName = Path.GetDirectoryName(text);
						if (!Directory.Exists(directoryName))
						{
							Directory.CreateDirectory(directoryName);
						}
						else if (this._container.ZipFile != null)
						{
							flag2 = this._container.ZipFile._inExtractAll;
						}
						stream = new FileStream(text, FileMode.CreateNew);
					}
					else
					{
						this.WriteStatus("extract entry {0} to stream...", new object[]
						{
							this.FileName
						});
						stream = outstream;
					}
					if (!this._ioOperationCanceled)
					{
						int actualCrc = this.ExtractOne(stream);
						if (!this._ioOperationCanceled)
						{
							this.VerifyCrcAfterExtract(actualCrc);
							if (text != null)
							{
								stream.Close();
								stream = null;
								string text3 = text;
								string text4 = null;
								text = text3.Substring(0, text3.Length - 4);
								if (flag)
								{
									text4 = text + ".PendingOverwrite";
									File.Move(text, text4);
								}
								File.Move(text3, text);
								this._SetTimes(text, true);
								if (text4 != null && File.Exists(text4))
								{
									ZipEntry.ReallyDelete(text4);
								}
								if (flag2 && this.FileName.IndexOf('/') != -1)
								{
									string directoryName2 = Path.GetDirectoryName(this.FileName);
									if (this._container.ZipFile[directoryName2] == null)
									{
										this._SetTimes(Path.GetDirectoryName(text), false);
									}
								}
								if (((int)this._VersionMadeBy & 65280) == 2560 || ((int)this._VersionMadeBy & 65280) == 0)
								{
									File.SetAttributes(text, (FileAttributes)this._ExternalFileAttrs);
								}
							}
							this.OnAfterExtract(baseDir);
						}
					}
					IL_2D6:;
				}
			}
			catch (Exception)
			{
				this._ioOperationCanceled = true;
				throw;
			}
			finally
			{
				if (this._ioOperationCanceled && text != null)
				{
					if (stream != null)
					{
						stream.Close();
					}
					if (File.Exists(text) && !flag)
					{
						File.Delete(text);
					}
				}
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001B694 File Offset: 0x00019894
		internal void VerifyCrcAfterExtract(int actualCrc32)
		{
			if (actualCrc32 != this._Crc32 && ((this.Encryption != EncryptionAlgorithm.WinZipAes128 && this.Encryption != EncryptionAlgorithm.WinZipAes256) || this._WinZipAesMethod != 2))
			{
				throw new BadCrcException("CRC error: the file being extracted appears to be corrupted. " + string.Format("Expected 0x{0:X8}, Actual 0x{1:X8}", this._Crc32, actualCrc32));
			}
			if (this.UncompressedSize == 0L)
			{
				return;
			}
			if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				WinZipAesCipherStream winZipAesCipherStream = this._inputDecryptorStream as WinZipAesCipherStream;
				this._aesCrypto_forExtract.CalculatedMac = winZipAesCipherStream.FinalAuthentication;
				this._aesCrypto_forExtract.ReadAndVerifyMac(this.ArchiveStream);
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001B754 File Offset: 0x00019954
		private int CheckExtractExistingFile(string baseDir, string targetFileName)
		{
			int num = 0;
			for (;;)
			{
				switch (this.ExtractExistingFile)
				{
				case ExtractExistingFileAction.OverwriteSilently:
					goto IL_24;
				case ExtractExistingFileAction.DoNotOverwrite:
					goto IL_3D;
				case ExtractExistingFileAction.InvokeExtractProgressEvent:
					if (num > 0)
					{
						goto Block_2;
					}
					this.OnExtractExisting(baseDir);
					if (this._ioOperationCanceled)
					{
						return 2;
					}
					num++;
					continue;
				}
				break;
			}
			goto IL_8E;
			IL_24:
			this.WriteStatus("the file {0} exists; will overwrite it...", new object[]
			{
				targetFileName
			});
			return 0;
			IL_3D:
			this.WriteStatus("the file {0} exists; not extracting entry...", new object[]
			{
				this.FileName
			});
			this.OnAfterExtract(baseDir);
			return 1;
			Block_2:
			throw new ZipException(string.Format("The file {0} already exists.", targetFileName));
			IL_8E:
			throw new ZipException(string.Format("The file {0} already exists.", targetFileName));
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001B80C File Offset: 0x00019A0C
		private void _CheckRead(int nbytes)
		{
			if (nbytes == 0)
			{
				throw new BadReadException(string.Format("bad read of entry {0} from compressed archive.", this.FileName));
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001B82C File Offset: 0x00019A2C
		private int ExtractOne(Stream output)
		{
			int result = 0;
			Stream archiveStream = this.ArchiveStream;
			try
			{
				archiveStream.Seek(this.FileDataPosition, SeekOrigin.Begin);
				byte[] array = new byte[this.BufferSize];
				long num = (this._CompressionMethod_FromZipFile != 0) ? this.UncompressedSize : this._CompressedFileDataSize;
				this._inputDecryptorStream = this.GetExtractDecryptor(archiveStream);
				Stream extractDecompressor = this.GetExtractDecompressor(this._inputDecryptorStream);
				long num2 = 0L;
				using (CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(extractDecompressor))
				{
					while (num > 0L)
					{
						int count = (num > (long)array.Length) ? array.Length : ((int)num);
						int num3 = crcCalculatorStream.Read(array, 0, count);
						this._CheckRead(num3);
						output.Write(array, 0, num3);
						num -= (long)num3;
						num2 += (long)num3;
						this.OnExtractProgress(num2, this.UncompressedSize);
						if (this._ioOperationCanceled)
						{
							break;
						}
					}
					result = crcCalculatorStream.Crc;
				}
			}
			finally
			{
				ZipSegmentedStream zipSegmentedStream = archiveStream as ZipSegmentedStream;
				if (zipSegmentedStream != null)
				{
					zipSegmentedStream.Dispose();
					this._archiveStream = null;
				}
			}
			return result;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001B964 File Offset: 0x00019B64
		internal Stream GetExtractDecompressor(Stream input2)
		{
			short compressionMethod_FromZipFile = this._CompressionMethod_FromZipFile;
			if (compressionMethod_FromZipFile == 0)
			{
				return input2;
			}
			if (compressionMethod_FromZipFile == 8)
			{
				return new DeflateStream(input2, CompressionMode.Decompress, true);
			}
			if (compressionMethod_FromZipFile != 12)
			{
				return null;
			}
			return new BZip2InputStream(input2, true);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001B9AC File Offset: 0x00019BAC
		internal Stream GetExtractDecryptor(Stream input)
		{
			Stream result;
			if (this._Encryption_FromZipFile == EncryptionAlgorithm.PkzipWeak)
			{
				result = new ZipCipherStream(input, this._zipCrypto_forExtract, CryptoMode.Decrypt);
			}
			else if (this._Encryption_FromZipFile == EncryptionAlgorithm.WinZipAes128 || this._Encryption_FromZipFile == EncryptionAlgorithm.WinZipAes256)
			{
				result = new WinZipAesCipherStream(input, this._aesCrypto_forExtract, this._CompressedFileDataSize, CryptoMode.Decrypt);
			}
			else
			{
				result = input;
			}
			return result;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001BA14 File Offset: 0x00019C14
		internal void _SetTimes(string fileOrDirectory, bool isFile)
		{
			try
			{
				if (this._ntfsTimesAreSet)
				{
					if (isFile)
					{
						if (File.Exists(fileOrDirectory))
						{
							File.SetCreationTimeUtc(fileOrDirectory, this._Ctime);
							File.SetLastAccessTimeUtc(fileOrDirectory, this._Atime);
							File.SetLastWriteTimeUtc(fileOrDirectory, this._Mtime);
						}
					}
					else if (Directory.Exists(fileOrDirectory))
					{
						Directory.SetCreationTimeUtc(fileOrDirectory, this._Ctime);
						Directory.SetLastAccessTimeUtc(fileOrDirectory, this._Atime);
						Directory.SetLastWriteTimeUtc(fileOrDirectory, this._Mtime);
					}
				}
				else
				{
					DateTime lastWriteTime = SharedUtilities.AdjustTime_Reverse(this.LastModified);
					if (isFile)
					{
						File.SetLastWriteTime(fileOrDirectory, lastWriteTime);
					}
					else
					{
						Directory.SetLastWriteTime(fileOrDirectory, lastWriteTime);
					}
				}
			}
			catch (IOException ex)
			{
				this.WriteStatus("failed to set time on {0}: {1}", new object[]
				{
					fileOrDirectory,
					ex.Message
				});
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0001BAFC File Offset: 0x00019CFC
		private string UnsupportedAlgorithm
		{
			get
			{
				string empty = string.Empty;
				uint unsupportedAlgorithmId = this._UnsupportedAlgorithmId;
				if (unsupportedAlgorithmId <= 26128u)
				{
					if (unsupportedAlgorithmId <= 26115u)
					{
						if (unsupportedAlgorithmId == 0u)
						{
							return "--";
						}
						switch (unsupportedAlgorithmId)
						{
						case 26113u:
							return "DES";
						case 26114u:
							return "RC2";
						case 26115u:
							return "3DES-168";
						}
					}
					else
					{
						if (unsupportedAlgorithmId == 26121u)
						{
							return "3DES-112";
						}
						switch (unsupportedAlgorithmId)
						{
						case 26126u:
							return "PKWare AES128";
						case 26127u:
							return "PKWare AES192";
						case 26128u:
							return "PKWare AES256";
						}
					}
				}
				else if (unsupportedAlgorithmId <= 26401u)
				{
					if (unsupportedAlgorithmId == 26370u)
					{
						return "RC2";
					}
					switch (unsupportedAlgorithmId)
					{
					case 26400u:
						return "Blowfish";
					case 26401u:
						return "Twofish";
					}
				}
				else
				{
					if (unsupportedAlgorithmId == 26625u)
					{
						return "RC4";
					}
					if (unsupportedAlgorithmId != 65535u)
					{
					}
				}
				return string.Format("Unknown (0x{0:X4})", this._UnsupportedAlgorithmId);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0001BC64 File Offset: 0x00019E64
		private string UnsupportedCompressionMethod
		{
			get
			{
				string empty = string.Empty;
				int compressionMethod = (int)this._CompressionMethod;
				if (compressionMethod <= 14)
				{
					switch (compressionMethod)
					{
					case 0:
						return "Store";
					case 1:
						return "Shrink";
					default:
						switch (compressionMethod)
						{
						case 8:
							return "DEFLATE";
						case 9:
							return "Deflate64";
						case 12:
							return "BZIP2";
						case 14:
							return "LZMA";
						}
						break;
					}
				}
				else
				{
					if (compressionMethod == 19)
					{
						return "LZ77";
					}
					if (compressionMethod == 98)
					{
						return "PPMd";
					}
				}
				return string.Format("Unknown (0x{0:X4})", this._CompressionMethod);
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001BD48 File Offset: 0x00019F48
		internal void ValidateEncryption()
		{
			if (this.Encryption == EncryptionAlgorithm.PkzipWeak || this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256 || this.Encryption == EncryptionAlgorithm.None)
			{
				return;
			}
			if (this._UnsupportedAlgorithmId != 0u)
			{
				throw new ZipException(string.Format("Cannot extract: Entry {0} is encrypted with an algorithm not supported by DotNetZip: {1}", this.FileName, this.UnsupportedAlgorithm));
			}
			throw new ZipException(string.Format("Cannot extract: Entry {0} uses an unsupported encryption algorithm ({1:X2})", this.FileName, (int)this.Encryption));
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001BDD0 File Offset: 0x00019FD0
		private void ValidateCompression()
		{
			if (this._CompressionMethod_FromZipFile != 0 && this._CompressionMethod_FromZipFile != 8 && this._CompressionMethod_FromZipFile != 12)
			{
				throw new ZipException(string.Format("Entry {0} uses an unsupported compression method (0x{1:X2}, {2})", this.FileName, this._CompressionMethod_FromZipFile, this.UnsupportedCompressionMethod));
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001BE2C File Offset: 0x0001A02C
		private void SetupCryptoForExtract(string password)
		{
			if (this._Encryption_FromZipFile == EncryptionAlgorithm.None)
			{
				return;
			}
			if (this._Encryption_FromZipFile != EncryptionAlgorithm.PkzipWeak)
			{
				if (this._Encryption_FromZipFile == EncryptionAlgorithm.WinZipAes128 || this._Encryption_FromZipFile == EncryptionAlgorithm.WinZipAes256)
				{
					if (password == null)
					{
						throw new ZipException("Missing password.");
					}
					if (this._aesCrypto_forExtract != null)
					{
						this._aesCrypto_forExtract.Password = password;
						return;
					}
					int lengthOfCryptoHeaderBytes = ZipEntry.GetLengthOfCryptoHeaderBytes(this._Encryption_FromZipFile);
					this.ArchiveStream.Seek(this.FileDataPosition - (long)lengthOfCryptoHeaderBytes, SeekOrigin.Begin);
					int keyStrengthInBits = ZipEntry.GetKeyStrengthInBits(this._Encryption_FromZipFile);
					this._aesCrypto_forExtract = WinZipAesCrypto.ReadFromStream(password, keyStrengthInBits, this.ArchiveStream);
				}
				return;
			}
			if (password == null)
			{
				throw new ZipException("Missing password.");
			}
			this.ArchiveStream.Seek(this.FileDataPosition - 12L, SeekOrigin.Begin);
			this._zipCrypto_forExtract = ZipCrypto.ForRead(password, this);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001BF10 File Offset: 0x0001A110
		private bool ValidateOutput(string basedir, Stream outstream, out string outFileName)
		{
			if (basedir != null)
			{
				string text = this.FileName.Replace("\\", "/");
				if (text.IndexOf(':') == 1)
				{
					text = text.Substring(2);
				}
				if (text.StartsWith("/"))
				{
					text = text.Substring(1);
				}
				if (this._container.ZipFile.FlattenFoldersOnExtract)
				{
					outFileName = Path.Combine(basedir, (text.IndexOf('/') != -1) ? Path.GetFileName(text) : text);
				}
				else
				{
					outFileName = Path.Combine(basedir, text);
				}
				outFileName = outFileName.Replace("/", "\\");
				if (this.IsDirectory || this.FileName.EndsWith("/"))
				{
					if (!Directory.Exists(outFileName))
					{
						Directory.CreateDirectory(outFileName);
						this._SetTimes(outFileName, false);
					}
					else if (this.ExtractExistingFile == ExtractExistingFileAction.OverwriteSilently)
					{
						this._SetTimes(outFileName, false);
					}
					return true;
				}
				return false;
			}
			else
			{
				if (outstream != null)
				{
					outFileName = null;
					return this.IsDirectory || this.FileName.EndsWith("/");
				}
				throw new ArgumentNullException("outstream");
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001C058 File Offset: 0x0001A258
		private void ReadExtraField()
		{
			this._readExtraDepth++;
			long position = this.ArchiveStream.Position;
			this.ArchiveStream.Seek(this._RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
			byte[] array = new byte[30];
			this.ArchiveStream.Read(array, 0, array.Length);
			int num = 26;
			short num2 = (short)((int)array[num++] + (int)array[num++] * 256);
			short extraFieldLength = (short)((int)array[num++] + (int)array[num++] * 256);
			this.ArchiveStream.Seek((long)num2, SeekOrigin.Current);
			this.ProcessExtraField(this.ArchiveStream, extraFieldLength);
			this.ArchiveStream.Seek(position, SeekOrigin.Begin);
			this._readExtraDepth--;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001C11C File Offset: 0x0001A31C
		private static bool ReadHeader(ZipEntry ze, Encoding defaultEncoding)
		{
			int num = 0;
			ze._RelativeOffsetOfLocalHeader = ze.ArchiveStream.Position;
			int num2 = SharedUtilities.ReadEntrySignature(ze.ArchiveStream);
			num += 4;
			if (ZipEntry.IsNotValidSig(num2))
			{
				ze.ArchiveStream.Seek(-4L, SeekOrigin.Current);
				if (ZipEntry.IsNotValidZipDirEntrySig(num2) && (long)num2 != 101010256L)
				{
					throw new BadReadException(string.Format("  Bad signature (0x{0:X8}) at position  0x{1:X8}", num2, ze.ArchiveStream.Position));
				}
				return false;
			}
			else
			{
				byte[] array = new byte[26];
				int num3 = ze.ArchiveStream.Read(array, 0, array.Length);
				if (num3 != array.Length)
				{
					return false;
				}
				num += num3;
				int num4 = 0;
				ze._VersionNeeded = (short)((int)array[num4++] + (int)array[num4++] * 256);
				ze._BitField = (short)((int)array[num4++] + (int)array[num4++] * 256);
				ze._CompressionMethod_FromZipFile = (ze._CompressionMethod = (short)((int)array[num4++] + (int)array[num4++] * 256));
				ze._TimeBlob = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				ze._LastModified = SharedUtilities.PackedToDateTime(ze._TimeBlob);
				ze._timestamp |= ZipEntryTimestamp.DOS;
				if ((ze._BitField & 1) == 1)
				{
					ze._Encryption_FromZipFile = (ze._Encryption = EncryptionAlgorithm.PkzipWeak);
					ze._sourceIsEncrypted = true;
				}
				ze._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				ze._CompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				ze._UncompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				if ((uint)ze._CompressedSize == 4294967295u || (uint)ze._UncompressedSize == 4294967295u)
				{
					ze._InputUsesZip64 = true;
				}
				short num5 = (short)((int)array[num4++] + (int)array[num4++] * 256);
				short extraFieldLength = (short)((int)array[num4++] + (int)array[num4++] * 256);
				array = new byte[(int)num5];
				num3 = ze.ArchiveStream.Read(array, 0, array.Length);
				num += num3;
				if ((ze._BitField & 2048) == 2048)
				{
					ze.AlternateEncoding = Encoding.UTF8;
					ze.AlternateEncodingUsage = ZipOption.Always;
				}
				ze._FileNameInArchive = ze.AlternateEncoding.GetString(array, 0, array.Length);
				if (ze._FileNameInArchive.EndsWith("/"))
				{
					ze.MarkAsDirectory();
				}
				num += ze.ProcessExtraField(ze.ArchiveStream, extraFieldLength);
				ze._LengthOfTrailer = 0;
				if (!ze._FileNameInArchive.EndsWith("/") && (ze._BitField & 8) == 8)
				{
					long position = ze.ArchiveStream.Position;
					bool flag = true;
					long num6 = 0L;
					int num7 = 0;
					while (flag)
					{
						num7++;
						if (ze._container.ZipFile != null)
						{
							ze._container.ZipFile.OnReadBytes(ze);
						}
						long num8 = SharedUtilities.FindSignature(ze.ArchiveStream, 134695760);
						if (num8 == -1L)
						{
							return false;
						}
						num6 += num8;
						if (ze._InputUsesZip64)
						{
							array = new byte[20];
							num3 = ze.ArchiveStream.Read(array, 0, array.Length);
							if (num3 != 20)
							{
								return false;
							}
							num4 = 0;
							ze._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
							ze._CompressedSize = BitConverter.ToInt64(array, num4);
							num4 += 8;
							ze._UncompressedSize = BitConverter.ToInt64(array, num4);
							num4 += 8;
							ze._LengthOfTrailer += 24;
						}
						else
						{
							array = new byte[12];
							num3 = ze.ArchiveStream.Read(array, 0, array.Length);
							if (num3 != 12)
							{
								return false;
							}
							num4 = 0;
							ze._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
							ze._CompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
							ze._UncompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
							ze._LengthOfTrailer += 16;
						}
						flag = (num6 != ze._CompressedSize);
						if (flag)
						{
							ze.ArchiveStream.Seek(-12L, SeekOrigin.Current);
							num6 += 4L;
						}
					}
					ze.ArchiveStream.Seek(position, SeekOrigin.Begin);
				}
				ze._CompressedFileDataSize = ze._CompressedSize;
				if ((ze._BitField & 1) == 1)
				{
					if (ze.Encryption == EncryptionAlgorithm.WinZipAes128 || ze.Encryption == EncryptionAlgorithm.WinZipAes256)
					{
						int keyStrengthInBits = ZipEntry.GetKeyStrengthInBits(ze._Encryption_FromZipFile);
						ze._aesCrypto_forExtract = WinZipAesCrypto.ReadFromStream(null, keyStrengthInBits, ze.ArchiveStream);
						num += ze._aesCrypto_forExtract.SizeOfEncryptionMetadata - 10;
						ze._CompressedFileDataSize -= (long)ze._aesCrypto_forExtract.SizeOfEncryptionMetadata;
						ze._LengthOfTrailer += 10;
					}
					else
					{
						ze._WeakEncryptionHeader = new byte[12];
						num += ZipEntry.ReadWeakEncryptionHeader(ze._archiveStream, ze._WeakEncryptionHeader);
						ze._CompressedFileDataSize -= 12L;
					}
				}
				ze._LengthOfHeader = num;
				ze._TotalEntrySize = (long)ze._LengthOfHeader + ze._CompressedFileDataSize + (long)ze._LengthOfTrailer;
				return true;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001C850 File Offset: 0x0001AA50
		internal static int ReadWeakEncryptionHeader(Stream s, byte[] buffer)
		{
			int num = s.Read(buffer, 0, 12);
			if (num != 12)
			{
				throw new ZipException(string.Format("Unexpected end of data at position 0x{0:X8}", s.Position));
			}
			return num;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001C890 File Offset: 0x0001AA90
		private static bool IsNotValidSig(int signature)
		{
			return signature != 67324752;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001C8A0 File Offset: 0x0001AAA0
		internal static ZipEntry ReadEntry(ZipContainer zc, bool first)
		{
			ZipFile zipFile = zc.ZipFile;
			Stream readStream = zc.ReadStream;
			Encoding alternateEncoding = zc.AlternateEncoding;
			ZipEntry zipEntry = new ZipEntry();
			zipEntry._Source = ZipEntrySource.ZipFile;
			zipEntry._container = zc;
			zipEntry._archiveStream = readStream;
			if (zipFile != null)
			{
				zipFile.OnReadEntry(true, null);
			}
			if (first)
			{
				ZipEntry.HandlePK00Prefix(readStream);
			}
			if (!ZipEntry.ReadHeader(zipEntry, alternateEncoding))
			{
				return null;
			}
			zipEntry.__FileDataPosition = zipEntry.ArchiveStream.Position;
			readStream.Seek(zipEntry._CompressedFileDataSize + (long)zipEntry._LengthOfTrailer, SeekOrigin.Current);
			ZipEntry.HandleUnexpectedDataDescriptor(zipEntry);
			if (zipFile != null)
			{
				zipFile.OnReadBytes(zipEntry);
				zipFile.OnReadEntry(false, zipEntry);
			}
			return zipEntry;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001C94C File Offset: 0x0001AB4C
		internal static void HandlePK00Prefix(Stream s)
		{
			uint num = (uint)SharedUtilities.ReadInt(s);
			if (num != 808471376u)
			{
				s.Seek(-4L, SeekOrigin.Current);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001C97C File Offset: 0x0001AB7C
		private static void HandleUnexpectedDataDescriptor(ZipEntry entry)
		{
			Stream archiveStream = entry.ArchiveStream;
			uint num = (uint)SharedUtilities.ReadInt(archiveStream);
			if ((ulong)num != (ulong)((long)entry._Crc32))
			{
				archiveStream.Seek(-4L, SeekOrigin.Current);
				return;
			}
			int num2 = SharedUtilities.ReadInt(archiveStream);
			if ((long)num2 != entry._CompressedSize)
			{
				archiveStream.Seek(-8L, SeekOrigin.Current);
				return;
			}
			num2 = SharedUtilities.ReadInt(archiveStream);
			if ((long)num2 == entry._UncompressedSize)
			{
				return;
			}
			archiveStream.Seek(-12L, SeekOrigin.Current);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
		internal static int FindExtraFieldSegment(byte[] extra, int offx, ushort targetHeaderId)
		{
			int num = offx;
			while (num + 3 < extra.Length)
			{
				ushort num2 = (ushort)((int)extra[num++] + (int)extra[num++] * 256);
				if (num2 == targetHeaderId)
				{
					return num - 2;
				}
				short num3 = (short)((int)extra[num++] + (int)extra[num++] * 256);
				num += (int)num3;
			}
			return -1;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001CA58 File Offset: 0x0001AC58
		internal int ProcessExtraField(Stream s, short extraFieldLength)
		{
			int num = 0;
			if (extraFieldLength > 0)
			{
				byte[] array = this._Extra = new byte[(int)extraFieldLength];
				num = s.Read(array, 0, array.Length);
				long posn = s.Position - (long)num;
				int num2 = 0;
				while (num2 + 3 < array.Length)
				{
					int num3 = num2;
					ushort num4 = (ushort)((int)array[num2++] + (int)array[num2++] * 256);
					short num5 = (short)((int)array[num2++] + (int)array[num2++] * 256);
					ushort num6 = num4;
					if (num6 <= 21589)
					{
						if (num6 <= 10)
						{
							if (num6 != 1)
							{
								if (num6 == 10)
								{
									num2 = this.ProcessExtraFieldWindowsTimes(array, num2, num5, posn);
								}
							}
							else
							{
								num2 = this.ProcessExtraFieldZip64(array, num2, num5, posn);
							}
						}
						else if (num6 != 23)
						{
							if (num6 == 21589)
							{
								num2 = this.ProcessExtraFieldUnixTimes(array, num2, num5, posn);
							}
						}
						else
						{
							num2 = this.ProcessExtraFieldPkwareStrongEncryption(array, num2);
						}
					}
					else if (num6 <= 30805)
					{
						if (num6 != 22613)
						{
							if (num6 != 30805)
							{
							}
						}
						else
						{
							num2 = this.ProcessExtraFieldInfoZipTimes(array, num2, num5, posn);
						}
					}
					else if (num6 != 30837)
					{
						if (num6 == 39169)
						{
							num2 = this.ProcessExtraFieldWinZipAes(array, num2, num5, posn);
						}
					}
					num2 = num3 + (int)num5 + 4;
				}
			}
			return num;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001CBEC File Offset: 0x0001ADEC
		private int ProcessExtraFieldPkwareStrongEncryption(byte[] Buffer, int j)
		{
			j += 2;
			this._UnsupportedAlgorithmId = (uint)((ushort)((int)Buffer[j++] + (int)Buffer[j++] * 256));
			this._Encryption_FromZipFile = (this._Encryption = EncryptionAlgorithm.Unsupported);
			return j;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001CC34 File Offset: 0x0001AE34
		private int ProcessExtraFieldWinZipAes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (this._CompressionMethod == 99)
			{
				if ((this._BitField & 1) != 1)
				{
					throw new BadReadException(string.Format("  Inconsistent metadata at position 0x{0:X16}", posn));
				}
				this._sourceIsEncrypted = true;
				if (dataSize != 7)
				{
					throw new BadReadException(string.Format("  Inconsistent size (0x{0:X4}) in WinZip AES field at position 0x{1:X16}", dataSize, posn));
				}
				this._WinZipAesMethod = BitConverter.ToInt16(buffer, j);
				j += 2;
				if (this._WinZipAesMethod != 1 && this._WinZipAesMethod != 2)
				{
					throw new BadReadException(string.Format("  Unexpected vendor version number (0x{0:X4}) for WinZip AES metadata at position 0x{1:X16}", this._WinZipAesMethod, posn));
				}
				short num = BitConverter.ToInt16(buffer, j);
				j += 2;
				if (num != 17729)
				{
					throw new BadReadException(string.Format("  Unexpected vendor ID (0x{0:X4}) for WinZip AES metadata at position 0x{1:X16}", num, posn));
				}
				int num2 = (buffer[j] == 1) ? 128 : ((buffer[j] == 3) ? 256 : -1);
				if (num2 < 0)
				{
					throw new BadReadException(string.Format("Invalid key strength ({0})", num2));
				}
				this._Encryption_FromZipFile = (this._Encryption = ((num2 == 128) ? EncryptionAlgorithm.WinZipAes128 : EncryptionAlgorithm.WinZipAes256));
				j++;
				this._CompressionMethod_FromZipFile = (this._CompressionMethod = BitConverter.ToInt16(buffer, j));
				j += 2;
			}
			return j;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001CDAC File Offset: 0x0001AFAC
		private int ProcessExtraFieldZip64(byte[] buffer, int j, short dataSize, long posn)
		{
			this._InputUsesZip64 = true;
			if (dataSize > 28)
			{
				throw new BadReadException(string.Format("  Inconsistent size (0x{0:X4}) for ZIP64 extra field at position 0x{1:X16}", dataSize, posn));
			}
			int remainingData = (int)dataSize;
			ZipEntry.Func<long> func = delegate()
			{
				if (remainingData < 8)
				{
					throw new BadReadException(string.Format("  Missing data for ZIP64 extra field, position 0x{0:X16}", posn));
				}
				long result = BitConverter.ToInt64(buffer, j);
				j += 8;
				remainingData -= 8;
				return result;
			};
			if (this._UncompressedSize == (long)((ulong)-1))
			{
				this._UncompressedSize = func();
			}
			if (this._CompressedSize == (long)((ulong)-1))
			{
				this._CompressedSize = func();
			}
			if (this._RelativeOffsetOfLocalHeader == (long)((ulong)-1))
			{
				this._RelativeOffsetOfLocalHeader = func();
			}
			return j;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001CE70 File Offset: 0x0001B070
		private int ProcessExtraFieldInfoZipTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (dataSize != 12 && dataSize != 8)
			{
				throw new BadReadException(string.Format("  Unexpected size (0x{0:X4}) for InfoZip v1 extra field at position 0x{1:X16}", dataSize, posn));
			}
			int num = BitConverter.ToInt32(buffer, j);
			this._Mtime = ZipEntry._unixEpoch.AddSeconds((double)num);
			j += 4;
			num = BitConverter.ToInt32(buffer, j);
			this._Atime = ZipEntry._unixEpoch.AddSeconds((double)num);
			j += 4;
			this._Ctime = DateTime.UtcNow;
			this._ntfsTimesAreSet = true;
			this._timestamp |= ZipEntryTimestamp.InfoZip1;
			return j;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001CF0C File Offset: 0x0001B10C
		private int ProcessExtraFieldUnixTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (dataSize != 13 && dataSize != 9 && dataSize != 5)
			{
				throw new BadReadException(string.Format("  Unexpected size (0x{0:X4}) for Extended Timestamp extra field at position 0x{1:X16}", dataSize, posn));
			}
			int remainingData = (int)dataSize;
			ZipEntry.Func<DateTime> func = delegate()
			{
				int num = BitConverter.ToInt32(buffer, j);
				j += 4;
				remainingData -= 4;
				return ZipEntry._unixEpoch.AddSeconds((double)num);
			};
			if (dataSize == 13 || this._readExtraDepth > 0)
			{
				byte b = buffer[j++];
				remainingData--;
				if ((b & 1) != 0 && remainingData >= 4)
				{
					this._Mtime = func();
				}
				this._Atime = (((b & 2) != 0 && remainingData >= 4) ? func() : DateTime.UtcNow);
				this._Ctime = (((b & 4) != 0 && remainingData >= 4) ? func() : DateTime.UtcNow);
				this._timestamp |= ZipEntryTimestamp.Unix;
				this._ntfsTimesAreSet = true;
				this._emitUnixTimes = true;
			}
			else
			{
				this.ReadExtraField();
			}
			return j;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001D05C File Offset: 0x0001B25C
		private int ProcessExtraFieldWindowsTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (dataSize != 32)
			{
				throw new BadReadException(string.Format("  Unexpected size (0x{0:X4}) for NTFS times extra field at position 0x{1:X16}", dataSize, posn));
			}
			j += 4;
			short num = (short)((int)buffer[j] + (int)buffer[j + 1] * 256);
			short num2 = (short)((int)buffer[j + 2] + (int)buffer[j + 3] * 256);
			j += 4;
			if (num == 1 && num2 == 24)
			{
				long fileTime = BitConverter.ToInt64(buffer, j);
				this._Mtime = DateTime.FromFileTimeUtc(fileTime);
				j += 8;
				fileTime = BitConverter.ToInt64(buffer, j);
				this._Atime = DateTime.FromFileTimeUtc(fileTime);
				j += 8;
				fileTime = BitConverter.ToInt64(buffer, j);
				this._Ctime = DateTime.FromFileTimeUtc(fileTime);
				j += 8;
				this._ntfsTimesAreSet = true;
				this._timestamp |= ZipEntryTimestamp.Windows;
				this._emitNtfsTimes = true;
			}
			return j;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001D138 File Offset: 0x0001B338
		internal void WriteCentralDirectoryEntry(Stream s)
		{
			byte[] array = new byte[4096];
			int num = 0;
			array[num++] = 80;
			array[num++] = 75;
			array[num++] = 1;
			array[num++] = 2;
			array[num++] = (byte)(this._VersionMadeBy & 255);
			array[num++] = (byte)(((int)this._VersionMadeBy & 65280) >> 8);
			short num2 = (this.VersionNeeded != 0) ? this.VersionNeeded : 20;
			if (this._OutputUsesZip64 == null)
			{
				this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always);
			}
			short num3 = this._OutputUsesZip64.Value ? 45 : num2;
			if (this.CompressionMethod == CompressionMethod.BZip2)
			{
				num3 = 46;
			}
			array[num++] = (byte)(num3 & 255);
			array[num++] = (byte)(((int)num3 & 65280) >> 8);
			array[num++] = (byte)(this._BitField & 255);
			array[num++] = (byte)(((int)this._BitField & 65280) >> 8);
			array[num++] = (byte)(this._CompressionMethod & 255);
			array[num++] = (byte)(((int)this._CompressionMethod & 65280) >> 8);
			if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				num -= 2;
				array[num++] = 99;
				array[num++] = 0;
			}
			array[num++] = (byte)(this._TimeBlob & 255);
			array[num++] = (byte)((this._TimeBlob & 65280) >> 8);
			array[num++] = (byte)((this._TimeBlob & 16711680) >> 16);
			array[num++] = (byte)(((long)this._TimeBlob & (long)((ulong)-16777216)) >> 24);
			array[num++] = (byte)(this._Crc32 & 255);
			array[num++] = (byte)((this._Crc32 & 65280) >> 8);
			array[num++] = (byte)((this._Crc32 & 16711680) >> 16);
			array[num++] = (byte)(((long)this._Crc32 & (long)((ulong)-16777216)) >> 24);
			if (this._OutputUsesZip64.Value)
			{
				for (int i = 0; i < 8; i++)
				{
					array[num++] = byte.MaxValue;
				}
			}
			else
			{
				array[num++] = (byte)(this._CompressedSize & 255L);
				array[num++] = (byte)((this._CompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._CompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._CompressedSize & (long)((ulong)-16777216)) >> 24);
				array[num++] = (byte)(this._UncompressedSize & 255L);
				array[num++] = (byte)((this._UncompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._UncompressedSize & (long)((ulong)-16777216)) >> 24);
			}
			byte[] encodedFileNameBytes = this.GetEncodedFileNameBytes();
			short num4 = (short)encodedFileNameBytes.Length;
			array[num++] = (byte)(num4 & 255);
			array[num++] = (byte)(((int)num4 & 65280) >> 8);
			this._presumeZip64 = this._OutputUsesZip64.Value;
			this._Extra = this.ConstructExtraField(true);
			short num5 = (short)((this._Extra == null) ? 0 : this._Extra.Length);
			array[num++] = (byte)(num5 & 255);
			array[num++] = (byte)(((int)num5 & 65280) >> 8);
			int num6 = (this._CommentBytes == null) ? 0 : this._CommentBytes.Length;
			if (num6 + num > array.Length)
			{
				num6 = array.Length - num;
			}
			array[num++] = (byte)(num6 & 255);
			array[num++] = (byte)((num6 & 65280) >> 8);
			bool flag = this._container.ZipFile != null && this._container.ZipFile.MaxOutputSegmentSize != 0;
			if (flag)
			{
				array[num++] = (byte)(this._diskNumber & 255u);
				array[num++] = (byte)((this._diskNumber & 65280u) >> 8);
			}
			else
			{
				array[num++] = 0;
				array[num++] = 0;
			}
			array[num++] = (this._IsText ? 1 : 0);
			array[num++] = 0;
			array[num++] = (byte)(this._ExternalFileAttrs & 255);
			array[num++] = (byte)((this._ExternalFileAttrs & 65280) >> 8);
			array[num++] = (byte)((this._ExternalFileAttrs & 16711680) >> 16);
			array[num++] = (byte)(((long)this._ExternalFileAttrs & (long)((ulong)-16777216)) >> 24);
			if (this._RelativeOffsetOfLocalHeader > (long)((ulong)-1))
			{
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
			}
			else
			{
				array[num++] = (byte)(this._RelativeOffsetOfLocalHeader & 255L);
				array[num++] = (byte)((this._RelativeOffsetOfLocalHeader & 65280L) >> 8);
				array[num++] = (byte)((this._RelativeOffsetOfLocalHeader & 16711680L) >> 16);
				array[num++] = (byte)((this._RelativeOffsetOfLocalHeader & (long)((ulong)-16777216)) >> 24);
			}
			Buffer.BlockCopy(encodedFileNameBytes, 0, array, num, (int)num4);
			num += (int)num4;
			if (this._Extra != null)
			{
				byte[] extra = this._Extra;
				int srcOffset = 0;
				Buffer.BlockCopy(extra, srcOffset, array, num, (int)num5);
				num += (int)num5;
			}
			if (num6 != 0)
			{
				Buffer.BlockCopy(this._CommentBytes, 0, array, num, num6);
				num += num6;
			}
			s.Write(array, 0, num);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001D71C File Offset: 0x0001B91C
		private byte[] ConstructExtraField(bool forCentralDirectory)
		{
			List<byte[]> list = new List<byte[]>();
			if (this._container.Zip64 == Zip64Option.Always || (this._container.Zip64 == Zip64Option.AsNecessary && (!forCentralDirectory || this._entryRequiresZip64.Value)))
			{
				int num = 4 + (forCentralDirectory ? 28 : 16);
				byte[] array = new byte[num];
				int num2 = 0;
				if (this._presumeZip64 || forCentralDirectory)
				{
					array[num2++] = 1;
					array[num2++] = 0;
				}
				else
				{
					array[num2++] = 153;
					array[num2++] = 153;
				}
				array[num2++] = (byte)(num - 4);
				array[num2++] = 0;
				Array.Copy(BitConverter.GetBytes(this._UncompressedSize), 0, array, num2, 8);
				num2 += 8;
				Array.Copy(BitConverter.GetBytes(this._CompressedSize), 0, array, num2, 8);
				num2 += 8;
				if (forCentralDirectory)
				{
					Array.Copy(BitConverter.GetBytes(this._RelativeOffsetOfLocalHeader), 0, array, num2, 8);
					num2 += 8;
					Array.Copy(BitConverter.GetBytes(0), 0, array, num2, 4);
				}
				list.Add(array);
			}
			if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				byte[] array = new byte[11];
				int num3 = 0;
				array[num3++] = 1;
				array[num3++] = 153;
				array[num3++] = 7;
				array[num3++] = 0;
				array[num3++] = 1;
				array[num3++] = 0;
				array[num3++] = 65;
				array[num3++] = 69;
				int keyStrengthInBits = ZipEntry.GetKeyStrengthInBits(this.Encryption);
				if (keyStrengthInBits == 128)
				{
					array[num3] = 1;
				}
				else if (keyStrengthInBits == 256)
				{
					array[num3] = 3;
				}
				else
				{
					array[num3] = byte.MaxValue;
				}
				num3++;
				array[num3++] = (byte)(this._CompressionMethod & 255);
				array[num3++] = (byte)((int)this._CompressionMethod & 65280);
				list.Add(array);
			}
			if (this._ntfsTimesAreSet && this._emitNtfsTimes)
			{
				byte[] array = new byte[36];
				int num4 = 0;
				array[num4++] = 10;
				array[num4++] = 0;
				array[num4++] = 32;
				array[num4++] = 0;
				num4 += 4;
				array[num4++] = 1;
				array[num4++] = 0;
				array[num4++] = 24;
				array[num4++] = 0;
				long value = this._Mtime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(value), 0, array, num4, 8);
				num4 += 8;
				value = this._Atime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(value), 0, array, num4, 8);
				num4 += 8;
				value = this._Ctime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(value), 0, array, num4, 8);
				num4 += 8;
				list.Add(array);
			}
			if (this._ntfsTimesAreSet && this._emitUnixTimes)
			{
				int num5 = 9;
				if (!forCentralDirectory)
				{
					num5 += 8;
				}
				byte[] array = new byte[num5];
				int num6 = 0;
				array[num6++] = 85;
				array[num6++] = 84;
				array[num6++] = (byte)(num5 - 4);
				array[num6++] = 0;
				array[num6++] = 7;
				int value2 = (int)(this._Mtime - ZipEntry._unixEpoch).TotalSeconds;
				Array.Copy(BitConverter.GetBytes(value2), 0, array, num6, 4);
				num6 += 4;
				if (!forCentralDirectory)
				{
					value2 = (int)(this._Atime - ZipEntry._unixEpoch).TotalSeconds;
					Array.Copy(BitConverter.GetBytes(value2), 0, array, num6, 4);
					num6 += 4;
					value2 = (int)(this._Ctime - ZipEntry._unixEpoch).TotalSeconds;
					Array.Copy(BitConverter.GetBytes(value2), 0, array, num6, 4);
					num6 += 4;
				}
				list.Add(array);
			}
			byte[] array2 = null;
			if (list.Count > 0)
			{
				int num7 = 0;
				int num8 = 0;
				for (int i = 0; i < list.Count; i++)
				{
					num7 += list[i].Length;
				}
				array2 = new byte[num7];
				for (int i = 0; i < list.Count; i++)
				{
					Array.Copy(list[i], 0, array2, num8, list[i].Length);
					num8 += list[i].Length;
				}
			}
			return array2;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001DBB8 File Offset: 0x0001BDB8
		private string NormalizeFileName()
		{
			string text = this.FileName.Replace("\\", "/");
			string result;
			if (this._TrimVolumeFromFullyQualifiedPaths && this.FileName.Length >= 3 && this.FileName[1] == ':' && text[2] == '/')
			{
				result = text.Substring(3);
			}
			else if (this.FileName.Length >= 4 && text[0] == '/' && text[1] == '/')
			{
				int num = text.IndexOf('/', 2);
				if (num == -1)
				{
					throw new ArgumentException("The path for that entry appears to be badly formatted");
				}
				result = text.Substring(num + 1);
			}
			else if (this.FileName.Length >= 3 && text[0] == '.' && text[1] == '/')
			{
				result = text.Substring(2);
			}
			else
			{
				result = text;
			}
			return result;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001DCC0 File Offset: 0x0001BEC0
		private byte[] GetEncodedFileNameBytes()
		{
			string text = this.NormalizeFileName();
			switch (this.AlternateEncodingUsage)
			{
			case ZipOption.Default:
				if (this._Comment != null && this._Comment.Length != 0)
				{
					this._CommentBytes = ZipEntry.ibm437.GetBytes(this._Comment);
				}
				this._actualEncoding = ZipEntry.ibm437;
				return ZipEntry.ibm437.GetBytes(text);
			case ZipOption.Always:
				if (this._Comment != null && this._Comment.Length != 0)
				{
					this._CommentBytes = this.AlternateEncoding.GetBytes(this._Comment);
				}
				this._actualEncoding = this.AlternateEncoding;
				return this.AlternateEncoding.GetBytes(text);
			}
			byte[] bytes = ZipEntry.ibm437.GetBytes(text);
			string @string = ZipEntry.ibm437.GetString(bytes, 0, bytes.Length);
			this._CommentBytes = null;
			if (@string != text)
			{
				bytes = this.AlternateEncoding.GetBytes(text);
				if (this._Comment != null && this._Comment.Length != 0)
				{
					this._CommentBytes = this.AlternateEncoding.GetBytes(this._Comment);
				}
				this._actualEncoding = this.AlternateEncoding;
				return bytes;
			}
			this._actualEncoding = ZipEntry.ibm437;
			if (this._Comment == null || this._Comment.Length == 0)
			{
				return bytes;
			}
			byte[] bytes2 = ZipEntry.ibm437.GetBytes(this._Comment);
			string string2 = ZipEntry.ibm437.GetString(bytes2, 0, bytes2.Length);
			if (string2 != this.Comment)
			{
				bytes = this.AlternateEncoding.GetBytes(text);
				this._CommentBytes = this.AlternateEncoding.GetBytes(this._Comment);
				this._actualEncoding = this.AlternateEncoding;
				return bytes;
			}
			this._CommentBytes = bytes2;
			return bytes;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001DEA0 File Offset: 0x0001C0A0
		private bool WantReadAgain()
		{
			return this._UncompressedSize >= 16L && this._CompressionMethod != 0 && this.CompressionLevel != CompressionLevel.None && this._CompressedSize >= this._UncompressedSize && (this._Source != ZipEntrySource.Stream || this._sourceStream.CanSeek) && (this._aesCrypto_forWrite == null || this.CompressedSize - (long)this._aesCrypto_forWrite.SizeOfEncryptionMetadata > this.UncompressedSize + 16L) && (this._zipCrypto_forWrite == null || this.CompressedSize - 12L > this.UncompressedSize);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001DF60 File Offset: 0x0001C160
		private void MaybeUnsetCompressionMethodForWriting(int cycle)
		{
			if (cycle > 1)
			{
				this._CompressionMethod = 0;
				return;
			}
			if (this.IsDirectory)
			{
				this._CompressionMethod = 0;
				return;
			}
			if (this._Source == ZipEntrySource.ZipFile)
			{
				return;
			}
			if (this._Source == ZipEntrySource.Stream)
			{
				if (this._sourceStream != null && this._sourceStream.CanSeek)
				{
					long length = this._sourceStream.Length;
					if (length == 0L)
					{
						this._CompressionMethod = 0;
						return;
					}
				}
			}
			else if (this._Source == ZipEntrySource.FileSystem && SharedUtilities.GetFileLength(this.LocalFileName) == 0L)
			{
				this._CompressionMethod = 0;
				return;
			}
			if (this.SetCompression != null)
			{
				this.CompressionLevel = this.SetCompression(this.LocalFileName, this._FileNameInArchive);
			}
			if (this.CompressionLevel == CompressionLevel.None && this.CompressionMethod == CompressionMethod.Deflate)
			{
				this._CompressionMethod = 0;
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001E050 File Offset: 0x0001C250
		internal void WriteHeader(Stream s, int cycle)
		{
			CountingStream countingStream = s as CountingStream;
			this._future_ROLH = ((countingStream != null) ? countingStream.ComputedPosition : s.Position);
			int num = 0;
			byte[] array = new byte[30];
			array[num++] = 80;
			array[num++] = 75;
			array[num++] = 3;
			array[num++] = 4;
			this._presumeZip64 = (this._container.Zip64 == Zip64Option.Always || (this._container.Zip64 == Zip64Option.AsNecessary && !s.CanSeek));
			short num2 = this._presumeZip64 ? 45 : 20;
			if (this.CompressionMethod == CompressionMethod.BZip2)
			{
				num2 = 46;
			}
			array[num++] = (byte)(num2 & 255);
			array[num++] = (byte)(((int)num2 & 65280) >> 8);
			byte[] encodedFileNameBytes = this.GetEncodedFileNameBytes();
			short num3 = (short)encodedFileNameBytes.Length;
			if (this._Encryption == EncryptionAlgorithm.None)
			{
				this._BitField &= -2;
			}
			else
			{
				this._BitField |= 1;
			}
			if (this._actualEncoding.CodePage == Encoding.UTF8.CodePage)
			{
				this._BitField |= 2048;
			}
			if (this.IsDirectory || cycle == 99)
			{
				this._BitField &= -9;
				this._BitField &= -2;
				this.Encryption = EncryptionAlgorithm.None;
				this.Password = null;
			}
			else if (!s.CanSeek)
			{
				this._BitField |= 8;
			}
			array[num++] = (byte)(this._BitField & 255);
			array[num++] = (byte)(((int)this._BitField & 65280) >> 8);
			if (this.__FileDataPosition == -1L)
			{
				this._CompressedSize = 0L;
				this._crcCalculated = false;
			}
			this.MaybeUnsetCompressionMethodForWriting(cycle);
			array[num++] = (byte)(this._CompressionMethod & 255);
			array[num++] = (byte)(((int)this._CompressionMethod & 65280) >> 8);
			if (cycle == 99)
			{
				this.SetZip64Flags();
			}
			else if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				num -= 2;
				array[num++] = 99;
				array[num++] = 0;
			}
			this._TimeBlob = SharedUtilities.DateTimeToPacked(this.LastModified);
			array[num++] = (byte)(this._TimeBlob & 255);
			array[num++] = (byte)((this._TimeBlob & 65280) >> 8);
			array[num++] = (byte)((this._TimeBlob & 16711680) >> 16);
			array[num++] = (byte)(((long)this._TimeBlob & (long)((ulong)-16777216)) >> 24);
			array[num++] = (byte)(this._Crc32 & 255);
			array[num++] = (byte)((this._Crc32 & 65280) >> 8);
			array[num++] = (byte)((this._Crc32 & 16711680) >> 16);
			array[num++] = (byte)(((long)this._Crc32 & (long)((ulong)-16777216)) >> 24);
			if (this._presumeZip64)
			{
				for (int i = 0; i < 8; i++)
				{
					array[num++] = byte.MaxValue;
				}
			}
			else
			{
				array[num++] = (byte)(this._CompressedSize & 255L);
				array[num++] = (byte)((this._CompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._CompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._CompressedSize & (long)((ulong)-16777216)) >> 24);
				array[num++] = (byte)(this._UncompressedSize & 255L);
				array[num++] = (byte)((this._UncompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._UncompressedSize & (long)((ulong)-16777216)) >> 24);
			}
			array[num++] = (byte)(num3 & 255);
			array[num++] = (byte)(((int)num3 & 65280) >> 8);
			this._Extra = this.ConstructExtraField(false);
			short num4 = (short)((this._Extra == null) ? 0 : this._Extra.Length);
			array[num++] = (byte)(num4 & 255);
			array[num++] = (byte)(((int)num4 & 65280) >> 8);
			byte[] array2 = new byte[num + (int)num3 + (int)num4];
			Buffer.BlockCopy(array, 0, array2, 0, num);
			Buffer.BlockCopy(encodedFileNameBytes, 0, array2, num, encodedFileNameBytes.Length);
			num += encodedFileNameBytes.Length;
			if (this._Extra != null)
			{
				Buffer.BlockCopy(this._Extra, 0, array2, num, this._Extra.Length);
				num += this._Extra.Length;
			}
			this._LengthOfHeader = num;
			ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
			if (zipSegmentedStream != null)
			{
				zipSegmentedStream.ContiguousWrite = true;
				uint num5 = zipSegmentedStream.ComputeSegment(num);
				if (num5 != zipSegmentedStream.CurrentSegment)
				{
					this._future_ROLH = 0L;
				}
				else
				{
					this._future_ROLH = zipSegmentedStream.Position;
				}
				this._diskNumber = num5;
			}
			if (this._container.Zip64 == Zip64Option.Default && (uint)this._RelativeOffsetOfLocalHeader >= 4294967295u)
			{
				throw new ZipException("Offset within the zip archive exceeds 0xFFFFFFFF. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
			}
			s.Write(array2, 0, num);
			if (zipSegmentedStream != null)
			{
				zipSegmentedStream.ContiguousWrite = false;
			}
			this._EntryHeader = array2;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001E5D0 File Offset: 0x0001C7D0
		private int FigureCrc32()
		{
			if (!this._crcCalculated)
			{
				Stream stream = null;
				if (this._Source == ZipEntrySource.WriteDelegate)
				{
					CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(Stream.Null);
					this._WriteDelegate(this.FileName, crcCalculatorStream);
					this._Crc32 = crcCalculatorStream.Crc;
				}
				else if (this._Source != ZipEntrySource.ZipFile)
				{
					if (this._Source == ZipEntrySource.Stream)
					{
						this.PrepSourceStream();
						stream = this._sourceStream;
					}
					else if (this._Source == ZipEntrySource.JitStream)
					{
						if (this._sourceStream == null)
						{
							this._sourceStream = this._OpenDelegate(this.FileName);
						}
						this.PrepSourceStream();
						stream = this._sourceStream;
					}
					else if (this._Source != ZipEntrySource.ZipOutputStream)
					{
						stream = File.Open(this.LocalFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					}
					CRC32 crc = new CRC32();
					this._Crc32 = crc.GetCrc32(stream);
					if (this._sourceStream == null)
					{
						stream.Dispose();
					}
				}
				this._crcCalculated = true;
			}
			return this._Crc32;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001E6E0 File Offset: 0x0001C8E0
		private void PrepSourceStream()
		{
			if (this._sourceStream == null)
			{
				throw new ZipException(string.Format("The input stream is null for entry '{0}'.", this.FileName));
			}
			if (this._sourceStreamOriginalPosition != null)
			{
				this._sourceStream.Position = this._sourceStreamOriginalPosition.Value;
				return;
			}
			if (this._sourceStream.CanSeek)
			{
				this._sourceStreamOriginalPosition = new long?(this._sourceStream.Position);
				return;
			}
			if (this.Encryption == EncryptionAlgorithm.PkzipWeak && this._Source != ZipEntrySource.ZipFile && (this._BitField & 8) != 8)
			{
				throw new ZipException("It is not possible to use PKZIP encryption on a non-seekable input stream");
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001E794 File Offset: 0x0001C994
		internal void CopyMetaData(ZipEntry source)
		{
			this.__FileDataPosition = source.__FileDataPosition;
			this.CompressionMethod = source.CompressionMethod;
			this._CompressionMethod_FromZipFile = source._CompressionMethod_FromZipFile;
			this._CompressedFileDataSize = source._CompressedFileDataSize;
			this._UncompressedSize = source._UncompressedSize;
			this._BitField = source._BitField;
			this._Source = source._Source;
			this._LastModified = source._LastModified;
			this._Mtime = source._Mtime;
			this._Atime = source._Atime;
			this._Ctime = source._Ctime;
			this._ntfsTimesAreSet = source._ntfsTimesAreSet;
			this._emitUnixTimes = source._emitUnixTimes;
			this._emitNtfsTimes = source._emitNtfsTimes;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001E850 File Offset: 0x0001CA50
		private void OnWriteBlock(long bytesXferred, long totalBytesToXfer)
		{
			if (this._container.ZipFile != null)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnSaveBlock(this, bytesXferred, totalBytesToXfer);
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001E88C File Offset: 0x0001CA8C
		private void _WriteEntryData(Stream s)
		{
			Stream stream = null;
			long _FileDataPosition = -1L;
			try
			{
				_FileDataPosition = s.Position;
			}
			catch (Exception)
			{
			}
			try
			{
				long num = this.SetInputAndFigureFileLength(ref stream);
				CountingStream countingStream = new CountingStream(s);
				Stream stream2;
				Stream stream3;
				if (num != 0L)
				{
					stream2 = this.MaybeApplyEncryption(countingStream);
					stream3 = this.MaybeApplyCompression(stream2, num);
				}
				else
				{
					stream3 = (stream2 = countingStream);
				}
				CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(stream3, true);
				if (this._Source == ZipEntrySource.WriteDelegate)
				{
					this._WriteDelegate(this.FileName, crcCalculatorStream);
				}
				else
				{
					byte[] array = new byte[this.BufferSize];
					int count;
					while ((count = SharedUtilities.ReadWithRetry(stream, array, 0, array.Length, this.FileName)) != 0)
					{
						crcCalculatorStream.Write(array, 0, count);
						this.OnWriteBlock(crcCalculatorStream.TotalBytesSlurped, num);
						if (this._ioOperationCanceled)
						{
							break;
						}
					}
				}
				this.FinishOutputStream(s, countingStream, stream2, stream3, crcCalculatorStream);
			}
			finally
			{
				if (this._Source == ZipEntrySource.JitStream)
				{
					if (this._CloseDelegate != null)
					{
						this._CloseDelegate(this.FileName, stream);
					}
				}
				else if (stream is FileStream)
				{
					stream.Dispose();
				}
			}
			if (this._ioOperationCanceled)
			{
				return;
			}
			this.__FileDataPosition = _FileDataPosition;
			this.PostProcessOutput(s);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
		private long SetInputAndFigureFileLength(ref Stream input)
		{
			long result = -1L;
			if (this._Source == ZipEntrySource.Stream)
			{
				this.PrepSourceStream();
				input = this._sourceStream;
				try
				{
					return this._sourceStream.Length;
				}
				catch (NotSupportedException)
				{
					return result;
				}
			}
			if (this._Source == ZipEntrySource.ZipFile)
			{
				string password = (this._Encryption_FromZipFile == EncryptionAlgorithm.None) ? null : (this._Password ?? this._container.Password);
				this._sourceStream = this.InternalOpenReader(password);
				this.PrepSourceStream();
				input = this._sourceStream;
				result = this._sourceStream.Length;
			}
			else
			{
				if (this._Source == ZipEntrySource.JitStream)
				{
					if (this._sourceStream == null)
					{
						this._sourceStream = this._OpenDelegate(this.FileName);
					}
					this.PrepSourceStream();
					input = this._sourceStream;
					try
					{
						return this._sourceStream.Length;
					}
					catch (NotSupportedException)
					{
						return result;
					}
				}
				if (this._Source == ZipEntrySource.FileSystem)
				{
					FileShare fileShare = FileShare.ReadWrite;
					fileShare |= FileShare.Delete;
					input = File.Open(this.LocalFileName, FileMode.Open, FileAccess.Read, fileShare);
					result = input.Length;
				}
			}
			return result;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001EB30 File Offset: 0x0001CD30
		internal void FinishOutputStream(Stream s, CountingStream entryCounter, Stream encryptor, Stream compressor, CrcCalculatorStream output)
		{
			if (output == null)
			{
				return;
			}
			output.Close();
			if (compressor is DeflateStream)
			{
				compressor.Close();
			}
			else if (compressor is BZip2OutputStream)
			{
				compressor.Close();
			}
			else if (compressor is ParallelBZip2OutputStream)
			{
				compressor.Close();
			}
			else if (compressor is ParallelDeflateOutputStream)
			{
				compressor.Close();
			}
			encryptor.Flush();
			encryptor.Close();
			this._LengthOfTrailer = 0;
			this._UncompressedSize = output.TotalBytesSlurped;
			WinZipAesCipherStream winZipAesCipherStream = encryptor as WinZipAesCipherStream;
			if (winZipAesCipherStream != null && this._UncompressedSize > 0L)
			{
				s.Write(winZipAesCipherStream.FinalAuthentication, 0, 10);
				this._LengthOfTrailer += 10;
			}
			this._CompressedFileDataSize = entryCounter.BytesWritten;
			this._CompressedSize = this._CompressedFileDataSize;
			this._Crc32 = output.Crc;
			this.StoreRelativeOffset();
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001EC30 File Offset: 0x0001CE30
		internal void PostProcessOutput(Stream s)
		{
			CountingStream countingStream = s as CountingStream;
			if (this._UncompressedSize == 0L && this._CompressedSize == 0L)
			{
				if (this._Source == ZipEntrySource.ZipOutputStream)
				{
					return;
				}
				if (this._Password != null)
				{
					int num = 0;
					if (this.Encryption == EncryptionAlgorithm.PkzipWeak)
					{
						num = 12;
					}
					else if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
					{
						num = this._aesCrypto_forWrite._Salt.Length + this._aesCrypto_forWrite.GeneratedPV.Length;
					}
					if (this._Source == ZipEntrySource.ZipOutputStream && !s.CanSeek)
					{
						throw new ZipException("Zero bytes written, encryption in use, and non-seekable output.");
					}
					if (this.Encryption != EncryptionAlgorithm.None)
					{
						s.Seek((long)(-1 * num), SeekOrigin.Current);
						s.SetLength(s.Position);
						if (countingStream != null)
						{
							countingStream.Adjust((long)num);
						}
						this._LengthOfHeader -= num;
						this.__FileDataPosition -= (long)num;
					}
					this._Password = null;
					this._BitField &= -2;
					int num2 = 6;
					this._EntryHeader[num2++] = (byte)(this._BitField & 255);
					this._EntryHeader[num2++] = (byte)(((int)this._BitField & 65280) >> 8);
					if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
					{
						short num3 = (short)((int)this._EntryHeader[26] + (int)this._EntryHeader[27] * 256);
						int offx = (int)(30 + num3);
						int num4 = ZipEntry.FindExtraFieldSegment(this._EntryHeader, offx, 39169);
						if (num4 >= 0)
						{
							this._EntryHeader[num4++] = 153;
							this._EntryHeader[num4++] = 153;
						}
					}
				}
				this.CompressionMethod = CompressionMethod.None;
				this.Encryption = EncryptionAlgorithm.None;
			}
			else if (this._zipCrypto_forWrite != null || this._aesCrypto_forWrite != null)
			{
				if (this.Encryption == EncryptionAlgorithm.PkzipWeak)
				{
					this._CompressedSize += 12L;
				}
				else if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
				{
					this._CompressedSize += (long)this._aesCrypto_forWrite.SizeOfEncryptionMetadata;
				}
			}
			int num5 = 8;
			this._EntryHeader[num5++] = (byte)(this._CompressionMethod & 255);
			this._EntryHeader[num5++] = (byte)(((int)this._CompressionMethod & 65280) >> 8);
			num5 = 14;
			this._EntryHeader[num5++] = (byte)(this._Crc32 & 255);
			this._EntryHeader[num5++] = (byte)((this._Crc32 & 65280) >> 8);
			this._EntryHeader[num5++] = (byte)((this._Crc32 & 16711680) >> 16);
			this._EntryHeader[num5++] = (byte)(((long)this._Crc32 & (long)((ulong)-16777216)) >> 24);
			this.SetZip64Flags();
			short num6 = (short)((int)this._EntryHeader[26] + (int)this._EntryHeader[27] * 256);
			short num7 = (short)((int)this._EntryHeader[28] + (int)this._EntryHeader[29] * 256);
			if (this._OutputUsesZip64.Value)
			{
				this._EntryHeader[4] = 45;
				this._EntryHeader[5] = 0;
				for (int i = 0; i < 8; i++)
				{
					this._EntryHeader[num5++] = byte.MaxValue;
				}
				num5 = (int)(30 + num6);
				this._EntryHeader[num5++] = 1;
				this._EntryHeader[num5++] = 0;
				num5 += 2;
				Array.Copy(BitConverter.GetBytes(this._UncompressedSize), 0, this._EntryHeader, num5, 8);
				num5 += 8;
				Array.Copy(BitConverter.GetBytes(this._CompressedSize), 0, this._EntryHeader, num5, 8);
			}
			else
			{
				this._EntryHeader[4] = 20;
				this._EntryHeader[5] = 0;
				num5 = 18;
				this._EntryHeader[num5++] = (byte)(this._CompressedSize & 255L);
				this._EntryHeader[num5++] = (byte)((this._CompressedSize & 65280L) >> 8);
				this._EntryHeader[num5++] = (byte)((this._CompressedSize & 16711680L) >> 16);
				this._EntryHeader[num5++] = (byte)((this._CompressedSize & (long)((ulong)-16777216)) >> 24);
				this._EntryHeader[num5++] = (byte)(this._UncompressedSize & 255L);
				this._EntryHeader[num5++] = (byte)((this._UncompressedSize & 65280L) >> 8);
				this._EntryHeader[num5++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
				this._EntryHeader[num5++] = (byte)((this._UncompressedSize & (long)((ulong)-16777216)) >> 24);
				if (num7 != 0)
				{
					num5 = (int)(30 + num6);
					short num8 = (short)((int)this._EntryHeader[num5 + 2] + (int)this._EntryHeader[num5 + 3] * 256);
					if (num8 == 16)
					{
						this._EntryHeader[num5++] = 153;
						this._EntryHeader[num5++] = 153;
					}
				}
			}
			if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				num5 = 8;
				this._EntryHeader[num5++] = 99;
				this._EntryHeader[num5++] = 0;
				num5 = (int)(30 + num6);
				do
				{
					ushort num9 = (ushort)((int)this._EntryHeader[num5] + (int)this._EntryHeader[num5 + 1] * 256);
					short num10 = (short)((int)this._EntryHeader[num5 + 2] + (int)this._EntryHeader[num5 + 3] * 256);
					if (num9 != 39169)
					{
						num5 += (int)(num10 + 4);
					}
					else
					{
						num5 += 9;
						this._EntryHeader[num5++] = (byte)(this._CompressionMethod & 255);
						this._EntryHeader[num5++] = (byte)((int)this._CompressionMethod & 65280);
					}
				}
				while (num5 < (int)(num7 - 30 - num6));
			}
			if ((this._BitField & 8) != 8 || (this._Source == ZipEntrySource.ZipOutputStream && s.CanSeek))
			{
				ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
				if (zipSegmentedStream != null && this._diskNumber != zipSegmentedStream.CurrentSegment)
				{
					using (Stream stream = ZipSegmentedStream.ForUpdate(this._container.ZipFile.Name, this._diskNumber))
					{
						stream.Seek(this._RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
						stream.Write(this._EntryHeader, 0, this._EntryHeader.Length);
						goto IL_707;
					}
				}
				s.Seek(this._RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
				s.Write(this._EntryHeader, 0, this._EntryHeader.Length);
				if (countingStream != null)
				{
					countingStream.Adjust((long)this._EntryHeader.Length);
				}
				s.Seek(this._CompressedSize, SeekOrigin.Current);
			}
			IL_707:
			if ((this._BitField & 8) == 8 && !this.IsDirectory)
			{
				byte[] array = new byte[16 + (this._OutputUsesZip64.Value ? 8 : 0)];
				num5 = 0;
				Array.Copy(BitConverter.GetBytes(134695760), 0, array, num5, 4);
				num5 += 4;
				Array.Copy(BitConverter.GetBytes(this._Crc32), 0, array, num5, 4);
				num5 += 4;
				if (this._OutputUsesZip64.Value)
				{
					Array.Copy(BitConverter.GetBytes(this._CompressedSize), 0, array, num5, 8);
					num5 += 8;
					Array.Copy(BitConverter.GetBytes(this._UncompressedSize), 0, array, num5, 8);
					num5 += 8;
				}
				else
				{
					array[num5++] = (byte)(this._CompressedSize & 255L);
					array[num5++] = (byte)((this._CompressedSize & 65280L) >> 8);
					array[num5++] = (byte)((this._CompressedSize & 16711680L) >> 16);
					array[num5++] = (byte)((this._CompressedSize & (long)((ulong)-16777216)) >> 24);
					array[num5++] = (byte)(this._UncompressedSize & 255L);
					array[num5++] = (byte)((this._UncompressedSize & 65280L) >> 8);
					array[num5++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
					array[num5++] = (byte)((this._UncompressedSize & (long)((ulong)-16777216)) >> 24);
				}
				s.Write(array, 0, array.Length);
				this._LengthOfTrailer += array.Length;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001F504 File Offset: 0x0001D704
		private void SetZip64Flags()
		{
			this._entryRequiresZip64 = new bool?(this._CompressedSize >= (long)((ulong)-1) || this._UncompressedSize >= (long)((ulong)-1) || this._RelativeOffsetOfLocalHeader >= (long)((ulong)-1));
			if (this._container.Zip64 == Zip64Option.Default && this._entryRequiresZip64.Value)
			{
				throw new ZipException("Compressed or Uncompressed size, or offset exceeds the maximum value. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
			}
			this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always || this._entryRequiresZip64.Value);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001F5A8 File Offset: 0x0001D7A8
		internal void PrepOutputStream(Stream s, long streamLength, out CountingStream outputCounter, out Stream encryptor, out Stream compressor, out CrcCalculatorStream output)
		{
			outputCounter = new CountingStream(s);
			if (streamLength != 0L)
			{
				encryptor = this.MaybeApplyEncryption(outputCounter);
				compressor = this.MaybeApplyCompression(encryptor, streamLength);
			}
			else
			{
				Stream stream;
				compressor = (stream = outputCounter);
				encryptor = stream;
			}
			output = new CrcCalculatorStream(compressor, true);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001F600 File Offset: 0x0001D800
		private Stream MaybeApplyCompression(Stream s, long streamLength)
		{
			if (this._CompressionMethod == 8 && this.CompressionLevel != CompressionLevel.None)
			{
				if (this._container.ParallelDeflateThreshold == 0L || (streamLength > this._container.ParallelDeflateThreshold && this._container.ParallelDeflateThreshold > 0L))
				{
					if (this._container.ParallelDeflater == null)
					{
						this._container.ParallelDeflater = new ParallelDeflateOutputStream(s, this.CompressionLevel, this._container.Strategy, true);
						if (this._container.CodecBufferSize > 0)
						{
							this._container.ParallelDeflater.BufferSize = this._container.CodecBufferSize;
						}
						if (this._container.ParallelDeflateMaxBufferPairs > 0)
						{
							this._container.ParallelDeflater.MaxBufferPairs = this._container.ParallelDeflateMaxBufferPairs;
						}
					}
					ParallelDeflateOutputStream parallelDeflater = this._container.ParallelDeflater;
					parallelDeflater.Reset(s);
					return parallelDeflater;
				}
				DeflateStream deflateStream = new DeflateStream(s, CompressionMode.Compress, this.CompressionLevel, true);
				if (this._container.CodecBufferSize > 0)
				{
					deflateStream.BufferSize = this._container.CodecBufferSize;
				}
				deflateStream.Strategy = this._container.Strategy;
				return deflateStream;
			}
			else
			{
				if (this._CompressionMethod != 12)
				{
					return s;
				}
				if (this._container.ParallelDeflateThreshold == 0L || (streamLength > this._container.ParallelDeflateThreshold && this._container.ParallelDeflateThreshold > 0L))
				{
					return new ParallelBZip2OutputStream(s, true);
				}
				return new BZip2OutputStream(s, true);
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001F798 File Offset: 0x0001D998
		private Stream MaybeApplyEncryption(Stream s)
		{
			if (this.Encryption == EncryptionAlgorithm.PkzipWeak)
			{
				return new ZipCipherStream(s, this._zipCrypto_forWrite, CryptoMode.Encrypt);
			}
			if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				return new WinZipAesCipherStream(s, this._aesCrypto_forWrite, CryptoMode.Encrypt);
			}
			return s;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001F7EC File Offset: 0x0001D9EC
		private void OnZipErrorWhileSaving(Exception e)
		{
			if (this._container.ZipFile != null)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnZipErrorSaving(this, e);
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001F818 File Offset: 0x0001DA18
		internal void Write(Stream s)
		{
			CountingStream countingStream = s as CountingStream;
			ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
			bool flag = false;
			do
			{
				try
				{
					if (this._Source == ZipEntrySource.ZipFile && !this._restreamRequiredOnSave)
					{
						this.CopyThroughOneEntry(s);
						break;
					}
					if (this.IsDirectory)
					{
						this.WriteHeader(s, 1);
						this.StoreRelativeOffset();
						this._entryRequiresZip64 = new bool?(this._RelativeOffsetOfLocalHeader >= (long)((ulong)-1));
						this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always || this._entryRequiresZip64.Value);
						if (zipSegmentedStream != null)
						{
							this._diskNumber = zipSegmentedStream.CurrentSegment;
						}
						break;
					}
					int num = 0;
					bool flag2;
					do
					{
						num++;
						this.WriteHeader(s, num);
						this.WriteSecurityMetadata(s);
						this._WriteEntryData(s);
						this._TotalEntrySize = (long)this._LengthOfHeader + this._CompressedFileDataSize + (long)this._LengthOfTrailer;
						flag2 = (num <= 1 && s.CanSeek && this.WantReadAgain());
						if (flag2)
						{
							if (zipSegmentedStream != null)
							{
								zipSegmentedStream.TruncateBackward(this._diskNumber, this._RelativeOffsetOfLocalHeader);
							}
							else
							{
								s.Seek(this._RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
							}
							s.SetLength(s.Position);
							if (countingStream != null)
							{
								countingStream.Adjust(this._TotalEntrySize);
							}
						}
					}
					while (flag2);
					this._skippedDuringSave = false;
					flag = true;
				}
				catch (Exception ex)
				{
					ZipErrorAction zipErrorAction = this.ZipErrorAction;
					int num2 = 0;
					while (this.ZipErrorAction != ZipErrorAction.Throw)
					{
						if (this.ZipErrorAction == ZipErrorAction.Skip || this.ZipErrorAction == ZipErrorAction.Retry)
						{
							long num3 = (countingStream != null) ? countingStream.ComputedPosition : s.Position;
							long num4 = num3 - this._future_ROLH;
							if (num4 > 0L)
							{
								s.Seek(num4, SeekOrigin.Current);
								long position = s.Position;
								s.SetLength(s.Position);
								if (countingStream != null)
								{
									countingStream.Adjust(num3 - position);
								}
							}
							if (this.ZipErrorAction == ZipErrorAction.Skip)
							{
								this.WriteStatus("Skipping file {0} (exception: {1})", new object[]
								{
									this.LocalFileName,
									ex.ToString()
								});
								this._skippedDuringSave = true;
								flag = true;
							}
							else
							{
								this.ZipErrorAction = zipErrorAction;
							}
						}
						else
						{
							if (num2 > 0)
							{
								throw;
							}
							if (this.ZipErrorAction == ZipErrorAction.InvokeErrorEvent)
							{
								this.OnZipErrorWhileSaving(ex);
								if (this._ioOperationCanceled)
								{
									flag = true;
									goto IL_28C;
								}
							}
							num2++;
							continue;
						}
						IL_28C:
						goto IL_291;
					}
					throw;
				}
				IL_291:;
			}
			while (!flag);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001FAD8 File Offset: 0x0001DCD8
		internal void StoreRelativeOffset()
		{
			this._RelativeOffsetOfLocalHeader = this._future_ROLH;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001FAE8 File Offset: 0x0001DCE8
		internal void NotifySaveComplete()
		{
			this._Encryption_FromZipFile = this._Encryption;
			this._CompressionMethod_FromZipFile = this._CompressionMethod;
			this._restreamRequiredOnSave = false;
			this._metadataChanged = false;
			this._Source = ZipEntrySource.ZipFile;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001FB18 File Offset: 0x0001DD18
		internal void WriteSecurityMetadata(Stream outstream)
		{
			if (this.Encryption == EncryptionAlgorithm.None)
			{
				return;
			}
			string password = this._Password;
			if (this._Source == ZipEntrySource.ZipFile && password == null)
			{
				password = this._container.Password;
			}
			if (password == null)
			{
				this._zipCrypto_forWrite = null;
				this._aesCrypto_forWrite = null;
				return;
			}
			if (this.Encryption == EncryptionAlgorithm.PkzipWeak)
			{
				this._zipCrypto_forWrite = ZipCrypto.ForWrite(password);
				Random random = new Random();
				byte[] array = new byte[12];
				random.NextBytes(array);
				if ((this._BitField & 8) == 8)
				{
					this._TimeBlob = SharedUtilities.DateTimeToPacked(this.LastModified);
					array[11] = (byte)(this._TimeBlob >> 8 & 255);
				}
				else
				{
					this.FigureCrc32();
					array[11] = (byte)(this._Crc32 >> 24 & 255);
				}
				byte[] array2 = this._zipCrypto_forWrite.EncryptMessage(array, array.Length);
				outstream.Write(array2, 0, array2.Length);
				this._LengthOfHeader += array2.Length;
				return;
			}
			if (this.Encryption == EncryptionAlgorithm.WinZipAes128 || this.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				int keyStrengthInBits = ZipEntry.GetKeyStrengthInBits(this.Encryption);
				this._aesCrypto_forWrite = WinZipAesCrypto.Generate(password, keyStrengthInBits);
				outstream.Write(this._aesCrypto_forWrite.Salt, 0, this._aesCrypto_forWrite._Salt.Length);
				outstream.Write(this._aesCrypto_forWrite.GeneratedPV, 0, this._aesCrypto_forWrite.GeneratedPV.Length);
				this._LengthOfHeader += this._aesCrypto_forWrite._Salt.Length + this._aesCrypto_forWrite.GeneratedPV.Length;
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001FCB4 File Offset: 0x0001DEB4
		private void CopyThroughOneEntry(Stream outStream)
		{
			if (this.LengthOfHeader == 0)
			{
				throw new BadStateException("Bad header length.");
			}
			bool flag = this._metadataChanged || this.ArchiveStream is ZipSegmentedStream || outStream is ZipSegmentedStream || (this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Default) || (!this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Always);
			if (flag)
			{
				this.CopyThroughWithRecompute(outStream);
			}
			else
			{
				this.CopyThroughWithNoChange(outStream);
			}
			this._entryRequiresZip64 = new bool?(this._CompressedSize >= (long)((ulong)-1) || this._UncompressedSize >= (long)((ulong)-1) || this._RelativeOffsetOfLocalHeader >= (long)((ulong)-1));
			this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always || this._entryRequiresZip64.Value);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001FDC0 File Offset: 0x0001DFC0
		private void CopyThroughWithRecompute(Stream outstream)
		{
			byte[] array = new byte[this.BufferSize];
			CountingStream countingStream = new CountingStream(this.ArchiveStream);
			long relativeOffsetOfLocalHeader = this._RelativeOffsetOfLocalHeader;
			int lengthOfHeader = this.LengthOfHeader;
			this.WriteHeader(outstream, 0);
			this.StoreRelativeOffset();
			if (!this.FileName.EndsWith("/"))
			{
				long num = relativeOffsetOfLocalHeader + (long)lengthOfHeader;
				int num2 = ZipEntry.GetLengthOfCryptoHeaderBytes(this._Encryption_FromZipFile);
				num -= (long)num2;
				this._LengthOfHeader += num2;
				countingStream.Seek(num, SeekOrigin.Begin);
				long num3 = this._CompressedSize;
				while (num3 > 0L)
				{
					num2 = ((num3 > (long)array.Length) ? array.Length : ((int)num3));
					int num4 = countingStream.Read(array, 0, num2);
					outstream.Write(array, 0, num4);
					num3 -= (long)num4;
					this.OnWriteBlock(countingStream.BytesRead, this._CompressedSize);
					if (this._ioOperationCanceled)
					{
						break;
					}
				}
				if ((this._BitField & 8) == 8)
				{
					int num5 = 16;
					if (this._InputUsesZip64)
					{
						num5 += 8;
					}
					byte[] buffer = new byte[num5];
					countingStream.Read(buffer, 0, num5);
					if (this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Default)
					{
						outstream.Write(buffer, 0, 8);
						if (this._CompressedSize > (long)((ulong)-1))
						{
							throw new InvalidOperationException("ZIP64 is required");
						}
						outstream.Write(buffer, 8, 4);
						if (this._UncompressedSize > (long)((ulong)-1))
						{
							throw new InvalidOperationException("ZIP64 is required");
						}
						outstream.Write(buffer, 16, 4);
						this._LengthOfTrailer -= 8;
					}
					else if (!this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Always)
					{
						byte[] buffer2 = new byte[4];
						outstream.Write(buffer, 0, 8);
						outstream.Write(buffer, 8, 4);
						outstream.Write(buffer2, 0, 4);
						outstream.Write(buffer, 12, 4);
						outstream.Write(buffer2, 0, 4);
						this._LengthOfTrailer += 8;
					}
					else
					{
						outstream.Write(buffer, 0, num5);
					}
				}
			}
			this._TotalEntrySize = (long)this._LengthOfHeader + this._CompressedFileDataSize + (long)this._LengthOfTrailer;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001FFFC File Offset: 0x0001E1FC
		private void CopyThroughWithNoChange(Stream outstream)
		{
			byte[] array = new byte[this.BufferSize];
			CountingStream countingStream = new CountingStream(this.ArchiveStream);
			countingStream.Seek(this._RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
			if (this._TotalEntrySize == 0L)
			{
				this._TotalEntrySize = (long)this._LengthOfHeader + this._CompressedFileDataSize + (long)this._LengthOfTrailer;
			}
			CountingStream countingStream2 = outstream as CountingStream;
			this._RelativeOffsetOfLocalHeader = ((countingStream2 != null) ? countingStream2.ComputedPosition : outstream.Position);
			long num = this._TotalEntrySize;
			while (num > 0L)
			{
				int count = (num > (long)array.Length) ? array.Length : ((int)num);
				int num2 = countingStream.Read(array, 0, count);
				outstream.Write(array, 0, num2);
				num -= (long)num2;
				this.OnWriteBlock(countingStream.BytesRead, this._TotalEntrySize);
				if (this._ioOperationCanceled)
				{
					return;
				}
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000200E4 File Offset: 0x0001E2E4
		[Conditional("Trace")]
		private void TraceWriteLine(string format, params object[] varParams)
		{
			lock (this._outputLock)
			{
				int hashCode = Thread.CurrentThread.GetHashCode();
				Console.ForegroundColor = hashCode % 8 + ConsoleColor.DarkGray;
				Console.Write("{0:000} ZipEntry.Write ", hashCode);
				Console.WriteLine(format, varParams);
				Console.ResetColor();
			}
		}

		// Token: 0x040001CB RID: 459
		private short _VersionMadeBy;

		// Token: 0x040001CC RID: 460
		private short _InternalFileAttrs;

		// Token: 0x040001CD RID: 461
		private int _ExternalFileAttrs;

		// Token: 0x040001CE RID: 462
		private short _filenameLength;

		// Token: 0x040001CF RID: 463
		private short _extraFieldLength;

		// Token: 0x040001D0 RID: 464
		private short _commentLength;

		// Token: 0x040001D1 RID: 465
		private ZipCrypto _zipCrypto_forExtract;

		// Token: 0x040001D2 RID: 466
		private ZipCrypto _zipCrypto_forWrite;

		// Token: 0x040001D3 RID: 467
		private WinZipAesCrypto _aesCrypto_forExtract;

		// Token: 0x040001D4 RID: 468
		private WinZipAesCrypto _aesCrypto_forWrite;

		// Token: 0x040001D5 RID: 469
		private short _WinZipAesMethod;

		// Token: 0x040001D6 RID: 470
		internal DateTime _LastModified;

		// Token: 0x040001D7 RID: 471
		private DateTime _Mtime;

		// Token: 0x040001D8 RID: 472
		private DateTime _Atime;

		// Token: 0x040001D9 RID: 473
		private DateTime _Ctime;

		// Token: 0x040001DA RID: 474
		private bool _ntfsTimesAreSet;

		// Token: 0x040001DB RID: 475
		private bool _emitNtfsTimes = true;

		// Token: 0x040001DC RID: 476
		private bool _emitUnixTimes;

		// Token: 0x040001DD RID: 477
		private bool _TrimVolumeFromFullyQualifiedPaths = true;

		// Token: 0x040001DE RID: 478
		internal string _LocalFileName;

		// Token: 0x040001DF RID: 479
		private string _FileNameInArchive;

		// Token: 0x040001E0 RID: 480
		internal short _VersionNeeded;

		// Token: 0x040001E1 RID: 481
		internal short _BitField;

		// Token: 0x040001E2 RID: 482
		internal short _CompressionMethod;

		// Token: 0x040001E3 RID: 483
		private short _CompressionMethod_FromZipFile;

		// Token: 0x040001E4 RID: 484
		private CompressionLevel _CompressionLevel;

		// Token: 0x040001E5 RID: 485
		internal string _Comment;

		// Token: 0x040001E6 RID: 486
		private bool _IsDirectory;

		// Token: 0x040001E7 RID: 487
		private byte[] _CommentBytes;

		// Token: 0x040001E8 RID: 488
		internal long _CompressedSize;

		// Token: 0x040001E9 RID: 489
		internal long _CompressedFileDataSize;

		// Token: 0x040001EA RID: 490
		internal long _UncompressedSize;

		// Token: 0x040001EB RID: 491
		internal int _TimeBlob;

		// Token: 0x040001EC RID: 492
		private bool _crcCalculated;

		// Token: 0x040001ED RID: 493
		internal int _Crc32;

		// Token: 0x040001EE RID: 494
		internal byte[] _Extra;

		// Token: 0x040001EF RID: 495
		private bool _metadataChanged;

		// Token: 0x040001F0 RID: 496
		private bool _restreamRequiredOnSave;

		// Token: 0x040001F1 RID: 497
		private bool _sourceIsEncrypted;

		// Token: 0x040001F2 RID: 498
		private bool _skippedDuringSave;

		// Token: 0x040001F3 RID: 499
		private uint _diskNumber;

		// Token: 0x040001F4 RID: 500
		private static Encoding ibm437 = Encoding.GetEncoding("IBM437");

		// Token: 0x040001F5 RID: 501
		private Encoding _actualEncoding;

		// Token: 0x040001F6 RID: 502
		internal ZipContainer _container;

		// Token: 0x040001F7 RID: 503
		private long __FileDataPosition = -1L;

		// Token: 0x040001F8 RID: 504
		private byte[] _EntryHeader;

		// Token: 0x040001F9 RID: 505
		internal long _RelativeOffsetOfLocalHeader;

		// Token: 0x040001FA RID: 506
		private long _future_ROLH;

		// Token: 0x040001FB RID: 507
		private long _TotalEntrySize;

		// Token: 0x040001FC RID: 508
		private int _LengthOfHeader;

		// Token: 0x040001FD RID: 509
		private int _LengthOfTrailer;

		// Token: 0x040001FE RID: 510
		internal bool _InputUsesZip64;

		// Token: 0x040001FF RID: 511
		private uint _UnsupportedAlgorithmId;

		// Token: 0x04000200 RID: 512
		internal string _Password;

		// Token: 0x04000201 RID: 513
		internal ZipEntrySource _Source;

		// Token: 0x04000202 RID: 514
		internal EncryptionAlgorithm _Encryption;

		// Token: 0x04000203 RID: 515
		internal EncryptionAlgorithm _Encryption_FromZipFile;

		// Token: 0x04000204 RID: 516
		internal byte[] _WeakEncryptionHeader;

		// Token: 0x04000205 RID: 517
		internal Stream _archiveStream;

		// Token: 0x04000206 RID: 518
		private Stream _sourceStream;

		// Token: 0x04000207 RID: 519
		private long? _sourceStreamOriginalPosition;

		// Token: 0x04000208 RID: 520
		private bool _sourceWasJitProvided;

		// Token: 0x04000209 RID: 521
		private bool _ioOperationCanceled;

		// Token: 0x0400020A RID: 522
		private bool _presumeZip64;

		// Token: 0x0400020B RID: 523
		private bool? _entryRequiresZip64;

		// Token: 0x0400020C RID: 524
		private bool? _OutputUsesZip64;

		// Token: 0x0400020D RID: 525
		private bool _IsText;

		// Token: 0x0400020E RID: 526
		private ZipEntryTimestamp _timestamp;

		// Token: 0x0400020F RID: 527
		private static DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000210 RID: 528
		private static DateTime _win32Epoch = DateTime.FromFileTimeUtc(0L);

		// Token: 0x04000211 RID: 529
		private static DateTime _zeroHour = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000212 RID: 530
		private WriteDelegate _WriteDelegate;

		// Token: 0x04000213 RID: 531
		private OpenDelegate _OpenDelegate;

		// Token: 0x04000214 RID: 532
		private CloseDelegate _CloseDelegate;

		// Token: 0x04000215 RID: 533
		private Stream _inputDecryptorStream;

		// Token: 0x04000216 RID: 534
		private int _readExtraDepth;

		// Token: 0x04000217 RID: 535
		private object _outputLock = new object();

		// Token: 0x0200026F RID: 623
		private class CopyHelper
		{
			// Token: 0x060016EE RID: 5870 RVA: 0x00076FD8 File Offset: 0x000751D8
			internal static string AppendCopyToFileName(string f)
			{
				ZipEntry.CopyHelper.callCount++;
				if (ZipEntry.CopyHelper.callCount > 25)
				{
					throw new OverflowException("overflow while creating filename");
				}
				int num = 1;
				int num2 = f.LastIndexOf(".");
				if (num2 == -1)
				{
					Match match = ZipEntry.CopyHelper.re.Match(f);
					if (match.Success)
					{
						num = int.Parse(match.Groups[1].Value) + 1;
						string str = string.Format(" (copy {0})", num);
						f = f.Substring(0, match.Index) + str;
					}
					else
					{
						string str2 = string.Format(" (copy {0})", num);
						f += str2;
					}
				}
				else
				{
					Match match2 = ZipEntry.CopyHelper.re.Match(f.Substring(0, num2));
					if (match2.Success)
					{
						num = int.Parse(match2.Groups[1].Value) + 1;
						string str3 = string.Format(" (copy {0})", num);
						f = f.Substring(0, match2.Index) + str3 + f.Substring(num2);
					}
					else
					{
						string str4 = string.Format(" (copy {0})", num);
						f = f.Substring(0, num2) + str4 + f.Substring(num2);
					}
				}
				return f;
			}

			// Token: 0x04000ACA RID: 2762
			private static Regex re = new Regex(" \\(copy (\\d+)\\)$");

			// Token: 0x04000ACB RID: 2763
			private static int callCount = 0;
		}

		// Token: 0x02000270 RID: 624
		// (Invoke) Token: 0x060016F2 RID: 5874
		private delegate T Func<T>();
	}
}
