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
	/// 引落金額確認表
	/// </summary>
	public class ReportFlexHikiotoshiKakunin
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
		const string F_ID_Kaiin = "F_ID_Kaiin";
		const string F_CD_Kaiin = "F_CD_Kaiin";
		const string F_Kaiin_Name = "F_Kaiin_Name";
		const string F_Kaiin_BankKozaNameKana = "F_Kaiin_BankKozaNameKana";
		const string F_Kaiin_BankCode = "F_Kaiin_BankCode";
		const string F_Kaiin_BankName = "F_Kaiin_BankName";
		const string F_Kaiin_BankShitenCode = "F_Kaiin_BankShitenCode";
		const string F_Kaiin_BankShitenName = "F_Kaiin_BankShitenName";
		const string F_Kaiin_BankKozaType = "F_Kaiin_BankKozaType";
		const string F_Kaiin_BankKozaNo = "F_Kaiin_BankKozaNo";
		const string F_costHiki = "F_costHiki";

		/// <summary>総件数</summary>
		int rowCount = 0;

		/// <summary>金額総合計</summary>
		Decimal costTotal = 0m;

		/// <summary>会員ごと最大ページ数</summary>
		//Dictionary<int, int> maxpages;
		int maxPage = 0;

		DBView dvHikiotoshi;
		DBView dvKaiin;

		/// <summary>
		/// 引落管理クラス(明細1行)
		/// </summary>
		class HikiotoshiData
		{
			public Kaiin Kaiin;
			public string CD_Kaiin; // 会員コード
			public string Kaiin_Name; // 氏名
			public string Kaiin_BankKozaNameKana; // 口座名義
			public string Kaiin_BankCode; // 銀行コード
			public string Kaiin_BankName; // 銀行名
			public string Kaiin_BankShitenCode; // 支店コード
			public string Kaiin_BankShitenName; // 支店名
			public string Kaiin_BankKozaType; // 口座区分 (名称か数値か)
			public string Kaiin_BankKozaNo; // 口座番号
			public decimal? costHiki; // 引落金額

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
		public ReportFlexHikiotoshiKakunin()
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
			dt.Columns.Add(F_CD_Kaiin, typeof(string));
			dt.Columns.Add(F_Kaiin_Name, typeof(string));
			dt.Columns.Add(F_Kaiin_BankKozaNameKana, typeof(string));
			dt.Columns.Add(F_Kaiin_BankCode, typeof(string));
			dt.Columns.Add(F_Kaiin_BankName, typeof(string));
			dt.Columns.Add(F_Kaiin_BankShitenCode, typeof(string));
			dt.Columns.Add(F_Kaiin_BankShitenName, typeof(string));
			dt.Columns.Add(F_Kaiin_BankKozaType, typeof(string));
			dt.Columns.Add(F_Kaiin_BankKozaNo, typeof(string));
			dt.Columns.Add(F_costHiki, typeof(string));

			DBView dv = new DBView(dt);
			dv.SortQuery($"{F_ID_Kaiin},{F_Page},{F_Line}");

			// 年指定のため、該当年の1月1日を作成
			//DateTime taishoDateFrom = new DateTime(SelectedDateYM.Year, 1, 1);
			//DateTime taishoDateTo = taishoDateFrom.AddYears(1); // 次年の1月1日 ※Whereでは含まないよう設定

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
					Kaiin kai = fromKaiin;
					fromKaiin	= toKaiin;
					toKaiin		= kai;
				}
			
				if (fromKaiin != null) where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin} >= {fromKaiin.CD}"; 
				if (toKaiin != null)   where_kaiin += $" AND {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin} <= {toKaiin.CD}";

				// 会員情報
				dvKaiin = new DBView(AppGlobal.DB.FillQuery($"SELECT {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin} FROM {TableProp.t_kaiin} WHERE {where_kaiin}"));

				// 会員別に集計の準備
				// Dictionary<会員CD, <支払方法, 金額>>
				SortedDictionary<int, Dictionary<eShiharai, HikiotoshiData>> dics = new SortedDictionary<int, Dictionary<eShiharai, HikiotoshiData>>();

				//maxpages = new Dictionary<int, int>();

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

						// 支払方法別引落金額Dictionary
						Dictionary<eShiharai, HikiotoshiData> dics_hiki = new Dictionary<eShiharai, HikiotoshiData>();

						if (dics.ContainsKey(kin.CD) == false)
						{
							dics.Add(kin.CD, new Dictionary<eShiharai, HikiotoshiData>());
						}

						// 引落データ
						for (int j = 0; j < dvHikiotoshi.Count; j++)
						{
							t_hikiotoshi tmprow = new t_hikiotoshi(dvHikiotoshi[j]);

							// 会員ごとに該当支払方法のDictionaryがない場合、作成
							if (dics_hiki.ContainsKey(tmprow.Hiki_Shiharai) == false)
							{
								dics_hiki.Add(tmprow.Hiki_Shiharai, new HikiotoshiData());
								dics_hiki[tmprow.Hiki_Shiharai].costHiki = 0; // 行合計の初期化

								// 会員情報
								dics_hiki[tmprow.Hiki_Shiharai].Kaiin = kin;
								dics_hiki[tmprow.Hiki_Shiharai].CD_Kaiin = kin.CodeString; // 0埋め？
								dics_hiki[tmprow.Hiki_Shiharai].Kaiin_Name = kin.XRow.Kaiin_Name; // 氏名

								// 銀行情報
								t_bank_code bank;
								switch (tmprow.Hiki_Shiharai)
								{
									case eShiharai.Koza1:
										bank = AppGlobal.BankCodeMg.GetBankCodeRow(kin.XRow.Kaiin_BankCode1_Null, kin.XRow.Kaiin_BankShitenCode1_Null);
										if (bank != null)
										{
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankKozaNameKana = kin.XRow.Kaiin_BankKozaName1; // 口座名義
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankCode = Cast.String(kin.XRow.Kaiin_BankCode1).PadLeft(4, '0'); // 銀行コード
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankName = bank.BCD_FullName; // 銀行名
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankShitenCode = Cast.String(kin.XRow.Kaiin_BankShitenCode1).PadLeft(3, '0'); // 支店コード
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankShitenName = bank.BCD_NameShiten; // 支店名
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankKozaType = enumKbn.DTypeKoza[(int)kin.XRow.Kaiin_BankKozaType1]; // 口座区分 (名称か数値か)
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankKozaNo = kin.XRow.Kaiin_BankKozaNo1; // 口座番号
										}
										break;

									case eShiharai.Koza2:
										bank = AppGlobal.BankCodeMg.GetBankCodeRow(kin.XRow.Kaiin_BankCode2_Null, kin.XRow.Kaiin_BankShitenCode2_Null);
										if (bank != null)
										{
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankKozaNameKana = kin.XRow.Kaiin_BankKozaName2; // 口座名義
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankCode = Cast.String(kin.XRow.Kaiin_BankCode2).PadLeft(4, '0'); // 銀行コード
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankName = bank.BCD_FullName; // 銀行名
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankShitenCode = Cast.String(kin.XRow.Kaiin_BankShitenCode2).PadLeft(3, '0'); // 支店コード
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankShitenName = bank.BCD_NameShiten; // 支店名
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankKozaType = enumKbn.DTypeKoza[(int)kin.XRow.Kaiin_BankKozaType2]; // 口座区分 (名称か数値か)
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankKozaNo = kin.XRow.Kaiin_BankKozaNo2; // 口座番号
										}
										break;

									case eShiharai.Koza3:
										bank = AppGlobal.BankCodeMg.GetBankCodeRow(kin.XRow.Kaiin_BankCode3_Null, kin.XRow.Kaiin_BankShitenCode3_Null);
										if (bank != null)
										{
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankKozaNameKana = kin.XRow.Kaiin_BankKozaName3; // 口座名義
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankCode = Cast.String(kin.XRow.Kaiin_BankCode3).PadLeft(4, '0'); // 銀行コード
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankName = bank.BCD_FullName; // 銀行名
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankShitenCode = Cast.String(kin.XRow.Kaiin_BankShitenCode3).PadLeft(3, '0'); // 支店コード
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankShitenName = bank.BCD_NameShiten; // 支店名
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankKozaType = enumKbn.DTypeKoza[(int)kin.XRow.Kaiin_BankKozaType3]; // 口座区分 (名称か数値か)
											dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankKozaNo = kin.XRow.Kaiin_BankKozaNo3; // 口座番号
										}
										break;

									case eShiharai.Genkin:
										dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankKozaNameKana = ""; // 口座名義
										dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankCode = "9999"; // 銀行コード
										dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankName = ""; // 銀行名
										dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankShitenCode = "999"; // 支店コード
										dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankShitenName = ""; // 支店名
										dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankKozaType = ""; // 口座区分 (名称か数値か)
										dics_hiki[tmprow.Hiki_Shiharai].Kaiin_BankKozaNo = "0000000"; // 口座番号
										break;
								}

								dics_hiki[tmprow.Hiki_Shiharai].costHiki += tmprow.Hiki_Cost; // 引落金額の加算
								costTotal += tmprow.Hiki_Cost; // 総合計へ加算
							}
						}

						// 会員ごとに引落Dicを追加
						dics[kin.CD] = dics_hiki;
					}
				}

				int page = 1;
				int line = 0;
				int no = 0;
				int preId = 0; // 集計キー

				// 出力用DBViewにデータ行を追加
				foreach (var kvp in dics) // 会員-Dic
				{
					Kaiin kin = AppGlobal.Kaiins.GetCode(kvp.Key);

					foreach (var kvp2 in kvp.Value) // Dic支払方法-引落クラス
					{
						DataRow nrow = null;

						if (no < LINE_MAX_PAGE)
						{
							nrow = dt.NewRow();

							if(preId == kin.ID && line > 0) 
							{
								// 前行と同じID かつ ページ先頭行以外
								// 同じ集計キーの場合、2行目以降は空欄の仕様
								nrow[F_CD_Kaiin] = ""; // コード:空欄
								nrow[F_Kaiin_Name] = ""; // 名称:空欄
							}
							else
							{
								nrow[F_CD_Kaiin] = kvp2.Value.CD_Kaiin;
								nrow[F_Kaiin_Name] = kvp2.Value.Kaiin_Name;
							}
							nrow[F_Kaiin_BankKozaNameKana] = kvp2.Value.Kaiin_BankKozaNameKana;
							nrow[F_Kaiin_BankCode] = kvp2.Value.Kaiin_BankCode;
							nrow[F_Kaiin_BankName] = kvp2.Value.Kaiin_BankName;
							nrow[F_Kaiin_BankShitenCode] = kvp2.Value.Kaiin_BankShitenCode;
							nrow[F_Kaiin_BankShitenName] = kvp2.Value.Kaiin_BankShitenName;
							nrow[F_Kaiin_BankKozaType] = kvp2.Value.Kaiin_BankKozaType;
							nrow[F_Kaiin_BankKozaNo] = kvp2.Value.Kaiin_BankKozaNo;
							nrow[F_costHiki] = kvp2.Value.costHiki;

							nrow[F_ID_Kaiin] = kin.ID;
							nrow[F_Line] = line;
							nrow[F_Page] = page;

							dv.Add(nrow);

							rowCount++; // 総合計件数

							line++;

							maxPage = page;
							preId = kin.ID; // 直近で処理したIDを保持
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
				// 会員別集計を全出力した後、lineがMAXでなければ空行を追加
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
													ReportProp.CRep14_HikiotoshiKakunin,
													"引落金額確認表",
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

			string title = $"引落金額確認表";
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
			setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldTitle, title);

			// 月分
			//string date = AppDate.GetYearMonth(SelectedDateYM)+"分";
			//setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldDateYear, date);

			// 会社名
			setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldCompanyName, AppGlobal.Basic.BAS_Name);

			// 引落合計
			setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldTotalCount, $"{rowCount}件");
			setCost_zero(rep, CRep14_HikiotoshiKakunin.FFldCostTotalFooter, costTotal);

			// 作成日時
			setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldDate, $"{DateTime.Now:yyyy/MM/dd HH:mm} 作成");

			// ページ番号
			setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldPage, pageno);
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

			if (row[F_CD_Kaiin] != DBNull.Value) // 会員コード空は空行と判断
			{
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldKaiinCode, row[F_CD_Kaiin]);
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldKaiinName, row[F_Kaiin_Name]);
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankKozaName, row[F_Kaiin_BankKozaNameKana]);
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankCode, row[F_Kaiin_BankCode]);
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankName, row[F_Kaiin_BankName]);
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankCodeShiten, row[F_Kaiin_BankShitenCode]);
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankNameShiten, row[F_Kaiin_BankShitenName]);
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankKozaType, row[F_Kaiin_BankKozaType]);
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankKozaNo, row[F_Kaiin_BankKozaNo]);
				setCost_zero(rep, CRep14_HikiotoshiKakunin.FFldCostTotal, row[F_costHiki]);
			}
			else
			{
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldKaiinCode, "");
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldKaiinName, "");
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankKozaName, "");
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankCode, "");
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankName, "");
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankCodeShiten, "");
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankNameShiten, "");
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankKozaType, "");
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldBankKozaNo, "");
				setFieldValue(rep, CRep14_HikiotoshiKakunin.FFldCostTotal, ""); // 0を表示しないため
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

