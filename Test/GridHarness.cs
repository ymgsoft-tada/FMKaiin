// SGridDB の検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
// フォームにグリッド（C1TrueDBGrid）を置き、列の定義と見本のデータを結び付けて、列の作成・幅・表示する文字列・行の連動を確かめる。
// セルに入力用のコントロールを重ねて入力する機能（SGridDBInput.cs）は、InputTests で確かめる。
// データベースには接続しない。グリッドのライセンスの情報は、run.sh がこの実行ファイルに埋め込む。
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ComponentSUI;
using C1.Win.C1TrueDBGrid;
using ComponentDB;

internal static class GridHarness
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    private static extern bool SetProcessDPIAware();

    private const int WM_KEYDOWN = 0x0100;
    private const int WM_CHAR = 0x0102;
    private const int WM_RBUTTONDOWN = 0x0204;
    private const int WM_RBUTTONUP = 0x0205;

    private static int _failed;

    [STAThread]
    private static int Main()
    {
        SetProcessDPIAware();
        Console.OutputEncoding = new System.Text.UTF8Encoding(false);
        Application.EnableVisualStyles();

        IntPtr previous = GetForegroundWindow();
        try
        {
            Tests();
            InputTests();
            ColumnTests();
        }
        finally
        {
            if (previous != IntPtr.Zero) SetForegroundWindow(previous);
        }
        Console.WriteLine(_failed == 0 ? "ALL PASSED" : ("FAILED: " + _failed));
        return _failed;
    }

    private static void Check(string name, object actual, object expected)
    {
        bool ok = Equals(actual == null ? null : actual.ToString(), expected == null ? null : expected.ToString());
        if (!ok) _failed++;
        Console.WriteLine((ok ? "ok   " : "NG   ") + name + " => [" + actual + "]" + (ok ? "" : " expected [" + expected + "]"));
    }

    private static string Throws(Action action)
    {
        try
        {
            action();
            return "no exception";
        }
        catch (Exception ex)
        {
            return ex.GetType().Name;
        }
    }

    private static string Widths(C1TrueDBGrid grid)
    {
        C1DisplayColumnCollection columns = grid.Splits[0].DisplayColumns;
        var parts = new string[columns.Count];
        for (int i = 0; i < columns.Count; i++) parts[i] = columns[i].Width.ToString();
        return string.Join(",", parts);
    }

    /// <summary>登録した列から、SGridDBColumn の説明のとおりに計算した幅（割合 0.15、ピクセル 100、残り）。</summary>
    private static string ExpectedWidths(C1TrueDBGrid grid, float scale)
    {
        int available = grid.Width - SystemInformation.VerticalScrollBarWidth;
        int code = (int)(available * 0.15f);
        int name = (int)Math.Round(100 * scale);
        int rest = available - (code + 1) - (name + 1) - 4;
        return code + "," + name + "," + rest;
    }

    private static void Tests()
    {
        var table = new DataTable();
        table.Columns.Add("Code", typeof(string));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Kind", typeof(int));
        table.Rows.Add("1", "鈴木", 2);
        table.Rows.Add("2", "佐藤", 3);
        table.Rows.Add("10", "田中", 2);
        table.AcceptChanges();

        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ClientSize = new Size(520, 320);
            form.ShowInTaskbar = false;
            var grid = new C1TrueDBGrid { Location = new Point(10, 10), Size = new Size(400, 200) };
            form.Controls.Add(grid);
            form.Show();
            form.Activate();
            Application.DoEvents();

            float scale;
            using (Graphics g = form.CreateGraphics()) scale = g.DpiX / 96f;

            // フォームの BindingContext を渡して作ると、グリッドの「指している行」と連動する
            var view = new DBView(table, form.BindingContext);
            var sgrid = new SGridDB(grid, form);
            SGridDBColumn kind = null;
            int fetched = 0;
            sgrid.Add(new SGridDBText("Code", "コード", 0.15f, SGridDBCellDisp.Right));
            sgrid.Add(new SGridDBText("Name", "氏名", 100));
            kind = sgrid.Add(new SGridDBText("Kind", "権限", 0.0f)
            {
                UnboundFetch = delegate(SGridDBColumn col, int row)
                {
                    fetched++;
                    return "区分" + col.UnboundValue + "(" + view[row].Row["Name"] + ")";
                }
            });
            Check("columns before EndAdd", sgrid.Columns.Count + "/" + (kind == sgrid.Columns[2]), "3/True");
            sgrid.EndAdd(view);
            Application.DoEvents();

            // ---- 列の作成 ----
            Check("grid columns", grid.Columns.Count + "/" + grid.Columns[0].Caption + "/" + grid.Columns[1].Caption + "/" + grid.Columns[2].Caption, "3/コード/氏名/権限");
            Check("rows", grid.RowCount, 3);
            Check("alignment", grid.Splits[0].DisplayColumns[0].Style.HorizontalAlignment + "/" + grid.Splits[0].DisplayColumns[1].Style.HorizontalAlignment, "Far/Near");
            Check("read only", grid.AllowUpdate + "/" + grid.AllowAddNew + "/" + grid.AllowSort, "False/False/False");

            // ---- 列の幅: 割合・ピクセル（表示倍率に合わせる）・残り ----
            Check("widths", Widths(grid), ExpectedWidths(grid, scale));
            grid.Width += 120;
            Application.DoEvents();
            Check("widths after resize", Widths(grid), ExpectedWidths(grid, scale));

            // ---- 表示する文字列 ----
            Check("bound text", grid.Columns[0].CellText(2) + "/" + grid.Columns[1].CellText(1), "10/佐藤");
            Check("unbound text", grid.Columns[2].CellText(0) + "/" + grid.Columns[2].CellText(1) + "/" + (fetched > 0), "区分2(鈴木)/区分3(佐藤)/True");

            // ---- 指している行が、DBView の現在の行と連動する ----
            grid.Row = 2;
            Application.DoEvents();
            Check("grid row -> view", view.CurrentRow.Row["Name"], "田中");
            view.SearchRow("Code", "2");
            Application.DoEvents();
            Check("view -> grid row", grid.Row, 1);

            // ---- マウスの右ボタンで押した行を、指している行にする ----
            Rectangle cell = grid.Splits[0].GetCellBounds(0, 1);
            IntPtr at = (IntPtr)(((cell.Top + cell.Height / 2) << 16) | (cell.Left + 10));
            SendMessage(grid.Handle, WM_RBUTTONDOWN, (IntPtr)2, at);
            SendMessage(grid.Handle, WM_RBUTTONUP, IntPtr.Zero, at);
            Application.DoEvents();
            Check("right button selects the row", grid.Row, 0);
            sgrid.EnableRightClickSelect = false;
            cell = grid.Splits[0].GetCellBounds(2, 1);
            at = (IntPtr)(((cell.Top + cell.Height / 2) << 16) | (cell.Left + 10));
            SendMessage(grid.Handle, WM_RBUTTONDOWN, (IntPtr)2, at);
            SendMessage(grid.Handle, WM_RBUTTONUP, IntPtr.Zero, at);
            Application.DoEvents();
            Check("right button does nothing when turned off", grid.Row, 0);

            // ---- 行を指定して、指している行にする（行の数より大きい番号は、最後の行）----
            sgrid.SetRow(2);
            Check("SetRow", grid.Row + "/" + view.CurrentRow.Row["Name"], "2/田中");
            sgrid.SetRow(99);
            Check("SetRow beyond the last row", grid.Row, 2);
            sgrid.SetRow(0);

            // ---- 行の追加・削除が、グリッドに反映される ----
            table.Rows.Add("11", "山田", 3);
            Application.DoEvents();
            Check("row added", grid.RowCount + "/" + grid.Columns[2].CellText(3), "4/区分3(山田)");

            // ---- 誤った使い方 ----
            Check("Add after EndAdd", Throws(delegate { sgrid.Add(new SGridDBText("Code", "x", 10)); }), "InvalidOperationException");
            Check("EndAdd twice", Throws(delegate { sgrid.EndAdd(view); }), "InvalidOperationException");
            Check("null arguments", Throws(delegate { new SGridDB(null, form); }) + "/" + Throws(delegate { new SGridDB(grid, null); }) + "/" + Throws(delegate { new SGridDBText("", "x", 10); }),
                "ArgumentNullException/ArgumentNullException/ArgumentException");

            form.Close();
        }
    }

    /// <summary>
    /// 文字列以外の列の種類（数値・金額・日付・計算で作る列）と、行ごとの見た目（RowStyle）。
    /// </summary>
    private static void ColumnTests()
    {
        var table = new DataTable();
        table.Columns.Add("No", typeof(int));
        table.Columns.Add("Cost", typeof(decimal));
        table.Columns.Add("Day", typeof(DateTime));
        table.Columns.Add("Status", typeof(int));
        table.Rows.Add(1, 1234567m, new DateTime(2026, 11, 26), 1);
        table.Rows.Add(20, 0m, DBNull.Value, 2);
        table.Rows.Add(300, DBNull.Value, new DateTime(2019, 4, 30), 3);
        table.AcceptChanges();

        SDateCalendarKind savedKind = SDateBox.DefaultCalendarKind;
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ClientSize = new Size(620, 320);
            form.ShowInTaskbar = false;
            var grid = new C1TrueDBGrid { Location = new Point(10, 10), Size = new Size(560, 200) };
            form.Controls.Add(grid);
            form.Show();
            form.Activate();
            Application.DoEvents();

            var view = new DBView(table, form.BindingContext);
            var sgrid = new SGridDB(grid, form);
            sgrid.Add(new SGridDBNumber("No", "No", 60));
            sgrid.Add(new SGridDBCurrency("Cost", "金額", 120));
            sgrid.Add(new SGridDBDate("Day", "日付", 120));
            // データに無い項目の名前でも、計算で作る列にできる（UnboundValue は null）
            sgrid.Add(new SGridDBUnbound("Mark", "状況", delegate(SGridDBColumn col, int row)
            {
                int status = (int)view[row].Row["Status"];
                return (col.UnboundValue == null ? "" : "?") + (status == 1 ? "〇" : status == 2 ? "△" : "×");
            }, 60, SGridDBCellDisp.Center));
            sgrid.Add(new SGridDBUnbound("Status", "値", delegate(SGridDBColumn col, int row) { return "[" + col.UnboundValue + "]"; }, 0.0f));
            var styled = new List<int>();
            sgrid.RowStyle = delegate(int row, Style style)
            {
                styled.Add(row);
                style.ForeColor = (int)view[row].Row["Status"] == 3 ? Color.Red : Color.Blue;
            };
            sgrid.EndAdd(view);
            Application.DoEvents();

            Func<int, string> rowText = delegate(int row)
            {
                var cells = new List<string>();
                for (int i = 0; i < grid.Columns.Count; i++) cells.Add(grid.Columns[i].CellText(row));
                return string.Join("|", cells.ToArray());
            };

            SDateBox.DefaultCalendarKind = SDateCalendarKind.Japanese;
            grid.Refresh();
            Check("columns: row 0 (Japanese)", rowText(0), "1|1,234,567|R08/11/26|〇|[1]");
            Check("columns: row 1 (blank date, zero)", rowText(1), "20|0||△|[2]");
            Check("columns: row 2 (blank cost, Heisei)", rowText(2), "300||H31/04/30|×|[3]");
            SDateBox.DefaultCalendarKind = SDateCalendarKind.Western;
            Check("columns: row 0 (Western)", rowText(0), "1|1,234,567|2026/11/26|〇|[1]");

            Split split = grid.Splits[0];
            Check("columns: alignment",
                split.DisplayColumns[0].Style.HorizontalAlignment + "/" + split.DisplayColumns[1].Style.HorizontalAlignment + "/"
                + split.DisplayColumns[2].Style.HorizontalAlignment + "/" + split.DisplayColumns[3].Style.HorizontalAlignment,
                "Far/Far/Center/Center");

            // 行ごとの見た目: 指している行（0 行目）には適用しない
            grid.Row = 0;
            styled.Clear();
            // フォームを画面の外に置いているので、画像に描かせて、行の見た目を尋ねさせる
            using (var bitmap = new Bitmap(grid.Width, grid.Height))
            {
                grid.DrawToBitmap(bitmap, new Rectangle(0, 0, grid.Width, grid.Height));
            }
            Check("row style: asked for the other rows", styled.Contains(1) + "/" + styled.Contains(2) + "/" + styled.Contains(0), "True/True/False");
            Check("row style: FetchRowStyles", grid.FetchRowStyles, "True");
            form.Close();
        }
        SDateBox.DefaultCalendarKind = savedKind;
    }

    /// <summary>フォームの中で、いまフォーカスがあるコントロールの名前。</summary>
    private static string Active(Form form)
    {
        return form.ActiveControl == null ? "null" : form.ActiveControl.Name;
    }

    /// <summary>コントロールが、フォームの見える範囲に少しでも入っているか。</summary>
    private static bool Shown(Form form, Control control)
    {
        Rectangle onScreen = control.Parent.RectangleToScreen(control.Bounds);
        return form.RectangleToScreen(form.ClientRectangle).IntersectsWith(onScreen);
    }

    /// <summary>
    /// セルに入力用のコントロールを重ねて入力する機能（SGridDBInput.cs）。
    /// グリッドはパネルの中、入力用のコントロールはフォームの上に置いて、親が違っても位置が合うことも確かめる。
    /// </summary>
    private static void InputTests()
    {
        var table = new DataTable();
        table.Columns.Add("Code", typeof(string));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Flag", typeof(bool));
        table.Rows.Add("1", "鈴木", false);
        table.Rows.Add("2", "佐藤", true);
        table.Rows.Add("3", "田中", false);
        table.AcceptChanges();

        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ClientSize = new Size(560, 360);
            form.ShowInTaskbar = false;
            var panel = new Panel { Location = new Point(30, 40), Size = new Size(500, 240) };
            var grid = new C1TrueDBGrid { Name = "grid", Location = new Point(12, 16), Size = new Size(420, 200) };
            panel.Controls.Add(grid);
            var nameBox = new TextBox { Name = "nameBox", Location = new Point(40, 300), Size = new Size(100, 20) };
            var flagBox = new CheckBox { Name = "flagBox", Location = new Point(160, 300), Size = new Size(60, 20), CheckAlign = ContentAlignment.MiddleCenter };
            var other = new TextBox { Name = "other", Location = new Point(240, 300), Size = new Size(100, 20) };
            form.Controls.Add(panel);
            form.Controls.Add(nameBox);
            form.Controls.Add(flagBox);
            form.Controls.Add(other);
            form.Show();
            form.Activate();
            Application.DoEvents();

            var view = new DBView(table, form.BindingContext);
            var sgrid = new SGridDB(grid, form);
            Check("input: SelectInput before EndAdd", Throws(delegate { sgrid.SelectInput(); }), "InvalidOperationException");
            sgrid.Add(new SGridDBText("Code", "コード", 80));
            // 移る順番は、氏名 → 可。順番の値を、列の並びと逆にしても、値の順に移る（後で確かめる）
            SGridDBColumn nameColumn = sgrid.Add(new SGridDBText("Name", "氏名", 150) { InputControl = nameBox, InputTabIndex = 1 });
            SGridDBColumn flagColumn = sgrid.Add(new SGridDBText("Flag", "可", 0.0f, SGridDBCellDisp.Center) { InputControl = flagBox, InputTabIndex = 2 });
            sgrid.EndAdd(view);
            Application.DoEvents();

            // ---- 登録した直後: 見えない位置にあって、Tab の移動先ではない ----
            Check("input: hidden after EndAdd", Shown(form, nameBox) + "/" + Shown(form, flagBox) + "/" + nameBox.TabStop + "/" + flagBox.TabStop, "False/False/False/False");
            Check("input: IsInputControl", sgrid.IsInputControl(nameBox) + "/" + sgrid.IsInputControl(flagBox) + "/" + sgrid.IsInputControl(other) + "/" + sgrid.IsInputControl(null), "True/True/False/False");
            Check("input: grid settings", grid.TabAction + "/" + grid.DirectionAfterEnter, "GridNavigation/MoveNone");

            // ---- 入力を始める ----
            grid.Select();
            grid.Row = 0;
            grid.Col = 0;
            sgrid.SelectInput();
            Check("input: column without a control", Active(form) + "/" + sgrid.IsInputActive, "grid/False");

            grid.Col = 1;
            sgrid.SelectInput();
            Application.DoEvents();
            Check("input: SelectInput", Active(form) + "/" + sgrid.IsInputActive + "/" + Shown(form, nameBox), "nameBox/True/True");
            // セルの右と下の 1 ピクセル（罫線）を除いた範囲に重なる。親が違うので、画面上の位置で比べる
            Rectangle cell = grid.RectangleToScreen(grid.Splits[0].GetCellBounds(0, 1));
            Rectangle box = form.RectangleToScreen(nameBox.Bounds);
            Check("input: over the cell", box.Left + "," + box.Top + "," + box.Width, cell.Left + "," + cell.Top + "," + (cell.Width - 1));
            sgrid.SelectInput();
            Check("input: SelectInput while editing", Active(form), "nameBox");

            // ---- 次・前の入力欄へ移る ----
            Check("input: next in the row", sgrid.SelectNextInput(true) + "/" + Active(form) + "/" + grid.Row + "/" + grid.Col + "/" + Shown(form, nameBox), "True/flagBox/0/2/False");
            box = form.RectangleToScreen(flagBox.Bounds);
            cell = grid.RectangleToScreen(grid.Splits[0].GetCellBounds(0, 2));
            Check("input: check box over the cell", box.Left + "," + box.Top, cell.Left + "," + cell.Top);
            Check("input: next row", sgrid.SelectNextInput(true) + "/" + Active(form) + "/" + grid.Row + "/" + grid.Col + "/" + view.CurrentRow.Row["Name"], "True/nameBox/1/1/佐藤");
            Check("input: previous row", sgrid.SelectNextInput(false) + "/" + Active(form) + "/" + grid.Row + "/" + grid.Col, "True/flagBox/0/2");
            Check("input: previous in the row", sgrid.SelectNextInput(false) + "/" + Active(form) + "/" + grid.Row + "/" + grid.Col, "True/nameBox/0/1");
            Check("input: no previous", sgrid.SelectNextInput(false) + "/" + Active(form) + "/" + sgrid.IsInputActive + "/" + Shown(form, nameBox), "False/grid/False/False");

            sgrid.SetRow(2);
            grid.Col = 2;
            sgrid.SelectInput();
            Check("input: no next", Active(form) + " -> " + sgrid.SelectNextInput(true) + "/" + Active(form) + "/" + grid.Row, "flagBox -> False/grid/2");

            // 入力欄の無い列を指しているときは、その行の最初の入力欄へ
            grid.Col = 0;
            Check("input: next from a column without a control", sgrid.SelectNextInput(true) + "/" + Active(form) + "/" + grid.Row + "/" + grid.Col, "True/nameBox/2/1");

            // ---- グリッドへ戻す（入力中だった列を、指している列にする）----
            grid.Col = 0;
            sgrid.Select();
            Check("input: Select", Active(form) + "/" + grid.Col + "/" + sgrid.IsInputActive, "grid/1/False");

            // ---- 移る順番は InputTabIndex。無効のコントロールは飛ばす ----
            nameColumn.InputTabIndex = 5;
            sgrid.SetRow(0);
            grid.Col = 2;
            sgrid.SelectInput();
            Check("input: order by InputTabIndex", sgrid.SelectNextInput(true) + "/" + Active(form) + "/" + grid.Row, "True/nameBox/0");
            Check("input: order by InputTabIndex (next row)", sgrid.SelectNextInput(true) + "/" + Active(form) + "/" + grid.Row, "True/flagBox/1");
            nameColumn.InputTabIndex = 1;
            sgrid.Select();

            flagBox.Enabled = false;
            sgrid.SetRow(0);
            grid.Col = 1;
            sgrid.SelectInput();
            Check("input: skips a disabled control", sgrid.SelectNextInput(true) + "/" + Active(form) + "/" + grid.Row, "True/nameBox/1");
            sgrid.Select();
            grid.Col = 2;
            sgrid.SelectInput();
            Check("input: SelectInput on a disabled control", Active(form) + "/" + sgrid.IsInputActive, "grid/False");
            flagBox.Enabled = true;

            // ---- キー操作: グリッドで Enter → 入力を始める。入力欄で Tab → 次の入力欄 ----
            sgrid.SetRow(0);
            grid.Col = 1;
            grid.Select();
            Application.DoEvents();
            SendMessage(grid.Handle, WM_KEYDOWN, (IntPtr)(int)Keys.Enter, (IntPtr)1);
            Application.DoEvents();
            Check("input: Enter on the grid", Active(form) + "/" + grid.Row, "nameBox/0");
            SendMessage(nameBox.Handle, WM_KEYDOWN, (IntPtr)(int)Keys.Tab, (IntPtr)1);
            Application.DoEvents();
            Check("input: Tab in the control", Active(form) + "/" + grid.Row + "/" + grid.Col, "flagBox/0/2");
            sgrid.Select();

            // ---- グリッドで文字を入力 → 入力を始めて、その文字を渡す ----
            nameBox.Text = "鈴木";
            grid.Col = 1;
            grid.Select();
            Application.DoEvents();
            SendMessage(grid.Handle, WM_CHAR, (IntPtr)'山', IntPtr.Zero);
            Application.DoEvents();
            Check("input: typing on the grid", Active(form) + "/" + nameBox.Text, "nameBox/山");
            // 入力を始めた直後に、続きの文字がグリッドへ届いた場合も、入力用のコントロールへ渡す
            SendMessage(grid.Handle, WM_CHAR, (IntPtr)'田', IntPtr.Zero);
            Application.DoEvents();
            Check("input: following characters", nameBox.Text, "山田");
            sgrid.Select();

            flagBox.Checked = false;
            grid.Col = 2;
            SendMessage(grid.Handle, WM_CHAR, (IntPtr)' ', IntPtr.Zero);
            Application.DoEvents();
            Check("input: Space on the grid toggles the check box", Active(form) + "/" + flagBox.Checked, "flagBox/True");
            sgrid.Select();
            grid.Col = 2;
            SendMessage(grid.Handle, WM_CHAR, (IntPtr)'a', IntPtr.Zero);
            Application.DoEvents();
            Check("input: other characters do not toggle", Active(form) + "/" + flagBox.Checked, "flagBox/True");
            sgrid.Select();

            grid.Col = 0;
            SendMessage(grid.Handle, WM_CHAR, (IntPtr)'x', IntPtr.Zero);
            Application.DoEvents();
            Check("input: typing on a column without a control", Active(form), "grid");

            // セルのダブルクリックで入力を始める動きは、ここでは確かめていない
            // （グリッドが、マウスの左ボタンでは実際のマウス カーソルの位置を見るので、メッセージを送るだけでは行が動かない）

            // ---- ほかのコントロールへフォーカスが移ると、見えない位置へ戻る ----
            sgrid.SetRow(1);
            grid.Col = 1;
            sgrid.SelectInput();
            other.Select();
            Application.DoEvents();
            Check("input: leaves to another control", Active(form) + "/" + sgrid.IsInputActive + "/" + Shown(form, nameBox), "other/False/False");

            // ---- グリッドの大きさが変わると、入力を終える ----
            grid.Select();
            grid.Col = 1;
            sgrid.SelectInput();
            grid.Width -= 20;
            Application.DoEvents();
            Check("input: ends when the grid is resized", Active(form) + "/" + sgrid.IsInputActive, "grid/False");

            // ---- 行が無いときは、入力を始めない ----
            view.RowFilterQuery("Code = 'none'");
            Application.DoEvents();
            sgrid.SelectInput();
            Check("input: no rows", grid.RowCount + "/" + Active(form) + "/" + sgrid.SelectNextInput(true), "0/grid/False");
            view.RowFilterQuery("");

            // ---- 誤った使い方: 同じコントロールを 2 つの列に指定 ----
            var grid2 = new C1TrueDBGrid { Name = "grid2", Location = new Point(12, 16), Size = new Size(200, 100) };
            form.Controls.Add(grid2);
            var sgrid2 = new SGridDB(grid2, form);
            sgrid2.Add(new SGridDBText("Code", "コード", 80) { InputControl = other });
            sgrid2.Add(new SGridDBText("Name", "氏名", 0.0f) { InputControl = other });
            Check("input: one control for two columns", Throws(delegate { sgrid2.EndAdd(new DBView(table, form.BindingContext)); }), "InvalidOperationException");

            form.Close();
        }
    }
}
