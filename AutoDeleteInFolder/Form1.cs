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
        private string path = "";
        private int maxFiles = 0;
        private double maxSize = 0.0;
        private int oldestAllowedFile = 0;
        private int currentAmoutOfFiles = 0;
        private double currentSizeOfFolder = 0.0;
        private DateTime fileDate;
        private string fileName = "";
        private string optionsFile = "";
        private string[] options;
        public delegate string watcher();
        Conditions con = new Conditions();
        public Form1()
        {
            InitializeComponent();
            options = new string[3];
        }
     

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                var pathWithName = systemPath + @"\AutoDeleteInFolder\";
                var optionFile = pathWithName + "options.txt";
                optionsFile = optionFile;
                if   (File.Exists(optionFile))
                {
                    options = File.ReadAllLines(optionsFile);
                    InitSettings();
                }
                else
                {
                    Directory.CreateDirectory(pathWithName);
                    File.WriteAllText(optionFile, ("0\r\n" + "0\r\n" + "0\r\n"));
                    
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
                txtMaxFiles.Text = Convert.ToString(0);
                txtMaxSize.Text = Convert.ToString(0);
                txtOldest.Text = Convert.ToString(0);
                 path = fBD.SelectedPath;
                 txtPath.Text = path;
                 UpdateAll();
                 Watch();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dia = MessageBox.Show("By updating the settings any files in the selected folder will be deleted if they meet the spesified critera. Are you sure you want to proceed?", "WARNING!",MessageBoxButtons.YesNo);
                if (dia == DialogResult.Yes)
                {
                    maxFiles = Convert.ToInt32(txtMaxFiles.Text);
                    maxSize = Convert.ToInt32(txtMaxSize.Text);
                    oldestAllowedFile = Convert.ToInt32(txtOldest.Text);
                    UpdateAll();
                    Watch();
                }
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
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static double CheckSizeOfFolder(string path)
        {
           long rawSize = 0;
           double size = 0.0;
           DirectoryInfo dI = new DirectoryInfo(path);

           rawSize =  dI.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi=> fi.Length);
           size = rawSize / 1024 / 1024 / 1024;
           return size;
        }
        private void CheckOldestFile()
        {
            var directory = new DirectoryInfo(path);
            var myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).Last();
            string temp = myFile.ToString();
            fileName = temp;
        }
        
        private DateTime CheckFileCreationDate ()
        {
            DateTime fileCreatedDate = File.GetLastWriteTime(path + "\\" + fileName);
            lblAge.Text = "File Created: "+ fileCreatedDate.ToString();
            return fileCreatedDate;
        }
        
        private void UpdateTextBoxes()
        {
            double temp = currentSizeOfFolder;
            txtCurFiles.Text = currentAmoutOfFiles.ToString();
            txtCurSize.Text = Math.Round((temp),2).ToString() + "GB";
            txtCurOld.Text = fileName;
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
            CheckNumOfFiles();
            currentSizeOfFolder = CheckSizeOfFolder(path);
            CheckOldestFile();
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
            tooOld = con.TooOld(oldestAllowedFile, CheckFileCreationDate());
            

            if (tooManyFiles == true || tooBig == true || tooOld == true)
            {
                DeleteFiles();
            }
        }
            private void DeleteFiles()
            {
                try
                {
                    DeleteFile del = new DeleteFile();
                    del.Delete(path, fileName);
                    UpdateAll();
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
               
            }
           private void UpdateAll()
            {
                CheckNumOfFiles();
                currentSizeOfFolder = CheckSizeOfFolder(path);
                CheckOldestFile();
                UpdateTextBoxes();
                CheckConditions();
            }
           private void ReadFromFile()
           {
               if (!File.Exists(optionsFile))
               {
                   options = File.ReadAllLines(optionsFile);
               }
           }
          private void WriteToFile()
           {
             
           }
        private void InitSettings()
          {
              maxFiles = Convert.ToInt32(options[0]);
              maxSize = Convert.ToInt32(options[1]);
              oldestAllowedFile = Convert.ToInt32(options[2]);
          }

        }
        
    }

