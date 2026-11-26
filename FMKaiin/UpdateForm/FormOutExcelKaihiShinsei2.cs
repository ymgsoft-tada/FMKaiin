using System;
using ComponentSUI;
using ComponentDB;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 各種医会等申請書の Excel 出力の画面。<see cref="FormOutExcelKaihiShinsei"/> と同じ内容を、S 系のコントロール（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。会員は <see cref="STableComboBox"/>、書類タイプの選択は <see cref="SRadioButton"/> を使う。
    /// 出力の処理は、元の画面と同じもの（<see cref="AppExcelKaihiShinsei"/>）を使う。
    /// </remarks>
    public partial class FormOutExcelKaihiShinsei2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormOutExcelKaihiShinsei2()
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
            DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin));
            AppSTableCombo.SetComboBox_Kaiin(iKaiinFrom, dv);

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncOutExcelKaihiShinsei.Functions);

            rdoNyukai.Checked = true;

            funcKeys.On(FuncOutExcelKaihiShinsei.OutExcel, doExec);
            funcKeys.On(FuncOutExcelKaihiShinsei.Close, formClose);
        }

        /// <summary>
        /// 選択されている書類タイプを取得します。
        /// </summary>
        AppExcelKaihiShinsei.eType getSelectedType()
        {
            if (rdoNyukaiKenshui.Checked == true)
            {
                return AppExcelKaihiShinsei.eType.NyukaiKenshui;
            }
            if (rdoHenko.Checked == true)
            {
                return AppExcelKaihiShinsei.eType.Henko;
            }
            if (rdoHenkoKorei.Checked == true)
            {
                return AppExcelKaihiShinsei.eType.HenkoKorei;
            }
            if (rdoHenko4joC.Checked == true)
            {
                return AppExcelKaihiShinsei.eType.Henko4joC;
            }

            return AppExcelKaihiShinsei.eType.Nyukai;
        }

        /// <summary>
        /// Excel 出力
        /// </summary>
        void doExec()
        {
            // 会員の指定は必須
            Kaiin ka = AppGlobal.Kaiins.Get(Cast.Int(iKaiinFrom.SelectedValue));
            if (ka == null)
            {
                iKaiinFrom.Select();
                appToolTip.Show(iKaiinFrom, AppToolTipIndex.CannotUseBlank);
                return;
            }

            AppExcelKaihiShinsei xls = new AppExcelKaihiShinsei(getSelectedType(), ka);

            switch (xls.Execute())
            {
                case AppExcel.eResult.Success:
                    AppMsgBox.Show(this, AppMsgBoxIndex.CreateExcelFile);
                    break;
                case AppExcel.eResult.Cancel:
                    break;
                case AppExcel.eResult.NotExistTemplate:
                    AppMsgBox.Show(this, AppMsgBoxIndex.NotExistFile);
                    break;
                case AppExcel.eResult.NotFoundData:
                    AppMsgBox.Show(this, AppMsgBoxIndex.NotFoundDataExport);
                    break;
                default:
                    AppMsgBox.Show(this, AppMsgBoxIndex.FailedExcelFile);
                    break;
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
