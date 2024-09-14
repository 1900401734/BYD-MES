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
	public class PrinterSettingServer 
	{ 
		//初始化 PrinterSetting 
		public static void InitPrinterSetting() 
		{ 
			try 
			{ 
				using (var db = DBConnSugClie.GetDBConn()) 
				{ 
					db.CodeFirst.InitTables<PrinterSetting>();


                    PrinterSetting printerSetting = PrinterSetting.PrinterSettingInitalize();
                    if (!db.Queryable<PrinterSetting>().Where(it => it.ID == printerSetting.ID).Any())
                    {
                        db.Insertable(printerSetting).ExecuteCommand();
                    }
                } 
			} 
			catch (Exception ex) 
			{ 
			} 
		} 
		//保存PrinterSetting 
		public static string GetPrinterSettingSave(PrinterSetting printersetting) 
		{ 
			try 
			{ 
				using (var db = DBConnSugClie.GetDBConn()) 
				{ 
					if(db.Queryable<PrinterSetting>().Where(it => it.ID == printersetting.ID).Any()) 
					{ 
						return db.Updateable(printersetting).ExecuteCommand() > 0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave; 
					} 
					else 
					{ 
						return db.Insertable(printersetting).ExecuteCommand() >0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave; 
					} 
				} 
				} 
				catch (Exception ex) 
				{ 

					return LanguageResour.ErrorBtnSave;
				} 
			} 
			//修改PrinterSetting 
			public static string GetPrinterSettingUpdate(PrinterSetting printersetting) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Updateable(printersetting).ExecuteCommand() > 0 ? LanguageResour.PassBtnUpdate : LanguageResour.ErrorBtnUpdate; 
					} 
					} 
					catch (Exception ex) 
					{ 
						return LanguageResour.ErrorBtnUpdate;
					} 
				} 
			//删除PrinterSetting 
			public static string GetPrinterSettingDelete(PrinterSetting printersetting) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Deleteable(printersetting).ExecuteCommand() > 0 ? LanguageResour.PassBtnDelete : LanguageResour.ErrorBtnDelete; 
					} 
					} 
					catch (Exception ex) 
					{ 
						return LanguageResour.ErrorBtnDelete;
					} 
				} 
			// 获取PrinterSetting 
			public static PrinterSetting GetPrinterSetting(int id) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<PrinterSetting>().Where(it=>it.ID == id).First(); 
					} 
					} 
					catch (Exception ex) 
					{ 

						return null; 
					} 
				} 
			// 获取PrinterSetting列表 
			public static List<PrinterSetting> GetPrinterSettingList() 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<PrinterSetting>().ToList(); 
					} 
					} 
					catch (Exception ex) 
					{ 

						return null; 
					} 
				} 
			//获取PrinterSetting列表 
			public static List<PrinterSetting> GetPrinterSettingList(int LanguageId) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<PrinterSetting>().Where(it=>it.LanguageId == LanguageId).ToList(); 
					} 
					} 
					catch (Exception ex) 
					{ 
 
						return null; 
					} 
				} 
			//获取BindingListPrinterSetting列表 
			public static BindingList<PrinterSetting> GetPrinterSettingBindingList() 
			{ 
				return new BindingList<PrinterSetting>( GetPrinterSettingList()); 
			} 
			// 获取{tableName} 
			public static PrinterSetting GetLangPrinterSetting(int LanguageId) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<PrinterSetting>().Where(it=>it.LanguageId == LanguageId).First(); 
					} 
					} 
					catch (Exception ex) 
					{ 
 
						return null; 
					} 
				} 
		} 
} 
