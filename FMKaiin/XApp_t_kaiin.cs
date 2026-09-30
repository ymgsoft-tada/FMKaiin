
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
	public partial class t_kaiin : FieldProp
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
		/// フィールド[会員CD]。
		/// </summary>
		public const string FCD_Kaiin = "CD_Kaiin";
		/// <summary>
		/// 会員CD
		/// </summary>
		public int CD_Kaiin
		{
			get	{	return Cast.Int(row == null ? null : row[FCD_Kaiin]);	}
			set	{	_set(FCD_Kaiin, value);	}
		}
		
		/// <summary>
		/// 会員CD。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? CD_Kaiin_Null
		{
			get	{	if (row == null || row[FCD_Kaiin] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FCD_Kaiin]); }	}
			set	{	_set(FCD_Kaiin, value);	}
		}
		
		/// <summary>
		/// フィールド[会員名]。
		/// </summary>
		public const string FKaiin_Name = "Kaiin_Name";
		/// <summary>
		/// 会員名
		/// </summary>
		public string Kaiin_Name
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_Name]);	}
			set	{	_set(FKaiin_Name, value);	}
		}
		
		/// <summary>
		/// 会員名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_Name_Null
		{
			get	{	if (row == null || row[FKaiin_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_Name]); }	}
			set	{	_set(FKaiin_Name, value);	}
		}
		
		/// <summary>
		/// フィールド[会員名フリガナ]。
		/// </summary>
		public const string FKaiin_NameKana = "Kaiin_NameKana";
		/// <summary>
		/// 会員名フリガナ
		/// </summary>
		public string Kaiin_NameKana
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_NameKana]);	}
			set	{	_set(FKaiin_NameKana, value);	}
		}
		
		/// <summary>
		/// 会員名フリガナ。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_NameKana_Null
		{
			get	{	if (row == null || row[FKaiin_NameKana] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_NameKana]); }	}
			set	{	_set(FKaiin_NameKana, value);	}
		}
		
		/// <summary>
		/// フィールド[性別 0/None/ 1/Men/男性 2/Women/女性]。
		/// </summary>
		public const string FKaiin_Sex = "Kaiin_Sex";
		/// <summary>
		/// 性別 0/None/ 1/Men/男性 2/Women/女性
		/// </summary>
		public eSex Kaiin_Sex
		{
			get	{	return (eSex)Cast.Int(row == null ? null : row[FKaiin_Sex]);	}
			set	{	_set(FKaiin_Sex, (int)value);	}
		}
		
		/// <summary>
		/// 性別 0/None/ 1/Men/男性 2/Women/女性。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_Sex_Null
		{
			get	{	if (row == null || row[FKaiin_Sex] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_Sex]); }	}
			set	{	_set(FKaiin_Sex, value);	}
		}
		
		/// <summary>
		/// フィールド[生年月日]。
		/// </summary>
		public const string FKaiin_DateBirth = "Kaiin_DateBirth";
		/// <summary>
		/// 生年月日
		/// </summary>
		public DateTime Kaiin_DateBirth
		{
			get	{	return Cast.DateTime(row == null ? null : row[FKaiin_DateBirth]);	}
			set	{	_set(FKaiin_DateBirth, value);	}
		}
		
		/// <summary>
		/// 生年月日。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? Kaiin_DateBirth_Null
		{
			get	{	if (row == null || row[FKaiin_DateBirth] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FKaiin_DateBirth]); }	}
			set	{	_set(FKaiin_DateBirth, value);	}
		}
		
		/// <summary>
		/// フィールド[郵便番号]。
		/// </summary>
		public const string FKaiin_Post = "Kaiin_Post";
		/// <summary>
		/// 郵便番号
		/// </summary>
		public string Kaiin_Post
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_Post]);	}
			set	{	_set(FKaiin_Post, value);	}
		}
		
		/// <summary>
		/// 郵便番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_Post_Null
		{
			get	{	if (row == null || row[FKaiin_Post] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_Post]); }	}
			set	{	_set(FKaiin_Post, value);	}
		}
		
		/// <summary>
		/// フィールド[住所１]。
		/// </summary>
		public const string FKaiin_Addr1 = "Kaiin_Addr1";
		/// <summary>
		/// 住所１
		/// </summary>
		public string Kaiin_Addr1
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_Addr1]);	}
			set	{	_set(FKaiin_Addr1, value);	}
		}
		
		/// <summary>
		/// 住所１。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_Addr1_Null
		{
			get	{	if (row == null || row[FKaiin_Addr1] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_Addr1]); }	}
			set	{	_set(FKaiin_Addr1, value);	}
		}
		
		/// <summary>
		/// フィールド[住所2]。
		/// </summary>
		public const string FKaiin_Addr2 = "Kaiin_Addr2";
		/// <summary>
		/// 住所2
		/// </summary>
		public string Kaiin_Addr2
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_Addr2]);	}
			set	{	_set(FKaiin_Addr2, value);	}
		}
		
		/// <summary>
		/// 住所2。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_Addr2_Null
		{
			get	{	if (row == null || row[FKaiin_Addr2] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_Addr2]); }	}
			set	{	_set(FKaiin_Addr2, value);	}
		}
		
		/// <summary>
		/// フィールド[電話番号]。
		/// </summary>
		public const string FKaiin_Tel1 = "Kaiin_Tel1";
		/// <summary>
		/// 電話番号
		/// </summary>
		public string Kaiin_Tel1
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_Tel1]);	}
			set	{	_set(FKaiin_Tel1, value);	}
		}
		
		/// <summary>
		/// 電話番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_Tel1_Null
		{
			get	{	if (row == null || row[FKaiin_Tel1] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_Tel1]); }	}
			set	{	_set(FKaiin_Tel1, value);	}
		}
		
		/// <summary>
		/// フィールド[携帯電話番号]。
		/// </summary>
		public const string FKaiin_Tel2 = "Kaiin_Tel2";
		/// <summary>
		/// 携帯電話番号
		/// </summary>
		public string Kaiin_Tel2
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_Tel2]);	}
			set	{	_set(FKaiin_Tel2, value);	}
		}
		
		/// <summary>
		/// 携帯電話番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_Tel2_Null
		{
			get	{	if (row == null || row[FKaiin_Tel2] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_Tel2]); }	}
			set	{	_set(FKaiin_Tel2, value);	}
		}
		
		/// <summary>
		/// フィールド[FAX番号]。
		/// </summary>
		public const string FKaiin_Fax = "Kaiin_Fax";
		/// <summary>
		/// FAX番号
		/// </summary>
		public string Kaiin_Fax
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_Fax]);	}
			set	{	_set(FKaiin_Fax, value);	}
		}
		
		/// <summary>
		/// FAX番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_Fax_Null
		{
			get	{	if (row == null || row[FKaiin_Fax] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_Fax]); }	}
			set	{	_set(FKaiin_Fax, value);	}
		}
		
		/// <summary>
		/// フィールド[メールアドレス１]。
		/// </summary>
		public const string FKaiin_Mail1 = "Kaiin_Mail1";
		/// <summary>
		/// メールアドレス１
		/// </summary>
		public string Kaiin_Mail1
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_Mail1]);	}
			set	{	_set(FKaiin_Mail1, value);	}
		}
		
		/// <summary>
		/// メールアドレス１。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_Mail1_Null
		{
			get	{	if (row == null || row[FKaiin_Mail1] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_Mail1]); }	}
			set	{	_set(FKaiin_Mail1, value);	}
		}
		
		/// <summary>
		/// フィールド[メールアドレス２]。
		/// </summary>
		public const string FKaiin_Mail2 = "Kaiin_Mail2";
		/// <summary>
		/// メールアドレス２
		/// </summary>
		public string Kaiin_Mail2
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_Mail2]);	}
			set	{	_set(FKaiin_Mail2, value);	}
		}
		
		/// <summary>
		/// メールアドレス２。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_Mail2_Null
		{
			get	{	if (row == null || row[FKaiin_Mail2] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_Mail2]); }	}
			set	{	_set(FKaiin_Mail2, value);	}
		}
		
		/// <summary>
		/// フィールド[医籍登録番号]。
		/// </summary>
		public const string FKaiin_IsekiTorokuNo = "Kaiin_IsekiTorokuNo";
		/// <summary>
		/// 医籍登録番号
		/// </summary>
		public int Kaiin_IsekiTorokuNo
		{
			get	{	return Cast.Int(row == null ? null : row[FKaiin_IsekiTorokuNo]);	}
			set	{	_set(FKaiin_IsekiTorokuNo, value);	}
		}
		
		/// <summary>
		/// 医籍登録番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_IsekiTorokuNo_Null
		{
			get	{	if (row == null || row[FKaiin_IsekiTorokuNo] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_IsekiTorokuNo]); }	}
			set	{	_set(FKaiin_IsekiTorokuNo, value);	}
		}
		
		/// <summary>
		/// フィールド[医籍登録年月日]。
		/// </summary>
		public const string FKaiin_DateIsekiToroku = "Kaiin_DateIsekiToroku";
		/// <summary>
		/// 医籍登録年月日
		/// </summary>
		public DateTime Kaiin_DateIsekiToroku
		{
			get	{	return Cast.DateTime(row == null ? null : row[FKaiin_DateIsekiToroku]);	}
			set	{	_set(FKaiin_DateIsekiToroku, value);	}
		}
		
		/// <summary>
		/// 医籍登録年月日。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? Kaiin_DateIsekiToroku_Null
		{
			get	{	if (row == null || row[FKaiin_DateIsekiToroku] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FKaiin_DateIsekiToroku]); }	}
			set	{	_set(FKaiin_DateIsekiToroku, value);	}
		}
		
		/// <summary>
		/// フィールド[出身校]。
		/// </summary>
		public const string FKaiin_ShusshinKou = "Kaiin_ShusshinKou";
		/// <summary>
		/// 出身校
		/// </summary>
		public string Kaiin_ShusshinKou
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_ShusshinKou]);	}
			set	{	_set(FKaiin_ShusshinKou, value);	}
		}
		
		/// <summary>
		/// 出身校。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_ShusshinKou_Null
		{
			get	{	if (row == null || row[FKaiin_ShusshinKou] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_ShusshinKou]); }	}
			set	{	_set(FKaiin_ShusshinKou, value);	}
		}
		
		/// <summary>
		/// フィールド[卒業年月]。
		/// </summary>
		public const string FKaiin_DateSotsugyo = "Kaiin_DateSotsugyo";
		/// <summary>
		/// 卒業年月
		/// </summary>
		public DateTime Kaiin_DateSotsugyo
		{
			get	{	return Cast.DateTime(row == null ? null : row[FKaiin_DateSotsugyo]);	}
			set	{	_set(FKaiin_DateSotsugyo, value);	}
		}
		
		/// <summary>
		/// 卒業年月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? Kaiin_DateSotsugyo_Null
		{
			get	{	if (row == null || row[FKaiin_DateSotsugyo] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FKaiin_DateSotsugyo]); }	}
			set	{	_set(FKaiin_DateSotsugyo, value);	}
		}
		
		/// <summary>
		/// フィールド[出身校(大学院)]。
		/// </summary>
		public const string FKaiin_ShusshinIn = "Kaiin_ShusshinIn";
		/// <summary>
		/// 出身校(大学院)
		/// </summary>
		public string Kaiin_ShusshinIn
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_ShusshinIn]);	}
			set	{	_set(FKaiin_ShusshinIn, value);	}
		}
		
		/// <summary>
		/// 出身校(大学院)。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_ShusshinIn_Null
		{
			get	{	if (row == null || row[FKaiin_ShusshinIn] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_ShusshinIn]); }	}
			set	{	_set(FKaiin_ShusshinIn, value);	}
		}
		
		/// <summary>
		/// フィールド[修了年月]。
		/// </summary>
		public const string FKaiin_DateShuryo = "Kaiin_DateShuryo";
		/// <summary>
		/// 修了年月
		/// </summary>
		public DateTime Kaiin_DateShuryo
		{
			get	{	return Cast.DateTime(row == null ? null : row[FKaiin_DateShuryo]);	}
			set	{	_set(FKaiin_DateShuryo, value);	}
		}
		
		/// <summary>
		/// 修了年月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? Kaiin_DateShuryo_Null
		{
			get	{	if (row == null || row[FKaiin_DateShuryo] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FKaiin_DateShuryo]); }	}
			set	{	_set(FKaiin_DateShuryo, value);	}
		}
		
		/// <summary>
		/// フィールド[学位取得年月]。
		/// </summary>
		public const string FKaiin_DateShutokuGakui = "Kaiin_DateShutokuGakui";
		/// <summary>
		/// 学位取得年月
		/// </summary>
		public DateTime Kaiin_DateShutokuGakui
		{
			get	{	return Cast.DateTime(row == null ? null : row[FKaiin_DateShutokuGakui]);	}
			set	{	_set(FKaiin_DateShutokuGakui, value);	}
		}
		
		/// <summary>
		/// 学位取得年月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? Kaiin_DateShutokuGakui_Null
		{
			get	{	if (row == null || row[FKaiin_DateShutokuGakui] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FKaiin_DateShutokuGakui]); }	}
			set	{	_set(FKaiin_DateShutokuGakui, value);	}
		}
		
		/// <summary>
		/// フィールド[在籍区分 0/None/ 1/Zaiseki/在籍 2/Ido/異動 3/Taikai/退会]。
		/// </summary>
		public const string FKaiin_TypeZaiseki = "Kaiin_TypeZaiseki";
		/// <summary>
		/// 在籍区分 0/None/ 1/Zaiseki/在籍 2/Ido/異動 3/Taikai/退会
		/// </summary>
		public eTypeZaiseki Kaiin_TypeZaiseki
		{
			get	{	return (eTypeZaiseki)Cast.Int(row == null ? null : row[FKaiin_TypeZaiseki]);	}
			set	{	_set(FKaiin_TypeZaiseki, (int)value);	}
		}
		
		/// <summary>
		/// 在籍区分 0/None/ 1/Zaiseki/在籍 2/Ido/異動 3/Taikai/退会。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_TypeZaiseki_Null
		{
			get	{	if (row == null || row[FKaiin_TypeZaiseki] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_TypeZaiseki]); }	}
			set	{	_set(FKaiin_TypeZaiseki, value);	}
		}
		
		/// <summary>
		/// フィールド[承認日]。
		/// </summary>
		public const string FKaiin_DateShonin = "Kaiin_DateShonin";
		/// <summary>
		/// 承認日
		/// </summary>
		public DateTime Kaiin_DateShonin
		{
			get	{	return Cast.DateTime(row == null ? null : row[FKaiin_DateShonin]);	}
			set	{	_set(FKaiin_DateShonin, value);	}
		}
		
		/// <summary>
		/// 承認日。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? Kaiin_DateShonin_Null
		{
			get	{	if (row == null || row[FKaiin_DateShonin] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FKaiin_DateShonin]); }	}
			set	{	_set(FKaiin_DateShonin, value);	}
		}
		
		/// <summary>
		/// フィールド[登録日]。
		/// </summary>
		public const string FKaiin_DateToroku = "Kaiin_DateToroku";
		/// <summary>
		/// 登録日
		/// </summary>
		public DateTime Kaiin_DateToroku
		{
			get	{	return Cast.DateTime(row == null ? null : row[FKaiin_DateToroku]);	}
			set	{	_set(FKaiin_DateToroku, value);	}
		}
		
		/// <summary>
		/// 登録日。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? Kaiin_DateToroku_Null
		{
			get	{	if (row == null || row[FKaiin_DateToroku] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FKaiin_DateToroku]); }	}
			set	{	_set(FKaiin_DateToroku, value);	}
		}
		
		/// <summary>
		/// フィールド[入会年月日]。
		/// </summary>
		public const string FKaiin_DateNyuukai = "Kaiin_DateNyuukai";
		/// <summary>
		/// 入会年月日
		/// </summary>
		public DateTime Kaiin_DateNyuukai
		{
			get	{	return Cast.DateTime(row == null ? null : row[FKaiin_DateNyuukai]);	}
			set	{	_set(FKaiin_DateNyuukai, value);	}
		}
		
		/// <summary>
		/// 入会年月日。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? Kaiin_DateNyuukai_Null
		{
			get	{	if (row == null || row[FKaiin_DateNyuukai] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FKaiin_DateNyuukai]); }	}
			set	{	_set(FKaiin_DateNyuukai, value);	}
		}
		
		/// <summary>
		/// フィールド[会員区分ID]。
		/// </summary>
		public const string FID_KaiinKbn = "ID_KaiinKbn";
		/// <summary>
		/// 会員区分ID
		/// </summary>
		public int ID_KaiinKbn
		{
			get	{	return Cast.Int(row == null ? null : row[FID_KaiinKbn]);	}
			set	{	_set(FID_KaiinKbn, value);	}
		}
		
		/// <summary>
		/// 会員区分ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_KaiinKbn_Null
		{
			get	{	if (row == null || row[FID_KaiinKbn] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_KaiinKbn]); }	}
			set	{	_set(FID_KaiinKbn, value);	}
		}
		
		/// <summary>
		/// フィールド[日医会員区分(医会会費ID)]。
		/// </summary>
		public const string FID_Kaihi_Nichii = "ID_Kaihi_Nichii";
		/// <summary>
		/// 日医会員区分(医会会費ID)
		/// </summary>
		public int ID_Kaihi_Nichii
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Kaihi_Nichii]);	}
			set	{	_set(FID_Kaihi_Nichii, value);	}
		}
		
		/// <summary>
		/// 日医会員区分(医会会費ID)。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Kaihi_Nichii_Null
		{
			get	{	if (row == null || row[FID_Kaihi_Nichii] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Kaihi_Nichii]); }	}
			set	{	_set(FID_Kaihi_Nichii, value);	}
		}
		
		/// <summary>
		/// フィールド[支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金]。
		/// </summary>
		public const string FKaiin_NichiiShiharai = "Kaiin_NichiiShiharai";
		/// <summary>
		/// 支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金
		/// </summary>
		public eShiharai Kaiin_NichiiShiharai
		{
			get	{	return (eShiharai)Cast.Int(row == null ? null : row[FKaiin_NichiiShiharai]);	}
			set	{	_set(FKaiin_NichiiShiharai, (int)value);	}
		}
		
		/// <summary>
		/// 支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_NichiiShiharai_Null
		{
			get	{	if (row == null || row[FKaiin_NichiiShiharai] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_NichiiShiharai]); }	}
			set	{	_set(FKaiin_NichiiShiharai, value);	}
		}
		
		/// <summary>
		/// フィールド[県医会員区分(医会会費ID)]。
		/// </summary>
		public const string FID_Kaihi_Keni = "ID_Kaihi_Keni";
		/// <summary>
		/// 県医会員区分(医会会費ID)
		/// </summary>
		public int ID_Kaihi_Keni
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Kaihi_Keni]);	}
			set	{	_set(FID_Kaihi_Keni, value);	}
		}
		
		/// <summary>
		/// 県医会員区分(医会会費ID)。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Kaihi_Keni_Null
		{
			get	{	if (row == null || row[FID_Kaihi_Keni] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Kaihi_Keni]); }	}
			set	{	_set(FID_Kaihi_Keni, value);	}
		}
		
		/// <summary>
		/// フィールド[支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金]。
		/// </summary>
		public const string FKaiin_KeniShiharai = "Kaiin_KeniShiharai";
		/// <summary>
		/// 支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金
		/// </summary>
		public eShiharai Kaiin_KeniShiharai
		{
			get	{	return (eShiharai)Cast.Int(row == null ? null : row[FKaiin_KeniShiharai]);	}
			set	{	_set(FKaiin_KeniShiharai, (int)value);	}
		}
		
		/// <summary>
		/// 支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_KeniShiharai_Null
		{
			get	{	if (row == null || row[FKaiin_KeniShiharai] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_KeniShiharai]); }	}
			set	{	_set(FKaiin_KeniShiharai, value);	}
		}
		
		/// <summary>
		/// フィールド[市医会員区分(医会会費ID)]。
		/// </summary>
		public const string FID_Kaihi_Shii = "ID_Kaihi_Shii";
		/// <summary>
		/// 市医会員区分(医会会費ID)
		/// </summary>
		public int ID_Kaihi_Shii
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Kaihi_Shii]);	}
			set	{	_set(FID_Kaihi_Shii, value);	}
		}
		
		/// <summary>
		/// 市医会員区分(医会会費ID)。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Kaihi_Shii_Null
		{
			get	{	if (row == null || row[FID_Kaihi_Shii] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Kaihi_Shii]); }	}
			set	{	_set(FID_Kaihi_Shii, value);	}
		}
		
		/// <summary>
		/// フィールド[支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金]。
		/// </summary>
		public const string FKaiin_ShiiShiharai = "Kaiin_ShiiShiharai";
		/// <summary>
		/// 支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金
		/// </summary>
		public eShiharai Kaiin_ShiiShiharai
		{
			get	{	return (eShiharai)Cast.Int(row == null ? null : row[FKaiin_ShiiShiharai]);	}
			set	{	_set(FKaiin_ShiiShiharai, (int)value);	}
		}
		
		/// <summary>
		/// 支払方法 0/None/ 1/Koza1/口座① 2/Koza2/口座② 3/Koza3/口座③ 9/Genkin/現金。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_ShiiShiharai_Null
		{
			get	{	if (row == null || row[FKaiin_ShiiShiharai] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_ShiiShiharai]); }	}
			set	{	_set(FKaiin_ShiiShiharai, value);	}
		}
		
		/// <summary>
		/// フィールド[異動前医師会ID]。
		/// </summary>
		public const string FID_Ishikai_Idomae = "ID_Ishikai_Idomae";
		/// <summary>
		/// 異動前医師会ID
		/// </summary>
		public int ID_Ishikai_Idomae
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Ishikai_Idomae]);	}
			set	{	_set(FID_Ishikai_Idomae, value);	}
		}
		
		/// <summary>
		/// 異動前医師会ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Ishikai_Idomae_Null
		{
			get	{	if (row == null || row[FID_Ishikai_Idomae] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Ishikai_Idomae]); }	}
			set	{	_set(FID_Ishikai_Idomae, value);	}
		}
		
		/// <summary>
		/// フィールド[診療科目(主)ID]。
		/// </summary>
		public const string FID_Shinryoka_Main = "ID_Shinryoka_Main";
		/// <summary>
		/// 診療科目(主)ID
		/// </summary>
		public int ID_Shinryoka_Main
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Shinryoka_Main]);	}
			set	{	_set(FID_Shinryoka_Main, value);	}
		}
		
		/// <summary>
		/// 診療科目(主)ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Shinryoka_Main_Null
		{
			get	{	if (row == null || row[FID_Shinryoka_Main] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Shinryoka_Main]); }	}
			set	{	_set(FID_Shinryoka_Main, value);	}
		}
		
		/// <summary>
		/// フィールド[所属医療機関ID]。
		/// </summary>
		public const string FID_Iryokikan = "ID_Iryokikan";
		/// <summary>
		/// 所属医療機関ID
		/// </summary>
		public int ID_Iryokikan
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Iryokikan]);	}
			set	{	_set(FID_Iryokikan, value);	}
		}
		
		/// <summary>
		/// 所属医療機関ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Iryokikan_Null
		{
			get	{	if (row == null || row[FID_Iryokikan] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Iryokikan]); }	}
			set	{	_set(FID_Iryokikan, value);	}
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
		/// フィールド[指定医(chkbox)]。
		/// </summary>
		public const string FKaiin_Shiteii = "Kaiin_Shiteii";
		/// <summary>
		/// 指定医(chkbox)
		/// </summary>
		public int Kaiin_Shiteii
		{
			get	{	return Cast.Int(row == null ? null : row[FKaiin_Shiteii]);	}
			set	{	_set(FKaiin_Shiteii, value);	}
		}
		
		/// <summary>
		/// 指定医(chkbox)。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_Shiteii_Null
		{
			get	{	if (row == null || row[FKaiin_Shiteii] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_Shiteii]); }	}
			set	{	_set(FKaiin_Shiteii, value);	}
		}
		
		/// <summary>
		/// フィールド[口座1_銀行コード]。
		/// </summary>
		public const string FKaiin_BankCode1 = "Kaiin_BankCode1";
		/// <summary>
		/// 口座1_銀行コード
		/// </summary>
		public int Kaiin_BankCode1
		{
			get	{	return Cast.Int(row == null ? null : row[FKaiin_BankCode1]);	}
			set	{	_set(FKaiin_BankCode1, value);	}
		}
		
		/// <summary>
		/// 口座1_銀行コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_BankCode1_Null
		{
			get	{	if (row == null || row[FKaiin_BankCode1] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_BankCode1]); }	}
			set	{	_set(FKaiin_BankCode1, value);	}
		}
		
		/// <summary>
		/// フィールド[口座1_銀行支店コード]。
		/// </summary>
		public const string FKaiin_BankShitenCode1 = "Kaiin_BankShitenCode1";
		/// <summary>
		/// 口座1_銀行支店コード
		/// </summary>
		public int Kaiin_BankShitenCode1
		{
			get	{	return Cast.Int(row == null ? null : row[FKaiin_BankShitenCode1]);	}
			set	{	_set(FKaiin_BankShitenCode1, value);	}
		}
		
		/// <summary>
		/// 口座1_銀行支店コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_BankShitenCode1_Null
		{
			get	{	if (row == null || row[FKaiin_BankShitenCode1] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_BankShitenCode1]); }	}
			set	{	_set(FKaiin_BankShitenCode1, value);	}
		}
		
		/// <summary>
		/// フィールド[口座区分 0/None/ 1/Futsu/普通 2/Touza/当座]。
		/// </summary>
		public const string FKaiin_BankKozaType1 = "Kaiin_BankKozaType1";
		/// <summary>
		/// 口座区分 0/None/ 1/Futsu/普通 2/Touza/当座
		/// </summary>
		public eTypeKoza Kaiin_BankKozaType1
		{
			get	{	return (eTypeKoza)Cast.Int(row == null ? null : row[FKaiin_BankKozaType1]);	}
			set	{	_set(FKaiin_BankKozaType1, (int)value);	}
		}
		
		/// <summary>
		/// 口座区分 0/None/ 1/Futsu/普通 2/Touza/当座。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_BankKozaType1_Null
		{
			get	{	if (row == null || row[FKaiin_BankKozaType1] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_BankKozaType1]); }	}
			set	{	_set(FKaiin_BankKozaType1, value);	}
		}
		
		/// <summary>
		/// フィールド[口座1_口座番号]。
		/// </summary>
		public const string FKaiin_BankKozaNo1 = "Kaiin_BankKozaNo1";
		/// <summary>
		/// 口座1_口座番号
		/// </summary>
		public string Kaiin_BankKozaNo1
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_BankKozaNo1]);	}
			set	{	_set(FKaiin_BankKozaNo1, value);	}
		}
		
		/// <summary>
		/// 口座1_口座番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_BankKozaNo1_Null
		{
			get	{	if (row == null || row[FKaiin_BankKozaNo1] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_BankKozaNo1]); }	}
			set	{	_set(FKaiin_BankKozaNo1, value);	}
		}
		
		/// <summary>
		/// フィールド[口座1_口座名義]。
		/// </summary>
		public const string FKaiin_BankKozaName1 = "Kaiin_BankKozaName1";
		/// <summary>
		/// 口座1_口座名義
		/// </summary>
		public string Kaiin_BankKozaName1
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_BankKozaName1]);	}
			set	{	_set(FKaiin_BankKozaName1, value);	}
		}
		
		/// <summary>
		/// 口座1_口座名義。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_BankKozaName1_Null
		{
			get	{	if (row == null || row[FKaiin_BankKozaName1] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_BankKozaName1]); }	}
			set	{	_set(FKaiin_BankKozaName1, value);	}
		}
		
		/// <summary>
		/// フィールド[口座2_銀行コード]。
		/// </summary>
		public const string FKaiin_BankCode2 = "Kaiin_BankCode2";
		/// <summary>
		/// 口座2_銀行コード
		/// </summary>
		public int Kaiin_BankCode2
		{
			get	{	return Cast.Int(row == null ? null : row[FKaiin_BankCode2]);	}
			set	{	_set(FKaiin_BankCode2, value);	}
		}
		
		/// <summary>
		/// 口座2_銀行コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_BankCode2_Null
		{
			get	{	if (row == null || row[FKaiin_BankCode2] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_BankCode2]); }	}
			set	{	_set(FKaiin_BankCode2, value);	}
		}
		
		/// <summary>
		/// フィールド[口座2_銀行支店コード]。
		/// </summary>
		public const string FKaiin_BankShitenCode2 = "Kaiin_BankShitenCode2";
		/// <summary>
		/// 口座2_銀行支店コード
		/// </summary>
		public int Kaiin_BankShitenCode2
		{
			get	{	return Cast.Int(row == null ? null : row[FKaiin_BankShitenCode2]);	}
			set	{	_set(FKaiin_BankShitenCode2, value);	}
		}
		
		/// <summary>
		/// 口座2_銀行支店コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_BankShitenCode2_Null
		{
			get	{	if (row == null || row[FKaiin_BankShitenCode2] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_BankShitenCode2]); }	}
			set	{	_set(FKaiin_BankShitenCode2, value);	}
		}
		
		/// <summary>
		/// フィールド[口座区分 0/None/ 1/Futsu/普通 2/Touza/当座]。
		/// </summary>
		public const string FKaiin_BankKozaType2 = "Kaiin_BankKozaType2";
		/// <summary>
		/// 口座区分 0/None/ 1/Futsu/普通 2/Touza/当座
		/// </summary>
		public eTypeKoza Kaiin_BankKozaType2
		{
			get	{	return (eTypeKoza)Cast.Int(row == null ? null : row[FKaiin_BankKozaType2]);	}
			set	{	_set(FKaiin_BankKozaType2, (int)value);	}
		}
		
		/// <summary>
		/// 口座区分 0/None/ 1/Futsu/普通 2/Touza/当座。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_BankKozaType2_Null
		{
			get	{	if (row == null || row[FKaiin_BankKozaType2] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_BankKozaType2]); }	}
			set	{	_set(FKaiin_BankKozaType2, value);	}
		}
		
		/// <summary>
		/// フィールド[口座2_口座番号]。
		/// </summary>
		public const string FKaiin_BankKozaNo2 = "Kaiin_BankKozaNo2";
		/// <summary>
		/// 口座2_口座番号
		/// </summary>
		public string Kaiin_BankKozaNo2
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_BankKozaNo2]);	}
			set	{	_set(FKaiin_BankKozaNo2, value);	}
		}
		
		/// <summary>
		/// 口座2_口座番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_BankKozaNo2_Null
		{
			get	{	if (row == null || row[FKaiin_BankKozaNo2] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_BankKozaNo2]); }	}
			set	{	_set(FKaiin_BankKozaNo2, value);	}
		}
		
		/// <summary>
		/// フィールド[口座2_口座名義]。
		/// </summary>
		public const string FKaiin_BankKozaName2 = "Kaiin_BankKozaName2";
		/// <summary>
		/// 口座2_口座名義
		/// </summary>
		public string Kaiin_BankKozaName2
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_BankKozaName2]);	}
			set	{	_set(FKaiin_BankKozaName2, value);	}
		}
		
		/// <summary>
		/// 口座2_口座名義。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_BankKozaName2_Null
		{
			get	{	if (row == null || row[FKaiin_BankKozaName2] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_BankKozaName2]); }	}
			set	{	_set(FKaiin_BankKozaName2, value);	}
		}
		
		/// <summary>
		/// フィールド[口座3_銀行コード]。
		/// </summary>
		public const string FKaiin_BankCode3 = "Kaiin_BankCode3";
		/// <summary>
		/// 口座3_銀行コード
		/// </summary>
		public int Kaiin_BankCode3
		{
			get	{	return Cast.Int(row == null ? null : row[FKaiin_BankCode3]);	}
			set	{	_set(FKaiin_BankCode3, value);	}
		}
		
		/// <summary>
		/// 口座3_銀行コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_BankCode3_Null
		{
			get	{	if (row == null || row[FKaiin_BankCode3] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_BankCode3]); }	}
			set	{	_set(FKaiin_BankCode3, value);	}
		}
		
		/// <summary>
		/// フィールド[口座3_銀行支店コード]。
		/// </summary>
		public const string FKaiin_BankShitenCode3 = "Kaiin_BankShitenCode3";
		/// <summary>
		/// 口座3_銀行支店コード
		/// </summary>
		public int Kaiin_BankShitenCode3
		{
			get	{	return Cast.Int(row == null ? null : row[FKaiin_BankShitenCode3]);	}
			set	{	_set(FKaiin_BankShitenCode3, value);	}
		}
		
		/// <summary>
		/// 口座3_銀行支店コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_BankShitenCode3_Null
		{
			get	{	if (row == null || row[FKaiin_BankShitenCode3] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_BankShitenCode3]); }	}
			set	{	_set(FKaiin_BankShitenCode3, value);	}
		}
		
		/// <summary>
		/// フィールド[口座区分 0/None/ 1/Futsu/普通 2/Touza/当座]。
		/// </summary>
		public const string FKaiin_BankKozaType3 = "Kaiin_BankKozaType3";
		/// <summary>
		/// 口座区分 0/None/ 1/Futsu/普通 2/Touza/当座
		/// </summary>
		public eTypeKoza Kaiin_BankKozaType3
		{
			get	{	return (eTypeKoza)Cast.Int(row == null ? null : row[FKaiin_BankKozaType3]);	}
			set	{	_set(FKaiin_BankKozaType3, (int)value);	}
		}
		
		/// <summary>
		/// 口座区分 0/None/ 1/Futsu/普通 2/Touza/当座。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_BankKozaType3_Null
		{
			get	{	if (row == null || row[FKaiin_BankKozaType3] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_BankKozaType3]); }	}
			set	{	_set(FKaiin_BankKozaType3, value);	}
		}
		
		/// <summary>
		/// フィールド[口座3_口座番号]。
		/// </summary>
		public const string FKaiin_BankKozaNo3 = "Kaiin_BankKozaNo3";
		/// <summary>
		/// 口座3_口座番号
		/// </summary>
		public string Kaiin_BankKozaNo3
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_BankKozaNo3]);	}
			set	{	_set(FKaiin_BankKozaNo3, value);	}
		}
		
		/// <summary>
		/// 口座3_口座番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_BankKozaNo3_Null
		{
			get	{	if (row == null || row[FKaiin_BankKozaNo3] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_BankKozaNo3]); }	}
			set	{	_set(FKaiin_BankKozaNo3, value);	}
		}
		
		/// <summary>
		/// フィールド[口座3_口座名義]。
		/// </summary>
		public const string FKaiin_BankKozaName3 = "Kaiin_BankKozaName3";
		/// <summary>
		/// 口座3_口座名義
		/// </summary>
		public string Kaiin_BankKozaName3
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_BankKozaName3]);	}
			set	{	_set(FKaiin_BankKozaName3, value);	}
		}
		
		/// <summary>
		/// 口座3_口座名義。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_BankKozaName3_Null
		{
			get	{	if (row == null || row[FKaiin_BankKozaName3] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_BankKozaName3]); }	}
			set	{	_set(FKaiin_BankKozaName3, value);	}
		}
		
		/// <summary>
		/// フィールド[退会年月日]。
		/// </summary>
		public const string FKaiin_DateTaikai = "Kaiin_DateTaikai";
		/// <summary>
		/// 退会年月日
		/// </summary>
		public DateTime Kaiin_DateTaikai
		{
			get	{	return Cast.DateTime(row == null ? null : row[FKaiin_DateTaikai]);	}
			set	{	_set(FKaiin_DateTaikai, value);	}
		}
		
		/// <summary>
		/// 退会年月日。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? Kaiin_DateTaikai_Null
		{
			get	{	if (row == null || row[FKaiin_DateTaikai] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FKaiin_DateTaikai]); }	}
			set	{	_set(FKaiin_DateTaikai, value);	}
		}
		
		/// <summary>
		/// フィールド[退会事由区分 0/None/ 1/Haiki/廃棄・退職 2/Shibo/死亡 3/Etc/その他]。
		/// </summary>
		public const string FKaiin_TypeTaikaiJiyu = "Kaiin_TypeTaikaiJiyu";
		/// <summary>
		/// 退会事由区分 0/None/ 1/Haiki/廃棄・退職 2/Shibo/死亡 3/Etc/その他
		/// </summary>
		public eTypeTaikaiJiyu Kaiin_TypeTaikaiJiyu
		{
			get	{	return (eTypeTaikaiJiyu)Cast.Int(row == null ? null : row[FKaiin_TypeTaikaiJiyu]);	}
			set	{	_set(FKaiin_TypeTaikaiJiyu, (int)value);	}
		}
		
		/// <summary>
		/// 退会事由区分 0/None/ 1/Haiki/廃棄・退職 2/Shibo/死亡 3/Etc/その他。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaiin_TypeTaikaiJiyu_Null
		{
			get	{	if (row == null || row[FKaiin_TypeTaikaiJiyu] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaiin_TypeTaikaiJiyu]); }	}
			set	{	_set(FKaiin_TypeTaikaiJiyu, value);	}
		}
		
		/// <summary>
		/// フィールド[退会事由その他メモ]。
		/// </summary>
		public const string FKaiin_TaikaiEtcmemo = "Kaiin_TaikaiEtcmemo";
		/// <summary>
		/// 退会事由その他メモ
		/// </summary>
		public string Kaiin_TaikaiEtcmemo
		{
			get	{	return Cast.String(row == null ? null : row[FKaiin_TaikaiEtcmemo]);	}
			set	{	_set(FKaiin_TaikaiEtcmemo, value);	}
		}
		
		/// <summary>
		/// 退会事由その他メモ。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaiin_TaikaiEtcmemo_Null
		{
			get	{	if (row == null || row[FKaiin_TaikaiEtcmemo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaiin_TaikaiEtcmemo]); }	}
			set	{	_set(FKaiin_TaikaiEtcmemo, value);	}
		}
		
		/// <summary>
		/// フィールド[文書送付先(施設/自宅)]。
		/// </summary>
		public const string FKaiin_BunshoSofusaki = "Kaiin_BunshoSofusaki";
		/// <summary>
		/// 文書送付先(施設/自宅)
		/// </summary>
		public bool Kaiin_BunshoSofusaki
		{
			get	{	return Cast.Bool(row == null ? null : row[FKaiin_BunshoSofusaki]);	}
			set	{	_set(FKaiin_BunshoSofusaki, value);	}
		}
		
		/// <summary>
		/// フィールド[FAX送付先(施設/自宅)]。
		/// </summary>
		public const string FKaiin_FaxSofusaki = "Kaiin_FaxSofusaki";
		/// <summary>
		/// FAX送付先(施設/自宅)
		/// </summary>
		public bool Kaiin_FaxSofusaki
		{
			get	{	return Cast.Bool(row == null ? null : row[FKaiin_FaxSofusaki]);	}
			set	{	_set(FKaiin_FaxSofusaki, value);	}
		}
		
		/// <summary>
		/// フィールド[月別会費内訳(不要/必要)]。
		/// </summary>
		public const string FKaiin_KaihiUchiwake = "Kaiin_KaihiUchiwake";
		/// <summary>
		/// 月別会費内訳(不要/必要)
		/// </summary>
		public bool Kaiin_KaihiUchiwake
		{
			get	{	return Cast.Bool(row == null ? null : row[FKaiin_KaihiUchiwake]);	}
			set	{	_set(FKaiin_KaihiUchiwake, value);	}
		}
		
		/// <summary>
		/// フィールド[公開区分(公開/非公開)]。
		/// </summary>
		public const string FKaiin_KoukaiKbn = "Kaiin_KoukaiKbn";
		/// <summary>
		/// 公開区分(公開/非公開)
		/// </summary>
		public bool Kaiin_KoukaiKbn
		{
			get	{	return Cast.Bool(row == null ? null : row[FKaiin_KoukaiKbn]);	}
			set	{	_set(FKaiin_KoukaiKbn, value);	}
		}
		
		/// <summary>
		/// フィールド[最終更新者ID]。
		/// </summary>
		public const string FLastUpdateUser = "LastUpdateUser";
		/// <summary>
		/// 最終更新者ID
		/// </summary>
		public int LastUpdateUser
		{
			get	{	return Cast.Int(row == null ? null : row[FLastUpdateUser]);	}
			set	{	_set(FLastUpdateUser, value);	}
		}
		
		/// <summary>
		/// 最終更新者ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? LastUpdateUser_Null
		{
			get	{	if (row == null || row[FLastUpdateUser] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FLastUpdateUser]); }	}
			set	{	_set(FLastUpdateUser, value);	}
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
		public t_kaiin(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_kaiin 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_kaiin 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_kaiin");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Kaiin, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FCD_Kaiin, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_NameKana, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_Sex, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_DateBirth, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_Post, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_Addr1, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_Addr2, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_Tel1, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_Tel2, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_Fax, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_Mail1, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_Mail2, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_IsekiTorokuNo, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_DateIsekiToroku, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_ShusshinKou, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_DateSotsugyo, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_ShusshinIn, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_DateShuryo, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_DateShutokuGakui, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_TypeZaiseki, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_DateShonin, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_DateToroku, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_DateNyuukai, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_KaiinKbn, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Kaihi_Nichii, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_NichiiShiharai, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Kaihi_Keni, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_KeniShiharai, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Kaihi_Shii, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_ShiiShiharai, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Ishikai_Idomae, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Shinryoka_Main, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Iryokikan, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_ShisetsuGyomu, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_Shiteii, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankCode1, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankShitenCode1, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankKozaType1, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankKozaNo1, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankKozaName1, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankCode2, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankShitenCode2, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankKozaType2, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankKozaNo2, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankKozaName2, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankCode3, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankShitenCode3, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankKozaType3, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankKozaNo3, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BankKozaName3, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_DateTaikai, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_TypeTaikaiJiyu, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_TaikaiEtcmemo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_BunshoSofusaki, typeof(bool));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_FaxSofusaki, typeof(bool));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_KaihiUchiwake, typeof(bool));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaiin_KoukaiKbn, typeof(bool));
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdateUser, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
