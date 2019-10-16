using LogSearcher.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSearcher.Domain
{
    public class FileGatherer  // Traverse files in specified location, get any of matching type
    {
        
        public FileGatherer(IList<SourceDirectory> sourceDirectories, SearchProfile search)
        {
            this.sourceDirectories = sourceDirectories;
            this.searchProfile = search;

            TraverseSourceDirs();

            var test = sourceDirectories;
        }


        private IList<SourceDirectory> sourceDirectories;
        public IList<SourceDirectory> SourceDirectories
        {
            get { return sourceDirectories; }
            private set { sourceDirectories = value; }
        }

        private SearchProfile searchProfile;
        public SearchProfile SearchProfile
        {
            get { return searchProfile; }
            set { searchProfile = value; }
        }

        
        public void TraverseSourceDirs()
        {
            foreach (var directory in SourceDirectories)
            {
                if (!Directory.Exists(directory.DirectoryName)) { continue; }

                var pattern = $"*{SearchProfile.FileExt}";                
                var list = Directory.GetFiles(directory.DirectoryName, pattern).ToList();

                directory.FoundFileList.Clear(); // ensure list is cleared before populating

                FindInFile findFile = new FindInFile(searchProfile);
                findFile.SearchInList(list);          
                directory.FoundFileList = findFile.HitList;
            }
        }

        public List<HitFile> GetFoundFiles()
        {
            List<HitFile> foundFiles = new List<HitFile>();

            foreach (var dir in SourceDirectories)
            {
                dir.FoundFileList.ForEach(foundFile => foundFiles.Add(foundFile));
            }

            return foundFiles;
        }

    }
}
