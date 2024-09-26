
using System.Drawing;

namespace MesDatas
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                if (timer != null)
                {
                    timer.Stop();
                    timer.Dispose();
                }
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabControl4 = new System.Windows.Forms.TabControl();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.groupBox40 = new System.Windows.Forms.GroupBox();
            this.btnSavePrinterConfig = new System.Windows.Forms.Button();
            this.label110 = new System.Windows.Forms.Label();
            this.cboPrintMode = new System.Windows.Forms.ComboBox();
            this.chkPlcControlPrint = new System.Windows.Forms.CheckBox();
            this.txtEndPrintPoint = new System.Windows.Forms.TextBox();
            this.label138 = new System.Windows.Forms.Label();
            this.label126 = new System.Windows.Forms.Label();
            this.txtStartPrintPoint = new System.Windows.Forms.TextBox();
            this.label137 = new System.Windows.Forms.Label();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.button31 = new System.Windows.Forms.Button();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.btnShowFilePath = new System.Windows.Forms.Button();
            this.button30 = new System.Windows.Forms.Button();
            this.lblPrnFilePath_TCP = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.button19 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnChangePath_TCP = new System.Windows.Forms.Button();
            this.txtPrinter_Port = new System.Windows.Forms.TextBox();
            this.label77 = new System.Windows.Forms.Label();
            this.txtPrinter_IP = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.chkLoadModel_TCP = new System.Windows.Forms.CheckBox();
            this.label81 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.txtPModel_TCP = new System.Windows.Forms.TextBox();
            this.textBox27 = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.label66 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.label65 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.textBox26 = new System.Windows.Forms.TextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.textBox25 = new System.Windows.Forms.TextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.cmbInstalledPrinters = new System.Windows.Forms.ComboBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.tabPage17 = new System.Windows.Forms.TabPage();
            this.groupBox41 = new System.Windows.Forms.GroupBox();
            this.lblPrintPrompt = new System.Windows.Forms.Label();
            this.groupBox35 = new System.Windows.Forms.GroupBox();
            this.cboPrinterType = new System.Windows.Forms.ComboBox();
            this.groupBox36 = new System.Windows.Forms.GroupBox();
            this.btnShowPath_COM = new System.Windows.Forms.Button();
            this.label82 = new System.Windows.Forms.Label();
            this.cboPrintFormat_COM = new System.Windows.Forms.ComboBox();
            this.btnPrint_COM = new System.Windows.Forms.Button();
            this.btnSave_COM = new System.Windows.Forms.Button();
            this.lblPrnFilePath_COM = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.btnChangePath_COM = new System.Windows.Forms.Button();
            this.label125 = new System.Windows.Forms.Label();
            this.groupBox37 = new System.Windows.Forms.GroupBox();
            this.groupBox38 = new System.Windows.Forms.GroupBox();
            this.label120 = new System.Windows.Forms.Label();
            this.chkLoadModel_COM = new System.Windows.Forms.CheckBox();
            this.chkUseFont = new System.Windows.Forms.CheckBox();
            this.txtPModel_COM = new System.Windows.Forms.TextBox();
            this.textBox53 = new System.Windows.Forms.TextBox();
            this.label127 = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.textBox54 = new System.Windows.Forms.TextBox();
            this.label129 = new System.Windows.Forms.Label();
            this.groupBox39 = new System.Windows.Forms.GroupBox();
            this.txtPrintCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkPlus2Print = new System.Windows.Forms.CheckBox();
            this.chkAutoAddDate = new System.Windows.Forms.CheckBox();
            this.txtSerialSpan = new System.Windows.Forms.TextBox();
            this.label132 = new System.Windows.Forms.Label();
            this.lblCodeContent = new System.Windows.Forms.Label();
            this.label134 = new System.Windows.Forms.Label();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.label135 = new System.Windows.Forms.Label();
            this.txtCodeNumber = new System.Windows.Forms.TextBox();
            this.label136 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblCurrentSelected = new System.Windows.Forms.Label();
            this.label141 = new System.Windows.Forms.Label();
            this.lblPlcAccess = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.groupBox42 = new System.Windows.Forms.GroupBox();
            this.label139 = new System.Windows.Forms.Label();
            this.lblReaderState = new System.Windows.Forms.Label();
            this.UPWD = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.UID = new System.Windows.Forms.TextBox();
            this.label97 = new System.Windows.Forms.Label();
            this.btnOpenReader = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btnRefreshUserData = new System.Windows.Forms.Button();
            this.UTYPE = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.tbxBrandID = new System.Windows.Forms.TextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.panel19 = new System.Windows.Forms.Panel();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.groupBox32 = new System.Windows.Forms.GroupBox();
            this.chkBypassBarcodeValidation = new System.Windows.Forms.CheckBox();
            this.chkBanLocalHistoricalData = new System.Windows.Forms.CheckBox();
            this.chkBypassLocalNgHistoricalData = new System.Windows.Forms.CheckBox();
            this.chkBypassFixtureValidation = new System.Windows.Forms.CheckBox();
            this.chkBypassQRcodeValidation = new System.Windows.Forms.CheckBox();
            this.groupBox29 = new System.Windows.Forms.GroupBox();
            this.label99 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.textBox36 = new System.Windows.Forms.TextBox();
            this.textBox31 = new System.Windows.Forms.TextBox();
            this.textBox29 = new System.Windows.Forms.TextBox();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.button28 = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.btnSaveAtSystemSetting = new System.Windows.Forms.Button();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.tbxReaderDeviceID = new System.Windows.Forms.TextBox();
            this.btnSearchReaderPort = new System.Windows.Forms.Button();
            this.cmbShowPort = new System.Windows.Forms.ComboBox();
            this.txtDeviceName = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.txtDefaultStyle = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.txtDisplayWidth = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblDataPath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChangeStoragePath = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cboConnectType = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.txt_IP = new System.Windows.Forms.TextBox();
            this.chkReadBarcodeSecondly = new System.Windows.Forms.CheckBox();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.panel16 = new System.Windows.Forms.Panel();
            this.button23 = new System.Windows.Forms.Button();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.panel21 = new System.Windows.Forms.Panel();
            this.btnSavePlcPoint = new System.Windows.Forms.Button();
            this.groupBox34 = new System.Windows.Forms.GroupBox();
            this.txtViewStatus = new System.Windows.Forms.TextBox();
            this.label123 = new System.Windows.Forms.Label();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.label119 = new System.Windows.Forms.Label();
            this.textBox34 = new System.Windows.Forms.TextBox();
            this.label118 = new System.Windows.Forms.Label();
            this.textBox47 = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.txtRecipeIdPoint = new System.Windows.Forms.TextBox();
            this.label111 = new System.Windows.Forms.Label();
            this.textBox43 = new System.Windows.Forms.TextBox();
            this.txtProductModelPoint = new System.Windows.Forms.TextBox();
            this.label112 = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.txtPMLength = new System.Windows.Forms.TextBox();
            this.label114 = new System.Windows.Forms.Label();
            this.txtDeviceStatePoint = new System.Windows.Forms.TextBox();
            this.label116 = new System.Windows.Forms.Label();
            this.groupBox33 = new System.Windows.Forms.GroupBox();
            this.txtProductResultPoint = new System.Windows.Forms.TextBox();
            this.label92 = new System.Windows.Forms.Label();
            this.txtEndProductPoint = new System.Windows.Forms.TextBox();
            this.txtSecondProductLength = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.txtReadSecondly = new System.Windows.Forms.TextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.txtStartPoint = new System.Windows.Forms.TextBox();
            this.label87 = new System.Windows.Forms.Label();
            this.tabPage18 = new System.Windows.Forms.TabPage();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.button37 = new System.Windows.Forms.Button();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_nccode = new System.Windows.Forms.TextBox();
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.textBox_opration = new System.Windows.Forms.TextBox();
            this.textBox_site = new System.Windows.Forms.TextBox();
            this.textBox_resource = new System.Windows.Forms.TextBox();
            this.textBox_timeout = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox_user = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.btnSaveMesConfig = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.rtbDashboardLog = new System.Windows.Forms.RichTextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.rtbProductLog = new System.Windows.Forms.RichTextBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.rtbMesLog = new System.Windows.Forms.RichTextBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.dataGridViewDynamic1 = new MesDatasCore.DataGridViewDynamic();
            this.panel12 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button14 = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.btnRefreshDirectory = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox_Code = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button13 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.数据源 = new System.Windows.Forms.GroupBox();
            this.directoryTreeView = new System.Windows.Forms.TreeView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.整页面 = new System.Windows.Forms.Panel();
            this.主页上半部分 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.groupBox31 = new System.Windows.Forms.GroupBox();
            this.dataGridViewDynamic3 = new MesDatasCore.DataGridViewDynamic();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lblProductResult = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lblOperatePrompt = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRunningStatus = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.条码数据 = new System.Windows.Forms.GroupBox();
            this.txtShowBarcode = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label73 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.chkBindWorkOrder = new System.Windows.Forms.CheckBox();
            this.btnChangeWorkOrder = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.txtWorkOrder = new System.Windows.Forms.TextBox();
            this.label104 = new System.Windows.Forms.Label();
            this.txtFixtureBinding = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.txtProductModel = new System.Windows.Forms.TextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.textBox42 = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.lblLoginMode = new System.Windows.Forms.Label();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.panel22 = new System.Windows.Forms.Panel();
            this.cboBarcodeRuleAndFixtures = new System.Windows.Forms.ComboBox();
            this.lblRecipeId = new System.Windows.Forms.Label();
            this.panel23 = new System.Windows.Forms.Panel();
            this.button25 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.chkReadRecipeId_PLC = new System.Windows.Forms.CheckBox();
            this.label103 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.dataGridViewDynamic4 = new MesDatasCore.DataGridViewDynamic();
            this.panel25 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblDeviceName = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label60 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label106 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.lblUploadStatus = new System.Windows.Forms.Label();
            this.lblValidationStatus = new System.Windows.Forms.Label();
            this.lblScanBarcodeStatus = new System.Windows.Forms.Label();
            this.lblPlcStatus = new System.Windows.Forms.Label();
            this.lblDashboardStatus = new System.Windows.Forms.Label();
            this.lblDeviceStatus = new System.Windows.Forms.Label();
            this.dataGridViewDynamic2 = new MesDatasCore.DataGridViewDynamic();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.panel20 = new System.Windows.Forms.Panel();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.dgvWeakInfo = new System.Windows.Forms.DataGridView();
            this.label39 = new System.Windows.Forms.Label();
            this.panel18 = new System.Windows.Forms.Panel();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.dgvFaultInfo = new System.Windows.Forms.DataGridView();
            this.label46 = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.chkReadPName = new System.Windows.Forms.CheckBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.label93 = new System.Windows.Forms.Label();
            this.btnRefreshAtBulletin = new System.Windows.Forms.Button();
            this.groupBox25 = new System.Windows.Forms.GroupBox();
            this.txtFaultLength = new System.Windows.Forms.TextBox();
            this.label101 = new System.Windows.Forms.Label();
            this.txtFaultStartPoint = new System.Windows.Forms.TextBox();
            this.label102 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.txtStationNameSets = new System.Windows.Forms.TextBox();
            this.label83 = new System.Windows.Forms.Label();
            this.chkEnableDashboard = new System.Windows.Forms.CheckBox();
            this.btnConnectDashboard = new System.Windows.Forms.Button();
            this.label40 = new System.Windows.Forms.Label();
            this.btnSaveAtDashboardSetting = new System.Windows.Forms.Button();
            this.label42 = new System.Windows.Forms.Label();
            this.txtDashboardPort = new System.Windows.Forms.TextBox();
            this.txtDashboardIP = new System.Windows.Forms.TextBox();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.groupBox30 = new System.Windows.Forms.GroupBox();
            this.label72 = new System.Windows.Forms.Label();
            this.btnSaveRecipeConfig = new System.Windows.Forms.Button();
            this.txtPointSets = new System.Windows.Forms.TextBox();
            this.txtNameSets = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label19 = new System.Windows.Forms.Label();
            this.tabPage4.SuspendLayout();
            this.tabControl4.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.groupBox40.SuspendLayout();
            this.tabPage16.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.groupBox26.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox24.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.tabPage17.SuspendLayout();
            this.groupBox41.SuspendLayout();
            this.groupBox35.SuspendLayout();
            this.groupBox36.SuspendLayout();
            this.groupBox37.SuspendLayout();
            this.groupBox38.SuspendLayout();
            this.groupBox39.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel6.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox42.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.panel19.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.groupBox32.SuspendLayout();
            this.groupBox29.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.groupBox27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.groupBox18.SuspendLayout();
            this.tabPage11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            this.panel16.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.panel21.SuspendLayout();
            this.groupBox34.SuspendLayout();
            this.groupBox33.SuspendLayout();
            this.tabPage18.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDynamic1)).BeginInit();
            this.panel12.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.panel3.SuspendLayout();
            this.数据源.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.整页面.SuspendLayout();
            this.主页上半部分.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel13.SuspendLayout();
            this.groupBox31.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDynamic3)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel11.SuspendLayout();
            this.条码数据.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDynamic4)).BeginInit();
            this.panel25.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDynamic2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.panel20.SuspendLayout();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeakInfo)).BeginInit();
            this.panel18.SuspendLayout();
            this.groupBox19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaultInfo)).BeginInit();
            this.panel17.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox25.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.groupBox30.SuspendLayout();
            this.groupBox28.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinDialogs = false;
            this.skinEngine1.SkinFile = null;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tabControl4);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabControl4
            // 
            this.tabControl4.Controls.Add(this.tabPage15);
            this.tabControl4.Controls.Add(this.tabPage16);
            this.tabControl4.Controls.Add(this.tabPage17);
            resources.ApplyResources(this.tabControl4, "tabControl4");
            this.tabControl4.Name = "tabControl4";
            this.tabControl4.SelectedIndex = 0;
            // 
            // tabPage15
            // 
            this.tabPage15.Controls.Add(this.groupBox40);
            resources.ApplyResources(this.tabPage15, "tabPage15");
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.UseVisualStyleBackColor = true;
            // 
            // groupBox40
            // 
            this.groupBox40.Controls.Add(this.btnSavePrinterConfig);
            this.groupBox40.Controls.Add(this.label110);
            this.groupBox40.Controls.Add(this.cboPrintMode);
            this.groupBox40.Controls.Add(this.chkPlcControlPrint);
            this.groupBox40.Controls.Add(this.txtEndPrintPoint);
            this.groupBox40.Controls.Add(this.label138);
            this.groupBox40.Controls.Add(this.label126);
            this.groupBox40.Controls.Add(this.txtStartPrintPoint);
            this.groupBox40.Controls.Add(this.label137);
            resources.ApplyResources(this.groupBox40, "groupBox40");
            this.groupBox40.Name = "groupBox40";
            this.groupBox40.TabStop = false;
            // 
            // btnSavePrinterConfig
            // 
            resources.ApplyResources(this.btnSavePrinterConfig, "btnSavePrinterConfig");
            this.btnSavePrinterConfig.Name = "btnSavePrinterConfig";
            this.btnSavePrinterConfig.UseVisualStyleBackColor = true;
            this.btnSavePrinterConfig.Click += new System.EventHandler(this.SavePrinterConfig_Click);
            // 
            // label110
            // 
            resources.ApplyResources(this.label110, "label110");
            this.label110.Name = "label110";
            // 
            // cboPrintMode
            // 
            this.cboPrintMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrintMode.FormattingEnabled = true;
            this.cboPrintMode.Items.AddRange(new object[] {
            resources.GetString("cboPrintMode.Items"),
            resources.GetString("cboPrintMode.Items1")});
            resources.ApplyResources(this.cboPrintMode, "cboPrintMode");
            this.cboPrintMode.Name = "cboPrintMode";
            // 
            // chkPlcControlPrint
            // 
            resources.ApplyResources(this.chkPlcControlPrint, "chkPlcControlPrint");
            this.chkPlcControlPrint.Name = "chkPlcControlPrint";
            this.chkPlcControlPrint.UseVisualStyleBackColor = true;
            // 
            // txtEndPrintPoint
            // 
            resources.ApplyResources(this.txtEndPrintPoint, "txtEndPrintPoint");
            this.txtEndPrintPoint.Name = "txtEndPrintPoint";
            // 
            // label138
            // 
            resources.ApplyResources(this.label138, "label138");
            this.label138.ForeColor = System.Drawing.Color.Red;
            this.label138.Name = "label138";
            // 
            // label126
            // 
            resources.ApplyResources(this.label126, "label126");
            this.label126.Name = "label126";
            // 
            // txtStartPrintPoint
            // 
            resources.ApplyResources(this.txtStartPrintPoint, "txtStartPrintPoint");
            this.txtStartPrintPoint.Name = "txtStartPrintPoint";
            // 
            // label137
            // 
            resources.ApplyResources(this.label137, "label137");
            this.label137.Name = "label137";
            // 
            // tabPage16
            // 
            this.tabPage16.Controls.Add(this.groupBox20);
            this.tabPage16.Controls.Add(this.groupBox22);
            this.tabPage16.Controls.Add(this.button8);
            resources.ApplyResources(this.tabPage16, "tabPage16");
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.UseVisualStyleBackColor = true;
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.button31);
            this.groupBox20.Controls.Add(this.groupBox26);
            this.groupBox20.Controls.Add(this.txtPrinter_Port);
            this.groupBox20.Controls.Add(this.label77);
            this.groupBox20.Controls.Add(this.txtPrinter_IP);
            this.groupBox20.Controls.Add(this.label75);
            this.groupBox20.Controls.Add(this.groupBox21);
            resources.ApplyResources(this.groupBox20, "groupBox20");
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.TabStop = false;
            // 
            // button31
            // 
            resources.ApplyResources(this.button31, "button31");
            this.button31.Name = "button31";
            this.button31.UseVisualStyleBackColor = true;
            this.button31.Click += new System.EventHandler(this.button31_Click);
            // 
            // groupBox26
            // 
            this.groupBox26.Controls.Add(this.btnShowFilePath);
            this.groupBox26.Controls.Add(this.button30);
            this.groupBox26.Controls.Add(this.lblPrnFilePath_TCP);
            this.groupBox26.Controls.Add(this.label84);
            this.groupBox26.Controls.Add(this.label47);
            this.groupBox26.Controls.Add(this.button19);
            this.groupBox26.Controls.Add(this.comboBox1);
            this.groupBox26.Controls.Add(this.btnChangePath_TCP);
            resources.ApplyResources(this.groupBox26, "groupBox26");
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.TabStop = false;
            // 
            // btnShowFilePath
            // 
            resources.ApplyResources(this.btnShowFilePath, "btnShowFilePath");
            this.btnShowFilePath.Name = "btnShowFilePath";
            this.btnShowFilePath.UseVisualStyleBackColor = true;
            this.btnShowFilePath.Click += new System.EventHandler(this.ShowPath_TCP_Click);
            // 
            // button30
            // 
            resources.ApplyResources(this.button30, "button30");
            this.button30.Name = "button30";
            this.button30.UseVisualStyleBackColor = true;
            this.button30.Click += new System.EventHandler(this.button30_Click);
            // 
            // lblPrnFilePath_TCP
            // 
            resources.ApplyResources(this.lblPrnFilePath_TCP, "lblPrnFilePath_TCP");
            this.lblPrnFilePath_TCP.Name = "lblPrnFilePath_TCP";
            // 
            // label84
            // 
            resources.ApplyResources(this.label84, "label84");
            this.label84.Name = "label84";
            // 
            // label47
            // 
            resources.ApplyResources(this.label47, "label47");
            this.label47.Name = "label47";
            // 
            // button19
            // 
            resources.ApplyResources(this.button19, "button19");
            this.button19.Name = "button19";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Name = "comboBox1";
            // 
            // btnChangePath_TCP
            // 
            resources.ApplyResources(this.btnChangePath_TCP, "btnChangePath_TCP");
            this.btnChangePath_TCP.Name = "btnChangePath_TCP";
            this.btnChangePath_TCP.UseVisualStyleBackColor = true;
            this.btnChangePath_TCP.Click += new System.EventHandler(this.ChangePath_TCP_Click);
            // 
            // txtPrinter_Port
            // 
            resources.ApplyResources(this.txtPrinter_Port, "txtPrinter_Port");
            this.txtPrinter_Port.Name = "txtPrinter_Port";
            // 
            // label77
            // 
            resources.ApplyResources(this.label77, "label77");
            this.label77.Name = "label77";
            // 
            // txtPrinter_IP
            // 
            resources.ApplyResources(this.txtPrinter_IP, "txtPrinter_IP");
            this.txtPrinter_IP.Name = "txtPrinter_IP";
            // 
            // label75
            // 
            resources.ApplyResources(this.label75, "label75");
            this.label75.Name = "label75";
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.groupBox24);
            this.groupBox21.Controls.Add(this.groupBox23);
            resources.ApplyResources(this.groupBox21, "groupBox21");
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.TabStop = false;
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.chkLoadModel_TCP);
            this.groupBox24.Controls.Add(this.label81);
            this.groupBox24.Controls.Add(this.checkBox3);
            this.groupBox24.Controls.Add(this.txtPModel_TCP);
            this.groupBox24.Controls.Add(this.textBox27);
            this.groupBox24.Controls.Add(this.label76);
            this.groupBox24.Controls.Add(this.label68);
            this.groupBox24.Controls.Add(this.textBox20);
            this.groupBox24.Controls.Add(this.label67);
            resources.ApplyResources(this.groupBox24, "groupBox24");
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.TabStop = false;
            // 
            // chkLoadModel_TCP
            // 
            resources.ApplyResources(this.chkLoadModel_TCP, "chkLoadModel_TCP");
            this.chkLoadModel_TCP.Name = "chkLoadModel_TCP";
            this.chkLoadModel_TCP.UseVisualStyleBackColor = true;
            // 
            // label81
            // 
            resources.ApplyResources(this.label81, "label81");
            this.label81.ForeColor = System.Drawing.Color.Red;
            this.label81.Name = "label81";
            // 
            // checkBox3
            // 
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // txtPModel_TCP
            // 
            resources.ApplyResources(this.txtPModel_TCP, "txtPModel_TCP");
            this.txtPModel_TCP.Name = "txtPModel_TCP";
            // 
            // textBox27
            // 
            resources.ApplyResources(this.textBox27, "textBox27");
            this.textBox27.Name = "textBox27";
            // 
            // label76
            // 
            resources.ApplyResources(this.label76, "label76");
            this.label76.Name = "label76";
            // 
            // label68
            // 
            resources.ApplyResources(this.label68, "label68");
            this.label68.Name = "label68";
            // 
            // textBox20
            // 
            resources.ApplyResources(this.textBox20, "textBox20");
            this.textBox20.Name = "textBox20";
            // 
            // label67
            // 
            resources.ApplyResources(this.label67, "label67");
            this.label67.Name = "label67";
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.label66);
            this.groupBox23.Controls.Add(this.label79);
            this.groupBox23.Controls.Add(this.textBox21);
            this.groupBox23.Controls.Add(this.label65);
            this.groupBox23.Controls.Add(this.label62);
            this.groupBox23.Controls.Add(this.label53);
            this.groupBox23.Controls.Add(this.textBox26);
            this.groupBox23.Controls.Add(this.label61);
            this.groupBox23.Controls.Add(this.textBox25);
            this.groupBox23.Controls.Add(this.label63);
            resources.ApplyResources(this.groupBox23, "groupBox23");
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.TabStop = false;
            // 
            // label66
            // 
            resources.ApplyResources(this.label66, "label66");
            this.label66.ForeColor = System.Drawing.Color.Red;
            this.label66.Name = "label66";
            // 
            // label79
            // 
            resources.ApplyResources(this.label79, "label79");
            this.label79.ForeColor = System.Drawing.Color.Red;
            this.label79.Name = "label79";
            // 
            // textBox21
            // 
            resources.ApplyResources(this.textBox21, "textBox21");
            this.textBox21.Name = "textBox21";
            // 
            // label65
            // 
            resources.ApplyResources(this.label65, "label65");
            this.label65.Name = "label65";
            // 
            // label62
            // 
            resources.ApplyResources(this.label62, "label62");
            this.label62.Name = "label62";
            // 
            // label53
            // 
            resources.ApplyResources(this.label53, "label53");
            this.label53.Name = "label53";
            // 
            // textBox26
            // 
            resources.ApplyResources(this.textBox26, "textBox26");
            this.textBox26.Name = "textBox26";
            // 
            // label61
            // 
            resources.ApplyResources(this.label61, "label61");
            this.label61.Name = "label61";
            // 
            // textBox25
            // 
            resources.ApplyResources(this.textBox25, "textBox25");
            this.textBox25.Name = "textBox25";
            // 
            // label63
            // 
            resources.ApplyResources(this.label63, "label63");
            this.label63.Name = "label63";
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.cmbInstalledPrinters);
            this.groupBox22.Controls.Add(this.richTextBox2);
            resources.ApplyResources(this.groupBox22, "groupBox22");
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.TabStop = false;
            // 
            // cmbInstalledPrinters
            // 
            this.cmbInstalledPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInstalledPrinters.FormattingEnabled = true;
            resources.ApplyResources(this.cmbInstalledPrinters, "cmbInstalledPrinters");
            this.cmbInstalledPrinters.Name = "cmbInstalledPrinters";
            // 
            // richTextBox2
            // 
            resources.ApplyResources(this.richTextBox2, "richTextBox2");
            this.richTextBox2.Name = "richTextBox2";
            // 
            // button8
            // 
            resources.ApplyResources(this.button8, "button8");
            this.button8.Name = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // tabPage17
            // 
            this.tabPage17.Controls.Add(this.groupBox41);
            this.tabPage17.Controls.Add(this.groupBox35);
            resources.ApplyResources(this.tabPage17, "tabPage17");
            this.tabPage17.Name = "tabPage17";
            this.tabPage17.UseVisualStyleBackColor = true;
            // 
            // groupBox41
            // 
            this.groupBox41.Controls.Add(this.lblPrintPrompt);
            resources.ApplyResources(this.groupBox41, "groupBox41");
            this.groupBox41.Name = "groupBox41";
            this.groupBox41.TabStop = false;
            // 
            // lblPrintPrompt
            // 
            resources.ApplyResources(this.lblPrintPrompt, "lblPrintPrompt");
            this.lblPrintPrompt.Name = "lblPrintPrompt";
            // 
            // groupBox35
            // 
            this.groupBox35.Controls.Add(this.cboPrinterType);
            this.groupBox35.Controls.Add(this.groupBox36);
            this.groupBox35.Controls.Add(this.label125);
            this.groupBox35.Controls.Add(this.groupBox37);
            resources.ApplyResources(this.groupBox35, "groupBox35");
            this.groupBox35.Name = "groupBox35";
            this.groupBox35.TabStop = false;
            // 
            // cboPrinterType
            // 
            this.cboPrinterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrinterType.FormattingEnabled = true;
            resources.ApplyResources(this.cboPrinterType, "cboPrinterType");
            this.cboPrinterType.Name = "cboPrinterType";
            // 
            // groupBox36
            // 
            this.groupBox36.Controls.Add(this.btnShowPath_COM);
            this.groupBox36.Controls.Add(this.label82);
            this.groupBox36.Controls.Add(this.cboPrintFormat_COM);
            this.groupBox36.Controls.Add(this.btnPrint_COM);
            this.groupBox36.Controls.Add(this.btnSave_COM);
            this.groupBox36.Controls.Add(this.lblPrnFilePath_COM);
            this.groupBox36.Controls.Add(this.label122);
            this.groupBox36.Controls.Add(this.btnChangePath_COM);
            resources.ApplyResources(this.groupBox36, "groupBox36");
            this.groupBox36.Name = "groupBox36";
            this.groupBox36.TabStop = false;
            // 
            // btnShowPath_COM
            // 
            resources.ApplyResources(this.btnShowPath_COM, "btnShowPath_COM");
            this.btnShowPath_COM.Name = "btnShowPath_COM";
            this.btnShowPath_COM.UseVisualStyleBackColor = true;
            this.btnShowPath_COM.Click += new System.EventHandler(this.ShowPath_COM_Click);
            // 
            // label82
            // 
            resources.ApplyResources(this.label82, "label82");
            this.label82.Name = "label82";
            // 
            // cboPrintFormat_COM
            // 
            this.cboPrintFormat_COM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrintFormat_COM.FormattingEnabled = true;
            this.cboPrintFormat_COM.Items.AddRange(new object[] {
            resources.GetString("cboPrintFormat_COM.Items"),
            resources.GetString("cboPrintFormat_COM.Items1")});
            resources.ApplyResources(this.cboPrintFormat_COM, "cboPrintFormat_COM");
            this.cboPrintFormat_COM.Name = "cboPrintFormat_COM";
            // 
            // btnPrint_COM
            // 
            resources.ApplyResources(this.btnPrint_COM, "btnPrint_COM");
            this.btnPrint_COM.Name = "btnPrint_COM";
            this.btnPrint_COM.UseVisualStyleBackColor = true;
            this.btnPrint_COM.Click += new System.EventHandler(this.BtnDriverPrint_Click);
            // 
            // btnSave_COM
            // 
            resources.ApplyResources(this.btnSave_COM, "btnSave_COM");
            this.btnSave_COM.Name = "btnSave_COM";
            this.btnSave_COM.UseVisualStyleBackColor = true;
            this.btnSave_COM.Click += new System.EventHandler(this.btnSave_COM_Click);
            // 
            // lblPrnFilePath_COM
            // 
            resources.ApplyResources(this.lblPrnFilePath_COM, "lblPrnFilePath_COM");
            this.lblPrnFilePath_COM.Name = "lblPrnFilePath_COM";
            // 
            // label122
            // 
            resources.ApplyResources(this.label122, "label122");
            this.label122.Name = "label122";
            // 
            // btnChangePath_COM
            // 
            resources.ApplyResources(this.btnChangePath_COM, "btnChangePath_COM");
            this.btnChangePath_COM.Name = "btnChangePath_COM";
            this.btnChangePath_COM.UseVisualStyleBackColor = true;
            this.btnChangePath_COM.Click += new System.EventHandler(this.ChangePath_Click);
            // 
            // label125
            // 
            resources.ApplyResources(this.label125, "label125");
            this.label125.Name = "label125";
            // 
            // groupBox37
            // 
            this.groupBox37.Controls.Add(this.groupBox38);
            this.groupBox37.Controls.Add(this.groupBox39);
            resources.ApplyResources(this.groupBox37, "groupBox37");
            this.groupBox37.Name = "groupBox37";
            this.groupBox37.TabStop = false;
            // 
            // groupBox38
            // 
            this.groupBox38.Controls.Add(this.label120);
            this.groupBox38.Controls.Add(this.chkLoadModel_COM);
            this.groupBox38.Controls.Add(this.chkUseFont);
            this.groupBox38.Controls.Add(this.txtPModel_COM);
            this.groupBox38.Controls.Add(this.textBox53);
            this.groupBox38.Controls.Add(this.label127);
            this.groupBox38.Controls.Add(this.label128);
            this.groupBox38.Controls.Add(this.textBox54);
            this.groupBox38.Controls.Add(this.label129);
            resources.ApplyResources(this.groupBox38, "groupBox38");
            this.groupBox38.Name = "groupBox38";
            this.groupBox38.TabStop = false;
            // 
            // label120
            // 
            resources.ApplyResources(this.label120, "label120");
            this.label120.ForeColor = System.Drawing.Color.Red;
            this.label120.Name = "label120";
            // 
            // chkLoadModel_COM
            // 
            resources.ApplyResources(this.chkLoadModel_COM, "chkLoadModel_COM");
            this.chkLoadModel_COM.Name = "chkLoadModel_COM";
            this.chkLoadModel_COM.UseVisualStyleBackColor = true;
            this.chkLoadModel_COM.CheckedChanged += new System.EventHandler(this.chkLoadModel_COM_CheckedChanged);
            // 
            // chkUseFont
            // 
            resources.ApplyResources(this.chkUseFont, "chkUseFont");
            this.chkUseFont.Name = "chkUseFont";
            this.chkUseFont.UseVisualStyleBackColor = true;
            this.chkUseFont.CheckedChanged += new System.EventHandler(this.chkUseFont_CheckedChanged);
            // 
            // txtPModel_COM
            // 
            resources.ApplyResources(this.txtPModel_COM, "txtPModel_COM");
            this.txtPModel_COM.Name = "txtPModel_COM";
            this.txtPModel_COM.TextChanged += new System.EventHandler(this.txtPModel_COM_TextChanged);
            // 
            // textBox53
            // 
            resources.ApplyResources(this.textBox53, "textBox53");
            this.textBox53.Name = "textBox53";
            this.textBox53.TextChanged += new System.EventHandler(this.textBox53_TextChanged);
            // 
            // label127
            // 
            resources.ApplyResources(this.label127, "label127");
            this.label127.Name = "label127";
            this.label127.Click += new System.EventHandler(this.label127_Click);
            // 
            // label128
            // 
            resources.ApplyResources(this.label128, "label128");
            this.label128.Name = "label128";
            this.label128.Click += new System.EventHandler(this.label128_Click);
            // 
            // textBox54
            // 
            resources.ApplyResources(this.textBox54, "textBox54");
            this.textBox54.Name = "textBox54";
            this.textBox54.TextChanged += new System.EventHandler(this.textBox54_TextChanged);
            // 
            // label129
            // 
            resources.ApplyResources(this.label129, "label129");
            this.label129.Name = "label129";
            this.label129.Click += new System.EventHandler(this.label129_Click);
            // 
            // groupBox39
            // 
            this.groupBox39.Controls.Add(this.label19);
            this.groupBox39.Controls.Add(this.txtPrintCount);
            this.groupBox39.Controls.Add(this.label2);
            this.groupBox39.Controls.Add(this.chkPlus2Print);
            this.groupBox39.Controls.Add(this.chkAutoAddDate);
            this.groupBox39.Controls.Add(this.txtSerialSpan);
            this.groupBox39.Controls.Add(this.label132);
            this.groupBox39.Controls.Add(this.lblCodeContent);
            this.groupBox39.Controls.Add(this.label134);
            this.groupBox39.Controls.Add(this.txtSerialNumber);
            this.groupBox39.Controls.Add(this.label135);
            this.groupBox39.Controls.Add(this.txtCodeNumber);
            this.groupBox39.Controls.Add(this.label136);
            resources.ApplyResources(this.groupBox39, "groupBox39");
            this.groupBox39.Name = "groupBox39";
            this.groupBox39.TabStop = false;
            // 
            // txtPrintCount
            // 
            resources.ApplyResources(this.txtPrintCount, "txtPrintCount");
            this.txtPrintCount.Name = "txtPrintCount";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // chkPlus2Print
            // 
            resources.ApplyResources(this.chkPlus2Print, "chkPlus2Print");
            this.chkPlus2Print.Name = "chkPlus2Print";
            this.chkPlus2Print.UseVisualStyleBackColor = true;
            // 
            // chkAutoAddDate
            // 
            resources.ApplyResources(this.chkAutoAddDate, "chkAutoAddDate");
            this.chkAutoAddDate.Name = "chkAutoAddDate";
            this.chkAutoAddDate.UseVisualStyleBackColor = true;
            // 
            // txtSerialSpan
            // 
            resources.ApplyResources(this.txtSerialSpan, "txtSerialSpan");
            this.txtSerialSpan.Name = "txtSerialSpan";
            // 
            // label132
            // 
            resources.ApplyResources(this.label132, "label132");
            this.label132.Name = "label132";
            // 
            // lblCodeContent
            // 
            resources.ApplyResources(this.lblCodeContent, "lblCodeContent");
            this.lblCodeContent.Name = "lblCodeContent";
            // 
            // label134
            // 
            resources.ApplyResources(this.label134, "label134");
            this.label134.Name = "label134";
            // 
            // txtSerialNumber
            // 
            resources.ApplyResources(this.txtSerialNumber, "txtSerialNumber");
            this.txtSerialNumber.Name = "txtSerialNumber";
            // 
            // label135
            // 
            resources.ApplyResources(this.label135, "label135");
            this.label135.Name = "label135";
            // 
            // txtCodeNumber
            // 
            resources.ApplyResources(this.txtCodeNumber, "txtCodeNumber");
            this.txtCodeNumber.Name = "txtCodeNumber";
            // 
            // label136
            // 
            resources.ApplyResources(this.label136, "label136");
            this.label136.Name = "label136";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Controls.Add(this.panel6);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "用户名";
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "用户密码";
            dataGridViewCellStyle3.Format = "*****";
            dataGridViewCellStyle3.NullValue = "#";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "用户权限";
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "厂牌UID";
            resources.ApplyResources(this.Column4, "Column4");
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "工号";
            resources.ApplyResources(this.Column5, "Column5");
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.groupBox6);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblCurrentSelected);
            this.groupBox6.Controls.Add(this.label141);
            this.groupBox6.Controls.Add(this.lblPlcAccess);
            this.groupBox6.Controls.Add(this.label131);
            this.groupBox6.Controls.Add(this.groupBox42);
            this.groupBox6.Controls.Add(this.UPWD);
            this.groupBox6.Controls.Add(this.textBox22);
            this.groupBox6.Controls.Add(this.UID);
            this.groupBox6.Controls.Add(this.label97);
            this.groupBox6.Controls.Add(this.btnOpenReader);
            this.groupBox6.Controls.Add(this.button5);
            this.groupBox6.Controls.Add(this.btnRefreshUserData);
            this.groupBox6.Controls.Add(this.UTYPE);
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Controls.Add(this.label86);
            this.groupBox6.Controls.Add(this.label96);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.button7);
            this.groupBox6.Controls.Add(this.tbxBrandID);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // lblCurrentSelected
            // 
            resources.ApplyResources(this.lblCurrentSelected, "lblCurrentSelected");
            this.lblCurrentSelected.Name = "lblCurrentSelected";
            // 
            // label141
            // 
            resources.ApplyResources(this.label141, "label141");
            this.label141.Name = "label141";
            // 
            // lblPlcAccess
            // 
            resources.ApplyResources(this.lblPlcAccess, "lblPlcAccess");
            this.lblPlcAccess.Name = "lblPlcAccess";
            // 
            // label131
            // 
            resources.ApplyResources(this.label131, "label131");
            this.label131.Name = "label131";
            // 
            // groupBox42
            // 
            this.groupBox42.Controls.Add(this.label139);
            this.groupBox42.Controls.Add(this.lblReaderState);
            this.groupBox42.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.groupBox42, "groupBox42");
            this.groupBox42.Name = "groupBox42";
            this.groupBox42.TabStop = false;
            // 
            // label139
            // 
            resources.ApplyResources(this.label139, "label139");
            this.label139.Name = "label139";
            // 
            // lblReaderState
            // 
            resources.ApplyResources(this.lblReaderState, "lblReaderState");
            this.lblReaderState.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblReaderState.Name = "lblReaderState";
            // 
            // UPWD
            // 
            resources.ApplyResources(this.UPWD, "UPWD");
            this.UPWD.Name = "UPWD";
            // 
            // textBox22
            // 
            resources.ApplyResources(this.textBox22, "textBox22");
            this.textBox22.Name = "textBox22";
            // 
            // UID
            // 
            resources.ApplyResources(this.UID, "UID");
            this.UID.Name = "UID";
            // 
            // label97
            // 
            resources.ApplyResources(this.label97, "label97");
            this.label97.ForeColor = System.Drawing.Color.Tomato;
            this.label97.Name = "label97";
            // 
            // btnOpenReader
            // 
            this.btnOpenReader.BackColor = System.Drawing.Color.BurlyWood;
            resources.ApplyResources(this.btnOpenReader, "btnOpenReader");
            this.btnOpenReader.Name = "btnOpenReader";
            this.btnOpenReader.UseVisualStyleBackColor = false;
            this.btnOpenReader.Click += new System.EventHandler(this.OpenReader_Click);
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnRefreshUserData
            // 
            resources.ApplyResources(this.btnRefreshUserData, "btnRefreshUserData");
            this.btnRefreshUserData.Name = "btnRefreshUserData";
            this.btnRefreshUserData.UseVisualStyleBackColor = true;
            this.btnRefreshUserData.Click += new System.EventHandler(this.btnRefreshUser_Click);
            // 
            // UTYPE
            // 
            this.UTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UTYPE.FormattingEnabled = true;
            this.UTYPE.Items.AddRange(new object[] {
            resources.GetString("UTYPE.Items"),
            resources.GetString("UTYPE.Items1"),
            resources.GetString("UTYPE.Items2"),
            resources.GetString("UTYPE.Items3"),
            resources.GetString("UTYPE.Items4")});
            resources.ApplyResources(this.UTYPE, "UTYPE");
            this.UTYPE.Name = "UTYPE";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // label86
            // 
            resources.ApplyResources(this.label86, "label86");
            this.label86.Name = "label86";
            // 
            // label96
            // 
            resources.ApplyResources(this.label96, "label96");
            this.label96.Name = "label96";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // button7
            // 
            resources.ApplyResources(this.button7, "button7");
            this.button7.Name = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbxBrandID
            // 
            this.tbxBrandID.BackColor = System.Drawing.SystemColors.ControlLight;
            resources.ApplyResources(this.tbxBrandID, "tbxBrandID");
            this.tbxBrandID.Name = "tbxBrandID";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.panel19);
            resources.ApplyResources(this.tabPage7, "tabPage7");
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.tabControl3);
            resources.ApplyResources(this.panel19, "panel19");
            this.panel19.Name = "panel19";
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage12);
            this.tabControl3.Controls.Add(this.tabPage13);
            this.tabControl3.Controls.Add(this.tabPage14);
            this.tabControl3.Controls.Add(this.tabPage18);
            resources.ApplyResources(this.tabControl3, "tabControl3");
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.groupBox32);
            this.tabPage12.Controls.Add(this.groupBox29);
            this.tabPage12.Controls.Add(this.btnSaveAtSystemSetting);
            this.tabPage12.Controls.Add(this.groupBox17);
            this.tabPage12.Controls.Add(this.groupBox3);
            this.tabPage12.Controls.Add(this.groupBox5);
            this.tabPage12.Controls.Add(this.chkReadBarcodeSecondly);
            resources.ApplyResources(this.tabPage12, "tabPage12");
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // groupBox32
            // 
            this.groupBox32.Controls.Add(this.chkBypassBarcodeValidation);
            this.groupBox32.Controls.Add(this.chkBanLocalHistoricalData);
            this.groupBox32.Controls.Add(this.chkBypassLocalNgHistoricalData);
            this.groupBox32.Controls.Add(this.chkBypassFixtureValidation);
            this.groupBox32.Controls.Add(this.chkBypassQRcodeValidation);
            resources.ApplyResources(this.groupBox32, "groupBox32");
            this.groupBox32.Name = "groupBox32";
            this.groupBox32.TabStop = false;
            // 
            // chkBypassBarcodeValidation
            // 
            resources.ApplyResources(this.chkBypassBarcodeValidation, "chkBypassBarcodeValidation");
            this.chkBypassBarcodeValidation.Name = "chkBypassBarcodeValidation";
            this.chkBypassBarcodeValidation.UseVisualStyleBackColor = true;
            // 
            // chkBanLocalHistoricalData
            // 
            resources.ApplyResources(this.chkBanLocalHistoricalData, "chkBanLocalHistoricalData");
            this.chkBanLocalHistoricalData.Name = "chkBanLocalHistoricalData";
            this.chkBanLocalHistoricalData.UseVisualStyleBackColor = true;
            // 
            // chkBypassLocalNgHistoricalData
            // 
            resources.ApplyResources(this.chkBypassLocalNgHistoricalData, "chkBypassLocalNgHistoricalData");
            this.chkBypassLocalNgHistoricalData.Name = "chkBypassLocalNgHistoricalData";
            this.chkBypassLocalNgHistoricalData.UseVisualStyleBackColor = true;
            // 
            // chkBypassFixtureValidation
            // 
            resources.ApplyResources(this.chkBypassFixtureValidation, "chkBypassFixtureValidation");
            this.chkBypassFixtureValidation.Name = "chkBypassFixtureValidation";
            this.chkBypassFixtureValidation.UseVisualStyleBackColor = true;
            // 
            // chkBypassQRcodeValidation
            // 
            resources.ApplyResources(this.chkBypassQRcodeValidation, "chkBypassQRcodeValidation");
            this.chkBypassQRcodeValidation.Name = "chkBypassQRcodeValidation";
            this.chkBypassQRcodeValidation.UseVisualStyleBackColor = true;
            // 
            // groupBox29
            // 
            this.groupBox29.Controls.Add(this.label99);
            this.groupBox29.Controls.Add(this.label98);
            this.groupBox29.Controls.Add(this.textBox36);
            this.groupBox29.Controls.Add(this.textBox31);
            this.groupBox29.Controls.Add(this.textBox29);
            this.groupBox29.Controls.Add(this.textBox23);
            this.groupBox29.Controls.Add(this.button28);
            this.groupBox29.Controls.Add(this.button27);
            resources.ApplyResources(this.groupBox29, "groupBox29");
            this.groupBox29.Name = "groupBox29";
            this.groupBox29.TabStop = false;
            // 
            // label99
            // 
            resources.ApplyResources(this.label99, "label99");
            this.label99.Name = "label99";
            // 
            // label98
            // 
            resources.ApplyResources(this.label98, "label98");
            this.label98.Name = "label98";
            // 
            // textBox36
            // 
            resources.ApplyResources(this.textBox36, "textBox36");
            this.textBox36.Name = "textBox36";
            // 
            // textBox31
            // 
            resources.ApplyResources(this.textBox31, "textBox31");
            this.textBox31.Name = "textBox31";
            // 
            // textBox29
            // 
            resources.ApplyResources(this.textBox29, "textBox29");
            this.textBox29.Name = "textBox29";
            // 
            // textBox23
            // 
            resources.ApplyResources(this.textBox23, "textBox23");
            this.textBox23.Name = "textBox23";
            // 
            // button28
            // 
            resources.ApplyResources(this.button28, "button28");
            this.button28.Name = "button28";
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            // 
            // button27
            // 
            resources.ApplyResources(this.button27, "button27");
            this.button27.Name = "button27";
            this.button27.UseVisualStyleBackColor = true;
            this.button27.Click += new System.EventHandler(this.button27_Click);
            // 
            // btnSaveAtSystemSetting
            // 
            resources.ApplyResources(this.btnSaveAtSystemSetting, "btnSaveAtSystemSetting");
            this.btnSaveAtSystemSetting.Name = "btnSaveAtSystemSetting";
            this.btnSaveAtSystemSetting.UseVisualStyleBackColor = true;
            this.btnSaveAtSystemSetting.Click += new System.EventHandler(this.BtnSaveAtSystemSetting_Click);
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.tbxReaderDeviceID);
            this.groupBox17.Controls.Add(this.btnSearchReaderPort);
            this.groupBox17.Controls.Add(this.cmbShowPort);
            this.groupBox17.Controls.Add(this.txtDeviceName);
            this.groupBox17.Controls.Add(this.label49);
            this.groupBox17.Controls.Add(this.txtDefaultStyle);
            this.groupBox17.Controls.Add(this.label45);
            this.groupBox17.Controls.Add(this.label8);
            this.groupBox17.Controls.Add(this.label64);
            this.groupBox17.Controls.Add(this.label78);
            this.groupBox17.Controls.Add(this.label91);
            this.groupBox17.Controls.Add(this.txtDisplayWidth);
            resources.ApplyResources(this.groupBox17, "groupBox17");
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.TabStop = false;
            // 
            // tbxReaderDeviceID
            // 
            this.tbxReaderDeviceID.BackColor = System.Drawing.SystemColors.MenuBar;
            resources.ApplyResources(this.tbxReaderDeviceID, "tbxReaderDeviceID");
            this.tbxReaderDeviceID.Name = "tbxReaderDeviceID";
            // 
            // btnSearchReaderPort
            // 
            resources.ApplyResources(this.btnSearchReaderPort, "btnSearchReaderPort");
            this.btnSearchReaderPort.Name = "btnSearchReaderPort";
            this.btnSearchReaderPort.UseVisualStyleBackColor = true;
            this.btnSearchReaderPort.Click += new System.EventHandler(this.SearchPort_Click);
            // 
            // cmbShowPort
            // 
            this.cmbShowPort.FormattingEnabled = true;
            resources.ApplyResources(this.cmbShowPort, "cmbShowPort");
            this.cmbShowPort.Name = "cmbShowPort";
            // 
            // txtDeviceName
            // 
            resources.ApplyResources(this.txtDeviceName, "txtDeviceName");
            this.txtDeviceName.Name = "txtDeviceName";
            // 
            // label49
            // 
            resources.ApplyResources(this.label49, "label49");
            this.label49.Name = "label49";
            // 
            // txtDefaultStyle
            // 
            resources.ApplyResources(this.txtDefaultStyle, "txtDefaultStyle");
            this.txtDefaultStyle.Name = "txtDefaultStyle";
            // 
            // label45
            // 
            resources.ApplyResources(this.label45, "label45");
            this.label45.Name = "label45";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label64
            // 
            resources.ApplyResources(this.label64, "label64");
            this.label64.Name = "label64";
            // 
            // label78
            // 
            resources.ApplyResources(this.label78, "label78");
            this.label78.Name = "label78";
            // 
            // label91
            // 
            resources.ApplyResources(this.label91, "label91");
            this.label91.ForeColor = System.Drawing.Color.Red;
            this.label91.Name = "label91";
            // 
            // txtDisplayWidth
            // 
            resources.ApplyResources(this.txtDisplayWidth, "txtDisplayWidth");
            this.txtDisplayWidth.Name = "txtDisplayWidth";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblDataPath);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btnChangeStoragePath);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // lblDataPath
            // 
            resources.ApplyResources(this.lblDataPath, "lblDataPath");
            this.lblDataPath.Name = "lblDataPath";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnChangeStoragePath
            // 
            resources.ApplyResources(this.btnChangeStoragePath, "btnChangeStoragePath");
            this.btnChangeStoragePath.Name = "btnChangeStoragePath";
            this.btnChangeStoragePath.UseVisualStyleBackColor = true;
            this.btnChangeStoragePath.Click += new System.EventHandler(this.ChangeStoragePath);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cboConnectType);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Controls.Add(this.label30);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.txt_port);
            this.groupBox5.Controls.Add(this.txt_IP);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // cboConnectType
            // 
            this.cboConnectType.FormattingEnabled = true;
            this.cboConnectType.Items.AddRange(new object[] {
            resources.GetString("cboConnectType.Items"),
            resources.GetString("cboConnectType.Items1"),
            resources.GetString("cboConnectType.Items2")});
            resources.ApplyResources(this.cboConnectType, "cboConnectType");
            this.cboConnectType.Name = "cboConnectType";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.BtnConnectPlc_Click);
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txt_port
            // 
            resources.ApplyResources(this.txt_port, "txt_port");
            this.txt_port.Name = "txt_port";
            // 
            // txt_IP
            // 
            resources.ApplyResources(this.txt_IP, "txt_IP");
            this.txt_IP.Name = "txt_IP";
            // 
            // chkReadBarcodeSecondly
            // 
            resources.ApplyResources(this.chkReadBarcodeSecondly, "chkReadBarcodeSecondly");
            this.chkReadBarcodeSecondly.Name = "chkReadBarcodeSecondly";
            this.chkReadBarcodeSecondly.UseVisualStyleBackColor = true;
            // 
            // tabPage13
            // 
            this.tabPage13.Controls.Add(this.tabControl2);
            this.tabPage13.Controls.Add(this.panel16);
            resources.ApplyResources(this.tabPage13, "tabPage13");
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage10);
            this.tabControl2.Controls.Add(this.tabPage11);
            resources.ApplyResources(this.tabControl2, "tabControl2");
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.groupBox27);
            this.tabPage10.Controls.Add(this.groupBox18);
            resources.ApplyResources(this.tabPage10, "tabPage10");
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // groupBox27
            // 
            this.groupBox27.Controls.Add(this.dataGridView4);
            resources.ApplyResources(this.groupBox27, "groupBox27");
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.TabStop = false;
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView4, "dataGridView4");
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowHeadersVisible = false;
            this.dataGridView4.RowTemplate.Height = 27;
            this.dataGridView4.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView4_CellContentClick);
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.label14);
            resources.ApplyResources(this.groupBox18, "groupBox18");
            this.groupBox18.ForeColor = System.Drawing.Color.Red;
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.TabStop = false;
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Name = "label14";
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.dataGridView6);
            resources.ApplyResources(this.tabPage11, "tabPage11");
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // dataGridView6
            // 
            this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView6, "dataGridView6");
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.RowTemplate.Height = 27;
            this.dataGridView6.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView6_CellContentClick);
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.button23);
            resources.ApplyResources(this.panel16, "panel16");
            this.panel16.Name = "panel16";
            // 
            // button23
            // 
            resources.ApplyResources(this.button23, "button23");
            this.button23.Name = "button23";
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // tabPage14
            // 
            this.tabPage14.Controls.Add(this.panel21);
            resources.ApplyResources(this.tabPage14, "tabPage14");
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.UseVisualStyleBackColor = true;
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.btnSavePlcPoint);
            this.panel21.Controls.Add(this.groupBox34);
            this.panel21.Controls.Add(this.groupBox33);
            resources.ApplyResources(this.panel21, "panel21");
            this.panel21.Name = "panel21";
            // 
            // btnSavePlcPoint
            // 
            resources.ApplyResources(this.btnSavePlcPoint, "btnSavePlcPoint");
            this.btnSavePlcPoint.Name = "btnSavePlcPoint";
            this.btnSavePlcPoint.UseVisualStyleBackColor = true;
            this.btnSavePlcPoint.Click += new System.EventHandler(this.PLCPointSaveBtn_Click);
            // 
            // groupBox34
            // 
            this.groupBox34.Controls.Add(this.txtViewStatus);
            this.groupBox34.Controls.Add(this.label123);
            this.groupBox34.Controls.Add(this.textBox35);
            this.groupBox34.Controls.Add(this.label119);
            this.groupBox34.Controls.Add(this.textBox34);
            this.groupBox34.Controls.Add(this.label118);
            this.groupBox34.Controls.Add(this.textBox47);
            this.groupBox34.Controls.Add(this.label117);
            this.groupBox34.Controls.Add(this.txtRecipeIdPoint);
            this.groupBox34.Controls.Add(this.label111);
            this.groupBox34.Controls.Add(this.textBox43);
            this.groupBox34.Controls.Add(this.txtProductModelPoint);
            this.groupBox34.Controls.Add(this.label112);
            this.groupBox34.Controls.Add(this.label113);
            this.groupBox34.Controls.Add(this.txtPMLength);
            this.groupBox34.Controls.Add(this.label114);
            this.groupBox34.Controls.Add(this.txtDeviceStatePoint);
            this.groupBox34.Controls.Add(this.label116);
            resources.ApplyResources(this.groupBox34, "groupBox34");
            this.groupBox34.Name = "groupBox34";
            this.groupBox34.TabStop = false;
            // 
            // txtViewStatus
            // 
            resources.ApplyResources(this.txtViewStatus, "txtViewStatus");
            this.txtViewStatus.Name = "txtViewStatus";
            // 
            // label123
            // 
            resources.ApplyResources(this.label123, "label123");
            this.label123.Name = "label123";
            // 
            // textBox35
            // 
            resources.ApplyResources(this.textBox35, "textBox35");
            this.textBox35.Name = "textBox35";
            // 
            // label119
            // 
            resources.ApplyResources(this.label119, "label119");
            this.label119.Name = "label119";
            // 
            // textBox34
            // 
            resources.ApplyResources(this.textBox34, "textBox34");
            this.textBox34.Name = "textBox34";
            // 
            // label118
            // 
            resources.ApplyResources(this.label118, "label118");
            this.label118.Name = "label118";
            // 
            // textBox47
            // 
            resources.ApplyResources(this.textBox47, "textBox47");
            this.textBox47.Name = "textBox47";
            // 
            // label117
            // 
            resources.ApplyResources(this.label117, "label117");
            this.label117.Name = "label117";
            // 
            // txtRecipeIdPoint
            // 
            resources.ApplyResources(this.txtRecipeIdPoint, "txtRecipeIdPoint");
            this.txtRecipeIdPoint.Name = "txtRecipeIdPoint";
            // 
            // label111
            // 
            resources.ApplyResources(this.label111, "label111");
            this.label111.Name = "label111";
            // 
            // textBox43
            // 
            resources.ApplyResources(this.textBox43, "textBox43");
            this.textBox43.Name = "textBox43";
            // 
            // txtProductModelPoint
            // 
            resources.ApplyResources(this.txtProductModelPoint, "txtProductModelPoint");
            this.txtProductModelPoint.Name = "txtProductModelPoint";
            // 
            // label112
            // 
            resources.ApplyResources(this.label112, "label112");
            this.label112.Name = "label112";
            // 
            // label113
            // 
            resources.ApplyResources(this.label113, "label113");
            this.label113.Name = "label113";
            // 
            // txtPMLength
            // 
            resources.ApplyResources(this.txtPMLength, "txtPMLength");
            this.txtPMLength.Name = "txtPMLength";
            // 
            // label114
            // 
            resources.ApplyResources(this.label114, "label114");
            this.label114.Name = "label114";
            // 
            // txtDeviceStatePoint
            // 
            resources.ApplyResources(this.txtDeviceStatePoint, "txtDeviceStatePoint");
            this.txtDeviceStatePoint.Name = "txtDeviceStatePoint";
            // 
            // label116
            // 
            resources.ApplyResources(this.label116, "label116");
            this.label116.Name = "label116";
            // 
            // groupBox33
            // 
            this.groupBox33.Controls.Add(this.txtProductResultPoint);
            this.groupBox33.Controls.Add(this.label92);
            this.groupBox33.Controls.Add(this.txtEndProductPoint);
            this.groupBox33.Controls.Add(this.txtSecondProductLength);
            this.groupBox33.Controls.Add(this.label90);
            this.groupBox33.Controls.Add(this.label89);
            this.groupBox33.Controls.Add(this.txtReadSecondly);
            this.groupBox33.Controls.Add(this.label88);
            this.groupBox33.Controls.Add(this.txtStartPoint);
            this.groupBox33.Controls.Add(this.label87);
            resources.ApplyResources(this.groupBox33, "groupBox33");
            this.groupBox33.Name = "groupBox33";
            this.groupBox33.TabStop = false;
            // 
            // txtProductResultPoint
            // 
            resources.ApplyResources(this.txtProductResultPoint, "txtProductResultPoint");
            this.txtProductResultPoint.Name = "txtProductResultPoint";
            // 
            // label92
            // 
            resources.ApplyResources(this.label92, "label92");
            this.label92.Name = "label92";
            // 
            // txtEndProductPoint
            // 
            resources.ApplyResources(this.txtEndProductPoint, "txtEndProductPoint");
            this.txtEndProductPoint.Name = "txtEndProductPoint";
            // 
            // txtSecondProductLength
            // 
            resources.ApplyResources(this.txtSecondProductLength, "txtSecondProductLength");
            this.txtSecondProductLength.Name = "txtSecondProductLength";
            // 
            // label90
            // 
            resources.ApplyResources(this.label90, "label90");
            this.label90.Name = "label90";
            // 
            // label89
            // 
            resources.ApplyResources(this.label89, "label89");
            this.label89.Name = "label89";
            // 
            // txtReadSecondly
            // 
            resources.ApplyResources(this.txtReadSecondly, "txtReadSecondly");
            this.txtReadSecondly.Name = "txtReadSecondly";
            // 
            // label88
            // 
            resources.ApplyResources(this.label88, "label88");
            this.label88.Name = "label88";
            // 
            // txtStartPoint
            // 
            resources.ApplyResources(this.txtStartPoint, "txtStartPoint");
            this.txtStartPoint.Name = "txtStartPoint";
            // 
            // label87
            // 
            resources.ApplyResources(this.label87, "label87");
            this.label87.Name = "label87";
            // 
            // tabPage18
            // 
            this.tabPage18.Controls.Add(this.richTextBox5);
            this.tabPage18.Controls.Add(this.button37);
            this.tabPage18.Controls.Add(this.richTextBox6);
            resources.ApplyResources(this.tabPage18, "tabPage18");
            this.tabPage18.Name = "tabPage18";
            this.tabPage18.UseVisualStyleBackColor = true;
            // 
            // richTextBox5
            // 
            resources.ApplyResources(this.richTextBox5, "richTextBox5");
            this.richTextBox5.Name = "richTextBox5";
            // 
            // button37
            // 
            resources.ApplyResources(this.button37, "button37");
            this.button37.Name = "button37";
            this.button37.UseVisualStyleBackColor = true;
            this.button37.Click += new System.EventHandler(this.button37_Click);
            // 
            // richTextBox6
            // 
            resources.ApplyResources(this.richTextBox6, "richTextBox6");
            this.richTextBox6.Name = "richTextBox6";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.tableLayoutPanel1);
            this.tabPage5.Controls.Add(this.btnSaveMesConfig);
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label74, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_nccode, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.textBox_url, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBox_opration, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.textBox_site, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBox_resource, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBox_timeout, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox_port, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_ip, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label24, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label27, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label26, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label25, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label23, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.textBox_user, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.textBox_password, 1, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label74
            // 
            resources.ApplyResources(this.label74, "label74");
            this.label74.Name = "label74";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // textBox_nccode
            // 
            resources.ApplyResources(this.textBox_nccode, "textBox_nccode");
            this.textBox_nccode.Name = "textBox_nccode";
            // 
            // textBox_url
            // 
            resources.ApplyResources(this.textBox_url, "textBox_url");
            this.textBox_url.Name = "textBox_url";
            // 
            // textBox_opration
            // 
            resources.ApplyResources(this.textBox_opration, "textBox_opration");
            this.textBox_opration.Name = "textBox_opration";
            // 
            // textBox_site
            // 
            resources.ApplyResources(this.textBox_site, "textBox_site");
            this.textBox_site.Name = "textBox_site";
            // 
            // textBox_resource
            // 
            resources.ApplyResources(this.textBox_resource, "textBox_resource");
            this.textBox_resource.Name = "textBox_resource";
            // 
            // textBox_timeout
            // 
            resources.ApplyResources(this.textBox_timeout, "textBox_timeout");
            this.textBox_timeout.Name = "textBox_timeout";
            // 
            // textBox_port
            // 
            resources.ApplyResources(this.textBox_port, "textBox_port");
            this.textBox_port.Name = "textBox_port";
            // 
            // textBox_ip
            // 
            resources.ApplyResources(this.textBox_ip, "textBox_ip");
            this.textBox_ip.Name = "textBox_ip";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // textBox_user
            // 
            resources.ApplyResources(this.textBox_user, "textBox_user");
            this.textBox_user.Name = "textBox_user";
            // 
            // textBox_password
            // 
            resources.ApplyResources(this.textBox_password, "textBox_password");
            this.textBox_password.Name = "textBox_password";
            // 
            // btnSaveMesConfig
            // 
            resources.ApplyResources(this.btnSaveMesConfig, "btnSaveMesConfig");
            this.btnSaveMesConfig.Name = "btnSaveMesConfig";
            this.btnSaveMesConfig.UseVisualStyleBackColor = true;
            this.btnSaveMesConfig.Click += new System.EventHandler(this.SaveMesConfig_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox12);
            this.tabPage2.Controls.Add(this.groupBox9);
            this.tabPage2.Controls.Add(this.groupBox14);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.rtbDashboardLog);
            resources.ApplyResources(this.groupBox12, "groupBox12");
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.TabStop = false;
            // 
            // rtbDashboardLog
            // 
            resources.ApplyResources(this.rtbDashboardLog, "rtbDashboardLog");
            this.rtbDashboardLog.Name = "rtbDashboardLog";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.rtbProductLog);
            resources.ApplyResources(this.groupBox9, "groupBox9");
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.TabStop = false;
            // 
            // rtbProductLog
            // 
            resources.ApplyResources(this.rtbProductLog, "rtbProductLog");
            this.rtbProductLog.Name = "rtbProductLog";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.rtbMesLog);
            resources.ApplyResources(this.groupBox14, "groupBox14");
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.TabStop = false;
            // 
            // rtbMesLog
            // 
            resources.ApplyResources(this.rtbMesLog, "rtbMesLog");
            this.rtbMesLog.Name = "rtbMesLog";
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.panel5);
            this.tabPage8.Controls.Add(this.panel4);
            this.tabPage8.Controls.Add(this.panel3);
            resources.ApplyResources(this.tabPage8, "tabPage8");
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox4);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panel14);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.panel15);
            this.panel14.Controls.Add(this.panel12);
            resources.ApplyResources(this.panel14, "panel14");
            this.panel14.Name = "panel14";
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.dataGridViewDynamic1);
            resources.ApplyResources(this.panel15, "panel15");
            this.panel15.Name = "panel15";
            // 
            // dataGridViewDynamic1
            // 
            this.dataGridViewDynamic1.AllowUserToAddRows = false;
            this.dataGridViewDynamic1.AllowUserToDeleteRows = false;
            this.dataGridViewDynamic1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDynamic1.ColumnKey = null;
            resources.ApplyResources(this.dataGridViewDynamic1, "dataGridViewDynamic1");
            this.dataGridViewDynamic1.Name = "dataGridViewDynamic1";
            this.dataGridViewDynamic1.ReadOnly = true;
            this.dataGridViewDynamic1.RowTemplate.Height = 23;
            this.dataGridViewDynamic1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridViewDynamic1_RowPrePaint);
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.tableLayoutPanel3);
            resources.ApplyResources(this.panel12, "panel12");
            this.panel12.Name = "panel12";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.button14, 13, 0);
            this.tableLayoutPanel3.Controls.Add(this.label29, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.button12, 11, 0);
            this.tableLayoutPanel3.Controls.Add(this.button11, 10, 0);
            this.tableLayoutPanel3.Controls.Add(this.button9, 9, 0);
            this.tableLayoutPanel3.Controls.Add(this.button4, 8, 0);
            this.tableLayoutPanel3.Controls.Add(this.label37, 7, 0);
            this.tableLayoutPanel3.Controls.Add(this.label36, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.label35, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.label34, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBox13, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label33, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label31, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBox16, 12, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // button14
            // 
            resources.ApplyResources(this.button14, "button14");
            this.button14.Name = "button14";
            this.button14.TabStop = false;
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // button12
            // 
            resources.ApplyResources(this.button12, "button12");
            this.button12.Name = "button12";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            resources.ApplyResources(this.button11, "button11");
            this.button11.Name = "button11";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click_1);
            // 
            // button9
            // 
            resources.ApplyResources(this.button9, "button9");
            this.button9.Name = "button9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click_1);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label37
            // 
            resources.ApplyResources(this.label37, "label37");
            this.label37.Name = "label37";
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.Name = "label36";
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.Name = "label35";
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.Name = "label34";
            // 
            // textBox13
            // 
            resources.ApplyResources(this.textBox13, "textBox13");
            this.textBox13.Name = "textBox13";
            this.textBox13.TextChanged += new System.EventHandler(this.textBox13_TextChanged);
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // textBox16
            // 
            resources.ApplyResources(this.textBox16, "textBox16");
            this.textBox16.Name = "textBox16";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox16);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.btnRefreshDirectory);
            this.groupBox16.Controls.Add(this.textBox1);
            this.groupBox16.Controls.Add(this.textBox_Code);
            this.groupBox16.Controls.Add(this.dateTimePicker1);
            this.groupBox16.Controls.Add(this.dateTimePicker2);
            this.groupBox16.Controls.Add(this.textBoxPath);
            this.groupBox16.Controls.Add(this.buttonSearch);
            this.groupBox16.Controls.Add(this.label3);
            this.groupBox16.Controls.Add(this.label4);
            this.groupBox16.Controls.Add(this.label13);
            this.groupBox16.Controls.Add(this.label6);
            this.groupBox16.Controls.Add(this.button13);
            this.groupBox16.Controls.Add(this.label15);
            resources.ApplyResources(this.groupBox16, "groupBox16");
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.TabStop = false;
            // 
            // btnRefreshDirectory
            // 
            resources.ApplyResources(this.btnRefreshDirectory, "btnRefreshDirectory");
            this.btnRefreshDirectory.Name = "btnRefreshDirectory";
            this.btnRefreshDirectory.UseVisualStyleBackColor = true;
            this.btnRefreshDirectory.Click += new System.EventHandler(this.BtnRefreshDirectory_Click);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // textBox_Code
            // 
            resources.ApplyResources(this.textBox_Code, "textBox_Code");
            this.textBox_Code.Name = "textBox_Code";
            // 
            // dateTimePicker1
            // 
            resources.ApplyResources(this.dateTimePicker1, "dateTimePicker1");
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.MaxDate = new System.DateTime(9998, 12, 17, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Value = new System.DateTime(2024, 3, 10, 10, 26, 0, 0);
            // 
            // dateTimePicker2
            // 
            resources.ApplyResources(this.dateTimePicker2, "dateTimePicker2");
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Value = new System.DateTime(2024, 3, 10, 10, 27, 0, 0);
            // 
            // textBoxPath
            // 
            resources.ApplyResources(this.textBoxPath, "textBoxPath");
            this.textBoxPath.Name = "textBoxPath";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.buttonSearch, "buttonSearch");
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // button13
            // 
            this.button13.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.button13, "button13");
            this.button13.Name = "button13";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.ExportProductData);
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.数据源);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // 数据源
            // 
            this.数据源.Controls.Add(this.directoryTreeView);
            resources.ApplyResources(this.数据源, "数据源");
            this.数据源.Name = "数据源";
            this.数据源.TabStop = false;
            // 
            // directoryTreeView
            // 
            resources.ApplyResources(this.directoryTreeView, "directoryTreeView");
            this.directoryTreeView.Name = "directoryTreeView";
            this.directoryTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.directoryTreeView_AfterSelect);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.整页面);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // 整页面
            // 
            this.整页面.Controls.Add(this.主页上半部分);
            resources.ApplyResources(this.整页面, "整页面");
            this.整页面.Name = "整页面";
            // 
            // 主页上半部分
            // 
            this.主页上半部分.Controls.Add(this.panel10);
            this.主页上半部分.Controls.Add(this.panel8);
            resources.ApplyResources(this.主页上半部分, "主页上半部分");
            this.主页上半部分.Name = "主页上半部分";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.panel13);
            this.panel10.Controls.Add(this.groupBox8);
            this.panel10.Controls.Add(this.groupBox7);
            this.panel10.Controls.Add(this.groupBox2);
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.panel10, "panel10");
            this.panel10.Name = "panel10";
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.groupBox31);
            resources.ApplyResources(this.panel13, "panel13");
            this.panel13.Name = "panel13";
            // 
            // groupBox31
            // 
            this.groupBox31.Controls.Add(this.dataGridViewDynamic3);
            resources.ApplyResources(this.groupBox31, "groupBox31");
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.TabStop = false;
            // 
            // dataGridViewDynamic3
            // 
            this.dataGridViewDynamic3.AllowUserToAddRows = false;
            this.dataGridViewDynamic3.AllowUserToDeleteRows = false;
            this.dataGridViewDynamic3.AllowUserToResizeColumns = false;
            this.dataGridViewDynamic3.AllowUserToResizeRows = false;
            this.dataGridViewDynamic3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDynamic3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewDynamic3.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewDynamic3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDynamic3.ColumnKey = null;
            resources.ApplyResources(this.dataGridViewDynamic3, "dataGridViewDynamic3");
            this.dataGridViewDynamic3.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridViewDynamic3.Name = "dataGridViewDynamic3";
            this.dataGridViewDynamic3.RowHeadersVisible = false;
            this.dataGridViewDynamic3.RowTemplate.Height = 25;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lblProductResult);
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            // 
            // lblProductResult
            // 
            this.lblProductResult.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblProductResult, "lblProductResult");
            this.lblProductResult.Name = "lblProductResult";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.lblOperatePrompt);
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // lblOperatePrompt
            // 
            resources.ApplyResources(this.lblOperatePrompt, "lblOperatePrompt");
            this.lblOperatePrompt.Name = "lblOperatePrompt";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblRunningStatus);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // lblRunningStatus
            // 
            resources.ApplyResources(this.lblRunningStatus, "lblRunningStatus");
            this.lblRunningStatus.Name = "lblRunningStatus";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.条码数据);
            resources.ApplyResources(this.panel11, "panel11");
            this.panel11.Name = "panel11";
            // 
            // 条码数据
            // 
            this.条码数据.Controls.Add(this.txtShowBarcode);
            resources.ApplyResources(this.条码数据, "条码数据");
            this.条码数据.Name = "条码数据";
            this.条码数据.TabStop = false;
            // 
            // txtShowBarcode
            // 
            resources.ApplyResources(this.txtShowBarcode, "txtShowBarcode");
            this.txtShowBarcode.ForeColor = System.Drawing.Color.Goldenrod;
            this.txtShowBarcode.Name = "txtShowBarcode";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.label73, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.panel9, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.label17, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.txtWorkOrder, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label104, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.txtFixtureBinding, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label32, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtProductModel, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label80, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBox42, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label44, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentUser, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label69, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblLoginMode, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentTime, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel22, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.panel23, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.label103, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.label94, 0, 7);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // label73
            // 
            resources.ApplyResources(this.label73, "label73");
            this.label73.BackColor = System.Drawing.Color.Transparent;
            this.label73.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label73.Name = "label73";
            // 
            // panel9
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel9, 3);
            this.panel9.Controls.Add(this.chkBindWorkOrder);
            this.panel9.Controls.Add(this.btnChangeWorkOrder);
            resources.ApplyResources(this.panel9, "panel9");
            this.panel9.Name = "panel9";
            // 
            // chkBindWorkOrder
            // 
            resources.ApplyResources(this.chkBindWorkOrder, "chkBindWorkOrder");
            this.chkBindWorkOrder.Name = "chkBindWorkOrder";
            this.chkBindWorkOrder.UseVisualStyleBackColor = true;
            // 
            // btnChangeWorkOrder
            // 
            resources.ApplyResources(this.btnChangeWorkOrder, "btnChangeWorkOrder");
            this.btnChangeWorkOrder.Name = "btnChangeWorkOrder";
            this.btnChangeWorkOrder.UseVisualStyleBackColor = true;
            this.btnChangeWorkOrder.Click += new System.EventHandler(this.ChangeWorkOrder_Click);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // txtWorkOrder
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.txtWorkOrder, 3);
            resources.ApplyResources(this.txtWorkOrder, "txtWorkOrder");
            this.txtWorkOrder.Name = "txtWorkOrder";
            // 
            // label104
            // 
            resources.ApplyResources(this.label104, "label104");
            this.label104.Name = "label104";
            // 
            // txtFixtureBinding
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.txtFixtureBinding, 3);
            resources.ApplyResources(this.txtFixtureBinding, "txtFixtureBinding");
            this.txtFixtureBinding.Name = "txtFixtureBinding";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // txtProductModel
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.txtProductModel, 3);
            resources.ApplyResources(this.txtProductModel, "txtProductModel");
            this.txtProductModel.Name = "txtProductModel";
            // 
            // label80
            // 
            resources.ApplyResources(this.label80, "label80");
            this.label80.Name = "label80";
            // 
            // textBox42
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.textBox42, 3);
            resources.ApplyResources(this.textBox42, "textBox42");
            this.textBox42.Name = "textBox42";
            // 
            // label44
            // 
            resources.ApplyResources(this.label44, "label44");
            this.label44.BackColor = System.Drawing.Color.Transparent;
            this.label44.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label44.Name = "label44";
            // 
            // lblCurrentUser
            // 
            resources.ApplyResources(this.lblCurrentUser, "lblCurrentUser");
            this.lblCurrentUser.Name = "lblCurrentUser";
            // 
            // label69
            // 
            resources.ApplyResources(this.label69, "label69");
            this.label69.Name = "label69";
            // 
            // lblLoginMode
            // 
            resources.ApplyResources(this.lblLoginMode, "lblLoginMode");
            this.lblLoginMode.BackColor = System.Drawing.Color.Transparent;
            this.lblLoginMode.Name = "lblLoginMode";
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.SetColumnSpan(this.lblCurrentTime, 4);
            resources.ApplyResources(this.lblCurrentTime, "lblCurrentTime");
            this.lblCurrentTime.Name = "lblCurrentTime";
            // 
            // panel22
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel22, 3);
            this.panel22.Controls.Add(this.cboBarcodeRuleAndFixtures);
            this.panel22.Controls.Add(this.lblRecipeId);
            resources.ApplyResources(this.panel22, "panel22");
            this.panel22.Name = "panel22";
            // 
            // cboBarcodeRuleAndFixtures
            // 
            resources.ApplyResources(this.cboBarcodeRuleAndFixtures, "cboBarcodeRuleAndFixtures");
            this.cboBarcodeRuleAndFixtures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBarcodeRuleAndFixtures.FormattingEnabled = true;
            this.cboBarcodeRuleAndFixtures.Name = "cboBarcodeRuleAndFixtures";
            // 
            // lblRecipeId
            // 
            resources.ApplyResources(this.lblRecipeId, "lblRecipeId");
            this.lblRecipeId.Name = "lblRecipeId";
            // 
            // panel23
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel23, 3);
            this.panel23.Controls.Add(this.button25);
            this.panel23.Controls.Add(this.button22);
            this.panel23.Controls.Add(this.chkReadRecipeId_PLC);
            resources.ApplyResources(this.panel23, "panel23");
            this.panel23.Name = "panel23";
            // 
            // button25
            // 
            resources.ApplyResources(this.button25, "button25");
            this.button25.Name = "button25";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.button25_Click_1);
            // 
            // button22
            // 
            resources.ApplyResources(this.button22, "button22");
            this.button22.Name = "button22";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // chkReadRecipeId_PLC
            // 
            resources.ApplyResources(this.chkReadRecipeId_PLC, "chkReadRecipeId_PLC");
            this.chkReadRecipeId_PLC.Name = "chkReadRecipeId_PLC";
            this.chkReadRecipeId_PLC.UseVisualStyleBackColor = true;
            // 
            // label103
            // 
            resources.ApplyResources(this.label103, "label103");
            this.label103.Name = "label103";
            // 
            // label94
            // 
            resources.ApplyResources(this.label94, "label94");
            this.label94.Name = "label94";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.dataGridViewDynamic4);
            this.panel8.Controls.Add(this.panel25);
            this.panel8.Controls.Add(this.dataGridViewDynamic2);
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.Name = "panel8";
            // 
            // dataGridViewDynamic4
            // 
            this.dataGridViewDynamic4.AllowUserToAddRows = false;
            this.dataGridViewDynamic4.AllowUserToDeleteRows = false;
            this.dataGridViewDynamic4.AllowUserToResizeColumns = false;
            this.dataGridViewDynamic4.AllowUserToResizeRows = false;
            this.dataGridViewDynamic4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDynamic4.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridViewDynamic4.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewDynamic4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDynamic4.ColumnKey = null;
            resources.ApplyResources(this.dataGridViewDynamic4, "dataGridViewDynamic4");
            this.dataGridViewDynamic4.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridViewDynamic4.Name = "dataGridViewDynamic4";
            this.dataGridViewDynamic4.RowHeadersVisible = false;
            this.dataGridViewDynamic4.RowTemplate.Height = 23;
            // 
            // panel25
            // 
            this.panel25.Controls.Add(this.panel2);
            this.panel25.Controls.Add(this.panel7);
            resources.ApplyResources(this.panel25, "panel25");
            this.panel25.Name = "panel25";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox11);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.lblVersion);
            this.groupBox11.Controls.Add(this.lblDeviceName);
            resources.ApplyResources(this.groupBox11, "groupBox11");
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.TabStop = false;
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.Name = "lblVersion";
            // 
            // lblDeviceName
            // 
            resources.ApplyResources(this.lblDeviceName, "lblDeviceName");
            this.lblDeviceName.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblDeviceName.Name = "lblDeviceName";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.groupBox10);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label60);
            this.groupBox10.Controls.Add(this.label59);
            this.groupBox10.Controls.Add(this.label106);
            this.groupBox10.Controls.Add(this.label107);
            this.groupBox10.Controls.Add(this.label55);
            this.groupBox10.Controls.Add(this.label108);
            this.groupBox10.Controls.Add(this.lblUploadStatus);
            this.groupBox10.Controls.Add(this.lblValidationStatus);
            this.groupBox10.Controls.Add(this.lblScanBarcodeStatus);
            this.groupBox10.Controls.Add(this.lblPlcStatus);
            this.groupBox10.Controls.Add(this.lblDashboardStatus);
            this.groupBox10.Controls.Add(this.lblDeviceStatus);
            resources.ApplyResources(this.groupBox10, "groupBox10");
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.TabStop = false;
            // 
            // label60
            // 
            resources.ApplyResources(this.label60, "label60");
            this.label60.Name = "label60";
            // 
            // label59
            // 
            resources.ApplyResources(this.label59, "label59");
            this.label59.Name = "label59";
            // 
            // label106
            // 
            resources.ApplyResources(this.label106, "label106");
            this.label106.Name = "label106";
            // 
            // label107
            // 
            resources.ApplyResources(this.label107, "label107");
            this.label107.Name = "label107";
            // 
            // label55
            // 
            resources.ApplyResources(this.label55, "label55");
            this.label55.Name = "label55";
            // 
            // label108
            // 
            resources.ApplyResources(this.label108, "label108");
            this.label108.Name = "label108";
            // 
            // lblUploadStatus
            // 
            resources.ApplyResources(this.lblUploadStatus, "lblUploadStatus");
            this.lblUploadStatus.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblUploadStatus.Name = "lblUploadStatus";
            // 
            // lblValidationStatus
            // 
            resources.ApplyResources(this.lblValidationStatus, "lblValidationStatus");
            this.lblValidationStatus.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblValidationStatus.Name = "lblValidationStatus";
            // 
            // lblScanBarcodeStatus
            // 
            resources.ApplyResources(this.lblScanBarcodeStatus, "lblScanBarcodeStatus");
            this.lblScanBarcodeStatus.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblScanBarcodeStatus.Name = "lblScanBarcodeStatus";
            // 
            // lblPlcStatus
            // 
            resources.ApplyResources(this.lblPlcStatus, "lblPlcStatus");
            this.lblPlcStatus.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblPlcStatus.Name = "lblPlcStatus";
            // 
            // lblDashboardStatus
            // 
            this.lblDashboardStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lblDashboardStatus, "lblDashboardStatus");
            this.lblDashboardStatus.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblDashboardStatus.Name = "lblDashboardStatus";
            // 
            // lblDeviceStatus
            // 
            this.lblDeviceStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lblDeviceStatus, "lblDeviceStatus");
            this.lblDeviceStatus.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblDeviceStatus.Name = "lblDeviceStatus";
            // 
            // dataGridViewDynamic2
            // 
            this.dataGridViewDynamic2.AllowUserToAddRows = false;
            this.dataGridViewDynamic2.AllowUserToDeleteRows = false;
            this.dataGridViewDynamic2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewDynamic2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDynamic2.ColumnKey = null;
            resources.ApplyResources(this.dataGridViewDynamic2, "dataGridViewDynamic2");
            this.dataGridViewDynamic2.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridViewDynamic2.Name = "dataGridViewDynamic2";
            this.dataGridViewDynamic2.RowHeadersVisible = false;
            this.dataGridViewDynamic2.RowTemplate.Height = 23;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.panel20);
            this.tabPage6.Controls.Add(this.panel18);
            this.tabPage6.Controls.Add(this.panel17);
            resources.ApplyResources(this.tabPage6, "tabPage6");
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.groupBox15);
            resources.ApplyResources(this.panel20, "panel20");
            this.panel20.Name = "panel20";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.dgvWeakInfo);
            this.groupBox15.Controls.Add(this.label39);
            resources.ApplyResources(this.groupBox15, "groupBox15");
            this.groupBox15.ForeColor = System.Drawing.Color.Red;
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.TabStop = false;
            // 
            // dgvWeakInfo
            // 
            this.dgvWeakInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvWeakInfo, "dgvWeakInfo");
            this.dgvWeakInfo.Name = "dgvWeakInfo";
            this.dgvWeakInfo.RowHeadersVisible = false;
            this.dgvWeakInfo.RowTemplate.Height = 27;
            this.dgvWeakInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Name = "label39";
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.groupBox19);
            resources.ApplyResources(this.panel18, "panel18");
            this.panel18.Name = "panel18";
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.dgvFaultInfo);
            this.groupBox19.Controls.Add(this.label46);
            resources.ApplyResources(this.groupBox19, "groupBox19");
            this.groupBox19.ForeColor = System.Drawing.Color.Red;
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.TabStop = false;
            // 
            // dgvFaultInfo
            // 
            this.dgvFaultInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvFaultInfo, "dgvFaultInfo");
            this.dgvFaultInfo.Name = "dgvFaultInfo";
            this.dgvFaultInfo.RowHeadersVisible = false;
            this.dgvFaultInfo.RowTemplate.Height = 27;
            this.dgvFaultInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellContentClick);
            // 
            // label46
            // 
            resources.ApplyResources(this.label46, "label46");
            this.label46.ForeColor = System.Drawing.Color.Red;
            this.label46.Name = "label46";
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.groupBox13);
            resources.ApplyResources(this.panel17, "panel17");
            this.panel17.Name = "panel17";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.chkReadPName);
            this.groupBox13.Controls.Add(this.txtProductName);
            this.groupBox13.Controls.Add(this.label93);
            this.groupBox13.Controls.Add(this.btnRefreshAtBulletin);
            this.groupBox13.Controls.Add(this.groupBox25);
            this.groupBox13.Controls.Add(this.label83);
            this.groupBox13.Controls.Add(this.chkEnableDashboard);
            this.groupBox13.Controls.Add(this.btnConnectDashboard);
            this.groupBox13.Controls.Add(this.label40);
            this.groupBox13.Controls.Add(this.btnSaveAtDashboardSetting);
            this.groupBox13.Controls.Add(this.label42);
            this.groupBox13.Controls.Add(this.txtDashboardPort);
            this.groupBox13.Controls.Add(this.txtDashboardIP);
            resources.ApplyResources(this.groupBox13, "groupBox13");
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.TabStop = false;
            // 
            // chkReadPName
            // 
            resources.ApplyResources(this.chkReadPName, "chkReadPName");
            this.chkReadPName.Name = "chkReadPName";
            this.chkReadPName.UseVisualStyleBackColor = true;
            // 
            // txtProductName
            // 
            resources.ApplyResources(this.txtProductName, "txtProductName");
            this.txtProductName.Name = "txtProductName";
            // 
            // label93
            // 
            resources.ApplyResources(this.label93, "label93");
            this.label93.Name = "label93";
            // 
            // btnRefreshAtBulletin
            // 
            resources.ApplyResources(this.btnRefreshAtBulletin, "btnRefreshAtBulletin");
            this.btnRefreshAtBulletin.Name = "btnRefreshAtBulletin";
            this.btnRefreshAtBulletin.UseVisualStyleBackColor = true;
            this.btnRefreshAtBulletin.Click += new System.EventHandler(this.BtnRefreshAtBulletin);
            // 
            // groupBox25
            // 
            this.groupBox25.Controls.Add(this.txtFaultLength);
            this.groupBox25.Controls.Add(this.label101);
            this.groupBox25.Controls.Add(this.txtFaultStartPoint);
            this.groupBox25.Controls.Add(this.label102);
            this.groupBox25.Controls.Add(this.label100);
            this.groupBox25.Controls.Add(this.label95);
            this.groupBox25.Controls.Add(this.label51);
            this.groupBox25.Controls.Add(this.txtStationNameSets);
            resources.ApplyResources(this.groupBox25, "groupBox25");
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.TabStop = false;
            // 
            // txtFaultLength
            // 
            resources.ApplyResources(this.txtFaultLength, "txtFaultLength");
            this.txtFaultLength.Name = "txtFaultLength";
            // 
            // label101
            // 
            resources.ApplyResources(this.label101, "label101");
            this.label101.Name = "label101";
            // 
            // txtFaultStartPoint
            // 
            resources.ApplyResources(this.txtFaultStartPoint, "txtFaultStartPoint");
            this.txtFaultStartPoint.Name = "txtFaultStartPoint";
            // 
            // label102
            // 
            resources.ApplyResources(this.label102, "label102");
            this.label102.Name = "label102";
            // 
            // label100
            // 
            resources.ApplyResources(this.label100, "label100");
            this.label100.Name = "label100";
            // 
            // label95
            // 
            resources.ApplyResources(this.label95, "label95");
            this.label95.ForeColor = System.Drawing.Color.Red;
            this.label95.Name = "label95";
            // 
            // label51
            // 
            resources.ApplyResources(this.label51, "label51");
            this.label51.Name = "label51";
            // 
            // txtStationNameSets
            // 
            resources.ApplyResources(this.txtStationNameSets, "txtStationNameSets");
            this.txtStationNameSets.Name = "txtStationNameSets";
            // 
            // label83
            // 
            resources.ApplyResources(this.label83, "label83");
            this.label83.ForeColor = System.Drawing.Color.Red;
            this.label83.Name = "label83";
            // 
            // chkEnableDashboard
            // 
            resources.ApplyResources(this.chkEnableDashboard, "chkEnableDashboard");
            this.chkEnableDashboard.Name = "chkEnableDashboard";
            this.chkEnableDashboard.UseVisualStyleBackColor = true;
            // 
            // btnConnectDashboard
            // 
            resources.ApplyResources(this.btnConnectDashboard, "btnConnectDashboard");
            this.btnConnectDashboard.Name = "btnConnectDashboard";
            this.btnConnectDashboard.UseVisualStyleBackColor = true;
            this.btnConnectDashboard.Click += new System.EventHandler(this.ConnectDashboard);
            // 
            // label40
            // 
            resources.ApplyResources(this.label40, "label40");
            this.label40.Name = "label40";
            // 
            // btnSaveAtDashboardSetting
            // 
            resources.ApplyResources(this.btnSaveAtDashboardSetting, "btnSaveAtDashboardSetting");
            this.btnSaveAtDashboardSetting.Name = "btnSaveAtDashboardSetting";
            this.btnSaveAtDashboardSetting.UseVisualStyleBackColor = true;
            this.btnSaveAtDashboardSetting.Click += new System.EventHandler(this.BtnSaveDashboardConfig_Click);
            // 
            // label42
            // 
            resources.ApplyResources(this.label42, "label42");
            this.label42.Name = "label42";
            // 
            // txtDashboardPort
            // 
            resources.ApplyResources(this.txtDashboardPort, "txtDashboardPort");
            this.txtDashboardPort.Name = "txtDashboardPort";
            // 
            // txtDashboardIP
            // 
            resources.ApplyResources(this.txtDashboardIP, "txtDashboardIP");
            this.txtDashboardIP.Name = "txtDashboardIP";
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.groupBox30);
            this.tabPage9.Controls.Add(this.label105);
            this.tabPage9.Controls.Add(this.groupBox28);
            resources.ApplyResources(this.tabPage9, "tabPage9");
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // groupBox30
            // 
            this.groupBox30.Controls.Add(this.label72);
            this.groupBox30.Controls.Add(this.btnSaveRecipeConfig);
            this.groupBox30.Controls.Add(this.txtPointSets);
            this.groupBox30.Controls.Add(this.txtNameSets);
            this.groupBox30.Controls.Add(this.label28);
            this.groupBox30.Controls.Add(this.label22);
            resources.ApplyResources(this.groupBox30, "groupBox30");
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.TabStop = false;
            // 
            // label72
            // 
            resources.ApplyResources(this.label72, "label72");
            this.label72.Name = "label72";
            // 
            // btnSaveRecipeConfig
            // 
            resources.ApplyResources(this.btnSaveRecipeConfig, "btnSaveRecipeConfig");
            this.btnSaveRecipeConfig.Name = "btnSaveRecipeConfig";
            this.btnSaveRecipeConfig.UseVisualStyleBackColor = true;
            this.btnSaveRecipeConfig.Click += new System.EventHandler(this.button29_Click);
            // 
            // txtPointSets
            // 
            resources.ApplyResources(this.txtPointSets, "txtPointSets");
            this.txtPointSets.Name = "txtPointSets";
            // 
            // txtNameSets
            // 
            resources.ApplyResources(this.txtNameSets, "txtNameSets");
            this.txtNameSets.Name = "txtNameSets";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // label105
            // 
            resources.ApplyResources(this.label105, "label105");
            this.label105.ForeColor = System.Drawing.Color.Red;
            this.label105.Name = "label105";
            // 
            // groupBox28
            // 
            this.groupBox28.Controls.Add(this.dataGridView5);
            this.groupBox28.Controls.Add(this.label7);
            resources.ApplyResources(this.groupBox28, "groupBox28");
            this.groupBox28.ForeColor = System.Drawing.Color.Red;
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.TabStop = false;
            // 
            // dataGridView5
            // 
            this.dataGridView5.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView5.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView5, "dataGridView5");
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.RowHeadersVisible = false;
            this.dataGridView5.RowTemplate.Height = 27;
            this.dataGridView5.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView5_CellContentClick);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Name = "label7";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Name = "label19";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tabPage4.ResumeLayout(false);
            this.tabControl4.ResumeLayout(false);
            this.tabPage15.ResumeLayout(false);
            this.groupBox40.ResumeLayout(false);
            this.groupBox40.PerformLayout();
            this.tabPage16.ResumeLayout(false);
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.groupBox26.ResumeLayout(false);
            this.groupBox26.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox24.ResumeLayout(false);
            this.groupBox24.PerformLayout();
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.tabPage17.ResumeLayout(false);
            this.groupBox41.ResumeLayout(false);
            this.groupBox41.PerformLayout();
            this.groupBox35.ResumeLayout(false);
            this.groupBox35.PerformLayout();
            this.groupBox36.ResumeLayout(false);
            this.groupBox36.PerformLayout();
            this.groupBox37.ResumeLayout(false);
            this.groupBox38.ResumeLayout(false);
            this.groupBox38.PerformLayout();
            this.groupBox39.ResumeLayout(false);
            this.groupBox39.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox42.ResumeLayout(false);
            this.groupBox42.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage12.PerformLayout();
            this.groupBox32.ResumeLayout(false);
            this.groupBox32.PerformLayout();
            this.groupBox29.ResumeLayout(false);
            this.groupBox29.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage13.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.groupBox27.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.tabPage11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            this.panel16.ResumeLayout(false);
            this.tabPage14.ResumeLayout(false);
            this.panel21.ResumeLayout(false);
            this.groupBox34.ResumeLayout(false);
            this.groupBox34.PerformLayout();
            this.groupBox33.ResumeLayout(false);
            this.groupBox33.PerformLayout();
            this.tabPage18.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDynamic1)).EndInit();
            this.panel12.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.数据源.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.整页面.ResumeLayout(false);
            this.主页上半部分.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.groupBox31.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDynamic3)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.条码数据.ResumeLayout(false);
            this.条码数据.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDynamic4)).EndInit();
            this.panel25.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDynamic2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeakInfo)).EndInit();
            this.panel18.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaultInfo)).EndInit();
            this.panel17.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.tabPage9.PerformLayout();
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            this.groupBox28.ResumeLayout(false);
            this.groupBox28.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSource1;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox UTYPE;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox UPWD;
        private System.Windows.Forms.TextBox UID;
        private System.Windows.Forms.Button btnRefreshUserData;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.TextBox txtDeviceName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblDataPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChangeStoragePath;
        private System.Windows.Forms.Button btnSaveAtSystemSetting;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_nccode;
        private System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.TextBox textBox_opration;
        private System.Windows.Forms.TextBox textBox_site;
        private System.Windows.Forms.TextBox textBox_resource;
        private System.Windows.Forms.TextBox textBox_timeout;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnSaveMesConfig;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RichTextBox rtbProductLog;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.RichTextBox rtbMesLog;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel15;
        private MesDatasCore.DataGridViewDynamic dataGridViewDynamic1;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox_Code;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox 数据源;
        private System.Windows.Forms.TreeView directoryTreeView;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.RichTextBox rtbDashboardLog;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button btnSaveAtDashboardSetting;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Button btnConnectDashboard;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txtDashboardPort;
        private System.Windows.Forms.TextBox txtDashboardIP;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView dgvWeakInfo;
        private System.Windows.Forms.Button btnRefreshAtBulletin;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.DataGridView dgvFaultInfo;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.CheckBox chkEnableDashboard;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.GroupBox groupBox25;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox txtStationNameSets;
        private System.Windows.Forms.CheckBox chkReadPName;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Button btnSearchReaderPort;
        private System.Windows.Forms.ComboBox cmbShowPort;
        private System.Windows.Forms.TextBox tbxReaderDeviceID;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.TextBox txtDisplayWidth;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.CheckBox chkBypassBarcodeValidation;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.TextBox tbxBrandID;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Button btnOpenReader;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TextBox txtFaultLength;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.TextBox txtFaultStartPoint;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.GroupBox groupBox29;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.TextBox textBox36;
        private System.Windows.Forms.TextBox textBox31;
        private System.Windows.Forms.TextBox textBox29;
        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.Button button28;
        private System.Windows.Forms.Button button27;
        private System.Windows.Forms.GroupBox groupBox28;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.CheckBox chkReadBarcodeSecondly;
        private System.Windows.Forms.Panel 整页面;
        private MesDatasCore.DataGridViewDynamic dataGridViewDynamic2;
        private System.Windows.Forms.Panel 主页上半部分;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.CheckBox chkBindWorkOrder;
        private System.Windows.Forms.Button btnChangeWorkOrder;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtWorkOrder;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.TextBox txtFixtureBinding;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtProductModel;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.TextBox textBox42;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label lblLoginMode;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.ComboBox cboBarcodeRuleAndFixtures;
        private System.Windows.Forms.Label lblRecipeId;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.CheckBox chkReadRecipeId_PLC;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label lblDeviceName;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lblProductResult;
        private System.Windows.Forms.GroupBox 条码数据;
        private System.Windows.Forms.TextBox txtShowBarcode;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label lblOperatePrompt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblRunningStatus;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label lblUploadStatus;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label lblDeviceStatus;
        private System.Windows.Forms.Label lblDashboardStatus;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label lblPlcStatus;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label lblScanBarcodeStatus;
        private System.Windows.Forms.Label lblValidationStatus;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel25;
        private System.Windows.Forms.GroupBox groupBox31;
        private System.Windows.Forms.GroupBox groupBox30;
        private System.Windows.Forms.Button btnSaveRecipeConfig;
        private System.Windows.Forms.TextBox txtPointSets;
        private System.Windows.Forms.TextBox txtNameSets;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblVersion;
        private MesDatasCore.DataGridViewDynamic dataGridViewDynamic4;
        private MesDatasCore.DataGridViewDynamic dataGridViewDynamic3;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.CheckBox chkBypassFixtureValidation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.TextBox textBox_user;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.TextBox txtDefaultStyle;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.DataGridView dataGridView6;
        private System.Windows.Forms.GroupBox groupBox27;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.CheckBox chkBypassQRcodeValidation;
        private System.Windows.Forms.CheckBox chkBypassLocalNgHistoricalData;
        private System.Windows.Forms.CheckBox chkBanLocalHistoricalData;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage12;
        private System.Windows.Forms.TabPage tabPage13;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.GroupBox groupBox32;
        private System.Windows.Forms.ComboBox cboConnectType;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TabPage tabPage14;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.GroupBox groupBox34;
        private System.Windows.Forms.TextBox textBox47;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.TextBox txtRecipeIdPoint;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.TextBox textBox43;
        private System.Windows.Forms.TextBox txtProductModelPoint;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.TextBox txtPMLength;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.TextBox txtDeviceStatePoint;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.GroupBox groupBox33;
        private System.Windows.Forms.TextBox txtProductResultPoint;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.TextBox txtEndProductPoint;
        private System.Windows.Forms.TextBox txtSecondProductLength;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.TextBox txtReadSecondly;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.TextBox txtStartPoint;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Button btnSavePlcPoint;
        private System.Windows.Forms.TabControl tabControl4;
        private System.Windows.Forms.TabPage tabPage15;
        private System.Windows.Forms.GroupBox groupBox40;
        private System.Windows.Forms.Button btnSavePrinterConfig;
        private System.Windows.Forms.CheckBox chkPlcControlPrint;
        private System.Windows.Forms.TextBox txtEndPrintPoint;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.TextBox txtStartPrintPoint;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.TabPage tabPage16;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.Button button31;
        private System.Windows.Forms.GroupBox groupBox26;
        private System.Windows.Forms.Button btnShowFilePath;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label lblPrnFilePath_TCP;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Button btnChangePath_TCP;
        private System.Windows.Forms.TextBox txtPrinter_Port;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.TextBox txtPrinter_IP;
        private System.Windows.Forms.Button button30;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.CheckBox chkLoadModel_TCP;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TextBox txtPModel_TCP;
        private System.Windows.Forms.TextBox textBox27;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TextBox textBox26;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.TextBox textBox25;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.ComboBox cmbInstalledPrinters;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TabPage tabPage17;
        private System.Windows.Forms.GroupBox groupBox35;
        private System.Windows.Forms.GroupBox groupBox36;
        private System.Windows.Forms.Label lblPrnFilePath_COM;
        private System.Windows.Forms.Label label122;
        private System.Windows.Forms.Button btnChangePath_COM;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.Button btnPrint_COM;
        private System.Windows.Forms.Button btnSave_COM;
        private System.Windows.Forms.GroupBox groupBox37;
        private System.Windows.Forms.GroupBox groupBox38;
        private System.Windows.Forms.CheckBox chkLoadModel_COM;
        private System.Windows.Forms.CheckBox chkUseFont;
        private System.Windows.Forms.TextBox txtPModel_COM;
        private System.Windows.Forms.TextBox textBox53;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.TextBox textBox54;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.GroupBox groupBox39;
        private System.Windows.Forms.TextBox txtSerialSpan;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label lblCodeContent;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.TextBox txtCodeNumber;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.ComboBox cboPrinterType;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.ComboBox cboPrintFormat_COM;
        private System.Windows.Forms.CheckBox chkAutoAddDate;
        private System.Windows.Forms.GroupBox groupBox41;
        private System.Windows.Forms.Label lblPrintPrompt;
        private System.Windows.Forms.Button btnShowPath_COM;
        private System.Windows.Forms.ComboBox cboPrintMode;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.CheckBox chkPlus2Print;
        private System.Windows.Forms.TabPage tabPage18;
        private System.Windows.Forms.Button button37;
        private System.Windows.Forms.RichTextBox richTextBox6;
        private System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.TextBox txtViewStatus;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.TextBox textBox35;
        private System.Windows.Forms.Label label119;
        private System.Windows.Forms.TextBox textBox34;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.GroupBox groupBox42;
        private System.Windows.Forms.Label lblReaderState;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.Button btnRefreshDirectory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Label lblPlcAccess;
        private System.Windows.Forms.Label label139;
        private System.Windows.Forms.Label lblCurrentSelected;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.Label label120;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrintCount;
        private System.Windows.Forms.Label label19;
    }
}

