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
using System.Text.RegularExpressions;

namespace App
{
	/// <summary>
	/// [作成者 tanaka]
	/// 会員情報入力画面
	/// </summary>
	public partial class FormKaiin_DlgEntry : FormFrame
	{
//		FormKaiin_DlgEntry_DetailTab1 tb2;

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
		/// 会員区分マスタ DBView iSearch用
		/// </summary>
		DBView dvKaiinKbn;

		/// <summary>
		/// 医会会費マスタ(医師会会費) DBView iSearch用
		/// </summary>
		DBView dvIkai;

		/// <summary>
		/// 医師会マスタ DBView iSearch用
		/// </summary>
		DBView dvIshikai;

		/// <summary>
		/// 施設業務マスタ DBView iSearch用
		/// </summary>
		DBView dvShisetsugyomu;

		/// <summary>
		/// 医療機関マスタ DBView
		/// </summary>
		DBView dvIryoKikan;
		/// <summary>		/// <summary>
		/// コントロールとデータ連結クラス 医療機関情報用
		/// </summary>
		GControlDB gctl_iryo;

		// --診療科目Grid--
		/// <summary>
		/// 診療科マスタ DBView
		/// </summary>
		DBView dvShinryo;
		/// <summary>
		/// 会員-診療科の関連 DBView
		/// </summary>
		DBView dvKaiinShinryo;
		public DBView DvKaiinShinryo
		{
			//get { return dvKaiinShinryo; }
			set { dvKaiinShinryo = new DBView(value, this.BindingContext); } // DlgEntry画面でBindするため、受取ったDBViewを引数にnewして別Viewを準備(DataTableは同じ)
		}
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
		public DBView DvKaiinGakkai
		{
			//get { return dvKaiinGakkai; }
			set { dvKaiinGakkai = new DBView(value, this.BindingContext); } // DlgEntry画面でBindするため、受取ったDBViewを引数にnewして別Viewを準備(DataTableは同じ)
		}
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
		public DBView DvKaiinKaihi
		{
			//get { return dvKaiinKaihi; }
			set { dvKaiinKaihi = new DBView(value, this.BindingContext); } // DlgEntry画面でBindするため、受取ったDBViewを引数にnewして別Viewを準備(DataTableは同じ)
		}
		/// <summary>
		/// 参加医会Grid
		/// </summary>
		GGridDBCommon gcom_kaihi;
		/// <summary>
		/// コントロールとデータ連結クラス_参加医会Grid用
		/// </summary>
		GControlDB gctl_kaihi;

		// --異動情報Grid--
		/// <summary>
		/// 会員-異動情報の関連 DBView
		/// </summary>
		DBView dvKaiinIdo;
		public DBView DvKaiinIdo
		{
			//get { return dvKaiinIdo; }
			set { dvKaiinIdo = new DBView(value, this.BindingContext); } // DlgEntry画面でBindするため、受取ったDBViewを引数にnewして別Viewを準備(DataTableは同じ)
		}
		/// <summary>
		/// 異動情報Grid
		/// </summary>
		GGridDBCommon gcom_ido;

		/// <summary>
		/// 起動時のレコード状態
		/// </summary>
		DataRow srcrow = null;
		/// <summary>
		/// 編集するレコード
		/// </summary>
		DataRow editrow = null;

		/// <summary>
		/// 画面間連携用レコード
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
		public FormKaiin_DlgEntry()
		{
			InitializeComponent();

			Mode = eMode.Add;
		}

		/// <summary>
		/// キーダウン処理
		/// </summary>
		protected override void FormFrame_KeyDown(object sender, KeyEventArgs e)
		{
			//■ ファンクションキーの標準ショートカットの抑止
			e.Handled = base.Patcher_CheckShortcutForKillingWindowsAction(e.KeyCode, e.Alt);

			// 処理が必要かどうかチェック。
			if (base.CheckEnabledFormKeyPreview(e.KeyCode) == true)
			{
				return;
			}
			if (this.ActiveControl == null)
			{
				return;
			}

			switch (e.KeyCode)
			{
				// Enterによるフォーカス移動で、Gridへの移動に対応するための記述

				case Keys.Enter:
					if (this.ActiveControl == grid_Sinryo) // Enterでのフォーカス先が診療Grid
					{
						gcom_shinryo.SelectInput(); // Grid内のセルへフォーカス移動

						return;
					}
					else
					if (gcom_shinryo.CheckFocusControl(this.ActiveControl))
					{
						if (gcom_shinryo.SelectNextControl(!e.Shift) == false) // 次コントロールが存在しない(最終行)
						{
							grid_Gakkai.Select(); // 学会Gridへフォーカス移動
						}
						return;
					}
					else
					if (this.ActiveControl == grid_Gakkai) // Enterでのフォーカス先が学会Grid
					{
						gcom_gakkai.SelectInput(); // Grid内のセルへフォーカス移動 ※Enter押下で行追加機能なし→Grid0行状態ではフォーカスが機能しない

						return;
					}
					else
					if (gcom_gakkai.CheckFocusControl(this.ActiveControl))
					{
						if (gcom_gakkai.SelectNextControl(!e.Shift) == false) // 次コントロールが存在しない(最終行)
						{
							iIryoKikan.Select(); // 医療機関コード選択へフォーカス移動
						}
						return;
					}
					else
					if (this.ActiveControl == grid_Kaihi) // Enterでのフォーカス先が参加医会Grid
					{
						gcom_kaihi.SelectInput(); // Grid内のセルへフォーカス移動

						return;
					}
					else
					if (gcom_kaihi.CheckFocusControl(this.ActiveControl))
					{
						if (gcom_kaihi.SelectNextControl(!e.Shift) == false) // 次コントロールが存在しない(最終行)
						{
							gbxBank.Select(); // 銀行情報グループへフォーカス移動
						}
						return;
					}
					else
					if (this.ActiveControl == grid_Ido) // Enterでのフォーカス先が異動情報Grid
					{
//						gcom_ido.SelectInput(); // Grid内のセルへフォーカス移動
						if (dvKaiinIdo.Count > 0)
						{
							rowEditIdo();
						}
						return;
					}
					break;
			}

			base.FormFrame_KeyDown(sender, e); // 条件合致しない場合、親のメソッド実行
		}

