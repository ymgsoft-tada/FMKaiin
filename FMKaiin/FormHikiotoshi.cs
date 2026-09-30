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
	/// [作成者 kj]
	/// 引落データの一覧画面
	/// </summary>
	public partial class FormHikiotoshi : FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormHikiotoshi()
		{
			InitializeComponent();
		}

		protected override void FormFrame_Load(object sender, EventArgs e)
		{
			base.FormFrame_Load(sender, e);
		}

		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// 一か月前を初期値としておく
			DateTime dt = DateTime.Now.AddMonths(-1);

			iDateYM.UcValue = new DateTime(dt.Year, dt.Month,1);

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncHikiotoshi.Functions);

			FuncHikiotoshi.Exec.Execute = doExec;
			FuncHikiotoshi.Close.Execute = formClose;
		}

		void doExec()
		{
			if (iDateYM.UcValue == null)
			{
				iDateYM.Select();
				appToolTip.Show(iDateYM, AppToolTipIndex.CannotUseBlank);
			}
			else
			{
			}
		}

		void formClose()
		{
			this.Close();
		}
	}
}
