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
	/// 医療機関情報印刷
	/// tachi
	/// </summary>
	class ReportFlexIryoKikanJoho
	{
		/// <summary>レポートファイル名</summary>
		private string ReportFile = $"{AppConst.ReportFolder}{ReportProp.CRepIryoKikanFilename}";

		private DBView outView;
		private DBView dvIryokikan_shinryoka;
		private DBView dvKamoku;
		private DBView dvKumi;
		private DBView dvKaisetsuShutai;
		private DBView dvTantosha;

		private int nendo;
		private int MAX_LINE = 1;
		private string FldKamoku = "F_Kamoku";

		/// <summary>
		/// 
		/// </summary>
		public ReportFlexIryoKikanJoho(DBView _outview)
		{
			outView = _outview;
			dvIryokikan_shinryoka = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_iryokikan_shinryoka));
			dvIryokikan_shinryoka.SortQuery(t_iryokikan_shinryoka.FID_Iryokikan);

			dvKamoku = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_shinryoka));
			dvKamoku.SortQuery(t_shinryoka.FID_Shinryoka);

			dvKumi = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kumicd));
			dvKumi.SortQuery(t_kumicd.FID_KumiCode);

			dvKaisetsuShutai = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaisetsushutai));
			dvKaisetsuShutai.SortQuery(t_kaisetsushutai.FID_Kaisetsushutai);

			dvTantosha = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_tantosha));
			dvTantosha.SortQuery(t_tantosha.FID_Tanto);
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
													ReportProp.CRepIryoKikan,
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
			DataRow xrow = outView[( e.Page - 1 ) * MAX_LINE + e.Line].Row;

			setFieldValue(rep, CRepIryoKikan.FF_Date, $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day} {DateTime.Now.Hour}:{DateTime.Now.Minute}");
			
			//担当者
			dvTantosha.FindRow(xrow[t_iryokikan.FLastUpdateUser]);
			setFieldValue(rep, CRepIryoKikan.FF_LastUpdate, $"{dvTantosha.CurrentRow[t_tantosha.FTNT_Name]}");
		}

		private void Repinfo_PrintSection(object sender, ReportFlex.ReportEventArgsEx e)
		{
			if (e.Source.Section != SectionTypeEnum.Detail)
			{
				return;
			}

			C1FlexReport rep = (C1FlexReport)sender;
			DataRow xrow = outView[( e.Page - 1 ) * MAX_LINE + e.Line].Row;

			setFieldValue(rep, CRepIryoKikan.FF_Code, xrow[t_iryokikan.FIRK_Code]);
			setFieldValue(rep, CRepIryoKikan.FF_Name, xrow[t_iryokikan.FIRK_Name]);
			setFieldValue(rep, CRepIryoKikan.FF_Kana, xrow[t_iryokikan.FIRK_Kana]);

			setFieldValue(rep, CRepIryoKikan.FF_Tsusho, xrow[t_iryokikan.FIRK_Tsusho]);
			setFieldValue(rep, CRepIryoKikan.FF_TsushoKana, xrow[t_iryokikan.FIRK_TsushoKana]);

			setFieldValue(rep, CRepIryoKikan.FF_Post, xrow[t_iryokikan.FIRK_Post]);
			setFieldValue(rep, CRepIryoKikan.FF_Add0, xrow[t_iryokikan.FIRK_Addr1]);
			setFieldValue(rep, CRepIryoKikan.FF_Add2, xrow[t_iryokikan.FIRK_Addr2]);

			setFieldValue(rep, CRepIryoKikan.FF_Tel, xrow[t_iryokikan.FIRK_Tel1]);
			setFieldValue(rep, CRepIryoKikan.FF_Fax, xrow[t_iryokikan.FIRK_Fax1]);

			//開設主体
			dvKaisetsuShutai.FindRow(xrow[t_iryokikan.FIRK_KaisetsuShutai]);
			setFieldValue(rep, CRepIryoKikan.FF_Kaisetsu, dvKaisetsuShutai.CurrentRow[t_kaisetsushutai.FKST_Name]);

			//病床有無
			string umu = Cast.Bool(xrow[t_iryokikan.FIRK_ByoshoUmu])?"有":"無";
			setFieldValue(rep, CRepIryoKikan.FF_ByoshoUmu, umu);

			setFieldValue(rep, CRepIryoKikan.FF_Count, xrow[t_iryokikan.FIRK_Kyoka]);

			//併設施設
			if(Cast.Bool(xrow[t_iryokikan.FIRK_Kaigo]) == true)
			{
				setFieldValue(rep, CRepIryoKikan.FF_Heisetsu, "介護施設");
			}
			else
			{
				setFieldValue(rep, CRepIryoKikan.FF_Heisetsu, xrow[t_iryokikan.FIRK_Memo]);
			}
			
			//組ｺｰﾄﾞ
			dvKumi.FindRow(xrow[t_iryokikan.FIRK_KumiCode]);
			setFieldValue(rep, CRepIryoKikan.FF_Kumimei, $"{Cast.String(xrow[t_iryokikan.FIRK_KumiCode]).PadLeft(2, '0')}　{dvKumi.CurrentRow[t_kumicd.FKMC_Name]}");

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
			setFieldValue(rep, CRepIryoKikan.FF_Taikai, taikai);

			//診療科目
			if (Cast.String(xrow[t_iryokikan.FID_Iryokikan]) != "")
			{
				DataRowView[] rows = dvIryokikan_shinryoka.DataView.FindRows(Cast.String(xrow[t_iryokikan.FID_Iryokikan]));

				for (int i = 0 ; i <= 9 ; i++)
				{

					if (i <= rows.Length - 1)
					{
						dvKamoku.FindRow(rows[i][t_shinryoka.FID_Shinryoka]);

						setFieldValue(rep, FldKamoku + i, $"{Cast.String(rows[i][t_shinryoka.FID_Shinryoka]).PadLeft(2, '0')}　{dvKamoku.CurrentRow[t_shinryoka.FSRK_Name]}");
					}
					else
					{
						setFieldValue(rep, FldKamoku + i, "");
					}
				}
			}
			else
			{
				for (int i = 0 ; i <= 9 ; i++)
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
