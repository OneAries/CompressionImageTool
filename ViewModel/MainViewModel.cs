using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CompressionImageTool.Command;
using System.Collections.Generic;
using System.Threading;

namespace CompressionImageTool.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            bBuildEnable = true;
            SelectDestPathCommand = new RelayCommand(() => SelectDestPath());
            SelectSourcePathCommand = new RelayCommand(() => SelectSourcePath());
            CompressionCommand = new RelayCommand(() => Compression());
        }
        public RelayCommand SelectDestPathCommand { get; set; }
        public void SelectDestPath()
        {
           DestPath =FileControlAPI.OpenFloderToSelectPath();
            
        }
        public RelayCommand SelectSourcePathCommand { get; set; }
        public void SelectSourcePath()
        {
            SourcePath = FileControlAPI.OpenFloderToSelectPath();
            List<string> TEMP = new List<string>();
            FileControlAPI.GetImagePathsInDirectory(SourcePath,ref TEMP);

            ImageSourcePaths = TEMP;
        }





        public RelayCommand CompressionCommand { get; set; }
        public void Compression()
        {
            List<string> Source = ImageSourcePaths;
            if (Source == null|| Source.Count==0)
                return;

            if (DestPath == null || DestPath == "")
                return;


            bBuildEnable = false;
            ThreadPool.QueueUserWorkItem(new WaitCallback(obj =>
            {
             
                for (int i=0; i< Source.Count;i++  )
                {
                    string sourcepath = Source[i];
                    string destpath = sourcepath.Replace(SourcePath, DestPath);
                    CompressionAPI.CompressImage(Source[i], destpath);
                    Schedule = (i + 1)*100 / Source.Count;
                }
                bBuildEnable = true;
            }));
           

        }
        private List<string> _ImageSourcePaths;

        public List<string> ImageSourcePaths
        {
            get { return _ImageSourcePaths; }
            set
            {
                _ImageSourcePaths = value;
                RaisePropertyChanged(() => ImageSourcePaths);
            }
        }

        private bool _bBuildEnable;

        public bool bBuildEnable
        {
            get { return _bBuildEnable; }
            set
            {
                _bBuildEnable = value;
                RaisePropertyChanged(() => bBuildEnable);
            }
        }

        private string _SourcePath;

        public string SourcePath
        {
            get { return _SourcePath; }
            set
            {
                _SourcePath = value;
                RaisePropertyChanged(() => SourcePath);
            }
        }

        private string _DestPath;

        public string DestPath
        {
            get { return _DestPath; }
            set
            {
                _DestPath = value;
                RaisePropertyChanged(() => DestPath);
            }
        }



        private int _Schedule;

        public int Schedule
        {
            get { return _Schedule; }
            set
            {
                _Schedule = value;
                RaisePropertyChanged(() => Schedule);
            }
        }

       


    }
}