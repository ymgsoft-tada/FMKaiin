using System;
using System.Globalization;
using System.Windows.Forms;
using App.Controls;
using ComponentDebug;
using ComponentIO;

namespace App
{
    /// <summary>
    /// <see cref="SControlDBItem"/> のフック（<see cref="SControlDBItem.FetchRunEx"/> など）が返す、特別な値。
    /// </summary>
    public enum SControlDBEnum
    {
        /// <summary>
        /// 何も変えない。読み込みのフックが返すとコントロールを書き換えず、書き戻しのフックが返すと行を書き換えない
        /// （フックの中で自分で処理した場合に使う）。
        /// </summary>
        NoChange,
    }

    /// <summary>
    /// 読み込み・書き戻しのときに値を変換するフック。
    /// </summary>
    /// <param name="row">対象の行。</param>
    /// <param name="data">変換前の値（読み込みでは行の値、書き戻しではコントロールの値）。</param>
    /// <returns>変換後の値。何も変えない場合は <see cref="SControlDBEnum.NoChange"/>。</returns>
    public delegate object SControlDBRunEx(ISRow row, object data);

    /// <summary>
    /// 行の値を書き換えたときの通知。
    /// </summary>
    /// <param name="row">書き換えた行。</param>
    /// <param name="field">書き換えた項目名。</param>
    /// <param name="value">書き込んだ値。</param>
    public delegate void SControlDBFieldUpdatedHandler(ISRow row, string field, object value);

    /// <summary>
    /// 行の項目と、画面のコントロールの対応 1 つ分。<see cref="SControlDB.Add"/> で登録する。
    /// 値の受け渡し方（文字列・数値・日付など）ごとに、継承したクラスがある。GControlDBBase に当たる。
    /// </summary>
    public abstract class SControlDBItem
    {
        private readonly string[] _rowFields;
        private readonly Control[] _controls;

        /// <param name="rowFields">行の項目名。</param>
        /// <param name="controls">対応するコントロール。</param>
        protected SControlDBItem(string[] rowFields, Control[] controls)
        {
            if (rowFields == null || rowFields.Length == 0)
            {
                throw new ArgumentException("項目名を 1 つ以上指定してください。", nameof(rowFields));
            }
            if (controls == null || controls.Length == 0 || Array.IndexOf(controls, null) >= 0)
            {
                throw new ArgumentException("コントロールを 1 つ以上指定してください（null は指定できません）。", nameof(controls));
            }

            _rowFields = (string[])rowFields.Clone();
            _controls = (Control[])controls.Clone();
            SameFieldFetch = true;
        }

        /// <summary>行の項目名。</summary>
        public string[] RowFields
        {
            get { return _rowFields; }
        }

        /// <summary>対応するコントロール。</summary>
        public Control[] Ctls
        {
            get { return _controls; }
        }

        /// <summary>
        /// 読み込みのときに値を変換するフック。行の値と、コントロールに表示する値が違う場合に使う。
        /// null を返すと空欄になる。
        /// </summary>
        public SControlDBRunEx FetchRunEx;

        /// <summary>書き戻しのときに値を変換するフック。</summary>
        public SControlDBRunEx UpdateRunEx;

        /// <summary>行の値を書き換えたときの通知。値が変わらなかった場合は呼ばれない。</summary>
        public SControlDBFieldUpdatedHandler FieldUpdated;

        /// <summary>
        /// 書き戻した後に、同じ項目を見ているほかのコントロールも読み込み直すか。既定は true。
        /// </summary>
        public bool SameFieldFetch;

        /// <summary>
        /// 行の値を、コントロールに表示する。行が無い（null の）場合は、値が無いものとして表示する。
        /// </summary>
        public abstract void Fetch(ISRow row);

        /// <summary>
        /// コントロールの値を、行に書き戻す。
        /// </summary>
        /// <param name="db">この対応を登録した <see cref="SControlDB"/>。</param>
        /// <param name="row">書き戻す先の行。</param>
        /// <param name="activeControl">利用者が操作したコントロール（書き戻しのきっかけになったもの）。</param>
        public abstract void Update(SControlDB db, ISRow row, Control activeControl);

