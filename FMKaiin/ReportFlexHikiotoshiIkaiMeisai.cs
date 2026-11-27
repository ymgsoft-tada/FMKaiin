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
	/// [作成者 tanaka]
	/// 所属医会別引落明細表
	/// </summary>
	public class ReportFlexHikiotoshiIkaiMeisai
	{
		/// <summary>レポートファイル名</summary>
		private string ReportFile = $"{AppConst.ReportFolder}{ReportProp.CRepHikiotoshiFilename}";

		private int LINE_MAX_PAGE = 31;

		/// <summary>選択中の月度</summary>
		public DateTime SelectedDateYM { get; set; } // 呼出し時にセットしておく

		/// <summary>出力するビュー</summary>
		public DBView OutView { get; private set;}

		// 出力用DataTableのカラム
		const string F_Line		= "F_Line";
		const string F_Page		= "F_Page";
		const string F_ID_Kaihi = "F_ID_Kaihi";
		const string F_CD_Kaihi = "F_CD_Kaihi";
		const string F_Kaihi_Name = "F_Kaihi_Name";
		const string F_CD_Kaiin = "F_CD_Kaiin"; // 0埋め
		const string F_Kaiin_Name = "F_Kaiin_Name";
		const string F_DateYMHiki = "F_DateYMHiki";
		const string F_costHiki = "F_costHiki";

		/// <summary>総件数</summary>
		int rowCount = 0;

		/// <summary>金額総合計</summary>
		Decimal costTotal = 0m;

		/// <summary>会員ごと最大ページ数</summary>
		//Dictionary<int, int> maxpages;
		int maxPage = 0;

		DBView dvHikiotoshi;
		DBView dvKaihi;

		/// <summary>
		/// 引落管理クラス(明細1行)
		/// </summary>
		class HikiotoshiData
		{
			public string CD_Kaihi; // 医会コード
			public string Kaihi_Name; // 医会名
			public string CD_Kaiin; // 会員コード
			public string Kaiin_Name; // 氏名
			public string DateYMHiki; // 引落年月
			public decimal costHiki; // 引落金額

			/// <summary>
			/// コンストラクタ
			/// </summary>
			public HikiotoshiData()
			{
				costHiki = 0m;
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ReportFlexHikiotoshiIkaiMeisai()
		{
		}

		/// <summary>
		/// 出力用ビュー作成
		/// <param name="fromId">出力対象会費From</param>
		/// <param name="toId">出力対象会費To</param>
		/// <param name="fromCost">出力対象金額From</param>
		/// <param name="toCost">出力対象金額To</param>
		/// </summary>
		public void CreateView(object fromId = null, object toId = null, object fromCost = null, object toCost = null)
		{
			// 出力用テーブルの作成
			DataTable dt = new DataTable();
			dt.Columns.Add(F_Line, typeof(int));
			dt.Columns.Add(F_Page, typeof(int));
			dt.Columns.Add(F_ID_Kaihi, typeof(int));
			dt.Columns.Add(F_CD_Kaihi, typeof(string));
			dt.Columns.Add(F_Kaihi_Name, typeof(string));
			dt.Columns.Add(F_CD_Kaiin, typeof(string));
			dt.Columns.Add(F_Kaiin_Name, typeof(string));
			dt.Columns.Add(F_DateYMHiki, typeof(string));
			dt.Columns.Add(F_costHiki, typeof(string));

			DBView dv = new DBView(dt);
			dv.SortQuery($"{F_ID_Kaihi},{F_Page},{F_Line}");

			string where_hikiotoshi = $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM} = #{SelectedDateYM}#"; // 対象年月

			// 抽出条件:金額
			decimal fromCt = Cast.Decimal(fromCost);
			decimal toCt = Cast.Decimal(toCost);

			// コードの指定が降順の場合の対応
			if (fromCt != 0 && toCt != 0 && fromCt > toCt)
			{
				decimal ct = fromCt;
				fromCt = toCt;
				toCt = ct;
			}

			if (fromCt != 0) where_hikiotoshi += $" AND {TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_Cost} >= {fromCt}";
			if (toCt != 0) where_hikiotoshi += $" AND {TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_Cost} <= {toCt}";

			dvHikiotoshi = new DBView(AppGlobal.DB.FillQuery($"SELECT * FROM {TableProp.t_hikiotoshi} WHERE {where_hikiotoshi}"));
			dvHikiotoshi.SortQuery($"{t_hikiotoshi.FID_Kaihi}, {t_hikiotoshi.FID_Kaiin}");

			// 対象年月に引落データあり
			if (dvHikiotoshi.Count > 0)
			{
				string where_kaihi = $"1 = 1";

				// 条件:会費マスタ
				Kaihi fromKaihi = AppGlobal.Kaihis.Get(Cast.Int(fromId));
				Kaihi toKaihi = AppGlobal.Kaihis.Get(Cast.Int(toId));

				// コードの指定が降順の場合の対応
				if (fromKaihi != null && toKaihi != null && fromKaihi.CD > toKaihi.CD)
				{
					Kaihi kh = fromKaihi;
					fromKaihi = toKaihi;
					toKaihi = kh;
				}

				if (fromKaihi != null) where_kaihi += $" AND {TableProp.t_kaihi}.{t_kaihi.FCD_Kaihi} >= {fromKaihi.CD}";
				if (toKaihi != null) where_kaihi += $" AND {TableProp.t_kaihi}.{t_kaihi.FCD_Kaihi} <= {toKaihi.CD}";

				// 会費情報
				dvKaihi = new DBView(AppGlobal.DB.FillQuery($"SELECT {TableProp.t_kaihi}.{t_kaihi.FCD_Kaihi} FROM {TableProp.t_kaihi} WHERE {where_kaihi}"));

				// 医会別に集計の準備
				// Dictionary<医会CD, List引落>
				SortedDictionary<int, List<HikiotoshiData>> dics = new SortedDictionary<int, List<HikiotoshiData>>();

				//maxpages = new Dictionary<int, int>();

				// --会費のループ--
				for (int i = 0; i < dvKaihi.Count; i++)
				{
					// 医会会費情報取得
					t_kaihi xrow = new t_kaihi(dvKaihi[i]);
					//Kaihi kih = AppGlobal.Kaihis.Get(xrow.ID_Kaihi);
					Kaihi kih = AppGlobal.Kaihis.GetCode(xrow.CD_Kaihi);

					if (kih != null)
					{
						dvHikiotoshi.RowFilterQuery($"{t_hikiotoshi.FID_Kaihi} = {kih.ID}"); // 条件の医会でフィルタ

						HikiotoshiData hd;

						// 該当医会のデータなしの場合、Dictionaryへ追加
						if (dics.ContainsKey(kih.CD) == false)
						{
							dics.Add(kih.CD, new List<HikiotoshiData>());
						}

						int totalRow = 0; // 会費ごと件数
						decimal totalKaihi = 0m; // 会費ごと合計

						// 引落データのループ
						for (int j = 0; j < dvHikiotoshi.Count; j++)
						{
							t_hikiotoshi tmprow = new t_hikiotoshi(dvHikiotoshi[j]);

							// 会員情報の取得
							Kaiin kin = AppGlobal.Kaiins.Get(tmprow.ID_Kaiin);

							// 情報の格納
							hd = new HikiotoshiData();
							hd.CD_Kaihi = Cast.String(kih.CD);
							hd.Kaihi_Name = kih.XRow.Kaihi_Name_Null;
							hd.CD_Kaiin = kin.CodeString;
							hd.Kaiin_Name = kin.XRow.Kaiin_Name_Null;
							hd.DateYMHiki = AppDate.GetYearMonthSeireki(tmprow.Hiki_DateYM);
							hd.costHiki = tmprow.Hiki_Cost;

							// 医会ごと引落リスト追加
							dics[kih.CD].Add(hd);

							// 医会別合計の処理
							totalRow++;
							totalKaihi += hd.costHiki;

							// 総合計の処理
							rowCount++;
							costTotal += hd.costHiki;
						}

						// 医会ごと合計行の追加
						if (totalRow > 0) // 明細が1件以上存在
						{
							hd = new HikiotoshiData();
							//hd.Kaiin = kin;
							hd.CD_Kaihi = "";
							hd.Kaihi_Name = "";
							hd.CD_Kaiin = "";
							hd.Kaiin_Name = "　　　　　　　　　　　　【合計】"; // 【総合計】と揃える
							hd.DateYMHiki = $"{totalRow.ToString("#,##0")}件";
							hd.costHiki = totalKaihi;
							// 医会ごとリストへ合計行を追加
							dics[kih.CD].Add(hd);
						}

					}
				}

				int page = 1;
				int line = 0;
				int no = 0;
				int preId = 0; // 集計キー

				// 出力用DBViewにデータ行を追加
				foreach (var kvp in dics) // 医会会費-Dic
				{
					Kaihi kih = AppGlobal.Kaihis.GetCode(kvp.Key);

					for (int i = 0; i < kvp.Value.Count; i++)
					{
						HikiotoshiData hd = kvp.Value[i];
						DataRow nrow = null;

						if (no < LINE_MAX_PAGE)
						{
							nrow = dt.NewRow();

							if (preId == kih.ID && line > 0)
							{
								// 前行と同じID かつ ページ先頭行以外
								// 同じ集計キーの場合、2行目以降は空欄の仕様
								nrow[F_CD_Kaihi] = ""; // コード:空欄
								nrow[F_Kaihi_Name] = ""; // 名称:空欄
							}
							else
							{
								nrow[F_CD_Kaihi] = hd.CD_Kaihi;
								nrow[F_Kaihi_Name] = hd.Kaihi_Name;
							}
							nrow[F_CD_Kaiin] = hd.CD_Kaiin;
							nrow[F_Kaiin_Name] = hd.Kaiin_Name;
							nrow[F_DateYMHiki] = hd.DateYMHiki;
							nrow[F_costHiki] = hd.costHiki;

							nrow[F_ID_Kaihi] = kih.ID; // 医会会費ID
							nrow[F_Line] = line;
							nrow[F_Page] = page;

							dv.Add(nrow);
							line++;

							preId = kih.ID; // 直近で処理したIDを保持
						}

						no++;
						if (no >= LINE_MAX_PAGE)
						{
							page++; // 改ページ
							line = 0;
							no = 0;
						}

					}
				}
				// 会費別集計を全出力した後、lineがMAXでなければ空行を追加
				if (line == LINE_MAX_PAGE)
				{
					// 最大行まで表示されていれば、改ページ
					page = 1;
				}
				else
				if (line > 0)
				{
					// 残行を空欄で埋めて改ページ
					for (int i = line; i < LINE_MAX_PAGE; i++)
					{
						DataRow nrow = dv.NewRow();
						nrow[F_Line] = line;
						nrow[F_Page] = page;
						dv.Add(nrow);

						maxPage = page;

						line++;
						if (line >= LINE_MAX_PAGE)
						{
							page = 1;
							line = 0;
						}
					}
				}
			}

			// 登録順のまま出力ビューへ反映
			OutView = new DBView(dt);

			// 会員別の最大ページ数を取得
			//for (int i = 0; i < OutView.Count; i++)
			//{
			//	int id = Cast.Int(OutView[i][F_ID_Kaiin]);
			//	int page = Cast.Int(OutView[i][F_Page]);

			//	if (maxpages.ContainsKey(id) == false)
			//	{
			//		maxpages.Add(id, 0);
			//	}
			//	maxpages[id] = page;
			//}

			// 最大ページ数を取得
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
													ReportProp.CRep12_HikiotoshiIkaiMeisai,
													"所属医会別引落明細表",
													LINE_MAX_PAGE,
													OutView
												);

			repinfo.StartPage		+= Repinfo_StartPage;
			repinfo.StartSection	+= Repinfo_StartSection;
			repinfo.PrintSection	+= Repinfo_PrintSection;
			repinfo.EndPage			+= Repinfo_EndPage;

			if(repinfo.View.Count == 0)
			{
				//出力するデータがない
				AppMsgBox.Show(AppMsgBoxIndex.NoPrintData);
				return false;
			}

			//直接印刷
			if(isDirect == true)
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

		private void Repinfo_EndPage(object sender, ReportFlex.ReportEventArgsEx e)
		{
		}

		/// <summary>
		/// ページ作成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Repinfo_StartPage(object sender, ReportFlex.ReportEventArgsEx e)
		{
			C1FlexReport rep = (C1FlexReport)sender;

			int lineNo = (e.Page - 1) * LINE_MAX_PAGE + e.Line;
			DataRow row = OutView[lineNo].Row;

			//Kaiin kin = AppGlobal.Kaiins.Get(Cast.Int(row[F_ID_Kaiin]));
			Kaihi kih = AppGlobal.Kaihis.Get(Cast.Int(row[F_ID_Kaihi]));

			string title = $"所属医会別引落明細表";
			string pageno = "";

			//if (maxpages.ContainsKey(kin.ID) == true && maxpages[kin.ID] > 1)
			//{
			//	pageno = $"{row[F_Page]}/{maxpages[kin.ID]}";
			//}
			if (maxPage > 1)
			{
				pageno = $"{row[F_Page]}/{maxPage}";
			}

			// タイトル
			setFieldValue(rep, CRep12_HikiotoshiIkaiMeisai.FFldTitle, title);

			// 月分
			//string date = AppDate.GetYearMonth(SelectedDateYM)+"分";
			//setFieldValue(rep, CRep12_HikiotoshiIkaiMeisai.FFldDateYear, date);

			// 会社名
			setFieldValue(rep, CRep12_HikiotoshiIkaiMeisai.FFldCompanyName, AppGlobal.Basic.BAS_Name);

			// 引落総合計
			setFieldValue(rep, CRep12_HikiotoshiIkaiMeisai.FFldTotalCount, $"{rowCount.ToString("#,##0")}件");
			setCost_zero(rep, CRep12_HikiotoshiIkaiMeisai.FFldCostTotalFooter, costTotal);

			// 作成日時
			setFieldValue(rep, CRep12_HikiotoshiIkaiMeisai.FFldDate, $"{DateTime.Now:yyyy/MM/dd HH:mm} 作成");

			// ページ番号
			setFieldValue(rep, CRep12_HikiotoshiIkaiMeisai.FFldPage, pageno);
		}

		private void Repinfo_StartSection(object sender, ReportFlex.ReportEventArgsEx e)
		{
			C1FlexReport rep = (C1FlexReport)sender;

			int lineNo = (e.Page - 1) * LINE_MAX_PAGE + e.Line;
		}

		/// <summary>
		/// 行作成開始
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Repinfo_PrintSection(object sender, ReportFlex.ReportEventArgsEx e)
		{
			if (e.Source.Section != SectionTypeEnum.Detail)
			{
				return;
			}

			int lineNo = (e.Page - 1) * LINE_MAX_PAGE + e.Line;

			C1FlexReport rep = (C1FlexReport)sender;

			DataRow row = OutView[lineNo].Row;

			setFieldValue(rep, CRep12_HikiotoshiIkaiMeisai.FFldIkaiCode, row[F_CD_Kaihi]);
			setFieldValue(rep, CRep12_HikiotoshiIkaiMeisai.FFldIkaiName, row[F_Kaihi_Name]);
			setFieldValue(rep, CRep12_HikiotoshiIkaiMeisai.FFldKaiinCode, row[F_CD_Kaiin]);
			setFieldValue(rep, CRep12_HikiotoshiIkaiMeisai.FFldKaiinName, row[F_Kaiin_Name]);
			setFieldValue(rep, CRep12_HikiotoshiIkaiMeisai.FFldHikiDateYM, row[F_DateYMHiki]);

			if (row[F_ID_Kaihi] != DBNull.Value) // 会費ID空を空行と判断
			{
				setCost_zero(rep, CRep12_HikiotoshiIkaiMeisai.FFldCostTotal, row[F_costHiki]);
			}
			else
			{
				setFieldValue(rep, CRep12_HikiotoshiIkaiMeisai.FFldCostTotal, ""); // 0を表示しないため
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
		/// 金額表示の編集 (0表示あり)
		/// </summary>
		/// <param name="rep"></param>
		/// <param name="fldname"></param>
		/// <param name="dec"></param>
		void setCost_zero(C1FlexReport rep, string fldname, object val)
		{
			string cost = "";

			if (val != null)
			{
				cost = $"{Cast.Decimal(val).ToString("#,##0")}";
			}

			setFieldValue(rep, fldname, cost);
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

