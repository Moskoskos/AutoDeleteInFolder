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

        public void Delete(string folder, string fileName)
        {
            string file = folder+"\\"+fileName;
            if (System.IO.File.Exists(file))
            {
                System.IO.File.Delete(file);
            }
        
        }
    }
}
