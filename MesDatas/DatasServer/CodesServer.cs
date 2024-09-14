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
    public class CodesServer
    {
        // 初始化 Codes 
        public static void InitCodes()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    db.CodeFirst.InitTables<Codes>();
                }
            }
            catch (Exception ex)
            {
            }
        }

        //保存Codes 
        public static string GetCodesSave(Codes codes)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    if (db.Queryable<Codes>().Where(it => it.ID == codes.ID).Any())
                    {
                        return db.Updateable(codes).Where(it => it.ID == codes.ID).ExecuteCommand() > 0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave;
                    }
                    else
                    {
                        return db.Insertable(codes).ExecuteCommand() > 0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave;
                    }
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnSave;
            }
        }

        //修改Codes 
        public static string GetCodesUpdate(Codes codes)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Updateable(codes).ExecuteCommand() > 0 ? LanguageResour.PassBtnUpdate : LanguageResour.ErrorBtnUpdate;
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnUpdate;
            }
        }

        //删除Codes 
        public static string GetCodesDelete(Codes codes)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Deleteable(codes).Where(it => it.ID == codes.ID).ExecuteCommand() > 0 ? LanguageResour.PassBtnDelete : LanguageResour.ErrorBtnDelete;
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnDelete;
            }
        }

        // 获取Codes 
        public static Codes GetCodes(string id)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Queryable<Codes>().Where(it => it.ID == id).First();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // 获取Codes列表 
        public static List<Codes> GetCodesList()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Queryable<Codes>().ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //获取Codes列表 
        ////public static List<Codes> GetCodesList(int LanguageId) 
        ////{ 
        ////	try 
        ////	{ 
        ////		using (var db = DBConnSugClie.GetDBConn()) 
        ////		{ 
        ////			return db.Queryable<Codes>().Where(it=>it.LanguageId == LanguageId).ToList(); 
        ////		} 
        ////		} 
        ////		catch (Exception ex) 
        ////		{ 
        ////			LogHelper.WriteLogWarn("[Codes] ", ex); 
        ////			return null; 
        ////		} 
        ////	} 
        //获取BindingListCodes列表 
        public static BindingList<Codes> GetCodesBindingList()
        {
            return new BindingList<Codes>(GetCodesList());
        }

        //// 获取{tableName} 
        //public static Codes GetLangCodes(int LanguageId) 
        //{ 
        //	try 
        //	{ 
        //		using (var db = DBConnSugClie.GetDBConn()) 
        //		{ 
        //			return db.Queryable<Codes>().Where(it=>it.LanguageId == LanguageId).First(); 
        //		} 
        //		} 
        //		catch (Exception ex) 
        //		{ 
        //			LogHelper.WriteLogWarn("[Codes] ", ex); 
        //			return null; 
        //		} 
        //	} 
    }
}
