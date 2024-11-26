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

        [SugarColumn(ColumnName = "PrintStep", IsNullable = false)]
        public string PrintStep { get; set; }         // 打印机步骤 

        [SugarColumn(ColumnName = "PrintResult", IsNullable = true)]
        public string PrintResult { get; set; }    // 打印机发送结果 

        [SugarColumn(ColumnName = "ControlledByPLC", IsNullable = true)]
        public bool ControlledByPLC { get; set; }       // PLC控制打印

        [SugarColumn(ColumnName = "PrintMode", IsNullable = true)]
        public string PrintMode { get; set; }           // 打印模式


        public static PrinterSetting PrinterSettingInitalize()
        {
            PrinterSetting printerSetting = new PrinterSetting();
            printerSetting.ID = 1;
            printerSetting.LanguageId = 0;
            printerSetting.PrintStep = "D1012";
            printerSetting.PrintResult = "D1014";

            return printerSetting;
        }

        public string Save()
        {
            if (string.IsNullOrWhiteSpace(PrintStep))
            {
                PrintStep = "D1012";
            }
            if (string.IsNullOrWhiteSpace(PrintResult))
            {
                PrintResult = "D1014";
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
