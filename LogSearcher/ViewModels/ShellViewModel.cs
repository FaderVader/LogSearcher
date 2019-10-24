using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using LogSearcher.Domain;
using LogSearcher.Models;
using static LogSearcher.Domain.Utils;

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

            GoSearch = new RelayCommand(StartSearch);
        }

        /* 
         Toggle for open file with Notepad++ via UI-checkbox: Use NPP
         Create two classes : 1 for just open default, 1 for invoking npp with args
         button-click-handler invokes same method, but we decide which delegate to use based on UI-checkbox
             */


        public RelayCommand GoSearch { get; }

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
            set { selectedFile = value; NotifyOfPropertyChange(() => SelectedFile); FileContent = SelectedFile?.SearchPosition.Text; }
        }

        private string fileContent;
        public string FileContent
        {
            get { return fileContent; }
            set { fileContent = value; NotifyOfPropertyChange(() => FileContent); }
        }


        #endregion


        #region Buttons

        public void FolderBrowse()
        {
            var folder = FileHandler.BrowseForFolder();

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
            // Not currently implemented
            var test = InputTargetFolder;
        }

        public async void StartSearch()
        {
            HitList.Clear();
            await SearchForFiles();
        }

        public void OpenFile()
        {
            var useNPP = Properties.Settings.Default.UseNPP;
            if (useNPP)
            {
                FileHandler.SendToNotePadPP(SelectedFile);
                return;
            }
            FileHandler.OpenWithFile(SelectedFile);
        }
        #endregion


        #region Methods
        public async Task SearchForFiles()
        {
            SearchProfile profile = new SearchProfile(InputSearchString, InputExtension);
            FileGatherer gatherer = new FileGatherer(SourceDirectories, profile);
            await gatherer.TraverseSourceDirs();
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
