using ComponentDB;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentSUI;

namespace App
{
	/// <summary>
	/// [作成者 kj]
	/// 会費申請書類
	/// </summary>
	public partial class FormOutExcelKaihiShinsei : FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormOutExcelKaihiShinsei()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin));
			AppTableCombo.SetComboBox_Kaiin(iKaiinFrom, dv);

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			funcKeys = new FunctionKeyBinder(funckey, FuncOutExcelKaihiShinsei.Functions);

			rdoNyukai.Checked = true;

			funcKeys.On(FuncOutExcelKaihiShinsei.OutExcel, doExec);
			funcKeys.On(FuncOutExcelKaihiShinsei.Close, formClose);
		}

		/// <summary>
		/// 選択されている書類タイプを取得します。
		/// </summary>
		AppExcelKaihiShinsei.eType getSelectedType()
		{
			if (rdoNyukaiKenshui.Checked == true)
			{
				return AppExcelKaihiShinsei.eType.NyukaiKenshui;
			}
			if (rdoHenko.Checked == true)
			{
				return AppExcelKaihiShinsei.eType.Henko;
			}
			if (rdoHenkoKorei.Checked == true)
			{
				return AppExcelKaihiShinsei.eType.HenkoKorei;
			}
			if (rdoHenko4joC.Checked == true)
			{
				return AppExcelKaihiShinsei.eType.Henko4joC;
			}

			return AppExcelKaihiShinsei.eType.Nyukai;
		}

		void doExec()
		{
			//¶ 2026/11/16 CC 会員の指定は必須
			Kaiin ka = AppGlobal.Kaiins.Get(Cast.Int(iKaiinFrom.Value));
			if (ka == null)
			{
				appToolTip.Show(iKaiinFrom, AppToolTipIndex.CannotUseBlank);
				iKaiinFrom.Focus();
				return;
			}

			AppExcelKaihiShinsei xls = new AppExcelKaihiShinsei(getSelectedType(), ka);

			switch (xls.Execute())
			{
				case AppExcel.eResult.Success:
					AppMsgBox.Show(this, AppMsgBoxIndex.CreateExcelFile);
					break;
				case AppExcel.eResult.Cancel:
					break;
				case AppExcel.eResult.NotExistTemplate:
					AppMsgBox.Show(this, AppMsgBoxIndex.NotExistFile);
					break;
				case AppExcel.eResult.NotFoundData:
					AppMsgBox.Show(this, AppMsgBoxIndex.NotFoundDataExport);
					break;
				default:
					AppMsgBox.Show(this, AppMsgBoxIndex.FailedExcelFile);
					break;
			}
		}

		void formClose()
		{
			this.Close();
		}
	}
}
