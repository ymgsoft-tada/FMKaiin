// SCheckBox の検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
// フォームにチェック ボックスを置き、既定値・デザイナーへの書き出しの判定・クリックとキーでの切り替え・描いた四角の大きさと位置を確かめる。
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using App;
using ComponentSUI;

internal static class CheckHarness
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
    private static string Serialized(SCheckBox box)
    {
        string[] names = { "Font", "ForeColor", "BackColor", "BoxSize", "CornerRadius", "BoxBorderColor", "BoxBackColor", "CheckedColor", "CheckMarkColor", "FocusBorderColor" };
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(box);
        var written = new System.Collections.Generic.List<string>();
        foreach (string name in names)
        {
            if (properties[name].ShouldSerializeValue(box)) written.Add(name);
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

    /// <summary>オンの四角の色（青）に近い画素か。背景（白）と、チェックの印（白）は、当たらない。</summary>
    private static bool IsBoxColor(Color c)
    {
        return c.B - c.R > 40;
    }

    /// <summary>
    /// コントロールを画像に描かせて、オンの四角が占めている範囲を「左,上,幅,高さ」で返す。
    /// 四角の上下の中央の行と、左右の中央の列で、四角の色の画素が並ぶ範囲を調べる。
    /// </summary>
    private static string BoxExtent(SCheckBox box)
    {
        using (var bitmap = new Bitmap(box.Width, box.Height))
        {
            box.DrawToBitmap(bitmap, new Rectangle(0, 0, box.Width, box.Height));

            int left = -1, right = -1, top = -1, bottom = -1;
            // 四角のどこかを通る行・列を探す（四角の色の画素がいちばん多い行と列）
            int bestRow = 0, bestRowCount = -1, bestColumn = 0, bestColumnCount = -1;
            for (int y = 0; y < bitmap.Height; y++)
            {
                int count = 0;
                for (int x = 0; x < bitmap.Width; x++) if (IsBoxColor(bitmap.GetPixel(x, y))) count++;
                if (count > bestRowCount) { bestRowCount = count; bestRow = y; }
            }
            for (int x = 0; x < bitmap.Width; x++)
            {
                int count = 0;
                for (int y = 0; y < bitmap.Height; y++) if (IsBoxColor(bitmap.GetPixel(x, y))) count++;
                if (count > bestColumnCount) { bestColumnCount = count; bestColumn = x; }
            }
            for (int x = 0; x < bitmap.Width; x++)
            {
                if (IsBoxColor(bitmap.GetPixel(x, bestRow))) { if (left < 0) left = x; right = x; }
            }
            for (int y = 0; y < bitmap.Height; y++)
            {
                if (IsBoxColor(bitmap.GetPixel(bestColumn, y))) { if (top < 0) top = y; bottom = y; }
            }
            if (left < 0 || top < 0) return "none";
            return left + "," + top + "," + (right - left + 1) + "," + (bottom - top + 1);
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
            var box = new SCheckBox { Name = "box", Text = "介護施設", Location = new Point(10, 10), Size = new Size(120, 30) };
            var center = new SCheckBox { Name = "center", Location = new Point(10, 60), Size = new Size(60, 30), CheckAlign = ContentAlignment.MiddleCenter, BackColor = Color.White };
            var other = new TextBox { Name = "other", Location = new Point(200, 10) };
            form.Controls.Add(box);
            form.Controls.Add(center);
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
            Check("defaults", box.BoxSize + "/" + box.CornerRadius + "/" + (box.BackColor == Color.Transparent) + "/" + (box.ForeColor == SControlStyle.TextColor) + "/" + box.Font.Equals(SControlStyle.Font)
                + "/" + box.Checked + "/" + box.AutoCheck, "17/3/True/True/True/False/True");
            Check("is a CheckBox", (box is CheckBox) + "/" + (new SControlDBCheckBox("F", box) != null), "True/True");

            // ---- デザイナーへの書き出し: 既定値のままなら書き出さない。変えたものだけ書き出す ----
            Check("serialized (defaults)", Serialized(box), "none");
            Check("serialized (center)", Serialized(center), "BackColor");
            box.BoxSize = 20;
            box.CheckedColor = Color.Red;
            box.ForeColor = Color.Black;
            Check("serialized (changed)", Serialized(box), "ForeColor,BoxSize,CheckedColor");
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(box);
            properties["BoxSize"].ResetValue(box);
            properties["CheckedColor"].ResetValue(box);
            properties["ForeColor"].ResetValue(box);
            Check("serialized (reset)", Serialized(box) + "/" + box.BoxSize, "none/17");
            // 色を空にすると、既定の色に戻る
            box.CheckedColor = Color.Red;
            box.CheckedColor = Color.Empty;
            box.BackColor = Color.White;
            box.BackColor = Color.Empty;
            Check("empty color -> default", Serialized(box) + "/" + (box.BackColor == Color.Transparent), "none/True");
            box.BoxSize = 1;
            Check("minimum box size", box.BoxSize, 7);
            box.BoxSize = 17;

            // ---- クリックと Space で切り替わる。切り替えられない設定（AutoCheck = false）では、切り替わらない ----
            int changed = 0;
            box.CheckedChanged += delegate { changed++; };
            Click(box);
            Check("click", box.Checked + "/" + changed + "/" + (form.ActiveControl == box), "True/1/True");
            Click(box);
            Check("click again", box.Checked + "/" + changed, "False/2");
            box.Select();
            Space(box);
            Check("space", box.Checked + "/" + changed, "True/3");
            box.AutoCheck = false;
            Click(box);
            Space(box);
            Check("AutoCheck off", box.Checked + "/" + changed, "True/3");
            box.AutoCheck = true;
            box.Enabled = false;
            Click(box);
            Check("disabled", box.Checked + "/" + changed, "True/3");
            box.Enabled = true;

            // ---- 描いた四角: 大きさは BoxSize（表示倍率に合わせる）。位置は CheckAlign ----
            // 文字の縁（にじみ）が四角の色と紛れるので、四角の範囲を調べる間は、文字なしにする
            string caption = box.Text;
            box.Text = "";
            other.Select();
            Application.DoEvents();
            box.Checked = true;
            int top = (box.Height - size17) / 2;
            Check("box drawn (left, 17)", BoxExtent(box), margin + "," + top + "," + size17 + "," + size17);
            box.BoxSize = 13;
            Check("box drawn (left, 13)", BoxExtent(box), margin + "," + ((box.Height - size13) / 2) + "," + size13 + "," + size13);
            box.BoxSize = 17;
            box.CheckAlign = ContentAlignment.MiddleRight;
            Check("box drawn (right)", BoxExtent(box), (box.Width - margin - size17) + "," + top + "," + size17 + "," + size17);
            box.CheckAlign = ContentAlignment.MiddleLeft;

            center.Checked = true;
            Check("box drawn (center)", BoxExtent(center), ((center.Width - size17) / 2) + "," + ((center.Height - size17) / 2) + "," + size17 + "," + size17);
            center.Checked = false;
            Check("box drawn (unchecked)", BoxExtent(center), "none");
            center.ThreeState = true;
            center.CheckState = CheckState.Indeterminate;
            Check("box drawn (indeterminate)", BoxExtent(center), ((center.Width - size17) / 2) + "," + ((center.Height - size17) / 2) + "," + size17 + "," + size17);

            // ---- 内容がちょうど収まる大きさ: 四角 + すき間 + 文字 ----
            box.Text = caption;
            Size text = TextRenderer.MeasureText(box.Text, box.Font, Size.Empty, TextFormatFlags.SingleLine);
            Size preferred = box.GetPreferredSize(Size.Empty);
            Check("preferred size", preferred.Width + "/" + (preferred.Height >= size17 + margin * 2), (size17 + margin * 2 + (int)Math.Round(5 * scale) + text.Width) + "/True");

            form.Close();
        }
    }
}
