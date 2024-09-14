
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.Entity
{
    public class StatisticsValue
    {
        private string _statname;
        private string _statvalue;

        public string StatName
        {
            get { return _statname; }
            set { _statname = value; }
        }

        public string StatValue
        {
            get { return _statvalue; }
            set { _statvalue = value; }
        }
    }
}
