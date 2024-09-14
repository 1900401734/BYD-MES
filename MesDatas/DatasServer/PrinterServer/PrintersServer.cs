using MesDatas.DatasModel; 
using HslCommunication.LogNet; 
using MesDatas.Utility.IniLaguagePath;
 using MesDatas.Utility.SugarDB;
 using SqlSugar; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel;  

namespace MesDatas.DatasServer 
{ 
	public class PrintersServer 
	{ 
		//初始化 Printers 
		public static void InitPrinters() 
		{ 
			try 
			{ 
				using (var db = DBConnSugClie.GetDBConn()) 
				{ 
					db.CodeFirst.InitTables<Printers>();
                    Printers printers = Printers.InitGetPrinters();
					if(!db.Queryable<Printers>().Where(it => it.ID == printers.ID).Any()) 
						{ 
							db.Insertable(printers).ExecuteCommand(); 
						} 
				} 
			} 
			catch (Exception ex) 
			{ 
			} 
		} 
		//保存Printers 
		public static string GetPrintersSave(Printers printers) 
		{ 
			try 
			{ 
				using (var db = DBConnSugClie.GetDBConn()) 
				{ 
					if(db.Queryable<Printers>().Where(it => it.ID == printers.ID).Any()) 
					{ 
						return db.Updateable(printers).Where(it => it.ID == printers.ID).ExecuteCommand() >0 ? LanguageUtiye.PassBtnSave : LanguageUtiye.ErrorBtnSave; 
					} 
					else 
					{ 
						return db.Insertable(printers).ExecuteCommand() >0 ? LanguageUtiye.PassBtnSave : LanguageUtiye.ErrorBtnSave; 
					} 
				} 
				} 
				catch (Exception ex) 
				{ 
					return LanguageUtiye.UnknownError; 
				} 
			} 
			//通过SQL语句添加Printers 
			public static string GetSqlPrintersInsert(Printers printers) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						string sql = "INSERT INTO Printers ( +"; 
						 sql+="LanguageId, ";
								sql += "Ptype,"; 
								sql += "Phead,"; 
								sql += "Ptime,"; 
								sql += "Ptail,"; 
								sql += "TFront,"; 
								sql += "Tmonarch,"; 
								sql += "Tlow,"; 
								sql += "Pertow,"; 
								sql += "Txttow,"; 
								sql += "Plcmodel,"; 
								sql += "Method,"; 
								sql += "ip,"; 
								sql += "port,"; 
								sql += "printCode,"; 
							sql +=" ID ) "; 
								sql += " VALUES ( +"; 
						 sql+="@LanguageId, ";
								sql += "@Ptype,"; 
								sql += "@Phead,"; 
								sql += "@Ptime,"; 
								sql += "@Ptail,"; 
								sql += "@TFront,"; 
								sql += "@Tmonarch,"; 
								sql += "@Tlow,"; 
								sql += "@Pertow,"; 
								sql += "@Txttow,"; 
								sql += "@Plcmodel,"; 
								sql += "@Method,"; 
								sql += "@ip,"; 
								sql += "@port,"; 
								sql += "@printCode,"; 
							sql +=" @ID ) "; 
						return db.Ado.ExecuteCommand(sql, printers) >0 ? LanguageUtiye.PassBtnAdd : LanguageUtiye.ErrorBtnAdd; 
					} 
					} 
					catch (Exception ex) 
					{  
						return LanguageUtiye.UnknownError; 
					} 
				} 
			//修改Printers 
			public static string GetPrintersUpdate(Printers printers) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Updateable(printers).Where(it => it.ID == printers.ID).ExecuteCommand() >0 ? LanguageUtiye.PassBtnUpdate : LanguageUtiye.ErrorBtnUpdate; 
					} 
					} 
					catch (Exception ex) 
					{ 
						return LanguageUtiye.UnknownError; 
					} 
				} 
			//通过SQL语句修改Printers 
			public static string GetSqlPrintersUpdate(Printers printers) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{
					string sql = "UPDATE Printers SET ";
						sql += "Ptype = @Ptype, ";
					sql += "Phead = @Phead, ";
					sql += "Ptime = @Ptime, ";
					sql += "Ptail = @Ptail, ";
					sql += "TFront = @TFront, ";
					sql += "Tmonarch = @Tmonarch, ";
					sql += "Tlow = @Tlow, ";
					sql += "Pertow = @Pertow, ";
					sql += "Txttow = @Txttow, ";
					sql += "Plcmodel = @Plcmodel, ";
					sql += "Method = @Method, ";
					sql += "ip = @ip, ";
					sql += "port = @port, ";
					sql += "printCode = @printCode, ";
					sql+=" LanguageId = @LanguageId "; 
						 sql+="WHERE ID = @ID"; 
						return db.Ado.ExecuteCommand(sql, printers) >0 ? LanguageUtiye.PassBtnUpdate : LanguageUtiye.ErrorBtnUpdate; 
					} 
					} 
					catch (Exception ex) 
					{ 
						return LanguageUtiye.UnknownError; 
					} 
				} 
			//删除Printers 
			public static string GetPrintersDelete(Printers printers) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Deleteable(printers).ExecuteCommand() > 0 ? LanguageUtiye.PassBtnDelete : LanguageUtiye.ErrorBtnDelete; 
					} 
					} 
					catch (Exception ex) 
					{ 
						return LanguageUtiye.UnknownError; 
					} 
				} 
			//通过SQL语句删除Printers 
			public static string GetSqlPrintersDelete(Printers printers) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						string sql=$"DELETE FROM Printers WHERE ID = @ID"; 
						return db.Ado.ExecuteCommand(sql, printers)> 0 ? LanguageUtiye.PassBtnDelete : LanguageUtiye.ErrorBtnDelete; 
					} 
					} 
					catch (Exception ex) 
					{ 
						return LanguageUtiye.UnknownError; 
					} 
				} 
			// 获取Printers 
			public static Printers GetPrinters(int id) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<Printers>().Where(it=>it.ID == id).First(); 
					} 
					} 
					catch (Exception ex) 
					{ 
						return null; 
					} 
				} 
			// 获取Printers列表 
			public static List<Printers> GetPrintersList() 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<Printers>().ToList(); 
					} 
					} 
					catch (Exception ex) 
					{ 
						return null; 
					} 
				} 
			//获取Printers列表 
			public static List<Printers> GetPrintersList(int LanguageId) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<Printers>().Where(it=>it.LanguageId == LanguageId).ToList(); 
					} 
					} 
					catch (Exception ex) 
					{ 
						return null; 
					} 
				} 
			//获取BindingListPrinters列表 
			public static BindingList<Printers> GetPrintersBindingList() 
			{ 
				return new BindingList<Printers>( GetPrintersList()); 
			} 
			// 获取{tableName} 
			public static Printers GetLangPrinters(int LanguageId) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<Printers>().Where(it=>it.LanguageId == LanguageId).First(); 
					} 
					} 
					catch (Exception ex) 
					{ 
						return null; 
					} 
				} 
		} 
} 
