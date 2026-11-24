
namespace App
{
	partial class FormOutExcelNofusho
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
			this.funckey = new App.Controls.FunctionKeyBar();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.iDateYM = new App.UcDate();
			this.ycLabelEx6 = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.iDate = new App.UcDate();
			this.ycLabelEx3 = new YControlLabelEx.YcLabelEx();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rdoNichii = new System.Windows.Forms.RadioButton();
			this.rdoKeni = new System.Windows.Forms.RadioButton();
			this.rdoShii = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 205);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(442, 66);
			this.funckey.TabIndex = 56;
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor2 = System.Drawing.Color.Empty;
			this.lblTitle.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblTitle.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblTitle.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblTitle.IconImage = null;
			this.lblTitle.Location = new System.Drawing.Point(50, 30);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(273, 23);
			this.lblTitle.TabIndex = 79;
			this.lblTitle.Text = "会費納付書";
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx1.Location = new System.Drawing.Point(30, 26);
			this.ycLabelEx1.Name = "ycLabelEx1";
			this.ycLabelEx1.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.Size = new System.Drawing.Size(10, 29);
			this.ycLabelEx1.TabIndex = 78;
			// 
			// iDateYM
			// 
			this.iDateYM.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(251)))));
			this.iDateYM.DisabledForeColor = System.Drawing.Color.Black;
			this.iDateYM.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iDateYM.Location = new System.Drawing.Point(173, 79);
			this.iDateYM.Name = "iDateYM";
			this.iDateYM.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iDateYM.Size = new System.Drawing.Size(120, 25);
			this.iDateYM.TabIndex = 0;
			this.iDateYM.UcBackColor = System.Drawing.SystemColors.Window;
			this.iDateYM.UcBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iDateYM.UcCalenderMaxDate = new System.DateTime(2100, 12, 31, 23, 59, 59, 0);
			this.iDateYM.UcCalenderMinDate = new System.DateTime(1868, 1, 1, 0, 0, 0, 0);
			this.iDateYM.UcEnabled = true;
			this.iDateYM.UcFormat = App.UcDate.UcDateFormat.YYYYMM;
			this.iDateYM.UcReadOnly = false;
			this.iDateYM.UcSideButton = true;
			this.iDateYM.UcValue = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
			// 
			// ycLabelEx6
			// 
			this.ycLabelEx6.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx6.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx6.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx6.Font = new System.Drawing.Font("メイリオ", 9.75F);
			this.ycLabelEx6.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx6.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.IconImage = null;
			this.ycLabelEx6.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx6.Location = new System.Drawing.Point(67, 79);
			this.ycLabelEx6.Name = "ycLabelEx6";
			this.ycLabelEx6.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx6.TabIndex = 334;
			this.ycLabelEx6.Text = "月分";
			this.ycLabelEx6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ycLabelEx2
			// 
			this.ycLabelEx2.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx2.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx2.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx2.Font = new System.Drawing.Font("メイリオ", 9.75F);
			this.ycLabelEx2.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx2.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.IconImage = null;
			this.ycLabelEx2.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx2.Location = new System.Drawing.Point(67, 116);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx2.TabIndex = 335;
			this.ycLabelEx2.Text = "処理日";
			this.ycLabelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iDate
			// 
			this.iDate.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(251)))));
			this.iDate.DisabledForeColor = System.Drawing.Color.Black;
			this.iDate.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iDate.Location = new System.Drawing.Point(173, 116);
			this.iDate.Name = "iDate";
			this.iDate.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iDate.Size = new System.Drawing.Size(143, 25);
			this.iDate.TabIndex = 1;
			this.iDate.UcBackColor = System.Drawing.SystemColors.Window;
			this.iDate.UcBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iDate.UcCalenderMaxDate = new System.DateTime(2100, 12, 31, 23, 59, 59, 0);
			this.iDate.UcCalenderMinDate = new System.DateTime(1868, 1, 1, 0, 0, 0, 0);
			this.iDate.UcEnabled = true;
			this.iDate.UcFormat = App.UcDate.UcDateFormat.YYYYMMDD;
			this.iDate.UcReadOnly = false;
			this.iDate.UcSideButton = true;
			this.iDate.UcValue = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
			// 
			// ycLabelEx3
			// 
			this.ycLabelEx3.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx3.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx3.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx3.Font = new System.Drawing.Font("メイリオ", 9.75F);
			this.ycLabelEx3.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx3.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.IconImage = null;
			this.ycLabelEx3.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx3.Location = new System.Drawing.Point(67, 153);
			this.ycLabelEx3.Name = "ycLabelEx3";
			this.ycLabelEx3.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx3.TabIndex = 336;
			this.ycLabelEx3.Text = "医師会";
			this.ycLabelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rdoNichii);
			this.groupBox1.Controls.Add(this.rdoKeni);
			this.groupBox1.Controls.Add(this.rdoShii);
			this.groupBox1.Location = new System.Drawing.Point(173, 142);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(225, 39);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// rdoNichii
			// 
			this.rdoNichii.AutoSize = true;
			this.rdoNichii.Location = new System.Drawing.Point(16, 13);
			this.rdoNichii.Name = "rdoNichii";
			this.rdoNichii.Size = new System.Drawing.Size(53, 24);
			this.rdoNichii.TabIndex = 0;
			this.rdoNichii.TabStop = true;
			this.rdoNichii.Text = "日医";
			this.rdoNichii.UseVisualStyleBackColor = true;
			// 
			// rdoKeni
			// 
			this.rdoKeni.AutoSize = true;
			this.rdoKeni.Location = new System.Drawing.Point(86, 13);
			this.rdoKeni.Name = "rdoKeni";
			this.rdoKeni.Size = new System.Drawing.Size(53, 24);
			this.rdoKeni.TabIndex = 1;
			this.rdoKeni.TabStop = true;
			this.rdoKeni.Text = "県医";
			this.rdoKeni.UseVisualStyleBackColor = true;
			// 
			// rdoShii
			// 
			this.rdoShii.AutoSize = true;
			this.rdoShii.Location = new System.Drawing.Point(156, 13);
			this.rdoShii.Name = "rdoShii";
			this.rdoShii.Size = new System.Drawing.Size(53, 24);
			this.rdoShii.TabIndex = 2;
			this.rdoShii.TabStop = true;
			this.rdoShii.Text = "市医";
			this.rdoShii.UseVisualStyleBackColor = true;
			// 
			// FormOutExcelNofusho
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(442, 271);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ycLabelEx3);
			this.Controls.Add(this.iDate);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.iDateYM);
			this.Controls.Add(this.ycLabelEx6);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx1);
			this.Controls.Add(this.funckey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FormOutExcelNofusho";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Excel出力 - 会費納付書";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.ycLabelEx6, 0);
			this.Controls.SetChildIndex(this.iDateYM, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.iDate, 0);
			this.Controls.SetChildIndex(this.ycLabelEx3, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private App.Controls.FunctionKeyBar funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private UcDate iDateYM;
		private YControlLabelEx.YcLabelEx ycLabelEx6;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private UcDate iDate;
		private YControlLabelEx.YcLabelEx ycLabelEx3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rdoNichii;
		private System.Windows.Forms.RadioButton rdoKeni;
		private System.Windows.Forms.RadioButton rdoShii;
	}
}