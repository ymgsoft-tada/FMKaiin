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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 tanaka]
	/// 銀行コード選択リスト
	/// </summary>
	public partial class FormSelectorBankCode : FormFrame
	{
		DBView dvBC;
		GGridDBCommon gcom;

		/// <summary>
		/// 選択された銀行コードのレコード
		/// </summary>
		public t_bank_code SelectedBankCode { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormSelectorBankCode()
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
						execClose();
					}
					break;
			}

			base.FormFrame_KeyDown(sender, e);
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			AppGridCommon.StyleSet(grid);

			dvBC = new DBView(AppGlobal.BankCodeMg.DbView, this.BindingContext);
			dvBC.SortQuery($"{t_bank_code.FBCD_Code}, {t_bank_code.FBCD_CodeShiten}");

			gcom = new GGridDBCommon(grid, this);
			gcom.Add(new GGridDBText(t_bank_code.FBCD_Code, "銀行ｺｰﾄﾞ", 0.15f, GGridDBCellDisp.Right));
			gcom.Add(new GGridDBText(t_bank_code.FBCD_FullName, "銀行名", 0.35f));
			gcom.Add(new GGridDBText(t_bank_code.FBCD_CodeShiten, "支店ｺｰﾄﾞ", 0.15f, GGridDBCellDisp.Right));
			gcom.Add(new GGridDBText(t_bank_code.FBCD_NameShiten, "支店名", 0.0f));
			gcom.EndAdd(dvBC);

			if (SelectedBankCode != null)
			{
				dvBC.FindRow(new object[]{SelectedBankCode.BCD_Code, SelectedBankCode.BCD_CodeShiten});
				AppGridCommon.SetRow(grid, grid.Row);
			}

//			AppDbRule.SetControlByRule(iBank, t_bank.FBNK_Name);
//			AppDbRule.SetControlByRule(iShiten, t_bank.FBNK_NameShiten);
			AppDbRule.SetControlByRule(iBank, t_bank_code.FBCD_Name); // t_bankは存在しない可能性ありのためt_bank_codeを指定
			AppDbRule.SetControlByRule(iShiten, t_bank_code.FBCD_NameShiten);

			// イベント登録
			grid.MouseDoubleClick += grid_MouseDoubleClick;
			iBank.TextChanged += iBank_TextChanged;
			iShiten.TextChanged += iShiten_TextChanged;

			base.FormFrame_Shown(sender, e);
		}

		void viewFilter()
		{
			string filter = "";

			if (iBank.Text != "")
			{
				string str = iBank.Text;
				string str_numchk = StrConv.ToNarrow(str);

				if (Regex.IsMatch(str_numchk, @"^[-.0-9]+$") == true)
				{
					// 数値のみならコード検索
					filter += string.Format("({0} = {1})", t_bank_code.FBCD_Code, str_numchk);
				}
				else
				if (str.Length == 1)
				{
					// 一文字だけなら先頭一致
					filter += string.Format("({0} OR {1})", 
												DBQuery.CreateLikeKanaString(t_bank_code.FBCD_FullName, str), 
												DBQuery.CreateLikeKanaString(t_bank_code.FBCD_NameFurigana, str));
				}
				else
				{
					// 部分一致
					filter += string.Format("({0} OR {1})", 
											DBQuery.CreateLikeKanaString(t_bank_code.FBCD_FullName, str, true), 
											DBQuery.CreateLikeKanaString(t_bank_code.FBCD_NameFurigana, str, true));
				}
			}
			if (iShiten.Text != "")
			{
				string str = iShiten.Text;
				string str_numchk = StrConv.ToNarrow(str);

				if (filter != "") filter += " AND ";

				if (Regex.IsMatch(str_numchk, @"^[-.0-9]+$") == true)
				{
					// 数値のみならコード検索
					filter += string.Format("({0} = {1})", t_bank_code.FBCD_CodeShiten, str_numchk);
				} else
				if (str.Length == 1)
				{
					// 一文字だけなら先頭一致
					filter += string.Format("({0} OR {1})", 
												DBQuery.CreateLikeKanaString(t_bank_code.FBCD_NameShiten, str), 
												DBQuery.CreateLikeKanaString(t_bank_code.FBCD_NameShitenFurigana, str));
				}
				else
				{
					// 部分一致
					filter += string.Format("({0} OR {1})", 
											DBQuery.CreateLikeKanaString(t_bank_code.FBCD_NameShiten, str, true), 
											DBQuery.CreateLikeKanaString(t_bank_code.FBCD_NameShitenFurigana, str, true));
				}
			}

			dvBC.RowFilterQuery(filter);
		}

		private void iShiten_TextChanged(object sender, EventArgs e)
		{
			viewFilter();
			grid.Row = 0;
		}

		private void iBank_TextChanged(object sender, EventArgs e)
		{
			viewFilter();
			grid.Row = 0;
		}

		private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			execClose();
		}

		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncSelectorBankCode.Functions);
			
			FuncSelectorBankCode.Clear.Execute		= filterClear;
			FuncSelectorBankCode.Exec.Execute		= execClose;
			FuncSelectorBankCode.Cancel.Execute		= cancelClose;
		}

		void filterClear()
		{
			iBank.TextChanged	-= iBank_TextChanged;
			iShiten.TextChanged -= iShiten_TextChanged;

			iBank.Clear();
			iShiten.Clear();

			viewFilter();

			iBank.Select();

			iBank.TextChanged	+= iBank_TextChanged;
			iShiten.TextChanged += iShiten_TextChanged;
		}

		void execClose()
		{
			if (dvBC.Count > 0)
			{
				formCloseReason = FormCloseReason.Exec;
				SelectedBankCode = new t_bank_code(dvBC.CurrentRow);
				this.Close();
			}
		}

		void cancelClose()
		{
			this.Close();
		}
	}
}
