using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentForm;
using GControlGcComboBoxEx;
using GControlGcDateEx;
using C1.Win.C1TrueDBGrid;
using ComponentRegistry;
using ComponentIO;
using ComponentGControlDB;

namespace App
{
	/// <summary>
	/// [作成者 K.Tada]
	/// フォームのスーパークラス
	/// </summary>
	public partial class FormFrame : Form
	{
		#region *** API ***
		/// <summary>
		/// MaxWindowTrackSize以上のサイズにするためのAPI関数。
		/// http://dobon.net/vb/dotnet/form/minimumsize.html
		/// を参考。
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="X"></param>
		/// <param name="Y"></param>
		/// <param name="nWidth"></param>
		/// <param name="nHeight"></param>
		/// <param name="bRepaint"></param>
		/// <returns></returns>
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern bool MoveWindow(
										IntPtr hWnd,
										int X,
										int Y,
										int nWidth,
										int nHeight,
										bool bRepaint);
		#endregion

		#region *** Protected Value ***
		/// <summary>
		/// フォームレイアウト
		/// </summary>
		protected FormLayout layout;
		/// <summary>
		/// ツールチップ
		/// </summary>
		protected AppToolTip appToolTip = null;
		/// <summary>
		/// ファンクションキー
		/// </summary>
		protected AppFunctionKey appFuncKey = null;
		/// <summary>
		/// フォームが閉じられた時の理由
		/// </summary>
		protected FormCloseReason formCloseReason = FormCloseReason.None;

		/// <summary>
		/// 初期表示位置を継承クラス側で固定するか
		/// ※継承先のコンストラクタで指定すること
		/// </summary>
		protected bool startPositionFixed = false;
		#endregion

		#region *** Private Value ***
		/// <summary>
		/// ダイアログフォームとして表示するかを取得・設定します。ダイアログフォームでない場合は、FormBorderStyle = Noneが指定されます。
		/// </summary>
		bool isDialogForm = true;
		#endregion

		#region *** Property ***
		/// <summary>
		/// ダイアログフォームとして表示するかを取得・設定します。ダイアログフォームでない場合は、FormBorderStyle = Noneが指定されます。
		/// </summary>
		[Description("ダイアログフォームとして表示するかを取得・設定します。ダイアログフォームでない場合は、FormBorderStyle = Noneが指定されます。")]
		public bool IsDialogForm
		{
			get { return isDialogForm; }
			set 
			{ 
				isDialogForm = value; 
				setFormStyle();
			}

		}

		/// <summary>
		/// 描画時のチラツキを抑える為のマスク処理をするか取得・設定します。
		/// </summary>
		public bool ShownMask
		{
			get { return isMask; }
			set { isMask = value; }
		}
		bool isMask = true;

		/// <summary>
		/// フォームが閉じられた理由を取得します。
		/// </summary>
		public FormCloseReason FormCloseReason
		{
			get { return formCloseReason; }
		}

		/// <summary>
		/// ファンクションキー操作時に、ファンクションにフォーカスを遷移させるかどうか
		/// </summary>
		public bool SendFocusForFunckey
		{
			get { return sendFocusForFunckey; }
			set { sendFocusForFunckey = value; }
		}
		bool sendFocusForFunckey = true;

		/// <summary>
		/// エンターキーによるフォーカス遷移
		/// </summary>
		public bool EnterFocus { get; set; }
		#endregion

		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormFrame()
		{
			initialize();
		}

		void initialize()
		{
			InitializeComponent();

			EnterFocus = true;

			this.Shown += new EventHandler(FormFrame_Shown);
			this.Load += new EventHandler(FormFrame_Load);
			this.Disposed += new EventHandler(FormFrame_Disposed);
			this.FormClosing += new FormClosingEventHandler(FormFrame_FormClosing);
		}
		#endregion

		#region *** Protected Event ***
		/// <summary>
		/// 解放処理
		/// </summary>
		protected virtual void FormFrame_Disposed(object sender, EventArgs e)
		{
			this.BindingContext = null;
		}
		/// <summary>
		/// ロード処理
		/// </summary>
		protected virtual void FormFrame_Load(object sender, EventArgs e)
		{
			// チラつき防止用マスク
			pnlMask.BringToFront();
			pnlMask.Visible = isMask;
			//■ フォームスタイルを設定
			setFormStyle();

			// ツールチップの初期化
			appToolTip = new AppToolTip(this);

			//■ 継承先の固定位置で表示
			if (startPositionFixed == true)
			{
				return;
			}

			if (isDialogForm == false)
			{
				// フォーム内フォーム以降の処理はしない
				return;
			}

			this.BackColor = AppConst.FormBackColor;

			// ファンクションの実行
			SetFunction();

			// 初期表示ポジション
			this.StartPosition = FormStartPosition.CenterParent;

			////■ フォーム位置をレジストリから復帰
			//RegControl.PopXYWH((Control)this);

			//// 幅・高さについては解像度以上のサイズ指定されているかもしれないので、 個別で対応する。
			//object w = RegControl.Pop(this, "_w");
			//if (w != null)
			//{
			//	SetWidthUnlimited(Cast.Int(w));
			//}
			//object h = RegControl.Pop(this, "_h");
			//if (h != null)
			//{
			//	SetHeightUnlimited(Cast.Int(h));
			//}

			//// ウィンドウ状態を反映
			//this.WindowState = AppRegistry.ReadFormStateEnum<FormWindowState>(this, AppConst.RegWindowState, FormWindowState.Normal);
		}
	
