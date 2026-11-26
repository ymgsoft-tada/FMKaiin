using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// S 系の入力欄（<see cref="STextBox"/>・<see cref="SNumberBox"/>・<see cref="SDateBox"/>・<see cref="STableComboBox"/>）に共通の土台。
    /// 標準の <see cref="TextBox"/> を継承していて、次のものを受け持つ。
    /// <list type="bullet">
    /// <item>角丸の枠と余白の描画、フォーカスがあるときの枠線の色（<see cref="FocusBorderColor"/>）</item>
    /// <item>文字の縦位置の指定（<see cref="ContentAlignment"/>）、無効のときの色（<see cref="DisabledBackColor"/>・<see cref="DisabledForeColor"/>）</item>
    /// <item>フォーカスを受けたときの全選択（<see cref="HighlightText"/>）</item>
    /// <item>枠の内側の右端に置くボタン（サイド ボタン。一覧を開く矢印など）</item>
    /// <item>利用者が入力した文字の選別（<see cref="InputFilter"/> と <see cref="ProcessInput"/>）</item>
    /// </list>
    /// どの文字を入力できるかは、継承したクラスが決める（<see cref="STextBox"/> は Format などのプロパティで指定でき、
    /// 数値・日付の入力欄は自分の規則に固定している）。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 枠と余白は、ウィンドウの非クライアント領域（標準の枠線が描かれる場所）に自前で描いている。
    /// 文字を入力する部分（クライアント領域）は枠の内側に縮めてあり、標準のテキスト ボックスがそのまま描く。
    /// 高さは自由に指定でき、1 行入力のときは <see cref="ContentAlignment"/> に従って文字の縦位置を決める。
    /// </para>
    /// <para>
    /// 入力の選別が掛かるのは、利用者の操作（キー入力・IME の確定・貼り付け）だけ。
    /// プログラムから Text に設定した文字列は、選別しない。
    /// </para>
    /// </remarks>
    public abstract class SEditBase : TextBox
    {
        #region Windows の定数

        private const int WM_SETFOCUS = 0x0007;
        private const int WM_KILLFOCUS = 0x0008;
        private const int WM_SETTEXT = 0x000C;
        private const int WM_PAINT = 0x000F;
        private const int WM_ERASEBKGND = 0x0014;
        private const int WM_SETCURSOR = 0x0020;
        private const int WM_SETFONT = 0x0030;
        private const int WM_NCCALCSIZE = 0x0083;
        private const int WM_NCHITTEST = 0x0084;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_CHAR = 0x0102;
        private const int WM_IME_STARTCOMPOSITION = 0x010D;
        private const int WM_IME_ENDCOMPOSITION = 0x010E;
        private const int WM_IME_COMPOSITION = 0x010F;
        private const int WM_TIMER = 0x0113;
        private const int WM_HSCROLL = 0x0114;
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_LBUTTONDBLCLK = 0x0203;
        private const int WM_CUT = 0x0300;
        private const int WM_PASTE = 0x0302;
        private const int WM_CLEAR = 0x0303;
        private const int WM_UNDO = 0x0304;

        private const int EM_SETSEL = 0x00B1;
        private const int EM_SCROLLCARET = 0x00B7;
        private const int EM_REPLACESEL = 0x00C2;
        private const int EM_UNDO = 0x00C7;
        private const int WM_PRINT = 0x0317;
        private const int WM_PRINTCLIENT = 0x0318;

        private const int EM_SETMARGINS = 0x00D3;
        private const int EC_LEFTMARGIN = 0x0001;
        private const int EC_RIGHTMARGIN = 0x0002;

        private const int HTCLIENT = 1;
        private const int HTHSCROLL = 6;
        private const int HTVSCROLL = 7;

        private const int PRF_NONCLIENT = 0x0002;

        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int SWP_FRAMECHANGED = 0x0020;

        private const int RDW_INVALIDATE = 0x0001;
        private const int RDW_FRAME = 0x0400;

        #endregion

        /// <summary>枠線から文字までの上下の余白の最小値。96 DPI 基準のピクセル。</summary>
        private const float VerticalPadding = 2f;

        /// <summary>無効のときに自前で文字を描くときの、共通の指定。標準のテキスト ボックスと同じ位置に描くため、余白を付けない。</summary>
        private const TextFormatFlags DisabledTextFlags = TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix;

        /// <summary>Ctrl+BackSpace を押したときに届く文字（DEL）。</summary>
        private const char DeleteCharacter = '\u007F';

        private readonly SInputFilter _filter = new SInputFilter();

        // 収まらない文字を縮めて表示するときのフォント（ShrinkTextToFit が true の部品だけが使う）
        private readonly SFittedFont _fittedFont = new SFittedFont();

        // プレースホルダーを描くフォント（入力欄のフォントより小さい）
        private readonly SPlaceholderFont _placeholderFont = new SPlaceholderFont();

        private BorderStyle _borderStyle = BorderStyle.FixedSingle;
        private Color _borderColor = SControlStyle.BorderColor;
        private Color _focusBorderColor = SControlStyle.FocusBorderColor;
        private Color _focusBackColor = SControlStyle.FocusBackColor;
        private Color _textSelectionColor = SControlStyle.SelectionBackColor;
        private Color _disabledBackColor = SControlStyle.DisabledBackColor;
        private Color _disabledForeColor = Color.Empty;
        private int _cornerRadius = SControlStyle.CornerRadius;
        private VerticalPosition _verticalPosition = VerticalPosition.Middle;

        private bool _highlightText;
        private bool _selectAllOnMouseUp;

        // いま、フォーカスがあるときの背景色（FocusBackColor）で表示しているか。UpdateFocusFill だけが書き換える
        private bool _focusFillShown;

        // 選択した文字を自前の色で描き直している最中は true（描き直しの中で送るメッセージで、もう一度描き直さないため）
        private bool _paintingSelection;

        private string _placeholderText = string.Empty;
        private Color _placeholderColor = SControlStyle.PlaceholderColor;

        // いま、プレースホルダーを画面に描いてあるか。RefreshPlaceholder だけが書き換える
        private bool _placeholderShown;

        // IME で変換している最中か（変換中の文字と重ならないよう、プレースホルダーを消しておく）
        private bool _imeComposing;

        // 1 行分の文字の高さ。0 は未計測（フォントが変わったら 0 に戻す）
        private int _lineHeight;

        private enum VerticalPosition
        {
            Top,
            Middle,
            Bottom,
        }

        protected SEditBase()
        {
            // 枠は自前で描くので、標準の枠線は使わない
            base.BorderStyle = BorderStyle.None;

            // 標準のテキスト ボックスは高さがフォントで決まる（AutoSize）。高さを自由に指定できるようにする
            base.AutoSize = false;

            base.Font = SControlStyle.Font;
            base.ForeColor = SControlStyle.TextColor;

            // 最大文字数の既定値は、標準のテキスト ボックスと同じにする
            _filter.MaxLength = base.MaxLength;
        }

        #region イベント

        /// <summary>
        /// 入力しようとした文字が、入力規則に合わず 1 文字も入力されなかったときに発生する。
        /// </summary>
        [Category("Action")]
        [Description("入力しようとした文字が、入力規則に合わず 1 文字も入力されなかったときに発生する。")]
        public event EventHandler InvalidInput;

        #endregion

        #region 見た目のプロパティ

        /// <summary>
        /// 枠線の種類。<see cref="BorderStyle.None"/> にすると、枠線と角丸をなくす（グリッドのセルに重ねる入力欄などに使う）。
        /// それ以外は角丸の枠線を描く（Fixed3D は FixedSingle と同じ）。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(BorderStyle.FixedSingle)]
        [Description("枠線の種類。None にすると枠線と角丸をなくす。それ以外は角丸の枠線を描く。")]
        public new BorderStyle BorderStyle
        {
            get { return _borderStyle; }
            set
            {
                if (!Enum.IsDefined(typeof(BorderStyle), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(BorderStyle));
                }
                if (_borderStyle != value)
                {
                    _borderStyle = value;
                    UpdateFrameLayout();
                }
            }
        }

        /// <summary>枠線の色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "186, 197, 211")]
        [Description("枠線の色。")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; RedrawFrame(); }
        }

        /// <summary>フォーカスがあるときの枠線の色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "64, 144, 232")]
        [Description("フォーカスがあるときの枠線の色。")]
        public Color FocusBorderColor
        {
            get { return _focusBorderColor; }
            set { _focusBorderColor = value; RedrawFrame(); }
        }

        /// <summary>
        /// フォーカスがあるときの背景色。空（<see cref="Color.Empty"/>）にすると、フォーカスがあっても背景色を変えない。
        /// 読み取り専用（ReadOnly）のときは変えない。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Lavender")]
        [Description("フォーカスがあるときの背景色。空にすると、フォーカスがあっても背景色を変えない。")]
        public Color FocusBackColor
        {
            get { return _focusBackColor; }
            set
            {
                if (_focusBackColor == value)
                {
                    return;
                }
                _focusBackColor = value;
                UpdateFocusFill(true);
            }
        }

        /// <summary>
        /// 背景色。フォーカスがあるときの背景色（<see cref="FocusBackColor"/>）で表示している間は、その色を返す。
        /// 設定した色は、フォーカスが外れると元どおり使われる。
        /// フォーカスの有無に関係なく、設定してある色を知りたいときは <see cref="NormalBackColor"/> を使うこと。
        /// </summary>
        /// <remarks>
        /// 標準のテキスト ボックスは、入力部分の背景を、このプロパティの値で塗る（Windows フォームの中の処理で、差し替える手段が無い）。
        /// そのため、フォーカスがある間だけ、このプロパティが返す値を切り替えている。
        /// </remarks>
        public override Color BackColor
        {
            get { return _focusFillShown ? _focusBackColor : base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// BackColor に設定してある色（フォーカスがあるときの背景色に切り替わっていても、元の色を返す）。
        /// 背景色を覚えておいて後で戻す処理や、一覧など別の部分の色に使うときは、BackColor ではなくこちらを読むこと。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color NormalBackColor
        {
            get { return base.BackColor; }
        }


        /// <summary>
        /// 選択した文字の背景色。文字は、この色の上で読める色（濃い色の上では白、明るい色の上では ForeColor）で描く。
        /// 空（<see cref="Color.Empty"/>）にすると、Windows の標準の色（濃い青に白い文字）になる。
        /// 1 行の入力欄でだけ効く（複数行では、Windows の標準の色になる）。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "RoyalBlue")]
        [Description("選択した文字の背景色。空にすると、Windows の標準の色になる。1 行の入力欄でだけ効く。")]
        public Color TextSelectionColor
        {
            get { return _textSelectionColor; }
            set
            {
                _textSelectionColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 入力欄が空のときに、薄い色で表示する案内の文字（プレースホルダー）。「入力必須」や入力例などに使う。
        /// 文字を入力すると消え、空に戻すとまた表示する。フォーカスがある間も表示する
        /// （入力済みの内容を消したその場で、案内が見えるようにするため）。
        /// 無効（Enabled = false）・読み取り専用（ReadOnly）のときは表示しない。Text の値には影響しない。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("入力欄が空のときに、薄い色で表示する案内の文字。")]
        public string PlaceholderText
        {
            get { return _placeholderText; }
            set
            {
                value = value ?? string.Empty;
                if (_placeholderText == value)
                {
                    return;
                }
                _placeholderText = value;
                Invalidate();
            }
        }

        /// <summary>プレースホルダー（<see cref="PlaceholderText"/>）の文字の色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "200, 210, 226")]
        [Description("プレースホルダー（PlaceholderText）の文字の色。")]
        public Color PlaceholderColor
        {
            get { return _placeholderColor; }
            set
            {
                _placeholderColor = value;
                Invalidate();
            }
        }

        /// <summary>無効（Enabled = false）のときの背景色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "236, 238, 241")]
        [Description("無効（Enabled = false）のときの背景色。")]
        public Color DisabledBackColor
        {
            get { return _disabledBackColor; }
            set { _disabledBackColor = value; RedrawDisabledSurface(); }
        }

        /// <summary>
        /// 無効（Enabled = false）のときの文字色。空（<see cref="Color.Empty"/>）の場合は ForeColor と同じ色
        /// （標準のテキスト ボックスのような薄い灰色にはならず、表示欄として読みやすいままになる）。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("無効（Enabled = false）のときの文字色。空の場合は ForeColor と同じ色。")]
        public Color DisabledForeColor
        {
            get { return _disabledForeColor; }
            set { _disabledForeColor = value; RedrawDisabledSurface(); }
        }

        /// <summary>枠の角の半径。96 DPI 基準のピクセルで指定する。</summary>
        [Category("Appearance")]
        [DefaultValue(4)]
        [Description("枠の角の半径。96 DPI 基準のピクセルで指定する。")]
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = Math.Max(0, value); RedrawFrame(); }
        }

        /// <summary>
        /// 文字の位置。左右は <see cref="TextBox.TextAlign"/> と連動する。上下は 1 行入力のときだけ効く。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(ContentAlignment.MiddleLeft)]
        [Description("文字の位置。左右は TextAlign と連動する。上下は 1 行入力のときだけ効く。")]
        public ContentAlignment ContentAlignment
        {
            get { return Combine(_verticalPosition, TextAlign); }
            set
            {
                if (!Enum.IsDefined(typeof(ContentAlignment), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(ContentAlignment));
                }

                _verticalPosition = GetVerticalPosition(value);
                HorizontalAlignment horizontal = GetHorizontalAlignment(value);
                if (TextAlign != horizontal)
                {
                    TextAlign = horizontal;
                }
                UpdateFrameLayout();
            }
        }

        /// <summary>フォント。既定は BIZ UDPゴシック（PC に無ければ游ゴシック）の 10.5 ポイント。</summary>
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value ?? SControlStyle.Font; }
        }

        /// <summary>文字の色。</summary>
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value.IsEmpty ? SControlStyle.TextColor : value; }
        }

        /// <summary>
        /// 高さをフォントに合わせて自動で決めるか。このコントロールは高さを自由に指定できるようにしているので、既定は false。
        /// </summary>
        [DefaultValue(false)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        protected override Size DefaultSize
        {
            get { return new Size(150, 28); }
        }

        // デザイナーが「既定値と違うときだけ」フォントと文字色を書き出すようにする。
        // Windows フォームのデザイナーは、ShouldSerialize＋プロパティ名 のメソッドを名前で探して使う
        private bool ShouldSerializeFont()
        {
            return !Font.Equals(SControlStyle.Font);
        }

        private bool ShouldSerializeForeColor()
        {
            return ForeColor != SControlStyle.TextColor;
        }

        #endregion

        #region 入力のプロパティ

        /// <summary>
        /// 利用者が入力した文字を選別する規則（入力できる文字の種類・空白・自動変換・最大文字数）。
        /// 継承したクラスが、自分のプロパティや固定の規則をここに設定する。
        /// </summary>
        internal SInputFilter InputFilter
        {
            get { return _filter; }
        }

        /// <summary>
        /// 入力できる最大文字数。0 は制限なし。数え方は、既定では文字単位（<see cref="STextBox.MaxLengthUnit"/> で変えられる）。
        /// </summary>
        /// <remarks>
        /// 標準の <see cref="TextBoxBase.MaxLength"/> は文字単位でしか数えられないので、同じ名前で置き換えている。
        /// 標準の側（Windows が行う制限）は、数え方が文字単位のときだけ同じ値にして、念のための二重の制限として使う。
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(32767)]
        [Description("入力できる最大文字数。0 は制限なし。")]
        public new int MaxLength
        {
            get { return _filter.MaxLength; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _filter.MaxLength = value;
                ApplyNativeMaxLength();
            }
        }

        /// <summary>
        /// フォーカスが移ってきたときに、文字列を全選択するか。
        /// マウスのクリックで移ってきた場合も全選択する（ドラッグで範囲を選んだ場合は、その選択を残す）。
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("フォーカスが移ってきたときに、文字列を全選択するか。")]
        public virtual bool HighlightText
        {
            get { return _highlightText; }
            set { _highlightText = value; }
        }

        /// <summary>
        /// 標準の側（Windows が行う文字数の制限）を、いまの設定に合わせる。最大文字数の数え方を変えたときに呼ぶ。
        /// 文字単位以外の数え方では、標準の制限が先に効いてしまわないよう、標準の側は制限なしにする。
        /// </summary>
        internal void ApplyNativeMaxLength()
        {
            base.MaxLength = _filter.MaxLengthUnit == SLengthUnit.Char ? _filter.MaxLength : 0;
        }

        #endregion

        #region 入力の制限

        /// <summary>
        /// 標準のテキスト ボックス（Windows の Edit コントロール）へメッセージを渡す直前に呼ばれる。
        /// キー入力も、IME で確定した文字も、最後は 1 文字ずつ WM_CHAR としてここを通るので、ここで入力規則を適用する。
        /// KeyPress イベントはこれより前に発生するため、KeyPress のハンドラーには変換前の文字が渡る。
        /// </summary>
        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_CHAR && !ReadOnly)
            {
                char typed = unchecked((char)m.WParam.ToInt64());

                if (typed == DeleteCharacter)
                {
                    // Ctrl+BackSpace。標準のテキスト ボックスは、これを目に見えない文字として挿入してしまうので、渡さない
                    return;
                }

                // BackSpace・Enter・Ctrl+C などの制御文字は制限の対象にしない
                if (!char.IsControl(typed))
                {
                    string accepted = AcceptTypedChar(typed);
                    if (accepted.Length == 0)
                    {
                        OnInvalidInput(EventArgs.Empty);
                        return;
                    }
                    accepted = ProcessInput(accepted);

                    // そのまま、または変換後の文字を渡す。1 文字が複数の文字になることがある（例: 「ガ」→「ｶﾞ」）。
                    // 後から届く文字と順序が入れ替わらないよう、この場で続けて渡す
                    foreach (char c in accepted)
                    {
                        Message charMessage = Message.Create(m.HWnd, WM_CHAR, (IntPtr)c, m.LParam);
                        base.DefWndProc(ref charMessage);
                        m.Result = charMessage.Result;
                    }
                    return;
                }
            }

            base.DefWndProc(ref m);
        }

        private string AcceptTypedChar(char c)
        {
            if (!char.IsHighSurrogate(c))
            {
                return _filter.Accept(c.ToString(), Text, SelectionStart, SelectionLength, Multiline);
            }

            // サロゲート ペアの文字は、前半・後半の 2 回に分かれて届く。前半だけが入って後半が文字数の制限で
            // 入らない、ということが起きないよう、前半の時点で 2 文字分が入るかを確かめる
            string pair = new string(new[] { c, '\uDC00' });
            string accepted = _filter.Accept(pair, Text, SelectionStart, SelectionLength, Multiline);
            return accepted.Length == pair.Length ? c.ToString() : string.Empty;
        }

        /// <summary>
        /// クリップボードの文字列を、入力規則に合う形に直してから貼り付ける。
        /// </summary>
        private void PasteFromClipboard()
        {
            if (ReadOnly)
            {
                return;
            }

            string text;
            try
            {
                text = Clipboard.ContainsText() ? Clipboard.GetText() : string.Empty;
            }
            catch (ExternalException)
            {
                // ほかのアプリがクリップボードを使用中で開けなかった。貼り付けをあきらめる
                return;
            }

            if (text.Length == 0)
            {
                return;
            }

            string accepted = _filter.Accept(text, Text, SelectionStart, SelectionLength, Multiline);
            if (accepted.Length == 0)
            {
                OnInvalidInput(EventArgs.Empty);
                return;
            }

            accepted = ProcessInput(accepted);
            if (accepted.Length > 0)
            {
                // 選択範囲を置き換える（「元に戻す」が効く）
                Paste(accepted);
            }
        }

        /// <summary>
        /// 入力規則（<see cref="InputFilter"/>）を通った文字列を、挿入する直前に受け取る。戻り値の文字列が、選択範囲と置き換えて挿入される。
        /// 既定では、受け取った文字列をそのまま返す。
        /// 文字の種類だけでは決められない規則（数値の形になっているか、など）を持つコントロールが、継承して使う。
        /// </summary>
        /// <remarks>
        /// 空文字列を返すと、何も挿入しない。このとき <see cref="InvalidInput"/> イベントは発生しないので、
        /// 入力を受け付けなかったことを知らせたい場合は <see cref="OnInvalidInput"/> を呼ぶこと。
        /// 呼ばれた時点では、まだ何も挿入されていない（Text・SelectionStart・SelectionLength は挿入前の状態）。
        /// </remarks>
        /// <param name="input">挿入しようとしている文字列。キー入力では 1 文字、貼り付けでは複数の文字。</param>
        protected virtual string ProcessInput(string input)
        {
            return input;
        }

        protected virtual void OnInvalidInput(EventArgs e)
        {
            EventHandler handler = InvalidInput;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region フォーカスと選択

        /// <summary>
        /// 文字列。入力中のコントロールにプログラムから設定した場合は、<see cref="HighlightText"/> に従って全選択し直す。
        /// </summary>
        /// <remarks>
        /// 標準の TextBox は、文字列を設定されると選択を解除して、カーソルを先頭に戻す。全選択はフォーカスが移ってきたときにしか
        /// 行わないので、そのままでは、フォーカスがある入力欄に値を表示した場合（画面を開いた直後の先頭の項目など）に、
        /// 全選択にならない。キー入力や貼り付けは、ここを通らない。
        /// </remarks>
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;

                if (_highlightText && IsActiveInput)
                {
                    SelectAll();
                }
            }
        }

        /// <summary>
        /// いま入力中のコントロールか（フォーカスがあり、ほかのコントロールへ移る途中でもない）。
        /// </summary>
        /// <remarks>
        /// Tab などでフォーカスが移るとき、Leave の時点では、まだこのコントロールにフォーカスが残っている。
        /// 派生クラスは Leave で表示用の文字列に直すので、フォーカスだけで判定すると、離れていく入力欄を全選択してしまう。
        /// 移る先は、Leave より前にコンテナーの ActiveControl に入っているので、それで見分ける。
        /// </remarks>
        private bool IsActiveInput
        {
            get
            {
                if (!Focused)
                {
                    return false;
                }

                var container = GetContainerControl() as ContainerControl;
                return container == null || container.ActiveControl == this;
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (_highlightText)
            {
                SelectAll();

                // クリックで移ってきた場合は、このあと標準の処理がクリック位置へカーソルを移して選択を解除する。
                // ボタンを離した時点で選択し直す
                _selectAllOnMouseUp = MouseButtons != MouseButtons.None;
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            _selectAllOnMouseUp = false;
            base.OnLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            if (_selectAllOnMouseUp)
            {
                _selectAllOnMouseUp = false;

                // ドラッグで範囲を選んでいた場合は、その選択を残す
                if (SelectionLength == 0)
                {
                    SelectAll();
                }
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            // 枠線の色と太さ、背景色を、フォーカスがあるときのものにする
            UpdateFocusFill(false);
            RedrawFrame();
            InvalidateFittedText();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            UpdateFocusFill(false);
            RedrawFrame();
            InvalidateFittedText();
        }

        /// <summary>
        /// フォーカスがあるときの背景色（<see cref="FocusBackColor"/>）で表示するかどうかを、いまの状態から決め直す。
        /// 表示する条件は、色が指定されていて、有効で、読み取り専用でなく、フォーカスがあること。
        /// </summary>
        /// <param name="colorChanged">表示する色そのものが変わった（表示するかどうかが同じでも、描き直す）。</param>
        private void UpdateFocusFill(bool colorChanged)
        {
            bool show = !_focusBackColor.IsEmpty && Enabled && !ReadOnly && Focused;
            if (show == _focusFillShown && !(colorChanged && show))
            {
                return;
            }
            _focusFillShown = show;

            // BackColor が返す色が切り替わったことを Windows フォームへ知らせて、入力部分を塗り直させる
            // （Windows フォームは背景を塗るブラシを覚えているので、知らせないと前の色のまま塗られる）。
            // この知らせ方しか無いので、フォーカスの出入りでも BackColorChanged イベントが発生する
            OnBackColorChanged(EventArgs.Empty);
        }

        /// <summary>
        /// 選択した文字を、<see cref="TextSelectionColor"/> の背景と、その上で読める色の文字で描き直す。
        /// </summary>
        /// <remarks>
        /// 標準のテキスト ボックスは、選択した文字を必ず Windows の標準の色（濃い青に白い文字）で描き、色を指定する手段が無い。
        /// そこで、標準の処理が描いた直後に、選択した範囲だけを上から描き直している。
        /// 標準の処理は、描画の要求（WM_PAINT）以外でも、キー入力やマウス操作のたびに直接描くので、
        /// 表示が変わりうるメッセージ（<see cref="ChangesSelectionDisplay"/>）を処理した後に、毎回呼ぶ。
        /// </remarks>
        private void PaintSelectionOverlay()
        {
            if (_paintingSelection || _textSelectionColor.IsEmpty || Multiline || !IsHandleCreated || !Enabled || !Focused)
            {
                return;
            }

            _paintingSelection = true;
            try
            {
                int start = SelectionStart;
                int length = SelectionLength;
                string text = GetDisplayText();
                if (length <= 0 || start >= text.Length)
                {
                    return;
                }
                string selected = text.Substring(start, Math.Min(length, text.Length - start));

                Rectangle client = ClientRectangle;
                const TextFormatFlags Flags =
                    DisabledTextFlags | TextFormatFlags.SingleLine | TextFormatFlags.Left | TextFormatFlags.PreserveGraphicsClipping;

                // 選択の先頭の文字の位置は、標準のテキスト ボックスに尋ねる（右寄せ・中央寄せや、横スクロールした状態でも正しい位置が返る）
                int left = GetPositionFromCharIndex(start).X;

                // 描いている間にキャレットが点滅すると、キャレットの跡が残るので、いったん隠す
                HideCaret(Handle);
                try
                {
                    using (Graphics g = CreateGraphics())
                    {
                        int width = TextRenderer.MeasureText(g, selected, Font, Size.Empty, Flags).Width;
                        Rectangle bounds = Rectangle.Intersect(client, new Rectangle(left, client.Top, width, client.Height));
                        if (bounds.Width <= 0)
                        {
                            return;
                        }

                        g.SetClip(bounds);
                        using (var brush = new SolidBrush(_textSelectionColor))
                        {
                            g.FillRectangle(brush, bounds);
                        }
                        Color textColor = SControlStyle.GetTextColorOn(_textSelectionColor, ForeColor);
                        TextRenderer.DrawText(g, selected, Font, new Rectangle(left, client.Top, width, client.Height), textColor, Flags);
                    }
                }
                finally
                {
                    ShowCaret(Handle);
                }
            }
            finally
            {
                _paintingSelection = false;
            }
        }

        /// <summary>
        /// <paramref name="m"/> を標準の処理が処理すると、選択した文字の表示が変わりうるか（描き直される、選択範囲が変わる、文字が変わる）。
        /// </summary>
        private bool ChangesSelectionDisplay(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_PAINT:
                case WM_SETFOCUS:
                case WM_SETTEXT:
                case WM_KEYDOWN:
                case WM_CHAR:
                case WM_LBUTTONDOWN:
                case WM_LBUTTONUP:
                case WM_LBUTTONDBLCLK:
                case WM_CUT:
                case WM_CLEAR:
                case WM_UNDO:
                case WM_IME_COMPOSITION:
                case WM_IME_ENDCOMPOSITION:
                case WM_HSCROLL:
                case EM_SETSEL:
                case EM_REPLACESEL:
                case EM_UNDO:
                case EM_SCROLLCARET:
                    return true;

                case WM_MOUSEMOVE:
                case WM_TIMER:
                    // マウスのボタンを押したままの移動（ドラッグでの範囲選択）と、その間の自動スクロール
                    return Capture;

                default:
                    return false;
            }
        }

        #endregion

        #region プレースホルダー

        /// <summary>
        /// いま、プレースホルダー（<see cref="PlaceholderText"/>）を表示する状態か。
        /// 表示する条件は、文字が指定されていて、入力欄が空で、有効で、読み取り専用でなく、IME で変換している最中でないこと。
        /// </summary>
        private bool ShouldShowPlaceholder
        {
            get
            {
                return _placeholderText.Length > 0 && !_imeComposing && IsHandleCreated && Enabled && !ReadOnly && TextLength == 0;
            }
        }

        /// <summary>
        /// プレースホルダーの表示を、いまの状態に合わせる。標準の処理が入力部分を描いた後に呼ぶ。
        /// </summary>
        /// <remarks>
        /// .NET Framework のテキスト ボックスには、プレースホルダーの機能が無い。Windows の機能（EM_SETCUEBANNER）は、
        /// 色を指定できず、複数行では使えない。そこで、選択した文字（<see cref="PaintSelectionOverlay"/>）と同じように、
        /// 標準の処理が描いた上から自前で描いている。
        /// </remarks>
        /// <param name="repainted">標準の処理が、入力部分の全体を描き直した直後か（WM_PAINT の後）。</param>
        private void RefreshPlaceholder(bool repainted)
        {
            bool show = ShouldShowPlaceholder;
            if (show)
            {
                // 描いている間にキャレットが点滅すると、キャレットの跡が残るので、いったん隠す
                HideCaret(Handle);
                try
                {
                    using (Graphics g = CreateGraphics())
                    {
                        PaintPlaceholder(g);
                    }
                }
                finally
                {
                    ShowCaret(Handle);
                }
            }
            else if (_placeholderShown && !repainted)
            {
                // 標準の処理は、変わった部分だけを描くので、プレースホルダーの一部が残ることがある。全体を描き直させる
                Invalidate();
            }
            _placeholderShown = show;
        }

        /// <summary>
        /// 入力部分に、背景とプレースホルダーを描く。
        /// </summary>
        /// <remarks>
        /// 文字だけを重ねて描くと、描くたびに文字の縁が濃くなる（縁をなめらかにする処理が、下の色と混ぜるため）。
        /// 背景から描き直し、ちらつかないよう、裏で描いてからまとめて写している。
        /// </remarks>
        private void PaintPlaceholder(Graphics target)
        {
            Rectangle bounds = ClientRectangle;
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                return;
            }

            using (BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(target, bounds))
            {
                using (var backBrush = new SolidBrush(BackColor))
                {
                    buffer.Graphics.FillRectangle(backBrush, bounds);
                }
                // 入力した文字より小さいフォントで描く。入力部分の高さは元のフォントの 1 行分なので、1 行入力では上下の中央に置く
                TextFormatFlags flags = GetClientTextFlags();
                if (!Multiline)
                {
                    flags |= TextFormatFlags.VerticalCenter;
                }
                TextRenderer.DrawText(buffer.Graphics, _placeholderText, _placeholderFont.Get(Font), bounds, _placeholderColor, flags);
                buffer.Render();
            }
        }

        #endregion

        #region 枠の配置と描画

        /// <summary>
        /// ウィンドウの大きさが <paramref name="windowSize"/> のときの、文字を入力する部分の位置と大きさ（ウィンドウの左上が原点）。
        /// この外側が、枠と余白になる。
        /// </summary>
        private Rectangle GetInnerBounds(Size windowSize)
        {
            float scale = LayoutScale.Of(this);
            bool bordered = _borderStyle != BorderStyle.None;

            // 枠線の分は、太いほう（フォーカスがあるとき）に合わせて空けておく。フォーカスの有無で文字が動かないようにするため
            int border = bordered ? SControlStyle.GetBorderWidth(scale, true) : 0;
            int horizontal = SControlStyle.GetTextInset(scale, bordered);
            int vertical = border + (int)Math.Round(VerticalPadding * scale);

            int left = Math.Min(horizontal, Math.Max(0, (windowSize.Width - 1) / 2));
            int width = Math.Max(1, windowSize.Width - left * 2 - GetSideButtonWidth(scale));

            if (Multiline)
            {
                int top = Math.Min(vertical, Math.Max(0, (windowSize.Height - 1) / 2));
                return new Rectangle(left, top, width, Math.Max(1, windowSize.Height - top * 2));
            }

            // 1 行入力では、入力部分の高さを 1 行分にして、縦位置の指定に従って置く
            int available = Math.Max(1, windowSize.Height - border * 2);
            int height = Math.Min(LineHeight, available);
            int y;
            switch (_verticalPosition)
            {
                case VerticalPosition.Top:
                    y = vertical;
                    break;
                case VerticalPosition.Bottom:
                    y = windowSize.Height - vertical - height;
                    break;
                default:
                    y = (windowSize.Height - height) / 2;
                    break;
            }

            // 高さが足りない場合でも、枠線の内側に収める
            y = Math.Max(border, Math.Min(y, windowSize.Height - border - height));
            return new Rectangle(left, Math.Max(0, y), width, height);
        }

        /// <summary>1 行分の文字の高さ。標準のテキスト ボックスが文字を描く高さと同じ値。</summary>
        private int LineHeight
        {
            get
            {
                if (_lineHeight == 0)
                {
                    _lineHeight = Math.Max(1, TextRenderer.MeasureText("あg", Font, Size.Empty, TextFormatFlags.NoPadding).Height);
                }
                return _lineHeight;
            }
        }

        /// <summary>
        /// 枠と余白の大きさを計算し直させる（枠線の種類・文字の縦位置・フォント・サイド ボタンの幅など、配置に関わるものを変えたとき）。
        /// </summary>
        protected void UpdateFrameLayout()
        {
            if (IsHandleCreated)
            {
                SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0,
                    SWP_NOSIZE | SWP_NOMOVE | SWP_NOZORDER | SWP_NOACTIVATE | SWP_FRAMECHANGED);
                RedrawFrame();
            }
        }

        /// <summary>枠（非クライアント領域。サイド ボタンを含む）を描き直させる。</summary>
        protected void RedrawFrame()
        {
            if (IsHandleCreated)
            {
                RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, RDW_FRAME | RDW_INVALIDATE);
            }
        }

        private void RedrawDisabledSurface()
        {
            if (!Enabled)
            {
                RedrawFrame();
            }
        }

        /// <summary>
        /// 枠と余白を <paramref name="target"/>（ウィンドウの左上が原点）へ描く。文字を入力する部分には描かない。
        /// </summary>
        private void PaintFrame(Graphics target)
        {
            int width = Width;
            int height = Height;
            if (width <= 0 || height <= 0)
            {
                return;
            }

            // いったん裏で描いてから、入力部分を除いて写す（ちらつきを防ぐ）
            using (var buffer = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(buffer))
                {
                    DrawFrame(g, width, height);
                }

                GraphicsState state = target.Save();
                try
                {
                    target.SetClip(new Rectangle(0, 0, width, height));
                    target.ExcludeClip(GetInnerBounds(new Size(width, height)));
                    target.DrawImageUnscaled(buffer, 0, 0);
                }
                finally
                {
                    target.Restore(state);
                }
            }
        }

        private void DrawFrame(Graphics g, int width, int height)
        {
            float scale = LayoutScale.Of(this);

            // 角丸の外側は、親コントロールと同じ見た目にする
            PaintHelper.PaintParentAppearance(this, g, InvokePaintBackground, InvokePaint);

            Color fill = Enabled ? BackColor : _disabledBackColor;
            if (_borderStyle == BorderStyle.None)
            {
                using (var fillBrush = new SolidBrush(fill))
                {
                    g.FillRectangle(fillBrush, 0, 0, width, height);
                }
            }
            else
            {
                bool focused = Focused && Enabled;
                SControlStyle.DrawFrame(
                    g, width, height, scale, fill,
                    focused ? _focusBorderColor : _borderColor,
                    SControlStyle.GetBorderWidth(scale, focused),
                    _cornerRadius);
            }

            Rectangle sideButton = GetSideButtonWindowBounds(new Size(width, height));
            if (sideButton.Width > 0)
            {
                PaintSideButton(g, sideButton, fill);
            }
        }

        #endregion

        #region サイド ボタン

        /// <summary>
        /// 枠の内側の右端に確保する、ボタン領域（サイド ボタン）の幅（ピクセル）。既定は 0（領域なし）。
        /// 一覧を開く矢印などを置くコントロールが、継承して使う。文字を入力する部分は、この幅の分だけ狭くなる。
        /// 幅を変えたときは <see cref="UpdateFrameLayout"/> を呼ぶこと。
        /// </summary>
        /// <param name="scale">表示倍率（<see cref="LayoutScale.Of"/> の値）。</param>
        protected virtual int GetSideButtonWidth(float scale)
        {
            return 0;
        }

        /// <summary>
        /// サイド ボタンを描く。枠と背景は描き終わっている。見た目が変わったときは <see cref="RedrawFrame"/> で描き直させる。
        /// </summary>
        /// <param name="bounds">サイド ボタンの領域（コントロールの左上が原点）。</param>
        /// <param name="backColor">いまの背景色（無効のときは無効の色）。</param>
        protected virtual void PaintSideButton(Graphics g, Rectangle bounds, Color backColor)
        {
        }

        /// <summary>
        /// サイド ボタンをマウスの左ボタンで押したときに呼ばれる。フォーカスは、呼ばれる前にこのコントロールへ移してある。
        /// </summary>
        protected virtual void OnSideButtonClick(EventArgs e)
        {
        }

        /// <summary>サイド ボタンの領域（コントロールの左上が原点）。サイド ボタンが無ければ空。</summary>
        private Rectangle GetSideButtonWindowBounds(Size windowSize)
        {
            float scale = LayoutScale.Of(this);
            int buttonWidth = GetSideButtonWidth(scale);
            if (buttonWidth <= 0)
            {
                return Rectangle.Empty;
            }

            int border = _borderStyle != BorderStyle.None ? SControlStyle.GetBorderWidth(scale, true) : 0;
            return new Rectangle(
                windowSize.Width - border - buttonWidth, border, buttonWidth, Math.Max(0, windowSize.Height - border * 2));
        }

        /// <summary>
        /// <paramref name="clientPoint"/>（文字を入力する部分の左上が原点）が、サイド ボタンの上か。
        /// </summary>
        private bool IsOverSideButton(Point clientPoint)
        {
            Rectangle bounds = GetSideButtonWindowBounds(Size);
            if (bounds.Width <= 0)
            {
                return false;
            }

            // マウスの座標は入力部分が基準なので、コントロールの左上が基準の座標に直してから比べる
            Point inner = GetInnerBounds(Size).Location;
            return bounds.Contains(clientPoint.X + inner.X, clientPoint.Y + inner.Y);
        }

        #endregion

        #region 収まらない文字を、縮めて表示する

        // null は、既定（DefaultShrinkTextToFit）のまま
        private bool? _shrinkTextToFit;

        /// <summary>
        /// 文字列が入力部分の幅に収まらないときに、収まる大きさまでフォントを縮めて表示するか。
        /// 既定は true（数値の入力欄 <see cref="SNumberBox"/> だけ、既定は false）。
        /// 縮めるのは、フォーカスがない間（と、無効の間）だけ。フォーカスがある間は、入力しやすいように、
        /// 元のフォントのまま表示する（収まらない分は、横に送られる）。
        /// </summary>
        /// <remarks>
        /// 縮めるのは 8 ポイントまで（<see cref="SFittedFont.MinimumSize"/>）。それでも収まらないときは、末尾を「…」にする。
        /// 標準のテキスト ボックスは、描くフォントを文字列ごとに変えられないので、縮めて表示する間は、入力部分を自前で描いている。
        /// 1 行入力のときだけ働く。
        /// </remarks>
        [Category("Appearance")]
        [Description("文字列が収まらないときに、収まる大きさまでフォントを縮めて表示するか（フォーカスがない間だけ）。")]
        public bool ShrinkTextToFit
        {
            get { return _shrinkTextToFit ?? DefaultShrinkTextToFit; }
            set
            {
                if (ShrinkTextToFit != value)
                {
                    _shrinkTextToFit = value;
                    RefreshFittedText();
                }
            }
        }

        /// <summary><see cref="ShrinkTextToFit"/> の既定値。部品の種類ごとに決める（上書きしなければ true）。</summary>
        protected virtual bool DefaultShrinkTextToFit
        {
            get { return true; }
        }

        // デザイナーが「既定値と違うときだけ」Designer.cs に書き出すようにする（既定値が部品の種類で違うので、DefaultValue 属性は使えない）
        private bool ShouldSerializeShrinkTextToFit()
        {
            return ShrinkTextToFit != DefaultShrinkTextToFit;
        }

        private void ResetShrinkTextToFit()
        {
            ShrinkTextToFit = DefaultShrinkTextToFit;
        }

        /// <summary><paramref name="text"/> を入力部分に表示するときのフォント（収まれば <see cref="Font"/>、収まらなければ縮めたもの）。</summary>
        private Font GetFittedFont(string text)
        {
            return _fittedFont.Get(text, Font, ClientSize.Width, GetClientTextFlags());
        }

        /// <summary>直前の <see cref="GetFittedFont"/> で、文字列が元のフォントでは収まらなかったか。</summary>
        private bool FittedTextOverflows
        {
            get { return _fittedFont.IsShrunk || _fittedFont.IsClipped; }
        }

        /// <summary>
        /// いま、入力部分を、縮めたフォントで自前で描く状態か。
        /// 条件は、縮める設定で、有効で、フォーカスがなく、1 行入力で、文字列が元のフォントでは収まらないこと。
        /// </summary>
        private bool ShouldPaintFittedText
        {
            get
            {
                if (!ShrinkTextToFit || !Enabled || Focused || Multiline || _imeComposing || !IsHandleCreated || TextLength == 0)
                {
                    return false;
                }
                GetFittedFont(GetDisplayText());
                return FittedTextOverflows;
            }
        }

        /// <summary>入力部分を、縮めたフォントで描く。背景と文字の色は、標準の処理が描くときと同じ。</summary>
        private void PaintFittedClient(Graphics g)
        {
            Rectangle bounds = ClientRectangle;
            using (var backBrush = new SolidBrush(BackColor))
            {
                g.FillRectangle(backBrush, bounds);
            }

            string text = GetDisplayText();
            TextRenderer.DrawText(g, text, GetFittedFont(text), bounds, ForeColor, GetClientTextFlags() | FittedTextFlags);
        }

        /// <summary>縮めたフォントで描くときに足す指定。入力部分の高さは元のフォントの 1 行分なので、上下の中央に置く。収まらなければ、末尾を「…」にする。</summary>
        private const TextFormatFlags FittedTextFlags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;

        /// <summary>縮めて表示する設定のときに、入力部分を描き直させる（フォーカスの出入りで、自前の描画と標準の描画が切り替わるため）。</summary>
        private void InvalidateFittedText()
        {
            if (ShrinkTextToFit && IsHandleCreated)
            {
                Invalidate();
            }
        }

        /// <summary>縮めて表示する設定を変えたときに、入力部分を描き直させる。</summary>
        private void RefreshFittedText()
        {
            if (IsHandleCreated)
            {
                Invalidate();
                RedrawDisabledSurface();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fittedFont.Dispose();
                _placeholderFont.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region 無効のときの描画と、文字の位置の変換

        /// <summary>
        /// 無効（Enabled = false）のときの入力部分を描く。標準のテキスト ボックスは無効のときの文字色を変えられないので、
        /// 無効の間だけ自前で描いている。
        /// </summary>
        private void PaintDisabledClient(Graphics g)
        {
            Rectangle bounds = ClientRectangle;
            using (var backBrush = new SolidBrush(_disabledBackColor))
            {
                g.FillRectangle(backBrush, bounds);
            }

            string text = GetDisplayText();
            if (text.Length == 0)
            {
                return;
            }

            Color textColor = _disabledForeColor.IsEmpty ? ForeColor : _disabledForeColor;
            Font font = Font;
            TextFormatFlags flags = GetClientTextFlags();
            if (ShrinkTextToFit && !Multiline)
            {
                // 収まらないときは、縮めたフォントで描く
                font = GetFittedFont(text);
                if (FittedTextOverflows)
                {
                    flags |= FittedTextFlags;
                }
            }
            TextRenderer.DrawText(g, text, font, bounds, textColor, flags);
        }

        /// <summary>
        /// 入力部分に自前で文字を描くときの指定。標準のテキスト ボックスが描く位置（寄せ方、1 行・複数行）に合わせる。
        /// </summary>
        private TextFormatFlags GetClientTextFlags()
        {
            TextFormatFlags flags = DisabledTextFlags;
            switch (TextAlign)
            {
                case HorizontalAlignment.Center:
                    flags |= TextFormatFlags.HorizontalCenter;
                    break;
                case HorizontalAlignment.Right:
                    flags |= TextFormatFlags.Right;
                    break;
                default:
                    flags |= TextFormatFlags.Left;
                    break;
            }

            if (Multiline)
            {
                flags |= TextFormatFlags.TextBoxControl;
                if (WordWrap)
                {
                    flags |= TextFormatFlags.WordBreak;
                }
            }
            else
            {
                flags |= TextFormatFlags.SingleLine;
            }

            return flags;
        }

        /// <summary>画面に表示する文字列。パスワード入力のときは、伏せ字にしたもの。</summary>
        private string GetDisplayText()
        {
            char mask = PasswordChar;
            return mask == '\0' || Multiline ? Text : new string(mask, TextLength);
        }

        private static ContentAlignment Combine(VerticalPosition vertical, HorizontalAlignment horizontal)
        {
            switch (vertical)
            {
                case VerticalPosition.Top:
                    return horizontal == HorizontalAlignment.Left ? ContentAlignment.TopLeft
                        : horizontal == HorizontalAlignment.Center ? ContentAlignment.TopCenter
                        : ContentAlignment.TopRight;
                case VerticalPosition.Bottom:
                    return horizontal == HorizontalAlignment.Left ? ContentAlignment.BottomLeft
                        : horizontal == HorizontalAlignment.Center ? ContentAlignment.BottomCenter
                        : ContentAlignment.BottomRight;
                default:
                    return horizontal == HorizontalAlignment.Left ? ContentAlignment.MiddleLeft
                        : horizontal == HorizontalAlignment.Center ? ContentAlignment.MiddleCenter
                        : ContentAlignment.MiddleRight;
            }
        }

        private static VerticalPosition GetVerticalPosition(ContentAlignment alignment)
        {
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    return VerticalPosition.Top;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    return VerticalPosition.Bottom;
                default:
                    return VerticalPosition.Middle;
            }
        }

        private static HorizontalAlignment GetHorizontalAlignment(ContentAlignment alignment)
        {
            switch (alignment)
            {
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    return HorizontalAlignment.Center;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    return HorizontalAlignment.Right;
                default:
                    return HorizontalAlignment.Left;
            }
        }

        #endregion

        #region 状態の変化に合わせた描き直し

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            _lineHeight = 0;
            UpdateFrameLayout();
        }

        protected override void OnMultilineChanged(EventArgs e)
        {
            base.OnMultilineChanged(e);
            UpdateFrameLayout();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RedrawFrame();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            UpdateFocusFill(false);
            RedrawFrame();
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            base.OnReadOnlyChanged(e);

            // フォーカスがあるときの背景色は、読み取り専用の間は使わない
            UpdateFocusFill(false);
            RedrawFrame();

            // プレースホルダーは、読み取り専用の間は表示しない
            Invalidate();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            RedrawFrame();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            RedrawFrame();
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            RedrawFrame();
        }

        #endregion

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCCALCSIZE:
                    // 文字を入力する部分（クライアント領域）を、枠と余白の分だけ内側へ縮める
                    ShrinkClientArea(m.LParam);
                    base.WndProc(ref m);
                    return;

                case WM_NCPAINT:
                    // 複数行のときのスクロール バーは標準の処理が描く。その後で枠を描く
                    base.WndProc(ref m);
                    PaintFrameToWindow();
                    return;

                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    int hit = m.Result.ToInt32();
                    if (hit != HTCLIENT && hit != HTHSCROLL && hit != HTVSCROLL)
                    {
                        // 枠と余白の上でのクリックも、入力部分のクリックとして扱う（フォーカスが移り、カーソルが近い位置に入る）
                        m.Result = (IntPtr)HTCLIENT;
                    }
                    return;

                case WM_PRINT:
                    // DrawToBitmap など、画面以外への描画要求。標準の処理は自前の枠を描かないので、後から描き足す
                    base.WndProc(ref m);
                    if ((m.LParam.ToInt64() & PRF_NONCLIENT) != 0)
                    {
                        using (Graphics g = Graphics.FromHdc(m.WParam))
                        {
                            PaintFrame(g);
                        }
                    }
                    return;

                case WM_PAINT:
                    if (!Enabled)
                    {
                        using (Graphics g = CreateGraphics())
                        {
                            PaintDisabledClient(g);
                        }
                        // 描画が済んだことを Windows に伝える（伝えないと描画要求が繰り返し届く）
                        ValidateRect(Handle, IntPtr.Zero);
                        m.Result = IntPtr.Zero;
                        return;
                    }
                    if (ShouldPaintFittedText)
                    {
                        // 収まらない文字を、縮めたフォントで描く（標準の処理には描かせない）
                        using (Graphics g = CreateGraphics())
                        {
                            PaintFittedClient(g);
                        }
                        ValidateRect(Handle, IntPtr.Zero);
                        m.Result = IntPtr.Zero;
                        return;
                    }
                    break;

                case WM_PRINTCLIENT:
                    if (!Enabled)
                    {
                        using (Graphics g = Graphics.FromHdc(m.WParam))
                        {
                            PaintDisabledClient(g);
                        }
                        m.Result = IntPtr.Zero;
                        return;
                    }
                    if (ShouldPaintFittedText)
                    {
                        using (Graphics g = Graphics.FromHdc(m.WParam))
                        {
                            PaintFittedClient(g);
                        }
                        m.Result = IntPtr.Zero;
                        return;
                    }
                    break;

                case WM_ERASEBKGND:
                    if (!Enabled)
                    {
                        // 無効の間は WM_PAINT で全面を描くので、標準の背景消去（別の色で塗られる）をさせない
                        m.Result = (IntPtr)1;
                        return;
                    }
                    break;

                case WM_SETFONT:
                    base.WndProc(ref m);
                    if (Multiline)
                    {
                        // 文字の左右の余白は枠の側で取っているので、標準の余白はなくす
                        //（1 行入力のときは Windows フォームが同じことをしている）
                        SendMessage(Handle, EM_SETMARGINS, (IntPtr)(EC_LEFTMARGIN | EC_RIGHTMARGIN), IntPtr.Zero);
                    }
                    return;

                case WM_LBUTTONDOWN:
                case WM_LBUTTONDBLCLK:
                    // マウスの位置は lParam の下位 16 ビットが X、上位 16 ビットが Y（どちらも符号付き）
                    long position = m.LParam.ToInt64();
                    if (IsOverSideButton(new Point((short)(position & 0xFFFF), (short)((position >> 16) & 0xFFFF))))
                    {
                        // サイド ボタンのクリックは、文字を入力する部分へは渡さない
                        if (!Focused)
                        {
                            Focus();
                        }
                        OnSideButtonClick(EventArgs.Empty);
                        return;
                    }
                    break;

                case WM_SETCURSOR:
                    if (IsOverSideButton(PointToClient(MousePosition)))
                    {
                        // サイド ボタンの上では、文字入力用のカーソル（I 字）ではなく矢印にする
                        Cursor.Current = Cursors.Default;
                        m.Result = (IntPtr)1;
                        return;
                    }
                    break;

                case WM_PASTE:
                    PasteFromClipboard();
                    return;

                case WM_IME_STARTCOMPOSITION:
                    _imeComposing = true;
                    break;

                case WM_IME_ENDCOMPOSITION:
                case WM_KILLFOCUS:
                    // フォーカスが外れたときにも戻す（変換の終わりが届かなかった場合に、プレースホルダーが出ないままにならないように）
                    _imeComposing = false;
                    break;
            }

            base.WndProc(ref m);

            // 標準の処理が描いた上から、自前の表示（選択した文字の色、プレースホルダー）を描き足す
            if (ChangesSelectionDisplay(ref m))
            {
                PaintSelectionOverlay();
                RefreshPlaceholder(m.Msg == WM_PAINT);
            }
            else if (m.Msg == WM_IME_STARTCOMPOSITION || m.Msg == WM_KILLFOCUS)
            {
                RefreshPlaceholder(false);
            }
            else if (m.Msg == WM_PRINTCLIENT && ShouldShowPlaceholder)
            {
                // DrawToBitmap など、画面以外への描画要求
                using (Graphics g = Graphics.FromHdc(m.WParam))
                {
                    PaintPlaceholder(g);
                }
            }
        }

        /// <summary>
        /// WM_NCCALCSIZE の処理。<paramref name="rectPointer"/> が指す矩形（ウィンドウ全体）を、文字を入力する部分の矩形に書き換える。
        /// </summary>
        private void ShrinkClientArea(IntPtr rectPointer)
        {
            // wParam の値にかかわらず、lParam が指す先頭には「ウィンドウ全体の矩形」が入っている
            var rect = (RECT)Marshal.PtrToStructure(rectPointer, typeof(RECT));
            Rectangle inner = GetInnerBounds(new Size(rect.Right - rect.Left, rect.Bottom - rect.Top));

            rect.Right = rect.Left + inner.Right;
            rect.Bottom = rect.Top + inner.Bottom;
            rect.Left += inner.Left;
            rect.Top += inner.Top;
            Marshal.StructureToPtr(rect, rectPointer, false);
        }

        private void PaintFrameToWindow()
        {
            IntPtr hdc = GetWindowDC(Handle);
            if (hdc == IntPtr.Zero)
            {
                return;
            }

            try
            {
                using (Graphics g = Graphics.FromHdc(hdc))
                {
                    PaintFrame(g);
                }
            }
            finally
            {
                ReleaseDC(Handle, hdc);
            }
        }

        #region Windows API

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);

        [DllImport("user32.dll")]
        private static extern bool RedrawWindow(IntPtr hWnd, IntPtr updateRect, IntPtr updateRegion, int flags);

        [DllImport("user32.dll")]
        private static extern bool ValidateRect(IntPtr hWnd, IntPtr rect);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowCaret(IntPtr hWnd);

        #endregion
    }
}
