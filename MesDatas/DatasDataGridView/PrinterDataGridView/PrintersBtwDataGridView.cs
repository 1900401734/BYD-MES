using MesDatas.DatasServer;
using MesDatas.Utility.IniLaguagePath;
namespace MesDatas.DatasDataGridView 
{ 
	public class PrintersBtwDataGridView : LanguageUtiye 
	{ 
		public static void GetPrintersBtwDataGridViewHeaderText(System.Windows.Forms.DataGridView dataGridView) 
		{ 
			dataGridView.Columns["ID"].HeaderText =inipathLanguage.IniLanguageValue("ID", LanguageId); 
			dataGridView.Columns["LanguageId"].HeaderText =inipathLanguage.IniLanguageValue("语言ID", LanguageId); 
			dataGridView.Columns["PtypeBtw"].HeaderText =inipathLanguage.IniLanguageValue("打印机类型", LanguageId); 
			dataGridView.Columns["PheadBtw"].HeaderText =inipathLanguage.IniLanguageValue("打印机头", LanguageId); 
			dataGridView.Columns["PtimeBtw"].HeaderText =inipathLanguage.IniLanguageValue("条码时间格式", LanguageId); 
			dataGridView.Columns["PtailBtw"].HeaderText =inipathLanguage.IniLanguageValue("条码尾", LanguageId); 
			dataGridView.Columns["TFrontBtw"].HeaderText =inipathLanguage.IniLanguageValue("条码前", LanguageId); 
			dataGridView.Columns["TmonarchBtw"].HeaderText =inipathLanguage.IniLanguageValue(" 条码后", LanguageId); 
			dataGridView.Columns["TlowBtw"].HeaderText =inipathLanguage.IniLanguageValue("条码下", LanguageId); 
			dataGridView.Columns["PertowBtw"].HeaderText =inipathLanguage.IniLanguageValue("是否打印二个条码", LanguageId); 
			dataGridView.Columns["TxttowBtw"].HeaderText =inipathLanguage.IniLanguageValue("是否打印文字", LanguageId); 
			dataGridView.Columns["PlcmodelBtw"].HeaderText =inipathLanguage.IniLanguageValue("是否读取PLC型号", LanguageId); 
			dataGridView.Columns["MethodBtw"].HeaderText =inipathLanguage.IniLanguageValue("prn文件位置", LanguageId); 
			dataGridView.Columns["ipBtw"].HeaderText =inipathLanguage.IniLanguageValue("打印机IP", LanguageId); 
			dataGridView.Columns["portBtw"].HeaderText =inipathLanguage.IniLanguageValue("打印机端口", LanguageId); 
			dataGridView.Columns["printCodeBtw"].HeaderText =inipathLanguage.IniLanguageValue("打印机打印条码", LanguageId); 
		} 
	} 
} 
