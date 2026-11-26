using System;
using System.Data;
using System.Windows.Forms;
using ComponentSUI;
using C1.Win.C1TrueDBGrid;
using ComponentDB;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 引落データ編集画面。<see cref="FormHikiotoshi_DlgEntry"/> と同じ内容を、S 系の部品（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。一覧は <see cref="SGridDB"/> で、セルに入力欄を重ねて、その場で編集する
    /// （参加医会は <see cref="STableComboBox"/>、金額は <see cref="SNumberBox"/>、備考は <see cref="STextBox"/>）。
    /// 入力欄と行の値の受け渡しは <see cref="SControlDB"/>、入力ルールは <see cref="AppSControlRule"/> を使う。
    /// </remarks>
    public partial class FormHikiotoshi2_DlgEntry : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>選択中の月度</summary>
        public DateTime SelectYM { get; set; }

        /// <summary>選択中の会員情報</summary>
        public Kaiin SelectKaiin { get; set; }

        DBView dvHiki;

        SGridDB sgrid;
        SControlDB sctl;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormHikiotoshi2_DlgEntry()
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
            if (sgrid != null)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (this.ActiveControl == grid)
                        {
                            // グリッドで Enter: 指しているセルの入力を始める
                            sgrid.SelectInput();
                            return;
                        }
                        if (sgrid.IsInputControl(this.ActiveControl))
                        {
                            // 参加医会の一覧を開いている間の Enter は、コントロールが使う（指している行の確定）
                            if (base.CheckEnabledFormKeyPreview(e.KeyCode) == true)
                            {
                                return;
                            }

                            // 次の入力欄へ。最後の行の最後の入力欄からは、グリッドへ戻る
                            sgrid.SelectNextInput(!e.Shift);
                            return;
                        }
                        break;

                    case Keys.Escape:
                        if (sgrid.IsInputControl(this.ActiveControl))
                        {
                            // 一覧を開いている間の Esc は、コントロールが使う（一覧を閉じる）
                            if (base.CheckEnabledFormKeyPreview(e.KeyCode) == true)
                            {
                                return;
                            }

                            // 入力をやめて、グリッドへ戻る
                            sgrid.Select();
                            return;
                        }
                        break;
                }
            }

            base.FormFrame_KeyDown(sender, e); // 条件に合わない場合は、親の処理
        }

        /// <summary>
        /// 初回描画処理
        /// </summary>
        protected override void FormFrame_Shown(object sender, EventArgs e)
        {
            dvHiki = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_hikiotoshi), this.BindingContext);
            dvHiki.RowFilterQuery($"{t_hikiotoshi.FID_Kaiin} = {SelectKaiin.ID} AND {t_hikiotoshi.FHiki_DateYM} = #{SelectYM}#");

            iDateYM.Text = AppDate.GetYearMonth(SelectYM);
            iKaiin.Text = $"{SelectKaiin.CD} {SelectKaiin.XRow.Kaiin_Name}";

            // 選択された会員の会費だけで抽出する
            DBView dvKaihi = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaihi));
            dvKaihi.SortQuery($"{t_kaihi.FCD_Kaihi}");
            string filter = "";
            foreach (Kaihi ka in SelectKaiin.Kaihis)
            {
                if (filter != "") filter += " OR ";
                filter += $"{t_kaihi.FID_Kaihi} = {ka.ID}";
            }
            dvKaihi.RowFilterQuery(filter);

            AppSTableCombo.SetComboBox_Kaihi(iKaihi, dvKaihi); // iSearch 参加医会

            sgrid = new SGridDB(grid, this);
            sgrid.Add(new SGridDBText(t_hikiotoshi.FID_Kaihi, "参加医会", 180)
            {
                UnboundFetch = ubIkai,
                InputControl = iKaihi,
                InputTabIndex = 0,
            });
            sgrid.Add(new SGridDBCurrency(t_hikiotoshi.FHiki_Cost, "金額", 150)
            {
                InputControl = iCost,
                InputTabIndex = 1,
            });
            sgrid.Add(new SGridDBText(t_hikiotoshi.FHiki_Memo, "備考", 0.0f)
            {
                InputControl = iMemo,
                InputTabIndex = 2,
            });
            sgrid.EndAdd(dvHiki);

            sctl = new SControlDB(this, getDataRow);
            SControlDBCombo cmb = new SControlDBCombo(t_hikiotoshi.FID_Kaihi, iKaihi);
            cmb.UpdateRunEx = cmbUpdateRunEx;
            sctl.Add(cmb);
            sctl.Add(new SControlDBNumber(t_hikiotoshi.FHiki_Cost, iCost));
            sctl.Add(new SControlDBText(t_hikiotoshi.FHiki_Memo, iMemo));
            sctl.EndAdd(AppSControlRule.Rule);

            rowFetch();
            calcTotal();

            // イベント登録
            grid.RowColChange += grid_RowColChange;
            iCost.ValueChanged += iCost_ValueChanged;

            grid.Select();

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// 参加医会を行へ書き戻すとき: その会費の支払方法を、会員の設定から入れる
        /// </summary>
        object cmbUpdateRunEx(ISRow row, object data)
        {
            DataRow current = getDataRow();
            if (current != null)
            {
                t_hikiotoshi xrow = new t_hikiotoshi(current);
                xrow.Hiki_Shiharai = eShiharai.None;
                Kaihi kaihi = AppGlobal.Kaihis.Get(Cast.Int(data));
                if (kaihi != null)
                {
                    xrow.Hiki_Shiharai = SelectKaiin.GetShiharai(kaihi);
                }
            }

            return data;
        }

        /// <summary>
        /// 閉じる処理
        /// </summary>
        protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formCloseReason == FormCloseReason.Save)
            {
                AppGlobal.DB.UpdateTable(TableProp.t_hikiotoshi);
            }
            else
            {
                // フォーカスがある入力欄の内容を、行へ書き戻してから、変更の有無を調べる
                if (sctl != null)
                {
                    sctl.Update();
                }

                if (dvHiki.HasChanges() == true && AppMsgBox.Show(this, AppMsgBoxIndex.CancelClose) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    dvHiki.RejectChanges();
                }
            }

            base.FormFrame_FormClosing(sender, e);
        }

        /// <summary>
        /// 金額の変更: 入力するたびに、行へ書いて、合計を直す
        /// </summary>
        private void iCost_ValueChanged(object sender, EventArgs e)
        {
            sctl.UpdateByControl(iCost);
            calcTotal();
        }

        private void grid_RowColChange(object sender, RowColChangeEventArgs e)
        {
            rowFetch();
        }

        /// <summary>
        /// 引落金額の合計の更新
        /// </summary>
        void calcTotal()
        {
            decimal cost = 0;
            for (int i = 0; i < dvHiki.Count; i++)
            {
                t_hikiotoshi xrow = new t_hikiotoshi(dvHiki[i]);
                cost += xrow.Hiki_Cost;
            }
            iTotalCost.Value = cost;
        }

        /// <summary>
        /// 指している行の内容を、入力欄にセットします（行が無ければ、入力欄を空にします）
        /// </summary>
        void rowFetch()
        {
            if (dvHiki.CurrentRow == null)
            {
                sctl.ClearAll();
            }
            else
            {
                sctl.FetchAll();
            }
        }

        /// <summary>
        /// 入力欄とやり取りする行（グリッドで指している行）
        /// </summary>
        DataRow getDataRow()
        {
            if (dvHiki.CurrentRow != null)
            {
                return dvHiki.CurrentRow.Row;
            }

            return null;
        }

        /// <summary>
        /// 参加医会名
        /// </summary>
        string ubIkai(SGridDBColumn col, int row)
        {
            t_hikiotoshi xrow = new t_hikiotoshi(dvHiki[row]);
            Kaihi ka = AppGlobal.Kaihis.Get(xrow.ID_Kaihi);

            if (ka != null)
            {
                return ka.XRow.Kaihi_Name;
            }

            return null;
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncHikiotoshi_DlgEntry.Functions);

            funcKeys.On(FuncHikiotoshi_DlgEntry.RowAdd, rowAdd);
            funcKeys.On(FuncHikiotoshi_DlgEntry.RowDelete, rowDelete);
            funcKeys.On(FuncHikiotoshi_DlgEntry.Save, saveClose);
            funcKeys.On(FuncHikiotoshi_DlgEntry.Cancel, formClose);
        }

        /// <summary>
        /// 追加（行を足して、参加医会の入力を始める）
        /// </summary>
        void rowAdd()
        {
            t_hikiotoshi xrow = new t_hikiotoshi(dvHiki.NewRow());
            xrow.ID_Hikiotoshi = AppDbID.GetNewID(dvHiki, t_hikiotoshi.FID_Hikiotoshi);
            xrow.ID_Kaiin = SelectKaiin.ID;
            xrow.Hiki_DateYM = SelectYM;
            xrow.LastUpdateUser = AppGlobal.LoginUser.ID;
            dvHiki.Add(xrow.Row);

            // 追加した行を指して、編集状態にする
            sgrid.SetRow(dvHiki.Count - 1);
            sgrid.Select();
            grid.Col = 0;
            sgrid.SelectInput();
        }

        /// <summary>
        /// 削除（指している行を消す）
        /// </summary>
        void rowDelete()
        {
            if (dvHiki.Count == 0)
            {
                return;
            }

            if (AppMsgBox.Show(this, AppMsgBoxIndex.DeleteSelectedRow) == DialogResult.Yes)
            {
                dvHiki.Delete();

                // 入力状態になっているかもしれないので、グリッドへフォーカスを移す
                sgrid.Select();
                rowFetch();
                calcTotal();
            }
        }

        /// <summary>
        /// 検証処理
        /// </summary>
        bool validateRow()
        {
            // フォーカスがある入力欄の内容を、行へ書き戻す（ファンクション キーではフォーカスが動かないため）
            sctl.Update();

            for (int i = 0; i < dvHiki.Count; i++)
            {
                t_hikiotoshi xrow = new t_hikiotoshi(dvHiki[i]);

                if (xrow.ID_Kaihi_Null == null)
                {
                    sgrid.SetRow(i);
                    sgrid.Select();
                    grid.Col = 0;
                    sgrid.SelectInput();
                    appToolTip.Show(iKaihi, AppToolTipIndex.CannotUseBlank);
                    return false;
                }

                if (xrow.Row.RowState != DataRowState.Unchanged)
                {
                    xrow.LastUpdateUser = AppGlobal.LoginUser.ID;
                }
            }

            return true;
        }

        /// <summary>
        /// 登録
        /// </summary>
        void saveClose()
        {
            if (validateRow() == true)
            {
                formCloseReason = FormCloseReason.Save;
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
