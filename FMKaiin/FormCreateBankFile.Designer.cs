
namespace App
{
	partial class FormCreateBankFile
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
			this.ycLabelEx4 = new YControlLabelEx.YcLabelEx();
			this.iBank = new App.UcTableComboBox();
			this.ycLabelEx6 = new YControlLabelEx.YcLabelEx();
			this.iDate = new App.UcDate();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.iDateYM = new App.UcDate();
			this.SuspendLayout();
			// 
			// ycLabelEx4
			// 
			this.ycLabelEx4.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx4.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx4.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx4.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx4.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.IconImage = null;
			this.ycLabelEx4.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx4.Location = new System.Drawing.Point(42, 74);
			this.ycLabelEx4.Name = "ycLabelEx4";
			this.ycLabelEx4.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.Size = new System.Drawing.Size(120, 25);
			this.ycLabelEx4.TabIndex = 335;
			this.ycLabelEx4.Text = "月度";
			this.ycLabelEx4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iBank
			// 
			this.iBank.BackColor = System.Drawing.Color.Transparent;
			this.iBank.ComboBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iBank.CompareValue = null;
			this.iBank.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.iBank.DBView = null;
			this.iBank.DiabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iBank.DisabledForeColor = System.Drawing.Color.Black;
			this.iBank.DropDownSize = new System.Drawing.Size(224, 44);
			this.iBank.EnterImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.iBank.ForeColor = System.Drawing.Color.Transparent;
			this.iBank.HighlightText = true;
			this.iBank.Location = new System.Drawing.Point(166, 114);
			this.iBank.Name = "iBank";
			this.iBank.RowFilter = "";
			this.iBank.SelectedIndexNullLeave = -1;
			this.iBank.Size = new System.Drawing.Size(301, 25);
			this.iBank.Sort = "";
			this.iBank.TabIndex = 338;
			this.iBank.TextSubItemIndex = -1;
			// 
			// ycLabelEx6
			// 
			this.ycLabelEx6.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx6.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx6.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx6.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx6.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.IconImage = null;
			this.ycLabelEx6.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx6.Location = new System.Drawing.Point(42, 114);
			this.ycLabelEx6.Name = "ycLabelEx6";
			this.ycLabelEx6.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.Size = new System.Drawing.Size(120, 25);
			this.ycLabelEx6.TabIndex = 337;
			this.ycLabelEx6.Text = "引落元銀行";
			this.ycLabelEx6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iDate
			// 
			this.iDate.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(251)))));
			this.iDate.DisabledForeColor = System.Drawing.Color.Black;
			this.iDate.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iDate.Location = new System.Drawing.Point(168, 156);
			this.iDate.Name = "iDate";
			this.iDate.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iDate.Size = new System.Drawing.Size(143, 25);
			this.iDate.TabIndex = 339;
			this.iDate.UcBackColor = System.Drawing.SystemColors.Window;
			this.iDate.UcBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iDate.UcCalenderMaxDate = new System.DateTime(2100, 12, 31, 23, 59, 59, 0);
			this.iDate.UcCalenderMinDate = new System.DateTime(1868, 1, 1, 0, 0, 0, 0);
			this.iDate.UcEnabled = true;
			this.iDate.UcFormat = App.UcDate.UcDateFormat.YYYYMMDD;
			this.iDate.UcReadOnly = false;
			this.iDate.UcSideButton = true;
			this.iDate.UcValue = new System.DateTime(2017, 3, 16, 13, 16, 12, 918);
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx1.Location = new System.Drawing.Point(42, 156);
			this.ycLabelEx1.Name = "ycLabelEx1";
			this.ycLabelEx1.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.Size = new System.Drawing.Size(120, 25);
			this.ycLabelEx1.TabIndex = 340;
			this.ycLabelEx1.Text = "引落日";
			this.ycLabelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 204);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(525, 25);
			this.funckey.TabIndex = 341;
			this.funckey.Text = "gcFunctionKey1";
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor2 = System.Drawing.Color.Empty;
			this.lblTitle.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblTitle.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblTitle.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblTitle.IconImage = null;
			this.lblTitle.Location = new System.Drawing.Point(37, 23);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(299, 23);
			this.lblTitle.TabIndex = 343;
			this.lblTitle.Text = "銀行用引落データ作成";
			// 
			// ycLabelEx2
			// 
			this.ycLabelEx2.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx2.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx2.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx2.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.IconImage = null;
			this.ycLabelEx2.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx2.Location = new System.Drawing.Point(21, 19);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(10, 30);
			this.ycLabelEx2.TabIndex = 342;
			// 
			// iDateYM
			// 
			this.iDateYM.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(251)))));
			this.iDateYM.DisabledForeColor = System.Drawing.Color.Black;
			this.iDateYM.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iDateYM.Location = new System.Drawing.Point(168, 74);
			this.iDateYM.Name = "iDateYM";
			this.iDateYM.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iDateYM.Size = new System.Drawing.Size(120, 25);
			this.iDateYM.TabIndex = 344;
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
			// FormCreateBankFile
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(525, 229);
			this.Controls.Add(this.iDateYM);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.funckey);
			this.Controls.Add(this.ycLabelEx1);
			this.Controls.Add(this.iDate);
			this.Controls.Add(this.iBank);
			this.Controls.Add(this.ycLabelEx6);
			this.Controls.Add(this.ycLabelEx4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCreateBankFile";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FBデータ作成";
			this.Controls.SetChildIndex(this.ycLabelEx4, 0);
			this.Controls.SetChildIndex(this.ycLabelEx6, 0);
			this.Controls.SetChildIndex(this.iBank, 0);
			this.Controls.SetChildIndex(this.iDate, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.iDateYM, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private YControlLabelEx.YcLabelEx ycLabelEx4;
		private UcTableComboBox iBank;
		private YControlLabelEx.YcLabelEx ycLabelEx6;
		private UcDate iDate;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private UcDate iDateYM;
	}
}