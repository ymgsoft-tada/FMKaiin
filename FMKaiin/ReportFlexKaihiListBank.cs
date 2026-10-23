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
	/// 会費種別一覧表（銀行有）
	/// </summary>
	public class ReportFlexKaihiListBank
	{
		/// <summary>レポートファイル名</summary>
		private string ReportFile = $"{AppConst.ReportFolder}{ReportProp.CRepKaihiFilename}";

		private int LINE_MAX_PAGE = 13;

		/// <summary>選択中の月度</summary>
		public DateTime SelectedDateYM { get; set; }
		/// <summary>出力するビュー</summary>
		public DBView OutView { get; private set;}

		// 出力用DataTableのカラム
		const string F_Line		= "F_Line";
		const string F_Page		= "F_Page";
		const string F_KaihiType = "F_KaihiType";
		const string F_CD_Kaihi = "F_CD_Kaihi";
		const string F_KaihiName = "F_KaihiName";
		const string F_Cost1	= "F_Cost1";
		const string F_Cost2 = "F_Cost2";
		const string F_Cost3 = "F_Cost3";
		const string F_Cost4 = "F_Cost4";
		const string F_Cost5 = "F_Cost5";
		const string F_Cost6 = "F_Cost6";
		const string F_Cost7 = "F_Cost7";
		const string F_Cost8 = "F_Cost8";
		const string F_Cost9 = "F_Cost9";
		const string F_Cost10 = "F_Cost10";
		const string F_Cost11 = "F_Cost11";
		const string F_Cost12 = "F_Cost12";
		const string F_CostTotal = "F_CostTotal";
		const string F_BankName = "F_BankName";
		const string F_BankNameShiten = "F_BankNameShiten";
		const string F_BankKozaType = "F_BankKozaType";
		const string F_BankKozaNo = "F_BankKozaNo";
		const string F_BankKozaName = "F_BankKozaName";
		const string F_BankIfax = "F_BankIfax";

		/// <summary>会員ごと最大ページ数</summary>
		Dictionary<int, int> maxpages;

		DBView dvKaihi;

		/// <summary>
		/// 会費管理クラス(明細1行)
		/// </summary>
		class KaihiData
		{
			public eTypeKaihi KaihiType;
			public int CD_Kaihi;
			public string KaihiName;
			public decimal? Cost1;
			public decimal? Cost2;
			public decimal? Cost3;
			public decimal? Cost4;
			public decimal? Cost5;
			public decimal? Cost6;
			public decimal? Cost7;
			public decimal? Cost8;
			public decimal? Cost9;
			public decimal? Cost10;
			public decimal? Cost11;
			public decimal? Cost12;
			public decimal? CostTotal;
			public int BankCode;
			public string BankName;
			public int BankCodeShiten;
			public string BankNameShiten;
			public eTypeKoza BankKozaType;
			public string BankKozaNo;
			public string BankKozaName;
			public int BankIfax;

			/// <summary>
			/// コンストラクタ
			/// </summary>
			public KaihiData()
			{
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ReportFlexKaihiListBank()
		{
		}

		/// <summary>
		/// 出力用ビュー作成
		/// <param name="fromKbnCd">出力対象区分コードFrom</param>
		/// <param name="toKbnCd">出力対象区分コードTo</param>
		/// <param name="fromCd">出力対象コードFrom</param>
		/// <param name="toCd">出力対象コードTo</param>
		/// </summary>
		public void CreateView(object fromKbnCd = null, object toKbnCd = null, object fromCd = null, object toCd = null)
		{
			// 出力用テーブルの作成
			DataTable dt = new DataTable();
			dt.Columns.Add(F_KaihiType, typeof(int));
			dt.Columns.Add(F_Line, typeof(int));
			dt.Columns.Add(F_Page, typeof(int));
			dt.Columns.Add(F_CD_Kaihi, typeof(int));
			dt.Columns.Add(F_KaihiName, typeof(string));
			dt.Columns.Add(F_Cost1, typeof(string));
			dt.Columns.Add(F_Cost2, typeof(string));
			dt.Columns.Add(F_Cost3, typeof(string));
			dt.Columns.Add(F_Cost4, typeof(string));
			dt.Columns.Add(F_Cost5, typeof(string));
			dt.Columns.Add(F_Cost6, typeof(string));
			dt.Columns.Add(F_Cost7, typeof(string));
			dt.Columns.Add(F_Cost8, typeof(string));
			dt.Columns.Add(F_Cost9, typeof(string));
			dt.Columns.Add(F_Cost10, typeof(string));
			dt.Columns.Add(F_Cost11, typeof(string));
			dt.Columns.Add(F_Cost12, typeof(string));
			dt.Columns.Add(F_CostTotal, typeof(string));
			dt.Columns.Add(F_BankName, typeof(string));
			dt.Columns.Add(F_BankNameShiten, typeof(string));
			dt.Columns.Add(F_BankKozaType, typeof(string));
			dt.Columns.Add(F_BankKozaNo, typeof(string));
			dt.Columns.Add(F_BankKozaName, typeof(string));
			dt.Columns.Add(F_BankIfax, typeof(string));

			DBView dv = new DBView(dt);
			dv.SortQuery($"{F_CD_Kaihi},{F_Page},{F_Line}"); // 出力用:会費コード順

			//dvKaiin = new DBView(AppGlobal.DB.FillQuery($"SELECT * FROM {TableProp.t_kaiin} WHERE {t_kaiin.FKYU_DateYM} = #{SelectedDateYM}#"));
			//dvKaihi = new DBView(AppGlobal.DB.FillQuery($"SELECT * FROM {TableProp.t_kaihi}"));
			//dvKaihi.SortQuery($"{t_kyuyo.FKYU_DateYM}, {t_kyuyo.FID_Staff}");
			//dvKaihi.SortQuery($"{t_kaihi.FID_Kaihi}");

			// 抽出条件作成
			string where_kaihi = $"1 = 1";

			// 会費区分コード条件
			//eTypeKaihi fromKbnKaihi = (eTypeKaihi)fromKbnCd;
			//eTypeKaihi toKbnKaihi = (eTypeKaihi)toKbnCd;
			int fromKbnKaihi = Cast.Int(fromKbnCd);
			int toKbnKaihi = Cast.Int(toKbnCd);

			// コードの指定が降順の場合の対応
			if (fromKbnKaihi > (int)eTypeKaihi.None && toKbnKaihi > (int)eTypeKaihi.None && fromKbnKaihi > toKbnKaihi)
			{
				int kkbn = fromKbnKaihi;
				fromKbnKaihi = toKbnKaihi;
				toKbnKaihi = kkbn;
			}

			if (fromKbnKaihi > (int)eTypeKaihi.None) where_kaihi += $" AND {TableProp.t_kaihi}.{t_kaihi.FKaihi_KaihiType} >= {fromKbnKaihi}";
			if (toKbnKaihi > (int)eTypeKaihi.None) where_kaihi += $" AND {TableProp.t_kaihi}.{t_kaihi.FKaihi_KaihiType} <= {toKbnKaihi}";

			// 会費コード条件
			Kaihi fromKaihi = AppGlobal.Kaihis.Get(Cast.Int(fromCd));
			Kaihi toKaihi	= AppGlobal.Kaihis.Get(Cast.Int(toCd));

			// コードの指定が降順の場合の対応
			if (fromKaihi != null && toKaihi != null && fromKaihi.CD > toKaihi.CD)
			{
				Kaihi kai	= fromKaihi;
				fromKaihi = toKaihi;
				toKaihi = kai;
			}
			
			if (fromKaihi != null) where_kaihi += $" AND {TableProp.t_kaihi}.{t_kaihi.FCD_Kaihi} >= {fromKaihi.CD}"; 
			if (toKaihi != null)   where_kaihi += $" AND {TableProp.t_kaihi}.{t_kaihi.FCD_Kaihi} <= {toKaihi.CD}";

			// 会費情報
			dvKaihi = new DBView(AppGlobal.DB.FillQuery($"SELECT * FROM {TableProp.t_kaihi} WHERE {where_kaihi}"));

			// 会費マスタが0件でなければ処理
			if (dvKaihi.Count > 0)
			{
				// 会費区分別に集計しておく
				SortedDictionary<int, List<KaihiData>> dics  = new SortedDictionary<int, List<KaihiData>>();

				maxpages = new Dictionary<int, int>();

				// 会費区分コードのループ
				foreach (var kvp in enumKbn.DTypeKaihi)
				{
					// 会費区分コードのdicsを作成
					dics.Add(kvp.Key, new List<KaihiData>());

					// 該当会費区分コード情報へフィルタ
					dvKaihi.RowFilterQuery($"{t_kaihi.FKaihi_KaihiType} = {kvp.Key}");

					// 会費情報のループ
					for (int i = 0; i < dvKaihi.Count; i++)
					{
						KaihiData kd;
						t_bank_code bank;

						// 会費情報の格納
						kd = new KaihiData();
						t_kaihi krow = new t_kaihi(dvKaihi[i].Row);

						kd.KaihiType = krow.Kaihi_KaihiType;
						kd.CD_Kaihi = krow.CD_Kaihi;
						kd.KaihiName = krow.Kaihi_Name_Null;
						kd.Cost1 = krow.Kaihi_GetsugakuCost1_Null;
						kd.Cost2 = krow.Kaihi_GetsugakuCost2_Null;
						kd.Cost3 = krow.Kaihi_GetsugakuCost3_Null;
						kd.Cost4 = krow.Kaihi_GetsugakuCost4_Null;
						kd.Cost5 = krow.Kaihi_GetsugakuCost5_Null;
						kd.Cost6 = krow.Kaihi_GetsugakuCost6_Null;
						kd.Cost7 = krow.Kaihi_GetsugakuCost7_Null;
						kd.Cost8 = krow.Kaihi_GetsugakuCost8_Null;
						kd.Cost9 = krow.Kaihi_GetsugakuCost9_Null;
						kd.Cost10 = krow.Kaihi_GetsugakuCost10_Null;
						kd.Cost11 = krow.Kaihi_GetsugakuCost11_Null;
						kd.Cost12 = krow.Kaihi_GetsugakuCost12_Null;
						kd.CostTotal = krow.Kaihi_GetsugakuCost1 + krow.Kaihi_GetsugakuCost2 + krow.Kaihi_GetsugakuCost3 + krow.Kaihi_GetsugakuCost4 +
									   krow.Kaihi_GetsugakuCost5 + krow.Kaihi_GetsugakuCost6 + krow.Kaihi_GetsugakuCost7 + krow.Kaihi_GetsugakuCost8 +
									   krow.Kaihi_GetsugakuCost9 + krow.Kaihi_GetsugakuCost10 + krow.Kaihi_GetsugakuCost11 + krow.Kaihi_GetsugakuCost12;

						bank = AppGlobal.BankCodeMg.GetBankCodeRow(krow.Kaihi_BankCode_Null, krow.Kaihi_BankCodeShiten_Null);
						if (bank != null)
						{
							kd.BankCode = krow.Kaihi_BankCode;
							kd.BankName = bank.BCD_FullName;
							kd.BankCodeShiten = krow.Kaihi_BankCodeShiten;
							kd.BankNameShiten = bank.BCD_NameShiten;
							kd.BankKozaType = krow.Kaihi_BankKozaType;
							kd.BankKozaNo = krow.Kaihi_BankKozaNo;
							kd.BankKozaName = krow.Kaihi_BankKozaName;
							kd.BankIfax = krow.Kaihi_BankIfax;
						}

						// 会費区分コードごとに会費リスト追加
						dics[kvp.Key].Add(kd);
					}
				}

				int page = 1;

				// 出力用DBViewにデータ行を追加
				foreach(var kvp in dics)
				{
					//					kvp.Value.Sort((a, b) => a.Day - b.Day);

					int kaihitype = kvp.Key;

					int line	= 0;
					int no		= 0;
					for(int i = 0; i < kvp.Value.Count; i++)
					{
						KaihiData kd = kvp.Value[i];
						DataRow nrow = null;

						//string day = $"{kd.Day}";

						if (no < LINE_MAX_PAGE)
						{
							string kaihiNm = kd.CD_Kaihi == 0 ? null : $"{Cast.String(kd.CD_Kaihi).PadLeft(5, '0')} {kd.KaihiName}";
							string bankNm = kd.BankCode == 0 ? null : $"{Cast.String(kd.BankCode).PadLeft(4, '0')} {kd.BankName}";
							string bankshitenNm = kd.BankCodeShiten == 0 ? null : $"{Cast.String(kd.BankCodeShiten).PadLeft(3, '0')} {kd.BankNameShiten}";
							string bankkozatypeNm = kd.BankKozaType == eTypeKoza.None ? null : $"{(int)kd.BankKozaType} {enumKbn.DTypeKoza[(int)kd.BankKozaType]}";
							string bankifax = kd.BankIfax == 0 ? null : Cast.String(kd.BankIfax).PadLeft(2, '0');

							nrow = dt.NewRow();
							nrow[F_KaihiType] = kd.KaihiType;
							nrow[F_CD_Kaihi] = kd.CD_Kaihi;
							nrow[F_KaihiName] = kaihiNm;
							nrow[F_Cost1] = kd.Cost1;
							nrow[F_Cost2] = kd.Cost2;
							nrow[F_Cost3] = kd.Cost3;
							nrow[F_Cost4] = kd.Cost4;
							nrow[F_Cost5] = kd.Cost5;
							nrow[F_Cost6] = kd.Cost6;
							nrow[F_Cost7] = kd.Cost7;
							nrow[F_Cost8] = kd.Cost8;
							nrow[F_Cost9] = kd.Cost9;
							nrow[F_Cost10] = kd.Cost10;
							nrow[F_Cost11] = kd.Cost11;
							nrow[F_Cost12] = kd.Cost12;
							nrow[F_CostTotal] = kd.CostTotal;
							nrow[F_BankName] = bankNm;
							nrow[F_BankNameShiten] = bankshitenNm;
							nrow[F_BankKozaType] = bankkozatypeNm;
							nrow[F_BankKozaNo] = kd.BankKozaNo;
							nrow[F_BankKozaName] = kd.BankKozaName;
							nrow[F_BankIfax] = bankifax;
							nrow[F_Line] = line;
							nrow[F_Page] = page;
							dv.Add(nrow);
							line++;
						}

						no++;
						//if (no >= LINE_MAX_PAGE*2)
						if (no >= LINE_MAX_PAGE)
						{
							page++;
							line	= 0;
							no		= 0;
						}
					}

					if (line == LINE_MAX_PAGE) 
					{
						// 最大行まで表示されていれば、対象が変わるので改ページ
						page = 1;
					}
					else
					if (line > 0)
					{
						// 残行を空欄で埋めて改ページ
						for (int i = line; i < LINE_MAX_PAGE; i++)
						{
							DataRow nrow = dv.NewRow();
							nrow[F_KaihiType]	= kaihitype;
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

			// 会費区分コード別の最大ページ数を取得
			for (int i = 0; i < OutView.Count; i++)
			{
				int id	 = Cast.Int(OutView[i][F_KaihiType]);
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
													ReportProp.CRep07_KaihiListBank,
													"会費種別一覧表（銀行有）",
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

			int kaihiKbn = Cast.Int(row[F_KaihiType]);
			string kaihiKbnName = enumKbn.DTypeKaihi[kaihiKbn];
			string title = $"会費種別一覧表（銀行有）";
			string pageno = "";
			if (maxpages.ContainsKey(kaihiKbn) == true && maxpages[kaihiKbn] > 1)
			{
				pageno = $"{row[F_Page]}/{maxpages[kaihiKbn]}";
			}

			// タイトル
			setFieldValue(rep, CRep07_KaihiListBank.FFldTitle, title);

			// 月分
			//string date = AppDate.GetYearMonth(SelectedDateYM)+"分";
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldDateYM, date);

			// 会社名,TEL,FAX
			setFieldValue(rep, CRep07_KaihiListBank.FFldCompanyName, AppGlobal.Basic.BAS_Name);

			// 会費区分コード情報
			setFieldValue(rep, CRep07_KaihiListBank.FFldKaihiKbnCode, Cast.String(kaihiKbn));
			setFieldValue(rep, CRep07_KaihiListBank.FFldKaihiKbnName, kaihiKbnName);

			// 作成日時
			setFieldValue(rep, CRep07_KaihiListBank.FFldDate, $"{DateTime.Now:yyyy/MM/dd HH:mm} 作成");

			// ページ番号
			setFieldValue(rep, CRep07_KaihiListBank.FFldPage, pageno); // 現行踏襲かどうか(他と合わせる
		}

		private void Repinfo_StartSection(object sender, ReportFlex.ReportEventArgsEx e)
		{
			C1FlexReport rep = (C1FlexReport)sender;

			int lineNo = (e.Page - 1) * LINE_MAX_PAGE + e.Line;

			// 最後のLine
			//ShapeField end1 = (ShapeField)rep.Fields[CRep05_KaihiUchiwakeYear.FFldLineEnd1];
			//ShapeField end2 = (ShapeField)rep.Fields[CRep05_KaihiUchiwakeYear.FFldLineEnd2];

			//if (e.Line == LINE_MAX_PAGE-1)
			//{
			//	end1.Line.Width =
			//	end2.Line.Width = 15;
			//}
			//else
			//{
			//	end1.Line.Width =
			//	end2.Line.Width = 5;
			//}
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
			TextField f;

			setFieldValue(rep, CRep07_KaihiListBank.FFldKaihiName, row[F_KaihiName]);
			setCost(rep, CRep07_KaihiListBank.FFldCost1, row[F_Cost1]);
			setCost(rep, CRep07_KaihiListBank.FFldCost2, row[F_Cost2]);
			setCost(rep, CRep07_KaihiListBank.FFldCost3, row[F_Cost3]);
			setCost(rep, CRep07_KaihiListBank.FFldCost4, row[F_Cost4]);
			setCost(rep, CRep07_KaihiListBank.FFldCost5, row[F_Cost5]);
			setCost(rep, CRep07_KaihiListBank.FFldCost6, row[F_Cost6]);
			setCost(rep, CRep07_KaihiListBank.FFldCost7, row[F_Cost7]);
			setCost(rep, CRep07_KaihiListBank.FFldCost8, row[F_Cost8]);
			setCost(rep, CRep07_KaihiListBank.FFldCost9, row[F_Cost9]);
			setCost(rep, CRep07_KaihiListBank.FFldCost10, row[F_Cost10]);
			setCost(rep, CRep07_KaihiListBank.FFldCost11, row[F_Cost11]);
			setCost(rep, CRep07_KaihiListBank.FFldCost12, row[F_Cost12]);
			setCost(rep, CRep07_KaihiListBank.FFldCostTotal, row[F_CostTotal]);

			// 銀行名(フォントサイズ調整)
			f = (TextField)rep.Fields[CRep07_KaihiListBank.FFldBankName];
			if (Cast.String(row[F_BankName]).Length > 15)
			{
				f.Font.Size = 6;
			}
			else
			{
				f.Font.Size = (float)8.25;
			}
			setFieldValue(rep, CRep07_KaihiListBank.FFldBankName, row[F_BankName]);

			// 銀行支店名(フォントサイズ調整)
			f = (TextField)rep.Fields[CRep07_KaihiListBank.FFldBankNameShiten];
			if (Cast.String(row[F_BankNameShiten]).Length > 10)
			{
				f.Font.Size = 6;
			}
			else
			{
				f.Font.Size = (float)8.25;
			}
			setFieldValue(rep, CRep07_KaihiListBank.FFldBankNameShiten, row[F_BankNameShiten]);

			setFieldValue(rep, CRep07_KaihiListBank.FFldBankKozaType, row[F_BankKozaType]);
			setFieldValue(rep, CRep07_KaihiListBank.FFldBankKozaNo, row[F_BankKozaNo]);

			// 銀行口座名義(フォントサイズ調整)
			f = (TextField)rep.Fields[CRep07_KaihiListBank.FFldBankKozaName];
			if (Cast.String(row[F_BankKozaName]).Length > 25)
			{
				f.Font.Size = 6;
			}
			else
			{
				f.Font.Size = (float)8.25;
			}
			setFieldValue(rep, CRep07_KaihiListBank.FFldBankKozaName, row[F_BankKozaName]);

			setFieldValue(rep, CRep07_KaihiListBank.FFldBankIfax, row[F_BankIfax]);
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
		/// 金額表示の編集
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

