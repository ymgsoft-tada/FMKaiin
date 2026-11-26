// SToolTip の検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
// フォームにテキスト ボックスを置いてツールチップを表示し、表示・非表示のきっかけと、表示する位置を確かめる。
// 位置を確かめるので、フォームは画面の中に表示する（実行中、小さなウィンドウが一時的に見える）。音は鳴らさない。
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using App.Controls;

internal static class ToolTipHarness
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

    private static void Wait(int milliseconds)
    {
        DateTime until = DateTime.Now.AddMilliseconds(milliseconds);
        while (DateTime.Now < until)
        {
            Application.DoEvents();
            Thread.Sleep(10);
        }
    }

    /// <summary>表示しているツールチップのウィンドウ（フォームが所有するウィンドウ）。無ければ null。</summary>
    private static Form Popup(Form form)
    {
        return form.OwnedForms.Length == 0 ? null : form.OwnedForms[0];
    }

    private static void Tests()
    {
        var form = new Form();
        form.StartPosition = FormStartPosition.Manual;
        form.Location = new Point(200, 200);
        form.ClientSize = new Size(400, 160);
        form.ShowInTaskbar = false;
        var name = new STextBox { Location = new Point(20, 20), Width = 300 };
        var other = new STextBox { Location = new Point(20, 60), Width = 300 };
        form.Controls.Add(name);
        form.Controls.Add(other);
        form.Show();
        form.Activate();
        name.Focus();
        Application.DoEvents();

        var tip = new SToolTip(form);
        Check("initial", tip.Active + "/[" + tip.Caption + "]/[" + tip.Text + "]", "False/[]/[]");

        // ---- 入力欄の下に表示する。フォーカスは動かない ----
        tip.Show(name, "入力必須項目", "空欄にする事はできません。", ToolTipIcon.Warning);
        Application.DoEvents();
        Form popup = Popup(form);
        Check("shown", tip.Active + "/" + (popup != null && popup.Visible) + "/[" + tip.Caption + "]/[" + tip.Text + "]", "True/True/[入力必須項目]/[空欄にする事はできません。]");
        // 入力欄の位置は、枠を含めた全体で比べる（文字を入力する部分は、枠の内側にある）
        Rectangle nameBounds = form.RectangleToScreen(name.Bounds);
        Check("placed below the control, from its center", (popup.Left == nameBounds.Left + nameBounds.Width / 2) + "/" + (popup.Top >= nameBounds.Bottom && popup.Top <= nameBounds.Bottom + 8), "True/True");
        Check("focus stays in the control", name.Focused + "/" + (Form.ActiveForm == form), "True/True");

        // ---- 別のツールチップを表示すると、前のものは消える ----
        tip.Show(other, "", "補足の説明", ToolTipIcon.None);
        Application.DoEvents();
        Check("replaced by the next one", form.OwnedForms.Length + "/[" + tip.Caption + "]/[" + tip.Text + "]", "1/[]/[補足の説明]");

        // ---- Hide ----
        tip.Hide();
        Application.DoEvents();
        Check("hidden", tip.Active + "/" + form.OwnedForms.Length + "/[" + tip.Text + "]", "False/0/[]");

        // ---- 位置を指定する表示 ----
        tip.Show(500, 300, "t", "x", ToolTipIcon.Info);
        Application.DoEvents();
        Check("shown at the screen position", Popup(form).Location, new Point(500, 300));
        tip.Show(name, 10, 5, "t", "x", ToolTipIcon.Error);
        Application.DoEvents();
        Check("shown at the offset from the control", Popup(form).Location, new Point(nameBounds.Left + 10, nameBounds.Top + 5));

        // ---- 表示時間が過ぎると消える ----
        tip.AutoPopDelay = 200;
        tip.Show(name, "t", "x", ToolTipIcon.Warning);
        Application.DoEvents();
        Check("shown before the delay", tip.Active, true);
        Wait(600);
        Check("hidden after the delay", tip.Active + "/" + form.OwnedForms.Length, "False/0");
        tip.AutoPopDelay = 5000;

        // ---- 表示する文字が無ければ、表示しない ----
        tip.Show(name, "", "", ToolTipIcon.Warning);
        Check("nothing to show", tip.Active, false);

        // ---- フォームが動くと消える ----
        tip.Show(name, "t", "x", ToolTipIcon.Warning);
        Application.DoEvents();
        form.Left += 10;
        Application.DoEvents();
        Check("hidden when the form moves", tip.Active, false);

        // ---- 指している入力欄が破棄されると消える ----
        tip.Show(other, "t", "x", ToolTipIcon.Warning);
        Application.DoEvents();
        other.Dispose();
        Application.DoEvents();
        Check("hidden when the control is disposed", tip.Active, false);

        // ---- フォームを閉じると消え、その後は表示しない ----
        tip.Show(name, "t", "x", ToolTipIcon.Warning);
        Application.DoEvents();
        form.Close();
        Application.DoEvents();
        Check("hidden when the form is closed", tip.Active, false);
        tip.Show(300, 300, "t", "x", ToolTipIcon.Warning);
        Check("not shown after the form is closed", tip.Active, false);
    }
}
