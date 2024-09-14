using MesDatas.DatasServer;
using MesDatas.Utility.IniLaguagePath;
namespace MesDatas.DatasDataGridView 
{ 
	public class PrintersDataGridView : LanguageUtiye 
	{ 
		public static void GetPrintersDataGridViewHeaderText(System.Windows.Forms.DataGridView dataGridView) 
		{ 
			dataGridView.Columns["ID"].HeaderText =inipathLanguage.IniLanguageValue("ID", LanguageId); 
			dataGridView.Columns["LanguageId"].HeaderText =inipathLanguage.IniLanguageValue("语言ID", LanguageId); 
			dataGridView.Columns["Ptype"].HeaderText =inipathLanguage.IniLanguageValue("打印机类型", LanguageId); 
			dataGridView.Columns["Phead"].HeaderText =inipathLanguage.IniLanguageValue("打印机头", LanguageId); 
			dataGridView.Columns["Ptime"].HeaderText =inipathLanguage.IniLanguageValue("条码时间格式", LanguageId); 
			dataGridView.Columns["Ptail"].HeaderText =inipathLanguage.IniLanguageValue("条码尾", LanguageId); 
			dataGridView.Columns["TFront"].HeaderText =inipathLanguage.IniLanguageValue("条码前", LanguageId); 
			dataGridView.Columns["Tmonarch"].HeaderText =inipathLanguage.IniLanguageValue(" 条码后", LanguageId); 
			dataGridView.Columns["Tlow"].HeaderText =inipathLanguage.IniLanguageValue("条码下", LanguageId); 
			dataGridView.Columns["Pertow"].HeaderText =inipathLanguage.IniLanguageValue("是否打印二个条码", LanguageId); 
			dataGridView.Columns["Txttow"].HeaderText =inipathLanguage.IniLanguageValue("是否打印文字", LanguageId); 
			dataGridView.Columns["Plcmodel"].HeaderText =inipathLanguage.IniLanguageValue("是否读取PLC型号", LanguageId); 
			dataGridView.Columns["Method"].HeaderText =inipathLanguage.IniLanguageValue("prn文件位置", LanguageId); 
			dataGridView.Columns["ip"].HeaderText =inipathLanguage.IniLanguageValue("打印机IP", LanguageId); 
			dataGridView.Columns["port"].HeaderText =inipathLanguage.IniLanguageValue("打印机端口", LanguageId); 
			dataGridView.Columns["printCode"].HeaderText =inipathLanguage.IniLanguageValue("打印机打印条码", LanguageId); 
		} 
	} 
} 
