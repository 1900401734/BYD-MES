using MesDatas.DatasServer;
using SqlSugar;
using System;
namespace MesDatas.DatasModel
{
    [SugarTable("RecipeEntity")]
    public class RecipeEntity : IComparable<RecipeEntity>
    {
        /// <summary>
        /// 配方编号
        /// </summary>        
        [SugarColumn(ColumnName = "RecipeID", IsNullable = false, IsPrimaryKey = true)]
        public string RecipeID { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        [SugarColumn(ColumnName = "ProductName", IsNullable = true)]
        public string ProductName { get; set; }

        /// <summary>
        /// 条码规则
        /// </summary>
        [SugarColumn(ColumnName = "BarcodeRule", IsNullable = true)]
        public string BarcodeRule { get; set; }

        /// <summary>
        /// 工装编号
        /// </summary>
        [SugarColumn(ColumnName = "FixtureNumber", IsNullable = true)]
        public string FixtureNumber { get; set; }

        /// <summary>
        /// 产品编码 
        /// </summary>
        [SugarColumn(ColumnName = "ProductCode", IsNullable = true)]
        public string ProductCode { get; set; }

        /// <summary>
        ///  二维码验证
        /// </summary>
        [SugarColumn(ColumnName = "MateQRcode", IsNullable = true)]
        public string MateQRcode { get; set; }

        public string Save()
        {
            if (RecipeID != null)
            {
                System.Windows.Forms.MessageBox.Show("保存成功", "提示");
                return RecipeManage.GetCodesSave(this);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("编号不能为空", "提示");
                return RecipeID = string.Empty;
            }
        }

        public string Update()
        {
            return RecipeManage.GetCodesUpdate(this);
        }

        public string Delete()
        {
            return RecipeManage.GetCodesDelete(this);
        }

        public int CompareTo(RecipeEntity other)
        {
            // 如果另一个对象为null，则当前对象更大
            if (other == null) return 1;

            // 尝试按数字进行比较，以实现 2 在 10 前面的效果
            bool isNum1 = int.TryParse(this.RecipeID, out int id1);
            bool isNum2 = int.TryParse(other.RecipeID, out int id2);

            if (isNum1 && isNum2)
            {
                // 如果两者都是数字，按数字比较
                return id1.CompareTo(id2);
            }

            // 如果至少有一个不是数字，则按字符串进行比较
            return string.Compare(this.RecipeID, other.RecipeID, StringComparison.Ordinal);
        }
    }
}
