using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.Utility.PrintersFileChecker
{
    public static class PrinFileChecker
    {
        public static bool IsPrnFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return extension != null && extension.Equals(".prn", StringComparison.OrdinalIgnoreCase);
        }
        public static bool IsBtwFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return extension != null && extension.Equals(".btw", StringComparison.OrdinalIgnoreCase);
        }

    }
}
