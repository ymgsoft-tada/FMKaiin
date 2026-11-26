using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using App.Controls;
using ComponentDB;
using ComponentDebug;

namespace App
{
    /// <summary>
    /// 画面のコントロールと、行（1 件分のデータ）の間で、値を受け渡す。GControlDB の代わりに、S 系のコントロールで使う。
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>使い方</b>（GControlDB と同じ流れ）:
    /// </para>
    /// <code>
    /// sctl = new SControlDB(this, getDataRow, AppGlobal.DB.DBZipCode);
    /// sctl.Add(new SControlDBText(t_basic.FBAS_NameDaihyo, iNameDaihyo));
    /// sctl.Add(new SControlDBCombo(t_basic.FID_Bank, iBank));
    /// sctl.EndAdd(rule);      // rule は入力ルールの表（SControlDBRule）。無ければ null
    /// sctl.FetchAll();        // 行の値を、コントロールに表示する
    /// </code>
    /// <para>
    /// 登録が済むと、コントロールからフォーカスが外れるたびに（チェック ボックスはオン・オフのたびに）、
    /// そのコントロールの値を行へ書き戻す。行は、コンストラクターに渡した関数で、必要になるたびに取得する。
    /// </para>
    /// <para>
    /// 行に対して行うのは、項目名での値の読み書きだけ（<see cref="ISRow"/>）。DataRow を返す関数を渡した場合は、中で包んで使う。
    /// </para>
    /// </remarks>
    public class SControlDB
    {
        /// <summary>行を取得する関数。行が無いときは null を返す。</summary>
        public delegate ISRow GetRow();

        /// <summary>行（DataRow）を取得する関数。行が無いときは null を返す。</summary>
        public delegate DataRow GetDataRow();

        /// <summary>登録したコントロール 1 つ分の、元の状態。</summary>
        private sealed class ControlState
        {
            public Color BackColor;
            public bool TabStop;
            public bool ReadOnly;
        }

        private readonly Form _form;
        private readonly GetRow _getRow;
        private readonly DBZipCode _zipCode;

        private readonly List<SControlDBItem> _items = new List<SControlDBItem>();
        private readonly List<Control> _controls = new List<Control>();
        private readonly Dictionary<Control, ControlState> _states = new Dictionary<Control, ControlState>();

        private bool _endAdd;

        // 読み込みの最中は true。この間は書き戻しをしない
        // （読み込みでコントロールの値が変わったことをきっかけに、書き戻しが走るのを防ぐ）
        private bool _fetching;

        /// <param name="form">コントロールを置いているフォーム。</param>
        /// <param name="getRow">行を取得する関数。</param>
        public SControlDB(Form form, GetRow getRow)
            : this(form, getRow, null)
        {
        }

        /// <param name="form">コントロールを置いているフォーム。</param>
        /// <param name="getRow">行を取得する関数。</param>
        /// <param name="zipCode">郵便番号から住所を引くためのもの。<see cref="SControlDBPostAddr"/> を使わなければ null でよい。</param>
        public SControlDB(Form form, GetRow getRow, DBZipCode zipCode)
        {
            if (getRow == null)
            {
                throw new ArgumentNullException(nameof(getRow));
            }

            _form = form;
            _getRow = getRow;
            _zipCode = zipCode;
        }

        /// <param name="form">コントロールを置いているフォーム。</param>
        /// <param name="getDataRow">行（DataRow）を取得する関数。</param>
        public SControlDB(Form form, GetDataRow getDataRow)
            : this(form, getDataRow, null)
        {
        }

        /// <param name="form">コントロールを置いているフォーム。</param>
        /// <param name="getDataRow">行（DataRow）を取得する関数。</param>
        /// <param name="zipCode">郵便番号から住所を引くためのもの。</param>
        public SControlDB(Form form, GetDataRow getDataRow, DBZipCode zipCode)
            : this(form, Wrap(getDataRow), zipCode)
        {
        }

