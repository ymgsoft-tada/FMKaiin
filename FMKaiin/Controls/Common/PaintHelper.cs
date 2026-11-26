using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// カスタム描画コントロールで共通して使う描画補助。
    /// </summary>
    internal static class PaintHelper
    {
        /// <summary>
        /// 角丸矩形のパスを作成する。呼び出し側で Dispose すること。
        /// </summary>
        public static GraphicsPath CreateRoundedRectangle(RectangleF bounds, float radius)
        {
            var path = new GraphicsPath();
            float diameter = Math.Min(radius * 2f, Math.Min(bounds.Width, bounds.Height));
            if (diameter <= 0f)
            {
                path.AddRectangle(bounds);
                return path;
            }

            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180f, 90f);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270f, 90f);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0f, 90f);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90f, 90f);
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 枠線用のペンを作成する。呼び出し側で Dispose すること。
        /// <paramref name="end"/> が空（<see cref="Color.Empty"/>）なら <paramref name="start"/> の単色。
        /// 指定した場合は、<paramref name="bounds"/> の左上が <paramref name="start"/>、右下が <paramref name="end"/> になる
        /// グラデーションの線になる。左上から光が当たっているように見せて、枠に立体感を付けるために使う。
        /// </summary>
        public static Pen CreateBorderPen(RectangleF bounds, Color start, Color end, float width)
        {
            if (end.IsEmpty || end.ToArgb() == start.ToArgb() || bounds.Width <= 0f || bounds.Height <= 0f)
            {
                return new Pen(start, width);
            }

            // 色が変わる向きを矩形の縦横比に合わせる。単純な 45 度にすると、横長の矩形では左右方向にしか
            // 変化しなくなるため、右上と左下の角がちょうど中間の色になる向き（幅・高さで正規化した対角方向）にする
            float w = bounds.Width;
            float h = bounds.Height;
            float k = 2f / (1f / (w * w) + 1f / (h * h));
            var from = new PointF(bounds.Left, bounds.Top);
            var to = new PointF(bounds.Left + k / w, bounds.Top + k / h);

            using (var brush = new LinearGradientBrush(from, to, start, end))
            {
                // 線の太さの分だけ矩形からはみ出す部分で、色が急に切り替わらないようにする
                brush.WrapMode = WrapMode.TileFlipXY;

                // Pen はブラシの内容を複製して持つので、ここでブラシを破棄してよい
                return new Pen(brush, width);
            }
        }

        /// <summary>
        /// <paramref name="child"/> の全面を、親コントロールと同じ見た目で塗る。
        /// 角丸の外側のように、コントロール自身が塗らない部分の下地にするために使う。
        /// 親の背景が単色ならその色で塗り、親の背景が透明（さらに後ろの背景を透かしている）なら親の描画内容を写す。
        /// 親の背景色でそのまま塗ると、親が透明のときは色がなく、黒く描かれてしまう。
        /// </summary>
        /// <param name="g">描画先。<paramref name="child"/> の座標系になっていること。</param>
        /// <param name="invokePaintBackground">
        /// <paramref name="child"/> の InvokePaintBackground。Control の保護されたメソッドなので、呼び出し側のコントロールから渡す。
        /// </param>
        /// <param name="invokePaint"><paramref name="child"/> の InvokePaint。同じ理由で呼び出し側から渡す。</param>
        public static void PaintParentAppearance(
            Control child,
            Graphics g,
            Action<Control, PaintEventArgs> invokePaintBackground,
            Action<Control, PaintEventArgs> invokePaint)
        {
            Control parent = child.Parent;
            Color surround = parent != null ? parent.BackColor : child.BackColor;
            if (parent == null || surround.A == 255)
            {
                g.Clear(surround);
                return;
            }

            GraphicsState state = g.Save();
            try
            {
                // 親の座標系に合わせてから、親に自分自身の背景と前景を描かせる
                g.TranslateTransform(-child.Left, -child.Top);
                using (var args = new PaintEventArgs(g, new Rectangle(child.Left, child.Top, child.Width, child.Height)))
                {
                    invokePaintBackground(parent, args);
                    invokePaint(parent, args);
                }
            }
            finally
            {
                g.Restore(state);
            }
        }

        /// <summary>
        /// 2 色を混ぜる。<paramref name="amount"/> が 0 なら <paramref name="from"/>、1 なら <paramref name="to"/>。
        /// 不透明度（アルファ値）も同じ割合で混ぜるので、半透明色どうしでも使える。
        /// </summary>
        public static Color Blend(Color from, Color to, float amount)
        {
            float t = Math.Max(0f, Math.Min(1f, amount));
            return Color.FromArgb(
                (int)Math.Round(from.A + (to.A - from.A) * t),
                (int)Math.Round(from.R + (to.R - from.R) * t),
                (int)Math.Round(from.G + (to.G - from.G) * t),
                (int)Math.Round(from.B + (to.B - from.B) * t));
        }
    }
}
