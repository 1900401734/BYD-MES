using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YYMqttClient;

namespace UpdateBYDServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MqttServerFactory mqttServerFactory;
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        HslCommunication.Enthernet.NetSoftUpdateServer netSoftUpdate;
        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
              //  MessageBox.Show("请选择文件路径");
             //  return;
            }
            // 实例化exe
           netSoftUpdate = new HslCommunication.Enthernet.NetSoftUpdateServer("Upgrade.exe");
            // 客户端程序放在服务器当前目录的Client里面
            netSoftUpdate.FileUpdatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Client"); 
            netSoftUpdate.ServerStart(4321);            // 绑定的更新端口号信息
            mqttServerFactory = new MqttServerFactory();
            mqttServerFactory.MqttServerStart();
            label2.Text = "OK";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.textBox1.Text = path.SelectedPath;
        }
    }
}
