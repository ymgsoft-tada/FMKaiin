using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 角丸の枠を持つテキスト ボックス。GcTextBoxEx（InputMan の GcTextBox）の代わりに使う。
    /// 標準の <see cref="TextBox"/> を継承しているので、Text・SelectAll・TextChanged などはそのまま使える。
    /// 枠・全選択などの共通の機能（<see cref="SEditBase"/>）に加えて、次の機能を持つ。
    /// <list type="bullet">
    /// <item>入力できる文字の制限と自動変換（<see cref="Format"/>・<see cref="AllowSpace"/>・<see cref="AutoConvert"/>）</item>
    /// <item>最大文字数の数え方の指定（<see cref="MaxLengthUnit"/>）、貼り付け時の改行の扱い（<see cref="AcceptsCrLf"/>）</item>
    /// <item>IME で入力した文字の読み（ふりがな）の取得（<see cref="ImeReading"/>）</item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// 入力の制限が掛かるのは、利用者の操作（キー入力・IME の確定・貼り付け）だけ。
    /// プログラムから Text に設定した文字列は、そのまま表示する（DB の値を黙って書き換えないため）。
    /// </remarks>
    public class STextBox : SEditBase
    {
        private const int WM_IME_COMPOSITION = 0x010F;

        private const int GCS_RESULTREADSTR = 0x0200;
        private const int GCS_RESULTSTR = 0x0800;

        private string _imeReading = string.Empty;

        /// <summary>
        /// IME で文字を確定し、その読みを取得したときに発生する。
        /// </summary>
        [Category("Action")]
        [Description("IME で文字を確定し、その読みを取得したときに発生する。")]
        public event EventHandler<SImeReadingEventArgs> ImeReadingCommitted;

        #region 入力のプロパティ

        /// <summary>
        /// 入力できる文字の種類。書き方は <see cref="SInputFormat"/> を参照（例: 9 は数字だけ、H は半角文字だけ、^@ は半角記号以外）。
        /// 空文字列は制限なし。
        /// </summary>
        [Category("Behavior")]
        [DefaultValue("")]
        [Description("入力できる文字の種類（例: 9 は数字だけ、H は半角文字だけ、^@ は半角記号以外）。空文字列は制限なし。")]
        public string Format
        {
            get { return InputFilter.Format.Pattern; }
            set { InputFilter.Format = new SInputFormat(value); }
        }

        /// <summary>入力できる空白の種類。</summary>
        [Category("Behavior")]
        [DefaultValue(SAllowSpace.Both)]
        [Description("入力できる空白の種類。")]
        public SAllowSpace AllowSpace
        {
            get { return InputFilter.AllowSpace; }
            set { InputFilter.AllowSpace = value; }
        }

        /// <summary>
        /// 入力できない文字を、入力できる文字へ自動で変換するか（全角と半角、大文字と小文字、ひらがなとカタカナ）。
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("入力できない文字を、入力できる文字へ自動で変換するか（全角と半角、大文字と小文字、ひらがなとカタカナ）。")]
        public bool AutoConvert
        {
            get { return InputFilter.AutoConvert; }
            set { InputFilter.AutoConvert = value; }
        }

        /// <summary>貼り付ける文字列に含まれる改行の扱い。</summary>
        [Category("Behavior")]
        [DefaultValue(SCrLfMode.NoControl)]
        [Description("貼り付ける文字列に含まれる改行の扱い。")]
        public SCrLfMode AcceptsCrLf
        {
            get { return InputFilter.AcceptsCrLf; }
            set { InputFilter.AcceptsCrLf = value; }
        }

        /// <summary>最大文字数（<see cref="SEditBase.MaxLength"/>）の数え方。</summary>
        [Category("Behavior")]
        [DefaultValue(SLengthUnit.Char)]
        [Description("最大文字数の数え方。")]
        public SLengthUnit MaxLengthUnit
        {
            get { return InputFilter.MaxLengthUnit; }
            set
            {
                InputFilter.MaxLengthUnit = value;
                ApplyNativeMaxLength();
            }
        }

        #endregion

        #region IME の読み

        /// <summary>
        /// IME で確定した文字の読み（ふりがな）をためたもの。文字を確定するたびに後ろへ追加され、
        /// 文字列（Text）が空になると空に戻る。読みの文字の種類（半角カタカナなど）は IME による。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ImeReading
        {
            get { return _imeReading; }
            set { _imeReading = value ?? string.Empty; }
        }

        /// <summary>
        /// <see cref="ImeReading"/> を返し、ためていた内容を空にする。
        /// </summary>
        public string TakeImeReading()
        {
            string reading = _imeReading;
            _imeReading = string.Empty;
            return reading;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (TextLength == 0)
            {
                _imeReading = string.Empty;
            }
            base.OnTextChanged(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_IME_COMPOSITION && (m.LParam.ToInt64() & GCS_RESULTREADSTR) != 0)
            {
                // IME が文字を確定した。標準の処理が文字を受け取る前に、読みを取得しておく
                CaptureImeReading();
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// IME が確定した文字列の読みを取得して、<see cref="ImeReading"/> の後ろへ追加する。
        /// </summary>
        private void CaptureImeReading()
        {
            IntPtr context = ImmGetContext(Handle);
            if (context == IntPtr.Zero)
            {
                return;
            }

            string reading;
            string text;
            try
            {
                reading = GetCompositionString(context, GCS_RESULTREADSTR);
                text = GetCompositionString(context, GCS_RESULTSTR);
            }
            finally
            {
                ImmReleaseContext(Handle, context);
            }

            _imeReading += reading;

            EventHandler<SImeReadingEventArgs> handler = ImeReadingCommitted;
            if (handler != null)
            {
                handler(this, new SImeReadingEventArgs(reading, text));
            }
        }

        private static string GetCompositionString(IntPtr context, int kind)
        {
            // バッファーを渡さずに呼ぶと、必要なバイト数が返る。エラーのときは負の値
            int byteCount = ImmGetCompositionString(context, kind, null, 0);
            if (byteCount <= 0)
            {
                return string.Empty;
            }

            var buffer = new byte[byteCount];
            byteCount = ImmGetCompositionString(context, kind, buffer, buffer.Length);
            return byteCount <= 0 ? string.Empty : Encoding.Unicode.GetString(buffer, 0, byteCount);
        }

        #endregion

        #region Windows API

        [DllImport("imm32.dll")]
        private static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("imm32.dll")]
        private static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

        [DllImport("imm32.dll", EntryPoint = "ImmGetCompositionStringW")]
        private static extern int ImmGetCompositionString(IntPtr hIMC, int index, byte[] buffer, int bufferLength);

        #endregion
    }

    /// <summary>
    /// <see cref="STextBox.ImeReadingCommitted"/> イベントのデータ。
    /// </summary>
    public class SImeReadingEventArgs : EventArgs
    {
        private readonly string _reading;
        private readonly string _text;

        public SImeReadingEventArgs(string reading, string text)
        {
            _reading = reading ?? string.Empty;
            _text = text ?? string.Empty;
        }

        /// <summary>確定した文字列の読み。</summary>
        public string Reading
        {
            get { return _reading; }
        }

        /// <summary>確定した文字列。</summary>
        public string Text
        {
            get { return _text; }
        }
    }
}
