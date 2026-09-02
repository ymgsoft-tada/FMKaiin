using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentDB;
using ComponentGControlDB;
using ComponentGGridDB;
using ComponentIO;
using C1.Win.C1TrueDBGrid;

namespace App
{
	/// <summary>
	/// スタッフ情報入力画面
	/// tachi
	/// </summary>
	public partial class FormStaff_DlgEntry : FormFrame
	{
//		FormStaff_DlgEntry_DetailTab1 tb2;

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

		/// コントロールとデータ連結クラス
		/// </summary>
		GControlDB gctl;

		/// <summary>
		/// 医療機関マスタ DBView iSearch用
		/// </summary>
		DBView dvIryoKikanCmb;

		/// <summary>
		/// 医療機関マスタ DBView
		/// </summary>
		DBView dvIryoKikan;
		/// <summary>		/// <summary>
		/// コントロールとデータ連結クラス 医療機関情報用
		/// </summary>
		GControlDB gctl_Iryo;

		// --診療科目Grid--
		/// <summary>
		/// 診療科マスタ DBView
		/// </summary>
		DBView dvShinryo;
		/// <summary>
		/// 会員-診療科の関連 DBView
		/// </summary>
		DBView dvKaiinShinryo;
		/// <summary>
		/// 診療科目Grid
		/// </summary>
		GGridDBCommon gcom_shinryo;
		/// <summary>
		/// コントロールとデータ連結クラス_診療科目Grid用
		/// </summary>
		GControlDB gctl_shinryo;

		// --所属学会Grid--
		/// <summary>
		/// 学会マスタ DBView
		/// </summary>
		DBView dvGakkai;
		/// <summary>
		/// 会員-学会の関連 DBView
		/// </summary>
		DBView dvKaiinGakkai;
		/// <summary>
		/// 所属学会Grid
		/// </summary>
		GGridDBCommon gcom_gakkai;
		/// <summary>
		/// コントロールとデータ連結クラス_所属学会Grid用
		/// </summary>
		GControlDB gctl_gakkai;

		// --参加医会Grid--
		/// <summary>
		/// 医会会費マスタ DBView
		/// </summary>
		DBView dvKaihi;
		/// <summary>
		/// 会員-医会の関連 DBView
		/// </summary>
		DBView dvKaiinKaihi;
		/// <summary>
		/// 所属学会Grid
		/// </summary>
		GGridDBCommon gcom_kaihi;
		/// <summary>
		/// コントロールとデータ連結クラス_参加医会Grid用
		/// </summary>
		GControlDB gctl_kaihi;

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

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormStaff_DlgEntry()
		{
			InitializeComponent();

			Mode = eMode.Add;
		}

		protected override void FormFrame_Load(object sender, EventArgs e)
		{
			string title = "【追加】";

			if (Mode == eMode.Edit)
			{
				title = "【訂正】";
			}

			this.Text += title;
			base.FormFrame_Load(sender, e);
		}

		/// <summary>
		/// ロード処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// 和暦・西暦
			iKoyomi.ExBeginUpdate();
			iKoyomi.ExAddItem("西暦", true);
			iKoyomi.ExAddItem("和暦", false);
			iKoyomi.ExEndUpdate();
			iKoyomi.ExSetSelectedIndexByValue(true);

			// コンボボックスの作成
			// enumから
			AppCombo.SetComboBox(iSex, enumKbn.DSex);
			//			AppCombo.SetComboBox(iJob, enumKbn.DTypeJob);
			AppCombo.SetComboBox(iUsed, enumKbn.DTypeZaiseki); // 在籍区分
			AppCombo.SetComboBox(iIdoShisetsuido, enumKbn.DTypeShisetsuIdo); // 施設異動
			AppCombo.SetComboBox(iIdoKaiinkbn, enumKbn.DTypeIdoKaiin); // 会員区分変更
			AppCombo.SetComboBox(iIdoEtc, enumKbn.DTypeIdoEtc); // 異動その他
			AppCombo.SetComboBox(iKozaType1, enumKbn.DTypeKoza, (int)eTypeKoza.None);
			AppCombo.SetComboBox(iKozaType2, enumKbn.DTypeKoza, (int)eTypeKoza.None);
			AppCombo.SetComboBox(iKozaType3, enumKbn.DTypeKoza, (int)eTypeKoza.None);

