using System;
using System.Collections.Generic;
using System.Text;

namespace App.Controls
{
    /// <summary>
    /// 入力できる文字の種類を表す書式。InputMan（GcTextBox）の Format プロパティと同じ書き方の文字列から作る。
    /// <para>
    /// 書式は、下のキーワードを並べた文字列で、並べたキーワードのどれかに当てはまる文字を入力できる。
    /// <c>^</c> より後ろに書いたものは「入力できない文字」になる（例: <c>H^@</c> は半角文字のうち記号以外）。
    /// <c>^</c> より前に何も書かなければ「後ろに書いたもの以外すべて」になる（例: <c>^@</c> は半角記号以外すべて）。
    /// キーワードと同じ字そのものを指定したいときは、前に <c>\</c> を付ける。空文字列は制限なし。
    /// </para>
    /// <para>
    /// 半角のキーワード: A（英大文字）、a（英小文字）、K（カタカナ）、N（カタカナ。小書きの ｧ ｯ などを除く）、
    /// 9（数字）、#（数字と + - $ % \ , .）、@（記号）、B（0 と 1）、X（16 進数）、S（空白）、H（すべての半角文字）。
    /// </para>
    /// <para>
    /// 全角のキーワード: Ａ ａ Ｋ Ｎ ９ ＃ ＠ Ｂ Ｘ Ｓ（半角の同じ字と同じ意味の全角版）、Ｊ（ひらがな）、
    /// Ｇ（ひらがな。小書きを除く）、Ｚ（すべての全角文字）、Ｔ（サロゲート ペアの文字）、
    /// Ｄ（サロゲート ペア以外の全角文字）、Ｉ（JIS X 0208 の文字）、Ｍ（Shift JIS の全角文字）、Ｖ（異体字セレクター）。
    /// </para>
    /// </summary>
    /// <remarks>
    /// 空白（半角・全角）を入力できるかどうかは、この書式ではなく <see cref="SAllowSpace"/> で決める。
    /// 判定は UTF-16 の 1 文字ごとに行う。サロゲート ペアは前半・後半をそれぞれ 1 文字として判定する。
    /// </remarks>
    public sealed class SInputFormat
    {
        /// <summary>小書きのかなと、対応する通常の大きさのかなを、2 文字ずつ並べたもの。</summary>
        internal const string SmallKanaPairs =
            "ぁあぃいぅうぇえぉおっつゃやゅゆょよゎわゕかゖけ" +
            "ァアィイゥウェエォオッツャヤュユョヨヮワヵカヶケ" +
            "ｧｱｨｲｩｳｪｴｫｵｯﾂｬﾔｭﾕｮﾖ";

        private delegate bool CharTest(char c);

        /// <summary>制限なし（空の書式）。</summary>
        public static readonly SInputFormat None = new SInputFormat(string.Empty);

        private static readonly Dictionary<char, CharTest> Keywords = CreateKeywords();

        // Shift JIS へ変換できない文字は 0 バイトになるようにして、例外を使わずに判定する
        private static readonly Encoding ShiftJis =
            Encoding.GetEncoding(932, new EncoderReplacementFallback(string.Empty), DecoderFallback.ReplacementFallback);

        private readonly string _pattern;
        private readonly CharTest[] _includes;
        private readonly CharTest[] _excludes;

        /// <param name="pattern">書式の文字列。null と空文字列は制限なし。</param>
        public SInputFormat(string pattern)
        {
            _pattern = pattern ?? string.Empty;

            var includes = new List<CharTest>();
            var excludes = new List<CharTest>();
            List<CharTest> current = includes;

            for (int i = 0; i < _pattern.Length; i++)
            {
                char keyword = _pattern[i];

                if (keyword == '\\')
                {
                    // 次の 1 文字を、キーワードではなくその字そのものとして扱う
                    if (i + 1 < _pattern.Length)
                    {
                        current.Add(CreateLiteralTest(_pattern[++i]));
                    }
                    continue;
                }

                if (keyword == '^' && current == includes)
                {
                    current = excludes;
                    continue;
                }

                CharTest test;
                if (!Keywords.TryGetValue(keyword, out test))
                {
                    test = CreateLiteralTest(keyword);
                }
                current.Add(test);
            }

            _includes = includes.ToArray();
            _excludes = excludes.ToArray();
        }

