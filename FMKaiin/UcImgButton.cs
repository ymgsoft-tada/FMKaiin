using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using ComponentDebug;
using ComponentForm;

namespace App
{
	/// <summary>
	/// Imageを用いて表現されるボタンです。
	/// </summary>
	public partial class UcImgButton : UserControl
	{
		#region *** Events ***
		/// <summary>
		/// ボタンをクリックし（、離し）た時に起こるイベントハンドラです。
		/// </summary>
		public delegate void ClickEventHandler(object sender, System.ComponentModel.CancelEventArgs e);
		/// <summary>
		/// ボタンをクリックし、離した時に起こるイベントです。
		/// </summary>
		public event ClickEventHandler	ClickEvent	= null;
		#endregion
		
		#region *** Private Value ***
		/// <summary>
		/// ボタンにアクションがあったときの文字カラー
		/// </summary>
		static Color			ColorDefault	= Color.Black;
		static Color			ColorOver		= Color.DimGray;
		static Color			ColorPush		= Color.FromArgb(255,128,0);
		static Color			ColorPull		= Color.Blue;
		/// <summary>
		/// 通常イメージ。
		/// </summary>
		Image					img_default		= null;
		/// <summary>
		/// マウスオーバー時のイメージ。
		/// </summary>
		Image					img_mouseover	= null;
		/// <summary>
		/// ボタンを押した時のイメージ。
		/// </summary>
		Image					img_mousepush	= null;
		/// <summary>
		/// プルダウン時のイメージ。
		/// </summary>
		Image					img_pulldown	= null;
		/// <summary>
		/// Disabled時のイメージ。
		/// </summary>
		Image					img_disabled	= null;
		/// <summary>
		/// イベント登録フラグ（２重登録が発生するので）
		/// </summary>
		bool					event_entry		= false;
		#endregion
		
		#region *** Property ***
		/// <summary>
		/// 選択された状態かどうかを取得します。
		/// </summary>
		public bool		IsSelect
		{
			get
			{
				return	selecting;
			}
		}
		bool			selecting = false;
		
		/// <summary>
		/// プルダウンボタンかどうかを取得または設定します。true でプルボタン、false で通常ボタンです。
		/// </summary>
		[Description("[Uc機能]プルダウンボタンかどうかを取得または設定します。")]
		public bool		PullDown
		{
			get
			{
				return	pulldown;
			}
			set
			{
				pulldown = value;
			}
		}
		bool			pulldown = false;
		
		/// <summary>
		/// マウスオーバー時に表示されるツールチップを取得または設定します。
		/// </summary>
		public ComponentForm.ToolTip	ToolTip
		{
			get
			{
				return tooltip;
			}
			set
			{
				tooltip = value;
			}
		}
		ComponentForm.ToolTip	tooltip;
		
		/// <summary>
		/// マウスオーバー時に表示されるツールチップテキストを取得または設定します。
		/// </summary>
		public string	ToolTipText
		{
			get
			{
				return	tooltiptext;
			}
			set
			{
				tooltiptext = value;
			}
		}
		string			tooltiptext = "";
		
		/// <summary>
		/// マウスオーバー時に表示されるツールチップキャプションを取得または設定します。
		/// </summary>
		public string	ToolTipCaption
		{
			get
			{
				return	tooltipcaption;
			}
			set
			{
				tooltipcaption = value;
			}
		}
		string			tooltipcaption = "";
		
		/// <summary>
		/// マウスオーバー時に表示されるツールチップアイコンを取得または設定します。
		/// </summary>
		public ToolTipIcon	ToolTipIcon
		{
			get
			{
				return		tooltipicon;
			}
			set
			{
				tooltipicon = value;
			}
		}
		ToolTipIcon			tooltipicon = ToolTipIcon.None;
		
