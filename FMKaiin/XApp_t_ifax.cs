
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
	public partial class t_ifax : FieldProp
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
		/// フィールド[iFaxID]。
		/// </summary>
		public const string FID_iFax = "ID_iFax";
		/// <summary>
		/// iFaxID
		/// </summary>
		public int ID_iFax
		{
			get	{	return Cast.Int(row == null ? null : row[FID_iFax]);	}
			set	{	_set(FID_iFax, value);	}
		}
		
		/// <summary>
		/// iFaxID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_iFax_Null
		{
			get	{	if (row == null || row[FID_iFax] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_iFax]); }	}
			set	{	_set(FID_iFax, value);	}
		}
		
		/// <summary>
		/// フィールド[iFaxコード]。
		/// </summary>
		public const string FCD_iFax = "CD_iFax";
		/// <summary>
		/// iFaxコード
		/// </summary>
		public int CD_iFax
		{
			get	{	return Cast.Int(row == null ? null : row[FCD_iFax]);	}
			set	{	_set(FCD_iFax, value);	}
		}
		
		/// <summary>
		/// iFaxコード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? CD_iFax_Null
		{
			get	{	if (row == null || row[FCD_iFax] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FCD_iFax]); }	}
			set	{	_set(FCD_iFax, value);	}
		}
		
		/// <summary>
		/// フィールド[iFax名]。
		/// </summary>
		public const string FIFX_Name = "IFX_Name";
		/// <summary>
		/// iFax名
		/// </summary>
		public string IFX_Name
		{
			get	{	return Cast.String(row == null ? null : row[FIFX_Name]);	}
			set	{	_set(FIFX_Name, value);	}
		}
		
		/// <summary>
		/// iFax名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IFX_Name_Null
		{
			get	{	if (row == null || row[FIFX_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIFX_Name]); }	}
			set	{	_set(FIFX_Name, value);	}
		}
		
		/// <summary>
		/// フィールド[iFax索引]。
		/// </summary>
		public const string FIFX_Sakuin = "IFX_Sakuin";
		/// <summary>
		/// iFax索引
		/// </summary>
		public string IFX_Sakuin
		{
			get	{	return Cast.String(row == null ? null : row[FIFX_Sakuin]);	}
			set	{	_set(FIFX_Sakuin, value);	}
		}
		
		/// <summary>
		/// iFax索引。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IFX_Sakuin_Null
		{
			get	{	if (row == null || row[FIFX_Sakuin] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIFX_Sakuin]); }	}
			set	{	_set(FIFX_Sakuin, value);	}
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
		public t_ifax(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_ifax 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_ifax 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_ifax");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_iFax, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FCD_iFax, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIFX_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIFX_Sakuin, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
