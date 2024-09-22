using MesDatas.DatasServer;
using SqlSugar;
namespace MesDatas.DatasModel
{
    [SugarTable("Codes")]
    public class Codes
    {

        [SugarColumn(ColumnName = "ID", IsNullable = false)]
        public string ID { get; set; }          // 配方编号 

        [SugarColumn(ColumnName = "CName", IsNullable = true)]
        public string CName { get; set; }       // 产品名称

        [SugarColumn(ColumnName = "TooName", IsNullable = true)]
        public string TooName { get; set; }     // 条码验证型号与工装编号 

        [SugarColumn(ColumnName = "MateName", IsNullable = true)]
        public string MateName { get; set; }    // 产品编码 

        [SugarColumn(ColumnName = "MateQRcode", IsNullable = true)]
        public string MateQRcode { get; set; }  // 二维码验证

        public string Save()
        {
            if (ID != null)
            {
                System.Windows.Forms.MessageBox.Show("保存成功", "提示");
                return CodesServer.GetCodesSave(this);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("编号不能为空", "提示");
                return ID = string.Empty;
            }
        }

        public string Update()
        {
            return CodesServer.GetCodesUpdate(this);
        }

        public string Delete()
        {
            return CodesServer.GetCodesDelete(this);
        }
    }
}
