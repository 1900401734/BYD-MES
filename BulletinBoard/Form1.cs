using MesDatas;
using MesDatas.DatasServer;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 工艺部信息化组;

namespace BulletinBoard
{
    public partial class Form1 : Form
    {
        public static string databasePath = AppDomain.CurrentDomain.BaseDirectory + "ProdModel.mdb";
        MDBHelper dbHelper = null;
        MDBHelper mdbABC = new MDBHelper();
        private Socket socketWatch;
        private bool IsServerStart;
        private Action<string> ShowMsgAction;
        DataTable stationTable;     // 机台
        DataTable clientInfoTable;  // 运行状态界面 > 已连接的客户端的信息

        Logger rawMsgLogger = LogManager.GetLogger("ReceivedMsg");
        Logger parsedMsg = LogManager.GetLogger("parsedMsg");

        public Form1()
        {
            InitializeComponent();
            ShowMsgAction += new Action<string>(ShowMsg);
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMESConfig();                // 加载MES参数配置
            InitializeProductModelBoard();  // 初始化产品型号面板
            LoadServerConfig();             // 加载服务器基本配置
            RefreshStatus();                // 加载已连接的客户端信息

            string dbPath = $@"{lblDatabasePath.Text}\{DateTime.Now:Y}产线数据.mdb";
            if (mdbABC.TryConnectDatabase(dbPath) == false)
                GenerateProductLineDatabase();  // 生成产线数据库

            ManageMonthlyDatabaseSwitch();
        }

        /// <summary>
        /// 窗体显示时启动Socket服务
        /// </summary>
        private void Form1_Shown(object sender, EventArgs e)
        {
            Thread thread = new Thread(StartSocketServer);
            thread.IsBackground = true;
            thread.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //LineModel_Write();
            //IsRunning = false;
            //StopTools();
            //dataGridView1.updateData -= ProcCompleteData;
            //SaveParam();

            Environment.Exit(0);
        }

        /// <summary>
        /// 管理数据库文件的月度切换
        /// </summary>
        /// <remarks>
        /// 1. 确保当月数据库文件存在
        /// 2. 将上月数据库文件移动到存档目录
        /// </remarks>
        private void ManageMonthlyDatabaseSwitch()
        {
            Invoke(new Action(() =>
            {
                string currentMonth = $"{lblDatabasePath.Text}\\{DateTime.Now:Y}产线数据.mdb";
                string lastMonth = $"{lblDatabasePath.Text}\\{DateTime.Now.AddMonths(-1):Y}产线数据.mdb";
                string Archive = $"{lblDatabasePath.Text}\\path\\{DateTime.Now.AddMonths(-1):Y}产线数据.mdb";
                try
                {
                    if (!File.Exists(currentMonth))
                    {
                        GenerateProductLineDatabase();  // 生成产线数据库
                        mdbABC.TryConnectDatabase(currentMonth);
                    }
                    if (File.Exists(lastMonth))
                    {
                        // 确保目标路径存在
                        string destinationDirectory = Path.GetDirectoryName(Archive);
                        if (!Directory.Exists(destinationDirectory))
                        {
                            Directory.CreateDirectory(destinationDirectory);
                        }
                        mdbABC.CloseConnection();
                        // 移动文件
                        File.Move(lastMonth, Archive);
                        Console.WriteLine("数据库文件移动成功。");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"发生错误: {ex.Message}");
                }
            }));
        }

        public bool IsRunningCheckCard = true;
        public int accesscard;
        public int access;

        #region------------- 服务器设置 -------------

