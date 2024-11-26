using MesDatas.DatasModel;
using MesDatas.Utility.IniLaguagePath;
using MesDatas.Utility.ResourcesLaguage;
using MesDatas.Utility.SugarDB;
using Org.BouncyCastle.Ocsp;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace MesDatas.DatasServer
{
    public class BarcodeVefictnServer
    {
        // 初始化 BarcodeVefictn 
        public static void InitBarcodeVefictn()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())    // 获取数据库连接
                {
                    // 初始化表结构。如果表不存在，将根据实体类 BarcodeVerification 创建
                    db.CodeFirst.InitTables<BarcodeVerification>();

                    // 获取一个默认的 BarcodeVerification 实例
                    BarcodeVerification bv = BarcodeVerification.GetBarcodeVefictnDefault();

                    // 查询数据库中是否已经存在与 barcodeVefictn.ID 相同的记录
                    if (!db.Queryable<BarcodeVerification>().Where(it => it.ID == bv.ID).Any())
                    {
                        // 如果不存在，则插入该条记录。
                        db.Insertable(bv).ExecuteCommand();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // 保存BarcodeVefictn 
        public static string GetBarcodeVefictnSave(BarcodeVerification barcodevefictn)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    if (db.Queryable<BarcodeVerification>().Where(it => it.ID == barcodevefictn.ID).Any())
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

        // 修改BarcodeVefictn 
        public static string GetBarcodeVefictnUpdate(BarcodeVerification barcodevefictn)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Updateable(barcodevefictn).ExecuteCommand() > 0 ? LanguageResour.PassBtnUpdate : LanguageResour.ErrorBtnUpdate;
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnUpdate;
            }
        }

        // 删除BarcodeVefictn 
        public static string GetBarcodeVefictnDelete(BarcodeVerification barcodevefictn)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
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
        public static BarcodeVerification GetBarcodeVefictn(int id)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Queryable<BarcodeVerification>().Where(it => it.ID == id).First();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // 获取BarcodeVefictn列表 
        public static List<BarcodeVerification> GetBarcodeVefictnList(int LanguageId)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    if (db.Queryable<BarcodeVerification>().Where(it => it.LanguageId == LanguageId).Any())
                    {

                        return db.Queryable<BarcodeVerification>().Where(it => it.LanguageId == LanguageId).ToList();
                    }
                    return db.Queryable<BarcodeVerification>().Where(it => it.LanguageId == 0).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //获取BindingListBarcodeVefictn列表 
        public static BindingList<BarcodeVerification> GetBarcodeVefictnBindingList()
        {
            return new BindingList<BarcodeVerification>(GetBarcodeVefictnList());
        }

        // 获取BarcodeVefictn列表 
        public static List<BarcodeVerification> GetBarcodeVefictnList()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Queryable<BarcodeVerification>().ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // 获取{tableName} 
        public static BarcodeVerification GetLangBarcodeVefictn(int LanguageId)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Queryable<BarcodeVerification>().Where(it => it.LanguageId == LanguageId).First();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
