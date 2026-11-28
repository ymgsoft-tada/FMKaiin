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
	/// MAMISデータ取込（取込設定）
	/// MAMISのデータで更新する項目を選択します。設定は基本情報に保存し、次回の取込でも使います。
	/// </summary>
	public partial class FormMamisImport_DlgItems : FormFrame
	{
		/// <summary>1行に並べるチェックの数</summary>
		const int ColCount = 3;
		/// <summary>チェック1つ分の幅</summary>
		const int ColWidth = 250;
		/// <summary>チェック1行分の高さ</summary>
		const int RowHeight = 28;

		/// <summary>
		/// チェックボックスと取込設定の項目の対応
		/// </summary>
		Dictionary<YControlYcCheckBoxEx.YcCheckBoxEx, AppMamisImport.ItemGroup> chks = new Dictionary<YControlYcCheckBoxEx.YcCheckBoxEx, AppMamisImport.ItemGroup>();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMamisImport_DlgItems()
		{
			InitializeComponent();
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
		/// 取込設定の項目の一覧（AppMamisImport.ItemGroups）から、チェックボックスを作成します。
		/// 会員情報入力画面の場所（基本情報・情報1・情報3）ごとに枠で囲みます。
		/// </summary>
		void createCheckBoxes()
		{
			List<string> offs = AppMamisImport.GetOffKeys();

			int top = 0;
			int tab = 0;

			pnlItems.SuspendLayout();

			foreach (string category in AppMamisImport.ItemGroups.Select(x => x.Category).Distinct())
			{
				AppMamisImport.ItemGroup[] grps = AppMamisImport.ItemGroups.Where(x => x.Category == category).ToArray();
				int rows = (grps.Length + ColCount - 1) / ColCount;

				GroupBox box = new GroupBox();
				box.Text = category;
				box.Location = new Point(0, top);
				box.Size = new Size(pnlItems.Width, RowHeight * rows + 40);

				for (int i = 0; i < grps.Length; i++)
				{
					AppMamisImport.ItemGroup grp = grps[i];

					YControlYcCheckBoxEx.YcCheckBoxEx chk = new YControlYcCheckBoxEx.YcCheckBoxEx();
					chk.AutoSize = true;
					chk.DisabledBackColor = SystemColors.Control;
					chk.Position = YControlYcCheckBoxEx.YcCheckBoxEx.CheckBoxExPosition.Normal;
					chk.SingleBorderColor = Color.DimGray;
					chk.UseVisualStyleBackColor = true;
					chk.Location = new Point(16 + ColWidth * (i % ColCount), 26 + RowHeight * (i / ColCount));
					chk.TabIndex = tab++;
					chk.Text = grp.Name;
					chk.Checked = (offs.Contains(grp.Key) == false);

					//¶ 2026/11/28 CC 常に取り込む項目は、チェックを付けたまま変更できないようにする
					if (grp.Fixed == true)
					{
						chk.Text = $"{grp.Name}（常に取込）";
						chk.Checked = true;
						chk.Enabled = false;
					}

					box.Controls.Add(chk);
					chks.Add(chk, grp);
				}

				pnlItems.Controls.Add(box);
				top += box.Height + 8;
			}

			pnlItems.ResumeLayout();
		}

		/// <summary>
		/// ファンクションの登録
		/// </summary>
		protected override void SetFunction()
		{
			funcKeys = new FunctionKeyBinder(funckey, FuncMamisImport_DlgItems.Functions);

			funcKeys.On(FuncMamisImport_DlgItems.SelectAll, selectAll);
			funcKeys.On(FuncMamisImport_DlgItems.Save, saveClose);
			funcKeys.On(FuncMamisImport_DlgItems.Cancel, formClose);
		}

		/// <summary>
		/// 全て選択（初期状態に戻す）
		/// </summary>
		void selectAll()
		{
			foreach (YControlYcCheckBoxEx.YcCheckBoxEx chk in chks.Keys)
			{
				chk.Checked = true;
			}
		}

		/// <summary>
		/// 登録
		/// </summary>
		void saveClose()
		{
			//¶ 2026/11/28 CC 保存するのは「取り込まない項目」。全てのチェックを外しても、常に取り込む項目は残る
			List<string> offs = new List<string>();

			foreach (KeyValuePair<YControlYcCheckBoxEx.YcCheckBoxEx, AppMamisImport.ItemGroup> pair in chks)
			{
				if (pair.Value.Fixed == false && pair.Key.Checked == false)
				{
					offs.Add(pair.Value.Key);
				}
			}

			if (AppMamisImport.SaveOffKeys(offs) == false)
			{
				AppMsgBox.Show(this, AppMsgBoxIndex.AnythingMessage, "取込設定を保存できませんでした。");
				return;
			}

			formCloseReason = FormCloseReason.Save;
			this.Close();
		}

		void formClose()
		{
			this.Close();
		}
	}
}
