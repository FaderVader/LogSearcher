using LogSearcher.Models;
using System;
using System.Diagnostics;
using System.IO;
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

        public void SendToNotePadPP(HitFile hitfile)
        {
            if (hitfile == null) return;

            try
            {
                var notePadPath = Properties.Settings.Default.NotePadPP_Path;
                var notePadExe = Properties.Settings.Default.NotePadPP_Exe;
                string npp = $"{notePadPath}\\{notePadExe}";
                string args = $"-n{hitfile?.SearchPosition.Line} -c{hitfile?.SearchPosition.Column} {hitfile?.FilePathAndName}";

                Process.Start(npp, args);
            }
            catch (Exception)
            {
                // do nothing - just ignore and return
                return;
            }

        }
    }
}