        /// <summary>
        /// コントロールを空にする（<see cref="SControlDB.ClearAll"/> から呼ばれる）。
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// <see cref="Ctls"/> の <paramref name="controlIndex"/> 番目のコントロールが、<see cref="RowFields"/> の何番目の項目に当たるかを返す。
        /// 既定では、項目とコントロールが同じ数なら同じ番号、項目が 1 つなら 0。数の対応が違うクラスは、これを書き換える。
        /// </summary>
        protected internal virtual int GetFieldIndex(int controlIndex)
        {
            return _rowFields.Length == _controls.Length ? controlIndex : 0;
        }

        /// <summary>
        /// <see cref="RowFields"/> の <paramref name="fieldIndex"/> 番目の項目に当たる、最初のコントロールが、<see cref="Ctls"/> の何番目かを返す。
        /// </summary>
        protected internal virtual int GetControlIndex(int fieldIndex)
        {
            return _rowFields.Length == _controls.Length ? fieldIndex : 0;
        }

        /// <summary>
        /// 登録が終わった（<see cref="SControlDB.EndAdd()"/>）ときに、1 回だけ呼ばれる。入力ルールは、設定し終えている。
        /// 登録先の <see cref="SControlDB"/> を使う準備（入力の最中に動く処理の用意など）が要るクラスは、これを書き換える。
        /// </summary>
        /// <param name="db">この対応を登録した <see cref="SControlDB"/>。</param>
        protected internal virtual void OnEndAdd(SControlDB db)
        {
        }

        #region 継承したクラスが使う処理

        /// <summary>
        /// 行から、<paramref name="index"/> 番目の項目の値を取得する。フックがあれば変換する。
        /// 行が無い・項目が無い・値が無い場合は <see cref="DBNull.Value"/>。フックが「何も変えない」を返した場合は null。
        /// </summary>
        protected object GetFieldValue(ISRow row, int index)
        {
            if (row == null)
            {
                return DBNull.Value;
            }

            string field = _rowFields[index];
            if (!row.Contains(field))
            {
                ErrLog.WriteLine("指定の項目は存在しませんでした。 '" + field + "'");
                return DBNull.Value;
            }

            object value = row[field];
            if (FetchRunEx != null)
            {
                value = FetchRunEx(row, value);
                if (IsNoChange(value))
                {
                    return null;
                }
                if (value == null)
                {
                    return DBNull.Value;
                }
            }
            return value;
        }

        /// <summary>コントロールの値を、書き戻しのフックで変換する。</summary>
        protected object ConvertControlValue(ISRow row, object value)
        {
            return UpdateRunEx != null ? UpdateRunEx(row, value) : value;
        }

        /// <summary>
        /// <paramref name="index"/> 番目の項目に <paramref name="value"/> を書き込む。いまの値と同じなら書き込まない。
        /// 書き込んだ場合は <see cref="FieldUpdated"/> で知らせる。
        /// </summary>
        protected void UpdateRowValue(ISRow row, int index, object value)
        {
            if (IsNoChange(value))
            {
                // フックの中で処理済み
                return;
            }

            string field = _rowFields[index];
            object newValue = value ?? DBNull.Value;
            if (!ValuesDiffer(row[field], newValue))
            {
                return;
            }

            try
            {
                row[field] = newValue;
            }
            catch (ArgumentException ex)
            {
                // 項目の型に合わない値（数値の項目に文字列、など）。値なしにする
                ErrLog.WriteException(ex);
                row[field] = DBNull.Value;
            }

            if (FieldUpdated != null)
            {
                FieldUpdated(row, field, newValue);
            }
        }

        /// <summary>
        /// 書き戻しの前に、行と項目がそろっているかを確かめる。
        /// </summary>
        protected bool CanUpdate(ISRow row)
        {
            if (row == null)
            {
                ErrLog.WriteLine("書き戻す先の行がありません。");
                return false;
            }

            foreach (string field in _rowFields)
            {
                if (!row.Contains(field))
                {
                    ErrLog.WriteLine("指定の項目は存在しません。 '" + field + "'");
                    return false;
                }
            }
            return true;
        }

        /// <summary>値を、表示用の文字列にする。値が無ければ空文字列。</summary>
        protected static string ToText(object value)
        {
            return value == null || value == DBNull.Value ? string.Empty : Convert.ToString(value);
        }

        /// <summary>空文字列を「値なし」として扱う（行には <see cref="DBNull.Value"/> が入る）。</summary>
        protected static object EmptyToNull(string text)
        {
            return string.IsNullOrEmpty(text) ? null : text;
        }

        private static bool IsNoChange(object value)
        {
            return value is SControlDBEnum && (SControlDBEnum)value == SControlDBEnum.NoChange;
        }

