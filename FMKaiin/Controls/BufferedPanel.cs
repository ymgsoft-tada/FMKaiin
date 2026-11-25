using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 描画を 1 回にまとめる <see cref="Panel"/>。使い方は標準の Panel と同じ。
    /// 標準の Panel は、位置や大きさが変わるたびに背景だけを先に描き直す。背景が単色ならそれで問題ないが、
    /// BackColor を Transparent にして親の描画内容（背景画像やガラス調のパネル）を透かしている場合は、
    /// その描き直しが重く、ウィンドウの大きさを変えるときにもたつく原因になる。
    /// このパネルは背景も通常の描画のタイミングでまとめて描くので、描き直しが 1 回で済む。
    /// </summary>
    public class BufferedPanel : Panel
    {
        public BufferedPanel()
        {
            // 背景だけを先に描く処理を止め、裏で描いてから一度に表示する
            DoubleBuffered = true;
        }
    }
}
