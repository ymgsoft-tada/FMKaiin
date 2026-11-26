using System.Collections.Generic;
using System.Linq;
using App.Controls;

namespace App
{
    /// <summary>
    /// コンボ ボックス（<see cref="SComboBox"/>）の、項目の設定。
    /// S 系のコントロールを使う画面用。Gc 系のコントロールを使う画面用の <see cref="AppCombo"/> に当たる。
    /// </summary>
    /// <remarks>
    /// AppCombo と同じ内容を、S 系のコントロール用に書いたもの。画面を S 系へ移している間は、両方を持つ。
    /// このクラスは Gc 系を参照しない。
    /// </remarks>
    public static class AppSCombo
    {
        /// <summary>「すべて」の項目の値。AppCombo.SelectAllValue と同じ値であること。</summary>
        public const int SelectAllValue = -1;

        /// <summary>
        /// 区分の辞書（値と表示名）から、コンボ ボックスの項目を作る。
        /// </summary>
        /// <param name="dics">区分の辞書。キーが値、値が表示名。</param>
        /// <param name="ignores">項目にしない値。</param>
        public static void SetComboBox(SComboBox cmb, Dictionary<int, string> dics, params int[] ignores)
        {
            SetComboBox(cmb, dics, false, ignores);
        }

        /// <summary>
        /// 区分の辞書（値と表示名）から、コンボ ボックスの項目を作る。
        /// </summary>
        /// <param name="dics">区分の辞書。キーが値、値が表示名。</param>
        /// <param name="select_all">true にすると、先頭に「すべて」（値は <see cref="SelectAllValue"/>）を足す。</param>
        /// <param name="ignores">項目にしない値。</param>
        public static void SetComboBox(SComboBox cmb, Dictionary<int, string> dics, bool select_all, params int[] ignores)
        {
            cmb.BeginUpdate();
            try
            {
                cmb.Items.Clear();

                if (select_all == true)
                {
                    cmb.AddItem("すべて", SelectAllValue);
                }

                foreach (KeyValuePair<int, string> kvp in dics)
                {
                    if (ignores == null || ignores.Contains(kvp.Key) == false)
                    {
                        cmb.AddItem(kvp.Value, kvp.Key);
                    }
                }
            }
            finally
            {
                cmb.EndUpdate();
            }
        }

        /// <summary>
        /// 区分の辞書（値と表示名）から、コンボ ボックスの項目を作る。表示は「値：表示名」の形にする（例: 「1：在籍」）。
        /// 区分を、値の範囲（○○ ～ ○○）で指定する欄に使う。値の並びが見えるので、範囲に何が含まれるかが分かる。
        /// 表示名が空の項目（未指定を表す項目）は、空のまま表示する。
        /// </summary>
        /// <param name="dics">区分の辞書。キーが値、値が表示名。</param>
        /// <param name="ignores">項目にしない値。</param>
        public static void SetComboBoxWithCode(SComboBox cmb, Dictionary<int, string> dics, params int[] ignores)
        {
            cmb.BeginUpdate();
            try
            {
                cmb.Items.Clear();

                foreach (KeyValuePair<int, string> kvp in dics)
                {
                    if (ignores == null || ignores.Contains(kvp.Key) == false)
                    {
                        string text = string.IsNullOrEmpty(kvp.Value) ? string.Empty : kvp.Key + "：" + kvp.Value;
                        cmb.AddItem(text, kvp.Key);
                    }
                }
            }
            finally
            {
                cmb.EndUpdate();
            }
        }
    }
}
