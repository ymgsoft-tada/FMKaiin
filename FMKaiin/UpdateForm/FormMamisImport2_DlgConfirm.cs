using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using App.Controls;
using C1.Win.C1TrueDBGrid;
using ComponentDB;
using ComponentIO;

namespace App
{
    /// <summary>
    /// MAMISデータ取込（取込確認）の画面。<see cref="FormMamisImport_DlgConfirm"/> と同じ内容を、S 系の部品（Controls/Common）で作ったもの。
    /// 上段に会員ごとの照合結果、下段に選択中の会員の取込内容を表示します。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。一覧は <see cref="SGridDB"/> を使う。
    /// </remarks>
    public partial class FormMamisImport2_DlgConfirm : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// 読込済みの取込内容
        /// </summary>
        public AppMamisImport Import { get; set; }

        SGridDB sgridHead;
        SGridDB sgridDetail;
        DBView dvHead;
        DBView dvDetail;
        DataTable tblDetail;

        const string F_No = "F_No";
        const string F_Status = "F_Status";
        const string F_StatusType = "F_StatusType";
        const string F_Code = "F_Code";
        const string F_Name = "F_Name";
        const string F_Iseki = "F_Iseki";
        const string F_Shinsei = "F_Shinsei";
        const string F_UpdateDate = "F_UpdateDate";
        const string F_Count = "F_Count";
        const string F_Content = "F_Content";
        const string F_Item = "F_Item";
        const string F_Type = "F_Type";
        const string F_Before = "F_Before";
        const string F_After = "F_After";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMamisImport2_DlgConfirm()
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
            // 上段：会員ごとの照合結果
            DataTable tbl = new DataTable();
            tbl.Columns.Add(F_No, typeof(int));
            tbl.Columns.Add(F_Status, typeof(string));
            tbl.Columns.Add(F_StatusType, typeof(int));
            tbl.Columns.Add(F_Code, typeof(string));
            tbl.Columns.Add(F_Name, typeof(string));
            tbl.Columns.Add(F_Iseki, typeof(string));
            tbl.Columns.Add(F_Shinsei, typeof(string));
            tbl.Columns.Add(F_UpdateDate, typeof(string));
            tbl.Columns.Add(F_Count, typeof(string));
            tbl.Columns.Add(F_Content, typeof(string));

            foreach (AppMamisImport.Person p in Import.Persons)
            {
                DataRow nrow = tbl.NewRow();
                nrow[F_No] = p.No;
                nrow[F_Status] = p.StatusText;
                nrow[F_StatusType] = (int)p.Status;
                nrow[F_Code] = (p.KaiinCD > 0) ? p.KaiinCD.ToString().PadLeft(6, '0') : "";
                nrow[F_Name] = p.Name;
                nrow[F_Iseki] = p.IsekiNo;
                nrow[F_Shinsei] = p.Shinsei;
                nrow[F_UpdateDate] = (p.UpdateDate.Length > 16) ? p.UpdateDate.Substring(0, 16) : p.UpdateDate; // 秒は表示しない
                nrow[F_Count] = (p.KaiinID > 0) ? p.Items.Count.ToString() : "";
                nrow[F_Content] = p.ContentText;
                tbl.Rows.Add(nrow);
            }

            dvHead = new DBView(tbl);

            // 横スクロールしないよう、固定幅の合計をグリッド幅に収め、最後の列（幅0）を残り幅に広げる
            sgridHead = new SGridDB(gridHead, this);
            sgridHead.Add(new SGridDBNumber(F_No, "No", 50));
            sgridHead.Add(new SGridDBText(F_Status, "状況", 165, SGridDBCellDisp.Center));
            sgridHead.Add(new SGridDBText(F_Code, "会員コード", 80, SGridDBCellDisp.Right));
            sgridHead.Add(new SGridDBText(F_Name, "氏名", 130));
            sgridHead.Add(new SGridDBText(F_Iseki, "医籍登録番号", 110, SGridDBCellDisp.Right));
            sgridHead.Add(new SGridDBText(F_Shinsei, "申請", 110, SGridDBCellDisp.Center));
            sgridHead.Add(new SGridDBText(F_UpdateDate, "MAMIS更新日時", 150, SGridDBCellDisp.Center));
            sgridHead.Add(new SGridDBText(F_Count, "項目数", 55, SGridDBCellDisp.Right));
            sgridHead.Add(new SGridDBText(F_Content, "内容", 0));
            sgridHead.RowStyle = rowStyleHead;
            sgridHead.EndAdd(dvHead);

