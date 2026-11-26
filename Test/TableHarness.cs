// STableComboBox の検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ComponentSUI;

internal static class TableHarness
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
    private const int WM_LBUTTONDOWN = 0x0201;

    private static int _failed;

    [STAThread]
    private static int Main(string[] args)
    {
        SetProcessDPIAware();
        Console.OutputEncoding = new System.Text.UTF8Encoding(false);
        Application.EnableVisualStyles();
        IntPtr previous = GetForegroundWindow();
        try
        {
            if (args.Length > 0 && args[0] == "shot")
            {
                Shot(args[1], args[2]);
                return 0;
            }
            if (args.Length > 0 && args[0] == "bench") { Bench(int.Parse(args[1])); return 0; }
            Tests();
            FixTests();
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
        string a = Convert.ToString(actual), x = Convert.ToString(expected);
        bool ok = a == x;
        if (!ok) _failed++;
        Console.WriteLine((ok ? "ok   " : "NG   ") + name + " => [" + a + "]" + (ok ? "" : " expected [" + x + "]"));
    }

    private static void Key(Control c, Keys key)
    {
        SendMessage(c.Handle, WM_KEYDOWN, (IntPtr)(int)key, (IntPtr)1);
        Application.DoEvents();
    }

    private static void Type(Control c, string chars)
    {
        foreach (char ch in chars) SendMessage(c.Handle, WM_CHAR, (IntPtr)ch, (IntPtr)1);
        Application.DoEvents();
    }

    private static DataTable Banks()
    {
        var dt = new DataTable();
        dt.Columns.Add("Id", typeof(int));
        dt.Columns.Add("Code", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Kana", typeof(string));
        dt.Rows.Add(10, 138, "横浜銀行 藤沢支店", "ﾖｺﾊﾏｷﾞﾝｺｳ ﾌｼﾞｻﾜ");
        dt.Rows.Add(20, 1281, "湘南信用金庫 本店", "ｼｮｳﾅﾝｼﾝﾖｳｷﾝｺ ﾎﾝﾃﾝ");
        dt.Rows.Add(30, 2011, "神奈川県医師信用組合", "ｶﾅｶﾞﾜｹﾝｲｼｼﾝﾖｳｸﾐｱｲ");
        dt.Rows.Add(40, 9900, "ゆうちょ銀行", "ﾕｳﾁｮｷﾞﾝｺｳ");
        dt.Rows.Add(50, 1, "みずほ銀行 横浜支店", "ﾐｽﾞﾎｷﾞﾝｺｳ ﾖｺﾊﾏ");
        dt.Rows.Add(60, 5, "", "ﾅﾏｴﾅｼ");
        return dt;
    }

    private static STableComboBox NewCombo(DataTable dt)
    {
        var c = new STableComboBox();
        c.DataSource = dt;
        c.Sort = "Code";
        c.AddColumn("Code", "コード", 60, HorizontalAlignment.Right);
        c.AddColumn("Name", "銀行名", 170);
        c.AddColumn("Kana", "フリガナ", 170);
        c.DisplayMember = "Name";
        c.ValueMember = "Id";
        c.Width = 260;
        return c;
    }

    /// <summary>
    /// コントロールを画像に描かせて、入力部分に描かれた文字の、上下の高さ（画素）を返す。何も描かれていなければ 0。
    /// フォントを縮めて描いたかどうかを、文字の高さで確かめるのに使う。
    /// </summary>
    private static int InkHeight(STableComboBox combo)
    {
        Application.DoEvents();
        using (var bitmap = new Bitmap(combo.Width, combo.Height))
        {
            combo.DrawToBitmap(bitmap, new Rectangle(0, 0, combo.Width, combo.Height));
            int right = combo.Width - combo.Height - 6;   // 矢印の領域は、幅がおよそ高さと同じ
            Color back = bitmap.GetPixel(right, combo.Height / 2);
            int top = -1, bottom = -1;
            for (int y = 3; y < combo.Height - 3; y++)
            {
                for (int x = 6; x < right; x++)
                {
                    Color c = bitmap.GetPixel(x, y);
                    if (Math.Abs(c.R - back.R) + Math.Abs(c.G - back.G) + Math.Abs(c.B - back.B) > 120)
                    {
                        if (top < 0) top = y;
                        bottom = y;
                        break;
                    }
                }
            }
            return top < 0 ? 0 : bottom - top + 1;
        }
    }

    private static void Tests()
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ShowInTaskbar = false;
            DataTable dt = Banks();
            STableComboBox combo = NewCombo(dt);
            combo.Location = new Point(10, 10);
            var other = new STextBox { Location = new Point(10, 60) };
            form.Controls.Add(combo);
            form.Controls.Add(other);
            form.Show();
            form.Activate();
            combo.Focus();
            Application.DoEvents();
            Check("focused", combo.Focused, true);

            int indexChanged = 0, opened = 0, closed = 0;
            combo.SelectedIndexChanged += delegate { indexChanged++; };
            combo.DropDown += delegate { opened++; };
            combo.DropDownClosed += delegate { closed++; };

            Check("row count (blank name excluded)", combo.RowCount, 5);
            Check("initial", combo.SelectedIndex + "/" + (combo.SelectedValue == null) + "/" + combo.Text, "-1/True/");

            // プログラムからの選択
            combo.SelectedValue = 20;
            Check("select by value", combo.Text + "/" + combo.SelectedIndex + "/" + combo.SelectedValue, "湘南信用金庫 本店/2/20");
            Check("no popup on programmatic", combo.DroppedDown, false);
            combo.SelectedValue = "30";
            Check("select by string value", combo.Text, "神奈川県医師信用組合");
            Check("missing value", combo.SelectByValue(999) + "/" + combo.Text + "/" + combo.SelectedIndex, "False//-1");
            combo.Text = "ゆうちょ銀行";
            Check("Text setter selects", combo.SelectedValue, 40);
            combo.SelectedIndex = 0;
            Check("SelectedIndex setter", combo.Text + "/" + combo.SelectedValue, "みずほ銀行 横浜支店/50");
            Check("GetValue/GetText", combo.GetValue(1) + "/" + combo.GetText(1), "10/横浜銀行 藤沢支店");

            // 入力による絞り込み（ひらがなで入力 → 半角カナの列に一致）
            combo.SelectAll();
            indexChanged = 0;
            Type(combo, "よこ");
            Check("typing opens popup", combo.DroppedDown, true);
            Check("typing clears selection", combo.SelectedValue == null, true);
            Check("typed text kept", combo.Text, "よこ");
            Key(combo, Keys.Enter);
            Check("Enter commits first match", combo.Text + "/" + combo.SelectedValue + "/" + combo.DroppedDown, "みずほ銀行 横浜支店/50/False");
            Check("events while search+commit", indexChanged, 2);

            // 2 行目を選ぶ
            combo.SelectAll();
            Type(combo, "よこ");
            Key(combo, Keys.Down);
            Key(combo, Keys.Enter);
            Check("Down + Enter", combo.Text + "/" + combo.SelectedValue, "横浜銀行 藤沢支店/10");

            // 1 文字は先頭一致。数値の列にも一致する
            combo.SelectAll();
            Type(combo, "1");
            Key(combo, Keys.Enter);
            Check("prefix on number column", combo.SelectedValue, 50);
            combo.SelectAll();
            Type(combo, "12");
            Key(combo, Keys.Enter);
            Check("number prefix 12", combo.SelectedValue, 20);

            // 途中一致（2 文字以上）
            combo.SelectAll();
            Type(combo, "信用");
            Key(combo, Keys.Down);
            Key(combo, Keys.Enter);
            Check("contains", combo.Text, "神奈川県医師信用組合");

            // Esc で取り消し
            combo.SelectAll();
            Type(combo, "ゆう");
            Check("popup open before Esc", combo.DroppedDown, true);
            Key(combo, Keys.Escape);
            Check("Esc reverts", combo.Text + "/" + combo.SelectedValue + "/" + combo.DroppedDown, "神奈川県医師信用組合/30/False");

            // 該当なし
            combo.SelectAll();
            Type(combo, "ZZZ");
            Check("no match closes popup", combo.DroppedDown + "/" + combo.Text, "False/ZZZ");
            other.Focus();
            Application.DoEvents();
            Check("leave without match clears", combo.Text + "/" + combo.SelectedIndex, "/-1");

            // フォーカスの移動で確定
            combo.Focus();
            Application.DoEvents();
            Type(combo, "しょうなん");
            other.Focus();
            Application.DoEvents();
            Check("leave commits candidate", combo.Text + "/" + combo.SelectedValue + "/" + combo.DroppedDown, "湘南信用金庫 本店/20/False");

            // 未選択のまま移動 → 代わりの行
            combo.FallbackSelectedIndex = 1;
            combo.SelectedValue = null;
            combo.Focus();
            other.Focus();
            Application.DoEvents();
            Check("fallback on leave", combo.SelectedValue, 10);
            combo.FallbackSelectedIndex = -1;

            // ↓ で開く・Alt+↓ で閉じる・矢印のクリック
            combo.Focus();
            Application.DoEvents();
            Key(combo, Keys.Down);
            Check("Down opens", combo.DroppedDown, true);
            Key(combo, Keys.Down);
            Key(combo, Keys.Enter);
            Check("open + Down + Enter", combo.SelectedValue, 20);
            combo.DroppedDown = true;
            Check("DroppedDown = true", combo.DroppedDown, true);
            combo.DroppedDown = false;
            int x = combo.ClientSize.Width + 12, y = 5;
            SendMessage(combo.Handle, WM_LBUTTONDOWN, (IntPtr)1, (IntPtr)((y << 16) | x));
            Application.DoEvents();
            Check("arrow click opens", combo.DroppedDown, true);
            SendMessage(combo.Handle, WM_LBUTTONDOWN, (IntPtr)1, (IntPtr)((y << 16) | x));
            Application.DoEvents();
            Check("arrow click closes", combo.DroppedDown, false);
            Check("selection unchanged by arrow", combo.SelectedValue, 20);

            // 条件（RowFilter）で選択中の行が外れる
            combo.RowFilter = "Id <> 20";
            Check("filtered out: selection cleared, text kept", combo.SelectedIndex + "/" + combo.Text + "/" + combo.RowCount, "-1/湘南信用金庫 本店/4");
            combo.RowFilter = "";
            combo.SelectedValue = 40;

            // テーブルの変更が反映される
            dt.Rows.Add(70, 3, "追加銀行", "ﾂｲｶ");
            Check("row added", combo.RowCount, 6);
            DataRow selected = combo.SelectedRow;
            selected.Delete();
            Check("selected row deleted", combo.SelectedIndex + "/" + combo.RowCount, "-1/5");

            // 読み取り専用
            combo.SelectedValue = 10;
            combo.ReadOnly = true;
            combo.Focus();
            Application.DoEvents();
            Key(combo, Keys.Down);
            Check("ReadOnly: no popup", combo.DroppedDown, false);
            SendMessage(combo.Handle, WM_LBUTTONDOWN, (IntPtr)1, (IntPtr)((y << 16) | x));
            Check("ReadOnly: arrow", combo.DroppedDown, false);
            other.Focus();
            Application.DoEvents();
            Check("ReadOnly: leave keeps", combo.SelectedValue, 10);
            combo.ReadOnly = false;

            Check("DropDown/Closed events balanced", opened == closed && opened > 5, true);

            var props = System.ComponentModel.TypeDescriptor.GetProperties(combo);
            foreach (string p in new[] { "HighlightText", "Font", "MaxDropDownItems", "DropDownWidth", "DisplayMember", "ShrinkTextToFit" })
            {
                Check("ShouldSerialize " + p, props[p].ShouldSerializeValue(new STableComboBox()), false);
            }

            // ---- 収まらない文字は、フォーカスがない間（と、無効の間）、縮めて表示する ----
            other.Focus(); Application.DoEvents();
            STableComboBox narrow = NewCombo(Banks());   // ここまでのテストで行を変えた表ではなく、新しい表を使う
            narrow.Location = new Point(10, 150);
            narrow.Width = 150;              // 「神奈川県医師信用組合」（10 文字）は、元の大きさでは収まらない
            form.Controls.Add(narrow);
            Application.DoEvents();
            Check("shrink: default", narrow.ShrinkTextToFit, true);
            narrow.SelectedValue = 30;
            int shrunk = InkHeight(narrow);
            narrow.ShrinkTextToFit = false;
            int normal = InkHeight(narrow);
            Check("shrink: long text is drawn smaller", shrunk > 0 && shrunk < normal, true);
            narrow.ShrinkTextToFit = true;
            Check("shrink: turned on again", InkHeight(narrow), shrunk);
            Check("shrink: the value is untouched", narrow.SelectedValue + "/" + narrow.Text, "30/神奈川県医師信用組合");

            // 収まる文字は、そのままの大きさ
            narrow.SelectedValue = 40;       // 「ゆうちょ銀行」
            int shortShrink = InkHeight(narrow);
            narrow.ShrinkTextToFit = false;
            Check("shrink: short text keeps the size", narrow.Text + "/" + (shortShrink == normal) + "/" + (InkHeight(narrow) == normal), "ゆうちょ銀行/True/True");
            narrow.ShrinkTextToFit = true;

            // 無効のときも、縮める
            narrow.SelectedValue = 30;
            narrow.Enabled = false;
            int disabledShrunk = InkHeight(narrow);
            narrow.ShrinkTextToFit = false;
            int disabledNormal = InkHeight(narrow);
            Check("shrink: disabled text is drawn smaller", disabledShrunk > 0 && disabledShrunk < disabledNormal, true);
            narrow.ShrinkTextToFit = true;
            narrow.Enabled = true;

            // フォーカスがある間は、元の大きさで表示する（入力しやすいように）。フォーカスが外れると、また縮める
            narrow.HighlightText = false;    // 全選択の色で、文字の高さが測れなくなるのを避ける
            narrow.Focus(); Application.DoEvents();
            narrow.SelectionLength = 0;
            Check("shrink: normal size while focused", InkHeight(narrow), normal);
            other.Focus(); Application.DoEvents();
            Check("shrink: smaller again after leaving", InkHeight(narrow), shrunk);
            form.Close();
        }
    }

    private static double Ms(Action a)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        a();
        sw.Stop();
        return sw.Elapsed.TotalMilliseconds;
    }

    private static void Fill(DataTable dt, int count)
    {
        string[] names = { "横浜銀行", "湘南信用金庫", "神奈川県医師信用組合", "ゆうちょ銀行", "みずほ銀行", "三井住友銀行", "三菱ＵＦＪ銀行", "りそな銀行" };
        string[] kanas = { "ﾖｺﾊﾏｷﾞﾝｺｳ", "ｼｮｳﾅﾝｼﾝﾖｳｷﾝｺ", "ｶﾅｶﾞﾜｹﾝｲｼｼﾝﾖｳｸﾐｱｲ", "ﾕｳﾁｮｷﾞﾝｺｳ", "ﾐｽﾞﾎｷﾞﾝｺｳ", "ﾐﾂｲｽﾐﾄﾓｷﾞﾝｺｳ", "ﾐﾂﾋﾞｼﾕｰｴﾌｼﾞｪｲｷﾞﾝｺｳ", "ﾘｿﾅｷﾞﾝｺｳ" };
        dt.BeginLoadData();
        for (int i = 0; i < count; i++)
        {
            dt.Rows.Add(1000 + i, i, names[i % names.Length] + " 第" + i + "支店", kanas[i % kanas.Length] + " ﾀﾞｲ" + i + "ｼﾃﾝ");
        }
        dt.EndLoadData();
    }

    private static void Bench(int count)
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ShowInTaskbar = false;
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Code", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Kana", typeof(string));
            Fill(dt, count);
            STableComboBox combo = NewCombo(dt);
            combo.Location = new Point(10, 10);
            var other = new STextBox { Location = new Point(10, 60) };
            form.Controls.Add(combo);
            form.Controls.Add(other);
            form.Show();
            form.Activate();
            combo.Focus();
            Application.DoEvents();

            Console.WriteLine("rows = " + count);
            Console.WriteLine("  RowCount (first, builds row list): {0:F1} ms", Ms(delegate { int n = combo.RowCount; }));
            Console.WriteLine("  SelectedValue = x (first, builds map): {0:F1} ms", Ms(delegate { combo.SelectedValue = 1000 + count / 2; }));
            Console.WriteLine("  SelectedValue = x (again): {0:F3} ms", Ms(delegate { combo.SelectedValue = 1000 + count / 3; }));
            Console.WriteLine("  SelectedValue get: {0:F3} ms", Ms(delegate { object v = combo.SelectedValue; }));
            Console.WriteLine("  SelectedIndex get: {0:F3} ms", Ms(delegate { int v = combo.SelectedIndex; }));
            Console.WriteLine("  Text = name (first, builds map): {0:F1} ms", Ms(delegate { combo.Text = "横浜銀行 第8支店"; }));
            combo.SelectAll();
            string typed = "よこはまぎん";
            for (int i = 0; i < typed.Length; i++)
            {
                char c = typed[i];
                Console.WriteLine("  type '{0}' (text '{1}'): {2:F1} ms  popup={3}", c, typed.Substring(0, i + 1), Ms(delegate { Type(combo, c.ToString()); }), combo.DroppedDown);
            }
            Console.WriteLine("  Down x5: {0:F2} ms", Ms(delegate { for (int i = 0; i < 5; i++) Key(combo, Keys.Down); }));
            Console.WriteLine("  Enter (commit): {0:F2} ms  -> {1}", Ms(delegate { Key(combo, Keys.Enter); }), combo.Text);
            combo.SelectAll();
            Console.WriteLine("  type '7' (prefix, number column): {0:F1} ms", Ms(delegate { Type(combo, "7"); }));
            Key(combo, Keys.Escape);
            Console.WriteLine("  open list (all rows): {0:F1} ms", Ms(delegate { combo.DroppedDown = true; Application.DoEvents(); }));
            Console.WriteLine("  PageDown x20: {0:F1} ms", Ms(delegate { for (int i = 0; i < 20; i++) Key(combo, Keys.PageDown); }));
            combo.DroppedDown = false;
            Console.WriteLine("  RowFilter set: {0:F1} ms", Ms(delegate { combo.RowFilter = "Code >= 10"; }));
            Console.WriteLine("  RowCount after filter: {0:F1} ms ({1})", Ms(delegate { int n = combo.RowCount; }), combo.RowCount);
            combo.RowFilter = "";

            // テーブルの一括更新（コントロールが付いた状態）
            Console.WriteLine("  change 1 row + read SelectedValue: {0:F1} ms", Ms(delegate { dt.Rows[5]["Name"] = "変更"; object v = combo.SelectedValue; }));
            var plain = new DataTable();
            plain.Columns.Add("Id", typeof(int)); plain.Columns.Add("Code", typeof(int)); plain.Columns.Add("Name", typeof(string)); plain.Columns.Add("Kana", typeof(string));
            double baseline = Ms(delegate { Fill(plain, count); });
            var withView = new DataTable();
            withView.Columns.Add("Id", typeof(int)); withView.Columns.Add("Code", typeof(int)); withView.Columns.Add("Name", typeof(string)); withView.Columns.Add("Kana", typeof(string));
            var plainView = new DataView(withView) { Sort = "Code" };
            double viewOnly = Ms(delegate { Fill(withView, count); });
            dt.Clear();
            double attached = Ms(delegate { Fill(dt, count); });
            Console.WriteLine("  bulk load {0} rows: no view {1:F0} ms / plain DataView {2:F0} ms / with STableComboBox {3:F0} ms", count, baseline, viewOnly, attached);
            Console.WriteLine("  managed memory: {0:F1} MB", GC.GetTotalMemory(true) / 1048576.0);
            form.Close();
        }
    }

    private static void Shot(string closedPng, string openPng)
    {
        using (var form = new Form())
        {
            form.Text = "STableComboBox 検証";
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(40, 40);
            form.ClientSize = new Size(560, 380);
            form.BackColor = Color.FromArgb(244, 252, 254);
            form.TopMost = true;
            DataTable dt = Banks();
            for (int i = 0; i < 12; i++) dt.Rows.Add(100 + i, 3000 + i, "テスト銀行 第" + (i + 1) + "支店", "ﾃｽﾄｷﾞﾝｺｳ");

            int y = 14;
            Func<string, Control, Control> add = delegate(string label, Control box)
            {
                var l = new Label { Text = label, Location = new Point(12, y + 5), AutoSize = true, Font = new STextBox().Font };
                box.Location = new Point(150, y);
                form.Controls.Add(l);
                form.Controls.Add(box);
                y += box.Height + 8;
                return box;
            };

            STableComboBox a = NewCombo(dt); a.SelectedValue = 10;
            add("通常", a);
            SComboBox s = new SComboBox { Width = 260 }; s.AddItem("横浜銀行 藤沢支店", 1); s.SelectedIndex = 0;
            add("SComboBox(比較)", s);
            STableComboBox d = NewCombo(dt); d.SelectedValue = 30; d.Enabled = false;
            add("無効", d);
            STableComboBox r = NewCombo(dt); r.SelectedValue = 40; r.ReadOnly = true;
            add("読み取り専用", r);
            STableComboBox open = NewCombo(dt);
            add("入力して絞り込み", open);

            form.Show();
            form.Activate();
            a.Focus();
            Application.DoEvents();
            System.Threading.Thread.Sleep(250);
            Application.DoEvents();
            Capture(form, closedPng);

            open.Focus();
            Application.DoEvents();
            Type(open, "ぎんこう");
            Key(open, Keys.Down);
            Application.DoEvents();
            System.Threading.Thread.Sleep(400);
            Application.DoEvents();
            Capture(form, openPng);
            open.DroppedDown = false;
            form.Close();
        }
    }

    private static void Capture(Form form, string path)
    {
        Rectangle r = form.RectangleToScreen(form.ClientRectangle);
        using (var bmp = new Bitmap(r.Width, r.Height))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(r.Location, Point.Empty, r.Size);
            }
            bmp.Save(path, ImageFormat.Png);
        }
    }

    // ---- レビューで見つかった不具合の再発防止 ----
    private static void FixTests()
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ShowInTaskbar = false;
            DataTable dt = Banks();
            var other = new TextBox { Location = new Point(10, 10) };
            STableComboBox combo = NewCombo(dt);
            combo.Location = new Point(10, 50);
            form.Controls.Add(other);
            form.Controls.Add(combo);
            form.Show();
            form.Activate();
            Application.DoEvents();

            // 並びはコード順: Id 50, 10, 20, 30, 40（Id 60 は表示する文字列が空なので一覧に出ない）
            combo.SelectedValue = 50;
            int changed = 0;
            combo.SelectedValueChanged += delegate { changed++; };

            // 文字を入力せずに ↓ で開き、矢印で次の行を指して、フォーカスを移す → 指した行を選ぶ
            combo.Focus();
            Application.DoEvents();
            Key(combo, Keys.Down);
            Key(combo, Keys.Down);
            // Tab キーでの移動と同じ順序（Leave が先、LostFocus が後）にするため、Focus ではなく Select を使う
            other.Select();
            Application.DoEvents();
            Check("arrow then leave selects the highlighted row", combo.SelectedValue + "/" + changed + "/" + combo.Text, "10/1/横浜銀行 藤沢支店");

            // 開いただけで動かさずにフォーカスを移す → 変わらない
            combo.Focus();
            Application.DoEvents();
            Key(combo, Keys.Down);
            other.Select();
            Application.DoEvents();
            Check("open then leave keeps the selection", combo.SelectedValue + "/" + changed, "10/1");

            // マウスでほかのコントロールをクリックした場合（一覧が先に閉じる）は、指していた行を選ばない
            combo.Focus();
            Application.DoEvents();
            Key(combo, Keys.Down);
            Key(combo, Keys.Down);
            other.Focus();
            Application.DoEvents();
            Check("arrow then click elsewhere keeps the selection", combo.SelectedValue + "/" + changed, "10/1");

            // 入力の途中でも、フォーカスを動かさずに確定できる（ファンクション キーでの保存の前に呼ぶ）
            combo.Focus();
            Application.DoEvents();
            combo.SelectAll();
            Type(combo, "ゆう");
            Check("pending search has no selection yet", combo.SelectedValue == null, true);
            combo.CommitEdit();
            Check("CommitEdit", combo.SelectedValue + "/" + combo.Text + "/" + combo.Focused + "/" + combo.DroppedDown, "40/ゆうちょ銀行/True/False");
            Check("ISEditCommit", ((object)combo) is ISEditCommit, true);
            other.Focus();
            Application.DoEvents();

            // 選択中の行が一覧から外れたら、選択の変更イベントを発生させて選択を外す。
            // テーブルが変更を通知している最中には発生させず、値を読んだ時点で確かめる
            DataRow row40 = dt.Select("Id = 40")[0];
            changed = 0;
            row40["Name"] = "";
            Check("dropout: no event inside the table notification", changed, 0);
            Check("dropout: checked when the value is read", (combo.SelectedValue == null) + "/" + changed + "/" + combo.SelectedIndex, "True/1/-1");
            Application.DoEvents();
            Check("dropout: event is raised once", changed, 1);

            // 値を読まなくても、メッセージの処理が一段落すれば確かめる
            row40["Name"] = "ゆうちょ銀行";
            combo.SelectedValue = 40;
            changed = 0;
            row40.Delete();
            Check("deleted row: not yet", changed, 0);
            Application.DoEvents();
            Check("deleted row: deferred check", changed + "/" + (combo.SelectedRow == null) + "/" + (combo.SelectedValue == null), "1/True/True");

            // ---- 表示する行が無いときに一覧を開こうとすると、理由を表示した一覧が開く ----
            STableComboBox emptyCombo = NewCombo(dt.Clone());
            emptyCombo.Location = new Point(10, 90);
            form.Controls.Add(emptyCombo);
            emptyCombo.Focus();
            Application.DoEvents();
            Key(emptyCombo, Keys.Down);
            Check("no rows: list opens with a message", emptyCombo.DroppedDown + "/" + emptyCombo.NoDataText, "True/選択できるデータがありません");
            Key(emptyCombo, Keys.Enter);
            Check("no rows: Enter closes the list, nothing is selected", emptyCombo.DroppedDown + "/" + (emptyCombo.SelectedValue == null), "False/True");

            // 入力した文字に当てはまる行が無いとき: 入力中は一覧を閉じる。↓ で開こうとすると、理由を表示する
            combo.SelectedValue = 10;
            combo.Focus();
            Application.DoEvents();
            combo.SelectAll();
            Type(combo, "ｚｚｚ");
            Check("no match: list is closed while typing", combo.DroppedDown, false);
            Key(combo, Keys.Down);
            Check("no match: list opens with a message", combo.DroppedDown + "/" + combo.NoMatchText, "True/該当するデータがありません");
            Key(combo, Keys.Escape);
            Check("no match: Esc restores the selection", combo.DroppedDown + "/" + combo.SelectedValue, "False/10");

            // 一覧の背景は、フォーカスがあるときの背景色には連動しない
            Check("list back color ignores the focus fill", (combo.BackColor == combo.FocusBackColor) + "/" + (combo.NormalBackColor.ToArgb() == SystemColors.Window.ToArgb()), "True/True");
            form.Close();
        }
    }
}
