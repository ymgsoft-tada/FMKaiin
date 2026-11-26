// SRadioButton の検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
// フォームにラジオ ボタンを置き、既定値・デザイナーへの書き出しの判定・クリックとキーでの選択（組の中で 1 つだけ選ばれること）・
// 描いた丸の大きさと位置を確かめる。
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using App.Controls;

internal static class RadioHarness
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
    private const int WM_KEYUP = 0x0101;
    private const int WM_LBUTTONDOWN = 0x0201;
    private const int WM_LBUTTONUP = 0x0202;

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

    /// <summary>デザイナーが Designer.cs に書き出すと判定するプロパティの名前（調べるものだけ）。</summary>
    private static string Serialized(SRadioButton radio)
    {
        string[] names = { "Font", "ForeColor", "BackColor", "CircleSize", "CircleBorderColor", "CircleBackColor", "CheckedColor", "DotColor", "FocusBorderColor" };
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(radio);
        var written = new System.Collections.Generic.List<string>();
        foreach (string name in names)
        {
            if (properties[name].ShouldSerializeValue(radio)) written.Add(name);
        }
        return written.Count == 0 ? "none" : string.Join(",", written.ToArray());
    }

    private static void Click(Control control)
    {
        IntPtr at = (IntPtr)(((control.Height / 2) << 16) | (control.Width / 2));
        SendMessage(control.Handle, WM_LBUTTONDOWN, (IntPtr)1, at);
        SendMessage(control.Handle, WM_LBUTTONUP, IntPtr.Zero, at);
        Application.DoEvents();
    }

    private static void Space(Control control)
    {
        SendMessage(control.Handle, WM_KEYDOWN, (IntPtr)(int)Keys.Space, (IntPtr)1);
        SendMessage(control.Handle, WM_KEYUP, (IntPtr)(int)Keys.Space, (IntPtr)1);
        Application.DoEvents();
    }

    /// <summary>選んだときの丸の色（青）に近い画素か。背景（白）と、中の点（白）は、当たらない。</summary>
    private static bool IsCircleColor(Color c)
    {
        return c.B - c.R > 40;
    }

    /// <summary>
    /// コントロールを画像に描かせて、選んだときの丸が占めている範囲を「左,上,幅,高さ」で返す。
    /// 丸の色の画素がある列の範囲と、行の範囲を調べる。
    /// </summary>
    private static string CircleExtent(SRadioButton radio)
    {
        using (var bitmap = new Bitmap(radio.Width, radio.Height))
        {
            radio.DrawToBitmap(bitmap, new Rectangle(0, 0, radio.Width, radio.Height));

            int left = -1, right = -1, top = -1, bottom = -1;
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    if (!IsCircleColor(bitmap.GetPixel(x, y))) continue;
                    if (left < 0 || x < left) left = x;
                    if (x > right) right = x;
                    if (top < 0) top = y;
                    bottom = y;
                }
            }
            if (left < 0) return "none";
            return left + "," + top + "," + (right - left + 1) + "," + (bottom - top + 1);
        }
    }

    /// <summary>丸の中央の画素が、白い点か（選んだときだけ、中に点を描く）。</summary>
    private static bool HasDot(SRadioButton radio, int centerX)
    {
        using (var bitmap = new Bitmap(radio.Width, radio.Height))
        {
            radio.DrawToBitmap(bitmap, new Rectangle(0, 0, radio.Width, radio.Height));
            Color c = bitmap.GetPixel(centerX, radio.Height / 2);
            return c.R > 230 && c.G > 230 && c.B > 230;
        }
    }

    private static void Tests()
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ClientSize = new Size(400, 240);
            form.BackColor = Color.White;
            form.ShowInTaskbar = false;
            // 同じ親に置いたものが、1 つの組になる
            var first = new SRadioButton { Name = "first", Text = "医療機関一覧表", Location = new Point(10, 10), Size = new Size(160, 30) };
            var second = new SRadioButton { Name = "second", Text = "医療機関情報", Location = new Point(10, 50), Size = new Size(160, 30) };
            var panel = new Panel { Location = new Point(200, 10), Size = new Size(180, 100), BackColor = Color.White };
            var alone = new SRadioButton { Name = "alone", Location = new Point(10, 10), Size = new Size(60, 30), CheckAlign = ContentAlignment.MiddleCenter, BackColor = Color.White };
            panel.Controls.Add(alone);
            var other = new TextBox { Name = "other", Location = new Point(10, 120) };
            form.Controls.Add(first);
            form.Controls.Add(second);
            form.Controls.Add(panel);
            form.Controls.Add(other);
            form.Show();
            form.Activate();
            Application.DoEvents();

            float scale;
            using (Graphics g = form.CreateGraphics()) scale = g.DpiX / 96f;
            int size17 = (int)Math.Round(17 * scale);
            int size13 = (int)Math.Round(13 * scale);
            int margin = Math.Max(1, (int)Math.Round(1 * scale));

            // ---- 既定値 ----
            Check("defaults", first.CircleSize + "/" + (first.BackColor == Color.Transparent) + "/" + (first.ForeColor == SControlStyle.TextColor) + "/" + first.Font.Equals(SControlStyle.Font)
                + "/" + first.Checked + "/" + first.AutoCheck, "17/True/True/True/False/True");
            Check("is a RadioButton", first is RadioButton, "True");

            // ---- デザイナーへの書き出し: 既定値のままなら書き出さない。変えたものだけ書き出す ----
            Check("serialized (defaults)", Serialized(first), "none");
            Check("serialized (alone)", Serialized(alone), "BackColor");
            first.CircleSize = 20;
            first.CheckedColor = Color.Red;
            first.ForeColor = Color.Black;
            Check("serialized (changed)", Serialized(first), "ForeColor,CircleSize,CheckedColor");
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(first);
            properties["CircleSize"].ResetValue(first);
            properties["CheckedColor"].ResetValue(first);
            properties["ForeColor"].ResetValue(first);
            Check("serialized (reset)", Serialized(first) + "/" + first.CircleSize, "none/17");
            first.DotColor = Color.Red;
            first.DotColor = Color.Empty;
            first.BackColor = Color.White;
            first.BackColor = Color.Empty;
            Check("empty color -> default", Serialized(first) + "/" + (first.BackColor == Color.Transparent), "none/True");
            first.CircleSize = 1;
            Check("minimum circle size", first.CircleSize, 7);
            first.CircleSize = 17;

            // ---- クリックと Space で選ぶ。組の中で、選ばれるのは 1 つだけ。選んであるものを押しても、外れない ----
            int changed = 0;
            first.CheckedChanged += delegate { changed++; };
            Click(first);
            Check("click", first.Checked + "/" + second.Checked + "/" + changed + "/" + (form.ActiveControl == first), "True/False/1/True");
            Click(first);
            Check("click again (stays selected)", first.Checked + "/" + changed, "True/1");
            Click(second);
            Check("click the other", first.Checked + "/" + second.Checked + "/" + changed, "False/True/2");
            first.Select();
            Application.DoEvents();
            Space(first);
            Check("space", first.Checked + "/" + second.Checked, "True/False");
            // 別の親に置いたものは、別の組
            Click(alone);
            Check("another group", first.Checked + "/" + alone.Checked, "True/True");
            second.AutoCheck = false;
            Click(second);
            Check("AutoCheck off", first.Checked + "/" + second.Checked, "True/False");
            second.AutoCheck = true;
            second.Enabled = false;
            Click(second);
            Check("disabled", first.Checked + "/" + second.Checked, "True/False");
            second.Enabled = true;

            // ---- 描いた丸: 大きさは CircleSize（表示倍率に合わせる）。位置は CheckAlign。選んだときは、中に点を描く ----
            // 文字の縁（にじみ）が丸の色と紛れるので、丸の範囲を調べる間は、文字なしにする
            string caption = first.Text;
            first.Text = "";
            other.Select();
            Application.DoEvents();
            int top = (first.Height - size17) / 2;
            Check("circle drawn (left, 17)", CircleExtent(first) + "/" + HasDot(first, margin + size17 / 2), margin + "," + top + "," + size17 + "," + size17 + "/True");
            first.CircleSize = 13;
            Check("circle drawn (left, 13)", CircleExtent(first), margin + "," + ((first.Height - size13) / 2) + "," + size13 + "," + size13);
            first.CircleSize = 17;
            first.CheckAlign = ContentAlignment.MiddleRight;
            Check("circle drawn (right)", CircleExtent(first), (first.Width - margin - size17) + "," + top + "," + size17 + "," + size17);
            first.CheckAlign = ContentAlignment.MiddleLeft;
            Check("circle drawn (center)", CircleExtent(alone), ((alone.Width - size17) / 2) + "," + ((alone.Height - size17) / 2) + "," + size17 + "," + size17);
            alone.Checked = false;
            Check("circle drawn (not selected)", CircleExtent(alone), "none");

            // ---- 内容がちょうど収まる大きさ: 丸 + すき間 + 文字 ----
            first.Text = caption;
            Size text = TextRenderer.MeasureText(first.Text, first.Font, Size.Empty, TextFormatFlags.SingleLine);
            Size preferred = first.GetPreferredSize(Size.Empty);
            Check("preferred size", preferred.Width + "/" + (preferred.Height >= size17 + margin * 2), (size17 + margin * 2 + (int)Math.Round(5 * scale) + text.Width) + "/True");

            form.Close();
        }
    }
}
