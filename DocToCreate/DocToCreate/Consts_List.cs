namespace DocToCreate
{
    /// <summary>
    /// 定数リスト
    /// </summary>
    public static class ConstsList
    {
        /// <summary>
        /// 定義
        /// </summary>
        public static class DefineList
        {
            /// <summary>
            /// テーブル物理名位置
            /// </summary>
            public const string TABLE_NAME_POS = "L5";
            /// <summary>
            /// 列定義開始位置
            /// </summary>
            public const int ROW_START_POS = 7;
            /// <summary>
            /// 列名位置
            /// </summary>
            public const string COLUMN_NAME_POS = "U";
            /// <summary>
            /// 列型位置
            /// </summary>
            public const string COLUMN_TYPE_POS = "AJ";
            /// <summary>
            /// 主キー位置
            /// </summary>
            public const string COLUNN_PRIMARY_POS = "BA";
            /// <summary>
            /// NotNullキー
            /// </summary>
            public const string COLUMN_REQUIRE_POS = "BH";
            /// <summary>
            /// 自動キー位置 
            /// </summary>
            public const string COLUMN_SEQUENTIAL_POS = "BO";
            /// <summary>
            /// 外部キー位置
            /// </summary>
            public const string COLUMN_FORIEN_POS = "CC";

            /// <summary>
            /// 規定値
            /// </summary>
            public const string COLUMN_DEFAULT_POS = "CJ";

            /// <summary>
            /// 外部キー区切り文字
            /// </summary>
            public const string FORINE_DIVIDE = ".";

            /// <summary>
            /// 文字列長位置
            /// </summary>
            public const string COLUMN_LENGTH_POS = "AU";
        }

        public static class RuleList
        {
            /// <summary>
            /// 主キー
            /// </summary>
            public static string PRIMARYKEY = "主キー";
            /// <summary>
            /// 一意
            /// </summary>
            public static string UNIQUE = "一意";

            /// <summary>
            /// 必須
            /// </summary>
            public static string NOTNULL = "必須";

            /// <summary>
            /// 外部
            /// </summary>
            public static string FORIEN = "外部";

            /// <summary>
            /// 既定値
            /// </summary>
            public static string DEFAULT = "既定値";

            /// <summary>
            /// 自動
            /// </summary>
            public static string AUTO = "自動";
            /// <summary>
            /// 配列開始
            /// </summary>
            public static string START_ARRAY = "配列開始";

            /// <summary>
            /// 配列終了
            /// </summary>
            public static string END_ARRAY = "配列終了";
        }
    }
}