			// iSearch
			dvIryoKikan = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_iryokikan, $"ORDER BY {t_iryokikan.FIRK_Code}"), this.BindingContext);
			dvIryoKikanCmb = new DBView(dvIryoKikan, this.BindingContext); // DBViewを複製(フィルタかけない)
			AppTableCombo.SetComboBox_IryoKikan(iIryoKikan, dvIryoKikanCmb); // 医療機関コード

			// 手動
			// 医療機関 施設・業務
			iIryoShisetsugyomu.ExBeginUpdate();
			iIryoShisetsugyomu.ExAddItem("施設", true);
			iIryoShisetsugyomu.ExAddItem("業務", false);
			iIryoShisetsugyomu.ExEndUpdate();
			iIryoShisetsugyomu.ExSetSelectedIndexByValue(true);

			// 文書送付先
			iBunshoSofu.ExBeginUpdate();
			iBunshoSofu.ExAddItem("施設", true);
			iBunshoSofu.ExAddItem("自宅", false);
			iBunshoSofu.ExEndUpdate();
			iBunshoSofu.ExSetSelectedIndexByValue(true);

			// FAX送付先
			iFaxSofu.ExBeginUpdate();
			iFaxSofu.ExAddItem("施設", true);
			iFaxSofu.ExAddItem("自宅", false);
			iFaxSofu.ExEndUpdate();
			iFaxSofu.ExSetSelectedIndexByValue(true);

			// 月別会費内訳
			iKaihiUchiwake.ExBeginUpdate();
			iKaihiUchiwake.ExAddItem("不要", true);
			iKaihiUchiwake.ExAddItem("必要", false);
			iKaihiUchiwake.ExEndUpdate();
			iKaihiUchiwake.ExSetSelectedIndexByValue(true);

			// 公開区分
			iKbnKoukai.ExBeginUpdate();
			iKbnKoukai.ExAddItem("公開", true);
			iKbnKoukai.ExAddItem("非公開", false);
			iKbnKoukai.ExEndUpdate();
			iKbnKoukai.ExSetSelectedIndexByValue(true);


			gctl = new GControlDB(this, getDataRow, AppGlobal.DB.DBZipCode);

			gctl.Add(new GControlDBText(t_staff.FCD_Staff, iCode));
			gctl.Add(new GControlDBCombo(t_staff.FSTF_Sex, iSex));
			//			gctl.Add(new GControlDBCombo(t_staff.FSTF_TypeJob, iJob));
			//			gctl.Add(new GControlDBCombo(t_staff.FID_Shokumu, iShokumu.ComboBox));

			gctl.Add(new GControlDBRuby(
							new string[] {
								t_staff.FSTF_Name,
								t_staff.FSTF_NameFurigane},
							new Control[] {
								iName,
								iNameFurigana
							}));

			gctl.Add(new GControlDBPostAddr(
							new string[]{
								t_staff.FSTF_Post,
								t_staff.FSTF_Addr1
							},
							new Control[]{
								iPost1,
								iPost2,
								iAddr1
							}));
			//			gctl.Add(new GControlDBText(t_staff.FSTF_NameOld, iNameOld));
			gctl.Add(new GControlDBText(t_staff.FSTF_Addr2, iAddr2));
			gctl.Add(new GControlDBHyphenSplit(t_staff.FSTF_Tel1, new Control[] { iTel1_1, iTel1_2, iTel1_3 }));
			gctl.Add(new GControlDBHyphenSplit(t_staff.FSTF_Tel2, new Control[] { iTel2_1, iTel2_2, iTel2_3 }));

			gctl.Add(new GControlDBCombo(t_staff.FSTF_Used, iUsed));
			gctl.Add(new GControlDBDate(t_staff.FSTF_DateBirthday, iBirthday.UcDateCtl));
			//			gctl.Add(new GControlDBDate(t_staff.FSTF_DateNyusha, iDateNyusha.UcDateCtl));
			//			gctl.Add(new GControlDBDate(t_staff.FSTF_DateTaishoku, iDateTaishoku.UcDateCtl));
			gctl.Add(new GControlDBText(t_staff.FSTF_Mail1, iMail1));
			//			gctl.Add(new GControlDBText(t_staff.FSTF_Mail2, iMail2));

			// 医療機関コード
			gctl.Add(new GControlDBCombo(t_staff.FID_Teishutsusaki, iIryoKikan.ComboBox)); // 202609 画面表示のため、暫定で医療機関コードを提出先コードで代用

			gctl.EndAdd(AppDbRule.Rule);

			rowFetch();

			iName.Select();

			// 医療機関情報バインド
			gctl_Iryo = new GControlDB(this, getDataRowIryo);

			gctl_Iryo.Add(new GControlDBText(t_iryokikan.FIRK_Tsusho, iIryoShisetsuName)); // 通称
			gctl_Iryo.Add(new GControlDBText(t_iryokikan.FIRK_TsushoKana, iIryoShisetsuNameKana)); // 通称カナ
			gctl_Iryo.Add(new GControlDBHyphenSplit(t_iryokikan.FIRK_Post, new Control[] { iIryoPost1, iIryoPost2 })); // 郵便番号
			gctl_Iryo.Add(new GControlDBText(t_iryokikan.FIRK_Addr1, iIryoAddr1)); // 住所1
			gctl_Iryo.Add(new GControlDBText(t_iryokikan.FIRK_Addr2, iIryoAddr2)); // 住所2
			gctl_Iryo.Add(new GControlDBHyphenSplit(t_iryokikan.FIRK_Tel1, new Control[] { iIryoTel1_1, iIryoTel1_2, iIryoTel1_3 })); // TEL
			gctl_Iryo.Add(new GControlDBHyphenSplit(t_iryokikan.FIRK_Fax1, new Control[] { iIryoFax1_1, iIryoFax1_2, iIryoFax1_3 })); // FAX
			GControlDBText txtKaisetsuShutai = new GControlDBText(t_iryokikan.FIRK_KaisetsuShutai, iIryoKaisetsushutai); // 開設主体 (ID→名称へ)
			txtKaisetsuShutai.FetchRunEx = KaisetsuShutaiEx; // 文言変換用メソッドへ
			gctl_Iryo.Add(txtKaisetsuShutai);
			GControlDBText txtByoshoUmu = new GControlDBText(t_iryokikan.FIRK_ByoshoUmu, iIryoByoshoUmu); // 病床有無
			txtByoshoUmu.FetchRunEx = ByoshoUmuEx; // 文言変換用メソッドへ
			gctl_Iryo.Add(txtByoshoUmu);
			gctl_Iryo.Add(new GControlDBNumber(t_iryokikan.FIRK_Kyoka, iIryoByoshoCnt)); // 許可病床数
			gctl_Iryo.Add(new GControlDBCheckBox(t_iryokikan.FIRK_Kaigo, iIryoHeisetsu)); // 併設の施設-介護施設
			gctl_Iryo.Add(new GControlDBCheckBox(t_iryokikan.FIRK_Etc, iIryoHeisetsuEtc)); // 併設の施設-その他
			gctl_Iryo.Add(new GControlDBText(t_iryokikan.FIRK_Memo, iIryoHeisetsuName)); // 併設の施設-その他名称
			gctl_Iryo.Add(new GControlDBText(t_iryokikan.FIRK_KumiCode, iIryoKumi)); // 組コード

			gctl_Iryo.EndAdd(AppDbRule.Rule);

			changeFilterIryoKikanShown(); // 表示中会員の情報のみへフィルタ
			rowFetchIryoKikan(); // コントロール値セット(グリッド初期表示値)

			//■ イベントの登録
			//			iJob.SelectedIndexChanged += iJob_SelectedIndexChanged;
			iIryoKikan.TextChanged += iIryoKikan_TextChanged;

			// 医療機関情報の非活性
			iIryoShisetsuName.Enabled = false;
			iIryoShisetsuNameKana.Enabled = false;
			iIryoPost1.Enabled = false;
			iIryoPost2.Enabled = false;
			iIryoAddr1.Enabled = false;
			iIryoAddr2.Enabled = false;
			iIryoTel1_1.Enabled = false;
			iIryoTel1_2.Enabled = false;
			iIryoTel1_3.Enabled = false;
			iIryoFax1_1.Enabled = false;
			iIryoFax1_2.Enabled = false;
			iIryoFax1_3.Enabled = false;
			iIryoKaisetsushutai.Enabled = false;
			iIryoByoshoUmu.Enabled = false;
			iIryoByoshoCnt.Enabled = false;
			iIryoHeisetsu.Enabled = false;
			iIryoHeisetsuEtc.Enabled = false;
			iIryoHeisetsuName.Enabled = false;
			iIryoKumi.Enabled = false;


			//-----担当診療科目Grid準備-----
			// ■データ取得
			dvShinryo = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_shinryoka, $"ORDER BY {t_shinryoka.FSRK_Code}"), this.BindingContext);

			// ソート順追加など、任意並び替えがあるならRefill側を使う
			// 追加時の動作仕様のため、再表示時にのみソートする(GetReFillTable()の利用)
			//			dvKaiinShinryo = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_kaiin_shinryoka, $"ORDER BY {t_kaiin_shinryoka.SRK_Code}"), this.BindingContext);
			dvKaiinShinryo = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin_shinryoka), this.BindingContext); // BindingContext:DBViewのカレント行とGridのカレント行追従させるため必要

			// ■コントロール設定
			AppTableCombo.SetComboBox_Shinryoka(iTantoKamoku, dvShinryo); // iSearch
			AppTableCombo.SetComboBox_Shinryoka(iTantoMainKamoku, dvShinryo); // iSearch(主担当)

			// ■Grid設定
			AppGridCommon.StyleSet(grid_Sinryo);

			gcom_shinryo = new GGridDBCommon(grid_Sinryo, this);

			gcom_shinryo.Add(new GGridDBText(t_kaiin_shinryoka.FID_Shinryoka, "名称", 60));
			gcom_shinryo.SetCellDisp(GGridDBCellDisp.Left);
			gcom_shinryo.SetFocusControlInGrid(iTantoKamoku); // Gridフォーカス時に表示させるコントロール
			gcom_shinryo.SetUnboundColumnFetch(ubShinryokaName); // ID→名称変換
			gcom_shinryo.SetTabIndex(1);
			gcom_shinryo.SetLocked(true);

			gcom_shinryo.EndAdd(dvKaiinShinryo);

			// コントロールのDBバインド
			gctl_shinryo = new GControlDB(this, getDataRowShinryo);
			gctl_shinryo.Add(new GControlDBCombo(t_kaiin_shinryoka.FID_Shinryoka, iTantoKamoku.ComboBox));
			gctl_shinryo.EndAdd(AppDbRule.Rule);

			// イベント登録
			btnKamokuAdd.Click += btnKamokuAdd_Click;
			btnKamokuDel.Click += btnKamokuDel_Click;
			dvKaiinShinryo.RowFetchDemand += dvKaiinShinryo_RowFetchDemand;

			changeFilterShinryoka(); // 表示中会員の情報のみへフィルタ
			rowFetchShinryo(); // コントロール値セット(グリッド初期表示値)

			//-----所属学会Grid準備-----
			// ■データ取得
			dvGakkai = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_gakkai, $"ORDER BY {t_gakkai.FGKAI_Code}"), this.BindingContext);

			// ソート順追加など、任意並び替えがあるならRefill側を使う
			// 追加時の動作仕様のため、再表示時にのみソートする(GetReFillTable()の利用)
			//			dvKaiinShinryo = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_kaiin_shinryoka, $"ORDER BY {t_kaiin_shinryoka.SRK_Code}"), this.BindingContext);
			dvKaiinGakkai = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin_gakkai), this.BindingContext); // BindingContext:DBViewのカレント行とGridのカレント行追従させるため必要

			// ■コントロール設定
			AppTableCombo.SetComboBox_Gakkai(iSyozokuGakkai, dvGakkai); // iSearch

			// ■Grid設定
			AppGridCommon.StyleSet(grid_Gakkai);

			gcom_gakkai = new GGridDBCommon(grid_Gakkai, this);

			gcom_gakkai.Add(new GGridDBText(t_kaiin_gakkai.FID_Gakkai, "名称", 60));
			gcom_gakkai.SetCellDisp(GGridDBCellDisp.Left);
			gcom_gakkai.SetFocusControlInGrid(iSyozokuGakkai); // Gridフォーカス時に表示させるコントロール
			gcom_gakkai.SetUnboundColumnFetch(ubGakkaiName); // ID→名称変換
			gcom_gakkai.SetTabIndex(1);
			gcom_gakkai.SetLocked(true);

			gcom_gakkai.EndAdd(dvKaiinGakkai);

			// コントロールのDBバインド
			gctl_gakkai = new GControlDB(this, getDataRowGakkai);
			gctl_gakkai.Add(new GControlDBCombo(t_kaiin_gakkai.FID_Gakkai, iSyozokuGakkai.ComboBox));
			gctl_gakkai.EndAdd(AppDbRule.Rule);

			// イベント登録
			btnGakkaiAdd.Click += btnGakkaiAdd_Click;
			btnGakkaiDel.Click += btnGakkaiDel_Click;
			dvKaiinGakkai.RowFetchDemand += dvKaiinGakkai_RowFetchDemand;

			changeFilterGakkai(); // 表示中会員の情報のみへフィルタ
			rowFetchGakkai(); // コントロール値セット(グリッド初期表示値)

			//-----参加医会Grid準備-----
			// ■データ取得
			dvKaihi = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_kaihi, $"ORDER BY {t_kaihi.FCD_Kaihi}"), this.BindingContext);

			// ソート順追加など、任意並び替えがあるならRefill側を使う
			// 追加時の動作仕様のため、再表示時にのみソートする(GetReFillTable()の利用)
			//			dvKaiinShinryo = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_kaiin_shinryoka, $"ORDER BY {t_kaiin_shinryoka.SRK_Code}"), this.BindingContext);
			dvKaiinKaihi = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin_kaihi), this.BindingContext); // BindingContext:DBViewのカレント行とGridのカレント行追従させるため必要

			// ■コントロール設定
			AppTableCombo.SetComboBox_Kaihi(iSankaIkai, dvKaihi); // iSearch

			// ■Grid設定
			AppGridCommon.StyleSet(grid_Kaihi);

			gcom_kaihi = new GGridDBCommon(grid_Kaihi, this);

			gcom_kaihi.Add(new GGridDBText(t_kaiin_kaihi.FID_Kaihi, "名称", 60));
			gcom_kaihi.SetCellDisp(GGridDBCellDisp.Left);
			gcom_kaihi.SetFocusControlInGrid(iSankaIkai); // Gridフォーカス時に表示させるコントロール
			gcom_kaihi.SetUnboundColumnFetch(ubIkaiName); // ID→名称変換
			gcom_kaihi.SetTabIndex(1);
			gcom_kaihi.SetLocked(true);

			gcom_kaihi.EndAdd(dvKaiinKaihi);

			// コントロールのDBバインド
			gctl_kaihi = new GControlDB(this, getDataRowKaihi);
			gctl_kaihi.Add(new GControlDBCombo(t_kaiin_kaihi.FID_Kaihi, iSankaIkai.ComboBox));
			gctl_kaihi.EndAdd(AppDbRule.Rule);

			// イベント登録
			btnKaihiAdd.Click += btnKaihiAdd_Click;
			btnKaihiDel.Click += btnKaihiDel_Click;
			dvKaiinKaihi.RowFetchDemand += dvKaiinKaihi_RowFetchDemand;

			changeFilterKaihi(); // 表示中会員の情報のみへフィルタ
			rowFetchKaihi(); // コントロール値セット(グリッド初期表示値)




			//-----タブへの画面表示準備-----------------
			// 先にベースの処理を走らせておく
			//			base.Form_Load(sender, e);

			//			dvKsnPL1 = new DBView(AppGlobal.IDB.FillToTable(TableProp.t_kessan_kojin_af1)); // たぶん1テーブル
			//			dvKsnPL2 = new DBView(AppGlobal.IDB.FillToTable(TableProp.t_kessan_kojin_af2));
			//			dvKsnPL3 = new DBView(AppGlobal.IDB.FillToTable(TableProp.t_kessan_kojin_af3));

			//			createData();

			//			dvKmKessan = new DBView(AppGlobal.IDB.FillToTable(TableProp.t_kamoku_kessan));
			//			dvShuuchi = new DBView(AppGlobal.IDB.FillToTable(TableProp.t_kessan_shuuchi));


			//			tb2 = new FormStaff_SubDetail();
			//			pl.DvShisanhyo = zanPL.DvShisanhyo;
			//			pl.DvKessanPL1 = dvKsnPL1;
			//			pl.KessanCostPL = costKsnPL;
			//			pl.DvKessanKamoku = dvKmKessan;
			//			tb2.TopLevel = false;
			//			tb2.Show();

			/*			pl2 = new FormOutKessansho_KojinFudosanPL2();
						pl2.DvKessanPL1 = dvKsnPL1;
						pl2.DvKessanPL2 = dvKsnPL2;
						pl2.DvShuuchi = dvShuuchi;
						pl2.TopLevel = false;
						pl2.Show();

						pl3 = new FormOutKessansho_KojinFudosanPL3();
						pl3.DvKessanPL1 = dvKsnPL1;
						pl3.DvKessanPL3 = dvKsnPL3;
						pl3.KessanCostPL = costKsnPL;
						pl3.TopLevel = false;
						pl3.Show();
			*/
			//			tabPagePL.Controls.Add(pl);
			//			tabPage2.Controls.Add(tb2);
			//			tabPagePL3.Controls.Add(pl3);

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// 医療機関情報-開設主体の文字列変換
		/// <param name="data">バインドしたDB値</param>
		/// </summary>
		object KaisetsuShutaiEx(DataRow row, object data)
		{
			// 開設主体情報の取得
			Kaisetsushutai ks = AppGlobal.Kaisetsushutais.Get(Cast.Int(data));

			// 該当情報が存在したら名称をreturn
			if (ks != null)
			{
				return ks.XRow.KST_Name_Null;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 医療機関情報-病床有無の文字列変換
		/// <param name="data">バインドしたDB値</param>
		/// </summary>
		object ByoshoUmuEx(DataRow row, object data)
		{
			// DB値によって名称をreturn
			if (Cast.Bool(data) == true)
			{
				return "有";
			}
			else
			{
				return "無";
			}
		}


		/// <summary>
		/// IDを基に、Gridに診療科名を表示します。
		/// </summary>
		string ubShinryokaName(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaiin_shinryoka xrow = new t_kaiin_shinryoka(dvKaiinShinryo[e.Row]);

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
			if (dvKaiinShinryo.CurrentRow != null && dvKaiinShinryo.Count > 0)
			{
				return dvKaiinShinryo.CurrentRow.Row;
			}

			return null;
		}

		/// <summary>
		/// フィルタ変更(t_kaiin_shinryoka)
		/// </summary>
		void changeFilterShinryoka()
		{
			string filter = "";

			/*
			if (iSearch.Text != "")
			{
				string str = iSearch.Text;
				string str_numchk = StrConv.ToNarrow(str);

				if (Regex.IsMatch(str_numchk, @"^[-.0-9]+$") == true)
				{
					// 数値のみならコード検索
					filter += string.Format("({0} = {1})", t_staff.FCD_Staff, Cast.Int(str_numchk));
				}
				else
				{
					if (str.Length == 1)
					{
						// 一文字だけなら先頭一致
						filter += string.Format("({0} OR {1})",
												DBQuery.CreateLikeKanaString(t_staff.FSTF_Name, str),
												DBQuery.CreateLikeKanaString(t_staff.FSTF_NameFurigane, str));
					}
					else
					{
						// 部分一致
						filter += string.Format("({0} OR {1})",
												DBQuery.CreateLikeKanaString(t_staff.FSTF_Name, str, true),
												DBQuery.CreateLikeKanaString(t_staff.FSTF_NameFurigane, str, true));
					}

				}
			}

			eTypeJob job = Cast.Enumelate<eTypeJob>(iJob.ExGetValue(), eTypeJob.None);
			if (job != eTypeJob.None)
			{
				if (filter != "")
					filter += " AND ";
				filter += $"({t_staff.FSTF_TypeJob} = {(int)job})";
			}
*/

			// 該当会員の診療科目情報へフィルタ
			t_staff tmprow = new t_staff(editrow);

			filter =
				$"{t_kaiin_shinryoka.FID_Kaiin} = {tmprow.ID_Staff}";
//				$"({t_shiharai.FShiharaiDate} >= #{dt.Year}/{dt.Month}/1# AND " +
//				$"{t_shiharai.FShiharaiDate} <= #{nextdt.Year}/{nextdt.Month}/{nextdt.Day}#)";

			// 適用
			dvKaiinShinryo.RowFilterQuery(filter);
		}

		/// <summary>
		/// Gridカレント行の内容を各コントロールにセットします(担当診療Grid)
		/// </summary>
		void rowFetchShinryo()
		{
			if (gctl_shinryo == null)
			{
//				Debug.WriteLine("GControlDBが設定されていません。");
				return;
			}

			if (dvKaiinShinryo.CurrentRow == null)
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
		/// 追加(担当診療Grid)
		/// </summary>
		private void btnKamokuAdd_Click(object sender, EventArgs e)
		{
			t_staff tmprow = new t_staff(editrow);
			t_kaiin_shinryoka nrow = new t_kaiin_shinryoka(dvKaiinShinryo.NewRow());

			// NewRowへフィルタ済の会員IDセット
			nrow.ID_Kaiin = tmprow.ID_Staff;

			// 行追加
			dvKaiinShinryo.Add(nrow.Row);
			// 追加した行をカレント行にする
			AppGridCommon.SetRow(grid_Sinryo, dvKaiinShinryo.Count - 1);

			// 編集状態にして表示
			gcom_shinryo.Select();
			grid_Sinryo.Col = 0;
			gcom_shinryo.SelectInput();
		}

		/// <summary>
		/// 削除(担当診療Grid)
		/// </summary>
		private void btnKamokuDel_Click(object sender, EventArgs e)
		{
			if (dvKaiinShinryo.Count > 0)
			{
				t_kaiin_shinryoka xrow = new t_kaiin_shinryoka(dvKaiinShinryo.CurrentRow);

				// 他で使われているかチェック,必要か検討 → 医会会費のときは必要そう？
				//				if (AppDbKeiyaku.CheckUsedField(T_Keiyaku.FID_BukkenRoom, xrow.ID_BukkenRoom) == true)
				//				{
				//					AppMsgBox.Show(AppMsgBoxIndex.CanNotDeleteReaseonUsed, xrow.BukkenRoom_Name);
				//					return;
				//				}

				DialogResult dr = AppMsgBox.Show(AppMsgBoxIndex.Delete, "選択中の診療科目(担当)情報");
				if (dr == DialogResult.No)
				{
					return;
				}

				dvKaiinShinryo.Delete();
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
		/// 変更要求処理(担当診療Grid)
		/// </summary>
		void dvKaiinShinryo_RowFetchDemand(object sender, EventArgs e)
		{
			// 再フィルタ
			changeFilterShinryoka();

			// コントロール値セット
			rowFetchShinryo();
		}

		// --学会Grid--
		/// <summary>
		/// IDを基に、Gridに学会名を表示します。
		/// </summary>
		string ubGakkaiName(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaiin_gakkai xrow = new t_kaiin_gakkai(dvKaiinGakkai[e.Row]);

			Gakkai gk = AppGlobal.Gakkais.Get(xrow.ID_Gakkai);

			string name = "";
			if (gk != null)
			{
				name = gk.XRow.GKAI_Name;
			}

			return name;
		}

		/// <summary>
		/// 所属学会gridの現在行を返します。
		/// </summary>
		/// <param name="datatag">タグ</param>
		/// <returns>現在行</returns>
		DataRow getDataRowGakkai(string datatag)
		{
			if (dvKaiinGakkai.CurrentRow != null && dvKaiinGakkai.Count > 0)
			{
				return dvKaiinGakkai.CurrentRow.Row;
			}

			return null;
		}

		/// <summary>
		/// フィルタ変更(t_kaiin_gakkai)
		/// </summary>
		void changeFilterGakkai()
		{
			string filter = "";

			// 該当会員の診療科目情報へフィルタ
			t_staff tmprow = new t_staff(editrow);

			filter =
				$"{t_kaiin_gakkai.FID_Kaiin} = {tmprow.ID_Staff}";

			// 適用
			dvKaiinGakkai.RowFilterQuery(filter);
		}

		/// <summary>
		/// Gridカレント行の内容を各コントロールにセットします(所属学会Grid)
		/// </summary>
		void rowFetchGakkai()
		{
			if (gctl_gakkai == null)
			{
//				Debug.WriteLine("GControlDBが設定されていません。");
				return;
			}

			if (dvKaiinGakkai.CurrentRow == null)
			{
				// 現在行がないので、コントロールの内容をクリアする。
				gctl_gakkai.ClearAll();
			}
			else
			{
				// 現在行を取得
				gctl_gakkai.FetchAll();
			}

			//setEnableFunctionButton();
		}

		/// <summary>
		/// 追加(所属学会Grid)
		/// </summary>
		private void btnGakkaiAdd_Click(object sender, EventArgs e)
		{
			t_staff tmprow = new t_staff(editrow);
			t_kaiin_gakkai nrow = new t_kaiin_gakkai(dvKaiinGakkai.NewRow());

			// NewRowへフィルタ済の会員IDセット
			nrow.ID_Kaiin = tmprow.ID_Staff;

			// 行追加
			dvKaiinGakkai.Add(nrow.Row);
			// 追加した行をカレント行にする
			AppGridCommon.SetRow(grid_Gakkai, dvKaiinGakkai.Count - 1);

			// 編集状態にして表示
			gcom_gakkai.Select();
			grid_Gakkai.Col = 0;
			gcom_gakkai.SelectInput();
		}

		/// <summary>
		/// 削除(所属学会Grid)
		/// </summary>
		private void btnGakkaiDel_Click(object sender, EventArgs e)
		{
			if (dvKaiinGakkai.Count > 0)
			{
				t_kaiin_gakkai xrow = new t_kaiin_gakkai(dvKaiinGakkai.CurrentRow);

				// 他で使われているかチェック,必要か検討 → 医会会費のときは必要そう？
//				if (AppDbKeiyaku.CheckUsedField(T_Keiyaku.FID_BukkenRoom, xrow.ID_BukkenRoom) == true)
//				{
//					AppMsgBox.Show(AppMsgBoxIndex.CanNotDeleteReaseonUsed, xrow.BukkenRoom_Name);
//					return;
//				}

				DialogResult dr = AppMsgBox.Show(AppMsgBoxIndex.Delete, "選択中の学会所属情報");
				if (dr == DialogResult.No)
				{
					return;
				}

				dvKaiinGakkai.Delete();
				grid_Gakkai.Refresh();

				// 削除後にソート番号を振り直す。
//				for (int i = 0; i < dvRoom.Count; i++)
//				{
//					T_BukkenRoom row = new T_BukkenRoom(dvRoom[i]);

//					row.BukkenRoom_SortNo = i;
//				}

				// 入力状態になっているかもしれないので、グリッドへフォーカスを移す。
				gcom_gakkai.Select();
			}
		}



		/// <summary>
		/// 変更要求処理(所属学会Grid)
		/// </summary>
		void dvKaiinGakkai_RowFetchDemand(object sender, EventArgs e)
		{
			// 再フィルタ
			changeFilterGakkai();

			// コントロール値セット
			rowFetchGakkai();
		}

		// --参加医会Grid--
		/// <summary>
		/// IDを基に、Gridに医会名を表示します。
		/// </summary>
		string ubIkaiName(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaiin_kaihi xrow = new t_kaiin_kaihi(dvKaiinKaihi[e.Row]);

			Kaihi kh = AppGlobal.Kaihis.Get(xrow.ID_Kaihi);

			string name = "";
			if (kh != null)
			{
				name = kh.XRow.Kaihi_Name;
			}

			return name;
		}

		/// <summary>
		/// 参加医会gridの現在行を返します。
		/// </summary>
		/// <param name="datatag">タグ</param>
		/// <returns>現在行</returns>
		DataRow getDataRowKaihi(string datatag)
		{
			if (dvKaiinKaihi.CurrentRow != null && dvKaiinKaihi.Count > 0)
			{
				return dvKaiinKaihi.CurrentRow.Row;
			}

			return null;
		}

		/// <summary>
		/// フィルタ変更(t_kaiin_kaihi)
		/// </summary>
		void changeFilterKaihi()
		{
			string filter = "";

			// 該当会員の診療科目情報へフィルタ
			t_staff tmprow = new t_staff(editrow);

			filter =
				$"{t_kaiin_kaihi.FID_Kaiin} = {tmprow.ID_Staff}";

			// 適用
			dvKaiinKaihi.RowFilterQuery(filter);
		}

		/// <summary>
		/// Gridカレント行の内容を各コントロールにセットします(参加医会Grid)
		/// </summary>
		void rowFetchKaihi()
		{
			if (gctl_kaihi == null)
			{
//				Debug.WriteLine("GControlDBが設定されていません。");
				return;
			}

			if (dvKaiinKaihi.CurrentRow == null)
			{
				// 現在行がないので、コントロールの内容をクリアする。
				gctl_kaihi.ClearAll();
			}
			else
			{
				// 現在行を取得
				gctl_kaihi.FetchAll();
			}

			//setEnableFunctionButton();
		}

		/// <summary>
		/// 追加(参加医会Grid)
		/// </summary>
		private void btnKaihiAdd_Click(object sender, EventArgs e)
		{
			t_staff tmprow = new t_staff(editrow);
			t_kaiin_kaihi nrow = new t_kaiin_kaihi(dvKaiinKaihi.NewRow());

			// NewRowへフィルタ済の会員IDセット
			nrow.ID_Kaiin = tmprow.ID_Staff;

			// 行追加
			dvKaiinKaihi.Add(nrow.Row);
			// 追加した行をカレント行にする
			AppGridCommon.SetRow(grid_Kaihi, dvKaiinKaihi.Count - 1);

			// 編集状態にして表示
			gcom_kaihi.Select();
			grid_Kaihi.Col = 0;
			gcom_kaihi.SelectInput();
		}

		/// <summary>
		/// 削除(参加医会Grid)
		/// </summary>
		private void btnKaihiDel_Click(object sender, EventArgs e)
		{
			if (dvKaiinKaihi.Count > 0)
			{
				t_kaiin_kaihi xrow = new t_kaiin_kaihi(dvKaiinKaihi.CurrentRow);

				// 他で使われているかチェック,必要か検討 → 医会会費のときは必要そう？
				//				if (AppDbKeiyaku.CheckUsedField(T_Keiyaku.FID_BukkenRoom, xrow.ID_BukkenRoom) == true)
				//				{
				//					AppMsgBox.Show(AppMsgBoxIndex.CanNotDeleteReaseonUsed, xrow.BukkenRoom_Name);
				//					return;
				//				}

				DialogResult dr = AppMsgBox.Show(AppMsgBoxIndex.Delete, "選択中の参加医会情報");
				if (dr == DialogResult.No)
				{
					return;
				}

				dvKaiinKaihi.Delete();
				grid_Kaihi.Refresh();

				// 削除後にソート番号を振り直す。
				//				for (int i = 0; i < dvRoom.Count; i++)
				//				{
				//					T_BukkenRoom row = new T_BukkenRoom(dvRoom[i]);

				//					row.BukkenRoom_SortNo = i;
				//				}

				// 入力状態になっているかもしれないので、グリッドへフォーカスを移す。
				gcom_kaihi.Select();
			}
		}

		/// <summary>
		/// 変更要求処理(所属学会Grid)
		/// </summary>
		void dvKaiinKaihi_RowFetchDemand(object sender, EventArgs e)
		{
			// 再フィルタ
			changeFilterKaihi();

			// コントロール値セット
			rowFetchKaihi();
		}


/* 不要メソッド
		/// <summary>
		/// コードに該当する医療機関情報を表示
		/// </summary>
		void fetchIryoKikan()
		{
			// 現在行を取得
//			gctl_Iryo.FetchAll();

			// 画面上の医療機関コード取得
			int cdIryoKikan = Cast.Int(iCodeIryoKikan.Text);

			// 医療機関情報の取得
			IryoKikan ik = AppGlobal.IryoKikans.GetCode(cdIryoKikan);

			// 該当情報が存在したら各コントロールへ値セット
			if (ik != null)
			{
				// 施設名通称
				iIryoShisetsuName.Text = ik.XRow.IRK_Tsusho_Null;
				// 施設名通称カナ
				iIryoShisetsuNameKana.Text = ik.XRow.IRK_TsushoKana_Null;

				// Post
				iIryoPost1.Text = ik.XRow.IRK_Post_Null;
				iIryoPost2.Text = ik.XRow.;
				// Addr
				iIryoAddr1.Text = ik.XRow.;
				iIryoAddr2.Text = ik.XRow.;
				// TEL
				iIryoTel1_1.Text = ik.XRow.;
				iIryoTel1_2.Text = ik.XRow.;
				iIryoTel1_3.Text = ik.XRow.;
				// FAX
				iIryoFax1_1.Text = ik.XRow.;
				iIryoFax1_2.Text = ik.XRow.;
				iIryoFax1_3.Text = ik.XRow.;

/*				// 開設主体
				Kaisetsusyutai ks = AppGlobal.Kaisetsusyutais.GetCode(ik.XRow.IRK_KaisetsuShutai_Null); // IDGetになるかも
				if (ks != null)
				{
					iIryoKaisetsusyutai.Text = ks.XRow.KST_Name;
				}
				// 病床有無
				if (ik.XRow.IRK_ByoshoUmu == true)
				{
					iIryoByoshoumu.Text = "有";
				}
				else
				{
					iIryoByoshoumu.Text = "無";
				}
				// 許可病床数
				iIryoByoshoCnt.Text = Cast.String(ik.XRow.IRK_Kyoka_Null);
				// 併設施設-介護施設
				iIryoHeisetsu.Checked = ik.XRow.IRK_Kaigo;
				// 併設施設-その他
				iIryoHeisetsuEtc.Checked = ik.XRow.IRK_Etc;
				// 併設施設名
				iIryoHeisetsuName.Text = ik.XRow.IRK_Memo_Null;
				// 組コード
				iIryoKumi.Text = Cast.String(ik.XRow.IRK_KumiCode); // 組マスタがないため、いったんコードをセット
				// 標榜科目？

			}
		}
*/

		/// <summary>
		/// 医療機関情報の現在行を返します。
		/// </summary>
		/// <param name="datatag">タグ</param>
		/// <returns>現在行</returns>
		DataRow getDataRowIryo(string datatag)
		{
			if (dvIryoKikan.CurrentRow != null && dvIryoKikan.Count > 0)
			{
				return dvIryoKikan.CurrentRow.Row;
			}

			return null;
		}

		/// <summary>
		/// 画面表示時フィルタ(t_iryokikan)
		/// </summary>
		void changeFilterIryoKikanShown()
		{
			string filter = "";

			// 該当会員の医療機関情報へフィルタ
			t_staff tmprow = new t_staff(editrow);

			filter =
//				$"{t_iryokikan.FID_Iryokikan} = {tmprow.ID_Iryokikan}";
				$"{t_iryokikan.FID_Iryokikan} = {tmprow.ID_Teishutsusaki}"; // 20260901 医療機関ID代用で提出先ID列を利用する

			// 適用
			dvIryoKikan.RowFilterQuery(filter);
		}

		/// <summary>
		/// 入力時フィルタ変更(t_iryokikan)
		/// </summary>
		void changeFilterIryoKikan()
		{
			string filter = "";

			// 医療機関コードの検索
//			int IryoKikanId = Cast.Int(iIryoKikan.ComboBox);
			int IryoKikanCd = Cast.Int(iIryoKikan.Text);
			if (IryoKikanCd != 0)
			{
				if (filter != "") filter += " AND ";
				// 数値のみならコード検索
//				filter += string.Format("({0} = {1})", t_iryokikan.FID_Iryokikan, IryoKikanId);
				filter += string.Format("({0} = {1})", t_iryokikan.FIRK_Code, IryoKikanCd);
			}

			// フィルタ適用
			dvIryoKikan.RowFilterQuery(filter);
		}

		/// <summary>
		/// 医療機関マスタDBView値を各コントロールにセットします
		/// </summary>
		void rowFetchIryoKikan()
		{
			if (gctl_Iryo == null)
			{
				//				Debug.WriteLine("GControlDBが設定されていません。");
				return;
			}

			if (dvIryoKikan.CurrentRow == null)
			{
				// 現在行がないので、コントロールの内容をクリアする。
				gctl_Iryo.ClearAll();
			}
			else
			{
				// 現在行を取得
				gctl_Iryo.FetchAll();
			}

			//setEnableFunctionButton();
		}

		/// <summary>
		/// 医療機関コード イベント
		/// </summary>
		private void iIryoKikan_TextChanged(object sender, EventArgs e)
		{
			changeFilterIryoKikan();
			rowFetchIryoKikan();
		}

		private void iKoyomi_SelectedIndexChanged(object sender, EventArgs e)
		{
			changedKoyomi();
		}

		void changedKoyomi()
		{
			iBirthday.ChangedKoyomiSeirekiOn(Cast.Bool(iKoyomi.ExGetValue()));
			iBirthday.Select();
		}

		/*
				private void iJob_SelectedIndexChanged(object sender, EventArgs e)
				{
					gctl.UpdateByControl(iJob);

					t_staff xrow = new t_staff(editrow);
					xrow.ID_Shokumu_Null = null;

					gctl.FetchByControl(iShokumu.ComboBox);
				}
		*/
		/// <summary>
		/// 行情報の取得(会員情報)
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
		/// 閉じる処理
		/// </summary>
		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (formCloseReason != App.FormCloseReason.Save)
			{
				if (this.ActiveControl is UcDate)
				{
					gctl.UpdateByControl(( (UcDate)this.ActiveControl ).UcDateCtl);
				}
				else
				if (this.ActiveControl is UcTableComboBox)
				{
					gctl.UpdateByControl(( (UcTableComboBox)this.ActiveControl ).ComboBox);
				}
				else
				{
					gctl.Update(this);
				}

				if (Mode == eMode.Add || AppDb.CheckModified(srcrow, editrow) == true)
				{
					if (AppMsgBox.Show(AppMsgBoxIndex.CancelClose) == System.Windows.Forms.DialogResult.No)
					{
						e.Cancel = true;
					}
				}
			}

			base.FormFrame_FormClosing(sender, e);
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncStaff_DlgEntry.Functions);

			FuncStaff_DlgEntry.Save.Execute = saveClose;
			FuncStaff_DlgEntry.Cancel.Execute = closeCancel;

//			bool enabled = false;

			//if (AppGlobal.LoginUser.AvailableMyno == true)
			////if (AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.Admin ||
			////  　AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.SU)
			//{
			//	enabled = true;
			//}

//			appFuncKey.SetEnabled(FuncStaff_DlgEntry.ShowMyno.Key, enabled);
//			appFuncKey.SetVisible(FuncStaff_DlgEntry.ShowMyno.Key, enabled);
		}

		/// <summary>
		/// 検証処理
		/// </summary>
		/// <returns></returns>
		bool validateRow()
		{
			// 最終フォーカスコントロールをRowへ反映
			updateCurrentControlValue(gctl);

			// 空白チェック
			Control[] ctls =
			{
				iCode, iName, iNameFurigana,
			};

			foreach (Control ctl in ctls)
			{
				int len = ctl.Text.Length;

				if (ctl is UcTableComboBox)
				{
					len = ( (UcTableComboBox)ctl ).Text.Length;
				}

				if (len == 0)
				{
					ctl.Select();
					appToolTip.Show(ctl, AppToolTipIndex.CannotUseBlank);
					return false;
				}
			}

			// 重複チェック
			Staff obj = AppGlobal.Staffs.GetCode(iCode.Text);

			if (obj != null && obj.ID != Cast.Int(editrow[t_staff.FID_Staff]))
			{
				iCode.Select();
				appToolTip.Show(iCode, AppToolTipIndex.BookingCode);
				return false;
			}

			// 日付範囲チェック


			return true;
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
	}
}
