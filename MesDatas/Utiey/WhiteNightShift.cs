using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NPOI.HSSF.Util.HSSFColor;

namespace MesDatas.Utiey
{
    public static class WhiteNightShift
    {
       public static string GetShift(DateTime time)
        {
            // 白班开始和结束时间（分钟）  
            int startMorning = 8 * 60 + 30; // 8:30  
            int endMorning = 20 * 60 + 30; // 20:30  

            // 将给定时间转换为分钟（从午夜开始）  
            int minutesOfDay = time.Hour * 60 + time.Minute;

            // 如果时间在白班范围内  
            if (minutesOfDay >= startMorning && minutesOfDay < endMorning)
            {
                return "白班";
            }
            // 否则，如果时间超过20:30（含），或者小于8:30（跨日情况）  
            else if (minutesOfDay >= endMorning || minutesOfDay < startMorning)
            {
                return "晚班";
            }

            // 理论上，上面的条件已经覆盖了所有情况，但这里留一个兜底  
            return "未知班次";
        }
    }
}