		/// <summary>
		/// フォームロード
		/// </summary>
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
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// 最終更新の情報表示
			t_kaiin tmprow = new t_kaiin(editrow);
			oLastUpdate.Text = $"{tmprow.LastUpdate:yyyy/MM/dd HH:mm}"; // 最終更新日時
			Tanto tt = AppGlobal.Tantos.Get(Cast.Int(tmprow.LastUpdateUser_Null));
			// 該当情報が存在したら名称をreturn
			if (tt != null)
			{
				oLastUpdateUser.Text = tt.Name;
			}
			else
			{
				oLastUpdateUser.Text = null;
			}

			// 日付コントロール(年月表示)の設定
			iSotsugyoNengetsu.UcFormat = UcDate.UcDateFormat.YYYYMM; // 卒業年月
			iSotsugyoNengetsu.UcDateCtl.DropDownCalendar.CalendarType = GrapeCity.Win.Editors.CalendarType.YearMonth;
			iShuryoNengetsu.UcFormat = UcDate.UcDateFormat.YYYYMM; // 修了年月
			iShuryoNengetsu.UcDateCtl.DropDownCalendar.CalendarType = GrapeCity.Win.Editors.CalendarType.YearMonth;
			iGakuiNengetsu.UcFormat = UcDate.UcDateFormat.YYYYMM; // 学位取得年月
			iGakuiNengetsu.UcDateCtl.DropDownCalendar.CalendarType = GrapeCity.Win.Editors.CalendarType.YearMonth;

			// コンボボックスの作成
			// enumから
			AppCombo.SetComboBox(iSex, enumKbn.DSex); // 性別
			AppCombo.SetComboBox(iZaiseki, enumKbn.DTypeZaiseki, (int)eTypeZaiseki.None); // 在籍区分
			AppCombo.SetComboBox(iIkaiNichiiShiharai, enumKbn.DShiharai); // 日医支払方法
			AppCombo.SetComboBox(iIkaiKeniShiharai, enumKbn.DShiharai); // 県医支払方法
			AppCombo.SetComboBox(iIkaiShiiShiharai, enumKbn.DShiharai); // 市医支払方法
			AppCombo.SetComboBox(iTaikaiJiyu, enumKbn.DTypeTaikaiJiyu); // 退会事由
			AppCombo.SetComboBox(iKozaType1, enumKbn.DTypeKoza); // 口座区分 空欄選択あり
			AppCombo.SetComboBox(iKozaType2, enumKbn.DTypeKoza);
			AppCombo.SetComboBox(iKozaType3, enumKbn.DTypeKoza);

