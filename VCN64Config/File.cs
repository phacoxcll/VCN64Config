using System.Collections.Generic;
using System.Windows.Forms;

namespace VCN64Config
{
    public class File : Node
    {
        public const string NullString = "[null]";

        public struct SectionRomOption
        {
            public CheckState AIIntPerFrame;
            public CheckState AISetControl;
            public CheckState AISetDAC;
            public CheckState AISetRateBit;
            public CheckState AIUseTimer;
            public int BackupSize;
            public int BackupType;
            public CheckState BootEnd;
            public CheckState BootPCChange;
            public CheckState CmpBlockAdvFlag;
            public int EEROMInitValue;
            public CheckState g_nN64CpuCmpBlockAdvFlag;
            public CheckState MemPak;
            public CheckState NoCntPak;
            public string PDFURL;
            public int PlayerNum;
            public int RamSize;
            public CheckState RetraceByVsync;
            public CheckState RomType;
            public CheckState RSPAMultiCoreWait;
            public CheckState RSPMultiCore;
            public int RSPMultiCoreInt;
            public CheckState RSPMultiCoreWait;
            public CheckState Rumble;
            public CheckState ScreenCaptureNG;
            public int TicksPerFrame;
            public CheckState TimeIntDelay;
            public CheckState TLBMissEnable;
            public CheckState TPak;
            public CheckState TrueBoot;
            public CheckState UseTimer;
        }

        public struct SectionRender
        {
            public CheckState bCutClip;
            public CheckState bForce720P;
            public CheckState CalculateLOD;
            public int CanvasWidth;
            public int CanvasHeight;
            public CheckState CheckTlutValid;
            public CheckState ClearVertexBuf;
            public int ClipBottom;
            public int ClipLeft;
            public int ClipRight;
            public int ClipTop;
            public List<int> ConstValue;
            public CheckState CopyAlphaForceOne;
            public CheckState CopyColorAfterTask;
            public CheckState CopyColorBuffer;
            public CheckState CopyDepthBuffer;
            public CheckState CopyMiddleBuffer;
            public CheckState DepthCompare;
            public CheckState DepthCompareLess;
            public CheckState DepthCompareMore;
            public CheckState DoubleFillCheck;
            public int FirstFrameAt;
            public CheckState FlushMemEachTask;
            public CheckState FogVertexAlpha;
            public CheckState ForceFilterPoint;
            public CheckState ForceRectFilterPoint;
            public CheckState FrameClearCacheInit;
            public CheckState InitPerspectiveMode;
            public CheckState NeedPreParse;
            public CheckState NeedTileSizeCheck;
            public CheckState PolygonOffset;
            public CheckState PreparseTMEMBlock;
            public CheckState RendererReset;
            public CheckState TexEdgeAlpha;
            public CheckState TileSizeCheckSpecial;
            public CheckState TLUTCheck;
            public CheckState UseColorDither;
            public CheckState useViewportXScale;
            public CheckState useViewportYScale;
            public CheckState useViewportZScale;
            public CheckState XClip;
            public CheckState YClip;
            public CheckState ZClip;
        }

        public struct SectionSound
        {
            public int BufFull;
            public int BufHalf;
            public int BufHave;
            public int FillAfterVCM;
            public int Resample;
        }

        public struct SectionInput
        {
            public CheckState AlwaysHave;
            public CheckState STICK_CLAMP;
            public int StickLimit;
            public int StickModify;
            public CheckState VPAD_STICK_CLAMP;
        }

        public struct SectionRSPG
        {
            public int GTaskDelay;
            public int RDPDelay;
            public CheckState RDPInt;
            public CheckState RIntAfterGTask;
            public CheckState RSPGWaitOnlyFirstGTaskDelay;
            public CheckState Skip;
            public CheckState WaitDelay;
            public CheckState WaitOnlyFirst;
        }

        public struct SectionCmp
        {
            public int BlockSize;
            public CheckState CmpLimit;
            public int FrameBlockLimit;
            public CheckState OptEnable;
            public CheckState W32OverlayCheck;
        }

