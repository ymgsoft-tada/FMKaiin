// SControlDB の検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using App;
using App.Controls;
using ComponentGControlDB;

internal static class DbHarness
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    private static extern bool SetProcessDPIAware();

    private const int WM_CHAR = 0x0102;
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
            RuleTests();
            DbTests();
            FixTests();
            PostTests();
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

    private static void Type(Control c, string chars)
    {
        foreach (char ch in chars) SendMessage(c.Handle, WM_CHAR, (IntPtr)ch, (IntPtr)1);
        Application.DoEvents();
    }

    private static string Describe(STextBox box)
    {
        return box.MaxLength + "/" + box.Format + "/" + box.AllowSpace + "/" + box.ImeMode + "/" + box.TextAlign + "/" + box.HighlightText + "/" + box.AcceptsCrLf + "/" + box.MaxLengthUnit;
    }

    private static void RuleTests()
    {
        // S 系のルール
        var text = new STextBox();
        SControlRule.Text(30).Apply(text);
        Check("rule Text", Describe(text), "30//Both/Hiragana/Left/True/Filter/TextElement");

        var code = new STextBox();
        SControlRule.Number(4, 0).Apply(code);
        Check("rule Number on STextBox", Describe(code), "4/9/None/Disable/Right/True/Filter/TextElement");

        var num = new SNumberBox();
        SControlRule cur = SControlRule.Currency(7, 0);
        cur.Parts[0].AllowBlank = true;
        cur.Apply(num);
        Check("rule Currency on SNumberBox", num.MaxValue + "/" + num.MinValue + "/" + num.DecimalPlaces + "/" + num.ThousandsSeparator + "/" + num.AllowBlank, "9999999/-9999999/0/True/True");

        var t1 = new STextBox(); var t2 = new STextBox(); var t3 = new STextBox();
        SControlRule.HyphenSplit(5, 4, 5).Apply(t1, t2, t3);
        Check("rule HyphenSplit", t1.MaxLength + "/" + t2.MaxLength + "/" + t3.MaxLength + "/" + t2.Format + "/" + t2.ImeMode, "5/4/5/9/Disable");

        var name = new STextBox(); var kana = new STextBox();
        SControlRule.Ruby(15, 30).Apply(name, kana);
        Check("rule Ruby", Describe(name) + " | " + Describe(kana), "15/^@/Both/Hiragana/Left/True/Filter/TextElement | 30/H/Narrow/KatakanaHalf/Left/True/Filter/TextElement");

        var date = new SDateBox();
        SControlRule.Date().Apply(date);
        Check("rule Date", date.TextAlign + "/" + date.HighlightText, "Center/True");

        // 対応表
        var table = new SControlDBRule();
        table.Add(SControlRule.Text(20), "Name");
        Check("rule lookup", table.GetRule("Name").Parts[0].MaxLength + "/" + (table.GetRule("None") == null), "20/True");
        bool duplicateRejected = false;
        try { table.Add(SControlRule.Text(40), "Name"); } catch (ArgumentException) { duplicateRejected = true; }
        Check("rule duplicate is rejected", duplicateRejected, true);

        // 既存のルール（GControlDBRule）の読み替え
        var g = new GControlDBRule();
        g.Add(new GControlDBRuleText("GName", 30));
        var gKana = new GControlDBRuleText("GKana", 20);
        gKana.Format[0] = "H";
        gKana.AllowSpace[0] = GrapeCity.Win.Editors.AllowSpace.Narrow;
        gKana.ImeMode[0] = ImeMode.KatakanaHalf;
        g.Add(gKana);
        var gCode = new GControlDBRuleNumber("GCode", 4, 0);
        g.Add(gCode);
        g.Add(gCode, "GCode2");
        var gCur = new GControlDBRuleCurrency("GCost", 7, 0);
        gCur.AlternateBlank[0] = true;
        g.Add(gCur);
        g.Add(new GControlDBRuleHyphenSplit("GTel", new[] { 5, 4, 5 }));
        g.Add(new GControlDBRuleRuby("GRuby", new[] { 15, 30 }));
        g.Add(new GControlDBRuleDate("GDate"));
        var gPost = new GControlDBRulePostAddr();
        gPost.MaxLength[2] = 30;
        g.Add(gPost, "GPost");

        SControlDBRule wrapped = SControlDBRuleConverter.Wrap(g);
        var c1 = new STextBox(); wrapped.SetControlByRule(c1, "GName");
        Check("converted Text", Describe(c1), "30//Both/Hiragana/Left/True/Filter/TextElement");
        var c2 = new STextBox(); wrapped.SetControlByRule(c2, "GKana");
        Check("converted kana", Describe(c2), "20/H/Narrow/KatakanaHalf/Left/True/Filter/TextElement");
        var c3 = new STextBox(); wrapped.SetControlByRule(c3, "GCode");
        Check("converted Number on STextBox", Describe(c3), "4/9/None/Disable/Right/True/Filter/TextElement");
        Check("converted rule is cached", ReferenceEquals(wrapped.GetRule("GCode"), wrapped.GetRule("GCode2")), true);
        var c4 = new SNumberBox(); wrapped.SetControlByRule(c4, "GCost");
        Check("converted Currency", c4.MaxValue + "/" + c4.MinValue + "/" + c4.ThousandsSeparator + "/" + c4.AllowBlank, "9999999/-9999999/True/True");
        var h1 = new STextBox(); var h2 = new STextBox(); var h3 = new STextBox();
        wrapped.SetControlByRule(new Control[] { h1, h2, h3 }, "GTel");
        Check("converted HyphenSplit", h1.MaxLength + "/" + h2.MaxLength + "/" + h3.MaxLength + "/" + h3.Format + "/" + h3.TextAlign, "5/4/5/9/Right");
        var r1 = new STextBox(); var r2 = new STextBox();
        wrapped.SetControlByRule(new Control[] { r1, r2 }, "GRuby");
        Check("converted Ruby", r1.MaxLength + "/" + r1.Format + "/" + r2.MaxLength + "/" + r2.Format + "/" + r2.ImeMode, "15/^@/30/H/KatakanaHalf");
        var p1 = new STextBox(); var p2 = new STextBox(); var p3 = new STextBox();
        wrapped.SetControlByRule(new Control[] { p1, p2, p3 }, "GPost");
        Check("converted PostAddr", p1.MaxLength + "/" + p2.MaxLength + "/" + p3.MaxLength + "/" + p3.Format + "/" + p3.HighlightText, "3/4/30/^@/False");
        var d1 = new SDateBox(); wrapped.SetControlByRule(d1, "GDate");
        Check("converted Date", d1.TextAlign, "Center");
        Check("converted missing", wrapped.GetRule("Nothing") == null, true);
    }

    private static DataTable CreateTable()
    {
        var dt = new DataTable();
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Kana", typeof(string));
        dt.Columns.Add("Tel", typeof(string));
        dt.Columns.Add("Post", typeof(string));
        dt.Columns.Add("Addr", typeof(string));
        dt.Columns.Add("Cost", typeof(int));
        dt.Columns.Add("Rate", typeof(decimal));
        dt.Columns.Add("Code", typeof(int));
        dt.Columns.Add("Birth", typeof(DateTime));
        dt.Columns.Add("Kbn", typeof(short));
        dt.Columns.Add("BankId", typeof(int));
        dt.Columns.Add("Flag", typeof(bool));
        dt.Columns.Add("Memo", typeof(string));
        dt.Rows.Add("藤沢 太郎", "ﾌｼﾞｻﾜ ﾀﾛｳ", "0466-22-3041", "251-0032", "神奈川県藤沢市片瀬", 36000, 1.5m, 12, new DateTime(1965, 3, 3), (short)2, 20, true, "メモ");
        dt.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);
        dt.AcceptChanges();
        return dt;
    }

    private static void DbTests()
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ShowInTaskbar = false;

            DataTable dt = CreateTable();
            int rowIndex = 0;

            var banks = new DataTable();
            banks.Columns.Add("Id", typeof(int));
            banks.Columns.Add("Name", typeof(string));
            banks.Rows.Add(10, "横浜銀行");
            banks.Rows.Add(20, "湘南信用金庫");

            var iName = new STextBox(); var iKana = new STextBox();
            var iTel1 = new STextBox(); var iTel2 = new STextBox(); var iTel3 = new STextBox();
            var iPost1 = new STextBox(); var iPost2 = new STextBox(); var iAddr = new STextBox();
            var iCost = new SNumberBox(); var iRate = new SNumberBox(); var iCode = new STextBox();
            var iBirth = new SDateBox { CalendarKind = SDateCalendarKind.Western };
            var iKbn = new SComboBox(); iKbn.AddItem("正会員", 1); iKbn.AddItem("準会員", 2);
            var iBank = new STableComboBox { DataSource = banks, DisplayMember = "Name", ValueMember = "Id" };
            var iFlag = new CheckBox();
            var iMemo = new STextBox(); var iMemoCopy = new STextBox();
            var other = new STextBox();
            Control[] all = { iName, iKana, iTel1, iTel2, iTel3, iPost1, iPost2, iAddr, iCost, iRate, iCode, iBirth, iKbn, iBank, iFlag, iMemo, iMemoCopy, other };
            int y = 5;
            foreach (Control c in all) { c.Location = new Point(5, y); y += 30; form.Controls.Add(c); }
            form.ClientSize = new Size(300, y + 10);

            // DataRow を返す関数を渡す（GControlDB と同じ書き方）
            var db = new SControlDB(form, delegate { return rowIndex < 0 ? null : dt.Rows[rowIndex]; });
            db.Add(new SControlDBRuby(new[] { "Name", "Kana" }, new[] { iName, iKana }));
            db.Add(new SControlDBHyphenSplit("Tel", new[] { iTel1, iTel2, iTel3 }));
            db.Add(new SControlDBPostAddr(new[] { "Post", "Addr" }, new[] { iPost1, iPost2, iAddr }));
            db.Add(new SControlDBNumber("Cost", iCost));
            db.Add(new SControlDBNumber("Rate", iRate));
            db.Add(new SControlDBNumber("Code", iCode));
            db.Add(new SControlDBDate("Birth", iBirth));
            db.Add(new SControlDBCombo("Kbn", iKbn));
            db.Add(new SControlDBCombo("BankId", iBank));
            db.Add(new SControlDBCheckBox("Flag", iFlag));
            db.Add(new SControlDBText("Memo", iMemo));
            db.Add(new SControlDBText("Memo", iMemoCopy));

            var rule = new SControlDBRule();
            rule.Add(SControlRule.Ruby(15, 30), "Name");
            rule.Add(SControlRule.HyphenSplit(5, 4, 5), "Tel");
            rule.Add(SControlRule.PostAddr(30, 50), "Post");
            SControlRule cost = SControlRule.Currency(7, 0);
            cost.Parts[0].AllowBlank = true;
            rule.Add(cost, "Cost");
            rule.Add(SControlRule.Number(3, 1), "Rate");
            rule.Add(SControlRule.Number(4, 0), "Code");
            rule.Add(SControlRule.Text(20), "Memo");
            db.EndAdd(rule);

            db.FetchAll();
            int updated = 0; string lastField = "";
            db.FieldUpdated += delegate(ISRow r, string f, object v) { updated++; lastField = f; };

            form.Show();
            form.Activate();
            other.Focus();
            Application.DoEvents();
            dt.AcceptChanges();
            updated = 0;

            Check("rules applied by EndAdd", iName.MaxLength + "/" + iKana.Format + "/" + iTel2.MaxLength + "/" + iPost2.MaxLength + "/" + iCost.MaxValue + "/" + iRate.DecimalPlaces + "/" + iCode.Format + "/" + iMemo.MaxLength,
                "15/H/4/4/9999999/1/9/20");
            Check("control list", db.ControlList.Count, 17);

            // 読み込み
            db.FetchAll();
            Check("fetch text/ruby", iName.Text + "|" + iKana.Text, "藤沢 太郎|ﾌｼﾞｻﾜ ﾀﾛｳ");
            Check("fetch hyphen", iTel1.Text + "|" + iTel2.Text + "|" + iTel3.Text, "0466|22|3041");
            Check("fetch post", iPost1.Text + "|" + iPost2.Text + "|" + iAddr.Text, "251|0032|神奈川県藤沢市片瀬");
            Check("fetch number", iCost.Value + "|" + iCost.Text + "|" + iRate.Text + "|" + iCode.Text, "36000|36,000|1.5|12");
            Check("fetch date", iBirth.Text, "1965年03月03日");
            Check("fetch combo", iKbn.Text + "|" + iBank.Text, "準会員|湘南信用金庫");
            Check("fetch check", iFlag.Checked, true);
            Check("fetch same field twice", iMemo.Text + "|" + iMemoCopy.Text, "メモ|メモ");
            Check("fetch does not update row", updated + "/" + (dt.GetChanges() == null), "0/True");

            // 書き戻し（フォーカスが外れたとき）
            iMemo.Focus(); Application.DoEvents();
            Type(iMemo, "変更後");
            other.Focus(); Application.DoEvents();
            Check("update text", dt.Rows[0]["Memo"] + "/" + lastField + "/" + updated, "変更後/Memo/1");
            Check("same field refetched", iMemoCopy.Text, "変更後");

            iCost.Focus(); Application.DoEvents();
            Type(iCost, "48000");
            other.Focus(); Application.DoEvents();
            Check("update number", dt.Rows[0]["Cost"], 48000);
            iCost.Focus(); Application.DoEvents();
            Type(iCost, "\b");
            other.Focus(); Application.DoEvents();
            Check("update number blank -> DBNull", dt.Rows[0]["Cost"] == DBNull.Value, true);

            iCode.Focus(); Application.DoEvents();
            Type(iCode, "34x56");
            other.Focus(); Application.DoEvents();
            Check("update number in STextBox", iCode.Text + "/" + dt.Rows[0]["Code"], "3456/3456");

            iBirth.Focus(); Application.DoEvents();
            Type(iBirth, "2000/1/2");
            other.Focus(); Application.DoEvents();
            Check("update date", ((DateTime)dt.Rows[0]["Birth"]).ToString("yyyy-MM-dd"), "2000-01-02");

            iTel2.Focus(); Application.DoEvents();
            Type(iTel2, "99");
            other.Focus(); Application.DoEvents();
            Check("update hyphen", dt.Rows[0]["Tel"], "0466-99-3041");

            iKbn.Focus(); Application.DoEvents();
            iKbn.SelectedValue = 1;
            other.Focus(); Application.DoEvents();
            Check("update combo", dt.Rows[0]["Kbn"], 1);

            iBank.Focus(); Application.DoEvents();
            Type(iBank, "横浜");
            other.Focus(); Application.DoEvents();
            Check("update table combo", iBank.Text + "/" + dt.Rows[0]["BankId"], "横浜銀行/10");

            iFlag.Checked = false;
            Check("update check on change", dt.Rows[0]["Flag"], false);

            iName.Focus(); Application.DoEvents();
            iName.SelectAll();
            Type(iName, "鈴木");
            iName.ImeReading = "ｽｽﾞｷ";
            other.Focus(); Application.DoEvents();
            Check("update name", dt.Rows[0]["Name"], "鈴木");
            Check("ruby appended from IME reading", iKana.Text + "/" + dt.Rows[0]["Kana"], "ﾌｼﾞｻﾜ ﾀﾛｳｽｽﾞｷ/ﾌｼﾞｻﾜ ﾀﾛｳｽｽﾞｷ");

            // 名称を入力するたびに、その場でふりがなへ反映する（欄を離れるのを待たない）。
            // IME で確定すると、先に読みがたまり、その後で文字が入る
            iName.Focus(); Application.DoEvents();
            iName.Select(iName.TextLength, 0);
            iName.ImeReading = "ｲﾁﾛｳ";
            Type(iName, "一郎");
            Check("ruby follows while typing", iName.Text + "/" + iKana.Text, "鈴木一郎/ﾌｼﾞｻﾜ ﾀﾛｳｽｽﾞｷｲﾁﾛｳ");
            Type(iName, "\b");
            Check("ruby stays when a part of the name is deleted", iName.Text + "/" + iKana.Text, "鈴木一/ﾌｼﾞｻﾜ ﾀﾛｳｽｽﾞｷｲﾁﾛｳ");
            iName.SelectAll();
            Type(iName, "\b");
            Check("ruby cleared as soon as the name is emptied", "[" + iName.Text + "]/[" + iKana.Text + "]", "[]/[]");
            iName.ImeReading = "ｻﾄｳ";
            Type(iName, "佐藤");
            Check("ruby restarts after the name is emptied", iKana.Text, "ｻﾄｳ");
            other.Focus(); Application.DoEvents();
            Check("row gets the name and the ruby", dt.Rows[0]["Name"] + "/" + dt.Rows[0]["Kana"], "佐藤/ｻﾄｳ");
            // 空欄を許さない数値の欄は、値が無いと 0 になる
            var strict = new SNumberBox { AllowBlank = false };
            new SControlDBNumber("Cost", strict).Fetch(new SDataRow(dt.Rows[1]));
            Check("number without blank shows 0 for null", strict.Text, "0");
            iName.Focus(); Application.DoEvents();
            Type(iName, "\b");
            other.Focus(); Application.DoEvents();
            Check("empty name clears ruby", "[" + iKana.Text + "]/" + (dt.Rows[0]["Name"] == DBNull.Value) + "/" + (dt.Rows[0]["Kana"] == DBNull.Value), "[]/True/True");

            // 値が変わらなければ、行を書き換えない
            dt.AcceptChanges();
            updated = 0;
            iMemo.Focus(); Application.DoEvents();
            other.Focus(); Application.DoEvents();
            iBirth.Focus(); Application.DoEvents();
            other.Focus(); Application.DoEvents();
            Check("no change, no update", updated + "/" + (dt.GetChanges() == null), "0/True");

            // 値なしの行
            rowIndex = 1;
            db.FetchAll();
            Check("fetch nulls", "[" + iName.Text + iTel1.Text + iPost1.Text + iAddr.Text + iCost.Text + iCode.Text + iBirth.Text + iKbn.Text + iBank.Text + iMemo.Text + "]/" + iFlag.Checked + "/" + iKbn.SelectedIndex, "[]/False/-1");

            // 行なし
            rowIndex = -1;
            db.FetchAll();
            Check("fetch without row", "[" + iName.Text + iMemo.Text + "]", "[]");
            iMemo.Focus(); Application.DoEvents();
            Type(iMemo, "x");
            other.Focus(); Application.DoEvents();
            Check("update without row is ignored", iMemo.Text, "x");

            // クリア
            rowIndex = 0;
            db.FetchAll();
            dt.AcceptChanges();
            db.ClearAll();
            Check("ClearAll", "[" + iName.Text + iTel1.Text + iCost.Text + iBirth.Text + iMemo.Text + "]/" + iFlag.Checked + "/" + iKbn.SelectedIndex + "/" + iBank.SelectedIndex + "/" + (dt.GetChanges() == null), "[]/False/0/0/True");

            // 読み取り専用
            db.FetchAll();
            Color memoBack = iMemo.BackColor;
            db.ReadOnlyAll(true);
            Check("ReadOnlyAll(true)", iMemo.ReadOnly + "/" + iKbn.ReadOnly + "/" + iBank.ReadOnly + "/" + iCost.ReadOnly + "/" + iFlag.AutoCheck + "/" + iMemo.TabStop + "/" + (iMemo.BackColor == SystemColors.Control), "True/True/True/True/False/False/True");
            db.ReadOnlyAll(false);
            Check("ReadOnlyAll(false)", iMemo.ReadOnly + "/" + iKbn.ReadOnly + "/" + iFlag.AutoCheck + "/" + iMemo.TabStop + "/" + (iMemo.BackColor == memoBack), "False/False/True/True/True");

            // 項目・コントロールでの読み込み、対応の検索
            dt.Rows[0]["Memo"] = "直接";
            Check("FetchByField", db.FetchByField("Memo") + "/" + iMemo.Text + "/" + iMemoCopy.Text, "True/直接/直接");
            dt.Rows[0]["Tel"] = "03-1234-5678";
            Check("FetchByControl", db.FetchByControl(iTel3) + "/" + iTel1.Text + "-" + iTel2.Text + "-" + iTel3.Text, "True/03-1234-5678");
            Check("FetchByField unknown", db.FetchByField("Unknown"), false);
            Check("GetControl/GetField", (db.GetControl("Kana") == iKana) + "/" + db.GetField(iPost2) + "/" + db.GetField(iAddr) + "/" + (db.GetItem(iBank) is SControlDBCombo), "True/Post/Addr/True");

            // フック
            SControlDBItem memoItem = db.GetItem("Memo");
            memoItem.FetchRunEx = delegate(ISRow r, object data) { return "[" + data + "]"; };
            memoItem.UpdateRunEx = delegate(ISRow r, object data) { r["Addr"] = "フックで設定"; return Convert.ToString(data).Trim('[', ']'); };
            db.FetchByField("Memo");
            Check("FetchRunEx", iMemo.Text, "[直接]");
            iMemo.Focus(); Application.DoEvents();
            iMemo.SelectAll();
            Type(iMemo, "[更新]");
            other.Focus(); Application.DoEvents();
            Check("UpdateRunEx", dt.Rows[0]["Memo"] + "/" + dt.Rows[0]["Addr"], "更新/フックで設定");

            // Update(): フォーカスを動かさずに確定
            memoItem.FetchRunEx = null; memoItem.UpdateRunEx = null;
            iMemo.Focus(); Application.DoEvents();
            iMemo.SelectAll();
            Type(iMemo, "確定前");
            db.Update();
            Check("Update() active control", dt.Rows[0]["Memo"], "確定前");

            // ISRow を返す関数を渡す
            var db2 = new SControlDB(form, delegate { return (ISRow)new SDataRow(dt.Rows[0]); });
            var extra = new STextBox();
            form.Controls.Add(extra);
            db2.Add(new SControlDBText("Name", extra));
            db2.EndAdd();
            dt.Rows[0]["Name"] = "ISRow";
            db2.FetchAll();
            Check("ISRow delegate", extra.Text, "ISRow");
            form.Close();
        }
    }

    /// <summary>指定した時間（ミリ秒）、メッセージを処理しながら待つ（タイマーが働くように）。</summary>
    private static void Wait(int milliseconds)
    {
        DateTime until = DateTime.Now.AddMilliseconds(milliseconds);
        while (DateTime.Now < until)
        {
            Application.DoEvents();
            System.Threading.Thread.Sleep(10);
        }
    }

    // ---- 郵便番号を入力している最中に、住所を探す（SControlDBPostAddr）----
    // 郵便番号の DB の代わりに、番号と住所の対応を返す関数を差し込んで、探しに行く時機と回数を確かめる
    private static void PostTests()
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ShowInTaskbar = false;
            var dt = new DataTable();
            dt.Columns.Add("Post", typeof(string));
            dt.Columns.Add("Addr", typeof(string));
            dt.Columns.Add("AddrKana", typeof(string));
            dt.Rows.Add("", "", "");
            dt.AcceptChanges();

            var iPost1 = new STextBox(); var iPost2 = new STextBox(); var iAddr = new STextBox(); var iKana = new STextBox();
            var other = new STextBox();
            int y = 5;
            foreach (Control c in new Control[] { iPost1, iPost2, iAddr, iKana, other }) { c.Location = new Point(5, y); y += 30; form.Controls.Add(c); }

            // 待つ時間は、テストが長くならないよう、短くしておく（既定は 500）
            const int Delay = 150;
            int calls = 0; string lastPost = null;
            var item = new SControlDBPostAddr(new[] { "Post", "Addr", "AddrKana" }, new[] { iPost1, iPost2, iAddr, iKana });
            Check("post: default delay", item.AutoLookupDelay, 500);
            item.AutoLookupDelay = Delay;
            item.AddressLookup = delegate(string post)
            {
                calls++;
                lastPost = post;
                return post == "251-0052" ? new[] { "神奈川県藤沢市藤沢", "ｶﾅｶﾞﾜｹﾝﾌｼﾞｻﾜｼﾌｼﾞｻﾜ" } : new[] { "", "" };
            };

            var db = new SControlDB(form, delegate { return dt.Rows[0]; });
            db.Add(item);
            var rule = new SControlDBRule();
            rule.Add(SControlRule.PostAddr(30, 50), "Post");
            db.EndAdd(rule);
            db.FetchAll();

            form.Show();
            form.Activate();
            Application.DoEvents();

            // 7 桁そろうまでは、探さない
            iPost1.Focus();
            Type(iPost1, "251");
            Wait(Delay * 2);
            Check("post: 3 digits", calls + "/" + iAddr.Text, "0/");
            iPost2.Focus();
            calls = 0;      // 前半の欄を離れたときに、これまでどおり 1 回探している（番号が途中なので、見つからない）
            Type(iPost2, "005");
            Wait(Delay * 2);
            Check("post: 6 digits", calls + "/" + iAddr.Text, "0/");

            // 7 桁そろっても、入力が止まるまでは探さない。止まってから、1 回だけ探して、住所とふりがなを入れる
            Type(iPost2, "2");
            Check("post: 7 digits, before the delay", calls + "/" + iAddr.Text, "0/");
            Wait(Delay * 3);
            Check("post: 7 digits, after the delay", calls + "/" + lastPost + "/" + iAddr.Text + "/" + iKana.Text + "/" + form.ActiveControl.TabIndex + "/" + iPost2.Focused,
                "1/251-0052/神奈川県藤沢市藤沢/ｶﾅｶﾞﾜｹﾝﾌｼﾞｻﾜｼﾌｼﾞｻﾜ/" + iPost2.TabIndex + "/True");
            // 行へ書くのは、これまでどおり、欄を離れたとき（ここでは、前半の欄を離れたときの「251-」までが書かれている）。
            // 住所が入っているので、離れたときに、もう一度は探さない
            Check("post: row before leaving", dt.Rows[0]["Post"] + "/" + dt.Rows[0]["Addr"], "251-/");
            other.Focus();
            Application.DoEvents();
            Check("post: row after leaving", dt.Rows[0]["Post"] + "/" + dt.Rows[0]["Addr"] + "/" + dt.Rows[0]["AddrKana"] + "/" + calls, "251-0052/神奈川県藤沢市藤沢/ｶﾅｶﾞﾜｹﾝﾌｼﾞｻﾜｼﾌｼﾞｻﾜ/1");

            // 住所が入っているときは、番号を打ち直しても、探さない（住所を書き換えない）
            iAddr.Text = "手で入れた住所";
            iPost2.Focus();
            iPost2.SelectAll();
            calls = 0;
            Type(iPost2, "0052");
            Wait(Delay * 3);
            Check("post: address is not empty", calls + "/" + iAddr.Text, "0/手で入れた住所");

            // 待っている最中に打ち直すと、待ち直す（最後の入力から数える）
            iAddr.Text = "";
            iKana.Text = "";
            iPost2.SelectAll();
            Type(iPost2, "0052");
            Wait(Delay * 2 / 3);
            Type(iPost2, "\b");
            Type(iPost2, "2");
            Wait(Delay * 2 / 3);
            Check("post: retyped during the delay", calls + "/" + iAddr.Text, "0/");
            Wait(Delay * 2);
            Check("post: after the restarted delay", calls + "/" + iAddr.Text, "1/神奈川県藤沢市藤沢");

            // 見つからない番号: 住所は空のまま
            iAddr.Text = "";
            iKana.Text = "";
            iPost2.SelectAll();
            calls = 0;
            Type(iPost2, "9999");
            Wait(Delay * 3);
            Check("post: not found", calls + "/" + lastPost + "/[" + iAddr.Text + "]", "1/251-9999/[]");

            // 待っている最中に欄を離れたら、離れたときに探す（待っていた分は、探さない）
            iPost2.SelectAll();
            calls = 0;
            Type(iPost2, "0052");
            other.Focus();
            Application.DoEvents();
            Check("post: left during the delay", calls + "/" + iAddr.Text, "1/神奈川県藤沢市藤沢");
            Wait(Delay * 3);
            Check("post: no second lookup", calls, 1);

            // プログラムから読み込んだとき（Fetch）は、探さない
            dt.Rows[0]["Post"] = "251-0052";
            dt.Rows[0]["Addr"] = DBNull.Value;
            iPost2.Focus();
            calls = 0;
            db.FetchAll();
            Wait(Delay * 3);
            Check("post: fetch does not look up", calls + "/[" + iAddr.Text + "]/" + iPost2.Text, "0/[]/0052");

            // 待つ時間を 0 にすると、入力の最中には探さない（欄を離れたときだけ）
            item.AutoLookupDelay = 0;
            iPost2.SelectAll();
            Type(iPost2, "0052");
            Wait(Delay * 3);
            Check("post: turned off", calls + "/[" + iAddr.Text + "]", "0/[]");
            other.Focus();
            Application.DoEvents();
            Check("post: turned off, on leaving", calls + "/" + iAddr.Text, "1/神奈川県藤沢市藤沢");

            form.Close();
        }
    }

    // ---- レビューで見つかった不具合の再発防止 ----
    private static void FixTests()
    {
        // 既存のルールの読み替え: AlternateBlank が false でも、空欄を許す（値が無い項目を 0 にしない）
        var g = new GControlDBRule();
        g.Add(new GControlDBRuleCurrency("GMonthly", 7, 0));
        var converted = new SNumberBox();
        SControlDBRuleConverter.Wrap(g).SetControlByRule(converted, "GMonthly");
        Check("converted Currency allows blank", converted.AllowBlank + "/" + converted.MaxValue + "/" + converted.ThousandsSeparator, "True/9999999/True");
        var direct = new SNumberBox();
        SControlRule.Currency(7, 0).Apply(direct);
        Check("Currency rule allows blank", direct.AllowBlank, true);

        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ShowInTaskbar = false;
            var dt = new DataTable();
            dt.Columns.Add("Birth", typeof(DateTime));
            dt.Columns.Add("BankId", typeof(int));
            dt.Columns.Add("Cost", typeof(int));
            dt.Rows.Add(new DateTime(1965, 3, 3), 10, DBNull.Value);
            dt.AcceptChanges();

            var banks = new DataTable();
            banks.Columns.Add("Id", typeof(int));
            banks.Columns.Add("Name", typeof(string));
            banks.Rows.Add(10, "横浜銀行");
            banks.Rows.Add(20, "湘南信用金庫");

            var iBirth = new SDateBox { CalendarKind = SDateCalendarKind.Western };
            var iBank = new STableComboBox { DataSource = banks, DisplayMember = "Name", ValueMember = "Id" };
            var iCost = new SNumberBox();
            var other = new STextBox();
            int y = 5;
            foreach (Control c in new Control[] { other, iBirth, iBank, iCost }) { c.Location = new Point(5, y); y += 30; form.Controls.Add(c); }

            var db = new SControlDB(form, delegate { return dt.Rows[0]; });
            db.Add(new SControlDBDate("Birth", iBirth));
            db.Add(new SControlDBCombo("BankId", iBank));
            db.Add(new SControlDBNumber("Cost", iCost));
            var rule = new SControlDBRule();
            rule.Add(SControlRule.Currency(7, 0), "Cost");
            db.EndAdd(rule);
            db.FetchAll();

            form.Show();
            form.Activate();
            other.Focus();
            Application.DoEvents();

            // 値が無い数値の項目は空欄のまま。フォーカスが通っても、行を書き換えない
            Check("null number stays blank", "[" + iCost.Text + "]", "[]");
            iCost.Focus();
            Application.DoEvents();
            other.Focus();
            Application.DoEvents();
            Check("null number: row is untouched", dt.Rows[0].RowState + "/" + (dt.Rows[0]["Cost"] == DBNull.Value), "Unchanged/True");

            // 日付: 入力の途中のまま、フォーカスを動かさずに書き戻す（ファンクション キーでの保存）
            iBirth.Focus();
            Application.DoEvents();
            Type(iBirth, "19700102");
            db.Update();
            Check("Update() commits the pending date", ((DateTime)dt.Rows[0]["Birth"]).ToString("yyyy-MM-dd") + "/" + iBirth.Focused, "1970-01-02/True");

            // テーブル コンボ: 入力の途中のまま書き戻す（未選択＝値なし が書かれない）
            iBank.Focus();
            Application.DoEvents();
            Type(iBank, "湘南");
            Check("pending table combo has no selection", iBank.SelectedValue == null, true);
            db.Update();
            Check("Update() commits the pending table combo", dt.Rows[0]["BankId"] + "/" + iBank.Text, "20/湘南信用金庫");

            // UpdateByControl も同じ
            iBirth.Focus();
            Application.DoEvents();
            Type(iBirth, "19800305");
            db.UpdateByControl(iBirth);
            Check("UpdateByControl commits the pending date", ((DateTime)dt.Rows[0]["Birth"]).ToString("yyyy-MM-dd"), "1980-03-05");

            // ---- 読み取り専用にして戻したとき、背景色は元の色に戻る ----
            // 登録を終えた時点でフォーカスがあった入力欄でも、フォーカス時の背景色を「元の色」として覚えない
            dt.Columns.Add("Memo", typeof(string));
            var memo = new STextBox { Location = new Point(5, 200) };
            form.Controls.Add(memo);
            var db3 = new SControlDB(form, delegate { return dt.Rows[0]; });
            db3.Add(new SControlDBText("Memo", memo));
            memo.Focus();
            Application.DoEvents();
            Check("focused when EndAdd is called", memo.BackColor == memo.FocusBackColor, true);
            db3.EndAdd();
            db3.ReadOnly(memo, true);
            Check("ReadOnly on", memo.ReadOnly + "/" + (memo.BackColor.ToArgb() == SystemColors.Control.ToArgb()), "True/True");
            db3.ReadOnly(memo, false);
            other.Focus();
            Application.DoEvents();
            Check("ReadOnly restores the normal back color", memo.ReadOnly + "/" + (memo.BackColor.ToArgb() == SystemColors.Window.ToArgb()), "False/True");
            form.Close();
        }
    }
}
