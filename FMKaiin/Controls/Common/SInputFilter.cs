using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.VisualBasic;

namespace App.Controls
{
    /// <summary>
    /// 利用者が入力しようとしている文字列（キー入力・IME の確定・貼り付け）を、入力規則に合う形へ直す。
    /// 入力規則は、文字の種類（<see cref="Format"/>）、空白（<see cref="AllowSpace"/>）、
    /// 改行（<see cref="AcceptsCrLf"/>）、最大文字数（<see cref="MaxLength"/>）の 4 つ。
    /// 入力コントロール（<see cref="STextBox"/> など）が共通で使う。
    /// </summary>
    public sealed class SInputFilter
    {
        /// <summary>自動変換で、1 文字に対して重ねて試す変換の回数の上限（例: ひらがな → カタカナ → 半角 で 2 回）。</summary>
        private const int MaxConversionSteps = 3;

        private const int JapaneseLocaleId = 0x0411;

        private delegate string Conversion(string text);

        // 自動変換で試す変換。少ない回数で入力できる形になるものを優先し、同じ回数ならこの並び順で先のものを使う
        private static readonly Conversion[] Conversions =
        {
            delegate(string text) { return ConvertWidthOrKana(text, VbStrConv.Narrow); },
            delegate(string text) { return ConvertWidthOrKana(text, VbStrConv.Wide); },
            delegate(string text) { return text.ToUpperInvariant(); },
            delegate(string text) { return text.ToLowerInvariant(); },
            delegate(string text) { return ConvertWidthOrKana(text, VbStrConv.Katakana); },
            delegate(string text) { return ConvertWidthOrKana(text, VbStrConv.Hiragana); },
            EnlargeSmallKana,
        };

        // 入力できない文字ごとの変換結果。規則（Format / AllowSpace）を変えたら捨てる
        private readonly Dictionary<char, string> _conversionCache = new Dictionary<char, string>();

        private SInputFormat _format = SInputFormat.None;
        private SAllowSpace _allowSpace = SAllowSpace.Both;
        private bool _autoConvert = true;
        private SCrLfMode _acceptsCrLf = SCrLfMode.NoControl;
        private int _maxLength;
        private SLengthUnit _maxLengthUnit = SLengthUnit.Char;

        /// <summary>入力できる文字の種類。既定は制限なし。</summary>
        public SInputFormat Format
        {
            get { return _format; }
            set
            {
                _format = value ?? SInputFormat.None;
                _conversionCache.Clear();
            }
        }

        /// <summary>入力できる空白の種類。既定は全角・半角とも可。</summary>
        public SAllowSpace AllowSpace
        {
            get { return _allowSpace; }
            set
            {
                _allowSpace = value;
                _conversionCache.Clear();
            }
        }

        /// <summary>
        /// 入力できない文字を、入力できる文字へ自動で変換するか。既定は true。
        /// 変換は、全角と半角、英字の大文字と小文字、ひらがなとカタカナ、小書きのかなと通常のかなの間で行う
        /// （例: 書式が数字だけのとき「１」を「1」に、半角カタカナだけのとき「あ」を「ｱ」にする）。
        /// 変換しても入力できない文字は捨てる。
        /// </summary>
        public bool AutoConvert
        {
            get { return _autoConvert; }
            set { _autoConvert = value; }
        }

        /// <summary>貼り付ける文字列に含まれる改行の扱い。既定は <see cref="SCrLfMode.NoControl"/>。</summary>
        public SCrLfMode AcceptsCrLf
        {
            get { return _acceptsCrLf; }
            set { _acceptsCrLf = value; }
        }

        /// <summary>最大文字数。数え方は <see cref="MaxLengthUnit"/> で決まる。0 以下は制限なし。</summary>
        public int MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }

        /// <summary>最大文字数の数え方。既定は <see cref="SLengthUnit.Char"/>。</summary>
        public SLengthUnit MaxLengthUnit
        {
            get { return _maxLengthUnit; }
            set { _maxLengthUnit = value; }
        }