			// iSearch
			dvIryoKikan = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_iryokikan)); // 下で会員IDフィルタし情報表示に使うdv
			dvIryoKikanCmb = new DBView(dvIryoKikan); // DBViewを複製(フィルタかけない版)
			AppTableCombo.SetComboBox_IryoKikan(iIryoKikan, dvIryoKikanCmb); // 医療機関コード

			dvKaiinKbn = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiinkbn));
			AppTableCombo.SetComboBox_KaiinKbn(iKaiinKbn, dvKaiinKbn); // 会員区分

			dvIkai = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaihi));
			dvIkai.RowFilterQuery($"{t_kaihi.FKaihi_KaihiType} = {(int)eTypeKaihi.Ishikai}"); // 会費区分1
			dvIkai.SortQuery($"{ t_kaihi.FCD_Kaihi}");
			AppTableCombo.SetComboBox_Kaihi(iIkaiNichii, dvIkai); // 日医会員区分
			AppTableCombo.SetComboBox_Kaihi(iIkaiKeni, dvIkai); // 県医会員区分
			AppTableCombo.SetComboBox_Kaihi(iIkaiShii, dvIkai); // 市医会員区分

			dvIshikai = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_ishikai));
			AppTableCombo.SetComboBox_Ishikai(iIshikaiIdomae, dvIshikai); // 異動前医師会

			dvShisetsugyomu = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_shisetsugyomu));
			AppTableCombo.SetComboBox_Shisetsugyomu(iIryoShisetsugyomu, dvShisetsugyomu); // 医療 - 施設・業務

			// 手動
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

			gctl.Add(new GControlDBText(t_kaiin.FCD_Kaiin, iCode)); // 会員コード
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_Mail1, iMail1)); // メールアドレス
			gctl.Add(new GControlDBNumber(t_kaiin.FKaiin_IsekiTorokuNo, iIsekiTourokuNo)); // 医籍登録番号
			gctl.Add(new GControlDBDate(t_kaiin.FKaiin_DateIsekiToroku, iIsekiTorokuDate.UcDateCtl)); // 医籍登録年月日
			gctl.Add(new GControlDBRuby(
				new string[] {
								t_kaiin.FKaiin_Name,    // 氏名
								t_kaiin.FKaiin_NameKana // 氏名カナ
				},
				new Control[] {
								iName,
								iNameFurigana
				}
			));
			gctl.Add(new GControlDBPostAddr(
				new string[]{
								t_kaiin.FKaiin_Post, // 郵便番号
								t_kaiin.FKaiin_Addr1 // 住所1
				},
				new Control[]{
								iPost1,
								iPost2,
								iAddr1
				}
			));
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_Addr2, iAddr2)); // 住所2
			gctl.Add(new GControlDBHyphenSplit(t_kaiin.FKaiin_Tel1, new Control[] { iTel1_1, iTel1_2, iTel1_3 })); // 電話番号
			gctl.Add(new GControlDBHyphenSplit(t_kaiin.FKaiin_Tel2, new Control[] { iTel2_1, iTel2_2, iTel2_3 })); // 携帯電話番号
			gctl.Add(new GControlDBHyphenSplit(t_kaiin.FKaiin_Fax, new Control[] { iFax_1, iFax_2, iFax_3 })); // FAX
			gctl.Add(new GControlDBDate(t_kaiin.FKaiin_DateBirth, iBirthday.UcDateCtl)); // 生年月日
			gctl.Add(new GControlDBCombo(t_kaiin.FKaiin_Sex, iSex)); // 性別

			gctl.Add(new GControlDBText(t_kaiin.FKaiin_ShusshinKou, iShusshinKou)); // 出身校
			gctl.Add(new GControlDBDate(t_kaiin.FKaiin_DateSotsugyo, iSotsugyoNengetsu.UcDateCtl)); // 卒業年月
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_ShusshinIn, iShusshinDaigakuin)); // 出身校(大学院)
			gctl.Add(new GControlDBDate(t_kaiin.FKaiin_DateShuryo, iShuryoNengetsu.UcDateCtl)); // 修了年月
			gctl.Add(new GControlDBDate(t_kaiin.FKaiin_DateShutokuGakui, iGakuiNengetsu.UcDateCtl)); // 学位取得年月
			gctl.Add(new GControlDBDate(t_kaiin.FKaiin_DateToroku, iTorokuDate.UcDateCtl)); // 登録日
			gctl.Add(new GControlDBCombo(t_kaiin.FID_KaiinKbn, iKaiinKbn.ComboBox)); // 会員区分
			gctl.Add(new GControlDBDate(t_kaiin.FKaiin_DateNyuukai, iNyuukaiDate.UcDateCtl)); // 入会年月日
			gctl.Add(new GControlDBDate(t_kaiin.FKaiin_DateShonin, iShoninDate.UcDateCtl)); // 承認日

			gctl.Add(new GControlDBCombo(t_kaiin.FKaiin_TypeZaiseki, iZaiseki)); // 在籍区分
			gctl.Add(new GControlDBCombo(t_kaiin.FID_Kaihi_Nichii, iIkaiNichii.ComboBox)); // 日医会員区分
			gctl.Add(new GControlDBCombo(t_kaiin.FKaiin_NichiiShiharai, iIkaiNichiiShiharai)); // 日医会員支払方法
			gctl.Add(new GControlDBCombo(t_kaiin.FID_Kaihi_Keni, iIkaiKeni.ComboBox)); // 県医会員区分
			gctl.Add(new GControlDBCombo(t_kaiin.FKaiin_KeniShiharai, iIkaiKeniShiharai)); // 県医会員支払方法
			gctl.Add(new GControlDBCombo(t_kaiin.FID_Kaihi_Shii, iIkaiShii.ComboBox)); // 市医会員区分
			gctl.Add(new GControlDBCombo(t_kaiin.FKaiin_ShiiShiharai, iIkaiShiiShiharai)); // 市医会員支払方法
			gctl.Add(new GControlDBCombo(t_kaiin.FID_Ishikai_Idomae, iIshikaiIdomae.ComboBox)); // 異動前医師会

			// --タブ1--
			gctl.Add(new GControlDBCombo(t_kaiin.FID_Shinryoka_Main, iTantoMainKamoku.ComboBox)); // 診療科目(主)

			// 医療機関
			gctl.Add(new GControlDBCombo(t_kaiin.FID_Iryokikan, iIryoKikan.ComboBox)); // 医療機関コード
			gctl.Add(new GControlDBCombo(t_kaiin.FID_ShisetsuGyomu, iIryoShisetsugyomu.ComboBox)); // 施設・業務
			// 指定医 -> chkbox処理個別対応

			// --タブ2--
			// 銀行
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_BankCode1, iBankCD1)); // 銀行コード
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_BankShitenCode1, iShitenCD1)); // 支店コード
			gctl.Add(new GControlDBCombo(t_kaiin.FKaiin_BankKozaType1, iKozaType1)); // 口座区分
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_BankKozaNo1, iKozaNo1)); // 口座番号
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_BankKozaName1, iKozaName1)); // 口座名義
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_BankCode2, iBankCD2));
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_BankShitenCode2, iShitenCD2));
			gctl.Add(new GControlDBCombo(t_kaiin.FKaiin_BankKozaType2, iKozaType2));
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_BankKozaNo2, iKozaNo2));
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_BankKozaName2, iKozaName2));
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_BankCode3, iBankCD3));
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_BankShitenCode3, iShitenCD3));
			gctl.Add(new GControlDBCombo(t_kaiin.FKaiin_BankKozaType3, iKozaType3));
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_BankKozaNo3, iKozaNo3));
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_BankKozaName3, iKozaName3));

			// --タブ3--
			// 退会
			gctl.Add(new GControlDBDate(t_kaiin.FKaiin_DateTaikai, iTaikaiDate.UcDateCtl)); // 退会年月日
			gctl.Add(new GControlDBCombo(t_kaiin.FKaiin_TypeTaikaiJiyu, iTaikaiJiyu)); // 退会事由
			gctl.Add(new GControlDBText(t_kaiin.FKaiin_TaikaiEtcmemo, iTaikaiJiyuEtc)); // 退会事由

			// 処理区分
			gctl.Add(new GControlDBCombo(t_kaiin.FKaiin_BunshoSofusaki, iBunshoSofu)); // 文書送付先
			gctl.Add(new GControlDBCombo(t_kaiin.FKaiin_FaxSofusaki, iFaxSofu)); // FAX送付先
			gctl.Add(new GControlDBCombo(t_kaiin.FKaiin_KaihiUchiwake, iKaihiUchiwake)); // 月別会費内訳
			gctl.Add(new GControlDBCombo(t_kaiin.FKaiin_KoukaiKbn, iKbnKoukai)); // 公開区分

			gctl.EndAdd(AppDbRule.Rule);

			// 医療機関情報-指定医
			// 指定医情報チェックボックスのTag値設定
			iIryoShiteii1.Tag = AppConst.eShiteii.Hokeni;
			iIryoShiteii2.Tag = AppConst.eShiteii.Botai;
			iIryoShiteii3.Tag = AppConst.eShiteii.Seishin;
			iIryoShiteii4.Tag = AppConst.eShiteii.Etc;

			// 入力させないコントロールの非活性
			iBankName1.Enabled = false; // 銀行名
			iShitenName1.Enabled = false; // 支店名
			iBankName2.Enabled = false; // 銀行名
			iShitenName2.Enabled = false; // 支店名
			iBankName3.Enabled = false; // 銀行名
			iShitenName3.Enabled = false; // 支店名

			// コントロールへDB値セット (中に指定医chkboxセット有)
			rowFetch();

			// 銀行名称1～3表示
			fetchBank();
			fetchShiten();

