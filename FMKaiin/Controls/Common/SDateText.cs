using System;
using System.Globalization;
using System.Text;

namespace App.Controls
{
    /// <summary>
    /// <see cref="SDateBox"/> に表示・入力する日付の形。
    /// </summary>
    public enum SDateFormat
    {
        /// <summary>年月日。表示は「2026年11月25日」「令和 08年11月25日」。</summary>
        YearMonthDay,
        /// <summary>年月日。表示は「2026/11/25」「令和 08/11/25」。</summary>
        YearMonthDaySlash,
        /// <summary>年月。表示は「2026年11月」「令和 08年11月」。入力した値の日は 1 日になる。</summary>
        YearMonth,
        /// <summary>年。表示は「2026年」「令和 08年」。入力した値は 1 月 1 日になる。</summary>
        Year,
    }

    /// <summary>
    /// 日付を和暦・西暦のどちらで表示するか。
    /// </summary>
    public enum SDateCalendarKind
    {
        /// <summary>アプリ全体の設定（<see cref="SDateBox.DefaultCalendarKind"/>）に従う。</summary>
        Default,
        /// <summary>和暦。</summary>
        Japanese,
        /// <summary>西暦。</summary>
        Western,
    }

    /// <summary>
    /// 日付と文字列の変換（表示用の形、編集用の形、入力された文字列の解釈）。和暦の元号もここで扱う。
    /// </summary>
    /// <remarks>
    /// 元号は <see cref="Eras"/> の表で持っている。Windows や .NET の元号の情報には頼らないので、
    /// 改元があったときは、この表に 1 行足すこと。
    /// </remarks>
    internal static class SDateText
    {
        /// <summary>元号。</summary>
        internal sealed class Era
        {
            public Era(string name, string shortName, char letter, DateTime start)
            {
                Name = name;
                ShortName = shortName;
                Letter = letter;
                Start = start;
            }

            /// <summary>名前（令和）。</summary>
            public readonly string Name;
            /// <summary>1 文字の略称（令）。</summary>
            public readonly string ShortName;
            /// <summary>アルファベットの略称（R）。</summary>
            public readonly char Letter;
            /// <summary>始まった日。</summary>
            public readonly DateTime Start;
        }

        /// <summary>元号の表。古い順に並べること。</summary>
        private static readonly Era[] Eras =
        {
            new Era("明治", "明", 'M', new DateTime(1868, 9, 8)),
            new Era("大正", "大", 'T', new DateTime(1912, 7, 30)),
            new Era("昭和", "昭", 'S', new DateTime(1926, 12, 25)),
            new Era("平成", "平", 'H', new DateTime(1989, 1, 8)),
            new Era("令和", "令", 'R', new DateTime(2019, 5, 1)),
        };

        /// <summary>年月日の区切りとして受け付ける文字。</summary>
        private static readonly char[] Separators = { '/', '-', '.', '年', '月', '日' };

        /// <summary>
        /// 入力欄で入力できる文字を表す書式（<see cref="SInputFormat"/> の書き方）。
        /// 数字、区切り、元号のアルファベット（大文字・小文字）、元号と「年月日」「元」の漢字。
        /// S と H は書式のキーワードなので、その字そのものを表すために \ を付けている。
        /// </summary>
        public const string InputCharacters = "9/-.\\M\\T\\S\\H\\Rmtshr年月日元明治大正昭和平成令";

        #region 元号

        /// <summary>
        /// <paramref name="date"/> の元号と、その元号での年を求める。元号の表より前の日付なら false。
        /// </summary>
        public static bool TryGetEra(DateTime date, out Era era, out int eraYear)
        {
            for (int i = Eras.Length - 1; i >= 0; i--)
            {
                if (date.Date >= Eras[i].Start)
                {
                    era = Eras[i];
                    eraYear = date.Year - era.Start.Year + 1;
                    return true;
                }
            }

            era = null;
            eraYear = 0;
            return false;
        }

        /// <summary>
        /// <paramref name="era"/> の次の元号が始まる日（この日から、<paramref name="era"/> ではなくなる）。いまの元号なら <see cref="DateTime.MaxValue"/>。
        /// </summary>
        private static DateTime GetEraEnd(Era era)
        {
            int index = Array.IndexOf(Eras, era);
            return index >= 0 && index < Eras.Length - 1 ? Eras[index + 1].Start : DateTime.MaxValue;
        }

