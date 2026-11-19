using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C1.Win.FlexReport;
using ComponentDB;
using ComponentDebug;
using ComponentIO;

namespace App
{
	/// <summary>
	/// 会員一覧表（銀行名）印刷
	/// tachi
	/// </summary>
	class ReportFlexKaiinList
	{       /// <summary>レポートファイル名</summary>
		private string ReportFile = $"{AppConst.ReportFolder}{ReportProp.CRepKaiinJohoFilename}";

		private DBView outView;
		private DBView dvBank;

		private int nendo;
		private int MAX_LINE = 15;
		private string FldKamoku = "FldKamoku";

		/// <summary>
		/// 
		/// </summary>
		public ReportFlexKaiinList(DBView _outview)
		{
			outView = _outview;
			dvBank = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_bank_code));
			dvBank.SortQuery(t_bank_code.FBCD_Code);
		}

		/// <summary>
		/// レポート作成
		/// </summary>
		public bool ReportExec(bool isDirect = false)
		{
			bool success = true;

			if (ComponentFile.FileIO.Exists(ReportFile) == false)
			{
				ErrLog.WriteLine("レポートファイルが見つかりません。");
				return false;
			}

			ReportFlex.ReportInfo repinfo = new ReportFlex.ReportInfo
												(
													ReportFile,
													ReportProp.CRepKaiinList,
													"",
													MAX_LINE,
													outView
												);

			repinfo.StartPage += Repinfo_StartPage;
			repinfo.PrintSection += Repinfo_PrintSection;

			if (repinfo.View.Count == 0)
			{
				//出力するデータがない
				AppMsgBox.Show(AppMsgBoxIndex.NoPrintData);
				return false;
			}

			//直接印刷
			if (isDirect == true)
			{
				repinfo.LoadReport();
				repinfo.Print();
			}
			else
			{
				//プレビュー
				FormReportPreview frm = new FormReportPreview();
				frm.Width = 1220;
				frm.SetReport(repinfo);

				frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
				frm.ShowDialog();

				if (frm.FormCloseReason == FormCloseReason.None)
				{
					success = false;
				}

				frm.Dispose();
			}

			return success;
		}

		private void Repinfo_StartPage(object sender, ReportFlex.ReportEventArgsEx e)
		{
			C1FlexReport rep = (C1FlexReport)sender;

			setFieldValue(rep, CRepKaiinList.FFldDate, $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day} {DateTime.Now.Hour}:{DateTime.Now.Minute} 作成");
			setFieldValue(rep, CRepKaiinList.FFldPage, $"-{e.Page}-");
		}

		private void Repinfo_PrintSection(object sender, ReportFlex.ReportEventArgsEx e)
		{
			if (e.Source.Section != SectionTypeEnum.Detail)
			{
				return;
			}

			C1FlexReport rep = (C1FlexReport)sender;
			DataRow xrow = outView[( e.Page - 1 ) * MAX_LINE + e.Line].Row;

			setFieldValue(rep, CRepKaiinList.FFldCode, Cast.String(xrow[t_kaiin.FCD_Kaiin]).PadLeft(6,'0'));;
			setFieldValue(rep, CRepKaiinList.FFldName, xrow[t_kaiin.FKaiin_Name]);

			//銀行名
			if(dvBank.FindRow(xrow[t_kaiin.FKaiin_BankCode1]) == true)
			{		
				setFieldValue(rep, CRepKaiinList.FFldGinkouName, $"{Cast.String(xrow[t_kaiin.FKaiin_BankCode1]).PadLeft(4,'0')}　{dvBank.CurrentRow[t_bank_code.FBCD_FullName]}");
				setFieldValue(rep, CRepKaiinList.FFldShitenName, $"{xrow[t_kaiin.FKaiin_BankShitenCode1]}　{dvBank.CurrentRow[t_bank_code.FBCD_NameShiten]}");
			}

			setFieldValue(rep, CRepKaiinList.FFldKozaName, xrow[t_kaiin.FKaiin_BankKozaName1]);
			setFieldValue(rep, CRepKaiinList.FFldKozaKbn, enumKbn.DTypeKoza[Cast.Int(xrow[t_kaiin.FKaiin_BankKozaType1])]);
			setFieldValue(rep, CRepKaiinList.FFldKozaNo, xrow[t_kaiin.FKaiin_BankKozaNo1]);

			//新規区分？
			//setFieldValue(rep, CRepKaiinList.FFldShinkiKbn, xrow[t_kaiin.FKaiin_KoukaiKbn]);
		}

		/// <summary>
		/// 指定したレポートのフィールドを取得します。
		/// </summary>
		/// <param name="rep"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		TextField getField(C1FlexReport rep, string name)
		{
			if (rep == null)
			{
				ErrLog.Assert("rep is null");
				return null;
			}
			if (rep.Fields.Contains(name) == false)
			{
				ErrLog.Assert($"illegal field name '{name}'");
				return null;
			}
			if (!( rep.Fields[name] is TextField ))
			{
				ErrLog.Assert($"not FlexReport's TextField '{name}'");
				return null;
			}

			return (TextField)rep.Fields[name];
		}

		/// <summary>
		/// レポートのフィールドに値を登録します。
		/// </summary>
		/// <param name="rep"></param>
		/// <param name="fldname"></param>
		/// <param name="value"></param>
		void setFieldValue(C1FlexReport rep, string fldname, object value)
		{
			TextField fld = getField(rep, fldname);

			if (fld != null)
			{
				fld.Text = Cast.String(value);
			}
			else
			{
				ErrLog.WriteLine($"レポートフィールド「{fldname}」は見つかりません。");
			}
		}
	}
}
