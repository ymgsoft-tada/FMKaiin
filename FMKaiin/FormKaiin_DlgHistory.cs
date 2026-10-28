using C1.Win.C1TrueDBGrid;
using ComponentDB;
using ComponentGGridDB;
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
	/// 在籍期間
	/// </summary>
	public partial class FormKaiin_DlgHistory : FormFrame
	{
		/// <summary>
		/// 会員情報
		/// </summary>
		public Kaiin SelectKaiin { get; set; }

		GGridDBCommon gcom;

		DBView dvGrid;

		const string F_Code = "F_Code";
		const string F_Range = "F_Range";
		const string F_From = "F_From";

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormKaiin_DlgHistory()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// グリッド表示用テーブル
			DataTable tbl = new DataTable();
			tbl.Columns.Add(F_Code, typeof(int));
			tbl.Columns.Add(F_Range, typeof(string));
			tbl.Columns.Add(F_From, typeof(DateTime));

			// データの生成
			List<Kaiin> kas = SelectKaiin.GetHistoryList();
			kas.Add(SelectKaiin); //　最新も入れておく
			foreach(Kaiin ka in kas)
			{
				DataRow nrow = tbl.NewRow();

				nrow[F_Code] = ka.CD;
				nrow[F_From] = ka.XRow.Kaiin_DateNyukai;

				string str = $"{AppDate.GetDate(ka.XRow.Kaiin_DateNyukai)} ～ ";

				if (ka.XRow.Kaiin_DateTaikai_Null == null)
				{
					str += $"{AppDate.GetDate(DateTime.Now)}";
				}
				else
				{
					str += $"{AppDate.GetDate(ka.XRow.Kaiin_DateTaikai)}";
				}

				str += $"（{ka.GetMembershipYearMonth()}）";

				nrow[F_Range] = str;

				tbl.Rows.Add(nrow);
			}
			dvGrid = new DBView(tbl);
			dvGrid.SortQuery(F_From);


			iKaiin.Text		= $"{SelectKaiin.XRow.Kaiin_Name}";
			iIsekiNo.Text	= $"{SelectKaiin.XRow.Kaiin_IsekiTorokuNo}";
			iTotal.Text		= $" {SelectKaiin.GetTotalMembershipYearMonth()}";

			iKaiin.Enabled		= false;
			iIsekiNo.Enabled	= false;
			iTotal.Enabled		= false;

			// グリッド
			AppGridCommon.StyleSet(grid);
			gcom = new GGridDBCommon(grid, this);
			gcom.Add(new GGridDBUnbound(F_Code,"No", ubNo, 50, GGridDBCellDisp.Right));
			gcom.Add(new GGridDBText(F_Code, "会員コード", 100, GGridDBCellDisp.Right));
			gcom.Add(new GGridDBText(F_Range, "期間",0));
			gcom.EndAdd(dvGrid);

			base.FormFrame_Shown(sender, e);
		}

		string	ubNo(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			return $"{e.Row + 1}";
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncKaiin_DlgHistory.Functions);

			FuncKaiin_DlgHistory.Close.Execute = formClose;
		}

		void formClose()
		{
			this.Close();
		}
	}
}
