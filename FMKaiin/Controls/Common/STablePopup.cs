using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// <see cref="STableComboBox"/> の下に表示する、見出し付き・複数列の一覧。
    /// キー操作（行の移動・確定・取り消し）は <see cref="STableComboBox"/> の側で処理し、ここではマウス操作と描画だけを扱う。
    /// フォーカスを受け取らないことと、外側のクリックなどで閉じる仕組みは <see cref="SPopupForm"/> にある。
    /// </summary>
    internal sealed class STablePopup : SPopupForm
    {
        /// <summary>行の高さの最小値。96 DPI 基準のピクセル。<see cref="SComboBox"/> の一覧の行と同じ高さ。</summary>
        private const float MinRowHeight = 22f;
        /// <summary>文字の上下に取る余白の合計。96 DPI 基準のピクセル。</summary>
        private const float RowVerticalPadding = 6f;
        /// <summary>セルの左右の余白。96 DPI 基準のピクセル。</summary>
        private const float CellPadding = 6f;

        private static readonly Color HeaderBackColor = Color.FromArgb(243, 246, 250);
        private static readonly Color HeaderForeColor = Color.FromArgb(96, 104, 120);
        private static readonly Color LineColor = Color.FromArgb(226, 232, 240);

        private const TextFormatFlags CellTextFlags =
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.SingleLine |
            TextFormatFlags.NoPadding |
            TextFormatFlags.NoPrefix |
            TextFormatFlags.EndEllipsis;

        private readonly STableComboBox _owner;
        private readonly VScrollBar _scrollBar;

        private IList<DataRow> _rows = new DataRow[0];
        // 表示する行が無いときに、行の代わりに表示する文字列（一覧を開けない理由）
        private string _emptyMessage = string.Empty;
        private int _highlightIndex = -1;
        private int _topIndex;

        // 表示倍率を掛けた後の寸法
        private float _scale = 1f;
        private int _rowHeight = 1;
        private int _headerHeight;
        private int _visibleRows = 1;

        private Point _lastMousePosition;

        public STablePopup(STableComboBox owner)
            : base(owner)
        {
            _owner = owner;

            _scrollBar = new VScrollBar();
            _scrollBar.Visible = false;
            _scrollBar.TabStop = false;
            _scrollBar.ValueChanged += delegate { SetTopIndex(_scrollBar.Value); };
            Controls.Add(_scrollBar);
        }

        /// <summary>強調表示している行（キーやマウスで指している行）の位置。無ければ -1。</summary>
        public int HighlightIndex
        {
            get { return _highlightIndex; }
            set
            {
                _highlightIndex = value >= 0 && value < _rows.Count ? value : -1;
                EnsureHighlightVisible();
                Invalidate();
            }
        }

        /// <summary>1 画面に表示している行数（PageUp / PageDown で移動する量）。</summary>
        public int PageSize
        {
            get { return Math.Max(1, _visibleRows); }
        }

        /// <summary>
        /// <paramref name="rows"/> を表示する。表示中に呼んだ場合は、内容と大きさを更新する。
        /// </summary>
        /// <param name="highlightIndex">強調表示する行。無ければ -1。</param>
        /// <param name="emptyMessage">
        /// <paramref name="rows"/> が 1 行も無いときに、行の代わりに表示する文字列（「選択できるデータがありません」など）。
        /// </param>
        public void ShowRows(IList<DataRow> rows, int highlightIndex, string emptyMessage)
        {
            _rows = rows;
            _emptyMessage = emptyMessage ?? string.Empty;
            _highlightIndex = highlightIndex >= 0 && highlightIndex < rows.Count ? highlightIndex : -1;
            _topIndex = 0;
            UpdateLayout();

            if (!Visible)
            {
                // 表示した瞬間にマウスの下にあった行を強調しないよう、いまの位置を覚えておく
                _lastMousePosition = MousePosition;
                ShowPopup();
            }

            EnsureHighlightVisible();
            Invalidate();
        }

        /// <summary>一覧を <paramref name="rowCount"/> 行分スクロールする（正の値で下へ）。</summary>
        public void ScrollBy(int rowCount)
        {
            SetTopIndex(_topIndex + rowCount);
        }

        #region 配置

        /// <summary>
        /// 行数・列・表示倍率から、ウィンドウの位置と大きさ、スクロール バーを決める。
        /// </summary>
        private void UpdateLayout()
        {
            _scale = LayoutScale.Of(_owner);

            int textHeight = TextRenderer.MeasureText("あg", _owner.Font, Size.Empty, TextFormatFlags.NoPadding).Height;
            _rowHeight = Math.Max((int)Math.Round(MinRowHeight * _scale), textHeight + (int)Math.Round(RowVerticalPadding * _scale));
            _headerHeight = _owner.HeaderVisible ? _rowHeight : 0;
            _visibleRows = Math.Max(1, Math.Min(_rows.Count, Math.Max(1, _owner.MaxDropDownItems)));

            bool needsScroll = _rows.Count > _visibleRows;
            int scrollWidth = needsScroll ? SystemInformation.VerticalScrollBarWidth : 0;

            int columnsWidth = 0;
            foreach (STableComboColumn column in _owner.PopupColumns)
            {
                columnsWidth += (int)Math.Round(column.Width * _scale);
            }

            // 幅の指定が無ければ、列の幅の合計と入力欄の幅の、広いほうに合わせる
            int width = _owner.DropDownWidth > 0
                ? (int)Math.Round(_owner.DropDownWidth * _scale)
                : Math.Max(_owner.Width, columnsWidth + scrollWidth + 2);

            if (_rows.Count == 0 && _emptyMessage.Length > 0)
            {
                // 行の代わりに表示する文字列が、収まる幅にする
                int messageWidth = TextRenderer.MeasureText(_emptyMessage, _owner.Font, Size.Empty, TextFormatFlags.NoPadding).Width;
                width = Math.Max(width, messageWidth + (int)Math.Round(CellPadding * _scale) * 2 + 2);
            }
            int height = 2 + _headerHeight + _visibleRows * _rowHeight;

            PlaceNearOwner(width, height);

            _scrollBar.Visible = needsScroll;
            if (needsScroll)
            {
                _scrollBar.Bounds = new Rectangle(width - 1 - scrollWidth, 1 + _headerHeight, scrollWidth, _visibleRows * _rowHeight);
                _scrollBar.Minimum = 0;
                _scrollBar.Value = 0;
                _scrollBar.Maximum = _rows.Count - 1;
                _scrollBar.LargeChange = _visibleRows;
                _scrollBar.SmallChange = 1;
            }
        }

        private int MaxTopIndex
        {
            get { return Math.Max(0, _rows.Count - _visibleRows); }
        }

        private void SetTopIndex(int value)
        {
            value = Math.Max(0, Math.Min(value, MaxTopIndex));
            if (value == _topIndex)
            {
                return;
            }

            _topIndex = value;
            if (_scrollBar.Visible && _scrollBar.Value != value)
            {
                _scrollBar.Value = value;
            }
            Invalidate();
        }

        private void EnsureHighlightVisible()
        {
            if (_highlightIndex < 0)
            {
                return;
            }

            if (_highlightIndex < _topIndex)
            {
                SetTopIndex(_highlightIndex);
            }
            else if (_highlightIndex >= _topIndex + _visibleRows)
            {
                SetTopIndex(_highlightIndex - _visibleRows + 1);
            }
        }

        /// <summary>行を描く領域の右端（スクロール バーの手前）。</summary>
        private int ContentRight
        {
            get { return Width - 1 - (_scrollBar.Visible ? _scrollBar.Width : 0); }
        }

        /// <summary>
        /// 各列の幅（ピクセル）。列の幅の合計が一覧の幅より狭ければ、最後の列を広げて埋める。
        /// </summary>
        private int[] GetColumnWidths(IList<STableComboColumn> columns)
        {
            var widths = new int[columns.Count];
            int total = 0;
            for (int i = 0; i < columns.Count; i++)
            {
                widths[i] = (int)Math.Round(columns[i].Width * _scale);
                total += widths[i];
            }

            int available = ContentRight - 1;
            if (widths.Length > 0 && total < available)
            {
                widths[widths.Length - 1] += available - total;
            }
            return widths;
        }

        /// <summary><paramref name="point"/> にある行の位置。行の上でなければ -1。</summary>
        private int GetRowIndexAt(Point point)
        {
            int top = 1 + _headerHeight;
            if (point.X < 1 || point.X >= ContentRight || point.Y < top)
            {
                return -1;
            }

            int index = _topIndex + (point.Y - top) / _rowHeight;
            return index < _rows.Count && index < _topIndex + _visibleRows ? index : -1;
        }

        #endregion

        #region マウス操作

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // マウスが実際に動いたときだけ、指している行を強調する
            // （キーで行を移動している最中に、止まっているマウスの下の行へ戻ってしまうのを防ぐ）
            Point position = MousePosition;
            if (position == _lastMousePosition)
            {
                return;
            }
            _lastMousePosition = position;

            int index = GetRowIndexAt(e.Location);
            if (index >= 0 && index != _highlightIndex)
            {
                _highlightIndex = index;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                int index = GetRowIndexAt(e.Location);
                if (index >= 0)
                {
                    _owner.CommitPopupRow(index);
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            ScrollBy(-Math.Sign(e.Delta) * Math.Max(1, SystemInformation.MouseWheelScrollLines));
        }

        #endregion

        #region 描画

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font font = _owner.Font;
            IList<STableComboColumn> columns = _owner.PopupColumns;
            int[] widths = GetColumnWidths(columns);
            int padding = (int)Math.Round(CellPadding * _scale);
            int contentRight = ContentRight;

            g.Clear(_owner.ListBackColor);

            if (_headerHeight > 0)
            {
                var headerBounds = new Rectangle(1, 1, Width - 2, _headerHeight);
                using (var headerBrush = new SolidBrush(HeaderBackColor))
                {
                    g.FillRectangle(headerBrush, headerBounds);
                }

                int x = 1;
                for (int i = 0; i < columns.Count; i++)
                {
                    var cell = new Rectangle(x + padding, 1, Math.Max(0, widths[i] - padding * 2), _headerHeight);
                    TextRenderer.DrawText(
                        g, columns[i].HeaderText, font, cell, HeaderForeColor, CellTextFlags | TextFormatFlags.HorizontalCenter);
                    x += widths[i];
                }

                using (var linePen = new Pen(LineColor))
                {
                    g.DrawLine(linePen, 1, headerBounds.Bottom - 1, Width - 2, headerBounds.Bottom - 1);
                }
            }

            int y = 1 + _headerHeight;
            if (_rows.Count == 0 && _emptyMessage.Length > 0)
            {
                // 表示する行が無い。1 行分の場所に、その理由を薄い色で表示する
                var messageBounds = new Rectangle(1 + padding, y, Math.Max(0, Width - 2 - padding * 2), _rowHeight);
                TextRenderer.DrawText(
                    g, _emptyMessage, font, messageBounds, SControlStyle.GetInactiveColor(_owner.ForeColor, _owner.ListBackColor),
                    CellTextFlags | TextFormatFlags.Left);
            }

            for (int index = _topIndex; index < _rows.Count && index < _topIndex + _visibleRows; index++)
            {
                DataRow row = _rows[index];
                Color rowTextColor = _owner.ForeColor;
                if (index == _highlightIndex)
                {
                    using (var selectionBrush = new SolidBrush(_owner.SelectionColor))
                    {
                        g.FillRectangle(selectionBrush, 1, y, contentRight - 1, _rowHeight);
                    }
                    // 選択の色の上で読める色にする（濃い色の上では白）
                    rowTextColor = SControlStyle.GetTextColorOn(_owner.SelectionColor, _owner.ForeColor);
                }

                int x = 1;
                for (int i = 0; i < columns.Count; i++)
                {
                    var cell = new Rectangle(x + padding, y, Math.Max(0, widths[i] - padding * 2), _rowHeight);
                    TextRenderer.DrawText(
                        g, _owner.GetCellText(row, columns[i].DataPropertyName), font, cell, rowTextColor,
                        CellTextFlags | GetAlignmentFlag(columns[i].TextAlign));
                    x += widths[i];
                }

                y += _rowHeight;
            }

            using (var borderPen = new Pen(SControlStyle.BorderColor))
            {
                g.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
            }
        }

        private static TextFormatFlags GetAlignmentFlag(HorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case HorizontalAlignment.Center:
                    return TextFormatFlags.HorizontalCenter;
                case HorizontalAlignment.Right:
                    return TextFormatFlags.Right;
                default:
                    return TextFormatFlags.Left;
            }
        }

        #endregion
    }
}
