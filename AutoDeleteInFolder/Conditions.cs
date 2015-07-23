﻿using System;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TooMany(int limit, int value)
         {
             if (limit == 0)
             {
                 limit = 99999999; //Make sure that 0 means unlimited
             }
             if (limit < value)
             {
                 return true;
             }
             else
             {
                 return false;
             }
         }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TooMuch(int limit, int value)
        {
            if (limit == 0)
            {
                limit = 99999999;
            }
            if (limit < value)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }
        
        public bool TooOld(int userInput, DateTime value)
        {
            if (userInput == 0)
            {
                userInput = 99999999;   
            }
            DateTime limit;
            limit = DateTime.Now.AddDays(-userInput);
           
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
