
namespace App
{
	partial class FormOutExcelShimingyoinKaihi
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
			this.funckey = new ComponentSUI.FunctionKeyBar();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.iYear = new App.UcDate();
			this.ycLabelEx14 = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.iPiriod = new GControlGcComboBoxEx.GcComboBoxEx(this.components);
			this.rdoKen = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.iPiriod)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 196);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(395, 66);
			this.funckey.TabIndex = 56;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor2 = System.Drawing.Color.Empty;
			this.lblTitle.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblTitle.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblTitle.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblTitle.IconImage = null;
			this.lblTitle.Location = new System.Drawing.Point(32, 31);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(160, 23);
			this.lblTitle.TabIndex = 79;
			this.lblTitle.Text = "市民病院会費請求資料";
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx1.Location = new System.Drawing.Point(12, 27);
			this.ycLabelEx1.Name = "ycLabelEx1";
			this.ycLabelEx1.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.Size = new System.Drawing.Size(10, 29);
			this.ycLabelEx1.TabIndex = 78;
			// 
			// iYear
			// 
			this.iYear.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(251)))));
			this.iYear.DisabledForeColor = System.Drawing.Color.Black;
			this.iYear.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iYear.Location = new System.Drawing.Point(149, 90);
			this.iYear.Name = "iYear";
			this.iYear.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iYear.Size = new System.Drawing.Size(80, 25);
			this.iYear.TabIndex = 0;
			this.iYear.UcBackColor = System.Drawing.SystemColors.Window;
			this.iYear.UcBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iYear.UcCalenderMaxDate = new System.DateTime(2100, 12, 31, 23, 59, 59, 0);
			this.iYear.UcCalenderMinDate = new System.DateTime(1868, 1, 1, 0, 0, 0, 0);
			this.iYear.UcEnabled = true;
			this.iYear.UcFormat = App.UcDate.UcDateFormat.YYYY;
			this.iYear.UcReadOnly = false;
			this.iYear.UcSideButton = false;
			this.iYear.UcValue = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
			// 
			// ycLabelEx14
			// 
			this.ycLabelEx14.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx14.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx14.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx14.Font = new System.Drawing.Font("メイリオ", 9.75F);
			this.ycLabelEx14.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx14.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx14.IconImage = null;
			this.ycLabelEx14.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx14.Location = new System.Drawing.Point(43, 90);
			this.ycLabelEx14.Name = "ycLabelEx14";
			this.ycLabelEx14.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx14.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx14.TabIndex = 332;
			this.ycLabelEx14.Text = "出力期間";
			this.ycLabelEx14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			this.ycLabelEx2.Location = new System.Drawing.Point(43, 133);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx2.TabIndex = 334;
			this.ycLabelEx2.Text = "医会";
			this.ycLabelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iPiriod
			// 
			this.iPiriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iPiriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.iPiriod.ExCompareContent = null;
			this.iPiriod.ExCompareValue = null;
			this.iPiriod.ExDataSource = null;
			this.iPiriod.ExFocusHighlight = true;
			this.iPiriod.FlatStyle = GrapeCity.Win.Editors.FlatStyleEx.Flat;
			this.iPiriod.ListHeaderPane.Height = 27;
			this.iPiriod.ListHeaderPane.Visible = false;
			this.iPiriod.Location = new System.Drawing.Point(235, 90);
			this.iPiriod.Name = "iPiriod";
			this.iPiriod.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iPiriod.Size = new System.Drawing.Size(69, 25);
			this.iPiriod.TabIndex = 1;
			// 
			// rdoKen
			// 
			this.rdoKen.AutoSize = true;
			this.rdoKen.Location = new System.Drawing.Point(16, 13);
			this.rdoKen.Name = "rdoKen";
			this.rdoKen.Size = new System.Drawing.Size(53, 24);
			this.rdoKen.TabIndex = 0;
			this.rdoKen.TabStop = true;
			this.rdoKen.Text = "県医";
			this.rdoKen.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new System.Drawing.Point(86, 13);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(53, 24);
			this.radioButton1.TabIndex = 1;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "市医";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rdoKen);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Location = new System.Drawing.Point(149, 121);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(155, 39);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// FormOutExcelShimingyoinKaihi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(395, 262);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.iPiriod);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.iYear);
			this.Controls.Add(this.ycLabelEx14);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx1);
			this.Controls.Add(this.funckey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FormOutExcelShimingyoinKaihi";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Excel出力 - 市民病院会費請求";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.ycLabelEx14, 0);
			this.Controls.SetChildIndex(this.iYear, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.iPiriod, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			((System.ComponentModel.ISupportInitialize)(this.iPiriod)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ComponentSUI.FunctionKeyBar funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private UcDate iYear;
		private YControlLabelEx.YcLabelEx ycLabelEx14;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private GControlGcComboBoxEx.GcComboBoxEx iPiriod;
		private System.Windows.Forms.RadioButton rdoKen;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}