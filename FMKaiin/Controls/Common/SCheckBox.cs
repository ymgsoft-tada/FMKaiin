using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// チェック ボックス。チェックの四角（大きさ・角の丸み・色）と文字を、すべて自前で描く。
    /// 標準のチェック ボックスは、四角の大きさや色を変えられないので、見た目を調整できるようにしたもの。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 標準の <see cref="CheckBox"/> を継承していて、Checked・CheckState・CheckedChanged・AutoCheck・ThreeState・CheckAlign・
    /// Text・Enabled・TabIndex などは、そのまま使える（<see cref="App.SControlDBCheckBox"/> にも、そのまま渡せる）。
    /// 文字の中の &amp; は、次の文字を下線付きのアクセス キーにする。
    /// </para>
    /// <para>
    /// 四角の大きさ（<see cref="BoxSize"/>）の既定は 17（96 DPI 基準）。標準のチェック ボックス（13）より、上下左右に 2 ずつ大きい。
    /// 背景は、既定で透明（親コントロールと同じ見た目）。グリッドのセルに重ねるときなどは、<see cref="BackColor"/> に色を指定する。
    /// </para>
    /// <para>
    /// 文字は 1 行で描く（収まらない分は、末尾を「…」にする。折り返さない）。
    /// 四角を中央に置く指定（<see cref="CheckBox.CheckAlign"/> が MiddleCenter など）は、文字なしで使うことを想定している
    /// （文字は、コントロールの全体に、<see cref="CheckBox.TextAlign"/> の位置で描くので、四角と重なる）。
    /// </para>
    /// <para>
    /// 配置・文字・色の決め方は、ラジオ ボタン（<see cref="SRadioButton"/>）と共通（<see cref="SCheckStyle"/>）。
    /// </para>
    /// </remarks>
    public class SCheckBox : CheckBox
    {
        /// <summary>四角の角の半径の既定値。96 DPI 基準のピクセル。</summary>
        private const int DefaultCornerRadius = 3;

        // 四角の中のチェックの印の、3 つの頂点の位置（四角の大きさに対する割合）と、線の太さ（同じく割合）
        private static readonly PointF[] CheckMarkPoints = { new PointF(0.24f, 0.53f), new PointF(0.43f, 0.71f), new PointF(0.77f, 0.31f) };
        private const float CheckMarkWeight = 0.115f;

        private int _boxSize = SCheckStyle.DefaultMarkSize;
        private int _cornerRadius = DefaultCornerRadius;
        private Color _boxBorderColor = SCheckStyle.MarkBorderColor;
        private Color _boxBackColor = SCheckStyle.MarkBackColor;
        private Color _checkedColor = SCheckStyle.CheckedColor;
        private Color _checkMarkColor = SCheckStyle.CheckMarkColor;
        private Color _focusBorderColor = SControlStyle.FocusBorderColor;
        private bool _hover;
        private bool _pressed;

        public SCheckBox()
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

        /// <summary>チェックの四角の大きさ（1 辺）。96 DPI 基準のピクセルで指定する。既定は 17。</summary>
        [Category("Appearance")]
        [DefaultValue(SCheckStyle.DefaultMarkSize)]
        [Description("チェックの四角の大きさ（1 辺）。96 DPI 基準のピクセルで指定する。")]
        public int BoxSize
        {
            get { return _boxSize; }
            set
            {
                _boxSize = Math.Max(SCheckStyle.MinimumMarkSize, value);
                Invalidate();
                // 自動で大きさを決める設定（AutoSize）のときに、大きさを計算し直させる
                if (AutoSize && Parent != null)
                {
                    Parent.PerformLayout(this, "BoxSize");
                }
            }
        }

        /// <summary>チェックの四角の、角の半径。96 DPI 基準のピクセルで指定する。既定は 3。</summary>
        [Category("Appearance")]
        [DefaultValue(DefaultCornerRadius)]
        [Description("チェックの四角の、角の半径。96 DPI 基準のピクセルで指定する。")]
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>オフのときの、四角の枠線の色。</summary>
        [Category("Appearance")]
        [Description("オフのときの、四角の枠線の色。")]
        public Color BoxBorderColor
        {
            get { return _boxBorderColor; }
            set { _boxBorderColor = value.IsEmpty ? SCheckStyle.MarkBorderColor : value; Invalidate(); }
        }

        /// <summary>オフのときの、四角の中の色。既定は白。</summary>
        [Category("Appearance")]
        [Description("オフのときの、四角の中の色。")]
        public Color BoxBackColor
        {
            get { return _boxBackColor; }
            set { _boxBackColor = value.IsEmpty ? SCheckStyle.MarkBackColor : value; Invalidate(); }
        }

        /// <summary>オンのとき（と、どちらでもない状態のとき）の、四角の色。</summary>
        [Category("Appearance")]
        [Description("オンのときの、四角の色。")]
        public Color CheckedColor
        {
            get { return _checkedColor; }
            set { _checkedColor = value.IsEmpty ? SCheckStyle.CheckedColor : value; Invalidate(); }
        }

        /// <summary>チェックの印の色。既定は白。</summary>
        [Category("Appearance")]
        [Description("チェックの印の色。")]
        public Color CheckMarkColor
        {
            get { return _checkMarkColor; }
            set { _checkMarkColor = value.IsEmpty ? SCheckStyle.CheckMarkColor : value; Invalidate(); }
        }

        /// <summary>フォーカスがあるときの、四角の枠線の色。既定は、S 系の入力欄と同じ。</summary>
        [Category("Appearance")]
        [Description("フォーカスがあるときの、四角の枠線の色。")]
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
        private bool ShouldSerializeBoxBorderColor() { return _boxBorderColor != SCheckStyle.MarkBorderColor; }
        private void ResetBoxBorderColor() { BoxBorderColor = Color.Empty; }
        private bool ShouldSerializeBoxBackColor() { return _boxBackColor != SCheckStyle.MarkBackColor; }
        private void ResetBoxBackColor() { BoxBackColor = Color.Empty; }
        private bool ShouldSerializeCheckedColor() { return _checkedColor != SCheckStyle.CheckedColor; }
        private void ResetCheckedColor() { CheckedColor = Color.Empty; }
        private bool ShouldSerializeCheckMarkColor() { return _checkMarkColor != SCheckStyle.CheckMarkColor; }
        private void ResetCheckMarkColor() { CheckMarkColor = Color.Empty; }
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
        /// 内容がちょうど収まる大きさ（自動で大きさを決める設定のときに使われる）。四角・すき間・文字を横に並べた大きさ。
        /// </summary>
        public override Size GetPreferredSize(Size proposedSize)
        {
            return SCheckStyle.GetPreferredSize(this, _boxSize);
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

        protected override void OnCheckStateChanged(EventArgs e)
        {
            base.OnCheckStateChanged(e);
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
            Rectangle box = SCheckStyle.GetMarkBounds(content, CheckAlign, _boxSize, scale);

            DrawBox(g, box, scale);
            SCheckStyle.DrawText(g, this, content, box, CheckAlign, TextAlign, ShowKeyboardCues, scale);
        }

        /// <summary>四角と、その中の印（チェック、または「どちらでもない」の横線）を描く。</summary>
        private void DrawBox(Graphics g, Rectangle box, float scale)
        {
            CheckState state = CheckState;
            bool filled = state != CheckState.Unchecked;

            Color fill;
            Color border;
            int borderWidth;
            SCheckStyle.GetMarkColors(this, filled, _pressed, _hover, _checkedColor, _boxBackColor, _boxBorderColor, _focusBorderColor, scale,
                out fill, out border, out borderWidth);
            float offset = (borderWidth - 1) / 2f;

            var bounds = new RectangleF(box.X + offset, box.Y + offset, box.Width - 1 - offset * 2f, box.Height - 1 - offset * 2f);
            SmoothingMode previous = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            try
            {
                using (GraphicsPath shape = PaintHelper.CreateRoundedRectangle(bounds, _cornerRadius * scale))
                using (var brush = new SolidBrush(fill))
                using (var pen = new Pen(border, borderWidth))
                {
                    g.FillPath(brush, shape);
                    g.DrawPath(pen, shape);
                }

                if (filled)
                {
                    Color mark = Enabled ? _checkMarkColor : Color.White;
                    using (var pen = new Pen(mark, Math.Max(1.5f, box.Width * CheckMarkWeight)))
                    {
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                        pen.LineJoin = LineJoin.Round;

                        if (state == CheckState.Checked)
                        {
                            var points = new PointF[CheckMarkPoints.Length];
                            for (int i = 0; i < points.Length; i++)
                            {
                                points[i] = new PointF(box.X + box.Width * CheckMarkPoints[i].X, box.Y + box.Height * CheckMarkPoints[i].Y);
                            }
                            g.DrawLines(pen, points);
                        }
                        else
                        {
                            // どちらでもない状態（ThreeState を使うときだけ）: 横線
                            float y = box.Y + box.Height / 2f;
                            g.DrawLine(pen, box.X + box.Width * 0.27f, y, box.X + box.Width * 0.73f, y);
                        }
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
