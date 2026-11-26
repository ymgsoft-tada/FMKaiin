using System;
using System.Collections.Generic;
using System.Windows.Forms;
using App.Controls;

namespace App
{
    /// <summary>
    /// 入力欄 1 つ分の入力ルール（桁数・入力できる文字・IME・文字の位置など）。
    /// <see cref="SControlRule"/> が、コントロールの数だけ持つ。
    /// </summary>
    public sealed class SFieldRule
    {
        public SFieldRule()
        {
            MaxLengthUnit = SLengthUnit.TextElement;
            Format = string.Empty;
            AllowSpace = SAllowSpace.Both;
            ImeMode = ImeMode.NoControl;
            TextAlign = HorizontalAlignment.Left;
            HighlightText = true;
            AcceptsCrLf = SCrLfMode.Filter;
            AllowBlank = true;
            AllowNegative = true;
        }

        /// <summary>最大文字数。数値のルールでは、整数の桁数。0 は指定なし。</summary>
        public int MaxLength { get; set; }

        /// <summary>最大文字数の数え方。</summary>
        public SLengthUnit MaxLengthUnit { get; set; }

        /// <summary>入力できる文字の種類（<see cref="SInputFormat"/> の書き方）。</summary>
        public string Format { get; set; }

        /// <summary>入力できる空白の種類。</summary>
        public SAllowSpace AllowSpace { get; set; }

        /// <summary>IME の状態。</summary>
        public ImeMode ImeMode { get; set; }

        /// <summary>文字の左右の位置。上下の位置は、ルールでは変えない。</summary>
        public HorizontalAlignment TextAlign { get; set; }

        /// <summary>フォーカスが移ってきたときに全選択するか。</summary>
        public bool HighlightText { get; set; }

        /// <summary>貼り付ける文字列に含まれる改行の扱い。</summary>
        public SCrLfMode AcceptsCrLf { get; set; }

        /// <summary>数値のルールか。true のとき、<see cref="SNumberBox"/> に桁数などを設定する。</summary>
        public bool IsNumber { get; set; }

        /// <summary>数値の、小数の桁数。</summary>
        public int DecimalPlaces { get; set; }

        /// <summary>数値を、3 桁ごとに区切って表示するか。</summary>
        public bool ThousandsSeparator { get; set; }

        /// <summary>
        /// 数値の空欄（未入力）を許すか。既定は true。false にすると、値が無い項目は 0 と表示され、行にも 0 が書き戻される。
        /// </summary>
        public bool AllowBlank { get; set; }

        /// <summary>数値のマイナスを許すか。</summary>
        public bool AllowNegative { get; set; }

        /// <summary>内容を写した、別のインスタンスを作る。</summary>
        public SFieldRule Clone()
        {
            return (SFieldRule)MemberwiseClone();
        }

        /// <summary>
        /// このルールを <paramref name="control"/> に設定する。ルールの対象でない種類のコントロール（コンボ ボックスなど）には何もしない。
        /// </summary>
        public void Apply(Control control)
        {
            var numberBox = control as SNumberBox;
            if (numberBox != null)
            {
                ApplyTo(numberBox);
                return;
            }

            var dateBox = control as SDateBox;
            if (dateBox != null)
            {
                dateBox.HighlightText = HighlightText;
                dateBox.TextAlign = TextAlign;
                return;
            }

            if (control is STableComboBox)
            {
                // 入力した文字で一覧を絞り込むコントロールなので、文字数や文字の種類は制限しない
                return;
            }

            var textBox = control as STextBox;
            if (textBox != null)
            {
                textBox.MaxLength = MaxLength;
                textBox.MaxLengthUnit = MaxLengthUnit;
                textBox.Format = Format;
                textBox.AllowSpace = AllowSpace;
                textBox.AcceptsCrLf = AcceptsCrLf;
                textBox.HighlightText = HighlightText;
                textBox.ImeMode = ImeMode;
                textBox.TextAlign = TextAlign;
            }
        }

        private void ApplyTo(SNumberBox numberBox)
        {
            if (IsNumber && MaxLength > 0)
            {
                numberBox.SetDigits(MaxLength, DecimalPlaces, AllowNegative);
                numberBox.ThousandsSeparator = ThousandsSeparator;
                numberBox.AllowBlank = AllowBlank;
            }
            numberBox.HighlightText = HighlightText;
            numberBox.TextAlign = TextAlign;
        }
    }

