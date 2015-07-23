using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDeleteInFolder
{
    class DeleteFile
    {
        public DeleteFile()
        {

        }

        public void Delete(string folder, string file)
        {
        string[] fileList = System.IO.Directory.GetFiles(rootFolderPath, filesToDelete);
        foreach(string file in fileList)
        {
            System.Diagnostics.Debug.WriteLine(file + "will be deleted");
        //  System.IO.File.Delete(file);
            }
        }
    }
}
