using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentDB;

namespace App
{
	public partial class FormOutIryoKikan:FormFrame
	{
		private DBView outview;

		public FormOutIryoKikan(DBView _outview)
		{
			outview = _outview;

			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			rdoList.Checked = true;
			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの登録
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncOutIryoKikan.Functions);

			FuncOutIryoKikan.Preview.Execute = preview;
			FuncOutIryoKikan.Close.Execute = formClose;
		}

		void preview()
		{
			if(rdoList.Checked == true)
			{
				ReportFlexIryoKikan rep = new ReportFlexIryoKikan(outview);
				rep.ReportExec();
				rep = null;
			}
			else
			{
				DBView view = new DBView(outview.DataTable.Clone());
				view.DataTable.ImportRow(outview.CurrentRow.Row);

				ReportFlexIryoKikanJoho rep = new ReportFlexIryoKikanJoho(view);
				rep.ReportExec();
				rep = null;
			}
		}

		void formClose()
		{
			this.Close();
		}
	}
}
