
//
// ※このプログラムはDBAutoProperties2Access2000により自動的に生成されました。(fj)
//
// MDB File :
//		D:\client\DotNet4.6_YMGLib5\FujisawaIshikai\FMKaiin\FMKaiin\bin\Debug\system\Data.mdb
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
	public partial class t_hikiotoshi : FieldProp
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
		/// フィールド[引落ID]。
		/// </summary>
		public const string FID_Hikiotoshi = "ID_Hikiotoshi";
		/// <summary>
		/// 引落ID
		/// </summary>
		public int ID_Hikiotoshi
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Hikiotoshi]);	}
			set	{	_set(FID_Hikiotoshi, value);	}
		}
		
		/// <summary>
		/// 引落ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Hikiotoshi_Null
		{
			get	{	if (row == null || row[FID_Hikiotoshi] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Hikiotoshi]); }	}
			set	{	_set(FID_Hikiotoshi, value);	}
		}
		
		/// <summary>
		/// フィールド[会員ID]。
		/// </summary>
		public const string FID_Kaiin = "ID_Kaiin";
		/// <summary>
		/// 会員ID
		/// </summary>
		public int ID_Kaiin
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Kaiin]);	}
			set	{	_set(FID_Kaiin, value);	}
		}
		
		/// <summary>
		/// 会員ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Kaiin_Null
		{
			get	{	if (row == null || row[FID_Kaiin] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Kaiin]); }	}
			set	{	_set(FID_Kaiin, value);	}
		}
		
		/// <summary>
		/// フィールド[会費ID]。
		/// </summary>
		public const string FID_Kaihi = "ID_Kaihi";
		/// <summary>
		/// 会費ID
		/// </summary>
		public int ID_Kaihi
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Kaihi]);	}
			set	{	_set(FID_Kaihi, value);	}
		}
		
		/// <summary>
		/// 会費ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Kaihi_Null
		{
			get	{	if (row == null || row[FID_Kaihi] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Kaihi]); }	}
			set	{	_set(FID_Kaihi, value);	}
		}
		
		/// <summary>
		/// フィールド[月分]。
		/// </summary>
		public const string FHiki_DateYM = "Hiki_DateYM";
		/// <summary>
		/// 月分
		/// </summary>
		public DateTime Hiki_DateYM
		{
			get	{	return Cast.DateTime(row == null ? null : row[FHiki_DateYM]);	}
			set	{	_set(FHiki_DateYM, value);	}
		}
		
		/// <summary>
		/// 月分。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? Hiki_DateYM_Null
		{
			get	{	if (row == null || row[FHiki_DateYM] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FHiki_DateYM]); }	}
			set	{	_set(FHiki_DateYM, value);	}
		}
		
		/// <summary>
		/// フィールド[金額]。
		/// </summary>
		public const string FHiki_Cost = "Hiki_Cost";
		/// <summary>
		/// 金額
		/// </summary>
		public decimal Hiki_Cost
		{
			get	{	return Cast.Decimal(row == null ? null : row[FHiki_Cost]);	}
			set	{	_set(FHiki_Cost, value);	}
		}
		
		/// <summary>
		/// 金額。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? Hiki_Cost_Null
		{
			get	{	if (row == null || row[FHiki_Cost] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FHiki_Cost]); }	}
			set	{	_set(FHiki_Cost, value);	}
		}
		
		/// <summary>
		/// フィールド[備考]。
		/// </summary>
		public const string FHiki_Memo = "Hiki_Memo";
		/// <summary>
		/// 備考
		/// </summary>
		public string Hiki_Memo
		{
			get	{	return Cast.String(row == null ? null : row[FHiki_Memo]);	}
			set	{	_set(FHiki_Memo, value);	}
		}
		
		/// <summary>
		/// 備考。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Hiki_Memo_Null
		{
			get	{	if (row == null || row[FHiki_Memo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FHiki_Memo]); }	}
			set	{	_set(FHiki_Memo, value);	}
		}
		
		/// <summary>
		/// フィールド[支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金]。
		/// </summary>
		public const string FHiki_Shiharai = "Hiki_Shiharai";
		/// <summary>
		/// 支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金
		/// </summary>
		public eShiharai Hiki_Shiharai
		{
			get	{	return (eShiharai)Cast.Int(row == null ? null : row[FHiki_Shiharai]);	}
			set	{	_set(FHiki_Shiharai, (int)value);	}
		}
		
		/// <summary>
		/// 支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Hiki_Shiharai_Null
		{
			get	{	if (row == null || row[FHiki_Shiharai] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FHiki_Shiharai]); }	}
			set	{	_set(FHiki_Shiharai, value);	}
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
		public t_hikiotoshi(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_hikiotoshi 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_hikiotoshi 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_hikiotoshi");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Hikiotoshi, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Kaiin, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Kaihi, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FHiki_DateYM, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FHiki_Cost, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FHiki_Memo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FHiki_Shiharai, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