        /// <summary>
        /// 入力しようとしている文字列を、入力規則に合う形へ直して返す。入力できる文字が残らなければ空文字列を返す。
        /// </summary>
        /// <param name="input">入力しようとしている文字列。</param>
        /// <param name="currentText">コントロールの現在の文字列。</param>
        /// <param name="selectionStart">選択範囲の開始位置（入力する位置）。</param>
        /// <param name="selectionLength">選択範囲の長さ。入力によって置き換わる部分。</param>
        /// <param name="multiline">複数行を入力できるコントロールか。false の場合、改行は入力できない。</param>
        public string Accept(string input, string currentText, int selectionStart, int selectionLength, bool multiline)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            string text = ApplyCrLfMode(input, multiline);
            text = ApplyCharacterRules(text, multiline);
            return FitToMaxLength(text, currentText ?? string.Empty, selectionStart, selectionLength);
        }

        /// <summary>
        /// <paramref name="text"/> の長さを、<paramref name="unit"/> の数え方で返す。
        /// </summary>
        public static int MeasureLength(string text, SLengthUnit unit)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            switch (unit)
            {
                case SLengthUnit.TextElement:
                    return new StringInfo(text).LengthInTextElements;

                case SLengthUnit.Byte:
                    int bytes = 0;
                    foreach (char c in text)
                    {
                        bytes += SInputFormat.IsHalfWidth(c) ? 1 : 2;
                    }
                    return bytes;

                default:
                    return text.Length;
            }
        }

        /// <summary>
        /// 改行の扱いを適用する。1 行入力のコントロールには改行を入れられないので、
        /// 改行を取り除く指定（Filter）以外では、最初の改行より後ろを捨てる。
        /// </summary>
        private string ApplyCrLfMode(string input, bool multiline)
        {
            int firstBreak = input.IndexOfAny(new[] { '\r', '\n' });
            if (firstBreak < 0)
            {
                return input;
            }

            if (_acceptsCrLf == SCrLfMode.Filter)
            {
                return input.Replace("\r", string.Empty).Replace("\n", string.Empty);
            }

            if (_acceptsCrLf == SCrLfMode.Cut || !multiline)
            {
                return input.Substring(0, firstBreak);
            }

            return input;
        }

        /// <summary>
        /// 文字の種類と空白の規則を適用する。入力できない文字は、自動変換できれば変換し、できなければ捨てる。
        /// </summary>
        private string ApplyCharacterRules(string input, bool multiline)
        {
            var result = new StringBuilder(input.Length);

            foreach (char c in input)
            {
                if (c == '\r' || c == '\n')
                {
                    // 改行は ApplyCrLfMode で処理済み
                    result.Append(c);
                }
                else if (c == '\t')
                {
                    // タブは、複数行の入力欄にだけ入れる（文字の種類の制限の対象にはしない）。
                    // 1 行の入力欄では捨てる。表計算ソフトの複数のセルを貼り付けると、セルの区切りとしてタブが入ってくるため
                    if (multiline)
                    {
                        result.Append(c);
                    }
                }
                else if (char.IsControl(c))
                {
                    // そのほかの制御文字は入力させない
                }
                else if (IsAllowed(c))
                {
                    result.Append(c);
                }
                else if (_autoConvert)
                {
                    result.Append(GetConverted(c));
                }
            }

            return result.ToString();
        }

        private bool IsAllowed(char c)
        {
            if (c == ' ')
            {
                return _allowSpace == SAllowSpace.Narrow || _allowSpace == SAllowSpace.Both;
            }
            if (c == '　')
            {
                return _allowSpace == SAllowSpace.Wide || _allowSpace == SAllowSpace.Both;
            }
            return _format.Allows(c);
        }

        private bool IsAllowed(string text)
        {
            if (text.Length == 0)
            {
                return false;
            }

            foreach (char c in text)
            {
                if (!IsAllowed(c))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 入力できない文字 <paramref name="c"/> を、入力できる文字（列）へ変換する。変換できなければ空文字列。
        /// 1 文字が 2 文字になることがある（例: 「ガ」→「ｶﾞ」）。
        /// </summary>
        private string GetConverted(char c)
        {
            string converted;
            if (!_conversionCache.TryGetValue(c, out converted))
            {
                converted = FindConversion(c.ToString());
                _conversionCache.Add(c, converted);
            }
            return converted;
        }

        /// <summary>
        /// 変換を重ねて試し、最初に入力できる形になったものを返す（変換の回数が少ないものから順に調べる）。
        /// </summary>
        private string FindConversion(string source)
        {
            var seen = new HashSet<string>(StringComparer.Ordinal);
            seen.Add(source);

            var current = new List<string>();
            current.Add(source);

            for (int step = 0; step < MaxConversionSteps && current.Count > 0; step++)
            {
                var next = new List<string>();
                foreach (string text in current)
                {
                    foreach (Conversion conversion in Conversions)
                    {
                        string candidate = conversion(text);
                        if (!seen.Add(candidate))
                        {
                            continue;
                        }
                        if (IsAllowed(candidate))
                        {
                            return candidate;
                        }
                        next.Add(candidate);
                    }
                }
                current = next;
            }

            return string.Empty;
        }

        /// <summary>
        /// 入力後の文字列が最大文字数を超える場合に、超えなくなるまで <paramref name="input"/> の末尾を削る。
        /// サロゲート ペアや結合文字の途中では切らない。
        /// </summary>
        private string FitToMaxLength(string input, string currentText, int selectionStart, int selectionLength)
        {
            if (_maxLength <= 0 || input.Length == 0)
            {
                return input;
            }

            int start = Math.Max(0, Math.Min(selectionStart, currentText.Length));
            int end = Math.Max(start, Math.Min(start + selectionLength, currentText.Length));
            string before = currentText.Substring(0, start);
            string after = currentText.Substring(end);

            if (MeasureLength(before + input + after, _maxLengthUnit) <= _maxLength)
            {
                return input;
            }

            int[] elementStarts = StringInfo.ParseCombiningCharacters(input);
            for (int count = elementStarts.Length - 1; count >= 1; count--)
            {
                string head = input.Substring(0, elementStarts[count]);
                if (MeasureLength(before + head + after, _maxLengthUnit) <= _maxLength)
                {
                    return head;
                }
            }

            return string.Empty;
        }

        private static string ConvertWidthOrKana(string text, VbStrConv conversion)
        {
            try
            {
                return Strings.StrConv(text, conversion, JapaneseLocaleId);
            }
            catch (ArgumentException)
            {
                // 日本語の変換が使えない環境では、変換しない（入力できない文字は捨てられる）
                return text;
            }
        }

        private static string EnlargeSmallKana(string text)
        {
            StringBuilder result = null;

            for (int i = 0; i < text.Length; i++)
            {
                int index = SInputFormat.SmallKanaPairs.IndexOf(text[i]);
                if (index >= 0 && index % 2 == 0)
                {
                    if (result == null)
                    {
                        result = new StringBuilder(text);
                    }
                    result[i] = SInputFormat.SmallKanaPairs[index + 1];
                }
            }

            return result == null ? text : result.ToString();
        }
    }
}
