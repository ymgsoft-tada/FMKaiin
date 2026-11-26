using System;
using System.Data;
using System.Windows.Forms;
using App.Controls;

namespace App
{
    /// <summary>
    /// 会員-異動情報登録画面。<see cref="FormKaiin_DlgEntry_Idoinfo"/> と同じ内容を、S 系の部品（Controls/Common）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。項目と入力欄の受け渡しは <see cref="SControlDB"/>、
    /// 入力ルールは <see cref="AppSControlRule"/>、区分の一覧は <see cref="AppSCombo"/> を使う。
    /// 異動事由ごとの詳細の欄は、同じ位置に重ねたパネルに置いてあり、選んだ異動事由のものだけを表示する。
    /// </remarks>
    public partial class FormKaiin2_DlgEntry_Idoinfo : FormFrame
    {
        /// <summary>
        /// 処理モード
        /// </summary>
        public enum eMode
        {
            /// <summary>追加</summary>
            Add,
            /// <summary>訂正</summary>
            Edit,
        }

        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// 処理モード
        /// </summary>
        public eMode Mode { get; set; }

        /// <summary>
        /// コントロールとデータ連結クラス_異動情報用
        /// </summary>
        SControlDB sctl;

        /// <summary>
        /// 起動時のレコード状態
        /// </summary>
        DataRow srcrow = null;
        /// <summary>
        /// 編集するレコード
        /// </summary>
        DataRow editrow = null;
        /// <summary>
        /// 画面を表示したときの、編集するレコードの内容（閉じるときに、入力があったかを調べるのに使う）
        /// </summary>
        RowSnapshot shownrow = null;

