using System;
using System.Collections.Generic;
using System.ComponentModel;
using ComponentSUI;
using ComponentDB;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 年間会費徴収書類の Excel 出力の画面。<see cref="FormOutExcelNenkanChoshu"/> と同じ内容を、S 系のコントロール（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。年は <see cref="SDateBox"/>、会員は <see cref="STableComboBox"/>、
    /// 書類タイプの選択は <see cref="SRadioButton"/> を使う。出力の処理は、元の画面と同じもの（<see cref="AppExcelNenkanChoshu"/>）を使う。
    /// </remarks>
    public partial class FormOutExcelNenkanChoshu2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormOutExcelNenkanChoshu2()
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
            // 会員コンボの作成
            DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin));
            AppSTableCombo.SetComboBox_Kaiin(iKaiinFrom, dv);

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncOutExcelNenkanChoshu.Functions);

            iYear.Value = AppDate.ParseDateFirstDay(DateTime.Now);

            rdoYotei.Checked = true;

            funcKeys.On(FuncOutExcelNenkanChoshu.OutExcel, doExec);
            funcKeys.On(FuncOutExcelNenkanChoshu.Close, formClose);
        }

        /// <summary>
        /// Excel 出力
        /// </summary>
        void doExec()
        {
            if (iYear.Value == null)
            {
                iYear.Select();
                appToolTip.Show(iYear, AppToolTipIndex.CannotUseBlank);
                return;
            }

            // 会員の指定は必須
            Kaiin ka = AppGlobal.Kaiins.Get(Cast.Int(iKaiinFrom.SelectedValue));
            if (ka == null)
            {
                iKaiinFrom.Select();
                appToolTip.Show(iKaiinFrom, AppToolTipIndex.CannotUseBlank);
                return;
            }

            int year = iYear.Value.Value.Year;
            AppExcelNenkanChoshu.eType type = (rdoYotei.Checked == true) ? AppExcelNenkanChoshu.eType.Yotei : AppExcelNenkanChoshu.eType.Meisai;

            // 出力は1人ずつ。複数人に広げる場合は、このリストに会員を足す
            List<Kaiin> kaiins = new List<Kaiin>();
            kaiins.Add(ka);

            // 銀行名の取得に銀行コードを使う。未ロードならここでロードする
            initBankCode();

            AppExcelNenkanChoshu xls = new AppExcelNenkanChoshu(year, type, kaiins);

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
        /// 銀行コードデータの取得処理
        /// </summary>
        void initBankCode()
        {
            // 銀行コードは件数が多く、使う画面を開くときにロードされる（Form1と同じ手順）
            if (AppGlobal.BankCodeMg == null)
            {
                FormBg_Progress prog = new FormBg_Progress();
                prog.TitleText = "しばらくお待ちください。";
                prog.LabelText = "データをロードしています。";
                prog.DoWorkEvent += prog_DoWorkEvent;
                prog.ShowDialog();
                prog.Dispose();
                prog = null;
            }
        }

        private void prog_DoWorkEvent(object sender, DoWorkEventArgs e)
        {
            FormBg_Progress prog = (FormBg_Progress)sender;
            prog.AdvanceProgress(100);

            AppGlobal.DB.GetReFillTable(TableProp.t_bank_code);
            AppGlobal.InitBankCodeMg();
            AppGlobal.Banks.Init();
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
