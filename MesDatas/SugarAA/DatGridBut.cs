using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesDatas.Utiye
{
    public class DatGridBut
    {
        public static void DataGdvewBut(System.Windows.Forms.DataGridView dgv)
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "操作";
            buttonColumn.Text = "保存";
            buttonColumn.Name = "BtnSave";
            buttonColumn.DefaultCellStyle.NullValue = "保存";
            //dataGridView2.Columns.Insert(0, buttonColumn);// dataGridView2.I
            dgv.Columns.Add(buttonColumn);
            //table1.Columns.Add("操作");
            //buttonColumn.C += new DataGridViewCellEventHandler(buttonColumn_CellClick)
            //  Models();//查询名称
            //  button16_Click(null, null);//初始品名表、刷新
            // mdb.CloseConnection();
            // 再次创建一个新的列对象并设置其属删除
            DataGridViewButtonColumn anotherButtonColumn = new DataGridViewButtonColumn();
            anotherButtonColumn.HeaderText = "操作"; // 第二个按钮的标题文本
            anotherButtonColumn.Name = "BtnDel"; // 第二个按钮的名称
            anotherButtonColumn.DefaultCellStyle.NullValue = "删除";
            // buttonColumn.DefaultCellStyle.NullValue = "删除";
            // 将该列对象也添加到DataGridView控件中
            dgv.Columns.Add(anotherButtonColumn);
        }
    }
}
