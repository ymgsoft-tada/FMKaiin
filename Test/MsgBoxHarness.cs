// SMsgBox の検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
// メッセージ ボックスを、モーダルではなく、ふつうのウィンドウとして表示して、ボタン・キー操作と結果を確かめる。
// 音は鳴らさない。実行中、小さなウィンドウが一時的に見える。
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using App.Controls;

internal static class MsgBoxHarness
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    private static extern bool SetProcessDPIAware();

    private const int WM_KEYDOWN = 0x0100;
    private const int WM_CHAR = 0x0102;

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

    private static SMsgBox Open(string text, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton def)
    {
        var form = new SMsgBox(null, text, "確認", buttons, icon, def);
        form.PlaySound = false;
        form.StartPosition = FormStartPosition.Manual;
        form.Location = new Point(200, 200);
        form.Show();
        form.Activate();
        Application.DoEvents();
        return form;
    }

    /// <summary>ボタンの文字を、左から順に並べたもの。</summary>
    private static string Buttons(SMsgBox form)
    {
        var buttons = new List<Control>();
        foreach (Control c in form.Controls) buttons.Add(c);
        buttons.Sort(delegate(Control a, Control b) { return a.Left.CompareTo(b.Left); });
        var texts = new List<string>();
        foreach (Control c in buttons) texts.Add(c.Text);
        return string.Join(" ", texts.ToArray());
    }

    private static Button Find(SMsgBox form, string text)
    {
        foreach (Control c in form.Controls)
        {
            if (c.Text == text) return (Button)c;
        }
        return null;
    }

    /// <summary>キーを、実際のキー入力と同じ経路（メッセージの待ち行列）で送る。</summary>
    private static void Press(Control target, int message, int key)
    {
        PostMessage(target.Handle, message, (IntPtr)key, (IntPtr)1);
        Application.DoEvents();
        Application.DoEvents();
    }

    private static string CloseByEscape(MessageBoxButtons buttons)
    {
        using (SMsgBox form = Open("x", buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1))
        {
            Press(form.ActiveControl, WM_KEYDOWN, (int)Keys.Escape);
            return form.Visible + "/" + form.Result + "/" + form.CloseReason;
        }
    }

    private static void Tests()
    {
        // ---- 文字列の解釈: 改行、色（後ろの行へ引き継ぐ）、太字（その行だけ）----
        List<SMsgBox.Line> lines = SMsgBox.ParseLines("@c(Red)@b一行目\n二行目\r\n@c(Nazo)三行目\n@x四行目", Color.Black);
        Check("lines", lines.Count, 4);
        Check("line 1", lines[0].Text + "/" + lines[0].Color.Name + "/" + lines[0].Bold, "一行目/Red/True");
        Check("line 2 keeps the color, not the bold", lines[1].Text + "/" + lines[1].Color.Name + "/" + lines[1].Bold, "二行目/Red/False");
        Check("unknown color name is ignored", lines[2].Text + "/" + lines[2].Color.Name, "三行目/Red");
        Check("unknown mark: only @ is removed", lines[3].Text, "x四行目");
        Check("no marks", SMsgBox.ParseLines("そのまま", Color.Black)[0].Text + "/" + SMsgBox.ParseLines(null, Color.Black).Count, "そのまま/1");

        // ---- ボタンの並びと、最初にフォーカスを置くボタン ----
        using (SMsgBox form = Open("よろしいですか？", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
        {
            Check("YesNo buttons / default", Buttons(form) + " / " + form.ActiveControl.Text, "はい(&Y) いいえ(&N) / いいえ(&N)");

            // ボタンを押す
            Find(form, "はい(&Y)").PerformClick();
            Application.DoEvents();
            Check("click: closed / result / reason", form.Visible + "/" + form.Result + "/" + form.CloseReason, "False/Yes/Default");
        }
        using (SMsgBox form = Open("x", MessageBoxButtons.YesNoCancel, MessageBoxIcon.None, MessageBoxDefaultButton.Button3))
        {
            Check("YesNoCancel buttons / default", Buttons(form) + " / " + form.ActiveControl.Text, "はい(&Y) いいえ(&N) キャンセル / キャンセル");
        }
        using (SMsgBox form = Open("x", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
        {
            Check("OK only: default falls back to the first", Buttons(form) + " / " + form.ActiveControl.Text, "OK / OK");

            // Enter は、フォーカスがあるボタンを押す
            Press(form.ActiveControl, WM_KEYDOWN, (int)Keys.Enter);
            Check("Enter: closed / result", form.Visible + "/" + form.Result, "False/OK");
        }

        // ---- 「はい(Y)」などは、その文字のキーだけで押せる ----
        using (SMsgBox form = Open("x", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2))
        {
            Press(form.ActiveControl, WM_CHAR, 'y');
            Check("Y key: closed / result", form.Visible + "/" + form.Result, "False/Yes");
        }

        // ---- Esc（右上の × と同じ）で閉じたときの結果 ----
        Check("Esc on YesNo", CloseByEscape(MessageBoxButtons.YesNo), "False/No/WindowCancel");
        Check("Esc on OKCancel", CloseByEscape(MessageBoxButtons.OKCancel), "False/Cancel/WindowCancel");
        Check("Esc on YesNoCancel", CloseByEscape(MessageBoxButtons.YesNoCancel), "False/Cancel/WindowCancel");
        Check("Esc on AbortRetryIgnore", CloseByEscape(MessageBoxButtons.AbortRetryIgnore), "False/Abort/WindowCancel");
        Check("Esc on OK", CloseByEscape(MessageBoxButtons.OK), "False/None/WindowCancel");

        // ---- 大きさ: 短い文でも一定の幅を保ち、長い文は折り返して縦に伸びる ----
        float scale;
        using (Graphics g = Graphics.FromHwnd(IntPtr.Zero)) scale = g.DpiX / 96f;
        string longText = new string('長', 200);
        using (SMsgBox small = Open("短い", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1))
        using (SMsgBox large = Open(longText, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1))
        {
            Check("minimum width", small.ClientSize.Width >= (int)(360 * scale) - 1, true);
            Check("long text wraps", (large.ClientSize.Width < Screen.PrimaryScreen.WorkingArea.Width) + "/" + (large.ClientSize.Height > small.ClientSize.Height), "True/True");
        }
    }
}
