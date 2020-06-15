using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using cam.DShowNET;

namespace cam.DirectX.Capture
{
	// Token: 0x0200009A RID: 154
	public class PropertyPageCollection : CollectionBase, IDisposable
	{
		// Token: 0x06000349 RID: 841 RVA: 0x00013B94 File Offset: 0x00011D94
		internal PropertyPageCollection()
		{
			this.InnerList.Capacity = 1;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00013BA8 File Offset: 0x00011DA8
		internal PropertyPageCollection(ICaptureGraphBuilder2 graphBuilder, IBaseFilter videoDeviceFilter, IBaseFilter audioDeviceFilter, IBaseFilter videoCompressorFilter, IBaseFilter audioCompressorFilter, SourceCollection videoSources, SourceCollection audioSources)
		{
			this.addFromGraph(graphBuilder, videoDeviceFilter, audioDeviceFilter, videoCompressorFilter, audioCompressorFilter, videoSources, audioSources);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00013BCC File Offset: 0x00011DCC
		~PropertyPageCollection()
		{
			this.Dispose();
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00013BFC File Offset: 0x00011DFC
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

		// Token: 0x0600034D RID: 845 RVA: 0x00013C40 File Offset: 0x00011E40
		public void Dispose()
		{
			this.Clear();
			this.InnerList.Capacity = 1;
		}

		// Token: 0x17000030 RID: 48
		public PropertyPage this[int index]
		{
			get
			{
				return (PropertyPage)this.InnerList[index];
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00013C68 File Offset: 0x00011E68
		protected void addFromGraph(ICaptureGraphBuilder2 graphBuilder, IBaseFilter videoDeviceFilter, IBaseFilter audioDeviceFilter, IBaseFilter videoCompressorFilter, IBaseFilter audioCompressorFilter, SourceCollection videoSources, SourceCollection audioSources)
		{
			object obj = null;
			Trace.Assert(graphBuilder != null);
			this.addIfSupported(videoDeviceFilter, "Video Capture Device");
			Guid guid = PinCategory.Capture;
			Guid guid2 = MediaType.Interleaved;
			Guid guid3 = typeof(IAMStreamConfig).GUID;
			int num = graphBuilder.FindInterface(ref guid, ref guid2, videoDeviceFilter, ref guid3, out obj);
			if (num != 0)
			{
				guid2 = MediaType.Video;
				num = graphBuilder.FindInterface(ref guid, ref guid2, videoDeviceFilter, ref guid3, out obj);
				if (num != 0)
				{
					obj = null;
				}
			}
			this.addIfSupported(RuntimeHelpers.GetObjectValue(obj), "Video Capture Pin");
			guid = PinCategory.Preview;
			guid2 = MediaType.Interleaved;
			guid3 = typeof(IAMStreamConfig).GUID;
			num = graphBuilder.FindInterface(ref guid, ref guid2, videoDeviceFilter, ref guid3, out obj);
			if (num != 0)
			{
				guid2 = MediaType.Video;
				num = graphBuilder.FindInterface(ref guid, ref guid2, videoDeviceFilter, ref guid3, out obj);
				if (num != 0)
				{
					obj = null;
				}
			}
			this.addIfSupported(RuntimeHelpers.GetObjectValue(obj), "Video Preview Pin");
			ArrayList arrayList = new ArrayList();
			int num2 = 1;
			int num3 = 0;
			checked
			{
				int num4 = videoSources.Count - 1;
				for (int i = num3; i <= num4; i++)
				{
					CrossbarSource crossbarSource = videoSources[i] as CrossbarSource;
					if (crossbarSource != null && arrayList.IndexOf(crossbarSource.Crossbar) < 0)
					{
						arrayList.Add(crossbarSource.Crossbar);
						if (this.addIfSupported(crossbarSource.Crossbar, "Video Crossbar " + ((num2 == 1) ? "" : num2.ToString())))
						{
							num2++;
						}
					}
				}
				arrayList.Clear();
				this.addIfSupported(videoCompressorFilter, "Video Compressor");
				guid = PinCategory.Capture;
				guid2 = MediaType.Interleaved;
				guid3 = typeof(IAMTVTuner).GUID;
				num = graphBuilder.FindInterface(ref guid, ref guid2, videoDeviceFilter, ref guid3, out obj);
				if (num != 0)
				{
					guid2 = MediaType.Video;
					num = graphBuilder.FindInterface(ref guid, ref guid2, videoDeviceFilter, ref guid3, out obj);
					if (num != 0)
					{
						obj = null;
					}
				}
				this.addIfSupported(RuntimeHelpers.GetObjectValue(obj), "TV Tuner");
				IAMVfwCompressDialogs iamvfwCompressDialogs = videoCompressorFilter as IAMVfwCompressDialogs;
				if (iamvfwCompressDialogs != null)
				{
					VfwCompressorPropertyPage value = new VfwCompressorPropertyPage("Video Compressor", iamvfwCompressDialogs);
					this.InnerList.Add(value);
				}
				this.addIfSupported(audioDeviceFilter, "Audio Capture Device");
				guid = PinCategory.Capture;
				guid2 = MediaType.Audio;
				guid3 = typeof(IAMStreamConfig).GUID;
				num = graphBuilder.FindInterface(ref guid, ref guid2, audioDeviceFilter, ref guid3, out obj);
				if (num != 0)
				{
					obj = null;
				}
				this.addIfSupported(RuntimeHelpers.GetObjectValue(obj), "Audio Capture Pin");
				guid = PinCategory.Preview;
				guid2 = MediaType.Audio;
				guid3 = typeof(IAMStreamConfig).GUID;
				num = graphBuilder.FindInterface(ref guid, ref guid2, audioDeviceFilter, ref guid3, out obj);
				if (num != 0)
				{
					obj = null;
				}
				this.addIfSupported(RuntimeHelpers.GetObjectValue(obj), "Audio Preview Pin");
				num2 = 1;
				int num5 = 0;
				int num6 = audioSources.Count - 1;
				for (int j = num5; j <= num6; j++)
				{
					CrossbarSource crossbarSource2 = audioSources[j] as CrossbarSource;
					if (crossbarSource2 != null && arrayList.IndexOf(crossbarSource2.Crossbar) < 0)
					{
						arrayList.Add(crossbarSource2.Crossbar);
						if (this.addIfSupported(crossbarSource2.Crossbar, "Audio Crossbar " + ((num2 == 1) ? "" : num2.ToString())))
						{
							num2++;
						}
					}
				}
				arrayList.Clear();
				this.addIfSupported(audioCompressorFilter, "Audio Compressor");
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00013FF8 File Offset: 0x000121F8
		protected bool addIfSupported(object o, string name)
		{
			ISpecifyPropertyPages specifyPropertyPages = null;
			DsCAUUID dsCAUUID = default(DsCAUUID);
			bool result = false;
			try
			{
				specifyPropertyPages = (o as ISpecifyPropertyPages);
				if (specifyPropertyPages != null)
				{
					int pages = specifyPropertyPages.GetPages(ref dsCAUUID);
					if (pages != 0 || dsCAUUID.cElems <= 0)
					{
						specifyPropertyPages = null;
					}
				}
			}
			finally
			{
				if (dsCAUUID.pElems != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(dsCAUUID.pElems);
				}
			}
			if (specifyPropertyPages != null)
			{
				DirectShowPropertyPage value = new DirectShowPropertyPage(name, specifyPropertyPages);
				this.InnerList.Add(value);
				result = true;
			}
			return result;
		}
	}
}
