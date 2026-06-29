using C1.Win.FlexReport;
using ComponentDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C1.Win.C1Document.Export;
using System.Windows.Forms;
using System.ComponentModel;
using ComponentIO;
using C1.Win.C1Document;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using ComponentDebug;
using System.Drawing.Imaging;

namespace App
{
	/// <summary>
	/// [作成者 kj]
	/// FlexReport用管理クラス
	/// </summary>
	public class ReportFlex
	{
		/// <summary>PDF保存用拡張子</summary>
		const string PDF_EXT = "Adobe PDF|*.pdf";
		/// <summary>XLS保存用拡張子</summary>
		const string XLS_EXT = "Microsoft XLSX|*.xlsx";


		#region *** callback ***
		/// <summary>
		/// event argument for report callback
		/// </summary>
		[Serializable]
		public class ReportEventArgsEx
		{
			/// <summary>
			/// レポートで表示するビュー
			/// </summary>
			public DBView		View;
			/// <summary>
			/// 現在のページ(1～) ※プリンタダイアログの結果に合わせて 1～
			/// </summary>
			public int			Page;
			/// <summary>
			/// １ページ内の行カウンタ(0～)
			/// </summary>
			public int			Line;
			/// <summary>
			/// 全ページ分の行カウンタ(0～)
			/// </summary>
			public int			AllLine;
			/// <summary>
			/// オリジナルの eventargs
			/// </summary>
			public ReportEventArgs Source;
			
			/// <summary>
			/// .ctor
			/// </summary>
			public ReportEventArgsEx(ReportEventArgs e, DBView view, int page, int line, int allLine)
			{
				Source  = e;
				View    = view;
				Page    = page;
				Line    = line;
				AllLine = allLine;
			}
		}
		
		/// <summary>
		/// event argument for report callback at loaded.
		/// </summary>
		public class ReportLoadedEventArgs
		{
			/// <summary>
			/// report
			/// </summary>
			public C1FlexReport	Report;
			/// <summary>
			/// view in report
			/// </summary>
			public DBView		View;
			
			/// <summary>
			/// .ctor
			/// </summary>
			public ReportLoadedEventArgs(C1FlexReport report, DBView view)
			{
				Report = report;
				View = view;
			}
		}
		
		/// <summary>
		/// report event
		/// </summary>
		public delegate void ReportHandler(object sender, ReportEventArgsEx e);
		/// <summary>
		/// report event
		/// </summary>
		public delegate void ReportLoadedHandler(object sender, ReportLoadedEventArgs e);
		#endregion 
		
		#region *** ReportInfo ***
		/// <summary>
		/// 
		/// </summary>
		public class ReportInfo
		{
			/// <summary>
			/// レポート描画開始時に発生するイベント
			/// </summary>
			public event EventHandler		StartReport;
			/// <summary>
			/// ページ描画開始時に発生するイベント
			/// </summary>
			public event ReportHandler		StartPage;
			/// <summary>
			/// ページ描画終了時に発生するイベント
			/// </summary>
			public event ReportHandler		EndPage;
			/// <summary>
			/// 行描画開始時に発生するイベント　※フィールドの配置変更する場合はこのイベントのみ可能です。
			/// </summary>
			public event ReportHandler		StartSection;
			/// <summary>
			/// 行描画終了時に発生するイベント　※文字の描画のみが反映されます。フィールドの配置変更は反映されません。
			/// </summary>
			public event ReportHandler		PrintSection;
			/// <summary>
			/// レポート生成時に発生するイベント
			/// </summary>
			public event ReportLoadedHandler	ReportLoaded;
			/// <summary>
			/// 印刷処理の直前で発生するイベント
			/// </summary>
			public event EventHandler		BeforePrint;

			/// <summary>
			/// レポートファイル名
			/// </summary>
			public string		Filename
			{
				get; private set;
			}
			/// <summary>
			/// レポート名
			/// </summary>
			public string		Reportname
			{
				get; private set;
			}
			/// <summary>
			/// 表示するテーブルビュー
			/// </summary>
			public DBView		View
			{
				get; private set;
			}
			/// <summary>
			/// レポート
			/// </summary>
			public C1FlexReport	Report
			{
				get; private set;
			}
			/// <summary>
			/// 最大ページ数
			/// </summary>
			public int			MaxPage
			{
				get; private set;
			}
			/// <summary>
			/// 1ページの最大行数
			/// </summary>
			public int			MaxLine
			{
				get; private set;
			}
			/// <summary>両面印刷の有無</summary>
			public bool Duplex { get; set; }
			
