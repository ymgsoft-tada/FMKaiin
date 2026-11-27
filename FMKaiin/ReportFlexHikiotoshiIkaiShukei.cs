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
	/// 所属医会別引落集計表
	/// </summary>
	public class ReportFlexHikiotoshiIkaiShukei
	{
		/// <summary>レポートファイル名</summary>
		private string ReportFile = $"{AppConst.ReportFolder}{ReportProp.CRepHikiotoshiFilename}";

		private int LINE_MAX_PAGE = 31;

		/// <summary>選択中の月度</summary>
		public DateTime SelectedDateYM { get; set; } // 呼出し時にセットしておく

		/// <summary>出力するビュー</summary>
		public DBView OutView { get; private set;}

		const int TyoseiNichiiGunCd = 1900; // 日医調整金の判断用-医会マスタ.会費群

		// 出力用DataTableのカラム
		const string F_Line		= "F_Line";
		const string F_Page		= "F_Page";
		const string F_ID_Kaihi = "F_ID_Kaihi";
		const string F_CD_Kaihi = "F_CD_Kaihi";
		const string F_Kaihi_Name = "F_Kaihi_Name";
		const string F_Kaihi_BankCode = "F_Kaihi_BankCode";
		const string F_Kaihi_BankName = "F_Kaihi_BankName";
		const string F_Kaihi_BankShitenCode = "F_Kaihi_BankShitenCode";
		const string F_Kaihi_BankShitenName = "F_Kaihi_BankShitenName";
		const string F_Kaihi_BankKozaNo = "F_Kaihi_BankKozaNo";
		const string F_Kaihi_BankKozaNameKana = "F_Kaihi_BankKozaNameKana";
		const string F_DateYMHiki = "F_DateYMHiki";
		const string F_costHiki = "F_costHiki";

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
			public string Kaihi_BankCode; // 銀行コード
			public string Kaihi_BankName; // 銀行名
			public string Kaihi_BankShitenCode; // 支店コード
			public string Kaihi_BankShitenName; // 支店名
			public string Kaihi_BankKozaNo; // 口座番号
			public string Kaihi_BankKozaNameKana; // 口座名義
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
		public ReportFlexHikiotoshiIkaiShukei()
		{
		}

		/// <summary>
		/// 出力用ビュー作成
		/// <param name="fromId">出力対象会費From</param>
		/// <param name="toId">出力対象会費To</param>
		/// </summary>
		public void CreateView(object fromId = null, object toId = null)
		{
			// 出力用テーブルの作成
			DataTable dt = new DataTable();
			dt.Columns.Add(F_Line, typeof(int));
			dt.Columns.Add(F_Page, typeof(int));
			dt.Columns.Add(F_ID_Kaihi, typeof(int));
			dt.Columns.Add(F_CD_Kaihi, typeof(string));
			dt.Columns.Add(F_Kaihi_Name, typeof(string));
			dt.Columns.Add(F_Kaihi_BankCode, typeof(string));
			dt.Columns.Add(F_Kaihi_BankName, typeof(string));
			dt.Columns.Add(F_Kaihi_BankShitenCode, typeof(string));
			dt.Columns.Add(F_Kaihi_BankShitenName, typeof(string));
			dt.Columns.Add(F_Kaihi_BankKozaNo, typeof(string));
			dt.Columns.Add(F_Kaihi_BankKozaNameKana, typeof(string));
			dt.Columns.Add(F_DateYMHiki, typeof(string));
			dt.Columns.Add(F_costHiki, typeof(string));

			DBView dv = new DBView(dt);
			dv.SortQuery($"{F_ID_Kaihi},{F_Page},{F_Line}");

			string where_hikiotoshi = $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM} = #{SelectedDateYM}#"; // 対象年月
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
				// ※現行帳票を確認したところ、会費群が入っている医会がリスト化されている
				where_kaihi += $" AND {TableProp.t_kaihi}.{t_kaihi.FKaihi_GunCode} > 0"; // 会費群の条件
				dvKaihi = new DBView(AppGlobal.DB.FillQuery($"SELECT {TableProp.t_kaihi}.{t_kaihi.FCD_Kaihi} FROM {TableProp.t_kaihi} WHERE {where_kaihi}"));

				// 医会別に集計の準備
				// Dictionary<医会CD, 引落>
				SortedDictionary<int, HikiotoshiData> dics = new SortedDictionary<int, HikiotoshiData>();

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

						HikiotoshiData hd = new HikiotoshiData();
						hd.costHiki = 0m;

						// 医会会費情報
						hd.CD_Kaihi = Cast.String(kih.CD);
						hd.Kaihi_Name = kih.XRow.Kaihi_Name_Null;

						// 銀行情報
						t_bank_code bank;
						bank = AppGlobal.BankCodeMg.GetBankCodeRow(kih.XRow.Kaihi_BankCode_Null, kih.XRow.Kaihi_BankCodeShiten_Null);
						if (bank != null)
						{
							hd.Kaihi_BankCode = Cast.String(kih.XRow.Kaihi_BankCode_Null).PadLeft(4, '0');
							hd.Kaihi_BankName = bank.BCD_FullName;
							hd.Kaihi_BankShitenCode = Cast.String(kih.XRow.Kaihi_BankCodeShiten_Null).PadLeft(3, '0');
							hd.Kaihi_BankShitenName = bank.BCD_NameShiten;
							hd.Kaihi_BankKozaNo = kih.XRow.Kaihi_BankKozaNo;
							hd.Kaihi_BankKozaNameKana = kih.XRow.Kaihi_BankKozaNameKana_Null;
						}
						else // 初期値をセット
						{
							hd.Kaihi_BankCode = "0000";
							hd.Kaihi_BankName = "";
							hd.Kaihi_BankShitenCode = "000";
							hd.Kaihi_BankShitenName = "";
							hd.Kaihi_BankKozaNo = "";
							hd.Kaihi_BankKozaNameKana = "";
						}

						// 引落年月
						hd.DateYMHiki = AppDate.GetYearMonthSeireki(SelectedDateYM);

						bool loopFlg = false;
						// 引落データのループ
						for (int j = 0; j < dvHikiotoshi.Count; j++)
						{
							t_hikiotoshi tmprow = new t_hikiotoshi(dvHikiotoshi[j]);

							// 引落金額の加算
							hd.costHiki += tmprow.Hiki_Cost;
							loopFlg = true; // ループに入った
						}

						if (kih.XRow.Kaihi_GunCode == TyoseiNichiiGunCd && loopFlg == false)
						{
							// 日医調整金のうち、引落実績がないものはセットしない
						}
						else
						{
							// 該当医会のデータなしの場合、Dictionaryへ追加
							if (dics.ContainsKey(kih.CD) == false)
							{
								dics.Add(kih.CD, new HikiotoshiData());
							}

							// 該当医会の集計データをDictionaryへセット
							dics[kih.CD] = hd;

							// 総合計の処理
							costTotal += hd.costHiki;
						}
					}
				}

				int page = 1;
				int line = 0;
				int no = 0;

				// 出力用DBViewにデータ行を追加
				foreach (var kvp in dics) // 医会会費-Dic
				{
					Kaihi kih = AppGlobal.Kaihis.GetCode(kvp.Key);

					HikiotoshiData hd = kvp.Value;
					DataRow nrow = null;

					if (no < LINE_MAX_PAGE)
					{
						nrow = dt.NewRow();
						nrow[F_CD_Kaihi] = hd.CD_Kaihi;
						nrow[F_Kaihi_Name] = hd.Kaihi_Name;
						nrow[F_Kaihi_BankCode] = hd.Kaihi_BankCode;
						nrow[F_Kaihi_BankName] = hd.Kaihi_BankName;
						nrow[F_Kaihi_BankShitenCode] = hd.Kaihi_BankShitenCode;
						nrow[F_Kaihi_BankShitenName] = hd.Kaihi_BankShitenName;
						nrow[F_Kaihi_BankKozaNo] = hd.Kaihi_BankKozaNo;
						nrow[F_Kaihi_BankKozaNameKana] = hd.Kaihi_BankKozaNameKana;
						nrow[F_Kaihi_BankName] = hd.Kaihi_BankName;
						nrow[F_DateYMHiki] = hd.DateYMHiki;
						nrow[F_costHiki] = hd.costHiki;

						nrow[F_ID_Kaihi] = kih.ID; // 医会会費ID
						nrow[F_Line] = line;
						nrow[F_Page] = page;

						dv.Add(nrow);
						line++;
					}

					no++;
					if (no >= LINE_MAX_PAGE)
					{
						page++; // 改ページ
						line = 0;
						no = 0;
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
													ReportProp.CRep13_HikiotoshiIkaiShukei,
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

			string title = $"所属医会別引落集計表";
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
			setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldTitle, title);

			// 月分
			//string date = AppDate.GetYearMonth(SelectedDateYM)+"分";
			//setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldDateYear, date);

			// 会社名
			setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldCompanyName, AppGlobal.Basic.BAS_Name);

			// 引落総合計
			setCost_zero(rep, CRep13_HikiotoshiIkaiShukei.FFldCostTotalFooter, costTotal);

			// 作成日時
			setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldDate, $"{DateTime.Now:yyyy/MM/dd HH:mm} 作成");

			// ページ番号
			setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldPage, pageno);
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
			TextField f;

			setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldIkaiCode, row[F_CD_Kaihi]);
			setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldIkaiName, row[F_Kaihi_Name]);
			setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldBankCode, row[F_Kaihi_BankCode]);
			setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldBankName, row[F_Kaihi_BankName]);
			setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldBankCodeShiten, row[F_Kaihi_BankShitenCode]);
			setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldBankNameShiten, row[F_Kaihi_BankShitenName]);
			setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldBankKozaNo, row[F_Kaihi_BankKozaNo]);

			// 銀行口座名義(フォントサイズ調整)
			f = (TextField)rep.Fields[CRep13_HikiotoshiIkaiShukei.FFldBankKozaName];
			if (Cast.String(row[F_Kaihi_BankKozaNameKana]).Length > 25)
			{
				f.Font.Size = 6;
			}
			else
			{
				f.Font.Size = (float)8.25;
			}
			setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldBankKozaName, row[F_Kaihi_BankKozaNameKana]);

			setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldHikiDateYM, row[F_DateYMHiki]);

			if (row[F_ID_Kaihi] != DBNull.Value) // 会費コード空を空行と判断
			{
				setCost_zero(rep, CRep13_HikiotoshiIkaiShukei.FFldCostTotal, row[F_costHiki]);
			}
			else
			{
				setFieldValue(rep, CRep13_HikiotoshiIkaiShukei.FFldCostTotal, ""); // 0を表示しないため
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

