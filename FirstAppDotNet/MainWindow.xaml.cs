using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
namespace FirstAppDotNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<FileClass> dataList { get; set; }
        public List<FileClass> list = new List<FileClass>();
        public FileClass FileInfo = new FileClass();
        public bool isPlaying = false;
        WMPLib.WindowsMediaPlayer wmplayer = new WMPLib.WindowsMediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
            getFiles();
            LoadListItems();
        }
        private void LoadListItems()
        {
            dataList = new List<FileClass>();
            dataList = list;
            ListBoxFiles.ItemsSource = dataList;
            this.ListBoxFiles.DataContext = this;
        }

        private void getFiles()
        {
            string[] drives = Environment.GetLogicalDrives();

            foreach (string dr in drives)
            {
                DriveInfo di = new DriveInfo(dr);
                DirectoryInfo rootDir = di.RootDirectory;
                WalkDirectoryTree(rootDir);
            }
        }
        void WalkDirectoryTree(DirectoryInfo root)
        {
            DirectoryInfo[] subDirs;

            FileInfo[] files = getMp3(root);
            if (files != null)
            {
                foreach (FileInfo file in files)
                {
                    createFileClass(file);
                }

                subDirs = root.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    WalkDirectoryTree(dirInfo);
                }
            }
        }
        private void createFileClass(FileInfo file)
        {
            FileClass tempItem;
            tempItem = new FileClass();
            tempItem.Name = file.Name;
            tempItem.PicturePath = "Images/mp3.png";
            tempItem.Path = file.FullName;
            list.Add(tempItem);
        }
        FileInfo[] getMp3(DirectoryInfo root)
        {
            FileInfo[] files = null;
            try
            {
                files = root.GetFiles("*.mp3");
            }

            catch (UnauthorizedAccessException e)
            {

                Console.WriteLine(e.Message);
            }

            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            return files;
        }
        private void playMusic(object sender, EventArgs e)
        {
            wmplayer.controls.stop();
            wmplayer.URL = list[ListBoxFiles.SelectedIndex].Path;
            wmplayer.controls.play();
            isPlaying = true;
        }

        private void musicCommand(object sender, EventArgs e)
        {
            if(isPlaying == true)
            {
                wmplayer.controls.pause();
                isPlaying = true;
            }
            else
            {
                wmplayer.controls.play();
                isPlaying = false;
            }
        }
        private void stopCommand(object sender, EventArgs e)
        {
            wmplayer.controls.stop();
        }
    }
}
