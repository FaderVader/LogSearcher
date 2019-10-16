﻿using LogSearcher.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogSearcher.Domain
{
    public class FileHandler  // Copy the specified files to the specified location
    {
        public void OpenFile(HitFile file)
        {
            if (!File.Exists(file?.FilePathAndName)) return;

            Process.Start(file.FilePathAndName);
        }

        public string BrowseForFolder()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            string selectedFolder = null;

            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var fileName = fileDialog.FileName;
                selectedFolder = new FileInfo(fileName)?.Directory.ToString();
            }
            return selectedFolder;
        }
    }
}