        /// <summary>
        /// 2 つの値が違うか。null と <see cref="DBNull.Value"/> は同じとみなす。
        /// 数値（decimal）は数値として、日付は日付として比べ、それ以外は文字列にして比べる。
        /// </summary>
        private static bool ValuesDiffer(object current, object value)
        {
            bool currentEmpty = current == null || current == DBNull.Value;
            bool valueEmpty = value == null || value == DBNull.Value;
            if (currentEmpty || valueEmpty)
            {
                return currentEmpty != valueEmpty;
            }

            if (current is decimal)
            {
                // 文字列で比べると「0.0000」と「0」が違うことになるので、数値として比べる
                decimal number;
                return !decimal.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), NumberStyles.Number, CultureInfo.InvariantCulture, out number)
                    || number != (decimal)current;
            }

            if (current is DateTime)
            {
                // 日付は文字列にして書き込むので、文字列で比べると、同じ日付でも「違う」ことになってしまう
                //（時刻の付き方が違うため）。日付として比べる
                DateTime date;
                if (value is DateTime)
                {
                    date = (DateTime)value;
                }
                else if (!DateTime.TryParse(value.ToString(), out date))
                {
                    return true;
                }
                return date != (DateTime)current;
            }

            return current.ToString() != value.ToString();
        }

        #endregion
    }

    /// <summary>
    /// 文字列の項目と <see cref="STextBox"/> の対応。GControlDBText に当たる。
    /// </summary>
    public class SControlDBText : SControlDBItem
    {
        private readonly STextBox _textBox;

        public SControlDBText(string rowField, STextBox control)
            : base(new[] { rowField }, new Control[] { control })
        {
            _textBox = control;
        }

        public override void Fetch(ISRow row)
        {
            object value = GetFieldValue(row, 0);
            if (value != null)
            {
                _textBox.Text = ToText(value);
            }
        }

        public override void Update(SControlDB db, ISRow row, Control activeControl)
        {
            if (!CanUpdate(row))
            {
                return;
            }

            string text = _textBox.Text;
            if (!_textBox.Multiline)
            {
                // 1 行の入力欄に改行が入っていたら取り除く
                text = text.Replace("\r\n", string.Empty);
            }
            UpdateRowValue(row, 0, ConvertControlValue(row, EmptyToNull(text)));
        }

        public override void Clear()
        {
            _textBox.Text = string.Empty;
        }
    }

    /// <summary>
    /// 数値の項目と、<see cref="SNumberBox"/> または <see cref="STextBox"/> の対応。GControlDBNumber に当たる。
    /// </summary>
    public class SControlDBNumber : SControlDBItem
    {
        private readonly SNumberBox _numberBox;
        private readonly STextBox _textBox;

        public SControlDBNumber(string rowField, SNumberBox control)
            : base(new[] { rowField }, new Control[] { control })
        {
            _numberBox = control;
        }

        /// <summary>
        /// 数値の項目を、通常の入力欄（数字だけを入力できるようにした <see cref="STextBox"/>）に表示する場合。
        /// </summary>
        public SControlDBNumber(string rowField, STextBox control)
            : base(new[] { rowField }, new Control[] { control })
        {
            _textBox = control;
        }

        public override void Fetch(ISRow row)
        {
            object value = GetFieldValue(row, 0);
            if (value == null)
            {
                return;
            }

            // 値なし・数値として読めない値は、空欄にする
            decimal number = 0m;
            bool hasNumber = value != DBNull.Value
                && decimal.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), NumberStyles.Number, CultureInfo.InvariantCulture, out number);

            if (_numberBox != null)
            {
                _numberBox.Value = hasNumber ? number : (decimal?)null;
            }
            else
            {
                _textBox.Text = hasNumber ? number.ToString(CultureInfo.InvariantCulture) : string.Empty;
            }
        }

        public override void Update(SControlDB db, ISRow row, Control activeControl)
        {
            if (!CanUpdate(row))
            {
                return;
            }

            object value;
            if (_numberBox != null)
            {
                value = _numberBox.Value;
            }
            else
            {
                // 数値として読めなければ、値なしにする
                decimal number;
                string text = _textBox.Text.Replace(",", string.Empty).Replace("\\", string.Empty);
                value = decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out number) ? (object)text : null;
            }
            UpdateRowValue(row, 0, ConvertControlValue(row, value));
        }

        public override void Clear()
        {
            if (_numberBox != null)
            {
                _numberBox.Value = null;
            }
            else
            {
                _textBox.Text = string.Empty;
            }
        }
    }

    /// <summary>
    /// 日付の項目と <see cref="SDateBox"/> の対応。GControlDBDate に当たる。
    /// </summary>
    public class SControlDBDate : SControlDBItem
    {
        /// <summary>行に書き込む値の形。</summary>
        public enum DateType
        {
            /// <summary>日付だけ（時刻なし）。</summary>
            Date,
            /// <summary>日付と時刻。</summary>
            DateTime,
        }

        private readonly SDateBox _dateBox;
        private readonly DateType _dateType;

        public SControlDBDate(string rowField, SDateBox control)
            : this(rowField, control, DateType.Date)
        {
        }

        public SControlDBDate(string rowField, SDateBox control, DateType dateType)
            : base(new[] { rowField }, new Control[] { control })
        {
            _dateBox = control;
            _dateType = dateType;
        }

        public override void Fetch(ISRow row)
        {
            object value = GetFieldValue(row, 0);
            if (value == null)
            {
                return;
            }

            DateTime date;
            if (value is DateTime)
            {
                _dateBox.Value = (DateTime)value;
            }
            else if (value != DBNull.Value && DateTime.TryParse(value.ToString(), out date))
            {
                _dateBox.Value = date;
            }
            else
            {
                _dateBox.Value = null;
            }
        }

        public override void Update(SControlDB db, ISRow row, Control activeControl)
        {
            if (!CanUpdate(row))
            {
                return;
            }

            // GControlDB と同じく、文字列にして書き込む（項目が日付型なら、行の側で日付に変換される）
            object value = null;
            if (_dateBox.Value.HasValue)
            {
                DateTime date = _dateBox.Value.Value;
                value = _dateType == DateType.DateTime ? date.ToString() : date.ToShortDateString();
            }
            UpdateRowValue(row, 0, ConvertControlValue(row, value));
        }

        public override void Clear()
        {
            _dateBox.Value = null;
        }
    }

    /// <summary>
    /// 値で選ぶ項目と、<see cref="SComboBox"/> または <see cref="STableComboBox"/> の対応。GControlDBCombo に当たる。
    /// 行の値と、コンボ ボックスの項目の値（SelectedValue）を対応させる。
    /// </summary>
    public class SControlDBCombo : SControlDBItem
    {
        private readonly SComboBox _comboBox;
        private readonly STableComboBox _tableComboBox;

        public SControlDBCombo(string rowField, SComboBox control)
            : base(new[] { rowField }, new Control[] { control })
        {
            _comboBox = control;
        }

        public SControlDBCombo(string rowField, STableComboBox control)
            : base(new[] { rowField }, new Control[] { control })
        {
            _tableComboBox = control;
        }

        public override void Fetch(ISRow row)
        {
            object value = GetFieldValue(row, 0);
            if (value == null)
            {
                return;
            }

            // 該当する項目が無い値・値なしの場合は、未選択になる
            if (_comboBox != null)
            {
                _comboBox.SelectByValue(value == DBNull.Value ? null : value);
            }
            else
            {
                _tableComboBox.SelectByValue(value);
            }
        }

        public override void Update(SControlDB db, ISRow row, Control activeControl)
        {
            if (!CanUpdate(row))
            {
                return;
            }

            object value = _comboBox != null ? _comboBox.SelectedValue : _tableComboBox.SelectedValue;
            UpdateRowValue(row, 0, ConvertControlValue(row, value));
        }

        public override void Clear()
        {
            // GControlDB と同じく、項目があれば先頭を選択する
            if (_comboBox != null)
            {
                if (_comboBox.Items.Count > 0)
                {
                    _comboBox.SelectedIndex = 0;
                }
            }
            else if (_tableComboBox.RowCount > 0)
            {
                _tableComboBox.SelectedIndex = 0;
            }
        }
    }

    /// <summary>
    /// 真偽の項目と <see cref="CheckBox"/> の対応。GControlDBCheckBox に当たる。
    /// 行の値は、真偽値のほか、数値（0 以外をオン）も読める。書き戻す値は真偽値。
    /// </summary>
    public class SControlDBCheckBox : SControlDBItem
    {
        private readonly CheckBox _checkBox;

        public SControlDBCheckBox(string rowField, CheckBox control)
            : base(new[] { rowField }, new Control[] { control })
        {
            _checkBox = control;
        }

        public override void Fetch(ISRow row)
        {
            object value = GetFieldValue(row, 0);
            if (value == null)
            {
                return;
            }

            int number;
            if (value is bool)
            {
                _checkBox.Checked = (bool)value;
            }
            else
            {
                _checkBox.Checked = int.TryParse(value.ToString(), out number) && number != 0;
            }
        }

        public override void Update(SControlDB db, ISRow row, Control activeControl)
        {
            if (CanUpdate(row))
            {
                UpdateRowValue(row, 0, ConvertControlValue(row, _checkBox.Checked));
            }
        }

        public override void Clear()
        {
            _checkBox.Checked = false;
        }
    }

    /// <summary>
    /// 名称とふりがなの 2 項目と、2 つの <see cref="STextBox"/> の対応。GControlDBRuby に当たる。
    /// 名称を入力すると、IME で入力したときの読みを、ふりがなの欄の末尾へ追記する。名称を全部消すと、ふりがなも空にする。
    /// どちらも、名称が書き換わるたびに、その場で行う（名称の欄を離れるのを待たない）。
    /// </summary>
    public class SControlDBRuby : SControlDBItem
    {
        private readonly STextBox _nameBox;
        private readonly STextBox _rubyBox;

        // Fetch・Clear で、プログラムから入力欄を書き換えている最中か（この間は、ふりがなを自動で書き換えない）
        private bool _settingControls;

        /// <param name="rowFields">名称の項目、ふりがなの項目の順に 2 つ。</param>
        /// <param name="controls">名称の入力欄、ふりがなの入力欄の順に 2 つ。</param>
        public SControlDBRuby(string[] rowFields, STextBox[] controls)
            : base(rowFields, controls)
        {
            if (rowFields.Length != 2 || controls.Length != 2)
            {
                throw new ArgumentException("項目とコントロールを、名称・ふりがなの順に 2 つずつ指定してください。");
            }
            _nameBox = controls[0];
            _rubyBox = controls[1];

            _nameBox.TextChanged += OnNameTextChanged;
        }

        /// <summary>ふりがなを全角カタカナにして入れるか。既定は false（IME が返した読みのまま）。</summary>
        public bool RubyZenkaku { get; set; }

        public override void Fetch(ISRow row)
        {
            object name = GetFieldValue(row, 0);
            if (name == null)
            {
                return;
            }

            _settingControls = true;
            try
            {
                _nameBox.Text = ToText(name);

                object ruby = GetFieldValue(row, 1);
                if (ruby != null)
                {
                    _rubyBox.Text = ToText(ruby);
                }
            }
            finally
            {
                _settingControls = false;
            }
        }

        /// <summary>
        /// 名称が書き換わるたびに、ふりがなを合わせる。利用者が名称の欄で入力しているときだけ行う。
        /// </summary>
        /// <remarks>
        /// IME で確定すると、先に読みが名称の欄にたまり、その後で文字が 1 文字ずつ入る。
        /// 最初の 1 文字が入った時点で、たまっている読みをまとめてふりがなへ移す。
        /// </remarks>
        private void OnNameTextChanged(object sender, EventArgs e)
        {
            if (_settingControls || !_nameBox.Focused || _nameBox.ReadOnly)
            {
                return;
            }
            AppendRuby(_nameBox, _rubyBox, RubyZenkaku);
        }

        public override void Update(SControlDB db, ISRow row, Control activeControl)
        {
            if (!CanUpdate(row))
            {
                return;
            }

            UpdateRowValue(row, 0, ConvertControlValue(row, EmptyToNull(_nameBox.Text)));

            // 名称を書き換えたときだけ、ふりがなを自動で入れる。入力のたびに反映しているので、ここで足すのは、
            // まだ反映していない読みが残っている場合だけ（確定した文字が、入力規則で 1 文字も入らなかったときなど）
            if (activeControl == _nameBox)
            {
                AppendRuby(_nameBox, _rubyBox, RubyZenkaku);
            }
            UpdateRowValue(row, 1, EmptyToNull(_rubyBox.Text));
        }

        public override void Clear()
        {
            _settingControls = true;
            try
            {
                _nameBox.Text = string.Empty;
                _rubyBox.Text = string.Empty;
            }
            finally
            {
                _settingControls = false;
            }
        }

        /// <summary>
        /// <paramref name="source"/> に入力したときの読みを、<paramref name="ruby"/> の後ろへ追記する。
        /// <paramref name="source"/> が空なら、<paramref name="ruby"/> も空にする。
        /// </summary>
        internal static void AppendRuby(STextBox source, STextBox ruby, bool zenkaku)
        {
            if (source.TextLength == 0)
            {
                source.TakeImeReading();
                ruby.Text = string.Empty;
                return;
            }

            string reading = source.TakeImeReading();
            if (zenkaku)
            {
                reading = StrConv.ToWideKana(reading);
            }
            if (!string.IsNullOrEmpty(reading))
            {
                ruby.Text += reading;
            }
        }
    }

    /// <summary>
    /// ハイフンで区切った 1 つの項目（電話番号・郵便番号など）と、区切りごとの <see cref="STextBox"/> の対応。
    /// GControlDBHyphenSplit に当たる。
    /// </summary>
    public class SControlDBHyphenSplit : SControlDBItem
    {
        private readonly STextBox[] _boxes;

        /// <param name="controls">区切りごとの入力欄。左から順に指定する。</param>
        public SControlDBHyphenSplit(string rowField, STextBox[] controls)
            : base(new[] { rowField }, controls)
        {
            _boxes = (STextBox[])controls.Clone();
        }

        /// <summary>
        /// 区切りの数が入力欄より少ないときに、左の欄から埋めるか。既定は false（右の欄から埋める）。
        /// </summary>
        public bool DataFillLeft { get; set; }

        public override void Fetch(ISRow row)
        {
            object value = GetFieldValue(row, 0);
            if (value != null)
            {
                SplitInto(_boxes, _boxes.Length, ToText(value), DataFillLeft);
            }
        }

        public override void Update(SControlDB db, ISRow row, Control activeControl)
        {
            if (CanUpdate(row))
            {
                UpdateRowValue(row, 0, ConvertControlValue(row, Join(_boxes, _boxes.Length)));
            }
        }

        public override void Clear()
        {
            foreach (STextBox box in _boxes)
            {
                box.Text = string.Empty;
            }
        }

        /// <summary>
        /// <paramref name="text"/> をハイフンで分けて、<paramref name="boxes"/> の先頭から <paramref name="count"/> 個の欄に入れる。
        /// </summary>
        internal static void SplitInto(STextBox[] boxes, int count, string text, bool fillLeft)
        {
            string[] parts = text.Split('-');
            int used = Math.Min(parts.Length, count);

            // 右から埋める場合は、余った左の欄を空にする
            int offset = fillLeft ? 0 : count - used;
            for (int i = 0; i < count; i++)
            {
                int partIndex = i - offset;
                boxes[i].Text = partIndex >= 0 && partIndex < used ? parts[partIndex] : string.Empty;
            }
        }

        /// <summary>
        /// <paramref name="boxes"/> の先頭から <paramref name="count"/> 個の欄を、ハイフンでつなぐ。すべて空なら空文字列。
        /// </summary>
        internal static string Join(STextBox[] boxes, int count)
        {
            var parts = new string[count];
            bool any = false;
            for (int i = 0; i < count; i++)
            {
                parts[i] = boxes[i].Text;
                any |= parts[i].Length > 0;
            }
            return any ? string.Join("-", parts) : string.Empty;
        }
    }

    /// <summary>
    /// 郵便番号・住所（・住所のふりがな）の項目と、入力欄の対応。GControlDBPostAddr に当たる。
    /// 郵便番号を入力すると、住所の欄が空のときに、郵便番号から住所を自動で入れる。
    /// </summary>
    /// <remarks>
    /// 住所を入れるのは、次の 2 つの場面。どちらも、住所の欄が空のときだけ（入っている住所は、書き換えない）。
    /// <list type="bullet">
    /// <item>
    /// 郵便番号を入力している最中: 7 桁がそろって、入力が止まってから <see cref="AutoLookupDelay"/>（既定 0.5 秒）たったとき。
    /// 入力のたびには探さないので、入力の邪魔にならない。
    /// </item>
    /// <item>郵便番号の欄を離れたとき（入力の最中に入らなかった場合）。</item>
    /// </list>
    /// </remarks>
    public class SControlDBPostAddr : SControlDBItem
    {
        /// <summary>郵便番号の前半・後半の桁数。そろったときだけ、入力の最中に住所を探す。</summary>
        private const int PostLength1 = 3;
        private const int PostLength2 = 4;

        private readonly STextBox[] _boxes;

        // 登録先（EndAdd の後で入る）。郵便番号から住所を引くためのものを、ここから借りる
        private SControlDB _db;

        // 入力が止まるのを待つタイマー。最初に必要になったときに作る
        private System.Windows.Forms.Timer _lookupTimer;

        // Fetch・Clear で、プログラムから入力欄を書き換えている最中か（この間は、住所を探さない）
        private bool _settingControls;

        /// <param name="rowFields">郵便番号、住所（、住所のふりがな）の項目。2 つまたは 3 つ。</param>
        /// <param name="controls">郵便番号の前半、後半、住所（、住所のふりがな）の入力欄。3 つまたは 4 つ。</param>
        public SControlDBPostAddr(string[] rowFields, STextBox[] controls)
            : base(rowFields, controls)
        {
            if (rowFields.Length < 2 || rowFields.Length > 3)
            {
                throw new ArgumentException("項目は、郵便番号・住所（・住所のふりがな）の 2 つまたは 3 つを指定してください。", nameof(rowFields));
            }
            if (controls.Length < 3 || controls.Length > 4)
            {
                throw new ArgumentException("コントロールは、郵便番号の前半・後半・住所（・住所のふりがな）の 3 つまたは 4 つを指定してください。", nameof(controls));
            }
            _boxes = (STextBox[])controls.Clone();
        }

        /// <summary>
        /// 郵便番号の区切りが 1 つしか無いときに、前半の欄から埋めるか。既定は false（後半の欄から埋める）。
        /// </summary>
        public bool DataFillLeft { get; set; }

        /// <summary>
        /// 郵便番号を入力している最中に、入力が止まってから住所を探すまでの時間（ミリ秒）。既定は 500。
        /// 0 以下にすると、入力の最中には探さない（郵便番号の欄を離れたときだけ探す）。
        /// </summary>
        public int AutoLookupDelay { get; set; } = 500;

        /// <summary>
        /// 郵便番号（「251-0052」の形）から、住所と住所のふりがなを引く処理。null なら、登録先の <see cref="SControlDB"/> に渡された
        /// 郵便番号の DB を使う。テストで、DB の代わりを差し込むためのもの。
        /// </summary>
        /// <remarks>戻り値は、住所・ふりがなの順の 2 つ。見つからなければ null か、空の住所。</remarks>
        internal Func<string, string[]> AddressLookup { get; set; }

        public override void Fetch(ISRow row)
        {
            StopLookupTimer();

            object post = GetFieldValue(row, 0);
            if (post == null)
            {
                return;
            }

            _settingControls = true;
            try
            {
                SControlDBHyphenSplit.SplitInto(_boxes, 2, ToText(post), DataFillLeft);

                // 住所・住所のふりがな（項目が指定されている分だけ）
                for (int i = 2; i < _boxes.Length && i - 1 < RowFields.Length; i++)
                {
                    object value = GetFieldValue(row, i - 1);
                    if (value == null)
                    {
                        return;
                    }
                    _boxes[i].Text = ToText(value);
                }
            }
            finally
            {
                _settingControls = false;
            }
        }

        public override void Update(SControlDB db, ISRow row, Control activeControl)
        {
            // 入力が止まるのを待っている最中でも、ここで住所を入れるので、待つのをやめる
            StopLookupTimer();

            if (!CanUpdate(row))
            {
                return;
            }

            string post = SControlDBHyphenSplit.Join(_boxes, 2);
            UpdateRowValue(row, 0, ConvertControlValue(row, post));

            if (activeControl == _boxes[0] || activeControl == _boxes[1])
            {
                // 郵便番号を書き換えた。住所が空なら、郵便番号から住所を入れる
                FillAddressIfEmpty(db, post);
            }
            else if (activeControl == _boxes[2] && _boxes.Length == 4)
            {
                // 住所を書き換えた。住所のふりがなを自動で入れる
                SControlDBRuby.AppendRuby(_boxes[2], _boxes[3], false);
            }

            for (int i = 2; i < _boxes.Length && i - 1 < RowFields.Length; i++)
            {
                UpdateRowValue(row, i - 1, _boxes[i].Text);
            }
        }

        public override void Clear()
        {
            StopLookupTimer();

            _settingControls = true;
            try
            {
                foreach (STextBox box in _boxes)
                {
                    box.Text = string.Empty;
                }
            }
            finally
            {
                _settingControls = false;
            }
        }

        #region 郵便番号を入力している最中に、住所を探す

        protected internal override void OnEndAdd(SControlDB db)
        {
            _db = db;
            _boxes[0].TextChanged += OnPostTextChanged;
            _boxes[1].TextChanged += OnPostTextChanged;
            _boxes[0].Disposed += OnPostBoxDisposed;
        }

        /// <summary>
        /// 郵便番号の欄が書き換わった。利用者が入力していて、7 桁がそろっていて、住所が空なら、入力が止まるのを待ち始める。
        /// 待っている最中に書き換わったら、待ち直す。
        /// </summary>
        private void OnPostTextChanged(object sender, EventArgs e)
        {
            StopLookupTimer();

            var box = (STextBox)sender;
            if (_settingControls || AutoLookupDelay <= 0 || !box.Focused || box.ReadOnly)
            {
                return;
            }
            if (!IsPostComplete() || _boxes[2].TextLength != 0)
            {
                return;
            }

            if (_lookupTimer == null)
            {
                _lookupTimer = new System.Windows.Forms.Timer();
                _lookupTimer.Tick += OnLookupTimerTick;
            }
            _lookupTimer.Interval = AutoLookupDelay;
            _lookupTimer.Start();
        }

        /// <summary>
        /// 入力が止まった。待っている間に状況が変わっていなければ、住所を探して入れる。
        /// </summary>
        private void OnLookupTimerTick(object sender, EventArgs e)
        {
            StopLookupTimer();

            // 郵便番号の欄を離れていたら、離れたときの処理（Update）に任せる
            if (!_boxes[0].Focused && !_boxes[1].Focused)
            {
                return;
            }
            if (!IsPostComplete())
            {
                return;
            }

            try
            {
                FillAddressIfEmpty(_db, SControlDBHyphenSplit.Join(_boxes, 2));
            }
            catch (Exception ex)
            {
                // 入力を助けるための処理なので、失敗しても、入力は続けられるようにする（欄を離れたときに、もう一度探す）
                ErrLog.WriteException(ex);
            }
        }

        /// <summary>
        /// 住所の欄が空なら、郵便番号から住所を引いて入れる（ふりがなの欄があれば、ふりがなも足す）。
        /// 住所が見つからなければ、何も変えない。
        /// </summary>
        private void FillAddressIfEmpty(SControlDB db, string post)
        {
            if (_boxes[2].TextLength != 0)
            {
                return;
            }

            string[] found = LookUpAddress(db, post);
            if (found == null || found.Length == 0 || string.IsNullOrEmpty(found[0]))
            {
                return;
            }

            _boxes[2].Text = found[0];
            if (_boxes.Length == 4 && found.Length > 1)
            {
                _boxes[3].Text += found[1];
            }
        }

        /// <summary>郵便番号から、住所とふりがなを引く。引く手段が無ければ null。</summary>
        private string[] LookUpAddress(SControlDB db, string post)
        {
            if (AddressLookup != null)
            {
                return AddressLookup(post);
            }
            if (db == null || db.ZipCode == null)
            {
                return null;
            }

            db.ZipCode.SetPost(post);
            return new[] { db.ZipCode.Addr, db.ZipCode.AddrRuby };
        }

        /// <summary>郵便番号の 2 つの欄に、数字が 3 桁・4 桁そろっているか。</summary>
        private bool IsPostComplete()
        {
            return IsDigits(_boxes[0].Text, PostLength1) && IsDigits(_boxes[1].Text, PostLength2);
        }

        private static bool IsDigits(string text, int length)
        {
            if (text.Length != length)
            {
                return false;
            }
            foreach (char c in text)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }

        private void StopLookupTimer()
        {
            if (_lookupTimer != null)
            {
                _lookupTimer.Stop();
            }
        }

        private void OnPostBoxDisposed(object sender, EventArgs e)
        {
            if (_lookupTimer != null)
            {
                _lookupTimer.Dispose();
                _lookupTimer = null;
            }
        }

        #endregion

        // 郵便番号の 1 項目に、入力欄が 2 つ対応する。その後ろは、項目と入力欄が 1 つずつ対応する
        protected internal override int GetFieldIndex(int controlIndex)
        {
            return Math.Max(0, controlIndex - 1);
        }

        protected internal override int GetControlIndex(int fieldIndex)
        {
            return fieldIndex == 0 ? 0 : fieldIndex + 1;
        }
    }
}
