using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>カレンダー（<see cref="SCalendarPopup"/>）の表示の種類。何を選ぶかで決まる。</summary>
    internal enum SCalendarMode
    {
        /// <summary>日を選ぶ（1 か月分を表示する）。</summary>
        Day,
        /// <summary>月を選ぶ（1 年分を表示する）。</summary>
        Month,
        /// <summary>年を選ぶ（12 年分を表示する）。</summary>
        Year,
    }

    /// <summary>
    /// <see cref="SDateBox"/> の下に表示するカレンダー。日を選ぶ表示（1 か月分）、月を選ぶ表示（1 年分）、年を選ぶ表示（12 年分）がある。
    /// キー操作は <see cref="SDateBox"/> の側で受けて <see cref="MoveCursor"/> などを呼ぶ。ここではマウス操作と描画を扱う。
    /// フォーカスを受け取らないことと、外側のクリックなどで閉じる仕組みは <see cref="SPopupForm"/> にある。
    /// </summary>
    internal sealed class SCalendarPopup : SPopupForm
    {
        // 寸法は 96 DPI 基準のピクセル
        private const float Padding96 = 8f;
        private const float HeaderHeight = 30f;
        private const float WeekdayHeight = 22f;
        private const float DayCellWidth = 34f;
        private const float DayCellHeight = 28f;
        private const float MonthCellWidth = 60f;
        private const float MonthCellHeight = 40f;
        // 年のマスは、和暦の表示（「令和 08年」）が、隣とくっついて見えない幅にする
        private const float YearCellWidth = 86f;
        private const float YearCellHeight = 40f;
        private const float NavButtonWidth = 26f;

        private const int DayColumns = 7;
        private const int DayRows = 6;
        private const int MonthColumns = 4;
        private const int MonthRows = 3;
        private const int YearColumns = 4;
        private const int YearRows = 3;

        /// <summary>年の表示で、1 画面に並べる年の数。</summary>
        private const int YearsPerPage = YearColumns * YearRows;

        private static readonly string[] WeekdayNames = { "日", "月", "火", "水", "木", "金", "土" };
        private static readonly Color SundayColor = Color.FromArgb(200, 60, 60);
        private static readonly Color SaturdayColor = Color.FromArgb(50, 100, 200);
        private static readonly Color HoverColor = Color.FromArgb(236, 242, 250);
        private static readonly Color MutedColor = Color.FromArgb(150, 158, 172);

        /// <summary>見出しの左右の、表示する月（年）を動かすボタン（≪ ＜ ＞ ≫）の色。</summary>
        private static readonly Color NavColor = Color.RoyalBlue;

        private const TextFormatFlags CenterFlags =
            TextFormatFlags.HorizontalCenter |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.SingleLine |
            TextFormatFlags.NoPadding |
            TextFormatFlags.NoPrefix;

        /// <summary>見出しの左右にある、表示する月（年）を動かすボタン。</summary>
        private enum NavButton
        {
            None,
            /// <summary>大きく戻る（日の表示では 1 年、月の表示では 10 年、年の表示では 5 画面分の 60 年）。</summary>
            BackFar,
            /// <summary>戻る（日の表示では 1 か月、月の表示では 1 年、年の表示では 1 画面分の 12 年）。</summary>
            Back,
            /// <summary>進む。</summary>
            Forward,
            /// <summary>大きく進む。</summary>
            ForwardFar,
        }

        private SCalendarMode _mode = SCalendarMode.Day;
        private bool _japanese;
        private DateTime _minDate = DateTime.MinValue;
        private DateTime _maxDate = DateTime.MaxValue;
        private DateTime _today = DateTime.Today;

        // キーやマウスで指している日（月の表示では、その月の 1 日。年の表示では、その年の 1 月 1 日）
        private DateTime _cursor = DateTime.Today;
        // マウスが乗っている日（月）。無ければ null
        private DateTime? _hover;

        private float _scale = 1f;

        public SCalendarPopup(Control owner)
            : base(owner)
        {
        }

        /// <summary>日（月の表示では月、年の表示では年）がクリックされたときに発生する。選ばれた日は <see cref="CursorDate"/>。</summary>
        public event EventHandler DateChosen;

        /// <summary>いま指している日。月の表示では、指している月の 1 日。年の表示では、指している年の 1 月 1 日。</summary>
        public DateTime CursorDate
        {
            get { return _cursor; }
        }

        /// <summary>
        /// カレンダーを表示する。
        /// </summary>
        /// <param name="mode">表示の種類（日・月・年のどれを選ぶか）。</param>
        /// <param name="japanese">見出しを和暦にするか。</param>
        /// <param name="cursor">最初に指しておく日。</param>
        /// <param name="minDate">選べる最初の日。</param>
        /// <param name="maxDate">選べる最後の日。</param>
        public void ShowCalendar(SCalendarMode mode, bool japanese, DateTime cursor, DateTime minDate, DateTime maxDate)
        {
            _mode = mode;
            _japanese = japanese;
            _minDate = minDate.Date;
            _maxDate = maxDate.Date;
            _today = DateTime.Today;
            _hover = null;
            _cursor = Normalize(Clamp(cursor.Date));

            _scale = LayoutScale.Of(OwnerControl);
            Size cell = CellSize;
            PlaceNearOwner(
                S(Padding96) * 2 + cell.Width * Columns,
                GridOrigin.Y + cell.Height * Rows + S(Padding96));
            ShowPopup();
            Invalidate();
        }

        /// <summary>
        /// 指している日を動かす。範囲の外へは動かない。
        /// </summary>
        /// <param name="days">動かす日数（月・年の表示では無視する）。</param>
        /// <param name="months">動かす月数。</param>
        public void MoveCursor(int days, int months)
        {
            DateTime moved = AddMonthsSafe(_cursor, months);
            if (_mode == SCalendarMode.Day)
            {
                moved = AddDaysSafe(moved, days);
            }
            _cursor = Normalize(Clamp(moved));
            Invalidate();
        }

        /// <summary>
        /// 矢印キーの 1 回分で、指している日を動かす。日の表示では左右 1 日・上下 1 週間、月の表示では左右 1 か月・上下 1 行分、
        /// 年の表示では左右 1 年・上下 1 行分。
        /// </summary>
        public void MoveCursorByArrow(int horizontal, int vertical)
        {
            switch (_mode)
            {
                case SCalendarMode.Year:
                    MoveCursor(0, (horizontal + vertical * YearColumns) * 12);
                    break;
                case SCalendarMode.Month:
                    MoveCursor(0, horizontal + vertical * MonthColumns);
                    break;
                default:
                    MoveCursor(horizontal + vertical * DayColumns, 0);
                    break;
            }
        }

        /// <summary>PageUp / PageDown の 1 回分。日の表示では 1 か月、月の表示では 1 年、年の表示では 1 画面分（12 年）。</summary>
        public void MoveCursorByPage(int direction)
        {
            MoveCursor(0, direction * GetNavMonths(false));
        }

        /// <summary>
        /// 見出しのボタン 1 回分で動かす月数。<paramref name="far"/> が true なら、大きく動かすボタンの分。
        /// </summary>
        private int GetNavMonths(bool far)
        {
            switch (_mode)
            {
                case SCalendarMode.Year:
                    return (far ? YearsPerPage * 5 : YearsPerPage) * 12;
                case SCalendarMode.Month:
                    return far ? 120 : 12;
                default:
                    return far ? 12 : 1;
            }
        }

        #region 日付の計算

        private int S(float value)
        {
            return (int)Math.Round(value * _scale);
        }

        /// <summary>月の表示では、その月の 1 日にそろえる。年の表示では、その年の 1 月 1 日にそろえる。</summary>
        private DateTime Normalize(DateTime date)
        {
            switch (_mode)
            {
                case SCalendarMode.Year:
                    return new DateTime(date.Year, 1, 1);
                case SCalendarMode.Month:
                    return new DateTime(date.Year, date.Month, 1);
                default:
                    return date;
            }
        }

        private DateTime Clamp(DateTime date)
        {
            return date < _minDate ? _minDate : date > _maxDate ? _maxDate : date;
        }

        /// <summary>選べる日（月・年の表示では、選べる日を 1 日でも含む月・年）か。</summary>
        private bool IsSelectable(DateTime date)
        {
            switch (_mode)
            {
                case SCalendarMode.Year:
                    return new DateTime(date.Year, 12, 31) >= _minDate && new DateTime(date.Year, 1, 1) <= _maxDate;
                case SCalendarMode.Month:
                    DateTime last = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                    return last >= _minDate && date <= _maxDate;
                default:
                    return date >= _minDate && date <= _maxDate;
            }
        }

        private static DateTime AddMonthsSafe(DateTime date, int months)
        {
            try
            {
                return date.AddMonths(months);
            }
            catch (ArgumentOutOfRangeException)
            {
                // DateTime で表せる範囲を超えた
                return months < 0 ? DateTime.MinValue.Date : DateTime.MaxValue.Date;
            }
        }

        private static DateTime AddDaysSafe(DateTime date, int days)
        {
            try
            {
                return date.AddDays(days);
            }
            catch (ArgumentOutOfRangeException)
            {
                return days < 0 ? DateTime.MinValue.Date : DateTime.MaxValue.Date;
            }
        }

        /// <summary>日の表示で、左上のマスに来る日（表示している月の 1 日を含む週の日曜日）。</summary>
        private DateTime FirstCellDate
        {
            get
            {
                var first = new DateTime(_cursor.Year, _cursor.Month, 1);
                return AddDaysSafe(first, -(int)first.DayOfWeek);
            }
        }

        /// <summary>
        /// 年の表示で、左上のマスに来る年。12 年ごとの区切り（年を 12 で割った商が同じ年）で 1 画面にするので、
        /// 1 画面分（12 年）動かしても、指しているマスの位置は変わらない。
        /// </summary>
        private int FirstYearOfPage
        {
            get { return (_cursor.Year / YearsPerPage) * YearsPerPage; }
        }

        /// <summary>年の表示で、<paramref name="index"/> 番目のマスの年（1 月 1 日）。DateTime で表せない年なら null。</summary>
        private DateTime? GetYearCellDate(int index)
        {
            int year = FirstYearOfPage + index;
            return year >= DateTime.MinValue.Year && year <= DateTime.MaxValue.Year ? new DateTime(year, 1, 1) : (DateTime?)null;
        }

        #endregion

        #region 配置

        private Rectangle HeaderBounds
        {
            get { return new Rectangle(S(Padding96), S(Padding96), Width - S(Padding96) * 2, S(HeaderHeight)); }
        }

        private Rectangle GetNavButtonBounds(NavButton button)
        {
            Rectangle header = HeaderBounds;
            int width = S(NavButtonWidth);
            switch (button)
            {
                case NavButton.BackFar:
                    return new Rectangle(header.Left, header.Top, width, header.Height);
                case NavButton.Back:
                    return new Rectangle(header.Left + width, header.Top, width, header.Height);
                case NavButton.Forward:
                    return new Rectangle(header.Right - width * 2, header.Top, width, header.Height);
                case NavButton.ForwardFar:
                    return new Rectangle(header.Right - width, header.Top, width, header.Height);
                default:
                    return Rectangle.Empty;
            }
        }

        private NavButton GetNavButtonAt(Point point)
        {
            foreach (NavButton button in new[] { NavButton.BackFar, NavButton.Back, NavButton.Forward, NavButton.ForwardFar })
            {
                if (GetNavButtonBounds(button).Contains(point))
                {
                    return button;
                }
            }
            return NavButton.None;
        }

        /// <summary>マス目（日・月・年）の左上の位置。日の表示だけ、見出しの下に曜日の行がある。</summary>
        private Point GridOrigin
        {
            get
            {
                int top = S(Padding96) + S(HeaderHeight) + (_mode == SCalendarMode.Day ? S(WeekdayHeight) : 0);
                return new Point(S(Padding96), top);
            }
        }

        private Size CellSize
        {
            get
            {
                switch (_mode)
                {
                    case SCalendarMode.Year:
                        return new Size(S(YearCellWidth), S(YearCellHeight));
                    case SCalendarMode.Month:
                        return new Size(S(MonthCellWidth), S(MonthCellHeight));
                    default:
                        return new Size(S(DayCellWidth), S(DayCellHeight));
                }
            }
        }

        /// <summary>マス目の列の数。</summary>
        private int Columns
        {
            get { return _mode == SCalendarMode.Year ? YearColumns : _mode == SCalendarMode.Month ? MonthColumns : DayColumns; }
        }

        /// <summary>マス目の行の数。</summary>
        private int Rows
        {
            get { return _mode == SCalendarMode.Year ? YearRows : _mode == SCalendarMode.Month ? MonthRows : DayRows; }
        }

        /// <summary><paramref name="point"/> にあるマスの日（月・年）。マスの上でなければ null。</summary>
        private DateTime? GetDateAt(Point point)
        {
            Point origin = GridOrigin;
            Size cell = CellSize;
            if (point.X < origin.X || point.Y < origin.Y)
            {
                return null;
            }

            int column = (point.X - origin.X) / cell.Width;
            int row = (point.Y - origin.Y) / cell.Height;
            if (column >= Columns || row >= Rows)
            {
                return null;
            }

            if (_mode == SCalendarMode.Year)
            {
                return GetYearCellDate(row * YearColumns + column);
            }
            if (_mode == SCalendarMode.Month)
            {
                return new DateTime(_cursor.Year, row * MonthColumns + column + 1, 1);
            }

            // 表示している月の日だけを対象にする（前後の月の日は表示していない）
            DateTime date = AddDaysSafe(FirstCellDate, row * DayColumns + column);
            return date.Year == _cursor.Year && date.Month == _cursor.Month ? date : (DateTime?)null;
        }

        #endregion

        #region マウス操作

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            DateTime? date = GetDateAt(e.Location);
            if (date.HasValue && !IsSelectable(date.Value))
            {
                date = null;
            }
            if (date != _hover)
            {
                _hover = date;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_hover.HasValue)
            {
                _hover = null;
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            // 見出しのボタン: 表示する月（年）を動かす
            switch (GetNavButtonAt(e.Location))
            {
                case NavButton.BackFar:
                    MoveCursor(0, -GetNavMonths(true));
                    return;
                case NavButton.Back:
                    MoveCursor(0, -GetNavMonths(false));
                    return;
                case NavButton.Forward:
                    MoveCursor(0, GetNavMonths(false));
                    return;
                case NavButton.ForwardFar:
                    MoveCursor(0, GetNavMonths(true));
                    return;
            }

            DateTime? date = GetDateAt(e.Location);
            if (date.HasValue && IsSelectable(date.Value))
            {
                _cursor = Clamp(date.Value);
                _cursor = Normalize(_cursor);
                Invalidate();

                EventHandler handler = DateChosen;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        #endregion

        #region 描画

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font font = OwnerControl.Font;
            Color textColor = OwnerControl.ForeColor;

            g.Clear(SystemColors.Window);
            PaintHeader(g, font, textColor);
            switch (_mode)
            {
                case SCalendarMode.Year:
                    PaintYears(g, font, textColor);
                    break;
                case SCalendarMode.Month:
                    PaintMonths(g, font, textColor);
                    break;
                default:
                    PaintDays(g, font, textColor);
                    break;
            }

            using (var borderPen = new Pen(SControlStyle.BorderColor))
            {
                g.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
            }
        }

        private void PaintHeader(Graphics g, Font font, Color textColor)
        {
            string title;
            switch (_mode)
            {
                case SCalendarMode.Year:
                    // 表示している 12 年の範囲
                    DateTime? first = GetYearCellDate(0), last = GetYearCellDate(YearsPerPage - 1);
                    title = (first.HasValue ? SDateText.FormatYear(first.Value, _japanese) : string.Empty)
                        + " ～ " + (last.HasValue ? SDateText.FormatYear(last.Value, _japanese) : string.Empty);
                    break;
                case SCalendarMode.Month:
                    title = SDateText.FormatYear(_cursor, _japanese);
                    break;
                default:
                    title = SDateText.FormatYearMonth(_cursor, _japanese);
                    break;
            }
            // 見出し（年月、年、または年の範囲）は、太字にする
            using (var titleFont = new Font(font, FontStyle.Bold))
            {
                TextRenderer.DrawText(g, title, titleFont, HeaderBounds, textColor, CenterFlags);
            }

            DrawChevrons(g, GetNavButtonBounds(NavButton.BackFar), -1, 2, NavColor);
            DrawChevrons(g, GetNavButtonBounds(NavButton.Back), -1, 1, NavColor);
            DrawChevrons(g, GetNavButtonBounds(NavButton.Forward), 1, 1, NavColor);
            DrawChevrons(g, GetNavButtonBounds(NavButton.ForwardFar), 1, 2, NavColor);
        }

        /// <summary>
        /// 左右向きの山形を、<paramref name="count"/> 個並べて描く。
        /// </summary>
        /// <param name="direction">-1 で左向き、1 で右向き。</param>
        private void DrawChevrons(Graphics g, Rectangle bounds, int direction, int count, Color color)
        {
            float halfHeight = 4.5f * _scale;
            float depth = 4f * _scale;
            float gap = 5f * _scale;
            float centerX = bounds.Left + bounds.Width / 2f - gap * (count - 1) / 2f;
            float centerY = bounds.Top + bounds.Height / 2f;

            SmoothingMode previous = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            try
            {
                using (var pen = new Pen(color, 1.6f * _scale))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    pen.LineJoin = LineJoin.Round;
                    for (int i = 0; i < count; i++)
                    {
                        float x = centerX + gap * i;
                        g.DrawLines(pen, new[]
                        {
                            new PointF(x - direction * depth / 2f, centerY - halfHeight),
                            new PointF(x + direction * depth / 2f, centerY),
                            new PointF(x - direction * depth / 2f, centerY + halfHeight)
                        });
                    }
                }
            }
            finally
            {
                g.SmoothingMode = previous;
            }
        }

        private void PaintDays(Graphics g, Font font, Color textColor)
        {
            Point origin = GridOrigin;
            Size cell = CellSize;

            // 曜日の見出し
            int weekdayTop = origin.Y - S(WeekdayHeight);
            for (int column = 0; column < DayColumns; column++)
            {
                var bounds = new Rectangle(origin.X + column * cell.Width, weekdayTop, cell.Width, S(WeekdayHeight));
                TextRenderer.DrawText(g, WeekdayNames[column], font, bounds, GetWeekdayColor(column, MutedColor), CenterFlags);
            }

            DateTime first = FirstCellDate;
            for (int index = 0; index < DayColumns * DayRows; index++)
            {
                DateTime date = AddDaysSafe(first, index);
                if (date.Year != _cursor.Year || date.Month != _cursor.Month)
                {
                    continue;
                }

                var bounds = new Rectangle(
                    origin.X + (index % DayColumns) * cell.Width,
                    origin.Y + (index / DayColumns) * cell.Height,
                    cell.Width,
                    cell.Height);
                PaintCell(
                    g, font, bounds, date.Day.ToString(CultureInfo.InvariantCulture), date,
                    GetWeekdayColor(index % DayColumns, textColor), date == _today);
            }
        }

        private void PaintMonths(Graphics g, Font font, Color textColor)
        {
            Point origin = GridOrigin;
            Size cell = CellSize;
            var thisMonth = new DateTime(_today.Year, _today.Month, 1);

            for (int index = 0; index < MonthColumns * MonthRows; index++)
            {
                var date = new DateTime(_cursor.Year, index + 1, 1);
                var bounds = new Rectangle(
                    origin.X + (index % MonthColumns) * cell.Width,
                    origin.Y + (index / MonthColumns) * cell.Height,
                    cell.Width,
                    cell.Height);
                PaintCell(g, font, bounds, (index + 1).ToString(CultureInfo.InvariantCulture) + "月", date, textColor, date == thisMonth);
            }
        }

        private void PaintYears(Graphics g, Font font, Color textColor)
        {
            Point origin = GridOrigin;
            Size cell = CellSize;
            var thisYear = new DateTime(_today.Year, 1, 1);

            for (int index = 0; index < YearsPerPage; index++)
            {
                DateTime? date = GetYearCellDate(index);
                if (!date.HasValue)
                {
                    continue;
                }

                var bounds = new Rectangle(
                    origin.X + (index % YearColumns) * cell.Width,
                    origin.Y + (index / YearColumns) * cell.Height,
                    cell.Width,
                    cell.Height);
                PaintCell(g, font, bounds, SDateText.FormatYear(date.Value, _japanese), date.Value, textColor, date.Value == thisYear);
            }
        }

        /// <summary>
        /// 1 マス（日・月・年）を描く。指しているマスは塗り、今日（今月・今年）は枠で囲み、選べないマスは薄くする。
        /// </summary>
        private void PaintCell(Graphics g, Font font, Rectangle bounds, string text, DateTime date, Color textColor, bool isToday)
        {
            bool selectable = IsSelectable(date);
            var inner = Rectangle.Inflate(bounds, -S(2f), -S(2f));

            SmoothingMode previous = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            try
            {
                using (GraphicsPath shape = PaintHelper.CreateRoundedRectangle(inner, SControlStyle.CornerRadius * _scale))
                {
                    if (date == _cursor)
                    {
                        using (var brush = new SolidBrush(SControlStyle.SelectionBackColor))
                        using (var pen = new Pen(SControlStyle.FocusBorderColor))
                        {
                            g.FillPath(brush, shape);
                            g.DrawPath(pen, shape);
                        }
                    }
                    else if (selectable && _hover.HasValue && date == _hover.Value)
                    {
                        using (var brush = new SolidBrush(HoverColor))
                        {
                            g.FillPath(brush, shape);
                        }
                    }

                    if (isToday && date != _cursor)
                    {
                        using (var pen = new Pen(SControlStyle.BorderColor))
                        {
                            g.DrawPath(pen, shape);
                        }
                    }
                }
            }
            finally
            {
                g.SmoothingMode = previous;
            }

            Color color = selectable ? textColor : SControlStyle.GetInactiveColor(textColor, SystemColors.Window);
            if (date == _cursor && selectable)
            {
                // 指している日は、選択の色の上で読める色にする（濃い色の上では白。日曜・土曜の色も、濃い色の上では白にする）
                color = SControlStyle.GetTextColorOn(SControlStyle.SelectionBackColor, textColor);
            }
            TextRenderer.DrawText(g, text, font, bounds, color, CenterFlags);
        }

        /// <summary>曜日の色。日曜は赤、土曜は青、ほかは <paramref name="normal"/>。</summary>
        private static Color GetWeekdayColor(int column, Color normal)
        {
            return column == 0 ? SundayColor : column == 6 ? SaturdayColor : normal;
        }

        #endregion
    }
}
