
//
// ※このプログラムはDBAutoProperties2Access2000により自動的に生成されました。(fj)
//
// MDB File :
//		G:\client\DotNet4.6\Ishikai\FMKaiin\FMKaiin\bin\Debug\system\Data.mdb
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
	public partial class t_kaiin_ido : FieldProp
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
		/// フィールド[異動情報ID]。
		/// </summary>
		public const string FID_Ido = "ID_Ido";
		/// <summary>
		/// 異動情報ID
		/// </summary>
		public int ID_Ido
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Ido]);	}
			set	{	_set(FID_Ido, value);	}
		}
		
		/// <summary>
		/// 異動情報ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Ido_Null
		{
			get	{	if (row == null || row[FID_Ido] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Ido]); }	}
			set	{	_set(FID_Ido, value);	}
		}
		
		/// <summary>
		/// フィールド[異動年月日]。
		/// </summary>
		public const string FIdoDate = "IdoDate";
		/// <summary>
		/// 異動年月日
		/// </summary>
		public DateTime IdoDate
		{
			get	{	return Cast.DateTime(row == null ? null : row[FIdoDate]);	}
			set	{	_set(FIdoDate, value);	}
		}
		
		/// <summary>
		/// 異動年月日。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? IdoDate_Null
		{
			get	{	if (row == null || row[FIdoDate] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FIdoDate]); }	}
			set	{	_set(FIdoDate, value);	}
		}
		
		/// <summary>
		/// フィールド[異動事由区分 0/None/ 1/Kaigyo/施設開業 2/Ido/施設異動 3/Kaiin/会員区分変更 4/Etc/その他]。
		/// </summary>
		public const string FIdoJiyuKbn = "IdoJiyuKbn";
		/// <summary>
		/// 異動事由区分 0/None/ 1/Kaigyo/施設開業 2/Ido/施設異動 3/Kaiin/会員区分変更 4/Etc/その他
		/// </summary>
		public eTypeIdoJiyu IdoJiyuKbn
		{
			get	{	return (eTypeIdoJiyu)Cast.Int(row == null ? null : row[FIdoJiyuKbn]);	}
			set	{	_set(FIdoJiyuKbn, (int)value);	}
		}
		
		/// <summary>
		/// 異動事由区分 0/None/ 1/Kaigyo/施設開業 2/Ido/施設異動 3/Kaiin/会員区分変更 4/Etc/その他。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IdoJiyuKbn_Null
		{
			get	{	if (row == null || row[FIdoJiyuKbn] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIdoJiyuKbn]); }	}
			set	{	_set(FIdoJiyuKbn, value);	}
		}
		
		/// <summary>
		/// フィールド[施設開業詳細(選択肢なし)]。
		/// </summary>
		public const string FIdoJiyuDetail_Kaigyo = "IdoJiyuDetail_Kaigyo";
		/// <summary>
		/// 施設開業詳細(選択肢なし)
		/// </summary>
		public int IdoJiyuDetail_Kaigyo
		{
			get	{	return Cast.Int(row == null ? null : row[FIdoJiyuDetail_Kaigyo]);	}
			set	{	_set(FIdoJiyuDetail_Kaigyo, value);	}
		}
		
		/// <summary>
		/// 施設開業詳細(選択肢なし)。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IdoJiyuDetail_Kaigyo_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_Kaigyo] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIdoJiyuDetail_Kaigyo]); }	}
			set	{	_set(FIdoJiyuDetail_Kaigyo, value);	}
		}
		
		/// <summary>
		/// フィールド[施設異動詳細 0/None/ 1/KinmuSaki/勤務先 2/Kyuyo/休養 3/Haigyo/廃業 4/Taisyoku/退職]。
		/// </summary>
		public const string FIdoJiyuDetail_Ido = "IdoJiyuDetail_Ido";
		/// <summary>
		/// 施設異動詳細 0/None/ 1/KinmuSaki/勤務先 2/Kyuyo/休養 3/Haigyo/廃業 4/Taisyoku/退職
		/// </summary>
		public eIdoShisetsu IdoJiyuDetail_Ido
		{
			get	{	return (eIdoShisetsu)Cast.Int(row == null ? null : row[FIdoJiyuDetail_Ido]);	}
			set	{	_set(FIdoJiyuDetail_Ido, (int)value);	}
		}
		
		/// <summary>
		/// 施設異動詳細 0/None/ 1/KinmuSaki/勤務先 2/Kyuyo/休養 3/Haigyo/廃業 4/Taisyoku/退職。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IdoJiyuDetail_Ido_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_Ido] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIdoJiyuDetail_Ido]); }	}
			set	{	_set(FIdoJiyuDetail_Ido, value);	}
		}
		
		/// <summary>
		/// フィールド[会員区分変更詳細 0/None/ 1/Kaigyo/開業 2/KanriHenko/管理者交代 3/KaisetsuHenko/開設者交代 4/KanriKaisetsuHenko/開設者・管理者交代 5/Haigyo/廃業]。
		/// </summary>
		public const string FIdoJiyuDetail_Kaiin = "IdoJiyuDetail_Kaiin";
		/// <summary>
		/// 会員区分変更詳細 0/None/ 1/Kaigyo/開業 2/KanriHenko/管理者交代 3/KaisetsuHenko/開設者交代 4/KanriKaisetsuHenko/開設者・管理者交代 5/Haigyo/廃業
		/// </summary>
		public eIdoKaiinHenko IdoJiyuDetail_Kaiin
		{
			get	{	return (eIdoKaiinHenko)Cast.Int(row == null ? null : row[FIdoJiyuDetail_Kaiin]);	}
			set	{	_set(FIdoJiyuDetail_Kaiin, (int)value);	}
		}
		
		/// <summary>
		/// 会員区分変更詳細 0/None/ 1/Kaigyo/開業 2/KanriHenko/管理者交代 3/KaisetsuHenko/開設者交代 4/KanriKaisetsuHenko/開設者・管理者交代 5/Haigyo/廃業。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IdoJiyuDetail_Kaiin_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_Kaiin] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIdoJiyuDetail_Kaiin]); }	}
			set	{	_set(FIdoJiyuDetail_Kaiin, value);	}
		}
		
		/// <summary>
		/// フィールド[異動その他詳細 0/None/ 1/Iten/移転 2/MeisyoHenko/名称変更 3/Hojinka/法人化 4/AddrHenko/自宅住所変更]。
		/// </summary>
		public const string FIdoJiyuDetail_Etc = "IdoJiyuDetail_Etc";
		/// <summary>
		/// 異動その他詳細 0/None/ 1/Iten/移転 2/MeisyoHenko/名称変更 3/Hojinka/法人化 4/AddrHenko/自宅住所変更
		/// </summary>
		public eIdoEtc IdoJiyuDetail_Etc
		{
			get	{	return (eIdoEtc)Cast.Int(row == null ? null : row[FIdoJiyuDetail_Etc]);	}
			set	{	_set(FIdoJiyuDetail_Etc, (int)value);	}
		}
		
		/// <summary>
		/// 異動その他詳細 0/None/ 1/Iten/移転 2/MeisyoHenko/名称変更 3/Hojinka/法人化 4/AddrHenko/自宅住所変更。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IdoJiyuDetail_Etc_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_Etc] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIdoJiyuDetail_Etc]); }	}
			set	{	_set(FIdoJiyuDetail_Etc, value);	}
		}
		
		/// <summary>
		/// フィールド[その他詳細記述]。
		/// </summary>
		public const string FIdoJiyuEtcMemo = "IdoJiyuEtcMemo";
		/// <summary>
		/// その他詳細記述
		/// </summary>
		public string IdoJiyuEtcMemo
		{
			get	{	return Cast.String(row == null ? null : row[FIdoJiyuEtcMemo]);	}
			set	{	_set(FIdoJiyuEtcMemo, value);	}
		}
		
		/// <summary>
		/// その他詳細記述。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IdoJiyuEtcMemo_Null
		{
			get	{	if (row == null || row[FIdoJiyuEtcMemo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIdoJiyuEtcMemo]); }	}
			set	{	_set(FIdoJiyuEtcMemo, value);	}
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
		public t_kaiin_ido(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_kaiin_ido 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_kaiin_ido 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_kaiin_ido");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Kaiin, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Ido, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoDate, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuKbn, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuDetail_Kaigyo, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuDetail_Ido, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuDetail_Kaiin, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuDetail_Etc, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuEtcMemo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
