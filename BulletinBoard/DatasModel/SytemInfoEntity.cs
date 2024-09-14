using SqlSugar; 
using MesDatas.DatasServer; 
using System.ComponentModel; 
namespace MesDatas.DatasModel 
{ 
	[SugarTable("SytemInfoEntity")] 
	public class SytemInfoEntity 
	{ 
		[SugarColumn(ColumnName = "ID", IsPrimaryKey = true,IsIdentity = true)] 
		public int ID { get; set; } 	 //ID 
		[SugarColumn(ColumnName = "LanguageId",DefaultValue = "0")] 
		public int LanguageId { get; set; } 	 //语言ID 
		[SugarColumn(ColumnName = "IP", IsNullable = true)] 
		public string IP { get; set; } 	  //IP 
		[SugarColumn(ColumnName = "Port", IsNullable = true)] 
		public string Port { get; set; } 	  //Port 
		[SugarColumn(ColumnName = "Timeout", IsNullable = true)] 
		public string Timeout { get; set; } 	  //时间 
		[SugarColumn(ColumnName = "NcCode", IsNullable = true)] 
		public string NcCode { get; set; } 	  //NcCode 
		[SugarColumn(ColumnName = "Opration", IsNullable = true)] 
		public string Opration { get; set; } 	  //Opration 
		[SugarColumn(ColumnName = "Password", IsNullable = true)] 
		public string Password { get; set; } 	  //Password 
		[SugarColumn(ColumnName = "Resource", IsNullable = true)] 
		public string Resource { get; set; } 	  //Resource 
		[SugarColumn(ColumnName = "Site", IsNullable = true)] 
		public string Site { get; set; } 	  //Site 
		[SugarColumn(ColumnName = "Url", IsNullable = true)] 
		public string Url { get; set; } 	  //Url 
		[SugarColumn(ColumnName = "User", IsNullable = true)] 
		public string User { get; set; } 	  //User 
		//默认初始化值 SytemInfoEntity 
		 public static SytemInfoEntity InitGetSytemInfoEntity() 
		{ 
			 SytemInfoEntity syteminfoentity = new SytemInfoEntity();
			   syteminfoentity.ID = 1;
			   syteminfoentity.LanguageId = 0;
			 return  syteminfoentity;
		} 
		//保存 SytemInfoEntity 
		public string Save() 
		{ 
			 return  SytemInfoEntityServer.GetSytemInfoEntitySave(this);
		} 
		//更新 SytemInfoEntity 
		public string Update() 
		{ 
			 return  SytemInfoEntityServer.GetSytemInfoEntityUpdate(this);
		} 
		//删除 SytemInfoEntity 
		public string Delete() 
		{ 
			 return  SytemInfoEntityServer.GetSytemInfoEntityDelete(this);
		} 
	} 
 } 
