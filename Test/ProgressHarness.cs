// SProgressBar（進み具合のバー）と SBusyIndicator（回る輪）の検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
// フォームに部品を置き、値の丸め・塗る幅・回す／止めるの切り替え・デザイナーへの書き出しの判定を確かめる。
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using App.Controls;

internal static class ProgressHarness
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    private static extern bool SetProcessDPIAware();

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
    private static string Serialized(Control control, params string[] names)
    {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(control);
        var written = new System.Collections.Generic.List<string>();
        foreach (string name in names)
        {
            if (properties[name].ShouldSerializeValue(control)) written.Add(name);
        }
        return written.Count == 0 ? "none" : string.Join(",", written.ToArray());
    }

    /// <summary>進んだ分の色（青）に近い画素か。下地（薄い灰色）と背景（白）は、当たらない。</summary>
    private static bool IsBarColor(Color c)
    {
        return c.B - c.R > 60;
    }

    /// <summary>バーを画像に描かせて、上下の中央の行で、進んだ分の色の画素が並ぶ範囲を「左,幅」で返す。</summary>
    private static string BarExtent(SProgressBar bar)
    {
        using (var bitmap = new Bitmap(bar.Width, bar.Height))
        {
            bar.DrawToBitmap(bitmap, new Rectangle(0, 0, bar.Width, bar.Height));
            int left = -1, right = -1;
            int y = bar.Height / 2;
            for (int x = 0; x < bitmap.Width; x++)
            {
                if (IsBarColor(bitmap.GetPixel(x, y))) { if (left < 0) left = x; right = x; }
            }
            return left < 0 ? "none" : left + "," + (right - left + 1);
        }
    }

    /// <summary>回る輪を画像に描かせて、弧の色（青）の画素の位置を文字列にする（弧の位置が変わると、違う文字列になる）。</summary>
    private static string Snapshot(SBusyIndicator busy)
    {
        using (var bitmap = new Bitmap(busy.Width, busy.Height))
        {
            busy.DrawToBitmap(bitmap, new Rectangle(0, 0, busy.Width, busy.Height));
            var text = new System.Text.StringBuilder();
            for (int y = 0; y < bitmap.Height; y += 2)
            {
                for (int x = 0; x < bitmap.Width; x += 2)
                {
                    text.Append(IsBarColor(bitmap.GetPixel(x, y)) ? '1' : '0');
                }
            }
            return text.ToString();
        }
    }

    private static void Wait(int milliseconds)
    {
        DateTime until = DateTime.Now.AddMilliseconds(milliseconds);
        while (DateTime.Now < until)
        {
            Application.DoEvents();
            System.Threading.Thread.Sleep(10);
        }
    }

    private static void Tests()
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ClientSize = new Size(400, 200);
            form.BackColor = Color.White;
            form.ShowInTaskbar = false;
            var bar = new SProgressBar { Name = "bar", Location = new Point(10, 10), Size = new Size(200, 10), BackColor = Color.White };
            var busy = new SBusyIndicator { Name = "busy", Location = new Point(10, 40), Size = new Size(36, 36) };

            // ---- 表示する前: 回る輪は、まだ回さない ----
            Check("busy: not running before it is shown", busy.IsRunning, "False");
            form.Controls.Add(bar);
            form.Controls.Add(busy);
            form.Show();
            form.Activate();
            Application.DoEvents();

            // ---- SProgressBar: 既定値と、値の丸め ----
            Check("bar: defaults", bar.Minimum + "/" + bar.Maximum + "/" + bar.Value + "/" + bar.TabStop, "0/100/0/False");
            bar.Value = 35;
            Check("bar: value", bar.Value + "/" + bar.FilledWidth, "35/70");
            bar.Value = 150;
            Check("bar: above the maximum", bar.Value + "/" + bar.FilledWidth, "100/200");
            bar.Value = -5;
            Check("bar: below the minimum", bar.Value + "/" + bar.FilledWidth, "0/0");
            bar.Maximum = 50;
            bar.Value = 25;
            Check("bar: another range", bar.Value + "/" + bar.FilledWidth, "25/100");
            bar.Maximum = 10;
            Check("bar: maximum below the value", bar.Maximum + "/" + bar.Value, "10/10");
            bar.Minimum = 20;
            Check("bar: minimum above the maximum", bar.Minimum + "/" + bar.Maximum + "/" + bar.Value + "/" + bar.FilledWidth, "20/20/20/0");
            bar.Minimum = 0;
            bar.Maximum = 100;

            // ---- SProgressBar: 塗る範囲。左端から、値の割合の幅まで ----
            bar.Value = 0;
            Check("bar: drawn (0)", BarExtent(bar), "none");
            bar.Value = 50;
            string half = BarExtent(bar);
            // 両端を丸くして、なめらかに描くので、端の 1 画素は、色が薄くなることがある
            Check("bar: drawn (50) starts at the left edge", int.Parse(half.Split(',')[0]) <= 1, "True");
            int halfWidth = int.Parse(half.Split(',')[1]);
            Check("bar: drawn (50) width", halfWidth >= 98 && halfWidth <= 100, "True");
            bar.Value = 100;
            int fullWidth = int.Parse(BarExtent(bar).Split(',')[1]);
            Check("bar: drawn (100) width", fullWidth >= 198 && fullWidth <= 200, "True");

            // ---- SProgressBar: デザイナーへの書き出し ----
            var plain = new SProgressBar();
            Check("bar: serialized (defaults)", Serialized(plain, "BackColor", "TrackColor", "BarColor", "Minimum", "Maximum", "Value"), "none");
            plain.BarColor = Color.Red;
            plain.Value = 10;
            Check("bar: serialized (changed)", Serialized(plain, "BackColor", "TrackColor", "BarColor", "Minimum", "Maximum", "Value"), "BarColor,Value");
            plain.BarColor = Color.Empty;
            Check("bar: empty color -> default", Serialized(plain, "BarColor"), "none");

            // ---- SBusyIndicator: 表示している間だけ、回る ----
            Check("busy: running while shown", busy.IsRunning + "/" + busy.Active + "/" + busy.TabStop, "True/True/False");
            busy.Active = false;
            Check("busy: stopped by Active", busy.IsRunning, "False");
            busy.Active = true;
            Check("busy: running again", busy.IsRunning, "True");
            busy.Visible = false;
            Check("busy: stopped when hidden", busy.IsRunning, "False");
            busy.Visible = true;
            Check("busy: running when shown again", busy.IsRunning, "True");

            // 回っている間は、弧の位置が変わっていく。止めている間は、変わらない
            // （フォームを画面の外に置いているので、描き直しの回数ではなく、描かせた絵を比べる）
            string first = Snapshot(busy);
            Wait(130);
            Check("busy: the arc moves while running", Snapshot(busy) != first, "True");
            busy.Active = false;
            string stopped = Snapshot(busy);
            Wait(300);
            Check("busy: the arc stays while stopped", Snapshot(busy) == stopped, "True");
            busy.Active = true;

            // ---- SBusyIndicator: デザイナーへの書き出し ----
            var plainBusy = new SBusyIndicator();
            Check("busy: serialized (defaults)", Serialized(plainBusy, "BackColor", "TrackColor", "IndicatorColor", "Active", "LineWeight"), "none");
            plainBusy.IndicatorColor = Color.Red;
            plainBusy.Active = false;
            Check("busy: serialized (changed)", Serialized(plainBusy, "BackColor", "TrackColor", "IndicatorColor", "Active", "LineWeight"), "IndicatorColor,Active");

            // ---- フォームを閉じると、止まる ----
            form.Close();
            Check("busy: stopped when the form is closed", busy.IsRunning, "False");
        }
    }
}
