using MesDatas.DatasServer;
using SqlSugar;
using System;
namespace MesDatas.DatasModel
{
    [SugarTable("SytemSetDerived")]
    public class SytemSetDerived
    {
        /// <summary>
        /// ID 
        /// </summary>
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 语言ID 
        /// </summary>
        [SugarColumn(ColumnName = "LanguageId", DefaultValue = "0")]
        public int LanguageId { get; set; }

        /// <summary>
        /// 数据初始化显示null 
        /// </summary>
        [SugarColumn(ColumnName = "SytemSetnullCoden", IsNullable = true)]
        public string SytemSetnullCoden { get; set; }

        /// <summary>
        /// 条形码屏蔽
        /// </summary>
        [SugarColumn(ColumnName = "SytemNoerifbarcodes", DefaultValue = "false")]
        public bool SytemNoerifbarcodes { get; set; }

        /// <summary>
        /// 工装屏蔽
        /// </summary>
        [SugarColumn(ColumnName = "SytemNorifytooling", DefaultValue = "false")]
        public bool SytemNorifytooling { get; set; }

        /// <summary>
        /// 二维码屏蔽
        /// </summary>
        [SugarColumn(ColumnName = "SytemQRcodNorif", DefaultValue = "false")]
        public bool SytemQRcodNorif { get; set; }

        /// <summary>
        /// NG数量屏蔽
        /// </summary>
        [SugarColumn(ColumnName = "SytemNGCodesData", DefaultValue = "false")]
        public bool SytemNGCodesData { get; set; }

        /// <summary>
        /// 历史数据屏蔽
        /// </summary>
        [SugarColumn(ColumnName = "SytemHistorCodes", DefaultValue = "false")]
        public bool SytemHistorCodes { get; set; }

        /// <summary>
        /// 当前PLC类型
        /// </summary>
        [SugarColumn(ColumnName = "CurrentPLCType", IsNullable = true)]
        public string CurrentPLCType { get; set; }

        #region 数据上传相关地址

        /// <summary>
        /// 开始生产点位
        /// </summary>
        [SugarColumn(ColumnName = "StartProductPoint", IsNullable = true)]
        public string StartProductPoint { get; set; }

        /// <summary>
        /// 第二次读取条码
        /// </summary>
        [SugarColumn(ColumnName = "SecondProductPoint", IsNullable = true)]
        public string SecondProductPoint { get; set; }

        /// <summary>
        /// 第二次读取长度
        /// </summary>
        [SugarColumn(ColumnName = "SecondProductLength", IsNullable = true)]
        public string SecondProductLength { get; set; }

        /// <summary>
        /// 结束生产点位
        /// </summary>
        [SugarColumn(ColumnName = "EndProductPoint", IsNullable = true)]
        public string EndProductPoint { get; set; }

        /// <summary>
        /// 总结果点位
        /// </summary>
        [SugarColumn(ColumnName = "TotalProductPoint", IsNullable = true)]
        public string TotalProductPoint { get; set; }

        //******************** 双工位 ********************

        /// <summary>
        /// 开始生产点位1
        /// </summary>
        [SugarColumn(ColumnName = "StartProductPoint1", IsNullable = true)]
        public string StartProductPoint1 { get; set; }

        /// <summary>
        /// 结束生产点位1
        /// </summary>
        [SugarColumn(ColumnName = "EndProductPoint1", IsNullable = true)]
        public string EndProductPoint1 { get; set; }

        /// <summary>
        /// 总结果点位1
        /// </summary>
        [SugarColumn(ColumnName = "TotalProductPoint1", IsNullable = true)]
        public string TotalProductPoint1 { get; set; }

        /// <summary>
        /// 开始生产点位2
        /// </summary>
        [SugarColumn(ColumnName = "StartProductPoint2", IsNullable = true)]
        public string StartProductPoint2 { get; set; }

        /// <summary>
        /// 结束生产点位2
        /// </summary>
        [SugarColumn(ColumnName = "EndProductPoint2", IsNullable = true)]
        public string EndProductPoint2 { get; set; }

        /// <summary>
        /// 总结果点位2
        /// </summary>
        [SugarColumn(ColumnName = "TotalProductPoint2", IsNullable = true)]
        public string TotalProductPoint2 { get; set; }

        /// <summary>
        /// 二次条码1
        /// </summary>
        [SugarColumn(ColumnName = "SecondPoint1", IsNullable = true)]
        public string SecondPoint1 { get; set; }

        /// <summary>
        /// 二次条码2
        /// </summary>
        [SugarColumn(ColumnName = "SecondPoint2", IsNullable = true)]
        public string SecondPoint2 { get; set; }

