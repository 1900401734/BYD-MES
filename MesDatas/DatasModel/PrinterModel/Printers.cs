using MesDatas.DatasServer;
using SqlSugar;
using System.ComponentModel;
namespace MesDatas.DatasModel
{
    [SugarTable("Printers")]
    public class Printers
    {
        [SugarColumn(ColumnName = "ID")]
        public int ID { get; set; }             // ID 

        [SugarColumn(ColumnName = "LanguageId", DefaultValue = "0")]
        public int LanguageId { get; set; }     // 语言ID 

        [SugarColumn(ColumnName = "Ptype", IsNullable = true)]
        public string Ptype { get; set; }       // 打印机类型 

        [SugarColumn(ColumnName = "Phead", IsNullable = true)]
        public string Phead { get; set; }       // 打印机头 

        [SugarColumn(ColumnName = "Ptime", IsNullable = true)]
        public string Ptime { get; set; }       // 条码时间格式 

        [SugarColumn(ColumnName = "Ptail", IsNullable = true)]
        public string Ptail { get; set; }       // 条码尾 

        [SugarColumn(ColumnName = "TFront", IsNullable = true)]
        public string TFront { get; set; }      // 条码前 

        [SugarColumn(ColumnName = "Tmonarch", IsNullable = true)]
        public string Tmonarch { get; set; }    // 条码后 

        [SugarColumn(ColumnName = "Tlow", IsNullable = true)]
        public string Tlow { get; set; }        // 条码下 

        [SugarColumn(ColumnName = "Pertow", IsNullable = true)]
        public string Pertow { get; set; }      // 是否打印二个条码 

        [SugarColumn(ColumnName = "Txttow", IsNullable = true)]
        public string Txttow { get; set; }      // 是否打印文字 

        [SugarColumn(ColumnName = "Plcmodel", IsNullable = true)]
        public string Plcmodel { get; set; }    // 是否读取PLC型号 

        [SugarColumn(ColumnName = "Method", IsNullable = true)]
        public string Method { get; set; }      // prn文件位置 

        [SugarColumn(ColumnName = "ip", IsNullable = true)]
        public string ip { get; set; }          // 打印机IP 

        [SugarColumn(ColumnName = "prot", IsNullable = true)]
        public string prot { get; set; }        // 打印机端口 

        [SugarColumn(ColumnName = "printCode", IsNullable = true)]
        public string printCode { get; set; }   // 打印机打印条码 

        /// <summary>
        /// 默认初始化值 Printers 
        /// </summary>
        public static Printers InitGetPrinters()
        {
            Printers printers = new Printers();
            printers.ID = 1;
            printers.LanguageId = 0;
            return printers;
        }

        //保存 Printers 
        public string Save()
        {
            return PrintersServer.GetPrintersSave(this);
        }

        //更新 Printers 
        public string Update()
        {
            return PrintersServer.GetPrintersUpdate(this);
        }

        //删除 Printers 
        public string Delete()
        {
            return PrintersServer.GetPrintersDelete(this);
        }
    }
}
