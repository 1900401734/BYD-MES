using HFrfid;
using HslCommunication;
using HslCommunication.ModBus;
using HslCommunication.Profinet.Keyence;
using HslCommunication.Profinet.Melsec;
using HslCommunication.Profinet.Omron;
using INIFile;
using MathNet.Numerics.Distributions;
using MesDatas.DatasDataGridView;
using MesDatas.DatasModel;
using MesDatas.DatasServer;
using MesDatas.Entity;
using MesDatas.MESModel;
using MesDatas.SqlConverter;
using MesDatas.Utiey;
using MesDatas.Utility.PrintersFileChecker;
using MesDatas.Utility.ResourcesLaguage;
using MesDatasCore;
using Microsoft.VisualBasic;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using NLog;
using NPOI.SS.Formula;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using Seagull.BarTender.Print;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Interop;
using System.Xml.Linq;
using UltimateFileSoftwareUpdate.UpdateModel;
using 工艺部信息化组;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace MesDatas
{
    public partial class Form1 : Form, IMesDatasBase
    {
        public static int iOperCount = 0;
        public static System.Timers.Timer timer;

        #region ----------------- 登录状态相关的属性与方法 -----------------

        private int MES { get; set; }
        public void SetMES(int strText)
        {
            MES = strText;
        }

        /// <summary>
        /// 登录模式（联机或单机）
        /// </summary>
        public string LoginMode { get; private set; }
        /// <summary>
        /// 0=联机  1=单机
        /// </summary>
        private int OffLineType { get; set; }
        public void SetoffLineType(int strText)
        {
            OffLineType = strText;
            LoginMode = OffLineType == 0 ? "联机" : "单机";
        }

        public string LoginMethod { get; private set; }
        private int CheckCard { get; set; }
        public void Setcheckcard(int strText)
        {
            CheckCard = strText;
            LoginMethod = CheckCard == 0 ? "密码登录" : "刷卡登录";
        }

        private int Access_take { get; set; }
        public void Setaccess_take(int strText)
        {
            Access_take = strText;
        }

        /// <summary>
        /// 用户权限
        /// </summary>
        private int Access { get; set; }
        public void Setaccess(int strText)
        {
            Access = strText;
            GetAccessName(Access);
        }
        /// <summary>
        /// 权限名
        /// </summary>
        public string AccessName { get; set; }
        private void GetAccessName(int access)
        {
            switch (access)
            {
                case 1:
                    AccessName = "OP";
                    break;
                case 2:
                    AccessName = "PE";
                    break;
                case 3:
                    AccessName = "ADM";
                    break;
                case 4:
                    AccessName = "DEV";
                    break;
                case 5:
                    AccessName = "QE";
                    break;
                case 6:
                    AccessName = "ME";
                    break;
                default:
                    AccessName = string.Empty;
                    break;
            }
        }

        /// <summary>
        /// 登录用户（工号）
        /// </summary>
        private string LoginUser { get; set; }
        public void SetloginUser(string strText)
        {
            LoginUser = strText;
        }

        /// <summary>
        /// 登录名称（姓名）
        /// </summary>
        private string LoginName { get; set; }
        public void SetloginName(string strText)
        {
            if (strText == null)
            {
                LoginName = "开发者";
            }
            else
            {
                LoginName = strText;
            }
        }

        /// <summary>
        /// 登录密码
        /// </summary>
        private string LoginPwd { get; set; }
        public void SetloginPwd(string strText)
        {
            LoginPwd = strText;
        }

        #endregion

        public string[] Parameter_txt = new string[10000];

        List<string> list = null;
        List<string> beatList = null;       // 节拍
        List<string> maxList = null;        // 上限
        List<string> minList = null;        // 下限
        List<string> resultList = null;     // 结果
        List<string> workstNameList = null;

        mdbDatas mdb = null;
        public string WorkOrder;
        public bool 产品结果;
        public int Num = 0;
        public string barcodeInfo = null;
        string[] Value = new string[10000];
        string[] Parameter_Model = new string[25000];

        Assembly asm = Assembly.GetExecutingAssembly();
        ResourceManager resources = null;
        int LanguageId = 0;

        public Form1()
        {
            string language = Properties.Settings.Default.DefaultLanguage;
            LanguageResour languageResour = new LanguageResour();
            if (language == "zh-CN")
            {
                LanguageId = 0;
                resources = new ResourceManager("MesDatas.Language_Resources.language_Chinese", asm);
            }
            else if (language == "en-US")
            {
                LanguageId = 1;
                resources = new ResourceManager("MesDatas.Language_Resources.language_English", asm);
            }
            else if (language == "th-TH")
            {
                LanguageId = 2;
                resources = new ResourceManager("MesDatas.Language_Resources.language_Thai", asm);
            }

            this.WindowState = FormWindowState.Maximized;

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            //this.skinEngine1.SkinFile = "Calmness.ssk";
            //sw.Start();
            //listViewToolsLib.RootPath = Application.StartupPath + @"\\Libs";
            //listViewToolsLib.UpdateTools();

            // 开启监听键盘和鼠标操作
            Application.AddMessageFilter(new MyIMessageFilter());

            // 实时更新当前时间
            new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        lblCurrentTime.BeginInvoke(new MethodInvoker(() =>
                            lblCurrentTime.Text = DateTime.Now.ToString()));
                    }
                    catch { }
                    Thread.Sleep(1000);
                }
            })
            { IsBackground = true }.Start();
        }

        string[] sequenceNum = new string[] { };    // 工位序号
        string[] testItems = new string[] { };      // 测试项目名称
        string[] actualValue = new string[] { };    // 实际值点位
        string[] maxValue = new string[] { };       // 上限点位
        string[] minValue = new string[] { };       // 下限点位
        string[] beat = new string[] { };           // 节拍点位
        string[] testResult = new string[] { };     // 结果点位
        string[] unitName = new string[] { };       // 单位
        string[] standardValue = new string[] { };  // 标准值点位

        string[] stationName = new string[] { };    // 工位名称
        string[] kpisPointSets = new string[] { };  // 生产指标点位集合
        string[] kpisNameSets = new string[] { };   // 生产指标名称集合

        List<BarcodeVefictn> barcodeVefictnList = new List<BarcodeVefictn>();

        // 字典用于存储每个CheckBox的初始状态
        private Dictionary<System.Windows.Forms.CheckBox, bool> checkBoxStates = new Dictionary<System.Windows.Forms.CheckBox, bool>();

        Logger loggerConfig = LogManager.GetLogger("ArgumentConfigLog");
        Logger loggerAccount = LogManager.GetLogger("AccountManageLog");

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            WhiteNightShift.GetShift(DateTime.Now);

            SearchPort();               // 初始化刷卡器端口

            SYS_Socket_Mo();            // 读取看板设置参数设置

            LoadParameter_MES();        // 读取联机参数设置

            SYS_BOARD();                // 读取PLC点位集合

            LoadSystemConfigArgument(); // 读取系统设置

            BtnRefreshDirectory_Click(null, null);  // 加载本地数据源

            PLCBarQRCode();             // 条码验证表格

            barcodeVefictnList = BarcodeVefictnServer.GetBarcodeVefictnList(LanguageId);

            this.lblDeviceName.Text = txtDeviceName.Text;   // 写机台名称
            FontStyle fontStyle = FontStyle.Bold;           // 设置字体粗细
            float size = 42F;                               // 字体大小
            changeLabelFont(lblDeviceName, size, fontStyle);// 内字体随着字数的增加而自动减小

            if (txtStationNameSets.Text.Length > 0)
            {
                stationName = txtStationNameSets.Text.ToString().Split(new char[] { '|' }); // 工位名称
                kpisPointSets = txtPointSets.Text.ToString().Split(new char[] { '|' });     // 生产指标点位集合
                kpisNameSets = txtNameSets.Text.ToString().Split(new char[] { '|' });       // 生产指标名称集合
            }

            btnRefreshUser_Click(null, null);  // 用户管理刷新按钮

            GetPrinter();             // 加载打印机

            LoadPrinterConfig();    // 加载打印信息

            UpLoginInfo();          // 修改最后登录时间和次数

            try
            {
                ConiferFile coniferFile = ConiferFile.GetJson();
                lblVersion.Text = "版本号: " + coniferFile.Version;
            }
            catch { }
        }

        /// <summary>
        /// 窗体加载后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            //根据登入信息  分配主界面菜单
            AssignUI();

            // 初始化 CheckBox 的初始状态
            InitializeCheckBoxStates(this.Controls);
            // 配置Nlog全局变量
            LogManager.Configuration.Variables["LoginName"] = LoginName;
            // 记录登录信息
            string loginInfo = $"【用户登录】\n工号：{LoginUser} | 姓名：{LoginName} | 权限：{AccessName} | 登录模式：{LoginMode} | 登录方式：{LoginMethod}";
            loggerAccount.Trace(loginInfo);

            // 存储和加载故障信息
            try
            {
                string jsonStr = File.ReadAllText(pathText);
                if (!string.IsNullOrWhiteSpace(jsonStr))
                {
                    faultsMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
                }
            }
            catch { }
            if (faultsMap == null)
            {
                faultsMap = new Dictionary<string, string>();
            }

            // 用户登录验证
            if (OffLineType == 0)
            {
                VarifyUserLogin_MES(null, null);
            }
            else
            {
                lblRunningStatus.ForeColor = G;
                lblRunningStatus.Text = resources.GetString("UserCheck");    // 单机用户验证成功
                lblOperatePrompt.Text = resources.GetString("scanning");     // 等待扫描条码
            }

            ConnectDashboard(null, null);  // 连接看板

            TCP_Connect(null, null);       // 连接PLC

            taskProcess_MES = new Task(Process_MES);// 更新PLC状态指示灯 & 向PLC反馈看板连接状态
            taskProcess_MES.Start();

            Model_Read_Other();                     // 加载产品型号 

            Process_Offline();                      // 根据状态写模式

            workstNameList = new List<string>();
            if (sequenceNum.Length > 0)
            {
                workstNameList = CodeNum.WorkIDName(sequenceNum, stationName);
            }

            InsertTable(null, null);
            rtbProductLog.Clear();
            UTYPE.SelectedIndex = 0;

            // 为 ADM 权限增加定时器
            if (Access == 3)
            {
                timer = new System.Timers.Timer();
                timer.Elapsed += Timer_Elapsed;
                timer.Enabled = true;
                timer.Interval = 1800000;
                timer.Start();
            }

            // 初始状态为待机
            lblProductResult.Text = resources.GetString("label_Value");
            lblProductResult.ForeColor = Color.Black;
            lblProductResult.BackColor = Color.White;

            // 联机用户验证失败,直接返回
            if (OffLineType == 0 && isMesLoginSuccessful == false)
            {
                return;
            }
            if (OffLineType == 1)
            {
                txtWorkOrder.Text = "111111111111";
            }
            else
            {
                txtWorkOrder.Text = Interaction.InputBox(resources.GetString("InputBox"), resources.GetString("InputBoxName"), "", 100, 100);

                // 超过3次自动退出
                for (int i = 1; i <= 5; i++)
                {
                    if (string.IsNullOrWhiteSpace(txtWorkOrder.Text))
                    {
                        if (i == 4)
                        {
                            Form1_FormClosed(null, null);
                        }
                        txtWorkOrder.Text = Interaction.InputBox(resources.GetString("InputBox") + i + resources.GetString("InputBox1"), resources.GetString("InputBoxName"), "", 100, 100);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            barcodeInfo = "1";

            taskProcesplc = new Task(ProcessPlc_ReadBarcode);   // 读取条码
            taskProcesplc.Start();
            taskProcesplc1 = new Task(ProcessPlc_ReadValue);    // 读取工单号等生产数据
            taskProcesplc1.Start();
            taskMinMaxplc = new Task(Bind_MaxMinValue);         // 绑定上下值数据
            taskMinMaxplc.Start();
            GetMinMaxplc = new Task(Get_MaxMinValue);           // 读取上下值数据
            GetMinMaxplc.Start();
            OpenReader_Click(null, null);              // 连接读卡器

            if (chkPlcControlPrint.Checked)
            {
                taskProcess_ZPL = new Task(PrintZPL_Click);     // PLC控制打印
                taskProcess_ZPL.Start();
            }

            sedbool = true;
        }

        /// <summary>
        /// 递归地初始化所有 CheckBox 的状态，包括嵌套在其他容器中的 CheckBox
        /// </summary>
        /// <param name="controls"></param>
        private void InitializeCheckBoxStates(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                // 如果是CheckBox，记录其状态并绑定CheckedChanged事件
                if (control is System.Windows.Forms.CheckBox cbx)
                {
                    if (!checkBoxStates.ContainsKey(cbx))
                    {
                        checkBoxStates[cbx] = cbx.Checked;
                        chkBindWorkOrder.CheckedChanged += CheckBox_CheckedChanged;             // 勾选绑定工单
                        chkReadPName.CheckedChanged += CheckBox_CheckedChanged;                 // 读取PLC
                        checkBox3.CheckedChanged += CheckBox_CheckedChanged;                    // 使用文字
                        checkBox4.CheckedChanged += CheckBox_CheckedChanged;                    // 读取PLC型号
                        chkEnableDashboard.CheckedChanged += CheckBox_CheckedChanged;           // 启用看板
                        chkPlcControlPrint.CheckedChanged += CheckBox_CheckedChanged;           // PLC控制打印
                        chkBypassBarcodeValidation.CheckedChanged += CheckBox_CheckedChanged;   // 屏蔽本地条码验证
                        chkReadBarcodeSecondly.CheckedChanged += CheckBox_CheckedChanged;       // 二次读条码
                        chkBypassFixtureValidation.CheckedChanged += CheckBox_CheckedChanged;   // 屏蔽本地扫工装验证
                        chkBypassQRcodeValidation.CheckedChanged += CheckBox_CheckedChanged;    // 屏蔽本地二维码验证
                        chkBypassLocalNgHistoricalData.CheckedChanged += CheckBox_CheckedChanged;// 屏蔽本地NG历史数据
                        chkBanLocalHistoricalData.CheckedChanged += CheckBox_CheckedChanged;    // 屏蔽本地历史数据
                    }
                }
                // 如果是容器控件，递归调用此方法
                else if (control is ContainerControl || control is Panel || control is System.Windows.Forms.GroupBox || control is TabPage || control is TabControl)
                {
                    InitializeCheckBoxStates(control.Controls);
                }
            }
        }

        /// <summary>
        /// CheckedChanged 事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.CheckBox cbx)
            {
                // 检查字典中是否存在对应的键
                if (checkBoxStates.TryGetValue(cbx, out bool initialState))
                {
                    // 当状态发生变化且与初始状态不同
                    if (cbx.Checked != initialState)
                    {
                        // 记录日志
                        string state = cbx.Checked ? "启用" : "已关闭";
                        loggerConfig.Trace($"【{cbx.Text}】{state}");

                        // 更新字典中的状态
                        checkBoxStates[cbx] = cbx.Checked;
                    }
                }
                else
                {
                    loggerConfig.Warn($"未能在字典中找到 CheckBox '{cbx.Name}' 的初始状态。");
                }
            }
        }

        // 达到时间间隔发生的方法
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            iOperCount++;
            if (iOperCount >= 1)
            {
                Console.WriteLine("30分钟未动作程序退出！");
                Environment.Exit(0);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //LineModel_Write();
            //IsRunning = false;
            //StopTools();
            //dataGridView1.updateData -= ProcCompleteData;
            //SaveParam();
            //关闭标签应用，并且不保存
            if (btApp != null)
            {
                btApp.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges);
            }
            try
            {
                Environment.Exit(0);
            }
            catch (Exception)
            {

            }

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //  SetLayout();
        }

        #region ------------ "tools manager" ------------

        private void StopTools()
        {
            foreach (KeyValuePair<string, IToolBase> tool in mTools)
            {
                tool.Value.IsRunLoop = false;
            }
        }

        private Dictionary<string, IToolBase> mTools = new Dictionary<string, IToolBase>();

        Dictionary<string, IToolBase> IMesDatasBase.Tools
        {
            get
            {
                return mTools;
            }
            set
            {
                mTools = value;
            }
        }

        private SendCommand SendComamandTest;

        public SendCommand sendCommand
        {
            get
            {
                return SendComamandTest;
            }
            set
            {
                SendComamandTest = value;
            }
        }
        // delegate IToolBase delegateLoadTool(string path);

        private IToolBase LoadTool(string toolPath)
        {
            // @"D:\YCwork\项目文件\2022\21.深圳比亚迪采集软件\MesDatas\ToolSample\bin\Debug\ToolSample.dll"
            try
            {
                Assembly ass = Assembly.LoadFrom(toolPath);
                var wormMain = ass.GetTypes().FirstOrDefault(m => m.GetInterface(typeof(IToolBase).Name) != null);
                var tmpObj = (IToolBase)Activator.CreateInstance(wormMain);
                if (tmpObj != null)
                {
                    tmpObj.ToolInit();
                    tmpObj.receiveChanged += EnqueueInteracrive;
                    tmpObj.MesDatasMain = this;
                    return tmpObj;
                }
                return null;

            }
            catch
            {
                return null;
            }

        }

        #endregion

        #region  ------------ "EnqueueInteracrive" ------------

        private void EnqueueInteracrive(InteractiveEventArgs e)
        {
            Invoke(new Action(() =>
            {
                lock (lockQueue)
                {
                    InterractiveQueue.Enqueue(e);
                }
            }));
        }

        private Queue<InteractiveEventArgs> InterractiveQueue = new Queue<InteractiveEventArgs>();
        Task taskProcesplc = null;
        Task GetMinMaxplc = null;
        Task taskMinMaxplc = null;
        Task taskProcesplc1 = null;
        Task taskPLCNFC = null;
        Task taskProcess = null;
        Task taskProcess_Offline = null;
        Task taskProcess_MES = null;
        Task taskProcess_ZPL = null;
        public bool IsRunningplc = true;
        bool IsRunning = true;
        bool IsRunningplc_ReadCode = true;
        bool IsRunningplc_ReadValue = true;
        bool IsRunningplc_ReadMaxMin = true;
        bool IsRunningplc_MES = true;
        bool IsRunningplc_NFC = true;
        bool IsRunningplc_tabPage = true;
        bool IsRunningPZL = true;
        object lockQueue1 = new object();
        object lockQueue = new object();

        public static string path4 = System.AppDomain.CurrentDomain.BaseDirectory + "SystemDateBase.mdb";
        public static string pathText = System.AppDomain.CurrentDomain.BaseDirectory + "logfault.txt";
        public static string userFileuRL = "D:\\BYD_Users\\Users_Data.MDB";

        /// <summary>
        /// 更新PLC状态指示灯 & 向 PLC 反馈看板连接状态
        /// </summary>
        private void Process_MES()
        {
            while (IsRunningplc_MES)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (isPlcConnected == true)
                    {
                        lblPlcStatus.ForeColor = Color.Green;
                        try
                        {
                            // 向 PLC 反馈看板连接状态
                            if (!string.IsNullOrEmpty(deviceInfo.ViewStatus))
                            {
                                if (isDashboardConnected)
                                {
                                    KeyenceMcNet.Write(deviceInfo.ViewStatus, 1);
                                    //Console.WriteLine("看板已连接");
                                }
                                else
                                {
                                    KeyenceMcNet.Write(deviceInfo.ViewStatus, 0);
                                    //Console.WriteLine("尚未连接到看板");
                                }
                            }
                        }
                        catch (Exception ex) { }
                    }
                    else
                    {
                        lblPlcStatus.ForeColor = Color.Red;
                    }

                })).AsyncWaitHandle.WaitOne();

                Thread.Sleep(1000);
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 更新登录模式，用户登录信息
        /// </summary>
        private void Process_Offline()
        {
            if (OffLineType == 1)
            {

                lblLoginMode.Text = resources.GetString("loginMode1");  // 离线
                lblCurrentUser.Text = $"{LoginUser} ({LoginName})";

            }
            else if (OffLineType == 0)
            {
                lblLoginMode.Text = resources.GetString("loginMode");   // 在线
                lblCurrentUser.Text = $"{LoginUser} ({LoginName})";
            }
        }

        /// <summary>
        /// 读取条码
        /// </summary>
        private void ProcessPlc_ReadBarcode()
        {
            while (IsRunningplc_ReadCode)
            {
                Button19_Click(null, null);
            }
            Thread.Sleep(50);
            Application.DoEvents();
        }

        /// <summary>
        /// 读取并上传生产数据
        /// </summary>
        private void ProcessPlc_ReadValue()
        {
            while (IsRunningplc_ReadValue)
            {
                Button20_Click(null, null);
            }
            Thread.Sleep(50);
            Application.DoEvents();
        }

        /// <summary>
        /// 根据用户权限分配界面
        /// </summary>
        private void AssignUI()
        {
            if (Access_take == 1)
            {
                if (Access == 1)//操作员
                {
                    this.tabPage2.Parent = this.tabControl1;
                    this.tabPage3.Parent = this.tabControl1;
                    this.tabPage4.Parent = null;
                    this.tabPage5.Parent = null;
                    this.tabPage6.Parent = null;
                    this.tabPage7.Parent = null;
                    this.tabPage8.Parent = this.tabControl1;
                    this.tabPage9.Parent = null;
                }
                else if (Access == 2)//工艺工程师
                {
                    this.tabPage2.Parent = this.tabControl1;
                    this.tabPage3.Parent = this.tabControl1;
                    this.tabPage4.Parent = this.tabControl1;
                    this.tabPage5.Parent = this.tabControl1;
                    this.tabPage6.Parent = this.tabControl1;
                    this.tabPage7.Parent = null;
                    this.tabPage8.Parent = this.tabControl1;
                    this.tabPage9.Parent = this.tabControl1;
                }
                else if (Access == 3)//超级用户
                {
                    this.tabPage2.Parent = this.tabControl1;    // 生产日志
                    this.tabPage3.Parent = this.tabControl1;    // 用户管理
                    this.tabPage4.Parent = this.tabControl1;    // 打印设置
                    this.tabPage5.Parent = this.tabControl1;    // MES参数
                    this.tabPage6.Parent = this.tabControl1;    // 看板设置
                    this.tabPage7.Parent = this.tabControl1;    // 系统设置
                    this.tabPage8.Parent = this.tabControl1;    // 历史数据
                    this.tabPage9.Parent = this.tabControl1;    // 配方设置
                }
                else if (Access == 4)//开发者
                {
                    this.tabPage2.Parent = this.tabControl1;
                    this.tabPage3.Parent = this.tabControl1;
                    this.tabPage4.Parent = this.tabControl1;
                    this.tabPage5.Parent = this.tabControl1;
                    this.tabPage6.Parent = this.tabControl1;
                    this.tabPage7.Parent = this.tabControl1;
                    this.tabPage8.Parent = this.tabControl1;
                    this.tabPage9.Parent = this.tabControl1;
                }
                else if (Access == 5)//品质
                {
                    this.tabPage2.Parent = this.tabControl1;
                    this.tabPage3.Parent = this.tabControl1;
                    this.tabPage4.Parent = null;
                    this.tabPage5.Parent = null;
                    this.tabPage6.Parent = null;
                    this.tabPage7.Parent = null;
                    this.tabPage8.Parent = this.tabControl1;
                    this.tabPage9.Parent = null;
                }
                else if (Access == 6)//设备
                {
                    this.tabPage2.Parent = this.tabControl1;
                    this.tabPage3.Parent = this.tabControl1;
                    this.tabPage4.Parent = this.tabControl1;
                    this.tabPage5.Parent = null;
                    this.tabPage6.Parent = null;
                    this.tabPage7.Parent = this.tabControl1;
                    this.tabPage8.Parent = this.tabControl1;
                    this.tabPage9.Parent = null;
                }
                Access_take = 0;
            }
        }

        #region ------------ Label内字体随着字数的增加而自动减小，Label大小不变 ------------

        public Label changeLabelFont(Label label, float size, FontStyle fontStyle)
        {
            Color color = label.ForeColor;
            //FontStyle fontStyle = FontStyle.Bold;
            System.Drawing.FontFamily ff = new System.Drawing.FontFamily(label.Font.Name);
            //float size = 42F;
            string content = label.Text;
            //初始化label状态
            label.Font = new Font(ff, size, fontStyle, GraphicsUnit.Point);
            while (true)
            {
                //获取当前一行能放多少个字======================================================
                //1、获取label宽度
                int labelwidth = label.Width;
                //2、获取当前字体宽度
                Graphics gh = label.CreateGraphics();
                SizeF sf = gh.MeasureString("0", label.Font);
                float fontwidth = sf.Width;
                //3、得到一行放几个字
                int OneRowFontNum = (int)((double)labelwidth / (double)fontwidth);


                //判断当前的Label能放多少列======================================================
                //1、获取当前字体的高度
                float fontheight = sf.Height;
                //2、获取当前label的高度
                int labelheight = label.Height;
                //3、得到当前label能放多少列
                int ColNum = (int)((double)labelheight / (double)fontheight);

                //获取当前字符串需要放多少列======================================================
                var NeedColNum = Math.Ceiling((double)content.Length / (double)OneRowFontNum);

                //如果超出范围，则缩小字体，然后返回再判断一次===================================
                if (ColNum <= NeedColNum)
                {
                    size -= 0.25F;
                    label.Font = new Font(ff, size, fontStyle, GraphicsUnit.Point);
                }
                else
                {
                    break;
                }
            }

            return label;
        }

        #endregion

        string year = DateTime.Now.Year.ToString();
        string month = DateTime.Now.Month.ToString();
        string day = DateTime.Now.Day.ToString();
        string hour = DateTime.Now.Hour.ToString();
        string minute = DateTime.Now.Minute.ToString();
        string second = DateTime.Now.Second.ToString();
        string millisecond = DateTime.Now.Millisecond.ToString();

        private void LogMsg(string msg)
        {
            this.Invoke(new Action(() =>
            {
                if (rtbProductLog.TextLength > 50000)
                {
                    rtbProductLog.Clear();
                }
                rtbProductLog.AppendText($"{hour}{minute}{second}{millisecond}:{msg}\r\n");
                rtbProductLog.ScrollToCaret();
                SaveCsvLog(msg);
            }));
        }

        public void SaveCsvLog(string log)
        {
            try
            {
                if (System.IO.Directory.Exists("D:\\Log") == false)
                {
                    System.IO.Directory.CreateDirectory("D:\\Log");
                }
                StringBuilder DataLine = new StringBuilder();

                string strT = $"{hour}时{minute}分{second}秒";

                //列标题
                // i.Append(log);
                //行数据
                DataLine.Append(strT + ":" + log);
                string FileName = $"{year}-{month}-{day}";
                string FilePath = $@"D:\Log\{FileName}.CSV";

                if (System.IO.File.Exists(FilePath) == false)
                {
                    System.IO.StreamWriter stream = new System.IO.StreamWriter(FilePath, true, Encoding.UTF8);
                    stream.WriteLine(DataLine);
                    stream.Flush();
                    stream.Close();
                    stream.Dispose();
                }
                else
                {
                    System.IO.StreamWriter stream = new System.IO.StreamWriter(FilePath, true, Encoding.UTF8);
                    stream.WriteLine(DataLine);
                    stream.Flush();
                    stream.Close();
                    stream.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return;

        }

        #endregion

        #region  ------------ "Parameters & Tools Manager" ------------

        private void SaveTools()
        {

            string tools = null;
            foreach (KeyValuePair<string, IToolBase> tool in mTools)
            {
                tools += tool.Key + "_";
            }
            if (tools != null)
            {
                tools.TrimEnd('_');
                WriteString("Tools", "ToolList", tools);

                foreach (KeyValuePair<string, IToolBase> tool in mTools)
                {
                    tool.Value.SaveParameters();
                }
            }

        }

        private void LoadParam()
        {
            try
            {
                //加载工具
                //加载Config

                //CKL.Checked = Convert.ToBoolean(ReadString("工作模式", "是否联机"));
                //WLCombox.SelectedIndex = Convert.ToInt16(ReadString("物料索引号", "物料"));

                //Laser_add = Convert.ToInt32(ReadString("镭射批号数据", "批号"));
                //LaserString = INIClass.INIGetStringValue(filepath, "镭射字符判定", "记忆字符");


                //plcAddr.Text = ReadString("地址", "PLCIP");
                //plcPort.Text = ReadString("地址", "PLC端口");

                //ccdAddr.Text = ReadString("地址", "ccdIP");
                //ccdPort.Text = ReadString("地址", "ccd端口");

                //prsAddr.Text = ReadString("地址", "扭力IP");
                //prsPort.Text = ReadString("地址", "扭力端口");
                //MesUpdateCmd.Text = ReadString("Commands", "MesUpdateCmd");
                //UserCheckCmd.Text = ReadString("Commands", "UserCheckCmd");
                //OfflineSaveCmd.Text = ReadString("Commands", "OfflineSaveCmd");

                //GridCmds.LoadCacheDataFromCsv();




            }
            catch (Exception e)
            {
                // ShowLog(e.ToString());
            }

        }

        private void SaveParam()
        {
            //WriteString("物料索引号", "物料", WLCombox.SelectedIndex.ToString());
            //WriteString("工作模式", "是否联机", CKL.Checked.ToString());

            //WriteString("地址", "PLCIP", plcAddr.Text);
            //WriteString("地址", "PLC端口", plcPort.Text);

            //WriteString("Commands", "MesUpdateCmd", MesUpdateCmd.Text);
            //WriteString("Commands", "UserCheckCmd", UserCheckCmd.Text);
            //WriteString("Commands", "OfflineSaveCmd", OfflineSaveCmd.Text);


            SaveTools();
            //GridCmds.SaveCacheDateToCSV();

        }

        #endregion

        private string filepath = Application.StartupPath + "\\Config.ini";

        private string ReadString(string section, string key)
        {
            return INIClass.INIGetStringValue(filepath, section, key);
        }

        private bool WriteString(string section, string key, string value)
        {
            bool b;
            // lock (lockFileWrite)
            {
                b = INIClass.INIWriteValue(filepath, section, key, value);
            }


            return b;
        }

        object objLockLog = new object();

        #region ---------- 数据查询 ----------

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            // DateToAccess();
            // dataGridViewDynamic1.ResetGrid(true);
            //datatable();
            chaxun();
            if (pager != null)
            {
                pager.fenye();//分页
                PageLoad();//显示分页数据
                           //dataset();
            }
        }

        OleDbDataAdapter adp;

        private cToolBase cTool = null;

        public void SetTool(cToolBase Tool)
        {
            cTool = Tool;
        }

        private string ReadString(string key)
        {
            return cTool.ReadString(cTool.sName, key);
        }

        private void WriteString(string key, string value)
        {
            cTool.WriteString(cTool.sName, key, value);
        }

        public void LoadParameter()
        {
            // ReadString
            //textBoxPath.Text = ReadString("Path");
            //textBoxCmd.Text = ReadString("Command");
        }

        public void SaveParameter()
        {
            // WriteString
            //WriteString("Path", textBoxPath.Text);
            //WriteString("Command", textBoxCmd.Text);
        }

        /// <summary>
        /// 查询
        /// </summary>
        public void chaxun()
        {
            DateTime times_Start = dateTimePicker1.Value;
            DateTime times_End = dateTimePicker2.Value;
            //times_Start= times_Start.ToString("yyyy年MM月dd日 HH:mm:ss");
            // times_End= times_Start.ToString("yyyy年MM月dd日 HH:mm:ss");
            string times_Start_string = times_Start.ToString("yyyy年MM月dd日 HH:mm:ss");
            string times_End_string = times_End.ToString("yyyy年MM月dd日 HH:mm:ss");//dateTimePicker2.Value.ToString("yyyy/MM/dd").ToString();
            DateTime times_Start_Sub = DateTime.Parse(times_Start_string);
            DateTime times_End_Sub = DateTime.Parse(times_End_string);
            if (textBoxPath.Text.Length < 1)
            {
                MessageBox.Show("请先选择左侧数据源！");
                return;
            }
            string dataBaseUrl = textBoxPath.Text + ".mdb";
            mdb = new mdbDatas(dataBaseUrl);

            StringBuilder sql = new StringBuilder();

            sql.Append("select * from [Sheet1] where CDate(Format(测试时间,'yyyy/MM/dd HH:mm:ss')) between  #" + times_Start_Sub + "#" + " and #" + Convert.ToDateTime(times_End_Sub) + "# ");

            if (textBox_Code.Text != "")
            {
                sql.Append("and 条码 like '" + textBox_Code.Text + "%'");
            }
            if (textBox1.Text != "")
            {
                sql.Append("and 产品 = '" + textBox1.Text + "' ");
            }

            DataTable table = mdb.Find(sql.ToString());
            //tablelist_date(dataBaseUrl);
            //dataGridViewDynamic1.SetDataTable(table);
            // dataGridViewDynamic1.DataSource = table;
            pager = new Pager(table);//分页
            mdb.CloseConnection();
        }

        public void tablelist_date(string url)
        {
            mdb = new mdbDatas(url);
            var sql1 = "select * from [Sheet1]";
            DataTable dt = mdb.Find(sql1);
            if (dt.Columns.Count > 0)
            {

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dataGridViewDynamic1.AddHeader(dt.Columns[i].ColumnName + "\t");
                }
            }
            mdb.CloseConnection();

        }

        /// <summary>
        /// 返回datatable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void datatable()
        {
            string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + textBoxPath.Text + ";Persist Security Info=True;Jet OLEDB:Database Password=byd;User Id=admin";

            OleDbParameter[] pars = new OleDbParameter[] {
                new OleDbParameter("@column1",textBox_Code.Text),
                new OleDbParameter("@column2",dateTimePicker1.ToString())
            };

            var sql = "select * from HCBI开关组 where 条码 = @column1 and 测试时间 = @column2";
            // 条码,测试人,测试时间,测试结果
            DataTable table = AccessHelper.ExecuteDataTable(conn, sql, pars);

            dataGridViewDynamic1.SetDataTable(table);

            //foreach (DataRow row in table.Rows)
            //{
            //    foreach (DataColumn column in table.Columns)
            //    {
            //        listBox1.Text+= (row[column] + "\t");
            //    }
            //}
        }

        /// <summary>
        /// 返回dataSet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dataset()
        {
            string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + textBoxPath.Text + ";Persist Security Info=True;Jet OLEDB:Database Password=byd;User Id=admin";
            OleDbParameter[] pars = new OleDbParameter[] {
                new OleDbParameter("@column1",textBox_Code.Text),
                //new OleDbParameter("@column2",tbx_Vaule.Text)
            };
            var sql = "select 条码,测试人,测试时间 from HCBI开关组 where 条码 = @column1 "; //and 测试节拍 = @column2
            DataTable table = AccessHelper.ExecuteDataTable(conn, sql, pars);
            OleDbParameter[] pars1 = new OleDbParameter[] {
                new OleDbParameter("@column1",textBox_Code.Text)
            };
            var sql1 = "select 条码,测试人,测试时间 from HCBI开关组 where 条码 = @column1 ";
            DataSet ds = AccessHelper.ExecuteDataSet(conn, sql1, pars1);

            foreach (DataTable tb in ds.Tables)
            {
                foreach (DataColumn col in tb.Columns)
                {
                    dataGridViewDynamic1.AddHeader(col.ColumnName + "\t");
                }

                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn col in table.Columns)
                    {
                        //(row[col] + "\t");
                    }

                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SaveParameter();
        }

        #endregion

        #region ---------- MES上传 ----------

        private string ip => textBox_ip.Text;
        private string port => textBox_port.Text;
        private string timeout => textBox_timeout.Text;
        private string nccode => textBox_nccode.Text;
        private string operation => textBox_opration.Text;
        private string user => textBox_user.Text;
        private string password => textBox_password.Text;
        private string url => textBox_url.Text;
        private string site => textBox_site.Text;
        private string resource => textBox_resource.Text;

        /// <summary>
        /// 加载MES联机参数到系统
        /// </summary>
        public void LoadParameter_MES()
        {
            mdb = new mdbDatas(path4);
            DataTable table1 = mdb.Find("select * from SytemInfo where ID = '1'");
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                for (int j = 0; j < table1.Columns.Count; j++)
                {
                    textBox_ip.Text = table1.Rows[i]["IP"].ToString();
                    textBox_port.Text = table1.Rows[i]["Port"].ToString();
                    textBox_timeout.Text = table1.Rows[i]["Timeout"].ToString();
                    textBox_nccode.Text = table1.Rows[i]["NcCode"].ToString();
                    textBox_opration.Text = table1.Rows[i]["Opration"].ToString();
                    textBox_password.Text = table1.Rows[i]["Password"].ToString();
                    textBox_resource.Text = table1.Rows[i]["Resource"].ToString();
                    textBox_site.Text = table1.Rows[i]["Site"].ToString();
                    textBox_url.Text = table1.Rows[i]["Url"].ToString();
                    textBox_user.Text = table1.Rows[i]["User"].ToString();
                }
            }
            mdb.CloseConnection();
        }

        /// <summary>
        /// 保存 MES 联机参数到数据库
        /// </summary>
        public void SaveParameter_MES()
        {
            SytemInfoEntity infoEntity = new SytemInfoEntity();
            infoEntity.IP = textBox_ip.Text;
            infoEntity.Port = textBox_port.Text;
            infoEntity.Timeout = textBox_timeout.Text;
            infoEntity.NcCode = textBox_nccode.Text;
            infoEntity.Opration = textBox_opration.Text;
            infoEntity.Password = textBox_password.Text;
            infoEntity.Resource = textBox_resource.Text;
            infoEntity.Site = textBox_site.Text;
            infoEntity.Url = textBox_url.Text;
            infoEntity.User = textBox_user.Text;
            /*infoEntity.FileVersion = FileVersion.Text;
            infoEntity.SoftwareVersion = SoftwareVersion.Text;
            infoEntity.UserCheckCmd = "";
            infoEntity.UserCheckPass = UserCheckPass.Text;
            infoEntity.UserCheckFail = UserCheckFail.Text;
            infoEntity.CodeCheckCmd = CodeCheckCmd.Text;
            infoEntity.CodeCheckPass = CodeCheckPass.Text;
            infoEntity.CodeSendCmd = CodeSendCmd.Text;*/

            saveSytemInfo(path4, infoEntity);
        }

        public void Config_Mes(string ip, string port, string timeout, string url, string site,
            string user, string password, string resource, string operation, string ncCode)
        {
            工艺部信息化组.CONFIG.IP = ip;
            工艺部信息化组.CONFIG.PORT = port;
            工艺部信息化组.CONFIG.TimeOut = int.Parse(timeout);
            工艺部信息化组.CONFIG.URL = url;
            工艺部信息化组.CONFIG.Site = site;
            工艺部信息化组.CONFIG.UserName = user;
            工艺部信息化组.CONFIG.Password = password;
            工艺部信息化组.CONFIG.Resource = resource;
            工艺部信息化组.CONFIG.Operation = operation;
            工艺部信息化组.CONFIG.NcCode = ncCode;
        }

        public void BarCodeVarify(string 产品条码, out bool 验证结果, out string MES反馈, out string XMLOUT)
        {
            BydMesCom.条码验证(产品条码, out 验证结果, out MES反馈, out XMLOUT);
        }

        public void UpDateToMes(bool 测试结果, string 产品条码, string 文件版本, string 软件版本, string 测试项, out bool 验证结果, out string MES反馈, out string XMLOUT)
        {
            BydMesCom.条码上传(测试结果, 产品条码, 文件版本, 软件版本, 测试项, out 验证结果, out MES反馈, out XMLOUT);
        }

        public void 绑定工单_Mes(string 工单号, out bool 绑定结果, out string MES反馈, out string XMLOUT)
        {
            BydMesCom.绑定工单(工单号, out 绑定结果, out MES反馈, out XMLOUT);
        }

        bool isMesLoginSuccessful = false;    // 用户联机验证结果

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VarifyUserLogin_MES(object sender, EventArgs e)
        {
            lblRunningStatus.Text = resources.GetString("user_yzz");     // 用户验证中
            lblOperatePrompt.Text = resources.GetString("Wait");         // 请等待

            Config_Mes(ip, port, timeout, url, site, user, password, resource, operation, nccode);

            bool 验证结果;
            string MES反馈;
            string XMLOUT;
            BydMesCom.用户验证(out 验证结果, out MES反馈, out XMLOUT);

            if (验证结果 == true)
            {
                if (MES反馈 != null)
                {
                    rtbMesLog.Clear();
                    rtbMesLog.AppendText(MES反馈);

                    lblRunningStatus.ForeColor = G;
                    lblRunningStatus.Text = resources.GetString("onlineUser_OK");   // 联机用户验证成功
                    lblOperatePrompt.Text = resources.GetString("scanning");        // 等待扫描条码

                    isMesLoginSuccessful = true;
                }
                else
                {
                    rtbMesLog.Clear();
                    rtbMesLog.AppendText(MES反馈);

                    lblRunningStatus.ForeColor = R;
                    lblRunningStatus.Text = resources.GetString("onlineUser_NG");   // 联机用户验证失败
                    lblOperatePrompt.Text = resources.GetString("Check_param");     // 请检查联机参数
                }
            }
            else
            {
                rtbMesLog.Clear();
                rtbMesLog.AppendText(MES反馈);

                lblRunningStatus.ForeColor = R;
                lblRunningStatus.Text = resources.GetString("onlineUser_NG");
                lblOperatePrompt.Text = resources.GetString("Check_param");
            }
        }

        NLog.Logger loggerMESBarCoode = NLog.LogManager.GetLogger("MESBarCoodeLog");
        NLog.Logger loggerMESData = NLog.LogManager.GetLogger("MESDataLog");

        /// <summary>
        /// MES条码验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VarifyBarcode_MES(object sender, EventArgs e)
        {
            Parameter_txt[2002] = "0";
            Parameter_txt[2004] = "0";

            string 产品条码 = barcodeData;
            bool 验证结果;
            string MES反馈;
            string XMLOUT;
            BarCodeVarify(产品条码, out 验证结果, out MES反馈, out XMLOUT);

            rtbMesLog.Clear();
            rtbMesLog.AppendText(MES反馈);
            loggerMESBarCoode.Trace(MES反馈);
            rtbMesLog.SelectionStart = rtbMesLog.Text.Length;
            rtbMesLog.ScrollToCaret();

            if (验证结果 == true)
            {
                Parameter_txt[2002] = "1";
            }
            else
            {
                Parameter_txt[2004] = "1";
            }
        }

        /// <summary>
        /// 结果上传
        /// </summary>
        private void Button7_Click(object sender, EventArgs e)
        {
            Parameter_txt[2006] = "0";
            Parameter_txt[2008] = "0";
            LogMsg("结果联机上传");

            bool 测试结果 = 产品结果; string 产品条码 = barcodeInfo;
            string 文件版本 = Parameter_Model[17500];
            string 软件版本 = Parameter_Model[17800];

            StringBuilder sbl = new StringBuilder();
            string[] froddMes = txtFixtureBinding.Text.Split('+');
            if (froddMes.Length > 0)
            {
                for (int i = 0; i < froddMes.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(froddMes[i]))
                    {
                        sbl.Append("!工装编号" + (i + 1) + ",工装," + froddMes[i]);
                    }
                }
            }

            string[] frdeMafrMes = CodeNum.CodeMafror(cboBarcodeRuleAndFixtures.Text, codesTable);
            if (frdeMafrMes.Length > 0)
            {
                for (int i = 0; i < frdeMafrMes.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(frdeMafrMes[i]))
                    {
                        sbl.Append("!产品物料号" + (i + 1) + ",物料," + frdeMafrMes[i]);
                    }
                }
            }
            if (actualValue.Length > 0)
            {
                for (int i = 0; i < testItems.Length; i++)
                {
                    if (!list[i].Equals("null"))
                    {

                        if (!maxValue[i].Equals("NO") && !minValue[i].Equals("NO") && !testResult[i].Equals("NO"))
                        {
                            if (!resultList[i].Equals("null"))
                            {
                                sbl.Append("!" + testItems[i] + "," + maxList[i] + "~" + minList[i] + "," + list[i] + "");
                                sbl.Append("!" + testItems[i] + "测试结果,结果," + resultList[i]);
                            }
                        }
                        else
                        {
                            sbl.Append("!" + testItems[i] + ",测试项值," + list[i] + "");
                        }
                    }
                }
            }

            string 测试项 = "!用户ID," + LoginUser.ToString() + "," + LoginUser.ToString() + "!条码,条码信息," + 产品条码 +
                "!产品型号,型号," + txtProductModel.Text +
                "" + sbl.ToString() + "!测试总结果,测试结果," + Value[9999];

            //!测试项,测试参数,测试值!用户ID,YC,YC!测试人,测试时间,测试结果!YC,2023年8月2日19：1：3,OK!工装,工装代码,41!产品编码,产品名称,产品代码!1144_00,SA3F_5820120,0SF5!生产节拍,文件版本,软件版本!5,20220811,20220811
            bool 验证结果; string MES反馈; string XMLOUT;
            UpDateToMes(测试结果, 产品条码, 文件版本, 软件版本, 测试项, out 验证结果, out MES反馈, out XMLOUT);

            rtbMesLog.Clear();
            rtbMesLog.AppendText(MES反馈);
            loggerMESData.Trace(MES反馈);

            if (验证结果 == true)
            {
                Parameter_txt[2006] = "1";
            }
            else
            {
                LogMsg(MES反馈);
                Parameter_txt[2008] = "1";
            }
            LogMsg("联机结果上传完成");

        }

        private void BindWorkOrder(object sender, EventArgs e)
        {
            绑定工单_Mes(WorkOrder, out bool 绑定结果, out string MES反馈, out string XMLOUT);
        }

        #endregion

        private static HslCommunication.Core.Net.NetworkDeviceBase KeyenceMcNet;

        private bool isPlcConnected = false;

        private void TCP_Connect(object sender, EventArgs e)
        {
            try
            {
                switch (cboConnectType.Text)
                {
                    case "KeyenceMcNet":
                        KeyenceMcNet = new KeyenceMcNet(txt_IP.Text, Convert.ToInt16(txt_port.Text));
                        break;
                    case "ModbusTCP":
                        KeyenceMcNet = new ModbusTcpNet(txt_IP.Text, Convert.ToInt16(txt_port.Text));
                        break;
                    case "MelsecMcNet":
                        KeyenceMcNet = new MelsecMcNet(txt_IP.Text, Convert.ToInt16(txt_port.Text));
                        break;
                    default:
                        KeyenceMcNet = new KeyenceMcNet(txt_IP.Text, Convert.ToInt16(txt_port.Text));
                        break;
                }
                //KeyenceMcNet = new ModbusTcpNet(tbx_IP.Text, Convert.ToInt16(tbx_port.Text));
                KeyenceMcNet.ConnectClose();
                KeyenceMcNet.SetPersistentConnection();

                OperateResult connect = KeyenceMcNet.ConnectServer();
                if (connect.IsSuccess)
                {
                    isPlcConnected = true;
                    loggerConfig.Trace($"【PLC连接】\n状态：PLC连接成功\n" +
                        $"当前IP：{txt_IP.Text}\n当前端口：{txt_port.Text}\n连接类型：{cboConnectType.Text}");
                }
                else
                {
                    isPlcConnected = false;
                    loggerConfig.Trace($"【PLC连接】\n状态：PLC连接失败，请重启软件或重启机台！");
                    MessageBox.Show(resources.GetString("plcConn"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region ---------- 系统参数设置 ----------

        DatasModel.SytemSetDerived sytemSetDerivedsd = new DatasModel.SytemSetDerived();

        /// <summary>
        /// 保存系统设置参数
        /// 包括
        /// </summary>
        private void SaveSystemConfigArgument()
        {
            if (txt_IP.Text == String.Empty || txt_port.Text == String.Empty || txtDeviceName.Text == String.Empty)
            {
                MessageBox.Show("当前界面内容均为必填项、请先填写完善");
                return;
            }

            // 通用点位设置 > 读取生产数据点位
            sytemSetDerivedsd.StartProductPoint = txtStartPoint.Text;                   // 读取生产数据起始点位
            sytemSetDerivedsd.SecondProductPoint = txtReadSecondly.Text;                // 二次读取起始点位
            sytemSetDerivedsd.TotalProductPoint = txtProductResultPoint.Text;           // 总结果点位
            sytemSetDerivedsd.SecondProductLength = txtSecondProductLength.Text;        // 二次读取条码的长度
            sytemSetDerivedsd.EndProductPoint = txtEndProductPoint.Text;                // 结束点位

            // 屏蔽模块
            sytemSetDerivedsd.SytemNoerifbarcodes = chkBypassBarcodeValidation.Checked; // 屏蔽本地条码验证
            sytemSetDerivedsd.SytemNorifytooling = chkBypassFixtureValidation.Checked;  // 屏蔽本地扫工装
            sytemSetDerivedsd.SytemQRcodNorif = chkBypassQRcodeValidation.Checked;      // 屏蔽本地二维码验证
            sytemSetDerivedsd.SytemNGCodesData = chkBypassLocalNgHistoricalData.Checked;// 屏蔽本地NG历史记录
            sytemSetDerivedsd.SytemHistorCodes = chkBanLocalHistoricalData.Checked;     // 本地历史数据

            sytemSetDerivedsd.SytemSetnullCoden = txtDefaultStyle.Text;                 // 默认数据显示样式
            sytemSetDerivedsd.CurrentPLCType = cboConnectType.Text;                     // PLC连接类型
            sytemSetDerivedsd.Save();

            // PLC参数：IP、端口、
            // 本地数据存放路径：DataPath
            // 其他设置：DeviceName、RFIDPort、读卡器设备号、显示宽度
            // 二次读条码、点位集合、名称集合、
            mdb = new mdbDatas(path4);
            DataTable table1 = mdb.Find("select * from SytemSet where ID = '1'");

            if (table1.Rows.Count > 0)
            {
                string sql = $"update [SytemSet] set [IP]='{txt_IP.Text}', [Port]='{txt_port.Text}'," +
                    $"[DataUrl]='{lblDataPath.Text}', [DeviceName]='{txtDeviceName.Text}', [rfidProt]='{cmbShowPort.Text}'," +
                    $" [rfidCode]='{tbxReaderDeviceID.Text}', [StatisticsCode]='{txtPointSets.Text}', [StatisticsName]='{txtNameSets.Text}'," +
                    $" [ResultCode]='{txtDisplayWidth.Text}', [readBarCode]='{chkReadBarcodeSecondly.Checked}' where [ID] = '1'";

                var result = mdb.Change(sql);
                //SqlToJsonConverter.ConvertToJSONLog(sql);
                if (result)
                {
                    MessageBox.Show("保存成功");
                }
            }
            else
            {
                mdbDatas.CreateAccessDatabase(path4);
                mdbDatas.CreateMDBTable(path4, "SytemSet", new System.Collections.ArrayList(new object[] { "ID", "IP", "Port", "DataUrl", "DeviceName", "stationCode", "stationName", "wordNo" }));
                DataTable dt = new DataTable("SytemSet");
                DataColumn ID = new DataColumn("ID", typeof(string));
                dt.Columns.Add(ID);
                DataColumn IP = new DataColumn("IP", typeof(string));
                dt.Columns.Add(IP);
                DataColumn Port = new DataColumn("Port", typeof(string));
                dt.Columns.Add(Port);
                DataColumn DataUrl = new DataColumn("DataUrl", typeof(string));
                dt.Columns.Add(DataUrl);
                DataColumn sysName = new DataColumn("DeviceName", typeof(string));
                dt.Columns.Add(sysName);
                DataColumn stationCode = new DataColumn("stationCode", typeof(string));
                dt.Columns.Add(stationCode);
                DataColumn stationName = new DataColumn("stationName", typeof(string));
                dt.Columns.Add(stationName);
                DataColumn wordNo = new DataColumn("wordNo", typeof(string));
                dt.Columns.Add(wordNo);

                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);

                dr[0] = "1";
                dr[1] = txt_IP.Text;
                dr[2] = txt_port.Text;
                dr[3] = lblDataPath.Text;
                dr[4] = txtDeviceName.Text;
                // dr[5] = textBox4.Text;
                // dr[6] = textBox5.Text;
                dr[7] = txtWorkOrder.Text;
                mdb.DatatableToMdb("SytemSet", dt);
            }

            mdb.CloseConnection();

        }

        /// <summary>
        /// 加载系统配置参数
        /// </summary>
        private void LoadSystemConfigArgument()
        {
            sytemSetDerivedsd = DatasServer.SytemSetDerivedServer.GetSytemSetDerived(1);
            deviceInfo = DatasServer.DeviceInformationServer.GetDeviceInformation(1);

            // 屏蔽
            chkBypassBarcodeValidation.Checked = sytemSetDerivedsd.SytemNoerifbarcodes;     // 屏蔽本地条码验证
            chkBypassFixtureValidation.Checked = sytemSetDerivedsd.SytemNorifytooling;      // 屏蔽本地扫工装
            chkBypassQRcodeValidation.Checked = sytemSetDerivedsd.SytemQRcodNorif;          // 屏蔽本地二维码验证
            chkBypassLocalNgHistoricalData.Checked = sytemSetDerivedsd.SytemNGCodesData;    // 屏蔽本地NG历史数据
            chkBanLocalHistoricalData.Checked = sytemSetDerivedsd.SytemHistorCodes;         // 屏蔽本地历史数据

            cboConnectType.Text = sytemSetDerivedsd.CurrentPLCType;                         // 当前PLC连接类型
            txtDefaultStyle.Text = sytemSetDerivedsd.SytemSetnullCoden;                     // 默认数据显示样式

            // 读取生产数据点位
            txtStartPoint.Text = sytemSetDerivedsd.StartProductPoint;                       // 开始读取点位：D1200
            txtReadSecondly.Text = sytemSetDerivedsd.SecondProductPoint;                    // 二次读取点位：D1050
            txtSecondProductLength.Text = sytemSetDerivedsd.SecondProductLength;            // 二次读取长度：10
            txtEndProductPoint.Text = sytemSetDerivedsd.EndProductPoint;                    // 结束生产点位：D1202
            txtProductResultPoint.Text = sytemSetDerivedsd.TotalProductPoint;               // 产品结果点位：D1078

            // 设备产品点位
            txtDeviceStatePoint.Text = deviceInfo.DeviceStatus;     // 设备状态点位：D1007
            txtProductModelPoint.Text = deviceInfo.ProductModel;    // 产品型号点位：D1120
            txtPMLength.Text = deviceInfo.ProductModelLength;       // 产品型号长度：10
            txtRecipeIdPoint.Text = deviceInfo.RecipeIdPoint;       // 配方号点位：D1208
            textBox43.Text = deviceInfo.FormulaModify;              // 配方修改：D1204
            textBox47.Text = deviceInfo.FormulaNumModify;           // 配方号修改：D1206
            textBox34.Text = deviceInfo.StartNFC;                   // 开始NFC
            textBox35.Text = deviceInfo.EndNFC;                     // 结束NFC
            txtViewStatus.Text = deviceInfo.ViewStatus;                 // 看板状态

            // 连接数据库
            mdb = new mdbDatas(path4);
            // 检索 "SystemSet" 表格
            DataTable table1 = mdb.Find("select * from SytemSet where ID = '1'");
            // 从数据库加载系统配置参数
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                for (int j = 0; j < table1.Columns.Count; j++)
                {
                    // PLC参数
                    txt_IP.Text = table1.Rows[i]["IP"].ToString();
                    txt_port.Text = table1.Rows[i]["Port"].ToString();

                    // 数据库路径
                    lblDataPath.Text = table1.Rows[i]["DataUrl"].ToString();

                    // 其他设置
                    txtDeviceName.Text = table1.Rows[i]["DeviceName"].ToString();       // 设备名称
                    txtDisplayWidth.Text = table1.Rows[i]["ResultCode"].ToString();     // 运行界面显示宽度
                    cmbShowPort.SelectedItem = table1.Rows[i]["rfidProt"].ToString();   // 读卡器端口
                    tbxReaderDeviceID.Text = table1.Rows[i]["rfidCode"].ToString();     // 读卡器设备号

                    // 运行界面
                    txtWorkOrder.Text = table1.Rows[i]["wordNo"].ToString();            // 工单号
                    string isReadPlc = table1.Rows[i]["Workstname"].ToString();         // 实时读取配方号
                    if (isReadPlc == "True")
                    {
                        chkReadRecipeId_PLC.Checked = true;
                    }
                    txtFixtureBinding.Text = table1.Rows[i]["BoardBeat"].ToString();                // 工装绑定
                    cboBarcodeRuleAndFixtures.SelectedValue = table1.Rows[i]["faults"].ToString();  // 条码验证规则

                    // 二次读取条码（适用于转盘机台）
                    chkReadBarcodeSecondly.Checked = bool.Parse(table1.Rows[i]["readBarCode"].ToString());

                    // 生产指标
                    txtPointSets.Text = table1.Rows[i]["StatisticsCode"].ToString();    // 点位集合
                    txtNameSets.Text = table1.Rows[i]["StatisticsName"].ToString();     // 名称集合
                }
            }
            mdb.CloseConnection();
        }

        /// <summary>
        /// 系统设置页面 > 通用点位设置页 > 保存按钮
        /// </summary>
        private void PLCPointSaveBtn_Click(object sender, EventArgs e)
        {
            SaveSystemConfigArgument();

            deviceInfo.DeviceStatus = txtDeviceStatePoint.Text;
            deviceInfo.ProductModel = txtProductModelPoint.Text;
            deviceInfo.ProductModelLength = txtPMLength.Text;
            deviceInfo.RecipeIdPoint = txtRecipeIdPoint.Text;
            deviceInfo.FormulaModify = textBox43.Text;
            deviceInfo.FormulaNumModify = textBox47.Text;
            deviceInfo.StartNFC = textBox34.Text;
            deviceInfo.EndNFC = textBox35.Text;
            deviceInfo.ViewStatus = txtViewStatus.Text;
            deviceInfo.Save();
            loggerConfig.Trace($"通用点位设置保存成功");
        }

        #endregion

        #region ---------- 上下限数据 ----------

        DataTable maxminTable = null;

        /// <summary>
        /// 实时更新上下限数据
        /// </summary>
        public void Bind_MaxMinValue()
        {
            while (IsRunningplc_ReadMaxMin)
            {
                PLCMaxMin();
                Thread.Sleep(1500);
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 实时读取上限值
        /// </summary>
        public void Get_MaxMinValue()
        {
            while (IsRunningplc_ReadMaxMin)
            {
                GetPLCMaxMin();
                Thread.Sleep(1000);
                Application.DoEvents();
            }
        }

        public List<MaxMinValue> maxMinValues = null;

        /// <summary>
        /// 读取上下限
        /// </summary>
        private void GetPLCMaxMin()
        {
            if (isPlcConnected == true)
            {
                maxMinValues = new List<MaxMinValue>();
                for (int i = 0; i < boardTable.Rows.Count; i++)
                {
                    Console.WriteLine($"生产数据读取中{i}");
                    MaxMinValue value = new MaxMinValue();

                    value.BoardName = boardTable.Rows[i]["BoardName"].ToString();
                    value.StandardCode = NullModify(ProcessPointData_PLC(boardTable.Rows[i]["StandardCode"].ToString()));
                    value.MaxBoardCode = NullModify(ProcessPointData_PLC(boardTable.Rows[i]["MaxBoardCode"].ToString()));
                    value.MinBoardCode = NullModify(ProcessPointData_PLC(boardTable.Rows[i]["MinBoardCode"].ToString()));
                    value.BoardCode = NullModify(ProcessPointData_PLC(boardTable.Rows[i]["BoardCode"].ToString()));
                    value.Result = NullModify(ProcessPointData_PLC(boardTable.Rows[i]["ResultBoardCode"].ToString()));
                    maxMinValues.Add(value);
                }
                maxMinList = maxMinValues;
            }
        }

        public string NullModify(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                str = sytemSetDerivedsd.SytemSetnullCoden;
            }
            if (str.Equals("null"))
            {
                str = sytemSetDerivedsd.SytemSetnullCoden;
            }
            return str;
        }

        private List<MaxMinValue> maxMinList = null;

        private void PLCMaxMin()
        {
            BeginInvoke(new Action(() =>
            {
                Console.WriteLine("绑定上下限开始");

                if (isPlcConnected == true)
                {
                    List<MaxMinValue> maxMinList = this.maxMinList;

                    if (maxminTable == null)
                    {
                        maxminTable = new DataTable();
                        maxminTable.Columns.Add("序号", typeof(string));
                        maxminTable.Columns.Add("测试项目", typeof(string));
                        maxminTable.Columns.Add("标准值", typeof(string));
                        maxminTable.Columns.Add("上限值", typeof(string));
                        maxminTable.Columns.Add("下限值", typeof(string));
                        maxminTable.Columns.Add("实际值", typeof(string));
                        maxminTable.Columns.Add("测试结果", typeof(string));

                        if (boardTable.Rows.Count > 0)
                        {
                            for (int i = 0; i < boardTable.Rows.Count; i++)
                            {
                                DataRow dr = maxminTable.NewRow();
                                dr["序号"] = (i + 1);
                                dr["测试项目"] = boardTable.Rows[i]["BoardName"].ToString();
                                dr["标准值"] = (boardTable.Rows[i]["StandardCode"].ToString());
                                dr["上限值"] = (boardTable.Rows[i]["MaxBoardCode"].ToString());
                                dr["下限值"] = (boardTable.Rows[i]["MinBoardCode"].ToString());
                                dr["实际值"] = (boardTable.Rows[i]["BoardCode"].ToString());

                                if (dr["实际值"].Equals("NG") || dr["实际值"].Equals("OK"))
                                {
                                    dr["测试结果"] = dr["实际值"];
                                }
                                else
                                {
                                    dr["测试结果"] = (boardTable.Rows[i]["ResultBoardCode"].ToString());
                                }

                                maxminTable.Rows.Add(dr);
                            }
                        }

                        dataGridViewDynamic4.DataSource = maxminTable;
                        dataGridViewDynamic4.Columns[1].Width = 280;

                        // 禁用列排序
                        for (int i = 0; i < dataGridViewDynamic4.Columns.Count; i++)
                        {
                            dataGridViewDynamic4.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                        }

                    }
                    else
                    {
                        if (boardTable.Rows.Count > 0 && dataGridViewDynamic4.Rows.Count > 0)
                        {
                            for (int i = 0; i < dataGridViewDynamic4.Rows.Count; i++)
                            {
                                if (maxMinList != null && maxMinList.Count == dataGridViewDynamic4.Rows.Count && maxMinList.Count > 0)
                                {
                                    if (maxMinList[i].BoardName == boardTable.Rows[i]["BoardName"].ToString())
                                    {
                                        dataGridViewDynamic4.Rows[i].Cells[0].Value = (i + 1);
                                        dataGridViewDynamic4.Rows[i].Cells[1].Value = boardTable.Rows[i]["BoardName"].ToString();
                                        dataGridViewDynamic4.Rows[i].Cells[2].Value = maxMinList[i].StandardCode.ToString();
                                        dataGridViewDynamic4.Rows[i].Cells[3].Value = maxMinList[i].MaxBoardCode.ToString();
                                        dataGridViewDynamic4.Rows[i].Cells[4].Value = maxMinList[i].MinBoardCode.ToString();
                                        dataGridViewDynamic4.Rows[i].Cells[5].Value = maxMinList[i].BoardCode.ToString();

                                        string shujuViewValew5 = dataGridViewDynamic4.Rows[i].Cells[5].Value.ToString();

                                        if (shujuViewValew5.Equals("NG") || shujuViewValew5.Equals("OK"))
                                        {
                                            dataGridViewDynamic4.Rows[i].Cells[6].Value = shujuViewValew5;
                                        }
                                        else
                                        {
                                            dataGridViewDynamic4.Rows[i].Cells[6].Value = maxMinList[i].Result.ToString();
                                        }

                                        string str = dataGridViewDynamic4.Rows[i].Cells["测试结果"].Value.ToString();

                                        if (str.Equals("OK"))
                                        {
                                            dataGridViewDynamic4.Rows[i].Cells["测试结果"].Style.BackColor = Color.Green;
                                        }
                                        else if (str.Equals("NG"))
                                        {
                                            dataGridViewDynamic4.Rows[i].Cells["测试结果"].Style.BackColor = Color.Red;
                                        }
                                        else
                                        {
                                            dataGridViewDynamic4.Rows[i].Cells["测试结果"].Style.BackColor = Color.White;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("绑定上下限结束");
            })).AsyncWaitHandle.WaitOne();

        }

        #endregion

        private string productModel = " ";  // 产品型号 = 产品名称 = 产品编号，这三种叫法的是同一个东西
        private string D1080 = " ";         // 生产总数
        private string D1084 = " ";         // 工单数量
        private string D1086 = " ";         // 完成数量
        private string D1088 = " ";         // 合格数量
        private string D1090 = " ";         // 生产节拍
        private string D1082 = " ";         // 保养计数
        private string D1076 = " ";         // NG数量
        string completeRate = "  ";         // 完成率
        string passRate = "  ";             // 合格率
        string processTime = "   ";         // 工序时间
        string usingTime = "  ";            // 利用时间
        string loadTime = "  ";             // 负荷时间
        string fpy = "  ";                  // 直通率 FPY：First-pass yield = TPY：ThroughPut Yield
        private string deviceState = null;  // 设备状态
        string productName = " ";           // 成品名称

        Dictionary<string, string> faultsMap = new Dictionary<string, string>();
        short[] faultTime = new short[] { };
        bool frockbool = true;

        DataTable kpisTable = null;     // KPIS：Key Performance Indicators 关键性能指标，生产指标
        string recipeId = string.Empty; // 配方号

        DatasModel.DeviceInformation deviceInfo = new DatasModel.DeviceInformation();
        bool ModelboolPLC = true;

        /// <summary>
        /// 读取产品型号
        /// </summary>
        private void Model_Read_Other()
        {
            // 获取用户输入
            deviceInfo.DeviceStatus = txtDeviceStatePoint.Text;     // 设备运行状态点位
            deviceInfo.ProductModel = txtProductModelPoint.Text;    // 产品型号点位
            deviceInfo.ProductModelLength = txtPMLength.Text;       // PMLength：ProductModelLength 产品型号长度
            deviceInfo.Save();

            Task.Run(() =>
            {
                while (ModelboolPLC)
                {
                    Model_Read_PLC();
                    Thread.Sleep(250);
                    Application.DoEvents();
                }
            });
        }

        List<string> kpiList = null;

        private void Model_Read_PLC()
        {
            if (isPlcConnected == true)
            {
                // 读取设备运行状态：D1007
                deviceState = KeyenceMcNet.ReadInt32(deviceInfo.DeviceStatus).Content.ToString();

                // 读取产品型号(产品名称)：D1120
                ushort productModelLength = 10;
                ushort.TryParse(deviceInfo.ProductModelLength, out productModelLength);
                string pModel = KeyenceMcNet.ReadString(deviceInfo.ProductModel, productModelLength).Content;
                this.productModel = CodeNum.FormatString(pModel);

                // 从PLC读取生产指标数据，并对读取过来的数据二次处理
                kpiList = new List<string>();
                if (kpisPointSets.Length > 0)
                {
                    for (int i = 0; i < kpisPointSets.Length; i++)
                    {
                        // 从PLC读取相关指标的点位，并处理读过来的数据
                        if (kpisPointSets[i].Contains("-"))
                        {
                            kpiList.Add(ProcessPointData_PLC(kpisPointSets[i]));
                        }
                        else
                        {
                            int index = kpisPointSets[i].IndexOf(":");
                            string dataType = kpisPointSets[i].Substring(index + 1, 1);
                            string plcAddress = kpisPointSets[i].Substring(0, index);
                            kpiList.Add(CodeNum.HandlePlcData(KeyenceMcNet.ReadInt32(plcAddress).Content.ToString(), dataType));
                        }
                    }
                }

                D1080 = kpiList[0];          // 生产总数
                D1084 = kpiList[1];          // 工单数量
                D1086 = kpiList[2];          // 完成数量
                D1088 = kpiList[3];          // 合格数量
                D1090 = kpiList[4];          // 生产节拍
                D1082 = kpiList[5];          // 保养计数
                D1076 = kpiList[6];          // NG数量
                fpy = kpiList[7];            // 直通率
                completeRate = kpiList[8];   // 完成率
                passRate = kpiList[9];       // 合格率 
                processTime = kpiList[10];   // 工序时间
                usingTime = kpiList[11];     // 利用时间
                loadTime = kpiList[12];      // 负荷时间
                faultTime = KeyenceMcNet.ReadInt16(txtFaultStartPoint.Text, CodeNum.Shoubtowule(txtFaultLength.Text)).Content;

                Invoke(new Action(() =>
                {
                    // 启用实时读取PLC
                    if (chkReadRecipeId_PLC.Checked)
                    {
                        // 读取配方号：D1208
                        string currentId = KeyenceMcNet.ReadInt32(deviceInfo.RecipeIdPoint).Content.ToString();
                        if (recipeId != currentId)
                        {
                            recipeId = currentId;
                        }
                        // 根据配方号更新配方
                        cboBarcodeRuleAndFixtures.SelectedValue = currentId;
                        // 更新显示当前配方号
                        lblRecipeId.Text = currentId;

                        string[] expectedFixtures = CodeNum.ExtractFixturesInfo(cboBarcodeRuleAndFixtures.Text);
                        string[] frockB = txtFixtureBinding.Text.ToString().Split('+');

                        if (!CodeNum.CompareArray(expectedFixtures, frockB))
                        {
                            string frockmm = "";

                            foreach (var frockid in frockB)
                            {
                                if (expectedFixtures.Contains(frockid))
                                {
                                    if (string.IsNullOrWhiteSpace(frockmm))
                                    {
                                        frockmm = frockid;
                                    }
                                    else
                                    {
                                        frockmm = $"{frockmm} + {frockid}";
                                    }
                                }
                            }

                            txtFixtureBinding.Text = frockmm;

                            string[] frockC = txtFixtureBinding.Text.ToString().Split('+');

                            if (!CodeNum.CompareArray(expectedFixtures, frockC))
                            {
                                lblOperatePrompt.Text = resources.GetString("Wait_scan_Jig");   // 等待扫工装
                            }
                        }
                    }

                    // 产品编码
                    textBox42.Text = CodeNum.CodeStrfror(cboBarcodeRuleAndFixtures.Text, codesTable);

                    // 更新设备运行状态
                    if (deviceState != null)
                    {
                        switch (deviceState)
                        {
                            case "1":   // 生产运行 
                                lblDeviceStatus.ForeColor = Color.Green;
                                Fouddinog();
                                break;
                            case "2":   // 故障未停机  
                                lblDeviceStatus.ForeColor = Color.Red;
                                Namestation("触发故障");
                                break;
                            case "3":   // 故障停机 
                                lblDeviceStatus.ForeColor = Color.Red;
                                Namestation("故障停机");
                                break;
                            case "4":   // 待机
                                lblDeviceStatus.ForeColor = Color.Orange;
                                Fouddinog();
                                break;
                            default:
                                lblDeviceStatus.ForeColor = Color.Red;
                                break;
                        }
                    }

                    // 更新产品名称文本框的内容
                    txtProductModel.Text = this.productModel;

                    // 实时更新生产指标的显示
                    if (kpisNameSets.Length == kpisPointSets.Length && kpisPointSets.Length > 0)
                    {
                        // 初始化表格
                        if (kpisTable == null)
                        {
                            kpisTable = new DataTable();
                            kpisTable.Columns.Add("名称", typeof(string));
                            kpisTable.Columns.Add("值", typeof(string));

                            for (int i = 0; i < kpisNameSets.Length; i++)
                            {
                                DataRow dr = kpisTable.NewRow();
                                dr["名称"] = kpisNameSets[i];
                                dr["值"] = kpiList[i];
                                kpisTable.Rows.Add(dr);
                            }

                            dataGridViewDynamic3.DataSource = kpisTable;

                            // 禁用列排序
                            for (int i = 0; i < dataGridViewDynamic3.Columns.Count; i++)
                            {
                                dataGridViewDynamic3.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                            }
                        }
                        else
                        {
                            // 实时更新生产指标的显示
                            for (int i = 0; i < kpisNameSets.Length; i++)
                            {
                                dataGridViewDynamic3.Rows[i].Cells[0].Value = kpisNameSets[i];
                                dataGridViewDynamic3.Rows[i].Cells[1].Value = kpiList[i];
                            }
                        }

                    }

                    // 读取成品名称
                    /*if (chkReadPName.Checked)
                    {
                        txtProductName.Text = CodeNum.FormatString(KeyenceMcNet.ReadString("D1210", 10).Content);
                    }*/

                    // 网络打印设置 > 读取PLC型号
                    if (checkBox4.Checked)
                    {
                        textBox28.Text = txtProductModel.Text;
                    }
                    // 驱动打印设置 > 读取PLC型号
                    if (checkBox15.Checked)
                    {
                        textBox52.Text = txtProductModel.Text;
                    }
                    Thread.Sleep(150);
                    Application.DoEvents();
                }));
            }
        }

        /// <summary>
        /// 触发发送设备故障信息
        /// </summary>
        /// <param name="status"></param>
        public void Namestation(string status)
        {
            Invoke(new Action(() =>
            {
                if (isDashboardConnected)
                {
                    if (faultTime != null)
                    {
                        for (int i = 0; i < faultTime.Length; i++)
                        {
                            if (faultsTable.Rows.Count > i)
                            {
                                ///编号
                                string faultID = faultsTable.Rows[i]["编号"].ToString();
                                if (faultTime[i] == 1)
                                {
                                    if (!faultsMap.ContainsKey(faultID))
                                    {
                                        string statssName = CodeNum.WorkIDNm(faultsTable.Rows[i]["工位ID"].ToString(), stationName); //
                                        string faultas = "1+" + statssName + "+" + txtDeviceName.Text + "+" + status + "+"
                                         + faultsTable.Rows[i]["故障描述"] + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        ShowMsg(faultas);
                                        Send(faultas);
                                        faultsMap.Add(faultID, faultas);
                                    }
                                }
                                else if (faultTime[i] == 0)
                                {
                                    string faulvor = "";
                                    if (faultsMap.TryGetValue(faultID, out faulvor))
                                    {
                                        if (!string.IsNullOrWhiteSpace(faulvor))
                                        {
                                            string shofault = faulvor + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                            ShowMsg(shofault);
                                            Send(shofault);
                                            faultsMap.Remove(faultID);
                                        }
                                    }
                                }
                                try
                                {
                                    string jsonStr = JsonConvert.SerializeObject(faultsMap);
                                    Exception exception = new Exception(jsonStr);
                                    //devicelog.Trace(exception, status);
                                    File.WriteAllText(pathText, jsonStr);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }));
        }

        /// <summary>
        /// 结束故障结束时候触发
        /// </summary>
        public void Fouddinog()
        {
            Invoke(new Action(() =>
            {
                if (isDashboardConnected)
                {
                    if (faultsMap.Count > 0 & faultsMap != null)
                    {
                        try
                        {
                            File.WriteAllText(pathText, "{}");
                            foreach (var item in faultsMap)
                            {
                                //输出
                                //1+故障所在工位+机台名称+设备状态+
                                //故障的描述+触发故障的结束时间 
                                string shofault = item.Value + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                ShowMsg(shofault);
                                Send(shofault);
                                faultsMap.Remove(item.Key);
                                if (faultsMap.Count == 0)
                                {
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }));
        }

        Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        /// <summary>
        /// 系统设置页面的保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button25_Click(object sender, EventArgs e)
        {
            SaveSystemConfigArgument();
            loggerConfig.Trace($"系统设置参数保存成功");
        }

        /// <summary>
        /// 配方设置界面的保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button29_Click(object sender, EventArgs e)
        {
            SaveSystemConfigArgument();
            loggerConfig.Trace($"名称集合保存成功！\n点位集合保存成功！");
        }

        #region ---------- 显示列表 ----------

        /// <summary>
        /// 显示列表表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertTable(object sender, EventArgs e)
        {
            //dataGridViewDynamic2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewDynamic2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            int countH = 0;
            countH = CodeNum.Tresultstrhomd(maxValue) + CodeNum.Tresultstrhomd(minValue) + CodeNum.Tresultstrhomd(testResult);

            //添加三列
            for (int i = 0; i < 7; i++)//这里一定要改成对应个数，比如0~16是17个！
            {
                dataGridViewDynamic2.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridViewDynamic2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; //设置所有列自适应宽度

            }
            for (int i = 7; i < ((testItems.Length + countH) + 7); i++)//这里一定要改成对应个数，比如0~16是17个！
            {
                dataGridViewDynamic2.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridViewDynamic2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; //设置所有列自适应宽度
            }
            dataGridViewDynamic2.Columns[0].HeaderText = resources.GetString("d2No");
            dataGridViewDynamic2.Columns[1].HeaderText = resources.GetString("d2CPTM");
            dataGridViewDynamic2.Columns[2].HeaderText = resources.GetString("d2CPJG");
            dataGridViewDynamic2.Columns[3].HeaderText = resources.GetString("d2CPBH");
            dataGridViewDynamic2.Columns[4].HeaderText = resources.GetString("d2CZY");
            dataGridViewDynamic2.Columns[5].HeaderText = resources.GetString("d2SCZT");
            dataGridViewDynamic2.Columns[6].HeaderText = resources.GetString("d2CSSJ");
            int a = 7;
            if (actualValue.Length > 0)
            {
                for (int i = 0; i < testItems.Length; i++)
                {
                    dataGridViewDynamic2.Columns[a].Width = CodeNum.Doubtowule(txtDisplayWidth.Text);
                    dataGridViewDynamic2.Columns[a].HeaderText = testItems[i] + unitName[i];
                    a = a + 1;
                    if (!maxValue[i].Equals("NO"))
                    {
                        dataGridViewDynamic2.Columns[a].Width = CodeNum.Doubtowule(txtDisplayWidth.Text);
                        dataGridViewDynamic2.Columns[a].HeaderText = testItems[i] + "上限" + unitName[i];
                        a = a + 1;
                    }
                    if (!minValue[i].Equals("NO"))
                    {
                        dataGridViewDynamic2.Columns[a].Width = CodeNum.Doubtowule(txtDisplayWidth.Text);
                        dataGridViewDynamic2.Columns[a].HeaderText = testItems[i] + "下限" + unitName[i];
                        a = a + 1;
                    }
                    if (!testResult[i].Equals("NO"))
                    {
                        dataGridViewDynamic2.Columns[a].Width = CodeNum.Doubtowule(txtDisplayWidth.Text);
                        dataGridViewDynamic2.Columns[a].HeaderText = testItems[i] + "结果";
                        a = a + 1;
                    }
                }
            }

            //dataGridViewDynamic2.Columns[3].HeaderText = "卷簧扭力";
            //dataGridViewDynamic2.Columns[4].HeaderText = "压力";
            //dataGridViewDynamic2.Columns[5].HeaderText = "行程";
            //dataGridViewDynamic2.Columns[8].HeaderText = "软件版本";
            //dataGridViewDynamic2.Columns[9].HeaderText = "文件版本";
            //dataGridViewDynamic2.Columns[10].HeaderText = "产品编码";
            //dataGridViewDynamic2.Columns[11].HeaderText = "产品名称";
            //dataGridViewDynamic2.FirstDisplayedScrollingRowIndex = dataGridViewDynamic2.Rows.Count - 1;

        }//表格列表表头，根据设备不同自行增减 参照richTextBox.AppendText里边的字符串个数

        //表头不分左右！！！

        //显示行数据↓
        private void 显示结果_Left()
        {
            Invoke(new Action(() =>
            {
                if (dataGridViewDynamic2.RowCount > 5000)
                {
                    this.dataGridViewDynamic2.Rows.Clear();
                }

                var upState = "失败";
                if (Parameter_txt[2006] == "1")
                {
                    upState = "成功";
                }
                else if (OffLineType == 1)
                {
                    upState = "本地";
                }

                DateTime now = DateTime.Now;
                //添加行
                //int index = this.dataGridViewDynamic2.Rows.Add();
                DataGridViewRow dataGdVwRow = new DataGridViewRow();
                dataGdVwRow.CreateCells(this.dataGridViewDynamic2);
                dataGdVwRow.Cells[0].Value = Num;
                dataGdVwRow.Cells[1].Value = barcodeInfo;
                dataGdVwRow.Cells[2].Value = Value[9999];
                dataGdVwRow.Cells[3].Value = txtProductModel.Text;
                dataGdVwRow.Cells[4].Value = LoginUser.ToString();
                dataGdVwRow.Cells[5].Value = upState;
                dataGdVwRow.Cells[6].Value = now.ToString("MM-dd HH:mm:ss");

                int a = 7;
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        dataGdVwRow.Cells[a].Value = list[i];
                        a = a + 1;
                        if (maxList.Count > i)
                        {
                            if (!maxValue[i].Equals("NO"))
                            {
                                dataGdVwRow.Cells[a].Value = maxList[i];
                                a = a + 1;
                            }
                        }

                        if (minList.Count > i)
                        {
                            if (!minValue[i].Equals("NO"))
                            {
                                dataGdVwRow.Cells[a].Value = minList[i];
                                a = a + 1;
                            }
                        }

                        if (resultList.Count > i)
                        {
                            if (!testResult[i].Equals("NO"))
                            {
                                dataGdVwRow.Cells[a].Value = resultList[i];
                                a = a + 1;
                            }
                        }
                    }
                }
                this.dataGridViewDynamic2.Rows.Insert(0, dataGdVwRow);
            }));

            /*this.dataGridViewDynamic2.Rows.Insert(index, dataGridViewDynamic2.Rows[index]);
            //每台机定制参数
            this.dataGridViewDynamic2.Rows[index].Cells[2].Value = Parameter_txt[3692];
            this.dataGridViewDynamic2.Rows[index].Cells[3].Value = Parameter_txt[3694];
            this.dataGridViewDynamic2.Rows[index].Cells[4].Value = Parameter_txt[3696];
            this.dataGridViewDynamic2.Rows[index].Cells[5].Value = Parameter_txt[3698];
            //每台机定制参数

            this.dataGridViewDynamic2.Rows[index].Cells[4].Value = Parameter_txt[1032];
            this.dataGridViewDynamic2.Rows[index].Cells[5].Value = Parameter_txt[1034];
            this.dataGridViewDynamic2.Rows[index].Cells[6].Value = Value[1036];
            this.dataGridViewDynamic2.Rows[index].Cells[7].Value = Parameter_Model[17000];

            this.dataGridViewDynamic2.Rows[index].Cells[8].Value = Parameter_Model[17500];
            this.dataGridViewDynamic2.Rows[index].Cells[9].Value = Parameter_Model[17800];
            this.dataGridViewDynamic2.Rows[index].Cells[10].Value = Parameter_Model[18000];
            this.dataGridViewDynamic2.Rows[index].Cells[11].Value = Parameter_Model[18500];
            dataGridViewDynamic2.FirstDisplayedScrollingRowIndex = dataGridViewDynamic2.Rows.Count - 1;*/
        }

        #endregion

        private async void saveInfo()
        {
            await Task.Run(() =>
            {
                try
                {
                    LogMsg("生产数据本地保存中...");
                    DateTime time = DateTime.Now;
                    string year = time.Year.ToString();
                    string month = time.Month.ToString();
                    Test($"{lblDataPath.Text}\\{year}年{month}月生产数据.mdb");
                    LogMsg("生产数据本地保存完成");
                }
                catch (Exception ex)
                {
                }
            });
        }

        private void Test(string conn)
        {
            Invoke(new Action(() =>
            {
                mdbDatas.CreateAccessDatabase(conn);
                StringBuilder str = new StringBuilder("产品,当前工单号,工装编号,产品编码, 条码, 测试人,测试时间,测试结果");
                ArrayList arrayList = new ArrayList();
                object[] obj = new object[] { "产品", "当前工单号", "工装编号", "产品编码", "条码", "测试人", "测试时间", "测试结果" };
                arrayList.AddRange(obj);
                if (testItems.Length > 0)
                {
                    //object[] obj1 = new object[((testItems.Length)*4)];
                    for (int i = 0; i < (testItems.Length); i++)
                    {
                        str.Append(",");
                        arrayList.Add(testItems[i]);
                        str.Append(testItems[i]);
                        if (!maxValue[i].Equals("NO") && !minValue[i].Equals("NO") && !testResult[i].Equals("NO"))
                        {
                            str.Append(",");
                            arrayList.Add(testItems[i] + "上限");
                            str.Append(testItems[i] + "上限");
                            str.Append(",");
                            arrayList.Add(testItems[i] + "下限");
                            str.Append(testItems[i] + "下限");
                            str.Append(",");
                            arrayList.Add(testItems[i] + "结果");
                            str.Append(testItems[i] + "结果");
                        }
                    }
                    // arrayList.AddRange(obj1);
                }

                mdbDatas.CreateMDBTable(conn, "Sheet1", arrayList);//new System.Collections.ArrayList(new object[] { "产品", "条码", "测试人", "测试时间","测试结果", "PLC配方", "文件版本","软件版本"}));
                mdb = new mdbDatas(conn);

                DateTime now = DateTime.Now;
                string CP = txtProductModel.Text == "" ? "无" : txtProductModel.Text;
                string barcode = barcodeInfo == "" ? " " : barcodeInfo;
                StringBuilder str1 = new StringBuilder();
                str1.Append("'" + CP + "',");
                str1.Append("'" + txtWorkOrder.Text + "',");
                str1.Append("'" + txtFixtureBinding.Text + "',");
                str1.Append("'" + CodeNum.CodeStrfror(cboBarcodeRuleAndFixtures.Text, codesTable) + "',");
                str1.Append("'" + barcode + "',");
                str1.Append("'" + LoginUser.ToString() + "',");
                str1.Append("'" + now.ToString("yyyy年MM月dd日 HH:mm:ss") + "',");
                str1.Append("'" + Value[9999] + "'");

                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {

                        str1.Append(",");
                        string rst = " ";
                        if (list[i].Length > 0)
                        {
                            rst = list[i];
                        }
                        str1.Append("'" + rst + "'");
                        if (!maxValue[i].Equals("NO") && !minValue[i].Equals("NO") && !testResult[i].Equals("NO"))
                        {
                            str1.Append(",");
                            str1.Append("'" + CodeNum.NullECoshu(maxList[i]) + "'");
                            str1.Append(",");
                            str1.Append("'" + CodeNum.NullECoshu(minList[i]) + "'");
                            str1.Append(",");
                            str1.Append("'" + CodeNum.NullECoshu(resultList[i]) + "'");
                        }
                    }
                }

                string sql = "insert into Sheet1 (" + str + ") values (" + str1 + ")";
                bool result = mdb.Add(sql.ToString());
                mdb.CloseConnection();
            }));
        }

        private void saveSytemInfo(string conn, SytemInfoEntity systemInfo)
        {
            mdb = new mdbDatas(conn);
            DataTable table1 = mdb.Find("select * from SytemInfo where ID = '1'");

            if (table1.Rows.Count > 0)
            {

                DataRow row = table1.Rows[0];
                List<string> changeDetails = new List<string>();

                // 检查每个字段的变化
                if (row["IP"].ToString() != systemInfo.IP)
                {
                    changeDetails.Add($"IP：{row["IP"]} -> {systemInfo.IP}");
                }
                if (row["Port"].ToString() != systemInfo.Port)
                {
                    changeDetails.Add($"端口：{row["Port"]} -> {systemInfo.Port}");
                }
                if (row["Timeout"].ToString() != systemInfo.Timeout)
                {
                    changeDetails.Add($"连接超时：{row["Timeout"]} -> {systemInfo.Timeout}");
                }
                if (row["Url"].ToString() != systemInfo.Url)
                {
                    changeDetails.Add($"URL：{row["Url"]} -> {systemInfo.Url}");
                }
                if (row["Site"].ToString() != systemInfo.Site)
                {
                    changeDetails.Add($"站点：{row["Site"]} -> {systemInfo.Site}");
                }
                if (row["Resource"].ToString() != systemInfo.Resource)
                {
                    changeDetails.Add($"工位：{row["Resource"]} -> {systemInfo.Resource}");
                }
                if (row["Opration"].ToString() != systemInfo.Opration)
                {
                    changeDetails.Add($"工序：{row["Opration"]} -> {systemInfo.Opration}");
                }
                if (row["NcCode"].ToString() != systemInfo.NcCode)
                {
                    changeDetails.Add($"不合格代码：{row["NcCode"]} -> {systemInfo.NcCode}");
                }
                if (row["Password"].ToString() != systemInfo.Password)
                {
                    changeDetails.Add($"密码：{row["Password"]} -> {systemInfo.Password}");
                }
                if (row["User"].ToString() != systemInfo.User)
                {
                    changeDetails.Add($"用户：{row["User"]} -> {systemInfo.User}");
                }

                if (changeDetails.Count > 0)
                {
                    string sql = $"update [SytemInfo] set [IP] = '{systemInfo.IP}', [Port] = '{systemInfo.Port}', [Timeout] = '{systemInfo.Timeout}', " +
                        $"[NcCode] = '{systemInfo.NcCode}', [Opration] = '{systemInfo.Opration}', [Resource] = '{systemInfo.Resource}', " +
                        $"[Site] = '{systemInfo.Site}', [Url] = '{systemInfo.Url}', [User] = '{systemInfo.User}', [Password] = '{systemInfo.Password}' where [ID] = '1'";

                    var result = mdb.Change(sql);
                    if (result)
                    {
                        string changeLog = string.Join("\n", changeDetails);
                        loggerConfig.Trace($"【MES参数修改成功】\n修改详情：\n{changeLog}");
                        MessageBox.Show("保存成功");
                    }
                }
                else
                {

                    MessageBox.Show("没有任何变化，无需保存。");

                }
            }

            else
            {
                mdbDatas.CreateAccessDatabase(conn);
                mdbDatas.CreateMDBTable(conn, "SytemInfo", new System.Collections.ArrayList(new object[]
                { "ID", "IP", "Port", "Timeout", "NcCode", "Opration", "Password", "Resource", "Site", "Url",
                    "User", "FileVersion", "SoftwareVersion", "UserCheckCmd", "UserCheckPass", "UserCheckFail",
                    "CodeCheckCmd", "CodeCheckPass", "CodeSendCmd" }));
                DataTable dt = new DataTable("SytemInfo");

                DataColumn ID = new DataColumn("ID", typeof(string));
                dt.Columns.Add(ID);
                DataColumn IP = new DataColumn("IP", typeof(string));
                dt.Columns.Add(IP);
                DataColumn Port = new DataColumn("Port", typeof(string));
                dt.Columns.Add(Port);
                DataColumn Timeout = new DataColumn("Timeout", typeof(string));
                dt.Columns.Add(Timeout);
                DataColumn NcCode = new DataColumn("NcCode", typeof(string));
                dt.Columns.Add(NcCode);
                DataColumn Opration = new DataColumn("Opration", typeof(string));
                dt.Columns.Add(Opration);
                DataColumn Password = new DataColumn("Password", typeof(string));
                dt.Columns.Add(Password);
                DataColumn Resource = new DataColumn("Resource", typeof(string));
                dt.Columns.Add(Resource);
                DataColumn Site = new DataColumn("Site", typeof(string));
                dt.Columns.Add(Site);
                DataColumn Url = new DataColumn("Url", typeof(string));
                dt.Columns.Add(Url);
                DataColumn User = new DataColumn("User", typeof(string));
                dt.Columns.Add(User);
                DataColumn FileVersion = new DataColumn("FileVersion", typeof(string));
                dt.Columns.Add(FileVersion);
                DataColumn SoftwareVersion = new DataColumn("SoftwareVersion", typeof(string));
                dt.Columns.Add(SoftwareVersion);
                DataColumn UserCheckCmd = new DataColumn("UserCheckCmd", typeof(string));
                dt.Columns.Add(UserCheckCmd);
                DataColumn UserCheckFail = new DataColumn("UserCheckFail", typeof(string));
                dt.Columns.Add(UserCheckFail);
                DataColumn UserCheckPass = new DataColumn("UserCheckPass", typeof(string));
                dt.Columns.Add(UserCheckPass);
                DataColumn CodeCheckCmd = new DataColumn("CodeCheckCmd", typeof(string));
                dt.Columns.Add(CodeCheckCmd);
                DataColumn CodeCheckPass = new DataColumn("CodeCheckPass", typeof(string));
                dt.Columns.Add(CodeCheckPass);
                DataColumn CodeSendCmd = new DataColumn("CodeSendCmd", typeof(string));
                dt.Columns.Add(CodeSendCmd);

                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);

                dr[0] = "1";
                dr[1] = systemInfo.IP;
                dr[2] = systemInfo.Port;
                dr[3] = systemInfo.Timeout;
                dr[4] = systemInfo.NcCode;
                dr[5] = systemInfo.Opration;
                dr[6] = "";
                dr[7] = systemInfo.Resource;
                dr[8] = systemInfo.Site;
                dr[9] = systemInfo.Url;
                dr[10] = "";
                dr[11] = "";
                dr[12] = "";
                dr[13] = "";
                dr[14] = "";
                dr[15] = "";
                dr[16] = "";
                dr[17] = "";
                dr[18] = "";
                mdb.DatatableToMdb("SytemInfo", dt);
            }

            mdb.CloseConnection();
        }

        BarcodeVefictn barcodeValidation = null;

        string barcodeData = string.Empty;

        /// <summary>
        /// 读取条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button19_Click(object sender, EventArgs e)
        {
            if (isPlcConnected == true)
            {
                /*Invoke(new Action(() =>
                {
                    DateTime time = DateTime.Now;
                    string currentYear = time.Year.ToString();
                    string currentMonth = time.Month.ToString();
                    string currentDay = time.Day.ToString();

                    DateTime times_Start = dateTimePicker1.Value.Date;
                    DateTime times_End = dateTimePicker2.Value.Date;

                    string times_Start_string = times_Start.ToString();
                    string times_End_string = times_End.ToString();

                    DateTime times_Start_Sub = DateTime.Parse(times_Start_string);
                    DateTime times_End_Sub = DateTime.Parse(times_End_string);
                }));*/

                barcodeValidation = null;

                for (int i = 0; i < barcodeVefictnList.Count; i++)
                {
                    // BarcodeStartPLC：D1000 = 1 时可以从PLC读条码
                    var plcValue = KeyenceMcNet.ReadInt32(barcodeVefictnList[i].BarcodeStartPLC).Content;

                    // 读到1开始读取条码
                    if (plcValue == 1)
                    {
                        barcodeValidation = barcodeVefictnList[i];
                        break;
                    }
                }

                if (barcodeValidation != null)
                {
                    Invoke(new Action(() =>
                    {
                        // 初始化状态指示灯
                        lblScanBarcodeStatus.ForeColor = Color.Black; // 扫码状态指示灯
                        lblValidationStatus.ForeColor = Color.Black;  // 验证状态指示灯
                        lblUploadStatus.ForeColor = Color.Black;      // 上传状态指示灯

                        lblRunningStatus.ForeColor = Color.Black;
                        lblProductResult.Text = resources.GetString("label_Value"); // 待机
                        lblProductResult.ForeColor = Color.Black;
                        lblProductResult.BackColor = Color.White;

                        // 读取条码 (D1100)
                        LogMsg("准备读码....");
                        ushort barcodeLength = barcodeValidation.GetBarcodeLength();
                        string rawBarcode = KeyenceMcNet.ReadString(barcodeValidation.BarcodePositionPLC, barcodeLength).Content;
                        barcodeData = CodeNum.FormatString(rawBarcode);
                        txtShowBarcode.Text = barcodeData;
                        LogMsg($"条码【D1100】 = {barcodeData}");

                        // 判断条码是否为空
                        if (string.IsNullOrEmpty(barcodeData))
                        {
                            lblScanBarcodeStatus.ForeColor = R;
                            txtShowBarcode.Text = barcodeValidation.NoBarcodePrompt;   // 条码数据：未读到条码
                            LogMsg(barcodeValidation.NoBarcodePrompt);
                            try
                            {
                                // 验证失败结束点位
                                KeyenceMcNet.Write(barcodeValidation.ErrorBarcodeEndPLC, 1);
                                LogMsg("反馈条码验证【D1005】 = 1");
                            }
                            catch (Exception ex)
                            {
                                LogMsg(ex.ToString());
                            }
                            return;
                        }

                        if (barcodeValidation.BarcodeVerS == true)
                        {
                            // 条码验证
                            if (chkBypassBarcodeValidation.Checked == false)
                            {
                                // 工装验证
                                if (chkBypassFixtureValidation.Checked == false)
                                {
                                    string[] expectedFixtures = CodeNum.ExtractFixturesInfo(cboBarcodeRuleAndFixtures.Text);
                                    string[] frockB = (txtFixtureBinding.Text).Split('+');

                                    // 判断工装是否相等
                                    if (!CodeNum.CompareArray(expectedFixtures, frockB))
                                    {
                                        // 判断条码是否在工装里面
                                        if (expectedFixtures.Contains(barcodeData))
                                        {
                                            if (string.IsNullOrWhiteSpace(txtFixtureBinding.Text))
                                            {
                                                txtFixtureBinding.Text = barcodeData;
                                            }
                                            else
                                            {
                                                if (!frockB.Contains(barcodeData))
                                                {
                                                    txtFixtureBinding.Text = $"{txtFixtureBinding.Text}+{barcodeData}";
                                                }
                                            }

                                            string[] frockC = txtFixtureBinding.Text.ToString().Split('+');

                                            // 验证条码则保存到数据库
                                            if (CodeNum.CompareArray(expectedFixtures, frockC))
                                            {
                                                mdb = new mdbDatas(path4);
                                                DataTable table1 = mdb.Find("select * from SytemSet where ID = '1'");
                                                if (table1.Rows.Count > 0)
                                                {
                                                    string sql = "update [SytemSet] set [BoardBeat]='" + txtFixtureBinding.Text + "' where [ID] = '1'";
                                                    var result = mdb.Change(sql);
                                                }
                                                mdb.CloseConnection();
                                                lblOperatePrompt.Text = resources.GetString("scanning");// 等待扫描条码
                                            }

                                            lblScanBarcodeStatus.ForeColor = Color.Green;
                                            lblRunningStatus.ForeColor = Color.Green;
                                            lblRunningStatus.Text = resources.GetString("Fixture_OK");  // 工装编号验证通过

                                            // 条码验证通过
                                            try
                                            {
                                                // 保存通过验证的条码
                                                KeyenceMcNet.Write(barcodeValidation.PassBarcodeEndPLC, 2);
                                            }
                                            catch (Exception ex)
                                            {
                                                LogMsg(ex.ToString());
                                            }
                                            LogMsg("反馈条码验证【D1003】 = 2");
                                            return;
                                        }
                                        else
                                        {
                                            lblRunningStatus.ForeColor = R;
                                            lblRunningStatus.Text = resources.GetString("Fixture_NG");
                                            lblValidationStatus.ForeColor = R;
                                            LogMsg("工装编号验证失败！");
                                            try
                                            {
                                                // 验证失败结束
                                                KeyenceMcNet.Write(barcodeValidation.ErrorBarcodeEndPLC, 2);
                                                LogMsg("反馈条码验证【D1005】 = 2");
                                            }
                                            catch (Exception ex)
                                            {
                                                LogMsg(ex.ToString());
                                            }
                                            return;
                                        }
                                    }
                                }

                                // 屏蔽查询历史数据
                                if (chkBanLocalHistoricalData.Checked == false)
                                {
                                    if (barcodeData != "1")
                                    {
                                        DateTime time = DateTime.Now;
                                        string currentYear = time.Year.ToString();
                                        string currentMonth = time.Month.ToString();
                                        string dataPath = $"{lblDataPath.Text}\\{currentYear}年{currentMonth}月生产数据.mdb";
                                        mdb = new mdbDatas(dataPath);
                                        string selectSql = $" select * from Sheet1 where 条码 = '{barcodeData}' ";

                                        // 屏蔽NG历史数据
                                        if (chkBypassLocalNgHistoricalData.Checked == true)
                                        {
                                            selectSql += " and 测试结果 = 'OK' ";
                                        }

                                        bool isDataExist = mdb.DoesDataExist(selectSql);
                                        mdb.CloseConnection();

                                        if (isDataExist)
                                        {
                                            lblRunningStatus.ForeColor = R;
                                            lblRunningStatus.Text = barcodeValidation.RepeatcodePrompt;
                                            lblValidationStatus.ForeColor = R;
                                            LogMsg("条码重复扫过，请重新扫描！");
                                            try
                                            {
                                                // 验证失败
                                                KeyenceMcNet.Write(barcodeValidation.ErrorBarcodeEndPLC, 1);
                                                LogMsg("反馈条码验证【D1005】 = 1");
                                            }
                                            catch (Exception ex)
                                            {
                                                LogMsg(ex.ToString());
                                            }
                                            return;
                                        }
                                    }
                                }

                                // 本地条码验证（若实际获取的条码前缀 = 设定的条码验证规则 => 通过验证）
                                string barcodeRule = CodeNum.ExtractBarcodeValidationRule(cboBarcodeRuleAndFixtures.Text);
                                bool ValidationResult = VarifyBarcodeRule(barcodeValidation, barcodeRule);
                                if (ValidationResult == false)
                                {
                                    return;
                                }
                            }
                        }

                        // 验证二维码前缀
                        if (barcodeValidation.QRcodeVerS == true)
                        {
                            // 二维码验证
                            if (chkBypassQRcodeValidation.Checked == false)
                            {
                                string frockA = CodeNum.CodeStrQRcode(cboBarcodeRuleAndFixtures.Text, codesTable);

                                if (VarifyBarcodeRule(barcodeValidation, frockA) == false)
                                {
                                    return;
                                }
                            }
                        }

                        // 联机上传MES进行条码验证，单机直接通过
                        if (OffLineType == 1)
                        {
                            lblScanBarcodeStatus.ForeColor = Color.Green;
                            lblRunningStatus.ForeColor = Color.Green;
                            lblRunningStatus.Text = barcodeValidation.PassPrompt;  // 条码验证通过

                            try
                            {
                                KeyenceMcNet.Write(barcodeValidation.PassBarcodeEndPLC, 1);
                            }
                            catch (Exception ex)
                            {
                                LogMsg(ex.ToString());
                            }
                            LogMsg("反馈条码验证【D1003】 = 1");
                        }
                        else
                        {
                            // 绑定工单
                            if (chkBindWorkOrder.Checked == true)
                            {
                                WorkOrder = txtWorkOrder.Text;
                                BindWorkOrder(null, null);
                            }

                            // 上传MES进行条码验证
                            VarifyBarcode_MES(null, null);

                            // 验证成功：2002=1；失败：2004=1；
                            if (Parameter_txt[2002] == "1")
                            {
                                lblRunningStatus.ForeColor = Color.Green;
                                lblRunningStatus.Text = barcodeValidation.PassPrompt;
                                lblValidationStatus.ForeColor = Color.Green;

                                try
                                {
                                    KeyenceMcNet.Write(barcodeValidation.PassBarcodeEndPLC, 1);
                                }
                                catch (Exception ex)
                                {
                                    LogMsg(ex.ToString());
                                }
                                LogMsg("反馈条码验证【D1003】 = 1");
                            }
                            else if (Parameter_txt[2004] == "1")
                            {
                                lblRunningStatus.ForeColor = Color.Red;
                                lblRunningStatus.Text = barcodeValidation.MesErrorPrompt;  // MES条码验证失败
                                lblValidationStatus.ForeColor = Color.Red;
                                try
                                {
                                    KeyenceMcNet.Write(barcodeValidation.ErrorBarcodeEndPLC, 1);
                                }
                                catch (Exception ex)
                                {
                                    LogMsg(ex.ToString());
                                }
                                LogMsg("反馈条码验证【D1005】 = 1");
                            }

                            Thread.Sleep(200);
                            Application.DoEvents();
                        }

                        lblOperatePrompt.Text = resources.GetString("ScanBarCode_OK");  // 扫码完成
                        LogMsg("条码读取完成.........");
                    }));
                }

                //扫物料，置3
                var Read_data = KeyenceMcNet.ReadInt32(barcodeVefictnList[0].BarcodeStartPLC).Content;
                if (Read_data == 3)
                {
                    Invoke(new Action(() =>
                    {
                        barcodeValidation = barcodeVefictnList[0];
                        ushort barcodeLength = barcodeValidation.GetBarcodeLength();
                        string rawBarcode = KeyenceMcNet.ReadString(barcodeValidation.BarcodePositionPLC, barcodeLength).Content;
                        barcodeData = CodeNum.FormatString(rawBarcode);
                        txtShowBarcode.Text = barcodeData;
                        LogMsg($"条码【D1100】 = {barcodeData}");

                        if (string.IsNullOrEmpty(this.barcodeData))
                        {
                            lblScanBarcodeStatus.ForeColor = R;
                            txtShowBarcode.Text = resources.GetString("barCode_State");
                            LogMsg("未获取到条码，请重新扫描！");
                            try
                            {
                                //KeyenceMcNet.Write("D1005", 1);
                                KeyenceMcNet.Write(barcodeValidation.ErrorBarcodeEndPLC, 1);
                                LogMsg("反馈条码验证【D1005】 = 1");
                            }
                            catch (Exception ex)
                            { LogMsg(ex.ToString()); }
                            return;
                        }
                        string[] frockA = textBox42.Text.ToString().Split('+');
                        if (frockA.Contains(this.barcodeData))
                        {
                            lblScanBarcodeStatus.ForeColor = G;
                            lblRunningStatus.ForeColor = G;
                            lblRunningStatus.Text = resources.GetString("material_OK");
                            try
                            {

                                //KeyenceMcNet.Write("D1003", 3);
                                KeyenceMcNet.Write(barcodeValidation.PassBarcodeEndPLC, 3);
                            }
                            catch (Exception ex)
                            { LogMsg(ex.ToString()); }
                            LogMsg("反馈条码验证【D1003】 =3");
                            return;
                        }
                        else
                        {
                            lblRunningStatus.ForeColor = R;
                            lblRunningStatus.Text = resources.GetString("material_NG");
                            lblValidationStatus.ForeColor = R;
                            LogMsg("物料验证失败，请重新扫描！");
                            try
                            {
                                //KeyenceMcNet.Write("D1005", 3);
                                KeyenceMcNet.Write(barcodeValidation.ErrorBarcodeEndPLC, 3);
                                LogMsg("反馈条码验证【D1005】 =3");
                            }
                            catch (Exception ex)
                            { LogMsg(ex.ToString()); }
                            return;
                        }
                    }));
                }
            }
        }

        private bool VarifyBarcodeRule(BarcodeVefictn barcodeVefictn, string barcodeRule)
        {
            // 条码验证规则为空
            if (string.IsNullOrWhiteSpace(barcodeRule))
            {
                lblRunningStatus.ForeColor = R;
                lblRunningStatus.Text = barcodeVefictn.ErrorPrompt;
                lblValidationStatus.ForeColor = R;
                LogMsg("没有选择条码验证规则！！！");
                try
                {
                    KeyenceMcNet.Write(barcodeVefictn.ErrorBarcodeEndPLC, 1);
                    LogMsg("反馈条码验证【D1005】 = 1");
                }
                catch (Exception ex)
                {
                    LogMsg(ex.ToString());
                }
                return false;
            }
            else
            {
                int ruleLength = barcodeRule.Length;
                bool isVarifySuccessful = false;

                // 条码是否符合规范
                if (barcodeData.Length > 12 && barcodeData.Length > ruleLength)
                {
                    string actualBarcodePrefix = barcodeData.Substring(0, ruleLength);
                    string[] validationRule = barcodeRule.Split('|');

                    // 验证通过
                    if (validationRule.Contains(actualBarcodePrefix))
                    {
                        isVarifySuccessful = true;
                    }
                }

                // 验证失败
                if (isVarifySuccessful == false)
                {
                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = barcodeVefictn.ErrorPrompt;
                    lblValidationStatus.ForeColor = Color.Red;
                    LogMsg("实际条码前缀与条码验证规则不匹配！！！");
                    try
                    {
                        KeyenceMcNet.Write(barcodeVefictn.ErrorBarcodeEndPLC, 1);
                        LogMsg("反馈条码验证【D1005】 = 1");
                    }
                    catch (Exception ex)
                    {
                        LogMsg(ex.ToString());
                    }
                    return false;
                }
            }
            return true;
        }

        public Label lb = new Label();
        Color G = Color.Green;
        Color R = Color.Red;
        Color W = Color.White;
        Color B = Color.Black;
        Color O = Color.Orange;

        Stopwatch sw = Stopwatch.StartNew();
        /// <summary>
        /// 获取生产结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button20_Click(object sender, EventArgs e)
        {
            if (isPlcConnected == true)
            {
                // D1200:
                var short_D100 = KeyenceMcNet.ReadInt32(sytemSetDerivedsd.StartProductPoint).Content;

                if (short_D100 == 1)
                {
                    Invoke(new Action(() =>
                   {
                       //Console.WriteLine("开始读取数据 = " + DateTime.Now.Hour.ToString() +
                       //   DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" +
                       //   DateTime.Now.Millisecond.ToString() + ":" + short_D100);
                       lblOperatePrompt.Text = resources.GetString("begin_read_data");  // 开始读取数据
                       barcodeInfo = barcodeData;
                       LogMsg("生产结果数据读取中....");
                       string[] Read_string = new string[10000];
                       //string[] Read_string1 = new string[100];
                       list = new List<string>();
                       beatList = new List<string>();
                       maxList = new List<string>();//
                       minList = new List<string>();
                       resultList = new List<string>();

                       // 二次读条码（适用于转盘机台）
                       if (chkReadBarcodeSecondly.Checked == true)
                       {
                           // D1050
                           ushort secondProductLength = 10;
                           ushort.TryParse(sytemSetDerivedsd.SecondProductLength, out secondProductLength);
                           string rawBarcode = KeyenceMcNet.ReadString(sytemSetDerivedsd.SecondProductPoint, secondProductLength).Content;
                           barcodeInfo = CodeNum.FormatString(rawBarcode);
                       }

                       // 读取测试项对应的具体测试数据
                       if (boardTable.Rows.Count > 0)
                       {
                           for (int i = 0; i < boardTable.Rows.Count; i++)
                           {
                               list.Add(ProcessPointData_PLC(boardTable.Rows[i]["BoardCode"].ToString()));            // 实际值
                               maxList.Add(ProcessPointData_PLC(boardTable.Rows[i]["MaxBoardCode"].ToString()));      // 上限值
                               minList.Add(ProcessPointData_PLC(boardTable.Rows[i]["MinBoardCode"].ToString()));      // 下限值
                               resultList.Add(ProcessPointData_PLC(boardTable.Rows[i]["ResultBoardCode"].ToString()));// 测试结果
                               beatList.Add(ProcessPointData_PLC(boardTable.Rows[i]["BeatBoardCode"].ToString()));    // 节拍
                           }
                       }

                       // 产品结果（产品状态）D1078 TotalProductPoint
                       Read_string[3688] = KeyenceMcNet.ReadInt32(sytemSetDerivedsd.TotalProductPoint).Content.ToString();//产品状态
                       Parameter_txt[3688] = Convert.ToString(Convert.ToDouble(Read_string[3688]));

                   }));

                    Invoke(new Action(() =>
                    {
                        if (Parameter_txt[3688] == "3")
                        {
                            lblProductResult.Text = "OK";
                            lblProductResult.ForeColor = W;
                            lblProductResult.BackColor = G;
                            Value[9999] = "OK";
                        }
                        else
                        {
                            lblProductResult.Text = "NG";
                            lblProductResult.ForeColor = W;
                            lblProductResult.BackColor = R;
                            Value[9999] = "NG";
                        }
                    }));

                    // 上传数据
                    if (OffLineType == 0)
                    {
                        Invoke(new Action(() =>
                        {
                            lblRunningStatus.ForeColor = B;
                            lblRunningStatus.Text = resources.GetString("Mes_upload");  // 联机数据上传中
                            lblOperatePrompt.Text = resources.GetString("Wait");        // 请等待
                            lblUploadStatus.ForeColor = O;

                            if (Value[9999] == "OK")
                            {
                                产品结果 = true;
                            }
                            else
                            {
                                产品结果 = false;
                            }

                            Button7_Click(null, null);

                            if (Parameter_txt[2006] == "1")
                            {
                                lblRunningStatus.Text = resources.GetString("Mes_upload_OK");
                                lblUploadStatus.ForeColor = G;
                                //KeyenceMcNet.Write("D3672", Convert.ToInt16(1));
                            }
                            else if (Parameter_txt[2008] == "1")
                            {
                                lblRunningStatus.Text = resources.GetString("Mes_upload_NG");
                                lblUploadStatus.ForeColor = R;
                                lblOperatePrompt.Text = resources.GetString("Re_upload");
                                // KeyenceMcNet.Write("D3674", Convert.ToInt16(1));
                            }
                        }));
                    }

                    // 生产数据上传看板
                    if (isDashboardConnected)
                    {
                        Thread thread = new Thread(SendReceived);
                        thread.IsBackground = true;
                        thread.Start();
                    }

                    // 生产数据保存到本地
                    Invoke(new Action(() =>
                    {
                        Num = Num + 1;
                        Thread threadLeft = new Thread(显示结果_Left);
                        threadLeft.IsBackground = true;
                        threadLeft.Start();

                        lblRunningStatus.Text = resources.GetString("Read_data");   // 本地数据保存中
                        lblOperatePrompt.Text = resources.GetString("Wait");        // 请等待
                        Thread threadsaveInfo = new Thread(saveInfo);
                        threadsaveInfo.IsBackground = true;
                        threadsaveInfo.Start();

                        lblRunningStatus.Text = resources.GetString("Read_data_OK");        // 本地数据保存完成 
                        lblOperatePrompt.Text = resources.GetString("Continue_production"); // 请取下产品继续生产
                        LogMsg("数据读取完成");
                        LogMsg("生产结果读取完成...");

                        try
                        {
                            // D1202 EndProductPoint
                            KeyenceMcNet.Write(sytemSetDerivedsd.EndProductPoint, 1);
                        }
                        catch (Exception ex)
                        {
                            LogMsg(ex.ToString());
                        }
                        LogMsg("生产结果读取反馈【D1202】 = 1");

                        Thread.Sleep(200);
                        Application.DoEvents();
                        lblRunningStatus.Text = resources.GetString("scanning");
                    }));
                }
            }
        }

        /// <summary>
        /// 读取 PLC 值并根据指定的格式返回结果。
        /// </summary>
        /// <param name="plcPointInfo"></param>
        /// <returns></returns>
        private static string ProcessPointData_PLC(string plcPointInfo)
        {
            string outputStr = "";

            if (plcPointInfo.Equals("NO"))
            {
                outputStr = "null";
            }

            else if (plcPointInfo.Contains(":"))
            {
                // 获取索引标志 D1090:H-3
                int index = plcPointInfo.IndexOf(":");
                int index1 = plcPointInfo.IndexOf("-");

                // 获取类型
                string dataType = plcPointInfo.Substring(index + 1, 1);

                // 获取保留小数位
                ushort length = 0;
                ushort.TryParse(plcPointInfo.Substring(index1 + 1), out length);

                // 获取PLC点位
                string plcAddress = plcPointInfo.Substring(0, index);

                if (dataType == "H")
                {
                    string aa = "";
                    OperateResult<short> result = KeyenceMcNet.ReadInt16(plcAddress);

                    if (result.IsSuccess)
                    {
                        aa = TypeRead.Typerdess(result.Content.ToString(), length.ToString());
                    }
                    else
                    {
                        aa = "null";
                    }
                    outputStr = aa;

                }        // short

                else if (dataType == "I")
                {
                    string aa = "";
                    OperateResult<int> readss = KeyenceMcNet.ReadInt32(plcAddress);

                    if (readss.IsSuccess)
                    {
                        aa = TypeRead.Typerdess(readss.Content.ToString(), length.ToString());
                    }
                    else
                    {
                        aa = "null";
                    }
                    outputStr = aa;

                }   // Int32

                else if (dataType == "F")
                {
                    string aa = "";
                    OperateResult<float> readss = KeyenceMcNet.ReadFloat(plcAddress);

                    if (readss.IsSuccess)
                    {
                        aa = TypeRead.Typerdess(readss.Content.ToString(), length.ToString());
                    }
                    else
                    {
                        aa = "null";
                    }
                    outputStr = aa;

                }   // float

                else if (dataType == "J")
                {
                    string aa = "";
                    OperateResult<int> readss = KeyenceMcNet.ReadInt32(plcAddress);

                    if (readss.IsSuccess)
                    {

                        aa = CodeNum.PdounInCode(readss.Content.ToString());
                    }
                    else
                    {
                        aa = "null";
                    }
                    outputStr = aa;

                }   // Int32

                else if (dataType == "N")
                {
                    string aa = "";
                    OperateResult<int> readss = KeyenceMcNet.ReadInt32(plcAddress);

                    if (readss.IsSuccess)
                    {
                        aa = CodeNum.PNumOKAG(readss.Content.ToString());
                    }
                    else
                    {
                        aa = "null";
                    }
                    outputStr = aa;

                }   // Int32

                else if (dataType == "O")
                {
                    string aa = "";
                    OperateResult<int> readss = KeyenceMcNet.ReadInt32(plcAddress);

                    if (readss.IsSuccess)
                    {
                        aa = readss.Content.ToString();
                    }
                    else
                    {
                        aa = "null";
                    }
                    outputStr = aa;

                }   // Int32

                else if (dataType == "S")
                {
                    string ss = "";
                    OperateResult<string> readss = KeyenceMcNet.ReadString(plcAddress, length);

                    if (readss.IsSuccess)
                    {
                        ss = CodeNum.FormatString(readss.Content.ToString());
                    }
                    else
                    {
                        ss = "null";
                    }
                    outputStr = ss;

                }   // string

                else
                {
                    var ss = KeyenceMcNet.ReadString(plcAddress, length).Content;
                    outputStr = ss;
                }                        // string
            }

            else
            {
                string aa = "";
                OperateResult<int> readss = KeyenceMcNet.ReadInt32(plcPointInfo);
                if (readss.IsSuccess)
                {

                    aa = CodeNum.PNumCode(readss.Content.ToString());
                }
                else
                {
                    aa = "null";
                }
                outputStr = aa;
            }

            return outputStr;
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="KeyenceMcNet"></param>
        public List<string> PCodenum(string[] strarr, string strtyp)
        {
            List<string> listarr = new List<string>();
            if (strarr.Length > 0)
            {
                string boardBeat = "   ";//
                for (int i = 0; i < strarr.Length; i++)
                {
                    string beatcode = strarr[i];//
                    if (beatcode.Contains("+"))
                    {
                        int count = 0;
                        string newStr = beatcode.Replace("+", "");
                        int.TryParse(newStr, out count);
                        for (int j = 0; j < count; j++)
                        {
                            listarr.Add(boardBeat);
                        }
                    }
                    else
                    {
                        if (beatcode.Equals("NO"))
                        {
                            boardBeat = "   ";
                            listarr.Add(boardBeat);
                        }
                        else
                        {
                            boardBeat = KeyenceMcNet.ReadInt32(beatcode).Content.ToString();
                            boardBeat = Bocmdb.BoardCode(boardBeat, strtyp);
                            listarr.Add(boardBeat);
                        }
                    }
                }
            }
            return listarr;
        }

        /// <summary>
        /// 导出生产数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportProductData(object sender, EventArgs e)
        {
            //打开文件对话框，导出文件
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "保存文件";
            saveFileDialog1.Filter = "Excel文件(*.xls,*.xlsx,*.xlsm)|*.xls,*.xlsx,*.xlsm";
            saveFileDialog1.FileName = "历史数据.xls"; //设置默认另存为的名字

            string dataBaseUrl = textBoxPath.Text + ".mdb";
            mdb = new mdbDatas(dataBaseUrl);
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string txtPath = this.saveFileDialog1.FileName;
                DateTime times_Start = dateTimePicker1.Value;
                DateTime times_End = dateTimePicker2.Value;
                string times_Start_string = times_Start.ToString();
                string times_End_string = times_End.ToString();//dateTimePicker2.Value.ToString("yyyy/MM/dd").ToString() + " 23:59:59";
                DateTime times_Start_Sub = DateTime.Parse(times_Start_string);
                DateTime times_End_Sub = DateTime.Parse(times_End_string);
                if (textBoxPath.Text.Length < 1)
                {
                    MessageBox.Show("请先选择左侧数据源！");
                    return;
                }

                StringBuilder sql = new StringBuilder();

                sql.Append("select * from [Sheet1] where CDate(Format(测试时间,'yyyy/MM/dd HH:mm:ss')) between  #" + times_Start_Sub + "#" + " and #" + Convert.ToDateTime(times_End_Sub) + "# ");

                if (textBox_Code.Text != "")
                {
                    sql.Append("and 条码 = '" + textBox_Code.Text + "'");
                }
                if (textBox1.Text != "")
                {
                    sql.Append("and 产品 = '" + textBox1.Text + "' ");
                }

                DataTable table = mdb.Find(sql.ToString());

                NPOIHelper.DataTableToExcel(table, txtPath);
            }
            mdb.CloseConnection();
        }

        /// <summary>
        /// MES参数 > 保存
        /// </summary>
        private void SaveMesConfig_Click(object sender, EventArgs e)
        {
            SaveParameter_MES();
            LoadParameter_MES();
            VarifyUserLogin_MES(null, null);
        }

        /// <summary>
        /// 选择本地文件存放路径
        /// </summary>
        private void ChangeStoragePath(object sender, EventArgs e)
        {
            string prePath = lblDataPath.Text;
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.lblDataPath.Text = path.SelectedPath;
            loggerConfig.Trace($"【变更存放路径】\n原先存放路径：{prePath}\n路径已变更为：{this.lblDataPath.Text}");
        }

        /// <summary>
        /// 历史数据 > 刷新目录
        /// </summary>
        private void BtnRefreshDirectory_Click(object sender, EventArgs e)
        {
            directoryTreeView.Nodes.Clear();        // 每次确定时需要刷新内容
            string selectedPath = lblDataPath.Text; // 获得输入框的内容

            // 文件路径存在
            if (Directory.Exists(selectedPath))
            {
                TreeNode rootNode = new TreeNode(selectedPath); // 创建树节点
                directoryTreeView.Nodes.Add(rootNode);          // 加入视图
                FindDirectory(selectedPath, rootNode);          // 通过递归函数进行目录的遍历
            }
        }

        /// <summary>
        /// 递归函数 遍历当前目录
        /// </summary>
        void FindDirectory(string nowDirectory, TreeNode parentNode)
        {
            try  // 当文件目录不可访问时，需要捕获异常
            {
                // 获取当前目录下的所有文件夹数组
                string[] directoryArray = Directory.GetFiles(nowDirectory);
                if (directoryArray.Length > 0)
                {
                    foreach (string item in directoryArray)
                    {
                        // 遍历数组，将节点添加到父亲节点的
                        string str = Path.GetFileNameWithoutExtension(item);
                        TreeNode node = new TreeNode(str);
                        parentNode.Nodes.Add(node);
                        //FindDirectory(item, node);
                    }
                }
            }
            catch (Exception)
            {
                parentNode.Nodes.Add("禁止访问");
            }
        }

        private void directoryTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (directoryTreeView.SelectedNode.FullPath == directoryTreeView.SelectedNode.Text)
            {
                string table = directoryTreeView.SelectedNode.Text;     // 数据表名

                directoryTreeView.SelectedNode.Expand();                // 展开选中的节点
            }
            else
            {
                int count = directoryTreeView.SelectedNode.Text.Length; // 获取选中节点字符长度
                string str = directoryTreeView.SelectedNode.FullPath;   // 获取选中节点从父节点到目标节点的路径

                textBoxPath.Text = str;
            }
        }

        /// <summary>
        /// 变更工单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeWorkOrder_Click(object sender, EventArgs e)
        {
            mdb = new mdbDatas(path4);
            DataTable table1 = mdb.Find("select * from SytemSet where ID = '1'");
            if (table1.Rows.Count > 0)
            {
                string sql = "update [SytemSet] set [wordNo]='" + txtWorkOrder.Text + "' where [ID] = '1'";
                var result = mdb.Change(sql);
                if (result == true)
                {
                    MessageBox.Show("变更成功");
                }
            }
            mdb.CloseConnection();
        }

        #region ------------ 用户管理 ------------

        DataTable userCollection;

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // OP = 1, PE = 2, ADM = 3, DEV = 4, QE = 5, ME = 6
            int[] OpAuth = { 1, 2, 5, 6 };                      // 只能注册操作员权限
            string[] restrictedUsers = { "PE", "QE", "ME" };    // 只能由 ADM 进行管理的权限
            int[] topLevel = { 3, 4 };                          // 最高权限用户
            // 获取数据库中，用户工号的集合
            string[] userID = userCollection.AsEnumerable()
                                  .Select(row => row.Field<string>("工号"))
                                  .ToArray();

            // 确认当前用户是否为 OP、PE、QE、ME
            bool isOpAuth = OpAuth.Contains(Access);
            // 确认当前用户是否为 ADM、DEV
            bool isTopLevel = topLevel.Contains(Access);

            // 检查必填项是否为空
            if (UID.Text.Trim().Length < 1 || textBox22.Text.Trim().Length < 1 || UPWD.Text.Trim().Length < 1)
            {
                MessageBox.Show("工号、姓名、密码为必填项！", "提示");
                return;
            }

            // 确保 OP、PE、QE、ME 用户只能注册 OP 权限
            if (isOpAuth && UTYPE.Text != "OP")
            {
                MessageBox.Show("您只能注册操作员权限，请重新选择！", "提示");
                return;
            }

            // 确保 PE、QE、ME 用户只能由 ADM、DEV 等级用户进行管理
            if (restrictedUsers.Contains(UTYPE.Text) && !isTopLevel)
            {
                MessageBox.Show("您没有权限更改此用户等级，请重新选择！", "提示");
                return;
            }

            // 确保 OP 可以被注册
            if (UTYPE.Text == "OP" && !userID.Contains(UID.Text))
            {

            }
            // 防止除最高权限以外的权限将其他权限被篡改为 OP 权限
            else if (UTYPE.Text == "OP" && lblCurrentSelected.Text != "OP" && !isTopLevel)
            {
                MessageBox.Show("您没有权限更改此用户等级，请重新选择！", "提示");
                return;
            }

            string sql = "";
            mdb = new mdbDatas(userFileuRL);
            DataTable table1 = mdb.Find("select * from Users where 工号 = '" + UID.Text + "'");
            if (table1.Rows.Count > 0)
            {
                //if (comboBox3.SelectedIndex == 1)
                //{
                if (tbxBrandID.Text.Length > 0)
                {
                    DataTable table = mdb.Find("select * from Users where [厂牌UID] = '" + tbxBrandID.Text + "' and [厂牌UID] <> '' and [工号] <> '" + UID.Text + "' ");
                    if (table.Rows.Count > 0)
                    {
                        if (tbxBrandID.Text == table.Rows[0]["厂牌UID"].ToString())
                        {
                            MessageBox.Show("不能重复注册、请检查厂牌UID是否重复！");
                            return;
                        }

                    }
                }
                sql = "update [Users] set [用户密码]='" + UPWD.Text + "',[用户权限]='" + UTYPE.Text + "',[用户名]='" + textBox22.Text + "',[厂牌UID]='" + tbxBrandID.Text + "'" +
                             " where [工号] = '" + UID.Text + "'";
                //}
                //else 
                //{
                //    sql = "update [Users] set [工号]='" + UID.Text + "',[用户密码]='" + UPWD.Text + "',[用户权限]='" + UTYPE.Text + "',[登录方式]='" + GetAuthorityByCode(comboBox3.SelectedIndex) + "',[用户名]='" + textBox22.Text + "',[厂牌UID]=' '" +
                //              " where [工号] = '" + UID.Text + "'";
                //}

                var result = mdb.Change(sql);
                if (result == true)
                {
                    MessageBox.Show("修改成功");
                }
            }
            else
            {
                //if (comboBox3.SelectedIndex == 1)
                //{
                if (tbxBrandID.Text.Length > 0)
                {
                    DataTable table = mdb.Find("select * from Users where [厂牌UID] = '" + tbxBrandID.Text + "' and [厂牌UID] <> '' ");
                    if (table.Rows.Count > 0)
                    {
                        MessageBox.Show("不能重复注册、请检查厂牌UID是否重复！");
                        return;
                    }
                }

                sql = "insert into Users ([工号],[用户密码],[用户权限],[用户名],[厂牌UID]) values ('" + UID.Text + "','" + UPWD.Text + "','" + UTYPE.Text + "','" + textBox22.Text + "','" + tbxBrandID.Text + "')";
                //}
                //else 
                //{
                //     sql = "insert into Users ([工号],[用户密码],[用户权限],[登录方式],[用户名]) values ('" + UID.Text + "','" + UPWD.Text + "','" + UTYPE.Text + "','" + GetAuthorityByCode(comboBox3.SelectedIndex) + "','" + textBox22.Text + "')";
                //}


                bool result = mdb.Add(sql.ToString());
                if (result == true)
                {
                    MessageBox.Show("新增成功");
                }

            }
            btnRefreshUser_Click(null, null);
            mdb.CloseConnection();

            string modifyInfo = SqlToJsonConverter.ConvertToJSON(sql);
            string final = SqlToJsonConverter.ConvertToCustomLog(modifyInfo);
            loggerAccount.Trace(final);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            // OP = 1, PE = 2, ADM = 3, DEV = 4, QE = 5, ME = 6
            int[] OPAuth = new int[] { 1, 2, 5, 6 };
            bool resultUser = OPAuth.Contains(Access);

            if (resultUser && UTYPE.Text != "OP")
            {
                MessageBox.Show("您无权进行更改！");
                return;
            }

            // 获取当前的行
            int rowindex = dataGridView1.CurrentRow.Index;

            // 将获取到的当前行转为id
            string id = (string)dataGridView1.Rows[rowindex].Cells[4].Value;

            // 连接到数据库
            mdb = new mdbDatas(userFileuRL);

            // 删除前先执行查找
            string selectSql = $"SELECT [用户名], [用户权限] FROM [Users] WHERE [工号] = '{id}'";
            DataTable userInfo = mdb.Find(selectSql);

            string username = "";
            string permissions = "";
            if (userInfo.Rows.Count > 0)
            {
                username = userInfo.Rows[0]["用户名"].ToString();
                permissions = userInfo.Rows[0]["用户权限"].ToString();
            }

            string sql = $" DELETE FROM [Users] WHERE [工号] = '{id}' ";
            bool bl = mdb.Del(sql);
            if (bl == true)
            {
                MessageBox.Show("删除成功");
                // 记录日志
                string delInfo = $"【用户删除】\n工号：{id} | 姓名：{username} | 权限：{permissions}";
                loggerAccount.Trace(delInfo);
            }
            btnRefreshUser_Click(null, null);
            mdb.CloseConnection();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshUser_Click(object sender, EventArgs e)
        {
            mdb = new mdbDatas(userFileuRL);
            userCollection = mdb.Find("select 用户名,用户密码,用户权限,厂牌UID,工号 from Users where 用户权限 <> 'DEV'");
            userInfoEntities = DataConverter.ConvertDataTableToList(userCollection);
            dataGridView1.DataSource = userCollection;
            mdb.CloseConnection();
        }

        /// <summary>
        /// 登录方式转换
        /// </summary>
        /// <param name="authorityName"></param>
        /// <returns></returns>
        public string GetAuthorityByCode(int authorityName)
        {
            string aCode = "";
            switch (authorityName)
            {
                case 0:
                    aCode = "密码";
                    break;
                case 1:
                    aCode = "刷卡";
                    break;
                default:
                    aCode = "密码";
                    break;
            }
            return aCode;
        }

        /// <summary>
        /// 编码转换名称
        /// </summary>
        /// <param name="authorityName"></param>
        /// <returns></returns>
        public int GetCodeByAuthority(string Code)
        {
            int aCode = 0;
            switch (Code)
            {
                case "密码":
                    aCode = 0;
                    break;
                case "刷卡":
                    aCode = 1;
                    break;
                default:
                    aCode = 0;
                    break;
            }
            return aCode;
        }

        /// <summary>
        /// 修改登录次数和登录时间
        /// </summary>
        public void UpLoginInfo()
        {
            mdb = new mdbDatas(userFileuRL);
            DataTable table = mdb.Find("select 登录次数 from Users where [工号] = '" + LoginUser + "'");
            if (table.Rows.Count > 0)
            {
                int i = 0;
                if (!string.IsNullOrWhiteSpace(table.Rows[0][0].ToString()))
                {
                    i = int.Parse(table.Rows[0][0].ToString());
                }
                string sql = "update [Users] set [最后登录时间]='" + DateTime.Now.ToString() + "',[登录次数]='" + (i + 1) + "' where [工号] = '" + LoginUser + "'";

                var result = mdb.Change(sql);
                //if (result == true)
                //{
                //    MessageBox.Show("修改成功");
                //}
            }
            mdb.CloseConnection();
        }

        /// <summary>
        /// 选中行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                this.UID.Text = this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                this.UPWD.Text = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.textBox22.Text = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                this.tbxBrandID.Text = this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                this.UTYPE.Text = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.lblCurrentSelected.Text = UTYPE.Text;
                //this.comboBox3.SelectedIndex = GetCodeByAuthority(this.dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
            }
        }

        #endregion

        #region ------------ 设置验证条码与读取PLC ------------

        /// <summary>
        /// 设置验证条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button22_Click(object sender, EventArgs e)
        {
            mdb = new mdbDatas(path4);
            DataTable table1 = mdb.Find("select * from SytemSet where ID = '1'");
            if (table1.Rows.Count > 0)
            {
                string comtb = "0";
                if (!string.IsNullOrWhiteSpace(cboBarcodeRuleAndFixtures.Text))
                {
                    comtb = cboBarcodeRuleAndFixtures.SelectedValue.ToString();
                }
                string sql = $"update [SytemSet] set [faults]='{comtb}', Workstname ='{chkReadRecipeId_PLC.Checked}' where [ID] = '1'";
                var result = mdb.Change(sql);
                if (result == true)
                {
                    lblRecipeId.Text = comtb;
                    MessageBox.Show("保存成功");
                    // 记录操作日志
                    bool isChecked = chkReadRecipeId_PLC.Checked;
                    string readPlcStatus = isChecked ? "是" : "否";
                    string msgSave = $"【配方操作保存成功】\n是否读取PLC：{readPlcStatus}\n当前配方号：{lblRecipeId.Text}";
                    loggerConfig.Trace(msgSave);
                }
                //SqlToJsonConverter.ConvertToJSONLog(sql);
            }

            mdb.CloseConnection();


        }
        /// <summary>
        /// 发给PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click_1(object sender, EventArgs e)
        {
            if (chkReadRecipeId_PLC.Checked)
            {
                MessageBox.Show("读取PLC中，不能发送！");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(cboBarcodeRuleAndFixtures.Text))
                {
                    MessageBox.Show("请选择条码验证信息！");
                    return;
                }
                try
                {
                    //D1204
                    KeyenceMcNet.Write(deviceInfo.FormulaModify, 1);
                    //D1206
                    KeyenceMcNet.Write(deviceInfo.FormulaNumModify, int.Parse(cboBarcodeRuleAndFixtures.SelectedValue.ToString()));
                    MessageBox.Show("发送PLC成功！");
                }
                catch (Exception ex)
                {
                    LogMsg(ex.ToString());
                    MessageBox.Show("发送PLC失败！");
                }
            }
        }
        #endregion

        #region ------------ 分页 ------------

        Pager pager = null;
        //将分页面数据显示出来
        private void PageLoad()
        {
            dataGridViewDynamic1.DataSource = pager.LoadPage();//显示数据
            this.textBox13.Text = pager.pageSize.ToString();//每页的条
            this.label34.Text = pager.currentPage + "/" + pager.pageCount;//当前页0/0
            this.label36.Text = pager.recordCount.ToString();//  共条
        }

        //首页
        private void button4_Click(object sender, EventArgs e)
        {
            if (pager != null)
            {
                pager.currentPage = 1;
                PageLoad();//显示分页数据
            }
        }
        //上一页
        private void button9_Click_1(object sender, EventArgs e)
        {
            if (pager != null)
            {
                pager.currentPage--;
                PageLoad();//显示分页数据
            }
        }
        //下一页
        private void button11_Click_1(object sender, EventArgs e)
        {
            if (pager != null)
            {
                pager.currentPage++;
                PageLoad();//显示分页数据
            }
        }
        //尾页
        private void button12_Click(object sender, EventArgs e)
        {
            if (pager != null)
            {
                pager.currentPage = pager.pageCount;
                PageLoad();//显示分页数据
            }
        }
        //跳转
        private void button14_Click(object sender, EventArgs e)
        {
            // pager.currentPage = this.textBox16.Text;
            if (pager != null)
            {
                int i;
                if (int.TryParse(this.textBox16.Text, out i))
                {
                    pager.currentPage = i;
                    PageLoad();//显示分页数据
                }
            }

        }
        //重新分页
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (pager != null)
            {
                int i;
                if (int.TryParse(this.textBox13.Text, out i))
                {
                    if (i != 0) pager.pageSize = i;
                    pager.fenye();//分页
                    PageLoad();//显示分页数据
                }

            }
        }



        #endregion

        #region ------------ 看板数据库设置 ------------

        Socket socketSend;
        private bool isDashboardConnected = false;  // 看板连接状态
        private Action<string> ShowMsgAction;
        DataTable vubPartsTable;                    // 易损件表
        DataTable faultsTable;                      // 故障信息表

        /// <summary>
        /// Socket设置加载
        /// </summary>
        private void SYS_Socket_Mo()
        {
            BtnRefreshAtBulletin(null, null);

            mdb = new mdbDatas(path4);
            DataTable table1 = mdb.Find("select * from SytemSocket where ID = 1");

            for (int i = 0; i < table1.Rows.Count; i++)
            {
                for (int j = 0; j < table1.Columns.Count; j++)
                {
                    txtDashboardIP.Text = table1.Rows[i]["IP"].ToString();
                    txtDashboardPort.Text = table1.Rows[i]["Port"].ToString();

                    string device = table1.Rows[i]["Devicestatu"].ToString();
                    if (device == "True") chkEnableDashboard.Checked = true;
                    txtStationNameSets.Text = table1.Rows[i]["BoardName"].ToString();

                    txtProductName.Text = table1.Rows[i]["BoardTheory"].ToString();
                    txtFaultStartPoint.Text = table1.Rows[i]["FaultCode"].ToString();
                    txtFaultLength.Text = table1.Rows[i]["FaultLeng"].ToString();
                    string cokPosi = table1.Rows[i]["BoardPosition"].ToString();
                    if (cokPosi == "True") chkReadPName.Checked = true;
                }
            }
            mdb.CloseConnection();

            // 创建一个新的列对象并设置其属性保存
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "操作";
            buttonColumn.Text = "保存";
            buttonColumn.Name = "btnCol";
            buttonColumn.DefaultCellStyle.NullValue = "保存";
            dgvWeakInfo.Columns.Add(buttonColumn);

            // 再次创建一个新的列对象并设置其属删除
            DataGridViewButtonColumn anotherButtonColumn = new DataGridViewButtonColumn();
            anotherButtonColumn.HeaderText = "操作"; // 第二个按钮的标题文本
            anotherButtonColumn.Name = "btnCol2"; // 第二个按钮的名称
            anotherButtonColumn.DefaultCellStyle.NullValue = "删除";
            dgvWeakInfo.Columns.Add(anotherButtonColumn);

            DataGridViewButtonColumn butnCo = new DataGridViewButtonColumn();
            butnCo.HeaderText = "操作";
            butnCo.Text = "保存";
            butnCo.Name = "btnCol";
            butnCo.DefaultCellStyle.NullValue = "保存";
            dgvFaultInfo.Columns.Add(butnCo);

            DataGridViewButtonColumn anotrButCo = new DataGridViewButtonColumn();
            anotrButCo.HeaderText = "操作"; // 第二个按钮的标题文本
            anotrButCo.Name = "btnCol2"; // 第二个按钮的名称
            anotrButCo.DefaultCellStyle.NullValue = "删除";
            dgvFaultInfo.Columns.Add(anotrButCo);
        }

        /// <summary>
        /// 刷新易损件表和故障信息表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshAtBulletin(object sender, EventArgs e)
        {
            mdb = new mdbDatas(path4);

            vubPartsTable = mdb.Find("select  [ID] as 编号, [WorkID] as 工位ID, [BoardPosition] as 易损件所在的位置, " +
                "[BoardName] as 易损件的名称, [BoardTheory] as 理论使用次数PLC点位, " +
                "[BoardCode] as 已经使用的PLC点位 from VulnbleParts");

            vubPartsTable.Columns["编号"].AutoIncrement = true;
            vubPartsTable.Columns["编号"].ReadOnly = true;
            vubPartsTable.Columns["编号"].AutoIncrementSeed = 0;

            int rowMax = 0;
            if (vubPartsTable.Rows.Count > 0)
            {
                rowMax = vubPartsTable.AsEnumerable().Max(row => row.Field<int>("编号"));
            }

            DataRow newRow = vubPartsTable.NewRow();
            newRow["编号"] = rowMax;

            // 刷新易损件数据
            dgvWeakInfo.DataSource = vubPartsTable;

            // 刷新故障信息
            faultsTable = mdb.Find("select ID as 编号, [WorkID] as 工位ID, [CodeID] as 故障点位, Faults as 故障描述 from SytemFaults");
            dgvFaultInfo.DataSource = faultsTable;
            mdb.CloseConnection();
        }

        /// <summary>
        /// 易损件1.删除2.保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            { return; }

            // 需要记录到日志的字段
            string[] logFields = { "WorkID", "BoardPosition", "BoardName", "BoardTheory", "BoardCode" };

            // 定义字段与别名的映射
            Dictionary<string, string> fieldAliases = new Dictionary<string, string>
            {
                { "WorkID", "工位序号" },
                { "BoardPosition", "易损件所在的位置" },
                { "BoardName", "易损件的名称" },
                { "BoardTheory", "理论使用次数PLC点位" },
                { "BoardCode", "已经使用的PLC点位" },
            };

            // 删除
            DeleteRowFromDataGridView<int>(dgvWeakInfo, e, "VulnbleParts", "ID", logFields, 2, path4, "btnCol2", fieldAliases, "易损件数据");

            //保存
            if (dgvWeakInfo.Columns[e.ColumnIndex].Name == "btnCol")
            {
                //说明点击的列是DataGridViewButtonColumn列
                DataGridViewColumn column = dgvWeakInfo.Columns[e.ColumnIndex];
                string pid = this.dgvWeakInfo.Rows[e.RowIndex].Cells[2].Value.ToString();
                string workid = this.dgvWeakInfo.Rows[e.RowIndex].Cells[3].Value.ToString();
                string bdPosition = this.dgvWeakInfo.Rows[e.RowIndex].Cells[4].Value.ToString();
                string bdName = this.dgvWeakInfo.Rows[e.RowIndex].Cells[5].Value.ToString();
                string bdTheory = this.dgvWeakInfo.Rows[e.RowIndex].Cells[6].Value.ToString();
                string bdCode = this.dgvWeakInfo.Rows[e.RowIndex].Cells[7].Value.ToString();

                mdb = new mdbDatas(path4);
                DataTable table1 = mdb.Find("select * from VulnbleParts where [ID] = " + pid);

                // 修改
                if (table1.Rows.Count > 0)
                {
                    DataRow row = table1.Rows[0];

                    // 修改前的详细数据
                    string logDetail = $"编号：{row["ID"]} | 工位序号：{row["WorkID"]} | " +
                        $"易损件所在的位置：{row["BoardPosition"]} | 易损件的名称：{row["BoardName"]} | " +
                        $"理论使用次数PLC点位：{row["BoardTheory"]} | 已经使用的PLC点位：{row["BoardCode"]}";

                    string sql = $"update [VulnbleParts] set [WorkID] = '{workid}', [BoardPosition] = '{bdPosition}', " +
                    $"[BoardName] = '{bdName}', [BoardTheory] = '{bdTheory}', [BoardCode] = '{bdCode}' where [ID] = {pid}";

                    var result = mdb.Change(sql);
                    if (result == true)
                    {
                        MessageBox.Show("修改成功");
                        string modifyInfo = $"【易损件数据修改成功】\n修改前的详细信息：\n{logDetail}\n修改后的详细信息：\n" +
                            $"编号：{pid} | 工位序号：{workid} | 易损件所在的位置：{bdPosition} | 易损件的名称：{bdName} | " +
                            $"理论使用次数PLC点位：{bdTheory} | 已经使用的PLC点位：{bdCode}";
                        loggerConfig.Trace(modifyInfo);
                    }
                }

                // 新增
                else
                {
                    string sql = $"insert into VulnbleParts ([ID], [WorkID], [BoardPosition], [BoardName], [BoardTheory], [BoardCode]) " +
                                 $"values ({pid}, '{workid}', '{bdPosition}', '{bdName}', '{bdTheory}', '{bdCode}')";
                    bool result = mdb.Add(sql.ToString());
                    if (result == true)
                    {
                        MessageBox.Show("新增成功");
                        string insertInfo = $"【易损件数据新增成功】\n新增详情：\n" +
                            $"编号：{pid} | 工位序号：{workid} | 易损件所在的位置：{bdPosition} | 易损件的名称：{bdName} | " +
                            $"理论使用次数PLC点位：{bdTheory} | 已经使用的PLC点位：{bdCode}";
                        loggerConfig.Trace(insertInfo);
                    }
                }

                mdb.CloseConnection();
                //button18_Click(null, null);
            }

        }

        /// <summary>
        /// 故障信息 1.删除2.保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            { return; }

            // 需要记录日志的字段
            string[] logField = { "WorkID", "CodeID", "Faults" };

            // 定义字段与别名的映射
            Dictionary<string, string> fieldAliases = new Dictionary<string, string>
            {
                { "WorkID", "工位序号" },
                { "CodeID", "故障点位" },
                { "Faults", "故障描述" },
            };

            // 删除
            DeleteRowFromDataGridView<string>(dgvFaultInfo, e, "SytemFaults", "ID", logField, 2, path4, "btnCol2", fieldAliases, "故障信息");

            // 保存
            if (dgvFaultInfo.Columns[e.ColumnIndex].Name == "btnCol")
            {
                DataGridViewColumn column = dgvFaultInfo.Columns[e.ColumnIndex];
                string pid = this.dgvFaultInfo.Rows[e.RowIndex].Cells[2].Value.ToString();
                string workID = this.dgvFaultInfo.Rows[e.RowIndex].Cells[3].Value.ToString();
                string codeID = this.dgvFaultInfo.Rows[e.RowIndex].Cells[4].Value.ToString();
                string funmae = this.dgvFaultInfo.Rows[e.RowIndex].Cells[5].Value.ToString();

                if (string.IsNullOrWhiteSpace(pid))
                {
                    MessageBox.Show("故障信息编号不能为空！");
                    return;
                }

                mdb = new mdbDatas(path4);
                DataTable table1 = mdb.Find("select * from SytemFaults where [ID] = '" + pid + "'");

                // 修改
                if (table1.Rows.Count > 0)
                {
                    DataRow row = table1.Rows[0];

                    // 修改前的详细数据
                    string logDetail = $"修改前的详细信息：\n" +
                        $"编号：{row["ID"]} | 工位序号：{row["WorkID"]} | " +
                        $"故障点位：{row["CodeID"]} | 故障描述：{row["Faults"]}";

                    string sql = $"update [SytemFaults] set [WorkID] = '{workID}', [CodeID] = '{codeID}', " +
                        $"[Faults] = '{funmae}' where [ID] = '{pid}'";

                    var result = mdb.Change(sql);
                    if (result == true)
                    {
                        MessageBox.Show("修改成功");
                        string updateInfo = $"修改后的详细信息：\n" +
                            $"编号：{pid} | 工位序号：{workID} | " +
                            $"故障点位：{codeID} | 故障描述：{funmae}";
                        loggerConfig.Trace($"【故障信息修改成功】\n{logDetail}\n{updateInfo}");
                    }
                }

                // 新增
                else
                {
                    string sql = $"insert into SytemFaults ([ID],[WorkID],[CodeID],[Faults]) " +
                        $"values ('{pid}', '{workID}', '{codeID}', '{funmae}')";

                    bool result = mdb.Add(sql.ToString());
                    if (result == true)
                    {
                        MessageBox.Show("新增成功");
                        string insertInfo = $"【故障信息新增成功】\n新增详情：\n" +
                            $"编号：{pid} | 工位序号：{workID} | " +
                            $"故障点位：{codeID} | 故障描述：{funmae}";
                        loggerConfig.Trace(insertInfo);
                    }
                }

                mdb.CloseConnection();
                //button18_Click(null, null);
            }

        }

        /// <summary>
        /// 看板设置 > 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveDashboardConfig_Click(object sender, EventArgs e)
        {
            if (txtDashboardIP.Text == String.Empty || txtDashboardPort.Text == String.Empty)
            {
                MessageBox.Show("当前界面内容均为必填项、请先填写完善");
                return;
            }

            mdb = new mdbDatas(path4);
            DataTable table1 = mdb.Find("select * from SytemSocket where ID = 1");
            if (table1.Rows.Count > 0)
            {
                string sql = $"update [SytemSocket] set [IP]='{txtDashboardIP.Text}', [Port]='{txtDashboardPort.Text}', " +
                    $"[Devicestatu]='{chkEnableDashboard.Checked}', [BoardName]='{txtStationNameSets.Text}', " +
                    $"[BoardTheory]='{txtProductName.Text}', [BoardPosition]='{chkReadPName.Checked}', " +
                    $"[FaultCode]='{txtFaultStartPoint.Text}',[FaultLeng]='{txtFaultLength.Text}' where [ID] = 1";

                var result = mdb.Change(sql);
                if (result == true)
                {
                    MessageBox.Show("保存成功");
                }
            }
            mdb.CloseConnection();
        }

        #endregion

        #region ------------ 看板Socket连接服务器 ------------

        private System.Net.Sockets.Socket socket;

        /// <summary>
        /// Socket连接
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public async void Connect()
        {
            Invoke(new Action(() =>
            {
                lblDashboardStatus.ForeColor = R;
            }));

            await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        // Internet 协议、字节流、IPv4连接
                        socket = new Socket(
                        AddressFamily.InterNetwork,
                        SocketType.Stream,
                        ProtocolType.IP);
                        IPAddress ip = IPAddress.Parse(txtDashboardIP.Text);
                        IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtDashboardPort.Text));
                        socket.Connect(point);
                        isDashboardConnected = true;
                        if (isDashboardConnected)
                        {
                            //读取消息
                            System.Threading.Thread thread = new System.Threading.Thread(Received);
                            thread.IsBackground = true;
                            thread.Start();

                            //发送心跳包
                            System.Threading.Thread thread1 = new System.Threading.Thread(Sendheartbeat);
                            thread1.IsBackground = true;
                            thread1.Start();

                            //读取工单号等生产数据发送
                            System.Threading.Thread thread2 = new System.Threading.Thread(SedMegin);
                            thread2.IsBackground = true;
                            thread2.Start();

                            Invoke(new Action(() =>
                            {
                                lblDashboardStatus.ForeColor = G;

                            }));

                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(50000);
                        ShowMsg("Reconnection failed: " + ex.Message);
                    }
                }
            });

        }

        /// <summary>
        /// 连接看板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectDashboard(object sender, EventArgs e)
        {

            if (chkEnableDashboard.Checked == false)
            {
                isDashboardConnected = false;
                lblDashboardStatus.ForeColor = Color.Black;
                return;
            }
            Connect();
        }

        /// <summary>
        /// 接收线程
        /// </summary>
        private void Received()
        {
            while (isDashboardConnected)
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024 * 3];
                    // 实际接收到的有效字节数
                    int len = socket.Receive(buffer);
                    if (len == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, len);
                    ReceiveData(str);
                    this.BeginInvoke(ShowMsgAction, socket.RemoteEndPoint + ":" + str);
                    ShowMsg(socket.RemoteEndPoint + ":" + str);
                }
                catch (Exception ex)
                {
                    isDashboardConnected = false;
                    Console.WriteLine(ex.Message);
                    socket.Close();
                    Connect();
                }
            }
        }

        /// <summary>
        /// 定时发送心跳包
        /// </summary>
        private async void Sendheartbeat()
        {
            await Task.Run(() =>
            {
                // 定时发送心跳包并检测连接状态
                while (isDashboardConnected)
                {
                    try
                    {
                        // 发送心跳包
                        byte[] heartbeatData = System.Text.Encoding.UTF8.GetBytes("heartbeat");
                        socket.Send(heartbeatData);
                        Thread.Sleep(100000);

                    }
                    catch (Exception ex)
                    {
                        isDashboardConnected = false;
                        Console.WriteLine(ex.Message);
                        socket.Close();

                        Connect();
                        //while (true)
                        //{
                        //    try
                        //    {
                        //        if (!IsStart)
                        //        {
                        //            Connect();
                        //            if (IsStart)
                        //            {
                        //                break;
                        //            }

                        //        }
                        //        Thread.Sleep(5000);
                        //    }
                        //    catch (Exception ex2)
                        //    {
                        //        ShowMsg("Connect failed: " + ex2.Message);
                        //    }
                        //}

                    }
                }
            });
        }

        public void SedMegin()
        {
            Invoke(new Action(() =>
            {
                for (int i = 0; i < stationName.Length; i++)
                {
                    Thread.Sleep(50);
                    Application.DoEvents();
                    Send("0+" + stationName[i]);
                    Thread.Sleep(50);
                    Application.DoEvents();
                }
                Thread.Sleep(200);
                Application.DoEvents();
                Send("5+" + txtDeviceName.Text);
            }));
        }

        public void SendReceived()
        {
            Invoke(new Action(() =>
             {
                 string bulletindata = "";
                 //统计

                 //3+机台名称+工单号+工单数量+完成数量+
                 //完成率+合格率+整体节拍+生产产品数量（总数）
                 //+ 工序时间+利用时间+负荷时间+直通率+成品名称
                 string shotji = "3+" + txtDeviceName.Text + "+" + txtWorkOrder.Text + "+" + D1084 + "+" + D1086 + "+"
                     + completeRate + "+" + passRate + "+" + D1090 + "+" + D1080
                         + "+" + processTime + "+" + usingTime + "+" + loadTime
                         + "+" + fpy + "+" + txtProductModel.Text;
                 bulletindata += shotji;
                 //ShowMsg(shotji);
                 //Send(shotji);
                 //读取易损件使用数目vulpalist
                 if (vubPartsTable != null)
                 {
                     foreach (DataRow row in vubPartsTable.Rows)
                     {
                         string vulpa = "  ";
                         string kordnme = " ";
                         string vuboardTheory = row["理论使用次数PLC点位"].ToString();
                         string vuboard = row["已经使用的PLC点位"].ToString();
                         var boardTheory = KeyenceMcNet.ReadInt32(vuboardTheory).Content;
                         OperateResult<int> readss = KeyenceMcNet.ReadInt32(vuboard);
                         if (readss.IsSuccess)
                         {
                             vulpa = readss.Content.ToString();
                             kordnme = CodeNum.WorkIDNm(row["工位ID"].ToString(), stationName);
                             //输出：
                             //4+易损件所在工位+机台名称+ 易损件所在位置+
                             //易损件名称+易损件理论使用次数易损件已使用次数
                             string shotyisjian = "4+" + kordnme + "+" + lblDeviceName.Text + "+" + row["易损件所在的位置"] + "+"
                                + row["易损件的名称"] + "+" + boardTheory + "+" + vulpa;
                             bulletindata += "|" + shotyisjian;

                             //ShowMsg(shotyisjian);
                             //Send(shotyisjian);
                         }
                     }
                 }
                 //2+工位名称+当前工单号+产品条码+
                 //操作人员+则式时间+
                 //则试结果+ 则试节拍+则试项名称+ 则试项上限+则试项下限+测式项实际值
                 for (int j = 0; j < stationName.Length; j++)
                 {

                     string chesAAA = "2+" + stationName[j] + "+" + txtWorkOrder.Text + "+" + barcodeInfo + "+"
                                            + LoginUser.ToString() + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                                "+" + Value[9999] + "+" + D1090 + "+产品名称+" + "  " + "+" + "  " + "+" + txtProductModel.Text;
                     bulletindata += "|" + chesAAA;
                     //ShowMsg(chesAAA);
                     //Send(chesAAA);
                     string[] frorckstr = txtFixtureBinding.Text.Split('+');
                     for (int i = 0; i < frorckstr.Length; i++)
                     {
                         if (!string.IsNullOrWhiteSpace(frorckstr[i]))
                         {
                             string chesBBB = "2+" + stationName[j] + "+" + txtWorkOrder.Text + "+" + barcodeInfo + "+"
                                             + LoginUser.ToString() + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                                 "+" + Value[9999] + "+" + D1090 + "+工装编号" + (i + 1) + "+" + "  " + "+" + "  " + "+" + frorckstr[i];
                             bulletindata += "|" + chesBBB;
                         }
                         // ShowMsg(chesBBB);
                         // Send(chesBBB);
                     }
                     string[] prodrow = CodeNum.CodeMafror(cboBarcodeRuleAndFixtures.Text, codesTable);
                     for (int i = 0; i < prodrow.Length; i++)
                     {
                         if (!string.IsNullOrWhiteSpace(prodrow[i]))
                         {
                             string chesCCC = "2+" + stationName[j] + "+" + txtWorkOrder.Text + "+" + barcodeInfo + "+"
                                                + LoginUser.ToString() + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                                    "+" + Value[9999] + "+" + D1090 + "+产品物料号" + (i + 1) + "+" + " " + "+" + "  " + "+" + prodrow[i];
                             bulletindata += "|" + chesCCC;
                         }
                         // ShowMsg(chesCCC);
                         //Send(chesCCC);
                     }
                 }
                 if (beatList.Count == list.Count &&
                maxList.Count == minList.Count &&
                maxList.Count == list.Count &&
                workstNameList.Count == list.Count &&
                resultList.Count == list.Count &&
                list.Count > 0 && testItems.Length > 0)
                 {

                     for (int i = 0; i < list.Count; i++)
                     {
                         if (!list[i].Equals("null"))
                         {
                             //输出：
                             //2+工位名称+当前工单号+产品条码+
                             //操作人员+则式时间+
                             //则试结果+ 则试节拍+则试项名称+ 则试项上限+则试项下限+测式项实际值
                             if (maxValue[i].Equals("NO") && minValue[i].Equals("NO") && testResult[i].Equals("NO"))
                             {
                                 string cheshixm = "2+" + workstNameList[i] + "+" + txtWorkOrder.Text + "+" + barcodeInfo + "+"
                                         + LoginUser.ToString() + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                             "+" + Value[9999] + "+" + D1090 + "+" + testItems[i] + "+" + "" + "+" + "" + "+" + list[i];
                                 bulletindata += "|" + cheshixm;
                                 // ShowMsg(cheshixm);
                                 // Send(cheshixm);
                             }
                             else
                             {

                                 string cheshixm1 = "2+" + workstNameList[i] + "+" + txtWorkOrder.Text + "+" + barcodeInfo + "+"
                                  + LoginUser.ToString() + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                      "+" + Value[9999] + "+" + D1090 + "+" + testItems[i] + "+" + maxList[i] + "+" + minList[i] + "+" + list[i];
                                 bulletindata += "|" + cheshixm1;

                                 //ShowMsg(cheshixm1);
                                 //Send(cheshixm1);
                             }
                         }
                     }
                 }
                 else
                 {
                     //输出：
                     //2+工位名称+当前工单号+产品条码+
                     //操作人员+则式时间+
                     //则试结果+ 则试节拍+则试项名称+ 则试项上限+则试项下限+测式项实际值
                     //注意：(则试项名称+ 则试项上限+则试项下限+测式项实际值)为空


                     for (int i = 0; i < stationName.Length; i++)
                     {
                         string cheshixm2 = "2+" + stationName[i] + "+" + txtWorkOrder.Text + "+" + barcodeInfo + "+"
                          + LoginUser.ToString() + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                 "+" + Value[9999] + "+" + D1090 + "+" + "测试总结果" + "+" + "   " + "+" + "    " + "+" + Value[9999];
                         bulletindata += "|" + cheshixm2;
                     }
                     //ShowMsg(cheshixm2);
                     // Send(cheshixm2);

                 }
                 ShowMsg(bulletindata);
                 Send(bulletindata);
             }));
        }

        private static readonly object objSync = new object();

        /// <summary>
        /// 发信息
        /// 1+故障所在工位+机台名称+ 故障状态+故障的描述+触发故障的开始时间 
        /// 1+故障所在工位+机台名称+ 故障状态+故障的描述+触发故障的结束时间
        /// 2+工位名称+当前工单号+产品条码+操作人员+则式时间+则试结果+ 则试节拍+则试项名称+ 则试项上限+则试项下限+测式项实际值
        /// 3+工位+工单号+工单数量+完成数量+完成率+合格率+整体节拍+生产产品数量（总数）+ 工序时间+利用时间+负荷时间
        /// 4+易损件所在工位+机台名称+ 易损件所在位置+易损件名称+易损件理论使用次数+易损件已使用次数
        /// </summary>
        /// <param name="msg"></param>
        public async void Send(string msg)
        {
            if (!string.IsNullOrWhiteSpace(msg))
            {
                await Task.Run(() =>
                {
                    string[] strmsg = msg.Split('|');
                    for (int i = 0; i < strmsg.Length; i++)
                    {
                        try
                        {
                            lock (objSync)
                            {
                                //string[] strsed = strmsg[i].Split('+');
                                // string msgjson = strsed.ToJsonString();
                                byte[] buffer = new byte[1024 * 1024 * 3];
                                buffer = Encoding.UTF8.GetBytes("|" + strmsg[i] + "|");
                                socket.Send(buffer);
                            }
                        }
                        catch (Exception ex)
                        {
                            isDashboardConnected = false;
                            // label43.ForeColor = R;
                            // label43.Text = "看板状态：未连接";
                            ShowMsg("发送失败：" + ex.Message);
                        }
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }
                });
            }
        }

        private void ShowMsg(string msg)
        {
            Invoke(new Action(() =>
            {
                if (rtbDashboardLog.TextLength > 50000)
                {
                    rtbDashboardLog.Clear();
                }

                string info = string.Format("{0}:{1}\r\n", DateTime.Now.ToString("G"), msg);
                rtbDashboardLog.AppendText(info);
            }));
        }

        #endregion

        #region ------------ 处理接受过来的数据 ------------

        bool sedbool = false;

        public void ReceiveData(string strdata)
        {
            string[] dateshuzu = new string[] { };
            dateshuzu = strdata.Split(new char[] { '+' });
            switch (dateshuzu[0])
            {
                case "0":
                    try
                    {
                        //D1204
                        KeyenceMcNet.Write(deviceInfo.FormulaModify, 1);
                        //D1206
                        KeyenceMcNet.Write(deviceInfo.FormulaNumModify, int.Parse(dateshuzu[1].ToString()));
                        mdb = new mdbDatas(path4);
                        DataTable table1 = mdb.Find("select * from SytemSet where ID = '1'");
                        if (table1.Rows.Count > 0)
                        {
                            string sql = "update [SytemSet] set [faults]='" + dateshuzu[1] + "'" +
                                //",Workstname ='" + checkBox6.Checked + "'" + 
                                " where [ID] = '1'";
                            var result = mdb.Change(sql);
                        }
                        mdb.CloseConnection();
                        Invoke(new Action(() =>
                        {
                            cboBarcodeRuleAndFixtures.SelectedValue = dateshuzu[1];
                            lblRecipeId.Text = dateshuzu[1];
                            Send("6+" + lblDeviceName.Text + "配方切换成功");
                        }));
                    }
                    catch (Exception ex)
                    {
                        LogMsg(ex.ToString());
                        Send("6+" + lblDeviceName.Text + "配方切换失败");
                    }
                    break;
                case "1":
                    try
                    {
                        if (OffLineType == 0 && isMesLoginSuccessful == true)
                        {
                            //MessageBox.Show("确认接收生产工单");
                            if (sedbool == true)
                            {
                                Invoke(new Action(() =>
                                {
                                    txtWorkOrder.Text = dateshuzu[1];
                                }));
                            }
                            else
                            {
                                char[] prowrd = dateshuzu[1].ToCharArray();
                                foreach (var chstr in prowrd)
                                {
                                    SendKeys.SendWait("{" + chstr + "}");
                                }
                                SendKeys.SendWait("{Enter}");
                            }
                            Invoke(new Action(() =>
                            {
                                if (txtWorkOrder.Text.Equals(dateshuzu[1]))
                                {
                                    Send("6+" + lblDeviceName.Text + "生产工单发送成功");
                                }
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        Send("6+" + lblDeviceName.Text + "生产工单发送失败");
                    }
                    break;
            }
        }

        #endregion

        #region ------------ 打印信息 ------------

        DatasModel.Printers printers = new DatasModel.Printers();
        DatasModel.PrintersBtw printersBtw = new PrintersBtw();

        /// <summary>
        /// 选择打印机
        /// </summary>
        private void GetPrinter()
        {
            foreach (string pkInstalledPrinters in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmbInstalledPrinters.Items.Add(pkInstalledPrinters);
                cboPrinterType.Items.Add(pkInstalledPrinters);
            }
            if (cmbInstalledPrinters.Items.Contains("ZDesigner GK888t (EPL)"))
            {
                cmbInstalledPrinters.Text = "ZDesigner GK888t (EPL)";
            }
            if (cboPrinterType.Items.Contains("ZDesigner GK888t (EPL)"))
            {
                cboPrinterType.Text = "ZDesigner GK888t (EPL)";
            }
        }

        /// <summary>
        /// 保存打印配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePrinterConfig_Click(object sender, EventArgs e)
        {
            printerConfig.PrinterStep = txtStartPrintPoint.Text;
            printerConfig.PrinteSendResult = txtEndPrintPoint.Text;
            printerConfig.ControlledByPLC = chkPlcControlPrint.Checked;
            printerConfig.PrintMode = cboPrintMode.Text;
            var result = printerConfig.Save();
            MessageBox.Show(result);
        }

        /// <summary>
        /// 加载打印信息
        /// </summary>
        private void LoadPrinterConfig()
        {
            try
            {
                #region ------ 加载打印机基本配置 ------

                printerConfig = DatasServer.PrinterSettingServer.GetPrinterSetting(1);
                txtStartPrintPoint.Text = printerConfig.PrinterStep;
                txtEndPrintPoint.Text = printerConfig.PrinteSendResult;
                chkPlcControlPrint.Checked = printerConfig.ControlledByPLC;
                cboPrintMode.Text = printerConfig.PrintMode;

                #endregion

                #region ------ 加载网络打印相关配置 printers ------

                printers = DatasServer.PrintersServer.GetPrinters(1);

                // 打印机网络配置
                txtPrinter_IP.Text = printers.ip;       // IP
                txtPrinter_Port.Text = printers.prot;   // 端口

                // 动态码
                textBox25.Text = printers.Ptime;        // 码号
                textBox26.Text = printers.Ptail;        // 条码数字（流水号）

                // 文本
                textBox20.Text = printers.TFront;       // 条码前端
                textBox27.Text = printers.Tmonarch;     // 条码后端
                textBox28.Text = printers.Tlow;         // 条码型号

                // 配置prn文件
                lblPrnFilePath_TCP.Text = printers.Method;
                LoadPrnFiles(lblPrnFilePath_TCP.Text);

                for (int n = 0; n < comboBox1.Items.Count; n++)
                {
                    if (comboBox1.GetItemText(comboBox1.Items[n]).Equals(printers.Ptype))
                    {
                        // 将每个选项转换为文本并存入数组
                        comboBox1.SelectedItem = printers.Ptype;
                        break;
                    }
                }

                string txttow = printers.Txttow;        // 是否打印文字
                string plcmodel = printers.Plcmodel;    // 是否读取PLC型号
                string phead = printers.Phead;          // 是否由PLC控制

                if (txttow == "True") checkBox3.Checked = true;
                if (plcmodel == "True") checkBox4.Checked = true;
                //if (phead == "True") checkBox7.Checked = true;    
                label62.Text = Pfunime() + textBox26.Text;  // 条码内容

                #endregion

                #region------ 加载驱动打印相关配置 PrintersBtw ------

                printersBtw = DatasServer.PrintersBtwServer.GetPrintersBtw(1);

                // 打印配置
                cboPrinterType.Text = printersBtw.PtypeBtw;         // 打印机类型

                // 文本
                textBox54.Text = printersBtw.PheadBtw;              // 条码前端
                textBox53.Text = printersBtw.PtimeBtw;              // 条码后端
                textBox52.Text = printersBtw.PtailBtw;              // 条码型号
                checkBox15.Checked = printersBtw.PlcmodelBtw;       // 读取PLC型号
                checkBox16.Checked = printersBtw.MethodBtw;         // 使用文字

                // 动态码
                textBox57.Text = printersBtw.TFrontBtw;             // 码号
                textBox56.Text = printersBtw.TmonarchBtw;           // 流水号
                txtPrintNum.Text = printersBtw.TlowBtw;             // 打印数量
                checkBox7.Checked = printersBtw.PrintNowDateTime;   // 自动添加日期
                checkBox18.Checked = printersBtw.PrintCodeTwoBool;  // +2打印

                // 配置prn文件
                lblPrnFilePath_COM.Text = printersBtw.PertowBtw;    // prn文件路径
                comboBox5.Text = printersBtw.TxttowBtw;             // 打印文件格式

                PfunCodeBtw();

                #endregion
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        /// <summary>
        /// 保存驱动打印设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_COM_Click(object sender, EventArgs e)
        {
            printersBtw.PtypeBtw = cboPrinterType.Text;
            printersBtw.PheadBtw = textBox54.Text;                  // 条码前端
            printersBtw.PtimeBtw = textBox53.Text;                  // 条码后端
            printersBtw.PtailBtw = textBox52.Text;                  // 条码型号
            printersBtw.PlcmodelBtw = checkBox15.Checked;           // 读取PLC型号
            printersBtw.MethodBtw = checkBox16.Checked;             // 使用文字

            printersBtw.TFrontBtw = textBox57.Text;                 // 码号
            printersBtw.TmonarchBtw = textBox56.Text;               // 流水号
            printersBtw.TlowBtw = txtPrintNum.Text;                 // 打印数量
            printersBtw.PertowBtw = lblPrnFilePath_COM.Text;        // prn文件路径
            printersBtw.TxttowBtw = comboBox5.Text;                 // 打印文件格式
            printersBtw.PrintNowDateTime = checkBox7.Checked;       // 自动添加日期
            printersBtw.PrintSerialNumber = false;                  // 打印机打印条码
            printersBtw.PrintCodeTwoBool = checkBox18.Checked;      // +2打印
            var result = printersBtw.Save();
            MessageBox.Show(result);
            PfunCodeBtw();
        }

        DatasModel.PrinterSetting printerConfig = new DatasModel.PrinterSetting();

        /// <summary>
        /// 修改打印信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click_1(object sender, EventArgs e)
        {
            if (textBox25.Text == String.Empty)
            {
                MessageBox.Show("当前界面内容均为必填项、请先填写完善");
                return;
            }
            try
            {
                printers.Ptype = comboBox1.Text;
                printers.Ptime = textBox25.Text;// table1.Rows[i]["Ptime"].ToString();//条码时间格式
                printers.Ptail = textBox26.Text;//条码数字
                printers.TFront = textBox20.Text;//字体前端
                printers.Tmonarch = textBox27.Text;//字体后端
                printers.Tlow = textBox28.Text;//字体下面
                printers.Txttow = checkBox3.Checked.ToString();
                printers.Plcmodel = checkBox4.Checked.ToString();
                printers.Method = lblPrnFilePath_TCP.Text;
                printers.Phead = "False";
                printers.ip = txtPrinter_IP.Text;
                printers.prot = txtPrinter_Port.Text;
                var result = printers.Save();
                MessageBox.Show(result);
                /* PatPZLhead();
                 //FileInfo myFile1 = new FileInfo(pathtxtPZL1);
                 File.WriteAllText(pathtxtPZL1, richTextBox5.Text, Encoding.UTF8);//第二种
                 PatPZLtail();
                 File.WriteAllText(pathtxtPZL2, richTextBox6.Text, Encoding.UTF8);//第二种*/
                string method = "  ";
                //mdb = new mdbDatas(path4);
                //DataTable table1 = mdb.Find("select * from Printers where ID = 1");
                //if (table1.Rows.Count > 0)
                //{//TFront Tmonarch Tlow Pertow Method Phead

                //    string sql = "update [Printers] set [Ptype]='" + comboBox1.Text + "'" +
                //                  ",[Ptime]='" + textBox25.Text + "',[Ptail]='" + textBox26.Text + "'" +
                //                  ",[TFront]='" + textBox20.Text + "',[Tmonarch]='" + textBox27.Text + "'" +
                //                  ",[Tlow]='" + textBox28.Text + "'" +
                //                  ",[Txttow]='" + checkBox3.Checked + "',[Plcmodel]='" + checkBox4.Checked + "'" +
                //                  ",[Method]='" + label48.Text + "',[Phead]='" + "False" + "'" +
                //                  ",[ip]='" + textBox10.Text + "',[prot]='" + textBox11.Text + "'" +
                //                  " where [ID] = 1";
                //    var result = mdb.Change(sql);
                //    if (result == true)
                //    {
                //        MessageBox.Show("保存成功");
                //    }
                //}
                label62.Text = Pfunime() + textBox26.Text;
                //  mdb.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        /// <summary>
        /// 发送打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbInstalledPrinters.Text))
            {
                // Send a printer-specific to the printer.
                RawPrinterHelper.SendStringToPrinter(cmbInstalledPrinters.Text, this.richTextBox2.Text);
            }
        }

        #region ----------- 其它打印 -----------

        bool prinBoolOK = false;

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cboPrinterType.Text))
                {
                    // 获取打印个数
                    int num = 1;
                    int.TryParse(txtPrintNum.Text, out num);

                    Prin_Serial_Number(num);//添加一

                    PfunCodeBtw();

                    GoPrintCodes();

                    lblPrintPrompt.Text = "OK";
                    lblPrintPrompt.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                PrinPromptNG();
            }
            if (prinBoolOK == false)
            {
                PrinPromptNG();
            }
        }

        private void PrinPromptNG()
        {
            lblPrintPrompt.Text = "NG";
            lblPrintPrompt.ForeColor = Color.Red;
        }

        private string GoPrintCodes()
        {
            string printCode = label133.Text;//条码内容
            string printAgo = textBox54.Text;//条码前端
            string printAfter = textBox53.Text;//条码后端
            string printType = textBox52.Text;//产品型号
            string printSerial = textBox56.Text;//流水号

            int printNum = 1;
            int.TryParse(txtPrintNum.Text, out printNum);

            switch (comboBox5.SelectedIndex)
            {
                case 0: // BTW 文件打印
                    BTW_PrintersCodes(printCode, printAgo, printAfter, printType, printNum);
                    break;
                case 1: // PRN 文件打印
                    ZPL_PrinterCodes(printNum, printCode, printAgo, printAfter, printType);
                    break;
            }
            return printCode;

        }

        private async void BTW_PrintersCodes(string printCode, string printAgo, string printAfter, string printType, int printNum)
        {
            try
            {
                Init_Bartender();
                if (btApp == null)
                {
                    prinBoolOK = false;
                    return;
                }
                if (PrinFileChecker.IsBtwFile(lblPrnFilePath_COM.Text) == false)
                {
                    prinBoolOK = false;
                    return;
                }
                btFormat = btApp.Formats.Open(lblPrnFilePath_COM.Text);
                btFormat.PrintSetup.NumberSerializedLabels = printNum; //设置打印份数
                switch (checkBox16.Checked)
                {
                    case false:
                        //设置打印字段值
                        btFormat.SetNamedSubStringValue("0", printCode);
                        break;
                    case true:
                        btFormat.SetNamedSubStringValue("1", printAgo);
                        btFormat.SetNamedSubStringValue("2", printAfter);
                        btFormat.SetNamedSubStringValue("3", printType);
                        goto case false;
                }
                if (checkBox18.Checked)
                {
                    Prin_Serial_Number(1);//添加一
                    string printCode2 = PfunCodeBtw();
                    btFormat.SetNamedSubStringValue("4", printCode2);
                }
                else
                {
                    btFormat.SetNamedSubStringValue("4", printCode);
                }
                await Task.Run(() =>
                {
                    //打印标签
                    btFormat.PrintOut(false, false);
                    //不保存标签退出
                    btFormat.Close(BarTender.BtSaveOptions.btDoNotSaveChanges);
                    prinBoolOK = true;
                });
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        private static BarTender.Application btApp = null;
        private static BarTender.Format btFormat;

        public static void Init_Bartender()
        {
            try
            {
                if (btApp == null)
                {
                    btApp = new BarTender.Application();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return;
            }
        }

        /// <summary>
        /// prn打印
        /// </summary>
        /// <param name="printCode"></param>
        /// <param name="printAgo"></param>
        /// <param name="printAfter"></param>
        /// <param name="printType"></param>
        private void ZPL_PrinterCodes(int printNum, string printCode, string printAgo, string printAfter, string printType)
        {
            try
            {
                for (int i = 0; i < printNum; i++)
                {
                    string instrctis;
                    string fileContent = File.ReadAllText(lblPrnFilePath_COM.Text);

                    // 使用文字 && +2
                    if (checkBox16.Checked && checkBox18.Checked == false)
                    {
                        instrctis = string.Format(fileContent, printCode, printAgo, printAfter, printType);
                    }
                    else if (checkBox18.Checked)
                    {
                        Prin_Serial_Number(1);//添加一
                        string printCode2 = PfunCodeBtw();
                        instrctis = string.Format(fileContent, printCode, printAgo, printAfter, printType, printCode2);
                    }
                    else
                    {
                        instrctis = string.Format(fileContent, printCode);
                    }

                    // Send a printer-specific to the printer.
                    RawPrinterHelper.SendStringToPrinter(cboPrinterType.Text, instrctis);
                    if (printNum > 1)
                    {
                        Prin_Serial_Number(1);//添加一
                        printCode = PfunCodeBtw();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 条码加一
        /// </summary>
        /// <returns></returns>
        public string PfunCodeBtw()
        {
            string prinCodetext = textBox57.Text;
            if (checkBox7.Checked)
            {
                // prinCodetext;

                string Ptime = prinCodetext;
                DateTime times_Month = DateTime.Now;
                string times_Month_string0 = times_Month.Year.ToString();
                string times_Month_string1 = times_Month.Month.ToString();
                switch (times_Month_string1)
                {
                    case "1": Ptime += 1; break;
                    case "2": Ptime += 2; break;
                    case "3": Ptime += 3; break;
                    case "4": Ptime += 4; break;
                    case "5": Ptime += 5; break;
                    case "6": Ptime += 6; break;
                    case "7": Ptime += 7; break;
                    case "8": Ptime += 8; break;
                    case "9": Ptime += 9; break;
                    case "10": Ptime += 0; break;
                    case "11": Ptime += "A"; break;
                    case "12": Ptime += "B"; break;
                }
                string times_Month_string2 = times_Month.ToString("dd");
                Ptime += times_Month_string2;
                prinCodetext = Ptime;
            }
            string prinCode = prinCodetext + textBox56.Text;
            label133.Text = prinCode;
            return prinCode;
        }

        private async void Prin_Serial_Number(int num)
        {
            int prinCount = 0;
            int.TryParse(textBox56.Text, out prinCount);
            prinCount = prinCount + num;
            printersBtw.TmonarchBtw = prinCount.ToString("D4");
            textBox56.Text = printersBtw.TmonarchBtw;
            await Task.Run(() =>
           {
               printersBtw.Save();
           });
        }

        #endregion

        #region ----------- 网络打印 -----------

        /// <summary>
        /// 打印机
        /// </summary>
        private void PrintZPL_Click()
        {
            Task.Run(() =>
            {
                while (IsRunningPZL)
                {
                    // 触发通讯读
                    if (isPlcConnected == true)
                    {
                        // D1014
                        var Read_data1 = KeyenceMcNet.ReadInt16(printerConfig.PrinteSendResult).Content;
                        // D1012
                        var read_data2 = KeyenceMcNet.ReadInt32(printerConfig.PrinterStep).Content; // 1=新打  2=重打

                        if (read_data2 == 1 && Read_data1 == 0) // 新打印一个标签
                        {
                            Invoke(new Action(() =>
                            {
                                try
                                {
                                    switch (cboPrintMode.SelectedIndex)
                                    {
                                        case 0:
                                            button30_Click(null, null); // 发送打印机指令
                                            break;
                                        case 1:
                                            btnPrint_Click(null, null);
                                            break;
                                    }

                                    //D1014
                                    KeyenceMcNet.Write(printerConfig.PrinteSendResult, 1);
                                }
                                catch (Exception ex)
                                {
                                    //MessageBox.Show("发送打印机指令异常：" + ex.Message);
                                    //D1014
                                    KeyenceMcNet.Write(printerConfig.PrinteSendResult, 2);
                                }
                            }));
                            Thread.Sleep(250);
                            Application.DoEvents();
                        }
                        else if (read_data2 == 2)  //重打标签
                        {
                            Invoke(new Action(() =>
                            {
                                try
                                {
                                    // Send a printer-specific to the printer.

                                    switch (cboPrintMode.SelectedIndex)
                                    {
                                        case 0:
                                            repeatBarCode();//发送打印机指令
                                            break;
                                        case 1:
                                            GoPrintCodes();
                                            break;

                                    }
                                    //D1014
                                    KeyenceMcNet.Write(printerConfig.PrinteSendResult, 1);
                                }
                                catch (Exception ex)
                                {
                                    //MessageBox.Show("发送打印机指令异常：" + ex.Message);
                                    //D1014
                                    KeyenceMcNet.Write(printerConfig.PrinteSendResult, 2);
                                }
                            }));
                            Thread.Sleep(250);
                            Application.DoEvents();
                        }
                    }

                }
            });
        }

        /// <summary>
        /// 条码前部分+日期处理
        /// </summary>
        /// <returns></returns>
        private string Pfunime()
        {
            string Ptime = textBox25.Text;
            DateTime times_Month = DateTime.Now;
            string times_Month_string0 = times_Month.Year.ToString();
            string times_Month_string1 = times_Month.Month.ToString();
            switch (times_Month_string1)
            {
                case "1": Ptime += 1; break;
                case "2": Ptime += 2; break;
                case "3": Ptime += 3; break;
                case "4": Ptime += 4; break;
                case "5": Ptime += 5; break;
                case "6": Ptime += 6; break;
                case "7": Ptime += 7; break;
                case "8": Ptime += 8; break;
                case "9": Ptime += 9; break;
                case "10": Ptime += 0; break;
                case "11": Ptime += "A"; break;
                case "12": Ptime += "B"; break;
            }
            string times_Month_string2 = times_Month.ToString("dd");
            Ptime += times_Month_string2;
            return Ptime;
        }

        /// <summary>
        /// 处理流水号数据
        /// </summary>
        private void Serialnumber()
        {
            string serialunm = textBox26.Text;
            int num;//发送一条加一
            if (int.TryParse(serialunm, out num))
            {
                SeriaPtail(num);
            }

        }

        /// <summary>
        /// 修改流水号数据
        /// </summary>
        /// <param name="num"></param>
        private void SeriaPtail(int num)
        {
            mdb = new mdbDatas(path4);
            DataTable table1 = mdb.Find("select Ptail from Printers where ID = 1");
            if (table1.Rows.Count > 0)
            {
                int numA = num + 1;
                string seriaA = numA.ToString().PadLeft(5, '0');
                string sql = "update [Printers] set [Ptail]='" + seriaA + "'" +
                              " where [ID] = 1";
                var result = mdb.Change(sql);
            }
            DataTable table2 = mdb.Find("select Ptail from Printers where ID = 1");
            for (int i = 0; i < table2.Rows.Count; i++)
            {
                for (int j = 0; j < table2.Columns.Count; j++)
                {
                    textBox26.Text = table2.Rows[i]["Ptail"].ToString();
                }
            }
            mdb.CloseConnection();
            label62.Text = Pfunime() + textBox26.Text;
        }

        private Socket clientSocket = null;

        private void button31_Click(object sender, EventArgs e)
        {
            ConnPrintServer();
        }

        public Boolean ConnPrintServer()
        {
            IPAddress ipAddress;
            if (!IPAddress.TryParse(txtPrinter_IP.Text.ToString(), out ipAddress))
            {
                MessageBox.Show("IP地址无效");
                return false;
            }
            string ip = txtPrinter_IP.Text.ToString();
            int port = 9100;
            bool isNumber = int.TryParse(txtPrinter_Port.Text.ToString(), out port);

            // 设置连接
            EndPoint point = new IPEndPoint(IPAddress.Parse(ip), port);		// Server IP 和端

            Console.Write("连接中...");
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 建立TCP / IP连接
            try
            {
                clientSocket.Connect(point);       //尝试连接
                Console.WriteLine("连接成功！");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"连接异常：\n{ex}");
                clientSocket.Close();
                clientSocket.Dispose();
                return false;
            }
        }

        /// <summary>
        /// 发送新的条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button30_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("请选择打印文件！！!");
                return;
            }
            try
            {
                Serialnumber();//将流水号+1
                string instrctis = "";
                string fileContent = File.ReadAllText(comboBox1.Text);
                if (checkBox3.Checked)
                {
                    instrctis = string.Format(fileContent, label62.Text, textBox20.Text, textBox27.Text, textBox28.Text);
                }
                else
                {
                    instrctis = string.Format(fileContent, label62.Text);
                }
                /*StringBuilder str = new StringBuilder();
                str.Append("RUN \"YC.BAS\"");
                str.Append("\r\n");
                str.Append(label62.Text);
                str.Append("\r\n");
                str.Append(textBox27.Text);
                str.Append("\r\n");
                str.Append(textBox20.Text);
               str.Append("\r\n");
              str.Append(textBox28.Text);
               str.Append("\r\n");*/
                byte[] heartbeatData = System.Text.Encoding.UTF8.GetBytes(instrctis.ToString());
                Send(heartbeatData);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 重复发送条码
        /// </summary>
        private void repeatBarCode()
        {
            try
            {
                /*StringBuilder str = new StringBuilder();
                str.Append("RUN \"YC.BAS\"");
                str.Append("\r\n");
                str.Append(label62.Text);
                str.Append("\r\n");
                str.Append(textBox27.Text);
                str.Append("\r\n");
                str.Append(textBox20.Text);
                str.Append("\r\n");
                str.Append(textBox28.Text);
                str.Append("\r\n");*/
                string instrctis = "";
                string fileContent = File.ReadAllText(comboBox1.Text);
                if (checkBox3.Checked)
                {
                    instrctis = string.Format(fileContent, label62.Text, textBox20.Text, textBox27.Text, textBox28.Text);
                }
                else
                {
                    instrctis = string.Format(fileContent, label62.Text);
                }
                byte[] heartbeatData = System.Text.Encoding.UTF8.GetBytes(instrctis.ToString());
                Send(heartbeatData);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 发送执行函数(使用异步方式发送消息，不用阻塞等待)
        /// </summary>
        /// <param name="msg">待发送信息的byte数组</param>
        /// <returns></returns>
        public async void Send(byte[] msg)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (clientSocket is null || !clientSocket.Connected) while (!ConnPrintServer()) ;   // 如果未连接，就连接上
                    clientSocket.Send(msg);    //把字节数组发送到服务器端
                    Console.WriteLine("发送成功！");
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
            });
            //  return 1;
        }

        #endregion

        private void LoadPrnFiles(string folderPath)
        {
            try
            {
                comboBox1.Items.Clear();
                List<string> listprn = PRNchDPrnFiles.LoadAllPrnFilesFromDisk(folderPath);
                //string[] prnFiles = Directory.GetFiles(folderPath, "*.prn", SearchOption.AllDirectories);
                // string[] prnFiles = Directory.GetFiles(folderPath, "*.prn");
                foreach (string file in listprn)
                {
                    // 在这里处理PRN文件
                    // 例如：打开文件、读取内容、显示在界面等
                    comboBox1.Items.Add(file);
                    //Console.WriteLine(file);
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 网络打印 >　更改prn文件所在位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePath_TCP_Click(object sender, EventArgs e)
        {
            // PrintDialog path = new PrintDialog();
            // OpenFileDialog path = new OpenFileDialog() { Filter = "Files (*.prn)|*.prn" };
            // path.ShowDialog();

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderBrowserDialog.SelectedPath;
                    this.lblPrnFilePath_TCP.Text = folderPath;
                    LoadPrnFiles(folderPath);
                }
            }
        }

        /// <summary>
        /// 网络打印 > 查看文件所在位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPath_TCP_Click(object sender, EventArgs e)
        {
            string filePath = comboBox1.Text;
            Direc_Troy_Path_Position(filePath);
        }

        /// <summary>
        /// 驱动打印 > 更改prn文件所在位置
        /// </summary>
        private void ChangePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (string.IsNullOrEmpty(comboBox5.Text))
            {
                MessageBox.Show("请选择打印文件格式");
                return;
            }
            openFileDialog.Filter = "PRN files " + comboBox5.Text;
            openFileDialog.Title = "选择打印文件";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取选中的PRN文件路径
                string prnFilePath = openFileDialog.FileName;
                lblPrnFilePath_COM.Text = prnFilePath;
                // 在这里添加代码以处理PRN文件
            }
        }

        /// <summary>
        /// 驱动打印 > 查看文件所在位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPath_COM_Click(object sender, EventArgs e)
        {
            string filePath = lblPrnFilePath_COM.Text;
            Direc_Troy_Path_Position(filePath);
        }

        private static void Direc_Troy_Path_Position(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                string directoryPath = Path.GetDirectoryName(filePath);
                if (System.IO.Directory.Exists(directoryPath))
                {
                    try
                    {
                        // 使用Windows Shell打开文件夹  
                        /* ProcessStartInfo startInfo = new ProcessStartInfo
                         {
                             Arguments = directoryPath,
                             FileName = "explorer.exe"
                         };
                         Process.Start("explorer.exe", directoryPath);*/
                        System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();

                        processStartInfo.FileName = "explorer.exe";  //资源管理器
                        processStartInfo.Arguments = directoryPath;

                        System.Diagnostics.Process.Start(processStartInfo);
                        Console.WriteLine("文件夹已在'此电脑'中打开。");
                    }
                    catch (Exception ex)
                    {
                        // 处理异常  
                        Console.WriteLine("打开文件夹时发生错误：");
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("文件夹不存在。");
                }
            }
        }

        #endregion

        #region ------------ PLC点位 ------------

        DataTable codesTable;   // 存储产品编号与条码验证型号
        DataTable boardTable;   // 存储PLC点位信息
        BindingList<Codes> bindingCodes = new BindingList<Codes>();                         // 存储配方设置相关的参数
        BindingList<BarcodeVefictn> bindingVefictn = new BindingList<BarcodeVefictn>();     // 条码相关数据

        /// <summary>
        /// 从数据库加载 Board 表格保存到对应的数组中，并为 DataGridView4,5 设置属性
        /// </summary>
        private void SYS_BOARD()
        {
            button23_Click(null, null);

            mdb = new mdbDatas(path4);
            boardTable = mdb.Find("select * from Board");

            // 检查表格中是否存在数据
            if (boardTable.Rows.Count > 0)
            {
                // 从数据库表 Board 中的每一行提取特定列的数据，并存储在相应的数组中
                sequenceNum = boardTable.AsEnumerable().Select(row => row["WorkID"].ToString()).ToArray();
                testItems = boardTable.AsEnumerable().Select(row => row["BoardName"].ToString()).ToArray();
                actualValue = boardTable.AsEnumerable().Select(row => row["BoardCode"].ToString()).ToArray();
                maxValue = boardTable.AsEnumerable().Select(row => row["MaxBoardCode"].ToString()).ToArray();
                minValue = boardTable.AsEnumerable().Select(row => row["MinBoardCode"].ToString()).ToArray();
                beat = boardTable.AsEnumerable().Select(row => row["BeatBoardCode"].ToString()).ToArray();
                testResult = boardTable.AsEnumerable().Select(row => row["ResultBoardCode"].ToString()).ToArray();
                unitName = boardTable.AsEnumerable().Select(row => row["BoardA1"].ToString()).ToArray();
                standardValue = boardTable.AsEnumerable().Select(row => row["StandardCode"].ToString()).ToArray();
            }
            mdb.CloseConnection();

            // 为 DataGridView4 添加 ButtonColumn 列，标题为操作，按钮Text默认显示为 “NO保存”
            DataGridViewButtonColumn btnNOSave = new DataGridViewButtonColumn();
            btnNOSave.HeaderText = "操作";
            btnNOSave.Text = "NO保存";
            btnNOSave.Name = "btnColNO";
            btnNOSave.DefaultCellStyle.NullValue = "NO保存";
            dataGridView4.Columns.Add(btnNOSave);

            // 为 DataGridView4 添加 ButtonColumn 列，标题为操作，按钮Text默认显示为 “ONE保存”
            DataGridViewButtonColumn btnOneSave = new DataGridViewButtonColumn();
            btnOneSave.HeaderText = "操作";
            btnOneSave.Text = "ONE保存";
            btnOneSave.Name = "btnColONE";
            btnOneSave.DefaultCellStyle.NullValue = "ONE保存";
            dataGridView4.Columns.Add(btnOneSave);

            // 为 DataGridView4 添加 ButtonColumn 列，标题为操作，按钮Text默认显示为 “删除”
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.HeaderText = "操作"; // 第二个按钮的标题文本
            btnDelete.Name = "btnCol2"; // 第二个按钮的名称
            btnDelete.DefaultCellStyle.NullValue = "删除";
            dataGridView4.Columns.Add(btnDelete);

            // DataGridView5 属性设置，保存
            DataGridViewButtonColumn btnSave = new DataGridViewButtonColumn();
            btnSave.HeaderText = resources.GetString("operation");
            btnSave.Text = resources.GetString("save");
            btnSave.Name = "btnCol";
            btnSave.DefaultCellStyle.NullValue = resources.GetString("save");
            dataGridView5.Columns.Add(btnSave);

            // DataGridView5 属性设置，删除
            DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn();
            btnDel.HeaderText = resources.GetString("operation"); // 第二个按钮的标题文本
            btnDel.Name = "btnCol2"; // 第二个按钮的名称
            btnDel.DefaultCellStyle.NullValue = resources.GetString("del");
            dataGridView5.Columns.Add(btnDel);
        }

        /// <summary>
        /// 刷新界面数据
        /// </summary>
        private void button23_Click(object sender, EventArgs e)
        {
            // 初始化连接数据库
            mdb = new mdbDatas(path4);

            // 查询Board表格， ID WorkID BoardName BoardCode MaxBoardCode MinBoardCode BeatBoardCode ResultBoardCode 
            string selectSql = $"select [ID] as 编号, [WorkID] as 工位ID, [BoardName] as 测试项目的名称," +
                $"[BoardCode] as 测试项目的PLC点位, [StandardCode] as 测试项目标准值的PLC点位," +
                $"[MaxBoardCode] as 测试项目的上限PLC点位, [MinBoardCode] as 测试项目的下限PLC点位," +
                $"[ResultBoardCode] as 测试项目的结果PLC点位, [BeatBoardCode] as 测试项目的节拍PLC点位," +
                $"[BoardA1] as 单位 from Board";
            DataTable boardTable = mdb.Find(selectSql);

            // 设置 “编号” 列的属性为自动增长、只读、起始值为0
            boardTable.Columns["编号"].AutoIncrement = true;
            boardTable.Columns["编号"].ReadOnly = true;
            boardTable.Columns["编号"].AutoIncrementSeed = 0;

            // 获取 "编号" 列中的最大值
            int maxPid = 0;
            if (boardTable.Rows.Count > 0)
            {
                maxPid = boardTable.AsEnumerable().Max(row => row.Field<int>("编号"));
            }

            // 创建一个新行，设置其 "编号" 为最大值 maxPid
            DataRow newRow = boardTable.NewRow();
            newRow["编号"] = maxPid;

            // 刷新 dataGridView4
            dataGridView4.DataSource = boardTable;

            // 查询 Codes 表的数据
            codesTable = mdb.Find("select * from Codes");

            // 获取绑定列表对象，刷新 dataGridView5
            bindingCodes = CodesServer.GetCodesBindingList();
            dataGridView5.DataSource = bindingCodes;

            // 设置 dataGridView5 的列头文本
            CodesDataGridView codesDataGridView = new CodesDataGridView();
            codesDataGridView.GetCodesDataGridViewHeaderText(dataGridView5);

            // 将 Codes 表的数据绑定到 comboBox2 控件，并设置显示成员和值成员
            cboBarcodeRuleAndFixtures.DataSource = codesTable;
            cboBarcodeRuleAndFixtures.DisplayMember = "TooName";
            cboBarcodeRuleAndFixtures.ValueMember = "ID";

            mdb.CloseConnection();

            bindingVefictn = BarcodeVefictnServer.GetBarcodeVefictnBindingList();
            dataGridView6.DataSource = bindingVefictn;
        }

        /// <summary>
        /// PLC点位信息表格中，删除与保存按钮的事件处理器
        /// </summary>
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            { return; }

            // 需要记录日志的字段
            string[] logFields = { "WorkID", "BoardName", "BoardCode", "MaxBoardCode", "MinBoardCode", "BeatBoardCode", "ResultBoardCode", "BoardA1", "StandardCode" };

            // 定义字段与别名的映射
            Dictionary<string, string> fieldAliases = new Dictionary<string, string>
            {
                { "WorkID", "工位序号" },
                { "BoardName", "测试项" },
                { "BoardCode", "实际值点位" },
                { "MaxBoardCode", "上限值点位" },
                { "MinBoardCode", "下限值点位" },
                { "BeatBoardCode", "节拍点位" },
                { "ResultBoardCode", "测试结果" },
                { "BoardA1", "单位" },
                { "StandardCode", "标准值点位" },
            };

            // 从DataGridView 删除选定行
            DeleteRowFromDataGridView<int>(dataGridView4, e, "Board", "ID", logFields, 3, path4, btnName: "btnCol2", fieldAliases, "PLC点位数据");

            // NO保存
            if (dataGridView4.Columns[e.ColumnIndex].Name == "btnColNO")
            {
                //说明点击的列是DataGridViewButtonColumn列
                DataGridViewColumn column = dataGridView4.Columns[e.ColumnIndex];
                string pid = this.dataGridView4.Rows[e.RowIndex].Cells[3].Value.ToString();
                string workID = CodeNum.WorkIDONECodes(this.dataGridView4.Rows[e.RowIndex].Cells[4].Value.ToString());
                string boardName = this.dataGridView4.Rows[e.RowIndex].Cells[5].Value.ToString();
                string boardCode = this.dataGridView4.Rows[e.RowIndex].Cells[6].Value.ToString();
                string stanCode = CodeNum.NOCodes(this.dataGridView4.Rows[e.RowIndex].Cells[7].Value.ToString());
                string maxBoardCode = CodeNum.NOCodes(this.dataGridView4.Rows[e.RowIndex].Cells[8].Value.ToString());
                string minBoardCode = CodeNum.NOCodes(this.dataGridView4.Rows[e.RowIndex].Cells[9].Value.ToString());
                string resultBoardCode = CodeNum.NOCodes(this.dataGridView4.Rows[e.RowIndex].Cells[10].Value.ToString());
                string beatBoardCode = CodeNum.NOCodes(this.dataGridView4.Rows[e.RowIndex].Cells[11].Value.ToString());
                string beatBoardA1 = CodeNum.GetNullUnit(this.dataGridView4.Rows[e.RowIndex].Cells[12].Value.ToString());

                ModifyPlcAddress(pid, workID, stanCode, boardName, boardCode, maxBoardCode, minBoardCode, resultBoardCode, beatBoardCode, beatBoardA1);
                button23_Click(null, null);
            }

            // ONE保存
            if (dataGridView4.Columns[e.ColumnIndex].Name == "btnColONE")
            {
                //Board  
                //ID WorkID BoardName BoardCode MaxBoardCode MinBoardCode BeatBoardCode ResultBoardCode 
                //说明点击的列是DataGridViewButtonColumn列
                DataGridViewColumn column = dataGridView4.Columns[e.ColumnIndex];
                string pid = this.dataGridView4.Rows[e.RowIndex].Cells[3].Value.ToString();
                string workID = CodeNum.WorkIDONECodes(this.dataGridView4.Rows[e.RowIndex].Cells[4].Value.ToString());
                string boardName = this.dataGridView4.Rows[e.RowIndex].Cells[5].Value.ToString();
                string boardCode = this.dataGridView4.Rows[e.RowIndex].Cells[6].Value.ToString();
                string stanCode = CodeNum.ONECodes(this.dataGridView4.Rows[e.RowIndex].Cells[7].Value.ToString());
                string maxBoardCode = CodeNum.ONECodes(this.dataGridView4.Rows[e.RowIndex].Cells[8].Value.ToString());
                string minBoardCode = CodeNum.ONECodes(this.dataGridView4.Rows[e.RowIndex].Cells[9].Value.ToString());
                string resultBoardCode = CodeNum.ONECodes(this.dataGridView4.Rows[e.RowIndex].Cells[10].Value.ToString());
                string beatBoardCode = CodeNum.ONECodes(this.dataGridView4.Rows[e.RowIndex].Cells[11].Value.ToString());
                string beatBoardA1 = CodeNum.GetNullUnit(this.dataGridView4.Rows[e.RowIndex].Cells[12].Value.ToString());
                //int i = dateM.Rows.Count;
                ModifyPlcAddress(pid, workID, stanCode, boardName, boardCode, maxBoardCode, minBoardCode, resultBoardCode, beatBoardCode, beatBoardA1);
                button23_Click(null, null);
            }
        }

        /// <summary>
        /// 删除某一行，并记录删除的数据
        /// </summary>
        private void DeleteRowFromDataGridView<T>(DataGridView dataGridView, DataGridViewCellEventArgs e,
            string tableName, string primaryKeyField, string[] logFields, int primaryKeyColumnIndex,
            string connectionString, string btnName, Dictionary<string, string> fieldAliases, string viewName)
        {
            // 检查选中的行是否为空
            if (dataGridView.Rows[e.RowIndex].Cells[primaryKeyColumnIndex].Value == null ||
                string.IsNullOrWhiteSpace(dataGridView.Rows[e.RowIndex].Cells[primaryKeyColumnIndex].Value.ToString()))
            {
                //MessageBox.Show("选中的行无效或编号为空，无法删除！");
                return;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == btnName)
            {
                // 获取选中行的主键值，并将其转换为泛型类型
                T primaryKeyValue = (T)dataGridView.Rows[e.RowIndex].Cells[primaryKeyColumnIndex].Value;

                // 根据主键类型创建 SQL 语句
                string primaryKeyCondition = primaryKeyValue is string ? $"'{primaryKeyValue}'" : primaryKeyValue.ToString();

                // 查询要删除的行数据
                mdb = new mdbDatas(connectionString);
                string selectSql = $"SELECT * FROM {tableName} WHERE [{primaryKeyField}] = {primaryKeyCondition}";
                DataTable rowTable = mdb.Find(selectSql);

                if (rowTable.Rows.Count > 0)
                {
                    DataRow row = rowTable.Rows[0];

                    // 生成日志详细数据
                    string logDetail = $"编号: {row[primaryKeyField]}";
                    foreach (var field in logFields)
                    {
                        //logDetail += $" | {field}: {row[field]}";
                        if (fieldAliases.TryGetValue(field, out string alias))
                        {
                            logDetail += $" | {alias}：{row[field]}";
                        }
                        else
                        {
                            // 如果映射中没有找到别名，就使用字段名
                            logDetail += $" | {field}：{row[field]}";
                        }
                    }

                    // 执行删除操作
                    string deleteSql = $"DELETE FROM {tableName} WHERE [{primaryKeyField}] = {primaryKeyCondition}";
                    bool bl = mdb.Del(deleteSql);

                    if (bl)
                    {
                        MessageBox.Show("删除成功");
                        loggerConfig.Trace($"【{viewName}删除】\n" +
                                           $"成功删除第{primaryKeyCondition}行, 该行的详细数据: \n{logDetail}");

                        button23_Click(null, null);
                        BtnRefreshAtBulletin(null, null);
                    }
                }

                mdb.CloseConnection();
                //button23_Click(null, null);
            }
        }

        /// <summary>
        /// 更新或新增 DataGridView4 的数据
        /// </summary>
        private void ModifyPlcAddress(string pid, string workID, string stanCode, string boardName, string boardCode, string maxBoardCode, string minBoardCode, string resultBoardCode, string beatBoardCode, string beatBoardA1)
        {
            mdb = new mdbDatas(path4);
            string selectSql = $"select * from Board where [ID] = {pid}";
            DataTable table1 = mdb.Find(selectSql);

            // 修改
            if (table1.Rows.Count > 0)
            {
                DataRow row = table1.Rows[0];

                // 修改前的详细数据
                string logDetail = $"修改前的详细信息：\n" +
                                   $"编号：{row["ID"]} | 工位序号：{row["WorkID"]} | 测试项：{row["BoardName"]} | " +
                                   $"实际值点位：{row["BoardCode"]} | 上限值点位：{row["MaxBoardCode"]} | " +
                                   $"下限值点位：{row["MinBoardCode"]} | 节拍点位：{row["BeatBoardCode"]} | " +
                                   $"测试结果点位：{row["ResultBoardCode"]} | 单位：{row["BoardA1"]} | " +
                                   $"标准值点位：{row["StandardCode"]}";

                if (table1.Rows.Count > 0)
                {
                    string sql = $"update [Board] set [WorkID] = '{workID}', [BoardName] = '{boardName}', " +
                        $"[BoardCode] = '{boardCode}', [MaxBoardCode] = '{maxBoardCode}', [MinBoardCode] = '{minBoardCode}', " +
                        $"[BeatBoardCode] = '{beatBoardCode}', [ResultBoardCode] = '{resultBoardCode}', [BoardA1] = '{beatBoardA1}', " +
                        $"[StandardCode] = '{stanCode}' where [ID] = {pid}";

                    var result = mdb.Change(sql);
                    if (result == true)
                    {
                        MessageBox.Show("修改成功");
                        string modifyInfo = $"【点位数据修改成功】\n{logDetail}\n修改后的详细信息：\n编号：{pid} | 工位序号：{workID} | 测试项：{boardName} | " +
                            $"实际值点位：{boardCode} | 上限值点位：{maxBoardCode} | 下限值点位：{minBoardCode} | 节拍点位：{beatBoardCode} | " +
                            $"测试结果点位：{resultBoardCode} | 单位：{beatBoardA1} | 标准值点位：{stanCode} ";
                        loggerConfig.Trace(modifyInfo);
                    }
                }

            }
            // 新增
            else
            {
                string sql = "insert into Board ([ID],[WorkID],[StandardCode],[BoardName],[BoardCode],[MaxBoardCode],[MinBoardCode],[BeatBoardCode],[ResultBoardCode],[BoardA1]) values ("
                    + pid + ",'" + workID + "','" + stanCode + "','" + boardName + "','" + boardCode + "','" + maxBoardCode + "','" + minBoardCode + "','" + beatBoardCode + "','" + resultBoardCode + "','" + beatBoardA1 + "')";

                bool result = mdb.Add(sql.ToString());
                if (result == true)
                {
                    MessageBox.Show("新增成功");
                    loggerConfig.Trace($"【点位数据新增成功】\n新增详情：\n" +
                        $"编号：{pid} | 工位序号：{workID} | 测试项：{boardName} | " +
                        $"实际值点位：{boardCode} | 上限值点位：{maxBoardCode} | 下限值点位：{minBoardCode} | " +
                        $"节拍点位：{beatBoardCode} | 测试结果：{resultBoardCode} | 单位：{beatBoardA1} | 标准值点位：{stanCode} ");
                }
            }

            mdb.CloseConnection();
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            { return; }

            // 需要记录日志的字段
            string[] logFields = { "CName", "TooName" };

            // 定义字段与别名的映射
            Dictionary<string, string> fieldAliases = new Dictionary<string, string>
            {
                { "CName", "产品名称" },
                { "TooName", "条码规则" },
            };

            // 删除
            DeleteRowFromDataGridView<string>(dataGridView5, e, "Codes", "ID", logFields, 2, path4, btnName: "btnCol2", fieldAliases, "配方信息");

            // 保存
            if (dataGridView5.Columns[e.ColumnIndex].Name == "btnCol")
            {
                Codes codes = bindingCodes[e.RowIndex];

                // 检查是否是修改操作
                bool isModification = !string.IsNullOrEmpty(codes.ID) && codes.ID != "0";
                if (isModification)
                {
                    // 修改前的 Codes 信息
                    Codes originalCodes = CodesServer.GetCodes(codes.ID);
                    if (originalCodes != null)
                    {
                        string originalInfo = $"编号：{originalCodes.ID} | 产品名称：{originalCodes.CName} | " +
                                              $"条码验证型号与工装编号：{originalCodes.TooName} | 产品编码：{originalCodes.MateName}";

                        // 保存新的信息
                        string saveResult = CodesServer.GetCodesSave(codes);

                        if (saveResult == LanguageResour.PassBtnSave)
                        {
                            // 修改后的信息
                            string modifiedInfo = $"编号：{codes.ID} | 产品名称：{codes.CName} | " +
                                                  $"条码验证型号与工装编号：{codes.TooName} | 产品编码：{codes.MateName}";

                            // 记录修改日志
                            string modifyInfo = $"【产品信息修改成功】\n" +
                                                $"修改前的详细信息：\n{originalInfo}\n" +
                                                $"修改后的详细信息：\n{modifiedInfo}";
                            loggerConfig.Trace(modifyInfo);
                            MessageBox.Show("修改成功", "提示");
                        }
                    }
                    else
                    {
                        string saveResult = CodesServer.GetCodesSave(codes);
                        if (saveResult == LanguageResour.PassBtnSave)
                        {
                            string logDetail = $"【配方信息保存成功】\n" +
                                $"编号：{codes.ID} | 产品名称：{codes.CName} | " +
                                $"条码验证型号与工装编号：{codes.TooName} | 产品编码：{codes.MateName}";
                            loggerConfig.Trace(logDetail);
                            MessageBox.Show("保存成功", "提示");
                        }
                    }
                }
            }
        }

        #endregion

        #region ------------ 条码验证 ------------

        public void PLCBarQRCode()
        {
            Utility.DGVButAdd.DatGridBut.DataGdvewBut(dataGridView6);
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            { return; }
            BarcodeVefictn barcodeVefictn = bindingVefictn[e.RowIndex];
            if (dataGridView6.Columns[e.ColumnIndex].Name == "BtnDel")
            {
                //说明点击的列是DataGridViewButtonColumn列
                MessageBox.Show(barcodeVefictn.Delete());
                button23_Click(null, null);
            }
            //保存
            if (dataGridView6.Columns[e.ColumnIndex].Name == "BtnSave")
            {
                MessageBox.Show(barcodeVefictn.Save());
                //说明点击的列是DataGridViewButtonColumn列
                button23_Click(null, null);
            }
        }

        #endregion

        #region ------------ 读卡器 ------------

        RfidReader Reader = new RfidReader();
        List<UserInfoEntity> userInfoEntities;
        Task taskProcess_UserID = null;
        bool IsRunningplc_UserID = true;
        bool isReaderOpen = false;
        string PuserUID = string.Empty;

        public void SearchPort()
        {
            string[] ports = SerialPort.GetPortNames();

            cmbShowPort.Items.Clear();
            cmbShowPort.Text = null;

            foreach (string port in ports)
            {
                cmbShowPort.Items.Add(port);
            }

            if (ports.Length > 0)
            {
                cmbShowPort.Text = ports[0];
            }
            //else
            //{
            //    MessageBox.Show("没有发现可用端口");
            //}
        }

        /// <summary>
        /// 搜索读卡器端口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchPort_Click(object sender, EventArgs e)
        {
            SearchPort();
        }

        /// <summary>
        /// 连接读卡器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenReader_Click(object sender, EventArgs e)
        {
            try
            {
                Reader.DisConnect();

                // 判断读卡器当前连接状态
                if (isReaderOpen)
                {
                    btnOpenReader.Enabled = false;
                    isReaderOpen = false;
                    btnOpenReader.Text = resources.GetString("readCard_Open");  // 打开读卡器
                    Task.WaitAll(taskProcess_UserID);
                    Thread.Sleep(1000);
                    btnOpenReader.Enabled = true;
                }
                else
                {
                    if (cmbShowPort.Text.Length < 1 || tbxReaderDeviceID.Text.Length < 1)
                    {
                        lblReaderState.Text = (resources.GetString("posType"));   // 读卡器端口、设备号获取失败、请先维护！
                        lblReaderState.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        // 连接读卡器
                        if (cmbShowPort.Text.Length < 1 || tbxReaderDeviceID.Text.Length < 1)
                        {
                            lblReaderState.Text = (resources.GetString("posType"));
                            lblReaderState.ForeColor = Color.Red;
                            return;
                        }
                        else
                        {
                            bool flg = Reader.Connect(cmbShowPort.Text, 9600);
                            if (flg == true)
                            {
                                lblReaderState.Text = "成功连接读卡器";
                                lblReaderState.ForeColor = Color.Green;
                                isReaderOpen = true;
                                btnOpenReader.Text = resources.GetString("readCard_Close");  // 关闭读卡器
                                taskProcess_UserID = new Task(Process_UserID);
                                taskProcess_UserID.Start();
                            }
                            else
                            {
                                lblReaderState.Text = (resources.GetString("portNG"));  // 状态显示：端口打开失败！
                                lblReaderState.ForeColor = Color.Red;
                                return;
                            }
                        }

                    }
                }
            }
            catch
            {
                lblReaderState.Text = (resources.GetString("portType"));    // 状态显示：端口被占用！
                lblReaderState.ForeColor = Color.Red;
                return;
            }
        }

        private void Process_UserID()
        {
            while (isReaderOpen)
            {
                this.Invoke(new Action(() =>
                {
                    timer1_Tick(null, null);
                }));

                Thread.Sleep(500);
                Application.DoEvents();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                int status;
                byte[] type = new byte[2];
                byte[] id = new byte[4];

                Reader.Cmd = Cmd.M1_ReadId;//读卡号命令
                Reader.Addr = Convert.ToByte(tbxReaderDeviceID.Text, 16);//读写器地址,设备号
                Reader.Beep = Beep.On;

                status = Reader.M1_Operation();
                if (status == 0)//读卡成功
                {
                    for (int i = 0; i < 2; i++)//获取2字节卡类型
                    {
                        type[i] = Reader.RxBuffer[i];
                    }
                    for (int i = 0; i < 04; i++)//获取4字节卡号
                    {
                        id[i] = Reader.RxBuffer[i + 2];
                    }

                    string userid = byteToHexStr(id, 4);
                    if (userid.Length > 0)
                    {
                        tbxBrandID.Text = userid;
                        PuserUID = tbxBrandID.Text;

                        //查询用户
                        //根据userid 读取账号密码
                        List<UserInfoEntity> list = userInfoEntities.Where(x => x.cardID == PuserUID).ToList();
                        if (list.Count > 0)
                        {
                            if (list.Count > 1)
                            {
                                KeyenceMcNet.Write(deviceInfo.EndNFC, -1);
                            }
                            foreach (var v in list)
                            {
                                int Paccess = 0;
                                if (v.Uuser.Length > 0 && v.Upwd.Length > 0)
                                {
                                    if (v.Utype == "ADM")
                                    {
                                        Paccess = 5;
                                    }
                                    else if (v.Utype == "PE")
                                    {
                                        Paccess = 2;
                                    }
                                    else if (v.Utype == "QE")
                                    {
                                        Paccess = 4;
                                    }
                                    else if (v.Utype == "ME")
                                    {
                                        Paccess = 3;
                                    }
                                    else if (v.Utype == "OP")
                                    {
                                        Paccess = 1;
                                    }
                                }
                                lblPlcAccess.Text = Paccess.ToString();
                                KeyenceMcNet.Write(deviceInfo.EndNFC, Paccess);
                                loggerAccount.Trace($"【PLC触摸屏当前权限信息】\n 工号：{v.Uuser} | 姓名：{v.userName} | 权限：{v.Utype}");
                            }
                        }
                        else
                        {
                            KeyenceMcNet.Write(deviceInfo.EndNFC, -1);
                        }

                        PuserUID = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string byteToHexStr(byte[] bytes, int len)  //数组转十六进制字符
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < len; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        #endregion

        #region ------------ PLC测试 ------------

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button27_Click(object sender, EventArgs e)
        {
            textBox31.Text = KeyenceMcNet.ReadInt32(textBox23.Text).Content.ToString();
            MessageBox.Show("执行完成！");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            KeyenceMcNet.Write(textBox29.Text, int.Parse(textBox36.Text));
            MessageBox.Show("执行完成！");
        }

        #endregion

        #region ------------ Mes统计信息 ------------

        private void button37_Click(object sender, EventArgs e)
        {
            richTextBox5.Clear();
            string paraams = richTextBox6.Text;
            string outMES = BydWorkCom.GetParamsAsy(paraams);
            richTextBox5.Text = outMES + "\r\n";
        }

        #endregion

        private void dataGridViewDynamic1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                //示例：根据第4列的状态0,1,2 显示不同的颜色
                string status = dataGridViewDynamic1.Rows[e.RowIndex].Cells[7].Value.ToString();
                switch (status)
                {
                    case "NG":
                        dataGridViewDynamic1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        break;
                    case "OK":
                        //dataGridViewDynamic1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                        break;
                }
            }
            catch
            {

            }
        }
    }
}