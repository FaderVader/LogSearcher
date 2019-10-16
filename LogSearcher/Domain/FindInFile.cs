using LogSearcher.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSearcher.Domain
{
    public class FindInFile // Look for specified string in specified file, flag if found
    {
        private SearchProfile searchProfile;
        private List<HitFile> hitList;
        
        public List<HitFile> HitList
        {
            get { return hitList; }
            set { hitList = value; }
        }


        public FindInFile(SearchProfile profile)
        {
            this.searchProfile = profile;
            HitList = new List<HitFile>();
        }

        public void SearchInList(List<string> list)
        {
            foreach (var file in list)
            {
                try
                {
                    var fileContent = File.ReadAllText(file);

                    if (fileContent.Contains(searchProfile.SearchString))
                    {
                        // also locate the line and col of first occurence?                       
                        HitList.Add(new HitFile(file));
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }

        private void FindLine(string file)
        {
            // in log-files, each line ends with [cr-lf]
            // split into array @ cr-lf
            // traverse this array until match is found
            // return index (offset+1 because arrayindex->linenumber)
        }
    }
}
