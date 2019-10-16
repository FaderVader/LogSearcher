using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using LogSearcher.Domain;
using LogSearcher.Models;

namespace LogSearcher.ViewModels
{
    class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {
            SourceDirectories = new BindableCollection<SourceDirectory>();
            HitList = new BindableCollection<HitFile>();

            InputExtension = "";
            InputSearchString = "";
        }


        #region Main lists

        private BindableCollection<SourceDirectory> sourceDirectories;
        private TargetDirectory targetDirectory;
        private BindableCollection<HitFile> hitList;
        public BindableCollection<SourceDirectory> SourceDirectories
        {
            get { return sourceDirectories; }
            set
            {
                sourceDirectories = value;
                NotifyOfPropertyChange(() => SourceDirectories);
            }
        }

        public TargetDirectory TargetDirectory
        {
            get { return targetDirectory; }
            set
            {
                targetDirectory = value;
                NotifyOfPropertyChange(() => TargetDirectory);
            }
        }

        public BindableCollection<HitFile> HitList
        {
            get { return hitList; }
            set { hitList = value; NotifyOfPropertyChange(() => HitList); }
        }
        #endregion


        #region View-property bindings 
        private string inputSearchString;
        public string InputSearchString
        {
            get { return inputSearchString; }
            set
            {
                inputSearchString = value;
                NotifyOfPropertyChange(() => InputSearchString);
            }
        }


        private string inputExtension;
        public string InputExtension
        {
            get { return inputExtension; }
            set
            {
                inputExtension = value;
                NotifyOfPropertyChange(() => InputExtension);
            }
        }

        private string inputSourceFolder;
        public string InputSourceFolder
        {
            get { return inputSourceFolder; }
            set { inputSourceFolder = value; NotifyOfPropertyChange(() => InputSourceFolder); }
        }

        private string inputTargetFolder;
        public string InputTargetFolder
        {
            get { return inputTargetFolder; }
            set { inputTargetFolder = value; NotifyOfPropertyChange(() => InputTargetFolder); }
        }

        private HitFile selectedFile;
        public HitFile SelectedFile
        {
            get { return selectedFile; }
            set { selectedFile = value; NotifyOfPropertyChange(() => SelectedFile); }
        }

        #endregion


        #region Buttons

        public void FolderBrowse()
        {
            var fileHandler = new FileHandler();
            var folder = fileHandler.BrowseForFolder();

            if (Utils.ValidateDirectory(folder))
            {
                SourceDirectory sourceDir = new SourceDirectory(folder);
                SourceDirectories.Add(sourceDir);
            }

        }


        public void SubmitSourceFolder()
        {           
            if (Utils.ValidateDirectory(InputSourceFolder))
            {
                SourceDirectory sourceDir = new SourceDirectory(InputSourceFolder);
                sourceDirectories.Add(sourceDir);

                InputSourceFolder = "";
            }
        }

        public void ResetSourceFolderDisplay()
        {
            // reset value of SourceFolderDisplay
            SourceDirectories = new BindableCollection<SourceDirectory>();
        }

        public void SubmitTargetFolder()
        {
            // grab value of TargetDirectory
            var test = InputTargetFolder;
        }

        public void StartSearch()
        {
            HitList.Clear();
            SearchForFiles();
        }

        public void OpenFile()
        {
            var fileHandler = new FileHandler();
            fileHandler.OpenFile(SelectedFile);
        }
        #endregion


        #region Methods
        public void SearchForFiles()
        {
            SearchProfile profile = new SearchProfile(InputSearchString, InputExtension);
            FileGatherer gatherer = new FileGatherer(SourceDirectories, profile);
            var result = gatherer.GetFoundFiles();

            BindableCollection<HitFile> localHits = new BindableCollection<HitFile>();
            foreach (var file in result)
            {
                localHits.Add(file);
            }

            HitList = localHits;
        }
        #endregion

    }
}
