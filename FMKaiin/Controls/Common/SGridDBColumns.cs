using System;

namespace App.Controls
{
    /// <summary>
    /// グリッドのセルの中での、文字の横位置。GGridDBCellDisp に当たる。
    /// </summary>
    public enum SGridDBCellDisp
    {
        /// <summary>指定しない（グリッドの設定のまま）。</summary>
        Default,
        /// <summary>左寄せ。</summary>
        Left,
        /// <summary>中央寄せ。</summary>
        Center,
        /// <summary>右寄せ。</summary>
        Right,
    }

    /// <summary>
    /// 項目の値をそのまま表示せず、表示する文字列をプログラムで作る列（区分の値を名称にする、など）の、文字列を返す関数。
    /// </summary>
    /// <param name="column">対象の列。その行の項目の値は <see cref="SGridDBColumn.UnboundValue"/> で読める。</param>
    /// <param name="row">行の番号（グリッドに表示している並びでの、0 から始まる番号）。</param>
    /// <returns>セルに表示する文字列。</returns>
    public delegate string SGridDBUnboundFetch(SGridDBColumn column, int row);

    /// <summary>
    /// 行ごとの見た目（文字の色など）を決める関数。GGridDBCommon.SetRowStyles に渡していたものに当たる。
    /// </summary>
    /// <param name="row">行の番号（グリッドに表示している並びでの、0 から始まる番号）。</param>
    /// <param name="style">その行のセルの見た目。文字の色（ForeColor）などを設定する。</param>
    public delegate void SGridDBRowStyle(int row, C1.Win.C1TrueDBGrid.Style style);

    /// <summary>
    /// <see cref="SGridDB"/> に登録する、グリッドの列 1 つ分の定義。GGridDBBase に当たる。
    /// 列の種類ごとのクラス（<see cref="SGridDBText"/> など）を使う。
    /// </summary>
    /// <remarks>
    /// 列の幅は、次のどちらかで指定する（GGridDB と同じ決まり）。
    /// <list type="bullet">
    /// <item>ピクセル数（<see cref="Width"/>）。96 DPI 基準で、表示倍率に合わせて拡大する。</item>
    /// <item>
    /// 割合（<see cref="WidthRatio"/>）。グリッドの幅（縦スクロール バーを除く）に対する割合。
    /// 0 にすると、ここまでの列の残りの幅をすべて使う（いちばん右の列に指定する）。
    /// </item>
    /// </list>
    /// </remarks>
    public abstract class SGridDBColumn
    {
        /// <param name="dataField">表示する項目の名前。</param>
        /// <param name="caption">列の見出し。</param>
        /// <param name="width">列の幅（96 DPI 基準のピクセル）。</param>
        protected SGridDBColumn(string dataField, string caption, int width)
            : this(dataField, caption)
        {
            // 0 以下は、残りの幅をすべて使う指定（GGridDB と同じ）
            Width = width;
        }

        /// <param name="dataField">表示する項目の名前。</param>
        /// <param name="caption">列の見出し。</param>
        /// <param name="widthRatio">列の幅の、グリッドの幅に対する割合。0 にすると、残りの幅をすべて使う。</param>
        protected SGridDBColumn(string dataField, string caption, float widthRatio)
            : this(dataField, caption)
        {
            if (widthRatio < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(widthRatio));
            }
            WidthRatio = widthRatio;
        }

        private SGridDBColumn(string dataField, string caption)
        {
            if (string.IsNullOrEmpty(dataField))
            {
                throw new ArgumentException("項目の名前を指定してください。", nameof(dataField));
            }

            DataField = dataField;
            Caption = caption ?? string.Empty;
            Visible = true;
        }

        /// <summary>表示する項目の名前。</summary>
        public string DataField { get; private set; }

        /// <summary>列の見出し。</summary>
        public string Caption { get; set; }

        /// <summary>列の幅（96 DPI 基準のピクセル）。0 以下の場合は、<see cref="WidthRatio"/> に従う。</summary>
        public int Width { get; set; }

        /// <summary>
        /// 列の幅の、グリッドの幅（縦スクロール バーを除く）に対する割合。<see cref="Width"/> が 0 以下のときに使う。
        /// 0 にすると、ここまでの列の残りの幅をすべて使う。
        /// </summary>
        public float WidthRatio { get; set; }

        /// <summary>セルの中での、文字の横位置。</summary>
        public SGridDBCellDisp CellDisp { get; set; }

        /// <summary>列を表示するか。既定は true。</summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 表示する文字列を作る関数。指定すると、項目の値をそのまま表示せず、この関数が返した文字列を表示する。
        /// </summary>
        public SGridDBUnboundFetch UnboundFetch { get; set; }

