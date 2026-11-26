using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// メッセージ ボックス。comset の ComponentForm.MsgBox の代わりで、同じ呼び出し方ができる（コンストラクター・Show・Result・CloseReason が同じ）。
    /// 見た目は、S 系のコントロールに合わせて自前で描いている。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 表示する文字列では、次の指定ができる（ComponentForm.MsgBox と同じ書き方）。
    /// <list type="bullet">
    /// <item>\n で改行する。行数に制限は無い。長い行は、幅に合わせて折り返す。</item>
    /// <item>行の先頭に @c(色の名前) を書くと、その行から後ろの文字の色を変える。色の名前は Color の名前（Red など）。</item>
    /// <item>行の先頭に @b を書くと、その行だけ太字にする。</item>
    /// </list>
    /// </para>
    /// <para>
    /// 右上の × や Esc で閉じたときの結果は、ボタンの組み合わせで決まる。
    /// 「はい・いいえ」は いいえ、「OK・キャンセル」「再試行・キャンセル」「はい・いいえ・キャンセル」は キャンセル、
    /// 「中止・再試行・無視」は 中止、「OK」だけのときは None。このとき <see cref="CloseReason"/> は WindowCancel になる。
    /// </para>
    /// </remarks>
    public class SMsgBox : Form
    {
        /// <summary>
        /// ダイアログが閉じられた理由。
        /// </summary>
        /// <remarks>名前は、ComponentForm.MsgBox と同じにしてある（呼び出す側を書き換えずに済むように）。</remarks>
        public enum eCloseReason
        {
            /// <summary>なし。</summary>
            None,
            /// <summary>通常（ボタンを押した）。</summary>
            Default,
            /// <summary>右上の × を押した、または Esc を押した。</summary>
            WindowCancel,
        }

        #region 大きさと色

        // ここの大きさは、すべて 96 DPI 基準のピクセル
        private const float ContentPaddingX = 24f;
        private const float ContentPaddingY = 22f;
        private const float IconSize = 36f;
        private const float IconGap = 16f;
        private const float LineGap = 3f;

        /// <summary>文字の幅の上限。これより長い行は折り返す。全角で 40 文字ほどの 1 行は、折り返さずに収まる幅にしてある。</summary>
        private const float MaxTextWidth = 600f;

        /// <summary>ダイアログの幅（タイトル バーより下の部分）の下限。</summary>
        private const float MinClientWidth = 360f;

        private const float FooterHeight = 60f;
        private const float FooterPaddingX = 20f;
        private const float ButtonHeight = 32f;
        private const float ButtonMinWidth = 96f;
        private const float ButtonPaddingX = 18f;
        private const float ButtonGap = 10f;

        // ボタンを置く帯と、その上の線。テーブル コンボの一覧・グリッドの見出しと同じ色
        private static readonly Color FooterBackColor = Color.FromArgb(243, 246, 250);
        private static readonly Color FooterLineColor = Color.FromArgb(226, 232, 240);

        private const TextFormatFlags TextFlags =
            TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.WordBreak | TextFormatFlags.Left | TextFormatFlags.Top;

        #endregion

        /// <summary>表示する文字列の 1 行分。</summary>
        internal sealed class Line
        {
            public string Text;
            public Color Color;
            public bool Bold;
            public Rectangle Bounds;
        }

        private readonly MessageBoxButtons _buttons;
        private readonly MessageBoxIcon _icon;
        private readonly List<Line> _lines;
        private readonly Font _boldFont;

        private Rectangle _iconBounds;
        private Rectangle _footerBounds;
        private DialogResult _result = DialogResult.None;
        private eCloseReason _closeReason = eCloseReason.Default;

        /// <summary>
        /// 指定は、標準の MessageBox と同じ。
        /// </summary>
        /// <param name="owner">親のウィンドウ（使っていない。ComponentForm.MsgBox と引数をそろえるためのもの）。</param>
        /// <param name="text">表示する文字列。\n・@c(色)・@b が使える。</param>
        /// <param name="caption">タイトル バーの文字列。</param>
        /// <param name="defbutton">最初にフォーカスを置くボタン。</param>
        public SMsgBox(
            IWin32Window owner,
            string text,
            string caption,
            MessageBoxButtons buttons,
            MessageBoxIcon icons,
            MessageBoxDefaultButton defbutton)
        {
            _buttons = buttons;
            _icon = icons;

            Text = caption ?? string.Empty;
            Font = SControlStyle.Font;
            _boldFont = new Font(SControlStyle.Font, FontStyle.Bold);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            AutoScaleMode = AutoScaleMode.None;
            StartPosition = FormStartPosition.CenterParent;
            DoubleBuffered = true;

            _lines = ParseLines(text, SControlStyle.TextColor);
            LayoutContent(CreateButtons(defbutton));
        }

        #region プロパティ

        /// <summary>押されたボタン。</summary>
        public DialogResult Result
        {
            get { return _result; }
        }

        /// <summary>閉じられた理由。</summary>
        public eCloseReason CloseReason
        {
            get { return _closeReason; }
        }

        /// <summary>表示したときに、アイコンに合わせた音を鳴らすか。既定は true。</summary>
        public bool PlaySound { get; set; } = true;

        #endregion

        #region 表示（static）

        /// <summary>メッセージ ボックスを表示する。指定は、標準の MessageBox と同じ。</summary>
        /// <returns>押されたボタン。</returns>
        public static DialogResult Show(string text)
        {
            return Show((IWin32Window)null, text);
        }

        /// <summary>メッセージ ボックスを表示する。指定は、標準の MessageBox と同じ。</summary>
        public static DialogResult Show(string text, string caption)
        {
            return Show(null, text, caption);
        }

        /// <summary>メッセージ ボックスを表示する。指定は、標準の MessageBox と同じ。</summary>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return Show(null, text, caption, buttons);
        }

        /// <summary>メッセージ ボックスを表示する。指定は、標準の MessageBox と同じ。</summary>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icons)
        {
            return Show(null, text, caption, buttons, icons);
        }

        /// <summary>メッセージ ボックスを表示する。指定は、標準の MessageBox と同じ。</summary>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icons, MessageBoxDefaultButton defbutton)
        {
            return Show(null, text, caption, buttons, icons, defbutton);
        }

        /// <summary>メッセージ ボックスを表示する。指定は、標準の MessageBox と同じ。</summary>
        public static DialogResult Show(IWin32Window owner, string text)
        {
            return Show(owner, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        /// <summary>メッセージ ボックスを表示する。指定は、標準の MessageBox と同じ。</summary>
        public static DialogResult Show(IWin32Window owner, string text, string caption)
        {
            return Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        /// <summary>メッセージ ボックスを表示する。指定は、標準の MessageBox と同じ。</summary>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            return Show(owner, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        /// <summary>メッセージ ボックスを表示する。指定は、標準の MessageBox と同じ。</summary>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icons)
        {
            return Show(owner, text, caption, buttons, icons, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// メッセージ ボックスを表示する。指定は、標準の MessageBox と同じ。
        /// <paramref name="owner"/> があればその中央に、無ければ画面の中央に表示する。
        /// </summary>
        /// <returns>押されたボタン。</returns>
        public static DialogResult Show(
            IWin32Window owner,
            string text,
            string caption,
            MessageBoxButtons buttons,
            MessageBoxIcon icons,
            MessageBoxDefaultButton defbutton)
        {
            using (var form = new SMsgBox(owner, text, caption, buttons, icons, defbutton))
            {
                form.StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;
                form.ShowDialog();
                return form.Result;
            }
        }

        #endregion

        #region 文字列の解釈

        /// <summary>
        /// 表示する文字列を、行ごとに分けて、行の先頭の指定（@c(色)・@b）を取り出す。
        /// </summary>
        /// <param name="defaultColor">色の指定が無いときの文字の色。</param>
        internal static List<Line> ParseLines(string text, Color defaultColor)
        {
            var lines = new List<Line>();
            Color color = defaultColor;

            foreach (string source in (text ?? string.Empty).Replace("\r\n", "\n").Split('\n'))
            {
                string rest = source;
                bool bold = false;

                // 指定は、行の先頭にいくつ並んでいてもよい
                while (rest.Length > 0 && rest[0] == '@')
                {
                    char kind = rest.Length > 1 ? char.ToLowerInvariant(rest[1]) : '\0';
                    int open = rest.IndexOf('(');
                    int close = rest.IndexOf(')');

                    if (kind == 'c' && open == 2 && close > open)
                    {
                        // 色は、後ろの行にも引き継ぐ。知らない名前のときは、色を変えない
                        Color named = Color.FromName(rest.Substring(open + 1, close - open - 1).Trim());
                        if (named.IsKnownColor)
                        {
                            color = named;
                        }
                        rest = rest.Substring(close + 1);
                    }
                    else if (kind == 'b')
                    {
                        // 太字は、この行だけ
                        bold = true;
                        rest = rest.Substring(2);
                    }
                    else
                    {
                        // 知らない指定は、@ だけを取り除く
                        rest = rest.Substring(1);
                    }
                }

                lines.Add(new Line { Text = rest, Color = color, Bold = bold });
            }

            return lines;
        }

        #endregion

        #region 配置

        /// <summary>ボタンを作る。並べる順（左から）に返す。</summary>
        private List<SMsgBoxButton> CreateButtons(MessageBoxDefaultButton defbutton)
        {
            var buttons = new List<SMsgBoxButton>();
            switch (_buttons)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    buttons.Add(CreateButton("中止(&A)", DialogResult.Abort));
                    buttons.Add(CreateButton("再試行(&R)", DialogResult.Retry));
                    buttons.Add(CreateButton("無視(&I)", DialogResult.Ignore));
                    break;
                case MessageBoxButtons.OKCancel:
                    buttons.Add(CreateButton("OK", DialogResult.OK));
                    buttons.Add(CreateButton("キャンセル", DialogResult.Cancel));
                    break;
                case MessageBoxButtons.RetryCancel:
                    buttons.Add(CreateButton("再試行(&R)", DialogResult.Retry));
                    buttons.Add(CreateButton("キャンセル", DialogResult.Cancel));
                    break;
                case MessageBoxButtons.YesNo:
                    buttons.Add(CreateButton("はい(&Y)", DialogResult.Yes));
                    buttons.Add(CreateButton("いいえ(&N)", DialogResult.No));
                    break;
                case MessageBoxButtons.YesNoCancel:
                    buttons.Add(CreateButton("はい(&Y)", DialogResult.Yes));
                    buttons.Add(CreateButton("いいえ(&N)", DialogResult.No));
                    buttons.Add(CreateButton("キャンセル", DialogResult.Cancel));
                    break;
                default:
                    buttons.Add(CreateButton("OK", DialogResult.OK));
                    break;
            }

            // 最初にフォーカスを置くボタン。指定が、ボタンの数より後ろなら、先頭
            int index = defbutton == MessageBoxDefaultButton.Button2 ? 1 : defbutton == MessageBoxDefaultButton.Button3 ? 2 : 0;
            ActiveControl = buttons[index < buttons.Count ? index : 0];
            return buttons;
        }

        private SMsgBoxButton CreateButton(string text, DialogResult result)
        {
            var button = new SMsgBoxButton { Text = text, Tag = result, BackColor = FooterBackColor };
            button.Click += OnButtonClick;
            Controls.Add(button);
            return button;
        }

        /// <summary>アイコン・文字・ボタンの位置を決めて、ダイアログの大きさを設定する。</summary>
        private void LayoutContent(List<SMsgBoxButton> buttons)
        {
            float scale = LayoutScale.Of(this);
            Func<float, int> px = value => (int)Math.Round(value * scale);

            bool hasIcon = GetIconKind() != MenuIconKind.None;
            int iconSize = hasIcon ? px(IconSize) : 0;
            int iconGap = hasIcon ? px(IconGap) : 0;
            int lineGap = px(LineGap);
            var limit = new Size(px(MaxTextWidth), int.MaxValue);

            // 文字の大きさ
            int lineHeight = TextRenderer.MeasureText("あg", Font, Size.Empty, TextFormatFlags.NoPadding).Height;
            int textWidth = 0;
            int textHeight = 0;
            foreach (Line line in _lines)
            {
                Size size = line.Text.Length > 0
                    ? TextRenderer.MeasureText(line.Text, line.Bold ? _boldFont : Font, limit, TextFlags)
                    : new Size(0, lineHeight);
                line.Bounds = new Rectangle(0, textHeight, size.Width, size.Height);
                textWidth = Math.Max(textWidth, size.Width);
                textHeight += size.Height + lineGap;
            }
            textHeight = Math.Max(0, textHeight - lineGap);

            // ボタンの大きさ
            int buttonHeight = px(ButtonHeight);
            int buttonGap = px(ButtonGap);
            int buttonsWidth = 0;
            foreach (SMsgBoxButton button in buttons)
            {
                int width = TextRenderer.MeasureText(button.Text.Replace("&", string.Empty), button.Font).Width + px(ButtonPaddingX) * 2;
                button.Size = new Size(Math.Max(px(ButtonMinWidth), width), buttonHeight);
                buttonsWidth += button.Width + buttonGap;
            }
            buttonsWidth -= buttonGap;

            // ダイアログの大きさ
            int paddingX = px(ContentPaddingX);
            int paddingY = px(ContentPaddingY);
            int contentHeight = Math.Max(iconSize, textHeight);
            int clientWidth = Math.Max(
                px(MinClientWidth),
                Math.Max(paddingX * 2 + iconSize + iconGap + textWidth, px(FooterPaddingX) * 2 + buttonsWidth));
            int footerHeight = px(FooterHeight);
            ClientSize = new Size(clientWidth, paddingY * 2 + contentHeight + footerHeight);

            // アイコンと文字のかたまりは、それぞれ上下の中央に置く
            _iconBounds = new Rectangle(paddingX, paddingY + (contentHeight - iconSize) / 2, iconSize, iconSize);
            int textLeft = paddingX + iconSize + iconGap;
            int textTop = paddingY + (contentHeight - textHeight) / 2;
            foreach (Line line in _lines)
            {
                line.Bounds = new Rectangle(textLeft, textTop + line.Bounds.Top, textWidth, line.Bounds.Height);
            }

            // ボタンは、下の帯の中に、右寄せで並べる
            _footerBounds = new Rectangle(0, ClientSize.Height - footerHeight, clientWidth, footerHeight);
            int x = clientWidth - px(FooterPaddingX) - buttonsWidth;
            int y = _footerBounds.Top + (footerHeight - buttonHeight) / 2;
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Location = new Point(x, y);
                buttons[i].TabIndex = i;
                x += buttons[i].Width + buttonGap;
            }
        }

        private MenuIconKind GetIconKind()
        {
            switch (_icon)
            {
                case MessageBoxIcon.Error:
                    return MenuIconKind.ErrorFilled;
                case MessageBoxIcon.Information:
                    return MenuIconKind.InfoFilled;
                case MessageBoxIcon.Question:
                    return MenuIconKind.QuestionFilled;
                case MessageBoxIcon.Warning:
                    return MenuIconKind.WarningFilled;
                default:
                    return MenuIconKind.None;
            }
        }

        private Color GetIconColor()
        {
            switch (_icon)
            {
                case MessageBoxIcon.Error:
                    return SControlStyle.ErrorIconColor;
                case MessageBoxIcon.Warning:
                    return SControlStyle.WarningIconColor;
                default:
                    return SControlStyle.InfoIconColor;
            }
        }

        #endregion

        #region 描画

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // 背景は、BackColor に関係なく、白と、ボタンを置く帯の色で描く
            // （呼び出す側が、親のフォームの色を BackColor に設定してくるため）
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            using (var footerBrush = new SolidBrush(FooterBackColor))
            using (var linePen = new Pen(FooterLineColor))
            {
                g.FillRectangle(footerBrush, _footerBounds);
                g.DrawLine(linePen, _footerBounds.Left, _footerBounds.Top, _footerBounds.Right, _footerBounds.Top);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            MenuIconKind kind = GetIconKind();
            if (kind != MenuIconKind.None)
            {
                MenuIconPainter.Draw(g, kind, _iconBounds, GetIconColor());
            }

            foreach (Line line in _lines)
            {
                if (line.Text.Length > 0)
                {
                    TextRenderer.DrawText(g, line.Text, line.Bold ? _boldFont : Font, line.Bounds, line.Color, TextFlags);
                }
            }
        }

        #endregion

        #region 表示と、閉じるとき

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // タイトルバーを、画面と同じ色にする（色を指定できない Windows では、標準の色のまま）
            App.TitleBarColor.ApplyStandard(this);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // 必ず最前面に表示する
            Activate();
            TopMost = true;

            // アイコンに合わせた音を鳴らす
            if (!PlaySound)
            {
                return;
            }
            switch (_icon)
            {
                case MessageBoxIcon.Error:
                    SystemSounds.Hand.Play();
                    break;
                case MessageBoxIcon.Information:
                    SystemSounds.Asterisk.Play();
                    break;
                case MessageBoxIcon.Question:
                    SystemSounds.Question.Play();
                    break;
                case MessageBoxIcon.Warning:
                    SystemSounds.Exclamation.Play();
                    break;
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            _result = (DialogResult)((Control)sender).Tag;
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                // 右上の × と同じ
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_result == DialogResult.None)
            {
                // ボタンを押さずに閉じた（右上の ×、Esc）
                _closeReason = eCloseReason.WindowCancel;

                switch (_buttons)
                {
                    case MessageBoxButtons.AbortRetryIgnore:
                        _result = DialogResult.Abort;
                        break;
                    case MessageBoxButtons.OKCancel:
                    case MessageBoxButtons.RetryCancel:
                    case MessageBoxButtons.YesNoCancel:
                        _result = DialogResult.Cancel;
                        break;
                    case MessageBoxButtons.YesNo:
                        _result = DialogResult.No;
                        break;
                }
            }

            base.OnFormClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _boldFont.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
