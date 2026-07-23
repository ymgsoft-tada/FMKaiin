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
	public partial class Form1 : FormFrame
	{
		public Form1()
		{
			InitializeComponent();

			btnBasic.Click += BtnBasic_Click;
			btnStaff.Click += BtnStaff_Click;
			btnTanto.Click += BtnTanto_Click;
		}

		private void BtnTanto_Click(object sender, EventArgs e)
		{
			FormMasterTantosha frm = new FormMasterTantosha();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		private void BtnStaff_Click(object sender, EventArgs e)
		{
			FormStaff frm = new FormStaff();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		private void BtnBasic_Click(object sender, EventArgs e)
		{
			FormMasterBasic frm = new FormMasterBasic();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			base.FormFrame_Shown(sender, e);
		}
	}
}
