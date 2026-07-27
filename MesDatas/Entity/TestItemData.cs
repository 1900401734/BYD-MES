using MesDatas.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.Entity
{
    /// <summary>
    /// 用于测试数据的实时绑定
    /// </summary>
    public class TestItemData
    {
        public string ItemName { get; set; }       // 测试项名称
        public string ActualValue { get; set; }    // 实际值
        public string MaxValue { get; set; }       // 上限值
        public string MinValue { get; set; }       // 下限值
        public string Result { get; set; }         // 测试结果
        public string StandardValue { get; set; }  // 标准值
    }
}

// 工位结果数据结构
public class StationResult
{
    public string stationId { get; set; }
    public string Barcode { get; set; }
    public string Result { get; set; } // "OK" 或 "NG"
    public DateTime TestTime { get; set; } = DateTime.Now; // 添加时间戳
    public List<string> actualValueList { get; set; }// 实际值
    public List<string> maxList { get; set; }        // 上限
    public List<string> minList { get; set; }      // 下限
    public List<string> resultList { get; set; }  // 结果
    public List<string> nameList { get; set; }      // 工位名称
}

public class StationDataManager
{
    private readonly string _dataFilePath;
    private readonly int _maxCacheSize;
    private readonly TimeSpan _dataExpireTime;
    private readonly object _lockObject = new object();

    public StationDataManager(string dataFilePath = "StationData.json", int maxCacheSize = 1000, TimeSpan? dataExpireTime = null)
    {
        _dataFilePath = dataFilePath;
        _maxCacheSize = maxCacheSize;
        _dataExpireTime = dataExpireTime ?? TimeSpan.FromHours(24); // 默认24小时过期
    }

    #region 数据持久化

    /// <summary>
    /// 保存数据到文件
    /// </summary>
    public async Task SaveDataAsync(List<StationResult> stationResultList1, List<StationResult> stationResultList2)
    {
        try
        {
            var data = new
            {
                StationResultList1 = stationResultList1,
                StationResultList2 = stationResultList2,
                SaveTime = DateTime.Now
            };

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_dataFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"保存数据失败: {ex.Message}");
        }
    }
    /// <summary>
    /// 从文件加载数据
    /// </summary>
    public async Task<(List<StationResult> stationResultList1, List<StationResult> stationResultList2)> LoadDataAsync()
    {
        try
        {
            if (!File.Exists(_dataFilePath))
            {
                return (new List<StationResult>(), new List<StationResult>());
            }

            string json = File.ReadAllText(_dataFilePath);
            var data = JsonConvert.DeserializeObject<dynamic>(json);

            var stationResultList1 = JsonConvert.DeserializeObject<List<StationResult>>(data.StationResultList1.ToString());
            var stationResultList2 = JsonConvert.DeserializeObject<List<StationResult>>(data.StationResultList2.ToString());

            // 清理过期数据
            stationResultList1 = CleanExpiredData(stationResultList1);
            stationResultList2 = CleanExpiredData(stationResultList2);

            return (stationResultList1, stationResultList2);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"加载数据失败: {ex.Message}");
            return (new List<StationResult>(), new List<StationResult>());
        }
    }

    #endregion

    #region 内存管理

    /// <summary>
    /// 清理过期数据
    /// </summary>
    private List<StationResult> CleanExpiredData(List<StationResult> dataList)
    {
        if (dataList == null) return new List<StationResult>();

        var cutoffTime = DateTime.Now - _dataExpireTime;
        return dataList.Where(x => x.TestTime > cutoffTime).ToList();
    }

    /// <summary>
    /// 清理已完成的测试数据
    /// </summary>
    public void CleanCompletedData(List<StationResult> stationResultList1, List<StationResult> stationResultList2, string completedBarcode)
    {
        lock (_lockObject)
        {
            // 移除已完成的条码数据
            stationResultList1.RemoveAll(x => x.Barcode == completedBarcode);
            stationResultList2.RemoveAll(x => x.Barcode == completedBarcode);

            Console.WriteLine($"已清理条码 {completedBarcode} 的数据");
        }
    }

    /// <summary>
    /// 限制缓存大小
    /// </summary>
    public void LimitCacheSize(List<StationResult> stationResultList1, List<StationResult> stationResultList2)
    {
        lock (_lockObject)
        {
            // 按时间排序，移除最旧的数据
            if (stationResultList1.Count > _maxCacheSize)
            {
                var sortedList1 = stationResultList1.OrderBy(x => x.TestTime).ToList();
                int removeCount = stationResultList1.Count - _maxCacheSize;
                stationResultList1.Clear();
                stationResultList1.AddRange(sortedList1.Skip(removeCount));
            }

            if (stationResultList2.Count > _maxCacheSize)
            {
                var sortedList2 = stationResultList2.OrderBy(x => x.TestTime).ToList();
                int removeCount = stationResultList2.Count - _maxCacheSize;
                stationResultList2.Clear();
                stationResultList2.AddRange(sortedList2.Skip(removeCount));
            }
        }
    }

    /// <summary>
    /// 定时清理过期数据
    /// </summary>
    public void CleanExpiredDataPeriodically(List<StationResult> stationResultList1, List<StationResult> stationResultList2)
    {
        lock (_lockObject)
        {
            var cutoffTime = DateTime.Now - _dataExpireTime;

            int removed1 = stationResultList1.RemoveAll(x => x.TestTime < cutoffTime);
            int removed2 = stationResultList2.RemoveAll(x => x.TestTime < cutoffTime);

            if (removed1 > 0 || removed2 > 0)
            {
                Console.WriteLine($"定时清理: 移除了 {removed1 + removed2} 条过期数据");
            }
        }
    }

    #endregion
}

/// <summary>
/// 用于MES上传的数据模型
/// </summary>
public class UploadDataModel
{
    public List<string> ResultList { get; set; } = new List<string>();
    public List<string> ActualValueList { get; set; } = new List<string>();
    public List<string> MaxList { get; set; } = new List<string>();
    public List<string> MinList { get; set; } = new List<string>();
    public string[] TestItemsNames { get; set; } = new string[0];
    public string[] MaxValuePoint { get; set; } = new string[0];
    public string[] MinValuePoint { get; set; } = new string[0];
    public string FinalResult { get; set; } = "NG";
}

/// <summary>
/// 列信息结构，用于统一管理列的创建和更新
/// </summary>
public class ColumnInfo
{
    public string HeaderKey { get; set; }           // 资源键
    public string TestItemName { get; set; }        // 测试项名称
    public string ColumnType { get; set; }          // 列类型：Value/UpperLimit/LowerLimit/Result
    public string Unit { get; set; }               // 单位
    public int TestItemIndex { get; set; }         // 在测试项数组中的索引
}