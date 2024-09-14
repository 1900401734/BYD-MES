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
	public class PrintersBtwServer 
	{ 
		//初始化 printersBtw 
		public static void InitPrintersBtw() 
		{ 
			try 
			{ 
				using (var db = DBConnSugClie.GetDBConn()) 
				{ 
					db.CodeFirst.InitTables<PrintersBtw>(); 
					PrintersBtw printersbtw =PrintersBtw.InitGetPrintersBtw(); 
					if(!db.Queryable<PrintersBtw>().Where(it => it.ID == printersbtw.ID).Any()) 
						{ 
							db.Insertable(printersbtw).ExecuteCommand(); 
						} 
				} 
			} 
			catch (Exception ex) 
			{ 
			} 
		} 
		//保存PrintersBtw 
		public static string GetPrintersBtwSave(PrintersBtw printersbtw) 
		{ 
			try 
			{ 
				using (var db = DBConnSugClie.GetDBConn()) 
				{ 
					if(db.Queryable<PrintersBtw>().Where(it => it.ID == printersbtw.ID).Any()) 
					{ 
						return db.Updateable(printersbtw).Where(it => it.ID == printersbtw.ID).ExecuteCommand() >0 ? LanguageUtiye.PassBtnSave : LanguageUtiye.ErrorBtnSave; 
					} 
					else 
					{ 
						return db.Insertable(printersbtw).ExecuteCommand() >0 ? LanguageUtiye.PassBtnSave : LanguageUtiye.ErrorBtnSave; 
					} 
				} 
				} 
				catch (Exception ex) 
				{
					return LanguageUtiye.UnknownError; 
				} 
			} 
			//通过SQL语句添加PrintersBtw 
			public static string GetSqlPrintersBtwInsert(PrintersBtw printersbtw) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						string sql = "INSERT INTO printersBtw ( +"; 
						 sql+="LanguageId, ";
								sql += "PtypeBtw,"; 
								sql += "PheadBtw,"; 
								sql += "PtimeBtw,"; 
								sql += "PtailBtw,"; 
								sql += "TFrontBtw,"; 
								sql += "TmonarchBtw,"; 
								sql += "TlowBtw,"; 
								sql += "PertowBtw,"; 
								sql += "TxttowBtw,"; 
								sql += "PlcmodelBtw,"; 
								sql += "MethodBtw,"; 
								sql += "ipBtw,"; 
								sql += "portBtw,"; 
								sql += "printCodeBtw,"; 
							sql +=" ID ) "; 
								sql += " VALUES ( +"; 
						 sql+="@LanguageId, ";
								sql += "@PtypeBtw,"; 
								sql += "@PheadBtw,"; 
								sql += "@PtimeBtw,"; 
								sql += "@PtailBtw,"; 
								sql += "@TFrontBtw,"; 
								sql += "@TmonarchBtw,"; 
								sql += "@TlowBtw,"; 
								sql += "@PertowBtw,"; 
								sql += "@TxttowBtw,"; 
								sql += "@PlcmodelBtw,"; 
								sql += "@MethodBtw,"; 
								sql += "@ipBtw,"; 
								sql += "@portBtw,"; 
								sql += "@printCodeBtw,"; 
							sql +=" @ID ) "; 
						return db.Ado.ExecuteCommand(sql, printersbtw) >0 ? LanguageUtiye.PassBtnAdd : LanguageUtiye.ErrorBtnAdd; 
					} 
					} 
					catch (Exception ex) 
					{ 
						return LanguageUtiye.UnknownError; 
					} 
				} 
			//修改PrintersBtw 
			public static string GetPrintersBtwUpdate(PrintersBtw printersbtw) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Updateable(printersbtw).Where(it => it.ID == printersbtw.ID).ExecuteCommand() >0 ? LanguageUtiye.PassBtnUpdate : LanguageUtiye.ErrorBtnUpdate; 
					} 
					} 
					catch (Exception ex) 
					{ 
						return LanguageUtiye.UnknownError; 
					} 
				} 
			//通过SQL语句修改PrintersBtw 
			public static string GetSqlPrintersBtwUpdate(PrintersBtw printersbtw) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						string sql = "UPDATE printersBtw SET " ; 
						sql+="PtypeBtw = @PtypeBtw, ";
 						sql+="PheadBtw = @PheadBtw, ";
 						sql+="PtimeBtw = @PtimeBtw, ";
 						sql+="PtailBtw = @PtailBtw, ";
 						sql+="TFrontBtw = @TFrontBtw, ";
 						sql+="TmonarchBtw = @TmonarchBtw, ";
 						sql+="TlowBtw = @TlowBtw, ";
 						sql+="PertowBtw = @PertowBtw, ";
 						sql+="TxttowBtw = @TxttowBtw, ";
 						sql+="PlcmodelBtw = @PlcmodelBtw, ";
 						sql+="MethodBtw = @MethodBtw, ";
 						sql+="ipBtw = @ipBtw, ";
 						sql+="portBtw = @portBtw, ";
 						sql+="printCodeBtw = @printCodeBtw, ";
 						sql+=" LanguageId = @LanguageId "; 
						 sql+="WHERE ID = @ID"; 
						return db.Ado.ExecuteCommand(sql, printersbtw) >0 ? LanguageUtiye.PassBtnUpdate : LanguageUtiye.ErrorBtnUpdate; 
					} 
					} 
					catch (Exception ex) 
					{ 
						return LanguageUtiye.UnknownError; 
					} 
				} 
			//删除PrintersBtw 
			public static string GetPrintersBtwDelete(PrintersBtw printersbtw) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Deleteable(printersbtw).ExecuteCommand() > 0 ? LanguageUtiye.PassBtnDelete : LanguageUtiye.ErrorBtnDelete; 
					} 
					} 
					catch (Exception ex) 
					{ 
						return LanguageUtiye.UnknownError; 
					} 
				} 
			//通过SQL语句删除PrintersBtw 
			public static string GetSqlPrintersBtwDelete(PrintersBtw printersbtw) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						string sql=$"DELETE FROM printersBtw WHERE ID = @ID"; 
						return db.Ado.ExecuteCommand(sql, printersbtw)> 0 ? LanguageUtiye.PassBtnDelete : LanguageUtiye.ErrorBtnDelete; 
					} 
					} 
					catch (Exception ex) 
					{ 
						return LanguageUtiye.UnknownError; 
					} 
				} 
			// 获取PrintersBtw 
			public static PrintersBtw GetPrintersBtw(int id) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<PrintersBtw>().Where(it=>it.ID == id).First(); 
					} 
					} 
					catch (Exception ex) 
					{ 
						return null; 
					} 
				} 
			// 获取PrintersBtw列表 
			public static List<PrintersBtw> GetPrintersBtwList() 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<PrintersBtw>().ToList(); 
					} 
					} 
					catch (Exception ex) 
					{ 
						return null; 
					} 
				} 
			//获取PrintersBtw列表 
			public static List<PrintersBtw> GetPrintersBtwList(int LanguageId) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<PrintersBtw>().Where(it=>it.LanguageId == LanguageId).ToList(); 
					} 
					} 
					catch (Exception ex) 
					{ 
						return null; 
					} 
				} 
			//获取BindingListPrintersBtw列表 
			public static BindingList<PrintersBtw> GetPrintersBtwBindingList() 
			{ 
				return new BindingList<PrintersBtw>( GetPrintersBtwList()); 
			} 
			// 获取{tableName} 
			public static PrintersBtw GetLangPrintersBtw(int LanguageId) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<PrintersBtw>().Where(it=>it.LanguageId == LanguageId).First(); 
					} 
					} 
					catch (Exception ex) 
					{ 
						return null; 
					} 
				} 
		} 
} 
