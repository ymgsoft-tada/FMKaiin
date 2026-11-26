
namespace App
{
	partial class FormOutExcelFurikomi
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
			this.funckey = new ComponentSUI.FunctionKeyBar();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.iDateYM = new App.UcDate();
			this.ycLabelEx6 = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.iDate = new App.UcDate();
			this.SuspendLayout();
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 196);
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
			this.lblTitle.Text = "医師信用組合振込データ一覧";
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
			this.iDateYM.Location = new System.Drawing.Point(172, 84);
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
			this.ycLabelEx6.Location = new System.Drawing.Point(66, 84);
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
			this.ycLabelEx2.Location = new System.Drawing.Point(66, 121);
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
			this.iDate.Location = new System.Drawing.Point(172, 121);
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
			// FormOutExcelFurikomi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(442, 262);
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
			this.Name = "FormOutExcelFurikomi";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Excel出力 - 医師信用組合振込データ一覧";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.ycLabelEx6, 0);
			this.Controls.SetChildIndex(this.iDateYM, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.iDate, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ComponentSUI.FunctionKeyBar funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private UcDate iDateYM;
		private YControlLabelEx.YcLabelEx ycLabelEx6;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private UcDate iDate;
	}
}