using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000082 RID: 130
	[ComVisible(false)]
	public class DsROT
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x00012D6C File Offset: 0x00010F6C
		public static bool AddGraphToRot(object graph, ref int cookie)
		{
			cookie = 0;
			UCOMIRunningObjectTable ucomirunningObjectTable = null;
			UCOMIMoniker ucomimoniker = null;
			bool result;
			try
			{
				int num = DsROT.GetRunningObjectTable(0, ref ucomirunningObjectTable);
				int currentProcessId = DsROT.GetCurrentProcessId();
				IntPtr iunknownForObject = Marshal.GetIUnknownForObject(RuntimeHelpers.GetObjectValue(graph));
				int num2 = (int)iunknownForObject;
				Marshal.Release(iunknownForObject);
				string item = string.Format("FilterGraph {0} pid {1}", num2.ToString("x8"), currentProcessId.ToString("x8"));
				num = DsROT.CreateItemMoniker("!", item, ref ucomimoniker);
				ucomirunningObjectTable.Register(1, RuntimeHelpers.GetObjectValue(graph), ucomimoniker, out cookie);
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
			}
			finally
			{
				if (ucomimoniker != null)
				{
					Marshal.ReleaseComObject(ucomimoniker);
				}
				ucomimoniker = null;
				if (ucomirunningObjectTable != null)
				{
					Marshal.ReleaseComObject(ucomirunningObjectTable);
				}
				ucomirunningObjectTable = null;
			}
			return result;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00012E4C File Offset: 0x0001104C
		public static bool RemoveGraphFromRot(ref int cookie)
		{
			UCOMIRunningObjectTable ucomirunningObjectTable = null;
			bool result;
			try
			{
				int runningObjectTable = DsROT.GetRunningObjectTable(0, ref ucomirunningObjectTable);
				ucomirunningObjectTable.Revoke(cookie);
				cookie = 0;
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
			}
			finally
			{
				if (ucomirunningObjectTable != null)
				{
					Marshal.ReleaseComObject(ucomirunningObjectTable);
				}
				ucomirunningObjectTable = null;
			}
			return result;
		}

		// Token: 0x060002FB RID: 763
		[DllImport("ole32.dll", ExactSpelling = true)]
		private static extern int GetRunningObjectTable(int r, ref UCOMIRunningObjectTable pprot);

		// Token: 0x060002FC RID: 764
		[DllImport("ole32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		private static extern int CreateItemMoniker(string delim, string item, ref UCOMIMoniker ppmk);

		// Token: 0x060002FD RID: 765
		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern int GetCurrentProcessId();

		// Token: 0x0400024B RID: 587
		private const int ROTFLAGS_REGISTRATIONKEEPSALIVE = 1;
	}
}
