using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 入力コントロールのすぐ下に出す、小さなウィンドウ（一覧・カレンダー）の土台。
    /// フォーカスを受け取らないウィンドウで、表示中もキー入力は持ち主のコントロールに届く。
    /// 中身の描画とマウス操作は、継承したクラスが行う。
    /// </summary>
    /// <remarks>
    /// このクラスは、自分からは閉じない。外側がクリックされた・親のフォームが動いた、などの
    /// 「閉じるべきこと」が起きたら <see cref="CloseRequested"/> で知らせるので、持ち主のコントロールが Hide() を呼ぶ。
    /// </remarks>
    internal abstract class SPopupForm : Form, IMessageFilter
    {
        private const int WM_MOUSEACTIVATE = 0x0021;
        private const int WM_NCLBUTTONDOWN = 0x00A1;
        private const int WM_NCRBUTTONDOWN = 0x00A4;
        private const int WM_NCMBUTTONDOWN = 0x00A7;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_MBUTTONDOWN = 0x0207;

        private const int MA_NOACTIVATE = 3;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int CS_DROPSHADOW = 0x00020000;

        private readonly Control _owner;
        private Form _hookedForm;
        private bool _filterAdded;

        /// <param name="owner">持ち主のコントロール。このウィンドウは、その下（または上）に表示する。</param>
        protected SPopupForm(Control owner)
        {
            _owner = owner;

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            AutoScaleMode = AutoScaleMode.None;
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);
        }

        /// <summary>
        /// 閉じるべきことが起きたときに発生する。このウィンドウと持ち主のコントロール以外がクリックされた、
        /// 親のフォームが動いた・大きさが変わった・ほかのウィンドウへ切り替わった、のどれか。
        /// </summary>
        public event EventHandler CloseRequested;

        /// <summary>このウィンドウが閉じた（非表示になった）ときに発生する。</summary>
        public event EventHandler PopupClosed;

        /// <summary>持ち主のコントロール。</summary>
        protected Control OwnerControl
        {
            get { return _owner; }
        }

        /// <summary>
        /// このウィンドウを、持ち主のコントロールのすぐ下に、左端をそろえて置く。画面の下にはみ出すなら上に置く。
        /// </summary>
        protected void PlaceNearOwner(int width, int height)
        {
            Rectangle anchor = _owner.Parent != null
                ? _owner.Parent.RectangleToScreen(_owner.Bounds)
                : new Rectangle(_owner.PointToScreen(Point.Empty), _owner.Size);
            Rectangle screen = Screen.FromRectangle(anchor).WorkingArea;

            int x = Math.Max(screen.Left, Math.Min(anchor.Left, screen.Right - width));
            int y = anchor.Bottom + 1;
            if (y + height > screen.Bottom && anchor.Top - 1 - height >= screen.Top)
            {
                y = anchor.Top - 1 - height;
            }
            Bounds = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// このウィンドウを表示する（表示済みなら何もしない）。フォーカスは移さない。
        /// </summary>
        protected void ShowPopup()
        {
            if (Visible)
            {
                return;
            }

            // 所有者を指定して表示すると、親のフォームより常に手前に表示される
            Control topLevel = _owner.TopLevelControl;
            if (topLevel != null)
            {
                Show(topLevel);
            }
            else
            {
                Show();
            }
        }

        #region フォーカスを受け取らないウィンドウにする

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // クリックしてもアクティブにならず、タスク バーや Alt+Tab にも出ないウィンドウにする。影を付ける
                cp.ExStyle |= WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MOUSEACTIVATE)
            {
                // クリックされても、フォーカスを持ち主のコントロールから奪わない
                m.Result = (IntPtr)MA_NOACTIVATE;
                return;
            }
            base.WndProc(ref m);
        }

        #endregion

        #region 表示中の監視

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                StartWatching();
            }
            else
            {
                StopWatching();

                EventHandler handler = PopupClosed;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopWatching();
            }
            base.Dispose(disposing);
        }

        private void StartWatching()
        {
            if (!_filterAdded)
            {
                Application.AddMessageFilter(this);
                _filterAdded = true;
            }

            Form form = _owner.TopLevelControl as Form;
            if (form != null && _hookedForm == null)
            {
                _hookedForm = form;
                form.Deactivate += OwnerFormChanged;
                form.LocationChanged += OwnerFormChanged;
                form.SizeChanged += OwnerFormChanged;
            }
        }

        private void StopWatching()
        {
            if (_filterAdded)
            {
                Application.RemoveMessageFilter(this);
                _filterAdded = false;
            }

            if (_hookedForm != null)
            {
                _hookedForm.Deactivate -= OwnerFormChanged;
                _hookedForm.LocationChanged -= OwnerFormChanged;
                _hookedForm.SizeChanged -= OwnerFormChanged;
                _hookedForm = null;
            }
        }

        private void OwnerFormChanged(object sender, EventArgs e)
        {
            RequestClose();
        }

        private void RequestClose()
        {
            EventHandler handler = CloseRequested;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// アプリ内のマウスのボタン操作を、各ウィンドウへ届く前に調べる。このウィンドウと持ち主のコントロール以外が押されたら、
        /// 閉じるよう求める（フォームの余白のように、押してもフォーカスが移らない場所があるため、フォーカスの移動だけでは閉じられない）。
        /// </summary>
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_LBUTTONDOWN:
                case WM_RBUTTONDOWN:
                case WM_MBUTTONDOWN:
                case WM_NCLBUTTONDOWN:
                case WM_NCRBUTTONDOWN:
                case WM_NCMBUTTONDOWN:
                    bool inside = m.HWnd == Handle || IsChild(Handle, m.HWnd) || m.HWnd == _owner.Handle;
                    if (!inside)
                    {
                        RequestClose();
                    }
                    break;
            }

            // メッセージは止めない（押された先のコントロールにも、そのまま届ける）
            return false;
        }

        #endregion

        [DllImport("user32.dll")]
        private static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);
    }
}
