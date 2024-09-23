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
        #region ---------- 数值转换 ----------

        /// <summary>
        /// 将字符串转换为 double，除以 10，再转回字符串。
        /// 如果转换失败或结果为 0，则返回原始值的字符串形式。
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string DivBy10(string input)
        {
            /*string aa = "0";
            double db = 0;
            if (double.TryParse(input, out db))
            {
                if (db != 0)
                {
                    aa = Convert.ToString((Convert.ToDouble(input)) / 10);
                }
                else
                {
                    aa = Convert.ToString((Convert.ToDouble(input)));
                }
            }
            return aa;*/

            if (double.TryParse(input, out double number))
            {
                if (number == 0)
                {
                    return "0";
                }
                return (number / 10).ToString();
            }
            return "0";
        }

        /// <summary>
        /// 将字符串转换为 double，除以 100，再转回字符串。
        /// 如果转换失败或结果为 0，则返回原始值的字符串形式。
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string DivBy100(string input)
        {
            /*string aa = "0";
            double db = 0;
            if (double.TryParse(input, out db))
            {
                if (db != 0)
                {
                    aa = Convert.ToString((Convert.ToDouble(input)) / 100);
                }
                else
                {
                    aa = Convert.ToString((Convert.ToDouble(input)));
                }
            }
            return aa;*/

            if (double.TryParse(input, out double number))
            {
                if (number == 0)
                {
                    return "0";
                }
                return (number / 100).ToString();
            }
            return "0";
        }

        /// <summary>
        /// 将字符串转换为 double，除以 100，保留两位小数，再转回字符串。
        /// 如果转换失败或结果为 0，则返回原始值的字符串形式。
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>处理后的字符串，保留两位小数</returns>
        public static string DivBy100Rounded2(string input)
        {
            /*string aa = "0";
            double db = 0;
            if (double.TryParse(input, out db))
            {
                if (db != 0)
                {
                    double mydo = Math.Round((Convert.ToDouble(input)) / 100, 2);
                    aa = mydo.ToString("0.00");
                }
                else
                {
                    aa = Convert.ToString((Convert.ToDouble(input)));
                }
            }
            return aa;*/

            if (double.TryParse(input, out double number))
            {
                if (number == 0)
                {
                    return "0.00";
                }
                return (number / 100).ToString("0.00");
            }
            return "0.00";
        }

        /// <summary>
        /// 将字符串转换为 double，除以 1000，再转回字符串。
        /// 如果转换失败或结果为 0，则返回原始值的字符串形式。
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string DivBy1000(string input)
        {
            /*string aa = "0";
            double db = 0;
            if (double.TryParse(input, out db))
            {
                if (db != 0)
                {
                    aa = Convert.ToString((Convert.ToDouble(input)) / 1000);
                }
                else
                {
                    aa = Convert.ToString((Convert.ToDouble(input)));
                }
            }
            return aa;*/

            if (double.TryParse(input, out double number))
            {
                if (number == 0)
                {
                    return "0";
                }
                return (number / 1000).ToString();
            }
            return "0";

        }

        /// <summary>
        /// 将字符串转换为 double，除以 1000，保留三位小数，再转回字符串。
        /// 如果转换失败或结果为 0，则返回原始值的字符串形式。
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>处理后的字符串，保留三位小数</returns>
        public static string DivBy1000Rounded3(string input)
        {
            /*string aa = "0";
            double db = 0;
            if (double.TryParse(input, out db))
            {
                if (db != 0)
                {
                    double mydo = Math.Round((Convert.ToDouble(input)) / 1000, 3);
                    aa = mydo.ToString("0.000");
                }
                else
                {
                    aa = Convert.ToString((Convert.ToDouble(input)));
                }
            }
            return aa;*/

            if (double.TryParse(input, out double number))
            {
                if (number == 0)
                {
                    return "0.000";
                }
                return (number / 1000).ToString("0.000");
            }
            return "0.000";
        }

        /// <summary>
        /// 将数字字符串转换为 "OK" 或 "NG"。
        /// 3 转换为 "OK"，2 转换为 "NG"，其他值返回 "null"。
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>"OK", "NG", 或 "null"</returns>
        public static string ConvertToOkNg(string input)
        {
            /*string aa = "null";
            int db = 0;
            if (int.TryParse(input, out db))
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
            return aa;*/

            if (int.TryParse(input, out int number))
            {
                if (number == 2)
                {
                    return "NG";
                }
                else if (number == 3)
                {
                    return "OK";
                }
            }
            return "null";
        }

        #endregion

        /// <summary>
        /// 清理字符串中的特殊字符（\0, ?, \r, \n）并去除两端空白。
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>清理后的字符串</returns>
        public static string CleanString(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (input.IndexOf("\0") >= 0)
                {
                    input = input.Replace("\0", "");
                }
                if (input.IndexOf("?") >= 0)
                {
                    input = input.Replace("?", "");
                }
                if (input.IndexOf("\r") >= 0)
                {
                    input = input.Replace("\r", "");
                }
                if (input.IndexOf("\n") >= 0)
                {
                    input = input.Replace("\n", "");
                }
                input = input.Trim();
            }
            return input;
        }

        /// <summary>
        /// 处理工位
        /// </summary>
        /// <param name="targetStation"></param>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public static List<string> WorkIDName(string[] targetStation, string[] stationName)
        {
            List<string> workstNamelist = new List<string>();
            string worName = "  ";

            for (int i = 0; i < targetStation.Length; i++)
            {
                string workID = targetStation[i];
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
                    if (int.TryParse(targetStation[i], out id))
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
        /// 通过工位 ID 获取对应的工位名称。
        /// </summary>
        /// <param name="workstationId">工位 ID</param>
        /// <param name="stationNames">工位名称数组</param>
        /// <returns>对应的工位名称，如果未找到则返回空字符串</returns>
        public static string GetWorkstationNameById(string workstID, string[] stationName)
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

        /*/// <summary>
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
        }*/

        /// <summary>
        /// 计算数组中不等于 "NO" 的元素数量。
        /// </summary>
        /// <param name="testItems">输入的字符串数组</param>
        /// <returns>不等于 "NO" 的元素数量</returns>
        public static int GetValidItems(string[] testItems)
        {
            int count = 0;

            for (int i = 0; i < testItems.Length; i++)
            {
                if (!testItems[i].Equals("NO"))
                {
                    count++;
                }
            }
            return count;
        }

        /*public static int GetValidItems(List<string> testItems)
        {
            int count = 0;

            for (int i = 0; i < testItems.Count; i++)
            {
                if (!testItems[i].Equals("NO"))
                {
                    count++;
                }
            }
            return count;
        }*/


        /// <summary>
        /// 如果输入为空或空白，返回 "NO"，否则返回原始输入。
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>"NO" 或原始输入</returns>
        public static string GetNoIfEmpty(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                input = "NO";
            }
            return input;
        }

        /// <summary>
        /// 如果输入为空或空白，返回 "+1"，否则返回原始输入。
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>"+1" 或原始输入</returns>
        public static string GetPlusOneIfEmpty(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                input = "+1";
            }
            return input;
        }

        /// <summary>
        /// 如果输入为空或空白，返回 "1"，否则返回原始输入。
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>"1" 或原始输入</returns>
        public static string GetOneIfEmpty(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                input = "1";
            }
            return input;
        }

        /// <summary>
        /// 如果输入为空或空白，返回空格，否则返回原始输入。
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>空格或原始输入</returns>
        public static string GetNullUnit(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                input = "   ";
            }
            return input;
        }

        /// <summary>
        /// 尝试将字符串转换为整数，如果失败则返回默认值 100。
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>转换后的整数或 100</returns>
        public static int ParseIntOrDefault(string input)
        {
            int couiou = 0;
            if (!int.TryParse(input, out couiou))
            {
                couiou = 100;
            }
            return couiou;
        }

        /// <summary>
        /// 尝试将字符串转换为 ushort，如果失败则返回默认值 200。
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>转换后的 ushort 或 200</returns>
        public static ushort ParseUshortOrDefault(string input)
        {
            ushort couiou = 0;
            if (!ushort.TryParse(input, out couiou))
            {
                couiou = 200;
            }
            return couiou;
        }

        /// <summary>
        /// 从条码验证型号与工装编号字符串中提取条码验证规则。
        /// 条码验证规则是以 '|' 分隔的第一个部分。
        /// </summary>
        /// <param name="barcodeAndFixturesInfo">包含条码验证规则和工装编号的字符串</param>
        /// <returns>提取的条码验证规则</returns>
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
        /// 从条码验证型号与工装编号字符串中提取工装编号信息。
        /// 工装编号是以 '|' 分隔的第二个部分，可能包含多个以 '+' 连接的编号。
        /// </summary>
        /// <param name="barcodeAndFixturesInfo">包含条码验证规则和工装编号的字符串</param>
        /// <returns>提取的工装编号数组</returns>
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
        /// 比较两个字符串数组的内容是否相同，不考虑元素顺序。
        /// </summary>
        /// <param name="array1">第一个字符串数组</param>
        /// <param name="array2">第二个字符串数组</param>
        /// <returns>如果数组内容相同返回 true，否则返回 false</returns>
        public static bool CompareArray(string[] array1, string[] array2)
        {
            // 找出两个数组中共同的元素
            var q = from a in array1 join b in array2 on a equals b select a;

            // 检查：1. 两个数组长度是否相等
            //      2. 共同元素的数量是否与数组长度相等
            bool result = array1.Length == array2.Length && q.Count() == array1.Length;
            return result;
        }

        /// <summary>
        /// 根据条码验证规则从配方信息表格中查找并返回相应的产品编码数组。
        /// 产品编码 = 产品物料号1 + 产品物料号2 + 产品物料号3
        /// </summary>
        /// <param name="barcodeRule">条码验证规则</param>
        /// <param name="recipeInfoTable">配方信息表格</param>
        /// <returns>与条码规则对应的产品编码数组，如果未找到则返回空数组</returns>
        public static string[] GetProductCodes(string barcodeRule, DataTable recipeInfoTable)
        {
            string[] productCode = new string[] { };
            DataRow[] rows = recipeInfoTable.Select($"TooName = '{barcodeRule}'");  // TooName:条码验证规则
            if (rows.Length > 0)
            {
                productCode = rows[0]["MateName"].ToString().Split('+');    // MateName:产品编码
            }
            return productCode;
        }

        /// <summary>
        /// 获取条码规则对应的完整产品编码字符串
        /// </summary>
        /// <returns>完整的产品编码字符串，如果未找到则返回空字符串</returns>
        public static string GetProductCodeString(string barcodeRule, DataTable recipeInfoTable)
        {
            string productCode = "";
            DataRow[] rows = recipeInfoTable.Select($"TooName = '{barcodeRule}'");
            if (rows.Length > 0)
            {
                productCode = rows[0]["MateName"].ToString();
            }
            return productCode;
        }

        /// <summary>
        /// 根据条码验证规则从配方信息表格中查找并返回二维码验证字符串
        /// </summary>
        /// <returns>二维码验证字符串，如果未找到则返回空字符串</returns>
        public static string GetQRCodeVerification(string strCodedat, DataTable codesDataM)
        {
            string QRCodeStr = "";
            DataRow[] rows = codesDataM.Select($"TooName = '{strCodedat}'");
            if (rows.Length > 0)
            {
                QRCodeStr = rows[0]["MateQRcode"].ToString();
            }
            return QRCodeStr;
        }

        /// <summary>
        /// 如果输入字符串为空或仅包含空白字符，则返回 "null"，否则返回原始字符串。
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>"null" 或原始字符串</returns>
        public static string GetNullIfEmpty(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                input = "null";
            }
            return input;
        }

        /// <summary>
        /// 根据指定的类型处理从 PLC 读取的数据。
        /// </summary>
        /// <param name="data">要处理的数据</param>
        /// <param name="type">数据处理类型：
        ///   "I" - 除以 100
        ///   "T" - 除以 10
        ///   "J" - 除以 100 并保留 2 位小数
        ///   "H" - 转换为 "OK" 或 "NG"
        /// </param>
        /// <returns>处理后的数据字符串</returns>
        public static string HandlePlcData(string data, string type)
        {
            if (type.Equals("I"))
            {
                data = DivBy100(data);
            }
            else if (type.Equals("T"))
            {
                data = DivBy10(data);
            }
            else if (type.Equals("J"))
            {
                data = DivBy100Rounded2(type);
            }
            else if (type.Equals("H"))
            {
                data = ConvertToOkNg(type);
            }
            return data;
        }
    }
}
