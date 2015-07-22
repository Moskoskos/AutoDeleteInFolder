using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Permissions;

namespace AutoDeleteInFolder
{
    public partial class Form1 : Form
    {
        public delegate string watcher();
        public Form1()
        {
            InitializeComponent();
        }
        string path = "";
        private string Path
        {

            get { return path; }
            set { this.path = value; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                var programFolderPath = systemPath + "\\AutoDeleteInFolder\\";
                if   (File.Exists(programFolderPath))
                {

                }
                else
                {
                    StreamWriter sWrite = new StreamWriter(programFolderPath + "\\options.txt"); 
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            

                      
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            if (fBD.ShowDialog() == DialogResult.OK)
            {
                 Path = fBD.SelectedPath;
                 txtPath.Text = Path;
                 UpdateTextBoxes(CheckNumOfFiles(Path), CheckSizeOfFolder(Path), CheckOldestFile(Path));
                Watch();
                 
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
               
                UpdateTextBoxes(CheckNumOfFiles(Path), CheckSizeOfFolder(Path), CheckOldestFile(Path));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);               
            }

            
        }

        private int CheckNumOfFiles(string sourcePath)
        {
            int count = 0;
            if (sourcePath != "")
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(sourcePath);
                count = dir.GetFiles().Length;
                
            }
            return count;
        }
        private static long CheckSizeOfFolder(string sourcePath)
        {
            DirectoryInfo dI = new DirectoryInfo(sourcePath);
           long rawSize =  dI.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi=> fi.Length);
           rawSize = (rawSize/1024/1024/1024);
            return rawSize;
        }
        private string CheckOldestFile(string sourcePath)
        {
            var directory = new DirectoryInfo(sourcePath);
            var myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).Last();
            string temp = myFile.ToString();
            return temp;
        }
        
        private void UpdateTextBoxes(int files, long size, string oldest)
        {
            txtCurFiles.Text = files.ToString();
            txtCurSize.Text = size.ToString() + "GB";
            txtCurOld.Text = oldest;
        }


        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Watch()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = this.path;

            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);

            watcher.EnableRaisingEvents = true;
        }
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            Invoke((MethodInvoker)delegate { UpdateTextBoxes(CheckNumOfFiles(Path), CheckSizeOfFolder(Path), CheckOldestFile(Path)); });
        }
    }
}
