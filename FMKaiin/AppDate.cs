using System;
using System.Collections.Generic;
using System.Text;
using ComponentIO;
using System.Diagnostics;
using ComponentDebug;
using ComponentGGridDB;

namespace App
{
	/// <summary>
	/// [作成者 K.Tada]
	/// 日付に関する共通メソッドクラスです。
	/// </summary>
	public partial class AppDate
	{
		#region *** Private Value ***
		/// <summary>
		/// アプリケーション内の日付表示を和暦にするか取得・設定します。(trueなら西暦、falseは和暦）
		/// </summary>
		static bool dispSeikreki = true;
		#endregion

		#region *** Property ***
		/// <summary>
		/// アプリケーション内の日付表示を和暦にするか取得します。(trueなら西暦、falseは和暦）
		/// </summary>
		public static bool DispSeireki
		{
			get { return dispSeikreki; }
		}
		#endregion

		#region *** Public Method ***
		/// <summary>
		/// 指定した日付の月初日を日付型に変換して返します。失敗するとMinValueが返ります。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <returns>月初</returns>
		public static DateTime ParseDateFirstDay(DateTime dt)
		{			
			return ParseDateFirstDay(dt.Year, dt.Month);
		}

		/// <summary>
		/// 指定した年月の月初日を日付型に変換して返します。失敗するとMinValueが返ります。
		/// ex) 2009 1 → 2009/1/1
		/// </summary>
		/// <param name="year">年</param>
		/// <param name="month">月</param>
		/// <returns>月初</returns>
		public static DateTime ParseDateFirstDay(int year, int month)
		{
			DateTime dt;

			if (DateTime.TryParse(string.Format("{0}/{1}/1", year, month), out dt) == false)
			{
				ErrLog.WriteLine("指定された年月では、日付に変化できません。");
			}

			return dt;
		}

		/// <summary>
		/// 指定した日付の月末日を日付型に変換して返します。失敗するとMinValueが返ります。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <returns>月末</returns>
		public static DateTime ParseDateLastDay(DateTime dt)
		{
			return ParseDateLastDay(dt.Year, dt.Month);
		}

		/// <summary>
		/// 指定した年月の月末日を日付型に変換して返します。失敗するとMinValueが返ります。
		/// ex) 2009 1 → 2009/1/31
		/// </summary>
		/// <param name="year">年</param>
		/// <param name="month">月</param>
		/// <returns>月末</returns>
		public static DateTime ParseDateLastDay(int year, int month)
		{
			DateTime dt = ParseDateFirstDay(year, month);

			if (dt > DateTime.MinValue)
			{
				// 正しく変換された
				dt = dt.AddMonths(1).AddSeconds(-1);
			}

			return dt;
		}

		/// <summary>
		/// 指定した日付を年月日のみ（時刻部分を0:00:00）にして返します。
		/// </summary>
		public static DateTime GetShortDate(DateTime dt)
		{
			return new DateTime(dt.Year, dt.Month, dt.Day);
		}

		/// <summary>
		/// 年（ex 2010 または 平成22）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <returns>ex 2010 または 平成22</returns>
		public static string GetYear(DateTime dt)
		{
			return GetYear(dt, false);
		}

		/// <summary>
		/// 年（ex 2010 または 平成22 または H22）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="alphabet">元号の表記 trueならアルファベット</param>
		/// <returns>ex 2010 または 平成22 または H22</returns>
		public static string GetYear(DateTime dt, bool alphabet)
		{
			return getDate(dt, alphabet, null, DateType.Year);
		}

		/// <summary>
		/// 和暦年（ex 平成22 または H22）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="alphabet">元号の表記 trueならアルファベット</param>
		/// <returns>ex 平成22 または H22</returns>
		public static string GetYearWareki(DateTime dt,bool alphabet)
		{
			return getDate(dt, alphabet, null, DateType.Year, false);
		}

		/// <summary>
		/// 西暦年（ex 2010）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <returns>ex 2010</returns>
		public static string GetYearSeireki(DateTime dt)
		{
			return getDate(dt, false, null, DateType.Year, true);
		}

		/// <summary>
		/// 年月(ex 2010年01月 または 平成21年01月）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <returns>ex 2010年01月 または 平成21年01月</returns>
		public static string GetYearMonth(DateTime dt)
		{
			return getDate(dt, false, "0", DateType.YearMonth);
		}

