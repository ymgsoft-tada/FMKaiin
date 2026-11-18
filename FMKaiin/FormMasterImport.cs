using ComponentForm;
using ComponentIO;
using System;
using System.IO;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// マスター取込（デバッグ用画面）
	/// 医療機関・会費・会員情報のExcelファイルを指定して取り込む。
	/// </summary>
	public partial class FormMasterImport : FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMasterImport()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			btnIryoKikan.Click += BtnIryoKikan_Click;
			btnKaihi.Click += BtnKaihi_Click;
			btnKaiin.Click += BtnKaiin_Click;
			btnClose.Click += BtnClose_Click;

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// 医療機関マスターの取込
		/// </summary>
		private void BtnIryoKikan_Click(object sender, EventArgs e)
		{
			string path = Cast.String(iFileIryoKikan.UcText).Trim();

			if (path == "")
			{
				appToolTip.Show(iFileIryoKikan, AppToolTipIndex.CannotUseBlank);
				return;
			}

			if (File.Exists(path) == false)
			{
				AppMsgBox.Show(this, AppMsgBoxIndex.NotExistFile);
				return;
			}

			//¶ 2026/11/14 CC デバッグ用画面のため、確認メッセージはAppMsgBoxに定義せず直接表示する
			string msg = "医療機関マスターの取込を実行します。よろしいですか？";
			MessageBoxIcon icon = MessageBoxIcon.Question;

			if (chkClearIryoKikan.Checked == true)
			{
				msg = "登録済みの医療機関データを全て削除してから、取込を実行します。\r\n削除すると元には戻せません。よろしいですか？";
				icon = MessageBoxIcon.Warning;
			}

			if (MsgBox.Show(this, msg, this.Text, MessageBoxButtons.YesNo, icon) != DialogResult.Yes)
			{
				return;
			}

			AppExcelImportIryoKikan imp = new AppExcelImportIryoKikan();
			imp.ClearAll = chkClearIryoKikan.Checked;
			imp.Execute(path);

			showLog(imp.Log, "医療機関マスター取込結果");
		}

		/// <summary>
		/// 会費マスターの取込
		/// </summary>
		private void BtnKaihi_Click(object sender, EventArgs e)
		{
			string path = Cast.String(iFileKaihi.UcText).Trim();

			if (path == "")
			{
				appToolTip.Show(iFileKaihi, AppToolTipIndex.CannotUseBlank);
				return;
			}

			if (File.Exists(path) == false)
			{
				AppMsgBox.Show(this, AppMsgBoxIndex.NotExistFile);
				return;
			}

			string msg = "会費マスターの取込を実行します。よろしいですか？";
			MessageBoxIcon icon = MessageBoxIcon.Question;

			if (chkClearKaihi.Checked == true)
			{
				//¶ 2026/11/14 CC 口座情報・会費属性はExcelにないため、クリアすると取込では戻らない
				msg = "取り込むファイルと同じ会費区分の会費データを全て削除してから、取込を実行します。\r\n"
					+ "口座情報・会費属性も削除され、元には戻せません。よろしいですか？";
				icon = MessageBoxIcon.Warning;
			}

			if (MsgBox.Show(this, msg, this.Text, MessageBoxButtons.YesNo, icon) != DialogResult.Yes)
			{
				return;
			}

			AppExcelImportKaihi imp = new AppExcelImportKaihi();
			imp.ClearAll = chkClearKaihi.Checked;
			imp.Execute(path);

			showLog(imp.Log, "会費マスター取込結果");
		}

		/// <summary>
		/// 会員情報の取込
		/// </summary>
		private void BtnKaiin_Click(object sender, EventArgs e)
		{
			string path = Cast.String(iFileKaiin.UcText).Trim();

			if (path == "")
			{
				appToolTip.Show(iFileKaiin, AppToolTipIndex.CannotUseBlank);
				return;
			}

			if (File.Exists(path) == false)
			{
				AppMsgBox.Show(this, AppMsgBoxIndex.NotExistFile);
				return;
			}

			string msg = "会員情報の取込を実行します。よろしいですか？";
			MessageBoxIcon icon = MessageBoxIcon.Question;

			if (chkClearKaiin.Checked == true)
			{
				//¶ 2026/11/14 CC 参加医会・会費調整・引落データはExcelにないため、クリアすると取込では戻らない
				msg = "登録済みの会員データを全て削除してから、取込を実行します。\r\n"
					+ "会員の参加医会・会費調整・引落データも削除され、元には戻せません。よろしいですか？";
				icon = MessageBoxIcon.Warning;
			}

			if (MsgBox.Show(this, msg, this.Text, MessageBoxButtons.YesNo, icon) != DialogResult.Yes)
			{
				return;
			}

			AppExcelImportKaiin imp = new AppExcelImportKaiin();
			imp.ClearAll = chkClearKaiin.Checked;
			imp.FillIryoKikan = chkFillIryoKikan.Checked;
			imp.Execute(path);

			showLog(imp.Log, "会員情報取込結果");
		}

		/// <summary>
		/// 取込結果のログを表示します。
		/// </summary>
		/// <param name="log">ログ</param>
		/// <param name="title">タイトル</param>
		void showLog(LogTable log, string title)
		{
			FormLogView frm = new FormLogView();
			frm.LogType = FormLogView.eLogType.Result;
			frm.Table = log.GetTable();
			frm.TitleText = title;
			frm.SelectedYM = DateTime.Now;
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		private void BtnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
