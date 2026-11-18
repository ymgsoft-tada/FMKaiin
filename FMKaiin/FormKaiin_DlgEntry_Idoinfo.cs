using ComponentDB;
using ComponentGControlDB;
using ComponentIO;
using GControlGcTextBoxEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 tanaka]
	/// 会員-異動情報登録画面
	/// </summary>
	public partial class FormKaiin_DlgEntry_Idoinfo : FormFrame
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

		/// <summary>
		/// 処理モード
		/// </summary>
		public eMode Mode { get; set;}

		/// <summary>
		/// コントロールとデータ連結クラス_異動情報用
		/// </summary>
		GControlDB gctl;

		/// <summary>
		/// 起動時のレコード状態
		/// </summary>
		DataRow srcrow = null;
		/// <summary>
		/// 編集するレコード
		/// </summary>
		DataRow editrow = null;

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
		public FormKaiin_DlgEntry_Idoinfo()
		{
			InitializeComponent();

			Mode = eMode.Add;
		}

		/// <summary>
		/// フォームロード
		/// </summary>
		protected override void FormFrame_Load(object sender, EventArgs e)
		{
			string title = "【追加】";

			if (Mode == eMode.Edit)
			{
				title = "【訂正】";
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
			AppCombo.SetComboBox(iIdoKbn, enumKbn.DTypeIdoJiyu); // 異動事由区分
			AppCombo.SetComboBox(iIdoIdo, enumKbn.DIdoShisetsu); // 医師会の異動・施設開業・施設異動:詳細
			AppCombo.SetComboBox(iIdoKaiin, enumKbn.DIdoKaiinHenko); // 会員区分の変更:詳細
			AppCombo.SetComboBox(iIdoShozai, enumKbn.DIdoShozai); // 施設所在地変更:詳細
			AppCombo.SetComboBox(iIdoAddress, enumKbn.DIdoAddress); // 現住所変更:詳細
			AppCombo.SetComboBox(iIdoEtc, enumKbn.DIdoEtc); // その他:詳細

			// 画面～DBの紐づけ
			gctl = new GControlDB(this, getDataRow);

			gctl.Add(new GControlDBDate(t_kaiin_ido.FIdoDate, iIdoDate.UcDateCtl));
			gctl.Add(new GControlDBCombo(t_kaiin_ido.FIdoJiyuKbn, iIdoKbn));

			gctl.Add(new GControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Ido, iIdoIdo));
			gctl.Add(new GControlDBText(t_kaiin_ido.FIdoJiyuDetail_IdoMemo, iIdoIdoMemo));
			gctl.Add(new GControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Kaiin, iIdoKaiin));
			gctl.Add(new GControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Shozai, iIdoShozai));
			gctl.Add(new GControlDBText(t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo, iIdoShozaiMemo));
			gctl.Add(new GControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Address, iIdoAddress));
			gctl.Add(new GControlDBText(t_kaiin_ido.FIdoJiyuDetail_AddressMemo, iIdoAddressMemo));
			gctl.Add(new GControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Etc, iIdoEtc));
			gctl.Add(new GControlDBText(t_kaiin_ido.FIdoJiyuDetail_EtcMemo, iIdoEtcMemo));

			gctl.EndAdd(AppDbRule.Rule);

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
			//iIdoKaiin.SelectedIndexChanged += iIdoKaiin_SelectedIndexChanged;
			iIdoShozai.SelectedIndexChanged += iIdoShozai_SelectedIndexChanged;
			iIdoAddress.SelectedIndexChanged += iIdoAddress_SelectedIndexChanged;
			iIdoEtc.SelectedIndexChanged += iIdoEtc_SelectedIndexChanged;

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
		/// 異動詳細部の切替
		/// </summary>
		void idoDetailDisp(int selectIdx)
		{
			switch (selectIdx)
			{
				// 医師会の異動・施設開業・施設異動
				case (int)eTypeIdoJiyu.Ido:
					pnl_Ido.Visible = true; // 医師会の異動・施設開業・施設異動
					pnl_Kaiin.Visible = false; // 会員区分の変更
					pnl_Shozai.Visible = false; // 施設所在地変更
					pnl_Address.Visible = false; // 現住所変更
					pnl_Etc.Visible = false; // その他
					break;

				// 会員区分の変更
				case (int)eTypeIdoJiyu.Kaiin:
					pnl_Ido.Visible = false; // 医師会の異動・施設開業・施設異動
					pnl_Kaiin.Visible = true; // 会員区分の変更
					pnl_Shozai.Visible = false; // 施設所在地変更
					pnl_Address.Visible = false; // 現住所変更
					pnl_Etc.Visible = false; // その他
					break;

				// 施設所在地変更
				case (int)eTypeIdoJiyu.Shozai:
					pnl_Ido.Visible = false; // 医師会の異動・施設開業・施設異動
					pnl_Kaiin.Visible = false; // 会員区分の変更
					pnl_Shozai.Visible = true; // 施設所在地変更
					pnl_Address.Visible = false; // 現住所変更
					pnl_Etc.Visible = false; // その他
					break;

				// 現住所変更
				case (int)eTypeIdoJiyu.Address:
					pnl_Ido.Visible = false; // 医師会の異動・施設開業・施設異動
					pnl_Kaiin.Visible = false; // 会員区分の変更
					pnl_Shozai.Visible = false; // 施設所在地変更
					pnl_Address.Visible = true; // 現住所変更
					pnl_Etc.Visible = false; // その他
					break;

				// その他
				case (int)eTypeIdoJiyu.Etc:
					pnl_Ido.Visible = false; // 医師会の異動・施設開業・施設異動
					pnl_Kaiin.Visible = false; // 会員区分の変更
					pnl_Shozai.Visible = false; // 施設所在地変更
					pnl_Address.Visible = false; // 現住所変更
					pnl_Etc.Visible = true; // その他
					break;

				default:
					pnl_Ido.Visible = false; // 医師会の異動・施設開業・施設異動
					pnl_Kaiin.Visible = false; // 会員区分の変更
					pnl_Shozai.Visible = false; // 施設所在地変更
					pnl_Address.Visible = false; // 現住所変更
					pnl_Etc.Visible = false; // その他
					break;
			}
		}

		/// <summary>
		/// 異動事由:異動 詳細区分変更イベント
		/// </summary>
		void iIdoIdo_SelectedIndexChanged(object sender, EventArgs e)
		{
			enabled_iIdoEtcMemo();
		}

		/// <summary>
		/// 異動事由:会員 詳細区分変更イベント
		/// </summary>
		//void iIdoKaiin_SelectedIndexChanged(object sender, EventArgs e)
		//{
		// 会員区分の変更:廃業・その他 入力欄なし
		//}

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
				gctl.FetchAll();
			}
			else
			{
				gctl.ClearAll();
			}
		}

		/// <summary>
		/// 閉じる処理
		/// </summary>
		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (formCloseReason != App.FormCloseReason.Save)
			{
				if (this.ActiveControl is UcDate)
				{
					gctl.UpdateByControl(((UcDate)this.ActiveControl).UcDateCtl);
				}
				else
				if (this.ActiveControl is UcTableComboBox)
				{
					gctl.UpdateByControl(((UcTableComboBox)this.ActiveControl).ComboBox);
				}
				else
				{
					gctl.Update(this);
				}

				if (Mode == eMode.Add || AppDb.CheckModified(srcrow, editrow) == true)
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
			appFuncKey = new AppFunctionKey(funckey, FuncKaiin_DlgEntry_Idoinfo.Functions);
;
			FuncKaiin_DlgEntry_Idoinfo.Save.Execute   = saveClose;
			FuncKaiin_DlgEntry_Idoinfo.Cancel.Execute = closeCancel;
		}

		/// <summary>
		/// 検証処理
		/// </summary>
		/// <returns></returns>
		bool validateRow()
		{
			// 最終フォーカスコントロールをRowへ反映
			updateCurrentControlValue(gctl);

			// 現在の異動事由区分関連以外の詳細値クリア
			switch (iIdoKbn.SelectedIndex)
			{
				// 医師会の異動・施設開業・施設異動
				case (int)eTypeIdoJiyu.Ido:
					//this.Row[t_kaiin_ido.FIdoJiyuDetail_Ido] = eIdoShisetsu.None;
					//this.Row[t_kaiin_ido.FIdoJiyuDetail_IdoMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Kaiin] = eIdoKaiinHenko.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Shozai] = eIdoShozai.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Address] = eIdoAddress.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_AddressMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Etc] = eIdoEtc.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_EtcMemo] = null;
					break;

				// 会員区分の変更
				case (int)eTypeIdoJiyu.Kaiin:
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Ido] = eIdoShisetsu.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_IdoMemo] = null;
					//this.Row[t_kaiin_ido.FIdoJiyuDetail_Kaiin] = eIdoKaiinHenko.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Shozai] = eIdoShozai.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Address] = eIdoAddress.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_AddressMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Etc] = eIdoEtc.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_EtcMemo] = null;
					break;

				// 施設所在地変更
				case (int)eTypeIdoJiyu.Shozai:
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Ido] = eIdoShisetsu.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_IdoMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Kaiin] = eIdoKaiinHenko.None;
					//this.Row[t_kaiin_ido.FIdoJiyuDetail_Shozai] = eIdoShozai.None;
					//this.Row[t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Address] = eIdoAddress.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_AddressMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Etc] = eIdoEtc.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_EtcMemo] = null;
					break;

				// 現住所変更
				case (int)eTypeIdoJiyu.Address:
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Ido] = eIdoShisetsu.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_IdoMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Kaiin] = eIdoKaiinHenko.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Shozai] = eIdoShozai.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo] = null;
					//this.Row[t_kaiin_ido.FIdoJiyuDetail_Address] = eIdoAddress.None;
					//this.Row[t_kaiin_ido.FIdoJiyuDetail_AddressMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Etc] = eIdoEtc.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_EtcMemo] = null;
					break;

				// その他
				case (int)eTypeIdoJiyu.Etc:
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Ido] = eIdoShisetsu.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_IdoMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Kaiin] = eIdoKaiinHenko.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Shozai] = eIdoShozai.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Address] = eIdoAddress.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_AddressMemo] = null;
					//this.Row[t_kaiin_ido.FIdoJiyuDetail_Etc] = eIdoEtc.None;
					//this.Row[t_kaiin_ido.FIdoJiyuDetail_EtcMemo] = null;
					break;

				default:
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Ido] = eIdoShisetsu.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_IdoMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Kaiin] = eIdoKaiinHenko.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Shozai] = eIdoShozai.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Address] = eIdoAddress.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_AddressMemo] = null;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_Etc] = eIdoEtc.None;
					this.Row[t_kaiin_ido.FIdoJiyuDetail_EtcMemo] = null;
					break;
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
		DataRow getDataRow(string tag)
		{
			if (editrow != null)
			{
				return editrow;
			}

			return null;
		}
	}
}
