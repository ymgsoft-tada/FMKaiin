using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentDB;
using ComponentGControlDB;
using ComponentIO;
using GControlGcTextBoxEx;

namespace App
{
	/// <summary>
	/// 担当者マスタ編集画面
	/// tachi
	/// </summary>
	public partial class FormMasterTantosha_DlgEntry:FormFrame
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
		public FormMasterTantosha_DlgEntry()
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
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// コンボの作成
			AppCombo.SetComboBox(iAuth, enumKbn.DAuth, (int)eAuth.None, (int)eAuth.SU);
			AppCombo.SetComboBox(iShinryojo, enumKbn.DTypeShinryojo);

			gctl = new GControlDB(this, getDataRow);
			gctl.Add(new GControlDBText(t_tantosha.FCD_Tanto, iCode));
			gctl.Add(new GControlDBText(t_tantosha.FTNT_Name, iName));
			gctl.Add(new GControlDBText(t_tantosha.FTNT_Password, iPwd));
			gctl.Add(new GControlDBCombo(t_tantosha.FTNT_Auth, iAuth));
			gctl.Add(new GControlDBCombo(t_tantosha.FTNT_TypeShinryojo, iShinryojo));
			gctl.EndAdd(AppDbRule.Rule);

			rowFetch();

			iCode.Select();

			//■ イベントの登録
			iAuth.SelectedValueChanged += iAuth_SelectedValueChanged;

			base.FormFrame_Shown(sender, e);
		}

		private void iAuth_SelectedValueChanged(object sender, EventArgs e)
		{
			gctl.UpdateByControl(iAuth);
		}

		/// <summary>
		/// 行情報の取得
		/// </summary>
		void rowFetch()
		{
			if (Row != null)
			{
				gctl.FetchAll();

				if (Mode == eMode.Edit)
				{
					iPwd.Enabled = false;
				}
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
			appFuncKey = new AppFunctionKey(funckey, FuncMasterTanto_DlgEntry.Functions);

			if (Mode == eMode.Add)
			{
				appFuncKey.SetVisible(FuncMasterTanto_DlgEntry.ChangePWD.Key, false);
			}

			FuncMasterTanto_DlgEntry.ChangePWD.Execute = changePWD;
			FuncMasterTanto_DlgEntry.Save.Execute = saveClose;
			FuncMasterTanto_DlgEntry.Cancel.Execute = closeCancel;
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
			GcTextBoxEx[] ctls =
			{
				iCode, iName, iPwd,
			};

			foreach (GcTextBoxEx ctl in ctls)
			{
				if (ctl.TextLength == 0)
				{
					ctl.Select();
					appToolTip.Show(ctl, AppToolTipIndex.CannotUseBlank);
					return false;
				}
			}

			// 重複チェック
			Tanto obj = AppGlobal.Tantos.GetCode(iCode.Text);

			if (obj != null && obj.ID != Cast.Int(editrow[t_tantosha.FID_Tanto]))
			{
				iCode.Select();
				appToolTip.Show(iCode, AppToolTipIndex.BookingCode);
				return false;
			}

			// 管理者以外はマイナンバー管理者にはなれない。
			t_tantosha xrow = new t_tantosha(editrow);

			if (xrow.TNT_Auth != eAuth.Admin && xrow.TNT_AvailableMyNo == true)
			{
				xrow.TNT_AvailableMyNo = false;
			}

			return true;
		}

		void changePWD()
		{
			t_tantosha xrow = new t_tantosha(editrow);

			FormMasterTantosha_DlgPwd frm = new FormMasterTantosha_DlgPwd();
			frm.TantoRow = xrow;
			frm.ShowDialog();
			if (frm.FormCloseReason == FormCloseReason.Save)
			{
				xrow.TNT_Password = frm.NewPassword;

				gctl.FetchByField(t_tantosha.FTNT_Password);
			}
			frm.Dispose();
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
