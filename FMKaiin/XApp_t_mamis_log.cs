
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
	public partial class t_mamis_log : FieldProp
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
		/// フィールド[取込履歴ID]。
		/// </summary>
		public const string FID_MamisLog = "ID_MamisLog";
		/// <summary>
		/// 取込履歴ID
		/// </summary>
		public int ID_MamisLog
		{
			get	{	return Cast.Int(row == null ? null : row[FID_MamisLog]);	}
			set	{	_set(FID_MamisLog, value);	}
		}
		
		/// <summary>
		/// 取込履歴ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_MamisLog_Null
		{
			get	{	if (row == null || row[FID_MamisLog] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_MamisLog]); }	}
			set	{	_set(FID_MamisLog, value);	}
		}
		
		/// <summary>
		/// フィールド[	[要時間]取込日時]。
		/// </summary>
		public const string FMLG_Date = "MLG_Date";
		/// <summary>
		/// 	[要時間]取込日時
		/// </summary>
		public DateTime MLG_Date
		{
			get	{	return Cast.DateTime(row == null ? null : row[FMLG_Date]);	}
			set	{	_set_datetime(FMLG_Date, value);	}
		}
		
		/// <summary>
		/// 	[要時間]取込日時。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? MLG_Date_Null
		{
			get	{	if (row == null || row[FMLG_Date] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FMLG_Date]); }	}
			set	{	_set_datetime(FMLG_Date, value);	}
		}
		
		/// <summary>
		/// フィールド[	担当者ID]。
		/// </summary>
		public const string FID_Tanto = "ID_Tanto";
		/// <summary>
		/// 	担当者ID
		/// </summary>
		public int ID_Tanto
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Tanto]);	}
			set	{	_set(FID_Tanto, value);	}
		}
		
		/// <summary>
		/// 	担当者ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Tanto_Null
		{
			get	{	if (row == null || row[FID_Tanto] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Tanto]); }	}
			set	{	_set(FID_Tanto, value);	}
		}
		
		/// <summary>
		/// フィールド[	担当者名（取込時点の名前）]。
		/// </summary>
		public const string FMLG_TantoName = "MLG_TantoName";
		/// <summary>
		/// 	担当者名（取込時点の名前）
		/// </summary>
		public string MLG_TantoName
		{
			get	{	return Cast.String(row == null ? null : row[FMLG_TantoName]);	}
			set	{	_set(FMLG_TantoName, value);	}
		}
		
		/// <summary>
		/// 	担当者名（取込時点の名前）。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string MLG_TantoName_Null
		{
			get	{	if (row == null || row[FMLG_TantoName] == System.DBNull.Value) { return null; } else { return Cast.String(row[FMLG_TantoName]); }	}
			set	{	_set(FMLG_TantoName, value);	}
		}
		
		/// <summary>
		/// フィールド[	取込ファイル名]。
		/// </summary>
		public const string FMLG_FileName = "MLG_FileName";
		/// <summary>
		/// 	取込ファイル名
		/// </summary>
		public string MLG_FileName
		{
			get	{	return Cast.String(row == null ? null : row[FMLG_FileName]);	}
			set	{	_set(FMLG_FileName, value);	}
		}
		
		/// <summary>
		/// 	取込ファイル名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string MLG_FileName_Null
		{
			get	{	if (row == null || row[FMLG_FileName] == System.DBNull.Value) { return null; } else { return Cast.String(row[FMLG_FileName]); }	}
			set	{	_set(FMLG_FileName, value);	}
		}
		
		/// <summary>
		/// フィールド[	取込会員数]。
		/// </summary>
		public const string FMLG_CntKaiin = "MLG_CntKaiin";
		/// <summary>
		/// 	取込会員数
		/// </summary>
		public int MLG_CntKaiin
		{
			get	{	return Cast.Int(row == null ? null : row[FMLG_CntKaiin]);	}
			set	{	_set(FMLG_CntKaiin, value);	}
		}
		
		/// <summary>
		/// 	取込会員数。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? MLG_CntKaiin_Null
		{
			get	{	if (row == null || row[FMLG_CntKaiin] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FMLG_CntKaiin]); }	}
			set	{	_set(FMLG_CntKaiin, value);	}
		}
		
		/// <summary>
		/// フィールド[	取込項目数]。
		/// </summary>
		public const string FMLG_CntItem = "MLG_CntItem";
		/// <summary>
		/// 	取込項目数
		/// </summary>
		public int MLG_CntItem
		{
			get	{	return Cast.Int(row == null ? null : row[FMLG_CntItem]);	}
			set	{	_set(FMLG_CntItem, value);	}
		}
		
		/// <summary>
		/// 	取込項目数。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? MLG_CntItem_Null
		{
			get	{	if (row == null || row[FMLG_CntItem] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FMLG_CntItem]); }	}
			set	{	_set(FMLG_CntItem, value);	}
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
		public t_mamis_log(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_mamis_log 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_mamis_log 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_mamis_log");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_MamisLog, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FMLG_Date, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Tanto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FMLG_TantoName, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FMLG_FileName, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FMLG_CntKaiin, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FMLG_CntItem, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
