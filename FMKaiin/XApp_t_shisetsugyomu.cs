
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
	public partial class t_shisetsugyomu : FieldProp
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
		/// フィールド[施設業務ID]。
		/// </summary>
		public const string FID_ShisetsuGyomu = "ID_ShisetsuGyomu";
		/// <summary>
		/// 施設業務ID
		/// </summary>
		public int ID_ShisetsuGyomu
		{
			get	{	return Cast.Int(row == null ? null : row[FID_ShisetsuGyomu]);	}
			set	{	_set(FID_ShisetsuGyomu, value);	}
		}
		
		/// <summary>
		/// 施設業務ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_ShisetsuGyomu_Null
		{
			get	{	if (row == null || row[FID_ShisetsuGyomu] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_ShisetsuGyomu]); }	}
			set	{	_set(FID_ShisetsuGyomu, value);	}
		}
		
		/// <summary>
		/// フィールド[施設業務コード]。
		/// </summary>
		public const string FSGY_Code = "SGY_Code";
		/// <summary>
		/// 施設業務コード
		/// </summary>
		public int SGY_Code
		{
			get	{	return Cast.Int(row == null ? null : row[FSGY_Code]);	}
			set	{	_set(FSGY_Code, value);	}
		}
		
		/// <summary>
		/// 施設業務コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? SGY_Code_Null
		{
			get	{	if (row == null || row[FSGY_Code] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FSGY_Code]); }	}
			set	{	_set(FSGY_Code, value);	}
		}
		
		/// <summary>
		/// フィールド[所属施設]。
		/// </summary>
		public const string FSGY_Affiliation = "SGY_Affiliation";
		/// <summary>
		/// 所属施設
		/// </summary>
		public string SGY_Affiliation
		{
			get	{	return Cast.String(row == null ? null : row[FSGY_Affiliation]);	}
			set	{	_set(FSGY_Affiliation, value);	}
		}
		
		/// <summary>
		/// 所属施設。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string SGY_Affiliation_Null
		{
			get	{	if (row == null || row[FSGY_Affiliation] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSGY_Affiliation]); }	}
			set	{	_set(FSGY_Affiliation, value);	}
		}
		
		/// <summary>
		/// フィールド[施設業務名称]。
		/// </summary>
		public const string FSGY_Name = "SGY_Name";
		/// <summary>
		/// 施設業務名称
		/// </summary>
		public string SGY_Name
		{
			get	{	return Cast.String(row == null ? null : row[FSGY_Name]);	}
			set	{	_set(FSGY_Name, value);	}
		}
		
		/// <summary>
		/// 施設業務名称。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string SGY_Name_Null
		{
			get	{	if (row == null || row[FSGY_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSGY_Name]); }	}
			set	{	_set(FSGY_Name, value);	}
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
		public t_shisetsugyomu(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_shisetsugyomu 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_shisetsugyomu 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_shisetsugyomu");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_ShisetsuGyomu, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FSGY_Code, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FSGY_Affiliation, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FSGY_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
