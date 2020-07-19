using System;
using System.Collections.Generic;
using System.Linq;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000F6 RID: 246
	public static class NodeExtensions
	{
		// Token: 0x060008B7 RID: 2231 RVA: 0x0003BB7C File Offset: 0x00039D7C
		public static long GetFolderSize(this INodeInfo node, IMegaApiClient client)
		{
			IEnumerable<INode> nodes = client.GetNodes();
			return node.GetFolderSize(nodes);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0003BB9C File Offset: 0x00039D9C
		public static long GetFolderSize(this INodeInfo node, IEnumerable<INode> allNodes)
		{
			if (node.Type == NodeType.File)
			{
				throw new InvalidOperationException("node is not a Directory");
			}
			long num = 0L;
			foreach (INode node2 in from x in allNodes
			where x.ParentId == node.Id
			select x)
			{
				if (node2.Type == NodeType.File)
				{
					num += node2.Size;
				}
				else if (node2.Type == NodeType.Directory)
				{
					long folderSize = node2.GetFolderSize(allNodes);
					num += folderSize;
				}
			}
			return num;
		}
	}
}
