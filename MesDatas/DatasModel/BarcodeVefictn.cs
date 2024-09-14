using MesDatas.DatasServer;
using SqlSugar;
using System.ComponentModel;
namespace MesDatas.DatasModel
{
    [SugarTable("BarcodeVefictn")]
    public class BarcodeVefictn
    {
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        [DisplayName("ID")]
        public int ID { get; set; }                     // ID 

        [SugarColumn(ColumnName = "LanguageId", DefaultValue = "0")]
        [DisplayName("语言ID")]
        public int LanguageId { get; set; }             // 语言ID 

        [SugarColumn(ColumnName = "BarcodeStartPLC", IsNullable = true)]
        [DisplayName("开始读取点位")]
        public string BarcodeStartPLC { get; set; }     // 开始读取点位 

        [SugarColumn(ColumnName = "BarcodePositinPLC", IsNullable = false)]
        [DisplayName("读取条码点位")]
        public string BarcodePositionPLC { get; set; }   // 读取条码点位 

        [SugarColumn(ColumnName = "BarcodeLengthPLC", IsNullable = false)]
        [DisplayName("读取条码长度点位")]
        public string BarcodeLengthPLC { get; set; }    // 读取条码长度点位 

        [SugarColumn(ColumnName = "PassBarcodeEndPLC", IsNullable = false)]
        [DisplayName("验证通过结束点位")]
        public string PassBarcodeEndPLC { get; set; }   // 验证通过结束点位 

        [SugarColumn(ColumnName = "ErrorBarcodeEndPLC", IsNullable = false)]
        [DisplayName("验证失败结束点位")]
        public string ErrorBarcodeEndPLC { get; set; }  // 验证失败结束点位 

        [SugarColumn(ColumnName = "NotCodePrompt", DefaultValue = "未读到条码", IsNullable = true)]
        [DisplayName("未读到条码提示")]
        public string NoBarcodePrompt { get; set; }       // 未读到条码提示 

        [SugarColumn(ColumnName = "RepeatcodePrompt", DefaultValue = "重复条码", IsNullable = true)]
        [DisplayName("重复条码提示")]
        public string RepeatcodePrompt { get; set; }    // 重复条码提示

        [SugarColumn(ColumnName = "PassPrompt", DefaultValue = "条码验证通过", IsNullable = true)]
        [DisplayName("验证通过提示")]
        public string PassPrompt { get; set; }          // 验证通过提示 

        [SugarColumn(ColumnName = "ErrorPrompt", DefaultValue = "条码验证失败", IsNullable = true)]
        [DisplayName("验证失败提示")]
        public string ErrorPrompt { get; set; }         // 验证失败提示 

        [SugarColumn(ColumnName = "MesErrorPrompt", DefaultValue = "MES条码验证失败", IsNullable = true)]
        [DisplayName("MES验证失败提示")]
        public string MesErrorPrompt { get; set; }      // MES验证失败提示 

        [SugarColumn(ColumnName = "BarcodeVerS", IsNullable = true)]
        [DisplayName("条码验证")]
        public bool BarcodeVerS { get; set; }           // 条码验证 

        [SugarColumn(ColumnName = "QRcodeVerS", IsNullable = true)]
        [DisplayName("二维码验证")]
        public bool QRcodeVerS { get; set; }            // 二维码验证 

        public ushort GetBarcodeLength()
        {
            ushort length = 10;
            ushort.TryParse(BarcodeLengthPLC, out length);
            return length;
        }

        public static BarcodeVefictn GetBarcodeVefictnDefault()
        {
            BarcodeVefictn barcodeVefictn = new BarcodeVefictn();
            barcodeVefictn.ID = 1;
            barcodeVefictn.LanguageId = 0;
            barcodeVefictn.BarcodeStartPLC = "D1000";
            barcodeVefictn.BarcodePositionPLC = "D1100";
            barcodeVefictn.BarcodeLengthPLC = "10";
            barcodeVefictn.PassBarcodeEndPLC = "D1003";
            barcodeVefictn.ErrorBarcodeEndPLC = "D1005";
            barcodeVefictn.NoBarcodePrompt = "未读到条码";
            barcodeVefictn.RepeatcodePrompt = "重复条码";
            barcodeVefictn.PassPrompt = "条码验证通过";
            barcodeVefictn.ErrorPrompt = "条码验证失败";
            barcodeVefictn.MesErrorPrompt = "MES条码验证失败";
            barcodeVefictn.BarcodeVerS = true;  // 条码验证
            barcodeVefictn.QRcodeVerS = false;  // 二维码验证
            return barcodeVefictn;
        }

        public string Save()
        {
            if (string.IsNullOrWhiteSpace(NoBarcodePrompt))
            {
                NoBarcodePrompt = "未读到条码";
            }
            if (string.IsNullOrWhiteSpace(RepeatcodePrompt))
            {
                RepeatcodePrompt = "重复条码";
            }
            if (string.IsNullOrWhiteSpace(PassPrompt))
            {
                PassPrompt = "条码验证通过";
            }
            if (string.IsNullOrWhiteSpace(ErrorPrompt))
            {
                ErrorPrompt = "条码验证失败";
            }
            if (string.IsNullOrWhiteSpace(MesErrorPrompt))
            {
                MesErrorPrompt = "MES条码验证失败";
            }
            if (string.IsNullOrWhiteSpace(BarcodeLengthPLC))
            {
                BarcodeLengthPLC = "10";
            }

            return BarcodeVefictnServer.GetBarcodeVefictnSave(this);
        }

        public string Update()
        {
            return BarcodeVefictnServer.GetBarcodeVefictnUpdate(this);
        }

        public string Delete()
        {
            return BarcodeVefictnServer.GetBarcodeVefictnDelete(this);
        }
    }
}
