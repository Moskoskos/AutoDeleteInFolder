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
       private int maxFiles = 0;
       private int maxSize = 0;
       private int oldestAllowedFile = 0;
       private int currentAmoutOfFiles = 0;
       private int currentSizeOfFolder = 0;
       private string oldestFile = "";
       private DateTime fileDate;

       private string fileName = "";
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
                 path = fBD.SelectedPath;
                 txtPath.Text = path;
                 UpdateTextBoxes();
                Watch();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            try
            {
                maxFiles = Convert.ToInt32(txtMaxFiles.Text);
                maxSize = Convert.ToInt32(txtMaxSize.Text);
                oldestAllowedFile = Convert.ToInt32(txtOldest.Text);
                CheckConditions();
                UpdateTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);               
            }
        }
        
        private void CheckNumOfFiles()
        {
            int count = 0;
            if (path != "")
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
                count = dir.GetFiles().Length;
            }
            currentAmoutOfFiles = count;
        }
        private static void CheckSizeOfFolder()
        {
           double rawSize = 0.0;
           DirectoryInfo dI = new DirectoryInfo(path);
           rawSize =  dI.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi=> fi.Length);
           rawSize = (rawSize/1024/1024/1024);
           currentAmoutOfFile = rawSize;
        }
        private void CheckOldestFile()
        {
            var directory = new DirectoryInfo(path);
            var myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).Last();
            string temp = myFile.ToString();
            fileName = temp;
        }
        
        private DateTime CheckFileCreationDate (string fileName)
        {
            DateTime fileCreatedDate = File.GetCreationTime(path + "\\" + fileName);
            lblAge.Text = fileCreatedDate.ToString();
            return fileCreatedDate;
        }
        
        private void UpdateTextBoxes()
        {
            txtCurFiles.Text = currentAmoutOfFiles.ToString();
            if (currentSizeOfFolder > 1000000000)
            {
                txtCurSize.Text = currentSizeOfFolder.ToString() + "GB";
            }
            else
            {
                txtCurSize.Text = currentSizeOfFolder.ToString() + "MB";
            }
            txtCurOld.Text = oldestFile;
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
            Invoke((MethodInvoker)delegate { UpdateTextBoxes(); });
            Invoke((MethodInvoker)delegate { CheckConditions(); });
        }
        private void CheckConditions()
        {
            bool tooManyFiles = false;
            bool tooBig = false;
            bool tooOld = false;
            
            tooManyFiles = con.TooMany(maxFiles,currentAmoutOfFiles);
            tooBig = con.TooMuch(maxSize, currentSizeOfFolder);
            tooOld = con.TooOld(oldestAllowedFile, fileDate);

            if (tooManyFiles == true || tooBig == true || tooOld == true)
            {
                
            }
        }
        
    }
}