    /// <summary>
    /// 1 つの項目（または、ひとまとまりの項目）に対する入力ルール。GControlDBRuleText などに当たる。
    /// 郵便番号や電話番号のように入力欄が複数ある項目では、入力欄ごとの <see cref="SFieldRule"/> を並べて持つ。
    /// ルールは、<see cref="Text"/> などのメソッドで作り、必要なら <see cref="Parts"/> の値を変えてから登録する。
    /// </summary>
    public sealed class SControlRule
    {
        private readonly SFieldRule[] _parts;

        public SControlRule(params SFieldRule[] parts)
        {
            if (parts == null || parts.Length == 0)
            {
                throw new ArgumentException("入力欄のルールを 1 つ以上指定してください。", nameof(parts));
            }
            _parts = parts;
        }

        /// <summary>
        /// 入力欄ごとのルール。1 つだけの場合は、すべての入力欄に同じルールを使う。
        /// </summary>
        public SFieldRule[] Parts
        {
            get { return _parts; }
        }

        /// <summary>
        /// ルールを、コントロールに設定する。<paramref name="controls"/> の i 番目に、<see cref="Parts"/> の i 番目を使う
        /// （<see cref="Parts"/> が足りない分は、最後のものを使う）。
        /// </summary>
        public void Apply(params Control[] controls)
        {
            for (int i = 0; i < controls.Length; i++)
            {
                if (controls[i] != null)
                {
                    _parts[Math.Min(i, _parts.Length - 1)].Apply(controls[i]);
                }
            }
        }

        #region ルールを作る

        /// <summary>文字列。IME はひらがな、左寄せ、空白は全角・半角とも可。GControlDBRuleText に当たる。</summary>
        public static SControlRule Text(int maxLength)
        {
            return new SControlRule(new SFieldRule { MaxLength = maxLength, ImeMode = ImeMode.Hiragana });
        }

        /// <summary>半角の英数字。IME は無効、空白は半角だけ。GControlDBRuleHankaku に当たる。</summary>
        public static SControlRule Hankaku(int maxLength)
        {
            return new SControlRule(new SFieldRule
            {
                MaxLength = maxLength,
                Format = "Aa9",
                AllowSpace = SAllowSpace.Narrow,
                ImeMode = ImeMode.Disable,
            });
        }

        /// <summary>
        /// 数値（桁区切りなし）。IME は無効、右寄せ、空白は不可。GControlDBRuleNumber に当たる。
        /// <see cref="STextBox"/> に設定した場合は、「数字だけ・<paramref name="integerDigits"/> 文字まで」になる。
        /// </summary>
        public static SControlRule Number(int integerDigits, int decimalPlaces)
        {
            return new SControlRule(CreateNumberPart(integerDigits, decimalPlaces, false));
        }

        /// <summary>金額（桁区切りあり）。GControlDBRuleCurrency に当たる。</summary>
        public static SControlRule Currency(int integerDigits, int decimalPlaces)
        {
            return new SControlRule(CreateNumberPart(integerDigits, decimalPlaces, true));
        }

        /// <summary>
        /// ハイフンで区切る項目（電話番号など）。入力欄ごとの最大文字数を指定する。数字だけ、IME は無効、右寄せ。
        /// GControlDBRuleHyphenSplit に当たる。
        /// </summary>
        public static SControlRule HyphenSplit(params int[] maxLengths)
        {
            var parts = new SFieldRule[maxLengths.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = CreateDigitsPart(maxLengths[i]);
            }
            return new SControlRule(parts);
        }

        /// <summary>
        /// 名称とふりがな。名称は半角記号以外・IME はひらがな、ふりがなは半角文字・IME は半角カタカナ。GControlDBRuleRuby に当たる。
        /// </summary>
        public static SControlRule Ruby(int nameMaxLength, int rubyMaxLength)
        {
            return new SControlRule(
                new SFieldRule { MaxLength = nameMaxLength, Format = "^@", ImeMode = ImeMode.Hiragana },
                CreateHalfKanaPart(rubyMaxLength, "H"));
        }

        /// <summary>
        /// 郵便番号（3 桁・4 桁）と住所、住所のふりがな。GControlDBRulePostAddr に当たる。
        /// </summary>
        /// <param name="addressMaxLength">住所の最大文字数。</param>
        /// <param name="addressRubyMaxLength">住所のふりがなの最大文字数。</param>
        public static SControlRule PostAddr(int addressMaxLength, int addressRubyMaxLength)
        {
            // 住所と住所のふりがなは、続きを入力することが多いので、フォーカスが移ってきても全選択にしない
            SFieldRule addressRuby = CreateHalfKanaPart(addressRubyMaxLength, "H^@");
            addressRuby.HighlightText = false;

            return new SControlRule(
                CreateDigitsPart(3),
                CreateDigitsPart(4),
                new SFieldRule { MaxLength = addressMaxLength, Format = "^@", ImeMode = ImeMode.Hiragana, HighlightText = false },
                addressRuby);
        }