		/// <summary>
		/// ボタンの文字ラベルに表示するテキストを取得または設定します。
		/// </summary>
		[Description("[Uc機能]ボタンの文字ラベルに表示するテキストを取得または設定します。")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
		public string		TextButton
		{
			get
			{
				return pLbl.Text;
			}
			set
			{
				pLbl.Text = value;
				setSize();
			}
		}
		
		/// <summary>
		/// 通常イメージ
		/// </summary>
		[Description("[Uc機能]通常表示されるイメージを取得、または設定します。")]
		public Image ImgDefault
		{
			set
			{
				Image	img = value;
				
//				if (img != null)
//				{
					img_default = img;
					pBox.Image	= img;
					
					setSize();
//				}
			}
			
			get
			{
				return img_default;
			}
		}
		
		/// <summary>
		/// マウスオーバー時のイメージ
		/// </summary>
		[Description("[Uc機能]マウスオーバー時に表示されるイメージを取得、または設定します。")]
		public Image ImgMouseOver
		{
			set
			{
				Image	img = checkimage(value);
				
//				if (img != null)
//				{
					img_mouseover = img;
				}
//			}
			
			get
			{
				return img_mouseover;
			}
		}
		
		/// <summary>
		/// ボタンを押した時のイメージ。
		/// </summary>
		[Description("[Uc機能]ボタンが押された時に表示されるイメージを取得、または設定します。")]
		public Image ImgMousePush
		{
			set
			{
				Image	img = checkimage(value);
				
//				if (img != null)
//				{
					img_mousepush = img;
//				}
			}
			
			get
			{
				return img_mousepush;
			}
		}
		
		/// <summary>
		/// プルダウン時のイメージ。
		/// </summary>
		[Description("[Uc機能]ボタンが押されっぱなしの状態で表示されるイメージを取得、または設定します。")]
		public Image ImgPullDown
		{
			set
			{
				Image	img = checkimage(value);
				
//				if (img != null)
//				{
					img_pulldown = img;
//				}
			}
			
			get
			{
				return img_pulldown;
			}
		}
		
		/// <summary>
		/// Disabled時のイメージ。
		/// </summary>
		[Description("[Uc機能]ボタンが押せない時に表示されるイメージを取得、または設定します。")]
		public Image ImgDisabled
		{
			set
			{
				Image	img = checkimage(value);
				
//				if (img != null)
//				{
					img_disabled = img;
//				}
			}
			
			get
			{
				return img_disabled;
			}
		}

		/// <summary>
		/// ラベルに表示するフォントサイズを取得・設定します。
		/// </summary>
		[Description("[Uc機能]ラベルに表示するフォントサイズを取得・設定します。")]
		public float LabelFontSize
		{
			get { return pLbl.Font.Size; }
			set { pLbl.Font = new Font(pLbl.Font.FontFamily, value); }
		}
		#endregion
		
		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public UcImgButton()
		{
			InitializeComponent();
			
			setSize();
			
			this.Load += new EventHandler(UcImgButton_Load);
			this.SizeChanged += new EventHandler(UcImgButton_SizeChanged);
			this.EnabledChanged += new EventHandler(UcImgButton_EnabledChanged);
		}

		#endregion
		
		#region *** Event ***
		/// <summary>
		/// フォームロード。
		/// </summary>
		void UcImgButton_Load(object sender, EventArgs e)
		{
			if (this.Enabled == true)
			{
				eventAdd();
			}
			else
			{
				eventClear();
			}
		}
		
		/// <summary>
		/// リサイズ
		/// </summary>
		void UcImgButton_SizeChanged(object sender, EventArgs e)
		{
			setSize();
		}
		
		/// <summary>
		/// フォーカスのある状態でEnterを押すとクリックしたのと同じ効果。
		/// </summary>
		void UcImgButton_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (this.Focused == true)
				{
					PerformClick();
				}
			}
		}
		
		/// <summary>
		/// ボタンを離した（ただし、マウスカーソルはコントロール上にある）
		/// </summary>
		void UcImgButton_MouseUp(object sender, MouseEventArgs e)
		{
			if (selecting == true)
			{
				return;
			}
			
			// マウスが離された時点でコントロールの上にマウスカーソルがない場合は無反応に
			if ((e.X < 0 || e.X > pBox.Width) || (e.Y < 0 || e.Y > pBox.Height))
			{
				pBox.Image		= img_default;
				pLbl.ForeColor	= ColorDefault;
				return;
			}
			
			// 押して離したら解除
			pBox.Image		= img_mouseover;
			pLbl.ForeColor	= ColorOver;
			
			PerformClick();
		}
		
