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
            sourceDirectories = new BindableCollection<SourceDirectory>();
            hitList = new BindableCollection<HitFile>();
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


        #region view property-bindings 
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
        #endregion


        #region buttons
        public void SubmitSearchString()
        {
            // grab value of InputSearchString
            var test = InputSearchString;
        }

        public void SubmitExtension()
        {
            // grab value of InputExtension
            var test = InputExtension;
        }

        public void SubmitSourceFolder()
        {
            // grab value of InputSourceFolder -> SourceDirectories
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
            // GO GO
            SearchForFiles();
        }
        #endregion


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
     
    }
}
