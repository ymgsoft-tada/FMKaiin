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
using C1.Win.C1TrueDBGrid;
using ComponentDB;
using ComponentGGridDB;
using ComponentIO;

namespace App
{
	/// <summary>
	/// スタッフ情報
	/// tachi
	/// </summary>
	public partial class FormStaff:FormFrame
	{

		DBView dvStaff;
		GGridDBCommon gcom;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormStaff()
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
						rowEdit();
					}
					break;
			}
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// グリッド作成
			AppCombo.SetComboBox(iJob, enumKbn.DTypeJob, true, (int)eTypeJob.None);
			iJob.ExSetSelectedIndexByValue(AppCombo.SelectAllValue);

			dvStaff = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_staff, $"ORDER BY {t_staff.FCD_Staff}"), this.BindingContext);

			AppGridCommon.StyleSet(grid);

			gcom = new GGridDBCommon(grid, this);
			gcom.Add(new GGridDBText(t_staff.FCD_Staff, "コード", 80, GGridDBCellDisp.Right));
			gcom.SetUnboundColumnFetch(ubCode);
			gcom.Add(new GGridDBText(t_staff.FSTF_Name, "氏　名", 200));
			gcom.Add(new GGridDBText(t_staff.FSTF_TypeJob, "性別", 60, GGridDBCellDisp.Center));
			gcom.SetUnboundColumnFetch(ubSex);
			gcom.Add(new GGridDBText(t_staff.FSTF_TypeJob, "職種区分", 80, GGridDBCellDisp.Left));
			gcom.SetUnboundColumnFetch(ubType);
			//gcom.Add(new GGridDBText(t_staff.FSTF_TypeJob, "職　務", 120, GGridDBCellDisp.Left));
			//gcom.SetUnboundColumnFetch(ubShokumu);

			//if (AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.SU ||
			//	AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.Admin)
			//if (AppGlobal.LoginUser.AvailableMyno == true)
			//{
			//	gcom.Add(new GGridDBText(t_staff.FSTF_MyNo, "My№", 50, GGridDBCellDisp.Center));
			//	gcom.SetUnboundColumnFetch(ubMyno);
			//}

			//gcom.Add(new GGridDBText(t_staff.FSTF_Addr1, "住　所", 0));
			//gcom.SetUnboundColumnFetch(ubAddr);
			//gcom.SetRowStyles(rowStyleUsed);

			gcom.EndAdd(dvStaff);

			AppDbRule.SetControlByRule(iSearch, t_staff.FSTF_Name);

			changeFilter();

			grid.Select();

			//■ イベント
			grid.MouseDoubleClick += grid_MouseDoubleClick;
			iJob.SelectedIndexChanged += iJob_SelectedIndexChanged;
			iSearch.TextChanged += iSearch_TextChanged;
			btnClear.Click += btnClear_Click;
			chkUnUsed.CheckedChanged += chkUnUsed_CheckedChanged;

			base.FormFrame_Shown(sender, e);
		}

		void rowStyleUsed(object sender, FetchRowStyleEventArgs e)
		{
			t_staff xrow = new t_staff(dvStaff[e.Row]);

			if (xrow.STF_Used == false)
			{
				e.CellStyle.ForeColor = Color.Firebrick;
			}
			else
			{
				e.CellStyle.ResetForeColor();
			}
		}

		private void chkUnUsed_CheckedChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		private void iSearch_TextChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		private void iJob_SelectedIndexChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			iJob.SelectedIndexChanged -= iJob_SelectedIndexChanged;
			iSearch.TextChanged -= iSearch_TextChanged;

			iSearch.ResetText();
			iJob.ExSetSelectedIndexByValue(AppCombo.SelectAllValue);

			changeFilter();

			iJob.SelectedIndexChanged += iJob_SelectedIndexChanged;
			iSearch.TextChanged += iSearch_TextChanged;
		}

		string ubCode(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_staff xrow = new t_staff(dvStaff[e.Row]);

			Staff stf = AppGlobal.Staffs.Get(xrow.ID_Staff);

			if (stf != null)
			{
				return stf.CodeString;
			}

			return null;
		}

		//string ubShokumu(GGridDBBase col, UnboundColumnFetchEventArgs e)
		//{
		//	t_staff xrow = new t_staff(dvStaff[e.Row]);

		//	Shokumu sk = AppGlobal.Shokumus.Get(xrow.ID_Shokumu);

		//	if (sk != null)
		//	{
		//		return sk.XRow.SKM_Name;
		//	}

		//	return null;
		//}

		string ubMyno(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_staff xrow = new t_staff(dvStaff[e.Row]);

			if (xrow.STF_MyNo != "")
			{
				return "〇";
			}

			return null;
		}

		string ubAddr(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_staff xrow = new t_staff(dvStaff[e.Row]);

			return xrow.STF_Addr1 + xrow.STF_Addr2;
		}

		string ubType(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_staff xrow = new t_staff(dvStaff[e.Row]);

			return enumKbn.DTypeJob[(int)xrow.STF_TypeJob];
		}
		string ubSex(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_staff xrow = new t_staff(dvStaff[e.Row]);

			return Regex.Replace(enumKbn.DSex[(int)xrow.STF_Sex], "性", "");
		}

		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			// ShownでRefillしているので終了時に共通クラスを初期化し直す。
			AppGlobal.InitStaff();

			base.FormFrame_FormClosing(sender, e);
		}

		private void chkAll_CheckedChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		/// <summary>
		/// フィルタ変更
		/// </summary>
		void changeFilter()
		{
			string filter = "";

			if (iSearch.Text != "")
			{
				string str = iSearch.Text;
				string str_numchk = StrConv.ToNarrow(str);

				if (Regex.IsMatch(str_numchk, @"^[-.0-9]+$") == true)
				{
					// 数値のみならコード検索
					filter += string.Format("({0} = {1})", t_staff.FCD_Staff, Cast.Int(str_numchk));
				}
				else
				{
					if (str.Length == 1)
					{
						// 一文字だけなら先頭一致
						filter += string.Format("({0} OR {1})",
												DBQuery.CreateLikeKanaString(t_staff.FSTF_Name, str),
												DBQuery.CreateLikeKanaString(t_staff.FSTF_NameFurigane, str));
					}
					else
					{
						// 部分一致
						filter += string.Format("({0} OR {1})",
												DBQuery.CreateLikeKanaString(t_staff.FSTF_Name, str, true),
												DBQuery.CreateLikeKanaString(t_staff.FSTF_NameFurigane, str, true));
					}

				}
			}

			eTypeJob job = Cast.Enumelate<eTypeJob>(iJob.ExGetValue(), eTypeJob.None);
			if (job != eTypeJob.None)
			{
				if (filter != "")
					filter += " AND ";
				filter += $"({t_staff.FSTF_TypeJob} = {(int)job})";
			}
			if (chkUnUsed.Checked == false)
			{
				if (filter != "")
					filter += " AND ";
				filter += $"({t_staff.FSTF_Used} = TRUE)";
			}

			dvStaff.RowFilterQuery(filter);
		}

		private void iFilter_TextChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dvStaff.Count > 0)
			{
				rowEdit();
			}
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncStaff.Functions);

			FuncStaff.RowAdd.Execute = rowAdd;
			FuncStaff.RowEdit.Execute = rowEdit;
			FuncStaff.RowDelete.Execute = rowDelete;
			FuncStaff.Close.Execute = formClose;

			//bool enabled = false;

			//if (AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.Admin || 
			//	AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.SU)
			//{
			//	enabled = true;
			//}

			//appFuncKey.SetEnabled(FuncStaff.ShowMyno.Key, enabled);
			//appFuncKey.SetVisible(FuncStaff.ShowMyno.Key, enabled);

		}

		/// <summary>
		/// 追加
		/// </summary>
		void rowAdd()
		{
			t_staff nrow = new t_staff(dvStaff.NewRow());

			nrow.ID_Staff = AppDbID.GetNewID(dvStaff, t_staff.FID_Staff);
			nrow.CD_Staff_Null = AppDbID.GetNewCode(dvStaff, t_staff.FCD_Staff);
			nrow.STF_Used = true;
			nrow.STF_Sex = eSex.Men;
			nrow.STF_TypeJob = eTypeJob.Dr;

			FormStaff_DlgEntry frm = new FormStaff_DlgEntry();
			frm.Mode = FormStaff_DlgEntry.eMode.Add;
			frm.Row = nrow.Row;
			frm.ShowDialog();

			if (frm.FormCloseReason == FormCloseReason.Save)
			{
				dvStaff.Add(frm.Row);
				AppGlobal.DB.UpdateTable(TableProp.t_staff);

				// 共通クラスの初期化処理
				AppGlobal.InitStaff();
				dvStaff.SearchRow(t_staff.FID_Staff, nrow.ID_Staff);

			}

			frm.Dispose();
		}

		/// <summary>
		/// 編集
		/// </summary>
		void rowEdit()
		{
			if (dvStaff.Count > 0)
			{

				DataRow row = dvStaff.NewRow();

				AppDb.CopyDataRow(dvStaff.CurrentRow.Row, row);

				FormStaff_DlgEntry frm = new FormStaff_DlgEntry();
				frm.Mode = FormStaff_DlgEntry.eMode.Edit;
				frm.Row = row;
				frm.ShowDialog();

				if (frm.FormCloseReason == FormCloseReason.Save)
				{
					AppDb.CopyDataRow(frm.Row, dvStaff.CurrentRow.Row);
					AppGlobal.DB.UpdateTable(TableProp.t_staff);

					// 共通クラスの初期化処理
					AppGlobal.InitStaff();
				}

				frm.Dispose();
				frm = null;
			}
		}

		/// <summary>
		/// 削除
		/// </summary>
		void rowDelete()
		{
			if (dvStaff.Count > 0)
			{
				t_staff xrow = new t_staff(dvStaff.CurrentRow);

				if (AppGlobal.DB.CheckUsedOtherTable(TableProp.t_staff, t_staff.FID_Staff, xrow.ID_Staff, TableProp.t_staff) == true)
				{
					AppMsgBox.Show(AppMsgBoxIndex.UsedOtherTable);
					return;
				}

				if (AppMsgBox.Show(AppMsgBoxIndex.DeleteSelectedRow) == System.Windows.Forms.DialogResult.Yes)
				{
					dvStaff.Delete();
					AppGlobal.DB.UpdateTable(TableProp.t_staff);

					// 共通クラスの初期化処理
					AppGlobal.InitStaff();
				}
			}
		}

		void formClose()
		{
			this.Close();
		}
	}
}
