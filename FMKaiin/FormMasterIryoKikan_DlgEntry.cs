using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using ComponentDB;
using ComponentGControlDB;
using ComponentGGridDB;
using ComponentIO;

namespace App
{
	public partial class FormMasterIryoKikan_DlgEntry:FormFrame
	{
		GControlDB gctl;

		/// <summary>
		/// 処理モード
		/// </summary>
		public enum eMode
		{
			/// <summary>追加</summary>
			Add,
			/// <summary>訂正</summary>
			Edit,
		}

		/// <summary>
		/// 処理モード
		/// </summary>
		public eMode Mode
		{
			get; set;
		}

		/// <summary>
		/// 開設主体情報 DBView
		/// </summary>
		DBView dvKaisetsu;

		// --診療科目Grid--
		/// <summary>
		/// 診療科マスタ DBView
		/// </summary>
		DBView dvShinryo;
		/// <summary>
		/// 医療機関-診療科の関連 DBView
		/// </summary>
		public DBView DvIryokikanShinryo
		{
			//get { return dvIryokikanShinryo; }
			set { dvIryokikanShinryo = new DBView(value, this.BindingContext); } // DlgEntry画面でBindするため、DBViewを引数にnewして別Viewを準備(DataTableは同じ)
		}
		DBView dvIryokikanShinryo;

		/// <summary>
		/// 診療科目Grid
		/// </summary>
		GGridDBCommon gcom_shinryo;
		/// <summary>
		/// コントロールとデータ連結クラス_診療科目Grid用
		/// </summary>
		GControlDB gctl_shinryo;

		/// <summary>
		/// 起動時のレコード状態
		/// </summary>
		DataRow srcrow = null;
		/// <summary>
		/// 編集するレコード
		/// </summary>
		DataRow editrow = null;

		/// <summary>
		/// 取引先用レコード
		/// </summary>
		public DataRow Row
		{
			get
			{
				return editrow;
			}
			set
			{
				srcrow = value;
				if (this.Mode == eMode.Add)
				{
					editrow = srcrow;
				}
				else
				{
					editrow = srcrow.Table.NewRow();
					AppDb.CopyDataRow(srcrow, editrow);
				}
			}
		}

		public FormMasterIryoKikan_DlgEntry()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// コンボの作成
			// 手動セット
			iByoshoUmu.ExBeginUpdate();
			iByoshoUmu.ExAddItem("有", true);
			iByoshoUmu.ExAddItem("無", false);
			iByoshoUmu.ExEndUpdate();

			iTaikaiKbn.ExBeginUpdate();
			iTaikaiKbn.ExAddItem("会員", true);
			iTaikaiKbn.ExAddItem("退会", false);
			iTaikaiKbn.ExEndUpdate();

			// iSearchセット
			dvKaisetsu = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_kaisetsushutai, $"ORDER BY {t_kaisetsushutai.FKST_Code}"), this.BindingContext);
			AppTableCombo.SetComboBox_Kaisetsushutai(iKaisetsushutai, dvKaisetsu);

			// 画面～DBの紐づけ
			gctl = new GControlDB(this, getDataRow, AppGlobal.DB.DBZipCode);

			gctl.Add(new GControlDBText(t_iryokikan.FIRK_Code, iCode));
			gctl.Add(new GControlDBRuby(
							new string[] {
								t_iryokikan.FIRK_Name,
								t_iryokikan.FIRK_Kana},
							new Control[] {
								iShisetsuName,
								iShisetsuKana
							}));
			gctl.Add(new GControlDBText(t_iryokikan.FIRK_Tsusho, iShisetsuTsusho));
			gctl.Add(new GControlDBText(t_iryokikan.FIRK_TsushoKana, iShisetsuTsushoKana));

			gctl.Add(new GControlDBPostAddr(
							new string[]{
								t_iryokikan.FIRK_Post,
								t_iryokikan.FIRK_Addr1
							},
							new Control[]{
								iPost1,
								iPost2,
								iAddr1
							}));
			gctl.Add(new GControlDBText(t_iryokikan.FIRK_Addr2, iAddr2));
			gctl.Add(new GControlDBHyphenSplit(t_iryokikan.FIRK_Tel1, new Control[] { iTel1_1, iTel1_2, iTel1_3 }));
			gctl.Add(new GControlDBHyphenSplit(t_iryokikan.FIRK_Fax1, new Control[] { iTel2_1, iTel2_2, iTel2_3 }));
			gctl.Add(new GControlDBCombo(t_iryokikan.FIRK_KaisetsuShutai, iKaisetsushutai.ComboBox));
			gctl.Add(new GControlDBCombo(t_iryokikan.FIRK_ByoshoUmu, iByoshoUmu)); // truefalse
			gctl.Add(new GControlDBText(t_iryokikan.FIRK_Kyoka, iKyokabyosho));
			gctl.Add(new GControlDBCheckBox(t_iryokikan.FIRK_Kaigo, chkKaigo));
			gctl.Add(new GControlDBCheckBox(t_iryokikan.FIRK_Etc, chkEtc));
			gctl.Add(new GControlDBText(t_iryokikan.FIRK_Memo, iHeisetsu));
			gctl.Add(new GControlDBText(t_iryokikan.FIRK_KumiCode, iKumiCode));
			gctl.Add(new GControlDBCombo(t_iryokikan.FIRK_TaikaiKbn, iTaikaiKbn));

			gctl.EndAdd(AppDbRule.Rule);

			rowFetch();

			iCode.Select();

			//-----担当診療科目Grid準備-----
			// ■データ取得
			dvShinryo = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_shinryoka, $"ORDER BY {t_shinryoka.FSRK_Code}"), this.BindingContext);

			// ソート順追加など、任意並び替えがあるならRefill側を使う
			// 追加時の動作仕様のため、再表示時にのみソートする(GetReFillTable()の利用)
			//			dvKaiinShinryo = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_kaiin_shinryoka, $"ORDER BY {t_kaiin_shinryoka.SRK_Code}"), this.BindingContext);
