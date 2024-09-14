using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.Utiey
{
    class PRNchDPrnFiles
    {
        public static List<string> LoadAllPrnFilesFromDisk(string rootPath)
        {
            List<string> prnFiles = new List<string>();

            // 遍历指定根路径下的所有目录和文件  
            try
            {
                // 递归搜索所有.prn文件  
                SearchDirectoryForPrnFiles(rootPath, prnFiles);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("无法访问某些目录或文件，原因：" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("发生错误：" + ex.Message);
            }

            return prnFiles;
        }
        public static void SearchDirectoryForPrnFiles(string directoryPath, List<string> prnFiles)
        {
            try
            {
                // 获取当前目录下的所有.prn文件  
                string[] files = Directory.GetFiles(directoryPath, "*.prn");
                if (files.All(s => !string.IsNullOrWhiteSpace(s)))
                {
                    prnFiles.AddRange(files);
                }

                // 递归搜索子目录  
                foreach (var subDirectory in Directory.GetDirectories(directoryPath))
                {
                    SearchDirectoryForPrnFiles(subDirectory, prnFiles);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // 跳过没有访问权限的目录  
                Console.WriteLine($"跳过无访问权限的目录：{directoryPath}");
            }
            catch (Exception ex)
            {
                // 处理其他类型的异常（可选）  
                Console.WriteLine($"在目录 {directoryPath} 中发生异常：{ex.Message}");
            }
        }
    }
}
