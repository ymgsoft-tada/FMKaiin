using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using App.Controls;
using ComponentGControlDB;

namespace App
{
    /// <summary>
    /// 既存の入力ルールの定義（comset の GControlDBRule。AppDbRule が作っているもの）を、
    /// S 系のコントロール用のルール（<see cref="SControlRule"/>）に読み替える。
    /// </summary>
    /// <remarks>
    /// テスト専用。FMKaiin のプロジェクトには含めない（Gc 系に依存するので、アプリの S 系の側からは使わない）。
    /// S 系の画面のルールは、S 系だけで書いた定義（AppSControlRule）を使う。Gc 系の画面用の定義（AppDbRule）とは、
    /// 同じ内容を 2 か所に書く。このクラスは、その 2 つが同じ内容になっているかを確かめるために使う
    /// （AppDbRule の定義を読み替えたものと、AppSControlRule の定義を、項目ごとに比べる）。
    /// すべての画面が S 系に移り、AppDbRule をやめたら、このファイルも不要になる。
    /// </remarks>
    public static class SControlDBRuleConverter
    {
        /// <summary>
        /// <paramref name="source"/> に登録されているルールを、そのまま引ける <see cref="SControlDBRule"/> を作る。
        /// 読み替えは、ルールが最初に使われたときに行う。
        /// </summary>
        public static SControlDBRule Wrap(GControlDBRule source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            // 同じルールが複数の項目に登録されていることが多いので、読み替えた結果を覚えておく
            var converted = new Dictionary<GControlDBRuleBase, SControlRule>();

            var rule = new SControlDBRule();
            rule.Fallback = delegate(string rowField)
            {
                GControlDBRuleBase original = source.GetRule(rowField, string.Empty, false);
                if (original == null)
                {
                    return null;
                }

                SControlRule result;
                if (!converted.TryGetValue(original, out result))
                {
                    result = Convert(original);
                    converted.Add(original, result);
                }
                return result;
            };
            return rule;
        }

        /// <summary>
        /// GControlDBRule のルール 1 つを、<see cref="SControlRule"/> に読み替える。
        /// </summary>
        /// <remarks>
        /// GControlDBRule のルールは、設定ごとに配列を持ち、配列の要素が 1 つならすべての入力欄に、
        /// 複数なら入力欄ごとに使う。ここでは、いちばん長い配列に合わせて、入力欄ごとのルールに展開する。
        /// 文字の位置は、左右だけを引き継ぐ（Gc 用の定義は上下が「下」になっているが、S 系では上下を変えない）。
        /// </remarks>
        public static SControlRule Convert(GControlDBRuleBase source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            int count = 1;
            foreach (Array array in new Array[]
            {
                source.MaxLength, source.Format, source.AllowSpace, source.ImeMode, source.Align,
                source.HighlightText, source.AcceptsCrLf, source.MaxLengthUnit, source.SubLength
            })
            {
                if (array != null)
                {
                    count = Math.Max(count, array.Length);
                }
            }

            // 数値のルール（GControlDBRuleNumber / GControlDBRuleCurrency）だけが、小数の桁数を持っている
            bool isNumber = source.SubLength != null;

            var parts = new SFieldRule[count];
            for (int i = 0; i < count; i++)
            {
                var part = new SFieldRule();
                part.MaxLength = Pick(source.MaxLength, i, 0);
                part.Format = Pick(source.Format, i, string.Empty) ?? string.Empty;
                part.ImeMode = Pick(source.ImeMode, i, ImeMode.NoControl);
                part.HighlightText = Pick(source.HighlightText, i, true);
                part.TextAlign = ToHorizontal(Pick(source.Align, i, ContentAlignment.MiddleLeft));

                // GrapeCity の列挙型は、同じ名前の S 系の列挙型に読み替える
                if (source.AllowSpace != null)
                {
                    part.AllowSpace = ToEnum<SAllowSpace>(Pick(source.AllowSpace, i, default(GrapeCity.Win.Editors.AllowSpace)));
                }
                if (source.MaxLengthUnit != null)
                {
                    part.MaxLengthUnit = ToEnum<SLengthUnit>(Pick(source.MaxLengthUnit, i, default(GrapeCity.Win.Editors.LengthUnit)));
                }
                if (source.AcceptsCrLf != null)
                {
                    part.AcceptsCrLf = ToEnum<SCrLfMode>(Pick(source.AcceptsCrLf, i, default(GrapeCity.Win.Editors.CrLfMode)));
                }

                if (isNumber)
                {
                    part.IsNumber = true;
                    part.DecimalPlaces = Pick(source.SubLength, i, 0);
                    part.ThousandsSeparator = !string.IsNullOrEmpty(Pick(source.CommaDelimiter, i, string.Empty));
                    part.AllowNegative = Pick(source.MinValue, i, 0m) < 0m;

                    // AlternateBlank は読み替えない。名前は「空欄を許すか」だが、GcNumberEx での実際の働きは
                    // 「値が 0 のときに何と表示するか」（false なら空欄で表示）で、値が無い項目を 0 にするものではない。
                    // S 系では、値が無い項目は空欄のまま（AllowBlank は既定の true）、値が 0 の項目は「0」と表示する
                }

                parts[i] = part;
            }
            return new SControlRule(parts);
        }

        /// <summary>
        /// 配列から、<paramref name="index"/> 番目の入力欄に使う値を取り出す（要素が 1 つなら、どの入力欄にもそれを使う）。
        /// </summary>
        private static T Pick<T>(T[] values, int index, T fallback)
        {
            if (values == null || values.Length == 0)
            {
                return fallback;
            }
            return values.Length > 1 ? values[Math.Min(index, values.Length - 1)] : values[0];
        }

        private static T ToEnum<T>(Enum value) where T : struct
        {
            T result;
            return Enum.TryParse(value.ToString(), out result) ? result : default(T);
        }

        private static HorizontalAlignment ToHorizontal(ContentAlignment alignment)
        {
            switch (alignment)
            {
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    return HorizontalAlignment.Center;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    return HorizontalAlignment.Right;
                default:
                    return HorizontalAlignment.Left;
            }
        }
    }
}
