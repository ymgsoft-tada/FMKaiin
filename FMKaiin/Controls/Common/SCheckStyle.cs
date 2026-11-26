using System;
using System.Drawing;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// チェック ボックス（<see cref="SCheckBox"/>）とラジオ ボタン（<see cref="SRadioButton"/>）に共通の、既定の見た目と、配置・描画の処理。
    /// どちらも「印（四角・丸）と、その横の文字」という同じ形なので、印の形を描く部分だけを、それぞれのコントロールが持つ。
    /// </summary>
    /// <remarks>
    /// 寸法は 96 DPI 基準のピクセル。描くときに、表示倍率（<see cref="LayoutScale"/>）を掛ける。
    /// </remarks>
    internal static class SCheckStyle
    {
        /// <summary>印（四角・丸）の大きさの既定値。標準のチェック ボックス・ラジオ ボタンは 13。</summary>
        public const int DefaultMarkSize = 17;

        /// <summary>印の大きさの下限。</summary>
        public const int MinimumMarkSize = 7;

        /// <summary>印と文字の間のすき間。</summary>
        private const float TextGap = 5f;

        /// <summary>フォーカスがあるときの太い枠線が欠けないように、印の外側にあけておく幅。</summary>
        private const float MarkMargin = 1f;

        /// <summary>マウスを載せたとき・押したときに、色へ混ぜる黒の割合（<see cref="SButton"/> と同じ）。</summary>
        private const float HoverShade = 0.05f;
        private const float PressedShade = 0.14f;

        // 既定の色。オフの印の枠線は、ボタン（SButton）の枠線と同じ。オンの印は、主な操作のボタンと同じ系統の青
        public static readonly Color MarkBorderColor = Color.FromArgb(168, 184, 206);
        public static readonly Color MarkBackColor = Color.White;
        public static readonly Color CheckedColor = Color.FromArgb(34, 103, 182);
        public static readonly Color CheckMarkColor = Color.White;

        // 無効のときの色
        private static readonly Color DisabledBorderColor = Color.FromArgb(208, 213, 220);
        private static readonly Color DisabledCheckedColor = Color.FromArgb(178, 186, 198);
        private static readonly Color DisabledTextColor = Color.FromArgb(160, 166, 175);

        /// <summary>印の大きさ（表示倍率を掛けたピクセル数）。</summary>
        public static int ScaleMarkSize(int markSize, float scale)
        {
            return Math.Max(MinimumMarkSize, (int)Math.Round(markSize * scale));
        }

        /// <summary>
        /// 内容がちょうど収まる大きさ（自動で大きさを決める設定のときに使う）。印・すき間・文字を横に並べた大きさ。
        /// </summary>
        public static Size GetPreferredSize(Control control, int markSize)
        {
            float scale = LayoutScale.Of(control);
            int mark = ScaleMarkSize(markSize, scale) + ScaleMargin(scale) * 2;
            var size = new Size(mark, mark);

            if (!string.IsNullOrEmpty(control.Text))
            {
                Size text = TextRenderer.MeasureText(control.Text, control.Font, Size.Empty, TextFormatFlags.SingleLine);
                size.Width += (int)Math.Round(TextGap * scale) + text.Width;
                size.Height = Math.Max(size.Height, text.Height);
            }

            size.Width += control.Padding.Horizontal;
            size.Height += control.Padding.Vertical;
            return size;
        }

        /// <summary>印と文字を置く範囲（コントロールの全体から、余白を除いたもの）。</summary>
        public static Rectangle GetContentBounds(Control control)
        {
            Padding padding = control.Padding;
            return Rectangle.FromLTRB(padding.Left, padding.Top, control.Width - padding.Right, control.Height - padding.Bottom);
        }

        /// <summary>
        /// 印の位置。<paramref name="align"/>（CheckAlign）に従って、<paramref name="content"/> の中に置く。
        /// </summary>
        public static Rectangle GetMarkBounds(Rectangle content, ContentAlignment align, int markSize, float scale)
        {
            int size = ScaleMarkSize(markSize, scale);
            int margin = ScaleMargin(scale);

            int x = IsLeft(align) ? content.Left + margin
                : IsRight(align) ? content.Right - margin - size
                : content.Left + (content.Width - size) / 2;
            int y = IsTop(align) ? content.Top + margin
                : IsBottom(align) ? content.Bottom - margin - size
                : content.Top + (content.Height - size) / 2;

            return new Rectangle(x, y, size, size);
        }

        /// <summary>
        /// 背景を描く。背景の色が透明・半透明なら、先に、親コントロールと同じ見た目を描く（半透明のパネルの上でも、浮かないように）。
        /// </summary>
        /// <param name="invokePaintBackground">コントロールの InvokePaintBackground（保護されたメソッドなので、呼び出し側から渡す）。</param>
        /// <param name="invokePaint">コントロールの InvokePaint（同じ理由で、呼び出し側から渡す）。</param>
        public static void PaintBackground(
            Control control,
            Graphics g,
            Action<Control, PaintEventArgs> invokePaintBackground,
            Action<Control, PaintEventArgs> invokePaint)
        {
            Color back = control.BackColor;
            if (back.A < 255)
            {
                PaintHelper.PaintParentAppearance(control, g, invokePaintBackground, invokePaint);
            }
            if (back.A > 0)
            {
                using (var brush = new SolidBrush(back))
                {
                    g.FillRectangle(brush, control.ClientRectangle);
                }
            }
        }

        /// <summary>
        /// 印の、中の色・枠線の色・枠線の太さを決める。
        /// </summary>
        /// <param name="filled">印を塗る状態（オン。チェック ボックスの「どちらでもない」も含む）か。</param>
        /// <param name="pressed">マウスで押している最中か。</param>
        /// <param name="hover">マウスが載っているか。</param>
        /// <remarks>
        /// フォーカスは、入力欄と同じく、枠線の色と太さで示す。オンのときの枠線は、ふだんは中と同じ色（枠線が無いように見える）。
        /// </remarks>
        public static void GetMarkColors(
            Control control,
            bool filled,
            bool pressed,
            bool hover,
            Color checkedColor,
            Color backColor,
            Color borderColor,
            Color focusBorderColor,
            float scale,
            out Color fill,
            out Color border,
            out int borderWidth)
        {
            bool enabled = control.Enabled;
            bool focused = control.Focused && enabled;
            float shade = !enabled ? 0f : pressed ? PressedShade : hover ? HoverShade : 0f;

            fill = filled
                ? (enabled ? PaintHelper.Blend(checkedColor, Color.Black, shade) : DisabledCheckedColor)
                : (enabled ? PaintHelper.Blend(backColor, Color.Black, shade) : SControlStyle.DisabledBackColor);
            border = !enabled ? (filled ? DisabledCheckedColor : DisabledBorderColor)
                : focused ? focusBorderColor
                : filled ? fill
                : borderColor;
            borderWidth = SControlStyle.GetBorderWidth(scale, focused);
        }

        /// <summary>
        /// 文字を描く。印が左か右にあれば、その反対側に、すき間をあけて描く。印が中央にあれば、全体に描く（印と重なる）。
        /// 文字は 1 行で、収まらない分は、末尾を「…」にする。
        /// </summary>
        /// <param name="checkAlign">印の位置（CheckAlign）。</param>
        /// <param name="textAlign">文字の位置（TextAlign）。</param>
        /// <param name="showKeyboardCues">アクセス キー（&amp; の次の文字）の下線を表示するか（コントロールの ShowKeyboardCues）。</param>
        public static void DrawText(
            Graphics g,
            Control control,
            Rectangle content,
            Rectangle mark,
            ContentAlignment checkAlign,
            ContentAlignment textAlign,
            bool showKeyboardCues,
            float scale)
        {
            string text = control.Text;
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            int gap = (int)Math.Round(TextGap * scale);
            Rectangle area = content;
            if (IsLeft(checkAlign))
            {
                area = Rectangle.FromLTRB(Math.Min(content.Right, mark.Right + gap), content.Top, content.Right, content.Bottom);
            }
            else if (IsRight(checkAlign))
            {
                area = Rectangle.FromLTRB(content.Left, content.Top, Math.Max(content.Left, mark.Left - gap), content.Bottom);
            }
            if (area.Width <= 0 || area.Height <= 0)
            {
                return;
            }

            // & は、下線にする。下線は、Alt を押すなどしてキーボードの操作を始めるまでは表示しない（Windows の設定に従う）
            TextFormatFlags flags = TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis;
            flags |= IsLeft(textAlign) ? TextFormatFlags.Left : IsRight(textAlign) ? TextFormatFlags.Right : TextFormatFlags.HorizontalCenter;
            flags |= IsTop(textAlign) ? TextFormatFlags.Top : IsBottom(textAlign) ? TextFormatFlags.Bottom : TextFormatFlags.VerticalCenter;
            if (!showKeyboardCues)
            {
                flags |= TextFormatFlags.HidePrefix;
            }

            Color color = control.Enabled ? control.ForeColor : DisabledTextColor;
            TextRenderer.DrawText(g, text, control.Font, area, color, flags);
        }

        private static int ScaleMargin(float scale)
        {
            return Math.Max(1, (int)Math.Round(MarkMargin * scale));
        }

        private static bool IsLeft(ContentAlignment align)
        {
            return align == ContentAlignment.TopLeft || align == ContentAlignment.MiddleLeft || align == ContentAlignment.BottomLeft;
        }

        private static bool IsRight(ContentAlignment align)
        {
            return align == ContentAlignment.TopRight || align == ContentAlignment.MiddleRight || align == ContentAlignment.BottomRight;
        }

        private static bool IsTop(ContentAlignment align)
        {
            return align == ContentAlignment.TopLeft || align == ContentAlignment.TopCenter || align == ContentAlignment.TopRight;
        }

        private static bool IsBottom(ContentAlignment align)
        {
            return align == ContentAlignment.BottomLeft || align == ContentAlignment.BottomCenter || align == ContentAlignment.BottomRight;
        }
    }
}
