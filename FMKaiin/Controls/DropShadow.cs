using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace App.Controls
{
    /// <summary>
    /// カードやパネルの下に落とす、柔らかい影の寸法と描画。
    /// 影はコントロールの領域内に描くので、影を付ける面（カードやパネルの本体）はコントロールより一回り小さくなる。
    /// 上と左右は影の広がり（<see cref="SideInset"/>）の分、下は影の大きさ（<see cref="Size"/>）の分だけ内側になる。
    /// 影の大きさが 0 のときは、面はコントロールいっぱいになり、影は描かない。
    /// </summary>
    internal struct DropShadow
    {
        /// <summary>影の大きさのうち、影を下へずらす量に充てる割合。残りがぼかしの幅になる。</summary>
        private const float OffsetRatio = 0.375f;
        /// <summary>影の最も濃い部分の不透明度（0～255）。</summary>
        private const int MaxAlpha = 45;

        /// <summary>面の下に影が伸びる長さ（実際の画素数）。</summary>
        public readonly int Size;
        /// <summary>影を下へずらす量。</summary>
        public readonly int Offset;
        /// <summary>影が外側へ薄れていく幅。</summary>
        public readonly int Blur;
        /// <summary>面の上と左右に影が出る幅。面はこの分だけコントロールの内側になる。</summary>
        public readonly int SideInset;

        /// <param name="size">面の下に影が伸びる長さ。96 DPI 基準のピクセル。</param>
        /// <param name="scale">96 DPI を 1 とした表示倍率。</param>
        public DropShadow(int size, float scale)
        {
            // 影の元になる矩形は、面を左右から Offset ずつ縮めて同じだけ下へずらしたもので、
            // そこから Blur の幅で外側へ薄れていく。真下に濃く、左右と上にはわずかに出る影になる
            Size = (int)Math.Round(Math.Max(0, size) * scale);
            Offset = (int)Math.Round(Size * OffsetRatio);
            Blur = Size - Offset;
            SideInset = Math.Max(0, Blur - Offset);
        }

        /// <summary>
        /// 大きさが <paramref name="width"/> × <paramref name="height"/> のコントロールの中で、影を付ける面が占める範囲を返す。
        /// </summary>
        public RectangleF GetSurfaceBounds(int width, int height)
        {
            return new RectangleF(
                SideInset,
                SideInset,
                width - 1 - SideInset * 2,
                height - 1 - SideInset - Size);
        }

        /// <summary>
        /// <paramref name="surface"/>（影を付ける面）の影を描く。
        /// 面が半透明の場合は、影が面越しに透けて見えないよう、呼び出し側で面の範囲を描画対象から外しておくこと。
        /// </summary>
        /// <param name="radius">面の角の半径（実際の画素数）。</param>
        /// <param name="color">影の色。濃さはここで薄めるので、色味だけが使われる。</param>
        /// <param name="strength">濃さの倍率。1 が標準。</param>
        public void Draw(Graphics g, RectangleF surface, float radius, Color color, float strength)
        {
            var shadowBase = new RectangleF(
                surface.X + Offset,
                surface.Y + Offset,
                surface.Width - Offset * 2,
                surface.Height);
            if (Blur <= 0 || shadowBase.Width <= 0f || shadowBase.Height <= 0f)
            {
                return;
            }

            // 1 ピクセルずつ大きさを変えた半透明の角丸矩形を重ねる。外側の層ほど薄くしてあり、
            // 重なった結果は縁からの距離の 2 乗に沿って薄れる（縁の近くで濃く、遠くでは緩やかに消える）
            for (int layer = Blur; layer >= 1; layer--)
            {
                float weight = (2f * (Blur - layer) + 1f) / (Blur * Blur);
                int alpha = (int)Math.Round(MaxAlpha * strength * weight);
                if (alpha <= 0)
                {
                    continue;
                }

                RectangleF layerBounds = RectangleF.Inflate(shadowBase, layer, layer);
                using (GraphicsPath path = PaintHelper.CreateRoundedRectangle(layerBounds, radius + layer))
                using (var brush = new SolidBrush(Color.FromArgb(Math.Min(255, alpha), color)))
                {
                    g.FillPath(brush, path);
                }
            }
        }
    }
}