        /// <summary>日付。IME は無効、中央寄せ。GControlDBRuleDate に当たる。</summary>
        public static SControlRule Date()
        {
            return new SControlRule(new SFieldRule { ImeMode = ImeMode.Disable, TextAlign = HorizontalAlignment.Center });
        }

        private static SFieldRule CreateNumberPart(int integerDigits, int decimalPlaces, bool thousandsSeparator)
        {
            SFieldRule part = CreateDigitsPart(integerDigits);
            part.IsNumber = true;
            part.DecimalPlaces = decimalPlaces;
            part.ThousandsSeparator = thousandsSeparator;
            return part;
        }

        /// <summary>数字だけの入力欄。</summary>
        private static SFieldRule CreateDigitsPart(int maxLength)
        {
            return new SFieldRule
            {
                MaxLength = maxLength,
                Format = "9",
                AllowSpace = SAllowSpace.None,
                ImeMode = ImeMode.Disable,
                TextAlign = HorizontalAlignment.Right,
            };
        }

        /// <summary>半角カタカナで入力する欄。</summary>
        private static SFieldRule CreateHalfKanaPart(int maxLength, string format)
        {
            return new SFieldRule
            {
                MaxLength = maxLength,
                Format = format,
                AllowSpace = SAllowSpace.Narrow,
                ImeMode = ImeMode.KatakanaHalf,
            };
        }

        #endregion
    }

    /// <summary>
    /// 項目名と入力ルールの対応表。GControlDBRule に当たる。
    /// <see cref="SControlDB.EndAdd(SControlDBRule)"/> に渡すと、登録したコントロールに、項目名で引いたルールが設定される。
    /// </summary>
    public sealed class SControlDBRule
    {
        private readonly Dictionary<string, SControlRule> _rules = new Dictionary<string, SControlRule>(StringComparer.Ordinal);

        /// <summary>
        /// この表に無い項目のルールを、ほかから引くための関数。引数は項目名。無ければ null を返すこと。
        /// ルールを別の形で持っているもの（テストで使う、Gc 系の定義の読み替えなど）を、この表につなぐために用意している。
        /// </summary>
        public Func<string, SControlRule> Fallback { get; set; }

        /// <summary>ルールを登録してある項目名の一覧（<see cref="Fallback"/> から引けるものは含まない）。</summary>
        public ICollection<string> Fields
        {
            get { return _rules.Keys; }
        }

        /// <summary>
        /// 項目にルールを登録する。同じ項目に 2 回登録することはできない。
        /// </summary>
        public void Add(SControlRule rule, string rowField)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }
            if (_rules.ContainsKey(rowField))
            {
                throw new ArgumentException("同じ項目に、入力ルールを複数登録することはできません。 '" + rowField + "'");
            }
            _rules.Add(rowField, rule);
        }

        /// <summary>
        /// 項目のルールを返す。登録が無ければ <see cref="Fallback"/> を調べる。無ければ null。
        /// </summary>
        public SControlRule GetRule(string rowField)
        {
            SControlRule rule;
            if (_rules.TryGetValue(rowField, out rule))
            {
                return rule;
            }
            return Fallback == null ? null : Fallback(rowField);
        }

        /// <summary>
        /// <paramref name="rowField"/> のルールを、コントロールに設定する。DB にバインドしない入力欄（検索欄など）に使う。
        /// ルールが登録されていなければ、何もしない。
        /// </summary>
        public void SetControlByRule(Control control, string rowField)
        {
            SetControlByRule(new[] { control }, rowField);
        }

        /// <summary>
        /// <paramref name="rowField"/> のルールを、コントロールに設定する（入力欄が複数ある項目）。
        /// </summary>
        public void SetControlByRule(Control[] controls, string rowField)
        {
            SControlRule rule = GetRule(rowField);
            if (rule != null)
            {
                rule.Apply(controls);
            }
        }

        /// <summary>
        /// <paramref name="db"/> に登録されているすべてのコントロールに、対応する項目のルールを設定する。
        /// </summary>
        internal void SetRule(SControlDB db)
        {
            foreach (SControlDBItem item in db.Items)
            {
                foreach (string field in item.RowFields)
                {
                    SControlRule rule = GetRule(field);
                    if (rule != null)
                    {
                        rule.Apply(item.Ctls);
                    }
                }
            }
        }
    }
}
