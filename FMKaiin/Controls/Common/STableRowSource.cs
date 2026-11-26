using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// <see cref="STableComboBox"/> が一覧に出す行を管理する。テーブルから「選択できる行」を取り出し、
    /// 値・表示する文字列での検索と、入力した文字による絞り込みを行う。
    /// </summary>
    /// <remarks>
    /// 行数の多いテーブル（数万行）でも入力のたびに待たされないよう、次のものを一度だけ作って使い回す。
    /// どれも、最初に必要になった時点で作り、テーブルの行や設定が変わったら捨てる。
    /// <list type="bullet">
    /// <item>選択できる行の一覧と、行から位置を引く表</item>
    /// <item>値から行を引く表、表示する文字列から行を引く表</item>
    /// <item>絞り込み用に表記をそろえた、各行・各列の文字列</item>
    /// </list>
    /// </remarks>
    internal sealed class STableRowSource : IDisposable
    {
        private readonly List<STableComboColumn> _columns = new List<STableComboColumn>();

        private DataTable _table;
        private DataView _view;
        private string _rowFilter = string.Empty;
        private string _sort = string.Empty;
        private string _displayMember = string.Empty;
        private string _valueMember = string.Empty;

        // ここから下は使い回すもの。null は「作り直しが必要」を表す
        private List<DataRow> _rows;
        private Dictionary<DataRow, int> _positions;
        private Dictionary<string, DataRow> _rowsByValue;
        private Dictionary<string, DataRow> _rowsByText;
        private List<STableComboColumn> _visibleColumns;
        private string[][] _searchCells;
        private bool[] _searchColumnIsText;

        /// <summary>
        /// テーブルの行、または設定（条件・並び順・列など）が変わったときに発生する。
        /// テーブルを一括で読み込むと 1 行ごとに発生するので、ハンドラーでは重い処理をしないこと。
        /// </summary>
        public event EventHandler Changed;

        #region 設定

        /// <summary>一覧のもとになるテーブル。</summary>
        public DataTable Table
        {
            get { return _table; }
            set
            {
                if (ReferenceEquals(_table, value))
                {
                    return;
                }

                DetachView();
                _table = value;
                if (value != null)
                {
                    // 専用のビューを作る。条件や並び順が、同じテーブルを使うほかの画面に影響しないようにするため
                    _view = new DataView(value);
                    _view.RowFilter = _rowFilter;
                    _view.Sort = _sort;
                    _view.ListChanged += OnViewListChanged;
                }
                Invalidate();
            }
        }

        /// <summary>一覧に出す行の条件（<see cref="DataView.RowFilter"/> と同じ書き方）。</summary>
        public string RowFilter
        {
            get { return _rowFilter; }
            set
            {
                string filter = value ?? string.Empty;
                if (_view != null)
                {
                    // 式が正しくなければ、ここで例外になる（そのときは設定を変えない）
                    _view.RowFilter = filter;
                }
                _rowFilter = filter;
                Invalidate();
            }
        }

        /// <summary>一覧の並び順（<see cref="DataView.Sort"/> と同じ書き方）。</summary>
        public string Sort
        {
            get { return _sort; }
            set
            {
                string sort = value ?? string.Empty;
                if (_view != null)
                {
                    _view.Sort = sort;
                }
                _sort = sort;
                Invalidate();
            }
        }

        /// <summary>入力欄に表示する列の名前。空文字列の場合は、一覧の最初の列。</summary>
        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value ?? string.Empty;
                Invalidate();
            }
        }

        /// <summary>値として使う列の名前。</summary>
        public string ValueMember
        {
            get { return _valueMember; }
            set
            {
                _valueMember = value ?? string.Empty;
                Invalidate();
            }
        }

        /// <summary>一覧に出す列（指定されたもの）。変更は <see cref="AddColumn"/> と <see cref="ClearColumns"/> で行う。</summary>
        public List<STableComboColumn> Columns
        {
            get { return _columns; }
        }

        public void AddColumn(STableComboColumn column)
        {
            _columns.Add(column);
            Invalidate();
        }

        public void ClearColumns()
        {
            _columns.Clear();
            Invalidate();
        }

        public void Dispose()
        {
            DetachView();
        }

        private void DetachView()
        {
            if (_view != null)
            {
                _view.ListChanged -= OnViewListChanged;
                _view.Dispose();
                _view = null;
            }
        }

        private void OnViewListChanged(object sender, ListChangedEventArgs e)
        {
            Invalidate();
        }

        /// <summary>使い回しているものをすべて捨てて、変更を知らせる。</summary>
        private void Invalidate()
        {
            _rows = null;
            _positions = null;
            _rowsByValue = null;
            _rowsByText = null;
            _visibleColumns = null;
            _searchCells = null;
            _searchColumnIsText = null;

            EventHandler handler = Changed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion

        #region 行と列

        /// <summary>
        /// 選択できる行。条件（<see cref="RowFilter"/>）を通り、表示する文字列が空でない行を、並び順のとおりに並べたもの。
        /// </summary>
        public IList<DataRow> Rows
        {
            get
            {
                EnsureRows();
                return _rows;
            }
        }

        /// <summary><paramref name="row"/> の、<see cref="Rows"/> の中での位置。選択できる行でなければ -1。</summary>
        public int IndexOf(DataRow row)
        {
            if (row == null)
            {
                return -1;
            }

            EnsureRows();
            int position;
            return _positions.TryGetValue(row, out position) ? position : -1;
        }

        /// <summary>行がテーブルに残っているか（削除されたり、別のテーブルのものになったりしていないか）。</summary>
        public bool IsLive(DataRow row)
        {
            return row != null
                && row.Table == _table
                && row.RowState != DataRowState.Detached
                && row.RowState != DataRowState.Deleted;
        }

        /// <summary>
        /// 一覧に実際に出す列。<see cref="Columns"/> のうちテーブルにある列。1 つも無ければ、入力欄に表示する列だけを出す。
        /// </summary>
        public IList<STableComboColumn> VisibleColumns
        {
            get
            {
                if (_visibleColumns == null)
                {
                    var columns = new List<STableComboColumn>();
                    if (_table != null)
                    {
                        foreach (STableComboColumn column in _columns)
                        {
                            if (_table.Columns.Contains(column.DataPropertyName))
                            {
                                columns.Add(column);
                            }
                        }

                        string display = DisplayColumnName;
                        if (columns.Count == 0 && display != null)
                        {
                            columns.Add(new STableComboColumn(display, string.Empty, 0, HorizontalAlignment.Left));
                        }
                    }
                    _visibleColumns = columns;
                }
                return _visibleColumns;
            }
        }

        /// <summary><paramref name="row"/> の <paramref name="columnName"/> 列の値を、表示用の文字列にして返す。値が無ければ空文字列。</summary>
        public string GetCellText(DataRow row, string columnName)
        {
            return ToText(row[columnName]);
        }

        /// <summary><paramref name="row"/> の、入力欄に表示する文字列。</summary>
        public string GetDisplayText(DataRow row)
        {
            string display = DisplayColumnName;
            return row == null || display == null ? string.Empty : ToText(row[display]);
        }

        /// <summary><paramref name="row"/> の、<see cref="ValueMember"/> の列の値。列の指定が無ければ null。</summary>
        public object GetValue(DataRow row)
        {
            DataColumn column = ValueColumn;
            return row == null || column == null ? null : row[column];
        }

        /// <summary>入力欄に表示する列の名前。テーブルに無ければ null。</summary>
        private string DisplayColumnName
        {
            get
            {
                if (_table == null)
                {
                    return null;
                }

                string name = _displayMember;
                if (name.Length == 0 && _columns.Count > 0)
                {
                    name = _columns[0].DataPropertyName;
                }
                if (name.Length == 0 && _table.Columns.Count > 0)
                {
                    name = _table.Columns[0].ColumnName;
                }
                return name.Length > 0 && _table.Columns.Contains(name) ? name : null;
            }
        }

        private DataColumn ValueColumn
        {
            get { return _table != null && _valueMember.Length > 0 ? _table.Columns[_valueMember] : null; }
        }

        private void EnsureRows()
        {
            if (_rows != null)
            {
                return;
            }

            var rows = new List<DataRow>();
            var positions = new Dictionary<DataRow, int>();
            if (_view != null)
            {
                string display = DisplayColumnName;
                DataColumn displayColumn = display == null ? null : _table.Columns[display];
                foreach (DataRowView rowView in _view)
                {
                    DataRow row = rowView.Row;
                    if (displayColumn == null || !IsBlank(row[displayColumn]))
                    {
                        positions[row] = rows.Count;
                        rows.Add(row);
                    }
                }
            }

            _rows = rows;
            _positions = positions;
        }

        private static bool IsBlank(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return true;
            }

            var text = value as string;
            return text != null && text.Length == 0;
        }

        private static string ToText(object value)
        {
            return value == null || value == DBNull.Value ? string.Empty : Convert.ToString(value);
        }

        #endregion

        #region 値・文字列での検索

        /// <summary>
        /// <see cref="ValueMember"/> の列の値が <paramref name="value"/> の行を返す。無ければ null。
        /// 型が違っていても、文字列にしたときに同じなら一致とみなす（<see cref="SComboBox.ValuesMatch"/> と同じ考え方）。
        /// 同じ値の行が複数あるときは、並び順で最初の行。
        /// </summary>
        public DataRow FindByValue(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }

            if (_rowsByValue == null)
            {
                var map = new Dictionary<string, DataRow>(StringComparer.Ordinal);
                DataColumn column = ValueColumn;
                if (column != null)
                {
                    foreach (DataRow row in Rows)
                    {
                        object rowValue = row[column];
                        if (rowValue == null || rowValue == DBNull.Value)
                        {
                            continue;
                        }

                        string key = ToKey(rowValue);
                        if (!map.ContainsKey(key))
                        {
                            map.Add(key, row);
                        }
                    }
                }
                _rowsByValue = map;
            }

            DataRow found;
            return _rowsByValue.TryGetValue(ToKey(value), out found) ? found : null;
        }

        /// <summary>
        /// 入力欄に表示する文字列が <paramref name="text"/> と一致する行を返す。無ければ null。
        /// 同じ文字列の行が複数あるときは、並び順で最初の行。
        /// </summary>
        public DataRow FindByText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            if (_rowsByText == null)
            {
                var map = new Dictionary<string, DataRow>(StringComparer.Ordinal);
                foreach (DataRow row in Rows)
                {
                    string key = GetDisplayText(row);
                    if (!map.ContainsKey(key))
                    {
                        map.Add(key, row);
                    }
                }
                _rowsByText = map;
            }

            DataRow found;
            return _rowsByText.TryGetValue(text, out found) ? found : null;
        }

        private static string ToKey(object value)
        {
            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        #endregion

        #region 入力した文字による絞り込み

        /// <summary>
        /// 選択できる行のうち、一覧に出している列のどれかが <paramref name="text"/> に当てはまる行を、並び順のとおりに返す。
        /// 文字列の列は、入力が 1 文字なら先頭の一致、2 文字以上なら途中の一致も含める。数値などの列は、常に先頭の一致。
        /// ひらがなとカタカナ、全角と半角、大文字と小文字は区別しない。
        /// </summary>
        public List<DataRow> Search(string text)
        {
            var matches = new List<DataRow>();
            string key = NormalizeForSearch(text);
            if (key.Length == 0)
            {
                return matches;
            }

            EnsureSearchIndex();
            bool prefixOnly = text.Length == 1;
            int columnCount = _searchColumnIsText.Length;

            for (int i = 0; i < _searchCells.Length; i++)
            {
                string[] cells = _searchCells[i];
                for (int c = 0; c < columnCount; c++)
                {
                    bool matched = _searchColumnIsText[c] && !prefixOnly
                        ? cells[c].IndexOf(key, StringComparison.Ordinal) >= 0
                        : cells[c].StartsWith(key, StringComparison.Ordinal);
                    if (matched)
                    {
                        matches.Add(_rows[i]);
                        break;
                    }
                }
            }
            return matches;
        }

        /// <summary>
        /// 各行・各列の、表記をそろえた文字列を作る。1 文字入力するたびに全行の文字列を変換しなくて済むよう、
        /// 一度作ったものを、テーブルが変わるまで使い回す。
        /// </summary>
        private void EnsureSearchIndex()
        {
            if (_searchCells != null)
            {
                return;
            }

            EnsureRows();
            IList<STableComboColumn> visible = VisibleColumns;
            var columns = new DataColumn[visible.Count];
            var isText = new bool[visible.Count];
            for (int c = 0; c < visible.Count; c++)
            {
                columns[c] = _table.Columns[visible[c].DataPropertyName];
                isText[c] = columns[c].DataType == typeof(string);
            }

            var cells = new string[_rows.Count][];
            for (int i = 0; i < cells.Length; i++)
            {
                DataRow row = _rows[i];
                var rowCells = new string[columns.Length];
                for (int c = 0; c < columns.Length; c++)
                {
                    rowCells[c] = NormalizeForSearch(ToText(row[columns[c]]));
                }
                cells[i] = rowCells;
            }

            _searchCells = cells;
            _searchColumnIsText = isText;
        }

        /// <summary>
        /// 検索で比べるために、文字列の表記をそろえる。
        /// Unicode の互換分解（全角の英数字 → 半角、半角カタカナ → 全角カタカナ、濁点付きの文字 → 文字 + 濁点）を行い、
        /// ひらがなをカタカナに、英字を大文字にする。これで、ひらがなとカタカナ、全角と半角、大文字と小文字の違いを無視して比べられる。
        /// </summary>
        internal static string NormalizeForSearch(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            string normalized;
            try
            {
                normalized = text.Normalize(NormalizationForm.FormKD);
            }
            catch (ArgumentException)
            {
                // 壊れた文字（対になっていないサロゲート）が含まれている。分解せずに比べる
                normalized = text;
            }
            normalized = normalized.ToUpperInvariant();

            // ひらがな（ぁ～ゖ）を、同じ並びのカタカナ（ァ～ヶ）にする
            StringBuilder builder = null;
            for (int i = 0; i < normalized.Length; i++)
            {
                char c = normalized[i];
                if (c >= 'ぁ' && c <= 'ゖ')
                {
                    if (builder == null)
                    {
                        builder = new StringBuilder(normalized);
                    }
                    builder[i] = (char)(c + 0x60);
                }
            }
            return builder == null ? normalized : builder.ToString();
        }

        #endregion
    }
}
