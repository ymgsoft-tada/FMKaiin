using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// テーブルの行を、見出し付き・複数列の一覧から選ぶコンボ ボックス。UcTableComboBox（中身は GcComboBoxEx）の代わりに使う。
    /// 文字を入力すると、その文字を含む行だけに一覧を絞り込む。
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>使い方</b>: <see cref="DataSource"/> にテーブルを設定し、<see cref="AddColumn(string, string, int)"/> で一覧に出す列を、
    /// <see cref="DisplayMember"/> で選択後に入力欄へ表示する列を、<see cref="ValueMember"/> で値として使う列を指定する。
    /// 選択の取得・設定は <see cref="SelectedValue"/> で行う（<see cref="SComboBox"/> と同じ名前）。
    /// </para>
    /// <para>
    /// <b>入力したときの動作</b>: 文字を入力すると選択が外れ、一覧に出している列のどれかにその文字を含む行だけが一覧に出る
    /// （1 文字のときは先頭が一致する行。ひらがな・カタカナ、全角・半角、大文字・小文字は区別しない）。
    /// Enter・一覧のクリック・フォーカスの移動で、指している行（指していなければ先頭の行）を選択し、入力欄をその行の文字列にする。
    /// 該当する行が無いままフォーカスが移ると、入力した文字列を消す（<see cref="FallbackSelectedIndex"/> を指定していればその行を選択する）。
    /// Esc で、入力を取り消して元の選択に戻す。
    /// 確定するまで <see cref="SelectedValue"/> は「未選択」のままなので、フォーカスを動かさずに値を読む処理
    /// （ファンクション キーでの保存など）は、読む前に <see cref="CommitEdit"/> を呼ぶこと。
    /// </para>
    /// <para>
    /// <b>キー操作</b>: ↑ ↓ PageUp PageDown で一覧を開く・指している行を移動する。Alt+↓ で開閉する。
    /// 文字を入力していなくても、一覧を開いて行を指した状態で Tab などでフォーカスを移すと、指している行を選択する。
    /// </para>
    /// <para>
    /// 見た目とフォーカス時の動作は <see cref="STextBox"/> と同じ（共通の土台 <see cref="SEditBase"/> による）。
    /// 入力した文字は検索に使うので、文字の種類は制限しない。
    /// 表示する文字列が空の行は、一覧に出さない（選択できない）。
    /// 行の取り出しと検索は <see cref="STableRowSource"/> が受け持ち、このクラスは選択の状態と操作を扱う。
    /// </para>
    /// </remarks>
    public class STableComboBox : SEditBase, ISEditCommit, ISKeyUser
    {
        private const string DefaultNoDataText = "選択できるデータがありません";
        private const string DefaultNoMatchText = "該当するデータがありません";

        private readonly STableRowSource _source = new STableRowSource();
        private readonly ReadOnlyCollection<STableComboColumn> _columnsView;

        // 一覧に表示している行。絞り込み中は、選択できる行の一部
        private IList<DataRow> _shownRows = new DataRow[0];

        private DataRow _selectedRow;
        // テーブルや一覧の設定が変わり、選択中の行がまだ一覧にあるかを確かめる必要がある間 true（EnsureSelectionAvailable）
        private bool _selectionCheckPending;

        // 利用者が文字を入力してから、選択を確定するまでの間 true
        private bool _searching;
        // 入力を始める前に選択していた行（Esc で戻すため）
        private DataRow _rowBeforeSearch;
        // このコントロール自身が Text を書き換えている間 true（利用者の入力と区別するため）
        private bool _settingText;

        private STablePopup _popup;
        private int _maxDropDownItems = 8;
        private int _dropDownWidth;
        private bool _headerVisible = true;
        private int _fallbackSelectedIndex = -1;
        private Color _selectionColor = SControlStyle.SelectionBackColor;
        private string _noDataText = DefaultNoDataText;
        private string _noMatchText = DefaultNoMatchText;

        public STableComboBox()
        {
            _columnsView = new ReadOnlyCollection<STableComboColumn>(_source.Columns);
            _source.Changed += OnSourceChanged;
            base.HighlightText = true;
        }

        #region イベント

        /// <summary>選択している行が変わったときに発生する（利用者の操作でも、プログラムからの設定でも）。</summary>
        [Category("Behavior")]
        [Description("選択している行が変わったときに発生する。")]
        public event EventHandler SelectedIndexChanged;

        /// <summary>選択している行が変わったときに、<see cref="SelectedIndexChanged"/> に続いて発生する。</summary>
        [Category("Behavior")]
        [Description("選択している行が変わったときに、SelectedIndexChanged に続いて発生する。")]
        public event EventHandler SelectedValueChanged;

        /// <summary>一覧を開いたときに発生する。</summary>
        [Category("Behavior")]
        [Description("一覧を開いたときに発生する。")]
        public event EventHandler DropDown;

        /// <summary>一覧を閉じたときに発生する。</summary>
        [Category("Behavior")]
        [Description("一覧を閉じたときに発生する。")]
        public event EventHandler DropDownClosed;

        #endregion

        #region データと列

        /// <summary>
        /// 一覧のもとになるテーブル。行の追加・変更・削除は、一覧に自動で反映される。
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTable DataSource
        {
            get { return _source.Table; }
            set
            {
                _source.Table = value;
                DropUnavailableSelection();
            }
        }

        /// <summary>
        /// 一覧に出す行の条件（<see cref="DataView.RowFilter"/> と同じ書き方）。空文字列は条件なし。
        /// 選択中の行が条件から外れた場合は、選択が外れる（入力欄の文字列は残る）。
        /// </summary>
        [Category("Data")]
        [DefaultValue("")]
        [Description("一覧に出す行の条件（DataView.RowFilter と同じ書き方）。空文字列は条件なし。")]
        public string RowFilter
        {
            get { return _source.RowFilter; }
            set
            {
                _source.RowFilter = value;
                DropUnavailableSelection();
            }
        }

        /// <summary>一覧の並び順（<see cref="DataView.Sort"/> と同じ書き方）。空文字列はテーブルの順。</summary>
        [Category("Data")]
        [DefaultValue("")]
        [Description("一覧の並び順（DataView.Sort と同じ書き方）。空文字列はテーブルの順。")]
        public string Sort
        {
            get { return _source.Sort; }
            set { _source.Sort = value; }
        }

        /// <summary>
        /// 選択した行の、入力欄に表示する列の名前。空文字列の場合は、一覧の最初の列。
        /// </summary>
        [Category("Data")]
        [DefaultValue("")]
        [Description("選択した行の、入力欄に表示する列の名前。空文字列の場合は、一覧の最初の列。")]
        public string DisplayMember
        {
            get { return _source.DisplayMember; }
            set
            {
                _source.DisplayMember = value;
                DropUnavailableSelection();
            }
        }

        /// <summary>
        /// <see cref="SelectedValue"/> で値として使う列の名前。
        /// </summary>
        [Category("Data")]
        [DefaultValue("")]
        [Description("SelectedValue で値として使う列の名前。")]
        public string ValueMember
        {
            get { return _source.ValueMember; }
            set { _source.ValueMember = value; }
        }

        /// <summary>一覧に出す列。追加は <see cref="AddColumn(string, string, int)"/> で行う。</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<STableComboColumn> Columns
        {
            get { return _columnsView; }
        }

        /// <summary>
        /// 一覧に出す列を追加する（文字は左寄せ）。追加した順に、左から並ぶ。
        /// </summary>
        /// <param name="dataPropertyName">テーブルの列の名前。</param>
        /// <param name="headerText">見出しの文字列。</param>
        /// <param name="width">列の幅。96 DPI 基準のピクセル。</param>
        public STableComboColumn AddColumn(string dataPropertyName, string headerText, int width)
        {
            return AddColumn(dataPropertyName, headerText, width, HorizontalAlignment.Left);
        }

        /// <summary>
        /// 一覧に出す列を追加する。追加した順に、左から並ぶ。
        /// </summary>
        /// <param name="dataPropertyName">テーブルの列の名前。</param>
        /// <param name="headerText">見出しの文字列。</param>
        /// <param name="width">列の幅。96 DPI 基準のピクセル。</param>
        /// <param name="textAlign">セルの文字の位置。</param>
        public STableComboColumn AddColumn(string dataPropertyName, string headerText, int width, HorizontalAlignment textAlign)
        {
            var column = new STableComboColumn(dataPropertyName, headerText, width, textAlign);
            _source.AddColumn(column);
            DropUnavailableSelection();
            return column;
        }

        /// <summary>一覧に出す列を、すべて取り除く。</summary>
        public void ClearColumns()
        {
            _source.ClearColumns();
            DropUnavailableSelection();
        }

        /// <summary>
        /// テーブルの行、または一覧の設定が変わった。選択中の行が一覧から外れていないかを、後で確かめるようにしておく。
        /// </summary>
        /// <remarks>
        /// ここですぐに確かめない理由は 2 つある。テーブルを一括で読み込むと 1 行ごとに呼ばれるので、そのたびに行を探すと遅くなること。
        /// また、ここはテーブルが変更を通知している最中なので、ここで選択の変更イベントを発生させると、
        /// イベントを受けた側がテーブルを書き換えたときに、変更の途中のテーブルを触ることになること。
        /// </remarks>
        private void OnSourceChanged(object sender, EventArgs e)
        {
            RequestSelectionCheck();
            RefreshPopupRows();
        }

        /// <summary>
        /// 選択中の行がまだ一覧にあるかを、後で（メッセージの処理が一段落したときに）確かめるよう予約する。
        /// それより前に選択が読まれた場合は、読まれた時点で確かめる（<see cref="EnsureSelectionAvailable"/>）。
        /// </summary>
        private void RequestSelectionCheck()
        {
            if (_selectedRow == null || _selectionCheckPending)
            {
                return;
            }

            _selectionCheckPending = true;
            if (IsHandleCreated)
            {
                BeginInvoke((MethodInvoker)EnsureSelectionAvailable);
            }
        }

        /// <summary>
        /// 確認が予約されていれば、選択中の行がまだ一覧にあるかを確かめる。削除された・条件（RowFilter）から外れた・
        /// 表示する文字列が空になった、などで一覧から外れていたら、選択を外して、選択の変更イベントを発生させる。入力欄の文字列は残す。
        /// </summary>
        private void EnsureSelectionAvailable()
        {
            if (!_selectionCheckPending)
            {
                return;
            }
            _selectionCheckPending = false;

            if (!IsDisposed && _selectedRow != null && _source.IndexOf(_selectedRow) < 0)
            {
                SetSelectedRow(null, false);
            }
        }

        /// <summary>
        /// 選択していた行が一覧から外れていたら、選択を外す（一覧の設定をプログラムから変えた直後に呼ぶ）。入力欄の文字列は残す。
        /// </summary>
        private void DropUnavailableSelection()
        {
            _selectionCheckPending = _selectedRow != null;
            EnsureSelectionAvailable();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // ウィンドウができる前に予約された確認は、まだ実行されていない
            if (_selectionCheckPending)
            {
                BeginInvoke((MethodInvoker)EnsureSelectionAvailable);
            }
        }

        /// <summary>一覧に実際に出す列（<see cref="STablePopup"/> が使う）。</summary>
        internal IList<STableComboColumn> PopupColumns
        {
            get { return _source.VisibleColumns; }
        }

        /// <summary><paramref name="row"/> の <paramref name="columnName"/> 列の、表示用の文字列（<see cref="STablePopup"/> が使う）。</summary>
        internal string GetCellText(DataRow row, string columnName)
        {
            return _source.GetCellText(row, columnName);
        }

        #endregion

        #region 選択

        /// <summary>
        /// 選択中の行の、<see cref="ValueMember"/> の列の値。何も選択していなければ null。
        /// 設定すると、その値を持つ行を選択する。該当する行が無い場合と、null を設定した場合は、未選択にして入力欄を空にする。
        /// </summary>
        /// <remarks>値の比較は <see cref="SComboBox.SelectedValue"/> と同じ（型が違っても、文字列にして同じなら一致）。</remarks>
        [Browsable(false)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedValue
        {
            get { return _source.GetValue(SelectedRow); }
            set { SelectByValue(value); }
        }

        /// <summary>選択中の行。何も選択していなければ null。</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataRow SelectedRow
        {
            get
            {
                // テーブルが変わった後、まだ確かめていなければ、ここで確かめる（一覧から外れていれば、イベントを発生させて選択を外す）
                EnsureSelectionAvailable();
                return _selectedRow;
            }
        }

        /// <summary>
        /// 選択中の行の位置（一覧を絞り込んでいないときの、上からの順番）。何も選択していなければ -1。
        /// 設定すると、その位置の行を選択する。範囲外の値を設定すると、未選択にする。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get { return _source.IndexOf(SelectedRow); }
            set { SelectRow(GetRowAt(value)); }
        }

        /// <summary>選択できる行の数（一覧を絞り込んでいないときの行数）。</summary>
        [Browsable(false)]
        public int RowCount
        {
            get { return _source.Rows.Count; }
        }

        /// <summary>
        /// <paramref name="index"/> の位置の行の、<see cref="ValueMember"/> の列の値を返す。範囲外なら null。
        /// </summary>
        public object GetValue(int index)
        {
            return _source.GetValue(GetRowAt(index));
        }

        /// <summary>
        /// <paramref name="index"/> の位置の行の、入力欄に表示する文字列を返す。範囲外なら null。
        /// </summary>
        public string GetText(int index)
        {
            DataRow row = GetRowAt(index);
            return row == null ? null : _source.GetDisplayText(row);
        }

        /// <summary>
        /// <see cref="ValueMember"/> の列の値が <paramref name="value"/> の行を選択する。
        /// 該当する行が無い場合と、null を渡した場合は、未選択にして入力欄を空にする。
        /// </summary>
        /// <returns>該当する行があったか。</returns>
        public bool SelectByValue(object value)
        {
            DataRow found = _source.FindByValue(value);
            SelectRow(found);
            return found != null;
        }

        /// <summary>
        /// 入力欄に表示する文字列が <paramref name="text"/> と一致する行を選択する。
        /// 該当する行が無い場合は、未選択にして入力欄を空にする。
        /// </summary>
        /// <returns>該当する行があったか。</returns>
        public bool SelectByText(string text)
        {
            DataRow found = _source.FindByText(text);
            SelectRow(found);
            return found != null;
        }

        /// <summary>
        /// 入力欄の文字列。設定すると、表示する文字列が一致する行を選択する。
        /// 該当する行が無い場合は、未選択にして、渡された文字列をそのまま表示する。
        /// </summary>
        public override string Text
        {
            get { return base.Text; }
            set
            {
                if (_settingText)
                {
                    base.Text = value;
                    return;
                }

                DataRow found = _source.FindByText(value);
                if (found != null)
                {
                    SelectRow(found);
                }
                else
                {
                    EndSearch();
                    SetSelectedRow(null, false);
                    SetTextInternal(value ?? string.Empty);
                }
            }
        }

        private DataRow GetRowAt(int index)
        {
            IList<DataRow> rows = _source.Rows;
            return index >= 0 && index < rows.Count ? rows[index] : null;
        }

        /// <summary>
        /// <paramref name="row"/> を選択して、入力欄をその行の文字列にする（null なら未選択にして空にする）。
        /// 入力の途中だった場合は、入力を終わらせる。
        /// </summary>
        private void SelectRow(DataRow row)
        {
            EndSearch();
            SetSelectedRow(row, true);
        }

        /// <summary>利用者の操作で選択を確定する。<see cref="SelectRow"/> に加えて、入力欄を全選択にする。</summary>
        private void CommitRow(DataRow row)
        {
            SelectRow(row);
            if (Focused)
            {
                SelectAll();
            }
        }

        private void EndSearch()
        {
            _searching = false;
            _rowBeforeSearch = null;
            ClosePopup();
        }

        private void SetSelectedRow(DataRow row, bool updateText)
        {
            bool changed = !ReferenceEquals(_selectedRow, row);
            _selectedRow = row;

            if (updateText)
            {
                SetTextInternal(_source.GetDisplayText(row));
            }

            if (changed)
            {
                OnSelectedIndexChanged(EventArgs.Empty);
                OnSelectedValueChanged(EventArgs.Empty);
            }
        }

        private void SetTextInternal(string text)
        {
            _settingText = true;
            try
            {
                base.Text = text;
            }
            finally
            {
                _settingText = false;
            }
        }

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            EventHandler handler = SelectedIndexChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSelectedValueChanged(EventArgs e)
        {
            EventHandler handler = SelectedValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region 一覧のプロパティ

        /// <summary>一覧に一度に表示する行数の上限。これより多いときはスクロールする。</summary>
        [Category("Behavior")]
        [DefaultValue(8)]
        [Description("一覧に一度に表示する行数の上限。これより多いときはスクロールする。")]
        public int MaxDropDownItems
        {
            get { return _maxDropDownItems; }
            set { _maxDropDownItems = Math.Max(1, value); }
        }

        /// <summary>
        /// 一覧の幅。96 DPI 基準のピクセル。0 の場合は、列の幅の合計とこのコントロールの幅の、広いほうに合わせる。
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(0)]
        [Description("一覧の幅。96 DPI 基準のピクセル。0 の場合は、列の幅の合計とこのコントロールの幅の、広いほうに合わせる。")]
        public int DropDownWidth
        {
            get { return _dropDownWidth; }
            set { _dropDownWidth = Math.Max(0, value); }
        }

        /// <summary>一覧に列の見出しを表示するか。</summary>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("一覧に列の見出しを表示するか。")]
        public bool HeaderVisible
        {
            get { return _headerVisible; }
            set { _headerVisible = value; }
        }

        /// <summary>
        /// 何も選択しないままフォーカスが移ったときに、代わりに選択する行の位置。-1 の場合は、未選択のまま入力欄を空にする。
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(-1)]
        [Description("何も選択しないままフォーカスが移ったときに、代わりに選択する行の位置。-1 の場合は、未選択のまま入力欄を空にする。")]
        public int FallbackSelectedIndex
        {
            get { return _fallbackSelectedIndex; }
            set { _fallbackSelectedIndex = Math.Max(-1, value); }
        }

        /// <summary>
        /// 選択できる行が 1 つも無いときに、一覧を開こうとすると表示する文字列（矢印のクリックや ↓ で、何も起きないように見えるのを防ぐ）。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(DefaultNoDataText)]
        [Description("選択できる行が 1 つも無いときに、一覧を開こうとすると表示する文字列。")]
        public string NoDataText
        {
            get { return _noDataText; }
            set { _noDataText = value ?? string.Empty; }
        }

        /// <summary>
        /// 入力した文字に当てはまる行が無いときに、一覧を開こうとすると表示する文字列。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(DefaultNoMatchText)]
        [Description("入力した文字に当てはまる行が無いときに、一覧を開こうとすると表示する文字列。")]
        public string NoMatchText
        {
            get { return _noMatchText; }
            set { _noMatchText = value ?? string.Empty; }
        }

        /// <summary>一覧で、指している行の背景色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "RoyalBlue")]
        [Description("一覧で、指している行の背景色。")]
        public Color SelectionColor
        {
            get { return _selectionColor; }
            set { _selectionColor = value; }
        }

        /// <summary>
        /// フォーカスが移ってきたときに、文字列を全選択するか。このコントロールでは、続けて入力すると検索し直せるよう、既定を true にしている。
        /// </summary>
        [DefaultValue(true)]
        public override bool HighlightText
        {
            get { return base.HighlightText; }
            set { base.HighlightText = value; }
        }

        /// <summary>一覧の背景色。フォーカスがあるときの背景色（FocusBackColor）には連動させず、設定してある背景色を使う。</summary>
        internal Color ListBackColor
        {
            get { return ReadOnly ? SystemColors.Window : NormalBackColor; }
        }

        #endregion

        #region 入力による絞り込み

        protected override void OnTextChanged(EventArgs e)
        {
            if (!_settingText)
            {
                OnUserEdited();
            }
            base.OnTextChanged(e);
        }

        /// <summary>
        /// 利用者が入力欄の文字列を変えたとき（キー入力・IME の確定・貼り付け・削除）の処理。
        /// 選択を外し、入力した文字列で一覧を絞り込んで表示する。
        /// </summary>
        private void OnUserEdited()
        {
            if (!_searching)
            {
                _searching = true;
                _rowBeforeSearch = _selectedRow;
            }
            SetSelectedRow(null, false);

            // フォーカスが無いとき（プログラムからの文字の挿入など）は、一覧を出さないので検索もしない
            IList<DataRow> matches = Focused && TextLength > 0 ? _source.Search(base.Text) : null;
            if (matches == null || matches.Count == 0)
            {
                ClosePopup();
                return;
            }

            // 先頭の行を指しておく。このまま Enter やフォーカスの移動で確定すると、その行が選択される
            ShowPopup(matches, 0);
        }

        /// <summary>いま一覧に出すべき行。入力の途中なら絞り込んだ行、そうでなければ選択できるすべての行。</summary>
        private IList<DataRow> GetRowsToShow()
        {
            return _searching && TextLength > 0 ? _source.Search(base.Text) : _source.Rows;
        }

        /// <summary>
        /// 確定したときに選択される行。一覧で指している行、指していなければ絞り込んだ結果の先頭の行。該当する行が無ければ null。
        /// </summary>
        private DataRow GetCandidateRow()
        {
            if (IsPopupOpen)
            {
                int index = _popup.HighlightIndex;
                if (index >= 0 && index < _shownRows.Count)
                {
                    return _shownRows[index];
                }
                return _searching && _shownRows.Count > 0 ? _shownRows[0] : null;
            }

            if (_searching && TextLength > 0)
            {
                List<DataRow> matches = _source.Search(base.Text);
                return matches.Count > 0 ? matches[0] : null;
            }
            return null;
        }

        /// <summary>入力を取り消して、入力を始める前の選択に戻す。</summary>
        private void CancelSearch()
        {
            if (!_searching)
            {
                ClosePopup();
                return;
            }

            DataRow before = _rowBeforeSearch;
            CommitRow(_source.IndexOf(before) >= 0 ? before : null);
        }

        #endregion

        #region 一覧の開閉

        /// <summary>
        /// 一覧を開いているか。設定して開閉することもできる（選択できる行が無いとき、読み取り専用・無効のときは開かない）。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DroppedDown
        {
            get { return IsPopupOpen; }
            set
            {
                if (value)
                {
                    OpenPopup();
                }
                else
                {
                    ClosePopup();
                }
            }
        }

        private bool IsPopupOpen
        {
            get { return _popup != null && !_popup.IsDisposed && _popup.Visible; }
        }

        /// <summary>
        /// 一覧を開く。入力の途中なら絞り込んだ行を、そうでなければすべての行を表示し、選択中の行を指しておく。
        /// </summary>
        private void OpenPopup()
        {
            if (IsPopupOpen)
            {
                return;
            }

            IList<DataRow> rows = GetRowsToShow();
            if (rows.Count == 0)
            {
                // 表示する行が無い。何も起きないと、開かない理由が分からないので、理由を書いた一覧を開く
                ShowPopup(rows, -1);
                return;
            }

            // 絞り込んでいないときは、選択中の行の位置をそのまま使える
            int highlight = _searching ? 0 : _source.IndexOf(_selectedRow);
            ShowPopup(rows, highlight);
        }

        /// <summary>
        /// 表示する行が無いときに、行の代わりに一覧に表示する文字列。
        /// 入力した文字で絞り込んだ結果が無い場合は <see cref="NoMatchText"/>、選択できる行がそもそも無い場合は <see cref="NoDataText"/>。
        /// </summary>
        private string EmptyMessage
        {
            get { return _searching && TextLength > 0 ? _noMatchText : _noDataText; }
        }

        private void ShowPopup(IList<DataRow> rows, int highlightIndex)
        {
            if (!IsHandleCreated || !Visible || !Enabled || ReadOnly || DesignMode)
            {
                return;
            }

            if (_popup == null || _popup.IsDisposed)
            {
                _popup = new STablePopup(this);
                _popup.CloseRequested += delegate { ClosePopup(); };
                _popup.PopupClosed += delegate { OnPopupClosed(); };
            }

            bool wasOpen = _popup.Visible;
            _shownRows = rows;
            _popup.ShowRows(rows, highlightIndex, EmptyMessage);

            if (!wasOpen)
            {
                EventHandler handler = DropDown;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        private void ClosePopup()
        {
            if (IsPopupOpen)
            {
                _popup.Hide();
            }
        }

        /// <summary>表示中の一覧の内容を、いまのデータに合わせて作り直す（一覧を開いている間にテーブルが変わったとき）。</summary>
        private void RefreshPopupRows()
        {
            if (!IsPopupOpen)
            {
                return;
            }

            // 指していた行を覚えておき、作り直した後も同じ行を指す
            DataRow highlighted = _popup.HighlightIndex >= 0 && _popup.HighlightIndex < _shownRows.Count
                ? _shownRows[_popup.HighlightIndex]
                : null;

            // 行が 1 つも無くなった場合は、その理由を表示した一覧になる
            IList<DataRow> rows = GetRowsToShow();
            ShowPopup(rows, highlighted == null ? -1 : rows.IndexOf(highlighted));
        }

        /// <summary>一覧の行がクリックされた（<see cref="STablePopup"/> から呼ばれる）。</summary>
        internal void CommitPopupRow(int index)
        {
            if (index >= 0 && index < _shownRows.Count)
            {
                CommitRow(_shownRows[index]);
            }
        }

        /// <summary>一覧が閉じた。</summary>
        private void OnPopupClosed()
        {
            // 閉じた後は使わないので、行の一覧を手放す（絞り込み結果を抱えたままにしない）
            _shownRows = new DataRow[0];

            EventHandler handler = DropDownClosed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void MoveHighlight(int delta)
        {
            int count = _shownRows.Count;
            if (count == 0)
            {
                return;
            }

            int index = _popup.HighlightIndex;
            if (index < 0)
            {
                index = delta > 0 ? 0 : count - 1;
            }
            else
            {
                index = Math.Max(0, Math.Min(index + delta, count - 1));
            }
            _popup.HighlightIndex = index;
        }

        #endregion

        #region キーとマウスの操作

        /// <summary>
        /// いまの状態で、<paramref name="keyCode"/> のキーをこのコントロールが使うか（<see cref="ISKeyUser"/>）。
        /// 一覧を開いている間は、Enter（指している行の確定）・Esc・↑ ↓・PageUp / PageDown を使う。入力の途中は、Esc（入力の取り消し）を使う。
        /// フォームは、これらのキーを先に処理しないこと（Enter で次の項目へ移る処理など）。
        /// </summary>
        public bool UsesKey(Keys keyCode)
        {
            if (ReadOnly)
            {
                return false;
            }

            switch (keyCode)
            {
                case Keys.Enter:
                case Keys.Up:
                case Keys.Down:
                case Keys.PageUp:
                case Keys.PageDown:
                    return IsPopupOpen;
                case Keys.Escape:
                    return IsPopupOpen || _searching;
                default:
                    return false;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Handled || ReadOnly)
            {
                return;
            }

            bool open = IsPopupOpen;
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.PageDown:
                case Keys.PageUp:
                    if (e.Alt)
                    {
                        if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                        {
                            DroppedDown = !open;
                            e.Handled = true;
                        }
                    }
                    else if (!open)
                    {
                        OpenPopup();
                        e.Handled = true;
                    }
                    else
                    {
                        bool down = e.KeyCode == Keys.Down || e.KeyCode == Keys.PageDown;
                        bool page = e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp;
                        MoveHighlight((down ? 1 : -1) * (page ? _popup.PageSize : 1));
                        e.Handled = true;
                    }
                    break;

                case Keys.Enter:
                    if (open)
                    {
                        DataRow candidate = GetCandidateRow();
                        if (candidate != null)
                        {
                            CommitRow(candidate);
                        }
                        else
                        {
                            ClosePopup();
                        }

                        // 一覧の確定に使った Enter を、フォームの処理（次の項目への移動など）に渡さない
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;

                case Keys.Escape:
                    if (open || _searching)
                    {
                        CancelSearch();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (IsPopupOpen)
            {
                _popup.ScrollBy(-Math.Sign(e.Delta) * Math.Max(1, SystemInformation.MouseWheelScrollLines));

                var handled = e as HandledMouseEventArgs;
                if (handled != null)
                {
                    handled.Handled = true;
                }
                return;
            }

            base.OnMouseWheel(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            // ほかのコントロールの Validating より前に、選択を確定しておく
            CommitEdit();
            base.OnLeave(e);
        }

        /// <summary>
        /// 入力の途中の内容を確定する。フォーカスがほかのコントロールへ移ったときと同じ処理（フォーカスは動かさない）。
        /// 入力の途中なら候補の行を、一覧を開いて行を指していればその行を選択し、入力欄をその行の文字列にする。
        /// 何も選択していなければ、<see cref="FallbackSelectedIndex"/> の行を選択するか、入力欄を空にする。
        /// </summary>
        /// <remarks>
        /// フォーカスを動かさずに値を読む処理（ファンクション キーでの保存など）が、読む前に呼ぶ。
        /// </remarks>
        public void CommitEdit()
        {
            if (ReadOnly)
            {
                ClosePopup();
                return;
            }

            DataRow row = GetRowToCommit();
            if (row == null)
            {
                row = GetRowAt(_fallbackSelectedIndex);
            }
            SelectRow(row);
        }

        /// <summary>確定したときに選択する行。該当する行が無ければ null。</summary>
        private DataRow GetRowToCommit()
        {
            if (_searching)
            {
                return GetCandidateRow();
            }

            // 文字を入力していなくても、一覧を開いて矢印キーなどで行を指していれば、その行を選ぶ（標準のコンボ ボックスと同じ）。
            // 一覧の外をクリックした場合は、ここへ来る前に一覧が閉じているので、選択は変わらない
            if (IsPopupOpen)
            {
                int index = _popup.HighlightIndex;
                if (index >= 0 && index < _shownRows.Count)
                {
                    return _shownRows[index];
                }
            }
            return SelectedRow;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            // ほかのアプリへ切り替えた場合など。入力の途中の状態は残し、一覧だけ閉じる
            ClosePopup();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (!Enabled)
            {
                ClosePopup();
            }
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            base.OnReadOnlyChanged(e);
            if (ReadOnly)
            {
                ClosePopup();
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (!Visible)
            {
                ClosePopup();
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ClosePopup();
            base.OnHandleDestroyed(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_popup != null)
                {
                    _popup.Dispose();
                    _popup = null;
                }
                _source.Changed -= OnSourceChanged;
                _source.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region 一覧を開く矢印（サイド ボタン）

        protected override int GetSideButtonWidth(float scale)
        {
            return SControlStyle.GetDropDownButtonWidth(scale);
        }

        protected override void PaintSideButton(Graphics g, Rectangle bounds, Color backColor)
        {
            // 一覧を開けないときは、矢印を薄くする
            Color color = Enabled && !ReadOnly ? ForeColor : SControlStyle.GetInactiveColor(ForeColor, backColor);
            SControlStyle.DrawDropDownArrow(g, bounds, color, LayoutScale.Of(this));
        }

        protected override void OnSideButtonClick(EventArgs e)
        {
            if (!ReadOnly)
            {
                DroppedDown = !IsPopupOpen;
            }
        }

        #endregion
    }

    /// <summary>
    /// <see cref="STableComboBox"/> の一覧に出す列。
    /// </summary>
    public sealed class STableComboColumn
    {
        private readonly string _dataPropertyName;
        private readonly string _headerText;
        private readonly int _width;
        private readonly HorizontalAlignment _textAlign;

        public STableComboColumn(string dataPropertyName, string headerText, int width, HorizontalAlignment textAlign)
        {
            _dataPropertyName = dataPropertyName ?? string.Empty;
            _headerText = headerText ?? string.Empty;
            _width = Math.Max(0, width);
            _textAlign = textAlign;
        }

        /// <summary>テーブルの列の名前。</summary>
        public string DataPropertyName
        {
            get { return _dataPropertyName; }
        }

        /// <summary>見出しの文字列。</summary>
        public string HeaderText
        {
            get { return _headerText; }
        }

        /// <summary>列の幅。96 DPI 基準のピクセル。最後の列は、一覧の幅に余りがあれば広がる。</summary>
        public int Width
        {
            get { return _width; }
        }

        /// <summary>セルの文字の位置。</summary>
        public HorizontalAlignment TextAlign
        {
            get { return _textAlign; }
        }
    }
}