		/// <summary>
		/// ボタンを押した
		/// </summary>
		void UcImgButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (selecting == true)
			{
				return;
			}
			
			pBox.Image		= img_mousepush;
			pLbl.ForeColor	= ColorPush;
			
			tipsOut();
		}
		/// <summary>
		/// フォーカスアウト
		/// </summary>
		void UcImgButton_MouseLeave(object sender, EventArgs e)
		{
			if (selecting == true)
			{
				return;
			}
			
			if (this.Focused == false)
			{
				pBox.Image		= img_default;
				pLbl.ForeColor	= ColorDefault;
				
				tipsOut();
			}
		}
		
		/// <summary>
		/// フォーカスイン
		/// </summary>
		void UcImgButton_MouseEnter(object sender, EventArgs e)
		{
			if (selecting == true)
			{
				return;
			}
			
			pBox.Image		= img_mouseover;
			pLbl.ForeColor	= ColorOver;
			
			tipsIn();

			this.Cursor = Cursors.Hand;
		}
		
		/// <summary>
		/// セレクトアウト
		/// </summary>
		void UcImgButton_Leave(object sender, EventArgs e)
		{
			if (selecting == true)
			{
				return;
			}
			
			pBox.Image		= img_default;
			pLbl.ForeColor	= ColorDefault;
			
			tipsOut();

			this.Cursor = Cursors.Default;
		}
		
		/// <summary>
		/// セレクトイン
		/// </summary>
		void UcImgButton_Enter(object sender, EventArgs e)
		{
			if (selecting == true)
			{
				return;
			}
			
		    pBox.Image		= img_mouseover;
			pLbl.ForeColor	= ColorOver;
			
			tipsIn();
		}
		
		/// <summary>
		/// Enabled が変化した時。
		/// </summary>
		void UcImgButton_EnabledChanged(object sender, EventArgs e)
		{
			if (this.Enabled == true)
			{
				eventAdd();
			}
			else
			{
				eventClear();
			}
		}
		#endregion
		
		#region *** Public Method ***
		/// <summary>
		/// 選択された状態にします。
		/// </summary>
		public void SetClick()
		{
			// 選択中
			if (selecting == true)
			{
				return;
			}
			
			if (pulldown == true)
			{
				selecting = true;
				// 押されっぱなしボタン
				if (img_pulldown != null)
				{
					pBox.Image	= img_pulldown;
				}
				else
				{
					pBox.Image	= img_mousepush;
				}
				pLbl.ForeColor	= ColorPull;
			}
		}
		/// <summary>
		/// 選択された状態を解除します。
		/// </summary>
		public void ResetClick()
		{
			if (selecting == false)
			{
				return;
			}
			
			selecting		= false;
			pBox.Image		= img_default;
			pLbl.ForeColor	= ColorDefault;
		}
		/// <summary>
		/// プログラム上からボタンが押された状態にします。
		/// </summary>
		public void PerformClick()
		{
			// 選択中
			if (selecting == true)
			{
				return;
			}
			
			if (ClickEvent != null)
			{
				CancelEventArgs e = new CancelEventArgs();
				
				ClickEvent(this, e);
				
				// 押されっぱなしキャンセル。
				if (e.Cancel == true)
				{
					return;
				}
			}
			
			if (pulldown == true)
			{
				selecting = true;
				// 押されっぱなしボタン
				if (img_pulldown != null)
				{
					pBox.Image	= img_pulldown;
				}
				else
				{
					pBox.Image		= img_mousepush;
				}
				pLbl.ForeColor	= ColorPull;
			}
		}
		#endregion
		
		#region *** Private Method ***
		/// <summary>
		/// ツールチップ表示。
		/// </summary>
		void tipsIn()
		{
			if (tooltip != null)
			{
				tooltip.Show(this, tooltipcaption, tooltiptext, tooltipicon);
			}
		}
		/// <summary>
		/// ツールチップ削除。
		/// </summary>
		void tipsOut()
		{
			if (tooltip != null)
			{
				tooltip.Hide();
			}
		}
		/// <summary>
		/// ボタンイベントクリア
		/// </summary>
		void eventClear()
		{
			pBox.Image = img_disabled;
			
			if (event_entry == true)
			{
				this.KeyDown -= new KeyEventHandler(UcImgButton_KeyDown);
				pBox.KeyDown -= new KeyEventHandler(UcImgButton_KeyDown);
				
				this.Enter -= new EventHandler(UcImgButton_Enter);
				this.Leave -= new EventHandler(UcImgButton_Leave);
				pBox.MouseEnter -= new EventHandler(UcImgButton_MouseEnter);
				pBox.MouseLeave -= new EventHandler(UcImgButton_MouseLeave);
				pBox.MouseDown -= new MouseEventHandler(UcImgButton_MouseDown);
				pBox.MouseUp -= new MouseEventHandler(UcImgButton_MouseUp);

				event_entry = false;
			}

			selecting = false;
		}
		
		/// <summary>
		/// ボタンイベント追加
		/// </summary>
		void eventAdd()
		{
			pBox.Image = img_default;
			
			if (event_entry == false)
			{
				this.KeyDown += new KeyEventHandler(UcImgButton_KeyDown);
				pBox.KeyDown += new KeyEventHandler(UcImgButton_KeyDown);
				
				this.Enter += new EventHandler(UcImgButton_Enter);
				this.Leave += new EventHandler(UcImgButton_Leave);
				pBox.MouseEnter += new EventHandler(UcImgButton_MouseEnter);
				pBox.MouseLeave += new EventHandler(UcImgButton_MouseLeave);
				pBox.MouseDown += new MouseEventHandler(UcImgButton_MouseDown);
				pBox.MouseUp += new MouseEventHandler(UcImgButton_MouseUp);

				event_entry = true;
			}
		}
		
		/// <summary>
		///	表示するイメージをチェックし、問題がなければ返します
		/// </summary>
		/// <param name="img">チェックするイメージ</param>
		/// <returns>null..エラー, Image..OK</returns>
		Image checkimage(Image img)
		{
			if (img == null)
			{
				return null;
			}
			
			if (img_default == null)
			{
				ErrLog.WriteLine("基準となるImgDefaultの登録がありません。");
				return null;
			}
			else if (img.Width != img_default.Width || img.Height != img_default.Height)
			{
				ErrLog.WriteLine("ImgDefaultと同じサイズである必要があります。");
				return null;
			}
			
			return img;
		}
		
		/// <summary>
		/// コントロールの高さをプロパティにより設定。
		/// </summary>
		void	setSize()
		{
			int		labelHeight = 40;
			
			if (img_default == null)
			{
				pLbl.Left	= 0;
				pLbl.Top	= 0;
				pLbl.Width	= this.Width;
				pLbl.Height	= this.Height;
				
				return;
			}
			
			if (pLbl.Text.Length == 0)
			{
				if (this.Width < img_default.Width)
				{
					this.Width	= img_default.Width;
				}
				if (this.Height < img_default.Height)
				{
					this.Height = img_default.Height;
				}
				pBox.Width	= this.Width;
				pBox.Height = this.Height;
			}
			else
			{
				// 文字エリアはアイコン絵の縦1/2サイズ
				if (this.Width < img_default.Width)
				{
					this.Width	= img_default.Width;
				}
				
				if (this.Height < img_default.Height + labelHeight)
				{
					this.Height = img_default.Height + labelHeight;
				}
				pBox.Width	= this.Width;
				pBox.Height = img_default.Height;
			}
			
			pBox.Left	= 0;
			pBox.Width	= this.Width;
			pLbl.Left	= 0;
			pLbl.Width	= this.Width;
			
			if (pLbl.Text.Length == 0)
			{
				pBox.Top	= 0;
				pBox.Height	= this.Height;
				pLbl.Top	= this.Height;
				pLbl.Height = 0;
			}
			else
			{
				pBox.Top	= 0;
				pBox.Height	= this.Height - labelHeight;
				pLbl.Top	= this.Height - labelHeight;
				pLbl.Height = labelHeight;
			}
		}
		#endregion
	}
}
