using System;
using App.Controls;
using ComponentDB;
using ComponentIO;

namespace App
{
    /// <summary>
    /// iFAXデータ（BizFAX 宛先リスト）作成の画面。<see cref="FormCreateIfaxFile"/> と同じ内容を、S 系のコントロール（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。iFAX グループは <see cref="STableComboBox"/> で選ぶ。
    /// 作成の処理は、元の画面と同じもの（<see cref="AppIfaxFile"/>）を使う。結果は、S 系の結果の画面（<see cref="FormLogView2"/>）で表示する。
    /// </remarks>
    public partial class FormCreateIfaxFile2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// iFAXグループ
        /// </summary>
        DBView dvIfax;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormCreateIfaxFile2()
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
            dvIfax = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_ifax));
            AppSTableCombo.SetComboBox_IfaxGroup(iIfax, dvIfax); // iFAXグループ

            iIfax.Select();

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// ファンクションの登録
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncCreateIfaxFile.Functions);

            funcKeys.On(FuncCreateIfaxFile.Exec, doExec);
            funcKeys.On(FuncCreateIfaxFile.Close, formClose);
        }

        /// <summary>
        /// 作成実行
        /// </summary>
        void doExec()
        {
            // iFAXグループ：未指定の場合は全グループを出力
            int? id_ifax = null;
            if (iIfax.Text != "")
            {
                id_ifax = Cast.Int(iIfax.SelectedValue);
            }

            AppIfaxFile aif = new AppIfaxFile();

            aif.ShowSaveFilePath();

            // 実行処理
            AppIfaxFile.eResut ret = aif.Create(id_ifax);

            switch (ret)
            {
                case AppIfaxFile.eResut.DataNotFound:
                case AppIfaxFile.eResut.Success:
                case AppIfaxFile.eResut.CannotFileSave:
                    // 結果画面を表示
                    FormLogView2 frm = new FormLogView2();
                    frm.SelectedYM = AppDate.ParseDateFirstDay(DateTime.Now); // 年月の指定はないため、システム日付の年月
                    frm.Table = aif.Logs.GetTable();
                    frm.TitleText = "iFAXデータ作成結果";
                    frm.ShowDialog();
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
