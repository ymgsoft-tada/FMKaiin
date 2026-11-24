using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 業務メニューのカード型ボタン。
    /// 下に柔らかい影を落とした角丸のカードの中に、アイコンと文字列を表示する。
    /// 並べ方は <see cref="ContentLayout"/> で選ぶ（既定は横並び。縦並びにするとアイコンの下に文字列が来る）。
    /// 影はコントロールの領域内に描くので、カード自体はコントロールより一回り小さくなる
    /// （上と左右は影の広がりの分、下は <see cref="ShadowSize"/> の分だけ内側）。
    /// 寸法のプロパティはすべて 96 DPI 基準のピクセルで指定する。
    /// 塗り色・枠線の色には半透明色（不透明度付きの色。例: 110, 255, 255, 255）も指定できる。
    /// 半透明のパネルの上に置いてガラスのように見せる場合は、BackColor を Transparent にするか、
    /// 親コントロールの BackColor を Transparent にしておく（引き継がれる）。
    /// </summary>
    public class CardTileButton : IconButtonBase
    {

        /// <summary>文字列の右側に確保する余白。縦並びのときは左右の両側に確保する。</summary>
        private const float TextRightPadding = 4f;
        /// <summary>マウスオーバー時に影を濃くする倍率。</summary>
        private const float HoverShadowStrength = 1.4f;
        /// <summary>無効状態のときに影を薄くする倍率。</summary>
        private const float DisabledShadowStrength = 0.5f;
        /// <summary>マウスオーバー時と押下時に、枠線をアイコンの色へ寄せる割合。</summary>
        private const float HighlightBorderBlend = 0.6f;

        private static readonly Color DefaultFillColor = Color.FromArgb(253, 252, 250);
        private static readonly Color DefaultBorderColor = Color.FromArgb(200, 197, 199);
        private static readonly Color DefaultShadowColor = Color.FromArgb(72, 56, 48);
        private static readonly Color DefaultCardIconColor = Color.FromArgb(184, 142, 94);

        private Color _fillColor = DefaultFillColor;
        private Color _borderColor = DefaultBorderColor;
        private Color _borderGradientColor = Color.Empty;
        private Color _shadowColor = DefaultShadowColor;
        private int _shadowSize = 8;
        private int _cornerRadius = 7;
        private int _iconSize = 28;
        private int _iconLeft = 16;
        private int _textGap = 20;
        private Color _hoverFillColor = Color.Empty;
        private Color _fillGradientColor = Color.Empty;
        private CardTileLayout _contentLayout = CardTileLayout.Horizontal;

        /// <summary>アイコンの既定色は落ち着いた金色。</summary>
        protected override Color DefaultIconColor
        {
            get { return DefaultCardIconColor; }
        }

        /// <summary>カードの塗り色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "253, 252, 250")]
        [Description("カードの塗り色。")]
        public Color FillColor
        {
            get { return _fillColor; }
            set { _fillColor = value; Invalidate(); }
        }

        /// <summary>
        /// カードの塗りを上下方向のグラデーションにする場合の、下端の色。空（<see cref="Color.Empty"/>）の場合は <see cref="FillColor"/> の単色。
        /// 指定すると、上端が FillColor、下端がこの色になる。上を白く、下を透明に近い色にすると、
        /// 下ほど後ろが透けて見えるガラスのようなカードになる。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("カードの塗りを上下方向のグラデーションにする場合の、下端の色。空の場合は FillColor の単色。指定すると上端が FillColor、下端がこの色になる。")]
        public Color FillGradientColor
        {
            get { return _fillGradientColor; }
            set { _fillGradientColor = value; Invalidate(); }
        }

        /// <summary>
        /// マウスオーバー時と押下時のカードの塗り色。空（<see cref="Color.Empty"/>）の場合は <see cref="FillColor"/> のまま変えない。
        /// 半透明色も指定できる。指定した場合、マウスオーバー時と押下時はグラデーションにせず、この色の単色で塗る。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("マウスオーバー時と押下時のカードの塗り色。空の場合は FillColor のまま変えない。")]
        public Color HoverFillColor
        {
            get { return _hoverFillColor; }
            set { _hoverFillColor = value; Invalidate(); }
        }

        /// <summary>アイコンと文字列の並べ方。既定は横並び（アイコンが左、文字列が右）。</summary>
        [Category("Appearance")]
        [DefaultValue(CardTileLayout.Horizontal)]
        [Description("アイコンと文字列の並べ方。Vertical にすると、アイコンの下に文字列を置いて左右中央に揃える。")]
        public CardTileLayout ContentLayout
        {
            get { return _contentLayout; }
            set
            {
                if (!Enum.IsDefined(typeof(CardTileLayout), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(CardTileLayout));
                }
                _contentLayout = value;
                Invalidate();
            }
        }

        /// <summary>カードの枠線の色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "200, 197, 199")]
        [Description("カードの枠線の色。")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        /// <summary>
        /// 枠線をグラデーションにする場合の、右下側の色。空（<see cref="Color.Empty"/>）の場合は <see cref="BorderColor"/> の単色。
        /// 指定すると、左上が BorderColor、右下がこの色になり、左上から光が当たっているような立体感が出る。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("枠線をグラデーションにする場合の、右下側の色。空の場合は BorderColor の単色。指定すると左上が BorderColor、右下がこの色になる。")]
        public Color BorderGradientColor
        {
            get { return _borderGradientColor; }
            set { _borderGradientColor = value; Invalidate(); }
        }

        /// <summary>影の色。濃さは自動で薄められるので、ここでは色味だけを指定する。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "72, 56, 48")]
        [Description("影の色。濃さは自動で薄められるので、ここでは色味だけを指定する。")]
        public Color ShadowColor
        {
            get { return _shadowColor; }
            set { _shadowColor = value; Invalidate(); }
        }

        /// <summary>
        /// カードの下に影が伸びる長さ。0 にすると影なしになる。
        /// カードの高さは、コントロールの高さからこの値と上側の余白を引いたものになる。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(8)]
        [Description("カードの下に影が伸びる長さ。0 にすると影なしになる。カードはその分だけコントロールより小さく描かれる。")]
        public int ShadowSize
        {
            get { return _shadowSize; }
            set { _shadowSize = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>カードの角の半径。0 にすると角丸なしになる。</summary>
        [Category("Appearance")]
        [DefaultValue(7)]
        [Description("カードの角の半径。0 にすると角丸なしになる。")]
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>アイコンの一辺。カードの高さに収まらない場合は自動で縮む。</summary>
        [Category("Appearance")]
        [DefaultValue(28)]
        [Description("アイコンの一辺。カードの高さに収まらない場合は自動で縮む。")]
        public int IconSize
        {
            get { return _iconSize; }
            set { _iconSize = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>カードの左端からアイコンまでの間隔。縦並びのときは使わない（アイコンは左右中央に置く）。</summary>
        [Category("Appearance")]
        [DefaultValue(16)]
        [Description("カードの左端からアイコンまでの間隔。縦並びのときは使わない。")]
        public int IconLeft
        {
            get { return _iconLeft; }
            set { _iconLeft = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>アイコンと文字列の間隔。横並びのときは左右、縦並びのときは上下の間隔になる。</summary>
        [Category("Appearance")]
        [DefaultValue(20)]
        [Description("アイコンと文字列の間隔。横並びのときは左右、縦並びのときは上下の間隔になる。")]
        public int TextGap
        {
            get { return _textGap; }
            set { _textGap = Math.Max(0, value); Invalidate(); }
        }

        protected override void PaintButton(Graphics g)
        {
            float scale = DpiScale;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 影はコントロールの領域内に描くので、カードはその分だけ内側になる
            var shadow = new DropShadow(_shadowSize, scale);
            RectangleF card = shadow.GetSurfaceBounds(Width, Height);
            if (card.Width <= 0f || card.Height <= 0f)
            {
                return;
            }

            // 押下中はカードを影の側へ少し沈める。影の位置は動かさないので、その分だけ影が短く見える
            int sink = 0;
            if (Enabled && IsPressed)
            {
                sink = Math.Min(shadow.Offset, Math.Max(1, (int)Math.Round(scale)));
            }

            float radius = _cornerRadius * scale;
            Color fill = _fillColor;
            Color fillEnd = _fillGradientColor;
            Color border = _borderColor;
            Color borderEnd = _borderGradientColor;
            float shadowStrength = 1f;
            if (!Enabled)
            {
                shadowStrength = DisabledShadowStrength;
            }
            else if (IsPressed || IsHovered)
            {
                // マウスオーバー時と押下時は、枠線をアイコンの色に寄せて目立たせる
                border = PaintHelper.Blend(_borderColor, CurrentIconColor, HighlightBorderBlend);
                if (!borderEnd.IsEmpty)
                {
                    borderEnd = PaintHelper.Blend(_borderGradientColor, CurrentIconColor, HighlightBorderBlend);
                }

                // マウスオーバー時の塗り色を指定している場合は、その単色に切り替える
                if (!_hoverFillColor.IsEmpty)
                {
                    fill = _hoverFillColor;
                    fillEnd = Color.Empty;
                }

                if (IsPressed)
                {
                    fill = PaintHelper.Blend(fill, CurrentIconColor, 0.08f);
                    if (!fillEnd.IsEmpty)
                    {
                        fillEnd = PaintHelper.Blend(fillEnd, CurrentIconColor, 0.08f);
                    }
                }
                else
                {
                    shadowStrength = HoverShadowStrength;
                }
            }

            // 影は沈める前のカードの位置を基準にする
            RectangleF shadowSurface = card;
            card.Offset(0f, sink);
            using (GraphicsPath frame = PaintHelper.CreateRoundedRectangle(card, radius))
            using (Brush fillBrush = CreateFillBrush(card, fill, fillEnd))
            using (Pen borderPen = PaintHelper.CreateBorderPen(card, border, borderEnd, Math.Max(1f, (float)Math.Round(scale))))
            {
                // 影はカードの外側にだけ描く。カードが半透明でも、影がカード越しに透けて見えないようにする
                GraphicsState beforeShadow = g.Save();
                g.SetClip(frame, CombineMode.Exclude);
                shadow.Draw(g, shadowSurface, radius, _shadowColor, shadowStrength);
                g.Restore(beforeShadow);

                g.FillPath(fillBrush, frame);
                g.DrawPath(borderPen, frame);
            }

            // キーボード操作時のフォーカス表示
            if (Focused && ShowFocusCues)
            {
                var focusBounds = RectangleF.Inflate(card, -1f, -1f);
                using (GraphicsPath focus = PaintHelper.CreateRoundedRectangle(focusBounds, Math.Max(0f, radius - 1f)))
                using (var focusPen = new Pen(SystemColors.Highlight, Math.Max(1f, (float)Math.Round(scale))))
                {
                    g.DrawPath(focusPen, focus);
                }
            }

            if (_contentLayout == CardTileLayout.Vertical)
            {
                DrawVerticalContent(g, card, scale);
            }
            else
            {
                DrawHorizontalContent(g, card, scale);
            }
        }

        /// <summary>
        /// カードを塗るブラシを作る。呼び出し側で Dispose すること。
        /// <paramref name="bottom"/> が空（<see cref="Color.Empty"/>）なら <paramref name="top"/> の単色。
        /// 指定した場合は、カードの上端が <paramref name="top"/>、下端が <paramref name="bottom"/> になるグラデーション。
        /// </summary>
        private static Brush CreateFillBrush(RectangleF card, Color top, Color bottom)
        {
            if (bottom.IsEmpty || bottom.ToArgb() == top.ToArgb() || card.Height <= 0f)
            {
                return new SolidBrush(top);
            }

            var brush = new LinearGradientBrush(
                new PointF(0f, card.Top),
                new PointF(0f, card.Bottom),
                top,
                bottom);

            // 上端・下端の 1 画素に反対側の色が回り込まないようにする
            brush.WrapMode = WrapMode.TileFlipXY;
            return brush;
        }

        /// <summary>
        /// アイコンを左、文字列をその右に描く。どちらもカードの上下中央に置く。
        /// </summary>
        private void DrawHorizontalContent(Graphics g, RectangleF card, float scale)
        {
            // アイコン
            float iconSide = Math.Min(_iconSize * scale, card.Height - 4f * scale);
            var iconBounds = new RectangleF(
                card.X + _iconLeft * scale,
                card.Y + (card.Height - iconSide) / 2f,
                Math.Max(0f, iconSide),
                Math.Max(0f, iconSide));
            MenuIconPainter.Draw(g, IconKind, iconBounds, CurrentIconColor, IconLineWeight);

            // 文字列
            int textLeft = (int)Math.Round(iconBounds.Right + _textGap * scale);
            int textRight = (int)Math.Round(card.Right - TextRightPadding * scale);
            int textTop = (int)Math.Round(card.Y);
            int textHeight = (int)Math.Round(card.Height);
            DrawButtonText(g, new Rectangle(textLeft, textTop, textRight - textLeft, textHeight), CurrentTextColor);
        }

        /// <summary>
        /// アイコンを上、文字列をその下に描く。アイコンと文字列をひとまとまりとして、カードの上下左右の中央に置く。
        /// </summary>
        private void DrawVerticalContent(Graphics g, RectangleF card, float scale)
        {
            int textLeft = (int)Math.Round(card.X + TextRightPadding * scale);
            int textRight = (int)Math.Round(card.Right - TextRightPadding * scale);
            int textWidth = Math.Max(0, textRight - textLeft);
            int textHeight = MeasureButtonTextHeight(g, textWidth);

            // アイコンなし、または文字列なしの場合は、間隔を空けずに残った方だけを中央に置く
            float iconSide = IconKind == MenuIconKind.None ? 0f : Math.Max(0f, Math.Min(_iconSize * scale, card.Height - 4f * scale));
            float gap = iconSide > 0f && textHeight > 0 ? _textGap * scale : 0f;
            float top = card.Y + (card.Height - (iconSide + gap + textHeight)) / 2f;

            var iconBounds = new RectangleF(card.X + (card.Width - iconSide) / 2f, top, iconSide, iconSide);
            MenuIconPainter.Draw(g, IconKind, iconBounds, CurrentIconColor, IconLineWeight);

            int textTop = (int)Math.Round(top + iconSide + gap);
            DrawButtonText(g, new Rectangle(textLeft, textTop, textWidth, textHeight), CurrentTextColor, HorizontalAlignment.Center);
        }
    }
}
