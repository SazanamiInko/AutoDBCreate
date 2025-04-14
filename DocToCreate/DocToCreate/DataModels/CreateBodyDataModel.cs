namespace DocToCreate.DataModels
{
    /// <summary>
    /// Create文本体
    /// </summary>
    public class CreateBodyDataModel
    {
        #region プロパティ
        /// <summary>
        /// テーブル名
        /// </summary>
        public string TableName{get;set;}

        /// <summary>
        /// 列
        /// </summary>
        public List<ColumnDataModel> Columns { get; set; }

        /// <summary>
        /// 主キーのグループ
        /// </summary>
        public List<String> PrimaryKeys { get; }
        
        /// <summary>
        /// 外部キー
        /// </summary>
        public List<ForienKeyDataModel> ForienKeys { get; }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CreateBodyDataModel() 
        {
            TableName = string.Empty;
            Columns = new List<ColumnDataModel>();
            PrimaryKeys = new List<String>();
            ForienKeys = new List<ForienKeyDataModel>();
        }
    }
}
