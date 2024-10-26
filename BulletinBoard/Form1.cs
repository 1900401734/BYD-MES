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
        mdbDatas mdb = null;
        mdbDatas mdbABC = new mdbDatas();
        private Socket socketWatch;
        private bool IsServerStart;
        private Action<string> ShowMsgAction;
        DataTable stationTable;     // 机台
        DataTable clientInfoTable;  // 运行状态界面 > 已连接的客户端的信息

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
            button8_Click();

            string conn = lblDatabasePath.Text + "\\" + DateTime.Now.ToString("Y") + "产线数据.mdb";
            if (mdbABC.mdbDatesconn(conn) == false)
            {
                InitProductLineDatabase();//初始化数据库
                // mdb.OpenConnction();
                mdbABC.OpenConnction();
                /// ShowMsg("打开数据库");
            }
            mdbABC.OpenConnction();

            DBFilemoveBate();
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

        private void DBFilemoveBate()
        {
            Invoke(new Action(() =>
            {
                string newDbPath = lblDatabasePath.Text + "\\" + DateTime.Now.ToString("Y") + "产线数据.mdb";
                string oldDbPath = lblDatabasePath.Text + "\\" + DateTime.Now.AddMonths(-1).ToString("Y") + "产线数据.mdb";
                string destinationDbPath = lblDatabasePath.Text + "\\" + "path" + "\\" + DateTime.Now.AddMonths(-1).ToString("Y") + "产线数据.mdb";
                try
                {
                    if (!File.Exists(newDbPath))
                    {
                        InitProductLineDatabase();//初始化数据库
                        mdbABC.mdbDatesconn(newDbPath);
                    }
                    if (File.Exists(oldDbPath))
                    {
                        // 确保目标路径存在
                        string destinationDirectory = Path.GetDirectoryName(destinationDbPath);
                        if (!Directory.Exists(destinationDirectory))
                        {
                            Directory.CreateDirectory(destinationDirectory);
                        }
                        mdbABC.CloseConnection();
                        // 移动文件
                        File.Move(oldDbPath, destinationDbPath);
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
            LoadStation();  // 加载工位信息

            mdb = new mdbDatas(databasePath);
            DataTable dashboardCongfig = mdb.Find("select * from Bulletins where ID = '1'");

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
                    txt_FinalDeviceName.Text = dashboardCongfig.Rows[i]["WorkName"].ToString();
                    string isStationIDChecked = dashboardCongfig.Rows[i]["FinishedName"].ToString();
                    if (isStationIDChecked == "True")
                    {
                        chkStationID.Checked = true;
                    }
                    txt_GenarateSpeed.Text = dashboardCongfig.Rows[i]["DegreesSev"].ToString();
                }
            }
            mdb.CloseConnection();
        }

        /// <summary>
        /// 加载工位
        /// </summary>
        private void LoadStation()
        {
            mdb = new mdbDatas(databasePath);
            stationTable = mdb.Find("select * from Model");

            string stationInfo = "";
            foreach (DataRow row in stationTable.Rows)
            {
                stationInfo += "{" + row[0] + "}\t";
            }

            lblTotalCount.Text = $"总共：{stationTable.Rows.Count}";
            txtStationList.Text = stationInfo;
            mdb.CloseConnection();
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

            mdb = new mdbDatas(databasePath);
            DataTable BulletinsTable = mdb.Find("select * from Bulletins where ID = '1'");
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
                var result = mdb.Change(updateSql);

                if (result)
                {
                    tbx_IP.Text = txt_ServerIP.Text;
                    tbx_port.Text = txt_ServerPort.Text;
                    MessageBox.Show("保存成功");
                }
            }
            mdb.CloseConnection();
        }

        /// <summary>
        /// 清空工位表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCleanStationList_Click(object sender, EventArgs e)
        {
            mdb = new mdbDatas(databasePath);
            bool bl = mdb.Del(" DELETE FROM [product] ");
            bool b2 = mdb.Del(" DELETE FROM [Model]   ");
            bool b3 = mdb.Del(" DELETE FROM [ModPros] ");

            if (bl == true)
            {
                txtStationList.Text = "";
                MessageBox.Show("清空成功");
            }
            lblTotalCount.Text = "总共：0";
            mdb.CloseConnection();
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

            mdb = new mdbDatas(databasePath);
            DataTable BulletinsTable = mdb.Find("select * from Bulletins where ID = '1'");
            if (BulletinsTable.Rows.Count > 0)
            {
                /*string sql = "update [Bulletins] set [WorkName]='" + txt_FinalDeviceName.Text + "'" + ",[FinishedName]='" + checkBox1.Checked + "'" +
                              ",[DegreesSev]='" + textBox9.Text + "'" + " where [ID] = '1'";*/

                string updateSql = $@" UPDATE [Bulletins]
                                       SET [WorkName] = '{txt_FinalDeviceName.Text}'
                                       [FinishedName] = '{chkStationID.Checked}'
                                       [DegreesSev] = '{txt_GenarateSpeed.Text}'
                                       WHERE [ID] = '1' ";

                var result = mdb.Change(updateSql);
                if (result)
                {
                    tbx_IP.Text = txt_ServerIP.Text;
                    tbx_port.Text = txt_ServerPort.Text;
                    MessageBox.Show("保存成功");
                }
            }
            mdb.CloseConnection();
        }

        #endregion

        #region ------------- 产线数据库 -------------

        /// <summary>
        /// 重新生成产线数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RebuildDatabase_Click(object sender, EventArgs e)
        {
            string dbPath = $@"{lblDatabasePath.Text}\{DateTime.Now:Y}产线数据.mdb";
            mdb = new mdbDatas();

            if (mdb.mdbDatescomm(dbPath) == false)
            {
                InitProductLineDatabase();
                MessageBox.Show("数据库初始成功！");
            }
            else
            {
                MessageBox.Show("该数据库已经存在，请删除数据库在初始化！");
            }
            mdb.CloseConnection();
        }

        /// <summary>
        /// 生成产线数据库
        /// </summary>
        private void InitProductLineDatabase()
        {
            string dbPath = $@"{lblDatabasePath.Text}\{DateTime.Now:Y}产线数据.mdb";
            mdbDatas.CreateAccessDatabase(dbPath);

            // 创建产线信息表
            StringBuilder sb = new StringBuilder(" 基地名称,车间名称,产线名称,产线工位数量,产线描述,产线属性");
            ArrayList arrayList = new ArrayList();
            object[] obj = new object[] { "基地名称", "车间名称", "产线名称", "产线工位数量", "产线描述", "产线属性" };
            arrayList.AddRange(obj);

            if (stationTable.Rows.Count > 0)
            {
                object[] obj1 = new object[stationTable.Rows.Count];
                for (int i = 0; i < stationTable.Rows.Count; i++)
                {
                    sb.Append(",");
                    int count = (i + 1);
                    obj1[i] = "工位" + count;
                    sb.Append("工位" + count);
                }
                arrayList.AddRange(obj1);
            }
            mdbDatas.CreateMDBTable(dbPath, "产线信息", arrayList);

            // 创建故障信息表
            ArrayList arrayList1 = new ArrayList();
            object[] o1 = new object[] { "故障发生工位", "机台名称", "故障类型", "故障描述", "发生时间", "结束时间", "更新标识" };
            arrayList1.AddRange(o1);
            mdbDatas.CreateMDBTable(dbPath, "故障信息", arrayList1);

            // 创建生产信息表
            ArrayList arrayList2 = new ArrayList();
            object[] obj2 = new object[] {  "工位名称", "当前工单号", "产品条码", "操作人员", "测试时间",
                "测试结果","测试节拍","测试项名称","测试项上限","测试项下限","测试项实际值", "更新标识" };
            arrayList2.AddRange(obj2);
            mdbDatas.CreateMDBTable(dbPath, "生产信息", arrayList2);//new System.Collections.ArrayList(new object[] { "产品", "条码", "测试人", "测试时间","测试结果", "PLC配方", "文件版本","软件版本"}));

            //创建统计信息表
            ArrayList arrayList3 = new ArrayList();
            object[] obj3 = new object[] { "工单号", "成品名称", "工单数量", "完成数量", "完成率", "合格率",
                "整线节拍","线平衡","OEE","直通率","更新时间","更新标识" };
            arrayList3.AddRange(obj3);
            mdbDatas.CreateMDBTable(dbPath, "统计信息", arrayList3);

            // 创建易损件信息表
            ArrayList arrayList4 = new ArrayList();
            object[] obj4 = new object[] { "易损件所在工位", "机台名称", "易损件所在位置", "易损件名称", "易损件理论使用次数",
                "易损件已使用次数","易损件剩余使用次数" };
            arrayList4.AddRange(obj4);
            mdbDatas.CreateMDBTable(dbPath, "易损件信息", arrayList4);

            // 初始化产线信息数据
            mdb = new mdbDatas(dbPath);
            DataTable table1 = mdb.Find("select * from 产线信息 where ID = 1");
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

                string sql = "insert into 产线信息 (" + sb + ") values (" + str1 + ")";
                bool result = mdb.Add(sql.ToString());
                if (result)
                {
                    ShowMsg("数据库生成成功!");
                }
            }
            mdb.CloseConnection();
        }

        #endregion

        #region------------- 添加生产数据 -------------

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

                    mdbABC.OpenConnction();

                    // 1+故障所在工位+机台名称+ 故障状态+故障的描述+触发故障的开始时间 
                    // 1+故障所在工位+机台名称+ 故障状态+故障的描述+触发故障的结束时间
                    // 2+工位名称+当前工单号+产品条码+操作人员+测试时间+测试结果+测试节拍+测试项名称+测试项上限+测试项下限+测式项实际值
                    // 3+工位+工单数量+完成数量+完成率+合格率+整体节拍+生产产品数量（总数）+ 工序时间+利用时间+负荷时间
                    // 4+易损件所在工位+机台名称+ 易损件所在位置+易损件名称+易损件理论使用次数易损件已使用次数

                    // 根据数据类型进行相应处理
                    // 0: 新工位配置
                    // 1: 故障信息
                    // 2: 生产信息
                    // 3: 统计信息
                    // 4: 易损件信息
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
                            GweiNameID(processedData);
                            NewMethod1(processedData);
                            break;
                        case "2":   // 生产信息处理
                            GweiNameID(processedData);
                            NewMethod2(processedData);
                            break;
                        case "3":   // 统计信息ModPros
                            NewMethod3Async(processedData);
                            break;
                        case "4":   // 易损件信息
                            GweiNameID(processedData);
                            NewMethod4(processedData);
                            break;
                        case "5":   // 工位状态处理
                            AddStationName(sourceIP, processedData);
                            break;
                        case "6":   // 日志信息处理
                            LogMsg(processedData[1]);
                            break;
                    }
                }
                //mdb.CloseConnection();
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
                    mdbDatas dbHelper = new mdbDatas(databasePath);

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
        /// 将名称修改工位ID
        /// </summary>
        /// <param name="dateshuzu"></param>
        private void GweiNameID(string[] dateshuzu)
        {
            if (chkStationID.Checked)
            {//[Model] ([Model],[Mname]
                DataRow[] rows = stationTable.Select("Model = '" + dateshuzu[1] + "'");
                if (rows.Length > 0)
                {
                    dateshuzu[1] = rows[0]["Mname"].ToString();
                }
            }
        }

        /// <summary>
        /// 添加机台名称
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
        /// 易损件信息
        /// </summary>
        /// <param name="dateshuzu"></param>
        private void NewMethod4(string[] dateshuzu)
        {
            /// 4+易损件所在工位+机台名称+ 易损件所在位置+易损件名称+易损件理论使用次数+易损件已使用次数
            /// 易损件所在工位", "机台名称", "易损件所在位置", "易损件名称", "易损件理论使用次数",
            // "易损件已使用次数","易损件剩余使用次数"
            int count4_1 = 0;//易损件理论使用次数
            int count4_2 = 0;//易损件已使用次数
            int count4_3 = 0;//易损件剩余使用次数
            if (int.TryParse(dateshuzu[5], out count4_1) && int.TryParse(dateshuzu[6], out count4_2))
            {
                count4_3 = count4_1 - count4_2;
            }
            DataTable table4 = mdbABC.Find("select * from 易损件信息 where 易损件所在工位='" + dateshuzu[1] + "'and 机台名称='"
                + dateshuzu[2] + "'and 易损件所在位置='" + dateshuzu[3] + "'and 易损件名称='" + dateshuzu[4] + "'");
            if (table4.Rows.Count > 0)
            {
                string sql1 = "update [易损件信息] set [易损件已使用次数]='" + count4_2 + "'," +
                    "[易损件理论使用次数]='" + count4_1 + "', [易损件剩余使用次数] = '" + count4_3 + "'" +
                    " where [机台名称] = '" + dateshuzu[2] + "'and 易损件名称='" + dateshuzu[4] + "'";
                var result1 = mdbABC.Change(sql1);
                if (result1 == true)
                {
                    this.BeginInvoke(ShowMsgAction, "工位：" + dateshuzu[1] + "机台：" + dateshuzu[2] +
                        "易损件信息更新：" + dateshuzu[3] + dateshuzu[4]);
                }
            }
            else
            {
                string sql1 = "insert into 易损件信息 (易损件所在工位, 机台名称, 易损件所在位置, 易损件名称, " +
                    "易损件理论使用次数,易损件已使用次数,易损件剩余使用次数)" +
                    " values ('" + dateshuzu[1] + "','" + dateshuzu[2] + "','" + dateshuzu[3] + "','" + dateshuzu[4] + "','"
                    + count4_1 + "','" + count4_2 + "','" + count4_3 + "')";
                bool result1 = mdbABC.Add(sql1.ToString());
                if (result1 == true)
                {
                    this.BeginInvoke(ShowMsgAction, "工位：" + dateshuzu[1] + "机台：" + dateshuzu[2] +
                        "新增易损件信息:" + dateshuzu[3] + dateshuzu[4]);
                }
            }
        }

        /// <summary>
        /// 统计信息ModPros
        /// </summary>
        /// <param name="dateshuzu"></param>
        List<Dictionary<string, List<string>>> listDic = new List<Dictionary<string, List<string>>>();

        private async void NewMethod3Async(string[] dateshuzu)
        {
            if (txt_FinalDeviceName.Text.Trim().Length == 0)
            {
                this.BeginInvoke(ShowMsgAction, "输入最后一个机台！！！");
                return;
            }
            if (listDic.Count == 0)
            {
                Dictionary<string, List<string>> dicmapA = new Dictionary<string, List<string>>();
                dicmapA.Add(dateshuzu[1], ListDicodmh(dateshuzu));
                listDic.Add(dicmapA);
                /*if (dicmapA.Count == productTable.Rows.Count)
                {
                    double count1 = productTable.Rows.Count;//工位数量
                    double num1 = 0;//工序
                    double num2 = 0;//工序总和
                    double num3 = 0;//瓶颈;
                    double num4 = 0;//直通率
                    double num5 = 1; //直通率乘积
                    double timenum1 = 0;//利用时间
                                        //  double timenum2 = 0;//利用时间总和
                    double timenum3 = 0;//负荷时间
                                        // double timenum4 = 0;//负荷时间总和
                    foreach (List<string> valuelist in dicmapA.Values)
                    {
                        //0工单号//1成品名称//2工单数量
                        //3完成数量//4完成率//5合格率//6整体节拍//7工序时间//8利用时间 //9负荷时间
                        //10生产产品数量（总数）//11直通率
                        num2 += num1;
                        double.TryParse(valuelist[7], out num3);
                        if (num3 > num1)//计算最小的
                        {
                            double.TryParse(valuelist[7], out num1);//工序
                            num3 = num1;
                        }
                        double.TryParse(valuelist[11], out num4);
                        if (num4 != 0)//直通率乘积
                        {
                            num5 *= num4;
                        }
                        double.TryParse(valuelist[8], out timenum1);
                        double.TryParse(valuelist[9], out timenum3);
                    }
                    DataRowView dataRowView = (DataRowView)comboBox2.SelectedItem;
                    if (dicmapA.ContainsKey(dataRowView["名称"].ToString()))
                    {
                        List<string> dicstrkey = dicmapA[dataRowView["名称"].ToString()];
                        string sql3_1 = "insert into 统计信息 (工单号, 成品名称, 工单数量, 完成数量, 完成率, 合格率,整线节拍" +
                                                                ",线平衡,OEE,直通率,更新时间,更新标识 )  values ('";
                        //0工单号//1成品名称//2工单数量
                        //3完成数量//4完成率//5合格率//6整体节拍//7工序时间//8利用时间 //9负荷时间
                        //10生产产品数量（总数）//11直通率
                        sql3_1 += dicstrkey[0] + "','";
                        sql3_1 += dicstrkey[1] + "','";
                        sql3_1 += dicstrkey[2] + "','";
                        sql3_1 += dicstrkey[3] + "','";
                        sql3_1 += dicstrkey[4] + "','";
                        sql3_1 += dicstrkey[5] + "','";
                        sql3_1 += dicstrkey[6] + "','";
                        if (num2 != 0 && num3 != 0 && count1 != 0)
                        {
                            sql3_1 += num2 / (count1 / num3) + "','";
                        }
                        else
                        {
                            sql3_1 += "  " + "','";
                        }
                        string OEE = " ";
                        double.TryParse(dicstrkey[8], out timenum1);//利用时间
                        double.TryParse(dicstrkey[9], out timenum3);//负荷时间
                        double rowcount9 = 0;//生产产品数量（总数）
                        double.TryParse(dicstrkey[10], out rowcount9);
                        double textcount9 = 0;//设计数度
                        double.TryParse(textBox9.Text, out textcount9);
                        double numhg = 0;//合格率
                        double.TryParse(dicstrkey[5], out numhg);
                        if (timenum1 != 0 && timenum3 != 0 && textcount9 != 0 && textcount9 != 0 && numhg != 0)
                        {
                            OEE = (timenum1 / timenum3) * (rowcount9 / (timenum1 * textcount9)) * numhg + "";
                        }
                        sql3_1 += OEE + "','";
                        sql3_1 += num5 + "','";
                        sql3_1 += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','";
                        sql3_1 += "F" + "')";
                        var result3_1 = mdb.Add(sql3_1.ToString());
                        if (result3_1)
                        {
                            this.BeginInvoke(ShowMsgAction, "添加一条统计信息");
                            listDic.Remove(dicmapA);
                        }
                    }
                }*/
            }
            else
            {
                for (int i = 0; i < listDic.Count; i++)
                {
                    Dictionary<string, List<string>> dicmap = new Dictionary<string, List<string>>();
                    dicmap = listDic[i];
                    if (dicmap.ContainsKey(txt_FinalDeviceName.Text))
                    {
                        bool pingjin = true;
                        double count1 = dicmap.Count;//工位数量
                        double num1 = 0;//工序
                        double num2 = 0;//工序总和
                        double num3 = 0;//瓶颈;
                        double num4 = 0;//直通率
                        double num5 = 1; //直通率乘积
                        double timenum1 = 0;//利用时间
                                            //  double timenum2 = 0;//利用时间总和
                        double timenum3 = 0;//负荷时间
                                            // double timenum4 = 0;//负荷时间总和
                        foreach (List<string> valuelist in dicmap.Values)
                        {
                            double.TryParse(valuelist[7], out num1);//工序
                            //0工单号//1成品名称//2工单数量
                            //3完成数量//4完成率//5合格率//6整体节拍//7工序时间//8利用时间 //9负荷时间
                            //10生产产品数量（总数）//11直通率
                            num2 += num1;
                            if (pingjin)
                            {
                                pingjin = false;
                                double.TryParse(valuelist[7], out num3);
                            }
                            if (num3 > num1)//计算最小的
                            {
                                //double.TryParse(valuelist[7], out num1);//工序
                                num3 = num1;
                            }
                            double.TryParse(valuelist[11], out num4);
                            num4 /= 100;
                            if (num4 != 0)//直通率乘积
                            {
                                num5 *= num4;
                            }
                            //   double.TryParse(valuelist[8], out timenum1);
                            //   double.TryParse(valuelist[9], out timenum3);
                        }
                        // DataRowView dataRowView = (DataRowView)comboBox2.SelectedItem;

                        List<string> dicstrkey = dicmap[txt_FinalDeviceName.Text.ToString()];
                        StatInformaAS statInformaAS = await BydWorkCom.BydWorkStatisticsAsync(dicstrkey[0], "");
                        if (statInformaAS.IsHandle && statInformaAS.IsProcess)
                        {
                            dicstrkey[2] = statInformaAS.ORDER_NUM;
                            dicstrkey[3] = statInformaAS.COMP_NUM;
                            dicstrkey[4] = statInformaAS.COMP_RATE.TrimEnd('%');
                        }

                        string sql3_1 = "insert into 统计信息 (工单号, 成品名称, 工单数量, 完成数量, 完成率, 合格率,整线节拍" +
          ",线平衡,OEE,直通率,更新时间,更新标识 )  values ('";
                        //0工单号//1成品名称//2工单数量
                        //3完成数量//4完成率//5合格率//6整体节拍//7工序时间//8利用时间 //9负荷时间
                        //10生产产品数量（总数）//11直通率
                        double dicstrkey4 = 0;
                        double.TryParse(dicstrkey[4], out dicstrkey4);
                        dicstrkey[4] = (dicstrkey4 / 100).ToString();
                        double dicstrkey5 = 0;
                        double.TryParse(dicstrkey[5], out dicstrkey5);
                        dicstrkey[5] = (dicstrkey5 / 100).ToString();
                        sql3_1 += dicstrkey[0] + "','";
                        sql3_1 += dicstrkey[1] + "','";
                        sql3_1 += dicstrkey[2] + "','";
                        sql3_1 += dicstrkey[3] + "','";
                        sql3_1 += dicstrkey[4] + "','";
                        sql3_1 += dicstrkey[5] + "','";
                        sql3_1 += dicstrkey[6] + "','";
                        if (num2 != 0 && num3 != 0 && count1 != 0)
                        {
                            double AA = num2 / (count1 * num3);
                            sql3_1 += AA.ToString("0.0000") + "','";
                        }
                        else
                        {
                            sql3_1 += "0" + "','";
                        }
                        string OEE = "";
                        double.TryParse(dicstrkey[8], out timenum1);//利用时间
                        double.TryParse(dicstrkey[9], out timenum3);//负荷时间
                        double rowcount9 = 0;//生产产品数量（总数）
                        double.TryParse(dicstrkey[10], out rowcount9);
                        double textcount9 = 0;//设计数度
                        double.TryParse(txt_GenarateSpeed.Text, out textcount9);
                        double numhg = 0;//合格率
                        double.TryParse(dicstrkey[5], out numhg);
                        if (timenum1 != 0 && timenum3 != 0 && textcount9 != 0 && textcount9 != 0 && numhg != 0)
                        {
                            double OEE1 = ((timenum1 / timenum3) * (rowcount9 / (timenum1 * textcount9)) * numhg);
                            OEE = OEE1.ToString("0.0000");
                        }
                        else
                        {
                            OEE = "0";
                        }
                        sql3_1 += OEE + "','";
                        sql3_1 += num5.ToString("0.0000") + "','";
                        sql3_1 += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','";
                        sql3_1 += "F" + "')";
                        var result3_1 = mdbABC.Add(sql3_1.ToString());
                        if (result3_1)
                        {
                            this.BeginInvoke(ShowMsgAction, "添加一条统计信息");
                            listDic.Remove(dicmap);
                        }

                    }
                    else if (dicmap.ContainsKey(dateshuzu[1]))
                    {
                        Dictionary<string, List<string>> dicmapA = new Dictionary<string, List<string>>();
                        dicmapA.Add(dateshuzu[1], ListDicodmh(dateshuzu));
                        listDic.Add(dicmapA);
                        break;
                    }
                    else
                    {
                        dicmap.Add(dateshuzu[1], ListDicodmh(dateshuzu));
                        listDic[i] = dicmap;
                        break;
                    }
                }
            }

        }

        private List<string> ListDicodmh(string[] dateshuzu)
        {
            //3+机台名称1+工单号2+工单数量3+完成数量4+
            //完成率5+合格率6+整体节拍7+生产产品数量（总数）8
            //+ 工序时间9+利用时间10+负荷时间11+直通率12+成品名称13
            List<string> list = new List<string>();
            list.Add(dateshuzu[2]);//工单号
            list.Add(dateshuzu[13]);//成品名称
            list.Add(dateshuzu[3]);//工单数量
            list.Add(dateshuzu[4]);//完成数量
            list.Add(dateshuzu[5]);//完成率
            list.Add(dateshuzu[6]);//合格率
            list.Add(dateshuzu[7]);//整体节拍
            list.Add(dateshuzu[9]);//工序时间
            list.Add(dateshuzu[10]);//利用时间
            list.Add(dateshuzu[11]);//负荷时间
            list.Add(dateshuzu[8]);//生产产品数量（总数）
            list.Add(dateshuzu[12]);//直通率
            return list;
        }

        /// <summary>
        /// 生成信息表
        /// </summary>
        /// <param name="dateshuzu"></param>
        /// <param name="conn"></param>
        private async void NewMethod2(string[] dateshuzu)
        {
            await Task.Run(() =>
            {
                /// 2+工位名称+当前工单号+产品条码+操作人员+则式时间+则试结果+ 则试节拍+则试项名称+ 则试项上限+则试项下限+测式项实际值
                string sql2 = "insert into 生产信息 (工位名称 ,当前工单号, 产品条码,操作人员, " +
                    "测试时间,测试结果,测试节拍,测试项名称," +
                    "测试项上限,测试项下限,测试项实际值, 更新标识)" +
                       " values ('" + dateshuzu[1] + "','" + dateshuzu[2] + "','" + dateshuzu[3] + "','" + dateshuzu[4] + "','"
                   + dateshuzu[5] + "','" + dateshuzu[6] + "','" + dateshuzu[7] + "','" + dateshuzu[8] + "','"
                   + dateshuzu[9] + "','" + dateshuzu[10] + "','" + dateshuzu[11] + "','" + "F" + "')";
                var result2 = mdbABC.Add(sql2.ToString());
            });
        }

        /// <summary>
        /// 故障信息表
        /// </summary>
        /// <param name="dateshuzu"></param>
        private void NewMethod1(string[] dateshuzu)
        {
            /// 1+故障所在工位+机台名称+ 故障状态+故障的描述+触发故障的开始时间 
            /// 1+故障所在工位+机台名称+故障的描述+触发故障的结束时间
            DataTable table1 = mdbABC.Find("select * from 故障信息 where 故障发生工位='" + dateshuzu[1] + "'and 机台名称='"
                + dateshuzu[2] + "'and 故障描述='" + dateshuzu[4] + "'" + "and 发生时间='" + dateshuzu[5] + "'");
            if (table1.Rows.Count > 0)
            {
                DateTime dateTime1 = new DateTime();
                if (dateshuzu.Length > 6)
                {
                    bool redtime1 = DateTime.TryParse(dateshuzu[6], out dateTime1);
                    if (redtime1 == false)
                    {
                        dateshuzu[6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    //string sql = "update [Codes] set [Name]='" + funmae + "'" + " where [ID] = '" +
                    //
                    //
                    //+ "'";
                    string sql1 = "update [故障信息] set [结束时间]='" + dateshuzu[6] + "',更新标识='F'" + " where 故障发生工位='" + dateshuzu[1] + "'and 机台名称='"
                    + dateshuzu[2] + "'and 故障描述='" + dateshuzu[4] + "'" + "and 发生时间='" + dateshuzu[5] + "'";
                    var result1 = mdbABC.Change(sql1);
                    if (result1 == true)
                    {
                        this.BeginInvoke(ShowMsgAction, "工位：" + dateshuzu[1] + "机台：" + dateshuzu[2] +
                            "故障停止信息：" + dateshuzu[3] + dateshuzu[6]);
                    }
                }
            }
            else
            {
                string datime = "";
                DateTime dateTime1 = new DateTime();
                if (dateshuzu.Length > 6)
                {
                    bool redtime1 = DateTime.TryParse(dateshuzu[6], out dateTime1);
                    if (redtime1 == false)
                    {
                        dateshuzu[6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    datime = dateshuzu[6];
                }
                DateTime dateTime = new DateTime();
                bool redtime = DateTime.TryParse(dateshuzu[5], out dateTime);
                if (redtime)
                {
                    string sql1 = "insert into 故障信息 (故障发生工位, 机台名称, 故障类型, 故障描述, 发生时间, 结束时间, 更新标识)" +
                        " values ('" + dateshuzu[1] + "','" + dateshuzu[2] + "','" + dateshuzu[3] + "','" + dateshuzu[4] + "','"
                        + dateshuzu[5] + "','" + datime + "','" + "F" + "')";
                    bool result1 = mdbABC.Add(sql1.ToString());
                    if (result1 == true)
                    {
                        this.BeginInvoke(ShowMsgAction, "工位：" + dateshuzu[1] + "机台：" + dateshuzu[2] +
                            "故障发生信息：" + dateshuzu[3] + dateshuzu[4] + dateshuzu[5]);
                    }
                }
            }
        }

        #endregion

        #region------------- 型号设置 -------------

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
            mdb = new mdbDatas(databasePath);

            // Codes  ID CName
            DataTable PModelTable = mdb.Find("select ID as 编号 , Name as 型号 from Codes");
            dgvPModelSettings.DataSource = PModelTable;
            cboProductModel.DataSource = PModelTable;
            cboProductModel.DisplayMember = "型号";
            cboProductModel.ValueMember = "编号";

            mdb.CloseConnection();
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Codes  ID CName
            if (e.RowIndex == -1) return;

            // 删除
            if (dgvPModelSettings.Columns[e.ColumnIndex].Name == "DeleteOperation")
            {
                mdb = new mdbDatas(databasePath);

                string currentID = this.dgvPModelSettings.Rows[e.RowIndex].Cells[2].Value.ToString();
                bool result = mdb.Del($" DELETE FROM [Codes] WHERE [ID] = '{currentID}' ");
                if (result == true)
                {
                    MessageBox.Show("删除成功");
                }

                mdb.CloseConnection();
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

                mdb = new mdbDatas(databasePath);

                DataTable table1 = mdb.Find($"select * from Codes where [ID] = '{ID}'");
                if (table1.Rows.Count > 0)
                {
                    string updateSql = $" UPDATE [Codes] SET [Name]='{productModel}' where [ID] = '{ID}' ";
                    var result = mdb.Change(updateSql);
                    if (result == true)
                    {
                        MessageBox.Show("修改成功");
                    }
                }
                else
                {
                    string insertSql = $"INSERT INTO Codes ([ID],[Name]) VALUES ('{ID}', '{productModel}')";
                    bool result = mdb.Add(insertSql.ToString());
                    if (result == true)
                    {
                        MessageBox.Show("新增成功");
                    }
                }

                mdb.CloseConnection();
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
                ShowMsg("信息: 服务器监听启动成功!");

                // 设置监听队列长度
                serverSocket.Listen(100);  // 最大允许100个连接请求排队
                IsServerStart = true;

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
                ShowMsg($"错误信息: {ex.Message}");
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

        Logger rawMsgLogger = LogManager.GetLogger("ReceivedMsg");

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

                                Send(clientSocket, "OK");   // 发送心跳响应
                            }
                            else
                            {
                                // 服务器显示客户端的端口号和消息
                                Task.Run(() =>
                                {
                                    // 数据库文件处理
                                    DBFilemoveBate();
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
        void Send(Socket clientSocket, string message)
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
                ShowMsg(ex.Message);
            }

        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        void Send(string message)
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
                ShowMsg(ex.Message);
                MessageBox.Show("发送失败");
            }

        }

        private void ShowMsg(string msg)
        {
            Invoke(new Action(() =>
            {
                if (richTextBox1.TextLength > 50000)
                {
                    richTextBox1.Clear();
                }
                string info = string.Format("{0}:{1}\r\n", DateTime.Now.ToString("G"), msg);
                richTextBox1.AppendText(info);
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

        /// <summary>
        /// 刷新状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click()
        {
            Invoke(new Action(() =>
            {
                mdb = new mdbDatas(databasePath);

                clientInfoTable = mdb.Find("select Mname as 名称, IP as IP,conndnew as 时间 ,connt as 状态 from product");
                dgvClientInfo.DataSource = clientInfoTable;

                mdb.CloseConnection();
            }));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Send("0+" + cboProductModel.SelectedValue);


        }

        private void button11_Click(object sender, EventArgs e)
        {
            // textBox11.Focus();
            Send("1+" + txt_WorkOrder.Text);
        }

        private void LogMsg(string msg)
        {
            this.Invoke(new Action(() =>
            {
                if (richTextBox3.TextLength > 50000)
                {
                    richTextBox3.Clear();
                }
                richTextBox3.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + msg + "\r\n");
                richTextBox3.ScrollToCaret();
            }));
        }

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

        private async void button8_Click_1(object sender, EventArgs e)
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
