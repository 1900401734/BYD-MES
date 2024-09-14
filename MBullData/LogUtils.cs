using HslCommunication.LogNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBullData
{
    public class LogUtils 
    {
        public static string pathlogs = System.AppDomain.CurrentDomain.BaseDirectory + "Logs";
       // ILogNet logNet = new LogNetDateTime("D:\\Logs", GenerateMode.ByEveryDay);
        public static ILogNet logNet;
        public static ILogNet LogNet
        {
            // 实例化一个日志后，路径是D的一个文件夹，按照每天的规律来存储
            get { return logNet ?? (logNet = new LogNetDateTime(pathlogs, GenerateMode.ByEveryDay)); }
        }
    }
}