        private static GetRow Wrap(GetDataRow getDataRow)
        {
            if (getDataRow == null)
            {
                throw new ArgumentNullException(nameof(getDataRow));
            }
            return delegate { return SDataRow.Wrap(getDataRow()); };
        }

        /// <summary>行の値を書き換えたときに発生する（どの項目の対応で書き換えた場合でも）。</summary>
        public event SControlDBFieldUpdatedHandler FieldUpdated;

        /// <summary>登録したコントロール。<see cref="EndAdd()"/> の後で使える。</summary>
        public IList<Control> ControlList
        {
            get { return _controls.AsReadOnly(); }
        }

        /// <summary><see cref="EndAdd()"/> を呼び終えたか。</summary>
        public bool EndAddFlg
        {
            get { return _endAdd; }
        }

        /// <summary>登録した対応。</summary>
        internal IList<SControlDBItem> Items
        {
            get { return _items; }
        }

        /// <summary>郵便番号から住所を引くためのもの。指定されていなければ null。</summary>
        internal DBZipCode ZipCode
        {
            get { return _zipCode; }
        }

        #region 登録

        /// <summary>
        /// 項目とコントロールの対応を登録する。すべて登録したら <see cref="EndAdd()"/> を呼ぶこと。
        /// </summary>
        public void Add(SControlDBItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (_endAdd)
            {
                throw new InvalidOperationException("EndAdd の後で、対応を追加することはできません。");
            }

            item.FieldUpdated += OnItemFieldUpdated;
            _items.Add(item);
        }

        /// <summary>
        /// 登録を終えて、使える状態にする（入力ルールは設定しない）。
        /// </summary>
        public void EndAdd()
        {
            EndAdd(null);
        }

        /// <summary>
        /// 登録を終えて、使える状態にする。登録したコントロールに入力ルールを設定し、フォーカスが外れたときの書き戻しを始める。
        /// </summary>
        /// <param name="rule">入力ルールの表。null なら設定しない。</param>
        public void EndAdd(SControlDBRule rule)
        {
            if (_endAdd)
            {
                throw new InvalidOperationException("EndAdd は 1 回だけ呼んでください。");
            }
            _endAdd = true;

            // 同じコントロールが複数の対応に入っていることがあるので、重複を除いて覚える
            foreach (SControlDBItem item in _items)
            {
                foreach (Control control in item.Ctls)
                {
                    if (!_states.ContainsKey(control))
                    {
                        _controls.Add(control);
                        // S 系の入力欄は、フォーカスがある間、BackColor がフォーカス時の色を返す。元の色（NormalBackColor）を覚える
                        var edit = control as SEditBase;
                        Color backColor = edit != null ? edit.NormalBackColor : control.BackColor;
                        _states.Add(control, new ControlState { BackColor = backColor, TabStop = control.TabStop });
                    }
                }
            }

            if (rule != null)
            {
                rule.SetRule(this);
            }

            // 対応ごとの準備（郵便番号を入力している最中に住所を探す、など）
            foreach (SControlDBItem item in _items)
            {
                item.OnEndAdd(this);
            }

            foreach (Control control in _controls)
            {
                control.Enter += OnControlEnter;

                var checkBox = control as CheckBox;
                if (checkBox != null)
                {
                    // チェック ボックスは、オン・オフを変えた時点で書き戻す
                    checkBox.CheckedChanged += OnControlValueChanged;
                }
                else
                {
                    control.Validating += OnControlValidating;
                }
            }
        }

        private void RequireEndAdd()
        {
            if (!_endAdd)
            {
                throw new InvalidOperationException("先に EndAdd を呼んで、登録が終わったことを知らせてください。");
            }
        }

        #endregion

        #region 有効・無効と読み取り専用

