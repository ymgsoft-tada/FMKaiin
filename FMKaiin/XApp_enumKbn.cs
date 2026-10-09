
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
	/// [列挙] 診療所区分
	/// </summary>
	public enum eTypeShinryojo
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 北
		/// </summary>
		North = 1,
		/// <summary>
		/// 南
		/// </summary>
		South = 2,
	}
	
	/// <summary>
	/// [列挙] 職種区分
	/// </summary>
	public enum eTypeJob
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 医師
		/// </summary>
		Dr = 1,
		/// <summary>
		/// 看護師
		/// </summary>
		Nurse = 2,
		/// <summary>
		/// 事務
		/// </summary>
		Jimu = 3,
		/// <summary>
		/// 薬剤師
		/// </summary>
		Yakuzaishi = 4,
	}
	
	/// <summary>
	/// [列挙] 性別
	/// </summary>
	public enum eSex
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 男性
		/// </summary>
		Men = 1,
		/// <summary>
		/// 女性
		/// </summary>
		Women = 2,
	}
	
	/// <summary>
	/// [列挙] 端数処理
	/// </summary>
	public enum eHasu
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 切捨
		/// </summary>
		Kirisute = 1,
		/// <summary>
		/// 切上
		/// </summary>
		Kiriage = 2,
		/// <summary>
		/// 四捨五入
		/// </summary>
		Shishagonyu = 3,
	}
	
	/// <summary>
	/// [列挙] 口座区分
	/// </summary>
	public enum eTypeKoza
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 普通
		/// </summary>
		Futsu = 1,
		/// <summary>
		/// 当座
		/// </summary>
		Touza = 2,
	}
	
	/// <summary>
	/// [列挙] 権限区分
	/// </summary>
	public enum eAuth
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// SuperUser
		/// </summary>
		SU = 1,
		/// <summary>
		/// 管理者
		/// </summary>
		Admin = 2,
		/// <summary>
		/// 一般
		/// </summary>
		Ippan = 3,
	}
	
	/// <summary>
	/// [列挙] 作成区分
	/// </summary>
	public enum eCreateType
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// インポート
		/// </summary>
		Import = 1,
	}
	
	/// <summary>
	/// [列挙] 在籍区分
	/// </summary>
	public enum eTypeZaiseki
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 在籍
		/// </summary>
		Zaiseki = 1,
		/// <summary>
		/// 異動
		/// </summary>
		Ido = 2,
		/// <summary>
		/// 退会
		/// </summary>
		Taikai = 3,
	}
	
	/// <summary>
	/// [列挙] 支払方法
	/// </summary>
	public enum eShiharai
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 口座①
		/// </summary>
		Koza1 = 1,
		/// <summary>
		/// 口座②
		/// </summary>
		Koza2 = 2,
		/// <summary>
		/// 口座③
		/// </summary>
		Koza3 = 3,
		/// <summary>
		/// 現金
		/// </summary>
		Genkin = 9,
	}
	
	/// <summary>
	/// [列挙] 異動事由区分
	/// </summary>
	public enum eTypeIdoJiyu
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 施設開業
		/// </summary>
		Kaigyo = 1,
		/// <summary>
		/// 施設異動
		/// </summary>
		Ido = 2,
		/// <summary>
		/// 会員区分変更
		/// </summary>
		Kaiin = 3,
		/// <summary>
		/// その他
		/// </summary>
		Etc = 4,
	}
	
	/// <summary>
	/// [列挙] 退会事由区分
	/// </summary>
	public enum eTypeTaikaiJiyu
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 廃棄・退職
		/// </summary>
		Haiki = 1,
		/// <summary>
		/// 死亡
		/// </summary>
		Shibo = 2,
		/// <summary>
		/// その他
		/// </summary>
		Etc = 3,
	}
	
	/// <summary>
	/// [列挙] 施設異動詳細
	/// </summary>
	public enum eIdoShisetsu
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 勤務先
		/// </summary>
		KinmuSaki = 1,
		/// <summary>
		/// 休養
		/// </summary>
		Kyuyo = 2,
		/// <summary>
		/// 廃業
		/// </summary>
		Haigyo = 3,
		/// <summary>
		/// 退職
		/// </summary>
		Taisyoku = 4,
	}
	
	/// <summary>
	/// [列挙] 会員区分変更詳細
	/// </summary>
	public enum eIdoKaiinHenko
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 開業
		/// </summary>
		Kaigyo = 1,
		/// <summary>
		/// 管理者交代
		/// </summary>
		KanriHenko = 2,
		/// <summary>
		/// 開設者交代
		/// </summary>
		KaisetsuHenko = 3,
		/// <summary>
		/// 開設者・管理者交代
		/// </summary>
		KanriKaisetsuHenko = 4,
		/// <summary>
		/// 廃業
		/// </summary>
		Haigyo = 5,
	}
	
	/// <summary>
	/// [列挙] 異動その他詳細
	/// </summary>
	public enum eIdoEtc
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 移転
		/// </summary>
		Iten = 1,
		/// <summary>
		/// 名称変更
		/// </summary>
		MeisyoHenko = 2,
		/// <summary>
		/// 法人化
		/// </summary>
		Hojinka = 3,
		/// <summary>
		/// 自宅住所変更
		/// </summary>
		AddrHenko = 4,
	}
	
	/// <summary>
	/// [列挙] 会費区分
	/// </summary>
	public enum eTypeKaihi
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 医師会会費
		/// </summary>
		Ishikai = 1,
		/// <summary>
		/// 医会等会費
		/// </summary>
		Ikai = 2,
	}
	
	/// <summary>
	/// [列挙] 医会区分
	/// </summary>
	public enum eTypeIkai
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 日医
		/// </summary>
		Nichii = 1,
		/// <summary>
		/// 県医
		/// </summary>
		Keni = 2,
		/// <summary>
		/// 市医
		/// </summary>
		Shii = 3,
		/// <summary>
		/// その他
		/// </summary>
		Etc = 4,
	}
	
	/// <summary>
	/// [作成者 fj]
	/// テーブル編集の際に使うクラスです。
	/// </summary>
	public static class enumKbn
	{
		/// <summary>
		/// eTypeShinryojo に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DTypeShinryojo;
		/// <summary>
		/// eTypeJob に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DTypeJob;
		/// <summary>
		/// eSex に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DSex;
		/// <summary>
		/// eHasu に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DHasu;
		/// <summary>
		/// eTypeKoza に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DTypeKoza;
		/// <summary>
		/// eAuth に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DAuth;
		/// <summary>
		/// eCreateType に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DCreateType;
		/// <summary>
		/// eTypeZaiseki に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DTypeZaiseki;
		/// <summary>
		/// eShiharai に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DShiharai;
		/// <summary>
		/// eTypeIdoJiyu に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DTypeIdoJiyu;
		/// <summary>
		/// eTypeTaikaiJiyu に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DTypeTaikaiJiyu;
		/// <summary>
		/// eIdoShisetsu に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DIdoShisetsu;
		/// <summary>
		/// eIdoKaiinHenko に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DIdoKaiinHenko;
		/// <summary>
		/// eIdoEtc に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DIdoEtc;
		/// <summary>
		/// eTypeKaihi に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DTypeKaihi;
		/// <summary>
		/// eTypeIkai に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DTypeIkai;
		
		/// <summary>
		/// 列挙辞書を初期化します。
		/// </summary>
		public static void InitEnumDictionary()
		{
			DTypeShinryojo = new Dictionary<int, string>();
			DTypeShinryojo.Add((int)eTypeShinryojo.None, "");
			DTypeShinryojo.Add((int)eTypeShinryojo.North, "北");
			DTypeShinryojo.Add((int)eTypeShinryojo.South, "南");
			
			DTypeJob = new Dictionary<int, string>();
			DTypeJob.Add((int)eTypeJob.None, "");
			DTypeJob.Add((int)eTypeJob.Dr, "医師");
			DTypeJob.Add((int)eTypeJob.Nurse, "看護師");
			DTypeJob.Add((int)eTypeJob.Jimu, "事務");
			DTypeJob.Add((int)eTypeJob.Yakuzaishi, "薬剤師");
			
			DSex = new Dictionary<int, string>();
			DSex.Add((int)eSex.None, "");
			DSex.Add((int)eSex.Men, "男性");
			DSex.Add((int)eSex.Women, "女性");
			
			DHasu = new Dictionary<int, string>();
			DHasu.Add((int)eHasu.None, "");
			DHasu.Add((int)eHasu.Kirisute, "切捨");
			DHasu.Add((int)eHasu.Kiriage, "切上");
			DHasu.Add((int)eHasu.Shishagonyu, "四捨五入");
			
			DTypeKoza = new Dictionary<int, string>();
			DTypeKoza.Add((int)eTypeKoza.None, "");
			DTypeKoza.Add((int)eTypeKoza.Futsu, "普通");
			DTypeKoza.Add((int)eTypeKoza.Touza, "当座");
			
			DAuth = new Dictionary<int, string>();
			DAuth.Add((int)eAuth.None, "");
			DAuth.Add((int)eAuth.SU, "SuperUser");
			DAuth.Add((int)eAuth.Admin, "管理者");
			DAuth.Add((int)eAuth.Ippan, "一般");
			
			DCreateType = new Dictionary<int, string>();
			DCreateType.Add((int)eCreateType.None, "");
			DCreateType.Add((int)eCreateType.Import, "インポート");
			
			DTypeZaiseki = new Dictionary<int, string>();
			DTypeZaiseki.Add((int)eTypeZaiseki.None, "");
			DTypeZaiseki.Add((int)eTypeZaiseki.Zaiseki, "在籍");
			DTypeZaiseki.Add((int)eTypeZaiseki.Ido, "異動");
			DTypeZaiseki.Add((int)eTypeZaiseki.Taikai, "退会");
			
			DShiharai = new Dictionary<int, string>();
			DShiharai.Add((int)eShiharai.None, "");
			DShiharai.Add((int)eShiharai.Koza1, "口座①");
			DShiharai.Add((int)eShiharai.Koza2, "口座②");
			DShiharai.Add((int)eShiharai.Koza3, "口座③");
			DShiharai.Add((int)eShiharai.Genkin, "現金");
			
			DTypeIdoJiyu = new Dictionary<int, string>();
			DTypeIdoJiyu.Add((int)eTypeIdoJiyu.None, "");
			DTypeIdoJiyu.Add((int)eTypeIdoJiyu.Kaigyo, "施設開業");
			DTypeIdoJiyu.Add((int)eTypeIdoJiyu.Ido, "施設異動");
			DTypeIdoJiyu.Add((int)eTypeIdoJiyu.Kaiin, "会員区分変更");
			DTypeIdoJiyu.Add((int)eTypeIdoJiyu.Etc, "その他");
			
			DTypeTaikaiJiyu = new Dictionary<int, string>();
			DTypeTaikaiJiyu.Add((int)eTypeTaikaiJiyu.None, "");
			DTypeTaikaiJiyu.Add((int)eTypeTaikaiJiyu.Haiki, "廃棄・退職");
			DTypeTaikaiJiyu.Add((int)eTypeTaikaiJiyu.Shibo, "死亡");
			DTypeTaikaiJiyu.Add((int)eTypeTaikaiJiyu.Etc, "その他");
			
			DIdoShisetsu = new Dictionary<int, string>();
			DIdoShisetsu.Add((int)eIdoShisetsu.None, "");
			DIdoShisetsu.Add((int)eIdoShisetsu.KinmuSaki, "勤務先");
			DIdoShisetsu.Add((int)eIdoShisetsu.Kyuyo, "休養");
			DIdoShisetsu.Add((int)eIdoShisetsu.Haigyo, "廃業");
			DIdoShisetsu.Add((int)eIdoShisetsu.Taisyoku, "退職");
			
			DIdoKaiinHenko = new Dictionary<int, string>();
			DIdoKaiinHenko.Add((int)eIdoKaiinHenko.None, "");
			DIdoKaiinHenko.Add((int)eIdoKaiinHenko.Kaigyo, "開業");
			DIdoKaiinHenko.Add((int)eIdoKaiinHenko.KanriHenko, "管理者交代");
			DIdoKaiinHenko.Add((int)eIdoKaiinHenko.KaisetsuHenko, "開設者交代");
			DIdoKaiinHenko.Add((int)eIdoKaiinHenko.KanriKaisetsuHenko, "開設者・管理者交代");
			DIdoKaiinHenko.Add((int)eIdoKaiinHenko.Haigyo, "廃業");
			
			DIdoEtc = new Dictionary<int, string>();
			DIdoEtc.Add((int)eIdoEtc.None, "");
			DIdoEtc.Add((int)eIdoEtc.Iten, "移転");
			DIdoEtc.Add((int)eIdoEtc.MeisyoHenko, "名称変更");
			DIdoEtc.Add((int)eIdoEtc.Hojinka, "法人化");
			DIdoEtc.Add((int)eIdoEtc.AddrHenko, "自宅住所変更");
			
			DTypeKaihi = new Dictionary<int, string>();
			DTypeKaihi.Add((int)eTypeKaihi.None, "");
			DTypeKaihi.Add((int)eTypeKaihi.Ishikai, "医師会会費");
			DTypeKaihi.Add((int)eTypeKaihi.Ikai, "医会等会費");
			
			DTypeIkai = new Dictionary<int, string>();
			DTypeIkai.Add((int)eTypeIkai.None, "");
			DTypeIkai.Add((int)eTypeIkai.Nichii, "日医");
			DTypeIkai.Add((int)eTypeIkai.Keni, "県医");
			DTypeIkai.Add((int)eTypeIkai.Shii, "市医");
			DTypeIkai.Add((int)eTypeIkai.Etc, "その他");
		}
	}
}
