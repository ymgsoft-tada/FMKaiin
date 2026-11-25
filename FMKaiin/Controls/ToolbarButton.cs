using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// ツールバー用の枠なしボタン。アイコンと文字列を横並びで表示し、マウスオーバー時だけ背景を付ける。
    /// アイコンを指定しない（<see cref="MenuIconKind.None"/>）場合は文字列だけのボタンになる。
    /// 寸法のプロパティはすべて 96 DPI 基準のピクセルで指定する。
    /// </summary>
    public class ToolbarButton : IconButtonBase
    {
        /// <summary>マウスオーバー時の背景の角の半径。</summary>
        private const float CornerRadius = 4f;
        /// <summary>左端からアイコンまでの間隔。</summary>
        private const float IconLeft = 8f;
        /// <summary>アイコンと文字列の間隔。</summary>
        private const float TextGap = 12f;
        /// <summary>文字列の右側に確保する余白。</summary>
        private const float TextRightPadding = 4f;

        private static readonly Color DefaultHoverColor = Color.FromArgb(230, 240, 248);
        private static readonly Color DefaultTextColor = Color.FromArgb(64, 64, 64);

        private Color _hoverColor = DefaultHoverColor;
        private int _iconSize = 22;

        public ToolbarButton()
        {
            // 文字の既定色。親の ForeColor は引き継がず、ツールバー ボタン共通の色にする
            base.ForeColor = DefaultTextColor;
        }

        /// <summary>文字の色。既定は濃いグレー（64, 64, 64）。</summary>
        [DefaultValue(typeof(Color), "64, 64, 64")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        /// <summary>アイコンの既定色は黒。</summary>
        protected override Color DefaultIconColor
        {
            get { return Color.Black; }
        }

        /// <summary>マウスオーバー時の背景色。押下時の色もここから作る。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "230, 240, 248")]
        [Description("マウスオーバー時の背景色。押下時の色もここから作る。")]
        public Color HoverColor
        {
            get { return _hoverColor; }
            set { _hoverColor = value; Invalidate(); }
        }

        /// <summary>アイコンの一辺。</summary>
        [Category("Appearance")]
        [DefaultValue(22)]
        [Description("アイコンの一辺。")]
        public int IconSize
        {
            get { return _iconSize; }
            set { _iconSize = Math.Max(0, value); Invalidate(); }
        }

        protected override void PaintButton(Graphics g)
        {
            float scale = DpiScale;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var bounds = new RectangleF(0f, 0f, Width - 1, Height - 1);

            if (Enabled && (IsPressed || IsHovered))
            {
                Color background = IsPressed ? PaintHelper.Blend(_hoverColor, ForeColor, 0.1f) : _hoverColor;
                using (GraphicsPath path = PaintHelper.CreateRoundedRectangle(bounds, CornerRadius * scale))
                using (var brush = new SolidBrush(background))
                {
                    g.FillPath(brush, path);
                }
            }

            // キーボード操作時のフォーカス表示
            if (Focused && ShowFocusCues)
            {
                using (GraphicsPath focus = PaintHelper.CreateRoundedRectangle(bounds, CornerRadius * scale))
                using (var focusPen = new Pen(SystemColors.Highlight, Math.Max(1f, (float)Math.Round(scale))))
                {
                    g.DrawPath(focusPen, focus);
                }
            }

            // アイコンなしの場合はアイコンの場所を空けず、左端の余白のすぐ後から文字列を描く
            float textStart = IconLeft * scale;
            if (IconKind != MenuIconKind.None)
            {
                float iconSide = Math.Min(_iconSize * scale, Height);
                var iconBounds = new RectangleF(IconLeft * scale, (Height - iconSide) / 2f, iconSide, iconSide);
                MenuIconPainter.Draw(g, IconKind, iconBounds, CurrentIconColor, IconLineWeight);
                textStart = iconBounds.Right + TextGap * scale;
            }

            int textLeft = (int)Math.Round(textStart);
            int textRight = Width - (int)Math.Round(TextRightPadding * scale);
            DrawButtonText(g, new Rectangle(textLeft, 0, textRight - textLeft, Height), CurrentTextColor);
        }
    }
}
