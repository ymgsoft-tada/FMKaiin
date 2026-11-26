using System.Drawing;

namespace App.Controls
{
    /// <summary>
    /// メッセージ ボックス（<see cref="SMsgBox"/>）のボタン。<see cref="SButton"/> を、メッセージ ボックス向けに設定したもの。
    /// フォーカスがあるものを濃い青で示す（Enter を押したときに働くボタンが、ひと目で分かるように）。
    /// </summary>
    internal sealed class SMsgBoxButton : SButton
    {
        public SMsgBoxButton()
        {
            HighlightWhenFocused = true;
        }

        /// <summary>
        /// 角丸の外側は、置いてある場所の色（BackColor に、メッセージ ボックスが設定する）で塗る。
        /// メッセージ ボックスは、背景を自前で描いていて、親と同じ見た目を写す方法では、帯の色にならないため。
        /// </summary>
        protected override void PaintOutside(Graphics g)
        {
            g.Clear(BackColor);
        }
    }
}
