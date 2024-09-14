using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.Utility.IniLaguagePath
{
    public class inipathLanguage
    {
        public static string inipathPath = System.AppDomain.CurrentDomain.BaseDirectory + "Language";
        public static string inipathLanguagezhCN = System.AppDomain.CurrentDomain.BaseDirectory + "Language\\Language_Chinese.ini";
        public static string inipathLanguageenUS = System.AppDomain.CurrentDomain.BaseDirectory + "Language\\Language_English.ini";
        public static string inipathLanguagethTH = System.AppDomain.CurrentDomain.BaseDirectory + "Language\\Language_Thai.ini";

        public static void GetiniFiles(int LanguageId)
        {
            switch (LanguageId)
            {
                case 0://zh-CN 中文
                    inipath = inipathLanguagezhCN;
                    Section="zh-CN";
                    break;
                case 1://en-US 英文
                    inipath = inipathLanguageenUS;
                    Section ="en-US";
                    break;
                case 2://th-TH 泰文
                    inipath = inipathLanguagethTH;
                    Section="th-TH";
                    break;
                default:
                    inipath = inipathLanguagezhCN;
                    Section = "zh-CN";
                    break;
            }
        }

        /// <summary>
        /// 获取语言包的值
        /// </summary>
        /// <param name="language"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string IniLanguageValue(string key, int LanguageId)
        {
           GetiniFiles(LanguageId);
            Directory.CreateDirectory(inipathPath);
            if (!File.Exists(inipath))
            {
                File.Create(inipath);
            }
            var value = IniReadValue(key);
            if (string.IsNullOrEmpty(value))
            {
                IniWriteValue(key, key);
                value = key;
            }
            return value;
        }
        //声明读写INI文件API函数

        public static string inipath;
        //声明API函数
        public static string Section;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);



        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        /// <param name="Value">值</param> 
        public static void IniWriteValue(string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, inipath);
        }
        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public static string IniReadValue(string Key)
        {
            StringBuilder temp = new StringBuilder(1024);
            int i = GetPrivateProfileString(Section, Key, "", temp, 1024, inipath);
            return temp.ToString();
        }
        /// <summary> 
        /// 验证文件是否存在 
        /// </summary> 
        /// <returns>布尔值</returns> 
        public static bool ExistINIFile()
        {
            return File.Exists(inipath);
        }
    }
}
