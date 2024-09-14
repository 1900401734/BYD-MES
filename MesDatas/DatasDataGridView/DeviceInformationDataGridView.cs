using MesDatas.DatasServer;
using MesDatas.Utility.IniLaguagePath;
namespace MesDatas.DatasDataGridView 
{ 
	public class DeviceInformationDataGridView : LanguageUtiye 
	{ 
		public static void GetDeviceInformationDataGridViewHeaderText(System.Windows.Forms.DataGridView dataGridView) 
		{ 
			dataGridView.Columns["ID"].HeaderText ="ID"; 
			dataGridView.Columns["LanguageId"].HeaderText ="语言ID"; 
			dataGridView.Columns["DeviceStatus"].HeaderText ="设备状态"; 
			dataGridView.Columns["ProductModelNum"].HeaderText ="产品型号"; 
			dataGridView.Columns["ProductModelLength"].HeaderText ="型号长度"; 
			dataGridView.Columns["FormulaNum"].HeaderText ="配方号"; 
			dataGridView.Columns["FormulaModify"].HeaderText ="配方修改"; 
			dataGridView.Columns["FormulaNumModify"].HeaderText ="配方号修改"; 
		} 
	} 
} 