		/// <summary>
		/// フォームが閉じられる時の処理
		/// </summary>
		protected virtual void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{

			//■ 解放しきれてないメモリを強制解放。
			//   (不必要なldbファイルの解放。）
			GC.Collect();

			if (isDialogForm == false)
			{
				// フォーム内フォームなら処理しない
				return;
			}
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected virtual void SetFunction()
		{
		}
		#endregion

		#region *** Event ***
		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected virtual void FormFrame_Shown(object sender, EventArgs e)
		{
			// キーダウンイベントの登録
			this.KeyPreview = true;
			this.KeyDown	+= new KeyEventHandler(FormFrame_KeyDown);
			this.KeyPress	+= new KeyPressEventHandler(FormFrame_KeyPress);

			if (appFuncKey != null)
			{
				appFuncKey.Function.FunctionKeyDown += Function_FunctionKeyDown;
			}

			this.PerformAutoScale();

			// 最後にマスクを外す
			pnlMask.Visible = false;
		}

		/// <summary>
		/// ファンクションキー操作
		/// </summary>
		void Function_FunctionKeyDown(object sender, KeyEventArgs e)
		{
			if (appFuncKey != null)
			{
				appFuncKey.Exec(e.KeyCode);
			}

			e.Handled = true;
		}

		/// <summary>
		/// キーダウン処理
		/// </summary>
		protected virtual void FormFrame_KeyDown(object sender, KeyEventArgs e)
		{
			Form frm = (Form)sender;
			
			//■ ファンクションキーの標準ショートカットの抑止
			e.Handled = Patcher_CheckShortcutForKillingWindowsAction(e.KeyCode, e.Alt);

			if (CheckEnabledFormKeyPreview(e.KeyCode) == true)
			{
				return;
			}

			if (frm.ActiveControl == null)
			{
				return;
			}

			if (EnterFocus == true)
			{
				FormCommon.EnterFocusKeyDown(sender, e);
			}
		}

		/// <summary>
		/// ファンクションキーの押下処理
		/// </summary>
		protected virtual void FormFrame_FunctionKeyDown(object sender, KeyEventArgs e)
		{
			// アクティブフォームでなければ無視。
            if (this != Form.ActiveForm)
			{
				return;
			}
			
			if (appFuncKey != null)
			{
				if (sendFocusForFunckey == true)
				{
					// フォーカスを FunctionKey に。
					if (appFuncKey.Function != null)
					{
						appFuncKey.Function.Select();
					}
				}
				
				// ファンクション処理の実行。
				appFuncKey.Exec(e.KeyCode);
			}
		}

		/// <summary>
		/// ファンクションキーによるWindows標準ショートカット機能を抑止するかチェックします。
		/// </summary>
		/// <param name="keycode">キーコード</param>
		/// <param name="alt">Altキー</param>
		/// <returns></returns>
		public bool Patcher_CheckShortcutForKillingWindowsAction(Keys keycode, bool alt)
		{
			bool	handled = false;

			if (isDialogForm == false) 
			{
				// フォーム内フォームは無視。
				return handled;
			}
			
			if ((keycode == Keys.F4 && alt == true) ||
				(keycode == Keys.F10))
			{
				// Alt+F4とF10キーは無効とする。
				handled  = true;
			}

			return handled;
		}
		
		/// <summary>
		/// フォームでキープレビューしても問題ないか調査します。
		/// </summary>
		/// <returns></returns>
		public bool CheckEnabledFormKeyPreview(Keys keycode)
		{
			bool	handled = false;
			
			switch (keycode)
			{
				case Keys.Escape :
					// ?
					//if (this.ActiveControl is C1TrueDBGrid)
					//{
					//    // GGridDB に委ねる。
					//}
					break;
				case Keys.Enter :
					//if (this.ActiveControl is UcTableComboBox)
					//{
					//	UcTableComboBox	c = (UcTableComboBox)this.ActiveControl;
						
					//	if (c.DroppedDown == true)	handled = true;
					//}
					//else 
					if (this.ActiveControl is GcComboBoxEx)
					{
						GcComboBoxEx		c = (GcComboBoxEx)this.ActiveControl;
						
						if (c.DroppedDown == true)	handled = true;
					}
					else if (this.ActiveControl is GcDateEx)
					{
						GcDateEx		c = (GcDateEx)this.ActiveControl;
						
						if (c.DroppedDown == true)	handled = true;
					}
					break;
				case Keys.Up :
				case Keys.Down :
					if (this.ActiveControl is C1TrueDBGrid)
					{
						// GGridDB に委ねる。
					}
					else
					{
						//if (this.ActiveControl is UcTableComboBox)
						//{
						//	// ComboEx は上下で項目変更。
						//	handled = true;
						//}
						//else 
						if (this.ActiveControl is GcComboBoxEx)
						{
							// ComboEx は上下で項目変更。
							handled = true;
						}
						else
						if (this.ActiveControl is GcDateEx)
						{
							// DateEx は上下で日付変更。
							handled = true;
						}
					}
					break;
				default :
					break;
			}
			
			return handled;
		}

	
		/// <summary>
		/// キープレス処理
		/// </summary>
		protected virtual void FormFrame_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (EnterFocus == true)
			{
				FormCommon.EnterFocusKeyPress(sender, e);
			}
		}
		#endregion

