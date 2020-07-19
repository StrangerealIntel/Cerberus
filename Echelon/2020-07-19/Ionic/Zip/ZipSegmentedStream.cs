using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x020000B6 RID: 182
	internal class ZipSegmentedStream : Stream
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x00026758 File Offset: 0x00024958
		private ZipSegmentedStream()
		{
			this._exceptionPending = false;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00026768 File Offset: 0x00024968
		public static ZipSegmentedStream ForReading(string name, uint initialDiskNumber, uint maxDiskNumber)
		{
			ZipSegmentedStream zipSegmentedStream = new ZipSegmentedStream
			{
				rwMode = ZipSegmentedStream.RwMode.ReadOnly,
				CurrentSegment = initialDiskNumber,
				_maxDiskNumber = maxDiskNumber,
				_baseName = name
			};
			zipSegmentedStream._SetReadStream();
			return zipSegmentedStream;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000267A4 File Offset: 0x000249A4
		public static ZipSegmentedStream ForWriting(string name, int maxSegmentSize)
		{
			ZipSegmentedStream zipSegmentedStream = new ZipSegmentedStream
			{
				rwMode = ZipSegmentedStream.RwMode.Write,
				CurrentSegment = 0u,
				_baseName = name,
				_maxSegmentSize = maxSegmentSize,
				_baseDir = Path.GetDirectoryName(name)
			};
			if (zipSegmentedStream._baseDir == "")
			{
				zipSegmentedStream._baseDir = ".";
			}
			zipSegmentedStream._SetWriteStream(0u);
			return zipSegmentedStream;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00026810 File Offset: 0x00024A10
		public static Stream ForUpdate(string name, uint diskNumber)
		{
			if (diskNumber >= 99u)
			{
				throw new ArgumentOutOfRangeException("diskNumber");
			}
			string path = string.Format("{0}.z{1:D2}", Path.Combine(Path.GetDirectoryName(name), Path.GetFileNameWithoutExtension(name)), diskNumber + 1u);
			return File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00026864 File Offset: 0x00024A64
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x0002686C File Offset: 0x00024A6C
		public bool ContiguousWrite { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00026878 File Offset: 0x00024A78
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00026880 File Offset: 0x00024A80
		public uint CurrentSegment
		{
			get
			{
				return this._currentDiskNumber;
			}
			private set
			{
				this._currentDiskNumber = value;
				this._currentName = null;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00026890 File Offset: 0x00024A90
		public string CurrentName
		{
			get
			{
				if (this._currentName == null)
				{
					this._currentName = this._NameForSegment(this.CurrentSegment);
				}
				return this._currentName;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x000268B8 File Offset: 0x00024AB8
		public string CurrentTempName
		{
			get
			{
				return this._currentTempName;
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000268C0 File Offset: 0x00024AC0
		private string _NameForSegment(uint diskNumber)
		{
			if (diskNumber >= 99u)
			{
				this._exceptionPending = true;
				throw new OverflowException("The number of zip segments would exceed 99.");
			}
			return string.Format("{0}.z{1:D2}", Path.Combine(Path.GetDirectoryName(this._baseName), Path.GetFileNameWithoutExtension(this._baseName)), diskNumber + 1u);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00026918 File Offset: 0x00024B18
		public uint ComputeSegment(int length)
		{
			if (this._innerStream.Position + (long)length > (long)this._maxSegmentSize)
			{
				return this.CurrentSegment + 1u;
			}
			return this.CurrentSegment;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00026944 File Offset: 0x00024B44
		public override string ToString()
		{
			return string.Format("{0}[{1}][{2}], pos=0x{3:X})", new object[]
			{
				"ZipSegmentedStream",
				this.CurrentName,
				this.rwMode.ToString(),
				this.Position
			});
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0002699C File Offset: 0x00024B9C
		private void _SetReadStream()
		{
			if (this._innerStream != null)
			{
				this._innerStream.Dispose();
			}
			if (this.CurrentSegment + 1u == this._maxDiskNumber)
			{
				this._currentName = this._baseName;
			}
			this._innerStream = File.OpenRead(this.CurrentName);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000269F4 File Offset: 0x00024BF4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.rwMode != ZipSegmentedStream.RwMode.ReadOnly)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Stream Error: Cannot Read.");
			}
			int num = this._innerStream.Read(buffer, offset, count);
			int num2 = num;
			while (num2 != count)
			{
				if (this._innerStream.Position != this._innerStream.Length)
				{
					this._exceptionPending = true;
					throw new ZipException(string.Format("Read error in file {0}", this.CurrentName));
				}
				if (this.CurrentSegment + 1u == this._maxDiskNumber)
				{
					return num;
				}
				this.CurrentSegment += 1u;
				this._SetReadStream();
				offset += num2;
				count -= num2;
				num2 = this._innerStream.Read(buffer, offset, count);
				num += num2;
			}
			return num;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00026AC0 File Offset: 0x00024CC0
		private void _SetWriteStream(uint increment)
		{
			if (this._innerStream != null)
			{
				this._innerStream.Dispose();
				if (File.Exists(this.CurrentName))
				{
					File.Delete(this.CurrentName);
				}
				File.Move(this._currentTempName, this.CurrentName);
			}
			if (increment > 0u)
			{
				this.CurrentSegment += increment;
			}
			SharedUtilities.CreateAndOpenUniqueTempFile(this._baseDir, out this._innerStream, out this._currentTempName);
			if (this.CurrentSegment == 0u)
			{
				this._innerStream.Write(BitConverter.GetBytes(134695760), 0, 4);
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00026B64 File Offset: 0x00024D64
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.rwMode != ZipSegmentedStream.RwMode.Write)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Stream Error: Cannot Write.");
			}
			if (this.ContiguousWrite)
			{
				if (this._innerStream.Position + (long)count > (long)this._maxSegmentSize)
				{
					this._SetWriteStream(1u);
				}
			}
			else
			{
				while (this._innerStream.Position + (long)count > (long)this._maxSegmentSize)
				{
					int num = this._maxSegmentSize - (int)this._innerStream.Position;
					this._innerStream.Write(buffer, offset, num);
					this._SetWriteStream(1u);
					count -= num;
					offset += num;
				}
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00026C1C File Offset: 0x00024E1C
		public long TruncateBackward(uint diskNumber, long offset)
		{
			if (diskNumber >= 99u)
			{
				throw new ArgumentOutOfRangeException("diskNumber");
			}
			if (this.rwMode != ZipSegmentedStream.RwMode.Write)
			{
				this._exceptionPending = true;
				throw new ZipException("bad state.");
			}
			if (diskNumber == this.CurrentSegment)
			{
				return this._innerStream.Seek(offset, SeekOrigin.Begin);
			}
			if (this._innerStream != null)
			{
				this._innerStream.Dispose();
				if (File.Exists(this._currentTempName))
				{
					File.Delete(this._currentTempName);
				}
			}
			for (uint num = this.CurrentSegment - 1u; num > diskNumber; num -= 1u)
			{
				string path = this._NameForSegment(num);
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
			this.CurrentSegment = diskNumber;
			for (int i = 0; i < 3; i++)
			{
				try
				{
					this._currentTempName = SharedUtilities.InternalGetTempFileName();
					File.Move(this.CurrentName, this._currentTempName);
					break;
				}
				catch (IOException)
				{
					if (i == 2)
					{
						throw;
					}
				}
			}
			this._innerStream = new FileStream(this._currentTempName, FileMode.Open);
			return this._innerStream.Seek(offset, SeekOrigin.Begin);
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00026D50 File Offset: 0x00024F50
		public override bool CanRead
		{
			get
			{
				return this.rwMode == ZipSegmentedStream.RwMode.ReadOnly && this._innerStream != null && this._innerStream.CanRead;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00026D78 File Offset: 0x00024F78
		public override bool CanSeek
		{
			get
			{
				return this._innerStream != null && this._innerStream.CanSeek;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00026D94 File Offset: 0x00024F94
		public override bool CanWrite
		{
			get
			{
				return this.rwMode == ZipSegmentedStream.RwMode.Write && this._innerStream != null && this._innerStream.CanWrite;
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00026DBC File Offset: 0x00024FBC
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00026DCC File Offset: 0x00024FCC
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00026DDC File Offset: 0x00024FDC
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00026DEC File Offset: 0x00024FEC
		public override long Position
		{
			get
			{
				return this._innerStream.Position;
			}
			set
			{
				this._innerStream.Position = value;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00026DFC File Offset: 0x00024FFC
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(offset, origin);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00026E1C File Offset: 0x0002501C
		public override void SetLength(long value)
		{
			if (this.rwMode != ZipSegmentedStream.RwMode.Write)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException();
			}
			this._innerStream.SetLength(value);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00026E44 File Offset: 0x00025044
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this._innerStream != null)
				{
					this._innerStream.Dispose();
					if (this.rwMode == ZipSegmentedStream.RwMode.Write)
					{
						bool exceptionPending = this._exceptionPending;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x040002C6 RID: 710
		private ZipSegmentedStream.RwMode rwMode;

		// Token: 0x040002C7 RID: 711
		private bool _exceptionPending;

		// Token: 0x040002C8 RID: 712
		private string _baseName;

		// Token: 0x040002C9 RID: 713
		private string _baseDir;

		// Token: 0x040002CA RID: 714
		private string _currentName;

		// Token: 0x040002CB RID: 715
		private string _currentTempName;

		// Token: 0x040002CC RID: 716
		private uint _currentDiskNumber;

		// Token: 0x040002CD RID: 717
		private uint _maxDiskNumber;

		// Token: 0x040002CE RID: 718
		private int _maxSegmentSize;

		// Token: 0x040002CF RID: 719
		private Stream _innerStream;

		// Token: 0x02000276 RID: 630
		private enum RwMode
		{
			// Token: 0x04000ADF RID: 2783
			None,
			// Token: 0x04000AE0 RID: 2784
			ReadOnly,
			// Token: 0x04000AE1 RID: 2785
			Write
		}
	}
}