        /// <summary>
        /// コントロールの有効・無効を切り替える。
        /// </summary>
        public void Enabled(Control control, bool enabled)
        {
            if (control != null)
            {
                control.Enabled = enabled;
            }
        }

        /// <summary>登録したすべてのコントロールの有効・無効を切り替える。</summary>
        public void EnabledAll(bool enabled)
        {
            RequireEndAdd();
            foreach (Control control in _controls)
            {
                control.Enabled = enabled;
            }
        }

        /// <summary>
        /// 登録したコントロールを、読み取り専用にする・戻す。
        /// 読み取り専用の間は、背景をグレー（<see cref="SystemColors.Control"/>）にし、Tab キーでの移動先から外す
        /// （チェック ボックスは、背景を変えずに、クリックで切り替わらないようにする）。戻すと、元の背景色と Tab の設定に戻る。
        /// </summary>
        public void ReadOnly(Control control, bool readOnly)
        {
            RequireEndAdd();

            ControlState state;
            if (control == null || !_states.TryGetValue(control, out state) || state.ReadOnly == readOnly)
            {
                return;
            }

            var textBox = control as TextBoxBase;
            var comboBox = control as SComboBox;
            var checkBox = control as CheckBox;
            if (textBox != null)
            {
                textBox.ReadOnly = readOnly;
            }
            else if (comboBox != null)
            {
                comboBox.ReadOnly = readOnly;
            }
            else if (checkBox != null)
            {
                checkBox.AutoCheck = !readOnly;
            }
            else
            {
                ErrLog.WriteLine("読み取り専用にできない種類のコントロールです。 '" + control.Name + "'");
                return;
            }

            state.ReadOnly = readOnly;
            control.TabStop = readOnly ? false : state.TabStop;
            if (checkBox == null)
            {
                control.BackColor = readOnly ? SystemColors.Control : state.BackColor;
            }
        }

        /// <summary>登録したすべてのコントロールを、読み取り専用にする・戻す。</summary>
        public void ReadOnlyAll(bool readOnly)
        {
            RequireEndAdd();
            foreach (Control control in _controls)
            {
                ReadOnly(control, readOnly);
            }
        }

        #endregion

        #region 読み込み（行 → コントロール）

        /// <summary>
        /// 登録したすべての項目について、行の値をコントロールに表示する。
        /// </summary>
        public void FetchAll()
        {
            RequireEndAdd();
            Fetch(_items);
        }

        /// <summary>
        /// <paramref name="fieldName"/> の項目に対応するコントロールに、行の値を表示する。
        /// </summary>
        /// <returns>対応が登録されていたか。</returns>
        public bool FetchByField(string fieldName)
        {
            return FetchByField(new[] { fieldName });
        }

        /// <summary>
        /// <paramref name="fieldNames"/> の項目に対応するコントロールに、行の値を表示する。
        /// </summary>
        /// <returns>すべての項目について、対応が登録されていたか。</returns>
        public bool FetchByField(string[] fieldNames)
        {
            RequireEndAdd();

            bool allFound = true;
            foreach (string fieldName in fieldNames)
            {
                List<SControlDBItem> items = FindItemsByField(fieldName, null);
                if (items.Count == 0)
                {
                    ErrLog.WriteLine("指定された項目は登録されていません。 '" + fieldName + "'");
                    allFound = false;
                    continue;
                }
                Fetch(items);
            }
            return allFound;
        }

        /// <summary>
        /// <paramref name="control"/> と同じ項目を見ているすべてのコントロールに、行の値を表示する。
        /// </summary>
        /// <returns>コントロールが登録されていたか。</returns>
        public bool FetchByControl(Control control)
        {
            RequireEndAdd();

            List<SControlDBItem> items = FindItemsByControl(control);
            if (items.Count == 0)
            {
                ErrLog.WriteLine("指定されたコントロールは登録されていません。 '" + (control == null ? "null" : control.Name) + "'");
                return false;
            }

            foreach (SControlDBItem item in items)
            {
                FetchByField(item.RowFields);
            }
            return true;
        }

