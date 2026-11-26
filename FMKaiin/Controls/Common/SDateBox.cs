using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 日付の入力欄。UcDate（中身は GcDateEx）の代わりに使う。
    /// 値は <see cref="Value"/>（DateTime?）で読み書きする。未入力は null。
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>表示</b>: フォーカスが無いときは「令和 08年11月25日」「2026年11月25日」のように表示する。
    /// フォーカスがある間は、入力しやすい形（「R08/11/25」「2026/11/25」）にして編集する。
    /// 年月日・年月・年のどれを扱うかは <see cref="DateFormat"/> で、和暦・西暦は <see cref="CalendarKind"/> で決まる。
    /// </para>
    /// <para>
    /// <b>入力</b>: 日付は自由な書き方で入力でき、Enter またはフォーカスの移動で確定する（読み方は <see cref="SDateText.TryParse"/>）。
    /// 例: 2026/11/25、20261125、R8.11.25、11/25（今年）、25（今月）。日付として読めない場合は、入力する前の値に戻す。
    /// 範囲（<see cref="MinDate"/>～<see cref="MaxDate"/>）の外の日付は、範囲の端に丸める。
    /// 確定するまで <see cref="Value"/> は変わらないので、フォーカスを動かさずに値を読む処理（ファンクション キーでの保存など）は、
    /// 読む前に <see cref="CommitEdit"/> を呼ぶこと。
    /// </para>
    /// <para>
    /// <b>カレンダー</b>: 右端のボタン、Space、↑ ↓ で開く。開いている間は、矢印キーで日を、PageUp / PageDown で月を移動し、
    /// Enter またはクリックで確定する。Esc で閉じる。
    /// </para>
    /// <para>
    /// 見た目とフォーカス時の動作は <see cref="STextBox"/> と同じ（共通の土台 <see cref="SEditBase"/> による）。
    /// 既定で、文字は中央寄せ、フォーカス時に全選択、IME は無効。
    /// </para>
    /// </remarks>
    public class SDateBox : SEditBase, ISEditCommit, ISKeyUser
    {
        private const string DefaultMinDateText = "1868-01-01";
        private const string DefaultMaxDateText = "2100-12-31";

        /// <summary>カレンダーのアイコンの一辺。96 DPI 基準のピクセル。</summary>
        private const float IconSize = 16f;

        private static SDateCalendarKind _defaultCalendarKind = SDateCalendarKind.Japanese;

        private DateTime? _value;
        private DateTime _minDate = new DateTime(1868, 1, 1);
        private DateTime _maxDate = new DateTime(2100, 12, 31);
        private SDateFormat _dateFormat = SDateFormat.YearMonthDay;
        private SDateCalendarKind _calendarKind = SDateCalendarKind.Default;
        private bool _showCalendarButton = true;

        // フォーカスがあり、編集用の表示にしている間 true
        private bool _editing;
        // 編集用の表示にしてから、利用者が文字列を変えたか（変えていなければ、確定のときに値を触らない）
        private bool _edited;
        // このコントロール自身が Text を書き換えている間 true（利用者の入力と区別するため）
        private bool _settingText;

        private SCalendarPopup _popup;

        public SDateBox()
        {
            InputFilter.Format = new SInputFormat(SDateText.InputCharacters);
            InputFilter.AllowSpace = SAllowSpace.None;
            base.HighlightText = true;
            TextAlign = HorizontalAlignment.Center;
        }

        #region イベント

        /// <summary>
        /// <see cref="Value"/> が変わったときに発生する。入力の途中では発生せず、確定したとき（Enter・フォーカスの移動・カレンダーでの選択）に発生する。
        /// </summary>
        [Category("Action")]
        [Description("Value が変わったときに発生する。入力の途中では発生せず、確定したときに発生する。")]
        public event EventHandler ValueChanged;

        /// <summary>カレンダーを開いたときに発生する。</summary>
        [Category("Behavior")]
        [Description("カレンダーを開いたときに発生する。")]
        public event EventHandler DropDown;

        /// <summary>カレンダーを閉じたときに発生する。</summary>
        [Category("Behavior")]
        [Description("カレンダーを閉じたときに発生する。")]
        public event EventHandler DropDownClosed;

        #endregion

        #region 値と設定

        /// <summary>
        /// 和暦・西暦の、アプリ全体の設定。<see cref="CalendarKind"/> が Default のコントロールは、これに従う。既定は和暦。
        /// 変更しても、表示済みのコントロールは自動では変わらないので、必要なら <see cref="RefreshDisplay"/> を呼ぶこと。
        /// </summary>
        public static SDateCalendarKind DefaultCalendarKind
        {
            get { return _defaultCalendarKind; }
            set
            {
                if (value != SDateCalendarKind.Japanese && value != SDateCalendarKind.Western)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _defaultCalendarKind = value;
            }
        }

        /// <summary>
        /// 値。未入力は null。範囲（<see cref="MinDate"/>～<see cref="MaxDate"/>）の外の値を設定すると、範囲の端に丸める。
        /// 設定した値は、時刻や（年月の形での）日も含めてそのまま持つ。利用者が入力した値は、時刻なしになる。
        /// </summary>
        [Category("Data")]
        [DefaultValue(null)]
        [Description("値。未入力は null。")]
        public DateTime? Value
        {
            get { return _value; }
            set
            {
                DateTime? clamped = value.HasValue ? Clamp(value.Value) : (DateTime?)null;
                bool changed = _value != clamped;
                _value = clamped;
                _edited = false;
                RefreshText();
                if (changed)
                {
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>入力できる最初の日。</summary>
        [Category("Behavior")]
        [DefaultValue(typeof(DateTime), DefaultMinDateText)]
        [Description("入力できる最初の日。")]
        public DateTime MinDate
        {
            get { return _minDate; }
            set
            {
                _minDate = value.Date;
                if (_maxDate < _minDate)
                {
                    _maxDate = _minDate;
                }
                Value = _value;
            }
        }

        /// <summary>入力できる最後の日。</summary>
        [Category("Behavior")]
        [DefaultValue(typeof(DateTime), DefaultMaxDateText)]
        [Description("入力できる最後の日。")]
        public DateTime MaxDate
        {
            get { return _maxDate; }
            set
            {
                _maxDate = value.Date;
                if (_minDate > _maxDate)
                {
                    _minDate = _maxDate;
                }
                Value = _value;
            }
        }

        /// <summary>
        /// 扱う日付の形（年月日・年月・年）。年月・年の形では、カレンダーが月を選ぶ表示になる。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(SDateFormat.YearMonthDay)]
        [Description("扱う日付の形（年月日・年月・年）。年月・年の形では、カレンダーが月を選ぶ表示になる。")]
        public SDateFormat DateFormat
        {
            get { return _dateFormat; }
            set
            {
                if (!Enum.IsDefined(typeof(SDateFormat), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(SDateFormat));
                }
                _dateFormat = value;
                ClosePopup();
                RefreshText();
            }
        }

        /// <summary>
        /// 和暦・西暦のどちらで表示するか。Default なら、アプリ全体の設定（<see cref="DefaultCalendarKind"/>）に従う。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(SDateCalendarKind.Default)]
        [Description("和暦・西暦のどちらで表示するか。Default なら、アプリ全体の設定に従う。")]
        public SDateCalendarKind CalendarKind
        {
            get { return _calendarKind; }
            set
            {
                if (!Enum.IsDefined(typeof(SDateCalendarKind), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(SDateCalendarKind));
                }
                _calendarKind = value;
                ClosePopup();
                RefreshText();
            }
        }

        /// <summary>
        /// 右端にカレンダーのボタンを表示するか。false にすると、カレンダーを使わない（キーでも開かない）。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("右端にカレンダーのボタンを表示するか。false にすると、カレンダーを使わない（キーでも開かない）。")]
        public bool ShowCalendarButton
        {
            get { return _showCalendarButton; }
            set
            {
                if (_showCalendarButton != value)
                {
                    _showCalendarButton = value;
                    ClosePopup();
                    UpdateFrameLayout();
                }
            }
        }

        /// <summary>
        /// いまの設定（和暦・西暦など）で、表示を作り直す。<see cref="DefaultCalendarKind"/> を変えた後に呼ぶ。
        /// </summary>
        public void RefreshDisplay()
        {
            if (!_edited)
            {
                RefreshText();
            }
        }

        /// <summary>
        /// 文字列。取得すると、いま表示している文字列を返す。設定すると、日付として読み取って <see cref="Value"/> に入れる
        /// （日付として読めなければ未入力）。値の読み書きには、このプロパティではなく <see cref="Value"/> を使うこと。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return base.Text; }
            set
            {
                if (_settingText)
                {
                    base.Text = value;
                    return;
                }

                DateTime parsed;
                Value = SDateText.TryParse(value, _dateFormat, IsJapanese, DateTime.Today, out parsed) ? parsed : (DateTime?)null;
            }
        }

        /// <summary>フォーカスが移ってきたときに、文字列を全選択するか。日付は入力し直すことが多いので、既定を true にしている。</summary>
        [DefaultValue(true)]
        public override bool HighlightText
        {
            get { return base.HighlightText; }
            set { base.HighlightText = value; }
        }

        /// <summary>IME の既定の状態。日付の入力では使わないので、無効にする。</summary>
        protected override ImeMode DefaultImeMode
        {
            get { return ImeMode.Disable; }
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            EventHandler handler = ValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>いま、和暦で表示するか。</summary>
        private bool IsJapanese
        {
            get
            {
                SDateCalendarKind kind = _calendarKind == SDateCalendarKind.Default ? _defaultCalendarKind : _calendarKind;
                return kind == SDateCalendarKind.Japanese;
            }
        }

        /// <summary>カレンダーの表示の種類。年月の形なら月を選ぶ表示、年の形なら年を選ぶ表示、それ以外は日を選ぶ表示。</summary>
        private SCalendarMode CalendarMode
        {
            get
            {
                switch (_dateFormat)
                {
                    case SDateFormat.Year:
                        return SCalendarMode.Year;
                    case SDateFormat.YearMonth:
                        return SCalendarMode.Month;
                    default:
                        return SCalendarMode.Day;
                }
            }
        }

        private DateTime Clamp(DateTime date)
        {
            if (date.Date < _minDate)
            {
                return _minDate;
            }
            return date.Date > _maxDate ? _maxDate : date;
        }

        #endregion

        #region 表示の切り替えと確定

        protected override void OnEnter(EventArgs e)
        {
            // 編集用の表示に切り替えてから、標準の処理（全選択など）を行う
            if (!ReadOnly)
            {
                _editing = true;
                _edited = false;
                RefreshText();
            }
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            // ほかのコントロールの Validating より前に、値を確定して表示用の形に戻す
            ClosePopup();
            if (_editing)
            {
                CommitEdit();
                _editing = false;
                RefreshText();
            }
            base.OnLeave(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            // ほかのアプリへ切り替えた場合など。入力の途中の文字列は残し、カレンダーだけ閉じる
            ClosePopup();
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            base.OnReadOnlyChanged(e);

            // 読み取り専用にされたら、編集を終えて表示用の形に戻す。フォーカスがあるまま解除されたら、編集用の形にする
            bool editing = !ReadOnly && Focused;
            if (ReadOnly)
            {
                ClosePopup();
            }
            if (_editing != editing)
            {
                _editing = editing;
                _edited = false;
                RefreshText();
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (!Enabled)
            {
                ClosePopup();
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (!Visible)
            {
                ClosePopup();
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ClosePopup();
            base.OnHandleDestroyed(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            // 利用者が編集した（入力・削除・貼り付け）
            if (_editing && !_settingText)
            {
                _edited = true;
                ClosePopup();
            }
            base.OnTextChanged(e);
        }

        /// <summary>
        /// 入力の途中の文字列を日付として読み取り、値（<see cref="Value"/>）にする。Enter を押したとき・フォーカスが移ったときと同じ処理。
        /// 利用者が文字列を変えていなければ、何もしない。日付として読めない場合は、入力する前の値に戻す。
        /// </summary>
        /// <remarks>
        /// フォーカスを動かさずに値を読む処理（ファンクション キーでの保存など）が、読む前に呼ぶ。
        /// </remarks>
        public void CommitEdit()
        {
            if (!_editing || !_edited)
            {
                return;
            }

            string text = base.Text;
            DateTime parsed;
            if (text.Trim().Length == 0)
            {
                Value = null;
            }
            else if (SDateText.TryParse(text, _dateFormat, IsJapanese, DateTime.Today, out parsed))
            {
                Value = parsed;
            }
            else
            {
                OnInvalidInput(EventArgs.Empty);
                _edited = false;
                RefreshText();
            }
        }

        /// <summary>いまの値を、状態に合った形（編集用・表示用）で入力欄に表示する。</summary>
        private void RefreshText()
        {
            string text = !_value.HasValue ? string.Empty
                : _editing ? SDateText.FormatForEdit(_value.Value, _dateFormat, IsJapanese)
                : SDateText.FormatForDisplay(_value.Value, _dateFormat, IsJapanese);
            if (base.Text == text)
            {
                return;
            }

            _settingText = true;
            try
            {
                base.Text = text;
            }
            finally
            {
                _settingText = false;
            }
        }

        #endregion

        #region カレンダー

        /// <summary>
        /// カレンダーを開いているか。設定して開閉することもできる（読み取り専用・無効のとき、
        /// <see cref="ShowCalendarButton"/> が false のときは開かない）。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DroppedDown
        {
            get { return IsPopupOpen; }
            set
            {
                if (value)
                {
                    OpenPopup();
                }
                else
                {
                    ClosePopup();
                }
            }
        }

        private bool IsPopupOpen
        {
            get { return _popup != null && !_popup.IsDisposed && _popup.Visible; }
        }

        private void OpenPopup()
        {
            if (IsPopupOpen || !_showCalendarButton || !IsHandleCreated || !Visible || !Enabled || ReadOnly || DesignMode)
            {
                return;
            }

            // 入力の途中の文字列があれば、先に確定する（読めなければ元の値に戻る）
            CommitEdit();

            if (_popup == null || _popup.IsDisposed)
            {
                _popup = new SCalendarPopup(this);
                _popup.CloseRequested += delegate { ClosePopup(); };
                _popup.PopupClosed += delegate { OnPopupClosed(); };
                _popup.DateChosen += delegate { ChooseDate(_popup.CursorDate); };
            }

            _popup.ShowCalendar(CalendarMode, IsJapanese, _value ?? DateTime.Today, _minDate, _maxDate);

            EventHandler handler = DropDown;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void ClosePopup()
        {
            if (IsPopupOpen)
            {
                _popup.Hide();
            }
        }

        private void OnPopupClosed()
        {
            EventHandler handler = DropDownClosed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>カレンダーで選ばれた日を値にして、カレンダーを閉じる。</summary>
        private void ChooseDate(DateTime date)
        {
            ClosePopup();
            Value = date;
            if (Focused)
            {
                SelectAll();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Handled || ReadOnly)
            {
                return;
            }

            if (IsPopupOpen)
            {
                HandlePopupKey(e);
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    // 入力を確定する。Enter そのものは止めない（フォームが次の項目への移動などに使える）。
                    // フォーカスが残る場合に続けて入力し直せるよう、全選択にしておく
                    CommitEdit();
                    SelectAll();
                    break;

                case Keys.Escape:
                    if (_edited)
                    {
                        // 入力を取り消して、元の値に戻す
                        _edited = false;
                        RefreshText();
                        SelectAll();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;

                case Keys.Space:
                case Keys.Up:
                case Keys.Down:
                    if (_showCalendarButton && (e.Modifiers == Keys.None || e.Modifiers == Keys.Alt))
                    {
                        OpenPopup();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// いまの状態で、<paramref name="keyCode"/> のキーをこのコントロールが使うか（<see cref="ISKeyUser"/>）。
        /// カレンダーを開いている間は、Enter（指している日の確定）・Esc・矢印・PageUp / PageDown・Space を使う。
        /// フォームは、これらのキーを先に処理しないこと（Enter で次の項目へ移る処理など）。
        /// </summary>
        public bool UsesKey(Keys keyCode)
        {
            if (!IsPopupOpen)
            {
                return false;
            }

            switch (keyCode)
            {
                case Keys.Enter:
                case Keys.Escape:
                case Keys.Space:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.PageUp:
                case Keys.PageDown:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>カレンダーを開いている間のキー操作。</summary>
        private void HandlePopupKey(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    _popup.MoveCursorByArrow(-1, 0);
                    break;
                case Keys.Right:
                    _popup.MoveCursorByArrow(1, 0);
                    break;
                case Keys.Up:
                    if (e.Alt)
                    {
                        ClosePopup();
                    }
                    else
                    {
                        _popup.MoveCursorByArrow(0, -1);
                    }
                    break;
                case Keys.Down:
                    if (e.Alt)
                    {
                        ClosePopup();
                    }
                    else
                    {
                        _popup.MoveCursorByArrow(0, 1);
                    }
                    break;
                case Keys.PageUp:
                    _popup.MoveCursorByPage(-1);
                    break;
                case Keys.PageDown:
                    _popup.MoveCursorByPage(1);
                    break;
                case Keys.Enter:
                    ChooseDate(_popup.CursorDate);
                    break;
                case Keys.Escape:
                case Keys.Space:
                    ClosePopup();
                    break;
                default:
                    // そのほかのキー（数字など）は、入力欄の通常の処理に任せる。文字が入力されるとカレンダーは閉じる
                    return;
            }

            // カレンダーの操作に使ったキーを、入力欄やフォームの処理に渡さない
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _popup != null)
            {
                _popup.Dispose();
                _popup = null;
            }
            base.Dispose(disposing);
        }

        #endregion

        #region カレンダーのボタン（サイド ボタン）

        protected override int GetSideButtonWidth(float scale)
        {
            return _showCalendarButton ? SControlStyle.GetDropDownButtonWidth(scale) : 0;
        }

        protected override void PaintSideButton(Graphics g, Rectangle bounds, Color backColor)
        {
            // カレンダーを開けないときは、アイコンを薄くする
            Color color = Enabled && !ReadOnly ? ForeColor : SControlStyle.GetInactiveColor(ForeColor, backColor);

            float size = IconSize * LayoutScale.Of(this);
            var iconBounds = new RectangleF(
                bounds.Left + (bounds.Width - size) / 2f,
                bounds.Top + (bounds.Height - size) / 2f,
                size,
                size);
            MenuIconPainter.Draw(g, MenuIconKind.Calendar, iconBounds, color);
        }

        protected override void OnSideButtonClick(EventArgs e)
        {
            if (!ReadOnly)
            {
                DroppedDown = !IsPopupOpen;
            }
        }

        #endregion
    }
}
