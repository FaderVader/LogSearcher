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




        // for each sourceDir
        // validate path - check if folder exists (Directory.Exists() )
        // if OK, get all files with matching ext (Path.GetFileName() )
        // for each file, search for searchstring  (FileInfo .ReadAllText() )
        // if searchString is matched, add file to list of HitFiles

        public void TraverseSourceDirs()
        {
            foreach (var directory in SourceDirectories)
            {
                if (!Directory.Exists(directory.DirectoryName)) { continue; }

                var pattern = $"*{SearchProfile.FileExt}";                
                var list = Directory.GetFiles(directory.DirectoryName, pattern).ToList(); 

                foreach (var file in list)
                {
                    try
                    {
                        var fileContent = File.ReadAllText(file);

                        if (fileContent.Contains(searchProfile.SearchString))
                        {
                            directory.FoundFileList.Add(file);
                        }
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }

            }
        }

        public List<HitFile> GetFoundFiles()
        {
            List<HitFile> foundFiles = new List<HitFile>();

            foreach (var dir in SourceDirectories)
            {
                dir.FoundFileList.ForEach(foundFile => foundFiles.Add(new HitFile(foundFile)));
            }

            return foundFiles;
        }

    }
}
