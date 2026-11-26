using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using ComponentDB;

namespace App.Controls
{
    /// <summary>
    /// グリッド（C1TrueDBGrid）に、列の定義とデータを結び付ける。comset の GGridDBCommon の代わり。
    /// 列を <see cref="Add"/> で登録し、<see cref="EndAdd"/> でデータ（DBView）を渡すと、グリッドに列を作って表示する。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 一覧の表示（このファイル）と、セルに入力欄を重ねて入力する機能（SGridDBInput.cs）がある。
    /// 列の種類は、文字列・数値・金額・日付・計算で作る列（SGridDBColumns.cs）。行ごとの見た目は <see cref="RowStyle"/> で指定する。
    /// </para>
    /// <para>
    /// Gc 系のコントロール・コードは参照しない（GGridDBCommon は、セルの中で入力する機能のために Gc 系を参照している）。
    /// グリッドそのものは、これまでどおり C1TrueDBGrid を使う。
    /// </para>
    /// </remarks>
    public partial class SGridDB
    {
        /// <summary>残りの幅をすべて使う列の、右端のすき間（ピクセル）。GGridDBCommon と同じ値。</summary>
        private const int RemainingWidthMargin = 4;

        /// <summary>セルと、セルに収まらない文字列を表示するツールチップの間のすき間。96 DPI 基準のピクセル。</summary>
        private const float CellTipGap = 2f;

        private readonly C1TrueDBGrid _grid;
        private readonly SToolTip _toolTip;
        private readonly List<SGridDBColumn> _columns = new List<SGridDBColumn>();

        private DBView _view;
        private bool _endAdd;

        // セルに収まらない文字列のツールチップを表示しているセル。表示していなければ -1
        private int _tipRow = -1;
        private int _tipColumn = -1;

        /// <param name="grid">列とデータを結び付けるグリッド。S 系の画面の既定の見た目（<see cref="SGridStyle"/>）にする。</param>
        /// <param name="form">グリッドを置いているフォーム。</param>
        public SGridDB(C1TrueDBGrid grid, Form form)
        {
            if (grid == null)
            {
                throw new ArgumentNullException(nameof(grid));
            }
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            _grid = grid;
            _toolTip = new SToolTip(form);
            EnableRightClickSelect = true;

            SGridStyle.Apply(grid);

            _grid.MouseDown += OnGridMouseDown;
            _grid.Disposed += OnGridDisposed;
        }

        #region プロパティ

        /// <summary>結び付けているグリッド。</summary>
        public C1TrueDBGrid Grid
        {
            get { return _grid; }
        }

        /// <summary>登録した列。登録した順。</summary>
        public ReadOnlyCollection<SGridDBColumn> Columns
        {
            get { return _columns.AsReadOnly(); }
        }

        /// <summary>マウスの右ボタンで押した行を、指している行にするか。既定は true。</summary>
        public bool EnableRightClickSelect { get; set; }

        /// <summary>
        /// 行ごとの見た目（文字の色など）を決める関数。<see cref="EndAdd"/> の前に設定する。GGridDBCommon.SetRowStyles に当たる。
        /// 指している行を選択の色で塗る設定（既定）のときは、その行には適用しない（選択の色の上で、文字が読めなくならないように）。
        /// </summary>
        public SGridDBRowStyle RowStyle { get; set; }

        #endregion

        #region 登録

        /// <summary>
        /// 列を登録する。グリッドには、登録した順に左から並ぶ。<see cref="EndAdd"/> の前に呼ぶこと。
        /// </summary>
        /// <returns>登録した列（続けてプロパティを設定できる）。</returns>
        public SGridDBColumn Add(SGridDBColumn column)
        {
            if (column == null)
            {
                throw new ArgumentNullException(nameof(column));
            }
            if (_endAdd)
            {
                throw new InvalidOperationException("EndAdd の後は、列を登録できません。");
            }

            _columns.Add(column);
            return column;
        }

