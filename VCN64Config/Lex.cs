using System;
using System.Collections;
using System.IO;
using System.Text;

namespace VCN64Config
{
    public class Lex
    {
        public int Line { private set; get; }
        private StreamReader Source;
        private char Lookahead;
        private Hashtable Words;

        public Lex(StreamReader source)
        {
            Line = 1;
            Lookahead = ' ';
            Words = new Hashtable();

            Words.Add("RomOption", KeyLabel.Section);
            Words.Add("Render", KeyLabel.Section);
            Words.Add("Sound", KeyLabel.Section);
            Words.Add("Input", KeyLabel.Section);
            Words.Add("RSPG", KeyLabel.Section);
            Words.Add("Cmp", KeyLabel.Section);
            Words.Add("TempConfig", KeyLabel.Section);
            Words.Add("SI", KeyLabel.Section);
            Words.Add("VI", KeyLabel.Section);
            Words.Add("FrameTickHack", KeyLabel.Section);
            Words.Add("Idle", KeyLabel.Section);
            Words.Add("InsertIdleInst", KeyLabel.Section);
            Words.Add("SpecialInst", KeyLabel.Section);
            Words.Add("BreakBlockInst", KeyLabel.Section);
            Words.Add("RomHack", KeyLabel.Section);
            Words.Add("VertexHack", KeyLabel.Section);
            Words.Add("FilterHack", KeyLabel.Section);
            Words.Add("Cheat", KeyLabel.Cheat);

            Words.Add("AIIntPerFrame", KeyLabel.PropertyRomOption);
            Words.Add("AISetControl", KeyLabel.PropertyRomOption);
            Words.Add("AISetDAC", KeyLabel.PropertyRomOption);
            Words.Add("AISetRateBit", KeyLabel.PropertyRomOption);
            Words.Add("AIUseTimer", KeyLabel.PropertyRomOption);
            Words.Add("BackupSize", KeyLabel.PropertyRomOption);
            Words.Add("BackupType", KeyLabel.PropertyRomOption);
            Words.Add("BootEnd", KeyLabel.PropertyRomOption);
            Words.Add("BootPCChange", KeyLabel.PropertyRomOption);
            Words.Add("CmpBlockAdvFlag", KeyLabel.PropertyRomOption);
            Words.Add("EEROMInitValue", KeyLabel.PropertyRomOption);
            Words.Add("g_nN64CpuCmpBlockAdvFlag", KeyLabel.PropertyRomOption);
            Words.Add("MemPak", KeyLabel.PropertyRomOption);
            Words.Add("NoCntPak", KeyLabel.PropertyRomOption);
            Words.Add("PDFURL", KeyLabel.PropertyRomOption);
            Words.Add("PlayerNum", KeyLabel.PropertyRomOption);
            Words.Add("RamSize", KeyLabel.PropertyRomOption);
            Words.Add("RetraceByVsync", KeyLabel.PropertyRomOption);
            Words.Add("RomType", KeyLabel.PropertyRomOption);
            Words.Add("RSPAMultiCoreWait", KeyLabel.PropertyRomOption);
            Words.Add("RSPMultiCore", KeyLabel.PropertyRomOption);
            Words.Add("RSPMultiCoreInt", KeyLabel.PropertyRomOption);
            Words.Add("RSPMultiCoreWait", KeyLabel.PropertyRomOption);
            Words.Add("Rumble", KeyLabel.PropertyRomOption);
            Words.Add("ScreenCaptureNG", KeyLabel.PropertyRomOption);
            Words.Add("TicksPerFrame", KeyLabel.PropertyRomOption);
            Words.Add("TimeIntDelay", KeyLabel.PropertyRomOption);
            Words.Add("TLBMissEnable", KeyLabel.PropertyRomOption);
            Words.Add("TPak", KeyLabel.PropertyRomOption);
            Words.Add("TrueBoot", KeyLabel.PropertyRomOption);
            Words.Add("UseTimer", KeyLabel.PropertyRomOption);

            Words.Add("bCutClip", KeyLabel.PropertyRender);
            Words.Add("bForce720P", KeyLabel.PropertyRender);
            Words.Add("CalculateLOD", KeyLabel.PropertyRender);
            Words.Add("CanvasWidth", KeyLabel.PropertyRender);
            Words.Add("CanvasHeight", KeyLabel.PropertyRender);
            Words.Add("CheckTlutValid", KeyLabel.PropertyRender);
            Words.Add("ClearVertexBuf", KeyLabel.PropertyRender);
            Words.Add("ClipBottom", KeyLabel.PropertyRender);
            Words.Add("ClipLeft", KeyLabel.PropertyRender);
            Words.Add("ClipRight", KeyLabel.PropertyRender);
            Words.Add("ClipTop", KeyLabel.PropertyRender);
            Words.Add("ConstValue", KeyLabel.PropertyRender);
            Words.Add("CopyAlphaForceOne", KeyLabel.PropertyRender);
            Words.Add("CopyColorAfterTask", KeyLabel.PropertyRender);
            Words.Add("CopyColorBuffer", KeyLabel.PropertyRender);
            Words.Add("CopyDepthBuffer", KeyLabel.PropertyRender);
            Words.Add("CopyMiddleBuffer", KeyLabel.PropertyRender);
            Words.Add("DepthCompare", KeyLabel.PropertyRender);
            Words.Add("DepthCompareLess", KeyLabel.PropertyRender);
            Words.Add("DepthCompareMore", KeyLabel.PropertyRender);
            Words.Add("DoubleFillCheck", KeyLabel.PropertyRender);
            Words.Add("FirstFrameAt", KeyLabel.PropertyRender);
            Words.Add("FlushMemEachTask", KeyLabel.PropertyRender);
            Words.Add("FogVertexAlpha", KeyLabel.PropertyRender);
            Words.Add("ForceFilterPoint", KeyLabel.PropertyRender);
            Words.Add("ForceRectFilterPoint", KeyLabel.PropertyRender);
            Words.Add("FrameClearCacheInit", KeyLabel.PropertyRender);
            Words.Add("InitPerspectiveMode", KeyLabel.PropertyRender);
            Words.Add("NeedPreParse", KeyLabel.PropertyRender);
            Words.Add("NeedTileSizeCheck", KeyLabel.PropertyRender);
            Words.Add("PolygonOffset", KeyLabel.PropertyRender);
            Words.Add("PreparseTMEMBlock", KeyLabel.PropertyRender);
            Words.Add("RendererReset", KeyLabel.PropertyRender);
            Words.Add("TexEdgeAlpha", KeyLabel.PropertyRender);
            Words.Add("TileSizeCheckSpecial", KeyLabel.PropertyRender);
            Words.Add("TLUTCheck", KeyLabel.PropertyRender);
            Words.Add("UseColorDither", KeyLabel.PropertyRender);
            Words.Add("useViewportXScale", KeyLabel.PropertyRender);
            Words.Add("useViewportYScale", KeyLabel.PropertyRender);
            Words.Add("useViewportZScale", KeyLabel.PropertyRender);
            Words.Add("XClip", KeyLabel.PropertyRender);
            Words.Add("YClip", KeyLabel.PropertyRender);
            Words.Add("ZClip", KeyLabel.PropertyRender);

            Words.Add("BufFull", KeyLabel.PropertySound);
            Words.Add("BufHalf", KeyLabel.PropertySound);
            Words.Add("BufHave", KeyLabel.PropertySound);
            Words.Add("FillAfterVCM", KeyLabel.PropertySound);
            Words.Add("Resample", KeyLabel.PropertySound);

            Words.Add("AlwaysHave", KeyLabel.PropertyInput);
            Words.Add("STICK_CLAMP", KeyLabel.PropertyInput);
            Words.Add("StickLimit", KeyLabel.PropertyInput);
            Words.Add("StickModify", KeyLabel.PropertyInput);
            Words.Add("VPAD_STICK_CLAMP", KeyLabel.PropertyInput);

            Words.Add("GTaskDelay", KeyLabel.PropertyRSPG);
            Words.Add("RDPDelay", KeyLabel.PropertyRSPG);
            Words.Add("RDPInt", KeyLabel.PropertyRSPG);
            Words.Add("RIntAfterGTask", KeyLabel.PropertyRSPG);
            Words.Add("RSPGWaitOnlyFirstGTaskDelay", KeyLabel.PropertyRSPG);
            Words.Add("Skip", KeyLabel.PropertyRSPG);
            Words.Add("WaitDelay", KeyLabel.PropertyRSPG);
            Words.Add("WaitOnlyFirst", KeyLabel.PropertyRSPG);

            Words.Add("BlockSize", KeyLabel.PropertyCmp);
            Words.Add("CmpLimit", KeyLabel.PropertyCmp);
            Words.Add("FrameBlockLimit", KeyLabel.PropertyCmp);
            Words.Add("OptEnable", KeyLabel.PropertyCmp);
            Words.Add("W32OverlayCheck", KeyLabel.PropertyCmp);

            Words.Add("g_nN64CpuPC", KeyLabel.PropertyTempConfig);
            Words.Add("n64MemAcquireForground", KeyLabel.PropertyTempConfig);
            Words.Add("n64MemDefaultRead32MemTest", KeyLabel.PropertyTempConfig);
            Words.Add("n64MemReleaseForground", KeyLabel.PropertyTempConfig);
            Words.Add("RSPGDCFlush", KeyLabel.PropertyTempConfig);

            Words.Add("SIDelay", KeyLabel.PropertySI);
            Words.Add("ScanReadTime", KeyLabel.PropertyVI);
            Words.Add("Hack", KeyLabel.PropertyFrameTickHack);

            Words.Add("Count", KeyLabel.PropertyCount);
            Words.Add("Address", KeyLabel.PropertyAddress);
            Words.Add("Inst", KeyLabel.PropertyInst);
            Words.Add("Type", KeyLabel.PropertyType);
            Words.Add("Value", KeyLabel.PropertyValue);
            Words.Add("JmpPC", KeyLabel.PropertyJmpPC);

            Words.Add("VertexCount", KeyLabel.PropertyVertexCount);
            Words.Add("VertexAddress", KeyLabel.PropertyVertexAddress);
            Words.Add("FirstVertex", KeyLabel.PropertyFirstVertex);
            Words.Add("TextureAddress", KeyLabel.PropertyTextureAddress);
            Words.Add("SumPixel", KeyLabel.PropertySumPixel);
            Words.Add("Data2_", KeyLabel.PropertyData2);
            Words.Add("Data3_", KeyLabel.PropertyData3);
            Words.Add("AlphaTest", KeyLabel.PropertyAlphaTest);
            Words.Add("MagFilter", KeyLabel.PropertyMagFilter);
            Words.Add("OffsetS", KeyLabel.PropertyOffsetS);
            Words.Add("OffsetT", KeyLabel.PropertyOffsetT);

            Words.Add("_Addr", KeyLabel.PropertyCheatAddr);
            Words.Add("_Value", KeyLabel.PropertyCheatValue);
            Words.Add("_Bytes", KeyLabel.PropertyCheatBytes);

            Words.Add("a", KeyLabel.ByteArray);

            Source = source;
        }