			/// <summary>印刷時のスプール名</summary>
			string			docName;
			/// <summary>レポートのレイアウトマージン</summary>
			double			marginLeft = 0;
			double			marginTop = 0;
			double			marginRight = 0;
			double			marginBottom = 0;
			/// <summary>現在処理しているページ</summary>
			int				reportPage = 1;
			/// <summary>プレビューで表示中のページ番号</summary>
			int				currentPage = 1;
//			/// <summary>印刷中のページ番号</summary>
//			int				printPage = 1;
			/// <summary>1ページの行カウンタ(0～)</summary>
			int				line = 0;
			/// <summary>全ページ分の行カウンタ(0～)</summary>
			int				allLine = 0;
			
			/// <summary>StartReport イベントを抑制する場合 true。StartReport イベントを通過すると自動的に false に戻される</summary>
			bool			prohibitStartReport;
			
			/// <summary>進捗表示</summary>
			FormBg_Progress bgworker;
			/// <summary>印刷開始ページ</summary>
			int				fromPage;
			/// <summary>印刷終了ページ</summary>
			int				toPage;

			C1FlexReport	out_rep;

			/// <summary>
			/// .ctor
			/// </summary>
			public ReportInfo(string _fileName, string _repName, string _docName, int _maxLine, DBView _view)
			{
				Filename   = _fileName;
				Reportname = _repName;
				View       = _view;
				docName    = _docName;
				
				MaxLine    = _maxLine;
				if (MaxLine > 0)
				{
					MaxPage    = (View.Count / MaxLine) +  ((View.Count % MaxLine) != 0 ? 1 : 0);
				}
				
				Report	=	new C1FlexReport();
				Report.StartReport  += Rep_StartReport;
				Report.StartPage    += Rep_StartPage;
				Report.StartSection += Rep_StartSection;
				Report.PrintSection += Rep_PrintSection;
				Report.EndPage      += Rep_EndPage;
//				rep.EndReport    += Rep_EndReport;

				out_rep	=new C1FlexReport();
				out_rep.StartReport		+= Rep_StartReport;
				out_rep.StartPage		+= Rep_StartPage;
				out_rep.StartSection	+= Rep_StartSection;
				out_rep.PrintSection	+= Rep_PrintSection;
				out_rep.EndPage			+= Rep_EndPage;
			}
			
			/// <summary>
			/// dispose
			/// </summary>
			public void Dispose()
			{
				View = null;
				
				Report.Dispose();
				Report = null;

				out_rep.Dispose();
				out_rep = null;
			}
			
			/// <summary>
			/// load report
			/// </summary>
			public void LoadReport()
			{
				// ページは PrintDialog と値を合わせるため、1～
				reportPage = 1;

				Report.Clear();
				Report.Load(Filename, Reportname);
				
				marginBottom = Report.Layout.MarginBottom;
				marginLeft	 = Report.Layout.MarginLeft;
				marginRight	 = Report.Layout.MarginRight;
				marginTop	 = Report.Layout.MarginTop;
				
				//// C1Studio2013よりGDI+からGDIに変更になったので、前バージョンと印字位置がズレる現象が発生
				//// GDI+をTrueにする事で前バージョンに近い描画とさせる
				//→ FlexReport にはない
				//rep.UseGdiPlusTextRendering = true;
				
				ReportLoaded?.Invoke(this, new ReportLoadedEventArgs(Report, View));
			}
			
			/// <summary>
			/// マージンの設定
			/// </summary>
			/// <param name="horizontal">横余白</param>
			/// <param name="vertical">縦余白</param>
			public void SetMargin(int horizontal, int vertical)
			{
				// 余白を上下左右で正しく割り振る
				// 複数レポートに対応したマージン設定
				double hh = (marginTop   * (vertical - 50)) / 100;
				double ww = (marginRight * (horizontal - 50)) / 100;
				
				double dT = marginTop	 + hh;
				double dL = marginLeft	 + ww;
				double dB = marginBottom - hh;
				double dR = marginRight	 - ww;
				
				// マージン0以下は強制的に0
				Report.Layout.MarginTop		= dT >= 0 ? dT : 0;
				Report.Layout.MarginLeft	= dL >= 0 ? dL : 0;
				Report.Layout.MarginBottom	= dB >= 0 ? dB : 0;
				Report.Layout.MarginRight	= dR >= 0 ? dR : 0;
			}
			
			/// <summary>
			/// プレビュー開始ページを設定
			/// </summary>
			/// <param name="page">開始ページ</param>
			public void SetCurrentPage(int page)
			{
				currentPage	=
				reportPage	= page;
				
				setPageView(reportPage, reportPage);
			}
			
