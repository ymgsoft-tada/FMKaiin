
//
// ※このプログラムはSrcMakerForApplicationFuncKeyにより自動的に生成されました。(K.Tada)
//
// Inport File :
//		D:\client\DotNet4.6_YMGLib5\FujisawaIshikai\FMKyuyo\_doc\AppFuncKey.xlsx
// Template File :
//		D:\client\DotNet4.6_YMGLib5\FujisawaIshikai\FMKyuyo\_doc\AppFormFuncKey.cs.template
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

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
					Keys.F8,	
					"マイナンバーパスワード変更",	
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
		/// マイナンバーパスワード変更
		/// </summary>
		public readonly static FuncKeyDefine ShowMynoPWD = Functions[1];
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

	#region *** FuncMasterShokumu ***
	/// <summary>
	/// FuncMasterShokumuのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterShokumu
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

	#region *** FuncMasterShokumu_DlgEntry ***
	/// <summary>
	/// FuncMasterShokumu_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterShokumu_DlgEntry
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

	#region *** FuncMasterTeate ***
	/// <summary>
	/// FuncMasterTeateのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterTeate
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

	#region *** FuncMasterTeate_DlgEntry ***
	/// <summary>
	/// FuncMasterTeate_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterTeate_DlgEntry
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

	#region *** FuncMasterKinmuTanka ***
	/// <summary>
	/// FuncMasterKinmuTankaのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterKinmuTanka
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

	#region *** FuncMasterKinmuTanka_DlgEntry ***
	/// <summary>
	/// FuncMasterKinmuTanka_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterKinmuTanka_DlgEntry
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

	#region *** FuncStaff ***
	/// <summary>
	/// FuncStaffのファンクションキー定義クラス
	/// </summary>
	public class FuncStaff
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
					Keys.F7,	
					"提出先マスター",	
					""),
			new FuncKeyDefine(
					Keys.F9,	
					"一覧表印刷",	
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
		/// 提出先マスター
		/// </summary>
		public readonly static FuncKeyDefine ShowTeishutsusaki = Functions[3];
		/// <summary>
		/// 一覧表印刷
		/// </summary>
		public readonly static FuncKeyDefine Print = Functions[4];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[5];
	}
	#endregion

	#region *** FuncStaff_DlgEntry ***
	/// <summary>
	/// FuncStaff_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncStaff_DlgEntry
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
					Keys.F8,	
					"マイナンバー管理",	
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
		/// マイナンバー管理
		/// </summary>
		public readonly static FuncKeyDefine ShowMyno = Functions[1];
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

	#region *** FuncKinmubo_DlgSelector ***
	/// <summary>
	/// FuncKinmubo_DlgSelectorのファンクションキー定義クラス
	/// </summary>
	public class FuncKinmubo_DlgSelector
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F5,	
					"選択実行",	
					""),
			new FuncKeyDefine(
					Keys.F8,	
					"CSVインポート",	
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
		/// CSVインポート
		/// </summary>
		public readonly static FuncKeyDefine ShowImportCSV = Functions[1];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[2];
	}
	#endregion

	#region *** FuncKinmubo ***
	/// <summary>
	/// FuncKinmuboのファンクションキー定義クラス
	/// </summary>
	public class FuncKinmubo
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F1,	
					"摘要辞書",	
					""),
			new FuncKeyDefine(
					Keys.F2,	
					"休診追加",	
					""),
			new FuncKeyDefine(
					Keys.F3,	
					"休診訂正",	
					""),
			new FuncKeyDefine(
					Keys.F4,	
					"休診削除",	
					""),
			new FuncKeyDefine(
					Keys.F5,	
					"事業手当追加",	
					""),
			new FuncKeyDefine(
					Keys.F6,	
					"事業手当訂正",	
					""),
			new FuncKeyDefine(
					Keys.F7,	
					"事業手当削除",	
					""),
			new FuncKeyDefine(
					Keys.F9,	
					"月度手当追加",	
					""),
			new FuncKeyDefine(
					Keys.F10,	
					"月度手当訂正",	
					""),
			new FuncKeyDefine(
					Keys.F11,	
					"月度手当削除",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// 摘要辞書
		/// </summary>
		public readonly static FuncKeyDefine ShowTekiyo = Functions[0];
		/// <summary>
		/// 休診追加
		/// </summary>
		public readonly static FuncKeyDefine AddKyushin = Functions[1];
		/// <summary>
		/// 休診訂正
		/// </summary>
		public readonly static FuncKeyDefine EditKyushin = Functions[2];
		/// <summary>
		/// 休診削除
		/// </summary>
		public readonly static FuncKeyDefine DeleteKyushin = Functions[3];
		/// <summary>
		/// 事業手当追加
		/// </summary>
		public readonly static FuncKeyDefine AddTeate = Functions[4];
		/// <summary>
		/// 事業手当訂正
		/// </summary>
		public readonly static FuncKeyDefine EditTeate = Functions[5];
		/// <summary>
		/// 事業手当削除
		/// </summary>
		public readonly static FuncKeyDefine DeleteTeate = Functions[6];
		/// <summary>
		/// 月度手当追加
		/// </summary>
		public readonly static FuncKeyDefine AddTeateMonth = Functions[7];
		/// <summary>
		/// 月度手当訂正
		/// </summary>
		public readonly static FuncKeyDefine EditTeateMonth = Functions[8];
		/// <summary>
		/// 月度手当削除
		/// </summary>
		public readonly static FuncKeyDefine DeleteTeateMoth = Functions[9];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[10];
	}
	#endregion

	#region *** FuncKnmubo_DlgEntryTeate ***
	/// <summary>
	/// FuncKnmubo_DlgEntryTeateのファンクションキー定義クラス
	/// </summary>
	public class FuncKnmubo_DlgEntryTeate
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

	#region *** FuncKnmubo_DlgEntryKyushin ***
	/// <summary>
	/// FuncKnmubo_DlgEntryKyushinのファンクションキー定義クラス
	/// </summary>
	public class FuncKnmubo_DlgEntryKyushin
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F5,	
					"摘要選択",	
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
		/// 摘要選択
		/// </summary>
		public readonly static FuncKeyDefine ShowTekiyo = Functions[0];
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

	#region *** FuncMasterGensen ***
	/// <summary>
	/// FuncMasterGensenのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterGensen
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

	#region *** FuncFixedKyuyo ***
	/// <summary>
	/// FuncFixedKyuyoのファンクションキー定義クラス
	/// </summary>
	public class FuncFixedKyuyo
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
					Keys.F11,	
					"作成実行",	
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
		/// 作成実行
		/// </summary>
		public readonly static FuncKeyDefine Exec = Functions[1];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[2];
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

	#region *** FuncSelectorTekiyo ***
	/// <summary>
	/// FuncSelectorTekiyoのファンクションキー定義クラス
	/// </summary>
	public class FuncSelectorTekiyo
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

	#region *** FuncMasterTekiyo ***
	/// <summary>
	/// FuncMasterTekiyoのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterTekiyo
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

	#region *** FuncMasterTekiyo_DlgEntry ***
	/// <summary>
	/// FuncMasterTekiyo_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterTekiyo_DlgEntry
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

	#region *** FuncMasterJigyo ***
	/// <summary>
	/// FuncMasterJigyoのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterJigyo
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

	#region *** FuncMasterJigyo_DlgEntry ***
	/// <summary>
	/// FuncMasterJigyo_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterJigyo_DlgEntry
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

	#region *** FuncOutKyuyoMeisai ***
	/// <summary>
	/// FuncOutKyuyoMeisaiのファンクションキー定義クラス
	/// </summary>
	public class FuncOutKyuyoMeisai
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

	#region *** FuncOutTeateCheckList ***
	/// <summary>
	/// FuncOutTeateCheckListのファンクションキー定義クラス
	/// </summary>
	public class FuncOutTeateCheckList
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

	#region *** FuncOutKinmuCheckList ***
	/// <summary>
	/// FuncOutKinmuCheckListのファンクションキー定義クラス
	/// </summary>
	public class FuncOutKinmuCheckList
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
					Keys.F10,	
					"CSV出力",	
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
		/// CSV出力
		/// </summary>
		public readonly static FuncKeyDefine ExportCSV = Functions[1];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[2];
	}
	#endregion

	#region *** FuncStaff_DlgPrint ***
	/// <summary>
	/// FuncStaff_DlgPrintのファンクションキー定義クラス
	/// </summary>
	public class FuncStaff_DlgPrint
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
					Keys.F10,	
					"CSV出力",	
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
		/// CSV出力
		/// </summary>
		public readonly static FuncKeyDefine ExportCSV = Functions[1];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[2];
	}
	#endregion

	#region *** FuncStaff_DlgMyno ***
	/// <summary>
	/// FuncStaff_DlgMynoのファンクションキー定義クラス
	/// </summary>
	public class FuncStaff_DlgMyno
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

	#region *** FuncMyno_DlgEntryPWD ***
	/// <summary>
	/// FuncMyno_DlgEntryPWDのファンクションキー定義クラス
	/// </summary>
	public class FuncMyno_DlgEntryPWD
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

	#region *** FuncKinmubo_DlgImportCSV ***
	/// <summary>
	/// FuncKinmubo_DlgImportCSVのファンクションキー定義クラス
	/// </summary>
	public class FuncKinmubo_DlgImportCSV
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F5,	
					"インポート実行",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"キャンセル",	
					""),
		};

		/// <summary>
		/// インポート実行
		/// </summary>
		public readonly static FuncKeyDefine Exec = Functions[0];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[1];
	}
	#endregion

	#region *** FuncOutGenchoshuhyo ***
	/// <summary>
	/// FuncOutGenchoshuhyoのファンクションキー定義クラス
	/// </summary>
	public class FuncOutGenchoshuhyo
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

	#region *** FuncMasterTeishutsusaki ***
	/// <summary>
	/// FuncMasterTeishutsusakiのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterTeishutsusaki
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

	#region *** FuncMasterTeishutsusaki_DlgEntry ***
	/// <summary>
	/// FuncMasterTeishutsusaki_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterTeishutsusaki_DlgEntry
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F5,	
					"市区町村コード選択",	
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
		/// 市区町村コード選択
		/// </summary>
		public readonly static FuncKeyDefine ShowShikuchosonCode = Functions[0];
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

	#region *** FuncSelectorShikuchosonCode ***
	/// <summary>
	/// FuncSelectorShikuchosonCodeのファンクションキー定義クラス
	/// </summary>
	public class FuncSelectorShikuchosonCode
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
					"キャンセル",	
					""),
		};

		/// <summary>
		/// 選択実行
		/// </summary>
		public readonly static FuncKeyDefine Exec = Functions[0];
		/// <summary>
		/// キャンセル
		/// </summary>
		public readonly static FuncKeyDefine Cancel = Functions[1];
	}
	#endregion

	#region *** FuncOutGenchoshuhyoFile ***
	/// <summary>
	/// FuncOutGenchoshuhyoFileのファンクションキー定義クラス
	/// </summary>
	public class FuncOutGenchoshuhyoFile
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F9,	
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

	#region *** FuncNenchoData ***
	/// <summary>
	/// FuncNenchoDataのファンクションキー定義クラス
	/// </summary>
	public class FuncNenchoData
	{
		/// <summary>
		/// ファンクションキー定義。
		/// </summary>
		public readonly static FuncKeyDefine[]	Functions =
		{
			new FuncKeyDefine(
					Keys.F2,	
					"その他年調入力",	
					""),
			new FuncKeyDefine(
					Keys.F9,	
					"CSV出力",	
					""),
			new FuncKeyDefine(
					Keys.F12,	
					"閉じる",	
					""),
		};

		/// <summary>
		/// その他年調入力
		/// </summary>
		public readonly static FuncKeyDefine AddNencho = Functions[0];
		/// <summary>
		/// CSV出力
		/// </summary>
		public readonly static FuncKeyDefine OutCSV = Functions[1];
		/// <summary>
		/// 閉じる
		/// </summary>
		public readonly static FuncKeyDefine Close = Functions[2];
	}
	#endregion

	#region *** FuncNenchoDataList ***
	/// <summary>
	/// FuncNenchoDataListのファンクションキー定義クラス
	/// </summary>
	public class FuncNenchoDataList
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

	#region *** FuncNenchoDataDlg ***
	/// <summary>
	/// FuncNenchoDataDlgのファンクションキー定義クラス
	/// </summary>
	public class FuncNenchoDataDlg
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


}

