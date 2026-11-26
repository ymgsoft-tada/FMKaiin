using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// フォームの上に、アイコン・タイトル・本文の付いたツールチップを表示する。入力の誤りを、その入力欄のそばで知らせるのに使う。
    /// comset の ComponentForm.ToolTip の代わりで、同じ呼び出し方ができる（メソッドの名前・引数が同じ）。
    /// 見た目は、S 系のコントロールに合わせて自前で描いている（<see cref="SToolTipPopup"/>）。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 表示したツールチップは、次のどれかで消える。
    /// 表示時間（<see cref="AutoPopDelay"/>）が過ぎた、<see cref="Hide"/> を呼んだ、別のツールチップを表示した、
    /// ツールチップそのもの、または指している入力欄以外の場所をクリックした、
    /// フォームが動いた・大きさが変わった・ほかのウィンドウへ切り替わった・閉じた、指している入力欄が破棄された。
    /// </para>
    /// <para>
    /// ComponentForm.ToolTip にあって、ここに無いもの: マウスを載せたときに表示するツールチップ
    /// （SetToolTip・RemoveAll と、そのための AutomaticDelay・InitialDelay・ReshowDelay）、ToolTipIcon プロパティ。
    /// </para>
    /// </remarks>
    public class SToolTip
    {
        /// <summary>入力欄の下端と、ツールチップの間のすき間。96 DPI 基準のピクセル。</summary>
        private const float Gap = 3f;

        private readonly Form _form;
        private readonly Timer _timer = new Timer();

        private SToolTipPopup _popup;
        private Control _target;
        private int _autoPopDelay = 5000;
        private string _caption = string.Empty;
        private string _text = string.Empty;
        private bool _closed;

        /// <param name="form">ツールチップを表示するフォーム。</param>
        public SToolTip(Form form)
        {
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            _form = form;
            _form.FormClosed += OnFormClosed;
            _timer.Tick += OnTimerTick;
        }

        #region プロパティ

        /// <summary>ツールチップを表示しておく時間（ミリ秒）。既定は 5000（5 秒）。</summary>
        public int AutoPopDelay
        {
            get { return _autoPopDelay; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _autoPopDelay = value;
            }
        }

        /// <summary>いま、ツールチップを表示しているか。</summary>
        public bool Active
        {
            get { return _popup != null; }
        }

        /// <summary>表示しているツールチップのタイトル。表示していなければ空。</summary>
        public string Caption
        {
            get { return _caption; }
        }

        /// <summary>表示しているツールチップの本文。表示していなければ空。</summary>
        public string Text
        {
            get { return _text; }
        }

        #endregion

        #region 表示（種類ごと）

        /// <summary>情報のツールチップを、画面の座標 (<paramref name="x0"/>, <paramref name="y0"/>) に表示する。</summary>
        public void Info(int x0, int y0, string caption, string text)
        {
            Show(x0, y0, caption, text, ToolTipIcon.Info);
        }

        /// <summary>情報のツールチップを、<paramref name="ctl"/> の左上から (<paramref name="x0"/>, <paramref name="y0"/>) の位置に表示する。</summary>
        public void Info(Control ctl, int x0, int y0, string caption, string text)
        {
            Show(ctl, x0, y0, caption, text, ToolTipIcon.Info);
        }

        /// <summary>情報のツールチップを、<paramref name="ctl"/> の下に表示する。</summary>
        public void Info(Control ctl, string caption, string text)
        {
            Show(ctl, caption, text, ToolTipIcon.Info);
        }

        /// <summary>エラーのツールチップを、画面の座標 (<paramref name="x0"/>, <paramref name="y0"/>) に表示して、エラーの音を鳴らす。</summary>
        public void Error(int x0, int y0, string caption, string text)
        {
            Show(x0, y0, caption, text, ToolTipIcon.Error);
            SystemSounds.Hand.Play();
        }

        /// <summary>エラーのツールチップを、<paramref name="ctl"/> の左上から (<paramref name="x0"/>, <paramref name="y0"/>) の位置に表示して、エラーの音を鳴らす。</summary>
        public void Error(Control ctl, int x0, int y0, string caption, string text)
        {
            Show(ctl, x0, y0, caption, text, ToolTipIcon.Error);
            SystemSounds.Hand.Play();
        }

        /// <summary>エラーのツールチップを、<paramref name="ctl"/> の下に表示して、エラーの音を鳴らす。</summary>
        public void Error(Control ctl, string caption, string text)
        {
            Show(ctl, caption, text, ToolTipIcon.Error);
            SystemSounds.Hand.Play();
        }

        /// <summary>警告のツールチップを、画面の座標 (<paramref name="x0"/>, <paramref name="y0"/>) に表示して、警告の音を鳴らす。</summary>
        public void Warning(int x0, int y0, string caption, string text)
        {
            Show(x0, y0, caption, text, ToolTipIcon.Warning);
            SystemSounds.Exclamation.Play();
        }

        /// <summary>警告のツールチップを、<paramref name="ctl"/> の左上から (<paramref name="x0"/>, <paramref name="y0"/>) の位置に表示して、警告の音を鳴らす。</summary>
        public void Warning(Control ctl, int x0, int y0, string caption, string text)
        {
            Show(ctl, x0, y0, caption, text, ToolTipIcon.Warning);
            SystemSounds.Exclamation.Play();
        }

        /// <summary>警告のツールチップを、<paramref name="ctl"/> の下に表示して、警告の音を鳴らす。</summary>
        public void Warning(Control ctl, string caption, string text)
        {
            Show(ctl, caption, text, ToolTipIcon.Warning);
            SystemSounds.Exclamation.Play();
        }

        /// <summary>アイコンとタイトルの無いツールチップ（補足の説明）を、画面の座標 (<paramref name="x0"/>, <paramref name="y0"/>) に表示する。</summary>
        public void Tips(int x0, int y0, string text)
        {
            Show(x0, y0, string.Empty, text, ToolTipIcon.None);
        }

        /// <summary>アイコンとタイトルの無いツールチップ（補足の説明）を、<paramref name="ctl"/> の左上から (<paramref name="x0"/>, <paramref name="y0"/>) の位置に表示する。</summary>
        public void Tips(Control ctl, int x0, int y0, string text)
        {
            Show(ctl, x0, y0, string.Empty, text, ToolTipIcon.None);
        }

        /// <summary>アイコンとタイトルの無いツールチップ（補足の説明）を、<paramref name="ctl"/> の下に表示する。</summary>
        public void Tips(Control ctl, string text)
        {
            Show(ctl, string.Empty, text, ToolTipIcon.None);
        }

        #endregion

        #region 表示と非表示

        /// <summary>
        /// ツールチップを、左上が画面の座標 (<paramref name="x"/>, <paramref name="y"/>) に来るように表示する。
        /// </summary>
        public void Show(int x, int y, string caption, string text, ToolTipIcon tooltipicon)
        {
            ShowCore(null, new Point(x, y), y, caption, text, tooltipicon);
        }

        /// <summary>
        /// ツールチップを、左上が <paramref name="ctl"/> の左上から (<paramref name="x"/>, <paramref name="y"/>) の位置に来るように表示する。
        /// </summary>
        public void Show(Control ctl, int x, int y, string caption, string text, ToolTipIcon tooltipicon)
        {
            if (ctl == null)
            {
                throw new ArgumentNullException(nameof(ctl));
            }

            Rectangle bounds = GetScreenBounds(ctl);
            var location = new Point(bounds.Left + x, bounds.Top + y);
            ShowCore(ctl, location, location.Y, caption, text, tooltipicon);
        }

        /// <summary>
        /// ツールチップを、<paramref name="ctl"/> の下に表示する。左端は <paramref name="ctl"/> の左右の中央に合わせる
        /// （すぐ下の行の、項目名と入力欄の先頭を隠さないため）。画面の下にはみ出すなら、<paramref name="ctl"/> の上に表示する。
        /// </summary>
        public void Show(Control ctl, string caption, string text, ToolTipIcon tooltipicon)
        {
            if (ctl == null)
            {
                throw new ArgumentNullException(nameof(ctl));
            }

            int gap = (int)Math.Round(Gap * LayoutScale.Of(ctl));
            Rectangle bounds = GetScreenBounds(ctl);
            var location = new Point(bounds.Left + bounds.Width / 2, bounds.Bottom + gap);
            ShowCore(ctl, location, bounds.Top - gap, caption, text, tooltipicon);
        }

        /// <summary>
        /// <paramref name="ctl"/> の、枠を含めた全体の位置と大きさ（画面の座標）。
        /// </summary>
        /// <remarks>
        /// S 系の入力欄は、枠の内側だけが文字を入力する部分（クライアント領域）なので、PointToScreen で求めると枠の分だけずれる。
        /// 親から見た位置を使って求める。
        /// </remarks>
        private static Rectangle GetScreenBounds(Control ctl)
        {
            return ctl.Parent != null
                ? ctl.Parent.RectangleToScreen(ctl.Bounds)
                : new Rectangle(ctl.PointToScreen(Point.Empty), ctl.Size);
        }

        /// <summary>表示しているツールチップを消す。表示していなければ何もしない。</summary>
        public void Hide()
        {
            _timer.Stop();

            if (_target != null)
            {
                _target.Disposed -= OnTargetDisposed;
                _target = null;
            }

            if (_popup != null)
            {
                SToolTipPopup popup = _popup;
                _popup = null;
                popup.CloseRequested -= OnPopupCloseRequested;
                popup.MouseDown -= OnPopupMouseDown;
                popup.Dispose();
            }

            _caption = string.Empty;
            _text = string.Empty;
        }

        private void ShowCore(Control target, Point location, int flipBottom, string caption, string text, ToolTipIcon icon)
        {
            // 現在の表示があれば消す
            Hide();

            caption = caption ?? string.Empty;
            text = text ?? string.Empty;
            Control owner = target ?? _form;
            if (_closed || _form.IsDisposed || owner.IsDisposed || (caption.Length == 0 && text.Length == 0))
            {
                return;
            }

            _popup = new SToolTipPopup(owner, caption, text, icon);
            _popup.CloseRequested += OnPopupCloseRequested;
            _popup.MouseDown += OnPopupMouseDown;
            _popup.ShowAt(location, flipBottom);

            if (target != null)
            {
                _target = target;
                _target.Disposed += OnTargetDisposed;
            }

            _caption = caption;
            _text = text;

            _timer.Interval = _autoPopDelay;
            _timer.Start();
        }

        #endregion

        #region 消すきっかけ

        private void OnTimerTick(object sender, EventArgs e)
        {
            Hide();
        }

        private void OnPopupCloseRequested(object sender, EventArgs e)
        {
            Hide();
        }

        private void OnPopupMouseDown(object sender, MouseEventArgs e)
        {
            Hide();
        }

        private void OnTargetDisposed(object sender, EventArgs e)
        {
            Hide();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            // フォームを閉じた後は、もう表示しない
            Hide();
            _closed = true;
            _timer.Dispose();
            _form.FormClosed -= OnFormClosed;
        }

        #endregion
    }
}
