
//
// ※このプログラムはDBAutoProperties2Access2000により自動的に生成されました。(fj)
//
// MDB File :
//		D:\client\DotNet4.6_YMGLib5\FujisawaIshikai\FMKaiin\FMKyuyo\bin\Debug\system\Data.mdb
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
	public partial class t_tantosha : FieldProp
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
		/// フィールド[担当者CD]。
		/// </summary>
		public const string FCD_Tanto = "CD_Tanto";
		/// <summary>
		/// 担当者CD
		/// </summary>
		public int CD_Tanto
		{
			get	{	return Cast.Int(row == null ? null : row[FCD_Tanto]);	}
			set	{	_set(FCD_Tanto, value);	}
		}
		
		/// <summary>
		/// 担当者CD。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? CD_Tanto_Null
		{
			get	{	if (row == null || row[FCD_Tanto] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FCD_Tanto]); }	}
			set	{	_set(FCD_Tanto, value);	}
		}
		
		/// <summary>
		/// フィールド[担当者ID]。
		/// </summary>
		public const string FID_Tanto = "ID_Tanto";
		/// <summary>
		/// 担当者ID
		/// </summary>
		public int ID_Tanto
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Tanto]);	}
			set	{	_set(FID_Tanto, value);	}
		}
		
		/// <summary>
		/// 担当者ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Tanto_Null
		{
			get	{	if (row == null || row[FID_Tanto] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Tanto]); }	}
			set	{	_set(FID_Tanto, value);	}
		}
		
		/// <summary>
		/// フィールド[担当者名]。
		/// </summary>
		public const string FTNT_Name = "TNT_Name";
		/// <summary>
		/// 担当者名
		/// </summary>
		public string TNT_Name
		{
			get	{	return Cast.String(row == null ? null : row[FTNT_Name]);	}
			set	{	_set(FTNT_Name, value);	}
		}
		
		/// <summary>
		/// 担当者名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string TNT_Name_Null
		{
			get	{	if (row == null || row[FTNT_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FTNT_Name]); }	}
			set	{	_set(FTNT_Name, value);	}
		}
		
		/// <summary>
		/// フィールド[パスワード]。
		/// </summary>
		public const string FTNT_Password = "TNT_Password";
		/// <summary>
		/// パスワード
		/// </summary>
		public string TNT_Password
		{
			get	{	return Cast.String(row == null ? null : row[FTNT_Password]);	}
			set	{	_set(FTNT_Password, value);	}
		}
		
		/// <summary>
		/// パスワード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string TNT_Password_Null
		{
			get	{	if (row == null || row[FTNT_Password] == System.DBNull.Value) { return null; } else { return Cast.String(row[FTNT_Password]); }	}
			set	{	_set(FTNT_Password, value);	}
		}
		
		/// <summary>
		/// フィールド[診療所区分 0/None/ 1/North/北 2/South/南]。
		/// </summary>
		public const string FTNT_TypeShinryojo = "TNT_TypeShinryojo";
		/// <summary>
		/// 診療所区分 0/None/ 1/North/北 2/South/南
		/// </summary>
		public eTypeShinryojo TNT_TypeShinryojo
		{
			get	{	return (eTypeShinryojo)Cast.Int(row == null ? null : row[FTNT_TypeShinryojo]);	}
			set	{	_set(FTNT_TypeShinryojo, (int)value);	}
		}
		
		/// <summary>
		/// 診療所区分 0/None/ 1/North/北 2/South/南。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? TNT_TypeShinryojo_Null
		{
			get	{	if (row == null || row[FTNT_TypeShinryojo] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FTNT_TypeShinryojo]); }	}
			set	{	_set(FTNT_TypeShinryojo, value);	}
		}
		
		/// <summary>
		/// フィールド[権限区分 0/None/ 1/SU/SuperUser 2/Admin/管理者 3/Ippan/一般]。
		/// </summary>
		public const string FTNT_Auth = "TNT_Auth";
		/// <summary>
		/// 権限区分 0/None/ 1/SU/SuperUser 2/Admin/管理者 3/Ippan/一般
		/// </summary>
		public eAuth TNT_Auth
		{
			get	{	return (eAuth)Cast.Int(row == null ? null : row[FTNT_Auth]);	}
			set	{	_set(FTNT_Auth, (int)value);	}
		}
		
		/// <summary>
		/// 権限区分 0/None/ 1/SU/SuperUser 2/Admin/管理者 3/Ippan/一般。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? TNT_Auth_Null
		{
			get	{	if (row == null || row[FTNT_Auth] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FTNT_Auth]); }	}
			set	{	_set(FTNT_Auth, value);	}
		}
		
		/// <summary>
		/// フィールド[使用有無]。
		/// </summary>
		public const string FTNT_Used = "TNT_Used";
		/// <summary>
		/// 使用有無
		/// </summary>
		public bool TNT_Used
		{
			get	{	return Cast.Bool(row == null ? null : row[FTNT_Used]);	}
			set	{	_set(FTNT_Used, value);	}
		}
		
		/// <summary>
		/// フィールド[マイナンバーの管理有無]。
		/// </summary>
		public const string FTNT_AvailableMyNo = "TNT_AvailableMyNo";
		/// <summary>
		/// マイナンバーの管理有無
		/// </summary>
		public bool TNT_AvailableMyNo
		{
			get	{	return Cast.Bool(row == null ? null : row[FTNT_AvailableMyNo]);	}
			set	{	_set(FTNT_AvailableMyNo, value);	}
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
		public t_tantosha(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_tantosha 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_tantosha 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_tantosha");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FCD_Tanto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Tanto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FTNT_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FTNT_Password, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FTNT_TypeShinryojo, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FTNT_Auth, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FTNT_Used, typeof(bool));
			dt.Columns.Add(col);
			
			col = new DataColumn(FTNT_AvailableMyNo, typeof(bool));
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