        private void Read()
        {
            if (!Source.EndOfStream)
                Lookahead = Convert.ToChar(Source.Read());
            else
                Lookahead = '\0';
        }

        public Token GetNextToken()
        {
            for (; ; Read())
            {
                if (Lookahead == '\u000A')          // '\n' LF                
                    Line++;                
                else if (Lookahead == '\u0009' ||   // '\t' HT
                    Lookahead == '\u000D' ||        // '\r' CR
                    Lookahead == '\u0020')          // ' '  SP
                    continue;
                else if (Lookahead == ';')
                {
                    while (true)
                    {
                        Read();
                        if (Lookahead == '\u000A')  // '\n' LF
                        {
                            Line++;
                            break;
                        }
                        else if (Lookahead == '\0')
                            break;
                    }
                }
                else
                    break;
            }

            if (Lookahead == '"')
                return StringToken();

            if (char.IsLetter(Lookahead) || Lookahead == '_')
                return WordToken();

            if (char.IsDigit(Lookahead))
                return IntToken();

            Token token = new Token(Lookahead);
            Lookahead = ' ';
            return token;
        }

        private Token WordToken()
        {
            StringBuilder strBuilder = new StringBuilder();

            for (; ; Read())
            {
                if (char.IsLetter(Lookahead) || Lookahead == '_')
                    strBuilder.Append(Lookahead);
                else if (strBuilder.ToString() == "g_nN" && Lookahead == '6')
                    strBuilder.Append(Lookahead);
                else if (strBuilder.ToString() == "g_nN6" && Lookahead == '4')
                    strBuilder.Append(Lookahead);
                else if (strBuilder.ToString() == "n" && Lookahead == '6')
                    strBuilder.Append(Lookahead);
                else if (strBuilder.ToString() == "n6" && Lookahead == '4')
                    strBuilder.Append(Lookahead);
                else if (strBuilder.ToString() == "bForce" && Lookahead == '7')
                    strBuilder.Append(Lookahead);
                else if (strBuilder.ToString() == "bForce7" && Lookahead == '2')
                    strBuilder.Append(Lookahead);
                else if (strBuilder.ToString() == "bForce72" && Lookahead == '0')
                    strBuilder.Append(Lookahead);
                else if (strBuilder.ToString() == "W" && Lookahead == '3')
                    strBuilder.Append(Lookahead);
                else if (strBuilder.ToString() == "W3" && Lookahead == '2')
                    strBuilder.Append(Lookahead);
                else if (strBuilder.ToString() == "Data" && (Lookahead == '2' || Lookahead == '3'))
                    strBuilder.Append(Lookahead);
                else
                    break;
            }

            string str = strBuilder.ToString();
            object keyLabel = Words[str];

            if (keyLabel != null)
            {
                if ((int)keyLabel == KeyLabel.Section)
                    return new Word(str, KeyLabel.Section);
                else if ((int)keyLabel == KeyLabel.Cheat)
                    return new Word(str, KeyLabel.Cheat);
                else if ((int)keyLabel >= KeyLabel.Property &&
                         (int)keyLabel <= KeyLabel.PropertyCheatBytes)
                    return new Word(str, (int)keyLabel);
                else if ((int)keyLabel == KeyLabel.ByteArray)
                    return ByteArrayToken();
                else
                    throw new Exception("Lexical analyzer line " + Line.ToString() + ": Unexpected KeyLabel.");
            }

            Words.Add(str, KeyLabel.Property);
            return new Word(str, KeyLabel.Property);
        }

