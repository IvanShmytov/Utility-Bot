using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility_Bot.Services
{
    public static class DigitExtractor
    {
        public static int Extract(string str) 
        {
            string temp = null;
            int result = 0;
            foreach (var item in str)
            {
                if (char.IsDigit(item))
                {
                    temp += item;
                }
                else 
                {
                    result += Convert.ToInt32(temp);
                    temp = null;
                }
            }
            result += Convert.ToInt32(temp);
            return result;
        }
    }
}
