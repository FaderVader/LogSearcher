using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSearcher.Domain
{
    public static class Utils
    {
        public static bool ValidateDirectory(string path)
        {    
                DirectoryInfo DirInfo = new DirectoryInfo(path);
                if (DirInfo.Exists == false) return false; // log error
                return true;           
        }

        public static DirectoryInfo GetDirInfo(this string path)
        {
            DirectoryInfo DirInfo = new DirectoryInfo(path);
            if(DirInfo.Exists == false) return null ; // log error
            return DirInfo;
        }
    }
}
