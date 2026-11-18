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
	public partial class FormMain:FormFrame
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
				ib = _ib;
				method = _method;

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
			//this.Text = $"{AppConst.AppTitle} Version{AppConst.AppVersion}";
			//FileInfo fi = new FileInfo(Application.ExecutablePath);
			//lblVer.Text = $"Version:{AppConst.AppVersion}";//\r\nUpdate:{AppDate.GetDateSeirekiSlash(fi.LastWriteTime)}";
		}

		bool restart = false;

		/// <summary>
		/// 閉じる処理
		/// </summary>
		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			//if (restart == false)
			//{
			//	if (formCloseReason != FormCloseReason.Exec && AppMsgBox.Show(this, AppMsgBoxIndex.ShutDown) == DialogResult.No)
			//	{
			//		e.Cancel = true;
			//		return;
			//	}
			//}

			base.FormFrame_FormClosing(sender, e);
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			menuBtns = new List<MenuButton>();
			menuBtns.Add(new MenuButton(ibTantosha, showTanto));
			menuBtns.Add(new MenuButton(ibKaihi, showKaihi));
			//menuBtns.Add(new MenuButton(ibShokumu, showSelectorYM));
			//menuBtns.Add(new MenuButton(ibBasic, showSelectorYM));
			//menuBtns.Add(new MenuButton(ibBankCode, showSelectorYM));
			//menuBtns.Add(new MenuButton(ibShokumuPrice, showSelectorYM));
			//menuBtns.Add(new MenuButton(ibShikuchosaon, showSelectorYM));
			
			//menuBtns.Add(new MenuButton(ibFixedKyuyo, showSelectorYM));
			//menuBtns.Add(new MenuButton(ibOutKyuyoMeisai, showSelectorYM));
			//menuBtns.Add(new MenuButton(ibOutBankFile, showSelectorYM));
			//menuBtns.Add(new MenuButton(ibOutCheckKinmu, showSelectorYM));
			//menuBtns.Add(new MenuButton(ibOutCheckTeate, showSelectorYM));
			
			//menuBtns.Add(new MenuButton(ibNencho, showSelectorYM));
			//menuBtns.Add(new MenuButton(ibGensenchoshuhyo, showSelectorYM));
			//menuBtns.Add(new MenuButton(ibEtax, showSelectorYM));


			setTitle();
			setEnable();
			updateLable();

			//txtDummy.Select();


			base.FormFrame_Shown(sender, e);
		}

		void setEnable()
		{
			//bool enabled_su = false;
			////			if (AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.SU)
			//{
			//	enabled_su = true;
			//}
			//gbxDebug.Visible = enabled_su;
			//gbxDebugGensen.Visible = enabled_su;
		}

		void updateLable()
		{
			lblCompany.Text = $"{AppGlobal.Basic.BAS_Name}";

			
			iTantosha.Text	= $" {AppGlobal.LoginUser.CD}:{AppGlobal.LoginUser.Name}";
		}

		/// <summary>
		/// 遠隔リモート
		/// </summary>
		void execRemote()
		{
			//if (AppMsgBox.Show(AppMsgBoxIndex.StartRemoteSupport) == System.Windows.Forms.DialogResult.No)
			//{
			//	return;
			//}

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

		/// <summary>
		/// ユーザーマスタ
		/// </summary>
		void showTanto()
		{
			FormMasterTantosha frm = new FormMasterTantosha();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 会費マスタ
		/// </summary>
		void showKaihi()
		{
			initBankCode();

			FormKaiin frm = new FormKaiin();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 銀行コードデータの取得処理
		/// </summary>
		void initBankCode()
		{
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

		/// <summary>
		/// ログアウト
		/// </summary>
		void formClose()
		{
			this.Close();
		}


		private void prog_DoWorkEvent(object sender, DoWorkEventArgs e)
		{
			FormBg_Progress prog = (FormBg_Progress)sender;
			prog.AdvanceProgress(100);

			// 銀行コードは大量のデータになるので、スプラッシュ時ではなくログインしてから取得する
			AppGlobal.DB.GetReFillTable(TableProp.t_bank_code);
			AppGlobal.InitBankCodeMg();
			//			AppGlobal.Banks.Init(); // FBファイル作成に利用

		}
	}
}
