using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 kj]
	/// メイン画面
	/// </summary>
	public partial class FormMain : FormFrame
	{
		List<MenuButton> menuBtns;

		public class MenuButton
		{
			/// <summary>
			/// 
			/// </summary>
			public delegate void ButtonClickMethod();

			UcImgButton ib;
			ButtonClickMethod method;

			/// <summary>
			/// コンストラクタ
			/// </summary>
			public MenuButton(UcImgButton _ib, ButtonClickMethod _method)
			{
				ib		= _ib;
				method	= _method;

				setEvent();
			}

			void setEvent()
			{
				ib.ClickEvent += ib_ClickEvent;
			}

			/// <summary>
			/// ボタンの実行処理
			/// </summary>
			private void ib_ClickEvent(object sender, CancelEventArgs e)
			{
				method?.Invoke();
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMain()
		{
			EnterFocus = false;

			InitializeComponent();
		}

		/// <summary>
		/// タイトル設定
		/// </summary>
		void setTitle()
		{
			this.Text = $"{AppConst.AppTitle} Version{AppConst.AppVersion}";
			FileInfo fi = new FileInfo(Application.ExecutablePath);
			lblVer.Text = $"Version:{AppConst.AppVersion}";//\r\nUpdate:{AppDate.GetDateSeirekiSlash(fi.LastWriteTime)}";
		}

		bool restart = false;

		/// <summary>
		/// 閉じる処理
		/// </summary>
		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (restart == false)
			{
				if (formCloseReason != FormCloseReason.Exec && AppMsgBox.Show(this, AppMsgBoxIndex.ShutDown) == DialogResult.No)
				{
					e.Cancel = true;
					return;
				}
			}

			base.FormFrame_FormClosing(sender, e);
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			menuBtns = new List<MenuButton>();
			//menuBtns.Add(new MenuButton(ibSelectYM, showSelectorYM));
			//menuBtns.Add(new MenuButton(ibCalendar, showCalendar));
			//menuBtns.Add(new MenuButton(ibKinmubo, showKinmubo));
			//menuBtns.Add(new MenuButton(ibStaff, showStaff));
			//menuBtns.Add(new MenuButton(ibOutCheckKinmu, outCheckKinmu));
			//menuBtns.Add(new MenuButton(ibOutCheckTeate, outCheckTeate));
			//menuBtns.Add(new MenuButton(ibFixedKyuyo, showFixedKyuyo));
			//menuBtns.Add(new MenuButton(ibOutKyuyoMeisai, outKyuyoMeisai));
			//menuBtns.Add(new MenuButton(ibOutBankFile, outBankFile));

			//menuBtns.Add(new MenuButton(ibNencho, showNencho));
			//menuBtns.Add(new MenuButton(ibGensenchoshuhyo, outGensenchoshuhyo));
			//menuBtns.Add(new MenuButton(ibEtax, outEtaxEltax));

			//menuBtns.Add(new MenuButton(ibBasic, showBasic));
			//menuBtns.Add(new MenuButton(ibShokumu, showShokumu));
			//menuBtns.Add(new MenuButton(ibShokumuPrice, showShokumuPrice));
			//menuBtns.Add(new MenuButton(ibGensen, showGensen));
			//menuBtns.Add(new MenuButton(ibTantosha, showTantosha));
			//menuBtns.Add(new MenuButton(ibJigyo, showJigyo));
			//menuBtns.Add(new MenuButton(ibTeateTanka, showTeateTanka));
			//menuBtns.Add(new MenuButton(ibShikuchosaon, showShikuchoson));
			//menuBtns.Add(new MenuButton(ibBankCode, showBankCode));

			//menuBtns.Add(new MenuButton(ibBackup, dataBackup));
			//menuBtns.Add(new MenuButton(ibRestore, dataRestore));
			//menuBtns.Add(new MenuButton(ibRemote, execRemote));
			//menuBtns.Add(new MenuButton(ibLogout, formClose));

			setTitle();
			setEnable();
			updateLable();

			txtDummy.Select();


			base.FormFrame_Shown(sender, e);
		}

		void setEnable()
		{
			bool enabled_su = false;
//			if (AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.SU)
			{
				enabled_su = true;
			}
			gbxDebug.Visible		= enabled_su;
			gbxDebugGensen.Visible	= enabled_su;
		}

		void updateLable()
		{
			lblCompany.Text = $"{AppGlobal.Basic.BAS_Name}";

			iDateYM.Text	= $"{AppDate.GetYearMonth(AppGlobal.Basic.BAS_DateYM)}";
//			iTantosha.Text	= $" {AppGlobal.LoginUser.CD}:{AppGlobal.LoginUser.Name}";
		}

		/// <summary>
		/// 遠隔リモート
		/// </summary>
		void execRemote()
		{
			if (AppMsgBox.Show(AppMsgBoxIndex.StartRemoteSupport) == System.Windows.Forms.DialogResult.No)
			{
				return;
			}

			try
			{
				Process.Start(AppConst.RemoteSupportFilePath);
			}
			catch
			{
				// 実行キャンセルでエラーとなる場合がある。
			}
		}

		/// <summary>
		/// データ復元
		/// </summary>
		void dataRestore()
		{
			string path = AppDb.ShowOpenFilePath(Properties.Settings.Default.BackUpDir);

			if (path != null)
			{
				restart = false;

				AppMsgBoxIndex msg;

				Cursor = Cursors.WaitCursor;

				if (AppGlobal.DB.RestoreFromZip(path) == true)
				{
					msg = AppMsgBoxIndex.RestoreSuccess;
					restart = true;
				}
				else
				{
					msg = AppMsgBoxIndex.RestoreFailed;
				}

				AppMsgBox.Show(msg);

				if (restart == true)
				{
					Application.Restart();
				}
			}
		}
		/// <summary>
		/// データ保存
		/// </summary>
		void dataBackup()
		{
			string file = string.Format("会員管理データ_{0}.{1}", DateTime.Now.ToString("yyMMdd_HHmm"), AppConst.BackUpFileExt);

			if (Properties.Settings.Default.BackUpDir != "")
			{
				file = string.Format(@"{0}\{1}", Properties.Settings.Default.BackUpDir, file);
			}

			string path = AppDb.ShowSaveFilePath(file);

			if (path != null)
			{
				AppMsgBoxIndex msg;

				Cursor = Cursors.WaitCursor;

				if (AppGlobal.DB.BackupToZip(path) == true)
				{
					msg = AppMsgBoxIndex.BackupSuccess;
					Properties.Settings.Default.BackUpDir = Path.GetDirectoryName(path);
					Properties.Settings.Default.Save();
				}
				else
				{
					msg = AppMsgBoxIndex.BackupFailed;
				}

				Cursor = Cursors.Default;

				AppMsgBox.Show(msg);
			}
		}

		///// <summary>
		///// 手当単価マスタの表示
		///// </summary>
		//void showTeateTanka()
		//{
		//	if (checkAuthAdmin() == true)
		//	{
		//		FormMasterTeate frm = new FormMasterTeate();
		//		frm.ShowDialog();
		//		frm.Dispose();
		//		frm = null;
		//	}
		//}

		/// <summary>
		/// ログアウト
		/// </summary>
		void formClose()
		{
			this.Close();
		}

		bool checkAuthAdmin()
		{
			bool ret = false;

			//if (AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.Admin ||
			//	AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.SU)
			//{
			//	ret = true;
			//}
			//else
			{
				AppMsgBox.Show(this, AppMsgBoxIndex.HavenotAuthAdmin);
			}

			return ret;
		}
	}
}
