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
	/// 在籍期間
	/// </summary>
	public partial class FormKaiin_DlgHistory : FormFrame
	{
		/// <summary>
		/// 会員情報
		/// </summary>
		public Kaiin SelectKaiin { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormKaiin_DlgHistory()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncKaiin_DlgHistory.Functions);

			FuncKaiin_DlgHistory.Close.Execute = formClose;
		}

		void formClose()
		{
		}
	}
}
