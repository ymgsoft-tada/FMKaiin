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

		protected override void OnShown(EventArgs e)
		{
			button1.Click += BtnIfax_Click; //¶ 2026/11/11 CC iFAXデータ出力
			btnMasterImport.Click += BtnMasterImport_Click; //¶ 2026/11/14 CC マスター取込
			btnRep1.Click += BtnRep1_Click; // 2026/11/19 帳票メニュー用1
			btnRep2.Click += BtnRep2_Click; // 2026/11/19 帳票メニュー用2

			base.OnShown(e);
		}

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
			menuBtns.Add(new MenuButton(ibIkaiKaihi, showIkaiKaihi));
			menuBtns.Add(new MenuButton(ibBasic, showBasic));
			menuBtns.Add(new MenuButton(ibBankCode, showBank));
			menuBtns.Add(new MenuButton(ibShinryoKamoku, showShinryoKamoku));
			menuBtns.Add(new MenuButton(ibIryoKikan, showIryoKikan));

			menuBtns.Add(new MenuButton(ibHikiotoshimeisai, showHikiotoshimeisai));
			menuBtns.Add(new MenuButton(ibSeikyusho, showSeikyusho));
			menuBtns.Add(new MenuButton(ibLabel, showLabel));
			menuBtns.Add(new MenuButton(ibHikiotoshiData, showHikiotoshiData));
			menuBtns.Add(new MenuButton(ibFB, showFB));

			menuBtns.Add(new MenuButton(ibKaiinjohoToroku, showKaiinjohoToroku));
			menuBtns.Add(new MenuButton(ibOutKaiinjoho, showOutKaiinjoho));
			menuBtns.Add(new MenuButton(ibOutKaihi, showOutKaihi));


			setTitle();
			setEnable();
			updateLable();

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
		/// 基本情報
		/// </summary>
		void showBasic()
		{
			// 銀行コードテーブルの取得
			initBankCode();

			FormMasterBasic frm = new FormMasterBasic();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 銀行マスタ
		/// </summary>
		void showBank()
		{
			FormMasterBankCode frm = new FormMasterBankCode();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 診療科目マスタ
		/// </summary>
		void showShinryoKamoku()
		{
			FormMasterShinryoka frm = new FormMasterShinryoka();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 医療機関マスタ
		/// </summary>
		void showIryoKikan()
		{
			FormMasterIryoKikan frm = new FormMasterIryoKikan();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 医会会費マスタ
		/// </summary>
		void showIkaiKaihi()
		{
			// 銀行コードテーブルの取得
			initBankCode();

			FormMasterKaihi frm = new FormMasterKaihi();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 引落明細表
		/// </summary>
		void showHikiotoshimeisai()
		{

		}

		/// <summary>
		/// 請求書
		/// </summary>
		void showSeikyusho()
		{

		}

		/// <summary>
		/// ラベル印刷
		/// </summary>
		void showLabel()
		{

		}

		/// <summary>
		/// 引落データ作成
		/// </summary>
		void showHikiotoshiData()
		{
			FormHikiotoshi frm = new FormHikiotoshi();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// FBデータ作成
		/// </summary>
		void showFB()
		{
			initBankCode();

			FormCreateBankFile frm = new FormCreateBankFile();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 会員情報登録
		/// </summary>
		void showKaiinjohoToroku()
		{
			// 銀行コードテーブルの取得 毎回いらない？検討
			initBankCode();

			FormKaiin frm = new FormKaiin();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 会員情報出力
		/// </summary>
		void showOutKaiinjoho()
		{

		}

		/// <summary>
		/// 会費情報出力
		/// </summary>
		void showOutKaihi()
		{

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


		private void BtnMasterImport_Click(object sender, EventArgs e)
		{
			FormMasterImport frm = new FormMasterImport();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		private void BtnIfax_Click(object sender, EventArgs e)
		{
			FormCreateIfaxFile frm = new FormCreateIfaxFile();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		private void BtnRep1_Click(object sender, EventArgs e)
		{
			// 銀行コードテーブルの取得 時間かかるか知らないがもうGrobalでInitでいいのでは・・・？
			initBankCode();

			FormReportMenu1 frm = new FormReportMenu1();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		private void BtnRep2_Click(object sender, EventArgs e)
		{
			// 銀行コードテーブルの取得
			initBankCode();

			FormReportMenu2 frm = new FormReportMenu2();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
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
