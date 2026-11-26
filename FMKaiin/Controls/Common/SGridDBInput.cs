using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;

namespace App.Controls
{
    // SGridDB のうち、グリッドのセルに入力用のコントロールを重ねて入力する機能。
    // comset の GGridDBCommon の SetFocusControlInGrid・SetTabIndex・SelectInput・SelectNextControl・CheckFocusControl・Select に当たる。
    //
    // 仕組み:
    //   列に、入力に使うコントロール（SGridDBColumn.InputControl）を指定しておく。コントロールは、ふだんは見えない位置に置いておき、
    //   入力を始めるとき（SelectInput）にフォーカスを移す。フォーカスが入った時点で、指している行のセルの位置・大きさに合わせて表示し、
    //   フォーカスが出た時点で、見えない位置へ戻す。
    //   コントロールと行の値の受け渡しは、この機能では行わない。画面の側で、指している行を返す関数を渡した SControlDB を用意する
    //   （フォーカスが出たときに、SControlDB が値を行へ書き戻す。指している行が変わったときの読み込みは、画面の側で行う）。
    public partial class SGridDB
    {
        private const int WM_CHAR = 0x0102;

        /// <summary>入力用のコントロールを、ふだん置いておく位置の、親の端からの距離（ピクセル）。</summary>
        private const int HiddenMargin = 32;

        // 入力用のコントロールを持つ列があるか
        private bool _hasInput;

        // フォーカスがある入力用のコントロール。入力中でなければ null
        private Control _activeInput;

        #region 公開しているもの

        /// <summary>入力用のコントロールにフォーカスがあるか（セルで入力している最中か）。</summary>
        public bool IsInputActive
        {
            get { return _activeInput != null; }
        }

        /// <summary>
        /// <paramref name="control"/> が、このグリッドの列に指定した入力用のコントロール（<see cref="SGridDBColumn.InputControl"/>）か。
        /// GGridDBCommon.CheckFocusControl に当たる。
        /// </summary>
        public bool IsInputControl(Control control)
        {
            return control != null && IndexOfInput(control) >= 0;
        }

        /// <summary>
        /// 指しているセルで、入力を始める（その列の入力用のコントロールを、セルに重ねて表示して、フォーカスを移す）。
        /// </summary>
        /// <remarks>
        /// 行が無い、その列に入力用のコントロールが無い、コントロールが無効、のどれかなら、グリッドにフォーカスを置くだけで、入力は始めない。
        /// すでに入力中なら、何もしない。
        /// </remarks>
        public void SelectInput()
        {
            if (!_endAdd)
            {
                throw new InvalidOperationException("EndAdd の後に呼んでください。");
            }

            if (_grid.RowCount <= 0)
            {
                _grid.Select();
                return;
            }

            Control input = InputAt(_grid.Col);
            if (input == null || !input.Enabled)
            {
                if (!_grid.Focused)
                {
                    _grid.Select();
                }
                return;
            }
            if (_activeInput != null)
            {
                return;
            }

            // 指している行が見えていないと、コントロールを重ねる位置が決まらない
            ScrollRowIntoView(false);
            input.Select();
        }

        /// <summary>
        /// 入力を、次の入力欄へ移す。順番は <see cref="SGridDBColumn.InputTabIndex"/>（同じ値なら左から）。
        /// 行の最後の入力欄からは、次の行の最初の入力欄へ移る。GGridDBCommon.SelectNextControl に当たる。
        /// </summary>
        /// <param name="forward">true で次へ、false で前へ。</param>
        /// <returns>
        /// 移れたら true。移る先が無い（最後の行の最後の入力欄から次へ、最初の行の最初の入力欄から前へ）ときは false。
        /// false のときは、入力を終えて、フォーカスはグリッドにある。
        /// </returns>
        public bool SelectNextInput(bool forward)
        {
            if (!_endAdd)
            {
                throw new InvalidOperationException("EndAdd の後に呼んでください。");
            }

            // いまの入力欄の内容を行へ書き戻させるために、先にフォーカスをグリッドへ移す（行を移る前に行うこと）
            Select();

            if (_grid.RowCount <= 0)
            {
                return false;
            }

            List<int> order = InputOrder();
            if (order.Count == 0)
            {
                return false;
            }

            int position = order.IndexOf(_grid.Col);
            int next;
            if (position < 0)
            {
                // 入力欄の無い列を指している: いまの行の、最初（前へ移るときは最後）の入力欄へ
                next = forward ? order[0] : order[order.Count - 1];
            }
            else if (forward && position < order.Count - 1)
            {
                next = order[position + 1];
            }
            else if (!forward && position > 0)
            {
                next = order[position - 1];
            }
            else if (forward && _grid.Row < _grid.RowCount - 1)
            {
                _grid.Row++;
                next = order[0];
            }
            else if (!forward && _grid.Row > 0)
            {
                _grid.Row--;
                next = order[order.Count - 1];
            }
            else
            {
                return false;
            }

            ScrollRowIntoView(true);
            _grid.Col = next;
            _columns[next].InputControl.Select();
            return true;
        }

