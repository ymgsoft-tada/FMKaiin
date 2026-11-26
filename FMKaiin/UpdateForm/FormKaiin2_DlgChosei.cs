using System;
using System.Data;
using System.Windows.Forms;
using ComponentSUI;
using ComponentDB;

namespace App
{
    /// <summary>
    /// 会費調整金額入力画面。<see cref="FormKaiin_DlgChosei"/> と同じ内容を、S 系の部品（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。一覧は <see cref="SGridDB"/> で、セルに入力欄を重ねて、その場で編集する
    /// （月分は <see cref="SDateBox"/>、会費は <see cref="STableComboBox"/>、金額は <see cref="SNumberBox"/>、
    /// 支払方法は <see cref="SComboBox"/>、備考は <see cref="STextBox"/>）。
    /// 入力欄と行の値の受け渡しは <see cref="SControlDB"/>、入力ルールは <see cref="AppSControlRule"/> を使う。
    /// </remarks>
    public partial class FormKaiin2_DlgChosei : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        public Kaiin SelectKaiin { get; set; }

        DBView dvGrid;

        SGridDB sgrid;
        SControlDB sctl;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormKaiin2_DlgChosei()
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
                            // 一覧・カレンダーを開いている間の Enter は、コントロールが使う（指している行・日の確定）
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
                            // 一覧・カレンダーを開いている間の Esc は、コントロールが使う（閉じる）
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
            if (SelectKaiin != null)
            {
                iKaiin.Text = $"{SelectKaiin.CD} {SelectKaiin.XRow.Kaiin_Name}";
            }

