using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBullData
{
    public class LanguageUtiye
    {
        public static int LanguageId = 0;
        public static string PassBtnSave = inipathLanguage.IniLanguageValue("保存成功",LanguageId);
        public static string ErrorBtnSave = inipathLanguage.IniLanguageValue("保存失败",LanguageId);
        public static string PassBtnAdd = inipathLanguage.IniLanguageValue("添加成功",LanguageId);
        public static string ErrorBtnAdd = inipathLanguage.IniLanguageValue("添加失败",LanguageId);
        public static string PassBtnUpdate = inipathLanguage.IniLanguageValue("修改成功",LanguageId);
        public static string ErrorBtnUpdate = inipathLanguage.IniLanguageValue("修改失败",LanguageId);
        public static string PassBtnDelete = inipathLanguage.IniLanguageValue("删除成功",LanguageId);
        public static string ErrorBtnDelete = inipathLanguage.IniLanguageValue("删除失败",LanguageId);
    }
}
