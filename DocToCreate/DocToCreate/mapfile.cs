using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToCreate
{
    /// <summary>
    /// マップファイル
    /// </summary>
    public static class mapfile
    {
        //マッピング辞書
        public static Dictionary<string, string> Map=new Dictionary<string, string>();

        /// <summary>
        /// 初期化
        /// </summary>
        public static void Initialize()
        {
            var lines = File.ReadLines(@"C:\Users\Public\Documents\data\DocToCreate\map\map.map"); ;

            foreach (var line in lines)
            {
                string[] parts = line.Split(",");
                string key = parts[0];
                string value = parts[1];
                Map.Add(key, value);
            }
        }
    }
}
