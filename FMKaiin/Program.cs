using ComponentDebug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
	static class Program
	{
		private static System.Threading.Mutex _mutex;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//■ 二重起動をチェックする。
			_mutex = new System.Threading.Mutex(false, AppConst.AppName);
			
			// ミューテックスの所有権を要求する。
			if (_mutex.WaitOne(0, false) == false)
			{
				// 既に開いているプロセスをアクティブに。
				ShowPrevProcess();
				return;
			}

			ErrLog.ErrorLogFilePath = @"err.log";
			ErrLog.Initialize();

			try
			{
				Application.Run(new Form1());

				//DlgSplash2 splash = new DlgSplash2();
				//splash.Show();
				//splash.Cursor = Cursors.WaitCursor;

				//if (AppGlobal.InitDB() == false)
				//{
				//	Application.Restart();
				//	return;
				//}

				//AppGlobal.Init();

				//// スプラッシュを閉じる
				//if (splash != null)
				//{
				//	splash.Cursor = Cursors.Default;
				//	splash.Dispose();
				//	splash = null;
				//}

				//FormLogin login = new FormLogin();
				//login.ShowDialog();

				//if (login.FormCloseReason == FormCloseReason.Exec)
				//{
				//	//Form1 frm = new Form1();
				//	FormMain frm = new FormMain();

				//	// メイン画面を最前面へ
				//	AppApi.SetForegroundWindow(frm.Handle);
				//	Application.Run(frm);
				//}
			}
			catch(Exception ex)
			{
				AppMsgBox.Show(AppMsgBoxIndex.UnKnownError);
				ErrLog.WriteException(ex);
			}
			finally
			{
				AppGlobal.Finish();
			}
		}

		/// <summary>
		/// 既に開いているプロセスをアクティブに。
		/// </summary>
		static bool ShowPrevProcess()
		{
			Process hThisProcess	= Process.GetCurrentProcess();
			Process[] hProcesses	= Process.GetProcessesByName(hThisProcess.ProcessName);
			int iThisProcessId		= hThisProcess.Id;
			
			foreach (Process hProcess in hProcesses)
			{
				if (hProcess.Id != iThisProcessId)
				{
					AppApi.ShowWindow(hProcess.MainWindowHandle, AppApi.SW_NORMAL);
					AppApi.SetForegroundWindow(hProcess.MainWindowHandle);
					return true;
				}
			}
			
			return false;
		}
	}
}