        public struct SectionTempConfig
        {
            public CheckState g_nN64CpuPC;
            public CheckState n64MemAcquireForground;
            public CheckState n64MemDefaultRead32MemTest;
            public CheckState n64MemReleaseForground;
            public CheckState RSPGDCFlush;
        }

        public struct SectionOthers
        {
            public int SIDelay;
            public CheckState ScanReadTime;
            public List<int> FrameTickHack;
        }

        public SectionRomOption RomOption;
        public SectionRender Render;
        public SectionSound Sound;
        public SectionInput Input;
        public SectionRSPG RSPG;
        public SectionCmp Cmp;
        public SectionTempConfig TempConfig;
        public SectionOthers Others;
        public SectionIdle Idle;
        public SectionInsertIdleInst InsertIdleInst;
        public SectionSpecialInst SpecialInst;
        public SectionBreakBlockInst BreakBlockInst;
        public SectionRomHack RomHack;
        public SectionVertexHack VertexHack;
        public SectionFilterHack FilterHack;
        public SectionCheat Cheat;

        public File()
        {
            RomOption = new SectionRomOption();
            Render = new SectionRender();
            Sound = new SectionSound();
            Input = new SectionInput();
            RSPG = new SectionRSPG();
            Cmp = new SectionCmp();
            TempConfig = new SectionTempConfig();
            Others = new SectionOthers();
            Idle = new SectionIdle();
            InsertIdleInst = new SectionInsertIdleInst();
            SpecialInst = new SectionSpecialInst();
            BreakBlockInst = new SectionBreakBlockInst();
            RomHack = new SectionRomHack();
            VertexHack = new SectionVertexHack();
            FilterHack = new SectionFilterHack();
            Cheat = new SectionCheat();

            RomOption.AIIntPerFrame = CheckState.Indeterminate;
            RomOption.AISetControl = CheckState.Indeterminate;
            RomOption.AISetDAC = CheckState.Indeterminate;
            RomOption.AISetRateBit = CheckState.Indeterminate;
            RomOption.AIUseTimer = CheckState.Indeterminate;
            RomOption.BackupSize = -1;
            RomOption.BackupType = -1;
            RomOption.BootEnd = CheckState.Indeterminate;
            RomOption.BootPCChange = CheckState.Indeterminate;
            RomOption.CmpBlockAdvFlag = CheckState.Indeterminate;
            RomOption.EEROMInitValue = -1;
            RomOption.g_nN64CpuCmpBlockAdvFlag = CheckState.Indeterminate;
            RomOption.MemPak = CheckState.Indeterminate;
            RomOption.NoCntPak = CheckState.Indeterminate;
            RomOption.PDFURL = "";
            RomOption.PlayerNum = -1;
            RomOption.RamSize = -1;
            RomOption.RetraceByVsync = CheckState.Indeterminate;
            RomOption.RomType = CheckState.Indeterminate;
            RomOption.RSPAMultiCoreWait = CheckState.Indeterminate;
            RomOption.RSPMultiCore = CheckState.Indeterminate;
            RomOption.RSPMultiCoreInt = -1;
            RomOption.RSPMultiCoreWait = CheckState.Indeterminate;
            RomOption.Rumble = CheckState.Indeterminate;
            RomOption.ScreenCaptureNG = CheckState.Indeterminate;
            RomOption.TicksPerFrame = -1;
            RomOption.TimeIntDelay = CheckState.Indeterminate;
            RomOption.TLBMissEnable = CheckState.Indeterminate;
            RomOption.TPak = CheckState.Indeterminate;
            RomOption.TrueBoot = CheckState.Indeterminate;
            RomOption.UseTimer = CheckState.Indeterminate;

            Render.bCutClip = CheckState.Indeterminate;
            Render.bForce720P = CheckState.Indeterminate;
            Render.CalculateLOD = CheckState.Indeterminate;
            Render.CanvasWidth = -1;
            Render.CanvasHeight = -1;
            Render.CheckTlutValid = CheckState.Indeterminate;
            Render.ClearVertexBuf = CheckState.Indeterminate;
            Render.ClipBottom = 0;
            Render.ClipLeft = 0;
            Render.ClipRight = 0;
            Render.ClipTop = 0;
            Render.ConstValue = new List<int>();
            Render.CopyAlphaForceOne = CheckState.Indeterminate;
            Render.CopyColorAfterTask = CheckState.Indeterminate;
            Render.CopyColorBuffer = CheckState.Indeterminate;
            Render.CopyDepthBuffer = CheckState.Indeterminate;
            Render.CopyMiddleBuffer = CheckState.Indeterminate;
            Render.DepthCompare = CheckState.Indeterminate;
            Render.DepthCompareLess = CheckState.Indeterminate;
            Render.DepthCompareMore = CheckState.Indeterminate;
            Render.DoubleFillCheck = CheckState.Indeterminate;
            Render.FirstFrameAt = -1;
            Render.FlushMemEachTask = CheckState.Indeterminate;
            Render.FogVertexAlpha = CheckState.Indeterminate;
            Render.ForceFilterPoint = CheckState.Indeterminate;
            Render.ForceRectFilterPoint = CheckState.Indeterminate;
            Render.FrameClearCacheInit = CheckState.Indeterminate;
            Render.InitPerspectiveMode = CheckState.Indeterminate;
            Render.NeedPreParse = CheckState.Indeterminate;
            Render.NeedTileSizeCheck = CheckState.Indeterminate;
            Render.PolygonOffset = CheckState.Indeterminate;
            Render.PreparseTMEMBlock = CheckState.Indeterminate;
            Render.RendererReset = CheckState.Indeterminate;
            Render.TexEdgeAlpha = CheckState.Indeterminate;
            Render.TileSizeCheckSpecial = CheckState.Indeterminate;
            Render.TLUTCheck = CheckState.Indeterminate;
            Render.UseColorDither = CheckState.Indeterminate;
            Render.useViewportXScale = CheckState.Indeterminate;
            Render.useViewportYScale = CheckState.Indeterminate;
            Render.useViewportZScale = CheckState.Indeterminate;
            Render.XClip = CheckState.Indeterminate;
            Render.YClip = CheckState.Indeterminate;
            Render.ZClip = CheckState.Indeterminate;

            Sound.BufFull = -1;
            Sound.BufHalf = -1;
            Sound.BufHave = -1;
            Sound.FillAfterVCM = -1;
            Sound.Resample = -1;

            Input.AlwaysHave = CheckState.Indeterminate;
            Input.STICK_CLAMP = CheckState.Indeterminate;
            Input.StickLimit = -1;
            Input.StickModify = -1;
            Input.VPAD_STICK_CLAMP = CheckState.Indeterminate;

            RSPG.GTaskDelay = -1;
            RSPG.RDPDelay = -1;
            RSPG.RDPInt = CheckState.Indeterminate;
            RSPG.RIntAfterGTask = CheckState.Indeterminate;
            RSPG.RSPGWaitOnlyFirstGTaskDelay = CheckState.Indeterminate;
            RSPG.Skip = CheckState.Indeterminate;
            RSPG.WaitDelay = CheckState.Indeterminate;
            RSPG.WaitOnlyFirst = CheckState.Indeterminate;

            Cmp.BlockSize = -1;
            Cmp.CmpLimit = CheckState.Indeterminate;
            Cmp.FrameBlockLimit = -1;
            Cmp.OptEnable = CheckState.Indeterminate;
            Cmp.W32OverlayCheck = CheckState.Indeterminate;

            TempConfig.g_nN64CpuPC = CheckState.Indeterminate;
            TempConfig.n64MemAcquireForground = CheckState.Indeterminate;
            TempConfig.n64MemDefaultRead32MemTest = CheckState.Indeterminate;
            TempConfig.n64MemReleaseForground = CheckState.Indeterminate;
            TempConfig.RSPGDCFlush = CheckState.Indeterminate;

            Others.SIDelay = -1;
            Others.ScanReadTime = CheckState.Indeterminate;
            Others.FrameTickHack = new List<int>();
        }
    }
}
