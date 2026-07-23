using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GControlGcTextBoxEx;

namespace App
{
	public partial class FormMasterTantosha_DlgPwd:FormFrame
	{
		/// <summary>
		/// 取引先用レコード
		/// </summary>
		public t_tantosha TantoRow
		{
			get; set;
		}

		/// <summary>
		/// 新しいパスワード
		/// </summary>
		public string NewPassword
		{
			get
			{
				return iPwd_New.Text;
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMasterTantosha_DlgPwd()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{

			AppDbRule.SetControlByRule(iPwd_Old, t_tantosha.FTNT_Password);
			AppDbRule.SetControlByRule(iPwd_New, t_tantosha.FTNT_Password);
			AppDbRule.SetControlByRule(iPwd_New2, t_tantosha.FTNT_Password);

			iPwd_Old.Select();

			//■ イベントの登録


			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncMasterTanto_DlgPwd.Functions);

			FuncMasterTanto_DlgPwd.Save.Execute = saveClose;
			FuncMasterTanto_DlgPwd.Cancel.Execute = closeCancel;
		}

		/// <summary>
		/// 検証処理
		/// </summary>
		/// <returns></returns>
		bool validateRow()
		{
			// 空白チェック
			GcTextBoxEx[] ctls =
			{
				iPwd_Old, iPwd_New, iPwd_New2,
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

			if (iPwd_Old.Text != TantoRow.TNT_Password)
			{
				iPwd_Old.Select();
				appToolTip.Show(iPwd_Old, AppToolTipIndex.UnMatchOldPassword);
				return false;
			}

			if (iPwd_New.Text != iPwd_New2.Text)
			{
				iPwd_New2.Select();
				appToolTip.Show(iPwd_New2, AppToolTipIndex.UnMatchPassword2);
				return false;
			}

			if (AppMsgBox.Show(this, AppMsgBoxIndex.ConfirmChangePassword) == DialogResult.No)
			{
				return false;
			}

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
	}
}
