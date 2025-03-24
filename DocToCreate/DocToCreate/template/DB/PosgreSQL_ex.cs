using DocToCreate.DataModels;

namespace DocToCreate.template.DB
{
    /// <summary>
    /// PosgreSQL拡張
    /// </summary>
    partial class PosgreSQL
    {
        #region プロパティ
        /// <summary>
        /// データモデル
        /// </summary>
        public CreateBodyDataModel BodyDataModel { get; }

        /// <summary>
        /// テーブル名
        /// </summary>
        public String TableName { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        ///   コンストラクタ
        /// </summary>
        /// <param name="datamodel">データモデル</param>
        public PosgreSQL(CreateBodyDataModel datamodel)
        {
            BodyDataModel = datamodel;
        }


        #endregion
    }
}
