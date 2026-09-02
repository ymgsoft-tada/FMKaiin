
//
// ※このプログラムはDBAutoProperties2Access2000により自動的に生成されました。(fj)
//
// MDB File :
//		D:\client\DotNet4.6\Ishikai\FMKaiin\FMKaiin\bin\Debug\system\Data.mdb
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ComponentIO;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// テーブル編集の際に使うクラスです。
	/// </summary>
	public partial class t_bank_code : FieldProp
	{
		/// <summary>
		/// フィールド[Access 高速検索用]。
		/// </summary>
		public const string FID_Auto = "ID_Auto";
		/// <summary>
		/// Access 高速検索用
		/// </summary>
		public int ID_Auto
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Auto]);	}
			set	{	_set(FID_Auto, value);	}
		}
		
		/// <summary>
		/// Access 高速検索用。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Auto_Null
		{
			get	{	if (row == null || row[FID_Auto] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Auto]); }	}
			set	{	_set(FID_Auto, value);	}
		}
		
		/// <summary>
		/// フィールド[正式銀行名]。
		/// </summary>
		public const string FBCD_FullName = "BCD_FullName";
		/// <summary>
		/// 正式銀行名
		/// </summary>
		public string BCD_FullName
		{
			get	{	return Cast.String(row == null ? null : row[FBCD_FullName]);	}
			set	{	_set(FBCD_FullName, value);	}
		}
		
		/// <summary>
		/// 正式銀行名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string BCD_FullName_Null
		{
			get	{	if (row == null || row[FBCD_FullName] == System.DBNull.Value) { return null; } else { return Cast.String(row[FBCD_FullName]); }	}
			set	{	_set(FBCD_FullName, value);	}
		}
		
		/// <summary>
		/// フィールド[銀行名]。
		/// </summary>
		public const string FBCD_Name = "BCD_Name";
		/// <summary>
		/// 銀行名
		/// </summary>
		public string BCD_Name
		{
			get	{	return Cast.String(row == null ? null : row[FBCD_Name]);	}
			set	{	_set(FBCD_Name, value);	}
		}
		
		/// <summary>
		/// 銀行名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string BCD_Name_Null
		{
			get	{	if (row == null || row[FBCD_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FBCD_Name]); }	}
			set	{	_set(FBCD_Name, value);	}
		}
		
		/// <summary>
		/// フィールド[フリガナ]。
		/// </summary>
		public const string FBCD_NameFurigana = "BCD_NameFurigana";
		/// <summary>
		/// フリガナ
		/// </summary>
		public string BCD_NameFurigana
		{
			get	{	return Cast.String(row == null ? null : row[FBCD_NameFurigana]);	}
			set	{	_set(FBCD_NameFurigana, value);	}
		}
		
		/// <summary>
		/// フリガナ。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string BCD_NameFurigana_Null
		{
			get	{	if (row == null || row[FBCD_NameFurigana] == System.DBNull.Value) { return null; } else { return Cast.String(row[FBCD_NameFurigana]); }	}
			set	{	_set(FBCD_NameFurigana, value);	}
		}
		
		/// <summary>
		/// フィールド[銀行コード]。
		/// </summary>
		public const string FBCD_Code = "BCD_Code";
		/// <summary>
		/// 銀行コード
		/// </summary>
		public int BCD_Code
		{
			get	{	return Cast.Int(row == null ? null : row[FBCD_Code]);	}
			set	{	_set(FBCD_Code, value);	}
		}
		
		/// <summary>
		/// 銀行コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? BCD_Code_Null
		{
			get	{	if (row == null || row[FBCD_Code] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FBCD_Code]); }	}
			set	{	_set(FBCD_Code, value);	}
		}
		
		/// <summary>
		/// フィールド[支店名]。
		/// </summary>
		public const string FBCD_NameShiten = "BCD_NameShiten";
		/// <summary>
		/// 支店名
		/// </summary>
		public string BCD_NameShiten
		{
			get	{	return Cast.String(row == null ? null : row[FBCD_NameShiten]);	}
			set	{	_set(FBCD_NameShiten, value);	}
		}
		
		/// <summary>
		/// 支店名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string BCD_NameShiten_Null
		{
			get	{	if (row == null || row[FBCD_NameShiten] == System.DBNull.Value) { return null; } else { return Cast.String(row[FBCD_NameShiten]); }	}
			set	{	_set(FBCD_NameShiten, value);	}
		}
		
		/// <summary>
		/// フィールド[支店名フリガナ]。
		/// </summary>
		public const string FBCD_NameShitenFurigana = "BCD_NameShitenFurigana";
		/// <summary>
		/// 支店名フリガナ
		/// </summary>
		public string BCD_NameShitenFurigana
		{
			get	{	return Cast.String(row == null ? null : row[FBCD_NameShitenFurigana]);	}
			set	{	_set(FBCD_NameShitenFurigana, value);	}
		}
		
		/// <summary>
		/// 支店名フリガナ。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string BCD_NameShitenFurigana_Null
		{
			get	{	if (row == null || row[FBCD_NameShitenFurigana] == System.DBNull.Value) { return null; } else { return Cast.String(row[FBCD_NameShitenFurigana]); }	}
			set	{	_set(FBCD_NameShitenFurigana, value);	}
		}
		
		/// <summary>
		/// フィールド[支店コード]。
		/// </summary>
		public const string FBCD_CodeShiten = "BCD_CodeShiten";
		/// <summary>
		/// 支店コード
		/// </summary>
		public int BCD_CodeShiten
		{
			get	{	return Cast.Int(row == null ? null : row[FBCD_CodeShiten]);	}
			set	{	_set(FBCD_CodeShiten, value);	}
		}
		
		/// <summary>
		/// 支店コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? BCD_CodeShiten_Null
		{
			get	{	if (row == null || row[FBCD_CodeShiten] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FBCD_CodeShiten]); }	}
			set	{	_set(FBCD_CodeShiten, value);	}
		}
		
		/// <summary>
		/// フィールド[[要時間]最終更新日時]。
		/// </summary>
		public const string FLastUpdate = "LastUpdate";
		/// <summary>
		/// [要時間]最終更新日時
		/// </summary>
		public DateTime LastUpdate
		{
			get	{	return Cast.DateTime(row == null ? null : row[FLastUpdate]);	}
			set	{	_set_datetime(FLastUpdate, value);	}
		}
		
		/// <summary>
		/// [要時間]最終更新日時。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? LastUpdate_Null
		{
			get	{	if (row == null || row[FLastUpdate] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FLastUpdate]); }	}
			set	{	_set_datetime(FLastUpdate, value);	}
		}
		
		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="o">編集する行のDataRow、DataRowView、DBViewのどれか。DBViewの場合、現在指している行のデータになります。</param>
		public t_bank_code(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_bank_code 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_bank_code 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_bank_code");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FBCD_FullName, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FBCD_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FBCD_NameFurigana, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FBCD_Code, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FBCD_NameShiten, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FBCD_NameShitenFurigana, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FBCD_CodeShiten, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