//			dvIryokikanShinryo = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_iryokikan_shinryoka), this.BindingContext); // BindingContext:DBViewのカレント行とGridのカレント行追従させるため必要

			// ■コントロール設定
			AppTableCombo.SetComboBox_Shinryoka(iHyoboKamoku, dvShinryo); // iSearch

			// ■Grid設定
			AppGridCommon.StyleSet(grid_Sinryo);

			gcom_shinryo = new GGridDBCommon(grid_Sinryo, this);

			gcom_shinryo.Add(new GGridDBText(t_iryokikan_shinryoka.FID_Shinryoka, "名称", 60));
			gcom_shinryo.SetCellDisp(GGridDBCellDisp.Left);
			gcom_shinryo.SetFocusControlInGrid(iHyoboKamoku); // Gridフォーカス時に表示させるコントロール
			gcom_shinryo.SetUnboundColumnFetch(ubShinryokaName); // ID→名称変換
			gcom_shinryo.SetTabIndex(1);
			gcom_shinryo.SetLocked(true);

			gcom_shinryo.EndAdd(dvIryokikanShinryo);

			// コントロールのDBバインド
			gctl_shinryo = new GControlDB(this, getDataRowShinryo);
			gctl_shinryo.Add(new GControlDBCombo(t_iryokikan_shinryoka.FID_Shinryoka, iHyoboKamoku.ComboBox));
			gctl_shinryo.EndAdd(AppDbRule.Rule);

			// イベント登録
			btnKamokuAdd.Click += btnKamokuAdd_Click;
			btnKamokuDel.Click += btnKamokuDel_Click;
						
			dvIryokikanShinryo.RowFetchDemand += dvIryokikanShinryo_RowFetchDemand;

			changeFilterShinryoka(); // 表示中会員の情報のみへフィルタ
			rowFetchShinryo(); // コントロール値セット(グリッド初期表示値)

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// IDを基に、Gridに診療科名を表示します。
		/// </summary>
		string ubShinryokaName(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_iryokikan_shinryoka xrow = new t_iryokikan_shinryoka(dvIryokikanShinryo[e.Row]);

			Shinryoka sk = AppGlobal.Shinryokas.Get(xrow.ID_Shinryoka);

			string name = "";
			if (sk != null)
			{
				name = sk.XRow.SRK_Name;
			}

			return name;
		}

		/// <summary>
		/// 診療科目gridの現在行を返します。
		/// </summary>
		/// <param name="datatag">タグ</param>
		/// <returns>現在行</returns>
		DataRow getDataRowShinryo(string datatag)
		{
			if (dvIryokikanShinryo.CurrentRow != null && dvIryokikanShinryo.Count > 0)
			{
				return dvIryokikanShinryo.CurrentRow.Row;
			}

			return null;
		}

		/// <summary>
		/// フィルタ変更(t_kaiin_shinryoka)
		/// </summary>
		void changeFilterShinryoka()
		{
			string filter = "";

			// 該当医療機関の診療科目情報へフィルタ
			t_iryokikan tmprow = new t_iryokikan(editrow);

			filter =
				$"{t_iryokikan_shinryoka.FID_Iryokikan} = {tmprow.ID_Iryokikan}";

			// 適用
			dvIryokikanShinryo.RowFilterQuery(filter);
		}

		/// <summary>
		/// Gridカレント行の内容を各コントロールにセットします(標榜診療科目Grid)
		/// </summary>
		void rowFetchShinryo()
		{
			if (gctl_shinryo == null)
			{
				//				Debug.WriteLine("GControlDBが設定されていません。");
				return;
			}

			if (dvIryokikanShinryo.CurrentRow == null)
			{
				// 現在行がないので、コントロールの内容をクリアする。
				gctl_shinryo.ClearAll();
			}
			else
			{
				// 現在行を取得
				gctl_shinryo.FetchAll();
			}

			//setEnableFunctionButton();
		}

		/// <summary>
		/// 追加(標榜診療科目Grid)
		/// </summary>
		private void btnKamokuAdd_Click(object sender, EventArgs e)
		{
			t_iryokikan tmprow = new t_iryokikan(editrow);
			t_iryokikan_shinryoka nrow = new t_iryokikan_shinryoka(dvIryokikanShinryo.NewRow());

			// NewRowへフィルタ済の会員IDセット
			nrow.ID_Iryokikan = tmprow.ID_Iryokikan;

			// 行追加
			dvIryokikanShinryo.Add(nrow.Row);
			// 追加した行をカレント行にする
			AppGridCommon.SetRow(grid_Sinryo, dvIryokikanShinryo.Count - 1);

			// 編集状態にして表示
			gcom_shinryo.Select();
			grid_Sinryo.Col = 0;
			gcom_shinryo.SelectInput();
		}


		/// <summary>
		/// 削除(標榜診療科目Grid)
		/// </summary>
		private void btnKamokuDel_Click(object sender, EventArgs e)
		{
			if (dvIryokikanShinryo.Count > 0)
			{
				t_iryokikan_shinryoka xrow = new t_iryokikan_shinryoka(dvIryokikanShinryo.CurrentRow);

				// 他で使われているかチェック,必要か検討 → 医会会費のときは必要そう？
				//				if (AppDbKeiyaku.CheckUsedField(T_Keiyaku.FID_BukkenRoom, xrow.ID_BukkenRoom) == true)
				//				{
				//					AppMsgBox.Show(AppMsgBoxIndex.CanNotDeleteReaseonUsed, xrow.BukkenRoom_Name);
				//					return;
				//				}

				DialogResult dr = AppMsgBox.Show(AppMsgBoxIndex.Delete, "選択中の標榜科目情報");
				if (dr == DialogResult.No)
				{
					return;
				}

				dvIryokikanShinryo.Delete();
				grid_Sinryo.Refresh();

				// 削除後にソート番号を振り直す。
				//				for (int i = 0; i < dvRoom.Count; i++)
				//				{
				//					T_BukkenRoom row = new T_BukkenRoom(dvRoom[i]);

				//					row.BukkenRoom_SortNo = i;
				//				}

				// 入力状態になっているかもしれないので、グリッドへフォーカスを移す。
				gcom_shinryo.Select();
			}
		}

		/// <summary>
		/// 変更要求処理(標榜診療科目Grid)
		/// </summary>
		void dvIryokikanShinryo_RowFetchDemand(object sender, EventArgs e)
		{
			// 再フィルタ
			changeFilterShinryoka();

			// コントロール値セット
			rowFetchShinryo();
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncMasterIryoKikan_DlgEntry.Functions);

			FuncMasterIryoKikan_DlgEntry.Save.Execute = saveClose;
			FuncMasterIryoKikan_DlgEntry.Cancel.Execute = closeCancel;
		}

		/// <summary>
		/// 保存終了
		/// </summary>
		void saveClose()
		{
			if (validateRow() == true)
			{
				formCloseReason = FormCloseReason.Save;
				this.Close();
			}
		}

		/// <summary>
		/// キャンセル
		/// </summary>
		void closeCancel()
		{
			this.Close();
		}

		/// <summary>
		/// 行情報の取得
		/// </summary>
		void rowFetch()
		{
			if (Row != null)
			{
				gctl.FetchAll();
			}
			else
			{
				gctl.ClearAll();
			}
		}

		/// <summary>
		/// データ取得
		/// </summary>
		DataRow getDataRow(string tag)
		{
			if (editrow != null)
			{
				return editrow;
			}

			return null;
		}

		/// <summary>
		/// 検証処理
		/// </summary>
		/// <returns></returns>
		bool validateRow()
		{
			// 最終フォーカスコントロールをRowへ反映
			updateCurrentControlValue(gctl);
//			updateCurrentControlValue(gctl_shinryo);

			// 空白チェック
			Control[] ctls =
			{
//				iCode, iName, iShortName, // ★必須は仕様確認
			};

			foreach (Control ctl in ctls)
			{
				int len = ctl.Text.Length;

				if (ctl is UcTableComboBox)
				{
					len = ((UcTableComboBox)ctl).Text.Length;
				}

				if (len == 0)
				{
					ctl.Select();
					appToolTip.Show(ctl, AppToolTipIndex.CannotUseBlank);
					return false;
				}
			}

			// 重複チェック
			IryoKikan obj = AppGlobal.IryoKikans.GetCode(iCode.Text);

			if (obj != null && obj.ID != Cast.Int(editrow[t_iryokikan.FID_Iryokikan]))
			{
				iCode.Select();
				appToolTip.Show(iCode, AppToolTipIndex.BookingCode);
				return false;
			}

			return true;
		}
	}
}
