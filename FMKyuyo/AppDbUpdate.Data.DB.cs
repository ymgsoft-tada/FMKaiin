using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComponentDB;
using System.Diagnostics;
using System.Data;
using ComponentForm;
using ComponentIO;
using ComponentDebug;

namespace App
{
	/// <summary>
	/// [作成者 kj]
	/// Data.DB アップデートの実行メソッド履歴リスト。
	/// </summary>
	public partial class AppDbUpdate
	{
		#region *** Public Value ***
		/// <summary>
		/// ユーザーDB 更新履歴。
		/// </summary>
		public static AppDbUpdateHistory[]		HistoryDataDB =
		{
																	//■ 参考例
			//new AppDbUpdateHistory("0.51.00", DataDB_05100),		// Field追加。

		};
		#endregion	

		#region *** Public Method ***
		/// <summary>
		/// Data.DBのバージョンを更新します。
		/// </summary>
		public static void UpdateDataDB(object sender, DoWorkEventArgsEx e)
		{
			// 更新するリストはData.DBだけにする。
			History = HistoryDataDB;

			// アップデート実行
			AppDbUpdateManager.UpdateVersion(sender, e);
		}
		#endregion

		//#region >>> 0.8.0
		///// <summary>
		///// 0.8.0更新メソッド。
		///// </summary>
		//static bool DataDB_0080(DB moddb, DB seedb)
		//{
		//	DAO.Database db = null;
		//	DAO.DBEngine de = new DAO.DBEngine();
		//	DAO.Workspace ws = de.Workspaces[0];

		//	try
		//	{
		//		string sql;

		//		// DBLocation は mdb へのパス
		//		db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.AppDBPassword);

		//		sql = DBQuery.GetSql("ALTER TABLE {0} Add Column {1} DATETIME, {2} LONG", TableProp.t_loan, t_loan.FDateYMUpdateKyuyo, t_loan.FLoanState);
		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.t_loan].Fields.Refresh();

		//		db.Close();
		//		db = null;


		//		DBView dv = new DBView(moddb.AddAdapter(TableProp.t_loan));

		//		for (int i = 0; i < dv.Count; i++)
		//		{
		//			t_loan xrow = new t_loan(dv[i]);

		//			if (xrow.CostHensai >= xrow.CostKariire)
		//			{
		//				xrow.LoanState = eLoanState.Kansai;
		//			}
		//			else
		//			{
		//				xrow.LoanState = eLoanState.Hensaichu;
		//			}
		//		}

		//		if (dv.HasChanges() == true)
		//		{
		//			dv.Update();
		//		}

		//		moddb.DelAdapter(dv.DBAdapter);
		//	}
		//	catch (Exception ex)
		//	{
		//		if (db != null)
		//		{
		//			db.Close();
		//			db = null;
		//		}

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 0.8.1
		///// <summary>
		///// 0.8.1更新メソッド。
		///// </summary>
		//static bool DataDB_0081(DB moddb, DB seedb)
		//{
		//	DAO.Database db = null;
		//	DAO.DBEngine de = new DAO.DBEngine();
		//	DAO.Workspace ws = de.Workspaces[0];

		//	try
		//	{
		//		string sql;

		//		// DBLocation は mdb へのパス
		//		db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.AppDBPassword);

