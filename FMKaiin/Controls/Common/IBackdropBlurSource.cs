using System.Drawing;
using System.Drawing.Drawing2D;

namespace App.Controls
{
    /// <summary>
    /// すりガラス調のコントロールに「後ろの背景をぼかしたもの」を提供する側（フォーム）が実装する。
    /// Windows フォームには、後ろをぼかして透かす機能がない。そのため、フォームが背景画像をあらかじめ
    /// ぼかして持っておき、ガラス調のコントロールは自分が重なっている範囲をそこから描いてもらう。
    /// </summary>
    public interface IBackdropBlurSource
    {
        /// <summary>
        /// <paramref name="path"/> の範囲を、ぼかした背景で塗る。
        /// </summary>
        /// <param name="g">描画先。呼び出したコントロールの座標系になっている。</param>
        /// <param name="path">塗る範囲。<paramref name="g"/> の座標系で指定する。</param>
        /// <param name="origin">
        /// <paramref name="g"/> の原点（呼び出したコントロールの左上）が、このフォームの表示領域のどこに当たるか。
        /// 背景のどの部分を描けばよいかを決めるために使う。
        /// </param>
        /// <returns>描いた場合は true。ぼかす背景がない（背景画像を設定していないなど）場合は false。</returns>
        bool FillBlurredBackdrop(Graphics g, GraphicsPath path, Point origin);
    }
}
