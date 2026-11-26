using System;
using System.Data;
using System.Windows.Forms;
using App.Controls;
using ComponentDB;

namespace App
{
    /// <summary>
    /// 基本情報。<see cref="FormMasterBasic"/> と同じ内容を、S 系のコントロール（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系のコントロール・コードは使っていない。項目と入力欄の受け渡しは <see cref="SControlDB"/>、
    /// 入力ルールは <see cref="AppSControlRule"/>、銀行の一覧は <see cref="AppSTableCombo"/> を使う。
    /// FormMasterBasic との違いは、FAX 番号の入力欄を電話番号とは別に持つこと
    /// （FormMasterBasic は、「電話番号 2」と「FAX 番号」の 2 項目を、同じ 3 つの入力欄に結び付けている）。
    /// 「電話番号 2」の項目は、この画面では表示も変更もしない。
    /// </remarks>
    public partial class FormMasterBasic2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        private SControlDB sctl;
        private DBView dvBasic;

        public FormMasterBasic2()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);
        }

        /// <summary>
        /// フォームを閉じる
        /// </summary>
        protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formCloseReason == FormCloseReason.Save)
            {
                if (dvBasic.HasChanges() == true)
                {
                    AppGlobal.DB.UpdateTable(TableProp.t_basic);

                    // Basicクラスの再定義(情報更新の可能性があるため)
                    AppGlobal.InitBasic();
                }
            }

            base.FormFrame_FormClosing(sender, e);
        }

        /// <summary>
        /// 初回描画処理
        /// </summary>
        protected override void FormFrame_Shown(object sender, EventArgs e)
        {
            // 振込元銀行の一覧
            AppSTableCombo.SetComboBox_Bank(iBank);

            // 日付表示のコンボ ボックス
            iDispSeireki.BeginUpdate();
            iDispSeireki.AddItem("西暦", true);
            iDispSeireki.AddItem("和暦", false);
            iDispSeireki.EndUpdate();

            dvBasic = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_basic));

            sctl = new SControlDB(this, getDataRow, AppGlobal.DB.DBZipCode);

            sctl.Add(new SControlDBRuby(
                            new string[]{
                                t_basic.FBAS_Name,
                                t_basic.FBAS_NameFurigana,
                            },
                            new STextBox[]{
                                iName,
                                iNameFurigana,
                            }));

            sctl.Add(new SControlDBText(t_basic.FBAS_NameDaihyo, iNameDaihyo));

            sctl.Add(new SControlDBPostAddr(
                            new string[]{
                                t_basic.FBAS_Post,
                                t_basic.FBAS_Addr1
                            },
                            new STextBox[]{
                                iPost1,
                                iPost2,
                                iAddr1
                            }));

            sctl.Add(new SControlDBText(t_basic.FBAS_Addr2, iAddr2));
            sctl.Add(new SControlDBHyphenSplit(t_basic.FBAS_Tel1, new STextBox[] { iTel1_1, iTel1_2, iTel1_3 }));
            sctl.Add(new SControlDBHyphenSplit(t_basic.FBAS_Fax, new STextBox[] { iFax_1, iFax_2, iFax_3 }));
            sctl.Add(new SControlDBText(t_basic.FBAS_HojinNo, iHojinNo));
            sctl.Add(new SControlDBCombo(t_basic.FBAS_DispSeireki, iDispSeireki));
            sctl.Add(new SControlDBCombo(t_basic.FID_Bank, iBank));

            sctl.EndAdd(AppSControlRule.Rule);

            rowFetch();

            iName.Select();

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncMasterBasic.Functions);

            funcKeys.On(FuncMasterBasic.ShowBank, showBank);
            funcKeys.On(FuncMasterBasic.Save, saveClose);
            funcKeys.On(FuncMasterBasic.Cancel, cancelClose);
        }

        /// <summary>
        /// 銀行情報リスト表示
        /// </summary>
        void showBank()
        {
            FormMasterBasic2_Bank frm = new FormMasterBasic2_Bank();
            frm.ShowDialog();
            frm.Dispose();

            // 銀行情報が変更されている可能性があるので、一覧を作り直してから、行の値で選択し直す
            AppSTableCombo.SetComboBox_Bank(iBank);
            sctl.FetchByControl(iBank);
        }

        /// <summary>
        /// レコードの検証
        /// </summary>
        /// <returns></returns>
        bool validateRow()
        {
            // フォーカスがある入力欄の内容を、行へ書き戻す（ファンクション キーではフォーカスが動かないため）
            sctl.Update();

            t_basic xrow = new t_basic(dvBasic[0]);

            // 空白チェック
            STextBox[] ctls =
            {
                iName, iNameFurigana
            };

            foreach (STextBox ctl in ctls)
            {
                if (ctl.TextLength == 0)
                {
                    ctl.Select();
                    appToolTip.Show(ctl, AppToolTipIndex.CannotUseBlank);
                    return false;
                }
            }

            if (xrow.BAS_Name == null)
            {
                iName.Select();
                appToolTip.Show(iName, AppToolTipIndex.CannotUseBlank);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 登録処理
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
        void cancelClose()
        {
            this.Close();
        }

        /// <summary>
        /// 行情報のフェッチ
        /// </summary>
        void rowFetch()
        {
            if (sctl != null)
            {
                sctl.FetchAll();
            }
        }

        /// <summary>
        /// 行情報の取得
        /// </summary>
        DataRow getDataRow()
        {
            return dvBasic[0].Row;
        }
    }
}