        /// <summary>
        /// 登録したすべてのコントロールを空にする（コンボ ボックスは、項目があれば先頭を選択する）。行には書き戻さない。
        /// </summary>
        public void ClearAll()
        {
            RequireEndAdd();

            bool wasFetching = _fetching;
            _fetching = true;
            try
            {
                foreach (SControlDBItem item in _items)
                {
                    item.Clear();
                }
            }
            finally
            {
                _fetching = wasFetching;
            }
        }

        private void Fetch(IEnumerable<SControlDBItem> items)
        {
            bool wasFetching = _fetching;
            _fetching = true;
            try
            {
                foreach (SControlDBItem item in items)
                {
                    item.Fetch(_getRow());
                }
            }
            finally
            {
                _fetching = wasFetching;
            }
        }

        #endregion

        #region 書き戻し（コントロール → 行）

        /// <summary>
        /// 登録したすべてのコントロールの値を、行に書き戻す。
        /// </summary>
        /// <returns>書き戻しを行ったか（読み込みの最中は行わない）。</returns>
        public bool UpdateAll()
        {
            RequireEndAdd();
            if (_fetching)
            {
                return false;
            }

            foreach (Control control in _controls)
            {
                if (control.Focused)
                {
                    CommitPendingEdit(control);
                }
            }

            foreach (SControlDBItem item in _items)
            {
                Update(item, item.Ctls[0]);
            }
            return true;
        }

        /// <summary>
        /// <paramref name="control"/> の値を、行に書き戻す。同じ項目を見ているほかのコントロールは、書き戻した値で表示し直す。
        /// 入力の途中の内容があるコントロール（日付・テーブル コンボ）は、先に確定してから書き戻す。
        /// </summary>
        /// <returns>書き戻しを行ったか（読み込みの最中と、登録されていないコントロールでは行わない）。</returns>
        public bool UpdateByControl(Control control)
        {
            RequireEndAdd();
            if (_fetching)
            {
                return false;
            }

            List<SControlDBItem> items = FindItemsByControl(control);
            if (items.Count == 0)
            {
                return false;
            }

            CommitPendingEdit(control);
            foreach (SControlDBItem item in items)
            {
                Update(item, control);
            }
            return true;
        }

        /// <summary>
        /// <paramref name="control"/> に入力の途中の内容があれば、値として確定させる。
        /// </summary>
        /// <remarks>
        /// 日付やテーブル コンボの入力欄は、フォーカスがほかへ移るときに値を確定する。ファンクション キーでの保存のように、
        /// フォーカスを動かさずに書き戻す場合は、確定していない値（入力する前の値）を読んでしまうので、ここで確定させる。
        /// </remarks>
        private static void CommitPendingEdit(Control control)
        {
            var pending = control as ISEditCommit;
            if (pending != null)
            {
                pending.CommitEdit();
            }
        }

        /// <summary>
        /// フォーム（コンストラクターに渡したもの）の中で、いまフォーカスがあるコントロールの値を、行に書き戻す。
        /// 保存の直前など、フォーカスを動かさずに、入力中の値を確定させたいときに使う
        /// （ファンクション キーでの保存は、フォーカスが動かないので、保存の前にこれを呼ぶこと）。
        /// </summary>
        public void Update()
        {
            if (_form != null)
            {
                Update(_form);
            }
        }

        /// <summary>
        /// <paramref name="container"/>（フォームなど）の中で、いまフォーカスがあるコントロールの値を、行に書き戻す。
        /// </summary>
        public void Update(ContainerControl container)
        {
            // フォーカスが別のコンテナー（ユーザー コントロールなど）の中にある場合は、中までたどる
            Control active = container.ActiveControl;
            var inner = active as ContainerControl;
            while (inner != null && inner.ActiveControl != null)
            {
                active = inner.ActiveControl;
                inner = active as ContainerControl;
            }

            if (active != null)
            {
                UpdateByControl(active);
            }
        }

