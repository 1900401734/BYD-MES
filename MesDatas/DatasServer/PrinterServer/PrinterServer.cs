using MesDatas.DatasModel;
using MesDatas.Utility.IniLaguagePath;
using MesDatas.Utility.ResourcesLaguage;
using MesDatas.Utility.SugarDB;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace MesDatas.DatasServer
{
    public class PrinterServer
    {
        // 初始化
        public static void InitPrinterSetting()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    db.CodeFirst.InitTables<Printer>();

                    Printer printerSetting = Printer.PrinterSettingInitalize();

                    if (!db.Queryable<Printer>().Where(it => it.ID == printerSetting.ID).Any())
                    {
                        db.Insertable(printerSetting).ExecuteCommand();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        // 保存
        public static string GetPrinterSettingSave(Printer printersetting)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    if (db.Queryable<Printer>().Where(it => it.ID == printersetting.ID).Any())
                    {
                        return db.Updateable(printersetting).ExecuteCommand() > 0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave;
                    }
                    else
                    {
                        return db.Insertable(printersetting).ExecuteCommand() > 0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave;
                    }
                }
            }
            catch (Exception ex)
            {

                return LanguageResour.ErrorBtnSave;
            }
        }

        // 修改
        public static string GetPrinterSettingUpdate(Printer printersetting)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Updateable(printersetting).ExecuteCommand() > 0 ? LanguageResour.PassBtnUpdate : LanguageResour.ErrorBtnUpdate;
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnUpdate;
            }
        }

        // 删除
        public static string GetPrinterSettingDelete(Printer printersetting)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Deleteable(printersetting).ExecuteCommand() > 0 ? LanguageResour.PassBtnDelete : LanguageResour.ErrorBtnDelete;
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnDelete;
            }
        }

        // 获取
        public static Printer GetPrinterSetting(int id)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Queryable<Printer>().Where(it => it.ID == id).First();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        // 获取PrinterSetting列表 
        public static List<Printer> GetPrinterSettingList()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Queryable<Printer>().ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        // 获取PrinterSetting列表 
        public static List<Printer> GetPrinterSettingList(int LanguageId)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Queryable<Printer>().Where(it => it.LanguageId == LanguageId).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        // 获取BindingListPrinterSetting列表 
        public static BindingList<Printer> GetPrinterSettingBindingList()
        {
            return new BindingList<Printer>(GetPrinterSettingList());
        }

        // 获取{tableName} 
        public static Printer GetLangPrinterSetting(int LanguageId)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Queryable<Printer>().Where(it => it.LanguageId == LanguageId).First();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
