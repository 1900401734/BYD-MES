using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.Utiey
{
    class TypeRead
    {
        /// <summary>
        /// 根据指定的转换类型将PLC值转换为相应的字符串表示。
        /// </summary>
        /// <param name="plcValue">要转换的PLC值</param>
        /// <param name="number">转换类型（0-6）</param>
        /// <returns>转换后的字符串结果</returns>
        public static string NumericOperate(string plcValue, string number)
        {
            string outputStr = "";

            // ÷100
            if (number.Equals("0"))
            {
                outputStr = CodeNum.DivBy100(plcValue);
            }

            // ÷10
            else if (number.Equals("1"))
            {
                outputStr = CodeNum.DivBy10(plcValue);
            }

            // ÷100（保留两位小数）
            else if (number.Equals("2"))
            {
                outputStr = CodeNum.DivBy100Rounded2(plcValue);
            }

            // 转成OK/NG
            else if (number.Equals("3"))
            {
                outputStr = CodeNum.ConvertToOkNg(plcValue);
            }

            // 实际值
            else if (number.Equals("4"))
            {
                outputStr = plcValue;
            }

            // ÷1000
            else if (number.Equals("5"))
            {
                outputStr = CodeNum.DivBy1000(plcValue);
            }

            // ÷1000（保留3位小数）
            else if (number.Equals("6"))
            {
                outputStr = CodeNum.DivBy1000Rounded3(plcValue);
            }

            return outputStr;
        }
    }
}
