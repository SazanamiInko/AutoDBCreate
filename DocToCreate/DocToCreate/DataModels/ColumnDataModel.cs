namespace DocToCreate.DataModels
{
    /// <summary>
    /// 列データモデル
    /// </summary>
    public class ColumnDataModel
    {
        #region プロパティ

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; } = string.Empty;

        /// <summary>
        /// 列タイプ
        /// </summary>
        public string ColumnType { get; set; } = string.Empty;
        /// <summary>
        ///　制約
        /// </summary>
        public List<string> ColumnRule { get; } = new List<string>();

        /// <summary>
        /// 外部テーブル
        /// </summary>
        public string ForienTable { get; set; } = string.Empty;

       /// <summary>
       /// 制約文字
       /// </summary>
        public string RuleString { get; set; } = string.Empty;

        public string EndMark { get; set; } = string.Empty;
        #endregion
    }
}
