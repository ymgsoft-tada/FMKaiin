using System;
using System.Collections.Generic;
using System.Data;
using App.Controls;
using ComponentDB;

namespace App
{
    /// <summary>
    /// 在籍期間の表示の画面。<see cref="FormKaiin_DlgHistory"/> と同じ内容を、S 系の部品（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。会員名などは、無効にした <see cref="STextBox"/>（表示するだけ）、
    /// 期間の一覧は <see cref="SGridDB"/> を使う。
    /// </remarks>
    public partial class FormKaiin2_DlgHistory : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// 会員情報
        /// </summary>
        public Kaiin SelectKaiin { get; set; }

        SGridDB sgrid;
        DBView dvGrid;

        const string F_Code = "F_Code";
        const string F_Range = "F_Range";
        const string F_From = "F_From";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormKaiin2_DlgHistory()
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
            // グリッド表示用テーブル
            DataTable tbl = new DataTable();
            tbl.Columns.Add(F_Code, typeof(int));
            tbl.Columns.Add(F_Range, typeof(string));
            tbl.Columns.Add(F_From, typeof(DateTime));

            // データの生成
            List<Kaiin> kas = SelectKaiin.GetHistoryList();
            kas.Add(SelectKaiin); // 最新も入れておく

            foreach (Kaiin ka in kas)
            {
                DataRow nrow = tbl.NewRow();
                nrow[F_Code] = ka.CD;
                nrow[F_From] = ka.XRow.Kaiin_DateNyukai;

                string str = $"{AppDate.GetDate(ka.XRow.Kaiin_DateNyukai)} ～ ";
                if (ka.XRow.Kaiin_DateTaikai_Null == null)
                {
                    str += $"{AppDate.GetDate(DateTime.Now)}";
                }
                else
                {
                    str += $"{AppDate.GetDate(ka.XRow.Kaiin_DateTaikai)}";
                }
                str += $"（{ka.GetMembershipYearMonth()}）";

                nrow[F_Range] = str;

                tbl.Rows.Add(nrow);
            }

            dvGrid = new DBView(tbl);
            dvGrid.SortQuery(F_From);

            iKaiin.Text = $"{SelectKaiin.XRow.Kaiin_Name}";
            iIsekiNo.Text = $"{SelectKaiin.XRow.Kaiin_IsekiTorokuNo}";
            iTotal.Text = $" {SelectKaiin.GetTotalMembershipYearMonth()}";

            // グリッド
            sgrid = new SGridDB(grid, this);
            sgrid.Add(new SGridDBUnbound(F_Code, "No", ubNo, 50, SGridDBCellDisp.Right));
            sgrid.Add(new SGridDBText(F_Code, "会員コード", 100, SGridDBCellDisp.Right));
            sgrid.Add(new SGridDBText(F_Range, "期間", 0.0f));
            sgrid.EndAdd(dvGrid);

            grid.Select();

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// 行の番号（1 から）
        /// </summary>
        string ubNo(SGridDBColumn col, int row)
        {
            return $"{row + 1}";
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncKaiin_DlgHistory.Functions);

            funcKeys.On(FuncKaiin_DlgHistory.Close, formClose);
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        void formClose()
        {
            this.Close();
        }
    }
}
