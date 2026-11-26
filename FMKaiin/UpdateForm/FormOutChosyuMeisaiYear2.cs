using System;
using App.Controls;
using ComponentDB;

namespace App
{
    /// <summary>
    /// 年間会費徴収明細書の印刷条件の画面。<see cref="FormOutChosyuMeisaiYear"/> と同じ内容を、S 系のコントロール（Controls/Common）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。対象年は <see cref="SDateBox"/>、会員・医療機関は <see cref="STableComboBox"/>、
    /// 在籍区分は <see cref="SComboBox"/> を使う。帳票は、元の画面と同じもの（<see cref="ReportFlexChoshuMeisaiYear"/>）を使う。
    /// 対象年の欄の形（年だけ。カレンダーは、年を選ぶ表示になる）は、元の画面では表示のときに設定していたが、この画面ではデザイナーで設定してある。
    /// </remarks>
    public partial class FormOutChosyuMeisaiYear2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormOutChosyuMeisaiYear2()
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
            // 対象年の初期値: 当日
            iTaishoYear.Value = DateTime.Now;

            // コンボの作成
            // 会員コード
            DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin));
            AppSTableCombo.SetComboBox_Kaiin(iKaiinFrom, dv);
            AppSTableCombo.SetComboBox_Kaiin(iKaiinTo, dv);

            // 在籍区分。値の範囲で指定するので、値の並びが分かるように「1：在籍」の形で表示する
            // （「1：在籍 ～ 3：退会」なら、間の「2：異動」も含む）
            AppSCombo.SetComboBoxWithCode(iZaisekiFrom, enumKbn.DTypeZaiseki);
            AppSCombo.SetComboBoxWithCode(iZaisekiTo, enumKbn.DTypeZaiseki);

            // 医療機関コード
            DBView dv_iryo = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_iryokikan));
            AppSTableCombo.SetComboBox_IryoKikan(iIryoFrom, dv_iryo);
            AppSTableCombo.SetComboBox_IryoKikan(iIryoTo, dv_iryo);

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncOutChosyuMeisaiYear.Functions);

            funcKeys.On(FuncOutChosyuMeisaiYear.Preview, preview);
            funcKeys.On(FuncOutChosyuMeisaiYear.Close, formClose);
        }

        /// <summary>
        /// 検証処理
        /// </summary>
        /// <returns></returns>
        bool validateRow()
        {
            // 空白チェック（入力必須は、対象年だけ。会員・在籍区分・医療機関は、空欄なら絞り込まない）
            if (iTaishoYear.Value == null)
            {
                iTaishoYear.Select();
                appToolTip.Show(iTaishoYear, AppToolTipIndex.CannotUseBlank);
                return false;
            }

            return true;
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        void preview()
        {
            if (validateRow() == true)
            {
                ReportFlexChoshuMeisaiYear rep = new ReportFlexChoshuMeisaiYear();
                rep.SelectedDateYM = iTaishoYear.Value.Value;
                rep.CreateView(
                    iKaiinFrom.SelectedValue, iKaiinTo.SelectedValue,
                    iZaisekiFrom.GetValue(iZaisekiFrom.SelectedIndex), iZaisekiTo.GetValue(iZaisekiTo.SelectedIndex),
                    iIryoFrom.SelectedValue, iIryoTo.SelectedValue);
                rep.ReportExec();

                rep = null;
            }
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
