using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using cam.DShowNET;
using Microsoft.VisualBasic.CompilerServices;

namespace cam.DirectX.Capture
{
	// Token: 0x02000041 RID: 65
	public class Capture : ISampleGrabberCB
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00010B34 File Offset: 0x0000ED34
		public bool Capturing
		{
			get
			{
				return this.zgraphState == Capture.GraphState.Capturing;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00010B40 File Offset: 0x0000ED40
		public bool Cued
		{
			get
			{
				return this.isCaptureRendered && this.zgraphState == Capture.GraphState.Rendered;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00010B5C File Offset: 0x0000ED5C
		public bool Stopped
		{
			get
			{
				return this.zgraphState != Capture.GraphState.Capturing;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00010B6C File Offset: 0x0000ED6C
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00010B74 File Offset: 0x0000ED74
		public string Filename
		{
			get
			{
				return this.m_filename;
			}
			set
			{
				this.assertStopped();
				this.m_filename = value;
				if (this.fileWriterFilter != null)
				{
					AMMediaType ammediaType = new AMMediaType();
					string text;
					int num = this.fileWriterFilter.GetCurFile(out text, ammediaType);
					if (ammediaType.formatSize > 0)
					{
						Marshal.FreeCoTaskMem(ammediaType.formatPtr);
					}
					num = this.fileWriterFilter.SetFileName(this.m_filename, ammediaType);
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00010BD8 File Offset: 0x0000EDD8
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00010BE0 File Offset: 0x0000EDE0
		public Control PreviewWindow
		{
			get
			{
				return this.m_previewWindow;
			}
			set
			{
				this.assertStopped();
				this.derenderGraph();
				this.m_previewWindow = value;
				this.wantPreviewRendered = (this.m_previewWindow != null && this.m_videoDevice != null);
				this.renderGraph();
				this.startPreviewIfNeeded();
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00010C30 File Offset: 0x0000EE30
		public VideoCapabilities VideoCaps
		{
			get
			{
				if (this.m_videoCaps == null && this.videoStreamConfig != null)
				{
					try
					{
						this.m_videoCaps = new VideoCapabilities(this.videoStreamConfig);
					}
					catch (Exception ex)
					{
					}
				}
				return this.m_videoCaps;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00010C90 File Offset: 0x0000EE90
		public Filter VideoDevice
		{
			get
			{
				return this.m_videoDevice;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00010C98 File Offset: 0x0000EE98
		public Filter AudioDevice
		{
			get
			{
				return this.m_audioDevice;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00010CA0 File Offset: 0x0000EEA0
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		public Filter VideoCompressor
		{
			get
			{
				return this.m_videoCompressor;
			}
			set
			{
				this.assertStopped();
				this.destroyGraph();
				this.m_videoCompressor = value;
				this.renderGraph();
				this.startPreviewIfNeeded();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00010CCC File Offset: 0x0000EECC
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00010CD4 File Offset: 0x0000EED4
		public Filter AudioCompressor
		{
			get
			{
				return this.m_audioCompressor;
			}
			set
			{
				this.assertStopped();
				this.destroyGraph();
				this.m_audioCompressor = value;
				this.renderGraph();
				this.startPreviewIfNeeded();
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00010CF8 File Offset: 0x0000EEF8
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00010D08 File Offset: 0x0000EF08
		public Source VideoSource
		{
			get
			{
				return this.VideoSources.CurrentSource;
			}
			set
			{
				this.VideoSources.CurrentSource = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00010D18 File Offset: 0x0000EF18
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00010D28 File Offset: 0x0000EF28
		public Source AudioSource
		{
			get
			{
				return this.AudioSources.CurrentSource;
			}
			set
			{
				this.AudioSources.CurrentSource = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00010D38 File Offset: 0x0000EF38
		public SourceCollection VideoSources
		{
			get
			{
				if (this.m_videoSources == null)
				{
					try
					{
						if (this.m_videoDevice != null)
						{
							this.m_videoSources = new SourceCollection(this.captureGraphBuilder, this.videoDeviceFilter, true);
						}
						else
						{
							this.m_videoSources = new SourceCollection();
						}
					}
					catch (Exception ex)
					{
					}
				}
				return this.m_videoSources;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		public SourceCollection AudioSources
		{
			get
			{
				if (this.m_audioSources == null)
				{
					try
					{
						if (this.m_audioDevice != null)
						{
							this.m_audioSources = new SourceCollection(this.captureGraphBuilder, this.audioDeviceFilter, false);
						}
						else
						{
							this.m_audioSources = new SourceCollection();
						}
					}
					catch (Exception ex)
					{
					}
				}
				return this.m_audioSources;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00010E28 File Offset: 0x0000F028
		public PropertyPageCollection PropertyPages
		{
			get
			{
				if (this.m_propertyPages == null)
				{
					try
					{
						this.m_propertyPages = new PropertyPageCollection(this.captureGraphBuilder, this.videoDeviceFilter, this.audioDeviceFilter, this.videoCompressorFilter, this.audioCompressorFilter, this.VideoSources, this.AudioSources);
					}
					catch (Exception ex)
					{
					}
				}
				return this.m_propertyPages;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		public Tuner Tuner
		{
			get
			{
				return this.m_tuner;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00010EAC File Offset: 0x0000F0AC
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00010EDC File Offset: 0x0000F0DC
		public double FrameRate
		{
			get
			{
				long num = Conversions.ToLong(this.getStreamConfigSetting(this.videoStreamConfig, "AvgTimePerFrame"));
				return 10000000.0 / (double)num;
			}
			set
			{
				long num = checked((long)Math.Round(Math.Truncate(10000000.0 / value)));
				this.setStreamConfigSetting(this.videoStreamConfig, "AvgTimePerFrame", num);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00010F18 File Offset: 0x0000F118
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00010F58 File Offset: 0x0000F158
		public Size FrameSize
		{
			get
			{
				BitmapInfoHeader bitmapInfoHeader = (BitmapInfoHeader)this.getStreamConfigSetting(this.videoStreamConfig, "BmiHeader");
				Size result = new Size(bitmapInfoHeader.Width, bitmapInfoHeader.Height);
				return result;
			}
			set
			{
				BitmapInfoHeader bitmapInfoHeader = (BitmapInfoHeader)this.getStreamConfigSetting(this.videoStreamConfig, "BmiHeader");
				bitmapInfoHeader.Width = value.Width;
				bitmapInfoHeader.Height = value.Height;
				this.setStreamConfigSetting(this.videoStreamConfig, "BmiHeader", bitmapInfoHeader);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00010FB8 File Offset: 0x0000F1B8
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00010FE0 File Offset: 0x0000F1E0
		public short AudioChannels
		{
			get
			{
				return Conversions.ToShort(this.getStreamConfigSetting(this.audioStreamConfig, "nChannels"));
			}
			set
			{
				this.setStreamConfigSetting(this.audioStreamConfig, "nChannels", value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00010FFC File Offset: 0x0000F1FC
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00011024 File Offset: 0x0000F224
		public int AudioSamplingRate
		{
			get
			{
				return Conversions.ToInteger(this.getStreamConfigSetting(this.audioStreamConfig, "nSamplesPerSec"));
			}
			set
			{
				this.setStreamConfigSetting(this.audioStreamConfig, "nSamplesPerSec", value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00011040 File Offset: 0x0000F240
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00011068 File Offset: 0x0000F268
		public short AudioSampleSize
		{
			get
			{
				return Conversions.ToShort(this.getStreamConfigSetting(this.audioStreamConfig, "wBitsPerSample"));
			}
			set
			{
				this.setStreamConfigSetting(this.audioStreamConfig, "wBitsPerSample", value);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000187 RID: 391 RVA: 0x00011084 File Offset: 0x0000F284
		// (remove) Token: 0x06000188 RID: 392 RVA: 0x000110A0 File Offset: 0x0000F2A0
		public event Capture.HeFrame FrameEvent2;

		// Token: 0x06000189 RID: 393 RVA: 0x000110BC File Offset: 0x0000F2BC
		public Capture(Filter videoDevice, Filter audioDevice)
		{
			this.zgraphState = Capture.GraphState.Null;
			this.isPreviewRendered = false;
			this.isCaptureRendered = false;
			this.wantPreviewRendered = false;
			this.wantCaptureRendered = false;
			this.rotCookie = 0;
			this.m_videoDevice = null;
			this.m_audioDevice = null;
			this.m_videoCompressor = null;
			this.m_audioCompressor = null;
			this.m_filename = "";
			this.m_previewWindow = null;
			this.m_videoCaps = null;
			this.m_videoSources = null;
			this.m_audioSources = null;
			this.m_propertyPages = null;
			this.m_tuner = null;
			this.captureGraphBuilder = null;
			this.videoStreamConfig = null;
			this.audioStreamConfig = null;
			this.videoDeviceFilter = null;
			this.videoCompressorFilter = null;
			this.audioDeviceFilter = null;
			this.audioCompressorFilter = null;
			this.muxFilter = null;
			this.fileWriterFilter = null;
			this.sampGrabber = null;
			this.m_videoDevice = videoDevice;
			this.m_audioDevice = audioDevice;
			this.Filename = this.getTempFilename();
			this.createGraph();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000111B0 File Offset: 0x0000F3B0
		~Capture()
		{
			this.Dispose();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000111E0 File Offset: 0x0000F3E0
		public void Cue()
		{
			this.assertStopped();
			this.wantCaptureRendered = true;
			this.renderGraph();
			int num = this.mediaControl.Pause();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0001120C File Offset: 0x0000F40C
		public void Start()
		{
			this.assertStopped();
			this.wantCaptureRendered = true;
			this.renderGraph();
			int num = this.mediaControl.Run();
			this.zgraphState = Capture.GraphState.Capturing;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00011240 File Offset: 0x0000F440
		public void Stop()
		{
			this.wantCaptureRendered = false;
			if (this.mediaControl != null)
			{
				this.mediaControl.Stop();
			}
			if (this.zgraphState == Capture.GraphState.Capturing)
			{
				this.zgraphState = Capture.GraphState.Rendered;
			}
			try
			{
				this.renderGraph();
			}
			catch (Exception ex)
			{
			}
			try
			{
				this.startPreviewIfNeeded();
			}
			catch (Exception ex2)
			{
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000112D0 File Offset: 0x0000F4D0
		public void Dispose()
		{
			this.wantPreviewRendered = false;
			this.wantCaptureRendered = false;
			try
			{
				this.destroyGraph();
			}
			catch (Exception ex)
			{
			}
			if (this.m_videoSources != null)
			{
				this.m_videoSources.Dispose();
			}
			this.m_videoSources = null;
			if (this.m_audioSources != null)
			{
				this.m_audioSources.Dispose();
			}
			this.m_audioSources = null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00011350 File Offset: 0x0000F550
		protected void createGraph()
		{
			if (this.zgraphState < Capture.GraphState.Created)
			{
				GC.Collect();
				this.graphBuilder = (IGraphBuilder)Activator.CreateInstance(Type.GetTypeFromCLSID(Clsid.FilterGraph, true));
				Guid captureGraphBuilder = Clsid.CaptureGraphBuilder2;
				Guid guid = typeof(ICaptureGraphBuilder2).GUID;
				this.captureGraphBuilder = (ICaptureGraphBuilder2)DsBugWO.CreateDsInstance(ref captureGraphBuilder, ref guid);
				int num = this.captureGraphBuilder.SetFiltergraph(this.graphBuilder);
				Type typeFromCLSID = Type.GetTypeFromCLSID(Clsid.SampleGrabber);
				object objectValue = RuntimeHelpers.GetObjectValue(Activator.CreateInstance(typeFromCLSID));
				this.sampGrabber = (ISampleGrabber)objectValue;
				this.baseGrabFlt = (IBaseFilter)this.sampGrabber;
				AMMediaType ammediaType = new AMMediaType();
				if (this.VideoDevice != null)
				{
					this.videoDeviceFilter = (IBaseFilter)Marshal.BindToMoniker(this.VideoDevice.MonikerString);
					num = this.graphBuilder.AddFilter(this.videoDeviceFilter, "Video Capture Device");
					ammediaType.majorType = MediaType.Video;
					ammediaType.subType = MediaSubType.RGB24;
					ammediaType.formatType = FormatType.VideoInfo;
					num = this.sampGrabber.SetMediaType(ammediaType);
					num = this.graphBuilder.AddFilter(this.baseGrabFlt, "Ds.NET Grabber");
				}
				if (this.AudioDevice != null)
				{
					this.audioDeviceFilter = (IBaseFilter)Marshal.BindToMoniker(this.AudioDevice.MonikerString);
					num = this.graphBuilder.AddFilter(this.audioDeviceFilter, "Audio Capture Device");
				}
				if (this.VideoCompressor != null)
				{
					this.videoCompressorFilter = (IBaseFilter)Marshal.BindToMoniker(this.VideoCompressor.MonikerString);
					num = this.graphBuilder.AddFilter(this.videoCompressorFilter, "Video Compressor");
				}
				if (this.AudioCompressor != null)
				{
					this.audioCompressorFilter = (IBaseFilter)Marshal.BindToMoniker(this.AudioCompressor.MonikerString);
					num = this.graphBuilder.AddFilter(this.audioCompressorFilter, "Audio Compressor");
				}
				Guid capture = PinCategory.Capture;
				Guid guid2 = MediaType.Interleaved;
				Guid guid3 = typeof(IAMStreamConfig).GUID;
				object obj;
				num = this.captureGraphBuilder.FindInterface(ref capture, ref guid2, this.videoDeviceFilter, ref guid3, out obj);
				if (num != 0)
				{
					guid2 = MediaType.Video;
					num = this.captureGraphBuilder.FindInterface(ref capture, ref guid2, this.videoDeviceFilter, ref guid3, out obj);
					if (num != 0)
					{
						obj = null;
					}
				}
				this.videoStreamConfig = (obj as IAMStreamConfig);
				obj = null;
				capture = PinCategory.Capture;
				guid2 = MediaType.Audio;
				guid3 = typeof(IAMStreamConfig).GUID;
				num = this.captureGraphBuilder.FindInterface(ref capture, ref guid2, this.audioDeviceFilter, ref guid3, out obj);
				if (num != 0)
				{
					obj = null;
				}
				this.audioStreamConfig = (obj as IAMStreamConfig);
				this.mediaControl = (IMediaControl)this.graphBuilder;
				if (this.m_videoSources != null)
				{
					this.m_videoSources.Dispose();
				}
				this.m_videoSources = null;
				if (this.m_audioSources != null)
				{
					this.m_audioSources.Dispose();
				}
				this.m_audioSources = null;
				if (this.m_propertyPages != null)
				{
					this.m_propertyPages.Dispose();
				}
				this.m_propertyPages = null;
				this.m_videoCaps = null;
				obj = null;
				capture = PinCategory.Capture;
				guid2 = MediaType.Interleaved;
				guid3 = typeof(IAMTVTuner).GUID;
				num = this.captureGraphBuilder.FindInterface(ref capture, ref guid2, this.videoDeviceFilter, ref guid3, out obj);
				if (num != 0)
				{
					guid2 = MediaType.Video;
					num = this.captureGraphBuilder.FindInterface(ref capture, ref guid2, this.videoDeviceFilter, ref guid3, out obj);
					if (num != 0)
					{
						obj = null;
					}
				}
				IAMTVTuner iamtvtuner = obj as IAMTVTuner;
				if (iamtvtuner != null)
				{
					this.m_tuner = new Tuner(iamtvtuner);
				}
				this.videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(ammediaType.formatPtr, typeof(VideoInfoHeader));
				Marshal.FreeCoTaskMem(ammediaType.formatPtr);
				ammediaType.formatPtr = IntPtr.Zero;
				num = this.sampGrabber.SetBufferSamples(false);
				if (num == 0)
				{
					num = this.sampGrabber.SetOneShot(false);
				}
				if (num == 0)
				{
					num = this.sampGrabber.SetCallback(null, 0);
				}
				this.zgraphState = Capture.GraphState.Created;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00011794 File Offset: 0x0000F994
		protected void renderGraph()
		{
			bool flag = false;
			this.assertStopped();
			if (this.mediaControl != null)
			{
				this.mediaControl.Stop();
			}
			this.createGraph();
			if (!this.wantPreviewRendered && this.isPreviewRendered)
			{
				this.derenderGraph();
			}
			if (!this.wantCaptureRendered && this.isCaptureRendered && this.wantPreviewRendered)
			{
				this.derenderGraph();
			}
			if (this.wantCaptureRendered && !this.isCaptureRendered)
			{
				Guid avi = MediaSubType.Avi;
				int num = this.captureGraphBuilder.SetOutputFileName(ref avi, this.Filename, out this.muxFilter, out this.fileWriterFilter);
				if (this.VideoDevice != null)
				{
					Guid guid = PinCategory.Capture;
					Guid guid2 = MediaType.Interleaved;
					num = this.captureGraphBuilder.RenderStream(ref guid, ref guid2, this.videoDeviceFilter, this.videoCompressorFilter, this.muxFilter);
					if (num < 0)
					{
						guid2 = MediaType.Video;
						num = this.captureGraphBuilder.RenderStream(ref guid, ref guid2, this.videoDeviceFilter, this.videoCompressorFilter, this.muxFilter);
					}
				}
				if (this.AudioDevice != null)
				{
					Guid guid = PinCategory.Capture;
					Guid guid2 = MediaType.Audio;
					num = this.captureGraphBuilder.RenderStream(ref guid, ref guid2, this.audioDeviceFilter, this.audioCompressorFilter, this.muxFilter);
				}
				this.isCaptureRendered = true;
				flag = true;
			}
			if (this.wantPreviewRendered && !this.isPreviewRendered)
			{
				Guid guid = PinCategory.Preview;
				Guid guid2 = MediaType.Video;
				int num = this.captureGraphBuilder.RenderStream(ref guid, ref guid2, this.videoDeviceFilter, this.baseGrabFlt, null);
				this.videoWindow = (IVideoWindow)this.graphBuilder;
				num = this.videoWindow.put_Owner(this.m_previewWindow.Handle);
				num = this.videoWindow.put_WindowStyle(1174405120);
				this.m_previewWindow.Resize += this.onPreviewWindowResize;
				this.onPreviewWindowResize(this, null);
				num = this.videoWindow.put_Visible(-1);
				this.isPreviewRendered = true;
				flag = true;
				AMMediaType ammediaType = new AMMediaType();
				num = this.sampGrabber.GetConnectedMediaType(ammediaType);
				this.videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(ammediaType.formatPtr, typeof(VideoInfoHeader));
				Marshal.FreeCoTaskMem(ammediaType.formatPtr);
				ammediaType.formatPtr = IntPtr.Zero;
			}
			if (flag)
			{
				this.zgraphState = Capture.GraphState.Rendered;
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000119FC File Offset: 0x0000FBFC
		protected void startPreviewIfNeeded()
		{
			if (this.wantPreviewRendered && this.isPreviewRendered && !this.isCaptureRendered)
			{
				this.mediaControl.Run();
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00011A2C File Offset: 0x0000FC2C
		protected void derenderGraph()
		{
			if (this.mediaControl != null)
			{
				this.mediaControl.Stop();
			}
			if (this.videoWindow != null)
			{
				this.videoWindow.put_Visible(0);
				this.videoWindow.put_Owner(IntPtr.Zero);
				this.videoWindow = null;
			}
			if (this.PreviewWindow != null)
			{
				this.m_previewWindow.Resize -= this.onPreviewWindowResize;
			}
			if (this.zgraphState >= Capture.GraphState.Rendered)
			{
				this.zgraphState = Capture.GraphState.Created;
				this.isCaptureRendered = false;
				this.isPreviewRendered = false;
				if (this.videoDeviceFilter != null)
				{
					this.removeDownstream(this.videoDeviceFilter, this.m_videoCompressor == null);
				}
				if (this.audioDeviceFilter != null)
				{
					this.removeDownstream(this.audioDeviceFilter, this.m_audioCompressor == null);
				}
				this.muxFilter = null;
				this.fileWriterFilter = null;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00011B14 File Offset: 0x0000FD14
		protected void removeDownstream(IBaseFilter filter, bool removeFirstFilter)
		{
			IEnumPins enumPins;
			int num = filter.EnumPins(out enumPins);
			enumPins.Reset();
			if (num == 0 && enumPins != null)
			{
				IPin[] array = new IPin[1];
				do
				{
					int num2;
					num = enumPins.Next(1, array, out num2);
					if (num == 0 && array[0] != null)
					{
						IPin pin = null;
						array[0].ConnectedTo(out pin);
						if (pin != null)
						{
							PinInfo pinInfo = default(PinInfo);
							num = pin.QueryPinInfo(out pinInfo);
							if (num == 0 && pinInfo.dir == PinDirection.Input)
							{
								this.removeDownstream(pinInfo.filter, true);
								this.graphBuilder.Disconnect(pin);
								this.graphBuilder.Disconnect(array[0]);
								if (pinInfo.filter == this.videoCompressorFilter && pinInfo.filter == this.audioCompressorFilter)
								{
									this.graphBuilder.RemoveFilter(pinInfo.filter);
								}
							}
							Marshal.ReleaseComObject(pinInfo.filter);
							Marshal.ReleaseComObject(pin);
						}
						Marshal.ReleaseComObject(array[0]);
					}
				}
				while (num == 0);
				Marshal.ReleaseComObject(enumPins);
				enumPins = null;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00011C2C File Offset: 0x0000FE2C
		protected void destroyGraph()
		{
			try
			{
				this.derenderGraph();
			}
			catch (Exception ex)
			{
			}
			this.zgraphState = Capture.GraphState.Null;
			this.isCaptureRendered = false;
			this.isPreviewRendered = false;
			if (this.rotCookie != 0)
			{
				DsROT.RemoveGraphFromRot(ref this.rotCookie);
				this.rotCookie = 0;
			}
			if (this.muxFilter != null)
			{
				this.graphBuilder.RemoveFilter(this.muxFilter);
			}
			if (this.videoCompressorFilter != null)
			{
				this.graphBuilder.RemoveFilter(this.videoCompressorFilter);
			}
			if (this.audioCompressorFilter != null)
			{
				this.graphBuilder.RemoveFilter(this.audioCompressorFilter);
			}
			if (this.videoDeviceFilter != null)
			{
				this.graphBuilder.RemoveFilter(this.videoDeviceFilter);
			}
			if (this.audioDeviceFilter != null)
			{
				this.graphBuilder.RemoveFilter(this.audioDeviceFilter);
			}
			if (this.m_videoSources != null)
			{
				this.m_videoSources.Dispose();
			}
			this.m_videoSources = null;
			if (this.m_audioSources != null)
			{
				this.m_audioSources.Dispose();
			}
			this.m_audioSources = null;
			if (this.m_propertyPages != null)
			{
				this.m_propertyPages.Dispose();
			}
			this.m_propertyPages = null;
			if (this.m_tuner != null)
			{
				this.m_tuner.Dispose();
			}
			this.m_tuner = null;
			if (this.graphBuilder != null)
			{
				Marshal.ReleaseComObject(this.graphBuilder);
			}
			this.graphBuilder = null;
			if (this.captureGraphBuilder != null)
			{
				Marshal.ReleaseComObject(this.captureGraphBuilder);
			}
			this.captureGraphBuilder = null;
			if (this.muxFilter != null)
			{
				Marshal.ReleaseComObject(this.muxFilter);
			}
			this.muxFilter = null;
			if (this.fileWriterFilter != null)
			{
				Marshal.ReleaseComObject(this.fileWriterFilter);
			}
			this.fileWriterFilter = null;
			if (this.videoDeviceFilter != null)
			{
				Marshal.ReleaseComObject(this.videoDeviceFilter);
			}
			this.videoDeviceFilter = null;
			if (this.audioDeviceFilter != null)
			{
				Marshal.ReleaseComObject(this.audioDeviceFilter);
			}
			this.audioDeviceFilter = null;
			if (this.videoCompressorFilter != null)
			{
				Marshal.ReleaseComObject(this.videoCompressorFilter);
			}
			this.videoCompressorFilter = null;
			if (this.audioCompressorFilter != null)
			{
				Marshal.ReleaseComObject(this.audioCompressorFilter);
			}
			this.audioCompressorFilter = null;
			this.mediaControl = null;
			this.videoWindow = null;
			GC.Collect();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00011EA0 File Offset: 0x000100A0
		protected void onPreviewWindowResize(object sender, EventArgs e)
		{
			if (this.videoWindow != null)
			{
				Rectangle clientRectangle = this.m_previewWindow.ClientRectangle;
				this.videoWindow.SetWindowPosition(0, 0, clientRectangle.Right, clientRectangle.Bottom);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00011EE0 File Offset: 0x000100E0
		protected string getTempFilename()
		{
			checked
			{
				string text;
				try
				{
					int num = 0;
					Random random = new Random();
					string tempPath = Path.GetTempPath();
					do
					{
						text = Path.Combine(tempPath, random.Next().ToString("X") + ".avi");
						num++;
						if (num > 100)
						{
						}
					}
					while (File.Exists(text));
				}
				catch (Exception ex)
				{
					text = "c:\temp.avi";
				}
				return text;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00011F64 File Offset: 0x00010164
		protected object getStreamConfigSetting(IAMStreamConfig streamConfig, string fieldName)
		{
			this.assertStopped();
			this.derenderGraph();
			object result = null;
			IntPtr zero = IntPtr.Zero;
			AMMediaType ammediaType = new AMMediaType();
			try
			{
				int format = streamConfig.GetFormat(out zero);
				Marshal.PtrToStructure(zero, ammediaType);
				object obj;
				if (ammediaType.formatType == FormatType.WaveEx)
				{
					obj = new WaveFormatEx();
				}
				else if (ammediaType.formatType == FormatType.VideoInfo)
				{
					obj = new VideoInfoHeader();
				}
				else if (ammediaType.formatType == FormatType.VideoInfo2)
				{
					obj = new VideoInfoHeader2();
				}
				Marshal.PtrToStructure(ammediaType.formatPtr, RuntimeHelpers.GetObjectValue(obj));
				Type type = obj.GetType();
				FieldInfo field = type.GetField(fieldName);
				result = RuntimeHelpers.GetObjectValue(field.GetValue(RuntimeHelpers.GetObjectValue(obj)));
			}
			finally
			{
				DsUtils.FreeAMMediaType(ammediaType);
				Marshal.FreeCoTaskMem(zero);
			}
			this.renderGraph();
			this.startPreviewIfNeeded();
			return result;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00012064 File Offset: 0x00010264
		protected object setStreamConfigSetting(IAMStreamConfig streamConfig, string fieldName, object newValue)
		{
			this.assertStopped();
			this.derenderGraph();
			object result = null;
			IntPtr zero = IntPtr.Zero;
			AMMediaType ammediaType = new AMMediaType();
			try
			{
				int num = streamConfig.GetFormat(out zero);
				Marshal.PtrToStructure(zero, ammediaType);
				object obj;
				if (ammediaType.formatType == FormatType.WaveEx)
				{
					obj = new WaveFormatEx();
				}
				else if (ammediaType.formatType == FormatType.VideoInfo)
				{
					obj = new VideoInfoHeader();
				}
				else if (ammediaType.formatType == FormatType.VideoInfo2)
				{
					obj = new VideoInfoHeader2();
				}
				Marshal.PtrToStructure(ammediaType.formatPtr, RuntimeHelpers.GetObjectValue(obj));
				Type type = obj.GetType();
				FieldInfo field = type.GetField(fieldName);
				field.SetValue(RuntimeHelpers.GetObjectValue(obj), RuntimeHelpers.GetObjectValue(newValue));
				Marshal.StructureToPtr(RuntimeHelpers.GetObjectValue(obj), ammediaType.formatPtr, false);
				num = streamConfig.SetFormat(ammediaType);
			}
			finally
			{
				DsUtils.FreeAMMediaType(ammediaType);
				Marshal.FreeCoTaskMem(zero);
			}
			this.renderGraph();
			this.startPreviewIfNeeded();
			return result;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0001217C File Offset: 0x0001037C
		protected void assertStopped()
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00012180 File Offset: 0x00010380
		int ISampleGrabberCB.ISampleGrabberCB_SampleCB(double SampleTime, IMediaSample pSample)
		{
			Trace.Write("Sample");
			return 0;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00012190 File Offset: 0x00010390
		int ISampleGrabberCB.ISampleGrabberCB_BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
		{
			checked
			{
				try
				{
					this.bufferedSize = BufferLen;
					int width = this.videoInfoHeader.BmiHeader.Width;
					int height = this.videoInfoHeader.BmiHeader.Height;
					int num = width * 3;
					Marshal.Copy(pBuffer, this.savedArray, 0, BufferLen);
					GCHandle gchandle = GCHandle.Alloc(this.savedArray, GCHandleType.Pinned);
					int num2 = (int)gchandle.AddrOfPinnedObject();
					num2 += (height - 1) * num;
					Bitmap setBitmap = new Bitmap(width, height, 0 - num, PixelFormat.Format24bppRgb, (IntPtr)num2);
					gchandle.Free();
					this.SetBitmap = setBitmap;
				}
				catch (Exception ex)
				{
				}
				return 0;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0001224C File Offset: 0x0001044C
		private void OnCaptureDone()
		{
			Trace.WriteLine("!!DLG: OnCaptureDone");
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00012258 File Offset: 0x00010458
		public void GrapImg()
		{
			Trace.Write("IMG");
			try
			{
				if (this.savedArray == null)
				{
					int imageSize = this.videoInfoHeader.BmiHeader.ImageSize;
					if (imageSize < 1000 || imageSize > 16000000)
					{
						return;
					}
					this.savedArray = new byte[checked(imageSize + 63999 + 1)];
				}
				this.sampGrabber.SetCallback(this, 1);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x1700002C RID: 44
		// (set) Token: 0x0600019E RID: 414 RVA: 0x000122F0 File Offset: 0x000104F0
		public Bitmap SetBitmap
		{
			set
			{
				Capture.HeFrame frameEvent2Event = this.FrameEvent2Event;
				if (frameEvent2Event != null)
				{
					frameEvent2Event(value);
				}
			}
		}

		// Token: 0x04000146 RID: 326
		protected Capture.GraphState zgraphState;

		// Token: 0x04000147 RID: 327
		protected bool isPreviewRendered;

		// Token: 0x04000148 RID: 328
		protected bool isCaptureRendered;

		// Token: 0x04000149 RID: 329
		protected bool wantPreviewRendered;

		// Token: 0x0400014A RID: 330
		protected bool wantCaptureRendered;

		// Token: 0x0400014B RID: 331
		protected int rotCookie;

		// Token: 0x0400014C RID: 332
		protected Filter m_videoDevice;

		// Token: 0x0400014D RID: 333
		protected Filter m_audioDevice;

		// Token: 0x0400014E RID: 334
		protected Filter m_videoCompressor;

		// Token: 0x0400014F RID: 335
		protected Filter m_audioCompressor;

		// Token: 0x04000150 RID: 336
		protected string m_filename;

		// Token: 0x04000151 RID: 337
		protected Control m_previewWindow;

		// Token: 0x04000152 RID: 338
		protected VideoCapabilities m_videoCaps;

		// Token: 0x04000153 RID: 339
		protected SourceCollection m_videoSources;

		// Token: 0x04000154 RID: 340
		protected SourceCollection m_audioSources;

		// Token: 0x04000155 RID: 341
		protected PropertyPageCollection m_propertyPages;

		// Token: 0x04000156 RID: 342
		protected Tuner m_tuner;

		// Token: 0x04000157 RID: 343
		protected IGraphBuilder graphBuilder;

		// Token: 0x04000158 RID: 344
		protected IMediaControl mediaControl;

		// Token: 0x04000159 RID: 345
		protected IVideoWindow videoWindow;

		// Token: 0x0400015A RID: 346
		protected ICaptureGraphBuilder2 captureGraphBuilder;

		// Token: 0x0400015B RID: 347
		protected IAMStreamConfig videoStreamConfig;

		// Token: 0x0400015C RID: 348
		protected IAMStreamConfig audioStreamConfig;

		// Token: 0x0400015D RID: 349
		protected IBaseFilter videoDeviceFilter;

		// Token: 0x0400015E RID: 350
		protected IBaseFilter videoCompressorFilter;

		// Token: 0x0400015F RID: 351
		protected IBaseFilter audioDeviceFilter;

		// Token: 0x04000160 RID: 352
		protected IBaseFilter audioCompressorFilter;

		// Token: 0x04000161 RID: 353
		protected IBaseFilter muxFilter;

		// Token: 0x04000162 RID: 354
		protected IFileSinkFilter fileWriterFilter;

		// Token: 0x04000163 RID: 355
		private IBaseFilter baseGrabFlt;

		// Token: 0x04000164 RID: 356
		protected ISampleGrabber sampGrabber;

		// Token: 0x04000165 RID: 357
		private VideoInfoHeader videoInfoHeader;

		// Token: 0x04000167 RID: 359
		private byte[] savedArray;

		// Token: 0x04000168 RID: 360
		private int bufferedSize;

		// Token: 0x02000042 RID: 66
		protected enum GraphState
		{
			// Token: 0x0400016A RID: 362
			Null,
			// Token: 0x0400016B RID: 363
			Created,
			// Token: 0x0400016C RID: 364
			Rendered,
			// Token: 0x0400016D RID: 365
			Capturing
		}

		// Token: 0x02000043 RID: 67
		// (Invoke) Token: 0x060001A2 RID: 418
		public delegate void HeFrame(Bitmap BM);
	}
}
