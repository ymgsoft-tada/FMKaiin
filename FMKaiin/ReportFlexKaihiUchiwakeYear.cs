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

		/// <summary>選択中の月度</summary>
		public DateTime SelectedDateYM { get; set; }
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
		const string F_BankName = "F_BankName";
		const string F_BankNameShiten = "F_BankNameShiten";
		const string F_BankKozaType = "F_BankKozaType";
		const string F_BankKozaNo = "F_BankKozaNo";

		/// <summary>会員ごと最大ページ数</summary>
		Dictionary<int, int> maxpages;

		DBView dvKaiinKaihi;
		DBView dvKaiin;
		//		DBView dvKyushin;

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
			//public string BankName;
			//public string BankNameShiten;
			//public string BankKozaType;
			//public string BankKozaNo;

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
		/// <param name="fromVal">出力対象会員From</param>
		/// <param name="toVal">出力対象会員To</param>
		/// </summary>
		public void CreateView(object fromVal = null, object toVal = null)
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
			//dt.Columns.Add(F_BankName, typeof(string)); // 黒塗り部
			//dt.Columns.Add(F_BankNameShiten, typeof(string)); // 黒塗り部
			//dt.Columns.Add(F_BankKozaType, typeof(string)); // 黒塗り部
			//dt.Columns.Add(F_BankKozaNo, typeof(string)); // 黒塗り部

			DBView dv = new DBView(dt);
			dv.SortQuery($"{F_ID_Kaiin},{F_Page},{F_Line}");

			//dvKaiin = new DBView(AppGlobal.DB.FillQuery($"SELECT * FROM {TableProp.t_kaiin} WHERE {t_kaiin.FKYU_DateYM} = #{SelectedDateYM}#"));
			dvKaiinKaihi = new DBView(AppGlobal.DB.FillQuery($"SELECT * FROM {TableProp.t_kaiin_kaihi}"));
			//dvKaihi.SortQuery($"{t_kyuyo.FKYU_DateYM}, {t_kyuyo.FID_Staff}");
			dvKaiinKaihi.SortQuery($"{t_kaiin_kaihi.FID_Kaiin}, {t_kaiin_kaihi.FID_Kaihi}");

			// 日医/県医/市医,その他参加医会があれば明細を作成 -> 日医/県医/市医が必須になるなら会員範囲を取ってその数ループだけでOK
			if (dvKaiinKaihi.Count > 0)
			{
				string where_kaiin = $"1 = 1";

				Kaiin fromKaiin = AppGlobal.Kaiins.Get(Cast.Int(fromVal));
				Kaiin toKaiin	= AppGlobal.Kaiins.Get(Cast.Int(toVal));

				// コードの指定が降順の場合の対応
				if (fromKaiin != null && toKaiin != null && fromKaiin.CD > toKaiin.CD)
				{
					Kaiin sf	= fromKaiin;
					fromKaiin	= toKaiin;
					toKaiin		= sf;
				}
			
				if (fromKaiin != null) where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin} >= {fromKaiin.CD}"; 
				if (toKaiin != null)   where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin} <= {toKaiin.CD}";

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

					// 医会会費情報(日医)の取得
					//Kaihi NichiiInfo = AppGlobal.Kaihis.Get(kin.XRow.ID_Kaihi_Nichii);
					// 医会会費情報(県医)の取得
					//Kaihi KeniInfo = AppGlobal.Kaihis.Get(kin.XRow.ID_Kaihi_Keni);
					// 医会会費情報(市医)の取得
					//Kaihi ShiiInfo = AppGlobal.Kaihis.Get(kin.XRow.ID_Kaihi_Shii);

					if (kin != null)
					{
						if (dics.ContainsKey(kin.CD) == false)
						{
							dics.Add(kin.CD, new List<KaihiData>());
							dics_shiharaiKoza.Add(kin.CD, new List<eShiharai>());
							dics_costTotal.Add(kin.CD, new Decimal());
						}

						KaihiData kd;

						// 日医データ
						//if (NichiiInfo != null)
						//{
						//	kd = new KaihiData();
						//	kd.Kaiin = kin;
						//	kd.KaihiName = NichiiInfo.XRow.Kaihi_Name_Null;
						//	kd.Cost = 0;
						//	for (int j = 1; j <= 12; j++)
						//	{
						//		kd.Cost += NichiiInfo.GetCost(j);
						//	}
						//	kd.BankIdx = (int)kin.XRow.Kaiin_NichiiShiharai;
						//	kd.Biko = "";

/*
							t_bank_code bank;
							switch (kin.XRow.Kaiin_NichiiShiharai)
							{
								case eShiharai.Koza1:
									kd.BankIdx = (int)eShiharai.Koza1; // 「扱銀行」の数字表示

									bank = AppGlobal.BankCodeMg.GetBankCodeRow(kin.XRow.Kaiin_BankCode1_Null, kin.XRow.Kaiin_BankShitenCode1_Null);
									if (bank != null)
									{
										kd.BankName = bank.BCD_FullName;
										kd.BankNameShiten = bank.BCD_NameShiten;
										kd.BankKozaType = kin.XRow.Kaiin_BankKozaType1.ToString();
										kd.BankKozaNo = kin.XRow.Kaiin_BankKozaNo1;
									}
									break;

								case eShiharai.Koza2:
									kd.BankIdx = (int)eShiharai.Koza2; // 「扱銀行」の数字表示

									bank = AppGlobal.BankCodeMg.GetBankCodeRow(kin.XRow.Kaiin_BankCode2_Null, kin.XRow.Kaiin_BankShitenCode2_Null);
									if (bank != null)
									{
										kd.BankName = bank.BCD_FullName;
										kd.BankNameShiten = bank.BCD_NameShiten;
										kd.BankKozaType = kin.XRow.Kaiin_BankKozaType2.ToString();
										kd.BankKozaNo = kin.XRow.Kaiin_BankKozaNo2;
									}
									break;

								case eShiharai.Koza3:
									kd.BankIdx = (int)eShiharai.Koza3; // 「扱銀行」の数字表示

									bank = AppGlobal.BankCodeMg.GetBankCodeRow(kin.XRow.Kaiin_BankCode3_Null, kin.XRow.Kaiin_BankShitenCode3_Null);
									if (bank != null)
									{
										kd.BankName = bank.BCD_FullName;
										kd.BankNameShiten = bank.BCD_NameShiten;
										kd.BankKozaType = kin.XRow.Kaiin_BankKozaType3.ToString();
										kd.BankKozaNo = kin.XRow.Kaiin_BankKozaNo3;
									}
									break;
							}
*/
						//	// 会員ごと会費リスト追加
						//	dics[kin.CD].Add(kd);
						//	// 支払方法リストの処理
						//	if (dics_shiharaiKoza[kin.CD].Contains(kin.XRow.Kaiin_NichiiShiharai) == false)
						//	{
						//		dics_shiharaiKoza[kin.CD].Add(kin.XRow.Kaiin_NichiiShiharai);
						//	}
						//	//  会費合計の処理
						//	dics_costTotal[kin.CD] += kd.Cost;
						//}

						//// 県医データ
						//if (KeniInfo != null)
						//{
						//	kd = new KaihiData();
						//	kd.Kaiin = kin;
						//	kd.KaihiName = KeniInfo.XRow.Kaihi_Name_Null;
						//	kd.Cost = 0;
						//	for (int j = 1; j <= 12; j++)
						//	{
						//		kd.Cost += KeniInfo.GetCost(j);
						//	}

						//	kd.BankIdx = (int)kin.XRow.Kaiin_KeniShiharai;
						//	kd.Biko = "";

						//	// 会員ごと会費リスト追加
						//	dics[kin.CD].Add(kd);
						//	// 支払方法リストの処理
						//	if (dics_shiharaiKoza[kin.CD].Contains(kin.XRow.Kaiin_KeniShiharai) == false)
						//	{
						//		dics_shiharaiKoza[kin.CD].Add(kin.XRow.Kaiin_KeniShiharai);
						//	}
						//	//  会費合計の処理
						//	dics_costTotal[kin.CD] += kd.Cost;
						//}

						//// 市医データ
						//if (ShiiInfo != null)
						//{
						//	kd = new KaihiData();
						//	kd.Kaiin = kin;
						//	kd.KaihiName = ShiiInfo.XRow.Kaihi_Name_Null;
						//	kd.Cost = 0;
						//	for (int j = 1; j <= 12; j++)
						//	{
						//		kd.Cost += ShiiInfo.GetCost(j);
						//	}

						//	kd.BankIdx = (int)kin.XRow.Kaiin_ShiiShiharai;
						//	kd.Biko = "";

						//	// 会員ごと会費リスト追加
						//	dics[kin.CD].Add(kd);
						//	// 支払方法リストの処理
						//	if (dics_shiharaiKoza[kin.CD].Contains(kin.XRow.Kaiin_ShiiShiharai) == false)
						//	{
						//		dics_shiharaiKoza[kin.CD].Add(kin.XRow.Kaiin_ShiiShiharai);
						//	}
						//	//  会費合計の処理
						//	dics_costTotal[kin.CD] += kd.Cost;
						//}

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
								kd.Cost += kin.Kaihis[k].GetCost(j);
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

							//  会費合計の処理
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
							//nrow[F_BankName] = 
							//nrow[F_BankNameShiten] =
							//nrow[F_BankKozaType] =
							//nrow[F_BankKozaNo] =
							nrow[F_ID_Kaiin] = kin.ID;
							nrow[F_Line] = line;
							nrow[F_Page] = page;

//							nrow[F_Day1]		= kd.Day;
//							nrow[F_Day1]		= day == "99" ? "-" : day;
//							nrow[F_Komoku1]		= kd.Komoku;
//							nrow[F_Cost1]		= kd.Cost;
//							nrow[F_ID_Staff]	= stf.ID;
//							nrow[F_Line]		= line;
//							nrow[F_Page]		= page;
							dv.Add(nrow);
							line++;
						}
						// 手当2列目出す用？
						//else
						//if (dv.FindRow(new object[]{stf.ID, page, no % LINE_MAX_PAGE}) == true)
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

			//Staff stf = AppGlobal.Staffs.Get(Cast.Int(row[F_ID_Staff]));
			Kaiin stf = AppGlobal.Kaiins.Get(Cast.Int(row[F_ID_Kaiin]));

			string cd = "";
			string name = "";
			string post = "";
			string addr1 = "";
			string addr2 = "";
			string kikanname = "";
			string title = "";
			string pageno = "";

			if (stf != null)
			{
				post = $"〒{stf.XRow.Kaiin_Post}";
				cd	 = stf.CodeString;
				name = $"{stf.XRow.Kaiin_Name} 様";
				addr1 = $"{stf.XRow.Kaiin_Addr1}";
				addr2 = $"{stf.XRow.Kaiin_Addr2}";

				// 医療機関名
				IryoKikan ik = AppGlobal.IryoKikans.Get(stf.XRow.ID_Iryokikan);
				if (ik != null)
				{
					kikanname = ik.XRow.IRK_Name_Null;
				}

				title = $"会費内訳";
				//title = $"{enumKbn.DTypeJob[(int)stf.XRow.STF_TypeJob]}手当明細書";

				if (maxpages.ContainsKey(stf.ID) == true && maxpages[stf.ID] > 1)
				{
					pageno = $"{row[F_Page]}/{maxpages[stf.ID]}";
				}
			}

			// スタッフ情報
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldCode, cd);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldPost, post);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldAddr1, addr1);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldAddr2, addr2);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldNameKikan, kikanname);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldName, name);

			// タイトル
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldTitle, title);

			// 月分
			//string date = AppDate.GetYearMonth(SelectedDateYM)+"分";
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldDateYM, date);

			// 会社名,TEL,FAX
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldCompanyName, AppGlobal.Basic.BAS_Name);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldCompanyTel, "TEL:" + AppGlobal.Basic.BAS_Tel1);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldCompanyFax, "FAX:" + AppGlobal.Basic.BAS_Fax);

			// 会費合計
			setCost(rep, CRep05_KaihiUchiwakeYear.FFldCostTotal, dics_costTotal[stf.CD]);

			// 扱銀行 まず仕様を確認しないことには(同ページに2銀行以上出る、1ページ1銀行しか出ないなど)
			//string[,] bankField = new string[,]
			//{
			//	{ CRep05_KaihiUchiwakeYear.FFldBankName, CRep05_KaihiUchiwakeYear.FFldBankShitenName, CRep05_KaihiUchiwakeYear.FFldBankType, CRep05_KaihiUchiwakeYear.FFldBankNo, },
				//{ CRep05_KaihiUchiwakeYear.FFldBankName, CRep05_KaihiUchiwakeYear.FFldBankShitenName, CRep05_KaihiUchiwakeYear.FFldBankType, CRep05_KaihiUchiwakeYear.FFldBankNo },
			//};
			string bankname1 = "";
			string bankshitenname1 = "";
			string bankkozatype1 = "";
			string bankkozano1 = "";
			string bankname2 = "";
			string bankshitenname2 = "";
			string bankkozatype2 = "";
			string bankkozano2 = "";
			string bankname3 = "";
			string bankshitenname3 = "";
			string bankkozatype3 = "";
			string bankkozano3 = "";

			for (int i = 0; i < dics_shiharaiKoza[stf.CD].Count; i++)
			{
				t_bank_code bank;
				
				switch (dics_shiharaiKoza[stf.CD][i])
				{
					case eShiharai.Koza1:
						bank = AppGlobal.BankCodeMg.GetBankCodeRow(stf.XRow.Kaiin_BankCode1_Null, stf.XRow.Kaiin_BankShitenCode1_Null);
						if (bank != null)
						{
							//KaiinBankinfo kb = new KaiinBankinfo();
							//kb.shiharaiIdx = dics_shiharaiKoza[stf.CD][i];
							//kb.bankname = bank.BCD_FullName;
							//kb.banknameshiten = bank.BCD_NameShiten;
							//kb.bankkozatype = enumKbn.DTypeKoza[(int)stf.XRow.Kaiin_BankKozaType1];
							//kb.bankkozano = stf.XRow.Kaiin_BankKozaNo1;
							bankname1 = bank.BCD_FullName;
							bankshitenname1 = bank.BCD_NameShiten;
							bankkozatype1 = enumKbn.DTypeKoza[(int)stf.XRow.Kaiin_BankKozaType1];
							bankkozano1 = stf.XRow.Kaiin_BankKozaNo1;

						}
						break;

					case eShiharai.Koza2:
						bank = AppGlobal.BankCodeMg.GetBankCodeRow(stf.XRow.Kaiin_BankCode2_Null, stf.XRow.Kaiin_BankShitenCode2_Null);
						if (bank != null)
						{
							bankname2 = bank.BCD_FullName;
							bankshitenname2 = bank.BCD_NameShiten;
							bankkozatype2 = enumKbn.DTypeKoza[(int)stf.XRow.Kaiin_BankKozaType2];
							bankkozano2 = stf.XRow.Kaiin_BankKozaNo2;
						}
						break;

					case eShiharai.Koza3:
						bank = AppGlobal.BankCodeMg.GetBankCodeRow(stf.XRow.Kaiin_BankCode3_Null, stf.XRow.Kaiin_BankShitenCode3_Null);
						if (bank != null)
						{
							bankname3 = bank.BCD_FullName;
							bankshitenname3 = bank.BCD_NameShiten;
							bankkozatype3 = enumKbn.DTypeKoza[(int)stf.XRow.Kaiin_BankKozaType3];
							bankkozano3 = stf.XRow.Kaiin_BankKozaNo3;
						}
						break;
				}
			}

			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankName, bankname1);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankShitenName, bankshitenname1);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankType, bankkozatype1);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankNo, bankkozano1);


			//				eShiharai sh = dics_shiharai[stf.CD][i];

			//	t_bank_code bank;
			//	switch (dics_shiharaiKoza[stf.CD][i])
			//	{
			//		case eShiharai.Koza1:
			//			bank = AppGlobal.BankCodeMg.GetBankCodeRow(stf.XRow.Kaiin_BankCode1_Null, stf.XRow.Kaiin_BankShitenCode1_Null);
			//			if (bank != null)
			//			{
			//				KaiinBankinfo kb = new KaiinBankinfo();
			//				kb.shiharaiIdx = dics_shiharaiKoza[stf.CD][i];
			//				kb.bankname = bank.BCD_FullName;
			//				kb.banknameshiten = bank.BCD_NameShiten;
			//				kb.bankkozatype = enumKbn.DTypeKoza[(int)stf.XRow.Kaiin_BankKozaType1];
			//				kb.bankkozano = stf.XRow.Kaiin_BankKozaNo1;
			//			}
			//			break;

			//		case eShiharai.Koza2:
			//			bank = AppGlobal.BankCodeMg.GetBankCodeRow(stf.XRow.Kaiin_BankCode2_Null, stf.XRow.Kaiin_BankShitenCode2_Null);
			//			if (bank != null)
			//			{
			//				bankname2 = bank.BCD_FullName;
			//				bankshitenname2 = bank.BCD_NameShiten;
			//				bankkozatype2 = enumKbn.DTypeKoza[(int)stf.XRow.Kaiin_BankKozaType2];
			//				bankkozano2 = stf.XRow.Kaiin_BankKozaNo2;
			//			}
			//			break;

			//		case eShiharai.Koza3:
			//			bank = AppGlobal.BankCodeMg.GetBankCodeRow(stf.XRow.Kaiin_BankCode3_Null, stf.XRow.Kaiin_BankShitenCode3_Null);
			//			if (bank != null)
			//			{
			//				bankname3 = bank.BCD_FullName;
			//				bankshitenname3 = bank.BCD_NameShiten;
			//				bankkozatype3 = enumKbn.DTypeKoza[(int)stf.XRow.Kaiin_BankKozaType3];
			//				bankkozano3 = stf.XRow.Kaiin_BankKozaNo3;
			//			}
			//			break;
			//	}
			//}

			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankName, bankname1);
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankShitenName, bankshitenname1);
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankType, bankkozatype1);
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankNo, bankkozano1);
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankName2, bankname2);
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankShitenName2, bankshitenname2);
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankType2, bankkozatype2);
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankNo2, bankkozano2);
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankName3, bankname3);
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankShitenName3, bankshitenname3);
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankType3, bankkozatype3);
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankNo3, bankkozano3);

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

			//setCode(rep, row);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldKaihiName, row[F_KaihiName]);
			setCost(rep, CRep05_KaihiUchiwakeYear.FFldCost1, row[F_Cost1]);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBankIdx, row[F_BankIdx]);
			setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldBiko, row[F_Biko]);

			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldDay2, row[F_Day2]);
			//setFieldValue(rep, CRep05_KaihiUchiwakeYear.FFldKomoku2, row[F_Komoku2]);
			//setCost(rep, CRep05_KaihiUchiwakeYear.FFldCost2, row[F_Cost2]);
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

		/// <summary>
		/// 会員ごと銀行情報用クラス
		/// </summary>
		public class KaiinBankinfo
		{
			/// <summary>銀行インデックス</summary>
			public eShiharai shiharaiIdx
			{
				get; set;
			}
			/// <summary>銀行名</summary>
			public string bankname
			{
				get; set;
			}
			/// <summary>銀行支店名</summary>
			public string banknameshiten
			{
				get; set;
			}
			/// <summary>口座区分</summary>
			public string bankkozatype
			{
				get; set;
			}
			/// <summary>口座番号</summary>
			public string bankkozano
			{
				get; set;
			}

			/// <summary>
			/// コンストラクタ
			/// </summary>
			public KaiinBankinfo()
			{
			}
		}
	}
}

