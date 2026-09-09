using ComponentDB;
using ComponentFile;
using ComponentIO;
using ComponentRegistry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
	/// <summary>
	/// グローバル値の管理
	/// tachi
	/// </summary>
	public class AppGlobal
	{
		/// <summary>DB</summary>
		public static AppDb DB { get; private set; }

		/// <summary>基本情報</summary>
		public static t_basic Basic { get; private set; }
		/// <summary>スタッフ情報</summary>
		public static AppStaff Staffs { get; private set; }
		/// <summary>銀行情報</summary>
		public static AppBank Banks { get; private set; } // Banksが必要かどうか
		/// <summary>担当者情報</summary>
		public static AppTanto Tantos { get; private set; }

		/// <summary>医療機関情報</summary>
		public static AppIryoKikan IryoKikans { get; private set; }
		/// <summary>医会会費情報</summary>
		public static AppKaihi Kaihis { get; private set; }

		/// <summary>診療科情報</summary>
		public static AppShinryoka Shinryokas { get; private set; }
		/// <summary>学会情報</summary>
		public static AppGakkai Gakkais { get; private set; }
		/// <summary>学校情報</summary>
		public static AppGakko Gakkos { get; private set; }
		/// <summary>施設・業務情報</summary>
		public static AppShisetsugyomu Shisetsugyomus { get; private set; } // enum持ちかも
		/// <summary>開設主体情報</summary>
		public static AppKaisetsushutai Kaisetsushutais{ get; private set; } // enum持ちかも

		/// <summary>ログインユーザー</summary>
		public static Tanto LoginUser { get; private set; }

		/// <summary>
		/// 銀行コード管理クラス
		/// </summary>
		public static BankCodeManager BankCodeMg { get; private set; }

		/// <summary>
		/// 全初期化
		/// </summary>
		public static void Init()
		{
			enumKbn.InitEnumDictionary();
			AppDbRule.Initialize();

			RegCommon.SetMasterKey(AppConst.RegKey);

			InitBasic();
			InitStaff();
			InitTanto();
			InitIryoKikan();
			InitKaihi();
			InitShinryoka();
			InitGakkai();
			InitGakko();
			InitShisetsugyomu();
			InitKaisetsusyutai();
		}

		/// <summary>
		/// 基本情報の初期化
		/// </summary>
		public static void InitBasic()
		{
			DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_basic));

			Basic = new t_basic(dv[0]);

//			Banks = new AppBank(); // 基本情報に持たないなら全初期化()でInitとしたい,Banks不要なら削除
//			Banks.Init();
//			BasicBank = Banks.Get(Basic.ID_Bank);

			//和暦表示
			AppDate.SetDispSeireki(false);
		}

		/// <summary>
		/// スタッフ情報の初期化
		/// </summary>
		public static void InitStaff()
		{
			Staffs = new AppStaff();
			Staffs.Init();
		}

		/// <summary>
		/// 職務の初期化
		/// </summary>
		public static void InitTanto()
		{
			t_tantosha xrow = null;
			if (LoginUser != null)
			{
				xrow = LoginUser.XRow;
			}

			Tantos = new AppTanto();
			Tantos.Init();

			// ログインユーザー情報の更新
			if (xrow != null)
			{
				SetLoginUser(xrow);
			}
		}

		/// <summary>
		/// 銀行コード管理クラスの初期化
		/// </summary>
		public static void InitBankCodeMg()
		{
			BankCodeMg = new BankCodeManager();
			BankCodeMg.Init();
		}

		/// <summary>
		/// 医療機関情報の初期化
		/// </summary>
		public static void InitIryoKikan()
		{
			IryoKikans = new AppIryoKikan();
			IryoKikans.Init();
		}

		/// <summary>
		/// 医会会費情報の初期化
		/// </summary>
		public static void InitKaihi()
		{
			Kaihis = new AppKaihi();
			Kaihis.Init();
		}

		/// <summary>
		/// 診療科情報の初期化
		/// </summary>
		public static void InitShinryoka()
		{
			Shinryokas = new AppShinryoka();
			Shinryokas.Init();
		}

		/// <summary>
		/// 学会情報の初期化
		/// </summary>
		public static void InitGakkai()
		{
			Gakkais = new AppGakkai();
			Gakkais.Init();
		}

		/// <summary>
		/// 学校情報の初期化
		/// </summary>
		public static void InitGakko()
		{
			Gakkos = new AppGakko();
			Gakkos.Init();
		}

		/// <summary>
		/// 施設・業務情報の初期化
		/// </summary>
		public static void InitShisetsugyomu()
		{
			Shisetsugyomus = new AppShisetsugyomu();
			Shisetsugyomus.Init();
		}

		/// <summary>
		/// 開設主体情報の初期化
		/// </summary>
		public static void InitKaisetsusyutai()
		{
			Kaisetsushutais = new AppKaisetsushutai();
			Kaisetsushutais.Init();
		}
		//----

		/// <summary>
		/// DBの初期化処理
		/// </summary>
		public static bool InitDB()
		{
			if (FileIO.Exists(AppConst.DBPath) == false)
			{
				FileIO.Copy(AppConst.SystemDBPath, AppConst.DBPath);
			}

			AppGlobal.DB = new AppDb();

			return AppGlobal.DB.Init();
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		public static void Finish()
		{
			if (AppGlobal.DB != null)
			{
				AppGlobal.DB.Finish();
			}
		}

		/// <summary>
		/// ログインユーザーを設定します。
		/// </summary>
		/// <param name="trow"></param>
		public static void SetLoginUser(t_tantosha trow)
		{
			SetLoginUser(trow.ID_Tanto);
		}

		/// <summary>
		/// ログインユーザーを設定します。
		/// </summary>
		/// <param name="id"></param>
		public static void SetLoginUser(int id)
		{
			LoginUser = Tantos.Get(id);
		}

		/// <summary>
		/// 金額の小数点以下の端数処理をおこないます。
		/// </summary>
		/// <param name="cost">金額</param>
		/// <param name="hasu">端数処理</param>
		/// <param name="n">端数処理をする小数位</param>
		/// <returns></returns>
		public static decimal Round(decimal cost, eHasu hasu, int n = 0)
		{
			double pow = Math.Pow(10, n);
			double dob = (double)cost * pow;

			switch(hasu)
			{
				case eHasu.Kiriage :
					dob = Math.Ceiling(dob)/pow;
					break;
				case eHasu.Kirisute :
					dob = Math.Truncate(dob)/pow;
					break;
				case eHasu.Shishagonyu :
					dob = Math.Round(dob,MidpointRounding.AwayFromZero)/pow;
					break;
				default :
					break;
			}

			if (dob - Math.Truncate(dob) == 0)
			{
				// 「#.0」と言った表示はさせない。
				dob = Math.Truncate(dob);
			}

			return Cast.Decimal(dob);
		}
	}
}
