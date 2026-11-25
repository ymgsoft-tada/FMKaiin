
namespace App
{
	partial class FormReportMenu2
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
			this.btnHikiIkaiShukei = new System.Windows.Forms.Button();
			this.funckey = new App.Controls.FunctionKeyBar();
			this.btnHikiMeisai = new System.Windows.Forms.Button();
			this.btnHikiIkaiMeisai = new System.Windows.Forms.Button();
			this.btnHikiKakunin = new System.Windows.Forms.Button();
			this.btnMinyukin = new System.Windows.Forms.Button();
			this.btnChoshuMeisai = new System.Windows.Forms.Button();
			this.btnHikiShukei = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnHikiIkaiShukei
			// 
			this.btnHikiIkaiShukei.Location = new System.Drawing.Point(63, 123);
			this.btnHikiIkaiShukei.Name = "btnHikiIkaiShukei";
			this.btnHikiIkaiShukei.Size = new System.Drawing.Size(248, 36);
			this.btnHikiIkaiShukei.TabIndex = 3;
			this.btnHikiIkaiShukei.Text = "⑬所属医会別引落集計表";
			this.btnHikiIkaiShukei.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnHikiIkaiShukei.UseVisualStyleBackColor = true;
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 396);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(392, 66);
			this.funckey.TabIndex = 56;
			// 
			// btnHikiMeisai
			// 
			this.btnHikiMeisai.Location = new System.Drawing.Point(63, 39);
			this.btnHikiMeisai.Name = "btnHikiMeisai";
			this.btnHikiMeisai.Size = new System.Drawing.Size(248, 36);
			this.btnHikiMeisai.TabIndex = 1;
			this.btnHikiMeisai.Text = "⑪会員別引落明細表";
			this.btnHikiMeisai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnHikiMeisai.UseVisualStyleBackColor = true;
			// 
			// btnHikiIkaiMeisai
			// 
			this.btnHikiIkaiMeisai.Location = new System.Drawing.Point(63, 81);
			this.btnHikiIkaiMeisai.Name = "btnHikiIkaiMeisai";
			this.btnHikiIkaiMeisai.Size = new System.Drawing.Size(248, 36);
			this.btnHikiIkaiMeisai.TabIndex = 2;
			this.btnHikiIkaiMeisai.Text = "⑫所属医会別引落明細表";
			this.btnHikiIkaiMeisai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnHikiIkaiMeisai.UseVisualStyleBackColor = true;
			// 
			// btnHikiKakunin
			// 
			this.btnHikiKakunin.Location = new System.Drawing.Point(63, 165);
			this.btnHikiKakunin.Name = "btnHikiKakunin";
			this.btnHikiKakunin.Size = new System.Drawing.Size(248, 36);
			this.btnHikiKakunin.TabIndex = 57;
			this.btnHikiKakunin.Text = "⑭引落金額確認表";
			this.btnHikiKakunin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnHikiKakunin.UseVisualStyleBackColor = true;
			// 
			// btnMinyukin
			// 
			this.btnMinyukin.Location = new System.Drawing.Point(63, 207);
			this.btnMinyukin.Name = "btnMinyukin";
			this.btnMinyukin.Size = new System.Drawing.Size(248, 36);
			this.btnMinyukin.TabIndex = 58;
			this.btnMinyukin.Text = "⑮未入金者用請求書";
			this.btnMinyukin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnMinyukin.UseVisualStyleBackColor = true;
			// 
			// btnChoshuMeisai
			// 
			this.btnChoshuMeisai.Location = new System.Drawing.Point(63, 249);
			this.btnChoshuMeisai.Name = "btnChoshuMeisai";
			this.btnChoshuMeisai.Size = new System.Drawing.Size(248, 36);
			this.btnChoshuMeisai.TabIndex = 59;
			this.btnChoshuMeisai.Text = "⑯年間会費徴収明細書";
			this.btnChoshuMeisai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnChoshuMeisai.UseVisualStyleBackColor = true;
			// 
			// btnHikiShukei
			// 
			this.btnHikiShukei.Location = new System.Drawing.Point(63, 291);
			this.btnHikiShukei.Name = "btnHikiShukei";
			this.btnHikiShukei.Size = new System.Drawing.Size(248, 36);
			this.btnHikiShukei.TabIndex = 60;
			this.btnHikiShukei.Text = "⑰引落口座別集計表";
			this.btnHikiShukei.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnHikiShukei.UseVisualStyleBackColor = true;
			// 
			// FormReportMenu2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(392, 462);
			this.Controls.Add(this.btnHikiShukei);
			this.Controls.Add(this.btnChoshuMeisai);
			this.Controls.Add(this.btnMinyukin);
			this.Controls.Add(this.btnHikiKakunin);
			this.Controls.Add(this.btnHikiIkaiMeisai);
			this.Controls.Add(this.btnHikiMeisai);
			this.Controls.Add(this.funckey);
			this.Controls.Add(this.btnHikiIkaiShukei);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormReportMenu2";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "帳票出力2";
			this.Controls.SetChildIndex(this.btnHikiIkaiShukei, 0);
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.btnHikiMeisai, 0);
			this.Controls.SetChildIndex(this.btnHikiIkaiMeisai, 0);
			this.Controls.SetChildIndex(this.btnHikiKakunin, 0);
			this.Controls.SetChildIndex(this.btnMinyukin, 0);
			this.Controls.SetChildIndex(this.btnChoshuMeisai, 0);
			this.Controls.SetChildIndex(this.btnHikiShukei, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnHikiIkaiShukei;
		private App.Controls.FunctionKeyBar funckey;
		private System.Windows.Forms.Button btnHikiMeisai;
		private System.Windows.Forms.Button btnHikiIkaiMeisai;
		private System.Windows.Forms.Button btnHikiKakunin;
		private System.Windows.Forms.Button btnMinyukin;
		private System.Windows.Forms.Button btnChoshuMeisai;
		private System.Windows.Forms.Button btnHikiShukei;
	}
}