		/// <summary>
		/// 年月(ex 2010年1月 または 平成21年1月）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="padding">桁を埋める文字列</param>
		/// <returns>ex 2010年1月 または 平成21年1月</returns>
		public static string GetYearMonth(DateTime dt, string padding)
		{
			return getDate(dt, false, padding, DateType.YearMonth);
		}

		/// <summary>
		/// 和暦年月(ex 平成21年01月）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <returns>ex 平成21年01月</returns>
		public static string GetYearMonthWareki(DateTime dt)
		{
			return getDate(dt, false, "0", DateType.YearMonth, false);
		}

		/// <summary>
		/// 和暦年月(ex 平成21年1月）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="padding">桁を埋める文字列</param>
		/// <returns>ex 平成21年1月</returns>
		public static string GetYearMonthWareki(DateTime dt, string padding)
		{
			return getDate(dt, false, padding, DateType.YearMonth, false);
		}

		/// <summary>
		/// 西暦年月(ex 2010年01月）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <returns>ex 2010年01月</returns>
		public static string GetYearMonthSeireki(DateTime dt)
		{
			return getDate(dt, false, "0", DateType.YearMonth, true);
		}

		/// <summary>
		/// 西暦年月(ex 2010年1月）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="padding">桁を埋める文字列</param>
		/// <returns>ex 2010年1月</returns>
		public static string GetYearMonthSeireki(DateTime dt, string padding)
		{
			return getDate(dt, false, padding, DateType.YearMonth, true);
		}

		/// <summary>
		/// 年月(ex 2010/01 または H21/01）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <returns>ex 2010/01 または H21/01</returns>
		public static string GetYearMonthSlash(DateTime dt)
		{
			return getDate(dt, true, "0", DateType.YearMonth);
		}

		/// <summary>
		/// 年月(ex 2010/1 または H21/1）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="padding">桁を埋める文字列</param>
		/// <returns>ex 2010/1 または H21/1</returns>
		public static string GetYearMonthSlash(DateTime dt, string padding)
		{
			return getDate(dt, true, padding, DateType.YearMonth);
		}

		/// <summary>
		/// 和暦年月(ex H21/01）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <returns>ex H21/01</returns>
		public static string GetYearMonthWarekiSlash(DateTime dt)
		{
			return getDate(dt, true, "0", DateType.YearMonth, false);
		}

		/// <summary>
		/// 和暦年月(ex H21/1）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="padding">桁を埋める文字列</param>
		/// <returns>ex H21/1</returns>
		public static string GetYearMonthWarekiSlash(DateTime dt, string padding)
		{
			return getDate(dt, true, padding, DateType.YearMonth, false);
		}

		/// <summary>
		/// 西暦年月(ex 2010/01）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <returns>ex 2010/01</returns>
		public static string GetYearMonthSeirekiSlash(DateTime dt)
		{
			return getDate(dt, true, "0", DateType.YearMonth, true);
		}

		/// <summary>
		/// 西暦年月(ex 2010/1）を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="padding">桁を埋める文字列</param>
		/// <returns>ex 2010/1</returns>
		public static string GetYearMonthSeirekiSlash(DateTime dt, string padding)
		{
			return getDate(dt, true, padding, DateType.YearMonth, true);
		}

		/// <summary>
		/// 年月日(ex 2010年01月01日 または 平成22年01月01日)を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		public static string GetDate(DateTime dt)
		{
			return getDate(dt, false, "0", DateType.Date);
		}

		/// <summary>
		/// 年月日(ex 2010年1月1日 または 平成22年1月1日)を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="padding">桁を埋める文字列</param>
		public static string GetDate(DateTime dt, string padding)
		{
			return getDate(dt, false, padding, DateType.Date);
		}

		/// <summary>
		/// 和暦年月日(ex 平成22年01月01日)を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		public static string GetDateWareki(DateTime dt)
		{
			return getDate(dt, false, "0", DateType.Date, false);
		}

		/// <summary>
		/// 和暦年月日(ex 平成22年1月1日)を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="padding">桁を埋める文字列</param>
		public static string GetDateWareki(DateTime dt, string padding)
		{
			return getDate(dt, false, padding, DateType.Date, false);
		}

		/// <summary>
		/// 西暦年月日(ex 2010年01月01日)を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		public static string GetDateSeireki(DateTime dt)
		{
			return getDate(dt, false, "0", DateType.Date, true);
		}

