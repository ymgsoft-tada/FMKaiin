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
	/// 引落データの一覧画面
	/// </summary>
	public partial class FormHikiotoshi : FormFrame
	{ 
		// 引落データ
		DBView dvHiki;
		DBView dvGrid;

		GGridDBCommon gcom;

		const string F_Hiki_CostTotal = "F_Hiki_CostTotal";

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormHikiotoshi()
		{
			InitializeComponent();
		}

		/// <summary>
		/// キー操作
		/// </summary>
		protected override void FormFrame_KeyDown(object sender, KeyEventArgs e)
		{
			//■ ファンクションキーの標準ショートカットの抑止
			e.Handled = base.Patcher_CheckShortcutForKillingWindowsAction(e.KeyCode, e.Alt);

			// 処理が必要かどうかチェック。
			if (base.CheckEnabledFormKeyPreview(e.KeyCode) == true)
			{
				return;
			}
			if (this.ActiveControl == null)
			{
				return;
			}

			switch (e.KeyCode)
			{
				case Keys.Enter:
					if (this.ActiveControl == grid)
					{
						showEdit();
					}
					break;
			}
		}

		protected override void FormFrame_Load(object sender, EventArgs e)
		{

			base.FormFrame_Load(sender, e);
		}

		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			dvHiki = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_hikiotoshi));

			// 一か月前を初期値としておく
			DateTime dt = DateTime.Now.AddMonths(-1);

			iDateYM.UcValue = new DateTime(dt.Year, dt.Month,1);

			AppGridCommon.StyleSet(grid);

			gcom = new GGridDBCommon(grid,this);
			gcom.Add(new GGridDBNumber(t_kaiin.FCD_Kaiin, "会員コード",120));
			gcom.Add(new GGridDBText(t_kaiin.FKaiin_Name, "氏名", 200));
			gcom.Add(new GGridDBCurrency(F_Hiki_CostTotal, "引落金額", 0));
			gcom.EndAdd(dvGrid);

			dataLoad();

			// イベントの登録
			iDateYM.UcValueChanged += IDateYM_UcValueChanged;
			grid.MouseDoubleClick += Grid_MouseDoubleClick;

			base.FormFrame_Shown(sender, e);
		}

		private void Grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dvGrid.Count > 0)
			{
				showEdit();
			}
		}

		private void IDateYM_UcValueChanged(object sender, EventArgs e)
		{
			dataLoad();
		}

		/// <summary>
		/// 引落データ（個人合算分）のロード
		/// </summary>
		void dataLoad()
		{
			grid.SuspendBinding();

			DateTime ym;
			if (iDateYM.UcValue == null)
			{
				ym = new DateTime(1900,1,1);
			}
			else
			{
				ym = iDateYM.UcValue.Value;
			}

			FormBg_Progress prog = new FormBg_Progress();
			prog.TitleText = "しばらくお待ちください。";
			prog.LabelText = "データを取得しています。";
			prog.DoWorkEvent += prog_DoWorkEvent;
			prog.Args = new object[] {ym};
			prog.ShowDialog();
			prog.Dispose();

			grid.SetDataBinding(dvGrid.DataView, "",true,true);
			grid.ResumeBinding();

			iTotalCount.Text = dvGrid.Count.ToString("#,##0");

			decimal cost = 0;
			for (int i = 0; i < dvGrid.Count; i++)
			{
				cost += Cast.Decimal(dvGrid[i][F_Hiki_CostTotal]);
			}

			iTotalCost.Text = cost.ToString("#,##0");

			DBView dv = new DBView(dvHiki);
			dv.RowFilterQuery($"{t_hikiotoshi.FHiki_DateYM} = #{ym}#");
			dv.SortQuery($"{t_hikiotoshi.FLastUpdate} DESC");

			string lbl = "";

			if (dv.Count > 0)
			{
				t_hikiotoshi xrow = new t_hikiotoshi(dv[0]);

				lbl = $"最終更新：{xrow.LastUpdate.ToString("yyyy/MM/dd hh:mm")}";
			}

			lblCreateDate.Text = lbl;

			// 編集ボタンの使用可否
			appFuncKey.SetEnabled(FuncHikiotoshi.Edit.Key, dvGrid.Count > 0);
		}

		private void prog_DoWorkEvent(object sender, DoWorkEventArgs e)
		{
			FormBg_Progress prog = (FormBg_Progress)sender;

			DateTime ym = Cast.DateTime(prog.Args[0]);

			// 集計結果のクエリ
			string sql = $"SELECT " +
				 $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FID_Kaiin}, " +
				 $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM}, " +
				 $"Sum({TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_Cost}) AS {F_Hiki_CostTotal}, " +
				 $"{TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin}, " +
				 $"{TableProp.t_kaiin}.{t_kaiin.FKaiin_Name} " +
				 $"FROM {TableProp.t_hikiotoshi} " +
				 $"LEFT JOIN {TableProp.t_kaiin} ON {TableProp.t_hikiotoshi}.{t_hikiotoshi.FID_Kaiin} = {TableProp.t_kaiin}.{t_kaiin.FID_Kaiin} " +
				 $"GROUP BY " +
				 $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FID_Kaiin}, " +
				 $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM}, " +
				 $"{TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin}, " +
				 $"{TableProp.t_kaiin}.{t_kaiin.FKaiin_Name} " +
				 $"HAVING ((({TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM}) = #{ym}#)) " +
				 $"ORDER BY " +
				 $"{TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin}, " +
				 $"{TableProp.t_kaiin}.{t_kaiin.FKaiin_Name}";

			DataTable dt = AppGlobal.DB.FillQuery(sql);
			dvGrid = new DBView(dt, this.BindingContext);

		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncHikiotoshi.Functions);

			FuncHikiotoshi.Exec.Execute = doExec;
			FuncHikiotoshi.Edit.Execute = showEdit;
			FuncHikiotoshi.Close.Execute = formClose;
		}

		void showEdit()
		{

			if (dvGrid.Count > 0)
			{
				t_kaiin xrow = new t_kaiin(dvGrid.CurrentRow);

				FormHikiotoshi_DlgEntry frm = new FormHikiotoshi_DlgEntry();
				frm.SelectYM = iDateYM.UcValue.Value;
				frm.SelectKaiin = AppGlobal.Kaiins.Get(xrow.ID_Kaiin);
				frm.ShowDialog();
				
				if (frm.FormCloseReason == FormCloseReason.Save)
				{
					// 現在の位置
					int frow = grid.FirstRow;
					int crow = grid.Row;

					// データのロード
					dataLoad();

					// 元のグリッド位置へ
					grid.FirstRow = frow;
					grid.Row = crow;
				}
			}
		}

		void doExec()
		{
			if (iDateYM.UcValue == null)
			{
				iDateYM.Select();
				appToolTip.Show(iDateYM, AppToolTipIndex.CannotUseBlank);
			}
			else
			{
				string date = AppDate.GetYearMonth(iDateYM.UcValue.Value) + "度";


				if (dvGrid.Count == 0)
				{
					if (AppMsgBox.Show(this, AppMsgBoxIndex.ConfirmCreateHikiotoshi, date) == DialogResult.Yes)
					{
						if (createData() == true)
						{
							dataLoad();
							AppMsgBox.Show(this,AppMsgBoxIndex.CreateHikiotoshiData, date);
						}
						else
						{
							AppMsgBox.Show(this,AppMsgBoxIndex.FailedHikiotoshiData);
						}
					}
				}
				else
				{
					if (AppMsgBox.Show(this, AppMsgBoxIndex.ConfirmRemakeHikiotoshi, date) == DialogResult.Yes &&
						AppMsgBox.Show(this, AppMsgBoxIndex.ConfirmRemakeHikiotoshi2) == DialogResult.Yes)
					{
						// 既存のデータは消す
						DBView dv = new DBView(dvHiki);
						dv.RowFilterQuery($"{t_hikiotoshi.FHiki_DateYM} = #{iDateYM.UcValue}#");

						for(;dv.Count > 0;)
						{
							dv.Delete();
						}

						if (createData() == true)
						{
							dataLoad();
							AppMsgBox.Show(this,AppMsgBoxIndex.CreateHikiotoshiData, date);
						}
						else
						{
							AppMsgBox.Show(this,AppMsgBoxIndex.FailedHikiotoshiData);
						}
					}
				}

			}
		}

		bool createData()
		{
			bool ret = true;

			try
			{
				FormBg_Progress prog = new FormBg_Progress();
				prog.DoWorkEvent += prog_create;
				prog.ShowDialog();
				prog.Dispose();			
			}
			catch(Exception ex)
			{
				ErrLog.WriteException(ex);
				ret = false;
			}
			

			return ret;
		}

		/// <summary>
		/// 引落データの生成
		/// </summary>
		private void prog_create(object sender, DoWorkEventArgs e)
		{
			FormBg_Progress prog = (FormBg_Progress)sender;

			prog.AdvanceProgress(20);

			DBView dv_kai = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin));
			dv_kai.RowFilterQuery($"{t_kaiin.FKaiin_TypeZaiseki} <> {(int)eTypeZaiseki.Taikai}");
			dv_kai.SortQuery($"{t_kaiin.FCD_Kaiin}");


			prog.AdvanceProgress(100);

			// 新規ID
			int id = AppDbID.GetNewID(dvHiki, t_hikiotoshi.FID_Hikiotoshi);
			DateTime ym = iDateYM.UcValue.Value;

			for (int i = 0; i < dv_kai.Count; i++)
			{
				t_kaiin xrow = new t_kaiin(dv_kai[i]);

				// 会員情報から所属会費を取得
				Kaiin ka = AppGlobal.Kaiins.Get(xrow.ID_Kaiin);

				if (ka != null)
				{
					foreach(Kaihi k in ka.Kaihis)
					{					
						t_hikiotoshi nrow = new t_hikiotoshi(dvHiki.NewRow());
						nrow.ID_Hikiotoshi	= id++;
						nrow.ID_Kaihi		= k.ID;
						nrow.ID_Kaiin		= ka.ID;
						nrow.Hiki_DateYM	= ym;
						nrow.Hiki_Cost_Null = k.GetCost(ym);
						nrow.Hiki_Shiharai	= ka.GetShiharai(k);

						dvHiki.Add(nrow.Row);
					}
				}
			}

			AppGlobal.DB.UpdateTable(TableProp.t_hikiotoshi);
		}

		void formClose()
		{
			this.Close();
		}
	}
}
