using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MesDatas.Entity
{
    public static class PascalCaseConverter
    {
        /// <summary>
        /// 将测试项英文名称数组转换为Pascal命名格式
        /// </summary>
        /// <param name="testItems">测试项英文名称数组</param>
        /// <returns>Pascal命名格式的数组</returns>
        public static string[] ConvertToPascalCase(string[] testItems)
        {
            return testItems.Select(ConvertSingleItemToPascalCase).ToArray();
        }

        /// <summary>
        /// 将测试项英文名称列表转换为Pascal命名格式
        /// </summary>
        /// <param name="testItems">测试项英文名称列表</param>
        /// <returns>Pascal命名格式的列表</returns>
        public static List<string> ConvertToPascalCase(List<string> testItems)
        {
            return testItems.Select(ConvertSingleItemToPascalCase).ToList();
        }

        /// <summary>
        /// 将单个测试项英文名称转换为Pascal命名格式
        /// </summary>
        /// <param name="testItem">测试项英文名称</param>
        /// <returns>Pascal命名格式的字符串</returns>
        private static string ConvertSingleItemToPascalCase(string testItem)
        {
            if (string.IsNullOrWhiteSpace(testItem))
                return string.Empty;

            // 移除多余的空格并分割成单词
            string[] words = testItem.Trim()
                                   .Split(new char[] { ' ', '\t', '\n', '\r' },
                                         StringSplitOptions.RemoveEmptyEntries);

            // 处理每个单词
            var processedWords = words.Select(ProcessWord);

            // 连接所有单词
            return string.Join("", processedWords);
        }

        /// <summary>
        /// 处理单个单词，转换为适当的Pascal格式
        /// </summary>
        /// <param name="word">要处理的单词</param>
        /// <returns>处理后的单词</returns>
        private static string ProcessWord(string word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;

            // 处理特殊缩写词
            string upperWord = word.ToUpper();

            // CCD特殊处理
            if (upperWord == "CCD")
            {
                return "Ccd";
            }

            // 处理M+数字格式（如M5, M6）
            if (Regex.IsMatch(upperWord, @"^M\d+$"))
            {
                return char.ToUpper(word[0]) + word.Substring(1).ToLower();
            }

            // 处理其他常见缩写
            switch (upperWord)
            {
                case "ID":
                    return "Id";
                case "URL":
                    return "Url";
                case "XML":
                    return "Xml";
                case "HTML":
                    return "Html";
                case "API":
                    return "Api";
                default:
                    // 普通单词处理：首字母大写，其余小写
                    return char.ToUpper(word[0]) + word.Substring(1).ToLower();
            }
        }
    }
}

