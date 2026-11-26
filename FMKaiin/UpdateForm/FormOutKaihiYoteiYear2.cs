using System;
using App.Controls;
using ComponentDB;

namespace App
{
    /// <summary>
    /// 年間会費徴収予定通知書の印刷条件の画面。<see cref="FormOutKaihiYoteiYear"/> と同じ内容を、S 系のコントロール（Controls/Common）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。対象年月は <see cref="SDateBox"/>、会員・医療機関は <see cref="STableComboBox"/>、在籍区分は <see cref="SComboBox"/> を使う。
    /// 帳票は、元の画面と同じもの（<see cref="ReportFlexKaihiYoteiYear"/>）を使う。
    /// </remarks>
    public partial class FormOutKaihiYoteiYear2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormOutKaihiYoteiYear2()
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
            // 対象年月の初期値: 今月（調整データは○月1日登録のため、月の 1 日にする）
            iTaishoYM.Value = AppDate.ParseDateFirstDay(DateTime.Now);

            // コンボの作成
            // 会員コード
            DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin));
            AppSTableCombo.SetComboBox_Kaiin(iKaiinFrom, dv);
            AppSTableCombo.SetComboBox_Kaiin(iKaiinTo, dv);

            // 在籍区分。値の範囲で指定するので、値の並びが分かるように「値：名称」の形で表示する
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
            funcKeys = new FunctionKeyBinder(funckey, FuncOutKaihiYoteiYear.Functions);

            funcKeys.On(FuncOutKaihiYoteiYear.Preview, preview);
            funcKeys.On(FuncOutKaihiYoteiYear.Close, formClose);
        }

        /// <summary>
        /// 検証処理
        /// </summary>
        /// <returns></returns>
        bool validateRow()
        {
            // 空白チェック（入力必須は、対象年月だけ。ほかの欄は、空欄なら絞り込まない）
            if (iTaishoYM.Value == null)
            {
                iTaishoYM.Select();
                appToolTip.Show(iTaishoYM, AppToolTipIndex.CannotUseBlank);
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
                ReportFlexKaihiYoteiYear rep = new ReportFlexKaihiYoteiYear();
                rep.SelectedDateYM = iTaishoYM.Value.Value;
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
