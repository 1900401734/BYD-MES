using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.DatasModel
{
    [SugarTable("SytemSet")]
    public class SytemSetAll
    {
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true)]
        //  public int SystemSetID { get; set; }     // ID 
        // 数据库ID
        public string ID { get; set; }

        // PLC相关设置
        public string IP { get; set; }
        public string Port { get; set; }

        // 数据库路径
        public string DataUrl { get; set; }

        // 设备信息
        public string DeviceName { get; set; }
        public string wordNo { get; set; }  // 工单号
        public string Workstname { get; set; }  // 是否实时读取配方号

        // 工装绑定
        public string BoardBeat { get; set; }

        // 条码验证规则
        public string faults { get; set; }

        // 运行界面设置
        public string ResultCode { get; set; }  // 显示宽度

        // 读卡器设置
        public string rfidProt { get; set; }  // 端口
        public string rfidCode { get; set; }  // 设备号

        // 条码读取设置
        public string readBarCode { get; set; }  // 二次读取条码

        // 统计相关设置
        public string StatisticsCode { get; set; }  // 点位集合
        public string StatisticsName { get; set; }  // 名称集合
        public string StatisticsThaiName { get; set; } = "ยอดสั่งงาน|ยอดเสร็จ|ยอดผ่าน|ยอด NG|ยอดผลิต|% สำเร็จ|% ของเสีย|% ผ่านครั้งแรก|แทคไทม์|เวลารอบรวม|เวลารอบ (เครื่อง)|เวลารอบ (คน)|ยอดซ่อมบำรุง|เวลากระบวนการ|เวลาเดินเครื่อง|เวลาภาระงาน";  // 泰语名称集合
        public string StatisticsEnglishName { get; set; } = "Work Order Qty|Completed Qty|OK Qty|NG Qty|Completion Rate|Yield Rate|Defect Rate|First Pass Yield|Takt Time|Overall Cycle Time|Machine Cycle Time|Manual Cycle Time|Maintenance Count|Process Time|Utilization Time|Loading Time"; // 英语名称集合
    }
}
