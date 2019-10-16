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
            if (path == null) return false; 

            DirectoryInfo DirInfo = new DirectoryInfo(path);
            if (DirInfo.Exists == false) return false;
            return true;
        }

        public static DirectoryInfo GetDirInfo(this string path)
        {            
            DirectoryInfo DirInfo = new DirectoryInfo(path);
            if (DirInfo.Exists == false) return null; 
            return DirInfo;
        }

        public class TextPosition
        {
            public int Line;
            public int Column;
            public string Text = "";
        }
    }
}
