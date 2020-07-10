using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MufMaOSvGyvz
{
	// Token: 0x0200001E RID: 30
	public class Shortcut
	{
		// Token: 0x060000A0 RID: 160
		public static void CreateShortcut(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6)
		{
			Shortcut.FBqQxbujAkO fbqQxbujAkO = (Shortcut.FBqQxbujAkO)Shortcut.guALPVXzZrXCxQGy.InvokeMember(MainCore.DecodeBase64("Q3JlYXRlU2hvcnRjdXQ="), BindingFlags.InvokeMethod, null, Shortcut.GOgcrtCCxONESyk, new object[]
			{ // -> CreateShortcut
				string_0
			});
			fbqQxbujAkO.KvNZJAMjCVsdYlb = string_4;
			fbqQxbujAkO.kvXUHrmDwjogj = string_5;
			fbqQxbujAkO.xAvWKKHxzJVu = string_1;
			fbqQxbujAkO.SjUoxQLIQfmnVDK = string_3;
			fbqQxbujAkO.peMgjfIzVGWqUKjBsb = string_2;
			if (!string.IsNullOrEmpty(string_6))
			{
				fbqQxbujAkO.uthEHKoaLAbUg = string_6;
			}
			fbqQxbujAkO.Save();
		}

		// Token: 0x04000082 RID: 130
		private static Type guALPVXzZrXCxQGy = Type.GetTypeFromProgID(MainCore.DecodeBase64("V1NjcmlwdC5TaGVsbA==")); // -> WScript.Shell

		// Token: 0x04000083 RID: 131
		private static object GOgcrtCCxONESyk = Activator.CreateInstance(Shortcut.guALPVXzZrXCxQGy);

		// Token: 0x0200001F RID: 31
		[TypeLibType(4160)]
		[Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B")]
		[ComImport]
		private interface FBqQxbujAkO
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x060000A3 RID: 163
			[DispId(0)]
			string APmUqWqGqXhZg { [DispId(0)] [return: MarshalAs(UnmanagedType.BStr)] get; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x060000A4 RID: 164
			// (set) Token: 0x060000A5 RID: 165
			[DispId(1000)]
			string peMgjfIzVGWqUKjBsb { [DispId(1000)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1000)] [param: MarshalAs(UnmanagedType.BStr)] set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x060000A6 RID: 166
			// (set) Token: 0x060000A7 RID: 167
			[DispId(1001)]
			string KvNZJAMjCVsdYlb { [DispId(1001)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1001)] [param: MarshalAs(UnmanagedType.BStr)] set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x060000A8 RID: 168
			// (set) Token: 0x060000A9 RID: 169
			[DispId(1002)]
			string kvXUHrmDwjogj { [DispId(1002)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1002)] [param: MarshalAs(UnmanagedType.BStr)] set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x060000AA RID: 170
			// (set) Token: 0x060000AB RID: 171
			[DispId(1003)]
			string uthEHKoaLAbUg { [DispId(1003)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1003)] [param: MarshalAs(UnmanagedType.BStr)] set; }

			// Token: 0x17000008 RID: 8
			// (set) Token: 0x060000AC RID: 172
			[DispId(1004)]
			string ELHnWikFFpqxRW { [DispId(1004)] [param: MarshalAs(UnmanagedType.BStr)] set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x060000AD RID: 173
			// (set) Token: 0x060000AE RID: 174
			[DispId(1005)]
			string xAvWKKHxzJVu { [DispId(1005)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1005)] [param: MarshalAs(UnmanagedType.BStr)] set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x060000AF RID: 175
			// (set) Token: 0x060000B0 RID: 176
			[DispId(1006)]
			int wvirdNYNMobjS { [DispId(1006)] get; [DispId(1006)] set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x060000B1 RID: 177
			// (set) Token: 0x060000B2 RID: 178
			[DispId(1007)]
			string SjUoxQLIQfmnVDK { [DispId(1007)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1007)] [param: MarshalAs(UnmanagedType.BStr)] set; }

			// Token: 0x060000B3 RID: 179
			[TypeLibFunc(64)]
			[DispId(2000)]
			void Load([MarshalAs(UnmanagedType.BStr)] [In] string PathLink);

			// Token: 0x060000B4 RID: 180
			[DispId(2001)]
			void Save();
		}
	}
}
