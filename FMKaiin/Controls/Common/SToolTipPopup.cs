using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// <see cref="SToolTip"/> が表示する、ツールチップのウィンドウ。アイコン・タイトル・本文を描く。
    /// 表示する内容はコンストラクターで受け取り、表示のたびに作り直す。
    /// </summary>
    /// <remarks>
    /// 角の丸みと枠線は、Windows 11 では Windows に描かせている（ウィンドウの角を、なめらかに丸くできるのは Windows だけなので）。
    /// それより前の Windows では、角は四角のままで、枠線は自前で描く。
    /// </remarks>
    internal sealed class SToolTipPopup : SPopupForm
    {
        // ここの大きさは、すべて 96 DPI 基準のピクセル
        private const float HorizontalPadding = 12f;
        private const float VerticalPadding = 9f;
        private const float IconSize = 20f;
        private const float IconGap = 9f;
        private const float TitleGap = 3f;

        /// <summary>タイトル・本文の幅の上限。これより長い文は折り返す。</summary>
        private const float MaxTextWidth = 420f;

        private const TextFormatFlags TextFlags =
            TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.WordBreak | TextFormatFlags.Left | TextFormatFlags.Top;

        private const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        private const int DWMWA_BORDER_COLOR = 34;
        private const int DWMWCP_ROUNDSMALL = 3;

        private readonly string _caption;
        private readonly string _text;
        private readonly ToolTipIcon _icon;
        private readonly Font _titleFont;

        private Rectangle _iconBounds;
        private Rectangle _titleBounds;
        private Rectangle _textBounds;

        // 枠線を Windows が描いているか（描いていなければ、自前で描く）
        private bool _borderDrawnBySystem;

        /// <param name="owner">持ち主のコントロール（ツールチップが指す入力欄。無ければフォーム）。</param>
        public SToolTipPopup(Control owner, string caption, string text, ToolTipIcon icon)
            : base(owner)
        {
            _caption = caption ?? string.Empty;
            _text = text ?? string.Empty;
            _icon = icon;

            BackColor = Color.White;
            Font = SControlStyle.Font;
            _titleFont = new Font(SControlStyle.Font, FontStyle.Bold);
        }

        /// <summary>
        /// 左上が <paramref name="location"/>（画面の座標）に来るように表示する。フォーカスは移さない。
        /// 画面の右にはみ出すなら左へ寄せ、下にはみ出すなら、下端が <paramref name="flipBottom"/> に来るように上へ置く。
        /// </summary>
        /// <param name="flipBottom">上へ置くときの、下端の位置（画面の座標）。指している入力欄の上端などを渡す。</param>
        public void ShowAt(Point location, int flipBottom)
        {
            Size size = LayoutContent();
            Rectangle screen = Screen.FromPoint(location).WorkingArea;

            int x = Math.Max(screen.Left, Math.Min(location.X, screen.Right - size.Width));
            int y = location.Y;
            if (y + size.Height > screen.Bottom && flipBottom - size.Height >= screen.Top)
            {
                y = flipBottom - size.Height;
            }

            Bounds = new Rectangle(x, y, size.Width, size.Height);
            ShowPopup();
        }

        /// <summary>アイコン・タイトル・本文の位置を決めて、ウィンドウの大きさを返す。</summary>
        private Size LayoutContent()
        {
            float scale = LayoutScale.Of(OwnerControl);
            int paddingX = (int)Math.Round(HorizontalPadding * scale);
            int paddingY = (int)Math.Round(VerticalPadding * scale);
            int iconSize = _icon == ToolTipIcon.None ? 0 : (int)Math.Round(IconSize * scale);
            int iconGap = _icon == ToolTipIcon.None ? 0 : (int)Math.Round(IconGap * scale);
            var limit = new Size((int)Math.Round(MaxTextWidth * scale), int.MaxValue);

            Size title = _caption.Length > 0 ? TextRenderer.MeasureText(_caption, _titleFont, limit, TextFlags) : Size.Empty;
            Size body = _text.Length > 0 ? TextRenderer.MeasureText(_text, Font, limit, TextFlags) : Size.Empty;
            int titleGap = title.Height > 0 && body.Height > 0 ? (int)Math.Round(TitleGap * scale) : 0;

            int textWidth = Math.Max(title.Width, body.Width);
            int textHeight = title.Height + titleGap + body.Height;
            int contentHeight = Math.Max(iconSize, textHeight);

            // アイコンと文字のかたまりは、それぞれ上下の中央に置く
            int textLeft = paddingX + iconSize + iconGap;
            int textTop = paddingY + (contentHeight - textHeight) / 2;
            _iconBounds = new Rectangle(paddingX, paddingY + (contentHeight - iconSize) / 2, iconSize, iconSize);
            _titleBounds = new Rectangle(textLeft, textTop, textWidth, title.Height);
            _textBounds = new Rectangle(textLeft, textTop + title.Height + titleGap, textWidth, body.Height);

            return new Size(textLeft + textWidth + paddingX, paddingY * 2 + contentHeight);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Windows 11 では、角を小さく丸めて、枠線を指定の色で描かせる。対応していない Windows では失敗するので、そのときは自前の枠線にする
            _borderDrawnBySystem = false;
            int corner = DWMWCP_ROUNDSMALL;
            if (DwmSetWindowAttribute(Handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref corner, sizeof(int)) == 0)
            {
                int borderColor = ColorTranslator.ToWin32(SControlStyle.BorderColor);
                _borderDrawnBySystem = DwmSetWindowAttribute(Handle, DWMWA_BORDER_COLOR, ref borderColor, sizeof(int)) == 0;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackColor);

            if (!_borderDrawnBySystem)
            {
                using (var borderPen = new Pen(SControlStyle.BorderColor))
                {
                    g.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
                }
            }

            switch (_icon)
            {
                case ToolTipIcon.Info:
                    MenuIconPainter.Draw(g, MenuIconKind.InfoFilled, _iconBounds, SControlStyle.InfoIconColor);
                    break;
                case ToolTipIcon.Warning:
                    MenuIconPainter.Draw(g, MenuIconKind.WarningFilled, _iconBounds, SControlStyle.WarningIconColor);
                    break;
                case ToolTipIcon.Error:
                    MenuIconPainter.Draw(g, MenuIconKind.ErrorFilled, _iconBounds, SControlStyle.ErrorIconColor);
                    break;
            }

            if (_caption.Length > 0)
            {
                TextRenderer.DrawText(g, _caption, _titleFont, _titleBounds, SControlStyle.HeadingTextColor, TextFlags);
            }
            if (_text.Length > 0)
            {
                TextRenderer.DrawText(g, _text, Font, _textBounds, SControlStyle.TextColor, TextFlags);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _titleFont.Dispose();
            }
            base.Dispose(disposing);
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int value, int size);
    }
}
