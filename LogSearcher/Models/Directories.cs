using System;
using System.Collections.Generic;
using System.Text;

namespace LogSearcher.Models
{
    public class SourceDirectory: LogDirectory
    {
        private List<string> foundFileList;

        public List<string> FoundFileList
        {
            get { return foundFileList; }
            set { foundFileList = value; }
        }

        public SourceDirectory(string path) : base(path)
        {
            foundFileList = new List<string>();
        }

    }

    public class TargetDirectory : LogDirectory
    {
        private string targetDir;

        public string TargetDir
        {
            get { return targetDir; }
            set { targetDir = value; }
        }

        public TargetDirectory(string path) : base(path)
        {

        }

    }
}
