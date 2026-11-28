
namespace App
{
	partial class FormOutAtenaLabel_DlgIkai
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
			this.funckey = new App.Controls.FunctionKeyBar();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.lblGuide = new YControlLabelEx.YcLabelEx();
			this.pnlItems = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			//
			// funckey
			//
			this.funckey.Location = new System.Drawing.Point(0, 500);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(830, 66);
			this.funckey.TabIndex = 341;
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
			this.lblTitle.Text = "参加医会の選択";
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
			// lblGuide
			//
			this.lblGuide.BackColor2 = System.Drawing.Color.Empty;
			this.lblGuide.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblGuide.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblGuide.IconImage = null;
			this.lblGuide.Location = new System.Drawing.Point(26, 60);
			this.lblGuide.Name = "lblGuide";
			this.lblGuide.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblGuide.Size = new System.Drawing.Size(778, 23);
			this.lblGuide.TabIndex = 344;
			this.lblGuide.Text = "チェックした参加医会のラベルだけを出力します。";
			//
			// pnlItems
			//
			this.pnlItems.AutoScroll = true;
			this.pnlItems.Location = new System.Drawing.Point(26, 88);
			this.pnlItems.Name = "pnlItems";
			this.pnlItems.Size = new System.Drawing.Size(778, 404);
			this.pnlItems.TabIndex = 1;
			//
			// FormOutAtenaLabel_DlgIkai
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(830, 566);
			this.Controls.Add(this.pnlItems);
			this.Controls.Add(this.lblGuide);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.funckey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormOutAtenaLabel_DlgIkai";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "宛名ラベル - 参加医会の選択";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.lblGuide, 0);
			this.Controls.SetChildIndex(this.pnlItems, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private App.Controls.FunctionKeyBar funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private YControlLabelEx.YcLabelEx lblGuide;
		private System.Windows.Forms.Panel pnlItems;
	}
}
