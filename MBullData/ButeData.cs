using ADOX;
using HslCommunication.LogNet;
using MBullData.GeneratorS;
using MBullData.Properties;
using MesDatas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MBullData
{
    public partial class ButeData : Form
    {
        public ButeData()
        {
            InitializeComponent();
        }
        // 实例化一个日志后，路径是D的一个文件夹，按照每天的规律来存储
       // ILogNet logNet = new LogNetDateTime("D:\\Logs", GenerateMode.ByEveryDay);
        private void button1_Click(object sender, EventArgs e)
        {
            mdbDatas mdbDa = new mdbDatas(label1.Text + "\\"+ "SystemDateBase.mdb");

            string sqlSw = "ALTER TABLE [Board] ADD StandardCode  varchar(200)";
            mdbDa.Add(sqlSw);
            string sqlS = "ALTER TABLE [SytemSet] ADD StatisticsCode  varchar(200)";
            mdbDa.Add(sqlS);

            string sqlS1 = "ALTER TABLE [SytemSet] ADD StatisticsName  varchar(200)";
            mdbDa.Add(sqlS1);
            string sqlS2 = "update [SytemSet] set " +
                "[StatisticsCode]='D1080:A|D1084:A|D1086:A|D1088:A|D1090:T|D1082:A|D1076:A|D1064:I|D1066:I|D1068:I|D1070:T|D1072:T|D1074:T' " +
                "[StatisticsName]='生产总数|工单数量|完成数量|合格数量|生产节拍|保养计数|NG数量|直通率|完成率|合格率|工序时间|利用时间|负荷时间' where [ID] = '1'";
            mdbDa.Add(sqlS2);
            string sql0 = "ALTER TABLE [Codes] ADD TooName  varchar(200)";
            mdbDa.Add(sql0);
            string sqlA = "ALTER TABLE [Codes] ADD MateName  varchar(200)";
            mdbDa.Add(sqlA);
            string sql8 = "ALTER TABLE [SytemFaults] ADD WorkID  varchar(200)";
            mdbDa.Add(sql8);
            string sql9 = "ALTER TABLE [SytemFaults] ADD CodeID  varchar(200)";
            mdbDa.Add(sql9);
            string sql1 = "ALTER TABLE [UserInfo] ADD loginType  varchar(200)";
             mdbDa.Add(sql1);
            string sql2 = "update  [UserInfo] set Utype ='操作员',loginType='刷卡' where Uuser='sys' ";
            mdbDa.Add(sql2);
            string sql3 = "update  [UserInfo] set Utype ='操作员',loginType='密码' where Uuser='2' ";
            mdbDa.Add(sql3);
            string sql4 = "insert into [UserInfo] ([Uuser],[Upwd],[Utype],[loginType]) " +
                "values ('FC5E38011','FC5E38011','管理员','刷卡')";
            mdbDa.Add(sql4);//rfidCode
            string sql5 = "ALTER TABLE [SytemSet] ADD rfidPort  varchar(200)";
            mdbDa.Add(sql5);
            string sql6 = "ALTER TABLE [SytemSet] ADD rfidCode  varchar(200)";
            mdbDa.Add(sql6);
            string sql7 = "update  [SytemSet] set rfidPort ='COM5',rfidCode='20' where ID='1' ";
            mdbDa.Add(sql7);
           
            mdbDa.CloseConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.label1.Text = path.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mdbDatas mdbDa = new mdbDatas(label1.Text + "\\" + "SystemDateBase.mdb");
            string sql = "insert into SytemFaults ([ID],[WorkID],[CodeID],[Faults]) values" +
                "('1','1','管理员','刷卡')," +
                "('2','1','管理员','刷卡')," +
                "('3','1','管理员','刷卡')," +
                "('4','1','管理员','刷卡')," +
                "('5','1','管理员','刷卡')," +
                "('6','1','管理员','刷卡')," +
                "('7','1','管理员','刷卡')," +
                "('8','1','管理员','刷卡')," +
                "('9','1','管理员','刷卡')," +
                "('10','1','管理员','刷卡')," +
                "('11','1','管理员','刷卡')," +
                "('12','1','管理员','刷卡')," +
                "('13','1','管理员','刷卡')," +
                "('14','1','管理员','刷卡')," +
                "('15','1','管理员','刷卡')," +
                "('16','1','管理员','刷卡')," +
                "('17','1','管理员','刷卡')," +
                "('18','1','管理员','刷卡')," +
                "('19','1','管理员','刷卡')," +
                "('20','1','管理员','刷卡')," +
                "('21','1','管理员','刷卡')," +
                "('22','1','管理员','刷卡')," +
                "('23','1','管理员','刷卡')," +
                "('24','1','管理员','刷卡')," +
                "('25','1','管理员','刷卡')," +
                "('26','1','管理员','刷卡')," +
                "('27','1','管理员','刷卡')," +
                "('28','1','管理员','刷卡')," +
                "('29','1','管理员','刷卡')," +
                "('30','1','管理员','刷卡'),";
            mdbDa.Add(sql);
            mdbDa.CloseConnection();
        }

        private void ButeData_Load(object sender, EventArgs e)
        {
            GetLogs();
            // 然后我们的代码就可以调用下面的方法来存储日志了，支持下面的5个等级的
            LogUtils.LogNet.WriteDebug("Debug log test");
            LogUtils.LogNet.WriteInfo("Info log test","AAAAA");
            LogUtils.LogNet.WriteWarn("Warn log test");
            LogUtils.LogNet.WriteError("Error log test");
            LogUtils.LogNet.WriteFatal("Fatal log test");
        }
        public void GetLogs()
        {
            LogUtils.LogNet.BeforeSaveToFile += (object sender, HslEventArgs e) =>
            {
                this.Invoke(new Action(() =>
                {
                    if (richTextBox4.TextLength > 50000)
                    {
                        richTextBox4.Clear();
                    }
                    richTextBox4.AppendText(e.HslMessage.ToString()+"\r\n");
                    richTextBox4.ScrollToCaret();
                }));
            };
        }
       
        private void button4_Click(object sender, EventArgs e)
        {
            string tableName = "SytemInfoEntity";
            List<string> columns = new List<string>();
                List<string> types = new List<string>();
            List< string > comments = new List<string>();
            columns.Add("IP");
            types.Add("string");
            comments.Add("IP");
            columns.Add("Port");
            types.Add("string");
            comments.Add("Port");
            columns.Add("Timeout");
            types.Add("string");
            comments.Add("时间");
            columns.Add("NcCode");
            types.Add("string");
            comments.Add("NcCode");
            columns.Add("Opration");
            types.Add("string");
            comments.Add("Opration");
            columns.Add("Password");
            types.Add("string");
            comments.Add("Password");
            columns.Add("Resource");
            types.Add("string");
            comments.Add("Resource");
            columns.Add("Site");
            types.Add("string");
            comments.Add("Site");
            columns.Add("Url");
            types.Add("string");
            comments.Add("Url");
            columns.Add("User");
            types.Add("string");
            comments.Add("User");

            UtiyeGenerator.Generate(tableName, columns, types, comments);

            MessageBox.Show("生成成功！");

        }
    }
}
