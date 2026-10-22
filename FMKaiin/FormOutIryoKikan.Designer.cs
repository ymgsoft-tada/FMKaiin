
namespace App
{
	partial class FormOutIryoKikan
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
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx3 = new YControlLabelEx.YcLabelEx();
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.ycLabelEx7 = new YControlLabelEx.YcLabelEx();
			this.rdoList = new System.Windows.Forms.RadioButton();
			this.rdoJoho = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
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
			// funckey
			// 
			this.funckey.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.funckey.Location = new System.Drawing.Point(0, 163);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(346, 25);
			this.funckey.TabIndex = 429;
			this.funckey.Text = "gcFunctionKey1";
			// 
			// ycLabelEx7
			// 
			this.ycLabelEx7.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx7.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx7.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx7.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx7.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx7.IconImage = null;
			this.ycLabelEx7.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx7.Location = new System.Drawing.Point(43, 80);
			this.ycLabelEx7.Name = "ycLabelEx7";
			this.ycLabelEx7.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx7.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx7.TabIndex = 432;
			this.ycLabelEx7.Text = "印刷範囲";
			this.ycLabelEx7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// rdoList
			// 
			this.rdoList.AutoSize = true;
			this.rdoList.Location = new System.Drawing.Point(164, 81);
			this.rdoList.Name = "rdoList";
			this.rdoList.Size = new System.Drawing.Size(118, 24);
			this.rdoList.TabIndex = 433;
			this.rdoList.TabStop = true;
			this.rdoList.Text = "医療機関一覧表";
			this.rdoList.UseVisualStyleBackColor = true;
			// 
			// rdoJoho
			// 
			this.rdoJoho.AutoSize = true;
			this.rdoJoho.Location = new System.Drawing.Point(164, 111);
			this.rdoJoho.Name = "rdoJoho";
			this.rdoJoho.Size = new System.Drawing.Size(105, 24);
			this.rdoJoho.TabIndex = 434;
			this.rdoJoho.TabStop = true;
			this.rdoJoho.Text = "医療機関情報";
			this.rdoJoho.UseVisualStyleBackColor = true;
			// 
			// FormOutIryoKikan
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(346, 188);
			this.Controls.Add(this.rdoJoho);
			this.Controls.Add(this.rdoList);
			this.Controls.Add(this.ycLabelEx7);
			this.Controls.Add(this.funckey);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Name = "FormOutIryoKikan";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "医療機関出力";
			this.Controls.SetChildIndex(this.ycLabelEx3, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx7, 0);
			this.Controls.SetChildIndex(this.rdoList, 0);
			this.Controls.SetChildIndex(this.rdoJoho, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx3;
		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private YControlLabelEx.YcLabelEx ycLabelEx7;
		private System.Windows.Forms.RadioButton rdoList;
		private System.Windows.Forms.RadioButton rdoJoho;
	}
}