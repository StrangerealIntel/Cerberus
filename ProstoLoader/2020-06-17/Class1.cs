using System;
using System.Windows.Forms;

// Token: 0x02000029 RID: 41
internal static class Class1
{
	// Token: 0x0600043F RID: 1087 RVA: 0x000039FF File Offset: 0x00001BFF
	[STAThread]
	private static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new Form1());
	}
}
