using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesDatas.Utility.DGVButAdd
{
    public class DataGridViewButton
    {
        public static void AddDGVButton(System.Windows.Forms.DataGridView dgv)
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "操作";
            buttonColumn.Text = "保存";
            buttonColumn.Name = "BtnSave";
            buttonColumn.DefaultCellStyle.NullValue = "保存";
            dgv.Columns.Add(buttonColumn);

            // 再次创建一个新的列对象并设置其属删除
            DataGridViewButtonColumn anotherButtonColumn = new DataGridViewButtonColumn();
            anotherButtonColumn.HeaderText = "操作"; // 第二个按钮的标题文本
            anotherButtonColumn.Name = "BtnDel"; // 第二个按钮的名称
            anotherButtonColumn.DefaultCellStyle.NullValue = "删除";
            dgv.Columns.Add(anotherButtonColumn);
        }
    }
}