		#region *** Private Method ***
		/// <summary>
		/// フォームのスタイルを設定します。
		/// </summary>
		void setFormStyle()
		{
			// デザイン時に変更さるとプロパティが不正となるので。。。
			if (DesignMode == false)
			{
				// トップレベルウィンドウの指定
				this.TopLevel = isDialogForm;
			}

			if (isDialogForm == true)
			{
				this.StartPosition = FormStartPosition.Manual;
				if (DesignMode == true)
				{
					// デザインモードならダイアログフォームを表示
					this.FormBorderStyle = FormBorderStyle.FixedSingle;
				}
			}
			else
			{
				// フォーム内フォームを想定
				this.FormBorderStyle = FormBorderStyle.None;
				this.StartPosition = FormStartPosition.Manual;
			}
		}

		/// <summary>
		/// カレントコントロールの内容をＤＢへ反映します。
		/// </summary>
		/// <param name="gc">ＤＢとの通信クラス</param>
		protected void updateCurrentControlValue(GControlDB gc)
		{
			if (gc != null)
			{
				Control ctl = this.ActiveControl;

				if (ctl is UcTableComboBox)
				{
					ctl = ((UcTableComboBox)ctl).ComboBox;
				}
				else
				if (ctl is UcDate)
				{
					ctl = ((UcDate)ctl).UcDateCtl;
				}

				if (ctl != null)
				{
					gc.UpdateByControl(ctl);
				}
			}
		}

		#endregion

		#region *** Public Method ***
		/// <summary>
		/// MaxWindowTrackSizeに関わらず、指定の高さでフォームを描画します。
		/// </summary>
		/// <param name="height">高さ</param>
		public void SetHeightUnlimited(int height)
		{
			Rectangle rec = new Rectangle(
									this.Left,
									this.Top,
									this.Width,
									height);

			SetBoundsUnlimited(rec);
		}

		/// <summary>
		/// MaxWindowTrackSizeに関わらず、指定の幅でフォームを描画します。
		/// </summary>
		/// <param name="width">幅</param>
		public void SetWidthUnlimited(int width)
		{
			Rectangle rec = new Rectangle(
									this.Left,
									this.Top,
									width,
									this.Height);

			SetBoundsUnlimited(rec);
		}

		/// <summary>
		/// MaxWindowTrackSizeに関わらず、指定の位置・サイズでフォームを描画します。
		/// </summary>
		/// <param name="rec">指定位置・サイズ</param>
		public void SetBoundsUnlimited(Rectangle rec)
		{			
			MoveWindow(
				this.Handle,
				rec.X, 
				rec.Y,
				rec.Width, 
				rec.Height, 
				true);

			this.UpdateBounds();
		}

		/// <summary>
		/// 外部からの登録処理を実行します。
		/// </summary>
		/// <returns>エラー：false</returns>
		public virtual bool SaveData()
		{
			return true;
		}
		#endregion

		/// <summary>
		/// Windowsショートカットの抑制
		/// </summary>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.F10)
			{
				// メニューストリップが表示されている場合、F10で勝手にフォーカス移動させない。
				return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}
	}

	/// <summary>
	/// フォームが閉じられた時の理由
	/// </summary>
	public enum FormCloseReason
	{	
		/// <summary>
		/// なし（未初期化）
		/// </summary>
		None,
		/// <summary>
		/// 閉じる。
		/// </summary>
		Close,
		/// <summary>
		/// 登録
		/// </summary>
		Save,
		/// <summary>
		/// キャンセル
		/// </summary>
		Cancel,
		/// <summary>
		/// クローズ前になんらかのエラーが発生（2010/4/6 fj）
		/// </summary>
		Error,
		/// <summary>
		/// 実行
		/// </summary>
		Exec,
	}

}
