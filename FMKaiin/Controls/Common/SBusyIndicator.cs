using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 処理中であることを示す、回る輪。薄い色の輪（下地）の上を、色の付いた弧が回る。
    /// 画像を切り替えるのではなく、線で描くので、どの大きさ・表示倍率でも、にじまない。
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="Active"/> が true で、コントロールが表示されている間だけ、回る（タイマーを動かす）。
    /// 非表示にする・<see cref="Active"/> を false にする・フォームを閉じると、止まる。デザイナーでは、回さない。
    /// </para>
    /// <para>
    /// 輪は、コントロールの中央に、幅と高さの小さいほうに合わせて描く。背景は、既定で透明（親コントロールと同じ見た目）。
    /// フォーカスは受け取らない。
    /// </para>
    /// </remarks>
    public class SBusyIndicator : Control
    {
        /// <summary>弧を 1 回に進める角度（度）と、進める間隔（ミリ秒）。1 回転に、約 0.75 秒。</summary>
        private const int StepAngle = 24;
        private const int StepInterval = 50;

        /// <summary>弧の長さ（度）。</summary>
        private const float SweepAngle = 100f;

        // 既定の色。下地は、進み具合のバー（SProgressBar）の下地と同じ。弧は、チェック ボックスのオンの色と同じ系統の青
        private static readonly Color DefaultTrackColor = Color.FromArgb(226, 232, 240);
        private static readonly Color DefaultIndicatorColor = Color.FromArgb(34, 103, 182);

        private readonly Timer _timer;
        private bool _active = true;
        private float _lineWeight = 0.11f;
        private Color _trackColor = DefaultTrackColor;
        private Color _indicatorColor = DefaultIndicatorColor;

        // 弧の始まりの角度（度。真上を 0 として、時計回り）
        private int _angle;

        public SBusyIndicator()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                // 親の BackColor が Transparent のときに、親の描画内容の上へ輪だけを描けるようにする
                ControlStyles.SupportsTransparentBackColor,
                true);
            SetStyle(ControlStyles.Selectable, false);
            TabStop = false;
            base.BackColor = Color.Transparent;

            _timer = new Timer { Interval = StepInterval };
            _timer.Tick += OnTimerTick;
        }

        #region プロパティ

        /// <summary>回すか。既定は true。false にすると、止まった状態で表示する。</summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("回すか。false にすると、止まった状態で表示する。")]
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                UpdateTimer();
            }
        }

        /// <summary>線の太さ。輪の直径に対する割合で指定する。既定は 0.11。</summary>
        [Category("Appearance")]
        [DefaultValue(0.11f)]
        [Description("線の太さ。輪の直径に対する割合で指定する。")]
        public float LineWeight
        {
            get { return _lineWeight; }
            set { _lineWeight = Math.Max(0.02f, Math.Min(0.4f, value)); Invalidate(); }
        }

        /// <summary>下地の輪の色。</summary>
        [Category("Appearance")]
        [Description("下地の輪の色。")]
        public Color TrackColor
        {
            get { return _trackColor; }
            set { _trackColor = value.IsEmpty ? DefaultTrackColor : value; Invalidate(); }
        }

        /// <summary>回る弧の色。</summary>
        [Category("Appearance")]
        [Description("回る弧の色。")]
        public Color IndicatorColor
        {
            get { return _indicatorColor; }
            set { _indicatorColor = value.IsEmpty ? DefaultIndicatorColor : value; Invalidate(); }
        }

        /// <summary>背景の色。既定は透明（親コントロールと同じ見た目にする）。</summary>
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value.IsEmpty ? Color.Transparent : value; }
        }

        /// <summary>いま、回っているか（タイマーが動いているか）。</summary>
        [Browsable(false)]
        public bool IsRunning
        {
            get { return _timer.Enabled; }
        }

        // デザイナーが「既定値と違うときだけ」Designer.cs に書き出すようにする
        private bool ShouldSerializeTrackColor() { return _trackColor != DefaultTrackColor; }
        private void ResetTrackColor() { TrackColor = Color.Empty; }
        private bool ShouldSerializeIndicatorColor() { return _indicatorColor != DefaultIndicatorColor; }
        private void ResetIndicatorColor() { IndicatorColor = Color.Empty; }
        private bool ShouldSerializeBackColor() { return BackColor != Color.Transparent; }
        public override void ResetBackColor() { base.BackColor = Color.Transparent; }

        protected override Size DefaultSize
        {
            get { return new Size(36, 36); }
        }

        #endregion

        #region 回す・止める

        /// <summary>
        /// 回すべき状態（回す設定で、表示されていて、デザイナーの中ではない）なら、タイマーを動かす。そうでなければ、止める。
        /// </summary>
        private void UpdateTimer()
        {
            bool run = _active && Visible && IsHandleCreated && !IsDisposed && !DesignMode;
            if (_timer.Enabled != run)
            {
                _timer.Enabled = run;
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _angle = (_angle + StepAngle) % 360;
            Invalidate();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            UpdateTimer();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateTimer();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            _timer.Stop();
            base.OnHandleDestroyed(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region 描画

        protected override void OnPaint(PaintEventArgs e)
        {
            int diameter = Math.Min(Width, Height);
            if (diameter <= 4)
            {
                return;
            }

            // 線の太さの分だけ内側に、輪を描く（線の外側が、コントロールからはみ出さないように）
            float thickness = Math.Max(1.5f, diameter * _lineWeight);
            float size = diameter - thickness - 1f;
            var bounds = new RectangleF((Width - size) / 2f, (Height - size) / 2f, size, size);

            Graphics g = e.Graphics;
            SmoothingMode previous = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            try
            {
                using (var track = new Pen(_trackColor, thickness))
                {
                    g.DrawEllipse(track, bounds);
                }
                using (var arc = new Pen(Enabled ? _indicatorColor : SControlStyle.GetInactiveColor(_indicatorColor, _trackColor), thickness))
                {
                    arc.StartCap = LineCap.Round;
                    arc.EndCap = LineCap.Round;
                    // DrawArc の角度は、右（3 時の方向）が 0。真上から始めるので、90 度戻す
                    g.DrawArc(arc, bounds, _angle - 90f, SweepAngle);
                }
            }
            finally
            {
                g.SmoothingMode = previous;
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
