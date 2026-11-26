// SNumberBox の検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ComponentSUI;

internal static class NumberHarness
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
    private const int WM_IME_CHAR = 0x0286;

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
                Shot(args[1]);
                return 0;
            }
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
        string a = Convert.ToString(actual, System.Globalization.CultureInfo.InvariantCulture);
        string x = Convert.ToString(expected, System.Globalization.CultureInfo.InvariantCulture);
        bool ok = a == x;
        if (!ok) _failed++;
        Console.WriteLine((ok ? "ok   " : "NG   ") + name + " => [" + a + "]" + (ok ? "" : " expected [" + x + "]"));
    }

    private static void Type(Control c, string chars)
    {
        foreach (char ch in chars) SendMessage(c.Handle, WM_CHAR, (IntPtr)ch, (IntPtr)1);
        Application.DoEvents();
    }

    private static string V(SNumberBox box)
    {
        return box.Value.HasValue ? box.Value.Value.ToString(System.Globalization.CultureInfo.InvariantCulture) : "null";
    }

    /// <summary>コントロールを画像に描かせて、その内容を文字列にする（表示が変わったかを比べるのに使う）。</summary>
    private static string Image(Control control)
    {
        Application.DoEvents();
        using (var bitmap = new Bitmap(control.Width, control.Height))
        {
            control.DrawToBitmap(bitmap, new Rectangle(0, 0, control.Width, control.Height));
            var text = new System.Text.StringBuilder();
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    text.Append(bitmap.GetPixel(x, y).ToArgb().ToString("X8"));
                }
            }
            return text.ToString();
        }
    }

    private static void Tests()
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ShowInTaskbar = false;
            var other = new STextBox { Location = new Point(10, 10) };
            var num = new SNumberBox { Location = new Point(10, 50) };
            form.Controls.Add(other);
            form.Controls.Add(num);
            form.Show();
            form.Activate();
            other.Focus();
            Application.DoEvents();

            int changed = 0, invalid = 0;
            num.ValueChanged += delegate { changed++; };
            num.InvalidInput += delegate { invalid++; };

            Check("initial", V(num) + "/" + num.Text, "null/");
            Check("defaults", num.TextAlign + "/" + num.ImeMode + "/" + num.HighlightText, "Right/Disable/True");

            num.Value = 1234567;
            Check("display format", num.Text + "/" + changed, "1,234,567/1");
            num.Value = 1234567;
            Check("same value: no event", changed, 1);

            // フォーカスで編集用の表示になり、全選択される
            num.Focus();
            Application.DoEvents();
            Check("edit format", num.Text + "/" + num.SelectionLength, "1234567/7");
            changed = 0;
            Type(num, "5");
            Check("typing replaces selection", num.Text + "/" + V(num), "5/5");
            Type(num, "00");
            Check("typing digits", num.Text + "/" + V(num) + "/" + changed, "500/500/3");
            Type(num, ".");
            Check("period multiplies by 1000", num.Text + "/" + V(num), "500000/500000");
            other.Focus();
            Application.DoEvents();
            Check("leave: display format", num.Text + "/" + V(num), "500,000/500000");

            // Tab での移動（Leave の時点では、まだフォーカスが残っている）: 表示用の文字列に直すときに、離れていく入力欄を全選択しない
            num.Focus(); Application.DoEvents();
            other.Select(); Application.DoEvents();
            Check("leave by Tab: not selected again", num.Text + "/" + num.SelectionLength, "500,000/0");
            // フォーカスがある間に値を設定した場合は、全選択し直す
            num.Focus(); Application.DoEvents();
            num.Value = 42;
            Check("value set while focused: selected again", num.Text + "/" + num.SelectionLength, "42/2");
            num.Value = 500000;
            other.Focus(); Application.DoEvents();

            // 桁数の制限
            num.SetDigits(7, 0, true);
            Check("SetDigits", num.MaxValue + "/" + num.MinValue + "/" + num.DecimalPlaces, "9999999/-9999999/0");
            num.Focus(); Application.DoEvents();
            invalid = 0;
            Type(num, "123456789");
            Check("7 digits max", num.Text + "/" + invalid, "1234567/2");
            Type(num, ".");
            Check("x1000 over range rejected", num.Text + "/" + invalid, "1234567/3");
            Type(num, "a ,");
            Check("non-number chars rejected", num.Text, "1234567");

            // 符号
            num.SelectAll();
            Type(num, "-");
            Check("minus on select-all starts over", num.Text + "/" + V(num), "-/null");
            Type(num, "12");
            Check("negative", num.Text + "/" + V(num), "-12/-12");
            Type(num, "-");
            Check("minus toggles", num.Text + "/" + V(num), "12/12");
            Type(num, "-");
            Type(num, "+");
            Check("plus makes positive", num.Text + "/" + V(num), "12/12");
            Type(num, "\b\b");
            Check("backspace to blank", num.Text + "/" + V(num), "/null");
            other.Focus(); Application.DoEvents();
            Check("blank stays blank", num.Text + "/" + V(num), "/null");

            // マイナスを入力できない設定
            num.MinValue = 0;
            num.Focus(); Application.DoEvents();
            invalid = 0;
            Type(num, "-5");
            Check("no negative", num.Text + "/" + invalid, "5/1");

            // 全角の数字（IME 経由）
            num.SelectAll();
            foreach (char c in "１２３") { SendMessage(num.Handle, WM_IME_CHAR, (IntPtr)c, (IntPtr)1); }
            Application.DoEvents();
            Check("full-width digits", num.Text + "/" + V(num), "123/123");

            // 範囲
            num.MaxValue = 500;
            Check("clamped by MaxValue", V(num), "123");
            num.SelectAll();
            Type(num, "501");
            Check("over max rejected", num.Text, "50");
            num.MinValue = 10;
            num.SelectAll();
            Type(num, "5");
            other.Focus(); Application.DoEvents();
            Check("below min: clamped on leave", num.Text + "/" + V(num), "10/10");

            // 小数
            var dec = new SNumberBox { Location = new Point(10, 90) };
            dec.SetDigits(3, 1, true);
            form.Controls.Add(dec);
            dec.Value = 5;
            Check("fixed decimals", dec.Text, "5.0");
            dec.Value = 12.36m;
            Check("rounded", dec.Text + "/" + V(dec), "12.4/12.4");
            dec.Focus(); Application.DoEvents();
            Check("edit format with decimals", dec.Text, "12.4");
            Type(dec, "1234.567");
            Check("decimal typing", dec.Text + "/" + V(dec), "123.5/123.5");
            dec.SelectAll();
            Type(dec, ".");
            Check("leading point", dec.Text + "/" + V(dec), "./null");
            Type(dec, "5");
            other.Focus(); Application.DoEvents();
            Check("point five", dec.Text + "/" + V(dec), "0.5/0.5");
            dec.Value = -1234.5m;
            Check("clamped to min", dec.Text, "-999.9");

            // 空欄を許さない
            var req = new SNumberBox { Location = new Point(10, 130), AllowBlank = false };
            form.Controls.Add(req);
            Check("no blank: initial", req.Text + "/" + V(req), "0/0");
            req.Value = null;
            Check("no blank: null becomes 0", V(req), "0");
            req.Focus(); Application.DoEvents();
            Type(req, "\b");
            Check("no blank: empty while editing", req.Text + "/" + V(req), "/0");
            other.Focus(); Application.DoEvents();
            Check("no blank: leave", req.Text, "0");

            // Text からの設定
            var txt = new SNumberBox { Location = new Point(10, 170) };
            form.Controls.Add(txt);
            txt.Text = "1,234";
            Check("Text = \"1,234\"", V(txt) + "/" + txt.Text, "1234/1,234");
            txt.Text = "￥１２，３４５";
            Check("Text full-width", V(txt), "12345");
            txt.Text = "-98.7";
            Check("Text decimals rounded", V(txt) + "/" + txt.Text, "-99/-99");
            txt.Text = "abc";
            Check("Text not a number", V(txt) + "/" + txt.Text, "null/");
            txt.Value = 1234567;
            txt.ThousandsSeparator = false;
            Check("no separator", txt.Text, "1234567");
            txt.Clear();
            Check("Clear()", V(txt), "null");

            // 読み取り専用
            txt.ThousandsSeparator = true;
            txt.Value = 1000;
            txt.ReadOnly = true;
            txt.Focus(); Application.DoEvents();
            Check("ReadOnly keeps display format", txt.Text, "1,000");
            txt.ReadOnly = false;
            Check("ReadOnly off while focused", txt.Text, "1000");
            other.Focus(); Application.DoEvents();

            // デザイナーの書き出し判定
            var fresh = new SNumberBox();
            var props = System.ComponentModel.TypeDescriptor.GetProperties(fresh);
            foreach (string p in new[] { "Value", "MaxValue", "MinValue", "DecimalPlaces", "ThousandsSeparator", "AllowBlank", "HighlightText", "ImeMode", "Font", "PeriodMultipliesByThousand" })
            {
                Check("ShouldSerialize " + p, props[p].ShouldSerializeValue(fresh), false);
            }
            Check("hidden Text", props["Text"].SerializationVisibility, System.ComponentModel.DesignerSerializationVisibility.Hidden);
            // 文字の種類の指定（Format・AllowSpace）は STextBox だけが持つ
            Check("no Format / AllowSpace", (props["Format"] == null) + "/" + (props["AllowSpace"] == null), "True/True");

            // ---- 収まらない文字を縮める設定は、数値の入力欄では、既定でオフ（欄ごとにオンにできる）----
            Check("shrink: default is off", fresh.ShrinkTextToFit + "/" + props["ShrinkTextToFit"].ShouldSerializeValue(fresh), "False/False");
            var narrowNumber = new SNumberBox { Location = new Point(10, 200), Width = 60 };
            form.Controls.Add(narrowNumber);
            narrowNumber.Value = 1234567m;       // 「1,234,567」は、幅 60 には収まらない
            other.Focus(); Application.DoEvents();
            string plainNumber = Image(narrowNumber);
            narrowNumber.ShrinkTextToFit = true;
            Check("shrink: drawn differently when turned on", Image(narrowNumber) != plainNumber, true);
            Check("shrink: serialized when turned on", props["ShrinkTextToFit"].ShouldSerializeValue(narrowNumber), true);
            Check("shrink: the value is untouched", narrowNumber.Value + "/" + narrowNumber.Text, "1234567/1,234,567");
            narrowNumber.ShrinkTextToFit = false;
            Check("shrink: turned off again", Image(narrowNumber) == plainNumber, true);
            form.Close();
        }
    }

    private static void Shot(string png)
    {
        using (var form = new Form())
        {
            form.Text = "SNumberBox 検証";
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(40, 40);
            form.ClientSize = new Size(420, 260);
            form.BackColor = Color.FromArgb(244, 252, 254);
            form.TopMost = true;

            int y = 14;
            Func<string, Control, Control> add = delegate(string label, Control box)
            {
                var l = new Label { Text = label, Location = new Point(12, y + 5), AutoSize = true, Font = new STextBox().Font };
                box.Location = new Point(190, y);
                form.Controls.Add(l);
                form.Controls.Add(box);
                y += box.Height + 8;
                return box;
            };

            var focus = new SNumberBox { Value = 1234567 };
            add("フォーカスあり(編集中)", focus);
            add("通常", new SNumberBox { Value = 1234567 });
            add("負の数", new SNumberBox { Value = -98765 });
            var dec = new SNumberBox(); dec.SetDigits(5, 2, true); dec.Value = 1234.5m;
            add("小数 2 桁", dec);
            add("未入力", new SNumberBox());
            add("無効(合計の表示)", new SNumberBox { Value = 36000, Enabled = false });
            add("STextBox(比較)", new STextBox { Text = "1,234,567", ContentAlignment = ContentAlignment.MiddleRight });

            form.Show();
            form.Activate();
            focus.Focus();
            focus.Select(focus.TextLength, 0);
            Application.DoEvents();
            System.Threading.Thread.Sleep(300);
            Application.DoEvents();
            Rectangle r = form.RectangleToScreen(form.ClientRectangle);
            using (var bmp = new Bitmap(r.Width, r.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp)) g.CopyFromScreen(r.Location, Point.Empty, r.Size);
                bmp.Save(png, ImageFormat.Png);
            }
            form.Close();
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
            var other = new TextBox { Location = new Point(10, 10) };
            var number = new SNumberBox { Location = new Point(10, 50), Width = 200 };
            form.Controls.Add(other);
            form.Controls.Add(number);
            form.Show();
            form.Activate();
            Application.DoEvents();

            number.SetDigits(7, 2, false);
            number.Value = 1234567.89m;
            int invalid = 0;
            int changed = 0;
            number.InvalidInput += delegate { invalid++; };
            number.ValueChanged += delegate { changed++; };

            // 小数点を Delete で消すと、整数が 9 桁になって桁数を超える。元に戻す（値を範囲の端に丸めない）
            number.Focus();
            Application.DoEvents();
            number.Select(7, 0);
            SendMessage(number.Handle, 0x0100, (IntPtr)(int)Keys.Delete, (IntPtr)1);
            Application.DoEvents();
            Check("Delete of decimal point is reverted", number.Text + "/" + V(number) + "/" + invalid + "/" + changed + "/" + number.SelectionStart, "1234567.89/1234567.89/1/0/7");

            // 受け付けられる削除は、そのまま通る
            number.Select(9, 0);
            SendMessage(number.Handle, 0x0100, (IntPtr)(int)Keys.Delete, (IntPtr)1);
            Application.DoEvents();
            Check("acceptable Delete", number.Text + "/" + V(number) + "/" + invalid, "1234567.8/1234567.8/1");

            other.Focus();
            Application.DoEvents();
            Check("after leave", number.Text + "/" + V(number), "1,234,567.80/1234567.8");
            form.Close();
        }
    }
}
