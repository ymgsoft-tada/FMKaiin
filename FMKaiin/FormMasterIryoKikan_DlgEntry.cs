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
			AppCombo.SetComboBox(iByosho, enumKbn.DByosho);
			AppCombo.SetComboBox(iTaikai, enumKbn.DTaikai);

			gctl = new GControlDB(this, getDataRow, AppGlobal.DB.DBZipCode);

			gctl.Add(new GControlDBText(t_iryokikan.FIRK_Code, icode));
			gctl.Add(new GControlDBRuby(
							new string[] {
								t_iryokikan.FIRK_Name,
								t_iryokikan.FIRK_Kana},
							new Control[] {
								iShisetsuName,
								iShisetsuKana
							}));
			gctl.Add(new GControlDBText(t_iryokikan.FIRK_Tsusho, iShisetsuTsusho));

			gctl.Add(new GControlDBPostAddr(
							new string[]{
								t_iryokikan.FIRK_Post,
								t_iryokikan.FIRK_Add1
							},
							new Control[]{
								iPost1,
								iPost2,
								iAddr1
							}));
			gctl.Add(new GControlDBText(t_iryokikan.FIRK_Add2, iAddr2));
			gctl.Add(new GControlDBHyphenSplit(t_iryokikan.FIRK_Tel1, new Control[] { iTel1_1, iTel1_2, iTel1_3 }));
			gctl.Add(new GControlDBHyphenSplit(t_iryokikan.FIRK_Tes2, new Control[] { iTel2_1, iTel2_2, iTel2_3 }));
			gctl.Add(new GControlDBText(t_iryokikan.FIRK_KaisetsuShutai, iKaisetsushutai));

			gctl.Add(new GControlDBCombo(t_iryokikan.FIRK_ByoshoUmu, iByosho));

			gctl.Add(new GControlDBText(t_iryokikan.FIRK_Kyoka, iKyokabyosho));
			gctl.Add(new GControlDBCheckBox(t_iryokikan.FIRK_Kaigo, chkKaigo));
			gctl.Add(new GControlDBCheckBox(t_iryokikan.FIRK_Etc, chkEtc));
			gctl.Add(new GControlDBText(t_iryokikan.FIRK_Memo, iHeisetsu));
			gctl.Add(new GControlDBText(t_iryokikan.FIRK_KumiCode, iKumiCode));

			gctl.Add(new GControlDBCombo(t_iryokikan.FIRK_TaikaiKbn, iTaikai));

			gctl.EndAdd(AppDbRule.Rule);

			rowFetch();

			icode.Select();

			base.FormFrame_Shown(sender, e);
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
			formCloseReason = FormCloseReason.Save;
			this.Close();
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
	}
}
