using ComponentIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentSUI;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// MAMISデータ取込（取込CSVの指定）
	/// </summary>
	public partial class FormMamisImport : FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMamisImport()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			iFile.Select();

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの登録
		/// </summary>
		protected override void SetFunction()
		{
			funcKeys = new FunctionKeyBinder(funckey, FuncMamisImport.Functions);

			funcKeys.On(FuncMamisImport.History, showHistory);
			funcKeys.On(FuncMamisImport.Exec, doExec);
			funcKeys.On(FuncMamisImport.Close, formClose);
		}

		/// <summary>
		/// 取込開始
		/// </summary>
		void doExec()
		{
			string path = Cast.String(iFile.UcText).Trim();

			if (path == "")
			{
				appToolTip.Show(iFile, AppToolTipIndex.CannotUseBlank);
				return;
			}

			if (File.Exists(path) == false)
			{
				AppMsgBox.Show(this, AppMsgBoxIndex.NotExistFile);
				return;
			}

			//¶ 2026/11/21 CC 読込と会員との照合。この時点ではデータベースを更新しない
			AppMamisImport imp = new AppMamisImport();

			switch (imp.Load(path))
			{
				case AppMamisImport.eResult.Success:
					break;

				case AppMamisImport.eResult.NoData:
					AppMsgBox.Show(this, AppMsgBoxIndex.NotFoundMamisImportData);
					return;

				case AppMamisImport.eResult.NotMamisFile:
					AppMsgBox.Show(this, AppMsgBoxIndex.NotMamisImportFile);
					return;

				case AppMamisImport.eResult.OpenFileFailed:
					AppMsgBox.Show(this, AppMsgBoxIndex.AnythingMessage, "対象外のファイルか、他で使用中の為ファイルをオープンできません。");
					return;

				default:
					AppMsgBox.Show(this, AppMsgBoxIndex.FailedMamisImport);
					return;
			}

			//¶ 2026/11/21 CC 取込確認
			FormMamisImport_DlgConfirm frm = new FormMamisImport_DlgConfirm();
			frm.Import = imp;
			frm.ShowDialog();
			bool exec = (frm.FormCloseReason == FormCloseReason.Exec);
			frm.Dispose();
			frm = null;

			if (exec == false)
			{
				return;
			}

			//¶ 2026/11/21 CC 取込処理
			if (imp.Update() != AppMamisImport.eResult.Success)
			{
				AppMsgBox.Show(this, AppMsgBoxIndex.FailedMamisImport);
				return;
			}

			//¶ 2026/11/21 CC 取込結果
			showLog(imp.LogID);
		}

		/// <summary>
		/// 履歴
		/// </summary>
		void showHistory()
		{
			showLog(null);
		}

		/// <summary>
		/// 取込結果・取込履歴を表示します。
		/// </summary>
		/// <param name="logid">取込履歴ID。nullの場合は全ての履歴</param>
		void showLog(int? logid)
		{
			FormMamisImport_DlgLog frm = new FormMamisImport_DlgLog();
			frm.LogID = logid;
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		void formClose()
		{
			this.Close();
		}
	}
}