        /// <summary>
        /// 画面間連携用レコード
        /// </summary>
        public DataRow Row
        {
            get
            {
                return editrow;
            }
            set
            {
                srcrow = value; // 呼び元画面から受取った情報セット
                if (this.Mode == eMode.Add)
                {
                    editrow = srcrow;
                }
                else
                {
                    editrow = srcrow.Table.NewRow();
                    AppDb.CopyDataRow(srcrow, editrow);
                }
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormKaiin2_DlgEntry_Idoinfo()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);

            Mode = eMode.Add;
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        protected override void FormFrame_Load(object sender, EventArgs e)
        {
            string title = "：追加";

            if (Mode == eMode.Edit)
            {
                title = "：訂正";
            }

            this.Text += title;
            base.FormFrame_Load(sender, e);
        }

        /// <summary>
        /// 初回描画処理
        /// </summary>
        protected override void FormFrame_Shown(object sender, EventArgs e)
        {
            // コンボボックスの作成
            // enumから
            AppSCombo.SetComboBox(iIdoKbn, enumKbn.DTypeIdoJiyu); // 異動事由区分
            AppSCombo.SetComboBox(iIdoIdo, enumKbn.DIdoShisetsu); // 医師会の異動・施設開業・施設異動:詳細
            AppSCombo.SetComboBox(iIdoKaiin, enumKbn.DIdoKaiinHenko); // 会員区分の変更:詳細
            AppSCombo.SetComboBox(iIdoShozai, enumKbn.DIdoShozai); // 施設所在地変更:詳細
            AppSCombo.SetComboBox(iIdoAddress, enumKbn.DIdoAddress); // 現住所変更:詳細
            AppSCombo.SetComboBox(iIdoEtc, enumKbn.DIdoEtc); // その他:詳細

            // 画面～DBの紐づけ
            sctl = new SControlDB(this, getDataRow);

            sctl.Add(new SControlDBDate(t_kaiin_ido.FIdoDate, iIdoDate));
            sctl.Add(new SControlDBCombo(t_kaiin_ido.FIdoJiyuKbn, iIdoKbn));

            sctl.Add(new SControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Ido, iIdoIdo));
            sctl.Add(new SControlDBText(t_kaiin_ido.FIdoJiyuDetail_IdoMemo, iIdoIdoMemo));
            sctl.Add(new SControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Kaiin, iIdoKaiin));
            sctl.Add(new SControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Shozai, iIdoShozai));
            sctl.Add(new SControlDBText(t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo, iIdoShozaiMemo));
            sctl.Add(new SControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Address, iIdoAddress));
            sctl.Add(new SControlDBText(t_kaiin_ido.FIdoJiyuDetail_AddressMemo, iIdoAddressMemo));
            sctl.Add(new SControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Etc, iIdoEtc));
            sctl.Add(new SControlDBText(t_kaiin_ido.FIdoJiyuDetail_EtcMemo, iIdoEtcMemo));

            sctl.EndAdd(AppSControlRule.Rule);

            rowFetchIdo();

            // コントロール制御
            // 詳細部コントロール表示制御
            idoDetailDisp(iIdoKbn.SelectedIndex);
            enabled_iIdoEtcMemo();

            funckey.Select();
            iIdoDate.Select();

            // ■イベント
            iIdoKbn.SelectedIndexChanged += iIdoKbn_SelectedIndexChanged;

            iIdoIdo.SelectedIndexChanged += iIdoIdo_SelectedIndexChanged;
            iIdoShozai.SelectedIndexChanged += iIdoShozai_SelectedIndexChanged;
            iIdoAddress.SelectedIndexChanged += iIdoAddress_SelectedIndexChanged;
            iIdoEtc.SelectedIndexChanged += iIdoEtc_SelectedIndexChanged;

            // ここまでに入れた最初の値を控える。閉じるときに、これと比べて、入力があったかを調べる
            shownrow = new RowSnapshot(editrow);

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// 異動事由区分の変更イベント
        /// </summary>
        void iIdoKbn_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 詳細部の表示切替
            idoDetailDisp(iIdoKbn.SelectedIndex);
        }

        /// <summary>
        /// 異動詳細部の切替（選んだ異動事由の詳細の欄だけを表示する）
        /// </summary>
        void idoDetailDisp(int selectIdx)
        {
            pnl_Ido.Visible = selectIdx == (int)eTypeIdoJiyu.Ido; // 医師会の異動・施設開業・施設異動
            pnl_Kaiin.Visible = selectIdx == (int)eTypeIdoJiyu.Kaiin; // 会員区分の変更
            pnl_Shozai.Visible = selectIdx == (int)eTypeIdoJiyu.Shozai; // 施設所在地変更
            pnl_Address.Visible = selectIdx == (int)eTypeIdoJiyu.Address; // 現住所変更
            pnl_Etc.Visible = selectIdx == (int)eTypeIdoJiyu.Etc; // その他

            // 詳細の欄が無いとき（異動事由が未選択）は、項目名も消す
            lblIdoDetail.Visible = pnl_Ido.Visible || pnl_Kaiin.Visible || pnl_Shozai.Visible || pnl_Address.Visible || pnl_Etc.Visible;
        }

        /// <summary>
        /// 異動事由:異動 詳細区分変更イベント
        /// </summary>
        void iIdoIdo_SelectedIndexChanged(object sender, EventArgs e)
        {
            enabled_iIdoEtcMemo();
        }

        /// <summary>
        /// 異動事由:施設所在地 詳細区分変更イベント
        /// </summary>
        void iIdoShozai_SelectedIndexChanged(object sender, EventArgs e)
        {
            enabled_iIdoEtcMemo();
        }

        /// <summary>
        /// 異動事由:現住所 詳細区分変更イベント
        /// </summary>
        void iIdoAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            enabled_iIdoEtcMemo();
        }

        /// <summary>
        /// 異動事由:その他 詳細区分変更イベント
        /// </summary>
        void iIdoEtc_SelectedIndexChanged(object sender, EventArgs e)
        {
            enabled_iIdoEtcMemo();
        }

        /// <summary>
        /// 異動事由詳細 その他テキストボックス制御
        /// </summary>
        void enabled_iIdoEtcMemo()
        {
            // 医師会の異動・施設開業・施設異動:その他
            if (iIdoIdo.SelectedIndex == (int)eIdoShisetsu.Etc)
            {
                iIdoIdoMemo.Enabled = true;
            }
            else
            {
                iIdoIdoMemo.Enabled = false;
                iIdoIdoMemo.Text = null;
                this.Row[t_kaiin_ido.FIdoJiyuDetail_IdoMemo] = null;
            }

            // 会員区分の変更:廃業・その他 入力欄なし

            // 施設所在地変更:その他
            if (iIdoShozai.SelectedIndex == (int)eIdoShozai.Etc)
            {
                iIdoShozaiMemo.Enabled = true;
            }
            else
            {
                iIdoShozaiMemo.Enabled = false;
                iIdoShozaiMemo.Text = null;
                this.Row[t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo] = null;
            }

            // 現住所変更:その他
            if (iIdoAddress.SelectedIndex == (int)eIdoAddress.Etc)
            {
                iIdoAddressMemo.Enabled = true;
            }
            else
            {
                iIdoAddressMemo.Enabled = false;
                iIdoAddressMemo.Text = null;
                this.Row[t_kaiin_ido.FIdoJiyuDetail_AddressMemo] = null;
            }

            // その他:その他
            if (iIdoEtc.SelectedIndex == (int)eIdoEtc.Etc)
            {
                iIdoEtcMemo.Enabled = true;
            }
            else
            {
                iIdoEtcMemo.Enabled = false;
                iIdoEtcMemo.Text = null;
                this.Row[t_kaiin_ido.FIdoJiyuDetail_EtcMemo] = null;
            }
        }

        /// <summary>
        /// 追加/編集Row情報を各コントロールにセットします
        /// </summary>
        void rowFetchIdo()
        {
            if (Row != null)
            {
                sctl.FetchAll();
            }
            else
            {
                sctl.ClearAll();
            }
        }

        /// <summary>
        /// 閉じる処理
        /// </summary>
        protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formCloseReason != App.FormCloseReason.Save)
            {
                // フォーカスがある入力欄の内容を、行へ書き戻してから、変更の有無を調べる
                if (sctl != null)
                {
                    sctl.Update();
                }

                // 画面を表示したときから内容が変わっているときだけ、破棄の確認を出す
                if (shownrow != null && shownrow.IsModified(editrow) == true)
                {
                    if (AppMsgBox.Show(AppMsgBoxIndex.CancelClose) == System.Windows.Forms.DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }

            base.FormFrame_FormClosing(sender, e);
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncKaiin_DlgEntry_Idoinfo.Functions);

            funcKeys.On(FuncKaiin_DlgEntry_Idoinfo.Save, saveClose);
            funcKeys.On(FuncKaiin_DlgEntry_Idoinfo.Cancel, closeCancel);
        }

        /// <summary>
        /// 検証処理
        /// </summary>
        /// <returns></returns>
        bool validateRow()
        {
            // フォーカスがある入力欄の内容を、行へ書き戻す（ファンクション キーではフォーカスが動かないため）
            sctl.Update();

            // 選んでいる異動事由のもの以外の、詳細の値を消す
            int kbn = iIdoKbn.SelectedIndex;

            if (kbn != (int)eTypeIdoJiyu.Ido) // 医師会の異動・施設開業・施設異動
            {
                this.Row[t_kaiin_ido.FIdoJiyuDetail_Ido] = eIdoShisetsu.None;
                this.Row[t_kaiin_ido.FIdoJiyuDetail_IdoMemo] = null;
            }
            if (kbn != (int)eTypeIdoJiyu.Kaiin) // 会員区分の変更
            {
                this.Row[t_kaiin_ido.FIdoJiyuDetail_Kaiin] = eIdoKaiinHenko.None;
            }
            if (kbn != (int)eTypeIdoJiyu.Shozai) // 施設所在地変更
            {
                this.Row[t_kaiin_ido.FIdoJiyuDetail_Shozai] = eIdoShozai.None;
                this.Row[t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo] = null;
            }
            if (kbn != (int)eTypeIdoJiyu.Address) // 現住所変更
            {
                this.Row[t_kaiin_ido.FIdoJiyuDetail_Address] = eIdoAddress.None;
                this.Row[t_kaiin_ido.FIdoJiyuDetail_AddressMemo] = null;
            }
            if (kbn != (int)eTypeIdoJiyu.Etc) // その他
            {
                this.Row[t_kaiin_ido.FIdoJiyuDetail_Etc] = eIdoEtc.None;
                this.Row[t_kaiin_ido.FIdoJiyuDetail_EtcMemo] = null;
            }

            // 空白チェック
            // なし

            // 重複チェック
            // なし

            return true;
        }

        /// <summary>
        /// 保存終了
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
        void closeCancel()
        {
            this.Close();
        }

        /// <summary>
        /// データ取得
        /// </summary>
        DataRow getDataRow()
        {
            return editrow;
        }
    }
}
