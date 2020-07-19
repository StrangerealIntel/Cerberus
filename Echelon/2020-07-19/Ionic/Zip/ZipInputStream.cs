using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Ionic.Crc;

namespace Ionic.Zip
{
	// Token: 0x020000B3 RID: 179
	[ComVisible(true)]
	public class ZipInputStream : Stream
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x000257B8 File Offset: 0x000239B8
		public ZipInputStream(Stream stream) : this(stream, false)
		{
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000257C4 File Offset: 0x000239C4
		public ZipInputStream(string fileName)
		{
			Stream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this._Init(stream, false, fileName);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000257F0 File Offset: 0x000239F0
		public ZipInputStream(Stream stream, bool leaveOpen)
		{
			this._Init(stream, leaveOpen, null);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00025804 File Offset: 0x00023A04
		private void _Init(Stream stream, bool leaveOpen, string name)
		{
			this._inputStream = stream;
			if (!this._inputStream.CanRead)
			{
				throw new ZipException("The stream must be readable.");
			}
			this._container = new ZipContainer(this);
			this._provisionalAlternateEncoding = Encoding.GetEncoding("IBM437");
			this._leaveUnderlyingStreamOpen = leaveOpen;
			this._findRequired = true;
			this._name = (name ?? "(stream)");
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00025874 File Offset: 0x00023A74
		public override string ToString()
		{
			return string.Format("ZipInputStream::{0}(leaveOpen({1})))", this._name, this._leaveUnderlyingStreamOpen);
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00025894 File Offset: 0x00023A94
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0002589C File Offset: 0x00023A9C
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				return this._provisionalAlternateEncoding;
			}
			set
			{
				this._provisionalAlternateEncoding = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x000258A8 File Offset: 0x00023AA8
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x000258B0 File Offset: 0x00023AB0
		public int CodecBufferSize { get; set; }

		// Token: 0x170000EB RID: 235
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x000258BC File Offset: 0x00023ABC
		public string Password
		{
			set
			{
				if (this._closed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._Password = value;
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000258E4 File Offset: 0x00023AE4
		private void SetupStream()
		{
			this._crcStream = this._currentEntry.InternalOpenReader(this._Password);
			this._LeftToRead = this._crcStream.Length;
			this._needSetup = false;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00025918 File Offset: 0x00023B18
		internal Stream ReadStream
		{
			get
			{
				return this._inputStream;
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00025920 File Offset: 0x00023B20
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._closed)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			if (this._needSetup)
			{
				this.SetupStream();
			}
			if (this._LeftToRead == 0L)
			{
				return 0;
			}
			int count2 = (this._LeftToRead > (long)count) ? count : ((int)this._LeftToRead);
			int num = this._crcStream.Read(buffer, offset, count2);
			this._LeftToRead -= (long)num;
			if (this._LeftToRead == 0L)
			{
				int crc = this._crcStream.Crc;
				this._currentEntry.VerifyCrcAfterExtract(crc);
				this._inputStream.Seek(this._endOfEntry, SeekOrigin.Begin);
			}
			return num;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000259E0 File Offset: 0x00023BE0
		public ZipEntry GetNextEntry()
		{
			if (this._findRequired)
			{
				long num = SharedUtilities.FindSignature(this._inputStream, 67324752);
				if (num == -1L)
				{
					return null;
				}
				this._inputStream.Seek(-4L, SeekOrigin.Current);
			}
			else if (this._firstEntry)
			{
				this._inputStream.Seek(this._endOfEntry, SeekOrigin.Begin);
			}
			this._currentEntry = ZipEntry.ReadEntry(this._container, !this._firstEntry);
			this._endOfEntry = this._inputStream.Position;
			this._firstEntry = true;
			this._needSetup = true;
			this._findRequired = false;
			return this._currentEntry;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00025A90 File Offset: 0x00023C90
		protected override void Dispose(bool disposing)
		{
			if (this._closed)
			{
				return;
			}
			if (disposing)
			{
				if (this._exceptionPending)
				{
					return;
				}
				if (!this._leaveUnderlyingStreamOpen)
				{
					this._inputStream.Dispose();
				}
			}
			this._closed = true;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00025AD0 File Offset: 0x00023CD0
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00025AD4 File Offset: 0x00023CD4
		public override bool CanSeek
		{
			get
			{
				return this._inputStream.CanSeek;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00025AE4 File Offset: 0x00023CE4
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00025AE8 File Offset: 0x00023CE8
		public override long Length
		{
			get
			{
				return this._inputStream.Length;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00025AF8 File Offset: 0x00023CF8
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x00025B08 File Offset: 0x00023D08
		public override long Position
		{
			get
			{
				return this._inputStream.Position;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00025B14 File Offset: 0x00023D14
		public override void Flush()
		{
			throw new NotSupportedException("Flush");
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00025B20 File Offset: 0x00023D20
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Write");
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00025B2C File Offset: 0x00023D2C
		public override long Seek(long offset, SeekOrigin origin)
		{
			this._findRequired = true;
			return this._inputStream.Seek(offset, origin);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00025B54 File Offset: 0x00023D54
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000295 RID: 661
		private Stream _inputStream;

		// Token: 0x04000296 RID: 662
		private Encoding _provisionalAlternateEncoding;

		// Token: 0x04000297 RID: 663
		private ZipEntry _currentEntry;

		// Token: 0x04000298 RID: 664
		private bool _firstEntry;

		// Token: 0x04000299 RID: 665
		private bool _needSetup;

		// Token: 0x0400029A RID: 666
		private ZipContainer _container;

		// Token: 0x0400029B RID: 667
		private CrcCalculatorStream _crcStream;

		// Token: 0x0400029C RID: 668
		private long _LeftToRead;

		// Token: 0x0400029D RID: 669
		internal string _Password;

		// Token: 0x0400029E RID: 670
		private long _endOfEntry;

		// Token: 0x0400029F RID: 671
		private string _name;

		// Token: 0x040002A0 RID: 672
		private bool _leaveUnderlyingStreamOpen;

		// Token: 0x040002A1 RID: 673
		private bool _closed;

		// Token: 0x040002A2 RID: 674
		private bool _findRequired;

		// Token: 0x040002A3 RID: 675
		private bool _exceptionPending;
	}
}
