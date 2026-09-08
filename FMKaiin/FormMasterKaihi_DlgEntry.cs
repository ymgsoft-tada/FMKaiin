using ComponentDB;
using ComponentGControlDB;
using ComponentIO;
using GControlGcTextBoxEx;
using GControlGcNumberEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 tanaka]
	/// 医会会費マスタ 情報画面
	/// </summary>
	public partial class FormMasterKaihi_DlgEntry : FormFrame
	{
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
		public eMode Mode { get; set; }

		/// <summary>
		/// 画面項目とDBとの関連情報
		/// </summary>
		GControlDB gctl;

		/// <summary>
		/// 画面起動時のレコード状態
		/// </summary>
		DataRow srcrow = null;
		/// <summary>
		/// 編集するレコード
		/// </summary>
		DataRow editrow = null;

		/// <summary>
		/// 画面間連携用
		/// </summary>
		public DataRow Row
		{
			get
			{
				return editrow;
			}
			set
			{
				srcrow = value; // 元画面から受取った情報セット
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
		/// 月額費用コントロール配列
		/// </summary>
		GcNumberEx[] iCosts;

		/// <summary>
		/// 月額費用field
		/// </summary>
		string[] costsField;


	/// <summary>
	/// コンストラクタ
	/// </summary>
	public FormMasterKaihi_DlgEntry()
		{
			InitializeComponent();

			Mode = eMode.Add;

			// 月額費用1～12のコントロールとt_kaihiカラム名を配列化
			iCosts = new GcNumberEx[]
			{
				iCost1,  iCost2,  iCost3,
				iCost4,  iCost5,  iCost6,
				iCost7,  iCost8,  iCost9,
				iCost10, iCost11, iCost12,
			};

			costsField = new string[]
			{
				t_kaihi.FKaihi_GetsugakuCost1,  t_kaihi.FKaihi_GetsugakuCost2,
				t_kaihi.FKaihi_GetsugakuCost3,  t_kaihi.FKaihi_GetsugakuCost4,
				t_kaihi.FKaihi_GetsugakuCost5,  t_kaihi.FKaihi_GetsugakuCost6,
				t_kaihi.FKaihi_GetsugakuCost7,  t_kaihi.FKaihi_GetsugakuCost8,
				t_kaihi.FKaihi_GetsugakuCost9,  t_kaihi.FKaihi_GetsugakuCost10,
				t_kaihi.FKaihi_GetsugakuCost11, t_kaihi.FKaihi_GetsugakuCost12,
			};
		}

		protected override void FormFrame_Load(object sender, EventArgs e)
		{
			string title = "【追加】";

			if (Mode == eMode.Edit)
			{
				title = "【訂正】";
			}

			this.Text +=  title;
			base.FormFrame_Load(sender, e);
		}

		/// <summary>
		/// ロード処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// コンボボックスの値セット
			// enumをセット
			AppCombo.SetComboBox(iKozaType, enumKbn.DTypeKoza, (int)eTypeKoza.None);
//			AppCombo.SetComboBox(iBankIfaxType, enumKbn.DTypeIfax); テーブル取得へ修正する
// エラー出ないよう1データ手動セット
			iBankIfax.ExBeginUpdate();
			iBankIfax.ExAddItem("その他", 1);
			iBankIfax.ExEndUpdate();
//ここまで

			// iSearch
			//			AppTableCombo.SetComboBox_Shokumu(iKbnKaihi);

			// 手動セット
			iSearchUsed.ExBeginUpdate();
			iSearchUsed.ExAddItem("表示する", true);
			iSearchUsed.ExAddItem("表示しない", false);
			iSearchUsed.ExEndUpdate();

			iKbnKaihi.ExBeginUpdate(); // iKbnKaihi(仮) 会費区分コードはテーブルから取得予定
			iKbnKaihi.ExAddItem("医師会会費", 1);
			iKbnKaihi.ExEndUpdate();

			// 画面～DBの紐づけ
			gctl = new GControlDB(this, getDataRow);

//			gctl.Add(new GControlDBCombo(t_kaihi.FID_KbnKaihi, iKbnKaihi.ComboBox)); // 会費区分
			gctl.Add(new GControlDBCombo(t_kaihi.FID_KaihiKbn, iKbnKaihi)); // 会費区分
			gctl.Add(new GControlDBText(t_kaihi.FCD_Kaihi, iCode)); // 会費コード
//			gctl.Add(new GControlDBText(t_kaihi.FKaihi_Name, iName)); // 会費印刷用名称
//			gctl.Add(new GControlDBText(t_kaihi.FKaihi_ShortName, iShortName)); // 会費略称
			gctl.Add(new GControlDBRuby( // 2つ目にカナ入力される
							new string[] {
								t_kaihi.FKaihi_Name, // 会費印刷用名称
								t_kaihi.FKaihi_ShortName}, // 会費略称
							new Control[] {
								iName,
								iShortName
							}));

			gctl.Add(new GControlDBText(t_kaihi.FKaihi_Bikou, iBikou)); // 備考
			gctl.Add(new GControlDBNumber(t_kaihi.FKaihi_GunCode, iKaihiGunCode)); // 会費群
			gctl.Add(new GControlDBCombo(t_kaihi.FKaihi_SearchUsed, iSearchUsed)); // 検索時表示区分

			// 月額費用
			gctl.Add(new GControlDBNumber(t_kaihi.FKaihi_GetsugakuCost1, iCost1));
			gctl.Add(new GControlDBNumber(t_kaihi.FKaihi_GetsugakuCost2, iCost2));
			gctl.Add(new GControlDBNumber(t_kaihi.FKaihi_GetsugakuCost3, iCost3));
			gctl.Add(new GControlDBNumber(t_kaihi.FKaihi_GetsugakuCost4, iCost4));
			gctl.Add(new GControlDBNumber(t_kaihi.FKaihi_GetsugakuCost5, iCost5));
			gctl.Add(new GControlDBNumber(t_kaihi.FKaihi_GetsugakuCost6, iCost6));
			gctl.Add(new GControlDBNumber(t_kaihi.FKaihi_GetsugakuCost7, iCost7));
			gctl.Add(new GControlDBNumber(t_kaihi.FKaihi_GetsugakuCost8, iCost8));
			gctl.Add(new GControlDBNumber(t_kaihi.FKaihi_GetsugakuCost9, iCost9));
			gctl.Add(new GControlDBNumber(t_kaihi.FKaihi_GetsugakuCost10, iCost10));
			gctl.Add(new GControlDBNumber(t_kaihi.FKaihi_GetsugakuCost11, iCost11));
			gctl.Add(new GControlDBNumber(t_kaihi.FKaihi_GetsugakuCost12, iCost12));

			// 銀行情報
			gctl.Add(new GControlDBText(t_kaihi.FKaihi_BankCode, iBankCD));
			gctl.Add(new GControlDBText(t_kaihi.FKaihi_BankCodeShiten, iShitenCD));
			gctl.Add(new GControlDBCombo(t_kaihi.FKaihi_BankKozaType, iKozaType));
			gctl.Add(new GControlDBText(t_kaihi.FKaihi_BankKozaNo, iKozaNo));
			gctl.Add(new GControlDBText(t_kaihi.FKaihi_BankKozaName, iKozaName));
			gctl.Add(new GControlDBCombo(t_kaihi.FKaihi_BankIfax, iBankIfax));

			gctl.EndAdd(AppDbRule.Rule);

			rowFetch();

			fetchBank();
			fetchShiten();

			refreshTotal();

			// 入力させないコントロールの非活性
			oCostAll.Enabled = false; // 月額費用-合計
			iBankName.Enabled = false; // 銀行名
			iShitenName.Enabled = false; // 支店名

			iCode.Select(); // 初期フォーカスどこに当てるか指定あれば設定

			//■ イベントの登録
			iBankCD.TextChanged += iBankCD_TextChanged;
			iShitenCD.TextChanged += iShitenCD_TextChanged;
			iBankCD.MouseDoubleClick += cd_MouseDoubleClick;
			iShitenCD.MouseDoubleClick += cd_MouseDoubleClick;

			// 月額費用1～12
			foreach (GcNumberEx ctl in iCosts)
			{
				// テキスト変更時に合計更新
				ctl.ValueChanged += iCost_ValueChanged;
			}

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// グリッド上マウスダブルクリック
		/// </summary>
		private void cd_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			showBankCode();
		}

		/// <summary>
		/// 銀行-銀行コード 値変更
		/// </summary>
		private void iBankCD_TextChanged(object sender, EventArgs e)
		{
			fetchBank();
			fetchShiten();
		}

		/// <summary>
		/// 銀行-支店コード 値変更
		/// </summary>
		private void iShitenCD_TextChanged(object sender, EventArgs e)
		{
			fetchShiten();
		}

		/// <summary>
		/// 銀行-銀行名のセット
		/// </summary>
		void fetchBank()
		{
			BankCode bc = AppGlobal.BankCodeMg.GetBankCode(Cast.Int(iBankCD.Text));

			string name = "";
			if (bc != null)
			{
				name = bc.Name;
			}

			iBankName.Text = name;
		}

		/// <summary>
		/// 銀行-支店名のセット
		/// </summary>
		void fetchShiten()
		{
			t_bank_code si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD.Text), Cast.Int(iShitenCD.Text));

			string name = "";
			if (si != null)
			{
				name = si.BCD_NameShiten + "支店";
			}

			iShitenName.Text = name;
		}

		/// <summary>
		/// 月額費用 値変更
		/// </summary>
		private void iCost_ValueChanged(object sender, EventArgs e)
		{
			gctl.UpdateByControl((Control)sender); // 該当コントロール値→row更新
			refreshTotal();
		}

		/// <summary>
		/// 会費合計の更新
		/// </summary>
		void refreshTotal()
		{
			decimal costTotal = 0m;

			foreach (string field in costsField)
			{
				costTotal += Cast.Decimal(Row[field]);
			}

			oCostAll.Value = costTotal;
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
		/// 閉じる処理
		/// </summary>
		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (formCloseReason != App.FormCloseReason.Save)
			{
				// フォーカスLeaveが動いていない場合に備えて、rowを更新する
				// 複数コントロール組合せで作られているものは、rowと紐づけたコントロールを指定
				if (this.ActiveControl is UcDate)
				{
					gctl.UpdateByControl(((UcDate)this.ActiveControl).UcDateCtl);
				}
				else
				if (this.ActiveControl is UcTableComboBox)
				{
					gctl.UpdateByControl(((UcTableComboBox)this.ActiveControl).ComboBox);
				}
				else
				{
					// 単一コントロールはそのままUpdate
					gctl.Update(this);
				}

				// ここで訂正時の変更を検知するために、上でrow値を更新する
				if (Mode == eMode.Add || AppDb.CheckModified(srcrow, editrow) == true)
				{
					if (AppMsgBox.Show(AppMsgBoxIndex.CancelClose) == DialogResult.No) // データ破棄メッセージ
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
			appFuncKey = new AppFunctionKey(funckey, FuncMasterKaihi_DlgEntry.Functions);

			FuncMasterKaihi_DlgEntry.Save.Execute   = saveClose;   // 登録
			FuncMasterKaihi_DlgEntry.Cancel.Execute = closeCancel; // キャンセル
		}

		/// <summary>
		/// 銀行コード選択呼出し
		/// </summary>
		void showBankCode()
		{
			t_kaihi xrow = new t_kaihi(editrow);

			FormSelectorBankCode frm = new FormSelectorBankCode();
			frm.SelectedBankCode = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(xrow.Kaihi_BankCode), Cast.Int(xrow.Kaihi_BankCodeShiten));
			frm.ShowDialog();
			if (frm.FormCloseReason == FormCloseReason.Exec)
			{				
				xrow.Kaihi_BankCode = frm.SelectedBankCode.BCD_Code;
				xrow.Kaihi_BankCodeShiten = frm.SelectedBankCode.BCD_CodeShiten;

				rowFetch();
			}
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
			Control [] ctls =
			{
				iCode, iName, iShortName, // ★必須は仕様確認
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
					appToolTip.Show(ctl,AppToolTipIndex.CannotUseBlank);
					return false;
				}
			}

			// 重複チェック
			Kaihi obj = AppGlobal.Kaihis.GetCode(iCode.Text);

			if (obj != null && obj.ID != Cast.Int(editrow[t_kaihi.FID_Kaihi]))
			{
				iCode.Select();
				appToolTip.Show(iCode, AppToolTipIndex.BookingCode);
				return false;
			}

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
