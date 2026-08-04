namespace App
{
	partial class FormMasterBankCode_DlgEntry
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
			if (disposing && (components != null))
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
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.gcShortcut1 = new GrapeCity.Win.Editors.GcShortcut(this.components);
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.iNameBank = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx4 = new YControlLabelEx.YcLabelEx();
			this.iNameShiten = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx5 = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx6 = new YControlLabelEx.YcLabelEx();
			this.iNameKanaBank = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx8 = new YControlLabelEx.YcLabelEx();
			this.iNameKanaShiten = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx9 = new YControlLabelEx.YcLabelEx();
			this.iBankCode = new GControlGcTextBoxEx.GcTextBoxEx();
			this.iShitenCode = new GControlGcTextBoxEx.GcTextBoxEx();
			this.iFullNameBank = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx3 = new YControlLabelEx.YcLabelEx();
			((System.ComponentModel.ISupportInitialize)(this.iNameBank)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iNameShiten)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iNameKanaBank)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iNameKanaShiten)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iBankCode)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iShitenCode)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iFullNameBank)).BeginInit();
			this.SuspendLayout();
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 193);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(711, 25);
			this.funckey.TabIndex = 34;
			this.funckey.Text = "gcFunctionKey1";
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor2 = System.Drawing.Color.Empty;
			this.lblTitle.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblTitle.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblTitle.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblTitle.IconImage = null;
			this.lblTitle.Location = new System.Drawing.Point(43, 23);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(115, 23);
			this.lblTitle.TabIndex = 39;
			this.lblTitle.Text = "銀行コード情報";
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx1.Location = new System.Drawing.Point(23, 19);
			this.ycLabelEx1.Name = "ycLabelEx1";
			this.ycLabelEx1.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.Size = new System.Drawing.Size(10, 29);
			this.ycLabelEx1.TabIndex = 38;
			// 
			// ycLabelEx2
			// 
			this.ycLabelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(126)))), ((int)(((byte)(113)))));
			this.ycLabelEx2.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx2.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx2.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx2.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.IconImage = null;
			this.ycLabelEx2.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx2.Location = new System.Drawing.Point(49, 65);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(100, 24);
			this.ycLabelEx2.TabIndex = 308;
			this.ycLabelEx2.Text = "銀行コード";
			this.ycLabelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iNameBank
			// 
			this.iNameBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iNameBank.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iNameBank.DisabledForeColor = System.Drawing.Color.Black;
			this.iNameBank.ExFocusHighlight = true;
			this.iNameBank.ImeStr = "";
			this.iNameBank.Location = new System.Drawing.Point(154, 94);
			this.iNameBank.Name = "iNameBank";
			this.iNameBank.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iNameBank.Size = new System.Drawing.Size(197, 23);
			this.iNameBank.TabIndex = 1;
			// 
			// ycLabelEx4
			// 
			this.ycLabelEx4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(126)))), ((int)(((byte)(113)))));
			this.ycLabelEx4.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx4.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx4.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx4.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.IconImage = null;
			this.ycLabelEx4.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx4.Location = new System.Drawing.Point(49, 94);
			this.ycLabelEx4.Name = "ycLabelEx4";
			this.ycLabelEx4.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.Size = new System.Drawing.Size(100, 23);
			this.ycLabelEx4.TabIndex = 312;
			this.ycLabelEx4.Text = "銀行名";
			this.ycLabelEx4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iNameShiten
			// 
			this.iNameShiten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iNameShiten.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iNameShiten.DisabledForeColor = System.Drawing.Color.Black;
			this.iNameShiten.ExFocusHighlight = true;
			this.iNameShiten.ImeStr = "";
			this.iNameShiten.Location = new System.Drawing.Point(480, 94);
			this.iNameShiten.Name = "iNameShiten";
			this.iNameShiten.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iNameShiten.Size = new System.Drawing.Size(197, 23);
			this.iNameShiten.TabIndex = 5;
			// 
			// ycLabelEx5
			// 
			this.ycLabelEx5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(126)))), ((int)(((byte)(113)))));
			this.ycLabelEx5.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx5.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx5.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx5.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx5.IconImage = null;
			this.ycLabelEx5.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx5.Location = new System.Drawing.Point(375, 94);
			this.ycLabelEx5.Name = "ycLabelEx5";
			this.ycLabelEx5.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx5.Size = new System.Drawing.Size(100, 23);
			this.ycLabelEx5.TabIndex = 316;
			this.ycLabelEx5.Text = "支店名";
			this.ycLabelEx5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ycLabelEx6
			// 
			this.ycLabelEx6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(126)))), ((int)(((byte)(113)))));
			this.ycLabelEx6.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx6.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx6.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx6.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.IconImage = null;
			this.ycLabelEx6.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx6.Location = new System.Drawing.Point(375, 65);
			this.ycLabelEx6.Name = "ycLabelEx6";
			this.ycLabelEx6.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.Size = new System.Drawing.Size(100, 23);
			this.ycLabelEx6.TabIndex = 314;
			this.ycLabelEx6.Text = "支店コード";
			this.ycLabelEx6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iNameKanaBank
			// 
			this.iNameKanaBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iNameKanaBank.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iNameKanaBank.DisabledForeColor = System.Drawing.Color.Black;
			this.iNameKanaBank.ExFocusHighlight = true;
			this.iNameKanaBank.ImeStr = "";
			this.iNameKanaBank.Location = new System.Drawing.Point(154, 119);
			this.iNameKanaBank.Name = "iNameKanaBank";
			this.iNameKanaBank.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iNameKanaBank.Size = new System.Drawing.Size(197, 23);
			this.iNameKanaBank.TabIndex = 2;
			// 
			// ycLabelEx8
			// 
			this.ycLabelEx8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(126)))), ((int)(((byte)(113)))));
			this.ycLabelEx8.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx8.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx8.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx8.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx8.IconImage = null;
			this.ycLabelEx8.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx8.Location = new System.Drawing.Point(49, 119);
			this.ycLabelEx8.Name = "ycLabelEx8";
			this.ycLabelEx8.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx8.Size = new System.Drawing.Size(100, 23);
			this.ycLabelEx8.TabIndex = 318;
			this.ycLabelEx8.Text = "銀行名カナ";
			this.ycLabelEx8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iNameKanaShiten
			// 
			this.iNameKanaShiten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iNameKanaShiten.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iNameKanaShiten.DisabledForeColor = System.Drawing.Color.Black;
			this.iNameKanaShiten.ExFocusHighlight = true;
			this.iNameKanaShiten.ImeStr = "";
			this.iNameKanaShiten.Location = new System.Drawing.Point(480, 119);
			this.iNameKanaShiten.Name = "iNameKanaShiten";
			this.iNameKanaShiten.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iNameKanaShiten.Size = new System.Drawing.Size(197, 23);
			this.iNameKanaShiten.TabIndex = 6;
			// 
			// ycLabelEx9
			// 
			this.ycLabelEx9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(126)))), ((int)(((byte)(113)))));
			this.ycLabelEx9.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx9.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx9.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx9.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx9.IconImage = null;
			this.ycLabelEx9.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx9.Location = new System.Drawing.Point(375, 119);
			this.ycLabelEx9.Name = "ycLabelEx9";
			this.ycLabelEx9.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx9.Size = new System.Drawing.Size(100, 23);
			this.ycLabelEx9.TabIndex = 320;
			this.ycLabelEx9.Text = "支店名カナ";
			this.ycLabelEx9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iBankCode
			// 
			this.iBankCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iBankCode.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iBankCode.DisabledForeColor = System.Drawing.Color.Black;
			this.iBankCode.ExFocusHighlight = true;
			this.iBankCode.ImeStr = "";
			this.iBankCode.Location = new System.Drawing.Point(154, 66);
			this.iBankCode.Name = "iBankCode";
			this.iBankCode.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iBankCode.Size = new System.Drawing.Size(50, 23);
			this.iBankCode.TabIndex = 0;
			// 
			// iShitenCode
			// 
			this.iShitenCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iShitenCode.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iShitenCode.DisabledForeColor = System.Drawing.Color.Black;
			this.iShitenCode.ExFocusHighlight = true;
			this.iShitenCode.ImeStr = "";
			this.iShitenCode.Location = new System.Drawing.Point(480, 65);
			this.iShitenCode.Name = "iShitenCode";
			this.iShitenCode.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iShitenCode.Size = new System.Drawing.Size(50, 23);
			this.iShitenCode.TabIndex = 4;
			// 
			// iFullNameBank
			// 
			this.iFullNameBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iFullNameBank.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iFullNameBank.DisabledForeColor = System.Drawing.Color.Black;
			this.iFullNameBank.ExFocusHighlight = true;
			this.iFullNameBank.ImeStr = "";
			this.iFullNameBank.Location = new System.Drawing.Point(154, 148);
			this.iFullNameBank.Name = "iFullNameBank";
			this.iFullNameBank.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iFullNameBank.Size = new System.Drawing.Size(197, 23);
			this.iFullNameBank.TabIndex = 3;
			// 
			// ycLabelEx3
			// 
			this.ycLabelEx3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(126)))), ((int)(((byte)(113)))));
			this.ycLabelEx3.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx3.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx3.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx3.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.IconImage = null;
			this.ycLabelEx3.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx3.Location = new System.Drawing.Point(49, 148);
			this.ycLabelEx3.Name = "ycLabelEx3";
			this.ycLabelEx3.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.Size = new System.Drawing.Size(100, 23);
			this.ycLabelEx3.TabIndex = 330;
			this.ycLabelEx3.Text = "正式名称";
			this.ycLabelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FormMasterBankCode_DlgEntry
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(711, 218);
			this.Controls.Add(this.iFullNameBank);
			this.Controls.Add(this.ycLabelEx3);
			this.Controls.Add(this.iShitenCode);
			this.Controls.Add(this.iBankCode);
			this.Controls.Add(this.iNameKanaShiten);
			this.Controls.Add(this.ycLabelEx9);
			this.Controls.Add(this.iNameKanaBank);
			this.Controls.Add(this.ycLabelEx8);
			this.Controls.Add(this.iNameShiten);
			this.Controls.Add(this.ycLabelEx5);
			this.Controls.Add(this.ycLabelEx6);
			this.Controls.Add(this.iNameBank);
			this.Controls.Add(this.ycLabelEx4);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx1);
			this.Controls.Add(this.funckey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMasterBankCode_DlgEntry";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "銀行コードマスタ";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.ycLabelEx4, 0);
			this.Controls.SetChildIndex(this.iNameBank, 0);
			this.Controls.SetChildIndex(this.ycLabelEx6, 0);
			this.Controls.SetChildIndex(this.ycLabelEx5, 0);
			this.Controls.SetChildIndex(this.iNameShiten, 0);
			this.Controls.SetChildIndex(this.ycLabelEx8, 0);
			this.Controls.SetChildIndex(this.iNameKanaBank, 0);
			this.Controls.SetChildIndex(this.ycLabelEx9, 0);
			this.Controls.SetChildIndex(this.iNameKanaShiten, 0);
			this.Controls.SetChildIndex(this.iBankCode, 0);
			this.Controls.SetChildIndex(this.iShitenCode, 0);
			this.Controls.SetChildIndex(this.ycLabelEx3, 0);
			this.Controls.SetChildIndex(this.iFullNameBank, 0);
			((System.ComponentModel.ISupportInitialize)(this.iNameBank)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iNameShiten)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iNameKanaBank)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iNameKanaShiten)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iBankCode)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iShitenCode)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iFullNameBank)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private GrapeCity.Win.Editors.GcShortcut gcShortcut1;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private GControlGcTextBoxEx.GcTextBoxEx iNameBank;
		private YControlLabelEx.YcLabelEx ycLabelEx4;
		private GControlGcTextBoxEx.GcTextBoxEx iNameShiten;
		private YControlLabelEx.YcLabelEx ycLabelEx5;
		private YControlLabelEx.YcLabelEx ycLabelEx6;
		private GControlGcTextBoxEx.GcTextBoxEx iNameKanaBank;
		private YControlLabelEx.YcLabelEx ycLabelEx8;
		private GControlGcTextBoxEx.GcTextBoxEx iNameKanaShiten;
		private YControlLabelEx.YcLabelEx ycLabelEx9;
		private GControlGcTextBoxEx.GcTextBoxEx iBankCode;
		private GControlGcTextBoxEx.GcTextBoxEx iShitenCode;
		private GControlGcTextBoxEx.GcTextBoxEx iFullNameBank;
		private YControlLabelEx.YcLabelEx ycLabelEx3;
	}
}