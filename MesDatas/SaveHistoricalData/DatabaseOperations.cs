using MesDatas;
using MesDatas.Utiey;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class DatabaseOperations
{
    //private readonly Control invokeTarget;
    private readonly string txtProductModel;
    private readonly string txtWorkOrder;
    private readonly string txtFixtureBinding;
    private readonly string cboBarcodeRuleAndFixtures;
    private readonly string LoginUser;
    private readonly string[] testItems;
    private readonly string[] maxValue;
    private readonly string[] minValue;
    private readonly string[] testResult;

    public DatabaseOperations(string txtProductModel, string txtWorkOrder, string txtFixtureBinding,
                              string cboBarcodeRuleAndFixtures, string LoginUser, string[] testItems, string[] maxValue,
                              string[] minValue, string[] testResult)
    {
        this.txtProductModel = txtProductModel;
        this.txtWorkOrder = txtWorkOrder;
        this.txtFixtureBinding = txtFixtureBinding;
        this.cboBarcodeRuleAndFixtures = cboBarcodeRuleAndFixtures;
        this.LoginUser = LoginUser;
        this.testItems = testItems;
        this.maxValue = maxValue;
        this.minValue = minValue;
        this.testResult = testResult;
    }

    public async Task TestAsync(string conn, string barcodeInfo, string[] Value, DataTable codesTable,
                                List<string> list, List<string> maxList, List<string> minList, List<string> resultList)
    {
        await Task.Run(() =>
        {

            CreateDatabaseAndTable(conn);
            InsertData(conn, barcodeInfo, Value, codesTable, list, maxList, minList, resultList);

        });
    }

    private void CreateDatabaseAndTable(string conn)
    {
        mdbDatas.CreateAccessDatabase(conn);
        var (columnNames, arrayList) = PrepareColumnData();
        mdbDatas.CreateMDBTable(conn, "Sheet1", arrayList);
    }

    private (string columnNames, ArrayList arrayList) PrepareColumnData()
    {
        var baseColumns = new[] { "产品", "当前工单号", "工装编号", "产品编码", "条码", "测试人", "测试时间", "测试结果" };
        var columnNames = new StringBuilder(string.Join(",", baseColumns));
        var arrayList = new ArrayList(baseColumns);

        if (testItems.Length > 0)
        {
            for (int i = 0; i < testItems.Length; i++)
            {
                columnNames.Append($",{testItems[i]}");
                arrayList.Add(testItems[i]);

                if (maxValue[i] != "NO" && minValue[i] != "NO" && testResult[i] != "NO")
                {
                    string[] additionalColumns = { $"{testItems[i]}上限", $"{testItems[i]}下限", $"{testItems[i]}结果" };
                    columnNames.Append($",{string.Join(",", additionalColumns)}");
                    arrayList.AddRange(additionalColumns);
                }
            }
        }

        return (columnNames.ToString(), arrayList);
    }

    private void InsertData(string conn, string barcodeInfo, string[] Value, DataTable codesTable,
                            List<string> list, List<string> maxList, List<string> minList, List<string> resultList)
    {
        var mdb = new mdbDatas(conn);
        try
        {
            var (columnNames, values) = PrepareInsertData(barcodeInfo, Value, codesTable, list, maxList, minList, resultList);
            string sql = $"INSERT INTO Sheet1 ({columnNames}) VALUES ({values})";
            bool result = mdb.Add(sql);
            // Handle result if needed
        }
        finally
        {
            mdb.CloseConnection();
        }
    }

    private (string columnNames, string values) PrepareInsertData(string barcodeInfo, string[] Value, DataTable codesTable,
                                                                  List<string> list, List<string> maxList, List<string> minList, List<string> resultList)
    {
        var (columnNames, _) = PrepareColumnData();
        var values = new StringBuilder();

        // Basic information
        string CP = string.IsNullOrEmpty(txtProductModel) ? "无" : txtProductModel;
        string barcode = string.IsNullOrEmpty(barcodeInfo) ? " " : barcodeInfo;
        DateTime now = DateTime.Now;

        values.Append($"'{CP}',");
        values.Append($"'{txtWorkOrder}',");
        values.Append($"'{txtFixtureBinding}',");
        values.Append($"'{CodeNum.GetProductCodeString(cboBarcodeRuleAndFixtures, codesTable)}',");
        values.Append($"'{barcode}',");
        values.Append($"'{LoginUser}',");
        values.Append($"'{now:yyyy年MM月dd日 HH:mm:ss}',");
        values.Append($"'{Value[9999]}'");

        // Test items
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                values.Append($",'{(list[i].Length > 0 ? list[i] : " ")}'");

                if (maxValue[i] != "NO" && minValue[i] != "NO" && testResult[i] != "NO")
                {
                    values.Append($",'{CodeNum.GetNullIfEmpty(maxList[i])}'");
                    values.Append($",'{CodeNum.GetNullIfEmpty(minList[i])}'");
                    values.Append($",'{CodeNum.GetNullIfEmpty(resultList[i])}'");
                }
            }
        }

        return (columnNames, values.ToString());
    }
}

