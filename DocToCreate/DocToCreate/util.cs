using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocToCreate
{
    public static class util
    {
        /// <summary>
        /// 行位置の取得
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string GetRowString(string address)
        {
            string result = Regex.Replace(address, @"\D", "");
            return result;
        }

        /// <summary>
        /// 列を取得
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string GetColumnString(string address) 
        {
             string result = Regex.Replace(address, @"\d", "");
             return result;
        }
    }
}
