﻿using LogSearcher.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static LogSearcher.Domain.Utils;

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

        public async Task SearchInList(List<string> list)
        {
            foreach (var file in list)
            {
                try
                {
                    using (var reader = File.OpenText(file))
                    {
                        var fileContent = await reader.ReadToEndAsync();

                        if (fileContent.Contains(searchProfile.SearchString))
                        {
                            // also locate the line and col of first occurence?                       
                            var hit = new HitFile(file);
                            hit.SearchPosition = await FindLine(fileContent);
                            HitList.Add(hit);
                        }
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }

        private async Task<TextPosition> FindLine(string file)
        {
            // in log-files, each line ends with [cr-lf]
            // split into array @ cr-lf
            // traverse this array until match is found
            // return index (offset+1 because arrayindex->linenumber)

            TextPosition position = new TextPosition(); ;
            var lines = file.Split('\n');
            
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(searchProfile.SearchString))
                {
                    position.Text = lines[i];
                    position.Line = i +1; // +1 -> convert from array-pos to line num
                    break;
                }
            }

            position.Column = position.Text.IndexOf(searchProfile.SearchString) +1; // +1 -> convert from array-pos to text column num

            return position;
        }
    }
}
