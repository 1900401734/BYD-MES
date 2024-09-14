using HslCommunication.LogNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.Utility.ResourcesLaguage
{
    public class LanguageResour
    {
        static Assembly asm = Assembly.GetExecutingAssembly();
        static ResourceManager resources = null;
        public static ResourceManager GetResourceManagerAS()
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
            return resources;
        }
        public static ResourceManager resourceManager
        {
            get { return resources ??  (resources = GetResourceManagerAS()); }
        }
        public static string SystemDefaultDataNotDel = resourceManager.GetString("SystemDefaultDataNotDel");
        public static string PassBtnSave = resourceManager.GetString("PassBtnSave");
        public static string ErrorBtnSave = resourceManager.GetString("ErrorBtnSave");
        public static string PassBtnAdd = resourceManager.GetString("PassBtnAdd");
        public static string ErrorBtnAdd = resourceManager.GetString("ErrorBtnAdd");
        public static string PassBtnUpdate = resourceManager.GetString("PassBtnUpdate");
        public static string ErrorBtnUpdate = resourceManager.GetString("ErrorBtnUpdate");
        public static string PassBtnDelete = resourceManager.GetString("PassBtnDelete");
        public static string ErrorBtnDelete = resourceManager.GetString("ErrorBtnDelete");
    }
}