        /// <summary>
        /// 列の登録を終えて、グリッドに列を作り、<paramref name="view"/> のデータを表示する。
        /// </summary>
        /// <remarks>
        /// グリッドの「指している行」と <paramref name="view"/> の現在の行を連動させるには、<paramref name="view"/> を
        /// フォームの BindingContext を渡して作っておくこと（GGridDBCommon と同じ）。
        /// </remarks>
        public void EndAdd(DBView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            if (_endAdd)
            {
                throw new InvalidOperationException("EndAdd は、1 回だけ呼んでください。");
            }

            _view = view;
            CreateColumns();
            ResizeColumns();

            // セルに重ねて入力するコントロールを持つ列があれば、その準備をする（SGridDBInput.cs）
            SetUpInput();

            // セルに収まらない文字列は、マウスを載せたときにツールチップで表示する（OnFetchCellTips）
            _grid.CellTips = CellTipEnum.Floating;
            _grid.CellTipsDelay = 300;

            _grid.UnboundColumnFetch += OnUnboundColumnFetch;
            if (RowStyle != null)
            {
                // 行ごとの見た目を尋ねるイベントを、発生させる
                _grid.FetchRowStyles = true;
                _grid.FetchRowStyle += OnFetchRowStyle;
            }
            _grid.FetchCellTips += OnFetchCellTips;
            _grid.Resize += OnGridResize;
            _grid.MouseLeave += OnGridMouseLeave;

            // 作った列を残したまま、データを結び付ける（3 つ目の引数を true にしないと、グリッドが列を作り直す）
            _grid.SetDataBinding(view.DataView, "", true);

            _endAdd = true;
        }

        /// <summary>
        /// 表示するデータを、別の <paramref name="view"/> に差し替える（条件を変えて読み直したときなど）。
        /// 列は作り直さない。<see cref="EndAdd"/> の後に呼ぶこと。入力中なら、入力を終えてから差し替える。
        /// </summary>
        public void SetView(DBView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            if (!_endAdd)
            {
                throw new InvalidOperationException("SetView は、EndAdd の後に呼んでください。");
            }

            EndInput();
            HideCellTip();
            _view = view;
            // 作った列を残したまま、データを結び付ける
            _grid.SetDataBinding(view.DataView, "", true);
        }

        /// <summary>
        /// 列の幅を、いまのグリッドの幅に合わせて計算し直す。グリッドの大きさが変わったときは、自動で行う。
        /// </summary>
        public void PerformResize()
        {
            if (_endAdd)
            {
                ResizeColumns();
            }
        }

        /// <summary>
        /// <paramref name="row"/> 番目の行を、指している行にして、その行がグリッドの上下の中央あたりに来るように表示する。
        /// 行の数より大きい番号は、最後の行にする。行が無ければ何もしない。AppGridCommon.SetRow に当たる。
        /// </summary>
        public void SetRow(int row)
        {
            if (_grid.RowCount <= 0)
            {
                return;
            }

            row = Math.Max(0, Math.Min(row, _grid.RowCount - 1));
            int first = Math.Max(0, row - _grid.VisibleRows / 2);
            _grid.Row = row;
            _grid.FirstRow = first;
        }

