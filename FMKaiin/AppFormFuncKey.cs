
//
// ※このプログラムはSrcMakerForApplicationFuncKeyにより自動的に生成されました。(K.Tada)
//
// Inport File :
//		D:\client\DotNet4.6_YMGLib5\FujisawaIshikai\FMKaiin\_doc\AppFuncKey.xlsx
// Template File :
//		D:\client\DotNet4.6_YMGLib5\FujisawaIshikai\FMKaiin\_doc\AppFormFuncKey.cs.template
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ComponentSUI;

namespace App
{

	#region *** FuncMasterBasic ***
	/// <summary>
	/// FuncMasterBasicのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterBasic
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F5,	
					"銀行情報",	
					""),
			new FuncKeyDefine(
					Keys.F11,	
					"登録",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 銀行情報
		/// </summary>
		public readonly static FuncKeyDefine ShowBank = Functions[0];
		/// <summary>
		/// 登録
		/// </summary>
		public readonly static FuncKeyDefine Save = Functions[1];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[2];
	}
	#endregion

	#region *** FuncSelectorDateYM ***
	/// <summary>
	/// FuncSelectorDateYMのファンクションキー定義クラス
	/// </summary>
	public class FuncSelectorDateYM
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"選択実行",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 選択実行
		/// </summary>
		public readonly static FuncKeyDefine Exec = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncCalendarInfo ***
	/// <summary>
	/// FuncCalendarInfoのファンクションキー定義クラス
	/// </summary>
	public class FuncCalendarInfo
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"登録",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 登録
		/// </summary>
		public readonly static FuncKeyDefine Save = Functions[0];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[1];
	}
	#endregion

	#region *** FuncMasterBank ***
	/// <summary>
	/// FuncMasterBankのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterBank
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F2,	
					"追加",	
					""),
			new FuncKeyDefine(
					Keys.F3,	
					"訂正",	
					""),
			new FuncKeyDefine(
					Keys.F4,	
					"削除",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 追加
		/// </summary>
		public readonly static FuncKeyDefine RowAdd = Functions[0];
		/// <summary>
		/// 訂正
		/// </summary>
		public readonly static FuncKeyDefine RowEdit = Functions[1];
		/// <summary>
		/// 削除
		/// </summary>
		public readonly static FuncKeyDefine RowDelete = Functions[2];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[3];
	}
	#endregion

	#region *** FuncMasterBank_DlgEntry ***
	/// <summary>
	/// FuncMasterBank_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterBank_DlgEntry
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F5,	
					"銀行コード選択",	
					""),
			new FuncKeyDefine(
					Keys.F11,	
					"登録",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 銀行コード選択
		/// </summary>
		public readonly static FuncKeyDefine ShowBankCode = Functions[0];
		/// <summary>
		/// 登録
		/// </summary>
		public readonly static FuncKeyDefine Save = Functions[1];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[2];
	}
	#endregion

	#region *** FuncMasterTanto ***
	/// <summary>
	/// FuncMasterTantoのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterTanto
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F2,	
					"追加",	
					""),
			new FuncKeyDefine(
					Keys.F3,	
					"訂正",	
					""),
			new FuncKeyDefine(
					Keys.F4,	
					"削除",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 追加
		/// </summary>
		public readonly static FuncKeyDefine RowAdd = Functions[0];
		/// <summary>
		/// 訂正
		/// </summary>
		public readonly static FuncKeyDefine RowEdit = Functions[1];
		/// <summary>
		/// 削除
		/// </summary>
		public readonly static FuncKeyDefine RowDelete = Functions[2];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[3];
	}
	#endregion

	#region *** FuncMasterTanto_DlgEntry ***
	/// <summary>
	/// FuncMasterTanto_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterTanto_DlgEntry
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F5,	
					"パスワード変更",	
					""),
			new FuncKeyDefine(
					Keys.F11,	
					"登録",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// パスワード変更
		/// </summary>
		public readonly static FuncKeyDefine ChangePWD = Functions[0];
		/// <summary>
		/// 登録
		/// </summary>
		public readonly static FuncKeyDefine Save = Functions[1];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[2];
	}
	#endregion

	#region *** FuncMasterTanto_DlgPwd ***
	/// <summary>
	/// FuncMasterTanto_DlgPwdのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterTanto_DlgPwd
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"登録",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 登録
		/// </summary>
		public readonly static FuncKeyDefine Save = Functions[0];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[1];
	}
	#endregion

	#region *** FuncCreateBankFile ***
	/// <summary>
	/// FuncCreateBankFileのファンクションキー定義クラス
	/// </summary>
	public class FuncCreateBankFile
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"作成実行",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 作成実行
		/// </summary>
		public readonly static FuncKeyDefine Exec = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncSelectorBankCode ***
	/// <summary>
	/// FuncSelectorBankCodeのファンクションキー定義クラス
	/// </summary>
	public class FuncSelectorBankCode
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F2,	
					"検索クリア",	
					""),
			new FuncKeyDefine(
					Keys.F11,	
					"選択実行",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 検索クリア
		/// </summary>
		public readonly static FuncKeyDefine Clear = Functions[0];
		/// <summary>
		/// 選択実行
		/// </summary>
		public readonly static FuncKeyDefine Exec = Functions[1];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[2];
	}
	#endregion

	#region *** FuncMasterBankCode ***
	/// <summary>
	/// FuncMasterBankCodeのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterBankCode
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F2,	
					"追加",	
					""),
			new FuncKeyDefine(
					Keys.F3,	
					"訂正",	
					""),
			new FuncKeyDefine(
					Keys.F4,	
					"削除",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 追加
		/// </summary>
		public readonly static FuncKeyDefine RowAdd = Functions[0];
		/// <summary>
		/// 訂正
		/// </summary>
		public readonly static FuncKeyDefine RowEdit = Functions[1];
		/// <summary>
		/// 削除
		/// </summary>
		public readonly static FuncKeyDefine RowDelete = Functions[2];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[3];
	}
	#endregion

	#region *** FuncMasterBankCode_DlgEntry ***
	/// <summary>
	/// FuncMasterBankCode_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterBankCode_DlgEntry
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"登録",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 登録
		/// </summary>
		public readonly static FuncKeyDefine Save = Functions[0];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[1];
	}
	#endregion

	#region *** FuncMasterKaihi ***
	/// <summary>
	/// FuncMasterKaihiのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterKaihi
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F2,	
					"追加",	
					""),
			new FuncKeyDefine(
					Keys.F3,	
					"訂正",	
					""),
			new FuncKeyDefine(
					Keys.F4,	
					"削除",	
					""),
			new FuncKeyDefine(
					Keys.F5,	
					"コピー追加",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 追加
		/// </summary>
		public readonly static FuncKeyDefine RowAdd = Functions[0];
		/// <summary>
		/// 訂正
		/// </summary>
		public readonly static FuncKeyDefine RowEdit = Functions[1];
		/// <summary>
		/// 削除
		/// </summary>
		public readonly static FuncKeyDefine RowDelete = Functions[2];
		/// <summary>
		/// コピー追加
		/// </summary>
		public readonly static FuncKeyDefine CopyAdd = Functions[3];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[4];
	}
	#endregion

	#region *** FuncMasterKaihi_DlgEntry ***
	/// <summary>
	/// FuncMasterKaihi_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterKaihi_DlgEntry
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"登録",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 登録
		/// </summary>
		public readonly static FuncKeyDefine Save = Functions[0];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[1];
	}
	#endregion

	#region *** FuncMasterIryoKikan_DlgEntry ***
	/// <summary>
	/// FuncMasterIryoKikan_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterIryoKikan_DlgEntry
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"登録",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 登録
		/// </summary>
		public readonly static FuncKeyDefine Save = Functions[0];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[1];
	}
	#endregion

	#region *** FuncMasterIryoKikan ***
	/// <summary>
	/// FuncMasterIryoKikanのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterIryoKikan
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F2,	
					"追加",	
					""),
			new FuncKeyDefine(
					Keys.F3,	
					"訂正",	
					""),
			new FuncKeyDefine(
					Keys.F4,	
					"削除",	
					""),
			new FuncKeyDefine(
					Keys.F9,	
					"印刷プレビュー",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 追加
		/// </summary>
		public readonly static FuncKeyDefine RowAdd = Functions[0];
		/// <summary>
		/// 訂正
		/// </summary>
		public readonly static FuncKeyDefine RowEdit = Functions[1];
		/// <summary>
		/// 削除
		/// </summary>
		public readonly static FuncKeyDefine RowDelete = Functions[2];
		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public readonly static FuncKeyDefine Preview = Functions[3];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[4];
	}
	#endregion

	#region *** FuncMasterShinryokamoku_DlgEntry ***
	/// <summary>
	/// FuncMasterShinryokamoku_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterShinryokamoku_DlgEntry
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"登録",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 登録
		/// </summary>
		public readonly static FuncKeyDefine Save = Functions[0];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[1];
	}
	#endregion

	#region *** FuncMasterShinryokamoku ***
	/// <summary>
	/// FuncMasterShinryokamokuのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterShinryokamoku
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F2,	
					"追加",	
					""),
			new FuncKeyDefine(
					Keys.F3,	
					"訂正",	
					""),
			new FuncKeyDefine(
					Keys.F4,	
					"削除",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 追加
		/// </summary>
		public readonly static FuncKeyDefine RowAdd = Functions[0];
		/// <summary>
		/// 訂正
		/// </summary>
		public readonly static FuncKeyDefine RowEdit = Functions[1];
		/// <summary>
		/// 削除
		/// </summary>
		public readonly static FuncKeyDefine RowDelete = Functions[2];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[3];
	}
	#endregion

	#region *** FuncKaiin ***
	/// <summary>
	/// FuncKaiinのファンクションキー定義クラス
	/// </summary>
	public class FuncKaiin
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F2,	
					"追加",	
					""),
			new FuncKeyDefine(
					Keys.F3,	
					"訂正",	
					""),
			new FuncKeyDefine(
					Keys.F4,	
					"削除",	
					""),
			new FuncKeyDefine(
					Keys.F5,	
					"コピー追加",	
					""),
			new FuncKeyDefine(
					Keys.F6,	
					"再入会",	
					""),
			new FuncKeyDefine(
					Keys.F7,	
					"MAMIS取込",	
					""),
			new FuncKeyDefine(
					Keys.F8,	
					"会費調整",	
					""),
			new FuncKeyDefine(
					Keys.F9,	
					"印刷プレビュー",	
					""),
			new FuncKeyDefine(
					Keys.F10,	
					"在籍期間表示",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 追加
		/// </summary>
		public readonly static FuncKeyDefine RowAdd = Functions[0];
		/// <summary>
		/// 訂正
		/// </summary>
		public readonly static FuncKeyDefine RowEdit = Functions[1];
		/// <summary>
		/// 削除
		/// </summary>
		public readonly static FuncKeyDefine RowDelete = Functions[2];
		/// <summary>
		/// コピー追加
		/// </summary>
		public readonly static FuncKeyDefine CopyAdd = Functions[3];
		/// <summary>
		/// 再入会
		/// </summary>
		public readonly static FuncKeyDefine Rejoin = Functions[4];
		/// <summary>
		/// MAMIS取込
		/// </summary>
		public readonly static FuncKeyDefine ShowMamis = Functions[5];
		/// <summary>
		/// 会費調整
		/// </summary>
		public readonly static FuncKeyDefine ShowChosei = Functions[6];
		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public readonly static FuncKeyDefine Preview = Functions[7];
		/// <summary>
		/// 在籍期間表示
		/// </summary>
		public readonly static FuncKeyDefine ShowZaiseki = Functions[8];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[9];
	}
	#endregion

	#region *** FuncKaiin_DlgEntry ***
	/// <summary>
	/// FuncKaiin_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncKaiin_DlgEntry
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F5,	
					"情報1表示",	
					""),
			new FuncKeyDefine(
					Keys.F6,	
					"情報2表示",	
					""),
			new FuncKeyDefine(
					Keys.F7,	
					"情報3表示",	
					""),
			new FuncKeyDefine(
					Keys.F11,	
					"登録",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 情報1表示
		/// </summary>
		public readonly static FuncKeyDefine ShowTab1 = Functions[0];
		/// <summary>
		/// 情報2表示
		/// </summary>
		public readonly static FuncKeyDefine ShowTab2 = Functions[1];
		/// <summary>
		/// 情報3表示
		/// </summary>
		public readonly static FuncKeyDefine ShowTab3 = Functions[2];
		/// <summary>
		/// 登録
		/// </summary>
		public readonly static FuncKeyDefine Save = Functions[3];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[4];
	}
	#endregion

	#region *** FuncKaiin_DlgEntry_Idoinfo ***
	/// <summary>
	/// FuncKaiin_DlgEntry_Idoinfoのファンクションキー定義クラス
	/// </summary>
	public class FuncKaiin_DlgEntry_Idoinfo
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"登録",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 登録
		/// </summary>
		public readonly static FuncKeyDefine Save = Functions[0];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[1];
	}
	#endregion

	#region *** FuncHikiotoshi ***
	/// <summary>
	/// FuncHikiotoshiのファンクションキー定義クラス
	/// </summary>
	public class FuncHikiotoshi
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F5,	
					"作成実行",	
					""),
			new FuncKeyDefine(
					Keys.F6,	
					"個別再作成",	
					""),
			new FuncKeyDefine(
					Keys.F8,	
					"編集",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 作成実行
		/// </summary>
		public readonly static FuncKeyDefine Exec = Functions[0];
		/// <summary>
		/// 個別再作成
		/// </summary>
		public readonly static FuncKeyDefine ReMake = Functions[1];
		/// <summary>
		/// 編集
		/// </summary>
		public readonly static FuncKeyDefine Edit = Functions[2];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[3];
	}
	#endregion

	#region *** FuncHikiotoshi_DlgEntry ***
	/// <summary>
	/// FuncHikiotoshi_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncHikiotoshi_DlgEntry
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F2,	
					"追加",	
					""),
			new FuncKeyDefine(
					Keys.F4,	
					"削除",	
					""),
			new FuncKeyDefine(
					Keys.F11,	
					"登録",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 追加
		/// </summary>
		public readonly static FuncKeyDefine RowAdd = Functions[0];
		/// <summary>
		/// 削除
		/// </summary>
		public readonly static FuncKeyDefine RowDelete = Functions[1];
		/// <summary>
		/// 登録
		/// </summary>
		public readonly static FuncKeyDefine Save = Functions[2];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[3];
	}
	#endregion

	#region *** FuncOutIryoKikan ***
	/// <summary>
	/// FuncOutIryoKikanのファンクションキー定義クラス
	/// </summary>
	public class FuncOutIryoKikan
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F9,	
					"印刷プレビュー",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public readonly static FuncKeyDefine Preview = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutKaihiUchiwakeYear ***
	/// <summary>
	/// FuncOutKaihiUchiwakeYearのファンクションキー定義クラス
	/// </summary>
	public class FuncOutKaihiUchiwakeYear
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F9,	
					"印刷プレビュー",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public readonly static FuncKeyDefine Preview = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutKaihiListBank ***
	/// <summary>
	/// FuncOutKaihiListBankのファンクションキー定義クラス
	/// </summary>
	public class FuncOutKaihiListBank
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F9,	
					"印刷プレビュー",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public readonly static FuncKeyDefine Preview = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncKaiin_DlgHistory ***
	/// <summary>
	/// FuncKaiin_DlgHistoryのファンクションキー定義クラス
	/// </summary>
	public class FuncKaiin_DlgHistory
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[0];
	}
	#endregion

	#region *** FuncOutKaihiYoteiYear ***
	/// <summary>
	/// FuncOutKaihiYoteiYearのファンクションキー定義クラス
	/// </summary>
	public class FuncOutKaihiYoteiYear
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F9,	
					"印刷プレビュー",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public readonly static FuncKeyDefine Preview = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutKaihiList ***
	/// <summary>
	/// FuncOutKaihiListのファンクションキー定義クラス
	/// </summary>
	public class FuncOutKaihiList
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F9,	
					"印刷プレビュー",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public readonly static FuncKeyDefine Preview = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutKaihiUchiwakeMonth ***
	/// <summary>
	/// FuncOutKaihiUchiwakeMonthのファンクションキー定義クラス
	/// </summary>
	public class FuncOutKaihiUchiwakeMonth
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F9,	
					"印刷プレビュー",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public readonly static FuncKeyDefine Preview = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncCreateIfaxFile ***
	/// <summary>
	/// FuncCreateIfaxFileのファンクションキー定義クラス
	/// </summary>
	public class FuncCreateIfaxFile
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"作成実行",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 作成実行
		/// </summary>
		public readonly static FuncKeyDefine Exec = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncKaiin_DlgChosei ***
	/// <summary>
	/// FuncKaiin_DlgChoseiのファンクションキー定義クラス
	/// </summary>
	public class FuncKaiin_DlgChosei
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F2,	
					"追加",	
					""),
			new FuncKeyDefine(
					Keys.F4,	
					"削除",	
					""),
			new FuncKeyDefine(
					Keys.F11,	
					"登録",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 追加
		/// </summary>
		public readonly static FuncKeyDefine RowAdd = Functions[0];
		/// <summary>
		/// 削除
		/// </summary>
		public readonly static FuncKeyDefine RowDelete = Functions[1];
		/// <summary>
		/// 登録
		/// </summary>
		public readonly static FuncKeyDefine Save = Functions[2];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[3];
	}
	#endregion

	#region *** FuncExcelMenu ***
	/// <summary>
	/// FuncExcelMenuのファンクションキー定義クラス
	/// </summary>
	public class FuncExcelMenu
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[0];
	}
	#endregion

	#region *** FuncOutExcelShimingyoinKaihi ***
	/// <summary>
	/// FuncOutExcelShimingyoinKaihiのファンクションキー定義クラス
	/// </summary>
	public class FuncOutExcelShimingyoinKaihi
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"Excel出力",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// Excel出力
		/// </summary>
		public readonly static FuncKeyDefine OutExcel = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutExcelNenkanChoshu ***
	/// <summary>
	/// FuncOutExcelNenkanChoshuのファンクションキー定義クラス
	/// </summary>
	public class FuncOutExcelNenkanChoshu
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"Excel出力",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// Excel出力
		/// </summary>
		public readonly static FuncKeyDefine OutExcel = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutExcelKaihiShinsei ***
	/// <summary>
	/// FuncOutExcelKaihiShinseiのファンクションキー定義クラス
	/// </summary>
	public class FuncOutExcelKaihiShinsei
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"Excel出力",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// Excel出力
		/// </summary>
		public readonly static FuncKeyDefine OutExcel = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutExcelFurikomi ***
	/// <summary>
	/// FuncOutExcelFurikomiのファンクションキー定義クラス
	/// </summary>
	public class FuncOutExcelFurikomi
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"Excel出力",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// Excel出力
		/// </summary>
		public readonly static FuncKeyDefine OutExcel = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutExcelNofusho ***
	/// <summary>
	/// FuncOutExcelNofushoのファンクションキー定義クラス
	/// </summary>
	public class FuncOutExcelNofusho
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"Excel出力",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// Excel出力
		/// </summary>
		public readonly static FuncKeyDefine OutExcel = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutExcelFurikomiIrai ***
	/// <summary>
	/// FuncOutExcelFurikomiIraiのファンクションキー定義クラス
	/// </summary>
	public class FuncOutExcelFurikomiIrai
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"Excel出力",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// Excel出力
		/// </summary>
		public readonly static FuncKeyDefine OutExcel = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutHikiotoshiKakunin ***
	/// <summary>
	/// FuncOutHikiotoshiKakuninのファンクションキー定義クラス
	/// </summary>
	public class FuncOutHikiotoshiKakunin
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F9,	
					"印刷プレビュー",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public readonly static FuncKeyDefine Preview = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutSeikyuMinyukin ***
	/// <summary>
	/// FuncOutSeikyuMinyukinのファンクションキー定義クラス
	/// </summary>
	public class FuncOutSeikyuMinyukin
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F9,	
					"印刷プレビュー",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public readonly static FuncKeyDefine Preview = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutChosyuMeisaiYear ***
	/// <summary>
	/// FuncOutChosyuMeisaiYearのファンクションキー定義クラス
	/// </summary>
	public class FuncOutChosyuMeisaiYear
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F9,	
					"印刷プレビュー",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public readonly static FuncKeyDefine Preview = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutIkaiHikiotoshiMeisai ***
	/// <summary>
	/// FuncOutIkaiHikiotoshiMeisaiのファンクションキー定義クラス
	/// </summary>
	public class FuncOutIkaiHikiotoshiMeisai
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F9,	
					"印刷プレビュー",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public readonly static FuncKeyDefine Preview = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncOutIkaiHikiotoshiShukei ***
	/// <summary>
	/// FuncOutIkaiHikiotoshiShukeiのファンクションキー定義クラス
	/// </summary>
	public class FuncOutIkaiHikiotoshiShukei
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F9,	
					"印刷プレビュー",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public readonly static FuncKeyDefine Preview = Functions[0];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[1];
	}
	#endregion

	#region *** FuncMamisImport ***
	/// <summary>
	/// FuncMamisImportのファンクションキー定義クラス
	/// </summary>
	public class FuncMamisImport
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F9,	
					"履歴",	
					""),
			new FuncKeyDefine(
					Keys.F11,	
					"取込開始",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 履歴
		/// </summary>
		public readonly static FuncKeyDefine History = Functions[0];
		/// <summary>
		/// 取込開始
		/// </summary>
		public readonly static FuncKeyDefine Exec = Functions[1];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[2];
	}
	#endregion

	#region *** FuncMamisImport_DlgConfirm ***
	/// <summary>
	/// FuncMamisImport_DlgConfirmのファンクションキー定義クラス
	/// </summary>
	public class FuncMamisImport_DlgConfirm
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F11,	
					"実行",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 実行
		/// </summary>
		public readonly static FuncKeyDefine Exec = Functions[0];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[1];
	}
	#endregion

	#region *** FuncMamisImport_DlgLog ***
	/// <summary>
	/// FuncMamisImport_DlgLogのファンクションキー定義クラス
	/// </summary>
	public class FuncMamisImport_DlgLog
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[0];
	}
	#endregion


}

