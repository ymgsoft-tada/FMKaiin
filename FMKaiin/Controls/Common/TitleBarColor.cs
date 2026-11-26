using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace App
{
    /// <summary>
    /// ウィンドウのタイトルバーの色を変えるための補助。
    /// タイトルバーは Windows が描画しており、色を指定できるのは Windows 11 以降に限られる。
    /// それより前の Windows では何も変わらず、標準の色のまま表示される（エラーにはならない）。
    /// </summary>
    internal static class TitleBarColor
    {
        // dwmapi.h の DWMWINDOWATTRIBUTE。どちらも Windows 11（ビルド 22000）で追加された
        private const int DWMWA_CAPTION_COLOR = 35;
        private const int DWMWA_TEXT_COLOR = 36;

        /// <summary>
        /// 画面で共通に使う、タイトルバーの背景色（濃い紺色）。メイン画面（FormMain2）のタイトルバーと同じ値。
        /// 色を変えるときは、FormMain2 の定義も合わせること。
        /// </summary>
        public static readonly Color StandardBackColor = Color.FromArgb(32, 64, 99);

        /// <summary>画面で共通に使う、タイトルバーの文字色。</summary>
        public static readonly Color StandardForeColor = Color.White;

        /// <summary>
        /// <paramref name="form"/> のタイトルバーを、画面で共通の色（<see cref="StandardBackColor"/>・<see cref="StandardForeColor"/>）にする。
        /// <see cref="Apply"/> と同じく、ハンドルが作られるたび（<see cref="Form.OnHandleCreated"/>）に呼ぶこと。
        /// </summary>
        /// <returns>色を設定できた場合は true。色の指定に対応していない Windows では false。</returns>
        public static bool ApplyStandard(Form form)
        {
            return Apply(form, StandardBackColor, StandardForeColor);
        }

        /// <summary>
        /// <paramref name="form"/> のタイトルバーの背景色と文字色を設定する。
        /// ウィンドウ ハンドルに対する設定なので、ハンドルが作られるたび（<see cref="Form.OnHandleCreated"/>）に呼ぶこと。
        /// </summary>
        /// <returns>色を設定できた場合は true。色の指定に対応していない Windows では false。</returns>
        public static bool Apply(Form form, Color background, Color text)
        {
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form));
            }
            if (!form.IsHandleCreated)
            {
                return false;
            }

            bool backgroundApplied = SetColorAttribute(form.Handle, DWMWA_CAPTION_COLOR, background);
            bool textApplied = SetColorAttribute(form.Handle, DWMWA_TEXT_COLOR, text);
            return backgroundApplied && textApplied;
        }

        private static bool SetColorAttribute(IntPtr handle, int attribute, Color color)
        {
            // COLORREF は 0x00BBGGRR の並び
            int colorRef = color.R | (color.G << 8) | (color.B << 16);
            try
            {
                return DwmSetWindowAttribute(handle, attribute, ref colorRef, sizeof(int)) == 0;
            }
            catch (DllNotFoundException)
            {
                return false;
            }
            catch (EntryPointNotFoundException)
            {
                return false;
            }
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int value, int valueSize);
    }
}