        private Token StringToken()
        {
            StringBuilder strBuilder = new StringBuilder();

            for (; ; )
            {
                Read();
                if (char.IsControl(Lookahead))
                    throw new Exception("Lexical analyzer line " + Line.ToString() + ": Found control character.");
                else if (Lookahead == '\\')
                {
                    Read();
                    switch (Lookahead)
                    {
                        case '"':
                            strBuilder.Append("\"");
                            break;
                        case '\\':
                            strBuilder.Append("\\");
                            break;
                        case '/':
                            strBuilder.Append("/");
                            break;
                        case 'b':
                            strBuilder.Append("\b");
                            break;
                        case 'f':
                            strBuilder.Append("\f");
                            break;
                        case 'n':
                            strBuilder.Append("\n");
                            break;
                        case 'r':
                            strBuilder.Append("\r");
                            break;
                        case 't':
                            strBuilder.Append("\t");
                            break;
                        default:
                            throw new Exception("Lexical analyzer line " + Line.ToString() + ": Unexpected escape sequence.");
                    }
                }
                else if (Lookahead != '"')
                    strBuilder.Append(Lookahead);
                else if (Lookahead == '"')
                {
                    Lookahead = ' ';
                    break;
                }
                else
                    throw new Exception("Lexical analyzer line " + Line.ToString() + ": Unexpected char '" + Lookahead.ToString() + "'.");
            }

            return new StringValue(strBuilder.ToString(), KeyLabel.String);
        }

