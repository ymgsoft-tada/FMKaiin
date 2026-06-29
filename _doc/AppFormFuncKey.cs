
//
// ※このプログラムはSrcMakerForApplicationFuncKeyにより自動的に生成されました。(K.Tada)
//
// Inport File :
//		D:\client\DotNet4.6\Itoshokai\Chokuso\_doc\AppFuncKey.xlsx
// Template File :
//		D:\client\DotNet4.6\Itoshokai\Chokuso\_doc\AppFormFuncKey.cs.template
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

	#region *** FuncMasterTorihikisaki ***
	/// <summary>
	/// FuncMasterTorihikisakiのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterTorihikisaki
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

	#region *** FuncMasterTorihikisaki_DlgEntry ***
	/// <summary>
	/// FuncMasterTorihikisaki_DlgEntryのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterTorihikisaki_DlgEntry
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

	#region *** FuncMasterKyokyuUkeire ***
	/// <summary>
	/// FuncMasterKyokyuUkeireのファンクションキー定義クラス
	/// </summary>
	public class FuncMasterKyokyuUkeire
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


}

