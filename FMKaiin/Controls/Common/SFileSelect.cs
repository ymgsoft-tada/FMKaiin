using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// <see cref="SFileSelect"/> の右端のボタンで開くダイアログの種類。
    /// </summary>
    public enum SFileDialogType
    {
        /// <summary>フォルダーを選ぶ。</summary>
        FolderSelect,
        /// <summary>開くファイルを選ぶ。</summary>
        FileOpen,
        /// <summary>保存するファイルを決める。</summary>
        FileSave,
    }

    /// <summary>
    /// ファイル・フォルダーのパスを入れる入力欄。UcFileSelect（テキスト ボックスと「...」のボタンを並べたもの）の代わり。
    /// 右端のボタン（「…」）を押すと、ファイル・フォルダーを選ぶダイアログが開き、選んだパスが入る。
    /// エクスプローラーからファイルをドラッグして落としても、そのパスが入る。パスは <see cref="Control.Text"/> で読み書きする。
    /// </summary>
    /// <remarks>
    /// 見た目・フォーカスの示し方・プレースホルダーなどは、ほかの S 系の入力欄と同じ（<see cref="SEditBase"/>）。
    /// 入力できる文字は制限しない（パスは、手で入力・貼り付けもできる）。フォーカスを受け取ると、全体を選択する。
    /// </remarks>
    public class SFileSelect : SEditBase
    {
        /// <summary>ボタンに描く点（「…」）の直径と、点の間隔。96 DPI 基準のピクセル。</summary>
        private const float DotSize = 2.6f;
        private const float DotGap = 5f;

        private const string DefaultFileFilter = "すべてのファイル(*.*)|*.*";

        private SFileDialogType _fileDialogType = SFileDialogType.FileOpen;
        private string _fileFilter = DefaultFileFilter;

        public SFileSelect()
        {
            base.HighlightText = true;
            AllowDrop = true;
        }

        #region プロパティ

        /// <summary>右端のボタンで開くダイアログの種類。既定は、開くファイルを選ぶ。</summary>
        [Category("Behavior")]
        [DefaultValue(SFileDialogType.FileOpen)]
        [Description("右端のボタンで開くダイアログの種類。")]
        public SFileDialogType FileDialogType
        {
            get { return _fileDialogType; }
            set
            {
                if (!Enum.IsDefined(typeof(SFileDialogType), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(SFileDialogType));
                }
                _fileDialogType = value;
            }
        }

        /// <summary>
        /// ダイアログで選べるファイルの種類（<see cref="FileDialog.Filter"/> と同じ書き方。例: "CSVファイル|*.csv"）。
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(DefaultFileFilter)]
        [Description("ダイアログで選べるファイルの種類（例: CSVファイル|*.csv）。")]
        public string FileFilter
        {
            get { return _fileFilter; }
            set { _fileFilter = string.IsNullOrEmpty(value) ? DefaultFileFilter : value; }
        }

        /// <summary>ダイアログを開いたときに、何番目の種類を選んでおくか（<see cref="FileDialog.FilterIndex"/> と同じ）。</summary>
        [Category("Behavior")]
        [DefaultValue(0)]
        [Description("ダイアログを開いたときに、何番目の種類を選んでおくか。")]
        public int FileFilterIndex { get; set; }

        /// <summary>
        /// ダイアログで選んだパスを、入力欄へ入れてよいかを決める関数。false を返すと、入れない。指定しなければ、いつも入れる。
        /// UcFileSelect.CheckFilePathMethod に当たる。手で入力したパスと、ドラッグして落としたパスには、使わない（元の部品と同じ）。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<string, bool> CheckFilePath { get; set; }

        #endregion

        #region ダイアログ

        /// <summary>
        /// ファイル・フォルダーを選ぶダイアログを開く。選んだパスを入力欄へ入れる。取り消した場合は、何もしない。
        /// </summary>
        public void ShowDialog()
        {
            if (ReadOnly || !Enabled)
            {
                return;
            }

            string path;
            switch (_fileDialogType)
            {
                case SFileDialogType.FolderSelect:
                    path = SelectFolder();
                    break;
                case SFileDialogType.FileSave:
                    path = SelectFile(new SaveFileDialog(), "保存するファイルを選択してください", Text.Length == 0 ? string.Empty : Path.GetFileName(Text));
                    break;
                default:
                    path = SelectFile(new OpenFileDialog(), "開くファイルを選択してください", Text);
                    break;
            }

            if (path != null && (CheckFilePath == null || CheckFilePath(path)))
            {
                Text = path;
            }
        }

        private string SelectFolder()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "フォルダを指定してください。";
                dialog.RootFolder = Environment.SpecialFolder.Desktop;
                dialog.SelectedPath = Text.Length == 0 ? @"C:\" : Text;
                dialog.ShowNewFolderButton = false;
                return dialog.ShowDialog(FindForm()) == DialogResult.OK ? dialog.SelectedPath : null;
            }
        }

        /// <summary>ファイルを選ぶダイアログ（開く・保存）を開く。設定は、UcFileSelect と同じ。</summary>
        private string SelectFile(FileDialog dialog, string title, string fileName)
        {
            using (dialog)
            {
                dialog.FileName = fileName;
                dialog.InitialDirectory = Text.Length == 0 ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : Text;
                dialog.Filter = _fileFilter;
                dialog.FilterIndex = FileFilterIndex;
                dialog.Title = title;
                dialog.RestoreDirectory = true;
                // 存在しないファイル・パスが指定されても、警告を出さない（存在の確認は、使う側で行う）
                dialog.CheckFileExists = false;
                dialog.CheckPathExists = false;
                return dialog.ShowDialog(FindForm()) == DialogResult.OK ? dialog.FileName : null;
            }
        }

        #endregion

        #region ファイルのドラッグ

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);
            // ファイル（フォルダー）のときだけ受け付ける
            bool acceptable = !ReadOnly && drgevent.Data.GetDataPresent(DataFormats.FileDrop);
            drgevent.Effect = acceptable ? DragDropEffects.Copy : DragDropEffects.None;
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);
            if (ReadOnly)
            {
                return;
            }

            var paths = drgevent.Data.GetData(DataFormats.FileDrop, false) as string[];
            if (paths != null && paths.Length > 0)
            {
                // 複数を落とした場合は、先頭の 1 つを使う
                Text = paths[0];
            }
        }

        #endregion

        #region 右端のボタン（サイド ボタン）

        protected override int GetSideButtonWidth(float scale)
        {
            return SControlStyle.GetDropDownButtonWidth(scale);
        }

        protected override void PaintSideButton(Graphics g, Rectangle bounds, Color backColor)
        {
            // ダイアログを開けないときは、点を薄くする
            Color color = Enabled && !ReadOnly ? ForeColor : SControlStyle.GetInactiveColor(ForeColor, backColor);

            float scale = LayoutScale.Of(this);
            float size = DotSize * scale;
            float gap = DotGap * scale;
            float centerX = bounds.Left + bounds.Width / 2f;
            float centerY = bounds.Top + bounds.Height / 2f;

            SmoothingMode previous = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            try
            {
                using (var brush = new SolidBrush(color))
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        g.FillEllipse(brush, centerX + gap * i - size / 2f, centerY - size / 2f, size, size);
                    }
                }
            }
            finally
            {
                g.SmoothingMode = previous;
            }
        }

        protected override void OnSideButtonClick(EventArgs e)
        {
            ShowDialog();
        }

        #endregion
    }
}