			/// <summary>
			/// 次の StartReport 呼び出しを抑制する
			/// </summary>
			public void SetProhibitStartReport()
			{
				prohibitStartReport = true;
			}
			
			/// <summary>
			/// PDF保存
			/// </summary>
			public void SavePDF()
			{
				saveContents(PDF_EXT, bg_SavePDF);
			}
			
			/// <summary>
			/// XLS保存
			/// </summary>
			public void SaveXLS()
			{
				saveContents(XLS_EXT, bg_SaveXLS);
			}
			
			void saveContents(string filter, DoWorkEventHandler eventHandler)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				
				sfd.Filter = filter;
				sfd.RestoreDirectory = true;
				
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					// レポート生成
					bgworker = new FormBg_Progress();
					bgworker.TitleText	  = "しばらくお待ちください。";
					bgworker.LabelText	  = "レポート情報作成中";
					bgworker.Arg          = sfd.FileName;
					bgworker.DoWorkEvent += eventHandler;
					bgworker.ShowDialog();
					
					if (bgworker.Arg == null)
					{
						AppMsgBox.Show(AppMsgBoxIndex.FailedWriteNgFile);
					}
					
					bgworker.Dispose();
					bgworker = null;
				}
				
				sfd.Dispose();
				sfd = null;
			}
			
			/// <summary>
			/// 印刷
			/// </summary>
			public void Print()
			{
				// 印刷設定画面の作成
				PrintDialog		dlg	= new PrintDialog();
				// 印刷ドキュメント
				PrintDocument	pd	= new PrintDocument();

				// 印刷中ダイアログは非表示にする
				pd.PrintController			 = new StandardPrintController();
				pd.DocumentName				 = docName;
				
				dlg.Document				 = pd;
				dlg.AllowSomePages			 = true;
				// プレビュー（currentPageがセットされている）時のみ選択中のページ印刷の指定が可能
				dlg.AllowSelection			 = currentPage > 0 ? true : false;
				dlg.AllowPrintToFile		 = false;
				dlg.PrinterSettings.FromPage = 1;
				dlg.PrinterSettings.ToPage	 = MaxPage;

				//¶2019/10/31 kj 
				// プリンタがサポートしている用紙サイズを取得
				foreach (System.Drawing.Printing.PaperSize psize in dlg.PrinterSettings.PaperSizes)
				{
　　				  // 最初のレポートと用紙タイプが合致したサイズを設定する
　　				  if (psize.Kind == Report.Layout.PaperSize)
　　				  {
　　　　				dlg.PrinterSettings.DefaultPageSettings.PaperSize = psize;
　　　　				break;
　　				  }
				}
				//¶2019/10/31 kj 
				
				if (dlg.ShowDialog() != DialogResult.Cancel)
				{
					BeforePrint?.Invoke(this, new EventArgs());

					// レポート生成
					bgworker = new FormBg_Progress();
					bgworker.TitleText	= "しばらくお待ちください。";
					bgworker.LabelText	= "レポート情報を出力中.....";
					bgworker.Arg = dlg.Document;
					bgworker.DoWorkEvent += new DoWorkEventHandler(bg_Print);
					bgworker.ShowDialog();
					
					bgworker.Dispose();
					bgworker = null;
				}
			}
			
			/// <summary>
			/// PDF作成処理
			/// </summary>
			void bg_SavePDF(object sender, DoWorkEventArgs e)
			{
				FormBg_Progress prog = (FormBg_Progress)sender;
				
				PdfFilter filter = new PdfFilter();
				filter.FileName = Cast.String(e.Argument);
				filter.ShowOptions = false;

				fromPage	= 1;
				toPage		= MaxPage;
				
				// 出力用のレポート生成
				makeOutRep(fromPage, toPage);

				out_rep.Export(filter);
			}
			
			/// <summary>
			/// XLS作成処理
			/// </summary>
			void bg_SaveXLS(object sender, DoWorkEventArgs e)
			{
				FormBg_Progress prog = (FormBg_Progress)sender;
				
				XlsFilter filter = new XlsFilter();
				filter.FileName = Cast.String(e.Argument);
				filter.ShowOptions		= true;
				filter.Preview			= true;
				filter.Tolerance		= 2;

				fromPage	= 1;
				toPage		= MaxPage;
				
				// 出力用のレポート生成
				makeOutRep(fromPage, toPage);

				out_rep.Export(filter);
			}
			
