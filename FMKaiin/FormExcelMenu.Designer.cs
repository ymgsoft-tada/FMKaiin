
namespace App
{
	partial class FormExcelMenu
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
			this.btnShiminbyoinKaihi = new System.Windows.Forms.Button();
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.SuspendLayout();
			// 
			// btnShiminbyoinKaihi
			// 
			this.btnShiminbyoinKaihi.Location = new System.Drawing.Point(116, 64);
			this.btnShiminbyoinKaihi.Name = "btnShiminbyoinKaihi";
			this.btnShiminbyoinKaihi.Size = new System.Drawing.Size(180, 36);
			this.btnShiminbyoinKaihi.TabIndex = 1;
			this.btnShiminbyoinKaihi.Text = "市民病院会費請求";
			this.btnShiminbyoinKaihi.UseVisualStyleBackColor = true;
			// 
			// funckey
			// 
			this.funckey.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.funckey.Location = new System.Drawing.Point(0, 438);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(409, 25);
			this.funckey.TabIndex = 56;
			this.funckey.Text = "gcFunctionKey1";
			// 
			// FormExcelMenu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(409, 463);
			this.Controls.Add(this.funckey);
			this.Controls.Add(this.btnShiminbyoinKaihi);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormExcelMenu";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Excel出力";
			this.Controls.SetChildIndex(this.btnShiminbyoinKaihi, 0);
			this.Controls.SetChildIndex(this.funckey, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnShiminbyoinKaihi;
		private GrapeCity.Win.Bars.GcFunctionKey funckey;
	}
}