using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x020000D7 RID: 215
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000D")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public sealed class ZlibCodec
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00034580 File Offset: 0x00032780
		public int Adler32
		{
			get
			{
				return (int)this._Adler32;
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00034588 File Offset: 0x00032788
		public ZlibCodec()
		{
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000345A0 File Offset: 0x000327A0
		public ZlibCodec(CompressionMode mode)
		{
			if (mode == CompressionMode.Compress)
			{
				int num = this.InitializeDeflate();
				if (num != 0)
				{
					throw new ZlibException("Cannot initialize for deflate.");
				}
			}
			else
			{
				if (mode != CompressionMode.Decompress)
				{
					throw new ZlibException("Invalid ZlibStreamFlavor.");
				}
				int num2 = this.InitializeInflate();
				if (num2 != 0)
				{
					throw new ZlibException("Cannot initialize for inflate.");
				}
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00034610 File Offset: 0x00032810
		public int InitializeInflate()
		{
			return this.InitializeInflate(this.WindowBits);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00034620 File Offset: 0x00032820
		public int InitializeInflate(bool expectRfc1950Header)
		{
			return this.InitializeInflate(this.WindowBits, expectRfc1950Header);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00034630 File Offset: 0x00032830
		public int InitializeInflate(int windowBits)
		{
			this.WindowBits = windowBits;
			return this.InitializeInflate(windowBits, true);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00034644 File Offset: 0x00032844
		public int InitializeInflate(int windowBits, bool expectRfc1950Header)
		{
			this.WindowBits = windowBits;
			if (this.dstate != null)
			{
				throw new ZlibException("You may not call InitializeInflate() after calling InitializeDeflate().");
			}
			this.istate = new InflateManager(expectRfc1950Header);
			return this.istate.Initialize(this, windowBits);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0003467C File Offset: 0x0003287C
		public int Inflate(FlushType flush)
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Inflate(flush);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000346A0 File Offset: 0x000328A0
		public int EndInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			int result = this.istate.End();
			this.istate = null;
			return result;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000346DC File Offset: 0x000328DC
		public int SyncInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Sync();
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00034700 File Offset: 0x00032900
		public int InitializeDeflate()
		{
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0003470C File Offset: 0x0003290C
		public int InitializeDeflate(CompressionLevel level)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0003471C File Offset: 0x0003291C
		public int InitializeDeflate(CompressionLevel level, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0003472C File Offset: 0x0003292C
		public int InitializeDeflate(CompressionLevel level, int bits)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00034744 File Offset: 0x00032944
		public int InitializeDeflate(CompressionLevel level, int bits, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0003475C File Offset: 0x0003295C
		private int _InternalInitializeDeflate(bool wantRfc1950Header)
		{
			if (this.istate != null)
			{
				throw new ZlibException("You may not call InitializeDeflate() after calling InitializeInflate().");
			}
			this.dstate = new DeflateManager();
			this.dstate.WantRfc1950HeaderBytes = wantRfc1950Header;
			return this.dstate.Initialize(this, this.CompressLevel, this.WindowBits, this.Strategy);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000347B8 File Offset: 0x000329B8
		public int Deflate(FlushType flush)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.Deflate(flush);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000347DC File Offset: 0x000329DC
		public int EndDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate = null;
			return 0;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000347FC File Offset: 0x000329FC
		public void ResetDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate.Reset();
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00034820 File Offset: 0x00032A20
		public int SetDeflateParams(CompressionLevel level, CompressionStrategy strategy)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.SetParams(level, strategy);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00034848 File Offset: 0x00032A48
		public int SetDictionary(byte[] dictionary)
		{
			if (this.istate != null)
			{
				return this.istate.SetDictionary(dictionary);
			}
			if (this.dstate != null)
			{
				return this.dstate.SetDictionary(dictionary);
			}
			throw new ZlibException("No Inflate or Deflate state!");
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00034884 File Offset: 0x00032A84
		internal void flush_pending()
		{
			int num = this.dstate.pendingCount;
			if (num > this.AvailableBytesOut)
			{
				num = this.AvailableBytesOut;
			}
			if (num == 0)
			{
				return;
			}
			if (this.dstate.pending.Length <= this.dstate.nextPending || this.OutputBuffer.Length <= this.NextOut || this.dstate.pending.Length < this.dstate.nextPending + num || this.OutputBuffer.Length < this.NextOut + num)
			{
				throw new ZlibException(string.Format("Invalid State. (pending.Length={0}, pendingCount={1})", this.dstate.pending.Length, this.dstate.pendingCount));
			}
			Array.Copy(this.dstate.pending, this.dstate.nextPending, this.OutputBuffer, this.NextOut, num);
			this.NextOut += num;
			this.dstate.nextPending += num;
			this.TotalBytesOut += (long)num;
			this.AvailableBytesOut -= num;
			this.dstate.pendingCount -= num;
			if (this.dstate.pendingCount == 0)
			{
				this.dstate.nextPending = 0;
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000349E8 File Offset: 0x00032BE8
		internal int read_buf(byte[] buf, int start, int size)
		{
			int num = this.AvailableBytesIn;
			if (num > size)
			{
				num = size;
			}
			if (num == 0)
			{
				return 0;
			}
			this.AvailableBytesIn -= num;
			if (this.dstate.WantRfc1950HeaderBytes)
			{
				this._Adler32 = Adler.Adler32(this._Adler32, this.InputBuffer, this.NextIn, num);
			}
			Array.Copy(this.InputBuffer, this.NextIn, buf, start, num);
			this.NextIn += num;
			this.TotalBytesIn += (long)num;
			return num;
		}

		// Token: 0x0400046C RID: 1132
		public byte[] InputBuffer;

		// Token: 0x0400046D RID: 1133
		public int NextIn;

		// Token: 0x0400046E RID: 1134
		public int AvailableBytesIn;

		// Token: 0x0400046F RID: 1135
		public long TotalBytesIn;

		// Token: 0x04000470 RID: 1136
		public byte[] OutputBuffer;

		// Token: 0x04000471 RID: 1137
		public int NextOut;

		// Token: 0x04000472 RID: 1138
		public int AvailableBytesOut;

		// Token: 0x04000473 RID: 1139
		public long TotalBytesOut;

		// Token: 0x04000474 RID: 1140
		public string Message;

		// Token: 0x04000475 RID: 1141
		internal DeflateManager dstate;

		// Token: 0x04000476 RID: 1142
		internal InflateManager istate;

		// Token: 0x04000477 RID: 1143
		internal uint _Adler32;

		// Token: 0x04000478 RID: 1144
		public CompressionLevel CompressLevel = CompressionLevel.Default;

		// Token: 0x04000479 RID: 1145
		public int WindowBits = 15;

		// Token: 0x0400047A RID: 1146
		public CompressionStrategy Strategy;
	}
}
