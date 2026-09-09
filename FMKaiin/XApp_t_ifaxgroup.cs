
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
	public partial class t_ifaxgroup : FieldProp
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
		/// フィールド[iFAXグループID]。
		/// </summary>
		public const string FID_Ifax = "ID_Ifax";
		/// <summary>
		/// iFAXグループID
		/// </summary>
		public int ID_Ifax
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Ifax]);	}
			set	{	_set(FID_Ifax, value);	}
		}
		
		/// <summary>
		/// iFAXグループID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Ifax_Null
		{
			get	{	if (row == null || row[FID_Ifax] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Ifax]); }	}
			set	{	_set(FID_Ifax, value);	}
		}
		
		/// <summary>
		/// フィールド[iFAXグループコード]。
		/// </summary>
		public const string FIFAX_Code = "IFAX_Code";
		/// <summary>
		/// iFAXグループコード
		/// </summary>
		public int IFAX_Code
		{
			get	{	return Cast.Int(row == null ? null : row[FIFAX_Code]);	}
			set	{	_set(FIFAX_Code, value);	}
		}
		
		/// <summary>
		/// iFAXグループコード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IFAX_Code_Null
		{
			get	{	if (row == null || row[FIFAX_Code] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIFAX_Code]); }	}
			set	{	_set(FIFAX_Code, value);	}
		}
		
		/// <summary>
		/// フィールド[iFAXグループ名称]。
		/// </summary>
		public const string FIFAX_Name = "IFAX_Name";
		/// <summary>
		/// iFAXグループ名称
		/// </summary>
		public string IFAX_Name
		{
			get	{	return Cast.String(row == null ? null : row[FIFAX_Name]);	}
			set	{	_set(FIFAX_Name, value);	}
		}
		
		/// <summary>
		/// iFAXグループ名称。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IFAX_Name_Null
		{
			get	{	if (row == null || row[FIFAX_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIFAX_Name]); }	}
			set	{	_set(FIFAX_Name, value);	}
		}
		
		/// <summary>
		/// フィールド[iFAXグループ索引(カナ)]。
		/// </summary>
		public const string FIFAX_Kana = "IFAX_Kana";
		/// <summary>
		/// iFAXグループ索引(カナ)
		/// </summary>
		public string IFAX_Kana
		{
			get	{	return Cast.String(row == null ? null : row[FIFAX_Kana]);	}
			set	{	_set(FIFAX_Kana, value);	}
		}
		
		/// <summary>
		/// iFAXグループ索引(カナ)。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IFAX_Kana_Null
		{
			get	{	if (row == null || row[FIFAX_Kana] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIFAX_Kana]); }	}
			set	{	_set(FIFAX_Kana, value);	}
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
		public t_ifaxgroup(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_ifaxgroup 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_ifaxgroup 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_ifaxgroup");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Ifax, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIFAX_Code, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIFAX_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIFAX_Kana, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
