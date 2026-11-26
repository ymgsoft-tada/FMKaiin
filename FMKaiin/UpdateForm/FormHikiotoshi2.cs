using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using App.Controls;
using ComponentDB;
using ComponentDebug;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 引落データの一覧画面。<see cref="FormHikiotoshi"/> と同じ内容を、S 系の部品（Controls/Common）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。処理月度は <see cref="SDateBox"/>、一覧は <see cref="SGridDB"/>、
    /// 合計は、無効にした <see cref="SNumberBox"/>（表示するだけ）を使う。
    /// 引落データの作成・個別再作成の処理は、元の画面と同じ。編集は、S 系の画面（<see cref="FormHikiotoshi2_DlgEntry"/>）を開く。
    /// </remarks>
    public partial class FormHikiotoshi2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        // 引落データ
        DBView dvHiki;
        DBView dvGrid;

        SGridDB sgrid;

        /// <summary>グリッドに、データを結び付けたか（最初の読み込みで結び付け、その後は差し替える）</summary>
        bool gridBound = false;

        const string F_Hiki_CostTotal = "F_Hiki_CostTotal";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormHikiotoshi2()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);
        }

        /// <summary>
        /// キー操作
        /// </summary>
        protected override void FormFrame_KeyDown(object sender, KeyEventArgs e)
        {
            //■ ファンクションキーの標準ショートカットの抑止
            e.Handled = base.Patcher_CheckShortcutForKillingWindowsAction(e.KeyCode, e.Alt);

            // 処理が必要かどうかチェック。
            if (base.CheckEnabledFormKeyPreview(e.KeyCode) == true)
            {
                return;
            }
            if (this.ActiveControl == null)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (this.ActiveControl == grid)
                    {
                        showEdit();
                    }
                    break;
            }
        }

        /// <summary>
        /// 初回描画処理
        /// </summary>
        protected override void FormFrame_Shown(object sender, EventArgs e)
        {
            dvHiki = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_hikiotoshi));

            // 初期値はシステム日付（当月）
            DateTime dt = DateTime.Now;
            iDateYM.Value = new DateTime(dt.Year, dt.Month, 1);

            sgrid = new SGridDB(grid, this);
            sgrid.Add(new SGridDBNumber(t_kaiin.FCD_Kaiin, "会員コード", 120));
            sgrid.Add(new SGridDBText(t_kaiin.FKaiin_Name, "氏名", 200));
            sgrid.Add(new SGridDBCurrency(F_Hiki_CostTotal, "引落金額", 0.0f));

            // 読み込んで、グリッドに結び付ける
            dataLoad();

            // イベントの登録
            iDateYM.ValueChanged += iDateYM_ValueChanged;
            grid.MouseDoubleClick += grid_MouseDoubleClick;

            base.FormFrame_Shown(sender, e);
        }

        private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dvGrid.Count > 0)
            {
                showEdit();
            }
        }

        /// <summary>
        /// 処理月度を確定したとき（Enter・フォーカスの移動・カレンダーでの選択）に、その月度のデータを読み込む
        /// </summary>
        private void iDateYM_ValueChanged(object sender, EventArgs e)
        {
            dataLoad();
        }

        /// <summary>
        /// 引落データ（個人合算分）のロード
        /// </summary>
        void dataLoad()
        {
            DateTime ym;
            if (iDateYM.Value == null)
            {
                ym = new DateTime(1900, 1, 1);
            }
            else
            {
                ym = iDateYM.Value.Value;
            }

            FormBg_Progress prog = new FormBg_Progress();
            prog.TitleText = "しばらくお待ちください。";
            prog.LabelText = "データを取得しています。";
            prog.DoWorkEvent += prog_DoWorkEvent;
            prog.Args = new object[] { ym };
            prog.ShowDialog();
            prog.Dispose();

            // 読み込むたびに、新しいビューになる。最初は列と一緒に結び付け、その後はデータだけを差し替える
            if (gridBound == false)
            {
                sgrid.EndAdd(dvGrid);
                gridBound = true;
            }
            else
            {
                sgrid.SetView(dvGrid);
            }

            iTotalCount.Value = dvGrid.Count;

            decimal cost = 0;
            for (int i = 0; i < dvGrid.Count; i++)
            {
                cost += Cast.Decimal(dvGrid[i][F_Hiki_CostTotal]);
            }
            iTotalCost.Value = cost;

            DBView dv = new DBView(dvHiki);
            dv.RowFilterQuery($"{t_hikiotoshi.FHiki_DateYM} = #{ym}#");
            dv.SortQuery($"{t_hikiotoshi.FLastUpdate} DESC");

            string lblupdate = "";
            string lbluser = "";
            if (dv.Count > 0)
            {
                t_hikiotoshi xrow = new t_hikiotoshi(dv[0]);
                Tanto tnt = AppGlobal.Tantos.Get(xrow.LastUpdateUser);
                lblupdate = $"最終更新：{xrow.LastUpdate.ToString("yyyy/MM/dd hh:mm")}";
                if (tnt != null)
                {
                    lbluser = $"{tnt.Name}";
                }
            }
            lblCreateDate.Text = lblupdate;
            lblUpdateUser.Text = lbluser;

            // 編集ボタンの使用可否
            funcKeys.SetEnabled(FuncHikiotoshi.Edit.Key, dvGrid.Count > 0);
            funcKeys.SetEnabled(FuncHikiotoshi.ReMake.Key, dvGrid.Count > 0);
        }

        private void prog_DoWorkEvent(object sender, DoWorkEventArgs e)
        {
            FormBg_Progress prog = (FormBg_Progress)sender;
            DateTime ym = Cast.DateTime(prog.Args[0]);

            // 集計結果のクエリ
            string sql = $"SELECT " +
                 $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FID_Kaiin}, " +
                 $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM}, " +
                 $"Sum({TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_Cost}) AS {F_Hiki_CostTotal}, " +
                 $"{TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin}, " +
                 $"{TableProp.t_kaiin}.{t_kaiin.FKaiin_Name} " +
                 $"FROM {TableProp.t_hikiotoshi} " +
                 $"LEFT JOIN {TableProp.t_kaiin} ON {TableProp.t_hikiotoshi}.{t_hikiotoshi.FID_Kaiin} = {TableProp.t_kaiin}.{t_kaiin.FID_Kaiin} " +
                 $"GROUP BY " +
                 $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FID_Kaiin}, " +
                 $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM}, " +
                 $"{TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin}, " +
                 $"{TableProp.t_kaiin}.{t_kaiin.FKaiin_Name} " +
                 $"HAVING ((({TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM}) = #{ym}#)) " +
                 $"ORDER BY " +
                 $"{TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin}, " +
                 $"{TableProp.t_kaiin}.{t_kaiin.FKaiin_Name}";

            DataTable dt = AppGlobal.DB.FillQuery(sql);

            dvGrid = new DBView(dt, this.BindingContext);
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncHikiotoshi.Functions);

            funcKeys.On(FuncHikiotoshi.Exec, doExec);
            funcKeys.On(FuncHikiotoshi.ReMake, reMake);
            funcKeys.On(FuncHikiotoshi.Edit, showEdit);
            funcKeys.On(FuncHikiotoshi.Close, formClose);
        }

        /// <summary>
        /// 個別再作成（選んでいる会員の引落データだけを、作り直す）
        /// </summary>
        void reMake()
        {
            if (dvGrid.Count > 0)
            {
                t_kaiin xrow = new t_kaiin(dvGrid.CurrentRow);
                string name = $"{xrow.CD_Kaiin}：{xrow.Kaiin_Name}";

                if (AppMsgBox.Show(this, AppMsgBoxIndex.ConfirmRemakeHikiotoshiOnlyOne, name) == DialogResult.Yes)
                {
                    // 既存のデータは消す
                    DBView dv = new DBView(dvHiki);
                    dv.RowFilterQuery($"{t_hikiotoshi.FID_Kaiin} = {xrow.ID_Kaiin}");
                    for (; dv.Count > 0;)
                    {
                        dv.Delete();
                    }

                    if (createData(xrow.ID_Kaiin) == true)
                    {
                        // 現在の位置
                        int frow = grid.FirstRow;
                        int crow = grid.Row;

                        // データのロード
                        dataLoad();

                        // 元のグリッド位置へ
                        grid.FirstRow = frow;
                        grid.Row = crow;

                        AppMsgBox.Show(this, AppMsgBoxIndex.RemakeHikiotoshiData, name);
                    }
                    else
                    {
                        AppMsgBox.Show(this, AppMsgBoxIndex.FailedHikiotoshiData);
                    }
                }
            }
        }

        /// <summary>
        /// 編集（選んでいる会員の引落データの編集画面を開く）
        /// </summary>
        void showEdit()
        {
            if (dvGrid.Count > 0)
            {
                t_kaiin xrow = new t_kaiin(dvGrid.CurrentRow);

                FormHikiotoshi2_DlgEntry frm = new FormHikiotoshi2_DlgEntry();
                frm.SelectYM = iDateYM.Value.Value;
                frm.SelectKaiin = AppGlobal.Kaiins.Get(xrow.ID_Kaiin);
                frm.ShowDialog();
                if (frm.FormCloseReason == FormCloseReason.Save)
                {
                    // 現在の位置
                    int frow = grid.FirstRow;
                    int crow = grid.Row;

                    // データのロード
                    dataLoad();

                    // 元のグリッド位置へ
                    grid.FirstRow = frow;
                    grid.Row = crow;
                }
            }
        }

        /// <summary>
        /// 作成実行（処理月度の引落データを作る。すでにあれば、確認してから作り直す）
        /// </summary>
        void doExec()
        {
            if (iDateYM.Value == null)
            {
                iDateYM.Select();
                appToolTip.Show(iDateYM, AppToolTipIndex.CannotUseBlank);
            }
            else
            {
                string date = AppDate.GetYearMonth(iDateYM.Value.Value) + "度";

                if (dvGrid.Count == 0)
                {
                    if (AppMsgBox.Show(this, AppMsgBoxIndex.ConfirmCreateHikiotoshi, date) == DialogResult.Yes)
                    {
                        if (createData() == true)
                        {
                            dataLoad();
                            AppMsgBox.Show(this, AppMsgBoxIndex.CreateHikiotoshiData, date);
                        }
                        else
                        {
                            AppMsgBox.Show(this, AppMsgBoxIndex.FailedHikiotoshiData);
                        }
                    }
                }
                else
                {
                    if (AppMsgBox.Show(this, AppMsgBoxIndex.ConfirmRemakeHikiotoshi, date) == DialogResult.Yes &&
                        AppMsgBox.Show(this, AppMsgBoxIndex.ConfirmRemakeHikiotoshi2) == DialogResult.Yes)
                    {
                        // 既存のデータは消す
                        DBView dv = new DBView(dvHiki);
                        dv.RowFilterQuery($"{t_hikiotoshi.FHiki_DateYM} = #{iDateYM.Value}#");
                        for (; dv.Count > 0;)
                        {
                            dv.Delete();
                        }

                        if (createData() == true)
                        {
                            dataLoad();
                            AppMsgBox.Show(this, AppMsgBoxIndex.CreateHikiotoshiData, date);
                        }
                        else
                        {
                            AppMsgBox.Show(this, AppMsgBoxIndex.FailedHikiotoshiData);
                        }
                    }
                }
            }
        }

        bool createData(object kaiin_id = null)
        {
            bool ret = true;
            try
            {
                FormBg_Progress prog = new FormBg_Progress();
                prog.DoWorkEvent += prog_create;
                prog.Arg = kaiin_id;
                prog.ShowDialog();
                prog.Dispose();
            }
            catch (Exception ex)
            {
                ErrLog.WriteException(ex);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 引落データの生成
        /// </summary>
        private void prog_create(object sender, DoWorkEventArgs e)
        {
            FormBg_Progress prog = (FormBg_Progress)sender;

            prog.AdvanceProgress(20);

            DBView dv_kai = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin));
            dv_kai.RowFilterQuery($"{t_kaiin.FKaiin_TypeZaiseki} <> {(int)eTypeZaiseki.Taikai}");
            dv_kai.SortQuery($"{t_kaiin.FCD_Kaiin}");

            prog.AdvanceProgress(100);

            // 新規ID
            int id = AppDbID.GetNewID(dvHiki, t_hikiotoshi.FID_Hikiotoshi);
            DateTime ym = iDateYM.Value.Value;
            // 会員IDが指定されていれば数値が入る
            int kaiin_id = Cast.Int(prog.Arg);

            for (int i = 0; i < dv_kai.Count; i++)
            {
                t_kaiin xrow = new t_kaiin(dv_kai[i]);

                // 会員情報から所属会費を取得
                Kaiin ka = null;

                // 会員指定がない、または指定された会員であればデータを作成する
                if (kaiin_id == 0 || kaiin_id == xrow.ID_Kaiin)
                {
                    ka = AppGlobal.Kaiins.Get(xrow.ID_Kaiin);
                }

                if (ka != null)
                {
                    foreach (Kaihi k in ka.Kaihis)
                    {
                        t_hikiotoshi nrow = new t_hikiotoshi(dvHiki.NewRow());
                        nrow.ID_Hikiotoshi = id++;
                        nrow.ID_Kaihi = k.ID;
                        nrow.ID_Kaiin = ka.ID;
                        nrow.Hiki_DateYM = ym;
                        nrow.Hiki_Cost_Null = k.GetCost(ym);
                        nrow.Hiki_Shiharai = ka.GetShiharai(k);
                        nrow.LastUpdateUser = AppGlobal.LoginUser.ID;
                        dvHiki.Add(nrow.Row);
                    }

                    // 調整金額
                    if (ka.Choseis.ContainsKey(ym))
                    {
                        foreach (var crow in ka.Choseis[ym])
                        {
                            t_hikiotoshi nrow = new t_hikiotoshi(dvHiki.NewRow());
                            nrow.ID_Hikiotoshi = id++;
                            nrow.ID_Kaihi = crow.ID_Kaihi;
                            nrow.ID_Kaiin = ka.ID;
                            nrow.Hiki_DateYM = ym;
                            nrow.Hiki_Cost_Null = crow.KCS_Cost_Null;
                            nrow.Hiki_Shiharai = crow.KCS_Shiharai;
                            nrow.LastUpdateUser = AppGlobal.LoginUser.ID;
                            dvHiki.Add(nrow.Row);
                        }
                    }
                }
            }

            AppGlobal.DB.UpdateTable(TableProp.t_hikiotoshi);
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
