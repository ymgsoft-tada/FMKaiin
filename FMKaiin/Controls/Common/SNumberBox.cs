using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 数値の入力欄。GcNumberEx（InputMan の GcNumber）の代わりに使う。
    /// 値は <see cref="Value"/>（decimal?）で読み書きする。未入力は null。
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>表示</b>: フォーカスが無いときは、桁区切りと決まった桁数の小数を付けて表示する（例: 1,234,567）。
    /// フォーカスがある間は、桁区切りの無い数字だけにして編集する（例: 1234567）。
    /// </para>
    /// <para>
    /// <b>入力</b>: 数字・マイナス・小数点だけを入力できる。全角で入力しても半角になる。
    /// 整数の桁数（<see cref="MaxValue"/>・<see cref="MinValue"/> から決まる）、小数の桁数（<see cref="DecimalPlaces"/>）、
    /// 値の範囲を超える入力は受け付けない。入力するたびに <see cref="Value"/> が変わり、<see cref="ValueChanged"/> が発生する。
    /// </para>
    /// <para>
    /// <b>キー</b>: マイナス キーは符号を反転する（全選択のときは、入力し直しとして「-」だけにする）。プラス キーは正の数にする。
    /// 小数を持たない設定のとき、小数点のキーは値を 1000 倍する（<see cref="PeriodMultipliesByThousand"/>）。
    /// </para>
    /// <para>
    /// 見た目とフォーカス時の動作は <see cref="STextBox"/> と同じ（共通の土台 <see cref="SEditBase"/> による）。
    /// 既定で、文字は右寄せ、フォーカス時に全選択、IME は無効。
    /// </para>
    /// </remarks>
    public class SNumberBox : SEditBase
    {
        /// <summary>入力できる文字。数字と、マイナス・プラス・小数点。どの位置に入力できるかは <see cref="ProcessInput"/> で調べる。</summary>
        private const string NumberCharacters = "9-+.";

        private const string DefaultMaxValueText = "9999999999999";
        private const string DefaultMinValueText = "-9999999999999";

        private decimal? _value;
        private decimal _maxValue = decimal.Parse(DefaultMaxValueText, CultureInfo.InvariantCulture);
        private decimal _minValue = decimal.Parse(DefaultMinValueText, CultureInfo.InvariantCulture);
        private int _decimalPlaces;
        private bool _thousandsSeparator = true;
        private bool _allowBlank = true;
        private bool _periodMultipliesByThousand = true;

        // フォーカスがあり、編集用の表示（桁区切りなし）にしている間 true
        private bool _editing;
        // このコントロール自身が Text を書き換えている間 true（利用者の入力と区別するため）
        private bool _settingText;
        // 編集中の文字列のうち、最後に受け付けたもの。受け付けられない形に変わったとき（削除で桁数を超えた場合など）に、ここへ戻す
        private string _acceptedEditText = string.Empty;

        public SNumberBox()
        {
            InputFilter.Format = new SInputFormat(NumberCharacters);
            InputFilter.AllowSpace = SAllowSpace.None;
            base.HighlightText = true;
            TextAlign = HorizontalAlignment.Right;
        }

        /// <summary>
        /// <see cref="Value"/> が変わったときに発生する（入力の途中でも、1 文字ごとに発生する）。
        /// </summary>
        [Category("Action")]
        [Description("Value が変わったときに発生する（入力の途中でも、1 文字ごとに発生する）。")]
        public event EventHandler ValueChanged;

        /// <summary>
        /// 数値の入力欄は、既定では、収まらない文字を縮めない（<see cref="SEditBase.ShrinkTextToFit"/> の既定が false）。
        /// 金額などは、欄によって文字の大きさが違うと、桁を見比べにくいため。縮めたい欄だけ、プロパティで true にする。
        /// </summary>
        protected override bool DefaultShrinkTextToFit
        {
            get { return false; }
        }

        #region 値と設定

        /// <summary>
        /// 値。未入力は null。範囲（<see cref="MinValue"/>～<see cref="MaxValue"/>）の外の値を設定すると、範囲の端に丸める。
        /// 小数は <see cref="DecimalPlaces"/> の桁数に四捨五入する。<see cref="AllowBlank"/> が false のときは、null の代わりに 0 になる。
        /// </summary>
        [Category("Data")]
        [DefaultValue(null)]
        [Description("値。未入力は null。")]
        public decimal? Value
        {
            get { return _value; }
            set
            {
                bool changed = SetValueCore(Normalize(value));
                RefreshText();
                if (changed)
                {
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>入力できる最大の値。整数の桁数も、この値と <see cref="MinValue"/> から決まる。</summary>
        [Category("Behavior")]
        [DefaultValue(typeof(decimal), DefaultMaxValueText)]
        [Description("入力できる最大の値。整数の桁数も、この値と MinValue から決まる。")]
        public decimal MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                if (_minValue > value)
                {
                    _minValue = value;
                }
                ReapplySettings();
            }
        }

        /// <summary>入力できる最小の値。0 以上にすると、マイナスを入力できなくなる。</summary>
        [Category("Behavior")]
        [DefaultValue(typeof(decimal), DefaultMinValueText)]
        [Description("入力できる最小の値。0 以上にすると、マイナスを入力できなくなる。")]
        public decimal MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                if (_maxValue < value)
                {
                    _maxValue = value;
                }
                ReapplySettings();
            }
        }

        /// <summary>小数の桁数。0 なら整数だけ。表示するときは、この桁数まで 0 で埋める。</summary>
        [Category("Behavior")]
        [DefaultValue(0)]
        [Description("小数の桁数。0 なら整数だけ。表示するときは、この桁数まで 0 で埋める。")]
        public int DecimalPlaces
        {
            get { return _decimalPlaces; }
            set
            {
                // decimal が扱える小数の桁数は 28 まで
                if (value < 0 || value > 28)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _decimalPlaces = value;
                ReapplySettings();
            }
        }

        /// <summary>フォーカスが無いときの表示に、3 桁ごとの区切り（,）を付けるか。</summary>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("フォーカスが無いときの表示に、3 桁ごとの区切り（,）を付けるか。")]
        public bool ThousandsSeparator
        {
            get { return _thousandsSeparator; }
            set
            {
                _thousandsSeparator = value;
                RefreshText();
            }
        }

        /// <summary>未入力（空欄）を許すか。false にすると、空欄は 0 になる。</summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("未入力（空欄）を許すか。false にすると、空欄は 0 になる。")]
        public bool AllowBlank
        {
            get { return _allowBlank; }
            set
            {
                _allowBlank = value;
                ReapplySettings();
            }
        }

        /// <summary>
        /// 小数を持たない設定（<see cref="DecimalPlaces"/> が 0）のとき、小数点のキーで値を 1000 倍するか。
        /// 金額の「000」を 1 回のキーで入力するための機能で、GcNumberEx の動作に合わせて既定は true。
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("小数を持たない設定のとき、小数点のキーで値を 1000 倍するか。")]
        public bool PeriodMultipliesByThousand
        {
            get { return _periodMultipliesByThousand; }
            set { _periodMultipliesByThousand = value; }
        }

        /// <summary>
        /// 整数と小数の桁数から、範囲（<see cref="MaxValue"/>・<see cref="MinValue"/>）と <see cref="DecimalPlaces"/> をまとめて設定する。
        /// 例: 整数 7 桁・小数 0 桁なら、-9,999,999 ～ 9,999,999。
        /// </summary>
        /// <param name="integerDigits">整数の桁数（1～28）。</param>
        /// <param name="decimalPlaces">小数の桁数。</param>
        /// <param name="allowNegative">マイナスを入力できるようにするか。false なら最小値を 0 にする。</param>
        public void SetDigits(int integerDigits, int decimalPlaces, bool allowNegative)
        {
            if (integerDigits < 1 || decimalPlaces < 0 || integerDigits + decimalPlaces > 28)
            {
                throw new ArgumentOutOfRangeException(nameof(integerDigits));
            }

            // 9 を並べた値を作る（7 桁・小数 1 桁なら 9999999.9）
            string nines = new string('9', integerDigits) + (decimalPlaces > 0 ? "." + new string('9', decimalPlaces) : string.Empty);
            decimal max = decimal.Parse(nines, CultureInfo.InvariantCulture);

            _maxValue = max;
            _minValue = allowNegative ? -max : 0m;
            _decimalPlaces = decimalPlaces;
            ReapplySettings();
        }

        /// <summary>
        /// 文字列。取得すると、いま表示している文字列（フォーカスが無ければ桁区切り付き）を返す。
        /// 設定すると、数値として読み取って <see cref="Value"/> に入れる（桁区切りや通貨記号は無視する。数値として読めなければ未入力）。
        /// 値の読み書きには、このプロパティではなく <see cref="Value"/> を使うこと。
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
                }
                else
                {
                    Value = ParseLoose(value);
                }
            }
        }

        /// <summary>フォーカスが移ってきたときに、文字列を全選択するか。数値は入力し直すことが多いので、既定を true にしている。</summary>
        [DefaultValue(true)]
        public override bool HighlightText
        {
            get { return base.HighlightText; }
            set { base.HighlightText = value; }
        }

        /// <summary>IME の既定の状態。数値の入力では使わないので、無効にする。</summary>
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

        /// <summary>値を、設定に合う形にする（空欄の扱い、範囲、小数の桁数）。</summary>
        private decimal? Normalize(decimal? value)
        {
            if (!value.HasValue)
            {
                return _allowBlank ? (decimal?)null : Clamp(0m);
            }

            return Clamp(Math.Round(value.Value, _decimalPlaces, MidpointRounding.AwayFromZero));
        }

        private decimal Clamp(decimal value)
        {
            return Math.Max(_minValue, Math.Min(value, _maxValue));
        }

        /// <summary>値を覚える。変わったかどうかを返す（イベントは呼び出し側で発生させる）。</summary>
        private bool SetValueCore(decimal? value)
        {
            if (_value == value)
            {
                return false;
            }
            _value = value;
            return true;
        }

        /// <summary>設定（範囲・桁数・空欄の扱い）を変えた後、いまの値を設定に合わせ直す。</summary>
        private void ReapplySettings()
        {
            Value = _value;
        }

        #endregion

        #region 表示の切り替え

        protected override void OnEnter(EventArgs e)
        {
            // 編集用の表示（桁区切りなし）に切り替えてから、標準の処理（全選択など）を行う
            if (!ReadOnly)
            {
                _editing = true;
                RefreshText();
            }
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            // ほかのコントロールの Validating より前に、値を確定して表示用の形に戻す
            if (_editing)
            {
                _editing = false;
                Value = ParseEditText(base.Text);
                RefreshText();
            }
            base.OnLeave(e);
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            base.OnReadOnlyChanged(e);

            // 読み取り専用にされたら、編集を終えて表示用の形に戻す。フォーカスがあるまま解除されたら、編集用の形にする
            bool editing = !ReadOnly && Focused;
            if (_editing != editing)
            {
                _editing = editing;
                RefreshText();
            }
        }

        /// <summary>いまの値を、状態に合った形（編集用・表示用）で入力欄に表示する。</summary>
        private void RefreshText()
        {
            SetTextInternal(_editing ? FormatForEdit(_value) : FormatForDisplay(_value));
        }

        private void SetTextInternal(string text)
        {
            _acceptedEditText = text;
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

        private string FormatForDisplay(decimal? value)
        {
            return value.HasValue
                ? value.Value.ToString((_thousandsSeparator ? "#,##0" : "0") + DecimalFormat, CultureInfo.InvariantCulture)
                : string.Empty;
        }

        private string FormatForEdit(decimal? value)
        {
            return value.HasValue ? value.Value.ToString("0" + DecimalFormat, CultureInfo.InvariantCulture) : string.Empty;
        }

        /// <summary>小数部分の書式（.00 など）。小数を持たなければ空文字列。</summary>
        private string DecimalFormat
        {
            get { return _decimalPlaces > 0 ? "." + new string('0', _decimalPlaces) : string.Empty; }
        }

        #endregion

        #region 入力

        protected override void OnTextChanged(EventArgs e)
        {
            // 利用者が編集した（入力・削除・貼り付け）
            if (_editing && !_settingText)
            {
                string text = base.Text;
                if (!IsAcceptableEditText(text))
                {
                    // 文字の挿入は ProcessInput で調べているが、削除（Delete・BackSpace・切り取り）は調べられない。
                    // 削除で受け付けられない形になった場合（小数点を消して整数の桁数を超えた、など）は、元に戻す。
                    // 戻さないと、値が範囲の端に丸められて、利用者が入力していない数値に変わってしまう
                    RestoreAcceptedEditText();
                    OnInvalidInput(EventArgs.Empty);
                    return;
                }

                // 値を追従させる
                _acceptedEditText = text;
                SyncValueFromEditText();
            }
            base.OnTextChanged(e);
        }

        /// <summary>編集中の文字列を、最後に受け付けたものへ戻す。カーソルは、いまの位置に近いところに置く。</summary>
        private void RestoreAcceptedEditText()
        {
            int caret = SelectionStart;
            string text = _acceptedEditText;
            SetTextInternal(text);
            Select(Math.Min(caret, text.Length), 0);
        }

        /// <summary>
        /// 入力しようとしている文字列を調べる。挿入した結果が数値の形にならない場合と、桁数・範囲を超える場合は受け付けない。
        /// マイナス・プラス・（小数を持たない設定での）小数点は、文字として挿入せずに、値への操作として扱う。
        /// </summary>
        protected override string ProcessInput(string input)
        {
            string text = base.Text;
            int start = Math.Min(SelectionStart, text.Length);
            int length = Math.Min(SelectionLength, text.Length - start);
            bool allSelected = text.Length > 0 && length == text.Length;

            if (input == "-")
            {
                if (_minValue >= 0)
                {
                    OnInvalidInput(EventArgs.Empty);
                }
                else if (text.Length == 0 || allSelected)
                {
                    // 入力し直し。「-」から始める
                    ReplaceEditText("-");
                }
                else
                {
                    ReplaceEditText(text.StartsWith("-", StringComparison.Ordinal) ? text.Substring(1) : "-" + text);
                }
                return string.Empty;
            }

            if (input == "+")
            {
                if (text.StartsWith("-", StringComparison.Ordinal))
                {
                    ReplaceEditText(text.Substring(1));
                }
                return string.Empty;
            }

            if (input == "." && _decimalPlaces == 0)
            {
                decimal? current = ParseEditText(text);
                if (_periodMultipliesByThousand && current.HasValue && current.Value != 0m)
                {
                    ReplaceEditText(FormatForEdit(current.Value * 1000m));
                }
                else
                {
                    OnInvalidInput(EventArgs.Empty);
                }
                return string.Empty;
            }

            string result = text.Remove(start, length).Insert(start, input);
            if (IsAcceptableEditText(result))
            {
                return input;
            }

            OnInvalidInput(EventArgs.Empty);
            return string.Empty;
        }

        /// <summary>
        /// 編集中の文字列を <paramref name="text"/> に置き換える（符号の反転、1000 倍）。受け付けられない形なら何もしない。
        /// カーソルは末尾に置く。
        /// </summary>
        private void ReplaceEditText(string text)
        {
            if (!IsAcceptableEditText(text))
            {
                OnInvalidInput(EventArgs.Empty);
                return;
            }

            // 利用者の入力として扱う（Value を追従させる）ので、_settingText は立てない
            base.Text = text;
            Select(text.Length, 0);
        }

        /// <summary>
        /// 編集中の文字列として受け付けられるか。「-」だけ、「12.」のような入力の途中の形も受け付ける。
        /// </summary>
        private bool IsAcceptableEditText(string text)
        {
            int index = 0;
            if (text.Length > 0 && text[0] == '-')
            {
                if (_minValue >= 0)
                {
                    return false;
                }
                index = 1;
            }

            int integerDigits = 0;
            int decimalDigits = 0;
            bool seenPoint = false;
            for (; index < text.Length; index++)
            {
                char c = text[index];
                if (c == '.')
                {
                    if (seenPoint || _decimalPlaces == 0)
                    {
                        return false;
                    }
                    seenPoint = true;
                }
                else if (c >= '0' && c <= '9')
                {
                    if (seenPoint)
                    {
                        decimalDigits++;
                    }
                    else
                    {
                        integerDigits++;
                    }
                }
                else
                {
                    return false;
                }
            }

            if (integerDigits > MaxIntegerDigits || decimalDigits > _decimalPlaces)
            {
                return false;
            }

            // 範囲は、0 から遠い側だけを入力中に調べる。0 に近い側（最小値が 10 のときの「1」など）は、
            // 入力の途中では判断できないので、確定するときに範囲の端へ丸める
            decimal? value = ParseEditText(text);
            if (value.HasValue)
            {
                if (value.Value > 0m && value.Value > _maxValue)
                {
                    return false;
                }
                if (value.Value < 0m && value.Value < _minValue)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>入力できる整数の桁数。最大値・最小値のうち、桁数の多いほうに合わせる。</summary>
        private int MaxIntegerDigits
        {
            get
            {
                decimal largest = Math.Max(Math.Abs(decimal.Truncate(_maxValue)), Math.Abs(decimal.Truncate(_minValue)));
                return largest.ToString(CultureInfo.InvariantCulture).Length;
            }
        }

        /// <summary>編集中の文字列に合わせて、<see cref="Value"/> を更新する（入力欄の文字列は変えない）。</summary>
        private void SyncValueFromEditText()
        {
            if (SetValueCore(Normalize(ParseEditText(base.Text))))
            {
                OnValueChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 編集中の文字列（数字・マイナス・小数点だけ）を数値にする。数字が 1 つも無ければ null。
        /// </summary>
        private static decimal? ParseEditText(string text)
        {
            decimal value;
            return decimal.TryParse(
                text,
                NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,
                out value)
                ? value
                : (decimal?)null;
        }

        /// <summary>
        /// プログラムから渡された文字列を数値にする。桁区切り・空白・通貨記号を取り除き、全角の数字も読む。数値として読めなければ null。
        /// </summary>
        private static decimal? ParseLoose(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            // 全角の数字・記号を半角にそろえてから、数値に関係のない文字を取り除く
            string normalized;
            try
            {
                normalized = text.Normalize(NormalizationForm.FormKC);
            }
            catch (ArgumentException)
            {
                normalized = text;
            }

            var digits = new StringBuilder(normalized.Length);
            foreach (char c in normalized)
            {
                if ((c >= '0' && c <= '9') || c == '.' || (c == '-' && digits.Length == 0))
                {
                    digits.Append(c);
                }
            }
            return ParseEditText(digits.ToString());
        }

        #endregion
    }
}
