
namespace App
{
	partial class FormOutAtenaLabel
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && ( components != null ))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.ycLabelEx3 = new YControlLabelEx.YcLabelEx();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.lblDest = new YControlLabelEx.YcLabelEx();
			this.rdoKaiin = new System.Windows.Forms.RadioButton();
			this.rdoIkai = new System.Windows.Forms.RadioButton();
			this.rdoIryo = new System.Windows.Forms.RadioButton();
			this.lblKaiinCd = new YControlLabelEx.YcLabelEx();
			this.iKaiinFrom = new GControlGcTextBoxEx.GcTextBoxEx();
			this.lblKaiinKara = new System.Windows.Forms.Label();
			this.iKaiinTo = new GControlGcTextBoxEx.GcTextBoxEx();
			this.lblKaiinKbn = new YControlLabelEx.YcLabelEx();
			this.iKaiinKbn = new GControlGcComboBoxEx.GcComboBoxEx(this.components);
			this.lblZaiseki = new YControlLabelEx.YcLabelEx();
			this.chkZaiseki = new YControlYcCheckBoxEx.YcCheckBoxEx();
			this.chkIdo = new YControlYcCheckBoxEx.YcCheckBoxEx();
			this.chkTaikai = new YControlYcCheckBoxEx.YcCheckBoxEx();
			this.lblIryoCd = new YControlLabelEx.YcLabelEx();
			this.iIryoFrom = new GControlGcTextBoxEx.GcTextBoxEx();
			this.lblIryoKara = new System.Windows.Forms.Label();
			this.iIryoTo = new GControlGcTextBoxEx.GcTextBoxEx();
			this.lblIryoTaikai = new YControlLabelEx.YcLabelEx();
			this.chkTaikaiNozoku = new YControlYcCheckBoxEx.YcCheckBoxEx();
			this.pnlKaiin = new System.Windows.Forms.Panel();
			this.pnlIryo = new System.Windows.Forms.Panel();
			this.lblIkai = new YControlLabelEx.YcLabelEx();
			this.lblIkaiInfo = new System.Windows.Forms.Label();
			this.btnIkaiSelect = new System.Windows.Forms.Button();
			this.lblPos = new YControlLabelEx.YcLabelEx();
			this.numRow = new System.Windows.Forms.NumericUpDown();
			this.lblRow = new System.Windows.Forms.Label();
			this.numCol = new System.Windows.Forms.NumericUpDown();
			this.lblCol = new System.Windows.Forms.Label();
			this.lblCopy = new YControlLabelEx.YcLabelEx();
			this.numCopy = new System.Windows.Forms.NumericUpDown();
			this.lblMai = new System.Windows.Forms.Label();
			this.funckey = new App.Controls.FunctionKeyBar();
			((System.ComponentModel.ISupportInitialize)(this.iKaiinFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iKaiinTo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iKaiinKbn)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iIryoFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iIryoTo)).BeginInit();
			this.pnlKaiin.SuspendLayout();
			this.pnlIryo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numCol)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numCopy)).BeginInit();
			this.SuspendLayout();
			// 
			// ycLabelEx3
			// 
			this.ycLabelEx3.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx3.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx3.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx3.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.IconImage = null;
			this.ycLabelEx3.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx3.Location = new System.Drawing.Point(23, 20);
			this.ycLabelEx3.Name = "ycLabelEx3";
			this.ycLabelEx3.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.Size = new System.Drawing.Size(10, 29);
			this.ycLabelEx3.TabIndex = 427;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor2 = System.Drawing.Color.Empty;
			this.lblTitle.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblTitle.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblTitle.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblTitle.IconImage = null;
			this.lblTitle.Location = new System.Drawing.Point(43, 24);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(70, 23);
			this.lblTitle.TabIndex = 428;
			this.lblTitle.Text = "印刷条件";
			// 
			// lblDest
			// 
			this.lblDest.BackColor = System.Drawing.Color.SteelBlue;
			this.lblDest.BackColor2 = System.Drawing.Color.Empty;
			this.lblDest.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblDest.ForeColor = System.Drawing.Color.White;
			this.lblDest.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblDest.IconImage = null;
			this.lblDest.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.lblDest.Location = new System.Drawing.Point(43, 70);
			this.lblDest.Name = "lblDest";
			this.lblDest.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblDest.Size = new System.Drawing.Size(110, 25);
			this.lblDest.TabIndex = 401;
			this.lblDest.Text = "宛先";
			this.lblDest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// rdoKaiin
			// 
			this.rdoKaiin.AutoSize = true;
			this.rdoKaiin.Location = new System.Drawing.Point(164, 71);
			this.rdoKaiin.Name = "rdoKaiin";
			this.rdoKaiin.Size = new System.Drawing.Size(53, 24);
			this.rdoKaiin.TabIndex = 2;
			this.rdoKaiin.TabStop = true;
			this.rdoKaiin.Text = "会員";
			this.rdoKaiin.UseVisualStyleBackColor = true;
			// 
			// rdoIkai
			// 
			this.rdoIkai.AutoSize = true;
			this.rdoIkai.Location = new System.Drawing.Point(244, 71);
			this.rdoIkai.Name = "rdoIkai";
			this.rdoIkai.Size = new System.Drawing.Size(79, 24);
			this.rdoIkai.TabIndex = 3;
			this.rdoIkai.TabStop = true;
			this.rdoIkai.Text = "参加医会";
			this.rdoIkai.UseVisualStyleBackColor = true;
			// 
			// rdoIryo
			// 
			this.rdoIryo.AutoSize = true;
			this.rdoIryo.Location = new System.Drawing.Point(354, 71);
			this.rdoIryo.Name = "rdoIryo";
			this.rdoIryo.Size = new System.Drawing.Size(79, 24);
			this.rdoIryo.TabIndex = 4;
			this.rdoIryo.TabStop = true;
			this.rdoIryo.Text = "医療機関";
			this.rdoIryo.UseVisualStyleBackColor = true;
			// 
			// lblKaiinCd
			// 
			this.lblKaiinCd.BackColor = System.Drawing.Color.SteelBlue;
			this.lblKaiinCd.BackColor2 = System.Drawing.Color.Empty;
			this.lblKaiinCd.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblKaiinCd.ForeColor = System.Drawing.Color.White;
			this.lblKaiinCd.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblKaiinCd.IconImage = null;
			this.lblKaiinCd.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.lblKaiinCd.Location = new System.Drawing.Point(0, 0);
			this.lblKaiinCd.Name = "lblKaiinCd";
			this.lblKaiinCd.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblKaiinCd.Size = new System.Drawing.Size(110, 25);
			this.lblKaiinCd.TabIndex = 405;
			this.lblKaiinCd.Text = "会員コード";
			this.lblKaiinCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iKaiinFrom
			// 
			this.iKaiinFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iKaiinFrom.ExFocusHighlight = true;
			this.iKaiinFrom.ImeStr = "";
			this.iKaiinFrom.Location = new System.Drawing.Point(121, 1);
			this.iKaiinFrom.Name = "iKaiinFrom";
			this.iKaiinFrom.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iKaiinFrom.Size = new System.Drawing.Size(88, 23);
			this.iKaiinFrom.TabIndex = 6;
			// 
			// lblKaiinKara
			// 
			this.lblKaiinKara.AutoSize = true;
			this.lblKaiinKara.Location = new System.Drawing.Point(216, 3);
			this.lblKaiinKara.Name = "lblKaiinKara";
			this.lblKaiinKara.Size = new System.Drawing.Size(22, 20);
			this.lblKaiinKara.TabIndex = 407;
			this.lblKaiinKara.Text = "～";
			// 
			// iKaiinTo
			// 
			this.iKaiinTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iKaiinTo.ExFocusHighlight = true;
			this.iKaiinTo.ImeStr = "";
			this.iKaiinTo.Location = new System.Drawing.Point(246, 1);
			this.iKaiinTo.Name = "iKaiinTo";
			this.iKaiinTo.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iKaiinTo.Size = new System.Drawing.Size(88, 23);
			this.iKaiinTo.TabIndex = 8;
			// 
			// lblKaiinKbn
			// 
			this.lblKaiinKbn.BackColor = System.Drawing.Color.SteelBlue;
			this.lblKaiinKbn.BackColor2 = System.Drawing.Color.Empty;
			this.lblKaiinKbn.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblKaiinKbn.ForeColor = System.Drawing.Color.White;
			this.lblKaiinKbn.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblKaiinKbn.IconImage = null;
			this.lblKaiinKbn.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.lblKaiinKbn.Location = new System.Drawing.Point(0, 35);
			this.lblKaiinKbn.Name = "lblKaiinKbn";
			this.lblKaiinKbn.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblKaiinKbn.Size = new System.Drawing.Size(110, 25);
			this.lblKaiinKbn.TabIndex = 409;
			this.lblKaiinKbn.Text = "会員区分";
			this.lblKaiinKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iKaiinKbn
			// 
			this.iKaiinKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iKaiinKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.iKaiinKbn.ExCompareContent = null;
			this.iKaiinKbn.ExCompareValue = null;
			this.iKaiinKbn.ExDataSource = null;
			this.iKaiinKbn.ExFocusHighlight = true;
			this.iKaiinKbn.FlatStyle = GrapeCity.Win.Editors.FlatStyleEx.Flat;
			this.iKaiinKbn.ListHeaderPane.Height = 27;
			this.iKaiinKbn.ListHeaderPane.Visible = false;
			this.iKaiinKbn.Location = new System.Drawing.Point(121, 35);
			this.iKaiinKbn.Name = "iKaiinKbn";
			this.iKaiinKbn.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iKaiinKbn.Size = new System.Drawing.Size(150, 25);
			this.iKaiinKbn.TabIndex = 10;
			// 
			// lblZaiseki
			// 
			this.lblZaiseki.BackColor = System.Drawing.Color.SteelBlue;
			this.lblZaiseki.BackColor2 = System.Drawing.Color.Empty;
			this.lblZaiseki.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblZaiseki.ForeColor = System.Drawing.Color.White;
			this.lblZaiseki.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblZaiseki.IconImage = null;
			this.lblZaiseki.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.lblZaiseki.Location = new System.Drawing.Point(0, 70);
			this.lblZaiseki.Name = "lblZaiseki";
			this.lblZaiseki.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblZaiseki.Size = new System.Drawing.Size(110, 25);
			this.lblZaiseki.TabIndex = 411;
			this.lblZaiseki.Text = "在籍区分";
			this.lblZaiseki.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// chkZaiseki
			// 
			this.chkZaiseki.AutoSize = true;
			this.chkZaiseki.DisabledBackColor = System.Drawing.SystemColors.Control;
			this.chkZaiseki.Location = new System.Drawing.Point(121, 71);
			this.chkZaiseki.Name = "chkZaiseki";
			this.chkZaiseki.Position = YControlYcCheckBoxEx.YcCheckBoxEx.CheckBoxExPosition.Normal;
			this.chkZaiseki.SingleBorderColor = System.Drawing.Color.DimGray;
			this.chkZaiseki.Size = new System.Drawing.Size(54, 24);
			this.chkZaiseki.TabIndex = 12;
			this.chkZaiseki.Text = "在籍";
			this.chkZaiseki.UseVisualStyleBackColor = true;
			// 
			// chkIdo
			// 
			this.chkIdo.AutoSize = true;
			this.chkIdo.DisabledBackColor = System.Drawing.SystemColors.Control;
			this.chkIdo.Location = new System.Drawing.Point(191, 71);
			this.chkIdo.Name = "chkIdo";
			this.chkIdo.Position = YControlYcCheckBoxEx.YcCheckBoxEx.CheckBoxExPosition.Normal;
			this.chkIdo.SingleBorderColor = System.Drawing.Color.DimGray;
			this.chkIdo.Size = new System.Drawing.Size(54, 24);
			this.chkIdo.TabIndex = 13;
			this.chkIdo.Text = "異動";
			this.chkIdo.UseVisualStyleBackColor = true;
			// 
			// chkTaikai
			// 
			this.chkTaikai.AutoSize = true;
			this.chkTaikai.DisabledBackColor = System.Drawing.SystemColors.Control;
			this.chkTaikai.Location = new System.Drawing.Point(261, 71);
			this.chkTaikai.Name = "chkTaikai";
			this.chkTaikai.Position = YControlYcCheckBoxEx.YcCheckBoxEx.CheckBoxExPosition.Normal;
			this.chkTaikai.SingleBorderColor = System.Drawing.Color.DimGray;
			this.chkTaikai.Size = new System.Drawing.Size(54, 24);
			this.chkTaikai.TabIndex = 14;
			this.chkTaikai.Text = "退会";
			this.chkTaikai.UseVisualStyleBackColor = true;
			// 
			// lblIryoCd
			// 
			this.lblIryoCd.BackColor = System.Drawing.Color.SteelBlue;
			this.lblIryoCd.BackColor2 = System.Drawing.Color.Empty;
			this.lblIryoCd.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblIryoCd.ForeColor = System.Drawing.Color.White;
			this.lblIryoCd.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblIryoCd.IconImage = null;
			this.lblIryoCd.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.lblIryoCd.Location = new System.Drawing.Point(0, 0);
			this.lblIryoCd.Name = "lblIryoCd";
			this.lblIryoCd.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblIryoCd.Size = new System.Drawing.Size(110, 25);
			this.lblIryoCd.TabIndex = 415;
			this.lblIryoCd.Text = "医療機関コード";
			this.lblIryoCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iIryoFrom
			// 
			this.iIryoFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iIryoFrom.ExFocusHighlight = true;
			this.iIryoFrom.ImeStr = "";
			this.iIryoFrom.Location = new System.Drawing.Point(121, 1);
			this.iIryoFrom.Name = "iIryoFrom";
			this.iIryoFrom.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iIryoFrom.Size = new System.Drawing.Size(88, 23);
			this.iIryoFrom.TabIndex = 16;
			// 
			// lblIryoKara
			// 
			this.lblIryoKara.AutoSize = true;
			this.lblIryoKara.Location = new System.Drawing.Point(216, 3);
			this.lblIryoKara.Name = "lblIryoKara";
			this.lblIryoKara.Size = new System.Drawing.Size(22, 20);
			this.lblIryoKara.TabIndex = 417;
			this.lblIryoKara.Text = "～";
			// 
			// iIryoTo
			// 
			this.iIryoTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iIryoTo.ExFocusHighlight = true;
			this.iIryoTo.ImeStr = "";
			this.iIryoTo.Location = new System.Drawing.Point(246, 1);
			this.iIryoTo.Name = "iIryoTo";
			this.iIryoTo.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iIryoTo.Size = new System.Drawing.Size(88, 23);
			this.iIryoTo.TabIndex = 18;
			// 
			// lblIryoTaikai
			// 
			this.lblIryoTaikai.BackColor = System.Drawing.Color.SteelBlue;
			this.lblIryoTaikai.BackColor2 = System.Drawing.Color.Empty;
			this.lblIryoTaikai.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblIryoTaikai.ForeColor = System.Drawing.Color.White;
			this.lblIryoTaikai.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblIryoTaikai.IconImage = null;
			this.lblIryoTaikai.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.lblIryoTaikai.Location = new System.Drawing.Point(0, 35);
			this.lblIryoTaikai.Name = "lblIryoTaikai";
			this.lblIryoTaikai.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblIryoTaikai.Size = new System.Drawing.Size(110, 25);
			this.lblIryoTaikai.TabIndex = 419;
			this.lblIryoTaikai.Text = "退会区分";
			this.lblIryoTaikai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// chkTaikaiNozoku
			// 
			this.chkTaikaiNozoku.AutoSize = true;
			this.chkTaikaiNozoku.DisabledBackColor = System.Drawing.SystemColors.Control;
			this.chkTaikaiNozoku.Location = new System.Drawing.Point(121, 36);
			this.chkTaikaiNozoku.Name = "chkTaikaiNozoku";
			this.chkTaikaiNozoku.Position = YControlYcCheckBoxEx.YcCheckBoxEx.CheckBoxExPosition.Normal;
			this.chkTaikaiNozoku.SingleBorderColor = System.Drawing.Color.DimGray;
			this.chkTaikaiNozoku.Size = new System.Drawing.Size(171, 24);
			this.chkTaikaiNozoku.TabIndex = 20;
			this.chkTaikaiNozoku.Text = "退会した医療機関を除く";
			this.chkTaikaiNozoku.UseVisualStyleBackColor = true;
			// 
			// pnlKaiin
			// 
			this.pnlKaiin.Controls.Add(this.lblKaiinCd);
			this.pnlKaiin.Controls.Add(this.iKaiinFrom);
			this.pnlKaiin.Controls.Add(this.lblKaiinKara);
			this.pnlKaiin.Controls.Add(this.iKaiinTo);
			this.pnlKaiin.Controls.Add(this.lblKaiinKbn);
			this.pnlKaiin.Controls.Add(this.iKaiinKbn);
			this.pnlKaiin.Controls.Add(this.lblZaiseki);
			this.pnlKaiin.Controls.Add(this.chkZaiseki);
			this.pnlKaiin.Controls.Add(this.chkIdo);
			this.pnlKaiin.Controls.Add(this.chkTaikai);
			this.pnlKaiin.Location = new System.Drawing.Point(43, 105);
			this.pnlKaiin.Name = "pnlKaiin";
			this.pnlKaiin.Size = new System.Drawing.Size(443, 100);
			this.pnlKaiin.TabIndex = 21;
			// 
			// pnlIryo
			// 
			this.pnlIryo.Controls.Add(this.lblIryoCd);
			this.pnlIryo.Controls.Add(this.iIryoFrom);
			this.pnlIryo.Controls.Add(this.lblIryoKara);
			this.pnlIryo.Controls.Add(this.iIryoTo);
			this.pnlIryo.Controls.Add(this.lblIryoTaikai);
			this.pnlIryo.Controls.Add(this.chkTaikaiNozoku);
			this.pnlIryo.Location = new System.Drawing.Point(43, 105);
			this.pnlIryo.Name = "pnlIryo";
			this.pnlIryo.Size = new System.Drawing.Size(443, 100);
			this.pnlIryo.TabIndex = 22;
			// 
			// lblIkai
			// 
			this.lblIkai.BackColor = System.Drawing.Color.SteelBlue;
			this.lblIkai.BackColor2 = System.Drawing.Color.Empty;
			this.lblIkai.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblIkai.ForeColor = System.Drawing.Color.White;
			this.lblIkai.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblIkai.IconImage = null;
			this.lblIkai.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.lblIkai.Location = new System.Drawing.Point(43, 210);
			this.lblIkai.Name = "lblIkai";
			this.lblIkai.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblIkai.Size = new System.Drawing.Size(110, 25);
			this.lblIkai.TabIndex = 423;
			this.lblIkai.Text = "参加医会";
			this.lblIkai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblIkaiInfo
			// 
			this.lblIkaiInfo.AutoEllipsis = true;
			this.lblIkaiInfo.Location = new System.Drawing.Point(254, 213);
			this.lblIkaiInfo.Name = "lblIkaiInfo";
			this.lblIkaiInfo.Size = new System.Drawing.Size(390, 20);
			this.lblIkaiInfo.TabIndex = 24;
			this.lblIkaiInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnIkaiSelect
			// 
			this.btnIkaiSelect.Location = new System.Drawing.Point(164, 208);
			this.btnIkaiSelect.Name = "btnIkaiSelect";
			this.btnIkaiSelect.Size = new System.Drawing.Size(80, 30);
			this.btnIkaiSelect.TabIndex = 25;
			this.btnIkaiSelect.Text = "選択…";
			this.btnIkaiSelect.UseVisualStyleBackColor = true;
			// 
			// lblPos
			// 
			this.lblPos.BackColor = System.Drawing.Color.SteelBlue;
			this.lblPos.BackColor2 = System.Drawing.Color.Empty;
			this.lblPos.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblPos.ForeColor = System.Drawing.Color.White;
			this.lblPos.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblPos.IconImage = null;
			this.lblPos.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.lblPos.Location = new System.Drawing.Point(43, 245);
			this.lblPos.Name = "lblPos";
			this.lblPos.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblPos.Size = new System.Drawing.Size(110, 25);
			this.lblPos.TabIndex = 427;
			this.lblPos.Text = "印字開始位置";
			this.lblPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// numRow
			// 
			this.numRow.Location = new System.Drawing.Point(164, 245);
			this.numRow.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
			this.numRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numRow.Name = "numRow";
			this.numRow.Size = new System.Drawing.Size(56, 27);
			this.numRow.TabIndex = 28;
			this.numRow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numRow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lblRow
			// 
			this.lblRow.AutoSize = true;
			this.lblRow.Location = new System.Drawing.Point(224, 248);
			this.lblRow.Name = "lblRow";
			this.lblRow.Size = new System.Drawing.Size(22, 20);
			this.lblRow.TabIndex = 429;
			this.lblRow.Text = "行";
			// 
			// numCol
			// 
			this.numCol.Location = new System.Drawing.Point(260, 245);
			this.numCol.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.numCol.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numCol.Name = "numCol";
			this.numCol.Size = new System.Drawing.Size(56, 27);
			this.numCol.TabIndex = 30;
			this.numCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numCol.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lblCol
			// 
			this.lblCol.AutoSize = true;
			this.lblCol.Location = new System.Drawing.Point(320, 248);
			this.lblCol.Name = "lblCol";
			this.lblCol.Size = new System.Drawing.Size(22, 20);
			this.lblCol.TabIndex = 431;
			this.lblCol.Text = "列";
			// 
			// lblCopy
			// 
			this.lblCopy.BackColor = System.Drawing.Color.SteelBlue;
			this.lblCopy.BackColor2 = System.Drawing.Color.Empty;
			this.lblCopy.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblCopy.ForeColor = System.Drawing.Color.White;
			this.lblCopy.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblCopy.IconImage = null;
			this.lblCopy.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.lblCopy.Location = new System.Drawing.Point(43, 280);
			this.lblCopy.Name = "lblCopy";
			this.lblCopy.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblCopy.Size = new System.Drawing.Size(110, 25);
			this.lblCopy.TabIndex = 432;
			this.lblCopy.Text = "繰返し枚数";
			this.lblCopy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// numCopy
			// 
			this.numCopy.Location = new System.Drawing.Point(164, 280);
			this.numCopy.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.numCopy.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numCopy.Name = "numCopy";
			this.numCopy.Size = new System.Drawing.Size(56, 27);
			this.numCopy.TabIndex = 33;
			this.numCopy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numCopy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lblMai
			// 
			this.lblMai.AutoSize = true;
			this.lblMai.Location = new System.Drawing.Point(224, 283);
			this.lblMai.Name = "lblMai";
			this.lblMai.Size = new System.Drawing.Size(22, 20);
			this.lblMai.TabIndex = 434;
			this.lblMai.Text = "枚";
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 334);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(528, 66);
			this.funckey.TabIndex = 429;
			// 
			// FormOutAtenaLabel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(528, 400);
			this.Controls.Add(this.funckey);
			this.Controls.Add(this.lblMai);
			this.Controls.Add(this.numCopy);
			this.Controls.Add(this.lblCopy);
			this.Controls.Add(this.lblCol);
			this.Controls.Add(this.numCol);
			this.Controls.Add(this.lblRow);
			this.Controls.Add(this.numRow);
			this.Controls.Add(this.lblPos);
			this.Controls.Add(this.btnIkaiSelect);
			this.Controls.Add(this.lblIkaiInfo);
			this.Controls.Add(this.lblIkai);
			this.Controls.Add(this.pnlIryo);
			this.Controls.Add(this.pnlKaiin);
			this.Controls.Add(this.rdoIryo);
			this.Controls.Add(this.rdoIkai);
			this.Controls.Add(this.rdoKaiin);
			this.Controls.Add(this.lblDest);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MinimizeBox = false;
			this.Name = "FormOutAtenaLabel";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "宛名ラベル";
			this.Controls.SetChildIndex(this.ycLabelEx3, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.lblDest, 0);
			this.Controls.SetChildIndex(this.rdoKaiin, 0);
			this.Controls.SetChildIndex(this.rdoIkai, 0);
			this.Controls.SetChildIndex(this.rdoIryo, 0);
			this.Controls.SetChildIndex(this.pnlKaiin, 0);
			this.Controls.SetChildIndex(this.pnlIryo, 0);
			this.Controls.SetChildIndex(this.lblIkai, 0);
			this.Controls.SetChildIndex(this.lblIkaiInfo, 0);
			this.Controls.SetChildIndex(this.btnIkaiSelect, 0);
			this.Controls.SetChildIndex(this.lblPos, 0);
			this.Controls.SetChildIndex(this.numRow, 0);
			this.Controls.SetChildIndex(this.lblRow, 0);
			this.Controls.SetChildIndex(this.numCol, 0);
			this.Controls.SetChildIndex(this.lblCol, 0);
			this.Controls.SetChildIndex(this.lblCopy, 0);
			this.Controls.SetChildIndex(this.numCopy, 0);
			this.Controls.SetChildIndex(this.lblMai, 0);
			this.Controls.SetChildIndex(this.funckey, 0);
			((System.ComponentModel.ISupportInitialize)(this.iKaiinFrom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iKaiinTo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iKaiinKbn)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iIryoFrom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iIryoTo)).EndInit();
			this.pnlKaiin.ResumeLayout(false);
			this.pnlKaiin.PerformLayout();
			this.pnlIryo.ResumeLayout(false);
			this.pnlIryo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numCol)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numCopy)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private YControlLabelEx.YcLabelEx ycLabelEx3;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx lblDest;
		private System.Windows.Forms.RadioButton rdoKaiin;
		private System.Windows.Forms.RadioButton rdoIkai;
		private System.Windows.Forms.RadioButton rdoIryo;
		private YControlLabelEx.YcLabelEx lblKaiinCd;
		private GControlGcTextBoxEx.GcTextBoxEx iKaiinFrom;
		private System.Windows.Forms.Label lblKaiinKara;
		private GControlGcTextBoxEx.GcTextBoxEx iKaiinTo;
		private YControlLabelEx.YcLabelEx lblKaiinKbn;
		private GControlGcComboBoxEx.GcComboBoxEx iKaiinKbn;
		private YControlLabelEx.YcLabelEx lblZaiseki;
		private YControlYcCheckBoxEx.YcCheckBoxEx chkZaiseki;
		private YControlYcCheckBoxEx.YcCheckBoxEx chkIdo;
		private YControlYcCheckBoxEx.YcCheckBoxEx chkTaikai;
		private YControlLabelEx.YcLabelEx lblIryoCd;
		private GControlGcTextBoxEx.GcTextBoxEx iIryoFrom;
		private System.Windows.Forms.Label lblIryoKara;
		private GControlGcTextBoxEx.GcTextBoxEx iIryoTo;
		private YControlLabelEx.YcLabelEx lblIryoTaikai;
		private YControlYcCheckBoxEx.YcCheckBoxEx chkTaikaiNozoku;
		private System.Windows.Forms.Panel pnlKaiin;
		private System.Windows.Forms.Panel pnlIryo;
		private YControlLabelEx.YcLabelEx lblIkai;
		private System.Windows.Forms.Label lblIkaiInfo;
		private System.Windows.Forms.Button btnIkaiSelect;
		private YControlLabelEx.YcLabelEx lblPos;
		private System.Windows.Forms.NumericUpDown numRow;
		private System.Windows.Forms.Label lblRow;
		private System.Windows.Forms.NumericUpDown numCol;
		private System.Windows.Forms.Label lblCol;
		private YControlLabelEx.YcLabelEx lblCopy;
		private System.Windows.Forms.NumericUpDown numCopy;
		private System.Windows.Forms.Label lblMai;
		private App.Controls.FunctionKeyBar funckey;
	}
}