        /// <summary>書式の文字列。</summary>
        public string Pattern
        {
            get { return _pattern; }
        }

        /// <summary>入力できる文字を制限しているか。</summary>
        public bool IsRestricted
        {
            get { return _includes.Length > 0 || _excludes.Length > 0; }
        }

        /// <summary>
        /// <paramref name="c"/> を入力できるか。空白と制御文字（改行・タブ）は対象外で、呼び出し側で別に判定すること。
        /// </summary>
        public bool Allows(char c)
        {
            if (_includes.Length > 0 && !MatchesAny(_includes, c))
            {
                return false;
            }
            return !MatchesAny(_excludes, c);
        }

        public override string ToString()
        {
            return _pattern;
        }

        private static bool MatchesAny(CharTest[] tests, char c)
        {
            foreach (CharTest test in tests)
            {
                if (test(c))
                {
                    return true;
                }
            }
            return false;
        }

        private static CharTest CreateLiteralTest(char literal)
        {
            return delegate(char c) { return c == literal; };
        }

        private static Dictionary<char, CharTest> CreateKeywords()
        {
            var keywords = new Dictionary<char, CharTest>();

            // 半角
            keywords.Add('A', delegate(char c) { return c >= 'A' && c <= 'Z'; });
            keywords.Add('a', delegate(char c) { return c >= 'a' && c <= 'z'; });
            keywords.Add('K', IsHalfKatakana);
            keywords.Add('N', delegate(char c) { return IsHalfKatakana(c) && !IsSmallKana(c); });
            keywords.Add('9', delegate(char c) { return c >= '0' && c <= '9'; });
            keywords.Add('#', delegate(char c) { return (c >= '0' && c <= '9') || "+-$%\\,.".IndexOf(c) >= 0; });
            keywords.Add('@', IsHalfSymbol);
            keywords.Add('B', delegate(char c) { return c == '0' || c == '1'; });
            keywords.Add('X', delegate(char c) { return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f'); });
            keywords.Add('S', delegate(char c) { return c == ' '; });
            keywords.Add('H', IsHalfWidth);

            // 全角
            keywords.Add('Ａ', delegate(char c) { return c >= 'Ａ' && c <= 'Ｚ'; });
            keywords.Add('ａ', delegate(char c) { return c >= 'ａ' && c <= 'ｚ'; });
            keywords.Add('Ｋ', IsFullKatakana);
            keywords.Add('Ｎ', delegate(char c) { return IsFullKatakana(c) && !IsSmallKana(c); });
            keywords.Add('９', delegate(char c) { return c >= '０' && c <= '９'; });
            keywords.Add('＃', delegate(char c) { return (c >= '０' && c <= '９') || "＋－＄％￥＼，．".IndexOf(c) >= 0; });
            keywords.Add('＠', IsFullSymbol);
            keywords.Add('Ｂ', delegate(char c) { return c == '０' || c == '１'; });
            keywords.Add('Ｘ', delegate(char c) { return (c >= '０' && c <= '９') || (c >= 'Ａ' && c <= 'Ｆ') || (c >= 'ａ' && c <= 'ｆ'); });
            keywords.Add('Ｓ', delegate(char c) { return c == '　'; });
            keywords.Add('Ｊ', IsHiragana);
            keywords.Add('Ｇ', delegate(char c) { return IsHiragana(c) && !IsSmallKana(c); });
            keywords.Add('Ｚ', IsFullWidth);
            keywords.Add('Ｔ', char.IsSurrogate);
            keywords.Add('Ｄ', delegate(char c) { return IsFullWidth(c) && !char.IsSurrogate(c); });
            keywords.Add('Ｉ', IsJisX0208);
            keywords.Add('Ｍ', delegate(char c) { return GetShiftJisLeadByte(c) >= 0; });
            keywords.Add('Ｖ', IsVariationSelectorPart);

            return keywords;
        }

        /// <summary>半角文字（ASCII の表示できる文字と、半角カタカナ・半角の句読点）か。</summary>
        internal static bool IsHalfWidth(char c)
        {
            return (c >= ' ' && c <= '~') || (c >= '｡' && c <= 'ﾟ');
        }

        /// <summary>全角文字（半角文字と制御文字を除く、すべての文字）か。</summary>
        internal static bool IsFullWidth(char c)
        {
            return !IsHalfWidth(c) && !char.IsControl(c);
        }

        /// <summary>半角カタカナ（ｦ～ﾟ。長音・濁点・半濁点を含む）か。</summary>
        private static bool IsHalfKatakana(char c)
        {
            return c >= 'ｦ' && c <= 'ﾟ';
        }

        /// <summary>全角カタカナ（ァ～ヺ と、長音・繰り返し記号）か。</summary>
        private static bool IsFullKatakana(char c)
        {
            return (c >= 'ァ' && c <= 'ヺ') || (c >= 'ー' && c <= 'ヾ');
        }

        /// <summary>ひらがな（ぁ～ゖ と、長音・繰り返し記号）か。</summary>
        private static bool IsHiragana(char c)
        {
            return (c >= 'ぁ' && c <= 'ゖ') || c == 'ー' || c == 'ゝ' || c == 'ゞ';
        }

        /// <summary>小書きのかな（ぁ っ ゃ、ァ ッ ャ、ｧ ｯ ｬ など）か。</summary>
        private static bool IsSmallKana(char c)
        {
            // 表は「小書き・通常」の順に並んでいるので、偶数番目にあれば小書き
            int index = SmallKanaPairs.IndexOf(c);
            return index >= 0 && index % 2 == 0;
        }

        /// <summary>半角の記号（ASCII の記号と、半角の句読点 ｡ ｢ ｣ ､ ･）か。</summary>
        private static bool IsHalfSymbol(char c)
        {
            if (c >= '｡' && c <= '･')
            {
                return true;
            }
            return c > ' ' && c <= '~' && !char.IsLetterOrDigit(c);
        }

        /// <summary>全角の記号（全角文字のうち、句読点・記号に分類されるもの）か。</summary>
        private static bool IsFullSymbol(char c)
        {
            return IsFullWidth(c) && !char.IsSurrogate(c) && (char.IsPunctuation(c) || char.IsSymbol(c));
        }

        /// <summary>
        /// 異体字セレクター（U+E0100～U+E01EF）を構成する UTF-16 の文字か。
        /// 後半の文字だけでは、ほかのサロゲート ペアの後半と区別できないので、値の範囲だけで判定している。
        /// </summary>
        private static bool IsVariationSelectorPart(char c)
        {
            return c == '\uDB40' || (c >= '\uDD00' && c <= '\uDDEF');
        }

        /// <summary>JIS X 0208（JIS 第 1・第 2 水準の漢字と、非漢字）の文字か。</summary>
        private static bool IsJisX0208(char c)
        {
            // Shift JIS の 1 バイト目で区を判定する。0x81～0x84 が 1～8 区（非漢字）、
            // 0x88～0x9F と 0xE0～0xEA が 16～84 区（漢字）。それ以外は機種依存文字・拡張文字
            int lead = GetShiftJisLeadByte(c);
            return (lead >= 0x81 && lead <= 0x84) || (lead >= 0x88 && lead <= 0x9F) || (lead >= 0xE0 && lead <= 0xEA);
        }

        /// <summary>
        /// Shift JIS で 2 バイトになる文字の、1 バイト目を返す。2 バイトにならない文字（半角文字、変換できない文字）は -1。
        /// </summary>
        private static int GetShiftJisLeadByte(char c)
        {
            if (char.IsSurrogate(c))
            {
                return -1;
            }

            byte[] bytes = ShiftJis.GetBytes(new[] { c });
            return bytes.Length == 2 ? bytes[0] : -1;
        }
    }
}
