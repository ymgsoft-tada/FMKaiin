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
using ComponentSUI;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// MAMISデータ取込（取込結果・取込履歴）
	/// 上段に取込の一覧（処理日時・処理者）、下段に選択中の取込で変更した会員・項目を表示します。
	/// </summary>
	public partial class FormMamisImport_DlgLog : FormFrame
	{
		/// <summary>
		/// 表示する取込履歴ID。指定した場合はその取込だけを「取込結果」として表示します。
		/// nullの場合は全ての取込を「取込履歴」として表示します。
		/// </summary>
		public int? LogID { get; set; }

		GGridDBCommon gcomHead;
		GGridDBCommon gcomDetail;

		DBView dvHead;
		DBView dvDetail;
		DataTable tblDetail;

		const string F_No = "F_No";
		const string F_ID = "F_ID";
		const string F_Date = "F_Date";
		const string F_Tanto = "F_Tanto";
		const string F_File = "F_File";
		const string F_CntKaiin = "F_CntKaiin";
		const string F_CntItem = "F_CntItem";

		const string F_Code = "F_Code";
		const string F_Name = "F_Name";
		const string F_Item = "F_Item";
		const string F_Type = "F_Type";
		const string F_Before = "F_Before";
		const string F_After = "F_After";

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMamisImport_DlgLog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			string title = (LogID != null) ? "取込結果" : "取込履歴";
			lblTitle.Text = title;
			this.Text = $"MAMISデータ取込 - {title}";

			// 上段：取込の一覧（新しい順）
			DataTable tbl = new DataTable();
			tbl.Columns.Add(F_No, typeof(int));
			tbl.Columns.Add(F_ID, typeof(int));
			tbl.Columns.Add(F_Date, typeof(string));
			tbl.Columns.Add(F_Tanto, typeof(string));
			tbl.Columns.Add(F_File, typeof(string));
			tbl.Columns.Add(F_CntKaiin, typeof(int));
			tbl.Columns.Add(F_CntItem, typeof(int));

			List<DataRow> logs = new List<DataRow>();
			foreach (DataRow row in AppGlobal.DB.GetFillTable(TableProp.t_mamis_log).Rows)
			{
				if (LogID == null || Cast.Int(row[t_mamis_log.FID_MamisLog]) == LogID.Value)
				{
					logs.Add(row);
				}
			}

			int no = 0;
			foreach (DataRow row in logs.OrderByDescending(x => Cast.Int(x[t_mamis_log.FID_MamisLog])))
			{
				DataRow nrow = tbl.NewRow();

				nrow[F_No] = ++no;
				nrow[F_ID] = Cast.Int(row[t_mamis_log.FID_MamisLog]);
				nrow[F_Date] = (row[t_mamis_log.FMLG_Date] == DBNull.Value) ? "" : Cast.DateTime(row[t_mamis_log.FMLG_Date]).ToString("yyyy/MM/dd HH:mm");
				nrow[F_Tanto] = Cast.String(row[t_mamis_log.FMLG_TantoName]);
				nrow[F_File] = Cast.String(row[t_mamis_log.FMLG_FileName]);
				nrow[F_CntKaiin] = Cast.Int(row[t_mamis_log.FMLG_CntKaiin]);
				nrow[F_CntItem] = Cast.Int(row[t_mamis_log.FMLG_CntItem]);

				tbl.Rows.Add(nrow);
			}
			dvHead = new DBView(tbl);

			//¶ 2026/11/21 CC 横スクロールしないよう、固定幅の合計をグリッド幅に収め、最後の列（幅0）を残り幅に広げる
			AppGridCommon.StyleSet(gridHead);
			gridHead.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;

			gcomHead = new GGridDBCommon(gridHead, this);
			gcomHead.Add(new GGridDBNumber(F_No, "No", 50));
			gcomHead.SetLocked(true);
			gcomHead.Add(new GGridDBText(F_Date, "処理日時", 150, GGridDBCellDisp.Center));
			gcomHead.SetLocked(true);
			gcomHead.Add(new GGridDBText(F_Tanto, "処理者", 150));
			gcomHead.SetLocked(true);
			gcomHead.Add(new GGridDBNumber(F_CntKaiin, "取込会員数", 100));
			gcomHead.SetLocked(true);
			gcomHead.Add(new GGridDBNumber(F_CntItem, "取込項目数", 100));
			gcomHead.SetLocked(true);
			gcomHead.Add(new GGridDBText(F_File, "取込ファイル", 0));
			gcomHead.SetLocked(true);
			gcomHead.EndAdd(dvHead);

			// 下段：選択中の取込で変更した会員・項目
			tblDetail = new DataTable();
			tblDetail.Columns.Add(F_No, typeof(int));
			tblDetail.Columns.Add(F_Code, typeof(string));
			tblDetail.Columns.Add(F_Name, typeof(string));
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
			gcomDetail.Add(new GGridDBText(F_Code, "会員コード", 90, GGridDBCellDisp.Right));
			gcomDetail.SetLocked(true);
			gcomDetail.Add(new GGridDBText(F_Name, "氏名", 140));
			gcomDetail.SetLocked(true);
			gcomDetail.Add(new GGridDBText(F_Item, "項目", 160));
			gcomDetail.SetLocked(true);
			gcomDetail.Add(new GGridDBText(F_Type, "区分", 60, GGridDBCellDisp.Center));
			gcomDetail.SetLocked(true);
			gcomDetail.Add(new GGridDBText(F_Before, "変更前", 260));
			gcomDetail.SetLocked(true);
			gcomDetail.Add(new GGridDBText(F_After, "変更後", 0));
			gcomDetail.SetLocked(true);
			gcomDetail.EndAdd(dvDetail);

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
		/// 選択中の取込の内容を下段に表示します。
		/// </summary>
		void showDetail()
		{
			tblDetail.Rows.Clear();
			lblDetail.Text = "選択中の取込の内容";

			int idx = gridHead.Row;
			if (idx < 0 || idx >= dvHead.Count)
			{
				return;
			}

			int logid = Cast.Int(dvHead[idx][F_ID]);

			lblDetail.Text = $"選択中の取込の内容：　{Cast.String(dvHead[idx][F_Date])}　{Cast.String(dvHead[idx][F_Tanto])}";

			List<DataRow> details = new List<DataRow>();
			foreach (DataRow row in AppGlobal.DB.GetFillTable(TableProp.t_mamis_log_detail).Rows)
			{
				if (Cast.Int(row[t_mamis_log_detail.FID_MamisLog]) == logid)
				{
					details.Add(row);
				}
			}

			foreach (DataRow row in details.OrderBy(x => Cast.Int(x[t_mamis_log_detail.FMLD_No])))
			{
				DataRow nrow = tblDetail.NewRow();

				int type = Cast.Int(row[t_mamis_log_detail.FMLD_Type]);

				nrow[F_No] = Cast.Int(row[t_mamis_log_detail.FMLD_No]);
				nrow[F_Code] = Cast.Int(row[t_mamis_log_detail.FCD_Kaiin]).ToString().PadLeft(6, '0');
				nrow[F_Name] = Cast.String(row[t_mamis_log_detail.FMLD_KaiinName]);
				nrow[F_Item] = Cast.String(row[t_mamis_log_detail.FMLD_ItemName]);
				nrow[F_Type] = (enumKbn.DMamisLogType.ContainsKey(type) == true) ? enumKbn.DMamisLogType[type] : "";
				nrow[F_Before] = Cast.String(row[t_mamis_log_detail.FMLD_Before]);
				nrow[F_After] = Cast.String(row[t_mamis_log_detail.FMLD_After]);

				tblDetail.Rows.Add(nrow);
			}
		}

		/// <summary>
		/// ファンクションの登録
		/// </summary>
		protected override void SetFunction()
		{
			funcKeys = new FunctionKeyBinder(funckey, FuncMamisImport_DlgLog.Functions);

			funcKeys.On(FuncMamisImport_DlgLog.Close, formClose);
		}

		void formClose()
		{
			this.Close();
		}
	}
}
