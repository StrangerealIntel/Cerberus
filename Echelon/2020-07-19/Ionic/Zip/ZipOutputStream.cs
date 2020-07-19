using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Ionic.Crc;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x020000B4 RID: 180
	[ComVisible(true)]
	public class ZipOutputStream : Stream
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x00025B5C File Offset: 0x00023D5C
		public ZipOutputStream(Stream stream) : this(stream, false)
		{
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00025B68 File Offset: 0x00023D68
		public ZipOutputStream(string fileName)
		{
			this._alternateEncoding = Encoding.GetEncoding("IBM437");
			this._maxBufferPairs = 16;
			base..ctor();
			Stream stream = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
			this._Init(stream, false, fileName);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00025BAC File Offset: 0x00023DAC
		public ZipOutputStream(Stream stream, bool leaveOpen)
		{
			this._alternateEncoding = Encoding.GetEncoding("IBM437");
			this._maxBufferPairs = 16;
			base..ctor();
			this._Init(stream, leaveOpen, null);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00025BD8 File Offset: 0x00023DD8
		private void _Init(Stream stream, bool leaveOpen, string name)
		{
			this._outputStream = (stream.CanRead ? stream : new CountingStream(stream));
			this.CompressionLevel = CompressionLevel.Default;
			this.CompressionMethod = CompressionMethod.Deflate;
			this._encryption = EncryptionAlgorithm.None;
			this._entriesWritten = new Dictionary<string, ZipEntry>(StringComparer.Ordinal);
			this._zip64 = Zip64Option.Default;
			this._leaveUnderlyingStreamOpen = leaveOpen;
			this.Strategy = CompressionStrategy.Default;
			this._name = (name ?? "(stream)");
			this.ParallelDeflateThreshold = -1L;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00025C5C File Offset: 0x00023E5C
		public override string ToString()
		{
			return string.Format("ZipOutputStream::{0}(leaveOpen({1})))", this._name, this._leaveUnderlyingStreamOpen);
		}

		// Token: 0x170000F2 RID: 242
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00025C7C File Offset: 0x00023E7C
		public string Password
		{
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._password = value;
				if (this._password == null)
				{
					this._encryption = EncryptionAlgorithm.None;
					return;
				}
				if (this._encryption == EncryptionAlgorithm.None)
				{
					this._encryption = EncryptionAlgorithm.PkzipWeak;
				}
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00025CD8 File Offset: 0x00023ED8
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00025CE0 File Offset: 0x00023EE0
		public EncryptionAlgorithm Encryption
		{
			get
			{
				return this._encryption;
			}
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				if (value == EncryptionAlgorithm.Unsupported)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				this._encryption = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00025D20 File Offset: 0x00023F20
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x00025D28 File Offset: 0x00023F28
		public int CodecBufferSize { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00025D34 File Offset: 0x00023F34
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x00025D3C File Offset: 0x00023F3C
		public CompressionStrategy Strategy { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00025D48 File Offset: 0x00023F48
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x00025D50 File Offset: 0x00023F50
		public ZipEntryTimestamp Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._timestamp = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00025D78 File Offset: 0x00023F78
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00025D80 File Offset: 0x00023F80
		public CompressionLevel CompressionLevel { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00025D8C File Offset: 0x00023F8C
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x00025D94 File Offset: 0x00023F94
		public CompressionMethod CompressionMethod { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00025DA0 File Offset: 0x00023FA0
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00025DA8 File Offset: 0x00023FA8
		public string Comment
		{
			get
			{
				return this._comment;
			}
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._comment = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00025DD0 File Offset: 0x00023FD0
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00025DD8 File Offset: 0x00023FD8
		public Zip64Option EnableZip64
		{
			get
			{
				return this._zip64;
			}
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._zip64 = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00025E00 File Offset: 0x00024000
		public bool OutputUsedZip64
		{
			get
			{
				return this._anyEntriesUsedZip64 || this._directoryNeededZip64;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00025E18 File Offset: 0x00024018
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x00025E24 File Offset: 0x00024024
		public bool IgnoreCase
		{
			get
			{
				return !this._DontIgnoreCase;
			}
			set
			{
				this._DontIgnoreCase = !value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00025E30 File Offset: 0x00024030
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x00025E50 File Offset: 0x00024050
		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete. It will be removed in a future version of the library. Use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				return this._alternateEncoding == Encoding.UTF8 && this.AlternateEncodingUsage == ZipOption.AsNecessary;
			}
			set
			{
				if (value)
				{
					this._alternateEncoding = Encoding.UTF8;
					this._alternateEncodingUsage = ZipOption.AsNecessary;
					return;
				}
				this._alternateEncoding = ZipOutputStream.DefaultEncoding;
				this._alternateEncodingUsage = ZipOption.Default;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00025E80 File Offset: 0x00024080
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00025E98 File Offset: 0x00024098
		[Obsolete("use AlternateEncoding and AlternateEncodingUsage instead.")]
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				if (this._alternateEncodingUsage == ZipOption.AsNecessary)
				{
					return this._alternateEncoding;
				}
				return null;
			}
			set
			{
				this._alternateEncoding = value;
				this._alternateEncodingUsage = ZipOption.AsNecessary;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00025EA8 File Offset: 0x000240A8
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x00025EB0 File Offset: 0x000240B0
		public Encoding AlternateEncoding
		{
			get
			{
				return this._alternateEncoding;
			}
			set
			{
				this._alternateEncoding = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00025EBC File Offset: 0x000240BC
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00025EC4 File Offset: 0x000240C4
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				return this._alternateEncodingUsage;
			}
			set
			{
				this._alternateEncodingUsage = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00025ED0 File Offset: 0x000240D0
		public static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.GetEncoding("IBM437");
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00025F0C File Offset: 0x0002410C
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00025EDC File Offset: 0x000240DC
		public long ParallelDeflateThreshold
		{
			get
			{
				return this._ParallelDeflateThreshold;
			}
			set
			{
				if (value != 0L && value != -1L && value < 65536L)
				{
					throw new ArgumentOutOfRangeException("value must be greater than 64k, or 0, or -1");
				}
				this._ParallelDeflateThreshold = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00025F14 File Offset: 0x00024114
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x00025F1C File Offset: 0x0002411C
		public int ParallelDeflateMaxBufferPairs
		{
			get
			{
				return this._maxBufferPairs;
			}
			set
			{
				if (value < 4)
				{
					throw new ArgumentOutOfRangeException("ParallelDeflateMaxBufferPairs", "Value must be 4 or greater.");
				}
				this._maxBufferPairs = value;
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00025F3C File Offset: 0x0002413C
		private void InsureUniqueEntry(ZipEntry ze1)
		{
			if (this._entriesWritten.ContainsKey(ze1.FileName))
			{
				this._exceptionPending = true;
				throw new ArgumentException(string.Format("The entry '{0}' already exists in the zip archive.", ze1.FileName));
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00025F74 File Offset: 0x00024174
		internal Stream OutputStream
		{
			get
			{
				return this._outputStream;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00025F7C File Offset: 0x0002417C
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00025F84 File Offset: 0x00024184
		public bool ContainsEntry(string name)
		{
			return this._entriesWritten.ContainsKey(SharedUtilities.NormalizePathForUseInZipFile(name));
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00025F98 File Offset: 0x00024198
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			if (buffer == null)
			{
				this._exceptionPending = true;
				throw new ArgumentNullException("buffer");
			}
			if (this._currentEntry == null)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("You must call PutNextEntry() before calling Write().");
			}
			if (this._currentEntry.IsDirectory)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("You cannot Write() data for an entry that is a directory.");
			}
			if (this._needToWriteEntryHeader)
			{
				this._InitiateCurrentEntry(false);
			}
			if (count != 0)
			{
				this._entryOutputStream.Write(buffer, offset, count);
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00026044 File Offset: 0x00024244
		public ZipEntry PutNextEntry(string entryName)
		{
			if (string.IsNullOrEmpty(entryName))
			{
				throw new ArgumentNullException("entryName");
			}
			if (this._disposed)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			this._FinishCurrentEntry();
			this._currentEntry = ZipEntry.CreateForZipOutputStream(entryName);
			this._currentEntry._container = new ZipContainer(this);
			ZipEntry currentEntry = this._currentEntry;
			currentEntry._BitField |= 8;
			this._currentEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			this._currentEntry.CompressionLevel = this.CompressionLevel;
			this._currentEntry.CompressionMethod = this.CompressionMethod;
			this._currentEntry.Password = this._password;
			this._currentEntry.Encryption = this.Encryption;
			this._currentEntry.AlternateEncoding = this.AlternateEncoding;
			this._currentEntry.AlternateEncodingUsage = this.AlternateEncodingUsage;
			if (entryName.EndsWith("/"))
			{
				this._currentEntry.MarkAsDirectory();
			}
			this._currentEntry.EmitTimesInWindowsFormatWhenSaving = ((this._timestamp & ZipEntryTimestamp.Windows) != ZipEntryTimestamp.None);
			this._currentEntry.EmitTimesInUnixFormatWhenSaving = ((this._timestamp & ZipEntryTimestamp.Unix) != ZipEntryTimestamp.None);
			this.InsureUniqueEntry(this._currentEntry);
			this._needToWriteEntryHeader = true;
			return this._currentEntry;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000261A8 File Offset: 0x000243A8
		private void _InitiateCurrentEntry(bool finishing)
		{
			this._entriesWritten.Add(this._currentEntry.FileName, this._currentEntry);
			this._entryCount++;
			if (this._entryCount > 65534 && this._zip64 == Zip64Option.Default)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Too many entries. Consider setting ZipOutputStream.EnableZip64.");
			}
			this._currentEntry.WriteHeader(this._outputStream, finishing ? 99 : 0);
			this._currentEntry.StoreRelativeOffset();
			if (!this._currentEntry.IsDirectory)
			{
				this._currentEntry.WriteSecurityMetadata(this._outputStream);
				this._currentEntry.PrepOutputStream(this._outputStream, finishing ? 0L : -1L, out this._outputCounter, out this._encryptor, out this._deflater, out this._entryOutputStream);
			}
			this._needToWriteEntryHeader = false;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0002629C File Offset: 0x0002449C
		private void _FinishCurrentEntry()
		{
			if (this._currentEntry != null)
			{
				if (this._needToWriteEntryHeader)
				{
					this._InitiateCurrentEntry(true);
				}
				this._currentEntry.FinishOutputStream(this._outputStream, this._outputCounter, this._encryptor, this._deflater, this._entryOutputStream);
				this._currentEntry.PostProcessOutput(this._outputStream);
				if (this._currentEntry.OutputUsedZip64 != null)
				{
					this._anyEntriesUsedZip64 |= this._currentEntry.OutputUsedZip64.Value;
				}
				this._outputCounter = null;
				this._encryptor = (this._deflater = null);
				this._entryOutputStream = null;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0002635C File Offset: 0x0002455C
		protected override void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing && !this._exceptionPending)
			{
				this._FinishCurrentEntry();
				this._directoryNeededZip64 = ZipOutput.WriteCentralDirectoryStructure(this._outputStream, this._entriesWritten.Values, 1u, this._zip64, this.Comment, new ZipContainer(this));
				CountingStream countingStream = this._outputStream as CountingStream;
				Stream stream;
				if (countingStream != null)
				{
					stream = countingStream.WrappedStream;
					countingStream.Dispose();
				}
				else
				{
					stream = this._outputStream;
				}
				if (!this._leaveUnderlyingStreamOpen)
				{
					stream.Dispose();
				}
				this._outputStream = null;
			}
			this._disposed = true;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0002640C File Offset: 0x0002460C
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00026410 File Offset: 0x00024610
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00026414 File Offset: 0x00024614
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00026418 File Offset: 0x00024618
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00026420 File Offset: 0x00024620
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00026430 File Offset: 0x00024630
		public override long Position
		{
			get
			{
				return this._outputStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00026438 File Offset: 0x00024638
		public override void Flush()
		{
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0002643C File Offset: 0x0002463C
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Read");
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00026448 File Offset: 0x00024648
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Seek");
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00026454 File Offset: 0x00024654
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040002A5 RID: 677
		private EncryptionAlgorithm _encryption;

		// Token: 0x040002A6 RID: 678
		private ZipEntryTimestamp _timestamp;

		// Token: 0x040002A7 RID: 679
		internal string _password;

		// Token: 0x040002A8 RID: 680
		private string _comment;

		// Token: 0x040002A9 RID: 681
		private Stream _outputStream;

		// Token: 0x040002AA RID: 682
		private ZipEntry _currentEntry;

		// Token: 0x040002AB RID: 683
		internal Zip64Option _zip64;

		// Token: 0x040002AC RID: 684
		private Dictionary<string, ZipEntry> _entriesWritten;

		// Token: 0x040002AD RID: 685
		private int _entryCount;

		// Token: 0x040002AE RID: 686
		private ZipOption _alternateEncodingUsage;

		// Token: 0x040002AF RID: 687
		private Encoding _alternateEncoding;

		// Token: 0x040002B0 RID: 688
		private bool _leaveUnderlyingStreamOpen;

		// Token: 0x040002B1 RID: 689
		private bool _disposed;

		// Token: 0x040002B2 RID: 690
		private bool _exceptionPending;

		// Token: 0x040002B3 RID: 691
		private bool _anyEntriesUsedZip64;

		// Token: 0x040002B4 RID: 692
		private bool _directoryNeededZip64;

		// Token: 0x040002B5 RID: 693
		private CountingStream _outputCounter;

		// Token: 0x040002B6 RID: 694
		private Stream _encryptor;

		// Token: 0x040002B7 RID: 695
		private Stream _deflater;

		// Token: 0x040002B8 RID: 696
		private CrcCalculatorStream _entryOutputStream;

		// Token: 0x040002B9 RID: 697
		private bool _needToWriteEntryHeader;

		// Token: 0x040002BA RID: 698
		private string _name;

		// Token: 0x040002BB RID: 699
		private bool _DontIgnoreCase;

		// Token: 0x040002BC RID: 700
		internal ParallelDeflateOutputStream ParallelDeflater;

		// Token: 0x040002BD RID: 701
		private long _ParallelDeflateThreshold;

		// Token: 0x040002BE RID: 702
		private int _maxBufferPairs;
	}
}
