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
        Conditions con = new Conditions();
        public Form1()
        {
            InitializeComponent();
        }
       private string path = "";
       private string name = "";
       
        private string Path
        {

            get { return path; }
            set { this.path = value; }
        }
        private string FileName
        {
            get { return name; }
            set { this.name = value; }
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
        private static double CheckSizeOfFolder(string sourcePath)
        {
           double rawSize = 0.0;
           DirectoryInfo dI = new DirectoryInfo(sourcePath);
           rawSize =  dI.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi=> fi.Length);
           rawSize = (rawSize/1024/1024/1024);
           return rawSize;
        }
        private string CheckOldestFile(string sourcePath)
        {
            var directory = new DirectoryInfo(sourcePath);
            var myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).Last();
            string temp = myFile.ToString();
            FileName = temp;
            return temp;
           

            
        }
        
        private DateTime CheckFileCreationDate (string sourcePath, string fileName)
        {
            DateTime fileCreatedDate = File.GetCreationTime(sourcePath + "\\" + fileName);
            lblAge.Text = fileCreatedDate.ToString();
            return fileCreatedDate;
        }
        
        private void UpdateTextBoxes(int files, double size, string oldest)
        {
            txtCurFiles.Text = files.ToString();
            if (size >1000000000)
            {
                txtCurSize.Text = size.ToString() + "GB";
            }
            else
            {
                txtCurSize.Text = size.ToString() + "MB";
            }
            txtCurOld.Text = oldest;
        }


        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void Watch()
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
            Invoke((MethodInvoker)delegate { CheckConditions(); });
        }
        private void CheckConditions()
        {
            
            con.TooMany();
            con.TooMuch();
            con.TooOld();
        }
    }
}
