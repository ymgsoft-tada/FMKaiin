
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
	public partial class t_kumicd : FieldProp
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
		/// フィールド[組コードID]。
		/// </summary>
		public const string FID_KumiCode = "ID_KumiCode";
		/// <summary>
		/// 組コードID
		/// </summary>
		public int ID_KumiCode
		{
			get	{	return Cast.Int(row == null ? null : row[FID_KumiCode]);	}
			set	{	_set(FID_KumiCode, value);	}
		}
		
		/// <summary>
		/// 組コードID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_KumiCode_Null
		{
			get	{	if (row == null || row[FID_KumiCode] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_KumiCode]); }	}
			set	{	_set(FID_KumiCode, value);	}
		}
		
		/// <summary>
		/// フィールド[組コード]。
		/// </summary>
		public const string FCD_KumiCode = "CD_KumiCode";
		/// <summary>
		/// 組コード
		/// </summary>
		public int CD_KumiCode
		{
			get	{	return Cast.Int(row == null ? null : row[FCD_KumiCode]);	}
			set	{	_set(FCD_KumiCode, value);	}
		}
		
		/// <summary>
		/// 組コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? CD_KumiCode_Null
		{
			get	{	if (row == null || row[FCD_KumiCode] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FCD_KumiCode]); }	}
			set	{	_set(FCD_KumiCode, value);	}
		}
		
		/// <summary>
		/// フィールド[組コード名]。
		/// </summary>
		public const string FKMC_Name = "KMC_Name";
		/// <summary>
		/// 組コード名
		/// </summary>
		public string KMC_Name
		{
			get	{	return Cast.String(row == null ? null : row[FKMC_Name]);	}
			set	{	_set(FKMC_Name, value);	}
		}
		
		/// <summary>
		/// 組コード名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string KMC_Name_Null
		{
			get	{	if (row == null || row[FKMC_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKMC_Name]); }	}
			set	{	_set(FKMC_Name, value);	}
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
		public t_kumicd(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_kumicd 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_kumicd 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_kumicd");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_KumiCode, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FCD_KumiCode, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKMC_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