//			iName.Select(); // フォーカス指定

			// 退会事由その他詳細 状態制御
			enabled_iTaikaiJiyuEtc();

			//■ イベントの登録
			iIryoKikan.TextChanged += iIryoKikan_TextChanged;
			iBankCD1.TextChanged += iBankCD_TextChanged;
			iBankCD1.MouseDoubleClick += cd_MouseDoubleClick;
			iBankCD2.TextChanged += iBankCD_TextChanged;
			iBankCD2.MouseDoubleClick += cd_MouseDoubleClick;
			iBankCD3.TextChanged += iBankCD_TextChanged;
			iBankCD3.MouseDoubleClick += cd_MouseDoubleClick;
			iShitenCD1.TextChanged += iShitenCD_TextChanged;
			iShitenCD1.MouseDoubleClick += cd_MouseDoubleClick;
			iShitenCD2.TextChanged += iShitenCD_TextChanged;
			iShitenCD2.MouseDoubleClick += cd_MouseDoubleClick;
			iShitenCD3.TextChanged += iShitenCD_TextChanged;
			iShitenCD3.MouseDoubleClick += cd_MouseDoubleClick;
			iTaikaiJiyu.SelectedIndexChanged += iTaikaiJiyu_SelectedIndexChanged; // 退会事由cmb

			// 医療機関情報準備
			// 医療機関情報バインド
			gctl_iryo = new GControlDB(this, getDataRowIryo);

			gctl_iryo.Add(new GControlDBText(t_iryokikan.FIRK_Tsusho, iIryoShisetsuName)); // 通称
			gctl_iryo.Add(new GControlDBText(t_iryokikan.FIRK_TsushoKana, iIryoShisetsuNameKana)); // 通称カナ
			gctl_iryo.Add(new GControlDBHyphenSplit(t_iryokikan.FIRK_Post, new Control[] { iIryoPost1, iIryoPost2 })); // 郵便番号
			gctl_iryo.Add(new GControlDBText(t_iryokikan.FIRK_Addr1, iIryoAddr1)); // 住所1
			gctl_iryo.Add(new GControlDBText(t_iryokikan.FIRK_Addr2, iIryoAddr2)); // 住所2
			gctl_iryo.Add(new GControlDBHyphenSplit(t_iryokikan.FIRK_Tel1, new Control[] { iIryoTel1_1, iIryoTel1_2, iIryoTel1_3 })); // TEL
			gctl_iryo.Add(new GControlDBHyphenSplit(t_iryokikan.FIRK_Fax1, new Control[] { iIryoFax1_1, iIryoFax1_2, iIryoFax1_3 })); // FAX
			GControlDBText txtKaisetsuShutai = new GControlDBText(t_iryokikan.FIRK_KaisetsuShutai, iIryoKaisetsushutai); // 開設主体 (ID→名称へ)
			txtKaisetsuShutai.FetchRunEx = KaisetsuShutaiEx; // 文言変換用メソッドへ
			gctl_iryo.Add(txtKaisetsuShutai);
			GControlDBText txtByoshoUmu = new GControlDBText(t_iryokikan.FIRK_ByoshoUmu, iIryoByoshoUmu); // 病床有無
			txtByoshoUmu.FetchRunEx = ByoshoUmuEx; // 文言変換用メソッドへ
			gctl_iryo.Add(txtByoshoUmu);
			gctl_iryo.Add(new GControlDBNumber(t_iryokikan.FIRK_Kyoka, iIryoByoshoCnt)); // 許可病床数
			gctl_iryo.Add(new GControlDBCheckBox(t_iryokikan.FIRK_Kaigo, iIryoHeisetsu)); // 併設の施設-介護施設
			gctl_iryo.Add(new GControlDBCheckBox(t_iryokikan.FIRK_Etc, iIryoHeisetsuEtc)); // 併設の施設-その他
			gctl_iryo.Add(new GControlDBText(t_iryokikan.FIRK_Memo, iIryoHeisetsuName)); // 併設の施設-その他名称
			gctl_iryo.Add(new GControlDBText(t_iryokikan.FIRK_KumiCode, iIryoKumi)); // 組コード

			gctl_iryo.EndAdd(AppDbRule.Rule);

			changeFilterIryoKikanShown(); // 表示中会員の情報のみへフィルタ
			rowFetchIryoKikan(); // コントロール値セット

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
			// ■コントロール設定
			// コンボボックス
			dvShinryo = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_shinryoka));
			AppTableCombo.SetComboBox_Shinryoka(iTantoKamoku, dvShinryo); // iSearch
			AppTableCombo.SetComboBox_Shinryoka(iTantoMainKamoku, dvShinryo); // iSearch(主担当)

			// ■Grid設定
			AppGridCommon.StyleSet(grid_Sinryo);

			gcom_shinryo = new GGridDBCommon(grid_Sinryo, this);

			gcom_shinryo.Add(new GGridDBText(t_kaiin_shinryoka.FID_Shinryoka, "名称", 0));
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
			// ■コントロール設定
			// コンボボックス
			dvGakkai = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_gakkai));
			AppTableCombo.SetComboBox_Gakkai(iSyozokuGakkai, dvGakkai); // iSearch

			// ■Grid設定
			AppGridCommon.StyleSet(grid_Gakkai);

			gcom_gakkai = new GGridDBCommon(grid_Gakkai, this);

			gcom_gakkai.Add(new GGridDBText(t_kaiin_gakkai.FID_Gakkai, "名称", 0));
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
			// ■コントロール設定
			// コンボボックス
			AppCombo.SetComboBox(iShiharai, enumKbn.DShiharai, (int)eShiharai.None); // 支払方法

			dvKaihi = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaihi));
			dvKaihi.RowFilterQuery($"{t_kaihi.FKaihi_KaihiType} = {(int)eTypeKaihi.Ikai}"); // 会費区分2
			dvKaihi.SortQuery($"{t_kaihi.FCD_Kaihi}");
			AppTableCombo.SetComboBox_Kaihi(iSankaIkai, dvKaihi); // iSearch 参加医会

			// ■Grid設定
			AppGridCommon.StyleSet(grid_Kaihi);

			gcom_kaihi = new GGridDBCommon(grid_Kaihi, this);

			gcom_kaihi.Add(new GGridDBText(t_kaiin_kaihi.FID_Kaihi, "名称", 250));
			gcom_kaihi.SetCellDisp(GGridDBCellDisp.Left);
			gcom_kaihi.SetFocusControlInGrid(iSankaIkai); // Gridフォーカス時に表示させるコントロール
			gcom_kaihi.SetUnboundColumnFetch(ubIkaiName); // ID→名称変換
			gcom_kaihi.SetTabIndex(1);
			gcom_kaihi.SetLocked(true);

			gcom_kaihi.Add(new GGridDBText(t_kaiin_kaihi.FKaihiShiharai, "支払方法", 0));
			gcom_kaihi.SetCellDisp(GGridDBCellDisp.Center);
			gcom_kaihi.SetFocusControlInGrid(iShiharai); // Gridフォーカス時に表示させるコントロール
			gcom_kaihi.SetUnboundColumnFetch(ubShiharaiHoho); // 名称変換
			gcom_kaihi.SetTabIndex(2);
			gcom_kaihi.SetLocked(true);

			gcom_kaihi.EndAdd(dvKaiinKaihi);

			// コントロールのDBバインド
			gctl_kaihi = new GControlDB(this, getDataRowKaihi);
			gctl_kaihi.Add(new GControlDBCombo(t_kaiin_kaihi.FID_Kaihi, iSankaIkai.ComboBox));
			gctl_kaihi.Add(new GControlDBCombo(t_kaiin_kaihi.FKaihiShiharai, iShiharai));
			gctl_kaihi.EndAdd(AppDbRule.Rule);

			// イベント登録
			btnKaihiAdd.Click += btnKaihiAdd_Click;
			btnKaihiDel.Click += btnKaihiDel_Click;
			dvKaiinKaihi.RowFetchDemand += dvKaiinKaihi_RowFetchDemand;

			changeFilterKaihi(); // 表示中会員の情報のみへフィルタ
			rowFetchKaihi(); // コントロール値セット(グリッド初期表示値)

			//-----異動情報Grid準備-----
			// ■Grid設定
			AppGridCommon.StyleSet(grid_Ido);

			gcom_ido = new GGridDBCommon(grid_Ido, this);

			gcom_ido.Add(new GGridDBDate(t_kaiin_ido.FIdoDate, "年月日", 90));
			gcom_ido.SetCellDisp(GGridDBCellDisp.Center);
