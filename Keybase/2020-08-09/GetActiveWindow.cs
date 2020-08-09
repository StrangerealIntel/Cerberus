using System;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x02000008 RID: 8
internal class GetActiveWindow
{
	// Token: 0x0600002E RID: 46
	[DllImport("user32", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public static extern IntPtr GetForegroundWindow();

	// Token: 0x0600002F RID: 47
	[DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int cch);

	// Token: 0x06000030 RID: 48 RVA: 0x00002A9C File Offset: 0x00001A9C
	public string GetCaption()
	{
		StringBuilder stringBuilder = new StringBuilder(256);
		IntPtr foregroundWindow = GetActiveWindow.GetForegroundWindow();
		GetActiveWindow.GetWindowText(foregroundWindow, stringBuilder, stringBuilder.Capacity);
		return stringBuilder.ToString();
	}

	// Token: 0x04000022 RID: 34
	private string makel;
}
