using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Security.Permissions;


namespace AutoDeleteInFolder
{
    public partial class Main : Form
    {
        private string folderPath = ""; 
        private int maxFiles = 0; //Maximum number of files.
        private int maxSize = 0; //Maximum size of the folder.
        private int oldestAllowedFile = 0; //How long a file should remain in the folder.
        private int currentAmoutOfFiles = 0; //Number of files being monitored.
        private double currentSizeOfFolder = 0.0; //Size of folder which is monitored.
        private string fileName = ""; //Name of the selected file.
        private string optionsTxtFile = ""; //Name of the options.txt file.
        private string[] optionsArray;
        public delegate string watcher();
        Conditions con = new Conditions();
        public Main()
        {
            InitializeComponent();
            optionsArray = new string[5] {"","","","",""};
        }
     

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //Finds the default system folder for application data.
                var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                //States the specifed application folder for the application.
                var pathWithName = systemPath + @"\AutoDeleteInFolder\";
                //States the specified application options file.
                var optionFile = pathWithName + "options.txt";
                //In-between-storage of content for optionFile
                optionsTxtFile = optionFile;
                //Check if options file is created.
                if   (File.Exists(optionFile))
                {
                    //Reads settings from file.
                    ReadFromFile();
                    //Checks whether options have file has been initialized, if so, the program gets the path of the monitored folder.
                    if (optionsArray[4] == "1")
                    {
                        txtPath.Text = optionsArray[3];
                        folderPath = optionsArray[3];

                    }
                    InitSettings();
                    UpdateTextBoxes();
                    UpdateAll();
                    Watch();
                }
                else
                {
                    //if the options file doesnt exists, create file and create default settings.
                    Directory.CreateDirectory(pathWithName);
                    File.WriteAllText(optionFile, ("0\r\n" + "0\r\n" + "0\r\n" + "0\r\n" + "0\r\n"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //Opens a dialog box to choose the folder to monitor.
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            if (fBD.ShowDialog() == DialogResult.OK)
            {
                folderPath = fBD.SelectedPath;
                txtPath.Text = folderPath;
                WriteToFile();
                UpdateAll();
                Watch();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dia = MessageBox.Show("By updating the settings any files in the selected folder will be deleted if they do not meet the spesified critera. Are you sure you want to proceed?", "WARNING!",MessageBoxButtons.YesNo);
                if (dia == DialogResult.Yes)
                {
                    maxFiles = Convert.ToInt32(txtMaxFiles.Text);
                    maxSize = Convert.ToInt32(txtMaxSize.Text);
                    oldestAllowedFile = Convert.ToInt32(txtOldest.Text);
                    WriteToFile();
                    UpdateAll();
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
            if (folderPath != "")
            {
                DirectoryInfo dir = new DirectoryInfo(folderPath);
                count = dir.GetFiles().Length;
            }
            currentAmoutOfFiles = count;
            //After the number of files has been acquired, do a check to determine if there is zero files or more.
            Invoke((MethodInvoker)delegate { NumOfFilesStatus(); });
            
            

        }

        private void NumOfFilesStatus()
        {
            if (currentAmoutOfFiles == 0)
            {
                txtColor.Visible = true;
                txtColor.BackColor = Color.Red;
                lblNoFiles.Visible = true;
            }
            else
            {
                txtColor.Visible = false;
                lblNoFiles.Visible = false;
            }
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static double CheckSizeOfFolder(string path)
        {
           long rawSize = 0;
           double size = 0.0;
            //Creates object to read info from desierd path.
           DirectoryInfo dI = new DirectoryInfo(path);

            //Gets the raw size of the folder in bytes. To be honest I dont fully understand this code. Thanks StackOverFlow!
           rawSize =  dI.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi=> fi.Length);
            //We are not interested in kilos or megas. Only gigas count.
           size = rawSize / 1024 / 1024 / 1024;
           return size;
        }
        private void CheckOldestFile()
        {
            //Create object to read info from desierd direction
            
            try
            {
                var directory = new DirectoryInfo(folderPath);
                var myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).Last();
                string temp = myFile.ToString();
                fileName = temp;
                txtColor.Visible = false;
                lblNoFiles.Visible = false;
            }
            catch (Exception ex)
            {
                

            }
        }
        
        private DateTime CheckFileCreationDate()
        {
            DateTime fileCreatedDate = File.GetLastWriteTime(folderPath + "\\" + fileName);
            lblAge.Text = "File Created: "+ fileCreatedDate.ToString();
            return fileCreatedDate;
        }
        
        private void UpdateTextBoxes()
        {
            double temp = currentSizeOfFolder;
            txtCurFiles.Text = currentAmoutOfFiles.ToString();
            txtCurSize.Text = Math.Round((temp),2).ToString() + "GB";
            txtCurOld.Text = fileName;
            txtMaxFiles.Text = Convert.ToString(maxFiles);
            txtMaxSize.Text = Convert.ToString(maxSize);
            txtOldest.Text = Convert.ToString(oldestAllowedFile);
        }




        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void Watch()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = this.folderPath;
            
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);

            watcher.EnableRaisingEvents = true;
        }
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            CheckNumOfFiles();
            currentSizeOfFolder = CheckSizeOfFolder(folderPath);
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
                    del.Delete(folderPath, fileName);
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
                currentSizeOfFolder = CheckSizeOfFolder(folderPath);
                CheckOldestFile();
                UpdateTextBoxes();
                CheckConditions();
            }
           private void ReadFromFile()
           {
               if (File.Exists(optionsTxtFile))
               {
                   optionsArray = File.ReadAllLines(optionsTxtFile);
               }
           }
          private void WriteToFile()
           {
               optionsArray[0] = maxFiles.ToString();
               optionsArray[1] = maxSize.ToString();
               optionsArray[2] = oldestAllowedFile.ToString();
               optionsArray[3] = folderPath;
               optionsArray[4] = "1";
                  // File.WriteAllLines(optionsFile, options);
                   using (StreamWriter file = new StreamWriter(optionsTxtFile))
                   {
                       foreach (string line in optionsArray)
                       {
                               file.WriteLine(line);
                       }
                   }
           }
        private void InitSettings()
          {

              maxFiles = Convert.ToInt32(optionsArray[0]);
              maxSize = Convert.ToInt32(optionsArray[1]);
              oldestAllowedFile = Convert.ToInt32(optionsArray[2]);
           
          }
          

        
    

        }
        
    }

