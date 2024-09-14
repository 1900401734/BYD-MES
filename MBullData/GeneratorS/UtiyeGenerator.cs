using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBullData.GeneratorS
{
    public class UtiyeGenerator
    {
        public static string pathName = "D:\\MesDatas";
        public static void Generate(string tableName,List<string> columns,List<string> types,List<string> comments)
        {
            Directory.CreateDirectory(pathName);
            //实现代码生成
            ModelTable(tableName, columns, types, comments);
            GenerateServer(tableName, columns, types, comments);
            GeneratorDataGridView(tableName, columns, types, comments);
        }

        private static void ModelTable(string tableName, List<string> columns, List<string> types, List<string> comments)
        {
            string LowtableName = tableName.ToLower();
            Directory.CreateDirectory(pathName+"\\DatasModel");
            string fileName = pathName + "\\DatasModel\\" + tableName + ".cs";
            File.Create(fileName).Close();
            string content = "using SqlSugar; \r\n";
            content += "using MesDatas.DatasServer; \r\n";
            content += "using System.ComponentModel; \r\n";
            content += "namespace MesDatas.DatasModel \r\n";
            content += "{ \r\n";
            content += $"\t[SugarTable(\"{tableName}\")] \r\n";
            content += $"\tpublic class {tableName} \r\n";
            content += "\t{ \r\n";
            content += "\t\t[SugarColumn(ColumnName = \"ID\", IsPrimaryKey = true,IsIdentity = true)] \r\n";
            content += "\t\tpublic int ID { get; set; } \t //ID \r\n";
            content += "\t\t[SugarColumn(ColumnName = \"LanguageId\",DefaultValue = \"0\")] \r\n";
            content += "\t\tpublic int LanguageId { get; set; } \t //语言ID \r\n";
            for (int i = 0; i < columns.Count; i++)
            {
                content += $"\t\t[SugarColumn(ColumnName = \"{columns[i]}\", IsNullable = true)] \r\n";
                content += $"\t\tpublic {types[i]} {columns[i]} {{ get; set; }} \t  //{comments[i]} \r\n";
            }
            content += $"\t\t//默认初始化值 {tableName} \r\n";
            content += $"\t\t public static {tableName} InitGet{tableName}() \r\n";
            content += "\t\t{ \r\n";
            content += $"\t\t\t {tableName} {LowtableName} = new {tableName}();\r\n";
            content += $"\t\t\t   {LowtableName}.ID = 1;\r\n";
            content += $"\t\t\t   {LowtableName}.LanguageId = 0;\r\n";
            content += $"\t\t\t return  {LowtableName};\r\n";
            content += "\t\t} \r\n";
            content += $"\t\t//保存 {tableName} \r\n";
            content += "\t\tpublic string Save() \r\n";
            content += "\t\t{ \r\n";
            content += $"\t\t\t return  {tableName}Server.Get{tableName}Save(this);\r\n";
            content += "\t\t} \r\n";
            content += $"\t\t//更新 {tableName} \r\n";
            content += "\t\tpublic string Update() \r\n";
            content += "\t\t{ \r\n";
            content += $"\t\t\t return  {tableName}Server.Get{tableName}Update(this);\r\n";
            content += "\t\t} \r\n";
            content += $"\t\t//删除 {tableName} \r\n";
            content += "\t\tpublic string Delete() \r\n";
            content += "\t\t{ \r\n";
            content += $"\t\t\t return  {tableName}Server.Get{tableName}Delete(this);\r\n";
            content += "\t\t} \r\n";
            content += "\t} \r\n";
            content += " } \r\n";
            System.IO.File.WriteAllText(fileName, content);
        }
        public static void GenerateServer(string tableName, List<string> columns, List<string> types, List<string> comments)
        {
            Directory.CreateDirectory(pathName + "\\DatasServer");
            string fileName = pathName + "\\DatasServer\\" + tableName + "Server.cs";
            string LowtableName = tableName.ToLower();
            File.Create(fileName).Close();
            string content = "using MesDatas.DatasModel; \r\n";
            content += "using HslCommunication.LogNet; \r\n";
            content += "using MesDatas.Utility.IniLaguagePath;\r\n ";
            content += "using MesDatas.Utility.SugarDB;\r\n ";
            content += "using SqlSugar; \r\n";
            content += "using System; \r\n";
            content += "using System.Collections.Generic; \r\n";
            content += "using System.ComponentModel;  \r\n";
            content += "\r\n";
            content += "namespace MesDatas.DatasServer \r\n"; 
            content += "{ \r\n";
            content += $"\tpublic class {tableName}Server \r\n";
            content += "\t{ \r\n";
            content += $"\t\t//初始化 {tableName} \r\n";
            content += $"\t\tpublic static void Init{tableName}() \r\n"; 
            content += "\t\t{ \r\n";
            content += $"\t\t\ttry \r\n"; 
            content += "\t\t\t{ \r\n";
            content += $"\t\t\t\tusing (var db = DBConnSugClie.GetDBConn()) \r\n";
            content += "\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\tdb.CodeFirst.InitTables<{tableName}>(); \r\n";
            content += $"\t\t\t\t\t{tableName} {LowtableName} ={tableName}.InitGet{tableName}(); \r\n";
            content += $"\t\t\t\t\tif(!db.Queryable<{tableName}>().Where(it => it.ID == {LowtableName}.ID).Any()) \r\n";
            content += "\t\t\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\t\t\tdb.Insertable({LowtableName}).ExecuteCommand(); \r\n";
            content += "\t\t\t\t\t\t} \r\n";
            content += "\t\t\t\t} \r\n";
            content += "\t\t\t} \r\n";
            content += "\t\t\tcatch (Exception ex) \r\n";
            content += "\t\t\t{ \r\n";
//            content += $"\t\t\t\tLogUtils.LogNet.WriteError(\"[{tableName}]\", ex.Message.ToString());  \r\n";
            content += "\t\t\t} \r\n";
            content += "\t\t} \r\n";

            content += $"\t\t//保存{tableName} \r\n";
            content += $"\t\tpublic static string Get{tableName}Save({tableName} {LowtableName}) \r\n";
            content += "\t\t{ \r\n";
            content += $"\t\t\ttry \r\n";
            content += "\t\t\t{ \r\n";
            content += $"\t\t\t\tusing (var db = DBConnSugClie.GetDBConn()) \r\n";
            content += "\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\tif(db.Queryable<{tableName}>().Where(it => it.ID == {LowtableName}.ID).Any()) \r\n";
            content += "\t\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\t\treturn db.Updateable({LowtableName}).Where(it => it.ID == {LowtableName}.ID).ExecuteCommand() >0 ? LanguageUtiye.PassBtnSave : LanguageUtiye.ErrorBtnSave; \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\telse \r\n";
            content += "\t\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\t\treturn db.Insertable({LowtableName}).ExecuteCommand() >0 ? LanguageUtiye.PassBtnSave : LanguageUtiye.ErrorBtnSave; \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t} \r\n";
            content += "\t\t\t} \r\n";
            content += "\t\t\tcatch (Exception ex) \r\n";
            content += "\t\t\t{ \r\n";
//            content += $"\t\t\t\tLogUtils.LogNet.WriteError(\"[{tableName}]\", ex.Message.ToString()); \r\n";
            content += "\t\t\t\treturn LanguageUtiye.UnknownError; \r\n";
            content += "\t\t\t} \r\n";
            content += "\t\t} \r\n";

            content += $"\t\t//通过SQL语句添加{tableName} \r\n";
            content += $"\t\tpublic static string GetSql{tableName}Insert({tableName} {LowtableName}) \r\n";
            content += "\t\t{ \r\n";
            content += "\t\t\ttry \r\n";
            content += "\t\t\t{ \r\n";
            content += $"\t\t\t\tusing (var db = DBConnSugClie.GetDBConn()) \r\n";
            content += "\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\tstring sql = \"INSERT INTO {tableName} ( +\"; \r\n";
            content+= "\t\t\t\t\t sql+=\"LanguageId, \";\r\n";
            foreach (var item in columns)
            {
                content += $"\t\t\t\t\t\t\tsql += \"{item},\"; \r\n";
            }
            content += "\t\t\t\t\t\tsql +=\" ID ) \"; \r\n";
            content += "\t\t\t\t\t\t\tsql += \" VALUES ( +\"; \r\n";
            content += "\t\t\t\t\t sql+=\"@LanguageId, \";\r\n";
            foreach (var item in columns)
            {
                content += $"\t\t\t\t\t\t\tsql += \"@{item},\"; \r\n";
            }
            content += "\t\t\t\t\t\tsql +=\" @ID ) \"; \r\n";
            content += $"\t\t\t\t\treturn db.Ado.ExecuteCommand(sql, {LowtableName}) >0 ? LanguageUtiye.PassBtnAdd : LanguageUtiye.ErrorBtnAdd; \r\n";
            content += "\t\t\t\t} \r\n";
            content += "\t\t\t\t} \r\n";
            content += "\t\t\t\tcatch (Exception ex) \r\n";
            content += "\t\t\t\t{ \r\n";
//            content += $"\t\t\t\t\tLogUtils.LogNet.WriteError(\"[{tableName}]\", ex.Message.ToString()); \r\n";
            content += "\t\t\t\t\treturn LanguageUtiye.UnknownError; \r\n";
            content += "\t\t\t} \r\n";
            content += "\t\t} \r\n";

            content += $"\t\t\t//修改{tableName} \r\n";
            content += $"\t\t\tpublic static string Get{tableName}Update({tableName} {LowtableName}) \r\n";
            content += "\t\t\t{ \r\n";
            content += "\t\t\t\ttry \r\n";
            content += "\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\tusing (var db = DBConnSugClie.GetDBConn()) \r\n";
            content += "\t\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\t\treturn db.Updateable({LowtableName}).Where(it => it.ID == {LowtableName}.ID).ExecuteCommand() >0 ? LanguageUtiye.PassBtnUpdate : LanguageUtiye.ErrorBtnUpdate; \r\n";
            content += "\t\t\t\t} \r\n";
            content += "\t\t\t\t} \r\n";
            content += "\t\t\t\tcatch (Exception ex) \r\n";
            content += "\t\t\t\t{ \r\n";
 //           content += $"\t\t\t\t\tLogUtils.LogNet.WriteError(\"[{tableName}]\", ex.Message.ToString()); \r\n";
            content += "\t\t\t\t\treturn LanguageUtiye.UnknownError; \r\n";
            content += "\t\t\t\t} \r\n";
            content += "\t\t\t\t} \r\n";

            content += $"\t\t\t//通过SQL语句修改{tableName} \r\n";
            content += $"\t\t\tpublic static string GetSql{tableName}Update({tableName} {LowtableName}) \r\n";
            content += "\t\t\t{ \r\n";
            content += "\t\t\t\ttry \r\n";
            content += "\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\tusing (var db = DBConnSugClie.GetDBConn()) \r\n";
            content += "\t\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\t\tstring sql = \"UPDATE {tableName} SET \"; \r\n ";
            foreach (var item in columns)
            {
                content += $"\t\t\t\t\t\tsql+=\"{item} = @{item}, \" ; \r\n";
            }
            content += "\t\t\t\t\t\tsql+=\" LanguageId = @LanguageId \"; \r\n";
            content += $"\t\t\t\t\t\t sql+=\"WHERE ID = @ID\"; \r\n";
            content += $"\t\t\t\t\t\treturn db.Ado.ExecuteCommand(sql, {LowtableName}) >0 ? LanguageUtiye.PassBtnUpdate : LanguageUtiye.ErrorBtnUpdate; \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\tcatch (Exception ex) \r\n";
            content += "\t\t\t\t\t{ \r\n";
 //           content += $"\t\t\t\t\t\tLogUtils.LogNet.WriteError(\"[{tableName}]\", ex.Message.ToString()); \r\n";
            content += "\t\t\t\t\t\treturn LanguageUtiye.UnknownError; \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t} \r\n";

            content += $"\t\t\t//删除{tableName} \r\n";
            content += $"\t\t\tpublic static string Get{tableName}Delete({tableName} {LowtableName}) \r\n";
            content += "\t\t\t{ \r\n";
            content += "\t\t\t\ttry \r\n";
            content += "\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\tusing (var db = DBConnSugClie.GetDBConn()) \r\n";
            content += "\t\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\t\treturn db.Deleteable({LowtableName}).ExecuteCommand() > 0 ? LanguageUtiye.PassBtnDelete : LanguageUtiye.ErrorBtnDelete; \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\tcatch (Exception ex) \r\n";
            content += "\t\t\t\t\t{ \r\n";
//            content += $"\t\t\t\t\t\tLogUtils.LogNet.WriteError(\"[{tableName}]\", ex.Message.ToString()); \r\n";
            content += "\t\t\t\t\t\treturn LanguageUtiye.UnknownError; \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t} \r\n";

            content += $"\t\t\t//通过SQL语句删除{tableName} \r\n";
            content += $"\t\t\tpublic static string GetSql{tableName}Delete({tableName} {LowtableName}) \r\n";
            content += "\t\t\t{ \r\n";
            content += "\t\t\t\ttry \r\n";
            content += "\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\tusing (var db = DBConnSugClie.GetDBConn()) \r\n";
            content += "\t\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\t\tstring sql=$\"DELETE FROM {tableName} WHERE ID = @ID\"; \r\n";
            content += $"\t\t\t\t\t\treturn db.Ado.ExecuteCommand(sql, {LowtableName})> 0 ? LanguageUtiye.PassBtnDelete : LanguageUtiye.ErrorBtnDelete; \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t} \r\n";
            content += "\t\t\t\tcatch (Exception ex) \r\n";
            content += "\t\t\t\t{ \r\n";
 //           content += $"\t\t\t\t\tLogUtils.LogNet.WriteError(\"[{tableName}]\", ex.Message.ToString()); \r\n";
            content += "\t\t\t\t\treturn LanguageUtiye.UnknownError; \r\n";
            content += "\t\t\t\t} \r\n";
            content += "\t\t\t} \r\n";

            content += $"\t\t\t// 获取{tableName} \r\n";
            content += $"\t\t\tpublic static {tableName} Get{tableName}(int id) \r\n";
            content += "\t\t\t{ \r\n";
            content += "\t\t\t\ttry \r\n";
            content += "\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\tusing (var db = DBConnSugClie.GetDBConn()) \r\n";
            content += "\t\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\t\treturn db.Queryable<{tableName}>().Where(it=>it.ID == id).First(); \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\tcatch (Exception ex) \r\n";
            content += "\t\t\t\t\t{ \r\n";
   //         content += $"\t\t\t\t\t\tLogUtils.LogNet.WriteError(\"[ {tableName} ]\", ex.Message.ToString()); \r\n";
            content += "\t\t\t\t\t\treturn null; \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t} \r\n";
            content += $"\t\t\t// 获取{tableName}列表 \r\n";
            content += $"\t\t\tpublic static List<{tableName}> Get{tableName}List() \r\n";
            content += "\t\t\t{ \r\n";
            content += "\t\t\t\ttry \r\n";
            content += "\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\tusing (var db = DBConnSugClie.GetDBConn()) \r\n";
            content += "\t\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\t\treturn db.Queryable<{tableName}>().ToList(); \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\tcatch (Exception ex) \r\n";
            content += "\t\t\t\t\t{ \r\n";
    //        content += $"\t\t\t\t\t\tLogUtils.LogNet.WriteError(\"[ {tableName} ]\", ex.Message.ToString()); \r\n";
            content += "\t\t\t\t\t\treturn null; \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t} \r\n";
            content += $"\t\t\t// 获取System.Data.DataTable{tableName}列表 \r\n";
            content += $"\t\t\tpublic static System.Data.DataTable Get{tableName}DataTable() \r\n";
            content += "\t\t\t{ \r\n";
            content += "\t\t\t\ttry \r\n";
            content += "\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\tusing (var db = DBConnSugClie.GetDBConn()) \r\n";
            content += "\t\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\t\treturn db.Queryable<{tableName}>().ToDataTable(); \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\tcatch (Exception ex) \r\n";
            content += "\t\t\t\t\t{ \r\n";
   //         content += $"\t\t\t\t\t\tLogUtils.LogNet.WriteError(\"[ {tableName} ]\", ex.Message.ToString()); \r\n";
            content += "\t\t\t\t\t\treturn null; \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t} \r\n";


            content += $"\t\t\t//获取{tableName}列表 \r\n";
            content += $"\t\t\tpublic static List<{tableName}> Get{tableName}List(int LanguageId) \r\n";
            content += "\t\t\t{ \r\n";
            content += "\t\t\t\ttry \r\n";
            content += "\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\tusing (var db = DBConnSugClie.GetDBConn()) \r\n";
            content += "\t\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\t\treturn db.Queryable<{tableName}>().Where(it=>it.LanguageId == LanguageId).ToList(); \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\tcatch (Exception ex) \r\n";
            content += "\t\t\t\t\t{ \r\n";
 //           content += $"\t\t\t\t\t\tLogUtils.LogNet.WriteError(\"[ {tableName} ]\", ex.Message.ToString()); \r\n";
            content += "\t\t\t\t\t\treturn null; \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t} \r\n";
            content += $"\t\t\t//获取BindingList{tableName}列表 \r\n";
            content += $"\t\t\tpublic static BindingList<{tableName}> Get{tableName}BindingList() \r\n";
            content += "\t\t\t{ \r\n";
            content += $"\t\t\t\treturn new BindingList<{tableName}>( Get{tableName}List()); \r\n";
            content += "\t\t\t} \r\n";
            content += "\t\t\t// 获取{tableName} \r\n";
            content += $"\t\t\tpublic static {tableName} GetLang{tableName}(int LanguageId) \r\n";
            content += "\t\t\t{ \r\n";
            content += "\t\t\t\ttry \r\n";
            content += "\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\tusing (var db = DBConnSugClie.GetDBConn()) \r\n";
            content += "\t\t\t\t\t{ \r\n";
            content += $"\t\t\t\t\t\treturn db.Queryable<{tableName}>().Where(it=>it.LanguageId == LanguageId).First(); \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t\tcatch (Exception ex) \r\n";
            content += "\t\t\t\t\t{ \r\n";
     //       content += $"\t\t\t\t\t\tLogUtils.LogNet.WriteError(\"[  {tableName}  ]\", ex.Message.ToString()); \r\n";
            content += "\t\t\t\t\t\treturn null; \r\n";
            content += "\t\t\t\t\t} \r\n";
            content += "\t\t\t\t} \r\n";
            content += "\t\t} \r\n";
            content += "} \r\n";
            System.IO.File.WriteAllText(fileName, content);
        }
        public static void GeneratorDataGridView(string tableName, List<string> columns, List<string> types, List<string> comments)
        {
            Directory.CreateDirectory(pathName + "\\DatasDataGridView");
            string fileName = pathName + "\\DatasDataGridView\\" + tableName + "DataGridView.cs";
            File.Create(fileName).Close();
            string content = "using MesDatas.DatasServer;\r\n";
            content += "using MesDatas.Utility.IniLaguagePath;\r\n";
            content += "namespace MesDatas.DatasDataGridView \r\n";
            content += "{ \r\n";
            content += $"\tpublic class {tableName}DataGridView : LanguageUtiye \r\n";
            content += "\t{ \r\n";
            content += $"\t\tpublic static void Get{tableName}DataGridViewHeaderText(System.Windows.Forms.DataGridView dataGridView) \r\n";
            content += "\t\t{ \r\n";
            content += $"\t\t\tdataGridView.Columns[\"ID\"].HeaderText =inipathLanguage.IniLanguageValue(\"ID\", LanguageId); \r\n";
            content += $"\t\t\tdataGridView.Columns[\"LanguageId\"].HeaderText =inipathLanguage.IniLanguageValue(\"语言ID\", LanguageId); \r\n";
            for (int i = 0; i < columns.Count; i++)
            {
                content += $"\t\t\tdataGridView.Columns[\"{columns[i]}\"].HeaderText =inipathLanguage.IniLanguageValue(\"{comments[i]}\", LanguageId); \r\n";
            }
            content += "\t\t} \r\n";
            content += "\t} \r\n";
            content += "} \r\n";
            System.IO.File.WriteAllText(fileName, content);
        }
    }
}
