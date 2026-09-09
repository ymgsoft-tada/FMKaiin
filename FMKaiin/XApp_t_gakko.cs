
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
	public partial class t_gakko : FieldProp
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
		/// フィールド[学校ID]。
		/// </summary>
		public const string FID_Gakko = "ID_Gakko";
		/// <summary>
		/// 学校ID
		/// </summary>
		public int ID_Gakko
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Gakko]);	}
			set	{	_set(FID_Gakko, value);	}
		}
		
		/// <summary>
		/// 学校ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Gakko_Null
		{
			get	{	if (row == null || row[FID_Gakko] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Gakko]); }	}
			set	{	_set(FID_Gakko, value);	}
		}
		
		/// <summary>
		/// フィールド[学校コード(左0埋め4桁)]。
		/// </summary>
		public const string FGAK_Code = "GAK_Code";
		/// <summary>
		/// 学校コード(左0埋め4桁)
		/// </summary>
		public int GAK_Code
		{
			get	{	return Cast.Int(row == null ? null : row[FGAK_Code]);	}
			set	{	_set(FGAK_Code, value);	}
		}
		
		/// <summary>
		/// 学校コード(左0埋め4桁)。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? GAK_Code_Null
		{
			get	{	if (row == null || row[FGAK_Code] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FGAK_Code]); }	}
			set	{	_set(FGAK_Code, value);	}
		}
		
		/// <summary>
		/// フィールド[学校名称]。
		/// </summary>
		public const string FGAK_Name = "GAK_Name";
		/// <summary>
		/// 学校名称
		/// </summary>
		public string GAK_Name
		{
			get	{	return Cast.String(row == null ? null : row[FGAK_Name]);	}
			set	{	_set(FGAK_Name, value);	}
		}
		
		/// <summary>
		/// 学校名称。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string GAK_Name_Null
		{
			get	{	if (row == null || row[FGAK_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FGAK_Name]); }	}
			set	{	_set(FGAK_Name, value);	}
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
		public t_gakko(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_gakko 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_gakko 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_gakko");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Gakko, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FGAK_Code, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FGAK_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
