using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 角丸の枠を持つコンボ ボックス。GcComboBoxEx（InputMan の GcComboBox）の代わりに使う。
    /// 標準の <see cref="ComboBox"/> を継承しているので、Items・SelectedIndex・SelectedIndexChanged などはそのまま使える。
    /// 標準のコンボ ボックスに加えて、次の機能を持つ。
    /// <list type="bullet">
    /// <item>表示する文字列と内部の値を組にした項目（<see cref="AddItem"/>）と、値での選択・取得（<see cref="SelectedValue"/>）。
    /// 標準の SelectedValue は DataSource を使うときしか働かないが、このコントロールではどちらの場合も同じように使える</item>
    /// <item>利用者に選択を変えさせない設定（<see cref="ReadOnly"/>）</item>
    /// <item>Space キーでの一覧の開閉（<see cref="OpenBySpaceKey"/>）と、F4 キーで一覧を開かない設定（<see cref="OpenByF4Key"/>）</item>
    /// <item>無効のときの色（<see cref="DisabledBackColor"/>・<see cref="DisabledForeColor"/>）</item>
    /// <item>フォーカスがあるときの枠線の色と背景色（<see cref="FocusBorderColor"/>・<see cref="FocusBackColor"/>）</item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>
    /// 一覧から選ぶだけの形式（<see cref="ComboBoxStyle.DropDownList"/>。既定）のときは、閉じた状態の見た目（枠・文字列・矢印）を
    /// すべてこのコントロールが描く。文字を入力できる形式（DropDown・Simple）にした場合は、標準のコンボ ボックスの見た目のままになる。
    /// </para>
    /// <para>
    /// 高さは <see cref="ComboBox.ItemHeight"/> で決まる（コントロールの高さは ItemHeight + 6）。96 DPI 基準のピクセルで指定する。
    /// </para>
    /// </remarks>
    public class SComboBox : ComboBox, ISKeyUser
    {
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONDBLCLK = 0x0203;

        /// <summary>項目の高さの既定値。コントロールの高さが <see cref="STextBox"/> の既定値（28）と同じになる。</summary>
        private const int DefaultItemHeight = 22;

        private const TextFormatFlags ItemTextFlags =
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.SingleLine |
            TextFormatFlags.NoPadding |
            TextFormatFlags.NoPrefix |
            TextFormatFlags.EndEllipsis;

        private BorderStyle _borderStyle = BorderStyle.FixedSingle;
        private Color _borderColor = SControlStyle.BorderColor;
        private Color _focusBorderColor = SControlStyle.FocusBorderColor;
        private Color _focusBackColor = SControlStyle.FocusBackColor;
        private Color _disabledBackColor = SControlStyle.DisabledBackColor;
        private Color _disabledForeColor = Color.Empty;
        private Color _selectionColor = SControlStyle.SelectionBackColor;
        private Color _selectionForeColor = Color.Empty;
        private int _cornerRadius = SControlStyle.CornerRadius;

        private bool _readOnly;
        private string _placeholderText = string.Empty;
        private Color _placeholderColor = SControlStyle.PlaceholderColor;
        private bool _shrinkTextToFit = true;
        private readonly SFittedFont _fittedFont = new SFittedFont();
        private readonly SPlaceholderFont _placeholderFont = new SPlaceholderFont();
        private bool _openBySpaceKey = true;
        private bool _openByF4Key;

        public SComboBox()
        {
            // 標準の描画ではコントロールの高さがフォントで決まってしまう。項目を自前で描いて、ItemHeight で高さを指定できるようにする
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.ItemHeight = DefaultItemHeight;
            base.DropDownStyle = ComboBoxStyle.DropDownList;

            base.Font = SControlStyle.Font;
            base.ForeColor = SControlStyle.TextColor;

            UpdatePaintMode();
        }

        #region 項目と値

        /// <summary>
        /// 表示する文字列と内部の値を組にした項目を、一覧の末尾に追加する。
        /// まとめて追加するときは、前後を BeginUpdate / EndUpdate で囲むと描き直しが 1 回で済む。
        /// </summary>
        /// <param name="text">一覧に表示する文字列。</param>
        /// <param name="value">内部の値。<see cref="SelectedValue"/> で取得・選択に使う。</param>
        /// <returns>追加した項目の位置。</returns>
        public int AddItem(string text, object value)
        {
            return Items.Add(new SComboItem(text, value));
        }

        /// <summary>
        /// 選択中の項目の内部の値。何も選択していなければ null。
        /// 設定すると、その値を持つ項目を選択する。該当する項目が無い場合と、null を設定した場合は、未選択にする。
        /// </summary>
        /// <remarks>
        /// 値の比較は <see cref="SelectByValue"/> と同じ。項目の値は、<see cref="AddItem"/> で追加した項目ならその値、
        /// DataSource を使っている場合は ValueMember の列の値になる。
        /// 標準の <see cref="ListControl.SelectedValue"/> は DataSource を使うときしか働かないので、同じ名前で置き換えている。
        /// </remarks>
        [Bindable(true)]
        [Browsable(false)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new object SelectedValue
        {
            get { return GetValue(SelectedIndex); }
            set { SelectByValue(value); }
        }

        /// <summary>
        /// <paramref name="index"/> の位置の項目の、内部の値を返す。範囲外なら null。
        /// </summary>
        public object GetValue(int index)
        {
            if (index < 0 || index >= Items.Count)
            {
                return null;
            }

            object item = Items[index];
            var pair = item as SComboItem;
            if (pair != null)
            {
                return pair.Value;
            }

            // DataSource の行など。ValueMember を指定していれば、その列の値を返す
            return string.IsNullOrEmpty(ValueMember) ? item : FilterItemOnProperty(item, ValueMember);
        }

        /// <summary>
        /// <paramref name="index"/> の位置の項目の、表示する文字列を返す。範囲外なら null。
        /// </summary>
        public string GetText(int index)
        {
            return index < 0 || index >= Items.Count ? null : GetItemText(Items[index]);
        }

        /// <summary>
        /// 内部の値が <paramref name="value"/> の項目を選択する。該当する項目が無い場合と、null を渡した場合は、未選択にする。
        /// </summary>
        /// <remarks>
        /// 型が違っていても、文字列にしたときに同じなら一致とみなす（項目の値が int の 1 で、DB から読んだ値が short の 1 や
        /// 文字列の "1" でも選択できる）。
        /// </remarks>
        /// <returns>該当する項目があったか。</returns>
        public bool SelectByValue(object value)
        {
            int index = -1;
            if (value != null)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (ValuesMatch(GetValue(i), value))
                    {
                        index = i;
                        break;
                    }
                }
            }

            SelectedIndex = index;
            return index >= 0;
        }

        /// <summary>
        /// 表示する文字列が <paramref name="text"/> と一致する項目を選択する（大文字と小文字は区別しない）。
        /// 該当する項目が無い場合は、未選択にする。
        /// </summary>
        /// <returns>該当する項目があったか。</returns>
        public bool SelectByText(string text)
        {
            int index = string.IsNullOrEmpty(text) ? -1 : FindStringExact(text);
            SelectedIndex = index;
            return index >= 0;
        }

        /// <summary>
        /// 項目の値 <paramref name="itemValue"/> と、探している値 <paramref name="value"/> が一致するか。
        /// 型が違っていても、文字列にしたときに同じなら一致とみなす。どちらかが null なら一致しない。
        /// </summary>
        internal static bool ValuesMatch(object itemValue, object value)
        {
            if (itemValue == null || value == null)
            {
                return false;
            }
            if (itemValue.Equals(value))
            {
                return true;
            }

            return string.Equals(
                Convert.ToString(itemValue, CultureInfo.InvariantCulture),
                Convert.ToString(value, CultureInfo.InvariantCulture),
                StringComparison.Ordinal);
        }

        #endregion

        #region 動作のプロパティ

        /// <summary>
        /// 利用者に選択を変えさせないか。true にすると、クリックやキー操作で一覧が開かず、選択も変わらない。
        /// プログラムからの変更（SelectedIndex・SelectedValue の設定）はできる。フォーカスは受け取る。
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("利用者に選択を変えさせないか。true にすると、クリックやキー操作で一覧が開かず、選択も変わらない。")]
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                if (_readOnly != value)
                {
                    _readOnly = value;
                    if (value && IsHandleCreated && DroppedDown)
                    {
                        DroppedDown = false;
                    }
                    Invalidate();
                }
            }
        }

        /// <summary>Space キーで一覧を開閉するか。</summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Space キーで一覧を開閉するか。")]
        public bool OpenBySpaceKey
        {
            get { return _openBySpaceKey; }
            set { _openBySpaceKey = value; }
        }

        /// <summary>
        /// F4 キーで一覧を開閉するか（標準のコンボ ボックスの動作）。
        /// このアプリでは F4 を画面のファンクション キーに使うので、既定は false。
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("F4 キーで一覧を開閉するか。このアプリでは F4 を画面のファンクション キーに使うので、既定は false。")]
        public bool OpenByF4Key
        {
            get { return _openByF4Key; }
            set { _openByF4Key = value; }
        }

        /// <summary>
        /// コンボ ボックスの形式。既定は、一覧から選ぶだけの <see cref="ComboBoxStyle.DropDownList"/>。
        /// それ以外にした場合は、標準のコンボ ボックスの見た目になる。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(ComboBoxStyle.DropDownList)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description("コンボ ボックスの形式。既定は、一覧から選ぶだけの DropDownList。それ以外にした場合は、標準のコンボ ボックスの見た目になる。")]
        public new ComboBoxStyle DropDownStyle
        {
            get { return base.DropDownStyle; }
            set { base.DropDownStyle = value; }
        }

        /// <summary>
        /// 項目の描き方。このコントロールは項目を自前で描くので、固定（OwnerDrawFixed）で変更できない。
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DrawMode DrawMode
        {
            get { return base.DrawMode; }
            set { }
        }

        #endregion

        #region 見た目のプロパティ

        /// <summary>
        /// 枠線の種類。<see cref="BorderStyle.None"/> にすると、枠線と角丸をなくす（グリッドのセルに重ねる入力欄などに使う）。
        /// それ以外は角丸の枠線を描く（Fixed3D は FixedSingle と同じ）。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(BorderStyle.FixedSingle)]
        [Description("枠線の種類。None にすると枠線と角丸をなくす。それ以外は角丸の枠線を描く。")]
        public BorderStyle BorderStyle
        {
            get { return _borderStyle; }
            set
            {
                if (!Enum.IsDefined(typeof(BorderStyle), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(BorderStyle));
                }
                _borderStyle = value;
                Invalidate();
            }
        }

        /// <summary>枠線の色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "186, 197, 211")]
        [Description("枠線の色。")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        /// <summary>フォーカスがあるときの枠線の色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "64, 144, 232")]
        [Description("フォーカスがあるときの枠線の色。")]
        public Color FocusBorderColor
        {
            get { return _focusBorderColor; }
            set { _focusBorderColor = value; Invalidate(); }
        }

        /// <summary>
        /// フォーカスがあるときの背景色。空（<see cref="Color.Empty"/>）にすると、フォーカスがあっても背景色を変えない。
        /// 利用者に選択を変えさせない設定（<see cref="ReadOnly"/>）のときは変えない。開いた一覧の背景は、BackColor のまま。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Lavender")]
        [Description("フォーカスがあるときの背景色。空にすると、フォーカスがあっても背景色を変えない。")]
        public Color FocusBackColor
        {
            get { return _focusBackColor; }
            set { _focusBackColor = value; Invalidate(); }
        }

        /// <summary>無効（Enabled = false）のときの背景色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "236, 238, 241")]
        [Description("無効（Enabled = false）のときの背景色。")]
        public Color DisabledBackColor
        {
            get { return _disabledBackColor; }
            set { _disabledBackColor = value; Invalidate(); }
        }

        /// <summary>
        /// 無効（Enabled = false）のときの文字色。空（<see cref="Color.Empty"/>）の場合は ForeColor と同じ色。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("無効（Enabled = false）のときの文字色。空の場合は ForeColor と同じ色。")]
        public Color DisabledForeColor
        {
            get { return _disabledForeColor; }
            set { _disabledForeColor = value; Invalidate(); }
        }

        /// <summary>開いた一覧で、選択中の行の背景色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "RoyalBlue")]
        [Description("開いた一覧で、選択中の行の背景色。")]
        public Color SelectionColor
        {
            get { return _selectionColor; }
            set { _selectionColor = value; }
        }

        /// <summary>
        /// 開いた一覧で、選択中の行の文字色。空（<see cref="Color.Empty"/>）の場合は ForeColor と同じ色。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("開いた一覧で、選択中の行の文字色。空の場合は ForeColor と同じ色。")]
        public Color SelectionForeColor
        {
            get { return _selectionForeColor; }
            set { _selectionForeColor = value; }
        }

        /// <summary>枠の角の半径。96 DPI 基準のピクセルで指定する。</summary>
        [Category("Appearance")]
        [DefaultValue(4)]
        [Description("枠の角の半径。96 DPI 基準のピクセルで指定する。")]
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>
        /// 何も選択していないとき（選択中の項目の文字列が空のときも含む）に、薄い色で表示する案内の文字（プレースホルダー）。
        /// 「入力必須」などに使う。入力欄（<see cref="SEditBase.PlaceholderText"/>）と同じ見た目・同じ条件で表示する。
        /// 無効（Enabled = false）・読み取り専用（<see cref="ReadOnly"/>）のときは表示しない。選択の状態には影響しない。
        /// 一覧から選ぶ形（DropDownList。既定）のときだけ表示する。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("何も選択していないときに、薄い色で表示する案内の文字。")]
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

        /// <summary>
        /// 選択中の項目の文字列が、表示する幅に収まらないときに、収まる大きさまでフォントを縮めて表示するか。既定は true。
        /// 縮めるのは 8 ポイントまで（<see cref="SFittedFont.MinimumSize"/>）。それでも収まらないときは、末尾を「…」にする。
        /// 縮めるのは、閉じた状態の表示だけ（開いた一覧は、<see cref="Font"/> のまま）。
        /// false にすると、縮めずに、収まらない分の末尾を「…」にする。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("選択中の項目の文字列が収まらないときに、収まる大きさまでフォントを縮めて表示するか。")]
        public bool ShrinkTextToFit
        {
            get { return _shrinkTextToFit; }
            set
            {
                _shrinkTextToFit = value;
                Invalidate();
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

        #region キーとマウスの操作

        /// <summary>
        /// いまの状態で、<paramref name="keyCode"/> のキーをこのコントロールが使うか（<see cref="ISKeyUser"/>）。
        /// 一覧を開いている間は、Enter（指している項目の確定）・Esc・↑ ↓・PageUp / PageDown を使う。
        /// フォームは、これらのキーを先に処理しないこと（Enter で次の項目へ移る処理など）。
        /// </summary>
        public bool UsesKey(Keys keyCode)
        {
            if (!DroppedDown)
            {
                return false;
            }

            switch (keyCode)
            {
                case Keys.Enter:
                case Keys.Escape:
                case Keys.Up:
                case Keys.Down:
                case Keys.PageUp:
                case Keys.PageDown:
                    return true;
                default:
                    return false;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Handled)
            {
                return;
            }

            if (_readOnly)
            {
                // 選択を変えるキーと、一覧を開くキーを無効にする（Tab や Enter などは通す）
                if (IsSelectionKey(e.KeyCode))
                {
                    e.Handled = true;
                }
                return;
            }

            if (e.KeyCode == Keys.F4 && !e.Alt && !_openByF4Key)
            {
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Space && _openBySpaceKey && e.Modifiers == Keys.None)
            {
                DroppedDown = !DroppedDown;
                e.Handled = true;
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            // 標準のコンボ ボックスは、文字キーでその文字から始まる項目を選択する。
            // 読み取り専用のときはこれを止める。Space は一覧の開閉に使うので、項目の検索には使わせない
            if (_readOnly || (e.KeyChar == ' ' && _openBySpaceKey))
            {
                e.Handled = true;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (_readOnly)
            {
                // ホイールでの選択の変更を止める
                var handled = e as HandledMouseEventArgs;
                if (handled != null)
                {
                    handled.Handled = true;
                }
                return;
            }

            base.OnMouseWheel(e);
        }

        private static bool IsSelectionKey(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Home:
                case Keys.End:
                case Keys.F4:
                case Keys.Space:
                    return true;
                default:
                    return false;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (_readOnly && (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_LBUTTONDBLCLK))
            {
                // 読み取り専用のときは、クリックで一覧を開かせない。フォーカスだけ受け取る
                if (!Focused)
                {
                    Focus();
                }
                return;
            }

            base.WndProc(ref m);
        }

        #endregion

        #region 描画

        /// <summary>閉じた状態の見た目を、このコントロール自身がすべて描くか。</summary>
        private bool PaintsOwnSurface
        {
            get { return base.DropDownStyle == ComboBoxStyle.DropDownList; }
        }

        /// <summary>
        /// 描画の方式を切り替える。自分で描く場合は、標準の描画を止めて（UserPaint）、裏で描いてから一度に表示する。
        /// </summary>
        private void UpdatePaintMode()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, PaintsOwnSurface);
        }

        private void InvalidateOwnSurface()
        {
            if (PaintsOwnSurface)
            {
                Invalidate();
            }
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            base.ScaleControl(factor, specified);

            // 自前描画の ItemHeight は自動スケーリングの対象外なので、高さの拡大率に合わせて追従させる
            if ((specified & BoundsSpecified.Height) != 0 && factor.Height != 1f)
            {
                ItemHeight = Math.Max(1, (int)Math.Round(ItemHeight * factor.Height));
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // 自分で描く場合は OnPaint が全面を描くので、ここでは何もしない
            if (!PaintsOwnSurface)
            {
                base.OnPaintBackground(pevent);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // ここが呼ばれるのは、自分で描く方式（UserPaint）のときだけ
            if (PaintsOwnSurface)
            {
                PaintClosedSurface(e.Graphics);
            }
            base.OnPaint(e);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if ((e.State & DrawItemState.ComboBoxEdit) != 0)
            {
                // 閉じた状態の表示部分。Windows が選択やフォーカスの変化に合わせて、ここだけを描くよう求めてくることがある。
                // 表示部分は OnPaint が枠や矢印と一緒に描いているので、全体の描き直しを求めるだけにする
                InvalidateOwnSurface();
                return;
            }

            // 開いた一覧の 1 行
            bool selected = (e.State & DrawItemState.Selected) != 0;
            Color background = selected ? _selectionColor : BackColor;
            // 選択中の項目の文字の色は、指定があればその色、無ければ、選択の色の上で読める色（濃い色の上では白）
            Color foreground = !selected ? ForeColor
                : !_selectionForeColor.IsEmpty ? _selectionForeColor
                : SControlStyle.GetTextColorOn(_selectionColor, ForeColor);

            using (var backgroundBrush = new SolidBrush(background))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            if (e.Index >= 0)
            {
                // 閉じた状態の文字列と、左端をそろえる
                int inset = SControlStyle.GetTextInset(LayoutScale.Of(this), _borderStyle != BorderStyle.None);
                var textBounds = new Rectangle(e.Bounds.X + inset, e.Bounds.Y, Math.Max(0, e.Bounds.Width - inset), e.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, GetItemText(Items[e.Index]), Font, textBounds, foreground, ItemTextFlags);
            }
        }

        /// <summary>
        /// 閉じた状態の見た目をすべて描く。下から順に、親の描画内容（角丸の外側）、塗りと枠線、選択中の項目の文字列、矢印。
        /// </summary>
        private void PaintClosedSurface(Graphics g)
        {
            float scale = LayoutScale.Of(this);
            bool bordered = _borderStyle != BorderStyle.None;
            bool focused = Focused && Enabled;

            Color fill = !Enabled ? _disabledBackColor
                : focused && !_readOnly && !_focusBackColor.IsEmpty ? _focusBackColor
                : BackColor;
            Color textColor = Enabled || _disabledForeColor.IsEmpty ? ForeColor : _disabledForeColor;

            PaintHelper.PaintParentAppearance(this, g, InvokePaintBackground, InvokePaint);

            if (bordered)
            {
                SControlStyle.DrawFrame(
                    g, Width, Height, scale, fill,
                    focused ? _focusBorderColor : _borderColor,
                    SControlStyle.GetBorderWidth(scale, focused),
                    _cornerRadius);
            }
            else
            {
                using (var fillBrush = new SolidBrush(fill))
                {
                    g.FillRectangle(fillBrush, 0, 0, Width, Height);
                }
            }

            Rectangle arrowBounds = GetArrowBounds(scale);
            int inset = SControlStyle.GetTextInset(scale, bordered);
            bool rightToLeft = RightToLeft == RightToLeft.Yes;
            int textLeft = rightToLeft ? arrowBounds.Right : inset;
            int textRight = rightToLeft ? Width - inset : arrowBounds.Left;

            var textBounds = new Rectangle(textLeft, 0, Math.Max(0, textRight - textLeft), Height);
            string selectedText = SelectedIndex >= 0 ? GetItemText(Items[SelectedIndex]) : string.Empty;
            if (selectedText.Length > 0)
            {
                // 収まらないときは、収まる大きさまで縮めたフォントで描く（下限まで縮めても収まらなければ、末尾が「…」になる）
                Font textFont = _shrinkTextToFit ? _fittedFont.Get(selectedText, Font, textBounds.Width, ItemTextFlags) : Font;
                TextRenderer.DrawText(g, selectedText, textFont, textBounds, textColor, ItemTextFlags);
            }
            else if (ShouldShowPlaceholder)
            {
                // 選んだ項目の文字より小さいフォントで描く（入力欄のプレースホルダーと同じ大きさ）
                TextRenderer.DrawText(g, _placeholderText, _placeholderFont.Get(Font), textBounds, _placeholderColor, ItemTextFlags);
            }

            // 選択を変えられないときは、矢印を薄くする
            Color arrowColor = Enabled && !_readOnly ? textColor : SControlStyle.GetInactiveColor(textColor, fill);
            SControlStyle.DrawDropDownArrow(g, arrowBounds, arrowColor, scale);
        }

        /// <summary>
        /// いま、プレースホルダー（<see cref="PlaceholderText"/>）を表示する状態か（選択中の項目の文字列が空であることは、呼び出す側で確かめる）。
        /// 表示する条件は、文字が指定されていて、有効で、読み取り専用でないこと。
        /// </summary>
        private bool ShouldShowPlaceholder
        {
            get { return _placeholderText.Length > 0 && Enabled && !_readOnly; }
        }

        /// <summary>右端（右から左へ書く設定では左端）の、矢印を描く領域。</summary>
        private Rectangle GetArrowBounds(float scale)
        {
            Rectangle bounds = SControlStyle.GetDropDownButtonBounds(Width, Height, scale, _borderStyle != BorderStyle.None);
            if (RightToLeft == RightToLeft.Yes)
            {
                // 左右を入れ替える
                bounds.X = Width - bounds.Right;
            }
            return bounds;
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fittedFont.Dispose();
                _placeholderFont.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 状態の変化に合わせた描き直し

        // 自分で描く方式では、表示内容が変わるきっかけごとに描き直しを求める必要がある
        // （標準の描画では Windows が自動で描き直す）

        protected override void OnDropDownStyleChanged(EventArgs e)
        {
            base.OnDropDownStyleChanged(e);
            UpdatePaintMode();
            Invalidate();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            InvalidateOwnSurface();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            InvalidateOwnSurface();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            InvalidateOwnSurface();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            InvalidateOwnSurface();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            InvalidateOwnSurface();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            InvalidateOwnSurface();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            InvalidateOwnSurface();
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            InvalidateOwnSurface();
        }

        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);
            InvalidateOwnSurface();
        }

        protected override void OnDisplayMemberChanged(EventArgs e)
        {
            base.OnDisplayMemberChanged(e);
            InvalidateOwnSurface();
        }

        #endregion
    }

    /// <summary>
    /// <see cref="SComboBox"/> の項目。表示する文字列と、内部の値の組。
    /// </summary>
    public sealed class SComboItem
    {
        private readonly string _text;
        private readonly object _value;

        public SComboItem(string text, object value)
        {
            _text = text ?? string.Empty;
            _value = value;
        }

        /// <summary>一覧に表示する文字列。</summary>
        public string Text
        {
            get { return _text; }
        }

        /// <summary>内部の値。</summary>
        public object Value
        {
            get { return _value; }
        }

        /// <summary>表示する文字列を返す（コンボ ボックスは、項目の ToString() を一覧に表示する）。</summary>
        public override string ToString()
        {
            return _text;
        }
    }
}
