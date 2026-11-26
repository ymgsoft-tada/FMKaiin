// STextBox の検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ComponentSUI;

internal static class TextHarness
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
        if (args.Length > 0 && args[0] == "shot")
        {
            Shot(args[1], args[2]);
            return 0;
        }
        FilterTests();
        ControlTests();
        FixTests();
        Console.WriteLine(_failed == 0 ? "ALL PASSED" : ("FAILED: " + _failed));
        return _failed;
    }

    private static void Check(string name, string actual, string expected)
    {
        bool ok = actual == expected;
        if (!ok) _failed++;
        Console.WriteLine((ok ? "ok   " : "NG   ") + name + " => [" + actual + "]" + (ok ? "" : " expected [" + expected + "]"));
    }

    private static SInputFilter F(string format, SAllowSpace space)
    {
        var f = new SInputFilter();
        f.Format = new SInputFormat(format);
        f.AllowSpace = space;
        return f;
    }

    private static void FilterTests()
    {
        Check("9", F("9", SAllowSpace.None).Accept("12a３４ 5", "", 0, 0, false), "12345");
        Check("H narrow", F("H", SAllowSpace.Narrow).Accept("アあガ　xＡ", "", 0, 0, false), "ｱｱｶﾞ xA");
        Check("^@", F("^@", SAllowSpace.Both).Accept("山田-太郎 #1a", "", 0, 0, false), "山田－太郎 ＃1a");
        Check("Aa9", F("Aa9", SAllowSpace.Narrow).Accept("ＡＢc１ あ-", "", 0, 0, false), "ABc1 ");
        Check("A upper", F("A", SAllowSpace.None).Accept("abcｄ", "", 0, 0, false), "ABCD");
        Check("N small", F("N", SAllowSpace.None).Accept("ｷｬｯﾄ", "", 0, 0, false), "ｷﾔﾂﾄ");
        Check("Ｋ", F("Ｋ", SAllowSpace.None).Accept("あｲう", "", 0, 0, false), "アイウ");
        Check("Ｊ", F("Ｊ", SAllowSpace.None).Accept("ｱイう", "", 0, 0, false), "あいう");
        Check("space none", F("", SAllowSpace.None).Accept("a b　c", "", 0, 0, false), "abc");
        Check("space wide", F("", SAllowSpace.Wide).Accept("a b　c", "", 0, 0, false), "a　b　c");
        Check("H^@", F("H^@", SAllowSpace.None).Accept("ab-c!ｱ", "", 0, 0, false), "abcｱ");
        Check("escape", F("9\\A", SAllowSpace.None).Accept("1AB2", "", 0, 0, false), "1A2");

        var noconv = F("9", SAllowSpace.None);
        noconv.AutoConvert = false;
        Check("no convert", noconv.Accept("1２3", "", 0, 0, false), "13");

        var len = F("", SAllowSpace.Both);
        len.MaxLength = 5;
        Check("max char", len.Accept("defg", "abc", 3, 0, false), "de");
        Check("max replace sel", len.Accept("XYZ", "abcde", 1, 3, false), "XYZ");
        Check("max full", len.Accept("x", "abcde", 5, 0, false), "");
        len.MaxLength = 2;
        len.MaxLengthUnit = SLengthUnit.TextElement;
        Check("max element", len.Accept("𠮷野家", "", 0, 0, false), "𠮷野");
        len.MaxLengthUnit = SLengthUnit.Char;
        Check("max char surrogate", len.Accept("a𠮷", "", 0, 0, false), "a");
        len.MaxLength = 5;
        len.MaxLengthUnit = SLengthUnit.Byte;
        Check("max byte", len.Accept("aあｱい", "", 0, 0, false), "aあｱ");

        var crlf = F("", SAllowSpace.Both);
        Check("crlf nocontrol single", crlf.Accept("a\r\nb", "", 0, 0, false), "a");
        Check("crlf nocontrol multi", crlf.Accept("a\r\nb", "", 0, 0, true), "a\r\nb");
        crlf.AcceptsCrLf = SCrLfMode.Filter;
        Check("crlf filter", crlf.Accept("a\r\nb\nc", "", 0, 0, true), "abc");
        crlf.AcceptsCrLf = SCrLfMode.Cut;
        Check("crlf cut", crlf.Accept("a\r\nb", "", 0, 0, true), "a");
    }

    private static void Type(STextBox box, int msg, string chars)
    {
        foreach (char c in chars)
        {
            SendMessage(box.Handle, msg, (IntPtr)c, (IntPtr)1);
        }
    }

    private static void ControlTests()
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ShowInTaskbar = false;

            var digits = new STextBox { Format = "9", AllowSpace = SAllowSpace.None, MaxLength = 4, Location = new Point(10, 10) };
            var kana = new STextBox { Format = "H", AllowSpace = SAllowSpace.Narrow, Location = new Point(10, 50) };
            var free = new STextBox { Location = new Point(10, 90), Size = new Size(200, 40) };
            var multi = new STextBox { Location = new Point(10, 140), Size = new Size(200, 80), Multiline = true };
            form.Controls.AddRange(new Control[] { digits, kana, free, multi });
            int invalid = 0;
            digits.InvalidInput += delegate { invalid++; };
            form.Show();

            Type(digits, WM_CHAR, "1a２ 34567");
            Check("WM_CHAR digits", digits.Text, "1234");
            Check("InvalidInput count", invalid.ToString(), "5");

            var plain = new TextBox { Location = new Point(300, 10) }; form.Controls.Add(plain); SendMessage(plain.Handle, WM_IME_CHAR, (IntPtr)(int)'あ', (IntPtr)1); Console.WriteLine("  plain TextBox before pump [" + plain.Text + "]"); Application.DoEvents(); Console.WriteLine("  plain TextBox after pump [" + plain.Text + "]");
            foreach (char c in "アあ　ガ") { Type(kana, WM_IME_CHAR, c.ToString()); Application.DoEvents(); Console.WriteLine("  after " + c + " -> [" + kana.Text + "]"); }
            Check("WM_IME_CHAR kana", kana.Text, "ｱｱ ｶﾞ");

            kana.Text = ""; Type(kana, WM_IME_CHAR, "ガアがｯ"); Application.DoEvents();
            Check("WM_IME_CHAR order (no pump between)", kana.Text, "ｶﾞｱｶﾞｯ");
            int pressed = 0; digits.Text = ""; digits.KeyPress += delegate(object s, KeyPressEventArgs e) { pressed++; if (e.KeyChar == '9') e.Handled = true; };
            Type(digits, WM_CHAR, "1x9２"); Check("KeyPress handled", digits.Text + "/" + pressed, "12/4");
            Type(free, WM_CHAR, "abc");
            Check("free", free.Text, "abc");
            free.Text = "プログラムからの設定は制限しない";
            Check("programmatic", free.Text, "プログラムからの設定は制限しない");

            Console.WriteLine("default size " + digits.Size + " client " + digits.ClientSize + " font " + digits.Font.Name + " " + digits.Font.SizeInPoints + "pt");
            Console.WriteLine("free(40px high) client " + free.ClientRectangle + " screenOffset " + (free.PointToScreen(Point.Empty).Y - free.Parent.PointToScreen(free.Location).Y));
            free.ContentAlignment = ContentAlignment.BottomRight;
            Console.WriteLine("bottom-right: TextAlign " + free.TextAlign + " offsetY " + (free.PointToScreen(Point.Empty).Y - free.Parent.PointToScreen(free.Location).Y));
            free.ContentAlignment = ContentAlignment.TopLeft;
            Console.WriteLine("top-left: offsetY " + (free.PointToScreen(Point.Empty).Y - free.Parent.PointToScreen(free.Location).Y));
            Console.WriteLine("multi client " + multi.ClientRectangle);
            Check("ContentAlignment roundtrip", free.ContentAlignment.ToString(), "TopLeft");
            Check("MaxLength default", free.MaxLength.ToString(), "32767");
            Check("native MaxLength (digits)", ((TextBoxBase)digits).MaxLength.ToString(), "4");
            digits.MaxLengthUnit = SLengthUnit.TextElement;
            Check("native MaxLength (element unit)", ((TextBoxBase)digits).MaxLength.ToString(), "0");

            // デザイナーの書き出し判定
            var props = System.ComponentModel.TypeDescriptor.GetProperties(free);
            Check("ShouldSerialize Font", props["Font"].ShouldSerializeValue(free).ToString(), "False");
            Check("ShouldSerialize ForeColor", props["ForeColor"].ShouldSerializeValue(free).ToString(), "False");
            Check("ShouldSerialize AutoSize", props["AutoSize"].ShouldSerializeValue(free).ToString(), "False");
            Check("ShouldSerialize BorderStyle", props["BorderStyle"].ShouldSerializeValue(free).ToString(), "False");
            Check("ShouldSerialize MaxLength", props["MaxLength"].ShouldSerializeValue(free).ToString(), "False");
            free.Font = new Font("Meiryo UI", 9f);
            Check("ShouldSerialize Font (changed)", props["Font"].ShouldSerializeValue(free).ToString(), "True");
            free.ResetFont();
            Check("ResetFont", free.Font.Equals(digits.Font).ToString(), "True");
            form.Close();
        }
    }

    private static void Shot(string screenPng, string printPng)
    {
        IntPtr previous = GetForegroundWindow();
        using (var form = new Form())
        {
            form.Text = "STextBox 検証";
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(40, 40);
            form.ClientSize = new Size(470, 440);
            form.BackColor = Color.FromArgb(244, 252, 254);
            form.TopMost = true;

            int y = 14;
            Func<string, STextBox, STextBox> add = delegate(string label, STextBox box)
            {
                var l = new Label { Text = label, Location = new Point(12, y + 5), AutoSize = true, Font = box.Font };
                box.Location = new Point(150, y);
                if (box.Width == 150) box.Width = 300;
                form.Controls.Add(l);
                form.Controls.Add(box);
                y += box.Height + 8;
                return box;
            };

            STextBox focus = add("フォーカスあり", new STextBox { Text = "公益社団法人 藤沢市医師会" });
            add("通常", new STextBox { Text = "ｺｳｴｷｼﾔﾀﾞﾝﾎｳｼﾞﾝ ﾌｼﾞｻﾜｼｲｼｶｲ" });
            add("無効", new STextBox { Text = "鈴木 紳一郎（表示欄）", Enabled = false });
            add("無効・右寄せ", new STextBox { Text = "1,234,567", Enabled = false, ContentAlignment = ContentAlignment.MiddleRight });
            add("読み取り専用", new STextBox { Text = "ReadOnly", ReadOnly = true });
            add("パスワード", new STextBox { Text = "secret", PasswordChar = '*' });
            add("右寄せ", new STextBox { Text = "0466", ContentAlignment = ContentAlignment.MiddleRight, Width = 80 });
            add("枠線なし", new STextBox { Text = "BorderStyle.None", BorderStyle = BorderStyle.None, BackColor = Color.FromArgb(255, 250, 220) });
            add("下寄せ(高さ40)", new STextBox { Text = "BottomLeft", ContentAlignment = ContentAlignment.BottomLeft, Height = 40 });
            add("上寄せ(高さ40)", new STextBox { Text = "TopLeft", ContentAlignment = ContentAlignment.TopLeft, Height = 40 });
            add("複数行", new STextBox { Text = "1 行目\r\n2 行目\r\n3 行目", Multiline = true, Height = 62 });

            form.Show();
            form.Activate();
            focus.Focus();
            focus.Select(focus.TextLength, 0);
            Application.DoEvents();
            System.Threading.Thread.Sleep(350);
            Application.DoEvents();

            Rectangle r = form.RectangleToScreen(form.ClientRectangle);
            using (var bmp = new Bitmap(r.Width, r.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(r.Location, Point.Empty, r.Size);
                }
                bmp.Save(screenPng, ImageFormat.Png);
            }
            using (var bmp = new Bitmap(form.ClientSize.Width, form.ClientSize.Height))
            {
                // 画面以外への描画（WM_PRINT）の確認
                foreach (Control c in form.Controls)
                {
                    using (var part = new Bitmap(c.Width, c.Height))
                    {
                        c.DrawToBitmap(part, new Rectangle(Point.Empty, c.Size));
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            if (c == form.Controls[0]) g.Clear(form.BackColor);
                            g.DrawImageUnscaled(part, c.Location);
                        }
                    }
                }
                bmp.Save(printPng, ImageFormat.Png);
            }
            Console.WriteLine("focused=" + focus.Focused + " scale=" + (form.DeviceDpi) );
            form.Close();
        }
        if (previous != IntPtr.Zero)
        {
            SetForegroundWindow(previous);
        }
    }

    /// <summary>コントロールを画像に描いて、その内容を 1 つの値にまとめる（2 つの状態で、表示が同じかどうかを比べるためのもの）。</summary>
    private static string Pixels(Control control)
    {
        using (var bitmap = new Bitmap(control.Width, control.Height))
        {
            control.DrawToBitmap(bitmap, new Rectangle(0, 0, control.Width, control.Height));
            long hash = 17;
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    hash = unchecked(hash * 31 + bitmap.GetPixel(x, y).ToArgb());
                }
            }
            return hash.ToString();
        }
    }

    // ---- レビューで見つかった不具合の再発防止 ----
    private static void FixTests()
    {
        // 貼り付けたタブ: 1 行の入力欄では捨てる、複数行では残す
        Check("tab dropped (single line)", F("9", SAllowSpace.None).Accept("123\t456\r\n", "", 0, 0, false), "123456");
        Check("tab kept (multiline)", F("", SAllowSpace.Both).Accept("a\tb", "", 0, 0, true), "a\tb");

        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ShowInTaskbar = false;
            var digits = new STextBox { Format = "9", AllowSpace = SAllowSpace.None, Location = new Point(10, 10) };
            form.Controls.Add(digits);
            form.Show();

            // Ctrl+BackSpace（DEL 文字）は入らない
            Type(digits, WM_CHAR, "12");
            SendMessage(digits.Handle, WM_CHAR, (IntPtr)0x7F, (IntPtr)1);
            Application.DoEvents();
            Check("Ctrl+BackSpace inserts nothing", digits.Text, "12");

            // 文字の種類の指定は STextBox だけが持つ（数値・日付・テーブル コンボは STextBox ではない）
            object number = new SNumberBox();
            object date = new SDateBox();
            object table = new STableComboBox();
            Check("derived controls are not STextBox", (number is STextBox) + "/" + (date is STextBox) + "/" + (table is STextBox), "False/False/False");
            Check("derived controls share SEditBase", (number is SEditBase) + "/" + (date is SEditBase) + "/" + (table is SEditBase) + "/" + (digits is SEditBase), "True/True/True/True");

            // ---- フォーカスがあるときの背景色 ----
            // 入力部分は BackColor の値で塗られるので、フォーカスがある間だけ BackColor が切り替わる。設定してある色は NormalBackColor で読める
            var away = new TextBox { Location = new Point(10, 50) };
            form.Controls.Add(away);
            form.Activate();
            away.Focus();
            Application.DoEvents();
            int window = SystemColors.Window.ToArgb();
            Check("focus fill: off without focus", (digits.BackColor.ToArgb() == window).ToString(), "True");
            digits.Focus();
            Application.DoEvents();
            Check("focus fill: focused", (digits.BackColor == digits.FocusBackColor) + "/" + (digits.NormalBackColor.ToArgb() == window), "True/True");
            digits.ReadOnly = true;
            Check("focus fill: not while read-only", (digits.BackColor == digits.FocusBackColor).ToString(), "False");
            digits.ReadOnly = false;
            Check("focus fill: back after read-only", (digits.BackColor == digits.FocusBackColor).ToString(), "True");
            Color focusColor = digits.FocusBackColor;
            digits.FocusBackColor = Color.Empty;
            Check("focus fill: turned off by an empty color", (digits.BackColor.ToArgb() == window).ToString(), "True");
            digits.FocusBackColor = focusColor;
            Check("focus fill: turned on again", (digits.BackColor == digits.FocusBackColor).ToString(), "True");
            away.Focus();
            Application.DoEvents();
            Check("focus fill: off after leaving", (digits.BackColor.ToArgb() == window).ToString(), "True");

            // 既定値のままなら、デザイナーは書き出さない
            var fresh = new STextBox();
            var freshProps = System.ComponentModel.TypeDescriptor.GetProperties(fresh);
            Check("ShouldSerialize colors", freshProps["FocusBackColor"].ShouldSerializeValue(fresh) + "/" + freshProps["TextSelectionColor"].ShouldSerializeValue(fresh) + "/" + freshProps["BackColor"].ShouldSerializeValue(fresh), "False/False/False");

            // 選択の色の上の文字: 濃い色の上では白、明るい色の上では元の色
            Check("text color on selection", (SControlStyle.GetTextColorOn(Color.SlateBlue, Color.Black).ToArgb() == Color.White.ToArgb()) + "/" + (SControlStyle.GetTextColorOn(Color.FromArgb(214, 231, 250), Color.Black).ToArgb() == Color.Black.ToArgb()), "True/True");

            // フォーカスがある間にプログラムから文字列を設定した場合（画面を開いた直後の先頭の項目など）: 全選択にする設定なら、全選択し直す
            digits.HighlightText = true;
            digits.Focus();
            Application.DoEvents();
            digits.Text = "123";
            Check("text set while focused: selected again", digits.SelectionStart + "/" + digits.SelectionLength, "0/3");
            digits.HighlightText = false;
            digits.Text = "45";
            Check("text set while focused: not selected without HighlightText", digits.SelectionLength.ToString(), "0");
            away.Focus();
            Application.DoEvents();
            digits.HighlightText = true;
            digits.Text = "678";
            Check("text set without focus: not selected", digits.SelectionLength.ToString(), "0");

            // プレースホルダー: 入力欄が空のときだけ描く。Text の値には影響しない。読み取り専用のときは描かない
            digits.Text = "";
            string emptyImage = Pixels(digits);
            digits.PlaceholderText = "入力必須";
            Check("placeholder: drawn when empty / Text unchanged", (Pixels(digits) != emptyImage) + "/[" + digits.Text + "]", "True/[]");
            digits.ReadOnly = true;
            string readOnlyImage = Pixels(digits);
            digits.PlaceholderText = "";
            Check("placeholder: not drawn while read-only", (Pixels(digits) == readOnlyImage).ToString(), "True");
            digits.ReadOnly = false;
            digits.Text = "12";
            string filledImage = Pixels(digits);
            digits.PlaceholderText = "入力必須";
            Check("placeholder: not drawn when filled", (Pixels(digits) == filledImage).ToString(), "True");
            Check("ShouldSerialize placeholder", freshProps["PlaceholderText"].ShouldSerializeValue(fresh) + "/" + freshProps["PlaceholderColor"].ShouldSerializeValue(fresh), "False/False");

            // ---- 収まらない文字は、フォーカスがない間、縮めて表示する（テキストの入力欄では、既定でオン）----
            Check("shrink: default is on", fresh.ShrinkTextToFit + "/" + freshProps["ShrinkTextToFit"].ShouldSerializeValue(fresh), "True/False");
            var longBox = new STextBox { Location = new Point(10, 230), Width = 120, Text = "神奈川県医師信用組合" };
            form.Controls.Add(longBox);
            away.Focus(); Application.DoEvents();
            string shrunkImage = Pixels(longBox);
            longBox.ShrinkTextToFit = false;
            string plainImage = Pixels(longBox);
            Check("shrink: long text is drawn differently", (shrunkImage != plainImage).ToString(), "True");
            Check("shrink: serialized when turned off", freshProps["ShrinkTextToFit"].ShouldSerializeValue(longBox).ToString(), "True");
            longBox.ShrinkTextToFit = true;
            Check("shrink: turned on again", (Pixels(longBox) == shrunkImage) + "/" + longBox.Text, "True/神奈川県医師信用組合");
            longBox.Text = "横浜";
            string shortImage = Pixels(longBox);
            longBox.ShrinkTextToFit = false;
            Check("shrink: short text is drawn the same", (Pixels(longBox) == shortImage).ToString(), "True");
            longBox.ShrinkTextToFit = true;
            // 複数行の入力欄では、縮めない（設定していても、標準の表示のまま）
            var memo = new STextBox { Location = new Point(150, 230), Size = new Size(120, 60), Multiline = true, Text = "神奈川県医師信用組合の本店" };
            form.Controls.Add(memo);
            Application.DoEvents();
            string memoImage = Pixels(memo);
            memo.ShrinkTextToFit = false;
            Check("shrink: multiline is not shrunk", (Pixels(memo) == memoImage).ToString(), "True");
            form.Close();
        }
    }
}
