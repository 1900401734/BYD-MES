using MesDatas.DatasServer;
using SqlSugar;
using System;
namespace MesDatas.DatasModel
{
    [SugarTable("Printer")]
    public class Printer
    {
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }                     // ID 

        [SugarColumn(ColumnName = "LanguageId", DefaultValue = "0")]
        public int LanguageId { get; set; }             // 语言ID 

        #region 打印机基本配置

        /// <summary>
        /// 打印指示：D1012（打新条码 = 1，重新打码 = 2）
        /// </summary>
        [SugarColumn(ColumnName = "StartPoint", IsNullable = true)]
        public string StepOfPrint { get; set; }

        /// <summary>
        /// 打印结果：D1014（成功 = 1，失败 = 2）
        /// </summary>
        [SugarColumn(ColumnName = "EndPoint", IsNullable = true)]
        public string ResultOfPrint { get; set; }

        /// <summary>
        /// PLC控制打印
        /// </summary>
        [SugarColumn(ColumnName = "ControlledByPLC", IsNullable = true)]
        public bool IsControlledByPLC { get; set; }

        #endregion

        #region 打印模式配置

        /// <summary>
        /// 打印模式
        /// </summary>
        [SugarColumn(ColumnName = "PrintMode", IsNullable = true)]
        public string PrintMode { get; set; }

        /// <summary>
        /// 打印机名称
        /// </summary>
        [SugarColumn(ColumnName = "PrinterName", IsNullable = true)]
        public string PrinterName { get; set; }

        /// <summary>
        /// 打印机IP 
        /// </summary>
        [SugarColumn(ColumnName = "IP", IsNullable = true)]
        public string IP { get; set; }

        /// <summary>
        /// 打印机端口
        /// </summary>
        [SugarColumn(ColumnName = "Port", IsNullable = true)]
        public string Port { get; set; }

        #endregion

        #region 文本配置

        /// <summary>
        /// 条码前端
        /// </summary>
        [SugarColumn(ColumnName = "BeforeBarcode", IsNullable = true)]
        public string BeforeBarcode { get; set; }

        /// <summary>
        /// 条码后端
        /// </summary>
        [SugarColumn(ColumnName = "AfterBarcode", IsNullable = true)]
        public string AfterBarcode { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        [SugarColumn(ColumnName = "ProductModel", IsNullable = true)]
        public string ProductModel { get; set; }

        /// <summary>
        /// 启用文本打印
        /// </summary>
        [SugarColumn(ColumnName = "UseFont", IsNullable = true)]
        public bool IsUseFont { get; set; }

        /// <summary>
        /// 从PLC读取产品型号
        /// </summary>
        [SugarColumn(ColumnName = "ReadModelFromPLC", IsNullable = true)]
        public bool IsLoadModel_PLC { get; set; }

        #endregion

        #region 动态码配置

        /// <summary>
        /// 码号
        /// </summary>
        [SugarColumn(ColumnName = "BarcodeNumber", IsNullable = true)]
        public string BarcodeNumber { get; set; }

        /// <summary>
        /// 流水号：5位，每天重置，范围 00000~99999
        /// </summary>
        [SugarColumn(ColumnName = "SerialNumber", IsNullable = true)]
        public string SerialNumber { get; set; }

        /// <summary>
        /// 流水号最后保存日期
        /// </summary>
        [SugarColumn(ColumnName = "LastSavedDate", IsNullable = true)]
        public DateTime LastSavedDate { get; set; }

        /// <summary>
        /// 流水间隔
        /// </summary>
        [SugarColumn(ColumnName = "SerialSpan", IsNullable = true)]
        public string SerialSpan { get; set; }

        /// <summary>
        /// 打印份数
        /// </summary>
        [SugarColumn(ColumnName = "PrintCount", IsNullable = true)]
        public string PrintCount { get; set; }

        /// <summary>
        /// 自动添加日期
        /// </summary>
        [SugarColumn(ColumnName = "AutoAddDate", IsNullable = true)]
        public bool IsAutoAddDate { get; set; }

        /// <summary>
        /// 启用流水号
        /// </summary>
        [SugarColumn(ColumnName = "IsEnableSN", IsNullable = true)]
        public bool IsEnableSN { get; set; }

        #endregion

        #region 文件配置

        /// <summary>
        /// 文件保存路径
        /// </summary>
        [SugarColumn(ColumnName = "FilePath", IsNullable = true)]
        public string FilePath { get; set; }

        /// <summary>
        /// 当前选定的文件格式
        /// </summary>
        [SugarColumn(ColumnName = "FileFormat", IsNullable = true)]
        public string CurrentFileFormat { get; set; }

        #endregion

        public static Printer PrinterSettingInitalize()
        {
            Printer printerSetting = new Printer();
            printerSetting.ID = 1;
            printerSetting.LanguageId = 0;
            printerSetting.StepOfPrint = "D1012";
            printerSetting.ResultOfPrint = "D1014";

            return printerSetting;
        }

        public string Save()
        {
            if (string.IsNullOrWhiteSpace(StepOfPrint))
            {
                StepOfPrint = "D1012";
            }
            if (string.IsNullOrWhiteSpace(ResultOfPrint))
            {
                ResultOfPrint = "D1014";
            }

            return PrinterServer.GetPrinterSettingSave(this);
        }

        public string Update()
        {
            return PrinterServer.GetPrinterSettingUpdate(this);
        }

        public string Delete()
        {
            return PrinterServer.GetPrinterSettingDelete(this);
        }
    }
}
