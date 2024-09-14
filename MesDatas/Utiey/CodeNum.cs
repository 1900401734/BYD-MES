using HslCommunication.Profinet.Keyence;
using MesDatas.DatasModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.Utiey
{
    class CodeNum
    {
        /// <summary>
        /// //对string 转double ➗1000 在转string
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        public static string PThousdCode(string ss)
        {
            string aa = "0";
            double db = 0;
            if (double.TryParse(ss, out db))
            {
                if (db != 0)
                {
                    aa = Convert.ToString((Convert.ToDouble(ss)) / 1000);
                }
                else
                {
                    aa = Convert.ToString((Convert.ToDouble(ss)));
                }
            }
            return aa;

        }

        /// <summary>
        /// //对string 转double ➗100 在转string
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        public static string PNumCode(string ss)
        {
            string aa = "0";
            double db = 0;
            if (double.TryParse(ss, out db))
            {
                if (db != 0)
                {
                    aa = Convert.ToString((Convert.ToDouble(ss)) / 100);
                }
                else
                {
                    aa = Convert.ToString((Convert.ToDouble(ss)));
                }
            }
            return aa;

        }

        /// <summary>
        /// //对string 转double ➗100 在转string(取2为)
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        public static string PdounInCode(string ss)
        {
            string aa = "0";
            double db = 0;
            if (double.TryParse(ss, out db))
            {
                if (db != 0)
                {
                    double mydo = Math.Round((Convert.ToDouble(ss)) / 100, 2);
                    aa = mydo.ToString("0.00");
                }
                else
                {
                    aa = Convert.ToString((Convert.ToDouble(ss)));
                }
            }
            return aa;
        }

        public static string PThouInCode(string ss)
        {
            string aa = "0";
            double db = 0;
            if (double.TryParse(ss, out db))
            {
                if (db != 0)
                {
                    double mydo = Math.Round((Convert.ToDouble(ss)) / 1000, 3);
                    aa = mydo.ToString("0.000");
                }
                else
                {
                    aa = Convert.ToString((Convert.ToDouble(ss)));
                }
            }
            return aa;
        }

        /// <summary>
        /// //对string 转double ➗10 在转string
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        public static string PNtimeCode(string ss)
        {
            string aa = "0";
            double db = 0;
            if (double.TryParse(ss, out db))
            {
                if (db != 0)
                {
                    aa = Convert.ToString((Convert.ToDouble(ss)) / 10);
                }
                else
                {
                    aa = Convert.ToString((Convert.ToDouble(ss)));
                }
            }
            return aa;
        }

        /// <summary>
        /// 转换3:OK 2:AG
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        public static string PNumOKAG(string ss)
        {
            string aa = "null";
            int db = 0;
            if (int.TryParse(ss, out db))
            {
                if (db == 3)
                {
                    aa = "OK";
                }
                else if (db == 2)
                {
                    aa = "NG";
                }
            }
            return aa;
        }

        /// <summary>
        /// 清空字符串中包含的 "?", "\0", "\r", "\n", 以及字符串两边的空白
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatString(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (str.IndexOf("\0") >= 0)
                {
                    str = str.Replace("\0", "");
                }
                if (str.IndexOf("?") >= 0)
                {
                    str = str.Replace("?", "");
                }
                if (str.IndexOf("\r") >= 0)
                {
                    str = str.Replace("\r", "");
                }
                if (str.IndexOf("\n") >= 0)
                {
                    str = str.Replace("\n", "");
                }
                str = str.Trim();
            }
            return str;
        }

        /// <summary>
        /// 处理工位
        /// </summary>
        /// <param name="workstID"></param>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public static List<string> WorkIDName(string[] workstID, string[] stationName)
        {
            List<string> workstNamelist = new List<string>();
            string worName = "  ";
            for (int i = 0; i < workstID.Length; i++)
            {
                string workID = workstID[i];
                if (workID.Contains("+"))
                {
                    int count = 0;
                    string newStr = workID.Replace("+", "");
                    int.TryParse(newStr, out count);
                    for (int j = 0; j < count; j++)
                    {
                        workstNamelist.Add(worName);
                    }
                }
                else
                {
                    int id = 0;
                    if (int.TryParse(workstID[i], out id))
                    {
                        id = id - 1;
                        if (stationName.Length > id)
                        {
                            worName = stationName[id];
                        }
                    }
                    workstNamelist.Add(worName);
                }
            }
            return workstNamelist;
        }

        /// <summary>
        /// 通过id获取名称
        /// </summary>
        /// <param name="workstID"></param>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public static string WorkIDNm(string workstID, string[] stationName)
        {
            string worName = "";
            int id = 0;
            if (int.TryParse(workstID, out id))
            {
                id = id - 1;
                if (stationName.Length > id)
                {
                    worName = stationName[id];
                }
            }
            return worName;
        }

        /// <summary>
        /// 获取全部信息
        /// </summary>
        /// <param name="mstr"></param>
        /// <returns></returns>
        public static List<string> SMaxMindemo(string[] mstr)
        {
            List<string> listarr = new List<string>();
            if (mstr.Length > 0)
            {
                string boardBeat = "NO";//
                for (int i = 0; i < mstr.Length; i++)
                {
                    string beatcode = mstr[i];//
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
                        boardBeat = beatcode;
                        listarr.Add(beatcode);
                    }
                }
            }
            return listarr;
        }

        /// <summary>
        /// 获取不等于NO的个数
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int TMaxMinstrhomd(List<string> list)
        {
            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].Equals("NO"))
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 获取不等于NO的个数
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int Tresultstrhomd(string[] strresult)
        {
            int count = 0;
            for (int i = 0; i < strresult.Length; i++)
            {
                if (!strresult[i].Equals("NO"))
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 返回No
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string NOCodes(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                code = "NO";
            }
            return code;
        }

        /// <summary>
        /// 返回+1
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string ONECodes(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                code = "+1";
            }
            return code;
        }

        /// <summary>
        /// 返回1
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string WorkIDONECodes(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                code = "1";
            }
            return code;
        }

        /// <summary>
        /// 如果为空或者null 就赋值空格
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetNullUnit(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                code = "   ";
            }
            return code;
        }

        /// <summary>
        /// 是否为int 如果不是默认100
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int Doubtowule(string strdou)
        {
            int couiou = 0;
            if (!int.TryParse(strdou, out couiou))
            {
                couiou = 100;
            }
            return couiou;
        }

        /// <summary>
        /// 是否为ushout 如果不是默认200
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static ushort Shoubtowule(string strdou)
        {
            ushort couiou = 0;
            if (!ushort.TryParse(strdou, out couiou))
            {
                couiou = 200;
            }
            return couiou;
        }

        /// <summary>
        /// 从条码验证型号与工装编号中提取条码验证型号，即条码验证规则。
        /// 条码验证型号 = 条码验证规则
        /// </summary>
        public static string ExtractBarcodeValidationRule(string barcodeAndFixturesInfo)
        {
            string barcodeRule = "";
            string[] array = barcodeAndFixturesInfo.Split('|');
            if (array.Length > 0)
            {
                barcodeRule = array[0];
            }
            return barcodeRule;
        }

        /// <summary>
        /// 从条码验证型号与工装编号中提取工装编号。工装编号 = 工装编号1 + 工装编号2
        /// </summary>
        public static string[] ExtractFixturesInfo(string barcodeAndFixturesInfo)
        {
            string[] fixturesInfo = new string[] { };
            string[] parts = barcodeAndFixturesInfo.Split('|');

            if (parts.Length > 1)
            {
                // 分离工装编号
                fixturesInfo = parts[1].ToString().Split('+');
            }
            else
            {
                // 返回空的工装信息
                string defaultInfo = string.Empty;
                fixturesInfo = defaultInfo.Split('+');
            }
            return fixturesInfo;
        }

        /// <summary>
        /// 比较两个字符串数组的内容是否相同，不考虑顺序
        /// </summary>
        /// <returns></returns>
        public static bool CompareArray(string[] array1, string[] array2)
        {
            // 找出两个数组中共同的元素
            var q = from a in array1 join b in array2 on a equals b select a;

            // 检查：1. 两个数组长度是否相等
            //      2. 共同元素的数量是否与数组长度相等
            bool flag = array1.Length == array2.Length && q.Count() == array1.Length;
            return flag;
        }

        /// <summary>
        /// 产品编码返回数组
        /// </summary>
        /// <param name="strCodedat"></param>
        /// <param name="codesDataM"></param>
        /// <returns></returns>
        public static string[] CodeMafror(string strCodedat, DataTable codesDataM)
        {
            string[] prodrow = new string[] { };
            DataRow[] rows = codesDataM.Select("TooName = '" + strCodedat + "'");
            if (rows.Length > 0)
            {
                prodrow = rows[0]["MateName"].ToString().Split('+');
            }
            return prodrow;
        }

        /// <summary>
        /// 产品编码返回string
        /// </summary>
        /// <param name="strCodedat"></param>
        /// <param name="codesDataM"></param>
        /// <returns></returns>
        public static string CodeStrfror(string strCodedat, DataTable codesDataM)
        {
            string prodrow = "";
            DataRow[] rows = codesDataM.Select("TooName = '" + strCodedat + "'");
            if (rows.Length > 0)
            {
                prodrow = rows[0]["MateName"].ToString();
            }
            return prodrow;
        }

        /// <summary>
        /// 二维码验证返回string
        /// </summary>
        /// <param name="strCodedat"></param>
        /// <param name="codesDataM"></param>
        /// <returns></returns>
        public static string CodeStrQRcode(string strCodedat, DataTable codesDataM)
        {
            string prodrow = "";
            DataRow[] rows = codesDataM.Select("TooName = '" + strCodedat + "'");
            if (rows.Length > 0)
            {
                prodrow = rows[0]["MateQRcode"].ToString();
            }
            return prodrow;
        }

        /// <summary>
        /// 如果为空返回null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NullECoshu(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                str = "null";
            }
            return str;
        }

        /// <summary>
        /// 对PLC读取过来的数据进行处理
        /// </summary>
        public static string HandlePlcData(string data, string type)
        {
            if (type.Equals("I"))
            {
                data = PNumCode(data);
            }
            else if (type.Equals("T"))
            {
                data = PNtimeCode(data);
            }
            else if (type.Equals("J"))
            {
                data = PdounInCode(type);
            }
            else if (type.Equals("H"))
            {
                data = PNumOKAG(type);
            }
            return data;
        }
    }
}
