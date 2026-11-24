using System;
using System.Windows.Forms;

namespace App
{
    /// <summary>
    /// 処理月度の選択肢。トップメニューの各画面（Form1・Form2）が同じ内容を表示するよう、ここ 1 か所で決める。
    /// 選択肢の範囲は仮の仕様（当月から過去 <see cref="MonthCount"/> か月）なので、
    /// 処理月度の管理方法が決まったらここを差し替える。
    /// </summary>
    internal static class ProcessingMonthChoices
    {
        /// <summary>選択肢として並べる月数（当月から過去へ遡る）。</summary>
        private const int MonthCount = 12;

        /// <summary>
        /// <paramref name="comboBox"/> の項目を処理月度の選択肢に入れ替え、先頭（当月）を選択する。
        /// 各項目は月初日の <see cref="DateTime"/> で、表示形式はコンボボックスの FormatString で決まる。
        /// </summary>
        public static void Fill(ComboBox comboBox)
        {
            if (comboBox == null)
            {
                throw new ArgumentNullException(nameof(comboBox));
            }

            DateTime today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);

            comboBox.BeginUpdate();
            try
            {
                comboBox.Items.Clear();
                for (int i = 0; i < MonthCount; i++)
                {
                    comboBox.Items.Add(currentMonth.AddMonths(-i));
                }
            }
            finally
            {
                comboBox.EndUpdate();
            }

            comboBox.SelectedIndex = 0;
        }
    }
}
