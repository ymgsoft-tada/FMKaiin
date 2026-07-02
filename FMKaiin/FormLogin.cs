using ComponentDB;
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
	/// ログイン
	/// </summary>
	public partial class FormLogin : FormFrame
	{
		DBView dvTnt;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormLogin()
		{
			InitializeComponent();

			this.Text = AppConst.AppTitle + " ＜ログイン＞";
		}

		/// <summary>
		/// 初回描画
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			appToolTip = new AppToolTip(this);

			iCode.Text = Properties.Settings.Default.LastTantosha;

			if (iCode.TextLength == 0)
			{
				iCode.Select();
			}
			else
			{
				iPwd.Select();
			}

			AppDbRule.SetControlByRule(iCode, t_tantosha.FCD_Tanto);

			btnLogin.Click += btnLogin_Click;

			// 自分自身にフォーカスさせる
			this.Activate();

//iCode.Text = "0";
//iPwd.Text = "adm";
//btnLogin.PerformClick();

			base.FormFrame_Shown(sender, e);
		}

		void btnLogin_Click(object sender, EventArgs e)
		{
			if (iCode.TextLength == 0)
			{
				appToolTip.Show(iCode, AppToolTipIndex.CannotUseBlank);
				iCode.Select();
			}
			else
			if (iPwd.TextLength  == 0)
			{
				appToolTip.Show(iPwd, AppToolTipIndex.CannotUseBlank);
				iPwd.Select();
			}
			else
			{
				// ログイン時に情報を担当者マスタ情報は取得する
				this.Cursor = Cursors.WaitCursor;

				dvTnt = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_tantosha));
				dvTnt.SortQuery(t_tantosha.FCD_Tanto);

				this.Cursor = Cursors.Default;

				if (dvTnt.FindRow(iCode.Text) == true)
				{
					t_tantosha xrow = new t_tantosha(dvTnt.CurrentRow);

					if (xrow.TNT_Password == iPwd.Text)
					{
						Properties.Settings.Default.LastTantosha = iCode.Text;
						Properties.Settings.Default.Save();

//						AppGlobal.SetLoginUser(xrow);

						formCloseReason = FormCloseReason.Exec;
						this.Close();
					}
					else
					{
						appToolTip.Show(iPwd, AppToolTipIndex.UnMatchPassword);
						iPwd.Select();
						iPwd.SelectAll();
					}
				}
				else
				{
					appToolTip.Show(iCode, AppToolTipIndex.NotFoundTantosha);
					iCode.Select();
					iCode.SelectAll();
				}
			}
		}
	}
}
