# VCN64Config Editor

Each N64 game requires a configuration file to function properly in the Wii U Virtual Console, this program makes it easy to create and edit these configuration files.

The Wii U N64 Virtual Console configuration file is the one found in "WiiUN64Game\content\config\\[rom name].ini" where [rom name] is the name of the file found in "WiiUN64Game\content\rom" folder.

## Template of a configuration file

```
;Game Name
[RomOption]
;AIIntPerFrame = 0
;AISetControl = 0
;AISetDAC = 0
;AISetRateBit = 0
;AIUseTimer = 0
;BackupSize = 
;BackupType = 
;BootEnd = 0
;BootPCChange = 0
;CmpBlockAdvFlag = 0
;EEROMInitValue = 0xFF
;g_nN64CpuCmpBlockAdvFlag = 0
;MemPak = 0
;NoCntPak = 0
;PDFURL = ""
;PlayerNum = 4
;RamSize = 0x400000
;RetraceByVsync = 0
;RomType = 0
;RSPAMultiCoreWait = 0
;RSPMultiCore = 0
;RSPMultiCoreInt = 0
;RSPMultiCoreWait = 0
;Rumble = 0
;ScreenCaptureNG = 0
;TicksPerFrame = 788000
;TimeIntDelay = 0
;TLBMissEnable = 0
;TPak = 0
;TrueBoot = 0
;UseTimer = 0

[Render]
;bCutClip = 0
;bForce720P = 0
;CalculateLOD = 0
;CanvasWidth = 854
;CanvasHeight = 480
;CheckTlutValid = 0
;ClearVertexBuf = 0
;ClipTop = 0
;ClipRight = 0
;ClipBottom = 0
;ClipLeft = 0
;ConstValue0 = 0x0
;ConstValue1 = 0x0
;ConstValue2 = 0x0
;ConstValue3 = 0x0
;CopyAlphaForceOne = 0
;CopyColorAfterTask = 0
;CopyColorBuffer = 0
;CopyDepthBuffer = 0
;CopyMiddleBuffer = 0
;DepthCompare = 0
;DepthCompareLess = 0
;DepthCompareMore = 0
;DoubleFillCheck = 0
;FirstFrameAt = 1000
;FlushMemEachTask = 0
;FogVertexAlpha = 0
;ForceFilterPoint = 0
;ForceRectFilterPoint = 0
;FrameClearCacheInit = 0
;InitPerspectiveMode = 0
;NeedPreParse = 0
;NeedTileSizeCheck = 0
;PolygonOffset = 0
;PreparseTMEMBlock = 0
;RendererReset = 0
;TexEdgeAlpha = 0
;TileSizeCheckSpecial = 0
;TLUTCheck = 0
;UseColorDither = 0
;useViewportXScale = 0
;useViewportYScale = 0
;useViewportZScale = 0
;XClip = 0
;YClip = 0
;ZClip = 0

[Sound]
;BufFull = 0x0
;BufHalf = 0x0
;BufHave = 0x0
;FillAfterVCM = 0
;Resample = 0

[Input]
;AlwaysHave = 0
;STICK_CLAMP = 0
;StickLimit = 100
;StickModify = 0
;VPAD_STICK_CLAMP = 0

[RSPG]
;GTaskDelay = 0
;RDPDelay = 0
;RDPInt = 0
;RIntAfterGTask = 0
;RSPGWaitOnlyFirstGTaskDelay = 0
;Skip = 0
;WaitDelay = 0
;WaitOnlyFirst = 0

[Cmp]
;BlockSize = 0x0
;CmpLimit = 0
;FrameBlockLimit = 0x100
;OptEnable = 0
;W32OverlayCheck = 0

[TempConfig]
;g_nN64CpuPC = 0
;n64MemAcquireForground = 0
;n64MemDefaultRead32MemTest = 0
;n64MemReleaseForground = 0
;RSPGDCFlush = 0

[SI]
;SIDelay = 0x0

[VI]
;ScanReadTime = 0

;[FrameTickHack]
;[Idle]
;[InsertIdleInst]
;[SpecialInst]
;[BreakBlockInst]
;[RomHack]
;[VertexHack]
;[FilterHack]
;[Cheat]
```

[Research done by CORE](https://gbatemp.net/entry/wiiu-n64-virtual-console-research.15301/) <- Requires gbatemp logging.
