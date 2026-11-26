using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using App.Controls;
using C1.Win.C1TrueDBGrid;
using ComponentDB;
using ComponentDebug;
using ComponentIO;

namespace App
{
    /// <summary>
    /// ログ表示画面（処理の結果を一覧で見せる）。<see cref="FormLogView"/> と同じ内容を、S 系の部品（Controls/Common）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。一覧は <see cref="SGridDB"/>、ボタンは <see cref="SButton"/> を使う。
    /// 元の画面と同じく、ファンクション キーのバーは無く、下のボタンで操作する。
    /// </remarks>
    public partial class FormLogView2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        SGridDB sgrid;

        public enum eLogType
        {
            /// <summary>実行結果</summary>
            Result,
            /// <summary>検証結果</summary>
            TestResult,
        }

        /// <summary>
        /// ログの種類
        /// </summary>
        public eLogType LogType { get; set; }

        /// <summary>
        /// ログ用のテーブル
        /// </summary>
        public DataTable Table { get; set; }

        /// <summary>
        /// タイトルラベル
        /// </summary>
        public string TitleText
        {
            get { return lblHeading.Text; }
            set { lblHeading.Text = value; }
        }

        /// <summary>
        /// 処理月度
        /// </summary>
        public DateTime SelectedYM { get; set; }

        DBView dv;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormLogView2()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);
        }

        /// <summary>
        /// 初回描画処理
        /// </summary>
        protected override void FormFrame_Shown(object sender, EventArgs e)
        {
            dv = new DBView(Table);

            sgrid = new SGridDB(grid, this);
            // 見るだけの一覧なので、指している行を選択の色で塗らない（元の画面と同じ）。状況ごとの文字の色を、すべての行に付ける
            grid.MarqueeStyle = MarqueeEnum.NoMarquee;

            sgrid.Add(new SGridDBNumber(LogTable.F_No, "No", 50));
            sgrid.Add(new SGridDBText(LogTable.F_Type, "状況", 50, SGridDBCellDisp.Center) { UnboundFetch = ubType });
            // 内容は、残りの幅をすべて使う（元の画面は幅 500 で、横スクロール バーを出していた）
            sgrid.Add(new SGridDBText(LogTable.F_Message, "内容", 0.0f));
            sgrid.RowStyle = rowStyle;
            sgrid.EndAdd(dv);

            btnClose.Click += btnOK_Click;
            btnExec.Click += btnExec_Click;
            btnCancel.Click += btnOK_Click;
            btnLog.Click += btnLog_Click;

            if (LogType == eLogType.Result)
            {
                btnExec.Visible = false;
                btnCancel.Visible = false;
                btnClose.Select();
            }
            else
            if (LogType == eLogType.TestResult)
            {
                btnClose.Visible = false;
                btnExec.Select();
            }

            base.FormFrame_Shown(sender, e);
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            if (AppMsgBox.Show(this, AppMsgBoxIndex.ExecImportCSV) == DialogResult.Yes)
            {
                formCloseReason = FormCloseReason.Exec;
                this.Close();
            }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();

            // はじめに表示されるフォルダ
            string inipath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fd.InitialDirectory = inipath;
            fd.FileName = $"FBログ_{AppDate.GetYearMonth(SelectedYM)}分";

            // [ファイルの種類]に表示される選択肢を指定する
            fd.Filter = string.Format("csvファイル|*.csv");
            fd.FilterIndex = 0;
            fd.Title = "ログファイルの保存先を指定してください。";
            fd.RestoreDirectory = true;

            // ダイアログを表示
            if (fd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    grid.ExportToDelimitedFile(fd.FileName, RowSelectorEnum.AllRows, ",", "", "", true, "shift_jis");
                    AppMsgBox.Show(this, AppMsgBoxIndex.SuccessExportLog);
                }
                catch (Exception ex)
                {
                    ErrLog.WriteException(ex);
                    AppMsgBox.Show(this, AppMsgBoxIndex.FailedExportLogReasonSave);
                }
            }
        }

        /// <summary>
        /// 閉じる時の処理
        /// </summary>
        protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (LogType == eLogType.TestResult && formCloseReason != FormCloseReason.Exec)
            {
                if (AppMsgBox.Show(this, AppMsgBoxIndex.CancelImportCSV) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

            base.FormFrame_FormClosing(sender, e);
        }

        /// <summary>
        /// 状況の列に表示する記号（〇・△・×）
        /// </summary>
        string ubType(SGridDBColumn col, int row)
        {
            LogTable.eStatus sts = Cast.Enumelate<LogTable.eStatus>(dv[row][LogTable.F_Type], LogTable.eStatus.None);
            string ret = "";

            switch (sts)
            {
                case LogTable.eStatus.Success: ret = "〇"; break;
                case LogTable.eStatus.Warning: ret = "△"; break;
                case LogTable.eStatus.Error: ret = "×"; break;
            }

            return ret;
        }

        /// <summary>
        /// 行の文字の色（成功は青、エラーは赤。ほかは、グリッドの文字の色のまま）
        /// </summary>
        void rowStyle(int row, Style style)
        {
            LogTable.eStatus sts = Cast.Enumelate<LogTable.eStatus>(dv[row][LogTable.F_Type], LogTable.eStatus.None);

            switch (sts)
            {
                case LogTable.eStatus.Success: style.ForeColor = Color.Blue; break;
                case LogTable.eStatus.Error: style.ForeColor = Color.Red; break;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            formCloseReason = FormCloseReason.Save;
            this.Close();
        }
    }
}
