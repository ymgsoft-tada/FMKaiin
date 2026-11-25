using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// <see cref="FunctionKeyBar.FunctionKeyPressed"/> イベントの引数。
    /// </summary>
    public class FunctionKeyEventArgs : EventArgs
    {
        public FunctionKeyEventArgs(FunctionKeyBarButton button)
        {
            Button = button;
        }

        /// <summary>押されたボタン。</summary>
        public FunctionKeyBarButton Button { get; private set; }

        /// <summary>押されたキー。</summary>
        public Keys Key
        {
            get { return Button.Key; }
        }
    }

    /// <summary>
    /// ファンクションキーのボタンを横に並べて表示する帯。画面の下端に置いて使う（既定で下端にドッキングする）。
    /// ボタンは <see cref="AddButton"/> で追加する。追加した順に左から並び、右寄せに指定したボタンは右端に並ぶ。
    /// ボタンをクリックするか、ボタンに対応するキー（F5 など）を押すと <see cref="FunctionKeyPressed"/> が発生する。
    /// <para>
    /// キーは、画面のどのコントロールにフォーカスがあっても受け取る。入力欄やグリッドより先に受け取るので、
    /// F4 でコンボボックスが開くといった各コントロールの動作よりも、この帯のボタンが優先される。
    /// 受け取るのは、この帯を置いたウィンドウがアクティブなときだけ。
    /// </para>
    /// <para>
    /// ボタンが帯の幅に収まらない場合は、間隔と余白を詰め、それでも足りなければ文字を小さくして収める。
    /// </para>
    /// 仕様は _doc/spec/ファンクションキー.md にある。
    /// </summary>
    [DefaultEvent("FunctionKeyPressed")]
    public class FunctionKeyBar : Panel
    {
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        /// <summary>キー メッセージの lParam のうち、「直前もそのキーが押されていた」（自動リピート）を表すビット。</summary>
        private const long KeyRepeatFlag = 0x40000000;

        // 以下の寸法はすべて 96 DPI 基準のピクセル
        /// <summary>帯の既定の高さ。</summary>
        private const int DefaultBarHeight = 66;
        /// <summary>帯の上端から区切り線までの間隔。</summary>
        private const int SeparatorTop = 6;
        /// <summary>ボタン本体の高さ。</summary>
        private const int ButtonHeight = 36;
        /// <summary>帯の左右の端からボタンまでの余白。</summary>
        private const int SideMargin = 12;
        /// <summary>帯の上下の端からボタンまでの余白の下限。</summary>
        private const int MinimumVerticalMargin = 3;

        private const string FontFamilyName = "BIZ UDPゴシック";
        /// <summary><see cref="FontFamilyName"/> が PC に入っていない場合に使うフォント。</summary>
        private const string FallbackFontFamilyName = "Meiryo UI";
        /// <summary>名称の文字の大きさ（ポイント）。</summary>
        private const float BaseFontSize = 10.5f;
        /// <summary>幅に収めるために文字を小さくするときの下限（ポイント）。</summary>
        private const float MinimumFontSize = 9f;
        /// <summary>幅に収めるために文字を小さくするときの刻み（ポイント）。</summary>
        private const float FontSizeStep = 0.5f;
        /// <summary>名称の文字に対する、バッジの文字の大きさの比率。</summary>
        private const float BadgeFontRatio = 0.88f;

        /// <summary>ボタンの詰め方の 1 段階。</summary>
        private struct Density
        {
            /// <summary>ボタンどうしの間隔。</summary>
            public readonly int Gap;
            /// <summary>ボタン内の左右の余白。</summary>
            public readonly int InnerPadding;
            /// <summary>ボタン内の、バッジと名称の間隔。</summary>
            public readonly int BadgeGap;
            /// <summary>名称の文字の大きさ（ポイント）。</summary>
            public readonly float FontSize;

            public Density(int gap, int innerPadding, int badgeGap, float fontSize)
            {
                Gap = gap;
                InnerPadding = innerPadding;
                BadgeGap = badgeGap;
                FontSize = fontSize;
            }
        }

        /// <summary>標準の並べ方。</summary>
        private static readonly Density NormalDensity = new Density(8, 14, 8, BaseFontSize);
        /// <summary>間隔と余白を詰めた並べ方。文字の大きさは標準のまま。</summary>
        private static readonly Density TightDensity = new Density(4, 8, 8, BaseFontSize);
        /// <summary>間隔と余白を限界まで詰め、文字も下限まで小さくした並べ方。ボタンが多い画面の最後の手段。</summary>
        private static readonly Density CompactDensity = new Density(2, 5, 4, MinimumFontSize);

        /// <summary>
        /// キー メッセージを、ウィンドウに届く前に受け取るためのフィルター。
        /// </summary>
        private sealed class KeyMessageFilter : IMessageFilter
        {
            private readonly FunctionKeyBar _owner;

            public KeyMessageFilter(FunctionKeyBar owner)
            {
                _owner = owner;
            }

            public bool PreFilterMessage(ref Message m)
            {
                return _owner.PreFilterKeyMessage(ref m);
            }
        }

        private static readonly Color DefaultSeparatorColor = Color.FromArgb(214, 221, 230);

        private readonly List<FunctionKeyBarButton> _buttons = new List<FunctionKeyBarButton>();
        private readonly Dictionary<float, Font[]> _fonts = new Dictionary<float, Font[]>();
        private readonly KeyMessageFilter _keyFilter;
        private readonly bool _designTime;
        private string _fontFamilyName;
        private bool _keyFilterAdded;
        private bool _handleKeys = true;
        private bool _arranging;
        private Color _separatorColor = DefaultSeparatorColor;

        public FunctionKeyBar()
        {
            // デザイナーが作ったインスタンスかどうかは、コンストラクタの中でしか確実に分からないので、ここで覚えておく
            _designTime = LicenseManager.UsageMode == LicenseUsageMode.Designtime;
            _keyFilter = new KeyMessageFilter(this);

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor,
                true);
            SetStyle(ControlStyles.Selectable, false);

            base.BackColor = Color.Transparent;
            base.Dock = DockStyle.Bottom;
        }

        /// <summary>
        /// ボタンがクリックされたとき、またはボタンに対応するキーが押されたときに発生する。
        /// ボタンが無効・非表示のときと、この帯が無効のときは発生しない。
        /// </summary>
        [Category("Action")]
        [Description("ボタンがクリックされたとき、またはボタンに対応するキーが押されたときに発生する。")]
        public event EventHandler<FunctionKeyEventArgs> FunctionKeyPressed;

        /// <summary>
        /// ボタンに対応するキーが押されたことを検知するかどうか。false にすると、クリックだけを受け付ける。
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("ボタンに対応するキーが押されたことを検知するかどうか。false にすると、クリックだけを受け付ける。")]
        public bool HandleKeys
        {
            get { return _handleKeys; }
            set { _handleKeys = value; }
        }

        /// <summary>
        /// 画面の内容とこの帯を区切る線の色。線は帯の上端の少し下に、帯の幅いっぱいに引く。
        /// 空（<see cref="Color.Empty"/>）にすると線を引かず、ボタンは帯の上下中央に置く。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "214, 221, 230")]
        [Description("画面の内容とこの帯を区切る線の色。空にすると線を引かない。")]
        public Color SeparatorColor
        {
            get { return _separatorColor; }
            set
            {
                if (_separatorColor == value)
                {
                    return;
                }
                _separatorColor = value;
                PerformLayout();
                Invalidate();
            }
        }

        /// <summary>背景色。既定は透明で、親の背景がそのまま見える。</summary>
        [DefaultValue(typeof(Color), "Transparent")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>ドッキング先。既定は下端。</summary>
        [DefaultValue(DockStyle.Bottom)]
        public override DockStyle Dock
        {
            get { return base.Dock; }
            set { base.Dock = value; }
        }

        protected override Size DefaultSize
        {
            get { return new Size(200, DefaultBarHeight); }
        }

        /// <summary>
        /// ボタンを追加する。追加した順に左から並ぶ（右寄せにしたボタンは、追加した順に右端へ並ぶ）。
        /// </summary>
        /// <param name="key">ボタンに対応するキー。修飾キーを含まないキー コードで指定する。</param>
        /// <param name="text">ボタンに表示する名称。</param>
        /// <exception cref="ArgumentException"><paramref name="key"/> のボタンをすでに追加している場合。</exception>
        public FunctionKeyBarButton AddButton(Keys key, string text)
        {
            if (GetButton(key) != null)
            {
                throw new ArgumentException(key + " のボタンはすでに追加されています。", nameof(key));
            }

            var button = new FunctionKeyBarButton
            {
                Key = key,
                Text = text
            };
            button.Click += Button_Click;

            _buttons.Add(button);
            Controls.Add(button);
            PerformLayout();
            return button;
        }

        /// <summary>
        /// <paramref name="key"/> に対応するボタンを返す。無ければ null。
        /// </summary>
        public FunctionKeyBarButton GetButton(Keys key)
        {
            foreach (FunctionKeyBarButton button in _buttons)
            {
                if (button.Key == key)
                {
                    return button;
                }
            }
            return null;
        }

        /// <summary>
        /// すべてのボタンを取り除く。
        /// </summary>
        public void ClearButtons()
        {
            SuspendLayout();
            foreach (FunctionKeyBarButton button in _buttons)
            {
                button.Click -= Button_Click;
                Controls.Remove(button);
                button.Dispose();
            }
            _buttons.Clear();
            ResumeLayout();
        }

        #region 並べ方

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            // ボタンの位置や大きさを変えると、その通知でもう一度ここへ来るので、並べている最中は何もしない
            if (_arranging)
            {
                return;
            }

            _arranging = true;
            try
            {
                ArrangeButtons();
            }
            finally
            {
                _arranging = false;
            }
        }

        /// <summary>
        /// 表示するボタンの大きさと位置を決める。
        /// </summary>
        private void ArrangeButtons()
        {
            var visibleButtons = new List<FunctionKeyBarButton>();
            foreach (FunctionKeyBarButton button in _buttons)
            {
                if (button.VisibleRequested)
                {
                    visibleButtons.Add(button);
                }
            }
            if (visibleButtons.Count == 0)
            {
                return;
            }

            float scale = LayoutScale.Of(this);
            int sideMargin = (int)Math.Round(SideMargin * scale);
            int available = Math.Max(0, ClientSize.Width - sideMargin * 2);

            // 標準の並べ方から順に試し、帯の幅に収まった段階を使う。どれも収まらなければ、最も詰めた段階のままにする
            int[] widths = null;
            int gap = 0;
            foreach (Density density in GetDensities())
            {
                gap = (int)Math.Round(density.Gap * scale);
                widths = ApplyDensity(visibleButtons, density, scale);
                if (Sum(widths) + gap * (widths.Length - 1) <= available)
                {
                    break;
                }
            }

            // 最も詰めても収まらない場合は、各ボタンの幅を同じ割合で縮める（名称は末尾が「…」になる）
            int totalWidth = Sum(widths);
            int widthForButtons = Math.Max(0, available - gap * (widths.Length - 1));
            if (totalWidth > widthForButtons && totalWidth > 0)
            {
                for (int i = 0; i < widths.Length; i++)
                {
                    widths[i] = widths[i] * widthForButtons / totalWidth;
                }
            }

            // 縦は、区切り線より下の範囲の中央に置く（線を引かない場合は帯全体の中央）。
            // 帯が低い場合は、ボタンの高さを帯に合わせて抑える
            int areaTop = GetSeparatorBounds(scale).Bottom;
            int areaHeight = Height - areaTop;
            int shadowHeight = FunctionKeyBarButton.GetControlHeight(0, scale);
            int maxSurfaceHeight = areaHeight - (int)Math.Round(MinimumVerticalMargin * scale) * 2 - shadowHeight;
            int surfaceHeight = Math.Max(0, Math.Min((int)Math.Round(ButtonHeight * scale), maxSurfaceHeight));
            int controlHeight = FunctionKeyBarButton.GetControlHeight(surfaceHeight, scale);
            int top = areaTop + (areaHeight - controlHeight) / 2;

            // 左寄せのボタンは左端から、右寄せのボタンは右端に詰めて、どちらも追加した順に左から並べる
            int rightGroupWidth = 0;
            for (int i = 0; i < widths.Length; i++)
            {
                if (visibleButtons[i].Alignment == FunctionKeyAlignment.Right)
                {
                    rightGroupWidth += widths[i] + (rightGroupWidth > 0 ? gap : 0);
                }
            }

            int left = sideMargin;
            int right = ClientSize.Width - sideMargin - rightGroupWidth;
            for (int i = 0; i < widths.Length; i++)
            {
                FunctionKeyBarButton button = visibleButtons[i];
                if (button.Alignment == FunctionKeyAlignment.Right)
                {
                    button.SetBounds(right, top, widths[i], controlHeight);
                    right += widths[i] + gap;
                }
                else
                {
                    button.SetBounds(left, top, widths[i], controlHeight);
                    left += widths[i] + gap;
                }
            }
        }

        /// <summary>
        /// 試す順に並べた詰め方。標準、間隔と余白を詰めたもの、さらに文字を小さくしていったもの、限界まで詰めたもの、の順。
        /// </summary>
        private static IEnumerable<Density> GetDensities()
        {
            yield return NormalDensity;
            yield return TightDensity;
            for (float size = BaseFontSize - FontSizeStep; size >= MinimumFontSize; size -= FontSizeStep)
            {
                yield return new Density(TightDensity.Gap, TightDensity.InnerPadding, TightDensity.BadgeGap, size);
            }
            yield return CompactDensity;
        }

        /// <summary>
        /// <paramref name="buttons"/> に <paramref name="density"/> の余白とフォントを設定し、それぞれに必要な幅を返す。
        /// </summary>
        private int[] ApplyDensity(List<FunctionKeyBarButton> buttons, Density density, float scale)
        {
            Font[] fonts = GetFonts(density.FontSize);
            var widths = new int[buttons.Count];
            for (int i = 0; i < buttons.Count; i++)
            {
                FunctionKeyBarButton button = buttons[i];
                button.Font = fonts[0];
                button.BadgeFont = fonts[1];
                button.InnerPadding = density.InnerPadding;
                button.BadgeGap = density.BadgeGap;
                widths[i] = button.MeasureWidth(scale);
            }
            return widths;
        }

        /// <summary>
        /// 名称用（[0]）とバッジ用（[1]）のフォントを返す。同じ大きさのフォントは作り置きを使い回す。
        /// </summary>
        private Font[] GetFonts(float size)
        {
            Font[] fonts;
            if (!_fonts.TryGetValue(size, out fonts))
            {
                if (_fontFamilyName == null)
                {
                    _fontFamilyName = FontFallback.IsInstalled(FontFamilyName) ? FontFamilyName : FallbackFontFamilyName;
                }
                fonts = new[]
                {
                    new Font(_fontFamilyName, size, FontStyle.Bold, GraphicsUnit.Point),
                    new Font(_fontFamilyName, size * BadgeFontRatio, FontStyle.Bold, GraphicsUnit.Point)
                };
                _fonts.Add(size, fonts);
            }
            return fonts;
        }

        private static int Sum(int[] values)
        {
            int total = 0;
            foreach (int value in values)
            {
                total += value;
            }
            return total;
        }

        #endregion

        #region ボタンとキーの入力

        private void Button_Click(object sender, EventArgs e)
        {
            Press((FunctionKeyBarButton)sender);
        }

        /// <summary>
        /// <paramref name="button"/> が押されたことを知らせる。ボタンが無効・非表示のときと、この帯が無効のときは何もしない。
        /// </summary>
        private void Press(FunctionKeyBarButton button)
        {
            if (IsDisposed || !Enabled || !button.Enabled || !button.VisibleRequested || !_buttons.Contains(button))
            {
                return;
            }

            EventHandler<FunctionKeyEventArgs> handler = FunctionKeyPressed;
            if (handler != null)
            {
                handler(this, new FunctionKeyEventArgs(button));
            }
        }

        /// <summary>
        /// ウィンドウに届く前のメッセージを調べ、この帯のボタンに対応するキーが押されたものであれば受け取る。
        /// </summary>
        /// <returns>受け取った（ほかのコントロールには渡さない）場合は true。</returns>
        private bool PreFilterKeyMessage(ref Message m)
        {
            if (m.Msg != WM_KEYDOWN && m.Msg != WM_SYSKEYDOWN)
            {
                return false;
            }
            if (!_handleKeys || !Visible)
            {
                return false;
            }

            // 日本語入力の変換中は、ここへはキーの代わりに「IME が処理中」を表すコードが来るので、ボタンとは一致しない
            var keyCode = (Keys)((int)m.WParam.ToInt64() & (int)Keys.KeyCode);
            FunctionKeyBarButton button = GetButton(keyCode);
            if (button == null)
            {
                return false;
            }

            // Shift・Ctrl・Alt を押しながらの場合は対象外（Alt+F4 などを横取りしない）
            if (ModifierKeys != Keys.None)
            {
                return false;
            }

            // モーダルの子画面やメッセージ ボックスを開いている間は、この帯のあるウィンドウはアクティブではない
            Form activeForm = Form.ActiveForm;
            if (activeForm == null || activeForm != TopLevelControl)
            {
                return false;
            }

            // キーを押しっぱなしにしたときの自動リピートでは実行しない（キーは受け取って捨てる）
            bool repeated = (m.LParam.ToInt64() & KeyRepeatFlag) != 0;
            if (!repeated)
            {
                // ここはメッセージを配る処理の途中なので、画面を開いたり閉じたりする処理はここでは動かさず、
                // このメッセージの処理が終わった後で実行する
                BeginInvoke((MethodInvoker)delegate { Press(button); });
            }
            return true;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // デザイナーの上では、Visual Studio 自身のキー入力に割り込まないよう、キーを検知しない
            if (!_designTime && !DesignMode && !_keyFilterAdded)
            {
                Application.AddMessageFilter(_keyFilter);
                _keyFilterAdded = true;
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            RemoveKeyFilter();
            base.OnHandleDestroyed(e);
        }

        /// <summary>
        /// キーの検知をやめる。フィルターはアプリケーション側から参照され続けるので、
        /// 外さないと、この帯と帯を置いた画面が破棄された後も残ってしまう。
        /// </summary>
        private void RemoveKeyFilter()
        {
            if (_keyFilterAdded)
            {
                Application.RemoveMessageFilter(_keyFilter);
                _keyFilterAdded = false;
            }
        }

        #endregion

        /// <summary>
        /// 区切り線を引く範囲を返す。線を引かない場合は、高さ 0 の範囲（帯の上端）を返す。
        /// </summary>
        /// <param name="scale">96 DPI を 1 とした表示倍率。</param>
        private Rectangle GetSeparatorBounds(float scale)
        {
            if (_separatorColor.IsEmpty)
            {
                return new Rectangle(0, 0, Width, 0);
            }

            int thickness = Math.Max(1, (int)Math.Round(scale));
            return new Rectangle(0, (int)Math.Round(SeparatorTop * scale), Width, thickness);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle separator = GetSeparatorBounds(LayoutScale.Of(this));
            if (separator.Height > 0)
            {
                using (var brush = new SolidBrush(_separatorColor))
                {
                    e.Graphics.FillRectangle(brush, separator);
                }
            }

            // デザイナーの上でボタンが無いと、どこに置いたか分からないので、枠と名前を描いておく
            if ((_designTime || DesignMode) && _buttons.Count == 0)
            {
                var bounds = new Rectangle(0, 0, Width - 1, Height - 1);
                using (var pen = new Pen(SystemColors.ControlDark) { DashStyle = DashStyle.Dash })
                {
                    e.Graphics.DrawRectangle(pen, bounds);
                }
                TextRenderer.DrawText(
                    e.Graphics,
                    Name,
                    Font,
                    bounds,
                    SystemColors.ControlDarkDark,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                RemoveKeyFilter();
            }
            base.Dispose(disposing);

            // フォントはボタンが使っているので、ボタン（子コントロール）を破棄した後で破棄する
            if (disposing)
            {
                foreach (Font[] fonts in _fonts.Values)
                {
                    foreach (Font font in fonts)
                    {
                        font.Dispose();
                    }
                }
                _fonts.Clear();
            }
        }
    }
}
