using MesDatas.DatasServer;
using MesDatas.Utility.IniLaguagePath;
namespace MesDatas.DatasDataGridView 
{ 
	public class PrinterSettingDataGridView : LanguageUtiye 
	{ 
		public static void GetPrinterSettingDataGridViewHeaderText(System.Windows.Forms.DataGridView dataGridView) 
		{ 
			dataGridView.Columns["ID"].HeaderText ="ID"; 
			dataGridView.Columns["LanguageId"].HeaderText ="语言ID"; 
			dataGridView.Columns["PrinterStep"].HeaderText ="打印机步骤"; 
			dataGridView.Columns["PrinteSendResult"].HeaderText ="打印机发送结果"; 
		} 
	} 
} 
