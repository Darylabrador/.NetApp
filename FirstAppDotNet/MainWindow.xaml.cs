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

			if (files!=null)
			{
				foreach (FileInfo fi in files)
				{
					FileClass vItem;
					vItem = new FileClass();
					vItem.Name = "Name";
					vItem.PicturePath = "Images/mp3.png";
					vItem.Path = fi.FullName;
					list.Add(vItem);
				}

				subDirs = root.GetDirectories();

				foreach (DirectoryInfo dirInfo in subDirs)
				{
					WalkDirectoryTree(dirInfo);
				}
			}
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

		}
}
