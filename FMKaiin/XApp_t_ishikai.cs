
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
	public partial class t_ishikai : FieldProp
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
		/// フィールド[医師会ID]。
		/// </summary>
		public const string FID_Ishikai = "ID_Ishikai";
		/// <summary>
		/// 医師会ID
		/// </summary>
		public int ID_Ishikai
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Ishikai]);	}
			set	{	_set(FID_Ishikai, value);	}
		}
		
		/// <summary>
		/// 医師会ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Ishikai_Null
		{
			get	{	if (row == null || row[FID_Ishikai] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Ishikai]); }	}
			set	{	_set(FID_Ishikai, value);	}
		}
		
		/// <summary>
		/// フィールド[地域医師会コード(英数字最大5桁)]。
		/// </summary>
		public const string FCD_Ishikai = "CD_Ishikai";
		/// <summary>
		/// 地域医師会コード(英数字最大5桁)
		/// </summary>
		public string CD_Ishikai
		{
			get	{	return Cast.String(row == null ? null : row[FCD_Ishikai]);	}
			set	{	_set(FCD_Ishikai, value);	}
		}
		
		/// <summary>
		/// 地域医師会コード(英数字最大5桁)。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string CD_Ishikai_Null
		{
			get	{	if (row == null || row[FCD_Ishikai] == System.DBNull.Value) { return null; } else { return Cast.String(row[FCD_Ishikai]); }	}
			set	{	_set(FCD_Ishikai, value);	}
		}
		
		/// <summary>
		/// フィールド[都道府県コード]。
		/// </summary>
		public const string FISK_Todofuken = "ISK_Todofuken";
		/// <summary>
		/// 都道府県コード
		/// </summary>
		public int ISK_Todofuken
		{
			get	{	return Cast.Int(row == null ? null : row[FISK_Todofuken]);	}
			set	{	_set(FISK_Todofuken, value);	}
		}
		
		/// <summary>
		/// 都道府県コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ISK_Todofuken_Null
		{
			get	{	if (row == null || row[FISK_Todofuken] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FISK_Todofuken]); }	}
			set	{	_set(FISK_Todofuken, value);	}
		}
		
		/// <summary>
		/// フィールド[医師会名称]。
		/// </summary>
		public const string FISK_Name = "ISK_Name";
		/// <summary>
		/// 医師会名称
		/// </summary>
		public string ISK_Name
		{
			get	{	return Cast.String(row == null ? null : row[FISK_Name]);	}
			set	{	_set(FISK_Name, value);	}
		}
		
		/// <summary>
		/// 医師会名称。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string ISK_Name_Null
		{
			get	{	if (row == null || row[FISK_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FISK_Name]); }	}
			set	{	_set(FISK_Name, value);	}
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
		public t_ishikai(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_ishikai 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_ishikai 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_ishikai");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Ishikai, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FCD_Ishikai, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FISK_Todofuken, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FISK_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
