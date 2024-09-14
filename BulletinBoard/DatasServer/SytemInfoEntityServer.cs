using MesDatas.DatasModel; 
using MesDatas.Utility.IniLaguagePath;
 using MesDatas.Utility.SugarDB;
 using SqlSugar; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel;  

namespace MesDatas.DatasServer 
{ 
	public class SytemInfoEntityServer 
	{ 
		//初始化 SytemInfoEntity 
		public static void InitSytemInfoEntity() 
		{ 
			try 
			{ 
				using (var db = DBConnSugClie.GetDBConn()) 
				{ 
					db.CodeFirst.InitTables<SytemInfoEntity>(); 
					SytemInfoEntity syteminfoentity =SytemInfoEntity.InitGetSytemInfoEntity(); 
					if(!db.Queryable<SytemInfoEntity>().Where(it => it.ID == syteminfoentity.ID).Any()) 
						{ 
							db.Insertable(syteminfoentity).ExecuteCommand(); 
						} 
				} 
			} 
			catch (Exception ex) 
			{ 
			} 
		} 
		//保存SytemInfoEntity 
		public static string GetSytemInfoEntitySave(SytemInfoEntity syteminfoentity) 
		{ 
			try 
			{ 
				using (var db = DBConnSugClie.GetDBConn()) 
				{ 
					if(db.Queryable<SytemInfoEntity>().Where(it => it.ID == syteminfoentity.ID).Any()) 
					{ 
						return db.Updateable(syteminfoentity).Where(it => it.ID == syteminfoentity.ID).ExecuteCommand() >0 ? LanguageUtiye.PassBtnSave : LanguageUtiye.ErrorBtnSave; 
					} 
					else 
					{ 
						return db.Insertable(syteminfoentity).ExecuteCommand() >0 ? LanguageUtiye.PassBtnSave : LanguageUtiye.ErrorBtnSave; 
					} 
				} 
			} 
			catch (Exception ex) 
			{ 
				return LanguageUtiye.UnknownError; 
			} 
		} 
		//通过SQL语句添加SytemInfoEntity 
		public static string GetSqlSytemInfoEntityInsert(SytemInfoEntity syteminfoentity) 
		{ 
			try 
			{ 
				using (var db = DBConnSugClie.GetDBConn()) 
				{ 
					string sql = "INSERT INTO SytemInfoEntity ( +"; 
					 sql+="LanguageId, ";
							sql += "IP,"; 
							sql += "Port,"; 
							sql += "Timeout,"; 
							sql += "NcCode,"; 
							sql += "Opration,"; 
							sql += "Password,"; 
							sql += "Resource,"; 
							sql += "Site,"; 
							sql += "Url,"; 
							sql += "User,"; 
						sql +=" ID ) "; 
							sql += " VALUES ( +"; 
					 sql+="@LanguageId, ";
							sql += "@IP,"; 
							sql += "@Port,"; 
							sql += "@Timeout,"; 
							sql += "@NcCode,"; 
							sql += "@Opration,"; 
							sql += "@Password,"; 
							sql += "@Resource,"; 
							sql += "@Site,"; 
							sql += "@Url,"; 
							sql += "@User,"; 
						sql +=" @ID ) "; 
					return db.Ado.ExecuteCommand(sql, syteminfoentity) >0 ? LanguageUtiye.PassBtnAdd : LanguageUtiye.ErrorBtnAdd; 
				} 
				} 
				catch (Exception ex) 
				{ 
					return LanguageUtiye.UnknownError; 
			} 
		} 
			//修改SytemInfoEntity 
			public static string GetSytemInfoEntityUpdate(SytemInfoEntity syteminfoentity) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Updateable(syteminfoentity).Where(it => it.ID == syteminfoentity.ID).ExecuteCommand() >0 ? LanguageUtiye.PassBtnUpdate : LanguageUtiye.ErrorBtnUpdate; 
				} 
				} 
				catch (Exception ex) 
				{ 
					return LanguageUtiye.UnknownError; 
				} 
				} 
			//通过SQL语句修改SytemInfoEntity 
			public static string GetSqlSytemInfoEntityUpdate(SytemInfoEntity syteminfoentity) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						string sql = "UPDATE SytemInfoEntity SET "; 
 						sql+="IP = @IP, " ; 
						sql+="Port = @Port, " ; 
						sql+="Timeout = @Timeout, " ; 
						sql+="NcCode = @NcCode, " ; 
						sql+="Opration = @Opration, " ; 
						sql+="Password = @Password, " ; 
						sql+="Resource = @Resource, " ; 
						sql+="Site = @Site, " ; 
						sql+="Url = @Url, " ; 
						sql+="User = @User, " ; 
						sql+=" LanguageId = @LanguageId "; 
						 sql+="WHERE ID = @ID"; 
						return db.Ado.ExecuteCommand(sql, syteminfoentity) >0 ? LanguageUtiye.PassBtnUpdate : LanguageUtiye.ErrorBtnUpdate; 
					} 
					} 
					catch (Exception ex) 
					{ 
						return LanguageUtiye.UnknownError; 
					} 
				} 
			//删除SytemInfoEntity 
			public static string GetSytemInfoEntityDelete(SytemInfoEntity syteminfoentity) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Deleteable(syteminfoentity).ExecuteCommand() > 0 ? LanguageUtiye.PassBtnDelete : LanguageUtiye.ErrorBtnDelete; 
					} 
					} 
					catch (Exception ex) 
					{ 
						return LanguageUtiye.UnknownError; 
					} 
				} 
			//通过SQL语句删除SytemInfoEntity 
			public static string GetSqlSytemInfoEntityDelete(SytemInfoEntity syteminfoentity) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						string sql=$"DELETE FROM SytemInfoEntity WHERE ID = @ID"; 
						return db.Ado.ExecuteCommand(sql, syteminfoentity)> 0 ? LanguageUtiye.PassBtnDelete : LanguageUtiye.ErrorBtnDelete; 
					} 
				} 
				catch (Exception ex) 
				{ 
					return LanguageUtiye.UnknownError; 
				} 
			} 
			// 获取SytemInfoEntity 
			public static SytemInfoEntity GetSytemInfoEntity(int id) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<SytemInfoEntity>().Where(it=>it.ID == id).First(); 
					} 
					} 
					catch (Exception ex) 
					{ 
						return null; 
					} 
				} 
			// 获取SytemInfoEntity列表 
			public static List<SytemInfoEntity> GetSytemInfoEntityList() 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<SytemInfoEntity>().ToList(); 
					} 
					} 
					catch (Exception ex) 
					{ 
						return null; 
					} 
				} 
			// 获取System.Data.DataTableSytemInfoEntity列表 
			public static System.Data.DataTable GetSytemInfoEntityDataTable() 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<SytemInfoEntity>().ToDataTable(); 
					} 
					} 
					catch (Exception ex) 
					{ 
						return null; 
					} 
				} 
			//获取SytemInfoEntity列表 
			public static List<SytemInfoEntity> GetSytemInfoEntityList(int LanguageId) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<SytemInfoEntity>().Where(it=>it.LanguageId == LanguageId).ToList(); 
					} 
					} 
					catch (Exception ex) 
					{ 
						return null; 
					} 
				} 
			//获取BindingListSytemInfoEntity列表 
			public static BindingList<SytemInfoEntity> GetSytemInfoEntityBindingList() 
			{ 
				return new BindingList<SytemInfoEntity>( GetSytemInfoEntityList()); 
			} 
			// 获取{tableName} 
			public static SytemInfoEntity GetLangSytemInfoEntity(int LanguageId) 
			{ 
				try 
				{ 
					using (var db = DBConnSugClie.GetDBConn()) 
					{ 
						return db.Queryable<SytemInfoEntity>().Where(it=>it.LanguageId == LanguageId).First(); 
					} 
					} 
					catch (Exception ex) 
					{ 
						return null; 
					} 
				} 
		} 
} 
