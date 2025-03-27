using MesDatas.DatasServer;
using SqlSugar;
using System.ComponentModel;
namespace MesDatas.DatasModel
{
    [SugarTable("BarcodeVefictn")]
    public class BarcodeVerification
    {
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        [DisplayName("ID")]
        public int ID { get; set; }                     // ID 

        [SugarColumn(ColumnName = "LanguageId", DefaultValue = "0")]
        [DisplayName("语言ID")]
        public int LanguageId { get; set; }             // 语言ID 

        #region ---------- 点位合集 ----------

        /// <summary>
        /// 一般情况下为D1000，用户可自定义
        /// </summary>
        [SugarColumn(ColumnName = "BarcodeStartPLC", IsNullable = true)]
        [DisplayName("开始读取点位")]
        public string BarcodeStartPLC { get; set; }     // 开始读取点位 

        /// <summary>
        /// 默认为D1000，可自定义
        /// </summary>
        [SugarColumn(ColumnName = "BarcodePositinPLC", IsNullable = false)]
        [DisplayName("读取条码点位")]
        public string BarcodePositionPLC { get; set; }  // 读取条码点位 

        /// <summary>
        /// 条码长度：10位
        /// </summary>
        [SugarColumn(ColumnName = "BarcodeLengthPLC", IsNullable = false)]
        [DisplayName("读取条码长度点位")]
        public string BarcodeLengthPLC { get; set; }    // 读取条码长度点位 

        /// <summary>
        /// <para>D1003</para>
        /// <para>条码验证通过反馈1；</para>
        /// <para>工装验证通过反馈2；</para>
        /// <para>物料验证通过反馈3。</para>
        /// </summary>
        [SugarColumn(ColumnName = "PassBarcodeEndPLC", IsNullable = false)]
        [DisplayName("验证通过结束点位")]
        public string PassBarcodeEndPLC { get; set; }   // 验证通过结束点位 

        /// <summary>
        /// <para>D1005</para>
        /// <para>条码验证失败反馈1；</para>
        /// <para>工装验证失败反馈2；</para>
        /// <para>物料验证失败反馈3。</para>
        /// </summary>
        [SugarColumn(ColumnName = "ErrorBarcodeEndPLC", IsNullable = false)]
        [DisplayName("验证失败结束点位")]
        public string ErrorBarcodeEndPLC { get; set; }  // 验证失败结束点位 

        #endregion

        #region ---------- 条码验证失败提示合集 ----------

        /// <summary>
        /// 条码有效性验证：未读到条码
        /// </summary>
        [SugarColumn(ColumnName = "NoBarcodePrompt", DefaultValue = "未读到条码", IsNullable = true)]
        [DisplayName("条码有效性验证")]
        public string BarcodeIsNullOrWhiteSpace { get; set; }

        /// <summary>
        /// 条码重复性验证：条码重复
        /// </summary>
        [SugarColumn(ColumnName = "RepeatBarcodePrompt", DefaultValue = "条码重复", IsNullable = true)]
        [DisplayName("条码重复性验证")]
        public string BarcodeIsRepeatdly { get; set; }

        /// <summary>
        /// 条码规则有效性验证：无条码验证规则
        /// </summary>
        [SugarColumn(ColumnName = "NoVerificationRulePrompt", DefaultValue = "无条码验证规则", IsNullable = true)]
        [DisplayName("条码规则有效性验证")]
        public string NoVerificationRule { get; set; }

        /// <summary>
        /// 条码规范性验证：条码不规范
        /// </summary>
        [SugarColumn(ColumnName = "BarcodeIsFaultePrompt", DefaultValue = "条码不规范", IsNullable = true)]
        [DisplayName("条码规范性验证")]
        public string BarcodeIsFault { get; set; }

        /// <summary>
        /// 条码规则验证：条码规则不匹配
        /// </summary>
        [SugarColumn(ColumnName = "BarcodeRuleDismachPrompt", DefaultValue = "条码规则不匹配", IsNullable = true)]
        [DisplayName("条码规则验证")]
        public string BarcodeRuleDismachPrompt { get; set; }

        /// <summary>
        /// 条码验证失败
        /// </summary>
        /*[SugarColumn(ColumnName = "ErrorPrompt", DefaultValue = "条码验证失败", IsNullable = true)]
        [DisplayName("验证失败提示")]
        public string ErrorPrompt { get; set; }*/