        /// <summary>
        /// <see cref="UnboundFetch"/> を呼んでいる行の、この列の項目の値。行が無い場合と、データにその項目が無い場合は null。
        /// <see cref="UnboundFetch"/> の中で読むためのもの。
        /// </summary>
        public object UnboundValue { get; internal set; }

        /// <summary>
        /// 数値の表示の書式（グリッドの列の NumberFormat に渡す。例: "###,###,###,##0"）。空なら、値をそのまま表示する。
        /// <see cref="UnboundFetch"/> を指定した列では、使わない。列の種類ごとのクラスが設定する。
        /// </summary>
        protected internal string NumberFormat { get; set; }

        /// <summary>
        /// この列のセルで入力するときに、セルに重ねて表示するコントロール（入力欄・コンボ・チェック ボックスなど）。
        /// 指定しなければ、この列では入力できない。GGridDBCommon.SetFocusControlInGrid に当たる。
        /// </summary>
        /// <remarks>
        /// コントロールは、フォームに置いておく（グリッドと同じ親でなくてもよい）。<see cref="SGridDB.EndAdd"/> で、
        /// 見えない位置へ移され、Tab での移動先から外される。入力を始めると、指している行のセルの位置に表示される。
        /// コントロールと行の値の受け渡しは、<see cref="App.SControlDB"/> で行う（行を返す関数が、指している行を返すようにする）。
        /// </remarks>
        public System.Windows.Forms.Control InputControl { get; set; }

