namespace Stub
{
	// Token: 0x02000036 RID: 54
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class Form3 : global::System.Windows.Forms.Form
	{
		// Token: 0x0600012E RID: 302 RVA: 0x0000F028 File Offset: 0x0000D228
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

		// Token: 0x0600012F RID: 303 RVA: 0x0000F088 File Offset: 0x0000D288
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.Sendbox = new global::System.Windows.Forms.TextBox();
			this.Recv = new global::System.Windows.Forms.TextBox();
			this.TextBox1 = new global::System.Windows.Forms.TextBox();
			this.SuspendLayout();
			this.Sendbox.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			global::System.Windows.Forms.Control sendbox = this.Sendbox;
			global::System.Drawing.Point location = new global::System.Drawing.Point(0, 201);
			sendbox.Location = location;
			this.Sendbox.Name = "Sendbox";
			global::System.Windows.Forms.Control sendbox2 = this.Sendbox;
			global::System.Drawing.Size size = new global::System.Drawing.Size(441, 20);
			sendbox2.Size = size;
			this.Sendbox.TabIndex = 11;
			this.Recv.BackColor = global::System.Drawing.SystemColors.MenuText;
			this.Recv.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.Recv.Font = new global::System.Drawing.Font("Tahoma", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Recv.ForeColor = global::System.Drawing.Color.Lime;
			global::System.Windows.Forms.Control recv = this.Recv;
			location = new global::System.Drawing.Point(0, 0);
			recv.Location = location;
			this.Recv.Multiline = true;
			this.Recv.Name = "Recv";
			this.Recv.ReadOnly = true;
			this.Recv.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			global::System.Windows.Forms.Control recv2 = this.Recv;
			size = new global::System.Drawing.Size(441, 207);
			recv2.Size = size;
			this.Recv.TabIndex = 10;
			this.Recv.TabStop = false;
			global::System.Windows.Forms.Control textBox = this.TextBox1;
			location = new global::System.Drawing.Point(123, 270);
			textBox.Location = location;
			this.TextBox1.Name = "TextBox1";
			global::System.Windows.Forms.Control textBox2 = this.TextBox1;
			size = new global::System.Drawing.Size(194, 20);
			textBox2.Size = size;
			this.TextBox1.TabIndex = 12;
			this.TextBox1.Visible = false;
			global::System.Drawing.SizeF autoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			this.AutoScaleDimensions = autoScaleDimensions;
			this.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			size = new global::System.Drawing.Size(441, 221);
			this.ClientSize = size;
			this.ControlBox = false;
			this.Controls.Add(this.TextBox1);
			this.Controls.Add(this.Sendbox);
			this.Controls.Add(this.Recv);
			this.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "Form3";
			this.ShowIcon = false;
			this.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Chat";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		// Token: 0x04000114 RID: 276
		private global::System.ComponentModel.IContainer components;
	}
}
