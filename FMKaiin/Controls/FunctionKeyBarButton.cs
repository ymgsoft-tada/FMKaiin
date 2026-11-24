using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// <see cref="FunctionKeyBar"/> の中で、ボタンを左右どちらへ寄せるか。
    /// </summary>
    public enum FunctionKeyAlignment
    {
        /// <summary>左から順に並べる。</summary>
        Left = 0,
        /// <summary>右端へ寄せる。</summary>
        Right
    }

    /// <summary>
    /// <see cref="FunctionKeyBar"/> に並べるボタン 1 つ分。
    /// 角丸のボタンの中に、キー名を入れた小さな枠（バッジ。「F5」など）と名称を横並びで表示する。
    /// <see cref="Emphasized"/> を true にすると、青く塗った目立つ見た目になる。
    /// 画面に直接置くものではなく、<see cref="FunctionKeyBar.AddButton"/> が作る。大きさと位置も FunctionKeyBar が決める。
    /// クリックしてもフォーカスは受け取らない（入力中のコントロールからフォーカスを奪わないため）。
    /// </summary>
    [ToolboxItem(false)]
    public class FunctionKeyBarButton : IconButtonBase
    {
        /// <summary>ボタンの下に影が伸びる長さ。96 DPI 基準のピクセル。</summary>
        internal const int ShadowSize = 4;

        /// <summary>ボタンの角の半径。</summary>
        private const float CornerRadius = 8f;
        /// <summary>バッジの角の半径。</summary>
        private const float BadgeCornerRadius = 4f;
        /// <summary>バッジの中の、キー名の左右の余白。</summary>
        private const float BadgePaddingX = 5f;
        /// <summary>バッジの中の、キー名の上下の余白。</summary>
        private const float BadgePaddingY = 2f;
        /// <summary>マウスオーバー時と押下時に、枠線を強調色へ寄せる割合。</summary>
        private const float HighlightBorderBlend = 0.6f;
        /// <summary>マウスオーバー時に影を濃くする倍率。</summary>
        private const float HoverShadowStrength = 1.5f;
        /// <summary>無効状態のときに影を薄くする倍率。</summary>
        private const float DisabledShadowStrength = 0.4f;

        private const TextFormatFlags MeasureFlags =
            TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix;

        /// <summary>ボタンの状態ごとの配色。</summary>
        private struct Palette
        {
            public Color FillTop;
            public Color FillBottom;
            public Color Border;
            public Color Text;
            public Color BadgeFill;
            public Color BadgeBorder;
            public Color BadgeText;
        }

        private static readonly Color AccentColor = Color.FromArgb(30, 96, 170);
        private static readonly Color ShadowColor = Color.FromArgb(60, 84, 120);

        private static readonly Palette NormalPalette = new Palette
        {
            FillTop = Color.White,
            FillBottom = Color.FromArgb(240, 245, 251),
            Border = Color.FromArgb(168, 184, 206),
            Text = Color.FromArgb(20, 38, 66),
            BadgeFill = Color.FromArgb(238, 245, 253),
            BadgeBorder = Color.FromArgb(120, 160, 210),
            BadgeText = AccentColor
        };

        private static readonly Palette EmphasizedPalette = new Palette
        {
            FillTop = Color.FromArgb(44, 118, 198),
            FillBottom = Color.FromArgb(24, 88, 166),
            Border = Color.FromArgb(20, 74, 144),
            Text = Color.White,
            BadgeFill = Color.FromArgb(46, 255, 255, 255),
            BadgeBorder = Color.FromArgb(215, 255, 255, 255),
            BadgeText = Color.White
        };

        private static readonly Palette DisabledPalette = new Palette
        {
            FillTop = Color.FromArgb(247, 248, 250),
            FillBottom = Color.FromArgb(243, 245, 248),
            Border = Color.FromArgb(208, 213, 220),
            Text = Color.FromArgb(160, 166, 175),
            BadgeFill = Color.Empty,
            BadgeBorder = Color.FromArgb(200, 205, 213),
            BadgeText = Color.FromArgb(160, 166, 175)
        };

        private Keys _key = Keys.None;
        private FunctionKeyAlignment _alignment = FunctionKeyAlignment.Left;
        private bool _emphasized;
        private bool _visibleRequested = true;
        private Font _badgeFont;
        private float _innerPadding = 14f;
        private float _badgeGap = 8f;

        public FunctionKeyBarButton()
        {
            // クリックしてもフォーカスを受け取らず、Tab キーでも止まらないようにする
            SetStyle(ControlStyles.Selectable, false);
            TabStop = false;
        }

        /// <summary>このボタンに対応するキー。バッジにはこのキーの名前（「F5」など）を表示する。</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Keys Key
        {
            get { return _key; }
            internal set { _key = value; Invalidate(); }
        }

        /// <summary>帯の左右どちらへ寄せるか。</summary>
        [Category("Appearance")]
        [DefaultValue(FunctionKeyAlignment.Left)]
        [Description("帯の左右どちらへ寄せるか。")]
        public FunctionKeyAlignment Alignment
        {
            get { return _alignment; }
            set
            {
                if (!Enum.IsDefined(typeof(FunctionKeyAlignment), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(FunctionKeyAlignment));
                }
                if (_alignment == value)
                {
                    return;
                }
                _alignment = value;
                RequestParentLayout();
            }
        }

        /// <summary>true にすると、青く塗った目立つ見た目になる。登録や実行など、その画面の主な操作に使う。</summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("true にすると、青く塗った目立つ見た目になる。")]
        public bool Emphasized
        {
            get { return _emphasized; }
            set
            {
                if (_emphasized == value)
                {
                    return;
                }
                _emphasized = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 表示するように指定されているか。<see cref="Control.Visible"/> は親が非表示の間も false を返すので、
        /// 並べるときの「このボタンの場所を取るか」の判定には、指定された値そのものであるこちらを使う。
        /// </summary>
        internal bool VisibleRequested
        {
            get { return _visibleRequested; }
        }

        /// <summary>バッジの文字に使うフォント。<see cref="FunctionKeyBar"/> が設定し、破棄も FunctionKeyBar が行う。</summary>
        internal Font BadgeFont
        {
            get { return _badgeFont ?? Font; }
            set { _badgeFont = value; Invalidate(); }
        }

        /// <summary>ボタンの左右の端から中身までの余白。96 DPI 基準のピクセル。<see cref="FunctionKeyBar"/> が設定する。</summary>
        internal float InnerPadding
        {
            get { return _innerPadding; }
            set { _innerPadding = Math.Max(0f, value); Invalidate(); }
        }

        /// <summary>バッジと名称の間隔。96 DPI 基準のピクセル。<see cref="FunctionKeyBar"/> が設定する。</summary>
        internal float BadgeGap
        {
            get { return _badgeGap; }
            set { _badgeGap = Math.Max(0f, value); Invalidate(); }
        }

        /// <summary>バッジに表示する文字列。</summary>
        private string KeyText
        {
            get { return _key == Keys.None ? string.Empty : _key.ToString(); }
        }

        /// <summary>
        /// 現在のフォントと余白で、中身を省略せずに表示するのに必要なコントロールの幅を返す。
        /// </summary>
        /// <param name="scale">96 DPI を 1 とした表示倍率。</param>
        internal int MeasureWidth(float scale)
        {
            var shadow = new DropShadow(ShadowSize, scale);
            float content =
                _innerPadding * scale * 2f +
                MeasureBadgeSize(scale).Width +
                _badgeGap * scale +
                TextRenderer.MeasureText(Text, Font, Size.Empty, MeasureFlags).Width;
            return (int)Math.Ceiling(content) + shadow.SideInset * 2 + 1;
        }

        /// <summary>
        /// ボタン本体の高さを <paramref name="surfaceHeight"/> にするために必要な、コントロールの高さを返す
        /// （影はコントロールの領域内に描くので、その分だけ大きくなる）。
        /// </summary>
        internal static int GetControlHeight(int surfaceHeight, float scale)
        {
            var shadow = new DropShadow(ShadowSize, scale);
            return surfaceHeight + shadow.SideInset + shadow.Size + 1;
        }

        protected override void SetVisibleCore(bool value)
        {
            // 親が非表示の間に指定された値も覚えておけるよう、ここで受け取る
            _visibleRequested = value;
            base.SetVisibleCore(value);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            RequestParentLayout();
        }

        protected override void PaintButton(Graphics g)
        {
            float scale = DpiScale;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 影はコントロールの領域内に描くので、ボタン本体はその分だけ内側になる
            var shadow = new DropShadow(ShadowSize, scale);
            RectangleF surface = shadow.GetSurfaceBounds(Width, Height);
            if (surface.Width <= 0f || surface.Height <= 0f)
            {
                return;
            }

            bool highlighted = Enabled && (IsHovered || IsPressed);
            Palette palette = GetPalette(highlighted);
            float shadowStrength = !Enabled ? DisabledShadowStrength : (IsHovered && !IsPressed ? HoverShadowStrength : 1f);

            // 押下中は本体を影の側へ少し沈める。影の位置は動かさないので、その分だけ影が短く見える
            RectangleF shadowSurface = surface;
            if (Enabled && IsPressed)
            {
                surface.Offset(0f, Math.Min(shadow.Offset, Math.Max(1, (int)Math.Round(scale))));
            }

            float radius = CornerRadius * scale;
            float lineWidth = Math.Max(1f, (float)Math.Round(scale));
            using (GraphicsPath frame = PaintHelper.CreateRoundedRectangle(surface, radius))
            using (var fillBrush = new LinearGradientBrush(
                new PointF(0f, surface.Top), new PointF(0f, surface.Bottom), palette.FillTop, palette.FillBottom))
            using (var borderPen = new Pen(palette.Border, lineWidth))
            {
                // 上端・下端の 1 画素に反対側の色が回り込まないようにする
                fillBrush.WrapMode = WrapMode.TileFlipXY;

                // 影は本体の外側にだけ描く
                GraphicsState beforeShadow = g.Save();
                g.SetClip(frame, CombineMode.Exclude);
                shadow.Draw(g, shadowSurface, radius, ShadowColor, shadowStrength);
                g.Restore(beforeShadow);

                g.FillPath(fillBrush, frame);
                g.DrawPath(borderPen, frame);
            }

            // バッジ（キー名）
            SizeF badgeSize = MeasureBadgeSize(scale);
            var badge = new RectangleF(
                surface.X + _innerPadding * scale,
                surface.Y + (surface.Height - badgeSize.Height) / 2f,
                badgeSize.Width,
                badgeSize.Height);
            using (GraphicsPath badgePath = PaintHelper.CreateRoundedRectangle(badge, BadgeCornerRadius * scale))
            using (var badgePen = new Pen(palette.BadgeBorder, lineWidth))
            {
                if (!palette.BadgeFill.IsEmpty)
                {
                    using (var badgeBrush = new SolidBrush(palette.BadgeFill))
                    {
                        g.FillPath(badgeBrush, badgePath);
                    }
                }
                g.DrawPath(badgePen, badgePath);
            }
            TextRenderer.DrawText(
                g,
                KeyText,
                BadgeFont,
                Rectangle.Round(badge),
                palette.BadgeText,
                MeasureFlags | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            // 名称。幅が足りない場合は末尾を「…」で省略する
            int textLeft = (int)Math.Round(badge.Right + _badgeGap * scale);
            int textRight = (int)Math.Round(surface.Right - _innerPadding * scale);
            if (textRight > textLeft)
            {
                var textBounds = new Rectangle(textLeft, (int)Math.Round(surface.Y), textRight - textLeft, (int)Math.Round(surface.Height));
                TextRenderer.DrawText(
                    g,
                    Text,
                    Font,
                    textBounds,
                    palette.Text,
                    MeasureFlags | TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }
        }

        /// <summary>
        /// 現在の状態（無効・強調・マウスオーバー）に合った配色を返す。
        /// </summary>
        private Palette GetPalette(bool highlighted)
        {
            if (!Enabled)
            {
                return DisabledPalette;
            }

            Palette palette = _emphasized ? EmphasizedPalette : NormalPalette;
            if (highlighted)
            {
                if (_emphasized)
                {
                    // 青いボタンは、全体を少し明るくして反応を示す
                    palette.FillTop = PaintHelper.Blend(palette.FillTop, Color.White, 0.12f);
                    palette.FillBottom = PaintHelper.Blend(palette.FillBottom, Color.White, 0.12f);
                }
                else
                {
                    // 白いボタンは、枠線と下側の色を強調色へ寄せて反応を示す
                    palette.Border = PaintHelper.Blend(palette.Border, AccentColor, HighlightBorderBlend);
                    palette.FillBottom = PaintHelper.Blend(palette.FillBottom, AccentColor, 0.1f);
                }
            }
            return palette;
        }

        private SizeF MeasureBadgeSize(float scale)
        {
            Size text = TextRenderer.MeasureText(KeyText, BadgeFont, Size.Empty, MeasureFlags);
            return new SizeF(text.Width + BadgePaddingX * scale * 2f, text.Height + BadgePaddingY * scale * 2f);
        }

        /// <summary>
        /// 並べ方に関わる値が変わったので、親（<see cref="FunctionKeyBar"/>）に並べ直してもらう。
        /// </summary>
        private void RequestParentLayout()
        {
            if (Parent != null)
            {
                Parent.PerformLayout();
            }
            Invalidate();
        }
    }
}