			/// <summary>
			/// 印刷処理
			/// </summary>
			void bg_Print(object sender, DoWorkEventArgs e)
			{
				PrintDocument pd = (PrintDocument)e.Argument;

				//■ 指定ページ分だけビューを作成
				fromPage = 1;
				toPage	 = MaxPage;

				// 範囲指定
				if (pd.PrinterSettings.PrintRange == PrintRange.SomePages)
				{
					fromPage = pd.PrinterSettings.FromPage;
					toPage = pd.PrinterSettings.ToPage;
				}
				else
				// 選択中のページ
				if (pd.PrinterSettings.PrintRange == PrintRange.Selection)
				{
					fromPage =
					toPage = currentPage;
				}

				// 出力用のレポートを指定範囲分だけ生成
				makeOutRep(fromPage, toPage);

				out_rep.DefaultPrintOptions.PrinterSettings	= pd.DefaultPageSettings.PrinterSettings;

				// 実際の出力範囲は出力用レポートの全ページ
				out_rep.DefaultPrintOptions.PrinterSettings.FromPage	= 1;
				out_rep.DefaultPrintOptions.PrinterSettings.ToPage		= out_rep.PageCount;

				out_rep.DefaultPrintOptions.PrinterSettings.DefaultPageSettings.Landscape	= out_rep.PageSettings.Landscape;

				// レポートで指定されている向きをプリンタに指定
				out_rep.DefaultPrintOptions.PrinterSettings.DefaultPageSettings.Landscape	= out_rep.PageSettings.Landscape;

				//両面印刷に対応
				if (Duplex == true && out_rep.DefaultPrintOptions.PrinterSettings.CanDuplex == true)
				{
					out_rep.DefaultPrintOptions.PrinterSettings.Duplex = System.Drawing.Printing.Duplex.Vertical;
				}

				// キュー名称をセット
				out_rep.DefaultPrintOptions.PrintJobName = docName;

				// 印刷
				out_rep.Print();
			}

			/// <summary>
			/// 指定範囲の出力用のレポート情報を取得します。
			/// </summary>
			/// <param name="from"></param>
			/// <param name="to"></param>
			/// <returns></returns>
			public C1FlexReport GetOutReport(int from, int to)
			{
				C1FlexReport orep	=	new C1FlexReport();
				orep.StartReport		+= Rep_StartReport;
				orep.StartPage			+= Rep_StartPage;
				orep.StartSection		+= Rep_StartSection;
				orep.PrintSection		+= Rep_PrintSection;
				orep.EndPage			+= Rep_EndPage;

				// 処理開始ページを印刷開始ページにしておく
				reportPage = from;

				// 印刷・外部出力用のレポートファイルを再読み込み
				orep.Load(Filename, Reportname);
				// 印刷ジョブ名を設定
				orep.DefaultPrintOptions.PrintJobName = docName;
				
				orep.Layout.MarginBottom = Report.Layout.MarginBottom;
				orep.Layout.MarginLeft	 = Report.Layout.MarginLeft;
				orep.Layout.MarginRight	 = Report.Layout.MarginRight;
				orep.Layout.MarginTop	 = Report.Layout.MarginTop;

				// ビューの生成
				DataTable	dt	= View.DataTable.Clone();
				
				for (int i = (from-1) * MaxLine; i < to * MaxLine; i++)
				{
					if (i >= View.Count)
					{
						break;
					}
					
					DataRow		row = dt.NewRow();
					
					row.BeginEdit();
					row.ItemArray = View[i].Row.ItemArray;
					row.EndEdit();
					
					dt.Rows.Add(row);
				}

				orep.DataSource.Recordset = dt;
				orep.Render();

				return orep;
			}

			/// <summary>
			/// 出力用レポートの生成
			/// </summary>
			/// <param name="from">開始ページ</param>
			/// <param name="to">終了ページ</param>
			void makeOutRep(int from , int to)
			{
				// 処理開始ページを印刷開始ページにしておく
				reportPage = fromPage;

				// 印刷・外部出力用のレポートファイルを再読み込み
				out_rep.Clear();
				out_rep.Load(Filename, Reportname);
				// 印刷ジョブ名を設定
				out_rep.DefaultPrintOptions.PrintJobName = docName;
				
				out_rep.Layout.MarginBottom	 = Report.Layout.MarginBottom;
				out_rep.Layout.MarginLeft	 = Report.Layout.MarginLeft;
				out_rep.Layout.MarginRight	 = Report.Layout.MarginRight;
				out_rep.Layout.MarginTop	 = Report.Layout.MarginTop;

				// ビューの生成
				DataTable	dt	= View.DataTable.Clone();
				
				for (int i = (from-1) * MaxLine; i < to * MaxLine; i++)
				{
					if (i >= View.Count)
					{
						break;
					}
					
					DataRow		row = dt.NewRow();
					
					row.BeginEdit();
					row.ItemArray = View[i].Row.ItemArray;
					row.EndEdit();
					
					dt.Rows.Add(row);
				}

				out_rep.DataSource.Recordset = dt;
				out_rep.Render();
			}
			