        /// <summary>
        /// 二次长度1
        /// </summary>
        [SugarColumn(ColumnName = "SecondLength1", IsNullable = true)]
        public string SecondLength1 { get; set; }

        /// <summary>
        /// 二次长度2
        /// </summary>
        [SugarColumn(ColumnName = "SecondLength2", IsNullable = true)]
        public string SecondLength2 { get; set; }


        //******************** 左右款 ********************

        /// <summary>
        /// 左右款触发1
        /// </summary>
        [SugarColumn(ColumnName = "LRTrigger1", IsNullable = true)]
        public string LRTrigger1 { get; set; }

        /// <summary>
        /// 左右款触发2
        /// </summary>
        [SugarColumn(ColumnName = "LRTrigger2", IsNullable = true)]
        public string LRTrigger2 { get; set; }

        //******************** 工装验证 ********************

        /// <summary>
        /// 触发工装验证
        /// </summary>
        [SugarColumn(ColumnName = "FixtureValidate", IsNullable = true)]
        public string FixtureValidate { get; set; } = "D1300";

        /// <summary>
        /// 工装验证成功
        /// </summary>
        [SugarColumn(ColumnName = "FixtureOK", IsNullable = true)]
        public string FixtureOK { get; set; } = "D1303";

        /// <summary>
        /// 工装条码
        /// </summary>
        [SugarColumn(ColumnName = "FixtureNumber", IsNullable = true)]
        public string FixtureNumber { get; set; } = "D1350";

        /// <summary>
        /// 工装条码长度
        /// </summary>
        [SugarColumn(ColumnName = "FixtureLength", IsNullable = true)]
        public string FixtureLength { get; set; } = "20";

        #endregion

        /// <summary>
        /// 自动生成条码（包含条码验证）
        /// </summary>
        [SugarColumn(ColumnName = "ISAutoGenerate", IsNullable = true)]
        public bool ISAutoGenerateBarcode { get; set; }

        /// <summary>
        /// 跳过本地条码验证
        /// </summary>
        [SugarColumn(ColumnName = "SkipBarcodeVerifyLocally", IsNullable = true)]
        public bool IsSkipBarcodeVerifyLocally { get; set; }

        /// <summary>
        /// 允许重复上传
        /// </summary>
        [SugarColumn(ColumnName = "AllowUploadContinuously", IsNullable = true)]
        public bool AllowUploadContinuously { get; set; }

        /// <summary>
        /// 绑定工单
        /// </summary>
        [SugarColumn(ColumnName = "IsOrderNumberBinding", IsNullable = true)]
        public bool IsOrderNumberBinding { get; set; }

        /// <summary>
        /// MES用户绑定
        /// </summary>
        [SugarColumn(ColumnName = "IsUserBinding", IsNullable = true)]
        public bool IsUserBinding { get; set; }

        /// <summary>
        /// 开机自启
        /// </summary>
        [SugarColumn(ColumnName = "IsAutoLaunch", IsNullable = true)]
        public bool IsAutoLaunch { get; set; }

        /// <summary>
        /// ADM权限自动退出
        /// </summary>
        [SugarColumn(ColumnName = "IsAutoExit", IsNullable = true)]
        public bool IsAutoExit { get; set; }

        /// <summary>
        /// 流水号：5位，每天重置，范围 00000~99999） 
        /// </summary>
        [SugarColumn(ColumnName = "SerialNumber", IsNullable = true)]
        public string SerialNumber { get; set; }

        /// <summary>
        /// 码号
        /// </summary>
        [SugarColumn(ColumnName = "BarcodeNumber", IsNullable = true)]
        public string BarcodeNumber { get; set; }

        /// <summary>
        /// 左右款
        /// </summary>
        [SugarColumn(ColumnName = "IsLeftRight", IsNullable = true)]
        public bool IsLeftRight { get; set; }

        /// <summary>
        /// 显示条码
        /// </summary>
        [SugarColumn(ColumnName = "IsDoubleStation", IsNullable = true)]
        public bool IsDoubleStation { get; set; }

        /// <summary>
        /// 工位数量
        /// </summary>
        [SugarColumn(ColumnName = "StationCount", IsNullable = true)]
        public string StationCount { get; set; }

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
            if (string.IsNullOrWhiteSpace(FixtureValidate))
            {
                FixtureValidate = "D1300";
            }
            if (string.IsNullOrWhiteSpace(FixtureOK))
            {
                FixtureOK = "D1303";
            }
            if (string.IsNullOrWhiteSpace(FixtureNumber))
            {
                FixtureNumber = "D1350";
            }
            if (string.IsNullOrWhiteSpace(FixtureLength))
            {
                FixtureLength = "20";
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
