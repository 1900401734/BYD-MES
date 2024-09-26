using MesDatas.DatasServer;
using SqlSugar;
using System;
using System.ComponentModel;
namespace MesDatas.DatasModel
{
    [SugarTable("printersBtw")]
    public class PrintersBtw
    {
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }                 // ID 

        [SugarColumn(ColumnName = "LanguageId", DefaultValue = "0")]
        public int LanguageId { get; set; }         // 语言ID 

        [SugarColumn(ColumnName = "PtypeBtw", IsNullable = true)]
        public string PtypeBtw { get; set; }        // 打印机类型 

        [SugarColumn(ColumnName = "PheadBtw", IsNullable = true)]
        public string PheadBtw { get; set; }        // 打印机头 

        [SugarColumn(ColumnName = "PtimeBtw", IsNullable = true)]
        public string PtimeBtw { get; set; }        // 条码时间格式 

        [SugarColumn(ColumnName = "PtailBtw", IsNullable = true)]
        public string PModel { get; set; }        // 条码尾 (产品型号)

        [SugarColumn(ColumnName = "TFrontBtw", IsNullable = true)]
        public string TFrontBtw { get; set; }       // 条码前 

        [SugarColumn(ColumnName = "TmonarchBtw", IsNullable = true)]
        public string SerialNumber { get; set; }     // 条码后（流水号：5位，每天重置，范围 00000~99999） 

        [SugarColumn(ColumnName = "LastSavedDate", IsNullable = true)]
        public DateTime LastSavedDate { get; set; }     // 流水号最后保存日期

        [SugarColumn(ColumnName = "TlowBtw", IsNullable = true)]
        public string TlowBtw { get; set; }         // 条码下 

        [SugarColumn(ColumnName = "PertowBtw", IsNullable = true)]
        public string PertowBtw { get; set; }       // 是否打印二个条码 

        [SugarColumn(ColumnName = "TxttowBtw", IsNullable = true)]
        public string TxttowBtw { get; set; }       // 是否打印文字 

        [SugarColumn(ColumnName = "PlcmodelBtw", IsNullable = true)]
        public bool IsLoadModel_PLC { get; set; }       // 是否读取PLC型号 

        [SugarColumn(ColumnName = "MethodBtw", IsNullable = true)]
        public bool MethodBtw { get; set; }         // prn文件位置 

        [SugarColumn(ColumnName = "ipBtw", IsNullable = true)]
        public string ipBtw { get; set; }           // 打印机IP 

        [SugarColumn(ColumnName = "portBtw", IsNullable = true)]
        public string portBtw { get; set; }         // 打印机端口 

        [SugarColumn(ColumnName = "printCodeBtw", IsNullable = true)]
        public string printCodeBtw { get; set; }    // 打印机打印条码 

        [SugarColumn(ColumnName = "PrintNowDateTime", IsNullable = true)]
        public bool PrintNowDateTime { get; set; }  // 打印机打印条码 

        [SugarColumn(ColumnName = "PrintSerialNumber", IsNullable = true)]
        public bool PrintSerialNumber { get; set; } // 打印机打印条码 

        [SugarColumn(ColumnName = "PrintCodeTwoBool", IsNullable = true)]
        public bool PrintCodeTwoBool { get; set; }  // 打印机打印条码 



        // 默认初始化值 printersBtw 
        public static PrintersBtw InitGetPrintersBtw()
        {
            PrintersBtw printersbtw = new PrintersBtw();
            printersbtw.ID = 1;
            printersbtw.LanguageId = 0;
            return printersbtw;
        }

        // 保存 printersBtw 
        public string Save()
        {
            return PrintersBtwServer.GetPrintersBtwSave(this);
        }

        // 更新 printersBtw 
        public string Update()
        {
            return PrintersBtwServer.GetPrintersBtwUpdate(this);
        }

        // 删除 printersBtw 
        public string Delete()
        {
            return PrintersBtwServer.GetPrintersBtwDelete(this);
        }
    }
}