        /// <summary>
        /// MES验证失败提示：
        /// <para>MES条码验证失败</para>
        /// </summary>
        [SugarColumn(ColumnName = "MesErrorPrompt", DefaultValue = "MES条码验证失败", IsNullable = true)]
        [DisplayName("MES验证失败提示")]
        public string MesErrorPrompt { get; set; }

        /// <summary>
        /// 单机条码验证、MES条码验证通过提示：
        /// <para>条码验证通过</para>
        /// </summary>
        [SugarColumn(ColumnName = "PassPrompt", DefaultValue = "条码验证通过", IsNullable = true)]
        [DisplayName("验证通过提示")]
        public string PassPrompt { get; set; }

        #endregion

        #region ---------- 功能开启状态 ----------

        /// <summary>
        /// 条码验证
        /// </summary>
        [SugarColumn(ColumnName = "BarcodeVerS", IsNullable = true)]
        [DisplayName("条码验证")]
        public bool IsEnableBarcodeVerify { get; set; } // 条码验证 

        /// <summary>
        /// 二维码验证
        /// </summary>
        [SugarColumn(ColumnName = "QRcodeVerS", IsNullable = true)]
        [DisplayName("二维码验证")]
        public bool IsEnableQRcodeVerify { get; set; }  // 二维码验证 

        #endregion

        public ushort GetBarcodeLength()
        {
            ushort length = 10;
            ushort.TryParse(BarcodeLengthPLC, out length);
            return length;
        }

        public static BarcodeVerification GetBarcodeVefictnDefault()
        {
            BarcodeVerification bv = new BarcodeVerification();
            bv.ID = 1;
            bv.LanguageId = 0;
            bv.BarcodeStartPLC = "D1000";
            bv.BarcodePositionPLC = "D1100";
            bv.BarcodeLengthPLC = "10";
            bv.PassBarcodeEndPLC = "D1003";
            bv.ErrorBarcodeEndPLC = "D1005";

            // 1. 条码有效性验证
            bv.BarcodeIsNullOrWhiteSpace = "未读到条码";
            // 2. 条码重复性验证 
            bv.BarcodeIsRepeatdly = "条码重复";
            // 3. 条码规则有效性验证
            bv.NoVerificationRule = "无条码验证规则";
            // 4. 条码规范性验证
            bv.BarcodeIsFault = "条码不规范";
            // 5. 条码规则验证
            bv.BarcodeRuleDismachPrompt = "条码规则不匹配";
            // 6. 条码验证通过
            bv.PassPrompt = "条码验证通过";
            // 7. MES条码验证失败
            bv.MesErrorPrompt = "MES条码验证失败";
            // 条码验证
            bv.IsEnableBarcodeVerify = true;
            // 二维码验证
            bv.IsEnableQRcodeVerify = false;

            return bv;
        }

        public string Save()
        {
            if (string.IsNullOrWhiteSpace(BarcodeIsNullOrWhiteSpace))
            {
                BarcodeIsNullOrWhiteSpace = "未读到条码";
            }
            if (string.IsNullOrWhiteSpace(BarcodeIsRepeatdly))
            {
                BarcodeIsRepeatdly = "条码重复";
            }
            if (string.IsNullOrWhiteSpace(NoVerificationRule))
            {
                NoVerificationRule = "无条码验证规则";
            }
            if (string.IsNullOrWhiteSpace(BarcodeIsFault))
            {
                BarcodeIsFault = "条码不规范";
            }
            if (string.IsNullOrWhiteSpace(BarcodeRuleDismachPrompt))
            {
                BarcodeRuleDismachPrompt = "条码规则不匹配";
            }
            if (string.IsNullOrWhiteSpace(PassPrompt))
            {
                PassPrompt = "条码验证通过";
            }
            /*if (string.IsNullOrWhiteSpace(ErrorPrompt))
            {
                ErrorPrompt = "条码验证失败";
            }*/
            if (string.IsNullOrWhiteSpace(MesErrorPrompt))
            {
                MesErrorPrompt = "MES条码验证失败";
            }
            if (string.IsNullOrWhiteSpace(BarcodeLengthPLC))
            {
                BarcodeLengthPLC = "10";
            }

            return BarcodeVerificationServer.SaveBarcodeVerification(this);
        }

        public string Update()
        {
            return BarcodeVerificationServer.UpdateBarcodeVerification(this);
        }

        public string Delete()
        {
            return BarcodeVerificationServer.DeleteBarcodeVerification(this);
        }
    }
}
