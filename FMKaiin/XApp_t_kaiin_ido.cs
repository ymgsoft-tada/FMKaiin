
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
		/// フィールド[異動事由区分 0/None/ 1/Ido/医師会の異動・施設開業・施設異動 2/Kaiin/会員区分の変更 3/Shozai/施設所在地変更 4/Address/現住所変更 5/Etc/その他]。
		/// </summary>
		public const string FIdoJiyuKbn = "IdoJiyuKbn";
		/// <summary>
		/// 異動事由区分 0/None/ 1/Ido/医師会の異動・施設開業・施設異動 2/Kaiin/会員区分の変更 3/Shozai/施設所在地変更 4/Address/現住所変更 5/Etc/その他
		/// </summary>
		public eTypeIdoJiyu IdoJiyuKbn
		{
			get	{	return (eTypeIdoJiyu)Cast.Int(row == null ? null : row[FIdoJiyuKbn]);	}
			set	{	_set(FIdoJiyuKbn, (int)value);	}
		}
		
		/// <summary>
		/// 異動事由区分 0/None/ 1/Ido/医師会の異動・施設開業・施設異動 2/Kaiin/会員区分の変更 3/Shozai/施設所在地変更 4/Address/現住所変更 5/Etc/その他。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IdoJiyuKbn_Null
		{
			get	{	if (row == null || row[FIdoJiyuKbn] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIdoJiyuKbn]); }	}
			set	{	_set(FIdoJiyuKbn, value);	}
		}
		
		/// <summary>
		/// フィールド[施設異動詳細 0/None/ 1/KinmuSaki/勤務先 2/Kyuyo/休養 3/Haigyo/廃業 4/Taisｈoku/退職 5/Etc/その他]。
		/// </summary>
		public const string FIdoJiyuDetail_Ido = "IdoJiyuDetail_Ido";
		/// <summary>
		/// 施設異動詳細 0/None/ 1/KinmuSaki/勤務先 2/Kyuyo/休養 3/Haigyo/廃業 4/Taisｈoku/退職 5/Etc/その他
		/// </summary>
		public eIdoShisetsu IdoJiyuDetail_Ido
		{
			get	{	return (eIdoShisetsu)Cast.Int(row == null ? null : row[FIdoJiyuDetail_Ido]);	}
			set	{	_set(FIdoJiyuDetail_Ido, (int)value);	}
		}
		
		/// <summary>
		/// 施設異動詳細 0/None/ 1/KinmuSaki/勤務先 2/Kyuyo/休養 3/Haigyo/廃業 4/Taisｈoku/退職 5/Etc/その他。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IdoJiyuDetail_Ido_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_Ido] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIdoJiyuDetail_Ido]); }	}
			set	{	_set(FIdoJiyuDetail_Ido, value);	}
		}
		
		/// <summary>
		/// フィールド[事由:医師会の異動～-その他詳細記述]。
		/// </summary>
		public const string FIdoJiyuDetail_IdoMemo = "IdoJiyuDetail_IdoMemo";
		/// <summary>
		/// 事由:医師会の異動～-その他詳細記述
		/// </summary>
		public string IdoJiyuDetail_IdoMemo
		{
			get	{	return Cast.String(row == null ? null : row[FIdoJiyuDetail_IdoMemo]);	}
			set	{	_set(FIdoJiyuDetail_IdoMemo, value);	}
		}
		
		/// <summary>
		/// 事由:医師会の異動～-その他詳細記述。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IdoJiyuDetail_IdoMemo_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_IdoMemo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIdoJiyuDetail_IdoMemo]); }	}
			set	{	_set(FIdoJiyuDetail_IdoMemo, value);	}
		}
		
		/// <summary>
		/// フィールド[会員区分変更詳細 0/None/ 1/Kaigyo/開業 2/KanriHenko/管理者交代 3/KaisetsuHenko/開設者交代 4/KanriKaisetsuHenko/開設者・管理者交代 5/Haigyo/廃業・その他]。
		/// </summary>
		public const string FIdoJiyuDetail_Kaiin = "IdoJiyuDetail_Kaiin";
		/// <summary>
		/// 会員区分変更詳細 0/None/ 1/Kaigyo/開業 2/KanriHenko/管理者交代 3/KaisetsuHenko/開設者交代 4/KanriKaisetsuHenko/開設者・管理者交代 5/Haigyo/廃業・その他
		/// </summary>
		public eIdoKaiinHenko IdoJiyuDetail_Kaiin
		{
			get	{	return (eIdoKaiinHenko)Cast.Int(row == null ? null : row[FIdoJiyuDetail_Kaiin]);	}
			set	{	_set(FIdoJiyuDetail_Kaiin, (int)value);	}
		}
		
		/// <summary>
		/// 会員区分変更詳細 0/None/ 1/Kaigyo/開業 2/KanriHenko/管理者交代 3/KaisetsuHenko/開設者交代 4/KanriKaisetsuHenko/開設者・管理者交代 5/Haigyo/廃業・その他。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IdoJiyuDetail_Kaiin_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_Kaiin] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIdoJiyuDetail_Kaiin]); }	}
			set	{	_set(FIdoJiyuDetail_Kaiin, value);	}
		}
		
		/// <summary>
		/// フィールド[(入力なし)事由:会員区分の変更-その他詳細記述]。
		/// </summary>
		public const string FIdoJiyuDetail_KaiinMemo = "IdoJiyuDetail_KaiinMemo";
		/// <summary>
		/// (入力なし)事由:会員区分の変更-その他詳細記述
		/// </summary>
		public string IdoJiyuDetail_KaiinMemo
		{
			get	{	return Cast.String(row == null ? null : row[FIdoJiyuDetail_KaiinMemo]);	}
			set	{	_set(FIdoJiyuDetail_KaiinMemo, value);	}
		}
		
		/// <summary>
		/// (入力なし)事由:会員区分の変更-その他詳細記述。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IdoJiyuDetail_KaiinMemo_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_KaiinMemo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIdoJiyuDetail_KaiinMemo]); }	}
			set	{	_set(FIdoJiyuDetail_KaiinMemo, value);	}
		}
		
		/// <summary>
		/// フィールド[施設所在地変更詳細 0/None/ 1/Iten/移転 2/Etc/その他]。
		/// </summary>
		public const string FIdoJiyuDetail_Shozai = "IdoJiyuDetail_Shozai";
		/// <summary>
		/// 施設所在地変更詳細 0/None/ 1/Iten/移転 2/Etc/その他
		/// </summary>
		public eIdoShozai IdoJiyuDetail_Shozai
		{
			get	{	return (eIdoShozai)Cast.Int(row == null ? null : row[FIdoJiyuDetail_Shozai]);	}
			set	{	_set(FIdoJiyuDetail_Shozai, (int)value);	}
		}
		
		/// <summary>
		/// 施設所在地変更詳細 0/None/ 1/Iten/移転 2/Etc/その他。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IdoJiyuDetail_Shozai_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_Shozai] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIdoJiyuDetail_Shozai]); }	}
			set	{	_set(FIdoJiyuDetail_Shozai, value);	}
		}
		
		/// <summary>
		/// フィールド[事由:施設所在地変更-その他詳細記述]。
		/// </summary>
		public const string FIdoJiyuDetail_ShozaiMemo = "IdoJiyuDetail_ShozaiMemo";
		/// <summary>
		/// 事由:施設所在地変更-その他詳細記述
		/// </summary>
		public string IdoJiyuDetail_ShozaiMemo
		{
			get	{	return Cast.String(row == null ? null : row[FIdoJiyuDetail_ShozaiMemo]);	}
			set	{	_set(FIdoJiyuDetail_ShozaiMemo, value);	}
		}
		
		/// <summary>
		/// 事由:施設所在地変更-その他詳細記述。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IdoJiyuDetail_ShozaiMemo_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_ShozaiMemo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIdoJiyuDetail_ShozaiMemo]); }	}
			set	{	_set(FIdoJiyuDetail_ShozaiMemo, value);	}
		}
		
		/// <summary>
		/// フィールド[現住所変更詳細 0/None/ 1/Tenkyo/転居 2/Etc/その他]。
		/// </summary>
		public const string FIdoJiyuDetail_Address = "IdoJiyuDetail_Address";
		/// <summary>
		/// 現住所変更詳細 0/None/ 1/Tenkyo/転居 2/Etc/その他
		/// </summary>
		public eIdoAddress IdoJiyuDetail_Address
		{
			get	{	return (eIdoAddress)Cast.Int(row == null ? null : row[FIdoJiyuDetail_Address]);	}
			set	{	_set(FIdoJiyuDetail_Address, (int)value);	}
		}
		
		/// <summary>
		/// 現住所変更詳細 0/None/ 1/Tenkyo/転居 2/Etc/その他。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IdoJiyuDetail_Address_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_Address] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIdoJiyuDetail_Address]); }	}
			set	{	_set(FIdoJiyuDetail_Address, value);	}
		}
		
		/// <summary>
		/// フィールド[事由:現住所変更-その他詳細記述]。
		/// </summary>
		public const string FIdoJiyuDetail_AddressMemo = "IdoJiyuDetail_AddressMemo";
		/// <summary>
		/// 事由:現住所変更-その他詳細記述
		/// </summary>
		public string IdoJiyuDetail_AddressMemo
		{
			get	{	return Cast.String(row == null ? null : row[FIdoJiyuDetail_AddressMemo]);	}
			set	{	_set(FIdoJiyuDetail_AddressMemo, value);	}
		}
		
		/// <summary>
		/// 事由:現住所変更-その他詳細記述。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IdoJiyuDetail_AddressMemo_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_AddressMemo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIdoJiyuDetail_AddressMemo]); }	}
			set	{	_set(FIdoJiyuDetail_AddressMemo, value);	}
		}
		
		/// <summary>
		/// フィールド[異動その他詳細 0/None/ 1/MeishoHenko/名称変更 2/Hojinka/法人化 3/Etc/その他]。
		/// </summary>
		public const string FIdoJiyuDetail_Etc = "IdoJiyuDetail_Etc";
		/// <summary>
		/// 異動その他詳細 0/None/ 1/MeishoHenko/名称変更 2/Hojinka/法人化 3/Etc/その他
		/// </summary>
		public eIdoEtc IdoJiyuDetail_Etc
		{
			get	{	return (eIdoEtc)Cast.Int(row == null ? null : row[FIdoJiyuDetail_Etc]);	}
			set	{	_set(FIdoJiyuDetail_Etc, (int)value);	}
		}
		
		/// <summary>
		/// 異動その他詳細 0/None/ 1/MeishoHenko/名称変更 2/Hojinka/法人化 3/Etc/その他。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IdoJiyuDetail_Etc_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_Etc] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIdoJiyuDetail_Etc]); }	}
			set	{	_set(FIdoJiyuDetail_Etc, value);	}
		}
		
		/// <summary>
		/// フィールド[事由:その他-その他詳細記述]。
		/// </summary>
		public const string FIdoJiyuDetail_EtcMemo = "IdoJiyuDetail_EtcMemo";
		/// <summary>
		/// 事由:その他-その他詳細記述
		/// </summary>
		public string IdoJiyuDetail_EtcMemo
		{
			get	{	return Cast.String(row == null ? null : row[FIdoJiyuDetail_EtcMemo]);	}
			set	{	_set(FIdoJiyuDetail_EtcMemo, value);	}
		}
		
		/// <summary>
		/// 事由:その他-その他詳細記述。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IdoJiyuDetail_EtcMemo_Null
		{
			get	{	if (row == null || row[FIdoJiyuDetail_EtcMemo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIdoJiyuDetail_EtcMemo]); }	}
			set	{	_set(FIdoJiyuDetail_EtcMemo, value);	}
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
			
			col = new DataColumn(FIdoJiyuDetail_Ido, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuDetail_IdoMemo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuDetail_Kaiin, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuDetail_KaiinMemo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuDetail_Shozai, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuDetail_ShozaiMemo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuDetail_Address, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuDetail_AddressMemo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuDetail_Etc, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIdoJiyuDetail_EtcMemo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
