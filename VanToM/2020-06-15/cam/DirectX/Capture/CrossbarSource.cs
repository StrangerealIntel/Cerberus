using System;
using System.Runtime.InteropServices;
using cam.DShowNET;

namespace cam.DirectX.Capture
{
	// Token: 0x02000044 RID: 68
	public class CrossbarSource : Source
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00012314 File Offset: 0x00010514
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0001234C File Offset: 0x0001054C
		public override bool Enabled
		{
			get
			{
				int num;
				return this.Crossbar.get_IsRoutedTo(this.OutputPin, out num) == 0 && this.InputPin == num;
			}
			set
			{
				if (value)
				{
					int num = this.Crossbar.Route(this.OutputPin, this.InputPin);
				}
				else
				{
					int num2 = this.Crossbar.Route(this.OutputPin, -1);
				}
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00012390 File Offset: 0x00010590
		internal CrossbarSource(IAMCrossbar crossbar, int outputPin, int inputPin, PhysicalConnectorType connectorType)
		{
			this.Crossbar = crossbar;
			this.OutputPin = outputPin;
			this.InputPin = inputPin;
			this.ConnectorType = connectorType;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000123B8 File Offset: 0x000105B8
		private string getName(PhysicalConnectorType connectorType)
		{
			string result;
			if (connectorType == PhysicalConnectorType.Video_Tuner)
			{
				result = "Video Tuner";
			}
			else if (connectorType == PhysicalConnectorType.Video_Composite)
			{
				result = "Video Composite";
			}
			else if (connectorType == PhysicalConnectorType.Video_SVideo)
			{
				result = "Video S-Video";
			}
			else if (connectorType == PhysicalConnectorType.Video_RGB)
			{
				result = "Video RGB";
			}
			else if (connectorType == PhysicalConnectorType.Video_YRYBY)
			{
				result = "Video YRYBY";
			}
			else if (connectorType == PhysicalConnectorType.Video_SerialDigital)
			{
				result = "Video Serial Digital";
			}
			else if (connectorType == PhysicalConnectorType.Video_ParallelDigital)
			{
				result = "Video Parallel Digital";
			}
			else if (connectorType == PhysicalConnectorType.Video_SCSI)
			{
				result = "Video SCSI";
			}
			else if (connectorType == PhysicalConnectorType.Video_AUX)
			{
				result = "Video AUX";
			}
			else if (connectorType == PhysicalConnectorType.Video_1394)
			{
				result = "Video Firewire";
			}
			else if (connectorType == PhysicalConnectorType.Video_USB)
			{
				result = "Video USB";
			}
			else if (connectorType == PhysicalConnectorType.Video_VideoDecoder)
			{
				result = "Video Decoder";
			}
			else if (connectorType == PhysicalConnectorType.Video_VideoEncoder)
			{
				result = "Video Encoder";
			}
			else if (connectorType == PhysicalConnectorType.Video_SCART)
			{
				result = "Video SCART";
			}
			else if (connectorType == PhysicalConnectorType.Audio_Tuner)
			{
				result = "Audio Tuner";
			}
			else if (connectorType == PhysicalConnectorType.Audio_Line)
			{
				result = "Audio Line In";
			}
			else if (connectorType == PhysicalConnectorType.Audio_Mic)
			{
				result = "Audio Mic";
			}
			else if (connectorType == PhysicalConnectorType.Audio_AESDigital)
			{
				result = "Audio AES Digital";
			}
			else if (connectorType == PhysicalConnectorType.Audio_SPDIFDigital)
			{
				result = "Audio SPDIF Digital";
			}
			else if (connectorType == PhysicalConnectorType.Audio_SCSI)
			{
				result = "Audio SCSI";
			}
			else if (connectorType == PhysicalConnectorType.Audio_AUX)
			{
				result = "Audio AUX";
			}
			else if (connectorType == PhysicalConnectorType.Audio_1394)
			{
				result = "Audio Firewire";
			}
			else if (connectorType == PhysicalConnectorType.Audio_USB)
			{
				result = "Audio USB";
			}
			else if (connectorType == PhysicalConnectorType.Audio_AudioDecoder)
			{
				result = "Audio Decoder";
			}
			else
			{
				result = "Unknown Connector";
			}
			return result;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000125AC File Offset: 0x000107AC
		public override void Dispose()
		{
			if (this.Crossbar != null)
			{
				Marshal.ReleaseComObject(this.Crossbar);
			}
			this.Crossbar = null;
			base.Dispose();
		}

		// Token: 0x0400016E RID: 366
		internal IAMCrossbar Crossbar;

		// Token: 0x0400016F RID: 367
		internal int OutputPin;

		// Token: 0x04000170 RID: 368
		internal int InputPin;

		// Token: 0x04000171 RID: 369
		internal PhysicalConnectorType ConnectorType;
	}
}
