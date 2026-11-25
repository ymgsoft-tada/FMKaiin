using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 描画を 1 回にまとめる <see cref="TableLayoutPanel"/>。使い方は標準の TableLayoutPanel と同じ。
    /// 背景を透かして親の描画内容を見せている場合に、位置や大きさが変わるたびの描き直しを減らす
    /// （理由は <see cref="BufferedPanel"/> と同じ）。
    /// </summary>
    public class BufferedTableLayoutPanel : TableLayoutPanel
    {
        public BufferedTableLayoutPanel()
        {
            // 背景だけを先に描く処理を止め、裏で描いてから一度に表示する
            DoubleBuffered = true;
        }
    }
}
