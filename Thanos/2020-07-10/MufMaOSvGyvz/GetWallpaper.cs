using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;

namespace MufMaOSvGyvz
{
	// Token: 0x0200000F RID: 15
	public sealed class GetWallpaper
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002557 File Offset: 0x00000757
		private GetWallpaper()
		{
		}

		// Token: 0x06000042 RID: 66
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern int SystemParametersInfo(int int_0, int int_1, string string_0, int int_2);

		// Token: 0x06000043 RID: 67
		public static void DownloadWallpaper(Uri uri_0)
		{
			try
			{
				Stream stream = new WebClient().OpenRead(uri_0.ToString());
				Image image = Image.FromStream(stream);
				string text = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
				image.Save(text, ImageFormat.Bmp);
				GetWallpaper.SystemParametersInfo(20, 0, text, 3);
				Thread.Sleep(5000);
			}
			catch (Exception)
			{
			}
		}
	}
}
