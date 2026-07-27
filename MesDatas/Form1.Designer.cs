
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvTest = new MesDatasCore.DataGridViewDynamic();
            this.tabControl_UploadData = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvResult1 = new MesDatasCore.DataGridViewDynamic();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvResult2 = new MesDatasCore.DataGridViewDynamic();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvResult3 = new MesDatasCore.DataGridViewDynamic();
            this.splitContainer_LR = new System.Windows.Forms.SplitContainer();
            this.panel_运行界面左 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dgvShowBarcode = new MesDatasCore.DataGridViewDynamic();
            this.tableLayoutPanel59 = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_Status = new System.Windows.Forms.TableLayoutPanel();
            this.label_Upload = new System.Windows.Forms.Label();
            this.lblPlcStatus = new System.Windows.Forms.Label();
            this.label_Validate = new System.Windows.Forms.Label();
            this.lblDashboardStatus = new System.Windows.Forms.Label();
            this.label_Scan = new System.Windows.Forms.Label();
            this.lblDeviceStatus = new System.Windows.Forms.Label();
            this.label_Device = new System.Windows.Forms.Label();
            this.label_Board = new System.Windows.Forms.Label();
            this.label_PLC = new System.Windows.Forms.Label();
            this.lblScanBarcodeStatus = new System.Windows.Forms.Label();
            this.lblValidationStatus = new System.Windows.Forms.Label();
            this.lblUploadStatus = new System.Windows.Forms.Label();
            this.panelDeviceName = new System.Windows.Forms.Panel();
            this.lblDeviceName = new System.Windows.Forms.Label();
            this.tlp运行界面右 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox生产信息 = new System.Windows.Forms.GroupBox();
            this.dgvProductionIndex = new MesDatasCore.DataGridViewDynamic();
            this.tlp_运行状态 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtShowBarcode = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblOperatePrompt = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lblRunningStatus = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.tlp_ProductResult = new System.Windows.Forms.TableLayoutPanel();
            this.lblProductResult = new System.Windows.Forms.Label();
            this.lbl_Left = new System.Windows.Forms.Label();
            this.lbl_Right = new System.Windows.Forms.Label();
            this.tpl_生产面板 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel41 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLoginMode = new System.Windows.Forms.Label();
            this.cboCurrentLanguage = new System.Windows.Forms.ComboBox();
            this.label69 = new System.Windows.Forms.Label();
            this.tlp_Version = new System.Windows.Forms.TableLayoutPanel();
            this.label_version = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tableLayoutPanel40 = new System.Windows.Forms.TableLayoutPanel();
            this.btnChangeWorkOrder = new System.Windows.Forms.Button();
            this.chkBindOrderNumber = new System.Windows.Forms.CheckBox();
            this.label63 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.tableLayoutPanel39 = new System.Windows.Forms.TableLayoutPanel();
            this.chkReadRecipeId_PLC = new System.Windows.Forms.CheckBox();
            this.button22 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.txtWorkOrder = new System.Windows.Forms.TextBox();
            this.label104 = new System.Windows.Forms.Label();
            this.txtFixtureBinding = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.txtProductModel = new System.Windows.Forms.TextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.panel22 = new System.Windows.Forms.Panel();
            this.cboBarcodeRule = new System.Windows.Forms.ComboBox();
            this.lblRecipeId = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.tabPage打印设置 = new System.Windows.Forms.TabPage();
            this.tabControl4 = new System.Windows.Forms.TabControl();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel60 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox45 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox35 = new System.Windows.Forms.GroupBox();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.btnPrint_ZebraTest = new System.Windows.Forms.Button();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox40 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel51 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel52 = new System.Windows.Forms.TableLayoutPanel();
            this.chkPlcControlPrint = new System.Windows.Forms.CheckBox();
            this.label138 = new System.Windows.Forms.Label();
            this.btnSavePrinterConfig = new System.Windows.Forms.Button();
            this.label137 = new System.Windows.Forms.Label();
            this.btnConnectPrinter = new System.Windows.Forms.Button();
            this.txtStartPoint_print = new System.Windows.Forms.TextBox();
            this.label110 = new System.Windows.Forms.Label();
            this.cboPrintMode = new System.Windows.Forms.ComboBox();
            this.txtPrinter_Port = new System.Windows.Forms.TextBox();
            this.cboPrinterType = new System.Windows.Forms.ComboBox();
            this.label126 = new System.Windows.Forms.Label();
            this.label125 = new System.Windows.Forms.Label();
            this.txtEndPoint_Print = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.txtPrinter_IP = new System.Windows.Forms.TextBox();
            this.label77 = new System.Windows.Forms.Label();
            this.lblConnectStatus = new System.Windows.Forms.Label();
            this.groupBox36 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel56 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel57 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnShowPath = new System.Windows.Forms.Button();
            this.btnChangePath = new System.Windows.Forms.Button();
            this.cboFileFormat = new System.Windows.Forms.ComboBox();
            this.label82 = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.groupBox37 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel50 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox38 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel53 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel54 = new System.Windows.Forms.TableLayoutPanel();
            this.chkLoadModel = new System.Windows.Forms.CheckBox();
            this.chkUseFont = new System.Windows.Forms.CheckBox();
            this.label129 = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.label128 = new System.Windows.Forms.Label();
            this.label127 = new System.Windows.Forms.Label();
            this.txtAfter = new System.Windows.Forms.TextBox();
            this.txtBefore = new System.Windows.Forms.TextBox();
            this.label130 = new System.Windows.Forms.Label();
            this.groupBox39 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel55 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPrintCount = new System.Windows.Forms.TextBox();
            this.label135 = new System.Windows.Forms.Label();
            this.groupBox41 = new System.Windows.Forms.GroupBox();
            this.lblPrintResultTips = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblBarodeContent_Printer = new System.Windows.Forms.Label();
            this.label134 = new System.Windows.Forms.Label();
            this.txtBarcodeNumber_Printer = new System.Windows.Forms.TextBox();
            this.label136 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSN_Printer = new System.Windows.Forms.TextBox();
            this.txtSerialSpan = new System.Windows.Forms.TextBox();
            this.label132 = new System.Windows.Forms.Label();
            this.chkEnableSN = new System.Windows.Forms.CheckBox();
            this.chkAutoAddDate = new System.Windows.Forms.CheckBox();
            this.label47 = new System.Windows.Forms.Label();
            this.tabPage用户管理 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel32 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel34 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox用户信息 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel30 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel35 = new System.Windows.Forms.TableLayoutPanel();
            this.label65 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.label_Reader = new System.Windows.Forms.Label();
            this.lblPlcAccess = new System.Windows.Forms.Label();
            this.cmbShowPort = new System.Windows.Forms.ComboBox();
            this.label91 = new System.Windows.Forms.Label();
            this.groupBox42 = new System.Windows.Forms.GroupBox();
            this.label139 = new System.Windows.Forms.Label();
            this.lblReaderState = new System.Windows.Forms.Label();
            this.btnSearchReaderPort = new System.Windows.Forms.Button();
            this.label_DeviceID = new System.Windows.Forms.Label();
            this.tbxReaderDeviceID = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel31 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOpenReader = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btnRefreshUserData = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.UID = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.UPWD = new System.Windows.Forms.TextBox();
            this.label86 = new System.Windows.Forms.Label();
            this.lblCurrentSelected = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label141 = new System.Windows.Forms.Label();
            this.tbxBrandID = new System.Windows.Forms.TextBox();
            this.UTYPE = new System.Windows.Forms.ComboBox();
            this.label97 = new System.Windows.Forms.Label();
            this.tableLayoutPanel33 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage系统设置 = new System.Windows.Forms.TabPage();
            this.panel19 = new System.Windows.Forms.Panel();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage初始化设置 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox29 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPoint_Write = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel47 = new System.Windows.Forms.TableLayoutPanel();
            this.cboReadValue = new System.Windows.Forms.ComboBox();
            this.btnReadValue = new System.Windows.Forms.Button();
            this.txtValue_Write = new System.Windows.Forms.TextBox();
            this.label99 = new System.Windows.Forms.Label();
            this.txtValue_Read = new System.Windows.Forms.TextBox();
            this.txtPoint_Read = new System.Windows.Forms.TextBox();
            this.label98 = new System.Windows.Forms.Label();
            this.tableLayoutPanel48 = new System.Windows.Forms.TableLayoutPanel();
            this.btnWriteValue = new System.Windows.Forms.Button();
            this.cboWriteValue = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChangeStoragePath = new System.Windows.Forms.Button();
            this.lblDataPath = new System.Windows.Forms.Label();
            this.groupBox32 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.chkBanLocalVerification = new System.Windows.Forms.CheckBox();
            this.chkBanLocalHistoricalData = new System.Windows.Forms.CheckBox();
            this.chkBypassFixtureValidation = new System.Windows.Forms.CheckBox();
            this.chkBanNGDataVerify = new System.Windows.Forms.CheckBox();
            this.chkBanQRcodeValidation = new System.Windows.Forms.CheckBox();
            this.chkBanRuleValidation = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel64 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSaveAtSystemSetting = new System.Windows.Forms.Button();
            this.groupBox44 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel66 = new System.Windows.Forms.TableLayoutPanel();
            this.label78 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel65 = new System.Windows.Forms.TableLayoutPanel();
            this.chkDoubleStation = new System.Windows.Forms.CheckBox();
            this.chkLeftRight = new System.Windows.Forms.CheckBox();
            this.chkGenerateBarcode = new System.Windows.Forms.CheckBox();
            this.chkAutoExit = new System.Windows.Forms.CheckBox();
            this.chkAutoLaunch = new System.Windows.Forms.CheckBox();
            this.chkUserBinding = new System.Windows.Forms.CheckBox();
            this.chkAllowUploadContinuously = new System.Windows.Forms.CheckBox();
            this.chkReadBarcodeSecondly = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.cboConnectType = new System.Windows.Forms.ComboBox();
            this.txt_IP = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label_ChineseName = new System.Windows.Forms.Label();
            this.txtStationCount = new System.Windows.Forms.TextBox();
            this.label_ShowStyle = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label_EnglishName = new System.Windows.Forms.Label();
            this.label_ThaiName = new System.Windows.Forms.Label();
            this.txtDeviceName_Thai = new System.Windows.Forms.TextBox();
            this.txtDeviceName = new System.Windows.Forms.TextBox();
            this.txtDisplayWidth = new System.Windows.Forms.TextBox();
            this.label_DisplayWidth = new System.Windows.Forms.Label();
            this.txtDeviceName_English = new System.Windows.Forms.TextBox();
            this.txtDefaultStyle = new System.Windows.Forms.TextBox();
            this.groupBox43 = new System.Windows.Forms.GroupBox();
            this.label43 = new System.Windows.Forms.Label();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.label41 = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.txtBarcodeNumber = new System.Windows.Forms.TextBox();
            this.lblBarcodeContent = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.dgvPLCPointInfo = new System.Windows.Forms.DataGridView();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.panel16 = new System.Windows.Forms.Panel();
            this.button23 = new System.Windows.Forms.Button();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel45 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel46 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox33 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel49 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel17 = new System.Windows.Forms.TableLayoutPanel();
            this.label87 = new System.Windows.Forms.Label();
            this.txtStartPoint = new System.Windows.Forms.TextBox();
            this.txtEndPoint = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.txtResultPoint = new System.Windows.Forms.TextBox();
            this.label92 = new System.Windows.Forms.Label();
            this.txtSecondPoint = new System.Windows.Forms.TextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.txtSecondLength = new System.Windows.Forms.TextBox();
            this.label89 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel43 = new System.Windows.Forms.TableLayoutPanel();
            this.txtFixtureLength = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.txtFixtutreNumber = new System.Windows.Forms.TextBox();
            this.label71 = new System.Windows.Forms.Label();
            this.txtFixtureOK = new System.Windows.Forms.TextBox();
            this.label68 = new System.Windows.Forms.Label();
            this.txtFixtureValidata = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSecondPoint1 = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.txtResultPoint1 = new System.Windows.Forms.TextBox();
            this.txtSecondPoint2 = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.txtResultPoint2 = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtStartPoint1 = new System.Windows.Forms.TextBox();
            this.txtEndPoint1 = new System.Windows.Forms.TextBox();
            this.txtSecondLength2 = new System.Windows.Forms.TextBox();
            this.txtEndPoint2 = new System.Windows.Forms.TextBox();
            this.txtSecondLength1 = new System.Windows.Forms.TextBox();
            this.txtStartPoint2 = new System.Windows.Forms.TextBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel19 = new System.Windows.Forms.TableLayoutPanel();
            this.txt_LR2 = new System.Windows.Forms.TextBox();
            this.txt_LR1 = new System.Windows.Forms.TextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.tableLayoutPanel44 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox34 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel20 = new System.Windows.Forms.TableLayoutPanel();
            this.label116 = new System.Windows.Forms.Label();
            this.txtDeviceStatePoint = new System.Windows.Forms.TextBox();
            this.txtViewStatus = new System.Windows.Forms.TextBox();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.label113 = new System.Windows.Forms.Label();
            this.textBox47 = new System.Windows.Forms.TextBox();
            this.label123 = new System.Windows.Forms.Label();
            this.txtPMLength = new System.Windows.Forms.TextBox();
            this.txtProductModelPoint = new System.Windows.Forms.TextBox();
            this.label112 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.label117 = new System.Windows.Forms.Label();
            this.textBox43 = new System.Windows.Forms.TextBox();
            this.txtRecipeIdPoint = new System.Windows.Forms.TextBox();
            this.label114 = new System.Windows.Forms.Label();
            this.btnSavePlcPoint = new System.Windows.Forms.Button();
            this.tabPage18 = new System.Windows.Forms.TabPage();
            this.rtbMESOutput = new System.Windows.Forms.RichTextBox();
            this.btnAccessMES = new System.Windows.Forms.Button();
            this.rtbMESInput = new System.Windows.Forms.RichTextBox();
            this.tabPageMES参数 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_nccode = new System.Windows.Forms.TextBox();
            this.txtMES_url = new System.Windows.Forms.TextBox();
            this.txtOperation = new System.Windows.Forms.TextBox();
            this.txtMES_site = new System.Windows.Forms.TextBox();
            this.txtResource = new System.Windows.Forms.TextBox();
            this.txtMES_Timeout = new System.Windows.Forms.TextBox();
            this.txtMES_Port = new System.Windows.Forms.TextBox();
            this.txtMES_IP = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.btnSaveMesConfig = new System.Windows.Forms.Button();
            this.tabPage生产日志 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.rtbMesLog = new System.Windows.Forms.RichTextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.rtbProductLog = new System.Windows.Forms.RichTextBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.rtbDashboardLog = new System.Windows.Forms.RichTextBox();
            this.tabPage历史数据 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel37 = new System.Windows.Forms.TableLayoutPanel();
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.数据源 = new System.Windows.Forms.GroupBox();
            this.directoryTreeView = new System.Windows.Forms.TreeView();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel36 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.textBox_Code = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPage运行界面 = new System.Windows.Forms.TabPage();
            this.Panel运行界面 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage看板设置 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel21 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox看板参数 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel26 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel29 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel62 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel25 = new System.Windows.Forms.TableLayoutPanel();
            this.txtStationNameSets_Thai = new System.Windows.Forms.TextBox();
            this.txtStationNameSets_English = new System.Windows.Forms.TextBox();
            this.txtStationName = new System.Windows.Forms.TextBox();
            this.label93 = new System.Windows.Forms.Label();
            this.txtStationNameSets = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.tableLayoutPanel61 = new System.Windows.Forms.TableLayoutPanel();
            this.tlp启用看板 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel28 = new System.Windows.Forms.TableLayoutPanel();
            this.chkReadPName = new System.Windows.Forms.CheckBox();
            this.label83 = new System.Windows.Forms.Label();
            this.chkEnableDashboard = new System.Windows.Forms.CheckBox();
            this.txtDashboardIP = new System.Windows.Forms.TextBox();
            this.txtDashboardPort = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.btnConnectDashboard = new System.Windows.Forms.Button();
            this.tableLayoutPanel27 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel63 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel24 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel42 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSaveAtDashboardSetting = new System.Windows.Forms.Button();
            this.btnRefreshAtBulletin = new System.Windows.Forms.Button();
            this.label100 = new System.Windows.Forms.Label();
            this.txtFaultStartPoint = new System.Windows.Forms.TextBox();
            this.txtFaultLength = new System.Windows.Forms.TextBox();
            this.label101 = new System.Windows.Forms.Label();
            this.tableLayoutPanel22 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel23 = new System.Windows.Forms.TableLayoutPanel();
            this.txtLineName = new System.Windows.Forms.TextBox();
            this.txtUseTimePoint = new System.Windows.Forms.TextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.BoardCreateDataBaseBtn = new System.Windows.Forms.Button();
            this.panel易损件数据 = new System.Windows.Forms.Panel();
            this.groupBox易损件 = new System.Windows.Forms.GroupBox();
            this.dgvWeakInfo = new System.Windows.Forms.DataGridView();
            this.label39 = new System.Windows.Forms.Label();
            this.panel故障信息 = new System.Windows.Forms.Panel();
            this.groupBox故障信息 = new System.Windows.Forms.GroupBox();
            this.dgvFaultInfo = new System.Windows.Forms.DataGridView();
            this.label46 = new System.Windows.Forms.Label();
            this.tabPage配方设置 = new System.Windows.Forms.TabPage();
            this.tlp_配方设置 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox30 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel38 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSaveRecipeConfig = new System.Windows.Forms.Button();
            this.label72 = new System.Windows.Forms.Label();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.txtNameSets = new System.Windows.Forms.TextBox();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.txtPointSets = new System.Windows.Forms.TextBox();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.txtEnglishNameSets = new System.Windows.Forms.TextBox();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.txtThaiNameSets = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel58 = new System.Windows.Forms.TableLayoutPanel();
            this.label105 = new System.Windows.Forms.Label();
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.dgvRecipeManage = new MesDatasCore.DataGridViewDynamic();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTest)).BeginInit();
            this.tabControl_UploadData.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_LR)).BeginInit();
            this.splitContainer_LR.Panel1.SuspendLayout();
            this.splitContainer_LR.Panel2.SuspendLayout();
            this.splitContainer_LR.SuspendLayout();
            this.panel_运行界面左.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowBarcode)).BeginInit();
            this.tableLayoutPanel59.SuspendLayout();
            this.tlp_Status.SuspendLayout();
            this.panelDeviceName.SuspendLayout();
            this.tlp运行界面右.SuspendLayout();
            this.groupBox生产信息.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionIndex)).BeginInit();
            this.tlp_运行状态.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tlp_ProductResult.SuspendLayout();
            this.tpl_生产面板.SuspendLayout();
            this.tableLayoutPanel41.SuspendLayout();
            this.tlp_Version.SuspendLayout();
            this.tableLayoutPanel40.SuspendLayout();
            this.tableLayoutPanel39.SuspendLayout();
            this.panel22.SuspendLayout();
            this.tabPage打印设置.SuspendLayout();
            this.tabControl4.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel60.SuspendLayout();
            this.groupBox45.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox35.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.groupBox40.SuspendLayout();
            this.tableLayoutPanel51.SuspendLayout();
            this.tableLayoutPanel52.SuspendLayout();
            this.groupBox36.SuspendLayout();
            this.tableLayoutPanel56.SuspendLayout();
            this.tableLayoutPanel57.SuspendLayout();
            this.groupBox37.SuspendLayout();
            this.tableLayoutPanel50.SuspendLayout();
            this.groupBox38.SuspendLayout();
            this.tableLayoutPanel53.SuspendLayout();
            this.tableLayoutPanel54.SuspendLayout();
            this.groupBox39.SuspendLayout();
            this.tableLayoutPanel55.SuspendLayout();
            this.groupBox41.SuspendLayout();
            this.tabPage用户管理.SuspendLayout();
            this.tableLayoutPanel32.SuspendLayout();
            this.tableLayoutPanel34.SuspendLayout();
            this.groupBox用户信息.SuspendLayout();
            this.tableLayoutPanel30.SuspendLayout();
            this.tableLayoutPanel35.SuspendLayout();
            this.groupBox42.SuspendLayout();
            this.tableLayoutPanel31.SuspendLayout();
            this.tableLayoutPanel33.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage系统设置.SuspendLayout();
            this.panel19.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage初始化设置.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.groupBox29.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel47.SuspendLayout();
            this.tableLayoutPanel48.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox32.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel64.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox44.SuspendLayout();
            this.tableLayoutPanel66.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.tableLayoutPanel65.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.groupBox43.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.groupBox27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPLCPointInfo)).BeginInit();
            this.groupBox18.SuspendLayout();
            this.tabPage11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            this.panel16.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.tableLayoutPanel45.SuspendLayout();
            this.tableLayoutPanel46.SuspendLayout();
            this.groupBox33.SuspendLayout();
            this.tableLayoutPanel49.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tableLayoutPanel17.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tableLayoutPanel43.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tableLayoutPanel18.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.tableLayoutPanel19.SuspendLayout();
            this.tableLayoutPanel44.SuspendLayout();
            this.groupBox34.SuspendLayout();
            this.tableLayoutPanel20.SuspendLayout();
            this.tabPage18.SuspendLayout();
            this.tabPageMES参数.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage生产日志.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.tabPage历史数据.SuspendLayout();
            this.tableLayoutPanel37.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDynamic1)).BeginInit();
            this.panel12.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.数据源.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.tableLayoutPanel36.SuspendLayout();
            this.tabPage运行界面.SuspendLayout();
            this.Panel运行界面.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage看板设置.SuspendLayout();
            this.tableLayoutPanel21.SuspendLayout();
            this.groupBox看板参数.SuspendLayout();
            this.tableLayoutPanel26.SuspendLayout();
            this.tableLayoutPanel29.SuspendLayout();
            this.tableLayoutPanel62.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.tableLayoutPanel25.SuspendLayout();
            this.tableLayoutPanel61.SuspendLayout();
            this.tlp启用看板.SuspendLayout();
            this.tableLayoutPanel28.SuspendLayout();
            this.tableLayoutPanel27.SuspendLayout();
            this.tableLayoutPanel63.SuspendLayout();
            this.tableLayoutPanel24.SuspendLayout();
            this.tableLayoutPanel42.SuspendLayout();
            this.tableLayoutPanel22.SuspendLayout();
            this.tableLayoutPanel23.SuspendLayout();
            this.panel易损件数据.SuspendLayout();
            this.groupBox易损件.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeakInfo)).BeginInit();
            this.panel故障信息.SuspendLayout();
            this.groupBox故障信息.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaultInfo)).BeginInit();
            this.tabPage配方设置.SuspendLayout();
            this.tlp_配方设置.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox30.SuspendLayout();
            this.tableLayoutPanel38.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.groupBox24.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel58.SuspendLayout();
            this.groupBox28.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipeManage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.dgvTest);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl_UploadData);
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            // 
            // dgvTest
            // 
            this.dgvTest.AllowUserToAddRows = false;
            this.dgvTest.AllowUserToDeleteRows = false;
            this.dgvTest.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTest.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvTest.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTest.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTest.ColumnKey = null;
            this.dgvTest.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTest.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.dgvTest, "dgvTest");
            this.dgvTest.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvTest.Name = "dgvTest";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTest.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTest.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvTest.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTest.RowTemplate.Height = 23;
            // 
            // tabControl_UploadData
            // 
            this.tabControl_UploadData.Controls.Add(this.tabPage1);
            this.tabControl_UploadData.Controls.Add(this.tabPage2);
            this.tabControl_UploadData.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabControl_UploadData, "tabControl_UploadData");
            this.tabControl_UploadData.Name = "tabControl_UploadData";
            this.tabControl_UploadData.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvResult1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvResult1
            // 
            this.dgvResult1.AllowUserToAddRows = false;
            this.dgvResult1.AllowUserToDeleteRows = false;
            this.dgvResult1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResult1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvResult1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvResult1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult1.ColumnKey = null;
            this.dgvResult1.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResult1.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.dgvResult1, "dgvResult1");
            this.dgvResult1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvResult1.Name = "dgvResult1";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult1.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvResult1.RowHeadersVisible = false;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvResult1.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvResult1.RowTemplate.Height = 23;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvResult2);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvResult2
            // 
            this.dgvResult2.AllowUserToAddRows = false;
            this.dgvResult2.AllowUserToDeleteRows = false;
            this.dgvResult2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResult2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvResult2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvResult2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult2.ColumnKey = null;
            this.dgvResult2.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResult2.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.dgvResult2, "dgvResult2");
            this.dgvResult2.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvResult2.Name = "dgvResult2";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult2.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvResult2.RowHeadersVisible = false;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvResult2.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvResult2.RowTemplate.Height = 23;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvResult3);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvResult3
            // 
            this.dgvResult3.AllowUserToAddRows = false;
            this.dgvResult3.AllowUserToDeleteRows = false;
            this.dgvResult3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResult3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvResult3.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvResult3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult3.ColumnKey = null;
            this.dgvResult3.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResult3.DefaultCellStyle = dataGridViewCellStyle14;
            resources.ApplyResources(this.dgvResult3, "dgvResult3");
            this.dgvResult3.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvResult3.Name = "dgvResult3";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult3.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvResult3.RowHeadersVisible = false;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvResult3.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvResult3.RowTemplate.Height = 23;
            // 
            // splitContainer_LR
            // 
            resources.ApplyResources(this.splitContainer_LR, "splitContainer_LR");
            this.splitContainer_LR.Name = "splitContainer_LR";
            // 
            // splitContainer_LR.Panel1
            // 
            this.splitContainer_LR.Panel1.Controls.Add(this.panel_运行界面左);
            resources.ApplyResources(this.splitContainer_LR.Panel1, "splitContainer_LR.Panel1");
            // 
            // splitContainer_LR.Panel2
            // 
            this.splitContainer_LR.Panel2.Controls.Add(this.tlp运行界面右);
            resources.ApplyResources(this.splitContainer_LR.Panel2, "splitContainer_LR.Panel2");
            // 
            // panel_运行界面左
            // 
            this.panel_运行界面左.Controls.Add(this.panel1);
            this.panel_运行界面左.Controls.Add(this.tableLayoutPanel59);
            resources.ApplyResources(this.panel_运行界面左, "panel_运行界面左");
            this.panel_运行界面左.Name = "panel_运行界面左";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer3);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // splitContainer3
            // 
            resources.ApplyResources(this.splitContainer3, "splitContainer3");
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dgvShowBarcode);
            resources.ApplyResources(this.splitContainer3.Panel1, "splitContainer3.Panel1");
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer1);
            resources.ApplyResources(this.splitContainer3.Panel2, "splitContainer3.Panel2");
            // 
            // dgvShowBarcode
            // 
            this.dgvShowBarcode.AllowUserToAddRows = false;
            this.dgvShowBarcode.AllowUserToDeleteRows = false;
            this.dgvShowBarcode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShowBarcode.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvShowBarcode.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShowBarcode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvShowBarcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowBarcode.ColumnKey = null;
            this.dgvShowBarcode.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvShowBarcode.DefaultCellStyle = dataGridViewCellStyle18;
            resources.ApplyResources(this.dgvShowBarcode, "dgvShowBarcode");
            this.dgvShowBarcode.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvShowBarcode.Name = "dgvShowBarcode";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShowBarcode.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvShowBarcode.RowHeadersVisible = false;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvShowBarcode.RowsDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvShowBarcode.RowTemplate.Height = 23;
            // 
            // tableLayoutPanel59
            // 
            resources.ApplyResources(this.tableLayoutPanel59, "tableLayoutPanel59");
            this.tableLayoutPanel59.Controls.Add(this.tlp_Status, 0, 0);
            this.tableLayoutPanel59.Controls.Add(this.panelDeviceName, 1, 0);
            this.tableLayoutPanel59.Name = "tableLayoutPanel59";
            // 
            // tlp_Status
            // 
            resources.ApplyResources(this.tlp_Status, "tlp_Status");
            this.tlp_Status.Controls.Add(this.label_Upload, 5, 1);
            this.tlp_Status.Controls.Add(this.lblPlcStatus, 0, 0);
            this.tlp_Status.Controls.Add(this.label_Validate, 4, 1);
            this.tlp_Status.Controls.Add(this.lblDashboardStatus, 1, 0);
            this.tlp_Status.Controls.Add(this.label_Scan, 3, 1);
            this.tlp_Status.Controls.Add(this.lblDeviceStatus, 2, 0);
            this.tlp_Status.Controls.Add(this.label_Device, 2, 1);
            this.tlp_Status.Controls.Add(this.label_Board, 1, 1);
            this.tlp_Status.Controls.Add(this.label_PLC, 0, 1);
            this.tlp_Status.Controls.Add(this.lblScanBarcodeStatus, 3, 0);
            this.tlp_Status.Controls.Add(this.lblValidationStatus, 4, 0);
            this.tlp_Status.Controls.Add(this.lblUploadStatus, 5, 0);
            this.tlp_Status.Name = "tlp_Status";
            // 
            // label_Upload
            // 
            resources.ApplyResources(this.label_Upload, "label_Upload");
            this.label_Upload.Name = "label_Upload";
            // 
            // lblPlcStatus
            // 
            resources.ApplyResources(this.lblPlcStatus, "lblPlcStatus");
            this.lblPlcStatus.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblPlcStatus.Name = "lblPlcStatus";
            // 
            // label_Validate
            // 
            resources.ApplyResources(this.label_Validate, "label_Validate");
            this.label_Validate.Name = "label_Validate";
            // 
            // lblDashboardStatus
            // 
            resources.ApplyResources(this.lblDashboardStatus, "lblDashboardStatus");
            this.lblDashboardStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDashboardStatus.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblDashboardStatus.Name = "lblDashboardStatus";
            // 
            // label_Scan
            // 
            resources.ApplyResources(this.label_Scan, "label_Scan");
            this.label_Scan.Name = "label_Scan";
            // 
            // lblDeviceStatus
            // 
            resources.ApplyResources(this.lblDeviceStatus, "lblDeviceStatus");
            this.lblDeviceStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDeviceStatus.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblDeviceStatus.Name = "lblDeviceStatus";
            // 
            // label_Device
            // 
            resources.ApplyResources(this.label_Device, "label_Device");
            this.label_Device.Name = "label_Device";
            // 
            // label_Board
            // 
            resources.ApplyResources(this.label_Board, "label_Board");
            this.label_Board.Name = "label_Board";
            // 
            // label_PLC
            // 
            resources.ApplyResources(this.label_PLC, "label_PLC");
            this.label_PLC.Name = "label_PLC";
            // 
            // lblScanBarcodeStatus
            // 
            resources.ApplyResources(this.lblScanBarcodeStatus, "lblScanBarcodeStatus");
            this.lblScanBarcodeStatus.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblScanBarcodeStatus.Name = "lblScanBarcodeStatus";
            // 
            // lblValidationStatus
            // 
            resources.ApplyResources(this.lblValidationStatus, "lblValidationStatus");
            this.lblValidationStatus.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblValidationStatus.Name = "lblValidationStatus";
            // 
            // lblUploadStatus
            // 
            resources.ApplyResources(this.lblUploadStatus, "lblUploadStatus");
            this.lblUploadStatus.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblUploadStatus.Name = "lblUploadStatus";
            // 
            // panelDeviceName
            // 
            this.panelDeviceName.Controls.Add(this.lblDeviceName);
            resources.ApplyResources(this.panelDeviceName, "panelDeviceName");
            this.panelDeviceName.Name = "panelDeviceName";
            // 
            // lblDeviceName
            // 
            resources.ApplyResources(this.lblDeviceName, "lblDeviceName");
            this.lblDeviceName.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblDeviceName.Name = "lblDeviceName";
            // 
            // tlp运行界面右
            // 
            resources.ApplyResources(this.tlp运行界面右, "tlp运行界面右");
            this.tlp运行界面右.Controls.Add(this.groupBox生产信息, 0, 2);
            this.tlp运行界面右.Controls.Add(this.tlp_运行状态, 0, 1);
            this.tlp运行界面右.Controls.Add(this.tpl_生产面板, 0, 0);
            this.tlp运行界面右.Name = "tlp运行界面右";
            // 
            // groupBox生产信息
            // 
            this.groupBox生产信息.Controls.Add(this.dgvProductionIndex);
            resources.ApplyResources(this.groupBox生产信息, "groupBox生产信息");
            this.groupBox生产信息.Name = "groupBox生产信息";
            this.groupBox生产信息.TabStop = false;
            // 
            // dgvProductionIndex
            // 
            this.dgvProductionIndex.AllowUserToAddRows = false;
            this.dgvProductionIndex.AllowUserToDeleteRows = false;
            this.dgvProductionIndex.AllowUserToResizeColumns = false;
            this.dgvProductionIndex.AllowUserToResizeRows = false;
            this.dgvProductionIndex.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductionIndex.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvProductionIndex.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductionIndex.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
            resources.ApplyResources(this.dgvProductionIndex, "dgvProductionIndex");
            this.dgvProductionIndex.ColumnKey = null;
            this.dgvProductionIndex.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvProductionIndex.Name = "dgvProductionIndex";
            this.dgvProductionIndex.RowHeadersVisible = false;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvProductionIndex.RowsDefaultCellStyle = dataGridViewCellStyle22;
            this.dgvProductionIndex.RowTemplate.Height = 25;
            // 
            // tlp_运行状态
            // 
            resources.ApplyResources(this.tlp_运行状态, "tlp_运行状态");
            this.tlp_运行状态.Controls.Add(this.groupBox1, 0, 0);
            this.tlp_运行状态.Controls.Add(this.groupBox2, 0, 2);
            this.tlp_运行状态.Controls.Add(this.groupBox7, 0, 1);
            this.tlp_运行状态.Controls.Add(this.groupBox8, 0, 3);
            this.tlp_运行状态.Name = "tlp_运行状态";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtShowBarcode);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtShowBarcode
            // 
            resources.ApplyResources(this.txtShowBarcode, "txtShowBarcode");
            this.txtShowBarcode.ForeColor = System.Drawing.Color.Goldenrod;
            this.txtShowBarcode.Name = "txtShowBarcode";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.lblOperatePrompt);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // lblOperatePrompt
            // 
            resources.ApplyResources(this.lblOperatePrompt, "lblOperatePrompt");
            this.lblOperatePrompt.Name = "lblOperatePrompt";
            // 
            // groupBox7
            // 
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Controls.Add(this.lblRunningStatus);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // lblRunningStatus
            // 
            resources.ApplyResources(this.lblRunningStatus, "lblRunningStatus");
            this.lblRunningStatus.Name = "lblRunningStatus";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.tlp_ProductResult);
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            // 
            // tlp_ProductResult
            // 
            resources.ApplyResources(this.tlp_ProductResult, "tlp_ProductResult");
            this.tlp_ProductResult.Controls.Add(this.lblProductResult, 1, 0);
            this.tlp_ProductResult.Controls.Add(this.lbl_Left, 0, 0);
            this.tlp_ProductResult.Controls.Add(this.lbl_Right, 2, 0);
            this.tlp_ProductResult.Name = "tlp_ProductResult";
            // 
            // lblProductResult
            // 
            this.lblProductResult.AutoEllipsis = true;
            resources.ApplyResources(this.lblProductResult, "lblProductResult");
            this.lblProductResult.BackColor = System.Drawing.Color.Transparent;
            this.lblProductResult.Name = "lblProductResult";
            // 
            // lbl_Left
            // 
            resources.ApplyResources(this.lbl_Left, "lbl_Left");
            this.lbl_Left.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Left.Name = "lbl_Left";
            // 
            // lbl_Right
            // 
            this.lbl_Right.AutoEllipsis = true;
            resources.ApplyResources(this.lbl_Right, "lbl_Right");
            this.lbl_Right.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Right.Name = "lbl_Right";
            // 
            // tpl_生产面板
            // 
            resources.ApplyResources(this.tpl_生产面板, "tpl_生产面板");
            this.tpl_生产面板.Controls.Add(this.tableLayoutPanel41, 1, 9);
            this.tpl_生产面板.Controls.Add(this.tlp_Version, 2, 1);
            this.tpl_生产面板.Controls.Add(this.tableLayoutPanel40, 1, 6);
            this.tpl_生产面板.Controls.Add(this.label63, 0, 9);
            this.tpl_生产面板.Controls.Add(this.label73, 0, 6);
            this.tpl_生产面板.Controls.Add(this.tableLayoutPanel39, 1, 8);
            this.tpl_生产面板.Controls.Add(this.label17, 0, 5);
            this.tpl_生产面板.Controls.Add(this.txtWorkOrder, 1, 5);
            this.tpl_生产面板.Controls.Add(this.label104, 0, 4);
            this.tpl_生产面板.Controls.Add(this.txtFixtureBinding, 1, 4);
            this.tpl_生产面板.Controls.Add(this.label32, 0, 3);
            this.tpl_生产面板.Controls.Add(this.txtProductModel, 1, 3);
            this.tpl_生产面板.Controls.Add(this.label80, 0, 2);
            this.tpl_生产面板.Controls.Add(this.txtProductCode, 1, 2);
            this.tpl_生产面板.Controls.Add(this.label44, 0, 1);
            this.tpl_生产面板.Controls.Add(this.lblCurrentUser, 1, 1);
            this.tpl_生产面板.Controls.Add(this.lblCurrentTime, 0, 0);
            this.tpl_生产面板.Controls.Add(this.panel22, 1, 7);
            this.tpl_生产面板.Controls.Add(this.label103, 0, 8);
            this.tpl_生产面板.Controls.Add(this.label94, 0, 7);
            this.tpl_生产面板.Name = "tpl_生产面板";
            // 
            // tableLayoutPanel41
            // 
            resources.ApplyResources(this.tableLayoutPanel41, "tableLayoutPanel41");
            this.tpl_生产面板.SetColumnSpan(this.tableLayoutPanel41, 3);
            this.tableLayoutPanel41.Controls.Add(this.lblLoginMode, 2, 0);
            this.tableLayoutPanel41.Controls.Add(this.cboCurrentLanguage, 0, 0);
            this.tableLayoutPanel41.Controls.Add(this.label69, 1, 0);
            this.tableLayoutPanel41.Name = "tableLayoutPanel41";
            // 
            // lblLoginMode
            // 
            this.lblLoginMode.AllowDrop = true;
            this.lblLoginMode.AutoEllipsis = true;
            resources.ApplyResources(this.lblLoginMode, "lblLoginMode");
            this.lblLoginMode.BackColor = System.Drawing.Color.LightGray;
            this.lblLoginMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLoginMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLoginMode.Name = "lblLoginMode";
            this.lblLoginMode.MouseEnter += new System.EventHandler(this.lblLoginMode_MouseEnter);
            this.lblLoginMode.MouseLeave += new System.EventHandler(this.lblLoginMode_MouseLeave);
            // 
            // cboCurrentLanguage
            // 
            resources.ApplyResources(this.cboCurrentLanguage, "cboCurrentLanguage");
            this.cboCurrentLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurrentLanguage.FormattingEnabled = true;
            this.cboCurrentLanguage.Items.AddRange(new object[] {
            resources.GetString("cboCurrentLanguage.Items"),
            resources.GetString("cboCurrentLanguage.Items1"),
            resources.GetString("cboCurrentLanguage.Items2")});
            this.cboCurrentLanguage.Name = "cboCurrentLanguage";
            this.cboCurrentLanguage.SelectedIndexChanged += new System.EventHandler(this.cboCurrentLanguage_SelectedIndexChanged);
            // 
            // label69
            // 
            resources.ApplyResources(this.label69, "label69");
            this.label69.Name = "label69";
            // 
            // tlp_Version
            // 
            resources.ApplyResources(this.tlp_Version, "tlp_Version");
            this.tpl_生产面板.SetColumnSpan(this.tlp_Version, 2);
            this.tlp_Version.Controls.Add(this.label_version, 0, 0);
            this.tlp_Version.Controls.Add(this.lblVersion, 1, 0);
            this.tlp_Version.Name = "tlp_Version";
            // 
            // label_version
            // 
            resources.ApplyResources(this.label_version, "label_version");
            this.label_version.Name = "label_version";
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.Name = "lblVersion";
            // 
            // tableLayoutPanel40
            // 
            resources.ApplyResources(this.tableLayoutPanel40, "tableLayoutPanel40");
            this.tpl_生产面板.SetColumnSpan(this.tableLayoutPanel40, 3);
            this.tableLayoutPanel40.Controls.Add(this.btnChangeWorkOrder, 1, 0);
            this.tableLayoutPanel40.Controls.Add(this.chkBindOrderNumber, 0, 0);
            this.tableLayoutPanel40.Name = "tableLayoutPanel40";
            // 
            // btnChangeWorkOrder
            // 
            resources.ApplyResources(this.btnChangeWorkOrder, "btnChangeWorkOrder");
            this.btnChangeWorkOrder.Name = "btnChangeWorkOrder";
            this.btnChangeWorkOrder.UseVisualStyleBackColor = true;
            this.btnChangeWorkOrder.Click += new System.EventHandler(this.ChangeWorkOrder_Click);
            // 
            // chkBindOrderNumber
            // 
            resources.ApplyResources(this.chkBindOrderNumber, "chkBindOrderNumber");
            this.chkBindOrderNumber.Name = "chkBindOrderNumber";
            this.chkBindOrderNumber.UseVisualStyleBackColor = true;
            // 
            // label63
            // 
            resources.ApplyResources(this.label63, "label63");
            this.label63.Name = "label63";
            // 
            // label73
            // 
            this.label73.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label73, "label73");
            this.label73.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label73.Name = "label73";
            // 
            // tableLayoutPanel39
            // 
            resources.ApplyResources(this.tableLayoutPanel39, "tableLayoutPanel39");
            this.tpl_生产面板.SetColumnSpan(this.tableLayoutPanel39, 3);
            this.tableLayoutPanel39.Controls.Add(this.chkReadRecipeId_PLC, 0, 0);
            this.tableLayoutPanel39.Controls.Add(this.button22, 1, 0);
            this.tableLayoutPanel39.Controls.Add(this.button25, 2, 0);
            this.tableLayoutPanel39.Name = "tableLayoutPanel39";
            // 
            // chkReadRecipeId_PLC
            // 
            resources.ApplyResources(this.chkReadRecipeId_PLC, "chkReadRecipeId_PLC");
            this.chkReadRecipeId_PLC.Name = "chkReadRecipeId_PLC";
            this.chkReadRecipeId_PLC.UseVisualStyleBackColor = true;
            // 
            // button22
            // 
            resources.ApplyResources(this.button22, "button22");
            this.button22.Name = "button22";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.btnSaveRecipeInfo_Click);
            // 
            // button25
            // 
            resources.ApplyResources(this.button25, "button25");
            this.button25.Name = "button25";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.btnSendRecipeInfo_Click);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // txtWorkOrder
            // 
            this.tpl_生产面板.SetColumnSpan(this.txtWorkOrder, 3);
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
            this.tpl_生产面板.SetColumnSpan(this.txtFixtureBinding, 3);
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
            this.tpl_生产面板.SetColumnSpan(this.txtProductModel, 3);
            resources.ApplyResources(this.txtProductModel, "txtProductModel");
            this.txtProductModel.Name = "txtProductModel";
            // 
            // label80
            // 
            this.label80.AutoEllipsis = true;
            resources.ApplyResources(this.label80, "label80");
            this.label80.Name = "label80";
            // 
            // txtProductCode
            // 
            this.tpl_生产面板.SetColumnSpan(this.txtProductCode, 3);
            resources.ApplyResources(this.txtProductCode, "txtProductCode");
            this.txtProductCode.Name = "txtProductCode";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label44, "label44");
            this.label44.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label44.Name = "label44";
            // 
            // lblCurrentUser
            // 
            resources.ApplyResources(this.lblCurrentUser, "lblCurrentUser");
            this.lblCurrentUser.Name = "lblCurrentUser";
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.BackColor = System.Drawing.Color.Transparent;
            this.tpl_生产面板.SetColumnSpan(this.lblCurrentTime, 4);
            resources.ApplyResources(this.lblCurrentTime, "lblCurrentTime");
            this.lblCurrentTime.Name = "lblCurrentTime";
            // 
            // panel22
            // 
            this.tpl_生产面板.SetColumnSpan(this.panel22, 3);
            this.panel22.Controls.Add(this.cboBarcodeRule);
            this.panel22.Controls.Add(this.lblRecipeId);
            resources.ApplyResources(this.panel22, "panel22");
            this.panel22.Name = "panel22";
            // 
            // cboBarcodeRule
            // 
            resources.ApplyResources(this.cboBarcodeRule, "cboBarcodeRule");
            this.cboBarcodeRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBarcodeRule.FormattingEnabled = true;
            this.cboBarcodeRule.Name = "cboBarcodeRule";
            this.cboBarcodeRule.SelectedIndexChanged += new System.EventHandler(this.cboBarcodeRule_SelectedIndexChanged);
            // 
            // lblRecipeId
            // 
            resources.ApplyResources(this.lblRecipeId, "lblRecipeId");
            this.lblRecipeId.Name = "lblRecipeId";
            this.lblRecipeId.TextChanged += new System.EventHandler(this.lblRecipeId_TextChanged);
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
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinDialogs = false;
            this.skinEngine1.SkinFile = null;
            // 
            // tabPage打印设置
            // 
            this.tabPage打印设置.Controls.Add(this.tabControl4);
            resources.ApplyResources(this.tabPage打印设置, "tabPage打印设置");
            this.tabPage打印设置.Name = "tabPage打印设置";
            this.tabPage打印设置.UseVisualStyleBackColor = true;
            // 
            // tabControl4
            // 
            this.tabControl4.Controls.Add(this.tabPage15);
            resources.ApplyResources(this.tabControl4, "tabControl4");
            this.tabControl4.Name = "tabControl4";
            this.tabControl4.SelectedIndex = 0;
            // 
            // tabPage15
            // 
            this.tabPage15.Controls.Add(this.tableLayoutPanel13);
            resources.ApplyResources(this.tabPage15, "tabPage15");
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel13
            // 
            resources.ApplyResources(this.tableLayoutPanel13, "tableLayoutPanel13");
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel15, 1, 0);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel14, 0, 0);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            // 
            // tableLayoutPanel15
            // 
            resources.ApplyResources(this.tableLayoutPanel15, "tableLayoutPanel15");
            this.tableLayoutPanel15.Controls.Add(this.panel7, 0, 0);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.tableLayoutPanel60);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            this.tableLayoutPanel15.SetRowSpan(this.panel7, 3);
            // 
            // tableLayoutPanel60
            // 
            resources.ApplyResources(this.tableLayoutPanel60, "tableLayoutPanel60");
            this.tableLayoutPanel60.Controls.Add(this.groupBox45, 0, 0);
            this.tableLayoutPanel60.Controls.Add(this.groupBox35, 0, 1);
            this.tableLayoutPanel60.Name = "tableLayoutPanel60";
            // 
            // groupBox45
            // 
            this.groupBox45.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.groupBox45, "groupBox45");
            this.groupBox45.Name = "groupBox45";
            this.groupBox45.TabStop = false;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // groupBox35
            // 
            this.groupBox35.Controls.Add(this.groupBox22);
            this.groupBox35.Controls.Add(this.btnPrint_ZebraTest);
            resources.ApplyResources(this.groupBox35, "groupBox35");
            this.groupBox35.Name = "groupBox35";
            this.groupBox35.TabStop = false;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.richTextBox2);
            resources.ApplyResources(this.groupBox22, "groupBox22");
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.TabStop = false;
            // 
            // richTextBox2
            // 
            resources.ApplyResources(this.richTextBox2, "richTextBox2");
            this.richTextBox2.Name = "richTextBox2";
            // 
            // btnPrint_ZebraTest
            // 
            resources.ApplyResources(this.btnPrint_ZebraTest, "btnPrint_ZebraTest");
            this.btnPrint_ZebraTest.Name = "btnPrint_ZebraTest";
            this.btnPrint_ZebraTest.UseVisualStyleBackColor = true;
            this.btnPrint_ZebraTest.Click += new System.EventHandler(this.btnPrintZebraTest_Click);
            // 
            // tableLayoutPanel14
            // 
            resources.ApplyResources(this.tableLayoutPanel14, "tableLayoutPanel14");
            this.tableLayoutPanel14.Controls.Add(this.groupBox40, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.groupBox36, 0, 2);
            this.tableLayoutPanel14.Controls.Add(this.groupBox37, 0, 1);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            // 
            // groupBox40
            // 
            this.groupBox40.Controls.Add(this.tableLayoutPanel51);
            resources.ApplyResources(this.groupBox40, "groupBox40");
            this.groupBox40.Name = "groupBox40";
            this.groupBox40.TabStop = false;
            // 
            // tableLayoutPanel51
            // 
            resources.ApplyResources(this.tableLayoutPanel51, "tableLayoutPanel51");
            this.tableLayoutPanel51.Controls.Add(this.tableLayoutPanel52, 0, 0);
            this.tableLayoutPanel51.Controls.Add(this.btnSavePrinterConfig, 3, 3);
            this.tableLayoutPanel51.Controls.Add(this.label137, 0, 1);
            this.tableLayoutPanel51.Controls.Add(this.btnConnectPrinter, 2, 3);
            this.tableLayoutPanel51.Controls.Add(this.txtStartPoint_print, 1, 1);
            this.tableLayoutPanel51.Controls.Add(this.label110, 2, 1);
            this.tableLayoutPanel51.Controls.Add(this.cboPrintMode, 3, 1);
            this.tableLayoutPanel51.Controls.Add(this.txtPrinter_Port, 1, 4);
            this.tableLayoutPanel51.Controls.Add(this.cboPrinterType, 3, 2);
            this.tableLayoutPanel51.Controls.Add(this.label126, 0, 2);
            this.tableLayoutPanel51.Controls.Add(this.label125, 2, 2);
            this.tableLayoutPanel51.Controls.Add(this.txtEndPoint_Print, 1, 2);
            this.tableLayoutPanel51.Controls.Add(this.label75, 0, 3);
            this.tableLayoutPanel51.Controls.Add(this.txtPrinter_IP, 1, 3);
            this.tableLayoutPanel51.Controls.Add(this.label77, 0, 4);
            this.tableLayoutPanel51.Controls.Add(this.lblConnectStatus, 3, 0);
            this.tableLayoutPanel51.Name = "tableLayoutPanel51";
            // 
            // tableLayoutPanel52
            // 
            resources.ApplyResources(this.tableLayoutPanel52, "tableLayoutPanel52");
            this.tableLayoutPanel51.SetColumnSpan(this.tableLayoutPanel52, 2);
            this.tableLayoutPanel52.Controls.Add(this.chkPlcControlPrint, 0, 0);
            this.tableLayoutPanel52.Controls.Add(this.label138, 1, 0);
            this.tableLayoutPanel52.Name = "tableLayoutPanel52";
            // 
            // chkPlcControlPrint
            // 
            resources.ApplyResources(this.chkPlcControlPrint, "chkPlcControlPrint");
            this.chkPlcControlPrint.Name = "chkPlcControlPrint";
            this.chkPlcControlPrint.UseVisualStyleBackColor = true;
            // 
            // label138
            // 
            resources.ApplyResources(this.label138, "label138");
            this.label138.ForeColor = System.Drawing.Color.Red;
            this.label138.Name = "label138";
            // 
            // btnSavePrinterConfig
            // 
            resources.ApplyResources(this.btnSavePrinterConfig, "btnSavePrinterConfig");
            this.btnSavePrinterConfig.Name = "btnSavePrinterConfig";
            this.tableLayoutPanel51.SetRowSpan(this.btnSavePrinterConfig, 2);
            this.btnSavePrinterConfig.UseVisualStyleBackColor = true;
            this.btnSavePrinterConfig.Click += new System.EventHandler(this.SavePrinterConfig_Click);
            // 
            // label137
            // 
            resources.ApplyResources(this.label137, "label137");
            this.label137.Name = "label137";
            // 
            // btnConnectPrinter
            // 
            resources.ApplyResources(this.btnConnectPrinter, "btnConnectPrinter");
            this.btnConnectPrinter.Name = "btnConnectPrinter";
            this.tableLayoutPanel51.SetRowSpan(this.btnConnectPrinter, 2);
            this.btnConnectPrinter.UseVisualStyleBackColor = true;
            this.btnConnectPrinter.Click += new System.EventHandler(this.btnConnectPrinter_Click);
            // 
            // txtStartPoint_print
            // 
            resources.ApplyResources(this.txtStartPoint_print, "txtStartPoint_print");
            this.txtStartPoint_print.Name = "txtStartPoint_print";
            // 
            // label110
            // 
            resources.ApplyResources(this.label110, "label110");
            this.label110.Name = "label110";
            // 
            // cboPrintMode
            // 
            resources.ApplyResources(this.cboPrintMode, "cboPrintMode");
            this.cboPrintMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrintMode.FormattingEnabled = true;
            this.cboPrintMode.Items.AddRange(new object[] {
            resources.GetString("cboPrintMode.Items"),
            resources.GetString("cboPrintMode.Items1")});
            this.cboPrintMode.Name = "cboPrintMode";
            // 
            // txtPrinter_Port
            // 
            resources.ApplyResources(this.txtPrinter_Port, "txtPrinter_Port");
            this.txtPrinter_Port.Name = "txtPrinter_Port";
            // 
            // cboPrinterType
            // 
            resources.ApplyResources(this.cboPrinterType, "cboPrinterType");
            this.cboPrinterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrinterType.FormattingEnabled = true;
            this.cboPrinterType.Name = "cboPrinterType";
            // 
            // label126
            // 
            resources.ApplyResources(this.label126, "label126");
            this.label126.Name = "label126";
            // 
            // label125
            // 
            resources.ApplyResources(this.label125, "label125");
            this.label125.Name = "label125";
            // 
            // txtEndPoint_Print
            // 
            resources.ApplyResources(this.txtEndPoint_Print, "txtEndPoint_Print");
            this.txtEndPoint_Print.Name = "txtEndPoint_Print";
            // 
            // label75
            // 
            resources.ApplyResources(this.label75, "label75");
            this.label75.Name = "label75";
            // 
            // txtPrinter_IP
            // 
            resources.ApplyResources(this.txtPrinter_IP, "txtPrinter_IP");
            this.txtPrinter_IP.Name = "txtPrinter_IP";
            // 
            // label77
            // 
            resources.ApplyResources(this.label77, "label77");
            this.label77.Name = "label77";
            // 
            // lblConnectStatus
            // 
            resources.ApplyResources(this.lblConnectStatus, "lblConnectStatus");
            this.lblConnectStatus.ForeColor = System.Drawing.Color.Red;
            this.lblConnectStatus.Name = "lblConnectStatus";
            // 
            // groupBox36
            // 
            this.groupBox36.Controls.Add(this.tableLayoutPanel56);
            resources.ApplyResources(this.groupBox36, "groupBox36");
            this.groupBox36.Name = "groupBox36";
            this.groupBox36.TabStop = false;
            // 
            // tableLayoutPanel56
            // 
            resources.ApplyResources(this.tableLayoutPanel56, "tableLayoutPanel56");
            this.tableLayoutPanel56.Controls.Add(this.tableLayoutPanel57, 0, 2);
            this.tableLayoutPanel56.Controls.Add(this.cboFileFormat, 1, 1);
            this.tableLayoutPanel56.Controls.Add(this.label82, 0, 1);
            this.tableLayoutPanel56.Controls.Add(this.lblFileName, 1, 0);
            this.tableLayoutPanel56.Controls.Add(this.label122, 0, 0);
            this.tableLayoutPanel56.Name = "tableLayoutPanel56";
            // 
            // tableLayoutPanel57
            // 
            resources.ApplyResources(this.tableLayoutPanel57, "tableLayoutPanel57");
            this.tableLayoutPanel56.SetColumnSpan(this.tableLayoutPanel57, 4);
            this.tableLayoutPanel57.Controls.Add(this.btnPrint, 3, 0);
            this.tableLayoutPanel57.Controls.Add(this.btnSave, 2, 0);
            this.tableLayoutPanel57.Controls.Add(this.btnShowPath, 1, 0);
            this.tableLayoutPanel57.Controls.Add(this.btnChangePath, 0, 0);
            this.tableLayoutPanel57.Name = "tableLayoutPanel57";
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnShowPath
            // 
            resources.ApplyResources(this.btnShowPath, "btnShowPath");
            this.btnShowPath.Name = "btnShowPath";
            this.btnShowPath.UseVisualStyleBackColor = true;
            this.btnShowPath.Click += new System.EventHandler(this.btnShowPath_Click);
            // 
            // btnChangePath
            // 
            resources.ApplyResources(this.btnChangePath, "btnChangePath");
            this.btnChangePath.Name = "btnChangePath";
            this.btnChangePath.UseVisualStyleBackColor = true;
            this.btnChangePath.Click += new System.EventHandler(this.btnChangePath_Click);
            // 
            // cboFileFormat
            // 
            this.tableLayoutPanel56.SetColumnSpan(this.cboFileFormat, 3);
            resources.ApplyResources(this.cboFileFormat, "cboFileFormat");
            this.cboFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFileFormat.FormattingEnabled = true;
            this.cboFileFormat.Items.AddRange(new object[] {
            resources.GetString("cboFileFormat.Items"),
            resources.GetString("cboFileFormat.Items1")});
            this.cboFileFormat.Name = "cboFileFormat";
            // 
            // label82
            // 
            resources.ApplyResources(this.label82, "label82");
            this.label82.Name = "label82";
            // 
            // lblFileName
            // 
            resources.ApplyResources(this.lblFileName, "lblFileName");
            this.tableLayoutPanel56.SetColumnSpan(this.lblFileName, 3);
            this.lblFileName.Name = "lblFileName";
            // 
            // label122
            // 
            resources.ApplyResources(this.label122, "label122");
            this.label122.Name = "label122";
            // 
            // groupBox37
            // 
            this.groupBox37.Controls.Add(this.tableLayoutPanel50);
            resources.ApplyResources(this.groupBox37, "groupBox37");
            this.groupBox37.Name = "groupBox37";
            this.groupBox37.TabStop = false;
            // 
            // tableLayoutPanel50
            // 
            resources.ApplyResources(this.tableLayoutPanel50, "tableLayoutPanel50");
            this.tableLayoutPanel50.Controls.Add(this.groupBox38, 0, 0);
            this.tableLayoutPanel50.Controls.Add(this.groupBox39, 0, 1);
            this.tableLayoutPanel50.Name = "tableLayoutPanel50";
            // 
            // groupBox38
            // 
            this.groupBox38.Controls.Add(this.tableLayoutPanel53);
            resources.ApplyResources(this.groupBox38, "groupBox38");
            this.groupBox38.Name = "groupBox38";
            this.groupBox38.TabStop = false;
            // 
            // tableLayoutPanel53
            // 
            resources.ApplyResources(this.tableLayoutPanel53, "tableLayoutPanel53");
            this.tableLayoutPanel53.Controls.Add(this.tableLayoutPanel54, 0, 0);
            this.tableLayoutPanel53.Controls.Add(this.label129, 0, 1);
            this.tableLayoutPanel53.Controls.Add(this.txtModel, 1, 3);
            this.tableLayoutPanel53.Controls.Add(this.label128, 0, 2);
            this.tableLayoutPanel53.Controls.Add(this.label127, 0, 3);
            this.tableLayoutPanel53.Controls.Add(this.txtAfter, 1, 2);
            this.tableLayoutPanel53.Controls.Add(this.txtBefore, 1, 1);
            this.tableLayoutPanel53.Controls.Add(this.label130, 2, 1);
            this.tableLayoutPanel53.Name = "tableLayoutPanel53";
            // 
            // tableLayoutPanel54
            // 
            resources.ApplyResources(this.tableLayoutPanel54, "tableLayoutPanel54");
            this.tableLayoutPanel53.SetColumnSpan(this.tableLayoutPanel54, 3);
            this.tableLayoutPanel54.Controls.Add(this.chkLoadModel, 0, 0);
            this.tableLayoutPanel54.Controls.Add(this.chkUseFont, 1, 0);
            this.tableLayoutPanel54.Name = "tableLayoutPanel54";
            // 
            // chkLoadModel
            // 
            resources.ApplyResources(this.chkLoadModel, "chkLoadModel");
            this.chkLoadModel.Name = "chkLoadModel";
            this.chkLoadModel.UseVisualStyleBackColor = true;
            // 
            // chkUseFont
            // 
            resources.ApplyResources(this.chkUseFont, "chkUseFont");
            this.chkUseFont.Name = "chkUseFont";
            this.chkUseFont.UseVisualStyleBackColor = true;
            // 
            // label129
            // 
            resources.ApplyResources(this.label129, "label129");
            this.label129.Name = "label129";
            // 
            // txtModel
            // 
            resources.ApplyResources(this.txtModel, "txtModel");
            this.txtModel.Name = "txtModel";
            // 
            // label128
            // 
            resources.ApplyResources(this.label128, "label128");
            this.label128.Name = "label128";
            // 
            // label127
            // 
            resources.ApplyResources(this.label127, "label127");
            this.label127.Name = "label127";
            // 
            // txtAfter
            // 
            resources.ApplyResources(this.txtAfter, "txtAfter");
            this.txtAfter.Name = "txtAfter";
            // 
            // txtBefore
            // 
            resources.ApplyResources(this.txtBefore, "txtBefore");
            this.txtBefore.Name = "txtBefore";
            // 
            // label130
            // 
            resources.ApplyResources(this.label130, "label130");
            this.tableLayoutPanel53.SetColumnSpan(this.label130, 2);
            this.label130.ForeColor = System.Drawing.Color.Red;
            this.label130.Name = "label130";
            this.tableLayoutPanel53.SetRowSpan(this.label130, 3);
            // 
            // groupBox39
            // 
            this.groupBox39.Controls.Add(this.tableLayoutPanel55);
            this.groupBox39.Controls.Add(this.label47);
            resources.ApplyResources(this.groupBox39, "groupBox39");
            this.groupBox39.Name = "groupBox39";
            this.groupBox39.TabStop = false;
            // 
            // tableLayoutPanel55
            // 
            resources.ApplyResources(this.tableLayoutPanel55, "tableLayoutPanel55");
            this.tableLayoutPanel55.Controls.Add(this.txtPrintCount, 3, 2);
            this.tableLayoutPanel55.Controls.Add(this.label135, 0, 0);
            this.tableLayoutPanel55.Controls.Add(this.groupBox41, 3, 4);
            this.tableLayoutPanel55.Controls.Add(this.label19, 0, 4);
            this.tableLayoutPanel55.Controls.Add(this.lblBarodeContent_Printer, 1, 3);
            this.tableLayoutPanel55.Controls.Add(this.label134, 0, 3);
            this.tableLayoutPanel55.Controls.Add(this.txtBarcodeNumber_Printer, 1, 0);
            this.tableLayoutPanel55.Controls.Add(this.label136, 0, 1);
            this.tableLayoutPanel55.Controls.Add(this.label2, 2, 2);
            this.tableLayoutPanel55.Controls.Add(this.txtSN_Printer, 1, 1);
            this.tableLayoutPanel55.Controls.Add(this.txtSerialSpan, 1, 2);
            this.tableLayoutPanel55.Controls.Add(this.label132, 0, 2);
            this.tableLayoutPanel55.Controls.Add(this.chkEnableSN, 2, 1);
            this.tableLayoutPanel55.Controls.Add(this.chkAutoAddDate, 2, 0);
            this.tableLayoutPanel55.Name = "tableLayoutPanel55";
            // 
            // txtPrintCount
            // 
            resources.ApplyResources(this.txtPrintCount, "txtPrintCount");
            this.txtPrintCount.Name = "txtPrintCount";
            // 
            // label135
            // 
            resources.ApplyResources(this.label135, "label135");
            this.label135.Name = "label135";
            // 
            // groupBox41
            // 
            this.groupBox41.Controls.Add(this.lblPrintResultTips);
            resources.ApplyResources(this.groupBox41, "groupBox41");
            this.groupBox41.Name = "groupBox41";
            this.groupBox41.TabStop = false;
            // 
            // lblPrintResultTips
            // 
            resources.ApplyResources(this.lblPrintResultTips, "lblPrintResultTips");
            this.lblPrintResultTips.Name = "lblPrintResultTips";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.tableLayoutPanel55.SetColumnSpan(this.label19, 3);
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Name = "label19";
            // 
            // lblBarodeContent_Printer
            // 
            resources.ApplyResources(this.lblBarodeContent_Printer, "lblBarodeContent_Printer");
            this.tableLayoutPanel55.SetColumnSpan(this.lblBarodeContent_Printer, 2);
            this.lblBarodeContent_Printer.Name = "lblBarodeContent_Printer";
            // 
            // label134
            // 
            resources.ApplyResources(this.label134, "label134");
            this.label134.Name = "label134";
            // 
            // txtBarcodeNumber_Printer
            // 
            resources.ApplyResources(this.txtBarcodeNumber_Printer, "txtBarcodeNumber_Printer");
            this.txtBarcodeNumber_Printer.Name = "txtBarcodeNumber_Printer";
            // 
            // label136
            // 
            resources.ApplyResources(this.label136, "label136");
            this.label136.Name = "label136";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtSN_Printer
            // 
            resources.ApplyResources(this.txtSN_Printer, "txtSN_Printer");
            this.txtSN_Printer.Name = "txtSN_Printer";
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
            // chkEnableSN
            // 
            resources.ApplyResources(this.chkEnableSN, "chkEnableSN");
            this.tableLayoutPanel55.SetColumnSpan(this.chkEnableSN, 2);
            this.chkEnableSN.Name = "chkEnableSN";
            this.chkEnableSN.UseVisualStyleBackColor = true;
            // 
            // chkAutoAddDate
            // 
            resources.ApplyResources(this.chkAutoAddDate, "chkAutoAddDate");
            this.tableLayoutPanel55.SetColumnSpan(this.chkAutoAddDate, 2);
            this.chkAutoAddDate.Name = "chkAutoAddDate";
            this.chkAutoAddDate.UseVisualStyleBackColor = true;
            // 
            // label47
            // 
            resources.ApplyResources(this.label47, "label47");
            this.label47.ForeColor = System.Drawing.Color.Red;
            this.label47.Name = "label47";
            // 
            // tabPage用户管理
            // 
            this.tabPage用户管理.Controls.Add(this.tableLayoutPanel32);
            resources.ApplyResources(this.tabPage用户管理, "tabPage用户管理");
            this.tabPage用户管理.Name = "tabPage用户管理";
            this.tabPage用户管理.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel32
            // 
            resources.ApplyResources(this.tableLayoutPanel32, "tableLayoutPanel32");
            this.tableLayoutPanel32.Controls.Add(this.tableLayoutPanel34, 0, 0);
            this.tableLayoutPanel32.Controls.Add(this.tableLayoutPanel33, 0, 1);
            this.tableLayoutPanel32.Name = "tableLayoutPanel32";
            // 
            // tableLayoutPanel34
            // 
            resources.ApplyResources(this.tableLayoutPanel34, "tableLayoutPanel34");
            this.tableLayoutPanel34.Controls.Add(this.groupBox用户信息, 0, 0);
            this.tableLayoutPanel34.Name = "tableLayoutPanel34";
            // 
            // groupBox用户信息
            // 
            this.groupBox用户信息.Controls.Add(this.tableLayoutPanel30);
            resources.ApplyResources(this.groupBox用户信息, "groupBox用户信息");
            this.groupBox用户信息.Name = "groupBox用户信息";
            this.groupBox用户信息.TabStop = false;
            // 
            // tableLayoutPanel30
            // 
            resources.ApplyResources(this.tableLayoutPanel30, "tableLayoutPanel30");
            this.tableLayoutPanel30.Controls.Add(this.tableLayoutPanel35, 4, 0);
            this.tableLayoutPanel30.Controls.Add(this.tableLayoutPanel31, 0, 4);
            this.tableLayoutPanel30.Controls.Add(this.label18, 0, 0);
            this.tableLayoutPanel30.Controls.Add(this.label96, 0, 1);
            this.tableLayoutPanel30.Controls.Add(this.label20, 0, 2);
            this.tableLayoutPanel30.Controls.Add(this.UID, 1, 0);
            this.tableLayoutPanel30.Controls.Add(this.textBox22, 1, 1);
            this.tableLayoutPanel30.Controls.Add(this.UPWD, 1, 2);
            this.tableLayoutPanel30.Controls.Add(this.label86, 2, 0);
            this.tableLayoutPanel30.Controls.Add(this.lblCurrentSelected, 3, 2);
            this.tableLayoutPanel30.Controls.Add(this.label21, 2, 1);
            this.tableLayoutPanel30.Controls.Add(this.label141, 2, 2);
            this.tableLayoutPanel30.Controls.Add(this.tbxBrandID, 3, 0);
            this.tableLayoutPanel30.Controls.Add(this.UTYPE, 3, 1);
            this.tableLayoutPanel30.Controls.Add(this.label97, 0, 3);
            this.tableLayoutPanel30.Name = "tableLayoutPanel30";
            // 
            // tableLayoutPanel35
            // 
            resources.ApplyResources(this.tableLayoutPanel35, "tableLayoutPanel35");
            this.tableLayoutPanel35.Controls.Add(this.label65, 0, 3);
            this.tableLayoutPanel35.Controls.Add(this.label131, 3, 0);
            this.tableLayoutPanel35.Controls.Add(this.label_Reader, 0, 0);
            this.tableLayoutPanel35.Controls.Add(this.lblPlcAccess, 1, 3);
            this.tableLayoutPanel35.Controls.Add(this.cmbShowPort, 1, 0);
            this.tableLayoutPanel35.Controls.Add(this.label91, 2, 1);
            this.tableLayoutPanel35.Controls.Add(this.groupBox42, 0, 2);
            this.tableLayoutPanel35.Controls.Add(this.btnSearchReaderPort, 2, 0);
            this.tableLayoutPanel35.Controls.Add(this.label_DeviceID, 0, 1);
            this.tableLayoutPanel35.Controls.Add(this.tbxReaderDeviceID, 1, 1);
            this.tableLayoutPanel35.Name = "tableLayoutPanel35";
            this.tableLayoutPanel30.SetRowSpan(this.tableLayoutPanel35, 5);
            // 
            // label65
            // 
            resources.ApplyResources(this.label65, "label65");
            this.label65.Name = "label65";
            // 
            // label131
            // 
            resources.ApplyResources(this.label131, "label131");
            this.label131.Name = "label131";
            this.tableLayoutPanel35.SetRowSpan(this.label131, 4);
            // 
            // label_Reader
            // 
            resources.ApplyResources(this.label_Reader, "label_Reader");
            this.label_Reader.Name = "label_Reader";
            // 
            // lblPlcAccess
            // 
            resources.ApplyResources(this.lblPlcAccess, "lblPlcAccess");
            this.lblPlcAccess.Name = "lblPlcAccess";
            // 
            // cmbShowPort
            // 
            resources.ApplyResources(this.cmbShowPort, "cmbShowPort");
            this.cmbShowPort.FormattingEnabled = true;
            this.cmbShowPort.Name = "cmbShowPort";
            // 
            // label91
            // 
            resources.ApplyResources(this.label91, "label91");
            this.label91.ForeColor = System.Drawing.Color.Red;
            this.label91.Name = "label91";
            // 
            // groupBox42
            // 
            this.tableLayoutPanel35.SetColumnSpan(this.groupBox42, 3);
            this.groupBox42.Controls.Add(this.label139);
            this.groupBox42.Controls.Add(this.lblReaderState);
            resources.ApplyResources(this.groupBox42, "groupBox42");
            this.groupBox42.ForeColor = System.Drawing.Color.Red;
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
            // btnSearchReaderPort
            // 
            resources.ApplyResources(this.btnSearchReaderPort, "btnSearchReaderPort");
            this.btnSearchReaderPort.Name = "btnSearchReaderPort";
            this.btnSearchReaderPort.UseVisualStyleBackColor = true;
            // 
            // label_DeviceID
            // 
            resources.ApplyResources(this.label_DeviceID, "label_DeviceID");
            this.label_DeviceID.Name = "label_DeviceID";
            // 
            // tbxReaderDeviceID
            // 
            this.tbxReaderDeviceID.BackColor = System.Drawing.SystemColors.MenuBar;
            resources.ApplyResources(this.tbxReaderDeviceID, "tbxReaderDeviceID");
            this.tbxReaderDeviceID.Name = "tbxReaderDeviceID";
            // 
            // tableLayoutPanel31
            // 
            resources.ApplyResources(this.tableLayoutPanel31, "tableLayoutPanel31");
            this.tableLayoutPanel30.SetColumnSpan(this.tableLayoutPanel31, 4);
            this.tableLayoutPanel31.Controls.Add(this.btnOpenReader, 3, 0);
            this.tableLayoutPanel31.Controls.Add(this.button5, 2, 0);
            this.tableLayoutPanel31.Controls.Add(this.btnRefreshUserData, 1, 0);
            this.tableLayoutPanel31.Controls.Add(this.button7, 0, 0);
            this.tableLayoutPanel31.Name = "tableLayoutPanel31";
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
            // button7
            // 
            this.button7.AutoEllipsis = true;
            resources.ApplyResources(this.button7, "button7");
            this.button7.Name = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button3_Click);
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label96
            // 
            resources.ApplyResources(this.label96, "label96");
            this.label96.Name = "label96";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // UID
            // 
            resources.ApplyResources(this.UID, "UID");
            this.UID.Name = "UID";
            // 
            // textBox22
            // 
            resources.ApplyResources(this.textBox22, "textBox22");
            this.textBox22.Name = "textBox22";
            // 
            // UPWD
            // 
            resources.ApplyResources(this.UPWD, "UPWD");
            this.UPWD.Name = "UPWD";
            // 
            // label86
            // 
            resources.ApplyResources(this.label86, "label86");
            this.label86.Name = "label86";
            // 
            // lblCurrentSelected
            // 
            resources.ApplyResources(this.lblCurrentSelected, "lblCurrentSelected");
            this.lblCurrentSelected.Name = "lblCurrentSelected";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label141
            // 
            resources.ApplyResources(this.label141, "label141");
            this.label141.Name = "label141";
            // 
            // tbxBrandID
            // 
            this.tbxBrandID.BackColor = System.Drawing.SystemColors.ControlLight;
            resources.ApplyResources(this.tbxBrandID, "tbxBrandID");
            this.tbxBrandID.Name = "tbxBrandID";
            // 
            // UTYPE
            // 
            resources.ApplyResources(this.UTYPE, "UTYPE");
            this.UTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UTYPE.FormattingEnabled = true;
            this.UTYPE.Items.AddRange(new object[] {
            resources.GetString("UTYPE.Items"),
            resources.GetString("UTYPE.Items1"),
            resources.GetString("UTYPE.Items2"),
            resources.GetString("UTYPE.Items3"),
            resources.GetString("UTYPE.Items4")});
            this.UTYPE.Name = "UTYPE";
            // 
            // label97
            // 
            resources.ApplyResources(this.label97, "label97");
            this.tableLayoutPanel30.SetColumnSpan(this.label97, 4);
            this.label97.ForeColor = System.Drawing.Color.Tomato;
            this.label97.Name = "label97";
            // 
            // tableLayoutPanel33
            // 
            resources.ApplyResources(this.tableLayoutPanel33, "tableLayoutPanel33");
            this.tableLayoutPanel33.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel33.Name = "tableLayoutPanel33";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle25;
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
            dataGridViewCellStyle24.Format = "*****";
            dataGridViewCellStyle24.NullValue = "#";
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle24;
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
            // tabPage系统设置
            // 
            this.tabPage系统设置.Controls.Add(this.panel19);
            resources.ApplyResources(this.tabPage系统设置, "tabPage系统设置");
            this.tabPage系统设置.Name = "tabPage系统设置";
            this.tabPage系统设置.UseVisualStyleBackColor = true;
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.tabControl3);
            resources.ApplyResources(this.panel19, "panel19");
            this.panel19.Name = "panel19";
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage初始化设置);
            this.tabControl3.Controls.Add(this.tabPage13);
            this.tabControl3.Controls.Add(this.tabPage14);
            this.tabControl3.Controls.Add(this.tabPage18);
            resources.ApplyResources(this.tabControl3, "tabControl3");
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            // 
            // tabPage初始化设置
            // 
            this.tabPage初始化设置.Controls.Add(this.tableLayoutPanel9);
            resources.ApplyResources(this.tabPage初始化设置, "tabPage初始化设置");
            this.tabPage初始化设置.Name = "tabPage初始化设置";
            this.tabPage初始化设置.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            resources.ApplyResources(this.tableLayoutPanel9, "tableLayoutPanel9");
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel12, 2, 0);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel11, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel10, 0, 0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            // 
            // tableLayoutPanel12
            // 
            resources.ApplyResources(this.tableLayoutPanel12, "tableLayoutPanel12");
            this.tableLayoutPanel12.Controls.Add(this.groupBox29, 0, 0);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            // 
            // groupBox29
            // 
            resources.ApplyResources(this.groupBox29, "groupBox29");
            this.groupBox29.Controls.Add(this.tableLayoutPanel8);
            this.groupBox29.Name = "groupBox29";
            this.groupBox29.TabStop = false;
            // 
            // tableLayoutPanel8
            // 
            resources.ApplyResources(this.tableLayoutPanel8, "tableLayoutPanel8");
            this.tableLayoutPanel8.Controls.Add(this.txtPoint_Write, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel47, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.txtValue_Write, 2, 2);
            this.tableLayoutPanel8.Controls.Add(this.label99, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.txtValue_Read, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.txtPoint_Read, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.label98, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel48, 2, 1);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            // 
            // txtPoint_Write
            // 
            resources.ApplyResources(this.txtPoint_Write, "txtPoint_Write");
            this.txtPoint_Write.Name = "txtPoint_Write";
            // 
            // tableLayoutPanel47
            // 
            resources.ApplyResources(this.tableLayoutPanel47, "tableLayoutPanel47");
            this.tableLayoutPanel47.Controls.Add(this.cboReadValue, 0, 0);
            this.tableLayoutPanel47.Controls.Add(this.btnReadValue, 1, 0);
            this.tableLayoutPanel47.Name = "tableLayoutPanel47";
            // 
            // cboReadValue
            // 
            resources.ApplyResources(this.cboReadValue, "cboReadValue");
            this.cboReadValue.FormattingEnabled = true;
            this.cboReadValue.Items.AddRange(new object[] {
            resources.GetString("cboReadValue.Items"),
            resources.GetString("cboReadValue.Items1"),
            resources.GetString("cboReadValue.Items2"),
            resources.GetString("cboReadValue.Items3"),
            resources.GetString("cboReadValue.Items4")});
            this.cboReadValue.Name = "cboReadValue";
            // 
            // btnReadValue
            // 
            resources.ApplyResources(this.btnReadValue, "btnReadValue");
            this.btnReadValue.Name = "btnReadValue";
            this.btnReadValue.UseVisualStyleBackColor = true;
            this.btnReadValue.Click += new System.EventHandler(this.btnReadValue_Click);
            // 
            // txtValue_Write
            // 
            resources.ApplyResources(this.txtValue_Write, "txtValue_Write");
            this.txtValue_Write.Name = "txtValue_Write";
            // 
            // label99
            // 
            resources.ApplyResources(this.label99, "label99");
            this.label99.Name = "label99";
            // 
            // txtValue_Read
            // 
            resources.ApplyResources(this.txtValue_Read, "txtValue_Read");
            this.txtValue_Read.Name = "txtValue_Read";
            // 
            // txtPoint_Read
            // 
            resources.ApplyResources(this.txtPoint_Read, "txtPoint_Read");
            this.txtPoint_Read.Name = "txtPoint_Read";
            // 
            // label98
            // 
            resources.ApplyResources(this.label98, "label98");
            this.label98.Name = "label98";
            // 
            // tableLayoutPanel48
            // 
            resources.ApplyResources(this.tableLayoutPanel48, "tableLayoutPanel48");
            this.tableLayoutPanel48.Controls.Add(this.btnWriteValue, 1, 0);
            this.tableLayoutPanel48.Controls.Add(this.cboWriteValue, 0, 0);
            this.tableLayoutPanel48.Name = "tableLayoutPanel48";
            // 
            // btnWriteValue
            // 
            resources.ApplyResources(this.btnWriteValue, "btnWriteValue");
            this.btnWriteValue.Name = "btnWriteValue";
            this.btnWriteValue.UseVisualStyleBackColor = true;
            this.btnWriteValue.Click += new System.EventHandler(this.btnWriteValue_Click);
            // 
            // cboWriteValue
            // 
            this.cboWriteValue.FormattingEnabled = true;
            this.cboWriteValue.Items.AddRange(new object[] {
            resources.GetString("cboWriteValue.Items"),
            resources.GetString("cboWriteValue.Items1"),
            resources.GetString("cboWriteValue.Items2"),
            resources.GetString("cboWriteValue.Items3"),
            resources.GetString("cboWriteValue.Items4")});
            resources.ApplyResources(this.cboWriteValue, "cboWriteValue");
            this.cboWriteValue.Name = "cboWriteValue";
            // 
            // tableLayoutPanel11
            // 
            resources.ApplyResources(this.tableLayoutPanel11, "tableLayoutPanel11");
            this.tableLayoutPanel11.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.groupBox32, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.tableLayoutPanel64, 0, 2);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel5);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnChangeStoragePath, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.lblDataPath, 1, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
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
            // lblDataPath
            // 
            resources.ApplyResources(this.lblDataPath, "lblDataPath");
            this.lblDataPath.Name = "lblDataPath";
            // 
            // groupBox32
            // 
            this.groupBox32.Controls.Add(this.tableLayoutPanel4);
            resources.ApplyResources(this.groupBox32, "groupBox32");
            this.groupBox32.Name = "groupBox32";
            this.groupBox32.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.chkBanLocalVerification, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.chkBanLocalHistoricalData, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.chkBypassFixtureValidation, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.chkBanNGDataVerify, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.chkBanQRcodeValidation, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.chkBanRuleValidation, 0, 2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // chkBanLocalVerification
            // 
            resources.ApplyResources(this.chkBanLocalVerification, "chkBanLocalVerification");
            this.chkBanLocalVerification.Name = "chkBanLocalVerification";
            this.chkBanLocalVerification.UseVisualStyleBackColor = true;
            // 
            // chkBanLocalHistoricalData
            // 
            resources.ApplyResources(this.chkBanLocalHistoricalData, "chkBanLocalHistoricalData");
            this.chkBanLocalHistoricalData.Name = "chkBanLocalHistoricalData";
            this.chkBanLocalHistoricalData.UseVisualStyleBackColor = true;
            // 
            // chkBypassFixtureValidation
            // 
            resources.ApplyResources(this.chkBypassFixtureValidation, "chkBypassFixtureValidation");
            this.chkBypassFixtureValidation.Name = "chkBypassFixtureValidation";
            this.chkBypassFixtureValidation.UseVisualStyleBackColor = true;
            this.chkBypassFixtureValidation.CheckedChanged += new System.EventHandler(this.chkBypassFixtureValidation_CheckedChanged);
            // 
            // chkBanNGDataVerify
            // 
            resources.ApplyResources(this.chkBanNGDataVerify, "chkBanNGDataVerify");
            this.chkBanNGDataVerify.Name = "chkBanNGDataVerify";
            this.chkBanNGDataVerify.UseVisualStyleBackColor = true;
            // 
            // chkBanQRcodeValidation
            // 
            resources.ApplyResources(this.chkBanQRcodeValidation, "chkBanQRcodeValidation");
            this.chkBanQRcodeValidation.Name = "chkBanQRcodeValidation";
            this.chkBanQRcodeValidation.UseVisualStyleBackColor = false;
            // 
            // chkBanRuleValidation
            // 
            resources.ApplyResources(this.chkBanRuleValidation, "chkBanRuleValidation");
            this.chkBanRuleValidation.Name = "chkBanRuleValidation";
            this.chkBanRuleValidation.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel64
            // 
            resources.ApplyResources(this.tableLayoutPanel64, "tableLayoutPanel64");
            this.tableLayoutPanel64.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel64.Controls.Add(this.groupBox19, 0, 0);
            this.tableLayoutPanel64.Name = "tableLayoutPanel64";
            // 
            // panel2
            // 
            this.tableLayoutPanel64.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.btnSaveAtSystemSetting);
            this.panel2.Controls.Add(this.groupBox44);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // btnSaveAtSystemSetting
            // 
            resources.ApplyResources(this.btnSaveAtSystemSetting, "btnSaveAtSystemSetting");
            this.btnSaveAtSystemSetting.Name = "btnSaveAtSystemSetting";
            this.btnSaveAtSystemSetting.UseVisualStyleBackColor = true;
            this.btnSaveAtSystemSetting.Click += new System.EventHandler(this.BtnSaveAtSystemSetting_Click);
            // 
            // groupBox44
            // 
            this.groupBox44.Controls.Add(this.tableLayoutPanel66);
            resources.ApplyResources(this.groupBox44, "groupBox44");
            this.groupBox44.Name = "groupBox44";
            this.groupBox44.TabStop = false;
            // 
            // tableLayoutPanel66
            // 
            resources.ApplyResources(this.tableLayoutPanel66, "tableLayoutPanel66");
            this.tableLayoutPanel66.Controls.Add(this.label78, 0, 4);
            this.tableLayoutPanel66.Controls.Add(this.label79, 0, 5);
            this.tableLayoutPanel66.Controls.Add(this.label81, 0, 6);
            this.tableLayoutPanel66.Controls.Add(this.label115, 0, 0);
            this.tableLayoutPanel66.Controls.Add(this.label85, 0, 1);
            this.tableLayoutPanel66.Controls.Add(this.label70, 0, 3);
            this.tableLayoutPanel66.Controls.Add(this.label109, 0, 2);
            this.tableLayoutPanel66.Name = "tableLayoutPanel66";
            // 
            // label78
            // 
            resources.ApplyResources(this.label78, "label78");
            this.label78.ForeColor = System.Drawing.Color.Red;
            this.label78.Name = "label78";
            // 
            // label79
            // 
            resources.ApplyResources(this.label79, "label79");
            this.label79.ForeColor = System.Drawing.Color.Red;
            this.label79.Name = "label79";
            // 
            // label81
            // 
            resources.ApplyResources(this.label81, "label81");
            this.label81.ForeColor = System.Drawing.Color.Red;
            this.label81.Name = "label81";
            // 
            // label115
            // 
            resources.ApplyResources(this.label115, "label115");
            this.label115.ForeColor = System.Drawing.Color.Red;
            this.label115.Name = "label115";
            // 
            // label85
            // 
            resources.ApplyResources(this.label85, "label85");
            this.label85.ForeColor = System.Drawing.Color.Red;
            this.label85.Name = "label85";
            // 
            // label70
            // 
            resources.ApplyResources(this.label70, "label70");
            this.label70.ForeColor = System.Drawing.Color.Red;
            this.label70.Name = "label70";
            // 
            // label109
            // 
            resources.ApplyResources(this.label109, "label109");
            this.label109.ForeColor = System.Drawing.Color.Red;
            this.label109.Name = "label109";
            // 
            // groupBox19
            // 
            this.tableLayoutPanel64.SetColumnSpan(this.groupBox19, 2);
            this.groupBox19.Controls.Add(this.tableLayoutPanel65);
            resources.ApplyResources(this.groupBox19, "groupBox19");
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.TabStop = false;
            // 
            // tableLayoutPanel65
            // 
            resources.ApplyResources(this.tableLayoutPanel65, "tableLayoutPanel65");
            this.tableLayoutPanel65.Controls.Add(this.chkDoubleStation, 2, 1);
            this.tableLayoutPanel65.Controls.Add(this.chkLeftRight, 2, 0);
            this.tableLayoutPanel65.Controls.Add(this.chkGenerateBarcode, 1, 2);
            this.tableLayoutPanel65.Controls.Add(this.chkAutoExit, 0, 2);
            this.tableLayoutPanel65.Controls.Add(this.chkAutoLaunch, 1, 1);
            this.tableLayoutPanel65.Controls.Add(this.chkUserBinding, 0, 1);
            this.tableLayoutPanel65.Controls.Add(this.chkAllowUploadContinuously, 1, 0);
            this.tableLayoutPanel65.Controls.Add(this.chkReadBarcodeSecondly, 0, 0);
            this.tableLayoutPanel65.Name = "tableLayoutPanel65";
            // 
            // chkDoubleStation
            // 
            resources.ApplyResources(this.chkDoubleStation, "chkDoubleStation");
            this.chkDoubleStation.Checked = true;
            this.chkDoubleStation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDoubleStation.Name = "chkDoubleStation";
            this.chkDoubleStation.UseVisualStyleBackColor = true;
            // 
            // chkLeftRight
            // 
            resources.ApplyResources(this.chkLeftRight, "chkLeftRight");
            this.chkLeftRight.Checked = true;
            this.chkLeftRight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLeftRight.Name = "chkLeftRight";
            this.chkLeftRight.UseVisualStyleBackColor = true;
            // 
            // chkGenerateBarcode
            // 
            resources.ApplyResources(this.chkGenerateBarcode, "chkGenerateBarcode");
            this.chkGenerateBarcode.Name = "chkGenerateBarcode";
            this.chkGenerateBarcode.UseVisualStyleBackColor = true;
            // 
            // chkAutoExit
            // 
            resources.ApplyResources(this.chkAutoExit, "chkAutoExit");
            this.chkAutoExit.Checked = true;
            this.chkAutoExit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoExit.Name = "chkAutoExit";
            this.chkAutoExit.UseVisualStyleBackColor = true;
            // 
            // chkAutoLaunch
            // 
            resources.ApplyResources(this.chkAutoLaunch, "chkAutoLaunch");
            this.chkAutoLaunch.Checked = true;
            this.chkAutoLaunch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoLaunch.Name = "chkAutoLaunch";
            this.chkAutoLaunch.UseVisualStyleBackColor = true;
            // 
            // chkUserBinding
            // 
            resources.ApplyResources(this.chkUserBinding, "chkUserBinding");
            this.chkUserBinding.Checked = true;
            this.chkUserBinding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUserBinding.Name = "chkUserBinding";
            this.chkUserBinding.UseVisualStyleBackColor = true;
            // 
            // chkAllowUploadContinuously
            // 
            resources.ApplyResources(this.chkAllowUploadContinuously, "chkAllowUploadContinuously");
            this.chkAllowUploadContinuously.Name = "chkAllowUploadContinuously";
            this.chkAllowUploadContinuously.UseVisualStyleBackColor = true;
            // 
            // chkReadBarcodeSecondly
            // 
            resources.ApplyResources(this.chkReadBarcodeSecondly, "chkReadBarcodeSecondly");
            this.chkReadBarcodeSecondly.Name = "chkReadBarcodeSecondly";
            this.chkReadBarcodeSecondly.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel10
            // 
            resources.ApplyResources(this.tableLayoutPanel10, "tableLayoutPanel10");
            this.tableLayoutPanel10.Controls.Add(this.groupBox5, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.groupBox17, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.groupBox43, 0, 2);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel6);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // tableLayoutPanel6
            // 
            resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
            this.tableLayoutPanel6.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label30, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.cboConnectType, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.txt_IP, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.label16, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.txt_port, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.button3, 2, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // cboConnectType
            // 
            resources.ApplyResources(this.cboConnectType, "cboConnectType");
            this.cboConnectType.FormattingEnabled = true;
            this.cboConnectType.Items.AddRange(new object[] {
            resources.GetString("cboConnectType.Items"),
            resources.GetString("cboConnectType.Items1"),
            resources.GetString("cboConnectType.Items2"),
            resources.GetString("cboConnectType.Items3")});
            this.cboConnectType.Name = "cboConnectType";
            // 
            // txt_IP
            // 
            resources.ApplyResources(this.txt_IP, "txt_IP");
            this.txt_IP.Name = "txt_IP";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // txt_port
            // 
            resources.ApplyResources(this.txt_port, "txt_port");
            this.txt_port.Name = "txt_port";
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.tableLayoutPanel6.SetRowSpan(this.button3, 3);
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.BtnConnectPlc_Click);
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.tableLayoutPanel7);
            resources.ApplyResources(this.groupBox17, "groupBox17");
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.TabStop = false;
            // 
            // tableLayoutPanel7
            // 
            resources.ApplyResources(this.tableLayoutPanel7, "tableLayoutPanel7");
            this.tableLayoutPanel7.Controls.Add(this.label_ChineseName, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.txtStationCount, 1, 4);
            this.tableLayoutPanel7.Controls.Add(this.label_ShowStyle, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.label22, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this.label_EnglishName, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.label_ThaiName, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.txtDeviceName_Thai, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.txtDeviceName, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.txtDisplayWidth, 1, 5);
            this.tableLayoutPanel7.Controls.Add(this.label_DisplayWidth, 0, 5);
            this.tableLayoutPanel7.Controls.Add(this.txtDeviceName_English, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.txtDefaultStyle, 1, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            // 
            // label_ChineseName
            // 
            resources.ApplyResources(this.label_ChineseName, "label_ChineseName");
            this.label_ChineseName.Name = "label_ChineseName";
            // 
            // txtStationCount
            // 
            resources.ApplyResources(this.txtStationCount, "txtStationCount");
            this.txtStationCount.Name = "txtStationCount";
            // 
            // label_ShowStyle
            // 
            resources.ApplyResources(this.label_ShowStyle, "label_ShowStyle");
            this.label_ShowStyle.Name = "label_ShowStyle";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // label_EnglishName
            // 
            resources.ApplyResources(this.label_EnglishName, "label_EnglishName");
            this.label_EnglishName.Name = "label_EnglishName";
            // 
            // label_ThaiName
            // 
            resources.ApplyResources(this.label_ThaiName, "label_ThaiName");
            this.label_ThaiName.Name = "label_ThaiName";
            // 
            // txtDeviceName_Thai
            // 
            resources.ApplyResources(this.txtDeviceName_Thai, "txtDeviceName_Thai");
            this.txtDeviceName_Thai.Name = "txtDeviceName_Thai";
            // 
            // txtDeviceName
            // 
            resources.ApplyResources(this.txtDeviceName, "txtDeviceName");
            this.txtDeviceName.Name = "txtDeviceName";
            // 
            // txtDisplayWidth
            // 
            resources.ApplyResources(this.txtDisplayWidth, "txtDisplayWidth");
            this.txtDisplayWidth.Name = "txtDisplayWidth";
            // 
            // label_DisplayWidth
            // 
            resources.ApplyResources(this.label_DisplayWidth, "label_DisplayWidth");
            this.label_DisplayWidth.Name = "label_DisplayWidth";
            // 
            // txtDeviceName_English
            // 
            resources.ApplyResources(this.txtDeviceName_English, "txtDeviceName_English");
            this.txtDeviceName_English.Name = "txtDeviceName_English";
            // 
            // txtDefaultStyle
            // 
            resources.ApplyResources(this.txtDefaultStyle, "txtDefaultStyle");
            this.txtDefaultStyle.Name = "txtDefaultStyle";
            // 
            // groupBox43
            // 
            this.groupBox43.Controls.Add(this.label43);
            this.groupBox43.Controls.Add(this.tableLayoutPanel16);
            this.groupBox43.Controls.Add(this.label38);
            this.groupBox43.Controls.Add(this.label48);
            resources.ApplyResources(this.groupBox43, "groupBox43");
            this.groupBox43.Name = "groupBox43";
            this.groupBox43.TabStop = false;
            // 
            // label43
            // 
            resources.ApplyResources(this.label43, "label43");
            this.label43.ForeColor = System.Drawing.Color.Red;
            this.label43.Name = "label43";
            // 
            // tableLayoutPanel16
            // 
            resources.ApplyResources(this.tableLayoutPanel16, "tableLayoutPanel16");
            this.tableLayoutPanel16.Controls.Add(this.label41, 0, 0);
            this.tableLayoutPanel16.Controls.Add(this.txtSN, 1, 1);
            this.tableLayoutPanel16.Controls.Add(this.txtBarcodeNumber, 1, 0);
            this.tableLayoutPanel16.Controls.Add(this.lblBarcodeContent, 1, 2);
            this.tableLayoutPanel16.Controls.Add(this.label52, 0, 1);
            this.tableLayoutPanel16.Controls.Add(this.label50, 0, 2);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            // 
            // label41
            // 
            resources.ApplyResources(this.label41, "label41");
            this.label41.Name = "label41";
            // 
            // txtSN
            // 
            resources.ApplyResources(this.txtSN, "txtSN");
            this.txtSN.Name = "txtSN";
            // 
            // txtBarcodeNumber
            // 
            resources.ApplyResources(this.txtBarcodeNumber, "txtBarcodeNumber");
            this.txtBarcodeNumber.Name = "txtBarcodeNumber";
            // 
            // lblBarcodeContent
            // 
            resources.ApplyResources(this.lblBarcodeContent, "lblBarcodeContent");
            this.lblBarcodeContent.Name = "lblBarcodeContent";
            // 
            // label52
            // 
            resources.ApplyResources(this.label52, "label52");
            this.label52.Name = "label52";
            // 
            // label50
            // 
            resources.ApplyResources(this.label50, "label50");
            this.label50.Name = "label50";
            // 
            // label38
            // 
            resources.ApplyResources(this.label38, "label38");
            this.label38.ForeColor = System.Drawing.Color.Red;
            this.label38.Name = "label38";
            // 
            // label48
            // 
            resources.ApplyResources(this.label48, "label48");
            this.label48.ForeColor = System.Drawing.Color.Red;
            this.label48.Name = "label48";
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
            this.groupBox27.Controls.Add(this.dgvPLCPointInfo);
            resources.ApplyResources(this.groupBox27, "groupBox27");
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.TabStop = false;
            // 
            // dgvPLCPointInfo
            // 
            this.dgvPLCPointInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPLCPointInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPLCPointInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPLCPointInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle26;
            this.dgvPLCPointInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPLCPointInfo.DefaultCellStyle = dataGridViewCellStyle27;
            resources.ApplyResources(this.dgvPLCPointInfo, "dgvPLCPointInfo");
            this.dgvPLCPointInfo.Name = "dgvPLCPointInfo";
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPLCPointInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.dgvPLCPointInfo.RowHeadersVisible = false;
            this.dgvPLCPointInfo.RowTemplate.Height = 27;
            this.dgvPLCPointInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView4_CellContentClick);
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
            this.dataGridView6.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView6.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle29.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView6.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle29;
            this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle30.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView6.DefaultCellStyle = dataGridViewCellStyle30;
            resources.ApplyResources(this.dataGridView6, "dataGridView6");
            this.dataGridView6.Name = "dataGridView6";
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView6.RowHeadersDefaultCellStyle = dataGridViewCellStyle31;
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView6.RowsDefaultCellStyle = dataGridViewCellStyle32;
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
            this.button23.Click += new System.EventHandler(this.btnRefreshTable_Click);
            // 
            // tabPage14
            // 
            this.tabPage14.Controls.Add(this.tableLayoutPanel45);
            resources.ApplyResources(this.tabPage14, "tabPage14");
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel45
            // 
            resources.ApplyResources(this.tableLayoutPanel45, "tableLayoutPanel45");
            this.tableLayoutPanel45.Controls.Add(this.tableLayoutPanel46, 0, 0);
            this.tableLayoutPanel45.Controls.Add(this.tableLayoutPanel44, 0, 1);
            this.tableLayoutPanel45.Name = "tableLayoutPanel45";
            // 
            // tableLayoutPanel46
            // 
            resources.ApplyResources(this.tableLayoutPanel46, "tableLayoutPanel46");
            this.tableLayoutPanel46.Controls.Add(this.groupBox33, 0, 0);
            this.tableLayoutPanel46.Name = "tableLayoutPanel46";
            // 
            // groupBox33
            // 
            this.tableLayoutPanel46.SetColumnSpan(this.groupBox33, 2);
            this.groupBox33.Controls.Add(this.tableLayoutPanel49);
            resources.ApplyResources(this.groupBox33, "groupBox33");
            this.groupBox33.Name = "groupBox33";
            this.groupBox33.TabStop = false;
            // 
            // tableLayoutPanel49
            // 
            resources.ApplyResources(this.tableLayoutPanel49, "tableLayoutPanel49");
            this.tableLayoutPanel49.Controls.Add(this.groupBox10, 0, 0);
            this.tableLayoutPanel49.Controls.Add(this.groupBox6, 3, 0);
            this.tableLayoutPanel49.Controls.Add(this.groupBox11, 1, 0);
            this.tableLayoutPanel49.Controls.Add(this.groupBox13, 2, 0);
            this.tableLayoutPanel49.Name = "tableLayoutPanel49";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.tableLayoutPanel17);
            resources.ApplyResources(this.groupBox10, "groupBox10");
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.TabStop = false;
            // 
            // tableLayoutPanel17
            // 
            resources.ApplyResources(this.tableLayoutPanel17, "tableLayoutPanel17");
            this.tableLayoutPanel17.Controls.Add(this.label87, 0, 0);
            this.tableLayoutPanel17.Controls.Add(this.txtStartPoint, 1, 0);
            this.tableLayoutPanel17.Controls.Add(this.txtEndPoint, 1, 1);
            this.tableLayoutPanel17.Controls.Add(this.label90, 0, 1);
            this.tableLayoutPanel17.Controls.Add(this.txtResultPoint, 1, 2);
            this.tableLayoutPanel17.Controls.Add(this.label92, 0, 2);
            this.tableLayoutPanel17.Controls.Add(this.txtSecondPoint, 1, 3);
            this.tableLayoutPanel17.Controls.Add(this.label88, 0, 3);
            this.tableLayoutPanel17.Controls.Add(this.txtSecondLength, 1, 4);
            this.tableLayoutPanel17.Controls.Add(this.label89, 0, 4);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            // 
            // label87
            // 
            resources.ApplyResources(this.label87, "label87");
            this.label87.Name = "label87";
            // 
            // txtStartPoint
            // 
            resources.ApplyResources(this.txtStartPoint, "txtStartPoint");
            this.txtStartPoint.Name = "txtStartPoint";
            // 
            // txtEndPoint
            // 
            resources.ApplyResources(this.txtEndPoint, "txtEndPoint");
            this.txtEndPoint.Name = "txtEndPoint";
            // 
            // label90
            // 
            resources.ApplyResources(this.label90, "label90");
            this.label90.Name = "label90";
            // 
            // txtResultPoint
            // 
            resources.ApplyResources(this.txtResultPoint, "txtResultPoint");
            this.txtResultPoint.Name = "txtResultPoint";
            // 
            // label92
            // 
            resources.ApplyResources(this.label92, "label92");
            this.label92.Name = "label92";
            // 
            // txtSecondPoint
            // 
            resources.ApplyResources(this.txtSecondPoint, "txtSecondPoint");
            this.txtSecondPoint.Name = "txtSecondPoint";
            // 
            // label88
            // 
            resources.ApplyResources(this.label88, "label88");
            this.label88.Name = "label88";
            // 
            // txtSecondLength
            // 
            resources.ApplyResources(this.txtSecondLength, "txtSecondLength");
            this.txtSecondLength.Name = "txtSecondLength";
            // 
            // label89
            // 
            resources.ApplyResources(this.label89, "label89");
            this.label89.Name = "label89";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tableLayoutPanel43);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // tableLayoutPanel43
            // 
            resources.ApplyResources(this.tableLayoutPanel43, "tableLayoutPanel43");
            this.tableLayoutPanel43.Controls.Add(this.txtFixtureLength, 1, 3);
            this.tableLayoutPanel43.Controls.Add(this.label76, 0, 3);
            this.tableLayoutPanel43.Controls.Add(this.txtFixtutreNumber, 1, 2);
            this.tableLayoutPanel43.Controls.Add(this.label71, 0, 2);
            this.tableLayoutPanel43.Controls.Add(this.txtFixtureOK, 1, 1);
            this.tableLayoutPanel43.Controls.Add(this.label68, 0, 1);
            this.tableLayoutPanel43.Controls.Add(this.txtFixtureValidata, 1, 0);
            this.tableLayoutPanel43.Controls.Add(this.label67, 0, 0);
            this.tableLayoutPanel43.Name = "tableLayoutPanel43";
            // 
            // txtFixtureLength
            // 
            resources.ApplyResources(this.txtFixtureLength, "txtFixtureLength");
            this.txtFixtureLength.Name = "txtFixtureLength";
            // 
            // label76
            // 
            resources.ApplyResources(this.label76, "label76");
            this.label76.Name = "label76";
            // 
            // txtFixtutreNumber
            // 
            resources.ApplyResources(this.txtFixtutreNumber, "txtFixtutreNumber");
            this.txtFixtutreNumber.Name = "txtFixtutreNumber";
            // 
            // label71
            // 
            resources.ApplyResources(this.label71, "label71");
            this.label71.Name = "label71";
            // 
            // txtFixtureOK
            // 
            resources.ApplyResources(this.txtFixtureOK, "txtFixtureOK");
            this.txtFixtureOK.Name = "txtFixtureOK";
            // 
            // label68
            // 
            resources.ApplyResources(this.label68, "label68");
            this.label68.Name = "label68";
            // 
            // txtFixtureValidata
            // 
            resources.ApplyResources(this.txtFixtureValidata, "txtFixtureValidata");
            this.txtFixtureValidata.Name = "txtFixtureValidata";
            // 
            // label67
            // 
            resources.ApplyResources(this.label67, "label67");
            this.label67.Name = "label67";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.tableLayoutPanel18);
            resources.ApplyResources(this.groupBox11, "groupBox11");
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.TabStop = false;
            // 
            // tableLayoutPanel18
            // 
            resources.ApplyResources(this.tableLayoutPanel18, "tableLayoutPanel18");
            this.tableLayoutPanel18.Controls.Add(this.txtSecondPoint1, 1, 3);
            this.tableLayoutPanel18.Controls.Add(this.label56, 0, 0);
            this.tableLayoutPanel18.Controls.Add(this.label57, 0, 1);
            this.tableLayoutPanel18.Controls.Add(this.label64, 2, 4);
            this.tableLayoutPanel18.Controls.Add(this.txtResultPoint1, 1, 2);
            this.tableLayoutPanel18.Controls.Add(this.txtSecondPoint2, 3, 3);
            this.tableLayoutPanel18.Controls.Add(this.label59, 2, 3);
            this.tableLayoutPanel18.Controls.Add(this.label49, 2, 2);
            this.tableLayoutPanel18.Controls.Add(this.txtResultPoint2, 3, 2);
            this.tableLayoutPanel18.Controls.Add(this.label58, 0, 2);
            this.tableLayoutPanel18.Controls.Add(this.label60, 0, 3);
            this.tableLayoutPanel18.Controls.Add(this.label45, 2, 1);
            this.tableLayoutPanel18.Controls.Add(this.label62, 0, 4);
            this.tableLayoutPanel18.Controls.Add(this.label28, 2, 0);
            this.tableLayoutPanel18.Controls.Add(this.txtStartPoint1, 1, 0);
            this.tableLayoutPanel18.Controls.Add(this.txtEndPoint1, 1, 1);
            this.tableLayoutPanel18.Controls.Add(this.txtSecondLength2, 3, 4);
            this.tableLayoutPanel18.Controls.Add(this.txtEndPoint2, 3, 1);
            this.tableLayoutPanel18.Controls.Add(this.txtSecondLength1, 1, 4);
            this.tableLayoutPanel18.Controls.Add(this.txtStartPoint2, 3, 0);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            // 
            // txtSecondPoint1
            // 
            resources.ApplyResources(this.txtSecondPoint1, "txtSecondPoint1");
            this.txtSecondPoint1.Name = "txtSecondPoint1";
            // 
            // label56
            // 
            resources.ApplyResources(this.label56, "label56");
            this.label56.Name = "label56";
            // 
            // label57
            // 
            resources.ApplyResources(this.label57, "label57");
            this.label57.Name = "label57";
            // 
            // label64
            // 
            resources.ApplyResources(this.label64, "label64");
            this.label64.Name = "label64";
            // 
            // txtResultPoint1
            // 
            resources.ApplyResources(this.txtResultPoint1, "txtResultPoint1");
            this.txtResultPoint1.Name = "txtResultPoint1";
            // 
            // txtSecondPoint2
            // 
            resources.ApplyResources(this.txtSecondPoint2, "txtSecondPoint2");
            this.txtSecondPoint2.Name = "txtSecondPoint2";
            // 
            // label59
            // 
            resources.ApplyResources(this.label59, "label59");
            this.label59.Name = "label59";
            // 
            // label49
            // 
            resources.ApplyResources(this.label49, "label49");
            this.label49.Name = "label49";
            // 
            // txtResultPoint2
            // 
            resources.ApplyResources(this.txtResultPoint2, "txtResultPoint2");
            this.txtResultPoint2.Name = "txtResultPoint2";
            // 
            // label58
            // 
            resources.ApplyResources(this.label58, "label58");
            this.label58.Name = "label58";
            // 
            // label60
            // 
            resources.ApplyResources(this.label60, "label60");
            this.label60.Name = "label60";
            // 
            // label45
            // 
            resources.ApplyResources(this.label45, "label45");
            this.label45.Name = "label45";
            // 
            // label62
            // 
            resources.ApplyResources(this.label62, "label62");
            this.label62.Name = "label62";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // txtStartPoint1
            // 
            resources.ApplyResources(this.txtStartPoint1, "txtStartPoint1");
            this.txtStartPoint1.Name = "txtStartPoint1";
            // 
            // txtEndPoint1
            // 
            resources.ApplyResources(this.txtEndPoint1, "txtEndPoint1");
            this.txtEndPoint1.Name = "txtEndPoint1";
            // 
            // txtSecondLength2
            // 
            resources.ApplyResources(this.txtSecondLength2, "txtSecondLength2");
            this.txtSecondLength2.Name = "txtSecondLength2";
            // 
            // txtEndPoint2
            // 
            resources.ApplyResources(this.txtEndPoint2, "txtEndPoint2");
            this.txtEndPoint2.Name = "txtEndPoint2";
            // 
            // txtSecondLength1
            // 
            resources.ApplyResources(this.txtSecondLength1, "txtSecondLength1");
            this.txtSecondLength1.Name = "txtSecondLength1";
            // 
            // txtStartPoint2
            // 
            resources.ApplyResources(this.txtStartPoint2, "txtStartPoint2");
            this.txtStartPoint2.Name = "txtStartPoint2";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.tableLayoutPanel19);
            resources.ApplyResources(this.groupBox13, "groupBox13");
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.TabStop = false;
            // 
            // tableLayoutPanel19
            // 
            resources.ApplyResources(this.tableLayoutPanel19, "tableLayoutPanel19");
            this.tableLayoutPanel19.Controls.Add(this.txt_LR2, 1, 1);
            this.tableLayoutPanel19.Controls.Add(this.txt_LR1, 1, 0);
            this.tableLayoutPanel19.Controls.Add(this.label55, 0, 0);
            this.tableLayoutPanel19.Controls.Add(this.label54, 0, 1);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            // 
            // txt_LR2
            // 
            resources.ApplyResources(this.txt_LR2, "txt_LR2");
            this.txt_LR2.Name = "txt_LR2";
            // 
            // txt_LR1
            // 
            resources.ApplyResources(this.txt_LR1, "txt_LR1");
            this.txt_LR1.Name = "txt_LR1";
            // 
            // label55
            // 
            resources.ApplyResources(this.label55, "label55");
            this.label55.Name = "label55";
            // 
            // label54
            // 
            resources.ApplyResources(this.label54, "label54");
            this.label54.Name = "label54";
            // 
            // tableLayoutPanel44
            // 
            resources.ApplyResources(this.tableLayoutPanel44, "tableLayoutPanel44");
            this.tableLayoutPanel44.Controls.Add(this.groupBox34, 0, 0);
            this.tableLayoutPanel44.Controls.Add(this.btnSavePlcPoint, 1, 0);
            this.tableLayoutPanel44.Name = "tableLayoutPanel44";
            // 
            // groupBox34
            // 
            this.groupBox34.Controls.Add(this.tableLayoutPanel20);
            resources.ApplyResources(this.groupBox34, "groupBox34");
            this.groupBox34.Name = "groupBox34";
            this.groupBox34.TabStop = false;
            // 
            // tableLayoutPanel20
            // 
            resources.ApplyResources(this.tableLayoutPanel20, "tableLayoutPanel20");
            this.tableLayoutPanel20.Controls.Add(this.label116, 0, 0);
            this.tableLayoutPanel20.Controls.Add(this.txtDeviceStatePoint, 1, 0);
            this.tableLayoutPanel20.Controls.Add(this.txtViewStatus, 3, 0);
            this.tableLayoutPanel20.Controls.Add(this.textBox35, 3, 3);
            this.tableLayoutPanel20.Controls.Add(this.label113, 0, 1);
            this.tableLayoutPanel20.Controls.Add(this.textBox47, 3, 2);
            this.tableLayoutPanel20.Controls.Add(this.label123, 2, 0);
            this.tableLayoutPanel20.Controls.Add(this.txtPMLength, 3, 1);
            this.tableLayoutPanel20.Controls.Add(this.txtProductModelPoint, 1, 1);
            this.tableLayoutPanel20.Controls.Add(this.label112, 0, 2);
            this.tableLayoutPanel20.Controls.Add(this.label8, 2, 3);
            this.tableLayoutPanel20.Controls.Add(this.label111, 0, 3);
            this.tableLayoutPanel20.Controls.Add(this.label117, 2, 2);
            this.tableLayoutPanel20.Controls.Add(this.textBox43, 1, 2);
            this.tableLayoutPanel20.Controls.Add(this.txtRecipeIdPoint, 1, 3);
            this.tableLayoutPanel20.Controls.Add(this.label114, 2, 1);
            this.tableLayoutPanel20.Name = "tableLayoutPanel20";
            // 
            // label116
            // 
            resources.ApplyResources(this.label116, "label116");
            this.label116.Name = "label116";
            // 
            // txtDeviceStatePoint
            // 
            resources.ApplyResources(this.txtDeviceStatePoint, "txtDeviceStatePoint");
            this.txtDeviceStatePoint.Name = "txtDeviceStatePoint";
            // 
            // txtViewStatus
            // 
            resources.ApplyResources(this.txtViewStatus, "txtViewStatus");
            this.txtViewStatus.Name = "txtViewStatus";
            // 
            // textBox35
            // 
            resources.ApplyResources(this.textBox35, "textBox35");
            this.textBox35.Name = "textBox35";
            // 
            // label113
            // 
            resources.ApplyResources(this.label113, "label113");
            this.label113.Name = "label113";
            // 
            // textBox47
            // 
            resources.ApplyResources(this.textBox47, "textBox47");
            this.textBox47.Name = "textBox47";
            // 
            // label123
            // 
            resources.ApplyResources(this.label123, "label123");
            this.label123.Name = "label123";
            // 
            // txtPMLength
            // 
            resources.ApplyResources(this.txtPMLength, "txtPMLength");
            this.txtPMLength.Name = "txtPMLength";
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
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label111
            // 
            resources.ApplyResources(this.label111, "label111");
            this.label111.Name = "label111";
            // 
            // label117
            // 
            resources.ApplyResources(this.label117, "label117");
            this.label117.Name = "label117";
            // 
            // textBox43
            // 
            resources.ApplyResources(this.textBox43, "textBox43");
            this.textBox43.Name = "textBox43";
            // 
            // txtRecipeIdPoint
            // 
            resources.ApplyResources(this.txtRecipeIdPoint, "txtRecipeIdPoint");
            this.txtRecipeIdPoint.Name = "txtRecipeIdPoint";
            // 
            // label114
            // 
            resources.ApplyResources(this.label114, "label114");
            this.label114.Name = "label114";
            // 
            // btnSavePlcPoint
            // 
            resources.ApplyResources(this.btnSavePlcPoint, "btnSavePlcPoint");
            this.btnSavePlcPoint.Name = "btnSavePlcPoint";
            this.btnSavePlcPoint.UseVisualStyleBackColor = true;
            this.btnSavePlcPoint.Click += new System.EventHandler(this.PLCPointSaveBtn_Click);
            // 
            // tabPage18
            // 
            this.tabPage18.Controls.Add(this.rtbMESOutput);
            this.tabPage18.Controls.Add(this.btnAccessMES);
            this.tabPage18.Controls.Add(this.rtbMESInput);
            resources.ApplyResources(this.tabPage18, "tabPage18");
            this.tabPage18.Name = "tabPage18";
            this.tabPage18.UseVisualStyleBackColor = true;
            // 
            // rtbMESOutput
            // 
            resources.ApplyResources(this.rtbMESOutput, "rtbMESOutput");
            this.rtbMESOutput.Name = "rtbMESOutput";
            // 
            // btnAccessMES
            // 
            resources.ApplyResources(this.btnAccessMES, "btnAccessMES");
            this.btnAccessMES.Name = "btnAccessMES";
            this.btnAccessMES.UseVisualStyleBackColor = true;
            this.btnAccessMES.Click += new System.EventHandler(this.button37_Click);
            // 
            // rtbMESInput
            // 
            resources.ApplyResources(this.rtbMESInput, "rtbMESInput");
            this.rtbMESInput.Name = "rtbMESInput";
            // 
            // tabPageMES参数
            // 
            this.tabPageMES参数.Controls.Add(this.tableLayoutPanel1);
            this.tabPageMES参数.Controls.Add(this.btnSaveMesConfig);
            resources.ApplyResources(this.tabPageMES参数, "tabPageMES参数");
            this.tabPageMES参数.Name = "tabPageMES参数";
            this.tabPageMES参数.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label74, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txt_nccode, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtMES_url, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtOperation, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtMES_site, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtResource, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtMES_Timeout, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtMES_Port, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtMES_IP, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label24, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label27, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label26, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label25, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label23, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.txt_user, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.txt_password, 1, 9);
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
            // txt_nccode
            // 
            resources.ApplyResources(this.txt_nccode, "txt_nccode");
            this.txt_nccode.Name = "txt_nccode";
            // 
            // txtMES_url
            // 
            resources.ApplyResources(this.txtMES_url, "txtMES_url");
            this.txtMES_url.Name = "txtMES_url";
            // 
            // txtOperation
            // 
            resources.ApplyResources(this.txtOperation, "txtOperation");
            this.txtOperation.Name = "txtOperation";
            // 
            // txtMES_site
            // 
            resources.ApplyResources(this.txtMES_site, "txtMES_site");
            this.txtMES_site.Name = "txtMES_site";
            // 
            // txtResource
            // 
            resources.ApplyResources(this.txtResource, "txtResource");
            this.txtResource.Name = "txtResource";
            // 
            // txtMES_Timeout
            // 
            resources.ApplyResources(this.txtMES_Timeout, "txtMES_Timeout");
            this.txtMES_Timeout.Name = "txtMES_Timeout";
            // 
            // txtMES_Port
            // 
            resources.ApplyResources(this.txtMES_Port, "txtMES_Port");
            this.txtMES_Port.Name = "txtMES_Port";
            // 
            // txtMES_IP
            // 
            resources.ApplyResources(this.txtMES_IP, "txtMES_IP");
            this.txtMES_IP.Name = "txtMES_IP";
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
            // txt_user
            // 
            resources.ApplyResources(this.txt_user, "txt_user");
            this.txt_user.Name = "txt_user";
            // 
            // txt_password
            // 
            resources.ApplyResources(this.txt_password, "txt_password");
            this.txt_password.Name = "txt_password";
            // 
            // btnSaveMesConfig
            // 
            resources.ApplyResources(this.btnSaveMesConfig, "btnSaveMesConfig");
            this.btnSaveMesConfig.Name = "btnSaveMesConfig";
            this.btnSaveMesConfig.UseVisualStyleBackColor = true;
            this.btnSaveMesConfig.Click += new System.EventHandler(this.SaveMesConfig_Click);
            // 
            // tabPage生产日志
            // 
            this.tabPage生产日志.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.tabPage生产日志, "tabPage生产日志");
            this.tabPage生产日志.Name = "tabPage生产日志";
            this.tabPage生产日志.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.groupBox14, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox9, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox12, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // groupBox14
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.groupBox14, 2);
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
            // tabPage历史数据
            // 
            this.tabPage历史数据.Controls.Add(this.tableLayoutPanel37);
            resources.ApplyResources(this.tabPage历史数据, "tabPage历史数据");
            this.tabPage历史数据.Name = "tabPage历史数据";
            this.tabPage历史数据.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel37
            // 
            resources.ApplyResources(this.tableLayoutPanel37, "tableLayoutPanel37");
            this.tableLayoutPanel37.Controls.Add(this.groupBox4, 1, 1);
            this.tableLayoutPanel37.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel37.Controls.Add(this.groupBox16, 1, 0);
            this.tableLayoutPanel37.Name = "tableLayoutPanel37";
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
            // panel3
            // 
            this.panel3.Controls.Add(this.数据源);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            this.tableLayoutPanel37.SetRowSpan(this.panel3, 2);
            // 
            // 数据源
            // 
            this.数据源.Controls.Add(this.directoryTreeView);
            this.数据源.Cursor = System.Windows.Forms.Cursors.Arrow;
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
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.tableLayoutPanel36);
            resources.ApplyResources(this.groupBox16, "groupBox16");
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.TabStop = false;
            // 
            // tableLayoutPanel36
            // 
            resources.ApplyResources(this.tableLayoutPanel36, "tableLayoutPanel36");
            this.tableLayoutPanel36.Controls.Add(this.textBoxPath, 1, 0);
            this.tableLayoutPanel36.Controls.Add(this.btnRefresh, 4, 2);
            this.tableLayoutPanel36.Controls.Add(this.btnSearch, 4, 0);
            this.tableLayoutPanel36.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel36.Controls.Add(this.textBox1, 3, 2);
            this.tableLayoutPanel36.Controls.Add(this.label13, 0, 1);
            this.tableLayoutPanel36.Controls.Add(this.btnExport, 4, 1);
            this.tableLayoutPanel36.Controls.Add(this.textBox_Code, 3, 1);
            this.tableLayoutPanel36.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel36.Controls.Add(this.dateTimePicker2, 1, 2);
            this.tableLayoutPanel36.Controls.Add(this.dateTimePicker1, 1, 1);
            this.tableLayoutPanel36.Controls.Add(this.label6, 2, 2);
            this.tableLayoutPanel36.Controls.Add(this.label15, 2, 1);
            this.tableLayoutPanel36.Name = "tableLayoutPanel36";
            // 
            // textBoxPath
            // 
            this.tableLayoutPanel36.SetColumnSpan(this.textBoxPath, 3);
            resources.ApplyResources(this.textBoxPath, "textBoxPath");
            this.textBoxPath.Name = "textBoxPath";
            // 
            // btnRefresh
            // 
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefreshDirectory_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // btnExport
            // 
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Name = "btnExport";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.ExportProductData);
            // 
            // textBox_Code
            // 
            resources.ApplyResources(this.textBox_Code, "textBox_Code");
            this.textBox_Code.Name = "textBox_Code";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // dateTimePicker2
            // 
            resources.ApplyResources(this.dateTimePicker2, "dateTimePicker2");
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Value = new System.DateTime(2024, 3, 10, 10, 27, 0, 0);
            // 
            // dateTimePicker1
            // 
            resources.ApplyResources(this.dateTimePicker1, "dateTimePicker1");
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.MaxDate = new System.DateTime(9998, 12, 17, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Value = new System.DateTime(2024, 3, 10, 10, 26, 0, 0);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // tabPage运行界面
            // 
            this.tabPage运行界面.Controls.Add(this.Panel运行界面);
            resources.ApplyResources(this.tabPage运行界面, "tabPage运行界面");
            this.tabPage运行界面.Name = "tabPage运行界面";
            this.tabPage运行界面.UseVisualStyleBackColor = true;
            // 
            // Panel运行界面
            // 
            this.Panel运行界面.Controls.Add(this.splitContainer_LR);
            resources.ApplyResources(this.Panel运行界面, "Panel运行界面");
            this.Panel运行界面.Name = "Panel运行界面";
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage运行界面);
            this.tabControl1.Controls.Add(this.tabPage历史数据);
            this.tabControl1.Controls.Add(this.tabPage生产日志);
            this.tabControl1.Controls.Add(this.tabPageMES参数);
            this.tabControl1.Controls.Add(this.tabPage系统设置);
            this.tabControl1.Controls.Add(this.tabPage用户管理);
            this.tabControl1.Controls.Add(this.tabPage打印设置);
            this.tabControl1.Controls.Add(this.tabPage看板设置);
            this.tabControl1.Controls.Add(this.tabPage配方设置);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage看板设置
            // 
            this.tabPage看板设置.Controls.Add(this.tableLayoutPanel21);
            resources.ApplyResources(this.tabPage看板设置, "tabPage看板设置");
            this.tabPage看板设置.Name = "tabPage看板设置";
            this.tabPage看板设置.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel21
            // 
            resources.ApplyResources(this.tableLayoutPanel21, "tableLayoutPanel21");
            this.tableLayoutPanel21.Controls.Add(this.groupBox看板参数, 0, 0);
            this.tableLayoutPanel21.Controls.Add(this.panel易损件数据, 0, 1);
            this.tableLayoutPanel21.Controls.Add(this.panel故障信息, 1, 1);
            this.tableLayoutPanel21.Name = "tableLayoutPanel21";
            // 
            // groupBox看板参数
            // 
            this.tableLayoutPanel21.SetColumnSpan(this.groupBox看板参数, 2);
            this.groupBox看板参数.Controls.Add(this.flowLayoutPanel1);
            this.groupBox看板参数.Controls.Add(this.tableLayoutPanel26);
            resources.ApplyResources(this.groupBox看板参数, "groupBox看板参数");
            this.groupBox看板参数.Name = "groupBox看板参数";
            this.groupBox看板参数.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // tableLayoutPanel26
            // 
            resources.ApplyResources(this.tableLayoutPanel26, "tableLayoutPanel26");
            this.tableLayoutPanel26.Controls.Add(this.tableLayoutPanel29, 0, 0);
            this.tableLayoutPanel26.Controls.Add(this.tableLayoutPanel27, 1, 0);
            this.tableLayoutPanel26.Name = "tableLayoutPanel26";
            // 
            // tableLayoutPanel29
            // 
            resources.ApplyResources(this.tableLayoutPanel29, "tableLayoutPanel29");
            this.tableLayoutPanel29.Controls.Add(this.tableLayoutPanel62, 0, 1);
            this.tableLayoutPanel29.Controls.Add(this.tableLayoutPanel61, 0, 0);
            this.tableLayoutPanel29.Name = "tableLayoutPanel29";
            // 
            // tableLayoutPanel62
            // 
            resources.ApplyResources(this.tableLayoutPanel62, "tableLayoutPanel62");
            this.tableLayoutPanel62.Controls.Add(this.groupBox15, 0, 0);
            this.tableLayoutPanel62.Name = "tableLayoutPanel62";
            // 
            // groupBox15
            // 
            this.tableLayoutPanel62.SetColumnSpan(this.groupBox15, 2);
            this.groupBox15.Controls.Add(this.tableLayoutPanel25);
            resources.ApplyResources(this.groupBox15, "groupBox15");
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.TabStop = false;
            // 
            // tableLayoutPanel25
            // 
            resources.ApplyResources(this.tableLayoutPanel25, "tableLayoutPanel25");
            this.tableLayoutPanel25.Controls.Add(this.txtStationNameSets_Thai, 1, 3);
            this.tableLayoutPanel25.Controls.Add(this.txtStationNameSets_English, 1, 2);
            this.tableLayoutPanel25.Controls.Add(this.txtStationName, 1, 0);
            this.tableLayoutPanel25.Controls.Add(this.label93, 0, 0);
            this.tableLayoutPanel25.Controls.Add(this.txtStationNameSets, 1, 1);
            this.tableLayoutPanel25.Controls.Add(this.label51, 0, 1);
            this.tableLayoutPanel25.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel25.Controls.Add(this.label66, 0, 3);
            this.tableLayoutPanel25.Controls.Add(this.label95, 0, 4);
            this.tableLayoutPanel25.Name = "tableLayoutPanel25";
            // 
            // txtStationNameSets_Thai
            // 
            resources.ApplyResources(this.txtStationNameSets_Thai, "txtStationNameSets_Thai");
            this.txtStationNameSets_Thai.Name = "txtStationNameSets_Thai";
            // 
            // txtStationNameSets_English
            // 
            resources.ApplyResources(this.txtStationNameSets_English, "txtStationNameSets_English");
            this.txtStationNameSets_English.Name = "txtStationNameSets_English";
            // 
            // txtStationName
            // 
            resources.ApplyResources(this.txtStationName, "txtStationName");
            this.txtStationName.Name = "txtStationName";
            // 
            // label93
            // 
            resources.ApplyResources(this.label93, "label93");
            this.label93.Name = "label93";
            // 
            // txtStationNameSets
            // 
            resources.ApplyResources(this.txtStationNameSets, "txtStationNameSets");
            this.txtStationNameSets.Name = "txtStationNameSets";
            // 
            // label51
            // 
            resources.ApplyResources(this.label51, "label51");
            this.label51.Name = "label51";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label66
            // 
            resources.ApplyResources(this.label66, "label66");
            this.label66.Name = "label66";
            // 
            // label95
            // 
            resources.ApplyResources(this.label95, "label95");
            this.tableLayoutPanel25.SetColumnSpan(this.label95, 2);
            this.label95.ForeColor = System.Drawing.Color.Red;
            this.label95.Name = "label95";
            // 
            // tableLayoutPanel61
            // 
            resources.ApplyResources(this.tableLayoutPanel61, "tableLayoutPanel61");
            this.tableLayoutPanel61.Controls.Add(this.tlp启用看板, 0, 0);
            this.tableLayoutPanel61.Name = "tableLayoutPanel61";
            // 
            // tlp启用看板
            // 
            resources.ApplyResources(this.tlp启用看板, "tlp启用看板");
            this.tableLayoutPanel61.SetColumnSpan(this.tlp启用看板, 2);
            this.tlp启用看板.Controls.Add(this.tableLayoutPanel28, 0, 0);
            this.tlp启用看板.Controls.Add(this.txtDashboardIP, 1, 1);
            this.tlp启用看板.Controls.Add(this.txtDashboardPort, 1, 2);
            this.tlp启用看板.Controls.Add(this.label42, 0, 1);
            this.tlp启用看板.Controls.Add(this.label40, 0, 2);
            this.tlp启用看板.Controls.Add(this.btnConnectDashboard, 2, 1);
            this.tlp启用看板.Name = "tlp启用看板";
            // 
            // tableLayoutPanel28
            // 
            resources.ApplyResources(this.tableLayoutPanel28, "tableLayoutPanel28");
            this.tlp启用看板.SetColumnSpan(this.tableLayoutPanel28, 3);
            this.tableLayoutPanel28.Controls.Add(this.chkReadPName, 2, 0);
            this.tableLayoutPanel28.Controls.Add(this.label83, 1, 0);
            this.tableLayoutPanel28.Controls.Add(this.chkEnableDashboard, 0, 0);
            this.tableLayoutPanel28.Name = "tableLayoutPanel28";
            // 
            // chkReadPName
            // 
            resources.ApplyResources(this.chkReadPName, "chkReadPName");
            this.chkReadPName.Name = "chkReadPName";
            this.chkReadPName.UseVisualStyleBackColor = true;
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
            // txtDashboardIP
            // 
            resources.ApplyResources(this.txtDashboardIP, "txtDashboardIP");
            this.txtDashboardIP.Name = "txtDashboardIP";
            // 
            // txtDashboardPort
            // 
            resources.ApplyResources(this.txtDashboardPort, "txtDashboardPort");
            this.txtDashboardPort.Name = "txtDashboardPort";
            // 
            // label42
            // 
            resources.ApplyResources(this.label42, "label42");
            this.label42.Name = "label42";
            // 
            // label40
            // 
            resources.ApplyResources(this.label40, "label40");
            this.label40.Name = "label40";
            // 
            // btnConnectDashboard
            // 
            resources.ApplyResources(this.btnConnectDashboard, "btnConnectDashboard");
            this.btnConnectDashboard.Name = "btnConnectDashboard";
            this.tlp启用看板.SetRowSpan(this.btnConnectDashboard, 2);
            this.btnConnectDashboard.UseVisualStyleBackColor = true;
            this.btnConnectDashboard.Click += new System.EventHandler(this.ConnectDashboard_Click);
            // 
            // tableLayoutPanel27
            // 
            resources.ApplyResources(this.tableLayoutPanel27, "tableLayoutPanel27");
            this.tableLayoutPanel27.Controls.Add(this.tableLayoutPanel63, 0, 1);
            this.tableLayoutPanel27.Controls.Add(this.tableLayoutPanel22, 0, 0);
            this.tableLayoutPanel27.Name = "tableLayoutPanel27";
            // 
            // tableLayoutPanel63
            // 
            resources.ApplyResources(this.tableLayoutPanel63, "tableLayoutPanel63");
            this.tableLayoutPanel63.Controls.Add(this.tableLayoutPanel24, 0, 0);
            this.tableLayoutPanel63.Name = "tableLayoutPanel63";
            // 
            // tableLayoutPanel24
            // 
            resources.ApplyResources(this.tableLayoutPanel24, "tableLayoutPanel24");
            this.tableLayoutPanel24.Controls.Add(this.tableLayoutPanel42, 0, 2);
            this.tableLayoutPanel24.Controls.Add(this.label100, 0, 0);
            this.tableLayoutPanel24.Controls.Add(this.txtFaultStartPoint, 1, 0);
            this.tableLayoutPanel24.Controls.Add(this.txtFaultLength, 1, 1);
            this.tableLayoutPanel24.Controls.Add(this.label101, 0, 1);
            this.tableLayoutPanel24.Name = "tableLayoutPanel24";
            // 
            // tableLayoutPanel42
            // 
            resources.ApplyResources(this.tableLayoutPanel42, "tableLayoutPanel42");
            this.tableLayoutPanel24.SetColumnSpan(this.tableLayoutPanel42, 2);
            this.tableLayoutPanel42.Controls.Add(this.btnSaveAtDashboardSetting, 0, 0);
            this.tableLayoutPanel42.Controls.Add(this.btnRefreshAtBulletin, 1, 0);
            this.tableLayoutPanel42.Name = "tableLayoutPanel42";
            // 
            // btnSaveAtDashboardSetting
            // 
            resources.ApplyResources(this.btnSaveAtDashboardSetting, "btnSaveAtDashboardSetting");
            this.btnSaveAtDashboardSetting.Name = "btnSaveAtDashboardSetting";
            this.btnSaveAtDashboardSetting.UseVisualStyleBackColor = true;
            this.btnSaveAtDashboardSetting.Click += new System.EventHandler(this.BtnSaveDashboardConfig_Click);
            // 
            // btnRefreshAtBulletin
            // 
            resources.ApplyResources(this.btnRefreshAtBulletin, "btnRefreshAtBulletin");
            this.btnRefreshAtBulletin.Name = "btnRefreshAtBulletin";
            this.btnRefreshAtBulletin.UseVisualStyleBackColor = true;
            this.btnRefreshAtBulletin.Click += new System.EventHandler(this.BtnRefreshAtDashboardSetting);
            // 
            // label100
            // 
            resources.ApplyResources(this.label100, "label100");
            this.label100.Name = "label100";
            // 
            // txtFaultStartPoint
            // 
            resources.ApplyResources(this.txtFaultStartPoint, "txtFaultStartPoint");
            this.txtFaultStartPoint.Name = "txtFaultStartPoint";
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
            // tableLayoutPanel22
            // 
            resources.ApplyResources(this.tableLayoutPanel22, "tableLayoutPanel22");
            this.tableLayoutPanel22.Controls.Add(this.tableLayoutPanel23, 0, 0);
            this.tableLayoutPanel22.Name = "tableLayoutPanel22";
            // 
            // tableLayoutPanel23
            // 
            resources.ApplyResources(this.tableLayoutPanel23, "tableLayoutPanel23");
            this.tableLayoutPanel23.Controls.Add(this.txtLineName, 1, 0);
            this.tableLayoutPanel23.Controls.Add(this.txtUseTimePoint, 1, 1);
            this.tableLayoutPanel23.Controls.Add(this.label61, 0, 0);
            this.tableLayoutPanel23.Controls.Add(this.label53, 0, 1);
            this.tableLayoutPanel23.Controls.Add(this.BoardCreateDataBaseBtn, 2, 0);
            this.tableLayoutPanel23.Name = "tableLayoutPanel23";
            // 
            // txtLineName
            // 
            resources.ApplyResources(this.txtLineName, "txtLineName");
            this.txtLineName.Name = "txtLineName";
            // 
            // txtUseTimePoint
            // 
            resources.ApplyResources(this.txtUseTimePoint, "txtUseTimePoint");
            this.txtUseTimePoint.Name = "txtUseTimePoint";
            // 
            // label61
            // 
            resources.ApplyResources(this.label61, "label61");
            this.label61.Name = "label61";
            // 
            // label53
            // 
            resources.ApplyResources(this.label53, "label53");
            this.label53.Name = "label53";
            // 
            // BoardCreateDataBaseBtn
            // 
            resources.ApplyResources(this.BoardCreateDataBaseBtn, "BoardCreateDataBaseBtn");
            this.BoardCreateDataBaseBtn.Name = "BoardCreateDataBaseBtn";
            this.tableLayoutPanel23.SetRowSpan(this.BoardCreateDataBaseBtn, 2);
            this.BoardCreateDataBaseBtn.UseVisualStyleBackColor = true;
            this.BoardCreateDataBaseBtn.Click += new System.EventHandler(this.BoardCreateDataBaseBtn_Click);
            // 
            // panel易损件数据
            // 
            this.panel易损件数据.Controls.Add(this.groupBox易损件);
            resources.ApplyResources(this.panel易损件数据, "panel易损件数据");
            this.panel易损件数据.Name = "panel易损件数据";
            // 
            // groupBox易损件
            // 
            this.groupBox易损件.Controls.Add(this.dgvWeakInfo);
            this.groupBox易损件.Controls.Add(this.label39);
            resources.ApplyResources(this.groupBox易损件, "groupBox易损件");
            this.groupBox易损件.ForeColor = System.Drawing.Color.Red;
            this.groupBox易损件.Name = "groupBox易损件";
            this.groupBox易损件.TabStop = false;
            // 
            // dgvWeakInfo
            // 
            this.dgvWeakInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWeakInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle33.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle33.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle33.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle33.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle33.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWeakInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle33;
            this.dgvWeakInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvWeakInfo, "dgvWeakInfo");
            this.dgvWeakInfo.Name = "dgvWeakInfo";
            this.dgvWeakInfo.RowHeadersVisible = false;
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvWeakInfo.RowsDefaultCellStyle = dataGridViewCellStyle34;
            this.dgvWeakInfo.RowTemplate.Height = 27;
            this.dgvWeakInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Name = "label39";
            // 
            // panel故障信息
            // 
            this.panel故障信息.Controls.Add(this.groupBox故障信息);
            resources.ApplyResources(this.panel故障信息, "panel故障信息");
            this.panel故障信息.Name = "panel故障信息";
            // 
            // groupBox故障信息
            // 
            this.groupBox故障信息.Controls.Add(this.dgvFaultInfo);
            this.groupBox故障信息.Controls.Add(this.label46);
            resources.ApplyResources(this.groupBox故障信息, "groupBox故障信息");
            this.groupBox故障信息.ForeColor = System.Drawing.Color.Red;
            this.groupBox故障信息.Name = "groupBox故障信息";
            this.groupBox故障信息.TabStop = false;
            // 
            // dgvFaultInfo
            // 
            this.dgvFaultInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFaultInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle35.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFaultInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle35;
            this.dgvFaultInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvFaultInfo, "dgvFaultInfo");
            this.dgvFaultInfo.Name = "dgvFaultInfo";
            this.dgvFaultInfo.RowHeadersVisible = false;
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvFaultInfo.RowsDefaultCellStyle = dataGridViewCellStyle36;
            this.dgvFaultInfo.RowTemplate.Height = 27;
            this.dgvFaultInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellContentClick);
            // 
            // label46
            // 
            resources.ApplyResources(this.label46, "label46");
            this.label46.ForeColor = System.Drawing.Color.Red;
            this.label46.Name = "label46";
            // 
            // tabPage配方设置
            // 
            this.tabPage配方设置.Controls.Add(this.tlp_配方设置);
            resources.ApplyResources(this.tabPage配方设置, "tabPage配方设置");
            this.tabPage配方设置.Name = "tabPage配方设置";
            this.tabPage配方设置.UseVisualStyleBackColor = true;
            // 
            // tlp_配方设置
            // 
            resources.ApplyResources(this.tlp_配方设置, "tlp_配方设置");
            this.tlp_配方设置.Controls.Add(this.panel5, 0, 0);
            this.tlp_配方设置.Controls.Add(this.panel4, 0, 1);
            this.tlp_配方设置.Name = "tlp_配方设置";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox30);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // groupBox30
            // 
            this.groupBox30.Controls.Add(this.tableLayoutPanel38);
            resources.ApplyResources(this.groupBox30, "groupBox30");
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.TabStop = false;
            // 
            // tableLayoutPanel38
            // 
            resources.ApplyResources(this.tableLayoutPanel38, "tableLayoutPanel38");
            this.tableLayoutPanel38.Controls.Add(this.flowLayoutPanel2, 0, 4);
            this.tableLayoutPanel38.Controls.Add(this.groupBox20, 0, 0);
            this.tableLayoutPanel38.Controls.Add(this.groupBox24, 0, 3);
            this.tableLayoutPanel38.Controls.Add(this.groupBox21, 0, 1);
            this.tableLayoutPanel38.Controls.Add(this.groupBox23, 0, 2);
            this.tableLayoutPanel38.Name = "tableLayoutPanel38";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnSaveRecipeConfig);
            this.flowLayoutPanel2.Controls.Add(this.label72);
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // btnSaveRecipeConfig
            // 
            resources.ApplyResources(this.btnSaveRecipeConfig, "btnSaveRecipeConfig");
            this.btnSaveRecipeConfig.Name = "btnSaveRecipeConfig";
            this.btnSaveRecipeConfig.UseVisualStyleBackColor = true;
            this.btnSaveRecipeConfig.Click += new System.EventHandler(this.btnSaveRecipeConfig_Click);
            // 
            // label72
            // 
            resources.ApplyResources(this.label72, "label72");
            this.label72.Name = "label72";
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.txtNameSets);
            resources.ApplyResources(this.groupBox20, "groupBox20");
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.TabStop = false;
            // 
            // txtNameSets
            // 
            resources.ApplyResources(this.txtNameSets, "txtNameSets");
            this.txtNameSets.Name = "txtNameSets";
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.txtPointSets);
            resources.ApplyResources(this.groupBox24, "groupBox24");
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.TabStop = false;
            // 
            // txtPointSets
            // 
            resources.ApplyResources(this.txtPointSets, "txtPointSets");
            this.txtPointSets.Name = "txtPointSets";
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.txtEnglishNameSets);
            resources.ApplyResources(this.groupBox21, "groupBox21");
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.TabStop = false;
            // 
            // txtEnglishNameSets
            // 
            resources.ApplyResources(this.txtEnglishNameSets, "txtEnglishNameSets");
            this.txtEnglishNameSets.Name = "txtEnglishNameSets";
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.txtThaiNameSets);
            resources.ApplyResources(this.groupBox23, "groupBox23");
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.TabStop = false;
            // 
            // txtThaiNameSets
            // 
            resources.ApplyResources(this.txtThaiNameSets, "txtThaiNameSets");
            this.txtThaiNameSets.Name = "txtThaiNameSets";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tableLayoutPanel58);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // tableLayoutPanel58
            // 
            resources.ApplyResources(this.tableLayoutPanel58, "tableLayoutPanel58");
            this.tableLayoutPanel58.Controls.Add(this.label105, 0, 0);
            this.tableLayoutPanel58.Controls.Add(this.groupBox28, 0, 1);
            this.tableLayoutPanel58.Name = "tableLayoutPanel58";
            // 
            // label105
            // 
            resources.ApplyResources(this.label105, "label105");
            this.label105.ForeColor = System.Drawing.Color.Red;
            this.label105.Name = "label105";
            // 
            // groupBox28
            // 
            this.groupBox28.Controls.Add(this.dgvRecipeManage);
            resources.ApplyResources(this.groupBox28, "groupBox28");
            this.groupBox28.ForeColor = System.Drawing.Color.Red;
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.TabStop = false;
            // 
            // dgvRecipeManage
            // 
            this.dgvRecipeManage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRecipeManage.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvRecipeManage.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle37.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle37.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle37.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle37.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle37.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecipeManage.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle37;
            this.dgvRecipeManage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecipeManage.ColumnKey = null;
            this.dgvRecipeManage.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle38.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle38.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle38.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle38.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecipeManage.DefaultCellStyle = dataGridViewCellStyle38;
            resources.ApplyResources(this.dgvRecipeManage, "dgvRecipeManage");
            this.dgvRecipeManage.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvRecipeManage.Name = "dgvRecipeManage";
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle39.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle39.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F);
            dataGridViewCellStyle39.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle39.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle39.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecipeManage.RowHeadersDefaultCellStyle = dataGridViewCellStyle39;
            this.dgvRecipeManage.RowHeadersVisible = false;
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvRecipeManage.RowsDefaultCellStyle = dataGridViewCellStyle40;
            this.dgvRecipeManage.RowTemplate.Height = 23;
            this.dgvRecipeManage.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView5_CellContentClick);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTest)).EndInit();
            this.tabControl_UploadData.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult3)).EndInit();
            this.splitContainer_LR.Panel1.ResumeLayout(false);
            this.splitContainer_LR.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_LR)).EndInit();
            this.splitContainer_LR.ResumeLayout(false);
            this.panel_运行界面左.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowBarcode)).EndInit();
            this.tableLayoutPanel59.ResumeLayout(false);
            this.tlp_Status.ResumeLayout(false);
            this.tlp_Status.PerformLayout();
            this.panelDeviceName.ResumeLayout(false);
            this.tlp运行界面右.ResumeLayout(false);
            this.tlp运行界面右.PerformLayout();
            this.groupBox生产信息.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionIndex)).EndInit();
            this.tlp_运行状态.ResumeLayout(false);
            this.tlp_运行状态.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.tlp_ProductResult.ResumeLayout(false);
            this.tlp_ProductResult.PerformLayout();
            this.tpl_生产面板.ResumeLayout(false);
            this.tpl_生产面板.PerformLayout();
            this.tableLayoutPanel41.ResumeLayout(false);
            this.tableLayoutPanel41.PerformLayout();
            this.tlp_Version.ResumeLayout(false);
            this.tlp_Version.PerformLayout();
            this.tableLayoutPanel40.ResumeLayout(false);
            this.tableLayoutPanel39.ResumeLayout(false);
            this.tableLayoutPanel39.PerformLayout();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.tabPage打印设置.ResumeLayout(false);
            this.tabControl4.ResumeLayout(false);
            this.tabPage15.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.tableLayoutPanel60.ResumeLayout(false);
            this.groupBox45.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox35.ResumeLayout(false);
            this.groupBox22.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.groupBox40.ResumeLayout(false);
            this.tableLayoutPanel51.ResumeLayout(false);
            this.tableLayoutPanel51.PerformLayout();
            this.tableLayoutPanel52.ResumeLayout(false);
            this.tableLayoutPanel52.PerformLayout();
            this.groupBox36.ResumeLayout(false);
            this.tableLayoutPanel56.ResumeLayout(false);
            this.tableLayoutPanel56.PerformLayout();
            this.tableLayoutPanel57.ResumeLayout(false);
            this.groupBox37.ResumeLayout(false);
            this.tableLayoutPanel50.ResumeLayout(false);
            this.groupBox38.ResumeLayout(false);
            this.tableLayoutPanel53.ResumeLayout(false);
            this.tableLayoutPanel53.PerformLayout();
            this.tableLayoutPanel54.ResumeLayout(false);
            this.tableLayoutPanel54.PerformLayout();
            this.groupBox39.ResumeLayout(false);
            this.groupBox39.PerformLayout();
            this.tableLayoutPanel55.ResumeLayout(false);
            this.tableLayoutPanel55.PerformLayout();
            this.groupBox41.ResumeLayout(false);
            this.groupBox41.PerformLayout();
            this.tabPage用户管理.ResumeLayout(false);
            this.tableLayoutPanel32.ResumeLayout(false);
            this.tableLayoutPanel34.ResumeLayout(false);
            this.groupBox用户信息.ResumeLayout(false);
            this.tableLayoutPanel30.ResumeLayout(false);
            this.tableLayoutPanel30.PerformLayout();
            this.tableLayoutPanel35.ResumeLayout(false);
            this.tableLayoutPanel35.PerformLayout();
            this.groupBox42.ResumeLayout(false);
            this.groupBox42.PerformLayout();
            this.tableLayoutPanel31.ResumeLayout(false);
            this.tableLayoutPanel33.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage系统设置.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage初始化设置.ResumeLayout(false);
            this.tabPage初始化设置.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.groupBox29.ResumeLayout(false);
            this.groupBox29.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel47.ResumeLayout(false);
            this.tableLayoutPanel47.PerformLayout();
            this.tableLayoutPanel48.ResumeLayout(false);
            this.tableLayoutPanel48.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox32.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel64.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox44.ResumeLayout(false);
            this.tableLayoutPanel66.ResumeLayout(false);
            this.tableLayoutPanel66.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.tableLayoutPanel65.ResumeLayout(false);
            this.tableLayoutPanel65.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.groupBox43.ResumeLayout(false);
            this.groupBox43.PerformLayout();
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tableLayoutPanel16.PerformLayout();
            this.tabPage13.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.groupBox27.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPLCPointInfo)).EndInit();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.tabPage11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            this.panel16.ResumeLayout(false);
            this.tabPage14.ResumeLayout(false);
            this.tableLayoutPanel45.ResumeLayout(false);
            this.tableLayoutPanel46.ResumeLayout(false);
            this.groupBox33.ResumeLayout(false);
            this.tableLayoutPanel49.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.tableLayoutPanel17.ResumeLayout(false);
            this.tableLayoutPanel17.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.tableLayoutPanel43.ResumeLayout(false);
            this.tableLayoutPanel43.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.tableLayoutPanel18.ResumeLayout(false);
            this.tableLayoutPanel18.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.tableLayoutPanel19.ResumeLayout(false);
            this.tableLayoutPanel19.PerformLayout();
            this.tableLayoutPanel44.ResumeLayout(false);
            this.groupBox34.ResumeLayout(false);
            this.tableLayoutPanel20.ResumeLayout(false);
            this.tableLayoutPanel20.PerformLayout();
            this.tabPage18.ResumeLayout(false);
            this.tabPageMES参数.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage生产日志.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.tabPage历史数据.ResumeLayout(false);
            this.tableLayoutPanel37.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDynamic1)).EndInit();
            this.panel12.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.数据源.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.tableLayoutPanel36.ResumeLayout(false);
            this.tableLayoutPanel36.PerformLayout();
            this.tabPage运行界面.ResumeLayout(false);
            this.Panel运行界面.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage看板设置.ResumeLayout(false);
            this.tableLayoutPanel21.ResumeLayout(false);
            this.groupBox看板参数.ResumeLayout(false);
            this.groupBox看板参数.PerformLayout();
            this.tableLayoutPanel26.ResumeLayout(false);
            this.tableLayoutPanel29.ResumeLayout(false);
            this.tableLayoutPanel62.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.tableLayoutPanel25.ResumeLayout(false);
            this.tableLayoutPanel25.PerformLayout();
            this.tableLayoutPanel61.ResumeLayout(false);
            this.tlp启用看板.ResumeLayout(false);
            this.tlp启用看板.PerformLayout();
            this.tableLayoutPanel28.ResumeLayout(false);
            this.tableLayoutPanel28.PerformLayout();
            this.tableLayoutPanel27.ResumeLayout(false);
            this.tableLayoutPanel63.ResumeLayout(false);
            this.tableLayoutPanel24.ResumeLayout(false);
            this.tableLayoutPanel24.PerformLayout();
            this.tableLayoutPanel42.ResumeLayout(false);
            this.tableLayoutPanel22.ResumeLayout(false);
            this.tableLayoutPanel23.ResumeLayout(false);
            this.tableLayoutPanel23.PerformLayout();
            this.panel易损件数据.ResumeLayout(false);
            this.groupBox易损件.ResumeLayout(false);
            this.groupBox易损件.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeakInfo)).EndInit();
            this.panel故障信息.ResumeLayout(false);
            this.groupBox故障信息.ResumeLayout(false);
            this.groupBox故障信息.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaultInfo)).EndInit();
            this.tabPage配方设置.ResumeLayout(false);
            this.tlp_配方设置.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBox30.ResumeLayout(false);
            this.tableLayoutPanel38.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.groupBox24.ResumeLayout(false);
            this.groupBox24.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel58.ResumeLayout(false);
            this.tableLayoutPanel58.PerformLayout();
            this.groupBox28.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipeManage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSource1;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.TabPage tabPage打印设置;
        private System.Windows.Forms.TabPage tabPage用户管理;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox用户信息;
        private System.Windows.Forms.ComboBox UTYPE;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox UPWD;
        private System.Windows.Forms.TextBox UID;
        private System.Windows.Forms.Button btnRefreshUserData;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabPage tabPage系统设置;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.TextBox txtDeviceName;
        private System.Windows.Forms.Label label_ChineseName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblDataPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChangeStoragePath;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.TabPage tabPageMES参数;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_nccode;
        private System.Windows.Forms.TextBox txtMES_url;
        private System.Windows.Forms.TextBox txtOperation;
        private System.Windows.Forms.TextBox txtMES_site;
        private System.Windows.Forms.TextBox txtResource;
        private System.Windows.Forms.TextBox txtMES_Timeout;
        private System.Windows.Forms.TextBox txtMES_Port;
        private System.Windows.Forms.TextBox txtMES_IP;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnSaveMesConfig;
        private System.Windows.Forms.TabPage tabPage生产日志;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RichTextBox rtbProductLog;
        private System.Windows.Forms.TabPage tabPage历史数据;
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
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox_Code;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox 数据源;
        private System.Windows.Forms.TreeView directoryTreeView;
        private System.Windows.Forms.TabPage tabPage运行界面;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.RichTextBox rtbDashboardLog;
        private System.Windows.Forms.TabPage tabPage看板设置;
        private System.Windows.Forms.Button btnSaveAtDashboardSetting;
        private System.Windows.Forms.GroupBox groupBox易损件;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.GroupBox groupBox看板参数;
        private System.Windows.Forms.Button btnConnectDashboard;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txtDashboardPort;
        private System.Windows.Forms.TextBox txtDashboardIP;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView dgvWeakInfo;
        private System.Windows.Forms.Button btnRefreshAtBulletin;
        private System.Windows.Forms.Panel panel易损件数据;
        private System.Windows.Forms.Panel panel故障信息;
        private System.Windows.Forms.GroupBox groupBox故障信息;
        private System.Windows.Forms.DataGridView dgvFaultInfo;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.CheckBox chkEnableDashboard;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox txtStationNameSets;
        private System.Windows.Forms.CheckBox chkReadPName;
        private System.Windows.Forms.TextBox txtStationName;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.DataGridView dgvPLCPointInfo;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.TextBox txtDisplayWidth;
        private System.Windows.Forms.Label label_DisplayWidth;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.TextBox tbxBrandID;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Button btnOpenReader;
        private System.Windows.Forms.TabPage tabPage配方设置;
        private System.Windows.Forms.TextBox txtFaultLength;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.TextBox txtFaultStartPoint;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.GroupBox groupBox29;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.TextBox txtValue_Write;
        private System.Windows.Forms.TextBox txtValue_Read;
        private System.Windows.Forms.TextBox txtPoint_Write;
        private System.Windows.Forms.TextBox txtPoint_Read;
        private System.Windows.Forms.GroupBox groupBox28;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.Panel Panel运行界面;
        private System.Windows.Forms.Panel panel_运行界面左;
        private System.Windows.Forms.Label lblDeviceName;
        private System.Windows.Forms.TextBox txtShowBarcode;
        private System.Windows.Forms.Label label_Upload;
        private System.Windows.Forms.Label lblUploadStatus;
        private System.Windows.Forms.Label label_Device;
        private System.Windows.Forms.Label label_Board;
        private System.Windows.Forms.Label lblDeviceStatus;
        private System.Windows.Forms.Label lblDashboardStatus;
        private System.Windows.Forms.Label label_PLC;
        private System.Windows.Forms.Label lblPlcStatus;
        private System.Windows.Forms.Label label_Scan;
        private System.Windows.Forms.Label label_Validate;
        private System.Windows.Forms.Label lblScanBarcodeStatus;
        private System.Windows.Forms.Label lblValidationStatus;
        private System.Windows.Forms.GroupBox groupBox生产信息;
        private System.Windows.Forms.GroupBox groupBox30;
        private System.Windows.Forms.Button btnSaveRecipeConfig;
        private System.Windows.Forms.TextBox txtPointSets;
        private System.Windows.Forms.TextBox txtNameSets;
        private System.Windows.Forms.Panel panelDeviceName;
        private System.Windows.Forms.Label lblVersion;
        private MesDatasCore.DataGridViewDynamic dgvProductionIndex;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txtDefaultStyle;
        private System.Windows.Forms.Label label_ShowStyle;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.DataGridView dataGridView6;
        private System.Windows.Forms.GroupBox groupBox27;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage初始化设置;
        private System.Windows.Forms.TabPage tabPage13;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.GroupBox groupBox32;
        private System.Windows.Forms.ComboBox cboConnectType;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TabPage tabPage14;
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
        private System.Windows.Forms.TextBox txtResultPoint;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.TextBox txtEndPoint;
        private System.Windows.Forms.TextBox txtSecondLength;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.TextBox txtSecondPoint;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.TextBox txtStartPoint;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Button btnSavePlcPoint;
        private System.Windows.Forms.TabControl tabControl4;
        private System.Windows.Forms.TabPage tabPage15;
        private System.Windows.Forms.GroupBox groupBox40;
        private System.Windows.Forms.Button btnSavePrinterConfig;
        private System.Windows.Forms.CheckBox chkPlcControlPrint;
        private System.Windows.Forms.TextBox txtEndPoint_Print;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.TextBox txtStartPoint_print;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.ComboBox cboPrintMode;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.TabPage tabPage18;
        private System.Windows.Forms.Button btnAccessMES;
        private System.Windows.Forms.RichTextBox rtbMESInput;
        private System.Windows.Forms.RichTextBox rtbMESOutput;
        private System.Windows.Forms.TextBox txtViewStatus;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.TextBox textBox35;
        private System.Windows.Forms.GroupBox groupBox42;
        private System.Windows.Forms.Label lblReaderState;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblPlcAccess;
        private System.Windows.Forms.Label label139;
        private System.Windows.Forms.Label lblCurrentSelected;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.GroupBox groupBox43;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox txtBarcodeNumber;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label lblBarcodeContent;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.CheckBox chkBanLocalVerification;
        private System.Windows.Forms.GroupBox groupBox44;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.ComboBox cboPrinterType;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.TextBox txtPrinter_Port;
        private System.Windows.Forms.TextBox txtPrinter_IP;
        private System.Windows.Forms.Label lblConnectStatus;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Button btnConnectPrinter;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.GroupBox groupBox35;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button btnPrint_ZebraTest;
        private System.Windows.Forms.GroupBox groupBox36;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.ComboBox cboFileFormat;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label label122;
        private System.Windows.Forms.GroupBox groupBox37;
        private System.Windows.Forms.GroupBox groupBox38;
        private System.Windows.Forms.CheckBox chkUseFont;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtAfter;
        private System.Windows.Forms.CheckBox chkLoadModel;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.TextBox txtBefore;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.GroupBox groupBox39;
        private System.Windows.Forms.TextBox txtPrintCount;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSerialSpan;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.CheckBox chkAutoAddDate;
        private System.Windows.Forms.Label lblBarodeContent_Printer;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.TextBox txtSN_Printer;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.TextBox txtBarcodeNumber_Printer;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.GroupBox groupBox41;
        private System.Windows.Forms.Label lblPrintResultTips;
        private System.Windows.Forms.GroupBox groupBox45;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Button BoardCreateDataBaseBtn;
        private System.Windows.Forms.TextBox txtUseTimePoint;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.TextBox txtLineName;
        private System.Windows.Forms.TextBox txtThaiNameSets;
        private System.Windows.Forms.TextBox txtEnglishNameSets;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.TableLayoutPanel tlp_Status;
        private System.Windows.Forms.SplitContainer splitContainer_LR;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MesDatasCore.DataGridViewDynamic dgvResult1;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.RichTextBox rtbMesLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tlp运行界面右;
        private System.Windows.Forms.TableLayoutPanel tpl_生产面板;
        private System.Windows.Forms.ComboBox cboCurrentLanguage;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.CheckBox chkBindOrderNumber;
        private System.Windows.Forms.Button btnChangeWorkOrder;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtWorkOrder;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.TextBox txtFixtureBinding;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtProductModel;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.ComboBox cboBarcodeRule;
        private System.Windows.Forms.Label lblRecipeId;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.CheckBox chkReadRecipeId_PLC;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label lblLoginMode;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label lblRunningStatus;
        private System.Windows.Forms.TextBox txtDeviceName_Thai;
        private System.Windows.Forms.TextBox txtDeviceName_English;
        private System.Windows.Forms.Label label_ThaiName;
        private System.Windows.Forms.Label label_EnglishName;
        private System.Windows.Forms.Label label_version;
        private System.Windows.Forms.TableLayoutPanel tlp_Version;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabControl tabControl_UploadData;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MesDatasCore.DataGridViewDynamic dgvResult2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TabPage tabPage3;
        private MesDatasCore.DataGridViewDynamic dgvResult3;
        private System.Windows.Forms.TextBox txtResultPoint2;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox txtEndPoint2;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txtStartPoint2;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txt_LR1;
        private System.Windows.Forms.TextBox txt_LR2;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.CheckBox chkEnableSN;
        private System.Windows.Forms.TextBox txtResultPoint1;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.TextBox txtEndPoint1;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.TextBox txtStartPoint1;
        private System.Windows.Forms.Label label56;
        private MesDatasCore.DataGridViewDynamic dgvShowBarcode;
        private MesDatasCore.DataGridViewDynamic dgvTest;
        private System.Windows.Forms.TextBox txtSecondPoint1;
        private System.Windows.Forms.TextBox txtSecondPoint2;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox txtSecondLength2;
        private System.Windows.Forms.TextBox txtSecondLength1;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TextBox txtStationCount;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.TextBox tbxReaderDeviceID;
        private System.Windows.Forms.Button btnSearchReaderPort;
        private System.Windows.Forms.ComboBox cmbShowPort;
        private System.Windows.Forms.Label label_DeviceID;
        private System.Windows.Forms.Label label_Reader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
        private System.Windows.Forms.TableLayoutPanel tlp_运行状态;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblOperatePrompt;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TableLayoutPanel tlp_ProductResult;
        private System.Windows.Forms.Label lblProductResult;
        private System.Windows.Forms.Label lbl_Left;
        private System.Windows.Forms.Label lbl_Right;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel17;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel20;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel19;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel21;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel23;
        private System.Windows.Forms.TableLayoutPanel tlp启用看板;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel24;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel25;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel26;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel28;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel27;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel29;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel30;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel31;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel32;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel34;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel33;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel35;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel36;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel37;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel38;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel tlp_配方设置;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel39;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel40;
        private System.Windows.Forms.Button btnSaveAtSystemSetting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel41;
        private System.Windows.Forms.TextBox txtStationNameSets_Thai;
        private System.Windows.Forms.TextBox txtStationNameSets_English;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel42;
        private System.Windows.Forms.ComboBox cboWriteValue;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel43;
        private System.Windows.Forms.TextBox txtFixtutreNumber;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.TextBox txtFixtureOK;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.TextBox txtFixtureValidata;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.TextBox txtFixtureLength;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel44;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel45;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel46;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel48;
        private System.Windows.Forms.Button btnWriteValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel47;
        private System.Windows.Forms.ComboBox cboReadValue;
        private System.Windows.Forms.Button btnReadValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel49;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel50;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel51;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel52;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel53;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel54;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel55;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel56;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel57;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnShowPath;
        private System.Windows.Forms.Button btnChangePath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel58;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel59;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel60;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel62;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel61;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel63;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel22;
        private MesDatasCore.DataGridViewDynamic dgvRecipeManage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel64;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel65;
        private System.Windows.Forms.CheckBox chkBanLocalHistoricalData;
        private System.Windows.Forms.CheckBox chkBypassFixtureValidation;
        private System.Windows.Forms.CheckBox chkBanQRcodeValidation;
        private System.Windows.Forms.CheckBox chkBanNGDataVerify;
        private System.Windows.Forms.CheckBox chkBanRuleValidation;
        private System.Windows.Forms.CheckBox chkGenerateBarcode;
        private System.Windows.Forms.CheckBox chkAutoExit;
        private System.Windows.Forms.CheckBox chkAutoLaunch;
        private System.Windows.Forms.CheckBox chkUserBinding;
        private System.Windows.Forms.CheckBox chkAllowUploadContinuously;
        private System.Windows.Forms.CheckBox chkReadBarcodeSecondly;
        private System.Windows.Forms.CheckBox chkDoubleStation;
        private System.Windows.Forms.CheckBox chkLeftRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel66;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label81;
    }
}

