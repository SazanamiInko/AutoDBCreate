using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToCreate.DataModels
{
    /// <summary>
    /// 外部キーデータモデル
    /// </summary>
    public class ForienKeyDataModel
    {
        #region プロパティ

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; } = String.Empty;

        /// <summary>
        /// 参照テーブル
        /// </summary>
        public string RefTable { get; set; } = string.Empty;

        /// <summary>
        /// 参照列
        /// </summary>
        public string RefColumn { get; set; } = string.Empty;

        /// <summary>
        /// 外部キーフレーズ
        /// </summary>
        public string ForienPhrase { get; set; } = string.Empty;
        #endregion


        #region コンストラクタ

        /// <summary>
        /// 使わないコンストラクタ
        /// </summary>
        private ForienKeyDataModel() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="columnname">列名</param>
        /// <param name="reftable">参照テーブル</param>
        public ForienKeyDataModel(string columnname, string reftable)
       {
            ColumnName = columnname;
            RefTable = reftable;
       }
        #endregion
    }
}
