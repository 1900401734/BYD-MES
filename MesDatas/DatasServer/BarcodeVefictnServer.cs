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
    public class BarcodeVefictnServer
    {
        //初始化 BarcodeVefictn 
        public static void InitBarcodeVefictn()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    db.CodeFirst.InitTables<BarcodeVefictn>();
                    BarcodeVefictn barcodeVefictn = BarcodeVefictn.GetBarcodeVefictnDefault();
                    if (!db.Queryable<BarcodeVefictn>().Where(it => it.ID == barcodeVefictn.ID).Any())
                    {
                        db.Insertable(barcodeVefictn).ExecuteCommand();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        //保存BarcodeVefictn 
        public static string GetBarcodeVefictnSave(BarcodeVefictn barcodevefictn)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    if (db.Queryable<BarcodeVefictn>().Where(it => it.ID == barcodevefictn.ID).Any())
                    {
                        return db.Updateable(barcodevefictn).ExecuteCommand() > 0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave;
                    }
                    else
                    {
                        return db.Insertable(barcodevefictn).ExecuteCommand() > 0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave;
                    }
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnSave;
            }
        }

        //修改BarcodeVefictn 
        public static string GetBarcodeVefictnUpdate(BarcodeVefictn barcodevefictn)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Updateable(barcodevefictn).ExecuteCommand() > 0 ? LanguageResour.PassBtnUpdate : LanguageResour.ErrorBtnUpdate;
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnUpdate;
            }
        }

        //删除BarcodeVefictn 
        public static string GetBarcodeVefictnDelete(BarcodeVefictn barcodevefictn)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    if (barcodevefictn.ID == 1)
                    {
                        return LanguageResour.SystemDefaultDataNotDel;
                    }
                    return db.Deleteable(barcodevefictn).ExecuteCommand() > 0 ? LanguageResour.PassBtnDelete : LanguageResour.ErrorBtnDelete;
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnDelete;
            }
        }

        // 获取BarcodeVefictn 
        public static BarcodeVefictn GetBarcodeVefictn(int id)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Queryable<BarcodeVefictn>().Where(it => it.ID == id).First();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // 获取BarcodeVefictn列表 
        public static List<BarcodeVefictn> GetBarcodeVefictnList()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Queryable<BarcodeVefictn>().ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //获取BarcodeVefictn列表 
        public static List<BarcodeVefictn> GetBarcodeVefictnList(int LanguageId)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    if (db.Queryable<BarcodeVefictn>().Where(it => it.LanguageId == LanguageId).Any())
                    {

                        return db.Queryable<BarcodeVefictn>().Where(it => it.LanguageId == LanguageId).ToList();
                    }
                    return db.Queryable<BarcodeVefictn>().Where(it => it.LanguageId == 0).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //获取BindingListBarcodeVefictn列表 
        public static BindingList<BarcodeVefictn> GetBarcodeVefictnBindingList()
        {
            return new BindingList<BarcodeVefictn>(GetBarcodeVefictnList());
        }

        // 获取{tableName} 
        public static BarcodeVefictn GetLangBarcodeVefictn(int LanguageId)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Queryable<BarcodeVefictn>().Where(it => it.LanguageId == LanguageId).First();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
