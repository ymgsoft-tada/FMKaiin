
namespace App
{
	partial class Form1
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
			this.btnBasic = new System.Windows.Forms.Button();
			this.btnStaff = new System.Windows.Forms.Button();
			this.btnTanto = new System.Windows.Forms.Button();
			this.btnIryokikanToroku = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnBasic
			// 
			this.btnBasic.Location = new System.Drawing.Point(259, 56);
			this.btnBasic.Name = "btnBasic";
			this.btnBasic.Size = new System.Drawing.Size(139, 29);
			this.btnBasic.TabIndex = 0;
			this.btnBasic.Text = "基本情報";
			this.btnBasic.UseVisualStyleBackColor = true;
			// 
			// btnStaff
			// 
			this.btnStaff.Location = new System.Drawing.Point(54, 56);
			this.btnStaff.Name = "btnStaff";
			this.btnStaff.Size = new System.Drawing.Size(139, 29);
			this.btnStaff.TabIndex = 1;
			this.btnStaff.Text = "スタッフマスタ";
			this.btnStaff.UseVisualStyleBackColor = true;
			// 
			// btnTanto
			// 
			this.btnTanto.Location = new System.Drawing.Point(404, 56);
			this.btnTanto.Name = "btnTanto";
			this.btnTanto.Size = new System.Drawing.Size(139, 29);
			this.btnTanto.TabIndex = 2;
			this.btnTanto.Text = "担当者マスタ";
			this.btnTanto.UseVisualStyleBackColor = true;
			// 
			// btnIryokikanToroku
			// 
			this.btnIryokikanToroku.Location = new System.Drawing.Point(259, 202);
			this.btnIryokikanToroku.Name = "btnIryokikanToroku";
			this.btnIryokikanToroku.Size = new System.Drawing.Size(139, 29);
			this.btnIryokikanToroku.TabIndex = 3;
			this.btnIryokikanToroku.Text = "医療機関登録";
			this.btnIryokikanToroku.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(583, 268);
			this.Controls.Add(this.btnIryokikanToroku);
			this.Controls.Add(this.btnTanto);
			this.Controls.Add(this.btnStaff);
			this.Controls.Add(this.btnBasic);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Controls.SetChildIndex(this.btnBasic, 0);
			this.Controls.SetChildIndex(this.btnStaff, 0);
			this.Controls.SetChildIndex(this.btnTanto, 0);
			this.Controls.SetChildIndex(this.btnIryokikanToroku, 0);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnBasic;
		private System.Windows.Forms.Button btnStaff;
		private System.Windows.Forms.Button btnTanto;
		private System.Windows.Forms.Button btnIryokikanToroku;
	}
}