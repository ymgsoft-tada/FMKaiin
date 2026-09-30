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
	public partial class FormStaff_DlgEntry_Idoinfo : FormFrame
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
			/// <summary>コピー追加</summary>
//			Copy
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
		public FormStaff_DlgEntry_Idoinfo()
		{
			InitializeComponent();

			Mode = eMode.Add;
		}

		protected override void FormFrame_Load(object sender, EventArgs e)
		{
			string title = "【追加】";

			if (Mode == eMode.Edit)
			{
				title = "【訂正】";
			}
//			else
//			if (Mode == eMode.Copy)
//			{
//				title = "【コピー追加】";
//			}

			this.Text +=  title;
			base.FormFrame_Load(sender, e);
		}

		/// <summary>
		/// ロード処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// コンボボックスの作成
			// enumから
			AppCombo.SetComboBox(iIdoKbn, enumKbn.DTypeIdoJiyu); // 異動事由区分
			AppCombo.SetComboBox(iIdoIdo, enumKbn.DIdoShisetsu); // 施設異動:詳細
			AppCombo.SetComboBox(iIdoKaiin, enumKbn.DIdoKaiinHenko); // 会員区分変更:詳細
			AppCombo.SetComboBox(iIdoEtc, enumKbn.DIdoEtc); // その他:詳細

			// 画面～DBの紐づけ
			gctl = new GControlDB(this, getDataRow);

			gctl.Add(new GControlDBDate(t_kaiin_ido.FIdoDate, iIdoDate.UcDateCtl));
			gctl.Add(new GControlDBCombo(t_kaiin_ido.FIdoJiyuKbn, iIdoKbn));

//			gctl.Add(new GControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Kaigyo, iIdoKaigyo)); // コンボなし登録値なしだがバインドしないとエラーになるか？
			gctl.Add(new GControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Ido, iIdoIdo));
			gctl.Add(new GControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Kaiin, iIdoKaiin));
			gctl.Add(new GControlDBCombo(t_kaiin_ido.FIdoJiyuDetail_Etc, iIdoEtc));
			gctl.Add(new GControlDBText(t_kaiin_ido.FIdoJiyuEtcMemo, iIdoEtcmemo));

			gctl.EndAdd(AppDbRule.Rule);

			rowFetchIdo();
			idoDetailDisp(iIdoKbn.SelectedIndex); // 詳細部コントロール表示制御

			funckey.Select(); // ?????
			iIdoDate.Select();

			// コントロール制御
			// 異動区分未選択のため詳細選択部パネルを非表示
//			pnl_Ido.Visible = false;
//			pnl_Kaiin.Visible = false;
//			pnl_Etc.Visible = false;

			// ■イベント
			iIdoKbn.SelectedIndexChanged += iIdoKbn_SelectedIndexChanged;

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
				// 施設開業
				case (int)eTypeIdoJiyu.Kaigyo:
					pnl_Ido.Visible = false; // 施設異動
					pnl_Kaiin.Visible = false; // 会員区分変更
					pnl_Etc.Visible = false; // その他
					break;

				// 施設異動
				case (int)eTypeIdoJiyu.Ido:
					pnl_Ido.Visible = true; // 施設異動
					pnl_Kaiin.Visible = false; // 会員区分変更
					pnl_Etc.Visible = false; // その他
					break;

				// 会員区分変更
				case (int)eTypeIdoJiyu.Kaiin:
					pnl_Ido.Visible = false; // 施設異動
					pnl_Kaiin.Visible = true; // 会員区分変更
					pnl_Etc.Visible = false; // その他
					break;

				// その他
				case (int)eTypeIdoJiyu.Etc:
					pnl_Ido.Visible = false; // 施設異動
					pnl_Kaiin.Visible = false; // 会員区分変更
					pnl_Etc.Visible = true; // その他
					break;

				default:
					pnl_Ido.Visible = false; // 施設異動
					pnl_Kaiin.Visible = false; // 会員区分変更
					pnl_Etc.Visible = false; // その他
					break;
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
			appFuncKey = new AppFunctionKey(funckey, FuncStaff_DlgEntry_Idoinfo.Functions);
;
			FuncStaff_DlgEntry_Idoinfo.Save.Execute   = saveClose;
			FuncStaff_DlgEntry_Idoinfo.Cancel.Execute = closeCancel;
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
				// 施設開業
				case (int)eTypeIdoJiyu.Kaigyo:
					iIdoIdo.SelectedIndex = (int)eIdoShisetsu.None;
					iIdoKaiin.SelectedIndex = (int)eIdoKaiinHenko.None;
					iIdoEtc.SelectedIndex = (int)eIdoEtc.None;
					iIdoEtcmemo.Text = null;
					break;

				// 施設異動
				case (int)eTypeIdoJiyu.Ido:
//					iIdoIdo.SelectedIndex = (int)eIdoShisetsu.None;
					iIdoKaiin.SelectedIndex = (int)eIdoKaiinHenko.None;
					iIdoEtc.SelectedIndex = (int)eIdoEtc.None;
					iIdoEtcmemo.Text = null;
					break;

				// 会員区分変更
				case (int)eTypeIdoJiyu.Kaiin:
					iIdoIdo.SelectedIndex = (int)eIdoShisetsu.None;
//					iIdoKaiin.SelectedIndex = (int)eIdoKaiinHenko.None;
					iIdoEtc.SelectedIndex = (int)eIdoEtc.None;
					iIdoEtcmemo.Text = null;
					break;

				// その他
				case (int)eTypeIdoJiyu.Etc:
					iIdoIdo.SelectedIndex = (int)eIdoShisetsu.None;
					iIdoKaiin.SelectedIndex = (int)eIdoKaiinHenko.None;
//					iIdoEtc.SelectedIndex = (int)eIdoEtc.None;
//					iIdoEtcmemo.Text = null;
					break;

				default:
					iIdoIdo.SelectedIndex = (int)eIdoShisetsu.None;
					iIdoKaiin.SelectedIndex = (int)eIdoKaiinHenko.None;
					iIdoEtc.SelectedIndex = (int)eIdoEtc.None;
					iIdoEtcmemo.Text = null;
					break;
			}

			/*
						// 空白チェック
						Control [] ctls =
						{
							iBankCode,	iShitenCode, iNameBank, iNameShiten, iNameKanaBank, iNameKanaShiten, iFullNameBank
						};

						foreach (Control ctl in ctls)
						{
							int len = ctl.Text.Length;

							if (ctl is UcTableComboBox)
							{
								len = ((UcTableComboBox)ctl).Text.Length;
							}

							if (len == 0)
							{
								ctl.Select();
								appToolTip.Show(ctl,AppToolTipIndex.CannotUseBlank);
								return false;
							}
						}
			*/
			// 重複チェック

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
