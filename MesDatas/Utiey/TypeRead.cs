using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.Utiey
{
    class TypeRead
    {
        public static string Typerdess(string plcValue, string typelen)
        {
            string bb = "";

            // ÷100
            if (typelen.Equals("0"))
            {
                bb = CodeNum.PNumCode(plcValue);
            }

            // ÷10
            else if (typelen.Equals("1"))
            {
                bb = CodeNum.PNtimeCode(plcValue);
            }

            // ÷100（保留两位小数）
            else if (typelen.Equals("2"))
            {
                bb = CodeNum.PdounInCode(plcValue);
            }

            // 转成OK/NG
            else if (typelen.Equals("3"))
            {
                bb = CodeNum.PNumOKAG(plcValue);
            }

            // 实际值
            else if (typelen.Equals("4"))
            {
                bb = plcValue;
            }

            // ÷1000
            else if (typelen.Equals("5"))
            {
                bb = CodeNum.PThousdCode(plcValue);
            }

            // ÷1000（保留3位小数）
            else if (typelen.Equals("6"))
            {
                bb = CodeNum.PThouInCode(plcValue);
            }

            return bb;
        }
    }
}
