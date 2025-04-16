using DocToCreate.DataModels;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocToCreate
{
    /// <summary>
    /// テーブル定義書分析
    /// </summary>
    public class DefineAnalyzer
    {
        #region　プロパティ

        /// <summary>
        /// パス
        /// </summary>
        public String Path { get; set; } = string.Empty;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DefineAnalyzer() { }

        /// <summary>
        /// 定義書からデータモデルを作成
        /// </summary>
        /// <returns></returns>
        public CreateBodyDataModel CreateCreateSQL()
        {
            CreateBodyDataModel dataModel = new CreateBodyDataModel();
            //EXCELを開く
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(this.Path, false))
            {
                var workbookPart = document.WorkbookPart;
                if (workbookPart == null) return dataModel;

                var sheet = workbookPart.Workbook.Sheets?.Elements<Sheet>().FirstOrDefault();

                // シートデータの取得
                var worksheetPart = workbookPart.WorksheetParts?.First(); // 先頭のシートデータ
                var sheetData = worksheetPart?.Worksheet.Elements<SheetData>().FirstOrDefault();
                var sharedStringTable = workbookPart.SharedStringTablePart?.SharedStringTable;
                if (sheetData == null) return dataModel;


                foreach (var row in sheetData.Elements<Row>())
                {
                    ColumnDataModel columnDataModel = new ColumnDataModel();
                    foreach (var cell in row.Elements<Cell>())
                    {
                        var pos = cell.CellReference?.Value;

                        if (pos == null) continue;

                        var row_pos = Int32.Parse(util.GetRowString(pos));
                        var column_pos_string = util.GetColumnString(pos);
                        var text = "";

                        //EXCELから値を取得
                        if (cell != null)
                        {
                            text = cell.CellValue?.Text;
                            if (!string.IsNullOrEmpty(text))
                            {

                                if (cell.DataType?.Value == CellValues.SharedString)
                                {
                                    var child = sharedStringTable?.ElementAt(int.Parse(text)).FirstChild;

                                    if (child != null)
                                    {
                                        text = child.InnerText;
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(text))
                        {

                            //場合分け
                            if (pos == ConstsList.DefineList.TABLE_NAME_POS)
                            {
                                if (!string.IsNullOrEmpty(text))
                                {
                                    dataModel.TableName = text;
                                }
                            }
                            else if (row_pos > ConstsList.DefineList.ROW_START_POS)
                            {
                                switch (column_pos_string)
                                {
                                    //列名
                                    case ConstsList.DefineList.COLUMN_NAME_POS:
                                        columnDataModel.ColumnName = text;
                                        break;
                                    //列型
                                    case ConstsList.DefineList.COLUMN_TYPE_POS:
                                        columnDataModel.ColumnType = mapfile.Map[text];
                                        break;
                                    //長さ
                                    case ConstsList.DefineList.COLUMN_LENGTH_POS:
                                        if (text != "0")
                                        {
                                            var start = mapfile.Map[ConstsList.RuleList.START_ARRAY];
                                            var end = mapfile.Map[ConstsList.RuleList.END_ARRAY];
                                            columnDataModel.ColumnType = String.Format("{0}{1}{2}{3}"
                                                , columnDataModel.ColumnType
                                                , start
                                                , text
                                                , end);

                                        }

                                        break;
                                    //主キー
                                    case ConstsList.DefineList.COLUNN_PRIMARY_POS:
                                        dataModel.PrimaryKeys.Add(columnDataModel.ColumnName);
                                        break;
                                　      
                                    //必須キー
                                    case ConstsList.DefineList.COLUMN_REQUIRE_POS:
                                        columnDataModel.ColumnRule.Add(mapfile.Map[ConstsList.RuleList.NOTNULL]);
                                        break;
                                    //自動キー
                                    case ConstsList.DefineList.COLUMN_SEQUENTIAL_POS:
                                        columnDataModel.ColumnRule.Add(mapfile.Map[ConstsList.RuleList.AUTO]);
                                        break;
                                    //外部キー
                                    case ConstsList.DefineList.COLUMN_FORIEN_TABLE_POS:
                                        columnDataModel.ForienTable = text;
                                        ForienKeyDataModel forien = new ForienKeyDataModel(columnDataModel.ColumnName, columnDataModel.ForienTable); 
                                        dataModel.ForienKeys.Add(forien);
                                        break;
                                    case ConstsList.DefineList.COLUMN_FORIEN_COLUMN_POS:
                                      if(!string.IsNullOrEmpty(columnDataModel.ForienTable))
                                       {
                                            var target = dataModel.ForienKeys.Where(record => record.ColumnName == columnDataModel.ColumnName)
                                                .First();
                                            target.RefColumn = text;
                                            var forien_rule = mapfile.Map[ConstsList.RuleList.FORIEN];
                                            var refarence_rule = mapfile.Map[ConstsList.RuleList.REFERENCE];
                                            var forien_phrase = $"{forien_rule} ({target.ColumnName}) {refarence_rule} {target.RefTable}({target.RefColumn})";
                                            target.ForienPhrase = forien_phrase;
                                        }
                                        break;
                                    //既定値
                                    case ConstsList.DefineList.COLUMN_DEFAULT_POS:
                                        var default_rule = mapfile.Map[ConstsList.RuleList.DEFAULT];
                                        var default_phrase = $"{default_rule}{text}";
                                        columnDataModel.ColumnRule.Add(default_phrase);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(columnDataModel.ColumnName))
                    {
                        columnDataModel.RuleString = string.Join(" ", columnDataModel.ColumnRule);
                        if (dataModel.Columns.Count > 0)
                        {

                            var last = dataModel.Columns.Last();
                            if (last != null
                                || dataModel.PrimaryKeys.Count > 0) //主キー指定に続く
                            {
                                last.EndMark = ",";
                            }
                        }
                        dataModel.Columns.Add(columnDataModel);
                    }
                }
            }

            //主キーの作成
            var phrase = mapfile.Map[ConstsList.RuleList.PRIMARYKEY];
            var keys = string.Join(",", dataModel.PrimaryKeys);
            dataModel.PrimalyKeyPhrase = string.Format("{0}({1})", phrase, keys);
            
            return dataModel;
        }
    }
}
