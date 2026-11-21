
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
	public partial class t_mamis_log_detail : FieldProp
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
		/// フィールド[明細番号（取込内の連番）]。
		/// </summary>
		public const string FMLD_No = "MLD_No";
		/// <summary>
		/// 明細番号（取込内の連番）
		/// </summary>
		public int MLD_No
		{
			get	{	return Cast.Int(row == null ? null : row[FMLD_No]);	}
			set	{	_set(FMLD_No, value);	}
		}
		
		/// <summary>
		/// 明細番号（取込内の連番）。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? MLD_No_Null
		{
			get	{	if (row == null || row[FMLD_No] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FMLD_No]); }	}
			set	{	_set(FMLD_No, value);	}
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
		/// フィールド[会員コード（取込時点）]。
		/// </summary>
		public const string FCD_Kaiin = "CD_Kaiin";
		/// <summary>
		/// 会員コード（取込時点）
		/// </summary>
		public int CD_Kaiin
		{
			get	{	return Cast.Int(row == null ? null : row[FCD_Kaiin]);	}
			set	{	_set(FCD_Kaiin, value);	}
		}
		
		/// <summary>
		/// 会員コード（取込時点）。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? CD_Kaiin_Null
		{
			get	{	if (row == null || row[FCD_Kaiin] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FCD_Kaiin]); }	}
			set	{	_set(FCD_Kaiin, value);	}
		}
		
		/// <summary>
		/// フィールド[氏名（取込時点）]。
		/// </summary>
		public const string FMLD_KaiinName = "MLD_KaiinName";
		/// <summary>
		/// 氏名（取込時点）
		/// </summary>
		public string MLD_KaiinName
		{
			get	{	return Cast.String(row == null ? null : row[FMLD_KaiinName]);	}
			set	{	_set(FMLD_KaiinName, value);	}
		}
		
		/// <summary>
		/// 氏名（取込時点）。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string MLD_KaiinName_Null
		{
			get	{	if (row == null || row[FMLD_KaiinName] == System.DBNull.Value) { return null; } else { return Cast.String(row[FMLD_KaiinName]); }	}
			set	{	_set(FMLD_KaiinName, value);	}
		}
		
		/// <summary>
		/// フィールド[	項目名（画面の項目名）]。
		/// </summary>
		public const string FMLD_ItemName = "MLD_ItemName";
		/// <summary>
		/// 	項目名（画面の項目名）
		/// </summary>
		public string MLD_ItemName
		{
			get	{	return Cast.String(row == null ? null : row[FMLD_ItemName]);	}
			set	{	_set(FMLD_ItemName, value);	}
		}
		
		/// <summary>
		/// 	項目名（画面の項目名）。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string MLD_ItemName_Null
		{
			get	{	if (row == null || row[FMLD_ItemName] == System.DBNull.Value) { return null; } else { return Cast.String(row[FMLD_ItemName]); }	}
			set	{	_set(FMLD_ItemName, value);	}
		}
		
		/// <summary>
		/// フィールド[変更区分 0/None/ 1/Add/追加 2/Replace/置換]。
		/// </summary>
		public const string FMLD_Type = "MLD_Type";
		/// <summary>
		/// 変更区分 0/None/ 1/Add/追加 2/Replace/置換
		/// </summary>
		public int MLD_Type
		{
			get	{	return Cast.Int(row == null ? null : row[FMLD_Type]);	}
			set	{	_set(FMLD_Type, value);	}
		}
		
		/// <summary>
		/// 変更区分 0/None/ 1/Add/追加 2/Replace/置換。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? MLD_Type_Null
		{
			get	{	if (row == null || row[FMLD_Type] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FMLD_Type]); }	}
			set	{	_set(FMLD_Type, value);	}
		}
		
		/// <summary>
		/// フィールド[変更前]。
		/// </summary>
		public const string FMLD_Before = "MLD_Before";
		/// <summary>
		/// 変更前
		/// </summary>
		public string MLD_Before
		{
			get	{	return Cast.String(row == null ? null : row[FMLD_Before]);	}
			set	{	_set(FMLD_Before, value);	}
		}
		
		/// <summary>
		/// 変更前。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string MLD_Before_Null
		{
			get	{	if (row == null || row[FMLD_Before] == System.DBNull.Value) { return null; } else { return Cast.String(row[FMLD_Before]); }	}
			set	{	_set(FMLD_Before, value);	}
		}
		
		/// <summary>
		/// フィールド[	変更後]。
		/// </summary>
		public const string FMLD_After = "MLD_After";
		/// <summary>
		/// 	変更後
		/// </summary>
		public string MLD_After
		{
			get	{	return Cast.String(row == null ? null : row[FMLD_After]);	}
			set	{	_set(FMLD_After, value);	}
		}
		
		/// <summary>
		/// 	変更後。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string MLD_After_Null
		{
			get	{	if (row == null || row[FMLD_After] == System.DBNull.Value) { return null; } else { return Cast.String(row[FMLD_After]); }	}
			set	{	_set(FMLD_After, value);	}
		}
		
		/// <summary>
		/// フィールド[	[要時間]最終更新日時]。
		/// </summary>
		public const string FLastUpdate = "LastUpdate";
		/// <summary>
		/// 	[要時間]最終更新日時
		/// </summary>
		public DateTime LastUpdate
		{
			get	{	return Cast.DateTime(row == null ? null : row[FLastUpdate]);	}
			set	{	_set_datetime(FLastUpdate, value);	}
		}
		
		/// <summary>
		/// 	[要時間]最終更新日時。System.DBNull.Value の場合 null を示します。
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
		public t_mamis_log_detail(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_mamis_log_detail 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_mamis_log_detail 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_mamis_log_detail");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_MamisLog, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FMLD_No, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Kaiin, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FCD_Kaiin, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FMLD_KaiinName, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FMLD_ItemName, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FMLD_Type, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FMLD_Before, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 65536;
			dt.Columns.Add(col);
			
			col = new DataColumn(FMLD_After, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 65536;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