            // 下段：選択中の会員の取込内容
            tblDetail = new DataTable();
            tblDetail.Columns.Add(F_No, typeof(int));
            tblDetail.Columns.Add(F_Item, typeof(string));
            tblDetail.Columns.Add(F_Type, typeof(string));
            tblDetail.Columns.Add(F_Before, typeof(string));
            tblDetail.Columns.Add(F_After, typeof(string));

            dvDetail = new DBView(tblDetail);

            sgridDetail = new SGridDB(gridDetail, this);
            // 下段は見るだけなので、指している行を選択の色で塗らない（元の画面と同じ）
            gridDetail.MarqueeStyle = MarqueeEnum.NoMarquee;
            sgridDetail.Add(new SGridDBNumber(F_No, "No", 50));
            sgridDetail.Add(new SGridDBText(F_Item, "項目", 170));
            sgridDetail.Add(new SGridDBText(F_Type, "区分", 60, SGridDBCellDisp.Center));
            sgridDetail.Add(new SGridDBText(F_Before, "現在の登録内容", 370));
            sgridDetail.Add(new SGridDBText(F_After, "MAMISの内容（取込後）", 0));
            sgridDetail.EndAdd(dvDetail);

            // 件数
            int cnt_ok = Import.TargetCount;
            int cnt_same = Import.Persons.Count(x => x.Status == AppMamisImport.eStatus.Same);
            int cnt_ng = Import.Persons.Count - cnt_ok - cnt_same;

            lblSummary.Text = $"CSVの会員数：{Import.Persons.Count}名　／　取込できる：{cnt_ok}名　／　変更なし：{cnt_same}名　／　取込できない：{cnt_ng}名";

            // 取込できる会員がいない場合は実行できない
            if (cnt_ok == 0)
            {
                funcKeys.SetEnabled(FuncMamisImport_DlgConfirm.Exec.Key, false);
            }

            gridHead.RowColChange += GridHead_RowColChange;

            showDetail();

            gridHead.Select();

            base.FormFrame_Shown(sender, e);
        }

        private void GridHead_RowColChange(object sender, RowColChangeEventArgs e)
        {
            showDetail();
        }

        /// <summary>
        /// 選択中の会員の取込内容を下段に表示します。
        /// </summary>
        void showDetail()
        {
            tblDetail.Rows.Clear();
            lblDetail.Text = "選択中の会員の取込内容";

            int idx = gridHead.Row;

            if (idx < 0 || idx >= dvHead.Count)
            {
                return;
            }

            int no = Cast.Int(dvHead[idx][F_No]);
            AppMamisImport.Person p = Import.Persons.FirstOrDefault(x => x.No == no);

            if (p == null)
            {
                return;
            }

            lblDetail.Text = $"選択中の会員の取込内容：　{Cast.String(dvHead[idx][F_Code])}　{p.Name}";

            int i = 0;
            foreach (AppMamisImport.Item item in p.Items)
            {
                DataRow nrow = tblDetail.NewRow();
                nrow[F_No] = ++i;
                nrow[F_Item] = item.Name;
                nrow[F_Type] = enumKbn.DMamisLogType[(int)item.Type];
                nrow[F_Before] = item.Before;
                nrow[F_After] = item.After;
                tblDetail.Rows.Add(nrow);
            }
        }

        /// <summary>
        /// 状況ごとの文字色（変更なしは灰色、取込できないものは赤。ほかは、グリッドの文字の色のまま）
        /// </summary>
        void rowStyleHead(int row, Style style)
        {
            AppMamisImport.eStatus sts = (AppMamisImport.eStatus)Cast.Int(dvHead[row][F_StatusType]);

            switch (sts)
            {
                case AppMamisImport.eStatus.Same: style.ForeColor = Color.Gray; break;
                case AppMamisImport.eStatus.NotFound:
                case AppMamisImport.eStatus.Mismatch: style.ForeColor = Color.Red; break;
            }
        }

        /// <summary>
        /// ファンクションの登録
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncMamisImport_DlgConfirm.Functions);

            funcKeys.On(FuncMamisImport_DlgConfirm.Exec, doExec);
            funcKeys.On(FuncMamisImport_DlgConfirm.Cancel, formClose);
        }

        /// <summary>
        /// 実行
        /// </summary>
        void doExec()
        {
            if (Import.TargetCount == 0)
            {
                return;
            }

            if (AppMsgBox.Show(this, AppMsgBoxIndex.ExecMamisImport, Import.TargetCount) == DialogResult.Yes)
            {
                formCloseReason = FormCloseReason.Exec;
                this.Close();
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        void formClose()
        {
            this.Close();
        }
    }
}