//			gcom_ido.SetUnboundColumnFetch(ubIdoDate); // 編集
//			gcom_ido.SetTabIndex(1);
//			gcom_ido.SetLocked(true);

			gcom_ido.Add(new GGridDBText(t_kaiin_ido.FIdoJiyuKbn, "事由", 90));
			gcom_ido.SetCellDisp(GGridDBCellDisp.Center);
			gcom_ido.SetUnboundColumnFetch(ubIdoJiyuKbn); // 名称変換
//			gcom_ido.SetTabIndex(2);
//			gcom_ido.SetLocked(true);

			gcom_ido.Add(new GGridDBUnbound("", "詳細", ubIdoJiyuDetail, 110)); // 利用DB値が1つではないためGGridDBUnbound()を使用
			gcom_ido.SetCellDisp(GGridDBCellDisp.Center);
//			gcom_ido.SetTabIndex(3);
//			gcom_ido.SetLocked(true);

			gcom_ido.Add(new GGridDBText(t_kaiin_ido.FIdoJiyuEtcMemo, "その他詳細", 0));
			gcom_ido.SetCellDisp(GGridDBCellDisp.Left);
//			gcom_ido.SetTabIndex(4);
//			gcom_ido.SetLocked(true);

			gcom_ido.EndAdd(dvKaiinIdo);

			// イベント登録
			btnIdoAdd.Click += btnIdoAdd_Click;
			btnIdoEdit.Click += btnIdoEdit_Click;
			btnIdoDel.Click += btnIdoDel_Click;
			grid_Ido.MouseDoubleClick += grid_Ido_MouseDoubleClick; // 編集へ
			dvKaiinIdo.RowFetchDemand += dvKaiinIdo_RowFetchDemand; // 編集へ

			changeFilterIdo(); // 表示中会員の情報のみへフィルタ

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
		/// 退会事由コンボボックス変更
		/// </summary>
		void iTaikaiJiyu_SelectedIndexChanged(object sender, EventArgs e)
		{
			enabled_iTaikaiJiyuEtc();
		}

		/// <summary>
		/// 退会事由その他テキストボックス制御
		/// </summary>
		void enabled_iTaikaiJiyuEtc()
		{
			if (iTaikaiJiyu.SelectedIndex == (int)eTypeTaikaiJiyu.Etc)
			{
				iTaikaiJiyuEtc.Enabled = true; // 退会事由:その他→活性
			}
			else
			{
				iTaikaiJiyuEtc.Enabled = false;
			}
		}

		/// <summary>
		/// 銀行-銀行コード 値変更
		/// </summary>
		private void iBankCD_TextChanged(object sender, EventArgs e)
		{
			fetchBank((GControlGcTextBoxEx.GcTextBoxEx)sender);
			fetchShiten((GControlGcTextBoxEx.GcTextBoxEx)sender);
		}

		/// <summary>
		/// 銀行-支店コード 値変更
		/// </summary>
		private void iShitenCD_TextChanged(object sender, EventArgs e)
		{
			fetchShiten((GControlGcTextBoxEx.GcTextBoxEx)sender);
		}

		/// <summary>
		/// 銀行-銀行コード,支店コード ダブルクリック
		/// </summary>
		private void cd_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			showBankCode((GControlGcTextBoxEx.GcTextBoxEx)sender);
		}

		/// <summary>
		/// 銀行-銀行名,支店名のセット(口座1～3)
		/// </summary>
		void fetchBank()
		{
			BankCode bc;
			string name = "";

			// 銀行名のセット(口座1)
			bc = AppGlobal.BankCodeMg.GetBankCode(Cast.Int(iBankCD1.Text));
			name = "";
			if (bc != null)
			{
				name = bc.Name;
			}
			iBankName1.Text = name;

			// 銀行名のセット(口座2)
			bc = AppGlobal.BankCodeMg.GetBankCode(Cast.Int(iBankCD2.Text));
			name = "";
			if (bc != null)
			{
				name = bc.Name;
			}
			iBankName2.Text = name;

			// 銀行名のセット(口座3)
			bc = AppGlobal.BankCodeMg.GetBankCode(Cast.Int(iBankCD3.Text));
			name = "";
			if (bc != null)
			{
				name = bc.Name;
			}
			iBankName3.Text = name;
		}

		/// <summary>
		/// 銀行-支店名のセット(口座1～3)
		/// </summary>
		void fetchShiten()
		{
			t_bank_code si;
			string name = "";

			// 銀行-支店名のセット(口座1)
			si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD1.Text), Cast.Int(iShitenCD1.Text));
			if (si != null)
			{
				name = si.BCD_NameShiten + "支店";
			}
			iShitenName1.Text = name;

			// 銀行-支店名のセット(口座2)
			si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD2.Text), Cast.Int(iShitenCD2.Text));
			name = "";
			if (si != null)
			{
				name = si.BCD_NameShiten + "支店";
			}
			iShitenName2.Text = name;

			// 銀行-支店名のセット(口座3)
			si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD3.Text), Cast.Int(iShitenCD3.Text));
			name = "";
			if (si != null)
			{
				name = si.BCD_NameShiten + "支店";
			}
			iShitenName3.Text = name;
		}

		/// <summary>
		/// 銀行-銀行名のセット(入力時イベント用)
		/// </summary>
		void fetchBank(GControlGcTextBoxEx.GcTextBoxEx txtbox)
		{
			// 銀行名の取得
			BankCode bc = AppGlobal.BankCodeMg.GetBankCode(Cast.Int(txtbox.Text));
			string name = "";
			if (bc != null)
			{
				name = bc.Name;
			}

			// イベント発生元分岐 文字列セット先決定
			switch (txtbox.Name)
			{
				case "iBankCD1":
					iBankName1.Text = name;
					break;
				case "iBankCD2":
					iBankName2.Text = name;
					break;
				case "iBankCD3":
					iBankName3.Text = name;
					break;
			}
		}

		/// <summary>
		/// 銀行-支店名のセット(入力時イベント用)
		/// </summary>
		void fetchShiten(GControlGcTextBoxEx.GcTextBoxEx txtbox)
		{
			t_bank_code si;
			string name = "";

			// イベント発生元分岐 銀行コード欄の特定やテキストセット先の決定
			switch (txtbox.Name)
			{
				case "iBankCD1":
				case "iShitenCD1":
					si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD1.Text), Cast.Int(iShitenCD1.Text));
					if (si != null)
					{
						name = si.BCD_NameShiten + "支店";
					}
					iShitenName1.Text = name;
					break;

				case "iBankCD2":
				case "iShitenCD2":
					si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD2.Text), Cast.Int(iShitenCD2.Text));
					if (si != null)
					{
						name = si.BCD_NameShiten + "支店";
					}
					iShitenName2.Text = name;
					break;

				case "iBankCD3":
				case "iShitenCD3":
					si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD3.Text), Cast.Int(iShitenCD3.Text));
					if (si != null)
					{
						name = si.BCD_NameShiten + "支店";
					}
					iShitenName3.Text = name;
					break;
			}
		}

		/// <summary>
		/// 銀行コード選択呼出し
		/// </summary>
		void showBankCode(GControlGcTextBoxEx.GcTextBoxEx txtbox)
		{
			t_kaiin xrow = new t_kaiin(editrow);
			FormSelectorBankCode frm = new FormSelectorBankCode();

			// イベント発生元分岐 銀行コード欄の特定やテキストセット先の決定
			switch (txtbox.Name)
			{
				case "iBankCD1":
				case "iShitenCD1":
//					FormSelectorBankCode frm = new FormSelectorBankCode();
					frm.SelectedBankCode = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD1.Text), Cast.Int(iShitenCD1.Text));