		//		// アルバイトの雇用保険対象用フラグを追加
		//		// t_kyuyo_base
		//		sql = DBQuery.GetSql("ALTER TABLE {0} Add Column {1} YESNO", TableProp.t_kyuyo_base, t_kyuyo_base.FCalcZeigaku);
		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.t_kyuyo_base].Fields.Refresh();

		//		db.Close();
		//		db = null;
		//	}
		//	catch (Exception ex)
		//	{
		//		if (db != null)
		//		{
		//			db.Close();
		//			db = null;
		//		}

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 0.8.2
		///// <summary>
		///// 0.8.2更新メソッド。
		///// </summary>
		//static bool DataDB_0082(DB moddb, DB seedb)
		//{
		//	DAO.Database db = null;
		//	DAO.DBEngine de = new DAO.DBEngine();
		//	DAO.Workspace ws = de.Workspaces[0];

		//	try
		//	{
		//		DBView dvShop = new DBView(moddb.AddAdapter(TableProp.t_shop));
		//		DBView dvPosFind = new DBView(moddb.AddAdapter(TableProp.t_position));
		//		DBView dvKi = new DBView(moddb.AddAdapter(TableProp.t_kyuyo_input));
		//		DBView dvKm = new DBView(moddb.AddAdapter(TableProp.t_kyuyo_meisai));

		//		t_shop shop_honbu = null;

		//		dvShop.RowFilterQuery("{0} = {1}", t_shop.FShp_ShopKbn, (int)eShopKbn.Honbu);

		//		if (dvShop.Count > 0)
		//		{
		//			shop_honbu = new t_shop(dvShop[0]);
		//		}

		//		dvPosFind.SortQuery(t_position.FID_Position);

		//		// 給与入力の修正
		//		for (int i = 0; i < dvKi.Count; i++)
		//		{
		//			t_kyuyo_input ki = new t_kyuyo_input(dvKi[i]);

		//			if (dvPosFind.FindRow(ki.ID_Position) == true)
		//			{
		//				t_position pos = new t_position(dvPosFind.CurrentRow);

		//				// 派遣は所属を本部とする。
		//				if (AppPosition.IsHaken(pos) == true && ki.ID_Shop != shop_honbu.ID_Shop)
		//				{
		//					ki.ID_Shop_Hakensaki	= ki.ID_Shop;
		//					if (shop_honbu != null)
		//					{
		//						ki.ID_Shop			= shop_honbu.ID_Shop;
		//					}
		//				}
		//			}
		//		}

		//		// 給与明細の修正
		//		for (int i = 0; i < dvKm.Count; i++)
		//		{
		//			t_kyuyo_meisai km = new t_kyuyo_meisai(dvKm[i]);

		//			if (dvPosFind.FindRow(km.ID_Position) == true)
		//			{
		//				t_position pos = new t_position(dvPosFind.CurrentRow);

		//				// 派遣は所属を本部とする。
		//				if (AppPosition.IsHaken(pos) == true && km.ID_Shop != shop_honbu.ID_Shop)
		//				{
		//					km.ID_Shop_Hakensaki	= km.ID_Shop;
		//					if (shop_honbu != null)
		//					{
		//						km.ID_Shop			= shop_honbu.ID_Shop;
		//					}
		//				}
		//			}
		//		}

		//		if (dvKi.HasChanges() == true)
		//		{
		//			dvKi.Update();
		//		}
		//		if (dvKm.HasChanges() == true)
		//		{
		//			dvKm.Update();
		//		}

		//		moddb.DelAdapter(dvShop.DBAdapter);
		//		moddb.DelAdapter(dvPosFind.DBAdapter);
		//		moddb.DelAdapter(dvKi.DBAdapter);
		//		moddb.DelAdapter(dvKm.DBAdapter);

		//	}
		//	catch (Exception ex)
		//	{
		//		if (db != null)
		//		{
		//			db.Close();
		//			db = null;
		//		}

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 0.8.3
		///// <summary>
		///// 0.8.3更新メソッド。
		///// </summary>
		//static bool DataDB_0083(DB moddb, DB seedb)
		//{
		//	DAO.Database db = null;
		//	DAO.DBEngine de = new DAO.DBEngine();
		//	DAO.Workspace ws = de.Workspaces[0];

		//	try
		//	{
		//		DBView dvStuff		= new DBView(moddb.AddAdapter(TableProp.t_stuff));
		//		DBView dvBaseDef	= new DBView(moddb.AddAdapter(TableProp.t_kyuyo_base_define));
		//		DBView dvBase		= new DBView(moddb.AddAdapter(TableProp.t_kyuyo_base));
		//		DBView dvPos		= new DBView(moddb.AddAdapter(TableProp.t_position));

		//		dvStuff.SortQuery(t_stuff.FID_Stuff);
		//		dvPos.SortQuery(t_position.FID_Position);

		//		//■ 基礎データの積立保証金項目を全ての区分でオンとする。
		//		for (int i = 0; i < dvBaseDef.Count; i++)
		//		{
		//			t_kyuyo_base_define xrow = new t_kyuyo_base_define(dvBaseDef[i]);

		//			xrow.ChkCostTsumitateHosho = true;
		//		}

		//		if (dvBaseDef.HasChanges() == true)
		//		{
		//			dvBaseDef.Update();
		//		}

		//		//■スタッフ別の基礎データについては積立金の初期値を2000円とする
		//		for (int i = 0; i < dvBase.Count; i++)
		//		{
		//			t_kyuyo_base xrow = new t_kyuyo_base(dvBase[i]);

		//			t_stuff srow = null;
		//			if (dvStuff.FindRow(xrow.ID_Stuff) == true)
		//			{
		//				srow = new t_stuff(dvStuff.CurrentRow);
		//			}

		//			if (dvPos.FindRow(xrow.ID_Position) == true)
		//			{
		//				t_position prow = new t_position(dvPos.CurrentRow);

		//				if (prow.Pos_Position == ePosition.Tenant2			||
		//					prow.Pos_Position == ePosition.Gijutsusha		||
		//					prow.Pos_Position == ePosition.ASF				||
		//					prow.Pos_Position == ePosition.HakenAS			||
		//					prow.Pos_Position == ePosition.HakenGijutsusha	||
		//					prow.Pos_Position == ePosition.HakenSenzokuAS	||
		//					prow.Pos_Position == ePosition.HakenSenzokuGijutsusha)
		//				{
		//					if (xrow.CostTsumitateHosho != 0)
		//					{
		//						// 元々値が入ってるものは無視
		//					}
		//					else
		//					{
		//						int as_cnt = 1;

		//						if (xrow.ID_StuffAS1 != 0) as_cnt++;
		//						if (xrow.ID_StuffAS2 != 0) as_cnt++;
		//						if (xrow.ID_StuffAS3 != 0) as_cnt++;
		//						if (xrow.ID_StuffAS4 != 0) as_cnt++;
		//						if (xrow.ID_StuffAS5 != 0) as_cnt++;

		//						xrow.CostTsumitateHosho = AppKyuyo.TankaTsumitate * as_cnt;
		//					}
		//				}
		//			}
		//		}

		//		if (dvBase.HasChanges() == true)
		//		{
		//			dvBase.Update();
		//		}

		//		moddb.DelAdapter(dvPos.DBAdapter);
		//		moddb.DelAdapter(dvBase.DBAdapter);
		//		moddb.DelAdapter(dvBaseDef.DBAdapter);
		//		moddb.DelAdapter(dvStuff.DBAdapter);

		//	}
		//	catch (Exception ex)
		//	{
		//		if (db != null)
		//		{
		//			db.Close();
		//			db = null;
		//		}

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 0.51.00
		/////// <summary>
		/////// 0.51.00更新メソッド。
		/////// </summary>
		////static bool DataDB_05100(DB moddb, DB seedb)
		////{
		////    DAO.Database	db = null;
		////    DAO.DBEngine	de = new DAO.DBEngine();
		////    DAO.Workspace	ws = de.Workspaces[0];

		////    try
		////    {								
		////        string			sql;

		////        // DBLocation は mdb へのパス
		////        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		////        //■ T_Userに消費税課税区分を追加
		////        sql = DBQuery.GetSql("ALTER TABLE {0} Add Column {1} LONG", TableProp.T_User, T_User.FUsr_KbnShohizeiKazei);
		////        db.Execute(sql, null);
		////        db.TableDefs[TableProp.T_User].Fields.Refresh();

		////        //■ X_ChintaiJokyoに解約日を追加
		////        sql = DBQuery.GetSql("ALTER TABLE {0} Add Column {1} TEXT(20)", TableProp.X_ChintaiJokyo, X_ChintaiJokyo.FKeiyaku_DateKaiyaku);
		////        db.Execute(sql, null);
		////        db.TableDefs[TableProp.X_ChintaiJokyo].Fields.Refresh();

		////        // 空文字列許可を「はい」に
		////        db.TableDefs[TableProp.X_ChintaiJokyo].Fields[X_ChintaiJokyo.FKeiyaku_DateKaiyaku].AllowZeroLength = true;
		////        db.TableDefs[TableProp.X_ChintaiJokyo].Fields.Refresh();

		////        //■ T_ShunyuUchiwakeに手入力を追加
		////        sql = DBQuery.GetSql("ALTER TABLE {0} Add Column {1} YESNO", TableProp.T_ShunyuUchiwake, T_ShunyuUchiwake.FShuUchi_ManualInput);
		////        db.Execute(sql, null);
		////        db.TableDefs[TableProp.T_ShunyuUchiwake].Fields.Refresh();				

		////        db.Close();
		////        db = null;
		////    }
		////    catch(Exception ex)
		////    {
		////        if (db != null)
		////        {
		////            db.Close();
		////            db = null;
		////        }

		////        _write_err(ex);

		////        return false;
		////    }

		////    return true;
		////}
		//#endregion

		//#region >>> 0.51.01
		/////// <summary>
		/////// 0.51.01更新メソッド。
		/////// </summary>
		////static bool DataDB_05101(DB moddb, DB seedb)
		////{
		////    DAO.Database	db = null;
		////    DAO.DBEngine	de = new DAO.DBEngine();
		////    DAO.Workspace	ws = de.Workspaces[0];

		////    try
		////    {								
		////        string			sql;

		////        // DBLocation は mdb へのパス
		////        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		////        //■ T_ShunyuUchiwakeに年度IDを追加
		////        sql = DBQuery.GetSql("ALTER TABLE {0} Add Column {1} LONG", TableProp.T_ShunyuUchiwake, T_ShunyuUchiwake.FID_UserNendo);
		////        db.Execute(sql, null);
		////        db.TableDefs[TableProp.T_ShunyuUchiwake].Fields.Refresh();

		////        db.Close();
		////        db = null;
		////    }
		////    catch(Exception ex)
		////    {
		////        if (db != null)
		////        {
		////            db.Close();
		////            db = null;
		////        }

		////        _write_err(ex);

		////        return false;
		////    }

		////    return true;
		////}
		//#endregion

		//#region >>> 0.51.03
		/////// <summary>
		/////// 0.51.03更新メソッド。
		/////// </summary>
		////static bool DataDB_05103(DB moddb, DB seedb)
		////{
		////    DAO.Database	db = null;
		////    DAO.DBEngine	de = new DAO.DBEngine();
		////    DAO.Workspace	ws = de.Workspaces[0];

		////    try
		////    {								
		////        string			sql;

		////        // DBLocation は mdb へのパス
		////        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		////        //■ X_DaichoにFGetsugakuTotalを追加
		////        sql = DBQuery.GetSql("ALTER TABLE {0} Add Column {1} CURRENCY", TableProp.X_Daicho, X_Daicho.FGetsugakuTotal);
		////        db.Execute(sql, null);
		////        db.TableDefs[TableProp.X_Daicho].Fields.Refresh();

		////        // 名前を変更する。
		////        db.TableDefs[TableProp.X_Daicho].Fields["MonthTotal"].Name	= X_Daicho.FNyukinTotal;
		////        db.TableDefs[TableProp.X_Daicho].Fields.Refresh();

		////        db.Close();
		////        db = null;
		////    }
		////    catch(Exception ex)
		////    {
		////        if (db != null)
		////        {
		////            db.Close();
		////            db = null;
		////        }

		////        _write_err(ex);

		////        return false;
		////    }

		////    return true;
		////}
		//#endregion

		//#region >>> 0.51.04
		/////// <summary>
		/////// 0.51.04更新メソッド。
		/////// </summary>
		////static bool DataDB_05104(DB moddb, DB seedb)
		////{
		////    DAO.Database	db = null;
		////    DAO.DBEngine	de = new DAO.DBEngine();
		////    DAO.Workspace	ws = de.Workspaces[0];

		////    try
		////    {								
		////        string			sql;

		////        // DBLocation は mdb へのパス
		////        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		////        //■ X_ChintaiJokyoのShikikinSumを通貨型へ
		////        sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} CURRENCY", TableProp.X_ChintaiJokyo, X_ChintaiJokyo.FShikikinSum);
		////        db.Execute(sql, null);
		////        db.TableDefs[TableProp.X_ChintaiJokyo].Fields.Refresh();

		////        //■ X_ChintaiJokyoのGetsugakuSumを通貨型へ
		////        sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} CURRENCY", TableProp.X_ChintaiJokyo, X_ChintaiJokyo.FGetsugakuSum);
		////        db.Execute(sql, null);
		////        db.TableDefs[TableProp.X_ChintaiJokyo].Fields.Refresh();

		////        db.Close();
		////        db = null;
		////    }
		////    catch(Exception ex)
		////    {
		////        if (db != null)
		////        {
		////            db.Close();
		////            db = null;
		////        }

		////        _write_err(ex);

		////        return false;
		////    }

		////    return true;
		////}
		//#endregion

		//#region >>> 0.51.05
		/////// <summary>
		/////// 0.51.05更新メソッド。
		/////// </summary>
		////static bool DataDB_05105(DB moddb, DB seedb)
		////{			
		////    // テーブル作成。
		////    if (moddb.ExecuteQuery(
		////            "CREATE TABLE {0} (" +
		////            "{1} COUNTER, " +
		////            "{2} TEXT(20), " +
		////            "{3} LONG, " +
		////            "{4} TEXT(20), " +
		////            "{5} LONG, " +
		////            "{6} TEXT(20), " +
		////            "{7} LONG, " +
		////            "{8} LONG, " +
		////            "{9} LONG, " +
		////            "{10} TEXT(20), " +
		////            "{11} DATETIME, " +
		////            "{12} CURRENCY, " +
		////            "{13} TEXT(20), " +
		////            "{14} TEXT(20), " +
		////            "{15} TEXT(20), " +

		////            "CONSTRAINT {1} PRIMARY KEY ({1})" +
		////            ")",
		////            TableProp.X_TainoJokyo,
		////            X_TainoJokyo.FID_Auto,
		////            X_TainoJokyo.FUsr_Name,
		////            X_TainoJokyo.FID_Bukken,
		////            X_TainoJokyo.FBukken_Name,
		////            X_TainoJokyo.FID_BukkenRoom,
		////            X_TainoJokyo.FBukkenRoom_Name,
		////            X_TainoJokyo.FID_KeiyakuGrp,
		////            X_TainoJokyo.FID_Keiyaku,
		////            X_TainoJokyo.FID_Karinushi,
		////            X_TainoJokyo.FKari_Name,
		////            X_TainoJokyo.FShiwake_DateYM,
		////            X_TainoJokyo.FGetsugakuSum,
		////            X_TainoJokyo.FKeiyaku_Addr0,
		////            X_TainoJokyo.FKeiyaku_Addr1,
		////            X_TainoJokyo.FKeiyaku_Tel
		////            ) < 0)
		////    {
		////        Debug.WriteLine("■ テーブル作成に失敗しました。");
		////        return false;
		////    }

		////    DAO.Database	db = null;
		////    DAO.DBEngine	de = new DAO.DBEngine();
		////    DAO.Workspace	ws = de.Workspaces[0];

		////    // 作成したテーブルの空文字列許可。
		////    try
		////    {							
		////        // DBLocation は mdb へのパス
		////        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		////        //■ M_Post.Post_Name
		////        // 空文字列許可を「はい」に
		////        db.TableDefs[TableProp.X_TainoJokyo].Fields[X_TainoJokyo.FUsr_Name].AllowZeroLength = true;
		////        db.TableDefs[TableProp.X_TainoJokyo].Fields[X_TainoJokyo.FBukken_Name].AllowZeroLength = true;
		////        db.TableDefs[TableProp.X_TainoJokyo].Fields[X_TainoJokyo.FBukkenRoom_Name].AllowZeroLength = true;
		////        db.TableDefs[TableProp.X_TainoJokyo].Fields[X_TainoJokyo.FKari_Name].AllowZeroLength = true;
		////        db.TableDefs[TableProp.X_TainoJokyo].Fields[X_TainoJokyo.FKeiyaku_Addr0].AllowZeroLength = true;
		////        db.TableDefs[TableProp.X_TainoJokyo].Fields[X_TainoJokyo.FKeiyaku_Addr1].AllowZeroLength = true;
		////        db.TableDefs[TableProp.X_TainoJokyo].Fields[X_TainoJokyo.FKeiyaku_Tel].AllowZeroLength = true;
		////        db.TableDefs[TableProp.X_TainoJokyo].Fields.Refresh();

		////        db.Close();
		////        db = null;
		////    }
		////    catch(Exception ex)
		////    {
		////        if (db != null)
		////        {
		////            db.Close();
		////            db = null;
		////        }

		////        _write_err(ex);

		////        return false;
		////    }

		////    return true;
		////}
		//#endregion

		//#region >>> 0.51.10
		/////// <summary>
		/////// 0.51.10更新メソッド。
		/////// </summary>
		////static bool DataDB_05110(DB moddb, DB seedb)
		////{
		////    DAO.Database	db = null;
		////    DAO.DBEngine	de = new DAO.DBEngine();
		////    DAO.Workspace	ws = de.Workspaces[0];

		////    try
		////    {								
		////        string			sql;

		////        // DBLocation は mdb へのパス
		////        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		////        //■ X_Daichoにフィールド追加
		////        sql = DBQuery.GetSql(
		////                    "ALTER TABLE {0} Add Column {1} CURRENCY, {2} CURRENCY, {3} CURRENCY, {4} CURRENCY, {5} CURRENCY, {6} CURRENCY",
		////                    TableProp.X_Daicho,
		////                    X_Daicho.FAzukariTotalNuki,
		////                    X_Daicho.FReikinTotalNuki,
		////                    X_Daicho.FKenrikinTotalNuki,
		////                    X_Daicho.FKoshinryoTotalNuki,
		////                    X_Daicho.FSonotaTotalNuki,
		////                    X_Daicho.FGetsugakuTotalNuki);

		////        db.Execute(sql, null);

		////        db.TableDefs[TableProp.T_User].Fields.Refresh();

		////        db.Close();
		////        db = null;
		////    }
		////    catch(Exception ex)
		////    {
		////        if (db != null)
		////        {
		////            db.Close();
		////            db = null;
		////        }

		////        _write_err(ex);

		////        return false;
		////    }

		////    return true;
		////}
		//#endregion

		//#region >>> 0.60.03
		///// <summary>
		///// 0.60.03更新メソッド。
		///// </summary>
		//static bool DataDB_06003(DB moddb, DB seedb)
		//{
		//    DAO.Database	db = null;
		//    DAO.DBEngine	de = new DAO.DBEngine();
		//    DAO.Workspace	ws = de.Workspaces[0];

		//    try
		//    {								
		//        string			sql;

		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//        //■ T_Shiwakeにフィールド追加
		//        sql = DBQuery.GetSql(
		//                    "ALTER TABLE {0} Add Column {1} LONG",
		//                    TableProp.T_Shiwake,
		//                    T_Shiwake.FShiwake_InvSort
		//					);
		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.T_Shiwake].Fields.Refresh();

		//		//■ X_DaichoGetsugaku
		//		sql = DBQuery.GetSql(
		//                    "ALTER TABLE {0} Add Column {1} LONG," +
		//					"Column {2} LONG, Column {3} LONG, Column {4} LONG, Column {5} LONG, Column {6} LONG, Column {7} LONG, Column {8} LONG, " +
		//					"Column {9} LONG, Column {10} LONG, Column {11} LONG, Column {12} LONG, Column {13} LONG, Column {14} LONG, Column {15} LONG, " +
		//					"Column {16} LONG, Column {17} LONG, Column {18} LONG, Column {19} LONG, Column {20} LONG, Column {21} LONG, Column {22} LONG,  Column {23} LONG",
		//                    TableProp.X_DaichoGetsugaku,
		//                    X_DaichoGetsugaku.FXG_InvSort,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku01,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku02,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku03,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku04,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku05,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku06,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku07,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku08,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku09,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku10,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku11,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku12,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku13,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku14,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku15,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku16,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku17,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku18,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku19,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku20,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku21,
		//					X_DaichoGetsugaku.FXG_KbnNyuryoku22
		//					);
		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_DaichoGetsugaku].Fields.Refresh();


		//        db.Close();
		//        db = null;
		//    }
		//    catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.60.04
		///// <summary>
		///// 0.60.04更新メソッド。
		///// </summary>
		//static bool DataDB_06004(DB moddb, DB seedb)
		//{			
		//    // テーブル作成。
		//    if (moddb.ExecuteQuery(
		//            "CREATE TABLE {0} (" +
		//            "{1} COUNTER, " +
		//            "{2} YESNO, " +
		//            "{3} YESNO, " +
		//            "{4} YESNO, " +
		//            "{5} LONG, " +
		//            "CONSTRAINT {1} PRIMARY KEY ({1})" +
		//            ")",
		//            TableProp.T_Option,
		//			T_Option.FID_Auto,
		//			T_Option.FOpt_DaichoDispDonyuMishu,
		//			T_Option.FOpt_DaichoDispKessan,
		//			T_Option.FOpt_DaichoDispMishuMaeukeColor,
		//			T_Option.FOpt_DaichoDispSort		            
		//            ) < 0)
		//    {
		//        Debug.WriteLine("■ テーブル作成に失敗しました。");
		//        return false;
		//    }

		//	// テーブルにデフォルトレコードを追加。
		//	try
		//	{
		//		DB		db	  = new DB(moddb.DBLocation, AppConst.DBPassword);
		//		DBView	moddv = new DBView(new DBAdapter(db, TableProp.T_Option));
		//		DBView	seedv = new DBView(new DBAdapter(seedb, TableProp.T_Option));

		//		for (int i = 0; i < seedv.Count; i++)
		//		{
		//			T_Option	mrow = new T_Option(moddv.NewRow());
		//			T_Option	srow = new T_Option(seedv[i]);

		//			mrow.Opt_DaichoDispDonyuMishu		= srow.Opt_DaichoDispDonyuMishu;
		//			mrow.Opt_DaichoDispKessan			= srow.Opt_DaichoDispKessan;
		//			mrow.Opt_DaichoDispMishuMaeukeColor	= srow.Opt_DaichoDispMishuMaeukeColor;
		//			mrow.Opt_DaichoDispSort				= srow.Opt_DaichoDispSort;

		//			moddv.Add(mrow.Row);
		//		}
		//		moddv.Update();
		//	}
		//	catch
		//	{
		//		return false;
		//	}

		//    return true;
		//}
		//#endregion

		//#region >>> 0.72.00
		///// <summary>
		///// 0.72.00更新メソッド。
		///// </summary>
		//static bool DataDB_07200(DB moddb, DB seedb)
		//{
		//    DAO.Database	db = null;
		//    DAO.DBEngine	de = new DAO.DBEngine();
		//    DAO.Workspace	ws = de.Workspaces[0];

		//    try
		//    {								
		//        string			sql;

		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//        //■ T_Bukkenにフィールド追加
		//        sql = DBQuery.GetSql(
		//                    "ALTER TABLE {0} Add Column {1} LONG, Column {2} LONG, Column {3} LONG, Column {4} LONG",
		//                    TableProp.T_Bukken,
		//                    T_Bukken.FID_Kamoku,
		//					T_Bukken.FID_Kamoku_Hojo,
		//					T_Bukken.FBukken_NyukinYoteibi,
		//					T_Bukken.FBukken_KbnShiharai
		//					);
		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.T_Bukken].Fields.Refresh();

		//        db.Close();
		//        db = null;
		//    }
		//    catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//	// テーブルにデフォルトレコードを追加。
		//	try
		//	{
		//		DB		mdb	  = new DB(moddb.DBLocation, AppConst.DBPassword);
		//		DBView	moddv = new DBView(new DBAdapter(mdb, TableProp.T_Bukken));

		//		for (int i = 0; i < moddv.Count; i++)
		//		{
		//			T_Bukken	mrow = new T_Bukken(moddv[i]);

		//			mrow.Bukken_KbnShiharai = eShiharai.Yokugetsu;
		//			mrow.Bukken_NyukinYoteibi = 31;					
		//		}
		//		moddv.Update();
		//	}
		//	catch
		//	{
		//		return false;
		//	}

		//    return true;
		//}
		//#endregion

		//#region >>> 0.81.01
		///// <summary>
		///// 0.81.01更新メソッド。
		///// </summary>
		//static bool DataDB_08101(DB moddb, DB seedb)
		//{
		//	// 1.X_DaichoのBukken_Kashiyaをint型へ変更
		//	// 2.T_ShunyuUchiwakeのShuUchi_KashiyaKashichiToをint型へ変更
		//	// 3.X_Out_ShunyuDaichoのBukken_Kashiyaをint型へ変更
		//	// 4.X_Out_ShunyuUchiwakeのShuUchi_KashiyaKashichiToをint型へ変更
		//	// 5.X_Out_ShunyuDaichoに Month0_DateYM ～ Month12_DateYM を日付型で追加
		//	// 6.T_KeiyakuTekiyoの店舗事務所の敷金を保証金へ変更。

		//    DAO.Database	db = null;
		//    DAO.DBEngine	de = new DAO.DBEngine();
		//    DAO.Workspace	ws = de.Workspaces[0];

		//    try
		//    {								
		//        string			sql;

		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//        // 1.X_DaichoのBukken_Kashiyaをint型へ変更
		//        sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FBukken_Kashiya);
		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Daicho].Fields.Refresh();

		//		// 2.T_ShunyuUchiwakeのShuUchi_KashiyaKashichiToをint型へ変更
		//        sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.T_ShunyuUchiwake, T_ShunyuUchiwake.FShuUchi_KashiyaKashichiTo);
		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.T_ShunyuUchiwake].Fields.Refresh();

		//		// 3.X_Out_ShunyuDaichoのBukken_Kashiyaをint型へ変更
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FBukken_Kashiya);
		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields.Refresh();

		//		// 4.X_Out_ShunyuUchiwakeのShuUchi_KashiyaKashichiToをint型へ変更
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuUchiwake, X_Out_ShunyuUchiwake.FShuUchi_KashiyaKashichiTo);
		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Out_ShunyuUchiwake].Fields.Refresh();

		//        // 5.X_Out_ShunyuDaichoに Month0_DateYM ～ Month12_DateYM を日付型で追加
		//        sql = DBQuery.GetSql(
		//                    "ALTER TABLE {0} Add Column {1} DATETIME, Column {2} DATETIME, Column {3} DATETIME, Column {4} DATETIME, Column {5} DATETIME, " +
		//									    "Column {6} DATETIME, Column {7} DATETIME, Column {8} DATETIME, Column {9} DATETIME, Column {10} DATETIME, " +
		//										"Column {11} DATETIME, Column {12} DATETIME, Column {13} DATETIME",
		//                    TableProp.X_Out_ShunyuDaicho,
		//                    X_Out_ShunyuDaicho.FMonth0_DateYM,
		//					X_Out_ShunyuDaicho.FMonth1_DateYM,
		//					X_Out_ShunyuDaicho.FMonth2_DateYM,
		//					X_Out_ShunyuDaicho.FMonth3_DateYM,
		//					X_Out_ShunyuDaicho.FMonth4_DateYM,
		//					X_Out_ShunyuDaicho.FMonth5_DateYM,
		//					X_Out_ShunyuDaicho.FMonth6_DateYM,
		//					X_Out_ShunyuDaicho.FMonth7_DateYM,
		//					X_Out_ShunyuDaicho.FMonth8_DateYM,
		//					X_Out_ShunyuDaicho.FMonth9_DateYM,
		//					X_Out_ShunyuDaicho.FMonth10_DateYM,
		//					X_Out_ShunyuDaicho.FMonth11_DateYM,
		//					X_Out_ShunyuDaicho.FMonth12_DateYM
		//					);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields.Refresh();

		//        db.Close();
		//        db = null;
		//    }
		//    catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//	// 6.T_KeiyakuTekiyoの店舗事務所の敷金を保証金へ変更。
		//	try
		//	{
		//		DB		mdb	  = new DB(moddb.DBLocation, AppConst.DBPassword);
		//		DBView	moddv = new DBView(new DBAdapter(mdb, TableProp.T_KeiyakuTekiyo));
		//		DBView	seedv = new DBView(new DBAdapter(seedb, TableProp.T_KeiyakuTekiyo));

		//		moddv.RowFilterQuery(
		//			"{0} = {1}",
		//			T_KeiyakuTekiyo.FID_Keiyaku,
		//			0);
		//		seedv.RowFilter = moddv.RowFilter;

		//		//■ デフォ値を全て削除。
		//		for (;;)
		//		{
		//			if (moddv.Count == 0)
		//			{
		//				break;
		//			}
		//			moddv.Delete();
		//		}

		//		//■ デフォ値を全て変更。
		//		for (int i = 0; i < seedv.Count; i++)
		//		{
		//			DataRow newrow = moddv.NewRow();

		//			AppDb.CopyDataRow(seedv[i].Row, newrow);				

		//			moddv.Add(newrow);
		//		}
		//		moddv.Update();
		//	}
		//	catch
		//	{
		//		return false;
		//	}

		//    return true;
		//}
		//#endregion

		//#region >>> 0.81.04
		///// <summary>
		///// 0.81.04更新メソッド。
		///// </summary>
		//static bool DataDB_08104(DB moddb, DB seedb)
		//{


		//    DAO.Database	db = null;
		//    DAO.DBEngine	de = new DAO.DBEngine();
		//    DAO.Workspace	ws = de.Workspaces[0];

		//	// 1.インデックス「はい（重複なし）」に設定
		//    try
		//    {
		//		// DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//        Dictionary<string, string> tbls = new Dictionary<string,string>();
		//		tbls.Add(TableProp.T_Bukken,			T_Bukken.FID_Bukken);
		//		tbls.Add(TableProp.T_BukkenRoom,		T_BukkenRoom.FID_BukkenRoom);
		//		tbls.Add(TableProp.T_Karinushi,			T_Karinushi.FID_Karinushi);
		//		tbls.Add(TableProp.T_Keiyaku,			T_Keiyaku.FID_Keiyaku);
		//		tbls.Add(TableProp.T_KeiyakuTekiyo,		T_KeiyakuTekiyo.FID_KeiyakuTekiyo);
		//		tbls.Add(TableProp.T_SeikyuTokusoku,	T_SeikyuTokusoku.FID_SeiToku);
		//		tbls.Add(TableProp.T_Shiwake,			T_Shiwake.FID_Shiwake);
		//		tbls.Add(TableProp.T_ShunyuUchiwake,	T_ShunyuUchiwake.FID_ShuUchi);
		//		tbls.Add(TableProp.T_UserNendo,			T_UserNendo.FID_UserNendo);		        

		//        // 各テーブルのインデックスフィールドを「はい（重複なし）にする。
		//		foreach(string tbl in tbls.Keys)
		//		{	
		//			// 既に存在するインデックスを削除
		//			for (int i = 0; i < db.TableDefs[tbl].Indexes.Count; i++)
		//			{
		//				if (db.TableDefs[tbl].Indexes[i].Name == tbls[tbl])
		//				{
		//					db.TableDefs[tbl].Indexes.Delete(tbls[tbl]);
		//					break;
		//				}
		//			}

		//			// インデックス作成
		//			DAO.Index idx = db.TableDefs[tbl].CreateIndex(tbls[tbl]);
		//			DAO.Field fld = db.TableDefs[tbl].Fields[tbls[tbl]];

		//			// 「はい（重複なし）」
		//			idx.Unique  = true;

		//			// 作成したインデックスを追加。
		//			DAO.IndexFields flds = (DAO.IndexFields)idx.Fields;
		//			flds.Append(idx.CreateField(fld.Name, fld.Type, fld.Size));
		//			db.TableDefs[tbl].Indexes.Append(idx);
		//		}

		//        db.Close();
		//        db = null;
		//    }
		//    catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);
		//		MsgBox.Show(
		//			"重複するインデックスが登録されているため、フィールドの更新に失敗しました。",
		//			"データベース更新エラー", 
		//			System.Windows.Forms.MessageBoxButtons.OK, 
		//			System.Windows.Forms.MessageBoxIcon.Error, 
		//			System.Windows.Forms.MessageBoxDefaultButton.Button1
		//			);

		//        return false;
		//    }

		//	// 2.X_ZaimuShiwakeにフィールド追加
		//	try
		//    {
		//		// DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//        string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} TEXT(50), Column {2} TEXT(50), Column {3} DATETIME, Column {4} LONG",
		//							TableProp.X_ZaimuShiwake,
		//							X_ZaimuShiwake.FZShiwake_BukkenName,
		//							X_ZaimuShiwake.FZShiwake_KarinushiName,
		//							X_ZaimuShiwake.FZShiwake_DateYM,
		//							X_ZaimuShiwake.FZShiwake_BukkenRoomSort
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_ZaimuShiwake].Fields.Refresh();

		//		// 空白文字を許可
		//        db.TableDefs[TableProp.X_ZaimuShiwake].Fields[X_ZaimuShiwake.FZShiwake_BukkenName].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_ZaimuShiwake].Fields[X_ZaimuShiwake.FZShiwake_KarinushiName].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_ZaimuShiwake].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	}
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);				
		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.82.01
		///// <summary>
		///// 0.82.01更新メソッド。
		///// </summary>
		//static bool DataDB_08201(DB moddb, DB seedb)
		//{

		//	DAO.Database	db = null;
		//    DAO.DBEngine	de = new DAO.DBEngine();
		//    DAO.Workspace	ws = de.Workspaces[0];

		//	//1.X_Out_ChintaiJokyo
		//	//Usr_Name				削除
		//	//XKeiyaku_DateFrom		削除
		//	//XKeiyaku_DateTo		削除
		//	//GetsugakuSum			削除
		//	//ShikikinSum			削除

		//	//ID_KeiyakuGrp		int型		追加
		//	//Shiwake_Cost 		decimal型	追加
		//	//Keiyaku_Latest 	bool型		追加
		//	//KTeki_CostYotei 	decimal型	追加
		//	//XBukken_Name		string型	追加
		//	//XKeiyaku_Date 	string型	追加
		//	//XKTeki_CostYotei 	string型	追加
		//	//XShiwake_Cost 	string型	追加

		//	//Keiyaku_DateFrom		string > DateTime	変更
		//	//Keiyaku_DateKaiyaku	string > DateTime	変更
		//	//Keiyaku_DateTo		string > DateTime	変更

		//	//2.T_LogShiwakeMakeにlgsm_Shohizeiを追加

		//	try
		//    {								
		//        string			sql;

		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//        // フィールド削除
		//        sql = DBQuery.GetSql("ALTER TABLE {0} DROP COLUMN {1} ", TableProp.X_Out_ChintaiJokyo, "Usr_Name");
		//        db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} DROP COLUMN {1} ", TableProp.X_Out_ChintaiJokyo, "XKeiyaku_DateFrom");
		//        db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} DROP COLUMN {1} ", TableProp.X_Out_ChintaiJokyo, "XKeiyaku_DateTo");
		//        db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} DROP COLUMN {1} ", TableProp.X_Out_ChintaiJokyo, "GetsugakuSum");
		//        db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} DROP COLUMN {1} ", TableProp.X_Out_ChintaiJokyo, "ShikikinSum");
		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Out_ChintaiJokyo].Fields.Refresh();

		//		// フィールド追加
		//        sql = DBQuery.GetSql(
		//                    "ALTER TABLE {0} Add Column {1} LONG, Column {2} CURRENCY, Column {3} YESNO, Column {4} CURRENCY, Column {5} TEXT(50), " +
		//									    "Column {6} TEXT(50), Column {7} TEXT(50), Column {8} TEXT(50)",
		//                    TableProp.X_Out_ChintaiJokyo,
		//                    X_Out_ChintaiJokyo.FID_KeiyakuGrp,
		//					X_Out_ChintaiJokyo.FShiwake_Cost,
		//					X_Out_ChintaiJokyo.FKeiyaku_Latest,
		//					X_Out_ChintaiJokyo.FKTeki_CostYotei,
		//					X_Out_ChintaiJokyo.FXBukken_Name,
		//					X_Out_ChintaiJokyo.FXKeiyaku_Date,
		//					X_Out_ChintaiJokyo.FXKTeki_CostYotei,
		//					X_Out_ChintaiJokyo.FXShiwake_Cost							
		//					);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Out_ChintaiJokyo].Fields.Refresh();

		//		// 空白文字を許可
		//        db.TableDefs[TableProp.X_Out_ChintaiJokyo].Fields[X_Out_ChintaiJokyo.FXBukken_Name].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ChintaiJokyo].Fields[X_Out_ChintaiJokyo.FXKeiyaku_Date].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ChintaiJokyo].Fields[X_Out_ChintaiJokyo.FXKTeki_CostYotei].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ChintaiJokyo].Fields[X_Out_ChintaiJokyo.FXShiwake_Cost].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ChintaiJokyo].Fields.Refresh();

		//		// 型変換
		//        sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} DATETIME", TableProp.X_Out_ChintaiJokyo, X_Out_ChintaiJokyo.FKeiyaku_DateFrom);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} DATETIME", TableProp.X_Out_ChintaiJokyo, X_Out_ChintaiJokyo.FKeiyaku_DateKaiyaku);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} DATETIME", TableProp.X_Out_ChintaiJokyo, X_Out_ChintaiJokyo.FKeiyaku_DateTo);
		//		db.Execute(sql, null);

		//        db.TableDefs[TableProp.X_Out_ChintaiJokyo].Fields.Refresh();

		//		// 2.T_LogShiwakeMakeにlgsm_Shohizeiを追加
		//        sql = DBQuery.GetSql(
		//                    "ALTER TABLE {0} Add Column {1} TEXT(50)",
		//                    TableProp.T_LogShiwakeMake,
		//                    T_LogShiwakeMake.Flgsm_Shohizei														
		//					);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.T_LogShiwakeMake].Fields.Refresh();

		//		db.TableDefs[TableProp.T_LogShiwakeMake].Fields[T_LogShiwakeMake.Flgsm_Shohizei].AllowZeroLength = true;
		//		db.TableDefs[TableProp.T_LogShiwakeMake].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.82.03
		///// <summary>
		///// 0.82.03更新メソッド。
		///// </summary>
		//static bool DataDB_08203(DB moddb, DB seedb)
		//{

		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	//1.X_Out_Mishumeisai
		//	//Shiwake_DateYM		string > DateTime	変更
		//	//Shiwake_DateNyukin	string > DateTime	変更
		//	try
		//    {								
		//        string			sql;

		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//		// 型変換
		//        sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} DATETIME", TableProp.X_Out_Mishumeisai, X_Out_Mishumeisai.FShiwake_DateYM);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} DATETIME", TableProp.X_Out_Mishumeisai, X_Out_Mishumeisai.FShiwake_DateNyukin);
		//		db.Execute(sql, null);

		//        db.TableDefs[TableProp.X_Out_Mishumeisai].Fields.Refresh();
		//	}
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }
		//    return true;
		//}
		//#endregion

		//#region >>> 0.82.04
		///// <summary>
		///// 0.82.04更新メソッド。
		///// </summary>
		//static bool DataDB_08204(DB moddb, DB seedb)
		//{		
		//    DAO.Database	db = null;
		//    DAO.DBEngine	de = new DAO.DBEngine();
		//    DAO.Workspace	ws = de.Workspaces[0];

		//	// X_Daichoにフィールド追加
		//	try
		//    {
		//		// DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//        string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} YESNO, Column {2} YESNO, Column {3} YESNO, Column {4} YESNO, Column {5} YESNO, Column {6} YESNO, Column {7} YESNO, " +
		//											    "Column {8} YESNO, Column {9} YESNO, Column {10} YESNO, Column {11} YESNO, Column {12} YESNO, Column {13} YESNO",
		//							TableProp.X_Daicho,
		//							X_Daicho.FMonth0_SeikyuNyukinOver,
		//							X_Daicho.FMonth1_SeikyuNyukinOver,
		//							X_Daicho.FMonth2_SeikyuNyukinOver,
		//							X_Daicho.FMonth3_SeikyuNyukinOver,
		//							X_Daicho.FMonth4_SeikyuNyukinOver,
		//							X_Daicho.FMonth5_SeikyuNyukinOver,
		//							X_Daicho.FMonth6_SeikyuNyukinOver,
		//							X_Daicho.FMonth7_SeikyuNyukinOver,
		//							X_Daicho.FMonth8_SeikyuNyukinOver,
		//							X_Daicho.FMonth9_SeikyuNyukinOver,
		//							X_Daicho.FMonth10_SeikyuNyukinOver,
		//							X_Daicho.FMonth11_SeikyuNyukinOver,
		//							X_Daicho.FMonth12_SeikyuNyukinOver
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Daicho].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	}
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);				
		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.82.05
		///// <summary>
		///// 0.82.05更新メソッド。
		///// </summary>
		//static bool DataDB_08205(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	// X_Out_ShunyuDaicho 追加


		//	// X_Daichoにフィールド追加
		//	try
		//    {
		//		// DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//        string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} YESNO, Column {2} YESNO, Column {3} YESNO, Column {4} YESNO, Column {5} YESNO, Column {6} YESNO, Column {7} YESNO, " +
		//											    "Column {8} YESNO, Column {9} YESNO, Column {10} YESNO, Column {11} YESNO, Column {12} YESNO, Column {13} YESNO",
		//							TableProp.X_Out_ShunyuDaicho,
		//							X_Out_ShunyuDaicho.FMonth0_SeikyuNyukinOver,
		//							X_Out_ShunyuDaicho.FMonth1_SeikyuNyukinOver,									
		//							X_Out_ShunyuDaicho.FMonth2_SeikyuNyukinOver,
		//							X_Out_ShunyuDaicho.FMonth3_SeikyuNyukinOver,
		//							X_Out_ShunyuDaicho.FMonth4_SeikyuNyukinOver,
		//							X_Out_ShunyuDaicho.FMonth5_SeikyuNyukinOver,
		//							X_Out_ShunyuDaicho.FMonth6_SeikyuNyukinOver,
		//							X_Out_ShunyuDaicho.FMonth7_SeikyuNyukinOver,
		//							X_Out_ShunyuDaicho.FMonth8_SeikyuNyukinOver,
		//							X_Out_ShunyuDaicho.FMonth9_SeikyuNyukinOver,
		//							X_Out_ShunyuDaicho.FMonth10_SeikyuNyukinOver,
		//							X_Out_ShunyuDaicho.FMonth11_SeikyuNyukinOver,
		//							X_Out_ShunyuDaicho.FMonth12_SeikyuNyukinOver
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	}
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);				
		//        return false;
		//    }
		//    return true;
		//}
		//#endregion		

		//#region >>> 0.84.01
		///// <summary>
		///// 0.84.01更新メソッド。
		///// </summary>
		//static bool DataDB_08401(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	// T_ShunyuUchiwake

		//	//■削除
		//	// ShuUchi_BukkenName
		//	// ShuUchi_BukkenRoomName

		//	//■追加
		//	//ID_Bukken			  int型		追加
		//	//ID_BukkenRoom		  int型		追加
		//	//BukkenRoom_SortNo	  int型		追加
		//	//Bukken_NameFurigana string型	追加
		//	try
		//    {								
		//        string			sql;

		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//        // フィールド削除
		//        sql = DBQuery.GetSql("ALTER TABLE {0} DROP COLUMN {1} ", TableProp.T_ShunyuUchiwake, "ShuUchi_BukkenName");
		//        db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} DROP COLUMN {1} ", TableProp.T_ShunyuUchiwake, "ShuUchi_BukkenRoomName");
		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.T_ShunyuUchiwake].Fields.Refresh();

		//		// フィールド追加
		//        sql = DBQuery.GetSql(
		//                    "ALTER TABLE {0} Add Column {1} LONG, Column {2} LONG, Column {3} LONG, Column {4} TEXT(50)",
		//                    TableProp.T_ShunyuUchiwake,
		//                    T_ShunyuUchiwake.FID_Bukken,
		//					T_ShunyuUchiwake.FID_BukkenRoom,
		//					T_ShunyuUchiwake.FBukkenRoom_SortNo,
		//					T_ShunyuUchiwake.FBukken_NameFurigana										
		//					);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.T_ShunyuUchiwake].Fields.Refresh();

		//		// 空白文字を許可
		//        db.TableDefs[TableProp.T_ShunyuUchiwake].Fields[T_ShunyuUchiwake.FBukken_NameFurigana].AllowZeroLength = true;
		//		db.TableDefs[TableProp.T_ShunyuUchiwake].Fields.Refresh();

		//		// 既存レコードを全て削除する。
		//		db.Execute(DBQuery.GetSql("DELETE * FROM {0}", TableProp.T_ShunyuUchiwake), null);

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.84.02
		///// <summary>
		///// 0.84.02更新メソッド。
		///// </summary>
		//static bool DataDB_08402(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	// X_Out_Maeukemeisai

		//	//■削除
		//	// Maeuke_XKeiyaku_DateFrom
		//	// Maeuke_XKeiyaku_DateTo
		//	// Keiyaku_Date

		//	//■追加
		//	//X_Out_Maeukemeisai.FMaeuke_Chk_Kaiyaku
		//	//X_Out_Maeukemeisai.FMaeuke_XKeiyaku_Date
		//	//X_Out_Maeukemeisai.FMaeuke_XShiwake_DateNyukin
		//	//X_Out_Maeukemeisai.FMaeuke_XShiwake_DateYM

		//	//■型変更
		//	// X_Out_Maeukemeisai.FKeiyaku_DateFrom		string > datetime
		//	// X_Out_Maeukemeisai.FKeiyaku_DateTo		string > datetime
		//	// X_Out_Maeukemeisai.FShiwake_DateYM		string > datetime
		//	// X_Out_Maeukemeisai.FShiwake_DateNyukin	string > datetime


		//	// X_Out_ShunyuUchiwake
		//	// X_Out_ShunyuUchiwake.FBukken_NameFuriganaを追加
		//	try
		//    {								
		//        string			sql;

		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//        // フィールド削除
		//        sql = DBQuery.GetSql("ALTER TABLE {0} DROP COLUMN {1} ", TableProp.X_Out_Maeukemeisai, "Maeuke_XKeiyaku_DateFrom");
		//        db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} DROP COLUMN {1} ", TableProp.X_Out_Maeukemeisai, "Maeuke_XKeiyaku_DateTo");
		//        db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} DROP COLUMN {1} ", TableProp.X_Out_Maeukemeisai, "Keiyaku_Date");
		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Out_Maeukemeisai].Fields.Refresh();

		//		// フィールド追加
		//        sql = DBQuery.GetSql(
		//                    "ALTER TABLE {0} Add Column {1} YESNO, Column {2} TEXT(50), Column {3} TEXT(50), Column {4} TEXT(50)",
		//                    TableProp.X_Out_Maeukemeisai,
		//                    X_Out_Maeukemeisai.FMaeuke_Chk_Kaiyaku,
		//					X_Out_Maeukemeisai.FMaeuke_XKeiyaku_Date,
		//					X_Out_Maeukemeisai.FMaeuke_XShiwake_DateNyukin,
		//					X_Out_Maeukemeisai.FMaeuke_XShiwake_DateYM										
		//					);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Out_Maeukemeisai].Fields.Refresh();

		//		// 空白文字を許可
		//        db.TableDefs[TableProp.X_Out_Maeukemeisai].Fields[X_Out_Maeukemeisai.FMaeuke_XKeiyaku_Date].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_Maeukemeisai].Fields[X_Out_Maeukemeisai.FMaeuke_XShiwake_DateNyukin].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_Maeukemeisai].Fields[X_Out_Maeukemeisai.FMaeuke_XShiwake_DateYM].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_Maeukemeisai].Fields.Refresh();

		//		// 型変換
		//        sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} DATETIME", TableProp.X_Out_Maeukemeisai, X_Out_Maeukemeisai.FKeiyaku_DateFrom);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} DATETIME", TableProp.X_Out_Maeukemeisai, X_Out_Maeukemeisai.FKeiyaku_DateTo);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} DATETIME", TableProp.X_Out_Maeukemeisai, X_Out_Maeukemeisai.FShiwake_DateYM);
		//		db.Execute(sql, null);
		//        sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} DATETIME", TableProp.X_Out_Maeukemeisai, X_Out_Maeukemeisai.FShiwake_DateNyukin);
		//		db.Execute(sql, null);

		//        db.TableDefs[TableProp.X_Out_ChintaiJokyo].Fields.Refresh();

		//		// フィールド追加
		//		sql = DBQuery.GetSql(
		//		            "ALTER TABLE {0} Add Column {1} TEXT(50)",
		//		            TableProp.X_Out_ShunyuUchiwake,
		//		            X_Out_ShunyuUchiwake.FBukken_NameFurigana						
		//		            );

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.X_Out_ShunyuUchiwake].Fields[X_Out_ShunyuUchiwake.FBukken_NameFurigana].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuUchiwake].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.84.03
		///// <summary>
		///// 0.84.03更新メソッド。
		///// </summary>
		//static bool DataDB_08403(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//    {												
		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} CURRENCY, Column {2} CURRENCY, Column {3} CURRENCY, Column {4} CURRENCY, Column {5} CURRENCY, Column {6} CURRENCY, Column {7} CURRENCY, " +
		//											    "Column {8} CURRENCY, Column {9} CURRENCY, Column {10} CURRENCY, Column {11} CURRENCY, Column {12} CURRENCY, Column {13} CURRENCY",
		//							TableProp.X_Daicho,
		//							X_Daicho.FMonth0_SeikyuNyukinTotalKishu,
		//							X_Daicho.FMonth1_SeikyuNyukinTotalKishu,
		//							X_Daicho.FMonth2_SeikyuNyukinTotalKishu,
		//							X_Daicho.FMonth3_SeikyuNyukinTotalKishu,
		//							X_Daicho.FMonth4_SeikyuNyukinTotalKishu,
		//							X_Daicho.FMonth5_SeikyuNyukinTotalKishu,
		//							X_Daicho.FMonth6_SeikyuNyukinTotalKishu,
		//							X_Daicho.FMonth7_SeikyuNyukinTotalKishu,
		//							X_Daicho.FMonth8_SeikyuNyukinTotalKishu,
		//							X_Daicho.FMonth9_SeikyuNyukinTotalKishu,
		//							X_Daicho.FMonth10_SeikyuNyukinTotalKishu,
		//							X_Daicho.FMonth11_SeikyuNyukinTotalKishu,
		//							X_Daicho.FMonth12_SeikyuNyukinTotalKishu
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Daicho].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.84.04
		///// <summary>
		///// 0.84.04更新メソッド。
		///// </summary>
		//static bool DataDB_08404(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	// X_Daichoへフィールド追加
		//	try
		//    {												
		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} CURRENCY, Column {2} CURRENCY, Column {3}  CURRENCY, Column {4}  CURRENCY, Column {5}  CURRENCY, Column {6}  CURRENCY, Column {7}  CURRENCY, " +
		//											    "Column {8} CURRENCY, Column {9} CURRENCY, Column {10} CURRENCY, Column {11} CURRENCY, Column {12} CURRENCY, Column {13} CURRENCY, Column {14} CURRENCY",
		//							TableProp.X_Daicho,
		//							X_Daicho.FMonth0_SeikyuTotalNuki,
		//							X_Daicho.FMonth1_SeikyuTotalNuki,
		//							X_Daicho.FMonth2_SeikyuTotalNuki,
		//							X_Daicho.FMonth3_SeikyuTotalNuki,
		//							X_Daicho.FMonth4_SeikyuTotalNuki,
		//							X_Daicho.FMonth5_SeikyuTotalNuki,
		//							X_Daicho.FMonth6_SeikyuTotalNuki,
		//							X_Daicho.FMonth7_SeikyuTotalNuki,
		//							X_Daicho.FMonth8_SeikyuTotalNuki,
		//							X_Daicho.FMonth9_SeikyuTotalNuki,
		//							X_Daicho.FMonth10_SeikyuTotalNuki,
		//							X_Daicho.FMonth11_SeikyuTotalNuki,
		//							X_Daicho.FMonth12_SeikyuTotalNuki,
		//							X_Daicho.FReiKenKoTotalNuki
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Daicho].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.84.05
		///// <summary>
		///// 0.84.05更新メソッド。
		///// </summary>
		//static bool DataDB_08405(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	//X_Out_ShunyuDaicho

		//	//■追加
		//	//X_Out_ShunyuDaicho.FAzukariTotalNuki			string型
		//	//X_Out_ShunyuDaicho.FReiKenKoTotalNuki			string型
		//	//X_Out_ShunyuDaicho.FMonth0_SeikyuTotalNuki	string型
		//	//X_Out_ShunyuDaicho.FMonth1_SeikyuTotalNuki	string型
		//	//X_Out_ShunyuDaicho.FMonth2_SeikyuTotalNuki	string型
		//	//X_Out_ShunyuDaicho.FMonth3_SeikyuTotalNuki	string型
		//	//X_Out_ShunyuDaicho.FMonth4_SeikyuTotalNuki	string型
		//	//X_Out_ShunyuDaicho.FMonth5_SeikyuTotalNuki	string型
		//	//X_Out_ShunyuDaicho.FMonth6_SeikyuTotalNuki	string型
		//	//X_Out_ShunyuDaicho.FMonth7_SeikyuTotalNuki	string型
		//	//X_Out_ShunyuDaicho.FMonth8_SeikyuTotalNuki	string型
		//	//X_Out_ShunyuDaicho.FMonth9_SeikyuTotalNuki	string型
		//	//X_Out_ShunyuDaicho.FMonth10_SeikyuTotalNuki	string型
		//	//X_Out_ShunyuDaicho.FMonth11_SeikyuTotalNuki	string型
		//	//X_Out_ShunyuDaicho.FMonth12_SeikyuTotalNuki	string型

		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld32_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld33_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld34_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld35_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld36_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld37_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld38_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld39_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld40_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld41_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld42_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld43_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld44_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld45_Length	bool型
		//	//X_Out_ShunyuDaicho.Fchk_Daicho_Fld46_Length	bool型

		//	try
		//    {												
		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1}  TEXT(50), Column {2} TEXT(50),  Column {3}  TEXT(50), Column {4}  TEXT(50), Column {5}  TEXT(50), " +
		//												"Column {6}  TEXT(50), Column {7}  TEXT(50), Column {8}  TEXT(50), Column {9}  TEXT(50), Column {10} TEXT(50), " +
		//												"Column {11} TEXT(50), Column {12} TEXT(50), Column {13} TEXT(50), Column {14} TEXT(50), Column {15} TEXT(50), " +
		//												"Column {16} YESNO,	   Column {17} YESNO,    Column {18} YESNO,    Column {19} YESNO,    Column {20} YESNO, " +
		//												"Column {21} YESNO,	   Column {22} YESNO,    Column {23} YESNO,    Column {24} YESNO,    Column {25} YESNO, " +
		//												"Column {26} YESNO,	   Column {27} YESNO,    Column {28} YESNO,    Column {29} YESNO,    Column {30} YESNO",
		//							TableProp.X_Out_ShunyuDaicho,
		//							"Month0_SeikyuTotalNuki",
		//							"Month1_SeikyuTotalNuki",
		//							"Month2_SeikyuTotalNuki",
		//							"Month3_SeikyuTotalNuki",
		//							"Month4_SeikyuTotalNuki",
		//							"Month5_SeikyuTotalNuki",
		//							"Month6_SeikyuTotalNuki",
		//							"Month7_SeikyuTotalNuki",
		//							"Month8_SeikyuTotalNuki",
		//							"Month9_SeikyuTotalNuki",
		//							"Month10_SeikyuTotalNuki",
		//							"Month11_SeikyuTotalNuki",
		//							"Month12_SeikyuTotalNuki",
		//							X_Out_ShunyuDaicho.FReiKenKoTotalNuki,
		//							X_Out_ShunyuDaicho.FAzukariTotalNuki,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld32_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld33_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld34_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld35_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld36_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld37_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld38_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld39_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld40_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld41_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld42_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld43_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld44_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld45_Length,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld46_Length
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields.Refresh();

		//		// 空白文字を許可
		//        db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month0_SeikyuTotalNuki"].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month1_SeikyuTotalNuki"].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month2_SeikyuTotalNuki"].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month3_SeikyuTotalNuki"].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month4_SeikyuTotalNuki"].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month5_SeikyuTotalNuki"].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month6_SeikyuTotalNuki"].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month7_SeikyuTotalNuki"].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month8_SeikyuTotalNuki"].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month9_SeikyuTotalNuki"].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month10_SeikyuTotalNuki"].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month11_SeikyuTotalNuki"].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month12_SeikyuTotalNuki"].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields[X_Out_ShunyuDaicho.FReiKenKoTotalNuki].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields[X_Out_ShunyuDaicho.FAzukariTotalNuki].AllowZeroLength = true;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.85.02
		///// <summary>
		///// 0.85.02更新メソッド。
		///// </summary>
		//static bool DataDB_08502(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//    {												
		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1}  LONGTEXT",
		//							TableProp.T_Option,
		//							T_Option.FOpt_ShiwakeFilePath
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.T_Option].Fields.Refresh();

		//		// 空白文字を許可
		//        db.TableDefs[TableProp.T_Option].Fields[T_Option.FOpt_ShiwakeFilePath].AllowZeroLength = true;				
		//		db.TableDefs[TableProp.T_Option].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.85.04
		///// <summary>
		///// 0.85.04更新メソッド。
		///// </summary>
		//static bool DataDB_08504(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//    {	
		//		// DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);	

		//		string sql = "";

		//		//■追加
		//		//X_Out_NyukinKakuninhyo.FID_Shiwake int型
		//		// フィールド追加
		//		sql = DBQuery.GetSql(
		//					"ALTER TABLE {0} Add Column {1} LONG",
		//					TableProp.X_Out_NyukinKakuninhyo,
		//					X_Out_NyukinKakuninhyo.FID_Shiwake
		//					);

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.X_Out_NyukinKakuninhyo].Fields.Refresh();

		//		// 型変換
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FMonth0_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FMonth1_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FMonth2_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FMonth3_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FMonth4_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FMonth5_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FMonth6_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FMonth7_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FMonth8_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FMonth9_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FMonth10_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FMonth11_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Daicho, X_Daicho.FMonth12_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.X_Daicho].Fields.Refresh();


		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FMonth0_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FMonth1_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FMonth2_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FMonth3_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FMonth4_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FMonth5_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FMonth6_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FMonth7_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FMonth8_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FMonth9_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FMonth10_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FMonth11_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		sql = DBQuery.GetSql("ALTER TABLE {0} ALTER COLUMN {1} LONG", TableProp.X_Out_ShunyuDaicho, X_Out_ShunyuDaicho.FMonth12_SeikyuNyukinOver);
		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields.Refresh();

		//   		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.86.00
		///// <summary>
		///// 0.86.00更新メソッド。
		///// </summary>
		//static bool DataDB_08600(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//    {	
		//		// DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);	

		//		string sql = "";

		//		//■追加
		//		// フィールド追加
		//		sql = DBQuery.GetSql(
		//					"ALTER TABLE {0} Add Column {1} DATETIME",
		//					TableProp.X_Daicho,
		//					X_Daicho.FBukken_DateJokyaku
		//					);

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.X_Daicho].Fields.Refresh();

		//   		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.90.02
		///// <summary>
		///// 0.90.02更新メソッド。
		///// </summary>
		//static bool DataDB_09002(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	// X_Daichoへフィールド追加
		//	try
		//    {												
		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} CURRENCY, Column {2} CURRENCY, Column {3}  CURRENCY, Column {4}  CURRENCY, Column {5}  CURRENCY, Column {6}  CURRENCY, Column {7}  CURRENCY, " +
		//											    "Column {8} CURRENCY, Column {9} CURRENCY, Column {10} CURRENCY, Column {11} CURRENCY, Column {12} CURRENCY, Column {13} CURRENCY, "+
		//												"Column {14} CURRENCY, Column {15} CURRENCY, Column {16} CURRENCY, Column {17} CURRENCY, Column {18} CURRENCY, Column {19} CURRENCY, Column {20} CURRENCY, " +
		//												"Column {21} CURRENCY, Column {22} CURRENCY, Column {23} CURRENCY, Column {24} CURRENCY, Column {25} CURRENCY, Column {26} CURRENCY",
		//							TableProp.X_Daicho,
		//							X_Daicho.FMonth0_DaichoKessanSeikyu,
		//							X_Daicho.FMonth1_DaichoKessanSeikyu,
		//							X_Daicho.FMonth2_DaichoKessanSeikyu,
		//							X_Daicho.FMonth3_DaichoKessanSeikyu,
		//							X_Daicho.FMonth4_DaichoKessanSeikyu,
		//							X_Daicho.FMonth5_DaichoKessanSeikyu,
		//							X_Daicho.FMonth6_DaichoKessanSeikyu,
		//							X_Daicho.FMonth7_DaichoKessanSeikyu,
		//							X_Daicho.FMonth8_DaichoKessanSeikyu,
		//							X_Daicho.FMonth9_DaichoKessanSeikyu,
		//							X_Daicho.FMonth10_DaichoKessanSeikyu,
		//							X_Daicho.FMonth11_DaichoKessanSeikyu,
		//							X_Daicho.FMonth12_DaichoKessanSeikyu,
		//							X_Daicho.FMonth0_DaichoKessanSeikyuNuki,
		//							X_Daicho.FMonth1_DaichoKessanSeikyuNuki,
		//							X_Daicho.FMonth2_DaichoKessanSeikyuNuki,
		//							X_Daicho.FMonth3_DaichoKessanSeikyuNuki,
		//							X_Daicho.FMonth4_DaichoKessanSeikyuNuki,
		//							X_Daicho.FMonth5_DaichoKessanSeikyuNuki,
		//							X_Daicho.FMonth6_DaichoKessanSeikyuNuki,
		//							X_Daicho.FMonth7_DaichoKessanSeikyuNuki,
		//							X_Daicho.FMonth8_DaichoKessanSeikyuNuki,
		//							X_Daicho.FMonth9_DaichoKessanSeikyuNuki,
		//							X_Daicho.FMonth10_DaichoKessanSeikyuNuki,
		//							X_Daicho.FMonth11_DaichoKessanSeikyuNuki,
		//							X_Daicho.FMonth12_DaichoKessanSeikyuNuki
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Daicho].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.90.03
		///// <summary>
		///// 0.90.03更新メソッド。
		///// </summary>
		//static bool DataDB_09003(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	//Month0_SeikyuTotal	->	X_Out_ShunyuDaicho.FMonth0_DaichoKessanSeikyu,
		//	//Month1_SeikyuTotal	->	X_Out_ShunyuDaicho.FMonth1_DaichoKessanSeikyu,
		//	//Month2_SeikyuTotal	->	X_Out_ShunyuDaicho.FMonth2_DaichoKessanSeikyu,
		//	//Month3_SeikyuTotal	->	X_Out_ShunyuDaicho.FMonth3_DaichoKessanSeikyu,
		//	//Month4_SeikyuTotal	->	X_Out_ShunyuDaicho.FMonth4_DaichoKessanSeikyu,
		//	//Month5_SeikyuTotal	->	X_Out_ShunyuDaicho.FMonth5_DaichoKessanSeikyu,
		//	//Month6_SeikyuTotal	->	X_Out_ShunyuDaicho.FMonth6_DaichoKessanSeikyu,
		//	//Month7_SeikyuTotal	->	X_Out_ShunyuDaicho.FMonth7_DaichoKessanSeikyu,
		//	//Month8_SeikyuTotal	->	X_Out_ShunyuDaicho.FMonth8_DaichoKessanSeikyu,
		//	//Month9_SeikyuTotal	->	X_Out_ShunyuDaicho.FMonth9_DaichoKessanSeikyu,
		//	//Month10_SeikyuTotal	->	X_Out_ShunyuDaicho.FMonth10_DaichoKessanSeikyu,
		//	//Month11_SeikyuTotal	->	X_Out_ShunyuDaicho.FMonth11_DaichoKessanSeikyu,
		//	//Month12_SeikyuTotal	->	X_Out_ShunyuDaicho.FMonth12_DaichoKessanSeikyu,

		//	//Month0_SeikyuTotalNuki	->	X_Out_ShunyuDaicho.FMonth0_DaichoKessanSeikyuNuki,
		//	//Month1_SeikyuTotalNuki	->	X_Out_ShunyuDaicho.FMonth1_DaichoKessanSeikyuNuki,
		//	//Month2_SeikyuTotalNuki	->	X_Out_ShunyuDaicho.FMonth2_DaichoKessanSeikyuNuki,
		//	//Month3_SeikyuTotalNuki	->	X_Out_ShunyuDaicho.FMonth3_DaichoKessanSeikyuNuki,
		//	//Month4_SeikyuTotalNuki	->	X_Out_ShunyuDaicho.FMonth4_DaichoKessanSeikyuNuki,
		//	//Month5_SeikyuTotalNuki	->	X_Out_ShunyuDaicho.FMonth5_DaichoKessanSeikyuNuki,
		//	//Month6_SeikyuTotalNuki	->	X_Out_ShunyuDaicho.FMonth6_DaichoKessanSeikyuNuki,
		//	//Month7_SeikyuTotalNuki	->	X_Out_ShunyuDaicho.FMonth7_DaichoKessanSeikyuNuki,
		//	//Month8_SeikyuTotalNuki	->	X_Out_ShunyuDaicho.FMonth8_DaichoKessanSeikyuNuki,
		//	//Month9_SeikyuTotalNuki	->	X_Out_ShunyuDaicho.FMonth9_DaichoKessanSeikyuNuki,
		//	//Month10_SeikyuTotalNuki	->	X_Out_ShunyuDaicho.FMonth10_DaichoKessanSeikyuNuki,
		//	//Month11_SeikyuTotalNuki	->	X_Out_ShunyuDaicho.FMonth11_DaichoKessanSeikyuNuki,
		//	//Month12_SeikyuTotalNuki	->	X_Out_ShunyuDaicho.FMonth12_DaichoKessanSeikyuNuki

		//	try
		//    {												
		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} CURRENCY, Column {2} CURRENCY",
		//							TableProp.X_Daicho,
		//							X_Daicho.FDaichoKessanTotal,
		//							X_Daicho.FDaichoKessanTotalNuki	
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Daicho].Fields.Refresh();

		//		sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} TEXT(50), Column {2} YESNO",
		//							TableProp.X_Out_ShunyuDaicho,
		//							X_Out_ShunyuDaicho.FDaichoKessanTotalNuki,
		//							X_Out_ShunyuDaicho.Fchk_Daicho_Fld47_Length
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields.Refresh();

		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields[X_Out_ShunyuDaicho.FDaichoKessanTotalNuki].AllowZeroLength = true;				
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields.Refresh();

		//		// 名前を変更する。
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month0_SeikyuTotal"].Name	= X_Out_ShunyuDaicho.FMonth0_DaichoKessanSeikyu;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month1_SeikyuTotal"].Name	= X_Out_ShunyuDaicho.FMonth1_DaichoKessanSeikyu;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month2_SeikyuTotal"].Name	= X_Out_ShunyuDaicho.FMonth2_DaichoKessanSeikyu;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month3_SeikyuTotal"].Name	= X_Out_ShunyuDaicho.FMonth3_DaichoKessanSeikyu;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month4_SeikyuTotal"].Name	= X_Out_ShunyuDaicho.FMonth4_DaichoKessanSeikyu;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month5_SeikyuTotal"].Name	= X_Out_ShunyuDaicho.FMonth5_DaichoKessanSeikyu;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month6_SeikyuTotal"].Name	= X_Out_ShunyuDaicho.FMonth6_DaichoKessanSeikyu;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month7_SeikyuTotal"].Name	= X_Out_ShunyuDaicho.FMonth7_DaichoKessanSeikyu;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month8_SeikyuTotal"].Name	= X_Out_ShunyuDaicho.FMonth8_DaichoKessanSeikyu;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month9_SeikyuTotal"].Name	= X_Out_ShunyuDaicho.FMonth9_DaichoKessanSeikyu;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month10_SeikyuTotal"].Name	= X_Out_ShunyuDaicho.FMonth10_DaichoKessanSeikyu;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month11_SeikyuTotal"].Name	= X_Out_ShunyuDaicho.FMonth11_DaichoKessanSeikyu;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month12_SeikyuTotal"].Name	= X_Out_ShunyuDaicho.FMonth12_DaichoKessanSeikyu;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month0_SeikyuTotalNuki"].Name	= X_Out_ShunyuDaicho.FMonth0_DaichoKessanSeikyuNuki;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month1_SeikyuTotalNuki"].Name	= X_Out_ShunyuDaicho.FMonth1_DaichoKessanSeikyuNuki;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month2_SeikyuTotalNuki"].Name	= X_Out_ShunyuDaicho.FMonth2_DaichoKessanSeikyuNuki;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month3_SeikyuTotalNuki"].Name	= X_Out_ShunyuDaicho.FMonth3_DaichoKessanSeikyuNuki;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month4_SeikyuTotalNuki"].Name	= X_Out_ShunyuDaicho.FMonth4_DaichoKessanSeikyuNuki;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month5_SeikyuTotalNuki"].Name	= X_Out_ShunyuDaicho.FMonth5_DaichoKessanSeikyuNuki;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month6_SeikyuTotalNuki"].Name	= X_Out_ShunyuDaicho.FMonth6_DaichoKessanSeikyuNuki;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month7_SeikyuTotalNuki"].Name	= X_Out_ShunyuDaicho.FMonth7_DaichoKessanSeikyuNuki;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month8_SeikyuTotalNuki"].Name	= X_Out_ShunyuDaicho.FMonth8_DaichoKessanSeikyuNuki;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month9_SeikyuTotalNuki"].Name	= X_Out_ShunyuDaicho.FMonth9_DaichoKessanSeikyuNuki;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month10_SeikyuTotalNuki"].Name	= X_Out_ShunyuDaicho.FMonth10_DaichoKessanSeikyuNuki;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month11_SeikyuTotalNuki"].Name	= X_Out_ShunyuDaicho.FMonth11_DaichoKessanSeikyuNuki;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["Month12_SeikyuTotalNuki"].Name	= X_Out_ShunyuDaicho.FMonth12_DaichoKessanSeikyuNuki;
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields["SeikyuTotal"].Name				= X_Out_ShunyuDaicho.FDaichoKessanTotal;	
		//		db.TableDefs[TableProp.X_Out_ShunyuDaicho].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 0.96.01
		///// <summary>
		///// 0.96.01更新メソッド。
		///// </summary>
		//static bool DataDB_09601(DB moddb, DB seedb)
		//{

		//	// テーブルにデフォルトレコードを追加。
		//	try
		//	{
		//		DB		mdb	  = new DB(moddb.DBLocation, AppConst.DBPassword);
		//		DB		sdb   = new DB(seedb.DBLocation, AppConst.DBPassword);

		//		DBView	moddv = new DBView(new DBAdapter(mdb, TableProp.T_Kamoku));
		//		DBView  seedv = new DBView(new DBAdapter(sdb, TableProp.T_Kamoku));

		//		// 旧属性値から新属性値へ。
		//		for (int i = 0; i < moddv.Count; i++)
		//		{
		//			int oldId = Cast.Int(moddv[i][T_Kamoku.FKamoku_KbnZokusei]);
		//			switch (oldId)
		//			{
		//				case 6 :
		//				case 7 :
		//					moddv.BeginEdit();
		//					moddv[i][T_Kamoku.FKamoku_KbnZokusei] = oldId + 1;
		//					moddv.EndEdit();
		//					break;
		//			}
		//		}

		//		// 前受金を追加。
		//		seedv.RowFilterQuery(
		//			"{0} = {1}", 
		//			T_Kamoku.FKamoku_KbnZokusei, 
		//			(int)eZokusei.Maeukekin
		//			);
		//		if (seedv.Count == 0)
		//		{
		//			Debug.WriteLine("■初期値に追加するためのレコードが見つかりません。");
		//			return false;
		//		}

		//		DataRow newrow = moddv.NewRow();
		//		AppDb.CopyDataRow(seedv[0].Row, newrow);
		//		moddv.Add(newrow);

		//		moddv.Update();
		//	}
		//	catch(Exception ex)
		//	{
		//		Debug.WriteLine(ex);
		//		return false;
		//	}

		//    return true;
		//}
		//#endregion

		//#region >>> 0.96.01
		///// <summary>
		///// 0.97.02更新メソッド。
		///// </summary>
		//static bool DataDB_09702(DB moddb, DB seedb)
		//{

		//	// テーブルにデフォルトレコードを追加。
		//	try
		//	{
		//		DB		mdb	  = new DB(moddb.DBLocation, AppConst.DBPassword);
		//		DB		sdb   = new DB(seedb.DBLocation, AppConst.DBPassword);

		//		DBView	moddv = new DBView(new DBAdapter(mdb, TableProp.T_KeiyakuTekiyo));
		//		DBView  seedv = new DBView(new DBAdapter(sdb, TableProp.T_KeiyakuTekiyo));

		//		seedv.RowFilterQuery(
		//			"{0} = {1}",
		//			T_KeiyakuTekiyo.FKTeki_KbnTekiyo,
		//			(int)eTekiyo.Mishu);

		//		if (seedv.Count == 0)
		//		{
		//			Debug.WriteLine("■初期値に追加するためのレコードが見つかりません。");
		//			return false;
		//		}

		//		T_KeiyakuTekiyo donyu = new T_KeiyakuTekiyo(seedv[0]);

		//		moddv.RowFilterQuery(
		//			"{0} = {1}",
		//			T_KeiyakuTekiyo.FKTeki_KbnTekiyo,
		//			(int)eTekiyo.Mishu);
		//		// 旧属性値から新属性値へ。
		//		for (int i = 0; i < moddv.Count; i++)
		//		{
		//			T_KeiyakuTekiyo xrow = new T_KeiyakuTekiyo(moddv[i]);

		//			// 新しい名称に変更。
		//			xrow.KTeki_Name = donyu.KTeki_Name;
		//		}

		//		moddv.Update();
		//	}
		//	catch(Exception ex)
		//	{
		//		Debug.WriteLine(ex);
		//		return false;
		//	}

		//    return true;
		//}
		//#endregion

		//#region >>> 0.97.04
		///// <summary>
		///// 0.97.04更新メソッド。
		///// </summary>
		//static bool DataDB_09704(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//    {												
		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} CURRENCY, Column {2} CURRENCY",
		//							TableProp.X_Daicho,
		//							X_Daicho.FKeiyaku_GetsugakuSeikyu,
		//							X_Daicho.FKeiyaku_GetsugakuSeikyuNuki	
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.X_Daicho].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 1.00.02
		///// <summary>
		///// 1.00.02更新メソッド。
		///// </summary>
		//static bool DataDB_10002(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//    {												
		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} LONG",
		//							TableProp.T_Option,
		//							T_Option.FOpt_ShiwakeType
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.T_Option].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 1.00.03
		///// <summary>
		///// 1.00.03更新メソッド。
		///// </summary>
		//static bool DataDB_10003(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//    {												
		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} TEXT(255), Column {2} TEXT(255)",
		//							TableProp.T_Option,
		//							T_Option.FOpt_UchiwakeFilePath,
		//							T_Option.FOpt_UchiwakeType
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.T_Option].Fields.Refresh();

		//		db.TableDefs[TableProp.T_Option].Fields[T_Option.FOpt_UchiwakeFilePath].AllowZeroLength = true;				
		//		db.TableDefs[TableProp.T_Option].Fields[T_Option.FOpt_UchiwakeType].AllowZeroLength		= true;				
		//		db.TableDefs[TableProp.T_Option].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion


		//#region >>> 1.3.0
		///// <summary>
		///// 1.3.0更新メソッド。
		///// </summary>
		//static bool DataDB_10300(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//	{
		//		// DBLocation は mdb へのパス
		//		db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//		DBQuery dq = new DBQuery(
		//						"UPDATE {0} SET {1} = {2} WHERE {3} = {4} AND {1} = {5}",
		//						TableProp.T_KeiyakuTekiyo,
		//						T_KeiyakuTekiyo.FKTeki_KbnKazei,
		//						(int)eKazei.Kazei5,
		//						T_KeiyakuTekiyo.FKTeki_KbnTekiyo,
		//						(int)eTekiyo.Getsugaku,
		//						(int)eKazei.Kazei
		//						);

		//		db.Execute(dq.Sql, null);

		//		db.Close();
		//        db = null;

		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 1.3.7
		///// <summary>
		///// 1.3.7更新メソッド。
		///// </summary>
		//static bool DataDB_10307(DB moddb, DB seedb)
		//{
		//	try
		//	{
		//		DB		mdb	  = new DB(moddb.DBLocation, AppConst.DBPassword);
		//		DBView	dvs = new DBView(new DBAdapter(mdb, TableProp.T_Shohizei));

		//		DataRow row = dvs.NewRow();

		//		row[T_Shohizei.FSho_Zeiritsu]	= 8;
		//		row[T_Shohizei.FSho_From]		= new DateTime(2014, 4, 1);

		//		dvs.Add(row);

		//		if (dvs.HasChanges() == true)
		//		{
		//			dvs.Update();
		//		}

		//		mdb.DelAdapter(dvs.DBAdapter);
		//		mdb.Dispose();
		//		mdb = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//		_write_err(ex);

		//		return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 1.3.10
		///// <summary>
		///// 1.3.10更新メソッド。
		///// </summary>
		//static bool DataDB_10310(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//    {												
		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} LONG",
		//							TableProp.X_ZaimuShiwake,
		//							X_ZaimuShiwake.FZShiwake_Ritsu
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.T_Option].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}


		//#region >>> 1.3.14
		///// <summary>
		///// 1.3.14更新メソッド。
		///// </summary>
		//static bool DataDB_10314(DB moddb, DB seedb)
		//{
		//	try
		//	{
		//		DB		mdb	= new DB(moddb.DBLocation, AppConst.DBPassword);
		//		DBView	dvs	= new DBView(new DBAdapter(mdb, TableProp.T_Shohizei));

		//		dvs.SortQuery(T_Shohizei.FSho_Zeiritsu);

		//		if (dvs.FindRow(8) == false)
		//		{
		//			DataRow row = dvs.NewRow();

		//			row[T_Shohizei.FSho_Zeiritsu]	= 8;
		//			row[T_Shohizei.FSho_From]		= new DateTime(2014, 4, 1);

		//			dvs.Add(row);

		//			if (dvs.HasChanges() == true)
		//			{
		//				dvs.Update();
		//			}
		//		}

		//		mdb.DelAdapter(dvs.DBAdapter);
		//		mdb.Dispose();
		//		mdb = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//		_write_err(ex);

		//		return false;
		//    }

		//    return true;
		//}
		//#endregion
		//#endregion

		//#region >>> 1.3.22
		///// <summary>
		///// 1.3.22更新メソッド。
		///// </summary>
		//static bool DataDB_10322(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//    {												
		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} LONG",
		//							TableProp.T_User,
		//							T_User.FUsr_KbnShohizeiKani
		//							);

		//        db.Execute(sql, null);
		//        db.TableDefs[TableProp.T_Option].Fields.Refresh();

		//		db.Close();
		//        db = null;

		//		DBView dv = new DBView(moddb.AddAdapter(TableProp.T_User));

		//		if (dv.Count > 0)
		//		{
		//			// 簡易の場合だけ初期値に５種をセット
		//			if (Cast.Enumelate<eShohizeiKazei>(dv[0][T_User.FUsr_KbnShohizeiKazei], eShohizeiKazei.None) == eShohizeiKazei.Kani)
		//			{
		//				dv.BeginEdit(0);
		//				dv[T_User.FUsr_KbnShohizeiKani] = (int)eShohizeiKani.Kani5;
		//				dv.EndEdit();

		//				dv.Update();
		//			}
		//		}

		//		moddb.DelAdapter(dv.DBAdapter);

		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 1.5.2
		///// <summary>
		///// 1.5.2更新メソッド。
		///// </summary>
		//static bool DataDB_10502(DB moddb, DB seedb)
		//{
		//	try
		//	{
		//		// 遂に予定仕訳の管理を変更！最新の年度から2年のみ！

		//		DBView dvNendo = new DBView(moddb.AddAdapter(TableProp.T_UserNendo));

		//		dvNendo.RowFilterQuery("{0} = TRUE", T_UserNendo.FUsr_KbnNew);
		//		DateTime dtTo = DateTime.MinValue;

		//		if (dvNendo.Count > 0)
		//		{
		//			dtTo = Cast.DateTime(dvNendo[0][T_UserNendo.FUsr_DateTo]);
		//			dtTo = dtTo.AddYears(AppConst.DaichoDispLatestYear).AddMonths(1);
		//		}

		//		// 表示領域を超えた請求仕訳は全部消す
		//		string qry = DBQuery.GetSql(
		//							"DELETE * FROM {0} WHERE {1} > #{2}# AND {3} = {4}", 
		//							TableProp.T_Shiwake, 
		//							T_Shiwake.FShiwake_DateYM,
		//							dtTo,
		//							T_Shiwake.FShiwake_KbnNyuryoku,
		//							(int)eNyuryoku.Seikyu);

		//		moddb.ExecuteQuery(qry);

		//		moddb.DelAdapter(dvNendo.DBAdapter);
		//	}
		//	catch(Exception ex)
		//	{

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 1.5.3
		///// <summary>
		///// 1.5.3更新メソッド。
		///// </summary>
		//static bool DataDB_10503(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//    {												
		//        // DBLocation は mdb へのパス
		//        db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);				

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} MEMO",
		//							TableProp.T_Option,
		//							T_Option.FOpt_DaichoSearch
		//							);

		//        db.Execute(sql, null);
		//		// 空白文字を許可
		//        db.TableDefs[TableProp.T_Option].Fields[T_Option.FOpt_DaichoSearch].AllowZeroLength = true;
		//        db.TableDefs[TableProp.T_Option].Fields.Refresh();

		//		db.Close();
		//        db = null;
		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion

		//#region >>> 1.7.0
		///// <summary>
		///// 1.7.0更新メソッド。
		///// </summary>
		//static bool DataDB_10700(DB moddb, DB seedb)
		//{
		//	try
		//	{
		//		// 摘要の仕入税区を置換え
		//		string qry = DBQuery.GetSql(
		//							"UPDATE {0} SET {1} = {2} WHERE ({1} = {3}) AND ({4} = {5} OR {4}  = {6})",
		//							TableProp.T_KeiyakuTekiyo,
		//							T_KeiyakuTekiyo.FKTeki_KbnKazei,
		//							(int)eKazei.KazeiShiireForHikazei,
		//							(int)eKazei.Kazei,
		//							T_KeiyakuTekiyo.FKTeki_KbnTekiyo,
		//							(int)eTekiyo.Kojo,
		//							(int)eTekiyo.ShuzenFutan);

		//		moddb.ExecuteQuery(qry);

		//		// 現金科目の補助追加

		//		DBView dvkm = new DBView(moddb.AddAdapter(TableProp.T_Kamoku));

		//		if (dvkm.Count > 0)
		//		{
		//			T_Kamoku xrow = new T_Kamoku(dvkm[0]);

		//			xrow.Kamoku_NotHojo = false;

		//			if (dvkm.HasChanges() == true)
		//			{
		//				dvkm.Update();
		//			}
		//		}

		//		moddb.DelAdapter(dvkm.DBAdapter);
		//	}
		//	catch(Exception ex)
		//	{

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 1.7.4
		///// <summary>
		///// 1.7.4更新メソッド。
		///// </summary>
		//static bool DataDB_10704(DB moddb, DB seedb)
		//{
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//	{
		//		// DBLocation は mdb へのパス
		//		db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} LONG",
		//							TableProp.T_Keiyaku,
		//							T_Keiyaku.FKeiyaku_Fusen);

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.T_Keiyaku].Fields.Refresh();

		//		sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} LONG",
		//							TableProp.X_Daicho,
		//							X_Daicho.FKeiyaku_Fusen);

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.X_Daicho].Fields.Refresh();

		//		db.Close();
		//		db = null;
		//		}
		//	catch(Exception ex)
		//	{
		//		if (db != null)
		//		{
		//			db.Close();
		//			db = null;
		//		}

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 1.7.5
		///// <summary>
		///// 1.7.5更新メソッド。
		///// </summary>
		//static bool DataDB_10705(DB moddb, DB seedb)
		//{
		//	try
		//	{
		//		// 店舗と駐車場の税区を置換え
		//		string qry = DBQuery.GetSql(
		//							"UPDATE {0} SET {1} = {2} WHERE ({1} = {3}) AND ({7} = {8} OR {7} = {9}) AND ({4} = {5} OR {4}  = {6})",
		//							TableProp.T_KeiyakuTekiyo,
		//							T_KeiyakuTekiyo.FKTeki_KbnKazei,
		//							(int)eKazei.KazeiShiireForKazei,
		//							(int)eKazei.KazeiShiireForHikazei,
		//							T_KeiyakuTekiyo.FKTeki_KbnTekiyo,
		//							(int)eTekiyo.Kojo,
		//							(int)eTekiyo.ShuzenFutan,
		//							T_KeiyakuTekiyo.FKTeki_KbnShiyo,
		//							(int)eShiyo.Chushajo,
		//							(int)eShiyo.Tenpo);

		//		moddb.ExecuteQuery(qry);
		//	}
		//	catch(Exception ex)
		//	{

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 1.7.7
		///// <summary>
		///// 1.7.7更新メソッド。
		///// </summary>
		//static bool DataDB_10707(DB moddb, DB seedb)
		//{
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//	{
		//		// DBLocation は mdb へのパス
		//		db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} DATETIME",
		//							TableProp.T_User,
		//							T_User.FUsr_DateToLatest);

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.T_User].Fields.Refresh();

		//		db.Close();
		//		db = null;

		//		// DBの処理年度

		//		DBView dvUser	= new DBView(moddb.AddAdapter(TableProp.T_User));
		//		DBView dvNendo	= new DBView(moddb.AddAdapter(TableProp.T_UserNendo));

		//		if (dvUser.Count > 0)
		//		{
		//			T_User xrow = new T_User(dvUser[0]);

		//			dvNendo.RowFilterQuery("{0} = TRUE", T_UserNendo.FUsr_KbnNew);

		//			if (dvNendo.Count > 0)
		//			{
		//				T_UserNendo nrow = new T_UserNendo(dvNendo[0]);

		//				xrow.Usr_DateToLatest = nrow.Usr_DateTo;
		//			}

		//			if (dvUser.HasChanges() == true)
		//			{
		//				dvUser.Update();
		//			}
		//		}

		//		moddb.DelAdapter(dvUser.DBAdapter);
		//		moddb.DelAdapter(dvNendo.DBAdapter);

		//	}
		//	catch(Exception ex)
		//	{
		//		if (db != null)
		//		{
		//			db.Close();
		//			db = null;
		//		}

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 1.8.3
		///// <summary>
		///// 1.8.3更新メソッド。
		///// </summary>
		//static bool DataDB_10803(DB moddb, DB seedb)
		//{
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//	{
		//		// DBLocation は mdb へのパス
		//		db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} DATETIME",
		//							TableProp.T_User,
		//							T_User.FUsr_DaichoLastUpdate);

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.T_User].Fields.Refresh();

		//		DBView dvUser		= new DBView(moddb.AddAdapter(TableProp.T_User));
		//		DBView dvShiwake	= new DBView(moddb.AddAdapter(TableProp.T_Shiwake));
		//		dvShiwake.SortQuery("{0} DESC", T_Shiwake.FLastUpdate);

		//		if (dvUser.Count > 0 && dvShiwake.Count > 0)
		//		{
		//			dvUser.BeginEdit(0);
		//			dvUser[T_User.FUsr_DaichoLastUpdate] = dvShiwake[0][T_Shiwake.FLastUpdate];
		//			dvUser.EndEdit();

		//			if (dvUser.HasChanges() == true)
		//			{
		//				dvUser.Update();
		//			}
		//		}

		//		moddb.DelAdapter(dvUser.DBAdapter);
		//		moddb.DelAdapter(dvShiwake.DBAdapter);

		//	}
		//	catch(Exception ex)
		//	{
		//		if (db != null)
		//		{
		//			db.Close();
		//			db = null;
		//		}

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 1.8.5
		///// <summary>
		///// 1.8.5更新メソッド。
		///// </summary>
		//static bool DataDB_10805(DB moddb, DB seedb)
		//{
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//	{
		//		// DBLocation は mdb へのパス
		//		db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} CURRENCY, Column {2} CURRENCY",
		//							TableProp.X_Daicho,
		//							X_Daicho.FAzukariKessanTotal,
		//							X_Daicho.FAzukariKessanTotalNuki);

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.X_Daicho].Fields.Refresh();
		//	}
		//	catch(Exception ex)
		//	{
		//		if (db != null)
		//		{
		//			db.Close();
		//			db = null;
		//		}

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 1.8.6
		///// <summary>
		///// 1.8.6更新メソッド。
		///// </summary>
		//static bool DataDB_10806(DB moddb, DB seedb)
		//{
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//	{
		//		// DBLocation は mdb へのパス
		//		db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} CURRENCY, Column {2} CURRENCY",
		//							TableProp.X_Daicho,
		//							X_Daicho.FReiKenKoKessanTotal,
		//							X_Daicho.FReiKenKoKessanTotalNuki);

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.X_Daicho].Fields.Refresh();
		//	}
		//	catch(Exception ex)
		//	{
		//		if (db != null)
		//		{
		//			db.Close();
		//			db = null;
		//		}

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 1.8.7
		///// <summary>
		///// 1.8.7更新メソッド。
		///// </summary>
		//static bool DataDB_10807(DB moddb, DB seedb)
		//{
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//	{
		//		// DBLocation は mdb へのパス
		//		db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} CURRENCY",
		//							TableProp.X_Daicho,
		//							X_Daicho.FNyukinTotal_Month13);

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.X_Daicho].Fields.Refresh();
		//	}
		//	catch(Exception ex)
		//	{
		//		if (db != null)
		//		{
		//			db.Close();
		//			db = null;
		//		}

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 1.8.8
		///// <summary>
		///// 1.8.8更新メソッド。
		///// </summary>
		//static bool DataDB_10808(DB moddb, DB seedb)
		//{
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//	{
		//		// DBLocation は mdb へのパス
		//		db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} YESNO",
		//							TableProp.T_Option,
		//							T_Option.FOpt_DaichoDispTotalNyukin12);

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.T_Option].Fields.Refresh();
		//	}
		//	catch(Exception ex)
		//	{
		//		if (db != null)
		//		{
		//			db.Close();
		//			db = null;
		//		}

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 1.8.9
		///// <summary>
		///// 1.8.9更新メソッド。
		///// </summary>
		//static bool DataDB_10809(DB moddb, DB seedb)
		//{
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//	{
		//		// DBLocation は mdb へのパス
		//		db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} YESNO",
		//							TableProp.T_Option,
		//							T_Option.FOpt_DaichoInputSingleClick);

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.T_Option].Fields.Refresh();
		//	}
		//	catch(Exception ex)
		//	{
		//		if (db != null)
		//		{
		//			db.Close();
		//			db = null;
		//		}

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 1.9.0
		///// <summary>
		///// 1.9.0更新メソッド。
		///// </summary>
		//static bool DataDB_10900(DB moddb, DB seedb)
		//{
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//	{
		//		// DBLocation は mdb へのパス
		//		db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//		// フィールド追加
		//		string sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} DATETIME",
		//							TableProp.T_Shiwake,
		//							T_Shiwake.FShiwake_DateExportMaker);

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.T_Shiwake].Fields.Refresh();

		//		// フィールド追加
		//		sql = DBQuery.GetSql(
		//							"ALTER TABLE {0} Add Column {1} DATETIME, Column {2} DATETIME",
		//							TableProp.T_Option,
		//							T_Option.FOpt_ShiwakeDateFrom,
		//							T_Option.FOpt_ShiwakeDateTo);

		//		db.Execute(sql, null);
		//		db.TableDefs[TableProp.T_Option].Fields.Refresh();
		//	}
		//	catch(Exception ex)
		//	{
		//		if (db != null)
		//		{
		//			db.Close();
		//			db = null;
		//		}

		//		_write_err(ex);

		//		return false;
		//	}

		//	return true;
		//}
		//#endregion

		//#region >>> 1.9.1
		///// <summary>
		///// 1.9.1更新メソッド。
		///// </summary>
		//static bool DataDB_10901(DB moddb, DB seedb)
		//{		
		//	DAO.Database	db = null;
		//	DAO.DBEngine	de = new DAO.DBEngine();
		//	DAO.Workspace	ws = de.Workspaces[0];

		//	try
		//	{
		//		// DBLocation は mdb へのパス
		//		db = ws.OpenDatabase(moddb.DBLocation, null, null, "MS Access;PWD=" + AppConst.DBPassword);

		//		DBQuery dq = new DBQuery(
		//						"UPDATE {0} SET {1} = {2} WHERE {3} = {4} AND ({1} = {5} OR {1} IS NULL)",
		//						TableProp.T_KeiyakuTekiyo,			// 0
		//						T_KeiyakuTekiyo.FKTeki_KbnKazei,	// 1
		//						(int)eKazei.Kazei8,					// 2
		//						T_KeiyakuTekiyo.FKTeki_KbnTekiyo,	// 3
		//						(int)eTekiyo.Getsugaku,				// 4
		//						(int)eKazei.Kazei);					// 5

		//		db.Execute(dq.Sql, null);

		//		db.Close();
		//        db = null;

		//	 }
		//	catch(Exception ex)
		//    {
		//        if (db != null)
		//        {
		//            db.Close();
		//            db = null;
		//        }

		//        _write_err(ex);

		//        return false;
		//    }

		//    return true;
		//}
		//#endregion
	}
}


