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
			btnBankCode.Click += BtnBankCode_Click;
			btnKaihi.Click += BtnKaihi_Click;
			btnIryokikanToroku.Click += BtnIryokikanToroku_Click;
		}

		private void BtnIryokikanToroku_Click(object sender, EventArgs e)
		{
			FormMasterIryoKikan frm = new FormMasterIryoKikan();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
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

		private void BtnBankCode_Click(object sender, EventArgs e)
		{
			FormMasterBankCode frm = new FormMasterBankCode();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		private void BtnKaihi_Click(object sender, EventArgs e)
		{
			// 銀行コードテーブルの取得
			initBankCode();

			FormMasterKaihi frm = new FormMasterKaihi();
			frm.ShowDialog();
			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 銀行コードデータの取得処理
		/// </summary>
		void initBankCode()
		{
			if (AppGlobal.BankCodeMg == null)
			{
				FormBg_Progress prog = new FormBg_Progress();
				prog.TitleText = "しばらくお待ちください。";
				prog.LabelText = "データをロードしています。";
				prog.DoWorkEvent += prog_DoWorkEvent;
				prog.ShowDialog();
				prog.Dispose();
				prog = null;
			}
		}

		private void prog_DoWorkEvent(object sender, DoWorkEventArgs e)
		{
			FormBg_Progress prog = (FormBg_Progress)sender;
			prog.AdvanceProgress(100);

			// 銀行コードは大量のデータになるので、スプラッシュ時ではなくログインしてから取得する
			AppGlobal.DB.GetReFillTable(TableProp.t_bank_code);
			AppGlobal.InitBankCodeMg();
			AppGlobal.Banks.Init(); // FBファイル作成に利用

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
