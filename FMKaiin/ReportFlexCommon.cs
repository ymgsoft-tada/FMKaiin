using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using C1.Win.FlexReport;

using ComponentIO;
using ComponentDebug;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// report common setting class for Flex Report
	/// old class: ReportOut
	/// </summary>
	public class ReportFlexCommon
	{
		
		/// <summary>
		/// 共通の比率書式
		/// </summary>
		public static string RatioFormat = "0.0";
		/// <summary>
		/// 共通の通貨型書式
		/// </summary>
		public static string CurrencyZero = "#,##0";
		/// <summary>
		/// 共通の通貨型書式
		/// </summary>
		public static string CurrencyZeroBlank = "#,###";
		/// <summary>
		/// 会社名文字列最大桁数
		/// </summary>
		public static int CopNameMaxLength = 30;
		/// <summary>
		/// 部門名文字列最大桁数
		/// </summary>
		public static int BumonNameMaxLength = 20;
		/// <summary>
		/// 文字列用書体
		/// </summary>
		public static FontFamily FontFamily_String = new FontFamily("ＭＳ 明朝");
		/// <summary>
		/// 数値用書体
		/// </summary>
		public static FontFamily FontFamily_Number = new FontFamily("ＭＳ 明朝");
		/// <summary>
		/// 文字列用フォント
		/// </summary>
		public static Font Font_String = new Font(FontFamily_String,9f);
		/// <summary>
		/// 数値用フォント
		/// </summary>
		public static Font Font_Number = new Font(FontFamily_Number,9f);

		/// <summary>
		/// 共通フィールドの設定
		/// </summary>
		/// <param name="rep">レポート</param>
		public static void SetCommonField(C1FlexReport rep)
		{
			SetCommonField(rep, DateTime.Now);
		}
		
		/// <summary>
		/// 共通フィールドの設定
		/// </summary>
		/// <param name="rep">レポート</param>
		/// <param name="makedate">作成日時</param>
		public static void SetCommonField(C1FlexReport rep, DateTime makedate)
		{
			//¶ 2015/4/14 kj 自動プロパティ生成ツールの改修が終わるまで、パッチ修正
			string[]	fLeftTop	 = { "FLeftTop",	"WC_LeftTop" };
			string[]	fRightTop	 = { "FRightTop",	"WC_RightTop" };
			string[]	fLeftBottom	 = { "FLeftBottom",	"WC_LeftBottom" };
			string[]	fRightBottom = { "FRightBottom","WC_RightBottom" };
			Font		font		 = new Font(FontFamily_String, 8f);
			//¶ 2015/4/14 kj

			// データ名
			for (int i = 0; i < fLeftTop.Length; i++)
			{
				string fname = fLeftTop[i];
				
				if (rep.Fields.Contains(fname) == true)
				{
					Field field = (Field)rep.Fields[fname];
					
					field.Font = font;
					
					//if (AppGlobal.Super.Super_ChkDataName == true)
					//{
					//	t_basic		basrow	 = AppDbBasic.XRow;
						
					//	rep.Fields[fname].Visible = true;
					//	//rep.Fields[fLeftTop].Text	 = basrow.CD_Basic + "　" + basrow.Basic_Name;
						
					//	string code = "[" + basrow.CD_Basic.ToString().PadLeft(8,' ') + "]";
						
					//	field.Text	= string.Format("{0}　{1}", code, basrow.Basic_Name);
					//}
					//else
					//{
						rep.Fields[fname].Visible = false;
					//}
				}
			}
			
			// 作成日時
			for (int i = 0; i < fRightTop.Length; i++)
			{
				string fname = fRightTop[i];
				
				if (rep.Fields.Contains(fname) == true)
				{
					Field field = (Field)rep.Fields[fname];
					
	//				field.Font = font;
					
	//				if (AppGlobal.Super.Super_ChkMakeDate == true)
	//				{
	//					if (AppGlobal.Super.Super_ChkMakeTime == true)
	//					{
	//						string	time;
							
	////						if (AppBasic.XRow.Basic_YearDispMode == eYearDispMode.Seireki)
	////						{
	//							time = string.Format("{0}:{1:D2}:{2:D2}", makedate.Hour, makedate.Minute, makedate.Second);
	////						}
	////						else
	////						{
	////							time = string.Format("{0}時{1:D2}分", makedate.Hour, makedate.Minute);
	////						}
							
	//						field.Visible = true;
	//						field.Text	  = 
	//							string.Format(
	//								"{0} {1}",
	//								getSpaceDateString(makedate),
	//								time
	//							);
	//					}
	//					else
	//					{
							field.Visible = true;
							field.Text	  = 
								string.Format(
									"{0}",
									getSpaceDateString(makedate)
								);
	//					}
	//				}
	//				else
	//				{
	//					field.Visible = false;
	//				}
				}
			}
			
			// 会計事務所
			for (int i = 0; i < fLeftBottom.Length; i++)
			{
				string fname = fLeftBottom[i];
				
				if (rep.Fields.Contains(fname) == true)
				{
					Field field = (Field)rep.Fields[fname];
					
					field.Font = font;
					
					//if (AppGlobal.Super.Super_ChkKaikeiJimusho == true)
					//{
					//	field.Visible	= true;
					//	field.Text		= AppDbBasic.XRow.Basic_KaikeiJimusho;
					//}
					//else
					//{
						field.Visible	= false;
					//}
				}
			}
			
			// 頁
			for (int i = 0; i < fRightBottom.Length; i++)
			{
				string fname = fRightBottom[i];
				
				if (rep.Fields.Contains(fname) == true)
				{
					Field field = (Field)rep.Fields[fname];
					
					//field.Font = font;
					
					//if (AppGlobal.Super.Super_ChkPage == true)
					//{
						field.Visible = true;
						field.Text	  = @"=[Page] & ""頁""";
					//}
					//else
					//{
					//	field.Visible = false;
					//}
				}
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		static string getSpaceDateString(DateTime value)
		{
			//¶ 2012/11/27 K.Tada 明治00年の表示はしない。
			if (value == DateTime.MinValue)
			{
				//　最小値は空欄と同じ扱いとする。
				return "";
			}
			//¶ 2012/11/27 K.Tada 明治00年の表示はしない。

			string strdate = ReportFlexCommon.GetDate(value);

			string g;
			string y;
			string m;
			string d;

			//if(AppDbBasic.XRow.Basic_YearDispMode == eYearDispMode.Wareki)
			//{
			//	g = strdate.Substring(0,2) + " ";
			//	y = strdate.Substring(2,2);
			//}
			//else
			//{
				g = "";
				y = strdate.Substring(0,4);
			//}

			m = strdate.Substring(5,2);
			d = strdate.Substring(8,2);

			return g + y + "年 " + m + "月 " + d + "日";
		}

		/// <summary>
		/// PL用 FromToの日付文字列を返します。[自 平成YY年MM月DD日 至 平成YY年MM月DD日]
		/// </summary>
		/// <param name="from">開始日付</param>
		/// <param name="to">終了日付</param>
		/// <param name="isKessan">決算フラグ</param>
		public static string GetFromToDateStr_PL(DateTime from, DateTime to, bool isKessan)
		{
			string result = "";
			
			//if (to > AppDbBasic.XRow.Basic_StartDate)
			//{
			//	if (from < AppDbBasic.XRow.Basic_StartDate)
			//	{
			//		from = AppDbBasic.XRow.Basic_StartDate;
			//	}
			//}

			//¶ 2012/11/27 K.Tada 日付の表示は両方が取得出来た場合だけとする。
			string sfrom	= getSpaceDateString(from);
			string sto		= getSpaceDateString(to);

			if (sfrom.Length > 0 && sto.Length > 0)
			{
				result = "自 " + sfrom + "　至 " + sto;
				if(isKessan == true)
				{
					result += " ＜決算＞";
				}
			}
			//¶ 2012/11/27 K.Tada 日付の表示は両方が取得出来た場合だけとする。

			return result;
		}

		/// <summary>
		/// PL用 Fromの日付文字列を返します。[自 平成YY年MM月DD日]
		/// </summary>
		/// <param name="from">開始日付</param>
		public static string GetFromDateStr_PL(DateTime from)
		{
			string result = "";

			//if (from < AppDbBasic.XRow.Basic_StartDate)
			//{
			//	from = AppDbBasic.XRow.Basic_StartDate;
			//}

			//¶ 2012/11/27 K.Tada 日付の表示は取得出来た場合だけとする。
			string sfrom = getSpaceDateString(from);

			if (sfrom.Length > 0)
			{
				result = "自 " + sfrom;
			}
			//¶ 2012/11/27 K.Tada 日付の表示は取得出来た場合だけとする。

			return result;
		}

		/// <summary>
		/// PL用 Toの日付文字列を返します。[至 平成YY年MM月DD日]
		/// </summary>
		/// <param name="to">終了日付</param>
		/// <param name="isKessan">決算フラグ</param>
		public static string GetToDateStr_PL(DateTime to, bool isKessan)
		{
			string result = "";
			
			//¶ 2012/11/27 K.Tada 日付の表示は取得出来た場合だけとする。
			string sto = getSpaceDateString(to);

			if (sto.Length > 0)
			{
				result = "至 " + sto;

				if(isKessan == true)
				{
					result += " ＜決算＞";
				}
			}
			//¶ 2012/11/27 K.Tada 日付の表示は取得出来た場合だけとする。

			return result;
		}

		/// <summary>
		/// BS用 Toの日付文字列を返します。 [平成YY年MM月DD日 現在]
		/// </summary>
		/// <param name="to">終了日付</param>
		/// <param name="isKessan">決算フラグ</param>
		public static string GetToDateStr_BS(DateTime to, bool isKessan)
		{
			string result = "";
			
			//¶ 2012/11/27 K.Tada 日付の表示は取得出来た場合だけとする。
			string sto = getSpaceDateString(to);

			if (sto.Length > 0)
			{
				result = sto + "　　現在";

				if(isKessan == true)
				{
					result += " ＜決算＞";
				}
			}
			//¶ 2012/11/27 K.Tada 日付の表示は取得出来た場合だけとする。

			return result;
		}

		/// <summary>
		/// To用の日付文字列を返します。 [平成YY年MM月DD日 現在]
		/// </summary>
		/// <param name="to">終了日付</param>
		/// <param name="isKessan">決算フラグ</param>
		public static string GetToDateStr(DateTime to, bool isKessan)
		{
			string result = getSpaceDateString(to);

			//¶ 2012/11/27 K.Tada 日付の表示は取得出来た場合だけとする。
			if (result.Length > 0)
			{
				if(isKessan == true)
				{
					result += " ＜決算＞";
				}
			}
			//¶ 2012/11/27 K.Tada 日付の表示は取得出来た場合だけとする。

			return result;
		}

		/// <summary>
		/// 集計行用背景色
		/// </summary>
		public static Color RowBackColor0 = Color.Gainsboro;
		//public static Color RowBackColor0 = Color.SkyBlue;
		/// <summary>
		/// 小計行用背景色
		/// </summary>
		public static Color RowBackColor1 = Color.WhiteSmoke;
		//public static Color RowBackColor1 = Color.LightCyan;

		

		/// <summary>
		/// 帳票で扱う日付（年月日）を取得します。
		/// ※半角スペースのpadding処理を行います。必ず帳票はこのメソッドから日付を取得して下さい。
		/// </summary>
		public static string GetDate(DateTime dt)
		{
			return AppDate.GetDate(dt, " ");
		}

		/// <summary>
		/// 帳票で扱う日付（年）を取得します。
		/// ※半角スペースのpadding処理を行います。必ず帳票はこのメソッドから日付を取得して下さい。
		/// </summary>
		public static string GetYear(DateTime dt)
		{
			return AppDate.GetYear(dt);
		}

		/// <summary>
		/// 帳票で扱う日付（年月）を取得します。
		/// ※半角スペースのpadding処理を行います。必ず帳票はこのメソッドから日付を取得して下さい。
		/// </summary>
		public static string GetYearMonth(DateTime dt)
		{
			return AppDate.GetYearMonth(dt, " ");
		}

		/// <summary>
		/// マイナス記号を△に置換します。
		/// </summary>
		public static string ReplaceMinus(object val)
		{
			return ReplaceMinus(val, "△");
		}

		/// <summary>
		/// マイナス記号を指定した文字列に置き換えます。
		/// </summary>
		public static string ReplaceMinus(object val, string str)
		{
			string s = val.ToString();

			try
			{
				s = val.ToString().Replace('-', str[0]);
			}
			catch
			{
				AppLog.WriteLine("マイナス置換出来ませんでした。");
			}

			return s;
		}

		/// <summary>
		/// 文字列書式区分
		/// </summary>
		public enum eDecimalFormat
		{
			/// <summary>
			/// 通貨（#,###）
			/// </summary>
			CurrencyZeroBlank,
			/// <summary>
			/// 通貨（#,##0）
			/// </summary>
			CurrencyZero,
			/// <summary>
			/// 比率（0.0）
			/// </summary>
			Ratio,
			/// <summary>
			/// 比率（#.#）
			/// </summary>
			RatioBlank,
		}

		/// <summary>
		/// マイナス記号表示区分
		/// </summary>
		public enum eReplaceMinus
		{
			/// <summary>
			/// - 表示
			/// </summary>
			Normal,
			/// <summary>
			/// △ 表示
			/// </summary>
			Triangle,
		}

		/// <summary>
		/// Decimal型と判っているObjectを指定された書式で文字列に変換します。
		/// 値の端数は切捨てて変換します。
		/// nullの場合の表示については、eFormatパラメータで指定可能。
		/// </summary>
		/// <param name="value">変換する値</param>
		/// <param name="eFormat">文字列書式区分</param>
		public static string DecimalToString(object value, eDecimalFormat eFormat)
		{
			return DecimalToString(Cast.Decimal(value), eFormat, eReplaceMinus.Normal);
		}

		/// <summary>
		/// Decimalを指定された書式で文字列に変換します。
		/// 値の端数は切捨てて変換します。
		/// ０の場合の表示については、eFormatパラメータで指定可能。
		/// </summary>
		/// <param name="value">変換する値</param>
		/// <param name="eFormat">文字列書式区分</param>
		public static string DecimalToString(decimal value, eDecimalFormat eFormat)
		{
			return DecimalToString(value, eFormat, eReplaceMinus.Normal);
		}

		/// <summary>
		/// Decimalを指定された書式で文字列に変換します。
		/// 値の端数は切捨てて変換します。
		/// </summary>
		/// <param name="value">変換する値</param>
		/// <param name="eFormat">文字列書式区分</param>
		/// <param name="eMinus">マイナス記号表示区分</param>
		/// <returns></returns>
		public static string DecimalToString(decimal value, eDecimalFormat eFormat, eReplaceMinus eMinus)
		{
			string result = "";
			
			switch(eFormat)
			{
				case eDecimalFormat.CurrencyZero:
					result = value > 0 ? Math.Floor(value).ToString(ReportFlexCommon.CurrencyZero) : Math.Ceiling(value).ToString(ReportFlexCommon.CurrencyZero);
					break;
				case eDecimalFormat.CurrencyZeroBlank:
					result = value > 0 ? Math.Floor(value).ToString(ReportFlexCommon.CurrencyZeroBlank) : Math.Ceiling(value).ToString(ReportFlexCommon.CurrencyZeroBlank);
					break;
				case eDecimalFormat.Ratio		:
				case eDecimalFormat.RatioBlank	:

					decimal dCoef = (decimal)Math.Pow(10,1);

					if (eFormat == eDecimalFormat.RatioBlank && value == 0)
					{
						result = "";
					}
					else
					{
						result = value > 0 ?	(Math.Floor(value * dCoef) / dCoef).ToString(ReportFlexCommon.RatioFormat) :
												(Math.Ceiling(value * dCoef) / dCoef).ToString(ReportFlexCommon.RatioFormat);
					}

					break;
				default:
					break;
			}

			switch(eMinus)
			{
				case eReplaceMinus.Triangle:
					result = ReplaceMinus(result);
					break;
				default:
					break;
			}

			return result;
		}

		/// <summary>
		/// 表示単位以下の金額を切捨て
		/// </summary>
		/// <param name="value">変換する値</param>
		/// <param name="roundoff">切り捨てる単位</param>
		/// <returns></returns>
		public static decimal RoundOff(decimal value, decimal roundoff)
		{
			return (value - (value % roundoff)) / roundoff;
		}

		/// <summary>
		/// 税表示文字列を取得します
		/// </summary>
		/// <param name="IsZeikomi">税込ならtrue</param>
		/// <returns>税込表示or税抜表示</returns>
		public static string GetZeiString(bool IsZeikomi)
		{
			if(IsZeikomi == true)
			{
				return "税込表示";
			}
			else
			{
				return "税抜表示";
			}
		}

	}
}
