// SDateBox の検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using App.Controls;

internal static class DateHarness
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
    private static readonly DateTime Today = new DateTime(2026, 11, 25);

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
                Shot(args[1], args[2], args[3]);
                return 0;
            }
            TextTests();
            ControlTests();
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

    private static string P(string text, SDateFormat format, bool japanese)
    {
        DateTime d;
        return SDateText.TryParse(text, format, japanese, Today, out d) ? d.ToString("yyyy-MM-dd") : "x";
    }

    private static void TextTests()
    {
        var d = new DateTime(2026, 11, 5);
        Check("display wareki", SDateText.FormatForDisplay(d, SDateFormat.YearMonthDay, true), "令和 08年11月05日");
        Check("display wareki slash", SDateText.FormatForDisplay(d, SDateFormat.YearMonthDaySlash, true), "令和 08/11/05");
        Check("display wareki ym", SDateText.FormatForDisplay(d, SDateFormat.YearMonth, true), "令和 08年11月");
        Check("display wareki y", SDateText.FormatForDisplay(d, SDateFormat.Year, true), "令和 08年");
        Check("display seireki", SDateText.FormatForDisplay(d, SDateFormat.YearMonthDay, false), "2026年11月05日");
        Check("display seireki slash", SDateText.FormatForDisplay(d, SDateFormat.YearMonthDaySlash, false), "2026/11/05");
        Check("display seireki ym", SDateText.FormatForDisplay(d, SDateFormat.YearMonth, false), "2026年11月");
        Check("edit wareki", SDateText.FormatForEdit(d, SDateFormat.YearMonthDay, true), "R08/11/05");
        Check("edit wareki ym", SDateText.FormatForEdit(d, SDateFormat.YearMonth, true), "R08/11");
        Check("edit wareki y", SDateText.FormatForEdit(d, SDateFormat.Year, true), "R08");
        Check("edit seireki", SDateText.FormatForEdit(d, SDateFormat.YearMonthDay, false), "2026/11/05");
        Check("era boundary heisei end", SDateText.FormatForDisplay(new DateTime(2019, 4, 30), SDateFormat.YearMonthDay, true), "平成 31年04月30日");
        Check("era boundary reiwa start", SDateText.FormatForDisplay(new DateTime(2019, 5, 1), SDateFormat.YearMonthDay, true), "令和 01年05月01日");
        Check("showa", SDateText.FormatForDisplay(new DateTime(1965, 3, 3), SDateFormat.YearMonthDay, true), "昭和 40年03月03日");
        Check("before meiji falls back", SDateText.FormatForDisplay(new DateTime(1868, 1, 1), SDateFormat.YearMonthDay, true), "1868年01月01日");

        SDateFormat ymd = SDateFormat.YearMonthDay;
        Check("parse 2026/11/25", P("2026/11/25", ymd, true), "2026-11-25");
        Check("parse 2026-1-5", P("2026-1-5", ymd, false), "2026-01-05");
        Check("parse 2026.11.25", P("2026.11.25", ymd, false), "2026-11-25");
        Check("parse 20261125", P("20261125", ymd, true), "2026-11-25");
        Check("parse kanji seireki", P("2026年11月25日", ymd, true), "2026-11-25");
        Check("parse R8/11/25", P("R8/11/25", ymd, false), "2026-11-25");
        Check("parse r08.11.25", P("r08.11.25", ymd, false), "2026-11-25");
        Check("parse R.08/11/25", P("R. 08/11/25", ymd, true), "2026-11-25");
        Check("parse R081125", P("R081125", ymd, true), "2026-11-25");
        Check("parse R81125", P("R81125", ymd, true), "2026-11-25");
        Check("parse 令和8年11月25日", P("令和8年11月25日", ymd, false), "2026-11-25");
        Check("parse 令和元年5月1日", P("令和元年5月1日", ymd, false), "2019-05-01");
        Check("parse 令8.11.25", P("令8.11.25", ymd, false), "2026-11-25");
        Check("parse S40/3/3", P("S40/3/3", ymd, true), "1965-03-03");
        Check("parse H31/4/30", P("H31/4/30", ymd, true), "2019-04-30");
        Check("parse M1/9/8", P("M1/9/8", ymd, true), "1868-09-08");
        Check("parse T15/12/24", P("T15/12/24", ymd, true), "1926-12-24");
        Check("parse full-width", P("Ｒ８／１１／２５", ymd, true), "2026-11-25");
        Check("parse 8/11/25 wareki", P("8/11/25", ymd, true), "2026-11-25");
        Check("parse 05/11/25 seireki", P("05/11/25", ymd, false), "2005-11-25");
        Check("parse 081125 wareki", P("081125", ymd, true), "2026-11-25");
        Check("parse 11/25 (this year)", P("11/25", ymd, true), "2026-11-25");
        Check("parse 1125", P("1125", ymd, false), "2026-11-25");
        Check("parse 305", P("305", ymd, false), "2026-03-05");
        Check("parse 7 (this month)", P("7", ymd, true), "2026-11-07");
        Check("parse leap day", P("2024/2/29", ymd, false), "2024-02-29");
        Check("reject 2026/2/29", P("2026/2/29", ymd, false), "x");
        Check("reject 2026/13/1", P("2026/13/1", ymd, false), "x");
        Check("reject garbage", P("R/", ymd, true) + P("abc", ymd, true) + P("1/2/3/4", ymd, true) + P("", ymd, true), "xxxx");
        Check("reject era without full date", P("R8/11", ymd, true), "x");

        SDateFormat ym = SDateFormat.YearMonth;
        Check("ym 2026/11", P("2026/11", ym, false), "2026-11-01");
        Check("ym 202611", P("202611", ym, true), "2026-11-01");
        Check("ym R8/11", P("R8/11", ym, false), "2026-11-01");
        Check("ym R0811", P("R0811", ym, true), "2026-11-01");
        Check("ym 0811 wareki", P("0811", ym, true), "2026-11-01");
        Check("ym 0511 seireki", P("0511", ym, false), "2005-11-01");
        Check("ym 3 (this year)", P("3", ym, true), "2026-03-01");
        Check("ym with day ignored", P("2026/11/25", ym, false), "2026-11-01");
        Check("ym 令和8年11月", P("令和8年11月", ym, true), "2026-11-01");

        SDateFormat y = SDateFormat.Year;
        Check("y 2026", P("2026", y, true), "2026-01-01");
        Check("y R8", P("R8", y, false), "2026-01-01");
        Check("y 8 wareki", P("8", y, true), "2026-01-01");
        Check("y 平成30年", P("平成30年", y, true), "2018-01-01");
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

    /// <summary>
    /// 日付の欄を画像に描かせて、入力部分に描かれた文字の、上下の高さ（画素）を返す。何も描かれていなければ 0。
    /// フォントを縮めて描いたかどうかを、文字の高さで確かめるのに使う。
    /// </summary>
    private static int InkHeight(SDateBox box)
    {
        Application.DoEvents();
        using (var bitmap = new Bitmap(box.Width, box.Height))
        {
            box.DrawToBitmap(bitmap, new Rectangle(0, 0, box.Width, box.Height));
            int right = box.Width - box.Height - 6;   // カレンダーのボタンの領域は、幅がおよそ高さと同じ
            Color back = bitmap.GetPixel(4, box.Height / 2);
            int top = -1, bottom = -1;
            for (int y = 3; y < box.Height - 3; y++)
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

    private static string V(SDateBox box)
    {
        return box.Value.HasValue ? box.Value.Value.ToString("yyyy-MM-dd") : "null";
    }

    private static void ControlTests()
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ShowInTaskbar = false;
            var other = new STextBox { Location = new Point(10, 10) };
            var date = new SDateBox { Location = new Point(10, 50), Width = 200 };
            form.Controls.Add(other);
            form.Controls.Add(date);
            form.Show();
            form.Activate();
            other.Focus();
            Application.DoEvents();

            int changed = 0, invalid = 0;
            date.ValueChanged += delegate { changed++; };
            date.InvalidInput += delegate { invalid++; };

            Check("initial", V(date) + "/" + date.Text, "null/");
            Check("defaults", date.TextAlign + "/" + date.ImeMode + "/" + date.HighlightText + "/" + date.DateFormat + "/" + date.CalendarKind,
                "Center/Disable/True/YearMonthDay/Default");

            date.Value = new DateTime(2026, 11, 25);
            Check("display (wareki default)", date.Text + "/" + changed, "令和 08年11月25日/1");
            date.CalendarKind = SDateCalendarKind.Western;
            Check("display seireki", date.Text, "2026年11月25日");
            date.CalendarKind = SDateCalendarKind.Default;
            SDateBox.DefaultCalendarKind = SDateCalendarKind.Western;
            date.RefreshDisplay();
            Check("app-wide seireki", date.Text, "2026年11月25日");
            SDateBox.DefaultCalendarKind = SDateCalendarKind.Japanese;
            date.RefreshDisplay();
            Check("app-wide wareki", date.Text, "令和 08年11月25日");

            // フォーカスで編集用の表示
            date.Focus(); Application.DoEvents();
            Check("edit format", date.Text + "/" + date.SelectionLength, "R08/11/25/9");
            other.Focus(); Application.DoEvents();
            Check("leave without edit keeps value", V(date) + "/" + changed, "2026-11-25/1");

            // 入力して確定
            date.Focus(); Application.DoEvents();
            Type(date, "S40/3/3");
            Check("typing does not change value", V(date), "2026-11-25");
            other.Focus(); Application.DoEvents();
            Check("leave commits", V(date) + "/" + date.Text + "/" + changed, "1965-03-03/昭和 40年03月03日/2");

            date.Focus(); Application.DoEvents();
            Type(date, "20240229");
            Key(date, Keys.Enter);
            Check("Enter commits, edit format", V(date) + "/" + date.Text, "2024-02-29/R06/02/29");
            Type(date, "ｘ　abc 9/9/99/99");
            Check("only date characters typed", date.Text, "9/9/99/99");
            invalid = 0;
            Key(date, Keys.Enter);
            Check("invalid reverts", V(date) + "/" + date.Text + "/" + invalid, "2024-02-29/R06/02/29/1");
            date.SelectAll();
            Type(date, "5/5");
            Key(date, Keys.Escape);
            Check("Esc cancels edit", V(date) + "/" + date.Text, "2024-02-29/R06/02/29");
            date.SelectAll();
            Type(date, "\b");
            other.Focus(); Application.DoEvents();
            Check("empty becomes null", V(date) + "/" + date.Text, "null/");

            // 範囲
            date.Focus(); Application.DoEvents();
            Type(date, "2200/1/1");
            other.Focus(); Application.DoEvents();
            Check("clamped to MaxDate", V(date), "2100-12-31");
            date.Value = new DateTime(1800, 1, 1);
            Check("clamped to MinDate", V(date), "1868-01-01");

            // 値は、設定されたものをそのまま持つ（年月の形でも日を保つ）
            var ym = new SDateBox { Location = new Point(10, 90), DateFormat = SDateFormat.YearMonth, CalendarKind = SDateCalendarKind.Western };
            form.Controls.Add(ym);
            ym.Value = new DateTime(2026, 11, 25, 13, 30, 0);
            Check("ym display", ym.Text, "2026年11月");
            ym.Focus(); Application.DoEvents();
            Check("ym edit format", ym.Text, "2026/11");
            other.Focus(); Application.DoEvents();
            Check("ym value untouched", ym.Value.Value.ToString("yyyy-MM-dd HH:mm"), "2026-11-25 13:30");
            ym.Focus(); Application.DoEvents();
            Type(ym, "202703");
            other.Focus(); Application.DoEvents();
            Check("ym typed", V(ym) + "/" + ym.Text, "2027-03-01/2027年03月");

            // Text からの設定
            ym.Text = "2025/4";
            Check("Text setter", V(ym), "2025-04-01");
            ym.Text = "zzz";
            Check("Text setter invalid", V(ym), "null");

            // カレンダー（キー操作）
            date.Value = new DateTime(2026, 11, 25);
            date.Focus(); Application.DoEvents();
            int opened = 0, closed = 0;
            date.DropDown += delegate { opened++; };
            date.DropDownClosed += delegate { closed++; };
            Key(date, Keys.Down);
            Check("Down opens calendar", date.DroppedDown, true);
            Key(date, Keys.Right);
            Key(date, Keys.Down);
            Key(date, Keys.PageDown);
            Key(date, Keys.Enter);
            Check("arrows + PageDown + Enter", V(date) + "/" + date.DroppedDown, "2027-01-03/False");
            Key(date, Keys.Space);
            Check("Space opens", date.DroppedDown, true);
            Key(date, Keys.Escape);
            Check("Esc closes without change", V(date) + "/" + date.DroppedDown, "2027-01-03/False");
            Key(date, Keys.Space);
            Type(date, "1");
            Check("typing closes calendar", date.DroppedDown + "/" + date.Text, "False/1");
            Key(date, Keys.Escape);
            int x = date.ClientSize.Width + 12;
            SendMessage(date.Handle, WM_LBUTTONDOWN, (IntPtr)1, (IntPtr)((5 << 16) | x));
            Application.DoEvents();
            Check("button click opens", date.DroppedDown, true);
            SendMessage(date.Handle, WM_LBUTTONDOWN, (IntPtr)1, (IntPtr)((5 << 16) | x));
            Application.DoEvents();
            Check("button click closes", date.DroppedDown, false);
            Check("DropDown events", opened + "/" + closed, "4/4");

            // 月のカレンダー
            ym.Value = new DateTime(2026, 11, 1);
            ym.Focus(); Application.DoEvents();
            Key(ym, Keys.Down);
            Key(ym, Keys.Left);
            Key(ym, Keys.PageUp);
            Key(ym, Keys.Enter);
            Check("month calendar", V(ym), "2025-10-01");

            // 年のカレンダー（年だけの形）。左右で 1 年、上下で 1 行分（4 年）、PageUp / PageDown で 1 画面分（12 年）
            var year = new SDateBox { Location = new Point(10, 120), DateFormat = SDateFormat.Year, CalendarKind = SDateCalendarKind.Western };
            form.Controls.Add(year);
            year.Value = new DateTime(2026, 11, 26);
            Check("year display", year.Text, "2026年");
            year.Focus(); Application.DoEvents();
            Key(year, Keys.Down);
            Check("year calendar opens", year.DroppedDown, true);
            Key(year, Keys.Right);
            Key(year, Keys.Down);
            Key(year, Keys.PageDown);
            Key(year, Keys.Enter);
            Check("year calendar: Right + Down + PageDown + Enter", V(year) + "/" + year.DroppedDown, "2043-01-01/False");
            Key(year, Keys.Down);
            Key(year, Keys.Left);
            Key(year, Keys.Up);
            Key(year, Keys.PageUp);
            Key(year, Keys.Enter);
            Check("year calendar: Left + Up + PageUp + Enter", V(year), "2026-01-01");
            Key(year, Keys.Down);
            Key(year, Keys.Right);
            Key(year, Keys.Escape);
            Check("year calendar: Esc keeps the value", V(year) + "/" + year.DroppedDown, "2026-01-01/False");
            year.MaxDate = new DateTime(2027, 6, 30);
            Key(year, Keys.Down);
            Key(year, Keys.PageDown);
            Key(year, Keys.Enter);
            Check("year calendar: stops at MaxDate", V(year), "2027-01-01");
            other.Focus(); Application.DoEvents();
            Check("year display after choosing", year.Text, "2027年");

            // カレンダーなし・読み取り専用
            ym.ShowCalendarButton = false;
            Key(ym, Keys.Down);
            Check("no calendar when button hidden", ym.DroppedDown, false);
            other.Focus(); Application.DoEvents();
            date.ReadOnly = true;
            date.Focus(); Application.DoEvents();
            Check("ReadOnly keeps display format", date.Text, "令和 09年01月03日");
            Key(date, Keys.Down);
            Check("ReadOnly: no calendar", date.DroppedDown, false);
            other.Focus(); Application.DoEvents();

            var fresh = new SDateBox();
            var props = System.ComponentModel.TypeDescriptor.GetProperties(fresh);
            foreach (string p in new[] { "Value", "MinDate", "MaxDate", "DateFormat", "CalendarKind", "ShowCalendarButton", "HighlightText", "ImeMode", "Font" })
            {
                Check("ShouldSerialize " + p, props[p].ShouldSerializeValue(fresh), false);
            }
            Check("hidden Text", props["Text"].SerializationVisibility, System.ComponentModel.DesignerSerializationVisibility.Hidden);
            // 文字の種類の指定（Format・AllowSpace）は STextBox だけが持つ
            Check("no Format / AllowSpace", (props["Format"] == null) + "/" + (props["AllowSpace"] == null), "True/True");

            // ---- 収まらない表示は、フォーカスがない間、縮めて表示する ----
            Check("shrink: default", fresh.ShrinkTextToFit + "/" + props["ShrinkTextToFit"].ShouldSerializeValue(fresh), "True/False");
            other.Focus(); Application.DoEvents();
            var narrowDate = new SDateBox { Location = new Point(10, 180), Width = 130, CalendarKind = SDateCalendarKind.Japanese };
            form.Controls.Add(narrowDate);
            narrowDate.Value = new DateTime(2026, 12, 15);    // 「令和 08年12月15日」は、幅 130 には収まらない
            int dateShrunk = InkHeight(narrowDate);
            narrowDate.ShrinkTextToFit = false;
            int dateNormal = InkHeight(narrowDate);
            Check("shrink: long date is drawn smaller", dateShrunk > 0 && dateShrunk < dateNormal, true);
            narrowDate.ShrinkTextToFit = true;
            narrowDate.Width = 240;
            Check("shrink: wide enough -> normal size", InkHeight(narrowDate), dateNormal);
            narrowDate.Width = 130;
            Check("shrink: narrow again", InkHeight(narrowDate), dateShrunk);
            Check("shrink: the value is untouched", V(narrowDate) + "/" + narrowDate.Text, "2026-12-15/令和 08年12月15日");
            narrowDate.Focus(); Application.DoEvents();
            narrowDate.SelectionLength = 0;
            Check("shrink: edit format while focused", narrowDate.Text, "R08/12/15");
            other.Focus(); Application.DoEvents();
            Check("shrink: smaller again after leaving", InkHeight(narrowDate), dateShrunk);
            form.Close();
        }
    }

    private static void Shot(string closedPng, string dayPng, string monthPng)
    {
        using (var form = new Form())
        {
            form.Text = "SDateBox 検証";
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(40, 40);
            form.ClientSize = new Size(470, 520);
            form.BackColor = Color.FromArgb(244, 252, 254);
            form.TopMost = true;

            int y = 14;
            Func<string, Control, Control> add = delegate(string label, Control box)
            {
                var l = new Label { Text = label, Location = new Point(12, y + 5), AutoSize = true, Font = new STextBox().Font };
                box.Location = new Point(190, y);
                box.Width = 200;
                form.Controls.Add(l);
                form.Controls.Add(box);
                y += box.Height + 8;
                return box;
            };

            var focus = new SDateBox { Value = new DateTime(2026, 11, 25) };
            add("フォーカスあり(編集中)", focus);
            add("和暦 年月日", new SDateBox { Value = new DateTime(2026, 11, 25) });
            add("西暦 年月日", new SDateBox { Value = new DateTime(2026, 11, 25), CalendarKind = SDateCalendarKind.Western });
            add("和暦 スラッシュ", new SDateBox { Value = new DateTime(1965, 3, 3), DateFormat = SDateFormat.YearMonthDaySlash });
            var ymBox = new SDateBox { Value = new DateTime(2026, 11, 1), DateFormat = SDateFormat.YearMonth };
            add("年月", ymBox);
            add("年(ボタンなし)", new SDateBox { Value = new DateTime(2026, 1, 1), DateFormat = SDateFormat.Year, ShowCalendarButton = false });
            add("未入力", new SDateBox());
            add("無効", new SDateBox { Value = new DateTime(2026, 11, 25), Enabled = false });
            var day = new SDateBox { Value = new DateTime(2026, 11, 25) };
            add("カレンダー", day);

            form.Show();
            form.Activate();
            focus.Focus();
            focus.Select(focus.TextLength, 0);
            Application.DoEvents();
            System.Threading.Thread.Sleep(300);
            Application.DoEvents();
            Capture(form, closedPng);

            day.Focus();
            day.DroppedDown = true;
            Application.DoEvents();
            Key(day, Keys.Right);
            System.Threading.Thread.Sleep(350);
            Application.DoEvents();
            Capture(form, dayPng);
            day.DroppedDown = false;

            ymBox.Focus();
            ymBox.DroppedDown = true;
            Application.DoEvents();
            System.Threading.Thread.Sleep(350);
            Application.DoEvents();
            Capture(form, monthPng);
            ymBox.DroppedDown = false;
            form.Close();
        }
    }

    private static void Capture(Form form, string path)
    {
        Rectangle r = form.RectangleToScreen(form.ClientRectangle);
        using (var bmp = new Bitmap(r.Width, r.Height))
        {
            using (Graphics g = Graphics.FromImage(bmp)) g.CopyFromScreen(r.Location, Point.Empty, r.Size);
            bmp.Save(path, ImageFormat.Png);
        }
    }

    // ---- レビューで見つかった不具合の再発防止 ----
    private static void FixTests()
    {
        const SDateFormat YMD = SDateFormat.YearMonthDay;
        const SDateFormat YM = SDateFormat.YearMonth;
        const SDateFormat Y = SDateFormat.Year;

        // 元号の期間に入らない日付は、読めないものとして扱う
        Check("era year out of range", P("S70/1/1", YMD, true) + "/" + P("H1/1/1", YMD, true) + "/" + P("S64/1/8", YMD, true) + "/" + P("H31/5/1", YMD, true) + "/" + P("M1/1/1", YMD, true), "x/x/x/x/x");
        Check("era boundary days", P("S64/1/7", YMD, true) + "/" + P("H1/1/8", YMD, true) + "/" + P("H31/4/30", YMD, true) + "/" + P("R1/5/1", YMD, true), "1989-01-07/1989-01-08/2019-04-30/2019-05-01");
        Check("era omitted (current era)", P("8/11/25", YMD, true) + "/" + P("081125", YMD, true), "2026-11-25/2026-11-25");
        Check("western is not checked", P("1989/1/1", YMD, true) + "/" + P("2019/5/1", YMD, false), "1989-01-01/2019-05-01");

        // 年・年月の形: その年・その月のうち 1 日でも元号の期間に入っていれば受け付ける
        Check("year form", P("R1", Y, true) + "/" + P("H31", Y, true) + "/" + P("H32", Y, true) + "/" + P("S64", Y, true), "2019-01-01/2019-01-01/x/1989-01-01");
        Check("year-month form", P("H1/1", YM, true) + "/" + P("S64/1", YM, true) + "/" + P("R1/4", YM, true) + "/" + P("H31/5", YM, true) + "/" + P("R1/5", YM, true), "1989-01-01/1989-01-01/x/x/2019-05-01");

        // 年・年月の形の表示は、その年・その月の最後の日の元号にする（元年を入力したのに前の元号で表示されない）
        Check("display year by year end", SDateText.FormatForDisplay(new DateTime(2019, 1, 1), Y, true) + "/" + SDateText.FormatForEdit(new DateTime(2019, 1, 1), Y, true) + "/" + SDateText.FormatForDisplay(new DateTime(1989, 1, 1), Y, true), "令和 01年/R01/平成 01年");
        Check("display year-month by month end", SDateText.FormatForDisplay(new DateTime(2019, 4, 1), YM, true) + "/" + SDateText.FormatForDisplay(new DateTime(2019, 5, 1), YM, true) + "/" + SDateText.FormatForDisplay(new DateTime(1989, 1, 1), YM, true) + "/" + SDateText.FormatForEdit(new DateTime(1989, 1, 1), YM, true), "平成 31年04月/令和 01年05月/平成 01年01月/H01/01");
        Check("display full date by the day", SDateText.FormatForDisplay(new DateTime(1989, 1, 7), YMD, true) + "/" + SDateText.FormatForDisplay(new DateTime(1989, 1, 8), YMD, true), "昭和 64年01月07日/平成 01年01月08日");
        Check("calendar heading is the same within a month", SDateText.FormatYearMonth(new DateTime(1989, 1, 7), true) + "/" + SDateText.FormatYearMonth(new DateTime(1989, 1, 8), true) + "/" + SDateText.FormatYear(new DateTime(2019, 4, 1), true) + "/" + SDateText.FormatYear(new DateTime(2019, 5, 1), true), "平成 01年01月/平成 01年01月/令和 01年/令和 01年");

        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ShowInTaskbar = false;
            var other = new TextBox { Location = new Point(10, 10) };
            var date = new SDateBox { Location = new Point(10, 50), Width = 200, CalendarKind = SDateCalendarKind.Western };
            form.Controls.Add(other);
            form.Controls.Add(date);
            form.Show();
            form.Activate();
            Application.DoEvents();

            int changed = 0;
            date.ValueChanged += delegate { changed++; };

            // フォーカスを動かさずに確定する（ファンクション キーでの保存の前に呼ぶ）
            date.Focus();
            Application.DoEvents();
            Type(date, "20261125");
            Check("pending edit is not the value yet", V(date) + "/" + changed, "null/0");
            date.CommitEdit();
            Check("CommitEdit", V(date) + "/" + changed + "/" + date.Focused + "/" + date.Text, "2026-11-25/1/True/2026/11/25");
            date.CommitEdit();
            Check("CommitEdit twice changes nothing", V(date) + "/" + changed, "2026-11-25/1");
            Check("ISEditCommit", ((object)date) is ISEditCommit, true);

            // 年の形: 元年を入力して確定すると、その元号で表示する
            other.Focus();
            Application.DoEvents();
            date.CalendarKind = SDateCalendarKind.Japanese;
            date.DateFormat = SDateFormat.Year;
            date.Value = null;
            date.Focus();
            Application.DoEvents();
            Type(date, "R1");
            other.Focus();
            Application.DoEvents();
            Check("year form: first year of era", V(date) + "/" + date.Text, "2019-01-01/令和 01年");
            form.Close();
        }
    }
}
