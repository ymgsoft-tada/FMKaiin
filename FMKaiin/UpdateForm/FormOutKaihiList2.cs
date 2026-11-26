using System;
using ComponentSUI;
using ComponentDB;

namespace App
{
    /// <summary>
    /// 会費種別一覧表の印刷条件の画面。<see cref="FormOutKaihiList"/> と同じ内容を、S 系のコントロール（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。会費区分は <see cref="SComboBox"/>、会費は <see cref="STableComboBox"/> を使う。
    /// 帳票は、元の画面と同じもの（<see cref="ReportFlexKaihiList"/>）を使う。
    /// </remarks>
    public partial class FormOutKaihiList2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormOutKaihiList2()
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
            // コンボの作成
            // 会費区分コード。値の範囲で指定するので、値の並びが分かるように「値：名称」の形で表示する
            AppSCombo.SetComboBoxWithCode(iTypeKaihiFrom, enumKbn.DTypeKaihi);
            AppSCombo.SetComboBoxWithCode(iTypeKaihiTo, enumKbn.DTypeKaihi);

            // 会費コード
            DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaihi));
            AppSTableCombo.SetComboBox_Kaihi(iKaihiFrom, dv);
            AppSTableCombo.SetComboBox_Kaihi(iKaihiTo, dv);

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncOutKaihiList.Functions);

            funcKeys.On(FuncOutKaihiList.Preview, preview);
            funcKeys.On(FuncOutKaihiList.Close, formClose);
        }

        /// <summary>
        /// プレビュー（入力必須の欄は無い。どの欄も、空欄なら絞り込まない）
        /// </summary>
        void preview()
        {
            ReportFlexKaihiList rep = new ReportFlexKaihiList();
            rep.CreateView(
                iTypeKaihiFrom.GetValue(iTypeKaihiFrom.SelectedIndex), iTypeKaihiTo.GetValue(iTypeKaihiTo.SelectedIndex),
                iKaihiFrom.SelectedValue, iKaihiTo.SelectedValue);
            rep.ReportExec();

            rep = null;
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
