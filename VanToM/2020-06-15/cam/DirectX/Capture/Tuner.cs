using System;
using System.Runtime.InteropServices;
using cam.DShowNET;

namespace cam.DirectX.Capture
{
	// Token: 0x020000A3 RID: 163
	public class Tuner : IDisposable
	{
		// Token: 0x0600036F RID: 879 RVA: 0x00014598 File Offset: 0x00012798
		public Tuner(IAMTVTuner tuner__1)
		{
			this.tvTuner = null;
			this.tvTuner = tuner__1;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000370 RID: 880 RVA: 0x000145B0 File Offset: 0x000127B0
		// (set) Token: 0x06000371 RID: 881 RVA: 0x000145D0 File Offset: 0x000127D0
		public int Channel
		{
			get
			{
				int result;
				int num2;
				int num3;
				int num = this.tvTuner.get_Channel(ref result, ref num2, ref num3);
				return result;
			}
			set
			{
				int num = this.tvTuner.put_Channel(value, AMTunerSubChannel.Default, AMTunerSubChannel.Default);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000372 RID: 882 RVA: 0x000145EC File Offset: 0x000127EC
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00014610 File Offset: 0x00012810
		public TunerInputType InputType
		{
			get
			{
				IAMTVTuner iamtvtuner = this.tvTuner;
				int lIndex = 0;
				TunerInputType tunerInputType2;
				TunerInputType tunerInputType = (TunerInputType)tunerInputType2;
				int num = iamtvtuner.get_InputType(lIndex, ref tunerInputType);
				tunerInputType2 = (TunerInputType)tunerInputType;
				return tunerInputType2;
			}
			set
			{
				int num = this.tvTuner.put_InputType(0, (TunerInputType)value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00014630 File Offset: 0x00012830
		public bool SignalPresent
		{
			get
			{
				AMTunerSignalStrength amtunerSignalStrength;
				int num = this.tvTuner.SignalPresent(ref amtunerSignalStrength);
				return amtunerSignalStrength == AMTunerSignalStrength.SignalPresent;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00014650 File Offset: 0x00012850
		public void Dispose()
		{
			if (this.tvTuner != null)
			{
				Marshal.ReleaseComObject(this.tvTuner);
			}
			this.tvTuner = null;
		}

		// Token: 0x040002B1 RID: 689
		protected IAMTVTuner tvTuner;
	}
}