//					frm.SelectedBankCode = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(xrow.Kaiin_BankCode1_Null), Cast.Int(xrow.Kaiin_BankShitenCode1_Null));
					frm.ShowDialog();
					if (frm.FormCloseReason == FormCloseReason.Exec)
					{
						xrow.Kaiin_BankCode1 = frm.SelectedBankCode.BCD_Code;
						xrow.Kaiin_BankShitenCode1 = frm.SelectedBankCode.BCD_CodeShiten;

						rowFetch();
					}
					break;

				case "iBankCD2":
				case "iShitenCD2":
					frm.SelectedBankCode = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD2.Text), Cast.Int(iShitenCD2.Text));
					frm.ShowDialog();
					if (frm.FormCloseReason == FormCloseReason.Exec)
					{
						xrow.Kaiin_BankCode2 = frm.SelectedBankCode.BCD_Code;
						xrow.Kaiin_BankShitenCode2 = frm.SelectedBankCode.BCD_CodeShiten;

						rowFetch();
					}
					break;

				case "iBankCD3":
				case "iShitenCD3":
					frm.SelectedBankCode = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD3.Text), Cast.Int(iShitenCD3.Text));
					frm.ShowDialog();
					if (frm.FormCloseReason == FormCloseReason.Exec)
					{
						xrow.Kaiin_BankCode3 = frm.SelectedBankCode.BCD_Code;
						xrow.Kaiin_BankShitenCode3 = frm.SelectedBankCode.BCD_CodeShiten;

						rowFetch();
					}
					break;
			}
		}

		// --医療機関情報関連--
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
		/// 指定医情報をチェックボックスにセットします。
		/// </summary>
		void fetchIryoShiteii()
		{
			int si = Cast.Int(this.Row[t_kaiin.FKaiin_Shiteii]);

			// チェックのコントロール配列

			List<YControlYcCheckBoxEx.YcCheckBoxEx> shiteiis = new List<YControlYcCheckBoxEx.YcCheckBoxEx>()
			{
				iIryoShiteii1, iIryoShiteii2, iIryoShiteii3, iIryoShiteii4,
			};

			// コントロールごとに処理
			foreach (YControlYcCheckBoxEx.YcCheckBoxEx chk in shiteiis)
			{
				if ((si & (int)chk.Tag) != 0)
				{
					chk.Checked = true;
				}
			}
		}

		/// <summary>
		/// 指定医情報をDB対象フィールドにセットします。
		/// </summary>
		void updateIryoShiteii()
		{
			int shiteii = 0;

			// チェックのコントロール配列
			List<YControlYcCheckBoxEx.YcCheckBoxEx> shiteiis = new List<YControlYcCheckBoxEx.YcCheckBoxEx>()
			{
				iIryoShiteii1, iIryoShiteii2, iIryoShiteii3, iIryoShiteii4,
			};

			// コントロールごとに処理
			foreach (YControlYcCheckBoxEx.YcCheckBoxEx chk in shiteiis)
			{
				if (chk.Checked == true)
				{
					shiteii |= (int)chk.Tag;
				}
			}

			this.Row[t_kaiin.FKaiin_Shiteii] = shiteii;
		}

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
			t_kaiin tmprow = new t_kaiin(editrow);

			filter =
				$"{t_iryokikan.FID_Iryokikan} = {tmprow.ID_Iryokikan}";

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
			if (gctl_iryo == null)
			{
				//				Debug.WriteLine("GControlDBが設定されていません。");
				return;
			}

			if (dvIryoKikan.CurrentRow == null)
			{
				// 現在行がないので、コントロールの内容をクリアする。
				gctl_iryo.ClearAll();
			}
			else
			{
				// 現在行を取得
				gctl_iryo.FetchAll();
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

		// --診療Grid--
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

			// 該当会員の情報へフィルタ
			t_kaiin tmprow = new t_kaiin(editrow);

			filter =
				$"{t_kaiin_shinryoka.FID_Kaiin} = {tmprow.ID_Kaiin}";

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
			t_kaiin tmprow = new t_kaiin(editrow);
			t_kaiin_shinryoka nrow = new t_kaiin_shinryoka(dvKaiinShinryo.NewRow());

			// NewRowへフィルタ済の会員IDセット
			nrow.ID_Kaiin = tmprow.ID_Kaiin;

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

			// 該当会員の情報へフィルタ
			t_kaiin tmprow = new t_kaiin(editrow);

			filter =
				$"{t_kaiin_gakkai.FID_Kaiin} = {tmprow.ID_Kaiin}";

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
			t_kaiin tmprow = new t_kaiin(editrow);
			t_kaiin_gakkai nrow = new t_kaiin_gakkai(dvKaiinGakkai.NewRow());

			// NewRowへフィルタ済の会員IDセット
			nrow.ID_Kaiin = tmprow.ID_Kaiin;

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
		/// Enum値より、Gridに支払方法名を表示します。
		/// </summary>
		string ubShiharaiHoho(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaiin_kaihi xrow = new t_kaiin_kaihi(dvKaiinKaihi[e.Row]);
//			eShiharai hoho = Cast.Enumelate<eShiharai>(xrow.KaihiShiharai_Null, eShiharai.None);
			//			Kaihi kh = AppGlobal.Kaihis.Get(xrow.ID_Kaihi);

			if (enumKbn.DShiharai.ContainsKey((int)xrow.KaihiShiharai) == true)
			{
				return enumKbn.DShiharai[(int)xrow.KaihiShiharai];
			}

			return null;
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

			// 該当会員の情報へフィルタ
			t_kaiin tmprow = new t_kaiin(editrow);

			filter =
				$"{t_kaiin_kaihi.FID_Kaiin} = {tmprow.ID_Kaiin}";

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
			t_kaiin tmprow = new t_kaiin(editrow);
			t_kaiin_kaihi nrow = new t_kaiin_kaihi(dvKaiinKaihi.NewRow());

			// NewRowへフィルタ済の会員IDセット
			nrow.ID_Kaiin = tmprow.ID_Kaiin;

			// 支払方法デフォルトセット
			nrow.KaihiShiharai = eShiharai.Koza1;

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
		/// 変更要求処理(参加医会Grid)
		/// </summary>
		void dvKaiinKaihi_RowFetchDemand(object sender, EventArgs e)
		{
			// 再フィルタ
			changeFilterKaihi();

			// コントロール値セット
			rowFetchKaihi();
		}

		// --異動情報Grid--
		/// <summary>
		/// Gridに異動事由名称を表示します(Enum値より)。
		/// </summary>
		string ubIdoJiyuKbn(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaiin_ido xrow = new t_kaiin_ido(dvKaiinIdo[e.Row]);

			//			string name = "";
			//			if (kh != null)
			//			{
			//				name = kh.XRow.Kaihi_Name;
			//			}

			//			return Cast.String(enumKbn.DTypeIdoJiyu[(int)xrow.IdoJiyuKbn]);
			return enumKbn.DTypeIdoJiyu[(int)xrow.IdoJiyuKbn];
		}

		/// <summary>
		/// Gridに異動詳細名称を表示します(Enum値より)。
		/// </summary>
		string ubIdoJiyuDetail(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaiin_ido xrow = new t_kaiin_ido(dvKaiinIdo[e.Row]);
			string name = "";

			// 異動事由区分により詳細の参照値を決定
			switch (xrow.IdoJiyuKbn)
			{
				// 施設開業
				case eTypeIdoJiyu.Kaigyo:
					name = ""; // 詳細選択肢なし
					break;

				// 施設異動
				case eTypeIdoJiyu.Ido:
					name = enumKbn.DIdoShisetsu[(int)xrow.IdoJiyuDetail_Ido];
					break;

				// 会員区分変更
				case eTypeIdoJiyu.Kaiin:
					name = enumKbn.DIdoKaiinHenko[(int)xrow.IdoJiyuDetail_Kaiin];
					break;

				// その他
				case eTypeIdoJiyu.Etc:
					name = enumKbn.DIdoEtc[(int)xrow.IdoJiyuDetail_Etc];
					break;

				default:
					name = ""; // 詳細選択肢なし
					break;
			}

			return name;
		}

//		/// <summary>
//		/// 異動情報gridの現在行を返します。
//		/// </summary>
//		/// <param name="datatag">タグ</param>
//		/// <returns>現在行</returns>
//		DataRow getDataRowIdo(string datatag)
//		{
//			if (dvKaiinIdo.CurrentRow != null && dvKaiinIdo.Count > 0)
//			{
//				return dvKaiinIdo.CurrentRow.Row;
//			}

//			return null;
//		}

		/// <summary>
		/// フィルタ変更(t_kaiin_ido)
		/// </summary>
		void changeFilterIdo()
		{
			string filter = "";

			// 該当会員の情報へフィルタ
			t_kaiin tmprow = new t_kaiin(editrow);

			filter =
				$"{t_kaiin_ido.FID_Kaiin} = {tmprow.ID_Kaiin}";

			// 適用
			dvKaiinIdo.RowFilterQuery(filter);
		}

		/// <summary>
		/// 追加ボタン(異動情報Grid)
		/// </summary>
		private void btnIdoAdd_Click(object sender, EventArgs e)
		{
			t_kaiin tmprow = new t_kaiin(editrow);
			t_kaiin_ido nrow = new t_kaiin_ido(dvKaiinIdo.NewRow());

			// NewRowへ内部項目をセット
			nrow.ID_Kaiin = tmprow.ID_Kaiin; // フィルタ済の会員ID
			nrow.ID_Ido = AppDbID.GetNewID(dvKaiinIdo, t_kaiin_ido.FID_Ido); // 異動情報ID(新規)

			// 編集画面表示
			FormKaiin_DlgEntry_Idoinfo frm = new FormKaiin_DlgEntry_Idoinfo();
			frm.Mode = FormKaiin_DlgEntry_Idoinfo.eMode.Add;
			frm.Row = nrow.Row;
			frm.ShowDialog();

			if (frm.FormCloseReason == FormCloseReason.Save)
			{
				// DBViewへ行追加
				dvKaiinIdo.Add(frm.Row);

				// 追加行にフォーカス
				dvKaiinIdo.SearchRow(t_kaiin_ido.FID_Ido, nrow.ID_Ido);
			}

			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 訂正ボタン(異動情報Grid)
		/// </summary>
		private void btnIdoEdit_Click(object sender, EventArgs e)
		{
			if (dvKaiinIdo.Count > 0)
			{
				rowEditIdo();
			}
		}

		/// <summary>
		/// ダブルクリック(異動情報Grid)
		/// </summary>
		private void grid_Ido_MouseDoubleClick(object sender, EventArgs e)
		{
			if (dvKaiinIdo.Count > 0)
			{
				rowEditIdo();
			}
		}

		/// <summary>
		/// 編集(異動情報Grid)
		/// </summary>
		void rowEditIdo()
		{
			if (dvKaiinIdo.Count > 0)
			{
				DataRow row = dvKaiinIdo.NewRow();

				AppDb.CopyDataRow(dvKaiinIdo.CurrentRow.Row, row);

				FormKaiin_DlgEntry_Idoinfo frm = new FormKaiin_DlgEntry_Idoinfo();
				frm.Mode = FormKaiin_DlgEntry_Idoinfo.eMode.Edit;
				frm.Row = row;
				frm.ShowDialog();

				if (frm.FormCloseReason == FormCloseReason.Save)
				{
					AppDb.CopyDataRow(frm.Row, dvKaiinIdo.CurrentRow.Row);
					// DB更新はDlgEntry呼び元画面で実施
				}

				frm.Dispose();
				frm = null;
			}
		}

		/// <summary>
		/// 削除(異動情報Grid)
		/// </summary>
		private void btnIdoDel_Click(object sender, EventArgs e)
		{
			if (dvKaiinIdo.Count > 0)
			{
				t_kaiin_ido xrow = new t_kaiin_ido(dvKaiinIdo.CurrentRow);

				// 他で使われているかチェック,必要か検討 → 医会会費のときは必要そう？
				//				if (AppDbKeiyaku.CheckUsedField(T_Keiyaku.FID_BukkenRoom, xrow.ID_BukkenRoom) == true)
				//				{
				//					AppMsgBox.Show(AppMsgBoxIndex.CanNotDeleteReaseonUsed, xrow.BukkenRoom_Name);
				//					return;
				//				}

				DialogResult dr = AppMsgBox.Show(AppMsgBoxIndex.Delete, "選択中の異動情報");
				if (dr == DialogResult.No)
				{
					return;
				}

				dvKaiinIdo.Delete();
				grid_Ido.Refresh();

				// 入力状態になっているかもしれないので、グリッドへフォーカスを移す。
				gcom_ido.Select();
			}
		}

		/// <summary>
		/// 変更要求処理(異動情報Grid)
		/// </summary>
		void dvKaiinIdo_RowFetchDemand(object sender, EventArgs e)
		{
			// 再フィルタ
			changeFilterIdo();
		}

		/// <summary>
		/// 行情報の取得(会員情報)
		/// </summary>
		void rowFetch()
		{
			if (Row != null)
			{
				gctl.FetchAll();
				
				// 医療機関-指定医のセット
				fetchIryoShiteii();
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
			appFuncKey = new AppFunctionKey(funckey, FuncKaiin_DlgEntry.Functions);

			FuncKaiin_DlgEntry.Save.Execute = saveClose;
			FuncKaiin_DlgEntry.Cancel.Execute = closeCancel;

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

			// 医療機関-指定医のDB値セット
			updateIryoShiteii();

			// 更新担当者のセット
			this.Row[t_kaiin.FLastUpdateUser] = AppGlobal.LoginUser.ID;

			// 退会事由-その他詳細のDB値をクリア
			if (iTaikaiJiyu.SelectedIndex != (int)eTypeTaikaiJiyu.Etc) // 退会事由:その他 以外を選択
			{
				this.Row[t_kaiin.FKaiin_TaikaiEtcmemo] = null;
			}

			// 空白チェック
			Control[] ctls =
			{
				iCode, iName, iNameFurigana, iZaiseki, iTorokuDate, iBunshoSofu,
			};

			foreach (Control ctl in ctls)
			{
				int len = ctl.Text.Length;

				if (ctl is UcDate)
				{
					UcDate tmp = (UcDate)ctl; // プロパティ指定のためいったん変換して格納
					if (tmp.UcValue == null) // 日付変換できない場合UcValueはnull
					{
						ctl.Select();
						appToolTip.Show(ctl, AppToolTipIndex.CannotUseBlank);
						return false;
					}
				}
				else
				{
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
			}

			// 重複チェック
			Kaiin obj = AppGlobal.Kaiins.GetCode(iCode.Text);

			if (obj != null && obj.ID != Cast.Int(editrow[t_kaiin.FID_Kaiin]))
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
