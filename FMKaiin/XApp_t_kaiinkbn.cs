
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
	public partial class t_kaiinkbn : FieldProp
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
		/// フィールド[会員区分ID]。
		/// </summary>
		public const string FID_KaiinKbn = "ID_KaiinKbn";
		/// <summary>
		/// 会員区分ID
		/// </summary>
		public int ID_KaiinKbn
		{
			get	{	return Cast.Int(row == null ? null : row[FID_KaiinKbn]);	}
			set	{	_set(FID_KaiinKbn, value);	}
		}
		
		/// <summary>
		/// 会員区分ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_KaiinKbn_Null
		{
			get	{	if (row == null || row[FID_KaiinKbn] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_KaiinKbn]); }	}
			set	{	_set(FID_KaiinKbn, value);	}
		}
		
		/// <summary>
		/// フィールド[会員区分CD]。
		/// </summary>
		public const string FCD_KaiinKbn = "CD_KaiinKbn";
		/// <summary>
		/// 会員区分CD
		/// </summary>
		public int CD_KaiinKbn
		{
			get	{	return Cast.Int(row == null ? null : row[FCD_KaiinKbn]);	}
			set	{	_set(FCD_KaiinKbn, value);	}
		}
		
		/// <summary>
		/// 会員区分CD。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? CD_KaiinKbn_Null
		{
			get	{	if (row == null || row[FCD_KaiinKbn] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FCD_KaiinKbn]); }	}
			set	{	_set(FCD_KaiinKbn, value);	}
		}
		
		/// <summary>
		/// フィールド[会員区分名]。
		/// </summary>
		public const string FKIK_Name = "KIK_Name";
		/// <summary>
		/// 会員区分名
		/// </summary>
		public string KIK_Name
		{
			get	{	return Cast.String(row == null ? null : row[FKIK_Name]);	}
			set	{	_set(FKIK_Name, value);	}
		}
		
		/// <summary>
		/// 会員区分名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string KIK_Name_Null
		{
			get	{	if (row == null || row[FKIK_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKIK_Name]); }	}
			set	{	_set(FKIK_Name, value);	}
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
		public t_kaiinkbn(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_kaiinkbn 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_kaiinkbn 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_kaiinkbn");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_KaiinKbn, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FCD_KaiinKbn, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKIK_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
