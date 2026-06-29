using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;

using GrapeCity.Win.Editors;

using ComponentDB;
using ComponentIO;
using ComponentRegistry;
using GControlGcComboBoxEx;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// DBView(DataView)をDataSourceにセットするタイプのComboBoxです。
	/// インクリメンタルサーチ機能も実装されています。
	/// </summary>
	public partial class UcTableComboBox : UcInputControl_Super
	{
		#region *** Event ***
		/// <summary>
		/// コンボボックスのテキストが書き換えられた時に呼び出されます。
		/// </summary>
		public new event EventHandler	TextChanged;
		/// <summary>
		/// コンボボックスのindexが変更された時に呼び出されます。
		/// </summary>
		public event EventHandler		SelectIndexChanged;
		/// <summary>
		/// コンボボックスの入力検証
		/// </summary>
		public event EventHandler ValidateComboBox;
		#endregion
		
		#region *** Private Value ***
		/// <summary>
		/// テーブル情報(DBView)。
		/// </summary>
		DBView		dv = null;
		/// <summary>
		/// サーチ用テーブル情報(DBView)。
		/// </summary>
		DBView		dv_search = null;
		/// <summary>
		/// コンボテキスト文字列が空欄のものについては選べないようにするフィルタです。
		/// </summary>
		string		exceptblank_rowfilter = "";
		/// <summary>
		/// テーブルにかけるフィルタ情報です。
		/// </summary>
		string		rowfilter = "";
		/// <summary>
		/// exceptblank_rowfilter と rowfilter を合わせたフィルタ情報です。
		/// </summary>
		string		allfilter = "";
		/// <summary>
		/// 文字入力したのか、カーソル移動させたのかを確認するためのフラグ。
		/// </summary>
		bool		wordInput = false;
		/// <summary>
		/// 表示カラムインデックス。
		/// </summary>
		int			textSubIndex = -1;
		/// <summary>
		/// セレクトせずにフォーカス移動する場合に、強制的に選ばせる INDEX。
		/// </summary>
		int			selectedIndexNullLeave = -1;
		/// <summary>
		/// ドロップダウンボックスが開くのを禁止します。
		/// </summary>
		bool		disabledDropDown = true;
		/// <summary>
		/// アイコンを表示するかどうかを設定します。
		/// </summary>
		bool		iconVisible = true;
		/// <summary>
		/// ドロップダウンボックスのサイズ。
		/// </summary>
		Size		sizeDropDown;
		/// <summary>
		/// Imeの初期状態。
		/// </summary>
		ImeMode		enterImeMode = ImeMode.NoControl;
		/// <summary>
		/// セパレータ。
		/// </summary>
		static string   seperator = "────";
		/// <summary>
		/// アイコンのイメージ画像を取得、または設定します。
		/// </summary>
		static Image	iconImage = null;
		#endregion
		
		#region *** Property ***
		/// <summary>
		/// アイコンのイメージ画像を取得、または設定します。
		/// </summary>
		public static Image IconImage
		{
			set
			{
				iconImage = value;
			}
			get
			{
				return iconImage;
			}
		}
		/// <summary>
		/// セパレータ。
		/// </summary>
		public static string Seperator
		{
			get
			{
				return seperator;
			}
		}
		/// <summary>
		/// コントロールの境界線の種類を取得、または設定します。
		/// </summary>
		public BorderStyle ComboBorderStyle
		{
			get
			{
				return cmb.BorderStyle;
			}
			set
			{
				cmb.BorderStyle = value;
			}
		}

		/// <summary>
		/// 非活性時の背景色を設定します。
		/// </summary>
		public Color DiabledBackColor
		{
			get { return cmb.DisabledBackColor; }
			set { cmb.DisabledBackColor = value; }
		}

		/// <summary>
		/// 非活性時の文字色を設定します。
		/// </summary>
		public Color DisabledForeColor
		{
			get { return cmb.DisabledForeColor; }
			set { cmb.DisabledForeColor = value; }
		}

		/// <summary>
		/// アイコンを表示するかどうか取得、または設定します。
		/// </summary>
		[Description("アイコンを表示するかどうか取得、または設定します。")]
		[DefaultValue( true )]
		public bool		IconVisible
		{
			get
			{
				return iconVisible;
			}
			set
			{
				iconVisible = value;
				pic.Visible = value;
				
				sizeRefresh();
			}
		}
		/// <summary>
		/// コンボボックスを取得します。
		/// </summary>
		public GcComboBoxEx	ComboBox
		{
			get
			{
				return cmb;
			}
		}
		/// <summary>
		/// コンボボックスの Value と比較するための比較フィールドを取得、または設定します。
		/// </summary>
		public string CompareValue
		{
			get
			{
				return cmb.ExCompareValue;
			}
			set
			{
				cmb.ExCompareValue = value;
			}
		}
		/// <summary>
		/// テーブル情報(DBView)。
		/// </summary>
		public DBView DBView
		{
			set
			{
				string		text = cmb.Text;
				
				cmb.TextChanged		-= new EventHandler(cmb_TextChanged);
				
				if (value != null)
				{
					dv		  = new DBView(value.DataTable);
					dv_search = new DBView(value.DataTable);
				}
				else
				{
					dv		  = null;
					dv_search = null;
				}
				
				if (dv != null)
				{
					cmb.ExDataSource = dv.DataView;
				}
				else
				{
					cmb.ExDataSource = null;
				}
				
				for (int i = 0; i < cmb.ListColumns.Count; i++)
				{
					cmb.ListColumns[i].Visible	= false;
				}
				
				cmb.Text = text;
				
				cmb.TextChanged		+= new EventHandler(cmb_TextChanged);
			}
			get
			{
				return dv;
			}
		}
		/// <summary>
		/// テーブルにかけるフィルタ情報です。
		/// </summary>
		public string RowFilter
		{
			get
			{
				return rowfilter;
			}
			set
			{
				rowfilter = value;
				allfilter = makeAllFilter();
				
				if (dv != null)
				{
					string	text = cmb.Text;
					
					dv.RowFilter = allfilter;
					if (text.Length > 0)
					{
						if (cmb.ListColumns.Count > cmb.TextSubItemIndex)
						{
							cmb.SelectedIndex = dv.Search(cmb.ListColumns[cmb.TextSubItemIndex].DataPropertyName, text);
						}
					}
					else
					{
						cmb.SelectedIndex = -1;
					}
					if (cmb.SelectedIndex < 0)
					{
						cmb.Text = text;
					}
				}
			}
		}
		/// <summary>
		/// テーブルにかけるフィルタ情報です。
		/// </summary>
		public string Sort
		{
			get
			{
				if (dv != null)
				{
					return dv.Sort;
				}
				return "";
			}
			set
			{
				if (dv != null)
				{
					dv.Sort = value;
				}
			}
		}
		/// <summary>
		/// 指定された文字列に該当する行を探し、行を移動します。
		/// </summary>
		public string Find
		{
			set
			{
				int		index;
				
				if (value != null && value.Length > 0)
				{
					index = dv.Search(cmb.ListColumns[cmb.TextSubItemIndex].DataPropertyName, value);
				}
				else
				{
					index = -1;
				}
				
				if (index != cmb.SelectedIndex)
				{
					cmb.SelectedIndex = index;
				}
			}
		}
		/// <summary>
		/// コンボのドロップダウンリストの初期サイズを取得、または設定します。
		/// </summary>
		public Size DropDownSize
		{
			set
			{
				sizeDropDown	  =
				cmb.DropDown.Size = value;
			}
			get
			{
				return cmb.DropDown.Size;
			}
		}
		/// <summary>
		/// セレクトせずにフォーカス移動する場合に、強制的に選ばせる INDEX。
		/// </summary>
		public int SelectedIndexNullLeave
		{
			set
			{
				selectedIndexNullLeave = value;
			}
			get
			{
				return selectedIndexNullLeave;
			}
		}
		/// <summary>
		/// コンボが選択された時に表示する項目の列番号を取得、または設定します。
		/// </summary>
		public int TextSubItemIndex
		{
			set
			{
				if (value < 0)
				{
					// 未設定値なので処理しない。
					return;
				}

				string	text  = cmb.Text;
				int		count = 0;
				int		i;
				
				cmb.TextChanged		-= new EventHandler(cmb_TextChanged);
				
				for (i = 0; i < cmb.ListColumns.Count; i++)
				{
					if (cmb.ListColumns[i].Visible == true)
					{
						if (count++ == value)
						{
							string colname = cmb.ListColumns[i].DataPropertyName;

							//¶2023/3/23 kj カラムの型で判断
							// 表示文字列が空欄の行は選択できないようにカットするフィルタ。
							exceptblank_rowfilter = DBQuery.GetSql($"({colname} IS NOT NULL)");

							if (dv.DataTable.Columns[colname].DataType == typeof(string))
							{
								exceptblank_rowfilter = $"({exceptblank_rowfilter} AND (LEN({colname}) > 0))";
							}

							//exceptblank_rowfilter = DBQuery.GetSql("(({0} IS NOT NULL) AND (LEN({0}) > 0))", cmb.ListColumns[i].DataPropertyName);
							
							allfilter = makeAllFilter();
							
							if (dv != null)
							{
								dv.RowFilter = allfilter;
								if (cmb.Text.Length > 0)
								{
									cmb.SelectedIndex = dv.Search(cmb.ListColumns[cmb.TextSubItemIndex].DataPropertyName, cmb.Text);
								}
								else
								{
									cmb.SelectedIndex = -1;
								}
							}
							break;
						}
					}
				}
				if (i == cmb.ListColumns.Count)
				{
					Debug.WriteLine("■ 指定した列は存在しませんでした。");
					i = 0;
				}
				
				cmb.TextSubItemIndex = i;
				textSubIndex		 = value;
				cmb.Text			 = text;
				
				cmb.TextChanged		+= new EventHandler(cmb_TextChanged);
			}
			get
			{
				return textSubIndex;
			}
		}
		/// <summary>
		/// テキストボックスの文字列が選択されているものと同一か取得します。
		/// </summary>
		public bool TextSubItemMatch
		{
			get
			{
				if (cmb.SelectedItem == null)
				{
					return false;
				}
				
				return cmb.SelectedItem.SubItems[cmb.TextSubItemIndex].Value.ToString() == cmb.Text;
			}
		}
		/// <summary>
		/// コントロールがフォーカスを受け取った時に、文字列を選択するかどうかを取得、または設定します。
		/// </summary>
		public bool HighlightText
		{
			set
			{
				cmb.HighlightText = value;
			}
			get
			{
				return cmb.HighlightText;
			}
		}
		/// <summary>
		/// コントロール内に表示するテキストの配置を取得、または設定します。
		/// </summary>
		public ContentAlignment ContentAlignment
		{
			set
			{
				cmb.ContentAlignment = value;
			}
			get
			{
				return cmb.ContentAlignment;
			}
		}
		///	<summary>
		/// ドロップダウンボックスが表示されているかどうかを取得します。
		/// </summary>
		public bool DroppedDown
		{
			get
			{
				return cmb.DroppedDown;
			}
		}
		/// <summary>
		/// 選択されているテキストの文字列を取得または設定します。
		/// </summary>
		public new string Text
		{
			get
			{
				return cmb.Text;
			}
			set
			{
				this.Find = value;
				
				if (cmb.Text != value)
				{
					cmb.Text = value;
				}
			}
		}
		/// <summary>
		/// 選択されているテキストの Value を取得します。
		/// </summary>
		public object Value
		{
			get
			{
				return cmb.ExGetValue();
			}
		}
		/// <summary>
		/// フォーカスIN時のImeMode
		/// </summary>
		public ImeMode EnterImeMode
		{
			set
			{
				enterImeMode = value;
				cmb.ImeMode = enterImeMode;
			}
			get
			{
				return enterImeMode;
			}
		}
		#endregion
		
		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public UcTableComboBox()
		{
			InitializeComponent();
			
			if (iconImage != null)
			{
				pic.Image = iconImage;
			}
			
			this.Load		 += new EventHandler(UcTableComboBox_Load);
			this.SizeChanged += new EventHandler(UcTableComboBox_SizeChanged);
		}
		#endregion
		
		/// <summary>
		/// 条件に合う行を検索し、カーソルを合わせます。
		/// </summary>
		/// <param name="field">フィールド</param>
		/// <param name="value">値</param>
		public void Search(string field, object value)
		{
			int		index = dv.Search(field, value);
			
			if (index != cmb.SelectedIndex)
			{
				cmb.SelectedIndex = index;
			}
		}
		
		/// <summary>
		/// コントロールの再描画を一時停止します。
		/// </summary>
		public void BeginUpdate()
		{
			cmb.BeginUpdate();
		}
		
		/// <summary>
		/// BeginUpdate() によって停止されていたコントロールの再描画を再開します。
		/// </summary>
		public void EndUpdate()
		{
			cmb.TextChanged	-= new EventHandler(cmb_TextChanged);
			
			// 表示アイテムが選択されていない。
			if (textSubIndex == -1)
			{
				Debug.WriteLine("■ TextSubItemIndex が設定されていません。");
				this.TextSubItemIndex = 0;
			}
			
			int		index = cmb.SelectedIndex;
			
			// EndUpdate で SelectedIndex が勝手に 0 に設定される。。。
			cmb.EndUpdate();
			
			cmb.SelectedIndex = index;
			
			cmb.TextChanged	+= new EventHandler(cmb_TextChanged);
			
			// レジストリの（保存してあれば）高さを設定。
			int		height = 0;//Cast.Int(RegControl.Pop(cmb, "Height"));
			
			if (height != 0)
			{
				sizeDropDown	  =
				cmb.DropDown.Size = new Size(sizeDropDown.Width, height);
			}
		}
		
		/// <summary>
		/// 列を設定します。
		/// </summary>
		/// <param name="field">フィールド名。</param>
		/// <param name="text">列名。</param>
		/// <param name="width">列幅。</param>
		public void SetColumn(string field, string text, int width)
		{
			SetColumn(field, text, width, ContentAlignment.MiddleLeft);
		}
		
		/// <summary>
		/// 列を設定します。
		/// </summary>
		/// <param name="field">フィールド名。</param>
		/// <param name="text">列名。</param>
		/// <param name="width">列幅。</param>
		/// <param name="align">列名の表示位置。</param>
		public void SetColumn(string field, string text, int width, ContentAlignment align)
		{
			int		colno = getColumn(field);
			
			if (colno == -1)
			{
				return;
			}
			cmb.ListColumns[colno].Visible					= true;
			cmb.ListColumns[colno].Width					= width;
			cmb.ListColumns[colno].Header.Text				= text;
			cmb.ListColumns[colno].Header.ContentAlignment	= ContentAlignment.MiddleCenter;
			
			cmb.ListColumns[colno].Header.ForeColor			= Color.DimGray;
			cmb.ListColumns[colno].DefaultSubItem.ContentAlignment = align;
		}
		
		#region *** Event ***
		/// <summary>
		/// フォームロード
		/// </summary>
		void UcTableComboBox_Load(object sender, EventArgs e)
		{
			// 列ヘッダを表示する。
			cmb.ListHeaderPane.Visible	= true;

			// 'a%5%' おかしい→ 'a％5%' a％5が先頭にある文字列 と正しくフィルタできるように半角記号は入力できなくする。
			cmb.Format = "^@";
			
			// ドロップダウン形式
			cmb.AutoSelect				= false;
			cmb.AutoCompleteMode		= AutoCompleteMode.None;
			cmb.AutoCompleteSource		= AutoCompleteSource.None;
			cmb.DropDownStyle			= ComboBoxStyle.DropDown;
			cmb.DropDown.AllowResize	= true;
			cmb.DropDown.ShowShadow		= true;
			
			//cmb.DisabledBackColor		= cmb.BackColor;
			
			cmb.ScrollBarMode			= ScrollBarMode.Fixed;
			cmb.ImeMode					= enterImeMode;
			
			//■ イベント設定
			this.Enter			+= new EventHandler(UcTableComboBox_Enter);
			this.Leave			+= new EventHandler(UcTableComboBox_Leave);
			cmb.TextChanging	+= new TextChangingEventHandler(cmb_TextChanging);
			cmb.TextChanged		-= new EventHandler(cmb_TextChanged);
			cmb.TextChanged		+= new EventHandler(cmb_TextChanged);
			cmb.KeyPress		+= new KeyPressEventHandler(cmb_KeyPress);
			cmb.KeyDown			+= new KeyEventHandler(cmb_KeyDown);
			cmb.KeyUp			+= new KeyEventHandler(cmb_KeyUp);
			cmb.DropDownOpening += new EventHandler<DropDownOpeningEventArgs>(cmb_DropDownOpening);
			cmb.DropDownOpened	+= new EventHandler(cmb_DropDownOpened);
			cmb.DropDownClosing += new EventHandler<DropDownClosingEventArgs>(cmb_DropDownClosing);
			cmb.DropDownClosed	+= new EventHandler(cmb_DropDownClosed);
			cmb.SelectedIndexChanged += new EventHandler(cmb_SelectedIndexChanged);
			cmb.PreviewKeyDown += new PreviewKeyDownEventHandler(cmb_PreviewKeyDown);
			cmb.Validated += cmb_Validated;
			pic.Click += new EventHandler(pic_Click);
		}

		/// <summary>
		/// コンボボックスの検証処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void cmb_Validated(object sender, EventArgs e)
		{
			if (ValidateComboBox != null)
			{
				ValidateComboBox(cmb, e);
			}
		}
		
		/// <summary>
		/// コンボボックスにフォーカスINさせる
		/// </summary>
		void UcTableComboBox_Enter(object sender, EventArgs e)
		{
			cmb.Select();
			cmb.SelectAll();
		}
		
		/// <summary>
		/// フォーカスアウト。選択がない場合に、強制的に項目を割り当てることができます。
		/// </summary>
		void UcTableComboBox_Leave(object sender, EventArgs e)
		{
			if (cmb.SelectedIndex < 0)
			{
				if (selectedIndexNullLeave >= 0)
				{
					if (cmb.Items.Count > 0)
					{
						cmb.SelectedIndex = selectedIndexNullLeave;
					}
				}

				// 未選択なので入力値をクリアしておく。
				cmb.Text = "";
			}
			
			// ドロップダウン禁止で終わらせる。
			disabledDropDown = true;
		}
		
		/// <summary>
		/// ユーザーコントロールのサイズ変更にコンボボックスを追従させる。
		/// </summary>
		void UcTableComboBox_SizeChanged(object sender, EventArgs e)
		{
			sizeRefresh();
		}
		
		/// <summary>
		/// アイコン絵をクリックした時の処理
		/// </summary>
		void pic_Click(object sender, EventArgs e)
		{
			cmb.Select();
			cmb.SelectAll();
		}
		
		#region ComboBox
		void cmb_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			
		}
		
		/// <summary>
		/// キー押した時。
		/// </summary>
		void cmb_KeyDown(object sender, KeyEventArgs e)
		{
			GcComboBoxEx	cbox = (GcComboBoxEx)sender;
			bool			cursor_move = false;
			
//Debug.WriteLine("--- KeyDown");
//Debug.WriteLine("  Key " + e.KeyCode);
//Debug.WriteLine("  SelectedIndex " + cbox.SelectedIndex);
			
			// ドロップダウン許可。
			disabledDropDown = false;
			
			switch (e.KeyCode)
			{
				case Keys.Delete :
				case Keys.Back :
					wordInput = true;
					disabledDropDown = true;
					break;
				case Keys.PageUp :
				case Keys.PageDown :
				case Keys.Up :
				case Keys.Down :
					// -1 といいながら、内部で dv.Count を超えた値を勝手に保持していたりする。
					// そのままカーソルを動かすとありえない Index で落ちるので、ここでその値をクリア。
					if (cbox.SelectedIndex == -1)
					{
						if (dv.Count > 0)
						{
							cbox.TextChanging  += new TextChangingEventHandler(cancelTextChanging);
							// 値をクリア。
							cbox.SelectedIndex =  0;
							
							if (dv.Count > 1)
							{
								// 非選択に戻し、先頭からカーソルが移動するように。
								// かなり意味不明だが、Count = 1 しかない場合にこれをやると、SelectIndexの値がどうこうという謎のエラーで落ちる。
								cbox.SelectedIndex = -1;
							}
							cbox.TextChanging  -= new TextChangingEventHandler(cancelTextChanging);
						}
					}
					
					wordInput = false;
					cursor_move = true;
					break;
				case Keys.Tab :
					wordInput = false;
					cursor_move = true;
					break;
				case Keys.Enter :
					wordInput = false;
					break;
				default :
					wordInput = true;
					break;
			}
			
			if (cursor_move == true)
			{
				if (cbox.DroppedDown == false)
				{
					if (disabledDropDown == false)
					{
						// テキストが無条件に変更されるのを無理やり防ぐ。
						cbox.TextChanging  += new TextChangingEventHandler(cancelTextChanging);
						cbox.DroppedDown	= true;
						cbox.TextChanging  -= new TextChangingEventHandler(cancelTextChanging);
					}
				}
				
				// マウスカーソルが選択の邪魔をしないように移動しておく
				Cursor.Position = this.PointToScreen(new Point(cbox.Left + cbox.Width, cbox.Top + cbox.Height));
			}
//Debug.WriteLine("  WordInput " + wordInput);
		}
		
		/// <summary>
		/// キー離した時。
		/// </summary>
		void cmb_KeyUp(object sender, KeyEventArgs e)
		{
//Debug.WriteLine("--- KeyUp");
			// ドロップダウン禁止。
			disabledDropDown = true;
		}
		
		/// <summary>
		/// キー押している時。
		/// </summary>
		void cmb_KeyPress(object sender, KeyPressEventArgs e)
		{
//Debug.WriteLine("--- KeyPress");
		}

		void cmb_TextChanging(object sender, TextChangingEventArgs e)
		{
			if (e.Result == seperator)
			{
				e.Cancel = true;
			}
		}
		
		/// <summary>
		/// テキストチェンジを強制キャンセル。
		/// </summary>
		void cancelTextChanging(object sender, TextChangingEventArgs e)
		{
			e.Cancel = true;
		}
		
		/// <summary>
		/// テキスト変更時。
		/// </summary>
		void cmb_TextChanged(object sender, EventArgs e)
		{
			GcComboBox	cbox = (GcComboBox)sender;
			int			pos	 = cbox.SelectionStart;
			string		text = cbox.Text;
			
			if (dv == null)
			{
				return;
			}
//Debug.WriteLine("--- TextChanged　" + cbox.SelectedIndex);
			
			// カーソル上下などの選択ではフィルタ変化させない（リスト表示中にフィルタを変更すると、Indexがおかしくなって落ちる）
			if (wordInput == false)
			{
				// 外部に TextChanged 通知
				if (TextChanged != null)
				{
					TextChanged(this, e);
				}
				return;
			}
//Debug.WriteLine("--- TextChanged　" + cbox.Text);
			// Textプロパティの無限書き換え（無限イベント呼び出し）を禁止。
			cbox.TextChanged -= new EventHandler(cmb_TextChanged);
			
			if (text.Length == 0)
			{
				if (cbox.DroppedDown == true)
				{
					cbox.DroppedDown = false;
				}
				else
				{
					// インクリメンタルサーチを解除する。フィルタかけていた項目は全て元に戻す。
					cbox.TextChanging += new TextChangingEventHandler(cancelTextChanging);
					dv.RowFilterQuery(allfilter);
					
					try
					{
						cbox.SelectedIndex = dv.Search(cbox.ListColumns[cbox.TextSubItemIndex].DataPropertyName, cbox.Text);
					}
					catch(Exception ex)
					{
						Debug.WriteLine(ex.Message);
					}
					cbox.TextChanging -= new TextChangingEventHandler(cancelTextChanging);
				}
			}
			else if (cbox.SelectedIndex == -1)
			{
				// 前回のサーチ文字列と違っていたら検索開始。
				//■ インクリメンタルサーチモード
				//
				// 有限会　▼
				//	|   1|有限会社 A|
				//	|   3|有限会社 C|
				//	|  31|有限会社 AA|
				//	| 100|有限会社 DD|
				//
				// 「有限会社」という言葉でリストアップした中での検索が出来る。
//Debug.WriteLine("  サーチ[" + text + "," + cbox.DroppedDown + "," + cbox.SelectedIndex + "]");
				string		filter = "";
				
				for (int i = 0; i < cbox.ListColumns.Count; i++)
				{
					if (cbox.ListColumns[i].Visible == false)
					{
						continue;
					}
					
					if (dv.DataTable.Columns[i].DataType == typeof(string))
					{
						if (text.Length == 1)
						{
							// １文字の場合は先頭から
							filter += $"{DBQuery.CreateLikeKanaString(dv.DataTable.Columns[i].Caption, getFilter(text))} OR ";
							//filter += DBQuery.GetSql("({0} LIKE '{1}%') OR ",
							//	dv.DataTable.Columns[i].Caption,
							//	getFilter(text)
							//	);
						}
						else
						{
							// ２文字を越える文字列は、言葉間にあってもサーチする。
							filter += $"{DBQuery.CreateLikeKanaString(dv.DataTable.Columns[i].Caption, getFilter(text), true)} OR ";
							//filter += DBQuery.GetSql("({0} LIKE '%{1}%') OR ",
							//	dv.DataTable.Columns[i].Caption,
							//	getFilter(text)
							//	);
						}
					}
					else
					{
						// 数字は先頭から入力しているとみなすサーチ。
						filter += DBQuery.GetSql("(Convert({0}, 'System.String') LIKE '{1}%') OR ", 
							dv.DataTable.Columns[i].Caption,
							getFilter(text)
							);
					}
				}
				if (filter != "")
				{
					// 最後の AND を消す。
					filter = filter.Remove(filter.LastIndexOf(" OR "), 4);
				}
				
				if (allfilter != "")
				{
					dv_search.RowFilterQuery("({0}) AND ({1})", allfilter, filter);
				}
				else
				{
					dv_search.RowFilterQuery("{0}'", filter);
				}
					
//Debug.WriteLine("  filter(" + dv_search.Count + ") [" + dv_search.RowFilter + "]");
				
				if (dv_search.RowFilter != dv.RowFilter)
				{
//Debug.WriteLine("  filter changed. '" + cbox.SelectedIndex + "'");
					//■ リストにフィルタをかける。
					// テキストが無条件に変更されるのを無理やり防ぐ。
					cbox.TextChanging  += new TextChangingEventHandler(cancelTextChanging);
					dv.RowFilter		= dv_search.RowFilter;
					
					// フィルタかけなおすので、非選択とする。
					cbox.SelectedIndex = -1;
					
					cbox.TextChanging  -= new TextChangingEventHandler(cancelTextChanging);
				}
				
				if (dv.Count == 0)
				{
//Debug.WriteLine("  drop down closing.");
					if (cbox.DroppedDown == true)
					{
//Debug.WriteLine("  drop down closed.");
						cbox.DroppedDown = false;
					}
				}
				else
				{
					if (cbox.DroppedDown == false)
					{
//Debug.WriteLine("  drop down opencheck.");
						if (disabledDropDown == false)
						{
//Debug.WriteLine("  drop down opening. '" + cbox.SelectedIndex + "'");
							// テキストが無条件に変更されるのを無理やり防ぐ。
							cbox.TextChanging  += new TextChangingEventHandler(cancelTextChanging);
							cbox.DroppedDown	= true;
							
							if (dv.Count == 1)
							{
								cbox.SelectedIndex = 0;
							}
							else
							{
								// オープン時は非選択。
								cbox.SelectedIndex = -1;
							}

							cbox.TextChanging  -= new TextChangingEventHandler(cancelTextChanging);
//Debug.WriteLine("  drop down opened. '" + cbox.SelectedIndex + "'");
						}
					}
					else
					{
						if (dv.Count == 1)
						{
							cbox.SelectedIndex = 0;
						}
					}
				}
				cbox.Text = text;
//Debug.WriteLine("  text[" + text + "] cbox.Text[" + cbox.Text + "]");
				cbox.SelectionStart = pos > cbox.Text.Length ? cbox.Text.Length : pos;
				cbox.Refresh();
			}
			
			// 外部に TextChanged 通知
			if (TextChanged != null)
			{
				TextChanged(this, e);
			}
			
			// イベント処理を元に戻す
			cbox.TextChanged += new EventHandler(cmb_TextChanged);
			
//Debug.WriteLine("--- textchange end[" + cbox.Text + "," + cbox.DroppedDown + "]");
		}
		
		/// <summary>
		/// index が変更された時に呼び出されます。
		/// </summary>
		void cmb_SelectedIndexChanged(object sender, EventArgs e)
		{
//Debug.WriteLine("--- SelectedIndexChanged " + cmb.SelectedIndex);
			if (SelectIndexChanged != null)
			{
				SelectIndexChanged(this, e);
			}
		}
		
		/// <summary>
		/// ドロップダウンを開く直前です。
		/// </summary>
		void cmb_DropDownOpening(object sender, DropDownOpeningEventArgs e)
		{
//Debug.WriteLine("--- DropDownOpening");
//Debug.WriteLine("  Focused : " + ((Control)sender).Focused);
			// DropDown をキャンセルするとサイズが勝手に初期化されるので、再設定する。
			GcComboBox	cbox  = (GcComboBox)sender;
			
			if (cbox.Focused == false)
			{
				e.Cancel = true;
			}
			else
			{
				int		width = 0;
				
				for (int i = 0; i < cbox.ListColumns.Count; i++)
				{
					if (cbox.ListColumns[i].Visible == false)
					{
						continue;
					}
					
					width += cbox.ListColumns[i].Width;
				}
				
				cbox.DropDown.Size = new Size(sizeDropDown.Width + 4, sizeDropDown.Height);
			}
		}
		
		/// <summary>
		/// ドロップダウンを開きます。
		/// </summary>
		void cmb_DropDownOpened(object sender, EventArgs e)
		{
//Debug.WriteLine("--- DropDownOpened");
			grid_move_permission = false;
		}
		
		/// <summary>
		/// ドロップダウンを閉じる直前です。
		/// </summary>
		void cmb_DropDownClosing(object sender, DropDownClosingEventArgs e)
		{
//Debug.WriteLine("--- DropDownClosing");
			GcComboBoxEx	cbox = (GcComboBoxEx)sender;

			if (cbox.SelectedIndex == -1 && cbox.Text.Length > 0)
			{
				if (((DataView)cbox.ExDataSource).Count > 0)
				{
					cbox.SelectedIndex = 0;
				}
			}
			
			// 現在の変更された高さを保存。
			sizeDropDown = new Size(sizeDropDown.Width + 4, cbox.DropDown.Size.Height);
			//RegControl.Push(cmb, "Height", cbox.DropDown.Size.Height);
//Debug.WriteLine("size: " + cbox.DropDown.Size.Width + ", " + cbox.DropDown.Size.Height);
		}
		
		/// <summary>
		/// ドロップダウンを閉じます。
		/// </summary>
		void cmb_DropDownClosed(object sender, EventArgs e)
		{
//Debug.WriteLine("--- DropDownClosed");
			GcComboBoxEx	cbox = (GcComboBoxEx)sender;
			
			grid_move_permission = true;
			

			object selected_val = null;

			// 選択しているテキストに変更。
			wordInput = false;
			if (cbox.SelectedItem != null)
			{
				cbox.Text = cbox.SelectedItem.SubItems[cbox.TextSubItemIndex].Value.ToString();
				selected_val = cbox.ExGetValue();
			}
			
			// インクリメンタルサーチを解除する。フィルタかけていた項目は全て元に戻す。
			cbox.TextChanging += new TextChangingEventHandler(cancelTextChanging);
			dv.RowFilterQuery(allfilter);

			if (selected_val != null)
			{
				cbox.ExSetSelectedIndexByValue(selected_val);
			}
			else
			{
				cbox.SelectedIndex = dv.Search(cbox.ListColumns[cbox.TextSubItemIndex].DataPropertyName, cbox.Text);
			}
			cbox.TextChanging -= new TextChangingEventHandler(cancelTextChanging);
			
			// クローズ（もしくはその直前の謎イベント・・・？）で全選択が解除されるっぽいので、ここでもう一度全選択しておく。
			cbox.SelectAll();

			if (ValidateComboBox != null)
			{
				ValidateComboBox(cmb, e);
			}
		}
		#endregion
		#endregion
		
		#region *** Private method ***
		/// <summary>
		/// 列番号を取得。
		/// </summary>
		/// <param name="field">列フィールド名。</param>
		/// <returns>列番号。</returns>
		int		getColumn(string field)
		{
			int		colno;
			
			for (colno = 0; colno < cmb.ListColumns.Count; colno++)
			{
				if (cmb.ListColumns[colno].DataPropertyName == field)
				{
					break;
				}
			}
			if (colno == cmb.ListColumns.Count)
			{
				colno = -1;
			}
			
			return colno;
		}
		
		/// <summary>
		/// ユーザー指定のrowfilterとシステム指定のフィルタをあわせた文字列を作成する。
		/// </summary>
		/// <returns>両方を合わせたフィルタ文字列。</returns>
		string makeAllFilter()
		{
			string[]	fils = {rowfilter, exceptblank_rowfilter};
			string		allfil;
			
			allfil = "(";
			
			for (int i = 0; i < fils.Length; i++)
			{
				if (fils[i] != null && fils[i] != "")
				{
					// 一つでもフィルタ文字列が作成されていたら AND で結ぶ。
					if (i > 0 && allfil != "(")
					{
						allfil += " AND ";
					}
					allfil += "(" + fils[i] + ")";
				}
			}
			
			allfil += ")";
			
			// かけるべきフィルタがなかった。
			if (allfil == "()")
			{
				allfil = "";
			}
			
			return allfil;
		}
		
		/// <summary>
		/// 正しいフィルタ文字列を取得します。
		/// </summary>
		/// <param name="text">特殊記号も含むテキスト。</param>
		/// <returns>特殊記号を除去したテキスト。</returns>
		string getFilter(string text)
		{
			text = Regex.Replace(text, "%", "");
			
			return text;
		}
		
		/// <summary>
		/// 表示サイズをリフレッシュします。
		/// </summary>
		void sizeRefresh()
		{
			if (iconVisible == true)
			{
				pic.SetBounds( 0, (this.Height-20)/2, 20, 20);
				cmb.SetBounds(20, 0, this.Width-20, this.Height);
			}
			else
			{
				cmb.SetBounds( 0, 0, this.Width, this.Height);
			}
		}
		#endregion
		
		#region *** For Debug ***
		/// <summary>
		/// エラー表示。DEBUG環境でのみ有効
		/// </summary>
		[Conditional("DEBUG")]
		private static void _write_err(Exception ex)
		{
			Debug.WriteLine("■■■ Exception");
			Debug.WriteLine("Source     = {0}", ex.Source);
			Debug.WriteLine("Type       = {0}", ex.GetType().ToString());
			Debug.WriteLine("Message    = {0}", ex.Message);
			Debug.WriteLine("StackTrace = {0}", ex.StackTrace);
			Debug.WriteLine("");
		}
		#endregion
	}
}
