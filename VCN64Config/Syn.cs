using System;
using System.Collections.Generic;
using System.IO;

namespace VCN64Config
{
    public class Syn
    {
        private VCN64Config.File Config;
        private Lex Lex;
        private Token Current;

        public Syn(StreamReader source)
        {
            Lex = new Lex(source);
        }

        private void Next()
        {
            Current = Lex.GetNextToken();
        }

        private void Match(int label)
        {
            if (Current.Label != label)
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() + ": '" + ((char)label).ToString() + "' was expected instead of '" + ((char)Current.Label).ToString() + "'.");
            Next();
        }

        public VCN64Config.File Run()
        {
            Config = new VCN64Config.File();
            Next();

            if (Current.Label == KeyLabel.LeftSquareBracket)
            {
                do
                {
                    Next();
                    if (Current.Label == KeyLabel.Section)
                    {
                        string section = Current.ToString();
                        Next();
                        Match(KeyLabel.RightSquareBracket);
                        switch (section)
                        {
                            case "RomOption":
                                SectionRomOption();
                                break;
                            case "Render":
                                SectionRender();
                                break;
                            case "Sound":
                                SectionSound();
                                break;
                            case "Input":
                                SectionInput();
                                break;
                            case "RSPG":
                                SectionRSPG();
                                break;
                            case "Cmp":
                                SectionCmp();
                                break;
                            case "TempConfig":
                                SectionTempConfig();
                                break;
                            case "SI":
                                SectionSI();
                                break;
                            case "VI":
                                SectionVI();
                                break;
                            case "FrameTickHack":
                                SectionFrameTickHack();
                                break;
                            case "Idle":
                                SectionIdle();
                                break;
                            case "InsertIdleInst":
                                SectionInsertIdleInst();
                                break;
                            case "SpecialInst":
                                SectionSpecialInst();
                                break;
                            case "BreakBlockInst":
                                SectionBreakBlockInst();
                                break;
                            case "RomHack":
                                SectionRomHack();
                                break;
                            case "VertexHack":
                                SectionVertexHack();
                                break;
                            case "FilterHack":
                                SectionFilterHack();
                                break;
                            default:
                                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() + ": Unexpected section.");
                        }
                    }
                    else if (Current.Label == KeyLabel.Cheat)
                    {
                        Next();
                        Match(KeyLabel.RightSquareBracket);
                        SectionCheat();
                    }
                    else
                        throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() + ": Invalid section name.");
                }
                while (Current.Label == KeyLabel.LeftSquareBracket);
            }
            else if (Current.Label == 0)
            {
                //Empty file
            }
            else if (Current.Label == KeyLabel.Property)
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() +
                    ": The word \"" + Current.ToString() + "\" was found instead of a section name. Missing a semicolon?");
            else
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() +
                    ": A section name was not found.");

            if (Current.Label > 32 && Current.Label < 127)
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() +
                    ": A section name or property was expected, but the symbol was found '" + ((char)Current.Label).ToString() + "'. Missing a semicolon?");
            else if (Current.Label == KeyLabel.Property || Current.Label == KeyLabel.Cheat)
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() +
                    ": A section name or property was expected, but the word was found \"" + Current.ToString() + "\". Missing a semicolon?");
            else if (Current.Label == KeyLabel.IntDEC )
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() +
                    ": A section name or property was expected, but the number was found \"" + Current.ToString() + "\". Missing a semicolon?");
            else if (Current.Label == KeyLabel.IntHEX)
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() +
                    ": A section name or property was expected, but the hexadecimal number was found \"0x" + Current.ToString() + "\". Missing a semicolon?");
            else if (Current.Label == KeyLabel.String)
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() +
                    ": A section name or property was expected, but a string was found \"" + Current.ToString() + "\". Missing a semicolon?");
            else if (Current.Label == KeyLabel.ByteArray)
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() +
                    ": A section name or property was expected, but an byte array was found \"" + Current.ToString() + "\". Missing a semicolon?");
            else if (Current.Label != 0)
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() +
                    ": A section name or property was expected. Missing a semicolon?");

            return Config;
        }

        private void SectionRomOption()
        {
            while (Current.Label == KeyLabel.PropertyRomOption)
            {
                string property = Current.ToString();
                Next();
                Match(KeyLabel.Assignment);
                switch (property)
                {
                    case "AIIntPerFrame":
                        Config.RomOption.AIIntPerFrame = GetZeroOrOne(property);
                        break;
                    case "AISetControl":
                        Config.RomOption.AISetControl = GetZeroOrOne(property);
                        break;
                    case "AISetDAC":
                        Config.RomOption.AISetDAC = GetZeroOrOne(property);
                        break;
                    case "AISetRateBit":
                        Config.RomOption.AISetRateBit = GetZeroOrOne(property);
                        break;
                    case "AIUseTimer":
                        Config.RomOption.AIUseTimer = GetZeroOrOne(property);
                        break;
                    case "BackupSize":
                        Config.RomOption.BackupSize = GetInt(property);
                        break;
                    case "BackupType":
                        Config.RomOption.BackupType = GetInt(property);
                        break;
                    case "BootEnd":
                        Config.RomOption.BootEnd = GetZeroOrOne(property);
                        break;
                    case "BootPCChange":
                        Config.RomOption.BootPCChange = GetZeroOrOne(property);
                        break;
                    case "CmpBlockAdvFlag":
                        Config.RomOption.CmpBlockAdvFlag = GetZeroOrOne(property);
                        break;
                    case "EEROMInitValue":
                        Config.RomOption.EEROMInitValue = GetHex(property);
                        break;
                    case "g_nN64CpuCmpBlockAdvFlag":
                        Config.RomOption.g_nN64CpuCmpBlockAdvFlag = GetZeroOrOne(property);
                        break;
                    case "MemPak":
                        Config.RomOption.MemPak = GetZeroOrOne(property);
                        break;
                    case "NoCntPak":
                        Config.RomOption.NoCntPak = GetZeroOrOne(property);
                        break;
                    case "PDFURL":
                        Config.RomOption.PDFURL = GetString(property);
                        break;
                    case "PlayerNum":
                        Config.RomOption.PlayerNum = GetInt(property);
                        break;
                    case "RamSize":
                        Config.RomOption.RamSize = GetHex(property);
                        break;
                    case "RetraceByVsync":
                        Config.RomOption.RetraceByVsync = GetZeroOrOne(property);
                        break;
                    case "RomType":
                        Config.RomOption.RomType = GetZeroOrOne(property);
                        break;
                    case "RSPAMultiCoreWait":
                        Config.RomOption.RSPAMultiCoreWait = GetZeroOrOne(property);
                        break;
                    case "RSPMultiCore":
                        Config.RomOption.RSPMultiCore = GetZeroOrOne(property);
                        break;
                    case "RSPMultiCoreInt":
                        Config.RomOption.RSPMultiCoreInt = GetInt(property);
                        break;
                    case "RSPMultiCoreWait":
                        Config.RomOption.RSPMultiCoreWait = GetZeroOrOne(property);
                        break;
                    case "Rumble":
                        Config.RomOption.Rumble = GetZeroOrOne(property);
                        break;
                    case "ScreenCaptureNG":
                        Config.RomOption.ScreenCaptureNG = GetZeroOrOne(property);
                        break;
                    case "TicksPerFrame":
                        Config.RomOption.TicksPerFrame = GetInt(property);
                        break;
                    case "TimeIntDelay":
                        Config.RomOption.TimeIntDelay = GetZeroOrOne(property);
                        break;
                    case "TLBMissEnable":
                        Config.RomOption.TLBMissEnable = GetZeroOrOne(property);
                        break;
                    case "TPak":
                        Config.RomOption.TPak = GetZeroOrOne(property);
                        break;
                    case "TrueBoot":
                        Config.RomOption.TrueBoot = GetZeroOrOne(property);
                        break;
                    case "UseTimer":
                        Config.RomOption.UseTimer = GetZeroOrOne(property);
                        break;
                    default:
                        throw new Exception("Syntactic analyzer [RomOption] line " + Lex.Line.ToString() + ": Unexpected property \"" + property + "\".");
                }
                Next();
            }
        }

        private void SectionRender()
        {
            while (Current.Label == KeyLabel.PropertyRender)
            {
                string property = Current.ToString();
                if (property == "ConstValue")
                    Config.Render.ConstValue = GetList(property);                
                else
                {
                    Next();
                    Match(KeyLabel.Assignment);
                    switch (property)
                    {
                        case "bCutClip":
                            Config.Render.bCutClip = GetZeroOrOne(property);
                            break;
                        case "bForce720P":
                            Config.Render.bForce720P = GetZeroOrOne(property);
                            break;
                        case "CalculateLOD":
                            Config.Render.CalculateLOD = GetZeroOrOne(property);
                            break;
                        case "CanvasWidth":
                            Config.Render.CanvasWidth = GetInt(property);
                            break;
                        case "CanvasHeight":
                            Config.Render.CanvasHeight = GetInt(property);
                            break;
                        case "CheckTlutValid":
                            Config.Render.CheckTlutValid = GetZeroOrOne(property);
                            break;
                        case "ClearVertexBuf":
                            Config.Render.ClearVertexBuf = GetZeroOrOne(property);
                            break;
                        case "ClipBottom":
                            Config.Render.ClipBottom = GetInt(property);
                            break;
                        case "ClipLeft":
                            Config.Render.ClipLeft = GetInt(property);
                            break;
                        case "ClipRight":
                            Config.Render.ClipRight = GetInt(property);
                            break;
                        case "ClipTop":
                            Config.Render.ClipTop = GetInt(property);
                            break;
                        case "CopyAlphaForceOne":
                            Config.Render.CopyAlphaForceOne = GetZeroOrOne(property);
                            break;
                        case "CopyColorAfterTask":
                            Config.Render.CopyColorAfterTask = GetZeroOrOne(property);
                            break;
                        case "CopyColorBuffer":
                            Config.Render.CopyColorBuffer = GetZeroOrOne(property);
                            break;
                        case "CopyDepthBuffer":
                            Config.Render.CopyDepthBuffer = GetZeroOrOne(property);
                            break;
                        case "CopyMiddleBuffer":
                            Config.Render.CopyMiddleBuffer = GetZeroOrOne(property);
                            break;
                        case "DepthCompare":
                            Config.Render.DepthCompare = GetZeroOrOne(property);
                            break;
                        case "DepthCompareLess":
                            Config.Render.DepthCompareLess = GetZeroOrOne(property);
                            break;
                        case "DepthCompareMore":
                            Config.Render.DepthCompareMore = GetZeroOrOne(property);
                            break;
                        case "DoubleFillCheck":
                            Config.Render.DoubleFillCheck = GetZeroOrOne(property);
                            break;
                        case "FirstFrameAt":
                            Config.Render.FirstFrameAt = GetInt(property);
                            break;
                        case "FlushMemEachTask":
                            Config.Render.FlushMemEachTask = GetZeroOrOne(property);
                            break;
                        case "FogVertexAlpha":
                            Config.Render.FogVertexAlpha = GetZeroOrOne(property);
                            break;
                        case "ForceFilterPoint":
                            Config.Render.ForceFilterPoint = GetZeroOrOne(property);
                            break;
                        case "ForceRectFilterPoint":
                            Config.Render.ForceRectFilterPoint = GetZeroOrOne(property);
                            break;
                        case "FrameClearCacheInit":
                            Config.Render.ForceRectFilterPoint = GetZeroOrOne(property);
                            break;
                        case "InitPerspectiveMode":
                            Config.Render.InitPerspectiveMode = GetZeroOrOne(property);
                            break;
                        case "NeedPreParse":
                            Config.Render.NeedPreParse = GetZeroOrOne(property);
                            break;
                        case "NeedTileSizeCheck":
                            Config.Render.NeedTileSizeCheck = GetZeroOrOne(property);
                            break;
                        case "PolygonOffset":
                            Config.Render.PolygonOffset = GetZeroOrOne(property);
                            break;
                        case "PreparseTMEMBlock":
                            Config.Render.PreparseTMEMBlock = GetZeroOrOne(property);
                            break;
                        case "RendererReset":
                            Config.Render.RendererReset = GetZeroOrOne(property);
                            break;
                        case "TexEdgeAlpha":
                            Config.Render.TexEdgeAlpha = GetZeroOrOne(property);
                            break;
                        case "TileSizeCheckSpecial":
                            Config.Render.TileSizeCheckSpecial = GetZeroOrOne(property);
                            break;
                        case "TLUTCheck":
                            Config.Render.TLUTCheck = GetZeroOrOne(property);
                            break;
                        case "UseColorDither":
                            Config.Render.UseColorDither = GetZeroOrOne(property);
                            break;
                        case "useViewportXScale":
                            Config.Render.useViewportXScale = GetZeroOrOne(property);
                            break;
                        case "useViewportYScale":
                            Config.Render.useViewportYScale = GetZeroOrOne(property);
                            break;
                        case "useViewportZScale":
                            Config.Render.useViewportZScale = GetZeroOrOne(property);
                            break;
                        case "XClip":
                            Config.Render.XClip = GetZeroOrOne(property);
                            break;
                        case "YClip":
                            Config.Render.YClip = GetZeroOrOne(property);
                            break;
                        case "ZClip":
                            Config.Render.ZClip = GetZeroOrOne(property);
                            break;
                        default:
                            throw new Exception("Syntactic analyzer [Render] line " + Lex.Line.ToString() + ": Unexpected property \"" + property + "\".");
                    }
                    Next();
                }                
            }
        }

        private void SectionSound()
        {
            while (Current.Label == KeyLabel.PropertySound)
            {
                string property = Current.ToString();
                Next();
                Match(KeyLabel.Assignment);
                switch (property)
                {
                    case "BufFull":
                        Config.Sound.BufFull = GetHex(property);
                        break;
                    case "BufHalf":
                        Config.Sound.BufHalf = GetHex(property);
                        break;
                    case "BufHave":
                        Config.Sound.BufHave = GetHex(property);
                        break;
                    case "FillAfterVCM":
                        Config.Sound.FillAfterVCM = GetInt(property);
                        break;
                    case "Resample":
                        Config.Sound.Resample = GetInt(property);
                        break;
                    default:
                        throw new Exception("Syntactic analyzer [Sound] line " + Lex.Line.ToString() + ": Unexpected property \"" + property + "\".");
                }
                Next();
            }
        }

        private void SectionInput()
        {
            while (Current.Label == KeyLabel.PropertyInput)
            {
                string property = Current.ToString();
                Next();
                Match(KeyLabel.Assignment);
                switch (property)
                {
                    case "AlwaysHave":
                        Config.Input.AlwaysHave = GetZeroOrOne(property);
                        break;
                    case "STICK_CLAMP":
                        Config.Input.STICK_CLAMP = GetZeroOrOne(property);
                        break;
                    case "StickLimit":
                        Config.Input.StickLimit = GetInt(property);
                        break;
                    case "StickModify":
                        Config.Input.StickModify = GetInt(property);
                        break;
                    case "VPAD_STICK_CLAMP":
                        Config.Input.VPAD_STICK_CLAMP = GetZeroOrOne(property);
                        break;
                    default:
                        throw new Exception("Syntactic analyzer [Input] line " + Lex.Line.ToString() + ": Unexpected property \"" + property + "\".");
                }
                Next();
            }
        }

        private void SectionRSPG()
        {
            while (Current.Label == KeyLabel.PropertyRSPG)
            {
                string property = Current.ToString();
                Next();
                Match(KeyLabel.Assignment);
                switch (property)
                {
                    case "GTaskDelay":
                        Config.RSPG.GTaskDelay = GetInt(property);
                        break;
                    case "RDPDelay":
                        Config.RSPG.RDPDelay = GetInt(property);
                        break;
                    case "RDPInt":
                        Config.RSPG.RDPInt = GetZeroOrOne(property);
                        break;
                    case "RIntAfterGTask":
                        Config.RSPG.RIntAfterGTask = GetZeroOrOne(property);
                        break;
                    case "RSPGWaitOnlyFirstGTaskDelay":
                        Config.RSPG.RSPGWaitOnlyFirstGTaskDelay = GetZeroOrOne(property);
                        break;
                    case "Skip":
                        Config.RSPG.Skip = GetZeroOrOne(property);
                        break;
                    case "WaitDelay":
                        Config.RSPG.WaitDelay = GetZeroOrOne(property);
                        break;
                    case "WaitOnlyFirst":
                        Config.RSPG.WaitOnlyFirst = GetZeroOrOne(property);
                        break;
                    default:
                        throw new Exception("Syntactic analyzer [RSPG] line " + Lex.Line.ToString() + ": Unexpected property \"" + property + "\".");
                }
                Next();
            }
        }

        private void SectionCmp()
        {
            while (Current.Label == KeyLabel.PropertyCmp)
            {
                string property = Current.ToString();
                Next();
                Match(KeyLabel.Assignment);
                switch (property)
                {
                    case "BlockSize":
                        Config.Cmp.BlockSize = GetHex(property);
                        break;
                    case "CmpLimit":
                        Config.Cmp.CmpLimit = GetZeroOrOne(property);
                        break;
                    case "FrameBlockLimit":
                        Config.Cmp.FrameBlockLimit = GetHex(property);
                        break;
                    case "OptEnable":
                        Config.Cmp.OptEnable = GetZeroOrOne(property);
                        break;
                    case "W32OverlayCheck":
                        Config.Cmp.W32OverlayCheck = GetZeroOrOne(property);
                        break;
                    default:
                        throw new Exception("Syntactic analyzer [Cmp] line " + Lex.Line.ToString() + ": Unexpected property \"" + property + "\".");
                }
                Next();
            }
        }

        private void SectionTempConfig()
        {
            while (Current.Label == KeyLabel.PropertyTempConfig)
            {
                string property = Current.ToString();
                Next();
                Match(KeyLabel.Assignment);
                switch (property)
                {
                    case "g_nN64CpuPC":
                        Config.TempConfig.g_nN64CpuPC = GetZeroOrOne(property);
                        break;
                    case "n64MemAcquireForground":
                        Config.TempConfig.n64MemAcquireForground = GetZeroOrOne(property);
                        break;
                    case "n64MemDefaultRead32MemTest":
                        Config.TempConfig.n64MemDefaultRead32MemTest = GetZeroOrOne(property);
                        break;
                    case "n64MemReleaseForground":
                        Config.TempConfig.n64MemReleaseForground = GetZeroOrOne(property);
                        break;
                    case "RSPGDCFlush":
                        Config.TempConfig.RSPGDCFlush = GetZeroOrOne(property);
                        break;
                    default:
                        throw new Exception("Syntactic analyzer [TempConfig] line " + Lex.Line.ToString() + ": Unexpected property \"" + property + "\".");
                }
                Next();
            }
        }

        private void SectionSI()
        {
            if (Current.Label == KeyLabel.PropertySI)
            {
                Next();
                Match(KeyLabel.Assignment);
                Config.Others.SIDelay = GetHex("SIDelay");
                Next();
            }
            //else
                //throw new Exception("Syntactic analyzer [SI] line " + Lex.Line.ToString() + ": Unexpected property.");
        }

        private void SectionVI()
        {
            if (Current.Label == KeyLabel.PropertyVI)
            {
                Next();
                Match(KeyLabel.Assignment);
                Config.Others.ScanReadTime = GetZeroOrOne("ScanReadTime");
                Next();
            }
            //else
                //throw new Exception("Syntactic analyzer [VI] line " + Lex.Line.ToString() + ": Unexpected property.");
        }

        private void SectionFrameTickHack()
        {
            if (Current.Label == KeyLabel.PropertyFrameTickHack)            
                Config.Others.FrameTickHack = GetList("Hack");            
            //else
                //throw new Exception("Syntactic analyzer [FrameTickHack] line " + Lex.Line.ToString() + ": Unexpected property.");
        }

        private void SectionIdle()
        {
            if (Current.Label == KeyLabel.PropertyCount)
            {
                Next();
                Match(KeyLabel.Assignment);
                Config.Idle.Count = GetInt("Count");
                Next();
                while (Current.Label == KeyLabel.PropertyAddress ||
                    Current.Label == KeyLabel.PropertyInst ||
                    Current.Label == KeyLabel.PropertyType)
                {
                    Word property = (Word)Current;
                    Next();
                    if (Current.Label == KeyLabel.IntDEC)
                    {
                        int index = (Current as IntValue).Value;
                        //if (index < Config.Idle.Count)
                        //{
                            Next();
                            Match(KeyLabel.Assignment);
                            if (property.Label == KeyLabel.PropertyAddress)
                                Config.Idle.SetAddress(index, GetHex(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyInst)
                                Config.Idle.SetInst(index, GetHex(property.ToString()));
                            else
                                Config.Idle.SetType(index, GetInt(property.ToString()));
                            Next();
                        //}
                        //else
                            //throw new Exception();
                    }
                    else
                        throw new Exception("Syntactic analyzer [Idle] line " + Lex.Line.ToString() + ": An index number was expected on the property \"" + property + "\".");
                }
            }
            else
                throw new Exception("Syntactic analyzer [Idle] line " + Lex.Line.ToString() + ": Unexpected property.");
        }

        private void SectionInsertIdleInst()
        {
            if (Current.Label == KeyLabel.PropertyCount)
            {
                Next();
                Match(KeyLabel.Assignment);
                Config.InsertIdleInst.Count = GetInt("Count");
                Next();
                while (Current.Label == KeyLabel.PropertyAddress ||
                    Current.Label == KeyLabel.PropertyInst ||
                    Current.Label == KeyLabel.PropertyType ||
                    Current.Label == KeyLabel.PropertyValue)
                {
                    Word property = (Word)Current;
                    Next();
                    if (Current.Label == KeyLabel.IntDEC)
                    {
                        int index = (Current as IntValue).Value;
                        //if (index < Config.InsertIdleInst.Count)
                        //{
                            Next();
                            Match(KeyLabel.Assignment);
                            if (property.Label == KeyLabel.PropertyAddress)
                                Config.InsertIdleInst.SetAddress(index, GetHex(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyInst)
                                Config.InsertIdleInst.SetInst(index, GetHex(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyType)
                                Config.InsertIdleInst.SetType(index, GetIntOrHex(property.ToString()));
                            else
                                Config.InsertIdleInst.SetValue(index, GetIntOrHex(property.ToString()));
                            Next();
                        //}
                        //else
                            //throw new Exception();
                    }
                    else
                        throw new Exception("Syntactic analyzer [InsertIdleInst] line " + Lex.Line.ToString() + ": An index number was expected on the property \"" + property + "\".");
                }
            }
            else
                throw new Exception("Syntactic analyzer [InsertIdleInst] line " + Lex.Line.ToString() + ": Unexpected property.");
        }

        private void SectionSpecialInst()
        {
            if (Current.Label == KeyLabel.PropertyCount)
            {
                Next();
                Match(KeyLabel.Assignment);
                Config.SpecialInst.Count = GetInt("Count");
                Next();
                while (Current.Label == KeyLabel.PropertyAddress ||
                    Current.Label == KeyLabel.PropertyInst ||
                    Current.Label == KeyLabel.PropertyType ||
                    Current.Label == KeyLabel.PropertyValue)
                {
                    Word property = (Word)Current;
                    Next();
                    if (Current.Label == KeyLabel.IntDEC)
                    {
                        int index = (Current as IntValue).Value;
                        //if (index < Config.SpecialInst.Count)
                        //{
                            Next();
                            Match(KeyLabel.Assignment);
                            if (property.Label == KeyLabel.PropertyAddress)
                                Config.SpecialInst.SetAddress(index, GetHex(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyInst)
                                Config.SpecialInst.SetInst(index, GetHex(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyType)
                                Config.SpecialInst.SetType(index, GetInt(property.ToString()));
                            else
                                Config.SpecialInst.SetValue(index, GetHex(property.ToString()));
                            Next();
                        //}
                        //else
                            //throw new Exception();
                    }
                    else
                        throw new Exception("Syntactic analyzer [SpecialInst] line " + Lex.Line.ToString() + ": An index number was expected on the property \"" + property + "\".");
                }
            }
            else
                throw new Exception("Syntactic analyzer [SpecialInst] line " + Lex.Line.ToString() + ": Unexpected property.");
        }

        private void SectionBreakBlockInst()
        {
            if (Current.Label == KeyLabel.PropertyCount)
            {
                Next();
                Match(KeyLabel.Assignment);
                Config.BreakBlockInst.Count = GetInt("Count");
                Next();
                while (Current.Label == KeyLabel.PropertyAddress ||
                    Current.Label == KeyLabel.PropertyInst ||
                    Current.Label == KeyLabel.PropertyType ||
                    Current.Label == KeyLabel.PropertyJmpPC)
                {
                    Word property = (Word)Current;
                    Next();
                    if (Current.Label == KeyLabel.IntDEC)
                    {
                        int index = (Current as IntValue).Value;
                        //if (index < Config.BreakBlockInst.Count)
                        //{
                            Next();
                            Match(KeyLabel.Assignment);
                            if (property.Label == KeyLabel.PropertyAddress)
                                Config.BreakBlockInst.SetAddress(index, GetHex(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyInst)
                                Config.BreakBlockInst.SetInst(index, GetHex(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyType)
                                Config.BreakBlockInst.SetType(index, GetInt(property.ToString()));
                            else
                                Config.BreakBlockInst.SetJmpPC(index, GetHex(property.ToString()));
                            Next();
                        //}
                        //else
                            //throw new Exception();
                    }
                    else
                        throw new Exception("Syntactic analyzer [BreakBlockInst] line " + Lex.Line.ToString() + ": An index number was expected on the property \"" + property + "\".");
                }
            }
            else
                throw new Exception("Syntactic analyzer [BreakBlockInst] line " + Lex.Line.ToString() + ": Unexpected property.");
        }

        private void SectionRomHack()
        {
            if (Current.Label == KeyLabel.PropertyCount)
            {
                Next();
                Match(KeyLabel.Assignment);
                Config.RomHack.Count = GetInt("Count");
                Next();
                while (Current.Label == KeyLabel.PropertyAddress ||
                    Current.Label == KeyLabel.PropertyType ||
                    Current.Label == KeyLabel.PropertyValue)
                {
                    Word property = (Word)Current;
                    Next();
                    if (Current.Label == KeyLabel.IntDEC)
                    {
                        int index = (Current as IntValue).Value;
                        //if (index < Config.RomHack.Count)
                        //{
                            Next();
                            Match(KeyLabel.Assignment);
                            if (property.Label == KeyLabel.PropertyAddress)
                                Config.RomHack.SetAddress(index, GetHex(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyType)
                                Config.RomHack.SetType(index, GetInt(property.ToString()));
                            else
                                Config.RomHack.SetValue(index, GetByteArray(property.ToString()));
                            Next();
                        //}
                        //else
                            //throw new Exception();
                    }
                    else
                        throw new Exception("Syntactic analyzer [RomHack] line " + Lex.Line.ToString() + ": An index number was expected on the property \"" + property + "\".");
                }
            }
            else
                throw new Exception("Syntactic analyzer [RomHack] line " + Lex.Line.ToString() + ": Unexpected property.");
        }

        private void SectionVertexHack()
        {
            if (Current.Label == KeyLabel.PropertyCount)
            {
                Next();
                Match(KeyLabel.Assignment);
                Config.VertexHack.Count = GetInt("Count");
                Next();
                while (Current.Label == KeyLabel.PropertyValue ||
                    (Current.Label >= KeyLabel.PropertyVertexCount &&
                    Current.Label <= KeyLabel.PropertyTextureAddress))
                {
                    Word property = (Word)Current;
                    Next();
                    if (Current.Label == KeyLabel.IntDEC)
                    {
                        int index = (Current as IntValue).Value;
                        //if (index < Config.VertexHack.Count)
                        //{
                            Next();
                            Match(KeyLabel.Assignment);
                            if (property.Label == KeyLabel.PropertyVertexCount)
                                Config.VertexHack.SetVertexCount(index, GetInt(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyVertexAddress)
                                Config.VertexHack.SetVertexAddress(index, GetHex(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyFirstVertex)
                                Config.VertexHack.SetFirstVertex(index, GetByteArray(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyTextureAddress)
                                Config.VertexHack.SetTextureAddress(index, GetHex(property.ToString()));
                            else
                                Config.VertexHack.SetValue(index, GetByteArray(property.ToString()));
                            Next();
                        //}
                        //else
                            //throw new Exception();
                    }
                    else
                        throw new Exception("Syntactic analyzer [VertexHack] line " + Lex.Line.ToString() + ": An index number was expected on the property \"" + property + "\".");
                }
            }
            else
                throw new Exception("Syntactic analyzer [VertexHack] line " + Lex.Line.ToString() + ": Unexpected property.");
        }

        private void SectionFilterHack()
        {
            if (Current.Label == KeyLabel.PropertyCount)
            {
                Next();
                Match(KeyLabel.Assignment);
                Config.FilterHack.Count = GetInt("Count");
                Next();
                while (Current.Label >= KeyLabel.PropertyTextureAddress &&
                    Current.Label <= KeyLabel.PropertyOffsetT)
                {
                    Word property = (Word)Current;
                    Next();
                    if (Current.Label == KeyLabel.IntDEC)
                    {
                        int index = (Current as IntValue).Value;
                        //if (index < Config.FilterHack.Count)
                        //{
                            Next();
                            Match(KeyLabel.Assignment);
                            if (property.Label == KeyLabel.PropertyTextureAddress)
                                Config.FilterHack.SetTextureAddress(index, GetHex(property.ToString()));
                            else if (property.Label == KeyLabel.PropertySumPixel)
                                Config.FilterHack.SetSumPixel(index, GetHex(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyData2)
                                Config.FilterHack.SetData2(index, GetHex(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyData3)
                                Config.FilterHack.SetData3(index, GetHex(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyAlphaTest)
                                Config.FilterHack.SetAlphaTest(index, GetInt(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyMagFilter)
                                Config.FilterHack.SetMagFilter(index, GetInt(property.ToString()));
                            else if (property.Label == KeyLabel.PropertyOffsetS)
                                Config.FilterHack.SetOffsetS(index, GetHex(property.ToString()));
                            else
                                Config.FilterHack.SetOffsetT(index, GetHex(property.ToString()));
                            Next();
                        //}
                        //else
                            //throw new Exception();
                    }
                    else
                        throw new Exception("Syntactic analyzer [FilterHack] line " + Lex.Line.ToString() + ": An index number was expected on the property \"" + property + "\".");
                }
            }
            else
                throw new Exception("Syntactic analyzer [FilterHack] line " + Lex.Line.ToString() + ": Unexpected property.");
        }

        private void SectionCheat()
        {
            while (Current.Label == KeyLabel.Cheat)
            {
                Next();
                if (Current.Label == KeyLabel.IntDEC)
                {
                    int index = (Current as IntValue).Value;
                    Next();
                    if (Current.Label == KeyLabel.Assignment)
                    {
                        Next();
                        Config.Cheat.SetN(index, GetInt("Cheat" + index));
                    }
                    else if (Current.Label == KeyLabel.PropertyCheatAddr)
                    {
                        Next();
                        Match(KeyLabel.Assignment);
                        Config.Cheat.SetAddr(index, GetHex("Cheat" + index + "_Addr"));
                    }
                    else if (Current.Label == KeyLabel.PropertyCheatValue)
                    {
                        Next();
                        Match(KeyLabel.Assignment);
                        Config.Cheat.SetValue(index, GetIntOrHex("Cheat" + index + "_Value"));
                    }
                    else if (Current.Label == KeyLabel.PropertyCheatBytes)
                    {
                        Next();
                        Match(KeyLabel.Assignment);
                        Config.Cheat.SetBytes(index, GetInt("Cheat" + index + "_Bytes"));
                    }
                    else
                        throw new Exception("Syntactic analyzer [Cheat] line " + Lex.Line.ToString() + ": A valid \"cheat property\" was expected.");
                    Next();
                }
                else
                    throw new Exception("Syntactic analyzer [Cheat] line " + Lex.Line.ToString() + ": An index number was expected on the property \"cheat\".");
            }
        }

        private System.Windows.Forms.CheckState GetZeroOrOne(string property)
        {
            if (Current.Label == KeyLabel.IntDEC)
            {
                int num = (Current as IntValue).Value;
                if (num == 0)
                    return System.Windows.Forms.CheckState.Unchecked;
                else if (num == 1)
                    return System.Windows.Forms.CheckState.Checked;
                else
                    throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() + ": A 0 or 1 was expected as the value for the property \"" + property + "\".");
            }
            else
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() + ": A 0 or 1 was expected as the value for the property \"" + property + "\".");
        }

        private int GetIntOrHex(string property)
        {
            if (Current.Label == KeyLabel.IntDEC || Current.Label == KeyLabel.IntHEX)
                return (Current as IntValue).Value;
            else
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() + ": A decimal or hexadecimal number was expected as the value for the property \"" + property + "\".");
        }

        private int GetInt(string property)
        {
            if (Current.Label == KeyLabel.IntDEC)
                return (Current as IntValue).Value;
            else
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() + ": A decimal number was expected as the value for the property \"" + property + "\".");
        }

        private int GetHex(string property)
        {
            if (Current.Label == KeyLabel.IntHEX)           
                return (Current as IntValue).Value;            
            else
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() + ": A hexadecimal number was expected as the value for the property \"" + property + "\".");
        }

        private string GetByteArray(string property)
        {
            if (Current.Label == KeyLabel.IntHEX)
            {
                int value = (Current as IntValue).Value;
                if (value <= 0xFF)
                    return (Current as IntValue).Value.ToString("X2");
                else if (value > 0xFF && value <= 0xFFFF)
                    return (Current as IntValue).Value.ToString("X4");
                else if (value > 0xFFFF && value <= 0xFFFFFF)
                    return (Current as IntValue).Value.ToString("X6");
                else
                    return (Current as IntValue).Value.ToString("X8");
            }
            else if (Current.Label == KeyLabel.ByteArray)
                return (Current as ByteArrayValue).Value;
            else
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() + ": An hexadecimal number or byte array was expected as the value for the property \"" + property + "\".");
        }

        private string GetString(string property)
        {
            if (Current.Label == KeyLabel.String)            
                 return Current.ToString();            
            else
                throw new Exception("Syntactic analyzer line " + Lex.Line.ToString() + ": A string was expected as the value for the property \"" + property + "\".");
        }

        private List<int> GetList(string property)
        {
            List<int> list = new List<int>();

            do
            {
                Next();
                if (Current.Label == KeyLabel.IntDEC)
                {
                    int index = (Current as IntValue).Value;
                    if (index == list.Count)
                    {
                        Next();
                        Match(KeyLabel.Assignment);
                        if (Current.Label == KeyLabel.IntDEC || Current.Label == KeyLabel.IntHEX)
                            list.Add((Current as IntValue).Value);
                        else
                            throw new Exception("Syntactic analyzer (list) line " + Lex.Line.ToString() + ": A decimal or hexadecimal number was expected as the value for the property \"" + property + "\".");
                    }
                    else
                        throw new Exception("Syntactic analyzer (list) line " + Lex.Line.ToString() + ": The property index \"" + property + index.ToString() + "\" is higher than expected (" + list.Count.ToString() + ").");
                }
                else
                    throw new Exception("Syntactic analyzer (list) line " + Lex.Line.ToString() + ": An index number was expected on the property \"" + property + "\".");
                Next();
            }
            while ((Current is Word) && Current.ToString() == property);

            return list;
        }
    }
}