        /// <summary>
        /// <paramref name="date"/> が表す期間の、最後の日。年の形ならその年の 12 月 31 日、年月の形ならその月の末日、それ以外はその日。
        /// </summary>
        /// <remarks>
        /// 年・年月の形では、元号をこの日で決める。年や月の途中で改元があった場合（2019 年は 4 月まで平成、5 月から令和）に、
        /// 値の「日」によって表示が変わらないようにするため。2019 年は「令和 01年」、2019 年 4 月は「平成 31年04月」になる。
        /// </remarks>
        private static DateTime GetPeriodEnd(DateTime date, SDateFormat format)
        {
            switch (format)
            {
                case SDateFormat.Year:
                    return new DateTime(date.Year, 12, 31);
                case SDateFormat.YearMonth:
                    return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                default:
                    return date.Date;
            }
        }

        #endregion

        #region 文字列にする

        /// <summary>
        /// フォーカスが無いときに表示する文字列（例: 令和 08年11月25日、2026年11月25日）。
        /// 和暦の指定でも、元号の表より前の日付は西暦で表示する。
        /// 年・年月の形では、その年・その月の最後の日の元号で表示する（<see cref="GetPeriodEnd"/>）。
        /// </summary>
        public static string FormatForDisplay(DateTime date, SDateFormat format, bool japanese)
        {
            Era era;
            int eraYear;
            string year = japanese && TryGetEra(GetPeriodEnd(date, format), out era, out eraYear)
                ? era.Name + " " + eraYear.ToString("00", CultureInfo.InvariantCulture)
                : date.Year.ToString("0000", CultureInfo.InvariantCulture);
            string month = date.Month.ToString("00", CultureInfo.InvariantCulture);
            string day = date.Day.ToString("00", CultureInfo.InvariantCulture);

            switch (format)
            {
                case SDateFormat.YearMonthDaySlash:
                    return year + "/" + month + "/" + day;
                case SDateFormat.YearMonth:
                    return year + "年" + month + "月";
                case SDateFormat.Year:
                    return year + "年";
                default:
                    return year + "年" + month + "月" + day + "日";
            }
        }

