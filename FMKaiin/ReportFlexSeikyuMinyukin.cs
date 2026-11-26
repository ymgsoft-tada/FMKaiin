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
	/// 未入金者用請求書
	/// </summary>
	public class ReportFlexSeikyuMinyukin
	{
		/// <summary>レポートファイル名</summary>
		private string ReportFile = $"{AppConst.ReportFolder}{ReportProp.CRepHikiotoshiFilename}";

		private int LINE_MAX_PAGE = 10;

		/// <summary>選択中の年月</summary>
		public DateTime SelectedDateYM { get; set; }
		/// <summary>出力するビュー</summary>
		public DBView OutView { get; private set;}

		// 出力用DataTableのカラム
		const string F_Line		= "F_Line";
		const string F_Page		= "F_Page";
		const string F_ID_Kaiin = "F_ID_Kaiin";
		const string F_CodeKaihi = "F_CodeKaihi";
		const string F_KaihiName = "F_KaihiName";
		const string F_Cost	= "F_Cost";

		/// <summary>会員ごと最大ページ数</summary>
		Dictionary<int, int> maxpages;

		DBView dvHikiotoshi;
		DBView dvKaiin;

		SortedDictionary<int, Decimal?> dics_costTotal;

		/// <summary>
		/// 引落管理クラス(明細1行)
		/// </summary>
		class HikiotoshiData
		{
			public Kaiin Kaiin;
			public int CodeKaihi;
			public string KaihiName;
			public decimal? Cost;

			/// <summary>
			/// コンストラクタ
			/// </summary>
			public HikiotoshiData()
			{
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ReportFlexSeikyuMinyukin()
		{
		}

		/// <summary>
		/// 出力用ビュー作成
		/// <param name="fromId">出力対象会員From</param>
		/// <param name="toId">出力対象会員To</param>
		/// <param name="fromShiharai">出力対象支払方法From</param>
		/// <param name="toShiharai">出力対象支払方法To</param>
		/// </summary>
		public void CreateView(object fromId = null, object toId = null, object fromShiharai = null, object toShiharai = null)
		{
			// 出力用テーブルの作成
			DataTable dt = new DataTable();
			dt.Columns.Add(F_Line, typeof(int));
			dt.Columns.Add(F_Page, typeof(int));
			dt.Columns.Add(F_ID_Kaiin, typeof(int));
			dt.Columns.Add(F_CodeKaihi, typeof(string));
			dt.Columns.Add(F_KaihiName, typeof(string));
			dt.Columns.Add(F_Cost, typeof(string));

			DBView dv = new DBView(dt);
			dv.SortQuery($"{F_ID_Kaiin},{F_Page},{F_Line}");

			// 年指定のため、該当年の1月1日を作成
			//DateTime taishoDateFrom = new DateTime(SelectedDateYM.Year, 1, 1);
			//DateTime taishoDateTo = taishoDateFrom.AddYears(1); // 次年の1月1日 ※Whereでは含まないよう設定
			//dvHikiotoshi = new DBView(AppGlobal.DB.FillQuery($"SELECT * FROM {TableProp.t_hikiotoshi} WHERE {t_hikiotoshi.FHiki_DateYM} >= #{taishoDateFrom}# AND {t_hikiotoshi.FHiki_DateYM } < #{taishoDateTo}#"));

			string where_hikiotoshi = $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM} = #{SelectedDateYM}#"; // 対象年月

			// 抽出条件:支払方法
			int fromSh = Cast.Int(fromShiharai);
			int toSh = Cast.Int(toShiharai);

			// コードの指定が降順の場合の対応
			if (fromSh != 0 && toSh != 0 && fromSh > toSh)
			{
				int sh = fromSh;
				fromSh = toSh;
				toSh = sh;
			}

			if (fromSh != 0) where_hikiotoshi += $" AND {TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_Shiharai} >= {fromSh}";
			if (toSh != 0) where_hikiotoshi += $" AND {TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_Shiharai} <= {toSh}";

			dvHikiotoshi = new DBView(AppGlobal.DB.FillQuery($"SELECT * FROM {TableProp.t_hikiotoshi} WHERE {where_hikiotoshi}"));
			dvHikiotoshi.SortQuery($"{t_hikiotoshi.FID_Kaiin}, {t_hikiotoshi.FID_Kaihi}");

			// 対象年月に引落データあり
			if (dvHikiotoshi.Count > 0)
			{
				string where_kaiin = $"1 = 1";

				Kaiin fromKaiin = AppGlobal.Kaiins.Get(Cast.Int(fromId));
				Kaiin toKaiin	= AppGlobal.Kaiins.Get(Cast.Int(toId));

				// コードの指定が降順の場合の対応
				if (fromKaiin != null && toKaiin != null && fromKaiin.CD > toKaiin.CD)
				{
					Kaiin kai	= fromKaiin;
					fromKaiin	= toKaiin;
					toKaiin		= kai;
				}
			
				if (fromKaiin != null) where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin} >= {fromKaiin.CD}"; 
				if (toKaiin != null)   where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin} <= {toKaiin.CD}";

				// 会員情報
				dvKaiin = new DBView(AppGlobal.DB.FillQuery($"SELECT {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin} FROM {TableProp.t_kaiin} WHERE {where_kaiin}"));

				// 対象月
				string selected_ym = AppDate.GetYearMonthSeirekiSlash(SelectedDateYM);
				int selectedMonth = Cast.Int(selected_ym.Substring(selected_ym.Length - 2));

				// 会員別に集計しておく
				SortedDictionary<int, List<HikiotoshiData>> dics  = new SortedDictionary<int, List<HikiotoshiData>>();
				dics_costTotal = new SortedDictionary<int, Decimal?>();

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

						if (dics.ContainsKey(kin.CD) == false)
						{
							dics.Add(kin.CD, new List<HikiotoshiData>());
							dics_costTotal.Add(kin.CD, new Decimal());
						}

						HikiotoshiData hd;

						// 引落データ
						for (int j = 0; j < dvHikiotoshi.Count; j++)
						{
							t_hikiotoshi tmprow = new t_hikiotoshi(dvHikiotoshi[j]);

							// 引落データの会費情報(コード,名称)
							Kaihi kh = AppGlobal.Kaihis.Get(tmprow.ID_Kaihi);

							// 引落情報の格納
							hd = new HikiotoshiData();
							hd.Kaiin = kin;
							hd.CodeKaihi = kh.CD;
							hd.KaihiName = kh.XRow.Kaihi_Name_Null;
							hd.Cost = tmprow.Hiki_Cost;

							// 会員ごと引落リスト追加
							dics[kin.CD].Add(hd);

							// 引落合計の処理
							dics_costTotal[kin.CD] += hd.Cost;
						}
					}
				}

				int page = 1;

				// 出力用DBViewにデータ行を追加
				foreach(var kvp in dics)
				{
					Kaiin kin = AppGlobal.Kaiins.GetCode(kvp.Key);

					int line	= 0;
					int no		= 0;
					for(int i = 0; i < kvp.Value.Count; i++)
					{
						HikiotoshiData hd = kvp.Value[i];
						DataRow nrow = null;

						if (no < LINE_MAX_PAGE)
						{
							nrow = dt.NewRow();
							nrow[F_CodeKaihi] = hd.CodeKaihi;
							nrow[F_KaihiName] = hd.KaihiName;
							nrow[F_Cost] = hd.Cost;
							nrow[F_ID_Kaiin] = kin.ID;
							nrow[F_Line] = line;
							nrow[F_Page] = page;

							dv.Add(nrow);
							line++;
						}

						no++;
						if (no >= LINE_MAX_PAGE)
						{
							page++;
							line	= 0;
							no		= 0;
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
													ReportProp.CRep15_SeikyuMinyukin,
													"未入金者用請求書",
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
				FormReportPreview2 frm = new FormReportPreview2();
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
			string headerlbl = "";
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


				title = $"{AppDate.GetYearMonth(SelectedDateYM)}分"; // 日付部のみ

				string datetmp = AppDate.GetYearMonthSlash(SelectedDateYM);
				int selected_m = Cast.Int(datetmp.Substring(datetmp.Length - 2));
				headerlbl = $"{selected_m, 2}月分会費徴収ですが、指定日に引落が出来ませんでした。（毎月27日）"; // 文章1行目

				if (maxpages.ContainsKey(kin.ID) == true && maxpages[kin.ID] > 1)
				{
					pageno = $"{row[F_Page]}/{maxpages[kin.ID]}";
				}
			}

			// 会員情報
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldCode, cd);
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldPost, post);
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldAddr1, addr1);
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldAddr2, addr2);
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldNameKikan, kikanname);
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldName, name);

			// タイトル(日付部)
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldTitle1, title);

			// 文章1行目
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldHeaderLabel1, headerlbl);

			// 会社名,TEL,FAX
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldCompanyName, AppGlobal.Basic.BAS_Name);
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldCompanyTel, "TEL:" + AppGlobal.Basic.BAS_Tel1);
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldCompanyFax, "FAX:" + AppGlobal.Basic.BAS_Fax);

			// 会費合計
			setCost_zero(rep, CRep15_SeikyuMinyukin.FFldCostTotal, dics_costTotal[kin.CD]);

			// 振込先銀行(基本情報より)
			string bankname_shitenname = "";
			string bankkozatype_No = "";
			string bankkozaName = "";

			t_bank_code bank;
			bank = AppGlobal.BankCodeMg.GetBankCodeRow(AppGlobal.BasicBank.XRow.BNK_Code, AppGlobal.BasicBank.XRow.BNK_CodeShiten);
			if (bank != null)
			{
				bankname_shitenname = $"{bank.BCD_FullName}　{bank.BCD_NameShiten}";
				bankkozatype_No = $"{enumKbn.DTypeKoza[(int)AppGlobal.BasicBank.XRow.BNK_KozaType]}　{AppGlobal.BasicBank.XRow.BNK_KozaNo}";
				bankkozaName = AppGlobal.Basic.BAS_Name;
			}
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldBankName, bankname_shitenname);
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldBankTypeAndNo, bankkozatype_No);
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldBankKozaName, bankkozaName);

			// ページ番号
			setFieldValue(rep, CRep15_SeikyuMinyukin.FFldPage, pageno);
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
				setFieldValue(rep, CRep15_SeikyuMinyukin.FFldCodeKaihi, row[F_CodeKaihi]);
				setFieldValue(rep, CRep15_SeikyuMinyukin.FFldKaihiName, row[F_KaihiName]);
				setCost_zero(rep, CRep15_SeikyuMinyukin.FFldCost, row[F_Cost]);
			}
			else
			{
				// 各項目に空をセット
				setFieldValue(rep, CRep15_SeikyuMinyukin.FFldCodeKaihi, "");
				setFieldValue(rep, CRep15_SeikyuMinyukin.FFldKaihiName, "");
				setFieldValue(rep, CRep15_SeikyuMinyukin.FFldCost, "");
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
	}
}

