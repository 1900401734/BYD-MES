using MesDatas.DatasServer;
using MesDatas.Properties;
using MesDatas.Utility.IniLaguagePath;
using System.Reflection;
using System.Resources;
namespace MesDatas.DatasDataGridView 
{ 
	public class CodesDataGridView : LanguageUtiye 
	{
        Assembly asm = Assembly.GetExecutingAssembly();
        ResourceManager resources = null;

        public CodesDataGridView()
        {
            string language = Properties.Settings.Default.DefaultLanguage;
            if (language == "zh-CN")
            {
                resources = new ResourceManager("MesDatas.Language_Resources.language_Chinese", asm);
            }
            else if (language == "en-US")
            {
                resources = new ResourceManager("MesDatas.Language_Resources.language_English", asm);
            }
            else if (language == "th-TH")
            {
                resources = new ResourceManager("MesDatas.Language_Resources.language_Thai", asm);
            }
        }
         public  void GetCodesDataGridViewHeaderText(System.Windows.Forms.DataGridView dataGridView)
        {

			dataGridView.Columns["ID"].HeaderText = resources.GetString("ID");              // 配方编号
            dataGridView.Columns["CName"].HeaderText = resources.GetString("CName");        // 产品名称 
            dataGridView.Columns["TooName"].HeaderText = resources.GetString("TooName");    // 条码验证型号与工装编号
            dataGridView.Columns["MateName"].HeaderText = resources.GetString("MateName");  // 产品编码 
            dataGridView.Columns["MateQRcode"].HeaderText = "二维条码验证";                  //resources.GetString("CName");// "产品名称";
		} 
	} 
} 
