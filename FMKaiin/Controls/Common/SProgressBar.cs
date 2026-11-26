using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 進み具合を表示するバー。標準の ProgressBar の代わり。両端が丸い帯（下地）の上に、進んだ分を色で塗る。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 値は <see cref="Minimum"/>〜<see cref="Maximum"/> の範囲の <see cref="Value"/>（既定は 0〜100）。範囲の外の値は、範囲の端に丸める。
    /// 値を設定すると、すぐにその位置まで塗る（標準の ProgressBar のような、ゆっくり伸びる動きは無い）。
    /// </para>
    /// <para>
    /// 背景は、既定で透明（親コントロールと同じ見た目）。フォーカスは受け取らない。
    /// </para>
    /// </remarks>
    public class SProgressBar : Control
    {
        // 既定の色。下地は、グリッドの罫線と同じ。進んだ分は、チェック ボックスのオンの色と同じ系統の青
        private static readonly Color DefaultTrackColor = Color.FromArgb(226, 232, 240);
        private static readonly Color DefaultBarColor = Color.FromArgb(34, 103, 182);

        private int _minimum;
        private int _maximum = 100;
        private int _value;
        private Color _trackColor = DefaultTrackColor;
        private Color _barColor = DefaultBarColor;

        public SProgressBar()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                // 親の BackColor が Transparent のときに、親の描画内容の上へバーだけを描けるようにする
                ControlStyles.SupportsTransparentBackColor,
                true);
            SetStyle(ControlStyles.Selectable, false);
            TabStop = false;
            base.BackColor = Color.Transparent;
        }

        #region プロパティ

        /// <summary>値の下限。既定は 0。上限より大きい値を設定すると、上限も同じ値にする。</summary>
        [Category("Behavior")]
        [DefaultValue(0)]
        [Description("値の下限。")]
        public int Minimum
        {
            get { return _minimum; }
            set
            {
                _minimum = value;
                if (_maximum < value) _maximum = value;
                if (_value < value) _value = value;
                Invalidate();
            }
        }

        /// <summary>値の上限。既定は 100。下限より小さい値を設定すると、下限も同じ値にする。</summary>
        [Category("Behavior")]
        [DefaultValue(100)]
        [Description("値の上限。")]
        public int Maximum
        {
            get { return _maximum; }
            set
            {
                _maximum = value;
                if (_minimum > value) _minimum = value;
                if (_value > value) _value = value;
                Invalidate();
            }
        }

        /// <summary>いまの値。範囲の外の値を設定すると、範囲の端に丸める。</summary>
        [Category("Behavior")]
        [DefaultValue(0)]
        [Description("いまの値。")]
        public int Value
        {
            get { return _value; }
            set
            {
                int clamped = Math.Max(_minimum, Math.Min(_maximum, value));
                if (_value == clamped)
                {
                    return;
                }
                _value = clamped;
                Invalidate();
            }
        }

        /// <summary>下地（まだ進んでいない部分）の色。</summary>
        [Category("Appearance")]
        [Description("下地（まだ進んでいない部分）の色。")]
        public Color TrackColor
        {
            get { return _trackColor; }
            set { _trackColor = value.IsEmpty ? DefaultTrackColor : value; Invalidate(); }
        }

        /// <summary>進んだ分の色。</summary>
        [Category("Appearance")]
        [Description("進んだ分の色。")]
        public Color BarColor
        {
            get { return _barColor; }
            set { _barColor = value.IsEmpty ? DefaultBarColor : value; Invalidate(); }
        }

        /// <summary>背景の色。既定は透明（親コントロールと同じ見た目にする）。</summary>
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value.IsEmpty ? Color.Transparent : value; }
        }

        // デザイナーが「既定値と違うときだけ」Designer.cs に書き出すようにする
        private bool ShouldSerializeTrackColor() { return _trackColor != DefaultTrackColor; }
        private void ResetTrackColor() { TrackColor = Color.Empty; }
        private bool ShouldSerializeBarColor() { return _barColor != DefaultBarColor; }
        private void ResetBarColor() { BarColor = Color.Empty; }
        private bool ShouldSerializeBackColor() { return BackColor != Color.Transparent; }
        public override void ResetBackColor() { base.BackColor = Color.Transparent; }

        protected override Size DefaultSize
        {
            get { return new Size(200, 8); }
        }

        /// <summary>
        /// 進んだ分の幅（ピクセル）。値が下限なら 0、上限ならコントロールの幅。
        /// </summary>
        internal int FilledWidth
        {
            get
            {
                int range = _maximum - _minimum;
                if (range <= 0 || Width <= 0)
                {
                    return 0;
                }
                return (int)Math.Round(Width * (double)(_value - _minimum) / range);
            }
        }

        #endregion

        #region 描画

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width <= 1 || Height <= 1)
            {
                return;
            }

            Graphics g = e.Graphics;
            SmoothingMode previousSmoothing = g.SmoothingMode;
            Region previousClip = g.Clip;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            try
            {
                // 両端を丸くした帯。角の半径は、高さの半分
                var bounds = new RectangleF(0f, 0f, Width - 1, Height - 1);
                float radius = bounds.Height / 2f;
                using (GraphicsPath track = PaintHelper.CreateRoundedRectangle(bounds, radius))
                {
                    using (var brush = new SolidBrush(_trackColor))
                    {
                        g.FillPath(brush, track);
                    }

                    int filled = FilledWidth;
                    if (filled > 0)
                    {
                        // 進んだ分も、両端を丸くする。帯の形からはみ出さないよう、帯の形で切り抜く
                        // （値が小さいときは、丸い形を保てる幅（高さと同じ）で描いて、帯の左端の丸みで切る）
                        g.SetClip(new RectangleF(0f, 0f, filled, Height), CombineMode.Intersect);
                        var barBounds = new RectangleF(0f, 0f, Math.Max(filled, Height) - 1, Height - 1);
                        using (GraphicsPath bar = PaintHelper.CreateRoundedRectangle(barBounds, radius))
                        using (var brush = new SolidBrush(Enabled ? _barColor : SControlStyle.GetInactiveColor(_barColor, _trackColor)))
                        {
                            g.FillPath(brush, bar);
                        }
                    }
                }
            }
            finally
            {
                g.Clip = previousClip;
                g.SmoothingMode = previousSmoothing;
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        #endregion
    }
}
