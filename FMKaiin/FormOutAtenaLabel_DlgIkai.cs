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
	/// 宛名ラベル 出力する参加医会の選択ダイアログ
	/// </summary>
	public partial class FormOutAtenaLabel_DlgIkai : FormFrame
	{
		const int ColCount = 3;
		const int ColWidth = 250;
		const int RowHeight = 28;

		//¶ 2026/11/28 CC チェックボックスと会費属性の対応
		Dictionary<YControlYcCheckBoxEx.YcCheckBoxEx, int> chks = new Dictionary<YControlYcCheckBoxEx.YcCheckBoxEx, int>();

		/// <summary>
		/// 出力する参加医会（会費属性）。決定で閉じたときに更新される
		/// </summary>
		public List<int> Selected { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="selected">出力する参加医会（会費属性）</param>
		public FormOutAtenaLabel_DlgIkai(List<int> selected)
		{
			Selected = new List<int>(selected);
			InitializeComponent();
		}

		/// <summary>
		/// 選択できる参加医会（会費属性）を番号順に取得する
		/// </summary>
		public static List<KeyValuePair<int, string>> GetIkaiList()
		{
			//¶ 2026/11/28 CC 医師会（日医・県医・市医）は参加医会として扱わない
			int[] ignores = { (int)eKaihiZokusei.None, (int)eKaihiZokusei.Nichii, (int)eKaihiZokusei.Keni, (int)eKaihiZokusei.Shii };
			return enumKbn.DKaihiZokusei
				.Where(x => ignores.Contains(x.Key) == false)
				.OrderBy(x => x.Key)
				.ToList();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			createCheckBoxes();

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// 参加医会のチェックボックスを会費属性の番号順に並べる
		/// </summary>
		void createCheckBoxes()
		{
			List<KeyValuePair<int, string>> ikais = GetIkaiList();
			//¶ 2026/11/28 CC 上から下、左の列から右の列の順に並べる
			int rows = (ikais.Count + ColCount - 1) / ColCount;
			pnlItems.SuspendLayout();
			for (int i = 0; i < ikais.Count; i++)
			{
				YControlYcCheckBoxEx.YcCheckBoxEx chk = new YControlYcCheckBoxEx.YcCheckBoxEx();
				chk.AutoSize = true;
				chk.DisabledBackColor = SystemColors.Control;
				chk.Position = YControlYcCheckBoxEx.YcCheckBoxEx.CheckBoxExPosition.Normal;
				chk.SingleBorderColor = Color.DimGray;
				chk.UseVisualStyleBackColor = true;
				chk.Location = new Point(16 + ColWidth * (i / rows), 8 + RowHeight * (i % rows));
				chk.TabIndex = i;
				chk.Text = ikais[i].Value;
				chk.Checked = Selected.Contains(ikais[i].Key);
				pnlItems.Controls.Add(chk);
				chks.Add(chk, ikais[i].Key);
			}
			pnlItems.ResumeLayout();
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			funcKeys = new FunctionKeyBinder(funckey, FuncOutAtenaLabel_DlgIkai.Functions);

			funcKeys.On(FuncOutAtenaLabel_DlgIkai.SelectAll, () => setChecked(true));
			funcKeys.On(FuncOutAtenaLabel_DlgIkai.ClearAll, () => setChecked(false));
			funcKeys.On(FuncOutAtenaLabel_DlgIkai.Save, okClose);
			funcKeys.On(FuncOutAtenaLabel_DlgIkai.Cancel, formClose);
		}

		/// <summary>
		/// チェックをまとめて切り替える
		/// </summary>
		void setChecked(bool value)
		{
			foreach (YControlYcCheckBoxEx.YcCheckBoxEx chk in chks.Keys)
			{
				chk.Checked = value;
			}
		}

		/// <summary>
		/// 決定して閉じる
		/// </summary>
		void okClose()
		{
			Selected = chks.Where(x => x.Key.Checked == true).Select(x => x.Value).ToList();
			formCloseReason = FormCloseReason.Save;
			this.Close();
		}

		/// <summary>
		/// キャンセル
		/// </summary>
		void formClose()
		{
			this.Close();
		}
	}
}
