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
	/// 年間会費徴収明細書
	/// </summary>
	public class ReportFlexChoshuMeisaiYear
	{
		/// <summary>レポートファイル名</summary>
		private string ReportFile = $"{AppConst.ReportFolder}{ReportProp.CRepHikiotoshiFilename}";

		private int LINE_MAX_PAGE = 15;

		/// <summary>選択中の月度</summary>
		public DateTime SelectedDateYM { get; set; } // 呼出し時にセットしておく

		/// <summary>出力するビュー</summary>
		public DBView OutView { get; private set;}

		// 出力用DataTableのカラム
		const string F_Line		= "F_Line";
		const string F_Page		= "F_Page";
		const string F_ID_Kaiin = "F_ID_Kaiin";
		const string F_ID_Kaihi = "F_ID_Kaihi";
		const string F_CD_Kaiin = "F_CD_Kaiin";
		const string F_KaihiName = "F_KaihiName";
		static readonly string[] F_Costs = { "F_Cost1", "FCost2", "F_Cost3", "F_Cost4", "F_Cost5", "F_Cost6",
											 "F_Cost7", "F_Cost8", "F_Cost9", "F_Cost10", "F_Cost11", "F_Cost12",};
		const string F_CostTotalKaihi = "F_CostTotalKaihi"; // 行の合計

		/// <summary>
		/// 帳票-月額field名
		/// </summary>
		string[] repCostsField;
		string[] repCostsColTotalField;

		/// <summary>会員ごと最大ページ数</summary>
		Dictionary<int, int> maxpages;

		DBView dvHikiotoshi;
		DBView dvKaiin;

		SortedDictionary<int, List<eShiharai>> dics_shiharaiKoza;
		SortedDictionary<int, Decimal?> dics_costTotal; // 会員別の総合計
		SortedDictionary<int, Dictionary<int, Decimal?>> dics_costTotalMonth; // 会員別の月別合計 <会員CD, <月, 費用>>

		Dictionary<int, int> dics_monthNyukai; // <会員CD, 入会月> 出力対象年に該当なければ月=0
		Dictionary<int, int> dics_monthTaikai; // <会員CD, 退会月> 出力対象年に該当なければ月=13

		/// <summary>
		/// 引落管理クラス(明細1行)
		/// </summary>
		class HikiotoshiData
		{
			public Kaiin Kaiin;
			public int ID_Kaihi; // 内部処理用
			public int CD_Kaihi; // 内部処理用
			public string KaihiName;
			public Dictionary<int, Decimal?> dics_cost; // 月額
			public decimal? CostTotalKaihi;

			/// <summary>
			/// コンストラクタ
			/// </summary>
			public HikiotoshiData()
			{
				dics_cost = new Dictionary<int, Decimal?>();
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ReportFlexChoshuMeisaiYear()
		{
			repCostsField = new string[]
			{
				CRep16_ChoshuMeisaiYear.FFldCost1,  CRep16_ChoshuMeisaiYear.FFldCost2,
				CRep16_ChoshuMeisaiYear.FFldCost3,  CRep16_ChoshuMeisaiYear.FFldCost4,
				CRep16_ChoshuMeisaiYear.FFldCost5,  CRep16_ChoshuMeisaiYear.FFldCost6,
				CRep16_ChoshuMeisaiYear.FFldCost7,  CRep16_ChoshuMeisaiYear.FFldCost8,
				CRep16_ChoshuMeisaiYear.FFldCost9,  CRep16_ChoshuMeisaiYear.FFldCost10,
				CRep16_ChoshuMeisaiYear.FFldCost11, CRep16_ChoshuMeisaiYear.FFldCost12,
			};

			repCostsColTotalField = new string[]
			{
				CRep16_ChoshuMeisaiYear.FFldCostColTotal1,  CRep16_ChoshuMeisaiYear.FFldCostColTotal2,
				CRep16_ChoshuMeisaiYear.FFldCostColTotal3,  CRep16_ChoshuMeisaiYear.FFldCostColTotal4,
				CRep16_ChoshuMeisaiYear.FFldCostColTotal5,  CRep16_ChoshuMeisaiYear.FFldCostColTotal6,
				CRep16_ChoshuMeisaiYear.FFldCostColTotal7,  CRep16_ChoshuMeisaiYear.FFldCostColTotal8,
				CRep16_ChoshuMeisaiYear.FFldCostColTotal9,  CRep16_ChoshuMeisaiYear.FFldCostColTotal10,
				CRep16_ChoshuMeisaiYear.FFldCostColTotal11, CRep16_ChoshuMeisaiYear.FFldCostColTotal12,
			};
		}

		/// <summary>
		/// 出力用ビュー作成
		/// <param name="fromId">出力対象会員From</param>
		/// <param name="toId">出力対象会員To</param>
		/// <param name="fromZaiseki">出力対象在籍区分From</param>
		/// <param name="toZaiseki">出力対象在籍区分To</param>
		/// <param name="fromIryoId">出力対象医療機関From</param>
		/// <param name="toIryoId">出力対象医療機関To</param>
		/// </summary>
		public void CreateView(object fromId = null, object toId = null, object fromZaiseki = null, object toZaiseki = null, object fromIryoId = null, object toIryoId = null)
		{
			// 出力用テーブルの作成
			DataTable dt = new DataTable();
			dt.Columns.Add(F_Line, typeof(int));
			dt.Columns.Add(F_Page, typeof(int));
			dt.Columns.Add(F_ID_Kaiin, typeof(int));
			dt.Columns.Add(F_ID_Kaihi, typeof(int));
			dt.Columns.Add(F_CD_Kaiin, typeof(int));
			dt.Columns.Add(F_KaihiName, typeof(string));
			for (int i = 0; i < 12; i++)
			{
				dt.Columns.Add(F_Costs[i], typeof(string));
			}
			dt.Columns.Add(F_CostTotalKaihi, typeof(string));

			DBView dv = new DBView(dt);
			dv.SortQuery($"{F_ID_Kaiin},{F_Page},{F_Line}");

			// 年指定のため、該当年の1月1日を作成
			DateTime taishoDateFrom = new DateTime(SelectedDateYM.Year, 1, 1);
			DateTime taishoDateTo = taishoDateFrom.AddYears(1); // 次年の1月1日 ※Whereでは含まないよう設定

			dvHikiotoshi = new DBView(AppGlobal.DB.FillQuery($"SELECT * FROM {TableProp.t_hikiotoshi} WHERE {t_hikiotoshi.FHiki_DateYM} >= #{taishoDateFrom}# AND {t_hikiotoshi.FHiki_DateYM } < #{taishoDateTo}#"));
			dvHikiotoshi.SortQuery($"{t_hikiotoshi.FID_Kaiin}, {t_hikiotoshi.FID_Kaihi}");

			// 対象年に引落データあり
			if (dvHikiotoshi.Count > 0)
			{
				string where_kaiin = $"1 = 1";

				Kaiin fromKaiin = AppGlobal.Kaiins.Get(Cast.Int(fromId));
				Kaiin toKaiin	= AppGlobal.Kaiins.Get(Cast.Int(toId));

				// コードの指定が降順の場合の対応
				if (fromKaiin != null && toKaiin != null && fromKaiin.CD > toKaiin.CD)
				{
					Kaiin kai = fromKaiin;
					fromKaiin	= toKaiin;
					toKaiin		= kai;
				}
			
				if (fromKaiin != null) where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin} >= {fromKaiin.CD}"; 
				if (toKaiin != null)   where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin} <= {toKaiin.CD}";

				// 条件:在籍区分
				int fromZai = Cast.Int(fromZaiseki);
				int toZai = Cast.Int(toZaiseki);

				// コードの指定が降順の場合の対応
				if (fromZai != 0 && toZai != 0 && fromZai > toZai)
				{
					int zai = fromZai;
					fromZai = toZai;
					toZai = zai;
				}

				if (fromZai != 0) where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FKaiin_TypeZaiseki} >= {fromZai}";
				if (toZai != 0) where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FKaiin_TypeZaiseki} <= {toZai}";

				// 条件:医療機関
				IryoKikan fromIryo = AppGlobal.IryoKikans.Get(Cast.Int(fromIryoId));
				IryoKikan toIryo = AppGlobal.IryoKikans.Get(Cast.Int(toIryoId));

				// コードの指定が降順の場合の対応
				if (fromIryo != null && toIryo != null && fromIryo.CD > toIryo.CD)
				{
					IryoKikan ir = fromIryo;
					fromIryo = toIryo;
					toIryo = ir;
				}

				if (fromIryo != null) where_kaiin += $" AND {TableProp.t_iryokikan}.{t_iryokikan.FIRK_Code} >= {fromIryo.CD}";
				if (toIryo != null) where_kaiin += $" AND {TableProp.t_iryokikan}.{t_iryokikan.FIRK_Code} <= {toIryo.CD}";

				// 会員情報
				string sqlstr = $"SELECT {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin} FROM {TableProp.t_kaiin} LEFT OUTER JOIN {TableProp.t_iryokikan} ON {TableProp.t_kaiin}.{t_kaiin.FID_Iryokikan} = {TableProp.t_iryokikan}.{t_iryokikan.FID_Iryokikan}";
				dvKaiin = new DBView(AppGlobal.DB.FillQuery($"{sqlstr} WHERE {where_kaiin}"));

				// 会員別に集計の準備
				// Dictionary<会員CD, <会費ID, 引落情報(1-12月)>>
				SortedDictionary<int, Dictionary<int, HikiotoshiData>> dics = new SortedDictionary<int, Dictionary<int, HikiotoshiData>>();
				dics_shiharaiKoza = new SortedDictionary<int, List<eShiharai>>();
				dics_costTotal = new SortedDictionary<int, Decimal?>();
				dics_costTotalMonth = new SortedDictionary<int, Dictionary<int, Decimal?>>();
				dics_monthNyukai = new Dictionary<int, int>();
				dics_monthTaikai = new Dictionary<int, int>();

				maxpages = new Dictionary<int, int>();

				// --会員のループ--
				for (int i = 0; i < dvKaiin.Count; i++)
				{
					// 会員情報取得
					t_kaiin xrow = new t_kaiin(dvKaiin[i]);
					//Kaiin kin = AppGlobal.Kaiins.Get(xrow.ID_Kaiin);
					Kaiin kin = AppGlobal.Kaiins.GetCode(xrow.CD_Kaiin);

					if (kin != null)
					{
						dvHikiotoshi.RowFilterQuery($"{t_hikiotoshi.FID_Kaiin} = {kin.ID}"); // 条件の会員でフィルタ

						// 会費管理用Dictionary
						Dictionary<int, HikiotoshiData> dics_kaihi = new Dictionary<int, HikiotoshiData>();

						if (dics.ContainsKey(kin.CD) == false)
						{
							dics.Add(kin.CD, new Dictionary<int, HikiotoshiData>());
							dics_shiharaiKoza.Add(kin.CD, new List<eShiharai>());
							dics_costTotal.Add(kin.CD, new Decimal());
							dics_costTotalMonth.Add(kin.CD, new Dictionary<int, Decimal?>());
							dics_monthNyukai.Add(kin.CD, 0); // 初期値0
							dics_monthTaikai.Add(kin.CD, 13); // 初期値13
						}

						// 対象月(入会)
						// 指定年が入会年月日の年である場合、入会月以前を空欄表示 の準備
						if (AppDate.GetYear(SelectedDateYM) == AppDate.GetYear(kin.XRow.Kaiin_DateNyukai))
						{
							string nyukai_ym = AppDate.GetYearMonthSeirekiSlash(kin.XRow.Kaiin_DateNyukai);
							dics_monthNyukai[kin.CD] = Cast.Int(nyukai_ym.Substring(nyukai_ym.Length - 2));
						}
						// 対象月(退会)
						// 指定年が退会年月日の年である場合、退会月以降を空欄表示 の準備
						// ※在籍区分:退会 かつ 退会年月日:値あり 以外は考慮しない
						if (kin.XRow.Kaiin_TypeZaiseki == eTypeZaiseki.Taikai && kin.XRow.Kaiin_DateTaikai_Null != null &&
							AppDate.GetYear(SelectedDateYM) == AppDate.GetYear(kin.XRow.Kaiin_DateTaikai))
						{
							string taikai_ym = AppDate.GetYearMonthSeirekiSlash(kin.XRow.Kaiin_DateTaikai);
							dics_monthTaikai[kin.CD] = Cast.Int(taikai_ym.Substring(taikai_ym.Length - 2));
						}

						// 引落データ
						for (int j = 0; j < dvHikiotoshi.Count; j++)
						{
							t_hikiotoshi tmprow = new t_hikiotoshi(dvHikiotoshi[j]);

							// 引落データの会費情報(コード,名称)
							Kaihi kh = AppGlobal.Kaihis.Get(tmprow.ID_Kaihi);
							if (kh != null)
							{
								if (dics_kaihi.ContainsKey(tmprow.ID_Kaihi) == false)
								{
									dics_kaihi.Add(tmprow.ID_Kaihi, new HikiotoshiData());

									// 引落情報の格納
									dics_kaihi[tmprow.ID_Kaihi].Kaiin = kin;
									dics_kaihi[tmprow.ID_Kaihi].ID_Kaihi = kh.ID;
									dics_kaihi[tmprow.ID_Kaihi].CD_Kaihi = kh.CD;
									dics_kaihi[tmprow.ID_Kaihi].KaihiName = kh.XRow.Kaihi_Name_Null;
									dics_kaihi[tmprow.ID_Kaihi].CostTotalKaihi = 0; // 行合計の初期化
								}

								// 引落データが何月か
								string selected_ym = AppDate.GetYearMonthSeirekiSlash(tmprow.Hiki_DateYM);
								int selectedMonth = Cast.Int(selected_ym.Substring(selected_ym.Length - 2));

								if (selectedMonth >= dics_monthNyukai[kin.CD] && selectedMonth <= dics_monthTaikai[kin.CD])
								{
									// 該当会費の該当月へ値をセット
									dics_kaihi[tmprow.ID_Kaihi].dics_cost[selectedMonth] = tmprow.Hiki_Cost; // 月額
									dics_kaihi[tmprow.ID_Kaihi].CostTotalKaihi += tmprow.Hiki_Cost; // 該当月を行合計に含める
									AddFee(dics_costTotalMonth, kin.CD, selectedMonth, tmprow.Hiki_Cost); // 該当月の列合計に含める
									dics_costTotal[kin.CD] += tmprow.Hiki_Cost; // 全体合計へ1レコードずつ加算
								}

								// 支払方法リストの処理
								if (dics_shiharaiKoza[kin.CD].Contains(tmprow.Hiki_Shiharai) == false)
								{
									dics_shiharaiKoza[kin.CD].Add(tmprow.Hiki_Shiharai);
								}
							}
						}

						// 会員ごとに会費Dicを追加
						dics[kin.CD] = dics_kaihi;
					}
				}

				int page = 1;

				// 出力用DBViewにデータ行を追加
				foreach(var kvp in dics)
				{
					Kaiin kin = AppGlobal.Kaiins.GetCode(kvp.Key);

					int line = 0;
					int no = 0;

					foreach (var kvp2 in kvp.Value) // 会費ID-月別会費
					{
						DataRow nrow = null;

						if (no < LINE_MAX_PAGE)
						{
							nrow = dt.NewRow();
							nrow[F_KaihiName] = kvp2.Value.KaihiName;
							for (int j = 1; j <= 12; j++)
							{
								if (kvp2.Value.dics_cost.TryGetValue(j, out Decimal? dec))
								{
									nrow[F_Costs[j - 1]] = kvp2.Value.dics_cost[j];
								}
								else
								{
									nrow[F_Costs[j - 1]] = 0; // 空欄用
								}
							}
							nrow[F_CostTotalKaihi] = kvp2.Value.CostTotalKaihi;

							nrow[F_ID_Kaiin] = kin.ID;
							nrow[F_ID_Kaihi] = kvp2.Key;
							nrow[F_CD_Kaiin] = kin.CD;
							nrow[F_Line] = line;
							nrow[F_Page] = page;

							dv.Add(nrow);
							line++;
						}

						no++;
						if (no >= LINE_MAX_PAGE)
						{
							page++;
							line = 0;
							no = 0;
						}

					}

					if (line == LINE_MAX_PAGE) 
					{
						// 最大行まで表示されていれば、対象者が変わるので改ページ
						page = 1;
					}
					else
					if (line > 0)
					{
						// 残行を空欄で埋めて改ページ
						for (int i = line; i < LINE_MAX_PAGE; i++)
						{
							DataRow nrow = dv.NewRow();
							nrow[F_ID_Kaiin]	= kin.ID;
							nrow[F_Line]		= line;
							nrow[F_Page]		= page;
							dv.Add(nrow);

							line++;
							if (line >= LINE_MAX_PAGE)
							{
								page = 1;
								line = 0;
							}
						}
					}
				}
			}

			// 登録順のまま出力ビューへ反映
			OutView = new DBView(dt);

			// 会員別の最大ページ数を取得
			for (int i = 0; i < OutView.Count; i++)
			{
				int id	 = Cast.Int(OutView[i][F_ID_Kaiin]);
				int page = Cast.Int(OutView[i][F_Page]);

				if (maxpages.ContainsKey(id) == false)
				{
					maxpages.Add(id, 0);
				}
				maxpages[id] = page;
			}
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
													ReportProp.CRep16_ChoshuMeisaiYear,
													"年間会費徴収明細書",
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

			Kaiin kin = AppGlobal.Kaiins.Get(Cast.Int(row[F_ID_Kaiin]));

			string cd = "";
			string name = "";
			string post = "";
			string addr1 = "";
			string addr2 = "";
			string kikanname = "";
			string title = "";
			string pageno = "";

			if (kin != null)
			{
				post = $"〒{kin.XRow.Kaiin_Post}";
				cd	 = kin.CodeString;
				name = $"{kin.XRow.Kaiin_Name}　様";

				// 医療機関名, 送付先住所
				IryoKikan ik = AppGlobal.IryoKikans.Get(kin.XRow.ID_Iryokikan);
				if (ik != null)
				{
					kikanname = ik.XRow.IRK_Name_Null;
					addr1 = $"{ik.XRow.IRK_Addr1_Null}";
					addr2 = $"{ik.XRow.IRK_Addr2_Null}";
				}

				// 送付先住所(自宅指定の場合)
				if (kin.XRow.Kaiin_BunshoSofusaki == false)
				{
					addr1 = $"{kin.XRow.Kaiin_Addr1}";
					addr2 = $"{kin.XRow.Kaiin_Addr2}";
				}

				title = $"年間会費徴収明細書";

				if (maxpages.ContainsKey(kin.ID) == true && maxpages[kin.ID] > 1)
				{
					pageno = $"{row[F_Page]}/{maxpages[kin.ID]}";
				}
			}

			// 会員情報
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldCode, cd);
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldPost, post);
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldAddr1, addr1);
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldAddr2, addr2);
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldNameKikan, kikanname);
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldName, name);

			// タイトル
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldTitle, title);

			// 年表示
			string year = $"{AppDate.GetYearSeireki(SelectedDateYM)}年分";
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldDateYear, year);

			// 月分
			//string date = AppDate.GetYearMonth(SelectedDateYM)+"分";
			//setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldDateYear, date);

			// 会社名,TEL,FAX
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldCompanyName, AppGlobal.Basic.BAS_Name);
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldCompanyTel, "TEL:" + AppGlobal.Basic.BAS_Tel1);
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldCompanyFax, "FAX:" + AppGlobal.Basic.BAS_Fax);

			// 会費合計(列)
			for (int i = 1; i <= 12; i++)
			{
				if (i >= dics_monthNyukai[kin.CD] && i <= dics_monthTaikai[kin.CD])
				{
					if (dics_costTotalMonth[kin.CD].TryGetValue(i, out Decimal? dec))
					{
						setCost_zero(rep, repCostsColTotalField[i - 1], dec);
					}
					else
					{
						setCost_zero(rep, repCostsColTotalField[i - 1], 0); // 0表示
					}
				}
				else
				{
					setFieldValue(rep, repCostsColTotalField[i - 1], ""); // 空欄
				}
			}

			// 会費合計
			setCost_zero(rep, CRep16_ChoshuMeisaiYear.FFldCostTotal, dics_costTotal[kin.CD]);
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldCostTotalDisp, $"{Cast.Decimal(dics_costTotal[kin.CD]).ToString("#,##0")}円");

			// 銀行情報
			//string lblbank1 = "";
			string bankname1 = "";
			string bankshitenname1 = "";
			//string bankkozatype1 = "";
			//string bankkozano1 = "";
			//string lblbank2 = "";
			string bankname2 = "";
			string bankshitenname2 = "";
			//string bankkozatype2 = "";
			//string bankkozano2 = "";
			//string lblbank3 = "";
			string bankname3 = "";
			string bankshitenname3 = "";
			//string bankkozatype3 = "";
			//string bankkozano3 = "";

			for (int i = 0; i < dics_shiharaiKoza[kin.CD].Count; i++)
			{
				t_bank_code bank;
				
				switch (dics_shiharaiKoza[kin.CD][i])
				{
					case eShiharai.Koza1:
						bank = AppGlobal.BankCodeMg.GetBankCodeRow(kin.XRow.Kaiin_BankCode1_Null, kin.XRow.Kaiin_BankShitenCode1_Null);
						if (bank != null)
						{
							//lblbank1 = "扱銀行 １．";
							bankname1 = bank.BCD_FullName;
							bankshitenname1 = bank.BCD_NameShiten;
							//bankkozatype1 = enumKbn.DTypeKoza[(int)kin.XRow.Kaiin_BankKozaType1];
							//bankkozano1 = kin.XRow.Kaiin_BankKozaNo1;

						}
						break;

					case eShiharai.Koza2:
						bank = AppGlobal.BankCodeMg.GetBankCodeRow(kin.XRow.Kaiin_BankCode2_Null, kin.XRow.Kaiin_BankShitenCode2_Null);
						if (bank != null)
						{
							//lblbank2 = "扱銀行 ２．";
							bankname2 = bank.BCD_FullName;
							bankshitenname2 = bank.BCD_NameShiten;
							//bankkozatype2 = enumKbn.DTypeKoza[(int)kin.XRow.Kaiin_BankKozaType2];
							//bankkozano2 = kin.XRow.Kaiin_BankKozaNo2;
						}
						break;

					case eShiharai.Koza3:
						bank = AppGlobal.BankCodeMg.GetBankCodeRow(kin.XRow.Kaiin_BankCode3_Null, kin.XRow.Kaiin_BankShitenCode3_Null);
						if (bank != null)
						{
							//lblbank3 = "扱銀行 ３．";
							bankname3 = bank.BCD_FullName;
							bankshitenname3 = bank.BCD_NameShiten;
							//bankkozatype3 = enumKbn.DTypeKoza[(int)kin.XRow.Kaiin_BankKozaType3];
							//bankkozano3 = kin.XRow.Kaiin_BankKozaNo3;
						}
						break;
				}
			}

			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldBankName, bankname1);
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldBankNameShiten, bankshitenname1);

			//setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldBanklbl2, lblbank2);
			//setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldBankName2, bankname2);
			//setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldBankShitenName2, bankshitenname2);
			//setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldBankType2, bankkozatype2);
			//setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldBankNo2, bankkozano2);

			//setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldBanklbl3, lblbank3);
			//setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldBankName3, bankname3);
			//setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldBankShitenName3, bankshitenname3);
			//setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldBankType3, bankkozatype3);
			//setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldBankNo3, bankkozano3);

			// 発行日
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldDateOut, $"{DateTime.Now:yyyy年MM月dd日}");

			// ページ番号
			setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldPage, pageno);
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

			if (row[F_KaihiName] != DBNull.Value) // 会費名が空なら空行とみなす
			{
				int CdKaiin = Cast.Int(row[F_CD_Kaiin]);

				// 会費名
				setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldKaihiName, row[F_KaihiName]);
				// 会費1-12月 有効月は金額表示
				for (int i = 1; i <= 12; i++)
				{
					if (i >= dics_monthNyukai[CdKaiin] && i <= dics_monthTaikai[CdKaiin])
					{
						setCost_zero(rep, repCostsField[i - 1], row[F_Costs[i - 1]]);
					}
					else
					{
						setFieldValue(rep, repCostsField[i - 1], "");
					}
				}
				setCost_zero(rep, CRep16_ChoshuMeisaiYear.FFldCostRowTotal, row[F_CostTotalKaihi]);
			}
			else
			{
				// 各項目に空をセット
				setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldKaihiName, "");
				for (int i = 1; i <= 12; i++)
				{
					setFieldValue(rep, repCostsField[i - 1], "");
				}
				setFieldValue(rep, CRep16_ChoshuMeisaiYear.FFldCostRowTotal, "");
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
		/// 金額表示の編集 (0表示なし)
		/// </summary>
		/// <param name="rep"></param>
		/// <param name="fldname"></param>
		/// <param name="dec"></param>
		void setCost(C1FlexReport rep, string fldname, object val)
		{
			string cost = "";

			if (val != null)
			{
				cost = $"{Cast.Decimal(val).ToString("#,###")}";
			}

			setFieldValue(rep, fldname, cost);
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

		/// <summary>
		/// 列合計のDictionaryセット
		/// </summary>
		void AddFee(SortedDictionary<int, Dictionary<int, Decimal?>> dics, int memberId, int month, decimal? fee)
		{
			// ① 会員ID(key)がまだ存在しない場合は、月別Dictionaryを新規作成して登録
			if (!dics.TryGetValue(memberId, out Dictionary<int, decimal?> monthlyFeeDics))
			{
				monthlyFeeDics = new Dictionary<int, decimal?>();
				dics[memberId] = monthlyFeeDics;
			}

			// ② 該当月(key)がまだ存在しない場合は 0 として初期化、存在する場合は加算
			if (monthlyFeeDics.TryGetValue(month, out decimal? currentFee))
			{
				monthlyFeeDics[month] = currentFee + fee;
			}
			else
			{
				monthlyFeeDics[month] = fee;
			}
		}
	}
}