			/// <summary>
			/// 指定ページ範囲のビューを作成
			/// </summary>
			/// <param name="from">開始ページ</param>
			/// <param name="to">終了ページ</param>
			void setPageView(int from, int to)
			{
				DataTable	dt	= View.DataTable.Clone();
				
				for (int i = (from-1) * MaxLine; i < to * MaxLine; i++)
				{
					if (i >= View.Count)
					{
						break;
					}
					
					DataRow		row = dt.NewRow();
					
					row.BeginEdit();
					row.ItemArray = View[i].Row.ItemArray;
					row.EndEdit();
					
					dt.Rows.Add(row);
				}

				Report.DataSource.Recordset = dt;
			}
			
			void Rep_StartReport(object sender, EventArgs e)
			{
				if (prohibitStartReport == true)
				{
					prohibitStartReport = false;
					return;
				}
				
				line	= 0;
				allLine = 0;
				
				StartReport?.Invoke(sender, e);
			}
			
			void Rep_StartPage(object sender, ReportEventArgs e)
			{
				line	= 0;
//AppLog.WriteLine($"▼Page");
				StartPage?.Invoke(sender, new ReportEventArgsEx(e, View, reportPage, line, allLine));
			}
			
			void Rep_StartSection(object sender, ReportEventArgs e)
			{
				if (e.Section == SectionTypeEnum.Detail)
				{
//AppLog.WriteLine($"-----StartSection Page:{reportPage} /  Line:{line}");
					StartSection?.Invoke(sender, new ReportEventArgsEx(e, View, reportPage, line, allLine));
				}
			}
			
			void Rep_PrintSection(object sender, ReportEventArgs e)
			{
				if (e.Section == SectionTypeEnum.Detail)
				{
//AppLog.WriteLine($"-----PrintSection Page:{reportPage} /  Line:{line}");
					PrintSection?.Invoke(sender, new ReportEventArgsEx(e, View, reportPage, line, allLine));

					line++;
					allLine++;

					if (line % MaxLine == 0)
					{
						reportPage++;
						line = 0;
					}
				}
			}
			
