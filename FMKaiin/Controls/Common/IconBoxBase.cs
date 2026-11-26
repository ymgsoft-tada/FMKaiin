using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 図柄を 1 つ表示するだけのコントロールの土台。背景と余白を受け持ち、何を描くかは継承したクラスが決める
    /// （<see cref="IconBox"/> は <see cref="MenuIconPainter"/> のアイコンを描く）。
    /// 図柄の色は <see cref="Control.ForeColor"/>、背景は <see cref="Control.BackColor"/>（既定では親と同じ色）を使う。
    /// <see cref="CornerRadius"/> を指定すると背景が角丸になり、色付きのタイルに図柄を載せたロゴのように使える。
    /// 図柄は <see cref="Control.Padding"/> の内側に描く。
    /// </summary>
    public abstract class IconBoxBase : Control
    {
        private int _cornerRadius;

        protected IconBoxBase()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                // 親の BackColor が Transparent のときに、親の描画内容の上へ図柄だけを描けるようにする
                ControlStyles.SupportsTransparentBackColor,
                true);
            SetStyle(ControlStyles.Selectable, false);
            TabStop = false;
        }

        /// <summary>
        /// 背景の角の半径（96 DPI 基準のピクセル）。0 の場合は四角のまま。
        /// 1 以上にすると、角丸の外側は親と同じ見た目（親が透明なら、その後ろの背景）、内側は <see cref="Control.BackColor"/> で塗る。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(0)]
        [Description("背景の角の半径。0 の場合は四角のまま。1 以上にすると、BackColor で塗った角丸の背景になる。")]
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set
            {
                int radius = Math.Max(0, value);
                if (_cornerRadius == radius)
                {
                    return;
                }
                _cornerRadius = radius;
                Invalidate();
            }
        }

        /// <summary>
        /// 図柄を描く。背景は描き終わっている。色は <see cref="Control.ForeColor"/> を使うこと。
        /// </summary>
        /// <param name="bounds">図柄を描く領域（<see cref="Control.Padding"/> の内側）。</param>
        protected abstract void DrawIcon(Graphics g, RectangleF bounds);

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (_cornerRadius <= 0 || BackColor.A < 255)
            {
                base.OnPaintBackground(pevent);
                return;
            }

            // 角丸の外側が親と同じ見た目になるよう、先に全体を親と同じ見た目にしてから、角丸の背景を描く
            // （親の背景が透明の場合は、親の描画内容を写す）
            Graphics g = pevent.Graphics;
            PaintHelper.PaintParentAppearance(this, g, InvokePaintBackground, InvokePaint);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var bounds = new RectangleF(0f, 0f, Width - 1, Height - 1);
            using (GraphicsPath path = PaintHelper.CreateRoundedRectangle(bounds, _cornerRadius * LayoutScale.Of(this)))
            using (var brush = new SolidBrush(BackColor))
            {
                g.FillPath(brush, path);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle bounds = ClientRectangle;
            bounds.X += Padding.Left;
            bounds.Y += Padding.Top;
            bounds.Width -= Padding.Horizontal;
            bounds.Height -= Padding.Vertical;

            DrawIcon(e.Graphics, bounds);
            base.OnPaint(e);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            Invalidate();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            Invalidate();
        }
    }
}
