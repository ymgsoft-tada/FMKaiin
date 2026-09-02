
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
	public partial class t_gakkai : FieldProp
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
		/// フィールド[学会ID]。
		/// </summary>
		public const string FID_Gakkai = "ID_Gakkai";
		/// <summary>
		/// 学会ID
		/// </summary>
		public int ID_Gakkai
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Gakkai]);	}
			set	{	_set(FID_Gakkai, value);	}
		}
		
		/// <summary>
		/// 学会ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Gakkai_Null
		{
			get	{	if (row == null || row[FID_Gakkai] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Gakkai]); }	}
			set	{	_set(FID_Gakkai, value);	}
		}
		
		/// <summary>
		/// フィールド[学会コード(3桁)]。
		/// </summary>
		public const string FGKAI_Code = "GKAI_Code";
		/// <summary>
		/// 学会コード(3桁)
		/// </summary>
		public int GKAI_Code
		{
			get	{	return Cast.Int(row == null ? null : row[FGKAI_Code]);	}
			set	{	_set(FGKAI_Code, value);	}
		}
		
		/// <summary>
		/// 学会コード(3桁)。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? GKAI_Code_Null
		{
			get	{	if (row == null || row[FGKAI_Code] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FGKAI_Code]); }	}
			set	{	_set(FGKAI_Code, value);	}
		}
		
		/// <summary>
		/// フィールド[学会名称]。
		/// </summary>
		public const string FGKAI_Name = "GKAI_Name";
		/// <summary>
		/// 学会名称
		/// </summary>
		public string GKAI_Name
		{
			get	{	return Cast.String(row == null ? null : row[FGKAI_Name]);	}
			set	{	_set(FGKAI_Name, value);	}
		}
		
		/// <summary>
		/// 学会名称。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string GKAI_Name_Null
		{
			get	{	if (row == null || row[FGKAI_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FGKAI_Name]); }	}
			set	{	_set(FGKAI_Name, value);	}
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
		public t_gakkai(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_gakkai 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_gakkai 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_gakkai");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Gakkai, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FGKAI_Code, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FGKAI_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
