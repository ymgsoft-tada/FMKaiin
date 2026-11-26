using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// ラジオ ボタン。丸（大きさ・色）と文字を、すべて自前で描く。
    /// 標準のラジオ ボタンは、丸の大きさや色を変えられないので、見た目を調整できるようにしたもの。
    /// チェック ボックス（<see cref="SCheckBox"/>）と、大きさ・色・配置をそろえてある。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 標準の <see cref="RadioButton"/> を継承していて、Checked・CheckedChanged・AutoCheck・CheckAlign・Text・Enabled・TabIndex などは、
    /// そのまま使える。同じ親コントロールに置いたものが 1 つの組になり、1 つを選ぶと、ほかの選択が外れる（標準の動き）。
    /// 文字の中の &amp; は、次の文字を下線付きのアクセス キーにする。
    /// </para>
    /// <para>
    /// 丸の大きさ（<see cref="CircleSize"/>）の既定は 17（96 DPI 基準）。標準のラジオ ボタン（13）より、上下左右に 2 ずつ大きい。
    /// 背景は、既定で透明（親コントロールと同じ見た目）。
    /// </para>
    /// <para>
    /// 文字は 1 行で描く（収まらない分は、末尾を「…」にする。折り返さない）。
    /// 丸を中央に置く指定（<see cref="RadioButton.CheckAlign"/> が MiddleCenter など）は、文字なしで使うことを想定している。
    /// </para>
    /// <para>
    /// 配置・文字・色の決め方は、チェック ボックスと共通（<see cref="SCheckStyle"/>）。
    /// </para>
    /// </remarks>
    public class SRadioButton : RadioButton
    {
        /// <summary>選んだときに、丸の中に描く点の大きさ（丸の大きさに対する割合）。</summary>
        private const float DotRatio = 0.42f;

        private int _circleSize = SCheckStyle.DefaultMarkSize;
        private Color _circleBorderColor = SCheckStyle.MarkBorderColor;
        private Color _circleBackColor = SCheckStyle.MarkBackColor;
        private Color _checkedColor = SCheckStyle.CheckedColor;
        private Color _dotColor = SCheckStyle.CheckMarkColor;
        private Color _focusBorderColor = SControlStyle.FocusBorderColor;
        private bool _hover;
        private bool _pressed;

        public SRadioButton()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor,
                true);
            base.Font = SControlStyle.Font;
            base.ForeColor = SControlStyle.TextColor;
            base.BackColor = Color.Transparent;
        }

        #region プロパティ

        /// <summary>丸の大きさ（直径）。96 DPI 基準のピクセルで指定する。既定は 17。</summary>
        [Category("Appearance")]
        [DefaultValue(SCheckStyle.DefaultMarkSize)]
        [Description("丸の大きさ（直径）。96 DPI 基準のピクセルで指定する。")]
        public int CircleSize
        {
            get { return _circleSize; }
            set
            {
                _circleSize = Math.Max(SCheckStyle.MinimumMarkSize, value);
                Invalidate();
                // 自動で大きさを決める設定（AutoSize）のときに、大きさを計算し直させる
                if (AutoSize && Parent != null)
                {
                    Parent.PerformLayout(this, "CircleSize");
                }
            }
        }

        /// <summary>選んでいないときの、丸の枠線の色。</summary>
        [Category("Appearance")]
        [Description("選んでいないときの、丸の枠線の色。")]
        public Color CircleBorderColor
        {
            get { return _circleBorderColor; }
            set { _circleBorderColor = value.IsEmpty ? SCheckStyle.MarkBorderColor : value; Invalidate(); }
        }

        /// <summary>選んでいないときの、丸の中の色。既定は白。</summary>
        [Category("Appearance")]
        [Description("選んでいないときの、丸の中の色。")]
        public Color CircleBackColor
        {
            get { return _circleBackColor; }
            set { _circleBackColor = value.IsEmpty ? SCheckStyle.MarkBackColor : value; Invalidate(); }
        }

        /// <summary>選んだときの、丸の色。</summary>
        [Category("Appearance")]
        [Description("選んだときの、丸の色。")]
        public Color CheckedColor
        {
            get { return _checkedColor; }
            set { _checkedColor = value.IsEmpty ? SCheckStyle.CheckedColor : value; Invalidate(); }
        }

        /// <summary>選んだときに、丸の中に描く点の色。既定は白。</summary>
        [Category("Appearance")]
        [Description("選んだときに、丸の中に描く点の色。")]
        public Color DotColor
        {
            get { return _dotColor; }
            set { _dotColor = value.IsEmpty ? SCheckStyle.CheckMarkColor : value; Invalidate(); }
        }

        /// <summary>フォーカスがあるときの、丸の枠線の色。既定は、S 系の入力欄と同じ。</summary>
        [Category("Appearance")]
        [Description("フォーカスがあるときの、丸の枠線の色。")]
        public Color FocusBorderColor
        {
            get { return _focusBorderColor; }
            set { _focusBorderColor = value.IsEmpty ? SControlStyle.FocusBorderColor : value; Invalidate(); }
        }

        /// <summary>フォント。既定は、S 系の入力欄と同じ。</summary>
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>文字の色。既定は、S 系の入力欄と同じ。</summary>
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value.IsEmpty ? SControlStyle.TextColor : value; }
        }

        /// <summary>背景の色。既定は透明（親コントロールと同じ見た目にする）。</summary>
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value.IsEmpty ? Color.Transparent : value; }
        }

        // デザイナーが「既定値と違うときだけ」Designer.cs に書き出すようにする
        private bool ShouldSerializeCircleBorderColor() { return _circleBorderColor != SCheckStyle.MarkBorderColor; }
        private void ResetCircleBorderColor() { CircleBorderColor = Color.Empty; }
        private bool ShouldSerializeCircleBackColor() { return _circleBackColor != SCheckStyle.MarkBackColor; }
        private void ResetCircleBackColor() { CircleBackColor = Color.Empty; }
        private bool ShouldSerializeCheckedColor() { return _checkedColor != SCheckStyle.CheckedColor; }
        private void ResetCheckedColor() { CheckedColor = Color.Empty; }
        private bool ShouldSerializeDotColor() { return _dotColor != SCheckStyle.CheckMarkColor; }
        private void ResetDotColor() { DotColor = Color.Empty; }
        private bool ShouldSerializeFocusBorderColor() { return _focusBorderColor != SControlStyle.FocusBorderColor; }
        private void ResetFocusBorderColor() { FocusBorderColor = Color.Empty; }

        private bool ShouldSerializeFont() { return !base.Font.Equals(SControlStyle.Font); }
        public override void ResetFont() { base.Font = SControlStyle.Font; }
        private bool ShouldSerializeForeColor() { return ForeColor != SControlStyle.TextColor; }
        public override void ResetForeColor() { base.ForeColor = SControlStyle.TextColor; }
        private bool ShouldSerializeBackColor() { return BackColor != Color.Transparent; }
        public override void ResetBackColor() { base.BackColor = Color.Transparent; }

        protected override Size DefaultSize
        {
            get { return new Size(104, 24); }
        }

        /// <summary>
        /// 内容がちょうど収まる大きさ（自動で大きさを決める設定のときに使われる）。丸・すき間・文字を横に並べた大きさ。
        /// </summary>
        public override Size GetPreferredSize(Size proposedSize)
        {
            return SCheckStyle.GetPreferredSize(this, _circleSize);
        }

        #endregion

        #region 状態の変化

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);
            _hover = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            base.OnMouseLeave(eventargs);
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
            _pressed = false;
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            Invalidate();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        #endregion

        #region 描画

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            SCheckStyle.PaintBackground(this, g, InvokePaintBackground, InvokePaint);

            float scale = LayoutScale.Of(this);
            Rectangle content = SCheckStyle.GetContentBounds(this);
            Rectangle circle = SCheckStyle.GetMarkBounds(content, CheckAlign, _circleSize, scale);

            DrawCircle(g, circle, scale);
            SCheckStyle.DrawText(g, this, content, circle, CheckAlign, TextAlign, ShowKeyboardCues, scale);
        }

        /// <summary>丸と、選んだときの中の点を描く。</summary>
        private void DrawCircle(Graphics g, Rectangle circle, float scale)
        {
            bool selected = Checked;

            Color fill;
            Color border;
            int borderWidth;
            SCheckStyle.GetMarkColors(this, selected, _pressed, _hover, _checkedColor, _circleBackColor, _circleBorderColor, _focusBorderColor, scale,
                out fill, out border, out borderWidth);
            float offset = (borderWidth - 1) / 2f;

            var bounds = new RectangleF(circle.X + offset, circle.Y + offset, circle.Width - 1 - offset * 2f, circle.Height - 1 - offset * 2f);
            SmoothingMode previous = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            try
            {
                using (var brush = new SolidBrush(fill))
                using (var pen = new Pen(border, borderWidth))
                {
                    g.FillEllipse(brush, bounds);
                    g.DrawEllipse(pen, bounds);
                }

                if (selected)
                {
                    float dot = circle.Width * DotRatio;
                    float left = circle.X + (circle.Width - 1 - dot) / 2f;
                    float top = circle.Y + (circle.Height - 1 - dot) / 2f;
                    using (var brush = new SolidBrush(Enabled ? _dotColor : Color.White))
                    {
                        g.FillEllipse(brush, left, top, dot, dot);
                    }
                }
            }
            finally
            {
                g.SmoothingMode = previous;
            }
        }

        #endregion
    }
}
