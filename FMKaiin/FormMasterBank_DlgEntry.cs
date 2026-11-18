using ComponentDB;
using ComponentGControlDB;
using ComponentIO;
using GControlGcTextBoxEx;
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
	/// 銀行情報画面  (給与システム流用)
	/// </summary>
	public partial class FormMasterBank_DlgEntry : FormFrame
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
			/// <summary>コピー追加</summary>
			Copy
		}

		/// <summary>
		/// 処理モード
		/// </summary>
		public eMode Mode { get; set;}

		GControlDB gctl;

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
		public FormMasterBank_DlgEntry()
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
			else
			if (Mode == eMode.Copy)
			{
				title = "【コピー追加】";
			}

			this.Text +=  title;
			base.FormFrame_Load(sender, e);
		}

		/// <summary>
		/// ロード処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			//　コンボボックスの作成
			AppCombo.SetComboBox(iKozaType, enumKbn.DTypeKoza, (int)eTypeKoza.None);

			gctl = new GControlDB(this, getDataRow);

			gctl.Add(new GControlDBText(t_bank.FBNK_Code, iBankCode));
			gctl.Add(new GControlDBText(t_bank.FBNK_CodeShiten, iShitenCode));
			gctl.Add(new GControlDBCombo(t_bank.FBNK_KozaType, iKozaType));
			gctl.Add(new GControlDBText(t_bank.FBNK_KozaNo, iKozaNo));
			gctl.Add(new GControlDBText(t_bank.FBNK_CodeCompany, iCodeCompany));

			gctl.EndAdd(AppDbRule.Rule);

			rowFetch();

			fetchBank();
			fetchShiten();

			iNameBank.Enabled		= false;
			iNameKanaBank.Enabled	= false;
			iNameShiten.Enabled		= false;
			iNameKanaShiten.Enabled	= false;

			iBankCode.Select();

			//■ イベントの登録
			iBankCode.TextChanged += iBankCode_Validated;
			iShitenCode.TextChanged += iShitenCode_Validated;
			iBankCode.MouseDoubleClick += cd_MouseDoubleClick;
			iShitenCode.MouseDoubleClick += cd_MouseDoubleClick;
			//iBank.SelectIndexChanged += iBank_SelectIndexChanged;
			//iShiten.SelectIndexChanged += iShiten_SelectIndexChanged;

			base.FormFrame_Shown(sender, e);
		}

		private void cd_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			showBankCode();
		}

		private void iBankCode_Validated(object sender, EventArgs e)
		{
			fetchBank();
			fetchShiten();
		}

		private void iShitenCode_Validated(object sender, EventArgs e)
		{
			fetchShiten();
		}

		void fetchBank()
		{
			BankCode bc = AppGlobal.BankCodeMg.GetBankCode(Cast.Int(iBankCode.Text));

			string name = "";
			string kana = "";
			if (bc != null)
			{
				name = bc.Name;
				kana = bc.Furigana;
			}

			iNameBank.Text		= name;
			iNameKanaBank.Text	= kana;
		}

		void fetchShiten()
		{
			t_bank_code si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCode.Text), Cast.Int(iShitenCode.Text));

			string name = "";
			string kana = "";
			if (si != null)
			{
				name = si.BCD_NameShiten;
				kana = si.BCD_NameShitenFurigana;
			}

			iNameShiten.Text		= name;
			iNameKanaShiten.Text	= kana;
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

			fetchBank();
			fetchShiten();
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
					gctl.UpdateByControl(((UcDate)this.ActiveControl).UcDateCtl);
				}
				else
				if (this.ActiveControl is UcTableComboBox)
				{
					gctl.UpdateByControl(((UcTableComboBox)this.ActiveControl).ComboBox);
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
			appFuncKey = new AppFunctionKey(funckey, FuncMasterBank_DlgEntry.Functions);
;
			FuncMasterBank_DlgEntry.ShowBankCode.Execute = showBankCode;
			FuncMasterBank_DlgEntry.Save.Execute		= saveClose;
			FuncMasterBank_DlgEntry.Cancel.Execute		= closeCancel;
		}

		void showBankCode()
		{
			t_bank xrow = new t_bank(editrow);

			FormSelectorBankCode frm = new FormSelectorBankCode();
			frm.SelectedBankCode = AppGlobal.BankCodeMg.GetBankCodeRow(xrow.BNK_Code, xrow.BNK_CodeShiten);
			frm.ShowDialog();
			if (frm.FormCloseReason == FormCloseReason.Exec)
			{				
				xrow.BNK_Code		= frm.SelectedBankCode.BCD_Code;
				xrow.BNK_CodeShiten = frm.SelectedBankCode.BCD_CodeShiten;

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
				iBankCode,	iShitenCode, iNameBank, iNameShiten,
				iKozaNo,	iCodeCompany,
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
