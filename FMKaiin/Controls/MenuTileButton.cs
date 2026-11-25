using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 業務メニューのタイル型ボタン。
    /// 角丸の枠の中に、色付きバッジに載せたアイコンと文字列を横並びで表示する。
    /// 寸法のプロパティはすべて 96 DPI 基準のピクセルで指定する。
    /// </summary>
    public class MenuTileButton : IconButtonBase
    {
        /// <summary>枠の角の半径。</summary>
        private const float CornerRadius = 5f;
        /// <summary>バッジの角の半径。</summary>
        private const float BadgeCornerRadius = 5f;
        /// <summary>枠の左端からバッジまでの間隔。</summary>
        private const float BadgeLeft = 9f;
        /// <summary>バッジの一辺に対するアイコンの一辺の比率。</summary>
        private const float IconRatio = 0.62f;
        /// <summary>文字列の右側に確保する余白。</summary>
        private const float TextRightPadding = 4f;

        private static readonly Color DefaultFillColor = Color.White;
        private static readonly Color DefaultBorderColor = Color.FromArgb(186, 197, 211);
        private static readonly Color DefaultBadgeColor = Color.FromArgb(217, 240, 252);

        private Color _fillColor = DefaultFillColor;
        private Color _borderColor = DefaultBorderColor;
        private Color _badgeColor = DefaultBadgeColor;
        private int _badgeSize = 42;
        private int _textGap = 14;

        /// <summary>タイルの塗り色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "White")]
        [Description("タイルの塗り色。")]
        public Color FillColor
        {
            get { return _fillColor; }
            set { _fillColor = value; Invalidate(); }
        }

        /// <summary>タイルの枠線の色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "186, 197, 211")]
        [Description("タイルの枠線の色。")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        /// <summary>アイコンの背景（バッジ）の色。マウスオーバー時の色もここから作る。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "217, 240, 252")]
        [Description("アイコンの背景（バッジ）の色。マウスオーバー時の色もここから作る。")]
        public Color BadgeColor
        {
            get { return _badgeColor; }
            set { _badgeColor = value; Invalidate(); }
        }

        /// <summary>バッジの一辺。タイルの高さに収まらない場合は自動で縮む。</summary>
        [Category("Appearance")]
        [DefaultValue(42)]
        [Description("バッジの一辺。タイルの高さに収まらない場合は自動で縮む。")]
        public int BadgeSize
        {
            get { return _badgeSize; }
            set { _badgeSize = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>バッジと文字列の間隔。</summary>
        [Category("Appearance")]
        [DefaultValue(14)]
        [Description("バッジと文字列の間隔。")]
        public int TextGap
        {
            get { return _textGap; }
            set { _textGap = Math.Max(0, value); Invalidate(); }
        }

        protected override void PaintButton(Graphics g)
        {
            float scale = DpiScale;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color fill = _fillColor;
            Color border = _borderColor;
            Color badge = _badgeColor;
            if (!Enabled)
            {
                badge = PaintHelper.Blend(_badgeColor, _fillColor, 0.5f);
            }
            else if (IsPressed)
            {
                fill = PaintHelper.Blend(_fillColor, _badgeColor, 0.7f);
                border = PaintHelper.Blend(_borderColor, ForeColor, 0.35f);
                badge = PaintHelper.Blend(_badgeColor, ForeColor, 0.08f);
            }
            else if (IsHovered)
            {
                fill = PaintHelper.Blend(_fillColor, _badgeColor, 0.35f);
                border = PaintHelper.Blend(_borderColor, ForeColor, 0.25f);
                badge = PaintHelper.Blend(_badgeColor, ForeColor, 0.04f);
            }

            // 枠
            var frameBounds = new RectangleF(0f, 0f, Width - 1, Height - 1);
            using (GraphicsPath frame = PaintHelper.CreateRoundedRectangle(frameBounds, CornerRadius * scale))
            using (var fillBrush = new SolidBrush(fill))
            using (var borderPen = new Pen(border, Math.Max(1f, (float)Math.Round(scale))))
            {
                g.FillPath(fillBrush, frame);
                g.DrawPath(borderPen, frame);
            }

            // キーボード操作時のフォーカス表示
            if (Focused && ShowFocusCues)
            {
                var focusBounds = RectangleF.Inflate(frameBounds, -1f, -1f);
                using (GraphicsPath focus = PaintHelper.CreateRoundedRectangle(focusBounds, (CornerRadius - 1f) * scale))
                using (var focusPen = new Pen(SystemColors.Highlight, Math.Max(1f, (float)Math.Round(scale))))
                {
                    g.DrawPath(focusPen, focus);
                }
            }

            // バッジとアイコン
            float badgeSide = Math.Min(_badgeSize * scale, Height - 4f * scale);
            var badgeBounds = new RectangleF(BadgeLeft * scale, (Height - badgeSide) / 2f, badgeSide, badgeSide);
            if (badgeSide > 0f)
            {
                using (GraphicsPath badgePath = PaintHelper.CreateRoundedRectangle(badgeBounds, BadgeCornerRadius * scale))
                using (var badgeBrush = new SolidBrush(badge))
                {
                    g.FillPath(badgeBrush, badgePath);
                }

                float iconSide = badgeSide * IconRatio;
                var iconBounds = new RectangleF(
                    badgeBounds.X + (badgeSide - iconSide) / 2f,
                    badgeBounds.Y + (badgeSide - iconSide) / 2f,
                    iconSide,
                    iconSide);
                MenuIconPainter.Draw(g, IconKind, iconBounds, CurrentIconColor, IconLineWeight);
            }

            // 文字列
            int textLeft = (int)Math.Round(badgeBounds.Right + _textGap * scale);
            int textRight = Width - (int)Math.Round(TextRightPadding * scale);
            DrawButtonText(g, new Rectangle(textLeft, 0, textRight - textLeft, Height), CurrentTextColor);
        }
    }
}
