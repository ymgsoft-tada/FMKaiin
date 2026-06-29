using ComponentDB;
using ComponentDebug;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace App
{
	/// <summary>
	/// [作成者 kj]
	/// DBのID値取得
	/// </summary>
	public partial class AppDbID
	{
		/// <summary>
		/// 最大コード数
		/// </summary>
		public const int Max_Code = 999999;

		/// <summary>
		/// 新規IDの取得
		/// </summary>
		/// <param name="tablename">テーブル名</param>
		/// <param name="fld">フィールド</param>
		/// <returns></returns>
		public static int GetNewID(string tablename, string fld)
		{
			int val = GetMaxValue(tablename, fld);

			return getNewID(val);
		}

		/// <summary>
		/// 新規IDの取得
		/// </summary>
		/// <param name="dv">ビュー</param>
		/// <param name="fld">フィールド</param>
		/// <returns></returns>
		public static int GetNewID(DBView dv, string fld)
		{
			int val = GetMaxValue(dv,fld);

			return getNewID(val);
		}

		static int getNewID(int val)
		{
			if (val >= 0)
			{
				return val + 1;
			}
			else
			{
				ErrLog.WriteLine("新規IDの取得に失敗しました。");
			}

			return -1;
		}

		/// <summary>
		/// 新規CDの取得
		/// </summary>
		/// <param name="tablename">テーブル名</param>
		/// <param name="fld">フィールド</param>
		/// <returns></returns>
		public static int? GetNewCode(string tablename, string fld)
		{
			int val = GetMaxValue(tablename,fld);

			return getNewCode(val, fld);
		}
		/// <summary>
		/// 新規CDの取得
		/// </summary>
		/// <param name="dv">ビュー</param>
		/// <param name="fld">フィールド</param>
		/// <returns></returns>
		public static int? GetNewCode(DBView dv, string fld)
		{
			int val = GetMaxValue(dv,fld);

			return getNewCode(val, fld);
		}

		static int? getNewCode(int val, string fld)
		{
			if (val >= 0)
			{
				val += 1;

				ComponentGControlDB.GControlDBRuleBase rule = AppDbRule.Rule.GetRule(fld);

				if (rule != null)
				{
					int max = rule.MaxLength[0];
				
					max = (int)Math.Pow(10, (double)max);

					if (val >= max)
					{
						AppLog.WriteLine("最大桁数を超えています。");
						return null;
					}

				}

				return val;
			}
			else
			{
				ErrLog.WriteLine("新規CDの取得に失敗しました。");
			}

			return null;
		}

		/// <summary>
		/// 新規CDの取得
		/// </summary>
		/// <param name="tablename">テーブル名</param>
		/// <param name="fld">フィールド</param>
		/// <returns></returns>
		public static string GetNewCodeString(string tablename, string fld)
		{
			int? val = GetNewCode(tablename, fld);

			if (val != null)
			{
				return Cast.String(val);
			}

			return "";
		}

		/// <summary>
		/// 新規CDの取得
		/// </summary>
		/// <param name="dv">ビュー</param>
		/// <param name="fld">フィールド</param>
		/// <returns></returns>
		public static string GetNewCodeString(DBView dv, string fld)
		{
			int? val = GetNewCode(dv, fld);

			if (val != null)
			{
				return Cast.String(val);
			}

			return "";
		}

		/// <summary>
		/// 該当テーブルの該当フィールドの最大値を取得します。
		/// </summary>
		/// <param name="tablename">テーブル名</param>
		/// <param name="fld">フィールド名</param>
		public static int GetMaxValue(string tablename, string fld)
		{
			const string MAX_FIELD = "MAX_FIELD";

			int val = -1;

			try
			{
				DataTable dt = AppGlobal.DB.FillQuery(new DBQuery($"SELECT MAX({fld}) AS {MAX_FIELD} FROM {tablename}"));

				if (dt.Rows.Count > 0)
				{
					val = Cast.Int(dt.Rows[0][MAX_FIELD]);
				}
			}
			catch(Exception ex)
			{
				ErrLog.WriteException(ex);
			}

			return val;
		}

		/// <summary>
		/// 該当テーブルの該当フィールドの最大値を取得します。
		/// </summary>
		/// <param name="dv">ビュー</param>
		/// <param name="fld">フィールド名</param>
		public static int GetMaxValue(DBView dv, string fld)
		{
			int val = -1;

			if (dv != null)
			{
				string _sort	= dv.Sort;
				string _filter  = dv.RowFilter;

				if (_sort != fld)
				{
					dv.SortQuery(fld);
				}

				if (_filter.Length > 0)
				{
					dv.RowFilterQuery("");
				}

				if (dv.Count > 0)
				{
					val = Cast.Int(dv[dv.Count-1][fld]);
				}
				else
				{
					val = 0;
				}

				if (_sort != fld)
				{
					dv.SortQuery(_sort);
				}

				if (_filter.Length > 0)
				{
					dv.RowFilterQuery(_filter);
				}
			}
			else
			{
				ErrLog.WriteLine("ビューが指定されていません。");
			}

			return val;
		}
	}
}