        /// <summary>登録した列を、グリッドに作る。</summary>
        private void CreateColumns()
        {
            C1DataColumnCollection dataColumns = _grid.Columns;
            Split split = _grid.Splits[0];

            _grid.Caption = string.Empty;

            // デザイナーで作った列があれば消して、作り直す（表示用の列は、これに合わせて増減する）
            dataColumns.Clear();

            split.MinWidth = _grid.Width;
            split.SplitSize = _grid.Width;

            for (int i = 0; i < _columns.Count; i++)
            {
                SGridDBColumn column = _columns[i];
                dataColumns.Add(new C1DataColumn());
                C1DataColumn dataColumn = dataColumns[i];
                C1DisplayColumn displayColumn = split.DisplayColumns[i];

                if (column.UnboundFetch != null)
                {
                    // 項目名を空にしておかないと、表示する文字列を尋ねるイベント（UnboundColumnFetch）が発生しない。
                    // 元の値は、OnUnboundColumnFetch で、データから項目名で読む
                    dataColumn.DataField = string.Empty;
                }
                else
                {
                    dataColumn.DataField = column.DataField;
                    if (!string.IsNullOrEmpty(column.NumberFormat))
                    {
                        dataColumn.NumberFormat = column.NumberFormat;
                    }
                }
                dataColumn.Caption = column.Caption;

                displayColumn.AllowSizing = false;
                SGridStyle.ApplyColumn(displayColumn);

                switch (column.CellDisp)
                {
                    case SGridDBCellDisp.Left:
                        displayColumn.Style.HorizontalAlignment = AlignHorzEnum.Near;
                        break;
                    case SGridDBCellDisp.Center:
                        displayColumn.Style.HorizontalAlignment = AlignHorzEnum.Center;
                        break;
                    case SGridDBCellDisp.Right:
                        displayColumn.Style.HorizontalAlignment = AlignHorzEnum.Far;
                        break;
                }

                displayColumn.Visible = column.Visible;
            }
        }

        /// <summary>
        /// 列の幅を設定する。決まりは <see cref="SGridDBColumn"/> の説明のとおり（GGridDBCommon と同じ計算）。
        /// </summary>
        private void ResizeColumns()
        {
            Split split = _grid.Splits[0];
            C1DisplayColumnCollection displayColumns = split.DisplayColumns;
            float scale = LayoutScale.Of(_grid);

            split.MinWidth = _grid.Width;
            split.SplitSize = _grid.Width;

            // 列に使える幅（縦スクロール バーを除く）
            int available = _grid.Width - SystemInformation.VerticalScrollBarWidth;
            if (_grid.RecordSelectors)
            {
                // 左端の、行を選ぶための列の分
                available -= SystemInformation.VerticalScrollBarWidth;
            }

            // ここまでの列の幅の合計（列の間の線の分として、1 列につき 1 を足す）
            int used = 0;
            for (int i = 0; i < _columns.Count && i < displayColumns.Count; i++)
            {
                SGridDBColumn column = _columns[i];
                int width;
                if (column.Width > 0)
                {
                    width = (int)Math.Round(column.Width * scale);
                    used += width + 1;
                }
                else if (column.WidthRatio > 0f)
                {
                    width = (int)(available * column.WidthRatio);
                    used += width + 1;
                }
                else
                {
                    // 残りの幅をすべて使う
                    width = available - used - RemainingWidthMargin;
                    used = 0;
                }
                displayColumns[i].Width = Math.Max(0, width);
            }
        }

        #endregion

        #region グリッドのイベント

        /// <summary>
        /// 表示する文字列をプログラムで作る列（<see cref="SGridDBColumn.UnboundFetch"/>）の、セルの文字列を返す。
        /// </summary>
        private void OnUnboundColumnFetch(object sender, UnboundColumnFetchEventArgs e)
        {
            if (e.Col < 0 || e.Col >= _columns.Count)
            {
                return;
            }

            SGridDBColumn column = _columns[e.Col];
            if (column.UnboundFetch == null)
            {
                e.Value = string.Empty;
                return;
            }

            column.UnboundValue = GetFieldValue(e.Row, column.DataField);
            e.Value = column.UnboundFetch(column, e.Row) ?? string.Empty;
        }

        /// <summary><paramref name="row"/> 番目の行の、項目の値。行が無い場合と、データにその項目が無い場合は null。</summary>
        private object GetFieldValue(int row, string field)
        {
            DataView dataView = _view.DataView;
            if (row < 0 || row >= dataView.Count || dataView.Table == null || !dataView.Table.Columns.Contains(field))
            {
                return null;
            }
            return dataView[row][field];
        }

