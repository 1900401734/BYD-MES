using MesDatas.DatasServer;
using MesDatas.Properties;
using MesDatas.Utility.IniLaguagePath;
using System.Reflection;
using System.Resources;
namespace MesDatas.DatasDataGridView
{
    public class RecipeDataGridView : LanguageUtiye
    {
        Assembly asm = Assembly.GetExecutingAssembly();
        ResourceManager resources = null;

        public RecipeDataGridView()
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

        public void GetCodesDataGridViewHeaderText(System.Windows.Forms.DataGridView dataGridView)
        {
            dataGridView.Columns["RecipeID"].HeaderText = resources.GetString("d5ID");              // 配方编号
            dataGridView.Columns["ProductName"].HeaderText = resources.GetString("d5PName");        // 产品名称 
            dataGridView.Columns["BarcodeRule"].HeaderText = resources.GetString("d5BarcodeRule");  // 条码规则
            dataGridView.Columns["FixtureNumber"].HeaderText = resources.GetString("d5FixtureID");  // 条码规则
            dataGridView.Columns["ProductCode"].HeaderText = resources.GetString("d5PCode");        // 物料编码 
            dataGridView.Columns["MateQRcode"].HeaderText = resources.GetString("d5QRCode");        // 二维码验证
        }
    }
}
