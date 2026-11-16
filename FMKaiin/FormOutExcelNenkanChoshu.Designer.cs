
namespace App
{
	partial class FormOutExcelNenkanChoshu
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
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.iYear = new App.UcDate();
			this.ycLabelEx14 = new YControlLabelEx.YcLabelEx();
			this.iKaiinFrom = new App.UcTableComboBox();
			this.ycLabelEx6 = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rdoYotei = new System.Windows.Forms.RadioButton();
			this.rdoJisseki = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// funckey
			// 
			this.funckey.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.funckey.Location = new System.Drawing.Point(0, 237);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(481, 25);
			this.funckey.TabIndex = 56;
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
			this.lblTitle.Location = new System.Drawing.Point(54, 26);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(130, 23);
			this.lblTitle.TabIndex = 79;
			this.lblTitle.Text = "年間会費徴収書類";
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx1.Location = new System.Drawing.Point(34, 22);
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
			this.iYear.Location = new System.Drawing.Point(168, 78);
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
			this.ycLabelEx14.Location = new System.Drawing.Point(62, 78);
			this.ycLabelEx14.Name = "ycLabelEx14";
			this.ycLabelEx14.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx14.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx14.TabIndex = 332;
			this.ycLabelEx14.Text = "年";
			this.ycLabelEx14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iKaiinFrom
			// 
			this.iKaiinFrom.BackColor = System.Drawing.Color.Transparent;
			this.iKaiinFrom.ComboBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iKaiinFrom.CompareValue = null;
			this.iKaiinFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.iKaiinFrom.DBView = null;
			this.iKaiinFrom.DiabledBackColor = System.Drawing.SystemColors.Control;
			this.iKaiinFrom.DisabledForeColor = System.Drawing.SystemColors.GrayText;
			this.iKaiinFrom.DropDownSize = new System.Drawing.Size(100, 44);
			this.iKaiinFrom.EnterImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.iKaiinFrom.ForeColor = System.Drawing.Color.Transparent;
			this.iKaiinFrom.HighlightText = true;
			this.iKaiinFrom.Location = new System.Drawing.Point(168, 122);
			this.iKaiinFrom.Name = "iKaiinFrom";
			this.iKaiinFrom.RowFilter = "";
			this.iKaiinFrom.SelectedIndexNullLeave = -1;
			this.iKaiinFrom.Size = new System.Drawing.Size(200, 25);
			this.iKaiinFrom.Sort = "";
			this.iKaiinFrom.TabIndex = 333;
			this.iKaiinFrom.TextSubItemIndex = -1;
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
			this.ycLabelEx6.Location = new System.Drawing.Point(62, 122);
			this.ycLabelEx6.Name = "ycLabelEx6";
			this.ycLabelEx6.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx6.TabIndex = 334;
			this.ycLabelEx6.Text = "会員";
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
			this.ycLabelEx2.Location = new System.Drawing.Point(62, 171);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx2.TabIndex = 335;
			this.ycLabelEx2.Text = "書類タイプ";
			this.ycLabelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rdoYotei);
			this.groupBox1.Controls.Add(this.rdoJisseki);
			this.groupBox1.Location = new System.Drawing.Point(168, 159);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(297, 39);
			this.groupBox1.TabIndex = 336;
			this.groupBox1.TabStop = false;
			// 
			// rdoYotei
			// 
			this.rdoYotei.AutoSize = true;
			this.rdoYotei.Location = new System.Drawing.Point(20, 13);
			this.rdoYotei.Name = "rdoYotei";
			this.rdoYotei.Size = new System.Drawing.Size(92, 24);
			this.rdoYotei.TabIndex = 0;
			this.rdoYotei.TabStop = true;
			this.rdoYotei.Text = "予定通知書";
			this.rdoYotei.UseVisualStyleBackColor = true;
			// 
			// rdoJisseki
			// 
			this.rdoJisseki.AutoSize = true;
			this.rdoJisseki.Location = new System.Drawing.Point(134, 12);
			this.rdoJisseki.Name = "rdoJisseki";
			this.rdoJisseki.Size = new System.Drawing.Size(144, 24);
			this.rdoJisseki.TabIndex = 1;
			this.rdoJisseki.TabStop = true;
			this.rdoJisseki.Text = "徴収明細書（実績）";
			this.rdoJisseki.UseVisualStyleBackColor = true;
			// 
			// FormOutExcelNenkanChoshu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(481, 262);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.iKaiinFrom);
			this.Controls.Add(this.ycLabelEx6);
			this.Controls.Add(this.iYear);
			this.Controls.Add(this.ycLabelEx14);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx1);
			this.Controls.Add(this.funckey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FormOutExcelNenkanChoshu";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Excel出力 - 年間会費徴収書類";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.ycLabelEx14, 0);
			this.Controls.SetChildIndex(this.iYear, 0);
			this.Controls.SetChildIndex(this.ycLabelEx6, 0);
			this.Controls.SetChildIndex(this.iKaiinFrom, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private UcDate iYear;
		private YControlLabelEx.YcLabelEx ycLabelEx14;
		private UcTableComboBox iKaiinFrom;
		private YControlLabelEx.YcLabelEx ycLabelEx6;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rdoYotei;
		private System.Windows.Forms.RadioButton rdoJisseki;
	}
}