        /// <summary>
        /// フォーカスがある間、編集用に表示する文字列（例: R08/11/25、2026/11/25）。
        /// </summary>
        public static string FormatForEdit(DateTime date, SDateFormat format, bool japanese)
        {
            Era era;
            int eraYear;
            string year = japanese && TryGetEra(GetPeriodEnd(date, format), out era, out eraYear)
                ? era.Letter + eraYear.ToString("00", CultureInfo.InvariantCulture)
                : date.Year.ToString("0000", CultureInfo.InvariantCulture);

            switch (format)
            {
                case SDateFormat.YearMonth:
                    return year + "/" + date.Month.ToString("00", CultureInfo.InvariantCulture);
                case SDateFormat.Year:
                    return year;
                default:
                    return year + "/" + date.Month.ToString("00", CultureInfo.InvariantCulture)
                        + "/" + date.Day.ToString("00", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>カレンダーの見出しに使う、年月の文字列（例: 令和 08年11月）。</summary>
        public static string FormatYearMonth(DateTime month, bool japanese)
        {
            return FormatForDisplay(month, SDateFormat.YearMonth, japanese);
        }

        /// <summary>カレンダーの見出しに使う、年の文字列（例: 令和 08年）。</summary>
        public static string FormatYear(DateTime year, bool japanese)
        {
            return FormatForDisplay(year, SDateFormat.Year, japanese);
        }

        #endregion

        #region 入力された文字列を解釈する

        /// <summary>
        /// 入力された文字列を日付にする。日付として読めなければ false。
        /// </summary>
        /// <remarks>
        /// <para>
        /// 区切り（/ - . 年 月 日）があれば、区切られた数字を順に 年・月・日 として読む。
        /// 年月日の形で 2 つしか無ければ 月・日（年は今年）として読む。
        /// </para>
        /// <para>
        /// 区切りが無い数字だけの場合は、桁数で読み分ける。年月日の形なら、8 桁は年月日（20261125）、6 桁は年 2 桁 + 月日、
        /// 3～4 桁は月日（今年）、1～2 桁は日（今年の今月）。年月の形なら、6 桁は年月、3～4 桁は年 2 桁 + 月、1～2 桁は月（今年）。
        /// </para>
        /// <para>
        /// 先頭に元号（R・令・令和 など）があれば、年を和暦として読む。元号が無く、年が 1～2 桁のときは、
        /// 和暦の表示なら今の元号の年、西暦の表示なら Windows の設定に従った西暦 2 桁の年として読む。
        /// 「元年」は 1 年。全角で書かれていても読める。
        /// </para>
        /// <para>
        /// 和暦として読んだ日付が、その元号の期間に入っていなければ、読めないものとして扱う（昭和 70 年、平成 31 年 5 月 1 日など）。
        /// 書類の和暦を写し間違えたときに、別の日付として黙って受け付けてしまわないようにするため。
        /// 年・年月の形では、その年・その月のうち 1 日でも元号の期間に入っていれば受け付ける（平成 31 年、令和元年はどちらも 2019 年）。
        /// </para>
        /// </remarks>
        /// <param name="japanese">和暦で表示しているか（元号の無い 1～2 桁の年の読み方に使う）。</param>
        /// <param name="today">今日の日付（省略された年・月を補うのに使う）。</param>
        public static bool TryParse(string text, SDateFormat format, bool japanese, DateTime today, out DateTime date)
        {
            date = DateTime.MinValue;

            string s = Normalize(text);
            if (s.Length == 0)
            {
                return false;
            }

            Era era = TakeEra(ref s);
            string[] parts = s.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0 || parts.Length > 3)
            {
                return false;
            }
            foreach (string part in parts)
            {
                if (!IsDigits(part))
                {
                    return false;
                }
            }

            // 区切りの無い数字は、桁数で 年・月・日 に分ける
            if (parts.Length == 1)
            {
                parts = SplitDigits(parts[0], format, era != null);
                if (parts == null)
                {
                    return false;
                }
            }

            string yearText = null;
            string monthText = null;
            string dayText = null;
            switch (format)
            {
                case SDateFormat.Year:
                    yearText = parts[0];
                    break;

                case SDateFormat.YearMonth:
                    if (parts.Length == 1)
                    {
                        // 月だけ（元号を付けて月だけ、という書き方は受け付けない）
                        if (era != null)
                        {
                            return false;
                        }
                        monthText = parts[0];
                    }
                    else
                    {
                        yearText = parts[0];
                        monthText = parts[1];
                    }
                    break;

                default:
                    if (parts.Length == 3)
                    {
                        yearText = parts[0];
                        monthText = parts[1];
                        dayText = parts[2];
                    }
                    else if (era != null)
                    {
                        // 元号を付けたのに、年・月・日 がそろっていない
                        return false;
                    }
                    else if (parts.Length == 2)
                    {
                        monthText = parts[0];
                        dayText = parts[1];
                    }
                    else
                    {
                        dayText = parts[0];
                    }
                    break;
            }

            // era には、年を和暦として読んだ場合に、その元号が入る（元号を書かずに 1～2 桁の年を入力した場合は、今の元号）
            int year = today.Year;
            if (yearText != null && !TryResolveYear(yearText, ref era, japanese, today, out year))
            {
                return false;
            }

            // 省略されたものを補う。年月日の形で日だけ入力されたら今月、それ以外で月が無ければ 1 月。日が無ければ 1 日
            int month = monthText != null ? ToNumber(monthText)
                : dayText != null ? today.Month
                : 1;
            int day = dayText != null ? ToNumber(dayText) : 1;

            if (year < 1 || year > 9999 || month < 1 || month > 12 || day < 1 || day > DateTime.DaysInMonth(year, month))
            {
                return false;
            }

            var parsed = new DateTime(year, month, day);
            if (era != null && yearText != null && !IsWithinEra(parsed, GetPeriodEnd(parsed, format), era))
            {
                return false;
            }

            date = parsed;
            return true;
        }

        /// <summary>
        /// <paramref name="start"/> から <paramref name="end"/> までの期間に、<paramref name="era"/> の日が 1 日でも含まれるか。
        /// </summary>
        private static bool IsWithinEra(DateTime start, DateTime end, Era era)
        {
            return end >= era.Start && start < GetEraEnd(era);
        }

        /// <summary>
        /// 比べやすい形にする。全角の英数字・記号を半角に、英字を大文字にし、空白を取り除く。「元年」は「1年」にする。
        /// </summary>
        private static string Normalize(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            string normalized;
            try
            {
                normalized = text.Normalize(NormalizationForm.FormKC);
            }
            catch (ArgumentException)
            {
                normalized = text;
            }

            var builder = new StringBuilder(normalized.Length);
            foreach (char c in normalized.ToUpperInvariant())
            {
                if (c == '元')
                {
                    builder.Append('1');
                }
                else if (!char.IsWhiteSpace(c))
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 先頭に元号（名前・1 文字の略称・アルファベット）があれば、それを取り除いて返す。無ければ null。
        /// </summary>
        private static Era TakeEra(ref string s)
        {
            foreach (Era era in Eras)
            {
                int length = 0;
                if (s.StartsWith(era.Name, StringComparison.Ordinal))
                {
                    length = era.Name.Length;
                }
                else if (s.StartsWith(era.ShortName, StringComparison.Ordinal))
                {
                    length = era.ShortName.Length;
                }
                else if (s[0] == era.Letter)
                {
                    length = 1;
                }

                if (length > 0)
                {
                    // 「R.08/11/25」のように、元号の後ろに区切りがあってもよい
                    s = s.Substring(length).TrimStart('.');
                    return era;
                }
            }
            return null;
        }

        /// <summary>
        /// 区切りの無い数字を、桁数で 年・月・日 に分ける。読み方が決まらない桁数なら null。
        /// </summary>
        /// <param name="hasEra">先頭に元号が付いていたか（付いていれば、年は必ず含まれる）。</param>
        private static string[] SplitDigits(string digits, SDateFormat format, bool hasEra)
        {
            int length = digits.Length;
            switch (format)
            {
                case SDateFormat.Year:
                    // 年だけ。4 桁までなら、そのまま年として読む
                    return length <= 4 ? new[] { digits } : null;

                case SDateFormat.YearMonth:
                    if (length == 6 && !hasEra)
                    {
                        return new[] { digits.Substring(0, 4), digits.Substring(4) };
                    }
                    if (length == 3 || length == 4)
                    {
                        // 年 1～2 桁 + 月 2 桁
                        return new[] { digits.Substring(0, length - 2), digits.Substring(length - 2) };
                    }
                    if (length <= 2 && !hasEra)
                    {
                        return new[] { digits };
                    }
                    return null;

                default:
                    if (length == 8 && !hasEra)
                    {
                        return new[] { digits.Substring(0, 4), digits.Substring(4, 2), digits.Substring(6) };
                    }
                    if (length == 6 || (length == 5 && hasEra))
                    {
                        // 年 1～2 桁 + 月 2 桁 + 日 2 桁
                        return new[] { digits.Substring(0, length - 4), digits.Substring(length - 4, 2), digits.Substring(length - 2) };
                    }
                    if (hasEra)
                    {
                        return null;
                    }
                    if (length == 3 || length == 4)
                    {
                        // 月 1～2 桁 + 日 2 桁
                        return new[] { digits.Substring(0, length - 2), digits.Substring(length - 2) };
                    }
                    return length <= 2 ? new[] { digits } : null;
            }
        }

        /// <summary>
        /// 年の数字を、西暦の年にする。
        /// </summary>
        /// <param name="era">
        /// 入力に書かれていた元号（無ければ null）。元号が無い 1～2 桁の年を今の元号の年として読んだ場合は、その元号を入れて返す。
        /// </param>
        private static bool TryResolveYear(string yearText, ref Era era, bool japanese, DateTime today, out int year)
        {
            int number = ToNumber(yearText);
            year = 0;

            if (era == null && yearText.Length >= 3)
            {
                // 3 桁以上は西暦
                year = number;
                return true;
            }

            if (era == null && japanese)
            {
                // 元号が無い 1～2 桁の年は、今の元号の年として読む
                int ignored;
                if (!TryGetEra(today, out era, out ignored))
                {
                    return false;
                }
            }

            if (era != null)
            {
                if (number < 1)
                {
                    return false;
                }
                year = era.Start.Year + number - 1;
                return true;
            }

            // 西暦の 2 桁の年。どの 100 年に当てるかは、Windows の設定（地域の「2 桁の年」の解釈）に従う
            year = new GregorianCalendar().ToFourDigitYear(number);
            return true;
        }

        private static bool IsDigits(string text)
        {
            if (text.Length == 0 || text.Length > 8)
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

        private static int ToNumber(string digits)
        {
            return int.Parse(digits, NumberStyles.None, CultureInfo.InvariantCulture);
        }

        #endregion
    }
}
