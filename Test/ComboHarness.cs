// SComboBox の検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ComponentSUI;

internal static class ComboHarness
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
    private const int WM_LBUTTONUP = 0x0202;
    private const int WM_MOUSEWHEEL = 0x020A;

    private static int _failed;

    [STAThread]
    private static int Main(string[] args)
    {
        SetProcessDPIAware();
        Console.OutputEncoding = new System.Text.UTF8Encoding(false);
        Application.EnableVisualStyles();
        if (args.Length > 0 && args[0] == "shot")
        {
            Shot(args[1], args[2]);
            return 0;
        }
        Tests();
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

    private static SComboBox NewKbn()
    {
        var c = new SComboBox();
        c.BeginUpdate();
        c.AddItem("正会員", 1);
        c.AddItem("準会員", 2);
        c.AddItem("賛助会員", 3);
        c.EndUpdate();
        return c;
    }

    /// <summary>
    /// 閉じた状態のコンボを画像に描かせて、文字の領域（左の余白と、右の矢印を除く）に何が描かれているかを返す。
    /// "none": 何も描かれていない、"light": 薄い色の文字（プレースホルダー）、"dark": 濃い色の文字（選択中の項目）。
    /// </summary>
    private static string TextInk(SComboBox combo)
    {
        Application.DoEvents();
        using (var bitmap = new Bitmap(combo.Width, combo.Height))
        {
            combo.DrawToBitmap(bitmap, new Rectangle(0, 0, combo.Width, combo.Height));
            int right = combo.Width - combo.Height - 6;   // 矢印の領域は、幅がおよそ高さと同じ
            Color back = bitmap.GetPixel(right, combo.Height / 2);
            bool any = false, dark = false;
            for (int y = 4; y < combo.Height - 4; y++)
            {
                for (int x = 6; x < right; x++)
                {
                    Color c = bitmap.GetPixel(x, y);
                    if (Math.Abs(c.R - back.R) + Math.Abs(c.G - back.G) + Math.Abs(c.B - back.B) > 40)
                    {
                        any = true;
                        if (c.R < 110) dark = true;
                    }
                }
            }
            return !any ? "none" : dark ? "dark" : "light";
        }
    }

    /// <summary>
    /// 閉じた状態のコンボを画像に描かせて、文字の領域に描かれた文字の、上下の高さ（画素）を返す。何も描かれていなければ 0。
    /// フォントを縮めて描いたかどうかを、文字の高さで確かめるのに使う。
    /// </summary>
    private static int InkHeight(SComboBox combo)
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

            SComboBox kbn = NewKbn();
            kbn.Location = new Point(10, 10);
            var flag = new SComboBox { Location = new Point(10, 50) };
            flag.AddItem("和暦", false);
            flag.AddItem("西暦", true);
            var table = new SComboBox { Location = new Point(10, 90) };
            var plain = new SComboBox { Location = new Point(10, 130) };
            plain.Items.AddRange(new object[] { "A", "B" });
            form.Controls.AddRange(new Control[] { kbn, flag, table, plain });
            form.Show();

            Check("initial index", kbn.SelectedIndex, -1);
            Check("initial value", kbn.SelectedValue == null, true);

            kbn.SelectedValue = 2;
            Check("select int", kbn.SelectedIndex + "/" + kbn.Text, "1/準会員");
            Check("get value", kbn.SelectedValue, 2);
            kbn.SelectedValue = (short)3;
            Check("select short", kbn.SelectedIndex, 2);
            kbn.SelectedValue = "1";
            Check("select string", kbn.SelectedIndex, 0);
            kbn.SelectedValue = 9;
            Check("select missing", kbn.SelectedIndex + "/" + kbn.Text, "-1/");
            kbn.SelectedValue = 1;
            kbn.SelectedValue = DBNull.Value;
            Check("select DBNull", kbn.SelectedIndex, -1);
            kbn.SelectedValue = 1;
            kbn.SelectedValue = null;
            Check("select null", kbn.SelectedIndex, -1);
            Check("SelectByValue result", kbn.SelectByValue(3) + "/" + kbn.SelectByValue(7), "True/False");
            Check("SelectByText", kbn.SelectByText("準会員") + "/" + kbn.SelectedIndex, "True/1");
            Check("GetValue/GetText", kbn.GetValue(2) + "/" + kbn.GetText(2) + "/" + (kbn.GetValue(5) == null), "3/賛助会員/True");

            flag.SelectedValue = true;
            Check("bool value", flag.SelectedIndex + "/" + flag.SelectedValue, "1/True");
            flag.SelectedValue = "False";
            Check("bool by string", flag.SelectedIndex, 0);

            int changed = 0, valueChanged = 0;
            kbn.SelectedIndexChanged += delegate { changed++; };
            kbn.SelectedValueChanged += delegate { valueChanged++; };
            kbn.SelectedValue = 3;
            Check("events", changed + "/" + valueChanged, "1/1");

            // DataSource（DataTable）でも同じ SelectedValue が使える
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add(10, "横浜銀行");
            dt.Rows.Add(20, "湘南信用金庫");
            dt.Rows.Add(30, "医師信用組合");
            table.DisplayMember = "Name";
            table.ValueMember = "Id";
            table.DataSource = dt;
            table.SelectedValue = 20;
            Check("table select", table.SelectedIndex + "/" + table.Text + "/" + table.SelectedValue, "1/湘南信用金庫/20");
            Check("table base SelectedValue", ((ListControl)table).SelectedValue, 20);
            Check("table GetValue", table.GetValue(2) + "/" + table.GetText(2), "30/医師信用組合");

            plain.SelectedValue = "B";
            Check("plain string items", plain.SelectedIndex + "/" + plain.SelectedValue, "1/B");

            // キー操作
            kbn.SelectedIndex = 0;
            Key(kbn, Keys.Down);
            Check("Down changes", kbn.SelectedIndex, 1);
            Key(kbn, Keys.F4);
            Check("F4 does not open", kbn.DroppedDown, false);
            Key(kbn, Keys.Space);
            Check("Space opens", kbn.DroppedDown, true);
            Key(kbn, Keys.Space);
            Check("Space closes", kbn.DroppedDown, false);
            SendMessage(kbn.Handle, WM_CHAR, (IntPtr)' ', (IntPtr)1);
            Check("Space char ignored", kbn.SelectedIndex, 1);
            kbn.OpenByF4Key = true;
            Key(kbn, Keys.F4);
            Check("F4 opens when enabled", kbn.DroppedDown, true);
            Key(kbn, Keys.F4);
            kbn.OpenByF4Key = false;

            // 読み取り専用
            kbn.ReadOnly = true;
            Key(kbn, Keys.Down);
            Key(kbn, Keys.End);
            Check("ReadOnly: keys", kbn.SelectedIndex, 1);
            Key(kbn, Keys.Space);
            Check("ReadOnly: Space", kbn.DroppedDown, false);
            SendMessage(kbn.Handle, WM_CHAR, (IntPtr)'賛', (IntPtr)1);
            Check("ReadOnly: type-ahead", kbn.SelectedIndex, 1);
            SendMessage(kbn.Handle, WM_LBUTTONDOWN, (IntPtr)1, (IntPtr)((5 << 16) | 5));
            SendMessage(kbn.Handle, WM_LBUTTONUP, IntPtr.Zero, (IntPtr)((5 << 16) | 5));
            Application.DoEvents();
            Check("ReadOnly: click", kbn.DroppedDown, false);
            SendMessage(kbn.Handle, WM_MOUSEWHEEL, (IntPtr)(-120 << 16), IntPtr.Zero);
            Check("ReadOnly: wheel", kbn.SelectedIndex, 1);
            kbn.SelectedValue = 3;
            Check("ReadOnly: programmatic", kbn.SelectedIndex, 2);
            kbn.ReadOnly = false;
            SendMessage(kbn.Handle, WM_LBUTTONDOWN, (IntPtr)1, (IntPtr)((5 << 16) | 5));
            Application.DoEvents();
            Check("click opens", kbn.DroppedDown, true);
            kbn.DroppedDown = false;

            // デザイナーの書き出し判定
            var props = System.ComponentModel.TypeDescriptor.GetProperties(flag);
            foreach (string p in new[] { "Font", "ForeColor", "DropDownStyle", "BorderStyle", "ReadOnly", "OpenBySpaceKey", "OpenByF4Key", "SelectionColor", "BorderColor" })
            {
                Check("ShouldSerialize " + p, props[p].ShouldSerializeValue(flag), false);
            }
            Check("DrawMode hidden", props["DrawMode"].SerializationVisibility, System.ComponentModel.DesignerSerializationVisibility.Hidden);
            Check("font", flag.Font.Name, new STextBox().Font.Name);

            // ---- プレースホルダー: 何も選択していないとき（空欄の項目を選んでいるときも）だけ、薄い色の文字を描く ----
            var ph = NewKbn();
            ph.AddItem("", 0);   // 空欄の項目（4 番目）
            ph.Location = new Point(10, 150);
            ph.Size = new Size(140, 24);
            form.Controls.Add(ph);
            Application.DoEvents();
            var phProps = System.ComponentModel.TypeDescriptor.GetProperties(ph);
            Check("placeholder: default", "[" + ph.PlaceholderText + "]/" + phProps["PlaceholderText"].ShouldSerializeValue(ph) + "/" + phProps["PlaceholderColor"].ShouldSerializeValue(ph), "[]/False/False");
            Check("placeholder: none without text", TextInk(ph), "none");
            ph.PlaceholderText = "入力必須";
            Check("placeholder: shown when nothing is selected", TextInk(ph) + "/" + ph.SelectedIndex + "/[" + ph.Text + "]", "light/-1/[]");
            Check("placeholder: serialized", phProps["PlaceholderText"].ShouldSerializeValue(ph), true);
            ph.SelectedIndex = 0;
            Check("placeholder: hidden when an item is selected", TextInk(ph), "dark");
            ph.SelectedIndex = 3;
            Check("placeholder: shown for the blank item", TextInk(ph), "light");
            ph.SelectedIndex = -1;
            ph.Enabled = false;
            Check("placeholder: hidden when disabled", TextInk(ph), "none");
            ph.Enabled = true;
            ph.ReadOnly = true;
            Check("placeholder: hidden when read-only", TextInk(ph), "none");
            ph.ReadOnly = false;
            Check("placeholder: shown again", TextInk(ph), "light");
            ph.PlaceholderText = null;
            Check("placeholder: null -> empty", "[" + ph.PlaceholderText + "]/" + TextInk(ph), "[]/none");

            // ---- SFittedFont: 文字列が収まる大きさを探す（収まる中でいちばん大きいもの。下限は 8 ポイント）----
            using (var fit = new SFittedFont())
            {
                Font baseFont = SControlStyle.Font;
                const TextFormatFlags fitFlags = TextFormatFlags.NoPadding | TextFormatFlags.SingleLine;
                const string longText = "神奈川県医師信用組合";
                Func<Font, int> measure = delegate(Font f) { return TextRenderer.MeasureText(longText, f, new Size(int.MaxValue, int.MaxValue), fitFlags).Width; };
                int full = measure(baseFont);

                Check("fit: wide enough", ReferenceEquals(fit.Get(longText, baseFont, full, fitFlags), baseFont) + "/" + fit.IsShrunk + "/" + fit.IsClipped, "True/False/False");

                int narrowWidth = full * 9 / 10;
                Font fitted = fit.Get(longText, baseFont, narrowWidth, fitFlags);
                float fittedSize = fitted.SizeInPoints;
                Check("fit: 90% of the width", (fittedSize < baseFont.SizeInPoints) + "/" + (fittedSize >= SFittedFont.MinimumSize) + "/" + (measure(fitted) <= narrowWidth) + "/" + fit.IsShrunk + "/" + fit.IsClipped,
                    "True/True/True/True/False");
                using (var bigger = new Font(baseFont.FontFamily, fittedSize + 0.5f, baseFont.Style, GraphicsUnit.Point))
                {
                    Check("fit: the largest size that fits", measure(bigger) > narrowWidth, true);
                }
                Check("fit: same request -> same font", ReferenceEquals(fit.Get(longText, baseFont, narrowWidth, fitFlags | TextFormatFlags.EndEllipsis), fitted), true);

                Font minimum = fit.Get(longText, baseFont, full / 2, fitFlags);
                Check("fit: 50% -> minimum size, clipped", minimum.SizeInPoints + "/" + fit.IsShrunk + "/" + fit.IsClipped, "8/True/True");
                Check("fit: empty text", ReferenceEquals(fit.Get("", baseFont, 10, fitFlags), baseFont) + "/" + fit.IsClipped, "True/False");
                using (var small = new Font(baseFont.FontFamily, 8f, baseFont.Style, GraphicsUnit.Point))
                {
                    Check("fit: already the minimum size", ReferenceEquals(fit.Get(longText, small, 10, fitFlags), small) + "/" + fit.IsShrunk + "/" + fit.IsClipped, "True/False/True");
                }
            }

            // ---- 閉じた状態の表示: 収まらない項目は、縮めて描く ----
            var fitCombo = new SComboBox { Location = new Point(10, 190), Size = new Size(130, 24) };
            fitCombo.BeginUpdate();
            fitCombo.AddItem("神奈川県医師信用組合", 1);
            fitCombo.AddItem("横浜", 2);
            fitCombo.EndUpdate();
            form.Controls.Add(fitCombo);
            Application.DoEvents();
            var fitProps = System.ComponentModel.TypeDescriptor.GetProperties(fitCombo);
            Check("shrink: default", fitCombo.ShrinkTextToFit + "/" + fitProps["ShrinkTextToFit"].ShouldSerializeValue(fitCombo), "True/False");
            fitCombo.SelectedIndex = 0;
            int shrunkHeight = InkHeight(fitCombo);
            fitCombo.ShrinkTextToFit = false;
            int normalHeight = InkHeight(fitCombo);
            Check("shrink: long item is drawn smaller", shrunkHeight > 0 && shrunkHeight < normalHeight, true);
            Check("shrink: serialized when turned off", fitProps["ShrinkTextToFit"].ShouldSerializeValue(fitCombo), true);
            fitCombo.SelectedIndex = 1;
            int shortNormal = InkHeight(fitCombo);
            fitCombo.ShrinkTextToFit = true;
            Check("shrink: short item keeps the size", InkHeight(fitCombo), shortNormal);
            form.Close();
        }
    }

    private static void Shot(string closedPng, string openPng)
    {
        IntPtr previous = GetForegroundWindow();
        using (var form = new Form())
        {
            form.Text = "SComboBox 検証";
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(40, 40);
            form.ClientSize = new Size(470, 420);
            form.BackColor = Color.FromArgb(244, 252, 254);
            form.TopMost = true;

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

            SComboBox focus = NewKbn(); focus.Width = 120; focus.SelectedValue = 1;
            add("フォーカスあり", focus);
            SComboBox normal = NewKbn(); normal.Width = 300; normal.SelectedValue = 2;
            add("通常", normal);
            add("テキスト(比較用)", new STextBox { Text = "準会員", Width = 300 });
            SComboBox none = NewKbn(); none.Width = 300;
            add("未選択", none);
            SComboBox disabled = NewKbn(); disabled.Width = 300; disabled.SelectedValue = 3; disabled.Enabled = false;
            add("無効", disabled);
            SComboBox ro = NewKbn(); ro.Width = 300; ro.SelectedValue = 1; ro.ReadOnly = true;
            add("読み取り専用", ro);
            SComboBox borderless = NewKbn(); borderless.Width = 300; borderless.SelectedValue = 2; borderless.BorderStyle = BorderStyle.None; borderless.BackColor = Color.FromArgb(255, 250, 220);
            add("枠線なし", borderless);
            SComboBox open = NewKbn(); open.Width = 200; open.SelectedValue = 2;
            add("一覧を開く", open);

            form.Show();
            form.Activate();
            focus.Focus();
            Application.DoEvents();
            System.Threading.Thread.Sleep(300);
            Application.DoEvents();
            Capture(form, closedPng);

            open.Focus();
            open.DroppedDown = true;
            Application.DoEvents();
            System.Threading.Thread.Sleep(400);
            Application.DoEvents();
            Capture(form, openPng);
            open.DroppedDown = false;
            form.Close();
        }
        if (previous != IntPtr.Zero)
        {
            SetForegroundWindow(previous);
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
}
