using MesDatas.DatasServer;
using SqlSugar;
namespace MesDatas.DatasModel
{
    [SugarTable("PrinterSetting")]
    public class PrinterSetting
    {
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }                     // ID 

        [SugarColumn(ColumnName = "LanguageId", DefaultValue = "0")]
        public int LanguageId { get; set; }             // 语言ID 

        [SugarColumn(ColumnName = "PrinterStep", IsNullable = true)]
        public string PrinterStep { get; set; }         // 打印机步骤 

        [SugarColumn(ColumnName = "PrinteSendResult", IsNullable = true)]
        public string PrinteSendResult { get; set; }    // 打印机发送结果 

        [SugarColumn(ColumnName = "ControlledByPLC", IsNullable = true)]
        public bool ControlledByPLC { get; set; }       // PLC控制打印

        [SugarColumn(ColumnName = "PrintMode", IsNullable = true)]
        public string PrintMode { get; set; }           // 打印模式


        public static PrinterSetting PrinterSettingInitalize()
        {
            PrinterSetting printerSetting = new PrinterSetting();
            printerSetting.ID = 1;
            printerSetting.LanguageId = 0;
            printerSetting.PrinterStep = "D1012";
            printerSetting.PrinteSendResult = "D1014";

            return printerSetting;
        }

        public string Save()
        {
            if (string.IsNullOrWhiteSpace(PrinterStep))
            {
                PrinterStep = "D1012";
            }
            if (string.IsNullOrWhiteSpace(PrinteSendResult))
            {
                PrinteSendResult = "D1014";
            }

            return PrinterSettingServer.GetPrinterSettingSave(this);

        }
        public string Update()
        {
            return PrinterSettingServer.GetPrinterSettingUpdate(this);
        }
        public string Delete()
        {
            return PrinterSettingServer.GetPrinterSettingDelete(this);
        }
    }
}
