using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSearcher.Models
{
    public class SearchProfile 
    {
        private string fileExt;

        public string FileExt
        {
            get { return fileExt; }
            set { fileExt = value; }
        }


        private string searchString;

        public string SearchString
        {
            get { return searchString; }
            set { searchString = value; }
        }

        public SearchProfile(string searchString, string ext)
        {
            this.SearchString = searchString;
            this.fileExt = ext;
        }



    }
}
