using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentGControlDB;
using ComponentIO;

namespace App
{
	/// <summary>
	/// スタッフ情報入力画面
	/// tachi
	/// </summary>
	public partial class FormStaff_DlgEntry:FormFrame
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
		public eMode Mode
		{
			get; set;
		}

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
		/// 取引先用レコード
		/// </summary>
		public DataRow Row
		{
			get
			{
				return editrow;
			}
			set
			{
				srcrow = value;
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
		public FormStaff_DlgEntry()
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

			this.Text += title;
			base.FormFrame_Load(sender, e);
		}

		/// <summary>
		/// ロード処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// 和暦・西暦
			iKoyomi.ExBeginUpdate();
			iKoyomi.ExAddItem("西暦", true);
			iKoyomi.ExAddItem("和暦", false);
			iKoyomi.ExEndUpdate();
			iKoyomi.ExSetSelectedIndexByValue(true);

			//　コンボボックスの作成
			AppCombo.SetComboBox(iSex, enumKbn.DSex);
			AppCombo.SetComboBox(iJob, enumKbn.DTypeJob);

			iUsed.ExBeginUpdate();
			iUsed.ExAddItem("在籍", true);
			iUsed.ExAddItem("退職", false);
			iUsed.ExEndUpdate();

			gctl = new GControlDB(this, getDataRow, AppGlobal.DB.DBZipCode);

			gctl.Add(new GControlDBText(t_staff.FCD_Staff, iCode));
			gctl.Add(new GControlDBCombo(t_staff.FSTF_Sex, iSex));
			gctl.Add(new GControlDBCombo(t_staff.FSTF_TypeJob, iJob));
			gctl.Add(new GControlDBCombo(t_staff.FID_Shokumu, iShokumu.ComboBox));

			gctl.Add(new GControlDBRuby(
							new string[] {
								t_staff.FSTF_Name,
								t_staff.FSTF_NameFurigane},
							new Control[] {
								iName,
								iNameFurigana
							}));

			gctl.Add(new GControlDBPostAddr(
							new string[]{
								t_staff.FSTF_Post,
								t_staff.FSTF_Addr1
							},
							new Control[]{
								iPost1,
								iPost2,
								iAddr1
							}));
			gctl.Add(new GControlDBText(t_staff.FSTF_NameOld, iNameOld));
			gctl.Add(new GControlDBText(t_staff.FSTF_Addr2, iAddr2));
			gctl.Add(new GControlDBHyphenSplit(t_staff.FSTF_Tel1, new Control[] { iTel1_1, iTel1_2, iTel1_3 }));
			gctl.Add(new GControlDBHyphenSplit(t_staff.FSTF_Tel2, new Control[] { iTel2_1, iTel2_2, iTel2_3 }));

			gctl.Add(new GControlDBCombo(t_staff.FSTF_Used, iUsed));
			gctl.Add(new GControlDBDate(t_staff.FSTF_DateBirthday, iBirthday.UcDateCtl));
			gctl.Add(new GControlDBDate(t_staff.FSTF_DateNyusha, iDateNyusha.UcDateCtl));
			gctl.Add(new GControlDBDate(t_staff.FSTF_DateTaishoku, iDateTaishoku.UcDateCtl));
			gctl.Add(new GControlDBText(t_staff.FSTF_Mail1, iMail1));
			gctl.Add(new GControlDBText(t_staff.FSTF_Mail2, iMail2));

			gctl.EndAdd(AppDbRule.Rule);

			rowFetch();

			iName.Select();

			//■ イベントの登録
			iJob.SelectedIndexChanged += iJob_SelectedIndexChanged;

			base.FormFrame_Shown(sender, e);
		}

		private void iKoyomi_SelectedIndexChanged(object sender, EventArgs e)
		{
			changedKoyomi();
		}

		void changedKoyomi()
		{
			iBirthday.ChangedKoyomiSeirekiOn(Cast.Bool(iKoyomi.ExGetValue()));
			iBirthday.Select();
		}

		private void iJob_SelectedIndexChanged(object sender, EventArgs e)
		{
			gctl.UpdateByControl(iJob);

			t_staff xrow = new t_staff(editrow);
			xrow.ID_Shokumu_Null = null;

			gctl.FetchByControl(iShokumu.ComboBox);
		}

		/// <summary>
		/// 行情報の取得
		/// </summary>
		void rowFetch()
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
					gctl.UpdateByControl(( (UcDate)this.ActiveControl ).UcDateCtl);
				}
				else
				if (this.ActiveControl is UcTableComboBox)
				{
					gctl.UpdateByControl(( (UcTableComboBox)this.ActiveControl ).ComboBox);
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
			appFuncKey = new AppFunctionKey(funckey, FuncStaff_DlgEntry.Functions);

			FuncStaff_DlgEntry.Save.Execute = saveClose;
			FuncStaff_DlgEntry.Cancel.Execute = closeCancel;

			bool enabled = false;

			//if (AppGlobal.LoginUser.AvailableMyno == true)
			////if (AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.Admin ||
			////  　AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.SU)
			//{
			//	enabled = true;
			//}

			appFuncKey.SetEnabled(FuncStaff_DlgEntry.ShowMyno.Key, enabled);
			appFuncKey.SetVisible(FuncStaff_DlgEntry.ShowMyno.Key, enabled);
		}

		/// <summary>
		/// 検証処理
		/// </summary>
		/// <returns></returns>
		bool validateRow()
		{
			// 最終フォーカスコントロールをRowへ反映
			updateCurrentControlValue(gctl);

			// 空白チェック
			Control[] ctls =
			{
				iCode, iName, iNameFurigana,
			};

			foreach (Control ctl in ctls)
			{
				int len = ctl.Text.Length;

				if (ctl is UcTableComboBox)
				{
					len = ( (UcTableComboBox)ctl ).Text.Length;
				}

				if (len == 0)
				{
					ctl.Select();
					appToolTip.Show(ctl, AppToolTipIndex.CannotUseBlank);
					return false;
				}
			}

			// 重複チェック
			Staff obj = AppGlobal.Staffs.GetCode(iCode.Text);

			if (obj != null && obj.ID != Cast.Int(editrow[t_staff.FID_Staff]))
			{
				iCode.Select();
				appToolTip.Show(iCode, AppToolTipIndex.BookingCode);
				return false;
			}

			// 日付範囲チェック


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