        /// <summary>
        /// 入力欄を次へ移るとき（<see cref="SGridDB.SelectNextInput"/>）の順番。小さいものから順に移る。
        /// 同じ値の列は、左から順。既定は 0（すべて 0 のままなら、左から順）。GGridDBCommon.SetTabIndex に当たる。
        /// </summary>
        public int InputTabIndex { get; set; }
    }

    /// <summary>
    /// 数字（コード・番号など）を表示する列。GGridDBNumber に当たる。右寄せで、値をそのまま表示する（桁区切りは付けない）。
    /// </summary>
    public class SGridDBNumber : SGridDBColumn
    {
        /// <param name="width">列の幅（96 DPI 基準のピクセル）。</param>
        public SGridDBNumber(string dataField, string caption, int width)
            : base(dataField, caption, width)
        {
            CellDisp = SGridDBCellDisp.Right;
        }

        /// <param name="widthRatio">列の幅の、グリッドの幅に対する割合。0 にすると、残りの幅をすべて使う。</param>
        public SGridDBNumber(string dataField, string caption, float widthRatio)
            : base(dataField, caption, widthRatio)
        {
            CellDisp = SGridDBCellDisp.Right;
        }
    }

    /// <summary>
    /// 金額を表示する列。GGridDBCurrency に当たる。右寄せで、3 桁ごとに区切りを付けて表示する。
    /// </summary>
    public class SGridDBCurrency : SGridDBColumn
    {
        /// <summary>金額の書式。GGridDBCurrency と同じ。</summary>
        private const string CurrencyFormat = "###,###,###,##0";

        /// <param name="width">列の幅（96 DPI 基準のピクセル）。</param>
        public SGridDBCurrency(string dataField, string caption, int width)
            : base(dataField, caption, width)
        {
            CellDisp = SGridDBCellDisp.Right;
            NumberFormat = CurrencyFormat;
        }

        /// <param name="widthRatio">列の幅の、グリッドの幅に対する割合。0 にすると、残りの幅をすべて使う。</param>
        public SGridDBCurrency(string dataField, string caption, float widthRatio)
            : base(dataField, caption, widthRatio)
        {
            CellDisp = SGridDBCellDisp.Right;
            NumberFormat = CurrencyFormat;
        }
    }

    /// <summary>
    /// 日付を表示する列。GGridDBDate に当たる。中央寄せで、和暦なら「R08/11/26」、西暦なら「2026/11/26」の形で表示する。
    /// 和暦・西暦は、アプリ全体の設定（<see cref="SDateBox.DefaultCalendarKind"/>）に従う。日付でない値と、空の値は、空欄にする。
    /// </summary>
    public class SGridDBDate : SGridDBColumn
    {
        /// <param name="width">列の幅（96 DPI 基準のピクセル）。</param>
        public SGridDBDate(string dataField, string caption, int width)
            : base(dataField, caption, width)
        {
            Initialize();
        }

        /// <param name="widthRatio">列の幅の、グリッドの幅に対する割合。0 にすると、残りの幅をすべて使う。</param>
        public SGridDBDate(string dataField, string caption, float widthRatio)
            : base(dataField, caption, widthRatio)
        {
            Initialize();
        }

        private void Initialize()
        {
            CellDisp = SGridDBCellDisp.Center;
            UnboundFetch = FormatDate;
        }

        /// <summary>その行の値を、日付の文字列にする。</summary>
        private static string FormatDate(SGridDBColumn column, int row)
        {
            object value = column.UnboundValue;
            if (value == null || value == DBNull.Value)
            {
                return string.Empty;
            }

            DateTime date;
            if (value is DateTime)
            {
                date = (DateTime)value;
            }
            else if (!DateTime.TryParse(value.ToString(), out date))
            {
                return string.Empty;
            }
            if (date == DateTime.MinValue)
            {
                return string.Empty;
            }

            bool japanese = SDateBox.DefaultCalendarKind == SDateCalendarKind.Japanese;
            return SDateText.FormatForEdit(date, SDateFormat.YearMonthDay, japanese);
        }
    }

    /// <summary>
    /// 表示する文字列を、プログラムで作る列。GGridDBUnbound に当たる。
    /// 項目の名前は、データに無いものでもよい（その場合、<see cref="SGridDBColumn.UnboundValue"/> は null になる）。
    /// </summary>
    public class SGridDBUnbound : SGridDBColumn
    {
        /// <param name="fetch">セルに表示する文字列を返す関数。</param>
        /// <param name="width">列の幅（96 DPI 基準のピクセル）。</param>
        public SGridDBUnbound(string dataField, string caption, SGridDBUnboundFetch fetch, int width)
            : this(dataField, caption, fetch, width, SGridDBCellDisp.Left)
        {
        }

        /// <param name="fetch">セルに表示する文字列を返す関数。</param>
        /// <param name="width">列の幅（96 DPI 基準のピクセル）。</param>
        public SGridDBUnbound(string dataField, string caption, SGridDBUnboundFetch fetch, int width, SGridDBCellDisp cellDisp)
            : base(dataField, caption, width)
        {
            Initialize(fetch, cellDisp);
        }

        /// <param name="fetch">セルに表示する文字列を返す関数。</param>
        /// <param name="widthRatio">列の幅の、グリッドの幅に対する割合。0 にすると、残りの幅をすべて使う。</param>
        public SGridDBUnbound(string dataField, string caption, SGridDBUnboundFetch fetch, float widthRatio)
            : this(dataField, caption, fetch, widthRatio, SGridDBCellDisp.Left)
        {
        }

        /// <param name="fetch">セルに表示する文字列を返す関数。</param>
        /// <param name="widthRatio">列の幅の、グリッドの幅に対する割合。0 にすると、残りの幅をすべて使う。</param>
        public SGridDBUnbound(string dataField, string caption, SGridDBUnboundFetch fetch, float widthRatio, SGridDBCellDisp cellDisp)
            : base(dataField, caption, widthRatio)
        {
            Initialize(fetch, cellDisp);
        }

        private void Initialize(SGridDBUnboundFetch fetch, SGridDBCellDisp cellDisp)
        {
            if (fetch == null)
            {
                throw new ArgumentNullException(nameof(fetch));
            }
            UnboundFetch = fetch;
            CellDisp = cellDisp;
        }
    }

    /// <summary>
    /// 文字列を表示する列。GGridDBText に当たる。文字は、指定しなければ左寄せ。
    /// </summary>
    public class SGridDBText : SGridDBColumn
    {
        /// <param name="width">列の幅（96 DPI 基準のピクセル）。</param>
        public SGridDBText(string dataField, string caption, int width)
            : this(dataField, caption, width, SGridDBCellDisp.Left)
        {
        }

        /// <param name="width">列の幅（96 DPI 基準のピクセル）。</param>
        public SGridDBText(string dataField, string caption, int width, SGridDBCellDisp cellDisp)
            : base(dataField, caption, width)
        {
            CellDisp = cellDisp;
        }

        /// <param name="widthRatio">列の幅の、グリッドの幅に対する割合。0 にすると、残りの幅をすべて使う。</param>
        public SGridDBText(string dataField, string caption, float widthRatio)
            : this(dataField, caption, widthRatio, SGridDBCellDisp.Left)
        {
        }

        /// <param name="widthRatio">列の幅の、グリッドの幅に対する割合。0 にすると、残りの幅をすべて使う。</param>
        public SGridDBText(string dataField, string caption, float widthRatio, SGridDBCellDisp cellDisp)
            : base(dataField, caption, widthRatio)
        {
            CellDisp = cellDisp;
        }
    }
}
