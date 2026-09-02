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
	/// [作成者 tanaka]
	/// 銀行コードマスタ登録画面
	/// </summary>
	public partial class FormMasterBankCode_DlgEntry : FormFrame
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
					editrow.ItemArray = srcrow.ItemArray;
//					AppDb.CopyDataRow(srcrow, editrow);
				}
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMasterBankCode_DlgEntry()
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
			gctl = new GControlDB(this, getDataRow);

			gctl.Add(new GControlDBText(t_bank_code.FBCD_Code, iBankCode));
			gctl.Add(new GControlDBText(t_bank_code.FBCD_CodeShiten, iShitenCode));

			gctl.Add(new GControlDBRuby(
							new string[]{
								t_bank_code.FBCD_Name,
								t_bank_code.FBCD_NameFurigana,
							},
							new Control[]{
								iNameBank,
								iNameKanaBank,
							}));

			gctl.Add(new GControlDBRuby(
							new string[]{
								t_bank_code.FBCD_NameShiten,
								t_bank_code.FBCD_NameShitenFurigana,
							},
							new Control[]{
								iNameShiten,
								iNameKanaShiten,
							}));
			gctl.Add(new GControlDBText(t_bank_code.FBCD_FullName, iFullNameBank));

			gctl.EndAdd(AppDbRule.Rule);

			rowFetch();

			funckey.Select();
			iBankCode.Select();

			base.FormFrame_Shown(sender, e);
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
			appFuncKey = new AppFunctionKey(funckey, FuncMasterBankCode_DlgEntry.Functions);
;
			FuncMasterBankCode_DlgEntry.Save.Execute		= saveClose;
			FuncMasterBankCode_DlgEntry.Cancel.Execute		= closeCancel;
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
				iBankCode,	iShitenCode, iNameBank, iNameShiten, iNameKanaBank, iNameKanaShiten, iFullNameBank
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
			BankCode obj = AppGlobal.BankCodeMg.GetBankCode(iBankCode.Text);

			if (obj != null)
			{
				t_bank_code xrow = obj.GetShiten(iShitenCode.Text);

				if (xrow != null && xrow.ID_Auto != Cast.Int(editrow[t_bank_code.FID_Auto]))
				{
					iShitenCode.Select();
					appToolTip.Show(iShitenCode, AppToolTipIndex.BookingCode);
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
