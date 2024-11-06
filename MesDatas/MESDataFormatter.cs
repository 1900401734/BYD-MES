using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas
{
    public class MESDataFormatter
    {
        public class TestItemInfo
        {
            public string Name { get; set; }
            public string MaxValue { get; set; }
            public string MinValue { get; set; }
            public string ActualValue { get; set; }
            public string TestResult { get; set; }
        }

        /// <summary>
        /// 格式化工装信息
        /// </summary>
        public static string FormatFixtureInfo(string fixtureBindingText)
        {
            var sb = new StringBuilder();
            var fixtureInfo = fixtureBindingText.Split('+');

            for (int i = 0; i < fixtureInfo.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(fixtureInfo[i]))
                {
                    sb.Append($"!工装编号{i + 1},工装,{fixtureInfo[i]}");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 格式化产品代码信息
        /// </summary>
        public static string FormatProductCodes(string[] productCodes)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < productCodes.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(productCodes[i]))
                {
                    sb.Append($"!产品物料号{i + 1},物料,{productCodes[i]}");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 格式化测试项信息
        /// </summary>
        /// <param name="testItemsName"></param>
        /// <param name="maxValuePoint"></param>
        /// <param name="minValuePoint"></param>
        /// <param name="resultPoint"> </param>
        /// <param name="actualValueList"></param>
        /// <param name="maxList"></param>
        /// <param name="minList"></param>
        /// <param name="resultList"></param>
        public static string FormatTestItems(string[] testItemsName, string[] maxValuePoint, string[] minValuePoint,
            string[] resultPoint, List<string> actualValueList, List<string> resultList, List<string> maxList, List<string> minList)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < testItemsName.Length; i++)
            {
                if (actualValueList[i] == "null") continue;

                if (maxValuePoint[i] != "NO" && minValuePoint[i] != "NO" && resultPoint[i] != "NO")
                {
                    if (resultList[i] != "null")
                    {
                        sb.Append($"!{testItemsName[i]},{maxList[i]}~{minList[i]},{actualValueList[i]}");
                        sb.Append($"!{testItemsName[i]}测试结果,结果,{resultList[i]}");
                    }
                }
                else
                {
                    sb.Append($"!{testItemsName[i]},测试项值,{actualValueList[i]}");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 构建完整的测试项信息字符串
        /// </summary>
        public static string BuildTestInfoString(string loginUser, string productBarcode, string productModel,
            string fixtureInfo, string productCodes, string testItemsInfo, string finalResult)
        {
            return $"!用户ID,{loginUser},{loginUser}" +
                   $"!条码,条码信息,{productBarcode}" +
                   $"!产品型号,型号,{productModel}" +
                   fixtureInfo +
                   productCodes +
                   testItemsInfo +
                   $"!测试总结果,测试结果,{finalResult}";
        }
    }
}
