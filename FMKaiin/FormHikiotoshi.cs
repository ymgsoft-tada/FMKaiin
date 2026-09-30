using ComponentDB;
using ComponentDebug;
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

			base.FormFrame_Shown(sender, e);
		}

		void dataLoad()
		{
			grid.SuspendBinding();

			FormBg_Progress prog = new FormBg_Progress();
			prog.TitleText = "しばらくお待ちください。";
			prog.LabelText = "データを取得しています。";
			prog.DoWorkEvent += prog_DoWorkEvent;
			prog.ShowDialog();
			prog.Dispose();

			grid.SetDataBinding(dvGrid.DataView, "",true,true);
			grid.ResumeBinding();
		}

		private void prog_DoWorkEvent(object sender, DoWorkEventArgs e)
		{
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
				 $"HAVING ((({TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM}) = #{iDateYM.UcValue.Value}#)) " +
				 $"ORDER BY " +
				 $"{TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin}, " +
				 $"{TableProp.t_kaiin}.{t_kaiin.FKaiin_Name}";

			//string sql = $"SELECT {TableProp.t_hikiotoshi}.{t_hikiotoshi.FID_Kaiin}," +
			//			 $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM}," +
			//			 $"Sum({TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_Cost}) AS {F_Hiki_CostTotal} " +
			//			 $"FROM {TableProp.t_hikiotoshi} " +
			//			 $"GROUP BY {TableProp.t_hikiotoshi}.{t_hikiotoshi.FID_Kaiin}, {TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM} " +
			//			 $"HAVING ((({TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM}) = #{iDateYM.UcValue.Value}#))";

			DataTable dt = AppGlobal.DB.FillQuery(sql);
			dvGrid = new DBView(dt);

		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncHikiotoshi.Functions);

			FuncHikiotoshi.Exec.Execute = doExec;
			FuncHikiotoshi.Close.Execute = formClose;
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
			
			}
			catch(Exception ex)
			{
				ErrLog.WriteException(ex);
				ret = false;
			}
			

			return ret;
		}

		private void prog_create(object sender, DoWorkEventArgs e)
		{
			FormBg_Progress prog = (FormBg_Progress)sender;

			prog.AdvanceProgress(20);

			DBView dv_kai = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin));
			dv_kai.RowFilterQuery($"{t_kaiin.FKaiin_TypeZaiseki} != {eTypeZaiseki.Taikai}");
			dv_kai.SortQuery($"{t_kaiin.FCD_Kaiin}");


			prog.AdvanceProgress(100);

			int id = AppDbID.GetNewID(dvHiki, t_hikiotoshi.FID_Hikiotoshi);

			for (int i = 0; i < dv_kai.Count; i++)
			{
				t_kaiin xrow = new t_kaiin(dv_kai[i]);



			}

		}

		void formClose()
		{
			this.Close();
		}
	}
}
