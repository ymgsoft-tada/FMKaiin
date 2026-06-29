using C1.Win.C1TrueDBGrid;
using ComponentDB;
using ComponentDebug;
using ComponentGGridDB;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 kj]
	/// ログ表示画面
	/// </summary>
	public partial class FormLogView : FormFrame
	{
		GGridDBCommon gcom;

		public enum eLogType
		{
			/// <summary>実行結果</summary>
			Result,
			/// <summary>検証結果</summary>
			TestResult,
		}

		/// <summary>
		/// ログの種類
		/// </summary>
		public eLogType LogType { get; set; }

		/// <summary>
		/// ログ用のテーブル
		/// </summary>
		public DataTable Table { get; set; }

		/// <summary>
		/// タイトルラベル
		/// </summary>
		public string TitleText
		{
			get { return lblTitle.Text; }
			set { lblTitle.Text = value; }
		}

		/// <summary>
		/// 処理月度
		/// </summary>
		public DateTime SelectedYM { get; set; }

		DBView dv;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormLogView()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			dv = new DBView(Table);

			AppGridCommon.StyleSet(grid);

			grid.Font				= new Font(grid.Font.Name, 10);
			grid.MarqueeStyle		= C1.Win.C1TrueDBGrid.MarqueeEnum.NoMarquee;
			grid.RecordSelectors	= false;
			grid.HScrollBar.Style   = C1.Win.C1TrueDBGrid.ScrollBarStyleEnum.Always;

			gcom = new GGridDBCommon(grid, this);

			gcom.Add(new GGridDBNumber(LogTable.F_No, "No", 50));
			gcom.SetLocked(true);
			gcom.Add(new GGridDBText(LogTable.F_Type, "状況", 50, GGridDBCellDisp.Center));
			gcom.SetUnboundColumnFetch(ubType);
			gcom.SetLocked(true);
			gcom.Add(new GGridDBText(LogTable.F_Message, "内容",500));
			gcom.SetLocked(true);

			gcom.SetRowStyles(rowStyle);

			gcom.EndAdd(dv);

			btnClose.Click += btnOK_Click;
			btnExec.Click += btnExec_Click;
			btnCancel.Click += btnOK_Click;
			btnLog.Click += btnLog_Click;

			btnClose.Select();

			if (LogType == eLogType.Result)
			{
				btnExec.Visible		= false;
				btnCancel.Visible	= false;
			}
			else
			if (LogType == eLogType.TestResult)
			{
				btnClose.Visible = false;
			}

			base.FormFrame_Shown(sender, e);
		}

		private void btnExec_Click(object sender, EventArgs e)
		{
			if (AppMsgBox.Show(this, AppMsgBoxIndex.ExecImportCSV) == DialogResult.Yes)
			{
				formCloseReason = FormCloseReason.Exec;
				this.Close();
			}
		}

		private void btnLog_Click(object sender, EventArgs e)
		{
			SaveFileDialog	fd		= new SaveFileDialog();

			// はじめに表示されるフォルダ
			string	inipath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			
			fd.InitialDirectory = inipath;
			fd.FileName			= $"FBログ_{AppDate.GetYearMonth(SelectedYM)}分";
			// [ファイルの種類]に表示される選択肢を指定する
			fd.Filter = string.Format("csvファイル|*.csv");
			fd.FilterIndex = 0;
			fd.Title = "ログファイルの保存先を指定してください。";
			fd.RestoreDirectory = true;
			
			// ダイアログを表示
			if (fd.ShowDialog() == DialogResult.OK)
			{
				try
				{
					grid.ExportToDelimitedFile(fd.FileName, RowSelectorEnum.AllRows, ",", "", "", true,"shift_jis");

					AppMsgBox.Show(this, AppMsgBoxIndex.SuccessExportLog);
				}
				catch(Exception ex)
				{
					ErrLog.WriteException(ex);
					AppMsgBox.Show(this, AppMsgBoxIndex.FailedExportLogReasonSave);
				}
			}
		}

		/// <summary>
		/// 閉じる時の処理
		/// </summary>
		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (LogType == eLogType.TestResult && formCloseReason != FormCloseReason.Exec)
			{
				if (AppMsgBox.Show(this, AppMsgBoxIndex.CancelImportCSV) == DialogResult.No)
				{
					e.Cancel = true;
				}
			}

			base.FormFrame_FormClosing(sender, e);
		}


		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		string	ubType(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			LogTable.eStatus sts = Cast.Enumelate<LogTable.eStatus>(dv[e.Row][LogTable.F_Type], LogTable.eStatus.None);

			string ret = "";

			switch(sts)
			{
				case LogTable.eStatus.Success	: ret = "〇"; break;
				case LogTable.eStatus.Warning	: ret = "△"; break;
				case LogTable.eStatus.Error		: ret = "×"; break;
			}

			return ret;
		}

		void rowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{
			LogTable.eStatus sts = Cast.Enumelate<LogTable.eStatus>(dv[e.Row][LogTable.F_Type], LogTable.eStatus.None);

			Color color = Color.Black;

			switch(sts)
			{
				case LogTable.eStatus.Success	: color = Color.Blue; break;
				case LogTable.eStatus.Error		: color = Color.Red; break;
			}

			e.CellStyle.ForeColor = color;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			formCloseReason = FormCloseReason.Save;
			this.Close();
		}
	}
}
