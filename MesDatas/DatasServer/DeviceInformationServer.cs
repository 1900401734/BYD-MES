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
    public class DeviceInformationServer
    {
        //初始化 DeviceInformation 
        public static void InitDeviceInformation()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    db.CodeFirst.InitTables<DeviceInformation>();

                    DeviceInformation deviceInformation = DeviceInformation.DeviceInformationInitalize();
                    if (!db.Queryable<DeviceInformation>().Where(it => it.ID == deviceInformation.ID).Any())
                    {
                        db.Insertable(deviceInformation).ExecuteCommand();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //保存DeviceInformation 
        public static string GetDeviceInformationSave(DeviceInformation deviceinformation)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    if (db.Queryable<DeviceInformation>().Where(it => it.ID == deviceinformation.ID).Any())
                    {
                        return db.Updateable(deviceinformation).ExecuteCommand() > 0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave;
                    }
                    else
                    {
                        return db.Insertable(deviceinformation).ExecuteCommand() > 0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave;
                    }
                }
            }
            catch (Exception ex)
            {

                return LanguageResour.ErrorBtnSave;
            }
        }

        //修改DeviceInformation 
        public static string GetDeviceInformationUpdate(DeviceInformation deviceinformation)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Updateable(deviceinformation).ExecuteCommand() > 0 ? LanguageResour.PassBtnUpdate : LanguageResour.ErrorBtnUpdate;
                }
            }
            catch (Exception ex)
            {

                return LanguageResour.ErrorBtnUpdate;
            }
        }

        //删除DeviceInformation 
        public static string GetDeviceInformationDelete(DeviceInformation deviceinformation)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Deleteable(deviceinformation).ExecuteCommand() > 0 ? LanguageResour.PassBtnDelete : LanguageResour.ErrorBtnDelete;
                }
            }
            catch (Exception ex)
            {

                return LanguageResour.ErrorBtnDelete;
            }
        }

        // 获取DeviceInformation 
        public static DeviceInformation GetDeviceInformation(int id)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Queryable<DeviceInformation>().Where(it => it.ID == id).First();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        // 获取DeviceInformation列表 
        public static List<DeviceInformation> GetDeviceInformationList()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Queryable<DeviceInformation>().ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        //获取DeviceInformation列表 
        public static List<DeviceInformation> GetDeviceInformationList(int LanguageId)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Queryable<DeviceInformation>().Where(it => it.LanguageId == LanguageId).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        //获取BindingListDeviceInformation列表 
        public static BindingList<DeviceInformation> GetDeviceInformationBindingList()
        {
            return new BindingList<DeviceInformation>(GetDeviceInformationList());
        }

        // 获取{tableName} 
        public static DeviceInformation GetLangDeviceInformation(int LanguageId)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConn())
                {
                    return db.Queryable<DeviceInformation>().Where(it => it.LanguageId == LanguageId).First();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
