
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
	public partial class t_nitikbn : FieldProp
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
		/// フィールド[日医区分ID]。
		/// </summary>
		public const string FID_Nichii = "ID_Nichii";
		/// <summary>
		/// 日医区分ID
		/// </summary>
		public int ID_Nichii
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Nichii]);	}
			set	{	_set(FID_Nichii, value);	}
		}
		
		/// <summary>
		/// 日医区分ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Nichii_Null
		{
			get	{	if (row == null || row[FID_Nichii] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Nichii]); }	}
			set	{	_set(FID_Nichii, value);	}
		}
		
		/// <summary>
		/// フィールド[日医区分コード]。
		/// </summary>
		public const string FCD_Nichii = "CD_Nichii";
		/// <summary>
		/// 日医区分コード
		/// </summary>
		public int CD_Nichii
		{
			get	{	return Cast.Int(row == null ? null : row[FCD_Nichii]);	}
			set	{	_set(FCD_Nichii, value);	}
		}
		
		/// <summary>
		/// 日医区分コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? CD_Nichii_Null
		{
			get	{	if (row == null || row[FCD_Nichii] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FCD_Nichii]); }	}
			set	{	_set(FCD_Nichii, value);	}
		}
		
		/// <summary>
		/// フィールド[日医区分名]。
		/// </summary>
		public const string FNTK_Name = "NTK_Name";
		/// <summary>
		/// 日医区分名
		/// </summary>
		public string NTK_Name
		{
			get	{	return Cast.String(row == null ? null : row[FNTK_Name]);	}
			set	{	_set(FNTK_Name, value);	}
		}
		
		/// <summary>
		/// 日医区分名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string NTK_Name_Null
		{
			get	{	if (row == null || row[FNTK_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FNTK_Name]); }	}
			set	{	_set(FNTK_Name, value);	}
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
		public t_nitikbn(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_nitikbn 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_nitikbn 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_nitikbn");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Nichii, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FCD_Nichii, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FNTK_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
