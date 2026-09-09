
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
	public partial class t_kaihikbn : FieldProp
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
		/// フィールド[会費区分ID]。
		/// </summary>
		public const string FID_KaihiKbn = "ID_KaihiKbn";
		/// <summary>
		/// 会費区分ID
		/// </summary>
		public int ID_KaihiKbn
		{
			get	{	return Cast.Int(row == null ? null : row[FID_KaihiKbn]);	}
			set	{	_set(FID_KaihiKbn, value);	}
		}
		
		/// <summary>
		/// 会費区分ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_KaihiKbn_Null
		{
			get	{	if (row == null || row[FID_KaihiKbn] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_KaihiKbn]); }	}
			set	{	_set(FID_KaihiKbn, value);	}
		}
		
		/// <summary>
		/// フィールド[会費区分コード]。
		/// </summary>
		public const string FCD_KaihiKbn = "CD_KaihiKbn";
		/// <summary>
		/// 会費区分コード
		/// </summary>
		public int CD_KaihiKbn
		{
			get	{	return Cast.Int(row == null ? null : row[FCD_KaihiKbn]);	}
			set	{	_set(FCD_KaihiKbn, value);	}
		}
		
		/// <summary>
		/// 会費区分コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? CD_KaihiKbn_Null
		{
			get	{	if (row == null || row[FCD_KaihiKbn] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FCD_KaihiKbn]); }	}
			set	{	_set(FCD_KaihiKbn, value);	}
		}
		
		/// <summary>
		/// フィールド[会費区分名称]。
		/// </summary>
		public const string FKaihiKbn_Name = "KaihiKbn_Name";
		/// <summary>
		/// 会費区分名称
		/// </summary>
		public string KaihiKbn_Name
		{
			get	{	return Cast.String(row == null ? null : row[FKaihiKbn_Name]);	}
			set	{	_set(FKaihiKbn_Name, value);	}
		}
		
		/// <summary>
		/// 会費区分名称。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string KaihiKbn_Name_Null
		{
			get	{	if (row == null || row[FKaihiKbn_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaihiKbn_Name]); }	}
			set	{	_set(FKaihiKbn_Name, value);	}
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
		public t_kaihikbn(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_kaihikbn 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_kaihikbn 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_kaihikbn");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_KaihiKbn, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FCD_KaihiKbn, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihiKbn_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
