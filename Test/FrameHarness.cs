// FormFrame と S 系のコントロールの組み合わせの検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
// FormFrame を継承した検証用のフォームに S 系のコントロールを置き、Enter での項目の移動、一覧・カレンダーを開いている間の Enter、
// ファンクション キーでの実行（フォーカスを動かさない）で、入力の途中の内容が確定されることを確かめる。
// ビルドした FMKaiin.exe の中のクラスを使うので、先に FMKaiin をビルドしておくこと。データベースには接続しない。
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using App;
using ComponentSUI;

internal static class FrameHarness
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

    private static int _failed;

    /// <summary>FormFrame を継承した、検証用のフォーム。</summary>
    private sealed class TestForm : FormFrame
    {
        public readonly STextBox Text1 = new STextBox();
        public readonly SDateBox Date = new SDateBox { CalendarKind = SDateCalendarKind.Western };
        public readonly STableComboBox Bank = new STableComboBox();
        public readonly SComboBox Kind = new SComboBox();
        public readonly SNumberBox Cost = new SNumberBox();
        public readonly STextBox Last = new STextBox();
        public readonly FunctionKeyBar Bar = new FunctionKeyBar();

        public readonly DataTable Rows = new DataTable();
        public SControlDB Db;
        public int Saved;
        public string ValueAtSave = string.Empty;

        public TestForm()
        {
            // FormFrame を継承するフォームの Designer と同じ指定
            AutoScaleMode = AutoScaleMode.Font;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            ClientSize = new Size(420, 320);
            StartPosition = FormStartPosition.Manual;
            Location = new Point(-3000, -3000);
            ShowInTaskbar = false;
            ShownMask = false;

            var banks = new DataTable();
            banks.Columns.Add("Id", typeof(int));
            banks.Columns.Add("Name", typeof(string));
            banks.Rows.Add(10, "横浜銀行");
            banks.Rows.Add(20, "湘南信用金庫");
            banks.Rows.Add(30, "みずほ銀行");
            Bank.DataSource = banks;
            Bank.AddColumn("Name", "銀行名", 200);
            Bank.DisplayMember = "Name";
            Bank.ValueMember = "Id";

            Kind.AddItem("西暦", true);
            Kind.AddItem("和暦", false);

            Control[] inputs = { Text1, Date, Bank, Kind, Cost, Last };
            int y = 10;
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].Location = new Point(10, y);
                inputs[i].Width = 200;
                inputs[i].TabIndex = i;
                Controls.Add(inputs[i]);
                y += 34;
            }
            Controls.Add(Bar);

            Rows.Columns.Add("Name", typeof(string));
            Rows.Columns.Add("Birth", typeof(DateTime));
            Rows.Columns.Add("BankId", typeof(int));
            Rows.Columns.Add("Seireki", typeof(bool));
            Rows.Columns.Add("Cost", typeof(int));
            Rows.Rows.Add("藤沢", new DateTime(1965, 3, 3), 10, false, 1000);
            Rows.AcceptChanges();
        }

        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(Bar, FuncMasterBasic.Functions);
            funcKeys.On(FuncMasterBasic.Save, delegate
            {
                // 登録の処理が読む値（フォーカスは動いていない）
                Saved++;
                ValueAtSave = (Date.Value.HasValue ? Date.Value.Value.ToString("yyyy-MM-dd") : "null") + "/" + Bank.SelectedValue;
            });
        }

        protected override void FormFrame_Shown(object sender, EventArgs e)
        {
            Db = new SControlDB(this, delegate { return Rows.Rows[0]; });
            Db.Add(new SControlDBText("Name", Text1));
            Db.Add(new SControlDBDate("Birth", Date));
            Db.Add(new SControlDBCombo("BankId", Bank));
            Db.Add(new SControlDBCombo("Seireki", Kind));
            Db.Add(new SControlDBNumber("Cost", Cost));
            Db.EndAdd();
            Db.FetchAll();

            base.FormFrame_Shown(sender, e);
        }

        public FunctionKeyBinder Keys_ { get { return funcKeys; } }
    }

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

    private static void Key(Control c, Keys key)
    {
        SendMessage(c.Handle, WM_KEYDOWN, (IntPtr)(int)key, (IntPtr)1);
        if (key == Keys.Enter)
        {
            // 実際のキー入力では、KeyDown に続いて文字（改行）も届く
            SendMessage(c.Handle, WM_CHAR, (IntPtr)13, (IntPtr)1);
        }
        Application.DoEvents();
    }

    private static void Type(Control c, string chars)
    {
        foreach (char ch in chars) SendMessage(c.Handle, WM_CHAR, (IntPtr)ch, (IntPtr)1);
        Application.DoEvents();
    }

    private static string Active(Form form)
    {
        return form.ActiveControl == null ? "null" : form.ActiveControl.GetType().Name;
    }

    private static void Tests()
    {
        using (var form = new TestForm())
        {
            form.Show();
            form.Activate();
            Application.DoEvents();

            Check("fetched", form.Text1.Text + "/" + form.Date.Value.Value.ToString("yyyy-MM-dd") + "/" + form.Bank.Text + "/" + form.Kind.Text + "/" + form.Cost.Value, "藤沢/1965-03-03/横浜銀行/和暦/1000");

            // 表示倍率に合わせた拡大（FormFrame がロード時に行う）で、入力欄の大きさが変わっても、文字が収まっている
            float scale;
            using (Graphics g = form.CreateGraphics()) scale = g.DpiX / 96f;
            Check("scaled by FormFrame", Math.Abs(form.Text1.Width - 200 * scale) <= 2f, true);
            Check("text fits in the scaled box", form.Text1.ClientSize.Height >= TextRenderer.MeasureText("あg", form.Text1.Font, Size.Empty, TextFormatFlags.NoPadding).Height, true);

            // ---- Enter で次の項目へ移る（FormFrame の処理）。移るときに、入力の途中の内容が確定して、行に書き戻される ----
            form.Text1.Focus();
            Application.DoEvents();
            Key(form.Text1, Keys.Enter);
            Check("Enter moves to the next control", Active(form), "SDateBox");

            Type(form.Date, "19700102");
            Key(form.Date, Keys.Enter);
            Check("Enter on the date box: moved / committed / written", Active(form) + "/" + form.Date.Value.Value.ToString("yyyy-MM-dd") + "/" + ((DateTime)form.Rows.Rows[0]["Birth"]).ToString("yyyy-MM-dd"), "STableComboBox/1970-01-02/1970-01-02");

            // ---- 一覧を開いている間の Enter は、指している行の確定に使う（フォームは次の項目へ移らない）----
            Key(form.Bank, Keys.Down);
            Check("list opened", form.Bank.DroppedDown, true);
            Key(form.Bank, Keys.Down);
            Key(form.Bank, Keys.Enter);
            Check("Enter in the open list: stays / selected / closed", Active(form) + "/" + form.Bank.SelectedValue + "/" + form.Bank.DroppedDown, "STableComboBox/20/False");
            Key(form.Bank, Keys.Enter);
            Check("next Enter moves on and writes the row", Active(form) + "/" + form.Rows.Rows[0]["BankId"], "SComboBox/20");

            // ---- コンボ ボックス: 一覧を閉じているときの Enter は、次の項目へ移る ----
            Key(form.Kind, Keys.Enter);
            Check("Enter on the combo box moves on", Active(form), "SNumberBox");

            // ---- カレンダーを開いている間の Enter は、指している日の確定に使う ----
            form.Date.Focus();
            Application.DoEvents();
            Key(form.Date, Keys.Down);
            Check("calendar opened", form.Date.DroppedDown, true);
            Key(form.Date, Keys.Right);
            Key(form.Date, Keys.Enter);
            Check("Enter in the open calendar: stays / chosen / closed", Active(form) + "/" + form.Date.Value.Value.ToString("yyyy-MM-dd") + "/" + form.Date.DroppedDown, "SDateBox/1970-01-03/False");

            // ---- ファンクション キー（フォーカスは動かない）: 実行するメソッドは、入力の途中の内容を確定した後の値を読む ----
            Type(form.Date, "19800305");
            Check("pending date before the function key", form.Date.Value.Value.ToString("yyyy-MM-dd"), "1970-01-03");
            form.Keys_.Exec(Keys.F11);
            Check("function key: date is committed before the handler", form.Saved + "/" + form.ValueAtSave + "/" + Active(form), "1/1980-03-05/20/SDateBox");

            form.Bank.Focus();
            Application.DoEvents();
            form.Bank.SelectAll();
            Type(form.Bank, "みずほ");
            Check("pending table combo before the function key", form.Bank.SelectedValue == null, true);
            form.Keys_.Exec(Keys.F11);
            Check("function key: table combo is committed before the handler", form.Saved + "/" + form.ValueAtSave + "/" + Active(form), "2/1980-03-05/30/STableComboBox");

            // SControlDB.Update()（保存の処理が呼ぶ）で、フォーカスがある入力欄の値が行に書き戻される
            form.Db.Update();
            Check("Update() writes the active control", form.Rows.Rows[0]["BankId"], 30);

            form.Close();
        }
    }
}
