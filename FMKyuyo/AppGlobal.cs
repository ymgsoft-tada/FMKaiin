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
	/// [作成者 kj]
	/// グローバル値の管理
	/// </summary>
	public class AppGlobal
	{
		/// <summary>DB</summary>
		public static AppDb DB { get; private set; }

		/// <summary>基本情報</summary>
		public static t_basic Basic { get; private set; }

		/// <summary>
		/// 全初期化
		/// </summary>
		public static void Init()
		{
			enumKbn.InitEnumDictionary();
			AppDbRule.Initialize();

			RegCommon.SetMasterKey(AppConst.RegKey);

//			ComponentGGridDB.GGridDBCommon.GridSortVectorUp		= Properties.Resources.GridSortVecUp;
//			ComponentGGridDB.GGridDBCommon.GridSortVectorDown	= Properties.Resources.GridSortVecDown;

//			InitBankCodeMg();

			InitBasic();
			
		}

		/// <summary>
		/// 基本情報の初期化
		/// </summary>
		public static void InitBasic()
		{
			DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_basic));

			Basic = new t_basic(dv[0]);

			//// 西暦表示固定
			//AppDate.SetDispSeireki(true);
			//¶2023/12/31 和暦表示
			AppDate.SetDispSeireki(false);
		}


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

		///// <summary>
		///// ログインユーザーを設定します。
		///// </summary>
		///// <param name="trow"></param>
		//public static void SetLoginUser(t_tantosha trow)
		//{
		//	SetLoginUser(trow.ID_Tanto);
		//}

		///// <summary>
		///// ログインユーザーを設定します。
		///// </summary>
		///// <param name="id"></param>
		//public static void SetLoginUser(int id)
		//{
		//	LoginUser = Tantos.Get(id);
		//}

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