        private Token IntToken()
        {
            StringBuilder strBuilder = new StringBuilder();

            if (Lookahead == '0')
            {
                Read();
                if (Lookahead == 'x')
                {
                    Read();
                    if (ByteArrayValue.IsHex(Lookahead))
                    {
                        strBuilder.Append(Lookahead);
                        Read();
                        for (int count = 1; ; Read())
                        {
                            if (ByteArrayValue.IsHex(Lookahead))
                                strBuilder.Append(Lookahead);
                            else
                                break;

                            if (count > 7)
                                throw new Exception("Lexical analyzer line " + Line.ToString() + ": Detected a hexadecimal number of more than 32 bits.");

                            count++;
                        }
                        return new IntValue(Convert.ToInt32(strBuilder.ToString(), 16), KeyLabel.IntHEX);
                    }
                    else
                        throw new Exception("Lexical analyzer line " + Line.ToString() + ": A hexadecimal number was expected.");
                }
                else if (char.IsDigit(Lookahead))
                    throw new Exception("Lexical analyzer line " + Line.ToString() + ": A number that starts with zero is invalid.");
                else
                    return new IntValue(0, KeyLabel.IntDEC);
            }
            else
            {
                strBuilder.Append(Lookahead);
                Read();
                for (; ; Read())
                {
                    if (char.IsDigit(Lookahead))
                        strBuilder.Append(Lookahead);
                    else
                        break;
                }
                return new IntValue(Convert.ToInt32(strBuilder.ToString()), KeyLabel.IntDEC);
            }
        }