        /// <summary>
        /// グリッドにフォーカスを移す（入力中なら、入力を終える）。入力中だった列を、指している列にする。
        /// GGridDBCommon.Select に当たる。
        /// </summary>
        public void Select()
        {
            if (_activeInput != null)
            {
                int index = IndexOfInput(_activeInput);
                if (index >= 0)
                {
                    _grid.Col = index;
                }
            }
            _grid.Select();
        }

        #endregion

        #region 準備と後始末

        /// <summary>
        /// 入力用のコントロールを持つ列があれば、コントロールとグリッドに、入力のための設定をする。EndAdd から呼ぶ。
        /// </summary>
        private void SetUpInput()
        {
            // 誤った指定は、コントロールに手を付ける前に、すべて調べる
            for (int i = 0; i < _columns.Count; i++)
            {
                Control input = _columns[i].InputControl;
                if (input == null)
                {
                    continue;
                }
                if (input.Parent == null)
                {
                    throw new InvalidOperationException("入力用のコントロールは、フォームに置いてから登録してください: " + _columns[i].DataField);
                }
                for (int j = 0; j < i; j++)
                {
                    if (_columns[j].InputControl == input)
                    {
                        throw new InvalidOperationException("同じコントロールを、2 つの列の入力用に指定することはできません: " + _columns[i].DataField);
                    }
                }
            }

            foreach (SGridDBColumn column in _columns)
            {
                Control input = column.InputControl;
                if (input == null)
                {
                    continue;
                }

                input.Enter += OnInputEnter;
                input.Leave += OnInputLeave;
                input.PreviewKeyDown += OnInputPreviewKeyDown;
                input.KeyDown += OnInputKeyDown;

                // 入力するとき以外は、見えない位置に置く。Tab での移動先にもしない（移る順番は、SelectNextInput が決める）
                input.TabStop = false;
                HideInput(input);
                _hasInput = true;
            }

            if (!_hasInput)
            {
                return;
            }

            // グリッドの中での Tab は、セルの間を移る。Enter は、入力を始めるのに使うので、グリッドには何もさせない
            _grid.TabAction = TabActionEnum.GridNavigation;
            _grid.DirectionAfterEnter = DirectionAfterEnterEnum.MoveNone;

            _grid.KeyDown += OnGridKeyDownForInput;
            _grid.KeyPress += OnGridKeyPressForInput;
            _grid.MouseDoubleClick += OnGridMouseDoubleClickForInput;
            _grid.Scroll += OnGridScrollForInput;
            _grid.LocationChanged += OnGridLocationChangedForInput;
        }

        /// <summary>グリッドが破棄されるときに、イベントの登録を外す。</summary>
        private void TearDownInput()
        {
            if (!_hasInput)
            {
                return;
            }

            _grid.KeyDown -= OnGridKeyDownForInput;
            _grid.KeyPress -= OnGridKeyPressForInput;
            _grid.MouseDoubleClick -= OnGridMouseDoubleClickForInput;
            _grid.Scroll -= OnGridScrollForInput;
            _grid.LocationChanged -= OnGridLocationChangedForInput;

            foreach (SGridDBColumn column in _columns)
            {
                Control input = column.InputControl;
                if (input != null)
                {
                    input.Enter -= OnInputEnter;
                    input.Leave -= OnInputLeave;
                    input.PreviewKeyDown -= OnInputPreviewKeyDown;
                    input.KeyDown -= OnInputKeyDown;
                }
            }
            _activeInput = null;
        }

        #endregion

        #region 入力用のコントロールのイベント

