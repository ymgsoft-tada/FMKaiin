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
	/// 年会費内訳書
	/// </summary>
	public class ReportFlexKaihiUchiwakeYear
	{
		/// <summary>レポートファイル名</summary>
		private string ReportFile = $"{AppConst.ReportFolder}{ReportProp.CRepKaihiFilename}";

		private int LINE_MAX_PAGE = 17;

		/// <summary>選択中の年</summary>
		public DateTime SelectedDateYear { get; set; }
		/// <summary>出力するビュー</summary>
		public DBView OutView { get; private set;}

		// 出力用DataTableのカラム
		const string F_Line		= "F_Line";
		const string F_Page		= "F_Page";
		const string F_ID_Kaiin = "F_ID_Kaiin";
		const string F_KaihiName = "F_KaihiName";
		const string F_Cost1	= "F_Cost1";
		const string F_BankIdx	= "F_BankIdx";
		const string F_Biko	= "F_Biko";

		/// <summary>会員ごと最大ページ数</summary>
		Dictionary<int, int> maxpages;

		DBView dvKaiinKaihi;
		DBView dvKaiin;

		SortedDictionary<int, List<eShiharai>> dics_shiharaiKoza;
		SortedDictionary<int, Decimal?> dics_costTotal;

		/// <summary>
		/// 会費管理クラス(明細1行)
		/// </summary>
		class KaihiData
		{
			public Kaiin Kaiin;
			public string KaihiName;
			public decimal? Cost;
			public int BankIdx;
			public string Biko;

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
		public ReportFlexKaihiUchiwakeYear()
		{
		}

		/// <summary>
		/// 出力用ビュー作成
		/// <param name="fromId">出力対象会員From</param>
		/// <param name="toId">出力対象会員To</param>
		/// <param name="fromZaiseki">出力対象在籍区分From</param>
		/// <param name="toZaiseki">出力対象在籍区分To</param>
		/// <param name="fromUchiwake">出力対象月別会費内訳From</param>
		/// </summary>
		public void CreateView(object fromId = null, object toId = null, object fromZaiseki = null, object toZaiseki = null, object fromUchiwake = null)
		{
			// 出力用テーブルの作成
			DataTable dt = new DataTable();
			dt.Columns.Add(F_Line, typeof(int));
			dt.Columns.Add(F_Page, typeof(int));
			dt.Columns.Add(F_ID_Kaiin, typeof(int));
			dt.Columns.Add(F_KaihiName, typeof(string));
			dt.Columns.Add(F_Cost1, typeof(string));
			dt.Columns.Add(F_BankIdx, typeof(string));
			dt.Columns.Add(F_Biko, typeof(string));

			DBView dv = new DBView(dt);
			dv.SortQuery($"{F_ID_Kaiin},{F_Page},{F_Line}");

			//dvKaiin = new DBView(AppGlobal.DB.FillQuery($"SELECT * FROM {TableProp.t_kaiin} WHERE {t_kaiin.FKYU_DateYM} = #{SelectedDateYM}#"));
			dvKaiinKaihi = new DBView(AppGlobal.DB.FillQuery($"SELECT * FROM {TableProp.t_kaiin_kaihi}"));
			//dvKaihi.SortQuery($"{t_kyuyo.FKYU_DateYM}, {t_kyuyo.FID_Staff}");
			dvKaiinKaihi.SortQuery($"{t_kaiin_kaihi.FID_Kaiin}, {t_kaiin_kaihi.FID_Kaihi}");

			// 会員-会費にデータあり
			if (dvKaiinKaihi.Count > 0)
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
				if (toZai != 0)   where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FKaiin_TypeZaiseki} <= {toZai}";

				// --enumで実装した版--
				//eTypeZaiseki fromZai = (eTypeZaiseki)fromZaiseki;
				//eTypeZaiseki toZai = (eTypeZaiseki)toZaiseki;

				//// コードの指定が降順の場合の対応
				//if (fromZai != eTypeZaiseki.None && toZai != eTypeZaiseki.None && (int)fromZai > (int)toZai)
				//{
				//	eTypeZaiseki zai = fromZai;
				//	fromZai = toZai;
				//	toZai = zai;
				//}

				//if (fromZai != eTypeZaiseki.None) where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FKaiin_TypeZaiseki} >= {(int)fromZai}";
				//if (toZai != eTypeZaiseki.None) where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FKaiin_TypeZaiseki} <= {(int)toZai}";
				// ----

				// 条件:月別会費内訳 2択のため選択した対象のみ。コントロールは1つ。両方出す場合は空欄で。
				if (fromUchiwake != null) where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FKaiin_KaihiUchiwake} = {fromUchiwake}";

				// 会員情報
				dvKaiin = new DBView(AppGlobal.DB.FillQuery($"SELECT {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin} FROM {TableProp.t_kaiin} WHERE {where_kaiin}"));

				// スタッフ別に集計しておく
				SortedDictionary<int, List<KaihiData>> dics  = new SortedDictionary<int, List<KaihiData>>();
				dics_shiharaiKoza = new SortedDictionary<int, List<eShiharai>>();
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
						if (dics.ContainsKey(kin.CD) == false)
						{
							dics.Add(kin.CD, new List<KaihiData>());
							dics_shiharaiKoza.Add(kin.CD, new List<eShiharai>());
							dics_costTotal.Add(kin.CD, new Decimal());
						}

						KaihiData kd;

						// 参加医会データ
						for (int k = 0; k < kin.Kaihis.Count; k++)
						{
							// 会費情報の格納
							kd = new KaihiData();
							kd.Kaiin = kin;
							kd.KaihiName = kin.Kaihis[k].XRow.Kaihi_Name_Null;
							kd.Cost = 0;
							for (int j = 1; j <= 12; j++)
							{
								kd.Cost += kin.Kaihis[k].GetCost(j); // 年合計
							}
							kd.BankIdx = (int)kin.GetShiharai(kin.Kaihis[k].ID);
							kd.Biko = kin.Kaihis[k].XRow.Kaihi_Biko;

							// 会員ごと会費リスト追加
							dics[kin.CD].Add(kd);

							// 支払方法リストの処理
							if (dics_shiharaiKoza[kin.CD].Contains(kin.GetShiharai(kin.Kaihis[k].ID)) == false)
							{
								dics_shiharaiKoza[kin.CD].Add(kin.GetShiharai(kin.Kaihis[k].ID));
							}

							// 会費合計の処理
							dics_costTotal[kin.CD] += kd.Cost;
						}
					}
				}

				int page = 1;

				// 出力用DBViewにデータ行を追加
				foreach(var kvp in dics)
				{
//					kvp.Value.Sort((a, b) => a.Day - b.Day);

					Kaiin kin = AppGlobal.Kaiins.GetCode(kvp.Key);

					int line	= 0;
					int no		= 0;
					for(int i = 0; i < kvp.Value.Count; i++)
					{
						KaihiData kd = kvp.Value[i];
						DataRow nrow = null;

						//string day = $"{kd.Day}";

						if (no < LINE_MAX_PAGE)
						{
							nrow = dt.NewRow();
							nrow[F_KaihiName] = kd.KaihiName;
							nrow[F_Cost1] = kd.Cost;
							nrow[F_BankIdx] = kd.BankIdx;
							nrow[F_Biko] = kd.Biko;
							nrow[F_ID_Kaiin] = kin.ID;
							nrow[F_Line] = line;
							nrow[F_Page] = page;

							dv.Add(nrow);
							line++;
						}
						// 手当2列目出す用？
						//else
						//if (dv.FindRow(new object[]{kin.ID, page, no % LINE_MAX_PAGE}) == true)
						//{
						//	nrow			= dv.CurrentRow.Row;
						//	nrow[F_Day2]	= day == "99" ? "-" : day;
						//	nrow[F_Komoku2] = kd.Komoku;
						//	nrow[F_Cost2]	= kd.Cost;
						//}

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

			// スタッフ別の最大ページ数を取得
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
													ReportProp.CRep05_KaihiUchiwakeYear,
													"年会費内訳書",
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

				title = $"会費内訳";
				//title = $"{enumKbn.DTypeJob[(int)kin.XRow.STF_TypeJob]}手当明細書";

				if (maxpages.ContainsKey(kin.ID) == true && maxpages[kin.ID] > 1)
				{
					pageno = $"{row[F_Page]}/{maxpages[kin.ID]}";
				}
			}

			// 会員情報
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldCode, cd);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldPost, post);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldAddr1, addr1);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldAddr2, addr2);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldNameKikan, kikanname);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldName, name);

			// タイトル
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldTitle, title);

			// 年表示
			//string year = $"{AppDate.GetYearSeireki(SelectedDateYear)}年分";
			//setFieldValue(rep, CRep.FFldDate, year);

			// 会社名,TEL,FAX
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldCompanyName, AppGlobal.Basic.BAS_Name);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldCompanyTel, "TEL:" + AppGlobal.Basic.BAS_Tel1);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldCompanyFax, "FAX:" + AppGlobal.Basic.BAS_Fax);

			// 会費合計
			setCost_zero(rep, CRep05_KaihiUchiwakeYear.FFldCostTotal, dics_costTotal[kin.CD]);

			// 扱銀行
			string lblbank1 = "";
			string bankname1 = "";
			string bankshitenname1 = "";
			string bankkozatype1 = "";
			string bankkozano1 = "";
			string lblbank2 = "";
			string bankname2 = "";
			string bankshitenname2 = "";
			string bankkozatype2 = "";
			string bankkozano2 = "";
			string lblbank3 = "";
			string bankname3 = "";
			string bankshitenname3 = "";
			string bankkozatype3 = "";
			string bankkozano3 = "";

			for (int i = 0; i < dics_shiharaiKoza[kin.CD].Count; i++)
			{
				t_bank_code bank;

				switch (dics_shiharaiKoza[kin.CD][i])
				{
					case eShiharai.Koza1:
						bank = AppGlobal.BankCodeMg.GetBankCodeRow(kin.XRow.Kaiin_BankCode1_Null, kin.XRow.Kaiin_BankShitenCode1_Null);
						if (bank != null)
						{
							lblbank1 = "扱銀行 １．";
							bankname1 = bank.BCD_FullName;
							bankshitenname1 = bank.BCD_NameShiten;
							bankkozatype1 = enumKbn.DTypeKoza[(int)kin.XRow.Kaiin_BankKozaType1];
							bankkozano1 = kin.XRow.Kaiin_BankKozaNo1;

						}
						break;

					case eShiharai.Koza2:
						bank = AppGlobal.BankCodeMg.GetBankCodeRow(kin.XRow.Kaiin_BankCode2_Null, kin.XRow.Kaiin_BankShitenCode2_Null);
						if (bank != null)
						{
							lblbank2 = "扱銀行 ２．";
							bankname2 = bank.BCD_FullName;
							bankshitenname2 = bank.BCD_NameShiten;
							bankkozatype2 = enumKbn.DTypeKoza[(int)kin.XRow.Kaiin_BankKozaType2];
							bankkozano2 = kin.XRow.Kaiin_BankKozaNo2;
						}
						break;

					case eShiharai.Koza3:
						bank = AppGlobal.BankCodeMg.GetBankCodeRow(kin.XRow.Kaiin_BankCode3_Null, kin.XRow.Kaiin_BankShitenCode3_Null);
						if (bank != null)
						{
							lblbank3 = "扱銀行 ３．";
							bankname3 = bank.BCD_FullName;
							bankshitenname3 = bank.BCD_NameShiten;
							bankkozatype3 = enumKbn.DTypeKoza[(int)kin.XRow.Kaiin_BankKozaType3];
							bankkozano3 = kin.XRow.Kaiin_BankKozaNo3;
						}
						break;
				}
			}

			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBanklbl1, lblbank1);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankName1, bankname1);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankShitenName1, bankshitenname1);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankType1, bankkozatype1);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankNo1, bankkozano1);

			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBanklbl2, lblbank2);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankName2, bankname2);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankShitenName2, bankshitenname2);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankType2, bankkozatype2);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankNo2, bankkozano2);

			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBanklbl3, lblbank3);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankName3, bankname3);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankShitenName3, bankshitenname3);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankType3, bankkozatype3);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankNo3, bankkozano3);

			// 発行日
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldDateOut, $"{DateTime.Now:yyyy年MM月dd日}");

			// ページ番号
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldPage, pageno);
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

			if (row[F_KaihiName] != DBNull.Value) // 会費名が空なら空行とみなす
			{
				setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldKaihiName, row[F_KaihiName]);
				setCost_zero(rep, CRep05_KaihiUchiwakeYear.FFldCost1, row[F_Cost1]);
				setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankIdx, row[F_BankIdx]);
				setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBiko, row[F_Biko]);
			}
			else
			{
				// 各項目に空をセット
				setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldKaihiName, "");
				setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldCost1, "");
				setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankIdx, "");
				setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBiko, "");
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

