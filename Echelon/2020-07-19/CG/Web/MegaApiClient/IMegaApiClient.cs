using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000EA RID: 234
	public interface IMegaApiClient
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600080F RID: 2063
		// (remove) Token: 0x06000810 RID: 2064
		event EventHandler<ApiRequestFailedEventArgs> ApiRequestFailed;

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000811 RID: 2065
		bool IsLoggedIn { get; }

		// Token: 0x06000812 RID: 2066
		MegaApiClient.LogonSessionToken Login(string email, string password);

		// Token: 0x06000813 RID: 2067
		MegaApiClient.LogonSessionToken Login(string email, string password, string mfaKey);

		// Token: 0x06000814 RID: 2068
		MegaApiClient.LogonSessionToken Login(MegaApiClient.AuthInfos authInfos);

		// Token: 0x06000815 RID: 2069
		void Login(MegaApiClient.LogonSessionToken logonSessionToken);

		// Token: 0x06000816 RID: 2070
		void Login();

		// Token: 0x06000817 RID: 2071
		void LoginAnonymous();

		// Token: 0x06000818 RID: 2072
		void Logout();

		// Token: 0x06000819 RID: 2073
		string GetRecoveryKey();

		// Token: 0x0600081A RID: 2074
		IAccountInformation GetAccountInformation();

		// Token: 0x0600081B RID: 2075
		IEnumerable<ISession> GetSessionsHistory();

		// Token: 0x0600081C RID: 2076
		IEnumerable<INode> GetNodes();

		// Token: 0x0600081D RID: 2077
		IEnumerable<INode> GetNodes(INode parent);

		// Token: 0x0600081E RID: 2078
		void Delete(INode node, bool moveToTrash = true);

		// Token: 0x0600081F RID: 2079
		INode CreateFolder(string name, INode parent);

		// Token: 0x06000820 RID: 2080
		Uri GetDownloadLink(INode node);

		// Token: 0x06000821 RID: 2081
		void DownloadFile(INode node, string outputFile, CancellationToken? cancellationToken = null);

		// Token: 0x06000822 RID: 2082
		void DownloadFile(Uri uri, string outputFile, CancellationToken? cancellationToken = null);

		// Token: 0x06000823 RID: 2083
		Stream Download(INode node, CancellationToken? cancellationToken = null);

		// Token: 0x06000824 RID: 2084
		Stream Download(Uri uri, CancellationToken? cancellationToken = null);

		// Token: 0x06000825 RID: 2085
		INodeInfo GetNodeFromLink(Uri uri);

		// Token: 0x06000826 RID: 2086
		IEnumerable<INode> GetNodesFromLink(Uri uri);

		// Token: 0x06000827 RID: 2087
		INode UploadFile(string filename, INode parent, CancellationToken? cancellationToken = null);

		// Token: 0x06000828 RID: 2088
		INode Upload(Stream stream, string name, INode parent, DateTime? modificationDate = null, CancellationToken? cancellationToken = null);

		// Token: 0x06000829 RID: 2089
		INode Move(INode node, INode destinationParentNode);

		// Token: 0x0600082A RID: 2090
		INode Rename(INode node, string newName);

		// Token: 0x0600082B RID: 2091
		MegaApiClient.AuthInfos GenerateAuthInfos(string email, string password, string mfaKey = null);
	}
}