        /// <summary>
        /// 入力用のコントロールにフォーカスが入った: 指している行の、その列のセルに重ねて表示する。
        /// </summary>
        private void OnInputEnter(object sender, EventArgs e)
        {
            var input = (Control)sender;
            int index = IndexOfInput(input);
            if (index < 0)
            {
                return;
            }

            Rectangle cell;
            if (!TryGetCellBounds(_grid.Row, index, out cell))
            {
                // セルの位置が分からない（行が無い、見えていない）ので、入力できない。グリッドへ戻す。
                // フォーカスが移っている最中なので、移り終えてから戻す
                if (_grid.IsHandleCreated)
                {
                    _grid.BeginInvoke(new MethodInvoker(_grid.Select));
                }
                return;
            }

            if (_grid.Col != index)
            {
                _grid.Col = index;
            }

            // セルの右と下の 1 ピクセルは、罫線
            Rectangle bounds = input.Parent.RectangleToClient(_grid.RectangleToScreen(cell));
            input.SetBounds(bounds.X, bounds.Y, Math.Max(1, bounds.Width - 1), Math.Max(1, bounds.Height - 1));
            input.BringToFront();
            _activeInput = input;
        }

        /// <summary>
        /// 入力用のコントロールからフォーカスが出た: 見えない位置へ戻す。
        /// </summary>
        private void OnInputLeave(object sender, EventArgs e)
        {
            var input = (Control)sender;
            HideInput(input);
            if (_activeInput == input)
            {
                _activeInput = null;
            }
        }