        private Token ByteArrayToken()
        {
            StringBuilder strBuilder = new StringBuilder();

            for (; ; Read())
            {
                if (char.IsDigit(Lookahead))
                    strBuilder.Append(Lookahead);
                else
                    break;
            }

            int length = Convert.ToInt32(strBuilder.ToString());
            strBuilder.Remove(0, strBuilder.Length);
            length *= 2;

            if (Lookahead == ':')
            {
                Read();
                for (int count = 0; count < length; Read())
                {
                    if (ByteArrayValue.IsHex(Lookahead))
                    {
                        strBuilder.Append(Lookahead);
                        count++;
                    }
                    else if (Lookahead == '\u000A')     // '\n' LF                
                        Line++;
                    else if (Lookahead == '\u0009' ||   // '\t' HT
                        Lookahead == '\u000D' ||        // '\r' CR
                        Lookahead == '\u0020')          // ' '  SP
                        continue;
                    else if (Lookahead == ';')
                    {
                        while (true)
                        {
                            Read();
                            if (Lookahead == '\u000A')  // '\n' LF
                            {
                                Line++;
                                break;
                            }
                            else if (Lookahead == '\0')
                                break;
                        }
                    }
                    else
                        throw new Exception("Lexical analyzer line " + Line.ToString() + ": Found a non-hexadecimal value.");
                }
            }
            else
                throw new Exception("Lexical analyzer line " + Line.ToString() + ": The ':' character of starting an byte array was expected.");

            return new ByteArrayValue(strBuilder.ToString(), KeyLabel.ByteArray);
        }
    }
}
