
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
	public partial class t_kaiin_chosei : FieldProp
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
		public const string FKCS_DateYM = "KCS_DateYM";
		/// <summary>
		/// 月分
		/// </summary>
		public DateTime KCS_DateYM
		{
			get	{	return Cast.DateTime(row == null ? null : row[FKCS_DateYM]);	}
			set	{	_set(FKCS_DateYM, value);	}
		}
		
		/// <summary>
		/// 月分。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? KCS_DateYM_Null
		{
			get	{	if (row == null || row[FKCS_DateYM] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FKCS_DateYM]); }	}
			set	{	_set(FKCS_DateYM, value);	}
		}
		
		/// <summary>
		/// フィールド[金額]。
		/// </summary>
		public const string FKCS_Cost = "KCS_Cost";
		/// <summary>
		/// 金額
		/// </summary>
		public decimal KCS_Cost
		{
			get	{	return Cast.Decimal(row == null ? null : row[FKCS_Cost]);	}
			set	{	_set(FKCS_Cost, value);	}
		}
		
		/// <summary>
		/// 金額。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? KCS_Cost_Null
		{
			get	{	if (row == null || row[FKCS_Cost] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FKCS_Cost]); }	}
			set	{	_set(FKCS_Cost, value);	}
		}
		
		/// <summary>
		/// フィールド[備考]。
		/// </summary>
		public const string FKCS_Memo = "KCS_Memo";
		/// <summary>
		/// 備考
		/// </summary>
		public string KCS_Memo
		{
			get	{	return Cast.String(row == null ? null : row[FKCS_Memo]);	}
			set	{	_set(FKCS_Memo, value);	}
		}
		
		/// <summary>
		/// 備考。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string KCS_Memo_Null
		{
			get	{	if (row == null || row[FKCS_Memo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKCS_Memo]); }	}
			set	{	_set(FKCS_Memo, value);	}
		}
		
		/// <summary>
		/// フィールド[支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金]。
		/// </summary>
		public const string FKCS_Shiharai = "KCS_Shiharai";
		/// <summary>
		/// 支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金
		/// </summary>
		public eShiharai KCS_Shiharai
		{
			get	{	return (eShiharai)Cast.Int(row == null ? null : row[FKCS_Shiharai]);	}
			set	{	_set(FKCS_Shiharai, (int)value);	}
		}
		
		/// <summary>
		/// 支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? KCS_Shiharai_Null
		{
			get	{	if (row == null || row[FKCS_Shiharai] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKCS_Shiharai]); }	}
			set	{	_set(FKCS_Shiharai, value);	}
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
		public t_kaiin_chosei(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_kaiin_chosei 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_kaiin_chosei 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_kaiin_chosei");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Kaiin, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Kaihi, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKCS_DateYM, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKCS_Cost, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKCS_Memo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKCS_Shiharai, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
