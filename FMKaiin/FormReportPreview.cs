using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.FlexReport;
using C1.Win.FlexViewer;
using ComponentDB;
using ComponentForm;
using ComponentIO;
using ComponentRegistry;
using C1.Win.C1Document.Export.Ssrs;
using C1.Win.C1Document.Export;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// report view (Flex Report Version)
	/// </summary>
	public partial class FormReportPreview : FormFrame
	{
		/// <summary>
		/// レポート
		/// </summary>
		ReportFlex.ReportInfo repInfo;
		/// <summary>
		/// カレントページ
		/// </summary>
		int			currentPage;
		/// <summary>
		/// 余白設定に使用されるレジストリヘッダ
		/// </summary>
		string		marginName;
		/// <summary>
		/// 横のマージン
		/// </summary>
		int			marginLeft;
		/// <summary>
		/// 縦のマージン
		/// </summary>
		int			marginTop;

		/// <summary>
		/// .ctor
		/// </summary>
		public FormReportPreview()
		{
			InitializeComponent();
			
			currentPage = 1;
			marginName	= "blank";
			KeyPreview	= true;
			
			layout = new FormLayout(this);
			layout.AddControl(ppc, FormLayoutRule.None, FormLayoutRule.None, FormLayoutRule.LinkBottom, FormLayoutRule.LinkBottom);
			layout.EndAdd();
			
			//// 開発時は邪魔らしいので普通のサイズで・・・
			//if (System.Diagnostics.Debugger.IsAttached == false)
			{
				// 最大化を標準とする。
				this.WindowState = FormWindowState.Maximized;
			}
			
			this.SizeChanged += FormReportPreview_SizeChanged;
		}

		/// <summary>
		/// 初回描画
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// マージンの初期化
			int		hval = Cast.Int(RegCommon.Read(marginName + "_H"), 50);
			int		vval = Cast.Int(RegCommon.Read(marginName + "_V"), 50);
			
			if (hval < 0)
			{
			    hval = 0;
			}
			else if (hval > 100)
			{
			    hval = 100;
			}
			
			if (vval < 0)
			{
			    vval = 0;
			}
			else if (vval > 100)
			{
			    vval = 100;
			}
			
			marginLeft	= hval;
			marginTop	= vval;
			
			tsZoom.MaxLength = 4;
			tsPage.MaxLength = 4;
			
			// レポート情報の初期化
			repInfo.LoadReport();

			// 最初のページを表示
			refreshPane();
			// 初期はページ全体で描画
			updateZoom(0);
			
			// event
			tsPageTop.Click		+= new EventHandler(tsPageTop_Click);
			tsPagePrev.Click	+= new EventHandler(tsPagePrev_Click);
			tsPageNext.Click	+= new EventHandler(tsPageNext_Click);
			tsPageEnd.Click		+= new EventHandler(tsPageEnd_Click);
			tsPage.KeyDown		+= new KeyEventHandler(tsPage_KeyDown);
			tsPage.KeyPress		+= new KeyPressEventHandler(ts_KeyPress_NumberOnly);
			tsPage.MouseUp		+= new MouseEventHandler(ts_MouseUp_SelectAll);
			tsPage.Leave		+= new EventHandler(tsPage_Leave);
			
			tsSetting.Click		+= new EventHandler(tsSetting_Click);
			tsPrint.Click		+= new EventHandler(tsPrint_Click);
			tsSave.Click		+= new EventHandler(tsPDF_Click);
			
			tsZC_400.Click		+= new EventHandler(tsZC_Click);
			tsZC_200.Click		+= new EventHandler(tsZC_Click);
			tsZC_150.Click		+= new EventHandler(tsZC_Click);
			tsZC_125.Click		+= new EventHandler(tsZC_Click);
			tsZC_100.Click		+= new EventHandler(tsZC_Click);
			tsZC_75.Click		+= new EventHandler(tsZC_Click);
			tsZC_50.Click		+= new EventHandler(tsZC_Click);
			tsZC_10000.Click	+= new EventHandler(tsZC_Click);
			tsZC_10001.Click	+= new EventHandler(tsZC_Click);
			tsZoom.KeyDown		+= new KeyEventHandler(tsZoom_KeyDown);
			tsZoom.KeyPress		+= new KeyPressEventHandler(ts_KeyPress_NumberOnly);
			tsZoom.MouseUp		+= new MouseEventHandler(ts_MouseUp_SelectAll);
			tsZoomPlus.Click	+= new EventHandler(tsZoomPlus_Click);
			tsZoomMinus.Click	+= new EventHandler(tsZoomMinus_Click);
			
			// last call
			base.FormFrame_Shown(sender, e);
		}
		
		void FormReportPreview_SizeChanged(object sender, EventArgs e)
		{
			// ページ全体の場合にフォームサイズに合わせて倍率表示を変更
			if (ppc.ZoomMode == FlexViewerZoomMode.WholePage)
			{
				updateZoom(0);
			}
		}
		
		/// <summary>
		/// レポートをセットする
		/// </summary>
		/// <param name="_repinfo">レポート情報クラス</param>
		public void SetReport(ReportFlex.ReportInfo _repinfo)
		{
			repInfo = _repinfo;
		}
		
		/// <summary>
		/// レポートをレポート名から取得
		/// </summary>
		/// <param name="reportName">レポート名</param>
		/// <returns>レポート情報</returns>
		public ReportFlex.ReportInfo GetReport(string reportName)
		{
			return repInfo;
		}
		
		#region Zoom
		int[]	zoomList = { 1000, 800, 400, 200, 150, 125, 100, 65, 50, 25, 5 };
		
		/// <summary>
		/// ＋
		/// </summary>
		void tsZoomPlus_Click(object sender, EventArgs e)
		{
			int	zoom = Cast.Int(tsZoom.Text.Replace("%", ""), -1);
			
			if (zoom < 0)
			{
				zoom = 100;
			}
			
			for (int i = zoomList.Length - 1; i > -1; i--)
			{
				if (zoom < zoomList[i])
				{
					updateZoom(zoomList[i]);
					break;
				}
			}
		}
		
		/// <summary>
		/// －
		/// </summary>
		void tsZoomMinus_Click(object sender, EventArgs e)
		{
			int	zoom = Cast.Int(tsZoom.Text.Replace("%", ""), -1);
			
			if (zoom < 0)
			{
				zoom = 100;
			}
			
			for (int i = 0; i < zoomList.Length; i++)
			{
				if (zoom > zoomList[i])
				{
					updateZoom(zoomList[i]);
					break;
				}
			}
		}
		
		/// <summary>
		/// 直接入力
		/// </summary>
		void tsZoom_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				updateZoom(Cast.Int(tsZoom.Text.Replace("%", ""), -1));
			}
		}
		
		/// <summary>
		/// コンボ選択
		/// </summary>
		void tsZC_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem	mi = (ToolStripMenuItem)sender;
			int					no = Cast.Int(mi.Name.Remove(0, mi.Name.IndexOf('_') + 1));
			
			switch (no)
			{
				case 10000	: no = 100;	break;
				case 10001	: no = 0;	break;
				default		:			break;
			}
			updateZoom(no);
		}
		#endregion
		
		#region Page
		/// <summary>
		/// 直接入力
		/// </summary>
		void tsPage_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				updatePage(Cast.Int(tsPage.Text));
			}
		}
		
		void ts_KeyPress_NumberOnly(object sender, KeyPressEventArgs e)
		{
			// フォーカス遷移後の音を消すためにキーイベントが処理されたことにする
			//if (e.KeyChar == (char)Keys.Enter) 
			//{
			//    e.Handled = true;
			//}
			// 数字以外入力させない
			if (e.KeyChar == (char)Keys.Enter)
			{
				//
			}
			else
			if (e.KeyChar == (char)Keys.Delete)
			{
				//
			}
			else
			if (e.KeyChar == (char)Keys.Back)
			{
				//
			}
			else
			if (e.KeyChar < '0' || e.KeyChar > '9')
			{
				e.Handled = true;
			}
		}
		
		/// <summary>
		/// ページ直接入力
		/// </summary>
		void tsPage_Leave(object sender, EventArgs e)
		{
			updatePage(Cast.Int(tsPage.Text));
		}
		/// <summary>
		/// 先頭に
		/// </summary>
		void tsPageTop_Click(object sender, EventArgs e)
		{
			int		page = 1;
			
			if (page != currentPage)
			{
				currentPage = page;
				refreshPane();
			}
		}
		
		/// <summary>
		/// １ページ前
		/// </summary>
		void tsPagePrev_Click(object sender, EventArgs e)
		{
			int		page = currentPage;
			
			if (--page < 1)
			{
				page = 1;
			}
			if (page != currentPage)
			{
				currentPage = page;
				refreshPane();
			}
		}
		/// <summary>
		/// １ページ後
		/// </summary>
		void tsPageNext_Click(object sender, EventArgs e)
		{
			int		page = currentPage;
			
			if (++page > repInfo.MaxPage)
			{
				page = repInfo.MaxPage;
			}
			if (page != currentPage)
			{
				currentPage = page;
				refreshPane();
			}
		}
		/// <summary>
		/// 末尾に
		/// </summary>
		void tsPageEnd_Click(object sender, EventArgs e)
		{
			int		page = repInfo.MaxPage;
			
			if (page != currentPage)
			{
				currentPage = page;
				refreshPane();
			}
		}
		#endregion
		
		/// <summary>
		/// 余白設定
		/// </summary>
		void tsSetting_Click(object sender, EventArgs e)
		{
			//■ 余白設定画面の表示
			FormOutBlankSetting	form = new FormOutBlankSetting("blank", marginLeft, marginTop);
			form.ShowDialog();
			
			if (form.Cancel == false)
			{
				RegCommon.Write(marginName + "_H", form.MarginH);
				RegCommon.Write(marginName + "_V", form.MarginV);
				marginLeft	= form.MarginH;
				marginTop	= form.MarginV;
				
				repInfo.SetMargin(marginLeft, marginTop);
				refreshPane();
			}
			
			form.Dispose();
			form = null;
		}
		
		/// <summary>
		/// 印刷
		/// </summary>
		void tsPrint_Click(object sender, EventArgs e)
		{
			// 直接印刷
			repInfo.Print();
		}
		
		/// <summary>
		/// PDF作成
		/// </summary>
		void tsPDF_Click(object sender, EventArgs e)
		{
			// PDF作成
			repInfo.SavePDF();
		}
		
		void ts_MouseUp_SelectAll(object sender, MouseEventArgs e)
		{
			ToolStripTextBox	tb = (ToolStripTextBox)sender;
			
			// 全選択から入力
			tb.SelectAll();
		}
		
		/// <summary>
		/// ページ更新
		/// </summary>
		void updatePage(int page)
		{
			if (page < 1)
			{
				page = 1;
			}
			if (page >= repInfo.MaxPage)
			{
				page = repInfo.MaxPage;
			}
			
			if (page != currentPage)
			{
				currentPage = page;
				refreshPane();
			}
			else
			{
				tsPage.Text	= currentPage.ToString();
				tsPage.SelectAll();
			}
		}
		
		/// <summary>
		/// 指定されたZoom値を適正までまるめ、設定する
		/// </summary>
		/// <param name="zoom">ズーム値(-1..Error, 範囲 0 - 1000, 0..ページ全体)</param>
		void updateZoom(int	zoom)
		{
			if (zoom < 0)
			{
				// エラーは100に
				zoom = 100;
			}
			if (zoom < 0)
			{
				zoom = 0;
			}
			if (zoom > 1000)
			{
				zoom = 1000;
			}
			
			// ドロップダウンのチェックを初期化
			foreach(object obj in tsZoomCombo.DropDownItems)
			{
				if (obj is ToolStripDropDownItem)
				{
					ToolStripMenuItem smi = (ToolStripMenuItem)obj;
					smi.Checked = false;
				}
			}
			
			List<int> tsz_menu = new List<int>()
			{
				400, 200, 150, 125, 100, 75, 50,
			};
			
			if (tsz_menu.Contains(zoom) == true)
			{
				string item_name = string.Format("tsZC_{0}", zoom);
				
				if (tsZoomCombo.DropDownItems.ContainsKey(item_name) == true)
				{
					((ToolStripMenuItem)tsZoomCombo.DropDownItems[item_name]).Checked = true;
				}
			}
			
			if (zoom == 100)
			{
				ppc.ZoomMode		= FlexViewerZoomMode.ActualSize;
				tsZC_10000.Checked	= true;
			}
			else
			if (zoom == 0)
			{
				ppc.ZoomMode		= FlexViewerZoomMode.WholePage;
				tsZC_10001.Checked = true;
				zoom = (int)(ppc.ZoomFactor * 100);
			}
			else
			{
				ppc.ZoomMode = FlexViewerZoomMode.Custom;
				ppc.ZoomFactor = (float)zoom / 100;
			}
			
			tsZoom.Text = string.Format("{0}%", zoom);
			tsZoom.SelectAll();
		}
		
		/// <summary>
		/// ページ表示の更新
		/// </summary>
		void refreshPane()
		{
			this.Cursor = Cursors.WaitCursor;
			
			toolStrip.Enabled = false;
			
			C1FlexReport	rep = repInfo.Report;
			
			// 現在のページを表示
			repInfo.SetCurrentPage(currentPage);

			ppc.DocumentSource = rep;
			ppc.Select();
			
			// 
			tsPage.Text	= currentPage.ToString();
			tsMaxPage.Text	= string.Format("/{0}", repInfo.MaxPage);
			
			// 先頭、終端ページでの押せるボタン、押せないボタンの制御
			if (currentPage == 1)
			{
				tsPageTop.Enabled	= 
				tsPagePrev.Enabled	= false;
			}
			else
			{
				tsPageTop.Enabled	= 
				tsPagePrev.Enabled	= true;
			}
			if (currentPage == repInfo.MaxPage)
			{
				tsPageEnd.Enabled	= 
				tsPageNext.Enabled	= false;
			}
			else
			{
				tsPageEnd.Enabled	= 
				tsPageNext.Enabled	= true;
			}
			
			toolStrip.Enabled = true;
			
			this.Cursor = Cursors.Default;
		}
	}
}
