using MesDatas.DatasServer;
using MesDatas.Utility.IniLaguagePath;
namespace MesDatas.DatasDataGridView 
{ 
	public class SytemInfoEntityDataGridView : LanguageUtiye 
	{ 
		public static void GetSytemInfoEntityDataGridViewHeaderText(System.Windows.Forms.DataGridView dataGridView) 
		{ 
			dataGridView.Columns["ID"].HeaderText =inipathLanguage.IniLanguageValue("ID", LanguageId); 
			dataGridView.Columns["LanguageId"].HeaderText =inipathLanguage.IniLanguageValue("语言ID", LanguageId); 
			dataGridView.Columns["IP"].HeaderText =inipathLanguage.IniLanguageValue("IP", LanguageId); 
			dataGridView.Columns["Port"].HeaderText =inipathLanguage.IniLanguageValue("Port", LanguageId); 
			dataGridView.Columns["Timeout"].HeaderText =inipathLanguage.IniLanguageValue("时间", LanguageId); 
			dataGridView.Columns["NcCode"].HeaderText =inipathLanguage.IniLanguageValue("NcCode", LanguageId); 
			dataGridView.Columns["Opration"].HeaderText =inipathLanguage.IniLanguageValue("Opration", LanguageId); 
			dataGridView.Columns["Password"].HeaderText =inipathLanguage.IniLanguageValue("Password", LanguageId); 
			dataGridView.Columns["Resource"].HeaderText =inipathLanguage.IniLanguageValue("Resource", LanguageId); 
			dataGridView.Columns["Site"].HeaderText =inipathLanguage.IniLanguageValue("Site", LanguageId); 
			dataGridView.Columns["Url"].HeaderText =inipathLanguage.IniLanguageValue("Url", LanguageId); 
			dataGridView.Columns["User"].HeaderText =inipathLanguage.IniLanguageValue("User", LanguageId); 
		} 
	} 
} 
