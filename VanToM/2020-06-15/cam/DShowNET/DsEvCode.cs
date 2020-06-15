using System;

namespace cam.DShowNET
{
	// Token: 0x02000050 RID: 80
	public enum DsEvCode
	{
		// Token: 0x04000178 RID: 376
		None,
		// Token: 0x04000179 RID: 377
		Complete,
		// Token: 0x0400017A RID: 378
		UserAbort,
		// Token: 0x0400017B RID: 379
		ErrorAbort,
		// Token: 0x0400017C RID: 380
		Time,
		// Token: 0x0400017D RID: 381
		Repaint,
		// Token: 0x0400017E RID: 382
		StErrStopped,
		// Token: 0x0400017F RID: 383
		StErrStPlaying,
		// Token: 0x04000180 RID: 384
		ErrorStPlaying,
		// Token: 0x04000181 RID: 385
		PaletteChanged,
		// Token: 0x04000182 RID: 386
		VideoSizeChanged,
		// Token: 0x04000183 RID: 387
		QualityChange,
		// Token: 0x04000184 RID: 388
		ShuttingDown,
		// Token: 0x04000185 RID: 389
		ClockChanged,
		// Token: 0x04000186 RID: 390
		Paused,
		// Token: 0x04000187 RID: 391
		OpeningFile = 16,
		// Token: 0x04000188 RID: 392
		BufferingData,
		// Token: 0x04000189 RID: 393
		FullScreenLost,
		// Token: 0x0400018A RID: 394
		Activate,
		// Token: 0x0400018B RID: 395
		NeedRestart,
		// Token: 0x0400018C RID: 396
		WindowDestroyed,
		// Token: 0x0400018D RID: 397
		DisplayChanged,
		// Token: 0x0400018E RID: 398
		Starvation,
		// Token: 0x0400018F RID: 399
		OleEvent,
		// Token: 0x04000190 RID: 400
		NotifyWindow,
		// Token: 0x04000191 RID: 401
		DvdDomChange = 257,
		// Token: 0x04000192 RID: 402
		DvdTitleChange,
		// Token: 0x04000193 RID: 403
		DvdChaptStart,
		// Token: 0x04000194 RID: 404
		DvdAudioStChange,
		// Token: 0x04000195 RID: 405
		DvdSubPicStChange,
		// Token: 0x04000196 RID: 406
		DvdAngleChange,
		// Token: 0x04000197 RID: 407
		DvdButtonChange,
		// Token: 0x04000198 RID: 408
		DvdValidUopsChange,
		// Token: 0x04000199 RID: 409
		DvdStillOn,
		// Token: 0x0400019A RID: 410
		DvdStillOff,
		// Token: 0x0400019B RID: 411
		DvdCurrentTime,
		// Token: 0x0400019C RID: 412
		DvdError,
		// Token: 0x0400019D RID: 413
		DvdWarning,
		// Token: 0x0400019E RID: 414
		DvdChaptAutoStop,
		// Token: 0x0400019F RID: 415
		DvdNoFpPgc,
		// Token: 0x040001A0 RID: 416
		DvdPlaybRateChange,
		// Token: 0x040001A1 RID: 417
		DvdParentalLChange,
		// Token: 0x040001A2 RID: 418
		DvdPlaybStopped,
		// Token: 0x040001A3 RID: 419
		DvdAnglesAvail,
		// Token: 0x040001A4 RID: 420
		DvdPeriodAStop,
		// Token: 0x040001A5 RID: 421
		DvdButtonAActivated,
		// Token: 0x040001A6 RID: 422
		DvdCmdStart,
		// Token: 0x040001A7 RID: 423
		DvdCmdEnd,
		// Token: 0x040001A8 RID: 424
		DvdDiscEjected,
		// Token: 0x040001A9 RID: 425
		DvdDiscInserted,
		// Token: 0x040001AA RID: 426
		DvdCurrentHmsfTime,
		// Token: 0x040001AB RID: 427
		DvdKaraokeMode
	}
}
