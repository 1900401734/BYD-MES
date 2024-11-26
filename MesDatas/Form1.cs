using HFrfid;
using HslCommunication;
using HslCommunication.Core;
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
using Org.BouncyCastle.Utilities.Net;
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
    public partial class Form1 : Form
    {
        #region ------------ 登录状态相关的属性与方法 ------------

        private int MES { get; set; }
        public void SetMES(int strText)
        {
            MES = strText;
        }

        /// <summary>
        /// 登录模式 (联机或单机) 0=联机  1=单机
        /// </summary>
        public string LoginMode { get; private set; }
        private int isOffLine { get; set; }
        public void SetoffLineType(int strText)
        {
            isOffLine = strText;
            LoginMode = isOffLine == 0 ? "联机" : "单机";
        }

        /// <summary>
        /// 登录方式(刷卡或密码)
        /// </summary>
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

        #region ------------ KPI产线关键性能指标字段 ------------

        /// <summary>
        ///产品型号 = 产品名称 = 产品编号
        /// </summary>
        private string productModel;
        /// <summary>
        /// 生产总数
        /// </summary>
        private string D1080;
        /// <summary>
        /// 工单数量
        /// </summary>
        private string D1084;
        /// <summary>
        /// 完成数量
        /// </summary>
        private string D1086;
        /// <summary>
        /// 合格数量
        /// </summary>
        private string D1088;
        /// <summary>
        /// 生产节拍
        /// </summary>
        private string D1090;
        /// <summary>
        /// 保养计数
        /// </summary>
        private string D1082;
        /// <summary>
        /// NG数量
        /// </summary>
        private string D1076;
        /// <summary>
        /// 完成率
        /// </summary>
        private string completeRate;
        /// <summary>
        /// 合格率
        /// </summary>
        private string passRate;
        /// <summary>
        /// 工序时间
        /// </summary>
        private string processTime;
        /// <summary>
        /// 利用时间
        /// </summary>
        private string usingTime;
        /// <summary>
        /// 负荷时间
        /// </summary>
        private string loadTime;
        /// <summary>
        /// 直通率 FPY：First-pass yield = TPY：ThroughPut Yield
        /// </summary>
        private string FPY;
        /// <summary>
        /// 设备状态
        /// </summary>
        private string deviceState;
        /// <summary>
        /// 成品名称
        /// </summary>
        private string productName;

        #endregion

        /// <summary>
        /// <para>
        /// MES_Result[2002] = "1" 条码验证通过，
        /// MES_Result[2004] = "1" 条码验证失败；
        /// </para>
        /// MES_Result[2006] = "1" 结果上传成功，
        /// MES_Result[2008] = "1" 结果上传失败；
        /// <para>
        /// MES_Result[3688] = "3" 产品总结果OK，否则NG；
        /// </para>
        /// </summary>
        public string[] MES_Result = new string[10000];
        string[] Value = new string[10000];
        public string workOrder;
        public string appVersion;
        public string fileVersion;
        public bool isProductOK;    // 产品测试总结果

        // 存储测试项目对应的PLC点位数据
        string[] targetStationNum = new string[] { };   // 目标工位序号
        string[] testItemsName = new string[] { };      // 测试项目名称
        string[] actualValuePoint = new string[] { };   // 实际值点位
        string[] maxValuePoint = new string[] { };      // 上限点位
        string[] minValuePoint = new string[] { };      // 下限点位
        string[] beatPoint = new string[] { };          // 节拍点位
        string[] testResultPoint = new string[] { };    // 结果点位
        string[] unitName = new string[] { };           // 单位
        string[] standardValuePoint = new string[] { }; // 标准值点位

        // 存储ReadData对应的测试数据
        List<string> actualValueList = null;// 实际值
        List<string> beatList = null;       // 节拍
        List<string> maxList = null;        // 上限
        List<string> minList = null;        // 下限
        List<string> resultList = null;     // 结果
        List<string> stationNameList = null;// 工位名称

        string[] stationNameSets = new string[] { };// 工位名称集合
        string[] kpisPointSets = new string[] { };  // 生产指标点位集合
        string[] kpisNameSets = new string[] { };   // 生产指标名称集合

        MDBHelper dbHelper = null;
        Assembly assembly = Assembly.GetExecutingAssembly();
        ResourceManager resources = null;
        int LanguageId = 0;

        public static int iOperCount = 0;
        public static System.Timers.Timer timer;    // 用于ADM计时退出
        private System.Windows.Forms.Timer timer1;  // 用于实时更新时间

        private static HslCommunication.Core.Net.NetworkDeviceBase KeyenceMcNet;

        private bool isPLCConnected = false;

        private Dictionary<System.Windows.Forms.CheckBox, bool> checkBoxStates = new Dictionary<System.Windows.Forms.CheckBox, bool>();
        Dictionary<string, string> faultsMap = new Dictionary<string, string>();    // 故障映射
        DatasModel.DeviceInformation deviceInfo = new DatasModel.DeviceInformation();
        short[] faultTime = new short[] { };
        DataTable KPIsTable = null;     // KPIS：Key Performance Indicators 关键性能指标，生产指标
        string recipeId = string.Empty; // 配方号

        // 字典用于存储每个CheckBox的初始状态
        Logger loggerConfig = LogManager.GetLogger("ArgumentConfigLog");
        Logger loggerAccount = LogManager.GetLogger("AccountManageLog");
        Logger logProduction = LogManager.GetLogger("ProductionLog");

        public Form1()
        {
            string language = Properties.Settings.Default.DefaultLanguage;
            LanguageResour languageResour = new LanguageResour();
            if (language == "zh-CN")
            {
                LanguageId = 0;
                resources = new ResourceManager("MesDatas.Language_Resources.language_Chinese", assembly);
            }
            else if (language == "en-US")
            {
                LanguageId = 1;
                resources = new ResourceManager("MesDatas.Language_Resources.language_English", assembly);
            }
            else if (language == "th-TH")
            {
                LanguageId = 2;
                resources = new ResourceManager("MesDatas.Language_Resources.language_Thai", assembly);
            }

            this.WindowState = FormWindowState.Maximized;

            InitializeComponent();

            InitializeTimer();      // 实时更新当前时间

            Control.CheckForIllegalCrossThreadCalls = false;

            // 开启监听键盘和鼠标操作
            Application.AddMessageFilter(new MyIMessageFilter());
        }

        // 窗体加载时触发
        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            WhiteNightShift.GetShift(DateTime.Now);

            SearchPort();               // 初始化刷卡器端口

            LoadDashboardConfig();      // 读取看板参数配置

            LoadParameter_MES();        // 读取联机参数设置

            SYS_BOARD();                // 读取PLC点位集合    

            LoadSystemConfig();         // 读取系统设置

            InitializeDirectoryTree();  // 初始化加载本地历史数据

            PLCBarQRCode();             // 条码验证表格

            //bvlist = BarcodeVefictnServer.GetBarcodeVefictnList(LanguageId);

            this.lblDeviceName.Text = txtDeviceName.Text;   // 写机台名称
            FontStyle fontStyle = FontStyle.Bold;           // 设置字体粗细
            float size = 42F;                               // 字体大小
            changeLabelFont(lblDeviceName, size, fontStyle);// 内字体随着字数的增加而自动减小

            if (txtStationNameSets.Text.Length > 0)
            {
                stationNameSets = txtStationNameSets.Text.ToString().Split(new char[] { '|' }); // 工位名称
                kpisPointSets = txtPointSets.Text.ToString().Split(new char[] { '|' });     // 生产指标点位集合
                kpisNameSets = txtNameSets.Text.ToString().Split(new char[] { '|' });       // 生产指标名称集合
            }

            btnRefreshUser_Click(null, null);  // 用户管理刷新按钮

            GetPrinterName();           // 加载打印机

            LoadPrinterConfig();        // 加载打印信息

            InitializeSerialNumber();   // 初始化流水号

            UpLoginInfo();              // 修改最后登录时间和次数

            try
            {
                ConiferFile coniferFile = ConiferFile.GetJson();
                lblVersion.Text = "版本号: " + coniferFile.Version;
            }
            catch { }
        }

        // 窗体加载后触发
        private void Form1_Shown(object sender, EventArgs e)
        {
            AssignUI(); // 根据用户权限分配主界面菜单

            InitializeCheckBoxStates(this.Controls);

            LogManager.Configuration.Variables["LoginName"] = LoginName;

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
            catch
            {

            }
            if (faultsMap == null)
            {
                faultsMap = new Dictionary<string, string>();
            }

            // 用户登录验证
            if (isOffLine == 0)
            {
                VarifyUserLogin_MES();
            }
            else
            {
                InvokeOnUIThreadAsync(() =>
                {
                    lblRunningStatus.ForeColor = Color.Green;
                    lblRunningStatus.Text = resources.GetString("OfflineUser_OK");    // 单机用户验证成功
                    lblOperatePrompt.Text = resources.GetString("WaittingScanBarcode");     // 等待扫描条码
                });
            }

            ConnectDashboard();                         // 连接看板
            ConnectPLC();                               // 连接PLC
            taskUpdateStatus = new Task(Process_MES);   // 更新PLC状态指示灯 & 向PLC反馈看板连接状态
            taskUpdateStatus.Start();
            InitializeModelReadAsync();                 // 读取生产指标
            Process_Offline();                          // 根据状态写模式
            InvokeOnUIThreadAsync(() =>
            {
                lblProductResult.ForeColor = Color.Black;
                lblProductResult.BackColor = Color.White;
                lblProductResult.Text = resources.GetString("Standby"); // 待机
            });           // 初始状态为待机
            InsertTable(null, null);
            rtbProductLog.Clear();
            UTYPE.SelectedIndex = 0;

            stationNameList = new List<string>();
            if (targetStationNum.Length > 0)
            {
                stationNameList = CodeNum.GetStationNameListByID(targetStationNum, stationNameSets);
            }

            // 为 ADM 权限增加定时器
            if (Access == 3)
            {
                timer = new System.Timers.Timer();
                timer.Elapsed += Timer_Elapsed;
                timer.Enabled = true;
                timer.Interval = 1800000;
                timer.Start();
            }

            // 联机用户验证失败,直接返回
            if (isOffLine == 0 && isMesLoginSuccessful == false)
            {
                return;
            }
            if (isOffLine == 1)
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

            barcodeData = "0";
            barcodeInfo = "1";
            isAllowSwitchWorkOrder = true;

            taskReadBar = new Task(ProcessPlc_ReadBarcode); // 读取条码
            taskReadBar.Start();
            taskReadData = new Task(ProcessPlc_ReadData);   // 读取工单号等生产数据
            taskReadData.Start();
            taskBindValue = new Task(Bind_MaxMinValue);     // 绑定上下值数据
            taskBindValue.Start();
            taskReadMaxMin = new Task(Get_MaxMinValue);     // 读取上下值数据
            taskReadMaxMin.Start();
            ConnectReader();                                // 连接读卡器

            if (chkPlcControlPrint.Checked)
            {
                taskProcess_ZPL = new Task(PlcControlPrint);// PLC控制打印
                taskProcess_ZPL.Start();
            }
        }

        // 在窗体关闭时调用此方法
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //关闭标签应用，并且不保存
            if (btApp != null)
            {
                btApp.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges);
            }
            try
            {
                Form1_FormClosing(sender, null);

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                // 记录异常信息
                MessageBox.Show("An error occurred while closing the application: " + ex.Message);
            }
        }

        #region ------------ 杂项 ------------

        private void InitializeTimer()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Tick += Timer_Tick;
            timer1.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// 达到时间间隔发生的方法
        /// </summary>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            iOperCount++;
            if (iOperCount >= 1)
            {
                Console.WriteLine("30分钟未动作程序退出！");
                Environment.Exit(0);
            }
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
                        chkUseFront_TCP.CheckedChanged += CheckBox_CheckedChanged;                    // 使用文字
                        chkLoadModel_TCP.CheckedChanged += CheckBox_CheckedChanged;                    // 读取PLC型号
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
        /// Label内字体随着字数的增加而自动减小，Label大小不变
        /// </summary>
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

        #endregion

        #region  ------------ 主业务逻辑 ------------

        Task taskReadBar = null;
        Task taskReadMaxMin = null;
        Task taskBindValue = null;
        Task taskReadData = null;
        Task taskUpdateStatus = null;
        Task taskProcess_ZPL = null;
        bool isReadBar_PLC = true;      // 开始读条码
        bool isReadData_PLC = true;     // 开始读取生产数据
        bool isReadMaxMin_PLC = true;   // 读取、绑定上下限
        bool IsUpdateStatus_PLC = true; // 更新PLC连接状态
        //private bool isBarcodeVerifySuccessfully = false;
        //List<BarcodeVerification> bvlist;
        //BarcodeVerification bv = null;
        private bool isProcessSuccessfully = true;  // 自动生成条码时使用
        private bool isSaveDataSuccessfully = true;
        private string barcodeData = string.Empty;  // 实时读取或自动生成的条码，用于本地验证、MES验证
        private string barcodeInfo = null;          // 存储二次读取过后的条码，同时用于测试结果上传、上传看板、保存本地、测试结果UI显示
        public static string path4 = System.AppDomain.CurrentDomain.BaseDirectory + "SystemDateBase.mdb";
        public static string pathText = System.AppDomain.CurrentDomain.BaseDirectory + "logfault.txt";
        public static string userFileuRL = "D:\\BYD_Users\\Users_Data.MDB";
        //Logger singleLog = LogManager.GetLogger("InteractionSingle");

        #region ------ 条码读取与验证 ------

        /// <summary>
        /// 读取条码
        /// </summary>
        private void ProcessPlc_ReadBarcode()
        {
            while (isReadBar_PLC)
            {
                ReadBarcode();
            }
            Thread.Sleep(50);
            Application.DoEvents();
        }

        /// <summary>
        /// 读条码/生成条码 -> 条码有效性验证 -> 本地条码验证 -> MES条码验证
        /// </summary>
        /// <remarks>
        /// <para>
        /// 本地条码验证：工装验证 -> 条码重复性验证 -> 条码规则验证 -> 二维码验证
        /// </para>
        /// <para>
        /// 条码规则验证：条码规则有效性验证 -> 条码规范性验证 -> 条码规则验证
        /// </para>
        /// </remarks>
        private void ReadBarcode()
        {
            if (!isPLCConnected) return;
            if (chkAutoBarcodeWithoutVerify.Checked) return;    // 自动生成条码但不进行MES条码验证，适用于气缸机；

            List<BarcodeVerification> bvlist = BarcodeVefictnServer.GetBarcodeVefictnList(LanguageId);
            BarcodeVerification bv = null;

            // 判断是否需要开始条码验证流程
            for (int i = 0; i < bvlist.Count; i++)
            {
                var D1000 = KeyenceMcNet.ReadInt32(bvlist[i].BarcodeStartPLC).Content;
                //var D1000 = KeyenceMcNet.ReadInt32("D1000").Content;
                if (D1000 != 1) return;

                bv = bvlist[i];
                if (bv == null) return;
            }

            // 进行条码验证和二维码验证
            Invoke(new Action(async () =>
            {
                // 初始化UI状态
                InvokeOnUIThreadAsync(() =>
                {
                    // 初始化状态指示灯
                    lblScanBarcodeStatus.ForeColor = Color.Black; // 扫码状态指示灯
                    lblValidationStatus.ForeColor = Color.Black;  // 验证状态指示灯
                    lblUploadStatus.ForeColor = Color.Black;      // 上传状态指示灯

                    lblProductResult.ForeColor = Color.Black;
                    lblProductResult.BackColor = Color.White;
                    lblProductResult.Text = resources.GetString("Standby"); // 待机
                });

                // 是否自动生成条码
                IsAutoGenerateBarcode(bv);

                // 条码有效性验证：判断是否为空引用、空字符或仅空格
                if (string.IsNullOrWhiteSpace(barcodeData))
                {
                    // 更新UI
                    lblScanBarcodeStatus.ForeColor = Color.Red;
                    txtShowBarcode.ForeColor = Color.Red;
                    txtShowBarcode.Text = bv.BarcodeIsNullOrWhiteSpace;

                    // 向PLC反馈条码状态
                    try
                    {
                        KeyenceMcNet.Write(bv.ErrorBarcodeEndPLC, 1);
                        DisplayMessage($"条码有效性验证：条码为空引用、空字符或仅空白，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"条码有效性验证：条码为空引用、空字符或仅空白，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                    }

                    return;
                }

                // 本地条码验证（工装验证 -> 重复性验证 -> 条码规则验证 -> 二维码验证）
                if (!chkBanBarcodeVerificationLocally.Checked)
                {
                    // 工装验证
                    if (!chkBypassFixtureValidation.Checked)
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

                                string[] frockC = txtFixtureBinding.Text.Split('+');

                                // 验证条码则保存到数据库
                                if (CodeNum.CompareArray(expectedFixtures, frockC))
                                {
                                    dbHelper = new MDBHelper(path4);
                                    DataTable table1 = dbHelper.Find("select * from SytemSet where ID = '1'");
                                    if (table1.Rows.Count > 0)
                                    {
                                        string sql = "update [SytemSet] set [BoardBeat]='" + txtFixtureBinding.Text + "' where [ID] = '1'";
                                        var result = dbHelper.Change(sql);
                                    }
                                    dbHelper.CloseConnection();
                                    lblOperatePrompt.Text = resources.GetString("WaittingScanBarcode");    // 等待扫描条码
                                }

                                lblScanBarcodeStatus.ForeColor = Color.Green;
                                lblRunningStatus.ForeColor = Color.Green;
                                lblRunningStatus.Text = resources.GetString("Fixture_OK");      // 工装编号验证通过

                                try
                                {
                                    KeyenceMcNet.Write(bv.PassBarcodeEndPLC, 2);
                                    DisplayMessage($"工装编号验证：工装编号验证通过，反馈(条码验证{bv.PassBarcodeEndPLC} = 2)成功");
                                }
                                catch (Exception ex)
                                {
                                    DisplayMessage($"工装编号验证：工装编号验证通过，反馈(条码验证{bv.PassBarcodeEndPLC} = 2)失败：{ex}");
                                }

                                return;
                            }
                            else
                            {
                                lblRunningStatus.ForeColor = Color.Red;
                                lblRunningStatus.Text = resources.GetString("Fixture_NG");
                                lblValidationStatus.ForeColor = Color.Red;

                                try
                                {
                                    KeyenceMcNet.Write(bv.ErrorBarcodeEndPLC, 2);
                                    DisplayMessage($"工装编号验证：工装编号验证未通过，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 2)成功");
                                }
                                catch (Exception ex)
                                {
                                    DisplayMessage($"工装编号验证：工装编号验证未通过，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 2)失败：{ex}");
                                }
                                return;
                            }
                        }
                    }

                    // 条码重复性验证：只要产品已经做过并且数据被成功保存在本地，不论产品结果是OK还是NG都会被判定为重复
                    if (!chkBanLocalHistoricalData.Checked)
                    {
                        string dataPath = $"{lblDataPath.Text}\\{DateTime.Now:Y}生产数据.mdb";
                        string selectSql = $" SELECT * FROM [Sheet1] WHERE 条码 = '{barcodeData}' ";

                        // 屏蔽本地NG数据：仅保留OK数据参与验证
                        if (chkBypassLocalNgHistoricalData.Checked)
                        {
                            selectSql += " AND 测试结果 = 'OK' ";
                        }

                        dbHelper = new MDBHelper(dataPath);
                        bool isDataExist = dbHelper.DoesDataExist(selectSql);
                        if (isDataExist)
                        {
                            InvokeOnUIThreadAsync(() =>
                            {
                                // 更新UI
                                lblValidationStatus.ForeColor = Color.Red;
                                lblRunningStatus.ForeColor = Color.Red;
                                lblRunningStatus.Text = bv.BarcodeIsRepeatdly;
                            });

                            try
                            {
                                KeyenceMcNet.Write(bv.ErrorBarcodeEndPLC, 1);
                                DisplayMessage($"条码重复性验证：条码重复扫过，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                            }
                            catch (Exception ex)
                            {
                                DisplayMessage($"条码重复性验证：条码重复扫过，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                            }

                            return;
                        }
                    }

                    // 条码规则验证
                    if (!chkBypassBarcodeValidation.Checked)
                    {
                        // 条码规则验证：若实际获取的条码前缀与设定的条码验证规则的长度和字符都相同 => 通过验证
                        string verificationRule = CodeNum.ExtractBarcodeVerificationRule(cboBarcodeRuleAndFixtures.Text);
                        bool isVerifySuccessfully = VerifyBarcodeLocally(bv, verificationRule);
                        if (!isVerifySuccessfully) return;
                    }

                    // 二维码验证：当前未启用，IsEnableQRcodeVerify = false;
                    if (bv.IsEnableQRcodeVerify)
                    {
                        // 验证二维码前缀
                        if (!chkBypassQRcodeValidation.Checked)
                        {
                            string verificationRule = CodeNum.GetQRCodeVerification(cboBarcodeRuleAndFixtures.Text, codesTable);

                            if (!VerifyBarcodeLocally(bv, verificationRule))
                            {
                                return;
                            }
                        }
                    }
                }

                // MES条码验证
                VerifyBarcode_MES(bv);
                if (MES_Result[2004] == "1") return;

                // 提示"条码已生成"或"扫码完成"
                /*if (chkGenerateBarcode.Checked || chkAutoBarcodeWithoutVerify.Checked)
                {
                    lblRunningStatus.ForeColor = Color.Green;
                    lblRunningStatus.Text = $"条码已生成";
                    lblOperatePrompt.ForeColor = Color.Black;
                    lblOperatePrompt.Text = "等待测试数据上传";
                }*/

                DisplayMessage($"条码验证完成{Environment.NewLine}\r\n--------------------条码验证流程结束--------------------");
            }));

            // 进行物料验证，PLC置3
            var Read_data = KeyenceMcNet.ReadInt32(bvlist[0].BarcodeStartPLC).Content;
            if (Read_data == 3)
            {
                Invoke(new Action(() =>
                {
                    bv = bvlist[0];
                    ushort barcodeLength = bv.GetBarcodeLength();
                    string rawBarcode = KeyenceMcNet.ReadString(bv.BarcodePositionPLC, barcodeLength).Content;
                    barcodeData = CodeNum.CleanString(rawBarcode);
                    txtShowBarcode.Text = barcodeData;
                    //DisplayMessage($"条码【D1100】 = {barcodeData}");

                    if (string.IsNullOrEmpty(this.barcodeData))
                    {
                        lblScanBarcodeStatus.ForeColor = Color.Red;
                        txtShowBarcode.Text = resources.GetString("barCode_State");
                        //DisplayMessage("未获取到条码，请重新扫描！");
                        try
                        {
                            //KeyenceMcNet.Write("D1005", 1);
                            KeyenceMcNet.Write(bv.ErrorBarcodeEndPLC, 1);
                            //DisplayMessage("反馈条码验证【D1005】 = 1");
                        }
                        catch (Exception ex)
                        {
                            //DisplayMessage(ex.ToString());
                        }
                        return;
                    }
                    string[] frockA = textBox42.Text.ToString().Split('+');
                    if (frockA.Contains(this.barcodeData))
                    {
                        lblScanBarcodeStatus.ForeColor = Color.Green;
                        lblRunningStatus.ForeColor = Color.Green;
                        lblRunningStatus.Text = resources.GetString("material_OK");
                        try
                        {

                            //KeyenceMcNet.Write("D1003", 3);
                            KeyenceMcNet.Write(bv.PassBarcodeEndPLC, 3);
                        }
                        catch (Exception ex)
                        {
                            //DisplayMessage(ex.ToString());
                        }
                        //DisplayMessage("反馈条码验证【D1003】 =3");
                        return;
                    }
                    else
                    {
                        lblRunningStatus.ForeColor = Color.Red;
                        lblRunningStatus.Text = resources.GetString("material_NG");
                        lblValidationStatus.ForeColor = Color.Red;
                        //DisplayMessage("物料验证失败，请重新扫描！");
                        try
                        {
                            //KeyenceMcNet.Write("D1005", 3);
                            KeyenceMcNet.Write(bv.ErrorBarcodeEndPLC, 3);
                            //DisplayMessage("反馈条码验证【D1005】 =3");
                        }
                        catch (Exception ex)
                        {
                            //DisplayMessage(ex.ToString());
                        }
                        return;
                    }
                }));
            }
        }

        private void IsAutoGenerateBarcode(BarcodeVerification bv)
        {
            // 自动生成条码或实时读取条码
            if (chkGenerateBarcode.Checked)
            {
                // 确保数据未上传完成无法生成条码
                if (!isProcessSuccessfully) return;
                // 生成条码
                barcodeData = AutoGenerateBarcode(txtBarcodeNumber.Text);
                DisplayMessage($"【条码验证流程开始】{Environment.NewLine}\r\n生成的条码为：{barcodeData} ");
                // 更新UI及相关控件可用性
                txtShowBarcode.ForeColor = Color.Black;
                txtShowBarcode.Text = lblBarcodeContent.Text = barcodeData;
                txtBarcodeNumber.Enabled = false;
                txtSN.Enabled = false;
                // 更新字段
                barcodeInfo = barcodeData;
                isProcessSuccessfully = false;
            }
            else
            {
                // 读取条码 (D1100)
                ushort barcodeLength = bv.GetBarcodeLength();
                string rawBarcode = KeyenceMcNet.ReadString(bv.BarcodePositionPLC, barcodeLength).Content;
                // 清理条码
                barcodeData = CodeNum.CleanString(rawBarcode);
                DisplayMessage($"【条码验证流程开始】{Environment.NewLine}\r\n读取的条码为：{barcodeData} ");
                // 更新UI
                txtShowBarcode.ForeColor = Color.Black;
                txtShowBarcode.Text = barcodeData;
                // 更新字段
                barcodeInfo = barcodeData;
            }
        }

        /// <summary>
        /// 本地条码验证（）
        /// </summary>
        /// <param name="bv"></param>
        private void VerifyBarcodeLocally(BarcodeVerification bv)
        {

        }

        /// <summary>
        /// 条码上传MES验证，单机直接通过
        /// </summary>
        /// <param name="bv"></param>
        private void VerifyBarcode_MES(BarcodeVerification bv)
        {
            if (isOffLine == 1)
            {
                lblScanBarcodeStatus.ForeColor = Color.Green;
                lblValidationStatus.ForeColor = Color.Green;
                lblRunningStatus.ForeColor = Color.Green;
                lblRunningStatus.Text = bv.PassPrompt;
                lblOperatePrompt.ForeColor = Color.Black;
                lblOperatePrompt.Text = "请开始生产！";

                try
                {
                    KeyenceMcNet.Write(bv.PassBarcodeEndPLC, 1);
                    DisplayMessage($"MES条码验证：单机模式条码验证通过，反馈(条码验证{bv.PassBarcodeEndPLC} = 1)成功");
                }
                catch (Exception ex)
                {
                    DisplayMessage($"MES条码验证：单机模式条码验证通过，反馈(条码验证{bv.PassBarcodeEndPLC} = 1)失败：{ex}");
                }
            }
            else
            {
                // 绑定工单
                if (chkBindWorkOrder.Checked == true)
                {
                    workOrder = txtWorkOrder.Text;
                    MesIntegrationService.BindWorkOrder(workOrder, out bool bindingResult, out string MESFeedback, out string XML);
                }

                // 上传MES进行条码验证
                VarifyBarcode_MES(barcodeData);

                // 更新验证结果并反馈给PLC
                if (MES_Result[2002] == "1")
                {
                    lblScanBarcodeStatus.ForeColor = Color.Green;
                    lblValidationStatus.ForeColor = Color.Green;
                    lblRunningStatus.ForeColor = Color.Green;
                    lblRunningStatus.Text = bv.PassPrompt;
                    lblOperatePrompt.ForeColor = Color.Black;
                    lblOperatePrompt.Text = "请开始生产！";

                    try
                    {
                        KeyenceMcNet.Write(bv.PassBarcodeEndPLC, 1);
                        DisplayMessage($"MES条码验证：MES条码验证通过，反馈(条码验证{bv.PassBarcodeEndPLC} = 1)成功");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"MES条码验证：MES条码验证通过，反馈(条码验证{bv.PassBarcodeEndPLC} = 1)失败：{ex}");
                    }

                }
                else if (MES_Result[2004] == "1")
                {
                    lblScanBarcodeStatus.ForeColor = Color.Green;
                    lblValidationStatus.ForeColor = Color.Red;
                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = bv.MesErrorPrompt;

                    try
                    {
                        KeyenceMcNet.Write(bv.ErrorBarcodeEndPLC, 1);
                        DisplayMessage($"MES条码验证：MES条码验证未通过，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"MES条码验证：MES条码验证未通过，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                    }
                }
                else
                {
                    lblScanBarcodeStatus.ForeColor = Color.Green;
                    lblValidationStatus.ForeColor = Color.Red;
                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = $"MES条码验证出错";

                    try
                    {
                        KeyenceMcNet.Write(bv.ErrorBarcodeEndPLC, 1);
                        DisplayMessage($"MES条码验证：MES条码验证出错(2002 = {MES_Result[2002]}|2004 = {MES_Result[2004]})，反馈(条码验证D1005 = 1)成功");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"MES条码验证：MES条码验证出错(2002 = {MES_Result[2002]}|2004 = {MES_Result[2004]})，反馈(条码验证D1005 = 1)失败：{ex}");
                    }
                }
            }
        }

        /// <summary>
        /// 自动生成条码内容
        /// </summary>
        /// <param name="codeNumber">10位字符串码号</param>
        /// <returns>18位的条码字符串</returns>
        private string AutoGenerateBarcode(string codeNumber)
        {
            DateTime currentDate = DateTime.Now.Date;

            if (ShouldResetSerialNumber())
            {
                ResetSerialNumber();
                SaveLastSavedDate(DateTime.Now.Date);
            }
            else
            {
                SaveLastSavedDate(currentDate);
            }

            // 获取当前流水号
            if (!int.TryParse(txtSN.Text, out int serialNumber))
            {
                serialNumber = 0;
            }
            // 计算新的流水号
            int newSerialNumber = serialNumber + 1;

            // 更新流水号并保存
            string formattedSerialNumber = newSerialNumber.ToString("D5");
            sytemSetDerivedsd.SerialNumber = formattedSerialNumber;
            txtSN.Text = formattedSerialNumber;
            sytemSetDerivedsd.Save();

            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString("D2");

            /*if (month == "10" || month == "11" || month == "12")
            {
                switch (month)
                {
                    case "10": month = "0"; break;
                    case "11": month = "A"; break;
                    case "12": month = "B"; break;
                }
            }*/

            codeNumber = codeNumber += month + day;
            string barcodeInfo = codeNumber + txtSN.Text;
            return barcodeInfo;
        }

        /// <summary>
        /// 条码规则验证（条码规则有效性验证 -> 条码规范性验证 -> 条码规则验证）
        /// </summary>
        /// <param name="bv"></param>
        /// <param name="verificationRule">条码验证规则</param>
        /// <returns>验证成功返回True，否则为False</returns>
        private bool VerifyBarcodeLocally(BarcodeVerification bv, string verificationRule)
        {
            // 条码规则有效性验证：判断是否为空引用、空字符或空白
            if (string.IsNullOrWhiteSpace(verificationRule))
            {
                InvokeOnUIThreadAsync(() =>
                {
                    // 更新UI
                    lblValidationStatus.ForeColor = Color.Red;
                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = bv.NoVerificationRule;
                });

                try
                {
                    KeyenceMcNet.Write(bv.ErrorBarcodeEndPLC, 1);
                    DisplayMessage($"条码规则有效性验证：未选择条码验证规则，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                }
                catch (Exception ex)
                {
                    DisplayMessage($"条码规则有效性验证：未选择条码验证规则，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                }

                return false;
            }
            else
            {
                // 从实际读取到的条码中提取条码前缀
                int ruleLength = verificationRule.Length;
                string actualBarcodePrefix = barcodeData.Substring(0, ruleLength);
                string[] validationRule = verificationRule.Split('|');

                // 条码规范性验证：判断条码内容长度是否有误
                if (barcodeData.Length <= 12 || barcodeData.Length <= ruleLength)
                {
                    InvokeOnUIThreadAsync(() =>
                    {
                        lblValidationStatus.ForeColor = Color.Red;
                        lblRunningStatus.ForeColor = Color.Red;
                        lblRunningStatus.Text = bv.BarcodeIsFault;
                    });

                    try
                    {
                        KeyenceMcNet.WaitAsync(bv.ErrorBarcodeEndPLC, 1);
                        DisplayMessage($"条码规范性验证：条码内容不符合规范，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"条码规范性验证：条码内容不符合规范，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                    }

                    return false;
                }

                // 条码规则验证：判断实际条码与设定的条码规则是否匹配
                if (validationRule.Contains(actualBarcodePrefix))
                {
                    return true;
                }
                else
                {
                    InvokeOnUIThreadAsync(() =>
                    {
                        lblValidationStatus.ForeColor = Color.Red;
                        lblRunningStatus.ForeColor = Color.Red;
                        lblRunningStatus.Text = bv.BarcodeRuleDismachPrompt;
                    });

                    try
                    {
                        KeyenceMcNet.Write(bv.ErrorBarcodeEndPLC, 1);
                        DisplayMessage($"条码规则验证：实际条码与设定的条码规则不匹配，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"条码规则验证：实际条码与设定的条码规则不匹配，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                    }

                    return false;
                }
            }
        }

        #endregion

        #region ------ 处理生产数据 ------

        /// <summary>
        /// 读取并上传生产数据
        /// </summary>
        private async void ProcessPlc_ReadData()
        {
            while (isReadData_PLC)
            {
                await ReadData();
            }
            Thread.Sleep(50);
            Application.DoEvents();
        }

        private async Task ReadData()
        {
            if (!isPLCConnected) return;

            var plcValue = await Task.Run(() => KeyenceMcNet.ReadInt32(sytemSetDerivedsd.StartProductPoint).Content);   // D1200

            if (plcValue != 1) return;

            await ProcessProductionData();
        }

        /// <summary>
        /// 读测试数据 -> 读产品总结果 -> 上传MES -> 上传看板 -> 数据显示 -> 本地保存
        /// </summary>
        /// <returns></returns>
        private async Task ProcessProductionData()
        {
            lblOperatePrompt.Text = resources.GetString("begin_read_data");  // 开始读取数据
            //DisplayMessage("准备读取测试结果");

            // 适用于气缸机生成条码
            if (chkAutoBarcodeWithoutVerify.Checked)
            {
                if (!isSaveDataSuccessfully) return;
                // 生成条码
                barcodeInfo = AutoGenerateBarcode(txtBarcodeNumber.Text);
                DisplayMessage($"生成的条码为：{barcodeInfo} ");
                // 更新UI及相关控件可用性
                txtShowBarcode.ForeColor = Color.Black;
                txtShowBarcode.Text = lblBarcodeContent.Text = barcodeInfo;
                txtBarcodeNumber.Enabled = false;
                txtSN.Enabled = false;
                // 更新字段
                isSaveDataSuccessfully = false;
            }

            // 二次读条码
            if (chkReadBarcodeSecondly.Checked)
            {
                await Task.Run(() => ReadBarcodeSecondly());
            }

            // 读取测试结果
            await ReadTestResult();

            // 读取产品总测试结果
            MES_Result[3688] = await Task.Run(() => KeyenceMcNet.ReadInt32(sytemSetDerivedsd.TotalProductPoint).Content.ToString());

            // 更新产品结果UI
            await UpdateProductResultUI();

            isProcessSuccessfully = true;

            // 上传条码及相应测试结果到MES
            await SendToMESAsync();

            // 上传看板
            SendToDashboardAsync();

            // 本地保存
            await SaveDataLocallyAsync();

            await FinalizeProductionCycle();
        }

        private void ReadBarcodeSecondly()
        {
            // 二次读条码：D1050
            ushort.TryParse(sytemSetDerivedsd.SecondProductLength, out ushort secondProductLength);
            string rawBarcode = KeyenceMcNet.ReadString(sytemSetDerivedsd.SecondProductPoint, secondProductLength).Content;
            barcodeInfo = CodeNum.CleanString(rawBarcode);
        }

        /// <summary>
        /// 读取测试结果
        /// </summary>
        private async Task ReadTestResult()
        {
            actualValueList = new List<string>();
            beatList = new List<string>();
            maxList = new List<string>();
            minList = new List<string>();
            resultList = new List<string>();

            if (PLCPointInfoTable.Rows.Count > 0)
            {
                var tasks = PLCPointInfoTable.AsEnumerable().Select(row => Task.Run(() =>
                {
                    var rowData = new
                    {
                        ListItem = ProcessPointData_PLC(row["BoardCode"].ToString()),           // 实际值
                        MaxItem = ProcessPointData_PLC(row["MaxBoardCode"].ToString()),         // 上限
                        MinItem = ProcessPointData_PLC(row["MinBoardCode"].ToString()),         // 下限
                        ResultItem = ProcessPointData_PLC(row["ResultBoardCode"].ToString()),   // 测试结果
                        BeatItem = ProcessPointData_PLC(row["BeatBoardCode"].ToString())        // 节拍
                    };
                    return rowData;

                })).ToList();

                var results = await Task.WhenAll(tasks);
                foreach (var result in results)
                {
                    actualValueList.Add(result.ListItem);
                    maxList.Add(result.MaxItem);
                    minList.Add(result.MinItem);
                    resultList.Add(result.ResultItem);
                    beatList.Add(result.BeatItem);
                }
            }
        }

        /// <summary>
        /// 更新产品结果UI显示
        /// </summary>
        /// <returns></returns>
        private async Task UpdateProductResultUI()
        {
            await InvokeOnUIThreadAsync(() =>
            {
                if (MES_Result[3688] == "3")
                {
                    lblProductResult.ForeColor = Color.White;
                    lblProductResult.BackColor = Color.Green;
                    lblProductResult.Text = "OK";

                    Value[9999] = "OK";
                }
                else
                {
                    lblProductResult.ForeColor = Color.White;
                    lblProductResult.BackColor = Color.Red;
                    lblProductResult.Text = "NG";

                    Value[9999] = "NG";
                }
            });
        }

        private async Task SendToMESAsync()
        {
            if (isOffLine == 1) return;

            InvokeOnUIThreadAsync(() =>
            {
                lblUploadStatus.ForeColor = Color.Orange;
                lblRunningStatus.ForeColor = Color.Black;
                lblRunningStatus.Text = resources.GetString("Mes_upload");  // 联机数据上传中
                lblOperatePrompt.Text = resources.GetString("Wait");        // 请等待
            });

            isProductOK = Value[9999] == "OK";

            // 联机上传条码测试数据
            await Task.Run(() => UploadResult_MES(barcodeInfo));

            InvokeOnUIThreadAsync(() =>
            {
                if (MES_Result[2006] == "1")
                {
                    lblUploadStatus.ForeColor = Color.Green;
                    lblRunningStatus.Text = resources.GetString("Mes_upload_OK");   // 联机数据上传成功
                }
                else if (MES_Result[2008] == "1")
                {
                    lblUploadStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = resources.GetString("Mes_upload_NG");   // 联机数据上传失败
                    lblOperatePrompt.Text = resources.GetString("Re_upload");       // 请重新上传
                }
            });
        }

        private async Task SendToDashboardAsync()
        {
            if (isDashboardConnected)
                Task.Run(() => GenerateProductionData());
        }

        private async Task SaveDataLocallyAsync()
        {
            await InvokeOnUIThreadAsync(() =>
            {
                Num++;
                lblRunningStatus.Text = resources.GetString("Read_data");   // 本地数据保存中
                lblOperatePrompt.Text = resources.GetString("Wait");        // 请等待
            });

            Task.Run(ShowResult);

            Task.Run(SaveProductionDataAsync);

            InvokeOnUIThreadAsync(() =>
            {
                lblRunningStatus.Text = resources.GetString("Read_data_OK");        // 本地数据保存完成
                lblOperatePrompt.Text = resources.GetString("Continue_production"); // 请取下产品继续生产
                //DisplayMessage("数据读取完成");
                //DisplayMessage("生产结果读取完成...");
            });
        }

        private async Task FinalizeProductionCycle()
        {
            try
            {
                await Task.Run(() => KeyenceMcNet.Write(sytemSetDerivedsd.EndProductPoint, 1));
                //DisplayMessage("生产结果读取反馈【D1202】 = 1");
            }
            catch (Exception ex)
            {
                //DisplayMessage(ex.ToString());
            }
            finally
            {
                isSaveDataSuccessfully = true;
            }

            await Task.Delay(200);
            await InvokeOnUIThreadAsync(() => lblRunningStatus.Text = resources.GetString("WaittingScanBarcode"));
        }

        private Task InvokeOnUIThreadAsync(Action action)
        {
            return Task.Run(() => Invoke(action));
        }

        #endregion

        /// <summary>
        /// 更新PLC状态指示灯 & 向 PLC 反馈看板连接状态
        /// </summary>
        private void Process_MES()
        {
            while (IsUpdateStatus_PLC)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (isPLCConnected)
                    {
                        lblPlcStatus.ForeColor = Color.Green;

                        try
                        {
                            // 向 PLC 反馈看板连接状态
                            if (!string.IsNullOrEmpty(deviceInfo.DashboardStatusPoint))
                            {
                                if (isDashboardConnected)
                                {
                                    KeyenceMcNet.Write(deviceInfo.DashboardStatusPoint, 1);
                                }
                                else
                                {
                                    KeyenceMcNet.Write(deviceInfo.DashboardStatusPoint, 0);
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
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
            if (isOffLine == 1)
            {

                lblLoginMode.Text = resources.GetString("loginMode1");  // 离线
                lblCurrentUser.Text = $"{LoginUser}\n({LoginName})";

            }
            else if (isOffLine == 0)
            {
                lblLoginMode.Text = resources.GetString("loginMode");   // 在线
                lblCurrentUser.Text = $"{LoginUser}\n({LoginName})";
            }
        }

        private async void DisplayMessage(string msg)
        {
            this.Invoke(new Action(() =>
            {
                if (rtbProductLog.TextLength > 50000)
                {
                    rtbProductLog.Clear();
                }
                rtbProductLog.AppendText($"{DateTime.Now.ToString("HH:mm:ss_fff")}:{msg}\r\n");
                rtbProductLog.ScrollToCaret();
            }));

            logProduction.Trace($"{msg}");
        }

        #endregion

        #region ------------ MES上传 ------------

        NLog.Logger loggerMESBarCoode = NLog.LogManager.GetLogger("MESBarCoodeLog");    // 记录上传条码后的MES反馈
        NLog.Logger loggerMESData = NLog.LogManager.GetLogger("MESDataLog");            // 记录上传测试结果的后MES反馈
        private bool isMesLoginSuccessful = false;  // 用户联机验证结果

        #region ----- 从文本框获取MES配置参数 -----
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
        #endregion

        /// <summary>
        /// MES参数 > 保存
        /// </summary>
        private void SaveMesConfig_Click(object sender, EventArgs e)
        {
            SaveParameter_MES();
            LoadParameter_MES();
            VarifyUserLogin_MES();
        }

        /// <summary>
        /// 加载MES联机参数到系统
        /// </summary>
        public void LoadParameter_MES()
        {
            dbHelper = new MDBHelper(path4);
            DataTable table = dbHelper.Find("select * from SytemInfo where ID = '1'");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    textBox_ip.Text = table.Rows[i]["IP"].ToString();
                    textBox_port.Text = table.Rows[i]["Port"].ToString();
                    textBox_timeout.Text = table.Rows[i]["Timeout"].ToString();
                    textBox_nccode.Text = table.Rows[i]["NcCode"].ToString();
                    textBox_opration.Text = table.Rows[i]["Opration"].ToString();
                    textBox_password.Text = table.Rows[i]["Password"].ToString();
                    textBox_resource.Text = table.Rows[i]["Resource"].ToString();
                    textBox_site.Text = table.Rows[i]["Site"].ToString();
                    textBox_url.Text = table.Rows[i]["Url"].ToString();
                    textBox_user.Text = table.Rows[i]["User"].ToString();
                }
            }
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 保存MES联机参数到数据库
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

            SaveSystemInfo(path4, infoEntity);
        }

        private void SaveSystemInfo(string conn, SytemInfoEntity systemInfo)
        {
            dbHelper = new MDBHelper(conn);
            DataTable table1 = dbHelper.Find("select * from SytemInfo where ID = '1'");

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

                    var result = dbHelper.Change(sql);
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
                MDBHelper.CreateAccessDatabase(conn);
                MDBHelper.CreateMDBTable(conn, "SytemInfo", new System.Collections.ArrayList(new object[]
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
                dbHelper.DatatableToMdb("SytemInfo", dt);
            }

            dbHelper.CloseConnection();
        }

        /// <summary>
        /// MES配置
        /// </summary>
        /// <param name="ip"></param>
        public void Config_Mes(string ip, string port, string timeout, string url, string site,
            string user, string password, string resource, string operation, string ncCode)
        {
            工艺部信息化组.MesConfig.IP = ip;
            工艺部信息化组.MesConfig.PORT = port;
            工艺部信息化组.MesConfig.TimeOut = int.Parse(timeout);
            工艺部信息化组.MesConfig.URL = url;
            工艺部信息化组.MesConfig.Site = site;
            工艺部信息化组.MesConfig.UserName = user;
            工艺部信息化组.MesConfig.Password = password;
            工艺部信息化组.MesConfig.Resource = resource;
            工艺部信息化组.MesConfig.Operation = operation;
            工艺部信息化组.MesConfig.NcCode = ncCode;
        }

        /// <summary>
        /// MES交互1：用户验证
        /// </summary>
        private void VarifyUserLogin_MES()
        {
            lblRunningStatus.Text = resources.GetString("user_yzz");     // 用户验证中
            lblOperatePrompt.Text = resources.GetString("Wait");         // 请等待

            Config_Mes(ip, port, timeout, url, site, user, password, resource, operation, nccode);

            bool isValidateSuccessful;
            string MESFeedback;
            string XMLOUT;
            MesIntegrationService.VarifyUserLogin(out isValidateSuccessful, out MESFeedback, out XMLOUT);

            if (isValidateSuccessful)
            {
                if (MESFeedback != null)
                {
                    rtbMesLog.Clear();
                    rtbMesLog.AppendText(MESFeedback);

                    lblRunningStatus.ForeColor = Color.Green;
                    lblRunningStatus.Text = resources.GetString("onlineUser_OK");   // 联机用户验证成功
                    lblOperatePrompt.Text = resources.GetString("WaittingScanBarcode");        // 等待扫描条码

                    isMesLoginSuccessful = true;
                }
                else
                {
                    rtbMesLog.Clear();
                    rtbMesLog.AppendText(MESFeedback);

                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = resources.GetString("onlineUser_NG");   // 联机用户验证失败
                    lblOperatePrompt.Text = resources.GetString("Check_param");     // 请检查联机参数
                }
            }
            else
            {
                rtbMesLog.Clear();
                rtbMesLog.AppendText(MESFeedback);

                lblRunningStatus.ForeColor = Color.Red;
                lblRunningStatus.Text = resources.GetString("onlineUser_NG");
                lblOperatePrompt.Text = resources.GetString("Check_param");
            }
        }

        /// <summary>
        /// MES交互2：条码验证
        /// </summary>
        private void VarifyBarcode_MES(string barcodeData)
        {
            bool isVerifySuccessfully;
            string MESFeedback;
            string XMLOUT;
            MesIntegrationService.VarifyBarcode(barcodeData, out isVerifySuccessfully, out MESFeedback, out XMLOUT);

            rtbMesLog.Clear();
            rtbMesLog.AppendText(MESFeedback);
            loggerMESBarCoode.Trace(MESFeedback);
            rtbMesLog.SelectionStart = rtbMesLog.Text.Length;
            rtbMesLog.ScrollToCaret();

            if (isVerifySuccessfully)
            {
                MES_Result[2002] = "1";
            }
            else
            {
                MES_Result[2004] = "1";
            }
        }

        /// <summary>
        /// MES交互3：条码上传
        /// </summary>
        /// <remarks>
        /// <para>验证成功，MES_Result[2006] = "1";
        /// </para>
        /// <para>验证失败，MES_Result[2008] = "1";
        /// </para>
        /// </remarks>
        private void UploadResult_MES(string barcode)
        {
            MES_Result[2006] = "0";
            MES_Result[2008] = "0";
            //DisplayMessage("结果联机上传");

            bool productResult = isProductOK;
            string productBarcode = barcode;
            string 文件版本 = fileVersion;
            string 软件版本 = appVersion;

            StringBuilder sb = new StringBuilder();

            //　工装信息
            string[] fixtureInfo = txtFixtureBinding.Text.Split('+');
            if (fixtureInfo.Length > 0)
            {
                for (int i = 0; i < fixtureInfo.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(fixtureInfo[i]))
                    {
                        sb.Append($"!工装编号{i + 1},工装,{fixtureInfo[i]}");
                    }
                }
            }

            // 产品编码
            string[] productCode = CodeNum.GetProductCodes(cboBarcodeRuleAndFixtures.Text, codesTable);
            if (productCode.Length > 0)
            {
                for (int i = 0; i < productCode.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(productCode[i]))
                    {
                        sb.Append($"!产品物料号{i + 1},物料,{productCode[i]}");
                    }
                }
            }

            // 测试项目名称1、上限点位1、下限点位1、测试结果点位、实际值1、上限值、下限值、测试结果 
            if (actualValuePoint.Length > 0)
            {
                for (int i = 0; i < testItemsName.Length; i++)
                {
                    if (actualValueList[i] != "null")
                    {
                        if (maxValuePoint[i] != "NO" && minValuePoint[i] != "NO" && testResultPoint[i] != "NO")
                        {
                            if (resultList[i] != "null")
                            {
                                sb.Append($"!{testItemsName[i]},{maxList[i]}~{minList[i]},{actualValueList[i]}");
                                sb.Append($"!{testItemsName[i]}测试结果,结果,{resultList[i]}");
                            }
                        }
                        else
                        {
                            sb.Append($"!{testItemsName[i]},测试项值,{actualValueList[i]}");
                        }
                    }
                }
            }

            // !测试项,测试参数,测试值!用户ID,YC,YC!测试人,测试时间,测试结果!YC,2023年8月2日19：1：3,OK!工装,工装代码,41!产品编码,产品名称,产品代码!1144_00,SA3F_5820120,0SF5!生产节拍,文件版本,软件版本!5,20220811,20220811

            string 测试项 = $"!用户ID,{LoginUser},{LoginName}!条码,条码信息,{productBarcode}!产品型号,型号,{txtProductModel.Text}{sb.ToString()}!测试总结果,测试结果,{Value[9999]}";

            bool 验证结果; string MES反馈; string XMLOUT;

            MesIntegrationService.UploadBarcode(productResult, productBarcode, 文件版本, 软件版本, 测试项, out 验证结果, out MES反馈, out XMLOUT);

            rtbMesLog.Clear();
            rtbMesLog.AppendText(MES反馈);
            loggerMESData.Trace(MES反馈);

            if (验证结果 == true)
            {
                MES_Result[2006] = "1";
            }
            else
            {
                //DisplayMessage(MES反馈);
                MES_Result[2008] = "1";
            }

            //DisplayMessage("联机结果上传完成");
        }

        #endregion

        #region ------------ PLC连接 ------------

        private void ConnectPLC()
        {
            BtnConnectPlc_Click(null, null);
        }

        private async void BtnConnectPlc_Click(object sender, EventArgs e)
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
                KeyenceMcNet.ConnectClose();
                KeyenceMcNet.SetPersistentConnection();

                // Task.Run将耗时操作放在后台线程执行
                OperateResult result = await Task.Run(() => KeyenceMcNet.ConnectServer());
                if (result.IsSuccess)
                {
                    isPLCConnected = true;
                }
                else
                {
                    isPLCConnected = false;
                    // 使用 Invoke 确保在 UI 线程上显示消息框
                    await ShowMessageBoxAsync(resources.GetString("plcConn"));
                }
            }
            catch (Exception ex)
            {
                await ShowMessageBoxAsync(ex.Message);
            }
        }

        private Task ShowMessageBoxAsync(string message)
        {
            return Task.Run(() =>
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => MessageBox.Show(message)));
                }
                else
                {
                    MessageBox.Show(message);
                }
            });
        }

        #endregion

        #region ------------ 保存 & 加载系统参数设置 ------------

        DatasModel.SytemSetDerived sytemSetDerivedsd = new DatasModel.SytemSetDerived();

        /// <summary>
        /// 保存系统设置参数，保存的内容范围：
        /// 系统设置 -> 初始化设置；
        /// 通用点位设置 -> 读取生产数据点位
        /// </summary>
        private void SaveSystemConfig()
        {
            // PLC的IP，端口号以及设备名称 -> 必填项
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

            // 功能模块
            sytemSetDerivedsd.SytemNoerifbarcodes = chkBypassBarcodeValidation.Checked; // 屏蔽本地条码验证
            sytemSetDerivedsd.SytemNorifytooling = chkBypassFixtureValidation.Checked;  // 屏蔽本地扫工装
            sytemSetDerivedsd.SytemQRcodNorif = chkBypassQRcodeValidation.Checked;      // 屏蔽本地二维码验证
            sytemSetDerivedsd.SytemNGCodesData = chkBypassLocalNgHistoricalData.Checked;// 屏蔽本地NG历史记录
            sytemSetDerivedsd.SytemHistorCodes = chkBanLocalHistoricalData.Checked;     // 本地历史数据
            sytemSetDerivedsd.ISAutoGenerateBarcode = chkGenerateBarcode.Checked;       // 自动生成条码
            sytemSetDerivedsd.IsAutoBarcodeWithoutVerify = chkAutoBarcodeWithoutVerify.Checked;
            sytemSetDerivedsd.IsSkipBarcodeVerifyLocally = chkBanBarcodeVerificationLocally.Checked;

            sytemSetDerivedsd.SerialNumber = txtSN.Text;                                // 流水号
            sytemSetDerivedsd.BarcodeNumber = txtBarcodeNumber.Text;                    // 码号

            sytemSetDerivedsd.TotalBarcodeCount = int.Parse(lblTotalBarcodesCount.Text);// 条码验证总数量
            sytemSetDerivedsd.BarcodeNGCount = int.Parse(lblBarcodeNGCount.Text);       // 条码验证失败的数量
            sytemSetDerivedsd.BarcodeOKCount = int.Parse(lblBarcodeOKCount.Text);       // 条码验证通过的数量

            sytemSetDerivedsd.TotalMESCount = int.Parse(lblTotalMESCount.Text);         // MES验证总数量
            sytemSetDerivedsd.MESNGCount = int.Parse(lblMESNGCount.Text);               // MES验证失败的数量
            sytemSetDerivedsd.MESOKCount = int.Parse(lblMESOKCount.Text);               // MES验证通过的数量

            sytemSetDerivedsd.SytemSetnullCoden = txtDefaultStyle.Text;                 // 默认数据显示样式
            sytemSetDerivedsd.CurrentPLCType = cboConnectType.Text;                     // PLC连接类型

            sytemSetDerivedsd.Save();

            // PLC参数：IP、端口、
            // 本地数据存放路径：DataPath
            // 其他设置：DeviceName、RFIDPort、读卡器设备号、显示宽度
            // 二次读条码、点位集合、名称集合、
            dbHelper = new MDBHelper(path4);
            DataTable table1 = dbHelper.Find("select * from SytemSet where ID = '1'");

            if (table1.Rows.Count > 0)
            {
                string sql = $"update [SytemSet] set [IP]='{txt_IP.Text}', [Port]='{txt_port.Text}'," +
                    $"[DataUrl]='{lblDataPath.Text}', [DeviceName]='{txtDeviceName.Text}', [rfidProt]='{cmbShowPort.Text}'," +
                    $" [rfidCode]='{tbxReaderDeviceID.Text}', [StatisticsCode]='{txtPointSets.Text}', [StatisticsName]='{txtNameSets.Text}'," +
                    $" [ResultCode]='{txtDisplayWidth.Text}', [readBarCode]='{chkReadBarcodeSecondly.Checked}' where [ID] = '1'";

                var result = dbHelper.Change(sql);
                //SqlToJsonConverter.ConvertToJSONLog(sql);
                if (result)
                {
                    MessageBox.Show("保存成功");
                }
            }
            else
            {
                MDBHelper.CreateAccessDatabase(path4);
                MDBHelper.CreateMDBTable(path4, "SytemSet", new System.Collections.ArrayList(new object[] { "ID", "IP", "Port", "DataUrl", "DeviceName", "stationCode", "stationName", "wordNo" }));
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
                dbHelper.DatatableToMdb("SytemSet", dt);
            }

            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 加载系统配置参数
        /// </summary>
        private void LoadSystemConfig()
        {
            sytemSetDerivedsd = DatasServer.SytemSetDerivedServer.GetSytemSetDerived(1);
            deviceInfo = DatasServer.DeviceInformationServer.GetDeviceInformation(1);

            // 屏蔽
            chkBypassBarcodeValidation.Checked = sytemSetDerivedsd.SytemNoerifbarcodes;     // 屏蔽本地条码验证
            chkBypassFixtureValidation.Checked = sytemSetDerivedsd.SytemNorifytooling;      // 屏蔽本地扫工装
            chkBypassQRcodeValidation.Checked = sytemSetDerivedsd.SytemQRcodNorif;          // 屏蔽本地二维码验证
            chkBypassLocalNgHistoricalData.Checked = sytemSetDerivedsd.SytemNGCodesData;    // 屏蔽本地NG历史数据
            chkBanLocalHistoricalData.Checked = sytemSetDerivedsd.SytemHistorCodes;         // 屏蔽本地历史数据
            chkGenerateBarcode.Checked = sytemSetDerivedsd.ISAutoGenerateBarcode;           // 自动生成条码
            chkAutoBarcodeWithoutVerify.Checked = sytemSetDerivedsd.IsAutoBarcodeWithoutVerify;         // 气缸机自动生成条码
            chkBanBarcodeVerificationLocally.Checked = sytemSetDerivedsd.IsSkipBarcodeVerifyLocally;    // 屏蔽本地条码验证

            txtSN.Text = sytemSetDerivedsd.SerialNumber;                                    // 流水号
            txtBarcodeNumber.Text = sytemSetDerivedsd.BarcodeNumber;                        // 码号

            lblTotalBarcodesCount.Text = sytemSetDerivedsd.TotalBarcodeCount.ToString();    // 条码验证总数量
            lblBarcodeNGCount.Text = sytemSetDerivedsd.BarcodeNGCount.ToString();           // 条码验证失败的数量
            lblBarcodeOKCount.Text = sytemSetDerivedsd.BarcodeOKCount.ToString();           // 条码验证通过的数量    

            lblTotalMESCount.Text = sytemSetDerivedsd.TotalMESCount.ToString();             // MES验证总数量
            lblMESNGCount.Text = sytemSetDerivedsd.MESNGCount.ToString();                   // MES验证失败的数量
            lblMESOKCount.Text = sytemSetDerivedsd.MESOKCount.ToString();                   // MES验证通过的数量   

            cboConnectType.Text = sytemSetDerivedsd.CurrentPLCType;                         // 当前PLC连接类型
            txtDefaultStyle.Text = sytemSetDerivedsd.SytemSetnullCoden;                     // 默认数据显示样式

            // 读取生产数据点位
            txtStartPoint.Text = sytemSetDerivedsd.StartProductPoint;                       // 开始读取点位：D1200
            txtReadSecondly.Text = sytemSetDerivedsd.SecondProductPoint;                    // 二次读取点位：D1050
            txtSecondProductLength.Text = sytemSetDerivedsd.SecondProductLength;            // 二次读取长度：10
            txtEndProductPoint.Text = sytemSetDerivedsd.EndProductPoint;                    // 结束生产点位：D1202
            txtProductResultPoint.Text = sytemSetDerivedsd.TotalProductPoint;               // 产品结果点位：D1078

            // 设备产品点位
            txtDeviceStatePoint.Text = deviceInfo.DeviceStatusPoint;     // 设备状态点位：D1007
            txtProductModelPoint.Text = deviceInfo.ProductModelPoint;    // 产品型号点位：D1120
            txtPMLength.Text = deviceInfo.ProductModelLength;       // 产品型号长度：10
            txtRecipeIdPoint.Text = deviceInfo.RecipeIdPoint;       // 配方号点位：D1208
            textBox43.Text = deviceInfo.ModifyRecipePoint;              // 配方修改：D1204
            textBox47.Text = deviceInfo.ModifyRecipeIDPoint;           // 配方号修改：D1206
            textBox34.Text = deviceInfo.StartNFCPoint;                   // 开始NFC
            textBox35.Text = deviceInfo.EndNFCPoint;                     // 结束NFC
            txtViewStatus.Text = deviceInfo.DashboardStatusPoint;             // 看板状态

            // 连接数据库
            dbHelper = new MDBHelper(path4);
            // 检索 "SystemSet" 表格
            DataTable table1 = dbHelper.Find("select * from SytemSet where ID = '1'");
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
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 系统设置页面 > 通用点位设置页 > 保存按钮
        /// </summary>
        private void PLCPointSaveBtn_Click(object sender, EventArgs e)
        {
            SaveSystemConfig();

            deviceInfo.DeviceStatusPoint = txtDeviceStatePoint.Text;
            deviceInfo.ProductModelPoint = txtProductModelPoint.Text;
            deviceInfo.ProductModelLength = txtPMLength.Text;
            deviceInfo.RecipeIdPoint = txtRecipeIdPoint.Text;
            deviceInfo.ModifyRecipePoint = textBox43.Text;
            deviceInfo.ModifyRecipeIDPoint = textBox47.Text;
            deviceInfo.StartNFCPoint = textBox34.Text;
            deviceInfo.EndNFCPoint = textBox35.Text;
            deviceInfo.DashboardStatusPoint = txtViewStatus.Text;
            deviceInfo.Save();
            loggerConfig.Trace($"通用点位设置保存成功");
        }

        /// <summary>
        /// 系统设置页面的保存按钮
        /// </summary>
        private void BtnSaveAtSystemSetting_Click(object sender, EventArgs e)
        {
            SaveSystemConfig();
            loggerConfig.Trace($"系统设置参数保存成功");
        }

        /// <summary>
        /// 配方设置界面的保存按钮
        /// </summary>
        private void btnSaveRecipeConfig_Click(object sender, EventArgs e)
        {
            SaveSystemConfig();
            loggerConfig.Trace($"名称集合保存成功！\n点位集合保存成功！");
        }

        #endregion

        #region ------------ 上下限数据 ------------

        DataTable maxminTable = null;
        public List<MaxMinValue> ValueList = null;

        /// <summary>
        /// 实时读取上限值
        /// </summary>
        public void Get_MaxMinValue()
        {
            while (isReadMaxMin_PLC)
            {
                GetPLCMaxMin();
                Thread.Sleep(1000);
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 读取上下限
        /// </summary>
        private void GetPLCMaxMin()
        {
            if (isPLCConnected == true)
            {
                ValueList = new List<MaxMinValue>();
                for (int i = 0; i < PLCPointInfoTable.Rows.Count; i++)
                {
                    MaxMinValue value = new MaxMinValue();

                    value.BoardName = PLCPointInfoTable.Rows[i]["BoardName"].ToString();
                    value.StandardCode = NullModify(ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["StandardCode"].ToString()));
                    value.MaxBoardCode = NullModify(ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["MaxBoardCode"].ToString()));
                    value.MinBoardCode = NullModify(ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["MinBoardCode"].ToString()));
                    value.BoardCode = NullModify(ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["BoardCode"].ToString()));
                    value.Result = NullModify(ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["ResultBoardCode"].ToString()));
                    ValueList.Add(value);
                }
            }
        }

        /// <summary>
        /// 实时更新上下限数据
        /// </summary>
        public void Bind_MaxMinValue()
        {
            while (isReadMaxMin_PLC)
            {
                PLCMaxMin();
                Thread.Sleep(1500);
                Application.DoEvents();
            }
        }

        private void PLCMaxMin()
        {
            BeginInvoke(new Action(() =>
            {
                if (isPLCConnected == true)
                {
                    List<MaxMinValue> maxMinList = this.ValueList;

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

                        if (PLCPointInfoTable.Rows.Count > 0)
                        {
                            for (int i = 0; i < PLCPointInfoTable.Rows.Count; i++)
                            {
                                DataRow dr = maxminTable.NewRow();
                                dr["序号"] = (i + 1);
                                dr["测试项目"] = PLCPointInfoTable.Rows[i]["BoardName"].ToString();
                                dr["标准值"] = (PLCPointInfoTable.Rows[i]["StandardCode"].ToString());
                                dr["上限值"] = (PLCPointInfoTable.Rows[i]["MaxBoardCode"].ToString());
                                dr["下限值"] = (PLCPointInfoTable.Rows[i]["MinBoardCode"].ToString());
                                dr["实际值"] = (PLCPointInfoTable.Rows[i]["BoardCode"].ToString());

                                if (dr["实际值"].Equals("NG") || dr["实际值"].Equals("OK"))
                                {
                                    dr["测试结果"] = dr["实际值"];
                                }
                                else
                                {
                                    dr["测试结果"] = (PLCPointInfoTable.Rows[i]["ResultBoardCode"].ToString());
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
                        if (PLCPointInfoTable.Rows.Count > 0 && dataGridViewDynamic4.Rows.Count > 0)
                        {
                            for (int i = 0; i < dataGridViewDynamic4.Rows.Count; i++)
                            {
                                if (maxMinList != null && maxMinList.Count == dataGridViewDynamic4.Rows.Count && maxMinList.Count > 0)
                                {
                                    if (maxMinList[i].BoardName == PLCPointInfoTable.Rows[i]["BoardName"].ToString())
                                    {
                                        dataGridViewDynamic4.Rows[i].Cells[0].Value = (i + 1);
                                        dataGridViewDynamic4.Rows[i].Cells[1].Value = PLCPointInfoTable.Rows[i]["BoardName"].ToString();
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

                //Console.WriteLine("绑定上下限结束");

            })).AsyncWaitHandle.WaitOne();

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
                // 提取索引标志，点位信息填写模板：D1090:H-3 D1080:I-5
                int index = plcPointInfo.IndexOf(":");
                int index1 = plcPointInfo.IndexOf("-");

                // 提取类型部分，包括：H-short，I-Int32，F-float，J-Int32，N-Int32，O-Int32，S-string
                string dataType = plcPointInfo.Substring(index + 1, 1);

                // 提取数值部分
                ushort number = 0;
                ushort.TryParse(plcPointInfo.Substring(index1 + 1), out number);

                // 提取PLC地址
                string plcAddress = plcPointInfo.Substring(0, index);

                if (dataType == "H")
                {
                    string aa = "";
                    OperateResult<short> result = KeyenceMcNet.ReadInt16(plcAddress);

                    if (result.IsSuccess)
                    {
                        aa = TypeRead.NumericOperate(result.Content.ToString(), number.ToString());
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
                    OperateResult<int> result = KeyenceMcNet.ReadInt32(plcAddress);

                    if (result.IsSuccess)
                    {
                        aa = TypeRead.NumericOperate(result.Content.ToString(), number.ToString());
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
                    OperateResult<float> result = KeyenceMcNet.ReadFloat(plcAddress);

                    if (result.IsSuccess)
                    {
                        aa = TypeRead.NumericOperate(result.Content.ToString(), number.ToString());
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
                    OperateResult<int> result = KeyenceMcNet.ReadInt32(plcAddress);

                    if (result.IsSuccess)
                    {

                        aa = CodeNum.DivBy100Rounded2(result.Content.ToString());
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
                    OperateResult<int> result = KeyenceMcNet.ReadInt32(plcAddress);

                    if (result.IsSuccess)
                    {
                        aa = CodeNum.ConvertToOkNg(result.Content.ToString());
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
                    OperateResult<int> result = KeyenceMcNet.ReadInt32(plcAddress);

                    if (result.IsSuccess)
                    {
                        aa = result.Content.ToString();
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
                    OperateResult<string> result = KeyenceMcNet.ReadString(plcAddress, number);

                    if (result.IsSuccess)
                    {
                        ss = CodeNum.CleanString(result.Content.ToString());
                    }
                    else
                    {
                        ss = "null";
                    }
                    outputStr = ss;

                }   // string

                else
                {
                    var ss = KeyenceMcNet.ReadString(plcAddress, number).Content;
                    outputStr = ss;
                }                        // string
            }

            else
            {
                string aa = "";
                OperateResult<int> readss = KeyenceMcNet.ReadInt32(plcPointInfo);
                if (readss.IsSuccess)
                {

                    aa = CodeNum.DivBy100(readss.Content.ToString());
                }
                else
                {
                    aa = "null";
                }
                outputStr = aa;
            }

            return outputStr;
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

        #endregion

        #region  ------------ 读取生产指标等信息 ------------

        List<string> kpiList = null;

        private CancellationTokenSource _cts;
        private readonly object _lockObject = new object();

        private async Task InitializeModelReadAsync()
        {
            // 获取用户输入
            deviceInfo.DeviceStatusPoint = txtDeviceStatePoint.Text;
            deviceInfo.ProductModelPoint = txtProductModelPoint.Text;
            deviceInfo.ProductModelLength = txtPMLength.Text;
            deviceInfo.Save();

            _cts = new CancellationTokenSource();

            try
            {
                await Task.Run(async () =>
                {
                    while (!_cts.Token.IsCancellationRequested)
                    {
                        await ReadModelAsync_PLC(_cts.Token);
                        await Task.Delay(250, _cts.Token);
                    }
                }, _cts.Token);
            }
            catch (OperationCanceledException)
            {
                // 任务被取消，正常退出
            }
            catch (Exception ex)
            {
                await ShowErrorMessageAsync($"读取 PLC 数据时发生错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 读取设备运行状态、产品型号、生产指标、更新UI
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task ReadModelAsync_PLC(CancellationToken token)
        {
            if (!isPLCConnected) return;

            try
            {
                // 读取设备运行状态 D1007
                deviceState = await ReadPlcValueAsync(deviceInfo.DeviceStatusPoint, token);

                // 读取产品型号 D1120
                ushort productModelLength = ushort.TryParse(deviceInfo.ProductModelLength, out var length) ? length : (ushort)10;
                string pModel = await ReadPlcStringAsync(deviceInfo.ProductModelPoint, productModelLength, token);
                productModel = CodeNum.CleanString(pModel);

                // 读取其他 KPI 数据
                kpiList = await ReadKpiDataAsync(token);

                // 更新 UI
                await UpdateUIAsync(token);
            }
            catch (OperationCanceledException)
            {
                // 任务被取消，正常退出
            }
            catch (Exception ex)
            {
                await ShowErrorMessageAsync($"读取 PLC 数据时发生错误: {ex.Message}");
            }
        }

        /// <summary>
        /// ReadInt32
        /// </summary>
        private async Task<string> ReadPlcValueAsync(string address, CancellationToken token)
        {
            return await Task.Run(() => KeyenceMcNet.ReadInt32(address).Content.ToString(), token);
        }

        private async Task<string> ReadPlcStringAsync(string address, ushort length, CancellationToken token)
        {
            return await Task.Run(() => KeyenceMcNet.ReadString(address, length).Content, token);
        }

        /// <summary>
        /// 根据Kpi点位集合，读取相应点位的数据
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<List<string>> ReadKpiDataAsync(CancellationToken token)
        {
            var kpiList = new List<string>();

            if (kpisPointSets.Length > 0)
            {
                foreach (var point in kpisPointSets)
                {
                    if (token.IsCancellationRequested) break;

                    if (point.Contains("-"))
                    {
                        kpiList.Add(await ProcessPointDataAsync(point, token));
                    }
                    else
                    {
                        int index = point.IndexOf(":");
                        string dataType = point.Substring(index + 1, 1);
                        string plcAddress = point.Substring(0, index);
                        // 读取PLC点位数据
                        string rawData = await ReadPlcValueAsync(plcAddress, token);
                        // 读取过来的数据按照dataType进行处理
                        kpiList.Add(CodeNum.HandlePlcData(rawData, dataType));
                    }
                }
            }

            return kpiList;
        }

        private Task<string> ProcessPointDataAsync(string pointSet, CancellationToken token)
        {
            return Task.Run(() => ProcessPointData_PLC(pointSet), token);
        }

        private async Task UpdateUIAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested) return;

            await this.InvokeAsync(() =>
            {
                // 更新设备状态
                UpdateDeviceStatus();

                // 读取并更新产品型号
                txtProductModel.Text = productModel;

                // 网络打印
                if (chkLoadModel_TCP.Checked)
                {
                    txtPModel_TCP.Text = txtProductModel.Text;
                }

                // 驱动打印
                if (chkLoadModel_COM.Checked)
                {
                    txtPModel_COM.Text = txtProductModel.Text;
                }

                // 更新 KPI 数据表格
                UpdateKpiTable();

                // 更新其他 UI 元素
                UpdateOtherUIElements();
            });
        }

        /// <summary>
        /// 更新设备状态
        /// </summary>
        private void UpdateDeviceStatus()
        {
            if (deviceState != null)
            {
                switch (deviceState)
                {
                    case "1":   // 运行
                        lblDeviceStatus.ForeColor = Color.Green;
                        EndFaults();
                        break;
                    case "2":   // 故障不停机
                        lblDeviceStatus.ForeColor = Color.Red;
                        SendFaultInfo("触发故障");
                        break;
                    case "3":   // 故障停机
                        lblDeviceStatus.ForeColor = Color.Red;
                        SendFaultInfo("故障停机");
                        break;
                    case "4":   // 待机
                        lblDeviceStatus.ForeColor = Color.Orange;
                        EndFaults();
                        break;
                    default:
                        lblDeviceStatus.ForeColor = Color.Black;
                        break;
                }
            }
        }

        private void UpdateKpiTable()
        {
            if (kpisNameSets.Length == kpisPointSets.Length && kpisPointSets.Length > 0)
            {
                if (KPIsTable == null)
                {
                    InitializeKpiTable();
                }
                else
                {
                    UpdateExistingKpiTable();
                }
            }
        }

        private void InitializeKpiTable()
        {
            KPIsTable = new DataTable();
            KPIsTable.Columns.Add("名称", typeof(string));
            KPIsTable.Columns.Add("值", typeof(string));

            for (int i = 0; i < kpisNameSets.Length; i++)
            {
                DataRow dr = KPIsTable.NewRow();
                dr["名称"] = kpisNameSets[i];
                dr["值"] = kpiList[i];
                KPIsTable.Rows.Add(dr);
            }

            dataGridViewDynamic3.DataSource = KPIsTable;

            for (int i = 0; i < dataGridViewDynamic3.Columns.Count; i++)
            {
                dataGridViewDynamic3.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void UpdateExistingKpiTable()
        {
            for (int i = 0; i < kpisNameSets.Length; i++)
            {
                dataGridViewDynamic3.Rows[i].Cells[0].Value = kpisNameSets[i];
                dataGridViewDynamic3.Rows[i].Cells[1].Value = kpiList[i];
            }
        }

        private void UpdateOtherUIElements()
        {
            if (chkReadRecipeId_PLC.Checked)
            {
                UpdateRecipeInfo();
            }
        }

        /// <summary>
        /// 更新配方号
        /// </summary>
        /// <returns></returns>
        private async Task UpdateRecipeInfo()
        {
            string currentId = await ReadPlcValueAsync(deviceInfo.RecipeIdPoint, CancellationToken.None);
            if (recipeId != currentId)
            {
                recipeId = currentId;
                cboBarcodeRuleAndFixtures.SelectedValue = currentId;
                lblRecipeId.Text = currentId;

                UpdateFixtureInfo();
            }
        }

        private void UpdateFixtureInfo()
        {
            string[] expectedFixtures = CodeNum.ExtractFixturesInfo(cboBarcodeRuleAndFixtures.Text);
            string[] frockB = txtFixtureBinding.Text.ToString().Split('+');

            if (!CodeNum.CompareArray(expectedFixtures, frockB))
            {
                string frockmm = string.Join(" + ", frockB.Where(frockid => expectedFixtures.Contains(frockid)));
                txtFixtureBinding.Text = frockmm;

                string[] frockC = frockmm.Split('+');

                if (!CodeNum.CompareArray(expectedFixtures, frockC))
                {
                    //lblOperatePrompt.Text = resources.GetString("Wait_scan_Jig");   
                    lblOperatePrompt.Text = resources.GetString("WaittingSacnFixtures");    // 等待扫工装
                }
            }
        }

        private Task ShowErrorMessageAsync(string message)
        {
            return this.InvokeAsync(() => MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error));
        }

        /// <summary>
        /// 触发发送设备故障信息
        /// </summary>
        /// <param name="status"></param>
        public void SendFaultInfo(string status)
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
                                        string statssName = CodeNum.GetStationNameByID(faultsTable.Rows[i]["工位ID"].ToString(), stationNameSets); //
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
        public void EndFaults()
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

        #endregion

        #region ------------ 显示列表 ------------

        private int Num = 0;

        /// <summary>
        /// 显示列表表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertTable(object sender, EventArgs e)
        {
            //dataGridViewDynamic2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridViewDynamic2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            int totalValidItems = 0;
            totalValidItems = CodeNum.GetValidItems(maxValuePoint) + CodeNum.GetValidItems(minValuePoint) + CodeNum.GetValidItems(testResultPoint);

            // 固定的基本信息列
            for (int i = 0; i < 7; i++)
            {
                dataGridViewDynamic2.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridViewDynamic2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            }
            dataGridViewDynamic2.Columns[0].HeaderText = resources.GetString("d2No");       // 序号
            dataGridViewDynamic2.Columns[1].HeaderText = resources.GetString("d2CPTM");     // 产品条码
            dataGridViewDynamic2.Columns[2].HeaderText = resources.GetString("d2CPJG");     // 产品结果
            dataGridViewDynamic2.Columns[3].HeaderText = resources.GetString("d2CPBH");     // 产品编号
            dataGridViewDynamic2.Columns[4].HeaderText = resources.GetString("d2CZY");      // 操作员
            dataGridViewDynamic2.Columns[5].HeaderText = resources.GetString("d2SCZT");     // 上传状态
            dataGridViewDynamic2.Columns[6].HeaderText = resources.GetString("d2CSSJ");     // 测试时间

            // 动态配置测试项相关的列
            for (int i = 7; i < ((testItemsName.Length + totalValidItems) + 7); i++)
            {
                dataGridViewDynamic2.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridViewDynamic2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            int a = 7;
            if (actualValuePoint.Length > 0)
            {
                for (int i = 0; i < testItemsName.Length; i++)
                {
                    dataGridViewDynamic2.Columns[a].Width = CodeNum.ParseIntOrDefault(txtDisplayWidth.Text);
                    dataGridViewDynamic2.Columns[a].HeaderText = testItemsName[i] + unitName[i];
                    a = a + 1;
                    if (!maxValuePoint[i].Equals("NO"))
                    {
                        dataGridViewDynamic2.Columns[a].Width = CodeNum.ParseIntOrDefault(txtDisplayWidth.Text);
                        dataGridViewDynamic2.Columns[a].HeaderText = testItemsName[i] + "上限" + unitName[i];
                        a = a + 1;
                    }
                    if (!minValuePoint[i].Equals("NO"))
                    {
                        dataGridViewDynamic2.Columns[a].Width = CodeNum.ParseIntOrDefault(txtDisplayWidth.Text);
                        dataGridViewDynamic2.Columns[a].HeaderText = testItemsName[i] + "下限" + unitName[i];
                        a = a + 1;
                    }
                    if (!testResultPoint[i].Equals("NO"))
                    {
                        dataGridViewDynamic2.Columns[a].Width = CodeNum.ParseIntOrDefault(txtDisplayWidth.Text);
                        dataGridViewDynamic2.Columns[a].HeaderText = testItemsName[i] + "结果";
                        a = a + 1;
                    }
                }
            }

            /*dataGridViewDynamic2.Columns[3].HeaderText = "卷簧扭力";
            dataGridViewDynamic2.Columns[4].HeaderText = "压力";
            dataGridViewDynamic2.Columns[5].HeaderText = "行程";
            dataGridViewDynamic2.Columns[8].HeaderText = "软件版本";
            dataGridViewDynamic2.Columns[9].HeaderText = "文件版本";
            dataGridViewDynamic2.Columns[10].HeaderText = "产品编码";
            dataGridViewDynamic2.Columns[11].HeaderText = "产品名称";
            dataGridViewDynamic2.FirstDisplayedScrollingRowIndex = dataGridViewDynamic2.Rows.Count - 1;*/

        }

        private void ShowResult()
        {
            Invoke(new Action(() =>
            {
                if (dataGridViewDynamic2.RowCount > 5000)
                {
                    this.dataGridViewDynamic2.Rows.Clear();
                }

                var uploadState = "失败";
                if (MES_Result[2006] == "1")
                {
                    uploadState = "成功";
                }
                else if (isOffLine == 1)
                {
                    uploadState = "本地";
                }

                DateTime now = DateTime.Now;
                //添加行
                DataGridViewRow dgvRow = new DataGridViewRow();
                dgvRow.CreateCells(this.dataGridViewDynamic2);
                dgvRow.Cells[0].Value = Num;                   // 序号
                dgvRow.Cells[1].Value = barcodeInfo;           // 条码
                dgvRow.Cells[2].Value = Value[9999];           // 产品结果
                dgvRow.Cells[3].Value = txtProductModel.Text;  // 产品型号 = 产品名称 = 产品编号
                dgvRow.Cells[4].Value = LoginUser.ToString();  // 操作员
                dgvRow.Cells[5].Value = uploadState;           // 上传状态
                dgvRow.Cells[6].Value = now.ToString("MM-dd HH:mm:ss");    // 测试时间

                int a = 7;
                if (actualValueList.Count > 0)
                {
                    for (int i = 0; i < actualValueList.Count; i++)
                    {
                        dgvRow.Cells[a].Value = actualValueList[i];
                        a = a + 1;

                        if (maxList.Count > i)
                        {
                            if (maxValuePoint[i] != "NO")
                            {
                                dgvRow.Cells[a].Value = maxList[i];
                                a = a + 1;
                            }
                        }

                        if (minList.Count > i)
                        {
                            if (minValuePoint[i] != "NO")
                            {
                                dgvRow.Cells[a].Value = minList[i];
                                a = a + 1;
                            }
                        }

                        if (resultList.Count > i)
                        {
                            if (testResultPoint[i] != "NO")
                            {
                                dgvRow.Cells[a].Value = resultList[i];
                                a = a + 1;
                            }
                        }
                    }
                }
                if (Value[9999] == "NG")
                {
                    dgvRow.DefaultCellStyle.BackColor = Color.Red;
                }

                this.dataGridViewDynamic2.Rows.Insert(0, dgvRow);
            }));
        }

        #endregion

        #region ------------ 保存历史数据到本地 ------------

        // 在您的表单类中使用
        private DatabaseOperations dbOps;

        /// <summary>
        /// 保存生产历史数据到本地
        /// </summary>
        private async void SaveProductionDataAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    //DisplayMessage("生产数据本地保存中...");

                    InitializeDatabaseOperations();
                    SaveHistoricalData($@"{lblDataPath.Text}\{DateTime.Now:Y}生产数据.mdb");

                    //DisplayMessage("生产数据本地保存完成");
                }
                catch (Exception ex)
                {
                }
            });
        }

        private void InitializeDatabaseOperations()
        {
            dbOps = new DatabaseOperations(
                // 假设这是在一个 Form 或 UserControl 中
                txtProductModel.Text,
                txtWorkOrder.Text,
                txtFixtureBinding.Text,
                cboBarcodeRuleAndFixtures.Text,
                LoginUser.ToString(),
                testItemsName,
                maxValuePoint,
                minValuePoint,
                testResultPoint
            );
        }

        /// <summary>
        /// 保存历史数据
        /// </summary>
        private async Task SaveHistoricalData(string conn)
        {
            await dbOps.TestAsync(conn, barcodeInfo, Value, codesTable, actualValueList, maxList, minList, resultList);
        }

        #endregion

        #region ------------ 历史数据页面 ------------

        #region ------------ 数据查询 ------------

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchHistoricalData();

            if (pager != null)
            {
                pager.fenye();  // 分页
                PageLoad();     // 显示分页数据
                //dataset();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        public void SearchHistoricalData()
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
            dbHelper = new MDBHelper(dataBaseUrl);

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

            DataTable table = dbHelper.Find(sql.ToString());
            //tablelist_date(dataBaseUrl);
            //dataGridViewDynamic1.SetDataTable(table);
            // dataGridViewDynamic1.DataSource = table;
            pager = new Pager(table);//分页
            dbHelper.CloseConnection();
        }

        public void tablelist_date(string url)
        {
            dbHelper = new MDBHelper(url);
            var sql1 = "select * from [Sheet1]";
            DataTable dt = dbHelper.Find(sql1);
            if (dt.Columns.Count > 0)
            {

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dataGridViewDynamic1.AddHeader(dt.Columns[i].ColumnName + "\t");
                }
            }
            dbHelper.CloseConnection();

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

        #endregion

        /// <summary>
        /// 导出生产历史数据
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
            dbHelper = new MDBHelper(dataBaseUrl);
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

                DataTable table = dbHelper.Find(sql.ToString());

                NPOIHelper.DataTableToExcel(table, txtPath);
            }
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 变更产线数据存放路径
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
        /// 初始化加载目录树
        /// </summary>
        private void InitializeDirectoryTree()
        {
            BtnRefreshDirectory_Click(null, null);
        }

        /// <summary>
        /// 刷新目录
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
        /// 遍历生产历史数据目录
        /// </summary>
        void FindDirectory(string nowDirectory, TreeNode parentNode)
        {
            try  // 当文件目录不可访问时，需要捕获异常
            {
                // 获取当前目录下的所有文件夹数组
                string[] directoryArray = Directory.GetFiles(nowDirectory, "*.mdb");
                if (directoryArray.Length > 0)
                {
                    foreach (string item in directoryArray)
                    {
                        // 遍历数组，将节点添加到父亲节点的
                        string str = Path.GetFileNameWithoutExtension(item);
                        TreeNode node = new TreeNode(str);
                        parentNode.Nodes.Add(node);
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

        /// <summary>
        /// 重置条码计数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetBarcodeCount_Click(object sender, EventArgs e)
        {
            sytemSetDerivedsd.TotalBarcodeCount = 0;
            sytemSetDerivedsd.BarcodeOKCount = 0;
            sytemSetDerivedsd.BarcodeNGCount = 0;
            sytemSetDerivedsd.Save();

            lblTotalBarcodesCount.Text = sytemSetDerivedsd.TotalBarcodeCount.ToString();
            lblBarcodeOKCount.Text = sytemSetDerivedsd.BarcodeOKCount.ToString();
            lblBarcodeNGCount.Text = sytemSetDerivedsd.BarcodeNGCount.ToString();
        }

        #endregion

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
            dbHelper = new MDBHelper(userFileuRL);
            DataTable table1 = dbHelper.Find("select * from Users where 工号 = '" + UID.Text + "'");
            if (table1.Rows.Count > 0)
            {
                //if (comboBox3.SelectedIndex == 1)
                //{
                if (tbxBrandID.Text.Length > 0)
                {
                    DataTable table = dbHelper.Find("select * from Users where [厂牌UID] = '" + tbxBrandID.Text + "' and [厂牌UID] <> '' and [工号] <> '" + UID.Text + "' ");
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

                var result = dbHelper.Change(sql);
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
                    DataTable table = dbHelper.Find("select * from Users where [厂牌UID] = '" + tbxBrandID.Text + "' and [厂牌UID] <> '' ");
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


                bool result = dbHelper.Add(sql.ToString());
                if (result == true)
                {
                    MessageBox.Show("新增成功");
                }

            }
            btnRefreshUser_Click(null, null);
            dbHelper.CloseConnection();

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
            dbHelper = new MDBHelper(userFileuRL);

            // 删除前先执行查找
            string selectSql = $"SELECT [用户名], [用户权限] FROM [Users] WHERE [工号] = '{id}'";
            DataTable userInfo = dbHelper.Find(selectSql);

            string username = "";
            string permissions = "";
            if (userInfo.Rows.Count > 0)
            {
                username = userInfo.Rows[0]["用户名"].ToString();
                permissions = userInfo.Rows[0]["用户权限"].ToString();
            }

            string sql = $" DELETE FROM [Users] WHERE [工号] = '{id}' ";
            bool bl = dbHelper.Del(sql);
            if (bl == true)
            {
                MessageBox.Show("删除成功");
                // 记录日志
                string delInfo = $"【用户删除】\n工号：{id} | 姓名：{username} | 权限：{permissions}";
                loggerAccount.Trace(delInfo);
            }
            btnRefreshUser_Click(null, null);
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshUser_Click(object sender, EventArgs e)
        {
            dbHelper = new MDBHelper(userFileuRL);
            userCollection = dbHelper.Find("select 用户名,用户密码,用户权限,厂牌UID,工号 from Users where 用户权限 <> 'DEV'");
            userInfoEntities = DataConverter.ConvertDataTableToList(userCollection);
            dataGridView1.DataSource = userCollection;
            dbHelper.CloseConnection();
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
            dbHelper = new MDBHelper(userFileuRL);
            DataTable table = dbHelper.Find("select 登录次数 from Users where [工号] = '" + LoginUser + "'");
            if (table.Rows.Count > 0)
            {
                int i = 0;
                if (!string.IsNullOrWhiteSpace(table.Rows[0][0].ToString()))
                {
                    i = int.Parse(table.Rows[0][0].ToString());
                }
                string sql = "update [Users] set [最后登录时间]='" + DateTime.Now.ToString() + "',[登录次数]='" + (i + 1) + "' where [工号] = '" + LoginUser + "'";

                var result = dbHelper.Change(sql);
                //if (result == true)
                //{
                //    MessageBox.Show("修改成功");
                //}
            }
            dbHelper.CloseConnection();
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

        #region ------------ 配方号的读取与发送 ------------

        /// <summary>
        /// 配方操作保存按钮
        /// </summary>
        private void btnSaveRecipeInfo_Click(object sender, EventArgs e)
        {
            dbHelper = new MDBHelper(path4);
            DataTable SytemSetTable = dbHelper.Find("SELECT * FROM [SytemSet] WHERE ID = '1'");

            if (SytemSetTable.Rows.Count > 0)
            {
                string currentRecipeID = "0";
                if (!string.IsNullOrWhiteSpace(cboBarcodeRuleAndFixtures.Text))
                {
                    currentRecipeID = cboBarcodeRuleAndFixtures.SelectedValue.ToString();
                }

                // 保存配方号，以及是否从PLC读取配方号
                string sql = $"update [SytemSet] set [faults]='{currentRecipeID}', Workstname ='{chkReadRecipeId_PLC.Checked}' where [ID] = '1'";

                var isUpdateSuccessfully = dbHelper.Change(sql);
                if (isUpdateSuccessfully)
                {
                    lblRecipeId.Text = currentRecipeID;
                    MessageBox.Show("保存成功");
                    // 记录操作日志
                    bool isChecked = chkReadRecipeId_PLC.Checked;
                    string readPlcStatus = isChecked ? "是" : "否";
                    string msgSave = $"【配方操作保存成功】\n是否读取PLC：{readPlcStatus}\n当前配方号：{lblRecipeId.Text}";
                    loggerConfig.Trace(msgSave);
                }
            }

            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 发给配方号给PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendRecipeInfo_Click(object sender, EventArgs e)
        {
            if (chkReadRecipeId_PLC.Checked)
            {
                MessageBox.Show("读取PLC中，不能发送！");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(cboBarcodeRuleAndFixtures.Text))
                {
                    MessageBox.Show("请选择条码验证规则！");
                    return;
                }
                try
                {
                    // D1204：允许修改标志
                    KeyenceMcNet.Write(deviceInfo.ModifyRecipePoint, 1);
                    // D1206：修改配方号
                    KeyenceMcNet.Write(deviceInfo.ModifyRecipeIDPoint, int.Parse(cboBarcodeRuleAndFixtures.SelectedValue.ToString()));
                    MessageBox.Show("发送PLC成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("发送PLC失败！");
                }
            }
        }

        /// <summary>
        /// 变更工单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeWorkOrder_Click(object sender, EventArgs e)
        {
            dbHelper = new MDBHelper(path4);
            DataTable table1 = dbHelper.Find("select * from SytemSet where ID = '1'");
            if (table1.Rows.Count > 0)
            {
                string sql = "update [SytemSet] set [wordNo]='" + txtWorkOrder.Text + "' where [ID] = '1'";
                var result = dbHelper.Change(sql);
                if (result == true)
                {
                    MessageBox.Show("变更成功");
                }
            }
            dbHelper.CloseConnection();
        }

        #endregion

        #region ------------ 分页 ------------

        Pager pager = null;

        /// <summary>
        /// 将分页面数据显示出来
        /// </summary>
        private void PageLoad()
        {
            dataGridViewDynamic1.DataSource = pager.LoadPage();//显示数据
            this.textBox13.Text = pager.pageSize.ToString();//每页的条
            this.label34.Text = pager.currentPage + "/" + pager.pageCount;//当前页0/0
            this.label36.Text = pager.recordCount.ToString();//  共条
        }

        /// <summary>
        /// 首页
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            if (pager != null)
            {
                pager.currentPage = 1;
                PageLoad();//显示分页数据
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click_1(object sender, EventArgs e)
        {
            if (pager != null)
            {
                pager.currentPage--;
                PageLoad();//显示分页数据
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click_1(object sender, EventArgs e)
        {
            if (pager != null)
            {
                pager.currentPage++;
                PageLoad();//显示分页数据
            }
        }

        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            if (pager != null)
            {
                pager.currentPage = pager.pageCount;
                PageLoad();//显示分页数据
            }
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 重新分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private Action<string> ShowMsgAction;
        private bool isDashboardConnected = false;  // 看板连接状态
        DataTable vulnerableTable;                  // 易损件信息表 vulnerable part
        DataTable faultsTable;                      // 故障信息表

        /// <summary>
        /// 加载看板设置页面相关参数
        /// </summary>
        private void LoadDashboardConfig()
        {
            BtnRefreshAtDashboardSetting(null, null);

            // 遍历SyemSocket表格，从数据库加载看板(Dashboard)相关的配置
            dbHelper = new MDBHelper(path4);
            DataTable table1 = dbHelper.Find("SELECT * FROM SytemSocket WHERE ID = 1");
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                for (int j = 0; j < table1.Columns.Count; j++)
                {
                    string isEnableDashboard = table1.Rows[i]["Devicestatu"].ToString();    // 是否启用看板
                    if (isEnableDashboard == "True") chkEnableDashboard.Checked = true;
                    string isReadFromPLC = table1.Rows[i]["BoardPosition"].ToString();      // 读取PLC
                    if (isReadFromPLC == "True") chkReadPName.Checked = true;

                    txtDashboardIP.Text = table1.Rows[i]["IP"].ToString();                  // 看板IP
                    txtDashboardPort.Text = table1.Rows[i]["Port"].ToString();              // 看板端口
                    txtProductName.Text = table1.Rows[i]["BoardTheory"].ToString();         // 成品名称
                    txtStationNameSets.Text = table1.Rows[i]["BoardName"].ToString();       // 工位名称集合
                    txtFaultStartPoint.Text = table1.Rows[i]["FaultCode"].ToString();       // 故障起始点位
                    txtFaultLength.Text = table1.Rows[i]["FaultLeng"].ToString();           // 故障长度
                }
            }
            dbHelper.CloseConnection();

            #region 初始化故障信息表
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "操作";
            buttonColumn.Text = "保存";
            buttonColumn.Name = "btnCol";
            buttonColumn.DefaultCellStyle.NullValue = "保存";
            dgvWeakInfo.Columns.Add(buttonColumn);

            DataGridViewButtonColumn anotherButtonColumn = new DataGridViewButtonColumn();
            anotherButtonColumn.HeaderText = "操作";
            anotherButtonColumn.Name = "btnCol2";
            anotherButtonColumn.DefaultCellStyle.NullValue = "删除";
            dgvWeakInfo.Columns.Add(anotherButtonColumn);

            DataGridViewButtonColumn butnCo = new DataGridViewButtonColumn();
            butnCo.HeaderText = "操作";
            butnCo.Text = "保存";
            butnCo.Name = "btnCol";
            butnCo.DefaultCellStyle.NullValue = "保存";
            dgvFaultInfo.Columns.Add(butnCo);

            DataGridViewButtonColumn anotrButCo = new DataGridViewButtonColumn();
            anotrButCo.HeaderText = "操作";
            anotrButCo.Name = "btnCol2";
            anotrButCo.DefaultCellStyle.NullValue = "删除";
            dgvFaultInfo.Columns.Add(anotrButCo);
            #endregion
        }

        /// <summary>
        /// 刷新易损件表和故障信息表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshAtDashboardSetting(object sender, EventArgs e)
        {
            dbHelper = new MDBHelper(path4);

            string selectSql = @" SELECT [ID] AS [编号],
                                     [WorkID] AS [工位ID],
                              [BoardPosition] AS [易损件所在的位置], 
                                  [BoardName] AS [易损件的名称],
                                [BoardTheory] AS [理论使用次数PLC点位],
                                  [BoardCode] AS [已经使用的PLC点位]
                                    FROM [VulnbleParts]";
            vulnerableTable = dbHelper.Find(selectSql);

            vulnerableTable.Columns["编号"].AutoIncrement = true;
            vulnerableTable.Columns["编号"].ReadOnly = true;
            vulnerableTable.Columns["编号"].AutoIncrementSeed = 0;

            int rowMax = 0;
            if (vulnerableTable.Rows.Count > 0)
            {
                rowMax = vulnerableTable.AsEnumerable().Max(row => row.Field<int>("编号"));
            }

            DataRow newRow = vulnerableTable.NewRow();
            newRow["编号"] = rowMax;

            // 刷新易损件数据
            dgvWeakInfo.DataSource = vulnerableTable;

            // 刷新故障信息
            faultsTable = dbHelper.Find("SELECT ID AS 编号, [WorkID] AS 工位ID, [CodeID] AS 故障点位, Faults AS 故障描述 from SytemFaults");
            dgvFaultInfo.DataSource = faultsTable;
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 易损件1.删除2.保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

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

                dbHelper = new MDBHelper(path4);
                DataTable table1 = dbHelper.Find("select * from VulnbleParts where [ID] = " + pid);

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

                    var result = dbHelper.Change(sql);
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
                    bool result = dbHelper.Add(sql.ToString());
                    if (result == true)
                    {
                        MessageBox.Show("新增成功");
                        string insertInfo = $"【易损件数据新增成功】\n新增详情：\n" +
                            $"编号：{pid} | 工位序号：{workid} | 易损件所在的位置：{bdPosition} | 易损件的名称：{bdName} | " +
                            $"理论使用次数PLC点位：{bdTheory} | 已经使用的PLC点位：{bdCode}";
                        loggerConfig.Trace(insertInfo);
                    }
                }

                dbHelper.CloseConnection();
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
            if (e.RowIndex == -1) return;

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

                dbHelper = new MDBHelper(path4);
                DataTable table1 = dbHelper.Find("select * from SytemFaults where [ID] = '" + pid + "'");

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

                    var result = dbHelper.Change(sql);
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

                    bool result = dbHelper.Add(sql.ToString());
                    if (result == true)
                    {
                        MessageBox.Show("新增成功");
                        string insertInfo = $"【故障信息新增成功】\n新增详情：\n" +
                            $"编号：{pid} | 工位序号：{workID} | " +
                            $"故障点位：{codeID} | 故障描述：{funmae}";
                        loggerConfig.Trace(insertInfo);
                    }
                }

                dbHelper.CloseConnection();
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

            dbHelper = new MDBHelper(path4);
            DataTable table1 = dbHelper.Find("select * from SytemSocket where ID = 1");
            if (table1.Rows.Count > 0)
            {
                string sql = $"update [SytemSocket] set [IP]='{txtDashboardIP.Text}', [Port]='{txtDashboardPort.Text}', " +
                    $"[Devicestatu]='{chkEnableDashboard.Checked}', [BoardName]='{txtStationNameSets.Text}', " +
                    $"[BoardTheory]='{txtProductName.Text}', [BoardPosition]='{chkReadPName.Checked}', " +
                    $"[FaultCode]='{txtFaultStartPoint.Text}',[FaultLeng]='{txtFaultLength.Text}' where [ID] = 1";

                var result = dbHelper.Change(sql);
                if (result == true)
                {
                    MessageBox.Show("保存成功");
                }
            }
            dbHelper.CloseConnection();
        }

        #endregion

        #region ------------ 看板Socket连接服务器 ------------

        private System.Net.Sockets.Socket socket;

        private void ConnectDashboard()
        {
            ConnectDashboard_Click(null, null);
        }

        /// <summary>
        /// 连接看板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectDashboard_Click(object sender, EventArgs e)
        {
            if (!chkEnableDashboard.Checked)
            {
                isDashboardConnected = false;
                lblDashboardStatus.ForeColor = Color.Black;
                return;
            }

            ConnectToServer();
        }

        /// <summary>
        /// Socket连接
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public async void ConnectToServer()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        // Internet 协议、字节流、IPv4连接
                        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                        System.Net.IPAddress serverIP = System.Net.IPAddress.Parse(txtDashboardIP.Text);
                        IPEndPoint serverEndPoint = new IPEndPoint(serverIP, Convert.ToInt32(txtDashboardPort.Text));

                        socket.Connect(serverEndPoint);
                        isDashboardConnected = true;

                        if (isDashboardConnected)
                        {
                            // 读取消息
                            System.Threading.Thread thread = new System.Threading.Thread(Received);
                            thread.IsBackground = true;
                            thread.Start();

                            // 发送心跳包
                            Task.Run(async () => SendHeartbeatAsync());

                            // 发送机台名称和工位名称集合
                            Task.Run(async () => SendDeviceAndStationName());

                            Invoke(new Action(() =>
                            {
                                lblDashboardStatus.ForeColor = Color.Green;

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
                    string receivedMsg = Encoding.UTF8.GetString(buffer, 0, len);
                    ReceiveData(receivedMsg);
                    this.BeginInvoke(ShowMsgAction, socket.RemoteEndPoint + ":" + receivedMsg);
                    ShowMsg(socket.RemoteEndPoint + ":" + receivedMsg);
                }
                catch (Exception ex)
                {
                    isDashboardConnected = false;
                    Console.WriteLine(ex.Message);
                    socket.Close();
                    ConnectToServer();
                }
            }
        }

        /// <summary>
        /// 定时发送心跳包
        /// </summary>
        private async void SendHeartbeatAsync()
        {
            await Task.Run(async () =>
            {
                // 定时发送心跳包并检测连接状态
                while (isDashboardConnected)
                {
                    try
                    {
                        // 发送心跳包
                        byte[] heartbeatData = System.Text.Encoding.UTF8.GetBytes("heartbeat");
                        socket.Send(heartbeatData);
                        await Task.Delay(100000);
                    }
                    catch (Exception ex)
                    {
                        isDashboardConnected = false;
                        Console.WriteLine(ex.Message);
                        socket.Close();

                        ConnectToServer();
                    }
                }
            });
        }

        /// <summary>
        /// 发送机台名称和工位名称
        /// <para>
        /// 0+工位名称
        /// </para>
        /// <para>
        /// 5+机台名称
        /// </para>
        /// </summary>
        public async void SendDeviceAndStationName()
        {
            foreach (var name in stationNameSets)
            {
                Send("0+" + name);
                await Task.Delay(50);
            }

            await Task.Delay(200); // 异步延迟
            Send("5+" + txtDeviceName.Text);
        }
        /*public void SendDeviceAndStationName()
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
        }*/

        /// <summary>
        /// 生成产线信息
        /// <para>
        /// 生产信息1：
        /// 2 + 工位名称 + 当前工单号 + 产品条码 + 操作人员 + 测试时间 + 测试结果 + 生产节拍 + 测试项名称 + 测试项上限 + 测试项下限 + 测式项实际值;
        /// </para>
        /// <para>
        /// 生产信息2：
        /// 2 + 工位名称 + 当前工单号 + 产品条码 + 操作人员 + 测试时间 + 测试结果 + 生产节拍 + 工装编号{i+1} +   +   + {};
        /// </para>
        /// <para>
        /// 生产信息3：
        /// 2 + 工位名称 + 当前工单号 + 产品条码 + 操作人员 + 测试时间 + 测试结果 + 生产节拍 + 产品物料号{i+1} +   +   + {};
        /// </para>
        /// <para>
        /// 统计信息：
        /// 3 + 机台名称 + 工单号 + 工单数量 + 完成数量 + 完成率 + 合格率 + 整体/生产/整线节拍 + 生产总数 + 工序时间 + 利用时间 + 负荷时间 + 直通率 + 产品型号;
        /// </para>
        /// <para>
        /// 易损件信息：
        /// 4 + 易损件所在工位 + 机台名称 + 易损件所在位置 + 易损件名称 + 易损件理论使用次数 + 易损件已使用次数；
        /// </para>
        /// </summary>
        public void GenerateProductionData()
        {
            Invoke(new Action(() =>
            {
                string productionData = string.Empty;

                // 生成统计信息：
                // 3 + 机台名称 + 工单号 + 工单数量 + 完成数量 + 完成率 + 合格率 + 整体/生产/整线节拍 + 生产总数 + 工序时间 + 利用时间 + 负荷时间 + 直通率 + 产品型号
                string statisticsInfo = $"3+{txtDeviceName.Text}+{txtWorkOrder.Text}+{D1084}+{D1086}+{completeRate}+{passRate}+{D1090}+{D1080}" +
                                        $"+{processTime}+{usingTime}+{loadTime}+{FPY}+{txtProductModel.Text}";

                productionData += statisticsInfo;

                // 生成易损件信息：
                // 4 + 易损件所在工位 + 机台名称 + 易损件所在位置 + 易损件名称 + 易损件理论使用次数 + 易损件已使用次数；
                if (vulnerableTable != null)
                {
                    foreach (DataRow row in vulnerableTable.Rows)
                    {
                        string actualUsage = string.Empty;
                        string stationName = string.Empty;

                        string theoreticalUsageAddress = row["理论使用次数PLC点位"].ToString();
                        string actualUsageAddress = row["已经使用的PLC点位"].ToString();

                        // 理论使用次数
                        var theoreticalUsage = KeyenceMcNet.ReadInt32(theoreticalUsageAddress).Content;
                        // 实际使用次数
                        var actualUsageResult = KeyenceMcNet.ReadInt32(actualUsageAddress);

                        if (actualUsageResult.IsSuccess)
                        {
                            actualUsage = actualUsageResult.Content.ToString();
                            stationName = CodeNum.GetStationNameByID(row["工位ID"].ToString(), this.stationNameSets);

                            // 4+易损件所在工位+机台名称+易损件所在位置+易损件名称+易损件理论使用次数+易损件已使用次数
                            /*string vulnerableInfo = "4+" + stationName + "+" + lblDeviceName.Text + "+" + row["易损件所在的位置"] + "+"
                               + row["易损件的名称"] + "+" + theoreticalUsage + "+" + actualUsage;*/
                            string vulnerableInfo = $"4+{stationName}+{lblDeviceName.Text}+{row["易损件所在的位置"]}" +
                                                     $"+{row["易损件的名称"]}+{theoreticalUsage}+{actualUsage}";

                            productionData += "|" + vulnerableInfo;
                            //ShowMsg(vulnerableInfo);
                            //Send(vulnerableInfo);
                        }
                    }
                }

                // 生成生产信息：
                for (int j = 0; j < stationNameSets.Length; j++)
                {
                    // 测试项信息：
                    // 2 + 工位名称 + 当前工单号 + 产品条码 + 操作人员 + 测试时间 + 测试结果 + 生产节拍 + 测试项名称 + 测试项上限 + 测试项下限 + 测式项实际值;
                    string chesAAA = $"2+{stationNameSets[j]}+{txtWorkOrder.Text}+{barcodeInfo}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}+{Value[9999]}+{D1090}+产品名称+ + +{txtProductModel.Text}";
                    productionData += "|" + chesAAA;

                    // 工装信息：
                    // 2 + 工位名称 + 当前工单号 + 产品条码 + 操作人员 + 测试时间 + 测试结果 + 生产节拍 + 工装编号{i+1} +   +   + {};
                    string[] fixturesInfo = txtFixtureBinding.Text.Split('+');
                    for (int i = 0; i < fixturesInfo.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(fixturesInfo[i]))
                        {
                            /*string chesBBB = "2+" + stationNameSets[j] + "+" + txtWorkOrder.Text + "+" + barcodeInfo + "+"
                                            + LoginUser.ToString() + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                                "+" + Value[9999] + "+" + D1090 + "+工装编号" + (i + 1) + "+" + "  " + "+" + "  " + "+" + fixturesInfo[i];*/
                            string chesBBB = $"2+{stationNameSets[j]}+{txtWorkOrder.Text}+{barcodeInfo}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}+{Value[9999]}+{D1090}+工装编号{i + 1}+ + +{fixturesInfo[i]}";
                            productionData += "|" + chesBBB;
                        }
                    }

                    // 物料号信息：
                    // 2 + 工位名称 + 当前工单号 + 产品条码 + 操作人员 + 测试时间 + 测试结果 + 生产节拍 + 产品物料号{i+1} +   +   + {};
                    string[] productCodeInfo = CodeNum.GetProductCodes(cboBarcodeRuleAndFixtures.Text, codesTable);
                    for (int i = 0; i < productCodeInfo.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(productCodeInfo[i]))
                        {
                            /*string chesCCC = "2+" + stationNameSets[j] + "+" + txtWorkOrder.Text + "+" + barcodeInfo + "+"
                                               + LoginUser.ToString() + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                                   "+" + Value[9999] + "+" + D1090 + "+产品物料号" + (i + 1) + "+" + " " + "+" + "  " + "+" + productCodeInfo[i];*/
                            string chesCCC = $"2+{stationNameSets[j]}+{txtWorkOrder.Text}+{barcodeInfo}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}+{Value[9999]}+{D1090}+产品物料号{i + 1}+ + +{productCodeInfo[i]}";

                            productionData += "|" + chesCCC;
                        }
                    }
                }

                if (beatList.Count == actualValueList.Count && maxList.Count == minList.Count &&
                     maxList.Count == actualValueList.Count && stationNameList.Count == actualValueList.Count &&
                  resultList.Count == actualValueList.Count && actualValueList.Count > 0 && testItemsName.Length > 0)
                {
                    for (int i = 0; i < actualValueList.Count; i++)
                    {
                        if (actualValueList[i] != "null")
                        {
                            // 生产信息：
                            if (maxValuePoint[i] == "NO" && minValuePoint[i] == "NO" && testResultPoint[i] == "NO")
                            {
                                /*string cheshixm1 = "2+" + stationNameList[i] + "+" + txtWorkOrder.Text + "+" + barcodeInfo + "+"
                                        + LoginUser.ToString() + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                            "+" + Value[9999] + "+" + D1090 + "+" + testItemsName[i] + "+" + "" + "+" + "" + "+" + actualValueList[i];*/

                                // 2 + 工位名称 + 当前工单号 + 产品条码 + 操作人员 + 测试时间 + 测试结果 + 生产节拍 + 测试项名称 +  +  + 测式项实际值;
                                string cheshixm1 = $"2+{stationNameList[i]}+{txtWorkOrder.Text}+{barcodeInfo}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}" +
                                                  $"+{Value[9999]}+{D1090}+{testItemsName[i]}+ + +{actualValueList[i]}";

                                productionData += "|" + cheshixm1;
                            }
                            else
                            {
                                /*string cheshixm1 = "2+" + stationNameList[i] + "+" + txtWorkOrder.Text + "+" + barcodeInfo + "+"
                                 + LoginUser.ToString() + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                     "+" + Value[9999] + "+" + D1090 + "+" + testItemsName[i] + "+" + maxList[i] + "+" + minList[i] + "+" + actualValueList[i];*/

                                // 2 + 工位名称 + 工单号 + 条码 + 操作人员 + 测试时间 + 测试结果 + 生产节拍 + 测试项目名称 + 测试项上限 + 测试项下限 + 测试项实际值;
                                string cheshixm1 = $"2+{stationNameList[i]}+{txtWorkOrder.Text}+{barcodeInfo}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}" +
                                $"+{Value[9999]}+{D1090}+{testItemsName[i]}+{maxList[i]}+{minList[i]}+{actualValueList[i]}";

                                productionData += "|" + cheshixm1;
                            }
                        }
                    }
                }
                else
                {
                    // 2 + 工位名称 + 当前工单号 + 产品条码 + 操作人员 + 测试时间 + 测试结果 + 生产节拍 + 测试项名称 + 测试项上限 + 测试项下限 + 测式项实际值;
                    // 注意：（测试项名称 + 测试项上限 + 测试项下限 + 测式项实际值）为空
                    for (int i = 0; i < stationNameSets.Length; i++)
                    {
                        /*string cheshixm2 = "2+" + stationNameSets[i] + "+" + txtWorkOrder.Text + "+" + barcodeInfo + "+"
                         + LoginUser.ToString() + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                "+" + Value[9999] + "+" + D1090 + "+" + "测试总结果" + "+" + " " + "+" + " " + "+" + Value[9999];*/

                        string cheshixm2 = $"2+{stationNameSets[i]}+{txtWorkOrder.Text}+{barcodeInfo}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}+{Value[9999]}+{D1090}+测试总结果+ + +{Value[9999]}";
                        productionData += "|" + cheshixm2;
                    }
                }

                ShowMsg(productionData);
                Send(productionData);
            }));
        }

        private static readonly object objSync = new object();

        /// <summary>
        /// 发送信息到服务器
        /// <para>
        /// |0+工位名称集合|
        /// </para>
        /// <para>
        /// 1+故障所在工位+机台名称+故障状态+故障的描述+触发故障的开始时间 
        /// 1+故障所在工位+机台名称+故障状态+故障的描述+触发故障的结束时间
        /// </para>
        /// <para>
        /// 2+工位名称+当前工单号+产品条码+操作人员+测试时间+测试结果+测试节拍+测试项名称+测试项上限+测试项下限+测式项实际值
        /// </para>
        /// <para>
        /// 统计信息：
        /// 3+机台名称+工单号+工单数量+完成数量+完成率+合格率+整体/生产/整线节拍+生产总数+工序时间+利用时间+负荷时间+直通率+产品型号
        /// </para>
        /// <para>
        /// 4+易损件所在工位+机台名称+ 易损件所在位置+易损件名称+易损件理论使用次数+易损件已使用次数
        /// </para>
        /// <para>
        /// 工位状态：
        /// |5+机台名称|
        /// </para>
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
                                byte[] buffer = new byte[1024 * 1024 * 3];
                                buffer = Encoding.UTF8.GetBytes("|" + strmsg[i] + "|");
                                socket.Send(buffer);
                            }
                        }
                        catch (Exception ex)
                        {
                            isDashboardConnected = false;
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

                string info = string.Format($"{DateTime.Now:G}:{msg}{Environment.NewLine}");
                rtbDashboardLog.AppendText(info);
            }));
        }

        #endregion

        #region ------------ 处理接受过来的数据 ------------

        bool isAllowSwitchWorkOrder = false;

        public void ReceiveData(string receivedMsg)
        {
            string[] dateArray = new string[] { };
            dateArray = receivedMsg.Split(new char[] { '+' });

            // dateArray[0]
            // "0"：切换配方号； 0+配方号
            // "1"：切换工单号； 0+工单号

            switch (dateArray[0])
            {
                case "0":
                    try
                    {
                        // 向PLC发送配方切换信号，并发送对应配方号
                        KeyenceMcNet.Write(deviceInfo.ModifyRecipePoint, 1);
                        KeyenceMcNet.Write(deviceInfo.ModifyRecipeIDPoint, int.Parse(dateArray[1].ToString()));

                        // 更新系统配方号
                        dbHelper = new MDBHelper(path4);
                        DataTable SytemSetTable = dbHelper.Find(" SELECT * FROM [SytemSet] WHERE ID = '1' ");
                        if (SytemSetTable.Rows.Count > 0)
                        {
                            string sql = $" UPDATE [SytemSet] SET [faults]= '{dateArray[1]}' WHERE [ID] = '1' ";
                            var result = dbHelper.Change(sql);
                        }

                        // 反馈配方切换状态
                        Invoke(new Action(() =>
                        {
                            cboBarcodeRuleAndFixtures.SelectedValue = dateArray[1];
                            lblRecipeId.Text = dateArray[1];
                            Send($"6+{lblDeviceName.Text}配方切换成功");
                        }));
                    }
                    catch (Exception ex)
                    {
                        //DisplayMessage(ex.ToString());
                        Send($"6+{lblDeviceName.Text}配方切换失败");
                    }
                    break;
                case "1":
                    try
                    {
                        // 联机登录成功时接收工单
                        if (isOffLine == 0 && isMesLoginSuccessful == true)
                        {
                            // 切换工单号
                            if (isAllowSwitchWorkOrder)
                            {
                                Invoke(new Action(() =>
                                {
                                    txtWorkOrder.Text = dateArray[1];
                                }));

                                Invoke(new Action(() =>
                                {
                                    if (txtWorkOrder.Text.Equals(dateArray[1]))
                                    {
                                        Send($"6+{lblDeviceName.Text}生产工单接收成功");
                                    }
                                }));
                            }
                            else
                            {
                                char[] prowrd = dateArray[1].ToCharArray();
                                foreach (var chstr in prowrd)
                                {
                                    SendKeys.SendWait("{" + chstr + "}");
                                }
                                SendKeys.SendWait("{Enter}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Send($"6+{lblDeviceName.Text}生产工单接收失败");
                    }
                    break;
            }
        }

        #endregion

        #region ------------ 打印信息 ------------

        DatasModel.Printers printers = new DatasModel.Printers();
        DatasModel.PrintersBtw printersBtw = new PrintersBtw();
        DatasModel.PrinterSetting printerConfig = new DatasModel.PrinterSetting();
        private static BarTender.Application btApp = null;
        private static BarTender.Format btFormat;
        bool prinBoolOK = false;
        private Socket clientSocket = null;     // 连接打印机

        #region ------ 打印机基本配置 ------

        /// <summary>
        /// 获取可用的打印机
        /// </summary>
        private void GetPrinterName()
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
        /// 加载打印机配置信息
        /// </summary>
        private void LoadPrinterConfig()
        {
            try
            {
                #region ------ 加载打印机基本配置 ------

                printerConfig = DatasServer.PrinterSettingServer.GetPrinterSetting(1);
                txtStartPoint_print.Text = printerConfig.PrintStep;
                txtEndPoint_Print.Text = printerConfig.PrintResult;
                chkPlcControlPrint.Checked = printerConfig.ControlledByPLC;
                cboPrintMode.Text = printerConfig.PrintMode;

                #endregion

                #region ------ 加载网络打印相关配置 printers ------

                printers = DatasServer.PrintersServer.GetPrinters(1);

                // 打印机网络配置
                txtPrinter_IP.Text = printers.ip;       // IP
                txtPrinter_Port.Text = printers.prot;   // 端口
                // 动态码
                txtCodeNum_TCP.Text = printers.Ptime;   // 码号
                txtSN_TCP.Text = printers.Ptail;        // 条码数字（流水号）
                // 文本
                txtBefore_TCP.Text = printers.TFront;       // 条码前端
                txtAfter_TCP.Text = printers.Tmonarch;     // 条码后端
                txtPModel_TCP.Text = printers.Tlow;         // 条码型号
                // 配置prn文件
                lblPrnFilePath_TCP.Text = printers.Method;
                LoadPrnFiles(lblPrnFilePath_TCP.Text);

                for (int n = 0; n < cboPrintFile_TCP.Items.Count; n++)
                {
                    if (cboPrintFile_TCP.GetItemText(cboPrintFile_TCP.Items[n]).Equals(printers.Ptype))
                    {
                        // 将每个选项转换为文本并存入数组
                        cboPrintFile_TCP.SelectedItem = printers.Ptype;
                        break;
                    }
                }

                string txttow = printers.Txttow;        // 是否打印文字
                string plcmodel = printers.Plcmodel;    // 是否读取PLC型号
                string phead = printers.Phead;          // 是否由PLC控制
                if (txttow == "True") chkUseFront_TCP.Checked = true;
                if (plcmodel == "True") chkLoadModel_TCP.Checked = true;
                //if (phead == "True") checkBox7.Checked = true;    
                lblContent_TCP.Text = Pfunime() + txtSN_TCP.Text;  // 条码内容

                #endregion

                #region------ 加载驱动打印相关配置 PrintersBtw ------

                printersBtw = DatasServer.PrintersBtwServer.GetPrintersBtw(1);

                // 打印配置
                cboPrinterType.Text = printersBtw.PtypeBtw;             // 打印机类型
                // 文本
                textBox54.Text = printersBtw.PheadBtw;                  // 条码前端
                textBox53.Text = printersBtw.PtimeBtw;                  // 条码后端
                txtPModel_COM.Text = printersBtw.PModel;                // 条码型号
                chkLoadModel_COM.Checked = printersBtw.IsLoadModel_PLC; // 是否读取PLC型号
                chkUseFont.Checked = printersBtw.MethodBtw;             // 使用文字
                // 动态码
                txtCodeNumber.Text = printersBtw.BarcodeNumber;         // 码号
                txtSerialNumber.Text = printersBtw.SerialNumber;        // 流水号
                txtSerialSpan.Text = printersBtw.TlowBtw;               // 打印数量
                chkAutoAddDate.Checked = printersBtw.PrintNowDateTime;  // 自动添加日期
                chkPlus2Print.Checked = printersBtw.PrintCodeTwoBool;   // +2打印
                // 配置prn文件
                lblPrnFilePath_COM.Text = printersBtw.PertowBtw;        // prn文件路径
                cboPrintFormat_COM.Text = printersBtw.TxttowBtw;        // 打印文件格式

                #endregion
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        /// <summary>
        /// 保存打印基本配置
        /// </summary>
        private void SavePrinterConfig_Click(object sender, EventArgs e)
        {
            printerConfig.PrintStep = txtStartPoint_print.Text;        // 开始点位
            printerConfig.PrintResult = txtEndPoint_Print.Text;     // 结束点位
            printerConfig.ControlledByPLC = chkPlcControlPrint.Checked; // 是否启用PLC控制打印
            printerConfig.PrintMode = cboPrintMode.Text;                // 打印模式
            var result = printerConfig.Save();
            MessageBox.Show(result);
        }

        #endregion

        #region ------ 打印格式 ------

        /// <summary>
        /// BTW 文件打印
        /// </summary>
        /// <param name="printCode"></param>
        /// <param name="printAgo"></param>
        /// <param name="printAfter"></param>
        /// <param name="printType"></param>
        /// <param name="printNum"></param>
        /// <exception cref="Exception"></exception>
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

                switch (chkUseFont.Checked)
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

                if (chkPlus2Print.Checked)
                {
                    SerialNumberIncrement(1);
                    string printCode2 = GenerateBarcode();
                    btFormat.SetNamedSubStringValue("4", printCode2);
                }
                else
                {
                    btFormat.SetNamedSubStringValue("4", printCode);
                }

                await Task.Run(() =>
                {
                    // 打印标签
                    btFormat.PrintOut(false, false);
                    // 不保存标签退出
                    btFormat.Close(BarTender.BtSaveOptions.btDoNotSaveChanges);
                    prinBoolOK = true;
                });
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// .prn文件格式打印
        /// </summary>
        /// <param name="barcodeInfo"></param>
        /// <param name="printAgo"></param>
        /// <param name="printAfter"></param>
        /// <param name="pModel"></param>
        private void PrinteWithPrn(string barcodeInfo, string printAgo, string printAfter, string pModel, int printCount)
        {
            /*try
            {
                for (int i = 0; i < printCount; i++)
                {
                    string instruction;
                    string fileContent = File.ReadAllText(lblPrnFilePath_COM.Text);

                    // 使用文字 && +2
                    if (chkUseFont.Checked && chkPlus2Print.Checked == false)
                    {
                        instruction = string.Format(fileContent, barcodeInfo, printAgo, printAfter, pModel);
                    }
                    else if (chkPlus2Print.Checked)
                    {
                        SerialNumberIncrement(1);   // 添加一
                        string barcodeInfo2 = GenerateBarcode();
                        instruction = string.Format(fileContent, barcodeInfo, printAgo, printAfter, pModel, barcodeInfo2);
                    }
                    else
                    {
                        instruction = string.Format(fileContent, barcodeInfo);
                    }

                    // Send a printer-specific to the printer.
                    RawPrinterHelper.SendStringToPrinter(cboPrinterType.Text, instruction);

                    if (printCount > 1)
                    {
                        SerialNumberIncrement(1);   // 添加一
                        barcodeInfo = GenerateBarcode();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }*/

            try
            {
                string fileContent = File.ReadAllText(lblPrnFilePath_COM.Text);
                int copiesPerBarcode = chkPlus2Print.Checked ? 2 : 1;           // 每个条码打印的份数

                for (int i = 0; i < printCount; i++)
                {
                    string instruction;

                    // 使用文字
                    if (chkUseFont.Checked)
                    {
                        instruction = string.Format(fileContent, barcodeInfo, printAgo, printAfter, pModel);
                    }
                    else
                    {
                        instruction = string.Format(fileContent, barcodeInfo);
                    }

                    // 打印指定份数的相同条码
                    for (int j = 0; j < copiesPerBarcode; j++)
                    {
                        // 发送打印指令到打印机
                        RawPrinterHelper.SendStringToPrinter(cboPrinterType.Text, instruction);
                    }

                    // 如果不是最后一组条码，则增加流水号并生成新的条码
                    if (i < printCount - 1)
                    {
                        SerialNumberIncrement(1);
                        barcodeInfo = GenerateBarcode();
                    }
                }
            }
            catch (Exception ex)
            {
                // 记录异常并重新抛出
                Console.WriteLine($"打印过程中发生错误：{ex.Message}");
                throw;
            }
        }

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

        #endregion

        #region ------ 驱动打印 ------

        /// <summary>
        /// 驱动打印 > 更改prn文件所在位置
        /// </summary>
        private void ChangePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (string.IsNullOrEmpty(cboPrintFormat_COM.Text))
            {
                MessageBox.Show("请选择打印文件格式");
                return;
            }
            openFileDialog.Filter = "PRN files " + cboPrintFormat_COM.Text;
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

        /// <summary>
        /// 驱动打印设置 -> 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveCOM_Click(object sender, EventArgs e)
        {
            printersBtw.PtypeBtw = cboPrinterType.Text;             // 打印机类型
            printersBtw.PheadBtw = textBox54.Text;                  // 条码前端
            printersBtw.PtimeBtw = textBox53.Text;                  // 条码后端
            printersBtw.PModel = txtPModel_COM.Text;                // 条码型号
            printersBtw.IsLoadModel_PLC = chkLoadModel_COM.Checked; // 是否读取PLC型号
            printersBtw.MethodBtw = chkUseFont.Checked;             // 使用文字

            printersBtw.BarcodeNumber = txtCodeNumber.Text;         // 码号
            printersBtw.SerialNumber = txtSerialNumber.Text;        // 流水号
            printersBtw.TlowBtw = txtSerialSpan.Text;               // 打印数量
            printersBtw.PertowBtw = lblPrnFilePath_COM.Text;        // prn文件路径
            printersBtw.TxttowBtw = cboPrintFormat_COM.Text;        // 打印文件格式
            printersBtw.PrintNowDateTime = chkAutoAddDate.Checked;  // 自动添加日期
            printersBtw.PrintSerialNumber = false;                  // 打印机打印条码
            printersBtw.PrintCodeTwoBool = chkPlus2Print.Checked;   // +2打印

            var result = printersBtw.Save();
            MessageBox.Show(result);
        }

        /// <summary>
        /// 驱动打印
        /// </summary>
        private void BtnDriverPrint_Click(object sender, EventArgs e)
        {
            // 判断是否选中打印机
            if (string.IsNullOrWhiteSpace(cboPrinterType.Text)) return;
            try
            {
                GenerateBarcode();  // 生成条码内容
                GoToPrint();        // 打印条码

                lblPrintPrompt.Text = "OK";
                lblPrintPrompt.ForeColor = Color.Green;
            }
            catch (Exception)
            {
                PrintNGPrompt();
            }
            if (prinBoolOK == false)
            {
                PrintNGPrompt();
            }
        }

        /// <summary>
        /// 生成条码内容
        /// </summary>
        /// <returns>18位条码字符串</returns>
        /// <remarks>
        /// <para>
        /// 条码 = 码号（10位）+ 生产日期（3位） + 流水号（5位）= 18位；
        /// </para>
        /// 码号 = 地区代码（5位）+ 总成代码（4位）+ 年份（1位）= 10位；
        /// </remarks>
        private string GenerateBarcode()
        {
            // 获取码号
            string codeNumber = txtCodeNumber.Text;

            // 获取流水号单次自增数量
            if (!int.TryParse(txtSerialSpan.Text, out int incrementCount))
            {
                // 解析失败时，默认打印数量为1
                incrementCount = 1;
            }

            DateTime currentDate = DateTime.Now.Date;

            // 检查日期并在需要时重置流水号
            if (ShouldResetSerialNumber())
            {
                ResetSerialNumber();
                SaveLastSavedDate(DateTime.Now.Date);
            }
            else
            {
                // 无论是否重置，都更新最后保存日期
                SaveLastSavedDate(currentDate);
            }

            SerialNumberIncrement(incrementCount);  // 设定流水号自增数量

            // 添加生产日期
            if (chkAutoAddDate.Checked)
            {
                string month = DateTime.Now.Month.ToString();
                string day = DateTime.Now.Day.ToString("D2");

                if (month == "10" || month == "11" || month == "12")
                {
                    switch (month)
                    {
                        case "10": month = "0"; break;
                        case "11": month = "A"; break;
                        case "12": month = "B"; break;
                    }
                }

                // 生成码号
                codeNumber = codeNumber += month + day;
            }

            // 生产日期的年份包含在码号里面，生产日期只对应月份和日期，其中月份(1位) + 日期(2位)
            // 月份使用数字：1 ~ 9，10(0), 11(A), 12(B); (年份字母禁用：I，O，Q，U，Z)
            // 日期使用数字：01 ~ 31
            string barcodeInfo = codeNumber + txtSerialNumber.Text;
            lblCodeContent.Text = barcodeInfo;
            return barcodeInfo;
        }

        /// <summary>
        /// 打印条码
        /// </summary>
        /// <returns>18为条码字符串</returns>
        private void GoToPrint()
        {
            string printAgo = textBox54.Text;        // 条码前端
            string printAfter = textBox53.Text;      // 条码后端
            string pModel = txtPModel_COM.Text;      // 产品型号
            string barcodeInfo = lblCodeContent.Text;   // 条码内容
            //string printSerial = txtSerialNumber.Text;// 流水号

            // 获取打印数量
            if (!int.TryParse(txtPrintCount.Text, out int printCount))
            {
                // 默认数量为1
                printCount = 1;
            }

            // 判断打印格式
            switch (cboPrintFormat_COM.SelectedIndex)
            {
                case 0: // BTW 文件打印
                    BTW_PrintersCodes(barcodeInfo, printAgo, printAfter, pModel, printCount);
                    break;
                case 1: // PRN 文件打印
                    PrinteWithPrn(barcodeInfo, printAgo, printAfter, pModel, printCount);
                    break;
            }
        }

        private void PrintNGPrompt()
        {
            lblPrintPrompt.Text = "NG";
            lblPrintPrompt.ForeColor = Color.Red;
        }

        /// <summary>
        /// 流水号自增
        /// </summary>
        /// <param name="incrementCount"></param>
        private async void SerialNumberIncrement(int incrementCount)
        {
            try
            {
                // 获取当前流水号
                int currentSerialNumber = GetCurrentSerialNumber();

                // 计算新的流水号
                int newSerialNumber = currentSerialNumber + incrementCount;

                // 更新流水号
                UpdateSerialNumber(newSerialNumber);
                Console.WriteLine($"Serial number incremented from {currentSerialNumber} to {newSerialNumber}");

                // 异步保存
                await SaveSerialNumberAsync();
            }
            catch (Exception ex)
            {
                // 处理异常
                MessageBox.Show($"更新流水号时发生错误：{ex.Message}");
            }
        }

        private int GetCurrentSerialNumber()
        {
            // 初始流水号：00000 共5位，且需要每天清零
            if (!int.TryParse(txtSerialNumber.Text, out int serialNumber))
            {
                serialNumber = 0;
            }
            return serialNumber;
        }

        public void InitializeSerialNumber()
        {
            if (ShouldResetSerialNumber())
            {
                ResetSerialNumber();
                SaveLastSavedDate(DateTime.Now.Date);
            }
        }

        private bool ShouldResetSerialNumber()
        {
            DateTime lastSavedDate = printersBtw.LastSavedDate;
            DateTime currentDate = DateTime.Now.Date;

            // 使用日期差值来判断
            TimeSpan difference = currentDate.Date - lastSavedDate.Date;

            // 如果当前日期小于最后保存日期，说明时间被回调了，我们也应该重置
            return currentDate > lastSavedDate || currentDate < lastSavedDate;
        }

        private void ResetSerialNumber()
        {
            txtSN.Text = sytemSetDerivedsd.SerialNumber = 0.ToString("D5");

            UpdateSerialNumber(0);

            SaveLastSavedDate(DateTime.Now.Date);
        }

        private void UpdateSerialNumber(int newSerialNumber)
        {
            string formattedSerialNumber = newSerialNumber.ToString("D5");

            txtSerialNumber.Text = printersBtw.SerialNumber = formattedSerialNumber;
        }

        public void SaveLastSavedDate(DateTime date)
        {
            printersBtw.LastSavedDate = date.Date;  // 只保存日期
            printersBtw.Save();
        }

        public async Task SaveSerialNumberAsync()
        {
            await Task.Run(() =>
            {
                printersBtw.Save();
                Console.WriteLine("All changes saved");
            });
        }

        #endregion

        #region ------ 网络打印 ------

        /// <summary>
        /// 网络打印设置 -> 打印按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTCPPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboPrintFile_TCP.Text))
            {
                MessageBox.Show("请选择打印文件！！!");
                return;
            }

            try
            {
                Serialnumber();         // 将流水号+1
                string instrctis = "";
                string fileContent = File.ReadAllText(cboPrintFile_TCP.Text);

                // 使用文字
                if (chkUseFront_TCP.Checked)
                {
                    instrctis = string.Format(fileContent, lblContent_TCP.Text, txtBefore_TCP.Text, txtAfter_TCP.Text, txtPModel_TCP.Text);
                }
                else
                {
                    instrctis = string.Format(fileContent, lblContent_TCP.Text);
                }

                byte[] heartbeatData = System.Text.Encoding.UTF8.GetBytes(instrctis.ToString());
                Send(heartbeatData);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 网络打印设置 -> 斑马测试打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintZebraTest_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbInstalledPrinters.Text))
            {
                // Send a printer-specific to the printer.
                RawPrinterHelper.SendStringToPrinter(cmbInstalledPrinters.Text, this.richTextBox2.Text);
            }
        }

        /// <summary>
        /// 处理流水号数据
        /// </summary>
        private void Serialnumber()
        {
            string serialunm = txtSN_TCP.Text;
            int num;        // 发送一条加一
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
            dbHelper = new MDBHelper(path4);
            DataTable table1 = dbHelper.Find("select Ptail from Printers where ID = 1");
            if (table1.Rows.Count > 0)
            {
                int numA = num + 1;
                string seriaA = numA.ToString().PadLeft(5, '0');
                string sql = "update [Printers] set [Ptail]='" + seriaA + "'" +
                              " where [ID] = 1";
                var result = dbHelper.Change(sql);
            }
            DataTable table2 = dbHelper.Find("select Ptail from Printers where ID = 1");
            for (int i = 0; i < table2.Rows.Count; i++)
            {
                for (int j = 0; j < table2.Columns.Count; j++)
                {
                    txtSN_TCP.Text = table2.Rows[i]["Ptail"].ToString();
                }
            }
            dbHelper.CloseConnection();
            lblContent_TCP.Text = Pfunime() + txtSN_TCP.Text;
        }

        /// <summary>
        /// 网络打印设置 > 查看文件所在位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPath_TCP_Click(object sender, EventArgs e)
        {
            string filePath = cboPrintFile_TCP.Text;
            Direc_Troy_Path_Position(filePath);
        }

        /// <summary>
        /// 网络打印设置 -> 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAtTCP_Click(object sender, EventArgs e)
        {
            if (txtCodeNum_TCP.Text == String.Empty)
            {
                MessageBox.Show("当前界面内容均为必填项、请先填写完善");
                return;
            }
            try
            {
                printers.Ptype = cboPrintFile_TCP.Text;
                printers.Ptime = txtCodeNum_TCP.Text;// table1.Rows[i]["Ptime"].ToString();//条码时间格式
                printers.Ptail = txtSN_TCP.Text;//条码数字
                printers.TFront = txtBefore_TCP.Text;//字体前端
                printers.Tmonarch = txtAfter_TCP.Text;//字体后端
                printers.Tlow = txtPModel_TCP.Text;//字体下面
                printers.Txttow = chkUseFront_TCP.Checked.ToString();
                printers.Plcmodel = chkLoadModel_TCP.Checked.ToString();
                printers.Method = lblPrnFilePath_TCP.Text;
                printers.Phead = "False";
                printers.ip = txtPrinter_IP.Text;
                printers.prot = txtPrinter_Port.Text;
                var result = printers.Save();
                MessageBox.Show(result);

                string method = "  ";

                lblContent_TCP.Text = Pfunime() + txtSN_TCP.Text;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        /// <summary>
        /// 网络打印设置 >　更改prn文件所在位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePath_TCP_Click(object sender, EventArgs e)
        {
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
        /// 网络打印设置 -> 连接按钮
        /// </summary>
        /// <remarks>
        /// 用于连接打印机
        /// </remarks>
        private void btnConnectPrinter_Click(object sender, EventArgs e)
        {
            ConnectPrintServer();
        }

        public Boolean ConnectPrintServer()
        {
            System.Net.IPAddress ipAddress;
            if (!System.Net.IPAddress.TryParse(txtPrinter_IP.Text.ToString(), out ipAddress))
            {
                MessageBox.Show("IP地址无效");
                return false;
            }
            string ip = txtPrinter_IP.Text.ToString();
            int port = 9100;
            bool isNumber = int.TryParse(txtPrinter_Port.Text.ToString(), out port);

            // 设置连接
            EndPoint point = new IPEndPoint(System.Net.IPAddress.Parse(ip), port);		// Server IP 和端

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
        /// 条码前部分+日期处理
        /// </summary>
        /// <returns></returns>
        private string Pfunime()
        {
            string Ptime = txtCodeNum_TCP.Text;
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

        private void LoadPrnFiles(string folderPath)
        {
            try
            {
                cboPrintFile_TCP.Items.Clear();
                List<string> listprn = PRNchDPrnFiles.LoadAllPrnFilesFromDisk(folderPath);

                foreach (string file in listprn)
                {
                    // 在这里处理PRN文件
                    // 例如：打开文件、读取内容、显示在界面等
                    cboPrintFile_TCP.Items.Add(file);
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        /// <summary>
        /// PCL控制打印
        /// </summary>
        private void PlcControlPrint()
        {
            Task.Run(() =>
            {
                // 触发通讯读
                if (isPLCConnected)
                {
                    var printResult = KeyenceMcNet.ReadInt16(printerConfig.PrintResult).Content;    // D1014
                    var printStep = KeyenceMcNet.ReadInt32(printerConfig.PrintStep).Content;        // D1012

                    // 打印成功：反馈PrintResult = 1;   新打：printStep = 1；
                    // 打印失败：反馈PrintResult = 2；  重打：printStep = 2；

                    // 新打印：
                    if (printStep == 1 && printResult == 0)
                    {
                        Invoke(new Action(() =>
                        {
                            try
                            {
                                switch (cboPrintMode.SelectedIndex)
                                {
                                    case 0:     // 网络打印
                                        btnTCPPrint_Click(null, null);
                                        break;
                                    case 1:     // 驱动打印
                                        BtnDriverPrint_Click(null, null);
                                        break;
                                }

                                // 反馈打印结果：成功
                                KeyenceMcNet.Write(printerConfig.PrintResult, 1);
                            }
                            catch (Exception ex)
                            {
                                // 反馈打印结果：失败
                                KeyenceMcNet.Write(printerConfig.PrintResult, 2);
                            }
                        }));

                        Thread.Sleep(250);
                        Application.DoEvents();
                    }
                    else if (printStep == 2)
                    {
                        Invoke(new Action(() =>
                        {
                            try
                            {
                                switch (cboPrintMode.SelectedIndex)
                                {
                                    case 0:
                                        repeatBarCode();// 发送打印机指令
                                        break;
                                    case 1:
                                        GoToPrint();    // 驱动打印 -> 打印条码
                                        break;
                                }
                                //D1014
                                KeyenceMcNet.Write(printerConfig.PrintResult, 1);
                            }
                            catch (Exception ex)
                            {
                                //D1014
                                KeyenceMcNet.Write(printerConfig.PrintResult, 2);
                            }
                        }));

                        Thread.Sleep(250);
                        Application.DoEvents();
                    }
                }
            });
        }

        /// <summary>
        /// 重复发送条码
        /// </summary>
        private void repeatBarCode()
        {
            try
            {
                string construction = "";
                // 读取 .prn文件内容
                string fileContent = File.ReadAllText(cboPrintFile_TCP.Text);

                // 是否使用文字
                if (chkUseFront_TCP.Checked)
                {
                    // .prn文件, 码内容, 码前端, 码后端, 型号;
                    construction = string.Format(fileContent, lblContent_TCP.Text, txtBefore_TCP.Text, txtAfter_TCP.Text, txtPModel_TCP.Text);
                }
                else
                {
                    // .prn文件, 码内容
                    construction = string.Format(fileContent, lblContent_TCP.Text);
                }

                byte[] heartbeatData = System.Text.Encoding.UTF8.GetBytes(construction.ToString());
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
                    if (clientSocket is null || !clientSocket.Connected)
                        while (!ConnectPrintServer()) ;   // 如果未连接，就连接上
                    clientSocket.Send(msg);    //把字节数组发送到服务器端
                    Console.WriteLine("发送成功！");
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
            });
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

        DataTable codesTable;           // 存储产品编号与条码验证型号
        DataTable PLCPointInfoTable;    // 存储PLC点位信息
        BindingList<Codes> bindingCodes = new BindingList<Codes>(); // 存储配方设置相关的参数
        BindingList<BarcodeVerification> bindingVefictn = new BindingList<BarcodeVerification>();   // 条码相关数据

        /// <summary>
        /// 从数据库加载 Board 表格保存到对应的数组中，并为 DataGridView4,5 设置属性
        /// </summary>
        private void SYS_BOARD()
        {
            RefreshTable();

            dbHelper = new MDBHelper(path4);
            PLCPointInfoTable = dbHelper.Find("SELECT * FROM Board");
            // 检查表格中是否存在数据
            if (PLCPointInfoTable.Rows.Count > 0)
            {
                // 从数据库表 Board 中的每一行提取特定列的数据，并存储在相应的数组中
                targetStationNum = PLCPointInfoTable.AsEnumerable().Select(row => row["WorkID"].ToString()).ToArray();
                testItemsName = PLCPointInfoTable.AsEnumerable().Select(row => row["BoardName"].ToString()).ToArray();
                actualValuePoint = PLCPointInfoTable.AsEnumerable().Select(row => row["BoardCode"].ToString()).ToArray();
                maxValuePoint = PLCPointInfoTable.AsEnumerable().Select(row => row["MaxBoardCode"].ToString()).ToArray();
                minValuePoint = PLCPointInfoTable.AsEnumerable().Select(row => row["MinBoardCode"].ToString()).ToArray();
                beatPoint = PLCPointInfoTable.AsEnumerable().Select(row => row["BeatBoardCode"].ToString()).ToArray();
                testResultPoint = PLCPointInfoTable.AsEnumerable().Select(row => row["ResultBoardCode"].ToString()).ToArray();
                unitName = PLCPointInfoTable.AsEnumerable().Select(row => row["BoardA1"].ToString()).ToArray();
                standardValuePoint = PLCPointInfoTable.AsEnumerable().Select(row => row["StandardCode"].ToString()).ToArray();
            }
            dbHelper.CloseConnection();

            #region 初始化PLC点位信息表格
            // 为 DataGridView4 添加 ButtonColumn 列，标题为操作，按钮Text默认显示为 “NO保存”
            DataGridViewButtonColumn btnNOSave = new DataGridViewButtonColumn();
            btnNOSave.HeaderText = "操作";
            btnNOSave.Text = "NO保存";
            btnNOSave.Name = "btnColNO";
            btnNOSave.DefaultCellStyle.NullValue = "NO保存";
            dgvPLCPointInfo.Columns.Add(btnNOSave);

            // 为 DataGridView4 添加 ButtonColumn 列，标题为操作，按钮Text默认显示为 “ONE保存”
            DataGridViewButtonColumn btnOneSave = new DataGridViewButtonColumn();
            btnOneSave.HeaderText = "操作";
            btnOneSave.Text = "ONE保存";
            btnOneSave.Name = "btnColONE";
            btnOneSave.DefaultCellStyle.NullValue = "ONE保存";
            dgvPLCPointInfo.Columns.Add(btnOneSave);

            // 为 DataGridView4 添加 ButtonColumn 列，标题为操作，按钮Text默认显示为 “删除”
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.HeaderText = "操作";
            btnDelete.Name = "btnCol2";
            btnDelete.DefaultCellStyle.NullValue = "删除";
            dgvPLCPointInfo.Columns.Add(btnDelete);

            // DataGridView5 属性设置，保存
            DataGridViewButtonColumn btnSave = new DataGridViewButtonColumn();
            btnSave.HeaderText = resources.GetString("operation");
            btnSave.Text = resources.GetString("save");
            btnSave.Name = "btnCol";
            btnSave.DefaultCellStyle.NullValue = resources.GetString("save");
            dataGridView5.Columns.Add(btnSave);

            // DataGridView5 属性设置，删除
            DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn();
            btnDel.HeaderText = resources.GetString("operation");
            btnDel.Name = "btnCol2";
            btnDel.DefaultCellStyle.NullValue = resources.GetString("del");
            dataGridView5.Columns.Add(btnDel);
            #endregion
        }

        private void RefreshTable()
        {
            btnRefreshTable_Click(null, null);
        }

        /// <summary>
        /// 刷新PLC点位数据、产品型号相关数据
        /// </summary>
        private void btnRefreshTable_Click(object sender, EventArgs e)
        {
            dbHelper = new MDBHelper(path4);

            // 查询Board表格
            string selectSql = $@" SELECT [ID] AS 编号,
                                      [WorkID] AS 工位ID,
                                   [BoardName] AS 测试项目的名称,
                                   [BoardCode] AS 测试项目的PLC点位,
                                [StandardCode] AS 测试项目标准值的PLC点位,
                                [MaxBoardCode] AS 测试项目的上限PLC点位,
                                [MinBoardCode] AS 测试项目的下限PLC点位,
                             [ResultBoardCode] AS 测试项目的结果PLC点位,
                               [BeatBoardCode] AS 测试项目的节拍PLC点位,
                                     [BoardA1] AS 单位 FROM [Board] ";
            DataTable boardTable = dbHelper.Find(selectSql);

            // 设置 “编号” 列的属性为自动增长、只读、起始值为0
            boardTable.Columns["编号"].AutoIncrement = true;
            boardTable.Columns["编号"].ReadOnly = true;
            boardTable.Columns["编号"].AutoIncrementSeed = 0;

            // 新增行自动填充编号
            int maxRowNum = 0;
            if (boardTable.Rows.Count > 0)
            {
                maxRowNum = boardTable.AsEnumerable().Max(row => row.Field<int>("编号"));
            }
            DataRow newRow = boardTable.NewRow();
            newRow["编号"] = maxRowNum;

            // 刷新PLC点位数据表格
            dgvPLCPointInfo.DataSource = boardTable;

            // 查询 Codes 表的数据
            codesTable = dbHelper.Find("SELECT * FROM [Codes]");

            // 获取绑定列表对象，刷新 dataGridView5
            bindingCodes = CodesServer.GetCodesBindingList();
            dataGridView5.DataSource = bindingCodes;

            // 设置 dataGridView5 的列头文本
            CodesDataGridView codesDataGridView = new CodesDataGridView();
            codesDataGridView.GetCodesDataGridViewHeaderText(dataGridView5);

            // 将 Codes 表的数据绑定到控件，并设置显示成员和值成员
            cboBarcodeRuleAndFixtures.DataSource = codesTable;
            cboBarcodeRuleAndFixtures.DisplayMember = "TooName";
            cboBarcodeRuleAndFixtures.ValueMember = "ID";

            dbHelper.CloseConnection();

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
            DeleteRowFromDataGridView<int>(dgvPLCPointInfo, e, "Board", "ID", logFields, 3, path4, btnName: "btnCol2", fieldAliases, "PLC点位数据");

            // NO保存
            if (dgvPLCPointInfo.Columns[e.ColumnIndex].Name == "btnColNO")
            {
                //说明点击的列是DataGridViewButtonColumn列
                DataGridViewColumn column = dgvPLCPointInfo.Columns[e.ColumnIndex];
                string pid = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[3].Value.ToString();
                string workID = CodeNum.GetOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[4].Value.ToString());
                string boardName = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[5].Value.ToString();
                string boardCode = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[6].Value.ToString();
                string stanCode = CodeNum.GetNoIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[7].Value.ToString());
                string maxBoardCode = CodeNum.GetNoIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[8].Value.ToString());
                string minBoardCode = CodeNum.GetNoIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[9].Value.ToString());
                string resultBoardCode = CodeNum.GetNoIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[10].Value.ToString());
                string beatBoardCode = CodeNum.GetNoIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[11].Value.ToString());
                string beatBoardA1 = CodeNum.GetNullUnit(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[12].Value.ToString());

                ModifyPlcAddress(pid, workID, stanCode, boardName, boardCode, maxBoardCode, minBoardCode, resultBoardCode, beatBoardCode, beatBoardA1);
                RefreshTable();
            }

            // ONE保存
            if (dgvPLCPointInfo.Columns[e.ColumnIndex].Name == "btnColONE")
            {
                //Board  
                //ID WorkID BoardName BoardCode MaxBoardCode MinBoardCode BeatBoardCode ResultBoardCode 
                //说明点击的列是DataGridViewButtonColumn列
                DataGridViewColumn column = dgvPLCPointInfo.Columns[e.ColumnIndex];
                string pid = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[3].Value.ToString();
                string workID = CodeNum.GetOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[4].Value.ToString());
                string boardName = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[5].Value.ToString();
                string boardCode = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[6].Value.ToString();
                string stanCode = CodeNum.GetPlusOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[7].Value.ToString());
                string maxBoardCode = CodeNum.GetPlusOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[8].Value.ToString());
                string minBoardCode = CodeNum.GetPlusOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[9].Value.ToString());
                string resultBoardCode = CodeNum.GetPlusOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[10].Value.ToString());
                string beatBoardCode = CodeNum.GetPlusOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[11].Value.ToString());
                string beatBoardA1 = CodeNum.GetNullUnit(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[12].Value.ToString());
                //int i = dateM.Rows.Count;
                ModifyPlcAddress(pid, workID, stanCode, boardName, boardCode, maxBoardCode, minBoardCode, resultBoardCode, beatBoardCode, beatBoardA1);
                RefreshTable();
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
                dbHelper = new MDBHelper(connectionString);
                string selectSql = $"SELECT * FROM {tableName} WHERE [{primaryKeyField}] = {primaryKeyCondition}";
                DataTable rowTable = dbHelper.Find(selectSql);

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
                    bool bl = dbHelper.Del(deleteSql);

                    if (bl)
                    {
                        MessageBox.Show("删除成功");
                        loggerConfig.Trace($"【{viewName}删除】\n" +
                                           $"成功删除第{primaryKeyCondition}行, 该行的详细数据: \n{logDetail}");

                        RefreshTable();
                        BtnRefreshAtDashboardSetting(null, null);
                    }
                }

                dbHelper.CloseConnection();
            }
        }

        /// <summary>
        /// 更新或新增 DataGridView4 的数据
        /// </summary>
        private void ModifyPlcAddress(string pid, string workID, string stanCode, string boardName, string boardCode, string maxBoardCode, string minBoardCode, string resultBoardCode, string beatBoardCode, string beatBoardA1)
        {
            dbHelper = new MDBHelper(path4);
            string selectSql = $"select * from Board where [ID] = {pid}";
            DataTable table1 = dbHelper.Find(selectSql);

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

                    var result = dbHelper.Change(sql);
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

                bool result = dbHelper.Add(sql);
                if (result == true)
                {
                    MessageBox.Show("新增成功");
                    loggerConfig.Trace($"【点位数据新增成功】\n新增详情：\n" +
                        $"编号：{pid} | 工位序号：{workID} | 测试项：{boardName} | " +
                        $"实际值点位：{boardCode} | 上限值点位：{maxBoardCode} | 下限值点位：{minBoardCode} | " +
                        $"节拍点位：{beatBoardCode} | 测试结果：{resultBoardCode} | 单位：{beatBoardA1} | 标准值点位：{stanCode} ");
                }
            }

            dbHelper.CloseConnection();
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
            Utility.DGVButAdd.DataGridViewButton.AddDGVButton(dataGridView6);
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            BarcodeVerification bv = bindingVefictn[e.RowIndex];

            // 删除
            if (dataGridView6.Columns[e.ColumnIndex].Name == "BtnDel")
            {
                MessageBox.Show(bv.Delete());
                RefreshTable();
            }

            // 保存
            if (dataGridView6.Columns[e.ColumnIndex].Name == "BtnSave")
            {
                MessageBox.Show(bv.Save());
                RefreshTable();
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

        private void ConnectReader()
        {
            OpenReader_Click(null, null);
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
                                KeyenceMcNet.Write(deviceInfo.EndNFCPoint, -1);
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
                                KeyenceMcNet.Write(deviceInfo.EndNFCPoint, Paccess);
                                loggerAccount.Trace($"【PLC触摸屏当前权限信息】\n 工号：{v.Uuser} | 姓名：{v.userName} | 权限：{v.Utype}");
                            }
                        }
                        else
                        {
                            KeyenceMcNet.Write(deviceInfo.EndNFCPoint, -1);
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
        private void btnReadValue_Click(object sender, EventArgs e)
        {
            txtValue_Read.Text = KeyenceMcNet.ReadInt32(txtPoint_Read.Text).Content.ToString();
            MessageBox.Show("执行完成！");
        }

        /// <summary>
        /// 写入
        /// </summary>
        private void btnWriteValue_Click(object sender, EventArgs e)
        {
            KeyenceMcNet.Write(txtPoint_Write.Text, int.Parse(txtValue_Write.Text));
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
    }
}