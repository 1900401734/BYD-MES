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
    public class SytemSetDerivedServer
    {
        //初始化 SytemSetDerived 
        public static void InitSytemSetDerived()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    db.CodeFirst.InitTables<SytemSetDerived>();
                    SytemSetDerived sytemsetderived = SytemSetDerived.SytemSetDerivedDMESS();
                    if (!db.Queryable<SytemSetDerived>().Where(it => it.ID == sytemsetderived.ID).Any())
                    {
                        db.Insertable(sytemsetderived).ExecuteCommand();
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        //保存SytemSetDerived 
        public static string GetSytemSetDerivedSave(SytemSetDerived sytemsetderived)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    if (db.Queryable<SytemSetDerived>().Where(it => it.ID == sytemsetderived.ID).Any())
                    {
                        return db.Updateable(sytemsetderived).ExecuteCommand() > 0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave;
                    }
                    else
                    {
                        return db.Insertable(sytemsetderived).ExecuteCommand() > 0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave;
                    }
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnSave;
            }
        }

        //修改SytemSetDerived 
        public static string GetSytemSetDerivedUpdate(SytemSetDerived sytemsetderived)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Updateable(sytemsetderived).ExecuteCommand() > 0 ? LanguageResour.PassBtnUpdate : LanguageResour.ErrorBtnUpdate;
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnUpdate;
            }
        }

        //删除SytemSetDerived 
        public static string GetSytemSetDerivedDelete(SytemSetDerived sytemsetderived)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Deleteable(sytemsetderived).ExecuteCommand() > 0 ? LanguageResour.ErrorBtnDelete : LanguageResour.PassBtnDelete;
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnDelete;
            }
        }

        // 获取SytemSetDerived 
        public static SytemSetDerived GetSytemSetDerived(int id)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Queryable<SytemSetDerived>().Where(it => it.ID == id).First();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // 获取SytemSetDerived列表 
        public static List<SytemSetDerived> GetSytemSetDerivedList()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Queryable<SytemSetDerived>().ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //获取SytemSetDerived列表 
        public static List<SytemSetDerived> GetSytemSetDerivedList(int LanguageId)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Queryable<SytemSetDerived>().Where(it => it.LanguageId == LanguageId).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //获取BindingListSytemSetDerived列表 
        public static BindingList<SytemSetDerived> GetSytemSetDerivedBindingList()
        {
            return new BindingList<SytemSetDerived>(GetSytemSetDerivedList());
        }

        // 获取{tableName} 
        public static SytemSetDerived GetLangSytemSetDerived(int LanguageId)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Queryable<SytemSetDerived>().Where(it => it.LanguageId == LanguageId).First();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
