using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrapeCity.Win.Bars;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// ファンクションキー情報を定義します。（一つ分）
	/// </summary>
	public class FuncKeyDefine
	{
		/// <summary>
		/// ファンクションキーが押された時に動作するメソッドのハンドラです。
		/// </summary>
		public delegate void ExecuteHandler();
		
		/// <summary>
		/// ファンクションキーに表示される名前です。
		/// </summary>
		public readonly string	Name;
		/// <summary>
		/// ファンクションキーに表示される説明です。
		/// </summary>
		public readonly string	Description;
		/// <summary>
		/// ファンクションキーで押すと反応するキーです。
		/// </summary>
		public readonly Keys	Key;
		/// <summary>
		/// ファンクションキーが押された時に動作するメソッドです。
		/// </summary>
		public ExecuteHandler	Execute;
		
		/// <summary>
		/// ファンクションキーデータ定義用の内部実行コンストラクタです。
		/// </summary>
		/// <param name="key">キー</param>
		/// <param name="name">表示される名称</param>
		/// <param name="description">説明</param>
		public FuncKeyDefine(Keys key, string name, string description)
		{
			Key	 = key;
			Name = name;
			Description = description;
		}
	}

	/// <summary>
	/// [作成者 K.Tada]
	/// アプリケーションで使用するファンクションキーを制御するクラスです。
	/// </summary>
	public class AppFunctionKey
	{
		#region *** Private Value ***
		/// <summary>
		/// 全てのファンクションキー情報。
		/// </summary>
		FuncKeyDefine[]	funcs;
		#endregion

		#region *** Private Value ***
		/// <summary>
		/// ファンクションキー
		/// </summary>
		public GcFunctionKey	Function
		{
			get
			{
				return func;
			}
		}
		GcFunctionKey func;
		#endregion

		/// <summary>
		/// キャンセル用のイベント引数
		/// </summary>
		public class CancelEventArgs : EventArgs
		{
			/// <summary></summary>
			public bool Cancel;

			/// <summary></summary>
			public Keys Key;

			/// <summary>
			/// コンストラクタ
			/// </summary>
			public CancelEventArgs()
			{
				this.Cancel = false;
				this.Key	= Keys.None;
			}
		}

		/// <summary>
		/// キャンセルイベント用
		/// </summary>
		public delegate void BeforeKeyDownHandler(object obj, CancelEventArgs e);
		
		/// <summary>
		/// ファンクション押下前の処理
		/// </summary>
		public event BeforeKeyDownHandler BeforeKeyDown;

		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="_func">ファンクションキーのオブジェクト</param>
		[Obsolete("現在サポートされてないコンストラクタです。")]
		public AppFunctionKey(GcFunctionKey _func)
		{
			func  = _func;
			func.FunctionKeyButtons.Clear();
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="_func">ファンクションキーのオブジェクト</param>
		/// <param name="_funcs">全てのファンクションキー情報。</param>
		public AppFunctionKey(GcFunctionKey _func, FuncKeyDefine[] _funcs)
		{
			func  = _func;
			funcs = _funcs;
			func.FunctionKeyButtons.Clear();

			//■ フォントの統一
			func.Font = new System.Drawing.Font("メイリオ", 10f);
			//■ 左端のグリップを非表示
			func.GripStyle = ToolStripGripStyle.Hidden;
			//■ Ctlキーでショートカットキーを表示させない。
			func.CombinationKeyDisplayMode = CombinationKeyDisplayMode.Always;

			func.ShowItemToolTips = false;

			func.RenderMode = ToolStripRenderMode.Professional;
			//func.RenderMode = ToolStripRenderMode.System;

			bool f10 = false;

			foreach (FuncKeyDefine fkd in funcs)
			{
				AddFunctionButton(fkd.Name, fkd.Description, fkd.Key);

				if (fkd.Key == Keys.F10)
				{
					f10 = true;
				}
			}

			if (f10 == false)
			{
				AddDummyButton(Keys.F10);
			}
		}
		#endregion

		#region *** Public Method ***
		/// <summary>
		/// ファンクションキーが押された時に登録されたメソッドを実行します。
		/// </summary>
		/// <param name="key">押されたキー</param>
		public void Exec(Keys key)
		{
			if (funcs == null || func.Enabled == false)
			{
				return;
			}
			
			foreach (FuncKeyDefine fkd in funcs)
			{
				if (fkd.Key == key)
				{
					if (fkd.Execute != null)
					{
						CancelEventArgs args = new CancelEventArgs();
						args.Key = key;

						if (BeforeKeyDown != null)
						{
							BeforeKeyDown(this, args);
						}

						if (args.Cancel == false)
						{
							fkd.Execute();
						}
					}
				}
			}
		}

		/// <summary>
		/// ファンクションボタンを追加します。
		/// </summary>
		/// <param name="title">タイトル</param>
		/// <param name="tips">Tipsに表示する文字列</param>
		/// <param name="key">キーコード</param>
		public void AddFunctionButton(string title, string tips, Keys key)
		{
			FunctionKeyButton btn = new FunctionKeyButton();
			btn.Text = title;
			btn.ToolTipText = tips;
			btn.FunctionKey = key;
			btn.Enabled = true;

			btn.Margin = new Padding(3, 1, 3, 2);

			func.FunctionKeyButtons.Add(btn);
		}

		/// <summary>
		/// Windows標準のショートカットを殺すためのダミーボタンを追加します。
		/// </summary>
		/// <param name="key">キーコード</param>
		public void AddDummyButton(Keys key)
		{
			FunctionKeyButton btn = new FunctionKeyButton();
			btn.Text = "";
			btn.ToolTipText = "";
			btn.FunctionKey = key;
			btn.Visible = false;
			btn.Enabled = true;
			btn.Size = new System.Drawing.Size(0, 0);

			btn.Margin = new Padding(0, 0, 0, 0);

			func.FunctionKeyButtons.Add(btn);	
		}

		/// <summary>
		/// 指定したタイトルの使用可否を設定します。
		/// </summary>
		/// <param name="title">タイトル</param>
		/// <param name="visible">表示可否</param>
		public void SetVisible(string title, bool visible)
		{
			FunctionKeyButton btn = GetFunctionButton(title);

			if (btn != null)
			{
				btn.Visible = visible;
			}
		}

		/// <summary>
		/// 指定したキーコードの使用可否を設定します。
		/// </summary>
		/// <param name="key">キーコード</param>
		/// <param name="visible">表示可否</param>
		public void SetVisible(Keys key, bool visible)
		{
			FunctionKeyButton btn = GetFunctionButton(key);
			
			if (btn != null)
			{
				btn.Visible = visible;
			}
		}
		
		/// <summary>
		/// 指定したタイトルの使用可否を設定します。
		/// </summary>
		/// <param name="title">タイトル</param>
		/// <param name="enable">使用可否</param>
		public void SetEnabled(string title, bool enable)
		{
			FunctionKeyButton btn = GetFunctionButton(title);

			if (btn != null)
			{
				btn.Enabled = enable;
			}
		}

		/// <summary>
		/// 指定したキーコードの使用可否を設定します。
		/// </summary>
		/// <param name="key">キーコード</param>
		/// <param name="enable">使用可否</param>
		public void SetEnabled(Keys key, bool enable)
		{
			FunctionKeyButton btn = GetFunctionButton(key);

			if (btn != null)
			{
				btn.Enabled = enable;
			}
		}

		/// <summary>
		/// 指定されたキーのタイトルを返します。
		/// </summary>
		/// <param name="key">キーコード</param>
		/// <returns>該当するファンクションキータイトル</returns>
		public string GetFunctionButtonTitle(Keys key)
		{
			string str = "";

			FunctionKeyButton btn = GetFunctionButton(key);

			if (btn != null)
			{
				str = btn.Text;
			}

			return str;
		}

		/// <summary>
		/// 指定されたキーのボタンを返します。
		/// </summary>
		/// <param name="key">キーコード</param>
		/// <returns>ファンクションボタン</returns>
		public FunctionKeyButton GetFunctionButton(Keys key)
		{
			foreach (FunctionKeyButton btn in func.FunctionKeyButtons)
			{
				if (btn.FunctionKey == key)
				{
					return btn;
				}
			}

			return null;
		}

		/// <summary>
		/// 指定されたタイトルのボタンを返します。
		/// </summary>
		/// <param name="title">タイトル</param>
		/// <returns>ファンクションボタン</returns>
		public FunctionKeyButton GetFunctionButton(string title)
		{
			foreach (FunctionKeyButton btn in func.FunctionKeyButtons)
			{
				if (btn.Text == title)
				{
					return btn;
				}
			}

			return null;
		}
		#endregion
	}
}
