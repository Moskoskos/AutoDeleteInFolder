using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDeleteInFolder
{
    class Conditions
    {
        public Conditions()
        {

        }

        public bool TooMany(int limit, int value)
         {
             if (limit < value)
             {
                 return true;
             }
             else
             {
                 return false;
             }
         }
        public bool TooMuch(int limit, int value)
        {
            if (limit < value)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }

        public bool TooOld(DateTime limit, DateTime value)
        {
            if (limit < value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
