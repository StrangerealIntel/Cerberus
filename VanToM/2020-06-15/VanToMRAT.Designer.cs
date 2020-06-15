namespace Stub
{
	// Token: 0x02000030 RID: 48
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class VanToMRAT : global::System.Windows.Forms.Form
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00009EEC File Offset: 0x000080EC
		[global::System.Diagnostics.DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				bool flag = disposing && this.components != null;
				if (flag)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00009F4C File Offset: 0x0000814C
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.Timer1 = new global::System.Windows.Forms.Timer(this.components);
			this.Timer2 = new global::System.Windows.Forms.Timer(this.components);
			this.Timer3 = new global::System.Windows.Forms.Timer(this.components);
			this.Timer4 = new global::System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			this.Timer2.Enabled = true;
			this.Timer2.Interval = 50;
			global::System.Drawing.SizeF autoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			this.AutoScaleDimensions = autoScaleDimensions;
			this.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			global::System.Drawing.Size clientSize = new global::System.Drawing.Size(10, 10);
			this.ClientSize = clientSize;
			this.ControlBox = false;
			this.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			global::Stub.VanToMRAT.name = "VanToMRAT";
			this.Opacity = 0.0;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "VanToM RAT";
			this.ResumeLayout(false);
		}

		// Token: 0x040000CA RID: 202
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400010E RID: 270
		public static string name;
	}
}
