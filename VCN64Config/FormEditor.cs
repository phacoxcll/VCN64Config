using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace VCN64Config
{
    public partial class FormEditor : Form
    {
        public const string Release = "1.1 debug"; //CllVersionReplace "major.minor stability"

        private int CountIdle;
        private int CountInsertIdleInst;
        private int CountSpecialInst;
        private int CountBreakBlockInst;
        private int CountRomHack;
        private int CountVertexHack;
        private int CountFilterHack;
        private string SavePath;
        private VCN64Config.File Config;

        public FormEditor()
        {
            InitializeComponent();

            this.Text = "VCN64Config Editor " + Release;

            buttonRomOption.BackColor = Color.FromArgb(16, 110, 190);
            comboBackupSize.Items.Add(VCN64Config.File.NullString);
            comboBackupSize.SelectedIndex = comboBackupSize.Items.Count - 1;
            comboBackupType.Items.Add(VCN64Config.File.NullString);
            comboBackupType.SelectedIndex = comboBackupSize.Items.Count - 1;
            comboPlayerNum.Items.Add(VCN64Config.File.NullString);
            comboPlayerNum.SelectedIndex = comboPlayerNum.Items.Count - 1;
            comboTicksPerFrame.Items.Add(VCN64Config.File.NullString);
            comboTicksPerFrame.SelectedIndex = comboTicksPerFrame.Items.Count - 1;
            comboBufFull.Items.Add(VCN64Config.File.NullString);
            comboBufFull.SelectedIndex = comboBufFull.Items.Count - 1;
            comboBufHalf.Items.Add(VCN64Config.File.NullString);
            comboBufHalf.SelectedIndex = comboBufHalf.Items.Count - 1;
            comboBufHave.Items.Add(VCN64Config.File.NullString);
            comboBufHave.SelectedIndex = comboBufHave.Items.Count - 1;
            comboFillAfterVCM.Items.Add(VCN64Config.File.NullString);
            comboFillAfterVCM.SelectedIndex = comboFillAfterVCM.Items.Count - 1;
            comboResample.Items.Add(VCN64Config.File.NullString);
            comboResample.SelectedIndex = comboResample.Items.Count - 1;
            comboStickLimit.Items.Add(VCN64Config.File.NullString);
            comboStickLimit.SelectedIndex = comboStickLimit.Items.Count - 1;
            comboStickModify.Items.Add(VCN64Config.File.NullString);
            comboStickModify.SelectedIndex = comboStickModify.Items.Count - 1;
            comboGTaskDelay.Items.Add(VCN64Config.File.NullString);
            comboGTaskDelay.SelectedIndex = comboGTaskDelay.Items.Count - 1;
            comboRDPDelay.Items.Add(VCN64Config.File.NullString);
            comboRDPDelay.SelectedIndex = comboRDPDelay.Items.Count - 1;
            comboBlockSize.Items.Add(VCN64Config.File.NullString);
            comboBlockSize.SelectedIndex = comboBlockSize.Items.Count - 1;
            comboSIDelay.Items.Add(VCN64Config.File.NullString);
            comboSIDelay.SelectedIndex = comboSIDelay.Items.Count - 1;

            dataGridViewConstValue.DefaultCellStyle.NullValue = VCN64Config.File.NullString;
            dataGridViewConstValue.Columns.Add("ConstValue", "ConstValue");
            dataGridViewConstValue.Columns["ConstValue"].ValueType = typeof(int);
            dataGridViewConstValue.Columns["ConstValue"].DefaultCellStyle.Format = "X";

            dataGridViewFrameTickHack.DefaultCellStyle.NullValue = VCN64Config.File.NullString;
            dataGridViewFrameTickHack.Columns.Add("FrameTickHack", "FrameTickHack");
            dataGridViewFrameTickHack.Columns["FrameTickHack"].ValueType = typeof(int);
            dataGridViewFrameTickHack.Columns["FrameTickHack"].Width = 136;

            dataGridViewIdle.DefaultCellStyle.NullValue = VCN64Config.File.NullString;
            dataGridViewIdle.Columns.Add("Index", "Index");
            dataGridViewIdle.Columns.Add("Address", "Address (hex)");
            dataGridViewIdle.Columns.Add("Inst", "Inst (hex)");
            dataGridViewIdle.Columns.Add("Type", "Type (dec)");
            dataGridViewIdle.Columns["Index"].ValueType = typeof(int);
            dataGridViewIdle.Columns["Address"].ValueType = typeof(int);
            dataGridViewIdle.Columns["Inst"].ValueType = typeof(int);
            dataGridViewIdle.Columns["Type"].ValueType = typeof(int);
            dataGridViewIdle.Columns["Index"].ReadOnly = true;
            dataGridViewIdle.Columns["Index"].Frozen = true;
            dataGridViewIdle.Columns["Index"].Width = 32;
            dataGridViewIdle.Columns["Address"].DefaultCellStyle.Format = "X";
            dataGridViewIdle.Columns["Inst"].DefaultCellStyle.Format = "X";

            dataGridViewInsertIdleInst.DefaultCellStyle.NullValue = VCN64Config.File.NullString;
            dataGridViewInsertIdleInst.Columns.Add("Index", "Index");
            dataGridViewInsertIdleInst.Columns.Add("Address", "Address (hex)");
            dataGridViewInsertIdleInst.Columns.Add("Inst", "Inst (hex)");
            dataGridViewInsertIdleInst.Columns.Add("Type", "Type (hex)");
            dataGridViewInsertIdleInst.Columns.Add("Value", "Value (hex)");
            dataGridViewInsertIdleInst.Columns["Index"].ValueType = typeof(int);
            dataGridViewInsertIdleInst.Columns["Address"].ValueType = typeof(int);
            dataGridViewInsertIdleInst.Columns["Inst"].ValueType = typeof(int);
            dataGridViewInsertIdleInst.Columns["Type"].ValueType = typeof(int);
            dataGridViewInsertIdleInst.Columns["Value"].ValueType = typeof(int);
            dataGridViewInsertIdleInst.Columns["Index"].ReadOnly = true;
            dataGridViewInsertIdleInst.Columns["Index"].Frozen = true;
            dataGridViewInsertIdleInst.Columns["Index"].Width = 32;
            dataGridViewInsertIdleInst.Columns["Address"].DefaultCellStyle.Format = "X";
            dataGridViewInsertIdleInst.Columns["Inst"].DefaultCellStyle.Format = "X";
            dataGridViewInsertIdleInst.Columns["Type"].DefaultCellStyle.Format = "X";
            dataGridViewInsertIdleInst.Columns["Value"].DefaultCellStyle.Format = "X";

            dataGridViewSpecialInst.DefaultCellStyle.NullValue = VCN64Config.File.NullString;
            dataGridViewSpecialInst.Columns.Add("Index", "Index");
            dataGridViewSpecialInst.Columns.Add("Address", "Address (hex)");
            dataGridViewSpecialInst.Columns.Add("Inst", "Inst (hex)");
            dataGridViewSpecialInst.Columns.Add("Type", "Type (dec)");
            dataGridViewSpecialInst.Columns.Add("Value", "Value (hex)");
            dataGridViewSpecialInst.Columns["Index"].ValueType = typeof(int);
            dataGridViewSpecialInst.Columns["Address"].ValueType = typeof(int);
            dataGridViewSpecialInst.Columns["Inst"].ValueType = typeof(int);
            dataGridViewSpecialInst.Columns["Type"].ValueType = typeof(int);
            dataGridViewSpecialInst.Columns["Value"].ValueType = typeof(int);
            dataGridViewSpecialInst.Columns["Index"].ReadOnly = true;
            dataGridViewSpecialInst.Columns["Index"].Frozen = true;
            dataGridViewSpecialInst.Columns["Index"].Width = 32;
            dataGridViewSpecialInst.Columns["Address"].DefaultCellStyle.Format = "X";
            dataGridViewSpecialInst.Columns["Inst"].DefaultCellStyle.Format = "X";
            dataGridViewSpecialInst.Columns["Value"].DefaultCellStyle.Format = "X";

            dataGridViewBreakBlockInst.DefaultCellStyle.NullValue = VCN64Config.File.NullString;
            dataGridViewBreakBlockInst.Columns.Add("Index", "Index");
            dataGridViewBreakBlockInst.Columns.Add("Address", "Address (hex)");
            dataGridViewBreakBlockInst.Columns.Add("Inst", "Inst (hex)");
            dataGridViewBreakBlockInst.Columns.Add("Type", "Type (dec)");
            dataGridViewBreakBlockInst.Columns.Add("JmpPC", "JmpPC (hex)");
            dataGridViewBreakBlockInst.Columns["Index"].ValueType = typeof(int);
            dataGridViewBreakBlockInst.Columns["Address"].ValueType = typeof(int);
            dataGridViewBreakBlockInst.Columns["Inst"].ValueType = typeof(int);
            dataGridViewBreakBlockInst.Columns["Type"].ValueType = typeof(int);
            dataGridViewBreakBlockInst.Columns["JmpPC"].ValueType = typeof(int);
            dataGridViewBreakBlockInst.Columns["Index"].ReadOnly = true;
            dataGridViewBreakBlockInst.Columns["Index"].Frozen = true;
            dataGridViewBreakBlockInst.Columns["Index"].Width = 32;
            dataGridViewBreakBlockInst.Columns["Address"].DefaultCellStyle.Format = "X";
            dataGridViewBreakBlockInst.Columns["Inst"].DefaultCellStyle.Format = "X";
            dataGridViewBreakBlockInst.Columns["JmpPC"].DefaultCellStyle.Format = "X";

            dataGridViewRomHack.DefaultCellStyle.NullValue = VCN64Config.File.NullString;
            dataGridViewRomHack.Columns.Add("Index", "Index");
            dataGridViewRomHack.Columns.Add("Address", "Address (hex)");
            dataGridViewRomHack.Columns.Add("Type", "Type (dec)");
            dataGridViewRomHack.Columns.Add("Value", "Value\n(byte array)");
            dataGridViewRomHack.Columns["Index"].ValueType = typeof(int);
            dataGridViewRomHack.Columns["Address"].ValueType = typeof(int);
            dataGridViewRomHack.Columns["Type"].ValueType = typeof(int);
            dataGridViewRomHack.Columns["Value"].ValueType = typeof(string);
            dataGridViewRomHack.Columns["Index"].ReadOnly = true;
            dataGridViewRomHack.Columns["Index"].Frozen = true;
            dataGridViewRomHack.Columns["Index"].Width = 32;
            dataGridViewRomHack.Columns["Address"].DefaultCellStyle.Format = "X";

            dataGridViewVertexHack.DefaultCellStyle.NullValue = VCN64Config.File.NullString;
            dataGridViewVertexHack.Columns.Add("Index", "Index");
            dataGridViewVertexHack.Columns.Add("VertexCount", "VertexCount\n(dec)");
            dataGridViewVertexHack.Columns.Add("VertexAddress", "VertexAddress\n(hex)");
            dataGridViewVertexHack.Columns.Add("TextureAddress", "TextureAddress\n(hex)");
            dataGridViewVertexHack.Columns.Add("FirstVertex", "FirstVertex\n(byte array)");
            dataGridViewVertexHack.Columns.Add("Value", "Value\n(byte array)");
            dataGridViewVertexHack.Columns["Index"].ValueType = typeof(int);
            dataGridViewVertexHack.Columns["VertexCount"].ValueType = typeof(int);
            dataGridViewVertexHack.Columns["VertexAddress"].ValueType = typeof(int);
            dataGridViewVertexHack.Columns["TextureAddress"].ValueType = typeof(int);
            dataGridViewVertexHack.Columns["FirstVertex"].ValueType = typeof(string);
            dataGridViewVertexHack.Columns["Value"].ValueType = typeof(string);
            dataGridViewVertexHack.Columns["Index"].ReadOnly = true;
            dataGridViewVertexHack.Columns["Index"].Frozen = true;
            dataGridViewVertexHack.Columns["Index"].Width = 32;
            dataGridViewVertexHack.Columns["VertexAddress"].DefaultCellStyle.Format = "X";
            dataGridViewVertexHack.Columns["TextureAddress"].DefaultCellStyle.Format = "X";

            dataGridViewFilterHack.DefaultCellStyle.NullValue = VCN64Config.File.NullString;
            dataGridViewFilterHack.Columns.Add("Index", "Index");
            dataGridViewFilterHack.Columns.Add("TextureAddress", "TextureAddress\n(hex)");
            dataGridViewFilterHack.Columns.Add("SumPixel", "SumPixel\n(hex)");
            dataGridViewFilterHack.Columns.Add("Data2", "Data2\n(hex)");
            dataGridViewFilterHack.Columns.Add("Data3", "Data3\n(hex)");
            dataGridViewFilterHack.Columns.Add("AlphaTest", "AlphaTest\n(dec)");
            dataGridViewFilterHack.Columns.Add("MagFilter", "MagFilter\n(dec)");
            dataGridViewFilterHack.Columns.Add("OffsetS", "OffsetS\n(hex)");
            dataGridViewFilterHack.Columns.Add("OffsetT", "OffsetT\n(hex)");
            dataGridViewFilterHack.Columns["Index"].ValueType = typeof(int);
            dataGridViewFilterHack.Columns["TextureAddress"].ValueType = typeof(int);
            dataGridViewFilterHack.Columns["SumPixel"].ValueType = typeof(int);
            dataGridViewFilterHack.Columns["Data2"].ValueType = typeof(int);
            dataGridViewFilterHack.Columns["Data3"].ValueType = typeof(int);
            dataGridViewFilterHack.Columns["AlphaTest"].ValueType = typeof(int);
            dataGridViewFilterHack.Columns["MagFilter"].ValueType = typeof(int);
            dataGridViewFilterHack.Columns["OffsetS"].ValueType = typeof(int);
            dataGridViewFilterHack.Columns["OffsetT"].ValueType = typeof(int);
            dataGridViewFilterHack.Columns["Index"].ReadOnly = true;
            dataGridViewFilterHack.Columns["Index"].Frozen = true;
            dataGridViewFilterHack.Columns["Index"].Width = 32;
            dataGridViewFilterHack.Columns["TextureAddress"].DefaultCellStyle.Format = "X";
            dataGridViewFilterHack.Columns["SumPixel"].DefaultCellStyle.Format = "X";
            dataGridViewFilterHack.Columns["Data2"].DefaultCellStyle.Format = "X";
            dataGridViewFilterHack.Columns["Data3"].DefaultCellStyle.Format = "X";
            dataGridViewFilterHack.Columns["OffsetS"].DefaultCellStyle.Format = "X";
            dataGridViewFilterHack.Columns["OffsetT"].DefaultCellStyle.Format = "X";

            dataGridViewCheat.DefaultCellStyle.NullValue = VCN64Config.File.NullString;
            dataGridViewCheat.Columns.Add("Index", "Index");
            dataGridViewCheat.Columns.Add("N", "N (dec)");
            dataGridViewCheat.Columns.Add("Addr", "Addr (hex)");
            dataGridViewCheat.Columns.Add("Value", "Value (hex)");
            dataGridViewCheat.Columns.Add("Bytes", "Bytes (dec)");
            dataGridViewCheat.Columns["Index"].ValueType = typeof(int);
            dataGridViewCheat.Columns["N"].ValueType = typeof(int);
            dataGridViewCheat.Columns["Addr"].ValueType = typeof(int);
            dataGridViewCheat.Columns["Value"].ValueType = typeof(int);
            dataGridViewCheat.Columns["Bytes"].ValueType = typeof(int);
            dataGridViewCheat.Columns["Index"].ReadOnly = true;
            dataGridViewCheat.Columns["Index"].Frozen = true;
            dataGridViewCheat.Columns["Index"].Width = 32;
            dataGridViewCheat.Columns["Addr"].DefaultCellStyle.Format = "X";
            dataGridViewCheat.Columns["Value"].DefaultCellStyle.Format = "X";

            CountIdle = 0;
            CountInsertIdleInst = 0;
            CountSpecialInst = 0;
            CountBreakBlockInst = 0;
            CountRomHack = 0;
            CountVertexHack = 0;
            CountFilterHack = 0;
            SavePath = null;
            Config = null;

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length == 3 && args[1] == "open")
                FileOpen(args[2]);
            else if (args.Length == 5 && args[1] == "dialog")
                EnableDialogMode(args[2], args[3], args[4]);
            else if (args.Length > 1)            
                MessageBox.Show("Use: open <input path>\nOr use: validate <input path>\nOr use: dialog <input path> <output path> <desciption>",
                    "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);            
        }

        public DialogResult ShowDialog(string input, string output, string description)
        {
            EnableDialogMode(input, output, description);
            return ShowDialog();
        }

        private void EnableDialogMode(string input, string output, string description)
        {
            if (input.Length == 0)
            {
                Config = new VCN64Config.File();
                LoadRomOption();
                LoadRender();
                LoadOthers();         
            }
            else
                FileOpen(input);

            if (description.Length > 0)
                textBoxDescription.Text = description;

            string path = null;
            string filename = null;
            try
            {
                path = Path.GetDirectoryName(output);
                filename = Path.GetFileName(output);
                if (!Directory.Exists(path))
                    MessageBox.Show("The output folder \"" + path + "\" does not exist, dialog mode disabled.",
                       "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (filename.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                    MessageBox.Show("The output file name \"" + filename + "\" is invalid, dialog mode disabled.",
                       "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    SavePath = output;
                    menuStrip.Visible = false;                    
                    buttonOK.Visible = true;
                    //textBoxDescription.Enabled = false;
                }
            }
            catch
            {
                MessageBox.Show("The output path is invalid, dialog mode disabled.",
                    "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }  

        #region Menu Buttons

        private void ButtonRomOption_Click(object sender, EventArgs e)
        {
            buttonRomOption.BackColor = Color.FromArgb(16, 110, 190);
            buttonRender.BackColor = Color.FromArgb(17, 17, 17);
            buttonOthers.BackColor = Color.FromArgb(17, 17, 17);
            buttonIdle.BackColor = Color.FromArgb(17, 17, 17);
            buttonInsertIdleInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonSpecialInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonBreakBlockInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonRomHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonVertexHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonFilterHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonCheat.BackColor = Color.FromArgb(17, 17, 17);
            panelRomOption.Visible = true;
            panelRender.Visible = false;
            panelOthers.Visible = false;
            dataGridViewIdle.Visible = false;
            labelCountIdle.Visible = false;
            dataGridViewInsertIdleInst.Visible = false;
            labelCountInsertIdleInst.Visible = false;
            dataGridViewSpecialInst.Visible = false;
            labelCountSpecialInst.Visible = false;
            dataGridViewBreakBlockInst.Visible = false;
            labelCountBreakBlockInst.Visible = false;
            dataGridViewRomHack.Visible = false;
            labelCountRomHack.Visible = false;
            dataGridViewVertexHack.Visible = false;
            labelCountVertexHack.Visible = false;
            dataGridViewFilterHack.Visible = false;
            labelCountFilterHack.Visible = false;
            dataGridViewCheat.Visible = false;
            labelBy.Visible = false;
        }

        private void ButtonRender_Click(object sender, EventArgs e)
        {
            buttonRomOption.BackColor = Color.FromArgb(17, 17, 17);
            buttonRender.BackColor = Color.FromArgb(16, 110, 190);
            buttonOthers.BackColor = Color.FromArgb(17, 17, 17);
            buttonIdle.BackColor = Color.FromArgb(17, 17, 17);
            buttonInsertIdleInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonSpecialInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonBreakBlockInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonRomHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonVertexHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonFilterHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonCheat.BackColor = Color.FromArgb(17, 17, 17);
            panelRomOption.Visible = false;
            panelRender.Visible = true;
            panelOthers.Visible = false;
            dataGridViewIdle.Visible = false;
            labelCountIdle.Visible = false;
            dataGridViewInsertIdleInst.Visible = false;
            labelCountInsertIdleInst.Visible = false;
            dataGridViewSpecialInst.Visible = false;
            labelCountSpecialInst.Visible = false;
            dataGridViewBreakBlockInst.Visible = false;
            labelCountBreakBlockInst.Visible = false;
            dataGridViewRomHack.Visible = false;
            labelCountRomHack.Visible = false;
            dataGridViewVertexHack.Visible = false;
            labelCountVertexHack.Visible = false;
            dataGridViewFilterHack.Visible = false;
            labelCountFilterHack.Visible = false;
            dataGridViewCheat.Visible = false;
            labelBy.Visible = false;
        }

        private void ButtonOthers_Click(object sender, EventArgs e)
        {
            buttonRomOption.BackColor = Color.FromArgb(17, 17, 17);
            buttonRender.BackColor = Color.FromArgb(17, 17, 17);
            buttonOthers.BackColor = Color.FromArgb(16, 110, 190);
            buttonIdle.BackColor = Color.FromArgb(17, 17, 17);
            buttonInsertIdleInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonSpecialInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonBreakBlockInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonRomHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonVertexHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonFilterHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonCheat.BackColor = Color.FromArgb(17, 17, 17);
            panelRomOption.Visible = false;
            panelRender.Visible = false;
            panelOthers.Visible = true;
            dataGridViewIdle.Visible = false;
            labelCountIdle.Visible = false;
            dataGridViewInsertIdleInst.Visible = false;
            labelCountInsertIdleInst.Visible = false;
            dataGridViewSpecialInst.Visible = false;
            labelCountSpecialInst.Visible = false;
            dataGridViewBreakBlockInst.Visible = false;
            labelCountBreakBlockInst.Visible = false;
            dataGridViewRomHack.Visible = false;
            labelCountRomHack.Visible = false;
            dataGridViewVertexHack.Visible = false;
            labelCountVertexHack.Visible = false;
            dataGridViewFilterHack.Visible = false;
            labelCountFilterHack.Visible = false;
            dataGridViewCheat.Visible = false;
            labelBy.Visible = false;
        }

        private void ButtonIdle_Click(object sender, EventArgs e)
        {
            buttonRomOption.BackColor = Color.FromArgb(17, 17, 17);
            buttonRender.BackColor = Color.FromArgb(17, 17, 17);
            buttonOthers.BackColor = Color.FromArgb(17, 17, 17);
            buttonIdle.BackColor = Color.FromArgb(16, 110, 190);
            buttonInsertIdleInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonSpecialInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonBreakBlockInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonRomHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonVertexHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonFilterHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonCheat.BackColor = Color.FromArgb(17, 17, 17);
            panelRomOption.Visible = false;
            panelRender.Visible = false;
            panelOthers.Visible = false;
            dataGridViewIdle.Visible = true;
            labelCountIdle.Visible = true;
            dataGridViewInsertIdleInst.Visible = false;
            labelCountInsertIdleInst.Visible = false;
            dataGridViewSpecialInst.Visible = false;
            labelCountSpecialInst.Visible = false;
            dataGridViewBreakBlockInst.Visible = false;
            labelCountBreakBlockInst.Visible = false;
            dataGridViewRomHack.Visible = false;
            labelCountRomHack.Visible = false;
            dataGridViewVertexHack.Visible = false;
            labelCountVertexHack.Visible = false;
            dataGridViewFilterHack.Visible = false;
            labelCountFilterHack.Visible = false;
            dataGridViewCheat.Visible = false;
            labelBy.Visible = false;
        }

        private void ButtonInsertIdleInst_Click(object sender, EventArgs e)
        {
            buttonRomOption.BackColor = Color.FromArgb(17, 17, 17);
            buttonRender.BackColor = Color.FromArgb(17, 17, 17);
            buttonOthers.BackColor = Color.FromArgb(17, 17, 17);
            buttonIdle.BackColor = Color.FromArgb(17, 17, 17);
            buttonInsertIdleInst.BackColor = Color.FromArgb(16, 110, 190);
            buttonSpecialInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonBreakBlockInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonRomHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonVertexHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonFilterHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonCheat.BackColor = Color.FromArgb(17, 17, 17);
            panelRomOption.Visible = false;
            panelRender.Visible = false;
            panelOthers.Visible = false;
            dataGridViewIdle.Visible = false;
            labelCountIdle.Visible = false;
            dataGridViewInsertIdleInst.Visible = true;
            labelCountInsertIdleInst.Visible = true;
            dataGridViewSpecialInst.Visible = false;
            labelCountSpecialInst.Visible = false;
            dataGridViewBreakBlockInst.Visible = false;
            labelCountBreakBlockInst.Visible = false;
            dataGridViewRomHack.Visible = false;
            labelCountRomHack.Visible = false;
            dataGridViewVertexHack.Visible = false;
            labelCountVertexHack.Visible = false;
            dataGridViewFilterHack.Visible = false;
            labelCountFilterHack.Visible = false;
            dataGridViewCheat.Visible = false;
            labelBy.Visible = false;
        }

        private void ButtonSpecialInst_Click(object sender, EventArgs e)
        {
            buttonRomOption.BackColor = Color.FromArgb(17, 17, 17);
            buttonRender.BackColor = Color.FromArgb(17, 17, 17);
            buttonOthers.BackColor = Color.FromArgb(17, 17, 17);
            buttonIdle.BackColor = Color.FromArgb(17, 17, 17);
            buttonInsertIdleInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonSpecialInst.BackColor = Color.FromArgb(16, 110, 190);
            buttonBreakBlockInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonRomHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonVertexHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonFilterHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonCheat.BackColor = Color.FromArgb(17, 17, 17);
            panelRomOption.Visible = false;
            panelRender.Visible = false;
            panelOthers.Visible = false;
            dataGridViewIdle.Visible = false;
            labelCountIdle.Visible = false;
            dataGridViewInsertIdleInst.Visible = false;
            labelCountInsertIdleInst.Visible = false;
            dataGridViewSpecialInst.Visible = true;
            labelCountSpecialInst.Visible = true;
            dataGridViewBreakBlockInst.Visible = false;
            labelCountBreakBlockInst.Visible = false;
            dataGridViewRomHack.Visible = false;
            labelCountRomHack.Visible = false;
            dataGridViewVertexHack.Visible = false;
            labelCountVertexHack.Visible = false;
            dataGridViewFilterHack.Visible = false;
            labelCountFilterHack.Visible = false;
            dataGridViewCheat.Visible = false;
            labelBy.Visible = false;
        }

        private void ButtonBreakBlockInst_Click(object sender, EventArgs e)
        {
            buttonRomOption.BackColor = Color.FromArgb(17, 17, 17);
            buttonRender.BackColor = Color.FromArgb(17, 17, 17);
            buttonOthers.BackColor = Color.FromArgb(17, 17, 17);
            buttonIdle.BackColor = Color.FromArgb(17, 17, 17);
            buttonInsertIdleInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonSpecialInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonBreakBlockInst.BackColor = Color.FromArgb(16, 110, 190);
            buttonRomHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonVertexHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonFilterHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonCheat.BackColor = Color.FromArgb(17, 17, 17);
            panelRomOption.Visible = false;
            panelRender.Visible = false;
            panelOthers.Visible = false;
            dataGridViewIdle.Visible = false;
            labelCountIdle.Visible = false;
            dataGridViewInsertIdleInst.Visible = false;
            labelCountInsertIdleInst.Visible = false;
            dataGridViewSpecialInst.Visible = false;
            labelCountSpecialInst.Visible = false;
            dataGridViewBreakBlockInst.Visible = true;
            labelCountBreakBlockInst.Visible = true;
            dataGridViewRomHack.Visible = false;
            labelCountRomHack.Visible = false;
            dataGridViewVertexHack.Visible = false;
            labelCountVertexHack.Visible = false;
            dataGridViewFilterHack.Visible = false;
            labelCountFilterHack.Visible = false;
            dataGridViewCheat.Visible = false;
            labelBy.Visible = false;
        }

        private void ButtonRomHack_Click(object sender, EventArgs e)
        {
            buttonRomOption.BackColor = Color.FromArgb(17, 17, 17);
            buttonRender.BackColor = Color.FromArgb(17, 17, 17);
            buttonOthers.BackColor = Color.FromArgb(17, 17, 17);
            buttonIdle.BackColor = Color.FromArgb(17, 17, 17);
            buttonInsertIdleInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonSpecialInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonBreakBlockInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonRomHack.BackColor = Color.FromArgb(16, 110, 190);
            buttonVertexHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonFilterHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonCheat.BackColor = Color.FromArgb(17, 17, 17);
            panelRomOption.Visible = false;
            panelRender.Visible = false;
            panelOthers.Visible = false;
            dataGridViewIdle.Visible = false;
            labelCountIdle.Visible = false;
            dataGridViewInsertIdleInst.Visible = false;
            labelCountInsertIdleInst.Visible = false;
            dataGridViewSpecialInst.Visible = false;
            labelCountSpecialInst.Visible = false;
            dataGridViewBreakBlockInst.Visible = false;
            labelCountBreakBlockInst.Visible = false;
            dataGridViewRomHack.Visible = true;
            labelCountRomHack.Visible = true;
            dataGridViewVertexHack.Visible = false;
            labelCountVertexHack.Visible = false;
            dataGridViewFilterHack.Visible = false;
            labelCountFilterHack.Visible = false;
            dataGridViewCheat.Visible = false;
            labelBy.Visible = false;
        }

        private void ButtonVertexHack_Click(object sender, EventArgs e)
        {
            buttonRomOption.BackColor = Color.FromArgb(17, 17, 17);
            buttonRender.BackColor = Color.FromArgb(17, 17, 17);
            buttonOthers.BackColor = Color.FromArgb(17, 17, 17);
            buttonIdle.BackColor = Color.FromArgb(17, 17, 17);
            buttonInsertIdleInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonSpecialInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonBreakBlockInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonRomHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonVertexHack.BackColor = Color.FromArgb(16, 110, 190);
            buttonFilterHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonCheat.BackColor = Color.FromArgb(17, 17, 17);
            panelRomOption.Visible = false;
            panelRender.Visible = false;
            panelOthers.Visible = false;
            dataGridViewIdle.Visible = false;
            labelCountIdle.Visible = false;
            dataGridViewInsertIdleInst.Visible = false;
            labelCountInsertIdleInst.Visible = false;
            dataGridViewSpecialInst.Visible = false;
            labelCountSpecialInst.Visible = false;
            dataGridViewBreakBlockInst.Visible = false;
            labelCountBreakBlockInst.Visible = false;
            dataGridViewRomHack.Visible = false;
            labelCountRomHack.Visible = false;
            dataGridViewVertexHack.Visible = true;
            labelCountVertexHack.Visible = true;
            dataGridViewFilterHack.Visible = false;
            labelCountFilterHack.Visible = false;
            dataGridViewCheat.Visible = false;
            labelBy.Visible = false;
        }

        private void ButtonFilterHack_Click(object sender, EventArgs e)
        {
            buttonRomOption.BackColor = Color.FromArgb(17, 17, 17);
            buttonRender.BackColor = Color.FromArgb(17, 17, 17);
            buttonOthers.BackColor = Color.FromArgb(17, 17, 17);
            buttonIdle.BackColor = Color.FromArgb(17, 17, 17);
            buttonInsertIdleInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonSpecialInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonBreakBlockInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonRomHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonVertexHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonFilterHack.BackColor = Color.FromArgb(16, 110, 190);
            buttonCheat.BackColor = Color.FromArgb(17, 17, 17);
            panelRomOption.Visible = false;
            panelRender.Visible = false;
            panelOthers.Visible = false;
            dataGridViewIdle.Visible = false;
            labelCountIdle.Visible = false;
            dataGridViewInsertIdleInst.Visible = false;
            labelCountInsertIdleInst.Visible = false;
            dataGridViewSpecialInst.Visible = false;
            labelCountSpecialInst.Visible = false;
            dataGridViewBreakBlockInst.Visible = false;
            labelCountBreakBlockInst.Visible = false;
            dataGridViewRomHack.Visible = false;
            labelCountRomHack.Visible = false;
            dataGridViewVertexHack.Visible = false;
            labelCountVertexHack.Visible = false;
            dataGridViewFilterHack.Visible = true;
            labelCountFilterHack.Visible = true;
            dataGridViewCheat.Visible = false;
            labelBy.Visible = false;
        }

        private void ButtonCheat_Click(object sender, EventArgs e)
        {
            buttonRomOption.BackColor = Color.FromArgb(17, 17, 17);
            buttonRender.BackColor = Color.FromArgb(17, 17, 17);
            buttonOthers.BackColor = Color.FromArgb(17, 17, 17);
            buttonIdle.BackColor = Color.FromArgb(17, 17, 17);
            buttonInsertIdleInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonSpecialInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonBreakBlockInst.BackColor = Color.FromArgb(17, 17, 17);
            buttonRomHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonVertexHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonFilterHack.BackColor = Color.FromArgb(17, 17, 17);
            buttonCheat.BackColor = Color.FromArgb(16, 110, 190);
            panelRomOption.Visible = false;
            panelRender.Visible = false;
            panelOthers.Visible = false;
            dataGridViewIdle.Visible = false;
            labelCountIdle.Visible = false;
            dataGridViewInsertIdleInst.Visible = false;
            labelCountInsertIdleInst.Visible = false;
            dataGridViewSpecialInst.Visible = false;
            labelCountSpecialInst.Visible = false;
            dataGridViewBreakBlockInst.Visible = false;
            labelCountBreakBlockInst.Visible = false;
            dataGridViewRomHack.Visible = false;
            labelCountRomHack.Visible = false;
            dataGridViewVertexHack.Visible = false;
            labelCountVertexHack.Visible = false;
            dataGridViewFilterHack.Visible = false;
            labelCountFilterHack.Visible = false;
            dataGridViewCheat.Visible = true;
            labelBy.Visible = true;
        }

        #endregion

        #region Tool Strip Menu

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save the current config?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                saveFileDialog.FileName = "";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (FileSave(saveFileDialog.FileName))
                    {
                        Config = new VCN64Config.File();
                        LoadRomOption();
                        LoadRender();
                        LoadOthers();
                        this.Text = "VCN64Config Editor " + Release;
                        textBoxDescription.Text = "";
                        SavePath = null;
                    }
                    else
                    {
                        SavePath = null;
                        MessageBox.Show("Not saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else if (result == DialogResult.No)
            {
                Config = new VCN64Config.File();
                LoadRomOption();
                LoadRender();
                LoadOthers();
                this.Text = "VCN64Config Editor " + Release;
                textBoxDescription.Text = "";
                SavePath = null;
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save the current config?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                saveFileDialog.FileName = "";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (FileSave(saveFileDialog.FileName))
                    {
                        SavePath = saveFileDialog.FileName;
                        openFileDialog.FileName = "";
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            if (FileOpen(openFileDialog.FileName))
                                SavePath = openFileDialog.FileName;
                            else
                                SavePath = null;
                        }
                    }
                    else
                    {
                        SavePath = null;
                        MessageBox.Show("Not saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else if (result == DialogResult.No)
            {
                openFileDialog.FileName = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (FileOpen(openFileDialog.FileName))
                        SavePath = openFileDialog.FileName;
                    else
                        SavePath = null;
                }
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SavePath == null)
            {
                saveFileDialog.FileName = "";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (FileSave(saveFileDialog.FileName))
                        SavePath = saveFileDialog.FileName;
                    else
                    {
                        SavePath = null;
                        MessageBox.Show("Not saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                if (FileSave(SavePath))
                    SavePath = saveFileDialog.FileName;
                else
                {
                    SavePath = null;
                    MessageBox.Show("Not saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = "";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (FileSave(saveFileDialog.FileName))
                    SavePath = saveFileDialog.FileName;
                else
                {
                    SavePath = null;
                    MessageBox.Show("Not saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(SavePath))
            {
                if (MessageBox.Show("The file \"" + SavePath + "\" exists.\nDo you want to overwrite it?",
                    "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    System.IO.File.Delete(SavePath);
                    FileSave(SavePath);
                    this.Close();
                }
            }
            else
            {
                FileSave(SavePath);
                this.Close();
            }
        }

        private bool FileOpen(string filename)
        {
            bool result = false;

            if (System.IO.File.Exists(filename))
            {
                this.Text = "VCN64Config Editor " + Release;
                textBoxDescription.Text = "";
                StreamReader source = null;
                try
                {
                    source = System.IO.File.OpenText(filename);
                    string desc = source.ReadLine();
                    source.Close();
                    source = System.IO.File.OpenText(filename);
                    Syn analizer = new Syn(source);
                    Config = analizer.Run();
                    LoadRomOption();
                    LoadRender();
                    LoadOthers();
                    if (desc != null && desc.Length > 0 && desc[0] == ';')
                        textBoxDescription.Text = desc.Substring(1, desc.Length - 1);
                    this.Text = "VCN64Config Editor " + Release + " :: " + Path.GetFileName(filename);
                    result = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("The file \"" + filename + "\" is invalid.\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (source != null)
                        source.Close();
                }
            }
            else
                MessageBox.Show("The file \"" + filename + "\" not exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return result;
        }

        private bool FileSave(string filename)
        {
            bool result = false;
            this.Text = "VCN64Config Editor " + Release;
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(filename);
                SaveRomOption(sw);
                SaveRender(sw);
                SaveOthers(sw);
                this.Text = "VCN64Config Editor " + Release + " :: " + Path.GetFileName(filename);
                result = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error saving file \"" + filename + "\".\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
            return result;
        }

        private void SaveRomOption(StreamWriter sw)
        {
            sw.WriteLine(";" + textBoxDescription.Text);

            sw.WriteLine("[RomOption]");

            if (checkAIIntPerFrame.CheckState == CheckState.Checked)
                sw.WriteLine("AIIntPerFrame = 1");
            else if (checkAIIntPerFrame.CheckState == CheckState.Unchecked)
                sw.WriteLine("AIIntPerFrame = 0");
            else
                sw.WriteLine(";AIIntPerFrame = 0");

            if (checkAIUseTimer.CheckState == CheckState.Checked)
                sw.WriteLine("AIUseTimer = 1");
            else if (checkAIUseTimer.CheckState == CheckState.Unchecked)
                sw.WriteLine("AIUseTimer = 0");
            else
                sw.WriteLine(";AIUseTimer = 0");

            switch (comboBackupSize.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("BackupSize = 512"); //4 Kibits
                    break;
                case 1:
                    sw.WriteLine("BackupSize = 2048"); //16 Kibits
                    break;
                case 2:
                    sw.WriteLine("BackupSize = 32768"); //256 Kibits
                    break;
                case 3:
                    sw.WriteLine("BackupSize = 131072"); //1024 Kibits
                    break;
                default:
                    sw.WriteLine(";BackupSize = ");
                    break;
            }

            switch (comboBackupType.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("BackupType = 0"); //Auto
                    break;
                case 1:
                    sw.WriteLine("BackupType = 1"); //SRAM
                    break;
                case 2:
                    sw.WriteLine("BackupType = 2"); //Flash
                    break;
                case 3:
                    sw.WriteLine("BackupType = 3"); //EEPROM
                    break;
                default:
                    sw.WriteLine(";BackupType = ");
                    break;
            }

            if (checkBootPCChange.CheckState == CheckState.Checked)
                sw.WriteLine("BootPCChange = 1");
            else if (checkBootPCChange.CheckState == CheckState.Unchecked)
                sw.WriteLine("BootPCChange = 0");
            else
                sw.WriteLine(";BootPCChange = 0");

            if (checkCmpBlockAdvFlag.CheckState == CheckState.Checked)
                sw.WriteLine("CmpBlockAdvFlag = 1");
            else if (checkCmpBlockAdvFlag.CheckState == CheckState.Unchecked)
                sw.WriteLine("CmpBlockAdvFlag = 0");
            else
                sw.WriteLine(";CmpBlockAdvFlag = 0");

            if (checkEEROMInitValue.Checked)
                sw.WriteLine("EEROMInitValue = 0xFF");
            else
                sw.WriteLine(";EEROMInitValue = 0xFF");

            if (checkMemPak.CheckState == CheckState.Checked)
                sw.WriteLine("MemPak = 1");
            else if (checkMemPak.CheckState == CheckState.Unchecked)
                sw.WriteLine("MemPak = 0");
            else
                sw.WriteLine(";MemPak = 0");

            if (checkNoCntPak.CheckState == CheckState.Checked)
                sw.WriteLine("NoCntPak = 1");
            else if (checkNoCntPak.CheckState == CheckState.Unchecked)
                sw.WriteLine("NoCntPak = 0");
            else
                sw.WriteLine(";NoCntPak = 0");

            sw.WriteLine(";PDFURL = \"" + textPDFURL.Text + "\"");

            switch (comboPlayerNum.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("PlayerNum = 1");
                    break;
                case 1:
                    sw.WriteLine("PlayerNum = 4");
                    break;
                default:
                    sw.WriteLine(";PlayerNum = ");
                    break;
            }

            if (checkRamSize.Checked)
                sw.WriteLine("RamSize = 0x400000");
            else
                sw.WriteLine(";RamSize = 0x400000");

            if (checkRetraceByVsync.CheckState == CheckState.Checked)
                sw.WriteLine("RetraceByVsync = 1");
            else if (checkRetraceByVsync.CheckState == CheckState.Unchecked)
                sw.WriteLine("RetraceByVsync = 0");
            else
                sw.WriteLine(";RetraceByVsync = 0");

            if (checkRomType.CheckState == CheckState.Checked)
                sw.WriteLine("RomType = 1");
            else if (checkRomType.CheckState == CheckState.Unchecked)
                sw.WriteLine("RomType = 0");
            else
                sw.WriteLine(";RomType = 0");

            if (checkRSPMultiCore.CheckState == CheckState.Checked)
                sw.WriteLine("RSPMultiCore = 1");
            else if (checkRSPMultiCore.CheckState == CheckState.Unchecked)
                sw.WriteLine("RSPMultiCore = 0");
            else
                sw.WriteLine(";RSPMultiCore = 0");

            if (checkRSPAMultiCoreWait.CheckState == CheckState.Checked)
                sw.WriteLine("RSPAMultiCoreWait = 1");
            else if (checkRSPAMultiCoreWait.CheckState == CheckState.Unchecked)
                sw.WriteLine("RSPAMultiCoreWait = 0");
            else
                sw.WriteLine(";RSPAMultiCoreWait = 0");

            if (checkRumble.CheckState == CheckState.Checked)
                sw.WriteLine("Rumble = 1");
            else if (checkRumble.CheckState == CheckState.Unchecked)
                sw.WriteLine("Rumble = 0");
            else
                sw.WriteLine(";Rumble = 0");

            if (checkScreenCaptureNG.CheckState == CheckState.Checked)
                sw.WriteLine("ScreenCaptureNG = 1");
            else if (checkScreenCaptureNG.CheckState == CheckState.Unchecked)
                sw.WriteLine("ScreenCaptureNG = 0");
            else
                sw.WriteLine(";ScreenCaptureNG = 0");

            switch (comboTicksPerFrame.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("TicksPerFrame = 940000"); //50.298 FPS
                    break;
                case 1:
                    sw.WriteLine("TicksPerFrame = 939900"); //50.303 FPS
                    break;
                case 2:
                    sw.WriteLine("TicksPerFrame = 937500"); //50.432 FPS
                    break;
                case 3:
                    sw.WriteLine("TicksPerFrame = 788000"); //60.000 FPS
                    break;
                case 4:
                    sw.WriteLine("TicksPerFrame = 783250"); //60.364 FPS
                    break;
                case 5:
                    sw.WriteLine("TicksPerFrame = 781250"); //60.518 FPS
                    break;
                case 6:
                    sw.WriteLine("TicksPerFrame = 601250"); //78.636 FPS
                    break;
                case 7:
                    sw.WriteLine("TicksPerFrame = 581250"); //81.342 FPS
                    break;
                case 8:
                    sw.WriteLine("TicksPerFrame = 501250"); //94.324 FPS
                    break;
                default:
                    sw.WriteLine(";TicksPerFrame = 788000");
                    break;
            }

            if (checkTimeIntDelay.CheckState == CheckState.Checked)
                sw.WriteLine("TimeIntDelay = 1");
            else if (checkTimeIntDelay.CheckState == CheckState.Unchecked)
                sw.WriteLine("TimeIntDelay = 0");
            else
                sw.WriteLine(";TimeIntDelay = 0");

            if (checkTrueBoot.CheckState == CheckState.Checked)
                sw.WriteLine("TrueBoot = 1");
            else if (checkTrueBoot.CheckState == CheckState.Unchecked)
                sw.WriteLine("TrueBoot = 0");
            else
                sw.WriteLine(";TrueBoot = 0");

            if (checkUseTimer.CheckState == CheckState.Checked)
                sw.WriteLine("UseTimer = 1");
            else if (checkUseTimer.CheckState == CheckState.Unchecked)
                sw.WriteLine("UseTimer = 0");
            else
                sw.WriteLine(";UseTimer = 0");

            sw.WriteLine();
        }

        private void SaveRender(StreamWriter sw)
        {
            sw.WriteLine("[Render]");

            if (checkbForce720P.CheckState == CheckState.Checked)
                sw.WriteLine("bForce720P = 1");
            else if (checkbForce720P.CheckState == CheckState.Unchecked)
                sw.WriteLine("bForce720P = 0");
            else
                sw.WriteLine(";bForce720P = 0");

            if (checkCalculateLOD.CheckState == CheckState.Checked)
                sw.WriteLine("CalculateLOD = 1");
            else if (checkCalculateLOD.CheckState == CheckState.Unchecked)
                sw.WriteLine("CalculateLOD = 0");
            else
                sw.WriteLine(";CalculateLOD = 0");

            if (checkCanvasWidth.Checked)
                sw.WriteLine("CanvasWidth = 600");
            else
                sw.WriteLine(";CanvasWidth = 600");

            if (checkCheckTlutValid.CheckState == CheckState.Checked)
                sw.WriteLine("CheckTlutValid = 1");
            else if (checkCheckTlutValid.CheckState == CheckState.Unchecked)
                sw.WriteLine("CheckTlutValid = 0");
            else
                sw.WriteLine(";CheckTlutValid = 0");

            if (checkClearVertexBuf.CheckState == CheckState.Checked)
                sw.WriteLine("ClearVertexBuf = 1");
            else if (checkClearVertexBuf.CheckState == CheckState.Unchecked)
                sw.WriteLine("ClearVertexBuf = 0");
            else
                sw.WriteLine(";ClearVertexBuf = 0");

            if (numericClipTop.Value == 0.0M)
                sw.WriteLine(";ClipTop = 0");
            else
                sw.WriteLine("ClipTop = " + Convert.ToInt32(numericClipTop.Value).ToString());

            if (numericClipRight.Value == 0.0M)
                sw.WriteLine(";ClipRight = 0");
            else
                sw.WriteLine("ClipRight = " + Convert.ToInt32(numericClipRight.Value).ToString());

            if (numericClipBottom.Value == 0.0M)
                sw.WriteLine(";ClipBottom = 0");
            else
                sw.WriteLine("ClipBottom = " + Convert.ToInt32(numericClipBottom.Value).ToString());

            if (numericClipLeft.Value == 0.0M)
                sw.WriteLine(";ClipLeft = 0");
            else
                sw.WriteLine("ClipLeft = " + Convert.ToInt32(numericClipLeft.Value).ToString());

            for (int i = 0; i < dataGridViewConstValue.Rows.Count - 1; i++)
            {
                sw.WriteLine("ConstValue" + i.ToString() + " = 0x" + ((int)dataGridViewConstValue.Rows[i].Cells[0].Value).ToString("X"));
            }

            if (checkCopyAlphaForceOne.CheckState == CheckState.Checked)
                sw.WriteLine("CopyAlphaForceOne = 1");
            else if (checkCopyAlphaForceOne.CheckState == CheckState.Unchecked)
                sw.WriteLine("CopyAlphaForceOne = 0");
            else
                sw.WriteLine(";CopyAlphaForceOne = 0");

            if (checkCopyColorBuffer.CheckState == CheckState.Checked)
                sw.WriteLine("CopyColorBuffer = 1");
            else if (checkCopyColorBuffer.CheckState == CheckState.Unchecked)
                sw.WriteLine("CopyColorBuffer = 0");
            else
                sw.WriteLine(";CopyColorBuffer = 0");

            if (checkCopyColorAfterTask.CheckState == CheckState.Checked)
                sw.WriteLine("CopyColorAfterTask = 1");
            else if (checkCopyColorAfterTask.CheckState == CheckState.Unchecked)
                sw.WriteLine("CopyColorAfterTask = 0");
            else
                sw.WriteLine(";CopyColorAfterTask = 0");

            if (checkCopyDepthBuffer.CheckState == CheckState.Checked)
                sw.WriteLine("CopyDepthBuffer = 1");
            else if (checkCopyDepthBuffer.CheckState == CheckState.Unchecked)
                sw.WriteLine("CopyDepthBuffer = 0");
            else
                sw.WriteLine(";CopyDepthBuffer = 0");

            if (checkCopyMiddleBuffer.CheckState == CheckState.Checked)
                sw.WriteLine("CopyMiddleBuffer = 1");
            else if (checkCopyMiddleBuffer.CheckState == CheckState.Unchecked)
                sw.WriteLine("CopyMiddleBuffer = 0");
            else
                sw.WriteLine(";CopyMiddleBuffer = 0");

            if (checkDepthCompareLess.CheckState == CheckState.Checked)
                sw.WriteLine("DepthCompareLess = 1");
            else if (checkDepthCompareLess.CheckState == CheckState.Unchecked)
                sw.WriteLine("DepthCompareLess = 0");
            else
                sw.WriteLine(";DepthCompareLess = 0");

            if (checkDoubleFillCheck.CheckState == CheckState.Checked)
                sw.WriteLine("DoubleFillCheck = 1");
            else if (checkDoubleFillCheck.CheckState == CheckState.Unchecked)
                sw.WriteLine("DoubleFillCheck = 0");
            else
                sw.WriteLine(";DoubleFillCheck = 0");

            if (checkFirstFrameAt.Checked)
                sw.WriteLine("FirstFrameAt = 1000");
            else
                sw.WriteLine(";FirstFrameAt = 1000");

            if (checkFlushMemEachTask.CheckState == CheckState.Checked)
                sw.WriteLine("FlushMemEachTask = 1");
            else if (checkFlushMemEachTask.CheckState == CheckState.Unchecked)
                sw.WriteLine("FlushMemEachTask = 0");
            else
                sw.WriteLine(";FlushMemEachTask = 0");

            if (checkFogVertexAlpha.CheckState == CheckState.Checked)
                sw.WriteLine("FogVertexAlpha = 1");
            else if (checkFogVertexAlpha.CheckState == CheckState.Unchecked)
                sw.WriteLine("FogVertexAlpha = 0");
            else
                sw.WriteLine(";FogVertexAlpha = 0");

            if (checkForceFilterPoint.CheckState == CheckState.Checked)
                sw.WriteLine("ForceFilterPoint = 1");
            else if (checkForceFilterPoint.CheckState == CheckState.Unchecked)
                sw.WriteLine("ForceFilterPoint = 0");
            else
                sw.WriteLine(";ForceFilterPoint = 0");

            if (checkForceRectFilterPoint.CheckState == CheckState.Checked)
                sw.WriteLine("ForceRectFilterPoint = 1");
            else if (checkForceRectFilterPoint.CheckState == CheckState.Unchecked)
                sw.WriteLine("ForceRectFilterPoint = 0");
            else
                sw.WriteLine(";ForceRectFilterPoint = 0");

            if (checkInitPerspectiveMode.CheckState == CheckState.Checked)
                sw.WriteLine("InitPerspectiveMode = 1");
            else if (checkInitPerspectiveMode.CheckState == CheckState.Unchecked)
                sw.WriteLine("InitPerspectiveMode = 0");
            else
                sw.WriteLine(";InitPerspectiveMode = 0");

            if (checkNeedPreParse.CheckState == CheckState.Checked)
                sw.WriteLine("NeedPreParse = 1");
            else if (checkNeedPreParse.CheckState == CheckState.Unchecked)
                sw.WriteLine("NeedPreParse = 0");
            else
                sw.WriteLine(";NeedPreParse = 0");

            if (checkNeedTileSizeCheck.CheckState == CheckState.Checked)
                sw.WriteLine("NeedTileSizeCheck = 1");
            else if (checkNeedTileSizeCheck.CheckState == CheckState.Unchecked)
                sw.WriteLine("NeedTileSizeCheck = 0");
            else
                sw.WriteLine(";NeedTileSizeCheck = 0");

            if (checkPolygonOffset.CheckState == CheckState.Checked)
                sw.WriteLine("PolygonOffset = 1");
            else if (checkPolygonOffset.CheckState == CheckState.Unchecked)
                sw.WriteLine("PolygonOffset = 0");
            else
                sw.WriteLine(";PolygonOffset = 0");

            if (checkPreparseTMEMBlock.CheckState == CheckState.Checked)
                sw.WriteLine("PreparseTMEMBlock = 1");
            else if (checkPreparseTMEMBlock.CheckState == CheckState.Unchecked)
                sw.WriteLine("PreparseTMEMBlock = 0");
            else
                sw.WriteLine(";PreparseTMEMBlock = 0");

            if (checkTileSizeCheckSpecial.CheckState == CheckState.Checked)
                sw.WriteLine("TileSizeCheckSpecial = 1");
            else if (checkTileSizeCheckSpecial.CheckState == CheckState.Unchecked)
                sw.WriteLine("TileSizeCheckSpecial = 0");
            else
                sw.WriteLine(";TileSizeCheckSpecial = 0");

            if (checkTLUTCheck.CheckState == CheckState.Checked)
                sw.WriteLine("TLUTCheck = 1");
            else if (checkTLUTCheck.CheckState == CheckState.Unchecked)
                sw.WriteLine("TLUTCheck = 0");
            else
                sw.WriteLine(";TLUTCheck = 0");

            if (checkUseColorDither.CheckState == CheckState.Checked)
                sw.WriteLine("UseColorDither = 1");
            else if (checkUseColorDither.CheckState == CheckState.Unchecked)
                sw.WriteLine("UseColorDither = 0");
            else
                sw.WriteLine(";UseColorDither = 0");

            if (checkuseViewportZScale.CheckState == CheckState.Checked)
                sw.WriteLine("useViewportZScale = 1");
            else if (checkuseViewportZScale.CheckState == CheckState.Unchecked)
                sw.WriteLine("useViewportZScale = 0");
            else
                sw.WriteLine(";useViewportZScale = 0");

            if (checkZClip.CheckState == CheckState.Checked)
                sw.WriteLine("ZClip = 1");
            else if (checkZClip.CheckState == CheckState.Unchecked)
                sw.WriteLine("ZClip = 0");
            else
                sw.WriteLine(";ZClip = 0");

            sw.WriteLine();
        }

        private void SaveOthers(StreamWriter sw)
        {
            sw.WriteLine("[Sound]");

            switch (comboBufFull.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("BufFull = 0x2000");
                    break;
                case 1:
                    sw.WriteLine("BufFull = 0x2800");
                    break;
                case 2:
                    sw.WriteLine("BufFull = 0x3800");
                    break;
                default:
                    sw.WriteLine(";BufFull = 0x");
                    break;
            }

            switch (comboBufHalf.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("BufHalf = 0x1400");
                    break;
                case 1:
                    sw.WriteLine("BufHalf = 0x1800");
                    break;
                case 2:
                    sw.WriteLine("BufHalf = 0x2000");
                    break;
                case 3:
                    sw.WriteLine("BufHalf = 0x2800");
                    break;
                default:
                    sw.WriteLine(";BufHalf = 0x");
                    break;
            }

            switch (comboBufHave.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("BufHave = 0x1000");
                    break;
                case 1:
                    sw.WriteLine("BufHave = 0x1400");
                    break;
                default:
                    sw.WriteLine(";BufHave = 0x");
                    break;
            }

            switch (comboFillAfterVCM.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("FillAfterVCM = 6");
                    break;
                case 1:
                    sw.WriteLine("FillAfterVCM = 8");
                    break;
                default:
                    sw.WriteLine(";FillAfterVCM = ");
                    break;
            }

            switch (comboResample.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("Resample = 31300");
                    break;
                case 1:
                    sw.WriteLine("Resample = 31900");
                    break;
                case 2:
                    sw.WriteLine("Resample = 32500");
                    break;
                default:
                    sw.WriteLine(";Resample = ");
                    break;
            }

            sw.WriteLine();

            sw.WriteLine("[Input]");

            if (checkAlwaysHave.CheckState == CheckState.Checked)
                sw.WriteLine("AlwaysHave = 1");
            else if (checkAlwaysHave.CheckState == CheckState.Unchecked)
                sw.WriteLine("AlwaysHave = 0");
            else
                sw.WriteLine(";AlwaysHave = 0");

            if (checkSTICK_CLAMP.CheckState == CheckState.Checked)
                sw.WriteLine("STICK_CLAMP = 1");
            else if (checkSTICK_CLAMP.CheckState == CheckState.Unchecked)
                sw.WriteLine("STICK_CLAMP = 0");
            else
                sw.WriteLine(";STICK_CLAMP = 0");

            switch (comboStickLimit.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("StickLimit = 80");
                    break;
                case 1:
                    sw.WriteLine("StickLimit = 90");
                    break;
                default:
                    sw.WriteLine(";StickLimit = ");
                    break;
            }

            switch (comboStickModify.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("StickModify = 1");
                    break;
                case 1:
                    sw.WriteLine("StickModify = 2");
                    break;
                case 2:
                    sw.WriteLine("StickModify = 3");
                    break;
                default:
                    sw.WriteLine(";StickModify = ");
                    break;
            }

            if (checkVPAD_STICK_CLAMP.CheckState == CheckState.Checked)
                sw.WriteLine("VPAD_STICK_CLAMP = 1");
            else if (checkVPAD_STICK_CLAMP.CheckState == CheckState.Unchecked)
                sw.WriteLine("VPAD_STICK_CLAMP = 0");
            else
                sw.WriteLine(";VPAD_STICK_CLAMP = 0");

            sw.WriteLine();

            sw.WriteLine("[RSPG]");

            switch (comboGTaskDelay.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("GTaskDelay = 0");
                    break;
                case 1:
                    sw.WriteLine("GTaskDelay = 1024");
                    break;
                case 2:
                    sw.WriteLine("GTaskDelay = 450000");
                    break;
                case 3:
                    sw.WriteLine("GTaskDelay = 761250");
                    break;
                case 4:
                    sw.WriteLine("GTaskDelay = 1481250");
                    break;
                default:
                    sw.WriteLine(";GTaskDelay = ");
                    break;
            }

            switch (comboRDPDelay.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("RDPDelay = 81250");
                    break;
                case 1:
                    sw.WriteLine("RDPDelay = 481250");
                    break;
                default:
                    sw.WriteLine(";RDPDelay = ");
                    break;
            }

            if (checkRDPInt.CheckState == CheckState.Checked)
                sw.WriteLine("RDPInt = 1");
            else if (checkRDPInt.CheckState == CheckState.Unchecked)
                sw.WriteLine("RDPInt = 0");
            else
                sw.WriteLine(";RDPInt = 0");

            if (checkRIntAfterGTask.CheckState == CheckState.Checked)
                sw.WriteLine("RIntAfterGTask = 1");
            else if (checkRIntAfterGTask.CheckState == CheckState.Unchecked)
                sw.WriteLine("RIntAfterGTask = 0");
            else
                sw.WriteLine(";RIntAfterGTask = 0");

            if (checkSkip.CheckState == CheckState.Checked)
                sw.WriteLine("Skip = 1");
            else if (checkSkip.CheckState == CheckState.Unchecked)
                sw.WriteLine("Skip = 0");
            else
                sw.WriteLine(";Skip = 0");

            if (checkWaitDelay.CheckState == CheckState.Checked)
                sw.WriteLine("WaitDelay = 1");
            else if (checkWaitDelay.CheckState == CheckState.Unchecked)
                sw.WriteLine("WaitDelay = 0");
            else
                sw.WriteLine(";WaitDelay = 0");

            if (checkWaitOnlyFirst.CheckState == CheckState.Checked)
                sw.WriteLine("WaitOnlyFirst = 1");
            else if (checkWaitOnlyFirst.CheckState == CheckState.Unchecked)
                sw.WriteLine("WaitOnlyFirst = 0");
            else
                sw.WriteLine(";WaitOnlyFirst = 0");

            sw.WriteLine();

            sw.WriteLine("[Cmp]");

            switch (comboBlockSize.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("BlockSize = 0x1C00");
                    break;
                case 1:
                    sw.WriteLine("BlockSize = 0x3000");
                    break;
                default:
                    sw.WriteLine(";BlockSize = ");
                    break;
            }

            if (checkFrameBlockLimit.Checked)
                sw.WriteLine("FrameBlockLimit = 0x100");
            else
                sw.WriteLine(";FrameBlockLimit = 0x100");

            if (checkOptEnable.CheckState == CheckState.Checked)
                sw.WriteLine("OptEnable = 1");
            else if (checkOptEnable.CheckState == CheckState.Unchecked)
                sw.WriteLine("OptEnable = 0");
            else
                sw.WriteLine(";OptEnable = 0");

            if (checkW32OverlayCheck.CheckState == CheckState.Checked)
                sw.WriteLine("W32OverlayCheck = 1");
            else if (checkW32OverlayCheck.CheckState == CheckState.Unchecked)
                sw.WriteLine("W32OverlayCheck = 0");
            else
                sw.WriteLine(";W32OverlayCheck = 0");

            sw.WriteLine();

            sw.WriteLine("[SI]");

            switch (comboSIDelay.SelectedIndex)
            {
                case 0:
                    sw.WriteLine("SIDelay = 0xD800");
                    break;
                case 1:
                    sw.WriteLine("SIDelay = 0x10800");
                    break;
                default:
                    sw.WriteLine(";SIDelay = ");
                    break;
            }

            sw.WriteLine();

            sw.WriteLine("[VI]");

            if (checkScanReadTime.CheckState == CheckState.Checked)
                sw.WriteLine("ScanReadTime = 1");
            else if (checkScanReadTime.CheckState == CheckState.Unchecked)
                sw.WriteLine("ScanReadTime = 0");
            else
                sw.WriteLine(";ScanReadTime = 0");

            sw.WriteLine();

            sw.WriteLine("[TempConfig]");

            if (checkRSPGDCFlush.CheckState == CheckState.Checked)
                sw.WriteLine("RSPGDCFlush = 1");
            else if (checkRSPGDCFlush.CheckState == CheckState.Unchecked)
                sw.WriteLine("RSPGDCFlush = 0");
            else
                sw.WriteLine(";RSPGDCFlush = 0");

            sw.WriteLine();

            if (dataGridViewFrameTickHack.Rows.Count - 1 > 0)
            {
                sw.WriteLine("[FrameTickHack]");

                for (int i = 0; i < dataGridViewFrameTickHack.Rows.Count - 1; i++)                
                    sw.WriteLine("Hack" + i.ToString() + " = " + ((int)dataGridViewFrameTickHack.Rows[i].Cells[0].Value).ToString());                

                sw.WriteLine();
            }

            if (dataGridViewIdle.Rows.Count - 1 > 0)
            {
                sw.WriteLine("[Idle]");
                sw.WriteLine("Count = " + CountIdle.ToString());
                for (int i = 0; i < dataGridViewIdle.Rows.Count - 1; i++)
                {
                    sw.WriteLine("Address" + i.ToString() + " = 0x" + ((int)dataGridViewIdle.Rows[i].Cells[1].Value).ToString("X"));
                    sw.WriteLine("Inst" + i.ToString() + " = 0x" + ((int)dataGridViewIdle.Rows[i].Cells[2].Value).ToString("X"));
                    sw.WriteLine("Type" + i.ToString() + " = " + ((int)dataGridViewIdle.Rows[i].Cells[3].Value).ToString());
                    sw.WriteLine();
                }
            }

            if (dataGridViewInsertIdleInst.Rows.Count - 1 > 0)
            {
                sw.WriteLine("[InsertIdleInst]");
                sw.WriteLine("Count = " + CountInsertIdleInst.ToString());
                for (int i = 0; i < dataGridViewInsertIdleInst.Rows.Count - 1; i++)
                {
                    sw.WriteLine("Address" + i.ToString() + " = 0x" + ((int)dataGridViewInsertIdleInst.Rows[i].Cells[1].Value).ToString("X"));
                    sw.WriteLine("Inst" + i.ToString() + " = 0x" + ((int)dataGridViewInsertIdleInst.Rows[i].Cells[2].Value).ToString("X"));
                    sw.WriteLine("Type" + i.ToString() + " = 0x" + ((int)dataGridViewInsertIdleInst.Rows[i].Cells[3].Value).ToString("X"));
                    sw.WriteLine("Value" + i.ToString() + " = 0x" + ((int)dataGridViewInsertIdleInst.Rows[i].Cells[4].Value).ToString("X"));
                    sw.WriteLine();
                }
            }

            if (dataGridViewSpecialInst.Rows.Count - 1 > 0)
            {
                sw.WriteLine("[SpecialInst]");
                sw.WriteLine("Count = " + CountSpecialInst.ToString());
                for (int i = 0; i < dataGridViewSpecialInst.Rows.Count - 1; i++)
                {
                    object value = dataGridViewSpecialInst.Rows[i].Cells[4].Value;
                    sw.WriteLine("Address" + i.ToString() + " = 0x" + ((int)dataGridViewSpecialInst.Rows[i].Cells[1].Value).ToString("X"));
                    sw.WriteLine("Inst" + i.ToString() + " = 0x" + ((int)dataGridViewSpecialInst.Rows[i].Cells[2].Value).ToString("X"));
                    sw.WriteLine("Type" + i.ToString() + " = " + ((int)dataGridViewSpecialInst.Rows[i].Cells[3].Value).ToString());
                    if (value != null && value != DBNull.Value)
                        sw.WriteLine("Value" + i.ToString() + " = 0x" + ((int)value).ToString("X"));
                    sw.WriteLine();
                }
            }

            if (dataGridViewBreakBlockInst.Rows.Count - 1 > 0)
            {
                sw.WriteLine("[BreakBlockInst]");
                sw.WriteLine("Count = " + CountBreakBlockInst.ToString());
                for (int i = 0; i < dataGridViewBreakBlockInst.Rows.Count - 1; i++)
                {
                    sw.WriteLine("Address" + i.ToString() + " = 0x" + ((int)dataGridViewBreakBlockInst.Rows[i].Cells[1].Value).ToString("X"));
                    sw.WriteLine("Inst" + i.ToString() + " = 0x" + ((int)dataGridViewBreakBlockInst.Rows[i].Cells[2].Value).ToString("X"));
                    sw.WriteLine("Type" + i.ToString() + " = " + ((int)dataGridViewBreakBlockInst.Rows[i].Cells[3].Value).ToString());
                    sw.WriteLine("JmpPC" + i.ToString() + " = 0x" + ((int)dataGridViewBreakBlockInst.Rows[i].Cells[4].Value).ToString("X"));
                    sw.WriteLine();
                }
            }

            if (dataGridViewRomHack.Rows.Count - 1 > 0)
            {
                sw.WriteLine("[RomHack]");
                sw.WriteLine("Count = " + CountRomHack.ToString());
                for (int i = 0; i < dataGridViewRomHack.Rows.Count - 1; i++)
                {
                    string value = (string)dataGridViewRomHack.Rows[i].Cells[3].Value;
                    sw.WriteLine("Address" + i.ToString() + " = 0x" + ((int)dataGridViewRomHack.Rows[i].Cells[1].Value).ToString("X"));
                    sw.WriteLine("Type" + i.ToString() + " = " + ((int)dataGridViewRomHack.Rows[i].Cells[2].Value).ToString());
                    sw.WriteLine("Value" + i.ToString() + " = " + ByteArrayValue.Formatting(value));
                    sw.WriteLine();
                }
            }

            if (dataGridViewVertexHack.Rows.Count - 1 > 0)
            {
                sw.WriteLine("[VertexHack]");
                sw.WriteLine("Count = " + CountVertexHack.ToString());
                for (int i = 0; i < dataGridViewVertexHack.Rows.Count - 1; i++)
                {
                    string firstVertex = (string)dataGridViewVertexHack.Rows[i].Cells[4].Value;
                    string value = (string)dataGridViewVertexHack.Rows[i].Cells[5].Value;
                    sw.WriteLine("VertexCount" + i.ToString() + " = " + ((int)dataGridViewVertexHack.Rows[i].Cells[1].Value).ToString());
                    sw.WriteLine("VertexAddress" + i.ToString() + " = 0x" + ((int)dataGridViewVertexHack.Rows[i].Cells[2].Value).ToString("X"));
                    sw.WriteLine("TextureAddress" + i.ToString() + " = 0x" + ((int)dataGridViewVertexHack.Rows[i].Cells[3].Value).ToString("X"));
                    sw.WriteLine("FirstVertex" + i.ToString() + " = " + ByteArrayValue.Formatting(firstVertex));
                    sw.WriteLine("Value" + i.ToString() + " = " + ByteArrayValue.Formatting(value));
                    sw.WriteLine();
                }
            }

            if (dataGridViewFilterHack.Rows.Count - 1 > 0)
            {
                sw.WriteLine("[FilterHack]");
                sw.WriteLine("Count = " + CountFilterHack.ToString());
                for (int i = 0; i < dataGridViewFilterHack.Rows.Count - 1; i++)
                {
                    object alphaTest = dataGridViewFilterHack.Rows[i].Cells[5].Value;
                    object magFilter = dataGridViewFilterHack.Rows[i].Cells[6].Value;
                    object offsetS = dataGridViewFilterHack.Rows[i].Cells[7].Value;
                    object offsetT = dataGridViewFilterHack.Rows[i].Cells[8].Value;
                    sw.WriteLine("TextureAddress" + i.ToString() + " = 0x" + ((int)dataGridViewFilterHack.Rows[i].Cells[1].Value).ToString("X"));
                    sw.WriteLine("SumPixel" + i.ToString() + " = 0x" + ((int)dataGridViewFilterHack.Rows[i].Cells[2].Value).ToString("X"));
                    sw.WriteLine("Data2_" + i.ToString() + " = 0x" + ((int)dataGridViewFilterHack.Rows[i].Cells[3].Value).ToString("X"));
                    sw.WriteLine("Data3_" + i.ToString() + " = 0x" + ((int)dataGridViewFilterHack.Rows[i].Cells[4].Value).ToString("X"));
                    if (alphaTest != null && alphaTest != DBNull.Value)
                        sw.WriteLine("AlphaTest" + i.ToString() + " = " + ((int)alphaTest).ToString());
                    if (magFilter != null && magFilter != DBNull.Value)
                        sw.WriteLine("MagFilter" + i.ToString() + " = " + ((int)magFilter).ToString());
                    if (offsetS != null && offsetS != DBNull.Value)
                        sw.WriteLine("OffsetS" + i.ToString() + " = 0x" + ((int)offsetS).ToString("X"));
                    if (offsetT != null && offsetT != DBNull.Value)
                        sw.WriteLine("OffsetT" + i.ToString() + " = 0x" + ((int)offsetT).ToString("X"));
                    sw.WriteLine();
                }
            }

            if (dataGridViewCheat.Rows.Count - 1 > 0)
            {
                sw.WriteLine("[Cheat]");

                for (int i = 0; i < dataGridViewCheat.Rows.Count - 1; i++)
                {
                    sw.WriteLine("Cheat" + i.ToString() + " = " + ((int)dataGridViewCheat.Rows[i].Cells[1].Value).ToString());
                    sw.WriteLine("Cheat" + i.ToString() + "_Addr = 0x" + ((int)dataGridViewCheat.Rows[i].Cells[2].Value).ToString("X"));
                    sw.WriteLine("Cheat" + i.ToString() + "_Value = 0x" + ((int)dataGridViewCheat.Rows[i].Cells[3].Value).ToString("X"));
                    sw.WriteLine("Cheat" + i.ToString() + "_Bytes = " + ((int)dataGridViewCheat.Rows[i].Cells[4].Value).ToString());
                    sw.WriteLine();
                }
            }
        }

        private void LoadRomOption()
        {
            checkAIIntPerFrame.CheckState = Config.RomOption.AIIntPerFrame;
            checkAIUseTimer.CheckState = Config.RomOption.AIUseTimer;

            switch (Config.RomOption.BackupSize)
            {
                case 512: //4 Kibits
                    comboBackupSize.SelectedIndex = 0;
                    break;
                case 2048: //16 Kibits
                    comboBackupSize.SelectedIndex = 1;
                    break;
                case 32768: //256 Kibits
                    comboBackupSize.SelectedIndex = 2;
                    break;
                case 131072: //1024 Kibits
                    comboBackupSize.SelectedIndex = 3;
                    break;
                default:
                    comboBackupSize.SelectedIndex = comboBackupSize.Items.Count - 1;
                    break;
            }

            switch (Config.RomOption.BackupType)
            {
                case 0: //Auto
                    comboBackupType.SelectedIndex = 0;
                    break;
                case 1: //SRAM
                    comboBackupType.SelectedIndex = 1;
                    break;
                case 2: //Flash
                    comboBackupType.SelectedIndex = 2;
                    break;
                case 3: //EEPROM
                    comboBackupType.SelectedIndex = 3;
                    break;
                default:
                    comboBackupType.SelectedIndex = comboBackupType.Items.Count - 1;
                    break;
            }

            checkBootPCChange.CheckState = Config.RomOption.BootPCChange;
            checkCmpBlockAdvFlag.CheckState = Config.RomOption.CmpBlockAdvFlag;

            if (Config.RomOption.EEROMInitValue == 0xFF)
                checkEEROMInitValue.Checked = true;
            else
                checkEEROMInitValue.Checked = false;

            checkMemPak.CheckState = Config.RomOption.MemPak;
            checkNoCntPak.CheckState = Config.RomOption.NoCntPak;
            textPDFURL.Text = Config.RomOption.PDFURL;

            switch (Config.RomOption.PlayerNum)
            {
                case 0:
                    comboPlayerNum.SelectedIndex = 0;
                    break;
                case 1:
                    comboPlayerNum.SelectedIndex = 1;
                    break;
                default:
                    comboPlayerNum.SelectedIndex = comboPlayerNum.Items.Count - 1;
                    break;
            }

            if (Config.RomOption.RamSize == 0x400000)
                checkRamSize.Checked = true;
            else
                checkRamSize.Checked = false;

            checkRetraceByVsync.CheckState = Config.RomOption.RetraceByVsync;
            checkRomType.CheckState = Config.RomOption.RomType;
            checkRSPMultiCore.CheckState = Config.RomOption.RSPMultiCore;
            checkRSPAMultiCoreWait.CheckState = Config.RomOption.RSPAMultiCoreWait;
            checkRumble.CheckState = Config.RomOption.Rumble;
            checkScreenCaptureNG.CheckState = Config.RomOption.ScreenCaptureNG;

            switch (Config.RomOption.TicksPerFrame)
            {
                case 940000: //50.298 FPS
                    comboTicksPerFrame.SelectedIndex = 0;
                    break;
                case 939900: //50.303 FPS
                    comboTicksPerFrame.SelectedIndex = 1;
                    break;
                case 937500: //50.432 FPS
                    comboTicksPerFrame.SelectedIndex = 2;
                    break;
                case 788000: //60.000 FPS
                    comboTicksPerFrame.SelectedIndex = 3;
                    break;
                case 783250: //60.364 FPS
                    comboTicksPerFrame.SelectedIndex = 4;
                    break;
                case 781250: //60.518 FPS
                    comboTicksPerFrame.SelectedIndex = 5;
                    break;
                case 601250: //78.636 FPS
                    comboTicksPerFrame.SelectedIndex = 6;
                    break;
                case 581250: //81.342 FPS
                    comboTicksPerFrame.SelectedIndex = 7;
                    break;
                case 501250: //94.324 FPS
                    comboTicksPerFrame.SelectedIndex = 8;
                    break;
                default:
                    comboTicksPerFrame.SelectedIndex = comboTicksPerFrame.Items.Count - 1;
                    break;
            }

            checkTimeIntDelay.CheckState = Config.RomOption.TimeIntDelay;
            checkTrueBoot.CheckState = Config.RomOption.TrueBoot;
            checkUseTimer.CheckState = Config.RomOption.UseTimer;
        }

        private void LoadRender()
        {
            checkbForce720P.CheckState = Config.Render.bForce720P;
            checkCalculateLOD.CheckState = Config.Render.CalculateLOD;

            if (Config.Render.CanvasWidth == 600)
                checkCanvasWidth.Checked = true;
            else
                checkCanvasWidth.Checked = false;

            checkCheckTlutValid.CheckState = Config.Render.CheckTlutValid;
            checkClearVertexBuf.CheckState = Config.Render.ClearVertexBuf;

            if (Config.Render.ClipBottom >= 0 && Config.Render.ClipBottom <= 12)
                numericClipBottom.Value = Config.Render.ClipBottom;
            else
                numericClipBottom.Value = 0;

            if (Config.Render.ClipLeft >= 0 && Config.Render.ClipLeft <= 12)
                numericClipLeft.Value = Config.Render.ClipLeft;
            else
                numericClipLeft.Value = 0;

            if (Config.Render.ClipRight >= 0 && Config.Render.ClipRight <= 12)
                numericClipRight.Value = Config.Render.ClipRight;
            else
                numericClipRight.Value = 0;

            if (Config.Render.ClipTop >= 0 && Config.Render.ClipTop <= 12)
                numericClipTop.Value = Config.Render.ClipTop;
            else
                numericClipTop.Value = 0;

            dataGridViewConstValue.Rows.Clear();
            foreach (object constValue in Config.Render.ConstValue)
                dataGridViewConstValue.Rows.Add(constValue);

            checkCopyAlphaForceOne.CheckState = Config.Render.CopyAlphaForceOne;
            checkCopyColorBuffer.CheckState = Config.Render.CopyColorBuffer;
            checkCopyColorAfterTask.CheckState = Config.Render.CopyColorAfterTask;
            checkCopyDepthBuffer.CheckState = Config.Render.CopyDepthBuffer;
            checkCopyMiddleBuffer.CheckState = Config.Render.CopyMiddleBuffer;
            checkDepthCompareLess.CheckState = Config.Render.DepthCompareLess;
            checkDoubleFillCheck.CheckState = Config.Render.DoubleFillCheck;

            if (Config.Render.FirstFrameAt == 1000)
                checkFirstFrameAt.Checked = true;
            else
                checkFirstFrameAt.Checked = false;

            checkFlushMemEachTask.CheckState = Config.Render.FlushMemEachTask;
            checkFogVertexAlpha.CheckState = Config.Render.FogVertexAlpha;
            checkForceFilterPoint.CheckState = Config.Render.ForceFilterPoint;
            checkInitPerspectiveMode.CheckState = Config.Render.InitPerspectiveMode;
            checkNeedPreParse.CheckState = Config.Render.NeedPreParse;
            checkNeedTileSizeCheck.CheckState = Config.Render.NeedTileSizeCheck;
            checkPolygonOffset.CheckState = Config.Render.PolygonOffset;
            checkPreparseTMEMBlock.CheckState = Config.Render.PreparseTMEMBlock;
            checkTileSizeCheckSpecial.CheckState = Config.Render.TileSizeCheckSpecial;
            checkTLUTCheck.CheckState = Config.Render.TLUTCheck;
            checkUseColorDither.CheckState = Config.Render.UseColorDither;
            checkuseViewportZScale.CheckState = Config.Render.useViewportZScale;
            checkZClip.CheckState = Config.Render.ZClip;
        }

        private void LoadOthers()
        {
            switch (Config.Sound.BufFull)
            {
                case 0x2000:
                    comboBufFull.SelectedIndex = 0;
                    break;
                case 0x2800:
                    comboBufFull.SelectedIndex = 1;
                    break;
                case 0x3800:
                    comboBufFull.SelectedIndex = 2;
                    break;
                default:
                    comboBufFull.SelectedIndex = comboBufFull.Items.Count - 1;
                    break;
            }

            switch (Config.Sound.BufHalf)
            {
                case 0x1400:
                    comboBufHalf.SelectedIndex = 0;
                    break;
                case 0x1800:
                    comboBufHalf.SelectedIndex = 1;
                    break;
                case 0x2000:
                    comboBufHalf.SelectedIndex = 2;
                    break;
                case 0x2800:
                    comboBufHalf.SelectedIndex = 3;
                    break;
                default:
                    comboBufHalf.SelectedIndex = comboBufHalf.Items.Count - 1;
                    break;
            }

            switch (Config.Sound.BufHave)
            {
                case 0x1000:
                    comboBufHave.SelectedIndex = 0;
                    break;
                case 0x1400:
                    comboBufHave.SelectedIndex = 1;
                    break;
                default:
                    comboBufHave.SelectedIndex = comboBufHave.Items.Count - 1;
                    break;
            }

            switch (Config.Sound.FillAfterVCM)
            {
                case 6:
                    comboFillAfterVCM.SelectedIndex = 0;
                    break;
                case 8:
                    comboFillAfterVCM.SelectedIndex = 1;
                    break;
                default:
                    comboFillAfterVCM.SelectedIndex = comboFillAfterVCM.Items.Count - 1;
                    break;
            }

            switch (Config.Sound.Resample)
            {
                case 31300:
                    comboResample.SelectedIndex = 0;
                    break;
                case 31900:
                    comboResample.SelectedIndex = 1;
                    break;
                case 32500:
                    comboResample.SelectedIndex = 2;
                    break;
                default:
                    comboResample.SelectedIndex = comboResample.Items.Count - 1;
                    break;
            }

            checkAlwaysHave.CheckState = Config.Input.AlwaysHave;
            checkSTICK_CLAMP.CheckState = Config.Input.STICK_CLAMP;

            switch (Config.Input.StickLimit)
            {
                case 80:
                    comboStickLimit.SelectedIndex = 0;
                    break;
                case 90:
                    comboStickLimit.SelectedIndex = 1;
                    break;
                default:
                    comboStickLimit.SelectedIndex = comboStickLimit.Items.Count - 1;
                    break;
            }

            switch (Config.Input.StickModify)
            {
                case 1:
                    comboStickModify.SelectedIndex = 0;
                    break;
                case 2:
                    comboStickModify.SelectedIndex = 1;
                    break;
                case 3:
                    comboStickModify.SelectedIndex = 2;
                    break;
                default:
                    comboStickModify.SelectedIndex = comboStickModify.Items.Count - 1;
                    break;
            }

            checkVPAD_STICK_CLAMP.CheckState = Config.Input.VPAD_STICK_CLAMP;

            switch (Config.RSPG.GTaskDelay)
            {
                case 0:
                    comboGTaskDelay.SelectedIndex = 0;
                    break;
                case 1024:
                    comboGTaskDelay.SelectedIndex = 1;
                    break;
                case 450000:
                    comboGTaskDelay.SelectedIndex = 2;
                    break;
                case 761250:
                    comboGTaskDelay.SelectedIndex = 3;
                    break;
                case 1481250:
                    comboGTaskDelay.SelectedIndex = 4;
                    break;
                default:
                    comboGTaskDelay.SelectedIndex = comboGTaskDelay.Items.Count - 1;
                    break;
            }

            switch (Config.RSPG.RDPDelay)
            {
                case 81250:
                    comboRDPDelay.SelectedIndex = 0;
                    break;
                case 481250:
                    comboRDPDelay.SelectedIndex = 1;
                    break;
                default:
                    comboRDPDelay.SelectedIndex = comboRDPDelay.Items.Count - 1;
                    break;
            }

            checkRDPInt.CheckState = Config.RSPG.RDPInt;
            checkRIntAfterGTask.CheckState = Config.RSPG.RIntAfterGTask;
            checkSkip.CheckState = Config.RSPG.Skip;
            checkWaitDelay.CheckState = Config.RSPG.WaitDelay;
            checkWaitOnlyFirst.CheckState = Config.RSPG.WaitOnlyFirst;

            switch (Config.Cmp.BlockSize)
            {
                case 0x1C00:
                    comboBlockSize.SelectedIndex = 0;
                    break;
                case 0x3000:
                    comboBlockSize.SelectedIndex = 1;
                    break;
                default:
                    comboBlockSize.SelectedIndex = comboBlockSize.Items.Count - 1;
                    break;
            }

            if (Config.Cmp.FrameBlockLimit == 0x100)
                checkFrameBlockLimit.Checked = true;
            else
                checkFrameBlockLimit.Checked = false;

            checkOptEnable.CheckState = Config.Cmp.OptEnable;
            checkW32OverlayCheck.CheckState = Config.Cmp.W32OverlayCheck;

            switch (Config.Others.SIDelay)
            {
                case 0xD800:
                    comboSIDelay.SelectedIndex = 0;
                    break;
                case 0x10800:
                    comboSIDelay.SelectedIndex = 1;
                    break;
                default:
                    comboSIDelay.SelectedIndex = comboSIDelay.Items.Count - 1;
                    break;
            }

            checkScanReadTime.CheckState = Config.Others.ScanReadTime;
            checkRSPGDCFlush.CheckState = Config.Others.RSPGDCFlush;

            dataGridViewFrameTickHack.Rows.Clear();
            foreach (object hack in Config.Others.FrameTickHack)
                dataGridViewFrameTickHack.Rows.Add(hack);

            dataGridViewIdle.Rows.Clear();
            foreach (DataRow row in Config.Idle.Table.Rows)
                dataGridViewIdle.Rows.Add(row.ItemArray);
            CountIdle = Config.InsertIdleInst.Count;
            labelCountIdle.Text = "Count = " + CountIdle.ToString();
            if (CountIdle == Config.Idle.Table.Rows.Count)
                labelCountIdle.ForeColor = Color.White;
            else
                labelCountIdle.ForeColor = Color.Red;

            dataGridViewInsertIdleInst.Rows.Clear();
            foreach (DataRow row in Config.InsertIdleInst.Table.Rows)
                dataGridViewInsertIdleInst.Rows.Add(row.ItemArray);
            CountInsertIdleInst = Config.InsertIdleInst.Count;
            labelCountInsertIdleInst.Text = "Count = " + CountInsertIdleInst.ToString();
            if (CountInsertIdleInst == Config.InsertIdleInst.Table.Rows.Count)
                labelCountInsertIdleInst.ForeColor = Color.White;
            else
                labelCountInsertIdleInst.ForeColor = Color.Red;

            dataGridViewSpecialInst.Rows.Clear();
            foreach (DataRow row in Config.SpecialInst.Table.Rows)
                dataGridViewSpecialInst.Rows.Add(row.ItemArray);
            CountSpecialInst = Config.SpecialInst.Count;
            labelCountSpecialInst.Text = "Count = " + CountSpecialInst.ToString();
            if (CountSpecialInst == Config.SpecialInst.Table.Rows.Count)
                labelCountSpecialInst.ForeColor = Color.White;
            else
                labelCountSpecialInst.ForeColor = Color.Red;

            dataGridViewBreakBlockInst.Rows.Clear();
            foreach (DataRow row in Config.BreakBlockInst.Table.Rows)
                dataGridViewBreakBlockInst.Rows.Add(row.ItemArray);
            CountBreakBlockInst = Config.BreakBlockInst.Count;
            labelCountBreakBlockInst.Text = "Count = " + CountBreakBlockInst.ToString();
            if (CountBreakBlockInst == Config.BreakBlockInst.Table.Rows.Count)
                labelCountBreakBlockInst.ForeColor = Color.White;
            else
                labelCountBreakBlockInst.ForeColor = Color.Red;

            dataGridViewRomHack.Rows.Clear();
            foreach (DataRow row in Config.RomHack.Table.Rows)
                dataGridViewRomHack.Rows.Add(row.ItemArray);
            CountRomHack = Config.RomHack.Count;
            labelCountRomHack.Text = "Count = " + CountRomHack.ToString();
            if (CountRomHack == Config.RomHack.Table.Rows.Count)
                labelCountRomHack.ForeColor = Color.White;
            else
                labelCountRomHack.ForeColor = Color.Red;

            dataGridViewVertexHack.Rows.Clear();
            foreach (DataRow row in Config.VertexHack.Table.Rows)
                dataGridViewVertexHack.Rows.Add(row.ItemArray);
            CountVertexHack = Config.VertexHack.Count;
            labelCountVertexHack.Text = "Count = " + CountVertexHack.ToString();
            if (CountVertexHack == Config.VertexHack.Table.Rows.Count)
                labelCountVertexHack.ForeColor = Color.White;
            else
                labelCountVertexHack.ForeColor = Color.Red;

            dataGridViewFilterHack.Rows.Clear();
            foreach (DataRow row in Config.FilterHack.Table.Rows)
                dataGridViewFilterHack.Rows.Add(row.ItemArray);
            CountFilterHack = Config.FilterHack.Count;
            labelCountFilterHack.Text = "Count = " + CountFilterHack.ToString();
            if (CountFilterHack == Config.FilterHack.Table.Rows.Count)
                labelCountFilterHack.ForeColor = Color.White;
            else
                labelCountFilterHack.ForeColor = Color.Red;

            dataGridViewCheat.Rows.Clear();
            foreach (DataRow row in Config.Cheat.Table.Rows)
                dataGridViewCheat.Rows.Add(row.ItemArray);
        }

        #endregion

        #region Data Grid View Events

        private void DataGridViewConstValue_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                int value = Convert.ToInt32((string)e.Value, 16);
                e.Value = value;
                e.ParsingApplied = true;
            }
            catch
            {
                MessageBox.Show("Invalid value: " + e.Value.ToString() +
                    "\nValid hexadecimal values range from 0 to FFFFFFFF.",
                    "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridViewFrameTickHack_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                int value = Convert.ToInt32((string)e.Value);
                if (value >= 0)
                {
                    e.Value = value;
                    e.ParsingApplied = true;
                }
                else
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Invalid value: " + e.Value.ToString() +
                    "\nValid decimal values range from 0 to 2147483647.",
                    "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridViewIdle_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 ||
                    e.ColumnIndex == 2)
                {
                    int value = Convert.ToInt32((string)e.Value, 16);
                    e.Value = value;
                    e.ParsingApplied = true;
                }
                else if (e.ColumnIndex == 3)
                {
                    int value = Convert.ToInt32((string)e.Value);
                    if (value >= 0)
                    {
                        e.Value = value;
                        e.ParsingApplied = true;
                    }
                    else
                        throw new Exception();
                }
            }
            catch
            {
                if (e.ColumnIndex == 1 ||
                    e.ColumnIndex == 2)
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid hexadecimal values range from 0 to FFFFFFFF.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid decimal values range from 0 to 2147483647.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridViewInsertIdleInst_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 ||
                    e.ColumnIndex == 2 ||
                    e.ColumnIndex == 3 ||
                    e.ColumnIndex == 4)
                {
                    int value = Convert.ToInt32((string)e.Value, 16);
                    e.Value = value;
                    e.ParsingApplied = true;
                }
            }
            catch
            {
                MessageBox.Show("Invalid value: " + e.Value.ToString() +
                    "\nValid hexadecimal values range from 0 to FFFFFFFF.",
                    "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridViewSpecialInst_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 ||
                    e.ColumnIndex == 2 ||
                    e.ColumnIndex == 4)
                {
                    int value = Convert.ToInt32((string)e.Value, 16);
                    e.Value = value;
                    e.ParsingApplied = true;
                }
                else if (e.ColumnIndex == 3)
                {
                    int value = Convert.ToInt32((string)e.Value);
                    if (value >= 0)
                    {
                        e.Value = value;
                        e.ParsingApplied = true;
                    }
                    else
                        throw new Exception();
                }
            }
            catch
            {
                if (e.ColumnIndex == 1 ||
                    e.ColumnIndex == 2 ||
                    e.ColumnIndex == 4)
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid hexadecimal values range from 0 to FFFFFFFF.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid decimal values range from 0 to 2147483647.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridViewBreakBlockInst_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 ||
                    e.ColumnIndex == 2 ||
                    e.ColumnIndex == 4)
                {
                    int value = Convert.ToInt32((string)e.Value, 16);
                    e.Value = value;
                    e.ParsingApplied = true;
                }
                else if (e.ColumnIndex == 3)
                {
                    int value = Convert.ToInt32((string)e.Value);
                    if (value >= 0)
                    {
                        e.Value = value;
                        e.ParsingApplied = true;
                    }
                    else
                        throw new Exception();
                }
            }
            catch
            {
                if (e.ColumnIndex == 1 ||
                    e.ColumnIndex == 2 ||
                    e.ColumnIndex == 4)
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid hexadecimal values range from 0 to FFFFFFFF.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid decimal values range from 0 to 2147483647.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridViewRomHack_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    int value = Convert.ToInt32((string)e.Value, 16);
                    e.Value = value;
                    e.ParsingApplied = true;
                }
                else if (e.ColumnIndex == 2)
                {
                    int value = Convert.ToInt32((string)e.Value);
                    if (value >= 0)
                    {
                        e.Value = value;
                        e.ParsingApplied = true;
                    }
                    else
                        throw new Exception();
                }
            }
            catch
            {
                if (e.ColumnIndex == 1)
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid hexadecimal values range from 0 to FFFFFFFF.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid decimal values range from 0 to 2147483647.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
                
        private void DataGridViewVertexHack_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2 ||
                    e.ColumnIndex == 3)
                {
                    int value = Convert.ToInt32((string)e.Value, 16);
                    e.Value = value;
                    e.ParsingApplied = true;
                }
                else if (e.ColumnIndex == 1)
                {
                    int value = Convert.ToInt32((string)e.Value);
                    if (value >= 0)
                    {
                        e.Value = value;
                        e.ParsingApplied = true;
                    }
                    else
                        throw new Exception();
                }
            }
            catch
            {
                if (e.ColumnIndex == 2 ||
                    e.ColumnIndex == 3)
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid hexadecimal values range from 0 to FFFFFFFF.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid decimal values range from 0 to 2147483647.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
                
        private void DataGridViewFilterHack_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 ||
                    e.ColumnIndex == 2 ||
                    e.ColumnIndex == 3 ||
                    e.ColumnIndex == 4 ||
                    e.ColumnIndex == 7 ||
                    e.ColumnIndex == 8)
                {
                    int value = Convert.ToInt32((string)e.Value, 16);
                    e.Value = value;
                    e.ParsingApplied = true;
                }
                else if (e.ColumnIndex == 5 ||
                    e.ColumnIndex == 6)
                {
                    int value = Convert.ToInt32((string)e.Value);
                    if (value >= 0)
                    {
                        e.Value = value;
                        e.ParsingApplied = true;
                    }
                    else
                        throw new Exception();
                }
            }
            catch
            {
                if (e.ColumnIndex == 1 ||
                    e.ColumnIndex == 2 ||
                    e.ColumnIndex == 3 ||
                    e.ColumnIndex == 4 ||
                    e.ColumnIndex == 7 ||
                    e.ColumnIndex == 8)
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid hexadecimal values range from 0 to FFFFFFFF.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid decimal values range from 0 to 2147483647.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridViewCheat_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2 ||
                    e.ColumnIndex == 3)
                {
                    int value = Convert.ToInt32((string)e.Value, 16);
                    e.Value = value;
                    e.ParsingApplied = true;
                }
                else if (e.ColumnIndex == 1 ||
                    e.ColumnIndex == 4)
                {
                    int value = Convert.ToInt32((string)e.Value);
                    if (value >= 0)
                    {
                        e.Value = value;
                        e.ParsingApplied = true;
                    }
                    else
                        throw new Exception();
                }
            }
            catch
            {
                if (e.ColumnIndex == 2 ||
                    e.ColumnIndex == 3)
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid hexadecimal values range from 0 to FFFFFFFF.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Invalid value: " + e.Value.ToString() +
                        "\nValid decimal values range from 0 to 2147483647.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void DataGridViewRomHack_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < dataGridViewRomHack.Rows.Count - 1)
            {
                if (e.ColumnIndex == 3)
                {
                    if (!ByteArrayValue.IsValid((string)e.FormattedValue))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Invalid value: " + (string)e.FormattedValue +
                            "\nValid values ​​are strings of hexadecimal characters (0 to 9 and A to F) pairs.",
                            "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void DataGridViewVertexHack_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < dataGridViewVertexHack.Rows.Count - 1)
            {
                if (e.ColumnIndex == 4 ||
                    e.ColumnIndex == 5)
                {
                    if (!ByteArrayValue.IsValid((string)e.FormattedValue))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Invalid value: " + (string)e.FormattedValue +
                            "\nValid values ​​are strings of hexadecimal characters (0 to 9 and A to F) pairs.",
                            "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }


        private void DataGridViewIdle_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < dataGridViewIdle.Rows.Count - 1)
            {
                if (dataGridViewIdle.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Address column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewIdle.Rows[e.RowIndex].Cells[2].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Inst column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewIdle.Rows[e.RowIndex].Cells[3].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Type column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DataGridViewInsertIdleInst_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < dataGridViewInsertIdleInst.Rows.Count - 1)
            {
                if (dataGridViewInsertIdleInst.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Address column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewInsertIdleInst.Rows[e.RowIndex].Cells[2].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Inst column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewInsertIdleInst.Rows[e.RowIndex].Cells[3].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Type column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewInsertIdleInst.Rows[e.RowIndex].Cells[4].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Value column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DataGridViewSpecialInst_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < dataGridViewSpecialInst.Rows.Count - 1)
            {
                if (dataGridViewSpecialInst.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Address column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewSpecialInst.Rows[e.RowIndex].Cells[2].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Inst column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewSpecialInst.Rows[e.RowIndex].Cells[3].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Type column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                /*else if (dataGridViewSpecialInst.Rows[e.RowIndex].Cells[4].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Value column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }*/
            }
        }

        private void DataGridViewBreakBlockInst_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < dataGridViewBreakBlockInst.Rows.Count - 1)
            {
                if (dataGridViewBreakBlockInst.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Address column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewBreakBlockInst.Rows[e.RowIndex].Cells[2].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Inst column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewBreakBlockInst.Rows[e.RowIndex].Cells[3].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Type column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewBreakBlockInst.Rows[e.RowIndex].Cells[4].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the JmpPC column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DataGridViewRomHack_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < dataGridViewRomHack.Rows.Count - 1)
            {
                if (dataGridViewRomHack.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Address column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewRomHack.Rows[e.RowIndex].Cells[2].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Type column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewRomHack.Rows[e.RowIndex].Cells[3].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Value column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DataGridViewVertexHack_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < dataGridViewVertexHack.Rows.Count - 1)
            {
                if (dataGridViewVertexHack.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the VertexCount column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewVertexHack.Rows[e.RowIndex].Cells[2].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the VertexAddress column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewVertexHack.Rows[e.RowIndex].Cells[3].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the TextureAddress column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewVertexHack.Rows[e.RowIndex].Cells[4].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the FirstVertex column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewVertexHack.Rows[e.RowIndex].Cells[5].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Value column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DataGridViewFilterHack_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < dataGridViewFilterHack.Rows.Count - 1)
            {
                if (dataGridViewFilterHack.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the TextureAddress column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewFilterHack.Rows[e.RowIndex].Cells[2].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the SumPixel column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewFilterHack.Rows[e.RowIndex].Cells[3].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Data2 column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewFilterHack.Rows[e.RowIndex].Cells[4].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Data3 column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DataGridViewCheat_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < dataGridViewCheat.Rows.Count - 1)
            {
                if (dataGridViewCheat.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the N column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewCheat.Rows[e.RowIndex].Cells[2].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Addr column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewCheat.Rows[e.RowIndex].Cells[3].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Value column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dataGridViewCheat.Rows[e.RowIndex].Cells[3].Value == null)
                {
                    e.Cancel = true;
                    MessageBox.Show("The cell in the Bytes column cannot be null.",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void DataGridViewIdle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CountIdle = dataGridViewIdle.Rows.Count - 1;
            labelCountIdle.Text = "Count = " + CountIdle.ToString();
            labelCountIdle.ForeColor = Color.White;
        }

        private void DataGridViewInsertIdleInst_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CountInsertIdleInst = dataGridViewInsertIdleInst.Rows.Count - 1;
            labelCountInsertIdleInst.Text = "Count = " + CountInsertIdleInst.ToString();
            labelCountInsertIdleInst.ForeColor = Color.White;
        }

        private void DataGridViewSpecialInst_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CountSpecialInst = dataGridViewSpecialInst.Rows.Count - 1;
            labelCountSpecialInst.Text = "Count = " + CountSpecialInst.ToString();
            labelCountSpecialInst.ForeColor = Color.White;
        }

        private void DataGridViewBreakBlockInst_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CountBreakBlockInst = dataGridViewBreakBlockInst.Rows.Count - 1;
            labelCountBreakBlockInst.Text = "Count = " + CountBreakBlockInst.ToString();
            labelCountBreakBlockInst.ForeColor = Color.White;
        }

        private void DataGridViewRomHack_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CountRomHack = dataGridViewRomHack.Rows.Count - 1;
            labelCountRomHack.Text = "Count = " + CountRomHack.ToString();
            labelCountRomHack.ForeColor = Color.White;
        }

        private void DataGridViewVertexHack_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CountVertexHack = dataGridViewVertexHack.Rows.Count - 1;
            labelCountVertexHack.Text = "Count = " + CountVertexHack.ToString();
            labelCountVertexHack.ForeColor = Color.White;
        }

        private void DataGridViewFilterHack_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CountFilterHack = dataGridViewFilterHack.Rows.Count - 1;
            labelCountFilterHack.Text = "Count = " + CountFilterHack.ToString();
            labelCountFilterHack.ForeColor = Color.White;
        }


        private void DataGridViewIdle_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView_RowLeave(dataGridViewIdle, e.RowIndex))
            {
                CountIdle = dataGridViewIdle.Rows.Count - 1;
                labelCountIdle.Text = "Count = " + CountIdle.ToString();
                labelCountIdle.ForeColor = Color.White;
            }
        }
                
        private void DataGridViewInsertIdleInst_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView_RowLeave(dataGridViewInsertIdleInst, e.RowIndex))
            {
                CountInsertIdleInst = dataGridViewInsertIdleInst.Rows.Count - 1;
                labelCountInsertIdleInst.Text = "Count = " + CountInsertIdleInst.ToString();
                labelCountInsertIdleInst.ForeColor = Color.White;
            }
        }

        private void DataGridViewSpecialInst_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView_RowLeave(dataGridViewSpecialInst, e.RowIndex))
            {
                CountSpecialInst = dataGridViewSpecialInst.Rows.Count - 1;
                labelCountSpecialInst.Text = "Count = " + CountSpecialInst.ToString();
                labelCountSpecialInst.ForeColor = Color.White;
            }
        }

        private void DataGridViewBreakBlockInst_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView_RowLeave(dataGridViewBreakBlockInst, e.RowIndex))
            {
                CountBreakBlockInst = dataGridViewBreakBlockInst.Rows.Count - 1;
                labelCountBreakBlockInst.Text = "Count = " + CountBreakBlockInst.ToString();
                labelCountBreakBlockInst.ForeColor = Color.White;
            }
        }

        private void DataGridViewRomHack_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView_RowLeave(dataGridViewRomHack, e.RowIndex))
            {
                CountRomHack = dataGridViewRomHack.Rows.Count - 1;
                labelCountRomHack.Text = "Count = " + CountRomHack.ToString();
                labelCountRomHack.ForeColor = Color.White;
            }
        }

        private void DataGridViewVertexHack_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView_RowLeave(dataGridViewVertexHack, e.RowIndex))
            {
                CountVertexHack = dataGridViewVertexHack.Rows.Count - 1;
                labelCountVertexHack.Text = "Count = " + CountVertexHack.ToString();
                labelCountVertexHack.ForeColor = Color.White;
            }
        }

        private void DataGridViewFilterHack_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView_RowLeave(dataGridViewFilterHack, e.RowIndex))
            {
                CountFilterHack = dataGridViewFilterHack.Rows.Count - 1;
                labelCountFilterHack.Text = "Count = " + CountFilterHack.ToString();
                labelCountFilterHack.ForeColor = Color.White;
            }
        }

        private void DataGridViewCheat_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView_RowLeave(dataGridViewCheat, e.RowIndex);
        }

        private bool DataGridView_RowLeave(DataGridView dgv, int index)
        {
            if (dgv.IsCurrentRowDirty && dgv.Rows[index].Cells[0].Value == null)
            {
                for (int i = 0; ; i++)
                {
                    bool valid = true;
                    for (int j = 0; j < dgv.Rows.Count - 2; j++)
                    {
                        if ((int)dgv.Rows[j].Cells[0].Value == i)
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (valid)
                    {
                        dgv.Rows[index].Cells[0].Value = i;
                        return true;
                    }
                }
            }

            return false;
        }


        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("Error happened " + e.Context.ToString());

            if (e.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error: " + e.Exception.Message);
            }
            if (e.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (e.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("Parsing error");
            }
            if (e.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("Leave control error");
            }

            /*if ((e.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[e.RowIndex].ErrorText = "an error";
                view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "an error";

                e.ThrowException = false;
            }*/
        }

        #endregion
    }
}