        /// <summary>
        /// 行ごとの見た目（<see cref="RowStyle"/>）を、グリッドに伝える。指している行は、選択の色のままにする。
        /// </summary>
        private void OnFetchRowStyle(object sender, FetchRowStyleEventArgs e)
        {
            SGridDBRowStyle rowStyle = RowStyle;
            if (rowStyle == null || e.Row < 0)
            {
                return;
            }

            // 指している行を選択の色で塗る設定のときは、その行には適用しない（選択の色の上で、文字が読めなくならないように）
            bool highlightsRow = _grid.MarqueeStyle == MarqueeEnum.HighlightRow || _grid.MarqueeStyle == MarqueeEnum.HighlightRowRaiseCell;
            if (highlightsRow && e.Row == _grid.Row)
            {
                return;
            }
            rowStyle(e.Row, e.CellStyle);
        }

        /// <summary>
        /// マウスを載せたセルの文字列が、セルに収まっていなければ、全体をツールチップで表示する。
        /// グリッドに備わっているツールチップは使わず、S 系のツールチップ（<see cref="SToolTip"/>）で、セルのすぐ下に表示する。
        /// </summary>
        private void OnFetchCellTips(object sender, FetchCellTipsEventArgs e)
        {
            // グリッドに備わっているツールチップは、表示させない
            e.CellTip = string.Empty;

            bool sameCell = _toolTip.Active && e.Row == _tipRow && e.ColIndex == _tipColumn;
            if (sameCell)
            {
                // 同じセルのものを表示中
                return;
            }
            HideCellTip();

            if (e.Column == null || e.Row < 0 || _grid.Columns[e.ColIndex].DataType == typeof(bool))
            {
                return;
            }

            string text = e.Column.DataColumn.CellText(e.Row);
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            const TextFormatFlags Flags = TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine;
            int textWidth = TextRenderer.MeasureText(text, e.Column.Style.Font, Size.Empty, Flags).Width;
            if (textWidth < e.Column.Width)
            {
                return;
            }

            Rectangle cell = _grid.Splits[e.SplitIndex].GetCellBounds(e.Row, e.ColIndex);
            int gap = (int)Math.Round(CellTipGap * LayoutScale.Of(_grid));
            _toolTip.Tips(_grid, cell.Left, cell.Bottom + gap, text);
            _tipRow = e.Row;
            _tipColumn = e.ColIndex;
        }

        private void HideCellTip()
        {
            if (_tipRow >= 0)
            {
                _toolTip.Hide();
                _tipRow = -1;
                _tipColumn = -1;
            }
        }

        private void OnGridMouseLeave(object sender, EventArgs e)
        {
            HideCellTip();
        }

        private void OnGridResize(object sender, EventArgs e)
        {
            // 入力中なら、入力欄がセルからずれるので、入力を終える
            EndInput();

            ResizeColumns();
        }

        /// <summary>
        /// マウスの右ボタンで押した行を、指している行にする（右クリックで出すメニューが、押した行を対象にできるように）。
        /// </summary>
        private void OnGridMouseDown(object sender, MouseEventArgs e)
        {
            if (!EnableRightClickSelect || e.Button != MouseButtons.Right)
            {
                return;
            }

            if (_grid.PointAt(e.Location) == PointAtEnum.AtDataArea)
            {
                _grid.Col = _grid.ColContaining(e.X);
                _grid.Row = _grid.RowContaining(e.Y);
            }
        }

        private void OnGridDisposed(object sender, EventArgs e)
        {
            _grid.MouseDown -= OnGridMouseDown;
            _grid.Disposed -= OnGridDisposed;
            _grid.UnboundColumnFetch -= OnUnboundColumnFetch;
            _grid.FetchRowStyle -= OnFetchRowStyle;
            _grid.FetchCellTips -= OnFetchCellTips;
            _grid.Resize -= OnGridResize;
            _grid.MouseLeave -= OnGridMouseLeave;
            TearDownInput();
            _toolTip.Hide();
        }

        #endregion
    }
}
