using C1.Win.C1TrueDBGrid;
using ComponentDB;
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
	/// [作成者 CC]
	/// MAMISデータ取込（取込確認）
	/// 上段に会員ごとの照合結果、下段に選択中の会員の取込内容を表示します。
	/// </summary>
	public partial class FormMamisImport_DlgConfirm : FormFrame
	{
		/// <summary>
		/// 読込済みの取込内容
		/// </summary>
		public AppMamisImport Import { get; set; }

		GGridDBCommon gcomHead;
		GGridDBCommon gcomDetail;

		DBView dvHead;
		DBView dvDetail;
		DataTable tblDetail;

		const string F_No = "F_No";
		const string F_Status = "F_Status";
		const string F_StatusType = "F_StatusType";
		const string F_Code = "F_Code";
		const string F_Name = "F_Name";
		const string F_Iseki = "F_Iseki";
		const string F_Shinsei = "F_Shinsei";
		const string F_UpdateDate = "F_UpdateDate";
		const string F_Count = "F_Count";
		const string F_Content = "F_Content";

		const string F_Item = "F_Item";
		const string F_Type = "F_Type";
		const string F_Before = "F_Before";
		const string F_After = "F_After";

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMamisImport_DlgConfirm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// 上段：会員ごとの照合結果
			DataTable tbl = new DataTable();
			tbl.Columns.Add(F_No, typeof(int));
			tbl.Columns.Add(F_Status, typeof(string));
			tbl.Columns.Add(F_StatusType, typeof(int));
			tbl.Columns.Add(F_Code, typeof(string));
			tbl.Columns.Add(F_Name, typeof(string));
			tbl.Columns.Add(F_Iseki, typeof(string));
			tbl.Columns.Add(F_Shinsei, typeof(string));
			tbl.Columns.Add(F_UpdateDate, typeof(string));
			tbl.Columns.Add(F_Count, typeof(string));
			tbl.Columns.Add(F_Content, typeof(string));

			foreach (AppMamisImport.Person p in Import.Persons)
			{
				DataRow nrow = tbl.NewRow();

				nrow[F_No] = p.No;
				nrow[F_Status] = p.StatusText;
				nrow[F_StatusType] = (int)p.Status;
				nrow[F_Code] = (p.KaiinCD > 0) ? p.KaiinCD.ToString().PadLeft(6, '0') : "";
				nrow[F_Name] = p.Name;
				nrow[F_Iseki] = p.IsekiNo;
				nrow[F_Shinsei] = p.Shinsei;
				nrow[F_UpdateDate] = (p.UpdateDate.Length > 16) ? p.UpdateDate.Substring(0, 16) : p.UpdateDate; //¶ 2026/11/21 CC 秒は表示しない
				nrow[F_Count] = (p.KaiinID > 0) ? p.Items.Count.ToString() : "";
				nrow[F_Content] = p.ContentText;

				tbl.Rows.Add(nrow);
			}
			dvHead = new DBView(tbl);

			//¶ 2026/11/21 CC 横スクロールしないよう、固定幅の合計をグリッド幅に収め、最後の列（幅0）を残り幅に広げる
			AppGridCommon.StyleSet(gridHead);
			gridHead.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;

			gcomHead = new GGridDBCommon(gridHead, this);
			gcomHead.Add(new GGridDBNumber(F_No, "No", 50));
			gcomHead.SetLocked(true);
			gcomHead.Add(new GGridDBText(F_Status, "状況", 165, GGridDBCellDisp.Center));
			gcomHead.SetLocked(true);
			gcomHead.Add(new GGridDBText(F_Code, "会員コード", 80, GGridDBCellDisp.Right));
			gcomHead.SetLocked(true);
			gcomHead.Add(new GGridDBText(F_Name, "氏名", 130));
			gcomHead.SetLocked(true);
			gcomHead.Add(new GGridDBText(F_Iseki, "医籍登録番号", 95, GGridDBCellDisp.Right));
			gcomHead.SetLocked(true);
			gcomHead.Add(new GGridDBText(F_Shinsei, "申請", 110, GGridDBCellDisp.Center));
			gcomHead.SetLocked(true);
			gcomHead.Add(new GGridDBText(F_UpdateDate, "MAMIS更新日時", 125, GGridDBCellDisp.Center));
			gcomHead.SetLocked(true);
			gcomHead.Add(new GGridDBText(F_Count, "項目数", 55, GGridDBCellDisp.Right));
			gcomHead.SetLocked(true);
			gcomHead.Add(new GGridDBText(F_Content, "内容", 0));
			gcomHead.SetLocked(true);
			gcomHead.SetRowStyles(rowStyleHead);
			gcomHead.EndAdd(dvHead);

			// 下段：選択中の会員の取込内容
			tblDetail = new DataTable();
			tblDetail.Columns.Add(F_No, typeof(int));
			tblDetail.Columns.Add(F_Item, typeof(string));
			tblDetail.Columns.Add(F_Type, typeof(string));
			tblDetail.Columns.Add(F_Before, typeof(string));
			tblDetail.Columns.Add(F_After, typeof(string));
			dvDetail = new DBView(tblDetail);

			AppGridCommon.StyleSet(gridDetail);
			gridDetail.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.NoMarquee;

			gcomDetail = new GGridDBCommon(gridDetail, this);
			gcomDetail.Add(new GGridDBNumber(F_No, "No", 50));
			gcomDetail.SetLocked(true);
			gcomDetail.Add(new GGridDBText(F_Item, "項目", 170));
			gcomDetail.SetLocked(true);
			gcomDetail.Add(new GGridDBText(F_Type, "区分", 60, GGridDBCellDisp.Center));
			gcomDetail.SetLocked(true);
			gcomDetail.Add(new GGridDBText(F_Before, "現在の登録内容", 370));
			gcomDetail.SetLocked(true);
			gcomDetail.Add(new GGridDBText(F_After, "MAMISの内容（取込後）", 0));
			gcomDetail.SetLocked(true);
			gcomDetail.EndAdd(dvDetail);

			// 件数
			int cnt_ok = Import.TargetCount;
			int cnt_same = Import.Persons.Count(x => x.Status == AppMamisImport.eStatus.Same);
			int cnt_ng = Import.Persons.Count - cnt_ok - cnt_same;

			lblSummary.Text = $"CSVの会員数：{Import.Persons.Count}名　／　取込できる：{cnt_ok}名　／　変更なし：{cnt_same}名　／　取込できない：{cnt_ng}名";

			//¶ 2026/11/21 CC 取込できる会員がいない場合は実行できない
			if (cnt_ok == 0)
			{
				appFuncKey.SetEnabled(FuncMamisImport_DlgConfirm.Exec.Key, false);
			}

			gridHead.RowColChange += GridHead_RowColChange;
			showDetail();

			gridHead.Select();

			base.FormFrame_Shown(sender, e);
		}

		private void GridHead_RowColChange(object sender, RowColChangeEventArgs e)
		{
			showDetail();
		}

		/// <summary>
		/// 選択中の会員の取込内容を下段に表示します。
		/// </summary>
		void showDetail()
		{
			tblDetail.Rows.Clear();
			lblDetail.Text = "選択中の会員の取込内容";

			int idx = gridHead.Row;
			if (idx < 0 || idx >= dvHead.Count)
			{
				return;
			}

			int no = Cast.Int(dvHead[idx][F_No]);
			AppMamisImport.Person p = Import.Persons.FirstOrDefault(x => x.No == no);
			if (p == null)
			{
				return;
			}

			lblDetail.Text = $"選択中の会員の取込内容：　{Cast.String(dvHead[idx][F_Code])}　{p.Name}";

			int i = 0;
			foreach (AppMamisImport.Item item in p.Items)
			{
				DataRow nrow = tblDetail.NewRow();

				nrow[F_No] = ++i;
				nrow[F_Item] = item.Name;
				nrow[F_Type] = enumKbn.DMamisLogType[(int)item.Type];
				nrow[F_Before] = item.Before;
				nrow[F_After] = item.After;

				tblDetail.Rows.Add(nrow);
			}
		}

		/// <summary>
		/// 状況ごとの文字色
		/// </summary>
		void rowStyleHead(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{
			AppMamisImport.eStatus sts = (AppMamisImport.eStatus)Cast.Int(dvHead[e.Row][F_StatusType]);

			Color color = Color.Black;

			switch (sts)
			{
				case AppMamisImport.eStatus.Same:		color = Color.Gray; break;
				case AppMamisImport.eStatus.NotFound:
				case AppMamisImport.eStatus.Mismatch:	color = Color.Red; break;
			}

			e.CellStyle.ForeColor = color;
		}

		/// <summary>
		/// ファンクションの登録
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncMamisImport_DlgConfirm.Functions);

			FuncMamisImport_DlgConfirm.Exec.Execute = doExec;
			FuncMamisImport_DlgConfirm.Cancel.Execute = formClose;
		}

		/// <summary>
		/// 実行
		/// </summary>
		void doExec()
		{
			if (Import.TargetCount == 0)
			{
				return;
			}

			if (AppMsgBox.Show(this, AppMsgBoxIndex.ExecMamisImport, Import.TargetCount) == DialogResult.Yes)
			{
				formCloseReason = FormCloseReason.Exec;
				this.Close();
			}
		}

		void formClose()
		{
			this.Close();
		}
	}
}
