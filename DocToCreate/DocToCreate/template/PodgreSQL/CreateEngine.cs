using DocToCreate.DataModels;
using DocumentFormat.OpenXml.Vml;
using System.Text;

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
            var new_line=line.Replace(ConstsList.ReplaceList.COLUMN, replace_token);
            return new_line;
        }
        
        /// <summary>
        /// 置き換え判定
        /// </summary>
        /// <param name="line">文字列</param>
        /// <param name="keyword">置き換えキーワード</param>
        /// <returns>置き換え文字があった場合はtrue</returns>
        private bool isHit(string line,string keyword)
        {
            return line.IndexOf(keyword) > -1;
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

                if (isHit(line, ConstsList.ReplaceList.TABLENAME))
                {

                    new_line = line.Replace(ConstsList.ReplaceList.TABLENAME, dataModel.TableName);
                    sb.AppendLine(new_line);
                }
                else if (isHit(line, ConstsList.ReplaceList.COLUMN))
                {
                    foreach (var column in dataModel.Columns)
                    {
                        if (!string.IsNullOrEmpty(column.EndMark))
                        {
                 
                            sb.AppendLine(CreateColumnString(line, "{0} {1} {2},", column));
                        }
                        else
                        {
                            sb.Append(CreateColumnString(line, "{0} {1} {2}", column));
                        }
                    }
                }
                else if (isHit(line, ConstsList.ReplaceList.PRIMARYKEY))
                {
                    var phrase = mapfile.Map[ConstsList.RuleList.PRIMARYKEY];
                    var keys = string.Join(",", dataModel.PrimaryKeys);
                    var primary_phrase = string.Format("{0}({1})", phrase, keys);
                    var primary_new_line = line.Replace(ConstsList.ReplaceList.PRIMARYKEY, primary_phrase);
                    sb.Append(",");
                    sb.AppendLine("");
                    sb.Append(primary_new_line);
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