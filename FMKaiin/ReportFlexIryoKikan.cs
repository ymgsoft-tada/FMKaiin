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
	/// 医療機関一覧印刷
	/// tachi
	/// </summary>
	class ReportFlexIryoKikan
	{
		/// <summary>レポートファイル名</summary>
		private string ReportFile = $"{AppConst.ReportFolder}{ReportProp.CRepIryoKikanFilename}";

		private DBView outView;
		private DBView dvIryokikan_shinryoka;

		private int nendo;
		private int MAX_LINE = 15;
		private string FldKamoku = "FldKamoku";

		/// <summary>
		/// 
		/// </summary>
		public ReportFlexIryoKikan(DBView _outview)
		{
			outView = _outview;
			dvIryokikan_shinryoka = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_iryokikan_shinryoka));

			dvIryokikan_shinryoka.SortQuery(t_iryokikan_shinryoka.FID_Iryokikan);
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
													ReportProp.CRepIryoKikanList,
													"給与所得の源泉徴収票",
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
			
			setFieldValue(rep, CRepIryoKikanList.FFldDate, $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day} {DateTime.Now.Hour}:{DateTime.Now.Minute} 作成");
			setFieldValue(rep, CRepIryoKikanList.FFldPage, $"-{e.Page}-");
		}

		private void Repinfo_PrintSection(object sender, ReportFlex.ReportEventArgsEx e)
		{
			if (e.Source.Section != SectionTypeEnum.Detail)
			{
				return;
			}

			C1FlexReport rep = (C1FlexReport)sender;
			DataRow xrow = outView[(e.Page - 1)* MAX_LINE + e.Line].Row;

			setFieldValue(rep, CRepIryoKikanList.FFldShisetsuCode, xrow[t_iryokikan.FIRK_Code]);
			setFieldValue(rep, CRepIryoKikanList.FFldShisetsuName, xrow[t_iryokikan.FIRK_Name]);

			setFieldValue(rep, CRepIryoKikanList.FFldPost, xrow[t_iryokikan.FIRK_Post]);

			//住所
			TextField f = (TextField)rep.Fields[CRepIryoKikanList.FFldAdd];
			if (Cast.String(xrow[t_iryokikan.FIRK_Addr1]).Length + Cast.String(xrow[t_iryokikan.FIRK_Addr2]).Length > 60)
			{				
				f.Font.Size = 6;
			}
			else
			{
				f.Font.Size = (float)8.25;
			}

			setFieldValue(rep, CRepIryoKikanList.FFldAdd, Cast.String(xrow[t_iryokikan.FIRK_Addr1]) +Environment.NewLine+ Cast.String(xrow[t_iryokikan.FIRK_Addr2]));

			setFieldValue(rep, CRepIryoKikanList.FFldTel, Cast.String(xrow[t_iryokikan.FIRK_Tel1]).Replace("-",""));

			setFieldValue(rep, CRepIryoKikanList.FFldFax, Cast.String(xrow[t_iryokikan.FIRK_Fax1]).Replace("-", ""));

			//退会区分
			string taikai = "";
			switch (xrow[t_iryokikan.FIRK_TaikaiKbn])
			{
				case true:
				taikai = "会員";
				break;

				case false:
				taikai = "退会";
				break;

				default:
				taikai = "";
				break;
			}
			setFieldValue(rep, CRepIryoKikanList.FFldTaikaiKbn, taikai);

			//組ｺｰﾄﾞ
			if(xrow[t_iryokikan.FIRK_KumiCode] == null || Cast.String(xrow[t_iryokikan.FIRK_KumiCode]) == "")
			{
				setFieldValue(rep, CRepIryoKikanList.FFldKumiCode, "");
			}
			else
			{
				setFieldValue(rep, CRepIryoKikanList.FFldKumiCode, Cast.String(xrow[t_iryokikan.FIRK_KumiCode]).PadLeft(2,'0'));
			}

			//診療科目
			DataRowView[] rows = dvIryokikan_shinryoka.DataView.FindRows(Cast.String(xrow[t_iryokikan.FID_Iryokikan]));

			for (int i = 0 ; i <= 4 ; i++)
			{

				if(i<=rows.Length-1)
				{
					 setFieldValue(rep, FldKamoku + i, Cast.String(rows[i][t_shinryoka.FID_Shinryoka]).PadLeft(2, '0'));
				 }
				else
				{
					setFieldValue(rep, FldKamoku + i, "");
				}
			}
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