		/// <summary>
		/// 西暦年月日(ex 2010年1月1日)を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="padding">桁を埋める文字列</param>
		public static string GetDateSeireki(DateTime dt, string padding)
		{
			return getDate(dt, false, padding, DateType.Date, true);
		}

		/// <summary>
		/// 年月日(ex 2010/01/01 または H22/01/01)を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		public static string GetDateSlash(DateTime dt)
		{
			return getDate(dt, true, "0", DateType.Date);
		}

		/// <summary>
		/// 年月日(ex 2010/1/1 または H22/1/1)を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="padding">桁を埋める文字列</param>
		public static string GetDateSlash(DateTime dt, string padding)
		{
			return getDate(dt, true, padding, DateType.Date);
		}

		/// <summary>
		/// 和暦年月日(ex H22/01/01)を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		public static string GetDateWarekiSlash(DateTime dt)
		{
			return getDate(dt, true, "0", DateType.Date, false);
		}

		/// <summary>
		/// 和暦年月日(ex H22/1/1)を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="padding">桁を埋める文字列</param>
		public static string GetDateWarekiSlash(DateTime dt, string padding)
		{
			return getDate(dt, true, padding, DateType.Date, false);
		}

		/// <summary>
		/// 西暦年月日(ex 2010/01/01)を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		public static string GetDateSeirekiSlash(DateTime dt)
		{
			return getDate(dt, true, "0", DateType.Date, true);
		}

		/// <summary>
		/// 西暦年月日(ex 2010/1/1)を取得します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="padding">桁を埋める文字列</param>
		public static string GetDateSeirekiSlash(DateTime dt, string padding)
		{
			return getDate(dt, true, padding, DateType.Date, true);
		}

		/// <summary>
		/// アプリケーションで表示する日付フォーマットを設定します。(trueなら西暦、falseは和暦)
		/// </summary>
		public static void SetDispSeireki(bool val)
		{
			dispSeikreki = val;
			UcDate.SetKoyomiSeirekiOn(val);
			GGridDBCommon.KoyomiSeirekiOn = val;
		}

		/// <summary>
		/// 指定日の1年前の日付を取得します。
		/// </summary>
		/// <param name="_dt">日付</param>
		/// <returns>1年前</returns>
		public static DateTime GetOneYearAgo(DateTime _dt)
		{
			// 引数の日付がミリ秒を初期化して作成
			DateTime dt = new DateTime(_dt.Year, _dt.Month, _dt.Day);

			return dt.AddDays(1).AddYears(-1);
		}

		/// <summary>
		/// 指定日が月末かをチェックします。
		/// </summary>
		/// <param name="dt">日付</param>
		public static bool CheckEndOfMonth(DateTime dt)
		{
			return dt.AddDays(1).Day == 1;
		}

		/// <summary>
		/// 日付型を地域と言語の設定のフォーマットに依存せず固定化したyyyy/MM/dd HH:mm:ssで返します。
		/// </summary>
		/// <param name="dt">日付型</param>
		public static string ToString(DateTime? dt)
		{
			DateTime pdt = DateTime.MinValue;

			if (dt != null)
			{
				pdt = dt.Value;
			} 

			return ToString(pdt);
		}

		/// <summary>
		/// 日付型を地域と言語の設定のフォーマットに依存せず固定化したyyyy/MM/dd HH:mm:ssで返します。
		/// </summary>
		/// <param name="dt">日付型</param>
		public static string ToString(DateTime dt)
		{
//AppLog.WriteLine("ToString:{0}", dt.ToString("yyyy/MM/dd HH:mm:ss"));

			return dt.ToString("yyyy/MM/dd HH:mm:ss");
		}

		/// <summary>
		/// 日付型を地域と言語の設定のフォーマットに依存せず固定化したyyyy/MM/ddで返します。
		/// </summary>
		/// <param name="dt">日付型</param>
		public static string ToShortDateString(DateTime? dt)
		{
			DateTime pdt = DateTime.MinValue;

			if (dt != null)
			{
				pdt = dt.Value;
			} 

			return ToString(pdt);
		}

		/// <summary>
		/// 日付型を地域と言語の設定のフォーマットに依存せず固定化したyyyy/MM/ddで返します。
		/// </summary>
		/// <param name="dt">日付型</param>
		public static string ToShortDateString(DateTime dt)
		{
//AppLog.WriteLine("ToShortDateString:{0}", dt.ToString("yyyy/MM/dd"));

			return dt.ToString("yyyy/MM/dd");
		}

