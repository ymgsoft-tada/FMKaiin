using ComponentDB;
using ComponentDebug;
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

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// iFAXデータ（BizFAX 宛先リスト）作成
	/// </summary>
	public partial class FormCreateIfaxFile : FormFrame
	{
		/// <summary>
		/// iFAXグループ
		/// </summary>
		DBView dvIfax;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormCreateIfaxFile()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			dvIfax = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_ifax));
			AppTableCombo.SetComboBox_IfaxGroup(iIfax, dvIfax); //¶ 2026/11/11 CC iFAXグループ

			iIfax.Select();

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの登録
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncCreateIfaxFile.Functions);

			FuncCreateIfaxFile.Exec.Execute = doExec;
			FuncCreateIfaxFile.Close.Execute = formClose;
		}

		void doExec()
		{
			//¶ 2026/11/11 CC iFAXグループ：未指定の場合は全グループを出力
			int? id_ifax = null;

			if (iIfax.Text != "")
			{
				id_ifax = Cast.Int(iIfax.Value);
			}

			AppIfaxFile aif = new AppIfaxFile();
			aif.ShowSaveFilePath();

			//¶ 2026/11/11 CC 実行処理
			AppIfaxFile.eResut ret = aif.Create(id_ifax);

			switch(ret)
			{
				case AppIfaxFile.eResut.DataNotFound :
				case AppIfaxFile.eResut.Success :
				case AppIfaxFile.eResut.CannotFileSave :
					//¶ 2026/11/11 CC 結果画面を表示
					FormLogView frm = new FormLogView();
					frm.SelectedYM = AppDate.ParseDateFirstDay(DateTime.Now); //¶ 2026/11/11 CC 年月の指定はないため、システム日付の年月
					frm.Table = aif.Logs.GetTable();
					frm.TitleText = "iFAXデータ作成結果";
					frm.ShowDialog();
					break;
			}
		}

		void formClose()
		{
			this.Close();
		}
	}
}
