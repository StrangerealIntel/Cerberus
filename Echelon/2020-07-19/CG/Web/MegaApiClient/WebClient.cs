using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000FD RID: 253
	public class WebClient : IWebClient
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x0003C628 File Offset: 0x0003A828
		public WebClient(int responseTimeout = -1, string userAgent = null)
		{
			this.BufferSize = 65536;
			this.responseTimeout = responseTimeout;
			this.userAgent = (userAgent ?? this.GenerateUserAgent());
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0003C668 File Offset: 0x0003A868
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x0003C670 File Offset: 0x0003A870
		public int BufferSize { get; set; }

		// Token: 0x060008F9 RID: 2297 RVA: 0x0003C67C File Offset: 0x0003A87C
		public string PostRequestJson(Uri url, string jsonData)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream(jsonData.ToBytes()))
			{
				result = this.PostRequest(url, memoryStream, "application/json");
			}
			return result;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0003C6C8 File Offset: 0x0003A8C8
		public string PostRequestRaw(Uri url, Stream dataStream)
		{
			return this.PostRequest(url, dataStream, "application/octet-stream");
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0003C6D8 File Offset: 0x0003A8D8
		public Stream GetRequestRaw(Uri url)
		{
			HttpWebRequest httpWebRequest = this.CreateRequest(url);
			httpWebRequest.Method = "GET";
			return httpWebRequest.GetResponse().GetResponseStream();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0003C708 File Offset: 0x0003A908
		private string PostRequest(Uri url, Stream dataStream, string contentType)
		{
			HttpWebRequest httpWebRequest = this.CreateRequest(url);
			httpWebRequest.ContentLength = dataStream.Length;
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = contentType;
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				dataStream.Position = 0L;
				dataStream.CopyTo(requestStream, this.BufferSize);
			}
			string result;
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				using (Stream responseStream = httpWebResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			return result;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0003C7F8 File Offset: 0x0003A9F8
		private HttpWebRequest CreateRequest(Uri url)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Timeout = this.responseTimeout;
			httpWebRequest.UserAgent = this.userAgent;
			return httpWebRequest;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0003C820 File Offset: 0x0003AA20
		private string GenerateUserAgent()
		{
			AssemblyName name = Assembly.GetExecutingAssembly().GetName();
			return string.Format("{0} v{1}", name.Name, name.Version.ToString(2));
		}

		// Token: 0x04000533 RID: 1331
		private const int DefaultResponseTimeout = -1;

		// Token: 0x04000534 RID: 1332
		private readonly int responseTimeout;

		// Token: 0x04000535 RID: 1333
		private readonly string userAgent;
	}
}
