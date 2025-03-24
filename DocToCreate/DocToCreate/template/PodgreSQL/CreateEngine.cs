using DocToCreate.DataModels;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToCreate.template.PodgreSQL
{
    /// <summary>
    /// PosgreSQL用作成エンジン
    /// </summary>
    public class CreateEngine
    {
        /// <summary>
        /// データモデル
        /// </summary>
        public CreateBodyDataModel dataModel { get; }

        private string Name{ get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dataModel"></param>
        public CreateEngine(CreateBodyDataModel dataModel)
        {
            this.dataModel = dataModel;
            Name = "PosgreSQL";
        }

        /// <summary>
        /// 行の文字列作成
        /// </summary>
        /// <param name="line">取り替える行の内容</param>
        /// <param name="format">フォーマット</param>
        /// <param name="column">列データモデル</param>
        /// <returns></returns>
        private string CreateColumnString(string line,string format, ColumnDataModel column)
        {
            
            string replace_token = String.Format(format,
                column.ColumnName,
                column.ColumnType,
                column.RuleString);
            var new_line=line.Replace(ConstsList.ReplaceList.Column, replace_token);
            return new_line;
        }        

        /// <summary>
        /// 作成
        /// </summary>
        /// <returns></returns>
        public string Create () 
        { 
            StringBuilder sb = new StringBuilder();

            string path = ConstsList.Templates.DB_CREATE_TEMPLATE;
            path = string.Format(@"{0}\{1}.txt", path, Name);
            var temp_line=File.ReadAllLines(path);

            foreach (var line in temp_line)
            {
                string new_line = string.Empty;
                if (string.IsNullOrEmpty(line))
                {
                    sb.AppendLine(line);
                    continue;
                }

                if (line.IndexOf(ConstsList.ReplaceList.TableName) > -1)
                {
                    
                    new_line=line.Replace(ConstsList.ReplaceList.TableName, dataModel.TableName);
                    sb.AppendLine(new_line);
                }
                else if (line.IndexOf(ConstsList.ReplaceList.Column) > -1)
                {
                    foreach (var column in dataModel.Columns)
                    {
                        if (!string.IsNullOrEmpty(column.EndMark))
                        {
                            sb.AppendLine(CreateColumnString(line, "{0} {1} {2},", column));
                        }
                        else
                        {
                            sb.AppendLine(CreateColumnString(line, "{0} {1} {2}", column));
                        }
                    }
                }
                else
                {
                    sb.AppendLine(line);
                }
            }
           
            return sb.ToString();
        }
    }
}