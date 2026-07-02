
//
// ※このプログラムはDBAutoProperties2Access2000により自動的に生成されました。(fj)
//
// MDB File :
//		D:\client\DotNet4.6_YMGLib5\FujisawaIshikai\FMKaiin\FMKyuyo\bin\Debug\system\Data.mdb
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ComponentIO;
using ComponentDB;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// Tableのフィールドプロパティを持つDataRow構造体を定義する親クラスです。
	/// </summary>
	public partial class FieldProp
	{
		#region *** Private Value ***
		/// <summary>
		/// 編集する行
		/// </summary>
		protected DataRow		row;
		#endregion
		
		#region *** Constructor, Dispose ***
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="o">編集する行のDataRow、DataRowView、DBViewのどれか。DBViewの場合、現在指している行のデータになります。</param>
		public FieldProp(object o)
		{
			if (o is DataRow)
			{
				row = (DataRow)o;
			}
			else if (o is DataRowView)
			{
				row = ((DataRowView)o).Row;
			}
			else if (o is DBView)
			{
				if (((DBView)o).CurrentRow == null)
				{
					row = null;
				}
				else
				{
					row = ((DBView)o).CurrentRow.Row;
				}
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("■ 正しく初期化することが出来ませんでした。");
			}
		}
		
		/// <summary>
		/// 破棄
		/// </summary>
		public void Dispose()
		{
			row = null;
		}
		#endregion
		
		#region *** Property ***
		/// <summary>
		/// 元となるDataRow
		/// </summary>
		public DataRow	Row
		{
			get
			{
				return row;
			}
		}
		#endregion
		
		#region *** Public Method ***
		/// <summary>
		/// 辞書の特定の Key に対する Value を取得します。
		/// </summary>
		/// <param name="dic">辞書。</param>
		/// <param name="key">キー。</param>
		/// <returns>Value</returns>
		public static string GetDictionaryValue(Dictionary<int, string> dic, int key)
		{
			if (dic == null)
			{
				System.Diagnostics.Debug.WriteLine("■ InitAllEnumDictionary() が行われていません。");
				return "";
			}
			if (dic.ContainsKey(key) == false)
			{
				System.Diagnostics.Debug.WriteLine("■ 指定されたキーがありません。'" + key + "'");
				return "";
			}
			
			return dic[key];
		}
		
		/// <summary>
		/// 辞書の全ての Key に対する Value リストを取得します。
		/// </summary>
		/// <param name="dic">辞書。</param>
		/// <returns>Value リスト。</returns>
		public static string[] GetDictionaryValues(Dictionary<int, string> dic)
		{
			if (dic == null)
			{
				System.Diagnostics.Debug.Write("■ InitAllEnumDictionary() が行われていません。");
				return null;
			}
			
			string[]	values = new string[dic.Values.Count];
			
			dic.Values.CopyTo(values, 0);
			
			return values;
		}
		
		/// <summary>
		/// 辞書の全ての Key リストを取得します。
		/// </summary>
		/// <param name="dic">辞書。</param>
		/// <returns>Key リスト。</returns>
		public static int[] GetDictionaryKeys(Dictionary<int, string> dic)
		{
			if (dic == null)
			{
				System.Diagnostics.Debug.Write("■ InitAllEnumDictionary() が行われていません。");
				return null;
			}
			
			int[]	keys = new int[dic.Keys.Count];
			
			dic.Keys.CopyTo(keys, 0);
			
			return keys;
		}
		#endregion
		
		#region *** Protected Method ***
		/// <summary>
		/// フィールドの情報を設定します。
		/// </summary>
		/// <param name="field">フィールド</param>
		/// <param name="val">設定する値</param>
		protected void _set(string field, object val)
		{
			if (DBView.ValueCompare(row[field], val) == false)
			{
				return;
			}
			
			// 時間は切り捨てて日付で比較する。
			if (val is DateTime)
			{
				if (Cast.DateTime(row[field]).ToShortDateString() == Cast.DateTime(val).ToShortDateString())
				{
					return;
				}
				row.BeginEdit();
				row[field] = ((DateTime)val).ToShortDateString();
				row.EndEdit();
			}
			else
			{
				row.BeginEdit();
				if (val == null)
				{
					row[field] = System.DBNull.Value;
				}
				else
				{
					row[field] = val;
				}
				row.EndEdit();
			}
		}
		/// <summary>
		/// フィールドの情報を設定します。日付型に時間も含めた代入ができます。
		/// TableMaker では、Access Database のコメント欄に「要時間」という文字があると、
		/// こちらのメソッドを参照するようになります。
		/// </summary>
		/// <param name="field">フィールド</param>
		/// <param name="val">設定する値</param>
		protected void _set_datetime(string field, object val)
		{
			if (DBView.ValueCompare(row[field], val) == false)
			{
				return;
			}
			
			row.BeginEdit();
			if (val == null)
			{
				row[field] = System.DBNull.Value;
			}
			else
			{
				row[field] = val;
			}
			row.EndEdit();
		}
		#endregion
	}
}
