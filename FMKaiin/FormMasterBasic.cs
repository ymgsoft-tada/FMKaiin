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
using GControlGcTextBoxEx;

namespace App
{
	/// <summary>
	/// 基本情報
	/// tachi
	/// </summary>
	public partial class FormMasterBasic:FormFrame
	{

		GControlDB gctl;
		DBView dvBasic;

		public FormMasterBasic()
		{
			InitializeComponent();
		}

		/// <summary>
		/// フォームを閉じる
		/// </summary>
		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (formCloseReason == FormCloseReason.Save)
			{
				if (dvBasic.HasChanges() == true)
				{
					AppGlobal.DB.UpdateTable(TableProp.t_basic);

					// 共通クラスの初期化
					AppGlobal.InitBasic();
				}
			}

			base.FormFrame_FormClosing(sender, e);
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			dvBasic = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_basic));

			gctl = new GControlDB(this, getDataRow, AppGlobal.DB.DBZipCode);

			gctl.Add(new GControlDBRuby(
							new string[]{
								t_basic.FBAS_Name,
								t_basic.FBAS_NameFurigana,
							},
							new Control[]{
								iName,
								iNameFurigana,
							}));


			gctl.Add(new GControlDBText(t_basic.FBAS_NameDaihyo, iNameDaihyo));



			gctl.Add(new GControlDBPostAddr(
							new string[]{
								t_basic.FBAS_Post,
								t_basic.FBAS_Addr1
							},
							new Control[]{
								iPost1,
								iPost2,
								iAddr1
							}));

			gctl.Add(new GControlDBText(t_basic.FBAS_Addr2, iAddr2));
			gctl.Add(new GControlDBHyphenSplit(t_basic.FBAS_Tel1, new Control[] { iTel1_1, iTel1_2, iTel1_3 }));
			gctl.Add(new GControlDBHyphenSplit(t_basic.FBAS_Tel2, new Control[] { iTel2_1, iTel2_2, iTel2_3 }));
			gctl.Add(new GControlDBHyphenSplit(t_basic.FBAS_Fax, new Control[] { iTel2_1, iTel2_2, iTel2_3 }));
			gctl.Add(new GControlDBText(t_basic.FBAS_HojinNo, iHojinNo));

			gctl.EndAdd(AppDbRule.Rule);

			rowFetch();

			funckey.Select();
			iName.Select();

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncMasterBasic.Functions);

			FuncMasterBasic.Save.Execute = saveClose;
			FuncMasterBasic.Cancel.Execute = cancelClose;

		}

		/// <summary>
		/// レコードの検証
		/// </summary>
		/// <returns></returns>
		bool validateRow()
		{
			updateCurrentControlValue(gctl);

			t_basic xrow = new t_basic(dvBasic[0]);

			// 空白チェック
			GcTextBoxEx[] ctls =
			{
				iName,iNameFurigana
			};

			foreach (GcTextBoxEx ctl in ctls)
			{
				if (ctl.TextLength == 0)
				{
					ctl.Select();
					appToolTip.Show(ctl, AppToolTipIndex.CannotUseBlank);
					return false;
				}
			}

			if (xrow.BAS_Name == null)
			{
				iName.Select();
				appToolTip.Show(iName, AppToolTipIndex.CannotUseBlank);
				return false;
			}

			return true;
		}

		/// <summary>
		/// 登録処理
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
		void cancelClose()
		{
			this.Close();
		}

		/// <summary>
		/// 行情報のフェッチ
		/// </summary>
		void rowFetch()
		{
			if (gctl != null)
			{
				gctl.FetchAll();
			}
		}

		/// <summary>
		/// 行情報の取得
		/// </summary>
		/// <param name="tag"></param>
		DataRow getDataRow(string tag)
		{
			return dvBasic[0].Row;
		}
	}
}
