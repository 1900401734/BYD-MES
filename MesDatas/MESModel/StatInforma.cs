using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.MESModel
{
    public class StatInforma
    {

        public string ORDER { get; set; }

        public string ITEM { get; set; }

        public string ITEM_DESC { get; set; }

        public string ROUTER { get; set; }

        public string ORDER_NUM { get; set; }

        public string COMP_NUM { get; set; }

        public string COMP_RATE { get; set; }

        public string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6}", ORDER, ITEM, ITEM_DESC, ROUTER, ORDER_NUM, COMP_NUM, COMP_RATE);
        }
    }
}