        /// <summary>
        /// 加载服务器参数配置
        /// </summary>
        private void LoadServerConfig()
        {
            LoadStationList();  // 加载工位信息

            dbHelper = new MDBHelper(databasePath);
            DataTable dashboardCongfig = dbHelper.Find(" SELECT * FROM Bulletins WHERE ID = '1' ");

            for (int i = 0; i < dashboardCongfig.Rows.Count; i++)
            {
                for (int j = 0; j < dashboardCongfig.Columns.Count; j++)
                {
                    txt_ServerIP.Text = tbx_IP.Text = dashboardCongfig.Rows[i]["IP"].ToString();
                    txt_ServerPort.Text = tbx_port.Text = dashboardCongfig.Rows[i]["Port"].ToString();
                    txt_BaseName.Text = dashboardCongfig.Rows[i]["BaseName"].ToString();
                    txt_WorkshopName.Text = dashboardCongfig.Rows[i]["WorkshopName"].ToString();
                    txt_PLineName.Text = dashboardCongfig.Rows[i]["ProductLineName"].ToString();
                    txt_PLineDescription.Text = dashboardCongfig.Rows[i]["ProductLineDescription"].ToString();
                    cboPLineAttribute.SelectedItem = dashboardCongfig.Rows[i]["ProductLlineAttributes"].ToString();
                    lblDatabasePath.Text = dashboardCongfig.Rows[i]["Posits"].ToString();
                    txtFinalStation.Text = dashboardCongfig.Rows[i]["WorkName"].ToString();
                    string isStationIDChecked = dashboardCongfig.Rows[i]["FinishedName"].ToString();
                    if (isStationIDChecked == "True")
                    {
                        chkNameToID.Checked = true;
                    }
                    txt_GenarateSpeed.Text = dashboardCongfig.Rows[i]["DegreesSev"].ToString();
                }
            }
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 加载工位信息
        /// </summary>
        private void LoadStationList()
        {
            dbHelper = new MDBHelper(databasePath);
            stationTable = dbHelper.Find("select * from Model");

            string stationInfo = "";
            foreach (DataRow row in stationTable.Rows)
            {
                //stationInfo += "{" + row[0] + "}\n";
                stationInfo += $"[{row[0]}]{Environment.NewLine}";
            }

            lblTotalCount.Text = $"总共：{stationTable.Rows.Count}";
            txtStationList.Text = stationInfo;
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 保存服务器配置设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveServerConfig_Click(object sender, EventArgs e)
        {
            // Bulletins 
            // ID BaseName WorkshopName ProductLineName ProductLineDescription ProductLlineAttributes Posits
            if (txt_BaseName.Text == String.Empty || txt_WorkshopName.Text == String.Empty || txt_PLineName.Text == String.Empty
                || txt_ServerPort.Text == String.Empty || txt_ServerIP.Text == String.Empty)
            {
                MessageBox.Show("当前界面内容均为必填项、请先填写完善");
                return;
            }

            dbHelper = new MDBHelper(databasePath);
            DataTable BulletinsTable = dbHelper.Find("select * from Bulletins where ID = '1'");
            if (BulletinsTable.Rows.Count > 0)
            {
                string updateSql = $@" UPDATE [Bulletins] 
                                        SET [BaseName] = '{txt_BaseName.Text}',
                                            [WorkshopName] = '{txt_WorkshopName.Text}',
                                            [ProductLineName] = '{txt_PLineName.Text}',
                                            [ProductLineDescription] = '{txt_PLineDescription.Text}',
                                            [ProductLlineAttributes] = '{cboPLineAttribute.Text}',
                                            [IP] = '{txt_ServerIP.Text}',
                                            [Port] = '{txt_ServerPort.Text}',
                                            [Posits] = '{lblDatabasePath.Text}'
                                        WHERE [ID] = '1' ";
                var result = dbHelper.Change(updateSql);

                if (result)
                {
                    tbx_IP.Text = txt_ServerIP.Text;
                    tbx_port.Text = txt_ServerPort.Text;
                    MessageBox.Show("保存成功");
                }
            }
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 清空工位表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCleanStationList_Click(object sender, EventArgs e)
        {
            dbHelper = new MDBHelper(databasePath);
            bool bl = dbHelper.Del(" DELETE FROM [product] ");
            bool b2 = dbHelper.Del(" DELETE FROM [Model]   ");
            bool b3 = dbHelper.Del(" DELETE FROM [ModPros] ");

            if (bl == true)
            {
                txtStationList.Text = "";
                MessageBox.Show("清空成功");
            }
            lblTotalCount.Text = "总共：0";
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 修改最后工位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifyFinalStation_Click(object sender, EventArgs e)
        {
            //FinishedName
            //WorkName
            //DegreesSev
            if (txt_GenarateSpeed.Text == String.Empty)
            {
                MessageBox.Show("当前界面内容均为必填项、请先填写完善");
                return;
            }

            dbHelper = new MDBHelper(databasePath);
            DataTable BulletinsTable = dbHelper.Find("select * from Bulletins where ID = '1'");
            if (BulletinsTable.Rows.Count > 0)
            {
                /*string sql = "update [Bulletins] set [WorkName]='" + txtFinalStation.Text + "'" + ",[FinishedName]='" + chkNameToID.Checked + "'" +
                              ",[DegreesSev]='" + txt_GenarateSpeed.Text + "'" + " where [ID] = '1'";*/

                string updateSql = $@" UPDATE [Bulletins]
                                       SET [WorkName] = '{txtFinalStation.Text}',
                                       [FinishedName] = '{chkNameToID.Checked}',
                                       [DegreesSev] = '{txt_GenarateSpeed.Text}'
                                       WHERE [ID] = '1' ";

                var result = dbHelper.Change(updateSql);
                if (result)
                {
                    tbx_IP.Text = txt_ServerIP.Text;
                    tbx_port.Text = txt_ServerPort.Text;
                    MessageBox.Show("保存成功");
                }
            }
            dbHelper.CloseConnection();
        }

        #endregion

        #region ------------- 生成产线数据库 -------------

        /// <summary>
        /// 重新生成产线数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RebuildDatabase_Click(object sender, EventArgs e)
        {
            string dbPath = $@"{lblDatabasePath.Text}\{DateTime.Now:Y}产线数据.mdb";
            dbHelper = new MDBHelper();

            if (dbHelper.TryConnectDatabase(dbPath) == false)
            {
                GenerateProductLineDatabase();
                MessageBox.Show("数据库初始成功！");
            }
            else
            {
                MessageBox.Show("该数据库已经存在，请删除数据库在初始化！");
            }
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 生成产线数据库
        /// </summary>
        private void GenerateProductLineDatabase()
        {
            string dbPath = $@"{lblDatabasePath.Text}\{DateTime.Now:Y}产线数据.mdb";
            MDBHelper.CreateAccessDatabase(dbPath);     // 创建数据库文件

            // 创建产线信息表
            StringBuilder field = new StringBuilder(" 基地名称,车间名称,产线名称,产线工位数量,产线描述,产线属性");
            ArrayList arrayList = new ArrayList();
            object[] obj = new object[] { "基地名称", "车间名称", "产线名称", "产线工位数量", "产线描述", "产线属性" };
            arrayList.AddRange(obj);

            if (stationTable.Rows.Count > 0)
            {
                object[] obj1 = new object[stationTable.Rows.Count];
                for (int i = 0; i < stationTable.Rows.Count; i++)
                {
                    field.Append(",");
                    int count = (i + 1);
                    obj1[i] = "工位" + count;
                    field.Append("工位" + count);
                }
                arrayList.AddRange(obj1);
            }
            MDBHelper.CreateMDBTable(dbPath, "产线信息", arrayList);

            // 创建故障信息表
            ArrayList arrayList1 = new ArrayList();
            object[] o1 = new object[] { "故障发生工位", "机台名称", "故障类型", "故障描述", "发生时间", "结束时间", "更新标识" };
            arrayList1.AddRange(o1);
            MDBHelper.CreateMDBTable(dbPath, "故障信息", arrayList1);

            // 创建生产信息表
            ArrayList arrayList2 = new ArrayList();
            object[] obj2 = new object[] {  "工位名称", "当前工单号", "产品条码", "操作人员", "测试时间",
                "测试结果","测试节拍","测试项名称","测试项上限","测试项下限","测试项实际值", "更新标识" };
            arrayList2.AddRange(obj2);
            MDBHelper.CreateMDBTable(dbPath, "生产信息", arrayList2);//new System.Collections.ArrayList(new object[] { "产品", "条码", "测试人", "测试时间","测试结果", "PLC配方", "文件版本","软件版本"}));

            //创建统计信息表
            ArrayList arrayList3 = new ArrayList();
            object[] obj3 = new object[] { "工单号", "成品名称", "工单数量", "完成数量", "完成率", "合格率",
                "整线节拍","线平衡","OEE","直通率","更新时间","更新标识" };
            arrayList3.AddRange(obj3);
            MDBHelper.CreateMDBTable(dbPath, "统计信息", arrayList3);

            // 创建易损件信息表
            ArrayList arrayList4 = new ArrayList();
            object[] obj4 = new object[] { "易损件所在工位", "机台名称", "易损件所在位置", "易损件名称", "易损件理论使用次数",
                "易损件已使用次数","易损件剩余使用次数" };
            arrayList4.AddRange(obj4);
            MDBHelper.CreateMDBTable(dbPath, "易损件信息", arrayList4);

            // 初始化产线信息数据
            dbHelper = new MDBHelper(dbPath);
            DataTable table1 = dbHelper.Find("select * from 产线信息 where ID = 1");
            if (table1.Rows.Count == 0)
            {
                // DateTime now = DateTime.Now;
                StringBuilder str1 = new StringBuilder();
                // str1.Append("'" + "1" + "',");
                str1.Append("'" + txt_BaseName.Text + "',");
                str1.Append("'" + txt_WorkshopName.Text + "',");
                str1.Append("'" + txt_PLineName.Text + "',");
                str1.Append("'" + stationTable.Rows.Count + "',");
                str1.Append("'" + txt_PLineDescription.Text + "',");
                str1.Append("'" + cboPLineAttribute.Text + "'");
                if (stationTable.Rows.Count > 0)
                {
                    for (int i = 0; i < stationTable.Rows.Count; i++)
                    {
                        str1.Append(",");
                        string rst = "  ";
                        rst = stationTable.Rows[i][0].ToString();
                        str1.Append("'" + rst + "'");

                    }
                }

                string sql = "insert into 产线信息 (" + field + ") values (" + str1 + ")";
                bool result = dbHelper.Add(sql.ToString());
                if (result)
                {
                    ShowMsg("数据库生成成功!");
                }
            }
            dbHelper.CloseConnection();
        }

        #endregion

        #region------------- 消息处理 -------------

        private string FormatParsedData(List<string[]> parsedDataList)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("处理数据内容:");

            for (int i = 0; i < parsedDataList.Count; i++)
            {
                sb.AppendLine($"数据组 {i + 1}:");
                string[] dataGroup = parsedDataList[i];

                string groupTitle;
                switch (dataGroup[0])
                {
                    case "0":
                        groupTitle = "工位配置信息";
                        break;
                    case "1":
                        groupTitle = "故障信息";
                        break;
                    case "2":
                        groupTitle = "生产信息";
                        break;
                    case "3":
                        groupTitle = "统计信息";
                        break;
                    case "4":
                        groupTitle = "易损件信息";
                        break;
                    case "5":
                        groupTitle = "工位状态";
                        break;
                    case "6":
                        groupTitle = "日志信息";
                        break;
                    default:
                        groupTitle = "未知类型";
                        break;
                }

                sb.AppendLine($"消息类型: {groupTitle}");

                // 添加具体数据内容
                sb.AppendLine("数据内容: " + string.Join(" | ", dataGroup));
                //sb.AppendLine("----------------------------------------");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 处理来自客户端的数据并存入数据库
        /// </summary>
        /// <param name="receivedData">接收到的原始数据字符串</param>
        /// <param name="sourceIP">数据来源的IP地址</param>
        private void ProcessReceivedData(string receivedData, string sourceIP)
        {
            Invoke(new Action(() =>
            {
                // 用于存储解析后的数据数组
                List<string[]> parsedDataList = new List<string[]>();

                // 判断数据格式：包含加号的按分隔符处理，否则尝试按JSON处理
                if (receivedData.Contains("+"))
                {
                    // 首先按竖线分割数据块
                    string[] dataBlocks = receivedData.Split('|');

                    // 原本逻辑
                    /*for (int i = 0; i < dataBlocks.Length; i++)
                    {
                        string[] dataArray = dataBlocks[i].Split(new char[] { '+' });
                        if (dataArray.Length > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(dataArray[0]))
                            {
                                parsedDataList.Add(dataArray);
                            }
                        }
                    }*/

                    foreach (string block in dataBlocks)
                    {
                        // 再按加号分割每个数据块
                        string[] dataArray = block.Split(new char[] { '+' });
                        if (dataArray.Length > 0 && !string.IsNullOrWhiteSpace(dataArray[0]))
                        {
                            parsedDataList.Add(dataArray);
                        }
                    }
                }
                else
                {
                    try
                    {
                        // JSON格式处理
                        using (JsonTextReader reader = new JsonTextReader(new StringReader(receivedData)))
                        {
                            reader.SupportMultipleContent = true;
                            while (reader.Read())
                            {
                                JsonSerializer serializer = new JsonSerializer();
                                string[] dateJson = serializer.Deserialize<string[]>(reader);
                                parsedDataList.Add(dateJson);
                            }
                        }// 作用域结束时自动调用 Dispose

                        /*JsonTextReader reader = new JsonTextReader(new StringReader(strdata));
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                break;
                            }
                            JsonSerializer serializer = new JsonSerializer();
                            string[] dateJson = serializer.Deserialize<string[]>(reader);
                            listjson.Add(dateJson);
                        }
                        // AdateJson = dateJson;*/
                    }
                    catch (JsonReaderException)
                    {
                        // JSON解析失败，作为普通字符串处理
                        parsedDataList.Add(new string[] { receivedData });
                    }
                }

                string formattedMsg = FormatParsedData(parsedDataList);
                parsedMsg.Trace($"客户端[{sourceIP}]\n{formattedMsg}");

                // 处理解析后的每组数据
                foreach (var dataGroup in parsedDataList)
                {
                    string[] processedData = new string[] { };
                    processedData = dataGroup;

                    // 清理数据：去除空格，将null转换为空字符串
                    for (int i = 0; i < processedData.Length; i++)
                    {
                        processedData[i] = processedData[i].Trim();

                        // 原先逻辑：
                        /*if (string.IsNullOrWhiteSpace(processedData[i]))
                        {
                            processedData[i] = "";
                        }
                        else if (processedData[i].Equals("null"))
                        {
                            processedData[i] = "";
                        }*/

                        if (string.IsNullOrWhiteSpace(processedData[i]) ||
                            processedData[i].Equals("null"))
                        {
                            processedData[i] = string.Empty;
                        }
                    }
                    mdbABC.EnsureConnectionOpened();

                    // 根据数据类型进行相应处理
                    // 0: 新工位配置
                    // 1: 故障信息
                    // 1+故障所在工位+机台名称+故障状态+故障的描述+触发故障的开始时间 
                    // 1+故障所在工位+机台名称+故障状态+故障的描述+触发故障的结束时间
                    // 2: 生产信息
                    // 2+工位名称+当前工单号+产品条码+操作人员+测试时间+测试结果+测试节拍+测试项名称+测试项上限+测试项下限+测式项实际值
                    // 3: 统计信息
                    // 3+工位+工单数量+完成数量+完成率+合格率+整体节拍+生产产品数量（总数）+ 工序时间+利用时间+负荷时间
                    // 4: 易损件信息
                    // 4+易损件所在工位+机台名称+ 易损件所在位置+易损件名称+易损件理论使用次数易损件已使用次数
                    // 5: 工位状态
                    // 6: 日志信息

                    switch (processedData[0])
                    {
                        case "0":   // 工位配置信息处理
                            // 原先逻辑：
                            /*DataRow[] existingStations = stationTable.Select("Model = '" + processedData[1] + "'");

                            if (existingStations.Length == 0)
                            {
                                try
                                {
                                    int stationCount = stationTable.Rows.Count + 1;

                                    // 创建数据库帮助类实例
                                    mdbDatas dbHelper = new mdbDatas(path4);//conndnew connt

                                    // 插入新工位记录
                                    string insertSql = "insert into [Model] ([Model],[Mname]) values ('"
                                    + processedData[1] + "','" + "工位" + stationCount + "')";
                                    dbHelper.Add(insertSql.ToString());

                                    // 关闭连接
                                    dbHelper.CloseConnection();

                                    SystBoardnt(); // 重新加载工位信息

                                    // 更新数据库结构和数据
                                    string alterTableSql = "ALTER TABLE [产线信息] ADD " + "工位" + stationCount + "  varchar(200)";
                                    mdbABC.Add(alterTableSql);

                                    string updateSql = "update  [产线信息] set 产线工位数量 ='" + stationCount +
                                    "',[工位" + stationCount + "]='" + processedData[1] + "'where ID=1 ";
                                    mdbABC.Add(updateSql.ToString());
                                }
                                catch
                                {
                                }
                                //button8_Click(null, null);    // 刷新状态
                            }*/
                            ProcessStationConfig(processedData);
                            break;
                        case "1":   // 故障信息处理
                            MapStationNameWithStationID(processedData);
                            ProcessFaultsTable(processedData);
                            break;
                        case "2":   // 生产信息处理
                            MapStationNameWithStationID(processedData);
                            ProcessProductTable(processedData);
                            break;
                        case "3":   // 统计信息
                            ProcessProductionStatisticsAsync(processedData);
                            break;
                        case "4":   // 易损件信息
                            MapStationNameWithStationID(processedData);
                            ProcessConsumablePartsInfo(processedData);
                            break;
                        case "5":   // 工位状态处理
                            AddStationName(sourceIP, processedData);
                            break;
                        case "6":   // 日志信息处理
                            LogMsg(processedData[1]);
                            break;
                    }
                }
            }));
        }

        /// <summary>
        /// 处理工位配置信息
        /// </summary>
        /// <remarks>
        /// configData[0] -> 0 ; 
        /// configData[1] -> 工位名称;
        /// </remarks>
        private void ProcessStationConfig(string[] configData)  // 新增方法，原来在case "0"中的逻辑
        {
            DataRow[] existingStations = stationTable.Select($"Model = '{configData[1]}'");
            if (existingStations.Length == 0)
            {
                try
                {
                    int stationCount = stationTable.Rows.Count + 1;
                    // 创建数据库帮助类实例
                    MDBHelper dbHelper = new MDBHelper(databasePath);

                    // 插入新工位记录
                    string insertSql = $"insert into [Model] ([Model],[Mname]) values ('{configData[1]}', '工位{stationCount}')";
                    dbHelper.Add(insertSql);

                    // 关闭连接
                    dbHelper.CloseConnection();

                    // 重新加载工位信息
                    LoadServerConfig();

                    // 更新数据库结构和数据
                    string alterTableSql = $"ALTER TABLE [产线信息] ADD 工位{stationCount} varchar(200)";
                    mdbABC.Add(alterTableSql);

                    string updateSql = $"update [产线信息] set 产线工位数量 ='{stationCount}', " +
                                     $"[工位{stationCount}]='{configData[1]}' where ID=1";
                    mdbABC.Add(updateSql);
                }
                catch
                {
                    // 记录错误日志或处理异常
                }
            }
        }

        /// <summary>
        /// 处理工位名称与工位ID的映射，将工位名称转换为对应的工位ID；
        /// </summary>
        /// <param name="processedData"></param>
        private void MapStationNameWithStationID(string[] processedData)
        {
            // [Model] ([Model],[Mname]
            if (chkNameToID.Checked)
            {
                DataRow[] rows = stationTable.Select($" Model = '{processedData[1]}' ");
                if (rows.Length > 0)
                {
                    processedData[1] = rows[0]["Mname"].ToString();
                }
            }
        }

        /// <summary>
        /// 根据客户端IP添加对应的机台名称
        /// </summary>
        /// <param name="sourceIP"></param>
        /// <param name="dataArray"></param>
        private void AddStationName(string sourceIP, string[] dataArray)
        {
            DataRow[] rows = clientInfoTable.Select($" 名称 = '{dataArray[1]}' ");
            if (rows.Length == 0)
            {
                clientInfoTable.Rows.Add(dataArray[1], sourceIP, DateTime.Now.ToString(), "成功");
            }
            else
            {
                // productTable[dateshuzu[1]].;
                // Mname as 名称, IP as IP,conndnew as 时间 ,connt as 状态
                if (rows[0]["名称"].Equals(dataArray[1]))
                {
                    rows[0]["IP"] = sourceIP;
                    rows[0]["时间"] = DateTime.Now;
                    rows[0]["状态"] = "成功";
                }
            }

            dgvClientInfo.DataSource = clientInfoTable;
        }

        /// <summary>
        /// 处理易损件信息的更新或新增
        /// </summary>
        /// <param name="processedData">易损件数据数组：
        /// [1] - 易损件所在工位
        /// [2] - 机台名称
        /// [3] - 易损件所在位置
        /// [4] - 易损件名称
        /// [5] - 易损件理论使用次数
        /// [6] - 易损件已使用次数
        /// </param>
        private void ProcessConsumablePartsInfo(string[] processedData)
        {
            // 4+易损件所在工位+机台名称+ 易损件所在位置+易损件名称+易损件理论使用次数+易损件已使用次数
            // "易损件所在工位", "机台名称", "易损件所在位置", "易损件名称", "易损件理论使用次数",
            // "易损件已使用次数","易损件剩余使用次数"

            int theoryCount = 0;
            int usedCount = 0;
            int remainingCount = 0;

            // 计算剩余使用次数 = 理论使用次数 - 已使用次数
            if (int.TryParse(processedData[5], out theoryCount) && int.TryParse(processedData[6], out usedCount))
            {
                remainingCount = theoryCount - usedCount;
            }

            //DataTable consumablePartsTable = mdbABC.Find("select * from 易损件信息 where 易损件所在工位='" + processedData[1] + "'and 机台名称='"
            //    + processedData[2] + "'and 易损件所在位置='" + processedData[3] + "'and 易损件名称='" + processedData[4] + "'");

            DataTable consumablePartsTable = mdbABC.Find($"SELECT * FROM 易损件信息 WHERE 易损件所在工位='{processedData[1]}' " +
                $"AND 机台名称='{processedData[2]}' " +
                $"AND 易损件所在位置='{processedData[3]}' " +
                $"AND 易损件名称='{processedData[4]}'");

            // 存在则更新使用次数信息
            if (consumablePartsTable.Rows.Count > 0)
            {
                //string updateSql = "update [易损件信息] set [易损件已使用次数]='" + usedCount + "'," +
                //    "[易损件理论使用次数]='" + theoryCount + "', [易损件剩余使用次数] = '" + remainingCount + "'" +
                //    " where [机台名称] = '" + processedData[2] + "'and 易损件名称='" + processedData[4] + "'";

                string updateSql = $@"UPDATE [易损件信息] 
                        SET [易损件已使用次数]='{usedCount}',
                            [易损件理论使用次数]='{theoryCount}', 
                            [易损件剩余使用次数]='{remainingCount}'
                        WHERE [机台名称]='{processedData[2]}' 
                        AND 易损件名称='{processedData[4]}'";

                var result = mdbABC.Change(updateSql);
                if (result == true)
                {
                    this.BeginInvoke(ShowMsgAction,
                        $"工位：{processedData[1]}机台：{processedData[2]}易损件信息更新：{processedData[3]}{processedData[4]}");
                }
            }
            // 不存在则新增易损件记录
            else
            {
                /*string insertSql = "insert into 易损件信息 (易损件所在工位, 机台名称, 易损件所在位置, 易损件名称, " +
                    "易损件理论使用次数,易损件已使用次数,易损件剩余使用次数)" +
                    " values ('" + processedData[1] + "','" + processedData[2] + "','" + processedData[3] + "','" + processedData[4] + "','"
                    + theoryCount + "','" + usedCount + "','" + remainingCount + "')";*/

                string insertSql = $@"INSERT INTO 易损件信息 (
                    易损件所在工位, 
                    机台名称, 
                    易损件所在位置, 
                    易损件名称, 
                    易损件理论使用次数,
                    易损件已使用次数,
                    易损件剩余使用次数
                ) values (
                    '{processedData[1]}',
                    '{processedData[2]}',
                    '{processedData[3]}',
                    '{processedData[4]}',
                    '{theoryCount}',
                    '{usedCount}',
                    '{remainingCount}'
                )";

                bool isAddSuccessful = mdbABC.Add(insertSql);
                if (isAddSuccessful == true)
                {
                    this.BeginInvoke(ShowMsgAction,
                        $"工位：{processedData[1]}机台：{processedData[2]}新增易损件信息:{processedData[3]}{processedData[4]}");
                }
            }
        }

        /// <summary>
        /// 生产工位的列表
        /// </summary>
        List<Dictionary<string, List<string>>> productionStationList = new List<Dictionary<string, List<string>>>();

        /// <summary>
        /// 处理生产统计信息并更新数据库
        /// </summary>
        /// <param name="productionData">生产数据数组:
        /// [0] - 类型标识符
        /// [1] - 工位名称
        /// [2] - 工单号
        /// [3] - 工单数量
        /// [4] - 完成数量
        /// [5] - 完成率
        /// [6] - 合格率
        /// [7] - 整体节拍
        /// [8] - 生产产品总数
        /// [9] - 工序时间
        /// [10] - 利用时间
        /// [11] - 负荷时间
        /// [12] - 直通率
        /// [13] - 成品名称
        /// </param>
        private async void ProcessProductionStatisticsAsync(string[] productionData)
        {
            // 验证最后工位名称是否输入
            if (txtFinalStation.Text.Trim().Length == 0)
            {
                this.BeginInvoke(ShowMsgAction, "输入最后一个机台！！！");
                return;
            }

            // 首次添加生产统计数据
            if (productionStationList.Count == 0)
            {
                // 即提取productionData[1]中所包含的工位名称，并建立工位名称与剩下索引值之间的映射
                string stationName = productionData[1];
                List<string> productionStatsticInfoList = ConvertToProductionStatsList(productionData);

                // stationDataMap：工位名称与对应生产统计数据的映射；
                var stationDataMap = new Dictionary<string, List<string>>() // 集合初始化器
                {
                    {stationName, productionStatsticInfoList}
                };

                // Fix bug: Add stationDataMap to productionSationList
                productionStationList.Add(stationDataMap);
            }
            else
            {
                // 遍历现有生产统计数据
                for (int i = 0; i < productionStationList.Count; i++)
                {
                    // currentStationMap：表示当前处理的机台名称与对应生产统计数据的映射
                    var currentStationDataMap = new Dictionary<string, List<string>>();
                    currentStationDataMap = productionStationList[i];

                    // 如果找到最终工位，计算统计信息
                    if (currentStationDataMap.ContainsKey(txtFinalStation.Text))
                    {
                        // 计算生产线性能指标
                        bool isFirstStation = true;
                        double count = currentStationDataMap.Count; // 工位数量
                        double processingTime = 0;                  // 处理时间（工序时间）
                        double totalProcessingTime = 0;             // 总处理时间（工序时间总和）
                        double bottleneckTime = 0;                  // 瓶颈时间;
                        double yieldRate = 0;                       // 直通率
                        double totalYieldRate = 1;                  // 总直通率（直通率乘积）
                        double utilizationTime = 0;                 // 利用时间
                        double loadTime = 0;                        // 负荷时间
                        //double timenum2 = 0;                      // 总利用时间（利用时间总和）
                        //double timenum4 = 0;                      // 总负荷时间（负荷时间总和）

                        // 遍历每个工位对应的生产统计数据
                        foreach (List<string> statisticsInfoList in currentStationDataMap.Values)
                        {
                            // statisticsInfoList[i]:
                            // [0] - 工单号         [1] - 成品名称         [2] - 工单数量
                            // [3] - 完成数量       [4] - 完成率           [5] - 合格率
                            // [6] - 整体节拍       [7] - 工序时间         [8] - 利用时间
                            // [9] - 负荷时间       [10] - 生产产品数量    [11] - 直通率

                            // 处理工序时间
                            double.TryParse(statisticsInfoList[7], out processingTime);    // 工序时间
                            totalProcessingTime += processingTime;

                            // 计算瓶颈时间
                            if (isFirstStation)
                            {
                                isFirstStation = false;
                                double.TryParse(statisticsInfoList[7], out bottleneckTime);
                            }

                            if (bottleneckTime > processingTime)    // 计算最小的
                            {
                                //double.TryParse(valuelist[7], out num1);//工序
                                bottleneckTime = processingTime;
                            }

                            // 计算直通率
                            double.TryParse(statisticsInfoList[11], out yieldRate);
                            yieldRate /= 100;

                            if (yieldRate != 0) // 直通率乘积
                            {
                                totalYieldRate *= yieldRate;
                            }

                        }

                        // 获取最终工位数据
                        List<string> productionStatsticInfoList = currentStationDataMap[txtFinalStation.Text];

                        string workOrder = productionStatsticInfoList[0];   // 工单号

                        // 更新工单完成情况
                        StatInformaAS statInformaAS = await BydWorkCom.BydWorkStatisticsAsync(workOrder, "");

                        if (statInformaAS.IsHandle && statInformaAS.IsProcess)
                        {
                            productionStatsticInfoList[2] = statInformaAS.ORDER_NUM;                        // 工单数量
                            productionStatsticInfoList[3] = statInformaAS.COMP_NUM;                         // 完成数量
                            productionStatsticInfoList[4] = statInformaAS.COMP_RATE.TrimEnd('%');  // 完成率
                        }

                        string insertSql = "INSERT INTO 统计信息 (工单号, 成品名称, 工单数量, 完成数量, 完成率, 合格率,整线节拍" +
                                        ",线平衡,OEE,直通率,更新时间,更新标识 )  VALUES ('";

                        // [0] - 工单号         [1] - 成品名称         [2] - 工单数量
                        // [3] - 完成数量       [4] - 完成率           [5] - 合格率
                        // [6] - 整体节拍       [7] - 工序时间         [8] - 利用时间
                        // [9] - 负荷时间       [10] - 产品数量    [11] - 直通率

                        // 计算完成率
                        double completeRate = 0;
                        double.TryParse(productionStatsticInfoList[4], out completeRate);
                        productionStatsticInfoList[4] = (completeRate / 100).ToString();

                        // 计算合格率
                        double passRate = 0;
                        double.TryParse(productionStatsticInfoList[5], out passRate);
                        productionStatsticInfoList[5] = (passRate / 100).ToString();

                        // 插入工单号、成品名称、工单数量、完成数量、完成率、合格率、整体节拍
                        insertSql += productionStatsticInfoList[0] + "','";
                        insertSql += productionStatsticInfoList[1] + "','";
                        insertSql += productionStatsticInfoList[2] + "','";
                        insertSql += productionStatsticInfoList[3] + "','";
                        insertSql += productionStatsticInfoList[4] + "','";
                        insertSql += productionStatsticInfoList[5] + "','";
                        insertSql += productionStatsticInfoList[6] + "','";

                        // 计算并插入线平衡
                        if (totalProcessingTime != 0 && bottleneckTime != 0 && count != 0)
                        {
                            double lineBalance = totalProcessingTime / (count * bottleneckTime);
                            insertSql += lineBalance.ToString("0.0000") + "','";
                        }
                        else
                        {
                            insertSql += "0" + "','";
                        }

                        string OEE = "";
                        double.TryParse(productionStatsticInfoList[8], out utilizationTime);    // 利用时间
                        double.TryParse(productionStatsticInfoList[9], out loadTime);           // 负荷时间

                        double rowcount9 = 0;
                        double.TryParse(productionStatsticInfoList[10], out rowcount9);         // 生产产品数量（总数）

                        double textcount9 = 0;
                        double.TryParse(txt_GenarateSpeed.Text, out textcount9);                      // 设计数度

                        double numhg = 0;
                        double.TryParse(productionStatsticInfoList[5], out numhg);              // 合格率

                        // 计算OEE
                        if (utilizationTime != 0 && loadTime != 0 && textcount9 != 0 && textcount9 != 0 && numhg != 0)
                        {
                            double OEE1 = ((utilizationTime / loadTime) * (rowcount9 / (utilizationTime * textcount9)) * numhg);
                            OEE = OEE1.ToString("0.0000");
                        }
                        else
                        {
                            OEE = "0";
                        }

                        // 插入OEE、直通率、更新时间、更新标识
                        insertSql += OEE + "','";
                        insertSql += totalYieldRate.ToString("0.0000") + "','";
                        insertSql += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','";
                        insertSql += "F" + "')";

                        // 执行数据插入
                        var result3_1 = mdbABC.Add(insertSql);
                        if (result3_1)
                        {
                            this.BeginInvoke(ShowMsgAction, "添加一条统计信息");
                            productionStationList.Remove(currentStationDataMap);
                        }

                    }
                    // 处理新工位数据
                    else if (currentStationDataMap.ContainsKey(productionData[1]))
                    {
                        Dictionary<string, List<string>> dicmapA = new Dictionary<string, List<string>>();
                        dicmapA.Add(productionData[1], ConvertToProductionStatsList(productionData));
                        productionStationList.Add(dicmapA);
                        break;
                    }
                    else
                    {
                        currentStationDataMap.Add(productionData[1], ConvertToProductionStatsList(productionData));
                        productionStationList[i] = currentStationDataMap;
                        break;
                    }
                }
            }

        }

        /// <summary>
        /// 将生产数据数组转换为生产统计信息列表
        /// </summary>
        /// <param name="productionData">生产数据数组:
        /// [0] - 数据类型标识符
        /// [1] - 机台名称
        /// [2] - 工单号
        /// [3] - 工单数量
        /// [4] - 完成数量
        /// [5] - 完成率
        /// [6] - 合格率
        /// [7] - 整体节拍
        /// [8] - 生产产品总数
        /// [9] - 工序时间
        /// [10] - 利用时间
        /// [11] - 负荷时间
        /// [12] - 直通率
        /// [13] - 成品名称
        /// </param>
        /// <returns>包含生产统计信息的有序列表</returns>
        private List<string> ConvertToProductionStatsList(string[] productionData)
        {
            // productionData[]：
            // 3 + 机台名称1 + 工单号2    + 工单数量3  + 完成数量4 + 完成率5    + 合格率6 + 整体节拍7 + 生产产品数量（总数）8
            //   + 工序时间9 + 利用时间10 + 负荷时间11 +  直通率12 + 成品名称13

            #region 

            // 初始化生产统计信息列表
            var productionStats = new List<string>();

            // [0] - 工单号         [1] - 成品名称         [2] - 工单数量
            // [3] - 完成数量       [4] - 完成率           [5] - 合格率
            // [6] - 整体节拍       [7] - 工序时间         [8] - 利用时间
            // [9] - 负荷时间       [10] - 生产产品数量    [11] - 直通率

            #endregion

            // 按固定顺序添加生产统计数据
            productionStats.Add(productionData[2]);     // 工单号
            productionStats.Add(productionData[13]);    // 成品名称
            productionStats.Add(productionData[3]);     // 工单数量
            productionStats.Add(productionData[4]);     // 完成数量
            productionStats.Add(productionData[5]);     // 完成率
            productionStats.Add(productionData[6]);     // 合格率
            productionStats.Add(productionData[7]);     // 整体节拍
            productionStats.Add(productionData[9]);     // 工序时间
            productionStats.Add(productionData[10]);    // 利用时间
            productionStats.Add(productionData[11]);    // 负荷时间
            productionStats.Add(productionData[8]);     // 生产产品数量（总数）
            productionStats.Add(productionData[12]);    // 直通率

            return productionStats;
        }

        /// <summary>
        /// 生成信息表
        /// </summary>
        /// <param name="processedData"></param>
        /// <param name="conn"></param>
        private async void ProcessProductTable(string[] processedData)
        {
            await Task.Run(() =>
            {
                // 2+工位名称+当前工单号+产品条码+操作人员+则式时间+则试结果+ 则试节拍+则试项名称+ 则试项上限+则试项下限+测式项实际值
                /*string sql2 = "insert into 生产信息 (工位名称 ,当前工单号, 产品条码,操作人员, " +
                    "测试时间,测试结果,测试节拍,测试项名称," +
                    "测试项上限,测试项下限,测试项实际值, 更新标识)" +
                       " values ('" + processedData[1] + "','" + processedData[2] + "','" + processedData[3] + "','" + processedData[4] + "','"
                   + processedData[5] + "','" + processedData[6] + "','" + processedData[7] + "','" + processedData[8] + "','"
                   + processedData[9] + "','" + processedData[10] + "','" + processedData[11] + "','" + "F" + "')";*/
                string insertSql = $@"INSERT INTO 生产信息 (工位名称,当前工单号,产品条码,操作人员,测试时间,
                                        测试结果,测试节拍,测试项名称,测试项上限,测试项下限,测试项实际值,更新标识) 
                                    VALUES ('{processedData[1]}',
                                            '{processedData[2]}',
                                            '{processedData[3]}',
                                            '{processedData[4]}',
                                            '{processedData[5]}',
                                            '{processedData[6]}',
                                            '{processedData[7]}',
                                            '{processedData[8]}',
                                            '{processedData[9]}',
                                            '{processedData[10]}',
                                            '{processedData[11]}',
                                            'F')";
                var result2 = mdbABC.Add(insertSql.ToString());
            });
        }

        /// <summary>
        /// 故障信息表处理方法，
        /// 处理设备故障的开始和结束记录
        /// </summary>
        /// <param name="faultData">故障信息数组：
        /// [1] - 故障发生工位
        /// [2] - 机台名称
        /// [3] - 故障类型
        /// [4] - 故障描述
        /// [5] - 发生时间
        /// [6] - 结束时间（可选）
        /// </param>
        private void ProcessFaultsTable(string[] faultData)
        {
            // 1+故障所在工位+机台名称+ 故障状态+故障描述+触发故障的开始时间 
            // 1+故障所在工位+机台名称+故障的描述+触发故障的结束时间
            /* DataTable faultsTable = mdbABC.Find("select * from 故障信息 where 故障发生工位='" + processedData[1] + "'and 机台名称='"
                 + processedData[2] + "'and 故障描述='" + processedData[4] + "'" + "and 发生时间='" + processedData[5] + "'");*/

            // 1. 查询是否存在匹配的故障记录
            DataTable faultsTable = mdbABC.Find($@"SELECT * FROM [故障信息] 
                WHERE [故障发生工位]='{faultData[1]}' 
                AND [机台名称]='{faultData[2]}' 
                AND [故障描述]='{faultData[4]}' 
                AND [发生时间]='{faultData[5]}'");

            // 2. 如果存在记录，说明这是一条故障结束的信息
            if (faultsTable.Rows.Count > 0)
            {
                DateTime endDataTime = new DateTime();

                // 检查是否有结束时间参数
                if (faultData.Length > 6)
                {
                    // 验证结束时间格式，如果无效则使用当前时间
                    bool isEndTimeValid = DateTime.TryParse(faultData[6], out endDataTime);
                    if (isEndTimeValid == false)
                    {
                        faultData[6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    // 更新故障记录的结束时间和更新标识
                    /*string updateSql = "update [故障信息] set [结束时间]='" + processedData[6] + "',更新标识='F'" + " where 故障发生工位='" + processedData[1] + "'and 机台名称='"
                    + processedData[2] + "'and 故障描述='" + processedData[4] + "'" + "and 发生时间='" + processedData[5] + "'";*/
                    string updateSql = $@"UPDATE [故障信息] 
                        SET [结束时间]='{faultData[6]}',
                            [更新标识]='F'
                      WHERE [故障发生工位]='{faultData[1]}' 
                        AND [机台名称]='{faultData[2]}'
                        AND [故障描述]='{faultData[4]}'
                        AND [发生时间]='{faultData[5]}'";

                    var isUpdateSuccessful = mdbABC.Change(updateSql);
                    if (isUpdateSuccessful)
                    {
                        // 显示更新成功消息
                        /*this.BeginInvoke(ShowMsgAction, "工位：" + processedData[1] + "机台：" + processedData[2] +
                            "故障停止信息：" + processedData[3] + processedData[6]);*/
                        this.BeginInvoke(ShowMsgAction,
                            $"工位：{faultData[1]}机台：{faultData[2]}故障停止信息：{faultData[3]}{faultData[6]}");
                    }
                }
            }
            // 3. 如果不存在记录，说明这是一条新的故障记录
            else
            {
                string endTime = "";
                DateTime dt = new DateTime();

                // 处理可能存在的结束时间
                if (faultData.Length > 6)
                {
                    bool redtime1 = DateTime.TryParse(faultData[6], out dt);
                    if (redtime1 == false)
                    {
                        faultData[6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    endTime = faultData[6];
                }

                // 验证故障发生时间的有效性
                DateTime startDateTime = new DateTime();
                bool isParseSuccessful = DateTime.TryParse(faultData[5], out startDateTime);
                if (isParseSuccessful)
                {
                    // 插入新的故障记录
                    /*string insertSql = "insert into 故障信息 (故障发生工位, 机台名称, 故障类型, 故障描述, 发生时间, 结束时间, 更新标识)" +
                        " values ('" + processedData[1] + "','" + processedData[2] + "','" + processedData[3] + "','" + processedData[4] + "','"
                        + processedData[5] + "','" + endTime + "','" + "F" + "')";*/
                    string insertSql = $@"INSERT INTO [故障信息] (
                        [故障发生工位], 
                        [机台名称], 
                        [故障类型], 
                        [故障描述], 
                        [发生时间], 
                        [结束时间], 
                        [更新标识]
                        ) VALUES (
                        '{faultData[1]}',
                        '{faultData[2]}',
                        '{faultData[3]}',
                        '{faultData[4]}',
                        '{faultData[5]}',
                        '{endTime}',
                        'F'
                        )";

                    bool isInsertSuccessful = mdbABC.Add(insertSql);
                    if (isInsertSuccessful)
                    {
                        // 显示插入成功消息
                        this.BeginInvoke(ShowMsgAction,
                        $"工位：{faultData[1]}机台：{faultData[2]}故障发生信息：{faultData[3]}{faultData[4]}{faultData[5]}");
                    }
                }
            }
        }

        #endregion

        #region------------- 产品型号 -------------

        /// <summary>
        /// 创建产品型号表格
        /// </summary>
        private void InitializeProductModelBoard()
        {
            BtnFreshModelBoard_Click(null, null);

            DataGridViewButtonColumn btnColumnSave = new DataGridViewButtonColumn();
            btnColumnSave.HeaderText = "操作";
            btnColumnSave.Text = "保存";
            btnColumnSave.Name = "SaveOperation";
            btnColumnSave.DefaultCellStyle.NullValue = "保存";
            dgvPModelSettings.Columns.Add(btnColumnSave);

            DataGridViewButtonColumn btnColumnDel = new DataGridViewButtonColumn();
            btnColumnDel.HeaderText = "操作";
            btnColumnDel.Name = "DeleteOperation";
            btnColumnDel.DefaultCellStyle.NullValue = "删除";
            dgvPModelSettings.Columns.Add(btnColumnDel);
        }

        /// <summary>
        /// 刷新产品型号面板
        /// </summary>
        private void BtnFreshModelBoard_Click(object sender, EventArgs e)
        {
            dbHelper = new MDBHelper(databasePath);

            // Codes  ID CName
            DataTable PModelTable = dbHelper.Find("select ID as 编号 , Name as 型号 from Codes");
            dgvPModelSettings.DataSource = PModelTable;
            cboProductModel.DataSource = PModelTable;
            cboProductModel.DisplayMember = "型号";
            cboProductModel.ValueMember = "编号";

            dbHelper.CloseConnection();
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Codes  ID CName
            if (e.RowIndex == -1) return;

            // 删除
            if (dgvPModelSettings.Columns[e.ColumnIndex].Name == "DeleteOperation")
            {
                dbHelper = new MDBHelper(databasePath);

                string currentID = this.dgvPModelSettings.Rows[e.RowIndex].Cells[2].Value.ToString();
                bool result = dbHelper.Del($" DELETE FROM [Codes] WHERE [ID] = '{currentID}' ");
                if (result == true)
                {
                    MessageBox.Show("删除成功");
                }

                dbHelper.CloseConnection();
                BtnFreshModelBoard_Click(null, null);
            }
            // 保存
            if (dgvPModelSettings.Columns[e.ColumnIndex].Name == "SaveOperation")
            {
                string ID = this.dgvPModelSettings.Rows[e.RowIndex].Cells[2].Value.ToString();
                string productModel = this.dgvPModelSettings.Rows[e.RowIndex].Cells[3].Value.ToString();

                if (string.IsNullOrWhiteSpace(ID))
                {
                    MessageBox.Show("编号不能为空！");
                    return;
                }

                dbHelper = new MDBHelper(databasePath);

                DataTable table1 = dbHelper.Find($"select * from Codes where [ID] = '{ID}'");
                if (table1.Rows.Count > 0)
                {
                    string updateSql = $" UPDATE [Codes] SET [Name]='{productModel}' where [ID] = '{ID}' ";
                    var result = dbHelper.Change(updateSql);
                    if (result == true)
                    {
                        MessageBox.Show("修改成功");
                    }
                }
                else
                {
                    string insertSql = $"INSERT INTO Codes ([ID],[Name]) VALUES ('{ID}', '{productModel}')";
                    bool result = dbHelper.Add(insertSql.ToString());
                    if (result == true)
                    {
                        MessageBox.Show("新增成功");
                    }
                }

                dbHelper.CloseConnection();
                BtnFreshModelBoard_Click(null, null);
            }
        }

        #endregion

        /// <summary>
        /// 选择本地文件存放路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChangeDatabasePath(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.lblDatabasePath.Text = path.SelectedPath;
        }

        Dictionary<string, Socket> clientList = new Dictionary<string, Socket>();

        /// <summary>
        /// 启动Socket服务器并监听客户端连接
        /// </summary>
        public void StartSocketServer()
        {
            try
            {
                // 创建TCP服务器Socket
                Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // 解析IP地址和端口
                IPAddress serverIP = IPAddress.Parse(this.tbx_IP.Text);
                int serverPort = Convert.ToInt32(tbx_port.Text);
                IPEndPoint serverEndPoint = new IPEndPoint(serverIP, serverPort);

                // 更新UI显示的IP地址
                this.tbx_IP.Text = serverIP.ToString();

                // 绑定端口并开始监听
                serverSocket.Bind(serverEndPoint);
                serverSocket.Listen(100);  // 最大允许100个连接请求排队
                IsServerStart = true;
                ShowMsg("信息: 服务器监听启动成功!");

                // 启动异步任务处理客户端连接
                Task.Factory.StartNew(() =>
                {
                    while (IsServerStart)
                    {
                        // 等待并接受客户端连接
                        Socket clientSocket = serverSocket.Accept();
                        if (clientSocket != null)
                        {
                            // 获取客户端的连接信息
                            string clientInfo = clientSocket.RemoteEndPoint.ToString();
                            // 将客户端连接添加到连接列表
                            clientList.Add(clientInfo, clientSocket);

                            // 开始接收该客户端的消息
                            ReceiveMessage(clientSocket);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ShowMsg($"服务器启动失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LaunchServer(object sender, EventArgs e)
        {
            //// this.tbx_IP.Text = "127.0.0.2";
            //try
            //{
            //    Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //    IPAddress ip = IPAddress.Parse(this.tbx_IP.Text);
            //    //创建对象端口
            //    IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(tbx_port.Text));
            //    this.tbx_IP.Text = ip.ToString();
            //    socketWatch.Bind(point);//绑定端口号
            //    ShowMsg("信息:监听成功!");
            //    socketWatch.Listen(100);//允许连接的客户端数量
            //                            //创建监听线程
            //    Thread thread = new Thread(Listen);
            //    thread.IsBackground = true;
            //    thread.Start(socketWatch);
            //    IsStart = true;


            //    // ShowBtnState();
            //}
            //catch (Exception ex)
            //{
            //    ShowMsg("错误信息:"+ex);
            //}

        }

        /// <summary>
        /// 停止服务器
        /// </summary>
        private void EndServer(object sender, EventArgs e)
        {
            socketWatch?.Close();
            IsServerStart = false;
            // ShowBtnState();
            ShowMsg("信息:停止监听!");
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="clientSocket"></param>
        public void ReceiveMessage(Socket clientSocket)
        {
            Task.Factory.StartNew(() =>
            {
                while (IsServerStart)
                {
                    try
                    {
                        // 定义接收缓冲区（3MB）
                        const int BUFFER_SIZE = 1024 * 1024 * 3;
                        byte[] messageBuffer = new byte[BUFFER_SIZE];

                        // 接收到的信息大小(所占字节数)
                        int receivedBytes = clientSocket.Receive(messageBuffer);
                        Console.WriteLine($"接收到数据大小: {receivedBytes} 字节");

                        if (receivedBytes > 0)
                        {
                            // 将接收到的字节转换为字符串
                            string receivedMsg = Encoding.UTF8.GetString(messageBuffer, 0, receivedBytes);
                            IPEndPoint endPoint = clientSocket.RemoteEndPoint as IPEndPoint;

                            //Logger.Info($"从客户端 [{endPoint}] 接收到消息");
                            rawMsgLogger.Trace($"客户端 [{endPoint}] 原始消息内容:\n{receivedMsg}");

                            // 处理心跳信息
                            if (receivedMsg == "heartbeat")
                            {
                                ShowMsg($"收到【{endPoint}】心跳：{receivedMsg}");

                                FeedbackToHeartbeat(clientSocket, "OK");   // 发送心跳响应
                            }
                            else
                            {
                                // 服务器显示客户端的端口号和消息
                                Task.Run(() =>
                                {
                                    // 数据库文件处理
                                    ManageMonthlyDatabaseSwitch();
                                    // 处理接收到的数据
                                    ProcessReceivedData(receivedMsg, clientSocket.RemoteEndPoint.ToString());
                                });
                            }
                        }

                    }
                    catch (Exception)
                    {
                        // 移除添加在字典中的服务器和客户端之间的线程
                        clientList.Remove(clientSocket.RemoteEndPoint.ToString());
                        // 关闭客户端
                        clientSocket.Close();
                        break;
                    }
                }
            });
        }

        /// <summary>
        /// 专门用于发送心跳响应
        /// </summary>
        /// <param name="message"></param>
        void FeedbackToHeartbeat(Socket clientSocket, string message)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                clientSocket.Send(buffer);
            }
            catch (Exception ex)
            {
                clientList.Remove(clientSocket.RemoteEndPoint.ToString());
                clientSocket.Close();
                ShowMsg($"心跳响应发送失败：{ex.Message}");
            }

        }

        /// <summary>
        /// 发送消息到客户端
        /// </summary>
        /// <param name="message"></param>
        void Send(string message, string messageSource)
        {
            try
            {
                foreach (var client in clientList)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    client.Value.Send(buffer);
                }

                MessageBox.Show("发送成功");
            }
            catch (Exception ex)
            {
                ShowMsg($"{messageSource}发送失败：{ex.Message}");
                MessageBox.Show("发送失败");
            }

        }

        private void ShowMsg(string msg)
        {
            Invoke(new Action(() =>
            {
                if (rtbShowMSG.TextLength > 50000)
                {
                    rtbShowMSG.Clear();
                }
                string info = string.Format("{0}:{1}\r\n", DateTime.Now.ToString("G"), msg);
                rtbShowMSG.AppendText(info);
            }));
        }

        /* private void btn_send_Click(object sender, EventArgs e)
         {
             if (!IsStart) return;
             string info = richTextBox1.Text;
             if (info == "") return;
             int index = richTextBox1.SelectedIndex;
             if (index < 0) return;
             if (ClientSockets.Count <= index) return;
             socketSend = ClientSockets[index];
             Send(info);
         }*/

        /*/// <summary>
        /// 等待接收客户端连接
        /// </summary>
        /// <param name="o"></param>
        void Listen(object o)
        {
            try
            {
                Socket socketWatch = o as Socket;
                while (IsServerStart)
                {
                    socketSend = socketWatch.Accept();//等待接收客户端连接
                    ClientSockets.Add(socketSend);
                    ClientIPPorts.Add(socketSend.RemoteEndPoint.ToString());
                    this.BeginInvoke(UpdateListViewDataAction);
                    this.BeginInvoke(ShowMsgAction, socketSend.RemoteEndPoint.ToString() + ":" + "连接成功!");
                    //开启一个新线程，执行接收消息方法
                    Thread r_thread = new Thread(Received);
                    r_thread.IsBackground = true;
                    r_thread.Start(socketSend);
                    // Thread t = new Thread(new ThreadStart(ScanOffline));
                    //  t.IsBackground = true;
                    //  t.Start();
                }
            }
            catch (Exception ex)
            {
                this.BeginInvoke(ShowMsgAction, "等待客户端监听发生异常:" + ex.Message);
            }
        }*/

        /// <summary>
        /// 扫描离线
        /// </summary>

        /*/// <summary>
        /// 服务器端不停的接收客户端发来的消息
        /// </summary>
        /// <param name="o"></param>
        void Received(object o)
        {
            try
            {
                Socket socketSend = o as Socket;
                if (!socketSend.Connected) return;
                while (IsServerStart)
                {
                    //客户端连接服务器成功后，服务器接收客户端发送的消息
                    byte[] buffer = new byte[1024 * 1024 * 3];
                    //实际接收到的有效字节数
                    int len = socketSend.Receive(buffer);
                    if (len == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, len);
                    Task.Run(() =>
                    {
                        ProcessReceivedData(str, socketSend.RemoteEndPoint.ToString());
                    });

                    //SaveCSVlog(str);
                    this.BeginInvoke(ShowMsgAction, socketSend.RemoteEndPoint + ":" + str);

                }
            }
            catch (Exception ex)
            {

                //  mdbDatas mdbDa = new mdbDatas(path4);//conndnew connt
                string sql = "update  [product] set IP ='无' " +
                    ",conndnew='" + DateTime.Now + "'" +
                    ",connt='断开'  " +
                    "where IP='" + socketSend.RemoteEndPoint.ToString() + "'";
                foreach (DataRow row in productTable.Rows)
                {
                    // 名称, IP as IP,conndnew as 时间 ,connt as 状态
                    if (row["IP"].Equals(socketSend.RemoteEndPoint.ToString()))
                    {
                        //row["IP"] = SIP;
                        row["时间"] = DateTime.Now;
                        row["状态"] = "断开";
                    }
                }
                // bool resultC = mdbDa.Add(sql.ToString());
                // mdbDa.CloseConnection();
                // button8_Click(null, null);
                this.BeginInvoke(ShowMsgAction, socketSend.RemoteEndPoint + "接收客户端内容发生异常:" + ex.Message);

            }
        }*/

        /*void SendFid(string str)
        {

            try
            {
                string path = @"E:\old F directory\TangWei\kangge\new1.jpg";

                FileInfo EzoneFile = new FileInfo(path);

                FileStream EzoneStream = EzoneFile.OpenRead();

                int PacketSize = 100000;

                int PacketCount = (int)(EzoneStream.Length / ((long)PacketSize));

                //    this.textBox8.Text = PacketCount.ToString();

                //    this.progressBar1.Maximum = PacketCount;

                int LastDataPacket = (int)(EzoneStream.Length - ((long)(PacketSize * PacketCount)));
                if (socketSend == null) return;
                byte[] data = new byte[PacketSize];
                foreach (Socket client in ClientSockets)
                {
                    for (int i = 0; i < PacketCount; i++)
                    {
                        EzoneStream.Read(data, 0, data.Length);

                        TransferFiles.SendVarData(client, data);

                        //             this.textBox10.Text = ((int)(i + 1)).ToString();

                        //             this.progressBar1.PerformStep();
                    }

                    if (LastDataPacket != 0)
                    {
                        data = new byte[LastDataPacket];

                        EzoneStream.Read(data, 0, data.Length);

                        TransferFiles.SendVarData(client, data);

                        //            this.progressBar1.Value = this.progressBar1.Maximum;
                    }
                    client.Close();
                    EzoneStream.Close();

                }
                foreach(string key int new List<string>(ClientIPPorts)){ 
                }
                //if()
                //byte[] buffer = Encoding.UTF8.GetBytes(str);
                // socketSend.Send(buffer);
                MessageBox.Show("发送成功");
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
                MessageBox.Show("发送失败");
            }

        }*/

        /*private void ShowBtnState()
        {
            //btn_start.Enabled = !IsStart;
            //button7.Enabled = IsStart;
        }*/

        /// <summary>
        /// 用于实时监测客户端是否断开连接
        /// </summary>
        /*private void timer1_Tick(object sender, EventArgs e)
        {

            if (ClientSockets.Count == 0) return;
            for (int i = ClientSockets.Count - 1; i >= 0; i--)
            {
                if (!ClientSockets[i].Connected)
                {
                    ClientSockets.RemoveAt(i);
                    ClientIPPorts.RemoveAt(i);
                    this.BeginInvoke(UpdateListViewDataAction);
                }
            }
        }*/

        /*private void UpdateListViewData()
        {
            // listBox1.DataSource = null;
            //listBox1.DataSource = ClientIPPorts;
            //listBox1.Items.Clear();
            //for (int i = 0; i < ClientIPPorts.Count; i++)
            //{

            //}
        }*/

        /*public void SaveCSVlog(string log)
        {
            try
            {
                //  myJobManager.Run();
                if (System.IO.Directory.Exists("D:\\Logs") == false)
                {
                    System.IO.Directory.CreateDirectory("D:\\Logs");
                }
                // StringBuilder i = new StringBuilder();
                StringBuilder DataLine = new StringBuilder();

                string strT = DateTime.Now.Hour.ToString() + "时" + DateTime.Now.Minute.ToString() + "分" + DateTime.Now.Second.ToString() + "秒";

                //列标题
                // i.Append(log);
                //行数据
                DataLine.Append(strT + ":" + log);
                string FileName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                string FilePath = "D:\\Logs" + "\\" + FileName + ".CSV";

                if (System.IO.File.Exists(FilePath) == false)
                {
                    System.IO.StreamWriter stream = new System.IO.StreamWriter(FilePath, true, Encoding.UTF8);
                    //stream.WriteLine(i);
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

        }*/

        #region ---------- 设备状态页面 ----------

        /// <summary>
        /// 刷新客户端连接状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshStatus()
        {
            Invoke(new Action(() =>
            {
                dbHelper = new MDBHelper(databasePath);

                clientInfoTable = dbHelper.Find("SELECT Mname AS 名称, IP AS IP,conndnew AS 时间 ,connt AS 状态 FROM product");
                dgvClientInfo.DataSource = clientInfoTable;

                dbHelper.CloseConnection();
            }));
        }

        private void btnSendPModel_Click(object sender, EventArgs e)
        {
            Send($"0+{cboProductModel.Text}", "产品型号");
        }

        private void btnSendWorkOrder_Click(object sender, EventArgs e)
        {
            Send($"1+{txt_WorkOrder.Text}", "工单号");
        }

        private void LogMsg(string msg)
        {
            this.Invoke(new Action(() =>
            {
                if (richTextBox3.TextLength > 50000)
                {
                    richTextBox3.Clear();
                }
                richTextBox3.AppendText($"{DateTime.Now:G}:{msg}\r\n");
                richTextBox3.ScrollToCaret();
            }));
        }

        #endregion

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

        MesDatas.DatasModel.SytemInfoEntity MESInfo = null;

        private void BtnSaveMESConfig_Click(object sender, EventArgs e)
        {
            MESInfo.IP = textBox_ip.Text;
            MESInfo.Port = textBox_port.Text;
            MESInfo.Timeout = textBox_timeout.Text;
            MESInfo.NcCode = textBox_nccode.Text;
            MESInfo.Opration = textBox_opration.Text;
            MESInfo.Password = textBox_password.Text;
            MESInfo.Resource = textBox_resource.Text;
            MESInfo.Site = textBox_site.Text;
            MESInfo.Url = textBox_url.Text;
            MESInfo.User = textBox_user.Text;
            SytemInfoEntityServer.GetSytemInfoEntityUpdate(MESInfo);
            LoadMESConfig();
        }

        private void LoadMESConfig()
        {
            SytemInfoEntityServer.InitSytemInfoEntity();
            MESInfo = SytemInfoEntityServer.GetSytemInfoEntity(1);
            textBox_ip.Text = MESInfo.IP;
            textBox_port.Text = MESInfo.Port;
            textBox_timeout.Text = MESInfo.Timeout;
            textBox_nccode.Text = MESInfo.NcCode;
            textBox_opration.Text = MESInfo.Opration;
            textBox_password.Text = MESInfo.Password;
            textBox_resource.Text = MESInfo.Resource;
            textBox_site.Text = MESInfo.Site;
            textBox_url.Text = MESInfo.Url;
            textBox_user.Text = MESInfo.User;
            Config_Mes(ip, port, timeout, url, site, user, password, resource, operation, nccode);
        }

        public void Config_Mes(string ip, string port, string timeout,
          string url, string site, string user, string password, string resource, string operation, string ncCode)
        {
            工艺部信息化组.CONFIG.IP = ip;
            工艺部信息化组.CONFIG.PORT = port;
            int timeout1 = 5000;
            int.TryParse(timeout, out timeout1);
            工艺部信息化组.CONFIG.TimeOut = timeout1;
            工艺部信息化组.CONFIG.URL = url;
            工艺部信息化组.CONFIG.Site = site;
            工艺部信息化组.CONFIG.UserName = user;
            工艺部信息化组.CONFIG.Password = password;
            工艺部信息化组.CONFIG.Resource = resource;
            工艺部信息化组.CONFIG.Operation = operation;
            工艺部信息化组.CONFIG.NcCode = ncCode;
        }

        /// <summary>
        /// 查询工单
        /// </summary>
        private async void btnFind_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            StatInformaAS statInformaAS = await BydWorkCom.BydWorkStatisticsAsync(textBox10.Text, "");
            richTextBox2.Text = statInformaAS.WorkInformats;
            if (statInformaAS.IsHandle)
            {
                label29.Text = "OK";
                label29.ForeColor = Color.Green;
                if (statInformaAS.IsProcess)
                {
                    label35.Text = "OK";
                    label35.ForeColor = Color.Green;
                    label31.Text = statInformaAS.ORDER_NUM;
                    label32.Text = statInformaAS.COMP_NUM;
                    label33.Text = statInformaAS.COMP_RATE;
                }
                else
                {
                    label35.Text = "NG";
                    label35.ForeColor = Color.Red;
                }
            }
            else
            {
                label29.Text = "NG";
                label29.ForeColor = Color.Red;
            }
        }
    }
}
