using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.Utiey
{
    class Bocmdb
    {
        public static string BoardCode(string boardBeat,string strtyp)
        {
           if (boardBeat != null)
            {
                if (strtyp == "节拍")
                {
                    boardBeat = CodeNum.DivBy10(boardBeat);
                }
                else if (strtyp == "结果")
                {
                    boardBeat = CodeNum.ConvertToOkNg(boardBeat);
                }
                else
                {
                    boardBeat = CodeNum.DivBy100(boardBeat);
                }
            }
            else
            {
                boardBeat = "  ";
            }
            return boardBeat;
        }
        
    }
}
