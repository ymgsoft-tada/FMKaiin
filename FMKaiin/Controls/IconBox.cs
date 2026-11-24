using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// <see cref="MenuIconPainter"/> のアイコンを 1 つ表示するだけのコントロール。
    /// アイコンの色は <see cref="Control.ForeColor"/>、背景は <see cref="Control.BackColor"/>（既定では親と同じ色）を使う。
    /// <see cref="CornerRadius"/> を指定すると背景が角丸になり、色付きのタイルにアイコンを載せたロゴのように使える。
    /// アイコンは <see cref="Control.Padding"/> の内側に描く。
    /// </summary>
    public class IconBox : Control
    {
        private MenuIconKind _iconKind = MenuIconKind.None;
        private float _lineWeight = 1f;
        private int _cornerRadius;

        public IconBox()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                // 親の BackColor が Transparent のときに、親の描画内容の上へアイコンだけを描けるようにする
                ControlStyles.SupportsTransparentBackColor,
                true);
            SetStyle(ControlStyles.Selectable, false);
            TabStop = false;
        }

        /// <summary>表示するアイコン。</summary>
        [Category("Appearance")]
        [DefaultValue(MenuIconKind.None)]
        [Description("表示するアイコン。")]
        public MenuIconKind IconKind
        {
            get { return _iconKind; }
            set
            {
                if (_iconKind == value)
                {
                    return;
                }
                _iconKind = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 線の太さの倍率。1 が標準。
        /// アイコンを大きく表示するときに線だけ細く保ちたい場合は 1 より小さくする。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(1f)]
        [Description("線の太さの倍率。1 が標準。大きく表示するときに線を細く保ちたい場合は 1 より小さくする。")]
        public float LineWeight
        {
            get { return _lineWeight; }
            set
            {
                if (value <= 0f || float.IsNaN(value) || float.IsInfinity(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "線の太さの倍率は 0 より大きい値で指定してください。");
                }
                if (_lineWeight == value)
                {
                    return;
                }
                _lineWeight = value;
                Invalidate();
            }
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

            MenuIconPainter.Draw(e.Graphics, _iconKind, bounds, ForeColor, _lineWeight);
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