			void Rep_EndPage(object sender, ReportEventArgs e)
			{
				EndPage?.Invoke(sender, new ReportEventArgsEx(e, View, reportPage, line, allLine));
				
				// update progress
				bgworker?.SetProgress(100 * (reportPage-fromPage) / (toPage+1-fromPage));
			}
		}
		#endregion

		#region *** MultiReportInfo ***
		/// <summary>
		/// 複数レポートの出力処理を管理するクラス
		/// </summary>
		public class MultiReportInfo
		{
			/*
			 * 前提条件として複数レポート内に設置されているPageHeader、PageFooterは表示されません。
			 * 各レポートのDetailのみの結合が可能です。
			 */

			/// <summary>最大ページ数</summary>
			public int MaxPage { get; private set; }
			/// <summary>カレントページ数</summary>
			public int CurrentPage { get; private set; }
			/// <summar>印刷キュー名を取得・設定します。</summar>
			public string DocmentName { get; set; }

			/// <summary>強制両面印刷の有無</summary>
			public bool ForcedDuplex { get; set; }

			/// <summary>レポート情報管理クラス</summary>
			List<ReportInfo> reps;
			/// <summary>カレントレポート</summary>
			ReportInfo currentRep;
			/// <summary>進捗表示</summary>
			FormBg_Progress bgworker;
			/// <summary>
			/// 登録されているレポート数を返します。
			/// </summary>
			public int ReportCount
			{
				get 
				{
					if (reps != null)
					{
						return reps.Count;
					}
					else
					{
						return 0;
					}				
				}
			}
			/// <summary>
			/// コンストラクタ
			/// </summary>
			public MultiReportInfo()
			{
				reps		= new List<ReportInfo>();
				currentRep	= null;
			}

			/// <summary>
			/// レポートのロード処理
			/// </summary>
			public void LoadReport()
			{
				this.CurrentPage = 1;

				for (int i = 0; i < reps.Count; i++)
				{
					reps[i].LoadReport();

					if (i == 0) currentRep = reps[i];
				}
			}

			/// <summary>
			/// 指定したページを1ページ分のみの情報として取得します。
			/// </summary>
			/// <param name="page"></param>
			/// <returns></returns>
			public C1FlexReport GetCurrentPage(int page)
			{
				int rep_page = page;

				for (int i = 0; i < reps.Count; i++)
				{
					if (reps[i].MaxPage >= rep_page)
					{
						currentRep = reps[i];
						reps[i].SetCurrentPage(rep_page);

						// レポート全体のページ数はそのままにしておく
						CurrentPage = page;
						return reps[i].Report;
					}
					else
					{
						rep_page -= reps[i].MaxPage;
					}
				}

				return null;
			}

			/// <summary>
			/// 印刷処理
			/// </summary>
			public void Print()
			{
				// 印刷設定画面の作成
				PrintDialog		dlg	= new PrintDialog();
				// 印刷ドキュメント
				PrintDocument	pd	= new PrintDocument();

				// 印刷中ダイアログは非表示にする
				pd.PrintController			 = new StandardPrintController();
				pd.DocumentName				 = DocmentName;

				dlg.Document				 = pd;
				dlg.AllowSomePages			 = true;
				// プレビュー（currentPageがセットされている）時のみ選択中のページ印刷の指定が可能
				dlg.AllowSelection			 = this.CurrentPage > 0 ? true : false;
				dlg.AllowPrintToFile		 = false;
				dlg.PrinterSettings.FromPage = 1;
				dlg.PrinterSettings.ToPage	 = MaxPage;

				//¶2019/10/31 kj 
				// プリンタがサポートしている用紙サイズを取得
				foreach (System.Drawing.Printing.PaperSize psize in dlg.PrinterSettings.PaperSizes)
				{
　　				  // 最初のレポートと用紙タイプが合致したサイズを設定する
　　				  if (psize.Kind == reps[0].Report.Layout.PaperSize)
　　				  {
　　　　				dlg.PrinterSettings.DefaultPageSettings.PaperSize = psize;
　　　　				break;
　　				  }
				}
				//¶2019/10/31 kj 
				
				if (dlg.ShowDialog() != DialogResult.Cancel)
				{
					// レポート生成
					bgworker = new FormBg_Progress();
					bgworker.TitleText	= "しばらくお待ちください。";
					bgworker.LabelText	= "レポート情報を出力中.....";
					bgworker.Arg = dlg.Document;
					bgworker.DoWorkEvent += new DoWorkEventHandler(bg_Print);
					bgworker.ShowDialog();
					
					bgworker.Dispose();
					bgworker = null;
				}
			}

			/// <summary>
			/// 印刷処理
			/// </summary>
			void bg_Print(object sender, DoWorkEventArgs e)
			{
				FormBg_Progress prog = (FormBg_Progress)sender;

				PrintDocument pd = (PrintDocument)e.Argument;

				//pd.PrinterSettings.DefaultPageSettings.Landscape = true;

				//■ 指定ページ分だけビューを作成
				int fromPage = 1;
				int toPage	 = MaxPage;
				
				// 範囲指定
				if (pd.PrinterSettings.PrintRange == PrintRange.SomePages)
				{
					fromPage = pd.PrinterSettings.FromPage;
					toPage	 = pd.PrinterSettings.ToPage;
				}
				else
				// 選択中のページ
				if (pd.PrinterSettings.PrintRange == PrintRange.Selection)
				{
					fromPage = 
					toPage	 = CurrentPage;
				}

				C1FlexReport outrep = new C1FlexReport();
				outrep.Sections.Detail.Visible = true;

				// レポートの生成
				makeOutReport(outrep, fromPage, toPage);
				outrep.DefaultPrintOptions.PrinterSettings			= pd.DefaultPageSettings.PrinterSettings;
				outrep.DefaultPrintOptions.PrinterSettings.FromPage = 1;
				outrep.DefaultPrintOptions.PrinterSettings.ToPage	= outrep.PageCount;

				//両面印刷に対応
				if (ForcedDuplex == true && outrep.DefaultPrintOptions.PrinterSettings.CanDuplex == true)
				{
					outrep.DefaultPrintOptions.PrinterSettings.Duplex = System.Drawing.Printing.Duplex.Vertical;
					outrep.Print();
					//Form frm = new Form();
					//C1.Win.FlexViewer.C1FlexViewerPane view = new C1.Win.FlexViewer.C1FlexViewerPane();
					//view.SetBounds(0,0, frm.Width - 30, frm.Height -30);
					//view.DocumentSource = outrep;
					//view.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
					//frm.Controls.Add(view);
					//frm.ShowDialog();
				}
				else
				{
					//サブレポートを個別に印刷(印刷向きを合わせるため)
					for (int i = 0; i < outrep.Fields.Count; i++)
					{
						prog.SetProgress(100 * (i + 1) / outrep.Fields.Count);

						C1FlexReport sub = ((SubreportField)outrep.Fields[i]).Subreport;
						C1.Win.C1Document.C1PrintOptions option = new C1.Win.C1Document.C1PrintOptions();
						option.PrinterSettings = pd.DefaultPageSettings.PrinterSettings;

						if (sub.Layout.Orientation == OrientationEnum.Portrait)
						{
							sub.PageSettings.Landscape = false;
							option.PrinterSettings.DefaultPageSettings.Landscape = false;
						}
						else
						if (sub.Layout.Orientation == OrientationEnum.Landscape)
						{
							sub.PageSettings.Landscape = true;
							option.PrinterSettings.DefaultPageSettings.Landscape = true;
						}

						//¶2019/12/23 kj キュー名称をオプションクラスへ渡しておく
						option.PrintJobName = sub.DefaultPrintOptions.PrintJobName;

						try
						{
							sub.Print(option);
						}
						catch (Exception ex)
						{
							//ファイルが開かれてる警告
							ErrLog.WriteException(ex);
						}
					}
				}

			}

			/// <summary>
			/// 出力用レポートの生成
			/// </summary>
			/// <param name="outrep"></param>
			/// <param name="fromPage"></param>
			/// <param name="toPage"></param>
			void makeOutReport(C1FlexReport outrep, int fromPage, int toPage)
			{
				if (outrep != null)
				{
					// 出力用のレポート生成
					for (int i = 0; i < reps.Count; i++)
					{
						if (reps[i].MaxPage >= fromPage && fromPage > 0)
						{
							if (reps[i].MaxPage >= toPage)
							{
								for(int j = fromPage; j <= toPage; j++)
								{
									setSubRepPage(outrep, reps[i], j);
								}
								break;
							}
							else
							{
								for(int j = fromPage; j <= reps[i].MaxPage; j++)
								{
									setSubRepPage(outrep, reps[i], j);
								}

								fromPage  = 0;
								toPage	 -= reps[i].MaxPage;
							}
						}
						else
						if (fromPage == 0)
						{
							if (reps[i].MaxPage >= toPage)
							{
								// ここまで
								for(int j = 1; j <= toPage; j++)
								{
									setSubRepPage(outrep, reps[i], j);
								}
								break;
							}
							else
							{
								// 範囲外であれば、途中のレポートは全て出力
								for(int j = 1; j <= reps[i].MaxPage; j++)
								{
									setSubRepPage(outrep, reps[i], j);
								}

								toPage	 -= reps[i].MaxPage;
							}
						}
						else
						{
							fromPage -= reps[i].MaxPage;
							toPage	 -= reps[i].MaxPage;
						}
					}

					outrep.Generate();
				}
			}

			/// <summary>
			/// 指定したレポートのページをサブレポートとして登録します。
			/// </summary>
			/// <param name="rep"></param>
			/// <param name="ri"></param>
			/// <param name="page"></param>
			void setSubRepPage(C1FlexReport rep, ReportInfo ri, int page)
			{
				SubreportField fld = new SubreportField();
				fld.Top		= rep.Sections.Detail.Fields.Count * 10;
				fld.Height	= 10;
				fld.Width	= 10;

				// 1ページ分だけをサブレポート化
				fld.Subreport = ri.GetOutReport(page, page);

				// 既に他のレポートがあれば改ページさせる
				if (rep.Sections.Detail.Fields.Count > 0)
				{
					fld.ForcePageBreak = ForcePageBreakEnum.Before;
				}
				else
				{
					// 複数レポートの場合は、1ページ目となるレポートの余白設定に準拠させる
					rep.Layout.MarginTop	= fld.Subreport.Layout.MarginTop;
					rep.Layout.MarginLeft	= fld.Subreport.Layout.MarginLeft;
					rep.Layout.MarginRight	= 
					rep.Layout.MarginBottom	= 0;
					rep.Layout.PaperSize	= fld.Subreport.Layout.PaperSize;
					rep.Layout.Orientation	= fld.Subreport.Layout.Orientation;
				}

				rep.Sections.Detail.Fields.Add(fld);
			}

			/// <summary>
			/// PDF出力
			/// </summary>
			public void SavePDF()
			{
				saveContents(PDF_EXT, bg_SavePDF);
			}

			/// <summary>
			/// PDF作成処理
			/// </summary>
			void bg_SavePDF(object sender, DoWorkEventArgs e)
			{
				FormBg_Progress prog = (FormBg_Progress)sender;

				string path = Cast.String(e.Argument);

				int fromPage	= 1;
				int toPage		= MaxPage;
				
				C1FlexReport outrep = new C1FlexReport();
				outrep.Sections.Detail.Visible = true;

				// 出力用のレポート生成
				makeOutReport(outrep, fromPage, toPage);

				C1.C1Pdf.C1PdfDocument pdf = new C1.C1Pdf.C1PdfDocument();
				pdf.Clear();

				//レポートの向きを合わせる
				for (int i = 0; i < outrep.Fields.Count; i++)
				{
					prog.SetProgress(100*(i+1)/outrep.Fields.Count);

					C1FlexReport sub = ((SubreportField)outrep.Fields[i]).Subreport;

					if (sub.Layout.Orientation == OrientationEnum.Portrait)
					{
						sub.PageSettings.Landscape = false;
					}
					else
					if (sub.Layout.Orientation == OrientationEnum.Landscape)
					{
						sub.PageSettings.Landscape = true;
					}

					if (i > 0) pdf.NewPage();

					pdf.Landscape = sub.PageSettings.Landscape;
					pdf.PaperKind = sub.PageSettings.PaperSize;
					pdf.FontType = C1.C1Pdf.FontTypeEnum.Embedded;

					Metafile meta =	sub.GetPageImage(0);
					pdf.DrawImage(meta, pdf.PageRectangle);
				}

				try
				{
					if (pdf.Pages.Count >= 0)
					{
						pdf.Save(path);
					}
				}
				catch (Exception ex)
				{
					//ファイルが開かれてる警告
					ErrLog.WriteException(ex);
				}
			}

			/// <summary>
			/// XLS出力
			/// </summary>
			public void SaveXLS()
			{
				saveContents(XLS_EXT, bg_SaveXLS);
			}

			/// <summary>
			/// XLS作成処理
			/// </summary>
			void bg_SaveXLS(object sender, DoWorkEventArgs e)
			{
				FormBg_Progress prog = (FormBg_Progress)sender;
				
				XlsFilter filter = new XlsFilter();
				filter.FileName = Cast.String(e.Argument);
				filter.ShowOptions		= true;
				filter.Preview			= true;
				filter.Tolerance		= 2;

				int fromPage	= 1;
				int toPage		= MaxPage;
				
				C1FlexReport outrep = new C1FlexReport();
				outrep.Sections.Detail.Visible = true;

				// 出力用のレポート生成
				makeOutReport(outrep, fromPage, toPage);

				outrep.Export(filter);
			}

			void saveContents(string filter, DoWorkEventHandler eventHandler)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				
				sfd.Filter = filter;
				sfd.RestoreDirectory = true;
				
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					// レポート生成
					bgworker = new FormBg_Progress();
					bgworker.TitleText	  = "しばらくお待ちください。";
					bgworker.LabelText	  = "レポート情報作成中";
					bgworker.Arg          = sfd.FileName;
					bgworker.DoWorkEvent += eventHandler;
					bgworker.ShowDialog();
					
					if (bgworker.Arg == null)
					{
//						AppMsgBox.Show(AppMsgBoxIndex.FailedWriteNgFile);
					}
					
					bgworker.Dispose();
					bgworker = null;
				}
				
				sfd.Dispose();
				sfd = null;
			}

			/// <summary>
			/// 余白設定
			/// </summary>
			/// <param name="horizontal"></param>
			/// <param name="vertical"></param>
			public void SetMargin(int horizontal, int vertical)
			{
				for (int i = 0; i < reps.Count; i++)
				{
					reps[i].SetMargin(horizontal, vertical);
				}
			}

			/// <summary>
			/// レポート情報を取得します。
			/// </summary>
			/// <returns></returns>
			public ReportInfo GetReportInfo(string repname)
			{
				return reps.Find(n => n.Reportname == repname);
			}

			/// <summary>
			/// レポートクラスの追加処理
			/// </summary>
			/// <param name="rep">レポート</param>
			public void AddReportInfo(ReportInfo rep)
			{
				if (rep != null)
				{
					reps.Add(rep);
					this.MaxPage += rep.MaxPage;
				}
			}

			/// <summary>
			/// 解放処理
			/// </summary>
			public void Dispose()
			{
				foreach(ReportInfo rep in reps)
				{
					rep.Dispose();
				}
			}
		}
		#endregion
	}
}
