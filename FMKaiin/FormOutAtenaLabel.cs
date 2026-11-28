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
using ComponentIO;
using GControlGcTextBoxEx;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// 宛名ラベル 印刷条件画面
	/// </summary>
	public partial class FormOutAtenaLabel : FormFrame
	{
		//¶ 2026/11/28 CC 出力する参加医会（会費属性）。初期状態はすべて出力
		List<int> ikaiSelected = new List<int>();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormOutAtenaLabel()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// コードの入力ルール
			AppDbRule.SetControlByRule(iKaiinFrom, t_kaiin.FCD_Kaiin);
			AppDbRule.SetControlByRule(iKaiinTo, t_kaiin.FCD_Kaiin);
			AppDbRule.SetControlByRule(iIryoFrom, t_iryokikan.FIRK_Code);
			AppDbRule.SetControlByRule(iIryoTo, t_iryokikan.FIRK_Code);

			// コンボの作成
			// 会員区分
			AppCombo.SetComboBox(iKaiinKbn, enumKbn.DTypeKaiin, true, (int)eTypeKaiin.None);
			iKaiinKbn.ExSetSelectedIndexByValue(AppCombo.SelectAllValue);

			// 参加医会（初期状態はすべて出力）
			ikaiSelected = FormOutAtenaLabel_DlgIkai.GetIkaiList().Select(x => x.Key).ToList();
			showIkaiInfo();

			// 初期値
			chkZaiseki.Checked = true;
			chkTaikaiNozoku.Checked = true;
			rdoKaiin.Checked = true;
			changeDest();

			//■ イベントの登録
			rdoKaiin.CheckedChanged += (s, ev) => changeDest();
			rdoIkai.CheckedChanged += (s, ev) => changeDest();
			rdoIryo.CheckedChanged += (s, ev) => changeDest();
			btnIkaiSelect.Click += (s, ev) => selectIkai();

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// 出力する参加医会を選択ダイアログで選ぶ
		/// </summary>
		void selectIkai()
		{
			FormOutAtenaLabel_DlgIkai frm = new FormOutAtenaLabel_DlgIkai(ikaiSelected);
			frm.ShowDialog();
			if (frm.FormCloseReason == FormCloseReason.Save)
			{
				ikaiSelected = frm.Selected;
			}
			frm.Dispose();
			frm = null;

			showIkaiInfo();
		}

		/// <summary>
		/// 出力する参加医会の件数と、除外した参加医会の名前を表示する
		/// </summary>
		void showIkaiInfo()
		{
			List<KeyValuePair<int, string>> ikais = FormOutAtenaLabel_DlgIkai.GetIkaiList();
			string[] offs = ikais.Where(x => ikaiSelected.Contains(x.Key) == false).Select(x => x.Value).ToArray();
			if (offs.Length == 0)
			{
				lblIkaiInfo.Text = $"{ikais.Count}件すべてを出力";
			}
			else if (offs.Length == ikais.Count)
			{
				lblIkaiInfo.Text = "出力する参加医会がありません";
			}
			else
			{
				lblIkaiInfo.Text = $"{ikais.Count}件中 {ikais.Count - offs.Length}件を出力（除外: {string.Join("、", offs)}）";
			}
		}

		/// <summary>
		/// 宛先の切り替え
		/// </summary>
		void changeDest()
		{
			//¶ 2026/11/28 CC 画面の大きさは変えない。会員と医療機関の条件は同じ位置で入れ替え、
			//¶ 2026/11/28 CC 参加医会の行は宛先が参加医会のときだけ表示する（一覧は別ダイアログ）
			bool isIryo = (rdoIryo.Checked == true);
			bool isIkai = (rdoIkai.Checked == true);
			pnlKaiin.Visible = (isIryo == false);
			pnlIryo.Visible = isIryo;
			lblIkai.Visible = isIkai;
			btnIkaiSelect.Visible = isIkai;
			lblIkaiInfo.Visible = isIkai;
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			funcKeys = new FunctionKeyBinder(funckey, FuncOutAtenaLabel.Functions);

			funcKeys.On(FuncOutAtenaLabel.Preview, preview);
			funcKeys.On(FuncOutAtenaLabel.Close, formClose);
		}

		/// <summary>
		/// コード欄の値を取得する。空欄は null
		/// </summary>
		int? getCode(GcTextBoxEx ctl)
		{
			string text = ctl.Text.Trim();
			int val;
			if (text.Length == 0 || int.TryParse(text, out val) == false)
			{
				return null;
			}
			return val;
		}

		/// <summary>
		/// 画面の内容から出力条件を作成する
		/// </summary>
		ReportFlexAtenaLabel.Joken createJoken()
		{
			ReportFlexAtenaLabel.Joken joken = new ReportFlexAtenaLabel.Joken();
			if (rdoIryo.Checked == true)
			{
				joken.Dest = ReportFlexAtenaLabel.eDest.IryoKikan;
			}
			else if (rdoIkai.Checked == true)
			{
				joken.Dest = ReportFlexAtenaLabel.eDest.Ikai;
			}
			else
			{
				joken.Dest = ReportFlexAtenaLabel.eDest.Kaiin;
			}

			// 会員の条件
			joken.KaiinCdFrom = getCode(iKaiinFrom);
			joken.KaiinCdTo = getCode(iKaiinTo);
			if (iKaiinKbn.SelectedIndex >= 0)
			{
				int kbn = Cast.Int(iKaiinKbn.ExGetValue(iKaiinKbn.SelectedIndex));
				if (kbn != AppCombo.SelectAllValue)
				{
					joken.KaiinKbn = kbn;
				}
			}
			if (chkZaiseki.Checked == true)
			{
				joken.Zaiseki.Add((int)eTypeZaiseki.Zaiseki);
			}
			if (chkIdo.Checked == true)
			{
				joken.Zaiseki.Add((int)eTypeZaiseki.Ido);
			}
			if (chkTaikai.Checked == true)
			{
				joken.Zaiseki.Add((int)eTypeZaiseki.Taikai);
			}

			// 参加医会の条件
			joken.Zokusei.AddRange(ikaiSelected);

			// 医療機関の条件
			joken.IryoCdFrom = getCode(iIryoFrom);
			joken.IryoCdTo = getCode(iIryoTo);
			joken.ExceptTaikai = chkTaikaiNozoku.Checked;

			// 印刷の設定
			joken.StartRow = (int)numRow.Value;
			joken.StartCol = (int)numCol.Value;
			joken.Copies = (int)numCopy.Value;
			return joken;
		}

		/// <summary>
		/// プレビュー
		/// </summary>
		void preview()
		{
			ReportFlexAtenaLabel rep = new ReportFlexAtenaLabel();
			rep.CreateView(createJoken());
			rep.ReportExec();
			rep = null;
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		void formClose()
		{
			this.Close();
		}
	}
}
