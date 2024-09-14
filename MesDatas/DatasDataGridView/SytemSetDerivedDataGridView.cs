using MesDatas.DatasServer;
using MesDatas.Utility.IniLaguagePath;
namespace MesDatas.DatasDataGridView 
{ 
	public class SytemSetDerivedDataGridView : LanguageUtiye 
	{ 
		public static void GetSytemSetDerivedDataGridViewHeaderText(System.Windows.Forms.DataGridView dataGridView) 
		{ 
			dataGridView.Columns["ID"].HeaderText ="ID"; 
			dataGridView.Columns["LanguageId"].HeaderText ="语言ID"; 
			dataGridView.Columns["SytemSetnullCoden"].HeaderText ="显示"; 

		} 
	} 
} 