		/// <summary>
		/// 経過月数を取得します。
		/// </summary>
		/// <param name="baseDay">基準日</param>
		/// <param name="day">指定日</param>
		/// <returns></returns>
		public static int GetElapsedMonths(DateTime baseDay, DateTime day)
		{
			if (day < baseDay)
			{
				// 日付が基準日より前の場合は例外とする
				return 0;
			}

			// 経過月数を求める(満月数を考慮しない単純計算)
			var elapsedMonths = (day.Year - baseDay.Year) * 12 + (day.Month - baseDay.Month);

			if (baseDay.Day <= day.Day)
			{
				// baseDayの日部分がdayの日部分以上の場合は、その月を満了しているとみなす
				// (例:1月30日→3月30日以降の場合は満(3-1)ヶ月)
				return elapsedMonths;
			}
			else 
			if (day.Day == DateTime.DaysInMonth(day.Year, day.Month) && day.Day <= baseDay.Day)
			{
				// baseDayの日部分がdayの表す月の末日以降の場合は、その月を満了しているとみなす
				// (例:1月30日→2月28日(平年2月末日)/2月29日(閏年2月末日)以降の場合は満(2-1)ヶ月)
				return elapsedMonths;
			}
			else
			{
				// それ以外の場合は、その月を満了していないとみなす
				// (例:1月30日→3月29日以前の場合は(3-1)ヶ月未満、よって満(3-1-1)ヶ月)
				return elapsedMonths - 1;
			}
		}


		#endregion

		#region *** Private Method ***
		/// <summary>
		/// 日付のタイプ
		/// </summary>
		private enum DateType
		{
			/// <summary>
			/// 年
			/// </summary>
			Year,
			/// <summary>
			/// 年月
			/// </summary>
			YearMonth,
			/// <summary>
			/// 年月日
			/// </summary>
			Date
		}
		/// <summary>
		/// 西暦・和暦の設定値から日付を文字列で返します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="splitSlash">スラッシュ区切り　true... 2010/1/1 false... 2010年1月1日</param>
		/// <param name="padding">桁を埋める文字列</param>
		/// <param name="type">取得タイプ</param>
		/// <returns>変換した日付文字列</returns>
		private static string getDate(DateTime dt, bool splitSlash, string padding, DateType type)
		{
			return getDate(dt, splitSlash, padding, type, dispSeikreki);
		}
		/// <summary>
		/// 日付を文字列で返します。
		/// </summary>
		/// <param name="dt">日付</param>
		/// <param name="splitSlash">スラッシュ区切り　true... 2010/1/1 false... 2010年1月1日</param>
		/// <param name="padding">桁を埋める文字列</param>
		/// <param name="type">取得タイプ</param>
		///<param name="seireki">true..西暦 false..和暦</param>
		/// <returns>変換した日付文字列</returns>
		private static string getDate(DateTime dt, bool splitSlash, string padding, DateType type, bool seireki)
		{
			string year		= "";
			string month	= dt.Month.ToString();
			string day		= dt.Day.ToString();
			string date		= "";

			if(seireki == false)
			{
				if (splitSlash == false)
				{
					year = DateParse.GetGengo(AppDate.ToShortDateString(dt));
				}
				else
				{
					year = DateParse.GetGengoAlphabet(AppDate.ToShortDateString(dt));
				}

				year += DateParse.GetGengoYear(AppDate.ToShortDateString(dt)).ToString().PadLeft(2, '0');
			}
			else
			{
				year = dt.Year.ToString();
			}

			if (type == DateType.Year)
			{
				return year;
			}

			// 0詰め
			if (padding != null)
			{
				char p = padding[0];
				month = dt.Month.ToString().PadLeft(2, p);
				day	  = dt.Day.ToString().PadLeft(2, p);
			}

			// 区切り
			if (splitSlash == true)
			{
				date = year + "/" + month;
				if (type == DateType.YearMonth)
				{
					return date;
				}
				date += "/" + day;
			}
			else
			{
				date = year + "年" + month + "月";
				if (type == DateType.YearMonth)
				{
					return date;
				}
				date += day + "日";
			}

			return date;
		}
		#endregion
	}
}
