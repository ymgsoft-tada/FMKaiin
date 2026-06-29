using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GControlGcTextBoxEx;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// ファイル、フォルダを選択するためのコントロールです。
	/// </summary>
	public partial class UcFileSelect : UserControl
	{
		/// <summary>
		/// 開くファイルダイアログの種類を示す列挙型です。
		/// </summary>
		public enum eFileDialogType
		{
			/// <summary>
			/// フォルダ選択。
			/// </summary>
			FolderSelect,
			/// <summary>
			/// ファイルオープン。
			/// </summary>
			FileOpen,
			/// <summary>
			/// ファイル保存。
			/// </summary>
			FileSave,
		}
		/// <summary>
		/// 選択されたファイルパスが適正であるかチェックするイベントです。
		/// </summary>
		/// <param name="path">ファイルパス。</param>
		/// <returns>true..OK、false..NG</returns>
		public delegate bool CheckFilePath(string path);
		
		#region *** Public Values ***
		/// <summary>
		/// 選択されたファイルパスが適正であるかチェックするイベントメソッドです。
		/// </summary>
		public CheckFilePath	CheckFilePathMethod = null;
		#endregion
		
		#region *** Properties ***
		/// <summary>
		/// セレクタがファイルダイアログ種別を取得、または設定します。
		/// </summary>
		[Description("セレクタのファイルダイアログ種別を取得、または設定します。")]
		public eFileDialogType FileDialogType
		{
			set
			{
				fileDialogType = value;
			}
			get
			{
				return fileDialogType;
			}
		}
		eFileDialogType fileDialogType = eFileDialogType.FileOpen;
		/// <summary>
		/// 開けるファイルの拡張子フィルタを取得、または設定します。
		/// </summary>
		[Description("開けるファイルの拡張子フィルタを取得、または設定します。")]
		public string FileFilter
		{
			set
			{
				fileFilter = value;
			}
			get
			{
				return fileFilter;
			}
		}
		string fileFilter = "すべてのファイル(*.*)|*.*";
		/// <summary>
		/// 開けるファイルの拡張子フィルタのうち、最初に何番目のフィルタでオープンするか取得、または設定します。
		/// </summary>
		[Description("開けるファイルの拡張子フィルタのうち、最初に何番目のフィルタでオープンするか取得、または設定します。")]
		public int FileFilterIndex
		{
			set
			{
				fileFilterIndex = value;
			}
			get
			{
				return fileFilterIndex;
			}
		}
		int fileFilterIndex = 0;
		/// <summary>
		/// テキストボックスのテキストを取得、または設定します。
		/// </summary>
		[Description("テキストボックスのテキストを取得、または設定します。")]
		public string UcText
		{
			set
			{
				txtSelect.Text = value;
			}
			get
			{
				return txtSelect.Text;
			}
		}
		/// <summary>
		/// テキストボックスを取得します。
		/// </summary>
		[Description("テキストボックスを取得します。")]
		public GcTextBoxEx UcTxtCtl
		{
			get
			{
				return txtSelect;
			}
		}
		#endregion
		
		/// <summary>
		/// テキストボックスが、
		/// 1度目のフォーカス＞全選択
		/// 2度目のフォーカス＞指定したクリック位置にカーソル
		/// といった状態を作り出すための小細工フラグ。
		/// </summary>
		bool txtSelect_clickSelectAll = false;

		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public UcFileSelect()
		{
			InitializeComponent();

			this.Resize += new EventHandler(UcSelect_Resize);
			this.Enter += new EventHandler(UcSelect_Enter);
			txtSelect.Click += new EventHandler(txtSelect_Click);
			txtSelect.Leave += new EventHandler(txtSelect_Leave);
			butSelect.Click += new EventHandler(butSelect_Click);
			txtSelect.AllowDrop = true;
			txtSelect.DragDrop += txtSelect_DragDrop;
			txtSelect.DragEnter += txtSelect_DragEnter;
		}

		void txtSelect_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				// ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				// ファイル以外は受け付けない
				e.Effect = DragDropEffects.None;
			}
		}

		void txtSelect_DragDrop(object sender, DragEventArgs e)
		{
			string[] strs = (string[])e.Data.GetData(DataFormats.FileDrop, false);

			this.Cursor = Cursors.WaitCursor;

			//if (checkExtensionCSV(strs[0]) == true)
			{
				txtSelect.Text = strs[0];
			}

			this.Cursor = Cursors.Default;
		}
		#endregion

		#region *** Events ***
		/// <summary>
		/// リサイズ。
		/// </summary>
		void UcSelect_Resize(object sender, EventArgs e)
		{
			txtSelect.Width = this.Width - 48;
			butSelect.Left	= this.Width - 42;
		}
		
		/// <summary>
		/// フォーカスIN。
		/// </summary>
		void UcSelect_Enter(object sender, EventArgs e)
		{
			txtSelect.Select();
			txtSelect.SelectAll();
		}
		
		/// <summary>
		/// テキストボックス選択時に文字列を全て選択。
		/// </summary>
		void txtSelect_Click(object sender, EventArgs e)
		{
			if (txtSelect_clickSelectAll == false)
			{
				txtSelect.SelectAll();
				txtSelect_clickSelectAll = true;
			}
		}

		void txtSelect_Leave(object sender, EventArgs e)
		{
			txtSelect_clickSelectAll = false;
		}
		
		/// <summary>
		/// 「選択」ボタン押した。
		/// </summary>
		void butSelect_Click(object sender, EventArgs e)
		{
			string		path = null;
			
			if (fileDialogType == eFileDialogType.FileOpen)
			{
				fileSelect(ref path);
			}
			else if (fileDialogType == eFileDialogType.FileSave)
			{
				fileSave(ref path);
			}
			else
			{
				folderSelect(ref path);
			}
			
			if (path != null)
			{
				if (CheckFilePathMethod == null || CheckFilePathMethod(path) == true)
				{
					txtSelect.Text = path;
				}
			}
		}
		#endregion
		
		#region *** Private Methods ***
		/// <summary>
		/// フォルダを選択します。
		/// </summary>
		/// <param name="path">設定されたフォルダパス。</param>
		void folderSelect(ref string path)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			
			// タイトル。
			fbd.Description = "フォルダを指定してください。";
			// ルートフォルダを指定。
			fbd.RootFolder = Environment.SpecialFolder.Desktop;
			// 初期オープンフォルダ。
			fbd.SelectedPath = txtSelect.Text.Length == 0 ? @"C:\" : txtSelect.Text;
			//ユーザーが新しいフォルダを作成できるようにする。
			fbd.ShowNewFolderButton = false;
			
			//ダイアログを表示する
			if (fbd.ShowDialog(this) == DialogResult.OK)
			{
				path = fbd.SelectedPath;
			}
		}
		
		/// <summary>
		/// ファイルを選択します。
		/// </summary>
		/// <param name="path">選択されたファイルパス。</param>
		void fileSelect(ref string path)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			
			// 初期ファイル名。
			ofd.FileName = txtSelect.Text.Length == 0 ? "" : txtSelect.Text;

			// 初期オープンフォルダ。
			ofd.InitialDirectory = txtSelect.Text.Length == 0 ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : txtSelect.Text;
			// 選択可能なファイルの種類。
			ofd.Filter = fileFilter;
			// [ファイルの種類]ではじめに選択されているファイル。
			ofd.FilterIndex = fileFilterIndex;
			// タイトル。
			ofd.Title = "開くファイルを選択してください";
			// ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする。
			ofd.RestoreDirectory = true;
			// 存在しないファイルの名前が指定されたとき警告を表示しない。
			ofd.CheckFileExists = false;
			// 存在しないパスが指定されたとき警告を表示。
			ofd.CheckPathExists = false;
			
			//ダイアログを表示する
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				path = ofd.FileName;
			}
		}
		
		/// <summary>
		/// ファイルを保存します。
		/// </summary>
		/// <param name="path">選択されたファイルパス。</param>
		void fileSave(ref string path)
		{
			SaveFileDialog ofd = new SaveFileDialog();
			
			// 初期ファイル名。
			ofd.FileName = txtSelect.Text.Length == 0 ? "" : Path.GetFileName(txtSelect.Text);
			// 初期オープンフォルダ。
			ofd.InitialDirectory = txtSelect.Text.Length == 0 ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : txtSelect.Text;
			// 選択可能なファイルの種類。
			ofd.Filter = fileFilter;
			// [ファイルの種類]ではじめに選択されているファイル。
			ofd.FilterIndex = fileFilterIndex;
			// タイトル。
			ofd.Title = "保存するファイルを選択してください";
			// ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする。
			ofd.RestoreDirectory = true;
			// 存在しないファイルの名前が指定されたとき警告を表示しない。
			ofd.CheckFileExists = false;
			// 存在しないパスが指定されたとき警告を表示。
			ofd.CheckPathExists = false;
			
			//ダイアログを表示する
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				path = ofd.FileName;
			}
		}
		#endregion
	}
}
