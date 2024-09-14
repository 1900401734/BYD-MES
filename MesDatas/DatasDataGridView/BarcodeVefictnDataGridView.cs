using MesDatas.DatasServer;
using MesDatas.Utility.IniLaguagePath;
namespace MesDatas.DatasDataGridView 
{ 
	public class BarcodeVefictnDataGridView : LanguageUtiye 
	{ 
		public static void GetBarcodeVefictnDataGridViewHeaderText(System.Windows.Forms.DataGridView dataGridView) 
		{
			dataGridView.Columns["ID"].HeaderText = "ID";
			dataGridView.Columns["LanguageId"].HeaderText ="语言ID"; 
			dataGridView.Columns["BarcodeStartPLC"].HeaderText ="开始读取点位"; 
			dataGridView.Columns["BarcodePositinPLC"].HeaderText ="读取条码点位"; 
			dataGridView.Columns["BarcodeLengthPLC"].HeaderText ="读取条码长度点位"; 
			dataGridView.Columns["PassBarcodeEndPLC"].HeaderText ="验证通过结束点位"; 
			dataGridView.Columns["ErrorBarcodeEndPLC"].HeaderText ="验证失败结束点位"; 
			dataGridView.Columns["NotCodePrompt"].HeaderText ="未读到条码提示"; 
			dataGridView.Columns["RepeatcodePrompt"].HeaderText ="重复条码提示";
			dataGridView.Columns["PassPrompt"].HeaderText ="验证通过提示"; 
			dataGridView.Columns["ErrorPrompt"].HeaderText ="验证失败提示"; 
			dataGridView.Columns["MesErrorPrompt"].HeaderText ="MES验证失败提示"; 
			dataGridView.Columns["BarcodeVerS"].HeaderText ="条码验证"; 
			dataGridView.Columns["QRcodeVerS"].HeaderText ="二维码验证"; 
		} 
	} 
} 