            DBView dvKaihi = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaihi));
            AppSTableCombo.SetComboBox_Kaihi(iKaihi, dvKaihi);

            dvGrid = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_kaiin_chosei, $"ORDER BY {t_kaiin_chosei.FKCS_DateYM}"), this.BindingContext);
            dvGrid.RowFilterQuery($"{t_kaiin_chosei.FID_Kaiin} = {SelectKaiin.ID}");

            AppSCombo.SetComboBox(iShiharai, enumKbn.DShiharai, (int)eShiharai.None);

            sgrid = new SGridDB(grid, this);
            sgrid.Add(new SGridDBText(t_kaiin_chosei.FKCS_DateYM, "月分", 130, SGridDBCellDisp.Center)
            {
                UnboundFetch = ubYM,
                InputControl = iDateYM,
                InputTabIndex = 0,
            });
            sgrid.Add(new SGridDBText(t_kaiin_chosei.FID_Kaihi, "会費", 200)
            {
                UnboundFetch = ubKaihi,
                InputControl = iKaihi,
                InputTabIndex = 1,
            });
            sgrid.Add(new SGridDBCurrency(t_kaiin_chosei.FKCS_Cost, "金額", 120)
            {
                InputControl = iCost,
                InputTabIndex = 2,
            });
            sgrid.Add(new SGridDBText(t_kaiin_chosei.FKCS_Shiharai, "支払方法", 100, SGridDBCellDisp.Center)
            {
                UnboundFetch = ubShiharai,
                InputControl = iShiharai,
                InputTabIndex = 3,
            });
            sgrid.Add(new SGridDBText(t_kaiin_chosei.FKCS_Memo, "備考", 0.0f)
            {
                InputControl = iMemo,
                InputTabIndex = 4,
            });
            sgrid.EndAdd(dvGrid);

            sctl = new SControlDB(this, getDataRow);
            sctl.Add(new SControlDBDate(t_kaiin_chosei.FKCS_DateYM, iDateYM));
            sctl.Add(new SControlDBCombo(t_kaiin_chosei.FID_Kaihi, iKaihi));
            sctl.Add(new SControlDBCombo(t_kaiin_chosei.FKCS_Shiharai, iShiharai));
            sctl.Add(new SControlDBNumber(t_kaiin_chosei.FKCS_Cost, iCost));
            sctl.Add(new SControlDBText(t_kaiin_chosei.FKCS_Memo, iMemo));
            sctl.EndAdd(AppSControlRule.Rule);

            rowFetch();

            // イベント登録
            grid.RowColChange += grid_RowColChange;

            grid.Select();

            base.FormFrame_Shown(sender, e);
        }

        private void grid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
        {
            rowFetch();
        }

        /// <summary>
        /// 月分
        /// </summary>
        string ubYM(SGridDBColumn col, int row)
        {
            t_kaiin_chosei xrow = new t_kaiin_chosei(dvGrid[row]);

            if (xrow.KCS_DateYM_Null == null)
            {
                return null;
            }

            return AppDate.GetYearMonth(xrow.KCS_DateYM);
        }

        /// <summary>
        /// 支払方法
        /// </summary>
        string ubShiharai(SGridDBColumn col, int row)
        {
            t_kaiin_chosei xrow = new t_kaiin_chosei(dvGrid[row]);
            return enumKbn.DShiharai[(int)xrow.KCS_Shiharai];
        }

        /// <summary>
        /// 会費
        /// </summary>
        string ubKaihi(SGridDBColumn col, int row)
        {
            t_kaiin_chosei xrow = new t_kaiin_chosei(dvGrid[row]);
            Kaihi ka = AppGlobal.Kaihis.Get(xrow.ID_Kaihi);

            if (ka != null)
            {
                return ka.XRow.Kaihi_Name;
            }

            return null;
        }

        /// <summary>
        /// 指している行の内容を、入力欄にセットします（行が無ければ、入力欄を空にします）
        /// </summary>
        void rowFetch()
        {
            if (sctl == null)
            {
                return;
            }

            if (dvGrid.Count == 0 || dvGrid.CurrentRow == null)
            {
                sctl.ClearAll();
            }
            else
            {
                sctl.FetchAll();
            }
        }

        protected override void FormFrame_Disposed(object sender, EventArgs e)
        {
            if (dvGrid != null)
            {
                dvGrid.Dispose();
            }

            base.FormFrame_Disposed(sender, e);
        }

        /// <summary>
        /// 入力欄とやり取りする行（グリッドで指している行）
        /// </summary>
        DataRow getDataRow()
        {
            if (dvGrid.Count > 0 && dvGrid.CurrentRow != null)
            {
                return dvGrid.CurrentRow.Row;
            }

            return null;
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncKaiin_DlgChosei.Functions);

            funcKeys.On(FuncKaiin_DlgChosei.RowAdd, rowAdd);
            funcKeys.On(FuncKaiin_DlgChosei.RowDelete, rowDelete);
            funcKeys.On(FuncKaiin_DlgChosei.Save, saveClose);
            funcKeys.On(FuncKaiin_DlgChosei.Cancel, formClose);
        }

        /// <summary>
        /// 追加（行を足して、月分の入力を始める）
        /// </summary>
        void rowAdd()
        {
            t_kaiin_chosei xrow = new t_kaiin_chosei(dvGrid.NewRow());
            xrow.ID_Kaiin = SelectKaiin.ID;
            xrow.KCS_Shiharai = eShiharai.Koza1;
            dvGrid.Add(xrow.Row);

            // 追加した行を指して、編集状態にする
            sgrid.SetRow(dvGrid.Count - 1);
            sgrid.Select();
            grid.Col = 0;
            rowFetch();
            sgrid.SelectInput();
        }

        /// <summary>
        /// 削除（指している行を消す）
        /// </summary>
        void rowDelete()
        {
            if (dvGrid.Count == 0)
            {
                return;
            }

            if (AppMsgBox.Show(this, AppMsgBoxIndex.DeleteSelectedRow) == DialogResult.Yes)
            {
                dvGrid.Delete();

                // 入力状態になっているかもしれないので、グリッドへフォーカスを移す
                sgrid.Select();
                rowFetch();
            }
        }

        /// <summary>
        /// 検証処理（月分・会費・金額は、入力必須）
        /// </summary>
        bool validateRow()
        {
            // フォーカスがある入力欄の内容を、行へ書き戻す（ファンクション キーではフォーカスが動かないため）
            sctl.Update();

            for (int i = 0; i < dvGrid.Count; i++)
            {
                t_kaiin_chosei xrow = new t_kaiin_chosei(dvGrid[i]);

                if (xrow.KCS_DateYM_Null == null)
                {
                    selectInputAt(i, 0);
                    appToolTip.Show(iDateYM, AppToolTipIndex.CannotUseBlank);
                    return false;
                }
                else
                if (xrow.ID_Kaihi_Null == null)
                {
                    selectInputAt(i, 1);
                    appToolTip.Show(iKaihi, AppToolTipIndex.CannotUseBlank);
                    return false;
                }
                else
                if (xrow.KCS_Cost_Null == null)
                {
                    selectInputAt(i, 2);
                    appToolTip.Show(iCost, AppToolTipIndex.CannotUseBlank);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 指定した行・列のセルの入力を始める
        /// </summary>
        void selectInputAt(int row, int col)
        {
            sgrid.SetRow(row);
            sgrid.Select();
            grid.Col = col;
            sgrid.SelectInput();
        }

        /// <summary>
        /// 登録
        /// </summary>
        void saveClose()
        {
            if (validateRow() == true)
            {
                // DB更新
                AppGlobal.DB.UpdateTable(TableProp.t_kaiin_chosei);

                formCloseReason = FormCloseReason.Save;
                this.Close();
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        void formClose()
        {
            // フォーカスがある入力欄の内容を、行へ書き戻してから、変更の有無を調べる
            sctl.Update();

            if (dvGrid.HasChanges() == true)
            {
                if (AppMsgBox.Show(this, AppMsgBoxIndex.CancelClose) == DialogResult.No) return;
            }
            this.Close();
        }
    }
}