        /// <summary>
        /// 入力用のコントロールでの Tab と、入力欄ではないコントロール（チェック ボックスなど）での矢印キーを、
        /// フォームのフォーカスの移動に使わせない。
        /// </summary>
        /// <remarks>
        /// 入力用のコントロールは、見えない位置に置いたコントロールなので、フォームの決まり（Tab の順番）で移ると、
        /// 利用者から見て脈絡のないところへフォーカスが飛ぶ。Tab は、次の入力欄へ移るのに使う（OnInputKeyDown）。
        /// </remarks>
        private void OnInputPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;
            }
            else if (IsArrowKey(e.KeyCode) && !(sender is TextBoxBase))
            {
                e.IsInputKey = true;
            }
        }

        /// <summary>
        /// 入力用のコントロールでの Tab: 次の入力欄へ移る（Shift を押していれば、前へ）。
        /// </summary>
        private void OnInputKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && !e.Control && !e.Alt)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextInput(!e.Shift);
            }
        }

        #endregion

        #region グリッドのイベント

        /// <summary>
        /// グリッドで Enter を押した: 指しているセルで、入力を始める。
        /// </summary>
        /// <remarks>
        /// フォームがキーを先に受け取って Enter を処理している画面（FormFrame）では、画面の側で SelectInput を呼ぶ。
        /// その場合、ここに来たときにはフォーカスが入力用のコントロールへ移っているので、何もしない。
        /// </remarks>
        private void OnGridKeyDownForInput(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || !_grid.Focused)
            {
                return;
            }
            if (InputAt(_grid.Col) == null)
            {
                return;
            }

            SelectInput();
            e.Handled = true;
        }

        /// <summary>
        /// グリッドで文字を入力した: 指しているセルで入力を始めて、その文字を入力用のコントロールへ渡す。
        /// </summary>
        private void OnGridKeyPressForInput(object sender, KeyPressEventArgs e)
        {
            // Enter・Tab・BackSpace・Esc など、文字ではないものは対象外。Ctrl・Alt との組み合わせも対象外
            if (char.IsControl(e.KeyChar) || (Control.ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
            {
                return;
            }

            if (_grid.Focused)
            {
                if (InputAt(_grid.Col) == null)
                {
                    return;
                }
                SelectInput();
                e.Handled = true;
            }
            else if (_activeInput == null)
            {
                return;
            }
            else
            {
                // 入力を始めた直後に、続きの文字がグリッドへ届いた（IME で確定した文字列の、2 文字目以降など）
                e.Handled = true;
            }

            if (_activeInput != null)
            {
                SendCharToInput(_activeInput, e.KeyChar);
            }
        }

        /// <summary>
        /// グリッドのセルをダブルクリックした: そのセルで、入力を始める。
        /// </summary>
        private void OnGridMouseDoubleClickForInput(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            int row;
            int column;
            _grid.CellContaining(e.X, e.Y, out row, out column);
            if (row >= 0 && column >= 0)
            {
                SelectInput();
            }
        }

        // グリッドをスクロールした・動かしたときは、入力欄がセルからずれるので、入力を終える
        private void OnGridScrollForInput(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            EndInput();
        }

        private void OnGridLocationChangedForInput(object sender, EventArgs e)
        {
            EndInput();
        }

        #endregion

        #region 内部の処理

        /// <summary>入力中なら、入力を終えて、グリッドにフォーカスを移す。</summary>
        private void EndInput()
        {
            if (_activeInput != null)
            {
                Select();
            }
        }

        /// <summary><paramref name="column"/> 番目の列の、入力用のコントロール。無ければ null。</summary>
        private Control InputAt(int column)
        {
            if (column < 0 || column >= _columns.Count)
            {
                return null;
            }
            return _columns[column].InputControl;
        }

        /// <summary><paramref name="control"/> を入力用に指定した列の番号。無ければ -1。</summary>
        private int IndexOfInput(Control control)
        {
            for (int i = 0; i < _columns.Count; i++)
            {
                if (_columns[i].InputControl == control)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// いま入力できる列（入力用のコントロールがあって、有効で、表示している列）の番号を、入力欄を移る順に並べたもの。
        /// </summary>
        private List<int> InputOrder()
        {
            var order = new List<int>();
            for (int i = 0; i < _columns.Count; i++)
            {
                SGridDBColumn column = _columns[i];
                if (column.InputControl != null && column.InputControl.Enabled && column.Visible)
                {
                    order.Add(i);
                }
            }
            // 順番の値が同じ列は、左から（List.Sort は、同じ値の並びを保たないので、列の番号も比べる）
            order.Sort(delegate(int a, int b)
            {
                int result = _columns[a].InputTabIndex.CompareTo(_columns[b].InputTabIndex);
                return result != 0 ? result : a.CompareTo(b);
            });
            return order;
        }

        /// <summary>
        /// 指している行が、グリッドに見えているようにする。
        /// </summary>
        /// <param name="nearEdge">
        /// true なら、見えている範囲のいちばん上・いちばん下の行でも、上下の中央あたりへ寄せる
        /// （入力欄を次々に移っていくときに、先の行が見えるように。GGridDBCommon と同じ）。
        /// </param>
        private void ScrollRowIntoView(bool nearEdge)
        {
            int row = _grid.Row;
            int first = _grid.FirstRow;
            int visible = Math.Max(1, _grid.VisibleRows);
            int margin = nearEdge ? 1 : 0;

            bool above = row < first + margin && first > 0;
            bool below = row >= first + visible - margin;
            if (above || below)
            {
                _grid.FirstRow = Math.Max(0, row - visible / 2);
            }
        }

        /// <summary>セルの位置と大きさ（グリッドの中での座標）。行が無い・見えていないなどで分からなければ false。</summary>
        private bool TryGetCellBounds(int row, int column, out Rectangle bounds)
        {
            bounds = Rectangle.Empty;
            if (row < 0 || row >= _grid.RowCount)
            {
                return false;
            }

            try
            {
                bounds = _grid.Splits[0].GetCellBounds(row, column);
            }
            catch (Exception)
            {
                // 見えていない行のセルを尋ねると、グリッドが例外を出す
                return false;
            }
            return bounds.Width > 0 && bounds.Height > 0 && _grid.ClientRectangle.IntersectsWith(bounds);
        }

        /// <summary>入力用のコントロールを、見えない位置（親の左上の外）へ移す。大きさは変えない。</summary>
        private static void HideInput(Control input)
        {
            input.Location = new Point(-input.Width - HiddenMargin, -input.Height - HiddenMargin);
        }

        /// <summary>
        /// グリッドで入力した文字を、入力用のコントロールへ渡す。
        /// </summary>
        private static void SendCharToInput(Control input, char character)
        {
            if (input is TextBoxBase)
            {
                // キーボードから入力したのと同じ経路で渡す（入力できる文字の選別や、一覧の絞り込みが働く）
                SendMessage(input.Handle, WM_CHAR, (IntPtr)character, IntPtr.Zero);
                return;
            }

            // チェック ボックス: Space で、オン・オフを切り替える（利用者が切り替えられる設定のときだけ）
            var checkBox = input as CheckBox;
            if (checkBox != null && character == ' ' && checkBox.AutoCheck)
            {
                checkBox.Checked = !checkBox.Checked;
            }
        }

        private static bool IsArrowKey(Keys keyCode)
        {
            return keyCode == Keys.Up || keyCode == Keys.Down || keyCode == Keys.Left || keyCode == Keys.Right;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        #endregion
    }
}
