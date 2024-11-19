using MesDatas.DatasServer;
using SqlSugar;
using System;
namespace MesDatas.DatasModel
{
    [SugarTable("SytemSetDerived")]
    public class SytemSetDerived
    {
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }                     // ID 

        [SugarColumn(ColumnName = "LanguageId", DefaultValue = "0")]
        public int LanguageId { get; set; }             // 语言ID 

        [SugarColumn(ColumnName = "SytemSetnullCoden", IsNullable = true)]
        public string SytemSetnullCoden { get; set; }   // 数据初始化显示null 

        [SugarColumn(ColumnName = "SytemSetnullCoden_CN", IsNullable = true)]
        public string SytemSetnullCoden_CN { get; set; }// 语言显示

        [SugarColumn(ColumnName = "SytemNoerifbarcodes", DefaultValue = "false")]
        public bool SytemNoerifbarcodes { get; set; }   // 条形码屏蔽

        [SugarColumn(ColumnName = "SytemNorifytooling", DefaultValue = "false")]
        public bool SytemNorifytooling { get; set; }    // 工装屏蔽

        [SugarColumn(ColumnName = "SytemQRcodNorif", DefaultValue = "false")]
        public bool SytemQRcodNorif { get; set; }       // 二维码屏蔽

        [SugarColumn(ColumnName = "SytemNGCodesData", DefaultValue = "false")]
        public bool SytemNGCodesData { get; set; }      // NG数量屏蔽

        [SugarColumn(ColumnName = "SytemHistorCodes", DefaultValue = "false")]
        public bool SytemHistorCodes { get; set; }      // 历史数据屏蔽

        [SugarColumn(ColumnName = "CurrentPLCType", IsNullable = true)]
        public string CurrentPLCType { get; set; }      // 当前PLC类型

        [SugarColumn(ColumnName = "StartProductPoint", IsNullable = true)]
        public string StartProductPoint { get; set; }   // 开始生产点位

        [SugarColumn(ColumnName = "SecondProductPoint", IsNullable = true)]
        public string SecondProductPoint { get; set; }  // 第二次读取条码

        [SugarColumn(ColumnName = "SecondProductLength", IsNullable = true)]
        public string SecondProductLength { get; set; } // 第二次读取长度

        [SugarColumn(ColumnName = "EndProductPoint", IsNullable = true)]
        public string EndProductPoint { get; set; }     // 结束生产点位

        [SugarColumn(ColumnName = "TotalProductPoint", IsNullable = true)]
        public string TotalProductPoint { get; set; }   // 总结果点位

        [SugarColumn(ColumnName = "ISAutoGenerate", IsNullable = true)]
        public bool ISAutoGenerateBarcode { get; set; }   // 是否自动生成条码

        [SugarColumn(ColumnName = "SerialNumber", IsNullable = true)]
        public string SerialNumber { get; set; }    // 流水号：5位，每天重置，范围 00000~99999） 

        [SugarColumn(ColumnName = "BarcodeNumber", IsNullable = true)]
        public string BarcodeNumber { get; set; }   // 码号

        [SugarColumn(ColumnName = "TotalBarcodeCount", IsNullable = true)]
        public int TotalBarcodeCount { get; set; }   // 读取的条码总数

        [SugarColumn(ColumnName = "BarcodeNGCount", IsNullable = true)]
        public int BarcodeNGCount { get; set; }   // 条码验证NG数量

        [SugarColumn(ColumnName = "BarcodeOKCount", IsNullable = true)]
        public int BarcodeOKCount { get; set; }   // 条码验证OK数量

        [SugarColumn(ColumnName = "TotalMESCount", IsNullable = true)]
        public int TotalMESCount { get; set; }   // 读取的条码总数

        [SugarColumn(ColumnName = "MESNGCount", IsNullable = true)]
        public int MESNGCount { get; set; }   // 条码验证NG数量

        [SugarColumn(ColumnName = "MESOKCount", IsNullable = true)]
        public int MESOKCount { get; set; }   // 条码验证OK数量


        public static SytemSetDerived SytemSetDerivedDMESS()
        {
            SytemSetDerived sytemSetDerived = new SytemSetDerived();
            sytemSetDerived.ID = 1;
            sytemSetDerived.LanguageId = 0;
            sytemSetDerived.SytemSetnullCoden = "null";
            sytemSetDerived.StartProductPoint = "D1200";
            sytemSetDerived.SecondProductPoint = "D1050";
            sytemSetDerived.SecondProductLength = "10";
            sytemSetDerived.EndProductPoint = "D1202";
            sytemSetDerived.TotalProductPoint = "D1078";

            return sytemSetDerived;
        }

        // 文本框不填写点位时，默认保存预设点位
        public string Save()
        {
            if (string.IsNullOrWhiteSpace(StartProductPoint))
            {
                StartProductPoint = "D1200";
            }
            if (string.IsNullOrWhiteSpace(SecondProductPoint))
            {
                SecondProductPoint = "D1050";
            }
            if (string.IsNullOrWhiteSpace(SecondProductLength))
            {
                SecondProductLength = "10";
            }
            if (string.IsNullOrWhiteSpace(EndProductPoint))
            {
                EndProductPoint = "D1202";
            }
            if (string.IsNullOrWhiteSpace(TotalProductPoint))
            {
                TotalProductPoint = "D1078";
            }

            return SytemSetDerivedServer.GetSytemSetDerivedSave(this);
        }

        public string Update()
        {
            return SytemSetDerivedServer.GetSytemSetDerivedUpdate(this);
        }

        public string Delete()
        {
            return SytemSetDerivedServer.GetSytemSetDerivedDelete(this);
        }
    }
}
