using MBullData.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MBullData
{
    public class inipathLanguage
    {
        public static string inipathLanguagezhCN = System.AppDomain.CurrentDomain.BaseDirectory+  "Language\\Language_Chinese.ini";
        public static string inipathLanguageenUS = System.AppDomain.CurrentDomain.BaseDirectory+ "Language\\Language_English.ini";
        public static string inipathLanguagethTH = System.AppDomain.CurrentDomain.BaseDirectory+ "Language\\Language_Thai.ini";

        public static IniFiles GetiniFiles(int LanguageId)
        {
            IniFiles iniFiles =new IniFiles();
            switch (LanguageId)
            {
                case 0://zh-CN 中文
                    iniFiles = new IniFiles(inipathLanguagezhCN, "zh-CN");
                    break;
                case 1://en-US 英文
                    iniFiles = new IniFiles(inipathLanguageenUS, "en-US");
                    break;
                case 2://th-TH 泰文
                    iniFiles = new IniFiles(inipathLanguagethTH, "th-TH");
                    break;
                default:
                    iniFiles = new IniFiles(inipathLanguagezhCN, "zh-CN");
                    break;
            }
            return iniFiles;
        }
        
        /// <summary>
        /// 获取语言包的值
        /// </summary>
        /// <param name="language"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string IniLanguageValue(string key,int LanguageId)
        {
            IniFiles iniFiles = GetiniFiles(LanguageId);
            var value = iniFiles.IniReadValue( key);
            if (string.IsNullOrEmpty(value))
            {
                iniFiles.IniWriteValue( key, key);
                value = key;
            }
            return value;
        }
    }
}