        private void Update(SControlDBItem item, Control activeControl)
        {
            item.Update(this, _getRow(), activeControl);

            if (item.SameFieldFetch)
            {
                // 同じ項目を見ているほかの対応を、書き戻した値で表示し直す
                var others = new List<SControlDBItem>();
                foreach (string field in item.RowFields)
                {
                    foreach (SControlDBItem other in FindItemsByField(field, item))
                    {
                        if (!others.Contains(other))
                        {
                            others.Add(other);
                        }
                    }
                }
                Fetch(others);
            }
        }

        private void OnItemFieldUpdated(ISRow row, string field, object value)
        {
            SControlDBFieldUpdatedHandler handler = FieldUpdated;
            if (handler != null)
            {
                handler(row, field, value);
            }
        }

        #endregion

        #region 対応を調べる

        /// <summary>
        /// <paramref name="field"/> の項目に対応するコントロールを返す。登録されていなければ null。
        /// </summary>
        public Control GetControl(string field)
        {
            foreach (SControlDBItem item in _items)
            {
                int index = Array.IndexOf(item.RowFields, field);
                if (index >= 0)
                {
                    // 入力欄が複数ある項目（ハイフン区切り・郵便番号）では、先頭の入力欄を返す
                    return item.Ctls[item.GetControlIndex(index)];
                }
            }
            return null;
        }

        /// <summary>
        /// <paramref name="control"/> に対応する項目名を返す。登録されていなければ null。
        /// </summary>
        public string GetField(Control control)
        {
            foreach (SControlDBItem item in _items)
            {
                int index = Array.IndexOf(item.Ctls, control);
                if (index >= 0)
                {
                    return item.RowFields[item.GetFieldIndex(index)];
                }
            }
            return null;
        }

        /// <summary>
        /// <paramref name="field"/> の項目の対応（最初に登録したもの）を返す。登録されていなければ null。
        /// </summary>
        public SControlDBItem GetItem(string field)
        {
            List<SControlDBItem> items = FindItemsByField(field, null);
            return items.Count > 0 ? items[0] : null;
        }

        /// <summary>
        /// <paramref name="control"/> の対応（最初に登録したもの）を返す。登録されていなければ null。
        /// </summary>
        public SControlDBItem GetItem(Control control)
        {
            List<SControlDBItem> items = FindItemsByControl(control);
            return items.Count > 0 ? items[0] : null;
        }

        private List<SControlDBItem> FindItemsByField(string field, SControlDBItem except)
        {
            var found = new List<SControlDBItem>();
            foreach (SControlDBItem item in _items)
            {
                if (item != except && Array.IndexOf(item.RowFields, field) >= 0)
                {
                    found.Add(item);
                }
            }
            return found;
        }

        private List<SControlDBItem> FindItemsByControl(Control control)
        {
            var found = new List<SControlDBItem>();
            if (control != null)
            {
                foreach (SControlDBItem item in _items)
                {
                    if (Array.IndexOf(item.Ctls, control) >= 0)
                    {
                        found.Add(item);
                    }
                }
            }
            return found;
        }

        #endregion

        #region コントロールのイベント

        private void OnControlEnter(object sender, EventArgs e)
        {
            // 全選択にしない入力欄は、カーソルを末尾に置く（続きを入力しやすいように）
            var textBox = sender as SEditBase;
            if (textBox != null && !textBox.HighlightText && !textBox.Multiline)
            {
                textBox.Select(textBox.TextLength, 0);
            }
        }

        private void OnControlValidating(object sender, CancelEventArgs e)
        {
            UpdateByControl((Control)sender);
        }

        private void OnControlValueChanged(object sender, EventArgs e)
        {
            UpdateByControl((Control)sender);
        }

        #endregion
    }
}
