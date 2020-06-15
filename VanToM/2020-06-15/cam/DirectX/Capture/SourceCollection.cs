using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using cam.DShowNET;

namespace cam.DirectX.Capture
{
	// Token: 0x020000A1 RID: 161
	public class SourceCollection : CollectionBase, IDisposable
	{
		// Token: 0x06000364 RID: 868 RVA: 0x00014118 File Offset: 0x00012318
		internal SourceCollection()
		{
			this.InnerList.Capacity = 1;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001412C File Offset: 0x0001232C
		internal SourceCollection(ICaptureGraphBuilder2 graphBuilder, IBaseFilter deviceFilter, bool isVideoDevice)
		{
			this.addFromGraph(graphBuilder, deviceFilter, isVideoDevice);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00014140 File Offset: 0x00012340
		~SourceCollection()
		{
			this.Dispose();
		}

		// Token: 0x17000033 RID: 51
		public Source this[int index]
		{
			get
			{
				return (Source)this.InnerList[index];
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00014184 File Offset: 0x00012384
		// (set) Token: 0x06000369 RID: 873 RVA: 0x000141F8 File Offset: 0x000123F8
		internal Source CurrentSource
		{
			get
			{
				try
				{
					foreach (object obj in this.InnerList)
					{
						Source source = (Source)obj;
						if (source.Enabled)
						{
							return source;
						}
					}
				}
				finally
				{
					IEnumerator enumerator;
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					try
					{
						foreach (object obj in this.InnerList)
						{
							Source source = (Source)obj;
							source.Enabled = false;
						}
						return;
					}
					finally
					{
						IEnumerator enumerator;
						if (enumerator is IDisposable)
						{
							(enumerator as IDisposable).Dispose();
						}
					}
				}
				if (value is CrossbarSource)
				{
					value.Enabled = true;
				}
				else
				{
					try
					{
						foreach (object obj2 in this.InnerList)
						{
							Source source2 = (Source)obj2;
							source2.Enabled = false;
						}
					}
					finally
					{
						IEnumerator enumerator2;
						if (enumerator2 is IDisposable)
						{
							(enumerator2 as IDisposable).Dispose();
						}
					}
					value.Enabled = true;
				}
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000142DC File Offset: 0x000124DC
		public new void Clear()
		{
			int num = 0;
			checked
			{
				int num2 = this.InnerList.Count - 1;
				for (int i = num; i <= num2; i++)
				{
					this[i].Dispose();
				}
				this.InnerList.Clear();
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00014320 File Offset: 0x00012520
		public void Dispose()
		{
			this.Clear();
			this.InnerList.Capacity = 1;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00014334 File Offset: 0x00012534
		protected void addFromGraph(ICaptureGraphBuilder2 graphBuilder, IBaseFilter deviceFilter, bool isVideoDevice)
		{
			Trace.Assert(graphBuilder != null);
			ArrayList arrayList = this.findCrossbars(graphBuilder, deviceFilter);
			try
			{
				foreach (object obj in arrayList)
				{
					IAMCrossbar crossbar = (IAMCrossbar)obj;
					ArrayList c = this.findCrossbarSources(graphBuilder, crossbar, isVideoDevice);
					this.InnerList.AddRange(c);
				}
			}
			finally
			{
				IEnumerator enumerator;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000143B8 File Offset: 0x000125B8
		protected ArrayList findCrossbars(ICaptureGraphBuilder2 graphBuilder, IBaseFilter deviceFilter)
		{
			ArrayList arrayList = new ArrayList();
			Guid upstreamOnly = FindDirection.UpstreamOnly;
			Guid guid = default(Guid);
			Guid guid2 = typeof(IAMCrossbar).GUID;
			object obj = null;
			object obj2 = null;
			int num = graphBuilder.FindInterface(ref upstreamOnly, ref guid, deviceFilter, ref guid2, out obj);
			while (num == 0 && obj != null)
			{
				if (obj is IAMCrossbar)
				{
					arrayList.Add(obj as IAMCrossbar);
					num = graphBuilder.FindInterface(ref upstreamOnly, ref guid, obj as IBaseFilter, ref guid2, out obj2);
					obj = RuntimeHelpers.GetObjectValue(obj2);
				}
				else
				{
					obj = null;
				}
			}
			return arrayList;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00014454 File Offset: 0x00012654
		protected ArrayList findCrossbarSources(ICaptureGraphBuilder2 graphBuilder, IAMCrossbar crossbar, bool isVideoDevice)
		{
			ArrayList arrayList = new ArrayList();
			int num2;
			int num3;
			int num = crossbar.get_PinCounts(out num2, out num3);
			int num4 = 0;
			checked
			{
				int num5 = num2 - 1;
				for (int i = num4; i <= num5; i++)
				{
					int num6 = 0;
					int num7 = num3 - 1;
					for (int j = num6; j <= num7; j++)
					{
						num = crossbar.CanRoute(i, j);
						if (num == 0)
						{
							int num8;
							PhysicalConnectorType physicalConnectorType;
							num = crossbar.get_CrossbarPinInfo(true, j, out num8, out physicalConnectorType);
							CrossbarSource value = new CrossbarSource(crossbar, i, j, physicalConnectorType);
							if (physicalConnectorType < PhysicalConnectorType.Audio_Tuner)
							{
								if (isVideoDevice)
								{
									arrayList.Add(value);
								}
								else if (!isVideoDevice)
								{
									arrayList.Add(value);
								}
							}
						}
					}
				}
				int k = 0;
				while (k < arrayList.Count)
				{
					bool flag = false;
					CrossbarSource crossbarSource = (CrossbarSource)arrayList[k];
					int num9 = 0;
					int num10 = arrayList.Count - 1;
					for (int l = num9; l <= num10; l++)
					{
						CrossbarSource crossbarSource2 = (CrossbarSource)arrayList[l];
						if (crossbarSource.OutputPin == crossbarSource2.OutputPin && k != l)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						k++;
					}
					else
					{
						arrayList.RemoveAt(k);
					}
				}
				return arrayList;
			}
		}
	}
}
