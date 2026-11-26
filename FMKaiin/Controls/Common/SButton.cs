using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 角丸のボタン。画面の中に置く、ファンクション キー以外の操作（検索クリアなど）に使う。
    /// 色は、ファンクション キーのボタンに合わせてある。
    /// </summary>
    /// <remarks>
    /// 標準の <see cref="Button"/> を継承していて、Click・Text・Enabled・TabIndex などは、そのまま使える。
    /// 文字の中の &amp; は、次の文字を下線付きのアクセス キーにする（「検索クリア(&amp;C)」）。
    /// </remarks>
    public class SButton : Button
    {
        // ふつうのボタン
        private static readonly Color NormalTop = Color.White;
        private static readonly Color NormalBottom = Color.FromArgb(240, 245, 251);
        private static readonly Color NormalBorder = Color.FromArgb(168, 184, 206);
        private static readonly Color NormalText = Color.FromArgb(20, 38, 66);

        // 主な操作のボタン（濃い青）
        private static readonly Color PrimaryTop = Color.FromArgb(44, 118, 198);
        private static readonly Color PrimaryBottom = Color.FromArgb(24, 88, 166);
        private static readonly Color PrimaryBorder = Color.FromArgb(20, 74, 144);

        // 無効のボタン
        private static readonly Color DisabledTop = Color.FromArgb(247, 248, 250);
        private static readonly Color DisabledBottom = Color.FromArgb(243, 245, 248);
        private static readonly Color DisabledBorder = Color.FromArgb(208, 213, 220);
        private static readonly Color DisabledText = Color.FromArgb(160, 166, 175);

        /// <summary>マウスを載せたとき・押したときに、色へ混ぜる黒の割合。</summary>
        private const float HoverShade = 0.05f;
        private const float PressedShade = 0.14f;

        private bool _primary;
        private bool _highlightWhenFocused;
        private int _cornerRadius = 6;
        private bool _hover;
        private bool _pressed;

        public SButton()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor,
                true);
            base.Font = SControlStyle.Font;
        }

        /// <summary>主な操作のボタンとして、濃い青で表示するか。既定は false。</summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("主な操作のボタンとして、濃い青で表示するか。")]
        public bool Primary
        {
            get { return _primary; }
            set { _primary = value; Invalidate(); }
        }

        /// <summary>
        /// フォーカスがある間、濃い青で表示するか。既定は false（フォーカスは、枠線の色で示す）。
        /// ボタンだけが並ぶ場所（メッセージ ボックスなど）で、Enter を押したときに働くボタンを分かりやすくするためのもの。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("フォーカスがある間、濃い青で表示するか。")]
        public bool HighlightWhenFocused
        {
            get { return _highlightWhenFocused; }
            set { _highlightWhenFocused = value; Invalidate(); }
        }

        /// <summary>角の半径。96 DPI 基準のピクセルで指定する。</summary>
        [Category("Appearance")]
        [DefaultValue(6)]
        [Description("角の半径。96 DPI 基準のピクセルで指定する。")]
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>フォント。既定は、S 系の入力欄と同じ。</summary>
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        private bool ShouldSerializeFont()
        {
            return !base.Font.Equals(SControlStyle.Font);
        }

        public override void ResetFont()
        {
            base.Font = SControlStyle.Font;
        }

        protected override Size DefaultSize
        {
            get { return new Size(96, 28); }
        }

        #region 状態の変化

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _hover = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hover = false;
            _pressed = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                _pressed = true;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            _pressed = false;
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        #endregion

        #region 描画

        /// <summary>
        /// 角丸の外側を描く。既定では、親コントロールと同じ見た目にする（半透明のパネルの上でも、角が浮かないように）。
        /// </summary>
        protected virtual void PaintOutside(Graphics g)
        {
            PaintHelper.PaintParentAppearance(this, g, InvokePaintBackground, InvokePaint);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            PaintOutside(g);

            bool enabled = Enabled;
            bool focused = Focused && enabled;
            bool primary = enabled && (_primary || (_highlightWhenFocused && focused));
            float shade = !enabled ? 0f : _pressed ? PressedShade : _hover ? HoverShade : 0f;
            float scale = LayoutScale.Of(this);

            Color top = !enabled ? DisabledTop : PaintHelper.Blend(primary ? PrimaryTop : NormalTop, Color.Black, shade);
            Color bottom = !enabled ? DisabledBottom : PaintHelper.Blend(primary ? PrimaryBottom : NormalBottom, Color.Black, shade);
            Color text = !enabled ? DisabledText : primary ? Color.White : NormalText;

            // 枠線。フォーカスは、濃い青で表示しないボタンでは、入力欄と同じ色・太さの枠線で示す
            bool focusBorder = focused && !primary;
            Color border = !enabled ? DisabledBorder : primary ? PrimaryBorder : focusBorder ? SControlStyle.FocusBorderColor : NormalBorder;
            int borderWidth = SControlStyle.GetBorderWidth(scale, focusBorder);
            float offset = (borderWidth - 1) / 2f;

            var bounds = new RectangleF(offset, offset, Width - 1 - offset * 2f, Height - 1 - offset * 2f);
            SmoothingMode previous = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            try
            {
                using (GraphicsPath shape = PaintHelper.CreateRoundedRectangle(bounds, _cornerRadius * scale))
                using (var fill = new LinearGradientBrush(new RectangleF(0f, 0f, Width, Height), top, bottom, LinearGradientMode.Vertical))
                using (var borderPen = new Pen(border, borderWidth))
                {
                    g.FillPath(fill, shape);
                    g.DrawPath(borderPen, shape);
                }
            }
            finally
            {
                g.SmoothingMode = previous;
            }

            // & は、下線にする。下線は、Alt を押すなどしてキーボードの操作を始めるまでは表示しない（Windows の設定に従う）
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
            if (!ShowKeyboardCues)
            {
                flags |= TextFormatFlags.HidePrefix;
            }
            TextRenderer.DrawText(g, Text, Font, ClientRectangle, text, flags);
        }

        #endregion
    }
}
